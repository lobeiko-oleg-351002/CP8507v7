using System;
using System.Collections.Generic;
using System.Data;
//using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CP8507_v7
{
    public partial class ELZIP : Protocol
    {
        private byte fileNumber;

        private DateTime dtNow;
        private ushort numOfRecords;
        private ushort storedNumOfRecords;
        private ushort cellNumber;

        private const byte error_FC = 0xEE;

        private const int RecordsRequestLimit = 50;
        private const int RecordsRequestLimitHarmonics = 10;

        public DateTime StartDT
        {
            set
            {
                dtNow = value;
            }
            get
            {
                return dtNow;
            }
        }

        public ushort NumberOfRecords
        {
            set
            {
                numOfRecords = value;
                storedNumOfRecords = value;
            }
            get
            {
                return numOfRecords;
            }
        }

        public ushort CellNumber
        {
            set
            {
                cellNumber = value;
            }
            get
            {
                return cellNumber;
            }
        }

        public byte FileNumber
        {
            set
            {
                fileNumber = value;
            }
            get
            {
                return fileNumber;
            }
        }

        private enum ErrorCodes : byte
        {
            MB_EX_NONE = 0x00,
            COD_NO_CURRENT_DATA = 0x01,  // отсутствуют данные;
            COD_NOT_READ = 0x02, // за пределами первой записи
            COD_INCORRECT_FILE = 0x03, // нет такого пкэ
            COD_INCORRECT_DT = 0x04,  // неверно задано время
            COD_NO_DATA = 0x05,  // нет сохраненных записей
            COD_TOO_MUCH_DATA_REQ = 0x06,  // нет сохраненных записей

        }

        public ELZIP(MainForm form)
        {
            mainForm = form;
        }


        public override bool CheckProtocol(byte[] buffer)
        {
            if (buffer.Length <= 1) throw new Exception("Количество принятых данных <= 1");

            if (buffer[1] == 0xFF && buffer[2] == 0xEE) return true;

            int byteCounter = (int)Modbus.ConvertByteToUInt16(buffer, 3);
            if (buffer[1] == 0xFF && buffer.Length - 7 == byteCounter) return true;

            return false;
        }

        public override void ProcessPackage(byte[] buffer)
        {
            base.stopWatch.Stop();
            noAnswer_Timer.Stop();

            if (mainForm.CommunicationComboBox == ProtocolGlobals.ETHERNET_TAG || base.CheckCRC(buffer))
            {
                byte funcCode = (byte)(buffer[2]);
                if (funcCode == error_FC)
                {
                    DecodeErrorPackage(buffer[3]);
                    mainForm.Read31ParamButton = Globals.READ_BUTTON_TAG;
                }
                else
                {
                    ProcessReadPackage(buffer);
                }
            }
            else
            {
                if (mainForm.AutoLoopIsOn)
                {
                    mainForm.StartAutoInquryTimer(Modbus.EnquryType.INQ_READ_ELZIP_AUTOLOOP, 50);
                }
                else
                {
                    mainForm.Read31ParamButton = Globals.READ_BUTTON_TAG;
                }

                mainForm.StatusLabel = ProtocolGlobals.CRC_ERROR_MESSAGE;
            }
        }


        public void ReadData()
        {
            if (mainForm.CommunicationComboBox == ProtocolGlobals.ETHERNET_TAG)
            {
                if (!base.TryToConnectTCP(mainForm.IPAddressComboBox, CRC_RB.Port))
                    return;
            }
            if (mainForm.DeviceNumberTextBox == "")
            {
                MessageBox.Show("Некорректно введен адрес устройства");
                return;
            }

            byte[] buffer = new byte[0];
            if (FileNumber >= 9 && FileNumber <= 11) buffer = new byte[8 + 4];
            else buffer = new byte[8 + 7];
            if (mainForm.CommunicationComboBox == ProtocolGlobals.ETHERNET_TAG)
            {
                buffer = new byte[8 + 5];
            }

            int byteCounter = 0;
            buffer[byteCounter++] = Convert.ToByte(mainForm.DeviceNumberTextBox); // сетевой адрес
            buffer[byteCounter++] = 0xFF;
            buffer[byteCounter++] = FileNumber;
            // byte[] byteArray = BitConverter.GetBytes(NumberOfRecords * 6); // размер пакета
            buffer[byteCounter++] = 0x00; // byteArray[1];
            buffer[byteCounter++] = (byte)(buffer.Length - 6 - 1); // byteArray[0];

            int readRecords = RecordsRequestLimit;
            if (FileNumber <= 11)
            {
                if (NumberOfRecords < RecordsRequestLimit) readRecords = NumberOfRecords;
            }
            else if (FileNumber >= 15 && FileNumber <= 20)
            {
                readRecords = RecordsRequestLimitHarmonics;
                if (NumberOfRecords < RecordsRequestLimitHarmonics) readRecords = NumberOfRecords;
            }

            byte[] byteArray = BitConverter.GetBytes(readRecords); // размер пакета
            buffer[byteCounter++] = byteArray[1];
            buffer[byteCounter++] = byteArray[0];

            if (FileNumber >= 9 && FileNumber <= 11) // для считывания рандомных пкэ(провалы...)
            {
                byte[] tempArray = BitConverter.GetBytes(storedNumOfRecords - NumberOfRecords + 1); // размер пакета
                buffer[byteCounter++] = tempArray[1];
                buffer[byteCounter++] = tempArray[0];

                buffer[byteCounter++] = (byte)CellNumber;
            }
            else
            {
                buffer[byteCounter++] = (byte)StartDT.Second;
                buffer[byteCounter++] = (byte)StartDT.Minute;
                buffer[byteCounter++] = (byte)StartDT.Hour;
                buffer[byteCounter++] = (byte)StartDT.Day;
                buffer[byteCounter++] = (byte)StartDT.Month;
                buffer[byteCounter++] = (byte)(StartDT.Year - 2000);
            }

            if (mainForm.CommunicationComboBox == ProtocolGlobals.RS485_TAG)
            {
                ushort crc = base.CalculateCRC16(buffer);
                byteArray = BitConverter.GetBytes(crc);
                buffer[byteCounter++] = byteArray[1];
                buffer[byteCounter++] = byteArray[0];
            }

            //StoredEnquryType = enquryType;

            if (mainForm.CommunicationComboBox == ProtocolGlobals.RS485_TAG)
            {
                base.SendMessage(mainForm.ComPortLink, mainForm.ComPortsComboBox, buffer);
            }
            else if (mainForm.CommunicationComboBox == ProtocolGlobals.ETHERNET_TAG)
            {
                base.SendMessageTCP(mainForm.tcpClient, buffer);
            }
        }

        private void DecodeErrorPackage(byte errorCode)
        {
            switch (errorCode)
            {
                case (byte)ErrorCodes.COD_NOT_READ:
                    mainForm.ShowMessageBox(ProtocolGlobals.OUT_OF_FIRST_RECORD);
                    break;
                case (byte)ErrorCodes.COD_INCORRECT_FILE:
                    mainForm.ShowMessageBox("Ошибка доступа к архиву");
                    break;
                case (byte)ErrorCodes.COD_INCORRECT_DT:
                    mainForm.ShowMessageBox("Неверно задано время");
                    break;
                case (byte)ErrorCodes.COD_NO_DATA:
                    mainForm.ShowMessageBox("Данные в архиве отсутсвуют");
                    break;
                case (byte)ErrorCodes.COD_TOO_MUCH_DATA_REQ:
                    mainForm.ShowMessageBox("Превышен лимит");
                    break;
                default:
                    MainForm.WriteLog("Не обработан код ошибки ");
                    break;
            }
            // mainForm.AutoLoopIsOn = false;
        }

        private void ProcessReadPackage(byte[] buffer)
        {
            if (mainForm.AutoLoopIsOn)
            {
                DecodeData(buffer);

                if (numOfRecords > 0)
                {
                    mainForm.StartAutoInquryTimer(Modbus.EnquryType.INQ_READ_ELZIP_AUTOLOOP, 50);
                    mainForm.StatusLabel = "Чтение данных: " + (100 - 100 / ((float)storedNumOfRecords / numOfRecords)).ToString("N1") + "%";
                }
                else
                {
                    mainForm.Read31ParamButton = Globals.READ_BUTTON_TAG;
                    mainForm.StatusLabel = "Чтение данных завершено";
                }
            }
        }

        private void DecodeData(byte[] buffer)
        {
            for (int i = 5; i < buffer.Length - 2; i = i)
            {
                try
                {
                    if ((FileNumber >= 15 && FileNumber <= 20) || (FileNumber >= 0 && FileNumber <= 8))
                    {
                        int sec = (int)buffer[i++];
                        int min = (int)buffer[i++];
                        int hour = (int)buffer[i++];
                        int day = (int)buffer[i++];
                        int mon = (int)buffer[i++];
                        int year = (int)buffer[i++] + 2000;

                        DateTime dt = new DateTime(year, mon, day, hour, min, sec);

                        string temp = "Данные отсутсвуют";
                        if (FileNumber >= 15 && FileNumber <= 20)
                        {
                            if (buffer[i + 100] == 0x00) // бит достоверности, типа присутсвуют ли данные
                            {
                                temp = "";
                                for (int j = 0; j < 50; j++)
                                {
                                    byte[] shortBuffer = new byte[2];
                                    shortBuffer[0] = buffer[i + 1 + j * 2];
                                    shortBuffer[1] = buffer[i + 0 + j * 2];
                                    short value = BitConverter.ToInt16(shortBuffer, 0);
                                    float floatValue = (float)value / 100;
                                    string prefix = "Ku";
                                    if (j > 0) prefix += "(" + (j + 1) + ")";
                                    temp = prefix + " = " + floatValue.ToString("N3");
                                    //if (j % 5 == 0 && j != 30) temp += Environment.NewLine;

                                    if (j == 0) mainForm.SetPKEDataGrid((storedNumOfRecords - numOfRecords + 1).ToString(), dt.ToString("dd.MM.yyyy  HH:mm:ss"), temp);
                                    else mainForm.SetPKEDataGrid("", "", temp);
                                }
                            }
                            else if (buffer[i + 100] == 0x02)
                            {
                                numOfRecords = 0;
                                mainForm.ShowMessageBox(ProtocolGlobals.OUT_OF_FIRST_RECORD);
                                return;
                            }
                            else
                            {
                                mainForm.SetPKEDataGrid((storedNumOfRecords - numOfRecords + 1).ToString(), dt.ToString(ProtocolGlobals.DATE_TIME_FORMAT), temp);
                            }
                            i += 101;
                        }
                        else
                        {
                            if (buffer[i + 2] == 0x00) // бит достоверности, типа присутсвуют ли данные
                            {
                                byte[] shortBuffer = new byte[2];
                                shortBuffer[0] = buffer[i + 1];
                                shortBuffer[1] = buffer[i + 0];
                                short value = BitConverter.ToInt16(shortBuffer, 0);

                                float floatValue = (float)value / 100;
                                temp = floatValue.ToString("N3");
                            }
                            else if (buffer[i + 2] == 0x02)
                            {
                                numOfRecords = 0;
                                mainForm.ShowMessageBox(ProtocolGlobals.OUT_OF_FIRST_RECORD);
                                return;
                            }
                            i += 3;

                            mainForm.SetPKEDataGrid("-1", dt.ToString(ProtocolGlobals.DATE_TIME_FORMAT), temp);
                        }

                        if (fileNumber == 2)
                        {
                            dtNow = dtNow.Add(new TimeSpan(0, 0, -10));
                        }
                        else
                        {
                            dtNow = dtNow.Add(new TimeSpan(0, -10, 0));
                        }
                    }
                    else
                    {
                        //mainForm.randomPKEForm.ProgressBar(100 - (int)(100 / ((float)storedNumOfRecords / numOfRecords)));

                        i += 2;// тут номер записи
                        if (buffer[i + 13] == 0x00) // бит достоверности, типа присутсвуют ли данные
                        {
                            int sec = (int)buffer[i++];
                            int min = (int)buffer[i++];
                            int hour = (int)buffer[i++];
                            int day = (int)buffer[i++];
                            int mon = (int)buffer[i++];
                            int year = (int)buffer[i++] + 2000;

                            byte[] periodBuffer = new byte[4];
                            periodBuffer[0] = buffer[i++];
                            periodBuffer[1] = buffer[i++];
                            periodBuffer[2] = buffer[i++];
                            periodBuffer[3] = buffer[i++];
                            uint period = BitConverter.ToUInt32(periodBuffer, 0);

                            byte[] shortBuffer = new byte[2];
                            shortBuffer[0] = buffer[i++];
                            shortBuffer[1] = buffer[i++];
                            short coef = BitConverter.ToInt16(shortBuffer, 0);
                            float floatCoef = (float)coef / 100;

                            int phaseNumber = buffer[i++];

                            DateTime dt = new DateTime(year, mon, day, hour, min, sec);
                            mainForm.randomPKEForm.SetPKEDataGrid(dt.ToString(ProtocolGlobals.DATE_TIME_FORMAT), phaseNumber, period, floatCoef);
                        }
                        else if (buffer[i + 13] == 0x02)
                        {
                            numOfRecords = 0;
                            mainForm.ShowMessageBox(ProtocolGlobals.OUT_OF_FIRST_RECORD);
                            return;
                        }
                        i += 1;
                    }

                    numOfRecords--;
                    //if (numOfRecords == 0) mainForm.randomPKEForm.ProgressBar(100);
                }
                catch (Exception ex)
                {
                    MainForm.WriteLog("Ошибка декодирования журнала PKE: " + ex.Message);
                }      
            }
        }


        protected override float ConvertByteToFloat(byte[] buffer, int offset)
        {
            try
            {
                if (offset < buffer.Length && offset + 4 <= buffer.Length)
                {
                    byte[] floatBuffer = new byte[4];

                    floatBuffer[0] = buffer[offset + 3];
                    floatBuffer[1] = buffer[offset + 2];
                    floatBuffer[2] = buffer[offset + 1];
                    floatBuffer[3] = buffer[offset + 0];

                    return BitConverter.ToSingle(floatBuffer, 0);
                }
            }
            catch (Exception ex)
            {
                MainForm.WriteLog("Ошибка конвертации Byte To Float: " + ex.Message);
            }
            return -1.0f;
        }


        protected override void NoAnswer_Timer_Tick(object sender, EventArgs e)
        {
            noAnswer_Timer.Stop();
            mainForm.StatusLabel = ProtocolGlobals.NO_RESPONSE_MESSAGE;

            if (mainForm.AutoLoopIsOn)
            {
                mainForm.StartAutoInquryTimer(Modbus.EnquryType.INQ_READ_ELZIP_AUTOLOOP, 50);
            }
            else
            {
                mainForm.Read31ParamButton = Globals.READ_BUTTON_TAG;
            }
        }
    }
}

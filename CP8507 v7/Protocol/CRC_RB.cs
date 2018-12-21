using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace CP8507_v7
{
    public class CRC_RB : Protocol
    {
        private ushort funcCode;
        private ushort startIndex;
        public byte status;

        public static int Port = 49050;

        private const byte RecordsRequestLimit = 5;
        private ushort numOfRecords;
        private ushort storedNumOfRecords;


        public enum AnswerCodes : ushort
        {
            COD_OK = 0,  // ответ полный и достоверный;
            COD_INCOMPLIT = 1,  // ответ неполный, отсутствуют некоторые данные;
            COD_NOTREAD = 2,  // запрашиваемые данные не готовы;
            COD_NOTSUPPORT = 3,  // функция не поддерживается, при этом поле данных отсутствует;
            COD_NODATA = 11, // запрошенная информация не существует
            COD_LENGTHERROR = 12, // запрошенный объем информации превышает размер буфера
        }

        public enum FunctionalCodes : ushort
        {
            INTERVAL_DAY = 0x0040,
            INTERVAL_MONTH = 0x0042,
            INTERVAL_YEAR = 0x1645,
            INTERVAL_3MIN = 0x1650,
            INTERVAL_30MIN = 0x1652,
            N_ITOG_MONTH = 0x0080,
            N_ITOG_DAY = 0x1681,
            N_ITOG_YEAR = 0x1683,
            N_ITOG_3MIN = 0x1700,
            N_ITOG_30MIN = 0x1710,
            CURRENT_ENERGY = 0x1685,
            GET_DATE_TIME = 0x0001,
            SET_DATE_TIME = 0x0002,
        }

        public struct PackageData
        {
            public DateTime dt;
            public float Wa, Wa_plus, Wa_minus, Wr, Wr_plus, Wr_minus, Wr1, Wr2, Wr3, Wr4;
            public string Notation;
        }
        public PackageData[] packageDataArray;


        public CRC_RB(MainForm form)
        {
            mainForm = form;
            packageDataArray = new PackageData[0];
        }



        public ushort FunctionalCode
        {
            set
            {
                funcCode = value;
            }
            get
            {
                return funcCode;
            }
        }

        public ushort StartIndex
        {
            set
            {
                startIndex = value;
            }
            get
            {
                return startIndex;
            }
        }

        public ushort NumberOfRecords
        {
            set
            {
                numOfRecords = value;
            }
            get
            {
                return numOfRecords;
            }
        }

        public byte Status
        {
            set
            {
                status = value;
            }
            get
            {
                return status;
            }
        }



        public override bool CheckProtocol(byte[] buffer)
        {
            if (buffer.Length <= 1) throw new Exception("Количество принятых данных <= 1");

            if (mainForm.CommunicationComboBox == ProtocolGlobals.ETHERNET_TAG)
            {
                if (buffer[0] == 0xC3 && buffer[buffer.Length - 2] == 0x50 && buffer[buffer.Length - 1] == 0x55)
                    return true;
            }
            else if (base.CheckCRC(buffer))
            {
                if (buffer[0] == 0xC3 && buffer[buffer.Length - 4] == 0x50 && buffer[buffer.Length - 3] == 0x55)
                    return true;
            }
            return false;
        }

        public override void ProcessPackage(byte[] buffer)
        {
            base.stopWatch.Stop();
            noAnswer_Timer.Stop();

            if (mainForm.CommunicationComboBox == ProtocolGlobals.ETHERNET_TAG || base.CheckCRC(buffer))
            {
                ProcessReadPackage(buffer);
            }
            else
            {
                mainForm.StatusLabel = ProtocolGlobals.CRC_ERROR_MESSAGE;

                if (mainForm.AutoLoopIsOn)
                {
                    mainForm.StartAutoInquryTimer(Modbus.EnquryType.INQ_READ_CRCRB_AUTOLOOP, 50);
                }
            }
        }

        private void ProcessReadPackage(byte[] buffer)
        {
            if (mainForm.AutoLoopIsOn)
            {
                DecodeData(buffer);
            }
            mainForm.StatusLabel = ProtocolGlobals.OPERATION_SUCCESS_MESSAGE;
        }

        private void DecodeData(byte[] buffer)
        {
            if (buffer[3] > 16)
            {
                Array.Resize(ref packageDataArray, packageDataArray.Length + 1);
                float Wa, Wa_plus, Wa_minus, Wr, Wr_plus, Wr_minus, Wr1, Wr2, Wr3, Wr4;
                int addr = 6;

                if (buffer[addr] == 0xFF && buffer[addr + 1] == 0xFF && buffer[addr + 2] == 0xFF && buffer[addr + 3] == 0xFF)
                {
                    Wa = 0;
                    Wa_plus = 0;
                    Wa_minus = 0;
                    Wr = 0;
                    Wr_plus = 0;
                    Wr_minus = 0;
                    Wr1 = 0;
                    Wr2 = 0;
                    Wr3 = 0;
                    Wr4 = 0;
                    packageDataArray[packageDataArray.Length - 1].Notation = "Нет информации";

                    this.status = buffer[46];
                }
                else
                {
                    Wa = BitConverter.ToSingle(buffer, addr);
                    addr += 4;
                    Wa_plus = BitConverter.ToSingle(buffer, addr);
                    addr += 4;
                    Wa_minus = BitConverter.ToSingle(buffer, addr);
                    addr += 4;
                    Wr = BitConverter.ToSingle(buffer, addr);
                    addr += 4;
                    Wr_plus = BitConverter.ToSingle(buffer, addr);
                    addr += 4;
                    Wr_minus = BitConverter.ToSingle(buffer, addr);
                    addr += 4;
                    Wr1 = BitConverter.ToSingle(buffer, addr);
                    addr += 4;
                    Wr2 = BitConverter.ToSingle(buffer, addr);
                    addr += 4;
                    Wr3 = BitConverter.ToSingle(buffer, addr);
                    addr += 4;
                    Wr4 = BitConverter.ToSingle(buffer, addr);
                    addr += 4;

                    packageDataArray[packageDataArray.Length - 1].Notation = "";
                }

                packageDataArray[packageDataArray.Length - 1].Wa = Wa;
                packageDataArray[packageDataArray.Length - 1].Wa_plus = Wa_plus;
                packageDataArray[packageDataArray.Length - 1].Wa_minus = Wa_minus;

                packageDataArray[packageDataArray.Length - 1].Wr = Wr;
                packageDataArray[packageDataArray.Length - 1].Wr_plus = Wr_plus;
                packageDataArray[packageDataArray.Length - 1].Wr_minus = Wr_minus;

                packageDataArray[packageDataArray.Length - 1].Wr1 = Wr1;
                packageDataArray[packageDataArray.Length - 1].Wr2 = Wr2;
                packageDataArray[packageDataArray.Length - 1].Wr3 = Wr3;
                packageDataArray[packageDataArray.Length - 1].Wr4 = Wr4;

                int sec = 0;
                int min = (int)buffer[47];
                int hour = (int)buffer[48];
                int day = (int)buffer[49];
                int mon = (int)buffer[50];
                int year = (int)buffer[51] + 2000;

                DateTime dt = new DateTime(year, mon, day, hour, min, sec);

                packageDataArray[packageDataArray.Length - 1].dt = dt;

                mainForm.SetEnergyDataGrid(packageDataArray[packageDataArray.Length - 1]);
                mainForm.SetEnergyChart(packageDataArray[packageDataArray.Length - 1]);
            }
            else
            {
                numOfRecords = 1;
                this.status = buffer[6];
            }


            if (--numOfRecords > 0)
            {
                startIndex++;
                mainForm.StartAutoInquryTimer(Modbus.EnquryType.INQ_READ_CRCRB_AUTOLOOP, 50);
            }
            else
            {
                if (this.status == (ushort)AnswerCodes.COD_OK) mainForm.ShowMessageBox(" Чтение данных окончено! Данные достоверны, ответ полный."); // \n\r 
                else if (this.status == (ushort)AnswerCodes.COD_INCOMPLIT) mainForm.ShowMessageBox(" Чтение данных окончено! Ответ неполный, отсутствуют некоторые данные."); // \n\r
                else if (this.status == (ushort)AnswerCodes.COD_NOTREAD) mainForm.ShowMessageBox(" Чтение данных окончено! Достигнута первая запись"); // /n/r
                else if (this.status == (ushort)AnswerCodes.COD_NOTSUPPORT) mainForm.ShowMessageBox(" Функция не поддерживается");
                else if (this.status == (ushort)AnswerCodes.COD_NODATA) mainForm.ShowMessageBox(" Записи отсутствуют");
                else if (this.status == (ushort)AnswerCodes.COD_LENGTHERROR) mainForm.ShowMessageBox(" Попытка чтения за пределы диапазона");

                mainForm.ReadEnergyButtonPush();

                // break;
            }
            //  }


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

            ushort dataLen = 0x12;
            byte[] buffer = new byte[18];
            if (mainForm.CommunicationComboBox == ProtocolGlobals.ETHERNET_TAG)
            {
                dataLen = 0x10;
                buffer = new byte[16];
            }
            buffer[0] = 0x55;
            buffer[1] = Convert.ToByte(mainForm.DeviceNumberTextBox); // сетевой адрес
            byte[] byteArray = BitConverter.GetBytes(dataLen); // размер пакета
            buffer[2] = byteArray[1];
            buffer[3] = byteArray[0];
            byteArray = BitConverter.GetBytes(funcCode); // номер функции
            buffer[4] = byteArray[1];
            buffer[5] = byteArray[0];
            buffer[6] = 0x00; // номер старнотового канала
            buffer[7] = 0x00;
            buffer[8] = 0x00; // количество каналов(читаем все)
            buffer[9] = 0x0A;
            byteArray = BitConverter.GetBytes(startIndex); // начальный индекс
            buffer[10] = byteArray[1];
            buffer[11] = byteArray[0];
            buffer[12] = 0x00;
         //   byte readRecords = RecordsRequestLimit;
          //  if (NumberOfRecords < RecordsRequestLimit) readRecords = (byte)NumberOfRecords;
            buffer[13] = 0x01;
            buffer[14] = 0x50;
            buffer[15] = 0x55;

            if (mainForm.CommunicationComboBox == ProtocolGlobals.RS485_TAG)
            {
                ushort crc = base.CalculateCRC16(buffer);
                byteArray = BitConverter.GetBytes(crc);
                buffer[16] = byteArray[1];
                buffer[17] = byteArray[0];
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


        protected override void NoAnswer_Timer_Tick(object sender, EventArgs e)
        {
            noAnswer_Timer.Stop();
            mainForm.StatusLabel = ProtocolGlobals.NO_RESPONSE_MESSAGE;

            if (mainForm.AutoLoopIsOn)
            {
                mainForm.StartAutoInquryTimer(Modbus.EnquryType.INQ_READ_CRCRB_AUTOLOOP, 50);
            }
        }
    }
}

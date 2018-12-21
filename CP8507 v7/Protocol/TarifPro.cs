using System;
using System.Collections.Generic;
using System.Drawing;
//using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CP8507_v7
{
    public class TarifPro : Protocol
    {
        public byte storedNumOfSeason;
        private byte seasonsToTX, seasonsToRX;
        private List<byte>[] RXTXBuffer;
        private ProgressForm progressFrom;
        private bool isRead;
        private List<SeasonStruct> RXSeasons;
        //private const short error_FC = 0xFFFF;

        public TarifPro(MainForm form)
        {
            mainForm = form;
            progressFrom = new ProgressForm(this);
        }

        public override bool CheckProtocol(byte[] buffer)
        {
            if (buffer.Length <= 1) throw new Exception("Количество принятых данных <= 1");

            if (buffer[0] > 0 && buffer[0] < 247 && ((buffer[1] == 0x55 && buffer[2] == 0xFF) || (buffer[1] == 0x75 && buffer[2] == 0xFF)))
                return true;
            else
                return false;
        }

        public override void ProcessPackage(byte[] buffer)
        {
            base.stopWatch.Stop();
            noAnswer_Timer.Stop();

            if (mainForm.CommunicationComboBox == ProtocolGlobals.ETHERNET_TAG || base.CheckCRC(buffer))
            {
                if (buffer[1] == 0x75)
                {
                    DecodeData(buffer);
                }
                else if (buffer[1] == 0x55)
                {
                    byte funcCode = buffer[3];
                    if (funcCode == 0x00)
                    {
                        WriteData();
                    }
                    else if (funcCode == 0x01)
                    {
                        try
                        {
                            progressFrom.Close();
                        }
                        catch { }
                        mainForm.ShowMessageBox("Возникла ошибка декодирования данных прибором");
                    }
                    else if (funcCode == 0x07)
                    {
                        mainForm.StatusLabel = "На изменяемый параметр установлена аппаратная защита";
                        mainForm.modbus.storedEnquryTypeForPassword = Modbus.EnquryType.INQ_WRITE_TARIF_PRO;    
                        mainForm.EnterPassword(0);               
                    }
                }
            }
            else
            {
                if (storedNumOfSeason >= 100)
                {
                    progressFrom.CloseForm();
                    return;
                }
                progressFrom.LabelText = ProtocolGlobals.CRC_ERROR_MESSAGE;
                storedNumOfSeason--;
                if (isRead)
                    mainForm.StartAutoInquryTimer(Modbus.EnquryType.INQ_READ_TARIF_PRO, 1000);
                else
                    mainForm.StartAutoInquryTimer(Modbus.EnquryType.INQ_WRITE_TARIF_PRO, 1000);
            }
        }

        public void PrepareForTX(List<byte>[] data)
        {
            RXTXBuffer = new List<byte>[data.Length];
            for (int i = 0; i < data.Length; i++) RXTXBuffer[i] = new List<byte>();
            data.CopyTo(RXTXBuffer, 0);
            seasonsToTX = (byte)data.Length;
            storedNumOfSeason = 1;
            isRead = false;

            progressFrom = new ProgressForm(this);
            progressFrom.progressBar.Maximum = seasonsToTX;
            progressFrom.progressBar.Minimum = 0;
            WriteData();

            progressFrom.StartPosition = FormStartPosition.Manual;
            progressFrom.Location = new Point(mainForm.Left + mainForm.Width / 3, mainForm.Top + mainForm.Height / 3);
            progressFrom.ShowDialog();
        }

        public void WriteData()
        {
            if (storedNumOfSeason > seasonsToTX)
            {
                if (storedNumOfSeason >= 100)
                {
                    progressFrom.CloseForm();
                    return;
                }
                mainForm.ShowMessageBox(ProtocolGlobals.RECORDING_DONE_MESSAGE);
                progressFrom.ProgressBarValue = seasonsToTX;
                progressFrom.CloseForm();
                return;
            }
            progressFrom.ProgressBarValue = storedNumOfSeason;
            progressFrom.LabelText = "Запись сезона " + storedNumOfSeason;

            //if (mainForm.CommunicationComboBox == "Ethernet")
            //{
            //    if (!base.TryToConnectTCP(mainForm.IPAddressComboBox, Modbus.Port))
            //        return;
            //}
            if (mainForm.DeviceNumberTextBox == "")
            {
                mainForm.ShowMessageBox(ProtocolGlobals.INCORRECT_DEVICE_ADDRESS_MESSAGE);
                return;
            }

            //if (enquryType != EnquryType.INQ_SET_PASSWORD)
            //{
            //    // сохраняем для повторной отправки после  ввода пароля
            //    storedDataForPassword = new byte[data.Length];
            //    Array.Copy(data, storedDataForPassword, data.Length);
            //    storedEnquryTypeForPassword = enquryType;
            //}
            byte[] buffer = new byte[RXTXBuffer[storedNumOfSeason - 1].Count + 9];
            byte[] byteArray;
            int byteIndex = 0;

            buffer[byteIndex++] = Convert.ToByte(mainForm.DeviceNumberTextBox);
            buffer[byteIndex++] = 0x55;
            buffer[byteIndex++] = 0xFF;
            byteArray = BitConverter.GetBytes(RXTXBuffer[storedNumOfSeason - 1].Count + 2);
            buffer[byteIndex++] = byteArray[1];
            buffer[byteIndex++] = byteArray[0];

            buffer[byteIndex++] = storedNumOfSeason;
            buffer[byteIndex++] = seasonsToTX;

            for (int i = 0; i < RXTXBuffer[storedNumOfSeason - 1].Count; i++)
            {
                buffer[byteIndex++] = RXTXBuffer[storedNumOfSeason - 1][i];
            }

            if (mainForm.CommunicationComboBox == ProtocolGlobals.RS485_TAG)
            {
                ushort crc = base.CalculateCRC16(buffer);
                byteArray = BitConverter.GetBytes(crc);
                buffer[byteIndex++] = byteArray[1];
                buffer[byteIndex++] = byteArray[0];
            }

            storedNumOfSeason++;

            if (mainForm.CommunicationComboBox == ProtocolGlobals.RS485_TAG)
            {
                base.SendMessage(mainForm.ComPortLink, mainForm.ComPortsComboBox, buffer);
            }
            //else if (mainForm.CommunicationComboBox == "Ethernet")
            //{
            //    base.SendMessageTCP(mainForm.tcpClient, buffer);
            //}
        }

        public void PrepareForRX()
        {
            RXSeasons = new List<SeasonStruct>();
            RXTXBuffer = new List<byte>[12];
            for (int i = 0; i < RXTXBuffer.Length; i++) RXTXBuffer[i] = new List<byte>();
            storedNumOfSeason = 1;
            seasonsToRX = 12;
            isRead = true;

            progressFrom = new ProgressForm(this);
            progressFrom.progressBar.Maximum = 12;
            progressFrom.progressBar.Minimum = 0;
            ReadData();

            progressFrom.StartPosition = FormStartPosition.Manual;
            progressFrom.Location = new Point(mainForm.Left + mainForm.Width / 3, mainForm.Top + mainForm.Height / 3);
            progressFrom.ShowDialog();
        }

        public void ReadData()
        {
            //if (mainForm.CommunicationComboBox == "Ethernet")
            //{
            //    if (!base.TryToConnectTCP(mainForm.IPAddressComboBox, Modbus.Port))
            //        return;
            //}
            if (mainForm.DeviceNumberTextBox == "")
            {
                mainForm.ShowMessageBox(ProtocolGlobals.INCORRECT_DEVICE_ADDRESS_MESSAGE);
                return;
            }

            if (storedNumOfSeason > seasonsToRX)
            {
                if (storedNumOfSeason >= 100)
                {
                    progressFrom.CloseForm();
                    return;
                }
                progressFrom.ProgressBarValue = seasonsToRX;
                progressFrom.CloseForm();
                SeasonStruct[] Seasons = new SeasonStruct[RXSeasons.Count];
                RXSeasons.CopyTo(Seasons);
                mainForm.tarificationForm.UnpackData(Seasons);
                return;
            }

            // progressFrom.ProgressBarValue = storedNumOfSeason;
            progressFrom.LabelText = "Чтение сезона " + storedNumOfSeason;

            byte[] buffer = new byte[8];
            byte[] byteArray;
            int byteIndex = 0;

            buffer[byteIndex++] = Convert.ToByte(mainForm.DeviceNumberTextBox);
            buffer[byteIndex++] = 0x75;
            buffer[byteIndex++] = 0xFF;
            byteArray = BitConverter.GetBytes(1);
            buffer[byteIndex++] = byteArray[1];
            buffer[byteIndex++] = byteArray[0];

            buffer[byteIndex++] = storedNumOfSeason;

            if (mainForm.CommunicationComboBox == ProtocolGlobals.RS485_TAG)
            {
                ushort crc = base.CalculateCRC16(buffer);
                byteArray = BitConverter.GetBytes(crc);
                buffer[byteIndex++] = byteArray[1];
                buffer[byteIndex++] = byteArray[0];
            }

            storedNumOfSeason++;

            if (mainForm.CommunicationComboBox == ProtocolGlobals.RS485_TAG)
            {
                base.SendMessage(mainForm.ComPortLink, mainForm.ComPortsComboBox, buffer);
            }
            //else if (mainForm.CommunicationComboBox == "Ethernet")
            //{
            //    base.SendMessageTCP(mainForm.tcpClient, buffer);
            //}
        }

        private void DecodeData(byte[] buffer)
        {
            SeasonStruct season = new SeasonStruct();

            try
            {
                int byteIndex = 6;
                seasonsToRX = buffer[byteIndex++];
                progressFrom.ProgressBarMax = seasonsToRX;
                progressFrom.ProgressBarValue = storedNumOfSeason - 1;

                int day = buffer[byteIndex++];
                int month = buffer[byteIndex++];
                season.StartDT = new DateTime(2016, month, day);

                day = buffer[byteIndex++];
                month = buffer[byteIndex++];
                season.EndDT = new DateTime(2016, month, day);

                season.SeasonActivated = true;
                season.SeasonNumber = RXSeasons.Count + 1;

                int WKintervals = buffer[byteIndex++];
                for (int i = 0; i < WKintervals; i++)
                {
                    int startHour = buffer[byteIndex] & 0x7F;
                    int startMin = 0;
                    if ((buffer[byteIndex++] & 0x80) == 0x80) startMin = 30;

                    int endHour = buffer[byteIndex] & 0x7F;
                    int endMin = 0;
                    if ((buffer[byteIndex++] & 0x80) == 0x80) endMin = 30;

                    int tarif = buffer[byteIndex++];

                    season.WorkDays.Tarif[tarif - 1].Interval[season.WorkDays.Tarif[tarif - 1].IntervalsActivated].Start = new TimeSpan(startHour, startMin, 0);
                    season.WorkDays.Tarif[tarif - 1].Interval[season.WorkDays.Tarif[tarif - 1].IntervalsActivated].End = new TimeSpan(endHour, endMin, 0);
                    season.WorkDays.Tarif[tarif - 1].IntervalsActivated++;
                }

                int Satintervals = buffer[byteIndex++];
                for (int i = 0; i < Satintervals; i++)
                {
                    int startHour = buffer[byteIndex] & 0x7F;
                    int startMin = 0;
                    if ((buffer[byteIndex++] & 0x80) == 0x80) startMin = 30;

                    int endHour = buffer[byteIndex] & 0x7F;
                    int endMin = 0;
                    if ((buffer[byteIndex++] & 0x80) == 0x80) endMin = 30;

                    int tarif = buffer[byteIndex++];

                    season.Saturday.Tarif[tarif - 1].Interval[season.Saturday.Tarif[tarif - 1].IntervalsActivated].Start = new TimeSpan(startHour, startMin, 0);
                    season.Saturday.Tarif[tarif - 1].Interval[season.Saturday.Tarif[tarif - 1].IntervalsActivated].End = new TimeSpan(endHour, endMin, 0);
                    season.Saturday.Tarif[tarif - 1].IntervalsActivated++;
                }

                int Sunintervals = buffer[byteIndex++];
                for (int i = 0; i < Sunintervals; i++)
                {
                    int startHour = buffer[byteIndex] & 0x7F;
                    int startMin = 0;
                    if ((buffer[byteIndex++] & 0x80) == 0x80) startMin = 30;

                    int endHour = buffer[byteIndex] & 0x7F;
                    int endMin = 0;
                    if ((buffer[byteIndex++] & 0x80) == 0x80) endMin = 30;

                    int tarif = buffer[byteIndex++];

                    season.Sunday.Tarif[tarif - 1].Interval[season.Sunday.Tarif[tarif - 1].IntervalsActivated].Start = new TimeSpan(startHour, startMin, 0);
                    season.Sunday.Tarif[tarif - 1].Interval[season.Sunday.Tarif[tarif - 1].IntervalsActivated].End = new TimeSpan(endHour, endMin, 0);
                    season.Sunday.Tarif[tarif - 1].IntervalsActivated++;
                }

                int fixedDays = buffer[byteIndex++];
                for (int i = 0; i < fixedDays; i++)
                {
                    day = buffer[byteIndex++];
                    month = buffer[byteIndex++];
                    season.FixedDays[i].DayActivated = true;
                    season.FixedDays[i].Date = new DateTime(2016, month, day);

                    int FixedIntervals = buffer[byteIndex++];
                    for (int j = 0; j < FixedIntervals; j++)
                    {
                        int startHour = buffer[byteIndex] & 0x7F;
                        int startMin = 0;
                        if ((buffer[byteIndex++] & 0x80) == 0x80) startMin = 30;

                        int endHour = buffer[byteIndex] & 0x7F;
                        int endMin = 0;
                        if ((buffer[byteIndex++] & 0x80) == 0x80) endMin = 30;

                        int tarif = buffer[byteIndex++];

                        season.FixedDays[i].Tarif[tarif - 1].Interval[season.FixedDays[i].Tarif[tarif - 1].IntervalsActivated].Start = new TimeSpan(startHour, startMin, 0);
                        season.FixedDays[i].Tarif[tarif - 1].Interval[season.FixedDays[i].Tarif[tarif - 1].IntervalsActivated].End = new TimeSpan(endHour, endMin, 0);
                        season.FixedDays[i].Tarif[tarif - 1].IntervalsActivated++;
                    }
                }

                RXSeasons.Add(season);
                mainForm.StartAutoInquryTimer(Modbus.EnquryType.INQ_READ_TARIF_PRO, 500);
            }
            catch
            {
                mainForm.ShowMessageBox("Ошибка приема данных с прибора");
            }
        }

        protected override void NoAnswer_Timer_Tick(object sender, EventArgs e)
        {
            noAnswer_Timer.Stop();
            if (storedNumOfSeason >= 100)
            {
                progressFrom.CloseForm();
                return;
            }
            progressFrom.LabelText = "Нет ответа от устройства";
            storedNumOfSeason--;
            if (isRead)
                mainForm.StartAutoInquryTimer(Modbus.EnquryType.INQ_READ_TARIF_PRO, 1000);
            else
                mainForm.StartAutoInquryTimer(Modbus.EnquryType.INQ_WRITE_TARIF_PRO, 1000);
        }

    }
}

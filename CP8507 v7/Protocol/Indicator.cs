using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace CP8507_v7
{
    public class Indicator : Protocol
    {
        public Indicator(MainForm form)
        {
            mainForm = form;
        }

        public override bool CheckProtocol(byte[] buffer)
        {
            if ((buffer[0] == 0x03 && buffer[1] == 0x00 && buffer[2] == 0xF0) || (buffer[0] == 0x03 && buffer[1] == 0x00 && buffer[2] == 0xE0) || (buffer[0] == 0x03 && buffer[1] == 0x00 && buffer[2] == 0x02))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private delegate void Delegate(byte[] buffer);

        public override void ProcessPackage(byte[] buffer)
        {
            if (mainForm.InvokeRequired)
            {
                mainForm.BeginInvoke(new Delegate(ProcessPackage), new object[] { buffer });
                return;
            }

            base.stopWatch.Stop();
            base.noAnswer_Timer.Stop();

            if (base.CheckCRC(buffer))
            {
                if (buffer[2] == 0xF0) // чтение
                {
                    DecodeReadPackage(buffer);
                    mainForm.StatusLabel = ProtocolGlobals.OPERATION_SUCCESS_MESSAGE;
                }
                else if (buffer[2] == 0xE0) // запись
                {
                    mainForm.StatusLabel = ProtocolGlobals.RECORDING_DONE_MESSAGE;
                    WriteData2();
                   // MessageBox.Show("Информация записана");
                }
                else if (buffer[2] == 0x02) // запись
                {
                    mainForm.StatusLabel = ProtocolGlobals.RECORDING_DONE_MESSAGE;
                    // MessageBox.Show("Информация записана");
                }
            }
            else
            {
                mainForm.StatusLabel = ProtocolGlobals.CRC_ERROR_MESSAGE;
            }
        }

        public void ReadData()
        {
            byte[] buffer = new byte[5];
            buffer[0] = 0x03;
            buffer[1] = 0x00;
            buffer[2] = 0xF0;
            ushort crc = base.CalculateCRC16(buffer);
            byte[] byteArray = BitConverter.GetBytes(crc);
            buffer[3] = byteArray[1];
            buffer[4] = byteArray[0];

            base.SendMessage(mainForm.ComPortLink, mainForm.ComPortsComboBox, buffer);
        }

        public void WriteData()
        {
            try
            {
                int byteIndex = 0;
                byte[] bytes;
                float temp;
                byte[] buffer = new byte[65];

                ///
                buffer[byteIndex++] = 0x03;
                buffer[byteIndex++] = 0x00;
                buffer[byteIndex++] = 0xE0;

                ///
                buffer[byteIndex++] = (byte)mainForm.Row1IndicatorParamComboBox;

                bytes = BitConverter.GetBytes(1.0f);
                bytes.CopyTo(buffer, byteIndex);
                byteIndex += 4;

                bytes = BitConverter.GetBytes(-1.0f);
                bytes.CopyTo(buffer, byteIndex);
                byteIndex += 4;

                temp = Convert.ToSingle(mainForm.ParamIndicator1MaxUstavkaTextBox) / 100;
                bytes = BitConverter.GetBytes(temp);
                bytes.CopyTo(buffer, byteIndex);
                byteIndex += 4;

                temp = Convert.ToSingle(mainForm.ParamIndicator1MinUstavkaTextBox) / 100;
                bytes = BitConverter.GetBytes(temp);
                bytes.CopyTo(buffer, byteIndex);
                byteIndex += 4;

                ///
                buffer[byteIndex++] = (byte)mainForm.Row2IndicatorParamComboBox;

                bytes = BitConverter.GetBytes(1.0f);
                bytes.CopyTo(buffer, byteIndex);
                byteIndex += 4;

                bytes = BitConverter.GetBytes(-1.0f);
                bytes.CopyTo(buffer, byteIndex);
                byteIndex += 4;

                temp = Convert.ToSingle(mainForm.ParamIndicator2MaxUstavkaTextBox) / 100;
                bytes = BitConverter.GetBytes(temp);
                bytes.CopyTo(buffer, byteIndex);
                byteIndex += 4;

                temp = Convert.ToSingle(mainForm.ParamIndicator2MinUstavkaTextBox) / 100;
                bytes = BitConverter.GetBytes(temp);
                bytes.CopyTo(buffer, byteIndex);
                byteIndex += 4;

                ///
                buffer[byteIndex++] = (byte)mainForm.Row3IndicatorParamComboBox;

                bytes = BitConverter.GetBytes(1.0f);
                bytes.CopyTo(buffer, byteIndex);
                byteIndex += 4;

                bytes = BitConverter.GetBytes(-1.0f);
                bytes.CopyTo(buffer, byteIndex);
                byteIndex += 4;

                temp = Convert.ToSingle(mainForm.ParamIndicator3MaxUstavkaTextBox) / 100;
                bytes = BitConverter.GetBytes(temp);
                bytes.CopyTo(buffer, byteIndex);
                byteIndex += 4;

                temp = Convert.ToSingle(mainForm.ParamIndicator3MinUstavkaTextBox) / 100;
                bytes = BitConverter.GetBytes(temp);
                bytes.CopyTo(buffer, byteIndex);
                byteIndex += 4;

                ///
                bytes = BitConverter.GetBytes(1.0f);
                bytes.CopyTo(buffer, byteIndex);
                byteIndex += 4;

                bytes = BitConverter.GetBytes(1.0f);
                bytes.CopyTo(buffer, byteIndex);
                byteIndex += 4;

                bytes = BitConverter.GetBytes((byte)mainForm.SelectIndicatorShcemeTypeRadioButton);
                bytes.CopyTo(buffer, byteIndex++);

                //buffer[byteIndex++] = 1;
                //buffer[byteIndex++] = 0;
                //buffer[byteIndex++] = 1;

                ///
                ushort crc = base.CalculateCRC16(buffer);
                byte[] byteArray = BitConverter.GetBytes(crc);
                buffer[byteIndex++] = byteArray[1];
                buffer[byteIndex] = byteArray[0];

                base.SendMessage(mainForm.ComPortLink, mainForm.ComPortsComboBox, buffer);
            }
            catch
            {
                MessageBox.Show("Неверные входные данные");
            }
        }

        public void WriteData2()
        {
            int byteIndex = 0;
            byte[] buffer = new byte[6];

            ///
            buffer[byteIndex++] = 0x03;
            buffer[byteIndex++] = 0x00;
            buffer[byteIndex++] = 0x02;

            byte temp = 0;
            if (mainForm.line1_checkBox.Checked) temp |= 1;
            if (mainForm.line2_checkBox.Checked) temp |= 1 << 1;
            if (mainForm.line3_checkBox.Checked) temp |= 1 << 2;
            buffer[byteIndex++] = temp;

            ///
            ushort crc = base.CalculateCRC16(buffer);
            byte[] byteArray = BitConverter.GetBytes(crc);
            buffer[byteIndex++] = byteArray[1];
            buffer[byteIndex] = byteArray[0];

            base.SendMessage(mainForm.ComPortLink, mainForm.ComPortsComboBox, buffer);
        }


        private void DecodeReadPackage(byte[] buffer)
        {
            try
            {
                int byteIndex = 3;

                mainForm.Row1IndicatorParamComboBox = buffer[byteIndex++];

                byteIndex += 8;
                mainForm.ParamIndicator1MaxUstavkaTextBox = (BitConverter.ToSingle(buffer, byteIndex) * 100).ToString("N0");

                byteIndex += 4;
                mainForm.ParamIndicator1MinUstavkaTextBox = (BitConverter.ToSingle(buffer, byteIndex) * 100).ToString("N0");

                byteIndex += 4;
                mainForm.Row2IndicatorParamComboBox = buffer[byteIndex++];

                byteIndex += 8;
                mainForm.ParamIndicator2MaxUstavkaTextBox = (BitConverter.ToSingle(buffer, byteIndex) * 100).ToString("N0");

                byteIndex += 4;
                mainForm.ParamIndicator2MinUstavkaTextBox = (BitConverter.ToSingle(buffer, byteIndex) * 100).ToString("N0");

                byteIndex += 4;
                mainForm.Row3IndicatorParamComboBox = buffer[byteIndex++];

                byteIndex += 8;
                mainForm.ParamIndicator3MaxUstavkaTextBox = (BitConverter.ToSingle(buffer, byteIndex) * 100).ToString("N0");

                byteIndex += 4;
                mainForm.ParamIndicator3MinUstavkaTextBox = (BitConverter.ToSingle(buffer, byteIndex) * 100).ToString("N0");

                byteIndex += 12;
                mainForm.SelectIndicatorShcemeTypeRadioButton = (int)buffer[byteIndex++];

                if (buffer[byteIndex++] > 0) mainForm.line1_checkBox.Checked = true;
                else mainForm.line1_checkBox.Checked = false;

                if (buffer[byteIndex++] > 0) mainForm.line2_checkBox.Checked = true;
                else mainForm.line2_checkBox.Checked = false;

                if (buffer[byteIndex] > 0) mainForm.line3_checkBox.Checked = true;
                else mainForm.line3_checkBox.Checked = false;
            }
            catch
            {
                MessageBox.Show("Ошибка обработки данных");
            }
        }

        private delegate void Delegate_2(object sender, EventArgs e);

        protected override void NoAnswer_Timer_Tick(object sender, EventArgs e)
        {
            if (mainForm.InvokeRequired)
            {
                mainForm.BeginInvoke(new Delegate_2(NoAnswer_Timer_Tick), new object[] { sender, e });
                return;
            }

            noAnswer_Timer.Stop();
            mainForm.StatusLabel = ProtocolGlobals.NO_RESPONSE_MESSAGE;

        }

    }
}

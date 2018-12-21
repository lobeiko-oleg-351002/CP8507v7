using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CP8507_v7
{
    public class Indicator_23 : Protocol
    {
        public struct TParamDevInfo
        {
            public CheckBox CBOnOff;    //
            public TextBox EAddr;
            public ComboBox CBNumParam;    //
        }
        public struct TParamIndInfo
        {
            public ComboBox CBAlgoritm;    //
            public TabControl PCDevs;
            public TParamDevInfo[] PDInfo;

            public CheckBox IsUsed;
            public TextBox DstAddress;
            public TextBox TimeOut;
            public TextBox DstValueAddress;
            public TextBox SrcValueAddress;
            public ComboBox PointPosition;
            public ComboBox FloatBytes;
        }

        TParamIndInfo[] PIAllInfo = new TParamIndInfo[3];

        public Indicator_23(MainForm form)
        {
            mainForm = form;

            PIAllInfo[0].PDInfo = new TParamDevInfo[10];
            PIAllInfo[1].PDInfo = new TParamDevInfo[10];
            PIAllInfo[2].PDInfo = new TParamDevInfo[10];

            PIAllInfo[0].CBAlgoritm = mainForm.comboBox1;
            PIAllInfo[0].PDInfo[0].CBOnOff = mainForm.checkBox2;
            PIAllInfo[0].PDInfo[0].EAddr = mainForm.textBox1;
            PIAllInfo[0].PDInfo[0].CBNumParam = mainForm.comboBox2;
            PIAllInfo[0].PDInfo[1].CBOnOff = mainForm.checkBox3;
            PIAllInfo[0].PDInfo[1].EAddr = mainForm.textBox3;
            PIAllInfo[0].PDInfo[1].CBNumParam = mainForm.comboBox3;
            PIAllInfo[0].PDInfo[2].CBOnOff = mainForm.checkBox4;
            PIAllInfo[0].PDInfo[2].EAddr = mainForm.textBox4;
            PIAllInfo[0].PDInfo[2].CBNumParam = mainForm.comboBox4;
            PIAllInfo[0].PDInfo[3].CBOnOff = mainForm.checkBox5;
            PIAllInfo[0].PDInfo[3].EAddr = mainForm.textBox5;
            PIAllInfo[0].PDInfo[3].CBNumParam = mainForm.comboBox5;
            PIAllInfo[0].PDInfo[4].CBOnOff = mainForm.checkBox6;
            PIAllInfo[0].PDInfo[4].EAddr = mainForm.textBox6;
            PIAllInfo[0].PDInfo[4].CBNumParam = mainForm.comboBox6;
            PIAllInfo[0].PDInfo[5].CBOnOff = mainForm.checkBox7;
            PIAllInfo[0].PDInfo[5].EAddr = mainForm.textBox7;
            PIAllInfo[0].PDInfo[5].CBNumParam = mainForm.comboBox7;
            PIAllInfo[0].PDInfo[6].CBOnOff = mainForm.checkBox8;
            PIAllInfo[0].PDInfo[6].EAddr = mainForm.textBox8;
            PIAllInfo[0].PDInfo[6].CBNumParam = mainForm.comboBox8;
            PIAllInfo[0].PDInfo[7].CBOnOff = mainForm.checkBox9;
            PIAllInfo[0].PDInfo[7].EAddr = mainForm.textBox9;
            PIAllInfo[0].PDInfo[7].CBNumParam = mainForm.comboBox9;
            PIAllInfo[0].PDInfo[8].CBOnOff = mainForm.checkBox10;
            PIAllInfo[0].PDInfo[8].EAddr = mainForm.textBox10;
            PIAllInfo[0].PDInfo[8].CBNumParam = mainForm.comboBox10;
            PIAllInfo[0].PDInfo[9].CBOnOff = mainForm.checkBox11;
            PIAllInfo[0].PDInfo[9].EAddr = mainForm.textBox11;
            PIAllInfo[0].PDInfo[9].CBNumParam = mainForm.comboBox11;

            PIAllInfo[0].IsUsed = mainForm.checkBox32;
            PIAllInfo[0].DstAddress = mainForm.textBox36;
            PIAllInfo[0].DstValueAddress = mainForm.textBox35;
            PIAllInfo[0].SrcValueAddress = mainForm.textBox42;
            PIAllInfo[0].TimeOut = mainForm.textBox41;
            PIAllInfo[0].PointPosition = mainForm.comboBox37;
            PIAllInfo[0].FloatBytes = mainForm.comboBox43;
            ////////////////////////////////////////////////////////////////////////////////
            PIAllInfo[1].CBAlgoritm = mainForm.comboBox23;
            PIAllInfo[1].PDInfo[0].CBOnOff = mainForm.checkBox12;
            PIAllInfo[1].PDInfo[0].EAddr = mainForm.textBox13;
            PIAllInfo[1].PDInfo[0].CBNumParam = mainForm.comboBox13;
            PIAllInfo[1].PDInfo[1].CBOnOff = mainForm.checkBox13;
            PIAllInfo[1].PDInfo[1].EAddr = mainForm.textBox14;
            PIAllInfo[1].PDInfo[1].CBNumParam = mainForm.comboBox14;
            PIAllInfo[1].PDInfo[2].CBOnOff = mainForm.checkBox14;
            PIAllInfo[1].PDInfo[2].EAddr = mainForm.textBox15;
            PIAllInfo[1].PDInfo[2].CBNumParam = mainForm.comboBox15;
            PIAllInfo[1].PDInfo[3].CBOnOff = mainForm.checkBox15;
            PIAllInfo[1].PDInfo[3].EAddr = mainForm.textBox16;
            PIAllInfo[1].PDInfo[3].CBNumParam = mainForm.comboBox16;
            PIAllInfo[1].PDInfo[4].CBOnOff = mainForm.checkBox16;
            PIAllInfo[1].PDInfo[4].EAddr = mainForm.textBox17;
            PIAllInfo[1].PDInfo[4].CBNumParam = mainForm.comboBox17;
            PIAllInfo[1].PDInfo[5].CBOnOff = mainForm.checkBox17;
            PIAllInfo[1].PDInfo[5].EAddr = mainForm.textBox18;
            PIAllInfo[1].PDInfo[5].CBNumParam = mainForm.comboBox18;
            PIAllInfo[1].PDInfo[6].CBOnOff = mainForm.checkBox18;
            PIAllInfo[1].PDInfo[6].EAddr = mainForm.textBox19;
            PIAllInfo[1].PDInfo[6].CBNumParam = mainForm.comboBox19;
            PIAllInfo[1].PDInfo[7].CBOnOff = mainForm.checkBox19;
            PIAllInfo[1].PDInfo[7].EAddr = mainForm.textBox20;
            PIAllInfo[1].PDInfo[7].CBNumParam = mainForm.comboBox20;
            PIAllInfo[1].PDInfo[8].CBOnOff = mainForm.checkBox20;
            PIAllInfo[1].PDInfo[8].EAddr = mainForm.textBox21;
            PIAllInfo[1].PDInfo[8].CBNumParam = mainForm.comboBox21;
            PIAllInfo[1].PDInfo[9].CBOnOff = mainForm.checkBox21;
            PIAllInfo[1].PDInfo[9].EAddr = mainForm.textBox22;
            PIAllInfo[1].PDInfo[9].CBNumParam = mainForm.comboBox22;

            PIAllInfo[1].IsUsed = mainForm.checkBox33;
            PIAllInfo[1].DstAddress = mainForm.textBox38;
            PIAllInfo[1].DstValueAddress = mainForm.textBox37;
            PIAllInfo[1].SrcValueAddress = mainForm.textBox46;
            PIAllInfo[1].TimeOut = mainForm.textBox45;
            PIAllInfo[1].PointPosition = mainForm.comboBox38;
            PIAllInfo[1].FloatBytes = mainForm.comboBox44;
            //////////////////////////////////////////////////////////////////////////////////
            PIAllInfo[2].CBAlgoritm = mainForm.comboBox36;
            PIAllInfo[2].PDInfo[0].CBOnOff = mainForm.checkBox22;
            PIAllInfo[2].PDInfo[0].EAddr = mainForm.textBox24;
            PIAllInfo[2].PDInfo[0].CBNumParam = mainForm.comboBox25;
            PIAllInfo[2].PDInfo[1].CBOnOff = mainForm.checkBox23;
            PIAllInfo[2].PDInfo[1].EAddr = mainForm.textBox25;
            PIAllInfo[2].PDInfo[1].CBNumParam = mainForm.comboBox26;
            PIAllInfo[2].PDInfo[2].CBOnOff = mainForm.checkBox24;
            PIAllInfo[2].PDInfo[2].EAddr = mainForm.textBox26;
            PIAllInfo[2].PDInfo[2].CBNumParam = mainForm.comboBox27;
            PIAllInfo[2].PDInfo[3].CBOnOff = mainForm.checkBox25;
            PIAllInfo[2].PDInfo[3].EAddr = mainForm.textBox27;
            PIAllInfo[2].PDInfo[3].CBNumParam = mainForm.comboBox28;
            PIAllInfo[2].PDInfo[4].CBOnOff = mainForm.checkBox26;
            PIAllInfo[2].PDInfo[4].EAddr = mainForm.textBox28;
            PIAllInfo[2].PDInfo[4].CBNumParam = mainForm.comboBox29;
            PIAllInfo[2].PDInfo[5].CBOnOff = mainForm.checkBox27;
            PIAllInfo[2].PDInfo[5].EAddr = mainForm.textBox29;
            PIAllInfo[2].PDInfo[5].CBNumParam = mainForm.comboBox30;
            PIAllInfo[2].PDInfo[6].CBOnOff = mainForm.checkBox28;
            PIAllInfo[2].PDInfo[6].EAddr = mainForm.textBox30;
            PIAllInfo[2].PDInfo[6].CBNumParam = mainForm.comboBox31;
            PIAllInfo[2].PDInfo[7].CBOnOff = mainForm.checkBox29;
            PIAllInfo[2].PDInfo[7].EAddr = mainForm.textBox31;
            PIAllInfo[2].PDInfo[7].CBNumParam = mainForm.comboBox32;
            PIAllInfo[2].PDInfo[8].CBOnOff = mainForm.checkBox30;
            PIAllInfo[2].PDInfo[8].EAddr = mainForm.textBox33;
            PIAllInfo[2].PDInfo[8].CBNumParam = mainForm.comboBox33;
            PIAllInfo[2].PDInfo[9].CBOnOff = mainForm.checkBox31;
            PIAllInfo[2].PDInfo[9].EAddr = mainForm.textBox34;
            PIAllInfo[2].PDInfo[9].CBNumParam = mainForm.comboBox35;

            PIAllInfo[2].IsUsed = mainForm.checkBox34;
            PIAllInfo[2].DstAddress = mainForm.textBox40;
            PIAllInfo[2].DstValueAddress = mainForm.textBox39;
            PIAllInfo[2].SrcValueAddress = mainForm.textBox44;
            PIAllInfo[2].TimeOut = mainForm.textBox43;
            PIAllInfo[2].PointPosition = mainForm.comboBox39;
            PIAllInfo[2].FloatBytes = mainForm.comboBox45;
        }

        public override bool CheckProtocol(byte[] buffer)
        {
            if ((buffer[0] == 0xFF && buffer[1] == 0x03) || (buffer[0] == 0xFF && buffer[1] == 0x0A))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private delegate void Delegate(byte[] buffer);


        public void ReadData()
        {
            // #FF#03#00#00#00#06#D0#16 
            byte[] buffer = new byte[8];
            buffer[0] = 0xFF;
            buffer[1] = 0x03;
            buffer[2] = 0x00;
            buffer[3] = 0x00;
            buffer[4] = 0x00;
            buffer[5] = 0x06;
            ushort crc = base.CalculateCRC16(buffer);
            byte[] byteArray = BitConverter.GetBytes(crc);
            buffer[6] = byteArray[1];
            buffer[7] = byteArray[0];

            base.SendMessage(mainForm.ComPortLink, mainForm.ComPortsComboBox, buffer);
        }

        public void ReadDataNew()
        {
            // #FF#03#00#01#00#06#D0#16 
            byte[] buffer = new byte[8];
            buffer[0] = 0xFF;
            buffer[1] = 0x03;
            buffer[2] = 0x01;
            buffer[3] = 0x00;
            buffer[4] = 0x00;
            buffer[5] = 0x06;
            ushort crc = base.CalculateCRC16(buffer);
            byte[] byteArray = BitConverter.GetBytes(crc);
            buffer[6] = byteArray[1];
            buffer[7] = byteArray[0];

            base.SendMessage(mainForm.ComPortLink, mainForm.ComPortsComboBox, buffer);
        }


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
                if (buffer[1] == 0x03) // чтение
                {
                    DecodeReadPackage(buffer);
                    mainForm.StatusLabel = ProtocolGlobals.OPERATION_SUCCESS_MESSAGE;
                }
                else if (buffer[1] == 0x0A) // запись
                {
                    mainForm.StatusLabel = ProtocolGlobals.RECORDING_DONE_MESSAGE;
                }
            }
            else
            {
                mainForm.StatusLabel = ProtocolGlobals.CRC_ERROR_MESSAGE;
            }
        }

        private void DecodeReadPackage(byte[] buffIn)
        {
            try
            {
                if (buffIn[2] == 0)
                {
                    int k = 4;
                    for (int line = 0; line < 3; line++)
                    {
                        PIAllInfo[line].CBAlgoritm.SelectedIndex = buffIn[k++];
                        for (int ndev = 0; ndev < 10; ndev++)
                        {
                            byte ucNetAddr = buffIn[k++];
                            if (ucNetAddr == 0xff)
                            {
                                ucNetAddr = 0;
                                PIAllInfo[line].PDInfo[ndev].CBOnOff.Checked = false;
                            }
                            else
                            {
                                PIAllInfo[line].PDInfo[ndev].CBOnOff.Checked = true;
                            }
                            PIAllInfo[line].PDInfo[ndev].EAddr.Text = ucNetAddr.ToString();
                            PIAllInfo[line].PDInfo[ndev].CBNumParam.SelectedIndex = buffIn[k++];
                        }
                    }
                    switch (buffIn[k++])
                    {
                        case 0: mainForm.radioButton34.Checked = true; break;
                        case 1: mainForm.radioButton35.Checked = true; break;
                        case 2: mainForm.radioButton36.Checked = true; break;
                        case 3: mainForm.radioButton37.Checked = true; break;
                        case 4: mainForm.radioButton38.Checked = true; break;

                    }
                    // 32 12 23
                    byte[] floatBuffer = new byte[4];

                    floatBuffer[3] = buffIn[k++];
                    floatBuffer[2] = buffIn[k++];
                    floatBuffer[1] = buffIn[k++];
                    floatBuffer[0] = buffIn[k++];
                    float scale0 = BitConverter.ToSingle(floatBuffer, 0);
                    mainForm.textBox32.Text = scale0.ToString();

                    floatBuffer[3] = buffIn[k++];
                    floatBuffer[2] = buffIn[k++];
                    floatBuffer[1] = buffIn[k++];
                    floatBuffer[0] = buffIn[k++];
                    float scale1 = BitConverter.ToSingle(floatBuffer, 0);
                    mainForm.textBox12.Text = scale1.ToString();

                    floatBuffer[3] = buffIn[k++];
                    floatBuffer[2] = buffIn[k++];
                    floatBuffer[1] = buffIn[k++];
                    floatBuffer[0] = buffIn[k++];
                    float scale2 = BitConverter.ToSingle(floatBuffer, 0);
                    mainForm.textBox23.Text = scale2.ToString();

                    mainForm.textBox2.Text = buffIn[k++].ToString();
                    mainForm.comboBox34.SelectedIndex = buffIn[k++];
                    mainForm.comboBox12.SelectedIndex = buffIn[k++];
                    mainForm.comboBox24.SelectedIndex = buffIn[k++];

                    ReadDataNew();
                }
                else if (buffIn[2] == 1)
                {
                    int k = 4;
                    for (int line = 0; line < 3; line++)
                    {
                        PIAllInfo[line].DstAddress.Text = buffIn[k++].ToString();

                        byte[] floatBuffer = new byte[2];
                        floatBuffer[1] = buffIn[k++];
                        floatBuffer[0] = buffIn[k++];
                        PIAllInfo[line].DstValueAddress.Text = BitConverter.ToUInt16(floatBuffer, 0).ToString();

                        floatBuffer[1] = buffIn[k++];
                        floatBuffer[0] = buffIn[k++];
                        PIAllInfo[line].SrcValueAddress.Text = BitConverter.ToUInt16(floatBuffer, 0).ToString();

                        floatBuffer[1] = buffIn[k++];
                        floatBuffer[0] = buffIn[k++];
                        PIAllInfo[line].TimeOut.Text = BitConverter.ToUInt16(floatBuffer, 0).ToString();

                        PIAllInfo[line].PointPosition.SelectedIndex = (int)buffIn[k++];
                        PIAllInfo[line].FloatBytes.SelectedIndex = (int)buffIn[k++];
                        PIAllInfo[line].IsUsed.Checked = (buffIn[k++] > 0);
                    }
                }
            }
            catch
            {
                MessageBox.Show("Ошибка обработки данных");
            }
        }

        public void WriteData()
        {
            try
            {
                if (mainForm.radioButton34.Checked || mainForm.radioButton35.Checked || mainForm.radioButton36.Checked)
                {
                    WriteData123();
                }
                else if (mainForm.radioButton37.Checked)
                {
                    WriteData4();
                }
                else if (mainForm.radioButton38.Checked)
                {
                    WriteData5();
                }
                else
                {
                    MessageBox.Show("Не выбран режим работы");
                }
            }
            catch
            {
                MessageBox.Show("Неверные входные данные");
            }
        }

        private void WriteData123()
        {
            int k = 0;
            byte[] bytes;
            byte[] buffOut = new byte[89];

            ///
            buffOut[k++] = 0xff;        // Slave Address
            buffOut[k++] = 10;          // Function
            buffOut[k++] = 0;           // Starting Address Hi
            buffOut[k++] = 0;           // Starting Address Lo
            buffOut[k++] = 0;           // No. of Registers Hi
            buffOut[k++] = 9;           // No. of Registers Lo
            buffOut[k++] = 18;          // Byte Count

            string errorString = "";

            for (int line = 0; line < 3; line++)
            {
                if (PIAllInfo[line].CBAlgoritm.SelectedIndex >= 0)
                {
                    buffOut[k++] = (byte)PIAllInfo[line].CBAlgoritm.SelectedIndex;
                }
                else
                {
                    errorString += "Ошибка поля 'Алгоритм'(Строка " + (line + 1) + ")" + Environment.NewLine;
                }

                for (int ndev = 0; ndev < 10; ndev++)
                {
                    if (PIAllInfo[line].PDInfo[ndev].CBOnOff.Checked)
                    {
                        try
                        {
                            buffOut[k++] = Convert.ToByte(PIAllInfo[line].PDInfo[ndev].EAddr.Text);
                        }
                        catch
                        {
                            errorString += "Ошибка поля 'Сетевой адрес'(Строка " + (line + 1) + ", Устройство " + (ndev + 1) + ")" + Environment.NewLine;
                        }
                    }
                    else
                    {
                        buffOut[k++] = 0xFF;
                    }
                    if (PIAllInfo[line].PDInfo[ndev].CBNumParam.SelectedIndex >= 0)
                    {
                        buffOut[k++] = (byte)PIAllInfo[line].PDInfo[ndev].CBNumParam.SelectedIndex;
                    }
                    else
                    {
                        errorString += "Ошибка поля 'Номер параметра'(Строка " + (line + 1) + ", Устройство " + (ndev + 1) + ")" + Environment.NewLine;
                    }
                }
            }
            if (mainForm.radioButton34.Checked) buffOut[k++] = 0;
            else if (mainForm.radioButton35.Checked) buffOut[k++] = 1;
            else buffOut[k++] = 2;

            try
            {
                float scale0 = Convert.ToSingle(mainForm.textBox32.Text);
                bytes = BitConverter.GetBytes(scale0);
                buffOut[k++] = bytes[0];
                buffOut[k++] = bytes[1];
                buffOut[k++] = bytes[2];
                buffOut[k++] = bytes[3];
            }
            catch
            {
                errorString += "Ошибка поля 'Шкала'(Строка 1)" + Environment.NewLine;
            }

            try
            {
                float scale1 = Convert.ToSingle(mainForm.textBox12.Text);
                bytes = BitConverter.GetBytes(scale1);
                buffOut[k++] = bytes[0];
                buffOut[k++] = bytes[1];
                buffOut[k++] = bytes[2];
                buffOut[k++] = bytes[3];
            }
            catch
            {
                errorString += "Ошибка поля 'Шкала'(Строка 2)" + Environment.NewLine;
            }

            try
            {
                float scale2 = Convert.ToSingle(mainForm.textBox23.Text);
                bytes = BitConverter.GetBytes(scale2);
                buffOut[k++] = bytes[0];
                buffOut[k++] = bytes[1];
                buffOut[k++] = bytes[2];
                buffOut[k++] = bytes[3];
            }
            catch
            {
                errorString += "Ошибка поля 'Шкала'(Строка 3)" + Environment.NewLine;
            }

            try
            {
                buffOut[k++] = Convert.ToByte(mainForm.textBox2.Text);
            }
            catch
            {
                errorString += "Ошибка поля 'Сетевой адрес'" + Environment.NewLine;
            }
            
            if (mainForm.comboBox34.SelectedIndex >= 0)
            {
                buffOut[k++] = (byte)mainForm.comboBox34.SelectedIndex;
            }
            else
            {
                errorString += "Ошибка поля 'Единица измерения'(Строка 1)" + Environment.NewLine;
            }

            if (mainForm.comboBox12.SelectedIndex >= 0)
            {
                buffOut[k++] = (byte)mainForm.comboBox12.SelectedIndex;
            }
            else
            {
                errorString += "Ошибка поля 'Единица измерения'(Строка 2)" + Environment.NewLine;
            }

            if (mainForm.comboBox24.SelectedIndex >= 0)
            {
                buffOut[k++] = (byte)mainForm.comboBox24.SelectedIndex;
            }
            else
            {
                errorString += "Ошибка поля 'Единица измерения'(Строка 2)" + Environment.NewLine;
            }

            if (errorString == "")
            {
                ushort crc = base.CalculateCRC16(buffOut);
                byte[] byteArray = BitConverter.GetBytes(crc);
                buffOut[k++] = byteArray[1];
                buffOut[k++] = byteArray[0];

                base.SendMessage(mainForm.ComPortLink, mainForm.ComPortsComboBox, buffOut);
            }
            else
            {
                MessageBox.Show(errorString);
            }
        }

        private void WriteData4()
        {
            int k = 0;
            byte[] buffOut = new byte[32];

            buffOut[k++] = 0xff;        // Slave Address
            buffOut[k++] = 10;          // Function
            buffOut[k++] = 1;           // Starting Address Hi
            buffOut[k++] = 0;           // Starting Address Lo
            buffOut[k++] = 0;           // No. of Registers Hi
            buffOut[k++] = 9;           // No. of Registers Lo
            buffOut[k++] = 18;          // Byte Count

            string errorString = "";

            byte[] byteArray;
            for (int line = 0; line < 3; line++)
            {
                if (PIAllInfo[line].IsUsed.Checked) buffOut[k++] = 1;
                else buffOut[k++] = 0;

                try
                {
                    UInt16 temp = Convert.ToUInt16(PIAllInfo[line].DstAddress.Text);
                    byteArray = BitConverter.GetBytes(temp);
                    buffOut[k++] = byteArray[1];
                    buffOut[k++] = byteArray[0];
                }
                catch
                {
                    errorString += "Ошибка поля 'Сетевой адрес удаленного устройства'(Строка " + (line + 1) + ")" + Environment.NewLine;
                }

                try
                {
                    UInt16 temp = Convert.ToUInt16(PIAllInfo[line].DstValueAddress.Text);
                    byteArray = BitConverter.GetBytes(temp);
                    buffOut[k++] = byteArray[1];
                    buffOut[k++] = byteArray[0];
                }
                catch
                {
                    errorString += "Ошибка поля 'Адрес данных удаленного устройства'(Строка " + (line + 1) + ")" + Environment.NewLine;
                } 

                if (PIAllInfo[line].PointPosition.SelectedIndex >= 0)
                {
                    buffOut[k++] = (byte)PIAllInfo[line].PointPosition.SelectedIndex;
                }
                else
                {
                    errorString += "Ошибка поля 'Положение децимальной точки'(Строка" + (line + 1) + ")" + Environment.NewLine;
                }

                if (PIAllInfo[line].FloatBytes.SelectedIndex >= 0)
                {
                    buffOut[k++] = (byte)PIAllInfo[line].FloatBytes.SelectedIndex;
                }
                else
                {
                    errorString += "Ошибка поля 'Порядок следования байт в 4-ех байтном числе(float)'(Строка" + (line + 1) + ")" + Environment.NewLine;
                }
            }

            buffOut[k++] = 3;
            try
            {
                buffOut[k++] = Convert.ToByte(mainForm.textBox2.Text);
            }
            catch
            {
                errorString += "Ошибка поля 'Сетевой адрес'" + Environment.NewLine;
            }

            if (errorString == "")
            {
                ushort crc = base.CalculateCRC16(buffOut);
                byteArray = BitConverter.GetBytes(crc);
                buffOut[k++] = byteArray[1];
                buffOut[k++] = byteArray[0];

                base.SendMessage(mainForm.ComPortLink, mainForm.ComPortsComboBox, buffOut);
            }
            else
            {
                MessageBox.Show(errorString);
            }
        }

        private void WriteData5()
        {
            int k = 0;
            byte[] buffOut = new byte[32];

            buffOut[k++] = 0xff;        // Slave Address
            buffOut[k++] = 10;          // Function
            buffOut[k++] = 2;           // Starting Address Hi
            buffOut[k++] = 0;           // Starting Address Lo
            buffOut[k++] = 0;           // No. of Registers Hi
            buffOut[k++] = 9;           // No. of Registers Lo
            buffOut[k++] = 18;          // Byte Count

            string errorString = "";

            byte[] byteArray;
            for (int line = 0; line < 3; line++)
            {
                if (PIAllInfo[line].IsUsed.Checked) buffOut[k++] = 1;
                else buffOut[k++] = 0;

                try
                {
                    UInt16 temp = Convert.ToUInt16(PIAllInfo[line].SrcValueAddress.Text);
                    byteArray = BitConverter.GetBytes(temp);
                    buffOut[k++] = byteArray[1];
                    buffOut[k++] = byteArray[0];
                }
                catch
                {
                    errorString += "Ошибка поля 'Адрес данных'(Строка " + (line + 1) + ")" + Environment.NewLine;
                } 
    
                try
                {
                    UInt16 temp = Convert.ToUInt16(PIAllInfo[line].TimeOut.Text);
                    byteArray = BitConverter.GetBytes(temp);
                    buffOut[k++] = byteArray[1];
                    buffOut[k++] = byteArray[0];
                }
                catch
                {
                    errorString += "Ошибка поля 'Таймаут отображения данных'(Строка " + (line + 1) + ")" + Environment.NewLine;
                } 

                if (PIAllInfo[line].PointPosition.SelectedIndex >= 0)
                {
                    buffOut[k++] = (byte)PIAllInfo[line].PointPosition.SelectedIndex;
                }
                else
                {
                    errorString += "Ошибка поля 'Положение децимальной точки'(Строка" + (line + 1) + ")" + Environment.NewLine;
                }

                if (PIAllInfo[line].FloatBytes.SelectedIndex >= 0)
                {
                    buffOut[k++] = (byte)PIAllInfo[line].FloatBytes.SelectedIndex;
                }
                else
                {
                    errorString += "Ошибка поля 'Порядок следования байт в 4-ех байтном числе(float)'(Строка" + (line + 1) + ")" + Environment.NewLine;
                }
            }

            buffOut[k++] = 4;
            try
            {
                buffOut[k++] = Convert.ToByte(mainForm.textBox2.Text);
            }
            catch
            {
                errorString += "Ошибка поля 'Сетевой адрес'" + Environment.NewLine;
            }

            if (errorString == "")
            {
                ushort crc = base.CalculateCRC16(buffOut);
                byteArray = BitConverter.GetBytes(crc);
                buffOut[k++] = byteArray[1];
                buffOut[k++] = byteArray[0];

                base.SendMessage(mainForm.ComPortLink, mainForm.ComPortsComboBox, buffOut);
            }
            else
            {
                MessageBox.Show(errorString);
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

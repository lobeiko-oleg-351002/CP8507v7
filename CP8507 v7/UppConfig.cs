using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
//using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace CP8507_v7
{
    public partial class UppConfig : Form
    {
        private const string ConnectButtonCaption = "Подключение";
        private const string DisconnectButtonCaption = "Отключение";
        private MainForm mainForm;
        private int counter = 0;
        // CultureInfo CurrentCulture = CultureInfo.GetCultureInfo("en-US");


        [Serializable]
        public struct Coefs
        {
            public UInt16[] angle; //  = new UInt16[9];
            public float[] AMP; //  = new float[9];
            public float freq;
            public float[] ADCmA_zero;
            public float[] ADCmA_top;
            public float[] DACmA_zero;
            public float[] DACmA_top; //  = new float[3];
        }

        public Coefs coeffs;

        public bool StopPoll, PollStoped;


        public bool VoltageIsOn
        {
            get
            {
                return turnOffVoltage_button.Visible;
            }
            set
            {
                turnOffVoltage_button.Visible = value;
            }
        }

        public bool IaIsOn
        {
            get
            {
                return IaOFF_button.Visible;
            }
            set
            {
                IaOFF_button.Visible = value;
            }
        }

        public bool IbIsOn
        {
            get
            {
                return IbOFF_button.Visible;
            }
            set
            {
                IbOFF_button.Visible = value;
            }
        }

        public bool IcIsOn
        {
            get
            {
                return IcOFF_button.Visible;
            }
            set
            {
                IcOFF_button.Visible = value;
            }
        }


        public UppConfig(MainForm mf)
        {
            InitializeComponent();
            mainForm = mf;

            coeffs = new Coefs();
            coeffs.angle = new UInt16[12];
            coeffs.AMP = new float[12];
            coeffs.ADCmA_zero = new float[2];
            coeffs.ADCmA_top = new float[2];
            coeffs.DACmA_zero = new float[2];
            coeffs.DACmA_top = new float[2];

            StopPoll = false;
            PollStoped = false;

            angleAMP_comboBox.SelectedIndex = 0;
            AMP_comboBox.SelectedIndex = 0;
        }

        private void UppConfig_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            e.Cancel = true; // this cancels the close event.
        }

        public void Reconnect()
        {
            //readCoefs_button.Text = "Подключение";
            //readCoefs_button_Click(null, null);
            if (counter++ < 2)
            {
                mainForm.modbus.SendLastPackage();
            }
        }

        private void readCoefs_button_Click(object sender, EventArgs e)
        {
            if (readCoefs_button.Text == ConnectButtonCaption)
            {
                try
                {
                    mainForm.deviceNumber_textBox.Text = "118";
                    mainForm.comPortSpeed_comboBox.SelectedIndex = 7;

                    UInt16 mode = 1;
                    byte[] modeBytes = BitConverter.GetBytes(mode);

                    UInt16 nominalCurrent = 0;
                    if (I5_radioButton.Checked) nominalCurrent = 1;
                    byte[] nomCurrentBytes = BitConverter.GetBytes(nominalCurrent);

                    UInt16 nominalVoltage = 0;
                    if (V400_radioButton.Checked) nominalVoltage = 1;
                    byte[] UaOutBytes = BitConverter.GetBytes(nominalVoltage);

                    UInt16 mAnumber = 1;
                    if (imp2_radioButton.Checked) mAnumber = 2;
                    else if (imp3_radioButton.Checked) mAnumber = 3;
                    byte[] mAnumberBytes = BitConverter.GetBytes(mAnumber);

                    UInt16 mAinput = 0;
                    if (mA20_radioButton.Checked) mAinput = 1;
                    byte[] mAinputBytes = BitConverter.GetBytes(mAinput);

                    UInt16 mAresistor = 0;
                    if (Rmax_radioButton.Checked) mAresistor = 1;
                    byte[] mAresistorBytes = BitConverter.GetBytes(mAresistor);

                    UInt16 wireScheme = 1;
                    byte[] wireSchemeBytes = BitConverter.GetBytes(wireScheme);

                    byte[] data = new byte[14];

                    wireSchemeBytes.CopyTo(data, 0);
                    mAresistorBytes.CopyTo(data, 2);
                    mAinputBytes.CopyTo(data, 4);
                    mAnumberBytes.CopyTo(data, 6);
                    UaOutBytes.CopyTo(data, 8);
                    nomCurrentBytes.CopyTo(data, 10);
                    modeBytes.CopyTo(data, 12);

                    mainForm.modbus.WriteData(Modbus.EnquryType.INQ_SET_UPP_CONFIG, data);
                }
                catch
                {
                    MessageBox.Show("Данные введены некорректно!");
                }
            }
            else
            {
                byte[] data = new byte[12];
                Array.Clear(data, 0, data.Length);
                mainForm.modbus.WriteData(Modbus.EnquryType.INQ_SET_UPP_OFF_VOLTAGE, data);
                Connection(false);
            }
        }

        public bool IsConnected()
        {
            return readCoefs_button.Text == DisconnectButtonCaption;
        }

        public void Connection(bool isConnected)
        {
            if (isConnected)
            {
                readCoefs_button.Text = DisconnectButtonCaption;
                mA_groupBox.Enabled = true;
                AMP_groupBox.Enabled = true;
                fileCoeff_groupBox.Enabled = true;
                saveChangedParams_button.Enabled = true;
                mainForm.SaveChangedParamsButton = true;
            }
            else
            {
                readCoefs_button.Text = ConnectButtonCaption;
                mA_groupBox.Enabled = false;
                AMP_groupBox.Enabled = false;
                fileCoeff_groupBox.Enabled = false;
                saveChangedParams_button.Enabled = false;
                mainForm.SaveChangedParamsButton = false;
                VoltageIsOn = true;
                IaIsOn = false;
                IbIsOn = false;
                IcIsOn = false;

                mainForm.ParamsGroupBoxVisible = false;
                mainForm.EnableCorrectionModeButton = true;
            }
        }

        private void WaitStopPoll()
        {
            counter = 0;
            StopPoll = true;

            for (int i = 0; i < 100; i++)
            {
                mainForm.main_progressBar.Value = i;
                mainForm.sendInqury_Timer.Stop();
                Thread.Sleep(5);
                if (PollStoped) break;
            }
            StopPoll = false;
            PollStoped = false;
            mainForm.main_progressBar.Value = 99;
        }

        private void setVoltage_button_Click(object sender, EventArgs e)
        {
            try
            {
                // float value = Convert.ToSingle(textBox4.Text);
                float value = float.Parse(textBox4.Text.Replace(",", CultureInfo.InvariantCulture.NumberFormat.NumberDecimalSeparator), CultureInfo.InvariantCulture);
                byte[] bytes = BitConverter.GetBytes(value);

                byte[] data = new byte[12];
                bytes.CopyTo(data, 0);
                bytes.CopyTo(data, 4);
                bytes.CopyTo(data, 8);

                WaitStopPoll();

                mainForm.modbus.WriteData(Modbus.EnquryType.INQ_SET_V_UPP, data);
            }
            catch
            {
                ShowError();
            }
        }

        private void setCurrent_button_Click(object sender, EventArgs e)
        {
            try
            {
                // float value = Convert.ToSingle(textBox3.Text);
                float value = float.Parse(textBox3.Text.Replace(",", CultureInfo.InvariantCulture.NumberFormat.NumberDecimalSeparator), CultureInfo.InvariantCulture);
                byte[] bytes = BitConverter.GetBytes(value);

                byte[] data = new byte[12];
                bytes.CopyTo(data, 0);
                bytes.CopyTo(data, 4);
                bytes.CopyTo(data, 8);

                WaitStopPoll();

                if (I5_radioButton.Checked)
                    mainForm.modbus.WriteData(Modbus.EnquryType.INQ_SET_I5_UPP, data);
                else
                    mainForm.modbus.WriteData(Modbus.EnquryType.INQ_SET_I_UPP, data);

                IaOFF_button.Visible = true;
                IbOFF_button.Visible = true;
                IcOFF_button.Visible = true;
            }
            catch
            {
                ShowError();
            }
        }

        private void setFreq_button_Click(object sender, EventArgs e)
        {
            try
            {
                //float value = Convert.ToSingle(textBox5.Text);
                float value = float.Parse(textBox5.Text.Replace(",", CultureInfo.InvariantCulture.NumberFormat.NumberDecimalSeparator), CultureInfo.InvariantCulture);
                byte[] data = BitConverter.GetBytes(value);

                WaitStopPoll();

                mainForm.modbus.WriteData(Modbus.EnquryType.INQ_SET_F_UPP, data);
            }
            catch
            {
                ShowError();
            }
        }

        private void setAngle_button_Click(object sender, EventArgs e)
        {
            try
            {
                UInt16 angle = Convert.ToUInt16(textBox8.Text);
                if (angle > 360) throw new IndexOutOfRangeException();

                byte[] data = BitConverter.GetBytes(angle);

                WaitStopPoll();

                mainForm.modbus.WriteData(Modbus.EnquryType.INQ_SET_ANGLE_UPP, data);
                
            }
            catch
            {
                ShowError();
            }
        }

        private void turnOffVoltage_button_Click(object sender, EventArgs e)
        {
            UInt16 Off = 0;
            byte[] offBytes = BitConverter.GetBytes(Off);

            byte[] data = new byte[6];
            offBytes.CopyTo(data, 0);
            offBytes.CopyTo(data, 2);
            offBytes.CopyTo(data, 4);

            WaitStopPoll();

            mainForm.modbus.WriteData(Modbus.EnquryType.INQ_SET_UPP_OFF_VOLTAGE, data);
        }

        private void turnOnVoltage_button_Click(object sender, EventArgs e)
        {
            UInt16 On = 1;
            byte[] onBytes = BitConverter.GetBytes(On);

            byte[] data = new byte[6];
            onBytes.CopyTo(data, 0);
            onBytes.CopyTo(data, 2);
            onBytes.CopyTo(data, 4);

            WaitStopPoll();

            mainForm.modbus.WriteData(Modbus.EnquryType.INQ_SET_UPP_ON_VOLTAGE, data);
        }

        private void IaON_button_Click(object sender, EventArgs e)
        {
            if (I5_radioButton.Checked)
                SendRegister(1, Modbus.EnquryType.INQ_SET_UPP_ON_Ia5);
            else
                SendRegister(1, Modbus.EnquryType.INQ_SET_UPP_ON_Ia);
        }

        private void IaOFF_button_Click(object sender, EventArgs e)
        {
            if (I5_radioButton.Checked)
                SendRegister(0, Modbus.EnquryType.INQ_SET_UPP_OFF_Ia5);
            else
                SendRegister(0, Modbus.EnquryType.INQ_SET_UPP_OFF_Ia);
        }

        private void IbON_button_Click(object sender, EventArgs e)
        {
            if (I5_radioButton.Checked)
                SendRegister(1, Modbus.EnquryType.INQ_SET_UPP_ON_Ib5);
            else
                SendRegister(1, Modbus.EnquryType.INQ_SET_UPP_ON_Ib);
        }

        private void IbOFF_button_Click(object sender, EventArgs e)
        {
            if (I5_radioButton.Checked)
                SendRegister(0, Modbus.EnquryType.INQ_SET_UPP_OFF_Ib5);
            else
                SendRegister(0, Modbus.EnquryType.INQ_SET_UPP_OFF_Ib);
        }

        private void IcON_button_Click(object sender, EventArgs e)
        {
            if (I5_radioButton.Checked)
                SendRegister(1, Modbus.EnquryType.INQ_SET_UPP_ON_Ic5);
            else
                SendRegister(1, Modbus.EnquryType.INQ_SET_UPP_ON_Ic);
        }

        private void IcOFF_button_Click(object sender, EventArgs e)
        {
            if (I5_radioButton.Checked)
                SendRegister(0, Modbus.EnquryType.INQ_SET_UPP_OFF_Ic5);
            else
                SendRegister(0, Modbus.EnquryType.INQ_SET_UPP_OFF_Ic);
        }

        private void SendRegister(Int16 value, Modbus.EnquryType mbType)
        {
            byte[] onBytes = BitConverter.GetBytes(value);

            byte[] data = new byte[2];
            onBytes.CopyTo(data, 0);

            WaitStopPoll();

            mainForm.modbus.WriteData(mbType, data);
        }

        private void I5_radioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (!I5_radioButton.Checked) return;
            SendRegister(0, Modbus.EnquryType.INQ_SET_UPP_5A_1A);
        }

        private void I1_radioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (!I1_radioButton.Checked) return;
            SendRegister(1, Modbus.EnquryType.INQ_SET_UPP_5A_1A);
        }


        private void imp1_radioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (!imp1_radioButton.Checked) return;
            SendRegister(1, Modbus.EnquryType.INQ_SET_UPP_IMP_NUM_OUT);
        }

        private void imp2_radioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (!imp2_radioButton.Checked) return;
            SendRegister(2, Modbus.EnquryType.INQ_SET_UPP_IMP_NUM_OUT);
        }

        private void imp3_radioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (!imp3_radioButton.Checked) return;
            SendRegister(3, Modbus.EnquryType.INQ_SET_UPP_IMP_NUM_OUT);
        }


        private void mA5_radioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (!mA5_radioButton.Checked) return;
            SendRegister(0, Modbus.EnquryType.INQ_SET_UPP_IMP_MA);
        }

        private void mA20_radioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (!mA20_radioButton.Checked) return;
            SendRegister(1, Modbus.EnquryType.INQ_SET_UPP_IMP_MA);
        }


        private void Rmin_radioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (!Rmin_radioButton.Checked) return;
            SendRegister(0, Modbus.EnquryType.INQ_SET_UPP_IMP_R);
        }

        private void Rmax_radioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (!Rmax_radioButton.Checked) return;
            SendRegister(1, Modbus.EnquryType.INQ_SET_UPP_IMP_R);
        }

        private void setFreqGenerator_button_Click(object sender, EventArgs e)
        {
            try
            {
                // float value = Convert.ToSingle(textBox2.Text);
                float value = float.Parse(textBox2.Text.Replace(",", CultureInfo.InvariantCulture.NumberFormat.NumberDecimalSeparator), CultureInfo.InvariantCulture);
                // float nominal = Convert.ToSingle(textBox5.Text);
                float nominal = float.Parse(textBox5.Text.Replace(",", CultureInfo.InvariantCulture.NumberFormat.NumberDecimalSeparator), CultureInfo.InvariantCulture);
                float coeffNew = mainForm.uppConfigurator.coeffs.freq * (value / nominal);
                mainForm.uppConfigurator.coeffs.freq = coeffNew;

                byte[] data = BitConverter.GetBytes(coeffNew);

                WaitStopPoll();

                mainForm.modbus.WriteData(Modbus.EnquryType.INQ_SET_UPP_F_GEN, data);
            }
            catch
            {
                ShowError();
            }
        }

        private void setAmpCoeff_button_Click(object sender, EventArgs e)
        {
            try
            {
                //float nominal = Convert.ToSingle(textBox4.Text);
                float nominal = float.Parse(textBox4.Text.Replace(",", CultureInfo.InvariantCulture.NumberFormat.NumberDecimalSeparator), CultureInfo.InvariantCulture);
                if (AMP_comboBox.SelectedIndex >= 3) nominal = float.Parse(textBox3.Text.Replace(",", CultureInfo.InvariantCulture.NumberFormat.NumberDecimalSeparator), CultureInfo.InvariantCulture);

                //float value = Convert.ToSingle(textBox1.Text);
                float value = float.Parse(textBox1.Text.Replace(",", CultureInfo.InvariantCulture.NumberFormat.NumberDecimalSeparator), CultureInfo.InvariantCulture);
                //float coeffNew = nominal / (value / mainForm.uppConfigurator.coeffs.AMP[AMP_comboBox.SelectedIndex]);   
                int index = AMP_comboBox.SelectedIndex;
                if (index >= 0 && index <= 2)
                {
                    if (V400_radioButton.Checked) index += 9;
                }
                else
                {
                    if (I5_radioButton.Checked) index += 3;
                }
                float coeffNew = mainForm.uppConfigurator.coeffs.AMP[index] / (value / nominal);
                mainForm.uppConfigurator.coeffs.AMP[index] = coeffNew;

                byte[] data = BitConverter.GetBytes(coeffNew);

                WaitStopPoll();

                if (AMP_comboBox.SelectedIndex == 0)
                {
                    if (V400_radioButton.Checked)
                        mainForm.modbus.WriteData(Modbus.EnquryType.INQ_SET_UPP_AMP_COEF_UA400, data);
                    else
                        mainForm.modbus.WriteData(Modbus.EnquryType.INQ_SET_UPP_AMP_COEF_UA, data);
                }
                else if (AMP_comboBox.SelectedIndex == 1)
                {
                    if (V400_radioButton.Checked)
                        mainForm.modbus.WriteData(Modbus.EnquryType.INQ_SET_UPP_AMP_COEF_UB400, data);
                    else
                        mainForm.modbus.WriteData(Modbus.EnquryType.INQ_SET_UPP_AMP_COEF_UB, data);
                }
                else if (AMP_comboBox.SelectedIndex == 2)
                {
                    if (V400_radioButton.Checked)
                        mainForm.modbus.WriteData(Modbus.EnquryType.INQ_SET_UPP_AMP_COEF_UC400, data);
                    else
                        mainForm.modbus.WriteData(Modbus.EnquryType.INQ_SET_UPP_AMP_COEF_UC, data);
                }
                else if (AMP_comboBox.SelectedIndex == 3)
                {
                    if (I5_radioButton.Checked)
                        mainForm.modbus.WriteData(Modbus.EnquryType.INQ_SET_UPP_AMP_COEF_IA5, data);
                    else
                        mainForm.modbus.WriteData(Modbus.EnquryType.INQ_SET_UPP_AMP_COEF_IA, data);
                }
                else if (AMP_comboBox.SelectedIndex == 4)
                {
                    if (I5_radioButton.Checked)
                        mainForm.modbus.WriteData(Modbus.EnquryType.INQ_SET_UPP_AMP_COEF_IB5, data);
                    else
                        mainForm.modbus.WriteData(Modbus.EnquryType.INQ_SET_UPP_AMP_COEF_IB, data);
                }
                else if (AMP_comboBox.SelectedIndex == 5)
                {
                    if (I5_radioButton.Checked)
                        mainForm.modbus.WriteData(Modbus.EnquryType.INQ_SET_UPP_AMP_COEF_IC5, data);
                    else
                        mainForm.modbus.WriteData(Modbus.EnquryType.INQ_SET_UPP_AMP_COEF_IC, data);
                }
            }
            catch
            {
                ShowError();
            }
        }

        private void angleDown_button_Click(object sender, EventArgs e)
        {
            SetAngleCoef(-50);

            updateText6();
        }

        private void angleUp_button_Click(object sender, EventArgs e)
        {
            SetAngleCoef(50);

            updateText6();
        }

        private void SetAngleCoef(Int16 value)
        {
            int index = angleAMP_comboBox.SelectedIndex;
            if (index >= 0 && index <= 2)
            {
                if (V400_radioButton.Checked) index += 9;
            }
            else
            {
                if (I5_radioButton.Checked) index += 3;
            }

            Int16 temp = (Int16)coeffs.angle[index];
            temp += value;
            // if (temp > 2000) temp -= 2000;
            // if (temp < 0) temp += 2000;

            coeffs.angle[index] = (UInt16)temp;

            if (angleAMP_comboBox.SelectedIndex == 0)
            {
                if (V400_radioButton.Checked)
                    SendRegister(temp, Modbus.EnquryType.INQ_SET_UPP_ANGLE_COEF_UA400);
                else
                    SendRegister(temp, Modbus.EnquryType.INQ_SET_UPP_ANGLE_COEF_UA);
            }
            else if (angleAMP_comboBox.SelectedIndex == 1)
            {
                if (V400_radioButton.Checked)
                    SendRegister(temp, Modbus.EnquryType.INQ_SET_UPP_ANGLE_COEF_UB400);
                else
                    SendRegister(temp, Modbus.EnquryType.INQ_SET_UPP_ANGLE_COEF_UB);
            }
            else if (angleAMP_comboBox.SelectedIndex == 2)
            {
                if (V400_radioButton.Checked)
                    SendRegister(temp, Modbus.EnquryType.INQ_SET_UPP_ANGLE_COEF_UC400);
                else
                    SendRegister(temp, Modbus.EnquryType.INQ_SET_UPP_ANGLE_COEF_UC);
            }
            else if (angleAMP_comboBox.SelectedIndex == 3)
            {
                if (I5_radioButton.Checked)
                    SendRegister(temp, Modbus.EnquryType.INQ_SET_UPP_ANGLE_COEF_IA5);
                else
                    SendRegister(temp, Modbus.EnquryType.INQ_SET_UPP_ANGLE_COEF_IA);
            }
            else if (angleAMP_comboBox.SelectedIndex == 4)
            {
                if (I5_radioButton.Checked)
                    SendRegister(temp, Modbus.EnquryType.INQ_SET_UPP_ANGLE_COEF_IB5);
                else
                    SendRegister(temp, Modbus.EnquryType.INQ_SET_UPP_ANGLE_COEF_IB);
            }
            else if (angleAMP_comboBox.SelectedIndex == 5)
            {
                if (I5_radioButton.Checked)
                    SendRegister(temp, Modbus.EnquryType.INQ_SET_UPP_ANGLE_COEF_IC5);
                else
                    SendRegister(temp, Modbus.EnquryType.INQ_SET_UPP_ANGLE_COEF_IC);
            }
        }

        private void ShowError()
        {
            MessageBox.Show("Значение введено неверно!");
            mainForm.StartAutoInquryTimer(Modbus.EnquryType.INQ_READ_31_PARAMS_FROM_UPP, 100);
        }

        private void setADCzero_button_Click(object sender, EventArgs e)
        {
            try
            {
                //float value = Convert.ToSingle(textBox8.Text);
                float value = float.Parse(mA_label.Text.Replace(",", CultureInfo.InvariantCulture.NumberFormat.NumberDecimalSeparator), CultureInfo.InvariantCulture);
                byte[] data = new byte[1];
                if (mA5_radioButton.Checked)
                {
                    mainForm.uppConfigurator.coeffs.ADCmA_zero[0] -= value;
                    data = BitConverter.GetBytes(mainForm.uppConfigurator.coeffs.ADCmA_zero[0]);
                }
                else if (mA20_radioButton.Checked)
                {
                    mainForm.uppConfigurator.coeffs.ADCmA_zero[1] -= value;
                    data = BitConverter.GetBytes(mainForm.uppConfigurator.coeffs.ADCmA_zero[1]);
                }

                WaitStopPoll();

                if (mA5_radioButton.Checked) mainForm.modbus.WriteData(Modbus.EnquryType.INQ_SET_UPP_ADC_ZERO_5, data);
                else if (mA20_radioButton.Checked) mainForm.modbus.WriteData(Modbus.EnquryType.INQ_SET_UPP_ADC_ZERO_20, data);
            }
            catch
            {
                ShowError();
            }
        }

        private void setDACzero_button_Click(object sender, EventArgs e)
        {
            try
            {
                //float value = Convert.ToSingle(textBox9.Text);
                float value = float.Parse(textBox9.Text.Replace(",", CultureInfo.InvariantCulture.NumberFormat.NumberDecimalSeparator), CultureInfo.InvariantCulture);
                byte[] data = new byte[4];
                if (mA5_radioButton.Checked)
                {
                    mainForm.uppConfigurator.coeffs.DACmA_zero[0] -= (value * mainForm.uppConfigurator.coeffs.DACmA_top[0]);
                    data = BitConverter.GetBytes(mainForm.uppConfigurator.coeffs.DACmA_zero[0]);
                }
                else if (mA20_radioButton.Checked)
                {
                    mainForm.uppConfigurator.coeffs.DACmA_zero[1] -= (value * mainForm.uppConfigurator.coeffs.DACmA_top[1]);
                    data = BitConverter.GetBytes(mainForm.uppConfigurator.coeffs.DACmA_zero[1]);
                }

                WaitStopPoll();

                if (mA5_radioButton.Checked) mainForm.modbus.WriteData(Modbus.EnquryType.INQ_SET_UPP_DAC_ZERO_5, data);
                else if (mA20_radioButton.Checked) mainForm.modbus.WriteData(Modbus.EnquryType.INQ_SET_UPP_DAC_ZERO_20, data);
            }
            catch
            {
                ShowError();
            }
        }

        private void setADCtop_button_Click(object sender, EventArgs e)
        {
            try
            {
                int shift = 0;
                if (mA20_radioButton.Checked) shift = 1;

                float nominal = 5;
                if (mA20_radioButton.Checked) nominal = 20;
                //float value = Convert.ToSingle(textBox6.Text);
                float value = float.Parse(mA_label.Text.Replace(",", CultureInfo.InvariantCulture.NumberFormat.NumberDecimalSeparator), CultureInfo.InvariantCulture);

                float coeffNew = mainForm.uppConfigurator.coeffs.ADCmA_top[shift] / (value / nominal);
                mainForm.uppConfigurator.coeffs.ADCmA_top[shift] = coeffNew;

                byte[] data = BitConverter.GetBytes(coeffNew);

                WaitStopPoll();

                if (mA5_radioButton.Checked) mainForm.modbus.WriteData(Modbus.EnquryType.INQ_SET_UPP_ADC_TOP_5, data);
                else if (mA20_radioButton.Checked) mainForm.modbus.WriteData(Modbus.EnquryType.INQ_SET_UPP_ADC_TOP_20, data);
            }
            catch
            {
                ShowError();
            }
        }

        private void setDACtop_button_Click(object sender, EventArgs e)
        {
            try
            {
                int shift = 0;
                if (mA20_radioButton.Checked) shift = 1;

                float nominal = 5;
                if (mA20_radioButton.Checked) nominal = 20;
                // float value = Convert.ToSingle(textBox7.Text);
                float value = float.Parse(textBox7.Text.Replace(",", CultureInfo.InvariantCulture.NumberFormat.NumberDecimalSeparator), CultureInfo.InvariantCulture);

                float coeffNew = mainForm.uppConfigurator.coeffs.DACmA_top[shift] / (value / nominal);
                mainForm.uppConfigurator.coeffs.DACmA_top[shift] = coeffNew;

                byte[] data = BitConverter.GetBytes(coeffNew);

                WaitStopPoll();

                if (mA5_radioButton.Checked) mainForm.modbus.WriteData(Modbus.EnquryType.INQ_SET_UPP_DAC_TOP_5, data);
                else if (mA20_radioButton.Checked) mainForm.modbus.WriteData(Modbus.EnquryType.INQ_SET_UPP_DAC_TOP_20, data);
            }
            catch
            {
                ShowError();
            }
        }

        private void coeffToFile_button_Click(object sender, EventArgs e)
        {
            CoeffSaveFileDialog.Filter = "(*.coeffs)|*.coeffs"; // 
            try
            {
                if (CoeffSaveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    FileStream writerFileStream = new FileStream(CoeffSaveFileDialog.FileName, FileMode.Create, FileAccess.Write);
                    BinaryFormatter formatter = new BinaryFormatter();
                    formatter.Serialize(writerFileStream, coeffs);
                    writerFileStream.Close();
                }
            }
            catch
            {
                MessageBox.Show("Ошибка сохрания файла");
            }
        }

        private void coeffFromFile_button_Click(object sender, EventArgs e)
        {
            CoeffOpenFileDialog.Filter = "(*.coeffs)|*.coeffs"; // 
            if (CoeffOpenFileDialog.ShowDialog() != DialogResult.OK) return;
            Encoding enc = Encoding.GetEncoding(1251);
            try
            {
                BinaryFormatter formatter = new BinaryFormatter();
                FileStream readerFileStream = new FileStream(CoeffOpenFileDialog.FileName, FileMode.Open, FileAccess.Read);
                coeffs = (Coefs)formatter.Deserialize(readerFileStream);
                readerFileStream.Close();
            }
            catch
            {
                MessageBox.Show("При открытии файла возникла ошибка");
            }
        }

        private void coeffsToDevice_button_Click(object sender, EventArgs e)
        {
            byte[] data = new byte[12];
            int byteIndex = 0;
            byte[] temp;

            for (int i = 0; i < 6; i++)
            {
                temp = BitConverter.GetBytes(mainForm.uppConfigurator.coeffs.angle[i]);
                Array.Reverse(temp);
                temp.CopyTo(data, byteIndex);
                byteIndex += 2;
            }

            Array.Reverse(data);

            WaitStopPoll();

            mainForm.modbus.WriteData(Modbus.EnquryType.INQ_SET_UPP_ANGLE_COEFFS, data);
        }

        public void SaveOtherCoeffs()
        {
            byte[] data = new byte[84];
            int byteIndex = 0;
            byte[] temp;

            for (int i = 0; i < 12; i++)
            {
                temp = BitConverter.GetBytes(mainForm.uppConfigurator.coeffs.AMP[i]);
                Array.Reverse(temp);
                temp.CopyTo(data, byteIndex);
                byteIndex += 4;
            }
            // byteIndex += 4;

            temp = BitConverter.GetBytes(mainForm.uppConfigurator.coeffs.freq);
            Array.Reverse(temp);
            temp.CopyTo(data, byteIndex);
            byteIndex += 4;

            for (int i = 0; i < 2; i++)
            {
                temp = BitConverter.GetBytes(mainForm.uppConfigurator.coeffs.ADCmA_zero[i]);
                Array.Reverse(temp);
                temp.CopyTo(data, byteIndex);
                byteIndex += 4;
            }

            for (int i = 0; i < 2; i++)
            {
                temp = BitConverter.GetBytes(mainForm.uppConfigurator.coeffs.ADCmA_top[i]);
                Array.Reverse(temp);
                temp.CopyTo(data, byteIndex);
                byteIndex += 4;
            }

            for (int i = 0; i < 2; i++)
            {
                temp = BitConverter.GetBytes(mainForm.uppConfigurator.coeffs.DACmA_zero[i]);
                Array.Reverse(temp);
                temp.CopyTo(data, byteIndex);
                byteIndex += 4;
            }

            for (int i = 0; i < 2; i++)
            {
                temp = BitConverter.GetBytes(mainForm.uppConfigurator.coeffs.DACmA_top[i]);
                Array.Reverse(temp);
                temp.CopyTo(data, byteIndex);
                byteIndex += 4;
            }

            Array.Reverse(data);

            WaitStopPoll();

            mainForm.modbus.WriteData(Modbus.EnquryType.INQ_SET_UPP_OTHER_COEFFS, data);
        }

        private void saveChangedParams_button_Click(object sender, EventArgs e)
        {
            SendRegister(1, Modbus.EnquryType.INQ_SAVE_UPP_COEFS);
        }

        private void I5_radioButton_CheckedChanged_1(object sender, EventArgs e)
        {
            updateText6();

            if (!I5_radioButton.Checked) return;
            textBox3.Text = "5";
            SendRegister(1, Modbus.EnquryType.INQ_SET_UPP_I_OUT);
        }

        private void I1_radioButton_CheckedChanged_1(object sender, EventArgs e)
        {
            updateText6();

            if (!I1_radioButton.Checked) return;
            textBox3.Text = "1";
            SendRegister(0, Modbus.EnquryType.INQ_SET_UPP_I_OUT);
        }

        private void V100_radioButton_CheckedChanged(object sender, EventArgs e)
        {
            updateText6();

            if (!V100_radioButton.Checked) return;
            textBox4.Text = "57.74";
            SendRegister(0, Modbus.EnquryType.INQ_SET_UPP_U_OUT);
        }

        private void V400_radioButton_CheckedChanged(object sender, EventArgs e)
        {
            updateText6();

            if (!V400_radioButton.Checked) return;
            textBox4.Text = "230.94";
            SendRegister(1, Modbus.EnquryType.INQ_SET_UPP_U_OUT);
        }

        public void angleAMP_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            updateText6();
        }

        public void updateText6()
        {
            int index = angleAMP_comboBox.SelectedIndex;
            if (index >= 0 && index <= 2)
            {
                if (V400_radioButton.Checked) index += 9;
            }
            else
            {
                if (I5_radioButton.Checked) index += 3;
            }

            textBox6.Text = coeffs.angle[index].ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                int index = angleAMP_comboBox.SelectedIndex;
                if (index >= 0 && index <= 2)
                {
                    if (V400_radioButton.Checked) index += 9;
                }
                else
                {
                    if (I5_radioButton.Checked) index += 3;
                }

                coeffs.angle[index] = Convert.ToUInt16(textBox6.Text);

                updateText6();
                SetAngleCoef(0);
            }
            catch 
            {
                ShowError();
            }
        }

        private void setDAC_button_Click(object sender, EventArgs e)
        {
            try
            {
                //float value = Convert.ToSingle(textBox6.Text);
                float value = float.Parse(textBox10.Text.Replace(",", CultureInfo.InvariantCulture.NumberFormat.NumberDecimalSeparator), CultureInfo.InvariantCulture);

                byte[] data = BitConverter.GetBytes(value);

                WaitStopPoll();

                mainForm.modbus.WriteData(Modbus.EnquryType.INQ_SET_UPP_DAC_VAL, data);
            }
            catch
            {
                ShowError();
            }
        }


    }
}

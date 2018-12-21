using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO.Ports;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace CP8507_v7
{
    partial class MainForm
    {
        private delegate void SetControlPropertyThreadSafeDelegate(Control control, string propertyName, object propertyValue);

        public static void SetControlPropertyThreadSafe(Control control, string propertyName, object propertyValue)
        {
            if (control.InvokeRequired)
            {
                control.BeginInvoke(new SetControlPropertyThreadSafeDelegate(SetControlPropertyThreadSafe),
                new object[] { control, propertyName, propertyValue });
                //control.Invoke(new SetControlPropertyThreadSafeDelegate(SetControlPropertyThreadSafe),
                //new object[] { control, propertyName, propertyValue });
            }
            else
            {
                control.GetType().InvokeMember(propertyName, BindingFlags.SetProperty, null, control, new object[] { propertyValue });
            }
        }

        /// <Контролы основного окна>
        /// 
        /// </summary>
        public string StatusLabel
        {
            set
            {
                SetControlPropertyThreadSafe(status_label, "Text", value);
            }
        }

        public string ProgVersionInfoTextBox
        {
            set
            {
                SetControlPropertyThreadSafe(progVersion_infoTextBox, "Text", value);
            }
        }

        public string NetAddressInfoTextBox
        {
            set
            {
                SetControlPropertyThreadSafe(netAddress_infoTextBox, "Text", value);
            }
        }

        public string DeviceNumberTextBox
        {
            get
            {
                if (deviceNumber_textBox.InvokeRequired)
                {
                    string text = "";
                    this.Invoke(new MethodInvoker(delegate() { text = deviceNumber_textBox.Text; }));
                    return text;
                }
                else
                {
                    return deviceNumber_textBox.Text;
                }
                //return (string)deviceNumber_textBox.GetType().InvokeMember("Text", BindingFlags.GetProperty, null, deviceNumber_textBox, new object[] { });
                //                 return deviceNumber_textBox.Text;
            }
            set
            {
                SetControlPropertyThreadSafe(deviceNumber_textBox, "Text", value);
            }
        }

        public string ComPortsComboBox
        {
            get
            {
                if (comPorts_comboBox.InvokeRequired)
                {
                    string text = "";
                    this.Invoke(new MethodInvoker(delegate() { text = comPorts_comboBox.Text; }));
                    return text;
                }
                else
                {
                    return comPorts_comboBox.Text;
                }
            }
        }


        public SerialPort ComPortLink
        {
            get
            {

                return ComPort;
            }
        }

        public MainForm MainFormLink
        {
            get
            {
                return this;
            }
        }

        public int MainTabControlSelectedIndex
        {
            get
            {
                if (TabControl_main.InvokeRequired)
                {
                    int temp = -1;
                    this.Invoke(new MethodInvoker(delegate() { temp = TabControl_main.SelectedIndex; }));
                    return temp;
                }
                else
                {
                    return TabControl_main.SelectedIndex;
                }
            }
        }

        public int ConfigurateTabControlSelectedIndex
        {
            get
            {
                if (configurate_tabControl.InvokeRequired)
                {
                    int temp = -1;
                    this.Invoke(new MethodInvoker(delegate() { temp = configurate_tabControl.SelectedIndex; }));
                    return temp;
                }
                else
                {
                    return configurate_tabControl.SelectedIndex;
                }
            }
        }

        public int ComPortSpeedComboBox
        {
            set
            {
                SetControlPropertyThreadSafe(comPortSpeed_comboBox, "SelectedIndex", value);
            }
        }

        public int TarifsComboBox
        {
            get
            {
                if (tarifs_comboBox.InvokeRequired)
                {
                    int text = 0;
                    this.Invoke(new MethodInvoker(delegate() { text = tarifs_comboBox.SelectedIndex; }));
                    return text;
                }
                else
                {
                    return tarifs_comboBox.SelectedIndex;
                }
            }
        }

        ///////////////////////////////////////

        /// <Контролы информационного канала>
        /// 
        /// </summary>
        public string UaInfoTextBox
        {
            set
            {
                SetControlPropertyThreadSafe(Ua_infoTextBox, "Text", value);
            }
        }

        public string UbInfoTextBox
        {
            set
            {
                SetControlPropertyThreadSafe(Ub_infoTextBox, "Text", value);
            }
        }

        public string UcInfoTextBox
        {
            set
            {
                SetControlPropertyThreadSafe(Uc_infoTextBox, "Text", value);
            }
        }

        public string UfInfoTextBox
        {
            set
            {
                SetControlPropertyThreadSafe(Uf_infoTextBox, "Text", value);
            }
        }

        public string UabInfoTextBox
        {
            set
            {
                SetControlPropertyThreadSafe(Uab_infoTextBox, "Text", value);
            }
        }

        public string UbcInfoTextBox
        {
            set
            {
                SetControlPropertyThreadSafe(Ubc_infoTextBox, "Text", value);
            }
        }

        public string UcaInfoTextBox
        {
            set
            {
                SetControlPropertyThreadSafe(Uca_infoTextBox, "Text", value);
            }
        }

        public string UlInfoTextBox
        {
            set
            {
                SetControlPropertyThreadSafe(Ul_infoTextBox, "Text", value);
            }
        }

        public string IaInfoTextBox
        {
            set
            {
                SetControlPropertyThreadSafe(Ia_infoTextBox, "Text", value);
            }
        }

        public string IbInfoTextBox
        {
            set
            {
                SetControlPropertyThreadSafe(Ib_infoTextBox, "Text", value);
            }
        }

        public string IcInfoTextBox
        {
            set
            {
                SetControlPropertyThreadSafe(Ic_infoTextBox, "Text", value);
            }
        }

        public string IInfoTextBox
        {
            set
            {
                SetControlPropertyThreadSafe(I_infoTextBox, "Text", value);
            }
        }

        public string PaInfoTextBox
        {
            set
            {
                SetControlPropertyThreadSafe(Pa_infoTextBox, "Text", value);
            }
        }

        public string PbInfoTextBox
        {
            set
            {
                SetControlPropertyThreadSafe(Pb_infoTextBox, "Text", value);
            }
        }

        public string PcInfoTextBox
        {
            set
            {
                SetControlPropertyThreadSafe(Pc_infoTextBox, "Text", value);
            }
        }

        public string PInfoTextBox
        {
            set
            {
                SetControlPropertyThreadSafe(P_infoTextBox, "Text", value);
            }
        }

        public string QaInfoTextBox
        {
            set
            {
                SetControlPropertyThreadSafe(Qa_infoTextBox, "Text", value);
            }
        }

        public string QbInfoTextBox
        {
            set
            {
                SetControlPropertyThreadSafe(Qb_infoTextBox, "Text", value);
            }
        }

        public string QcInfoTextBox
        {
            set
            {
                SetControlPropertyThreadSafe(Qc_infoTextBox, "Text", value);
            }
        }

        public string QInfoTextBox
        {
            set
            {
                SetControlPropertyThreadSafe(Q_infoTextBox, "Text", value);
            }
        }

        public string SaInfoTextBox
        {
            set
            {
                SetControlPropertyThreadSafe(Sa_infoTextBox, "Text", value);
            }
        }

        public string SbInfoTextBox
        {
            set
            {
                SetControlPropertyThreadSafe(Sb_infoTextBox, "Text", value);
            }
        }

        public string ScInfoTextBox
        {
            set
            {
                SetControlPropertyThreadSafe(Sc_infoTextBox, "Text", value);
            }
        }

        public string SInfoTextBox
        {
            set
            {
                SetControlPropertyThreadSafe(S_infoTextBox, "Text", value);
            }
        }

        public string KpaInfoTextBox
        {
            set
            {
                SetControlPropertyThreadSafe(Kpa_infoTextBox, "Text", value);
            }
        }

        public string KpbInfoTextBox
        {
            set
            {
                SetControlPropertyThreadSafe(Kpb_infoTextBox, "Text", value);
            }
        }

        public string KpcInfoTextBox
        {
            set
            {
                SetControlPropertyThreadSafe(Kpc_infoTextBox, "Text", value);
            }
        }

        public string KpInfoTextBox
        {
            set
            {
                SetControlPropertyThreadSafe(Kp_infoTextBox, "Text", value);
            }
        }

        public string FInfoTextBox
        {
            set
            {
                SetControlPropertyThreadSafe(F_infoTextBox, "Text", value);
            }
        }

        public string U0InfoTextBox
        {
            set
            {
                SetControlPropertyThreadSafe(U0_infoTextBox, "Text", value);
            }
        }

        public string I0InfoTextBox
        {
            set
            {
                SetControlPropertyThreadSafe(I0_infoTextBox, "Text", value);
            }
        }

        public string dUplusInfoTextBox
        {
            set
            {
                SetControlPropertyThreadSafe(dUplInfo_TextBox, "Text", value);
            }
        }

        public string dUminusInfoTextBox
        {
            set
            {
                SetControlPropertyThreadSafe(dUminInfo_TextBox, "Text", value);
            }
        }

        public string K0UInfoTextBox
        {
            set
            {
                SetControlPropertyThreadSafe(K0UInfo_TextBox, "Text", value);
            }
        }

        public string K2UInfoTextBox
        {
            set
            {
                SetControlPropertyThreadSafe(K2UInfo_TextBox, "Text", value);
            }
        }

        public string dFInfoTextBox
        {
            set
            {
                SetControlPropertyThreadSafe(dFInfo_TextBox, "Text", value);
            }
        }

        public string TimeOutInfoLabel
        {
            set
            {
                SetControlPropertyThreadSafe(timeOut_InfoLabel, "Text", value);
            }
        }

        /////////////////////


        /// <Контролы окна опроса параметров>
        /// 
        /// </summary>
        public bool EnergyChooseCheckBox
        {
            get
            {
                if (checkBox_EnergyChoose.InvokeRequired)
                {
                    bool temp = false;
                    this.Invoke(new MethodInvoker(delegate() { temp = checkBox_EnergyChoose.Checked; }));
                    return temp;
                }
                else
                {
                    return checkBox_EnergyChoose.Checked;
                }
            }
            set
            {
                SetControlPropertyThreadSafe(checkBox_EnergyChoose, "Enabled", value);
            }
        }

        public bool AutoLoopIsOn
        {
            get
            {
                //if (autoLoop_checkBox.InvokeRequired)
                //{
                //    bool temp = false;
                //    this.Invoke(new MethodInvoker(delegate() { temp = autoLoop_checkBox.Checked; }));
                //    return temp;
                //}
                //else
                //{
                return autoLoopIsOn;
                //}
            }
            set
            {
                autoLoopIsOn = value;
                // SetControlPropertyThreadSafe(autoLoop_checkBox, "Checked", value);
            }
        }

        public string PollPeriodTextBox
        {
            get
            {
                if (autoLoop_checkBox.InvokeRequired)
                {
                    string temp = "";
                    this.Invoke(new MethodInvoker(delegate() { temp = pollPeriod_textBox.Text; }));
                    return temp;
                }
                else
                {
                    return pollPeriod_textBox.Text;
                }
            }
        }

        public string PollPeriodPKETextBox
        {
            get
            {
                if (autoLoopPKE_checkBox.InvokeRequired)
                {
                    string temp = "";
                    this.Invoke(new MethodInvoker(delegate() { temp = pollPeriodPKE_textBox.Text; }));
                    return temp;
                }
                else
                {
                    return pollPeriodPKE_textBox.Text;
                }
            }
        }

        public string Read31ParamButton
        {
            set
            {
                SetControlPropertyThreadSafe(read31Params_button, "Text", value);
                SetControlPropertyThreadSafe(readPKE_button, "Text", value);
                SetControlPropertyThreadSafe(readAveragePKE_Button, "Text", value);
                SetControlPropertyThreadSafe(readRandomPKE_button, "Text", value);
            }
        }


        public bool Read31ParamButtonState
        {
            set
            {
                SetControlPropertyThreadSafe(read31Params_button, "Enabled", value);
            }
            get
            {
                if (read31Params_button.InvokeRequired)
                {
                    bool temp = false;
                    this.Invoke(new MethodInvoker(delegate() { temp = read31Params_button.Enabled; }));
                    return temp;
                }
                else
                {
                    return read31Params_button.Enabled;
                }
            }
        }

        public string UaTextBox
        {
            set
            {
                SetControlPropertyThreadSafe(Ua_TextBox, "Text", value);
            }
        }

        public string UbTextBox
        {
            set
            {
                SetControlPropertyThreadSafe(Ub_TextBox, "Text", value);
            }
        }

        public string UcTextBox
        {
            set
            {
                SetControlPropertyThreadSafe(Uc_TextBox, "Text", value);
            }
        }

        public string UfTextBox
        {
            set
            {
                SetControlPropertyThreadSafe(Uf_TextBox, "Text", value);
            }
        }

        public string UabTextBox
        {
            set
            {
                SetControlPropertyThreadSafe(Uab_TextBox, "Text", value);
            }
        }

        public string UbcTextBox
        {
            set
            {
                SetControlPropertyThreadSafe(Ubc_TextBox, "Text", value);
            }
        }

        public string UcaTextBox
        {
            set
            {
                SetControlPropertyThreadSafe(Uca_TextBox, "Text", value);
            }
        }

        public string UlTextBox
        {
            set
            {
                SetControlPropertyThreadSafe(Ul_TextBox, "Text", value);
            }
        }

        public string IaTextBox
        {
            set
            {
                SetControlPropertyThreadSafe(Ia_TextBox, "Text", value);
            }
        }

        public string IbTextBox
        {
            set
            {
                SetControlPropertyThreadSafe(Ib_TextBox, "Text", value);
            }
        }

        public string IcTextBox
        {
            set
            {
                SetControlPropertyThreadSafe(Ic_TextBox, "Text", value);
            }
        }

        public string ITextBox
        {
            set
            {
                SetControlPropertyThreadSafe(I_TextBox, "Text", value);
            }
        }

        public string PaTextBox
        {
            set
            {
                SetControlPropertyThreadSafe(Pa_TextBox, "Text", value);
            }
        }

        public string PbTextBox
        {
            set
            {
                SetControlPropertyThreadSafe(Pb_TextBox, "Text", value);
            }
        }

        public string PcTextBox
        {
            set
            {
                SetControlPropertyThreadSafe(Pc_TextBox, "Text", value);
            }
        }

        public string PTextBox
        {
            set
            {
                SetControlPropertyThreadSafe(P_TextBox, "Text", value);
            }
        }

        public string QaTextBox
        {
            set
            {
                SetControlPropertyThreadSafe(Qa_TextBox, "Text", value);
            }
        }

        public string QbTextBox
        {
            set
            {
                SetControlPropertyThreadSafe(Qb_TextBox, "Text", value);
            }
        }

        public string QcTextBox
        {
            set
            {
                SetControlPropertyThreadSafe(Qc_TextBox, "Text", value);
            }
        }

        public string QTextBox
        {
            set
            {
                SetControlPropertyThreadSafe(Q_TextBox, "Text", value);
            }
        }

        public string SaTextBox
        {
            set
            {
                SetControlPropertyThreadSafe(Sa_TextBox, "Text", value);
            }
        }

        public string SbTextBox
        {
            set
            {
                SetControlPropertyThreadSafe(Sb_TextBox, "Text", value);
            }
        }

        public string ScTextBox
        {
            set
            {
                SetControlPropertyThreadSafe(Sc_TextBox, "Text", value);
            }
        }

        public string STextBox
        {
            set
            {
                SetControlPropertyThreadSafe(S_TextBox, "Text", value);
            }
        }

        public string KpaTextBox
        {
            set
            {
                SetControlPropertyThreadSafe(Kpa_TextBox, "Text", value);
            }
        }

        public string KpbTextBox
        {
            set
            {
                SetControlPropertyThreadSafe(Kpb_TextBox, "Text", value);
            }
        }

        public string KpcTextBox
        {
            set
            {
                SetControlPropertyThreadSafe(Kpc_TextBox, "Text", value);
            }
        }

        public string KpTextBox
        {
            set
            {
                SetControlPropertyThreadSafe(Kp_TextBox, "Text", value);
            }
        }

        public string FTextBox
        {
            set
            {
                SetControlPropertyThreadSafe(F_TextBox, "Text", value);
            }
        }

        public string U0TextBox
        {
            set
            {
                SetControlPropertyThreadSafe(U0_TextBox, "Text", value);
            }
        }

        public string I0TextBox
        {
            set
            {
                SetControlPropertyThreadSafe(I0_TextBox, "Text", value);
            }
        }

        public string Wr1TextBox
        {
            set
            {
                SetControlPropertyThreadSafe(Wr1_TextBox, "Text", value);
            }
        }

        public string Wr2TextBox
        {
            set
            {
                SetControlPropertyThreadSafe(Wr2_TextBox, "Text", value);
            }
        }

        public string Wr3TextBox
        {
            set
            {
                SetControlPropertyThreadSafe(Wr3_TextBox, "Text", value);
            }
        }

        public string Wr4TextBox
        {
            set
            {
                SetControlPropertyThreadSafe(Wr4_TextBox, "Text", value);
            }
        }

        public string WaTextBox
        {
            set
            {
                SetControlPropertyThreadSafe(Wa_TextBox, "Text", value);
            }
        }

        public string WaPlusTextBox
        {
            set
            {
                SetControlPropertyThreadSafe(WaPlus_TextBox, "Text", value);
            }
        }

        public string WaMinusTextBox
        {
            set
            {
                SetControlPropertyThreadSafe(WaMinus_TextBox, "Text", value);
            }
        }

        public string WrTextBox
        {
            set
            {
                SetControlPropertyThreadSafe(Wr_TextBox, "Text", value);
            }
        }

        public string WrPlusTextBox
        {
            set
            {
                SetControlPropertyThreadSafe(WrPlus_TextBox, "Text", value);
            }
        }

        public string WrMinusTextBox
        {
            set
            {
                SetControlPropertyThreadSafe(WrMinus_TextBox, "Text", value);
            }
        }

        public string UaLabel
        {
            set
            {
                SetControlPropertyThreadSafe(Ua_Label, "Text", value);
            }
        }

        public string UbLabel
        {
            set
            {
                SetControlPropertyThreadSafe(Ub_Label, "Text", value);
            }
        }

        public string UcLabel
        {
            set
            {
                SetControlPropertyThreadSafe(Uc_Label, "Text", value);
            }
        }

        public string UfLabel
        {
            set
            {
                SetControlPropertyThreadSafe(Uf_Label, "Text", value);
            }
        }

        public string UabLabel
        {
            set
            {
                SetControlPropertyThreadSafe(Uab_Label, "Text", value);
            }
        }

        public string UbcLabel
        {
            set
            {
                SetControlPropertyThreadSafe(Ubc_Label, "Text", value);
            }
        }

        public string UcaLabel
        {
            set
            {
                SetControlPropertyThreadSafe(Uca_Label, "Text", value);
            }
        }

        public string UlLabel
        {
            set
            {
                SetControlPropertyThreadSafe(Ul_Label, "Text", value);
            }
        }

        public string IaLabel
        {
            set
            {
                SetControlPropertyThreadSafe(Ia_Label, "Text", value);
            }
        }

        public string IbLabel
        {
            set
            {
                SetControlPropertyThreadSafe(Ib_Label, "Text", value);
            }
        }

        public string IcLabel
        {
            set
            {
                SetControlPropertyThreadSafe(Ic_Label, "Text", value);
            }
        }

        public string ILabel
        {
            set
            {
                SetControlPropertyThreadSafe(I_Label, "Text", value);
            }
        }

        public string PaLabel
        {
            set
            {
                SetControlPropertyThreadSafe(Pa_Label, "Text", value);
            }
        }

        public string PbLabel
        {
            set
            {
                SetControlPropertyThreadSafe(Pb_Label, "Text", value);
            }
        }

        public string PcLabel
        {
            set
            {
                SetControlPropertyThreadSafe(Pc_Label, "Text", value);
            }
        }

        public string PLabel
        {
            set
            {
                SetControlPropertyThreadSafe(P_Label, "Text", value);
            }
        }

        public string QaLabel
        {
            set
            {
                SetControlPropertyThreadSafe(Qa_Label, "Text", value);
            }
        }

        public string QbLabel
        {
            set
            {
                SetControlPropertyThreadSafe(Qb_Label, "Text", value);
            }
        }

        public string QcLabel
        {
            set
            {
                SetControlPropertyThreadSafe(Qc_Label, "Text", value);
            }
        }

        public string QLabel
        {
            set
            {
                SetControlPropertyThreadSafe(Q_Label, "Text", value);
            }
        }

        public string SaLabel
        {
            set
            {
                SetControlPropertyThreadSafe(Sa_Label, "Text", value);
            }
        }

        public string SbLabel
        {
            set
            {
                SetControlPropertyThreadSafe(Sb_Label, "Text", value);
            }
        }

        public string ScLabel
        {
            set
            {
                SetControlPropertyThreadSafe(Sc_Label, "Text", value);
            }
        }

        public string SLabel
        {
            set
            {
                SetControlPropertyThreadSafe(S_Label, "Text", value);
            }
        }

        public string KpaLabel
        {
            set
            {
                SetControlPropertyThreadSafe(Kpa_Label, "Text", value);
            }
        }

        public string KpbLabel
        {
            set
            {
                SetControlPropertyThreadSafe(Kpb_Label, "Text", value);
            }
        }

        public string KpcLabel
        {
            set
            {
                SetControlPropertyThreadSafe(Kpc_Label, "Text", value);
            }
        }

        public string KpLabel
        {
            set
            {
                SetControlPropertyThreadSafe(Kp_Label, "Text", value);
            }
        }

        public string FLabel
        {
            set
            {
                SetControlPropertyThreadSafe(F_Label, "Text", value);
            }
        }

        public string U0Label
        {
            set
            {
                SetControlPropertyThreadSafe(U0_Label, "Text", value);
            }
        }

        public string I0Label
        {
            set
            {
                SetControlPropertyThreadSafe(I0_Label, "Text", value);
            }
        }

        public string Wr1Label
        {
            set
            {
                SetControlPropertyThreadSafe(Wr1_Label, "Text", value);
            }
        }

        public string Wr2Label
        {
            set
            {
                SetControlPropertyThreadSafe(Wr2_Label, "Text", value);
            }
        }

        public string Wr3Label
        {
            set
            {
                SetControlPropertyThreadSafe(Wr3_Label, "Text", value);
            }
        }

        public string Wr4Label
        {
            set
            {
                SetControlPropertyThreadSafe(Wr4_Label, "Text", value);
            }
        }

        public string WaLabel
        {
            set
            {
                SetControlPropertyThreadSafe(Wa_Label, "Text", value);
            }
        }

        public string WaPlusLabel
        {
            set
            {
                SetControlPropertyThreadSafe(WaPlus_Label, "Text", value);
            }
        }

        public string WaMinusLabel
        {
            set
            {
                SetControlPropertyThreadSafe(WaMinus_Label, "Text", value);
            }
        }

        public string WrLabel
        {
            set
            {
                SetControlPropertyThreadSafe(Wr_Label, "Text", value);
            }
        }

        public string WrPlusLabel
        {
            set
            {
                SetControlPropertyThreadSafe(WrPlus_Label, "Text", value);
            }
        }

        public string WrMinusLabel
        {
            set
            {
                SetControlPropertyThreadSafe(WrMinus_Label, "Text", value);
            }
        }

        public string AnswerTimeTextBox
        {
            set
            {
                SetControlPropertyThreadSafe(answerTime_textBox, "Text", value);
                SetControlPropertyThreadSafe(answerPKETime_textBox, "Text", value);
            }
        }

        public string MaxAnswerTimeTextBox
        {
            set
            {
                SetControlPropertyThreadSafe(maxAnswerTime_textBox, "Text", value);
                SetControlPropertyThreadSafe(maxPKEAnswerTime_textBox, "Text", value);
            }
        }

        public string InquresCounterTextBox
        {
            set
            {
                SetControlPropertyThreadSafe(inquresCounter_textBox, "Text", value);
                SetControlPropertyThreadSafe(inquresPKECounter_textBox, "Text", value);
            }
        }

        public string CRCErrorTextBox
        {
            set
            {
                SetControlPropertyThreadSafe(crcErrorPKE_textBox, "Text", value);
            }
        }

        public string NoAnswerCounterTextBox
        {
            set
            {
                SetControlPropertyThreadSafe(noAnswerCnt_textBox, "Text", value);
                SetControlPropertyThreadSafe(noAnswerPKECnt_textBox, "Text", value);
            }
        }

        public Color ColorStatusButton
        {
            set
            {
                SetControlPropertyThreadSafe(colorStatus_button, "BackColor", value);
                SetControlPropertyThreadSafe(colorStatusPKE_button, "BackColor", value);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public int MainProgressBar
        {
            get
            {
                if (main_progressBar.InvokeRequired)
                {
                    int text = 0;
                    this.Invoke(new MethodInvoker(delegate() { text = main_progressBar.Value; }));
                    return text;
                }
                else
                {
                    return main_progressBar.Value;
                }
            }
            set
            {
                SetControlPropertyThreadSafe(main_progressBar, "Value", value);
            }
        }


        /// <summary>
        /// 
        /// </summary>
        public string ProgrammVersionTextBox
        {
            set
            {
                SetControlPropertyThreadSafe(softVers_textBox, "Text", value);
            }
        }

        public string NumberOfParamsAddSettingsTextBox
        {
            set
            {
                SetControlPropertyThreadSafe(addSettings.numberOfParams_textBox, "Text", value);
            }
        }

        public string NetworkAddressTextBox
        {
            set
            {
                SetControlPropertyThreadSafe(networkAddress_textBox, "Text", value);
            }
        }

        public string FactoryNumberTextBox
        {
            set
            {
                //SetControlPropertyThreadSafe(factoryNumber_textBox, "Text", value);
                SetControlPropertyThreadSafe(factoryNumber_label, "Text", value);
            }
        }

        public string FactoryNumberAddSettingsTextBox
        {
            set
            {
                SetControlPropertyThreadSafe(addSettings.factoryNumber_textBox, "Text", value);
            }
        }

        public int SpeedAComboBox
        {
            get
            {
                if (speed1_comboBox.InvokeRequired)
                {
                    int text = 0;
                    this.Invoke(new MethodInvoker(delegate() { text = speed1_comboBox.SelectedIndex; }));
                    return text;
                }
                else
                {
                    return speed1_comboBox.SelectedIndex;
                }
            }
            set
            {
                SetControlPropertyThreadSafe(speed1_comboBox, "SelectedIndex", value);
            }
        }

        public int DeviceNumberAddSettingsComboBox
        {
            set
            {
                SetControlPropertyThreadSafe(addSettings.modification_comboBox, "SelectedIndex", value);
            }
        }

        public int RelaysAddSettingsComboBox
        {
            set
            {
                SetControlPropertyThreadSafe(addSettings.relays_comboBox, "SelectedIndex", value);
            }
        }

        public int InterfacesAddSettingsComboBox
        {
            set
            {
                SetControlPropertyThreadSafe(addSettings.interfaces_comboBox, "SelectedIndex", value);
            }
        }

        public string ConnectionSchemeTextBox
        {
            set
            {
                SetControlPropertyThreadSafe(connectionScheme_textBox, "Text", value);
            }
        }

        public string DeviceVersionTextBox
        {
            set
            {
                SetControlPropertyThreadSafe(priborVers_textBox, "Text", value);
            }
        }

        public int NoisePercentTrackBar
        {
            set
            {
                SetControlPropertyThreadSafe(noisePercent_trackBar, "Value", value);
            }
        }

        public string NoisePercentLabel
        {
            set
            {
                SetControlPropertyThreadSafe(noisePercent_label, "Text", value);
            }
        }

        public int ModeBComboBox
        {
            set
            {
                SetControlPropertyThreadSafe(configUart2_comboBox, "SelectedIndex", value);
            }
            get
            {
                if (configUart2_comboBox.InvokeRequired)
                {
                    int temp = -1;
                    this.Invoke(new MethodInvoker(delegate() { temp = configUart2_comboBox.SelectedIndex; }));
                    return temp;
                }
                else
                {
                    return configUart2_comboBox.SelectedIndex;
                }
            }
        }

        public int SpeedBComboBox
        {
            set
            {
                SetControlPropertyThreadSafe(speed2_comboBox, "SelectedIndex", value);
            }
        }

        public bool SpeedBComboBoxEnabled
        {
            set
            {
                SetControlPropertyThreadSafe(speed2_comboBox, "Enabled", value);
            }
        }

        public int ModbusBytesComboBox
        {
            set
            {
                SetControlPropertyThreadSafe(modbusBytes_comboBox, "SelectedIndex", value);
            }
        }

        public int ASDUAddressIEC101ComboBox
        {
            set
            {
                // SetControlPropertyThreadSafe(IEC101Settings.iec101ASDUAddress_comboBox, "SelectedIndex", value);
            }
        }

        public int ASDUAddressIEC104ComboBox
        {
            set
            {
                //SetControlPropertyThreadSafe(IECSettings.iec104ASDUAddress_comboBox, "SelectedIndex", value);
            }
        }

        public int InfoAddressIEC101ComboBox
        {
            set
            {
                //SetControlPropertyThreadSafe(IECSettings.iec101InfoAddress_comboBox, "SelectedIndex", value);
            }
        }

        public int InfoAddressIEC104ComboBox
        {
            set
            {
                //SetControlPropertyThreadSafe(IECSettings.iec104InfoAddress_comboBox, "SelectedIndex", value);
            }
        }

        public bool InfoAddressIEC101ComboBoxEnabled
        {
            set
            {
                // SetControlPropertyThreadSafe(IECSettings.iec101InfoAddress_comboBox, "Enabled", value);
            }
        }

        public bool InfoAddressIEC104ComboBoxEnabled
        {
            set
            {
                //  SetControlPropertyThreadSafe(IECSettings.iec104InfoAddress_comboBox, "Enabled", value);
            }
        }

        public int ReasonIEC101ComboBox
        {
            set
            {
                // SetControlPropertyThreadSafe(IECSettings.iec101Reason_comboBox, "SelectedIndex", value);
            }
        }

        public int ReasonIEC104ComboBox
        {
            set
            {
                // SetControlPropertyThreadSafe(IECSettings.iec104Reason_comboBox, "SelectedIndex", value);
            }
        }

        public int ASDUTypeIEC101ComboBox
        {
            set
            {
                // SetControlPropertyThreadSafe(IECSettings.iec101ASDUType_comboBox, "SelectedIndex", value);
            }
        }

        public int ASDUTypeIEC104ComboBox
        {
            set
            {
                //  SetControlPropertyThreadSafe(IECSettings.iec104ASDUType_comboBox, "SelectedIndex", value);
            }
        }

        public int RelayNumberComboBox
        {
            set
            {
                SetControlPropertyThreadSafe(relayN_comboBox, "SelectedIndex", value);
            }
        }


        public int RelayModeComboBox
        {
            set
            {
                SetControlPropertyThreadSafe(relayWorkMode_comboBox, "SelectedIndex", value);
            }
        }

        public string RelayUstavkaTextBox
        {
            set
            {
                SetControlPropertyThreadSafe(relayUstavka_textBox, "Text", value);
            }
        }

        public string RelayPingTextBox
        {
            set
            {
                SetControlPropertyThreadSafe(relayPing_textBox, "Text", value);
            }
        }

        public string RelayGisterezisTextBox
        {
            get
            {
                if (relayGist_textBox.InvokeRequired)
                {
                    string text = "";
                    this.Invoke(new MethodInvoker(delegate() { text = relayGist_textBox.Text; }));
                    return text;
                }
                else
                {
                    return relayGist_textBox.Text;
                }
            }
            set
            {
                SetControlPropertyThreadSafe(relayGist_textBox, "Text", value);
            }
        }

        public int RelayParanComboBox
        {
            set
            {
                SetControlPropertyThreadSafe(relayParam_comboBox, "SelectedIndex", value);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public int EnergyRow1ComboBox
        {
            set
            {
                SetControlPropertyThreadSafe(energyRow1_comboBox, "SelectedIndex", value);
            }
        }

        public int EnergyRow2ComboBox
        {
            set
            {
                SetControlPropertyThreadSafe(energyRow2_comboBox, "SelectedIndex", value);
            }
        }

        /// <Параметры индикации>
        /// 
        /// </summary>
        public int NumberOfRowsComboBox
        {
            get
            {
                if (numberOfRows_comboBox.InvokeRequired)
                {
                    int text = 0;
                    this.Invoke(new MethodInvoker(delegate() { text = numberOfRows_comboBox.SelectedIndex; }));
                    return text;
                }
                else
                {
                    return numberOfRows_comboBox.SelectedIndex;
                }
            }
            set
            {
                SetControlPropertyThreadSafe(numberOfRows_comboBox, "SelectedIndex", value);
            }
        }

        public int DisplayLightComboBox
        {
            set
            {
                SetControlPropertyThreadSafe(displayLight_comboBox, "SelectedIndex", value);
            }
        }

        public int Row1IndParamComboBox
        {
            set
            {
                SetControlPropertyThreadSafe(row1Param_comboBox, "SelectedIndex", value);
            }
        }

        public int Row2IndParamComboBox
        {
            set
            {
                SetControlPropertyThreadSafe(row2Param_comboBox, "SelectedIndex", value);
            }
        }

        public int Row3IndParamComboBox
        {
            set
            {
                SetControlPropertyThreadSafe(row3Param_comboBox, "SelectedIndex", value);
            }
        }

        public int Row4IndParamComboBox
        {
            set
            {
                SetControlPropertyThreadSafe(row4Param_comboBox, "SelectedIndex", value);
            }
        }

        public bool Row4IndParamComboBoxEnabled
        {
            get
            {
                if (row4Param_comboBox.InvokeRequired)
                {
                    bool text = false;
                    this.Invoke(new MethodInvoker(delegate() { text = row4Param_comboBox.Enabled; }));
                    return text;
                }
                else
                {
                    return row4Param_comboBox.Enabled;
                }
            }
        }

        public int Row5IndParamComboBox
        {
            set
            {
                SetControlPropertyThreadSafe(row5Param_comboBox, "SelectedIndex", value);
            }
        }

        public int Row6IndParamComboBox
        {
            set
            {
                SetControlPropertyThreadSafe(row6Param_comboBox, "SelectedIndex", value);
            }
        }

        public string Param1MinUstavkaTextBox
        {
            set
            {
                SetControlPropertyThreadSafe(param1MinUstavka_textBox, "Text", value);
            }
        }

        public string Param2MinUstavkaTextBox
        {
            set
            {
                SetControlPropertyThreadSafe(param2MinUstavka_textBox, "Text", value);
            }
        }

        public string Param3MinUstavkaTextBox
        {
            set
            {
                SetControlPropertyThreadSafe(param3MinUstavka_textBox, "Text", value);
            }
        }

        public string Param4MinUstavkaTextBox
        {
            set
            {
                SetControlPropertyThreadSafe(param4MinUstavka_textBox, "Text", value);
            }
        }

        public string Param5MinUstavkaTextBox
        {
            set
            {
                SetControlPropertyThreadSafe(param5MinUstavka_textBox, "Text", value);
            }
        }

        public string Param6MinUstavkaTextBox
        {
            set
            {
                SetControlPropertyThreadSafe(param6MinUstavka_textBox, "Text", value);
            }
        }

        public string Param1MaxUstavkaTextBox
        {
            set
            {
                SetControlPropertyThreadSafe(param1MaxUstavka_textBox, "Text", value);
            }
        }

        public string Param2MaxUstavkaTextBox
        {
            set
            {
                SetControlPropertyThreadSafe(param2MaxUstavka_textBox, "Text", value);
            }
        }

        public string Param3MaxUstavkaTextBox
        {
            set
            {
                SetControlPropertyThreadSafe(param3MaxUstavka_textBox, "Text", value);
            }
        }

        public string Param4MaxUstavkaTextBox
        {
            set
            {
                SetControlPropertyThreadSafe(param4MaxUstavka_textBox, "Text", value);
            }
        }

        public string Param5MaxUstavkaTextBox
        {
            set
            {
                SetControlPropertyThreadSafe(param5MaxUstavka_textBox, "Text", value);
            }
        }

        public string Param6MaxUstavkaTextBox
        {
            set
            {
                SetControlPropertyThreadSafe(param6MaxUstavka_textBox, "Text", value);
            }
        }



        /// <summary>
        /// 
        /// </summary>
        public string U1ComboBox
        {
            get
            {
                if (U1_comboBox.InvokeRequired)
                {
                    string text = "";
                    this.Invoke(new MethodInvoker(delegate() { text = U1_comboBox.Text; }));
                    return text;
                }
                else
                {
                    return U1_comboBox.Text;
                }
            }
            set
            {
                SetControlPropertyThreadSafe(U1_comboBox, "Text", value);
            }
        }

        public string U2ComboBox
        {
            get
            {
                if (U2_comboBox.InvokeRequired)
                {
                    string text = "";
                    this.Invoke(new MethodInvoker(delegate() { text = U2_comboBox.Text; }));
                    return text;
                }
                else
                {
                    return U2_comboBox.Text;
                }
            }
            set
            {
                SetControlPropertyThreadSafe(U2_comboBox, "Text", value);
            }
        }

        public string I1ComboBox
        {
            get
            {
                if (I1_comboBox.InvokeRequired)
                {
                    string text = "";
                    this.Invoke(new MethodInvoker(delegate() { text = I1_comboBox.Text; }));
                    return text;
                }
                else
                {
                    return I1_comboBox.Text;
                }
            }
            set
            {
                SetControlPropertyThreadSafe(I1_comboBox, "Text", value);
            }
        }

        public string I2ComboBox
        {
            get
            {
                if (I2_comboBox.InvokeRequired)
                {
                    string text = "";
                    this.Invoke(new MethodInvoker(delegate() { text = I2_comboBox.Text; }));
                    return text;
                }
                else
                {
                    return I2_comboBox.Text;
                }
            }
            set
            {
                SetControlPropertyThreadSafe(I2_comboBox, "Text", value);
            }
        }

        public int MeasureSchemeComboBox
        {
            get
            {
                if (measureScheme_comboBox.InvokeRequired)
                {
                    int text = 0;
                    this.Invoke(new MethodInvoker(delegate() { text = measureScheme_comboBox.SelectedIndex; }));
                    return text;
                }
                else
                {
                    return measureScheme_comboBox.SelectedIndex;
                }
            }
            set
            {
                SetControlPropertyThreadSafe(measureScheme_comboBox, "SelectedIndex", value);
            }
        }

        public int IbComboBox
        {
            get
            {
                if (currentIb_comboBox.InvokeRequired)
                {
                    int text = 0;
                    this.Invoke(new MethodInvoker(delegate() { text = currentIb_comboBox.SelectedIndex; }));
                    return text;
                }
                else
                {
                    return currentIb_comboBox.SelectedIndex;
                }
            }
            set
            {
                SetControlPropertyThreadSafe(currentIb_comboBox, "SelectedIndex", value);
            }
        }
        public int QCalcMethodBox
        {
            set
            {
                SetControlPropertyThreadSafe(Q_CalcMethod_comboBox, "SelectedIndex", value);
            }
        }

        public string TextCorrectionModeButton
        {
            set
            {
                SetControlPropertyThreadSafe(enableCorrectionMode_button, "Text", value);
            }
        }

        public bool EnableCorrectionModeButton
        {
            set
            {
                SetControlPropertyThreadSafe(enableCorrectionMode_button, "Visible", value);
            }
        }

        public bool ParamsGroupBoxVisible
        {
            set
            {
                SetControlPropertyThreadSafe(params_groupBox, "Visible", value);
            }
        }

        public string UaChangeLabel
        {
            set
            {
                SetControlPropertyThreadSafe(UaChange_label, "Text", value);
            }
        }

        public string UbChangeLabel
        {
            set
            {
                SetControlPropertyThreadSafe(UbChange_label, "Text", value);
            }
        }

        public string UcChangeLabel
        {
            set
            {
                SetControlPropertyThreadSafe(UcChange_label, "Text", value);
            }
        }

        public string UabChangeLabel
        {
            set
            {
                SetControlPropertyThreadSafe(UabChange_label, "Text", value);
            }
        }

        public string UbcChangeLabel
        {
            set
            {
                SetControlPropertyThreadSafe(UbcChange_label, "Text", value);
            }
        }

        public string UcaChangeLabel
        {
            set
            {
                SetControlPropertyThreadSafe(UcaChange_label, "Text", value);
            }
        }

        public string IaChangeLabel
        {
            set
            {
                SetControlPropertyThreadSafe(IaChange_label, "Text", value);
            }
        }

        public string IbChangeLabel
        {
            set
            {
                SetControlPropertyThreadSafe(IbChange_label, "Text", value);
            }
        }

        public string IcChangeLabel
        {
            set
            {
                SetControlPropertyThreadSafe(IcChange_label, "Text", value);
            }
        }

        public string PaChangeLabel
        {
            set
            {
                SetControlPropertyThreadSafe(PaChange_label, "Text", value);
            }
        }

        public string PbChangeLabel
        {
            set
            {
                SetControlPropertyThreadSafe(PbChange_label, "Text", value);
            }
        }

        public string PcChangeLabel
        {
            set
            {
                SetControlPropertyThreadSafe(PcChange_label, "Text", value);
            }
        }

        public string PChangeLabel
        {
            set
            {
                SetControlPropertyThreadSafe(Pchange_label, "Text", value);
            }
        }

        public string QChangeLabel
        {
            set
            {
                SetControlPropertyThreadSafe(Qchange_label, "Text", value);
            }
        }

        public string FChangeLabel
        {
            set
            {
                SetControlPropertyThreadSafe(Fchange_label, "Text", value);
            }
        }

        public bool SaveChangedParamsButton
        {
            set
            {
                SetControlPropertyThreadSafe(saveChangedParams_button, "Enabled", value);
            }
        }

        public string SaveStatusLabel
        {
            set
            {
                SetControlPropertyThreadSafe(saveStatus_label, "Text", value);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public int SelectActiveImpOutRadioButton
        {
            set
            {
                RadioButton temp = radioButton11;
                if (value == 1) temp = radioButton12;
                else if (value == 2) temp = radioButton13;
                SetControlPropertyThreadSafe(temp, "Checked", true);
            }
        }

        public int SelectReactiveImpOutRadioButton
        {
            set
            {
                RadioButton temp = radioButton4;
                if (value == 1) temp = radioButton5;
                else if (value == 2) temp = radioButton6;
                else if (value == 3) temp = radioButton7;
                else if (value == 4) temp = radioButton8;
                else if (value == 5) temp = radioButton9;
                else if (value == 6) temp = radioButton10;
                SetControlPropertyThreadSafe(temp, "Checked", true);
            }
        }

        public string ImpulseWtHComboBox
        {
            set
            {
                SetControlPropertyThreadSafe(impWtH_comboBox, "Text", value);
            }
        }

        public string DateTimeTextBox
        {
            set
            {
                SetControlPropertyThreadSafe(dateTime_textBox, "Text", value);
            }
        }

        public string DeviceDateTimeTextBox
        {
            set
            {
                SetControlPropertyThreadSafe(deviceDateTime_textBox, "Text", value);
            }
        }

        public string DifferenceDTTextBox
        {
            set
            {
                SetControlPropertyThreadSafe(differenceDT_textBox, "Text", value);
            }
        }

        public decimal HourNumericUpDownValue
        {
            set
            {
                SetControlPropertyThreadSafe(hour_numericUpDown, "Value", value);
            }
        }

        public decimal MinuteNumericUpDownValue
        {
            set
            {
                SetControlPropertyThreadSafe(minute_numericUpDown, "Value", value);
            }
        }

        public decimal SecondsNumericUpDownValue
        {
            set
            {
                SetControlPropertyThreadSafe(seconds_numericUpDown, "Value", value);
            }
        }

        public DateTime SetCalendarValue
        {
            set
            {
                SetControlPropertyThreadSafe(monthCalendar, "SelectionStart", value);
            }
        }


        /// <summary>
        /// 
        /// </summary>
        public bool SaveAnalogOutsButton
        {
            set
            {
                SetControlPropertyThreadSafe(saveAnalogouts_button, "Enabled", value);
            }
        }

        public int AnalogOutsRadioButtons
        {
            get
            {
                if (checkBox_EnergyChoose.InvokeRequired)
                {
                    bool temp = false;

                    this.Invoke(new MethodInvoker(delegate() { temp = radioButton1.Checked; }));
                    if (temp) return 1;

                    this.Invoke(new MethodInvoker(delegate() { temp = radioButton2.Checked; }));
                    if (temp) return 2;

                    this.Invoke(new MethodInvoker(delegate() { temp = radioButton3.Checked; }));
                    if (temp) return 3;
                }
                else
                {
                    if (radioButton1.Checked) return 1;
                    else if (radioButton2.Checked) return 2;
                    else if (radioButton3.Checked) return 3;
                }
                return 0;
            }
        }

        public int AnalogOutsTypeComboBox
        {
            set
            {
                SetControlPropertyThreadSafe(analogOutsType_comboBox, "SelectedIndex", value);
            }
            get
            {
                if (analogOutsType_comboBox.InvokeRequired)
                {
                    int text = 0;
                    this.Invoke(new MethodInvoker(delegate() { text = analogOutsType_comboBox.SelectedIndex; }));
                    return text;
                }
                else
                {
                    return analogOutsType_comboBox.SelectedIndex;
                }
            }
        }

        public bool AnalogOutsTypeGroupBox
        {
            set
            {
                SetControlPropertyThreadSafe(groupBox46, "Enabled", value);
            }
        }

        public bool AnalogOutsRangesGroupBox
        {
            set
            {
                SetControlPropertyThreadSafe(groupBox44, "Enabled", value);
            }
        }

        public bool AnalogOutsGroupBoxes
        {
            set
            {
                SetControlPropertyThreadSafe(groupBox47, "Enabled", value);
                SetControlPropertyThreadSafe(groupBox50, "Enabled", value);
                SetControlPropertyThreadSafe(groupBox53, "Enabled", value);
            }
        }

        public string AnalogCorrectLabel
        {
            set
            {
                SetControlPropertyThreadSafe(writeCorrectAnalogParam_label, "Text", value);
            }
        }

        public string ClockCheckButton
        {
            get
            {
                if (checkInputTimer_button.InvokeRequired)
                {
                    string text = "";
                    this.Invoke(new MethodInvoker(delegate() { text = checkInputTimer_button.Text; }));
                    return text;
                }
                else
                {
                    return checkInputTimer_button.Text;
                }
            }
            set
            {
                SetControlPropertyThreadSafe(checkInputTimer_button, "Text", value);
            }
        }


        /// <summary>
        /// 
        /// </summary>
        public int Row1IndicatorParamComboBox
        {
            get
            {
                if (indicatorRow1_comboBox.InvokeRequired)
                {
                    int temp = 0;
                    this.Invoke(new MethodInvoker(delegate() { temp = indicatorRow1_comboBox.SelectedIndex; }));
                    return temp;
                }
                else
                {
                    return indicatorRow1_comboBox.SelectedIndex;
                }
            }
            set
            {
                SetControlPropertyThreadSafe(indicatorRow1_comboBox, "SelectedIndex", value);
            }
        }

        public int Row2IndicatorParamComboBox
        {
            get
            {
                if (indicatorRow2_comboBox.InvokeRequired)
                {
                    int temp = 0;
                    this.Invoke(new MethodInvoker(delegate() { temp = indicatorRow2_comboBox.SelectedIndex; }));
                    return temp;
                }
                else
                {
                    return indicatorRow2_comboBox.SelectedIndex;
                }
            }
            set
            {
                SetControlPropertyThreadSafe(indicatorRow2_comboBox, "SelectedIndex", value);
            }
        }

        public int Row3IndicatorParamComboBox
        {
            get
            {
                if (indicatorRow3_comboBox.InvokeRequired)
                {
                    int temp = 0;
                    this.Invoke(new MethodInvoker(delegate() { temp = indicatorRow3_comboBox.SelectedIndex; }));
                    return temp;
                }
                else
                {
                    return indicatorRow3_comboBox.SelectedIndex;
                }
            }
            set
            {
                SetControlPropertyThreadSafe(indicatorRow3_comboBox, "SelectedIndex", value);
            }
        }


        public string ParamIndicator1MinUstavkaTextBox
        {
            get
            {
                if (indicator1MinUstavka_textBox.InvokeRequired)
                {
                    string temp = "";
                    this.Invoke(new MethodInvoker(delegate() { temp = indicator1MinUstavka_textBox.Text; }));
                    return temp;
                }
                else
                {
                    return indicator1MinUstavka_textBox.Text;
                }
            }
            set
            {
                SetControlPropertyThreadSafe(indicator1MinUstavka_textBox, "Text", value);
            }
        }

        public string ParamIndicator2MinUstavkaTextBox
        {
            get
            {
                if (indicator2MinUstavka_textBox.InvokeRequired)
                {
                    string temp = "";
                    this.Invoke(new MethodInvoker(delegate() { temp = indicator2MinUstavka_textBox.Text; }));
                    return temp;
                }
                else
                {
                    return indicator2MinUstavka_textBox.Text;
                }
            }
            set
            {
                SetControlPropertyThreadSafe(indicator2MinUstavka_textBox, "Text", value);
            }
        }

        public string ParamIndicator3MinUstavkaTextBox
        {
            get
            {
                if (indicator3MinUstavka_textBox.InvokeRequired)
                {
                    string temp = "";
                    this.Invoke(new MethodInvoker(delegate() { temp = indicator3MinUstavka_textBox.Text; }));
                    return temp;
                }
                else
                {
                    return indicator3MinUstavka_textBox.Text;
                }
            }
            set
            {
                SetControlPropertyThreadSafe(indicator3MinUstavka_textBox, "Text", value);
            }
        }


        public string ParamIndicator1MaxUstavkaTextBox
        {
            get
            {
                if (indicator1MaxUstavka_textBox.InvokeRequired)
                {
                    string temp = "";
                    this.Invoke(new MethodInvoker(delegate() { temp = indicator1MaxUstavka_textBox.Text; }));
                    return temp;
                }
                else
                {
                    return indicator1MaxUstavka_textBox.Text;
                }
            }
            set
            {
                SetControlPropertyThreadSafe(indicator1MaxUstavka_textBox, "Text", value);
            }
        }

        public string ParamIndicator2MaxUstavkaTextBox
        {
            get
            {
                if (indicator2MaxUstavka_textBox.InvokeRequired)
                {
                    string temp = "";
                    this.Invoke(new MethodInvoker(delegate() { temp = indicator2MaxUstavka_textBox.Text; }));
                    return temp;
                }
                else
                {
                    return indicator2MaxUstavka_textBox.Text;
                }
            }
            set
            {
                SetControlPropertyThreadSafe(indicator2MaxUstavka_textBox, "Text", value);
            }
        }

        public string ParamIndicator3MaxUstavkaTextBox
        {
            get
            {
                if (indicator3MaxUstavka_textBox.InvokeRequired)
                {
                    string temp = "";
                    this.Invoke(new MethodInvoker(delegate() { temp = indicator3MaxUstavka_textBox.Text; }));
                    return temp;
                }
                else
                {
                    return indicator3MaxUstavka_textBox.Text;
                }
            }
            set
            {
                SetControlPropertyThreadSafe(indicator3MaxUstavka_textBox, "Text", value);
            }
        }

        public int SelectIndicatorShcemeTypeRadioButton
        {
            get
            {
                if (radioButton14.InvokeRequired)
                {
                    bool temp = false;
                    this.Invoke(new MethodInvoker(delegate() { temp = radioButton14.Checked; }));
                    if (temp) return 1;
                    else return 0;
                }
                else
                {
                    if (radioButton14.Checked) return 1;
                    else return 0;
                }
            }
            set
            {
                RadioButton temp = null;
                if (value == 1) temp = radioButton14;
                else if (value == 0) temp = radioButton15;
                SetControlPropertyThreadSafe(temp, "Checked", true);
            }
        }


        public bool RelayGroupBoxEnabled
        {
            get
            {
                if (groupBox75.InvokeRequired)
                {
                    bool temp = false;
                    this.Invoke(new MethodInvoker(delegate() { temp = groupBox75.Enabled; }));
                    return temp;
                }
                else
                {
                    return groupBox75.Enabled;
                }
            }
            set
            {
                SetControlPropertyThreadSafe(groupBox75, "Enabled", value);
            }
        }

        public bool DisplayLightEnabled
        {
            get
            {
                if (groupBox26.InvokeRequired)
                {
                    bool temp = false;
                    this.Invoke(new MethodInvoker(delegate() { temp = groupBox26.Enabled; }));
                    return temp;
                }
                else
                {
                    return groupBox26.Enabled;
                }
            }
            set
            {
                SetControlPropertyThreadSafe(groupBox26, "Enabled", value);
            }
        }

        public bool NoiseGroupBoxEnabled
        {
            get
            {
                if (groupBox42.InvokeRequired)
                {
                    bool temp = false;
                    this.Invoke(new MethodInvoker(delegate() { temp = groupBox42.Enabled; }));
                    return temp;
                }
                else
                {
                    return groupBox42.Enabled;
                }
            }
            set
            {
                SetControlPropertyThreadSafe(groupBox42, "Enabled", value);
            }
        }



        private delegate void SetEventDataGridDelegate(string DT, string additionalInfo);

        public void SetEventDataGrid(string DT, string additionalInfo)
        {
            if (dataGridView1.InvokeRequired)
            {
                dataGridView1.BeginInvoke(new SetEventDataGridDelegate(SetEventDataGrid), new object[] { DT, additionalInfo });
            }
            else
            {
                //  control.GetType().InvokeMember(propertyName, BindingFlags.SetProperty, null, control, new object[] { propertyValue });
                dataGridView1.Rows.Add();
                dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[0].Value = dataGridView1.Rows.Count;
                dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[1].Value = DT;
                dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[2].Value = additionalInfo;
            }
        }


        private delegate void ReadEventsButtonPushDelegate();

        public void ReadEventsButtonPush()
        {

            if (readEventjournal_button.InvokeRequired)
            {
                readEventjournal_button.BeginInvoke(new ReadEventsButtonPushDelegate(ReadEventsButtonPush), new object[] { });
            }
            else
            {
                readEventjournal_button.PerformClick();
                readAveragePKE_Button.PerformClick();
            }
        }






        private delegate void SetEnergyDataGridDelegate(CRC_RB.PackageData data);

        public void SetEnergyDataGrid(CRC_RB.PackageData data)
        {
            if (dataGridView2.InvokeRequired)
            {
                dataGridView2.BeginInvoke(new SetEnergyDataGridDelegate(SetEnergyDataGrid), new object[] { data });
            }
            else
            {
                dataGridView2.Rows.Add();
                dataGridView2.Rows[dataGridView2.Rows.Count - 1].Cells[0].Value = dataGridView2.Rows.Count;
                dataGridView2.Rows[dataGridView2.Rows.Count - 1].Cells[1].Value = data.dt.ToString("dd.MM.yyyy");
                dataGridView2.Rows[dataGridView2.Rows.Count - 1].Cells[2].Value = data.dt.ToString("HH:mm");

                if (firstEnParam_comboBox.SelectedIndex == 0)
                    dataGridView2.Rows[dataGridView2.Rows.Count - 1].Cells[3].Value = data.Wa.ToString("N3");
                else if (firstEnParam_comboBox.SelectedIndex == 1)
                    dataGridView2.Rows[dataGridView2.Rows.Count - 1].Cells[3].Value = data.Wa_plus.ToString("N3");
                else if (firstEnParam_comboBox.SelectedIndex == 2)
                    dataGridView2.Rows[dataGridView2.Rows.Count - 1].Cells[3].Value = data.Wa_minus.ToString("N3");
                else if (firstEnParam_comboBox.SelectedIndex == 3)
                    dataGridView2.Rows[dataGridView2.Rows.Count - 1].Cells[3].Value = data.Wr.ToString("N3");
                else if (firstEnParam_comboBox.SelectedIndex == 4)
                    dataGridView2.Rows[dataGridView2.Rows.Count - 1].Cells[3].Value = data.Wr_plus.ToString("N3");
                else if (firstEnParam_comboBox.SelectedIndex == 5)
                    dataGridView2.Rows[dataGridView2.Rows.Count - 1].Cells[3].Value = data.Wr_minus.ToString("N3");
                else if (firstEnParam_comboBox.SelectedIndex == 6)
                    dataGridView2.Rows[dataGridView2.Rows.Count - 1].Cells[3].Value = data.Wr1.ToString("N3");
                else if (firstEnParam_comboBox.SelectedIndex == 7)
                    dataGridView2.Rows[dataGridView2.Rows.Count - 1].Cells[3].Value = data.Wr2.ToString("N3");
                else if (firstEnParam_comboBox.SelectedIndex == 8)
                    dataGridView2.Rows[dataGridView2.Rows.Count - 1].Cells[3].Value = data.Wr3.ToString("N3");
                else if (firstEnParam_comboBox.SelectedIndex == 9)
                    dataGridView2.Rows[dataGridView2.Rows.Count - 1].Cells[3].Value = data.Wr4.ToString("N3");

                if (secondEnParam_comboBox.SelectedIndex == 0)
                    dataGridView2.Rows[dataGridView2.Rows.Count - 1].Cells[4].Value = data.Wa.ToString("N3");
                else if (secondEnParam_comboBox.SelectedIndex == 1)
                    dataGridView2.Rows[dataGridView2.Rows.Count - 1].Cells[4].Value = data.Wa_plus.ToString("N3");
                else if (secondEnParam_comboBox.SelectedIndex == 2)
                    dataGridView2.Rows[dataGridView2.Rows.Count - 1].Cells[4].Value = data.Wa_minus.ToString("N3");
                else if (secondEnParam_comboBox.SelectedIndex == 3)
                    dataGridView2.Rows[dataGridView2.Rows.Count - 1].Cells[4].Value = data.Wr.ToString("N3");
                else if (secondEnParam_comboBox.SelectedIndex == 4)
                    dataGridView2.Rows[dataGridView2.Rows.Count - 1].Cells[4].Value = data.Wr_plus.ToString("N3");
                else if (secondEnParam_comboBox.SelectedIndex == 5)
                    dataGridView2.Rows[dataGridView2.Rows.Count - 1].Cells[4].Value = data.Wr_minus.ToString("N3");
                else if (secondEnParam_comboBox.SelectedIndex == 6)
                    dataGridView2.Rows[dataGridView2.Rows.Count - 1].Cells[4].Value = data.Wr1.ToString("N3");
                else if (secondEnParam_comboBox.SelectedIndex == 7)
                    dataGridView2.Rows[dataGridView2.Rows.Count - 1].Cells[4].Value = data.Wr2.ToString("N3");
                else if (secondEnParam_comboBox.SelectedIndex == 8)
                    dataGridView2.Rows[dataGridView2.Rows.Count - 1].Cells[4].Value = data.Wr3.ToString("N3");
                else if (secondEnParam_comboBox.SelectedIndex == 9)
                    dataGridView2.Rows[dataGridView2.Rows.Count - 1].Cells[4].Value = data.Wr4.ToString("N3");

                dataGridView2.Rows[dataGridView2.Rows.Count - 1].Cells[5].Value = data.Notation;
            }
        }


        private delegate void SetEnergyChartDelegate(CRC_RB.PackageData data);

        public void SetEnergyChart(CRC_RB.PackageData data)
        {
            if (chart1.InvokeRequired)
            {
                chart1.BeginInvoke(new SetEnergyChartDelegate(SetEnergyChart), new object[] { data });
            }
            else
            {
                try
                {
                    if (firstEnParam_comboBox.SelectedIndex == 0)
                        chart1.Series[0].Points.AddXY(data.dt.ToOADate(), data.Wa);
                    else if (firstEnParam_comboBox.SelectedIndex == 1)
                        chart1.Series[0].Points.AddXY(data.dt.ToOADate(), data.Wa_plus);
                    else if (firstEnParam_comboBox.SelectedIndex == 2)
                        chart1.Series[0].Points.AddXY(data.dt.ToOADate(), data.Wa_minus);
                    else if (firstEnParam_comboBox.SelectedIndex == 3)
                        chart1.Series[0].Points.AddXY(data.dt.ToOADate(), data.Wr);
                    else if (firstEnParam_comboBox.SelectedIndex == 4)
                        chart1.Series[0].Points.AddXY(data.dt.ToOADate(), data.Wr_plus);
                    else if (firstEnParam_comboBox.SelectedIndex == 5)
                        chart1.Series[0].Points.AddXY(data.dt.ToOADate(), data.Wr_minus);
                    else if (firstEnParam_comboBox.SelectedIndex == 6)
                        chart1.Series[0].Points.AddXY(data.dt.ToOADate(), data.Wr1);
                    else if (firstEnParam_comboBox.SelectedIndex == 7)
                        chart1.Series[0].Points.AddXY(data.dt.ToOADate(), data.Wr2);
                    else if (firstEnParam_comboBox.SelectedIndex == 8)
                        chart1.Series[0].Points.AddXY(data.dt.ToOADate(), data.Wr3);
                    else if (firstEnParam_comboBox.SelectedIndex == 9)
                        chart1.Series[0].Points.AddXY(data.dt.ToOADate(), data.Wr3);

                    if (secondEnParam_comboBox.SelectedIndex == 0)
                        chart1.Series[1].Points.AddXY(data.dt.ToOADate(), data.Wa);
                    else if (secondEnParam_comboBox.SelectedIndex == 1)
                        chart1.Series[1].Points.AddXY(data.dt.ToOADate(), data.Wa_plus);
                    else if (secondEnParam_comboBox.SelectedIndex == 2)
                        chart1.Series[1].Points.AddXY(data.dt.ToOADate(), data.Wa_minus);
                    else if (secondEnParam_comboBox.SelectedIndex == 3)
                        chart1.Series[1].Points.AddXY(data.dt.ToOADate(), data.Wr);
                    else if (secondEnParam_comboBox.SelectedIndex == 4)
                        chart1.Series[1].Points.AddXY(data.dt.ToOADate(), data.Wr_plus);
                    else if (secondEnParam_comboBox.SelectedIndex == 5)
                        chart1.Series[1].Points.AddXY(data.dt.ToOADate(), data.Wr_minus);
                    else if (secondEnParam_comboBox.SelectedIndex == 6)
                        chart1.Series[1].Points.AddXY(data.dt.ToOADate(), data.Wr1);
                    else if (secondEnParam_comboBox.SelectedIndex == 7)
                        chart1.Series[1].Points.AddXY(data.dt.ToOADate(), data.Wr2);
                    else if (secondEnParam_comboBox.SelectedIndex == 8)
                        chart1.Series[1].Points.AddXY(data.dt.ToOADate(), data.Wr3);
                    else if (secondEnParam_comboBox.SelectedIndex == 9)
                        chart1.Series[1].Points.AddXY(data.dt.ToOADate(), data.Wr3);
                }
                catch
                {

                }

                //dataGridView2.Rows[dataGridView2.Rows.Count - 1].Cells[5].Value = data.Notation;
            }
        }


        private delegate void ReadEnergyButtonPushDelegate();

        public void ReadEnergyButtonPush()
        {

            if (readEnergy_button.InvokeRequired)
            {
                readEnergy_button.BeginInvoke(new ReadEnergyButtonPushDelegate(ReadEnergyButtonPush), new object[] { });
            }
            else
            {
                readEnergy_button.PerformClick();
            }
        }


        public string CommunicationComboBox
        {
            get
            {
                if (communication_comboBox.InvokeRequired)
                {
                    string text = "";
                    try
                    {
                        this.Invoke(new MethodInvoker(delegate() { text = communication_comboBox.Text; }));
                    }
                    catch { }
                    return text;
                }
                else
                {
                    return communication_comboBox.Text;
                }
            }
        }

        public string IPAddressComboBox
        {
            get
            {
                if (ethernetAddr_comboBox.InvokeRequired)
                {
                    string text = "";
                    this.Invoke(new MethodInvoker(delegate() { text = ethernetAddr_comboBox.Text; }));
                    return text;
                }
                else
                {
                    return ethernetAddr_comboBox.Text;
                }
            }
        }


        private delegate void AddIPToComboBoxDelegate(string text);

        public void AddIPToComboBox(string text)
        {
            if (ethernetAddr_comboBox.InvokeRequired)
            {
                ethernetAddr_comboBox.BeginInvoke(new AddIPToComboBoxDelegate(AddIPToComboBox), new object[] { text });
            }
            else
            {
                if (ethernetAddr_comboBox.Items.IndexOf(text) == -1)
                    ethernetAddr_comboBox.Items.Add(text);
            }
        }



        public string IPAddressTextBox
        {
            get
            {
                if (IPAddress_textBox.InvokeRequired)
                {
                    string temp = "";
                    this.Invoke(new MethodInvoker(delegate() { temp = IPAddress_textBox.Text; }));
                    return temp;
                }
                else
                {
                    return IPAddress_textBox.Text;
                }
            }
            set
            {
                SetControlPropertyThreadSafe(IPAddress_textBox, "Text", value);
            }
        }


        private delegate void SetEnergyPrefixDelegate(float U1, float I1);

        public void SetEnergyPrefix(float U1, float I1)
        {
            if (dataGridView2.InvokeRequired)
            {
                readEventjournal_button.BeginInvoke(new SetEnergyPrefixDelegate(SetEnergyPrefix), new object[] { U1, I1 });
            }
            else
            {
                ushort unitShift = modbus.CalculateUnitShfit((U1 * I1) * (float)Math.Sqrt(3.0), false); // / sqrt(3.0)

                string prefix = "";
                if (firstEnParam_comboBox.SelectedIndex >= 0 && firstEnParam_comboBox.SelectedIndex <= 2)
                {
                    if (unitShift == 0) prefix = "Вт·ч";
                    else if (unitShift == 1) prefix = "кВт·ч";
                    else if (unitShift == 2) prefix = "МВт·ч";
                    else if (unitShift == 3) prefix = "ГВт·ч";
                }
                else
                {
                    if (unitShift == 0) prefix = "вар·ч";
                    else if (unitShift == 1) prefix = "квар·ч";
                    else if (unitShift == 2) prefix = "Мвар·ч";
                    else if (unitShift == 3) prefix = "Гвар·ч";
                }
                dataGridView2.Columns[3].HeaderText = firstEnParam_comboBox.Text + ", " + prefix;
                chart1.Series[0].LegendText = firstEnParam_comboBox.Text + ", " + prefix;

                if (secondEnParam_comboBox.SelectedIndex >= 0 && secondEnParam_comboBox.SelectedIndex <= 2)
                {
                    if (unitShift == 0) prefix = "Вт·ч";
                    else if (unitShift == 1) prefix = "кВт·ч";
                    else if (unitShift == 2) prefix = "МВт·ч";
                    else if (unitShift == 3) prefix = "ГВт·ч";
                }
                else
                {
                    if (unitShift == 0) prefix = "вар·ч";
                    else if (unitShift == 1) prefix = "квар·ч";
                    else if (unitShift == 2) prefix = "Мвар·ч";
                    else if (unitShift == 3) prefix = "Гвар·ч";
                }
                dataGridView2.Columns[4].HeaderText = secondEnParam_comboBox.Text + ", " + prefix;
                chart1.Series[1].LegendText = secondEnParam_comboBox.Text + ", " + prefix;
            }
        }



        public int Row1AnalogOutComboBox
        {
            set
            {
                SetControlPropertyThreadSafe(analogOut_1_ComboBox, "SelectedIndex", value);
            }
        }

        public int Row2AnalogOutComboBox
        {
            set
            {
                SetControlPropertyThreadSafe(analogOut_2_ComboBox, "SelectedIndex", value);
            }
        }

        public int Row3AnalogOutComboBox
        {
            set
            {
                SetControlPropertyThreadSafe(analogOut_3_ComboBox, "SelectedIndex", value);
            }
        }


        public string dUplusTextBox
        {
            set
            {
                SetControlPropertyThreadSafe(dUpl_TextBox, "Text", value);
            }
        }

        public string dUminusTextBox
        {
            set
            {
                SetControlPropertyThreadSafe(dUmin_TextBox, "Text", value);
            }
        }

        public string K0UTextBox
        {
            set
            {
                SetControlPropertyThreadSafe(K0U_TextBox, "Text", value);
            }
        }

        public string K2UTextBox
        {
            set
            {
                SetControlPropertyThreadSafe(K2U_TextBox, "Text", value);
            }
        }

        public string dFTextBox
        {
            set
            {
                SetControlPropertyThreadSafe(dF_TextBox, "Text", value);
            }
        }

        public string dIplusTextBox
        {
            set
            {
                SetControlPropertyThreadSafe(dIpl_TextBox, "Text", value);
            }
        }

        public string dIminusTextBox
        {
            set
            {
                SetControlPropertyThreadSafe(dImin_TextBox, "Text", value);
            }
        }

        public string K0ITextBox
        {
            set
            {
                SetControlPropertyThreadSafe(K0I_TextBox, "Text", value);
            }
        }

        public string K2ITextBox
        {
            set
            {
                SetControlPropertyThreadSafe(K2I_TextBox, "Text", value);
            }
        }


        private delegate void SetHarmonicsDataGridDelegate(int fase, int harmonic, string value, int table);

        public void SetHarmonicsDataGrid(int fase, int harmonic, string value, int table)
        {
            if (harmonicsV_GridView.InvokeRequired)
            {
                harmonicsV_GridView.BeginInvoke(new SetHarmonicsDataGridDelegate(SetHarmonicsDataGrid), new object[] { fase, harmonic, value, table });
            }
            else
            {
                if (table == 0) harmonicsV_GridView.Rows[harmonic].Cells[fase + 1].Value = value;
                else if (table == 1) harmonicsI_GridView.Rows[harmonic].Cells[fase + 1].Value = value;
                else if (table == 2) interharmonicsV_GridView.Rows[harmonic].Cells[fase + 1].Value = value;
                else if (table == 3) interharmonicsI_GridView.Rows[harmonic].Cells[fase + 1].Value = value;
            }
        }

        private delegate int HarmonicsDataGridDelegate();

        public int GetActiveHarmonicTable()
        {
            if (harmonics_TabContor.InvokeRequired)
            {
                harmonics_TabContor.BeginInvoke(new HarmonicsDataGridDelegate(GetActiveHarmonicTable), new object[] { });
                return 0;
            }
            else
            {
                if (harmonics_TabContor.SelectedTab == harmonics_TabContor.TabPages[0]) return 0;
                else if (harmonics_TabContor.SelectedTab == harmonics_TabContor.TabPages[1]) return 1;
                else if (harmonics_TabContor.SelectedTab == harmonics_TabContor.TabPages[2]) return 2;
                else if (harmonics_TabContor.SelectedTab == harmonics_TabContor.TabPages[3]) return 3;
                return 0;
            }
        }


        private delegate void SetPKEDataGridDelegate(string row, string DT, string additionalInfo);

        public void SetPKEDataGrid(string row, string DT, string additionalInfo)
        {
            if (PKE_Table.InvokeRequired)
            {
                PKE_Table.BeginInvoke(new SetPKEDataGridDelegate(SetPKEDataGrid), new object[] { row, DT, additionalInfo });
            }
            else
            {
                //  control.GetType().InvokeMember(propertyName, BindingFlags.SetProperty, null, control, new object[] { propertyValue });
                PKE_Table.Rows.Add();
                if (row != "-1") PKE_Table.Rows[PKE_Table.Rows.Count - 1].Cells[0].Value = row;
                else PKE_Table.Rows[PKE_Table.Rows.Count - 1].Cells[0].Value = PKE_Table.Rows.Count;
                PKE_Table.Rows[PKE_Table.Rows.Count - 1].Cells[1].Value = DT;
                PKE_Table.Rows[PKE_Table.Rows.Count - 1].Cells[2].Value = additionalInfo;
            }
        }


        private delegate void SetVoltagePKEDataGridDelegate(int row, int column, int data);

        public void SetDIPVoltagePKEDataGrid(int row, int column, int data)
        {
            if (dipVoltage_DGV.InvokeRequired)
            {
                dipVoltage_DGV.BeginInvoke(new SetVoltagePKEDataGridDelegate(SetDIPVoltagePKEDataGrid), new object[] { row, column, data });
            }
            else
            {
                dipVoltage_DGV.Rows[row].Cells[column].Value = data;
            }
        }

        private delegate void SetIEC_DGVDelegate(int column, bool data);

        public void SetIEC101_DGV(int column, bool data)
        {
            if (IEC101Settings.groupsDGV.DGV.InvokeRequired)
            {
                IEC101Settings.groupsDGV.DGV.BeginInvoke(new SetIEC_DGVDelegate(SetIEC101_DGV), new object[] { column, data });
            }
            else
            {
                IEC101Settings.groupsDGV.DGV.Rows[column].Cells[1].Value = data;
            }
        }

        public void SetIEC104_DGV(int column, bool data)
        {
            if (IEC104Settings.groupsDGV.DGV.InvokeRequired)
            {
                IEC104Settings.groupsDGV.DGV.BeginInvoke(new SetIEC_DGVDelegate(SetIEC104_DGV), new object[] { column, data });
            }
            else
            {
                IEC104Settings.groupsDGV.DGV.Rows[column].Cells[1].Value = data;
            }
        }

        public void SetNOVoltagePKEDataGrid(int row, int column, int data)
        {
            if (noVoltage_DGV.InvokeRequired)
            {
                noVoltage_DGV.BeginInvoke(new SetVoltagePKEDataGridDelegate(SetNOVoltagePKEDataGrid), new object[] { row, column, data });
            }
            else
            {
                noVoltage_DGV.Rows[row].Cells[column].Value = data;
            }
        }

        public void SetOverVoltagePKEDataGrid(int row, int column, int data)
        {
            if (overVoltage_DGV.InvokeRequired)
            {
                overVoltage_DGV.BeginInvoke(new SetVoltagePKEDataGridDelegate(SetOverVoltagePKEDataGrid), new object[] { row, column, data });
            }
            else
            {
                overVoltage_DGV.Rows[row].Cells[column].Value = data;
            }
        }

        public bool IbShiftButtons
        {
            set
            {
                SetControlPropertyThreadSafe(button26, "Enabled", value);
                SetControlPropertyThreadSafe(button27, "Enabled", value);
                SetControlPropertyThreadSafe(button28, "Enabled", value);
                SetControlPropertyThreadSafe(button29, "Enabled", value);
            }
        }


        public string UaInfoLabel
        {
            set
            {
                SetControlPropertyThreadSafe(UaInfo_Label, "Text", value);
            }
        }

        public string UbInfoLabel
        {
            set
            {
                SetControlPropertyThreadSafe(UbInfo_Label, "Text", value);
            }
        }

        public string UcInfoLabel
        {
            set
            {
                SetControlPropertyThreadSafe(UcInfo_Label, "Text", value);
            }
        }

        public string UfInfoLabel
        {
            set
            {
                SetControlPropertyThreadSafe(UfInfo_Label, "Text", value);
            }
        }

        public string UabInfoLabel
        {
            set
            {
                SetControlPropertyThreadSafe(UabInfo_Label, "Text", value);
            }
        }

        public string UbcInfoLabel
        {
            set
            {
                SetControlPropertyThreadSafe(UbcInfo_Label, "Text", value);
            }
        }

        public string UcaInfoLabel
        {
            set
            {
                SetControlPropertyThreadSafe(UcaInfo_Label, "Text", value);
            }
        }

        public string UlInfoLabel
        {
            set
            {
                SetControlPropertyThreadSafe(UlInfo_Label, "Text", value);
            }
        }

        public string IaInfoLabel
        {
            set
            {
                SetControlPropertyThreadSafe(IaInfo_Label, "Text", value);
            }
        }

        public string IbInfoLabel
        {
            set
            {
                SetControlPropertyThreadSafe(IbInfo_Label, "Text", value);
            }
        }

        public string IcInfoLabel
        {
            set
            {
                SetControlPropertyThreadSafe(IcInfo_Label, "Text", value);
            }
        }

        public string IInfoLabel
        {
            set
            {
                SetControlPropertyThreadSafe(IInfo_Label, "Text", value);
            }
        }

        public string PaInfoLabel
        {
            set
            {
                SetControlPropertyThreadSafe(PaInfo_Label, "Text", value);
            }
        }

        public string PbInfoLabel
        {
            set
            {
                SetControlPropertyThreadSafe(PbInfo_Label, "Text", value);
            }
        }

        public string PcInfoLabel
        {
            set
            {
                SetControlPropertyThreadSafe(PcInfo_Label, "Text", value);
            }
        }

        public string PInfoLabel
        {
            set
            {
                SetControlPropertyThreadSafe(PInfo_Label, "Text", value);
            }
        }

        public string QaInfoLabel
        {
            set
            {
                SetControlPropertyThreadSafe(QaInfo_Label, "Text", value);
            }
        }

        public string QbInfoLabel
        {
            set
            {
                SetControlPropertyThreadSafe(QbInfo_Label, "Text", value);
            }
        }

        public string QcInfoLabel
        {
            set
            {
                SetControlPropertyThreadSafe(QcInfo_Label, "Text", value);
            }
        }

        public string QInfoLabel
        {
            set
            {
                SetControlPropertyThreadSafe(QInfo_Label, "Text", value);
            }
        }

        public string SaInfoLabel
        {
            set
            {
                SetControlPropertyThreadSafe(SaInfo_Label, "Text", value);
            }
        }

        public string SbInfoLabel
        {
            set
            {
                SetControlPropertyThreadSafe(SbInfo_Label, "Text", value);
            }
        }

        public string ScInfoLabel
        {
            set
            {
                SetControlPropertyThreadSafe(ScInfo_Label, "Text", value);
            }
        }

        public string SInfoLabel
        {
            set
            {
                SetControlPropertyThreadSafe(SInfo_Label, "Text", value);
            }
        }

        public string KpaInfoLabel
        {
            set
            {
                SetControlPropertyThreadSafe(KpcInfo_Label, "Text", value);
            }
        }

        public string KpbInfoLabel
        {
            set
            {
                SetControlPropertyThreadSafe(KpbInfo_Label, "Text", value);
            }
        }

        public string KpcInfoLabel
        {
            set
            {
                SetControlPropertyThreadSafe(KpcInfo_Label, "Text", value);
            }
        }

        public string KpInfoLabel
        {
            set
            {
                SetControlPropertyThreadSafe(KpInfo_Label, "Text", value);
            }
        }

        public string FInfoLabel
        {
            set
            {
                SetControlPropertyThreadSafe(FInfo_Label, "Text", value);
            }
        }

        public string U0InfoLabel
        {
            set
            {
                SetControlPropertyThreadSafe(U0Info_Label, "Text", value);
            }
        }

        public string I0InfoLabel
        {
            set
            {
                SetControlPropertyThreadSafe(I0Info_Label, "Text", value);
            }
        }


        public int DayLightComboBox
        {
            get
            {
                if (daylightType_comboBox.InvokeRequired)
                {
                    int text = 0;
                    this.Invoke(new MethodInvoker(delegate() { text = daylightType_comboBox.SelectedIndex; }));
                    return text;
                }
                else
                {
                    return daylightType_comboBox.SelectedIndex;
                }
            }
            set
            {
                SetControlPropertyThreadSafe(daylightType_comboBox, "SelectedIndex", value);
            }
        }

        public string CTextBox
        {
            set
            {
                SetControlPropertyThreadSafe(C_textBox, "Text", value);
            }
        }

        public string CPercentTextBox
        {
            set
            {
                SetControlPropertyThreadSafe(C_Percent_textBox, "Text", value);
            }
        }

        public string CNominalTextBox
        {
            get
            {
                if (Cn_textBox.InvokeRequired)
                {
                    string text = "";
                    this.Invoke(new MethodInvoker(delegate() { text = Cn_textBox.Text; }));
                    return text;
                }
                else
                {
                    return Cn_textBox.Text;
                }
                //return (string)deviceNumber_textBox.GetType().InvokeMember("Text", BindingFlags.GetProperty, null, deviceNumber_textBox, new object[] { });
                //                 return deviceNumber_textBox.Text;
            }
        }
    }
}

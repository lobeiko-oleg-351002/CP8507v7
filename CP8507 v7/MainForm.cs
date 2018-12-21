using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO.Ports;
using System.Drawing.Text;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Forms.DataVisualization.Charting;
using System.Text.RegularExpressions;
using System.Net.Sockets;
using System.Net;
using System.Collections;
using System.Runtime.InteropServices;
using System.Globalization;


namespace CP8507_v7
{
    public partial class MainForm : Form
    {
        public TcpClient tcpClient;
        public TarificationForm tarificationForm;
        public RandomPKE_DG randomPKEForm;

        InformationChannel infoChannel;
        public Modbus modbus;
        Indicator indicator;
        Indicator_23 indicator23;
        CRC_RB crc_rb;
        ELZIP elZIP;
        public TarifPro tarifPro;

        public AddSettings addSettings;
        public IEC101_SettingsForm IEC101Settings;
        public IEC104_SettingsForm IEC104Settings;

        public UppConfig uppConfigurator;

        System.Timers.Timer infoChannel_Timer;
        System.Timers.Timer statusLabel_Timer;
        public System.Timers.Timer sendInqury_Timer;
        System.Timers.Timer updateDateTime_Timer;
        System.Timers.Timer EthernetTimeoutTimer;

        private bool autoLoopIsOn = false;



        public enum PARAMS : int
        {
            Ua, Ub, Uc, Ia, Ib, Ic, Uab, Ubc, Uca, Pa, Pb, Pc, Qa, Qb, Qc, Sa, Sb, Sc, KPa, KPb, KPc, P, Q, S, KP, F, Umd, Imd, Ulmd, U0, I0,
            KU0, KU2, dF, dUplus, dUminus,
            _OVER_VOLTAGE, _DIP_VOLTAGE, _NO_VOLTAGE, reserve4, reserve5, reserve6, reserve7, reserve8, reserve9, reserve10,
            KUn_A, KUn_A2, KUn_A3, KUn_A4, KUn_A5, KUn_A6, KUn_A7, KUn_A8, KUn_A9, KUn_A10, KUn_A11, KUn_A12, KUn_A13, KUn_A14, KUn_A15, KUn_A16, KUn_A17, KUn_A18, KUn_A19, KUn_A20, KUn_A21, KUn_A22, KUn_A23, KUn_A24, KUn_A25, KUn_A26, KUn_A27, KUn_A28, KUn_A29, KUn_A30, KUn_A31,
            KUn_B, KUn_B2, KUn_B3, KUn_B4, KUn_B5, KUn_B6, KUn_B7, KUn_B8, KUn_B9, KUn_B10, KUn_B11, KUn_B12, KUn_B13, KUn_B14, KUn_B15, KUn_B16, KUn_B17, KUn_B18, KUn_B19, KUn_B20, KUn_B21, KUn_B22, KUn_B23, KUn_B24, KUn_B25, KUn_B26, KUn_B27, KUn_B28, KUn_B29, KUn_B30, KUn_B31,
            KUn_C, KUn_C2, KUn_C3, KUn_C4, KUn_C5, KUn_C6, KUn_C7, KUn_C8, KUn_C9, KUn_C10, KUn_C11, KUn_C12, KUn_C13, KUn_C14, KUn_C15, KUn_C16, KUn_C17, KUn_C18, KUn_C19, KUn_C20, KUn_C21, KUn_C22, KUn_C23, KUn_C24, KUn_C25, KUn_C26, KUn_C27, KUn_C28, KUn_C29, KUn_C30, KUn_C31
        }

        private enum PKE_PARAMS : int
        {
            _KU0, _KU2, _dF, _dUplus, _dUminus
        }


        public MainForm(StartLogo st)
        {
            InitializeComponent();

            trackBar_NoisePercent_Scroll(null, null);
            LoadSettings();

            infoChannel_Timer = new System.Timers.Timer();
            infoChannel_Timer.Elapsed += new System.Timers.ElapsedEventHandler(infoChannel_Timer_Tick);
            infoChannel_Timer.Interval = 1500;
            infoChannel_Timer.Stop();

            statusLabel_Timer = new System.Timers.Timer();
            statusLabel_Timer.Elapsed += new System.Timers.ElapsedEventHandler(ClearStatusLabel);
            statusLabel_Timer.Interval = 3000;
            statusLabel_Timer.Stop();

            sendInqury_Timer = new System.Timers.Timer();
            sendInqury_Timer.Elapsed += new System.Timers.ElapsedEventHandler(AutoSendInqury);
            sendInqury_Timer.Interval = 50;
            sendInqury_Timer.Stop();

            updateDateTime_Timer = new System.Timers.Timer();
            updateDateTime_Timer.Elapsed += new System.Timers.ElapsedEventHandler(UpdateDateTime);
            updateDateTime_Timer.Interval = 250;
            updateDateTime_Timer.Stop();

            EthernetTimeoutTimer = new System.Timers.Timer();
            EthernetTimeoutTimer.Elapsed += new System.Timers.ElapsedEventHandler(EthernetTimeout);
            EthernetTimeoutTimer.Interval = 50;
            EthernetTimeoutTimer.Stop();

            tcpClient = new TcpClient();
            EthernetTimeoutTimer.Start();

            infoChannel = new InformationChannel(this);
            modbus = new Modbus(this);
            indicator = new Indicator(this);
            indicator23 = new Indicator_23(this);
            crc_rb = new CRC_RB(this);
            elZIP = new ELZIP(this);
            tarifPro = new TarifPro(this);

            addSettings = new AddSettings(modbus);
            IEC101Settings = new IEC101_SettingsForm(modbus);
            IEC104Settings = new IEC104_SettingsForm(modbus);

            uppConfigurator = new UppConfig(this);

            relayN_comboBox.SelectedIndex = 0;
            firstEnParam_comboBox.SelectedIndex = 0;
            secondEnParam_comboBox.SelectedIndex = 3;
            energyJournals_comboBox.SelectedIndex = 0;
            tarifs_comboBox.SelectedIndex = 0;
            daylightType_comboBox.SelectedIndex = 0;
            //iecInfoAddress_comboBox.SelectedIndex = 0;

            daylightType_comboBox.DrawMode = DrawMode.OwnerDrawFixed;
            daylightType_comboBox.DrawItem += new DrawItemEventHandler(daylightType_comboBox_DrawItem);

            chart1.Legends[0].LegendStyle = LegendStyle.Row;
            chart1.Series[0].ChartType = SeriesChartType.Spline;
            chart1.Series[0].BorderWidth = 3;
            chart1.Series[0].MarkerStyle = MarkerStyle.Circle;
            chart1.Series[0].MarkerSize = 7;
            chart1.Series[0].MarkerColor = Color.FromArgb(255, 170, 170);

            chart1.Series[1].ChartType = SeriesChartType.Spline;
            chart1.Series[1].BorderWidth = 3;
            chart1.Series[1].MarkerStyle = MarkerStyle.Circle;
            chart1.Series[1].MarkerSize = 7;
            chart1.Series[1].MarkerColor = Color.FromArgb(110, 190, 190);

            chart1.Series[0].XValueType = ChartValueType.DateTime;
            chart1.ChartAreas[0].AxisX.LabelStyle.Format = "dd.MM.yy" + Environment.NewLine + "HH:mm";
            chart1.Series[0].YValueType = ChartValueType.Single;
            chart1.ChartAreas[0].AxisY.LabelStyle.Format = "N2";

            chart1.Series[1].XValueType = ChartValueType.DateTime;
            chart1.ChartAreas[0].AxisX.LabelStyle.Format = "dd.MM.yy" + Environment.NewLine + "HH:mm";
            chart1.Series[1].YValueType = ChartValueType.Single;
            chart1.ChartAreas[0].AxisY.LabelStyle.Format = "N2";

            chart1.MouseWheel += new MouseEventHandler(chData_MouseWheel);

            tabControl6.TabPages.Remove(tabPage25);
            tabControl6.TabPages.Remove(tabPage26);
            tabControl6.TabPages.Remove(tabPage27);

            st.Close();

            PKE_Table.AutoGenerateColumns = false;
            dipVoltage_DGV.AutoGenerateColumns = false;

            GenerateDipVoltageTable();

            for (int i = 0; i < 50; i++)
            {
                harmonicsV_GridView.Rows.Add();
                harmonicsI_GridView.Rows.Add();
                interharmonicsV_GridView.Rows.Add();
                interharmonicsI_GridView.Rows.Add();
                if (i == 0)
                {
                    harmonicsV_GridView.Rows[harmonicsV_GridView.Rows.Count - 1].Cells[0].Value = "Ku";
                    harmonicsI_GridView.Rows[harmonicsI_GridView.Rows.Count - 1].Cells[0].Value = "Ki";
                    interharmonicsV_GridView.Rows[interharmonicsV_GridView.Rows.Count - 1].Cells[0].Value = "Ku(0.5)";
                    interharmonicsI_GridView.Rows[interharmonicsI_GridView.Rows.Count - 1].Cells[0].Value = "Ki(0.5)";
                }
                else
                {
                    harmonicsV_GridView.Rows[harmonicsV_GridView.Rows.Count - 1].Cells[0].Value = "Ku(" + (i + 1) + ")";
                    harmonicsI_GridView.Rows[harmonicsI_GridView.Rows.Count - 1].Cells[0].Value = "Ki(" + (i + 1) + ")";
                    interharmonicsV_GridView.Rows[interharmonicsV_GridView.Rows.Count - 1].Cells[0].Value = "Ku(" + (i + 0.5) + ")";
                    interharmonicsI_GridView.Rows[interharmonicsI_GridView.Rows.Count - 1].Cells[0].Value = "Ki(" + (i + 0.5) + ")";
                }
            }
        }

        void daylightType_comboBox_DrawItem(object sender, DrawItemEventArgs e)
        {
            string text = "";
            if (e.Index == 1) text = "Перевод времени прозводится \nв последнее воскресенье марта в 2:00 на 1 час вперед\nи в последнее воскресенье октября в 3:00 на 1 час назад";
            else if (e.Index == 2) text = "Перевод времени прозводится \nв последнее воскресенье марта в 1:00 на 1 час вперед\nи в последнее воскресенье октября в 2:00 на 1 час назад";
            else if (e.Index == 3) text = "Перевод времени прозводится \nво второе воскресенье марта в 2:00 на 1 час вперед\nи в первое воскресенье ноября в 2:00 на 1 час назад";

            e.DrawBackground();
            using (SolidBrush br = new SolidBrush(e.ForeColor))
            {
                e.Graphics.DrawString(daylightType_comboBox.GetItemText(daylightType_comboBox.Items[e.Index]), e.Font, br, e.Bounds);
            }

            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
            {
                toolTip1.Show(text, daylightType_comboBox, e.Bounds.Right, e.Bounds.Bottom);
            }
            else
            {
                toolTip1.Hide(daylightType_comboBox);
            }
            e.DrawFocusRectangle();
        }


        public static void WriteLog(string line)
        {
            MessageBox.Show(line);
            //statusLabel = line;
#if DEBUG
            try
            {
                string path = "";
                int log_size = 30000;
                //пишем все сообщения, генерируемые службой во время работы, в локальный файл на диске
                FileStream fs1 = new FileStream(path + "log.txt", FileMode.Append);
                long lenght = fs1.Length;
                fs1.Dispose();
                if (lenght >= log_size)//log_size - предельный размер лог-файла в байтах
                {
                    File.Move(path + "log.txt", path + "log_" + DateTime.Now.ToShortDateString() + "." + DateTime.Now.Hour + "." + DateTime.Now.Minute + "." + DateTime.Now.Second + @".old");
                }
                FileStream fs2 = new FileStream(path + "log.txt", FileMode.Append);
                StreamWriter sw = new StreamWriter(fs2);
                sw.WriteLine(DateTime.Now.ToString() + " " + line);
                sw.WriteLine(" ");
                sw.Close();
                fs2.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка создания лога: " + ex.Message);
            }
#endif
        }

        private void LoadSettings()
        {
            try
            {
                string[] ports = SerialPort.GetPortNames();
                foreach (string port in ports)
                {
                    comPorts_comboBox.Items.Add(port);
                }
            }
            catch { }


            comPorts_comboBox.SelectedIndex = comPorts_comboBox.Items.IndexOf(Properties.Settings.Default.ComPort);
            if (comPorts_comboBox.SelectedIndex == -1 && comPorts_comboBox.Items.Count != 0) comPorts_comboBox.SelectedIndex = 0;

            comPortSpeed_comboBox.SelectedIndex = comPortSpeed_comboBox.Items.IndexOf(Properties.Settings.Default.ComPort_Speed);
            if (comPortSpeed_comboBox.SelectedIndex == -1) comPortSpeed_comboBox.SelectedIndex = comPortSpeed_comboBox.Items.IndexOf("9600");
            Protocol.TryToChangeBoudRate(ComPort, Convert.ToInt32(comPortSpeed_comboBox.Text));

            if (Properties.Settings.Default.NumberOfDevice == "") deviceNumber_textBox.Text = "1";
            else deviceNumber_textBox.Text = Properties.Settings.Default.NumberOfDevice;

            communication_comboBox.SelectedIndex = communication_comboBox.Items.IndexOf(Properties.Settings.Default.Commenication);
            if (communication_comboBox.SelectedIndex == -1 && communication_comboBox.Items.Count != 0) communication_comboBox.SelectedIndex = 0;

            if (Properties.Settings.Default.ipAddresses != null)
            {
                ArrayList arr = Properties.Settings.Default.ipAddresses;
                for (int i = 0; i < arr.Count; i++)
                {
                    ethernetAddr_comboBox.Items.Add(arr[i].ToString());
                    ethernetAddr_comboBox.SelectedIndex = i;
                }
            }
        }

        private delegate void ShowMessageBoxDelegate(string text);
        public void ShowMessageBox(string text)
        {
            if (this.InvokeRequired)
                this.BeginInvoke(new ShowMessageBoxDelegate(ShowMessageBox), new object[] { text });
            else
                MessageBox.Show(text);
        }

        private void checkBox_EnergyChoose_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_EnergyChoose.Checked)
            {
                groupBox_QuadrantEnergy.Enabled = true;
                groupBox_ActiveEnergy.Enabled = true;
                groupBox_ReactiveEnergy.Enabled = true;
            }
            else
            {
                groupBox_QuadrantEnergy.Enabled = false;
                groupBox_ActiveEnergy.Enabled = false;
                groupBox_ReactiveEnergy.Enabled = false;
            }
        }

        private void trackBar_NoisePercent_Scroll(object sender, EventArgs e)
        {
            noisePercent_label.Text = (noisePercent_trackBar.Value / 100.0).ToString("F2");
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            //запись настроек
            Properties.Settings.Default.ComPort = comPorts_comboBox.Text;
            Properties.Settings.Default.ComPort_Speed = comPortSpeed_comboBox.Text;
            Properties.Settings.Default.NumberOfDevice = deviceNumber_textBox.Text;
            Properties.Settings.Default.Commenication = communication_comboBox.Text;

            ArrayList arr = new ArrayList();
            if (ethernetAddr_comboBox.Items.Count != 0)
            {
                for (int i = 0; i < ethernetAddr_comboBox.Items.Count; i++)
                {
                    arr.Add(ethernetAddr_comboBox.Items[i].ToString());
                }
            }
            Properties.Settings.Default.ipAddresses = arr;

            // ComPort.Close();

            for (int i = 0; i < 500; i++)
            {
                // Thread.Sleep(1);
            }

            //сохранение настроек
            Properties.Settings.Default.Save();

            sendInqury_Timer.Stop();
            sendInqury_Timer.Close();
            // sendInqury_Timer.Dispose();

            infoChannel_Timer.Stop();
            infoChannel_Timer.Close();
            // infoChannel_Timer.Dispose();

            statusLabel_Timer.Stop();
            statusLabel_Timer.Close();
            // statusLabel_Timer.Dispose();

            updateDateTime_Timer.Stop();
            updateDateTime_Timer.Close();
            // updateDateTime_Timer.Dispose();

            EthernetTimeoutTimer.Stop();
            EthernetTimeoutTimer.Close();
            //  EthernetTimeoutTimer.Dispose();

            // ComPort.Close();
            // ComPort.Dispose();

            for (int i = 0; i < 500; i++)
            {
                // Thread.Sleep(1);
            }

            //if (sendInqury_Timer != null)
            //{
            //    try
            //    {
            //        sendInqury_Timer.Stop();
            //        sendInqury_Timer.Dispose();
            //    }
            //    catch { }
            //}
            //if (ComPort.IsOpen)
            //{
            //    try
            //    {
            //        ComPort.Close();
            //        ComPort.Dispose();
            //    }
            //    catch { }
            //}
            //if (infoChannel_Timer != null)
            //{
            //    try
            //    {
            //        infoChannel_Timer.Stop();
            //        infoChannel_Timer.Dispose();
            //    }
            //    catch { }
            //}
        }

        private delegate void ComPortDelegate(byte[] buff);

        private void ProcessComPort(byte[] buff)
        {
            try
            {
                if (infoChannel.CheckProtocol(buff))
                {
                    this.StatusLabel = "";
                    this.TimeOutInfoLabel = "";
                    infoChannel_Timer.Stop();
                    infoChannel_Timer.Start();
                    infoChannel.ProcessPackage(buff);
                }
                else if (crc_rb.CheckProtocol(buff))
                {
                    crc_rb.ProcessPackage(buff);
                }
                else if (elZIP.CheckProtocol(buff))
                {
                    elZIP.ProcessPackage(buff);
                }
                else if (modbus.CheckProtocol(buff))
                {
                    modbus.ProcessPackage(buff);
                }
                else if (indicator.CheckProtocol(buff))
                {
                    indicator.ProcessPackage(buff);
                }
                else if (indicator23.CheckProtocol(buff))
                {
                    indicator23.ProcessPackage(buff);
                }
                else if (tarifPro.CheckProtocol(buff))
                {
                    tarifPro.ProcessPackage(buff);
                }
                else if (buff.Length == 7) // определяем адрес
                {
                    if (buff[1] == 0x77 && buff[2] == 0x02 && buff[3] == 0x00)
                    {
                        int temp = (int)(buff[0]);
                        DeviceNumberTextBox = temp.ToString();
                        modbus.noAnswer_Timer.Stop();
                    }
                }
                else
                    this.StatusLabel = "Ошибка обработки данных";
            }
            catch (Exception ex)
            {
                this.StatusLabel = "Ошибка приема данных";
                //WriteLog("Ошибка в функции приема данных из порта: " + ex.Message + "/n" + ex.InnerException + "/n" + ex.Source + "/n" + ex.StackTrace + "/n" + ex.TargetSite);
            }
        }

        private void ComPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            if (ComPort.BytesToRead == 0) return;
            int bytesReceived;
            do
            {
                bytesReceived = ComPort.BytesToRead;
                int sleep = (int)((1000.0 / (ComPort.BaudRate / 8)) * 30);
                if (sleep < 20) sleep = 20;
                Thread.Sleep(sleep);
            }
            while (bytesReceived != ComPort.BytesToRead);

            byte[] buff = new byte[ComPort.BytesToRead];
            ComPort.Read(buff, 0, buff.Length);

            if (main_progressBar.InvokeRequired)
            {
                main_progressBar.BeginInvoke(new ComPortDelegate(ProcessComPort), new object[] { buff });
            }
        }


        private void TabControl_main_SelectedIndexChanged(object sender, EventArgs e)
        {
            main_progressBar.Visible = false;
            main_progressBar.Value = 0;
            communication_comboBox.Enabled = true;

            if (TabControl_main.SelectedIndex == 2) // выбрали главную страницу
            {
                if (!comPortSpeed_comboBox.Enabled)
                {
                    Protocol.TryToChangeBoudRate(ComPort, Convert.ToInt32(comPortSpeed_comboBox.Text));
                    Protocol.TryToOpenPort(ComPort, comPorts_comboBox.Text);
                    comPortSpeed_comboBox.Enabled = true;
                }

                if (configurate_tabControl.SelectedIndex == 0)
                    main_progressBar.Visible = true;

                if (configurate_tabControl.SelectedIndex == 1 && indication_tabControl.SelectedIndex == 0)
                    main_progressBar.Visible = true;
            }
            else if (TabControl_main.SelectedIndex == 3)
            {
                communication_comboBox.SelectedIndex = 0;
                communication_comboBox.Enabled = false;
                Protocol.TryToChangeBoudRate(ComPort, 9600);
                Protocol.TryToOpenPort(ComPort, comPorts_comboBox.Text);
                comPortSpeed_comboBox.Enabled = false;
            }
            else if (TabControl_main.SelectedIndex == 4) // выбрали инф. канал
            {
                communication_comboBox.SelectedIndex = 0;
                communication_comboBox.Enabled = false;
                Protocol.TryToChangeBoudRate(ComPort, 9600);
                Protocol.TryToOpenPort(ComPort, comPorts_comboBox.Text);
                comPortSpeed_comboBox.Enabled = false;
                infoChannel.ClearFields();
                infoChannel_Timer.Stop();
                infoChannel_Timer.Start();
            }
            else
            {
                if (!comPortSpeed_comboBox.Enabled)
                {
                    Protocol.TryToChangeBoudRate(ComPort, Convert.ToInt32(comPortSpeed_comboBox.Text));
                    Protocol.TryToOpenPort(ComPort, comPorts_comboBox.Text);
                    comPortSpeed_comboBox.Enabled = true;
                }
            }

            if (AutoLoopIsOn)
            {
                AutoLoopIsOn = false;
                //autoLoop_checkBox.Checked = false;
                //autoLoopPKE_checkBox.Checked = false;
                saveChangedParams_button.Enabled = false;
                params_groupBox.Visible = false;
                AnalogOutsTypeGroupBox = true;
                saveAnalogouts_button.Enabled = false;
                saveChangedParams_button.Enabled = false;
                AnalogOutsRangesGroupBox = false;
                AnalogOutsGroupBoxes = false;
                enableCorrectionMode_button.Text = "Установить связь";
                read31Params_button.Text = Globals.READ_BUTTON_TAG;
                readEventjournal_button.Text = Globals.READ_BUTTON_TAG;
                readEnergy_button.Text = Globals.READ_BUTTON_TAG;
                readAveragePKE_Button.Text = Globals.READ_BUTTON_TAG;
                readPKE_button.Text = Globals.READ_BUTTON_TAG; ;

                modbus.noAnswer_Timer.Stop();
                sendInqury_Timer.Stop();

                if (readAnalogOut_button.Text == "Стоп")
                {
                    Thread.Sleep(50);
                    ushort temp = 0;
                    byte[] bytes = BitConverter.GetBytes(temp);
                    modbus.WriteData(Modbus.EnquryType.INQ_WRITE_CURRENT_MODE_OFF, bytes);
                }
                readAnalogOut_button.Text = "Установить связь";
            }

            modbus.ClearCRCErrorsCounter();
            modbus.ClearNoAnswersCounter();
            modbus.ClearInquresCounter();
            this.AnswerTimeTextBox = "";
            this.MaxAnswerTimeTextBox = "";
            this.InquresCounterTextBox = "";
            this.CRCErrorTextBox = "";
            this.NoAnswerCounterTextBox = "";
            this.ColorStatusButton = Color.Transparent;
        }

        private void configurate_tabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            main_progressBar.Visible = false;
            main_progressBar.Value = 0;

            if (configurate_tabControl.SelectedIndex == 0)
                main_progressBar.Visible = true;

            if (configurate_tabControl.SelectedIndex == 1 && indication_tabControl.SelectedIndex == 0)
                main_progressBar.Visible = true;

            if (configurate_tabControl.SelectedIndex == 2)
                main_progressBar.Visible = true;

            if (configurate_tabControl.SelectedIndex == 3)
                main_progressBar.Visible = true;

            if (AutoLoopIsOn)
            {
                AutoLoopIsOn = false;
                saveChangedParams_button.Enabled = false;
                params_groupBox.Visible = false;
                AnalogOutsTypeGroupBox = true;
                saveAnalogouts_button.Enabled = false;
                saveChangedParams_button.Enabled = false;
                AnalogOutsRangesGroupBox = false;
                AnalogOutsGroupBoxes = false;
                enableCorrectionMode_button.Text = "Установить связь";

                modbus.noAnswer_Timer.Stop();
                sendInqury_Timer.Stop();

                if (readAnalogOut_button.Text == "Стоп")
                {
                    Thread.Sleep(50);
                    ushort temp = 0;
                    byte[] bytes = BitConverter.GetBytes(temp);
                    modbus.WriteData(Modbus.EnquryType.INQ_WRITE_CURRENT_MODE_OFF, bytes);
                }
                readAnalogOut_button.Text = "Установить связь";
            }
        }

        private void tabControl2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (AutoLoopIsOn)
            {
                readEventjournal_button.Text = Globals.READ_BUTTON_TAG;
                readEnergy_button.Text = Globals.READ_BUTTON_TAG;
                //for (int i = 0; i < 100; i++)
                //{
                AutoLoopIsOn = false;
                modbus.noAnswer_Timer.Stop();
                sendInqury_Timer.Stop();
                //    Thread.Sleep(2);
                //}
            }
        }

        private void tabControl4_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (AutoLoopIsOn)
            {
                readPKE_button.Text = Globals.READ_BUTTON_TAG;
                readAveragePKE_Button.Text = Globals.READ_BUTTON_TAG;
                //for (int i = 0; i < 100; i++)
                //{
                AutoLoopIsOn = false;
                modbus.noAnswer_Timer.Stop();
                sendInqury_Timer.Stop();
                //    Thread.Sleep(2);
                //}
            }
        }

        private void indication_tabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            main_progressBar.Visible = false;
            main_progressBar.Value = 0;

            if (indication_tabControl.SelectedIndex == 0)
                main_progressBar.Visible = true;
        }

        private void comPorts_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (TabControl_main.SelectedIndex == 3) // выбрали инф. канал
            {
                Protocol.TryToOpenPort(ComPort, comPorts_comboBox.Text);
            }
        }

        private void infoChannel_Timer_Tick(object sender, EventArgs e)
        {
            infoChannel.ClearFields();
            this.TimeOutInfoLabel = "Нет информации!";
            infoChannel_Timer.Stop();
        }

        private void ClearStatusLabel(object sender, EventArgs e)
        {
            this.StatusLabel = "";
            this.SaveStatusLabel = "";
            this.AnalogCorrectLabel = "";
        }

        private void status_label_TextChanged(object sender, EventArgs e)
        {
            if (status_label.Text != "")
            {
                statusLabel_Timer.Stop();
                statusLabel_Timer.Start();
            }
            else
                statusLabel_Timer.Stop();
        }

        private void deviceNumber_textBox_TextChanged(object sender, EventArgs e)
        {
            long tempDeviceNumber;
            if (DeviceNumberTextBox == "") return;
            try
            {
                tempDeviceNumber = Convert.ToInt64(DeviceNumberTextBox);
                if (tempDeviceNumber < 1) DeviceNumberTextBox = "1";
                else if (tempDeviceNumber > 247) DeviceNumberTextBox = "247";
            }
            catch (Exception ex)
            {
                WriteLog("Ошибка смены логического номера, устанавили '1': " + ex.Message);
                DeviceNumberTextBox = "1";
            }
        }

        private void comPortSpeed_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Protocol.TryToChangeBoudRate(ComPort, Convert.ToInt32(comPortSpeed_comboBox.Text));
        }

        private void AutoSendInqury(object sender, EventArgs e)
        {
            sendInqury_Timer.Stop();

            if (modbus.StoredEnquryType >= Modbus.EnquryType.INQ_READ_TURN_ON_OFF_DT_JOURNAL
                && modbus.StoredEnquryType <= Modbus.EnquryType.INQ_READ_dUMINUS_JOURNAL)
            {
                modbus.ReadFile(modbus.StoredEnquryType, ++modbus.StoredRecordNumber);
            }
            else if (modbus.StoredEnquryType == Modbus.EnquryType.INQ_READ_CRCRB_AUTOLOOP)
            {
                crc_rb.ReadData();
            }
            else if (modbus.StoredEnquryType == Modbus.EnquryType.INQ_READ_ELZIP_AUTOLOOP)
            {
                elZIP.ReadData();
            }
            else if (modbus.StoredEnquryType == Modbus.EnquryType.INQ_WRITE_TARIF_PRO)
            {
                tarifPro.WriteData();
            }
            else if (modbus.StoredEnquryType == Modbus.EnquryType.INQ_READ_TARIF_PRO)
            {
                tarifPro.ReadData();
            }
            else
                modbus.ReadData(modbus.StoredEnquryType);
        }

        private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            modbus.ReadData(modbus.StoredEnquryType);
        }

        public void StartAutoInquryTimer(Modbus.EnquryType enquryType, double interval)
        {
            modbus.StoredEnquryType = enquryType;
            sendInqury_Timer.Interval = interval;
            sendInqury_Timer.Start();
        }

        private void pollPeriod_textBox_TextChanged(object sender, EventArgs e)
        {
            //if (!checkBox_EnergyChoose.Enabled) return;
            try
            {
                int temp = Convert.ToInt32(pollPeriod_textBox.Text);
                errorProvider.SetError(pollPeriod_textBox, "");
                if (temp < 100) errorProvider.SetError(pollPeriod_textBox, "Период меньше 100");
                else if (temp > 10000) errorProvider.SetError(pollPeriod_textBox, "Период больше 10000");
            }
            catch
            {
                errorProvider.SetError(pollPeriod_textBox, "Некоректно введен период");
            }
        }

        private void clearCouters_button_Click(object sender, EventArgs e)
        {
            modbus.ClearCRCErrorsCounter();
            modbus.ClearNoAnswersCounter();
        }

        private void relayN_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (relayN_comboBox.SelectedIndex == 0) saveRelayConfig_button.Text = "Запись (Реле №1)";
            else if (relayN_comboBox.SelectedIndex == 1) saveRelayConfig_button.Text = "Запись (Реле №2)";
            else if (relayN_comboBox.SelectedIndex == 2) saveRelayConfig_button.Text = "Запись (Реле №3)";

            RelayNumberComboBox = relayN_comboBox.SelectedIndex;
            RelayModeComboBox = modbus.relaySettings[relayN_comboBox.SelectedIndex].mode;
            RelayUstavkaTextBox = modbus.relaySettings[relayN_comboBox.SelectedIndex].ustavka.ToString();
            RelayPingTextBox = modbus.relaySettings[relayN_comboBox.SelectedIndex].ping.ToString();
            RelayParanComboBox = (int)modbus.relaySettings[relayN_comboBox.SelectedIndex].param;
            RelayGisterezisTextBox = modbus.relaySettings[relayN_comboBox.SelectedIndex].gisterezis.ToString("N4");
        }

        private void relayWorkMode_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            modbus.relaySettings[relayN_comboBox.SelectedIndex].mode = (ushort)relayWorkMode_comboBox.SelectedIndex;
        }

        private void relayParam_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            modbus.relaySettings[relayN_comboBox.SelectedIndex].param = (ushort)relayParam_comboBox.SelectedIndex;

            int ustavka = CheckLimit(modbus.relaySettings[relayN_comboBox.SelectedIndex].param, modbus.relaySettings[relayN_comboBox.SelectedIndex].ustavka);
            if (ustavka == -1) ustavka = modbus.relaySettings[relayN_comboBox.SelectedIndex].ustavka;
            relayUstavka_textBox.Text = ustavka.ToString();
        }

        private void relayUstavka_textBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                short temp = Convert.ToInt16(relayUstavka_textBox.Text);
                modbus.relaySettings[relayN_comboBox.SelectedIndex].ustavka = temp;

                int ustavka = CheckLimit(modbus.relaySettings[relayN_comboBox.SelectedIndex].param, modbus.relaySettings[relayN_comboBox.SelectedIndex].ustavka);
                if (ustavka == -1) ustavka = modbus.relaySettings[relayN_comboBox.SelectedIndex].ustavka;
                relayUstavka_textBox.Text = ustavka.ToString();

                errorProvider.SetError(relayUstavka_textBox, "");
            }
            catch
            {
                errorProvider.SetError(relayUstavka_textBox, "Некоректно введено значение");
            }
        }

        private void relayPing_textBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                ushort temp = Convert.ToUInt16(relayPing_textBox.Text);
                modbus.relaySettings[relayN_comboBox.SelectedIndex].ping = temp;
                errorProvider.SetError(relayPing_textBox, "");
            }
            catch
            {
                errorProvider.SetError(relayPing_textBox, "Некоректно введено значение");
            }
        }

        private void relayGist_textBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                float temp = Convert.ToSingle(relayGist_textBox.Text);
                modbus.relaySettings[relayN_comboBox.SelectedIndex].gisterezis = temp;
                errorProvider.SetError(relayGist_textBox, "");
            }
            catch
            {
                errorProvider.SetError(relayGist_textBox, "Некоректно введено значение");
            }
        }

        private void configUart2_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ModeBComboBox == 1)
            {
                SpeedBComboBoxEnabled = true;
                saveSpeedB_button.Enabled = true;
            }
            else
            {
                SpeedBComboBoxEnabled = false;
                saveSpeedB_button.Enabled = false;
            }
        }

        private void read31Params_button_Click(object sender, EventArgs e)
        {
            if (read31Params_button.Text == Globals.READ_BUTTON_TAG)
            {
                read31Params_button.Text = "Стоп";
                autoLoop_checkBox_CheckedChanged(sender, e);
                modbus.ReadData(Modbus.EnquryType.INQ_READ_COEFS_THEN_PARAMS);
            }
            else
            {
                read31Params_button.Text = Globals.READ_BUTTON_TAG;
                // autoLoop_checkBox.Checked = false;
                AutoLoopIsOn = false;
                modbus.noAnswer_Timer.Stop();
                sendInqury_Timer.Stop();
            }
        }

        private void readMainConfigParams_button_Click(object sender, EventArgs e)
        {
            MainProgressBar = 0;
            modbus.ReadData(Modbus.EnquryType.INQ_READ_MAIN_CONFIG_PARAMS);
        }

        private void readIndicationParams_button_Click(object sender, EventArgs e)
        {
            if (indication_tabControl.SelectedIndex == 0)
            {
                param1MaxUstavka_textBox.Enabled = false;
                param2MaxUstavka_textBox.Enabled = false;
                param3MaxUstavka_textBox.Enabled = false;
                param4MaxUstavka_textBox.Enabled = false;
                param5MaxUstavka_textBox.Enabled = false;
                param6MaxUstavka_textBox.Enabled = false;

                param1MinUstavka_textBox.Enabled = false;
                param2MinUstavka_textBox.Enabled = false;
                param3MinUstavka_textBox.Enabled = false;
                param4MinUstavka_textBox.Enabled = false;
                param5MinUstavka_textBox.Enabled = false;
                param6MinUstavka_textBox.Enabled = false;

                row1Param_comboBox.Enabled = false;
                row2Param_comboBox.Enabled = false;
                row3Param_comboBox.Enabled = false;
                row4Param_comboBox.Enabled = false;
                row5Param_comboBox.Enabled = false;
                row6Param_comboBox.Enabled = false;

                saveRowsInd_button.Enabled = false;
                writeUstavki_button.Enabled = false;

                modbus.ReadData(Modbus.EnquryType.INQ_READ_INDICATION_INFO);

                MainProgressBar = 0;
            }
            if (indication_tabControl.SelectedIndex == 1)
            {
                modbus.ReadData(Modbus.EnquryType.INQ_READ_ENERGY_ROWS);
            }
        }


        private void row1Param_comboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            row1Param_comboBox.Enabled = true;
            row2Param_comboBox.Enabled = true;
            row3Param_comboBox.Enabled = true;

            //param4MaxUstavka_textBox.Enabled = false;
            //param5MaxUstavka_textBox.Enabled = false;
            //param6MaxUstavka_textBox.Enabled = false;

            //param4MinUstavka_textBox.Enabled = false;
            //param5MinUstavka_textBox.Enabled = false;
            //param6MinUstavka_textBox.Enabled = false;

            //writeUstavki_button.Enabled = false;
            saveRowsInd_button.Enabled = true;
        }

        private void row4Param_comboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            row4Param_comboBox.Enabled = true;
            row5Param_comboBox.Enabled = true;
            row6Param_comboBox.Enabled = true;
        }

        private void rowParam_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((ComboBox)sender == row1Param_comboBox)
            {
                SetLimit(row1Param_comboBox.SelectedIndex, param1MaxUstavka_textBox, param1MinUstavka_textBox);
            }
            else if ((ComboBox)sender == row2Param_comboBox)
            {
                SetLimit(row2Param_comboBox.SelectedIndex, param2MaxUstavka_textBox, param2MinUstavka_textBox);
            }
            else if ((ComboBox)sender == row3Param_comboBox)
            {
                SetLimit(row3Param_comboBox.SelectedIndex, param3MaxUstavka_textBox, param3MinUstavka_textBox);
            }
            else if ((ComboBox)sender == row4Param_comboBox)
            {
                SetLimit(row4Param_comboBox.SelectedIndex, param4MaxUstavka_textBox, param4MinUstavka_textBox);
            }
            else if ((ComboBox)sender == row5Param_comboBox)
            {
                SetLimit(row5Param_comboBox.SelectedIndex, param5MaxUstavka_textBox, param5MinUstavka_textBox);
            }
            else if ((ComboBox)sender == row6Param_comboBox)
            {
                SetLimit(row6Param_comboBox.SelectedIndex, param6MaxUstavka_textBox, param6MinUstavka_textBox);
            }
            else if ((ComboBox)sender == indicatorRow1_comboBox)
            {
                SetLimit(indicatorRow1_comboBox.SelectedIndex, indicator1MaxUstavka_textBox, indicator1MinUstavka_textBox);
            }
            else if ((ComboBox)sender == indicatorRow2_comboBox)
            {
                SetLimit(indicatorRow2_comboBox.SelectedIndex, indicator2MaxUstavka_textBox, indicator2MinUstavka_textBox);
            }
            else if ((ComboBox)sender == indicatorRow3_comboBox)
            {
                SetLimit(indicatorRow3_comboBox.SelectedIndex, indicator3MaxUstavka_textBox, indicator3MinUstavka_textBox);
            }
            //param1MaxUstavka_textBox.Enabled = false;
            //param2MaxUstavka_textBox.Enabled = false;
            //param3MaxUstavka_textBox.Enabled = false;
            //param4MaxUstavka_textBox.Enabled = false;
            //param5MaxUstavka_textBox.Enabled = false;
            //param6MaxUstavka_textBox.Enabled = false;

            //param1MinUstavka_textBox.Enabled = false;
            //param2MinUstavka_textBox.Enabled = false;
            //param3MinUstavka_textBox.Enabled = false;
            //param4MinUstavka_textBox.Enabled = false;
            //param5MinUstavka_textBox.Enabled = false;
            //param6MinUstavka_textBox.Enabled = false;

            //writeUstavki_button.Enabled = false;
        }



        private void param1MaxUstavka_textBox_TextChanged(object sender, EventArgs e)
        {
            param1MaxUstavka_textBox.Enabled = true;
            param2MaxUstavka_textBox.Enabled = true;
            param3MaxUstavka_textBox.Enabled = true;

            param1MinUstavka_textBox.Enabled = true;
            param2MinUstavka_textBox.Enabled = true;
            param3MinUstavka_textBox.Enabled = true;

            ProcessUstavkiTextBox(param1MaxUstavka_textBox, 1);
        }

        private void param2MaxUstavka_textBox_TextChanged(object sender, EventArgs e)
        {
            ProcessUstavkiTextBox(param2MaxUstavka_textBox, 2);
        }

        private void param3MaxUstavka_textBox_TextChanged(object sender, EventArgs e)
        {
            ProcessUstavkiTextBox(param3MaxUstavka_textBox, 3);
        }

        private void param4MaxUstavka_textBox_TextChanged(object sender, EventArgs e)
        {
            param4MaxUstavka_textBox.Enabled = true;
            param5MaxUstavka_textBox.Enabled = true;
            param6MaxUstavka_textBox.Enabled = true;

            param4MinUstavka_textBox.Enabled = true;
            param5MinUstavka_textBox.Enabled = true;
            param6MinUstavka_textBox.Enabled = true;

            ProcessUstavkiTextBox(param4MaxUstavka_textBox, 4);
        }

        private void param5MaxUstavka_textBox_TextChanged(object sender, EventArgs e)
        {
            ProcessUstavkiTextBox(param5MaxUstavka_textBox, 5);
        }

        private void param6MaxUstavka_textBox_TextChanged(object sender, EventArgs e)
        {
            ProcessUstavkiTextBox(param6MaxUstavka_textBox, 6);
        }

        private void param1MinUstavka_textBox_TextChanged(object sender, EventArgs e)
        {
            ProcessUstavkiTextBox(param1MinUstavka_textBox, 1);
        }

        private void param2MinUstavka_textBox_TextChanged(object sender, EventArgs e)
        {
            ProcessUstavkiTextBox(param2MinUstavka_textBox, 2);
        }

        private void param3MinUstavka_textBox_TextChanged(object sender, EventArgs e)
        {
            ProcessUstavkiTextBox(param3MinUstavka_textBox, 3);
        }

        private void param4MinUstavka_textBox_TextChanged(object sender, EventArgs e)
        {
            ProcessUstavkiTextBox(param4MinUstavka_textBox, 4);
        }

        private void param5MinUstavka_textBox_TextChanged(object sender, EventArgs e)
        {
            ProcessUstavkiTextBox(param5MinUstavka_textBox, 5);
        }

        private void param6MinUstavka_textBox_TextChanged(object sender, EventArgs e)
        {
            ProcessUstavkiTextBox(param6MinUstavka_textBox, 6);
        }

        private void ProcessUstavkiTextBox(TextBox textBox, int row)
        {
            try
            {
                int temp = Convert.ToInt32(textBox.Text);
                errorProvider.SetError(textBox, "");

                ComboBox indicationRow = row1Param_comboBox;
                if (row == 2) indicationRow = row2Param_comboBox;
                else if (row == 3) indicationRow = row3Param_comboBox;
                else if (row == 4) indicationRow = row4Param_comboBox;
                else if (row == 5) indicationRow = row5Param_comboBox;
                else if (row == 6) indicationRow = row6Param_comboBox;

                int result = CheckLimit(indicationRow.SelectedIndex, temp);
                if (result != -1) textBox.Text = result.ToString();

                if (errorProvider.GetError(param1MaxUstavka_textBox) == ""
                    && errorProvider.GetError(param2MaxUstavka_textBox) == ""
                    && errorProvider.GetError(param3MaxUstavka_textBox) == ""
                    && errorProvider.GetError(param4MaxUstavka_textBox) == ""
                    && errorProvider.GetError(param5MaxUstavka_textBox) == ""
                    && errorProvider.GetError(param6MaxUstavka_textBox) == ""
                    && errorProvider.GetError(param1MinUstavka_textBox) == ""
                    && errorProvider.GetError(param2MinUstavka_textBox) == ""
                    && errorProvider.GetError(param3MinUstavka_textBox) == ""
                    && errorProvider.GetError(param4MinUstavka_textBox) == ""
                    && errorProvider.GetError(param5MinUstavka_textBox) == ""
                    && errorProvider.GetError(param6MinUstavka_textBox) == "")
                {
                    writeUstavki_button.Enabled = true;
                }
            }
            catch
            {
                errorProvider.SetError(textBox, "Некоректно введено значение");
                writeUstavki_button.Enabled = false;
            }
        }



        private int CheckLimit(int param, int limit)
        {
            if (param == (int)PARAMS.Ua || param == (int)PARAMS.Ub || param == (int)PARAMS.Uc || param == (int)PARAMS.Umd || param == (int)PARAMS.U0
                    || param == (int)PARAMS.Uab || param == (int)PARAMS.Ubc || param == (int)PARAMS.Uca || param == (int)PARAMS.Ulmd)
            {
                if (limit < 0) return 0;
                if (limit > 130) return 130;
                //if (lowLimit >= highLimit) return false;
                // return true;
            }
            else if (param == (int)PARAMS.Sa || param == (int)PARAMS.Sb || param == (int)PARAMS.Sc || param == (int)PARAMS.S)
            {
                if (limit < 0) return 0;
                if (limit > 120) return 120;
                //if (lowLimit >= highLimit) return false;
                // return true;
            }
            else if (param == (int)PARAMS.Ia || param == (int)PARAMS.Ib || param == (int)PARAMS.Ic || param == (int)PARAMS.Imd || param == (int)PARAMS.I0)
            {
                if (limit < 0) return 0;
                if (limit > 120) return 120;
                //  if (lowLimit >= highLimit) return false;
                //  return true;
            }
            else if (param == (int)PARAMS.Pa || param == (int)PARAMS.Pb || param == (int)PARAMS.Pc || param == (int)PARAMS.P
                     || param == (int)PARAMS.Qa || param == (int)PARAMS.Qb || param == (int)PARAMS.Qc || param == (int)PARAMS.Q)
            {
                if (limit < -120) return -120;
                if (limit > 120) return 120;
                //   if (lowLimit >= highLimit) return false;
                //  return true;
            }
            else if (param == (int)PARAMS.KPa || param == (int)PARAMS.KPb || param == (int)PARAMS.KPc
                || param == (int)PARAMS.KP || param == (int)PARAMS.F)
            {
                if (limit < 0) return 0;
                if (limit > 100) return 100;
                //  if (lowLimit >= highLimit) return false;
                //   return true;
            }
            return -1;
        }

        private void SetLimit(int param, TextBox max, TextBox min)
        {
            if (param == -1) return;
            if (param == (int)PARAMS.Ua || param == (int)PARAMS.Ub || param == (int)PARAMS.Uc || param == (int)PARAMS.Umd || param == (int)PARAMS.U0
                    || param == (int)PARAMS.Uab || param == (int)PARAMS.Ubc || param == (int)PARAMS.Uca || param == (int)PARAMS.Ulmd)
            {
                max.Text = "130";
                min.Text = "0";
            }
            else if (param == (int)PARAMS.Sa || param == (int)PARAMS.Sb || param == (int)PARAMS.Sc || param == (int)PARAMS.S)
            {
                max.Text = "120";
                min.Text = "0";
            }
            else if (param == (int)PARAMS.Ia || param == (int)PARAMS.Ib || param == (int)PARAMS.Ic || param == (int)PARAMS.Imd || param == (int)PARAMS.I0)
            {
                max.Text = "120";
                min.Text = "0";
            }
            else if (param == (int)PARAMS.Pa || param == (int)PARAMS.Pb || param == (int)PARAMS.Pc || param == (int)PARAMS.P
                     || param == (int)PARAMS.Qa || param == (int)PARAMS.Qb || param == (int)PARAMS.Qc || param == (int)PARAMS.Q)
            {
                max.Text = "120";
                min.Text = "-120";
            }
            else if (param == (int)PARAMS.KPa || param == (int)PARAMS.KPb || param == (int)PARAMS.KPc
                || param == (int)PARAMS.KP || param == (int)PARAMS.F)
            {
                max.Text = "100";
                min.Text = "0";
            }
            else
            {
                max.Text = "100";
                min.Text = "0";
            }
        }

        private void readCoefs_button_Click(object sender, EventArgs e)
        {
            main_progressBar.Value = 0;
            modbus.ReadData(Modbus.EnquryType.INQ_READ_COEFS_AND_MEASURE_PARAMS);
        }

        private void measureScheme_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (measureScheme_comboBox.SelectedIndex == 1)
            {
                currentIb_comboBox.SelectedIndex = 0;
                currentIb_comboBox.Enabled = false;
            }
            else
            {
                currentIb_comboBox.Enabled = true;
            }
        }

        private void enableCorrectionMode_button_Click(object sender, EventArgs e)
        {
            if (!AutoLoopIsOn)
            {
                main_progressBar.Value = 0;
                AutoLoopIsOn = true;
                UabChange_label.Visible = false;
                UbcChange_label.Visible = false;
                UcaChange_label.Visible = false;
                Ua_Uab_label.Text = "Ua =";
                Ub_Ubc_label.Text = "Ub =";
                Uc_Uca_label.Text = "Uc =";
                enableCorrectionMode_button.Text = "Стоп";
                modbus.ReadData(Modbus.EnquryType.INQ_READ_COEFS_AND_MEASURE_PARAMS);
            }
            else
            {
                AutoLoopIsOn = false;
                saveChangedParams_button.Enabled = false;
                params_groupBox.Visible = false;
                enableCorrectionMode_button.Text = "Установить связь";
                //for (int i = 0; i < 100; i++)
                //{
                sendInqury_Timer.Stop();
                modbus.noAnswer_Timer.Stop();
                //    Thread.Sleep(2);
                //}
            }
        }

        private void chooseFaseVoltage_button_Click(object sender, EventArgs e)
        {
            UabChange_label.Visible = false;
            UbcChange_label.Visible = false;
            UcaChange_label.Visible = false;
            Ua_Uab_label.Text = "Ua =";
            Ub_Ubc_label.Text = "Ub =";
            Uc_Uca_label.Text = "Uc =";
        }

        private void chooseLinVoltage_button_Click(object sender, EventArgs e)
        {
            UabChange_label.Visible = true;
            UbcChange_label.Visible = true;
            UcaChange_label.Visible = true;
            Ua_Uab_label.Text = "Uab =";
            Ub_Ubc_label.Text = "Ubc =";
            Uc_Uca_label.Text = "Uca =";
        }



        private void readImpulseOuts_button_Click(object sender, EventArgs e)
        {
            modbus.ReadData(Modbus.EnquryType.INQ_READ_IMPULSE_OUTS);
        }

        private void readImpWtH_button_Click(object sender, EventArgs e)
        {
            modbus.ReadData(Modbus.EnquryType.INQ_READ_IMP_WTH);
        }

        private void readDateTime_button_Click(object sender, EventArgs e)
        {
            modbus.ReadData(Modbus.EnquryType.INQ_READ_DATE_TIME);
        }

        private void hour_numericUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (hour_numericUpDown.Value >= 24) hour_numericUpDown.Value = 0;
            else if (hour_numericUpDown.Value < 0) hour_numericUpDown.Value = 23;
        }

        private void minute_numericUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (minute_numericUpDown.Value >= 60) minute_numericUpDown.Value = 0;
            else if (minute_numericUpDown.Value < 0) minute_numericUpDown.Value = 59;
        }

        private void seconds_numericUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (seconds_numericUpDown.Value >= 60) seconds_numericUpDown.Value = 0;
            else if (seconds_numericUpDown.Value < 0) seconds_numericUpDown.Value = 59;
        }

        private void currentDT_checkBox_CheckedChanged(object sender, EventArgs e)
        {
            if (currentDT_checkBox.Checked)
            {
                updateDateTime_Timer.Start();
            }
            else
            {
                updateDateTime_Timer.Stop();
            }
        }

        private void UpdateDateTime(object sender, EventArgs e)
        {
            DateTime dtNow = DateTime.Now;
            this.HourNumericUpDownValue = dtNow.Hour;
            this.MinuteNumericUpDownValue = dtNow.Minute;
            this.SecondsNumericUpDownValue = dtNow.Second;

            this.SetCalendarValue = dtNow;
        }

        private void EthernetTimeout(object sender, EventArgs e)
        {
            if (tcpClient != null && tcpClient.Available == 0) return;
            NetworkStream tcpStream = tcpClient.GetStream();
            byte[] buffer = new byte[tcpClient.Available];
            int bytesRead = tcpStream.Read(buffer, 0, tcpClient.Available);

            int port = ((IPEndPoint)tcpClient.Client.RemoteEndPoint).Port;
            if (port == Modbus.Port && buffer.Length > 6)
            {
                byte[] buff = new byte[buffer.Length - 6];
                for (int i = 0; i < buffer.Length - 6; i++)
                {
                    buff[i] = buffer[6 + i];
                }
                buffer = buff;

                if (modbus.CheckProtocol(buffer))
                {
                    modbus.ProcessPackage(buffer);
                }
            }
            else if (port == CRC_RB.Port)
            {
                if (crc_rb.CheckProtocol(buffer))
                {
                    crc_rb.ProcessPackage(buffer);
                }
            }
        }

        private void saveNetAddr_button_Click(object sender, EventArgs e)
        {
            try
            {
                ushort temp = Convert.ToUInt16(networkAddress_textBox.Text);
                byte[] data = BitConverter.GetBytes(temp);
                modbus.WriteData(Modbus.EnquryType.INQ_SAVE_NET_ADDR, data);
                Thread.Sleep(100);
                DeviceNumberTextBox = networkAddress_textBox.Text;
            }
            catch
            {
                MessageBox.Show(Globals.INCORRECT_DATA_MESSAGE);
            }
        }

        private void saveSpeedA_button_Click(object sender, EventArgs e)
        {
            try
            {
                if (speed1_comboBox.SelectedIndex == -1) throw new IndexOutOfRangeException();
                byte[] data = BitConverter.GetBytes((ushort)speed1_comboBox.SelectedIndex);
                modbus.WriteData(Modbus.EnquryType.INQ_SAVE_SPEED_A, data);
                // Thread.Sleep(500);
                //ComPortSpeedComboBox = SpeedAComboBox;
            }
            catch
            {
                MessageBox.Show(Globals.INCORRECT_DATA_MESSAGE);
            }
        }

        private void saveSpeedB_button_Click(object sender, EventArgs e)
        {
            try
            {
                if (speed2_comboBox.SelectedIndex == -1) throw new IndexOutOfRangeException();
                byte[] data = BitConverter.GetBytes((ushort)speed2_comboBox.SelectedIndex);
                modbus.WriteData(Modbus.EnquryType.INQ_SAVE_SPEED_B, data);
            }
            catch
            {
                MessageBox.Show(Globals.INCORRECT_DATA_MESSAGE);
            }
        }

        private void saveConfigB_button_Click(object sender, EventArgs e)
        {
            try
            {
                if (configUart2_comboBox.SelectedIndex == -1) throw new IndexOutOfRangeException();
                byte[] data = BitConverter.GetBytes((ushort)configUart2_comboBox.SelectedIndex);
                modbus.WriteData(Modbus.EnquryType.INQ_SAVE_CONFIG_B, data);
            }
            catch
            {
                MessageBox.Show(Globals.INCORRECT_DATA_MESSAGE);
            }
        }

        private void saveCuttOffpercent_button_Click(object sender, EventArgs e)
        {
            try
            {
                byte[] data = BitConverter.GetBytes((ushort)noisePercent_trackBar.Value);
                modbus.WriteData(Modbus.EnquryType.INQ_SAVE_CUTTOFF_PERCENT, data);
            }
            catch
            {
                MessageBox.Show(Globals.INCORRECT_DATA_MESSAGE);
            }
        }

        private void saveFactoryNumber_button_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    if (factoryNumber_textBox.Text.Length != 6) throw new FieldAccessException();

            //    ushort year = Convert.ToUInt16(factoryNumber_textBox.Text.Substring(0, 2));
            //    ushort factoryNumber = Convert.ToUInt16(factoryNumber_textBox.Text.Substring(3, 3));
            //    byte[] dataFactoryNum = BitConverter.GetBytes(factoryNumber);
            //    byte[] dataYear = BitConverter.GetBytes(year);
            //    byte[] data = new byte[4];
            //    dataFactoryNum.CopyTo(data, 2);
            //    dataYear.CopyTo(data, 0);

            //    modbus.WriteData(Modbus.EnquryType.INQ_SAVE_YEAR_AND_FACTORYNUM, data);
            //}
            //catch
            //{
            //    MessageBox.Show(Globals.IncorrectDataMessage);
            //}
        }

        private void saveModbusConfig_button_Click(object sender, EventArgs e)
        {
            try
            {
                if (modbusBytes_comboBox.SelectedIndex == -1) throw new IndexOutOfRangeException();
                byte[] data = BitConverter.GetBytes((ushort)modbusBytes_comboBox.SelectedIndex);
                modbus.WriteData(Modbus.EnquryType.INQ_SAVE_MODBUS_CONFIG, data);
            }
            catch
            {
                MessageBox.Show(Globals.INCORRECT_DATA_MESSAGE);
            }
        }

        private void saveRelayConfig_button_Click(object sender, EventArgs e)
        {
            try
            {
                MainProgressBar = 0;

                byte[] mode = BitConverter.GetBytes((ushort)(modbus.relaySettings[relayN_comboBox.SelectedIndex].mode));
                byte[] ustavka = BitConverter.GetBytes((short)(modbus.relaySettings[relayN_comboBox.SelectedIndex].ustavka));
                byte[] ping = BitConverter.GetBytes((ushort)(modbus.relaySettings[relayN_comboBox.SelectedIndex].ping));
                byte[] param = BitConverter.GetBytes((ushort)(modbus.relaySettings[relayN_comboBox.SelectedIndex].param));

                byte[] data = new byte[8];
                mode.CopyTo(data, 6);
                param.CopyTo(data, 4);
                ustavka.CopyTo(data, 2);
                ping.CopyTo(data, 0);

                if (relayN_comboBox.SelectedIndex == 0) modbus.WriteData(Modbus.EnquryType.INQ_SAVE_RELAY_1_CONFIG, data);
                else if (relayN_comboBox.SelectedIndex == 1) modbus.WriteData(Modbus.EnquryType.INQ_SAVE_RELAY_2_CONFIG, data);
                else if (relayN_comboBox.SelectedIndex == 2) modbus.WriteData(Modbus.EnquryType.INQ_SAVE_RELAY_3_CONFIG, data);
            }
            catch
            {
                MessageBox.Show(Globals.INCORRECT_DATA_MESSAGE);
            }
        }

        private void saveNumOfRows_button_Click(object sender, EventArgs e)
        {
            try
            {
                if (numberOfRows_comboBox.SelectedIndex == -1) throw new IndexOutOfRangeException();
                byte[] data = BitConverter.GetBytes((ushort)numberOfRows_comboBox.SelectedIndex);
                modbus.WriteData(Modbus.EnquryType.INQ_SAVE_NUMBER_OF_ROWS, data);

                row1Param_comboBox.Enabled = false;
                row2Param_comboBox.Enabled = false;
                row3Param_comboBox.Enabled = false;

                row4Param_comboBox.Enabled = false;
                row5Param_comboBox.Enabled = false;
                row6Param_comboBox.Enabled = false;

                param4MaxUstavka_textBox.Enabled = false;
                param5MaxUstavka_textBox.Enabled = false;
                param6MaxUstavka_textBox.Enabled = false;

                param4MinUstavka_textBox.Enabled = false;
                param5MinUstavka_textBox.Enabled = false;
                param6MinUstavka_textBox.Enabled = false;

                writeUstavki_button.Enabled = false;
                saveRowsInd_button.Enabled = false;
            }
            catch
            {
                MessageBox.Show(Globals.INCORRECT_DATA_MESSAGE);
            }
        }

        private void saveDisplayLight_button_Click(object sender, EventArgs e)
        {
            try
            {
                if (displayLight_comboBox.SelectedIndex == -1) throw new IndexOutOfRangeException();
                byte[] data = BitConverter.GetBytes((ushort)displayLight_comboBox.SelectedIndex);
                modbus.WriteData(Modbus.EnquryType.INQ_SAVE_DISPLAY_LIGHT, data);
            }
            catch
            {
                MessageBox.Show(Globals.INCORRECT_DATA_MESSAGE);
            }
        }

        private void saveRowsInd_button_Click(object sender, EventArgs e)
        {
            try
            {
                if (row1Param_comboBox.SelectedIndex == -1
                    || row2Param_comboBox.SelectedIndex == -1
                    || row3Param_comboBox.SelectedIndex == -1)
                {
                    throw new IndexOutOfRangeException();
                }

                if (row4Param_comboBox.Enabled)
                {
                    if (row4Param_comboBox.SelectedIndex == -1
                    || row5Param_comboBox.SelectedIndex == -1
                    || row6Param_comboBox.SelectedIndex == -1)
                    {
                        throw new IndexOutOfRangeException();
                    }
                }

                byte[] data;

                byte[] row1 = BitConverter.GetBytes(GetRowParamFromComboBox(row1Param_comboBox.SelectedIndex));
                byte[] row2 = BitConverter.GetBytes(GetRowParamFromComboBox(row2Param_comboBox.SelectedIndex));
                byte[] row3 = BitConverter.GetBytes(GetRowParamFromComboBox(row3Param_comboBox.SelectedIndex));

                byte[] row4;
                byte[] row5;
                byte[] row6;
                if (row4Param_comboBox.Enabled)
                {
                    data = new byte[18];

                    row4 = BitConverter.GetBytes(GetRowParamFromComboBox(row4Param_comboBox.SelectedIndex));
                    row5 = BitConverter.GetBytes(GetRowParamFromComboBox(row5Param_comboBox.SelectedIndex));
                    row6 = BitConverter.GetBytes(GetRowParamFromComboBox(row6Param_comboBox.SelectedIndex));

                    row1.CopyTo(data, 16);
                    row2.CopyTo(data, 14);
                    row3.CopyTo(data, 12);
                    row4.CopyTo(data, 4);
                    row5.CopyTo(data, 2);
                    row6.CopyTo(data, 0);
                }
                else
                {
                    data = new byte[6];
                    row1.CopyTo(data, 4);
                    row2.CopyTo(data, 2);
                    row3.CopyTo(data, 0);
                }

                modbus.WriteData(Modbus.EnquryType.INQ_SAVE_INDICATION_ROWS, data);
            }
            catch
            {
                MessageBox.Show(Globals.INCORRECT_DATA_MESSAGE);
            }
        }

        private ushort GetRowParamFromComboBox(int selectedIndex)
        {
            if (selectedIndex > (int)PARAMS.dUminus) selectedIndex += 10;
            return (ushort)selectedIndex;
        }

        private void writeUstavki_button_Click(object sender, EventArgs e)
        {
            try
            {
                if (param1MaxUstavka_textBox.Text == ""
                    || param2MaxUstavka_textBox.Text == ""
                    || param3MaxUstavka_textBox.Text == ""
                    || param1MinUstavka_textBox.Text == ""
                    || param2MinUstavka_textBox.Text == ""
                    || param3MinUstavka_textBox.Text == "")
                {
                    throw new IndexOutOfRangeException();
                }

                if (param4MaxUstavka_textBox.Enabled)
                {
                    if (param4MaxUstavka_textBox.Text == ""
                    || param5MaxUstavka_textBox.Text == ""
                    || param6MaxUstavka_textBox.Text == ""
                    || param4MinUstavka_textBox.Text == ""
                    || param5MinUstavka_textBox.Text == ""
                    || param6MinUstavka_textBox.Text == "")
                    {
                        throw new IndexOutOfRangeException();
                    }
                }

                byte[] data;

                short temp = Convert.ToInt16(param1MaxUstavka_textBox.Text);
                byte[] row1max = BitConverter.GetBytes(temp);
                temp = Convert.ToInt16(param1MinUstavka_textBox.Text);
                byte[] row1min = BitConverter.GetBytes(temp);

                temp = Convert.ToInt16(param2MaxUstavka_textBox.Text);
                byte[] row2max = BitConverter.GetBytes(temp);
                temp = Convert.ToInt16(param2MinUstavka_textBox.Text);
                byte[] row2min = BitConverter.GetBytes(temp);

                temp = Convert.ToInt16(param3MaxUstavka_textBox.Text);
                byte[] row3max = BitConverter.GetBytes(temp);
                temp = Convert.ToInt16(param3MinUstavka_textBox.Text);
                byte[] row3min = BitConverter.GetBytes(temp);

                byte[] row4max;
                byte[] row4min;
                byte[] row5max;
                byte[] row5min;
                byte[] row6max;
                byte[] row6min;

                if (param4MaxUstavka_textBox.Enabled)
                {
                    data = new byte[24];

                    temp = Convert.ToInt16(param4MaxUstavka_textBox.Text);
                    row4max = BitConverter.GetBytes(temp);
                    temp = Convert.ToInt16(param4MinUstavka_textBox.Text);
                    row4min = BitConverter.GetBytes(temp);

                    temp = Convert.ToInt16(param5MaxUstavka_textBox.Text);
                    row5max = BitConverter.GetBytes(temp);
                    temp = Convert.ToInt16(param5MinUstavka_textBox.Text);
                    row5min = BitConverter.GetBytes(temp);

                    temp = Convert.ToInt16(param6MaxUstavka_textBox.Text);
                    row6max = BitConverter.GetBytes(temp);
                    temp = Convert.ToInt16(param6MinUstavka_textBox.Text);
                    row6min = BitConverter.GetBytes(temp);

                    row1max.CopyTo(data, 22);
                    row1min.CopyTo(data, 20);
                    row2max.CopyTo(data, 18);
                    row2min.CopyTo(data, 16);
                    row3max.CopyTo(data, 14);
                    row3min.CopyTo(data, 12);
                    row4max.CopyTo(data, 10);
                    row4min.CopyTo(data, 8);
                    row5max.CopyTo(data, 6);
                    row5min.CopyTo(data, 4);
                    row6max.CopyTo(data, 2);
                    row6min.CopyTo(data, 0);
                }
                else
                {
                    data = new byte[12];

                    row1max.CopyTo(data, 10);
                    row1min.CopyTo(data, 8);
                    row2max.CopyTo(data, 6);
                    row2min.CopyTo(data, 4);
                    row3max.CopyTo(data, 2);
                    row3min.CopyTo(data, 0);
                }


                // Array.Reverse(data);

                modbus.WriteData(Modbus.EnquryType.INQ_SAVE_USTAVKI, data);
            }
            catch
            {
                MessageBox.Show(Globals.INCORRECT_DATA_MESSAGE);
            }
        }

        private void saveEnergyRows_button_Click(object sender, EventArgs e)
        {
            try
            {
                if (energyRow1_comboBox.SelectedIndex == -1
                    || energyRow2_comboBox.SelectedIndex == -1)
                {
                    throw new IndexOutOfRangeException();
                }

                byte[] row1 = BitConverter.GetBytes((ushort)energyRow1_comboBox.SelectedIndex);
                byte[] row2 = BitConverter.GetBytes((ushort)energyRow2_comboBox.SelectedIndex);

                byte[] data = new byte[4];

                row1.CopyTo(data, 2);
                row2.CopyTo(data, 0);

                modbus.WriteData(Modbus.EnquryType.INQ_SAVE_ENERGY_ROWS, data);
            }
            catch
            {
                MessageBox.Show(Globals.INCORRECT_DATA_MESSAGE);
            }
        }

        private void saveChangedParams_button_Click(object sender, EventArgs e)
        {
            //if (backgroundWorker.IsBusy)
            //{
            //    backgroundWorker.CancelAsync();
            //}

            for (int i = 0; i < 100; i++)
            {
                main_progressBar.Value = i;
                sendInqury_Timer.Stop();
                Thread.Sleep(2);
            }

            byte[] data = new byte[2];
            data[0] = 0;
            data[1] = 0;
            modbus.WriteData(Modbus.EnquryType.INQ_SAVE_TO_INTERNAL_FLASH, data);
        }

        private void saveStatus_label_TextChanged(object sender, EventArgs e)
        {
            if (saveStatus_label.Text != "")
            {
                statusLabel_Timer.Stop();
                statusLabel_Timer.Start();
            }
            else
                statusLabel_Timer.Stop();
        }

        private void cancelZeroingNoise_button_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 100; i++)
            {
                main_progressBar.Value = i;
                sendInqury_Timer.Stop();
                Thread.Sleep(2);
            }

            byte[] data = new byte[2];
            data[1] = 0;
            data[0] = 0;
            modbus.WriteData(Modbus.EnquryType.INQ_SAVE_ZEROING_NOISE, data);
        }

        private void zeroingNoise_button_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 100; i++)
            {
                main_progressBar.Value = i;
                sendInqury_Timer.Stop();
                Thread.Sleep(2);
            }

            byte[] data = new byte[2];
            data[1] = 1;
            data[0] = 0;
            modbus.WriteData(Modbus.EnquryType.INQ_SAVE_ZEROING_NOISE, data);
        }

        private void ShowEnterParamForm(object sender, EventArgs e)
        {
            if (modbus.storedMeasureSheme == (int)Modbus.MeasureScheme._4_WIRED)
            {
                Label temp = (Label)sender;
                int labelID = temp.TabIndex;

                if (labelID == 1 || labelID == 2 || labelID == 3 || labelID == 7 || labelID == 8 || labelID == 9)
                {
                    return; // не даем менять парамтры в трехпроводном режиме
                }
            }

            EnterParamForm form = new EnterParamForm(this, (Label)sender);
            form.StartPosition = FormStartPosition.Manual;
            form.Location = new Point(this.Left + this.Width / 3, this.Top + this.Height / 3);
            form.ShowDialog(this);
            form.Dispose();
        }

        public void SendChangeDataParam(int labelID, float data)
        {
            Modbus.EnquryType enqType = 0;
            if (labelID == 1) enqType = Modbus.EnquryType.INQ_SAVE_UAB_CHANGE_PARAM;
            else if (labelID == 2) enqType = Modbus.EnquryType.INQ_SAVE_UBC_CHANGE_PARAM;
            else if (labelID == 3) enqType = Modbus.EnquryType.INQ_SAVE_UCA_CHANGE_PARAM;
            else if (labelID == 4) enqType = Modbus.EnquryType.INQ_SAVE_IA_CHANGE_PARAM;
            else if (labelID == 5) enqType = Modbus.EnquryType.INQ_SAVE_IB_CHANGE_PARAM;
            else if (labelID == 6) enqType = Modbus.EnquryType.INQ_SAVE_IC_CHANGE_PARAM;
            else if (labelID == 7) enqType = Modbus.EnquryType.INQ_SAVE_UA_CHANGE_PARAM;
            else if (labelID == 8) enqType = Modbus.EnquryType.INQ_SAVE_UB_CHANGE_PARAM;
            else if (labelID == 9) enqType = Modbus.EnquryType.INQ_SAVE_UC_CHANGE_PARAM;
            else if (labelID == 16) enqType = Modbus.EnquryType.INQ_SAVE_F_CHANGE_PARAM;
            else return;

            byte[] bytes = BitConverter.GetBytes(data);

            for (int i = 0; i < 100; i++)
            {
                main_progressBar.Value = i;
                sendInqury_Timer.Stop();
                Thread.Sleep(2);
            }

            modbus.WriteData(enqType, bytes);
        }


        private void CorrectPowerUP(object sender, EventArgs e)
        {
            byte[] bytes = BitConverter.GetBytes((ushort)(((Button)sender).TabIndex));

            for (int i = 0; i < 100; i++)
            {
                main_progressBar.Value = i;
                sendInqury_Timer.Stop();
                Thread.Sleep(2);
            }

            modbus.WriteData(Modbus.EnquryType.INQ_SAVE_CORRECT_POWER_UP, bytes);
        }

        private void CorrectPowerDOWN(object sender, EventArgs e)
        {
            byte[] bytes = BitConverter.GetBytes((ushort)(((Button)sender).TabIndex));

            for (int i = 0; i < 100; i++)
            {
                main_progressBar.Value = i;
                sendInqury_Timer.Stop();
                Thread.Sleep(2);
            }

            modbus.WriteData(Modbus.EnquryType.INQ_SAVE_CORRECT_POWER_DOWN, bytes);
        }


        private void readAnalogOut_button_Click(object sender, EventArgs e)
        {
            if (!AutoLoopIsOn)
            {
                AutoLoopIsOn = true;
                radioButton2.Checked = true;
                AnalogOutsTypeGroupBox = false;
                readAnalogOut_button.Text = "Стоп";
                modbus.ReadData(Modbus.EnquryType.INQ_READ_ANALOG_OUT_THEN_CHANGE);
            }
            else
            {
                AutoLoopIsOn = false;
                AnalogOutsTypeGroupBox = true;
                saveAnalogouts_button.Enabled = false;
                saveChangedParams_button.Enabled = false;
                AnalogOutsRangesGroupBox = false;
                AnalogOutsGroupBoxes = false;
                readAnalogOut_button.Text = "Установить связь";
                sendInqury_Timer.Stop();

                Thread.Sleep(50);
                ushort temp = 0;
                byte[] bytes = BitConverter.GetBytes(temp);
                modbus.WriteData(Modbus.EnquryType.INQ_WRITE_CURRENT_MODE_OFF, bytes);
            }
        }

        private void autoLoop_checkBox_CheckedChanged(object sender, EventArgs e)
        {
            if (autoLoop_checkBox.Checked) AutoLoopIsOn = true;
            else AutoLoopIsOn = false;
        }

        private void saveAnalogoutsType_button_Click(object sender, EventArgs e)
        {
            try
            {
                if (analogOutsType_comboBox.SelectedIndex == -1) throw new IndexOutOfRangeException();
                byte[] data = BitConverter.GetBytes((ushort)analogOutsType_comboBox.SelectedIndex);
                modbus.WriteData(Modbus.EnquryType.INQ_SAVE_ANALOG_OUTS_TYPE, data);
            }
            catch
            {
                MessageBox.Show(Globals.INCORRECT_DATA_MESSAGE);
            }
        }

        private void AnalogOutsRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                if (analogOutsType_comboBox.SelectedIndex == 0)
                {
                    numericUpDown1.Value = 5;
                    numericUpDown2.Value = 5;
                    numericUpDown3.Value = 5;
                }
                else if (analogOutsType_comboBox.SelectedIndex == 1)
                {
                    numericUpDown1.Value = 20;
                    numericUpDown2.Value = 20;
                    numericUpDown3.Value = 20;
                }

                vScrollBar3.TabIndex = 0;
                vScrollBar4.TabIndex = 1;
                vScrollBar7.TabIndex = 2;

                numericUpDown1.Enabled = true;
                numericUpDown2.Enabled = true;
                numericUpDown3.Enabled = true;

                saveAnalogOut1_button.Enabled = true;
                saveAnalogOut2_button.Enabled = true;
                saveAnalogOut3_button.Enabled = true;

                vScrollBar1.Enabled = false;
                vScrollBar2.Enabled = false;
                vScrollBar3.Enabled = true;

                vScrollBar6.Enabled = false;
                vScrollBar5.Enabled = false;
                vScrollBar4.Enabled = true;

                vScrollBar9.Enabled = false;
                vScrollBar8.Enabled = false;
                vScrollBar7.Enabled = true;

            }
            else if (radioButton2.Checked)
            {
                if (analogOutsType_comboBox.SelectedIndex == 0)
                {
                    numericUpDown1.Value = 0;
                    numericUpDown2.Value = 0;
                    numericUpDown3.Value = 0;
                }
                else if (analogOutsType_comboBox.SelectedIndex == 1)
                {
                    numericUpDown1.Value = 12;
                    numericUpDown2.Value = 12;
                    numericUpDown3.Value = 12;
                }

                vScrollBar3.TabIndex = 3;
                vScrollBar4.TabIndex = 4;
                vScrollBar7.TabIndex = 5;

                numericUpDown1.Enabled = false;
                numericUpDown2.Enabled = false;
                numericUpDown3.Enabled = false;

                saveAnalogOut1_button.Enabled = false;
                saveAnalogOut2_button.Enabled = false;
                saveAnalogOut3_button.Enabled = false;

                vScrollBar1.Enabled = true;
                vScrollBar2.Enabled = true;
                vScrollBar3.Enabled = true;

                vScrollBar6.Enabled = true;
                vScrollBar5.Enabled = true;
                vScrollBar4.Enabled = true;

                vScrollBar9.Enabled = true;
                vScrollBar8.Enabled = true;
                vScrollBar7.Enabled = true;
            }
            else if (radioButton3.Checked)
            {
                if (analogOutsType_comboBox.SelectedIndex == 0)
                {
                    numericUpDown1.Value = -5;
                    numericUpDown2.Value = -5;
                    numericUpDown3.Value = -5;
                }
                else if (analogOutsType_comboBox.SelectedIndex == 1)
                {
                    numericUpDown1.Value = 4;
                    numericUpDown2.Value = 4;
                    numericUpDown3.Value = 4;
                }

                numericUpDown1.Enabled = false;
                numericUpDown2.Enabled = false;
                numericUpDown3.Enabled = false;

                saveAnalogOut1_button.Enabled = false;
                saveAnalogOut2_button.Enabled = false;
                saveAnalogOut3_button.Enabled = false;

                vScrollBar1.Enabled = false;
                vScrollBar2.Enabled = false;
                vScrollBar3.Enabled = false;

                vScrollBar6.Enabled = false;
                vScrollBar5.Enabled = false;
                vScrollBar4.Enabled = false;

                vScrollBar9.Enabled = false;
                vScrollBar8.Enabled = false;
                vScrollBar7.Enabled = false;
            }
        }



        private void CorrectAnalogOutUP(object sender, KeyEventArgs e)
        {
            byte[] bytes = BitConverter.GetBytes((ushort)(((VScrollBar)sender).TabIndex));

            ((VScrollBar)sender).Value = 0;
            for (int i = 0; i < 100; i++)
            {
                main_progressBar.Value = i;
                sendInqury_Timer.Stop();
                Thread.Sleep(2);
            }

            modbus.WriteData(Modbus.EnquryType.INQ_SAVE_ANALOG_CORRECT_UP, bytes);
        }

        private void CorrectAnalogOutDOWN(object sender, KeyEventArgs e)
        {
            byte[] bytes = BitConverter.GetBytes((ushort)(((VScrollBar)sender).TabIndex));

            ((VScrollBar)sender).Value = 0;
            for (int i = 0; i < 100; i++)
            {
                main_progressBar.Value = i;
                sendInqury_Timer.Stop();
                Thread.Sleep(2);
            }

            modbus.WriteData(Modbus.EnquryType.INQ_SAVE_ANALOG_CORRECT_DOWN, bytes);
        }

        private void vScrollBar_Scroll(object sender, ScrollEventArgs e)
        {
            if (e.OldValue > e.NewValue)
            {
                CorrectAnalogOutUP(sender, null);
            }
            else if (e.OldValue < e.NewValue)
            {
                CorrectAnalogOutDOWN(sender, null);
            }
        }


        private void saveAnalogOut_button_Click(object sender, EventArgs e)
        {
            Modbus.EnquryType enqType = 0;
            float data;
            if (sender == saveAnalogOut1_button)
            {
                data = (float)numericUpDown1.Value;
                enqType = Modbus.EnquryType.INQ_SAVE_CORRECT_ANALOG_OUT1;
            }
            else if (sender == saveAnalogOut2_button)
            {
                data = (float)numericUpDown2.Value;
                enqType = Modbus.EnquryType.INQ_SAVE_CORRECT_ANALOG_OUT2;
            }
            else if (sender == saveAnalogOut3_button)
            {
                data = (float)numericUpDown3.Value;
                enqType = Modbus.EnquryType.INQ_SAVE_CORRECT_ANALOG_OUT3;
            }
            else return;

            byte[] bytes = BitConverter.GetBytes(data);

            for (int i = 0; i < 100; i++)
            {
                main_progressBar.Value = i;
                sendInqury_Timer.Stop();
                Thread.Sleep(2);
            }

            modbus.WriteData(enqType, bytes);
        }

        private void radioButton_Click(object sender, EventArgs e)
        {
            ushort numberofCheckedRadioButton = (ushort)AnalogOutsRadioButtons;
            byte[] bytes = BitConverter.GetBytes(numberofCheckedRadioButton);
            for (int i = 0; i < 100; i++)
            {
                main_progressBar.Value = i;
                sendInqury_Timer.Stop();
                Thread.Sleep(2);
            }
            modbus.WriteData(Modbus.EnquryType.INQ_WRITE_CURRENT_MODE, bytes);
        }



        private void checkInputTimer_button_Click(object sender, EventArgs e)
        {
            byte[] bytes = new byte[2];
            if (checkInputTimer_button.Text == "Включить")
            {
                bytes = BitConverter.GetBytes((ushort)1);
            }
            else
            {
                bytes = BitConverter.GetBytes((ushort)0);
            }
            modbus.WriteData(Modbus.EnquryType.INQ_SET_INPUT_CLOCK_CHECK, bytes);
        }

        private void saveimpKWtH_button_Click(object sender, EventArgs e)
        {
            try
            {
                uint temp = Convert.ToUInt32(impWtH_comboBox.Text);
                byte[] data = BitConverter.GetBytes(temp);
                modbus.WriteData(Modbus.EnquryType.INQ_SAVE_IMP_WTH, data);
            }
            catch
            {
                MessageBox.Show(Globals.INCORRECT_DATA_MESSAGE);
            }
        }

        private delegate void setDateTime_button_ClickDelegate(object sender, EventArgs e);
        public void setDateTime_button_Click(object sender, EventArgs e)
        {
            if (this.InvokeRequired)
                this.BeginInvoke(new setDateTime_button_ClickDelegate(setDateTime_button_Click), new object[] { null, null });
            else
            {
                try
                {
                    string date = monthCalendar.SelectionRange.Start.ToShortDateString();
                    string time = hour_numericUpDown.Value.ToString() + ":" + minute_numericUpDown.Value.ToString() + ":" + seconds_numericUpDown.Value.ToString();
                    string dt = date + " " + time;
                    DateTime dateTime = DateTime.Parse(dt);

                    byte[] data = new byte[8];

                    byte[] year = BitConverter.GetBytes((ushort)dateTime.Year);
                    Array.Reverse(year);
                    year.CopyTo(data, 0);
                    data[2] = (byte)dateTime.Month;
                    data[3] = (byte)dateTime.Day;
                    data[5] = (byte)dateTime.Hour;
                    data[6] = (byte)dateTime.Minute;
                    data[7] = (byte)dateTime.Second;

                    //Array.Reverse(data);

                    modbus.WriteData(Modbus.EnquryType.INQ_SET_DATE_TIME, data);
                }
                catch
                {
                    MessageBox.Show(Globals.INCORRECT_DATA_MESSAGE);
                }
            }
        }

        private void impWtH_comboBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                UInt32 temp = Convert.ToUInt32(impWtH_comboBox.Text);
                errorProvider.SetError(impWtH_comboBox, "");
            }
            catch
            {
                errorProvider.SetError(impWtH_comboBox, Globals.INCORRECT_DATA_MESSAGE);
            }
        }

        //


        private void IndicatorRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton14.Checked)
            {
                indicatorRow1_comboBox.Enabled = true;
                indicatorRow2_comboBox.Enabled = true;
                indicatorRow3_comboBox.Enabled = true;

                indicator1MaxUstavka_textBox_TextChanged(null, null);
                indicator2MaxUstavka_textBox_TextChanged(null, null);
                indicator3MaxUstavka_textBox_TextChanged(null, null);

                indicator1MinUstavka_textBox_TextChanged(null, null);
                indicator2MinUstavka_textBox_TextChanged(null, null);
                indicator3MinUstavka_textBox_TextChanged(null, null);
            }
            else if (radioButton15.Checked)
            {
                ComboBox temp = indicatorRow1_comboBox;
                for (int i = 0; i < 3; i++)
                {
                    if (i == 1) temp = indicatorRow2_comboBox;
                    else if (i == 2) temp = indicatorRow3_comboBox;

                    if ((temp.SelectedIndex >= (int)PARAMS.Ua && temp.SelectedIndex <= (int)PARAMS.Uc)
                        || (temp.SelectedIndex >= (int)PARAMS.Pa && temp.SelectedIndex <= (int)PARAMS.Qc)
                        || temp.SelectedIndex == (int)PARAMS.Umd
                            || temp.SelectedIndex == (int)PARAMS.U0)
                    {
                        temp.Enabled = false;
                        writeindicator_button.Enabled = false;
                    }
                }
            }
        }

        private void indicator1MaxUstavka_textBox_TextChanged(object sender, EventArgs e)
        {
            ProcessUstavkiIndicatorTextBox(indicator1MaxUstavka_textBox, 1);
        }

        private void indicator2MaxUstavka_textBox_TextChanged(object sender, EventArgs e)
        {
            ProcessUstavkiIndicatorTextBox(indicator2MaxUstavka_textBox, 2);
        }

        private void indicator3MaxUstavka_textBox_TextChanged(object sender, EventArgs e)
        {
            ProcessUstavkiIndicatorTextBox(indicator3MaxUstavka_textBox, 3);
        }

        private void indicator1MinUstavka_textBox_TextChanged(object sender, EventArgs e)
        {
            ProcessUstavkiIndicatorTextBox(indicator1MinUstavka_textBox, 1);
        }

        private void indicator2MinUstavka_textBox_TextChanged(object sender, EventArgs e)
        {
            ProcessUstavkiIndicatorTextBox(indicator2MinUstavka_textBox, 2);
        }

        private void indicator3MinUstavka_textBox_TextChanged(object sender, EventArgs e)
        {
            ProcessUstavkiIndicatorTextBox(indicator3MinUstavka_textBox, 3);
        }

        private void ProcessUstavkiIndicatorTextBox(TextBox textBox, int row)
        {
            try
            {
                int temp = Convert.ToInt32(textBox.Text);
                errorProvider.SetError(textBox, "");

                ComboBox indicationRow = indicatorRow1_comboBox;
                if (row == 2) indicationRow = indicatorRow2_comboBox;
                else if (row == 3) indicationRow = indicatorRow3_comboBox;

                int result = CheckLimit(indicationRow.SelectedIndex, temp);
                if (result != -1) textBox.Text = result.ToString();

                if (errorProvider.GetError(indicator1MaxUstavka_textBox) == ""
                    && errorProvider.GetError(indicator2MaxUstavka_textBox) == ""
                    && errorProvider.GetError(indicator3MaxUstavka_textBox) == ""
                    && errorProvider.GetError(indicator1MinUstavka_textBox) == ""
                    && errorProvider.GetError(indicator2MinUstavka_textBox) == ""
                    && errorProvider.GetError(indicator3MinUstavka_textBox) == "")
                {
                    writeindicator_button.Enabled = true;
                }
            }
            catch
            {
                errorProvider.SetError(textBox, "Некоректно введено значение");
                writeindicator_button.Enabled = false;
            }
        }

        private void readIndicatorData_button_Click(object sender, EventArgs e)
        {
            indicator.ReadData();
        }

        private void writeindicator_button_Click(object sender, EventArgs e)
        {
            indicator.WriteData();
        }

        private void saveAnalogouts_button_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 100; i++)
            {
                main_progressBar.Value = i;
                sendInqury_Timer.Stop();
                Thread.Sleep(2);
            }

            byte[] data = new byte[2];
            data[0] = 0;
            data[1] = 0;
            modbus.WriteData(Modbus.EnquryType.INQ_SAVE_TO_INTERNAL_FLASH, data);
        }

        private void saveCoefs_button_Click(object sender, EventArgs e)
        {
            MainProgressBar = 0;
            try
            {
                if (currentIb_comboBox.SelectedIndex == -1) throw new IndexOutOfRangeException();
                byte[] data = BitConverter.GetBytes((ushort)currentIb_comboBox.SelectedIndex);
                modbus.WriteData(Modbus.EnquryType.INQ_SAVE_CURRENT_IB, data);
            }
            catch
            {
                MessageBox.Show(Globals.INCORRECT_DATA_MESSAGE);
            }
        }

        private void U1_comboBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
               // float temp = Convert.ToSingle(U1_comboBox.Text);
                float temp = float.Parse(U1_comboBox.Text.Replace(",", CultureInfo.InvariantCulture.NumberFormat.NumberDecimalSeparator), CultureInfo.InvariantCulture);
                errorProvider.SetError(U1_comboBox, "");
            }
            catch
            {
                errorProvider.SetError(U1_comboBox, Globals.INCORRECT_DATA_MESSAGE);
            }
        }

        private void I1_comboBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                //float temp = Convert.ToSingle(I1_comboBox.Text);
                float temp = float.Parse(I1_comboBox.Text.Replace(",", CultureInfo.InvariantCulture.NumberFormat.NumberDecimalSeparator), CultureInfo.InvariantCulture);
                errorProvider.SetError(I1_comboBox, "");
            }
            catch
            {
                errorProvider.SetError(I1_comboBox, Globals.INCORRECT_DATA_MESSAGE);
            }
        }

        private void U2_comboBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
               // float temp = Convert.ToSingle(U2_comboBox.Text);
                float temp = float.Parse(U2_comboBox.Text.Replace(",", CultureInfo.InvariantCulture.NumberFormat.NumberDecimalSeparator), CultureInfo.InvariantCulture);
                errorProvider.SetError(U2_comboBox, "");
            }
            catch
            {
                errorProvider.SetError(U2_comboBox, Globals.INCORRECT_DATA_MESSAGE);
            }
        }

        private void I2_comboBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                //float temp = Convert.ToSingle(I2_comboBox.Text);
                float temp = float.Parse(I2_comboBox.Text.Replace(",", CultureInfo.InvariantCulture.NumberFormat.NumberDecimalSeparator), CultureInfo.InvariantCulture);
                errorProvider.SetError(I2_comboBox, "");
            }
            catch
            {
                errorProvider.SetError(I2_comboBox, Globals.INCORRECT_DATA_MESSAGE);
            }
        }

        private void button49_Click(object sender, EventArgs e)
        {

        }

        private void clearEnergyJournal_button_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Данная операция очистит журнал энергии.\nУверены?", "Очистка журнала", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                byte[] data = new byte[2];
                data[0] = 0xFA;
                data[1] = 0;
                modbus.WriteData(Modbus.EnquryType.INQ_SAVE_CLEAR_JOURNAL, data);
            }
        }

        private void clearEventsJournal_button_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Данная операция очистит журнал событий.\nУверены?", "Очистка журнала", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                byte[] data = new byte[2];
                data[0] = 0xAF;
                data[1] = 0;
                modbus.WriteData(Modbus.EnquryType.INQ_SAVE_CLEAR_JOURNAL, data);
            }
        }

        private void readEventjournal_button_Click(object sender, EventArgs e)
        {
            if (readEventjournal_button.Text == Globals.READ_BUTTON_TAG)
            {
                AutoLoopIsOn = true;
                readEventjournal_button.Text = "Стоп";

                dataGridView1.Rows.Clear();


                if (radioButton16.Checked)
                    modbus.ReadFile(Modbus.EnquryType.INQ_READ_TURN_ON_OFF_DT_JOURNAL, 0);
                else if (radioButton17.Checked)
                    modbus.ReadFile(Modbus.EnquryType.INQ_READ_CHANGE_COEFS_DT_JOURNAL, 0);
                else if (radioButton18.Checked)
                    modbus.ReadFile(Modbus.EnquryType.INQ_READ_CURRENT_WITHOUT_VOLTAGE_DT_JOURNAL, 0);
                else if (radioButton19.Checked)
                    modbus.ReadFile(Modbus.EnquryType.INQ_READ_ERASE_LOG_DT_JOURNAL, 0);
                else if (radioButton20.Checked)
                    modbus.ReadFile(Modbus.EnquryType.INQ_READ_JUMPER_DT_JOURNAL, 0);
                else if (radioButton21.Checked)
                    modbus.ReadFile(Modbus.EnquryType.INQ_READ_SET_DATE_TIME_JOURNAL, 0);
                else if (radioButton22.Checked)
                    modbus.ReadFile(Modbus.EnquryType.INQ_READ_CLOCK_CHECK_DT_JOURNAL, 0);
                else if (radioButton31.Checked)
                    modbus.ReadFile(Modbus.EnquryType.INQ_READ_TARIFS_CHANGE, 0);
                else if (radioButton32.Checked)
                    modbus.ReadFile(Modbus.EnquryType.INQ_READ_SECONDS_DIFFERENCE_JOURNAL, 0);
                else if (radioButton33.Checked)
                    modbus.ReadFile(Modbus.EnquryType.INQ_READ_CLEAR_ENERGY_JOURNAL, 0);
            }
            else
            {
                readEventjournal_button.Text = Globals.READ_BUTTON_TAG;
                // for (int i = 0; i < 100; i++)
                // {
                AutoLoopIsOn = false;
                modbus.noAnswer_Timer.Stop();
                sendInqury_Timer.Stop();
                //  Thread.Sleep(2);
                // }
            }
        }

        private void energyJournals_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (energyJournals_comboBox.SelectedIndex == 0 || energyJournals_comboBox.SelectedIndex == 1)
            {
                interval_radioButton.Checked = true;
                //itog_radioButton.Enabled = false;
            }
            else
            {
                itog_radioButton.Enabled = true;
            }
        }

        private void readEnergy_button_Click(object sender, EventArgs e)
        {
            if (readEnergy_button.Text == Globals.READ_BUTTON_TAG)
            {
                ReadEnergyForm form = new ReadEnergyForm(energyJournals_comboBox.SelectedIndex, interval_radioButton.Checked);
                form.StartPosition = FormStartPosition.Manual;
                form.Location = new Point(this.Left + this.Width / 4, this.Top + this.Height / 3);
                if (form.ShowDialog(this) == DialogResult.OK)
                {
                    crc_rb.FunctionalCode = form.FunctionalCode;
                    crc_rb.StartIndex = form.StartIndex;
                    crc_rb.NumberOfRecords = form.NumberOfRecords;
                    crc_rb.Status = (byte)CRC_RB.AnswerCodes.COD_OK;
                    AutoLoopIsOn = true;
                    dataGridView2.Rows.Clear();
                    chart1.Series[0].Points.Clear();
                    chart1.Series[1].Points.Clear();
                    crc_rb.packageDataArray = new CRC_RB.PackageData[0];
                    readEnergy_button.Text = "Стоп";

                    modbus.ReadData(Modbus.EnquryType.INQ_READ_COEFS_FOR_CRCRB);
                }
                form.Dispose();
            }
            else
            {
                readEnergy_button.Text = Globals.READ_BUTTON_TAG;
                AutoLoopIsOn = false;
                modbus.noAnswer_Timer.Stop();
                sendInqury_Timer.Stop();
            }
        }

        private void EnParam_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetEnergyPrefix(modbus.transformationCoefs.U1, modbus.transformationCoefs.I1);

            dataGridView2.Rows.Clear();
            for (int i = 0; i < crc_rb.packageDataArray.Length; i++)
            {
                SetEnergyDataGrid(crc_rb.packageDataArray[i]);
            }

            chart1.Series[0].Points.Clear();
            chart1.Series[1].Points.Clear();
            for (int i = 0; i < crc_rb.packageDataArray.Length; i++)
            {
                SetEnergyChart(crc_rb.packageDataArray[i]);
            }
        }

        private void chData_MouseWheel(object sender, MouseEventArgs e)
        {
            try
            {
                //double currentSIZEx = chart1.ChartAreas[0].AxisX.Maximum;
                //double currentSIZEy = chart1.ChartAreas[0].AxisY.Maximum;
                if (e.Delta < 0)
                {
                    //  chart1.ChartAreas[0].AxisX.ScaleView.ZoomReset();
                    // chart1.ChartAreas[0].AxisY.ScaleView.ZoomReset();
                    //  chart1.ChartAreas[0].AxisY.Maximum = (currentSIZEy - currentSIZEy / 5);
                    // chart1.ChartAreas[0].AxisY.ScaleView.Zoom()
                }

                if (e.Delta > 0)
                {
                    //double xMin = chart1.ChartAreas[0].AxisX.ScaleView.ViewMinimum;
                    //double xMax = chart1.ChartAreas[0].AxisX.ScaleView.ViewMaximum;
                    //double yMin = chart1.ChartAreas[0].AxisY.ScaleView.ViewMinimum;
                    //double yMax = chart1.ChartAreas[0].AxisY.ScaleView.ViewMaximum;

                    //double posXStart = chart1.ChartAreas[0].AxisX.PixelPositionToValue(e.Location.X) - (xMax - xMin) / 10;
                    //double posXFinish = chart1.ChartAreas[0].AxisX.PixelPositionToValue(e.Location.X) + (xMax - xMin) / 10;
                    //double posYStart = chart1.ChartAreas[0].AxisY.PixelPositionToValue(e.Location.Y) - (yMax - yMin) / 10;
                    //double posYFinish = chart1.ChartAreas[0].AxisY.PixelPositionToValue(e.Location.Y) + (yMax - yMin) / 10;



                    //chart1.ChartAreas[0].AxisY.Maximum = (currentSIZEy + currentSIZEy / 5);
                    //chart1.ChartAreas[0].AxisX.Maximum = (currentSIZEx + currentSIZEx / 5);

                    // chart1.ChartAreas[0].AxisX.ScaleView.Zoom(100, 102);
                    //chart1.ChartAreas[0].AxisY.ScaleView.Zoom(100, 102);
                    //chart1.ChartAreas[0].AxisX.ScaleView.Zoom(posXStart, posXFinish);
                    //chart1.ChartAreas[0].AxisY.ScaleView.Zoom(posYStart, posYFinish);
                }
            }
            catch { }
        }

        private void chart1_MouseMove(object sender, MouseEventArgs e)
        {

            ShowChartLabel(e);

            //chart1.ChartAreas[0].AxisX.Minimum = 0;
            //chart1.ChartAreas[0].AxisX.Maximum = 1;
            //chart1.ChartAreas[0].AxisX.Interval = 1;
            //chart1.ChartAreas[0].AxisY.Minimum = 0;
            //chart1.ChartAreas[0].AxisY.Maximum = 1;
            //chart1.ChartAreas[0].AxisY.Interval = 1; 
            // chart1.Invalidate();

            //int vx = (int)chart1.ChartAreas[0].AxisX.PixelPositionToValue(e.Location.X);
            //double vy = chart1.ChartAreas[0].AxisY.PixelPositionToValue(e.Location.Y);

            //if (e.Button.HasFlag(MouseButtons.Left) && dp_ != null)
            //{
            //double vx = chart1.ChartAreas[0].AxisX.PixelPositionToValue(e.Location.X);
            //double vy = chart1.ChartAreas[0].AxisY.PixelPositionToValue(e.Location.Y);


            //    chart1.Invalidate();
            //}
            //else
            //{
            //    Cursor = Cursors.Default;
            //    foreach (DataPoint dp in s_.Points)
            //        if (((RectangleF)dp.Tag).Contains(e.Location))
            //        {
            //            Cursor = Cursors.Hand; break;
            //        }
            //}
        }

        private void ShowChartLabel(MouseEventArgs e)
        {
            if (checkBox1.Checked == false)
            {
                return;
            }
            try
            {
                int pointNumber1 = 0;
                int distance1 = chart1.Width + chart1.Height;
                int pointNumber2 = 0;
                int distance2 = chart1.Width + chart1.Height;
                for (int i = 0; i < chart1.Series[0].Points.Count; i++)
                {
                    int pointX = (int)(chart1.ChartAreas[0].AxisX.ValueToPixelPosition(chart1.Series[0].Points[i].XValue));
                    int pointY = (int)(chart1.ChartAreas[0].AxisY.ValueToPixelPosition(chart1.Series[0].Points[i].YValues[0]));

                    int dist = Convert.ToInt32(Math.Sqrt((pointX - e.Location.X) * (pointX - e.Location.X) + (pointY - e.Location.Y) * (pointY - e.Location.Y)));
                    if (dist <= distance1)
                    {
                        distance1 = dist;
                        pointNumber1 = i;
                    }

                    chart1.Series[0].Points[i].MarkerSize = 7;
                    chart1.Series[0].Points[i].MarkerColor = Color.FromArgb(255, 170, 170);
                }
                for (int i = 0; i < chart1.Series[1].Points.Count; i++)
                {
                    int pointX = (int)(chart1.ChartAreas[0].AxisX.ValueToPixelPosition(chart1.Series[1].Points[i].XValue));
                    int pointY = (int)(chart1.ChartAreas[0].AxisY.ValueToPixelPosition(chart1.Series[1].Points[i].YValues[0]));

                    int dist = Convert.ToInt32(Math.Sqrt((pointX - e.Location.X) * (pointX - e.Location.X) + (pointY - e.Location.Y) * (pointY - e.Location.Y)));
                    if (dist <= distance2)
                    {
                        distance2 = dist;
                        pointNumber2 = i;
                    }

                    chart1.Series[1].Points[i].MarkerSize = 7;
                    chart1.Series[1].Points[i].MarkerColor = Color.FromArgb(110, 190, 190);
                }
                if (chart1.Series[0].Points.Count != 0)
                {
                    int pointX;
                    int pointY;

                    if (distance2 < distance1)
                    {
                        pointX = (int)(chart1.ChartAreas[0].AxisX.ValueToPixelPosition(chart1.Series[1].Points[pointNumber2].XValue));
                        pointY = (int)(chart1.ChartAreas[0].AxisY.ValueToPixelPosition(chart1.Series[1].Points[pointNumber2].YValues[0]));
                        chart1.Series[1].Points[pointNumber2].MarkerSize = 11;
                        chart1.Series[1].Points[pointNumber2].MarkerColor = Color.White;

                        string value = secondEnParam_comboBox.Text + " = " + chart1.Series[1].Points[pointNumber2].YValues[0].ToString("N3");
                        if (crc_rb.packageDataArray[pointNumber2].Notation != "")
                        {
                            value = crc_rb.packageDataArray[pointNumber2].Notation;
                        }
                        label29.Text = value + Environment.NewLine + crc_rb.packageDataArray[pointNumber2].dt.ToString("dd.MM.yyyy HH:mm");
                    }
                    else
                    {
                        pointX = (int)(chart1.ChartAreas[0].AxisX.ValueToPixelPosition(chart1.Series[0].Points[pointNumber1].XValue));
                        pointY = (int)(chart1.ChartAreas[0].AxisY.ValueToPixelPosition(chart1.Series[0].Points[pointNumber1].YValues[0]));
                        chart1.Series[0].Points[pointNumber1].MarkerSize = 11;
                        chart1.Series[0].Points[pointNumber1].MarkerColor = Color.White;

                        string value = firstEnParam_comboBox.Text + " = " + chart1.Series[0].Points[pointNumber1].YValues[0].ToString("N3");
                        if (crc_rb.packageDataArray[pointNumber1].Notation != "")
                        {
                            value = crc_rb.packageDataArray[pointNumber2].Notation;
                        }
                        label29.Text = value + Environment.NewLine + crc_rb.packageDataArray[pointNumber1].dt.ToString("dd.MM.yyyy HH:mm");
                    }
                    label29.Location = new Point(pointX - label29.Width / 2 + 5, pointY - label29.Height - 3);
                    label29.Visible = true;
                }
            }
            catch (Exception ex)
            {
                MainForm.WriteLog("Ошибка вывода лейбла на график " + ex.Message + ex.InnerException + ex.Source + ex.StackTrace);
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                if (chart1.Series[1].Points.Count == 0)
                {
                    checkBox1.Checked = false;
                    return;
                }
            }
            else
            {
                label29.Visible = false;
                for (int i = 0; i < chart1.Series[1].Points.Count; i++)
                {
                    chart1.Series[0].Points[i].MarkerSize = 7;
                    chart1.Series[0].Points[i].MarkerColor = Color.FromArgb(255, 170, 170);
                    chart1.Series[1].Points[i].MarkerSize = 7;
                    chart1.Series[1].Points[i].MarkerColor = Color.FromArgb(110, 190, 190);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            chart1.ChartAreas[0].AxisX.ScaleView.ZoomReset(0);
            chart1.ChartAreas[0].AxisY.ScaleView.ZoomReset(0);
        }

        private void communication_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (communication_comboBox.SelectedIndex == 0)
            {
                panel5.Visible = true;
                panel6.Visible = false;
                findAddr_button.Visible = true;
            }
            else if (communication_comboBox.SelectedIndex == 1)
            {
                //panel5.Visible = false;
                panel6.Visible = true;
                findAddr_button.Visible = false;
            }
        }

        private void deviceNumber_textBox_TextChanged_1(object sender, EventArgs e)
        {
            if (deviceNumber_textBox.Text == "") return;
            try
            {
                int number = Convert.ToInt32(deviceNumber_textBox.Text);
                if (number < 0) deviceNumber_textBox.Text = "1";
                if (number > 254) deviceNumber_textBox.Text = "254";
            }
            catch
            {
                deviceNumber_textBox.Text = "1";
            }
        }

        private void ethernetAddr_comboBox_TextChanged(object sender, EventArgs e)
        {
            Regex regIP = new Regex(@"\b(([01]?\d?\d|2[0-4]\d|25[0-5])\.){3}([01]?\d?\d|2[0-4]\d|25[0-5])\b");
            if (regIP.IsMatch(ethernetAddr_comboBox.Text))
            {
                if (ethernetAddr_comboBox.Items.IndexOf(ethernetAddr_comboBox.Text) != -1)
                    button3.Visible = true;
                else
                    button3.Visible = false;
            }
            else
            {
                button3.Visible = false;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ethernetAddr_comboBox.Items.Remove(ethernetAddr_comboBox.Items[ethernetAddr_comboBox.SelectedIndex].ToString());
        }

        private void saveIP_button_Click(object sender, EventArgs e)
        {
            Regex regIP = new Regex(@"\b(([01]?\d?\d|2[0-4]\d|25[0-5])\.){3}([01]?\d?\d|2[0-4]\d|25[0-5])\b");
            byte[] data = new byte[12];
            if (regIP.IsMatch(IPAddress_textBox.Text))
            {
                string[] values = IPAddress_textBox.Text.Split('.');
                for (int i = 0; i < values.Length; i++)
                {
                    data[i + 8] = Convert.ToByte(values[values.Length - i - 1]);
                }
            }
            else
            {
                MessageBox.Show("Неверный формат IP адреса");
                return;
            }

            if (regIP.IsMatch(mask_textBox.Text))
            {
                string[] values = mask_textBox.Text.Split('.');
                for (int i = 0; i < values.Length; i++)
                {
                    data[i + 4] = Convert.ToByte(values[values.Length - i - 1]);
                }
            }
            else
            {
                MessageBox.Show("Неверный формат маски подсети");
                return;
            }

            if (regIP.IsMatch(gateway_textBox.Text))
            {
                string[] values = gateway_textBox.Text.Split('.');
                for (int i = 0; i < values.Length; i++)
                {
                    data[i] = Convert.ToByte(values[values.Length - i - 1]);
                }
            }
            else
            {
                MessageBox.Show("Неверный формат шлюза");
                return;
            }

            modbus.WriteData(Modbus.EnquryType.INQ_SAVE_ETHERNET_CONFIG, data);
        }

        private void SaveAOuts_button_Click(object sender, EventArgs e)
        {
            try
            {
                if (analogOut_1_ComboBox.SelectedIndex == -1
                    || analogOut_2_ComboBox.SelectedIndex == -1
                    || analogOut_3_ComboBox.SelectedIndex == -1)
                {
                    throw new IndexOutOfRangeException();
                }

                byte[] data;

                byte[] row1 = BitConverter.GetBytes((ushort)analogOut_1_ComboBox.SelectedIndex);
                byte[] row2 = BitConverter.GetBytes((ushort)analogOut_2_ComboBox.SelectedIndex);
                byte[] row3 = BitConverter.GetBytes((ushort)analogOut_3_ComboBox.SelectedIndex);

                data = new byte[6];
                row1.CopyTo(data, 4);
                row2.CopyTo(data, 2);
                row3.CopyTo(data, 0);

                modbus.WriteData(Modbus.EnquryType.INQ_SAVE_ANALOG_OUTS_ROWS, data);
            }
            catch
            {
                MessageBox.Show(Globals.INCORRECT_DATA_MESSAGE);
            }
        }

        private void readAnalogConfig_Button_Click(object sender, EventArgs e)
        {
            modbus.ReadData(Modbus.EnquryType.INQ_READ_ANALOG_OUT_THEN_ROWS);
        }

        private void readPKE_button_Click(object sender, EventArgs e)
        {
            if (readPKE_button.Text == Globals.READ_BUTTON_TAG)
            {
                readPKE_button.Text = "Стоп";
                autoLoopPKE_checkBox_CheckedChanged(sender, e);
                modbus.ReadData(Modbus.EnquryType.INQ_READ_PKE_INSTANT);
            }
            else
            {
                readPKE_button.Text = Globals.READ_BUTTON_TAG;
                //autoLoopPKE_checkBox.Checked = false;
                AutoLoopIsOn = false;
                modbus.noAnswer_Timer.Stop();
                sendInqury_Timer.Stop();
            }
        }

        private void readAveragePKE_Button_Click(object sender, EventArgs e)
        {
            if (readAveragePKE_Button.Text == Globals.READ_BUTTON_TAG)
            {
                int fileNumber = -1;
                if (radioButton29.Checked) fileNumber = 0;
                else if (radioButton28.Checked) fileNumber = 1;
                else if (radioButton27.Checked) fileNumber = 2;
                else if (radioButton26.Checked) fileNumber = 3;
                else if (radioButton25.Checked) fileNumber = 4;
                else if (radioButton23.Checked) fileNumber = 15;
                else if (radioButton24.Checked) fileNumber = 16;
                else if (radioButton30.Checked) fileNumber = 17;//
                else if (radioButton45.Checked) fileNumber = 5;
                else if (radioButton44.Checked) fileNumber = 6;
                else if (radioButton43.Checked) fileNumber = 7;
                else if (radioButton42.Checked) fileNumber = 8;
                else if (radioButton41.Checked) fileNumber = 18;
                else if (radioButton40.Checked) fileNumber = 19;
                else if (radioButton39.Checked) fileNumber = 20;

                ReadPKEForm form = new ReadPKEForm(fileNumber);
                form.StartPosition = FormStartPosition.Manual;
                form.Location = new Point(this.Left + this.Width / 4, this.Top + this.Height / 3);
                if (form.ShowDialog(this) == DialogResult.OK)
                {
                    elZIP.StartDT = form.DT;
                    elZIP.NumberOfRecords = form.NumberOfRecords;
                    elZIP.FileNumber = (byte)fileNumber;

                    AutoLoopIsOn = true;
                    PKE_Table.Rows.Clear();
                    readAveragePKE_Button.Text = "Стоп";

                    if (radioButton29.Checked) PKE_Table.Columns[2].HeaderText = "K0U (%)";
                    else if (radioButton28.Checked) PKE_Table.Columns[2].HeaderText = "K2U (%)";
                    else if (radioButton27.Checked) PKE_Table.Columns[2].HeaderText = "Δf (Гц)";
                    else if (radioButton26.Checked) PKE_Table.Columns[2].HeaderText = "δU+ (%)";
                    else if (radioButton25.Checked) PKE_Table.Columns[2].HeaderText = "δU- (%)";

                    elZIP.ReadData();
                }
                form.Dispose();
            }
            else
            {
                readAveragePKE_Button.Text = Globals.READ_BUTTON_TAG;
                AutoLoopIsOn = false;
                modbus.noAnswer_Timer.Stop();
                sendInqury_Timer.Stop();
            }

            //if (readAveragePKE_Button.Text == Globals.READ_BUTTON_TAG)
            //{
            //    AutoLoopIsOn = true;
            //    readAveragePKE_Button.Text = "Стоп";

            //    PKE_Table.Rows.Clear();


            //    if (radioButton29.Checked)
            //    {
            //        PKE_Table.Columns[2].HeaderText = "K0U (%)";
            //        modbus.ReadFile(Modbus.EnquryType.INQ_READ_KU0_JOURNAL, 0);
            //    }
            //    else if (radioButton28.Checked)
            //    {
            //        PKE_Table.Columns[2].HeaderText = "K2U (%)";
            //        modbus.ReadFile(Modbus.EnquryType.INQ_READ_KU2_JOURNAL, 0);
            //    }
            //    else if (radioButton27.Checked)
            //    {
            //        PKE_Table.Columns[2].HeaderText = "Δf (Гц)";
            //        modbus.ReadFile(Modbus.EnquryType.INQ_READ_dF_JOURNAL, 0);
            //    }
            //    else if (radioButton26.Checked)
            //    {
            //        PKE_Table.Columns[2].HeaderText = "δU+ (%)";
            //        modbus.ReadFile(Modbus.EnquryType.INQ_READ_dUPLUS_JOURNAL, 0);
            //    }
            //    else if (radioButton25.Checked)
            //    {
            //        PKE_Table.Columns[2].HeaderText = "δU- (%)";
            //        modbus.ReadFile(Modbus.EnquryType.INQ_READ_dUMINUS_JOURNAL, 0);
            //    }
            //}
            //else
            //{
            //    readAveragePKE_Button.Text = Globals.READ_BUTTON_TAG;
            //    // for (int i = 0; i < 100; i++)
            //    // {
            //    AutoLoopIsOn = false;
            //    modbus.noAnswer_Timer.Stop();
            //    sendInqury_Timer.Stop();
            //    //  Thread.Sleep(2);
            //    // }
            //}
        }

        private void autoLoopPKE_checkBox_CheckedChanged(object sender, EventArgs e)
        {
            if (autoLoopPKE_checkBox.Checked) AutoLoopIsOn = true;
            else AutoLoopIsOn = false;
        }

        private void saveToFile_button_Click(object sender, EventArgs e)
        {
            DataGridView temp = PKE_Table;

            Button butt = (Button)sender;
            if (butt == savePKEToFile_button) temp = PKE_Table;
            else if (butt == button2) temp = dataGridView1;

            saveFileDialog.Filter = "Документ Excel (*.xlsx)|*.xlsx|Документ Excel (*.csv)|*.csv"; // 

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                Microsoft.Office.Interop.Excel.Application ExcelApp = new Microsoft.Office.Interop.Excel.Application();
                try
                {
                    ExcelApp.DisplayAlerts = false;
                    if (saveFileDialog.FilterIndex == 1)
                    {
                        ExcelApp.DefaultSaveFormat = Microsoft.Office.Interop.Excel.XlFileFormat.xlExcel12; // xlExcel12
                    }
                    else if (saveFileDialog.FilterIndex == 2)
                    {
                        ExcelApp.DefaultSaveFormat = Microsoft.Office.Interop.Excel.XlFileFormat.xlCSV; // xlExcel12
                    }

                    ExcelApp.Workbooks.Add(Type.Missing);

                    ExcelApp.Columns.ColumnWidth = 20;

                    if (butt == button5)
                    {
                        ExcelApp.Cells[1, 1] = "№";
                        ExcelApp.Cells[1, 2] = "Дата \\ Время";
                        ExcelApp.Cells[1, 3] = "Wa";
                        ExcelApp.Cells[1, 4] = "Wa +";
                        ExcelApp.Cells[1, 5] = "Wa -";
                        ExcelApp.Cells[1, 6] = "Wr";
                        ExcelApp.Cells[1, 7] = "Wr +";
                        ExcelApp.Cells[1, 8] = "Wr -";
                        ExcelApp.Cells[1, 9] = "Wr 1";
                        ExcelApp.Cells[1, 10] = "Wr 2";
                        ExcelApp.Cells[1, 11] = "Wr 3";
                        ExcelApp.Cells[1, 12] = "Wr 4";
                        ExcelApp.Cells[1, 13] = "Примечание";
                    }
                    else
                    {
                        for (int j = 0; j < temp.ColumnCount; j++)
                        {
                            ExcelApp.Cells[1, j + 1] = temp.Columns[j].HeaderText;
                        }
                    }

                    if (butt == button5)
                    {
                        for (int i = 0; i < crc_rb.packageDataArray.Length; i++)
                        {
                            StatusLabel = "Создание файла: " + (100 / ((float)crc_rb.packageDataArray.Length / (i == 0 ? 1 : i))).ToString("N0") + "%";
                            ExcelApp.Cells[i + 2, 1] = (i + 1).ToString();
                            ExcelApp.Cells[i + 2, 2] = crc_rb.packageDataArray[i].dt.ToString("dd.MM.yyyy  HH:mm:ss");
                            ExcelApp.Cells[i + 2, 3] = crc_rb.packageDataArray[i].Wa.ToString("N3");
                            ExcelApp.Cells[i + 2, 4] = crc_rb.packageDataArray[i].Wa_plus.ToString("N3");
                            ExcelApp.Cells[i + 2, 5] = crc_rb.packageDataArray[i].Wa_minus.ToString("N3");
                            ExcelApp.Cells[i + 2, 6] = crc_rb.packageDataArray[i].Wr.ToString("N3");
                            ExcelApp.Cells[i + 2, 7] = crc_rb.packageDataArray[i].Wr_plus.ToString("N3");
                            ExcelApp.Cells[i + 2, 8] = crc_rb.packageDataArray[i].Wr_minus.ToString("N3");
                            ExcelApp.Cells[i + 2, 9] = crc_rb.packageDataArray[i].Wr1.ToString("N3");
                            ExcelApp.Cells[i + 2, 10] = crc_rb.packageDataArray[i].Wr2.ToString("N3");
                            ExcelApp.Cells[i + 2, 11] = crc_rb.packageDataArray[i].Wr3.ToString("N3");
                            ExcelApp.Cells[i + 2, 12] = crc_rb.packageDataArray[i].Wr4.ToString("N3");
                            ExcelApp.Cells[i + 2, 13] = crc_rb.packageDataArray[i].Notation.ToString();
                        }
                    }
                    else
                    {
                        for (int i = 0; i < temp.RowCount; i++)
                        {
                            StatusLabel = "Создание файла: " + (100 / ((float)temp.RowCount / (i == 0 ? 1 : i))).ToString("N0") + "%";
                            for (int j = 0; j < temp.ColumnCount; j++)
                            {
                                ExcelApp.Cells[i + 2, j + 1] = (temp[j, i].Value).ToString();
                            }
                        }
                    }


                    Microsoft.Office.Interop.Excel.Workbook workBook = ExcelApp.Workbooks[1];
                    workBook.SaveAs(saveFileDialog.FileName);
                    ShowMessageBox("Файл сохранен");
                    //StatusLabel = "Файл сохранен";
                    ExcelApp.Quit();
                }
                catch
                {
                    if (ExcelApp != null)
                    {
                        ExcelApp.Quit();
                    }
                    MessageBox.Show("Ошибка сохранения файла");
                }
            }
        }

        private void additionalSettings_button_Click(object sender, EventArgs e)
        {
            addSettings.StartPosition = FormStartPosition.Manual;
            addSettings.Location = new Point(this.Left + this.Width / 4, this.Top + this.Height / 3);
            if (addSettings.ShowDialog(this) == DialogResult.OK)
            {
                //    crc_rb.FunctionalCode = form.FunctionalCode;
                //    crc_rb.StartIndex = form.StartIndex;
                //    crc_rb.NumberOfRecords = form.NumberOfRecords;
                //    crc_rb.Status = (byte)CRC_RB.AnswerCodes.COD_OK;
                //    AutoLoopIsOn = true;
                //    dataGridView2.Rows.Clear();
                //    chart1.Series[0].Points.Clear();
                //    chart1.Series[1].Points.Clear();
                //    crc_rb.packageDataArray = new CRC_RB.PackageData[0];
                //    readEnergy_button.Text = "Стоп";

                //    modbus.ReadData(Modbus.EnquryType.INQ_READ_COEFS_FOR_CRCRB);
            }
        }

        private void Q_CalcMethod_button_Click(object sender, EventArgs e)
        {
            try
            {
                if (Q_CalcMethod_comboBox.SelectedIndex == -1) throw new IndexOutOfRangeException();
                byte[] data = BitConverter.GetBytes((ushort)Q_CalcMethod_comboBox.SelectedIndex);
                modbus.WriteData(Modbus.EnquryType.INQ_SAVE_Q_CALC_METHOD, data);
            }
            catch
            {
                MessageBox.Show(Globals.INCORRECT_DATA_MESSAGE);
            }
        }

        private void saveImpulseOuts_button_Click(object sender, EventArgs e)
        {
            ushort waTemp = 0;
            if (radioButton12.Checked) waTemp = 1;
            else if (radioButton13.Checked) waTemp = 2;

            ushort wrTemp = 0;
            if (radioButton5.Checked) wrTemp = 1;
            else if (radioButton6.Checked) wrTemp = 2;
            else if (radioButton7.Checked) wrTemp = 3;
            else if (radioButton8.Checked) wrTemp = 4;
            else if (radioButton9.Checked) wrTemp = 5;
            else if (radioButton10.Checked) wrTemp = 6;

            int temp = (waTemp << 8) | wrTemp;
            byte[] data = BitConverter.GetBytes((ushort)temp);
            modbus.WriteData(Modbus.EnquryType.INQ_SAVE_IMPULSE_OUTS, data);
        }

        private delegate void EnterPasswordDelegate(byte level);
        public void EnterPassword(byte level)
        {
            if (this.InvokeRequired)
                this.BeginInvoke(new EnterPasswordDelegate(EnterPassword), new object[] { level });
            else
            {
                // Modbus.EnquryType tempEnqType = storedEnqType;
                EnterPasswordForm form = new EnterPasswordForm();
                form.StartPosition = FormStartPosition.Manual;
                form.Location = new Point(this.Left + this.Width / 3, this.Top + this.Height / 3);
                form.ShowDialog(this);
                if (form.PasswordSetted)
                {
                    int pass = form.GetPassword;
                    pass |= (level << 24);
                    byte[] data = BitConverter.GetBytes(pass);
                    modbus.WriteData(Modbus.EnquryType.INQ_SET_PASSWORD, data);
                }
                form.Dispose();
            }
        }

        private void tarifEditorOpen_button_Click(object sender, EventArgs e)
        {
            tarificationForm = new TarificationForm(this);
            tarificationForm.ShowDialog();
        }

        private void readDaylight_button_Click(object sender, EventArgs e)
        {
            modbus.ReadData(Modbus.EnquryType.INQ_READ_DAYLIGHT);
        }

        private void writeDaylight_button_Click(object sender, EventArgs e)
        {
            try
            {
                if (daylightType_comboBox.SelectedIndex == -1) throw new IndexOutOfRangeException();
                byte[] data = BitConverter.GetBytes((ushort)daylightType_comboBox.SelectedIndex);
                modbus.WriteData(Modbus.EnquryType.INQ_SAVE_DAYLIGHT, data);
            }
            catch
            {
                MessageBox.Show(Globals.INCORRECT_DATA_MESSAGE);
            }
        }

        bool IsTheSameCellValue(int column, int row)
        {
            DataGridViewCell cell1 = PKE_Table[column, row];
            DataGridViewCell cell2 = PKE_Table[column, row - 1];
            if (cell1.Value == null || cell2.Value == null)
            {
                return false;
            }
            if (cell1.Value.ToString() == cell2.Value.ToString()) return true;
            if (cell1.Value.ToString() == "") return true;
            return false;
        }

        private void PKE_Table_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            e.AdvancedBorderStyle.Bottom = DataGridViewAdvancedCellBorderStyle.None;
            if (e.RowIndex < 1 || e.ColumnIndex < 0)
                return;
            if (IsTheSameCellValue(e.ColumnIndex, e.RowIndex) && e.ColumnIndex <= 1)
            {
                e.AdvancedBorderStyle.Top = DataGridViewAdvancedCellBorderStyle.None;
            }
            else
            {
                e.AdvancedBorderStyle.Top = PKE_Table.AdvancedCellBorderStyle.Top;
            }
        }

        private void PKE_Table_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex == 0)
                return;
            if (e.ColumnIndex > 1)
                return;
            if (IsTheSameCellValue(e.ColumnIndex, e.RowIndex))
            {
                e.FormattingApplied = true;
            }
        }

        private void GenerateDipVoltageTable()
        {
            dipVoltage_DGV.Rows.Add();
            dipVoltage_DGV.Rows[0].Height = 25;
            dipVoltage_DGV.Rows.Add();
            dipVoltage_DGV.Rows[1].Height = 26;
            dipVoltage_DGV.Rows.Add();
            dipVoltage_DGV.Rows[2].Height = 26;
            dipVoltage_DGV.Rows.Add();
            dipVoltage_DGV.Rows[3].Height = 26;
            dipVoltage_DGV.Rows.Add();
            dipVoltage_DGV.Rows[4].Height = 25;

            noVoltage_DGV.Rows.Add();
            noVoltage_DGV.Rows[0].Height = 24;

            overVoltage_DGV.Rows.Add();
            overVoltage_DGV.Rows[0].Height = 24;
        }

        private void dipVoltage_DGV_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex == 0 && e.ColumnIndex == 0)
            {
                e.AdvancedBorderStyle.Bottom = DataGridViewAdvancedCellBorderStyle.None;
            }
            if (e.RowIndex == 1 && e.ColumnIndex == 0)
            {
                e.AdvancedBorderStyle.Top = DataGridViewAdvancedCellBorderStyle.None;
            }
            if (e.RowIndex == 0 && e.ColumnIndex > 0 && e.ColumnIndex < 6)
            {
                e.AdvancedBorderStyle.Left = DataGridViewAdvancedCellBorderStyle.None;
                e.AdvancedBorderStyle.Right = DataGridViewAdvancedCellBorderStyle.None;
            }
        }

        private void dipVoltage_DGV_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                ((DataGridView)sender).SelectedCells[0].Selected = false;
            }
            catch { }
        }

        private void noVoltage_DGV_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                ((DataGridView)sender).SelectedCells[0].Selected = false;
            }
            catch { }
        }

        private void overVoltage_DGV_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                ((DataGridView)sender).SelectedCells[0].Selected = false;
            }
            catch { }
        }

        private void readRandomPKE_button_Click(object sender, EventArgs e)
        {
            if (readRandomPKE_button.Text == Globals.READ_BUTTON_TAG)
            {
                readRandomPKE_button.Text = "Стоп";
                modbus.ReadData(Modbus.EnquryType.INQ_READ_DIP_VOLTAGE_PKE);
            }
            else
            {
                readRandomPKE_button.Text = Globals.READ_BUTTON_TAG;
                //autoLoopPKE_checkBox.Checked = false;
                modbus.noAnswer_Timer.Stop();
                sendInqury_Timer.Stop();
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {
            if ((Control.ModifierKeys & Keys.Control) != 0)
            {
                groupBox101.Visible = !groupBox101.Visible;
                groupBox152.Visible = !groupBox152.Visible;
            }
            else if ((Control.ModifierKeys & Keys.Shift) != 0)
            {
                //form.StartPosition = FormStartPosition.Manual;
                //form.Location = new Point(this.Left + this.Width / 4, this.Top + this.Height / 3);
                //uppConfigurator = 

                uppConfigurator.Show();
            }
        }


        private void dipVoltage_DGV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dipVoltage_DGV.CurrentCell != null && dipVoltage_DGV.CurrentCell.Value != null && (int)dipVoltage_DGV.CurrentCell.Value != 0)
            {
                randomPKEForm = new RandomPKE_DG(this, "Провалы напряжения");
                randomPKEForm.StartPosition = FormStartPosition.Manual;
                randomPKEForm.Location = new Point(this.Left + this.Width / 3, this.Top + this.Height / 3);

                int temp = (int)dipVoltage_DGV.CurrentCell.Value;
                elZIP.NumberOfRecords = (ushort)temp;
                elZIP.FileNumber = 10;
                elZIP.CellNumber = (ushort)((int)dipVoltage_DGV.CurrentCell.RowIndex * dipVoltage_DGV.Columns.Count + (int)dipVoltage_DGV.CurrentCell.ColumnIndex);
                AutoLoopIsOn = true;
                elZIP.ReadData();

                randomPKEForm.ShowDialog();
            }
        }

        private void noVoltage_DGV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (noVoltage_DGV.CurrentCell != null && noVoltage_DGV.CurrentCell.Value != null && (int)noVoltage_DGV.CurrentCell.Value != 0)
            {
                randomPKEForm = new RandomPKE_DG(this, "Прерывания напряжения");
                randomPKEForm.StartPosition = FormStartPosition.Manual;
                randomPKEForm.Location = new Point(this.Left + this.Width / 3, this.Top + this.Height / 3);

                int temp = (int)noVoltage_DGV.CurrentCell.Value;
                elZIP.NumberOfRecords = (ushort)temp;
                elZIP.FileNumber = 11;
                elZIP.CellNumber = (ushort)((int)noVoltage_DGV.CurrentCell.RowIndex * noVoltage_DGV.Columns.Count + (int)noVoltage_DGV.CurrentCell.ColumnIndex);
                AutoLoopIsOn = true;
                elZIP.ReadData();

                randomPKEForm.ShowDialog();
            }
        }

        private void overVoltage_DGV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (overVoltage_DGV.CurrentCell != null && overVoltage_DGV.CurrentCell.Value != null && (int)overVoltage_DGV.CurrentCell.Value != 0)
            {
                randomPKEForm = new RandomPKE_DG(this, "Перенапряжения");
                randomPKEForm.StartPosition = FormStartPosition.Manual;
                randomPKEForm.Location = new Point(this.Left + this.Width / 3, this.Top + this.Height / 3);

                int temp = (int)overVoltage_DGV.CurrentCell.Value;
                elZIP.NumberOfRecords = (ushort)temp;
                elZIP.FileNumber = 9;
                elZIP.CellNumber = (ushort)((int)overVoltage_DGV.CurrentCell.RowIndex * overVoltage_DGV.Columns.Count + (int)overVoltage_DGV.CurrentCell.ColumnIndex);
                AutoLoopIsOn = true;
                elZIP.ReadData();

                randomPKEForm.ShowDialog();
            }
        }

        private void clearEnergy_button_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Данная операция очистит накопленную энергию.\nУверены?", "Очистка энергии", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                byte[] data = new byte[2];
                data[0] = 0xAF;
                data[1] = 0;
                modbus.WriteData(Modbus.EnquryType.INQ_SAVE_CLEAR_ENERGY, data);
            }
        }

        private void openIEC101_button_Click(object sender, EventArgs e)
        {

            IEC101Settings.StartPosition = FormStartPosition.Manual;
            IEC101Settings.Location = new Point(this.Left + this.Width / 10, this.Top + this.Height / 10);
            if (IEC101Settings.ShowDialog(this) == DialogResult.OK)
            {
                //    crc_rb.FunctionalCode = form.FunctionalCode;
                //    crc_rb.StartIndex = form.StartIndex;
                //    crc_rb.NumberOfRecords = form.NumberOfRecords;
                //    crc_rb.Status = (byte)CRC_RB.AnswerCodes.COD_OK;
                //    AutoLoopIsOn = true;
                //    dataGridView2.Rows.Clear();
                //    chart1.Series[0].Points.Clear();
                //    chart1.Series[1].Points.Clear();
                //    crc_rb.packageDataArray = new CRC_RB.PackageData[0];
                //    readEnergy_button.Text = "Стоп";

                //    modbus.ReadData(Modbus.EnquryType.INQ_READ_COEFS_FOR_CRCRB);
            }
        }

        private void openIEC104_button_Click(object sender, EventArgs e)
        {

            IEC104Settings.StartPosition = FormStartPosition.Manual;
            IEC104Settings.Location = new Point(this.Left + this.Width / 10, this.Top + this.Height / 10);
            if (IEC104Settings.ShowDialog(this) == DialogResult.OK)
            {
                //    crc_rb.FunctionalCode = form.FunctionalCode;
                //    crc_rb.StartIndex = form.StartIndex;
                //    crc_rb.NumberOfRecords = form.NumberOfRecords;
                //    crc_rb.Status = (byte)CRC_RB.AnswerCodes.COD_OK;
                //    AutoLoopIsOn = true;
                //    dataGridView2.Rows.Clear();
                //    chart1.Series[0].Points.Clear();
                //    chart1.Series[1].Points.Clear();
                //    crc_rb.packageDataArray = new CRC_RB.PackageData[0];
                //    readEnergy_button.Text = "Стоп";

                //    modbus.ReadData(Modbus.EnquryType.INQ_READ_COEFS_FOR_CRCRB);
            }
        }

        private void findAddr_button_Click(object sender, EventArgs e)
        {
            byte[] buffer = new byte[8];
            int byteIndex = 0;

            buffer[byteIndex++] = 0xFF;
            buffer[byteIndex++] = 0x77; //
            buffer[byteIndex++] = 0xFF;
            buffer[byteIndex++] = 0xFF; //
            buffer[byteIndex++] = 0x00;
            buffer[byteIndex++] = 0x01; //
            buffer[byteIndex++] = 0x21; //
            buffer[byteIndex++] = 0xFB; //

            modbus.StoredEnquryType = Modbus.EnquryType.INQ_READ_DEVICE_ADDR_2;

            modbus.SendMessage(ComPortLink, ComPortsComboBox, buffer);
        }

        private void tabPage22_Click(object sender, EventArgs e)
        {

        }

        private void radioButton34_CheckedChanged(object sender, EventArgs e)
        {
            tabControl6.TabPages.Remove(tabPage25);
            tabControl6.TabPages.Remove(tabPage26);
            tabControl6.TabPages.Remove(tabPage27);
            if ((RadioButton)sender == radioButton34 || (RadioButton)sender == radioButton35 || (RadioButton)sender == radioButton36)
            {
                tabControl6.TabPages.Add(tabPage25);
            }
            if ((RadioButton)sender == radioButton37)
            {
                tabControl6.TabPages.Add(tabPage26);
            }
            if ((RadioButton)sender == radioButton38)
            {
                tabControl6.TabPages.Add(tabPage27);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            indicator23.ReadData();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            indicator23.WriteData();
        }

        private void comboBox39_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((ComboBox)sender == comboBox39)
            {
                comboBox41.SelectedIndex = comboBox39.SelectedIndex;
            }
            else
            {
                comboBox39.SelectedIndex = comboBox41.SelectedIndex;
            }
        }

        private void comboBox38_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((ComboBox)sender == comboBox38)
            {
                comboBox42.SelectedIndex = comboBox38.SelectedIndex;
            }
            else
            {
                comboBox38.SelectedIndex = comboBox42.SelectedIndex;
            }
        }

        private void comboBox37_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((ComboBox)sender == comboBox37)
            {
                comboBox40.SelectedIndex = comboBox37.SelectedIndex;
            }
            else
            {
                comboBox37.SelectedIndex = comboBox40.SelectedIndex;
            }
        }

        private void checkBox32_CheckedChanged(object sender, EventArgs e)
        {
            if ((CheckBox)sender == checkBox32)
            {
                checkBox35.Checked = checkBox32.Checked;
            }
            else
            {
                checkBox32.Checked = checkBox35.Checked;
            }
        }

        private void checkBox33_CheckedChanged(object sender, EventArgs e)
        {
            if ((CheckBox)sender == checkBox33)
            {
                checkBox37.Checked = checkBox33.Checked;
            }
            else
            {
                checkBox33.Checked = checkBox37.Checked;
            }
        }

        private void checkBox34_CheckedChanged(object sender, EventArgs e)
        {
            if ((CheckBox)sender == checkBox34)
            {
                checkBox36.Checked = checkBox34.Checked;
            }
            else
            {
                checkBox34.Checked = checkBox36.Checked;
            }
        }

        private void comboBox43_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((ComboBox)sender == comboBox46)
            {
                comboBox43.SelectedIndex = comboBox46.SelectedIndex;
            }
            else
            {
                comboBox46.SelectedIndex = comboBox43.SelectedIndex;
            }
        }

        private void comboBox44_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((ComboBox)sender == comboBox47)
            {
                comboBox44.SelectedIndex = comboBox47.SelectedIndex;
            }
            else
            {
                comboBox47.SelectedIndex = comboBox44.SelectedIndex;
            }
        }

        private void comboBox45_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((ComboBox)sender == comboBox48)
            {
                comboBox45.SelectedIndex = comboBox48.SelectedIndex;
            }
            else
            {
                comboBox48.SelectedIndex = comboBox45.SelectedIndex;
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            modbus.TestIC();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            modbus.ReadData10();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            modbus.ReadData11();
        }

        private void clearCyclePKE_Button_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Уверены?", "Очистка данных", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                byte[] data = new byte[2];
                data[0] = 0xDF;
                data[1] = 0;
                modbus.WriteData(Modbus.EnquryType.INQ_SAVE_CLEAR_JOURNAL, data);
            }
        }

        private void clearRandomPKE_Button_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Уверены?", "Очистка данных", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                byte[] data = new byte[2];
                data[0] = 0xFD;
                data[1] = 0;
                modbus.WriteData(Modbus.EnquryType.INQ_SAVE_CLEAR_JOURNAL, data);
            }
        }
    }
}

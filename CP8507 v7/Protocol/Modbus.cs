using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO.Ports;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace CP8507_v7
{
    public partial class Modbus : Protocol
    {
        private EnquryType storedEnquryType;
        public EnquryType storedEnquryTypeForPassword;
        private byte[] storedDataForPassword;

        private ushort storedRecordNumber;

        public ushort _typeOfPribor = 0;

        public static int Port = 502;

        public int storedMeasureSheme = 1;

        public TransformationCoefs transformationCoefs;
        public RelaySettings[] relaySettings;

        private const byte readRegister_FC = 3;
        private const byte writeRegister_FC = 6;
        private const byte writeRegisters_FC = 16;
        private const byte readFile_FC = 20;
        private const byte error_FC = 0x80;

        private long maxAnswerTime = 0;
        private long inquresCounter = 0;
        private long crcErrorCounter = 0;
        private long noAnswerCounter = 0;

        private byte[] lastBuffer;
        private EnquryType lastType;

        public enum EnquryType
        {
            INQ_READ_COEFS_THEN_PARAMS,
            INQ_READ_31_PARAMS,
            INQ_READ_31_PARAMS_AND_ENERGY,
            INQ_READ_MAIN_CONFIG_PARAMS,
            INQ_READ_IEC_101_PARAMS,
            INQ_READ_IEC_104_PARAMS,
            INQ_READ_IEC_101_VALUES,
            INQ_READ_IEC_104_VALUES,
            INQ_READ_MODBUS_PARAMS,
            INQ_READ_RELAY_1_2_PARAMS,
            INQ_READ_RELAY_3_PARAMS,
            INQ_READ_RELAY_1_2_GISTEREZIS,
            INQ_READ_RELAY_3_GISTEREZIS,
            INQ_READ_ENERGY_ROWS,
            INQ_READ_INDICATION_INFO,
            INQ_READ_DISPLAY_ROWS,
            INQ_READ_DISPLAY_USTAVKI,
            INQ_READ_COEFS_AND_MEASURE_PARAMS,
            INQ_READ_MEASURE_PARAM,
            INQ_READ_TYPE_IB,
            INQ_READ_IMPULSE_OUTS,
            INQ_READ_IMP_WTH,
            INQ_READ_DATE_TIME,
            INQ_READ_CHECK_SPEED_A,
            INQ_READ_DEVICE_ADDR,
            INQ_READ_DEVICE_ADDR_2,

            INQ_SAVE_NET_ADDR,
            INQ_SAVE_SPEED_A,
            INQ_SAVE_SPEED_B,
            INQ_SAVE_CONFIG_B,
            INQ_SAVE_CUTTOFF_PERCENT,
            INQ_SAVE_YEAR_AND_FACTORYNUM,
            INQ_SAVE_MODBUS_CONFIG,
            INQ_SAVE_IEC_101_CONFIG,
            INQ_SAVE_IEC_104_CONFIG,
            INQ_SAVE_IEC_101_VALUES,
            INQ_SAVE_IEC_104_VALUES,
            INQ_SAVE_RELAY_1_CONFIG,
            INQ_SAVE_RELAY_1_GISTEREZIS,
            INQ_SAVE_RELAY_2_CONFIG,
            INQ_SAVE_RELAY_2_GISTEREZIS,
            INQ_SAVE_RELAY_3_CONFIG,
            INQ_SAVE_RELAY_3_GISTEREZIS,
            INQ_SAVE_NUMBER_OF_ROWS,
            INQ_SAVE_DISPLAY_LIGHT,
            INQ_SAVE_INDICATION_ROWS,
            INQ_SAVE_USTAVKI,
            INQ_SAVE_ENERGY_ROWS,
            INQ_SAVE_TO_INTERNAL_FLASH,
            INQ_SAVE_ZEROING_NOISE,

            INQ_READ_DEVICE_MODEL,

            INQ_SAVE_UA_CHANGE_PARAM,
            INQ_SAVE_UB_CHANGE_PARAM,
            INQ_SAVE_UC_CHANGE_PARAM,

            INQ_SAVE_UAB_CHANGE_PARAM,
            INQ_SAVE_UBC_CHANGE_PARAM,
            INQ_SAVE_UCA_CHANGE_PARAM,

            INQ_SAVE_IA_CHANGE_PARAM,
            INQ_SAVE_IB_CHANGE_PARAM,
            INQ_SAVE_IC_CHANGE_PARAM,

            INQ_SAVE_F_CHANGE_PARAM,

            INQ_SAVE_CORRECT_POWER_UP,
            INQ_SAVE_CORRECT_POWER_DOWN,

            INQ_READ_ANALOG_OUT_THEN_CHANGE,
            INQ_SAVE_ANALOG_OUTS_TYPE,

            INQ_WRITE_CURRENT_MODE,
            INQ_WRITE_CURRENT_MODE_OFF,
            INQ_READ_AUTOLOOP,

            INQ_SAVE_ANALOG_CORRECT_UP,
            INQ_SAVE_ANALOG_CORRECT_DOWN,

            INQ_SAVE_CORRECT_ANALOG_OUT1,
            INQ_SAVE_CORRECT_ANALOG_OUT2,
            INQ_SAVE_CORRECT_ANALOG_OUT3,

            INQ_SET_INPUT_CLOCK_CHECK,
            INQ_SET_DATE_TIME,
            INQ_SAVE_IMP_WTH,

            INQ_SAVE_MEASURE_SCHEME,
            INQ_SAVE_CURRENT_IB,
            INQ_SAVE_COEF_U1,
            INQ_SAVE_COEF_I1,
            INQ_SAVE_COEF_U2,
            INQ_SAVE_COEF_I2,
            INQ_SAVE_Q_CALC_METHOD,
            INQ_SAVE_IMPULSE_OUTS,

            INQ_SAVE_CLEAR_JOURNAL,

            INQ_SET_PASSWORD,

            INQ_READ_TURN_ON_OFF_DT_JOURNAL,
            INQ_READ_CHANGE_COEFS_DT_JOURNAL,
            INQ_READ_CURRENT_WITHOUT_VOLTAGE_DT_JOURNAL,
            INQ_READ_ERASE_LOG_DT_JOURNAL,
            INQ_READ_JUMPER_DT_JOURNAL,
            INQ_READ_SET_DATE_TIME_JOURNAL,
            INQ_READ_CLOCK_CHECK_DT_JOURNAL,
            INQ_READ_TARIFS_CHANGE,
            INQ_READ_SECONDS_DIFFERENCE_JOURNAL,
            INQ_READ_CLEAR_ENERGY_JOURNAL,

            INQ_READ_KU0_JOURNAL,
            INQ_READ_KU2_JOURNAL,
            INQ_READ_dF_JOURNAL,
            INQ_READ_dUPLUS_JOURNAL,
            INQ_READ_dUMINUS_JOURNAL,

            INQ_READ_COEFS_FOR_CRCRB,
            INQ_READ_CRCRB_AUTOLOOP,

            INQ_READ_ETHERNET_CONFIG,
            INQ_SAVE_ETHERNET_CONFIG,

            INQ_READ_ANALOG_OUTS_ROWS,
            INQ_SAVE_ANALOG_OUTS_ROWS,
            INQ_READ_ANALOG_OUT_THEN_ROWS,
            INQ_READ_PKE_INSTANT,
            INQ_READ_HARMONIC_VA_INSTANT,
            INQ_READ_HARMONIC_VB_INSTANT,
            INQ_READ_HARMONIC_VC_INSTANT,
            INQ_READ_HARMONIC_IA_INSTANT,
            INQ_READ_HARMONIC_IB_INSTANT,
            INQ_READ_HARMONIC_IC_INSTANT,
            INQ_READ_INTERHARMONIC_VA_INSTANT,
            INQ_READ_INTERHARMONIC_VB_INSTANT,
            INQ_READ_INTERHARMONIC_VC_INSTANT,
            INQ_READ_INTERHARMONIC_IA_INSTANT,
            INQ_READ_INTERHARMONIC_IB_INSTANT,
            INQ_READ_INTERHARMONIC_IC_INSTANT,
            INQ_READ_Q_CALC_METHOD,

            INQ_READ_MEASURE_SCHEME_THEN_PARAMS,

            INQ_READ_ELZIP_AUTOLOOP,

            INQ_SAVE_RELAYS_NUMBER,
            INQ_SAVE_INTERFACES_NUMBER,
            INQ_SAVE_MODIFICATION,

            INQ_READ_PARAMS_FOR_ADDSETTINGS_1,
            INQ_READ_PARAMS_FOR_ADDSETTINGS_2,
            INQ_READ_PARAMS_FOR_ADDSETTINGS_3,

            INQ_WRITE_TARIF_PRO,
            INQ_READ_TARIF_PRO,

            INQ_READ_TARIF0,
            INQ_READ_TARIF1,
            INQ_READ_TARIF2,
            INQ_READ_TARIF3,
            INQ_READ_TARIF4,
            INQ_READ_TARIF5,
            INQ_READ_TARIF6,
            INQ_READ_TARIF7,
            INQ_READ_TARIF8,

            INQ_READ_DAYLIGHT,
            INQ_SAVE_DAYLIGHT,

            INQ_READ_DIP_VOLTAGE_PKE,
            INQ_READ_NO_VOLTAGE_PKE,
            INQ_READ_OVER_VOLTAGE_PKE,

            INQ_SAVE_CLEAR_ENERGY,

            INQ_SET_UPP_CONFIG,

            INQ_SET_V_UPP,
            INQ_SET_I_UPP,
            INQ_SET_I5_UPP,
            INQ_SET_F_UPP,

            INQ_READ_31_PARAMS_FROM_UPP,

            INQ_SET_UPP_ON_VOLTAGE,
            INQ_SET_UPP_ON_Ia5,
            INQ_SET_UPP_ON_Ib5,
            INQ_SET_UPP_ON_Ic5,
            INQ_SET_UPP_ON_Ia,
            INQ_SET_UPP_ON_Ib,
            INQ_SET_UPP_ON_Ic,
            INQ_SET_UPP_OFF_VOLTAGE,
            INQ_SET_UPP_OFF_Ia5,
            INQ_SET_UPP_OFF_Ib5,
            INQ_SET_UPP_OFF_Ic5,
            INQ_SET_UPP_OFF_Ia,
            INQ_SET_UPP_OFF_Ib,
            INQ_SET_UPP_OFF_Ic,
            INQ_SET_UPP_OFF_CURRENT,
            INQ_SET_UPP_5A_1A,
            INQ_SET_UPP_IMP_NUM_OUT,
            INQ_SET_UPP_IMP_MA,
            INQ_SET_UPP_IMP_R,
            INQ_SET_UPP_F_GEN,
            INQ_SET_UPP_AMP_COEF_UA,
            INQ_SET_UPP_AMP_COEF_UB,
            INQ_SET_UPP_AMP_COEF_UC,
            INQ_SET_UPP_AMP_COEF_IA,
            INQ_SET_UPP_AMP_COEF_IB,
            INQ_SET_UPP_AMP_COEF_IC,
            INQ_SET_UPP_AMP_COEF_IA5,
            INQ_SET_UPP_AMP_COEF_IB5,
            INQ_SET_UPP_AMP_COEF_IC5,
            INQ_SET_UPP_AMP_COEF_UA400,
            INQ_SET_UPP_AMP_COEF_UB400,
            INQ_SET_UPP_AMP_COEF_UC400,
            INQ_SET_UPP_ANGLE_COEF_UA,
            INQ_SET_UPP_ANGLE_COEF_UB,
            INQ_SET_UPP_ANGLE_COEF_UC,
            INQ_SET_UPP_ANGLE_COEF_IA,
            INQ_SET_UPP_ANGLE_COEF_IB,
            INQ_SET_UPP_ANGLE_COEF_IC,
            INQ_SET_UPP_ANGLE_COEF_IA5,
            INQ_SET_UPP_ANGLE_COEF_IB5,
            INQ_SET_UPP_ANGLE_COEF_IC5,
            INQ_SET_UPP_ANGLE_COEF_UA400,
            INQ_SET_UPP_ANGLE_COEF_UB400,
            INQ_SET_UPP_ANGLE_COEF_UC400,
            INQ_SET_UPP_ADC_ZERO_5,
            INQ_SET_UPP_ADC_ZERO_20,
            INQ_SET_UPP_DAC_ZERO_5,
            INQ_SET_UPP_DAC_ZERO_20,
            INQ_SET_UPP_ADC_TOP_5,
            INQ_SET_UPP_ADC_TOP_20,
            INQ_SET_UPP_DAC_TOP_5,
            INQ_SET_UPP_DAC_TOP_20,
            INQ_READ_UPP_ANGLE_COEFS,
            INQ_READ_UPP_OTHER_COEFS,
            INQ_SET_UPP_ANGLE_COEFFS,
            INQ_SET_UPP_OTHER_COEFFS,
            INQ_SAVE_UPP_COEFS,
            INQ_SET_ANGLE_UPP,
            INQ_SET_UPP_DAC_VAL,
            INQ_SET_UPP_U_OUT,
            INQ_SET_UPP_I_OUT,
        }

        public enum MeasureScheme
        {
            _4_WIRED,
            _3_WIRED,
        }

        private delegate void Delegate(byte[] buffer);
        private delegate void Delegate_2(object sender, EventArgs e);

        public Modbus(MainForm form)
        {
            mainForm = form;

            relaySettings = new RelaySettings[3];
            transformationCoefs = new TransformationCoefs();
            transformationCoefs.U1 = 0;
            transformationCoefs.I1 = 0;
        }


        public override bool CheckProtocol(byte[] buffer)
        {
            if (buffer.Length <= 1) throw new Exception("Количество принятых данных <= 1");

            byte funcCode = (byte)(buffer[1] & 0x80);
            if (buffer[0] > 0 && buffer[0] < 247 &&
                (buffer[1] == readRegister_FC || buffer[1] == writeRegister_FC || buffer[1] == writeRegisters_FC || buffer[1] == readFile_FC || funcCode == error_FC))
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
                byte funcCode = (byte)(buffer[1] & 0x80);
                if (funcCode == error_FC)
                {
                    DecodeErrorPackage(buffer[2]);
                    if (mainForm.AutoLoopIsOn
                        && mainForm.MainTabControlSelectedIndex == 2)
                        mainForm.StartAutoInquryTimer(EnquryType.INQ_READ_31_PARAMS, 500);
                    if (mainForm.uppConfigurator.readCoefs_button.Text == ProtocolGlobals.OFF_BUTTON) mainForm.uppConfigurator.Reconnect();
                }
                else if (buffer[1] == readRegister_FC || buffer[1] == readFile_FC)
                {
                    ProcessReadPackage(buffer);
                }
                else if (buffer[1] == writeRegister_FC || buffer[1] == writeRegisters_FC)
                {
                    ProcessWritePackage(buffer);
                }
            }
            else
            {
                if (mainForm.MainTabControlSelectedIndex == 0 || mainForm.MainTabControlSelectedIndex == 1) // если выбрана первая вкладка
                {
                    crcErrorCounter++;
                    mainForm.CRCErrorTextBox = crcErrorCounter.ToString();
                    mainForm.ColorStatusButton = Color.Crimson;

                    if (mainForm.AutoLoopIsOn)
                    {
                        mainForm.StartAutoInquryTimer(storedEnquryType, 100);
                    }
                    else
                    {
                        mainForm.Read31ParamButton = Globals.READ_BUTTON_TAG;
                    }
                }
                else if (mainForm.MainTabControlSelectedIndex == 2 && mainForm.AutoLoopIsOn)
                {
                    mainForm.StartAutoInquryTimer(storedEnquryType, 50);
                }
                else if (mainForm.MainTabControlSelectedIndex == 5 && mainForm.AutoLoopIsOn)
                {
                    mainForm.StartAutoInquryTimer(storedEnquryType, 50);
                }

                if (mainForm.uppConfigurator.readCoefs_button.Text == ProtocolGlobals.OFF_BUTTON) mainForm.uppConfigurator.Reconnect();

                mainForm.StatusLabel = ProtocolGlobals.CRC_ERROR_MESSAGE;
            }
        }



        public void ReadFile(EnquryType fileType, ushort recordNumber)
        {
            if (mainForm.CommunicationComboBox == ProtocolGlobals.ETHERNET_TAG)
            {
                if (!base.TryToConnectTCP(mainForm.IPAddressComboBox, Modbus.Port))
                    return;
            }
            if (mainForm.DeviceNumberTextBox == "")
            {
                mainForm.ShowMessageBox(ProtocolGlobals.INCORRECT_DEVICE_ADDRESS_MESSAGE);
                return;
            }

            ushort fileNumber = 0;
            ushort datalenght = 0;
            ChooseParamsForReaFileInqury(fileType, out fileNumber, out datalenght);

            byte[] buffer = new byte[12];
            int byteIndex = 0;

            if (mainForm.CommunicationComboBox == ProtocolGlobals.ETHERNET_TAG)
            {
                buffer = new byte[16];
                buffer[byteIndex++] = 0x00;
                buffer[byteIndex++] = 0x00; //
                buffer[byteIndex++] = 0x00;
                buffer[byteIndex++] = 0x00; //
                buffer[byteIndex++] = 0x00;
                buffer[byteIndex++] = 0x0A; //
            }
            buffer[byteIndex++] = Convert.ToByte(mainForm.DeviceNumberTextBox);
            buffer[byteIndex++] = readFile_FC;
            buffer[byteIndex++] = 7;
            buffer[byteIndex++] = 6;
            byte[] byteArray = BitConverter.GetBytes(fileNumber);
            buffer[byteIndex++] = byteArray[1];
            buffer[byteIndex++] = byteArray[0];
            byteArray = BitConverter.GetBytes(recordNumber);
            buffer[byteIndex++] = byteArray[1];
            buffer[byteIndex++] = byteArray[0];
            byteArray = BitConverter.GetBytes(datalenght);
            buffer[byteIndex++] = byteArray[1];
            buffer[byteIndex++] = byteArray[0];

            if (mainForm.CommunicationComboBox == ProtocolGlobals.RS485_TAG)
            {
                ushort crc = base.CalculateCRC16(buffer);
                byteArray = BitConverter.GetBytes(crc);
                buffer[byteIndex++] = byteArray[1];
                buffer[byteIndex++] = byteArray[0];
            }

            storedEnquryType = fileType;
            storedRecordNumber = recordNumber;

            if (mainForm.CommunicationComboBox == ProtocolGlobals.RS485_TAG)
            {
                base.SendMessage(mainForm.ComPortLink, mainForm.ComPortsComboBox, buffer);
            }
            else if (mainForm.CommunicationComboBox == ProtocolGlobals.ETHERNET_TAG)
            {
                base.SendMessageTCP(mainForm.tcpClient, buffer);
            }
        }

        private void ChooseParamsForReaFileInqury(EnquryType fileType, out ushort fileNum, out ushort dataLenght)
        {
            fileNum = 0;
            dataLenght = 0;

            switch (fileType)
            {
                case EnquryType.INQ_READ_KU0_JOURNAL:
                    {
                        fileNum = 30;
                        dataLenght = 10;
                        break;
                    }
                case EnquryType.INQ_READ_KU2_JOURNAL:
                    {
                        fileNum = 31;
                        dataLenght = 10;
                        break;
                    }
                case EnquryType.INQ_READ_dF_JOURNAL:
                    {
                        fileNum = 32;
                        dataLenght = 10;
                        break;
                    }
                case EnquryType.INQ_READ_dUPLUS_JOURNAL:
                    {
                        fileNum = 33;
                        dataLenght = 10;
                        break;
                    }
                case EnquryType.INQ_READ_dUMINUS_JOURNAL:
                    {
                        fileNum = 34;
                        dataLenght = 10;
                        break;
                    }
                case EnquryType.INQ_READ_TURN_ON_OFF_DT_JOURNAL:
                    {
                        fileNum = 1;
                        dataLenght = 6;
                        break;
                    }
                case EnquryType.INQ_READ_CHANGE_COEFS_DT_JOURNAL:
                    {
                        fileNum = 2;
                        dataLenght = 7;
                        break;
                    }
                case EnquryType.INQ_READ_CURRENT_WITHOUT_VOLTAGE_DT_JOURNAL:
                    {
                        fileNum = 3;
                        dataLenght = 4;
                        break;
                    }
                case EnquryType.INQ_READ_ERASE_LOG_DT_JOURNAL:
                    {
                        fileNum = 4;
                        dataLenght = 4;
                        break;
                    }
                case EnquryType.INQ_READ_JUMPER_DT_JOURNAL:
                    {
                        fileNum = 5;
                        dataLenght = 4;
                        break;
                    }
                case EnquryType.INQ_READ_SET_DATE_TIME_JOURNAL:
                    {
                        fileNum = 6;
                        dataLenght = 5;
                        break;
                    }
                case EnquryType.INQ_READ_CLOCK_CHECK_DT_JOURNAL:
                    {
                        fileNum = 7;
                        break;
                    }
                case EnquryType.INQ_READ_TARIFS_CHANGE:
                    {
                        fileNum = 8;
                        break;
                    }
                case EnquryType.INQ_READ_SECONDS_DIFFERENCE_JOURNAL:
                    {
                        fileNum = 9;
                        break;
                    }
                case EnquryType.INQ_READ_CLEAR_ENERGY_JOURNAL:
                    {
                        fileNum = 10;
                        break;
                    }
            }
        }


        public void SendLastPackage()
        {
            StoredEnquryType = lastType;
            if (mainForm.CommunicationComboBox == ProtocolGlobals.RS485_TAG)
            {
                base.SendMessage(mainForm.ComPortLink, mainForm.ComPortsComboBox, lastBuffer);
            }
            else if (mainForm.CommunicationComboBox == ProtocolGlobals.ETHERNET_TAG)
            {
                base.SendMessageTCP(mainForm.tcpClient, lastBuffer);
            }
        }


        public void ReadData(EnquryType enquryType)
        {
            if (mainForm.CommunicationComboBox == ProtocolGlobals.ETHERNET_TAG)
            {
                if (!base.TryToConnectTCP(mainForm.IPAddressComboBox, Modbus.Port))
                    return;
            }
            if (mainForm.DeviceNumberTextBox == "")
            {
                mainForm.ShowMessageBox(ProtocolGlobals.INCORRECT_DEVICE_ADDRESS_MESSAGE);
                // 
                return;
            }

            ushort startAddress;
            ushort numberOfRegisters;
            this.ChooseParamsForReadInqury(enquryType, out startAddress, out numberOfRegisters);

            byte[] buffer = new byte[8];
            int byteIndex = 0;

            if (mainForm.CommunicationComboBox == ProtocolGlobals.ETHERNET_TAG)
            {
                buffer = new byte[12];
                buffer[byteIndex++] = 0x00;
                buffer[byteIndex++] = 0x00; //
                buffer[byteIndex++] = 0x00;
                buffer[byteIndex++] = 0x00; //
                buffer[byteIndex++] = 0x00;
                buffer[byteIndex++] = 0x06; //
            }
            byte DeviceNumber = Convert.ToByte(mainForm.DeviceNumberTextBox);
            if (enquryType >= EnquryType.INQ_SET_UPP_CONFIG && enquryType <= EnquryType.INQ_SET_UPP_I_OUT) DeviceNumber = 119;
            buffer[byteIndex++] = DeviceNumber;
            buffer[byteIndex++] = readRegister_FC;
            byte[] byteArray = BitConverter.GetBytes(startAddress);
            buffer[byteIndex++] = byteArray[1];
            buffer[byteIndex++] = byteArray[0];
            byteArray = BitConverter.GetBytes(numberOfRegisters);
            buffer[byteIndex++] = byteArray[1];
            buffer[byteIndex++] = byteArray[0];

            if (mainForm.CommunicationComboBox == ProtocolGlobals.RS485_TAG)
            {
                ushort crc = base.CalculateCRC16(buffer);
                byteArray = BitConverter.GetBytes(crc);
                buffer[byteIndex++] = byteArray[1];
                buffer[byteIndex++] = byteArray[0];
            }

            StoredEnquryType = enquryType;

            if (mainForm.CommunicationComboBox == ProtocolGlobals.RS485_TAG)
            {
                base.SendMessage(mainForm.ComPortLink, mainForm.ComPortsComboBox, buffer);
            }
            else if (mainForm.CommunicationComboBox == ProtocolGlobals.ETHERNET_TAG)
            {
                base.SendMessageTCP(mainForm.tcpClient, buffer);
            }
        }

        private void ChooseParamsForReadInqury(EnquryType enquryType, out ushort startAddress, out ushort numberOfRegisters)
        {
            startAddress = 0;
            numberOfRegisters = 0;

            switch (enquryType)
            {
                case EnquryType.INQ_READ_DIP_VOLTAGE_PKE:
                    {
                        startAddress = 0x3000;
                        numberOfRegisters = 30;
                        break;
                    }
                case EnquryType.INQ_READ_NO_VOLTAGE_PKE:
                    {
                        startAddress = 0x3100;
                        numberOfRegisters = 6;
                        break;
                    }
                case EnquryType.INQ_READ_OVER_VOLTAGE_PKE:
                    {
                        startAddress = 0x3132;
                        numberOfRegisters = 1;
                        break;
                    }
                case EnquryType.INQ_READ_DAYLIGHT:
                    {
                        startAddress = 0x0274;
                        numberOfRegisters = 1;
                        break;
                    }
                case EnquryType.INQ_READ_TARIF0:
                    {
                        startAddress = 0x1500;
                        numberOfRegisters = 20;
                        break;
                    }
                case EnquryType.INQ_READ_TARIF1:
                    {
                        startAddress = 0x1530;
                        numberOfRegisters = 20;
                        break;
                    }
                case EnquryType.INQ_READ_TARIF2:
                    {
                        startAddress = 0x1560;
                        numberOfRegisters = 20;
                        break;
                    }
                case EnquryType.INQ_READ_TARIF3:
                    {
                        startAddress = 0x1590;
                        numberOfRegisters = 20;
                        break;
                    }
                case EnquryType.INQ_READ_TARIF4:
                    {
                        startAddress = 0x15BA;
                        numberOfRegisters = 20;
                        break;
                    }
                case EnquryType.INQ_READ_TARIF5:
                    {
                        startAddress = 0x15E4;
                        numberOfRegisters = 20;
                        break;
                    }
                case EnquryType.INQ_READ_TARIF6:
                    {
                        startAddress = 0x160E;
                        numberOfRegisters = 20;
                        break;
                    }
                case EnquryType.INQ_READ_TARIF7:
                    {
                        startAddress = 0x1638;
                        numberOfRegisters = 20;
                        break;
                    }
                case EnquryType.INQ_READ_TARIF8:
                    {
                        startAddress = 0x1662;
                        numberOfRegisters = 20;
                        break;
                    }
                case EnquryType.INQ_READ_Q_CALC_METHOD:
                    {
                        startAddress = 0x0226;
                        numberOfRegisters = 1;
                        break;
                    }
                case EnquryType.INQ_READ_PARAMS_FOR_ADDSETTINGS_1:
                    {
                        startAddress = 0x0202;
                        numberOfRegisters = 11;
                        break;
                    }
                case EnquryType.INQ_READ_PARAMS_FOR_ADDSETTINGS_2:
                    {
                        startAddress = 0x0270;
                        numberOfRegisters = 1;
                        break;
                    }
                case EnquryType.INQ_READ_PARAMS_FOR_ADDSETTINGS_3:
                    {
                        startAddress = 0x0272;
                        numberOfRegisters = 1;
                        break;
                    }
                case EnquryType.INQ_READ_PKE_INSTANT:
                    {
                        startAddress = 0x0600;
                        numberOfRegisters = 9;
                        break;
                    }
                case EnquryType.INQ_READ_HARMONIC_VA_INSTANT:
                    {
                        startAddress = 0x2000;
                        numberOfRegisters = 50;
                        break;
                    }
                case EnquryType.INQ_READ_HARMONIC_VB_INSTANT:
                    {
                        startAddress = 0x2100;
                        numberOfRegisters = 50;
                        break;
                    }
                case EnquryType.INQ_READ_HARMONIC_VC_INSTANT:
                    {
                        startAddress = 0x2200;
                        numberOfRegisters = 50;
                        break;
                    }
                case EnquryType.INQ_READ_HARMONIC_IA_INSTANT:
                    {
                        startAddress = 0x2300;
                        numberOfRegisters = 50;
                        break;
                    }
                case EnquryType.INQ_READ_HARMONIC_IB_INSTANT:
                    {
                        startAddress = 0x2400;
                        numberOfRegisters = 50;
                        break;
                    }
                case EnquryType.INQ_READ_HARMONIC_IC_INSTANT:
                    {
                        startAddress = 0x2500;
                        numberOfRegisters = 50;
                        break;
                    }
                case EnquryType.INQ_READ_INTERHARMONIC_VA_INSTANT:
                    {
                        startAddress = 0x2600;
                        numberOfRegisters = 50;
                        break;
                    }
                case EnquryType.INQ_READ_INTERHARMONIC_VB_INSTANT:
                    {
                        startAddress = 0x2700;
                        numberOfRegisters = 50;
                        break;
                    }
                case EnquryType.INQ_READ_INTERHARMONIC_VC_INSTANT:
                    {
                        startAddress = 0x2800;
                        numberOfRegisters = 50;
                        break;
                    }
                case EnquryType.INQ_READ_INTERHARMONIC_IA_INSTANT:
                    {
                        startAddress = 0x2900;
                        numberOfRegisters = 50;
                        break;
                    }
                case EnquryType.INQ_READ_INTERHARMONIC_IB_INSTANT:
                    {
                        startAddress = 0x2A00;
                        numberOfRegisters = 50;
                        break;
                    }
                case EnquryType.INQ_READ_INTERHARMONIC_IC_INSTANT:
                    {
                        startAddress = 0x2B00;
                        numberOfRegisters = 50;
                        break;
                    }
                case EnquryType.INQ_READ_ANALOG_OUTS_ROWS:
                    {
                        startAddress = 0x0500;
                        numberOfRegisters = 3;
                        break;
                    }
                case EnquryType.INQ_READ_COEFS_THEN_PARAMS:
                case EnquryType.INQ_READ_COEFS_FOR_CRCRB: // запрос на считыввание коэф трансформации, а потом параметров
                    {
                        startAddress = 0x0800;
                        numberOfRegisters = 8;
                        break;
                    }
                case EnquryType.INQ_READ_31_PARAMS:
                    {
                        startAddress = 0x0000;
                        numberOfRegisters = 62;
                        break;
                    }
                case EnquryType.INQ_READ_31_PARAMS_AND_ENERGY:
                    {
                        startAddress = 0x0000;
                        numberOfRegisters = 82;
                        break;
                    }
                case EnquryType.INQ_READ_UPP_ANGLE_COEFS:
                    {
                        startAddress = 3000;
                        numberOfRegisters = 12; // 49
                        break;
                    }
                case EnquryType.INQ_READ_UPP_OTHER_COEFS:
                    {
                        startAddress = 3024;
                        numberOfRegisters = 42; // 49
                        break;
                    }
                case EnquryType.INQ_READ_31_PARAMS_FROM_UPP:
                    {
                        startAddress = 996;
                        numberOfRegisters = 64;
                        break;
                    }
                case EnquryType.INQ_READ_MAIN_CONFIG_PARAMS:
                    {
                        startAddress = 0x0200;
                        numberOfRegisters = 17;
                        break;
                    }
                case EnquryType.INQ_READ_IEC_101_PARAMS:
                    {
                        startAddress = 0x0A00;
                        numberOfRegisters = 4;
                        break;
                    }
                case EnquryType.INQ_READ_IEC_104_PARAMS:
                    {
                        startAddress = 0x0A0C;
                        numberOfRegisters = 8;
                        break;
                    }
                case EnquryType.INQ_READ_IEC_101_VALUES:
                    {
                        startAddress = 0x0A08;
                        numberOfRegisters = 2;
                        break;
                    }
                case EnquryType.INQ_READ_IEC_104_VALUES:
                    {
                        startAddress = 0x0A20;
                        numberOfRegisters = 2;
                        break;
                    }
                case EnquryType.INQ_READ_MODBUS_PARAMS:
                    {
                        startAddress = 0x0224;
                        numberOfRegisters = 1;
                        break;
                    }
                case EnquryType.INQ_READ_RELAY_1_2_PARAMS:
                    {
                        startAddress = 0x022A;
                        numberOfRegisters = 8;
                        break;
                    }
                case EnquryType.INQ_READ_RELAY_3_PARAMS:
                    {
                        startAddress = 0x0246;
                        numberOfRegisters = 4;
                        break;
                    }
                case EnquryType.INQ_READ_RELAY_1_2_GISTEREZIS:
                    {
                        startAddress = 0x023A;
                        numberOfRegisters = 4;
                        break;
                    }
                case EnquryType.INQ_READ_RELAY_3_GISTEREZIS:
                    {
                        startAddress = 0x0242;
                        numberOfRegisters = 2;
                        break;
                    }
                case EnquryType.INQ_READ_ENERGY_ROWS:
                    {
                        startAddress = 0x0412;
                        numberOfRegisters = 2;
                        break;
                    }
                case EnquryType.INQ_READ_INDICATION_INFO:
                    {
                        startAddress = 0x0206;
                        numberOfRegisters = 5;
                        break;
                    }
                case EnquryType.INQ_READ_DISPLAY_ROWS:
                    {
                        startAddress = 0x0400;
                        if (mainForm.NumberOfRowsComboBox == 0) numberOfRegisters = 6;
                        else if (mainForm.NumberOfRowsComboBox == 1) numberOfRegisters = 9;
                        break;
                    }
                case EnquryType.INQ_READ_DISPLAY_USTAVKI:
                    {
                        startAddress = 0x0B00;
                        if (!mainForm.Row4IndParamComboBoxEnabled) numberOfRegisters = 6;
                        else if (mainForm.Row4IndParamComboBoxEnabled) numberOfRegisters = 12;
                        break;
                    }
                case EnquryType.INQ_READ_COEFS_AND_MEASURE_PARAMS:
                    {
                        startAddress = 0x0800;
                        numberOfRegisters = 8;
                        break;
                    }
                case EnquryType.INQ_READ_MEASURE_PARAM:
                case EnquryType.INQ_READ_MEASURE_SCHEME_THEN_PARAMS:
                    {
                        startAddress = 0x0210;
                        numberOfRegisters = 1;
                        break;
                    }
                case EnquryType.INQ_READ_TYPE_IB:
                    {
                        startAddress = 0x0222;
                        numberOfRegisters = 1;
                        break;
                    }
                case EnquryType.INQ_READ_IMPULSE_OUTS:
                    {
                        startAddress = 0x0218;
                        numberOfRegisters = 1;
                        break;
                    }
                case EnquryType.INQ_READ_IMP_WTH:
                    {
                        startAddress = 0x0810;
                        numberOfRegisters = 2;
                        break;
                    }
                case EnquryType.INQ_READ_DATE_TIME:
                    {
                        startAddress = 0x0900;
                        numberOfRegisters = 7;
                        break;
                    }
                case EnquryType.INQ_READ_ANALOG_OUT_THEN_CHANGE:
                case EnquryType.INQ_READ_ANALOG_OUT_THEN_ROWS:
                    {
                        startAddress = 0x0212;
                        numberOfRegisters = 1;
                        break;
                    }
                case EnquryType.INQ_READ_AUTOLOOP:
                    {
                        startAddress = 0x0214;
                        numberOfRegisters = 1;
                        break;
                    }
                case EnquryType.INQ_READ_DEVICE_MODEL:
                    {
                        startAddress = 0x0216;
                        numberOfRegisters = 1;
                        break;
                    }
                case EnquryType.INQ_READ_DEVICE_ADDR:
                    {
                        startAddress = 0x0216;
                        numberOfRegisters = 1;
                        break;
                    }
                case EnquryType.INQ_READ_CHECK_SPEED_A:
                    {
                        startAddress = 0x020C;
                        numberOfRegisters = 1;
                        break;
                    }
                case EnquryType.INQ_READ_ETHERNET_CONFIG:
                    {
                        startAddress = 0x0250;
                        numberOfRegisters = 6;
                        break;
                    }

            }
        }

        private void ProcessReadPackage(byte[] buffer)
        {
            if (mainForm.InvokeRequired)
            {
                mainForm.BeginInvoke(new Delegate(ProcessReadPackage), new object[] { buffer });
                return;
            }

            switch (storedEnquryType)
            {
                case EnquryType.INQ_READ_DEVICE_ADDR:
                    {
                        noAnswer_Timer.Interval = 2000;
                        break;
                    }
                case EnquryType.INQ_READ_DIP_VOLTAGE_PKE:
                    {
                        DecodeDipVoltagePKE(buffer);
                        mainForm.StartAutoInquryTimer(EnquryType.INQ_READ_NO_VOLTAGE_PKE, 50);
                        break;
                    }
                case EnquryType.INQ_READ_NO_VOLTAGE_PKE:
                    {
                        DecodeNoVoltagePKE(buffer);
                        mainForm.StartAutoInquryTimer(EnquryType.INQ_READ_OVER_VOLTAGE_PKE, 50);
                        break;
                    }
                case EnquryType.INQ_READ_OVER_VOLTAGE_PKE:
                    {
                        DecodeOverVoltagePKE(buffer);
                        mainForm.Read31ParamButton = Globals.READ_BUTTON_TAG;
                        break;
                    }
                case EnquryType.INQ_READ_DAYLIGHT:
                    {
                        mainForm.DayLightComboBox = (int)ConvertByteToUInt16(buffer, 3);
                        break;
                    }
                case EnquryType.INQ_READ_HARMONIC_VA_INSTANT:
                    {
                        DecodeHarmonics(buffer, 0, 0);
                        mainForm.StartAutoInquryTimer(EnquryType.INQ_READ_HARMONIC_VB_INSTANT, 50);
                        break;
                    }
                case EnquryType.INQ_READ_HARMONIC_VB_INSTANT:
                    {
                        DecodeHarmonics(buffer, 1, 0);
                        mainForm.StartAutoInquryTimer(EnquryType.INQ_READ_HARMONIC_VC_INSTANT, 50);
                        break;
                    }
                case EnquryType.INQ_READ_HARMONIC_VC_INSTANT:
                    {
                        DecodeHarmonics(buffer, 2, 0);
                        if (mainForm.AutoLoopIsOn)
                        {
                            long interval = 50;
                            try
                            {
                                interval = Convert.ToInt64(mainForm.PollPeriodPKETextBox) - stopWatch.ElapsedMilliseconds;
                                if (interval < 50) interval = 50;
                            }
                            catch { }
                            mainForm.StartAutoInquryTimer(EnquryType.INQ_READ_PKE_INSTANT, interval);
                        }
                        else
                            mainForm.Read31ParamButton = Globals.READ_BUTTON_TAG;
                        break;
                    }
                case EnquryType.INQ_READ_HARMONIC_IA_INSTANT:
                    {
                        DecodeHarmonics(buffer, 0, 1);
                        mainForm.StartAutoInquryTimer(EnquryType.INQ_READ_HARMONIC_IB_INSTANT, 50);
                        break;
                    }
                case EnquryType.INQ_READ_HARMONIC_IB_INSTANT:
                    {
                        DecodeHarmonics(buffer, 1, 1);
                        mainForm.StartAutoInquryTimer(EnquryType.INQ_READ_HARMONIC_IC_INSTANT, 50);
                        break;
                    }
                case EnquryType.INQ_READ_HARMONIC_IC_INSTANT:
                    {
                        DecodeHarmonics(buffer, 2, 1);
                        if (mainForm.AutoLoopIsOn)
                        {
                            long interval = 50;
                            try
                            {
                                interval = Convert.ToInt64(mainForm.PollPeriodPKETextBox) - stopWatch.ElapsedMilliseconds;
                                if (interval < 50) interval = 50;
                            }
                            catch { }
                            mainForm.StartAutoInquryTimer(EnquryType.INQ_READ_PKE_INSTANT, interval);
                        }
                        else
                            mainForm.Read31ParamButton = Globals.READ_BUTTON_TAG;
                        break;
                    }
                case EnquryType.INQ_READ_INTERHARMONIC_VA_INSTANT:
                    {
                        DecodeHarmonics(buffer, 0, 2);
                        mainForm.StartAutoInquryTimer(EnquryType.INQ_READ_INTERHARMONIC_VB_INSTANT, 50);
                        break;
                    }
                case EnquryType.INQ_READ_INTERHARMONIC_VB_INSTANT:
                    {
                        DecodeHarmonics(buffer, 1, 2);
                        mainForm.StartAutoInquryTimer(EnquryType.INQ_READ_INTERHARMONIC_VC_INSTANT, 50);
                        break;
                    }
                case EnquryType.INQ_READ_INTERHARMONIC_VC_INSTANT:
                    {
                        DecodeHarmonics(buffer, 2, 2);
                        if (mainForm.AutoLoopIsOn)
                        {
                            long interval = 50;
                            try
                            {
                                interval = Convert.ToInt64(mainForm.PollPeriodPKETextBox) - stopWatch.ElapsedMilliseconds;
                                if (interval < 50) interval = 50;
                            }
                            catch { }
                            mainForm.StartAutoInquryTimer(EnquryType.INQ_READ_PKE_INSTANT, interval);
                        }
                        else
                            mainForm.Read31ParamButton = Globals.READ_BUTTON_TAG;
                        break;
                    }
                case EnquryType.INQ_READ_INTERHARMONIC_IA_INSTANT:
                    {
                        DecodeHarmonics(buffer, 0, 3);
                        mainForm.StartAutoInquryTimer(EnquryType.INQ_READ_INTERHARMONIC_IB_INSTANT, 50);
                        break;
                    }
                case EnquryType.INQ_READ_INTERHARMONIC_IB_INSTANT:
                    {
                        DecodeHarmonics(buffer, 1, 3);
                        mainForm.StartAutoInquryTimer(EnquryType.INQ_READ_INTERHARMONIC_IC_INSTANT, 50);
                        break;
                    }
                case EnquryType.INQ_READ_INTERHARMONIC_IC_INSTANT:
                    {
                        DecodeHarmonics(buffer, 2, 3);
                        if (mainForm.AutoLoopIsOn)
                        {
                            long interval = 50;
                            try
                            {
                                interval = Convert.ToInt64(mainForm.PollPeriodPKETextBox) - stopWatch.ElapsedMilliseconds;
                                if (interval < 50) interval = 50;
                            }
                            catch { }
                            mainForm.StartAutoInquryTimer(EnquryType.INQ_READ_PKE_INSTANT, interval);
                        }
                        else
                            mainForm.Read31ParamButton = Globals.READ_BUTTON_TAG;
                        break;
                    }
                case EnquryType.INQ_READ_PKE_INSTANT:
                    {
                        DecodeInstantPKE(buffer);

                        mainForm.ColorStatusButton = Color.Lime;

                        inquresCounter++; // увеличиваем счетчик запросов
                        mainForm.InquresCounterTextBox = inquresCounter.ToString();

                        mainForm.AnswerTimeTextBox = stopWatch.ElapsedMilliseconds.ToString() + " мс"; // выводим время ответа

                        if (stopWatch.ElapsedMilliseconds > maxAnswerTime) // выводим максимальное время ответа
                        {
                            maxAnswerTime = stopWatch.ElapsedMilliseconds;
                            mainForm.MaxAnswerTimeTextBox = maxAnswerTime.ToString() + " мс";
                        }

                        int table = mainForm.GetActiveHarmonicTable();
                        if (table == 0) mainForm.StartAutoInquryTimer(EnquryType.INQ_READ_HARMONIC_VA_INSTANT, 50);
                        else if (table == 1) mainForm.StartAutoInquryTimer(EnquryType.INQ_READ_HARMONIC_IA_INSTANT, 50);
                        else if (table == 2) mainForm.StartAutoInquryTimer(EnquryType.INQ_READ_INTERHARMONIC_VA_INSTANT, 50);
                        else if (table == 3) mainForm.StartAutoInquryTimer(EnquryType.INQ_READ_INTERHARMONIC_IA_INSTANT, 50);
                        break;
                    }
                case EnquryType.INQ_READ_ANALOG_OUTS_ROWS:
                    {
                        DecodeAnalogOutsRows(buffer);
                        break;
                    }
                case EnquryType.INQ_READ_COEFS_THEN_PARAMS:
                    {
                        transformationCoefs.U1 = this.ConvertByteToFloat(buffer, 3);
                        transformationCoefs.I1 = this.ConvertByteToFloat(buffer, 11);
                        UpdatePrefix(transformationCoefs.U1, transformationCoefs.I1);

                        mainForm.StartAutoInquryTimer(EnquryType.INQ_READ_MEASURE_SCHEME_THEN_PARAMS, 50);

                        break;
                    }
                case EnquryType.INQ_READ_MEASURE_SCHEME_THEN_PARAMS:
                    {
                        storedMeasureSheme = (int)ConvertByteToUInt16(buffer, 3);

                        if (mainForm.uppConfigurator.IsConnected())
                            mainForm.StartAutoInquryTimer(EnquryType.INQ_READ_31_PARAMS_FROM_UPP, 500);
                        else
                            mainForm.StartAutoInquryTimer(EnquryType.INQ_READ_31_PARAMS, 500);
                        break;
                    }
                case EnquryType.INQ_READ_31_PARAMS:
                    {
                        if (mainForm.MainTabControlSelectedIndex == 2 && mainForm.AutoLoopIsOn)
                        {
                            for (int i = 0; i <= 100; i++)
                            {
                                mainForm.MainProgressBar = i;
                                if (i % 10 == 0) Thread.Sleep(1);
                            }
                            Decode31ParamsForChange(buffer);
                            mainForm.StartAutoInquryTimer(storedEnquryType, 400);
                        }
                        else
                        {
                            Decode31ParamsAndEnergy(buffer);

                            mainForm.AnswerTimeTextBox = stopWatch.ElapsedMilliseconds.ToString() + " мс"; // выводим время ответа

                            if (stopWatch.ElapsedMilliseconds > maxAnswerTime) // выводим максимальное время ответа
                            {
                                maxAnswerTime = stopWatch.ElapsedMilliseconds;
                                mainForm.MaxAnswerTimeTextBox = maxAnswerTime.ToString() + " мс";
                            }

                            inquresCounter++; // увеличиваем счетчик запросов
                            mainForm.InquresCounterTextBox = inquresCounter.ToString();

                            mainForm.ColorStatusButton = Color.Lime;

                            if (mainForm.EnergyChooseCheckBox)
                            {
                                if (mainForm.TarifsComboBox == 0) mainForm.StartAutoInquryTimer(EnquryType.INQ_READ_TARIF0, 50);
                                else if (mainForm.TarifsComboBox == 1) mainForm.StartAutoInquryTimer(EnquryType.INQ_READ_TARIF1, 50);
                                else if (mainForm.TarifsComboBox == 2) mainForm.StartAutoInquryTimer(EnquryType.INQ_READ_TARIF2, 50);
                                else if (mainForm.TarifsComboBox == 3) mainForm.StartAutoInquryTimer(EnquryType.INQ_READ_TARIF3, 50);
                                else if (mainForm.TarifsComboBox == 4) mainForm.StartAutoInquryTimer(EnquryType.INQ_READ_TARIF4, 50);
                                else if (mainForm.TarifsComboBox == 5) mainForm.StartAutoInquryTimer(EnquryType.INQ_READ_TARIF5, 50);
                                else if (mainForm.TarifsComboBox == 6) mainForm.StartAutoInquryTimer(EnquryType.INQ_READ_TARIF6, 50);
                                else if (mainForm.TarifsComboBox == 7) mainForm.StartAutoInquryTimer(EnquryType.INQ_READ_TARIF7, 50);
                                else if (mainForm.TarifsComboBox == 8) mainForm.StartAutoInquryTimer(EnquryType.INQ_READ_TARIF8, 50);
                            }
                            else
                            {
                                if (mainForm.AutoLoopIsOn)
                                {
                                    long interval = 50;
                                    try
                                    {
                                        interval = Convert.ToInt64(mainForm.PollPeriodTextBox) - stopWatch.ElapsedMilliseconds;
                                        if (interval < 50) interval = 50;
                                    }
                                    catch { }
                                    mainForm.StartAutoInquryTimer(storedEnquryType, interval);
                                }
                                else
                                    mainForm.Read31ParamButton = Globals.READ_BUTTON_TAG;
                            }
                        }

                        break;
                    }
                case EnquryType.INQ_READ_31_PARAMS_FROM_UPP:
                    {
                        for (int i = 0; i <= 100; i++)
                        {
                            mainForm.MainProgressBar = i;
                            if (i % 10 == 0) Thread.Sleep(1);
                        }
                        byte[] buff2 = new byte[buffer.Length - 4];
                        Array.Copy(buffer, 4, buff2, 0, buff2.Length);
                        Decode31ParamsForChange(buff2);
                        mainForm.uppConfigurator.mA_label.Text = this.ConvertByteToFloat(buffer, 3).ToString("N3");

                        if (mainForm.uppConfigurator.IsConnected())
                        {
                            if (mainForm.uppConfigurator.StopPoll)
                                mainForm.uppConfigurator.PollStoped = true;
                            else
                                mainForm.StartAutoInquryTimer(EnquryType.INQ_READ_31_PARAMS_FROM_UPP, 400);
                        }

                        break;
                    }
                case EnquryType.INQ_READ_TARIF0:
                case EnquryType.INQ_READ_TARIF1:
                case EnquryType.INQ_READ_TARIF2:
                case EnquryType.INQ_READ_TARIF3:
                case EnquryType.INQ_READ_TARIF4:
                case EnquryType.INQ_READ_TARIF5:
                case EnquryType.INQ_READ_TARIF6:
                case EnquryType.INQ_READ_TARIF7:
                case EnquryType.INQ_READ_TARIF8:
                    {
                        DecodeEnergy(buffer);

                        if (mainForm.AutoLoopIsOn)
                        {
                            long interval = 50;
                            try
                            {
                                interval = Convert.ToInt64(mainForm.PollPeriodTextBox) - stopWatch.ElapsedMilliseconds;
                                if (interval < 50) interval = 50;
                            }
                            catch { }
                            mainForm.StartAutoInquryTimer(EnquryType.INQ_READ_31_PARAMS, interval);
                        }
                        else
                            mainForm.Read31ParamButton = Globals.READ_BUTTON_TAG;
                        break;
                    }
                case EnquryType.INQ_READ_MAIN_CONFIG_PARAMS:
                    {
                        DecodeMainConfigParams(buffer);

                        mainForm.MainProgressBar = 20;
                        mainForm.StartAutoInquryTimer(EnquryType.INQ_READ_MODBUS_PARAMS, 50);
                        break;
                    }
                case EnquryType.INQ_READ_RELAY_1_2_PARAMS:
                    {
                        DecodeRelay_1_2_Params(buffer);

                        mainForm.MainProgressBar = 40;
                        mainForm.StartAutoInquryTimer(EnquryType.INQ_READ_RELAY_1_2_GISTEREZIS, 50);
                        break;
                    }
                case EnquryType.INQ_READ_RELAY_1_2_GISTEREZIS:
                    {
                        DecodeRelay_1_2_Gistereziss(buffer);

                        mainForm.MainProgressBar = 60;
                        if (_typeOfPribor >= 711)
                            mainForm.StartAutoInquryTimer(EnquryType.INQ_READ_RELAY_3_PARAMS, 50);
                        else
                            mainForm.StartAutoInquryTimer(EnquryType.INQ_READ_ETHERNET_CONFIG, 50);
                        break;
                    }
                case EnquryType.INQ_READ_RELAY_3_PARAMS:
                    {
                        DecodeRelay_3_Params(buffer);

                        mainForm.MainProgressBar = 65;
                        mainForm.StartAutoInquryTimer(EnquryType.INQ_READ_RELAY_3_GISTEREZIS, 50);
                        break;
                    }
                case EnquryType.INQ_READ_RELAY_3_GISTEREZIS:
                    {
                        DecodeRelay_3_Gistereziss(buffer);

                        mainForm.MainProgressBar = 75;
                        mainForm.StartAutoInquryTimer(EnquryType.INQ_READ_ETHERNET_CONFIG, 50);
                        break;
                    }
                case EnquryType.INQ_READ_MODBUS_PARAMS:
                    {
                        DecodeModbusParams(buffer);

                        mainForm.MainProgressBar = 30;
                        mainForm.StartAutoInquryTimer(EnquryType.INQ_READ_Q_CALC_METHOD, 50);
                        break;
                    }
                case EnquryType.INQ_READ_IEC_101_PARAMS:
                    {
                        DecodeIEC101Params(buffer);
                        mainForm.StartAutoInquryTimer(EnquryType.INQ_READ_IEC_101_VALUES, 50);
                        break;
                    }
                case EnquryType.INQ_READ_IEC_101_VALUES:
                    {
                        DecodeIEC101Values(buffer);
                        break;
                    }
                case EnquryType.INQ_READ_IEC_104_PARAMS:
                    {
                        DecodeIEC104Params(buffer);
                        mainForm.StartAutoInquryTimer(EnquryType.INQ_READ_IEC_104_VALUES, 50);
                        break;
                    }
                case EnquryType.INQ_READ_IEC_104_VALUES:
                    {
                        DecodeIEC104Values(buffer);
                        break;
                    }
                case EnquryType.INQ_READ_Q_CALC_METHOD:
                    {
                        mainForm.QCalcMethodBox = (int)ConvertByteToUInt16(buffer, 3);

                        mainForm.MainProgressBar = 40;
                        if (mainForm.RelayGroupBoxEnabled)
                            mainForm.StartAutoInquryTimer(EnquryType.INQ_READ_RELAY_1_2_PARAMS, 50);
                        else
                            mainForm.StartAutoInquryTimer(EnquryType.INQ_READ_ETHERNET_CONFIG, 50);

                        break;
                    }
                case EnquryType.INQ_READ_ETHERNET_CONFIG:
                    {
                        DecodeIPAddress(buffer);
                        mainForm.MainProgressBar = 100;
                        break;
                    }
                case EnquryType.INQ_READ_ENERGY_ROWS:
                    {
                        DecodeEnergyRows(buffer);

                        break;
                    }
                case EnquryType.INQ_READ_INDICATION_INFO:
                    {
                        DecodeIndicationInfo(buffer);

                        mainForm.MainProgressBar = 33;
                        mainForm.StartAutoInquryTimer(EnquryType.INQ_READ_DISPLAY_ROWS, 50);
                        break;
                    }
                case EnquryType.INQ_READ_DISPLAY_ROWS:
                    {
                        DecodeDisplayedRows(buffer);

                        mainForm.MainProgressBar = 66;
                        mainForm.StartAutoInquryTimer(EnquryType.INQ_READ_DISPLAY_USTAVKI, 50);
                        break;
                    }
                case EnquryType.INQ_READ_DISPLAY_USTAVKI:
                    {
                        DecodeDisplayedUstavki(buffer);

                        mainForm.MainProgressBar = 100;
                        break;
                    }
                case EnquryType.INQ_READ_COEFS_AND_MEASURE_PARAMS:
                    {
                        DecodeCoefs(buffer);

                        mainForm.MainProgressBar = 33;
                        mainForm.StartAutoInquryTimer(EnquryType.INQ_READ_MEASURE_PARAM, 50);
                        break;
                    }
                case EnquryType.INQ_READ_MEASURE_PARAM:
                    {
                        storedMeasureSheme = (int)ConvertByteToUInt16(buffer, 3);
                        if (storedMeasureSheme == 0) storedMeasureSheme = 1;
                        else storedMeasureSheme = 0;
                        mainForm.MeasureSchemeComboBox = storedMeasureSheme;

                        if (storedMeasureSheme == (int)MeasureScheme._3_WIRED)
                        {
                            mainForm.IbShiftButtons = true;
                        }
                        else
                        {
                            mainForm.IbShiftButtons = false;
                        }

                        mainForm.MainProgressBar = 66;
                        mainForm.StartAutoInquryTimer(EnquryType.INQ_READ_TYPE_IB, 50);
                        break;
                    }
                case EnquryType.INQ_READ_TYPE_IB:
                    {
                        mainForm.IbComboBox = (int)ConvertByteToUInt16(buffer, 3);

                        mainForm.MainProgressBar = 80;
                        mainForm.StartAutoInquryTimer(EnquryType.INQ_READ_DEVICE_MODEL, 50);
                        break;
                    }
                case EnquryType.INQ_READ_DEVICE_MODEL:
                    {
                        ushort typeOfPribor = ConvertByteToUInt16(buffer, 3);
                        _typeOfPribor = typeOfPribor;
                        if (mainForm.AutoLoopIsOn)
                        {
                            if (typeOfPribor == 711 || typeOfPribor == 712)
                                mainForm.NoiseGroupBoxEnabled = false;
                            else
                                mainForm.NoiseGroupBoxEnabled = true;

                            mainForm.MainProgressBar = 100;

                            mainForm.ParamsGroupBoxVisible = true;
                            mainForm.SaveChangedParamsButton = true;
                            mainForm.StartAutoInquryTimer(EnquryType.INQ_READ_31_PARAMS, 500);
                        }
                        else
                        {
                            foreach (Form f in Application.OpenForms)
                            {
                                if (f.Name == "IEC_SettingsForm")
                                {
                                    // mainForm.IECSettings.save_101();
                                    break;
                                }
                            }
                        }
                        break;
                    }
                case EnquryType.INQ_READ_IMPULSE_OUTS:
                    {
                        DecodeImpulseOuts(buffer);
                        break;
                    }
                case EnquryType.INQ_READ_IMP_WTH:
                    {
                        DecodeImpulseWtH(buffer);
                        break;
                    }
                case EnquryType.INQ_READ_DATE_TIME:
                    {
                        DecodeDateTime(buffer);
                        break;
                    }
                case EnquryType.INQ_READ_ANALOG_OUT_THEN_ROWS:
                    {
                        mainForm.AnalogOutsTypeComboBox = (int)ConvertByteToUInt16(buffer, 3);

                        mainForm.StartAutoInquryTimer(EnquryType.INQ_READ_ANALOG_OUTS_ROWS, 50);
                        break;
                    }
                case EnquryType.INQ_READ_ANALOG_OUT_THEN_CHANGE:
                    {
                        mainForm.AnalogOutsTypeComboBox = (int)ConvertByteToUInt16(buffer, 3);

                        ushort numberofCheckedRadioButton = (ushort)mainForm.AnalogOutsRadioButtons;
                        byte[] bytes = BitConverter.GetBytes(numberofCheckedRadioButton);
                        Thread.Sleep(50);
                        this.WriteData(Modbus.EnquryType.INQ_WRITE_CURRENT_MODE, bytes);
                        break;
                    }
                case EnquryType.INQ_READ_AUTOLOOP:
                    {
                        if (mainForm.AutoLoopIsOn)
                        {
                            for (int i = 0; i <= 100; i++)
                            {
                                mainForm.MainProgressBar = i;
                                if (i % 10 == 0) Thread.Sleep(1);
                            }
                            mainForm.StartAutoInquryTimer(EnquryType.INQ_READ_AUTOLOOP, 400);
                        }
                        break;
                    }
                case EnquryType.INQ_READ_CHECK_SPEED_A:
                    {
                        //MessageBox.Show("Информация записана");
                        // mainForm.StatusLabel = "Информация записана";
                        mainForm.ShowMessageBox(ProtocolGlobals.RECORDING_DONE_MESSAGE);
                        break;
                    }
                case EnquryType.INQ_READ_SET_DATE_TIME_JOURNAL:
                case EnquryType.INQ_READ_CLOCK_CHECK_DT_JOURNAL:
                case EnquryType.INQ_READ_JUMPER_DT_JOURNAL:
                case EnquryType.INQ_READ_ERASE_LOG_DT_JOURNAL:
                case EnquryType.INQ_READ_CURRENT_WITHOUT_VOLTAGE_DT_JOURNAL:
                case EnquryType.INQ_READ_CHANGE_COEFS_DT_JOURNAL:
                case EnquryType.INQ_READ_TURN_ON_OFF_DT_JOURNAL:
                case EnquryType.INQ_READ_TARIFS_CHANGE:
                case EnquryType.INQ_READ_SECONDS_DIFFERENCE_JOURNAL:
                case EnquryType.INQ_READ_KU0_JOURNAL:
                case EnquryType.INQ_READ_KU2_JOURNAL:
                case EnquryType.INQ_READ_dF_JOURNAL:
                case EnquryType.INQ_READ_dUPLUS_JOURNAL:
                case EnquryType.INQ_READ_dUMINUS_JOURNAL:
                case EnquryType.INQ_READ_CLEAR_ENERGY_JOURNAL:
                    {
                        if (mainForm.AutoLoopIsOn)
                        {
                            if (storedEnquryType == EnquryType.INQ_READ_SET_DATE_TIME_JOURNAL)
                                DecodeSetDTJournal(buffer);
                            else if (storedEnquryType == EnquryType.INQ_READ_CLOCK_CHECK_DT_JOURNAL)
                                DecodeCJECJournal(buffer, EnquryType.INQ_READ_CLOCK_CHECK_DT_JOURNAL);
                            else if (storedEnquryType == EnquryType.INQ_READ_JUMPER_DT_JOURNAL)
                                DecodeCJECJournal(buffer, EnquryType.INQ_READ_JUMPER_DT_JOURNAL);
                            else if (storedEnquryType == EnquryType.INQ_READ_ERASE_LOG_DT_JOURNAL)
                                DecodeCJECJournal(buffer, EnquryType.INQ_READ_ERASE_LOG_DT_JOURNAL);
                            else if (storedEnquryType == EnquryType.INQ_READ_CURRENT_WITHOUT_VOLTAGE_DT_JOURNAL)
                                DecodeCJECJournal(buffer, EnquryType.INQ_READ_CURRENT_WITHOUT_VOLTAGE_DT_JOURNAL);
                            else if (storedEnquryType == EnquryType.INQ_READ_CHANGE_COEFS_DT_JOURNAL)
                                DecodeChangeCoefsJournal(buffer);
                            else if (storedEnquryType == EnquryType.INQ_READ_TURN_ON_OFF_DT_JOURNAL)
                                DecodeTurnOnOffJournal(buffer);
                            else if (storedEnquryType == EnquryType.INQ_READ_TARIFS_CHANGE)
                                DecodeTarifsChange(buffer);
                            else if (storedEnquryType == EnquryType.INQ_READ_SECONDS_DIFFERENCE_JOURNAL)
                                DecodeSecondsDifereceJournal(buffer);
                            else if (storedEnquryType == EnquryType.INQ_READ_CLEAR_ENERGY_JOURNAL)
                                DecodeCJECJournal(buffer, EnquryType.INQ_READ_CLEAR_ENERGY_JOURNAL);
                            else if (storedEnquryType == EnquryType.INQ_READ_KU0_JOURNAL
                                || storedEnquryType == EnquryType.INQ_READ_KU2_JOURNAL
                                || storedEnquryType == EnquryType.INQ_READ_dF_JOURNAL
                                || storedEnquryType == EnquryType.INQ_READ_dUPLUS_JOURNAL
                                || storedEnquryType == EnquryType.INQ_READ_dUMINUS_JOURNAL)
                                DecodeKU0Journal(buffer);



                            mainForm.StartAutoInquryTimer(storedEnquryType, 50);
                        }
                        break;
                    }
                case EnquryType.INQ_READ_COEFS_FOR_CRCRB:
                    {
                        transformationCoefs.U1 = this.ConvertByteToFloat(buffer, 3);
                        transformationCoefs.I1 = this.ConvertByteToFloat(buffer, 11);
                        mainForm.SetEnergyPrefix(transformationCoefs.U1, transformationCoefs.I1);
                        mainForm.StartAutoInquryTimer(Modbus.EnquryType.INQ_READ_CRCRB_AUTOLOOP, 50);
                        break;
                    }
                case EnquryType.INQ_READ_PARAMS_FOR_ADDSETTINGS_1:
                    {
                        mainForm.NumberOfParamsAddSettingsTextBox = ConvertByteToUInt16(buffer, 3).ToString("");

                        string numberOfDevice = ConvertByteToUInt16(buffer, 9).ToString("D4");
                        string yearOfDevice = ConvertByteToUInt16(buffer, 11).ToString("");
                        mainForm.FactoryNumberAddSettingsTextBox = yearOfDevice + numberOfDevice;

                        ushort typeOfPribor = ConvertByteToUInt16(buffer, 23);
                        if (typeOfPribor >= 700)
                        {
                            typeOfPribor -= 700;
                            mainForm.DeviceNumberAddSettingsComboBox = typeOfPribor - 1;
                        }

                        mainForm.StartAutoInquryTimer(Modbus.EnquryType.INQ_READ_PARAMS_FOR_ADDSETTINGS_2, 50);
                        break;
                    }
                case EnquryType.INQ_READ_PARAMS_FOR_ADDSETTINGS_2:
                    {
                        ushort temp = ConvertByteToUInt16(buffer, 3);
                        mainForm.RelaysAddSettingsComboBox = temp;

                        mainForm.StartAutoInquryTimer(Modbus.EnquryType.INQ_READ_PARAMS_FOR_ADDSETTINGS_3, 50);
                        break;
                    }
                case EnquryType.INQ_READ_PARAMS_FOR_ADDSETTINGS_3:
                    {
                        ushort temp = ConvertByteToUInt16(buffer, 3);

                        mainForm.InterfacesAddSettingsComboBox = temp - 1;
                        break;
                    }
                case EnquryType.INQ_READ_UPP_ANGLE_COEFS:
                    {
                        DecodeUppAngleCoeffs(buffer);

                        mainForm.StartAutoInquryTimer(EnquryType.INQ_READ_UPP_OTHER_COEFS, 50);
                        break;
                    }
                case EnquryType.INQ_READ_UPP_OTHER_COEFS:
                    {
                        DecodeUppOtherCoeffs(buffer);

                        mainForm.ParamsGroupBoxVisible = true;
                        mainForm.EnableCorrectionModeButton = false;
                        mainForm.uppConfigurator.Connection(true);

                        mainForm.StartAutoInquryTimer(EnquryType.INQ_READ_31_PARAMS_FROM_UPP, 150);
                        break;
                    }
            }
            mainForm.StatusLabel = ProtocolGlobals.OPERATION_SUCCESS_MESSAGE;
        }



        public void WriteData(EnquryType enquryType, byte[] data)
        {
            if (mainForm.CommunicationComboBox == ProtocolGlobals.ETHERNET_TAG)
            {
                if (!base.TryToConnectTCP(mainForm.IPAddressComboBox, Modbus.Port))
                    return;
            }
            if (mainForm.DeviceNumberTextBox == "")
            {
                mainForm.ShowMessageBox(ProtocolGlobals.INCORRECT_DEVICE_ADDRESS_MESSAGE);
                return;
            }

            if (enquryType != EnquryType.INQ_SET_PASSWORD)
            {
                // сохраняем для повторной отправки после  ввода пароля
                storedDataForPassword = new byte[data.Length];
                Array.Copy(data, storedDataForPassword, data.Length);
                storedEnquryTypeForPassword = enquryType;
            }

            ushort startAddress;
            byte typeOfInqury;
            this.ChooseParamsForWriteInqury(enquryType, out startAddress, out typeOfInqury);

            byte[] buffer = new byte[6 + data.Length + ((typeOfInqury == writeRegisters_FC) ? 3 : 0)];
            byte[] byteArray;
            int byteIndex = 0;

            if (mainForm.CommunicationComboBox == ProtocolGlobals.ETHERNET_TAG)
            {
                buffer = new byte[buffer.Length + 4];
                buffer[byteIndex++] = 0x00;
                buffer[byteIndex++] = 0x00; //
                buffer[byteIndex++] = 0x00;
                buffer[byteIndex++] = 0x00; //
                byteArray = BitConverter.GetBytes(buffer.Length - 6);
                buffer[byteIndex++] = byteArray[1];
                buffer[byteIndex++] = byteArray[0];
                // buffer[byteIndex++] = (byte)data.Length;
            }

            byte DeviceNumber = Convert.ToByte(mainForm.DeviceNumberTextBox);
            if (enquryType >= EnquryType.INQ_SET_UPP_CONFIG && enquryType <= EnquryType.INQ_SET_UPP_I_OUT) DeviceNumber = 119;
            buffer[byteIndex++] = DeviceNumber;
            buffer[byteIndex++] = typeOfInqury;
            byteArray = BitConverter.GetBytes(startAddress);
            buffer[byteIndex++] = byteArray[1];
            buffer[byteIndex++] = byteArray[0];
            if (typeOfInqury == writeRegisters_FC)
            {
                byteArray = BitConverter.GetBytes(data.Length / 2);
                buffer[byteIndex++] = byteArray[1];
                buffer[byteIndex++] = byteArray[0];
                buffer[byteIndex++] = (byte)data.Length;
            }
            Array.Reverse(data);
            for (int i = 0; i < data.Length; i++)
            {
                buffer[byteIndex++] = data[i];
            }

            if (mainForm.CommunicationComboBox == ProtocolGlobals.RS485_TAG)
            {
                ushort crc = base.CalculateCRC16(buffer);
                byteArray = BitConverter.GetBytes(crc);
                buffer[byteIndex++] = byteArray[1];
                buffer[byteIndex++] = byteArray[0];
            }

            StoredEnquryType = enquryType;

            lastBuffer = new byte[buffer.Length];
            buffer.CopyTo(lastBuffer, 0);
            lastType = enquryType;

            if (mainForm.CommunicationComboBox == ProtocolGlobals.RS485_TAG)
            {
                base.SendMessage(mainForm.ComPortLink, mainForm.ComPortsComboBox, buffer);
            }
            else if (mainForm.CommunicationComboBox == ProtocolGlobals.ETHERNET_TAG)
            {
                base.SendMessageTCP(mainForm.tcpClient, buffer);
            }
        }

        private void ChooseParamsForWriteInqury(EnquryType enquryType, out ushort startAddress, out byte typeOfInqury)
        {
            startAddress = 0;
            typeOfInqury = 0;

            switch (enquryType)
            {
                case EnquryType.INQ_SAVE_CLEAR_ENERGY:
                    {
                        startAddress = 0x0324;
                        typeOfInqury = writeRegister_FC;
                        break;
                    }
                case EnquryType.INQ_SAVE_DAYLIGHT:
                    {
                        startAddress = 0x0274;
                        typeOfInqury = writeRegister_FC;
                        break;
                    }
                case EnquryType.INQ_SET_PASSWORD:
                    {
                        startAddress = 0x0C00;
                        typeOfInqury = writeRegisters_FC;
                        break;
                    }
                case EnquryType.INQ_SAVE_IMPULSE_OUTS:
                    {
                        startAddress = 0x0218;
                        typeOfInqury = writeRegister_FC;
                        break;
                    }
                case EnquryType.INQ_SAVE_Q_CALC_METHOD:
                    {
                        startAddress = 0x0226;
                        typeOfInqury = writeRegister_FC;
                        break;
                    }
                case EnquryType.INQ_SAVE_MODIFICATION:
                    {
                        startAddress = 0x0216;
                        typeOfInqury = writeRegister_FC;
                        break;
                    }
                case EnquryType.INQ_SAVE_RELAYS_NUMBER:
                    {
                        startAddress = 0x0270;
                        typeOfInqury = writeRegister_FC;
                        break;
                    }
                case EnquryType.INQ_SAVE_INTERFACES_NUMBER:
                    {
                        startAddress = 0x0272;
                        typeOfInqury = writeRegister_FC;
                        break;
                    }
                case EnquryType.INQ_SAVE_ANALOG_OUTS_ROWS:
                    {
                        startAddress = 0x0500;
                        typeOfInqury = writeRegisters_FC;
                        break;
                    }
                case EnquryType.INQ_SAVE_CLEAR_JOURNAL:
                    {
                        startAddress = 0x0326;
                        typeOfInqury = writeRegister_FC;
                        break;
                    }
                case EnquryType.INQ_SAVE_NET_ADDR:
                    {
                        startAddress = 0x0204;
                        typeOfInqury = writeRegister_FC;
                        break;
                    }
                case EnquryType.INQ_SAVE_SPEED_A:
                    {
                        startAddress = 0x020C;
                        typeOfInqury = writeRegister_FC;
                        break;
                    }
                case EnquryType.INQ_SAVE_SPEED_B:
                    {
                        startAddress = 0x021E;
                        typeOfInqury = writeRegister_FC;
                        break;
                    }
                case EnquryType.INQ_SAVE_CONFIG_B:
                    {
                        startAddress = 0x021C;
                        typeOfInqury = writeRegister_FC;
                        break;
                    }
                case EnquryType.INQ_SAVE_CUTTOFF_PERCENT:
                    {
                        startAddress = 0x021A;
                        typeOfInqury = writeRegister_FC;
                        break;
                    }
                case EnquryType.INQ_SAVE_YEAR_AND_FACTORYNUM:
                    {
                        startAddress = 0x0208;
                        typeOfInqury = writeRegisters_FC;
                        break;
                    }
                case EnquryType.INQ_SAVE_MODBUS_CONFIG:
                    {
                        startAddress = 0x0224;
                        typeOfInqury = writeRegister_FC;
                        break;
                    }
                case EnquryType.INQ_SAVE_IEC_101_CONFIG:
                    {
                        startAddress = 0x0A00;
                        typeOfInqury = writeRegisters_FC;
                        break;
                    }
                case EnquryType.INQ_SAVE_IEC_101_VALUES:
                    {
                        startAddress = 0x0A08;
                        typeOfInqury = writeRegisters_FC;
                        break;
                    }
                case EnquryType.INQ_SAVE_IEC_104_CONFIG:
                    {
                        startAddress = 0x0A0C;
                        typeOfInqury = writeRegisters_FC;
                        break;
                    }
                case EnquryType.INQ_SAVE_IEC_104_VALUES:
                    {
                        startAddress = 0x0A20;
                        typeOfInqury = writeRegisters_FC;
                        break;
                    }
                case EnquryType.INQ_SAVE_RELAY_1_CONFIG:
                    {
                        startAddress = 0x022A;
                        typeOfInqury = writeRegisters_FC;
                        break;
                    }
                case EnquryType.INQ_SAVE_RELAY_1_GISTEREZIS:
                    {
                        startAddress = 0x023A;
                        typeOfInqury = writeRegisters_FC;
                        break;
                    }
                case EnquryType.INQ_SAVE_RELAY_2_CONFIG:
                    {
                        startAddress = 0x0232;
                        typeOfInqury = writeRegisters_FC;
                        break;
                    }
                case EnquryType.INQ_SAVE_RELAY_2_GISTEREZIS:
                    {
                        startAddress = 0x023E;
                        typeOfInqury = writeRegisters_FC;
                        break;
                    }
                case EnquryType.INQ_SAVE_RELAY_3_CONFIG:
                    {
                        startAddress = 0x0246;
                        typeOfInqury = writeRegisters_FC;
                        break;
                    }
                case EnquryType.INQ_SAVE_RELAY_3_GISTEREZIS:
                    {
                        startAddress = 0x0242;
                        typeOfInqury = writeRegisters_FC;
                        break;
                    }
                case EnquryType.INQ_SAVE_NUMBER_OF_ROWS:
                    {
                        startAddress = 0x020E;
                        typeOfInqury = writeRegister_FC;
                        break;
                    }
                case EnquryType.INQ_SAVE_DISPLAY_LIGHT:
                    {
                        startAddress = 0x0206;
                        typeOfInqury = writeRegister_FC;
                        break;
                    }
                case EnquryType.INQ_SAVE_INDICATION_ROWS:
                    {
                        startAddress = 0x0400;
                        typeOfInqury = writeRegisters_FC;
                        break;
                    }
                case EnquryType.INQ_SET_UPP_CONFIG:
                    {
                        startAddress = 0x0000;
                        typeOfInqury = writeRegisters_FC;
                        break;
                    }
                case EnquryType.INQ_SET_V_UPP:
                    {
                        startAddress = 100;
                        typeOfInqury = writeRegisters_FC;
                        break;
                    }
                case EnquryType.INQ_SET_UPP_ON_VOLTAGE:
                case EnquryType.INQ_SET_UPP_OFF_VOLTAGE:
                    {
                        startAddress = 200;
                        typeOfInqury = writeRegisters_FC;
                        break;
                    }
                case EnquryType.INQ_SET_UPP_ON_Ia5:
                case EnquryType.INQ_SET_UPP_OFF_Ia5:
                    {
                        startAddress = 212;
                        typeOfInqury = writeRegister_FC;
                        break;
                    }
                case EnquryType.INQ_SET_UPP_ON_Ib5:
                case EnquryType.INQ_SET_UPP_OFF_Ib5:
                    {
                        startAddress = 214;
                        typeOfInqury = writeRegister_FC;
                        break;
                    }
                case EnquryType.INQ_SET_UPP_ON_Ic5:
                case EnquryType.INQ_SET_UPP_OFF_Ic5:
                    {
                        startAddress = 216;
                        typeOfInqury = writeRegister_FC;
                        break;
                    }
                case EnquryType.INQ_SET_UPP_ON_Ia:
                case EnquryType.INQ_SET_UPP_OFF_Ia:
                    {
                        startAddress = 206;
                        typeOfInqury = writeRegister_FC;
                        break;
                    }
                case EnquryType.INQ_SET_UPP_OFF_CURRENT:
                    {
                        startAddress = 206;
                        typeOfInqury = writeRegisters_FC;
                        break;
                    }
                case EnquryType.INQ_SET_UPP_ON_Ib:
                case EnquryType.INQ_SET_UPP_OFF_Ib:
                    {
                        startAddress = 208;
                        typeOfInqury = writeRegister_FC;
                        break;
                    }
                case EnquryType.INQ_SET_UPP_ON_Ic:
                case EnquryType.INQ_SET_UPP_OFF_Ic:
                    {
                        startAddress = 210;
                        typeOfInqury = writeRegister_FC;
                        break;
                    }
                case EnquryType.INQ_SET_UPP_5A_1A:
                    {
                        startAddress = 2;
                        typeOfInqury = writeRegister_FC;
                        break;
                    }
                case EnquryType.INQ_SET_UPP_IMP_NUM_OUT:
                    {
                        startAddress = 6;
                        typeOfInqury = writeRegister_FC;
                        break;
                    }
                case EnquryType.INQ_SET_UPP_IMP_MA:
                    {
                        startAddress = 8;
                        typeOfInqury = writeRegister_FC;
                        break;
                    }
                case EnquryType.INQ_SET_UPP_IMP_R:
                    {
                        startAddress = 10;
                        typeOfInqury = writeRegister_FC;
                        break;
                    }
                case EnquryType.INQ_SET_UPP_F_GEN:
                    {
                        startAddress = 3072;
                        typeOfInqury = writeRegisters_FC;
                        break;
                    }
                case EnquryType.INQ_SET_UPP_AMP_COEF_UA:
                    {
                        startAddress = 3024;
                        typeOfInqury = writeRegisters_FC;
                        break;
                    }
                case EnquryType.INQ_SET_UPP_AMP_COEF_UB:
                    {
                        startAddress = 3028;
                        typeOfInqury = writeRegisters_FC;
                        break;
                    }
                case EnquryType.INQ_SET_UPP_AMP_COEF_UC:
                    {
                        startAddress = 3032;
                        typeOfInqury = writeRegisters_FC;
                        break;
                    }
                case EnquryType.INQ_SET_UPP_AMP_COEF_IA:
                    {
                        startAddress = 3036;
                        typeOfInqury = writeRegisters_FC;
                        break;
                    }
                case EnquryType.INQ_SET_UPP_AMP_COEF_IB:
                    {
                        startAddress = 3040;
                        typeOfInqury = writeRegisters_FC;
                        break;
                    }
                case EnquryType.INQ_SET_UPP_AMP_COEF_IC:
                    {
                        startAddress = 3044;
                        typeOfInqury = writeRegisters_FC;
                        break;
                    }
                case EnquryType.INQ_SET_UPP_AMP_COEF_IA5:
                    {
                        startAddress = 3048;
                        typeOfInqury = writeRegisters_FC;
                        break;
                    }
                case EnquryType.INQ_SET_UPP_AMP_COEF_IB5:
                    {
                        startAddress = 3052;
                        typeOfInqury = writeRegisters_FC;
                        break;
                    }
                case EnquryType.INQ_SET_UPP_AMP_COEF_IC5:
                    {
                        startAddress = 3056;
                        typeOfInqury = writeRegisters_FC;
                        break;
                    }
                case EnquryType.INQ_SET_UPP_AMP_COEF_UA400:
                    {
                        startAddress = 3060;
                        typeOfInqury = writeRegisters_FC;
                        break;
                    }
                case EnquryType.INQ_SET_UPP_AMP_COEF_UB400:
                    {
                        startAddress = 3064;
                        typeOfInqury = writeRegisters_FC;
                        break;
                    }
                case EnquryType.INQ_SET_UPP_AMP_COEF_UC400:
                    {
                        startAddress = 3068;
                        typeOfInqury = writeRegisters_FC;
                        break;
                    }
                case EnquryType.INQ_SET_UPP_ANGLE_COEF_UA:
                    {
                        startAddress = 3000;
                        typeOfInqury = writeRegister_FC;
                        break;
                    }
                case EnquryType.INQ_SET_UPP_ANGLE_COEF_UB:
                    {
                        startAddress = 3002;
                        typeOfInqury = writeRegister_FC;
                        break;
                    }
                case EnquryType.INQ_SET_UPP_ANGLE_COEF_UC:
                    {
                        startAddress = 3004;
                        typeOfInqury = writeRegister_FC;
                        break;
                    }
                case EnquryType.INQ_SET_UPP_ANGLE_COEF_IA:
                    {
                        startAddress = 3006;
                        typeOfInqury = writeRegister_FC;
                        break;
                    }
                case EnquryType.INQ_SET_UPP_ANGLE_COEF_IB:
                    {
                        startAddress = 3008;
                        typeOfInqury = writeRegister_FC;
                        break;
                    }
                case EnquryType.INQ_SET_UPP_ANGLE_COEF_IC:
                    {
                        startAddress = 3010;
                        typeOfInqury = writeRegister_FC;
                        break;
                    }
                case EnquryType.INQ_SET_UPP_ANGLE_COEF_IA5:
                    {
                        startAddress = 3012;
                        typeOfInqury = writeRegister_FC;
                        break;
                    }
                case EnquryType.INQ_SET_UPP_ANGLE_COEF_IB5:
                    {
                        startAddress = 3014;
                        typeOfInqury = writeRegister_FC;
                        break;
                    }
                case EnquryType.INQ_SET_UPP_ANGLE_COEF_IC5:
                    {
                        startAddress = 3016;
                        typeOfInqury = writeRegister_FC;
                        break;
                    }
                case EnquryType.INQ_SET_UPP_ANGLE_COEF_UA400:
                    {
                        startAddress = 3018;
                        typeOfInqury = writeRegister_FC;
                        break;
                    }
                case EnquryType.INQ_SET_UPP_ANGLE_COEF_UB400:
                    {
                        startAddress = 3020;
                        typeOfInqury = writeRegister_FC;
                        break;
                    }
                case EnquryType.INQ_SET_UPP_ANGLE_COEF_UC400:
                    {
                        startAddress = 3022;
                        typeOfInqury = writeRegister_FC;
                        break;
                    }
                case EnquryType.INQ_SET_UPP_ADC_ZERO_5:
                    {
                        startAddress = 3076;
                        typeOfInqury = writeRegisters_FC;
                        break;
                    }
                case EnquryType.INQ_SET_UPP_ADC_ZERO_20:
                    {
                        startAddress = 3080;
                        typeOfInqury = writeRegisters_FC;
                        break;
                    }
                case EnquryType.INQ_SET_UPP_ADC_TOP_5:
                    {
                        startAddress = 3084;
                        typeOfInqury = writeRegisters_FC;
                        break;
                    }
                case EnquryType.INQ_SET_UPP_ADC_TOP_20:
                    {
                        startAddress = 3088;
                        typeOfInqury = writeRegisters_FC;
                        break;
                    }
                case EnquryType.INQ_SET_UPP_DAC_ZERO_5:
                    {
                        startAddress = 3092;
                        typeOfInqury = writeRegisters_FC;
                        break;
                    }
                case EnquryType.INQ_SET_UPP_DAC_ZERO_20:
                    {
                        startAddress = 3096;
                        typeOfInqury = writeRegisters_FC;
                        break;
                    }
                case EnquryType.INQ_SET_UPP_DAC_TOP_5:
                    {
                        startAddress = 3100;
                        typeOfInqury = writeRegisters_FC;
                        break;
                    }
                case EnquryType.INQ_SET_UPP_DAC_TOP_20:
                    {
                        startAddress = 3104;
                        typeOfInqury = writeRegisters_FC;
                        break;
                    }
                case EnquryType.INQ_SET_UPP_ANGLE_COEFFS:
                    {
                        startAddress = 3000;
                        typeOfInqury = writeRegisters_FC;
                        break;
                    }
                case EnquryType.INQ_SET_UPP_OTHER_COEFFS:
                    {
                        startAddress = 3024;
                        typeOfInqury = writeRegisters_FC;
                        break;
                    }
                case EnquryType.INQ_SET_UPP_I_OUT:
                    {
                        startAddress = 2;
                        typeOfInqury = writeRegisters_FC;
                        break;
                    }
                case EnquryType.INQ_SET_UPP_U_OUT:
                    {
                        startAddress = 4;
                        typeOfInqury = writeRegisters_FC;
                        break;
                    }
                case EnquryType.INQ_SET_ANGLE_UPP:
                    {
                        startAddress = 140;
                        typeOfInqury = writeRegister_FC;
                        break;
                    }
                case EnquryType.INQ_SET_UPP_DAC_VAL:
                    {
                        startAddress = 146;
                        typeOfInqury = writeRegisters_FC;
                        break;
                    }
                case EnquryType.INQ_SAVE_UPP_COEFS:
                    {
                        startAddress = 3200;
                        typeOfInqury = writeRegister_FC;
                        break;
                    }     
                case EnquryType.INQ_SET_I_UPP:
                    {
                        startAddress = 112;
                        typeOfInqury = writeRegisters_FC;
                        break;
                    }
                case EnquryType.INQ_SET_I5_UPP:
                    {
                        startAddress = 124;
                        typeOfInqury = writeRegisters_FC;
                        break;
                    }
                case EnquryType.INQ_SET_F_UPP:
                    {
                        startAddress = 142;
                        typeOfInqury = writeRegisters_FC;
                        break;
                    }
                case EnquryType.INQ_SAVE_USTAVKI:
                    {
                        startAddress = 0x0B00;
                        typeOfInqury = writeRegisters_FC;
                        break;
                    }
                case EnquryType.INQ_SAVE_ENERGY_ROWS:
                    {
                        startAddress = 0x0412;
                        typeOfInqury = writeRegisters_FC;
                        break;
                    }
                case EnquryType.INQ_SAVE_TO_INTERNAL_FLASH:
                    {
                        startAddress = 0x0320;
                        typeOfInqury = writeRegister_FC;
                        break;
                    }
                case EnquryType.INQ_SAVE_ZEROING_NOISE:
                    {
                        startAddress = 0x2100;
                        typeOfInqury = writeRegister_FC;
                        break;
                    }
                case EnquryType.INQ_SAVE_UA_CHANGE_PARAM:
                case EnquryType.INQ_SAVE_UB_CHANGE_PARAM:
                case EnquryType.INQ_SAVE_UC_CHANGE_PARAM:
                case EnquryType.INQ_SAVE_UAB_CHANGE_PARAM:
                case EnquryType.INQ_SAVE_UBC_CHANGE_PARAM:
                case EnquryType.INQ_SAVE_UCA_CHANGE_PARAM:
                case EnquryType.INQ_SAVE_IA_CHANGE_PARAM:
                case EnquryType.INQ_SAVE_IB_CHANGE_PARAM:
                case EnquryType.INQ_SAVE_IC_CHANGE_PARAM:
                    {
                        int shift = (ushort)enquryType - (ushort)EnquryType.INQ_SAVE_UA_CHANGE_PARAM;
                        startAddress = (ushort)(0x0700 + shift * 4);
                        typeOfInqury = writeRegisters_FC;
                        break;
                    }
                case EnquryType.INQ_SAVE_F_CHANGE_PARAM:
                    {
                        startAddress = 0x0734;
                        typeOfInqury = writeRegisters_FC;
                        break;
                    }
                case EnquryType.INQ_SAVE_CORRECT_POWER_UP:
                    {
                        startAddress = 0x0280;
                        typeOfInqury = writeRegister_FC;
                        break;
                    }
                case EnquryType.INQ_SAVE_CORRECT_POWER_DOWN:
                    {
                        startAddress = 0x0282;
                        typeOfInqury = writeRegister_FC;
                        break;
                    }
                case EnquryType.INQ_WRITE_CURRENT_MODE:
                    {
                        startAddress = 0x031E;
                        typeOfInqury = writeRegister_FC;
                        break;
                    }
                case EnquryType.INQ_WRITE_CURRENT_MODE_OFF:
                    {
                        startAddress = 0x031E;
                        typeOfInqury = writeRegister_FC;
                        break;
                    }
                case EnquryType.INQ_SAVE_ANALOG_OUTS_TYPE:
                    {
                        startAddress = 0x0212;
                        typeOfInqury = writeRegister_FC;
                        break;
                    }
                case EnquryType.INQ_SAVE_ANALOG_CORRECT_UP:
                    {
                        startAddress = 0x0284;
                        typeOfInqury = writeRegister_FC;
                        break;
                    }
                case EnquryType.INQ_SAVE_ANALOG_CORRECT_DOWN:
                    {
                        startAddress = 0x0286;
                        typeOfInqury = writeRegister_FC;
                        break;
                    }
                case EnquryType.INQ_SAVE_CORRECT_ANALOG_OUT1:
                case EnquryType.INQ_SAVE_CORRECT_ANALOG_OUT2:
                case EnquryType.INQ_SAVE_CORRECT_ANALOG_OUT3:
                    {
                        int shift = (ushort)enquryType - (ushort)EnquryType.INQ_SAVE_CORRECT_ANALOG_OUT1;
                        startAddress = (ushort)(0x0780 + shift * 4);
                        typeOfInqury = writeRegisters_FC;
                        break;
                    }
                case EnquryType.INQ_SET_INPUT_CLOCK_CHECK:
                    {
                        startAddress = 0x0322;
                        typeOfInqury = writeRegister_FC;
                        break;
                    }
                case EnquryType.INQ_SET_DATE_TIME:
                    {
                        startAddress = 0x0900;
                        typeOfInqury = writeRegisters_FC;
                        break;
                    }
                case EnquryType.INQ_SAVE_IMP_WTH:
                    {
                        startAddress = 0x0810;
                        typeOfInqury = writeRegisters_FC;
                        break;
                    }
                case EnquryType.INQ_SAVE_MEASURE_SCHEME:
                    {
                        startAddress = 0x0210;
                        typeOfInqury = writeRegister_FC;
                        break;
                    }
                case EnquryType.INQ_SAVE_CURRENT_IB:
                    {
                        startAddress = 0x0222;
                        typeOfInqury = writeRegister_FC;
                        break;
                    }
                case EnquryType.INQ_SAVE_COEF_U1:
                    {
                        startAddress = 0x0800;
                        typeOfInqury = writeRegisters_FC;
                        break;
                    }
                case EnquryType.INQ_SAVE_COEF_I1:
                    {
                        startAddress = 0x0808;
                        typeOfInqury = writeRegisters_FC;
                        break;
                    }
                case EnquryType.INQ_SAVE_COEF_U2:
                    {
                        startAddress = 0x0804;
                        typeOfInqury = writeRegisters_FC;
                        break;
                    }
                case EnquryType.INQ_SAVE_COEF_I2:
                    {
                        startAddress = 0x080C;
                        typeOfInqury = writeRegisters_FC;
                        break;
                    }
                case EnquryType.INQ_SAVE_ETHERNET_CONFIG:
                    {
                        startAddress = 0x0250;
                        typeOfInqury = writeRegisters_FC;
                        break;
                    }
            }
        }

        private void ProcessWritePackage(byte[] buffer)
        {
            if (mainForm.InvokeRequired)
            {
                mainForm.BeginInvoke(new Delegate(ProcessWritePackage), new object[] { buffer });
                return;
            }

            switch (storedEnquryType)
            {
                case EnquryType.INQ_SET_PASSWORD:
                    {
                        if (storedEnquryTypeForPassword == EnquryType.INQ_SET_DATE_TIME)
                            mainForm.setDateTime_button_Click(null, null);
                        else if (storedEnquryTypeForPassword == EnquryType.INQ_WRITE_TARIF_PRO)
                            mainForm.StartAutoInquryTimer(EnquryType.INQ_WRITE_TARIF_PRO, 50);
                        else
                            this.WriteData(storedEnquryTypeForPassword, storedDataForPassword);
                        break;
                    }
                case EnquryType.INQ_SAVE_COEF_I2:
                    {
                        mainForm.MainProgressBar = 100;
                        break;
                    }
                case EnquryType.INQ_SAVE_COEF_U2:
                    {
                        mainForm.MainProgressBar = 75;
                        try
                        {
                            //float temp = Convert.ToSingle(mainForm.I2ComboBox);
                            float temp = float.Parse(mainForm.I2ComboBox.Replace(",", CultureInfo.InvariantCulture.NumberFormat.NumberDecimalSeparator), CultureInfo.InvariantCulture);
                            byte[] bytes = BitConverter.GetBytes(temp);
                            Thread.Sleep(50);
                            this.WriteData(Modbus.EnquryType.INQ_SAVE_COEF_I2, bytes);
                        }
                        catch
                        {
                            MessageBox.Show(ProtocolGlobals.INCORRECT_DATA_MESSAGE);
                        }
                        break;
                    }
                case EnquryType.INQ_SAVE_COEF_I1:
                    {
                        mainForm.MainProgressBar = 60;
                        try
                        {
                            //float temp = Convert.ToSingle(mainForm.U2ComboBox);
                            float temp = float.Parse(mainForm.U2ComboBox.Replace(",", CultureInfo.InvariantCulture.NumberFormat.NumberDecimalSeparator), CultureInfo.InvariantCulture);
                            byte[] bytes = BitConverter.GetBytes(temp);
                            Thread.Sleep(50);
                            this.WriteData(Modbus.EnquryType.INQ_SAVE_COEF_U2, bytes);
                        }
                        catch
                        {
                            MessageBox.Show(ProtocolGlobals.INCORRECT_DATA_MESSAGE);
                        }
                        break;
                    }
                case EnquryType.INQ_SAVE_COEF_U1:
                    {
                        mainForm.MainProgressBar = 45;
                        try
                        {
                           // float temp = Convert.ToSingle(mainForm.I1ComboBox);
                            float temp = float.Parse(mainForm.I1ComboBox.Replace(",", CultureInfo.InvariantCulture.NumberFormat.NumberDecimalSeparator), CultureInfo.InvariantCulture);
                            byte[] bytes = BitConverter.GetBytes(temp);
                            Thread.Sleep(50);
                            this.WriteData(Modbus.EnquryType.INQ_SAVE_COEF_I1, bytes);
                        }
                        catch
                        {
                            MessageBox.Show(ProtocolGlobals.INCORRECT_DATA_MESSAGE);
                        }
                        break;
                    }
                case EnquryType.INQ_SAVE_MEASURE_SCHEME:
                    {
                        mainForm.MainProgressBar = 30;
                        try
                        {
                           // float temp = Convert.ToSingle(mainForm.U1ComboBox);
                            float temp = float.Parse(mainForm.U1ComboBox.Replace(",", CultureInfo.InvariantCulture.NumberFormat.NumberDecimalSeparator), CultureInfo.InvariantCulture);
                            byte[] bytes = BitConverter.GetBytes(temp);
                            Thread.Sleep(50);
                            this.WriteData(Modbus.EnquryType.INQ_SAVE_COEF_U1, bytes);
                        }
                        catch
                        {
                            MessageBox.Show(ProtocolGlobals.INCORRECT_DATA_MESSAGE);
                        }
                        break;
                    }
                case EnquryType.INQ_SAVE_CURRENT_IB:
                    {
                        mainForm.MainProgressBar = 15;
                        try
                        {
                            if (mainForm.MeasureSchemeComboBox == -1) throw new IndexOutOfRangeException();
                            byte[] data = BitConverter.GetBytes((ushort)mainForm.MeasureSchemeComboBox);
                            Thread.Sleep(50);
                            this.WriteData(Modbus.EnquryType.INQ_SAVE_MEASURE_SCHEME, data);
                        }
                        catch
                        {
                            MessageBox.Show(ProtocolGlobals.INCORRECT_DATA_MESSAGE);
                        }
                        break;
                    }
                case EnquryType.INQ_SET_INPUT_CLOCK_CHECK:
                    {
                        if (mainForm.ClockCheckButton == ProtocolGlobals.ON_BUTTON) mainForm.ClockCheckButton = ProtocolGlobals.OFF_BUTTON;
                        else mainForm.ClockCheckButton = ProtocolGlobals.ON_BUTTON;
                        break;
                    }
                case EnquryType.INQ_SAVE_CORRECT_ANALOG_OUT1:
                case EnquryType.INQ_SAVE_CORRECT_ANALOG_OUT2:
                case EnquryType.INQ_SAVE_CORRECT_ANALOG_OUT3:
                case EnquryType.INQ_SAVE_ANALOG_CORRECT_UP:
                case EnquryType.INQ_SAVE_ANALOG_CORRECT_DOWN:
                    {
                        mainForm.AnalogCorrectLabel = ProtocolGlobals.DATA_PASSED_ON_MESSAGE;
                        mainForm.StartAutoInquryTimer(EnquryType.INQ_READ_AUTOLOOP, 500);
                        break;
                    }
                case EnquryType.INQ_WRITE_CURRENT_MODE:
                    {
                        mainForm.SaveAnalogOutsButton = true;
                        mainForm.AnalogOutsRangesGroupBox = true;
                        mainForm.AnalogOutsGroupBoxes = true;
                        mainForm.StartAutoInquryTimer(EnquryType.INQ_READ_AUTOLOOP, 500);
                        break;
                    }
                case EnquryType.INQ_WRITE_CURRENT_MODE_OFF:
                    {
                        break;
                    }
                case EnquryType.INQ_SAVE_UA_CHANGE_PARAM:
                case EnquryType.INQ_SAVE_UB_CHANGE_PARAM:
                case EnquryType.INQ_SAVE_UC_CHANGE_PARAM:
                case EnquryType.INQ_SAVE_UAB_CHANGE_PARAM:
                case EnquryType.INQ_SAVE_UBC_CHANGE_PARAM:
                case EnquryType.INQ_SAVE_UCA_CHANGE_PARAM:
                case EnquryType.INQ_SAVE_IA_CHANGE_PARAM:
                case EnquryType.INQ_SAVE_IB_CHANGE_PARAM:
                case EnquryType.INQ_SAVE_IC_CHANGE_PARAM:
                case EnquryType.INQ_SAVE_F_CHANGE_PARAM:
                case EnquryType.INQ_SAVE_CORRECT_POWER_UP:
                case EnquryType.INQ_SAVE_CORRECT_POWER_DOWN:
                    {
                        mainForm.SaveStatusLabel = ProtocolGlobals.DATA_PASSED_ON_MESSAGE;
                        if (mainForm.uppConfigurator.IsConnected())
                            mainForm.StartAutoInquryTimer(EnquryType.INQ_READ_31_PARAMS_FROM_UPP, 500);
                        else
                            mainForm.StartAutoInquryTimer(EnquryType.INQ_READ_31_PARAMS, 500);
                        break;
                    }
                case EnquryType.INQ_SAVE_ZEROING_NOISE:
                    {
                        mainForm.SaveStatusLabel = ProtocolGlobals.OPERATION_SUCCESS_MESSAGE;
                        if (mainForm.uppConfigurator.IsConnected())
                            mainForm.StartAutoInquryTimer(EnquryType.INQ_READ_31_PARAMS_FROM_UPP, 500);
                        else
                            mainForm.StartAutoInquryTimer(EnquryType.INQ_READ_31_PARAMS, 500);
                        break;
                    }
                case EnquryType.INQ_SAVE_TO_INTERNAL_FLASH:
                    {
                        mainForm.SaveStatusLabel = ProtocolGlobals.INFO_SAVED;
                        mainForm.AnalogCorrectLabel = ProtocolGlobals.INFO_SAVED;
                        if (mainForm.ConfigurateTabControlSelectedIndex == 2)
                        {
                            if (mainForm.uppConfigurator.IsConnected())
                                mainForm.StartAutoInquryTimer(EnquryType.INQ_READ_31_PARAMS_FROM_UPP, 500);
                            else
                                mainForm.StartAutoInquryTimer(EnquryType.INQ_READ_31_PARAMS, 500);
                        }
                        else if (mainForm.ConfigurateTabControlSelectedIndex == 3)
                        {
                            mainForm.StartAutoInquryTimer(EnquryType.INQ_READ_AUTOLOOP, 500);
                        }
                        break;
                    }
                case EnquryType.INQ_SAVE_INDICATION_ROWS:
                    {
                        mainForm.MainProgressBar = 100;
                        //mainForm.StartAutoInquryTimer(EnquryType.INQ_READ_DISPLAY_USTAVKI, 50);
                        break;
                    }
                case EnquryType.INQ_SAVE_NUMBER_OF_ROWS:
                    {
                        mainForm.MainProgressBar = 0;
                        mainForm.StartAutoInquryTimer(EnquryType.INQ_READ_INDICATION_INFO, 50);
                        break;
                    }
                case EnquryType.INQ_SAVE_RELAY_1_CONFIG:
                case EnquryType.INQ_SAVE_RELAY_2_CONFIG:
                case EnquryType.INQ_SAVE_RELAY_3_CONFIG:
                    {
                        mainForm.MainProgressBar = 50;
                        try
                        {
                            float temp = Convert.ToSingle(mainForm.RelayGisterezisTextBox);
                            byte[] bytes = BitConverter.GetBytes(temp);
                            Thread.Sleep(50);
                            if (storedEnquryType == EnquryType.INQ_SAVE_RELAY_1_CONFIG)
                                this.WriteData(Modbus.EnquryType.INQ_SAVE_RELAY_1_GISTEREZIS, bytes);
                            else if (storedEnquryType == EnquryType.INQ_SAVE_RELAY_2_CONFIG)
                                this.WriteData(Modbus.EnquryType.INQ_SAVE_RELAY_2_GISTEREZIS, bytes);
                            else if (storedEnquryType == EnquryType.INQ_SAVE_RELAY_3_CONFIG)
                                this.WriteData(Modbus.EnquryType.INQ_SAVE_RELAY_3_GISTEREZIS, bytes);
                        }
                        catch
                        {
                            MessageBox.Show(ProtocolGlobals.INCORRECT_DATA_MESSAGE);
                        }
                        break;
                    }
                case EnquryType.INQ_SAVE_RELAY_1_GISTEREZIS:
                case EnquryType.INQ_SAVE_RELAY_2_GISTEREZIS:
                    {
                        mainForm.MainProgressBar = 100;
                        break;
                    }
                case EnquryType.INQ_SAVE_SPEED_A:
                    {
                        mainForm.ComPortSpeedComboBox = mainForm.SpeedAComboBox;
                        // MessageBox.Show("Информация записана");
                        mainForm.ShowMessageBox(ProtocolGlobals.RECORDING_DONE_MESSAGE);
                        //  mainForm.StatusLabel = "Информация записана";
                        break;
                    }
                case EnquryType.INQ_SAVE_IEC_101_CONFIG:
                    {
                        mainForm.IEC101Settings.save_values();
                        break;
                    }
                case EnquryType.INQ_SAVE_IEC_104_CONFIG:
                    {
                        mainForm.IEC104Settings.save_values();
                        break;
                    }
                case EnquryType.INQ_SET_UPP_CONFIG:
                    {
                        mainForm.StartAutoInquryTimer(EnquryType.INQ_READ_UPP_ANGLE_COEFS, 50);
                        break;
                    }
                case EnquryType.INQ_SET_V_UPP:
                case EnquryType.INQ_SET_I_UPP:
                case EnquryType.INQ_SET_I5_UPP:
                case EnquryType.INQ_SET_F_UPP:
                    {
                        mainForm.SaveStatusLabel = ProtocolGlobals.DATA_PASSED_ON_MESSAGE;
                        if (storedEnquryType == EnquryType.INQ_SET_V_UPP) mainForm.uppConfigurator.VoltageIsOn = true;
                        else if (storedEnquryType == EnquryType.INQ_SET_I_UPP || storedEnquryType == EnquryType.INQ_SET_I5_UPP)
                        {
                            mainForm.uppConfigurator.IaIsOn = true;
                            mainForm.uppConfigurator.IbIsOn = true;
                            mainForm.uppConfigurator.IcIsOn = true;
                        }
                        mainForm.StartAutoInquryTimer(EnquryType.INQ_READ_31_PARAMS_FROM_UPP, 500);
                        break;
                    }
                case EnquryType.INQ_SET_UPP_OFF_CURRENT:
                    {
                        mainForm.uppConfigurator.IaIsOn = false;
                        mainForm.uppConfigurator.IbIsOn = false;
                        mainForm.uppConfigurator.IcIsOn = false;

                        mainForm.StartAutoInquryTimer(EnquryType.INQ_READ_31_PARAMS_FROM_UPP, 500);
                        break;
                    }
                case EnquryType.INQ_SET_UPP_ON_VOLTAGE:
                    {
                        mainForm.uppConfigurator.VoltageIsOn = true;
                        mainForm.StartAutoInquryTimer(EnquryType.INQ_READ_31_PARAMS_FROM_UPP, 500);
                        break;
                    }
                case EnquryType.INQ_SET_UPP_OFF_VOLTAGE:
                    {
                        mainForm.uppConfigurator.VoltageIsOn = false;
                        mainForm.StartAutoInquryTimer(EnquryType.INQ_READ_31_PARAMS_FROM_UPP, 500);
                        break;
                    }
                case EnquryType.INQ_SET_UPP_ON_Ia5:
                case EnquryType.INQ_SET_UPP_ON_Ia:
                    {
                        mainForm.uppConfigurator.IaIsOn = true;
                        mainForm.StartAutoInquryTimer(EnquryType.INQ_READ_31_PARAMS_FROM_UPP, 500);
                        break;
                    }
                case EnquryType.INQ_SET_UPP_OFF_Ia5:
                case EnquryType.INQ_SET_UPP_OFF_Ia:
                    {
                        mainForm.uppConfigurator.IaIsOn = false;
                        mainForm.StartAutoInquryTimer(EnquryType.INQ_READ_31_PARAMS_FROM_UPP, 500);
                        break;
                    }
                case EnquryType.INQ_SET_UPP_ON_Ib5:
                case EnquryType.INQ_SET_UPP_ON_Ib:
                    {
                        mainForm.uppConfigurator.IbIsOn = true;
                        mainForm.StartAutoInquryTimer(EnquryType.INQ_READ_31_PARAMS_FROM_UPP, 500);
                        break;
                    }
                case EnquryType.INQ_SET_UPP_OFF_Ib5:
                case EnquryType.INQ_SET_UPP_OFF_Ib:
                    {
                        mainForm.uppConfigurator.IbIsOn = false;
                        mainForm.StartAutoInquryTimer(EnquryType.INQ_READ_31_PARAMS_FROM_UPP, 500);
                        break;
                    }
                case EnquryType.INQ_SET_UPP_ON_Ic5:
                case EnquryType.INQ_SET_UPP_ON_Ic:
                    {
                        mainForm.uppConfigurator.IcIsOn = true;
                        mainForm.StartAutoInquryTimer(EnquryType.INQ_READ_31_PARAMS_FROM_UPP, 500);
                        break;
                    }
                case EnquryType.INQ_SET_UPP_OFF_Ic5:
                case EnquryType.INQ_SET_UPP_OFF_Ic:
                    {
                        mainForm.uppConfigurator.IcIsOn = false;
                        mainForm.StartAutoInquryTimer(EnquryType.INQ_READ_31_PARAMS_FROM_UPP, 500);
                        break;
                    }
                case EnquryType.INQ_SET_UPP_5A_1A:
                case EnquryType.INQ_SET_UPP_IMP_NUM_OUT:
                case EnquryType.INQ_SET_UPP_IMP_MA:
                case EnquryType.INQ_SET_UPP_IMP_R:
                case EnquryType.INQ_SET_UPP_F_GEN:
                case EnquryType.INQ_SET_UPP_AMP_COEF_UA:
                case EnquryType.INQ_SET_UPP_AMP_COEF_UB:
                case EnquryType.INQ_SET_UPP_AMP_COEF_UC:
                case EnquryType.INQ_SET_UPP_AMP_COEF_IA:
                case EnquryType.INQ_SET_UPP_AMP_COEF_IB:
                case EnquryType.INQ_SET_UPP_AMP_COEF_IC:
                case EnquryType.INQ_SET_UPP_AMP_COEF_IA5:
                case EnquryType.INQ_SET_UPP_AMP_COEF_IB5:
                case EnquryType.INQ_SET_UPP_AMP_COEF_IC5:
                case EnquryType.INQ_SET_UPP_AMP_COEF_UA400:
                case EnquryType.INQ_SET_UPP_AMP_COEF_UB400:
                case EnquryType.INQ_SET_UPP_AMP_COEF_UC400:
                case EnquryType.INQ_SET_UPP_ANGLE_COEF_UA:
                case EnquryType.INQ_SET_UPP_ANGLE_COEF_UB:
                case EnquryType.INQ_SET_UPP_ANGLE_COEF_UC:
                case EnquryType.INQ_SET_UPP_ANGLE_COEF_IA:
                case EnquryType.INQ_SET_UPP_ANGLE_COEF_IB:
                case EnquryType.INQ_SET_UPP_ANGLE_COEF_IC:
                case EnquryType.INQ_SET_UPP_ANGLE_COEF_IA5:
                case EnquryType.INQ_SET_UPP_ANGLE_COEF_IB5:
                case EnquryType.INQ_SET_UPP_ANGLE_COEF_IC5:
                case EnquryType.INQ_SET_UPP_ANGLE_COEF_UA400:
                case EnquryType.INQ_SET_UPP_ANGLE_COEF_UB400:
                case EnquryType.INQ_SET_UPP_ANGLE_COEF_UC400:
                case EnquryType.INQ_SET_UPP_ADC_ZERO_5:
                case EnquryType.INQ_SET_UPP_ADC_ZERO_20:
                case EnquryType.INQ_SET_UPP_DAC_ZERO_5:
                case EnquryType.INQ_SET_UPP_DAC_ZERO_20:
                case EnquryType.INQ_SET_UPP_ADC_TOP_5:
                case EnquryType.INQ_SET_UPP_ADC_TOP_20:
                case EnquryType.INQ_SET_UPP_DAC_TOP_5:
                case EnquryType.INQ_SET_UPP_DAC_TOP_20:
                case EnquryType.INQ_SAVE_UPP_COEFS:
                case EnquryType.INQ_SET_UPP_OTHER_COEFFS:
                case EnquryType.INQ_SET_UPP_DAC_VAL:
                case EnquryType.INQ_SET_ANGLE_UPP:
                    {
                        mainForm.StartAutoInquryTimer(EnquryType.INQ_READ_31_PARAMS_FROM_UPP, 500);
                        break;
                    }
                case EnquryType.INQ_SET_UPP_ANGLE_COEFFS:
                    {
                        mainForm.uppConfigurator.SaveOtherCoeffs();
                        break;
                    }
                case EnquryType.INQ_SET_UPP_U_OUT:
                    {
                        byte[] data = new byte[6];
                        Array.Clear(data, 0, data.Length);
                        mainForm.modbus.WriteData(Modbus.EnquryType.INQ_SET_UPP_OFF_VOLTAGE, data);
                        break;
                    }
                case EnquryType.INQ_SET_UPP_I_OUT:
                    {
                        byte[] data = new byte[6];
                        Array.Clear(data, 0, data.Length);
                        mainForm.modbus.WriteData(Modbus.EnquryType.INQ_SET_UPP_OFF_CURRENT, data);
                        break;
                    }
                default:
                    {
                        // MessageBox.Show("Информация записана");
                        // mainForm.StatusLabel = "Информация записана";
                        mainForm.ShowMessageBox(ProtocolGlobals.RECORDING_DONE_MESSAGE);
                        break;
                    }
            }
            mainForm.StatusLabel = ProtocolGlobals.OPERATION_SUCCESS_MESSAGE;
        }


        public void TestIC()
        {
            int k = 0;
            byte[] buffOut = new byte[13];

            string errorString = "";
            byte[] byteArray;

            try
            {
                buffOut[k++] = Convert.ToByte(mainForm.textBox47.Text);
            }
            catch
            {
                errorString += "Ошибка поля 'Сетевой адрес'" + Environment.NewLine;
            }

            buffOut[k++] = 0x10;

            try
            {
                UInt16 temp = Convert.ToUInt16(mainForm.textBox48.Text);
                byteArray = BitConverter.GetBytes(temp);
                buffOut[k++] = byteArray[1];
                buffOut[k++] = byteArray[0];
            }
            catch
            {
                errorString += "Ошибка поля 'Адрес данных'" + Environment.NewLine;
            }

            buffOut[k++] = 0x00;
            buffOut[k++] = 0x02;
            buffOut[k++] = 0x04;

            try
            {
                float tempFloat = Convert.ToSingle(mainForm.textBox49.Text);
                byteArray = BitConverter.GetBytes(tempFloat);

                if (mainForm.comboBox49.SelectedIndex != -1)
                {
                    if (mainForm.comboBox49.SelectedIndex == 0)
                    {
                        buffOut[k++] = byteArray[3];
                        buffOut[k++] = byteArray[2];
                        buffOut[k++] = byteArray[1];
                        buffOut[k++] = byteArray[0];
                    }
                    else
                    {
                        buffOut[k++] = byteArray[1];
                        buffOut[k++] = byteArray[0];
                        buffOut[k++] = byteArray[3];
                        buffOut[k++] = byteArray[2];
                    }
                }
                else
                {
                    errorString += "Ошибка поля 'Порядок байт'" + Environment.NewLine;
                }
            }
            catch
            {
                errorString += "Ошибка поля 'Данныe'" + Environment.NewLine;
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

        protected override void NoAnswer_Timer_Tick(object sender, EventArgs e)
        {
            if (mainForm.InvokeRequired)
            {
                mainForm.BeginInvoke(new Delegate_2(NoAnswer_Timer_Tick), new object[] { sender, e });
                return;
            }

            if (storedEnquryType == EnquryType.INQ_SAVE_SPEED_A)
            {
                mainForm.ComPortSpeedComboBox = mainForm.SpeedAComboBox;
                mainForm.StartAutoInquryTimer(EnquryType.INQ_READ_CHECK_SPEED_A, 50);
                return;
            }

            //if (storedEnquryType == EnquryType.INQ_READ_31_PARAMS_FROM_UPP)
            //{
            //    if (mainForm.uppConfigurator.IsConnected())
            //        mainForm.StartAutoInquryTimer(EnquryType.INQ_READ_31_PARAMS_FROM_UPP, 50);
            //    return;
            //}

            if (mainForm.uppConfigurator.readCoefs_button.Text == ProtocolGlobals.OFF_BUTTON) mainForm.uppConfigurator.Reconnect();

            if (storedEnquryType == EnquryType.INQ_READ_DEVICE_ADDR)
            {
                ushort number = Convert.ToUInt16(mainForm.deviceNumber_textBox.Text);
                if (number < 254)
                {
                    number++;
                    mainForm.deviceNumber_textBox.Text = number.ToString();
                    mainForm.StartAutoInquryTimer(EnquryType.INQ_READ_DEVICE_ADDR, 50);
                    return;
                }
                noAnswer_Timer.Interval = 2000;
                mainForm.StatusLabel = "Адрес не найден";
            }

            if (storedEnquryType == EnquryType.INQ_READ_DEVICE_ADDR_2)
            {
                mainForm.deviceNumber_textBox.Text = "1";
                noAnswer_Timer.Interval = 100;
                mainForm.StartAutoInquryTimer(EnquryType.INQ_READ_DEVICE_ADDR, 50);
            }

            noAnswer_Timer.Stop();
            mainForm.tcpClient = new TcpClient();
            mainForm.StatusLabel = ProtocolGlobals.NO_RESPONSE_MESSAGE;

            if (mainForm.MainTabControlSelectedIndex == 0 || mainForm.MainTabControlSelectedIndex == 1) // если выбрана первая вкладка
            {
                noAnswerCounter++;
                mainForm.NoAnswerCounterTextBox = noAnswerCounter.ToString();
                mainForm.ColorStatusButton = Color.Crimson;

                if (mainForm.AutoLoopIsOn)
                {
                    mainForm.StartAutoInquryTimer(storedEnquryType, 50);
                }
                else
                {
                    mainForm.Read31ParamButton = Globals.READ_BUTTON_TAG;
                }
            }
            else if (mainForm.MainTabControlSelectedIndex == 1 && mainForm.AutoLoopIsOn) // если выбрана первая вкладка
            {
                mainForm.StartAutoInquryTimer(storedEnquryType, 50);
            }
            else if (mainForm.MainTabControlSelectedIndex == 2 && mainForm.AutoLoopIsOn) // если выбрана первая вкладка
            {
                mainForm.StartAutoInquryTimer(storedEnquryType, 50);
            }
            else if (mainForm.MainTabControlSelectedIndex == 5 && mainForm.AutoLoopIsOn)
            {
                mainForm.StartAutoInquryTimer(storedEnquryType, 50);
            }
        }


        public void ReadData10()
        {
            if (mainForm.CommunicationComboBox == ProtocolGlobals.ETHERNET_TAG)
            {
                if (!base.TryToConnectTCP(mainForm.IPAddressComboBox, 2404))
                    return;
            }

            byte[] buffer = { 0x68, 0x04, 0x07, 0x00, 0x00, 0x00 };

            base.SendMessageTCP(mainForm.tcpClient, buffer);
        }

        public void ReadData11()
        {
            if (mainForm.CommunicationComboBox == ProtocolGlobals.ETHERNET_TAG)
            {
                if (!base.TryToConnectTCP(mainForm.IPAddressComboBox, 2404))
                    return;
            }

            byte[] buffer = { 0x68, 0x0D, 0x00, 0x00, 0x00, 0x00, 0x66, 0x01, 0x05, 0x00, 0x01, 0x00, 0x1F, 0x20, 0x00 };

            base.SendMessageTCP(mainForm.tcpClient, buffer);
        }

        public EnquryType StoredEnquryType
        {
            get
            {
                return storedEnquryType;
            }
            set
            {
                storedEnquryType = value;
            }
        }

        public ushort StoredRecordNumber
        {
            get
            {
                return storedRecordNumber;
            }
            set
            {
                storedRecordNumber = value;
            }
        }

        public void ClearCRCErrorsCounter()
        {
            crcErrorCounter = 0;
            mainForm.CRCErrorTextBox = "";
        }

        public void ClearNoAnswersCounter()
        {
            noAnswerCounter = 0;
            mainForm.NoAnswerCounterTextBox = "";
        }

        public void ClearInquresCounter()
        {
            inquresCounter = 0;
            maxAnswerTime = 0;
        }
    }
}
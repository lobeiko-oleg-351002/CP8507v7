using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Forms;

namespace CP8507_v7
{
    partial class Modbus
    {
        private enum ErrorCodes : byte
        {
            MB_EX_NONE = 0x00,
            MB_EX_ILLEGAL_FUNCTION = 0x01,
            MB_EX_ILLEGAL_DATA_ADDRESS = 0x02,
            MB_EX_ILLEGAL_DATA_VALUE = 0x03,
            MB_EX_SLAVE_DEVICE_FAILURE = 0x04,
            MB_EX_ACKNOWLEDGE = 0x05,
            MB_EX_SLAVE_BUSY = 0x06,
            MB_EX_NEGATIVE_ACKNOWLEDGE = 0x07,
            MB_EX_MEMORY_PARITY_ERROR = 0x08,
            MB_EX_GATEWAY_PATH_FAILED = 0x0A,
            MB_EX_GATEWAY_TGT_FAILED = 0x0B,
            MB_EX_DIVISIBLE_FAILED = 0x40,   // начало информации не кратно размерности
            MB_EX_DIMENSION_FAILED = 0x41,   // размер запрашиваемой информации превышает допустимую величину
            MB_EX_FORB_DATA_ADDRESS = 0x42,   // по запрашиваемому адресу информациЯ отсутствует или закрыта
            MB_EX_ILLEGAL_DATA_DIM = 0x43,   // не указан точный размер информации
            MB_EX_ILLEGAL_NET_ADDR = 0x44,   // недопустимый сетевой адрес
            MB_EX_ILLEGAL_DATA_VAL = 0x45,   // попытка установить недопустимое значение
            MB_EX_DEV_PROTECT_VALUE = 0x46,   // на изменЯемый параметр установлена аппаратнаЯ защита
            MB_EX_ILLEGAL_PASSWORD = 0x47    // передан неверный пароль
        }

        public struct RelaySettings
        {
            public ushort mode;
            public ushort param;
            public short ustavka;
            public ushort ping;
            public float gisterezis;
        }

        public struct TransformationCoefs
        {
            public float U1;
            public float I1;
        }

        private void DecodeErrorPackage(byte errorCode)
        {
            switch (errorCode)
            {
                case (byte)ErrorCodes.MB_EX_NONE:
                    //MessageBox.Show("MB_EX_NONE");
                    mainForm.StatusLabel = "MB_EX_NONE";
                    break;
                case (byte)ErrorCodes.MB_EX_ILLEGAL_FUNCTION:
                    //   MessageBox.Show("Код запроса не поддерживается");
                    mainForm.StatusLabel = "Код запроса не поддерживается";
                    break;
                case (byte)ErrorCodes.MB_EX_ILLEGAL_DATA_ADDRESS:
                    // MessageBox.Show("Адрес данных указанный в запросе недоступен");
                    mainForm.StatusLabel = "Адрес данных указанный в запросе недоступен";
                    // mainForm.ShowMessageBox("Адрес данных указанный в запросе недоступен");
                    break;
                case (byte)ErrorCodes.MB_EX_ILLEGAL_DATA_VALUE:
                    //  MessageBox.Show("Данные в запросе являются недопустимой величиной");
                    //  mainForm.StatusLabel = "Данные в запросе являются недопустимой величиной";
                    mainForm.ShowMessageBox("Данные в запросе являются недопустимой величиной");
                    break;
                case (byte)ErrorCodes.MB_EX_SLAVE_DEVICE_FAILURE:
                    // MessageBox.Show("Невосстанавливаемая ошибка");
                    mainForm.StatusLabel = "Невосстанавливаемая ошибка";
                    break;
                case (byte)ErrorCodes.MB_EX_NEGATIVE_ACKNOWLEDGE:
                    {
                        // MessageBox.Show("На изменяемый параметр установлена аппаратная защита");
                        mainForm.StatusLabel = "На изменяемый параметр установлена аппаратная защита";
                        // mainForm.StatusLabel = "Для изменения параметра необходимо ввести пароль";
                        mainForm.EnterPassword(0);
                        break;
                    }
                case (byte)ErrorCodes.MB_EX_ACKNOWLEDGE:
                    // MessageBox.Show("Обработка запроса требует много времени");
                    mainForm.StatusLabel = "Обработка запроса требует много времени";
                    break;
                case (byte)ErrorCodes.MB_EX_SLAVE_BUSY:
                    //   MessageBox.Show("Подчиненный занят обработкой команды");
                    mainForm.StatusLabel = "Подчиненный занят обработкой команды";
                    break;
                case (byte)ErrorCodes.MB_EX_MEMORY_PARITY_ERROR:
                    // MessageBox.Show("Ошибка паритета"); // Ошибка паритета
                    mainForm.StatusLabel = "Ошибка паритета";
                    break;
                case (byte)ErrorCodes.MB_EX_GATEWAY_PATH_FAILED:
                    //MessageBox.Show("MB_EX_GATEWAY_PATH_FAILED");
                    mainForm.StatusLabel = "MB_EX_GATEWAY_PATH_FAILED";
                    break;
                case (byte)ErrorCodes.MB_EX_GATEWAY_TGT_FAILED:
                    // MessageBox.Show("MB_EX_GATEWAY_TGT_FAILED");
                    mainForm.StatusLabel = "MB_EX_GATEWAY_TGT_FAILED";
                    break;
                case (byte)ErrorCodes.MB_EX_DIVISIBLE_FAILED:
                    // MessageBox.Show("Начало информации не кратно размерности");
                    mainForm.StatusLabel = "Начало информации не кратно размерности";
                    break;
                case (byte)ErrorCodes.MB_EX_DIMENSION_FAILED:
                    // MessageBox.Show("Размер запрашиваемой информации превышает допустимую величину");
                    mainForm.StatusLabel = "Размер запрашиваемой информации превышает допустимую величину";
                    break;
                case (byte)ErrorCodes.MB_EX_FORB_DATA_ADDRESS:
                    if (storedEnquryType >= EnquryType.INQ_READ_TURN_ON_OFF_DT_JOURNAL && storedEnquryType <= EnquryType.INQ_READ_dUMINUS_JOURNAL)
                    {
                        mainForm.ReadEventsButtonPush();
                        // MessageBox.Show("Чтение данных окончено");
                        // mainForm.StatusLabel = "Чтение данных окончено";
                        mainForm.ShowMessageBox("Чтение данных окончено");
                    }
                    else
                        //  MessageBox.Show("По запрашиваемому адресу информация отсутствует или закрыта");
                        mainForm.StatusLabel = "По запрашиваемому адресу информация отсутствует или закрыта";
                    //  mainForm.ShowMessageBox("По запрашиваемому адресу информация отсутствует или закрыта");
                    break;
                case (byte)ErrorCodes.MB_EX_ILLEGAL_DATA_DIM:
                    //MessageBox.Show("Не указан точный размер информации");
                    mainForm.StatusLabel = "Не указан точный размер информации";
                    break;
                case (byte)ErrorCodes.MB_EX_ILLEGAL_NET_ADDR:
                    // MessageBox.Show("недопустимый сетевой адрес");
                    mainForm.StatusLabel = "Недопустимый сетевой адрес";
                    break;
                case (byte)ErrorCodes.MB_EX_ILLEGAL_DATA_VAL:
                    //  MessageBox.Show("Попытка установить недопустимое значение");
                    //   mainForm.StatusLabel = "Попытка установить недопустимое значение";
                    mainForm.ShowMessageBox("Попытка установить недопустимое значение");
                    break;
                case (byte)ErrorCodes.MB_EX_DEV_PROTECT_VALUE:
                    // MessageBox.Show("На изменяемый параметр установлена аппаратная защита");
                    mainForm.StatusLabel = "На изменяемый параметр установлена аппаратная защита";
                    // MessageBox.Show("Некорректно введен адрес устройства", " ", MessageBoxButtons.OK, MessageBoxIcon.None, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
                    break;
                case (byte)ErrorCodes.MB_EX_ILLEGAL_PASSWORD:
                    // MessageBox.Show("Передан неверный пароль");
                    //mainForm.StatusLabel = "Передан неверный пароль";
                    mainForm.ShowMessageBox("Передан неверный пароль");
                    break;
                default:
                    MainForm.WriteLog("Не обработан код ошибки ");
                    break;
            }
        }

        private void Decode31ParamsAndEnergy(byte[] buffer)
        {
            try
            {
                ushort prefix = 0;
                prefix = this.CalculateUnitShfit(transformationCoefs.U1, false);

                if (storedMeasureSheme == (int)MeasureScheme._3_WIRED)
                {
                    mainForm.UaTextBox = "--------";
                    mainForm.UbTextBox = "--------";
                    mainForm.UcTextBox = "--------";

                    mainForm.UfTextBox = "--------";
                    mainForm.U0TextBox = "--------";
                }
                else
                {
                    mainForm.UaTextBox = this.PrepareParameter(this.ConvertByteToFloat(buffer, 3), prefix).ToString("F3");
                    mainForm.UbTextBox = this.PrepareParameter(this.ConvertByteToFloat(buffer, 7), prefix).ToString("F3");
                    mainForm.UcTextBox = this.PrepareParameter(this.ConvertByteToFloat(buffer, 11), prefix).ToString("F3");

                    mainForm.UfTextBox = this.PrepareParameter(this.ConvertByteToFloat(buffer, 107), prefix).ToString("F3");
                    mainForm.U0TextBox = this.PrepareParameter(this.ConvertByteToFloat(buffer, 119), prefix).ToString("F3");
                }

                mainForm.UabTextBox = this.PrepareParameter(this.ConvertByteToFloat(buffer, 27), prefix).ToString("F3");
                mainForm.UbcTextBox = this.PrepareParameter(this.ConvertByteToFloat(buffer, 31), prefix).ToString("F3");
                mainForm.UcaTextBox = this.PrepareParameter(this.ConvertByteToFloat(buffer, 35), prefix).ToString("F3");

                float Ul = this.ConvertByteToFloat(buffer, 115);
                mainForm.UlTextBox = this.PrepareParameter(Ul, prefix).ToString("F3");

                prefix = this.CalculateUnitShfit(transformationCoefs.I1, true);
                mainForm.IaTextBox = this.PrepareParameter(this.ConvertByteToFloat(buffer, 15), prefix).ToString("F4");
                mainForm.IbTextBox = this.PrepareParameter(this.ConvertByteToFloat(buffer, 19), prefix).ToString("F4");
                mainForm.IcTextBox = this.PrepareParameter(this.ConvertByteToFloat(buffer, 23), prefix).ToString("F4");

                float I = this.ConvertByteToFloat(buffer, 111);
                mainForm.ITextBox = this.PrepareParameter(I, prefix).ToString("F4");
                mainForm.I0TextBox = this.PrepareParameter(this.ConvertByteToFloat(buffer, 123), prefix).ToString("F4");

                prefix = this.CalculateUnitShfit((transformationCoefs.U1 * transformationCoefs.I1) * (float)Math.Sqrt(3.0), false); // / sqrt(3.0)
                if (storedMeasureSheme == (int)MeasureScheme._3_WIRED)
                {
                    mainForm.PaTextBox = "--------";
                    mainForm.PbTextBox = "--------";
                    mainForm.PcTextBox = "--------";

                    mainForm.QaTextBox = "--------";
                    mainForm.QbTextBox = "--------";
                    mainForm.QcTextBox = "--------";

                    mainForm.SaTextBox = "--------";
                    mainForm.SbTextBox = "--------";
                    mainForm.ScTextBox = "--------";

                    mainForm.KpaTextBox = "--------";
                    mainForm.KpbTextBox = "--------";
                    mainForm.KpcTextBox = "--------";
                }
                else
                {
                    mainForm.PaTextBox = this.PrepareParameter(this.ConvertByteToFloat(buffer, 39), prefix).ToString("F3");
                    mainForm.PbTextBox = this.PrepareParameter(this.ConvertByteToFloat(buffer, 43), prefix).ToString("F3");
                    mainForm.PcTextBox = this.PrepareParameter(this.ConvertByteToFloat(buffer, 47), prefix).ToString("F3");

                    mainForm.QaTextBox = this.PrepareParameter(this.ConvertByteToFloat(buffer, 51), prefix).ToString("F3");
                    mainForm.QbTextBox = this.PrepareParameter(this.ConvertByteToFloat(buffer, 55), prefix).ToString("F3");
                    mainForm.QcTextBox = this.PrepareParameter(this.ConvertByteToFloat(buffer, 59), prefix).ToString("F3");

                    mainForm.SaTextBox = this.PrepareParameter(this.ConvertByteToFloat(buffer, 63), prefix).ToString("F3");
                    mainForm.SbTextBox = this.PrepareParameter(this.ConvertByteToFloat(buffer, 67), prefix).ToString("F3");
                    mainForm.ScTextBox = this.PrepareParameter(this.ConvertByteToFloat(buffer, 71), prefix).ToString("F3");

                    mainForm.KpaTextBox = this.ConvertByteToFloat(buffer, 75).ToString("F3");
                    mainForm.KpbTextBox = this.ConvertByteToFloat(buffer, 79).ToString("F3");
                    mainForm.KpcTextBox = this.ConvertByteToFloat(buffer, 83).ToString("F3");
                }

                mainForm.PTextBox = this.PrepareParameter(this.ConvertByteToFloat(buffer, 87), prefix).ToString("F3");
                mainForm.QTextBox = this.PrepareParameter(this.ConvertByteToFloat(buffer, 91), prefix).ToString("F3");
                mainForm.STextBox = this.PrepareParameter(this.ConvertByteToFloat(buffer, 95), prefix).ToString("F3");

                float KP = this.ConvertByteToFloat(buffer, 99);
                mainForm.KpTextBox = KP.ToString("F3");

                mainForm.FTextBox = this.ConvertByteToFloat(buffer, 103).ToString("F3");

                try
                {
                    if (Ul == 0)
                    {
                        mainForm.CTextBox = "";
                        throw new IndexOutOfRangeException();
                    }
                    double C = (1000000 * I * Math.Sqrt(1 - Math.Pow(KP, 2))) / (314 * Ul * Math.Sqrt(3.0));
                    mainForm.CTextBox = C.ToString("F2");

                    double Cn = Convert.ToDouble(mainForm.CNominalTextBox);
                    if (Cn == 0) throw new IndexOutOfRangeException();
                    double percent = (Cn - C) / Cn * 100;
                    mainForm.CPercentTextBox = percent.ToString("F1");
                }
                catch
                {
                    mainForm.CPercentTextBox = "";
                }
            }
            catch (Exception ex)
            {
                MainForm.WriteLog("Ошибка в функции декодирования 31 параметра: " + ex.Message);
            }
        }

        private void DecodeEnergy(byte[] buffer)
        {
            try
            {
                float temp = this.ConvertByteToFloat(buffer, 3);
                if (Math.Abs(temp) < 1000000) mainForm.WaTextBox = (Math.Truncate(temp * 100) / 100).ToString("F2");
                else mainForm.WaTextBox = temp.ToString("R");

                temp = this.ConvertByteToFloat(buffer, 7);
                if (Math.Abs(temp) < 1000000) mainForm.WaPlusTextBox = (Math.Truncate(temp * 100) / 100).ToString("F2");
                else mainForm.WaPlusTextBox = temp.ToString("R");

                temp = this.ConvertByteToFloat(buffer, 11);
                if (Math.Abs(temp) < 1000000) mainForm.WaMinusTextBox = (Math.Truncate(temp * 100) / 100).ToString("F2");
                else mainForm.WaMinusTextBox = temp.ToString("R");

                temp = this.ConvertByteToFloat(buffer, 15);
                if (Math.Abs(temp) < 1000000) mainForm.WrTextBox = (Math.Truncate(temp * 100) / 100).ToString("F2");
                else mainForm.WrTextBox = temp.ToString("R");

                temp = this.ConvertByteToFloat(buffer, 19);
                if (Math.Abs(temp) < 1000000) mainForm.WrPlusTextBox = (Math.Truncate(temp * 100) / 100).ToString("F2");
                else mainForm.WrPlusTextBox = temp.ToString("R");

                temp = this.ConvertByteToFloat(buffer, 23);
                if (Math.Abs(temp) < 1000000) mainForm.WrMinusTextBox = (Math.Truncate(temp * 100) / 100).ToString("F2");
                else mainForm.WrMinusTextBox = temp.ToString("R");

                temp = this.ConvertByteToFloat(buffer, 27);
                if (Math.Abs(temp) < 1000000) mainForm.Wr1TextBox = (Math.Truncate(temp * 100) / 100).ToString("F2");
                else mainForm.Wr1TextBox = temp.ToString("R");

                temp = this.ConvertByteToFloat(buffer, 31);
                if (Math.Abs(temp) < 1000000) mainForm.Wr2TextBox = (Math.Truncate(temp * 100) / 100).ToString("F2");
                else mainForm.Wr2TextBox = temp.ToString("R");

                temp = this.ConvertByteToFloat(buffer, 35);
                if (Math.Abs(temp) < 1000000) mainForm.Wr3TextBox = (Math.Truncate(temp * 100) / 100).ToString("F2");
                else mainForm.Wr3TextBox = temp.ToString("R");

                temp = this.ConvertByteToFloat(buffer, 39);
                if (Math.Abs(temp) < 1000000) mainForm.Wr4TextBox = (Math.Truncate(temp * 100) / 100).ToString("F2");
                else mainForm.Wr4TextBox = temp.ToString("R");
            }
            catch (Exception ex)
            {
                MainForm.WriteLog("Ошибка в функции декодирования 31 параметра(раздел энергии): " + ex.Message);
            }
        }

        private void DecodeMainConfigParams(byte[] buffer)
        {
            try
            {
                mainForm.ProgrammVersionTextBox = ConvertByteToUInt16(buffer, 3).ToString("");
                // mainForm.NumberOfParamsTextBox = ConvertByteToUInt16(buffer, 5).ToString("");
                mainForm.NetworkAddressTextBox = ConvertByteToUInt16(buffer, 7).ToString("");

                string numberOfDevice = ConvertByteToUInt16(buffer, 11).ToString("D4");
                string yearOfDevice = ConvertByteToUInt16(buffer, 13).ToString("");
                mainForm.FactoryNumberTextBox = yearOfDevice + numberOfDevice;

                int speedA = (int)ConvertByteToUInt16(buffer, 15);
                if (speedA >= 1200)
                {
                    if (speedA == 1200) mainForm.SpeedAComboBox = 0;
                    else if (speedA == 2400) mainForm.SpeedAComboBox = 1;
                    else if (speedA == 4800) mainForm.SpeedAComboBox = 2;
                    else if (speedA == 9600) mainForm.SpeedAComboBox = 3;
                    else if (speedA == 19200) mainForm.SpeedAComboBox = 4;
                    else if (speedA == 38400) mainForm.SpeedAComboBox = 5;
                    else mainForm.SpeedAComboBox = -1;
                }
                else
                {
                    mainForm.SpeedAComboBox = speedA;
                }

                ushort measureScheme = ConvertByteToUInt16(buffer, 19);
                if (measureScheme == 1) mainForm.ConnectionSchemeTextBox = "3-ех проводная";
                else if (measureScheme == 0) mainForm.ConnectionSchemeTextBox = "4-ех проводная";

                ushort typeOfPribor = ConvertByteToUInt16(buffer, 25);
                _typeOfPribor = typeOfPribor;
                if (typeOfPribor > 700)
                {
                    int temp = typeOfPribor - 700;
                    mainForm.DeviceVersionTextBox = "ЦП8507/" + temp.ToString();
                    //mainForm.InfoAddressIECComboBoxEnabled = false;
                }
                else
                {
                    mainForm.DeviceVersionTextBox = "";
                    //mainForm.InfoAddressIECComboBoxEnabled = true;
                }

                if (typeOfPribor >= 711)
                {
                    //  mainForm.RelayGroupBoxEnabled = false;
                    mainForm.DisplayLightEnabled = false;
                }
                else
                {
                    mainForm.DisplayLightEnabled = true;
                    // mainForm.RelayGroupBoxEnabled = true;
                }

                ushort noisePercent = ConvertByteToUInt16(buffer, 29);
                mainForm.NoisePercentTrackBar = (int)noisePercent;
                mainForm.NoisePercentLabel = (noisePercent / 100.0).ToString("F2");

                mainForm.ModeBComboBox = (int)ConvertByteToUInt16(buffer, 31);

                int speedB = (int)ConvertByteToUInt16(buffer, 33);
                if (speedB == 1200) mainForm.SpeedBComboBox = 0;
                else if (speedB == 2400) mainForm.SpeedBComboBox = 1;
                else if (speedB == 4800) mainForm.SpeedBComboBox = 2;
                else if (speedB == 9600) mainForm.SpeedBComboBox = 3;
                else if (speedB == 19200) mainForm.SpeedBComboBox = 4;
                else if (speedB == 38400) mainForm.SpeedBComboBox = 5;
                else if (speedB > 7) mainForm.SpeedBComboBox = (int)buffer[34];
                else mainForm.SpeedBComboBox = speedB;
                // mainForm.SpeedBComboBox = speedB;
            }
            catch (Exception ex)
            {
                MainForm.WriteLog("Ошибка в функции декодирования основных параметров: " + ex.Message);
            }
        }

        private void DecodeModbusParams(byte[] buffer)
        {
            try
            {
                mainForm.ModbusBytesComboBox = (int)ConvertByteToUInt16(buffer, 3);
            }
            catch (Exception ex)
            {
                MainForm.WriteLog("Ошибка декодирования параметров Modbus: " + ex.Message);
            }
        }

        private void DecodeIEC101Params(byte[] buffer)
        {
            try
            {
                mainForm.IEC101Settings.iec101ASDUAddress_comboBox.SelectedIndex = (int)ConvertByteToUInt16(buffer, 3) - 1;
                mainForm.IEC101Settings.iec101InfoAddress_comboBox.SelectedIndex = (int)ConvertByteToUInt16(buffer, 5) - 2;
                mainForm.IEC101Settings.iec101Reason_comboBox.SelectedIndex = (int)ConvertByteToUInt16(buffer, 7) - 1;

                // mainForm.ASDUAddressIEC101ComboBox = (int)ConvertByteToUInt16(buffer, 3) - 1;
                // mainForm.InfoAddressIEC101ComboBox = (int)ConvertByteToUInt16(buffer, 5) - 2; //
                //  mainForm.ReasonIEC101ComboBox = (int)ConvertByteToUInt16(buffer, 7) - 1;

                ushort asduType = ConvertByteToUInt16(buffer, 9);
                if (asduType == 9) mainForm.IEC101Settings.iec101ASDUType_comboBox.SelectedIndex = 0;
                else if (asduType == 10) mainForm.IEC101Settings.iec101ASDUType_comboBox.SelectedIndex = 1;
                else if (asduType == 13) mainForm.IEC101Settings.iec101ASDUType_comboBox.SelectedIndex = 2;
                else if (asduType == 14) mainForm.IEC101Settings.iec101ASDUType_comboBox.SelectedIndex = 3;
                else if (asduType == 21) mainForm.IEC101Settings.iec101ASDUType_comboBox.SelectedIndex = 4;
                else if (asduType == 34) mainForm.IEC101Settings.iec101ASDUType_comboBox.SelectedIndex = 5;
                else if (asduType == 36) mainForm.IEC101Settings.iec101ASDUType_comboBox.SelectedIndex = 6;
            }
            catch (Exception ex)
            {
                MainForm.WriteLog("Ошибка декодирования параметров МЭК101: " + ex.Message);
            }
        }

        private void DecodeIEC104Params(byte[] buffer)
        {
            try
            {
                ushort asduType = ConvertByteToUInt16(buffer, 3);
                if (asduType == 9) mainForm.IEC104Settings.iec104ASDUType_comboBox.SelectedIndex = 0;
                else if (asduType == 10) mainForm.IEC104Settings.iec104ASDUType_comboBox.SelectedIndex = 1;
                else if (asduType == 13) mainForm.IEC104Settings.iec104ASDUType_comboBox.SelectedIndex = 2;
                else if (asduType == 14) mainForm.IEC104Settings.iec104ASDUType_comboBox.SelectedIndex = 3;
                else if (asduType == 21) mainForm.IEC104Settings.iec104ASDUType_comboBox.SelectedIndex = 4;
                else if (asduType == 34) mainForm.IEC104Settings.iec104ASDUType_comboBox.SelectedIndex = 5;
                else if (asduType == 36) mainForm.IEC104Settings.iec104ASDUType_comboBox.SelectedIndex = 6;

                mainForm.IEC104Settings.k_textBox.Text = ConvertByteToUInt16(buffer, 5).ToString();
                mainForm.IEC104Settings.w_textBox.Text = ConvertByteToUInt16(buffer, 7).ToString();
                mainForm.IEC104Settings.t1_textBox.Text = ConvertByteToUInt16(buffer, 11).ToString();
                mainForm.IEC104Settings.t2_textBox.Text = ConvertByteToUInt16(buffer, 13).ToString();
                mainForm.IEC104Settings.t3_textBox.Text = ConvertByteToUInt16(buffer, 15).ToString();
            }
            catch (Exception ex)
            {
                MainForm.WriteLog("Ошибка декодирования параметров МЭК104: " + ex.Message);
            }
        }

        private void DecodeIEC101Values(byte[] buffer)
        {
            try
            {
                byte[] floatBuffer = new byte[4];

                floatBuffer[0] = buffer[6];
                floatBuffer[1] = buffer[5];
                floatBuffer[2] = buffer[4];
                floatBuffer[3] = buffer[3];

                UInt32 temp = BitConverter.ToUInt32(floatBuffer, 0);

                for (int i = 0; i < 31; i++)
                {
                    bool flag = ((temp & ((UInt32)1 << i)) != 0);
                    mainForm.SetIEC101_DGV(i, flag);
                }
            }
            catch (Exception ex)
            {
                MainForm.WriteLog("Ошибка декодирования параметров МЭК: " + ex.Message);
            }
        }

        private void DecodeIEC104Values(byte[] buffer)
        {
            try
            {
                byte[] floatBuffer = new byte[4];

                floatBuffer[0] = buffer[6];
                floatBuffer[1] = buffer[5];
                floatBuffer[2] = buffer[4];
                floatBuffer[3] = buffer[3];

                UInt32 temp = BitConverter.ToUInt32(floatBuffer, 0);

                for (int i = 0; i < 31; i++)
                {
                    bool flag = ((temp & ((UInt32)1 << i)) != 0);
                    mainForm.SetIEC104_DGV(i, flag);
                }
            }
            catch (Exception ex)
            {
                MainForm.WriteLog("Ошибка декодирования параметров МЭК: " + ex.Message);
            }
        }


        private void DecodeRelay_1_2_Params(byte[] buffer)
        {
            try
            {
                relaySettings[0].mode = ConvertByteToUInt16(buffer, 3);
                relaySettings[0].param = ConvertByteToUInt16(buffer, 5);
                relaySettings[0].ustavka = ConvertByteToInt16(buffer, 7);
                relaySettings[0].ping = ConvertByteToUInt16(buffer, 9);

                relaySettings[1].mode = ConvertByteToUInt16(buffer, 11);
                relaySettings[1].param = ConvertByteToUInt16(buffer, 13);
                relaySettings[1].ustavka = ConvertByteToInt16(buffer, 15);
                relaySettings[1].ping = ConvertByteToUInt16(buffer, 17);

                mainForm.RelayNumberComboBox = 0;
                mainForm.RelayModeComboBox = relaySettings[0].mode;
                mainForm.RelayUstavkaTextBox = relaySettings[0].ustavka.ToString();
                mainForm.RelayPingTextBox = relaySettings[0].ping.ToString();
                mainForm.RelayParanComboBox = (int)relaySettings[0].param;
            }
            catch (Exception ex)
            {
                MainForm.WriteLog("Ошибка декодирования параметров реле: " + ex.Message);
            }
        }

        private void DecodeRelay_3_Params(byte[] buffer)
        {
            try
            {
                relaySettings[2].mode = ConvertByteToUInt16(buffer, 3);
                relaySettings[2].param = ConvertByteToUInt16(buffer, 5);
                relaySettings[2].ustavka = ConvertByteToInt16(buffer, 7);
                relaySettings[2].ping = ConvertByteToUInt16(buffer, 9);

                mainForm.RelayNumberComboBox = 0;
                mainForm.RelayModeComboBox = relaySettings[0].mode;
                mainForm.RelayUstavkaTextBox = relaySettings[0].ustavka.ToString();
                mainForm.RelayPingTextBox = relaySettings[0].ping.ToString();
                mainForm.RelayParanComboBox = (int)relaySettings[0].param;
            }
            catch (Exception ex)
            {
                MainForm.WriteLog("Ошибка декодирования параметров реле: " + ex.Message);
            }
        }

        private void DecodeRelay_1_2_Gistereziss(byte[] buffer)
        {
            try
            {
                relaySettings[0].gisterezis = this.ConvertByteToFloat(buffer, 3);
                relaySettings[1].gisterezis = this.ConvertByteToFloat(buffer, 7);

                mainForm.RelayGisterezisTextBox = relaySettings[0].gisterezis.ToString("F4");
            }
            catch (Exception ex)
            {
                MainForm.WriteLog("Ошибка декодирования гистерезиса: " + ex.Message);
            }
        }

        private void DecodeRelay_3_Gistereziss(byte[] buffer)
        {
            try
            {
                relaySettings[2].gisterezis = this.ConvertByteToFloat(buffer, 3);

                mainForm.RelayGisterezisTextBox = relaySettings[0].gisterezis.ToString("F4");
            }
            catch (Exception ex)
            {
                MainForm.WriteLog("Ошибка декодирования гистерезиса: " + ex.Message);
            }
        }

        private void DecodeEnergyRows(byte[] buffer)
        {
            try
            {
                mainForm.EnergyRow1ComboBox = (int)ConvertByteToUInt16(buffer, 3);
                mainForm.EnergyRow2ComboBox = (int)ConvertByteToUInt16(buffer, 5);
            }
            catch (Exception ex)
            {
                MainForm.WriteLog("Ошибка декодирования строк энергии: " + ex.Message);
            }
        }

        private void DecodeIndicationInfo(byte[] buffer)
        {
            try
            {
                mainForm.DisplayLightComboBox = (int)ConvertByteToUInt16(buffer, 3);
                mainForm.NumberOfRowsComboBox = (int)ConvertByteToUInt16(buffer, 11);
            }
            catch (Exception ex)
            {
                MainForm.WriteLog("Ошибка декодирования параметров индикации: " + ex.Message);
            }
        }

        private void DecodeDisplayedRows(byte[] buffer)
        {
            try
            {
                mainForm.Row1IndParamComboBox = -1;

                mainForm.Row1IndParamComboBox = SetRowParamFromComboBox(ConvertByteToUInt16(buffer, 3));
                mainForm.Row2IndParamComboBox = SetRowParamFromComboBox(ConvertByteToUInt16(buffer, 5));
                mainForm.Row3IndParamComboBox = SetRowParamFromComboBox(ConvertByteToUInt16(buffer, 7));

                if (mainForm.NumberOfRowsComboBox == 1)
                {
                    mainForm.Row4IndParamComboBox = -1;

                    mainForm.Row4IndParamComboBox = SetRowParamFromComboBox(ConvertByteToUInt16(buffer, 15));
                    mainForm.Row5IndParamComboBox = SetRowParamFromComboBox(ConvertByteToUInt16(buffer, 17));
                    mainForm.Row6IndParamComboBox = SetRowParamFromComboBox(ConvertByteToUInt16(buffer, 19));
                }
            }
            catch (Exception ex)
            {
                MainForm.WriteLog("Ошибка декодирования отображаемых строк: " + ex.Message);
            }
            //else
            //{
            //    mainForm.Row4IndParamComboBox = -1;
            //    mainForm.Row5IndParamComboBox = -1;
            //    mainForm.Row6IndParamComboBox = -1;
            //}
        }

        private int SetRowParamFromComboBox(int selectedIndex)
        {
            if (selectedIndex > (int)MainForm.PARAMS.dUminus) selectedIndex -= 10;
            return selectedIndex;
        }


        private void DecodeDisplayedUstavki(byte[] buffer)
        {
            try
            {
                mainForm.Param1MaxUstavkaTextBox = "";

                mainForm.Param1MaxUstavkaTextBox = this.ConvertByteToInt16(buffer, 3).ToString();
                mainForm.Param1MinUstavkaTextBox = this.ConvertByteToInt16(buffer, 5).ToString();

                mainForm.Param2MaxUstavkaTextBox = this.ConvertByteToInt16(buffer, 7).ToString();
                mainForm.Param2MinUstavkaTextBox = this.ConvertByteToInt16(buffer, 9).ToString();

                mainForm.Param3MaxUstavkaTextBox = this.ConvertByteToInt16(buffer, 11).ToString();
                mainForm.Param3MinUstavkaTextBox = this.ConvertByteToInt16(buffer, 13).ToString();

                if (mainForm.Row4IndParamComboBoxEnabled) // 6 параметров
                {
                    mainForm.Param4MaxUstavkaTextBox = "";

                    mainForm.Param4MaxUstavkaTextBox = this.ConvertByteToInt16(buffer, 15).ToString();
                    mainForm.Param4MinUstavkaTextBox = this.ConvertByteToInt16(buffer, 17).ToString();

                    mainForm.Param5MaxUstavkaTextBox = this.ConvertByteToInt16(buffer, 19).ToString();
                    mainForm.Param5MinUstavkaTextBox = this.ConvertByteToInt16(buffer, 21).ToString();

                    mainForm.Param6MaxUstavkaTextBox = this.ConvertByteToInt16(buffer, 23).ToString();
                    mainForm.Param6MinUstavkaTextBox = this.ConvertByteToInt16(buffer, 25).ToString();
                }
            }
            catch (Exception ex)
            {
                MainForm.WriteLog("Ошибка декодирования уставок " + ex.Message);
            }
        }

        private void DecodeCoefs(byte[] buffer)
        {
            try
            {
                mainForm.U1ComboBox = this.ConvertByteToFloat(buffer, 3).ToString("F1");
                mainForm.U2ComboBox = this.ConvertByteToFloat(buffer, 7).ToString("F1");
                mainForm.I1ComboBox = this.ConvertByteToFloat(buffer, 11).ToString("F1");
                mainForm.I2ComboBox = this.ConvertByteToFloat(buffer, 15).ToString("F1");
            }
            catch (Exception ex)
            {
                MainForm.WriteLog("Ошибка декодирования коэф. трансформации: " + ex.Message);
            }
        }

        private void Decode31ParamsForChange(byte[] buffer)
        {
            try
            {
                if (storedMeasureSheme == (int)MeasureScheme._4_WIRED)
                {
                    mainForm.UaChangeLabel = "--------";
                    mainForm.UbChangeLabel = "--------";
                    mainForm.UcChangeLabel = "--------";

                    mainForm.PaChangeLabel = "--------";
                    mainForm.PbChangeLabel = "--------";
                    mainForm.PcChangeLabel = "--------";
                }
                else
                {
                    mainForm.UaChangeLabel = this.ConvertByteToFloat(buffer, 3).ToString("F3");
                    mainForm.UbChangeLabel = this.ConvertByteToFloat(buffer, 7).ToString("F3");
                    mainForm.UcChangeLabel = this.ConvertByteToFloat(buffer, 11).ToString("F3");

                    mainForm.PaChangeLabel = this.ConvertByteToFloat(buffer, 39).ToString("F2");
                    mainForm.PbChangeLabel = this.ConvertByteToFloat(buffer, 43).ToString("F2");
                    mainForm.PcChangeLabel = this.ConvertByteToFloat(buffer, 47).ToString("F2");
                }

                mainForm.IaChangeLabel = this.ConvertByteToFloat(buffer, 15).ToString("F4");
                mainForm.IbChangeLabel = this.ConvertByteToFloat(buffer, 19).ToString("F4");
                mainForm.IcChangeLabel = this.ConvertByteToFloat(buffer, 23).ToString("F4");

                mainForm.UabChangeLabel = this.ConvertByteToFloat(buffer, 27).ToString("F3");
                mainForm.UbcChangeLabel = this.ConvertByteToFloat(buffer, 31).ToString("F3");
                mainForm.UcaChangeLabel = this.ConvertByteToFloat(buffer, 35).ToString("F3");

                mainForm.PChangeLabel = this.ConvertByteToFloat(buffer, 87).ToString("F2");
                mainForm.QChangeLabel = this.ConvertByteToFloat(buffer, 91).ToString("F2");

                mainForm.FChangeLabel = this.ConvertByteToFloat(buffer, 103).ToString("F3");
            }
            catch (Exception ex)
            {
                MainForm.WriteLog("Ошибка декодирования 31 параметра(настройка метрологии): " + ex.Message);
            }
        }

        private void DecodeImpulseOuts(byte[] buffer)
        {
            try
            {
                mainForm.SelectActiveImpOutRadioButton = (int)buffer[3];
                mainForm.SelectReactiveImpOutRadioButton = (int)buffer[4];
            }
            catch (Exception ex)
            {
                MainForm.WriteLog("Ошибка декодирования импульсных выходов: " + ex.Message);
            }
        }

        private void DecodeImpulseWtH(byte[] buffer)
        {
            try
            {
                byte[] uint32Buffer = new byte[4];

                uint32Buffer[0] = buffer[3 + 3];
                uint32Buffer[1] = buffer[3 + 2];
                uint32Buffer[2] = buffer[3 + 1];
                uint32Buffer[3] = buffer[3 + 0];

                UInt32 temp = BitConverter.ToUInt32(uint32Buffer, 0);
                mainForm.ImpulseWtHComboBox = temp.ToString();
            }
            catch (Exception ex)
            {
                MainForm.WriteLog("Ошибка декодирования постоянной счетчика: " + ex.Message);
            }
        }

        private void DecodeDateTime(byte[] buffer)
        {
            try
            {
                int sec = buffer[3];
                int min = buffer[4];
                int hour = buffer[5];
                int day = buffer[7];
                int mon = buffer[8];
                int year = (int)ConvertByteToUInt16(buffer, 9);

                DateTime dt = new DateTime(year, mon, day, hour, min, sec);
                DateTime dtNow = DateTime.Now;
                var difference = (dt - dtNow).TotalSeconds;
                mainForm.DifferenceDTTextBox = difference.ToString("F0") + " сек.";

                mainForm.DeviceDateTimeTextBox = dt.ToString(ProtocolGlobals.DATE_TIME_FORMAT);
                mainForm.DateTimeTextBox = dtNow.ToString(ProtocolGlobals.DATE_TIME_FORMAT);
            }
            catch (Exception ex)
            {
                MainForm.WriteLog("Ошибка декодирования даты и времени: " + ex.Message);
            }
        }

        private void DecodeIPAddress(byte[] buffer)
        {
            try
            {
                string ip = buffer[3].ToString() + "." + buffer[4].ToString() + "." + buffer[5].ToString() + "." + buffer[6].ToString();
                mainForm.IPAddressTextBox = ip;

                string mask = buffer[7].ToString() + "." + buffer[8].ToString() + "." + buffer[9].ToString() + "." + buffer[10].ToString();
                mainForm.mask_textBox.Text = mask;

                string gateway = buffer[11].ToString() + "." + buffer[12].ToString() + "." + buffer[13].ToString() + "." + buffer[14].ToString();
                mainForm.gateway_textBox.Text = gateway;
            }
            catch (Exception ex)
            {
                MainForm.WriteLog("Ошибка ip адреса: " + ex.Message);
            }
        }


        private void DecodeTurnOnOffJournal(byte[] buffer)
        {
            int sec, min, hour, day, mon, year;
            try
            {
                sec = (int)buffer[5];
                min = (int)buffer[6];
                hour = (int)buffer[7];
                day = (int)buffer[8];
                mon = (int)buffer[9];
                year = (int)buffer[10] + 2000;

                DateTime dt = new DateTime(year, mon, day, hour, min, sec);

                sec = (int)buffer[11];
                min = (int)buffer[12];
                hour = (int)buffer[13];
                day = (int)buffer[14];
                mon = (int)buffer[15];
                year = (int)buffer[16] + 2000;

                DateTime dt2 = new DateTime(year, mon, day, hour, min, sec);

                mainForm.SetEventDataGrid(dt2.ToString(ProtocolGlobals.DATE_TIME_FORMAT), ProtocolGlobals.ON_BUTTON);
                mainForm.SetEventDataGrid(dt.ToString(ProtocolGlobals.DATE_TIME_FORMAT), ProtocolGlobals.OFF_BUTTON);
            }
            catch (Exception ex)
            {
                MainForm.WriteLog("Ошибка декодирования журнала вкл./выкл.: " + ex.Message);
            }
        }

        private void DecodeChangeCoefsJournal(byte[] buffer)
        {
            try
            {
                int sec = (int)buffer[5];
                int min = (int)buffer[6];
                int hour = (int)buffer[7];
                int day = (int)buffer[8];
                int mon = (int)buffer[9];
                int year = (int)buffer[10] + 2000;

                DateTime dt = new DateTime(year, mon, day, hour, min, sec);

                float U1 = BitConverter.ToSingle(buffer, 11);
                float I1 = BitConverter.ToSingle(buffer, 15);
                float U2 = BitConverter.ToSingle(buffer, 19);

                float wa_plus = BitConverter.ToSingle(buffer, 23);
                float wa_minus = BitConverter.ToSingle(buffer, 27);

                float wr1 = BitConverter.ToSingle(buffer, 31);
                float wr2 = BitConverter.ToSingle(buffer, 35);
                float wr3 = BitConverter.ToSingle(buffer, 39);
                float wr4 = BitConverter.ToSingle(buffer, 43);

                float wa = wa_plus + wa_minus;

                float wr_plus = wr1 + wr2;
                float wr_minus = wr3 + wr4;
                float wr = wr_plus + wr_minus;

                int counter = 0;
                float maxCalculatedWatt = U1 * I1 * (float)Math.Sqrt(3.0) * 3;
                int days = 0;

                while (days < 180)
                {
                    days = (int)(999999 / maxCalculatedWatt / 24);
                    maxCalculatedWatt /= 1000;
                    counter++;
                }
                counter--;

                string prefix = "kWt*h";
                if (counter == 2) prefix = "MWt*h";
                else if (counter == 3) prefix = "GWt*h";

                mainForm.SetEventDataGrid(dt.ToString(ProtocolGlobals.DATE_TIME_FORMAT), "U1: " + U1.ToString("F2") + "    " + "I1: " + I1.ToString("F2")
                    + Environment.NewLine + "U2: " + U2.ToString("F2") + Environment.NewLine +
                    "Wa = " + wa.ToString("F2") + " " + prefix + Environment.NewLine +
                    "Wa+ = " + wa_plus.ToString("F2") + " " + prefix + Environment.NewLine +
                    "Wa- = " + wa_minus.ToString("F2") + " " + prefix + Environment.NewLine +
                    "Wr = " + wr.ToString("F2") + " " + prefix + Environment.NewLine +
                    "Wr+ = " + wr_plus.ToString("F2") + " " + prefix + Environment.NewLine +
                    "Wr- = " + wr_minus.ToString("F2") + " " + prefix + Environment.NewLine +
                    "Wr1 = " + wr1.ToString("F2") + " " + prefix + Environment.NewLine +
                    "Wr2 = " + wr2.ToString("F2") + " " + prefix + Environment.NewLine +
                    "Wr3 = " + wr3.ToString("F2") + " " + prefix + Environment.NewLine +
                    "Wr4 = " + wr4.ToString("F2") + " " + prefix);
            }
            catch (Exception ex)
            {
                MainForm.WriteLog("Ошибка декодирования журнала изменения коэфициентов: " + ex.Message);
            }
        }

        private void DecodeCJECJournal(byte[] buffer, EnquryType enqType)
        {
            try
            {
                int sec = (int)buffer[5];
                int min = (int)buffer[6];
                int hour = (int)buffer[7];
                int day = (int)buffer[8];
                int mon = (int)buffer[9];
                int year = (int)buffer[10] + 2000;

                int Event = (int)buffer[11];

                DateTime dt = new DateTime(year, mon, day, hour, min, sec);

                string text = "";
                if (enqType == EnquryType.INQ_READ_CURRENT_WITHOUT_VOLTAGE_DT_JOURNAL)
                {
                    if (Event == 0) text = "Фаза A";
                    else if (Event == 1) text = "Фаза A";
                    else if (Event == 2) text = "Фаза A";
                }
                else if (enqType == EnquryType.INQ_READ_ERASE_LOG_DT_JOURNAL)
                {
                    if (Event == 1) text = "Очистка журнала событий";
                    else if (Event == 2) text = "Очистка журнала энергии";
                    else if (Event == 3) text = "Очистка журнала ПКЭ";
                }
                else if (enqType == EnquryType.INQ_READ_JUMPER_DT_JOURNAL)
                {
                    if (Event == 0) text = "Перемычка снята";
                    else if (Event == 170) text = "Перемычка установлена";
                }
                else if (enqType == EnquryType.INQ_READ_CLOCK_CHECK_DT_JOURNAL)
                {
                    if (Event == 0) text = "Режим проверки выключен";
                    else if (Event == 170) text = "Режим проверки включен";
                }

                mainForm.SetEventDataGrid(dt.ToString(ProtocolGlobals.DATE_TIME_FORMAT), text);
            }
            catch (Exception ex)
            {
                MainForm.WriteLog("Ошибка декодирования CJE журнала: " + ex.Message);
            }
        }

        private void DecodeSetDTJournal(byte[] buffer)
        {
            try
            {
                int sec = (int)buffer[5];
                int min = (int)buffer[6];
                int hour = (int)buffer[7];
                int day = (int)buffer[8];
                int mon = (int)buffer[9];
                int year = (int)buffer[10] + 2000;

                int difference = BitConverter.ToInt32(buffer, 11);
                string znak = "  ";
                if (difference < 0)
                {
                    difference *= -1;
                    znak = "- ";
                }

                TimeSpan t = TimeSpan.FromSeconds(difference);

                DateTime dt = new DateTime(year, mon, day, hour, min, sec);

                year = t.Days / 365;
                mon = (t.Days - year * 365) / 30;
                day = (t.Days - year * 365) - mon * 30;
                hour = t.Hours;
                min = t.Minutes;
                sec = t.Seconds;

                string output = String.Format("{0:D2}.{1:D2}.{2:D2}  {3:D2}:{4:D2}:{5:D2}",
                    day, mon, year, hour, min, sec);

                mainForm.SetEventDataGrid(dt.ToString(ProtocolGlobals.DATE_TIME_FORMAT), znak + output);
            }
            catch (Exception ex)
            {
                MainForm.WriteLog("Ошибка декодирования журнала eстановки даты: " + ex.Message);
            }
        }

        private void DecodeTarifsChange(byte[] buffer)
        {
            try
            {
                int sec = (int)buffer[5];
                int min = (int)buffer[6];
                int hour = (int)buffer[7];
                int day = (int)buffer[8];
                int mon = (int)buffer[9];
                int year = (int)buffer[10] + 2000;

                DateTime dt = new DateTime(year, mon, day, hour, min, sec);

                mainForm.SetEventDataGrid(dt.ToString(ProtocolGlobals.DATE_TIME_FORMAT), "");
            }
            catch (Exception ex)
            {
                MainForm.WriteLog("Ошибка декодирования журнала изменения тарифов: " + ex.Message);
            }
        }

        private void DecodeSecondsDifereceJournal(byte[] buffer)
        {
            try
            {
                uint difference = BitConverter.ToUInt32(buffer, 6);
                uint minutes = difference / 60;
                uint seconds = difference - (minutes * 60);
                mainForm.SetEventDataGrid((buffer[5] + 2000).ToString() + "г.", minutes.ToString() + "мин. " + seconds.ToString() + "сек.");
            }
            catch (Exception ex)
            {
                MainForm.WriteLog("Ошибка декодирования журнала коррекции часов: " + ex.Message);
            }
        }


        private void DecodeAnalogOutsRows(byte[] buffer)
        {
            try
            {
                mainForm.Row1AnalogOutComboBox = (int)ConvertByteToUInt16(buffer, 3);
                mainForm.Row2AnalogOutComboBox = (int)ConvertByteToUInt16(buffer, 5);
                mainForm.Row3AnalogOutComboBox = (int)ConvertByteToUInt16(buffer, 7);
            }
            catch (Exception ex)
            {
                MainForm.WriteLog("Ошибка строк аналоговых выходов: " + ex.Message);
            }
        }

        private void DecodeHarmonics(byte[] buffer, int fase, int table)
        {
            try
            {
                for (int i = 0; i < 50; i++)
                {
                    int shift = 3 + i * 2;
                    Int16 value = ConvertByteToInt16(buffer, shift);
                    float singleValue = (float)value / 100;
                    mainForm.SetHarmonicsDataGrid(fase, i, singleValue.ToString("F2"), table);
                }
            }
            catch (Exception ex)
            {
                MainForm.WriteLog("Ошибка в функции декодирования гармоник: " + ex.Message);
            }
        }


        private void DecodeInstantPKE(byte[] buffer)
        {
            try
            {
                mainForm.K0UTextBox = ((float)(ConvertByteToInt16(buffer, 3)) / 100).ToString("F3");
                mainForm.K2UTextBox = ((float)(ConvertByteToInt16(buffer, 5)) / 100).ToString("F3");
                mainForm.dFTextBox = ((float)(ConvertByteToInt16(buffer, 7)) / 100).ToString("F3");
                mainForm.dUplusTextBox = ((float)(ConvertByteToInt16(buffer, 9)) / 100).ToString("F3");
                mainForm.dUminusTextBox = ((float)(ConvertByteToInt16(buffer, 11)) / 100).ToString("F3");
                mainForm.K0ITextBox = ((float)(ConvertByteToInt16(buffer, 13)) / 100).ToString("F3");
                mainForm.K2ITextBox = ((float)(ConvertByteToInt16(buffer, 15)) / 100).ToString("F3");
                mainForm.dIplusTextBox = ((float)(ConvertByteToInt16(buffer, 17)) / 100).ToString("F3");
                mainForm.dIminusTextBox = ((float)(ConvertByteToInt16(buffer, 19)) / 100).ToString("F3");
            }
            catch (Exception ex)
            {
                MainForm.WriteLog("Ошибка в функции декодирования 31 параметра: " + ex.Message);
            }
        }

        private void DecodeKU0Journal(byte[] buffer)
        {
            try
            {
                int sec = (int)buffer[2];
                int min = (int)buffer[3];
                int hour = (int)buffer[4];
                int day = (int)buffer[5];
                int mon = (int)buffer[6];
                int year = (int)buffer[7] + 2000;

                DateTime dt = new DateTime(year, mon, day, hour, min, sec);

                string temp = "Данные отсутсвуют";
                if (buffer[8] != 0xFF && buffer[9] != 0xFF && buffer[10] != 0xFF && buffer[11] != 0xFF)
                {
                    temp = this.ConvertByteToFloat(buffer, 8).ToString("F3");
                }

                mainForm.SetPKEDataGrid("-1", dt.ToString(ProtocolGlobals.DATE_TIME_FORMAT), temp);
            }
            catch (Exception ex)
            {
                MainForm.WriteLog("Ошибка декодирования журнала PKE: " + ex.Message);
            }
        }

        private void DecodeDipVoltagePKE(byte[] buffer)
        {
            try
            {
                int byteIndex = 3;
                for (int row = 0; row < 5; row++)
                {
                    for (int column = 0; column < 6; column++)
                    {
                        int data = (int)ConvertByteToUInt16(buffer, byteIndex);
                        byteIndex += 2;

                        mainForm.SetDIPVoltagePKEDataGrid(row, column, data);
                    }
                }
            }
            catch (Exception ex)
            {
                MainForm.WriteLog("Ошибка декодирования журнала PKE: " + ex.Message);
            }
        }

        private void DecodeNoVoltagePKE(byte[] buffer)
        {
            try
            {
                int byteIndex = 3;
                for (int column = 0; column < 6; column++)
                {
                    int data = (int)ConvertByteToUInt16(buffer, byteIndex);
                    byteIndex += 2;

                    mainForm.SetNOVoltagePKEDataGrid(0, column, data);
                }
            }
            catch (Exception ex)
            {
                MainForm.WriteLog("Ошибка декодирования журнала PKE: " + ex.Message);
            }
        }

        private void DecodeOverVoltagePKE(byte[] buffer)
        {
            try
            {
                int byteIndex = 3;

                int data = (int)ConvertByteToUInt16(buffer, byteIndex);

                mainForm.SetOverVoltagePKEDataGrid(0, 0, data);
            }
            catch (Exception ex)
            {
                MainForm.WriteLog("Ошибка декодирования журнала PKE: " + ex.Message);
            }
        }

        private void UpdatePrefix(float U1, float I1)
        {
            string prefix = "";
            ushort unitShift = 0;

            unitShift = CalculateUnitShfit(U1, false);  // / sqrt(3.0) // param == Ua || param == Ub || param == Uc || param == Umd || param == U0
            if (unitShift == 0) prefix = "В";
            else if (unitShift == 1) prefix = "кВ";
            else if (unitShift == 2) prefix = "МВ";
            else if (unitShift == 3) prefix = "ГВ";

            mainForm.UaLabel = "Ua, " + prefix;
            mainForm.UbLabel = "Ub, " + prefix;
            mainForm.UcLabel = "Uc, " + prefix;
            mainForm.UfLabel = "Uфср, " + prefix;

            mainForm.UabLabel = "Uab, " + prefix;
            mainForm.UbcLabel = "Ubc, " + prefix;
            mainForm.UcaLabel = "Uca, " + prefix;
            mainForm.UlLabel = "Uлср, " + prefix;

            mainForm.U0Label = "U0, " + prefix;


            unitShift = CalculateUnitShfit(I1, true); // param == Ia || param == Ib || param == Ic || param == Imd)
            if (unitShift == 0) prefix = "A";
            else if (unitShift == 1) prefix = "кA";
            else if (unitShift == 2) prefix = "МA";
            else if (unitShift == 3) prefix = "ГA";

            mainForm.IaLabel = "Ia, " + prefix;
            mainForm.IbLabel = "Ib, " + prefix;
            mainForm.IcLabel = "Ic, " + prefix;
            mainForm.ILabel = "Iср, " + prefix;

            mainForm.I0Label = "I0, " + prefix;


            /* param == Pa || param == Pb || param == Pc ||
                    param == Qa || param == Qb || param == Qc ||
                    param == Sa || param == Sb || param == Sc) */
            unitShift = CalculateUnitShfit((U1 * I1) * (float)Math.Sqrt(3.0), false); // / sqrt(3.0)
            if (unitShift == 0) prefix = "Вт";
            else if (unitShift == 1) prefix = "кВт";
            else if (unitShift == 2) prefix = "МВт";
            else if (unitShift == 3) prefix = "ГВт";

            mainForm.PaLabel = "Pa, " + prefix;
            mainForm.PbLabel = "Pb, " + prefix;
            mainForm.PcLabel = "Pc, " + prefix;
            mainForm.PLabel = "P, " + prefix;

            if (unitShift == 0) prefix = "вар";
            else if (unitShift == 1) prefix = "квар";
            else if (unitShift == 2) prefix = "Мвар";
            else if (unitShift == 3) prefix = "Гвар";

            mainForm.QaLabel = "Qa, " + prefix;
            mainForm.QbLabel = "Qb, " + prefix;
            mainForm.QcLabel = "Qc, " + prefix;
            mainForm.QLabel = "Q, " + prefix;

            if (unitShift == 0) prefix = "ВА";
            else if (unitShift == 1) prefix = "кВА";
            else if (unitShift == 2) prefix = "МВА";
            else if (unitShift == 3) prefix = "ГВА";

            mainForm.SaLabel = "Sa, " + prefix;
            mainForm.SbLabel = "Sb, " + prefix;
            mainForm.ScLabel = "Sc, " + prefix;
            mainForm.SLabel = "S, " + prefix;


            if (unitShift == 0) prefix = "Вт·ч";
            else if (unitShift == 1) prefix = "кВт·ч";
            else if (unitShift == 2) prefix = "МВт·ч";
            else if (unitShift == 3) prefix = "ГВт·ч";

            mainForm.WaLabel = "Wa, " + prefix;
            mainForm.WaPlusLabel = "Wa+, " + prefix;
            mainForm.WaMinusLabel = "Wa-, " + prefix;

            if (unitShift == 0) prefix = "вар·ч";
            else if (unitShift == 1) prefix = "квар·ч";
            else if (unitShift == 2) prefix = "Мвар·ч";
            else if (unitShift == 3) prefix = "Гвар·ч";

            mainForm.WrLabel = "Wr, " + prefix;
            mainForm.WrPlusLabel = "Wr+, " + prefix;
            mainForm.WrMinusLabel = "Wr-, " + prefix;

            mainForm.Wr1Label = "Wr1, " + prefix;
            mainForm.Wr2Label = "Wr2, " + prefix;
            mainForm.Wr3Label = "Wr3, " + prefix;
            mainForm.Wr4Label = "Wr4, " + prefix;
        }

        public void DecodeUppAngleCoeffs(byte[] buffer)
        {
            try
            {
                int byteIndex = 3;
                for (int i = 0; i < 12; i++)
                {
                    mainForm.uppConfigurator.coeffs.angle[i] = ConvertByteToUInt16(buffer, byteIndex);
                    byteIndex += 2;
                }
            }
            catch (Exception ex)
            {
                // MainForm.WriteLog("Ошибка декодирования 31 параметра(настройка метрологии): " + ex.Message);
            }
        }

        public void DecodeUppOtherCoeffs(byte[] buffer)
        {
            try
            {
                int byteIndex = 3;

                for (int i = 0; i < 12; i++)
                {
                    mainForm.uppConfigurator.coeffs.AMP[i] = this.ConvertByteToFloat(buffer, byteIndex);
                    byteIndex += 4;
                }
                //byteIndex += 4;

                mainForm.uppConfigurator.coeffs.freq = this.ConvertByteToFloat(buffer, byteIndex);
                byteIndex += 4;

                for (int i = 0; i < 2; i++)
                {
                    mainForm.uppConfigurator.coeffs.ADCmA_zero[i] = this.ConvertByteToFloat(buffer, byteIndex);
                    byteIndex += 4;
                }

                for (int i = 0; i < 2; i++)
                {
                    mainForm.uppConfigurator.coeffs.ADCmA_top[i] = this.ConvertByteToFloat(buffer, byteIndex);
                    byteIndex += 4;
                }

                for (int i = 0; i < 2; i++)
                {
                    mainForm.uppConfigurator.coeffs.DACmA_zero[i] = this.ConvertByteToFloat(buffer, byteIndex);
                    byteIndex += 4;
                }

                for (int i = 0; i < 2; i++)
                {
                    mainForm.uppConfigurator.coeffs.DACmA_top[i] = this.ConvertByteToFloat(buffer, byteIndex);
                    byteIndex += 4;
                }

                mainForm.uppConfigurator.updateText6();

            }
            catch (Exception ex)
            {
                // MainForm.WriteLog("Ошибка декодирования 31 параметра(настройка метрологии): " + ex.Message);
            }
        }

        public ushort CalculateUnitShfit(float param, bool isCurrent) /*    Тут считаем описаный выше сдвиг по коэф. трансформации    */
        {
            ushort tmp = 769; // значение взято из РЭ, стр.20
            if (isCurrent) tmp = 8000;
            ushort shift = 0;
            while ((param >= 1000) && (tmp < 1000))
            {
                param /= 1000;
                shift++;
            }
            if (param > tmp)
            {
                param /= 1000;
                shift++;
            }
            return shift;
        }

        public float PrepareParameter(float param, ushort prefix)
        {
            //  в зависимости от префикса преобразуем число, чтобы оно вместилось в 4 цифры
            if (prefix == 1)
            {
                param /= 1000;
            }
            else if (prefix == 2)
            {
                param /= 1000000;
            }
            else if (prefix == 3)
            {
                param /= 1000000000;
            }

            //if (param < 10) param = round((double)param * 1000) / 1000;
            //else if (param < 100) param = round((double)param * 100) / 100;
            //else if (param < 1000) param = round((double)param * 10) / 10;

            return param;
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

        public static ushort ConvertByteToUInt16(byte[] buffer, int offset)
        {
            try
            {
                if (offset < buffer.Length && offset + 2 <= buffer.Length)
                {
                    byte[] floatBuffer = new byte[2];

                    floatBuffer[0] = buffer[offset + 1];
                    floatBuffer[1] = buffer[offset + 0];

                    return BitConverter.ToUInt16(floatBuffer, 0);
                }
            }
            catch (Exception ex)
            {
                MainForm.WriteLog("Ошибка конвертации Byte To Uint16: " + ex.Message);
            }
            return 0;
        }

        private short ConvertByteToInt16(byte[] buffer, int offset)
        {
            try
            {
                if (offset < buffer.Length && offset + 2 <= buffer.Length)
                {
                    byte[] floatBuffer = new byte[2];

                    floatBuffer[0] = buffer[offset + 1];
                    floatBuffer[1] = buffer[offset + 0];

                    return BitConverter.ToInt16(floatBuffer, 0);
                }
            }
            catch (Exception ex)
            {
                MainForm.WriteLog("Ошибка конвертации Byte To Uint16: " + ex.Message);
            }
            return -1;
        }


    }
}

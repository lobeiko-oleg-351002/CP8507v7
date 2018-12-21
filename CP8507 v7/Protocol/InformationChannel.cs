using System;
using System.Collections.Generic;
using System.Text;

namespace CP8507_v7
{
    public class InformationChannel : Protocol
    {
        public InformationChannel(MainForm form)
        {
            mainForm = form;
        }

        public override bool CheckProtocol(byte[] buffer)
        {
            if (buffer.Length > 5 && buffer[0] == 0x55 && buffer[1] == 0xAA && buffer[2] == 0x01 && buffer[3] == 0x80 && buffer[4] == buffer.Length - 2)
            {
                return true;
            }
            else
                return false;
        }

        public override void ProcessPackage(byte[] buffer)
        {
            Array.Resize(ref buffer, buffer.Length - 2);
            if (base.CheckCRC(buffer))
            {
                this.DecodeInfo(buffer);
            }
            else
            {
                mainForm.StatusLabel = ProtocolGlobals.CRC_ERROR_MESSAGE;

                this.ClearFields();
            }
        }

        public void ClearFields()
        {
            mainForm.ProgVersionInfoTextBox = "";
            mainForm.NetAddressInfoTextBox = "";

            mainForm.UaInfoTextBox = "";
            mainForm.UbInfoTextBox = "";
            mainForm.UcInfoTextBox = "";
            mainForm.UfInfoTextBox = "";

            mainForm.UabInfoTextBox = "";
            mainForm.UbcInfoTextBox = "";
            mainForm.UcaInfoTextBox = "";
            mainForm.UlInfoTextBox = "";

            mainForm.IaInfoTextBox = "";
            mainForm.IbInfoTextBox = "";
            mainForm.IcInfoTextBox = "";
            mainForm.IInfoTextBox = "";

            mainForm.PaInfoTextBox = "";
            mainForm.PbInfoTextBox = "";
            mainForm.PcInfoTextBox = "";
            mainForm.PInfoTextBox = "";

            mainForm.QaInfoTextBox = "";
            mainForm.QbInfoTextBox = "";
            mainForm.QcInfoTextBox = "";
            mainForm.QInfoTextBox = "";

            mainForm.SaInfoTextBox = "";
            mainForm.SbInfoTextBox = "";
            mainForm.ScInfoTextBox = "";
            mainForm.SInfoTextBox = "";

            mainForm.KpaInfoTextBox = "";
            mainForm.KpbInfoTextBox = "";
            mainForm.KpcInfoTextBox = "";
            mainForm.KpInfoTextBox = "";

            mainForm.FInfoTextBox = "";

            mainForm.U0InfoTextBox = "";
            mainForm.I0InfoTextBox = "";
        }

        private void DecodeInfo(byte[] buffer)
        {
            try
            {
                byte scheme = buffer[5];

                float U1 = base.ConvertByteToFloat(buffer, 6);
                float I1 = base.ConvertByteToFloat(buffer, 10);

                UpdatePrefix(U1, I1);

                ushort prefix = 0;
                prefix = mainForm.modbus.CalculateUnitShfit(U1, false);

                mainForm.NetAddressInfoTextBox = buffer[14].ToString();
                mainForm.ProgVersionInfoTextBox = buffer[15].ToString();

                if (scheme == 0)
                {
                    mainForm.UaInfoTextBox = mainForm.modbus.PrepareParameter((float)(base.ConvertByteToFloat(buffer, 16) * U1 * 1.25), prefix).ToString("N3");
                    mainForm.UbInfoTextBox = mainForm.modbus.PrepareParameter((float)(base.ConvertByteToFloat(buffer, 20) * U1 * 1.25), prefix).ToString("N3");
                    mainForm.UcInfoTextBox = mainForm.modbus.PrepareParameter((float)(base.ConvertByteToFloat(buffer, 24) * U1 * 1.25), prefix).ToString("N3"); 
                }
                else
                {
                    mainForm.UaInfoTextBox = "--------";
                    mainForm.UbInfoTextBox = "--------";
                    mainForm.UcInfoTextBox = "--------";
                } 

                mainForm.UabInfoTextBox = mainForm.modbus.PrepareParameter((float)(base.ConvertByteToFloat(buffer, 40) * U1 * 1.25 * 1.7321), prefix).ToString("N3");
                mainForm.UbcInfoTextBox = mainForm.modbus.PrepareParameter((float)(base.ConvertByteToFloat(buffer, 44) * U1 * 1.25 * 1.7321), prefix).ToString("N3");
                mainForm.UcaInfoTextBox = mainForm.modbus.PrepareParameter((float)(base.ConvertByteToFloat(buffer, 48) * U1 * 1.25 * 1.7321), prefix).ToString("N3");

                if (scheme == 0)
                {
                    mainForm.UfInfoTextBox = mainForm.modbus.PrepareParameter((float)(base.ConvertByteToFloat(buffer, 120) * U1 * 1.25), prefix).ToString("N3");
                    mainForm.U0InfoTextBox = mainForm.modbus.PrepareParameter((float)(base.ConvertByteToFloat(buffer, 132) * U1 * 1.25), prefix).ToString("N3");
                }
                else
                {
                    mainForm.UfInfoTextBox = "--------";
                    mainForm.U0InfoTextBox = "--------";
                }
                
                mainForm.UlInfoTextBox = mainForm.modbus.PrepareParameter((float)(base.ConvertByteToFloat(buffer, 128) * U1 * 1.25 * 1.7321), prefix).ToString("N3");
                

                prefix = mainForm.modbus.CalculateUnitShfit(I1, true);
                mainForm.IaInfoTextBox = mainForm.modbus.PrepareParameter((float)(base.ConvertByteToFloat(buffer, 28) * I1), prefix).ToString("N3");
                mainForm.IbInfoTextBox = mainForm.modbus.PrepareParameter((float)(base.ConvertByteToFloat(buffer, 32) * I1), prefix).ToString("N3");
                mainForm.IcInfoTextBox = mainForm.modbus.PrepareParameter((float)(base.ConvertByteToFloat(buffer, 36) * I1), prefix).ToString("N3");

                mainForm.IInfoTextBox = mainForm.modbus.PrepareParameter((float)(base.ConvertByteToFloat(buffer, 124) * I1), prefix).ToString("N3");
                mainForm.I0InfoTextBox = mainForm.modbus.PrepareParameter((float)(base.ConvertByteToFloat(buffer, 136) * I1), prefix).ToString("N3");

                prefix = mainForm.modbus.CalculateUnitShfit((U1 * I1) * (float)Math.Sqrt(3.0), false);

                if (scheme == 0)
                {
                    mainForm.PaInfoTextBox = mainForm.modbus.PrepareParameter((float)(base.ConvertByteToFloat(buffer, 52) * U1 * I1), prefix).ToString("N3");
                    mainForm.PbInfoTextBox = mainForm.modbus.PrepareParameter((float)(base.ConvertByteToFloat(buffer, 56) * U1 * I1), prefix).ToString("N3");
                    mainForm.PcInfoTextBox = mainForm.modbus.PrepareParameter((float)(base.ConvertByteToFloat(buffer, 60) * U1 * I1), prefix).ToString("N3");

                    mainForm.QaInfoTextBox = mainForm.modbus.PrepareParameter((float)(base.ConvertByteToFloat(buffer, 64) * U1 * I1), prefix).ToString("N3");
                    mainForm.QbInfoTextBox = mainForm.modbus.PrepareParameter((float)(base.ConvertByteToFloat(buffer, 68) * U1 * I1), prefix).ToString("N3");
                    mainForm.QcInfoTextBox = mainForm.modbus.PrepareParameter((float)(base.ConvertByteToFloat(buffer, 72) * U1 * I1), prefix).ToString("N3");

                    mainForm.SaInfoTextBox = mainForm.modbus.PrepareParameter((float)(base.ConvertByteToFloat(buffer, 76) * U1 * I1), prefix).ToString("N3");
                    mainForm.SbInfoTextBox = mainForm.modbus.PrepareParameter((float)(base.ConvertByteToFloat(buffer, 80) * U1 * I1), prefix).ToString("N3");
                    mainForm.ScInfoTextBox = mainForm.modbus.PrepareParameter((float)(base.ConvertByteToFloat(buffer, 84) * U1 * I1), prefix).ToString("N3");

                    mainForm.KpaInfoTextBox = base.ConvertByteToFloat(buffer, 88).ToString("N3");
                    mainForm.KpbInfoTextBox = base.ConvertByteToFloat(buffer, 92).ToString("N3");
                    mainForm.KpcInfoTextBox = base.ConvertByteToFloat(buffer, 96).ToString("N3");
                }
                else
                {
                    mainForm.PaInfoTextBox = "--------";
                    mainForm.PbInfoTextBox = "--------";
                    mainForm.PcInfoTextBox = "--------";

                    mainForm.QaInfoTextBox = "--------";
                    mainForm.QbInfoTextBox = "--------";
                    mainForm.QcInfoTextBox = "--------";

                    mainForm.SaInfoTextBox = "--------";
                    mainForm.SbInfoTextBox = "--------";
                    mainForm.ScInfoTextBox = "--------";

                    mainForm.KpaInfoTextBox = "--------";
                    mainForm.KpbInfoTextBox = "--------";
                    mainForm.KpcInfoTextBox = "--------";
                }

                mainForm.PInfoTextBox = mainForm.modbus.PrepareParameter((float)(base.ConvertByteToFloat(buffer, 100) * U1 * I1 * 3), prefix).ToString("N3");
                mainForm.QInfoTextBox = mainForm.modbus.PrepareParameter((float)(base.ConvertByteToFloat(buffer, 104) * U1 * I1 * 3), prefix).ToString("N3");
                mainForm.SInfoTextBox = mainForm.modbus.PrepareParameter((float)(base.ConvertByteToFloat(buffer, 108) * U1 * I1 * 3), prefix).ToString("N3");             

                mainForm.KpInfoTextBox = base.ConvertByteToFloat(buffer, 112).ToString("N3");
                mainForm.FInfoTextBox = (base.ConvertByteToFloat(buffer, 116) * 10 + 45.0).ToString("N3");

                if (buffer.Length > 142)
                {
                    mainForm.K0UInfoTextBox = base.ConvertByteToFloat(buffer, 140).ToString("N3");
                    mainForm.K2UInfoTextBox = base.ConvertByteToFloat(buffer, 144).ToString("N3");

                    mainForm.dFInfoTextBox = base.ConvertByteToFloat(buffer, 148).ToString("N3");

                    mainForm.dUplusInfoTextBox = base.ConvertByteToFloat(buffer, 152).ToString("N3");
                    mainForm.dUminusInfoTextBox = base.ConvertByteToFloat(buffer, 156).ToString("N3");
                }
            }
            catch
            {
                mainForm.StatusLabel = "Ошибка декодирования информации";
            }
        }

        private void UpdatePrefix(float U1, float I1)
        {
            string prefix = "";
            ushort unitShift = 0;

            unitShift = mainForm.modbus.CalculateUnitShfit(U1, false);  // / sqrt(3.0) // param == Ua || param == Ub || param == Uc || param == Umd || param == U0
            if (unitShift == 0) prefix = "В";
            else if (unitShift == 1) prefix = "кВ";
            else if (unitShift == 2) prefix = "МВ";
            else if (unitShift == 3) prefix = "ГВ";

            mainForm.UaInfoLabel = "Ua, " + prefix;
            mainForm.UbInfoLabel = "Ub, " + prefix;
            mainForm.UcInfoLabel = "Uc, " + prefix;
            mainForm.UfInfoLabel = "Uфср, " + prefix;

            mainForm.UabInfoLabel = "Uab, " + prefix;
            mainForm.UbcInfoLabel = "Ubc, " + prefix;
            mainForm.UcaInfoLabel = "Uca, " + prefix;
            mainForm.UlInfoLabel = "Uлср, " + prefix;

            mainForm.U0InfoLabel = "U0, " + prefix;


            unitShift = mainForm.modbus.CalculateUnitShfit(I1, true); // param == Ia || param == Ib || param == Ic || param == Imd)
            if (unitShift == 0) prefix = "A";
            else if (unitShift == 1) prefix = "кA";
            else if (unitShift == 2) prefix = "МA";
            else if (unitShift == 3) prefix = "ГA";

            mainForm.IaInfoLabel = "Ia, " + prefix;
            mainForm.IbInfoLabel = "Ib, " + prefix;
            mainForm.IcInfoLabel = "Ic, " + prefix;
            mainForm.IInfoLabel = "Iср, " + prefix;

            mainForm.I0InfoLabel = "I0, " + prefix;


            unitShift = mainForm.modbus.CalculateUnitShfit((U1 * I1) * (float)Math.Sqrt(3.0), false); // / sqrt(3.0)
            if (unitShift == 0) prefix = "Вт";
            else if (unitShift == 1) prefix = "кВт";
            else if (unitShift == 2) prefix = "МВт";
            else if (unitShift == 3) prefix = "ГВт";

            mainForm.PaInfoLabel = "Pa, " + prefix;
            mainForm.PbInfoLabel = "Pb, " + prefix;
            mainForm.PcInfoLabel = "Pc, " + prefix;
            mainForm.PInfoLabel = "P, " + prefix;

            if (unitShift == 0) prefix = "вар";
            else if (unitShift == 1) prefix = "квар";
            else if (unitShift == 2) prefix = "Мвар";
            else if (unitShift == 3) prefix = "Гвар";

            mainForm.QaInfoLabel = "Qa, " + prefix;
            mainForm.QbInfoLabel = "Qb, " + prefix;
            mainForm.QcInfoLabel = "Qc, " + prefix;
            mainForm.QInfoLabel = "Q, " + prefix;

            if (unitShift == 0) prefix = "ВА";
            else if (unitShift == 1) prefix = "кВА";
            else if (unitShift == 2) prefix = "МВА";
            else if (unitShift == 3) prefix = "ГВА";

            mainForm.SaInfoLabel = "Sa, " + prefix;
            mainForm.SbInfoLabel = "Sb, " + prefix;
            mainForm.ScInfoLabel = "Sc, " + prefix;
            mainForm.SInfoLabel = "S, " + prefix;
        }
    }
}

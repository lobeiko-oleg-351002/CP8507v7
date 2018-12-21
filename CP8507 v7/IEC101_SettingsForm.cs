using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
//using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CP8507_v7
{
    public partial class IEC101_SettingsForm : Form
    {
        public IECGroupsDataGrid groupsDGV;
        private Modbus modbus;

        public IEC101_SettingsForm(Modbus mb)
        {
            InitializeComponent();

            groupsDGV = new IECGroupsDataGrid();
           
            panel1.Controls.Add(groupsDGV.DGV);

            modbus = mb;
        }

        private void read_button_Click(object sender, EventArgs e)
        {
            modbus.ReadData(Modbus.EnquryType.INQ_READ_IEC_101_PARAMS);
        }

        private void save_button_Click(object sender, EventArgs e)
        {
            save_101();
        }

        public void save_101()
        {
            try
            {
                if (iec101ASDUAddress_comboBox.SelectedIndex == -1
                    || iec101InfoAddress_comboBox.SelectedIndex == -1
                    || iec101Reason_comboBox.SelectedIndex == -1
                    || iec101ASDUType_comboBox.SelectedIndex == -1)
                {
                    throw new IndexOutOfRangeException();
                }

                byte[] asduAddress = BitConverter.GetBytes((ushort)(iec101ASDUAddress_comboBox.SelectedIndex + 1));
                byte[] infoAddress = BitConverter.GetBytes((ushort)(iec101InfoAddress_comboBox.SelectedIndex + 2));
                byte[] iecReason = BitConverter.GetBytes((ushort)(iec101Reason_comboBox.SelectedIndex + 1));

                ushort ASDUType = 0;
                if (iec101ASDUType_comboBox.SelectedIndex == 0) ASDUType = 9;
                if (iec101ASDUType_comboBox.SelectedIndex == 1) ASDUType = 10;
                if (iec101ASDUType_comboBox.SelectedIndex == 2) ASDUType = 13;
                if (iec101ASDUType_comboBox.SelectedIndex == 3) ASDUType = 14;
                if (iec101ASDUType_comboBox.SelectedIndex == 4) ASDUType = 21;
                if (iec101ASDUType_comboBox.SelectedIndex == 5) ASDUType = 34;
                if (iec101ASDUType_comboBox.SelectedIndex == 6) ASDUType = 36;

                byte[] asduType = BitConverter.GetBytes(ASDUType);

                byte[] data = new byte[8];
                asduAddress.CopyTo(data, 6);
                infoAddress.CopyTo(data, 4);
                iecReason.CopyTo(data, 2);
                asduType.CopyTo(data, 0);

                modbus.WriteData(Modbus.EnquryType.INQ_SAVE_IEC_101_CONFIG, data);
            }
            catch
            {
                MessageBox.Show(Globals.INCORRECT_DATA_MESSAGE);
            }
        }

        public void save_values()
        {
            try
            {
                UInt32 values = 0;
                for (int i = 0; i < 31; i++)
                {
                    if ((bool)groupsDGV.DGV.Rows[i].Cells[1].Value)
                    {
                        values |= ((UInt32)1 << i); // set
                    }
                }
                byte[] data2 = BitConverter.GetBytes((UInt32)(values));

                byte[] data = new byte[4];
                data2.CopyTo(data, 0);

                modbus.WriteData(Modbus.EnquryType.INQ_SAVE_IEC_101_VALUES, data2);
            }
            catch
            {
                MessageBox.Show(Globals.INCORRECT_DATA_MESSAGE);
            }
        }

        private void label6_Click(object sender, EventArgs e)
        {
            foreach (Form f in Application.OpenForms)
            {
                if (f.Name == "HelpForm")
                {
                    f.Focus();
                    return;
                }
            }
            HelpForm form = new HelpForm();
            form.Show();
        }
    }
}

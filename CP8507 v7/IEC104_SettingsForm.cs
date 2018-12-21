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
    public partial class IEC104_SettingsForm : Form
    {
        public IECGroupsDataGrid groupsDGV;
        private Modbus modbus;


        public IEC104_SettingsForm(Modbus mb)
        {
            InitializeComponent();

            groupsDGV = new IECGroupsDataGrid();

            panel1.Controls.Add(groupsDGV.DGV);

            modbus = mb;
        }

        private void read_button_Click(object sender, EventArgs e)
        {
            modbus.ReadData(Modbus.EnquryType.INQ_READ_IEC_104_PARAMS);
        }

        private void save_button_Click(object sender, EventArgs e)
        {
            save_104();
        }

        public void save_104()
        {
            try
            {
                if (iec104ASDUType_comboBox.SelectedIndex == -1)
                {
                    throw new IndexOutOfRangeException();
                }

                ushort k, w, t1, t2, t3;
                k = Convert.ToUInt16(k_textBox.Text);
                w = Convert.ToUInt16(w_textBox.Text);
                t1 = Convert.ToUInt16(t1_textBox.Text);
                t2 = Convert.ToUInt16(t2_textBox.Text);
                t3 = Convert.ToUInt16(t3_textBox.Text);

                ushort ASDUType = 0;
                if (iec104ASDUType_comboBox.SelectedIndex == 0) ASDUType = 9;
                if (iec104ASDUType_comboBox.SelectedIndex == 1) ASDUType = 10;
                if (iec104ASDUType_comboBox.SelectedIndex == 2) ASDUType = 13;
                if (iec104ASDUType_comboBox.SelectedIndex == 3) ASDUType = 14;
                if (iec104ASDUType_comboBox.SelectedIndex == 4) ASDUType = 21;
                if (iec104ASDUType_comboBox.SelectedIndex == 5) ASDUType = 34;
                if (iec104ASDUType_comboBox.SelectedIndex == 6) ASDUType = 36;

                byte[] data = new byte[16];

                ushort sporadic_disable = 1;
                byte[] temp = BitConverter.GetBytes(sporadic_disable);
                temp.CopyTo(data, 0);

                temp = BitConverter.GetBytes(t3);
                temp.CopyTo(data, 2);

                temp = BitConverter.GetBytes(t2);
                temp.CopyTo(data, 4);

                temp = BitConverter.GetBytes(t1);
                temp.CopyTo(data, 6);

                ushort t0 = 30;
                temp = BitConverter.GetBytes(t0);
                temp.CopyTo(data, 8);

                temp = BitConverter.GetBytes(w);
                temp.CopyTo(data, 10);

                temp = BitConverter.GetBytes(k);
                temp.CopyTo(data, 12);

                temp = BitConverter.GetBytes(ASDUType);
                temp.CopyTo(data, 14);

                modbus.WriteData(Modbus.EnquryType.INQ_SAVE_IEC_104_CONFIG, data);
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

                modbus.WriteData(Modbus.EnquryType.INQ_SAVE_IEC_104_VALUES, data2);
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

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
    public partial class AddSettings : Form
    {
        private Modbus modbus;

        public AddSettings(Modbus mb)
        {
            InitializeComponent();
            modbus = mb;
        }

        private void saveFactoryNumber_button_Click(object sender, EventArgs e)
        {
            try
            {
                if (factoryNumber_textBox.Text.Length != 6) throw new FieldAccessException();

                ushort year = Convert.ToUInt16(factoryNumber_textBox.Text.Substring(0, 2));
                ushort factoryNumber = Convert.ToUInt16(factoryNumber_textBox.Text.Substring(3, 3));
                byte[] dataFactoryNum = BitConverter.GetBytes(factoryNumber);
                byte[] dataYear = BitConverter.GetBytes(year);
                byte[] data = new byte[4];
                dataFactoryNum.CopyTo(data, 2);
                dataYear.CopyTo(data, 0);

                modbus.WriteData(Modbus.EnquryType.INQ_SAVE_YEAR_AND_FACTORYNUM, data);
            }
            catch
            {
                MessageBox.Show(Globals.INCORRECT_DATA_MESSAGE);
            }
        }

        private void saveModification_button_Click(object sender, EventArgs e)
        {
            try
            {
                if (modification_comboBox.SelectedIndex == -1) throw new FieldAccessException();

                ushort modification = Convert.ToUInt16(modification_comboBox.Text);
                modification += 700; // 8507

                byte[] data = BitConverter.GetBytes(modification);

                modbus.WriteData(Modbus.EnquryType.INQ_SAVE_MODIFICATION, data);
            }
            catch
            {
                MessageBox.Show(Globals.INCORRECT_DATA_MESSAGE);
            }
        }

        private void saveRelaysNumber_button_Click(object sender, EventArgs e)
        {
            try
            {
                if (relays_comboBox.SelectedIndex == -1) throw new FieldAccessException();

                ushort relays = Convert.ToUInt16(relays_comboBox.Text);
                byte[] data = BitConverter.GetBytes(relays);

                modbus.WriteData(Modbus.EnquryType.INQ_SAVE_RELAYS_NUMBER, data);
            }
            catch
            {
                MessageBox.Show(Globals.INCORRECT_DATA_MESSAGE);
            }
        }

        private void saveInterfacesNumber_button_Click(object sender, EventArgs e)
        {
            try
            {
                if (interfaces_comboBox.SelectedIndex == -1) throw new FieldAccessException();

                ushort interfaces = Convert.ToUInt16(interfaces_comboBox.Text);
                byte[] data = BitConverter.GetBytes(interfaces);

                modbus.WriteData(Modbus.EnquryType.INQ_SAVE_INTERFACES_NUMBER, data);
            }
            catch
            {
                MessageBox.Show(Globals.INCORRECT_DATA_MESSAGE);
            }
        }

        private void readCoefs_button_Click(object sender, EventArgs e)
        {
            modbus.ReadData(Modbus.EnquryType.INQ_READ_PARAMS_FOR_ADDSETTINGS_1);
        }
    }
}

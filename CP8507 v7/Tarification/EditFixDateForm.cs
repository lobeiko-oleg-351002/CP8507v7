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
    public partial class EditFixDateForm : Form
    {
        public DateTime Date;
        public bool Delete;

        public EditFixDateForm(int day, int month)
        {
            InitializeComponent();
            dayUpDown.Value = day;
            monthUpDown.Value = month;
            Delete = false;
        }

        private void dayUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (dayUpDown.Value >= 32) dayUpDown.Value = 1;
            else if (dayUpDown.Value < 1) dayUpDown.Value = 31;
        }

        private void monthUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (monthUpDown.Value >= 13) monthUpDown.Value = 1;
            else if (monthUpDown.Value < 1) monthUpDown.Value = 12;
        }

        private void deleteFixDay_button_Click(object sender, EventArgs e)
        {
            Delete = true;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void addFixDate_button_Click(object sender, EventArgs e)
        {
            string error = "";

            DateTime dateTime = new DateTime();
            if (dayUpDown.Value < 1 || dayUpDown.Value > 31
                || monthUpDown.Value < 1 || monthUpDown.Value > 12) error += "Неправильно введена дата" + Environment.NewLine;
            else
            {
                string input = "2016-" + monthUpDown.Value.ToString() + "-" + dayUpDown.Value.ToString();
                if (!DateTime.TryParse(input, out dateTime)) error += "Введенной даты не существует" + Environment.NewLine;
            }

            if (error != "") MessageBox.Show(error);
            else
            {
                Delete = false;
                Date = dateTime;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }
    }
}

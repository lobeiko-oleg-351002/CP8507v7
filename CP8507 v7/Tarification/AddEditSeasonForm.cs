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
    public partial class AddEditSeasonForm : Form
    {
        private DateTime startDT;
        private DateTime endDT;

        public DateTime StartDT
        {
            get
            {
                return startDT;
            }
        }
        public DateTime EndDT
        {
            get
            {
                return endDT;
            }
        }

        public AddEditSeasonForm()
        {
            InitializeComponent();

            this.Text = "Добавить сезон";
            addSeasonDate_button.Text = "Добавить";

            startDT = new DateTime();
            endDT = new DateTime();
        }

        public AddEditSeasonForm(DateTime start, DateTime end)
        {
            InitializeComponent();

            this.Text = "Изменить интервал сезона";
            addSeasonDate_button.Text = "Изменить";

            startDT = start;
            endDT = end;

            day_startUpDown.Value = StartDT.Day;
            month_startUpDown.Value = StartDT.Month;

            day_endUpDown.Value = endDT.Day;
            month_endUpDown.Value = endDT.Month;
        }

        private void dayUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (day_startUpDown.Value >= 32) day_startUpDown.Value = 1;
            else if (day_startUpDown.Value < 1) day_startUpDown.Value = 31;
        }

        private void monthUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (month_startUpDown.Value >= 13) month_startUpDown.Value = 1;
            else if (month_startUpDown.Value < 1) month_startUpDown.Value = 12;
        }

        private void day_endUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (day_endUpDown.Value >= 32) day_endUpDown.Value = 1;
            else if (day_endUpDown.Value < 1) day_endUpDown.Value = 31;
        }

        private void month_endUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (month_endUpDown.Value >= 13) month_endUpDown.Value = 1;
            else if (month_endUpDown.Value < 1) month_endUpDown.Value = 12;
        }

        private void addSeasonDate_button_Click(object sender, EventArgs e)
        {
            string error = "";

            startDT = new DateTime();
            if (day_startUpDown.Value < 1 || day_startUpDown.Value > 31
                || month_startUpDown.Value < 1 || month_startUpDown.Value > 12)
                error += "Неправильно введена дата (начало интервала)" + Environment.NewLine;
            else
            {
                string input = "2016-" + month_startUpDown.Value.ToString() + "-" + day_startUpDown.Value.ToString();
                if (!DateTime.TryParse(input, out startDT)) error += "Введенной даты не существует (начало интервала)" + Environment.NewLine;
            }

            endDT = new DateTime();
            if (day_endUpDown.Value < 1 || day_endUpDown.Value > 31
                || month_endUpDown.Value < 1 || month_endUpDown.Value > 12)
                error += "Неправильно введена дата (конец интервала)" + Environment.NewLine;
            else
            {
                string input = "2016-" + month_endUpDown.Value.ToString() + "-" + day_endUpDown.Value.ToString();
                if (!DateTime.TryParse(input, out endDT)) error += "Введенной даты не существует (конец интервала)" + Environment.NewLine;
            }

            if (startDT > endDT) error += "Начальная дата больше конечной" + Environment.NewLine;

            if (error != "") MessageBox.Show(error);
            else
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }
    }
}

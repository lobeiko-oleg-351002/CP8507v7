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
    public partial class AddFixDateForm : Form
    {
        public TimeSpan StartInterval;
        public TimeSpan EndInterval;
        public DateTime Date;
        public int Tarif;
        public int Season;

        public AddFixDateForm(TabControl seasons)
        {
            InitializeComponent();
            for (int i = 0; i < seasons.TabPages.Count; i++)
            {
                seasons_comboBox.Items.Add("Сезон " + (i + 1).ToString());
            }
            seasons_comboBox.SelectedIndex = seasons.SelectedIndex;
            tarif_comboBox.SelectedIndex = 0;
        }

        private void hour_numericUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (hour_startUpDown.Value >= 24) hour_startUpDown.Value = 0;
            else if (hour_startUpDown.Value < 0) hour_startUpDown.Value = 23;
        }

        private void minute_numericUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (minute_startUpDown.Value >= 60) minute_startUpDown.Value = 0;
            else if (minute_startUpDown.Value < 0) minute_startUpDown.Value = 30;
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            if (hour_endUpDown.Value == 24) minute_endUpDown.Value = 0;
            else if (hour_endUpDown.Value >= 25) hour_endUpDown.Value = 0;
            else if (hour_endUpDown.Value < 0)
            {
                hour_endUpDown.Value = 24;
                minute_endUpDown.Value = 0;
            }
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            if (minute_endUpDown.Value >= 60) minute_endUpDown.Value = 0;
            else if (minute_endUpDown.Value < 0) minute_endUpDown.Value = 30;
        }

        private void numericUpDown4_ValueChanged(object sender, EventArgs e)
        {
            if (dayUpDown.Value >= 32) dayUpDown.Value = 1;
            else if (dayUpDown.Value < 1) dayUpDown.Value = 31;
        }

        private void numericUpDown3_ValueChanged(object sender, EventArgs e)
        {
            if (monthUpDown.Value >= 13) monthUpDown.Value = 1;
            else if (monthUpDown.Value < 1) monthUpDown.Value = 12;
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
                // if (dateTime < startDT || dateTime > endDT) error += "Введенная дата находится за пределами данного сезона" + Environment.NewLine;
            }

            if (hour_startUpDown.Value < 0 || hour_startUpDown.Value > 23
                || (minute_startUpDown.Value != 0 && minute_startUpDown.Value != 30)) error += "Неправильно введен начальный интервал" + Environment.NewLine;

            if (hour_endUpDown.Value < 0 || hour_endUpDown.Value > 24
                || (minute_endUpDown.Value != 0 && minute_endUpDown.Value != 30)
                || (hour_endUpDown.Value == 24 && minute_endUpDown.Value != 0)) error += "Неправильно введен конечный интервал" + Environment.NewLine;

            StartInterval = new TimeSpan((int)hour_startUpDown.Value, (int)minute_startUpDown.Value, 0);
            EndInterval = new TimeSpan((int)hour_endUpDown.Value, (int)minute_endUpDown.Value, 0);

            if (EndInterval <= StartInterval) error += "Конечный интервал задан раньше начального" + Environment.NewLine;

            if (error != "") MessageBox.Show(error);
            else
            {
                Date = dateTime;
                Tarif = tarif_comboBox.SelectedIndex + 1;
                Season = seasons_comboBox.SelectedIndex + 1;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }
    }
}

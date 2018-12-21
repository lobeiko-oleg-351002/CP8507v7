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
    public partial class AddIntervalForm : Form
    {
        public TimeSpan StartInterval;
        public TimeSpan EndInterval;
        public int Tarif;
        public int Season;
        public bool[] Days;

        public AddIntervalForm(TabControl seasons)
        {
            InitializeComponent();
            for (int i = 0; i < seasons.TabPages.Count; i++)
            {
                seasons_comboBox.Items.Add("Сезон " + (i + 1).ToString());
            }
            seasons_comboBox.SelectedIndex = seasons.SelectedIndex;
            tarif_comboBox.SelectedIndex = 0;
        }

        private void hour_startUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (hour_startUpDown.Value >= 24) hour_startUpDown.Value = 0;
            else if (hour_startUpDown.Value < 0) hour_startUpDown.Value = 23;
        }

        private void minute_startUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (minute_startUpDown.Value >= 60) minute_startUpDown.Value = 0;
            else if (minute_startUpDown.Value < 0) minute_startUpDown.Value = 30;
        }

        private void hour_endUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (hour_endUpDown.Value == 24) minute_endUpDown.Value = 0;
            else if (hour_endUpDown.Value >= 25) hour_endUpDown.Value = 0;
            else if (hour_endUpDown.Value < 0)
            {
                hour_endUpDown.Value = 24;
                minute_endUpDown.Value = 0;
            }
        }

        private void minute_endUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (minute_endUpDown.Value >= 60) minute_endUpDown.Value = 0;
            else if (minute_endUpDown.Value < 0) minute_endUpDown.Value = 30;
        }

        private void addFixDate_button_Click(object sender, EventArgs e)
        {
            string error = "";

            if (hour_startUpDown.Value < 0 || hour_startUpDown.Value > 23
                || (minute_startUpDown.Value != 0 && minute_startUpDown.Value != 30)) error += "Неправильно введен начальный интервал" + Environment.NewLine;

            if (hour_endUpDown.Value < 0 || hour_endUpDown.Value > 24
                || (minute_endUpDown.Value != 0 && minute_endUpDown.Value != 30)
                || (hour_endUpDown.Value == 24 && minute_endUpDown.Value != 0)) error += "Неправильно введен конечный интервал" + Environment.NewLine;

            StartInterval = new TimeSpan((int)hour_startUpDown.Value, (int)minute_startUpDown.Value, 0);
            EndInterval = new TimeSpan((int)hour_endUpDown.Value, (int)minute_endUpDown.Value, 0);

            if (EndInterval <= StartInterval) error += "Конечный интервал задан раньше начального" + Environment.NewLine;

            if (days_checkedListBox.GetItemCheckState(0) != CheckState.Checked
                && days_checkedListBox.GetItemCheckState(1) != CheckState.Checked
                && days_checkedListBox.GetItemCheckState(2) != CheckState.Checked)
                error += "Не выбрано ни одного дня" + Environment.NewLine;

            if (error != "") MessageBox.Show(error);
            else
            {
                Days = new bool[days_checkedListBox.Items.Count];
                for (int i = 0; i < days_checkedListBox.Items.Count; i++)
                {
                    if (days_checkedListBox.GetItemCheckState(i) == CheckState.Checked) Days[i] = true;
                    else Days[i] = false;
                }
                Tarif = tarif_comboBox.SelectedIndex + 1;
                Season = seasons_comboBox.SelectedIndex + 1;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }
    }
}

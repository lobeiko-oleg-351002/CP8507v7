using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CP8507_v7
{
    public partial class ReadEnergyForm : Form
    {
        private const int ThreeMinutesCutOption = 0;
        private const int ThirtyMinutesCutOption = 1;
        private const int DayCutOption = 2;
        private const int MonthCutOption = 3;
        private const int YearCutOption = 4; 

        private int selectedEnergyJournal;
        private bool intervalChecked;

        private ushort funcCode;
        private ushort startIndex;
        private ushort numOfRecords;


        public ushort FunctionalCode
        {
            get
            {
                return funcCode;
            }
        }

        public ushort StartIndex
        {
            get
            {
                return startIndex;
            }
        }

        public ushort NumberOfRecords
        {
            get
            {
                return numOfRecords;
            }
        }



        public ReadEnergyForm(int selectedJournal, bool intervalCh)
        {
            InitializeComponent();

            selectedEnergyJournal = selectedJournal;
            intervalChecked = intervalCh;

            DateTime dtNow = DateTime.Now;
            this.hour_numericUpDown.Value = dtNow.Hour;

            if (selectedEnergyJournal == ThreeMinutesCutOption) // 3 мин срезы
            {
                this.minute_numericUpDown.Value = dtNow.Minute / 3 * 3;
                this.minute_numericUpDown.Increment = 3;
            }
            else if (selectedEnergyJournal == ThirtyMinutesCutOption) // 30 мин срезы
            {
                this.minute_numericUpDown.Value = dtNow.Minute / 30 * 30;
                this.minute_numericUpDown.Increment = 30;
            }
            else if (selectedEnergyJournal == DayCutOption) // день
            {
                this.minute_numericUpDown.Value = dtNow.Minute;
                groupBox57.Enabled = false;
            }
            else if (selectedEnergyJournal == MonthCutOption) // месяц
            {
                this.minute_numericUpDown.Value = dtNow.Minute;
                groupBox57.Enabled = false;
            }
            else if (selectedEnergyJournal == YearCutOption) // год
            {
                this.minute_numericUpDown.Value = dtNow.Minute;
                groupBox57.Enabled = false;
            }

        }


        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                if (selectedEnergyJournal == 0 || selectedEnergyJournal == 1)
                    groupBox57.Enabled = true;
                numericUpDown1.Enabled = true;
                monthCalendar1.Enabled = true;
            }
            else if (radioButton2.Checked)
            {
                groupBox57.Enabled = false;
                numericUpDown1.Enabled = false;
                monthCalendar1.Enabled = false;
            }
            else if (radioButton3.Checked)
            {
                groupBox57.Enabled = false;
                numericUpDown1.Enabled = false;
                monthCalendar1.Enabled = false;
            }
        }

        private void hour_numericUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (hour_numericUpDown.Value >= 24) hour_numericUpDown.Value = 0;
            else if (hour_numericUpDown.Value < 0) hour_numericUpDown.Value = 23;
        }

        private void minute_numericUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (minute_numericUpDown.Value >= 60)
            {
                minute_numericUpDown.Value = 0;
                hour_numericUpDown.Value++;
            }
            else if (minute_numericUpDown.Value < 0)
            {
                minute_numericUpDown.Value = 60 - minute_numericUpDown.Increment;
                hour_numericUpDown.Value--;
            }
        }

        private void readEnergy_button_Click(object sender, EventArgs e)
        {
            if (selectedEnergyJournal == ThreeMinutesCutOption)
            {
                if (intervalChecked) funcCode = (ushort)CRC_RB.FunctionalCodes.N_ITOG_3MIN;
                else funcCode = (ushort)CRC_RB.FunctionalCodes.INTERVAL_3MIN;
            }
            else if (selectedEnergyJournal == ThirtyMinutesCutOption)
            {
                if (intervalChecked) funcCode = (ushort)CRC_RB.FunctionalCodes.N_ITOG_30MIN;
                else funcCode = (ushort)CRC_RB.FunctionalCodes.INTERVAL_30MIN;
            }
            else if (selectedEnergyJournal == DayCutOption)
            {
                if (intervalChecked) funcCode = (ushort)CRC_RB.FunctionalCodes.INTERVAL_DAY;
                else funcCode = (ushort)CRC_RB.FunctionalCodes.N_ITOG_DAY;
            }
            else if (selectedEnergyJournal == MonthCutOption)
            {
                if (intervalChecked) funcCode = (ushort)CRC_RB.FunctionalCodes.INTERVAL_MONTH;
                else funcCode = (ushort)CRC_RB.FunctionalCodes.N_ITOG_MONTH;
            }
            else if (selectedEnergyJournal == YearCutOption)
            {
                if (intervalChecked) funcCode = (ushort)CRC_RB.FunctionalCodes.INTERVAL_YEAR;
                else funcCode = (ushort)CRC_RB.FunctionalCodes.N_ITOG_YEAR;
            }

            DateTime dtNow = DateTime.Now;
            string date = monthCalendar1.SelectionRange.Start.ToShortDateString();
            string time = hour_numericUpDown.Value.ToString() + ":" + minute_numericUpDown.Value.ToString() + ":" + "0";
            string dt = date + " " + time;
            DateTime calendarDT = DateTime.Parse(dt);

            if (radioButton1.Checked) // если хотим считать заданный интервал
            {
                if (selectedEnergyJournal == ThreeMinutesCutOption || selectedEnergyJournal == ThirtyMinutesCutOption)
                {
                    dtNow = new DateTime(dtNow.Year, dtNow.Month, dtNow.Day, dtNow.Hour, dtNow.Minute, 0);
                    calendarDT = new DateTime(calendarDT.Year, calendarDT.Month, calendarDT.Day, calendarDT.Hour, calendarDT.Minute, 0);
                }
                else if (selectedEnergyJournal == DayCutOption)
                {
                    dtNow = new DateTime(dtNow.Year, dtNow.Month, dtNow.Day, 0, 0, 0);
                    calendarDT = new DateTime(calendarDT.Year, calendarDT.Month, calendarDT.Day, 0, 0, 0);
                }
                else if (selectedEnergyJournal == MonthCutOption)
                {
                    dtNow = new DateTime(dtNow.Year, dtNow.Month, 1, 0, 0, 0);
                    calendarDT = new DateTime(calendarDT.Year, calendarDT.Month, 1, 0, 0, 0);
                }
                else if (selectedEnergyJournal == YearCutOption)
                {
                    dtNow = new DateTime(dtNow.Year, 1, 1, 0, 0, 0);
                    calendarDT = new DateTime(calendarDT.Year, 1, 1, 0, 0, 0);
                }

                TimeSpan diff1 = dtNow.Subtract(calendarDT);
                if (diff1.TotalMinutes < 0)
                {
                    MessageBox.Show("Попытка чтения будущего!");
                    return;
                }

                if (selectedEnergyJournal == ThreeMinutesCutOption)
                {
                    startIndex = (ushort)(diff1.TotalSeconds / (3 * 60));
                }
                else if (selectedEnergyJournal == ThirtyMinutesCutOption)
                {
                    startIndex = (ushort)(diff1.TotalSeconds / (30 * 60));
                }
                else if (selectedEnergyJournal == DayCutOption)
                {
                    startIndex = (ushort)(diff1.TotalSeconds / (60 * 60 * 24));
                }
                else if (selectedEnergyJournal == MonthCutOption)
                {
                    startIndex = (ushort)(Math.Abs((dtNow.Year - calendarDT.Year) * 12 - Math.Abs(dtNow.Month - calendarDT.Month)));
                }
                else if (selectedEnergyJournal == YearCutOption)
                {
                    startIndex = (ushort)(dtNow.Year - calendarDT.Year);
                }

                numOfRecords = (ushort)numericUpDown1.Value;
            }
            else if (radioButton2.Checked)
            {
                startIndex = 0;
                numOfRecords = 1;
            }
            else if (radioButton3.Checked)
            {
                startIndex = 0;
                numOfRecords = 10;
            } 

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void ReadEnergyForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                readEnergy_button_Click(sender, e);
            }
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}

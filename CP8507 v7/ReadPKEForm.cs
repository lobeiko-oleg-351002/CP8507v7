using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CP8507_v7
{
    public partial class ReadPKEForm : Form
    {
        private int fileNumber;

        private DateTime DateTime;
        private ushort numOfRecords;


        public DateTime DT
        {
            get
            {
                return DateTime;
            }
        }

        public ushort NumberOfRecords
        {
            get
            {
                return numOfRecords;
            }
        }



        public ReadPKEForm(int fileNumebr)
        {
            InitializeComponent();

            this.fileNumber = fileNumebr;

            DateTime = DateTime.Now;
            this.hour_numericUpDown.Value = DateTime.Hour;
            this.minute_numericUpDown.Value = DateTime.Minute;

            if (fileNumber == 2) // 
            {
                this.second_numericUpDown.Enabled = true;

                this.second_numericUpDown.Value = DateTime.Second / 10 * 10;
                this.second_numericUpDown.Increment = 10;   
            }
            else
            {
                this.second_numericUpDown.Enabled = false;

                this.minute_numericUpDown.Value = DateTime.Minute / 10 * 10;
                this.minute_numericUpDown.Increment = 10;
            }
        }


        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
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




        private void readEnergy_button_Click(object sender, EventArgs e)
        {
            DateTime dtNow = DateTime.Now;
            string date = monthCalendar1.SelectionRange.Start.ToShortDateString();
            if (fileNumber != 2) second_numericUpDown.Value = 0;
            string time = hour_numericUpDown.Value.ToString() + ":" + minute_numericUpDown.Value.ToString() + ":" + second_numericUpDown.Value.ToString();
            string dt = date + " " + time;
            DateTime calendarDT = DateTime.Parse(dt);

            if (radioButton1.Checked) // если хотим считать заданный интервал
            {
                calendarDT = new DateTime(calendarDT.Year, calendarDT.Month, calendarDT.Day, calendarDT.Hour, calendarDT.Minute, calendarDT.Second);
                DateTime = calendarDT;

                TimeSpan diff1 = dtNow.Subtract(calendarDT);
                if (diff1.TotalMinutes < 0)
                {
                    MessageBox.Show("Попытка чтения будущего!");
                    return;
                }

                numOfRecords = (ushort)numericUpDown1.Value;
            }
            else if (radioButton2.Checked)
            {
                DateTime = DateTime.Now;

                if (fileNumber == 2) // 
                {
                    numOfRecords = 8640;
                    DateTime = new DateTime(DateTime.Year, DateTime.Month, DateTime.Day, DateTime.Hour, DateTime.Minute, DateTime.Second / 10 * 10);
                }
                else
                {
                    numOfRecords = 144;
                    DateTime = new DateTime(DateTime.Year, DateTime.Month, DateTime.Day, DateTime.Hour, DateTime.Minute / 10 * 10, 0);
                }
            }
            else if (radioButton3.Checked)
            {
                DateTime = DateTime.Now;

                if (fileNumber == 2) // 
                {
                    numOfRecords = 60480;
                    DateTime = new DateTime(DateTime.Year, DateTime.Month, DateTime.Day, DateTime.Hour, DateTime.Minute, DateTime.Second / 10 * 10);
                }
                else
                {
                    numOfRecords = 1008;
                    DateTime = new DateTime(DateTime.Year, DateTime.Month, DateTime.Day, DateTime.Hour, DateTime.Minute / 10 * 10, 0);
                }
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

        private void second_numericUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (second_numericUpDown.Value >= 60)
            {
                second_numericUpDown.Value = 0;
                minute_numericUpDown.Value++;
            }
            else if (second_numericUpDown.Value < 0)
            {
                second_numericUpDown.Value = 60 - second_numericUpDown.Increment;
                minute_numericUpDown.Value--;
            }
        }
    }
}

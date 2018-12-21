using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Text;
using System.Windows.Forms;

namespace CP8507_v7
{
    public partial class EnterParamForm : Form
    {
        private int labelID;
        private MainForm mainForm;

        public EnterParamForm(MainForm form, Label label)
        {
            InitializeComponent();
            mainForm = form;
            labelID = label.TabIndex;
            textBox1.Text = label.Text;

            if (labelID == 1) label1.Text = "Uab =";
            else if (labelID == 2) label1.Text = "Ubc =";
            else if (labelID == 3) label1.Text = "Uca =";
            else if (labelID == 4) label1.Text = "Ia =";
            else if (labelID == 5) label1.Text = "Ib =";
            else if (labelID == 6) label1.Text = "Ic =";
            else if (labelID == 7) label1.Text = "Ua =";
            else if (labelID == 8) label1.Text = "Ub =";
            else if (labelID == 9) label1.Text = "Uc =";
            else if (labelID == 16) label1.Text = "F =";
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string text = textBox1.Text;
                text = text.Replace(" ", "");
                text = text.Replace(Environment.NewLine, "");
                float.Parse(text.Replace(",", CultureInfo.InvariantCulture.NumberFormat.NumberDecimalSeparator), CultureInfo.InvariantCulture);
                errorProvider.SetError(textBox1, "");
                textBox1.Text = text;
                button1.Enabled = true;
            }
            catch
            {
                errorProvider.SetError(textBox1, Globals.INCORRECT_DATA_MESSAGE);
                button1.Enabled = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            float temp = float.Parse(textBox1.Text.Replace(",", CultureInfo.InvariantCulture.NumberFormat.NumberDecimalSeparator), CultureInfo.InvariantCulture);
            mainForm.SendChangeDataParam(labelID, temp);
        }

        private void EnterParamForm_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button1_Click(sender, e);
            }
        }
    }
}

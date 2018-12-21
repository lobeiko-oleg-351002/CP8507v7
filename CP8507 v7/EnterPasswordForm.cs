using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CP8507_v7
{
    public partial class EnterPasswordForm : Form
    {
        private int password = 0;
        private bool isPasswordSetted = false;

        public int GetPassword
        {
            get
            {
                return password;
            }
        }

        public bool PasswordSetted
        {
            get
            {
                return isPasswordSetted;
            }
        }

        public EnterPasswordForm()
        {
            InitializeComponent();
            this.ActiveControl = maskedTextBox1;
            maskedTextBox1.Focus();
            maskedTextBox1.Select();
            maskedTextBox1.SelectAll();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string text = maskedTextBox1.Text;
            if (text.Length == 11)
            {
                byte temp = (byte)Char.GetNumericValue(text[0]);
                int pass = 0x00000000;
                pass |= temp << 20;
                temp = (byte)Char.GetNumericValue(text[2]);
                pass |= temp << 16;
                temp = (byte)Char.GetNumericValue(text[4]);
                pass |= temp << 12;
                temp = (byte)Char.GetNumericValue(text[6]);
                pass |= temp << 8;
                temp = (byte)Char.GetNumericValue(text[8]);
                pass |= temp << 4;
                temp = (byte)Char.GetNumericValue(text[10]);
                pass |= temp;
                password = pass;
                isPasswordSetted = true;
                this.Close();
            }
        }

        private void maskedTextBox1_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {
        }
    }
}

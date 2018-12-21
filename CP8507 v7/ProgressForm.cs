using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
//using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace CP8507_v7
{
    public partial class ProgressForm : Form
    {
        TarifPro tarif;

        public ProgressForm(TarifPro protocol)
        {
            InitializeComponent();
            tarif = protocol;
        }

        private delegate void SetControlPropertyThreadSafeDelegate(Control control, string propertyName, object propertyValue);

        private static void SetControlPropertyThreadSafe(Control control, string propertyName, object propertyValue)
        {
            if (control.InvokeRequired)
            {
                control.BeginInvoke(new SetControlPropertyThreadSafeDelegate(SetControlPropertyThreadSafe),
                new object[] { control, propertyName, propertyValue });
            }
            else
            {
                control.GetType().InvokeMember(propertyName, BindingFlags.SetProperty, null, control, new object[] { propertyValue });
            }
        }


        public string LabelText
        {
            set
            {
                SetControlPropertyThreadSafe(label, "Text", value);
            }
        }

        public int ProgressBarValue
        {
            set
            {
                SetControlPropertyThreadSafe(progressBar, "Value", value);
            }
        }

        public int ProgressBarMax
        {
            set
            {
                SetControlPropertyThreadSafe(progressBar, "Maximum", value);
            }
        }

        private delegate void CloseFormThreadSafeDelegate();
        public void CloseForm()
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new CloseFormThreadSafeDelegate(CloseForm), new object[] { });
            }
            else
            {
                this.Close();
            }
        }

        private void ProgressForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                tarif.storedNumOfSeason = 100;
            }
        }
    }
}

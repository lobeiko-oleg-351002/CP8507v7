using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
//using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CP8507_v7
{
    public partial class EditDeleteIntervalForm : Form
    {
        public string[] intervals;
        public bool Changed;

        public EditDeleteIntervalForm(string[] subString)
        {
            InitializeComponent();
            Changed = false;
            listBox1.DrawItem += new DrawItemEventHandler(listBox_DrawItem);
            intervals = subString;
            for (int i = 0; i < intervals.Length; i++)
            {
                listBox1.Items.Add(intervals[i]);
            }
            listBox1.Items.Add("Добавить новую запись");
            listBox1.SetSelected(0, true);
        }

        void listBox_DrawItem(object sender, DrawItemEventArgs e)
        {
            ListBox list = (ListBox)sender;
            if (e.Index > -1)
            {
                object item = list.Items[e.Index];
                e.DrawBackground();
                e.DrawFocusRectangle();
                Brush brush = new SolidBrush(e.ForeColor);
                SizeF size = e.Graphics.MeasureString(item.ToString(), e.Font);
                e.Graphics.DrawString(item.ToString(), e.Font, brush, e.Bounds.Left + (e.Bounds.Width / 2 - size.Width / 2), e.Bounds.Top + (e.Bounds.Height / 2 - size.Height / 2));
            }
        }


        private void delete_button_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex >= 0 && listBox1.SelectedIndex != listBox1.Items.Count - 1)
            {
                Changed = true;
                int index = listBox1.SelectedIndex;
                listBox1.Items.Remove(listBox1.SelectedItem);
                if (index > 0) listBox1.SetSelected(index - 1, true);
                else if (listBox1.Items.Count > 0) listBox1.SetSelected(0, true);
                intervals = new string[listBox1.Items.Count - 1];
                for (int i = 0; i < intervals.Length; i++)
                {
                    intervals[i] = listBox1.Items[i].ToString();
                }
            }
        }

        private void edit_button_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex >= 0 && listBox1.SelectedIndex != listBox1.Items.Count - 1)
            {
                Changed = true;
                string[] separator = new string[] { " - " };
                String[] substrings = listBox1.SelectedItem.ToString().Split(separator, StringSplitOptions.None);
                TimeSpan start = new TimeSpan();
                TimeSpan end = new TimeSpan();
                try
                {
                    start = TimeSpan.Parse(substrings[0] == "24:00" ? "1.00:00" : substrings[0]);
                    end = TimeSpan.Parse(substrings[1] == "24:00" ? "1.00:00" : substrings[1]);
                }
                catch { }       
                EditIntervalForm form = new EditIntervalForm(start, end);
                form.StartPosition = FormStartPosition.Manual;
                form.Location = new Point(this.Left + this.Width / 3, this.Top + this.Height / 3);
                if (form.ShowDialog(this) == DialogResult.OK)
                {
                    listBox1.Items[listBox1.SelectedIndex] = form.StartInterval.Hours.ToString("D2") + ":" + form.StartInterval.Minutes.ToString("D2") 
                        + " - "
                        + ((int)form.EndInterval.TotalHours).ToString("D2") + ":" + form.EndInterval.Minutes.ToString("D2");
                    SortList();
                    intervals = new string[listBox1.Items.Count - 1];
                    for (int i = 0; i < intervals.Length; i++)
                    {
                        intervals[i] = listBox1.Items[i].ToString();
                    }
                }
                form.Dispose();
            }
        }

        private void SortList()
        {
            ArrayList myAL = new ArrayList();
            for (int k = 0; k < listBox1.Items.Count; k++) myAL.Add(listBox1.Items[k]);
            myAL.Sort();
            listBox1.Items.Clear();
            for (int k = 0; k < myAL.Count; k++)
            {
                listBox1.Items.Add(myAL[k].ToString());
            }
        }

        private void listBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int index = this.listBox1.IndexFromPoint(e.Location);
            if (index != System.Windows.Forms.ListBox.NoMatches)
            {
                if (index == listBox1.Items.Count - 1)
                {
                    EditIntervalForm form = new EditIntervalForm();
                    form.StartPosition = FormStartPosition.Manual;
                    form.Location = new Point(this.Left + this.Width / 3, this.Top + this.Height / 3);
                    if (form.ShowDialog(this) == DialogResult.OK)
                    {
                        Changed = true;
                        string date = form.StartInterval.Hours.ToString("D2") + ":" + form.StartInterval.Minutes.ToString("D2")
                            + " - "
                            + ((int)form.EndInterval.TotalHours).ToString("D2") + ":" + form.EndInterval.Minutes.ToString("D2");
                        listBox1.Items.Add(date);
                        SortList();
                        intervals = new string[listBox1.Items.Count - 1];
                        for (int i = 0; i < intervals.Length; i++)
                        {
                            intervals[i] = listBox1.Items[i].ToString();
                        }
                        listBox1.SetSelected(0, true);
                    }
                }
                else
                {
                    edit_button_Click(null, null);
                }              
            }
        }
    }
}

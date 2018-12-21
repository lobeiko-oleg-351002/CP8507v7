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
    public partial class RandomPKEDataGrid : Form
    {
        private DateTime startDT;
        private DateTime endDT;
        private bool seasonActivated;
        private int seasonNumber;

        public DateTime StartDT
        {
            get
            {
                return startDT;
            }
            set
            {
                startDT = value;
            }
        }
        public DateTime EndDT
        {
            get
            {
                return endDT;
            }
            set
            {
                endDT = value;
            }
        }
        public bool SeasonActivated
        {
            get
            {
                return seasonActivated;
            }
            set
            {
                seasonActivated = value;
            }
        }
        public int SeasonNumber
        {
            get
            {
                return seasonNumber;
            }
            set
            {
                seasonNumber = value;
            }
        }

        public RandomPKEDataGrid()
        {
            InitializeComponent();
            seasonActivated = false;
            startDT = new DateTime();
            endDT = new DateTime();
        }

        public void ActivateSeason(int season, DateTime start, DateTime end)
        {
            seasonNumber = season;
            seasonActivated = true;
            StartDT = start;
            EndDT = end;

            DGV.Rows.Clear();
            DGV.Rows.Add();
            DGV.Rows.Add();
            DGV.Rows.Add();
            DGV.Rows[0].Cells[0].Value = "Рабочие дни";
            DGV.Rows[1].Cells[0].Value = "Суббота";
            DGV.Rows[2].Cells[0].Value = "Воскресенье";
            for (int i = 1; i < DGV.ColumnCount; i++)
            {
                for (int j = 0; j < DGV.Rows.Count; j++)
                {
                    DGV.Rows[j].Cells[i].Value = "-";
                }
            }
        }

        private void DGV_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (DGV.CurrentCell.ColumnIndex > 0 && DGV.CurrentCell.Value != "-")
            {
                string[] separator = new string[] { Environment.NewLine };
                String[] substrings = DGV.CurrentCell.Value.ToString().Split(separator, StringSplitOptions.None);
                EditDeleteIntervalForm form = new EditDeleteIntervalForm(substrings);
                form.StartPosition = FormStartPosition.Manual;
                form.Location = new Point(this.Left + this.Width / 3, this.Top + this.Height / 3);
                form.ShowDialog(this);
                if (form.Changed)
                {
                    if (MessageBox.Show("Применить изменения?", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        if (form.intervals.Length == 0)
                        {
                            DGV.CurrentCell.Value = "-";
                            if (DGV.CurrentCell.RowIndex > 2) // если фикс дата
                            {
                                bool clear = true;
                                for (int i = 1; i < DGV.ColumnCount; i++)
                                {
                                    if (DGV.Rows[DGV.CurrentCell.RowIndex].Cells[i].Value.ToString() != "-") clear = false;
                                }
                                if (clear) DGV.Rows.RemoveAt(DGV.CurrentCell.RowIndex); // если не осталось интервалов удаляем строку
                            }
                        }
                        else
                        {
                            DGV.CurrentCell.Value = "";
                            string newLine = "";
                            for (int i = 0; i < form.intervals.Length; i++)
                            {
                                DGV.CurrentCell.Value += newLine + form.intervals[i];
                                newLine = Environment.NewLine;
                            }
                        }
                    }
                }
                form.Dispose();
            }
            else if (DGV.CurrentCell.ColumnIndex > 0 && DGV.CurrentCell.Value == "-")
            {
                EditIntervalForm form = new EditIntervalForm();
                form.StartPosition = FormStartPosition.Manual;
                form.Location = new Point(this.Left + this.Width / 3, this.Top + this.Height / 3);
                if (form.ShowDialog(this) == DialogResult.OK)
                {
                    DGV.CurrentCell.Value = form.StartInterval.Hours.ToString("D2") + ":" + form.StartInterval.Minutes.ToString("D2")
                        + " - "
                        + ((int)form.EndInterval.TotalHours).ToString("D2") + ":" + form.EndInterval.Minutes.ToString("D2");
                }
            }
            if (DGV.CurrentCell.ColumnIndex == 0 && DGV.CurrentCell.RowIndex > 2)
            {
                String[] substrings = DGV.CurrentCell.Value.ToString().Split('.');
                DateTime dt = DateTime.Parse(DGV.CurrentCell.Value + ".2016");
                EditFixDateForm form = new EditFixDateForm(dt.Day, dt.Month);
                form.StartPosition = FormStartPosition.Manual;
                form.Location = new Point(this.Left + this.Width / 3, this.Top + this.Height / 3);
                if (form.ShowDialog(this) == DialogResult.OK)
                {
                    if (form.Delete)
                        DGV.Rows.RemoveAt(DGV.CurrentCell.RowIndex);
                    else
                    {
                        string date = form.Date.ToString("dd.MM");
                        int currentRow = DGV.CurrentCell.RowIndex;
                        bool finded = false;
                        for (int row = 3; row < DGV.Rows.Count; row++)
                        {
                            if (row == currentRow) continue;
                            if (DGV.Rows[row].Cells[0].Value.ToString() == date)
                            {
                                finded = true;
                                if (MessageBox.Show("Заменить существующую дату " + DGV.Rows[row].Cells[0].Value.ToString() + "?", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
                                {
                                    DGV.CurrentCell.Value = date;
                                    DGV.Rows.RemoveAt(row);
                                }
                                break;
                            }
                        }
                        if (!finded) DGV.CurrentCell.Value = date;
                    }
                }
                form.Dispose();
            }
        }

        private void DGV_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right || e.Button == MouseButtons.Left)
            {
                //var hti = DGV.HitTest(e.X, e.Y);
                //if (hti.RowIndex >= 0)
                //{
                //    DGV.ClearSelection();
                //    DGV.CurrentCell = DGV[hti.ColumnIndex, hti.RowIndex];
                //}
            }
        }
    }
}

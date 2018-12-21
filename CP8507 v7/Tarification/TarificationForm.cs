using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
//using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Windows.Forms;

namespace CP8507_v7
{
    public partial class TarificationForm : Form
    {
        MainForm mainForm;
        RandomPKEDataGrid[] seasons_DGV = new RandomPKEDataGrid[12];
       // public SeasonRXTXStruct[] seasons_RXTX;
        

        const int MAX_INTERVALS = 48;
        const int MAX_FIX_DAYS = 25;

        public TarificationForm(MainForm form)
        {
            InitializeComponent();

            mainForm = form;
            for (int i = 0; i < seasons_DGV.Length; i++)
                seasons_DGV[i] = new RandomPKEDataGrid();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void добавитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (seasons_tabControl.TabCount < seasons_DGV.Length)
            {
                AddEditSeasonForm seasonForm = new AddEditSeasonForm();
                seasonForm.StartPosition = FormStartPosition.Manual;
                seasonForm.Location = new Point(this.Left + this.Width / 3, this.Top + this.Height / 3);
                if (seasonForm.ShowDialog(this) == DialogResult.OK)
                {
                    AddSeason(seasonForm.StartDT, seasonForm.EndDT);
                }
                seasonForm.Dispose();
            }
            else
            {
                mainForm.ShowMessageBox("Максимальное количество сезонов - 12");
            }
        }

        private void AddSeason(DateTime StartDT, DateTime EndDT)
        {
            int i = -1;
            for (i = 0; i < seasons_DGV.Length; i++)
            {
                if (!seasons_DGV[i].SeasonActivated) break;
            }
            if (i == -1)
            {
                MessageBox.Show("Произошла внутрення ошибка");
                return;
            }

            int seasonNumber = seasons_tabControl.TabCount + 1;
            string temp = "Сезон " + seasonNumber.ToString() + StartDT.ToString(" (dd.MM - ") + EndDT.ToString("dd.MM)");
            seasons_tabControl.TabPages.Add(temp);

            ToolStripMenuItem deleteItem = new ToolStripMenuItem();
            deleteItem.Text = temp;
            deleteItem.Name = seasonNumber.ToString();
            deleteItem.Click += new EventHandler(DeleteItemClickHandler);
            удалитьToolStripMenuItem.DropDownItems.Add(deleteItem);

            ToolStripMenuItem editItem = new ToolStripMenuItem();
            editItem.Text = temp;
            editItem.Name = seasonNumber.ToString();
            editItem.Click += new EventHandler(EditItemClickHandler);
            изменитьИнтервалToolStripMenuItem.DropDownItems.Add(editItem);

            seasons_DGV[i].ActivateSeason(seasonNumber, StartDT, EndDT);
            seasons_DGV[i].TopLevel = false;
            seasons_DGV[i].Visible = true;
            seasons_DGV[i].FormBorderStyle = FormBorderStyle.None;
            seasons_DGV[i].Dock = DockStyle.Fill;

            seasons_tabControl.TabPages[seasons_tabControl.TabCount - 1].Controls.Add(seasons_DGV[i]);
        }

        private void DeleteItemClickHandler(object sender, EventArgs e)
        {
            ToolStripMenuItem clickedItem = (ToolStripMenuItem)sender;
            int seasonNumber = Convert.ToInt32(clickedItem.Name);
            if (MessageBox.Show("Удаление сезона приведет к потере данных, связанных с ним."
                + Environment.NewLine + "Уверены?", "Удаление сезона "
                + seasonNumber.ToString(), MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                int i = -1;
                for (i = 0; i < seasons_DGV.Length; i++)
                {
                    if (seasons_DGV[i].SeasonNumber == seasonNumber) break;
                }

                seasons_DGV[i].SeasonActivated = false;
                seasons_tabControl.TabPages.Remove(seasons_tabControl.TabPages[seasonNumber - 1]);
                удалитьToolStripMenuItem.DropDownItems.Remove(удалитьToolStripMenuItem.DropDownItems[seasonNumber - 1]);
                изменитьИнтервалToolStripMenuItem.DropDownItems.Remove(изменитьИнтервалToolStripMenuItem.DropDownItems[seasonNumber - 1]);

                for (int j = seasonNumber - 1; j < seasons_tabControl.TabPages.Count; j++)
                {
                    for (i = 0; i < seasons_DGV.Length; i++)
                    {
                        if (seasons_DGV[i].SeasonActivated && seasons_DGV[i].SeasonNumber == j + 2)
                        {
                            seasons_DGV[i].SeasonNumber = j + 1;
                            удалитьToolStripMenuItem.DropDownItems[j].Name = (j + 1).ToString();
                            изменитьИнтервалToolStripMenuItem.DropDownItems[j].Name = (j + 1).ToString();
                            string temp = "Сезон " + seasons_DGV[i].SeasonNumber.ToString()
                                + seasons_DGV[i].StartDT.ToString(" (dd.MM - ") + seasons_DGV[i].EndDT.ToString("dd.MM)");
                            seasons_tabControl.TabPages[j].Text = temp;
                            удалитьToolStripMenuItem.DropDownItems[j].Text = temp;
                            изменитьИнтервалToolStripMenuItem.DropDownItems[j].Text = temp;
                        }
                    }
                }
            }
        }

        private void EditItemClickHandler(object sender, EventArgs e)
        {
            ToolStripMenuItem clickedItem = (ToolStripMenuItem)sender;
            int seasonNumber = Convert.ToInt32(clickedItem.Name);
            int i = -1;
            for (i = 0; i < seasons_DGV.Length; i++)
            {
                if (seasons_DGV[i].SeasonNumber == seasonNumber) break;
            }

            AddEditSeasonForm seasonForm = new AddEditSeasonForm(seasons_DGV[i].StartDT, seasons_DGV[i].EndDT);
            seasonForm.StartPosition = FormStartPosition.Manual;
            seasonForm.Location = new Point(this.Left + this.Width / 3, this.Top + this.Height / 3);
            if (seasonForm.ShowDialog(this) == DialogResult.OK)
            {
                seasons_DGV[i].StartDT = seasonForm.StartDT;
                seasons_DGV[i].EndDT = seasonForm.EndDT;

                string temp = "Сезон " + seasonNumber.ToString() + seasonForm.StartDT.ToString(" (dd.MM - ") + seasonForm.EndDT.ToString("dd.MM)"); ;
                seasons_tabControl.TabPages[seasonNumber - 1].Text = temp;
                удалитьToolStripMenuItem.DropDownItems[seasonNumber - 1].Text = temp;
                изменитьИнтервалToolStripMenuItem.DropDownItems[seasonNumber - 1].Text = temp;
            }
            seasonForm.Dispose();
        }


        private void фToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (seasons_tabControl.TabPages.Count > 0)
            {
                AddFixDateForm fixForm = new AddFixDateForm(seasons_tabControl);
                fixForm.StartPosition = FormStartPosition.Manual;
                fixForm.Location = new Point(this.Left + this.Width / 3, this.Top + this.Height / 3);
                if (fixForm.ShowDialog(this) == DialogResult.OK)
                {
                    AddFixedDate(fixForm.Season, fixForm.Date, fixForm.Tarif, fixForm.StartInterval, fixForm.EndInterval);
                }
                fixForm.Dispose();
            }
        }

        private void AddFixedDate(int season, DateTime date, int selectedTatif, TimeSpan startInterval, TimeSpan endInterval)
        {
            int i = -1;
            for (i = 0; i < seasons_DGV.Length; i++)
            {
                if (seasons_DGV[i].SeasonNumber == season) break;
            }
            int writeRow = -1;
            if (seasons_DGV[i].DGV.Rows.Count > 3)
            {
                for (int j = 3; j < seasons_DGV[i].DGV.Rows.Count; j++)
                {
                    DateTime dt = DateTime.Parse(seasons_DGV[i].DGV.Rows[j].Cells[0].Value + ".2016");
                    if (dt == date)
                    {
                        writeRow = j;
                        break;
                    }
                }
            }

            if (writeRow == -1)
            {
                if (seasons_DGV[i].DGV.Rows.Count - 3 >= MAX_FIX_DAYS)
                {
                    MessageBox.Show("Максимальное количество фиксированных дат в сезоне - " + MAX_FIX_DAYS.ToString() + Environment.NewLine
                    + "(Сезон " + season.ToString() + ")");
                    return;
                }

                seasons_DGV[i].DGV.Rows.Add();
                seasons_DGV[i].DGV.Rows[seasons_DGV[i].DGV.Rows.Count - 1].Cells[0].Value = date.ToString("dd.MM");
                for (int j = 1; j < seasons_DGV[i].DGV.ColumnCount; j++)
                {
                    seasons_DGV[i].DGV.Rows[seasons_DGV[i].DGV.Rows.Count - 1].Cells[j].Value = "-";
                }
                writeRow = seasons_DGV[i].DGV.Rows.Count - 1;
            }

            int recordsCounter = 0;
            for (int tarif = 1; tarif <= 8; tarif++) // смотрим чтобы в одном дне былон не более 48 интервалов
            {
                if (seasons_DGV[i].DGV.Rows[writeRow].Cells[tarif].Value != "-")
                {
                    string[] separator = new string[] { Environment.NewLine };
                    String[] substrings = seasons_DGV[i].DGV.Rows[writeRow].Cells[tarif].Value.ToString().Split(separator, StringSplitOptions.None);
                    recordsCounter += substrings.Length;
                }
            }

            if (recordsCounter >= MAX_INTERVALS)
            {
                MessageBox.Show("Максимальное количество интервалов для одного дня - " + MAX_INTERVALS.ToString() + Environment.NewLine
                    + "(Сезон " + season.ToString() + ", Фиксированный день " + date.ToString("dd.MM)"));
            }
            else
            {
                string newLine = "";
                if (seasons_DGV[i].DGV.Rows[writeRow].Cells[selectedTatif].Value != "-") newLine = Environment.NewLine;
                else seasons_DGV[i].DGV.Rows[writeRow].Cells[selectedTatif].Value = "";
                seasons_DGV[i].DGV.Rows[writeRow].Cells[selectedTatif].Value += newLine +
                    startInterval.Hours.ToString("D2") + ":" + startInterval.Minutes.ToString("D2") + " - "
                    + ((int)(endInterval.TotalHours)).ToString("D2") + ":" + endInterval.Minutes.ToString("D2");

                SortCell(seasons_DGV[i].DGV.Rows[writeRow].Cells[selectedTatif]);
            }
        }

        private void расписаниеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (seasons_tabControl.TabPages.Count > 0)
            {
                AddIntervalForm addIntervalForm = new AddIntervalForm(seasons_tabControl);
                addIntervalForm.StartPosition = FormStartPosition.Manual;
                addIntervalForm.Location = new Point(this.Left + this.Width / 3, this.Top + this.Height / 3);
                if (addIntervalForm.ShowDialog(this) == DialogResult.OK)
                {
                    AddInterval(addIntervalForm.Season, addIntervalForm.Days, addIntervalForm.Tarif,
                        addIntervalForm.StartInterval, addIntervalForm.EndInterval);
                }
                addIntervalForm.Dispose();
            }
        }

        private void AddInterval(int season, bool[] days, int selectedTatif, TimeSpan startInterval, TimeSpan endInterval)
        {
            int i = -1;
            for (i = 0; i < seasons_DGV.Length; i++)
            {
                if (seasons_DGV[i].SeasonNumber == season) break;
            }
            for (int writeRow = 0; writeRow < 3; writeRow++)
            {
                if (days[writeRow])
                {
                    int recordsCounter = 0;
                    for (int tarif = 1; tarif <= 8; tarif++) // смотрим чтобы в одном дне было не более 48 интервалов
                    {
                        if (seasons_DGV[i].DGV.Rows[writeRow].Cells[tarif].Value != "-")
                        {
                            string[] separator = new string[] { Environment.NewLine };
                            String[] substrings = seasons_DGV[i].DGV.Rows[writeRow].Cells[tarif].Value.ToString().Split(separator, StringSplitOptions.None);
                            recordsCounter += substrings.Length;
                        }
                    }

                    if (recordsCounter >= MAX_INTERVALS)
                    {
                        string day = "Рабочие дни";
                        if (writeRow == 1) day = "Суббота";
                        else if (writeRow == 2) day = "Воскресенье";
                        MessageBox.Show("Максимальное количество интервалов для одного дня - " + MAX_INTERVALS.ToString() + Environment.NewLine
                            + "(Сезон " + season.ToString() + ", " + day + ")");
                        continue;
                    }

                    string newLine = "";
                    if (seasons_DGV[i].DGV.Rows[writeRow].Cells[selectedTatif].Value != "-") newLine = Environment.NewLine;
                    else seasons_DGV[i].DGV.Rows[writeRow].Cells[selectedTatif].Value = "";
                    seasons_DGV[i].DGV.Rows[writeRow].Cells[selectedTatif].Value += newLine +
                        startInterval.Hours.ToString("D2") + ":" + startInterval.Minutes.ToString("D2") + " - "
                        + ((int)(endInterval.TotalHours)).ToString("D2") + ":" + endInterval.Minutes.ToString("D2");

                    SortCell(seasons_DGV[i].DGV.Rows[writeRow].Cells[selectedTatif]);
                }
            }
        }

        private void SortCell(DataGridViewCell cell)
        {
            ArrayList myAL = new ArrayList();
            string[] separator2 = new string[] { Environment.NewLine };
            String[] substrings2 = cell.Value.ToString().Split(separator2, StringSplitOptions.None);
            for (int k = 0; k < substrings2.Length; k++) myAL.Add(substrings2[k]);
            myAL.Sort();
            cell.Value = "";
            for (int k = 0; k < substrings2.Length; k++)
            {
                cell.Value += myAL[k].ToString();
                if (k != substrings2.Length - 1) cell.Value += Environment.NewLine;
            }
        }

        private void тестНаОшибкиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (seasons_tabControl.TabPages.Count > 0)
            {
                SeasonStruct[] Seasons = new SeasonStruct[seasons_tabControl.TabPages.Count];
                PackData(ref Seasons);
                TestTarification(Seasons, sender);
            }
            else
            {
                MessageBox.Show("Данные отсутсвуют");
            }
        }

        public bool TestTarification(SeasonStruct[] Seasons, object sender)
        {
            string error = "";
            error = TestDatesStartToEnd(Seasons);
            if (error != "")
            {
                MessageBox.Show(error, "Список ошибок");
                return false;
            }
            error = TestIntersectionOfDates(Seasons);
            if (error != "")
            {
                MessageBox.Show(error, "Список ошибок");
                return false;
            }
            error = TestDayHoleInYear(Seasons);
            if (error != "")
            {
                MessageBox.Show(error, "Список ошибок");
                return false;
            }
            error = TestIntervalsStartToEnd(Seasons);
            if (error != "")
            {
                MessageBox.Show(error, "Список ошибок");
                return false;
            }
            error = TestIntersectionOfIntervals(Seasons);
            if (error != "")
            {
                MessageBox.Show(error, "Список ошибок");
                return false;
            }
            error = TestHolesInDay(Seasons);
            if (error != "")
            {
                MessageBox.Show(error, "Список ошибок");
                return false;
            }

            if (error == "" && (ToolStripMenuItem)sender == тестНаОшибкиToolStripMenuItem)
            {
                MessageBox.Show("Ошибок не обнаружено");
            }
            return true;
        }

        private string TestDatesStartToEnd(SeasonStruct[] Seasons)
        {
            string error = "";
            for (int i = 0; i < Seasons.Length; i++)
            {
                if (Seasons[i].EndDT < Seasons[i].StartDT)
                {
                    error += "Конец сезона " + Seasons[i].SeasonNumber + " указан раньше его начала" + Environment.NewLine;
                }
            }
            return error;
        }

        private string TestIntersectionOfDates(SeasonStruct[] Seasons) // тест на пересечение дат
        {
            string error = "";
            for (int i = 0; i < Seasons.Length; i++)
            {
                for (int j = i; j < Seasons.Length; j++)
                {
                    if (j == i) continue;
                    DateTime Max1, Max2, Min; // Min(Max(Line1_X1, Line1_X2), Max(Line2_X1, Line2_X2)
                    DateTime Min1, Min2, Max; // Max(Min(Line1_X1, Line1_X2), Min(Line2_X1, Line2_X2))

                    if (Seasons[i].StartDT > Seasons[i].EndDT) Max1 = Seasons[i].StartDT;
                    else Max1 = Seasons[i].EndDT;

                    if (Seasons[j].StartDT > Seasons[j].EndDT) Max2 = Seasons[j].StartDT;
                    else Max2 = Seasons[j].EndDT;

                    if (Max1 < Max2) Min = Max1;
                    else Min = Max2;

                    if (Seasons[i].StartDT < Seasons[i].EndDT) Min1 = Seasons[i].StartDT;
                    else Min1 = Seasons[i].EndDT;

                    if (Seasons[j].StartDT < Seasons[j].EndDT) Min2 = Seasons[j].StartDT;
                    else Min2 = Seasons[j].EndDT;

                    if (Min1 > Min2) Max = Min1;
                    else Max = Min2;

                    if (Min >= Max)
                    {
                        error += "Пересечение интервалов сезонов " + Seasons[i].SeasonNumber + " и " + Seasons[j].SeasonNumber + Environment.NewLine;
                    }
                }
            }
            return error;
        }

        private string TestDayHoleInYear(SeasonStruct[] Seasons)
        {
            int dayCounter = 0;
            for (int i = 0; i < Seasons.Length; i++)
            {
                TimeSpan ts = Seasons[i].EndDT - Seasons[i].StartDT;
                dayCounter += (int)ts.TotalDays + 1;
            }
            if (dayCounter == 366) return "";
            else return "Интервалы сезонов не покрывают весь год (пропущено дней: " + (366 - dayCounter).ToString() + ")" + Environment.NewLine;
        }

        private string TestIntervalsStartToEnd(SeasonStruct[] Seasons)
        {
            string error = "";
            for (int season = 0; season < Seasons.Length; season++)
            {
                for (int tarif = 0; tarif < Seasons[season].WorkDays.Tarif.Length; tarif++)
                {
                    for (int interval = 0; interval < Seasons[season].WorkDays.Tarif[tarif].IntervalsActivated; interval++)
                    {
                        if (Seasons[season].WorkDays.Tarif[tarif].Interval[interval].End <=
                            Seasons[season].WorkDays.Tarif[tarif].Interval[interval].Start)
                            error += "Конец интервала указан раньше его начала (Сезон "
                                + Seasons[season].SeasonNumber + ", Рабочие дни, Тариф " + (tarif + 1).ToString() + ")"
                                + Environment.NewLine;
                    }
                    for (int interval = 0; interval < Seasons[season].Saturday.Tarif[tarif].IntervalsActivated; interval++)
                    {
                        if (Seasons[season].Saturday.Tarif[tarif].Interval[interval].End <=
                                Seasons[season].Saturday.Tarif[tarif].Interval[interval].Start)
                            error += "Конец интервала указан раньше его начала (Сезон "
                                + Seasons[season].SeasonNumber + ", Суббота, Тариф " + (tarif + 1).ToString() + ")"
                                + Environment.NewLine;
                    }
                    for (int interval = 0; interval < Seasons[season].Sunday.Tarif[tarif].IntervalsActivated; interval++)
                    {
                        if (Seasons[season].Sunday.Tarif[tarif].Interval[interval].End <=
                                Seasons[season].Sunday.Tarif[tarif].Interval[interval].Start)
                            error += "Конец интервала указан раньше его начала (Сезон "
                                + Seasons[season].SeasonNumber + ", Воскресенье, Тариф " + (tarif + 1).ToString() + ")"
                                + Environment.NewLine;
                    }
                }
                int fixedDays = 0;
                for (int day = 0; day < Seasons[season].FixedDays.Length; day++)
                {
                    if (Seasons[season].FixedDays[day].DayActivated) fixedDays++;
                    else break;
                }
                for (int fixedDay = 0; fixedDay < fixedDays; fixedDay++)
                {
                    for (int tarif = 0; tarif < Seasons[season].WorkDays.Tarif.Length; tarif++)
                    {
                        for (int interval = 0; interval < Seasons[season].FixedDays[fixedDay].Tarif[tarif].IntervalsActivated; interval++)
                        {
                            if (Seasons[season].FixedDays[fixedDay].Tarif[tarif].Interval[interval].End <=
                                Seasons[season].FixedDays[fixedDay].Tarif[tarif].Interval[interval].Start)
                                error += "Конец интервала указан раньше его начала (Сезон "
                                    + Seasons[season].SeasonNumber + ", Фикс. дата "
                                    + Seasons[season].FixedDays[fixedDay].Date.ToString("dd.MM") + ", Тариф " + (tarif + 1).ToString() + ")"
                                    + Environment.NewLine;
                        }
                    }
                }
            }
            return error;
        }

        private string TestIntersectionOfIntervals(SeasonStruct[] Seasons)
        {
            string error = "";
            for (int season = 0; season < Seasons.Length; season++)
            {
                List<TarifStruct.TimeInterval> intervalsWorkDays = new List<TarifStruct.TimeInterval>();
                List<TarifStruct.TimeInterval> intervalsSaturday = new List<TarifStruct.TimeInterval>();
                List<TarifStruct.TimeInterval> intervalsSunday = new List<TarifStruct.TimeInterval>();
                for (int tarif = 0; tarif < Seasons[season].WorkDays.Tarif.Length; tarif++)
                {
                    for (int interval = 0; interval < Seasons[season].WorkDays.Tarif[tarif].IntervalsActivated; interval++)
                    {
                        intervalsWorkDays.Add(Seasons[season].WorkDays.Tarif[tarif].Interval[interval]);
                    }
                    for (int interval = 0; interval < Seasons[season].Saturday.Tarif[tarif].IntervalsActivated; interval++)
                    {
                        intervalsSaturday.Add(Seasons[season].Saturday.Tarif[tarif].Interval[interval]);
                    }
                    for (int interval = 0; interval < Seasons[season].Sunday.Tarif[tarif].IntervalsActivated; interval++)
                    {
                        intervalsSunday.Add(Seasons[season].Sunday.Tarif[tarif].Interval[interval]);
                    }
                }

                if (intervalsWorkDays.Count > MAX_INTERVALS)
                {
                    error += "Количество интервалов >  "
                    + MAX_INTERVALS.ToString() + " (Сезон " + Seasons[season].SeasonNumber.ToString() + ", Рабочие дни)" + Environment.NewLine;
                }
                else if (CheckIntervalsIntersection(intervalsWorkDays))
                {
                    error += "Пересечение интервалов рабочих дней (Сезон" + Seasons[season].SeasonNumber.ToString() + ")" + Environment.NewLine;
                }
                if (intervalsSaturday.Count > MAX_INTERVALS)
                {
                    error += "Количество интервалов > "
                    + MAX_INTERVALS.ToString() + " (Сезон " + Seasons[season].SeasonNumber.ToString() + ", Суббота)" + Environment.NewLine;
                }
                else if (CheckIntervalsIntersection(intervalsSaturday))
                {
                    error += "Пересечение интервалов субботы (Сезон" + Seasons[season].SeasonNumber.ToString() + ")" + Environment.NewLine;
                }
                if (intervalsSunday.Count > MAX_INTERVALS)
                {
                    error += "Количество интервалов > "
                    + MAX_INTERVALS.ToString() + " (Сезон " + Seasons[season].SeasonNumber.ToString() + ", Воскресенье)" + Environment.NewLine;
                }
                else if (CheckIntervalsIntersection(intervalsSunday))
                {
                    error += "Пересечение интервалов воскресенья (Сезон" + Seasons[season].SeasonNumber.ToString() + ")" + Environment.NewLine;
                }

                int fixedDays = 0;
                for (int day = 0; day < Seasons[season].FixedDays.Length; day++)
                {
                    if (Seasons[season].FixedDays[day].DayActivated) fixedDays++;
                    else break;
                }
                for (int fixedDay = 0; fixedDay < fixedDays; fixedDay++)
                {
                    List<TarifStruct.TimeInterval> intervalsFixedDays = new List<TarifStruct.TimeInterval>();
                    for (int tarif = 0; tarif < Seasons[season].WorkDays.Tarif.Length; tarif++)
                    {
                        for (int interval = 0; interval < Seasons[season].FixedDays[fixedDay].Tarif[tarif].IntervalsActivated; interval++)
                        {
                            intervalsFixedDays.Add(Seasons[season].FixedDays[fixedDay].Tarif[tarif].Interval[interval]);
                        }
                    }

                    if (intervalsFixedDays.Count > MAX_INTERVALS)
                    {
                        error += "Количество интервалов >  "
                        + MAX_INTERVALS.ToString() + " (Сезон " + Seasons[season].SeasonNumber.ToString() + ", Фикс. дата " +
                        Seasons[season].FixedDays[fixedDay].Date.ToString("dd.MM)") + Environment.NewLine;
                    }
                    else if (CheckIntervalsIntersection(intervalsFixedDays))
                    {
                        error += "Пересечение интервалов фиксированной даты "
                            + Seasons[season].FixedDays[fixedDay].Date.ToString("dd.MM") +
                            "(Сезон" + Seasons[season].SeasonNumber.ToString() + ")" + Environment.NewLine;
                    }
                }
                for (int fixedDay = 0; fixedDay < fixedDays; fixedDay++) // поиск одинаковых дат
                {
                    for (int j = fixedDay; j < fixedDays; j++)
                    {
                        if (j == fixedDay) continue;
                        if (Seasons[season].FixedDays[fixedDay].Date == Seasons[season].FixedDays[j].Date)
                            error += "Найдены 2 одинаковые фикс. даты "
                            + Seasons[season].FixedDays[fixedDay].Date.ToString("dd.MM и")
                            + Seasons[season].FixedDays[j].Date.ToString("dd.MM") +
                            " (Сезон" + Seasons[season].SeasonNumber.ToString() + ")" + Environment.NewLine;
                    }
                }
                for (int fixedDay = 0; fixedDay < fixedDays; fixedDay++) // входит ли дата в сезон
                {
                    if (Seasons[season].FixedDays[fixedDay].Date < Seasons[season].StartDT ||
                        Seasons[season].FixedDays[fixedDay].Date > Seasons[season].EndDT)
                        error += "Фиксированная дата "
                            + Seasons[season].FixedDays[fixedDay].Date.ToString("dd.MM")
                            + " не входит в диапазон сезона " +
                            Seasons[season].SeasonNumber.ToString() + Environment.NewLine;
                }
            }
            return error;
        }

        private bool CheckIntervalsIntersection(List<TarifStruct.TimeInterval> intervals)
        {
            bool haveError = false;
            for (int i = 0; i < intervals.Count; i++)
            {
                if (haveError) break;
                for (int j = i; j < intervals.Count; j++)
                {
                    if (j == i) continue;
                    TimeSpan Max1, Max2, Min; // Min(Max(Line1_X1, Line1_X2), Max(Line2_X1, Line2_X2)
                    TimeSpan Min1, Min2, Max; // Max(Min(Line1_X1, Line1_X2), Min(Line2_X1, Line2_X2))

                    if (intervals[i].Start > intervals[i].End) Max1 = intervals[i].Start;
                    else Max1 = intervals[i].End;

                    if (intervals[j].Start > intervals[j].End) Max2 = intervals[j].Start;
                    else Max2 = intervals[j].End;

                    if (Max1 < Max2) Min = Max1;
                    else Min = Max2;

                    if (intervals[i].Start < intervals[i].End) Min1 = intervals[i].Start;
                    else Min1 = intervals[i].End;

                    if (intervals[j].Start < intervals[j].End) Min2 = intervals[j].Start;
                    else Min2 = intervals[j].End;

                    if (Min1 > Min2) Max = Min1;
                    else Max = Min2;

                    if (Min > Max)
                    {
                        haveError = true;
                        break;
                    }
                }
            }
            return haveError;
        }

        private string TestHolesInDay(SeasonStruct[] Seasons)
        {
            string error = "";
            for (int season = 0; season < Seasons.Length; season++)
            {
                List<TarifStruct.TimeInterval> intervalsWorkDays = new List<TarifStruct.TimeInterval>();
                List<TarifStruct.TimeInterval> intervalsSaturday = new List<TarifStruct.TimeInterval>();
                List<TarifStruct.TimeInterval> intervalsSunday = new List<TarifStruct.TimeInterval>();
                for (int tarif = 0; tarif < Seasons[season].WorkDays.Tarif.Length; tarif++)
                {
                    for (int interval = 0; interval < Seasons[season].WorkDays.Tarif[tarif].IntervalsActivated; interval++)
                    {
                        intervalsWorkDays.Add(Seasons[season].WorkDays.Tarif[tarif].Interval[interval]);
                    }
                    for (int interval = 0; interval < Seasons[season].Saturday.Tarif[tarif].IntervalsActivated; interval++)
                    {
                        intervalsSaturday.Add(Seasons[season].Saturday.Tarif[tarif].Interval[interval]);
                    }
                    for (int interval = 0; interval < Seasons[season].Sunday.Tarif[tarif].IntervalsActivated; interval++)
                    {
                        intervalsSunday.Add(Seasons[season].Sunday.Tarif[tarif].Interval[interval]);
                    }
                }
                int minuteCounter = 0;
                bool kratnostError = false;
                for (int i = 0; i < intervalsWorkDays.Count; i++)
                {
                    TimeSpan ts = intervalsWorkDays[i].End - intervalsWorkDays[i].Start;
                    if ((int)ts.TotalMinutes % 30 != 0)
                    {
                        error += "Интервалы рабочих дней не кратны 30мин (Сезон" + Seasons[season].SeasonNumber.ToString() + ")" + Environment.NewLine;
                        kratnostError = true;
                        break;
                    }
                    minuteCounter += (int)ts.TotalMinutes;
                }
                if (!kratnostError && minuteCounter < 1440) error += "Интервалы рабочих дней не покрывают сутки (Сезон" + Seasons[season].SeasonNumber.ToString() + ")" + Environment.NewLine;

                minuteCounter = 0;
                kratnostError = false;
                for (int i = 0; i < intervalsSaturday.Count; i++)
                {
                    TimeSpan ts = intervalsSaturday[i].End - intervalsSaturday[i].Start;
                    if ((int)ts.TotalMinutes % 30 != 0)
                    {
                        error += "Интервалы субботы не кратны 30мин (Сезон" + Seasons[season].SeasonNumber.ToString() + ")" + Environment.NewLine;
                        kratnostError = true;
                        break;
                    }
                    minuteCounter += (int)ts.TotalMinutes;
                }
                if (!kratnostError && minuteCounter < 1440) error += "Интервалы субботы не покрывают сутки (Сезон" + Seasons[season].SeasonNumber.ToString() + ")" + Environment.NewLine;

                minuteCounter = 0;
                kratnostError = false;
                for (int i = 0; i < intervalsSunday.Count; i++)
                {
                    TimeSpan ts = intervalsSunday[i].End - intervalsSunday[i].Start;
                    if ((int)ts.TotalMinutes % 30 != 0)
                    {
                        error += "Интервалы воскресеньяы не кратны 30мин (Сезон" + Seasons[season].SeasonNumber.ToString() + ")" + Environment.NewLine;
                        kratnostError = true;
                        break;
                    }
                    minuteCounter += (int)ts.TotalMinutes;
                }
                if (!kratnostError && minuteCounter < 1440) error += "Интервалы воскресенья не покрывают сутки (Сезон" + Seasons[season].SeasonNumber.ToString() + ")" + Environment.NewLine;

                //int fixedDays = 0;
                //for (int day = 0; day < Seasons[season].FixedDays.Length; day++)
                //{
                //    if (Seasons[season].FixedDays[day].DayActivated) fixedDays++;
                //    else break;
                //}
                //for (int fixedDay = 0; fixedDay < fixedDays; fixedDay++)
                //{
                //    List<TarifStruct.TimeInterval> intervalsFixedDays = new List<TarifStruct.TimeInterval>();
                //    for (int tarif = 0; tarif < Seasons[season].WorkDays.Tarif.Length; tarif++)
                //    {
                //        for (int interval = 0; interval < Seasons[season].FixedDays[fixedDay].Tarif[tarif].IntervalsActivated; interval++)
                //        {
                //            intervalsFixedDays.Add(Seasons[season].FixedDays[fixedDay].Tarif[tarif].Interval[interval]);
                //        }
                //    }

                //    minuteCounter = 0;
                //    kratnostError = false;
                //    for (int i = 0; i < intervalsFixedDays.Count; i++)
                //    {
                //        TimeSpan ts = intervalsFixedDays[i].End - intervalsFixedDays[i].Start;
                //        if ((int)ts.TotalMinutes % 30 != 0)
                //        {
                //            error += "Интервалы фикс. дня "
                //                + Seasons[season].FixedDays[fixedDay].Date.ToString("dd.MM") +
                //                " не кратны 30мин (Сезон" + Seasons[season].SeasonNumber.ToString() + ")" + Environment.NewLine;
                //            kratnostError = true;
                //            break;
                //        }
                //        minuteCounter += (int)ts.TotalMinutes;
                //    }
                //    if (!kratnostError && minuteCounter < 1440) error += "Интервалы фикс. дня "
                //        + Seasons[season].FixedDays[fixedDay].Date.ToString("dd.MM") +
                //        " не покрывают сутки (Сезон" + Seasons[season].SeasonNumber.ToString() + ")" + Environment.NewLine;
                //}
            }
            return error;
        }


        private void изменитьИнтервалToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void вФайлToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (seasons_tabControl.TabPages.Count > 0)
            {
                SeasonStruct[] Seasons = new SeasonStruct[seasons_tabControl.TabPages.Count];
                TarifSaveFileDialog.Filter = "(*.tarif)|*.tarif"; // 
                if (PackData(ref Seasons))
                {
                    try
                    {
                        if (TarifSaveFileDialog.ShowDialog() == DialogResult.OK)
                        {
                            BinaryFormatter formatter = new BinaryFormatter();
                            FileStream writerFileStream = new FileStream(TarifSaveFileDialog.FileName, FileMode.Create, FileAccess.Write);
                            formatter.Serialize(writerFileStream, Seasons);
                            writerFileStream.Close();
                        }
                    }
                    catch
                    {
                        MessageBox.Show("Ошибка сохрания файла");
                    }
                }
            }
            else
            {
                MessageBox.Show("Данные отсутсвуют");
            }
        }

        private void изФалаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TarifOpenFileDialog.Filter = "(*.tarif)|*.tarif"; // 
            if (TarifOpenFileDialog.ShowDialog() != DialogResult.OK) return;
            Encoding enc = Encoding.GetEncoding(1251);
            try
            {
                BinaryFormatter formatter = new BinaryFormatter();
                FileStream readerFileStream = new FileStream(TarifOpenFileDialog.FileName, FileMode.Open, FileAccess.Read);
                SeasonStruct[] Seasons = (SeasonStruct[])formatter.Deserialize(readerFileStream);
                UnpackData(Seasons);
                readerFileStream.Close();
            }
            catch
            {
                MessageBox.Show("При открытии файла возникла ошибка");
            }
        }

        private bool PackData(ref SeasonStruct[] Seasons)
        {
            for (int j = 1; j <= seasons_tabControl.TabPages.Count; j++)
            {
                for (int i = 0; i < seasons_DGV.Length; i++)
                {
                    if (seasons_DGV[i].SeasonActivated && seasons_DGV[i].SeasonNumber == j)
                    {
                        Seasons[j - 1] = new SeasonStruct();
                        Seasons[j - 1].SeasonActivated = true;
                        Seasons[j - 1].SeasonNumber = seasons_DGV[i].SeasonNumber;
                        Seasons[j - 1].StartDT = seasons_DGV[i].StartDT;
                        Seasons[j - 1].EndDT = seasons_DGV[i].EndDT;
                    }
                }
            }
            for (int i = 0; i < Seasons.Length; i++) // посезонно заолняем поля
            {
                for (int tarif = 1; tarif <= Seasons[i].WorkDays.Tarif.Length; tarif++)
                {
                    string[] separator = new string[] { Environment.NewLine };

                    String[] workDaysSubstrings = seasons_DGV[i].DGV.Rows[0].Cells[tarif].Value.ToString().Split(separator, StringSplitOptions.None);
                    if (workDaysSubstrings.Length == 1 && workDaysSubstrings[0] == "-") workDaysSubstrings = new String[0];
                    String[] SaturdaySubstrings = seasons_DGV[i].DGV.Rows[1].Cells[tarif].Value.ToString().Split(separator, StringSplitOptions.None);
                    if (SaturdaySubstrings.Length == 1 && SaturdaySubstrings[0] == "-") SaturdaySubstrings = new String[0];
                    String[] SundaySubstrings = seasons_DGV[i].DGV.Rows[2].Cells[tarif].Value.ToString().Split(separator, StringSplitOptions.None);
                    if (SundaySubstrings.Length == 1 && SundaySubstrings[0] == "-") SundaySubstrings = new String[0];

                    Seasons[i].WorkDays.Tarif[tarif - 1].IntervalsActivated = workDaysSubstrings.Length;
                    Seasons[i].Saturday.Tarif[tarif - 1].IntervalsActivated = SaturdaySubstrings.Length;
                    Seasons[i].Sunday.Tarif[tarif - 1].IntervalsActivated = SundaySubstrings.Length;

                    TimeSpan start, end;
                    separator = new string[] { " - " };
                    for (int k = 0; k < workDaysSubstrings.Length; k++)
                    {
                        String[] intervals = workDaysSubstrings[k].Split(separator, StringSplitOptions.None);
                        try
                        {
                            start = TimeSpan.Parse(intervals[0] == "24:00" ? "1.00:00" : intervals[0]);
                            end = TimeSpan.Parse(intervals[1] == "24:00" ? "1.00:00" : intervals[1]);
                            Seasons[i].WorkDays.Tarif[tarif - 1].Interval[k] = new TarifStruct.TimeInterval { Start = start, End = end };
                        }
                        catch
                        {
                            MessageBox.Show("Во время считывания таблицы произошла ошибка" + Environment.NewLine +
                                "Сезон " + Seasons[i].SeasonNumber.ToString() + ", Рабочие дни, Тариф " + tarif.ToString());
                            return false;
                        }
                    }
                    for (int k = 0; k < SaturdaySubstrings.Length; k++)
                    {
                        String[] intervals = SaturdaySubstrings[k].Split(separator, StringSplitOptions.None);
                        try
                        {
                            start = TimeSpan.Parse(intervals[0] == "24:00" ? "1.00:00" : intervals[0]);
                            end = TimeSpan.Parse(intervals[1] == "24:00" ? "1.00:00" : intervals[1]);
                            Seasons[i].Saturday.Tarif[tarif - 1].Interval[k] = new TarifStruct.TimeInterval { Start = start, End = end };
                        }
                        catch
                        {
                            MessageBox.Show("Во время считывания таблицы произошла ошибка" + Environment.NewLine +
                                "Сезон " + Seasons[i].SeasonNumber.ToString() + ", Суббота, Тариф " + tarif.ToString());
                            return false;
                        }
                    }
                    for (int k = 0; k < SundaySubstrings.Length; k++)
                    {
                        String[] intervals = SundaySubstrings[k].Split(separator, StringSplitOptions.None);
                        try
                        {
                            start = TimeSpan.Parse(intervals[0] == "24:00" ? "1.00:00" : intervals[0]);
                            end = TimeSpan.Parse(intervals[1] == "24:00" ? "1.00:00" : intervals[1]);
                            Seasons[i].Sunday.Tarif[tarif - 1].Interval[k] = new TarifStruct.TimeInterval { Start = start, End = end };
                        }
                        catch
                        {
                            MessageBox.Show("Во время считывания таблицы произошла ошибка" + Environment.NewLine +
                               "Сезон " + Seasons[i].SeasonNumber.ToString() + ", Воскресенье, Тариф " + tarif.ToString());
                            return false;
                        }
                    }
                }
                if (seasons_DGV[i].DGV.Rows.Count >= 3) // заполняем фиксированные даты
                {
                    for (int tarif = 1; tarif <= Seasons[i].WorkDays.Tarif.Length; tarif++)
                    {
                        for (int row = 3; row < seasons_DGV[i].DGV.Rows.Count; row++)
                        {
                            string[] separator = new string[] { Environment.NewLine };
                            String[] substrings = seasons_DGV[i].DGV.Rows[row].Cells[tarif].Value.ToString().Split(separator, StringSplitOptions.None);
                            if (substrings.Length == 1 && substrings[0] == "-") substrings = new String[0];

                            DateTime dt = DateTime.Parse(seasons_DGV[i].DGV.Rows[row].Cells[0].Value + ".2016");
                            Seasons[i].FixedDays[row - 3].Date = dt;
                            Seasons[i].FixedDays[row - 3].DayActivated = true;
                            Seasons[i].FixedDays[row - 3].Tarif[tarif - 1].IntervalsActivated = substrings.Length;

                            for (int k = 0; k < substrings.Length; k++)
                            {
                                TimeSpan start, end;
                                separator = new string[] { " - " };
                                String[] intervals = substrings[k].Split(separator, StringSplitOptions.None);
                                try
                                {
                                    start = TimeSpan.Parse(intervals[0] == "24:00" ? "1.00:00" : intervals[0]);
                                    end = TimeSpan.Parse(intervals[1] == "24:00" ? "1.00:00" : intervals[1]);
                                    Seasons[i].FixedDays[row - 3].Tarif[tarif - 1].Interval[k] = new TarifStruct.TimeInterval { Start = start, End = end };
                                }
                                catch
                                {
                                    MessageBox.Show("Во время считывания таблицы произошла ошибка" + Environment.NewLine +
                              "Сезон " + Seasons[i].SeasonNumber.ToString() + ", Фикс. дата "
                              + Seasons[i].FixedDays[row - 3].Date.ToString("dd.MM") + ", Тариф " + tarif.ToString());
                                    return false;
                                }
                            }
                        }
                    }
                }
            }
            return true;
        }

        private delegate void UnpackDataDelegate(SeasonStruct[] Seasons);
        public void UnpackData(SeasonStruct[] Seasons)
        {
            if (seasons_tabControl.InvokeRequired)
            {
                seasons_tabControl.BeginInvoke(new UnpackDataDelegate(UnpackData), new object[] { Seasons });
                return;
            }
            seasons_tabControl.TabPages.Clear();
            удалитьToolStripMenuItem.DropDownItems.Clear();
            изменитьИнтервалToolStripMenuItem.DropDownItems.Clear();
            for (int i = 0; i < seasons_DGV.Length; i++) seasons_DGV[i].SeasonActivated = false;

            for (int season = 0; season < Seasons.Length; season++)
            {
                AddSeason(Seasons[season].StartDT, Seasons[season].EndDT);
                for (int tarif = 0; tarif < Seasons[season].WorkDays.Tarif.Length; tarif++)
                {
                    TimeSpan startInterval = new TimeSpan();
                    TimeSpan endInterval = new TimeSpan();
                    bool[] days = new bool[3] { true, false, false };
                    for (int interval = 0; interval < Seasons[season].WorkDays.Tarif[tarif].IntervalsActivated; interval++)
                    {
                        startInterval = Seasons[season].WorkDays.Tarif[tarif].Interval[interval].Start;
                        endInterval = Seasons[season].WorkDays.Tarif[tarif].Interval[interval].End;
                        AddInterval(Seasons[season].SeasonNumber, days, tarif + 1, startInterval, endInterval);
                    }
                    days = new bool[3] { false, true, false };
                    for (int interval = 0; interval < Seasons[season].Saturday.Tarif[tarif].IntervalsActivated; interval++)
                    {
                        startInterval = Seasons[season].Saturday.Tarif[tarif].Interval[interval].Start;
                        endInterval = Seasons[season].Saturday.Tarif[tarif].Interval[interval].End;
                        AddInterval(Seasons[season].SeasonNumber, days, tarif + 1, startInterval, endInterval);
                    }
                    days = new bool[3] { false, false, true };
                    for (int interval = 0; interval < Seasons[season].Sunday.Tarif[tarif].IntervalsActivated; interval++)
                    {
                        startInterval = Seasons[season].Sunday.Tarif[tarif].Interval[interval].Start;
                        endInterval = Seasons[season].Sunday.Tarif[tarif].Interval[interval].End;
                        AddInterval(Seasons[season].SeasonNumber, days, tarif + 1, startInterval, endInterval);
                    }

                }
                for (int day = 0; day < Seasons[season].FixedDays.Length; day++)
                {
                    if (Seasons[season].FixedDays[day].DayActivated)
                    {
                        for (int tarif = 0; tarif < Seasons[season].WorkDays.Tarif.Length; tarif++)
                        {
                            for (int interval = 0; interval < Seasons[season].FixedDays[day].Tarif[tarif].IntervalsActivated; interval++)
                            {
                                TimeSpan startInterval = Seasons[season].FixedDays[day].Tarif[tarif].Interval[interval].Start;
                                TimeSpan endInterval = Seasons[season].FixedDays[day].Tarif[tarif].Interval[interval].End;
                                AddFixedDate(Seasons[season].SeasonNumber, Seasons[season].FixedDays[day].Date, tarif + 1, startInterval, endInterval);
                            }
                        }
                    }
                    else break;
                }
            }
        }

        private void вПриборToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (seasons_tabControl.TabPages.Count > 0)
            {
                SeasonStruct[] Seasons = new SeasonStruct[seasons_tabControl.TabPages.Count];
                if (PackData(ref Seasons))
                {
                    List<byte>[] TXBuffer = new List<byte>[seasons_tabControl.TabPages.Count];
                    for (int i = 0; i < TXBuffer.Length; i++) TXBuffer[i] = new List<byte>();
                    for (int i = 0; i < Seasons.Length; i++)
                    {
                        TXBuffer[i].Add(new byte()); // в первом байте зарезервируем тарифы
                        TXBuffer[i].Add((byte)Seasons[i].StartDT.Day);
                        TXBuffer[i].Add((byte)Seasons[i].StartDT.Month);
                        TXBuffer[i].Add((byte)Seasons[i].EndDT.Day);
                        TXBuffer[i].Add((byte)Seasons[i].EndDT.Month);

                        int intervals = 0;
                        for (int j = 0; j < Seasons[i].WorkDays.Tarif.Length; j++)
                        {
                            intervals += Seasons[i].WorkDays.Tarif[j].IntervalsActivated;
                        }
                        TXBuffer[i].Add((byte)intervals); // доб кол интервалов в рабочих днях
                        for (int j = 0; j < Seasons[i].WorkDays.Tarif.Length; j++) // доб интервалы раб дней
                        {
                            for (int k = 0; k < Seasons[i].WorkDays.Tarif[j].IntervalsActivated; k++)
                            {
                                TXBuffer[i][0] |= (byte)(0x01 << j);
                                byte start = (byte)(Seasons[i].WorkDays.Tarif[j].Interval[k].Start.Hours);
                                if (Seasons[i].WorkDays.Tarif[j].Interval[k].Start.Minutes == 30) start |= (byte)0x80;
                                byte end = (byte)(Seasons[i].WorkDays.Tarif[j].Interval[k].End.TotalHours);
                                if (Seasons[i].WorkDays.Tarif[j].Interval[k].End.Minutes == 30) end |= (byte)0x80;
                                TXBuffer[i].Add(start);
                                TXBuffer[i].Add(end);
                                TXBuffer[i].Add((byte)(j + 1));
                            }
                        }

                        intervals = 0;
                        for (int j = 0; j < Seasons[i].Saturday.Tarif.Length; j++)
                        {
                            intervals += Seasons[i].Saturday.Tarif[j].IntervalsActivated;
                        }
                        TXBuffer[i].Add((byte)intervals); // доб кол интервалов в субботе
                        for (int j = 0; j < Seasons[i].Saturday.Tarif.Length; j++) // доб интервалы субботы
                        {
                            for (int k = 0; k < Seasons[i].Saturday.Tarif[j].IntervalsActivated; k++)
                            {
                                TXBuffer[i][0] |= (byte)(0x01 << j);
                                byte start = (byte)(Seasons[i].Saturday.Tarif[j].Interval[k].Start.Hours);
                                if (Seasons[i].Saturday.Tarif[j].Interval[k].Start.Minutes == 30) start |= (byte)0x80;
                                byte end = (byte)(Seasons[i].Saturday.Tarif[j].Interval[k].End.TotalHours);
                                if (Seasons[i].Saturday.Tarif[j].Interval[k].End.Minutes == 30) end |= (byte)0x80;
                                TXBuffer[i].Add(start);
                                TXBuffer[i].Add(end);
                                TXBuffer[i].Add((byte)(j + 1));
                            }
                        }

                        intervals = 0;
                        for (int j = 0; j < Seasons[i].Sunday.Tarif.Length; j++)
                        {
                            intervals += Seasons[i].Sunday.Tarif[j].IntervalsActivated;
                        }
                        TXBuffer[i].Add((byte)intervals); // доб кол интервалов в субботе
                        for (int j = 0; j < Seasons[i].Sunday.Tarif.Length; j++) // доб интервалы субботы
                        {
                            for (int k = 0; k < Seasons[i].Sunday.Tarif[j].IntervalsActivated; k++)
                            {
                                TXBuffer[i][0] |= (byte)(0x01 << j);
                                byte start = (byte)(Seasons[i].Sunday.Tarif[j].Interval[k].Start.Hours);
                                if (Seasons[i].Sunday.Tarif[j].Interval[k].Start.Minutes == 30) start |= (byte)0x80;
                                byte end = (byte)(Seasons[i].Sunday.Tarif[j].Interval[k].End.TotalHours);
                                if (Seasons[i].Sunday.Tarif[j].Interval[k].End.Minutes == 30) end |= (byte)0x80;
                                TXBuffer[i].Add(start);
                                TXBuffer[i].Add(end);
                                TXBuffer[i].Add((byte)(j + 1));
                            }
                        }

                        int fixedDays = 0;
                        for (int day = 0; day < Seasons[i].FixedDays.Length; day++)
                        {
                            if (Seasons[i].FixedDays[day].DayActivated) fixedDays++;
                            else break;
                        }
                        TXBuffer[i].Add((byte)fixedDays); // доб кол фик дней
                        for (int fixedDay = 0; fixedDay < fixedDays; fixedDay++)
                        {
                            TXBuffer[i].Add((byte)Seasons[i].FixedDays[fixedDay].Date.Day);
                            TXBuffer[i].Add((byte)Seasons[i].FixedDays[fixedDay].Date.Month);

                            intervals = 0;
                            for (int j = 0; j < Seasons[i].FixedDays[fixedDay].Tarif.Length; j++)
                            {
                                intervals += Seasons[i].FixedDays[fixedDay].Tarif[j].IntervalsActivated;
                            }
                            TXBuffer[i].Add((byte)intervals); // доб кол интервалов в субботе
                            for (int j = 0; j < Seasons[i].FixedDays[fixedDay].Tarif.Length; j++)
                            {
                                for (int k = 0; k < Seasons[i].FixedDays[fixedDay].Tarif[j].IntervalsActivated; k++)
                                {
                                    TXBuffer[i][0] |= (byte)(0x01 << j);
                                    byte start = (byte)(Seasons[i].FixedDays[fixedDay].Tarif[j].Interval[k].Start.Hours);
                                    if (Seasons[i].FixedDays[fixedDay].Tarif[j].Interval[k].Start.Minutes == 30) start |= (byte)0x80;
                                    byte end = (byte)(Seasons[i].FixedDays[fixedDay].Tarif[j].Interval[k].End.TotalHours);
                                    if (Seasons[i].FixedDays[fixedDay].Tarif[j].Interval[k].End.Minutes == 30) end |= (byte)0x80;
                                    TXBuffer[i].Add(start);
                                    TXBuffer[i].Add(end);
                                    TXBuffer[i].Add((byte)(j + 1));
                                }
                            }
                        }
                    }
                    mainForm.tarifPro.PrepareForTX(TXBuffer);
                }
            }
        }

        private void изПрибораToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mainForm.tarifPro.PrepareForRX();
        }
    }
}

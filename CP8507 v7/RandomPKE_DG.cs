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
    public partial class RandomPKE_DG : Form
    {
        private MainForm mainForm;

        public RandomPKE_DG(MainForm form, string text)
        {
            InitializeComponent();

            mainForm = form;
            this.Text = text;
        }

        private delegate void SetPKEDataGridDelegate(string DT, int phase, uint period, float coef);

        public void SetPKEDataGrid(string DT, int phase, uint period, float coef)
        {

            if (DGV.InvokeRequired)
            {
                DGV.BeginInvoke(new SetPKEDataGridDelegate(SetPKEDataGrid), new object[] { DT, phase, period, coef });
            }
            else
            {
                DGV.Rows.Add();
                DGV.Rows[DGV.Rows.Count - 1].Cells[0].Value = DGV.Rows.Count;
                DGV.Rows[DGV.Rows.Count - 1].Cells[1].Value = DT;
                if (phase == 0) DGV.Rows[DGV.Rows.Count - 1].Cells[2].Value = "Фаза А";
                else if (phase == 1) DGV.Rows[DGV.Rows.Count - 1].Cells[2].Value = "Фаза B";
                else if (phase == 2) DGV.Rows[DGV.Rows.Count - 1].Cells[2].Value = "Фаза C";
                DGV.Rows[DGV.Rows.Count - 1].Cells[3].Value = period;
                DGV.Rows[DGV.Rows.Count - 1].Cells[4].Value = coef.ToString("N2");
            }
        }

        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog.Filter = "Документ Excel (*.xlsx)|*.xlsx|Документ Excel (*.csv)|*.csv"; // 

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                Microsoft.Office.Interop.Excel.Application ExcelApp = new Microsoft.Office.Interop.Excel.Application();
                try
                {
                    ExcelApp.DisplayAlerts = false;
                    if (saveFileDialog.FilterIndex == 1)
                    {
                        ExcelApp.DefaultSaveFormat = Microsoft.Office.Interop.Excel.XlFileFormat.xlExcel12; // xlExcel12
                    }
                    else if (saveFileDialog.FilterIndex == 2)
                    {
                        ExcelApp.DefaultSaveFormat = Microsoft.Office.Interop.Excel.XlFileFormat.xlCSV; // xlExcel12
                    }

                    ExcelApp.Workbooks.Add(Type.Missing);

                    ExcelApp.Columns.ColumnWidth = 20;

                    ExcelApp.Cells[1, 1] = "№";
                    ExcelApp.Cells[1, 2] = "Дата \\ Время";
                    ExcelApp.Cells[1, 3] = "Фаза";
                    ExcelApp.Cells[1, 4] = "Δtп, мс";
                    ExcelApp.Cells[1, 5] = "Коэфициент, %";

                    for (int i = 0; i < DGV.RowCount; i++)
                    {
                        progressBar1.Value = (int)(100 / ((float)DGV.RowCount / (i + 1)));
                        for (int j = 0; j < DGV.ColumnCount; j++)
                        {
                            ExcelApp.Cells[i + 2, j + 1] = (DGV[j, i].Value).ToString();
                        }
                    }


                    Microsoft.Office.Interop.Excel.Workbook workBook = ExcelApp.Workbooks[1];
                    workBook.SaveAs(saveFileDialog.FileName);
                    mainForm.StatusLabel = "Файл сохранен";
                    ExcelApp.Quit();
                }
                catch
                {
                    if (ExcelApp != null)
                    {
                        ExcelApp.Quit();
                    }
                    MessageBox.Show("Ошибка сохранения файла");
                }
            }
        }


        private delegate void ProgressBarDelegate(int value);

        public void ProgressBar(int value)
        {
            if (progressBar1.InvokeRequired)
            {
                progressBar1.BeginInvoke(new ProgressBarDelegate(ProgressBar), new object[] { value });
            }
            else
            {
                progressBar1.Value = value;
            }
        }
    }
}

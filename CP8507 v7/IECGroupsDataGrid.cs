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
    public partial class IECGroupsDataGrid : Form
    {
        public IECGroupsDataGrid()
        {
            InitializeComponent();

            int startAddr = 8192;

            DGV.Rows.Add();
            DGV.Rows[DGV.Rows.Count - 1].Cells[1].Value = true;
            DGV.Rows[DGV.Rows.Count - 1].Cells[2].Value = startAddr + DGV.Rows.Count - 1;
            DGV.Rows[DGV.Rows.Count - 1].Cells[3].Value = "Ua";
            DGV.Rows[DGV.Rows.Count - 1].Cells[4].Value = "Напряжение фазное (фаза А)";

            DGV.Rows.Add();
            DGV.Rows[DGV.Rows.Count - 1].Cells[1].Value = true;
            DGV.Rows[DGV.Rows.Count - 1].Cells[2].Value = startAddr + DGV.Rows.Count - 1;
            DGV.Rows[DGV.Rows.Count - 1].Cells[3].Value = "Ub";
            DGV.Rows[DGV.Rows.Count - 1].Cells[4].Value = "Напряжение фазное (фаза B)";

            DGV.Rows.Add();
            DGV.Rows[DGV.Rows.Count - 1].Cells[1].Value = true;
            DGV.Rows[DGV.Rows.Count - 1].Cells[2].Value = startAddr + DGV.Rows.Count - 1;
            DGV.Rows[DGV.Rows.Count - 1].Cells[3].Value = "Uc";
            DGV.Rows[DGV.Rows.Count - 1].Cells[4].Value = "Напряжение фазное (фаза C)";


            DGV.Rows.Add();
            DGV.Rows[DGV.Rows.Count - 1].Cells[1].Value = true;
            DGV.Rows[DGV.Rows.Count - 1].Cells[2].Value = startAddr + DGV.Rows.Count - 1;
            DGV.Rows[DGV.Rows.Count - 1].Cells[3].Value = "Ia";
            DGV.Rows[DGV.Rows.Count - 1].Cells[4].Value = "Ток (фаза А)";

            DGV.Rows.Add();
            DGV.Rows[DGV.Rows.Count - 1].Cells[1].Value = true;
            DGV.Rows[DGV.Rows.Count - 1].Cells[2].Value = startAddr + DGV.Rows.Count - 1;
            DGV.Rows[DGV.Rows.Count - 1].Cells[3].Value = "Ib";
            DGV.Rows[DGV.Rows.Count - 1].Cells[4].Value = "Ток (фаза B)";

            DGV.Rows.Add();
            DGV.Rows[DGV.Rows.Count - 1].Cells[1].Value = true;
            DGV.Rows[DGV.Rows.Count - 1].Cells[2].Value = startAddr + DGV.Rows.Count - 1;
            DGV.Rows[DGV.Rows.Count - 1].Cells[3].Value = "Ic";
            DGV.Rows[DGV.Rows.Count - 1].Cells[4].Value = "Ток (фаза C)";


            DGV.Rows.Add();
            DGV.Rows[DGV.Rows.Count - 1].Cells[1].Value = true;
            DGV.Rows[DGV.Rows.Count - 1].Cells[2].Value = startAddr + DGV.Rows.Count - 1;
            DGV.Rows[DGV.Rows.Count - 1].Cells[3].Value = "Uab";
            DGV.Rows[DGV.Rows.Count - 1].Cells[4].Value = "Напряжение линейное (фаза А)";

            DGV.Rows.Add();
            DGV.Rows[DGV.Rows.Count - 1].Cells[1].Value = true;
            DGV.Rows[DGV.Rows.Count - 1].Cells[2].Value = startAddr + DGV.Rows.Count - 1;
            DGV.Rows[DGV.Rows.Count - 1].Cells[3].Value = "Ubc";
            DGV.Rows[DGV.Rows.Count - 1].Cells[4].Value = "Напряжение линейное (фаза B)";

            DGV.Rows.Add();
            DGV.Rows[DGV.Rows.Count - 1].Cells[1].Value = true;
            DGV.Rows[DGV.Rows.Count - 1].Cells[2].Value = startAddr + DGV.Rows.Count - 1;
            DGV.Rows[DGV.Rows.Count - 1].Cells[3].Value = "Uca";
            DGV.Rows[DGV.Rows.Count - 1].Cells[4].Value = "Напряжение линейное (фаза C)";


            DGV.Rows.Add();
            DGV.Rows[DGV.Rows.Count - 1].Cells[1].Value = true;
            DGV.Rows[DGV.Rows.Count - 1].Cells[2].Value = startAddr + DGV.Rows.Count - 1;
            DGV.Rows[DGV.Rows.Count - 1].Cells[3].Value = "Pa";
            DGV.Rows[DGV.Rows.Count - 1].Cells[4].Value = "Мощность активная (фаза А)";

            DGV.Rows.Add();
            DGV.Rows[DGV.Rows.Count - 1].Cells[1].Value = true;
            DGV.Rows[DGV.Rows.Count - 1].Cells[2].Value = startAddr + DGV.Rows.Count - 1;
            DGV.Rows[DGV.Rows.Count - 1].Cells[3].Value = "Pb";
            DGV.Rows[DGV.Rows.Count - 1].Cells[4].Value = "Мощность активная (фаза B)";

            DGV.Rows.Add();
            DGV.Rows[DGV.Rows.Count - 1].Cells[1].Value = true;
            DGV.Rows[DGV.Rows.Count - 1].Cells[2].Value = startAddr + DGV.Rows.Count - 1;
            DGV.Rows[DGV.Rows.Count - 1].Cells[3].Value = "Pc";
            DGV.Rows[DGV.Rows.Count - 1].Cells[4].Value = "Мощность активная (фаза C)";


            DGV.Rows.Add();
            DGV.Rows[DGV.Rows.Count - 1].Cells[1].Value = true;
            DGV.Rows[DGV.Rows.Count - 1].Cells[2].Value = startAddr + DGV.Rows.Count - 1;
            DGV.Rows[DGV.Rows.Count - 1].Cells[3].Value = "Qa";
            DGV.Rows[DGV.Rows.Count - 1].Cells[4].Value = "Мощность реактивная (фаза А)";

            DGV.Rows.Add();
            DGV.Rows[DGV.Rows.Count - 1].Cells[1].Value = true;
            DGV.Rows[DGV.Rows.Count - 1].Cells[2].Value = startAddr + DGV.Rows.Count - 1;
            DGV.Rows[DGV.Rows.Count - 1].Cells[3].Value = "Qb";
            DGV.Rows[DGV.Rows.Count - 1].Cells[4].Value = "Мощность реактивная (фаза B)";

            DGV.Rows.Add();
            DGV.Rows[DGV.Rows.Count - 1].Cells[1].Value = true;
            DGV.Rows[DGV.Rows.Count - 1].Cells[2].Value = startAddr + DGV.Rows.Count - 1;
            DGV.Rows[DGV.Rows.Count - 1].Cells[3].Value = "Qc";
            DGV.Rows[DGV.Rows.Count - 1].Cells[4].Value = "Мощность реактивная (фаза C)";


            DGV.Rows.Add();
            DGV.Rows[DGV.Rows.Count - 1].Cells[1].Value = true;
            DGV.Rows[DGV.Rows.Count - 1].Cells[2].Value = startAddr + DGV.Rows.Count - 1;
            DGV.Rows[DGV.Rows.Count - 1].Cells[3].Value = "Sa";
            DGV.Rows[DGV.Rows.Count - 1].Cells[4].Value = "Мощность полная (фаза А)";

            DGV.Rows.Add();
            DGV.Rows[DGV.Rows.Count - 1].Cells[1].Value = true;
            DGV.Rows[DGV.Rows.Count - 1].Cells[2].Value = startAddr + DGV.Rows.Count - 1;
            DGV.Rows[DGV.Rows.Count - 1].Cells[3].Value = "Sb";
            DGV.Rows[DGV.Rows.Count - 1].Cells[4].Value = "Мощность полная (фаза B)";

            DGV.Rows.Add();
            DGV.Rows[DGV.Rows.Count - 1].Cells[1].Value = true;
            DGV.Rows[DGV.Rows.Count - 1].Cells[2].Value = startAddr + DGV.Rows.Count - 1;
            DGV.Rows[DGV.Rows.Count - 1].Cells[3].Value = "Sc";
            DGV.Rows[DGV.Rows.Count - 1].Cells[4].Value = "Мощность полная (фаза C)";


            DGV.Rows.Add();
            DGV.Rows[DGV.Rows.Count - 1].Cells[1].Value = true;
            DGV.Rows[DGV.Rows.Count - 1].Cells[2].Value = startAddr + DGV.Rows.Count - 1;
            DGV.Rows[DGV.Rows.Count - 1].Cells[3].Value = "KPa";
            DGV.Rows[DGV.Rows.Count - 1].Cells[4].Value = "Коэффициент мощности (фаза А)";

            DGV.Rows.Add();
            DGV.Rows[DGV.Rows.Count - 1].Cells[1].Value = true;
            DGV.Rows[DGV.Rows.Count - 1].Cells[2].Value = startAddr + DGV.Rows.Count - 1;
            DGV.Rows[DGV.Rows.Count - 1].Cells[3].Value = "KPb";
            DGV.Rows[DGV.Rows.Count - 1].Cells[4].Value = "Коэффициент мощности (фаза B)";

            DGV.Rows.Add();
            DGV.Rows[DGV.Rows.Count - 1].Cells[1].Value = true;
            DGV.Rows[DGV.Rows.Count - 1].Cells[2].Value = startAddr + DGV.Rows.Count - 1;
            DGV.Rows[DGV.Rows.Count - 1].Cells[3].Value = "KPc";
            DGV.Rows[DGV.Rows.Count - 1].Cells[4].Value = "Коэффициент мощности (фаза C)";


            DGV.Rows.Add();
            DGV.Rows[DGV.Rows.Count - 1].Cells[1].Value = true;
            DGV.Rows[DGV.Rows.Count - 1].Cells[2].Value = startAddr + DGV.Rows.Count - 1;
            DGV.Rows[DGV.Rows.Count - 1].Cells[3].Value = "P";
            DGV.Rows[DGV.Rows.Count - 1].Cells[4].Value = "Мощность активная суммарная";

            DGV.Rows.Add();
            DGV.Rows[DGV.Rows.Count - 1].Cells[1].Value = true;
            DGV.Rows[DGV.Rows.Count - 1].Cells[2].Value = startAddr + DGV.Rows.Count - 1;
            DGV.Rows[DGV.Rows.Count - 1].Cells[3].Value = "Q";
            DGV.Rows[DGV.Rows.Count - 1].Cells[4].Value = "Мощность реактивная суммарная";

            DGV.Rows.Add();
            DGV.Rows[DGV.Rows.Count - 1].Cells[1].Value = true;
            DGV.Rows[DGV.Rows.Count - 1].Cells[2].Value = startAddr + DGV.Rows.Count - 1;
            DGV.Rows[DGV.Rows.Count - 1].Cells[3].Value = "S";
            DGV.Rows[DGV.Rows.Count - 1].Cells[4].Value = "Мощность полная суммарная";

            DGV.Rows.Add();
            DGV.Rows[DGV.Rows.Count - 1].Cells[1].Value = true;
            DGV.Rows[DGV.Rows.Count - 1].Cells[2].Value = startAddr + DGV.Rows.Count - 1;
            DGV.Rows[DGV.Rows.Count - 1].Cells[3].Value = "KP";
            DGV.Rows[DGV.Rows.Count - 1].Cells[4].Value = "Коэффициент мощности суммарный";

            DGV.Rows.Add();
            DGV.Rows[DGV.Rows.Count - 1].Cells[1].Value = true;
            DGV.Rows[DGV.Rows.Count - 1].Cells[2].Value = startAddr + DGV.Rows.Count - 1;
            DGV.Rows[DGV.Rows.Count - 1].Cells[3].Value = "F";
            DGV.Rows[DGV.Rows.Count - 1].Cells[4].Value = "Частота";

            DGV.Rows.Add();
            DGV.Rows[DGV.Rows.Count - 1].Cells[1].Value = true;
            DGV.Rows[DGV.Rows.Count - 1].Cells[2].Value = startAddr + DGV.Rows.Count - 1;
            DGV.Rows[DGV.Rows.Count - 1].Cells[3].Value = "U";
            DGV.Rows[DGV.Rows.Count - 1].Cells[4].Value = "Напряжение фазное среднее";

            DGV.Rows.Add();
            DGV.Rows[DGV.Rows.Count - 1].Cells[1].Value = true;
            DGV.Rows[DGV.Rows.Count - 1].Cells[2].Value = startAddr + DGV.Rows.Count - 1;
            DGV.Rows[DGV.Rows.Count - 1].Cells[3].Value = "I";
            DGV.Rows[DGV.Rows.Count - 1].Cells[4].Value = "Ток средний";

            DGV.Rows.Add();
            DGV.Rows[DGV.Rows.Count - 1].Cells[1].Value = true;
            DGV.Rows[DGV.Rows.Count - 1].Cells[2].Value = startAddr + DGV.Rows.Count - 1;
            DGV.Rows[DGV.Rows.Count - 1].Cells[3].Value = "Ul";
            DGV.Rows[DGV.Rows.Count - 1].Cells[4].Value = "Напряжение линейное среднее";

            DGV.Rows.Add();
            DGV.Rows[DGV.Rows.Count - 1].Cells[1].Value = true;
            DGV.Rows[DGV.Rows.Count - 1].Cells[2].Value = startAddr + DGV.Rows.Count - 1;
            DGV.Rows[DGV.Rows.Count - 1].Cells[3].Value = "U0";
            DGV.Rows[DGV.Rows.Count - 1].Cells[4].Value = "Напряжение нулевой последовательности";

            DGV.Rows.Add();
            DGV.Rows[DGV.Rows.Count - 1].Cells[1].Value = true;
            DGV.Rows[DGV.Rows.Count - 1].Cells[2].Value = startAddr + DGV.Rows.Count - 1;
            DGV.Rows[DGV.Rows.Count - 1].Cells[3].Value = "I0";
            DGV.Rows[DGV.Rows.Count - 1].Cells[4].Value = "Ток нулевой последовательности";

            DataGridViewCellStyle color1 = new DataGridViewCellStyle();
            color1.BackColor = Color.LightGreen;
            for (int i = 0; i < 10; i++)
            {
                DGV.Rows[i].Cells[0].Value = "1";
                DGV.Rows[i].DefaultCellStyle = color1;
            }

            DataGridViewCellStyle color2 = new DataGridViewCellStyle();
            color2.BackColor = Color.LightCyan;
            for (int i = 10; i < 20; i++)
            {
                DGV.Rows[i].Cells[0].Value = "2";
                DGV.Rows[i].DefaultCellStyle = color2;
            }

            DataGridViewCellStyle color3 = new DataGridViewCellStyle();
            color3.BackColor = Color.PapayaWhip;
            for (int i = 20; i < DGV.Rows.Count; i++)
            {
                DGV.Rows[i].Cells[0].Value = "3";
                DGV.Rows[i].DefaultCellStyle = color3;
            }

        }


    }
}

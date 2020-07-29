﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraPrinting;
using DevExpress.XtraPrinting.Native;
using static DevExpress.Utils.Drawing.Helpers.NativeMethods;
using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraCharts;
using MySql.Data.MySqlClient;
using DevExpress.XtraCharts.Printing;

namespace TaxesSystem
{
    public partial class GraphicDelegateAbsence_Report : DevExpress.XtraEditors.XtraForm
    {
        MySqlConnection dbconnection;
        Series series = new Series("Series1", ViewType.Bar);
        bool loaded = false;

        public GraphicDelegateAbsence_Report()
        {
            InitializeComponent();
            dbconnection = new MySqlConnection(connection.connectionString);
        }

        private void GraphicDelegateBusy_Report_Load(object sender, EventArgs e)
        {
            try
            {
                //series.DataSource = CreateChartData();
                series.Label.TextPattern = "{A} : {V}";
                //chartControl1.Series.Add(series);

                series.ArgumentDataMember = "المندوب";
                series.ValueDataMembers.AddRange(new string[] { "العدد" });
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /*private DataTable CreateChartData()
        {
            string query = "select Name as 'المندوب', count(Absence_Date) as 'العدد' from attendance where Absence_Date is not null and Error=0 and Delegate_ID is not null group by Delegate_ID";
            MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }*/

        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                chartControl1.OptionsPrint.SizeMode = PrintSizeMode.Stretch;
                chartControl1.OptionsPrint.ImageFormat = DevExpress.XtraCharts.Printing.PrintImageFormat.Bitmap;
                chartControl1.ShowRibbonPrintPreview();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                string query = "select Name as 'المندوب', count(Absence_Date) as 'العدد' from attendance where Absence_Date is not null and Error=0 and Delegate_ID is not null and DATE_FORMAT(Absence_Date,'%Y-%m-%d') between '" + this.dateTimePicker1.Value.ToString("yyyy-MM-dd") + "' and '" + this.dateTimePicker2.Value.ToString("yyyy-MM-dd") + "' group by Delegate_ID";
                MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                DataTable dt = new DataTable();
                da.Fill(dt);
                series.DataSource = dt;
                chartControl1.Series.Clear();
                chartControl1.Series.Add(series);

                if (!loaded)
                {
                    ChartTitle chartTitle1 = new ChartTitle();
                    chartTitle1.Text = "تقرير الغياب";
                    chartTitle1.TextColor = Color.DarkBlue;
                    chartTitle1.Alignment = StringAlignment.Center;
                    chartControl1.Titles.Add(chartTitle1);
                    loaded = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
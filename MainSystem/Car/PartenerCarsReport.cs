using DevExpress.XtraGrid.Columns;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TaxesSystem
{
    public partial class PartenerCarsReport : Form
    {
        MySqlConnection dbconnection;
        MainForm CarsMainForm;
        public PartenerCarsReport(MainForm CarsMainForm)
        {
            InitializeComponent();
            dbconnection = new MySqlConnection(connection.connectionString);
            this.CarsMainForm = CarsMainForm;
        }

        private void PartenerCarsReport_Load(object sender, EventArgs e)
        {
            try
            {
                dbconnection.Open();
                string query = "select * from cars";
                MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                DataTable dt = new DataTable();
                da.Fill(dt);
                comCarNumber.DataSource = dt;
                comCarNumber.DisplayMember = dt.Columns["Car_Number"].ToString();
                comCarNumber.ValueMember = dt.Columns["Car_ID"].ToString();
                comCarNumber.Text = "";

                query = "select * from drivers";
                da = new MySqlDataAdapter(query, dbconnection);
                dt = new DataTable();
                da.Fill(dt);
                comDriver.DataSource = dt;
                comDriver.DisplayMember = dt.Columns["Driver_Name"].ToString();
                comDriver.ValueMember = dt.Columns["Driver_ID"].ToString();
                comDriver.Text = "";

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                string d = dateTimeFrom.Text;
                string d2 = dateTimeTo.Text;
                string q = "";
                if (comCarNumber.Text != "")
                {
                    q += " and cars.Car_ID=" + comCarNumber.SelectedValue.ToString();
                }
                if (comDriver.Text != "")
                {
                    q += " and drivers.Driver_ID=" + comDriver.SelectedValue.ToString();
                }

                string query = "select cars.Car_Number as 'رقم العربية',drivers.Driver_Name as 'السائق', ClientName as 'اسم العميل',GROUP_CONCAT(Permission_Number) as 'رقم الاذن',NoCarton as 'عدد الكراتين',NoSets as 'عدد الاطقم',NoDocks as 'عدد الاحواض',NoColumns as 'عدد العواميد',NoCompinations as 'عدد الكوبينشن',NoPanio as 'عدد البانيوهات',DATE_FORMAT(Date,'%Y-%m-%d') as 'التاريخ',Note as'ملاحظات' from car_partner_income inner join cars on cars.Car_ID=car_partner_income.Car_ID inner join driver_car on cars.Car_ID=driver_car.Car_ID inner join drivers on drivers.Driver_ID=driver_car.Driver_ID inner join car_partner_permission on car_partner_permission.CarPartner_Income_ID=car_partner_income.CarPartner_Income_ID where cars.Type=0 "+q+" and Date between '" + d + "' and '" + d2 + "' group by car_partner_income.CarPartner_Income_ID";

                if (comCarNumber.Text != "")
                {
                    query = "select drivers.Driver_Name as 'السائق', ClientName as 'اسم العميل',GROUP_CONCAT(Permission_Number) as 'رقم الاذن',sum(NoCarton) as 'عدد الكراتين',sum(NoSets) as 'عدد الاطقم',sum(NoDocks) as 'عدد الاحواض',sum(NoColumns) as 'عدد العواميد',sum(NoCompinations) as 'عدد الكوبينشن',sum(NoPanio) as 'عدد البانيوهات',DATE_FORMAT(Date,'%Y-%m-%d') as 'التاريخ',Note as'ملاحظات' from car_partner_income inner join cars on cars.Car_ID=car_partner_income.Car_ID inner join driver_car on cars.Car_ID=driver_car.Car_ID inner join drivers on drivers.Driver_ID=driver_car.Driver_ID inner join car_partner_permission on car_partner_permission.CarPartner_Income_ID=car_partner_income.CarPartner_Income_ID where cars.Type=0 " + q + " and Date between '" + d + "' and '" + d2 + "' group by car_partner_income.CarPartner_Income_ID,cars.Car_Number ";
                }
                if (comDriver.Text != "")
                {
                    query = "select cars.Car_Number as 'رقم العربية', ClientName as 'اسم العميل',GROUP_CONCAT(Permission_Number) as 'رقم الاذن',sum(NoCarton) as 'عدد الكراتين',sum(NoSets) as 'عدد الاطقم',sum(NoDocks) as 'عدد الاحواض',sum(NoColumns) as 'عدد العواميد',sum(NoCompinations) as 'عدد الكوبينشن',sum(NoPanio) as 'عدد البانيوهات',DATE_FORMAT(Date,'%Y-%m-%d') as 'التاريخ',Note as'ملاحظات' from car_partner_income inner join cars on cars.Car_ID=car_partner_income.Car_ID inner join driver_car on cars.Car_ID=driver_car.Car_ID inner join drivers on drivers.Driver_ID=driver_car.Driver_ID inner join car_partner_permission on car_partner_permission.CarPartner_Income_ID=car_partner_income.CarPartner_Income_ID where cars.Type=0 " + q + " and Date between '" + d + "' and '" + d2 + "' group by car_partner_income.CarPartner_Income_ID ,drivers.Driver_ID ";
                }

                if (comDriver.Text != "" && comCarNumber.Text != "")
                {
                    query = "select ClientName as 'اسم العميل',GROUP_CONCAT(Permission_Number) as 'رقم الاذن',sum(NoCarton) as 'عدد الكراتين',sum(NoSets) as 'عدد الاطقم',sum(NoDocks) as 'عدد الاحواض',sum(NoColumns) as 'عدد العواميد',sum(NoCompinations) as 'عدد الكوبينشن',sum(NoPanio) as 'عدد البانيوهات',DATE_FORMAT(Date,'%Y-%m-%d') as 'التاريخ',Note as'ملاحظات' from car_partner_income inner join cars on cars.Car_ID=car_partner_income.Car_ID inner join driver_car on cars.Car_ID=driver_car.Car_ID inner join drivers on drivers.Driver_ID=driver_car.Driver_ID inner join car_partner_permission on car_partner_permission.CarPartner_Income_ID=car_partner_income.CarPartner_Income_ID where cars.Type=0 " + q + " and Date between '" + d + "' and '" + d2 + "' group by car_partner_income.CarPartner_Income_ID ,drivers.Driver_ID,cars.Car_Number ";
                }
                MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                DataTable dt = new DataTable();
                da.Fill(dt);
                gridView1.Columns.Clear();
                gridControl1.DataSource = dt;
              
                gridView1.Columns["عدد الكراتين"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
                gridView1.Columns["عدد الكراتين"].SummaryItem.DisplayFormat = "{0:n2}";

                gridView1.Columns["عدد الاطقم"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
                gridView1.Columns["عدد الاطقم"].SummaryItem.DisplayFormat = "{0:n2}";

                gridView1.Columns["عدد الاحواض"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
                gridView1.Columns["عدد الاحواض"].SummaryItem.DisplayFormat = "{0:n2}";

                gridView1.Columns["عدد العواميد"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
                gridView1.Columns["عدد العواميد"].SummaryItem.DisplayFormat = "{0:n2}";

                gridView1.Columns["عدد الكوبينشن"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
                gridView1.Columns["عدد الكوبينشن"].SummaryItem.DisplayFormat = "{0:n2}";

                gridView1.Columns["عدد البانيوهات"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
                gridView1.Columns["عدد البانيوهات"].SummaryItem.DisplayFormat = "{0:n2}";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            try
            {
                CarsMainForm.bindReportPartnerIncomesForm(gridControl1);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}

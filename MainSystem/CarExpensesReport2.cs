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
    public partial class CarExpensesReport2 : Form
    {
        MySqlConnection dbconnection;
        MainForm carsMainForm;
        public CarExpensesReport2(MainForm carsMainForm)
        {
            dbconnection = new MySqlConnection(connection.connectionString);
            InitializeComponent();
            this.carsMainForm = carsMainForm;
        }

        private void CarExpensesReport2_Load(object sender, EventArgs e)
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

                query = "select * from expense_type";
                da = new MySqlDataAdapter(query, dbconnection);
                dt = new DataTable();
                da.Fill(dt);
                comExpensesType.DataSource = dt;
                comExpensesType.DisplayMember = dt.Columns["Type"].ToString();
                comExpensesType.ValueMember = dt.Columns["ID"].ToString();
                comExpensesType.Text = "";
                // loaded = true;
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
                dbconnection.Open();
                DateTime date = dateTimeFrom.Value;
                string d = date.ToString("yyyy-MM-dd HH:mm:ss");
                DateTime date2 = dateTimeTo.Value;
                string d2 = date2.ToString("yyyy-MM-dd HH:mm:ss");
                string subQuery = "";
                bool flagCarID=true, flagExpenses_Type_ID = true;
                if (comCarNumber.Text != "")
                {
                    subQuery += " and cars.Car_ID=" + comCarNumber.SelectedValue;
                    flagCarID = false;
                }

                if (comExpensesType.Text != "")
                {
                    subQuery += " and Expenses_Type_ID="+comExpensesType.SelectedValue;
                    flagExpenses_Type_ID = false;
                }

                string query = "select ID as 'الرقم المسلسل', cars.Car_Number as 'رقم العربية',Expenses_Type as 'نوع المصروفات',Cost as 'التكلفة',DATE_FORMAT(Date,'%Y-%m-%d') as 'التاريخ',Note as 'الملاحظات' from car_expenses inner join cars on car_expenses.Car_ID=cars.Car_ID where Date between '" + d + "' and '" + d2 + "'  "+subQuery;

                MySqlCommand com = new MySqlCommand(query, dbconnection);
                MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                DataTable dt = new DataTable();
                da.Fill(dt);
                gridControl1.DataSource = dt;
                gridView1.Columns[1].Visible = flagCarID;
                gridView1.Columns[2].Visible = flagExpenses_Type_ID;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            try
            {
                carsMainForm.bindReportExpensesForm(gridControl1);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}

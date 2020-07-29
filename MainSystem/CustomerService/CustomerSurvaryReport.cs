using DevExpress.XtraEditors.Repository;
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

namespace TaxesSystem.CustomerService
{
    public partial class CustomerSurvaryReport : Form
    {
        MySqlConnection dbconnection;
        MainForm MainForm;
        public CustomerSurvaryReport(MainForm MainForm)
        {
            InitializeComponent();
            this.MainForm = MainForm;
            dbconnection = new MySqlConnection(connection.connectionString);
        }

        private void CustomerDeliveredBills_Load(object sender, EventArgs e)
        {
            try
            {
                dbconnection.Open();

                string query = "select * from branch";
                MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                DataTable dt = new DataTable();
                da.Fill(dt);

                comBranch.DataSource = dt;
                comBranch.DisplayMember = dt.Columns["Branch_Name"].ToString();
                comBranch.ValueMember = dt.Columns["Branch_ID"].ToString();
                comBranch.Text = "";
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
                displayProducts();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }
        private void btnNewChooes_Click(object sender, EventArgs e)
        {
            try
            {
                comBranch.Text = "";
                dateTimeFrom.Text = DateTime.Now.Date.ToString();
                dateTimeTo.Text = DateTime.Now.Date.ToString();
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
                MainForm.bindReportDeliveredCustomerBillsForm(dataGridView1, "استبيانات العملاء");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        void gridView2_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            try
            {
                DataRow dataRow = gridView2.GetDataRow(gridView2.FocusedRowHandle);
                MainForm.bindDisplayDeliveryBillsForm(dataRow);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
  
        //function
        public void displayProducts()
        {
            try
            {
                string subQuery = "where date(Date) between '" + dateTimeFrom.Value.ToString("yyyy-MM-dd") + "' and '" + dateTimeTo.Value.ToString("yyyy-MM-dd") + "'";

                if (comBranch.Text!="")
                {
                    subQuery += " and Branch_Name='" + comBranch.Text+"'";
                }
            
                string query = "SELECT Customer_Name as 'الاسم',Customer_Phone as 'التلفون',Customer_Address as 'عنوان العميل',Branch_Name as 'الفرع',Bill_Date as 'تاريخ الفاتورة',Description as 'الوصف',Communication_Way as 'طرق الاتصال',Communication_Info as 'معلومات الاتصال',Purchasing_Survey as 'تقييم عملية البيع',Delegate_Survey as 'تقييم المندوب',Showroom_Survey as 'تقييم العرض',Date as 'تاريخ الاستبيان' FROM customer_service_survey inner join branch on branch.Branch_ID=customer_service_survey.Branch_ID "+ subQuery;

                MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                DataTable dt = new DataTable();
                da.Fill(dt);
                //Bind the grid control to the data source 
                dataGridView1.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        
    }
}

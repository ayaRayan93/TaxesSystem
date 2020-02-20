using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraPrinting;
using DevExpress.XtraPrinting.Preview;
using DevExpress.XtraTab;
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

namespace MainSystem
{
    public partial class CustomerServiceAfterReceived_Report : Form
    {
        MySqlConnection dbconnection;
        int branchID = 0;
        bool loaded = false;
        DataRow selRow = null;

        public CustomerServiceAfterReceived_Report(MainForm BankMainForm/*, DataRow selrow*/)
        {
            InitializeComponent();
            dbconnection = new MySqlConnection(connection.connectionString);
            //selRow = selrow;
            //bankMainForm = BankMainForm;
            //MainTabControlBank = MainForm.tabControlBank;
        }

        private void Bills_Transitions_Report_Load(object sender, EventArgs e)
        {
            try
            {
                if (!loaded)
                {
                    loadBranch();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }


        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                dbconnection.Open();

                string query = "insert into customer_service_survey () values(@)";
                MySqlCommand com = new MySqlCommand(query, dbconnection);
                com.Parameters.Add("@Branch_ID", MySqlDbType.Int16, 11).Value = comBranch.SelectedValue.ToString();
                com.Parameters.Add("@BranchBillNumber", MySqlDbType.Int16, 11).Value = txtBillNum.Text;
                com.Parameters.Add("@CustomerBill_ID", MySqlDbType.Int16, 11).Value = selRow["CustomerBill_ID"].ToString();
                com.Parameters.Add("@Delegate_Name", MySqlDbType.VarChar, 255).Value = txtDelegate.Text;
                com.Parameters.Add("@Customer_Name", MySqlDbType.VarChar, 255).Value = txtClient.Text;
                com.Parameters.Add("@Customer_Phone", MySqlDbType.VarChar, 255).Value = txtPhone.Text;
                com.Parameters.Add("@Customer_Address", MySqlDbType.VarChar, 255).Value = txtAddress.Text;
                com.Parameters.Add("@Bill_Date", MySqlDbType.DateTime, 0).Value = dateTimePicker1.Value.Date;
                com.Parameters.Add("@Description", MySqlDbType.VarChar, 255).Value = txtComplaint.Text;
                com.Parameters.Add("@Communication_Way", MySqlDbType.VarChar, 255).Value = "";
                com.Parameters.Add("@Communication_Info", MySqlDbType.VarChar, 255).Value = txtConnection;
                com.Parameters.Add("@Purchasing_Survey", MySqlDbType.Int16, 11).Value = 0;
                com.Parameters.Add("@Delegate_Survey", MySqlDbType.Int16, 11).Value = 0;
                com.Parameters.Add("@Showroom_Survey", MySqlDbType.Int16, 11).Value = 0;
                com.Parameters.Add("@Date", MySqlDbType.DateTime, 0).Value = DateTime.Now;
                com.Parameters.Add("@Employee_ID", MySqlDbType.Int16, 11).Value = UserControl.EmpID;

                com.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }

        //functions
        private void loadBranch()
        {
            dbconnection.Open();
            string query = "select * from branch";
            MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
            DataTable dt = new DataTable();
            da.Fill(dt);
            comBranch.DataSource = dt;
            comBranch.DisplayMember = dt.Columns["Branch_Name"].ToString();
            comBranch.ValueMember = dt.Columns["Branch_ID"].ToString();
            comBranch.SelectedIndex = -1;
            comBranch.Text = selRow["الفرع"].ToString();
            txtBillNum.Text = selRow["رقم الفاتورة"].ToString();
            txtClient.Text = selRow["المستلم"].ToString();
            txtPhone.Text = selRow["تلفون المستلم"].ToString();
            txtDelegate.Text = selRow["المندوب"].ToString();
            dateTimePicker1.Value = Convert.ToDateTime(selRow["التاريخ"].ToString());
            txtAddress.Text = selRow["العنوان"].ToString();

            loaded = true;
        }
        
        public void clearCom()
        {
            foreach (Control co in this.tableLayoutPanel4.Controls)
            {
                if (co is System.Windows.Forms.ComboBox)
                {
                    co.Text = "";
                }
                else if (co is TextBox)
                {
                    co.Text = "";
                }
                else if (co is DateTimePicker)
                {
                    dateTimePicker1.Value = DateTime.Now;
                }
            }
        }
    }
}

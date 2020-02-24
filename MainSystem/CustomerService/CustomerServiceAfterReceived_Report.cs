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
        bool loaded = false;
        DataRow selRow = null;
        string CommunicationWay = "";
        int CustomerBill_ID = 0;
        MainForm mainForm = null;
        XtraTabControl tabControlCustomerService = null;

        public CustomerServiceAfterReceived_Report(XtraTabControl tabControlCustomerservice, MainForm mainform, DataRow dataRow)
        {
            InitializeComponent();
            dbconnection = new MySqlConnection(connection.connectionString);
            selRow = dataRow;
            mainForm = mainform;
            tabControlCustomerService = tabControlCustomerservice;
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

                string query = "insert into customer_service_survey (Branch_ID,BranchBillNumber,CustomerBill_ID,Delegate_Name,Customer_Name,Customer_Phone,Customer_Address,Bill_Date,Description,Communication_Way,Communication_Info,Purchasing_Survey,Delegate_Survey,Showroom_Survey,Date,Employee_ID) values(@Branch_ID,@BranchBillNumber,@CustomerBill_ID,@Delegate_Name,@Customer_Name,@Customer_Phone,@Customer_Address,@Bill_Date,@Description,@Communication_Way,@Communication_Info,@Purchasing_Survey,@Delegate_Survey,@Showroom_Survey,@Date,@Employee_ID)";
                MySqlCommand com = new MySqlCommand(query, dbconnection);
                com.Parameters.Add("@Branch_ID", MySqlDbType.Int16, 11).Value = comBranch.SelectedValue.ToString();
                com.Parameters.Add("@BranchBillNumber", MySqlDbType.Int16, 11).Value = txtBillNum.Text;
                com.Parameters.Add("@CustomerBill_ID", MySqlDbType.Int16, 11).Value = CustomerBill_ID;
                com.Parameters.Add("@Delegate_Name", MySqlDbType.VarChar, 255).Value = txtDelegate.Text;
                com.Parameters.Add("@Customer_Name", MySqlDbType.VarChar, 255).Value = txtClient.Text;
                com.Parameters.Add("@Customer_Phone", MySqlDbType.VarChar, 255).Value = txtPhone.Text;
                com.Parameters.Add("@Customer_Address", MySqlDbType.VarChar, 255).Value = txtAddress.Text;
                com.Parameters.Add("@Bill_Date", MySqlDbType.DateTime, 0).Value = dateTimePicker1.Value.Date;
                com.Parameters.Add("@Description", MySqlDbType.VarChar, 255).Value = txtComplaint.Text;
                com.Parameters.Add("@Communication_Way", MySqlDbType.VarChar, 255).Value = CommunicationWay;
                com.Parameters.Add("@Communication_Info", MySqlDbType.VarChar, 255).Value = txtCommunication.Text;
                com.Parameters.Add("@Purchasing_Survey", MySqlDbType.Int16, 11).Value = ratingControlPurchasing.EditValue.ToString();
                com.Parameters.Add("@Delegate_Survey", MySqlDbType.Int16, 11).Value = ratingControlDelegate.EditValue.ToString();
                com.Parameters.Add("@Showroom_Survey", MySqlDbType.Int16, 11).Value = ratingControlShowroom.EditValue.ToString();
                com.Parameters.Add("@Date", MySqlDbType.DateTime, 0).Value = DateTime.Now;
                com.Parameters.Add("@Employee_ID", MySqlDbType.Int16, 11).Value = UserControl.EmpID;
                com.ExecuteNonQuery();

                XtraTabPage xtraTabPage = getTabPage("استبيان");
                tabControlCustomerService.TabPages.Remove(xtraTabPage);
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
            dateTimePicker1.Value = Convert.ToDateTime(selRow["التاريخ"].ToString());
            txtAddress.Text = selRow["العنوان"].ToString();
            query = "select CustomerBill_ID from customer_bill where Branch_Name='"+ comBranch.Text+"' and Branch_BillNumber=" + txtBillNum.Text;
            MySqlCommand com = new MySqlCommand(query, dbconnection);
            CustomerBill_ID = Convert.ToInt16(com.ExecuteScalar());
            query = "select GROUP_CONCAT(DISTINCT delegate.Delegate_Name) from product_bill INNER JOIN delegate on   product_bill.Delegate_ID=delegate.Delegate_ID where CustomerBill_ID="+CustomerBill_ID;
            com = new MySqlCommand(query, dbconnection);
            txtDelegate.Text = com.ExecuteScalar().ToString();

            loaded = true;
        }

        public XtraTabPage getTabPage(string text)
        {
            for (int i = 0; i < tabControlCustomerService.TabPages.Count; i++)
                if (tabControlCustomerService.TabPages[i].Text == text)
                {
                    return tabControlCustomerService.TabPages[i];
                }
            return null;
        }
        
        private void radFaceBook_CheckedChanged(object sender, EventArgs e)
        {
            CommunicationWay = "FaceBook";
            txtCommunication.Text = "";
        }

        private void radInstagram_CheckedChanged(object sender, EventArgs e)
        {
            CommunicationWay = "Instagram";
            txtCommunication.Text = "";
        }

        private void radWhatsApp_CheckedChanged(object sender, EventArgs e)
        {
            CommunicationWay = "WhatsApp";
            txtCommunication.Text = txtPhone.Text;
        }

        private void radEmail_CheckedChanged(object sender, EventArgs e)
        {
            CommunicationWay = "Email";
            txtCommunication.Text = "";
        }
    }
}

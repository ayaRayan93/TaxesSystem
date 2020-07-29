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

namespace TaxesSystem
{
    public partial class SafePropertyIncome_Record : Form
    {
        MySqlConnection dbconnection;
        bool loaded = false;
        XtraTabControl tabControlProperty;
        int transitionbranchID = 0;

        public SafePropertyIncome_Record(XtraTabControl MainTabControlProperty)
        {
            InitializeComponent();
            dbconnection = new MySqlConnection(connection.connectionString);
            tabControlProperty = MainTabControlProperty;

            comBank.AutoCompleteMode = AutoCompleteMode.Suggest;
            comBank.AutoCompleteSource = AutoCompleteSource.ListItems;
        }

        private void BankPullProperty_Record_Load(object sender, EventArgs e)
        {
            try
            {
                if (!loaded)
                {
                    transitionbranchID = UserControl.EmpBranchID;
                    loadBranch();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }
        
        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (comBank.Text != "" && txtPullMoney.Text != "")
                {
                    double outParse;
                    if (double.TryParse(txtPullMoney.Text, out outParse))
                    {
                        dbconnection.Open();
                        
                        string query = "insert into property_transition (Branch_ID,Depositor_Name,Bank_ID,Date,Amount,Description,Type,Employee_ID) values(@Branch_ID,@Depositor_Name,@Bank_ID,@Date,@Amount,@Description,@Type,@Employee_ID)";
                        MySqlCommand com = new MySqlCommand(query, dbconnection);

                        com.Parameters.Add("@Type", MySqlDbType.VarChar, 255).Value = "ايداع";
                        com.Parameters.Add("@Branch_ID", MySqlDbType.Int16, 11).Value = transitionbranchID;
                        com.Parameters.Add("@Bank_ID", MySqlDbType.Int16, 11).Value = comBank.SelectedValue;
                        com.Parameters.Add("@Date", MySqlDbType.DateTime, 0).Value = dateTimePicker1.Value.Date;
                        com.Parameters.Add("@Depositor_Name", MySqlDbType.VarChar, 255).Value = txtClient.Text;
                        com.Parameters.Add("@Description", MySqlDbType.VarChar, 255).Value = txtDescrip.Text;
                        com.Parameters.Add("@Employee_ID", MySqlDbType.Int16).Value = UserControl.EmpID;
                        com.Parameters.Add("@Amount", MySqlDbType.Decimal, 10).Value = txtPullMoney.Text;
                        com.ExecuteNonQuery();

                        MySqlCommand com2 = new MySqlCommand("select Bank_Stock from bank where Bank_ID=" + comBank.SelectedValue, dbconnection);
                        double amount2 = Convert.ToDouble(com2.ExecuteScalar().ToString());
                        amount2 += outParse;
                        MySqlCommand com3 = new MySqlCommand("update bank set Bank_Stock=" + amount2 + " where Bank_ID=" + comBank.SelectedValue, dbconnection);
                        com3.ExecuteNonQuery();

                        //////////record adding/////////////
                        query = "select PropertyTransition_ID from property_transition order by PropertyTransition_ID desc limit 1";
                        com = new MySqlCommand(query, dbconnection);
                        string PropertyTransitionID = com.ExecuteScalar().ToString();

                        query = "insert into usercontrol (UserControl_UserID,UserControl_TableName,UserControl_Status,UserControl_RecordID,UserControl_Date,UserControl_Reason) values(@UserControl_UserID,@UserControl_TableName,@UserControl_Status,@UserControl_RecordID,@UserControl_Date,@UserControl_Reason)";
                        com = new MySqlCommand(query, dbconnection);
                        com.Parameters.Add("@UserControl_UserID", MySqlDbType.Int16, 11).Value = UserControl.userID;
                        com.Parameters.Add("@UserControl_TableName", MySqlDbType.VarChar, 255).Value = "property_transition";
                        com.Parameters.Add("@UserControl_Status", MySqlDbType.VarChar, 255).Value = "اضافة";
                        com.Parameters.Add("@UserControl_RecordID", MySqlDbType.VarChar, 255).Value = PropertyTransitionID;
                        com.Parameters.Add("@UserControl_Date", MySqlDbType.DateTime, 0).Value = DateTime.Now;
                        com.Parameters.Add("@UserControl_Reason", MySqlDbType.VarChar, 255).Value = null;
                        com.ExecuteNonQuery();

                        printProperty(Convert.ToInt32(PropertyTransitionID));
                        clear();
                    }
                    else
                    {
                        MessageBox.Show("المبلغ المدفوع يجب ان يكون عدد");
                    }
                }
                else
                {
                    MessageBox.Show("برجاء ادخال جميع البيانات المطلوبة");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }

        //clear function
        public void clear()
        {
            foreach (Control co in this.panel1.Controls)
            {
                foreach (Control item in co.Controls)
                {
                    if (item is ComboBox)
                    {
                        item.Text = "";
                    }
                    else if (item is TextBox)
                    {
                        item.Text = "";
                    }
                }
            }
        }

        //functions
        private void loadBranch()
        {
            dbconnection.Open();

            string query = "";
            
            if (UserControl.userType == 27)
            {
                query = "select * from bank INNER JOIN bank_employee ON bank_employee.Bank_ID = bank.Bank_ID where bank.Branch_ID=" + transitionbranchID + " and bank_employee.Employee_ID=" + UserControl.EmpID + "";
            }
            else
            {
                query = "select * from bank inner join bank_main on bank.MainBank_ID=bank_main.MainBank_ID where MainBank_Type='خزينة' and MainBank_Name='خزينة عقارات'";
            }

            MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
            DataTable dt = new DataTable();
            da.Fill(dt);
            comBank.DataSource = dt;
            comBank.DisplayMember = dt.Columns["Bank_Name"].ToString();
            comBank.ValueMember = dt.Columns["Bank_ID"].ToString();
            comBank.SelectedIndex = -1;

            loaded = true;
            dbconnection.Close();
        }

        void printProperty(int PropertyTransitionID)
        {
            //Print_IncomeExpense_Report f = new Print_IncomeExpense_Report();
            //f.PrintInvoice(ExpenseTransitionID, comBank.Text, txtPullMoney.Text, txtClient.Text, txtDescrip.Text);
            //f.ShowDialog();
        }
    }
}

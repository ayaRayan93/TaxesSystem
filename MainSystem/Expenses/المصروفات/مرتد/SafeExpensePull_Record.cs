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
    public partial class SafeExpensePull_Record : Form
    {
        MySqlConnection dbconnection;
        bool loaded = false;
        XtraTabControl tabControlExpense;
        int transitionbranchID = 0;

        public SafeExpensePull_Record(XtraTabControl MainTabControlExpense)
        {
            InitializeComponent();
            dbconnection = new MySqlConnection(connection.connectionString);
            tabControlExpense = MainTabControlExpense;

            comBank.AutoCompleteMode = AutoCompleteMode.Suggest;
            comBank.AutoCompleteSource = AutoCompleteSource.ListItems;
        }

        private void BankPullExpense_Record_Load(object sender, EventArgs e)
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

        private void txtExpenseNum_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                dbconnection.Close();
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtExpenseNum.Text != "" && comBank.Text != "" && txtPullMoney.Text != "" && txtDescrip.Text != "" && txtClient.Text != "")
                {
                    double outParse;
                    int ExpenseNum;
                    if (double.TryParse(txtPullMoney.Text, out outParse) && int.TryParse(txtExpenseNum.Text, out ExpenseNum))
                    {
                        dbconnection.Open();
                        
                        string query = "insert into expense_transition (PullExpenseTransition_ID,Branch_ID,Depositor_Name,Bank_ID,Date,Amount,Description,Type,Employee_ID) values(@PullExpenseTransition_ID,@Branch_ID,@Depositor_Name,@Bank_ID,@Date,@Amount,@Description,@Type,@Employee_ID)";
                        MySqlCommand com = new MySqlCommand(query, dbconnection);

                        com.Parameters.Add("@Type", MySqlDbType.VarChar, 255).Value = "مرتد";
                        com.Parameters.Add("@PullExpenseTransition_ID", MySqlDbType.Int16).Value = txtExpenseNum.Text;
                        com.Parameters.Add("@Branch_ID", MySqlDbType.Int16, 11).Value = transitionbranchID;
                        com.Parameters.Add("@Bank_ID", MySqlDbType.Int16, 11).Value = comBank.SelectedValue;
                        com.Parameters.Add("@Date", MySqlDbType.DateTime, 0).Value = DateTime.Now;
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
                        
                        com2 = new MySqlCommand("select Amount from expense_transition where ExpenseTransition_ID=" + txtExpenseNum.Text, dbconnection);
                        amount2 = Convert.ToDouble(com2.ExecuteScalar().ToString());
                        amount2 -= outParse;
                        com3 = new MySqlCommand("update expense_transition set Amount=" + amount2 + " where ExpenseTransition_ID=" + txtExpenseNum.Text, dbconnection);
                        com3.ExecuteNonQuery();

                        //////////record adding/////////////
                        query = "select ExpenseTransition_ID from expense_transition order by ExpenseTransition_ID desc limit 1";
                        com = new MySqlCommand(query, dbconnection);
                        string ExpenseTransitionID = com.ExecuteScalar().ToString();

                        query = "insert into usercontrol (UserControl_UserID,UserControl_TableName,UserControl_Status,UserControl_RecordID,UserControl_Date,UserControl_Reason) values(@UserControl_UserID,@UserControl_TableName,@UserControl_Status,@UserControl_RecordID,@UserControl_Date,@UserControl_Reason)";
                        com = new MySqlCommand(query, dbconnection);
                        com.Parameters.Add("@UserControl_UserID", MySqlDbType.Int16, 11).Value = UserControl.userID;
                        com.Parameters.Add("@UserControl_TableName", MySqlDbType.VarChar, 255).Value = "expense_transition";
                        com.Parameters.Add("@UserControl_Status", MySqlDbType.VarChar, 255).Value = "اضافة";
                        com.Parameters.Add("@UserControl_RecordID", MySqlDbType.VarChar, 255).Value = ExpenseTransitionID;
                        com.Parameters.Add("@UserControl_Date", MySqlDbType.DateTime, 0).Value = DateTime.Now;
                        com.Parameters.Add("@UserControl_Reason", MySqlDbType.VarChar, 255).Value = null;
                        com.ExecuteNonQuery();
                        
                        printExpense(Convert.ToInt32(ExpenseTransitionID));
                        clear();
                    }
                    else
                    {
                        MessageBox.Show("رقم المصروف والمبلغ المدفوع يجب ان يكون عدد");
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
            
            if (UserControl.userType == 3 || UserControl.userType == 16)
            {
                query = "select * from bank INNER JOIN bank_employee ON bank_employee.Bank_ID = bank.Bank_ID where bank.Branch_ID=" + transitionbranchID + " and bank_employee.Employee_ID=" + UserControl.EmpID + "";
            }
            //else if (UserControl.userType == 7)
            //{
            //    query = "select * from bank where Branch_ID=" + transitionbranchID + " and Bank_Type='خزينة مصروفات'";
            //}
            else /*if (UserControl.userType == 1)*/
            {
                query = "select * from bank inner join bank_main on bank.MainBank_ID=bank_main.MainBank_ID where MainBank_Type='خزينة' and MainBank_Name = 'خزينة مبيعات'";
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

        void printExpense(int ExpenseTransitionID)
        {
            Print_PullExpense_Report f = new Print_PullExpense_Report();
            f.PrintInvoice(ExpenseTransitionID, txtExpenseNum.Text, comBank.Text, txtPullMoney.Text, txtClient.Text, txtDescrip.Text);
            f.ShowDialog();
        }
    }
}

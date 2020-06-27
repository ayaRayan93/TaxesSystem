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
    public partial class SafeExpense_Update : Form
    {
        MySqlConnection dbconnection;
        bool loaded = false;
        XtraTabControl tabControlExpense;
        int transitionbranchID = 0;
        DataRowView selRow = null;

        public SafeExpense_Update(DataRowView selrow, Expenses_Transitions_Report ExpensesTransitionsReport, XtraTabControl MainTabControlExpense, MainForm mainform)
        {
            InitializeComponent();
            dbconnection = new MySqlConnection(connection.connectionString);
            tabControlExpense = MainTabControlExpense;
            selRow = selrow;
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

        private void comMain_SelectedValueChanged(object sender, EventArgs e)
        {
            if (loaded)
            {
                try
                {
                    string query = "select * from expense_sub where MainExpense_ID=" + comMain.SelectedValue.ToString();
                    MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    comSub.DataSource = dt;
                    comSub.DisplayMember = dt.Columns["SubExpense_Name"].ToString();
                    comSub.ValueMember = dt.Columns["SubExpense_ID"].ToString();
                    comSub.SelectedIndex = -1;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (comMain.Text != "" && comSub.Text != "" && comBank.Text != "" && txtPullMoney.Text != "")
                {
                    double money;
                    if (double.TryParse(txtPullMoney.Text, out money))
                    {
                        dbconnection.Open();
                        string query = "select Bank_Stock from bank where Bank_ID=" + comBank.SelectedValue.ToString();
                        MySqlCommand comand = new MySqlCommand(query, dbconnection);
                        double Bank_Stock = Convert.ToDouble(comand.ExecuteScalar().ToString());
                        Bank_Stock += Convert.ToDouble(selRow["مصروف"].ToString());
                        
                        if (money > Bank_Stock)
                        {
                            MessageBox.Show("لا يوجد ما يكفى");
                            dbconnection.Close();
                            return;
                        }
                        
                        query = "update expense_transition set SubExpense_ID=@SubExpense_ID,Depositor_Name=@Depositor_Name,Amount=@Amount,Description=@Description where ExpenseTransition_ID=" + selRow["التسلسل"].ToString();
                        MySqlCommand com = new MySqlCommand(query, dbconnection);
                        com.Parameters.Add("@SubExpense_ID", MySqlDbType.Int16, 11).Value = comSub.SelectedValue.ToString();
                        com.Parameters.Add("@Depositor_Name", MySqlDbType.VarChar, 255).Value = txtClient.Text;
                        com.Parameters.Add("@Description", MySqlDbType.VarChar, 255).Value = txtDescrip.Text;
                        com.Parameters.Add("@Amount", MySqlDbType.Decimal, 10).Value = txtPullMoney.Text;
                        com.ExecuteNonQuery();

                        Bank_Stock -= money;
                        MySqlCommand com3 = new MySqlCommand("update bank set Bank_Stock=" + Bank_Stock + " where Bank_ID=" + comBank.SelectedValue, dbconnection);
                        com3.ExecuteNonQuery();

                        //////////record adding/////////////

                        query = "insert into usercontrol (UserControl_UserID,UserControl_TableName,UserControl_Status,UserControl_RecordID,UserControl_Date,UserControl_Reason) values(@UserControl_UserID,@UserControl_TableName,@UserControl_Status,@UserControl_RecordID,@UserControl_Date,@UserControl_Reason)";
                        com = new MySqlCommand(query, dbconnection);
                        com.Parameters.Add("@UserControl_UserID", MySqlDbType.Int16, 11).Value = UserControl.userID;
                        com.Parameters.Add("@UserControl_TableName", MySqlDbType.VarChar, 255).Value = "expense_transition";
                        com.Parameters.Add("@UserControl_Status", MySqlDbType.VarChar, 255).Value = "تعديل";
                        com.Parameters.Add("@UserControl_RecordID", MySqlDbType.VarChar, 255).Value = selRow["التسلسل"].ToString();
                        com.Parameters.Add("@UserControl_Date", MySqlDbType.DateTime, 0).Value = DateTime.Now;
                        com.Parameters.Add("@UserControl_Reason", MySqlDbType.VarChar, 255).Value = null;
                        com.ExecuteNonQuery();

                        printExpense(Convert.ToInt32(selRow["التسلسل"].ToString()));
                        
                        XtraTabPage xtraTabPage = getTabPage("تعديل مصروف");
                        tabControlExpense.TabPages.Remove(xtraTabPage);
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
        public XtraTabPage getTabPage(string text)
        {
            for (int i = 0; i < tabControlExpense.TabPages.Count; i++)
                if (tabControlExpense.TabPages[i].Text == text)
                {
                    return tabControlExpense.TabPages[i];
                }
            return null;
        }
        
        //functions
        private void loadBranch()
        {
            dbconnection.Open();

            string query = "select * from expense_main";
            MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
            DataTable dt = new DataTable();
            da.Fill(dt);
            comMain.DataSource = dt;
            comMain.DisplayMember = dt.Columns["MainExpense_Name"].ToString();
            comMain.ValueMember = dt.Columns["MainExpense_ID"].ToString();
            comMain.SelectedIndex = -1;
            loaded = true;
            comMain.Text = selRow["المصروف الرئيسى"].ToString();
            comSub.Text = selRow["المصروف الفرعى"].ToString();
            
            if (UserControl.userType == 3 || UserControl.userType == 16)
            {
                query = "select * from bank INNER JOIN bank_employee ON bank_employee.Bank_ID = bank.Bank_ID where bank.Branch_ID=" + transitionbranchID + " and bank_employee.Employee_ID=" + UserControl.EmpID + "";
            }
            else
            {
                query = "select * from bank inner join bank_main on bank.MainBank_ID=bank_main.MainBank_ID where MainBank_Type='خزينة' and MainBank_Name = 'خزينة مبيعات'";
            }
            da = new MySqlDataAdapter(query, dbconnection);
            dt = new DataTable();
            da.Fill(dt);
            comBank.DataSource = dt;
            comBank.DisplayMember = dt.Columns["Bank_Name"].ToString();
            comBank.ValueMember = dt.Columns["Bank_ID"].ToString();
            comBank.SelectedIndex = -1;
            comBank.Text = selRow["الخزينة"].ToString();

            txtPullMoney.Text = selRow["مصروف"].ToString();
            txtClient.Text = selRow["المودع/المستلم"].ToString();
            txtDescrip.Text = selRow["البيان"].ToString();

            dbconnection.Close();
        }

        void printExpense(int ExpenseTransitionID)
        {
            Print_Expense_Report_Copy f = new Print_Expense_Report_Copy();
            f.PrintInvoice(ExpenseTransitionID, comMain.Text, comSub.Text, comBank.Text, txtPullMoney.Text, txtClient.Text, txtDescrip.Text, selRow["التاريخ"].ToString());
            f.ShowDialog();
        }
    }
}

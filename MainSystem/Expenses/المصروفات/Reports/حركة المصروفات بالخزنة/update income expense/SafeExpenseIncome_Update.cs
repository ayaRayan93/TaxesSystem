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
    public partial class SafeExpenseIncome_Update : Form
    {
        MySqlConnection dbconnection;
        bool loaded = false;
        XtraTabControl tabControlExpense;
        int transitionbranchID = 0;
        DataRowView selRow = null;

        public SafeExpenseIncome_Update(DataRowView selrow, XtraTabControl MainTabControlExpense, MainForm mainform)
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
        
        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (comBank.Text != "" && txtPullMoney.Text != "")
                {
                    double money;
                    if (double.TryParse(txtPullMoney.Text, out money))
                    {
                        dbconnection.Open();
                        string query = "select Bank_Stock from bank where Bank_ID=" + comBank.SelectedValue.ToString();
                        MySqlCommand comand = new MySqlCommand(query, dbconnection);
                        double Bank_Stock = Convert.ToDouble(comand.ExecuteScalar().ToString());
                        Bank_Stock -= Convert.ToDouble(selRow["وارد"].ToString());

                        if (money + Bank_Stock < 0)
                        {
                            MessageBox.Show("الرصيد سيصبح اقل من الصفر");
                        }
                        
                        query = "update expense_transition set Depositor_Name=@Depositor_Name,Amount=@Amount,Description=@Description,Date=@Date where ExpenseTransition_ID=" + selRow["التسلسل"].ToString();
                        MySqlCommand com = new MySqlCommand(query, dbconnection);
                        com.Parameters.Add("@Depositor_Name", MySqlDbType.VarChar, 255).Value = txtClient.Text;
                        com.Parameters.Add("@Description", MySqlDbType.VarChar, 255).Value = txtDescrip.Text;
                        com.Parameters.Add("@Amount", MySqlDbType.Decimal, 10).Value = txtPullMoney.Text;
                        com.Parameters.Add("@Date", MySqlDbType.DateTime, 0).Value = dateTimePicker1.Value.Date;
                        com.ExecuteNonQuery();

                        Bank_Stock += money;
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
                        
                        XtraTabPage xtraTabPage = getTabPage("تعديل ايداع مصروف");
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

            string query = "";
            
            if (UserControl.userType == 3 || UserControl.userType == 16)
            {
                query = "select * from bank INNER JOIN bank_employee ON bank_employee.Bank_ID = bank.Bank_ID where bank.Branch_ID=" + transitionbranchID + " and bank_employee.Employee_ID=" + UserControl.EmpID + "";
            }
            else
            {
                query = "select * from bank inner join bank_main on bank.MainBank_ID=bank_main.MainBank_ID where MainBank_Type='خزينة' and (MainBank_Name = 'خزينة مبيعات' or MainBank_Name = 'خزينة حسابات')";
            }
            MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
            DataTable dt = new DataTable();
            da.Fill(dt);
            comBank.DataSource = dt;
            comBank.DisplayMember = dt.Columns["Bank_Name"].ToString();
            comBank.ValueMember = dt.Columns["Bank_ID"].ToString();
            comBank.SelectedIndex = -1;
            comBank.Text = selRow["الخزينة"].ToString();

            txtPullMoney.Text = selRow["وارد"].ToString();
            txtClient.Text = selRow["المودع/المستلم"].ToString();
            txtDescrip.Text = selRow["البيان"].ToString();
            dateTimePicker1.Value = Convert.ToDateTime(selRow["التاريخ"].ToString());

            loaded = true;
            dbconnection.Close();
        }

        void printExpense(int ExpenseTransitionID)
        {
            Print_IncomeExpense_Report_Copy f = new Print_IncomeExpense_Report_Copy();
            f.PrintInvoice(ExpenseTransitionID, comBank.Text, txtPullMoney.Text, txtClient.Text, txtDescrip.Text, selRow["التاريخ"].ToString());
            f.ShowDialog();
        }
    }
}

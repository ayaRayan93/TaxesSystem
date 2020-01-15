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
    public partial class SafeExpense_Record2 : Form
    {
        MySqlConnection dbconnection;
        bool loaded = false;
        XtraTabControl tabControlExpense;
        int transitionbranchID = 0;

        public SafeExpense_Record2(XtraTabControl MainTabControlExpense)
        {
            InitializeComponent();
            dbconnection = new MySqlConnection(connection.connectionString);
            tabControlExpense = MainTabControlExpense;

            comBank.AutoCompleteMode = AutoCompleteMode.Suggest;
            comBank.AutoCompleteSource = AutoCompleteSource.ListItems;

            comMain.AutoCompleteMode = AutoCompleteMode.Suggest;
            comMain.AutoCompleteSource = AutoCompleteSource.ListItems;

            comSub.AutoCompleteMode = AutoCompleteMode.Suggest;
            comSub.AutoCompleteSource = AutoCompleteSource.ListItems;
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
                    double outParse;
                    if (double.TryParse(txtPullMoney.Text, out outParse))
                    {
                        dbconnection.Open();
                        MySqlCommand com2 = new MySqlCommand("select Bank_Stock from bank where Bank_ID=" + comBank.SelectedValue, dbconnection);
                        double amount2 = Convert.ToDouble(com2.ExecuteScalar().ToString());

                        if ((amount2 - outParse) >= 0)
                        {
                            string query = "insert into expense_transition (Branch_ID,Depositor_Name,Bank_ID,Date,Amount,Description,SubExpense_ID,Type,Employee_ID) values(@Branch_ID,@Depositor_Name,@Bank_ID,@Date,@Amount,@Description,@SubExpense_ID,@Type,@Employee_ID)";
                            MySqlCommand com = new MySqlCommand(query, dbconnection);

                            com.Parameters.Add("@Type", MySqlDbType.VarChar, 255).Value = "صرف";
                            com.Parameters.Add("@Branch_ID", MySqlDbType.Int16, 11).Value = transitionbranchID;
                            com.Parameters.Add("@SubExpense_ID", MySqlDbType.Int16, 11).Value = comSub.SelectedValue.ToString();
                            com.Parameters.Add("@Bank_ID", MySqlDbType.Int16, 11).Value = comBank.SelectedValue;
                            com.Parameters.Add("@Date", MySqlDbType.DateTime, 0).Value = DateTime.Now;
                            com.Parameters.Add("@Depositor_Name", MySqlDbType.VarChar, 255).Value = txtClient.Text;
                            com.Parameters.Add("@Description", MySqlDbType.VarChar, 255).Value = txtDescrip.Text;
                            com.Parameters.Add("@Employee_ID", MySqlDbType.Int16).Value = UserControl.EmpID;
                            com.Parameters.Add("@Amount", MySqlDbType.Decimal, 10).Value = txtPullMoney.Text;
                            com.ExecuteNonQuery();

                            amount2 -= outParse;
                            MySqlCommand com3 = new MySqlCommand("update bank set Bank_Stock=" + amount2 + " where Bank_ID=" + comBank.SelectedValue, dbconnection);
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
                            MessageBox.Show("لا يوجد رصيد كافى");
                            dbconnection.Close();
                            return;
                        }
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
            if (UserControl.userType == 16)
            {
                query = "select * from expense_main where MainExpense_ID=16";
            }
            else
            {
                query = "select * from expense_main";
            }
            MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
            DataTable dt = new DataTable();
            da.Fill(dt);
            comMain.DataSource = dt;
            comMain.DisplayMember = dt.Columns["MainExpense_Name"].ToString();
            comMain.ValueMember = dt.Columns["MainExpense_ID"].ToString();
            comMain.SelectedIndex = -1;
            
            if (UserControl.userType == 3 || UserControl.userType == 16)
            {
                query = "select * from bank INNER JOIN bank_employee ON bank_employee.Bank_ID = bank.Bank_ID where bank.Branch_ID=" + transitionbranchID + " and bank_employee.Employee_ID=" + UserControl.EmpID + " and Bank_Type='خزينة'";
            }
            else if (UserControl.userType == 7)
            {
                query = "select * from bank where Bank_Type='خزينة مصروفات' and bank.Branch_ID=" + transitionbranchID;
            }
            else if(UserControl.userType == 1)
            {
                query = "select * from bank where Bank_Type='خزينة' or Bank_Type='خزينة مصروفات'";
            }
            da = new MySqlDataAdapter(query, dbconnection);
            dt = new DataTable();
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
            Print_Expense_Report f = new Print_Expense_Report();
            f.PrintInvoice(ExpenseTransitionID, comMain.Text, comSub.Text, comBank.Text, txtPullMoney.Text, txtClient.Text, txtDescrip.Text);
            f.ShowDialog();
        }
    }
}

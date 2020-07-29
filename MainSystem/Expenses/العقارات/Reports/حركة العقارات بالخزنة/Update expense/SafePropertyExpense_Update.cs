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
    public partial class SafePropertyExpense_Update : Form
    {
        MySqlConnection dbconnection;
        bool loaded = false;
        bool loaded2 = false;
        XtraTabControl tabControlProperty;
        int transitionbranchID = 0;
        DataRowView selRow = null;
        XtraTabControl tabControlExpense;

        public SafePropertyExpense_Update(DataRowView Selrow, XtraTabControl MainTabControlProperty, MainForm mainform)
        {
            InitializeComponent();
            dbconnection = new MySqlConnection(connection.connectionString);
            tabControlProperty = MainTabControlProperty;
            selRow = Selrow;
            tabControlExpense = MainTabControlProperty;
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
                    loaded2 = false;
                    string query = "select * from property_sub where MainProperty_ID=" + comMain.SelectedValue.ToString();
                    MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    comSub.DataSource = dt;
                    comSub.DisplayMember = dt.Columns["SubProperty_Name"].ToString();
                    comSub.ValueMember = dt.Columns["SubProperty_ID"].ToString();
                    comSub.SelectedIndex = -1;

                    comDetails.DataSource = null;
                    loaded2 = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void comSub_SelectedValueChanged(object sender, EventArgs e)
        {
            if (loaded && loaded2)
            {
                try
                {
                    string query = "select * from property_details where SubProperty_ID=" + comSub.SelectedValue.ToString();
                    MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    comDetails.DataSource = dt;
                    comDetails.DisplayMember = dt.Columns["DetailsProperty_Name"].ToString();
                    comDetails.ValueMember = dt.Columns["DetailsProperty_ID"].ToString();
                    comDetails.SelectedIndex = -1;
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
                if (comMain.Text != "" && comSub.Text != "" && comDetails.Text != "" && comBank.Text != "" && txtPullMoney.Text != "")
                {
                    double money;
                    if (double.TryParse(txtPullMoney.Text, out money))
                    {
                        dbconnection.Open();
                        MySqlCommand com2 = new MySqlCommand("select Bank_Stock from bank where Bank_ID=" + comBank.SelectedValue, dbconnection);
                        double Bank_Stock = Convert.ToDouble(com2.ExecuteScalar().ToString());
                        Bank_Stock += Convert.ToDouble(selRow["مصروف"].ToString());

                        if (money > Bank_Stock)
                        {
                            MessageBox.Show("لا يوجد ما يكفى");
                            dbconnection.Close();
                            return;
                        }

                        string query = "update property_transition set Depositor_Name=@Depositor_Name,Date=@Date,Amount=@Amount,Description=@Description where PropertyTransition_ID=" + selRow["التسلسل"].ToString();
                        MySqlCommand com = new MySqlCommand(query, dbconnection);

                        com.Parameters.Add("@Date", MySqlDbType.DateTime, 0).Value = dateTimePicker1.Value.Date;
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
                        com.Parameters.Add("@UserControl_TableName", MySqlDbType.VarChar, 255).Value = "property_transition";
                        com.Parameters.Add("@UserControl_Status", MySqlDbType.VarChar, 255).Value = "تعديل";
                        com.Parameters.Add("@UserControl_RecordID", MySqlDbType.VarChar, 255).Value = selRow["التسلسل"].ToString();
                        com.Parameters.Add("@UserControl_Date", MySqlDbType.DateTime, 0).Value = DateTime.Now;
                        com.Parameters.Add("@UserControl_Reason", MySqlDbType.VarChar, 255).Value = null;
                        com.ExecuteNonQuery();

                        XtraTabPage xtraTabPage = getTabPage("تعديل مصروف عقار");
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

        //function
        public XtraTabPage getTabPage(string text)
        {
            for (int i = 0; i < tabControlExpense.TabPages.Count; i++)
                if (tabControlExpense.TabPages[i].Text == text)
                {
                    return tabControlExpense.TabPages[i];
                }
            return null;
        }

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
            string query = "select * from property_main";
            MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
            DataTable dt = new DataTable();
            da.Fill(dt);
            comMain.DataSource = dt;
            comMain.DisplayMember = dt.Columns["MainProperty_Name"].ToString();
            comMain.ValueMember = dt.Columns["MainProperty_ID"].ToString();
            comMain.SelectedIndex = -1;

            loaded = true;
            comMain.Text = selRow["العقار"].ToString();
            comSub.Text = selRow["المصروف الرئيسى"].ToString();
            comDetails.Text = selRow["المصروف الفرعى"].ToString();

            if (UserControl.userType == 27)
            {
                query = "select * from bank INNER JOIN bank_employee ON bank_employee.Bank_ID = bank.Bank_ID where bank.Branch_ID=" + transitionbranchID + " and bank_employee.Employee_ID=" + UserControl.EmpID + "";
            }
            else
            {
                query = "select * from bank inner join bank_main on bank.MainBank_ID=bank_main.MainBank_ID where MainBank_Type='خزينة' and MainBank_Name='خزينة عقارات'";
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
            dateTimePicker1.Value = Convert.ToDateTime(selRow["التاريخ"].ToString());

            dbconnection.Close();
        }

        void printProperty(int ExpenseTransitionID)
        {
            //Print_Expense_Report f = new Print_Expense_Report();
            //f.PrintInvoice(ExpenseTransitionID, comMain.Text, comSub.Text, comBank.Text, txtPullMoney.Text, txtClient.Text, txtDescrip.Text);
            //f.ShowDialog();
        }
    }
}

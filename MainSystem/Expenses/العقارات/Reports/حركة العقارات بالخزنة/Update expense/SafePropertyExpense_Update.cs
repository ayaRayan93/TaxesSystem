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
    public partial class SafePropertyExpense_Update : Form
    {
        MySqlConnection dbconnection;
        bool loaded = false;
        bool loaded2 = false;
        XtraTabControl tabControlProperty;
        int transitionbranchID = 0;
        DataRowView selRow = null;

        public SafePropertyExpense_Update(DataRowView selrow, Property_Transitions_Report PropertyTransitionsReport, XtraTabControl MainTabControlProperty, MainForm mainform)
        {
            InitializeComponent();
            dbconnection = new MySqlConnection(connection.connectionString);
            tabControlProperty = MainTabControlProperty;

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
                    double outParse;
                    if (double.TryParse(txtPullMoney.Text, out outParse))
                    {
                        dbconnection.Open();
                        MySqlCommand com2 = new MySqlCommand("select Bank_Stock from bank where Bank_ID=" + comBank.SelectedValue, dbconnection);
                        double amount2 = Convert.ToDouble(com2.ExecuteScalar().ToString());

                        if ((amount2 - outParse) >= 0)
                        {
                            string query = "insert into property_transition (Branch_ID,Depositor_Name,Bank_ID,Date,Amount,Description,DetailsProperty_ID,Type,Employee_ID) values(@Branch_ID,@Depositor_Name,@Bank_ID,@Date,@Amount,@Description,@DetailsProperty_ID,@Type,@Employee_ID)";
                            MySqlCommand com = new MySqlCommand(query, dbconnection);

                            com.Parameters.Add("@Type", MySqlDbType.VarChar, 255).Value = "صرف";
                            com.Parameters.Add("@Branch_ID", MySqlDbType.Int16, 11).Value = transitionbranchID;
                            com.Parameters.Add("@DetailsProperty_ID", MySqlDbType.Int16, 11).Value = comDetails.SelectedValue.ToString();
                            com.Parameters.Add("@Bank_ID", MySqlDbType.Int16, 11).Value = comBank.SelectedValue;
                            com.Parameters.Add("@Date", MySqlDbType.DateTime, 0).Value = dateTimePicker1.Value.Date;
                            com.Parameters.Add("@Depositor_Name", MySqlDbType.VarChar, 255).Value = txtClient.Text;
                            com.Parameters.Add("@Description", MySqlDbType.VarChar, 255).Value = txtDescrip.Text;
                            com.Parameters.Add("@Employee_ID", MySqlDbType.Int16).Value = UserControl.EmpID;
                            com.Parameters.Add("@Amount", MySqlDbType.Decimal, 10).Value = txtPullMoney.Text;
                            com.ExecuteNonQuery();

                            amount2 -= outParse;
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
            string query = "select * from property_main";
            MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
            DataTable dt = new DataTable();
            da.Fill(dt);
            comMain.DataSource = dt;
            comMain.DisplayMember = dt.Columns["MainProperty_Name"].ToString();
            comMain.ValueMember = dt.Columns["MainProperty_ID"].ToString();
            comMain.SelectedIndex = -1;

            comMain.Text = selRow["المصروف الرئيسى"].ToString();
            comSub.Text = selRow["المصروف الفرعى"].ToString();

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

            loaded = true;
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

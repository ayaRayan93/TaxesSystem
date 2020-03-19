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
    public partial class BankTransfers_Update2 : Form
    {
        MySqlConnection dbconnection;
        bool loaded = true;
        XtraTabPage xtraTabPage;
        int[] arrOFPhaatPlus;
        int[] arrOFPhaatMinus;
        int[] arrPaidMoneyPlus;
        bool flag = false;
        bool flagCategoriesSuccess = false;
        bool fromMainLoad = false;
        bool fromSafeLoad = false;
        bool toMainLoad = false;

        public BankTransfers_Update2()
        {
            InitializeComponent();
            dbconnection = new MySqlConnection(connection.connectionString);
            arrOFPhaatPlus = new int[9];
            arrOFPhaatMinus = new int[9];
            arrPaidMoneyPlus = new int[9];
        }

        private void BankTransfers_Record_Load(object sender, EventArgs e)
        {
            if (UserControl.userType == 3)
            {
                radFromSafe.Enabled = false;
                radFromBank.Enabled = false;
                radToBank.Enabled = false;
                radToSafe.Enabled = false;
                comFromMain.Enabled = false;
                comFromBank.Enabled = false;
                comToMain.Enabled = false;
                comToBank.Enabled = false;
                radFromSafe.Checked = true;
                radToSafe.Checked = true;
            }
        }

        private void radFromSafe_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                fromMainLoad = false;

                if (UserControl.userType == 3)
                {
                    string query = "select * from bank_main where MainBank_Type='خزينة' and MainBank_Name='خزينة مبيعات'";
                    MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    comFromMain.DataSource = dt;
                    comFromMain.DisplayMember = dt.Columns["MainBank_Name"].ToString();
                    comFromMain.ValueMember = dt.Columns["MainBank_ID"].ToString();
                    fromMainLoad = true;
                    comFromMain.SelectedIndex = -1;
                    comFromMain.SelectedIndex = 0;
                }
                else
                {
                    string query = "select * from bank_main where MainBank_Type='خزينة'";
                    MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    comFromMain.DataSource = dt;
                    comFromMain.DisplayMember = dt.Columns["MainBank_Name"].ToString();
                    comFromMain.ValueMember = dt.Columns["MainBank_ID"].ToString();
                    fromMainLoad = true;
                    comFromMain.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }

        private void radFromBank_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                fromMainLoad = false;
                string query = "select * from bank_main where MainBank_Type='حساب بنكى'";
                MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                DataTable dt = new DataTable();
                da.Fill(dt);
                comFromMain.DataSource = dt;
                comFromMain.DisplayMember = dt.Columns["MainBank_Name"].ToString();
                comFromMain.ValueMember = dt.Columns["MainBank_ID"].ToString();
                fromMainLoad = true;
                comFromMain.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }

        private void radToSafe_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                toMainLoad = false;
                if (UserControl.userType == 3)
                {
                    string query = "select * from bank_main where MainBank_Type='خزينة' and MainBank_Name='خزينة رئيسية'";
                    MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    comToMain.DataSource = dt;
                    comToMain.DisplayMember = dt.Columns["MainBank_Name"].ToString();
                    comToMain.ValueMember = dt.Columns["MainBank_ID"].ToString();
                    comToMain.SelectedIndex = -1;
                    toMainLoad = true;
                    comToMain.SelectedIndex = 0;
                }
                else
                {
                    string query = "select * from bank_main where MainBank_Type='خزينة'";
                    MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    comToMain.DataSource = dt;
                    comToMain.DisplayMember = dt.Columns["MainBank_Name"].ToString();
                    comToMain.ValueMember = dt.Columns["MainBank_ID"].ToString();
                    toMainLoad = true;
                    comToMain.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }

        private void radToBank_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                toMainLoad = false;
                string query = "select * from bank_main where MainBank_Type='حساب بنكى'";
                MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                DataTable dt = new DataTable();
                da.Fill(dt);
                comToMain.DataSource = dt;
                comToMain.DisplayMember = dt.Columns["MainBank_Name"].ToString();
                comToMain.ValueMember = dt.Columns["MainBank_ID"].ToString();
                toMainLoad = true;
                comToMain.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }

        private void comFromMain_SelectedValueChanged(object sender, EventArgs e)
        {
            if (fromMainLoad)
            {
                try
                {
                    fromSafeLoad = false;
                    if (UserControl.userType == 3)
                    {
                        string query = "select * from bank inner join bank_employee on bank.Bank_ID=bank_employee.Bank_ID where bank_employee.Employee_ID=" + UserControl.EmpID;
                        MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        comFromBank.DataSource = dt;
                        comFromBank.DisplayMember = dt.Columns["Bank_Name"].ToString();
                        comFromBank.ValueMember = dt.Columns["Bank_ID"].ToString();
                        comFromBank.SelectedIndex = -1;
                        fromSafeLoad = true;
                        comFromBank.SelectedIndex = 0;
                    }
                    else
                    {
                        if (comFromMain.SelectedValue != null)
                        {
                            string query = "select * from bank where MainBank_ID=" + comFromMain.SelectedValue.ToString();
                            MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                            DataTable dt = new DataTable();
                            da.Fill(dt);
                            comFromBank.DataSource = dt;
                            comFromBank.DisplayMember = dt.Columns["Bank_Name"].ToString();
                            comFromBank.ValueMember = dt.Columns["Bank_ID"].ToString();
                            fromSafeLoad = true;
                            comFromBank.SelectedIndex = -1;
                        }
                        else
                        {
                            fromSafeLoad = true;
                            comFromBank.DataSource = null;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                dbconnection.Close();
            }
        }

        private void comToMain_SelectedValueChanged(object sender, EventArgs e)
        {
            if (toMainLoad)
            {
                try
                {
                    if (UserControl.userType == 3)
                    {
                        string query = "select * from bank where MainBank_ID=" + comToMain.SelectedValue.ToString() + " and Bank_Name='خزينة د/سامى'";
                        MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        comToBank.DataSource = dt;
                        comToBank.DisplayMember = dt.Columns["Bank_Name"].ToString();
                        comToBank.ValueMember = dt.Columns["Bank_ID"].ToString();
                    }
                    else
                    {
                        if (comToMain.SelectedValue != null)
                        {
                            string query = "select * from bank where MainBank_ID=" + comToMain.SelectedValue.ToString();
                            MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                            DataTable dt = new DataTable();
                            da.Fill(dt);
                            comToBank.DataSource = dt;
                            comToBank.DisplayMember = dt.Columns["Bank_Name"].ToString();
                            comToBank.ValueMember = dt.Columns["Bank_ID"].ToString();
                            comToBank.SelectedIndex = -1;
                        }
                        else
                        {
                            comToBank.DataSource = null;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                dbconnection.Close();
            }
        }

        private void comFromBank_SelectedValueChanged(object sender, EventArgs e)
        {
            if (fromSafeLoad)
            {
                try
                {
                    if (comFromBank.SelectedValue != null)
                    {
                        dbconnection.Open();
                        string query = "select Bank_Stock from bank where Bank_ID=" + comFromBank.SelectedValue;
                        MySqlCommand comand = new MySqlCommand(query, dbconnection);
                        txtMoney.Text = comand.ExecuteScalar().ToString();
                    }
                    else
                    {
                        txtMoney.Text = "";
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                dbconnection.Close();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (comFromBank.SelectedIndex != -1 && comToBank.SelectedIndex != -1 && txtMoney.Text != "")
                {
                    if ((comFromBank.Text) != (comToBank.Text))
                    {
                        dbconnection.Open();
                        string query = "select Bank_Stock from bank where Bank_ID=" + comFromBank.SelectedValue;
                        MySqlCommand comand = new MySqlCommand(query, dbconnection);
                        double FromBank_Stock = Convert.ToDouble(comand.ExecuteScalar().ToString());

                        query = "select Bank_Stock from bank where Bank_ID=" + comToBank.SelectedValue;
                        comand = new MySqlCommand(query, dbconnection);
                        double ToBank_Stock = Convert.ToDouble(comand.ExecuteScalar().ToString());

                        double money = 0;
                        if (double.TryParse(txtMoney.Text, out money))
                        {
                            if (money > FromBank_Stock)
                            {
                                MessageBox.Show("لا يوجد ما يكفى");
                                dbconnection.Close();
                                return;
                            }
                        }
                        else
                        {
                            MessageBox.Show("المبلغ المدفوع يجب ان يكون عدد");
                            dbconnection.Close();
                            return;
                        }

                        if (!flagCategoriesSuccess)
                        {
                            if (MessageBox.Show("لم يتم ادخال الفئات..هل تريد الاستمرار؟", "تنبية", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                            {
                                return;
                            }
                        }

                        int FromBranchId = 0, ToBranchId = 0;
                        string FromBranchName = "", ToBranchName = "";
                        if (radFromSafe.Checked)
                        {
                            string q = "SELECT Branch_ID FROM bank where Bank_ID=" + comFromBank.SelectedValue;
                            MySqlCommand command = new MySqlCommand(q, dbconnection);
                            FromBranchId = Convert.ToInt16(command.ExecuteScalar().ToString());

                            q = "SELECT Branch_Name FROM bank where Bank_ID=" + comFromBank.SelectedValue;
                            command = new MySqlCommand(q, dbconnection);
                            FromBranchName = command.ExecuteScalar().ToString();
                        }

                        if (radToSafe.Checked)
                        {
                            string q = "SELECT Branch_ID FROM bank where Bank_ID=" + comToBank.SelectedValue;
                            MySqlCommand command = new MySqlCommand(q, dbconnection);
                            ToBranchId = Convert.ToInt16(command.ExecuteScalar().ToString());

                            q = "SELECT Branch_Name FROM bank where Bank_ID=" + comToBank.SelectedValue;
                            command = new MySqlCommand(q, dbconnection);
                            ToBranchName = command.ExecuteScalar().ToString();
                        }

                        string q2 = "UPDATE bank SET Bank_Stock = " + (FromBank_Stock - money) + " where Bank_ID=" + comFromBank.SelectedValue;
                        MySqlCommand command2 = new MySqlCommand(q2, dbconnection);
                        command2.ExecuteNonQuery();

                        q2 = "UPDATE bank SET Bank_Stock = " + (ToBank_Stock + money) + " where Bank_ID=" + comToBank.SelectedValue;
                        command2 = new MySqlCommand(q2, dbconnection);
                        command2.ExecuteNonQuery();

                        MySqlCommand com = dbconnection.CreateCommand();
                        com.CommandText = "INSERT INTO bank_Transfer (FromBranch_ID,FromBranch_Name,FromBank_ID,FromBank_Name,ToBranch_ID,ToBranch_Name,ToBank_ID,ToBank_Name,Money,Date,Description,Error) VALUES (@FromBranch_ID,@FromBranch_Name,@FromBank_ID,@FromBank_Name,@ToBranch_ID,@ToBranch_Name,@ToBank_ID,@ToBank_Name,@Money,@Date,@Description,@Error)";
                        if (radFromSafe.Checked)
                        {
                            com.Parameters.Add("@FromBranch_ID", MySqlDbType.Int16, 11).Value = FromBranchId;
                            com.Parameters.Add("@FromBranch_Name", MySqlDbType.VarChar, 255).Value = FromBranchName;
                        }
                        else
                        {
                            com.Parameters.Add("@FromBranch_ID", MySqlDbType.Int16, 11).Value = null;
                            com.Parameters.Add("@FromBranch_Name", MySqlDbType.VarChar, 255).Value = null;
                        }
                        com.Parameters.Add("@FromBank_ID", MySqlDbType.Int16, 11).Value = comFromBank.SelectedValue;
                        com.Parameters.Add("@FromBank_Name", MySqlDbType.VarChar, 255).Value = comFromBank.Text;
                        if (radToSafe.Checked)
                        {
                            com.Parameters.Add("@ToBranch_ID", MySqlDbType.Int16, 11).Value = ToBranchId;
                            com.Parameters.Add("@ToBranch_Name", MySqlDbType.VarChar, 255).Value = ToBranchName;
                        }
                        else
                        {
                            com.Parameters.Add("@ToBranch_ID", MySqlDbType.Int16, 11).Value = null;
                            com.Parameters.Add("@ToBranch_Name", MySqlDbType.VarChar, 255).Value = null;
                        }
                        com.Parameters.Add("@ToBank_ID", MySqlDbType.Int16, 11).Value = comToBank.SelectedValue;
                        com.Parameters.Add("@ToBank_Name", MySqlDbType.VarChar, 255).Value = comToBank.Text;
                        com.Parameters.Add("@Money", MySqlDbType.Decimal, 10).Value = money;
                        com.Parameters.Add("@Date", MySqlDbType.DateTime, 0).Value = DateTime.Now;
                        com.Parameters.Add("@Description", MySqlDbType.VarChar, 255).Value = txtDescription.Text;
                        com.Parameters.Add("@Error", MySqlDbType.Int16, 11).Value = 0;
                        com.ExecuteNonQuery();

                        //////////record adding/////////////
                        query = "select BankTransfer_ID from bank_Transfer order by BankTransfer_ID desc limit 1";
                        com = new MySqlCommand(query, dbconnection);
                        string bankTransferID = com.ExecuteScalar().ToString();

                        query = "insert into usercontrol (UserControl_UserID,UserControl_TableName,UserControl_Status,UserControl_RecordID,UserControl_Date,UserControl_Reason) values(@UserControl_UserID,@UserControl_TableName,@UserControl_Status,@UserControl_RecordID,@UserControl_Date,@UserControl_Reason)";
                        com = new MySqlCommand(query, dbconnection);
                        com.Parameters.Add("@UserControl_UserID", MySqlDbType.Int16, 11).Value = UserControl.userID;
                        com.Parameters.Add("@UserControl_TableName", MySqlDbType.VarChar, 255).Value = "bank_Transfer";
                        com.Parameters.Add("@UserControl_Status", MySqlDbType.VarChar, 255).Value = "اضافة";
                        com.Parameters.Add("@UserControl_RecordID", MySqlDbType.VarChar, 255).Value = bankTransferID;
                        com.Parameters.Add("@UserControl_Date", MySqlDbType.DateTime, 0).Value = DateTime.Now;
                        com.Parameters.Add("@UserControl_Reason", MySqlDbType.VarChar, 255).Value = null;
                        com.ExecuteNonQuery();
                        //////////////////////

                        query = "insert into transfer_categories_money (a200,a100,a50,a20,a10,a5,a1,aH,aQ,BankTransfer_ID) values(@a200,@a100,@a50,@a20,@a10,@a5,@a1,@aH,@aQ,@BankTransfer_ID)";
                        com = new MySqlCommand(query, dbconnection);
                        com.Parameters.Add("@a200", MySqlDbType.Int16, 11).Value = arrPaidMoneyPlus[0];
                        com.Parameters.Add("@a100", MySqlDbType.Int16, 11).Value = arrPaidMoneyPlus[1];
                        com.Parameters.Add("@a50", MySqlDbType.Int16, 11).Value = arrPaidMoneyPlus[2];
                        com.Parameters.Add("@a20", MySqlDbType.Int16, 11).Value = arrPaidMoneyPlus[3];
                        com.Parameters.Add("@a10", MySqlDbType.Int16, 11).Value = arrPaidMoneyPlus[4];
                        com.Parameters.Add("@a5", MySqlDbType.Int16, 11).Value = arrPaidMoneyPlus[5];
                        com.Parameters.Add("@a1", MySqlDbType.Int16, 11).Value = arrPaidMoneyPlus[6];
                        com.Parameters.Add("@aH", MySqlDbType.Int16, 11).Value = arrPaidMoneyPlus[7];
                        com.Parameters.Add("@aQ", MySqlDbType.Int16, 11).Value = arrPaidMoneyPlus[8];
                        com.Parameters.Add("@BankTransfer_ID", MySqlDbType.Int16, 11).Value = Convert.ToInt32(bankTransferID);
                        com.ExecuteNonQuery();

                        dbconnection.Close();
                        flagCategoriesSuccess = false;

                        clear();
                        t200.Text = "";
                        t100.Text = "";
                        t50.Text = "";
                        t20.Text = "";
                        t10.Text = "";
                        t5.Text = "";
                        t1.Text = "";
                        tH.Text = "";
                        tQ.Text = "";
                        PaidMoney.Text = "0";

                        for (int i = 0; i < arrPaidMoneyPlus.Length; i++)
                            arrPaidMoneyPlus[i] = 0;
                        for (int i = 0; i < arrOFPhaatPlus.Length; i++)
                            arrOFPhaatPlus[i] = 0;
                        for (int i = 0; i < arrOFPhaatMinus.Length; i++)
                            arrOFPhaatMinus[i] = 0;
                        xtraTabPage.ImageOptions.Image = null;
                    }
                    else
                    {
                        MessageBox.Show("لا يمكنك التحويل الى نفس المصدر");
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

        private void PaidMoney_KeyDown(object sender, KeyEventArgs e)
        {
            double totalPaid = 0;

            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    double total = 0;
                    if (txtMoney.Text != "" && double.TryParse(txtMoney.Text, out total))
                    {
                        if (comFromBank.Text == "" && comToBank.Text == "")
                        {
                            MessageBox.Show("يجب ان تختار الخزينة");
                            return;
                        }
                        dbconnection.Open();

                        if (!flag)
                        {
                            string query2 = "select * from categories_money where Bank_ID=" + comToBank.SelectedValue;
                            MySqlCommand com2 = new MySqlCommand(query2, dbconnection);
                            MySqlDataReader dr = com2.ExecuteReader();
                            while (dr.Read())
                            {
                                arrOFPhaatPlus[0] = Convert.ToInt32(dr["a200"]);
                                arrOFPhaatPlus[1] = Convert.ToInt32(dr["a100"]);
                                arrOFPhaatPlus[2] = Convert.ToInt32(dr["a50"]);
                                arrOFPhaatPlus[3] = Convert.ToInt32(dr["a20"]);
                                arrOFPhaatPlus[4] = Convert.ToInt32(dr["a10"]);
                                arrOFPhaatPlus[5] = Convert.ToInt32(dr["a5"]);
                                arrOFPhaatPlus[6] = Convert.ToInt32(dr["a1"]);
                                arrOFPhaatPlus[7] = Convert.ToInt32(dr["aH"]);
                                arrOFPhaatPlus[8] = Convert.ToInt32(dr["aQ"]);
                            }
                            dr.Close();

                            query2 = "select * from categories_money where Bank_ID=" + comFromBank.SelectedValue;
                            com2 = new MySqlCommand(query2, dbconnection);
                            MySqlDataReader dr2 = com2.ExecuteReader();
                            while (dr2.Read())
                            {
                                arrOFPhaatMinus[0] = Convert.ToInt32(dr2["a200"]);
                                arrOFPhaatMinus[1] = Convert.ToInt32(dr2["a100"]);
                                arrOFPhaatMinus[2] = Convert.ToInt32(dr2["a50"]);
                                arrOFPhaatMinus[3] = Convert.ToInt32(dr2["a20"]);
                                arrOFPhaatMinus[4] = Convert.ToInt32(dr2["a10"]);
                                arrOFPhaatMinus[5] = Convert.ToInt32(dr2["a5"]);
                                arrOFPhaatMinus[6] = Convert.ToInt32(dr2["a1"]);
                                arrOFPhaatMinus[7] = Convert.ToInt32(dr2["aH"]);
                                arrOFPhaatMinus[8] = Convert.ToInt32(dr2["aQ"]);
                            }
                            dr2.Close();
                            flag = true;
                        }
                        dbconnection.Close();
                        if (PaidMoney.Text != "")
                        {
                            totalPaid = Convert.ToDouble(PaidMoney.Text);
                        }
                        TextBox txt = (TextBox)sender;
                        string txtValue = txt.Name.Split('t')[1];
                        int num;
                        num = 0;
                        switch (txtValue)
                        {
                            case "200":
                                if (int.TryParse(t200.Text, out num))
                                {
                                    arrOFPhaatPlus[0] -= arrPaidMoneyPlus[0];
                                    arrOFPhaatMinus[0] += arrPaidMoneyPlus[0];
                                    arrPaidMoneyPlus[0] = num;
                                    arrOFPhaatPlus[0] += num;
                                    arrOFPhaatMinus[0] -= num;
                                    t100.Focus();
                                }
                                else
                                {
                                    MessageBox.Show("القيمة المدخلة يجب ان تكون عدد");
                                    return;
                                }
                                break;

                            case "100":
                                if (int.TryParse(t100.Text, out num))
                                {
                                    arrOFPhaatPlus[1] -= arrPaidMoneyPlus[1];
                                    arrOFPhaatMinus[1] += arrPaidMoneyPlus[1];
                                    arrPaidMoneyPlus[1] = num;
                                    arrOFPhaatPlus[1] += num;
                                    arrOFPhaatMinus[1] -= num;
                                    t50.Focus();
                                }
                                else
                                {
                                    MessageBox.Show("القيمة المدخلة يجب ان تكون عدد");
                                    return;
                                }
                                break;

                            case "50":
                                if (int.TryParse(t50.Text, out num))
                                {
                                    arrOFPhaatPlus[2] -= arrPaidMoneyPlus[2];
                                    arrOFPhaatMinus[2] += arrPaidMoneyPlus[2];
                                    arrPaidMoneyPlus[2] = num;
                                    arrOFPhaatPlus[2] += num;
                                    arrOFPhaatMinus[2] -= num;
                                    t20.Focus();
                                }
                                else
                                {
                                    MessageBox.Show("القيمة المدخلة يجب ان تكون عدد");
                                    return;
                                }
                                break;

                            case "20":
                                if (int.TryParse(t20.Text, out num))
                                {
                                    arrOFPhaatPlus[3] -= arrPaidMoneyPlus[3];
                                    arrOFPhaatMinus[3] += arrPaidMoneyPlus[3];
                                    arrPaidMoneyPlus[3] = num;
                                    arrOFPhaatPlus[3] += num;
                                    arrOFPhaatMinus[3] -= num;
                                    t10.Focus();
                                }
                                else
                                {
                                    MessageBox.Show("القيمة المدخلة يجب ان تكون عدد");
                                    return;
                                }
                                break;

                            case "10":
                                if (int.TryParse(t10.Text, out num))
                                {
                                    arrOFPhaatPlus[4] -= arrPaidMoneyPlus[4];
                                    arrOFPhaatMinus[4] += arrPaidMoneyPlus[4];
                                    arrPaidMoneyPlus[4] = num;
                                    arrOFPhaatPlus[4] += num;
                                    arrOFPhaatMinus[4] -= num;
                                    t5.Focus();
                                }
                                else
                                {
                                    MessageBox.Show("القيمة المدخلة يجب ان تكون عدد");
                                    return;
                                }
                                break;

                            case "5":
                                if (int.TryParse(t5.Text, out num))
                                {
                                    arrOFPhaatPlus[5] -= arrPaidMoneyPlus[5];
                                    arrOFPhaatMinus[5] += arrPaidMoneyPlus[5];
                                    arrPaidMoneyPlus[5] = num;
                                    arrOFPhaatPlus[5] += num;
                                    arrOFPhaatMinus[5] -= num;
                                    t1.Focus();
                                }
                                else
                                {
                                    MessageBox.Show("القيمة المدخلة يجب ان تكون عدد");
                                    return;
                                }
                                break;

                            case "1":
                                if (int.TryParse(t1.Text, out num))
                                {
                                    arrOFPhaatPlus[6] -= arrPaidMoneyPlus[6];
                                    arrOFPhaatMinus[6] += arrPaidMoneyPlus[6];
                                    arrPaidMoneyPlus[6] = num;
                                    arrOFPhaatPlus[6] += num;
                                    arrOFPhaatMinus[6] -= num;
                                    tH.Focus();
                                }
                                else
                                {
                                    MessageBox.Show("القيمة المدخلة يجب ان تكون عدد");
                                    return;
                                }
                                break;
                            case "H":
                                if (int.TryParse(tH.Text, out num))
                                {
                                    arrOFPhaatPlus[7] -= arrPaidMoneyPlus[7];
                                    arrOFPhaatMinus[7] += arrPaidMoneyPlus[7];
                                    arrPaidMoneyPlus[7] = num;
                                    arrOFPhaatPlus[7] += num;
                                    arrOFPhaatMinus[7] -= num;
                                    tQ.Focus();
                                }
                                else
                                {
                                    MessageBox.Show("القيمة المدخلة يجب ان تكون عدد");
                                    return;
                                }
                                break;
                            case "Q":
                                if (int.TryParse(tQ.Text, out num))
                                {
                                    arrOFPhaatPlus[8] -= arrPaidMoneyPlus[8];
                                    arrOFPhaatMinus[8] += arrPaidMoneyPlus[8];
                                    arrPaidMoneyPlus[8] = num;
                                    arrOFPhaatPlus[8] += num;
                                    arrOFPhaatMinus[8] -= num;
                                }
                                else
                                {
                                    MessageBox.Show("القيمة المدخلة يجب ان تكون عدد");
                                    return;
                                }
                                break;
                        }

                        totalPaid = arrPaidMoneyPlus[0] * 200 + arrPaidMoneyPlus[1] * 100 + arrPaidMoneyPlus[2] * 50 + arrPaidMoneyPlus[3] * 20 + arrPaidMoneyPlus[4] * 10 + arrPaidMoneyPlus[5] * 5 + arrPaidMoneyPlus[6] + arrPaidMoneyPlus[7] * 0.5 + arrPaidMoneyPlus[8] * 0.25;
                        PaidMoney.Text = totalPaid.ToString();

                        if ((total - totalPaid) == 0)
                        {
                            dbconnection.Open();
                            string query = "update categories_money set a200=" + arrOFPhaatPlus[0] + ",a100=" + arrOFPhaatPlus[1] + ",a50=" + arrOFPhaatPlus[2] + ",a20=" + arrOFPhaatPlus[3] + ",a10=" + arrOFPhaatPlus[4] + ",a5=" + arrOFPhaatPlus[5] + ",a1=" + arrOFPhaatPlus[6] + ",aH=" + arrOFPhaatPlus[7] + ",aQ=" + arrOFPhaatPlus[8] + " where Bank_ID=" + comToBank.SelectedValue;
                            MySqlCommand com = new MySqlCommand(query, dbconnection);
                            com.ExecuteNonQuery();

                            query = "update categories_money set a200=" + arrOFPhaatMinus[0] + ",a100=" + arrOFPhaatMinus[1] + ",a50=" + arrOFPhaatMinus[2] + ",a20=" + arrOFPhaatMinus[3] + ",a10=" + arrOFPhaatMinus[4] + ",a5=" + arrOFPhaatMinus[5] + ",a1=" + arrOFPhaatMinus[6] + ",aH=" + arrOFPhaatMinus[7] + ",aQ=" + arrOFPhaatMinus[8] + " where Bank_ID=" + comFromBank.SelectedValue;
                            com = new MySqlCommand(query, dbconnection);
                            com.ExecuteNonQuery();
                            flagCategoriesSuccess = true;
                            MessageBox.Show("تم");

                            PaidMoney.Text = "0";
                            flag = false;
                        }
                        else
                        { }
                        dbconnection.Close();
                    }
                    else
                    {
                        dbconnection.Close();
                        MessageBox.Show("تاكد من المبلغ المدفوع اولا");
                        return;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                dbconnection.Close();
            }
        }

        private void txtBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (loaded)
                {
                    xtraTabPage = getTabPage("tabPageRecordBankTransfer");
                    if (!IsClear())
                    {
                        xtraTabPage.ImageOptions.Image = Properties.Resources.unsave;
                    }
                    else
                    {
                        xtraTabPage.ImageOptions.Image = null;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //clear function
        public void clear()
        {
            comFromBank.SelectedIndex = -1;
            comToBank.SelectedIndex = -1;
            foreach (Control co in this.layoutControl1.Controls)
            {
                if (co is ComboBox)
                {
                    co.Text = "";
                }
                else if (co is TextBox)
                {
                    co.Text = "";

                }
            }
        }

        public XtraTabPage getTabPage(string text)
        {
            for (int i = 0; i < MainForm.tabControlBank.TabPages.Count; i++)
                if (MainForm.tabControlBank.TabPages[i].Name == text)
                {
                    return MainForm.tabControlBank.TabPages[i];
                }
            return null;
        }

        public bool IsClear()
        {
            bool flag5 = false;
            foreach (Control co in this.panel1.Controls)
            {
                foreach (Control item in co.Controls)
                {
                    if (item is ComboBox)
                    {
                        if (item.Text == "")
                            flag5 = true;
                        else
                            return false;
                    }
                    else if (item is TextBox)
                    {
                        if (item.Text == "")
                            flag5 = true;
                        else
                            return false;
                    }
                }
            }

            return flag5;
        }
    }
}

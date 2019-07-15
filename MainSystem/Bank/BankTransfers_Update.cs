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
    public partial class BankTransfers_Update : Form
    {
        MySqlConnection dbconnection;
        bool loaded = false;
        //string User_ID = "";
        XtraTabPage xtraTabPage;
        DataRowView selRow;
        int[] arrOFPhaatPlus;
        int[] arrOFPhaatMinus;
        int[] arrPaidMoneyPlus;
        bool flag = false;
        bool flagCategoriesSuccess = false;

        public BankTransfers_Update(DataRowView SelRow)
        {
            InitializeComponent();
            dbconnection = new MySqlConnection(connection.connectionString);
            selRow = SelRow;
            arrOFPhaatPlus = new int[9];
            arrOFPhaatMinus = new int[9];
            arrPaidMoneyPlus = new int[9];
        }

        private void BankTransfers_Update_Load(object sender, EventArgs e)
        {
            try
            {
                dbconnection.Open();
                string query = "select * from branch";
                MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                DataTable dt = new DataTable();
                da.Fill(dt);
                comFromBranch.DataSource = dt;
                comFromBranch.DisplayMember = dt.Columns["Branch_Name"].ToString();
                comFromBranch.ValueMember = dt.Columns["Branch_ID"].ToString();
                comFromBranch.Text = selRow["من"].ToString().Split(',')[1];
                
                da = new MySqlDataAdapter(query, dbconnection);
                dt = new DataTable();
                da.Fill(dt);
                comToBranch.DataSource = dt;
                comToBranch.DisplayMember = dt.Columns["Branch_Name"].ToString();
                comToBranch.ValueMember = dt.Columns["Branch_ID"].ToString();
                comToBranch.Text = selRow["الى"].ToString().Split(',')[1];

                //SELECT BankTransfer_ID as 'التسلسل',FromBank_ID,CONCAT(FromBank_Name, ',', FromBranch_Name) as 'من',ToBank_ID,CONCAT(ToBank_Name, ',', ToBranch_Name) as 'الى',Money as 'المبلغ',Date as 'تاريخ التحويل',Description as 'البيان',Error
                //comFromBank.Text = selRow["من"].ToString().Split(',')[0];
                //comToBank.Text = selRow["الى"].ToString().Split(',')[0];

                query = "select Bank_Type from bank where Bank_ID=" + selRow["FromBank_ID"].ToString();
                MySqlCommand comand = new MySqlCommand(query, dbconnection);
                string FromBankType = comand.ExecuteScalar().ToString();

                query = "select Bank_Type from bank where Bank_ID=" + selRow["ToBank_ID"].ToString();
                comand = new MySqlCommand(query, dbconnection);
                string ToBankType = comand.ExecuteScalar().ToString();

                if(FromBankType=="خزينة")
                {
                    radFromSafe.Checked = true;
                }
                else if (FromBankType == "حساب بنكى")
                {
                    radFromBank.Checked = true;
                }

                if (ToBankType == "خزينة")
                {
                    radToSafe.Checked = true;
                }
                else if (ToBankType == "حساب بنكى")
                {
                    radToBank.Checked = true;
                }

                comFromBank.SelectedValue = selRow["FromBank_ID"].ToString();
                comToBank.SelectedValue = selRow["ToBank_ID"].ToString();
                txtMoney.Text = selRow["المبلغ"].ToString();
                txtDescription.Text = selRow["البيان"].ToString();

                dbconnection.Open();
                query = "select * from transfer_categories_money where BankTransfer_ID=" + selRow[0].ToString();
                MySqlCommand com = new MySqlCommand(query, dbconnection);
                MySqlDataReader dr = com.ExecuteReader();
                while (dr.Read())
                {
                    t200.Text = dr["a200"].ToString();
                    if (dr["a200"].ToString() != "")
                    {
                        arrPaidMoneyPlus[0] = Convert.ToInt32(dr["a200"].ToString());
                    }
                    t100.Text = dr["a100"].ToString();
                    if (dr["a100"].ToString() != "")
                    {
                        arrPaidMoneyPlus[1] = Convert.ToInt32(dr["a100"].ToString());
                    }
                    t50.Text = dr["a50"].ToString();
                    if (dr["a50"].ToString() != "")
                    {
                        arrPaidMoneyPlus[2] = Convert.ToInt32(dr["a50"].ToString());
                    }
                    t20.Text = dr["a20"].ToString();
                    if (dr["a20"].ToString() != "")
                    {
                        arrPaidMoneyPlus[3] = Convert.ToInt32(dr["a20"].ToString());
                    }
                    t10.Text = dr["a10"].ToString();
                    if (dr["a10"].ToString() != "")
                    {
                        arrPaidMoneyPlus[4] = Convert.ToInt32(dr["a10"].ToString());
                    }
                    t5.Text = dr["a5"].ToString();
                    if (dr["a5"].ToString() != "")
                    {
                        arrPaidMoneyPlus[5] = Convert.ToInt32(dr["a5"].ToString());
                    }
                    t1.Text = dr["a1"].ToString();
                    if (dr["a1"].ToString() != "")
                    {
                        arrPaidMoneyPlus[6] = Convert.ToInt32(dr["a1"].ToString());
                    }
                    tH.Text = dr["aH"].ToString();
                    if (dr["aH"].ToString() != "")
                    {
                        arrPaidMoneyPlus[7] = Convert.ToInt32(dr["aH"].ToString());
                    }
                    tQ.Text = dr["aQ"].ToString();
                    if (dr["aQ"].ToString() != "")
                    {
                        arrPaidMoneyPlus[8] = Convert.ToInt32(dr["aQ"].ToString());
                    }
                }
                dr.Close();
                dbconnection.Close();

                loaded = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }

        private void radFromSafe_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                string query = "select * from bank where Bank_Type='خزينة' and Branch_ID=" + comFromBranch.SelectedValue;
                MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                DataTable dt = new DataTable();
                da.Fill(dt);
                comFromBank.DataSource = dt;
                comFromBank.DisplayMember = dt.Columns["Bank_Name"].ToString();
                comFromBank.ValueMember = dt.Columns["Bank_ID"].ToString();
                comFromBank.SelectedIndex = -1;
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
                string query = "select * from bank where Bank_Type='حساب بنكى' and Branch_ID=" + comFromBranch.SelectedValue;
                MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                DataTable dt = new DataTable();
                da.Fill(dt);
                comFromBank.DataSource = dt;
                comFromBank.DisplayMember = dt.Columns["Bank_Name"].ToString();
                comFromBank.ValueMember = dt.Columns["Bank_ID"].ToString();
                comFromBank.SelectedIndex = -1;
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
                string query = "select * from bank where Bank_Type='خزينة' and Branch_ID=" + comToBranch.SelectedValue;
                MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                DataTable dt = new DataTable();
                da.Fill(dt);
                comToBank.DataSource = dt;
                comToBank.DisplayMember = dt.Columns["Bank_Name"].ToString();
                comToBank.ValueMember = dt.Columns["Bank_ID"].ToString();
                comToBank.SelectedIndex = -1;
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
                string query = "select * from bank where Bank_Type='حساب بنكى' and Branch_ID=" + comToBranch.SelectedValue;
                MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                DataTable dt = new DataTable();
                da.Fill(dt);
                comToBank.DataSource = dt;
                comToBank.DisplayMember = dt.Columns["Bank_Name"].ToString();
                comToBank.ValueMember = dt.Columns["Bank_ID"].ToString();
                comToBank.SelectedIndex = -1;
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
                if (comFromBranch.SelectedIndex != -1 && comToBranch.SelectedIndex != -1 && comFromBank.SelectedIndex != -1 && comToBank.SelectedIndex != -1 && txtMoney.Text != "")
                {
                    dbconnection.Open();
                    string query = "select Bank_Stock from bank where Bank_ID=" + comFromBank.SelectedValue;
                    MySqlCommand comand = new MySqlCommand(query, dbconnection);
                    double FromBank_Stock = Convert.ToDouble(comand.ExecuteScalar().ToString());
                    FromBank_Stock += Convert.ToDouble(selRow["المبلغ"].ToString());

                    query = "select Bank_Stock from bank where Bank_ID=" + comToBank.SelectedValue;
                    comand = new MySqlCommand(query, dbconnection);
                    double ToBank_Stock = Convert.ToDouble(comand.ExecuteScalar().ToString());
                    ToBank_Stock -= Convert.ToDouble(selRow["المبلغ"].ToString());

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
                    
                    Form prompt = new Form()
                    {
                        Width = 500,
                        Height = 220,
                        FormBorderStyle = FormBorderStyle.FixedDialog,
                        Text = "",
                        StartPosition = FormStartPosition.CenterScreen,
                        MaximizeBox = false,
                        MinimizeBox = false
                    };
                    Label textLabel = new Label() { Left = 340, Top = 20, Text = "ما هو سبب التعديل؟" };
                    TextBox textBox = new TextBox() { Left = 50, Top = 50, Width = 385, Multiline = true, Height = 80, RightToLeft = RightToLeft };
                    Button confirmation = new Button() { Text = "تأكيد", Left = 200, Width = 100, Top = 140, DialogResult = DialogResult.OK };
                    prompt.Controls.Add(textBox);
                    prompt.Controls.Add(confirmation);
                    prompt.Controls.Add(textLabel);
                    prompt.AcceptButton = confirmation;
                    if (prompt.ShowDialog() == DialogResult.OK)
                    {
                        if (textBox.Text != "")
                        {
                            if (!flagCategoriesSuccess)
                            {
                                if (MessageBox.Show("لم يتم ادخال الفئات..هل تريد الاستمرار؟", "تنبية", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                                {
                                    return;
                                }
                            }

                            string q = "UPDATE bank SET Bank_Stock = " + (FromBank_Stock - money) + " where Bank_ID=" + comFromBank.SelectedValue;
                            MySqlCommand command = new MySqlCommand(q, dbconnection);
                            command.ExecuteNonQuery();

                            q = "UPDATE bank SET Bank_Stock = " + (ToBank_Stock + money) + " where Bank_ID=" + comToBank.SelectedValue;
                            command = new MySqlCommand(q, dbconnection);
                            command.ExecuteNonQuery();

                            MySqlCommand com = dbconnection.CreateCommand();
                            com.CommandText = "update bank_Transfer set Money=@Money,Description=@Description where BankTransfer_ID=" + selRow["التسلسل"].ToString();
                            com.Parameters.Add("@Money", MySqlDbType.Decimal, 10).Value = money;
                            com.Parameters.Add("@Description", MySqlDbType.VarChar, 255).Value = txtDescription.Text;
                            com.ExecuteNonQuery();

                            //////////record adding/////////////
                            query = "insert into usercontrol (UserControl_UserID,UserControl_TableName,UserControl_Status,UserControl_RecordID,UserControl_Date,UserControl_Reason) values(@UserControl_UserID,@UserControl_TableName,@UserControl_Status,@UserControl_RecordID,@UserControl_Date,@UserControl_Reason)";
                            com = new MySqlCommand(query, dbconnection);
                            com.Parameters.Add("@UserControl_UserID", MySqlDbType.Int16, 11).Value = UserControl.userID;
                            com.Parameters.Add("@UserControl_TableName", MySqlDbType.VarChar, 255).Value = "bank_Transfer";
                            com.Parameters.Add("@UserControl_Status", MySqlDbType.VarChar, 255).Value = "تعديل";
                            com.Parameters.Add("@UserControl_RecordID", MySqlDbType.VarChar, 255).Value = selRow["التسلسل"].ToString();
                            com.Parameters.Add("@UserControl_Date", MySqlDbType.DateTime, 0).Value = DateTime.Now;
                            com.Parameters.Add("@UserControl_Reason", MySqlDbType.VarChar, 255).Value = textBox.Text;
                            com.ExecuteNonQuery();
                            //////////////////////

                            query = "update transfer_categories_money set a200=@a200,a100=@a100,a50=@a50,a20=@a20,a10=@a10,a5=@a5,a1=@a1,aH=@aH,aQ=@aQ where BankTransfer_ID=" + selRow[0].ToString();
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
                            com.ExecuteNonQuery();

                            dbconnection.Close();
                            flagCategoriesSuccess = false;
                            for (int i = 0; i < arrPaidMoneyPlus.Length; i++)
                                arrPaidMoneyPlus[i] = 0;
                            
                            xtraTabPage.ImageOptions.Image = null;
                            MainForm.tabControlBank.TabPages.Remove(BankTransfers_Report.MainTabPageUpdateBankTransfer);
                        }
                        else
                        {
                            MessageBox.Show("يجب كتابة السبب");
                        }
                    }
                    else
                    { }
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
                    xtraTabPage = getTabPage("tabPageUpdateBankTransfer");
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
            comFromBranch.SelectedIndex = -1;
            comToBranch.SelectedIndex = -1;
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

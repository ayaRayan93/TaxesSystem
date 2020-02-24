using DevExpress.XtraTab;
using MySql.Data.MySqlClient;
using System;
using System.Collections;
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
    public partial class Bank_Record : Form
    {
        MySqlConnection dbconnection;
        bool loaded = false;
        XtraTabPage xtraTabPage;
        int[] arrOFPhaatPlus;
        //int[] arrOFPhaatMinus;
        int[] arrPaidMoneyPlus;
        bool flag = false;

        public Bank_Record()
        {
            InitializeComponent();
            dbconnection = new MySqlConnection(connection.connectionString);
            
            arrOFPhaatPlus = new int[9];
            //arrOFPhaatMinus = new int[9];
            arrPaidMoneyPlus = new int[9];

            cmbType.AutoCompleteMode = AutoCompleteMode.Suggest;
            cmbType.AutoCompleteSource = AutoCompleteSource.ListItems;

            cmbBranch.AutoCompleteMode = AutoCompleteMode.Suggest;
            cmbBranch.AutoCompleteSource = AutoCompleteSource.ListItems;

            cmbBank.AutoCompleteMode = AutoCompleteMode.Suggest;
            cmbBank.AutoCompleteSource = AutoCompleteSource.ListItems;
        }

        private void Bank_Record_Load(object sender, EventArgs e)
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

        private void PaidMoney_KeyDown(object sender, KeyEventArgs e)
        {
            double totalPaid = 0;

            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    double total = 0;
                    dbconnection.Open();

                    if (!flag)
                    {
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
                                //arrOFPhaatMinus[0] += arrPaidMoneyPlus[0];
                                arrPaidMoneyPlus[0] = num;
                                arrOFPhaatPlus[0] += num;
                                //arrOFPhaatMinus[0] -= num;
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
                                //arrOFPhaatMinus[1] += arrPaidMoneyPlus[1];
                                arrPaidMoneyPlus[1] = num;
                                arrOFPhaatPlus[1] += num;
                                //arrOFPhaatMinus[1] -= num;
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
                                //arrOFPhaatMinus[2] += arrPaidMoneyPlus[2];
                                arrPaidMoneyPlus[2] = num;
                                arrOFPhaatPlus[2] += num;
                                //arrOFPhaatMinus[2] -= num;
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
                                //arrOFPhaatMinus[3] += arrPaidMoneyPlus[3];
                                arrPaidMoneyPlus[3] = num;
                                arrOFPhaatPlus[3] += num;
                                //arrOFPhaatMinus[3] -= num;
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
                                //arrOFPhaatMinus[4] += arrPaidMoneyPlus[4];
                                arrPaidMoneyPlus[4] = num;
                                arrOFPhaatPlus[4] += num;
                                //arrOFPhaatMinus[4] -= num;
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
                                //arrOFPhaatMinus[5] += arrPaidMoneyPlus[5];
                                arrPaidMoneyPlus[5] = num;
                                arrOFPhaatPlus[5] += num;
                                //arrOFPhaatMinus[5] -= num;
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
                                //arrOFPhaatMinus[6] += arrPaidMoneyPlus[6];
                                arrPaidMoneyPlus[6] = num;
                                arrOFPhaatPlus[6] += num;
                                //arrOFPhaatMinus[6] -= num;
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
                                //arrOFPhaatMinus[7] += arrPaidMoneyPlus[7];
                                arrPaidMoneyPlus[7] = num;
                                arrOFPhaatPlus[7] += num;
                                //arrOFPhaatMinus[7] -= num;
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
                                //arrOFPhaatMinus[8] += arrPaidMoneyPlus[8];
                                arrPaidMoneyPlus[8] = num;
                                arrOFPhaatPlus[8] += num;
                                //arrOFPhaatMinus[8] -= num;
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
                        PaidMoney.Text = "0";
                        flag = false;
                    }
                    else
                    { }
                    dbconnection.Close();
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
                if (cmbType.Text != "")
                {
                    bool check = false;
                    if (cmbType.Text == "خزينة")
                    {
                        check = (txtName.Text != "" && cmbBranch.Text != "" && txtStock.Text != "" && dateEdit1.Text != "");
                    }
                    else if (cmbType.Text == "حساب بنكى")
                    {
                        check = (txtName.Text != "" && txtAccountNumber.Text != "" && txtAccountType.Text != "" && txtStock.Text != "" && dateEdit1.Text != "");
                    }
                    else if (cmbType.Text == "فيزا")
                    {
                        check = (txtName.Text != "" && cmbBranch.Text != "" && txtAccountNumber.Text != "" && txtAccountType.Text != "" && txtStock.Text != "" && txtMachineID.Text != "" && dateEdit1.Text != "");
                    }
                    else if (cmbType.Text == "خزينة مصروفات")
                    {
                        check = (txtName.Text != "" && cmbBranch.Text != "" && txtStock.Text != "" && dateEdit1.Text != "");
                    }

                    if (check)
                    {
                        string query = "";
                        if (cmbType.Text == "خزينة")
                        {
                            query = "select Bank_Name from bank where Branch_ID=" + cmbBranch.SelectedValue.ToString() + " and Bank_Name='" + txtName.Text + "'";
                        }
                        else if (cmbType.Text == "حساب بنكى")
                        {
                            query = "select Bank_Account from bank where Bank_Account='" + txtAccountNumber.Text + "' and Bank_Name='" + txtName.Text + "'";
                        }
                        else if (cmbType.Text == "فيزا")
                        {
                            query = "SELECT Machine_ID FROM bank where Branch_ID=" + cmbBranch.SelectedValue.ToString() + " and Machine_ID='" + txtMachineID.Text + "' and Bank_Name='" + txtName.Text + "' and BankVisa_ID=" + cmbBank.SelectedValue;
                        }
                        else if (cmbType.Text == "خزينة مصروفات")
                        {
                            query = "select Bank_Name from bank where Branch_ID=" + cmbBranch.SelectedValue.ToString() + " and Bank_Name='" + txtName.Text + "'";
                        }

                        MySqlCommand com = new MySqlCommand(query, dbconnection);
                        dbconnection.Open();

                        if (com.ExecuteScalar() == null)
                        {
                            dbconnection.Close();
                            double stock = 0;
                            if (double.TryParse(txtStock.Text, out stock))
                            {
                                int tt200,tt100,tt50,tt20,tt10,tt5,tt1,ttH,ttQ = 0;
                                if (Int32.TryParse(t200.Text, out tt200) && Int32.TryParse(t100.Text, out tt100) && Int32.TryParse(t50.Text, out tt50) && Int32.TryParse(t20.Text, out tt20) && Int32.TryParse(t10.Text, out tt10) && Int32.TryParse(t5.Text, out tt5) && Int32.TryParse(t1.Text, out tt1) && Int32.TryParse(tH.Text, out ttH) && Int32.TryParse(tQ.Text, out ttQ))
                                {
                                    MySqlCommand command = dbconnection.CreateCommand();
                                    command.CommandText = "INSERT INTO bank (Bank_Type,Bank_Name,Branch_ID,Branch_Name,Bank_Stock,Start_Date,Bank_Info,Bank_Account,BankAccount_Type,BankVisa_ID,BankVisa,Machine_ID) VALUES (?Bank_Type,?Bank_Name,?Branch_ID,?Branch_Name,?Bank_Stock,?Start_Date,?Bank_Info,?Bank_Account,?BankAccount_Type,?BankVisa_ID,?BankVisa,?Machine_ID)";

                                    if (cmbType.Text == "خزينة")
                                    {
                                        command.Parameters.AddWithValue("?Bank_Type", "خزينة");
                                        command.Parameters.AddWithValue("?Bank_Name", txtName.Text);
                                        command.Parameters.AddWithValue("?Branch_ID", cmbBranch.SelectedValue.ToString());
                                        command.Parameters.AddWithValue("?Branch_Name", cmbBranch.Text);
                                        command.Parameters.AddWithValue("?Bank_Account", "");
                                        command.Parameters.AddWithValue("?BankAccount_Type", "");
                                        command.Parameters.AddWithValue("?BankVisa_ID", null);
                                        command.Parameters.AddWithValue("?BankVisa", null);
                                        command.Parameters.AddWithValue("?Machine_ID", null);
                                    }
                                    else if (cmbType.Text == "حساب بنكى")
                                    {
                                        command.Parameters.AddWithValue("?Bank_Type", "حساب بنكى");
                                        command.Parameters.AddWithValue("?Bank_Name", txtName.Text);
                                        command.Parameters.AddWithValue("?Branch_ID", null);
                                        command.Parameters.AddWithValue("?Branch_Name", null);
                                        command.Parameters.AddWithValue("?Bank_Account", txtAccountNumber.Text);
                                        command.Parameters.AddWithValue("?BankAccount_Type", txtAccountType.Text);
                                        command.Parameters.AddWithValue("?BankVisa_ID", null);
                                        command.Parameters.AddWithValue("?BankVisa", null);
                                        command.Parameters.AddWithValue("?Machine_ID", null);
                                    }
                                    else if (cmbType.Text == "فيزا")
                                    {
                                        command.Parameters.AddWithValue("?Bank_Type", "فيزا");
                                        command.Parameters.AddWithValue("?Bank_Name", txtName.Text);
                                        command.Parameters.AddWithValue("?Branch_ID", cmbBranch.SelectedValue.ToString());
                                        command.Parameters.AddWithValue("?Branch_Name", cmbBranch.Text);
                                        command.Parameters.AddWithValue("?Bank_Account", txtAccountNumber.Text);
                                        command.Parameters.AddWithValue("?BankAccount_Type", txtAccountType.Text);
                                        command.Parameters.AddWithValue("?BankVisa_ID", cmbBank.SelectedValue);
                                        command.Parameters.AddWithValue("?BankVisa", cmbBank.Text);
                                        command.Parameters.AddWithValue("?Machine_ID", txtMachineID.Text);
                                    }
                                    if (cmbType.Text == "خزينة مصروفات")
                                    {
                                        command.Parameters.AddWithValue("?Bank_Type", "خزينة مصروفات");
                                        command.Parameters.AddWithValue("?Bank_Name", txtName.Text);
                                        command.Parameters.AddWithValue("?Branch_ID", cmbBranch.SelectedValue.ToString());
                                        command.Parameters.AddWithValue("?Branch_Name", cmbBranch.Text);
                                        command.Parameters.AddWithValue("?Bank_Account", "");
                                        command.Parameters.AddWithValue("?BankAccount_Type", "");
                                        command.Parameters.AddWithValue("?BankVisa_ID", null);
                                        command.Parameters.AddWithValue("?BankVisa", null);
                                        command.Parameters.AddWithValue("?Machine_ID", null);
                                    }

                                    command.Parameters.AddWithValue("?Bank_Stock", stock);
                                    command.Parameters.AddWithValue("?Start_Date", dateEdit1.DateTime.Date);
                                    command.Parameters.AddWithValue("?Bank_Info", txtInformation.Text);
                                    //command.Parameters.AddWithValue("?User_ID", UserControl.userID);
                                    //command.Parameters.AddWithValue("?User_Name", UserControl.userName);
                                    dbconnection.Open();
                                    command.ExecuteNonQuery();

                                    //////////record adding/////////////
                                    query = "select Bank_ID from bank order by Bank_ID desc limit 1";
                                    com = new MySqlCommand(query, dbconnection);
                                    string BankID = com.ExecuteScalar().ToString();

                                    query = "insert into usercontrol (UserControl_UserID,UserControl_TableName,UserControl_Status,UserControl_RecordID,UserControl_Date,UserControl_Reason) values(@UserControl_UserID,@UserControl_TableName,@UserControl_Status,@UserControl_RecordID,@UserControl_Date,@UserControl_Reason)";
                                    com = new MySqlCommand(query, dbconnection);
                                    com.Parameters.Add("@UserControl_UserID", MySqlDbType.Int16, 11).Value = UserControl.userID;
                                    com.Parameters.Add("@UserControl_TableName", MySqlDbType.VarChar, 255).Value = "bank";
                                    com.Parameters.Add("@UserControl_Status", MySqlDbType.VarChar, 255).Value = "اضافة";
                                    com.Parameters.Add("@UserControl_RecordID", MySqlDbType.VarChar, 255).Value = BankID;
                                    com.Parameters.Add("@UserControl_Date", MySqlDbType.DateTime, 0).Value = DateTime.Now;
                                    com.Parameters.Add("@UserControl_Reason", MySqlDbType.VarChar, 255).Value = null;
                                    com.ExecuteNonQuery();
                                    //////////////////////

                                    for (int i = 0; i < checkedListBoxControlUserID.ItemCount; i++)
                                    {
                                        query = "insert into bank_employee (Bank_ID,Employee_ID) values (@Bank_ID,@Employee_ID)";
                                        command = new MySqlCommand(query, dbconnection);
                                        command.Parameters.AddWithValue("@Bank_ID", BankID);
                                        command.Parameters.AddWithValue("@Employee_ID", checkedListBoxControlUserID.Items[i].Value.ToString());
                                        command.ExecuteNonQuery();
                                    }

                                    query = "insert into categories_money (a200,a100,a50,a20,a10,a5,a1,aH,aQ,Bank_ID,Bank_Name) values(@a200,@a100,@a50,@a20,@a10,@a5,@a1,@aH,@aQ,@Bank_ID,@Bank_Name)";
                                    com = new MySqlCommand(query, dbconnection);
                                    com.Parameters.Add("@a200", MySqlDbType.Int16, 11).Value = t200.Text;
                                    com.Parameters.Add("@a100", MySqlDbType.Int16, 11).Value = t100.Text;
                                    com.Parameters.Add("@a50", MySqlDbType.Int16, 11).Value = t50.Text;
                                    com.Parameters.Add("@a20", MySqlDbType.Int16, 11).Value = t20.Text;
                                    com.Parameters.Add("@a10", MySqlDbType.Int16, 11).Value = t10.Text;
                                    com.Parameters.Add("@a5", MySqlDbType.Int16, 11).Value = t5.Text;
                                    com.Parameters.Add("@a1", MySqlDbType.Int16, 11).Value = t1.Text;
                                    com.Parameters.Add("@aH", MySqlDbType.Int16, 11).Value = tH.Text;
                                    com.Parameters.Add("@aQ", MySqlDbType.Int16, 11).Value = tQ.Text;
                                    com.Parameters.Add("@Bank_ID", MySqlDbType.Int16, 11).Value = BankID;
                                    com.Parameters.Add("@Bank_Name", MySqlDbType.VarChar, 255).Value = txtName.Text;
                                    com.ExecuteNonQuery();

                                    dbconnection.Close();
                                    //MessageBox.Show("تمت الاضافة");
                                    cmbType.SelectedIndex = -1;
                                    txtName.Text = "";
                                    cmbBranch.SelectedIndex = -1;
                                    txtStock.Text = "";
                                    dateEdit1.Text = "";
                                    txtInformation.Text = "";
                                    txtAccountNumber.Text = "";
                                    txtAccountType.Text = "";
                                    cmbBank.SelectedIndex = -1;
                                    txtMachineID.Text = "";

                                    layoutControlItemBranch.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                                    labelBranch.Text = "";
                                    layoutControlItemName.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                                    labelName.Text = "";
                                    layoutControlItemStock.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                                    labelStock.Text = "";
                                    layoutControlItemDate.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                                    labelDate.Text = "";
                                    layoutControlItemInformation.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                                    labelInfo.Text = "";
                                    layoutControlItemAccountNumber.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                                    labelAccountNumber.Text = "";
                                    layoutControlItemAccountType.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                                    labelAccountType.Text = "";
                                    layoutControlItemBank.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                                    labelBank.Text = "";
                                    layoutControlItemID.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                                    labelID.Text = "";

                                    comBankUsers.SelectedIndex = -1;
                                    int cont = checkedListBoxControlUser.ItemCount;
                                    for (int i = 0; i < cont; i++)
                                    {
                                        checkedListBoxControlUser.Items.RemoveAt(0);
                                    }
                                    cont = checkedListBoxControlUserID.ItemCount;
                                    for (int i = 0; i < cont; i++)
                                    {
                                        checkedListBoxControlUserID.Items.RemoveAt(0);
                                    }
                                    labelEmp.Visible = false;
                                    comBankUsers.Visible = false;
                                    btnAddUserToBank.Visible = false;
                                    checkedListBoxControlUser.Visible = false;
                                    btnDeleteUser.Visible = false;

                                    t200.Text = "0";
                                    t100.Text = "0";
                                    t50.Text = "0";
                                    t20.Text = "0";
                                    t10.Text = "0";
                                    t5.Text = "0";
                                    t1.Text = "0";
                                    tH.Text = "0";
                                    tQ.Text = "0";
                                    PaidMoney.Text = "0";

                                    for (int i = 0; i < arrPaidMoneyPlus.Length; i++)
                                        arrPaidMoneyPlus[i] = 0;
                                    for (int i = 0; i < arrOFPhaatPlus.Length; i++)
                                        arrOFPhaatPlus[i] = 0;

                                    loadBranch();
                                    xtraTabPage.ImageOptions.Image = null;
                                }
                                else
                                {
                                    MessageBox.Show("يجب ادخال فئات صحيحة");
                                }
                            }
                            else
                            {
                                MessageBox.Show("الرصيد يجب ان يكون رقم");
                            }
                        }
                        else
                        {
                            MessageBox.Show("هذا العنصر تم ادخالة من قبل");
                        }
                    }
                    else
                    {
                        MessageBox.Show("برجاء ادخال جميع البيانات المطلوبة");
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }

        private void cmbType_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cmbType.Text == "خزينة")
            {
                layoutControlItemBranch.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                labelBranch.Text = "*";
                layoutControlItemName.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                labelName.Text = "*";
                layoutControlItemStock.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                labelStock.Text = "*";
                layoutControlItemDate.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                labelDate.Text = "*";
                layoutControlItemInformation.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                labelInfo.Text = "";
                layoutControlItemAccountNumber.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                labelAccountNumber.Text = "";
                layoutControlItemAccountType.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                labelAccountType.Text = "";
                layoutControlItemBank.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                labelBank.Text = "";
                layoutControlItemID.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                labelID.Text = "";
            }
            else if (cmbType.Text == "حساب بنكى")
            {
                layoutControlItemBranch.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                labelBranch.Text = "";
                layoutControlItemName.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                labelName.Text = "*";
                layoutControlItemStock.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                labelStock.Text = "*";
                layoutControlItemDate.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                labelDate.Text = "*";
                layoutControlItemInformation.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                labelInfo.Text = "";
                layoutControlItemAccountNumber.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                labelAccountNumber.Text = "*";
                layoutControlItemAccountType.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                labelAccountType.Text = "*";
                layoutControlItemBank.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                labelBank.Text = "";
                layoutControlItemID.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                labelID.Text = "";
            }
            else if (cmbType.Text == "فيزا")
            {
                layoutControlItemBranch.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                labelBranch.Text = "*";
                layoutControlItemName.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                labelName.Text = "*";
                layoutControlItemStock.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                labelStock.Text = "*";
                layoutControlItemDate.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                labelDate.Text = "*";
                layoutControlItemInformation.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                labelInfo.Text = "";
                layoutControlItemAccountNumber.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                labelAccountNumber.Text = "*";
                layoutControlItemAccountType.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                labelAccountType.Text = "*";
                layoutControlItemBank.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                labelBank.Text = "*";
                layoutControlItemID.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                labelID.Text = "*";
            }
            else if (cmbType.Text == "خزينة مصروفات")
            {
                layoutControlItemBranch.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                labelBranch.Text = "*";
                layoutControlItemName.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                labelName.Text = "*";
                layoutControlItemStock.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                labelStock.Text = "*";
                layoutControlItemDate.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                labelDate.Text = "*";
                layoutControlItemInformation.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                labelInfo.Text = "";
                layoutControlItemAccountNumber.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                labelAccountNumber.Text = "";
                layoutControlItemAccountType.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                labelAccountType.Text = "";
                layoutControlItemBank.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                labelBank.Text = "";
                layoutControlItemID.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                labelID.Text = "";
            }
            else if (cmbType.Text == "خزينة ادارة السيارات")
            {
                layoutControlItemBranch.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                labelBranch.Text = "*";
                layoutControlItemName.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                labelName.Text = "*";
                layoutControlItemStock.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                labelStock.Text = "*";
                layoutControlItemDate.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                labelDate.Text = "*";
                layoutControlItemInformation.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                labelInfo.Text = "";
                layoutControlItemAccountNumber.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                labelAccountNumber.Text = "";
                layoutControlItemAccountType.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                labelAccountType.Text = "";
                layoutControlItemBank.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                labelBank.Text = "";
                layoutControlItemID.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                labelID.Text = "";
            }
            labelEmp.Visible = true;
            comBankUsers.Visible = true;
            btnAddUserToBank.Visible = true;
            checkedListBoxControlUser.Visible = true;
            btnDeleteUser.Visible = true;
        }

        private void txtBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (loaded)
                {
                    xtraTabPage = getTabPage("tabPageAddBank");
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
            foreach (Control co in this.panel1.Controls)
            {
                //if (co is GroupBox)
                //{
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
                //}
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

        //functions
        private void loadBranch()
        {
            dbconnection.Open();
            string query = "select * from branch";
            MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
            DataTable dt = new DataTable();
            da.Fill(dt);
            cmbBranch.DataSource = dt;
            cmbBranch.DisplayMember = dt.Columns["Branch_Name"].ToString();
            cmbBranch.ValueMember = dt.Columns["Branch_ID"].ToString();
            cmbBranch.SelectedIndex = -1;

            query = "select * from bank where Branch_ID is null and BankVisa_ID is null";
            da = new MySqlDataAdapter(query, dbconnection);
            dt = new DataTable();
            da.Fill(dt);
            cmbBank.DataSource = dt;
            cmbBank.DisplayMember = dt.Columns["Bank_Name"].ToString();
            cmbBank.ValueMember = dt.Columns["Bank_ID"].ToString();
            cmbBank.SelectedIndex = -1;

            query = "select Employee_ID,Employee_Name from employee where employee.Employee_ID Not in(" + "select bank_employee.Employee_ID from bank_employee " + ")";
            da = new MySqlDataAdapter(query, dbconnection);
            dt = new DataTable();
            da.Fill(dt);
            comBankUsers.DataSource = dt;
            comBankUsers.DisplayMember = dt.Columns["Employee_Name"].ToString();
            comBankUsers.ValueMember = dt.Columns["Employee_ID"].ToString();
            comBankUsers.SelectedIndex = -1;

            loaded = true;
        }

        private void btnAddUserToBank_Click(object sender, EventArgs e)
        {
            try
            {
                if (comBankUsers.Text != "" && comBankUsers.SelectedIndex != -1 && comBankUsers.SelectedValue != null)
                {
                    dbconnection.Open();
                    
                    for (int i = 0; i < checkedListBoxControlUserID.ItemCount; i++)
                    {
                        if (comBankUsers.SelectedValue.ToString() == checkedListBoxControlUserID.Items[i].Value.ToString())
                        {
                            MessageBox.Show("هذا الموظف تم اضافتة");
                            dbconnection.Close();
                            return;
                        }
                    }

                    checkedListBoxControlUserID.Items.Add(comBankUsers.SelectedValue.ToString());
                    checkedListBoxControlUser.Items.Add(comBankUsers.Text);
                    comBankUsers.Text = "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }

        private void btnDeleteUser_Click(object sender, EventArgs e)
        {
            try
            {
                if (checkedListBoxControlUser.CheckedItemsCount > 0)
                {
                    if (MessageBox.Show("هل انت متاكد انك تريد الحذف؟", "", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                        return;

                    ArrayList temp = new ArrayList();
                    ArrayList tempId = new ArrayList();
                    foreach (int index in checkedListBoxControlUser.CheckedIndices)
                    {
                        temp.Add(checkedListBoxControlUser.Items[index]);
                        tempId.Add(checkedListBoxControlUserID.Items[index]);
                    }
                    foreach (object item in temp)
                    {
                        checkedListBoxControlUser.Items.Remove(item);
                    }
                    foreach (object item in tempId)
                    {
                        checkedListBoxControlUserID.Items.Remove(item);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}

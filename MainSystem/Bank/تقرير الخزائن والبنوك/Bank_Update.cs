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
    public partial class Bank_Update : Form
    {
        MySqlConnection dbconnection, dbconnectionr;
        XtraTabPage xtraTabPage;
        int[] arrOFPhaatPlus;
        int[] arrPaidMoneyPlus;
        bool flag = false;
        int BankID=0;
        

        public Bank_Update(int BankID)
        {
            InitializeComponent();
            dbconnection = new MySqlConnection(connection.connectionString);
            dbconnectionr = new MySqlConnection(connection.connectionString);

            arrOFPhaatPlus = new int[9];
            arrPaidMoneyPlus = new int[9];

            this.BankID = BankID;
        }

        private void Bank_Record_Load(object sender, EventArgs e)
        {
            try
            {
                dbconnection.Open();
                dbconnectionr.Open();
                string query = "select * from bank_main inner join bank on bank.MainBank_ID=bank_main.MainBank_ID left join supplier on supplier.Supplier_ID=bank.Supplier_ID where Bank_ID=" + BankID;
                MySqlCommand com = new MySqlCommand(query, dbconnectionr);
                MySqlDataReader dr = com.ExecuteReader();
                while (dr.Read())
                {
                    cmbType.Text = dr["MainBank_Type"].ToString();
                    cmbMain.Text = dr["MainBank_Name"].ToString();
                   // cmbMain.SelectedValue = dr["MainBank_ID"].ToString();
                    txtName_AccountNum.Text = dr["Bank_Name"].ToString();
                    comBranch.Text= dr["Branch_Name"].ToString();
                    //comBranch.SelectedValue= dr["Branch_Name"].ToString();
                    dateEdit1.Text= dr["Start_Date"].ToString();
                    txtInformation.Text= dr["Bank_Info"].ToString();
                    txtStock.Text= dr["Initial_Balance"].ToString();
                    txtAccountName.Text= dr["SupplierAccountName"].ToString();
                    txtAccountType.Text = dr["BankAccount_Type"].ToString();
                    comSupplier.Text= dr["Supplier_Name"].ToString();
                 // comSupplier.SelectedValue= dr["supplier.Supplier_ID"].ToString();
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
            dbconnectionr.Close();
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
                if (cmbType.Text != "" && cmbMain.Text != "")
                {
                    bool check = false;
                    if (cmbType.Text == "خزينة")
                    {
                        check = (txtName_AccountNum.Text != "" && comBranch.Text != "" && txtStock.Text != "" && dateEdit1.Text != "");
                    }
                    else if (cmbType.Text == "حساب بنكى")
                    {
                        check = (txtName_AccountNum.Text != "" && txtAccountType.Text != "" && txtStock.Text != "" && dateEdit1.Text != "");
                    }

                    if (check)
                    {
                        string query = "";
                       
                        dbconnection.Open();

                        if (BankID!= 0)
                        {
                            double stock = 0;
                            if (double.TryParse(txtStock.Text, out stock))
                            {
                                int tt200, tt100, tt50, tt20, tt10, tt5, tt1, ttH, ttQ = 0;
                                if (Int32.TryParse(t200.Text, out tt200) && Int32.TryParse(t100.Text, out tt100) && Int32.TryParse(t50.Text, out tt50) && Int32.TryParse(t20.Text, out tt20) && Int32.TryParse(t10.Text, out tt10) && Int32.TryParse(t5.Text, out tt5) && Int32.TryParse(t1.Text, out tt1) && Int32.TryParse(tH.Text, out ttH) && Int32.TryParse(tQ.Text, out ttQ))
                                {
                                    MySqlCommand command = dbconnection.CreateCommand();
                                    command.CommandText = "update bank set Bank_Name=?Bank_Name,Initial_Balance=?Initial_Balance,Start_Date=?Start_Date,Bank_Info=?Bank_Info,BankAccount_Type=?BankAccount_Type,SupplierAccountName=?SupplierAccountName where Bank_ID="+ BankID;

                                    if (cmbType.Text == "خزينة")
                                    {
                                        command.Parameters.AddWithValue("?Bank_Name", txtName_AccountNum.Text);
                                      
                                        command.Parameters.AddWithValue("?BankAccount_Type", null);
                                        command.Parameters.AddWithValue("?Supplier_ID", null);
                                    }
                                    else if (cmbType.Text == "حساب بنكى")
                                    {
                                        command.Parameters.AddWithValue("?Bank_Name", txtName_AccountNum.Text);
                                       
                                        command.Parameters.AddWithValue("?BankAccount_Type", txtAccountType.Text);
                                       
                                    }
                                    
                                    command.Parameters.AddWithValue("?Initial_Balance", stock);
                                    command.Parameters.AddWithValue("?Start_Date", dateEdit1.DateTime.Date);
                                    command.Parameters.AddWithValue("?Bank_Info", txtInformation.Text);
                                    command.Parameters.AddWithValue("?SupplierAccountName", txtAccountName.Text);
                                    command.ExecuteNonQuery();

                                    //////////record adding/////////////
                                    query = "insert into usercontrol (UserControl_UserID,UserControl_TableName,UserControl_Status,UserControl_RecordID,UserControl_Date,UserControl_Reason) values(@UserControl_UserID,@UserControl_TableName,@UserControl_Status,@UserControl_RecordID,@UserControl_Date,@UserControl_Reason)";
                                    command = new MySqlCommand(query, dbconnection);
                                    command.Parameters.Add("@UserControl_UserID", MySqlDbType.Int16, 11).Value = UserControl.userID;
                                    command.Parameters.Add("@UserControl_TableName", MySqlDbType.VarChar, 255).Value = "bank";
                                    command.Parameters.Add("@UserControl_Status", MySqlDbType.VarChar, 255).Value = "تعديل";
                                    command.Parameters.Add("@UserControl_RecordID", MySqlDbType.VarChar, 255).Value = BankID;
                                    command.Parameters.Add("@UserControl_Date", MySqlDbType.DateTime, 0).Value = DateTime.Now;
                                    command.Parameters.Add("@UserControl_Reason", MySqlDbType.VarChar, 255).Value = null;
                                    command.ExecuteNonQuery();
                                    //////////////////////
                                    setEmployee();
                                    if (cmbType.Text == "خزينة")
                                    {
                                        for (int i = 0; i < checkedListBoxControlUserID.ItemCount; i++)
                                        {
                                            query = "insert into bank_employee (Bank_ID,Employee_ID) values (@Bank_ID,@Employee_ID)";
                                            command = new MySqlCommand(query, dbconnection);
                                            command.Parameters.AddWithValue("@Bank_ID", BankID);
                                            command.Parameters.AddWithValue("@Employee_ID", checkedListBoxControlUserID.Items[i].Value.ToString());
                                            command.ExecuteNonQuery();
                                        }

                                        query = "update categories_money set a200=@a200,a100=@a100,a50=@a50,a20=@a20,a10=@a10,a5=@a5,a1=@a1,aH=@aH,aQ=@aQ ,Bank_Name=@Bank_Name where Bank_ID=" + BankID;
                                        command = new MySqlCommand(query, dbconnection);
                                        command.Parameters.Add("@a200", MySqlDbType.Int16, 11).Value = t200.Text;
                                        command.Parameters.Add("@a100", MySqlDbType.Int16, 11).Value = t100.Text;
                                        command.Parameters.Add("@a50", MySqlDbType.Int16, 11).Value = t50.Text;
                                        command.Parameters.Add("@a20", MySqlDbType.Int16, 11).Value = t20.Text;
                                        command.Parameters.Add("@a10", MySqlDbType.Int16, 11).Value = t10.Text;
                                        command.Parameters.Add("@a5", MySqlDbType.Int16, 11).Value = t5.Text;
                                        command.Parameters.Add("@a1", MySqlDbType.Int16, 11).Value = t1.Text;
                                        command.Parameters.Add("@aH", MySqlDbType.Int16, 11).Value = tH.Text;
                                        command.Parameters.Add("@aQ", MySqlDbType.Int16, 11).Value = tQ.Text;
                                        command.Parameters.Add("@Bank_Name", MySqlDbType.VarChar, 255).Value = txtName_AccountNum.Text;
                                        command.ExecuteNonQuery();
                                    }
                                    else if (cmbType.Text == "حساب بنكى")
                                    {
                                        for (int i = 0; i < checkedListBoxControlVisaID.ItemCount; i++)
                                        {
                                            query = "insert into bank_visa (Bank_ID,Machine_ID) values (@Bank_ID,@Machine_ID)";
                                            command = new MySqlCommand(query, dbconnection);
                                            command.Parameters.AddWithValue("@Bank_ID", BankID);
                                            command.Parameters.AddWithValue("@Machine_ID", checkedListBoxControlVisaID.Items[i].Value.ToString());
                                            command.ExecuteNonQuery();
                                        }
                                    }

                                    dbconnection.Close();
                                    cmbType.SelectedIndex = -1;
                                    cmbMain.SelectedIndex = -1;
                                    txtName_AccountNum.Text = "";
                                    comBranch.SelectedIndex = -1;
                                    txtStock.Text = "";
                                    dateEdit1.Text = "";
                                    txtInformation.Text = "";
                                    txtAccountType.Text = "";

                                    layoutControlItemBranch.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                                    labelBranch.Text = "";
                                    layoutControlItemName_AccountNum.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                                    labelName.Text = "";
                                    layoutControlItemStock.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                                    labelStock.Text = "";
                                    layoutControlItemDate.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                                    labelDate.Text = "";
                                    layoutControlItemInformation.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                                    labelInfo.Text = "";
                                    layoutControlItemAccountType.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                                    labelAccountType.Text = "";
                                    layoutControlItem28.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                                    //layoutControlItemBank.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                                    //labelBank.Text = "";
                                    //layoutControlItemID.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                                    //labelID.Text = "";
                                    layoutControlItemAccountName.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;

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
                                    cont = checkedListBoxControlVisaID.ItemCount;
                                    for (int i = 0; i < cont; i++)
                                    {
                                        checkedListBoxControlVisaID.Items.RemoveAt(0);
                                    }
                                    groupBoxEmployee.Visible = false;
                                    groupBoxVisa.Visible = false;
                                    groupBoxCategories.Visible = false;

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
            catch (Exception ex)
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
                    layoutControlItemName_AccountNum.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                    labelName.Text = "*";
                    layoutControlItemStock.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                    labelStock.Text = "*";
                    layoutControlItemDate.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                    labelDate.Text = "*";
                    layoutControlItemInformation.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                    labelInfo.Text = "";
                    layoutControlItemAccountType.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    labelAccountType.Text = "";
                    layoutControlItem28.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;

                    layoutControlItemAccountName.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;

                //string query = "select * from bank_main where MainBank_Type='خزينة'";
                //MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                //DataTable dt = new DataTable();
                //da.Fill(dt);
                //cmbMain.DataSource = dt;
                //cmbMain.DisplayMember = dt.Columns["MainBank_Name"].ToString();
                //cmbMain.ValueMember = dt.Columns["MainBank_ID"].ToString();
                //cmbMain.SelectedIndex = -1;
                //cmbMain.Text = "";

                //query = "SELECT Branch_Name,Branch_ID FROM branch";
                //da = new MySqlDataAdapter(query, dbconnection);
                //dt = new DataTable();
                //da.Fill(dt);
                //comBranch.DataSource = dt;
                //comBranch.DisplayMember = dt.Columns["Branch_Name"].ToString();
                //comBranch.ValueMember = dt.Columns["Branch_ID"].ToString();
                //comBranch.SelectedIndex = -1;

                string query = "select Employee_ID,Employee_Name from employee where employee.Employee_ID Not in(" + "select bank_employee.Employee_ID from bank_employee " + ")";
                MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                DataTable dt = new DataTable();
                da.Fill(dt);
                comBankUsers.DataSource = dt;
                comBankUsers.DisplayMember = dt.Columns["Employee_Name"].ToString();
                comBankUsers.ValueMember = dt.Columns["Employee_ID"].ToString();
                comBankUsers.SelectedIndex = -1;
                comBankUsers.Text = "";

                groupBoxEmployee.Visible = true;
                    groupBoxCategories.Visible = true;
                    groupBoxVisa.Visible = false;
                }
                else if (cmbType.Text == "حساب بنكى")
                {
                    layoutControlItemBranch.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    labelBranch.Text = "";
                    layoutControlItemName_AccountNum.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                    labelName.Text = "*";
                    layoutControlItemStock.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                    labelStock.Text = "*";
                    layoutControlItemDate.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                    labelDate.Text = "*";
                    layoutControlItemInformation.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                    labelInfo.Text = "";
                    layoutControlItemAccountType.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                    labelAccountType.Text = "*";
                    layoutControlItem28.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                    layoutControlItemAccountName.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;

                    //string query = "select * from bank_main where MainBank_Type='حساب بنكى'";
                    //MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                    //DataTable dt = new DataTable();
                    //da.Fill(dt);
                    //cmbMain.DataSource = dt;
                    //cmbMain.DisplayMember = dt.Columns["MainBank_Name"].ToString();
                    //cmbMain.ValueMember = dt.Columns["MainBank_ID"].ToString();
                    //cmbMain.SelectedIndex = -1;
                    //cmbMain.Text = "";

                    //query = "select * from supplier";
                    //da = new MySqlDataAdapter(query, dbconnection);
                    //dt = new DataTable();
                    //da.Fill(dt);
                    //comSupplier.DataSource = dt;
                    //comSupplier.DisplayMember = dt.Columns["Supplier_Name"].ToString();
                    //comSupplier.ValueMember = dt.Columns["Supplier_ID"].ToString();
                    //comSupplier.SelectedIndex = -1;
                    //comSupplier.Text = "";

                    groupBoxEmployee.Visible = false;
                    groupBoxCategories.Visible = false;
                    groupBoxVisa.Visible = true;
                }
            
        }

        private void txtBox_TextChanged(object sender, EventArgs e)
        {
            //try
            //{
            //    xtraTabPage = getTabPage("tabPageAddBank");
            //    if (!IsClear())
            //    {
            //        xtraTabPage.ImageOptions.Image = Properties.Resources.unsave;
            //    }
            //    else
            //    {
            //        xtraTabPage.ImageOptions.Image = null;
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
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

        private void btnAddUserToBank_Click(object sender, EventArgs e)
        {
            try
            {
                if (comBankUsers.Text != "" && comBankUsers.SelectedIndex != -1 && comBankUsers.SelectedValue != null)
                {
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

        private void btnAddVisa_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtVisaID.Text != "")
                {
                    for (int i = 0; i < checkedListBoxControlVisaID.ItemCount; i++)
                    {
                        if (txtVisaID.Text == checkedListBoxControlVisaID.Items[i].Value.ToString())
                        {
                            MessageBox.Show("هذه الفيزا تم اضافتها من قبل");
                            return;
                        }
                    }

                    checkedListBoxControlVisaID.Items.Add(txtVisaID.Text);
                    txtVisaID.Text = "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnRemoveVisa_Click(object sender, EventArgs e)
        {
            try
            {
                if (checkedListBoxControlVisaID.CheckedItemsCount > 0)
                {
                    if (MessageBox.Show("هل انت متاكد انك تريد الحذف؟", "", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                        return;

                    ArrayList temp = new ArrayList();
                    foreach (int index in checkedListBoxControlVisaID.CheckedIndices)
                    {
                        temp.Add(checkedListBoxControlVisaID.Items[index]);
                    }
                    foreach (object item in temp)
                    {
                        checkedListBoxControlVisaID.Items.Remove(item);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        
        public void setEmployee()
        {
            string query = "select * from bank_employee inner join employee on bank_employee.Employee_ID=employee.Employee_ID where Bank_ID=" + BankID;
            MySqlCommand com = new MySqlCommand(query, dbconnection);
            MySqlDataReader dr = com.ExecuteReader();
            while (dr.Read())
            {
                checkedListBoxControlUserID.Items.Add(dr["employee.Employee_ID"].ToString());
                checkedListBoxControlUser.Items.Add(dr["Employee_Name"].ToString());
            }
            dr.Close();

            query = "delete bank_employee where  Bank_ID=" + BankID;
            com = new MySqlCommand(query, dbconnection);
            com.ExecuteNonQuery();
        }
    }
}

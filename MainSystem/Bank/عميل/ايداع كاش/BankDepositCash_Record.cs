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
    public partial class BankDepositCash_Record : Form
    {
        MySqlConnection dbconnection, myConnection, connectionReader, connectionReader1, connectionReader2, connectionReader3, connectionReader4, connectionReader5;
        
        bool flag2 = false;
        int billNumber = 0;
        bool flag = false;
        int customerID = 0;
        int clientID = 0;
        string engName = "";
        string clientName = "";
        string customerPhoneNumber = "";
        string clientPhoneNumber = "";
        double totalCostBD = 0;
        double totalDiscount = 0;
        string TypeBuy = "";
        string delegateName = "";
        DateTime billDate;
        int branchID = 0;
        string branchName = "";
        int ID = -1;
        double paidAmount = 0;
        string PaymentMethod = "";
        int[] arrOFPhaat; //count of each catagory value of money in store
        int[] arrRestMoney;
        int[] arrPaidMoney;
        bool loaded = false;
        XtraTabPage xtraTabPage;
        bool loadedPayType = false;
        bool flagCategoriesSuccess = false;
        string TransitionID = "";
        string ConfirmEmp = "";
        bool flagBillNotFirstTime = false;
        XtraTabControl tabControlBank;
        int transitionbranchID = 0;

        public BankDepositCash_Record(BankDepositCash_Report form, XtraTabControl MainTabControlBank)
        {
            InitializeComponent();
            dbconnection = new MySqlConnection(connection.connectionString);
            myConnection = new MySqlConnection(connection.connectionString);
            connectionReader = new MySqlConnection(connection.connectionString);
            connectionReader1 = new MySqlConnection(connection.connectionString);
            connectionReader2 = new MySqlConnection(connection.connectionString);
            connectionReader3 = new MySqlConnection(connection.connectionString);
            connectionReader4 = new MySqlConnection(connection.connectionString);
            connectionReader5 = new MySqlConnection(connection.connectionString);
            arrOFPhaat = new int[9];
            arrPaidMoney = new int[9];
            arrRestMoney = new int[9];
            tabControlBank = MainTabControlBank;
            
            //cmbBranch.Enabled = false;
            //cmbBranch.DropDownStyle = ComboBoxStyle.DropDownList;

            this.dateEdit1.Properties.DisplayFormat.FormatString = "yyyy/MM/dd";
            this.dateEdit1.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dateEdit1.Properties.EditFormat.FormatString = "yyyy/MM/dd";
            this.dateEdit1.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dateEdit1.Properties.Mask.EditMask = "yyyy/MM/dd";
        }

        private void BankDepositCash_Record_Load(object sender, EventArgs e)
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

        private void cmbBranch_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (loaded)
                {
                    if (int.TryParse(cmbBranch.SelectedValue.ToString(), out branchID))
                    {
                        branchName = cmbBranch.Text;
                        txtBillNumber.Enabled = true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txtBillNum_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    dbconnection.Open();
                    if (branchID > 0)
                    {
                        if (int.TryParse(txtBillNumber.Text, out billNumber))
                        {
                            ID = -1;
                            delegateName = "";
                            ConfirmEmp = "";
                            flagBillNotFirstTime = false;

                            string query = "select * from customer_bill where Branch_BillNumber=" + billNumber + " and Branch_ID=" + branchID + " and (Type_Buy='كاش' or Type_Buy='آجل')";
                            MySqlCommand com = new MySqlCommand(query, dbconnection);
                            MySqlDataReader dr = com.ExecuteReader();

                            while (dr.Read())
                            {
                                //double amont = 0;
                                flag2 = true;
                                ID = Convert.ToInt32(dr["CustomerBill_ID"].ToString());
                                TypeBuy = dr["Type_Buy"].ToString();
                                billDate = Convert.ToDateTime(dr["Bill_Date"].ToString());
                                ConfirmEmp = dr["Employee_Name"].ToString();
                                
                                connectionReader3.Open();
                                string q1 = "SELECT distinct delegate.Delegate_Name FROM customer_bill INNER JOIN product_bill ON product_bill.CustomerBill_ID = customer_bill.CustomerBill_ID INNER JOIN delegate ON delegate.Delegate_ID = product_bill.Delegate_ID where customer_bill.CustomerBill_ID=" + ID;
                                MySqlCommand c1 = new MySqlCommand(q1, connectionReader3);
                                MySqlDataReader dr1 = c1.ExecuteReader();
                                while (dr1.Read())
                                {
                                    if (delegateName != "")
                                    {
                                        delegateName += ",";
                                    }
                                    delegateName += dr1["Delegate_Name"].ToString();
                                }
                                dr1.Close();
                                connectionReader3.Close();

                                myConnection.Open();
                                string query3 = "SELECT sum(Amount) FROM transitions where Bill_Number=" + billNumber + " and Branch_ID=" + branchID + " and Transition='ايداع' group by Bill_Number";
                                MySqlCommand com2 = new MySqlCommand(query3, myConnection);
                                if (com2.ExecuteScalar() != null)
                                {
                                    paidAmount = Convert.ToDouble(com2.ExecuteScalar().ToString());
                                }
                                else
                                {
                                    paidAmount = 0;
                                }

                                //query3 = "SELECT sum(Amount) FROM transitions where Bill_Number=" + billNumber + " and Branch_ID=" + branchID + " and Transition='سحب' group by Bill_Number";
                                //com2 = new MySqlCommand(query3, myConnection);
                                //if (com2.ExecuteScalar() != null)
                                //{
                                //    amont = Convert.ToDouble(com2.ExecuteScalar().ToString());
                                //}

                                /*query3 = "SELECT users.User_Name FROM customer_bill INNER JOIN users ON users.User_ID = customer_bill.Employee_ID where customer_bill.CustomerBill_ID=" + ID;
                                com2 = new MySqlCommand(query3, myConnection);
                                if (com2.ExecuteScalar() != null)
                                {
                                    ConfirmEmp = com2.ExecuteScalar().ToString();
                                }*/

                                myConnection.Close();

                                //paidAmount -= amont;

                                totalCostBD = Convert.ToDouble(dr["Total_CostBD"].ToString());
                                totalDiscount = Convert.ToDouble(dr["Total_Discount"].ToString());
                                txtTotalCost.Text = dr["Total_CostAD"].ToString();

                                if (dr["Customer_ID"].ToString() != "")
                                {
                                    customerID = Convert.ToInt32(dr["Customer_ID"].ToString());
                                }
                                else
                                {
                                    customerID = 0;
                                }
                                if (dr["Client_ID"].ToString() != "")
                                {
                                    clientID = Convert.ToInt32(dr["Client_ID"].ToString());
                                }
                                else
                                {
                                    clientID = 0;
                                }
                            }
                            dr.Close();
                            if (flag2 == true)
                            {
                                double total = Convert.ToDouble(txtTotalCost.Text);
                                txtRestMoney.Text = (total - paidAmount).ToString();

                                //extract customer info
                                if (clientID > 0)
                                {
                                    query = "select * from customer inner join customer_phone on customer.Customer_ID=customer_phone.Customer_ID where customer.Customer_ID=" + clientID + " order by customer_phone.CustomerPhone_ID desc limit 1";
                                    com = new MySqlCommand(query, dbconnection);
                                    dr = com.ExecuteReader();
                                    while (dr.Read())
                                    {
                                        //txtClientName.Text = dr["Customer_Name"].ToString();
                                        clientPhoneNumber = dr["Phone"].ToString();
                                        clientName = dr["Customer_Name"].ToString();
                                    }
                                    dr.Close();
                                }
                                else
                                {
                                    clientPhoneNumber = "";
                                    clientName = "";
                                }
                                //else
                                //{
                                //    MessageBox.Show("لابد من وجود عميل");
                                //    dbconnection.Close();
                                //    return;
                                //}
                                if (customerID > 0)
                                {
                                    query = "select * from customer inner join customer_phone on customer.Customer_ID=customer_phone.Customer_ID where customer.Customer_ID=" + customerID + " order by customer_phone.CustomerPhone_ID desc limit 1";
                                    com = new MySqlCommand(query, dbconnection);
                                    dr = com.ExecuteReader();
                                    while (dr.Read())
                                    {
                                        //txtEngName.Text = dr["Customer_Name"].ToString();
                                        customerPhoneNumber = dr["Phone"].ToString();
                                        engName = dr["Customer_Name"].ToString();
                                    }
                                    dr.Close();
                                }
                                else
                                {
                                    engName = "";
                                    customerPhoneNumber = "";
                                }
                                flag2 = false;
                            }
                            else
                            {
                                clear();
                                MessageBox.Show("لا يوجد فاتورة بهذا الرقم فى الفرع");
                            }
                        }
                        else
                        {
                            MessageBox.Show("رقم الفاتورة يجب ان يكون رقم");
                        }
                    }
                    else
                    {
                        MessageBox.Show("يجب ان تختار فرع اولا");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                dbconnection.Close();
                myConnection.Close();
                connectionReader3.Close();
            }
        }

        private void radioButtonSafe_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                RadioButton r = (RadioButton)sender;
                radCash.Enabled = true;
                radCash.Checked = false;
                radCredit.Enabled = true;
                radCredit.Checked = false;
                radBankAccount.Enabled = false;
                radBankAccount.Checked = false;
                radVisa.Enabled = false;
                radVisa.Checked = false;

                ///////////////////////
                layoutControlItemBank.Text = "خزينة";
                layoutControlItemBank.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                labelBank.Visible = false;
                labelBank.Text = "";
                layoutControlItemMoney.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                labelPaidMoney.Visible = false;
                labelPaidMoney.Text = "";
                layoutControlItemComment.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                labelDescrip.Visible = false;
                layoutControlItemPayDate.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                labelDate.Visible = false;
                labelDate.Text = "";
                layoutControlItemCheck.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                labelCheckNumber.Visible = false;
                labelCheckNumber.Text = "";
                layoutControlItemOperationNumber.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                labelOperationNumber.Visible = false;
                labelOperationNumber.Text = "";
                /////////////////////////////////////
                
                //radCash.Checked = true;

                if (UserControl.userType == 1)
                {
                    cmbBank.Enabled = true;
                    cmbBank.AutoCompleteMode = AutoCompleteMode.Suggest;
                    cmbBank.AutoCompleteSource = AutoCompleteSource.ListItems;
                }
                else
                {
                    cmbBank.Enabled = false;
                    cmbBank.AutoCompleteMode = AutoCompleteMode.None;
                    cmbBank.DropDownStyle = ComboBoxStyle.DropDownList;
                }

                string query = "select * from bank inner join bank_main on bank.MainBank_ID=bank_main.MainBank_ID where Branch_ID=" + transitionbranchID + " and MainBank_Type='خزينة' and MainBank_Name = 'خزينة مبيعات'";
                MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                DataTable dt = new DataTable();
                da.Fill(dt);
                cmbBank.DataSource = dt;
                cmbBank.DisplayMember = dt.Columns["Bank_Name"].ToString();
                cmbBank.ValueMember = dt.Columns["Bank_ID"].ToString();
                if (UserControl.userType == 1)
                {
                    cmbBank.SelectedIndex = -1;
                }
                else
                {
                    dbconnection.Open();
                    string q = "SELECT bank.Bank_Name,bank_employee.Bank_ID FROM bank_employee INNER JOIN bank ON bank.Bank_ID = bank_employee.Bank_ID where bank_employee.Employee_ID=" + UserControl.EmpID;
                    MySqlCommand com = new MySqlCommand(q, dbconnection);
                    MySqlDataReader dr = com.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            cmbBank.Text = dr["Bank_Name"].ToString();
                            cmbBank.SelectedValue = dr["Bank_ID"].ToString();
                        }
                        dr.Close();
                    }
                    else
                    {
                        cmbBank.SelectedIndex = -1;
                    }
                }
                loadedPayType = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }

        private void radCash_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                RadioButton r = (RadioButton)sender;
                PaymentMethod = r.Text;

                layoutControlItemBank.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                labelBank.Visible = true;
                labelBank.Text = "*";
                layoutControlItemMoney.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                labelPaidMoney.Visible = true;
                labelPaidMoney.Text = "*";
                layoutControlItemComment.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                labelDescrip.Visible = true;
                //groupBox1.Enabled = false;
                loadedPayType = true;
                
                //////////////////////////////////
                layoutControlItemPayDate.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                labelDate.Visible = false;
                labelDate.Text = "";
                layoutControlItemCheck.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                labelCheckNumber.Visible = false;
                labelCheckNumber.Text = "";
                groupBox1.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void radCredit_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                RadioButton r = (RadioButton)sender;
                PaymentMethod = r.Text;
                layoutControlItemPayDate.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                labelDate.Visible = true;
                labelDate.Text = "*";
                layoutControlItemCheck.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                labelCheckNumber.Visible = true;
                labelCheckNumber.Text = "*";
                layoutControlItemPayDate.Text = "تاريخ الاستحقاق";
                layoutControlItemCheck.Text = "رقم الشيك";
                groupBox1.Enabled = false;

                layoutControlItemBank.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                labelBank.Visible = true;
                labelBank.Text = "*";
                layoutControlItemMoney.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                labelPaidMoney.Visible = true;
                labelPaidMoney.Text = "*";
                layoutControlItemComment.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                labelDescrip.Visible = true;
                //groupBox1.Enabled = false;
                loadedPayType = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void radioButtonBank_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                RadioButton r = (RadioButton)sender;
                radCash.Enabled = false;
                radCash.Checked = false;
                radCredit.Enabled = false;
                radCredit.Checked = false;
                radVisa.Enabled = true;
                radVisa.Checked = false;
                radBankAccount.Enabled = true;
                radBankAccount.Checked = false;
                //radBankAccount.Checked = true;

                cmbBank.Enabled = true;
                cmbBank.AutoCompleteSource = AutoCompleteSource.ListItems;
                cmbBank.AutoCompleteMode = AutoCompleteMode.Suggest;

                ///////////////////////
                layoutControlItemBank.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                labelBank.Visible = false;
                labelBank.Text = "";
                layoutControlItemMoney.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                labelPaidMoney.Visible = false;
                labelPaidMoney.Text = "";
                layoutControlItemComment.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                labelDescrip.Visible = false;
                layoutControlItemPayDate.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                labelDate.Visible = false;
                labelDate.Text = "";
                layoutControlItemCheck.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                labelCheckNumber.Visible = false;
                labelCheckNumber.Text = "";
                layoutControlItemOperationNumber.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                labelOperationNumber.Visible = false;
                labelOperationNumber.Text = "";
                /////////////////////////////////////
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void radBankAccount_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                RadioButton r = (RadioButton)sender;
                PaymentMethod = r.Text;

                ////////////////////////////////
                layoutControlItemBank.Text = "بنك";
                layoutControlItemBank.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                labelBank.Visible = true;
                labelBank.Text = "*";
                layoutControlItemPayDate.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                labelDate.Visible = true;
                labelDate.Text = "*";
                layoutControlItemMoney.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                labelPaidMoney.Visible = true;
                labelPaidMoney.Text = "*";
                layoutControlItemComment.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                labelDescrip.Visible = true;
                //////////////////////////////////
                
                layoutControlItemOperationNumber.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                labelOperationNumber.Visible = false;
                labelOperationNumber.Text = "";
                layoutControlItemPayDate.Text = "تاريخ الايداع";
                layoutControlItemCheck.Text = "رقم الحساب";

                string query = "select concat(MainBank_Name,' ',Bank_Name) as Bank_Name,Bank_ID from bank inner join bank_main on bank.MainBank_ID=bank_main.MainBank_ID where MainBank_Type = 'حساب بنكى'";
                MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                DataTable dt = new DataTable();
                da.Fill(dt);
                cmbBank.DataSource = dt;
                cmbBank.DisplayMember = dt.Columns["Bank_Name"].ToString();
                cmbBank.ValueMember = dt.Columns["Bank_ID"].ToString();
                //if (UserControl.userType == 1)
                //{
                    cmbBank.SelectedIndex = -1;
                /*}
                else
                {
                    dbconnection.Open();
                    string q = "SELECT bank.Bank_Name,bank_employee.Bank_ID FROM bank_employee INNER JOIN bank ON bank.Bank_ID = bank_employee.Bank_ID where bank_employee.Employee_ID=" + UserControl.EmpID;
                    MySqlCommand com = new MySqlCommand(q, dbconnection);
                    MySqlDataReader dr = com.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            cmbBank.Text = dr["Bank_Name"].ToString();
                            cmbBank.SelectedValue = dr["Bank_ID"].ToString();
                        }
                        dr.Close();
                    }
                    else
                    {
                        cmbBank.SelectedIndex = -1;
                    }
                }*/
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }

        private void radVisa_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                RadioButton r = (RadioButton)sender;
                PaymentMethod = r.Text;
                
                ////////////////////////////////////
                layoutControlItemBank.Text = "فيزا";
                layoutControlItemBank.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                labelBank.Visible = true;
                labelBank.Text = "*";
                layoutControlItemPayDate.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                labelDate.Visible = false;
                labelDate.Text = "";
                layoutControlItemMoney.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                labelPaidMoney.Visible = true;
                labelPaidMoney.Text = "*";
                layoutControlItemComment.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                labelDescrip.Visible = true;
                ///////////////////////////////////////
                
                layoutControlItemOperationNumber.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                labelOperationNumber.Visible = true;
                labelOperationNumber.Text = "*";

                string query = "select concat(MainBank_Name,' ',Bank_Name,' - ',Machine_ID) as Machine_ID,Visa_ID from bank_visa inner join bank on bank_visa.Bank_ID=bank.Bank_ID inner join bank_main on bank.MainBank_ID=bank_main.MainBank_ID where MainBank_Type = 'حساب بنكى'";
                MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                DataTable dt = new DataTable();
                da.Fill(dt);
                cmbBank.DataSource = dt;
                cmbBank.DisplayMember = dt.Columns["Machine_ID"].ToString();
                cmbBank.ValueMember = dt.Columns["Visa_ID"].ToString();
                //if (UserControl.userType == 1)
                //{
                    cmbBank.SelectedIndex = -1;
                /*}
                else
                {
                    dbconnection.Open();
                    string q = "SELECT bank.Bank_Name,bank_employee.Bank_ID FROM bank_employee INNER JOIN bank ON bank.Bank_ID = bank_employee.Bank_ID where bank_employee.Employee_ID=" + UserControl.EmpID;
                    MySqlCommand com = new MySqlCommand(q, dbconnection);
                    MySqlDataReader dr = com.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            cmbBank.Text = dr["Bank_Name"].ToString();
                            cmbBank.SelectedValue = dr["Bank_ID"].ToString();
                        }
                        dr.Close();
                    }
                    else
                    {
                        cmbBank.SelectedIndex = -1;
                    }
                }*/
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
                bool check = false;
                if (PaymentMethod == "نقدى")
                {
                    check = (ID != -1 && cmbBranch.Text != "" && branchID != 0 && txtRestMoney.Text != "" && cmbBank.Text != "" && txtPaidMoney.Text != "");
                }
                else if (PaymentMethod == "شيك")
                {
                    check = (ID != -1 && cmbBranch.Text != "" && branchID != 0 && txtRestMoney.Text != "" && cmbBank.Text != "" && txtPaidMoney.Text != "" && dateEdit1.Text != "" && txtCheckNumber.Text != "");
                }
                else if (PaymentMethod == "حساب بنكى")
                {
                    check = (ID != -1 && cmbBranch.Text != "" && branchID != 0 && txtRestMoney.Text != "" && cmbBank.Text != "" && txtPaidMoney.Text != "" && dateEdit1.Text != "");
                }
                else if (PaymentMethod == "فيزا")
                {
                    check = (ID != -1 && cmbBranch.Text != "" && branchID != 0 && txtRestMoney.Text != "" && cmbBank.Text != "" && txtPaidMoney.Text != "" && txtOperationNumber.Text != "");
                }

                if (check)
                {
                    if (Convert.ToDouble(txtRestMoney.Text) > 0)
                    { }
                    else
                    {
                        MessageBox.Show("لا يوجد متبقى على الفاتورة");
                        return;
                    }

                    if (!flagCategoriesSuccess)
                    {
                        if (MessageBox.Show("لم يتم ادخال الفئات..هل تريد الاستمرار؟", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                        {
                            return;
                        }
                    }
                    
                    double outParse;
                    if (double.TryParse(txtPaidMoney.Text, out outParse))
                    {
                        double restMoney = 0;
                        if (double.TryParse(txtRestMoney.Text, out restMoney))
                        {
                            if (outParse <= restMoney)
                            {
                                string opNumString = null;

                                if (txtOperationNumber.Text != "")
                                {
                                    int OpNum = 0;
                                    if (int.TryParse(txtOperationNumber.Text, out OpNum))
                                    {
                                        opNumString = txtOperationNumber.Text;
                                    }
                                    else
                                    {
                                        MessageBox.Show("رقم العملية يجب ان يكون عدد");
                                        dbconnection.Close();
                                        return;
                                    }
                                }
                                
                                dbconnection.Open();
                                connectionReader4.Open();
                                string qt = "SELECT * FROM transitions where (transitions.Type='كاش' or transitions.Type='آجل') and Transition='ايداع' and transitions.Branch_ID=" + branchID + " and transitions.Bill_Number=" + billNumber;
                                MySqlCommand ct = new MySqlCommand(qt, connectionReader4);
                                MySqlDataReader dtt = ct.ExecuteReader();
                                if (!dtt.HasRows)
                                {
                                    DecreaseProductQuantity();
                                }
                                else
                                {
                                    flagBillNotFirstTime = true;
                                }
                                connectionReader4.Close();

                                string query = "insert into transitions (TransitionBranch_ID,TransitionBranch_Name,Branch_ID,Branch_Name,Client_ID,Client_Name,Customer_ID,Customer_Name,Transition,Payment_Method,Bank_ID,Bank_Name,Date,Amount,Data,PayDay,Check_Number,Operation_Number,Bill_Number,Type,Error,Employee_ID,Employee_Name) values(@TransitionBranch_ID,@TransitionBranch_Name,@Branch_ID,@Branch_Name,@Client_ID,@Client_Name,@Customer_ID,@Customer_Name,@Transition,@Payment_Method,@Bank_ID,@Bank_Name,@Date,@Amount,@Data,@PayDay,@Check_Number,@Operation_Number,@Bill_Number,@Type,@Error,@Employee_ID,@Employee_Name)";
                                MySqlCommand com = new MySqlCommand(query, dbconnection);

                                com.Parameters.Add("@Transition", MySqlDbType.VarChar, 255).Value = "ايداع";
                                com.Parameters.Add("@Type", MySqlDbType.VarChar, 255).Value = TypeBuy;
                                com.Parameters.Add("@TransitionBranch_ID", MySqlDbType.Int16, 11).Value = transitionbranchID;
                                com.Parameters.Add("@TransitionBranch_Name", MySqlDbType.VarChar, 255).Value = UserControl.EmpBranchName;
                                com.Parameters.Add("@Branch_ID", MySqlDbType.Int16, 11).Value = cmbBranch.SelectedValue;
                                com.Parameters.Add("@Branch_Name", MySqlDbType.VarChar, 255).Value = cmbBranch.Text;
                                com.Parameters.Add("@Bill_Number", MySqlDbType.Int16, 11).Value = billNumber;
                                com.Parameters.Add("@Payment_Method", MySqlDbType.VarChar, 255).Value = PaymentMethod;
                                com.Parameters.Add("@Bank_ID", MySqlDbType.Int16, 11).Value = cmbBank.SelectedValue;
                                com.Parameters.Add("@Bank_Name", MySqlDbType.VarChar, 255).Value = cmbBank.Text;
                                com.Parameters.Add("@Date", MySqlDbType.DateTime, 0).Value = DateTime.Now;
                                if (clientID > 0)
                                {
                                    com.Parameters.Add("@Client_ID", MySqlDbType.Int16, 11).Value = clientID;
                                    com.Parameters.Add("@Client_Name", MySqlDbType.VarChar, 255).Value = clientName;
                                }
                                else
                                {
                                    com.Parameters.Add("@Client_ID", MySqlDbType.Int16, 11).Value = null;
                                    com.Parameters.Add("@Client_Name", MySqlDbType.VarChar, 255).Value = null;
                                }
                                if (customerID > 0)
                                {
                                    com.Parameters.Add("@Customer_ID", MySqlDbType.Int16, 11).Value = customerID;
                                    com.Parameters.Add("@Customer_Name", MySqlDbType.VarChar, 255).Value = engName;
                                }
                                else
                                {
                                    com.Parameters.Add("@Customer_ID", MySqlDbType.Int16, 11).Value = null;
                                    com.Parameters.Add("@Customer_Name", MySqlDbType.VarChar, 255).Value = null;
                                }
                                com.Parameters.Add("@Operation_Number", MySqlDbType.Int16, 11).Value = opNumString;
                                com.Parameters.Add("@Data", MySqlDbType.VarChar, 255).Value = txtDescrip.Text;
                                com.Parameters.Add("@Error", MySqlDbType.Int16, 11).Value = 0;

                                com.Parameters.Add("@Amount", MySqlDbType.Decimal, 10).Value = txtPaidMoney.Text;
                                if (PaymentMethod != "فيزا")
                                {
                                    MySqlCommand com2 = new MySqlCommand("select Bank_Stock from bank where Bank_ID=" + cmbBank.SelectedValue, dbconnection);
                                    double amount2 = Convert.ToDouble(com2.ExecuteScalar().ToString());
                                    amount2 += outParse;
                                    MySqlCommand com3 = new MySqlCommand("update bank set Bank_Stock=" + amount2 + " where Bank_ID=" + cmbBank.SelectedValue, dbconnection);
                                    com3.ExecuteNonQuery();
                                }
                                else
                                {
                                    MySqlCommand com2 = new MySqlCommand("select bank.Bank_ID from bank inner join bank_visa on bank.Bank_ID=bank_visa.Bank_ID where Visa_ID=" + cmbBank.SelectedValue, dbconnection);
                                    string BankVisaId = com2.ExecuteScalar().ToString();

                                    com2 = new MySqlCommand("select Bank_Stock from bank where Bank_ID=" + BankVisaId, dbconnection);
                                    double amount2 = Convert.ToDouble(com2.ExecuteScalar().ToString());
                                    amount2 += outParse;
                                    MySqlCommand com3 = new MySqlCommand("update bank set Bank_Stock=" + amount2 + " where Bank_ID=" + BankVisaId, dbconnection);
                                    com3.ExecuteNonQuery();
                                }

                                if (dateEdit1.Text != "")
                                {
                                    com.Parameters.Add("@PayDay", MySqlDbType.Date, 0).Value = dateEdit1.DateTime.Date;
                                }
                                else
                                {
                                    com.Parameters.Add("@PayDay", MySqlDbType.Date, 0).Value = null;
                                }

                                if (txtCheckNumber.Text != "")
                                {
                                    com.Parameters.Add("@Check_Number", MySqlDbType.VarChar, 255).Value = txtCheckNumber.Text;
                                }
                                else
                                {
                                    com.Parameters.Add("@Check_Number", MySqlDbType.VarChar, 255).Value = null;
                                }
                                com.Parameters.Add("@Employee_ID", MySqlDbType.Int16, 11).Value = UserControl.EmpID;
                                com.Parameters.Add("@Employee_Name", MySqlDbType.VarChar, 255).Value = UserControl.EmpName;

                                com.ExecuteNonQuery();
                                //////////record adding/////////////
                                query = "select Transition_ID from transitions order by Transition_ID desc limit 1";
                                com = new MySqlCommand(query, dbconnection);
                                TransitionID = com.ExecuteScalar().ToString();

                                query = "insert into usercontrol (UserControl_UserID,UserControl_TableName,UserControl_Status,UserControl_RecordID,UserControl_Date,UserControl_Reason) values(@UserControl_UserID,@UserControl_TableName,@UserControl_Status,@UserControl_RecordID,@UserControl_Date,@UserControl_Reason)";
                                com = new MySqlCommand(query, dbconnection);
                                com.Parameters.Add("@UserControl_UserID", MySqlDbType.Int16, 11).Value = UserControl.userID;
                                com.Parameters.Add("@UserControl_TableName", MySqlDbType.VarChar, 255).Value = "transitions";
                                com.Parameters.Add("@UserControl_Status", MySqlDbType.VarChar, 255).Value = "اضافة";
                                com.Parameters.Add("@UserControl_RecordID", MySqlDbType.VarChar, 255).Value = TransitionID;
                                com.Parameters.Add("@UserControl_Date", MySqlDbType.DateTime, 0).Value = DateTime.Now;
                                com.Parameters.Add("@UserControl_Reason", MySqlDbType.VarChar, 255).Value = null;
                                com.ExecuteNonQuery();

                                //////////insert categories/////////
                                query = "insert into transition_categories_money (a200,a100,a50,a20,a10,a5,a1,aH,aQ,r200,r100,r50,r20,r10,r5,r1,rH,rQ,Transition_ID) values(@a200,@a100,@a50,@a20,@a10,@a5,@a1,@aH,@aQ,@r200,@r100,@r50,@r20,@r10,@r5,@r1,@rH,@rQ,@Transition_ID)";
                                com = new MySqlCommand(query, dbconnection);
                                com.Parameters.Add("@a200", MySqlDbType.Int16, 11).Value = arrPaidMoney[0];
                                com.Parameters.Add("@a100", MySqlDbType.Int16, 11).Value = arrPaidMoney[1];
                                com.Parameters.Add("@a50", MySqlDbType.Int16, 11).Value = arrPaidMoney[2];
                                com.Parameters.Add("@a20", MySqlDbType.Int16, 11).Value = arrPaidMoney[3];
                                com.Parameters.Add("@a10", MySqlDbType.Int16, 11).Value = arrPaidMoney[4];
                                com.Parameters.Add("@a5", MySqlDbType.Int16, 11).Value = arrPaidMoney[5];
                                com.Parameters.Add("@a1", MySqlDbType.Int16, 11).Value = arrPaidMoney[6];
                                com.Parameters.Add("@aH", MySqlDbType.Int16, 11).Value = arrPaidMoney[7];
                                com.Parameters.Add("@aQ", MySqlDbType.Int16, 11).Value = arrPaidMoney[8];
                                com.Parameters.Add("@r200", MySqlDbType.Int16, 11).Value = arrRestMoney[0];
                                com.Parameters.Add("@r100", MySqlDbType.Int16, 11).Value = arrRestMoney[1];
                                com.Parameters.Add("@r50", MySqlDbType.Int16, 11).Value = arrRestMoney[2];
                                com.Parameters.Add("@r20", MySqlDbType.Int16, 11).Value = arrRestMoney[3];
                                com.Parameters.Add("@r10", MySqlDbType.Int16, 11).Value = arrRestMoney[4];
                                com.Parameters.Add("@r5", MySqlDbType.Int16, 11).Value = arrRestMoney[5];
                                com.Parameters.Add("@r1", MySqlDbType.Int16, 11).Value = arrRestMoney[6];
                                com.Parameters.Add("@rH", MySqlDbType.Int16, 11).Value = arrRestMoney[7];
                                com.Parameters.Add("@rQ", MySqlDbType.Int16, 11).Value = arrRestMoney[8];
                                com.Parameters.Add("@Transition_ID", MySqlDbType.Int16, 11).Value = Convert.ToInt32(TransitionID);
                                com.ExecuteNonQuery();
                                flagCategoriesSuccess = false;
                                //////////////////////

                                if (TypeBuy == "آجل" && outParse == 0)
                                {
                                    query = "update customer_bill set Paid_Status=0 where CustomerBill_ID=" + ID;
                                }
                                else if (Convert.ToDouble(txtRestMoney.Text) - Convert.ToDouble(txtPaidMoney.Text) == 0 /*|| checkBoxTaswya.Checked == true*/)
                                {
                                    query = "update customer_bill set Paid_Status=1 where CustomerBill_ID=" + ID;
                                }
                                else if (checkBoxTaswya.Checked == true && (Convert.ToDouble(txtRestMoney.Text) - Convert.ToDouble(txtPaidMoney.Text)) <= 5)
                                {
                                    query = "update customer_bill set Paid_Status=1 where CustomerBill_ID=" + ID;
                                }
                                else if (Convert.ToDouble(txtRestMoney.Text) - Convert.ToDouble(txtPaidMoney.Text) > 0)
                                {
                                    query = "update customer_bill set Paid_Status=2 where CustomerBill_ID=" + ID;
                                }
                                
                                com = new MySqlCommand(query, dbconnection);
                                com.ExecuteNonQuery();
                                
                                dbconnection.Close();

                                if (!flagBillNotFirstTime)
                                {
                                    //print bill
                                    printBill();
                                    printBillAccounting();
                                    flagBillNotFirstTime = false;
                                }

                                //print bill
                                printCategoriesBill();

                                //finalFlag = true;
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
                                r200.Text = "";
                                r100.Text = "";
                                r50.Text = "";
                                r20.Text = "";
                                r10.Text = "";
                                r5.Text = "";
                                r1.Text = "";
                                rH.Text = "";
                                rQ.Text = "";
                                RestMoney.Text = "0";
                                PaidMoney.Text = "0";
                                txtPaidRest.Text = "0";
                                txtPaidRest2.Text = "0";
                                checkBoxTaswya.Checked = false;
                                
                                xtraTabPage.ImageOptions.Image = null;
                            }
                            else
                            {
                                MessageBox.Show("برجاء التاكد من المبلغ المدفوع");
                                dbconnection.Close();
                                return;
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("المبلغ المدفوع يجب ان يكون عدد");
                        dbconnection.Close();
                        return;
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
            connectionReader2.Close();
            connectionReader1.Close();
            connectionReader.Close();
            connectionReader3.Close();
            connectionReader4.Close();
            connectionReader5.Close();
        }
        
        private void PaidMoney_KeyDown(object sender, KeyEventArgs e)
        {
            double totalPaid = 0;

            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    double total = 0;
                    if (txtPaidMoney.Text != "" && double.TryParse(txtPaidMoney.Text, out total))
                    {
                        if (cmbBank.Text == "")
                        {
                            MessageBox.Show("يجب ان تختار الخزينة");
                            return;
                        }
                        dbconnection.Open();

                        string query = "select ID from categories_money where Bank_ID=" + cmbBank.SelectedValue;
                        MySqlCommand com = new MySqlCommand(query, dbconnection);
                        if (com.ExecuteScalar() == null)
                        {
                            query = "insert into categories_money (a200,a100,a50,a20,a10,a5,a1,aH,aQ,Bank_ID,Bank_Name)values(0,0,0,0,0,0,0,0,0,@Bank_ID,@Bank_Name) ";
                            com = new MySqlCommand(query, dbconnection);
                            com.Parameters.Add("@Bank_ID", MySqlDbType.Int16).Value = cmbBank.SelectedValue;
                            com.Parameters.Add("@Bank_Name", MySqlDbType.VarChar).Value = cmbBank.Text;
                            com.ExecuteNonQuery();
                        }

                        if (!flag)
                        {
                            string query2 = "select * from categories_money where Bank_ID=" + cmbBank.SelectedValue;
                            MySqlCommand com2 = new MySqlCommand(query2, dbconnection);
                            MySqlDataReader dr = com2.ExecuteReader();
                            while (dr.Read())
                            {
                                arrOFPhaat[0] = Convert.ToInt32(dr["a200"]);
                                arrOFPhaat[1] = Convert.ToInt32(dr["a100"]);
                                arrOFPhaat[2] = Convert.ToInt32(dr["a50"]);
                                arrOFPhaat[3] = Convert.ToInt32(dr["a20"]);
                                arrOFPhaat[4] = Convert.ToInt32(dr["a10"]);
                                arrOFPhaat[5] = Convert.ToInt32(dr["a5"]);
                                arrOFPhaat[6] = Convert.ToInt32(dr["a1"]);
                                arrOFPhaat[7] = Convert.ToInt32(dr["aH"]);
                                arrOFPhaat[8] = Convert.ToInt32(dr["aQ"]);
                            }
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
                                    arrOFPhaat[0] -= arrPaidMoney[0];
                                    arrPaidMoney[0] = num;
                                    arrOFPhaat[0] += num;
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
                                    arrOFPhaat[1] -= arrPaidMoney[1];
                                    arrPaidMoney[1] = num;
                                    arrOFPhaat[1] += num;
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
                                    arrOFPhaat[2] -= arrPaidMoney[2];
                                    arrPaidMoney[2] = num;
                                    arrOFPhaat[2] += num;
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
                                    arrOFPhaat[3] -= arrPaidMoney[3];
                                    arrPaidMoney[3] = num;
                                    arrOFPhaat[3] += num;
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
                                    arrOFPhaat[4] -= arrPaidMoney[4];
                                    arrPaidMoney[4] = num;
                                    arrOFPhaat[4] += num;
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
                                    arrOFPhaat[5] -= arrPaidMoney[5];
                                    arrPaidMoney[5] = num;
                                    arrOFPhaat[5] += num;
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
                                    arrOFPhaat[6] -= arrPaidMoney[6];
                                    arrPaidMoney[6] = num;
                                    arrOFPhaat[6] += num;
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
                                    arrOFPhaat[7] -= arrPaidMoney[7];
                                    arrPaidMoney[7] = num;
                                    arrOFPhaat[7] += num;
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
                                    arrOFPhaat[8] -= arrPaidMoney[8];
                                    arrPaidMoney[8] = num;
                                    arrOFPhaat[8] += num;
                                    r200.Focus();
                                }
                                else
                                {
                                    MessageBox.Show("القيمة المدخلة يجب ان تكون عدد");
                                    return;
                                }
                                break;
                        }

                        totalPaid = arrPaidMoney[0] * 200 + arrPaidMoney[1] * 100 + arrPaidMoney[2] * 50 + arrPaidMoney[3] * 20 + arrPaidMoney[4] * 10 + arrPaidMoney[5] * 5 + arrPaidMoney[6] + arrPaidMoney[7] * 0.5 + arrPaidMoney[8] * 0.25;
                        PaidMoney.Text = totalPaid.ToString();
                        if ((total - totalPaid) > 0)
                        {
                            txtPaidRest.Text = (total - totalPaid).ToString();
                            txtPaidRest2.Text = "0";
                            layoutControlItemRest.AppearanceItemCaption.ForeColor = Color.FromArgb(140, 140, 140);
                            layoutControlItemPaid.AppearanceItemCaption.ForeColor = Color.FromArgb(140, 140, 140);
                        }
                        else if ((total - totalPaid) == 0)
                        {
                            txtPaidRest.Text = "0";
                            layoutControlItemPaid.AppearanceItemCaption.ForeColor = Color.FromArgb(140, 140, 140);

                            txtPaidRest2.Text = "0";
                            layoutControlItemRest.AppearanceItemCaption.ForeColor = Color.FromArgb(140, 140, 140);
                        }
                        else
                        {
                            txtPaidRest.Text = "0";
                            double sub = (total - totalPaid);

                            txtPaidRest2.Text = (-1 * (sub + Convert.ToDouble(RestMoney.Text))).ToString();
                            layoutControlItemRest.AppearanceItemCaption.ForeColor = Color.Red;
                            layoutControlItemPaid.AppearanceItemCaption.ForeColor = Color.FromArgb(140, 140, 140);
                        }

                        if ((Convert.ToDouble(txtPaidRest.Text) == 0) && (Convert.ToDouble(txtPaidRest2.Text) == 0))
                        {
                            dbconnection.Open();
                            query = "update categories_money set a200=" + arrOFPhaat[0] + ",a100=" + arrOFPhaat[1] + ",a50=" + arrOFPhaat[2] + ",a20=" + arrOFPhaat[3] + ",a10=" + arrOFPhaat[4] + ",a5=" + arrOFPhaat[5] + ",a1=" + arrOFPhaat[6] + ",aH=" + arrOFPhaat[7] + ",aQ=" + arrOFPhaat[8] + " where Bank_ID=" + cmbBank.SelectedValue;
                            com = new MySqlCommand(query, dbconnection);
                            com.ExecuteNonQuery();
                            flagCategoriesSuccess = true;
                            MessageBox.Show("تم");
                            /*t200.Text = "";
                            t100.Text = "";
                            t50.Text = "";
                            t20.Text = "";
                            t10.Text = "";
                            t5.Text = "";
                            t1.Text = "";
                            tH.Text = "";
                            tQ.Text = "";
                            r200.Text = "";
                            r100.Text = "";
                            r50.Text = "";
                            r20.Text = "";
                            r10.Text = "";
                            r5.Text = "";
                            r1.Text = "";
                            rH.Text = "";
                            rQ.Text = "";*/
                            RestMoney.Text = "0";
                            PaidMoney.Text = "0";
                            txtPaidRest.Text = "0";
                            txtPaidRest2.Text = "0";
                            layoutControlItemPaid.AppearanceItemCaption.ForeColor = Color.FromArgb(140, 140, 140);
                            layoutControlItemRest.AppearanceItemCaption.ForeColor = Color.FromArgb(140, 140, 140);
                            //for (int i = 0; i < arrPaidMoney.Length; i++)
                            //    arrPaidMoney[i] = arrRestMoney[i] = 0;
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

        private void RestMoney_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                double total = 0;
                if (txtPaidMoney.Text != "" && double.TryParse(txtPaidMoney.Text, out total))
                {
                    double totalRest = 0, test = 0;
                    if (RestMoney.Text != "")
                    {
                        totalRest = Convert.ToDouble(RestMoney.Text);
                    }

                    if (txtPaidRest.Text != "")
                        test = Convert.ToDouble(txtPaidRest.Text);

                    if (e.KeyCode == Keys.Enter)
                    {
                        TextBox txt = (TextBox)sender;
                        string txtValue = txt.Name.Split('r')[1];
                        int num;
                        num = 0;
                        switch (txtValue)
                        {
                            case "200":
                                if (int.TryParse(r200.Text, out num))
                                {
                                    if (arrOFPhaat[0] >= num)
                                    {
                                        arrOFPhaat[0] += arrRestMoney[0];
                                        arrRestMoney[0] = num;
                                        arrOFPhaat[0] -= num;
                                        r100.Focus();
                                    }
                                    else
                                    {
                                        MessageBox.Show("لا يوجد ما يكفى");
                                        return;
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("القيمة المدخلة يجب ان تكون عدد");
                                    return;
                                }
                                break;

                            case "100":
                                if (int.TryParse(r100.Text, out num))
                                {
                                    if (arrOFPhaat[1] >= num)
                                    {
                                        arrOFPhaat[1] += arrRestMoney[1];
                                        arrRestMoney[1] = num;
                                        arrOFPhaat[1] -= num;
                                        r50.Focus();
                                    }
                                    else
                                    {
                                        MessageBox.Show("لا يوجد ما يكفى");
                                        return;
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("القيمة المدخلة يجب ان تكون عدد");
                                    return;
                                }
                                break;

                            case "50":
                                if (int.TryParse(r50.Text, out num))
                                {
                                    if (arrOFPhaat[2] >= num)
                                    {
                                        arrOFPhaat[2] += arrRestMoney[2];
                                        arrRestMoney[2] = num;
                                        arrOFPhaat[2] -= num;
                                        r20.Focus();
                                    }
                                    else
                                    {
                                        MessageBox.Show("لا يوجد ما يكفى");
                                        return;
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("القيمة المدخلة يجب ان تكون عدد");
                                    return;
                                }
                                break;

                            case "20":
                                if (int.TryParse(r20.Text, out num))
                                {
                                    if (arrOFPhaat[3] >= num)
                                    {
                                        arrOFPhaat[3] += arrRestMoney[3];
                                        arrRestMoney[3] = num;
                                        arrOFPhaat[3] -= num;
                                        r10.Focus();
                                    }
                                    else
                                    {
                                        MessageBox.Show("لا يوجد ما يكفى");
                                        return;
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("القيمة المدخلة يجب ان تكون عدد");
                                    return;
                                }
                                break;

                            case "10":
                                if (int.TryParse(r10.Text, out num))
                                {
                                    if (arrOFPhaat[4] >= num)
                                    {
                                        arrOFPhaat[4] += arrRestMoney[4];
                                        arrRestMoney[4] = num;
                                        arrOFPhaat[4] -= num;
                                        r5.Focus();
                                    }
                                    else
                                    {
                                        MessageBox.Show("لا يوجد ما يكفى");
                                        return;
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("القيمة المدخلة يجب ان تكون عدد");
                                    return;
                                }
                                break;

                            case "5":
                                if (int.TryParse(r5.Text, out num))
                                {
                                    if (arrOFPhaat[5] >= num)
                                    {
                                        arrOFPhaat[5] += arrRestMoney[5];
                                        arrRestMoney[5] = num;
                                        arrOFPhaat[5] -= num;
                                        r1.Focus();
                                    }
                                    else
                                    {
                                        MessageBox.Show("لا يوجد ما يكفى");
                                        return;
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("القيمة المدخلة يجب ان تكون عدد");
                                    return;
                                }
                                break;

                            case "1":
                                if (int.TryParse(r1.Text, out num))
                                {
                                    if (arrOFPhaat[6] >= 0)
                                    {
                                        arrOFPhaat[6] += arrRestMoney[6];
                                        arrRestMoney[6] = num;
                                        arrOFPhaat[6] -= num;
                                        rH.Focus();
                                    }
                                    else
                                    {
                                        MessageBox.Show("لا يوجد ما يكفى");
                                        return;
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("القيمة المدخلة يجب ان تكون عدد");
                                    return;
                                }
                                break;

                            case "H":
                                if (int.TryParse(rH.Text, out num))
                                {
                                    if (arrOFPhaat[7] >= 0)
                                    {
                                        arrOFPhaat[7] += arrRestMoney[7];
                                        arrRestMoney[7] = num;
                                        arrOFPhaat[7] -= num;
                                        rQ.Focus();
                                    }
                                    else
                                    {
                                        MessageBox.Show("لا يوجد ما يكفى");
                                        return;
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("القيمة المدخلة يجب ان تكون عدد");
                                    return;
                                }
                                break;

                            case "Q":
                                if (int.TryParse(rQ.Text, out num))
                                {
                                    if (arrOFPhaat[8] >= 0)
                                    {
                                        arrOFPhaat[8] += arrRestMoney[8];
                                        arrRestMoney[8] = num;
                                        arrOFPhaat[8] -= num;
                                    }
                                    else
                                    {
                                        MessageBox.Show("لا يوجد ما يكفى");
                                        return;
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("القيمة المدخلة يجب ان تكون عدد");
                                    return;
                                }
                                break;
                        }

                        totalRest = arrRestMoney[0] * 200 + arrRestMoney[1] * 100 + arrRestMoney[2] * 50 + arrRestMoney[3] * 20 + arrRestMoney[4] * 10 + arrRestMoney[5] * 5 + arrRestMoney[6] + arrRestMoney[7] * 0.5 + arrRestMoney[8] * 0.25;
                        RestMoney.Text = totalRest.ToString();

                        if ((Convert.ToDouble(RestMoney.Text) - (-1 * (Convert.ToDouble(txtPaidMoney.Text) - Convert.ToDouble(PaidMoney.Text)))) < 0)
                        {
                            txtPaidRest.Text = "0";
                            txtPaidRest2.Text = (-1 * ((Convert.ToDouble(RestMoney.Text) - (-1 * (Convert.ToDouble(txtPaidMoney.Text) - Convert.ToDouble(PaidMoney.Text)))))).ToString();
                            layoutControlItemRest.AppearanceItemCaption.ForeColor = Color.Red;
                            layoutControlItemPaid.AppearanceItemCaption.ForeColor = Color.FromArgb(140, 140, 140);
                        }
                        else if ((Convert.ToDouble(RestMoney.Text) - (-1 * (Convert.ToDouble(txtPaidMoney.Text) - Convert.ToDouble(PaidMoney.Text)))) == 0)
                        {
                            txtPaidRest2.Text = "0";
                            layoutControlItemRest.AppearanceItemCaption.ForeColor = Color.FromArgb(140, 140, 140);

                            txtPaidRest.Text = "0";
                            layoutControlItemPaid.AppearanceItemCaption.ForeColor = Color.FromArgb(140, 140, 140);
                        }
                        else
                        {
                            double sub = (Convert.ToDouble(txtPaidMoney.Text) - Convert.ToDouble(PaidMoney.Text));
                            txtPaidRest.Text = (Convert.ToDouble(RestMoney.Text) - (-1 * sub)).ToString();
                            txtPaidRest2.Text = "0";
                            layoutControlItemPaid.AppearanceItemCaption.ForeColor = Color.Red;
                            layoutControlItemRest.AppearanceItemCaption.ForeColor = Color.FromArgb(140, 140, 140);
                        }

                        if ((Convert.ToDouble(txtPaidRest.Text) == 0) && (Convert.ToDouble(txtPaidRest2.Text) == 0))
                        {
                            dbconnection.Open();
                            string query = "update categories_money set a200=" + arrOFPhaat[0] + ",a100=" + arrOFPhaat[1] + ",a50=" + arrOFPhaat[2] + ",a20=" + arrOFPhaat[3] + ",a10=" + arrOFPhaat[4] + ",a5=" + arrOFPhaat[5] + ",a1=" + arrOFPhaat[6] + ",aH=" + arrOFPhaat[7] + ",aQ=" + arrOFPhaat[8] + " where Bank_ID=" + cmbBank.SelectedValue;
                            MySqlCommand com = new MySqlCommand(query, dbconnection);
                            com.ExecuteNonQuery();
                            flagCategoriesSuccess = true;
                            MessageBox.Show("تم");
                            /*t200.Text = "";
                            t100.Text = "";
                            t50.Text = "";
                            t20.Text = "";
                            t10.Text = "";
                            t5.Text = "";
                            t1.Text = "";
                            tH.Text = "";
                            tQ.Text = "";
                            r200.Text = "";
                            r100.Text = "";
                            r50.Text = "";
                            r20.Text = "";
                            r10.Text = "";
                            r5.Text = "";
                            r1.Text = "";
                            rH.Text = "";
                            rQ.Text = "";*/
                            RestMoney.Text = "0";
                            PaidMoney.Text = "0";
                            txtPaidRest.Text = "0";
                            txtPaidRest2.Text = "0";
                            layoutControlItemPaid.AppearanceItemCaption.ForeColor = Color.FromArgb(140, 140, 140);
                            layoutControlItemRest.AppearanceItemCaption.ForeColor = Color.FromArgb(140, 140, 140);
                            //for (int i = 0; i < arrRestMoney.Length; i++)
                            //    arrRestMoney[i] = arrPaidMoney[i] = 0;
                            flag = false;
                        }
                        else
                        { }
                        dbconnection.Close();
                    }
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

        private void txtBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (loaded || loadedPayType)
                {
                    xtraTabPage = getTabPage("اضافة ايداع-كاش");
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
                // ImageProduct.Image = null;
            }
        }

        /*private void txtDesignNum_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txtDesignNum.Text != "")
                {
                    int DesignNum = 0;
                    if (int.TryParse(txtDesignNum.Text, out DesignNum))
                    {
                        try
                        {
                            dbconnection.Open();
                            string query = "select customer_design.PaidMoney from customer_design where customer_design.CustomerDesign_ID=" + DesignNum;
                            MySqlCommand com = new MySqlCommand(query, dbconnection);
                            if (com.ExecuteScalar() != null)
                            {
                                txtDesignValue.Text = com.ExecuteScalar().ToString();
                            }
                            else
                            {
                                txtDesignValue.Text = "";
                                MessageBox.Show("رقم التصميم غير موجود من قبل");
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                        dbconnection.Close();
                    }
                    else
                    {
                        txtDesignValue.Text = "";
                        MessageBox.Show("رقم التصميم يجب ان يكون عدد");
                    }
                }
                else
                {
                    txtDesignValue.Text = "";
                }
            }
        }*/
        
        public XtraTabPage getTabPage(string text)
        {
            for (int i = 0; i < tabControlBank.TabPages.Count; i++)
                if (tabControlBank.TabPages[i].Text == text)
                {
                    return tabControlBank.TabPages[i];
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
                //else if (co is PictureBox)
                //{
                //if (ImageProduct.Image == null)
                //    flag = true;
                //else
                //    return false;
                //}
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

            loaded = true;

            cmbBranch.Text = UserControl.EmpBranchName;
            cmbBranch.SelectedValue = UserControl.EmpBranchID;
            //because event not fire
            branchID = UserControl.EmpBranchID;
            branchName = cmbBranch.Text;
            txtBillNumber.Enabled = true;
        }

        public void DecreaseProductQuantity()
        {
            connectionReader.Open();
            connectionReader1.Open();
            connectionReader2.Open();
            string q;
            int id;
            double storageQ, productQ;

            #region not customer
            string query = "select Data_ID,Type,Quantity,Store_ID from product_bill where CustomerBill_ID=" + ID;
            MySqlCommand com = new MySqlCommand(query, connectionReader);
            MySqlDataReader dr = com.ExecuteReader();

            while (dr.Read())
            {
                if (dr["Type"].ToString() == "بند")
                {
                    #region بند
                    double quantityInStore = 0;
                    query = "select sum(Total_Meters) from storage where Data_ID=" + dr["Data_ID"].ToString() + " and Store_ID=" + dr["Store_ID"].ToString() + " GROUP BY Store_ID,Data_ID";
                    com = new MySqlCommand(query, connectionReader2);
                    if (com.ExecuteScalar() != null)
                    {
                        quantityInStore = Convert.ToDouble(com.ExecuteScalar());
                    }
                    productQ = Convert.ToDouble(dr["Quantity"].ToString());
                    query = "select Storage_ID,Total_Meters from storage where Data_ID=" + dr["Data_ID"].ToString() + " and Store_ID=" + dr["Store_ID"].ToString() + "";
                    com = new MySqlCommand(query, connectionReader2);
                    MySqlDataReader dr2 = com.ExecuteReader();
                    while (dr2.Read())
                    {
                        if (productQ > 0)
                        {
                            storageQ = Convert.ToDouble(dr2["Total_Meters"].ToString());

                            if (storageQ >= productQ)
                            {
                                id = Convert.ToInt32(dr2["Storage_ID"].ToString());
                                q = "update storage set Total_Meters=" + (storageQ - productQ) + " where Storage_ID=" + id;
                                MySqlCommand comm = new MySqlCommand(q, dbconnection);
                                comm.ExecuteNonQuery();
                                productQ = 0;
                                //flag = true;
                                break;
                            }
                            else
                            {
                                id = Convert.ToInt32(dr2["Storage_ID"].ToString());
                                q = "update storage set Total_Meters=" + 0 + " where Storage_ID=" + id;
                                MySqlCommand comm = new MySqlCommand(q, dbconnection);
                                comm.ExecuteNonQuery();
                                productQ -= storageQ;
                            }
                        }
                        else
                        {
                            break;
                        }
                    }
                    dr2.Close();
                    #endregion
                }

                else if (dr["Type"].ToString() == "طقم")
                {
                    #region طقم
                    query = "select sum(Total_Meters) from storage where Set_ID=" + dr["Data_ID"].ToString() + " and Store_ID=" + dr["Store_ID"].ToString() + " GROUP BY Store_ID,Set_ID";
                    com = new MySqlCommand(query, connectionReader2);
                    double quantityInStore = 0;
                    if (com.ExecuteScalar() != null)
                    {
                        quantityInStore = Convert.ToDouble(com.ExecuteScalar());
                    }
                    productQ = Convert.ToDouble(dr["Quantity"].ToString());
                    query = "select Storage_ID,Total_Meters from storage where Set_ID=" + dr["Data_ID"].ToString() + " and Store_ID=" + dr["Store_ID"].ToString() + "";
                    com = new MySqlCommand(query, connectionReader2);
                    MySqlDataReader dr2 = com.ExecuteReader();
                    while (dr2.Read())
                    {
                        if (productQ > 0)
                        {
                            storageQ = Convert.ToDouble(dr2["Total_Meters"].ToString());

                            if (storageQ > productQ)
                            {
                                id = Convert.ToInt32(dr2["Storage_ID"].ToString());
                                q = "update storage set Total_Meters=" + (storageQ - productQ) + " where Storage_ID=" + id;
                                MySqlCommand comm = new MySqlCommand(q, dbconnection);
                                comm.ExecuteNonQuery();
                                productQ = 0;
                                break;
                            }
                            else
                            {
                                id = Convert.ToInt32(dr2["Storage_ID"].ToString());
                                q = "update storage set Total_Meters=" + 0 + " where Storage_ID=" + id;
                                MySqlCommand comm = new MySqlCommand(q, dbconnection);
                                comm.ExecuteNonQuery();
                                productQ -= storageQ;
                            }
                        }
                        else
                        {
                            break;
                        }
                    }
                    dr2.Close();
                    #endregion
                }

                else if (dr["Type"].ToString() == "عرض")
                {
                    #region عرض
                    //and Store_ID=" + dr["Store_ID"].ToString() + "  ... Store_ID,
                    query = "select sum(Total_Meters) from storage where Offer_ID=" + dr["Data_ID"].ToString() + "  GROUP BY Offer_ID";
                    com = new MySqlCommand(query, connectionReader2);
                    double quantityInStore = 0;
                    if (com.ExecuteScalar() != null)
                    {
                        quantityInStore = Convert.ToDouble(com.ExecuteScalar());
                    }
                    productQ = Convert.ToDouble(dr["Quantity"].ToString());
                    // + " and Store_ID=" + dr["Store_ID"].ToString()
                    query = "select Storage_ID,Total_Meters from storage where Offer_ID=" + dr["Data_ID"].ToString();
                    com = new MySqlCommand(query, connectionReader2);
                    MySqlDataReader dr2 = com.ExecuteReader();
                    while (dr2.Read())
                    {
                        if (productQ > 0)
                        {
                            storageQ = Convert.ToDouble(dr2["Total_Meters"].ToString());

                            if (storageQ > productQ)
                            {
                                id = Convert.ToInt32(dr2["Storage_ID"].ToString());
                                q = "update storage set Total_Meters=" + (storageQ - productQ) + " where Storage_ID=" + id;
                                MySqlCommand comm = new MySqlCommand(q, dbconnection);
                                comm.ExecuteNonQuery();
                                productQ = 0;
                                break;
                            }
                            else
                            {
                                id = Convert.ToInt32(dr2["Storage_ID"].ToString());
                                q = "update storage set Total_Meters=" + 0 + " where Storage_ID=" + id;
                                MySqlCommand comm = new MySqlCommand(q, dbconnection);
                                comm.ExecuteNonQuery();
                                productQ -= storageQ;
                            }
                        }
                        else
                        {
                            break;
                        }
                    }
                    dr2.Close();
                    #endregion
                }
            }

            dr.Close();

            #endregion
            connectionReader2.Close();
            connectionReader1.Close();
            connectionReader.Close();
        }

        void printBill()
        {
            List<Bill_Items> bi = new List<Bill_Items>();

            //for (int i = 0; i < gridView1.RowCount; i++)
            //{
            //    Bill_Items item = new Bill_Items() { Code = gridView1.GetRowCellDisplayText(i, gridView1.Columns["الكود"]), Product_Type = gridView1.GetRowCellDisplayText(i, gridView1.Columns["النوع"]), Product_Name = gridView1.GetRowCellDisplayText(i, gridView1.Columns["الاسم"]), Color = gridView1.GetRowCellDisplayText(i, gridView1.Columns["اللون"]), Size = gridView1.GetRowCellDisplayText(i, gridView1.Columns["المقاس"]), Sort = gridView1.GetRowCellDisplayText(i, gridView1.Columns["الفرز"]), Quantity = Convert.ToDouble(gridView1.GetRowCellDisplayText(i, gridView1.Columns["الكمية"])), Cost = Convert.ToDouble(gridView1.GetRowCellDisplayText(i, gridView1.Columns["السعر"])), Total_Cost = Convert.ToDouble(gridView1.GetRowCellDisplayText(i, gridView1.Columns["الاجمالى"])), Store_Name = gridView1.GetRowCellDisplayText(i, gridView1.Columns["المخزن"]) };
            //    bi.Add(item);
            //}
            dbconnection.Open();
            string query = "SELECT product_bill.Data_ID,product_bill.Type,product_bill.Price,product_bill.Discount,product_bill.PriceAD,product_bill.Quantity,product_bill.Store_Name,product_bill.Cartons FROM product_bill where product_bill.CustomerBill_ID=" + ID;
            MySqlCommand com = new MySqlCommand(query, dbconnection);
            MySqlDataReader dr = com.ExecuteReader();
            while (dr.Read())
            {
                Bill_Items item;
                connectionReader3.Open();
                if (dr["Type"].ToString() == "بند")
                {
                    string q = "SELECT data.Code,type.Type_Name,concat(product.Product_Name,' ',COALESCE(color.Color_Name,''),' ',data.Description,' ',groupo.Group_Name,' ',factory.Factory_Name,' ',COALESCE(size.Size_Value,''),' ',COALESCE(sort.Sort_Value,'')) as 'Product_Name' FROM data INNER JOIN type ON data.Type_ID = type.Type_ID INNER JOIN product ON product.Product_ID = data.Product_ID INNER JOIN factory ON factory.Factory_ID = data.Factory_ID INNER JOIN groupo ON groupo.Group_ID = data.Group_ID LEFT JOIN color ON data.Color_ID = color.Color_ID LEFT JOIN size ON data.Size_ID = size.Size_ID LEFT JOIN sort ON data.Sort_ID = sort.Sort_ID where Data_ID=" + dr["Data_ID"].ToString();
                    MySqlCommand c = new MySqlCommand(q, connectionReader3);
                    MySqlDataReader dr1 = c.ExecuteReader();
                    while (dr1.Read())
                    {
                        item = new Bill_Items() { Code = dr1["Code"].ToString(), Type = dr1["Type_Name"].ToString(), Product_Type = "بند", Product_Name = dr1["Product_Name"].ToString(), Quantity = Convert.ToDouble(dr["Quantity"].ToString()), Cost = Convert.ToDouble(dr["Price"].ToString()), Total_Cost = Convert.ToDouble(dr["Price"].ToString()) * Convert.ToDouble(dr["Quantity"].ToString()), Discount = Convert.ToDouble(dr["Discount"].ToString()), Store_Name = dr["Store_Name"].ToString(), Carton = Convert.ToInt32(dr["Cartons"].ToString()) };
                        bi.Add(item);
                    }
                    dr1.Close();
                }
                else if(dr["Type"].ToString() == "طقم")
                {
                    string q = "SELECT sets.Set_ID,sets.Set_Name FROM sets where Set_ID=" + dr["Data_ID"].ToString();
                    MySqlCommand c = new MySqlCommand(q, connectionReader3);
                    MySqlDataReader dr1 = c.ExecuteReader();
                    while (dr1.Read())
                    {
                        item = new Bill_Items() { Code = dr1["Set_ID"].ToString(), Product_Type = "طقم", Product_Name = dr1["Set_Name"].ToString(), Quantity = Convert.ToDouble(dr["Quantity"].ToString()), Cost = Convert.ToDouble(dr["Price"].ToString()), Total_Cost = Convert.ToDouble(dr["Price"].ToString()) * Convert.ToDouble(dr["Quantity"].ToString()), Discount = Convert.ToDouble(dr["Discount"].ToString()), Store_Name = dr["Store_Name"].ToString(), Carton = Convert.ToInt32(dr["Cartons"].ToString()) };
                        bi.Add(item);
                    }
                    dr1.Close();
                }
                else if (dr["Type"].ToString() == "عرض")
                {
                    connectionReader5.Open();
                    string q = "SELECT offer.Offer_ID,offer.Offer_Name,offer.Description FROM offer where Offer_ID=" + dr["Data_ID"].ToString();
                    MySqlCommand c = new MySqlCommand(q, connectionReader3);
                    MySqlDataReader dr1 = c.ExecuteReader();
                    while (dr1.Read())
                    {
                        string itemName = "concat(product.Product_Name,' ',type.Type_Name,' ',factory.Factory_Name,' ',groupo.Group_Name,' ' ,COALESCE(color.Color_Name,''),' ',COALESCE(size.Size_Value,''),' ',COALESCE(sort.Sort_Value,''),' ',COALESCE(data.Classification,''),' ',COALESCE(data.Description,''))as 'البند'";
                        string DataTableRelations = "INNER JOIN type ON type.Type_ID = data.Type_ID INNER JOIN product ON product.Product_ID = data.Product_ID INNER JOIN factory ON data.Factory_ID = factory.Factory_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID LEFT outer JOIN color ON data.Color_ID = color.Color_ID LEFT outer  JOIN size ON data.Size_ID = size.Size_ID LEFT outer  JOIN sort ON data.Sort_ID = sort.Sort_ID";
                        string query3 = "select Code as 'الكود'," + itemName + " from offer inner join offer_details on offer.Offer_ID=offer_details.Offer_ID inner join data on data.Data_ID=offer_details.Data_ID " + DataTableRelations + "  where offer.Offer_ID=" + dr1["Offer_ID"];
                        MySqlCommand com3 = new MySqlCommand(query3, connectionReader5);
                        MySqlDataReader dr3 = com3.ExecuteReader();
                        string str = "";
                        int cont = 1;
                        while (dr3.Read())
                        {
                            str += cont + "-" + dr3[1].ToString() + "\n";
                            cont++;
                        }
                        dr3.Close();
                        item = new Bill_Items() { Code = dr1["Offer_ID"].ToString(), Type = dr1["Description"].ToString(), Product_Type = "عرض"/*, Product_Name = dr1["Offer_Name"].ToString()*/, Quantity = Convert.ToDouble(dr["Quantity"].ToString()), Cost = Convert.ToDouble(dr["Price"].ToString()), Total_Cost = Convert.ToDouble(dr["Price"].ToString()) * Convert.ToDouble(dr["Quantity"].ToString()), Discount = 0, Store_Name = dr["Store_Name"].ToString(), Carton = Convert.ToInt32(dr["Cartons"].ToString()) };
                        item.Product_Name = "-" + dr1["Offer_Name"].ToString() + "\n" + str;
                        bi.Add(item);
                    }
                    dr1.Close();

                    connectionReader5.Close();
                }
                connectionReader3.Close();
            }
            dbconnection.Close();

            Print_Bill_Report f = new Print_Bill_Report();
            if (clientID > 0 && customerID > 0)
            {
                f.PrintInvoice(clientName + " " + clientID, engName + " " + customerID, clientPhoneNumber, delegateName + " - " /*+ "("*/ + TypeBuy /*+ ")"*/, billDate, TypeBuy, billNumber, cmbBranch.SelectedValue.ToString(), branchName, totalCostBD, Convert.ToDouble(txtTotalCost.Text), totalDiscount, bi);
            }
            else if (clientID > 0)
            {
                f.PrintInvoice(clientName + " " + clientID, "", clientPhoneNumber, delegateName + " - " /*+ "("*/ + TypeBuy /*+ ")"*/, billDate, TypeBuy, billNumber, cmbBranch.SelectedValue.ToString(), branchName, totalCostBD, Convert.ToDouble(txtTotalCost.Text), totalDiscount, bi);
            }
            else if (customerID > 0)
            {
                f.PrintInvoice(engName + " " + customerID, "", customerPhoneNumber, delegateName + " - " /*+ "("*/ + TypeBuy /*+ ")"*/, billDate, TypeBuy, billNumber, cmbBranch.SelectedValue.ToString(), branchName, totalCostBD, Convert.ToDouble(txtTotalCost.Text), totalDiscount, bi);
            }
            f.ShowDialog();
        }

        void printBillAccounting()
        {
            List<Bill_ItemsAccounting> bi = new List<Bill_ItemsAccounting>();
            
            dbconnection.Open();
            string query = "SELECT product_bill.Data_ID,product_bill.Type,product_bill.Price,product_bill.Discount,product_bill.PriceAD,product_bill.Quantity,product_bill.Store_Name,product_bill.Cartons FROM product_bill where product_bill.CustomerBill_ID=" + ID;
            MySqlCommand com = new MySqlCommand(query, dbconnection);
            MySqlDataReader dr = com.ExecuteReader();
            while (dr.Read())
            {
                Bill_ItemsAccounting item;
                connectionReader3.Open();
                if (dr["Type"].ToString() == "بند")
                {
                    string q = "SELECT data.Code,type.Type_Name,concat(product.Product_Name,' ',COALESCE(color.Color_Name,''),' ',data.Description,' ',groupo.Group_Name,' ',factory.Factory_Name,' ',COALESCE(size.Size_Value,''),' ',COALESCE(sort.Sort_Value,'')) as 'Product_Name' FROM data INNER JOIN type ON data.Type_ID = type.Type_ID INNER JOIN product ON product.Product_ID = data.Product_ID INNER JOIN factory ON factory.Factory_ID = data.Factory_ID INNER JOIN groupo ON groupo.Group_ID = data.Group_ID LEFT JOIN color ON data.Color_ID = color.Color_ID LEFT JOIN size ON data.Size_ID = size.Size_ID LEFT JOIN sort ON data.Sort_ID = sort.Sort_ID where Data_ID=" + dr["Data_ID"].ToString();
                    MySqlCommand c = new MySqlCommand(q, connectionReader3);
                    MySqlDataReader dr1 = c.ExecuteReader();
                    while (dr1.Read())
                    {
                        item = new Bill_ItemsAccounting() { Code = dr1["Code"].ToString(), Type = dr1["Type_Name"].ToString(), Product_Type = "بند", Product_Name = dr1["Product_Name"].ToString(), Quantity = Convert.ToDouble(dr["Quantity"].ToString()), Cost = Convert.ToDouble(dr["Price"].ToString()), Total_Cost = Convert.ToDouble(dr["Price"].ToString()) * Convert.ToDouble(dr["Quantity"].ToString()), Discount = Convert.ToDouble(dr["Discount"].ToString()), Store_Name = dr["Store_Name"].ToString(), Carton = Convert.ToInt32(dr["Cartons"].ToString()) };
                        bi.Add(item);
                    }
                    dr1.Close();
                }
                else if (dr["Type"].ToString() == "طقم")
                {
                    string q = "SELECT sets.Set_ID,sets.Set_Name FROM sets where Set_ID=" + dr["Data_ID"].ToString();
                    MySqlCommand c = new MySqlCommand(q, connectionReader3);
                    MySqlDataReader dr1 = c.ExecuteReader();
                    while (dr1.Read())
                    {
                        item = new Bill_ItemsAccounting() { Code = dr1["Set_ID"].ToString(), Product_Type = "طقم", Product_Name = dr1["Set_Name"].ToString(), Quantity = Convert.ToDouble(dr["Quantity"].ToString()), Cost = Convert.ToDouble(dr["Price"].ToString()), Total_Cost = Convert.ToDouble(dr["Price"].ToString()) * Convert.ToDouble(dr["Quantity"].ToString()), Discount = Convert.ToDouble(dr["Discount"].ToString()), Store_Name = dr["Store_Name"].ToString(), Carton = Convert.ToInt32(dr["Cartons"].ToString()) };
                        bi.Add(item);
                    }
                    dr1.Close();
                }
                else if (dr["Type"].ToString() == "عرض")
                {
                    string q = "SELECT offer.Offer_ID,offer.Offer_Name FROM offer where Offer_ID=" + dr["Data_ID"].ToString();
                    MySqlCommand c = new MySqlCommand(q, connectionReader3);
                    MySqlDataReader dr1 = c.ExecuteReader();
                    while (dr1.Read())
                    {
                        item = new Bill_ItemsAccounting() { Code = dr1["Offer_ID"].ToString(), Product_Type = "عرض", Product_Name = dr1["Offer_Name"].ToString(), Quantity = Convert.ToDouble(dr["Quantity"].ToString()), Cost = Convert.ToDouble(dr["Price"].ToString()), Total_Cost = Convert.ToDouble(dr["Price"].ToString()) * Convert.ToDouble(dr["Quantity"].ToString()), Discount = 0, Store_Name = dr["Store_Name"].ToString(), Carton = Convert.ToInt32(dr["Cartons"].ToString()) };
                        bi.Add(item);
                    }
                    dr1.Close();
                }
                connectionReader3.Close();
            }
            dbconnection.Close();

            Print_Bill_ReportAccounting f = new Print_Bill_ReportAccounting();
            if (clientID > 0 && customerID > 0)
            {
                f.PrintInvoice(clientName + " " + clientID, engName + " " + customerID, clientPhoneNumber, delegateName + " - " /*+ "("*/ + TypeBuy /*+ ")"*/, billDate, TypeBuy, billNumber, cmbBranch.SelectedValue.ToString(), branchName, totalCostBD, Convert.ToDouble(txtTotalCost.Text), totalDiscount, bi);
            }
            else if (clientID > 0)
            {
                f.PrintInvoice(clientName + " " + clientID, "", clientPhoneNumber, delegateName + " - " /*+ "("*/ + TypeBuy /*+ ")"*/, billDate, TypeBuy, billNumber, cmbBranch.SelectedValue.ToString(), branchName, totalCostBD, Convert.ToDouble(txtTotalCost.Text), totalDiscount, bi);
            }
            else if (customerID > 0)
            {
                f.PrintInvoice(engName + " " + customerID, "", customerPhoneNumber, delegateName + " - " /*+ "("*/ + TypeBuy /*+ ")"*/, billDate, TypeBuy, billNumber, cmbBranch.SelectedValue.ToString(), branchName, totalCostBD, Convert.ToDouble(txtTotalCost.Text), totalDiscount, bi);
            }
            f.ShowDialog();
        }

        void printCategoriesBill()
        {
            if (TypeBuy == "كاش")
            {
                Print_CategoriesBill_Report f = new Print_CategoriesBill_Report();
                if (clientID > 0)
                {
                    f.PrintInvoice(DateTime.Now, TransitionID, UserControl.EmpBranchName, branchName, billNumber, clientName + " " + clientID, billDate, Convert.ToDouble(txtPaidMoney.Text), PaymentMethod, cmbBank.Text, txtCheckNumber.Text, dateEdit1.Text, txtOperationNumber.Text, txtDescrip.Text, ConfirmEmp, UserControl.EmpName, arrPaidMoney[0], arrPaidMoney[1], arrPaidMoney[2], arrPaidMoney[3], arrPaidMoney[4], arrPaidMoney[5], arrPaidMoney[6], arrPaidMoney[7], arrPaidMoney[8], arrRestMoney[0], arrRestMoney[1], arrRestMoney[2], arrRestMoney[3], arrRestMoney[4], arrRestMoney[5], arrRestMoney[6], arrRestMoney[7], arrRestMoney[8]);
                }
                else if (customerID > 0)
                {
                    f.PrintInvoice(DateTime.Now, TransitionID, UserControl.EmpBranchName, branchName, billNumber, engName + " " + customerID, billDate, Convert.ToDouble(txtPaidMoney.Text), PaymentMethod, cmbBank.Text, txtCheckNumber.Text, dateEdit1.Text, txtOperationNumber.Text, txtDescrip.Text, ConfirmEmp, UserControl.EmpName, arrPaidMoney[0], arrPaidMoney[1], arrPaidMoney[2], arrPaidMoney[3], arrPaidMoney[4], arrPaidMoney[5], arrPaidMoney[6], arrPaidMoney[7], arrPaidMoney[8], arrRestMoney[0], arrRestMoney[1], arrRestMoney[2], arrRestMoney[3], arrRestMoney[4], arrRestMoney[5], arrRestMoney[6], arrRestMoney[7], arrRestMoney[8]);
                }
                f.ShowDialog();
                for (int i = 0; i < arrPaidMoney.Length; i++)
                    arrPaidMoney[i] = arrRestMoney[i] = 0;
                for (int i = 0; i < arrRestMoney.Length; i++)
                    arrRestMoney[i] = arrPaidMoney[i] = 0;
            }
            else if (TypeBuy == "آجل")
            {
                Print_AglCategoriesBill_Report f = new Print_AglCategoriesBill_Report();
                if (clientID > 0)
                {
                    f.PrintInvoice(DateTime.Now, TransitionID, branchName, clientName + " " + clientID, Convert.ToDouble(txtPaidMoney.Text), PaymentMethod, cmbBank.Text, txtCheckNumber.Text, dateEdit1.Text, txtOperationNumber.Text, txtDescrip.Text, UserControl.EmpName, delegateName, arrPaidMoney[0], arrPaidMoney[1], arrPaidMoney[2], arrPaidMoney[3], arrPaidMoney[4], arrPaidMoney[5], arrPaidMoney[6], arrPaidMoney[7], arrPaidMoney[8], arrRestMoney[0], arrRestMoney[1], arrRestMoney[2], arrRestMoney[3], arrRestMoney[4], arrRestMoney[5], arrRestMoney[6], arrRestMoney[7], arrRestMoney[8]);
                }
                else if (customerID > 0)
                {
                    f.PrintInvoice(DateTime.Now, TransitionID, branchName, engName + " " + customerID, Convert.ToDouble(txtPaidMoney.Text), PaymentMethod, cmbBank.Text, txtCheckNumber.Text, dateEdit1.Text, txtOperationNumber.Text, txtDescrip.Text, UserControl.EmpName, delegateName, arrPaidMoney[0], arrPaidMoney[1], arrPaidMoney[2], arrPaidMoney[3], arrPaidMoney[4], arrPaidMoney[5], arrPaidMoney[6], arrPaidMoney[7], arrPaidMoney[8], arrRestMoney[0], arrRestMoney[1], arrRestMoney[2], arrRestMoney[3], arrRestMoney[4], arrRestMoney[5], arrRestMoney[6], arrRestMoney[7], arrRestMoney[8]);
                }
                f.ShowDialog();
                for (int i = 0; i < arrPaidMoney.Length; i++)
                    arrPaidMoney[i] = arrRestMoney[i] = 0;
                for (int i = 0; i < arrRestMoney.Length; i++)
                    arrRestMoney[i] = arrPaidMoney[i] = 0;
            }
        }
    }
}

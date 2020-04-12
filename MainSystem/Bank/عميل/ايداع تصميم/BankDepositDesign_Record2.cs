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
    public partial class BankDepositDesign_Record2 : Form
    {
        MySqlConnection dbconnection;
        string PaymentMethod = "";
        int[] arrOFPhaat; //count of each catagory value of money in store
        int[] arrRestMoney;
        int[] arrPaidMoney;
        bool loaded = false;
        bool loadedPayType = false;
        string TransitionID = "";
        bool flagCategoriesSuccess = false;
        int transitionbranchID = 0;
        List<DesignItem> listDesignItems;

        public BankDepositDesign_Record2()//BankDepositAgl_Report form, XtraTabControl MainTabControlBank)
        {
            InitializeComponent();
            dbconnection = new MySqlConnection(connection.connectionString);
            // tabControlBank = MainTabControlBank;
            arrOFPhaat = new int[9];
            arrPaidMoney = new int[9];
            arrRestMoney = new int[9];
            listDesignItems = new List<DesignItem>();
            if (UserControl.userType == 1)
            {
                cmbBank.AutoCompleteMode = AutoCompleteMode.Suggest;
                cmbBank.AutoCompleteSource = AutoCompleteSource.ListItems;
            }
            else
            {
                cmbBank.Enabled = false;
                cmbBank.DropDownStyle = ComboBoxStyle.DropDownList;
            }
            comClient.AutoCompleteMode = AutoCompleteMode.Suggest;
            comClient.AutoCompleteSource = AutoCompleteSource.ListItems;
            comEng.AutoCompleteMode = AutoCompleteMode.Suggest;
            comEng.AutoCompleteSource = AutoCompleteSource.ListItems;

            this.dateEdit1.Properties.DisplayFormat.FormatString = "yyyy/MM/dd";
            this.dateEdit1.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dateEdit1.Properties.EditFormat.FormatString = "yyyy/MM/dd";
            this.dateEdit1.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dateEdit1.Properties.Mask.EditMask = "yyyy/MM/dd";
        }

        private void BankDepositAgl_Record_Load(object sender, EventArgs e)
        {
            try
            {
                transitionbranchID = UserControl.EmpBranchID;
                //branchID = UserControl.UserBranch(dbconnection);
                dbconnection.Open();
                //string query = "select Branch_Name from branch where Branch_ID=" + transitionbranchID;
                //MySqlCommand com = new MySqlCommand(query, dbconnection);
                //branchName = com.ExecuteScalar().ToString();

                string query = "select * from delegate where Branch_ID=" + transitionbranchID;
                MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                DataTable dt = new DataTable();
                da.Fill(dt);
                comDelegate.DataSource = dt;
                comDelegate.DisplayMember = dt.Columns["Delegate_Name"].ToString();
                comDelegate.ValueMember = dt.Columns["Delegate_ID"].ToString();
                comDelegate.Text = "";

                query = "select * from employee where Department_ID=26";
                da = new MySqlDataAdapter(query, dbconnection);
                dt = new DataTable();
                da.Fill(dt);
                comEngDesign.DataSource = dt;
                comEngDesign.DisplayMember = dt.Columns["Employee_Name"].ToString();
                comEngDesign.ValueMember = dt.Columns["Employee_ID"].ToString();
                comEngDesign.Text = "";

                panelContent.Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }

        //when select customer(مهندس,مقاول)display in comCustomer the all clients of th customer 
        private void comCustomer_SelectedValueChanged(object sender, EventArgs e)
        {
            if (loaded)
            {
                try
                {
                    txtCustomerID.Text = comEng.SelectedValue.ToString();
                    loaded = false;
                    string query = "select * from customer where Customer_ID in(select Client_ID from custmer_client where Customer_ID=" + comEng.SelectedValue.ToString() + ")";
                    MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    comClient.DataSource = dt;
                    comClient.DisplayMember = dt.Columns["Customer_Name"].ToString();
                    comClient.ValueMember = dt.Columns["Customer_ID"].ToString();
                    comClient.Text = "";
                    comClient.SelectedIndex = -1;
                    txtClientID.Text = "";
                    txtClientID.Enabled = true;
                    comClient.Enabled = true;
                    loaded = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void comClient_SelectedValueChanged(object sender, EventArgs e)
        {
            if (loaded)
            {
                try
                {
                    txtClientID.Text = comClient.SelectedValue.ToString();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void txtBox_KeyDown(object sender, KeyEventArgs e)
        {
            TextBox txtBox = (TextBox)sender;
            string query;
            MySqlCommand com;
            string Name;
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    if (txtBox.Text != "")
                    {
                        dbconnection.Open();
                        switch (txtBox.Name)
                        {
                            case "txtClientID":
                                query = "select Customer_Name from customer where Customer_ID=" + txtClientID.Text + " and Customer_Type='عميل'";
                                com = new MySqlCommand(query, dbconnection);
                                if (com.ExecuteScalar() != null)
                                {
                                    Name = (string)com.ExecuteScalar();
                                    comClient.Text = Name;
                                    comClient.SelectedValue = txtClientID.Text;
                                }
                                else
                                {
                                    comClient.Text = "";
                                    MessageBox.Show("there is no item with this id");
                                    dbconnection.Close();
                                    return;
                                }
                                break;
                            case "txtCustomerID":
                                query = "select Customer_Name from customer where Customer_ID=" + txtCustomerID.Text + " and Customer_Type<>'عميل'";
                                com = new MySqlCommand(query, dbconnection);
                                if (com.ExecuteScalar() != null)
                                {
                                    Name = (string)com.ExecuteScalar();
                                    comEng.Text = Name;
                                    comEng.SelectedValue = txtCustomerID.Text;
                                }
                                else
                                {
                                    comEng.Text = "";
                                    MessageBox.Show("there is no item with this id");
                                    dbconnection.Close();
                                    return;
                                }
                                break;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                dbconnection.Close();
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
                labType.Text = "خزينة";
                panelContent.Visible = false;
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

                string query = "select * from bank inner join bank_main on bank.MainBank_ID=bank_main.MainBank_ID where Branch_ID=" + transitionbranchID + " and MainBank_Type='خزينة'";
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
                
                txtCheckNumber.Visible = false;
                labDate.Visible = false;
                dateEdit1.Visible = false;
                labelchekNumber.Visible = false;
                labelOperationNumber.Visible = false;
                txtOperationNumber.Visible = false;

                cmbBank.Visible = true;
                labType.Visible = true;
                txtPaidMoney.Visible = true;
                labelPaidMoney.Visible = true;
                txtDescrip.Visible = true;
                labelDescrip.Visible = true;
                loadedPayType = true;
                groupBox1.Enabled = true;
                panelContent.Visible = true;
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
           
                txtCheckNumber.Visible = true;
                labDate.Text = "تاريخ الاستحقاق";
                labDate.Visible = true;
                dateEdit1.Visible = true;
                labelchekNumber.Text = "رقم الشيك";
                labelchekNumber.Visible = true;
                groupBox1.Enabled = false;
                cmbBank.Visible = true;
                labType.Visible = true;
                txtPaidMoney.Visible = true;
                labelPaidMoney.Visible = true;
                txtDescrip.Visible = true;
                labelDescrip.Visible = true;
                loadedPayType = true;
                labelOperationNumber.Visible = false;
                txtOperationNumber.Visible = false;
                panelContent.Visible = true;
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

                cmbBank.Enabled = true;
                cmbBank.AutoCompleteSource = AutoCompleteSource.ListItems;
                cmbBank.AutoCompleteMode = AutoCompleteMode.Suggest;

                ///////////////////////
                panelContent.Visible = false;
                labType.Text = "بنك";

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
                labelOperationNumber.Visible = false;
                txtOperationNumber.Visible = false;
                labDate.Text = "تاريخ الايداع";
                labelchekNumber.Visible = false;
                txtCheckNumber.Visible = false;

                string query = "select concat(MainBank_Name,' ',Bank_Name) as Bank_Name,Bank_ID from bank inner join bank_main on bank.MainBank_ID=bank_main.MainBank_ID where MainBank_Type = 'حساب بنكى'";
                MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                DataTable dt = new DataTable();
                da.Fill(dt);
                cmbBank.DataSource = dt;
                cmbBank.DisplayMember = dt.Columns["Bank_Name"].ToString();
                cmbBank.ValueMember = dt.Columns["Bank_ID"].ToString();
                cmbBank.SelectedIndex = -1;


                panelContent.Visible = true;
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
                txtOperationNumber.Visible = true;
                labelOperationNumber.Visible = true;
                
                string query = "select concat(MainBank_Name,' ',Bank_Name,' - ',Machine_ID) as Machine_ID,Visa_ID from bank_visa inner join bank on bank_visa.Bank_ID=bank.Bank_ID inner join bank_main on bank.MainBank_ID=bank_main.MainBank_ID where MainBank_Type = 'حساب بنكى'";
                MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                DataTable dt = new DataTable();
                da.Fill(dt);
                cmbBank.DataSource = dt;
                cmbBank.DisplayMember = dt.Columns["Machine_ID"].ToString();
                cmbBank.ValueMember = dt.Columns["Visa_ID"].ToString();
                cmbBank.SelectedIndex = -1;


                panelContent.Visible = true;
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
                    check = (/*(radioAgel.Checked || radioCash.Checked) && */comDelegate.Text != "" && comEngDesign.Text != "" && (comClient.Text != "" || comEng.Text != "")/*&& txtRestMoney.Text != "" && cmbBranch.Text != ""*/ && cmbBank.Text != "" && txtPaidMoney.Text != "");
                }
                else if (PaymentMethod == "شيك")
                {
                    check = (/*(radioAgel.Checked || radioCash.Checked) && */comDelegate.Text != "" && comEngDesign.Text != "" && (comClient.Text != "" || comEng.Text != "") /*&& txtRestMoney.Text != "" && cmbBranch.Text != ""*/ && cmbBank.Text != "" && txtPaidMoney.Text != "" && dateEdit1.Text != "" && txtCheckNumber.Text != "");
                }
                else if (PaymentMethod == "حساب بنكى")
                {
                    check = (/*(radioAgel.Checked || radioCash.Checked) && */comDelegate.Text != "" && comEngDesign.Text != "" && (comClient.Text != "" || comEng.Text != "") /*&& txtRestMoney.Text != "" && cmbBranch.Text != ""*/ && cmbBank.Text != "" && txtPaidMoney.Text != "" && dateEdit1.Text != "" && txtCheckNumber.Text != "");
                }
                else if (PaymentMethod == "فيزا")
                {
                    check = (/*(radioAgel.Checked || radioCash.Checked) && */comDelegate.Text != "" && comEngDesign.Text != "" && (comClient.Text != "" || comEng.Text != "") /*&& txtRestMoney.Text != "" && cmbBranch.Text != ""*/ && cmbBank.Text != "" && txtPaidMoney.Text != "" && txtOperationNumber.Text != "");
                }

                if (check)
                {
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

                        int customerDesignID = addCustomerDesign();
                        //,Type
                        string query = "insert into transitions (TransitionBranch_ID,TransitionBranch_Name,Branch_ID,Branch_Name,Bill_Number,Client_ID,Client_Name,Customer_ID,Customer_Name,Transition,Type,Payment_Method,Bank_ID,Bank_Name,Date,Amount,Data,PayDay,Check_Number,Operation_Number,Error,Employee_ID,Employee_Name,Delegate_ID,Delegate_Name) values(@TransitionBranch_ID,@TransitionBranch_Name,@Branch_ID,@Branch_Name,@Bill_Number,@Client_ID,@Client_Name,@Customer_ID,@Customer_Name,@Transition,@Type,@Payment_Method,@Bank_ID,@Bank_Name,@Date,@Amount,@Data,@PayDay,@Check_Number,@Operation_Number,@Error,@Employee_ID,@Employee_Name,@Delegate_ID,@Delegate_Name)";
                        MySqlCommand com = new MySqlCommand(query, dbconnection);

                        com.Parameters.Add("@Transition", MySqlDbType.VarChar, 255).Value = "ايداع تصميم";
                        com.Parameters.Add("@Type", MySqlDbType.VarChar, 255).Value = "كاش";
                        //if (radioAgel.Checked)
                        //{
                        //    com.Parameters.Add("@Type", MySqlDbType.VarChar, 255).Value = "آجل";
                        //}
                        //else if (radioCash.Checked)
                        //{
                        //    com.Parameters.Add("@Type", MySqlDbType.VarChar, 255).Value = "كاش";
                        //}
                        //else
                        //{
                        //    MessageBox.Show("يجب اختيار طريقة الدفع");
                        //    dbconnection.Close();
                        //    return;
                        //}
                        com.Parameters.Add("@TransitionBranch_ID", MySqlDbType.Int16, 11).Value = transitionbranchID;
                        com.Parameters.Add("@TransitionBranch_Name", MySqlDbType.VarChar, 255).Value = UserControl.EmpBranchName;
                        com.Parameters.Add("@Branch_ID", MySqlDbType.Int16, 11).Value = transitionbranchID;
                        com.Parameters.Add("@Branch_Name", MySqlDbType.VarChar, 255).Value = UserControl.EmpBranchName;
                        com.Parameters.Add("@Bill_Number", MySqlDbType.Int16, 11).Value = customerDesignID;
                        com.Parameters.Add("@Payment_Method", MySqlDbType.VarChar, 255).Value = PaymentMethod;
                        com.Parameters.Add("@Bank_ID", MySqlDbType.Int16, 11).Value = cmbBank.SelectedValue;
                        com.Parameters.Add("@Bank_Name", MySqlDbType.VarChar, 255).Value = cmbBank.Text;
                        com.Parameters.Add("@Date", MySqlDbType.DateTime, 0).Value = DateTime.Now;
                        if (comClient.Text != "")
                        {
                            com.Parameters.Add("@Client_ID", MySqlDbType.Int16, 11).Value = comClient.SelectedValue;
                            com.Parameters.Add("@Client_Name", MySqlDbType.VarChar, 255).Value = comClient.Text;
                        }
                        else
                        {
                            com.Parameters.Add("@Client_ID", MySqlDbType.Int16, 11).Value = null;
                            com.Parameters.Add("@Client_Name", MySqlDbType.VarChar, 255).Value = null;
                        }
                        if (comEng.Text != "")
                        {
                            com.Parameters.Add("@Customer_ID", MySqlDbType.Int16, 11).Value = comEng.SelectedValue;
                            com.Parameters.Add("@Customer_Name", MySqlDbType.VarChar, 255).Value = comEng.Text;
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
                        MySqlCommand com2 = new MySqlCommand("select Bank_Stock from bank where Bank_ID=" + cmbBank.SelectedValue, dbconnection);
                        double amount2 = Convert.ToDouble(com2.ExecuteScalar().ToString());
                        amount2 += outParse;
                        MySqlCommand com3 = new MySqlCommand("update bank set Bank_Stock=" + amount2 + " where Bank_ID=" + cmbBank.SelectedValue, dbconnection);
                        com3.ExecuteNonQuery();

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
                        if (comDelegate.SelectedValue != null && comDelegate.Text != "")
                        {
                            com.Parameters.Add("@Delegate_ID", MySqlDbType.Int16, 11).Value = comDelegate.SelectedValue.ToString();
                            com.Parameters.Add("@Delegate_Name", MySqlDbType.VarChar, 255).Value = comDelegate.Text;
                        }
                        else
                        {
                            com.Parameters.Add("@Delegate_ID", MySqlDbType.Int16, 11).Value = null;
                            com.Parameters.Add("@Delegate_Name", MySqlDbType.VarChar, 255).Value = null;
                        }
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
                        
                        dbconnection.Close();

                        //IncreaseClientPaied();

                        //print bill
                        printCategoriesBill(customerDesignID);

                        clear();
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
        }

        private void radiotype_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton radio = (RadioButton)sender;
            string Customer_Type = radio.Text;
            try
            {
                loaded = false;
                if (Customer_Type == "عميل")
                {
                    string query = "select * from customer where Customer_Type='" + Customer_Type + "'";
                    MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    comClient.DataSource = dt;
                    comClient.DisplayMember = dt.Columns["Customer_Name"].ToString();
                    comClient.ValueMember = dt.Columns["Customer_ID"].ToString();
                    comClient.Text = "";
                    comEng.Text = "";
                    txtClientID.Text = "";
                    txtCustomerID.Text = "";
                    txtClientID.Enabled = true;
                    comClient.Enabled = true;
                    txtCustomerID.Enabled = false;
                    comEng.Enabled = false;
                }
                else
                {
                    string query = "select * from customer where Customer_Type='" + Customer_Type + "'";
                    MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    comEng.DataSource = dt;
                    comEng.DisplayMember = dt.Columns["Customer_Name"].ToString();
                    comEng.ValueMember = dt.Columns["Customer_ID"].ToString();
                    comEng.Text = "";
                    comClient.Text = "";
                    txtClientID.Text = "";
                    txtCustomerID.Text = "";
                    txtClientID.Enabled = false;
                    comClient.Enabled = false;
                    txtCustomerID.Enabled = true;
                    comEng.Enabled = true;
                }
                loaded = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }

        private void checkBox_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                CheckBox ch = (CheckBox)sender;
                switch (ch.Text)
                {
                    case "حمام":
                        if (ch.Checked)
                        {

                            txtSpace1.Enabled = true;
                            txtNoItem1.Enabled = true;
                            txtCost1.Enabled = true;
                            txtSpace1.Focus();
                            DesignItem designItem = new DesignItem();
                            designItem.DesignLocation = "حمام";
                            listDesignItems.Add(designItem);
                        }
                        else
                        {
                            txtSpace1.Enabled = false;
                            txtNoItem1.Enabled = false;
                            txtCost1.Enabled = false;
                            DesignItem designItem = getDesignItem("حمام");
                            listDesignItems.Remove(designItem);
                        }
                        break;
                    case "مطبخ":
                        if (ch.Checked)
                        {

                            txtSpace2.Enabled = true;
                            txtNoItem2.Enabled = true;
                            txtCost2.Enabled = true;
                            txtSpace2.Focus();
                            DesignItem designItem = new DesignItem();
                            designItem.DesignLocation = "مطبخ";
                            listDesignItems.Add(designItem);
                        }
                        else
                        {
                            txtSpace2.Enabled = false;
                            txtNoItem2.Enabled = false;
                            txtCost2.Enabled = false;
                            DesignItem designItem = getDesignItem("مطبخ");
                            listDesignItems.Remove(designItem);
                        }
                        break;
                    case "صالة":
                        if (ch.Checked)
                        {

                            txtSpace3.Enabled = true;
                            txtNoItem3.Enabled = true;
                            txtCost3.Enabled = true;
                            txtSpace3.Focus();
                            DesignItem designItem = new DesignItem();
                            designItem.DesignLocation = "صالة";
                            listDesignItems.Add(designItem);
                        }
                        else
                        {
                            txtSpace3.Enabled = false;
                            txtNoItem3.Enabled = false;
                            txtCost3.Enabled = false;
                            DesignItem designItem = getDesignItem("صالة");
                            listDesignItems.Remove(designItem);
                        }
                        break;
                    case "غرفة":
                        if (ch.Checked)
                        {

                            txtSpace4.Enabled = true;
                            txtNoItem4.Enabled = true;
                            txtCost4.Enabled = true;
                            txtSpace4.Focus();
                            DesignItem designItem = new DesignItem();
                            designItem.DesignLocation = "غرفة";
                            listDesignItems.Add(designItem);
                        }
                        else
                        {
                            txtSpace4.Enabled = false;
                            txtNoItem4.Enabled = false;
                            txtCost4.Enabled = false;
                            DesignItem designItem = getDesignItem("غرفة");
                            listDesignItems.Remove(designItem);
                        }
                        break;
                    case "اخري":
                        if (ch.Checked)
                        {

                            txtSpace5.Enabled = true;
                            txtNoItem5.Enabled = true;
                            txtCost5.Enabled = true;
                            txtSpace5.Focus();
                            DesignItem designItem = new DesignItem();
                            designItem.DesignLocation = "اخري";
                            listDesignItems.Add(designItem);
                        }
                        else
                        {
                            txtSpace5.Enabled = true;
                            txtNoItem5.Enabled = true;
                            txtCost5.Enabled = true;
                            DesignItem designItem = getDesignItem("اخري");
                            listDesignItems.Remove(designItem);
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txtSpace_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    double re = 0;
                    TextBox txtbox = (TextBox)sender;
                    switch (txtbox.Name)
                    {
                        case "txtSpace1":

                            if (!double.TryParse(txtSpace1.Text, out re))
                            {
                                MessageBox.Show("ادخل قيمة صحيحة");

                                txtSpace1.Text = "";
                                txtSpace1.Focus();
                            }
                            else
                            {
                                DesignItem designItem = getDesignItem("حمام");
                                designItem.Space = re;
                                txtNoItem1.Focus();

                            }
                            break;
                        case "txtSpace2":
                            re = 0;
                            if (!double.TryParse(txtSpace2.Text, out re))
                            {
                                MessageBox.Show("ادخل قيمة صحيحة");

                                txtSpace2.Text = "";
                                txtSpace2.Focus();
                            }
                            else
                            {
                                DesignItem designItem = getDesignItem("مطبخ");
                                designItem.Space = re;
                                txtNoItem2.Focus();
                            
                            }
                            break;
                        case "txtSpace3":
                            re = 0;
                            if (!double.TryParse(txtSpace3.Text, out re))
                            {
                                MessageBox.Show("ادخل قيمة صحيحة");

                                txtSpace1.Text = "";
                                txtSpace1.Focus();
                            }
                            else
                            {
                                DesignItem designItem = getDesignItem("صالة");
                                designItem.Space = re;
                                txtNoItem3.Focus();
                            }
                            break;
                        case "txtSpace4":
                            re = 0;
                            if (!double.TryParse(txtSpace4.Text, out re))
                            {
                                MessageBox.Show("ادخل قيمة صحيحة");

                                txtSpace4.Text = "";
                                txtSpace4.Focus();
                            }
                            else
                            {
                                DesignItem designItem = getDesignItem("غرفة");
                                designItem.Space = re;
                                txtNoItem4.Focus();

                            }
                            break;
                        case "txtSpace5":
                            re = 0;
                            if (!double.TryParse(txtSpace5.Text, out re))
                            {
                                MessageBox.Show("ادخل قيمة صحيحة");

                                txtSpace5.Text = "";
                                txtSpace5.Focus();
                            }
                            else
                            {
                                DesignItem designItem = getDesignItem("اخري");
                                designItem.Space = re;
                                txtNoItem5.Focus();
                            }
                            break;
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txtNoItem_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    int re = 0;
                    TextBox txtbox = (TextBox)sender;
                    switch (txtbox.Name)
                    {
                        case "txtNoItem1":

                            if (!int.TryParse(txtNoItem1.Text, out re))
                            {
                                MessageBox.Show("ادخل قيمة صحيحة");

                                txtNoItem1.Text = "";
                                txtNoItem1.Focus();
                            }
                            else
                            {
                                txtCost1.Focus();
                                DesignItem designItem = getDesignItem("حمام");
                                designItem.NoItems = re;
                            }
                            break;
                        case "txtNoItem2":

                            if (!int.TryParse(txtNoItem2.Text, out re))
                            {
                                MessageBox.Show("ادخل قيمة صحيحة");

                                txtNoItem2.Text = "";
                                txtNoItem2.Focus();
                            }
                            else
                            {
                                txtCost2.Focus();
                                DesignItem designItem = getDesignItem("مطبخ");
                                designItem.NoItems = re;
                            }
                            break;
                        case "txtNoItem3":

                            if (!int.TryParse(txtNoItem3.Text, out re))
                            {
                                MessageBox.Show("ادخل قيمة صحيحة");

                                txtNoItem3.Text = "";
                                txtNoItem3.Focus();
                            }
                            else
                            {
                                txtCost3.Focus();
                                DesignItem designItem = getDesignItem("صالة");
                                designItem.NoItems = re;
                            }
                            break;
                        case "txtNoItem4":

                            if (!int.TryParse(txtNoItem4.Text, out re))
                            {
                                MessageBox.Show("ادخل قيمة صحيحة");

                                txtNoItem4.Text = "";
                                txtNoItem4.Focus();
                            }
                            else
                            {
                                txtCost4.Focus();
                                DesignItem designItem = getDesignItem("غرفة");
                                designItem.NoItems = re;
                            }
                            break;
                        case "txtNoItem5":

                            if (!int.TryParse(txtNoItem5.Text, out re))
                            {
                                MessageBox.Show("ادخل قيمة صحيحة");

                                txtNoItem5.Text = "";
                                txtNoItem5.Focus();
                            }
                            else
                            {
                                txtCost5.Focus();
                                DesignItem designItem = getDesignItem("اخري");
                                designItem.NoItems = re;
                            }
                            break;
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void txtCost_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    double re = 0;
                    double totalrow = 0;
                    double total = 0;
                    TextBox txtbox = (TextBox)sender;
                    switch (txtbox.Name)
                    {
                        case "txtCost1":

                            if (!double.TryParse(txtCost1.Text, out re))
                            {
                                MessageBox.Show("ادخل قيمة صحيحة");

                                txtCost1.Text = "";
                                txtCost1.Focus();
                            }
                            else
                            {
                                txtTotal1.Text = (re * (Convert.ToInt16(txtNoItem1.Text))).ToString();
                                totalrow = Convert.ToDouble(txtTotal1.Text);
                                total = Convert.ToDouble(txtTotal.Text) + totalrow;
                                calTotal();
                                DesignItem designItem = getDesignItem("حمام");
                                designItem.ItemCost = re;
                                designItem.Total = re;
                            }
                            break;
                        case "txtCost2":

                            if (!double.TryParse(txtCost2.Text, out re))
                            {
                                MessageBox.Show("ادخل قيمة صحيحة");

                                txtNoItem2.Text = "";
                                txtNoItem2.Focus();
                            }
                            else
                            {
                                txtTotal2.Text = (re * (Convert.ToInt16(txtNoItem2.Text))).ToString();
                                totalrow = Convert.ToDouble(txtTotal2.Text);
                                total = Convert.ToDouble(txtTotal.Text) + totalrow;
                                calTotal();
                                DesignItem designItem = getDesignItem("مطبخ");
                                designItem.ItemCost = re;
                                designItem.Total = re;
                            }
                            break;
                        case "txtCost3":

                            if (!double.TryParse(txtCost3.Text, out re))
                            {
                                MessageBox.Show("ادخل قيمة صحيحة");

                                txtNoItem3.Text = "";
                                txtNoItem3.Focus();
                            }
                            else
                            {
                                txtTotal3.Text = (re * (Convert.ToInt16(txtNoItem3.Text))).ToString();
                                totalrow = Convert.ToDouble(txtTotal3.Text);
                                total = Convert.ToDouble(txtTotal.Text) + totalrow;
                                calTotal();
                                DesignItem designItem = getDesignItem("صالة");
                                designItem.ItemCost = re;
                                designItem.Total = re;
                            }
                            break;
                        case "txtCost4":

                            if (!double.TryParse(txtCost4.Text, out re))
                            {
                                MessageBox.Show("ادخل قيمة صحيحة");

                                txtNoItem4.Text = "";
                                txtNoItem4.Focus();
                            }
                            else
                            {
                                txtTotal4.Text = (re * (Convert.ToInt16(txtNoItem4.Text))).ToString();
                                totalrow = Convert.ToDouble(txtTotal4.Text);
                                total = Convert.ToDouble(txtTotal.Text) + totalrow;
                                calTotal();
                                DesignItem designItem = getDesignItem("غرفة");
                                designItem.ItemCost = re;
                                designItem.Total = re;
                            }
                            break;
                        case "txtCost5":

                            if (!double.TryParse(txtCost5.Text, out re))
                            {
                                MessageBox.Show("ادخل قيمة صحيحة");

                                txtNoItem5.Text = "";
                                txtNoItem5.Focus();
                            }
                            else
                            {
                                txtTotal5.Text = (re * (Convert.ToInt16(txtNoItem5.Text))).ToString();
                                totalrow = Convert.ToDouble(txtTotal5.Text);
                                total = Convert.ToDouble(txtTotal.Text) + totalrow;
                                calTotal();
                                DesignItem designItem = getDesignItem("اخري");
                                designItem.ItemCost = re;
                                designItem.Total = re;
                            }
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //functions
        public void clear()
        {
            foreach (Control co in this.panel1.Controls)
            {
                //if (co is GroupBox)
                //{
                if (co is ComboBox)
                {
                    co.Text = "";
                }
                else if (co is TextBox)
                {
                    co.Text = "";
                }
                else
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
        }       
        void printCategoriesBill(int customerDesignID)
        {
            Print_Design_Report f = new Print_Design_Report();
            if (comClient.Text != "")
            {
                f.PrintInvoice(DateTime.Now, customerDesignID, TransitionID, UserControl.EmpBranchName, comClient.Text + " " + comClient.SelectedValue.ToString(), Convert.ToDouble(txtPaidMoney.Text), PaymentMethod, cmbBank.Text, txtCheckNumber.Text, dateEdit1.Text, txtOperationNumber.Text, txtDescrip.Text, UserControl.EmpName, comEngDesign.Text, comDelegate.Text, arrPaidMoney[0], arrPaidMoney[1], arrPaidMoney[2], arrPaidMoney[3], arrPaidMoney[4], arrPaidMoney[5], arrPaidMoney[6], arrPaidMoney[7], arrPaidMoney[8], arrRestMoney[0], arrRestMoney[1], arrRestMoney[2], arrRestMoney[3], arrRestMoney[4], arrRestMoney[5], arrRestMoney[6], arrRestMoney[7], arrRestMoney[8]);
            }
            else if (comEng.Text != "")
            {
                f.PrintInvoice(DateTime.Now, customerDesignID, TransitionID, UserControl.EmpBranchName, comEng.Text + " " + comEng.SelectedValue.ToString(), Convert.ToDouble(txtPaidMoney.Text), PaymentMethod, cmbBank.Text, txtCheckNumber.Text, dateEdit1.Text, txtOperationNumber.Text, txtDescrip.Text, UserControl.EmpName, comEngDesign.Text, comDelegate.Text, arrPaidMoney[0], arrPaidMoney[1], arrPaidMoney[2], arrPaidMoney[3], arrPaidMoney[4], arrPaidMoney[5], arrPaidMoney[6], arrPaidMoney[7], arrPaidMoney[8], arrRestMoney[0], arrRestMoney[1], arrRestMoney[2], arrRestMoney[3], arrRestMoney[4], arrRestMoney[5], arrRestMoney[6], arrRestMoney[7], arrRestMoney[8]);
            }
            f.ShowDialog();
            for (int i = 0; i < arrPaidMoney.Length; i++)
                arrPaidMoney[i] = arrRestMoney[i] = 0;
            for (int i = 0; i < arrRestMoney.Length; i++)
                arrRestMoney[i] = arrPaidMoney[i] = 0;
        }
        public int addCustomerDesign()
        {
            //,BranchBillNumber,BranchName
            string query = "INSERT INTO customer_design(Branch_ID,Branch_Name,Customer_Name,Customer_ID,Client_Name,Client_ID,Engineer_Name,Engineer_ID,Delegate_ID,Delegate_Name,Date,PaidMoney) VALUES(@Branch_ID,@Branch_Name,@Customer_Name,@Customer_ID,@Client_Name,@Client_ID,@Engineer_Name,@Engineer_ID,@Delegate_ID,@Delegate_Name,@Date,@PaidMoney)";
            MySqlCommand com = new MySqlCommand(query, dbconnection);
            com.Parameters.Add("@Branch_ID", MySqlDbType.Int16, 11).Value = transitionbranchID;
            com.Parameters.Add("@Branch_Name", MySqlDbType.VarChar, 255).Value = UserControl.EmpBranchName;
            if (comEng.SelectedValue != null)
            {
                com.Parameters.Add("@Customer_Name", MySqlDbType.VarChar, 255).Value = comEng.Text;
                com.Parameters.Add("@Customer_ID", MySqlDbType.Int16, 11).Value = comEng.SelectedValue;
            }
            else
            {
                com.Parameters.Add("@Customer_Name", MySqlDbType.VarChar, 255).Value = null;
                com.Parameters.Add("@Customer_ID", MySqlDbType.Int16, 11).Value = null;
            }
            if (comClient.SelectedValue != null)
            {
                com.Parameters.Add("@Client_Name", MySqlDbType.VarChar, 255).Value = comClient.Text;
                com.Parameters.Add("@Client_ID", MySqlDbType.Int16, 11).Value = comClient.SelectedValue;
            }
            else
            {
                com.Parameters.Add("@Client_Name", MySqlDbType.VarChar, 255).Value = null;
                com.Parameters.Add("@Client_ID", MySqlDbType.Int16, 11).Value = null;
            }
            com.Parameters.Add("@Engineer_Name", MySqlDbType.VarChar, 255).Value = comEngDesign.Text;
            com.Parameters.Add("@Engineer_ID", MySqlDbType.Int16, 11).Value = comEngDesign.SelectedValue;
            com.Parameters.Add("@Delegate_Name", MySqlDbType.VarChar, 255).Value = comDelegate.Text;
            com.Parameters.Add("@Delegate_ID", MySqlDbType.Int16, 11).Value = comDelegate.SelectedValue;
            com.Parameters.Add("@Date", MySqlDbType.Date).Value = DateTime.Now.Date;
            com.Parameters.Add("@PaidMoney", MySqlDbType.Double).Value = txtPaidMoney.Text;
            com.ExecuteNonQuery();

            query = "select CustomerDesign_ID from customer_design order by CustomerDesign_ID desc limit 1";
            com = new MySqlCommand(query, dbconnection);
            int CustomerDesign_ID = Convert.ToInt16(com.ExecuteScalar());

            query = "INSERT INTO customer_design_details (CustomerDesignID, DesignLocation, Space, NoItems, ItemCost, Total) VALUES (@CustomerDesignID, @DesignLocation, @Space, @NoItems, @ItemCost, @Total) ";
            com.Parameters.Add("@CustomerDesignID", MySqlDbType.Int16, 11);
            com.Parameters.Add("@DesignLocation", MySqlDbType.VarChar, 255);
            com.Parameters.Add("@Space", MySqlDbType.Double);
            com.Parameters.Add("@ItemCost", MySqlDbType.Double);
            com.Parameters.Add("@Total", MySqlDbType.Double);
            foreach (DesignItem item in listDesignItems)
            {
                com.Parameters["@CustomerDesignID"].Value = CustomerDesign_ID;
                com.Parameters[ "@DesignLocation"].Value = item.DesignLocation;
                com.Parameters["@Space"].Value =item.Space;
                com.Parameters["@ItemCost"].Value =item.ItemCost;
                com.Parameters["@Total"].Value = item.Total;
                com.ExecuteNonQuery();
            }
         
            return CustomerDesign_ID;
        }
        public void calTotal()
        {
            double total1 = Convert.ToDouble(txtTotal1.Text);
            double total2 = Convert.ToDouble(txtTotal2.Text);
            double total3 = Convert.ToDouble(txtTotal3.Text);
            double total4 = Convert.ToDouble(txtTotal4.Text);
            double total5 = Convert.ToDouble(txtTotal5.Text);

            double total = total1 + total2 + total3 + total4 + total5;
            txtTotal.Text = total.ToString();
            txtPaidMoney.Text = total.ToString();
        }
        public DesignItem getDesignItem(string DesignLocation)
        {
            foreach (DesignItem item in listDesignItems)
            {
                if (item.DesignLocation == DesignLocation)
                {
                    return item;
                }
            }
            return null;
        }

        public class DesignItem {

            public string DesignLocation { get; set; }
            public double Space { get; set; }
            public int NoItems { get; set; }
            public double ItemCost { get; set; }
            public double Total { get; set; }
        }

    }
}

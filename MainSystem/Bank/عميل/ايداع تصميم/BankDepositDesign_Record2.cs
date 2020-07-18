﻿using DevExpress.XtraTab;
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
                    check = (/*(radioAgel.Checked || radioCash.Checked) && */comDelegate.Text != "" && (comClient.Text != "" || comEng.Text != "")/*&& txtRestMoney.Text != "" && cmbBranch.Text != ""*/ && cmbBank.Text != "" && txtPaidMoney.Text != "");
                }
                else if (PaymentMethod == "شيك")
                {
                    check = (/*(radioAgel.Checked || radioCash.Checked) && */comDelegate.Text != "" && (comClient.Text != "" || comEng.Text != "") /*&& txtRestMoney.Text != "" && cmbBranch.Text != ""*/ && cmbBank.Text != "" && txtPaidMoney.Text != "" && dateEdit1.Text != "" && txtCheckNumber.Text != "");
                }
                else if (PaymentMethod == "حساب بنكى")
                {
                    check = (/*(radioAgel.Checked || radioCash.Checked) && */comDelegate.Text != "" && (comClient.Text != "" || comEng.Text != "") /*&& txtRestMoney.Text != "" && cmbBranch.Text != ""*/ && cmbBank.Text != "" && txtPaidMoney.Text != "" && dateEdit1.Text != "");
                }
                else if (PaymentMethod == "فيزا")
                {
                    check = (/*(radioAgel.Checked || radioCash.Checked) && */comDelegate.Text != "" && (comClient.Text != "" || comEng.Text != "") /*&& txtRestMoney.Text != "" && cmbBranch.Text != ""*/ && cmbBank.Text != "" && txtPaidMoney.Text != "" && txtOperationNumber.Text != "");
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

                            txtNoItemBath.Enabled = true;
                            txtCostBath.Enabled = true;
                            txtNoItemBath.Focus();
                            DesignItem designItem = new DesignItem();
                            designItem.DesignLocation = "حمام";
                            listDesignItems.Add(designItem);
                        }
                        else
                        {
                            txtNoItemBath.Enabled = false;
                            txtNoItemBath.Text = "";
                            txtCostBath.Enabled = false;
                            txtCostBath.Text = "";
                            txtTotalBath.Text = "0";
                            
                            calTotal();
                            
                            DesignItem designItem = getDesignItem("حمام");
                            listDesignItems.Remove(designItem);
                        }
                        break;
                    case "مطبخ":
                        if (ch.Checked)
                        {
                            txtNoItemKitchen.Enabled = true;
                            txtCostKitchen.Enabled = true;
                            txtNoItemKitchen.Focus();
                            DesignItem designItem = new DesignItem();
                            designItem.DesignLocation = "مطبخ";
                            listDesignItems.Add(designItem);
                        }
                        else
                        {
                            txtNoItemKitchen.Enabled = false;
                            txtNoItemKitchen.Text = "";
                            txtCostKitchen.Enabled = false;
                            txtCostKitchen.Text = "";
                            txtTotalKitchen.Text = "0";

                            calTotal();

                            DesignItem designItem = getDesignItem("مطبخ");
                            listDesignItems.Remove(designItem);
                        }
                        break;
                    case "صالة":
                        if (ch.Checked)
                        {
                            txtNoItemHall.Enabled = true;
                            txtCostHall.Enabled = true;
                            txtNoItemHall.Focus();
                            DesignItem designItem = new DesignItem();
                            designItem.DesignLocation = "صالة";
                            listDesignItems.Add(designItem);
                        }
                        else
                        {
                            txtNoItemHall.Enabled = false;
                            txtNoItemHall.Text = "";
                            txtCostHall.Enabled = false;
                            txtCostHall.Text = "";
                            txtTotalHall.Text = "0";

                            calTotal();

                            DesignItem designItem = getDesignItem("صالة");
                            listDesignItems.Remove(designItem);
                        }
                        break;
                    case "غرفة":
                        if (ch.Checked)
                        {
                            txtNoItemRoom.Enabled = true;
                            txtCostRoom.Enabled = true;
                            txtNoItemRoom.Focus();
                            DesignItem designItem = new DesignItem();
                            designItem.DesignLocation = "غرفة";
                            listDesignItems.Add(designItem);
                        }
                        else
                        {
                            txtNoItemRoom.Enabled = false;
                            txtNoItemRoom.Text = "";
                            txtCostRoom.Enabled = false;
                            txtCostRoom.Text = "";
                            txtTotalRoom.Text = "0";

                            calTotal();

                            DesignItem designItem = getDesignItem("غرفة");
                            listDesignItems.Remove(designItem);
                        }
                        break;
                    case "اخري":
                        if (ch.Checked)
                        {
                            txtNoItemOther.Enabled = true;
                            txtCostOther.Enabled = true;
                            txtNoItemOther.Focus();
                            DesignItem designItem = new DesignItem();
                            designItem.DesignLocation = "اخري";
                            listDesignItems.Add(designItem);
                        }
                        else
                        {
                            txtNoItemOther.Enabled = false;
                            txtNoItemOther.Text = "";
                            txtCostOther.Enabled = false;
                            txtCostOther.Text = "";
                            txtTotalOther.Text = "0";

                            calTotal();

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
                        case "txtNoItemBath":

                            if (!int.TryParse(txtNoItemBath.Text, out re))
                            {
                                MessageBox.Show("ادخل قيمة صحيحة");

                                txtNoItemBath.Text = "";
                                txtNoItemBath.Focus();
                            }
                            else
                            {
                                txtCostBath.Focus();
                                DesignItem designItem = getDesignItem("حمام");
                                designItem.NoItems = re;
                            }
                            break;
                        case "txtNoItemKitchen":

                            if (!int.TryParse(txtNoItemKitchen.Text, out re))
                            {
                                MessageBox.Show("ادخل قيمة صحيحة");

                                txtNoItemKitchen.Text = "";
                                txtNoItemKitchen.Focus();
                            }
                            else
                            {
                                txtCostKitchen.Focus();
                                DesignItem designItem = getDesignItem("مطبخ");
                                designItem.NoItems = re;
                            }
                            break;
                        case "txtNoItemHall":

                            if (!int.TryParse(txtNoItemHall.Text, out re))
                            {
                                MessageBox.Show("ادخل قيمة صحيحة");

                                txtNoItemHall.Text = "";
                                txtNoItemHall.Focus();
                            }
                            else
                            {
                                txtCostHall.Focus();
                                DesignItem designItem = getDesignItem("صالة");
                                designItem.NoItems = re;
                            }
                            break;
                        case "txtNoItemRoom":

                            if (!int.TryParse(txtNoItemRoom.Text, out re))
                            {
                                MessageBox.Show("ادخل قيمة صحيحة");

                                txtNoItemRoom.Text = "";
                                txtNoItemRoom.Focus();
                            }
                            else
                            {
                                txtCostRoom.Focus();
                                DesignItem designItem = getDesignItem("غرفة");
                                designItem.NoItems = re;
                            }
                            break;
                        case "txtNoItemOther":

                            if (!int.TryParse(txtNoItemOther.Text, out re))
                            {
                                MessageBox.Show("ادخل قيمة صحيحة");

                                txtNoItemOther.Text = "";
                                txtNoItemOther.Focus();
                            }
                            else
                            {
                                txtCostOther.Focus();
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
                        case "txtCostBath":

                            if (!double.TryParse(txtCostBath.Text, out re))
                            {
                                MessageBox.Show("ادخل قيمة صحيحة");

                                txtCostBath.Text = "";
                                txtCostBath.Focus();
                            }
                            else
                            {
                                txtTotalBath.Text = (re * (Convert.ToInt16(txtNoItemBath.Text))).ToString();
                                totalrow = Convert.ToDouble(txtTotalBath.Text);
                                total = Convert.ToDouble(txtTotal.Text) + totalrow;
                                calTotal();
                                DesignItem designItem = getDesignItem("حمام");
                                designItem.ItemCost = re;
                                designItem.Total = re * (Convert.ToInt16(txtNoItemBath.Text));
                            }
                            break;
                        case "txtCostKitchen":

                            if (!double.TryParse(txtCostKitchen.Text, out re))
                            {
                                MessageBox.Show("ادخل قيمة صحيحة");

                                txtNoItemKitchen.Text = "";
                                txtNoItemKitchen.Focus();
                            }
                            else
                            {
                                txtTotalKitchen.Text = (re * (Convert.ToInt16(txtNoItemKitchen.Text))).ToString();
                                totalrow = Convert.ToDouble(txtTotalKitchen.Text);
                                total = Convert.ToDouble(txtTotal.Text) + totalrow;
                                calTotal();
                                DesignItem designItem = getDesignItem("مطبخ");
                                designItem.ItemCost = re;
                                designItem.Total = re * (Convert.ToInt16(txtNoItemKitchen.Text));
                            }
                            break;
                        case "txtCostHall":

                            if (!double.TryParse(txtCostHall.Text, out re))
                            {
                                MessageBox.Show("ادخل قيمة صحيحة");

                                txtNoItemHall.Text = "";
                                txtNoItemHall.Focus();
                            }
                            else
                            {
                                txtTotalHall.Text = (re * (Convert.ToInt16(txtNoItemHall.Text))).ToString();
                                totalrow = Convert.ToDouble(txtTotalHall.Text);
                                total = Convert.ToDouble(txtTotal.Text) + totalrow;
                                calTotal();
                                DesignItem designItem = getDesignItem("صالة");
                                designItem.ItemCost = re;
                                designItem.Total = re * (Convert.ToInt16(txtNoItemHall.Text));
                            }
                            break;
                        case "txtCostRoom":

                            if (!double.TryParse(txtCostRoom.Text, out re))
                            {
                                MessageBox.Show("ادخل قيمة صحيحة");

                                txtNoItemRoom.Text = "";
                                txtNoItemRoom.Focus();
                            }
                            else
                            {
                                txtTotalRoom.Text = (re * (Convert.ToInt16(txtNoItemRoom.Text))).ToString();
                                totalrow = Convert.ToDouble(txtTotalRoom.Text);
                                total = Convert.ToDouble(txtTotal.Text) + totalrow;
                                calTotal();
                                DesignItem designItem = getDesignItem("غرفة");
                                designItem.ItemCost = re;
                                designItem.Total = re * (Convert.ToInt16(txtNoItemRoom.Text));
                            }
                            break;
                        case "txtCostOther":

                            if (!double.TryParse(txtCostOther.Text, out re))
                            {
                                MessageBox.Show("ادخل قيمة صحيحة");

                                txtNoItemOther.Text = "";
                                txtNoItemOther.Focus();
                            }
                            else
                            {
                                txtTotalOther.Text = (re * (Convert.ToInt16(txtNoItemOther.Text))).ToString();
                                totalrow = Convert.ToDouble(txtTotalOther.Text);
                                total = Convert.ToDouble(txtTotal.Text) + totalrow;
                                calTotal();
                                DesignItem designItem = getDesignItem("اخري");
                                designItem.ItemCost = re;
                                designItem.Total = (re * (Convert.ToInt16(txtNoItemOther.Text)));
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
                f.PrintInvoice(DateTime.Now, customerDesignID, TransitionID, UserControl.EmpBranchName, comClient.Text + " " + comClient.SelectedValue.ToString(), Convert.ToDouble(txtPaidMoney.Text), PaymentMethod, cmbBank.Text, txtCheckNumber.Text, dateEdit1.Text, txtOperationNumber.Text, txtDescrip.Text, UserControl.EmpName, comDelegate.Text, txtNoItemBath.Text, txtCostBath.Text, txtTotalBath.Text, txtNoItemKitchen.Text, txtCostKitchen.Text, txtTotalKitchen.Text, txtNoItemHall.Text, txtCostHall.Text, txtTotalHall.Text, txtNoItemRoom.Text, txtCostRoom.Text, txtTotalRoom.Text, txtNoItemOther.Text, txtCostOther.Text, txtTotalOther.Text);
            }
            else if (comEng.Text != "")
            {
                f.PrintInvoice(DateTime.Now, customerDesignID, TransitionID, UserControl.EmpBranchName, comEng.Text + " " + comEng.SelectedValue.ToString(), Convert.ToDouble(txtPaidMoney.Text), PaymentMethod, cmbBank.Text, txtCheckNumber.Text, dateEdit1.Text, txtOperationNumber.Text, txtDescrip.Text, UserControl.EmpName, comDelegate.Text, txtNoItemBath.Text, txtCostBath.Text, txtTotalBath.Text, txtNoItemKitchen.Text, txtCostKitchen.Text, txtTotalKitchen.Text, txtNoItemHall.Text, txtCostHall.Text, txtTotalHall.Text, txtNoItemRoom.Text, txtCostRoom.Text, txtTotalRoom.Text, txtNoItemOther.Text, txtCostOther.Text, txtTotalOther.Text);
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
            string query = "INSERT INTO customer_design(Branch_ID,Branch_Name,Customer_Name,Customer_ID,Client_Name,Client_ID,Delegate_ID,Delegate_Name,Date,PaidMoney) VALUES(@Branch_ID,@Branch_Name,@Customer_Name,@Customer_ID,@Client_Name,@Client_ID,@Delegate_ID,@Delegate_Name,@Date,@PaidMoney)";
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
            com.Parameters.Add("@Delegate_Name", MySqlDbType.VarChar, 255).Value = comDelegate.Text;
            com.Parameters.Add("@Delegate_ID", MySqlDbType.Int16, 11).Value = comDelegate.SelectedValue;
            com.Parameters.Add("@Date", MySqlDbType.Date).Value = DateTime.Now.Date;
            com.Parameters.Add("@PaidMoney", MySqlDbType.Double).Value = txtPaidMoney.Text;
            com.ExecuteNonQuery();

            query = "select CustomerDesign_ID from customer_design order by CustomerDesign_ID desc limit 1";
            com = new MySqlCommand(query, dbconnection);
            int CustomerDesign_ID = Convert.ToInt16(com.ExecuteScalar());

            query = "INSERT INTO customer_design_details (CustomerDesignID, DesignLocation, Space, NoItems, ItemCost, Total) VALUES (@CustomerDesignID, @DesignLocation, @Space, @NoItems, @ItemCost, @Total) ";
        
            foreach (DesignItem item in listDesignItems)
            {
                com = new MySqlCommand(query, dbconnection);
                com.Parameters.Add("@CustomerDesignID", MySqlDbType.Int16, 11).Value = CustomerDesign_ID;
                com.Parameters.Add("@DesignLocation", MySqlDbType.VarChar, 255).Value = item.DesignLocation;
                com.Parameters.Add("@Space", MySqlDbType.Double).Value =item.Space;
                com.Parameters.Add("@NoItems", MySqlDbType.Double).Value =item.NoItems;
                com.Parameters.Add("@ItemCost", MySqlDbType.Double).Value =item.ItemCost;
                com.Parameters.Add("@Total", MySqlDbType.Double).Value = item.Total;
                com.ExecuteNonQuery();
            }
         
            return CustomerDesign_ID;
        }
        public void calTotal()
        {
            double total1 = Convert.ToDouble(txtTotalBath.Text);
            double total2 = Convert.ToDouble(txtTotalKitchen.Text);
            double total3 = Convert.ToDouble(txtTotalHall.Text);
            double total4 = Convert.ToDouble(txtTotalRoom.Text);
            double total5 = Convert.ToDouble(txtTotalOther.Text);

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

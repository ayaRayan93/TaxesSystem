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
    public partial class BankPullDesign_Record : Form
    {
        MySqlConnection dbconnection;
        bool flag = false;
        //int branchID = 0;
        string branchName = "";
        string PaymentMethod = "";
        int[] arrOFPhaat; //count of each catagory value of money in store
        int[] arrRestMoney;
        int[] arrPaidMoney;
        bool loaded = false;
        bool loadedPayType = false;
        XtraTabPage xtraTabPage;
        string TransitionID = "";
        bool flagCategoriesSuccess = false;
        XtraTabControl tabControlBank;
        int transitionbranchID = 0;

        public BankPullDesign_Record()//BankDepositAgl_Report form, XtraTabControl MainTabControlBank)
        {
            InitializeComponent();
            dbconnection = new MySqlConnection(connection.connectionString);
            // tabControlBank = MainTabControlBank;
            arrOFPhaat = new int[9];
            arrPaidMoney = new int[9];
            arrRestMoney = new int[9];

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

            comBranchName.Visible = false;
            txtBranchBillNumber.Visible = false;
            label8.Visible = false;
            label9.Visible = false;
        }

        private void BankDepositAgl_Record_Load(object sender, EventArgs e)
        {
            try
            {
                transitionbranchID = UserControl.EmpBranchID;
                //branchID = UserControl.UserBranch(dbconnection);
                dbconnection.Open();
                string query = "select Branch_Name from branch where Branch_ID=" + transitionbranchID;
                MySqlCommand com = new MySqlCommand(query, dbconnection);
                branchName = com.ExecuteScalar().ToString();

                comBranchName.Text = branchName;
                query = "select * from delegate where Branch_ID=" + transitionbranchID;
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

        private void txtDesignNum_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    dbconnection.Open();
                    string query = "select * from customer_design where CustomerDesign_ID=" + txtDesignNum.Text;
                    MySqlCommand com = new MySqlCommand(query, dbconnection);
                    MySqlDataReader dr = com.ExecuteReader();
                    while (dr.Read())
                    {
                        string designstatus = dr["Design_Status"].ToString();
                        if (designstatus == "ايداع تصميم")
                        {
                            btnAdd.Enabled = true;
                            comEng.Text = dr["Customer_Name"].ToString();
                            txtCustomerID.Text = dr["Customer_ID"].ToString();

                            comClient.Text = dr["Client_Name"].ToString();
                            txtClientID.Text = dr["Client_ID"].ToString();

                            comDelegate.Text = dr["Delegate_Name"].ToString();
                            comEngDesign.Text = dr["Engineer_Name"].ToString();

                            labPaidMoney.Text = dr["PaidMoney"].ToString();

                            btnAdd.LabelText = "حفظ";
                            panel3.Visible = true;
                            panel4.Visible = true;
                            panelContent.Visible = true;
                        }
                        else
                        {
                            if (dr["Branch_ID"].ToString() != null)
                            {
                                DialogResult dialogResult = MessageBox.Show("  ?هذا التصميم تم سحبه من قبل هل تريد تعديل رقم الفاتورة ", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                if (dialogResult == DialogResult.Yes)
                                {
                                    btnAdd.Enabled = true;
                                    btnAdd.LabelText = "حفظ التعديل";
                                    panel3.Visible = false;
                                    panel4.Visible = false;
                                    panelContent.Visible = false;

                                }
                                else
                                {
                                    clear();
                                }
                            }
                            else if (designstatus == "الغاء التصميم")
                            {
                                MessageBox.Show("هذا التصميم تم الغاءه وسحبه من قبل");
                                panel3.Visible = false;
                                panel4.Visible = false;
                                panelContent.Visible = false;
                                btnAdd.Enabled = false;
                                btnAdd.LabelText = "حفظ";
                            }
                        }
                    }
                    dr.Close();
                    panelContent.Visible = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
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
                labelchekNumber.Text = "رقم الحساب";

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

        private void radConfirmDesign_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (radConfirmBill.Checked)
                {
                    comBranchName.Visible = true;
                    txtBranchBillNumber.Visible = true;
                    label8.Visible = true;
                    label9.Visible = true;
                }
                else
                {
                    comBranchName.Visible = false;
                    txtBranchBillNumber.Visible = false;
                    label8.Visible = false;
                    label9.Visible = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (btnAdd.LabelText == "حفظ")
                {
                    bool check = false;
                    if (PaymentMethod == "نقدى")
                    {
                        check = ( comDelegate.Text != "" && comEngDesign.Text != "" && (comClient.Text != "" || comEng.Text != "")/*&& txtRestMoney.Text != "" && cmbBranch.Text != ""*/ && cmbBank.Text != "" && txtPaidMoney.Text != "");
                    }
                    else if (PaymentMethod == "شيك")
                    {
                        check = ( comDelegate.Text != "" && comEngDesign.Text != "" && (comClient.Text != "" || comEng.Text != "") /*&& txtRestMoney.Text != "" && cmbBranch.Text != ""*/ && cmbBank.Text != "" && txtPaidMoney.Text != "" && dateEdit1.Text != "" && txtCheckNumber.Text != "");
                    }
                    else if (PaymentMethod == "حساب بنكى")
                    {
                        check = ( comDelegate.Text != "" && comEngDesign.Text != "" && (comClient.Text != "" || comEng.Text != "") /*&& txtRestMoney.Text != "" && cmbBranch.Text != ""*/ && cmbBank.Text != "" && txtPaidMoney.Text != "" && dateEdit1.Text != "" && txtCheckNumber.Text != "");
                    }
                    else if (PaymentMethod == "فيزا")
                    {
                        check = ( comDelegate.Text != "" && comEngDesign.Text != "" && (comClient.Text != "" || comEng.Text != "") /*&& txtRestMoney.Text != "" && cmbBranch.Text != ""*/ && cmbBank.Text != "" && txtPaidMoney.Text != "" && txtOperationNumber.Text != "");
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

                            updateCustomerDesign();

                            string query = "insert into transitions (TransitionBranch_ID,TransitionBranch_Name,Client_ID,Client_Name,Customer_ID,Customer_Name,Transition,Payment_Method,Bank_ID,Bank_Name,Date,Amount,Data,PayDay,Check_Number,Operation_Number,Type,Error,Employee_ID,Employee_Name,Delegate_ID,Delegate_Name) values(@TransitionBranch_ID,@TransitionBranch_Name,@Client_ID,@Client_Name,@Customer_ID,@Customer_Name,@Transition,@Payment_Method,@Bank_ID,@Bank_Name,@Date,@Amount,@Data,@PayDay,@Check_Number,@Operation_Number,@Type,@Error,@Employee_ID,@Employee_Name,@Delegate_ID,@Delegate_Name)";
                            MySqlCommand com = new MySqlCommand(query, dbconnection);

                            com.Parameters.Add("@Transition", MySqlDbType.VarChar, 255).Value = "سحب تصميم";
                            com.Parameters.Add("@Type", MySqlDbType.VarChar, 255).Value = "كاش";                        
                            com.Parameters.Add("@TransitionBranch_ID", MySqlDbType.Int16, 11).Value = transitionbranchID;
                            com.Parameters.Add("@TransitionBranch_Name", MySqlDbType.VarChar, 255).Value = UserControl.EmpBranchName;
                            com.Parameters.Add("@Branch_ID", MySqlDbType.Int16, 11).Value = transitionbranchID;
                            com.Parameters.Add("@Branch_Name", MySqlDbType.VarChar, 255).Value = branchName;
                            com.Parameters.Add("@Bill_Number", MySqlDbType.Int16, 11).Value = txtDesignNum.Text;
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
                            printCategoriesBill();

                            clear();

                            xtraTabPage.ImageOptions.Image = null;
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
                else
                {
                    if (txtBranchBillNumber.Text != "")
                    {
                        dbconnection.Open();
                        string query = "update customer_design set Design_Status=@Design_Status ,BranchBillNumber=@BranchBillNumber ,Branch_Name=@Branch_Name where CustomerDesign_ID=" + txtDesignNum.Text;
                        MySqlCommand com = new MySqlCommand(query, dbconnection);
                        com.Parameters.Add("@Design_Status", MySqlDbType.VarChar, 255).Value = "اتمام فاتورة";
                        com.Parameters.Add("@Branch_Name", MySqlDbType.VarChar, 255).Value = comBranchName.Text;
                        com.Parameters.Add("@BranchBillNumber", MySqlDbType.Int16, 11).Value = txtBranchBillNumber.Text;
                        com.ExecuteNonQuery();
                    }
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
                        }
                        else if ((total - totalPaid) == 0)
                        {
                            txtPaidRest.Text = "0";

                            txtPaidRest2.Text = "0";
                        }
                        else
                        {
                            txtPaidRest.Text = "0";
                            double sub = (total - totalPaid);

                            txtPaidRest2.Text = (-1 * (sub + Convert.ToDouble(RestMoney.Text))).ToString();
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
                     
                        }
                        else if ((Convert.ToDouble(RestMoney.Text) - (-1 * (Convert.ToDouble(txtPaidMoney.Text) - Convert.ToDouble(PaidMoney.Text)))) == 0)
                        {
                            txtPaidRest2.Text = "0";

                            txtPaidRest.Text = "0";
                        }
                        else
                        {
                            double sub = (Convert.ToDouble(txtPaidMoney.Text) - Convert.ToDouble(PaidMoney.Text));
                            txtPaidRest.Text = (Convert.ToDouble(RestMoney.Text) - (-1 * sub)).ToString();
                            txtPaidRest2.Text = "0";
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

        //functions
        public void clear()
        {
            foreach (Control item in this.panel1.Controls)
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
            radDesignCancel.Checked = false;
            radConfirmBill.Checked = false;
        }

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
                //if (co is GroupBox)
                //{
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
                //}
            }

            return flag5;
        }

        void printCategoriesBill()
        {
            /*Print_AglCategoriesBill_Report f = new Print_AglCategoriesBill_Report();
            if (comClient.Text != "")
            {
                f.PrintInvoice(DateTime.Now, TransitionID, branchName, comClient.Text + " " + comClient.SelectedValue.ToString(), Convert.ToDouble(txtPaidMoney.Text), PaymentMethod, cmbBank.Text, txtCheckNumber.Text, dateEdit1.Text, txtOperationNumber.Text, txtDescrip.Text, UserControl.EmpName, comDelegate.Text, arrPaidMoney[0], arrPaidMoney[1], arrPaidMoney[2], arrPaidMoney[3], arrPaidMoney[4], arrPaidMoney[5], arrPaidMoney[6], arrPaidMoney[7], arrPaidMoney[8], arrRestMoney[0], arrRestMoney[1], arrRestMoney[2], arrRestMoney[3], arrRestMoney[4], arrRestMoney[5], arrRestMoney[6], arrRestMoney[7], arrRestMoney[8]);
            }
            else if (comEng.Text != "")
            {
                f.PrintInvoice(DateTime.Now, TransitionID, branchName, comEng.Text + " " + comEng.SelectedValue.ToString(), Convert.ToDouble(txtPaidMoney.Text), PaymentMethod, cmbBank.Text, txtCheckNumber.Text, dateEdit1.Text, txtOperationNumber.Text, txtDescrip.Text, UserControl.EmpName, comDelegate.Text, arrPaidMoney[0], arrPaidMoney[1], arrPaidMoney[2], arrPaidMoney[3], arrPaidMoney[4], arrPaidMoney[5], arrPaidMoney[6], arrPaidMoney[7], arrPaidMoney[8], arrRestMoney[0], arrRestMoney[1], arrRestMoney[2], arrRestMoney[3], arrRestMoney[4], arrRestMoney[5], arrRestMoney[6], arrRestMoney[7], arrRestMoney[8]);
            }
            f.ShowDialog();
            for (int i = 0; i < arrPaidMoney.Length; i++)
                arrPaidMoney[i] = arrRestMoney[i] = 0;
            for (int i = 0; i < arrRestMoney.Length; i++)
                arrRestMoney[i] = arrPaidMoney[i] = 0;*/
        }

        public void updateCustomerDesign()
        {
            if (radDesignCancel.Checked)
            {
                string query = "update customer_design set Design_Status=@Design_Status where CustomerDesign_ID=" + txtDesignNum.Text;
                MySqlCommand com = new MySqlCommand(query, dbconnection);
                com.Parameters.Add("@Design_Status", MySqlDbType.VarChar, 255).Value = "الغاء التصميم";
                com.ExecuteNonQuery();
            }
            else
            {
                string query = "update customer_design set Design_Status=@Design_Status ,BranchBillNumber=@BranchBillNumber ,Branch_Name=@Branch_Name where CustomerDesign_ID=" + txtDesignNum.Text;
                MySqlCommand com = new MySqlCommand(query, dbconnection);
                com.Parameters.Add("@Design_Status", MySqlDbType.VarChar, 255).Value = "اتمام فاتورة";
                com.Parameters.Add("@Branch_Name", MySqlDbType.VarChar, 255).Value =comBranchName.Text;
                com.Parameters.Add("@BranchBillNumber", MySqlDbType.Int16, 11).Value = txtBranchBillNumber.Text;
                com.ExecuteNonQuery();
            }
            
      

        }

      

  
    }
}

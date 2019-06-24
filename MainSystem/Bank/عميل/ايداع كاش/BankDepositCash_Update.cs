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
    public partial class BankDepositCash_Update : Form
    {
        private MySqlConnection dbconnection, myConnection;
        bool flag = false;
        int branchID = 0;
        int ID = -1;
        double paidAmount = 0;
        private string Transaction_Type;
        int[] arrOFPhaat; //count of each catagory value of money in store
        int[] arrRestMoney;
        int[] arrPaidMoney;
        bool loaded = false;
        DataRowView selRow;
        XtraTabPage xtraTabPage;
        bool loadedPayType = false;
        string ConfirmEmp = "";
        bool flagCategoriesSuccess = false;
        XtraTabControl tabControlBank;
        int transitionbranchID = 0;

        public BankDepositCash_Update(DataRowView SelRow, BankDepositCash_Report form, XtraTabControl MainTabControlBank)
        {
            InitializeComponent();
            selRow = SelRow;
            dbconnection = new MySqlConnection(connection.connectionString);
            myConnection = new MySqlConnection(connection.connectionString);
            arrOFPhaat = new int[9];
            arrPaidMoney = new int[9];
            arrRestMoney = new int[9];
            tabControlBank = MainTabControlBank;
            
            this.dateEdit1.Properties.DisplayFormat.FormatString = "yyyy/MM/dd";
            this.dateEdit1.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dateEdit1.Properties.EditFormat.FormatString = "yyyy/MM/dd";
            this.dateEdit1.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dateEdit1.Properties.Mask.EditMask = "yyyy/MM/dd";
        }

        private void BankDepositCash_Update_Load(object sender, EventArgs e)
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
                        //txtBillNumber.Enabled = true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void radioButtonSafe_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                RadioButton r = (RadioButton)sender;
                radCash.Enabled = true;
                radCredit.Enabled = true;
                radBankAccount.Enabled = false;
                radVisa.Enabled = false;
                radBankAccount.Checked = false;
                radVisa.Checked = false;
                layoutControlItemBank.Text = "خزينة";
                layoutControlItemBank.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                labelBank.Visible = true;
                labelBank.Text = "*";
                layoutControlItemMoney.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                labelPaidMoney.Visible = true;
                labelPaidMoney.Text = "*";
                layoutControlItemComment.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                labelDescrip.Visible = true;
                layoutControlItemVisaType.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                labelVisaType.Visible = false;
                labelVisaType.Text = "";
                layoutControlItemOperationNumber.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                labelOperationNumber.Visible = false;
                labelOperationNumber.Text = "";

                radCash.Checked = true;
                string query = "select * from bank where Branch_ID=" + transitionbranchID + " and Bank_Type='خزينة'";
                MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                DataTable dt = new DataTable();
                da.Fill(dt);
                cmbBank.DataSource = dt;
                cmbBank.DisplayMember = dt.Columns["Bank_Name"].ToString();
                cmbBank.ValueMember = dt.Columns["Bank_ID"].ToString();
                cmbBank.SelectedIndex = -1;
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
                Transaction_Type = r.Text;
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
                Transaction_Type = r.Text;
                layoutControlItemPayDate.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                labelDate.Visible = true;
                labelDate.Text = "*";
                layoutControlItemCheck.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                labelCheckNumber.Visible = true;
                labelCheckNumber.Text = "*";
                layoutControlItemPayDate.Text = "تاريخ الاستحقاق";
                layoutControlItemCheck.Text = "رقم الشيك";
                groupBox1.Enabled = false;
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
                radCredit.Enabled = false;
                radCash.Checked = false;
                radCredit.Checked = false;
                radBankAccount.Enabled = true;
                radVisa.Enabled = true;
                radBankAccount.Checked = true;
                layoutControlItemBank.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                labelBank.Visible = true;
                labelBank.Text = "*";
                layoutControlItemMoney.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                labelPaidMoney.Visible = true;
                labelPaidMoney.Text = "*";
                layoutControlItemComment.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                labelDescrip.Visible = true;
                layoutControlItemCheck.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                labelCheckNumber.Visible = true;
                labelCheckNumber.Text = "*";
                groupBox1.Enabled = false;
                loadedPayType = true;
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
                Transaction_Type = r.Text;
                layoutControlItemBank.Text = "بنك";
                layoutControlItemPayDate.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                labelDate.Visible = true;
                labelDate.Text = "*";
                layoutControlItemVisaType.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                labelVisaType.Visible = false;
                labelVisaType.Text = "";
                layoutControlItemOperationNumber.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                labelOperationNumber.Visible = false;
                labelOperationNumber.Text = "";
                layoutControlItemPayDate.Text = "تاريخ الايداع";
                layoutControlItemCheck.Text = "رقم الحساب";

                string query = "select * from bank where Bank_Type = 'حساب بنكى' and Branch_ID is null and BankVisa_ID is null";
                MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                DataTable dt = new DataTable();
                da.Fill(dt);
                cmbBank.DataSource = dt;
                cmbBank.DisplayMember = dt.Columns["Bank_Name"].ToString();
                cmbBank.ValueMember = dt.Columns["Bank_ID"].ToString();
                cmbBank.SelectedIndex = -1;
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
                Transaction_Type = r.Text;
                layoutControlItemBank.Text = "فيزا";
                layoutControlItemPayDate.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                labelDate.Visible = false;
                labelDate.Text = "";
                layoutControlItemVisaType.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                labelVisaType.Visible = true;
                labelVisaType.Text = "*";
                layoutControlItemOperationNumber.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                labelOperationNumber.Visible = true;
                labelOperationNumber.Text = "*";
                layoutControlItemCheck.Text = "رقم الكارت";

                string query = "select * from bank where Branch_ID=" + transitionbranchID + " and Bank_Type='فيزا' and BankVisa_ID is not null";
                MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                DataTable dt = new DataTable();
                da.Fill(dt);
                cmbBank.DataSource = dt;
                cmbBank.DisplayMember = dt.Columns["Bank_Name"].ToString();
                cmbBank.ValueMember = dt.Columns["Bank_ID"].ToString();
                cmbBank.SelectedIndex = -1;
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
                if (Transaction_Type == "نقدى")
                {
                    check = (ID != -1 && branchID != 0 && txtRestMoney.Text != "" && cmbBank.Text != "" && txtPaidMoney.Text != "");
                }
                else if (Transaction_Type == "شيك")
                {
                    check = (ID != -1 && branchID != 0 && txtRestMoney.Text != "" && cmbBank.Text != "" && txtPaidMoney.Text != "" && dateEdit1.Text != "" && txtCheckNumber.Text != "");
                }
                else if (Transaction_Type == "حساب بنكى")
                {
                    check = (ID != -1 && branchID != 0 && txtRestMoney.Text != "" && cmbBank.Text != "" && txtPaidMoney.Text != "" && dateEdit1.Text != "" && txtCheckNumber.Text != "");
                }
                else if (Transaction_Type == "فيزا")
                {
                    check = (ID != -1 && branchID != 0 && txtRestMoney.Text != "" && cmbBank.Text != "" && txtPaidMoney.Text != "" && txtCheckNumber.Text != "" && txtVisaType.Text != "" && txtOperationNumber.Text != "");
                }

                if (check)
                {
                    if (!flagCategoriesSuccess)
                    {
                        if (MessageBox.Show("هل انت متاكد من الفئات؟", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
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
                            /*if (outParse <= restMoney)
                            {*/
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
                                        dbconnection.Open();

                                        string query = "update Transitions set Amount=@Amount,Data=@Data,PayDay=@PayDay,Check_Number=@Check_Number,Visa_Type=@Visa_Type,Operation_Number=@Operation_Number where Transition_ID=" + selRow[0].ToString();
                                        MySqlCommand com = new MySqlCommand(query, dbconnection);

                                        com.Parameters.Add("@Operation_Number", MySqlDbType.Int16, 11).Value = opNumString;
                                        com.Parameters.Add("@Data", MySqlDbType.VarChar, 255).Value = txtDescrip.Text;

                                        com.Parameters.Add("@Amount", MySqlDbType.Decimal, 10).Value = txtPaidMoney.Text;
                                        MySqlCommand com2 = new MySqlCommand("select Bank_Stock from bank where Bank_ID=" + cmbBank.SelectedValue, dbconnection);
                                        double amount2 = Convert.ToDouble(com2.ExecuteScalar().ToString());
                                        amount2 -= Convert.ToDouble(selRow["المبلغ"].ToString());
                                        amount2 += outParse;
                                        MySqlCommand com3 = new MySqlCommand("update bank set Bank_Stock=" + amount2 + " where Bank_ID=" + cmbBank.SelectedValue, dbconnection);
                                        com3.ExecuteNonQuery();
                                        
                                        if (txtVisaType.Text != "")
                                        {
                                            com.Parameters.Add("@Visa_Type", MySqlDbType.VarChar, 255).Value = txtVisaType.Text;
                                        }
                                        else
                                        {
                                            com.Parameters.Add("@Visa_Type", MySqlDbType.VarChar, 255).Value = null;
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

                                        com.ExecuteNonQuery();

                                        //////////insert categories/////////
                                        query = "update transition_categories_money set a200=@a200,a100=@a100,a50=@a50,a20=@a20,a10=@a10,a5=@a5,a1=@a1,aH=@aH,aQ=@aQ,r200=@r200,r100=@r100,r50=@r50,r20=@r20,r10=@r10,r5=@r5,r1=@r1,rH=@rH,rQ=@rQ where Transition_ID=" + selRow[0].ToString();
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
                                        //com.Parameters.Add("@Transition_ID", MySqlDbType.Int16, 11).Value = Convert.ToInt16(selRow[0].ToString());
                                        com.ExecuteNonQuery();
                                        flagCategoriesSuccess = false;

                                        //////////record editing/////////////
                                        query = "insert into usercontrol (UserControl_UserID,UserControl_TableName,UserControl_Status,UserControl_RecordID,UserControl_Date,UserControl_Reason) values(@UserControl_UserID,@UserControl_TableName,@UserControl_Status,@UserControl_RecordID,@UserControl_Date,@UserControl_Reason)";
                                        com = new MySqlCommand(query, dbconnection);
                                        com.Parameters.Add("@UserControl_UserID", MySqlDbType.Int16, 11).Value = UserControl.userID;
                                        com.Parameters.Add("@UserControl_TableName", MySqlDbType.VarChar, 255).Value = "transitions";
                                        com.Parameters.Add("@UserControl_Status", MySqlDbType.VarChar, 255).Value = "تعديل";
                                        com.Parameters.Add("@UserControl_RecordID", MySqlDbType.VarChar, 255).Value = selRow[0].ToString();
                                        com.Parameters.Add("@UserControl_Date", MySqlDbType.DateTime, 0).Value = DateTime.Now;
                                        com.Parameters.Add("@UserControl_Reason", MySqlDbType.VarChar, 255).Value = textBox.Text;
                                        com.ExecuteNonQuery();
                                        //////////////////////

                                        if (Convert.ToDouble(txtRestMoney.Text) - Convert.ToDouble(txtPaidMoney.Text) == 0 || checkBoxTaswya.Checked == true)
                                        {
                                            query = "update customer_bill set Paid_Status=1 where customer_bill.Branch_BillNumber=" + ID + " and customer_bill.Branch_ID=" + branchID;
                                        }
                                        else if (Convert.ToDouble(txtRestMoney.Text) - Convert.ToDouble(txtPaidMoney.Text) > 0)
                                        {
                                            query = "update customer_bill set Paid_Status=2 where customer_bill.Branch_BillNumber=" + ID + " and customer_bill.Branch_ID=" + branchID;
                                        }

                                        com = new MySqlCommand(query, dbconnection);
                                        com.ExecuteNonQuery();

                                        dbconnection.Close();

                                        //print bill
                                        printCategoriesBill();

                                        xtraTabPage.ImageOptions.Image = null;
                                        //Main.DepositCashShow.search();
                                        tabControlBank.TabPages.Remove(xtraTabPage);
                                    }
                                    else
                                    {
                                        MessageBox.Show("يجب كتابة السبب");
                                    }
                                }
                                else
                                { }
                            /*}
                            else
                            {
                                MessageBox.Show("برجاء التاكد من المبلغ المدفوع");
                                dbconnection.Close();
                                return;
                            }*/
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
            //connectionReader2.Close();
            //connectionReader1.Close();
            //connectionReader.Close();
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
                                arrOFPhaat[0] = Convert.ToInt16(dr["a200"]);
                                arrOFPhaat[1] = Convert.ToInt16(dr["a100"]);
                                arrOFPhaat[2] = Convert.ToInt16(dr["a50"]);
                                arrOFPhaat[3] = Convert.ToInt16(dr["a20"]);
                                arrOFPhaat[4] = Convert.ToInt16(dr["a10"]);
                                arrOFPhaat[5] = Convert.ToInt16(dr["a5"]);
                                arrOFPhaat[6] = Convert.ToInt16(dr["a1"]);
                                arrOFPhaat[7] = Convert.ToInt16(dr["aH"]);
                                arrOFPhaat[8] = Convert.ToInt16(dr["aQ"]);
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

        private void txtBillNumber_TextChanged(object sender, EventArgs e)
        {
            try
            {
                //double amont = 0;
                ID = Convert.ToInt16(selRow["الفاتورة"].ToString());

                myConnection.Open();
                string query = "SELECT sum(Amount) FROM transitions where Bill_Number=" + ID + " and Branch_ID=" + branchID + " and Transition='ايداع' group by Bill_Number";
                MySqlCommand com = new MySqlCommand(query, myConnection);
                if (com.ExecuteScalar() != null)
                {
                    paidAmount = Convert.ToDouble(com.ExecuteScalar().ToString());
                }
                else
                {
                    paidAmount = 0;
                }

                query = "SELECT customer_bill.Employee_Name FROM customer_bill where customer_bill.Branch_BillNumber=" + ID + " and customer_bill.Branch_ID=" + branchID;
                com = new MySqlCommand(query, myConnection);
                if (com.ExecuteScalar() != null)
                {
                    ConfirmEmp = com.ExecuteScalar().ToString();
                }

                //query = "SELECT sum(Amount) FROM transitions where Bill_Number=" + ID + " and Branch_ID=" + branchID + " and Transition='سحب' group by Bill_Number";
                //com = new MySqlCommand(query, myConnection);
                //if (com.ExecuteScalar() != null)
                //{
                //    amont = Convert.ToDouble(com.ExecuteScalar().ToString());
                //}
                //myConnection.Close();

                //paidAmount -= amont;

                dbconnection.Open();
                query = "select Total_CostAD from customer_bill where Branch_BillNumber=" + ID + " and Branch_ID=" + branchID + " and Type_Buy='كاش'";
                com = new MySqlCommand(query, dbconnection);
                txtTotalCost.Text = com.ExecuteScalar().ToString();
                
                double total = Convert.ToDouble(txtTotalCost.Text);
                txtRestMoney.Text = (total - paidAmount).ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
            myConnection.Close();
        }
        
        private void txtBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (loaded || loadedPayType)
                {
                    xtraTabPage = getTabPage("تعديل ايداع-كاش");
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
            }
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
            cmbBranch.Text = selRow["الفرع"].ToString();
            branchID = Convert.ToInt16(selRow["Branch_ID"].ToString());
            
            dbconnection.Close();
            txtBillNumber.Text = selRow["الفاتورة"].ToString();

            Transaction_Type = selRow["طريقة الدفع"].ToString();
            if (selRow["طريقة الدفع"].ToString() == "نقدى")
            {
                radioButtonSafe.Checked = true;
                radCash.Checked = true;
                radCredit.Enabled = false;
                radioButtonBank.Enabled = false;
                radBankAccount.Enabled = false;
                radVisa.Enabled = false;
            }
            else if (selRow["طريقة الدفع"].ToString() == "شيك")
            {
                radioButtonSafe.Checked = true;
                radCredit.Checked = true;
                radCash.Enabled = false;
                radioButtonBank.Enabled = false;
                radBankAccount.Enabled = false;
                radVisa.Enabled = false;
            }
            else if (selRow["طريقة الدفع"].ToString() == "حساب بنكى")
            {
                radioButtonBank.Checked = true;
                radBankAccount.Checked = true;
                radCredit.Enabled = false;
                radioButtonSafe.Enabled = false;
                radCash.Enabled = false;
                radVisa.Enabled = false;
            }
            else if (selRow["طريقة الدفع"].ToString() == "فيزا")
            {
                radioButtonBank.Checked = true;
                radVisa.Checked = true;
                radCredit.Enabled = false;
                radioButtonSafe.Enabled = false;
                radBankAccount.Enabled = false;
                radCash.Enabled = false;
            }
            cmbBank.Text = selRow["الخزينة"].ToString();
            cmbBank.SelectedValue = selRow["Bank_ID"].ToString();

            txtPaidMoney.Text = selRow["المبلغ"].ToString();
            if (selRow["تاريخ الاستحقاق"].ToString() != "")
            {
                dateEdit1.Text = Convert.ToDateTime(selRow["تاريخ الاستحقاق"].ToString()).ToShortDateString();
            }
            txtCheckNumber.Text = selRow["رقم الشيك/الكارت"].ToString();
            txtVisaType.Text = selRow["نوع الكارت"].ToString();
            txtOperationNumber.Text = selRow["رقم العملية"].ToString();
            txtDescrip.Text = selRow["البيان"].ToString();

            dbconnection.Open();
            query = "select * from transition_categories_money where Transition_ID=" + selRow[0].ToString();
            MySqlCommand com = new MySqlCommand(query, dbconnection);
            MySqlDataReader dr = com.ExecuteReader();
            while (dr.Read())
            {
                t200.Text = dr["a200"].ToString();
                if (dr["a200"].ToString() != "")
                {
                    arrPaidMoney[0] = Convert.ToInt16(dr["a200"].ToString());
                }
                t100.Text = dr["a100"].ToString();
                if (dr["a100"].ToString() != "")
                {
                    arrPaidMoney[1] = Convert.ToInt16(dr["a100"].ToString());
                }
                t50.Text = dr["a50"].ToString();
                if (dr["a50"].ToString() != "")
                {
                    arrPaidMoney[2] = Convert.ToInt16(dr["a50"].ToString());
                }
                t20.Text = dr["a20"].ToString();
                if (dr["a20"].ToString() != "")
                {
                    arrPaidMoney[3] = Convert.ToInt16(dr["a20"].ToString());
                }
                t10.Text = dr["a10"].ToString();
                if (dr["a10"].ToString() != "")
                {
                    arrPaidMoney[4] = Convert.ToInt16(dr["a10"].ToString());
                }
                t5.Text = dr["a5"].ToString();
                if (dr["a5"].ToString() != "")
                {
                    arrPaidMoney[5] = Convert.ToInt16(dr["a5"].ToString());
                }
                t1.Text = dr["a1"].ToString();
                if (dr["a1"].ToString() != "")
                {
                    arrPaidMoney[6] = Convert.ToInt16(dr["a1"].ToString());
                }
                tH.Text = dr["aH"].ToString();
                if (dr["aH"].ToString() != "")
                {
                    arrPaidMoney[7] = Convert.ToInt16(dr["aH"].ToString());
                }
                tQ.Text = dr["aQ"].ToString();
                if (dr["aQ"].ToString() != "")
                {
                    arrPaidMoney[8] = Convert.ToInt16(dr["aQ"].ToString());
                }
                ////****************************************////
                r200.Text = dr["r200"].ToString();
                if (dr["r200"].ToString() != "")
                {
                    arrRestMoney[0] = Convert.ToInt16(dr["r200"].ToString());
                }
                r100.Text = dr["r100"].ToString();
                if (dr["r100"].ToString() != "")
                {
                    arrRestMoney[1] = Convert.ToInt16(dr["r100"].ToString());
                }
                r50.Text = dr["r50"].ToString();
                if (dr["r50"].ToString() != "")
                {
                    arrRestMoney[2] = Convert.ToInt16(dr["r50"].ToString());
                }
                r20.Text = dr["r20"].ToString();
                if (dr["r20"].ToString() != "")
                {
                    arrRestMoney[3] = Convert.ToInt16(dr["r20"].ToString());
                }
                r10.Text = dr["r10"].ToString();
                if (dr["r10"].ToString() != "")
                {
                    arrRestMoney[4] = Convert.ToInt16(dr["r10"].ToString());
                }
                r5.Text = dr["r5"].ToString();
                if (dr["r5"].ToString() != "")
                {
                    arrRestMoney[5] = Convert.ToInt16(dr["r5"].ToString());
                }
                r1.Text = dr["r1"].ToString();
                if (dr["r1"].ToString() != "")
                {
                    arrRestMoney[6] = Convert.ToInt16(dr["r1"].ToString());
                }
                rH.Text = dr["rH"].ToString();
                if (dr["rH"].ToString() != "")
                {
                    arrRestMoney[7] = Convert.ToInt16(dr["rH"].ToString());
                }
                rQ.Text = dr["rQ"].ToString();
                if (dr["rQ"].ToString() != "")
                {
                    arrRestMoney[8] = Convert.ToInt16(dr["rQ"].ToString());
                }
            }
            dr.Close();
            dbconnection.Close();

            loaded = true;
        }

        void printCategoriesBill()
        {
            Print_CategoriesBill_Report f = new Print_CategoriesBill_Report();
            if (selRow["Client_ID"].ToString() != "")
            {
                f.PrintInvoice(DateTime.Now, selRow[0].ToString(), UserControl.EmpBranchName, cmbBranch.Text, ID, selRow["العميل"].ToString() + " " + selRow["Client_ID"].ToString(), Convert.ToDateTime(selRow["التاريخ"].ToString()), Convert.ToDouble(txtPaidMoney.Text), Transaction_Type, cmbBank.Text, txtCheckNumber.Text, dateEdit1.Text, txtVisaType.Text, txtOperationNumber.Text, txtDescrip.Text, ConfirmEmp, selRow["الموظف"].ToString(), arrPaidMoney[0], arrPaidMoney[1], arrPaidMoney[2], arrPaidMoney[3], arrPaidMoney[4], arrPaidMoney[5], arrPaidMoney[6], arrPaidMoney[7], arrPaidMoney[8], arrRestMoney[0], arrRestMoney[1], arrRestMoney[2], arrRestMoney[3], arrRestMoney[4], arrRestMoney[5], arrRestMoney[6], arrRestMoney[7], arrRestMoney[8]);
            }
            else if (selRow["Customer_ID"].ToString() != "")
            {
                f.PrintInvoice(DateTime.Now, selRow[0].ToString(), UserControl.EmpBranchName, cmbBranch.Text, ID, selRow["المهندس/المقاول/التاجر"].ToString() + " " + selRow["Customer_ID"].ToString(), Convert.ToDateTime(selRow["التاريخ"].ToString()), Convert.ToDouble(txtPaidMoney.Text), Transaction_Type, cmbBank.Text, txtCheckNumber.Text, dateEdit1.Text, txtVisaType.Text, txtOperationNumber.Text, txtDescrip.Text, ConfirmEmp, selRow["الموظف"].ToString(), arrPaidMoney[0], arrPaidMoney[1], arrPaidMoney[2], arrPaidMoney[3], arrPaidMoney[4], arrPaidMoney[5], arrPaidMoney[6], arrPaidMoney[7], arrPaidMoney[8], arrRestMoney[0], arrRestMoney[1], arrRestMoney[2], arrRestMoney[3], arrRestMoney[4], arrRestMoney[5], arrRestMoney[6], arrRestMoney[7], arrRestMoney[8]);
            }
            f.ShowDialog();
            for (int i = 0; i < arrPaidMoney.Length; i++)
                arrPaidMoney[i] = arrRestMoney[i] = 0;
            for (int i = 0; i < arrRestMoney.Length; i++)
                arrRestMoney[i] = arrPaidMoney[i] = 0;
        }
    }
}

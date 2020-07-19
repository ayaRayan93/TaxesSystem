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
    public partial class BankPullPayAccount_Update : Form
    {
        MySqlConnection dbconnection;
        //int ClintID = 0;
        //int PayAccountID = 0;
        //bool successFlag = false;
        bool flag = false;
        //bool tempFlag = false;
        int branchID = 0;
        string PaymentMethod = "";
        int[] arrOFPhaat; //count of each catagory value of money in store
        int[] arrRestMoney;
        int[] arrPaidMoney;
        bool loadedBranch = false;
        public static bool updateBankPullPayAccountTextChangedFlag = false;
        XtraTabPage xtraTabPage;
        DataRowView selRow;

        public BankPullPayAccount_Update(DataRowView SelRow)
        {
            InitializeComponent();
            dbconnection = new MySqlConnection(connection.connectionString);
            selRow = SelRow;
            arrOFPhaat = new int[9];
            arrPaidMoney = new int[9];
            arrRestMoney = new int[9];

            cmbBank.AutoCompleteMode = AutoCompleteMode.Suggest;
            cmbBank.AutoCompleteSource = AutoCompleteSource.ListItems;

            cmbBranch.AutoCompleteMode = AutoCompleteMode.Suggest;
            cmbBranch.AutoCompleteSource = AutoCompleteSource.ListItems;

            this.dateEdit1.Properties.DisplayFormat.FormatString = "yyyy/MM/dd";
            this.dateEdit1.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dateEdit1.Properties.EditFormat.FormatString = "yyyy/MM/dd";
            this.dateEdit1.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dateEdit1.Properties.Mask.EditMask = "yyyy/MM/dd";
        }

        private void BankPullPayAccount_Update_Load(object sender, EventArgs e)
        {
            try
            {
                if (!loadedBranch)
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
        
        private void radioButtonSafe_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                RadioButton r = (RadioButton)sender;
                radCash.Enabled = true;
                radCredit.Enabled = true;
                radBankAccount.Enabled = false;
                //radVisa.Enabled = false;
                radBankAccount.Checked = false;
                //radVisa.Checked = false;
                layoutControlItemBank.Text = "خزينة";
                layoutControlItemBank.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                labelBank.Visible = true;
                labelBank.Text = "*";
                layoutControlItemMoney.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                labelPaidMoney.Visible = true;
                labelPaidMoney.Text = "*";
                layoutControlItemComment.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                labelDescrip.Visible = true;
                
                layoutControlItemOperationNumber.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                labelOperationNumber.Visible = false;
                labelOperationNumber.Text = "";

                radCash.Checked = true;
                string query = "select * from bank where Branch_ID=" + branchID + " and Bank_Type='خزينة'";
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

        private void radCash_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                RadioButton r = (RadioButton)sender;
                PaymentMethod = r.Text;
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
                //radVisa.Enabled = true;
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
                layoutControlItemBank.Text = "بنك";
                layoutControlItemPayDate.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                labelDate.Visible = true;
                labelDate.Text = "*";
                
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
                PaymentMethod = r.Text;
                layoutControlItemBank.Text = "فيزا";
                layoutControlItemPayDate.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                labelDate.Visible = false;
                labelDate.Text = "";
                
                layoutControlItemOperationNumber.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                labelOperationNumber.Visible = true;
                labelOperationNumber.Text = "*";

                string query = "select * from bank where Branch_ID=" + branchID + " and Bank_Type='فيزا' and BankVisa_ID is not null";
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
        
        private void txtBillNum_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    //if (int.TryParse(txtBillNum.Text, out PayAccountID))
                    //{
                    //    dbconnection.Open();

                    //    string query = "select * FROM pay_account where PayAccount_ID=" + PayAccountID + " and Taswya='لا'";
                    //    MySqlCommand com = new MySqlCommand(query, dbconnection);
                    //    MySqlDataReader dr = com.ExecuteReader();

                    //    while (dr.Read())
                    //    {
                    //        tempFlag = true;
                    //        ClintID = Convert.ToInt32(dr["Client_ID"].ToString());
                    //        txtName.Text = dr["Client_Name"].ToString();
                    //        txtPullMoney.Text = dr["Money"].ToString();
                    //    }
                    //    dr.Close();

                    //    if (tempFlag == false)
                    //    {
                    //        MessageBox.Show("لا يوجد فاتورة بهذا الرقم او تمت التسوية من قبل");
                    //        ClintID = 0;
                    //        txtName.Text = "";
                    //        txtPullMoney.Text = "";
                    //    }
                    //}
                    //else
                    //{
                    //    ClintID = 0;
                    //    txtName.Text = "";
                    //    txtPullMoney.Text = "";
                    //}
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
                bool check = false;
                if (PaymentMethod == "نقدى")
                {
                    check = (txtBillNum.Text != "" && txtName.Text != "" && cmbBranch.Text != "" && cmbBank.Text != "" && txtPullMoney.Text != "");
                }
                else if (PaymentMethod == "شيك")
                {
                    check = (txtBillNum.Text != "" && txtName.Text != "" && cmbBranch.Text != "" && cmbBank.Text != "" && txtPullMoney.Text != "" && dateEdit1.Text != "" && txtCheckNumber.Text != "");
                }
                else if (PaymentMethod == "حساب بنكى")
                {
                    check = (txtBillNum.Text != "" && txtName.Text != "" && cmbBranch.Text != "" && cmbBank.Text != "" && txtPullMoney.Text != "" && dateEdit1.Text != "");
                }

                if (check)
                {
                    double outParse;
                    if (double.TryParse(txtPullMoney.Text, out outParse))
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

                                string query = "update transitions set Amount=@Amount,Data=@Data,PayDay=@PayDay,Check_Number=@Check_Number,Operation_Number=@Operation_Number where Transition_ID=" + selRow[0].ToString();
                                MySqlCommand com = new MySqlCommand(query, dbconnection);
                                
                                com.Parameters.Add("@Operation_Number", MySqlDbType.Int16, 11).Value = opNumString;
                                com.Parameters.Add("@Data", MySqlDbType.VarChar, 255).Value = txtDescrip.Text;

                                com.Parameters.Add("@Amount", MySqlDbType.Decimal, 10).Value = txtPullMoney.Text;
                                MySqlCommand com2 = new MySqlCommand("select Bank_Stock from bank where Bank_ID=" + cmbBank.SelectedValue, dbconnection);
                                double amount2 = Convert.ToDouble(com2.ExecuteScalar().ToString());
                                amount2 += Convert.ToDouble(selRow["المبلغ"].ToString());
                                amount2 -= outParse;
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

                                com.ExecuteNonQuery();

                                //////////record adding/////////////
                                string TransitionID = selRow[0].ToString();

                                query = "insert into usercontrol (UserControl_UserID,UserControl_TableName,UserControl_Status,UserControl_RecordID,UserControl_Date,UserControl_Reason) values(@UserControl_UserID,@UserControl_TableName,@UserControl_Status,@UserControl_RecordID,@UserControl_Date,@UserControl_Reason)";
                                com = new MySqlCommand(query, dbconnection);
                                com.Parameters.Add("@UserControl_UserID", MySqlDbType.Int16, 11).Value = UserControl.userID;
                                com.Parameters.Add("@UserControl_TableName", MySqlDbType.VarChar, 255).Value = "transitions";
                                com.Parameters.Add("@UserControl_Status", MySqlDbType.VarChar, 255).Value = "تعديل";
                                com.Parameters.Add("@UserControl_RecordID", MySqlDbType.VarChar, 255).Value = TransitionID;
                                com.Parameters.Add("@UserControl_Date", MySqlDbType.DateTime, 0).Value = DateTime.Now;
                                com.Parameters.Add("@UserControl_Reason", MySqlDbType.VarChar, 255).Value = textBox.Text;
                                com.ExecuteNonQuery();
                                //////////////////////
                                dbconnection.Close();

                                MessageBox.Show("تم");

                                updateBankPullPayAccountTextChangedFlag = false;
                                xtraTabPage.ImageOptions.Image = null;
                                MainForm.tabControlBank.TabPages.Remove(BankPullPayAccount_Report.MainTabPageUpdatePullPayAccount);
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
        
        private void cmbBranch_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (loadedBranch)
                {
                    if (int.TryParse(cmbBranch.SelectedValue.ToString(), out branchID))
                    {
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void PaidMoney_KeyDown(object sender, KeyEventArgs e)
        {
            double totalPaid = 0;

            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    double total = 0;
                    if (txtPullMoney.Text != "" && double.TryParse(txtPullMoney.Text, out total))
                    {
                        if (cmbBank.Text == "")
                        {
                            MessageBox.Show("يجب ان تختار خزينة");
                            return;
                        }
                        dbconnection.Open();

                        string query = "select ID from categories_money where Bank_ID=" + cmbBank.SelectedValue;
                        MySqlCommand com = new MySqlCommand(query, dbconnection);
                        if (com.ExecuteScalar() == null)
                        {
                            MessageBox.Show("هذه الخزينة ليس بها فئات");
                            dbconnection.Close();
                            return;
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
                                    if (arrOFPhaat[0] >= num)
                                    {
                                        arrOFPhaat[0] += arrPaidMoney[0];
                                        arrPaidMoney[0] = num;
                                        arrOFPhaat[0] -= num;
                                        t100.Focus();
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
                                if (int.TryParse(t100.Text, out num))
                                {
                                    if (arrOFPhaat[1] >= num)
                                    {
                                        arrOFPhaat[1] += arrPaidMoney[1];
                                        arrPaidMoney[1] = num;
                                        arrOFPhaat[1] -= num;
                                        t50.Focus();
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
                                if (int.TryParse(t50.Text, out num))
                                {
                                    if (arrOFPhaat[2] >= num)
                                    {
                                        arrOFPhaat[2] += arrPaidMoney[2];
                                        arrPaidMoney[2] = num;
                                        arrOFPhaat[2] -= num;
                                        t20.Focus();
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
                                if (int.TryParse(t20.Text, out num))
                                {
                                    if (arrOFPhaat[3] >= num)
                                    {
                                        arrOFPhaat[3] += arrPaidMoney[3];
                                        arrPaidMoney[3] = num;
                                        arrOFPhaat[3] -= num;
                                        t10.Focus();
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
                                if (int.TryParse(t10.Text, out num))
                                {
                                    if (arrOFPhaat[4] >= num)
                                    {
                                        arrOFPhaat[4] += arrPaidMoney[4];
                                        arrPaidMoney[4] = num;
                                        arrOFPhaat[4] -= num;
                                        t5.Focus();
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
                                if (int.TryParse(t5.Text, out num))
                                {
                                    if (arrOFPhaat[5] >= num)
                                    {
                                        arrOFPhaat[5] += arrPaidMoney[5];
                                        arrPaidMoney[5] = num;
                                        arrOFPhaat[5] -= num;
                                        t1.Focus();
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
                                if (int.TryParse(t1.Text, out num))
                                {
                                    if (arrOFPhaat[6] >= num)
                                    {
                                        arrOFPhaat[6] += arrPaidMoney[6];
                                        arrPaidMoney[6] = num;
                                        arrOFPhaat[6] -= num;
                                        tH.Focus();
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
                                if (int.TryParse(tH.Text, out num))
                                {
                                    if (arrOFPhaat[7] >= num)
                                    {
                                        arrOFPhaat[7] += arrPaidMoney[7];
                                        arrPaidMoney[7] = num;
                                        arrOFPhaat[7] -= num;
                                        tQ.Focus();
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
                                if (int.TryParse(tQ.Text, out num))
                                {
                                    if (arrOFPhaat[8] >= num)
                                    {
                                        arrOFPhaat[8] += arrPaidMoney[8];
                                        arrPaidMoney[8] = num;
                                        arrOFPhaat[8] -= num;
                                        r200.Focus();
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

                        totalPaid = arrPaidMoney[0] * 200 + arrPaidMoney[1] * 100 + arrPaidMoney[2] * 50 + arrPaidMoney[3] * 20 + arrPaidMoney[4] * 10 + arrPaidMoney[5] * 5 + arrPaidMoney[6] + arrPaidMoney[7] * 0.5 + arrPaidMoney[8] * 0.25;
                        PaidMoney.Text = totalPaid.ToString();
                        if ((total - totalPaid) + Convert.ToDouble(RestMoney.Text) > 0)
                        {
                            txtPaidRest.Text = ((total - totalPaid) + Convert.ToDouble(RestMoney.Text)).ToString();
                            txtPaidRest2.Text = "0";
                            layoutControlItemRest.AppearanceItemCaption.ForeColor = Color.FromArgb(140, 140, 140);
                            layoutControlItemPull.AppearanceItemCaption.ForeColor = Color.Red;
                        }
                        else if ((total - totalPaid) + Convert.ToDouble(RestMoney.Text) == 0)
                        {
                            txtPaidRest.Text = "0";
                            txtPaidRest2.Text = "0";
                            layoutControlItemPull.AppearanceItemCaption.ForeColor = Color.FromArgb(140, 140, 140);
                            layoutControlItemRest.AppearanceItemCaption.ForeColor = Color.FromArgb(140, 140, 140);
                        }
                        else
                        {
                            txtPaidRest.Text = "0";
                            double sub = (total - totalPaid);

                            txtPaidRest2.Text = (-1 * (sub + Convert.ToDouble(RestMoney.Text))).ToString();
                            layoutControlItemRest.AppearanceItemCaption.ForeColor = Color.Red;
                            layoutControlItemPull.AppearanceItemCaption.ForeColor = Color.FromArgb(140, 140, 140);
                        }

                        if ((Convert.ToDouble(txtPaidRest.Text) == 0) && (Convert.ToDouble(txtPaidRest2.Text) == 0))
                        {
                            dbconnection.Open();
                            query = "update categories_money set a200=" + arrOFPhaat[0] + ",a100=" + arrOFPhaat[1] + ",a50=" + arrOFPhaat[2] + ",a20=" + arrOFPhaat[3] + ",a10=" + arrOFPhaat[4] + ",a5=" + arrOFPhaat[5] + ",a1=" + arrOFPhaat[6] + ",aH=" + arrOFPhaat[7] + ",aQ=" + arrOFPhaat[8] + " where Bank_ID=" + cmbBank.SelectedValue;
                            com = new MySqlCommand(query, dbconnection);
                            com.ExecuteNonQuery();
                            MessageBox.Show("تم");
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
                            layoutControlItemPull.AppearanceItemCaption.ForeColor = Color.FromArgb(140, 140, 140);
                            layoutControlItemRest.AppearanceItemCaption.ForeColor = Color.FromArgb(140, 140, 140);
                            for (int i = 0; i < arrPaidMoney.Length; i++)
                                arrPaidMoney[i] = arrRestMoney[i] = 0;
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
                if (txtPullMoney.Text != "" && double.TryParse(txtPullMoney.Text, out total))
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
                                    arrOFPhaat[0] -= arrRestMoney[0];
                                    arrRestMoney[0] = num;
                                    arrOFPhaat[0] += num;
                                    r100.Focus();
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
                                    arrOFPhaat[1] -= arrRestMoney[1];
                                    arrRestMoney[1] = num;
                                    arrOFPhaat[1] += num;
                                    r50.Focus();
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
                                    arrOFPhaat[2] -= arrRestMoney[2];
                                    arrRestMoney[2] = num;
                                    arrOFPhaat[2] += num;
                                    r20.Focus();
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
                                    arrOFPhaat[3] -= arrRestMoney[3];
                                    arrRestMoney[3] = num;
                                    arrOFPhaat[3] += num;
                                    r10.Focus();
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
                                    arrOFPhaat[4] -= arrRestMoney[4];
                                    arrRestMoney[4] = num;
                                    arrOFPhaat[4] += num;
                                    r5.Focus();
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
                                    arrOFPhaat[5] -= arrRestMoney[5];
                                    arrRestMoney[5] = num;
                                    arrOFPhaat[5] += num;
                                    r1.Focus();
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
                                    arrOFPhaat[6] -= arrRestMoney[6];
                                    arrRestMoney[6] = num;
                                    arrOFPhaat[6] += num;
                                    rH.Focus();
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
                                    arrOFPhaat[7] -= arrRestMoney[7];
                                    arrRestMoney[7] = num;
                                    arrOFPhaat[7] += num;
                                    rQ.Focus();
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
                                    arrOFPhaat[8] -= arrRestMoney[8];
                                    arrRestMoney[8] = num;
                                    arrOFPhaat[8] += num;
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

                        if ((Convert.ToDouble(RestMoney.Text) - (-1 * (Convert.ToDouble(txtPullMoney.Text) - Convert.ToDouble(PaidMoney.Text)))) < 0)
                        {
                            txtPaidRest.Text = "0";
                            txtPaidRest2.Text = (-1 * ((Convert.ToDouble(RestMoney.Text) - (-1 * (Convert.ToDouble(txtPullMoney.Text) - Convert.ToDouble(PaidMoney.Text)))))).ToString();
                            layoutControlItemRest.AppearanceItemCaption.ForeColor = Color.Red;
                            layoutControlItemPull.AppearanceItemCaption.ForeColor = Color.FromArgb(140, 140, 140);
                        }
                        else if ((Convert.ToDouble(RestMoney.Text) - (-1 * (Convert.ToDouble(txtPullMoney.Text) - Convert.ToDouble(PaidMoney.Text)))) == 0)
                        {
                            txtPaidRest2.Text = "0";
                            layoutControlItemRest.AppearanceItemCaption.ForeColor = Color.FromArgb(140, 140, 140);

                            txtPaidRest.Text = "0";
                            layoutControlItemPull.AppearanceItemCaption.ForeColor = Color.FromArgb(140, 140, 140);
                        }
                        else
                        {
                            double sub = (Convert.ToDouble(txtPullMoney.Text) - Convert.ToDouble(PaidMoney.Text));
                            txtPaidRest.Text = (Convert.ToDouble(RestMoney.Text) - (-1 * sub)).ToString();
                            txtPaidRest2.Text = "0";
                            layoutControlItemPull.AppearanceItemCaption.ForeColor = Color.Red;
                            layoutControlItemRest.AppearanceItemCaption.ForeColor = Color.FromArgb(140, 140, 140);
                        }

                        if ((Convert.ToDouble(txtPaidRest.Text) == 0) && (Convert.ToDouble(txtPaidRest2.Text) == 0))
                        {
                            dbconnection.Open();
                            string query = "update categories_money set a200=" + arrOFPhaat[0] + ",a100=" + arrOFPhaat[1] + ",a50=" + arrOFPhaat[2] + ",a20=" + arrOFPhaat[3] + ",a10=" + arrOFPhaat[4] + ",a5=" + arrOFPhaat[5] + ",a1=" + arrOFPhaat[6] + ",aH=" + arrOFPhaat[7] + ",aQ=" + arrOFPhaat[8] + " where Bank_ID=" + cmbBank.SelectedValue;
                            MySqlCommand com = new MySqlCommand(query, dbconnection);
                            com.ExecuteNonQuery();
                            MessageBox.Show("تم");
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
                            layoutControlItemPull.AppearanceItemCaption.ForeColor = Color.FromArgb(140, 140, 140);
                            layoutControlItemRest.AppearanceItemCaption.ForeColor = Color.FromArgb(140, 140, 140);
                            for (int i = 0; i < arrRestMoney.Length; i++)
                                arrRestMoney[i] = arrPaidMoney[i] = 0;
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
                if (loadedBranch)
                {
                    xtraTabPage = getTabPage("tabPageUpdatePullPayAccount");
                    if (!IsClear())
                    {
                        xtraTabPage.ImageOptions.Image = Properties.Resources.unsave;
                        updateBankPullPayAccountTextChangedFlag = true;
                    }
                    else
                    {
                        xtraTabPage.ImageOptions.Image = null;
                        updateBankPullPayAccountTextChangedFlag = false;
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
            cmbBranch.Text = selRow["الفرع"].ToString();
            branchID = Convert.ToInt32(cmbBranch.SelectedValue.ToString());

            txtBillNum.Text = selRow["الفاتورة"].ToString();
            txtName.Text = selRow["العميل"].ToString();
            if (selRow["طريقة الدفع"].ToString() == "نقدى")
            {
                radioButtonSafe.Checked = true;
                radCash.Checked = true;
                radCredit.Enabled = false;
                radioButtonBank.Enabled = false;
                radBankAccount.Enabled = false;
            }
            else if (selRow["طريقة الدفع"].ToString() == "شيك")
            {
                radioButtonSafe.Checked = true;
                radCredit.Checked = true;
                radCash.Enabled = false;
                radioButtonBank.Enabled = false;
                radBankAccount.Enabled = false;
            }
            else if (selRow["طريقة الدفع"].ToString() == "حساب بنكى")
            {
                radioButtonBank.Checked = true;
                radBankAccount.Checked = true;
                radCredit.Enabled = false;
                radioButtonSafe.Enabled = false;
                radCash.Enabled = false;
            }
            else if (selRow["طريقة الدفع"].ToString() == "فيزا")
            {
                radioButtonBank.Checked = true;
                radCredit.Enabled = false;
                radioButtonSafe.Enabled = false;
                radBankAccount.Enabled = false;
                radCash.Enabled = false;
            }
            cmbBank.Text = selRow["الخزينة"].ToString();

            txtPullMoney.Text = selRow["المبلغ"].ToString();
            if (selRow["تاريخ الاستحقاق"].ToString() != "")
            {
                dateEdit1.Text = Convert.ToDateTime(selRow["تاريخ الاستحقاق"].ToString()).ToShortDateString();
            }
            txtCheckNumber.Text = selRow["رقم الشيك"].ToString();
            txtOperationNumber.Text = selRow["رقم العملية"].ToString();
            txtDescrip.Text = selRow["البيان"].ToString();

            loadedBranch = true;
        }

        /*private void RecordPayAccount()
        {
            dbconnection.Open();

            string query = "update pay_account set Taswya=@Taswya where PayAccount_ID=" + PayAccountID;
            MySqlCommand com = new MySqlCommand(query, dbconnection);
            com.Parameters.Add("@Taswya", MySqlDbType.VarChar, 255).Value = "نعم";
            com.ExecuteNonQuery();

            //////////record adding/////////////
            query = "insert into usercontrol (UserControl_UserID,UserControl_TableName,UserControl_Status,UserControl_RecordID,UserControl_Date,UserControl_Reason) values(@UserControl_UserID,@UserControl_TableName,@UserControl_Status,@UserControl_RecordID,@UserControl_Date,@UserControl_Reason)";
            com = new MySqlCommand(query, dbconnection);
            com.Parameters.Add("@UserControl_UserID", MySqlDbType.Int16, 11).Value = UserControl.userID;
            com.Parameters.Add("@UserControl_TableName", MySqlDbType.VarChar, 255).Value = "pay_account";
            com.Parameters.Add("@UserControl_Status", MySqlDbType.VarChar, 255).Value = "تسوية";
            com.Parameters.Add("@UserControl_RecordID", MySqlDbType.VarChar, 255).Value = PayAccountID;
            com.Parameters.Add("@UserControl_Date", MySqlDbType.DateTime, 0).Value = DateTime.Now;
            com.Parameters.Add("@UserControl_Reason", MySqlDbType.VarChar, 255).Value = null;
            com.ExecuteNonQuery();
            //////////////////////

            dbconnection.Close();
            successFlag = true;
        }*/
    }
}

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
using System.Collections;

namespace TaxesSystem
{
    public partial class Bank_Updateold : Form
    {
        MySqlConnection conn;
        bool loaded = false;
        DataRowView selRow;
        XtraTabPage xtraTabPage;

        public Bank_Updateold(DataRowView SelRow)
        {
            InitializeComponent();
            conn = new MySqlConnection(connection.connectionString);
            selRow = SelRow;

            cmbBranch.AutoCompleteMode = AutoCompleteMode.Suggest;
            cmbBranch.AutoCompleteSource = AutoCompleteSource.ListItems;

            cmbBank.AutoCompleteMode = AutoCompleteMode.Suggest;
            cmbBank.AutoCompleteSource = AutoCompleteSource.ListItems;
        }

        private void Bank_Update_Load(object sender, EventArgs e)
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
            conn.Close();
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
                        check = (txtStock.Text != "" && dateEdit1.Text != "");
                    }
                    else if (cmbType.Text == "حساب بنكى")
                    {
                        check = (txtAccountType.Text != "" && txtStock.Text != "" &&  dateEdit1.Text != "");
                    }
                    else if(cmbType.Text == "فيزا")
                    {
                        check = (txtAccountNumber.Text != "" && txtAccountType.Text != "" && txtStock.Text != "" && dateEdit1.Text != "");
                    }

                    if (check)
                    {
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
                                conn.Close();
                                double stock = 0;
                                if (double.TryParse(txtStock.Text, out stock))
                                {
                                    MySqlCommand command = conn.CreateCommand();
                                    command.CommandText = "update bank set Bank_Stock=?Bank_Stock,Start_Date=?Start_Date,Bank_Info=?Bank_Info,Bank_Account=?Bank_Account,BankAccount_Type=?BankAccount_Type where Bank_ID=" + selRow[0].ToString();

                                    if (cmbType.Text == "خزينة")
                                    {
                                        command.Parameters.AddWithValue("?Bank_Account", "");
                                        command.Parameters.AddWithValue("?BankAccount_Type", "");
                                    }
                                    else if (cmbType.Text == "حساب بنكى")
                                    {
                                        command.Parameters.AddWithValue("?Bank_Account", txtAccountNumber.Text);
                                        command.Parameters.AddWithValue("?BankAccount_Type", txtAccountType.Text);
                                    }
                                    else if (cmbType.Text == "فيزا")
                                    {
                                        command.Parameters.AddWithValue("?Bank_Account", txtAccountNumber.Text);
                                        command.Parameters.AddWithValue("?BankAccount_Type", txtAccountType.Text);
                                    }
                                    command.Parameters.AddWithValue("?Bank_Stock", stock);
                                    command.Parameters.AddWithValue("?Start_Date", dateEdit1.DateTime.Date);
                                    command.Parameters.AddWithValue("?Bank_Info", txtInformation.Text);
                                    //command.Parameters.AddWithValue("?User_ID", UserControl.userID);
                                    //command.Parameters.AddWithValue("?User_Name", UserControl.userName);
                                    conn.Open();
                                    command.ExecuteNonQuery();

                                    //////////record editing/////////////
                                    string query = "insert into usercontrol (UserControl_UserID,UserControl_TableName,UserControl_Status,UserControl_RecordID,UserControl_Date,UserControl_Reason) values(@UserControl_UserID,@UserControl_TableName,@UserControl_Status,@UserControl_RecordID,@UserControl_Date,@UserControl_Reason)";
                                    command = new MySqlCommand(query, conn);
                                    command.Parameters.Add("@UserControl_UserID", MySqlDbType.Int16, 11).Value = UserControl.userID;
                                    command.Parameters.Add("@UserControl_TableName", MySqlDbType.VarChar, 255).Value = "bank";
                                    command.Parameters.Add("@UserControl_Status", MySqlDbType.VarChar, 255).Value = "تعديل";
                                    command.Parameters.Add("@UserControl_RecordID", MySqlDbType.VarChar, 255).Value = selRow[0].ToString();
                                    command.Parameters.Add("@UserControl_Date", MySqlDbType.DateTime, 0).Value = DateTime.Now;
                                    command.Parameters.Add("@UserControl_Reason", MySqlDbType.VarChar, 255).Value = textBox.Text;
                                    command.ExecuteNonQuery();
                                    //////////////////////
                                    
                                    query = "delete from bank_employee where Bank_ID=" + selRow[0].ToString();
                                    command = new MySqlCommand(query, conn);
                                    command.ExecuteNonQuery();

                                    for (int i = 0; i < checkedListBoxControlUserID.ItemCount; i++)
                                    {
                                        query = "insert into bank_employee (Bank_ID,Employee_ID) values (@Bank_ID,@Employee_ID)";
                                        command = new MySqlCommand(query, conn);
                                        command.Parameters.AddWithValue("@Bank_ID", selRow[0].ToString());
                                        command.Parameters.AddWithValue("@Employee_ID", checkedListBoxControlUserID.Items[i].Value.ToString());
                                        command.ExecuteNonQuery();
                                    }

                                    conn.Close();
                                    //xtraTabPage.ImageOptions.Image = null;
                                    xtraTabPage = getTabPage("tabPageUpdateBank");
                                    MainForm.tabControlBank.TabPages.Remove(xtraTabPage);
                                }
                                else
                                {
                                    MessageBox.Show("الرصيد يجب ان يكون رقم");
                                }
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
                        MessageBox.Show("برجاء ادخال البيانات كاملة");
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            conn.Close();
        }

        private void txtBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (loaded)
                {
                    xtraTabPage = getTabPage("tabPageUpdateBank");
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
            conn.Open();
            string query = "select * from branch";
            MySqlDataAdapter da = new MySqlDataAdapter(query, conn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            cmbBranch.DataSource = dt;
            cmbBranch.DisplayMember = dt.Columns["Branch_Name"].ToString();
            cmbBranch.ValueMember = dt.Columns["Branch_ID"].ToString();
            cmbBranch.SelectedIndex = -1;

            query = "select * from bank where Branch_ID is null and BankVisa_ID is null";
            da = new MySqlDataAdapter(query, conn);
            dt = new DataTable();
            da.Fill(dt);
            cmbBank.DataSource = dt;
            cmbBank.DisplayMember = dt.Columns["Bank_Name"].ToString();
            cmbBank.ValueMember = dt.Columns["Bank_ID"].ToString();
            cmbBank.SelectedIndex = -1;

            query = "select Employee_ID,Employee_Name from employee where employee.Employee_ID Not in(" + "select bank_employee.Employee_ID from bank_employee where bank_employee.Bank_ID <> " + selRow[0].ToString() + ")";
            da = new MySqlDataAdapter(query, conn);
            dt = new DataTable();
            da.Fill(dt);
            comBankUsers.DataSource = dt;
            comBankUsers.DisplayMember = dt.Columns["Employee_Name"].ToString();
            comBankUsers.ValueMember = dt.Columns["Employee_ID"].ToString();
            comBankUsers.SelectedIndex = -1;

            query = "select bank_employee.Employee_ID,employee.Employee_Name from bank_employee INNER JOIN employee ON employee.Employee_ID = bank_employee.Employee_ID where bank_employee.Bank_ID = " + selRow[0].ToString();
            MySqlCommand com = new MySqlCommand(query, conn);
            MySqlDataReader dr = com.ExecuteReader();
            while (dr.Read())
            {
                checkedListBoxControlUserID.Items.Add(dr["Employee_ID"].ToString());
                checkedListBoxControlUser.Items.Add(dr["Employee_Name"].ToString());
            }

            cmbType.Text = selRow[1].ToString();

            if (cmbType.Text == "خزينة")
            {
                layoutControlItemBranch.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                labelBranch.Text = "*";
                cmbBranch.SelectedValue = selRow[3].ToString();
                layoutControlItemName.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                labelName.Text = "*";
                txtName.Text = selRow[2].ToString();
                layoutControlItemStock.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                txtStock.Text = selRow[5].ToString();
                labelStock.Text = "*";
                layoutControlItemDate.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                dateEdit1.Text = Convert.ToDateTime(selRow[6].ToString()).ToShortDateString();
                labelDate.Text = "*";
                layoutControlItemInformation.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                txtInformation.Text = selRow[12].ToString();
                labelInfo.Text = "";
                layoutControlItemAccountNumber.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                labelAccountNumber.Text = "";
                layoutControlItemAccountType.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                labelAccountType.Text = "";
                layoutControlItemBank.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                labelBank.Text = "";
            }
            else if (cmbType.Text == "حساب بنكى")
            {
                layoutControlItemBranch.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                labelBranch.Text = "";
                layoutControlItemName.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                txtName.Text = selRow[2].ToString();
                labelName.Text = "*";
                layoutControlItemStock.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                txtStock.Text = selRow[5].ToString();
                labelStock.Text = "*";
                layoutControlItemDate.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                dateEdit1.Text = Convert.ToDateTime(selRow[6].ToString()).ToShortDateString();
                labelDate.Text = "*";
                layoutControlItemInformation.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                txtInformation.Text = selRow[12].ToString();
                labelInfo.Text = "";
                layoutControlItemAccountNumber.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                txtAccountNumber.Text = selRow[7].ToString();
                txtAccountNumber.Enabled = false;
                labelAccountNumber.Text = "*";
                layoutControlItemAccountType.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                txtAccountType.Text = selRow[8].ToString();
                labelAccountType.Text = "*";
                layoutControlItemBank.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                labelBank.Text = "";
            }
            else if (cmbType.Text == "فيزا")
            {
                layoutControlItemBranch.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                cmbBranch.SelectedValue = selRow[3].ToString();
                labelBranch.Text = "*";
                layoutControlItemName.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                txtName.Text = selRow[2].ToString();
                labelName.Text = "*";
                layoutControlItemStock.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                txtStock.Text = selRow[5].ToString();
                labelStock.Text = "*";
                layoutControlItemDate.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                dateEdit1.Text = Convert.ToDateTime(selRow[6].ToString()).ToShortDateString();
                labelDate.Text = "*";
                layoutControlItemInformation.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                txtInformation.Text = selRow[12].ToString();
                labelInfo.Text = "";
                layoutControlItemAccountNumber.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                txtAccountNumber.Text = selRow[7].ToString();
                labelAccountNumber.Text = "*";
                layoutControlItemAccountType.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                txtAccountType.Text = selRow[8].ToString();
                labelAccountType.Text = "*";
                layoutControlItemBank.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                cmbBank.SelectedValue = selRow[9].ToString();
                labelBank.Text = "*";
                layoutControlItemID.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                txtMachineID.Text = selRow[11].ToString();
                labelID.Text = "*";
            }

            loaded = true;
        }

        private void btnAddUserToBank_Click(object sender, EventArgs e)
        {
            try
            {
                if (comBankUsers.Text != "" && comBankUsers.SelectedIndex != -1 && comBankUsers.SelectedValue != null)
                {
                    conn.Open();

                    for (int i = 0; i < checkedListBoxControlUserID.ItemCount; i++)
                    {
                        if (comBankUsers.SelectedValue.ToString() == checkedListBoxControlUserID.Items[i].Value.ToString())
                        {
                            MessageBox.Show("هذا الموظف تم اضافتة");
                            conn.Close();
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
            conn.Close();
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

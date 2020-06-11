using DevExpress.XtraEditors;
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
    public partial class Shipping_Record : Form
    {
        MySqlConnection dbconnection;
        string Customer_Type = "";
        XtraTabPage xtraTabPage;
        bool loaded = false;
        DataRow row1;
        int id = 0;
        int ShippingID = 0;

        public Shipping_Record()
        {
            InitializeComponent();
            dbconnection = new MySqlConnection(connection.connectionString);
            
            comArea.AutoCompleteMode = AutoCompleteMode.Suggest;
            comArea.AutoCompleteSource = AutoCompleteSource.ListItems;

            dateTimePickerReceived.Value = DateTime.Now;
        }

        private void Shipping_Record_Load(object sender, EventArgs e)
        {
            try
            {
                //radClient.Checked = true;
                dbconnection.Open();
                string query = "select * from area";
                MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                DataTable dt = new DataTable();
                da.Fill(dt);
                comArea.DataSource = dt;
                comArea.DisplayMember = dt.Columns["Area_Name"].ToString();
                comArea.ValueMember = dt.Columns["Area_ID"].ToString();
                comArea.Text = "";

                query = "select * from delegate";
                da = new MySqlDataAdapter(query, dbconnection);
                dt = new DataTable();
                da.Fill(dt);
                comDelegate.DataSource = dt;
                comDelegate.DisplayMember = dt.Columns["Delegate_Name"].ToString();
                comDelegate.ValueMember = dt.Columns["Delegate_ID"].ToString();
                comDelegate.SelectedIndex = -1;
                
                search();

                query = "select * from bank inner join bank_main on bank.MainBank_ID=bank_main.MainBank_ID where Branch_ID=" + UserControl.EmpBranchID + " and MainBank_Type='خزينة' and MainBank_Name='خزينة شحن'";
                da = new MySqlDataAdapter(query, dbconnection);
                dt = new DataTable();
                da.Fill(dt);
                comBank.DataSource = dt;
                comBank.DisplayMember = dt.Columns["Bank_Name"].ToString();
                comBank.ValueMember = dt.Columns["Bank_ID"].ToString();
                if (UserControl.userType == 1)
                {
                    comBank.SelectedIndex = -1;
                }
                else
                {
                    comBank.Enabled = false;
                    comBank.DropDownStyle = ComboBoxStyle.DropDownList;

                    string q = "SELECT bank.Bank_Name,bank_employee.Bank_ID FROM bank_employee INNER JOIN bank ON bank.Bank_ID = bank_employee.Bank_ID where bank_employee.Employee_ID=" + UserControl.EmpID;
                    MySqlCommand com = new MySqlCommand(q, dbconnection);
                    MySqlDataReader dr = com.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            comBank.Text = dr["Bank_Name"].ToString();
                            comBank.SelectedValue = dr["Bank_ID"].ToString();
                        }
                        dr.Close();
                    }
                    else
                    {
                        comBank.SelectedIndex = -1;
                    }
                }

                loaded = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }

        private void comClient_SelectedValueChanged(object sender, EventArgs e)
        {
            if (loaded)
            {
                try
                {
                    dbconnection.Close();
                    dbconnection.Open();
                    string query = "select customer_phone.Phone,customer.Customer_Address from customer INNER JOIN customer_phone ON customer_phone.Customer_ID = customer.Customer_ID where customer.Customer_ID=" + comClient.SelectedValue.ToString() + " order by customer_phone.CustomerPhone_ID desc limit 1";
                    MySqlCommand com = new MySqlCommand(query, dbconnection);
                    MySqlDataReader dr = com.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            txtPhone.Text = dr["Phone"].ToString();
                            txtAddress.Text = dr["Customer_Address"].ToString();
                        }
                        dr.Close();
                    }
                    else
                    {
                        txtPhone.Text = "";
                        txtAddress.Text = "";
                    }
                    
                    query = "SELECT distinct Address FROM shipping where Customer_ID=" + comClient.SelectedValue.ToString();
                    com = new MySqlCommand(query, dbconnection);
                    dr = com.ExecuteReader();
                    if (dr.HasRows)
                    {
                        checkedListBoxControlAddress.Items.Clear();
                        while (dr.Read())
                        {
                            checkedListBoxControlAddress.Items.Add(dr["Address"].ToString());
                        }
                        dr.Close();
                    }
                    else
                    {
                        checkedListBoxControlAddress.Items.Clear();
                    }
                    comArea.Focus();
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
                if (id !=0 && txtReceivedClient.Text != "" && comClient.Text != "" && comDelegate.Text != "" && txtPhone.Text != "" && txtAddress.Text != "" && comArea.Text != "" && (txtCartons.Text != "" || txtQuantity.Text != "") && comBank.Text != "" && txtMoney.Text != "")
                {
                    double outParse;
                    if (double.TryParse(txtMoney.Text, out outParse))
                    {
                        dbconnection.Open();

                        string query = "insert into shipping (CustomerBill_ID,Customer_ID,Recipient_Name,Phone,Address,Area_ID,Date,Quantity,Cartons,Bank_ID,Money) values(@CustomerBill_ID,@Customer_ID,@Recipient_Name,@Phone,@Address,@Area_ID,@Date,@Quantity,@Cartons,@Bank_ID,@Money)";
                        MySqlCommand com = new MySqlCommand(query, dbconnection);
                        com.Parameters.Add("@CustomerBill_ID", MySqlDbType.Int16, 11);
                        com.Parameters["@CustomerBill_ID"].Value = id;
                        com.Parameters.Add("@Customer_ID", MySqlDbType.Int16, 11);
                        com.Parameters["@Customer_ID"].Value = comClient.SelectedValue;
                        com.Parameters.Add("@Recipient_Name", MySqlDbType.VarChar, 255);
                        com.Parameters["@Recipient_Name"].Value = txtReceivedClient.Text;
                        com.Parameters.Add("@Phone", MySqlDbType.VarChar, 255);
                        com.Parameters["@Phone"].Value = txtPhone.Text;
                        com.Parameters.Add("@Address", MySqlDbType.VarChar, 255);
                        com.Parameters["@Address"].Value = txtAddress.Text;
                        com.Parameters.Add("@Area_ID", MySqlDbType.Int16, 11);
                        com.Parameters["@Area_ID"].Value = comArea.SelectedValue.ToString();
                        com.Parameters.Add("@Date", MySqlDbType.DateTime);
                        com.Parameters["@Date"].Value = dateTimePickerReceived.Value;
                        if (txtQuantity.Text != "")
                        {
                            com.Parameters.Add("@Quantity", MySqlDbType.Decimal, 10);
                            com.Parameters["@Quantity"].Value = txtQuantity.Text;
                        }
                        else
                        {
                            com.Parameters.Add("@Quantity", MySqlDbType.Decimal, 10);
                            com.Parameters["@Quantity"].Value = null;
                        }
                        if (txtCartons.Text != "")
                        {
                            com.Parameters.Add("@Cartons", MySqlDbType.Decimal, 10);
                            com.Parameters["@Cartons"].Value = txtCartons.Text;
                        }
                        else
                        {
                            com.Parameters.Add("@Cartons", MySqlDbType.Decimal, 10);
                            com.Parameters["@Cartons"].Value = null;
                        }
                        com.Parameters.Add("@Bank_ID", MySqlDbType.Int16, 11);
                        com.Parameters["@Bank_ID"].Value = comBank.SelectedValue.ToString();
                        com.Parameters.Add("@Money", MySqlDbType.Decimal, 10);
                        com.Parameters["@Money"].Value = outParse;
                        com.ExecuteNonQuery();

                        query = "select Shipping_ID from shipping order by Shipping_ID desc limit 1";
                        com = new MySqlCommand(query, dbconnection);
                        ShippingID = Convert.ToInt16(com.ExecuteScalar().ToString());

                        query = "update customer_bill set Shipped=2 , RecivedType='شحن' where CustomerBill_ID=" + id;
                        com = new MySqlCommand(query, dbconnection);
                        com.ExecuteNonQuery();

                        MySqlCommand com2 = new MySqlCommand("select Bank_Stock from bank where Bank_ID=" + comBank.SelectedValue, dbconnection);
                        double amount2 = Convert.ToDouble(com2.ExecuteScalar().ToString());
                        amount2 += outParse;
                        MySqlCommand com3 = new MySqlCommand("update bank set Bank_Stock=" + amount2 + " where Bank_ID=" + comBank.SelectedValue, dbconnection);
                        com3.ExecuteNonQuery();

                        printBill();

                        search();
                        clear();
                        dbconnection.Close();
                        loaded = false;
                        radClient.Checked = false;
                        radEng.Checked = false;
                        radContractor.Checked = false;
                        radDealer.Checked = false;
                        comClient.SelectedIndex = -1;
                        checkedListBoxControlAddress.Items.Clear();
                        loaded = true;
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
                    MessageBox.Show("برجاء ادخال البيانات كاملة");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }

        //check type of customer if engineer,client or contract 
        private void radiotype_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                RadioButton radio = (RadioButton)sender;
                Customer_Type = radio.Text;
                dbconnection.Open();
                string query = "select * from customer where Customer_Type='" + Customer_Type + "'";
                MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                DataTable dt = new DataTable();
                da.Fill(dt);
                comClient.DataSource = dt;
                comClient.DisplayMember = dt.Columns["Customer_Name"].ToString();
                comClient.ValueMember = dt.Columns["Customer_ID"].ToString();
                comClient.Text = "";
                txtPhone.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }

        private void checkedListBoxControlAddress_ItemCheck(object sender, DevExpress.XtraEditors.Controls.ItemCheckEventArgs e)
        {
            try
            {
                if (e.State == CheckState.Checked)
                {
                    txtAddress.Text = checkedListBoxControlAddress.SelectedValue.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void gridView1_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            try
            {
                if (loaded)
                {
                    row1 = gridView1.GetDataRow(gridView1.GetRowHandle(e.RowHandle));

                    dbconnection.Open();
                    string query = "select CustomerBill_ID from customer_bill where Branch_BillNumber=" + row1["فاتورة رقم"].ToString() + " and Branch_ID=" + UserControl.EmpBranchID;
                    MySqlCommand com1 = new MySqlCommand(query, dbconnection);
                    id = Convert.ToInt32(com1.ExecuteScalar());

                    query = "select customer_bill.Bill_Date,product_bill.Delegate_ID from customer_bill inner join product_bill on product_bill.CustomerBill_ID=customer_bill.CustomerBill_ID where customer_bill.CustomerBill_ID=" + id + " order by product_bill.ProductBill_ID desc limit 1";
                    MySqlCommand com = new MySqlCommand(query, dbconnection);
                    MySqlDataReader dr = com.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            dateTimePickerBill.Text = dr["Bill_Date"].ToString();
                            comDelegate.SelectedValue = dr["Delegate_ID"].ToString();
                        }
                    }
                    else
                    {
                        dateTimePickerBill.Value = DateTime.Now.Date;
                        comDelegate.SelectedIndex = -1;
                    }
                    dr.Close();

                    query = "select sum(product_bill.Quantity) as 'Quantity' from customer_bill inner join product_bill on product_bill.CustomerBill_ID=customer_bill.CustomerBill_ID inner join data on data.Data_ID=product_bill.Data_ID where customer_bill.CustomerBill_ID=" + id + " and data.Carton=0 group by product_bill.Data_ID";
                    com = new MySqlCommand(query, dbconnection);
                    dr = com.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            txtQuantity.Text = dr["Quantity"].ToString();
                        }
                    }
                    else
                    {
                        txtQuantity.Text = "";
                    }
                    dr.Close();

                    query = "select sum(product_bill.Cartons) as 'Cartons' from customer_bill inner join product_bill on product_bill.CustomerBill_ID=customer_bill.CustomerBill_ID inner join data on data.Data_ID=product_bill.Data_ID where customer_bill.CustomerBill_ID=" + id + " and data.Carton<>0 group by product_bill.Data_ID";
                    com = new MySqlCommand(query, dbconnection);
                    dr = com.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            txtCartons.Text = dr["Cartons"].ToString();
                        }
                    }
                    else
                    {
                        txtCartons.Text = "";
                    }
                    dr.Close();


                    if (row1["Client_ID"].ToString() != "")
                    {
                        dbconnection.Close();
                        loaded = false;
                        radClient.Checked = true;
                        loaded = true;
                        comClient.SelectedValue = row1["Client_ID"].ToString();
                    }
                    else if(row1["Customer_ID"].ToString() != "")
                    {
                        //dbconnection.Open();
                        query = "select Customer_Type from customer where Customer_ID=" + row1["Customer_ID"].ToString();
                        MySqlCommand comm = new MySqlCommand(query, dbconnection);
                        string customerType = comm.ExecuteScalar().ToString();
                        dbconnection.Close();

                        loaded = false;
                        if (customerType == "مهندس")
                        {
                            radEng.Checked = true;
                        }
                        else if (customerType == "مقاول")
                        {
                            radContractor.Checked = true;
                        }
                        else if (customerType == "تاجر")
                        {
                            radDealer.Checked = true;
                        }
                        loaded = true;
                        comClient.SelectedValue = row1["Customer_ID"].ToString();
                    }
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
                if (loaded)
                {
                    xtraTabPage = getTabPage("تسجيل شحنة عميل");
                    if (!IsClear())
                    {
                        xtraTabPage.ImageOptions.Image = MainSystem.Properties.Resources.unsave;
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

        public void search()
        {
            //customer_bill.CustomerBill_ID NOT IN (SELECT shipping.CustomerBill_ID FROM shipping)
            string query = "SELECT customer_bill.CustomerBill_ID,customer_bill.Branch_BillNumber as 'فاتورة رقم',customer_bill.Branch_Name as 'الفرع',customer_bill.Client_Name as 'العميل',customer_bill.Customer_Name as 'المهندس/المقاول/التاجر',customer_bill.Customer_ID,customer_bill.Client_ID FROM customer_bill where customer_bill.Shipped=0 and customer_bill.RecivedFlag='Draft' and ((customer_bill.Paid_Status='1' and customer_bill.Type_Buy='كاش') or customer_bill.Type_Buy='آجل') and customer_bill.Branch_ID=" + UserControl.EmpBranchID;
            MySqlDataAdapter adabter = new MySqlDataAdapter(query, dbconnection);
            DataTable dTable = new DataTable();
            adabter.Fill(dTable);
            gridControl1.DataSource = dTable;

            gridView1.Columns["Customer_ID"].Visible = false;
            gridView1.Columns["Client_ID"].Visible = false;
            gridView1.Columns["CustomerBill_ID"].Visible = false;
        }

        void printBill()
        {
            Print_ShipWarehous_Report f = new Print_ShipWarehous_Report();
            f.PrintInvoice(ShippingID, Convert.ToInt16(row1["فاتورة رقم"].ToString()), UserControl.EmpBranchName, row1["المهندس/المقاول/التاجر"].ToString(), row1["العميل"].ToString(), txtReceivedClient.Text, comArea.Text, txtAddress.Text, txtPhone.Text, comDelegate.Text, dateTimePickerBill.Value.Date, dateTimePickerReceived.Value, txtQuantity.Text, txtCartons.Text, comBank.Text, Convert.ToDouble(txtMoney.Text));
            f.ShowDialog();
        }

        //clear function
        public void clear()
        {
            foreach (Control co in this.layoutControl1.Controls)
            {
                if (co is System.Windows.Forms.ComboBox)
                {
                    co.Text = "";
                    comDelegate.SelectedIndex = -1;
                }
                else if (co is TextBox)
                {
                    co.Text = "";
                }
                else if (co is DateTimePicker)
                {
                    dateTimePickerBill.Value = DateTime.Now;
                    dateTimePickerReceived.Value = DateTime.Now;
                }
            }
        }

        public XtraTabPage getTabPage(string text)
        {
            for (int i = 0; i < MainForm.tabControlShipping.TabPages.Count; i++)
                if (MainForm.tabControlShipping.TabPages[i].Text == text)
                {
                    return MainForm.tabControlShipping.TabPages[i];
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
                    if (item is System.Windows.Forms.ComboBox)
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

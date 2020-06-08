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

        public Shipping_Record()
        {
            InitializeComponent();
            dbconnection = new MySqlConnection(connection.connectionString);
            
            comArea.AutoCompleteMode = AutoCompleteMode.Suggest;
            comArea.AutoCompleteSource = AutoCompleteSource.ListItems;

            dateTimePicker1.Value = DateTime.Now;
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
                    
                    query = "SELECT Address FROM shipping where Customer_ID=" + comClient.SelectedValue.ToString();
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
                if (comClient.Text != "" && txtPhone.Text != "" && txtAddress.Text != "" && comArea.Text != "")
                {
                    int billNum = 0;
                    
                    dbconnection.Open();
                    
                    string query = "insert into shipping (CustomerBill_ID,Customer_ID,Customer_Name,Phone,Bill_Number,Branch_ID,Branch_Name,Address,Area_ID,Area_Name,Date,Description) values(@CustomerBill_ID,@Customer_ID,@Customer_Name,@Phone,@Bill_Number,@Branch_ID,@Branch_Name,@Address,@Area_ID,@Area_Name,@Date,@Description)";
                    MySqlCommand com = new MySqlCommand(query, dbconnection);
                    com.Parameters.Add("@CustomerBill_ID", MySqlDbType.Int16, 11);
                    com.Parameters["@CustomerBill_ID"].Value = id;
                    com.Parameters.Add("@Customer_ID", MySqlDbType.Int16, 11);
                    com.Parameters["@Customer_ID"].Value = comClient.SelectedValue.ToString();
                    com.Parameters.Add("@Customer_Name", MySqlDbType.VarChar, 255);
                    com.Parameters["@Customer_Name"].Value = comClient.Text;
                    com.Parameters.Add("@Phone", MySqlDbType.VarChar, 255);
                    com.Parameters["@Phone"].Value = txtPhone.Text;
                    com.Parameters.Add("@Bill_Number", MySqlDbType.Int16, 11);
                    com.Parameters["@Bill_Number"].Value = billNum;
                    com.Parameters.Add("@Branch_ID", MySqlDbType.Int16, 11);
                    com.Parameters["@Branch_ID"].Value = UserControl.EmpBranchID;
                    //com.Parameters.Add("@Branch_Name", MySqlDbType.VarChar, 255);
                    //com.Parameters["@Branch_Name"].Value = comBranch.Text;
                    com.Parameters.Add("@Address", MySqlDbType.VarChar, 255);
                    com.Parameters["@Address"].Value = txtAddress.Text;
                    com.Parameters.Add("@Area_ID", MySqlDbType.Int16, 11);
                    com.Parameters["@Area_ID"].Value = comArea.SelectedValue.ToString();
                    com.Parameters.Add("@Area_Name", MySqlDbType.VarChar, 255);
                    com.Parameters["@Area_Name"].Value = comArea.Text;
                    com.Parameters.Add("@Description", MySqlDbType.VarChar, 255);
                    com.Parameters["@Description"].Value = "";
                    com.Parameters.Add("@Date", MySqlDbType.DateTime);
                    com.Parameters["@Date"].Value = dateTimePicker1.Value;
                    com.ExecuteNonQuery();

                    query = "update customer_bill set Shipped=1 and RecivedType='شحن' where CustomerBill_ID=" + id;
                    com = new MySqlCommand(query, dbconnection);
                    com.ExecuteNonQuery();

                    //MessageBox.Show("تم");
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
                            dateTimePicker2.Text = dr["Bill_Date"].ToString();
                            comDelegate.SelectedValue = dr["Delegate_ID"].ToString();
                        }
                        dr.Close();
                    }
                    else
                    {
                        dateTimePicker2.Value = DateTime.Now.Date;
                        comDelegate.SelectedIndex = -1;
                    }

                    if (row1["Client_ID"].ToString() != "")
                    {
                        loaded = false;
                        radClient.Checked = true;
                        loaded = true;
                        dbconnection.Close();
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

        //clear function
        public void clear()
        {
            foreach (Control co in this.layoutControl1.Controls)
            {
                if (co is System.Windows.Forms.ComboBox)
                {
                    co.Text = "";
                }
                else if (co is TextBox)
                {
                    co.Text = "";
                }
                else if (co is DateTimePicker)
                {
                    dateTimePicker1.Value = DateTime.Now;
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

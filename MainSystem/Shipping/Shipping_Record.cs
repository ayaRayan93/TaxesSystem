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

        public Shipping_Record()
        {
            InitializeComponent();
            dbconnection = new MySqlConnection(connection.connectionString);
            
            comClient.AutoCompleteMode = AutoCompleteMode.Suggest;
            comClient.AutoCompleteSource = AutoCompleteSource.ListItems;
            comBranch.AutoCompleteMode = AutoCompleteMode.Suggest;
            comBranch.AutoCompleteSource = AutoCompleteSource.ListItems;
            comZone.AutoCompleteMode = AutoCompleteMode.Suggest;
            comZone.AutoCompleteSource = AutoCompleteSource.ListItems;
        }

        private void Shipping_Record_Load(object sender, EventArgs e)
        {
            try
            {
                radClient.Checked = true;
                dbconnection.Open();
                string query = "select * from zone";
                MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                DataTable dt = new DataTable();
                da.Fill(dt);
                comZone.DataSource = dt;
                comZone.DisplayMember = dt.Columns["Zone_Name"].ToString();
                comZone.ValueMember = dt.Columns["Zone_ID"].ToString();
                comZone.Text = "";

                query = "select * from branch";
                da = new MySqlDataAdapter(query, dbconnection);
                dt = new DataTable();
                da.Fill(dt);
                comBranch.DataSource = dt;
                comBranch.DisplayMember = dt.Columns["Branch_Name"].ToString();
                comBranch.ValueMember = dt.Columns["Branch_ID"].ToString();
                comBranch.Text = "";

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
                    string query = "select customer_phone.Phone from customer INNER JOIN customer_phone ON customer_phone.Customer_ID = customer.Customer_ID where customer.Customer_ID=" + comClient.SelectedValue.ToString() + " order by customer_phone.CustomerPhone_ID desc limit 1";
                    MySqlCommand com = new MySqlCommand(query, dbconnection);
                    if (com.ExecuteScalar() != null)
                    {
                        txtPhone.Text = com.ExecuteScalar().ToString();
                    }

                    query = "SELECT Address FROM shipping where Customer_ID=" + comClient.SelectedValue.ToString();
                    com = new MySqlCommand(query, dbconnection);
                    MySqlDataReader dr = com.ExecuteReader();
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
                    txtPhone.Focus();
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
                if (comClient.Text != "" && txtPhone.Text != "" && comBranch.Text != "" && txtBillNumber.Text != "" && txtAddress.Text != "" && comZone.Text != "")
                {
                    int billNum = 0;
                    if(int.TryParse(txtBillNumber.Text, out billNum))
                    {}
                    else
                    {
                        MessageBox.Show("رقم الفاتورة يجب ان يكون عدد");
                        dbconnection.Close();
                        return;
                    }
                    dbconnection.Open();
                    string query = "insert into shipping (Customer_ID,Customer_Name,Phone,Bill_Number,Branch_ID,Branch_Name,Address,Zone_ID,Zone_Name,Description) values(@Customer_ID,@Customer_Name,@Phone,@Bill_Number,@Branch_ID,@Branch_Name,@Address,@Zone_ID,@Zone_Name,@Description)";
                    MySqlCommand com = new MySqlCommand(query, dbconnection);
                    com.Parameters.Add("@Customer_ID", MySqlDbType.Int16, 11);
                    com.Parameters["@Customer_ID"].Value = comClient.SelectedValue.ToString();
                    com.Parameters.Add("@Customer_Name", MySqlDbType.VarChar, 255);
                    com.Parameters["@Customer_Name"].Value = comClient.Text;
                    com.Parameters.Add("@Phone", MySqlDbType.VarChar, 255);
                    com.Parameters["@Phone"].Value = txtPhone.Text;
                    com.Parameters.Add("@Bill_Number", MySqlDbType.Int16, 11);
                    com.Parameters["@Bill_Number"].Value = billNum;
                    com.Parameters.Add("@Branch_ID", MySqlDbType.Int16, 11);
                    com.Parameters["@Branch_ID"].Value = comBranch.SelectedValue.ToString();
                    com.Parameters.Add("@Branch_Name", MySqlDbType.VarChar, 255);
                    com.Parameters["@Branch_Name"].Value = comBranch.Text;
                    com.Parameters.Add("@Address", MySqlDbType.VarChar, 255);
                    com.Parameters["@Address"].Value = txtAddress.Text;
                    com.Parameters.Add("@Zone_ID", MySqlDbType.Int16, 11);
                    com.Parameters["@Zone_ID"].Value = comZone.SelectedValue.ToString();
                    com.Parameters.Add("@Zone_Name", MySqlDbType.VarChar, 255);
                    com.Parameters["@Zone_Name"].Value = comZone.Text;
                    com.Parameters.Add("@Description", MySqlDbType.VarChar, 255);
                    com.Parameters["@Description"].Value = txtDescription.Text;

                    com.ExecuteNonQuery();
                    
                    MessageBox.Show("تم");
                    clear();
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

        private void txtPhone_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    dbconnection.Open();
                    string query = "select Customer_Name from customer inner join customer_phone on customer_phone.Customer_ID=customer.Customer_ID where customer_phone.Phone='" + txtPhone.Text + "' and customer.Customer_Type='" + Customer_Type + "'";
                    MySqlCommand com = new MySqlCommand(query, dbconnection);
                    if (com.ExecuteScalar() != null)
                    {
                        comClient.Text = com.ExecuteScalar().ToString();
                    }
                    else
                    {
                        comClient.Text = "";
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                dbconnection.Close();
            }
        }

        private void txtBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (loaded)
                {
                    xtraTabPage = getTabPage("tabPageShippingRecord");
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

        //clear function
        public void clear()
        {
            foreach (Control co in this.tableLayoutPanel1.Controls)
            {
                foreach (Control item in co.Controls)
                {
                    if (item is System.Windows.Forms.ComboBox)
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
            for (int i = 0; i < MainForm.tabControlShipping.TabPages.Count; i++)
                if (MainForm.tabControlShipping.TabPages[i].Name == text)
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

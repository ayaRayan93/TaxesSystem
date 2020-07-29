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

namespace TaxesSystem
{
    public partial class checkPaidBillsForm : Form
    {
        private MySqlConnection dbconnection, dbconnectionR;
        private double recivedMoney = 0;
        int clientID = -1;
        private bool loaded=false;
        private bool load=false;
        private string Customer_Type;
        //double safay = -1;
        //string ClientName = "";
        public checkPaidBillsForm()//, StoreForm2CrystalReport crystalReport)
        {
            try
            {
                InitializeComponent();
                dbconnection = new MySqlConnection(connection.connectionString);
                dbconnectionR = new MySqlConnection(connection.connectionString);           
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void checkPaidBillsForm_Load(object sender, EventArgs e)
        {
            try
            {
                string query = "select customer.Customer_ID,customer.Customer_Name from customer where Type='آجل'";
                MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                DataTable dt = new DataTable();
                da.Fill(dt);
                comClient.DataSource = dt;
                comClient.DisplayMember = dt.Columns["Customer_Name"].ToString();
                comClient.ValueMember = dt.Columns["Customer_ID"].ToString();
                comClient.Text = "";
                txtClientID.Text = "";

                query = "select * from branch ";

                da = new MySqlDataAdapter(query, dbconnection);
                dt = new DataTable();

                da.Fill(dt);
                comBranch.DataSource = dt;
                comBranch.DisplayMember = dt.Columns["Branch_Name"].ToString();
                comBranch.ValueMember = dt.Columns["Branch_ID"].ToString();
                comBranch.Text = "";
                txtBranchID.Text = "";

                loaded = true;
                load = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //check type of customer if engineer,client or contract 
        private void radiotype_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton radio = (RadioButton)sender;
            Customer_Type = radio.Text;
            comClient.Text = "";
            txtClientID.Text = "";
            comEngCon.Text = "";
            txtCustomerID.Text = "";
            if (txtBranchID.Text != "")
            {
                loaded = false; //this is flag to prevent action of SelectedValueChanged event until datasource fill combobox
                try
                {
                    if (Customer_Type == "عميل")
                    {
                        labelEng.Visible = false;
                        comEngCon.Visible = false;
                        txtCustomerID.Visible = false;
                        labelClient.Visible = true;
                        comClient.Visible = true;
                        txtClientID.Visible = true;

                        string query = "select customer.Customer_ID,customer.Customer_Name from customer inner join customer_bill on customer_bill.Client_ID=customer.Customer_ID where Type_Buy='آجل' and Customer_Type='" + Customer_Type + "'";// and Branch_ID=" + txtBranchID.Text;

                        MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        comClient.DataSource = dt;
                        comClient.DisplayMember = dt.Columns["Customer_Name"].ToString();
                        comClient.ValueMember = dt.Columns["Customer_ID"].ToString();
                        comClient.Text = "";
                        txtClientID.Text = "";
                    }
                    else
                    {
                        labelEng.Visible = true;
                        comEngCon.Visible = true;
                        txtCustomerID.Visible = true;
                        labelClient.Visible = false;
                        comClient.Visible = false;
                        txtClientID.Visible = false;

                        string query = "select customer.Customer_ID,customer.Customer_Name from customer inner join customer_bill on customer_bill.Client_ID=customer.Customer_ID where Type_Buy='آجل' and Customer_Type='" + Customer_Type + "'";// and Branch_ID=" + txtBranchID.Text;

                        MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        comEngCon.DataSource = dt;
                        comEngCon.DisplayMember = dt.Columns["Customer_Name"].ToString();
                        comEngCon.ValueMember = dt.Columns["Customer_ID"].ToString();
                        comEngCon.Text = "";
                        txtCustomerID.Text = "";
                    }

                    loaded = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            dbconnection.Close();
        }
        //when select customer(مهندس,مقاول)display in comCustomer the all clients of th customer 
        private void comEngCon_SelectedValueChanged(object sender, EventArgs e)
        {
            if (loaded)
            {
                try
                {
                    loaded = false;
                    txtCustomerID.Text = comEngCon.SelectedValue.ToString();
                    labelClient.Visible = true;
                    comClient.Visible = true;
                    txtClientID.Visible = true;

                    string query = "select * from customer where Customer_ID in(select Client_ID from custmer_client where Customer_ID=" + comEngCon.SelectedValue + ")";
                    MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    comClient.DataSource = dt;
                    comClient.DisplayMember = dt.Columns["Customer_Name"].ToString();
                    comClient.ValueMember = dt.Columns["Customer_ID"].ToString();
                    comClient.Text = "";
                    txtClientID.Text = "";
                    loaded = true;
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
                            case "txtCustomerID":
                                query = "select Customer_Name from customer where Customer_ID=" + txtCustomerID.Text + "";
                                com = new MySqlCommand(query, dbconnection);
                                if (com.ExecuteScalar() != null)
                                {
                                    Name = (string)com.ExecuteScalar();
                                    comEngCon.Text = Name;
                                    comEngCon.SelectedValue = txtCustomerID.Text;
                                    comClient.Text = com.ExecuteScalar().ToString();
                                    Display();
                                }
                                else
                                {
                                    MessageBox.Show("there is no item with this id");
                                    dbconnection.Close();
                                    return;
                                }
                                break;
                            case "txtClientID":
                                query = "select Customer_Name from customer where Customer_ID=" + txtClientID.Text + "";
                                com = new MySqlCommand(query, dbconnection);
                                if (com.ExecuteScalar() != null)
                                {
                                    Name = (string)com.ExecuteScalar();
                                    comClient.Text = Name;
                                    comClient.SelectedValue = txtClientID.Text;
                                    comClient.Text = com.ExecuteScalar().ToString();
                                    Display();
                                }
                                else
                                {
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
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                dbconnection.Open();
                if (e.ColumnIndex == dataGridView1.Columns["PaidOrNot"].Index)
                {
                    DataGridViewRow row = dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex];
                    DataGridViewCheckBoxCell x =row.Cells["PaidOrNot"] as DataGridViewCheckBoxCell;

                    if (Convert.ToBoolean(row.Cells["PaidOrNot"].EditedFormattedValue) == false)
                    {
                        if (recivedMoney >= Convert.ToDouble(row.Cells["اجمالي الفاتورة"].Value))
                        {
                            recivedMoney -= Convert.ToDouble(row.Cells["اجمالي الفاتورة"].Value);
                            labRecivedMoney.Text = recivedMoney.ToString();
                            string query = "update customer_bill set Paid_Status=1 ,AgelBill_PaidDate=@AgelBill_PaidDate where Branch_BillNumber=" + row.Cells["رقم الفاتورة"].Value.ToString();
                            MySqlCommand com = new MySqlCommand(query, dbconnection);
                            com.Parameters.Add("@AgelBill_PaidDate", MySqlDbType.Date, 0);
                            com.Parameters["@AgelBill_PaidDate"].Value = DateTime.Now.Date;
                            com.ExecuteNonQuery();
                        }
                        else
                        {
                            MessageBox.Show("قيمة الفاتورة اكبر من المبلغ المدفوع");
                            x.Value = x.FalseValue;
                            dataGridView1.EndEdit();
                        }
                    }
                    else
                    {
                        recivedMoney += Convert.ToDouble(row.Cells["اجمالي الفاتورة"].Value);
                        labRecivedMoney.Text = recivedMoney.ToString();
                        string query = "update customer_bill set Paid_Status=0 ,AgelBill_PaidDate=Null where Branch_BillNumber=" + row.Cells["رقم الفاتورة"].Value.ToString();
                        MySqlCommand com = new MySqlCommand(query, dbconnection);
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
        private void comClientName_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (loaded)
                {
                    dbconnection.Open();
                    txtClientID.Text = comClient.SelectedValue.ToString();
                    clientID = Convert.ToInt32(txtClientID.Text);
                    Display();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                dbconnection.Open();
                Display();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }
        private void btnDone_Click(object sender, EventArgs e)
        {
            try
            {
                dbconnection.Open();
                if (clientID != -1)
                {
                    string query = "select Money from Client_Rest_Money where Client_ID=" + clientID;
                    MySqlCommand com = new MySqlCommand(query, dbconnection);
                    if (com.ExecuteScalar() != null)
                    {
                        double paidMoney = Convert.ToDouble(labRecivedMoney.Text);
                        query = "update Client_Rest_Money set Money=" + paidMoney + " where Client_ID=" + txtClientID.Text;
                    }
                    else
                    {
                        query = "insert into Client_Rest_Money (Client_ID,Client_Name,Money) values (" + clientID + ",'" + comClient.Text + "'," + labRecivedMoney.Text + ")";
                    }
                    com = new MySqlCommand(query, dbconnection);
                    com.ExecuteNonQuery();
                    MessageBox.Show("Done");

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }
        private void comBranch_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (load)
                {
                    txtBranchID.Text = comBranch.SelectedValue.ToString();
                    loaded = false;
                    if (Customer_Type == "عميل")
                    {
                        //labelEng.Visible = false;
                        //comEngCon.Visible = false;
                        //txtCustomerID.Visible = false;
                        //labelClient.Visible = true;
                        //comClient.Visible = true;
                        //txtClientID.Visible = true;

                        string query = "select customer.Customer_ID,customer.Customer_Name from customer  where Type='آجل' and Customer_Type='" + Customer_Type + "'";// and Branch_ID=" + txtBranchID.Text;

                        MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        comClient.DataSource = dt;
                        comClient.DisplayMember = dt.Columns["Customer_Name"].ToString();
                        comClient.ValueMember = dt.Columns["Customer_ID"].ToString();
                        comClient.Text = "";
                        txtClientID.Text = "";
                    }
                    else
                    {
                        //labelEng.Visible = true;
                        //comEngCon.Visible = true;
                        //txtCustomerID.Visible = true;
                        //labelClient.Visible = false;
                        //comClient.Visible = false;
                        //txtClientID.Visible = false;

                        string query = "select customer.Customer_ID,customer.Customer_Name from customer  where Type='آجل' and Customer_Type='" + Customer_Type + "'";// and Branch_ID=" + txtBranchID.Text;

                        MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        comEngCon.DataSource = dt;
                        comEngCon.DisplayMember = dt.Columns["Customer_Name"].ToString();
                        comEngCon.ValueMember = dt.Columns["Customer_ID"].ToString();
                        comEngCon.Text = "";
                        txtCustomerID.Text = "";
                    }

                    loaded = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }
        private void txtBranchID_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    dbconnection.Close();
                    string query = "select Branch_Name from branch where Branch_ID=" + txtBranchID.Text;
                    MySqlCommand comand = new MySqlCommand(query, dbconnection);
                    dbconnection.Open();
                    string Branch_Name = comand.ExecuteScalar().ToString();
                    dbconnection.Close();
                    comBranch.Text = Branch_Name;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        public void Display()
        {
            string query = "";
            //double m=0;
            if (txtClientID.Text != "" && txtCustomerID.Text != "")
            {
                query = "select distinct Branch_BillNumber as 'رقم الفاتورة' , Total_CostAD as 'اجمالي الفاتورة',branch.Branch_Name as 'الفرع',Bill_Date as'التاريخ'  from customer_bill inner join branch on branch.Branch_ID=customer_bill.Branch_ID " +/*inner join delegate on delegate.Delegate_ID=customer_bill.Delegate_ID */"" + "where Client_ID=" + txtClientID.Text + " and Customer_ID=" + txtCustomerID.Text + " and Paid_Status=0 and Type_Buy='آجل'";
             
            }
            else if (txtClientID.Text == "" && txtCustomerID.Text != "")
            {
                query = "select distinct Branch_BillNumber as 'رقم الفاتورة' , Total_CostAD as 'اجمالي الفاتورة',branch.Branch_Name as 'الفرع',Bill_Date as'التاريخ'  from customer_bill inner join branch on branch.Branch_ID=customer_bill.Branch_ID " +/*inner join delegate on delegate.Delegate_ID=customer_bill.Delegate_ID */"" + "where  Customer_ID=" + txtCustomerID.Text + " and Paid_Status=0 and Type_Buy='آجل'";
             
            }
            else if (txtClientID.Text != "" && txtCustomerID.Text == "")
            {
                query = "select distinct Branch_BillNumber as 'رقم الفاتورة', Total_CostAD as 'اجمالي الفاتورة',branch.Branch_Name as 'الفرع',Bill_Date as'التاريخ'  from customer_bill inner join branch on branch.Branch_ID=customer_bill.Branch_ID " +/*inner join delegate on delegate.Delegate_ID=customer_bill.Delegate_ID */"" + "where Client_ID=" + txtClientID.Text + "  and Paid_Status=0 and Type_Buy='آجل'";       
            }
            else
            {
                return;
            }

            MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dataGridView1.ColumnCount > 0)
                dataGridView1.Columns.Remove(dataGridView1.Columns[4]);
            dataGridView1.DataSource = dt;
            DataGridViewCheckBoxColumn checkColumn = new DataGridViewCheckBoxColumn();
            checkColumn.Name = "PaidOrNot";
            checkColumn.HeaderText = "تم الدفع";     
            checkColumn.ReadOnly = false;
            checkColumn.FalseValue = false;
            checkColumn.TrueValue = true;
            
            dataGridView1.Columns.Add(checkColumn);
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            checkColumn.Width = 100;

            CheckAddingOpenAccount();
        }

        private void btnNewChooes_Click(object sender, EventArgs e)
        {
            try
            {
                radClient.Checked = false;
                radCon.Checked = false;
                radDealer.Checked = false;
                radEng.Checked = false;

                labelEng.Visible = false;
                comEngCon.Visible = false;
                txtCustomerID.Visible = false;
                labelClient.Visible = false;
                comClient.Visible = false;
                txtClientID.Visible = false;
                dataGridView1.DataSource = null;
                if (dataGridView1.ColumnCount > 0)
                    dataGridView1.Columns.Remove(dataGridView1.Columns[0]);
                labRecivedMoney.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void CheckAddingOpenAccount()
        {
            string query1 = "", q = "", q1 = "",q2="",q3="",q4="";
            if (txtClientID.Text != "" && txtCustomerID.Text != "")
            {
                query1 = "select Money from Client_Rest_Money where Client_ID=" + txtClientID.Text + " and Customer_ID=" + txtCustomerID.Text;
                q = "select Customer_OpenAccount,Flag from custmer_client where  Client_ID=" + txtClientID.Text + " and Customer_ID=" + txtCustomerID.Text;
                q1 = "update  custmer_client  set flag='Yes'   where Client_ID=" + txtClientID.Text + " and Customer_ID=" + txtCustomerID.Text;
                q2 = "select ID from client_rest_money where Client_ID=" + txtClientID.Text + " and Customer_ID=" + txtCustomerID.Text;
                q3 = "update Client_Rest_Money set Money=@Money where Client_ID=" + txtClientID.Text + " and Customer_ID=" + txtCustomerID.Text;
                q4 = "insert into Client_Rest_Money (Customer_ID,Client_ID,Money) values (" + txtCustomerID.Text + ","+txtClientID.Text+",@Money)";
            }
            else if (txtClientID.Text == "" && txtCustomerID.Text != "")
            {
                query1 = "select Money from Client_Rest_Money where Customer_ID=" + txtCustomerID.Text+" and Client_ID is null";
                q = "select Customer_OpenAccount,Flag from customer where Customer_ID=" + txtCustomerID.Text;
                q1 = "update customer set Flag='Yes' where Customer_ID=" + txtCustomerID.Text;
                q2 = "select ID from Client_Rest_Money where Customer_ID=" + txtCustomerID.Text + " and Client_ID is null";
                q3 = "update Client_Rest_Money set Money=@Money where Customer_ID=" + txtCustomerID.Text + " and Client_ID is null";
                q4 = "insert into Client_Rest_Money (Customer_ID,Money) values (" + txtCustomerID.Text + ",@Money)";
            }
            else if (txtClientID.Text != "" && txtCustomerID.Text == "")
            {
                query1 = "select Money from Client_Rest_Money where Client_ID=" + txtClientID.Text+" and Customer_ID is null";
                q = "select Customer_OpenAccount,flag from customer  where Customer_ID=" + txtClientID.Text;
                q1 = "update  customer  set Flag='Yes'  where Customer_ID=" + txtClientID.Text;
                q2 = "select ID from Client_Rest_Money where Client_ID=" + txtClientID.Text + " and Customer_ID is null";
                q3 = "update Client_Rest_Money set Money=@Money where Client_ID=" + txtClientID.Text + " and Customer_ID is null";
                q4 = "insert into Client_Rest_Money (Client_ID,Money) values ("+ txtClientID.Text + ",@Money)";
            }
            else
            {
                return;
            }
            MySqlCommand com = new MySqlCommand(query1, dbconnection);

            double money = Convert.ToDouble(com.ExecuteScalar());

            double money1 = 0.0;
            MySqlCommand com1 = new MySqlCommand(q, dbconnection);
            MySqlDataReader dr = com1.ExecuteReader();
            while (dr.Read())
            {
                if (dr[1].ToString() == "No")
                {
                    money1 = Convert.ToDouble(dr[0].ToString());
                    money += money1;
                    dbconnectionR.Open();
                    MySqlCommand com2 = new MySqlCommand(q1, dbconnectionR);
                    com2.ExecuteNonQuery();
                    dbconnectionR.Close();
                }
                else
                {
                    
                }
            }
            dr.Close();
            com = new MySqlCommand(q2, dbconnection);
            if (com.ExecuteScalar()!= null)
            {
                com1 = new MySqlCommand(q3,dbconnection);
                com1.Parameters.Add("@Money", MySqlDbType.Decimal);
                com1.Parameters["@Money"].Value = money;
                com1.ExecuteNonQuery();
            }
            else
            {
                com1 = new MySqlCommand(q4, dbconnection);
                com1.Parameters.Add("@Money", MySqlDbType.Decimal);
                com1.Parameters["@Money"].Value = money;
                com1.ExecuteNonQuery();
            }
            recivedMoney = money;
            labRecivedMoney.Text = money.ToString();
        }
    }
}

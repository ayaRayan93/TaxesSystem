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
    public partial class checkPaidBillsForm : Form
    {
        private MySqlConnection dbconnection;
        private double recivedMoney = 0;
        int clientID = -1;
        private bool loaded=false;
        private string Customer_Type;
        double safay = -1;
        string ClientName = "";
        public checkPaidBillsForm()//, StoreForm2CrystalReport crystalReport)
        {
            try
            {
                InitializeComponent();
                dbconnection = new MySqlConnection(connection.connectionString);
                dbconnection.Open();
                /* and Bill_Date between '"+DateFrom+"' and '"+DateTo+ "'*/
              
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }

        private void checkPaidBillsForm_Load(object sender, EventArgs e)
        {
            try
            {
                string query = "select Customer_ID,Customer_Name from customer";
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
        //check type of customer if engineer,client or contract 
        private void radiotype_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton radio = (RadioButton)sender;
            Customer_Type = radio.Text;
            comClient.Text = "";
            txtClientID.Text = "";
            comEngCon.Text = "";
            txtCustomerID.Text = "";

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

                    string query = "select * from customer where Customer_Type='" + Customer_Type + "'";
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

                    string query = "select * from customer where Customer_Type='" + Customer_Type + "'";
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
                  
                    
                    if (recivedMoney >= Convert.ToDouble(row.Cells["اجمالي الفاتورة"].Value))
                    {
                        if (Convert.ToBoolean(row.Cells["PaidOrNot"].EditedFormattedValue) == false)
                        {
                            recivedMoney -= Convert.ToDouble(row.Cells["اجمالي الفاتورة"].Value);
                            labRecivedMoney.Text = recivedMoney.ToString();
                            string query = "update customer_bill set Paid_Status=1 where CustomerBill_ID=" + row.Cells["رقم الفاتورة"].Value.ToString();
                            MySqlCommand com = new MySqlCommand(query, dbconnection);
                            com.ExecuteNonQuery();
                        }
                        else
                        {
                            recivedMoney += Convert.ToDouble(row.Cells["اجمالي الفاتورة"].Value);
                            labRecivedMoney.Text = recivedMoney.ToString();
                            string query = "update customer_bill set Paid_Status=0 where CustomerBill_ID=" + row.Cells["رقم الفاتورة"].Value.ToString();
                            MySqlCommand com = new MySqlCommand(query, dbconnection);
                            com.ExecuteNonQuery();
                        }
                    }
                    else
                    {
                        MessageBox.Show("قيمة الفاتورة اكبر من المبلغ المدفوع");
                        x.Value = x.FalseValue;
                        dataGridView1.EndEdit();
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
                    clientID = Convert.ToInt16(txtClientID.Text);
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

        public void Display()
        {
            string query = "", query1 = ""; ;
            if (txtClientID.Text != "" && txtCustomerID.Text != "")
            {
                query = "select distinct CustomerBill_ID as 'رقم الفاتورة' " +/* ,delegate.Delegate_Name as 'المندوب' */"" + ", Total_CostAD as 'اجمالي الفاتورة',customer_bill.Branch_Name as 'الفرع',Bill_Date as'التاريخ'  from customer_bill " +/*inner join delegate on delegate.Delegate_ID=customer_bill.Delegate_ID */"" + "where Client_ID=" + txtClientID.Text + " and Customer_ID=" + txtCustomerID.Text + " and Paid_Status=0 and Type_Buy='آجل'";
                query1 = "select Money from Client_Rest_Money where Client_ID=" + txtClientID.Text + " and Customer_ID=" + txtCustomerID.Text;
            }
            else if (txtClientID.Text == "" && txtCustomerID.Text != "")
            {
                query = "select distinct CustomerBill_ID as 'رقم الفاتورة' " +/* ,delegate.Delegate_Name as 'المندوب' */"" + ", Total_CostAD as 'اجمالي الفاتورة',customer_bill.Branch_Name as 'الفرع',Bill_Date as'التاريخ'  from customer_bill " +/*inner join delegate on delegate.Delegate_ID=customer_bill.Delegate_ID */"" + "where  Customer_ID=" + txtCustomerID.Text + " and Paid_Status=0 and Type_Buy='آجل'";
                query1 = "select Money from Client_Rest_Money where Customer_ID=" + txtCustomerID.Text;
            }
            else
            {
                query = "select distinct CustomerBill_ID as 'رقم الفاتورة' " +/* ,delegate.Delegate_Name as 'المندوب' */"" + ", Total_CostAD as 'اجمالي الفاتورة',customer_bill.Branch_Name as 'الفرع',Bill_Date as'التاريخ'  from customer_bill " +/*inner join delegate on delegate.Delegate_ID=customer_bill.Delegate_ID */"" + "where Client_ID=" + txtClientID.Text + "  and Paid_Status=0 and Type_Buy='آجل'";
                query1 = "select Money from Client_Rest_Money where Client_ID=" + txtClientID.Text;
            }

            MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dataGridView1.ColumnCount > 0)
                dataGridView1.Columns.Remove(dataGridView1.Columns[4]);
            dataGridView1.DataSource = dt;
            dataGridView1.Columns[1].Visible = false;
            DataGridViewCheckBoxColumn checkColumn = new DataGridViewCheckBoxColumn();
            checkColumn.Name = "PaidOrNot";
            checkColumn.HeaderText = "تم الدفع"; 
       
            checkColumn.ReadOnly = false;
            checkColumn.FalseValue = false;
            checkColumn.TrueValue = true;
            // checkColumn.FillWeight = 10; //if the datagridview is resized (on form resize) the checkbox won't take up too much; value is relative to the other columns' fill values           
            dataGridView1.Columns.Add(checkColumn);
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            checkColumn.Width = 100;

            MySqlCommand com = new MySqlCommand(query1, dbconnection);
            if (com.ExecuteScalar() != null)
            {
                double money = Convert.ToDouble(com.ExecuteScalar());
                recivedMoney += money;
            }
            labRecivedMoney.Text = recivedMoney.ToString();
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
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}

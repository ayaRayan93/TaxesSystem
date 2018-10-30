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
    public partial class customerBills : Form
    {
        MySqlConnection dbconnection;
        MySqlConnection dbconnection1;
        private string Customer_Type;
        private bool loaded = false;

        public customerBills()
        {
            InitializeComponent();
            dbconnection = new MySqlConnection(connection.connectionString);
            dbconnection1 = new MySqlConnection(connection.connectionString);
            labClient.Visible = false;
            labCustomer.Visible = false;//label of مهندس/مقاول
            comClient.Visible = false;
            comCustomer.Visible = false;
            txtCustomerID.Visible = false;
            txtClientID.Visible = false;

        }
        //check type of customer if engineer,client or contract 
        private void radiotype_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton radio = (RadioButton)sender;
            Customer_Type = radio.Text;
            groupBox2.Visible = true;
            loaded = false;
            try
            {
                if (Customer_Type == "عميل")
                {
                    labCustomer.Visible = false;
                    comCustomer.Visible = false;
                    txtCustomerID.Visible = false;
                    labClient.Visible = true;
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
                    labCustomer.Visible = true;
                    comCustomer.Visible = true;
                    txtCustomerID.Visible = true;
                    labClient.Visible = false;
                    comClient.Visible = false;
                    txtClientID.Visible = false;


                    string query = "select * from customer where Customer_Type='" + Customer_Type + "'";
                    MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    comCustomer.DataSource = dt;
                    comCustomer.DisplayMember = dt.Columns["Customer_Name"].ToString();
                    comCustomer.ValueMember = dt.Columns["Customer_ID"].ToString();
                    comCustomer.Text = "";
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
        //when select customer(مهندس,مقاول)display in comCustomer the all clients of th customer 
        private void comCustomer_SelectedValueChanged(object sender, EventArgs e)
        {
            if (loaded)
            {
                txtCustomerID.Text = comCustomer.SelectedValue.ToString();

                labClient.Visible = true;
                comClient.Visible = true;
                txtClientID.Visible = true;

                try
                {
                    loaded = false;
                    string query = "select * from customer where Customer_ID in(select Client_ID from custmer_client where Customer_ID=" + comCustomer.SelectedValue + ")";
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
                                query = "select Customer_Name from customer where Customer_ID=" + txtClientID.Text + "";
                                com = new MySqlCommand(query, dbconnection);
                                if (com.ExecuteScalar() != null)
                                {
                                    Name = (string)com.ExecuteScalar();
                                    comClient.Text = Name;
                                    comClient.SelectedValue = txtClientID.Text;

                                }
                                else
                                {
                                    MessageBox.Show("there is no item with this id");
                                    dbconnection.Close();
                                    return;
                                }
                                break;
                            case "txtEngConID":
                                query = "select Customer_Name from customer where Customer_ID=" + txtCustomerID.Text + "";
                                com = new MySqlCommand(query, dbconnection);
                                if (com.ExecuteScalar() != null)
                                {
                                    Name = (string)com.ExecuteScalar();
                                    comCustomer.Text = Name;
                                    comCustomer.SelectedValue = txtCustomerID.Text;
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

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                dbconnection.Open();
                dbconnection1.Open();
                dataGridView1.Rows.Clear();
                if (txtClientID.Text != "" || txtCustomerID.Text != "")
                {
                    displayBill();
                    displayReturnBill();
                    displayPaidBill();
                    double totalBill=0, TotalReturn = 0,rest=0;

                    foreach (DataGridViewRow row1 in dataGridView1.Rows)
                    {
                        totalBill += Convert.ToDouble(row1.Cells[0].Value);
                        TotalReturn += Convert.ToDouble(row1.Cells[1].Value);
                        rest = totalBill - TotalReturn;
                    }

                    labTotalBillCost.Text = totalBill.ToString();
                    labTotalReturnCost.Text = TotalReturn.ToString();
                    labRest.Text = rest.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
            dbconnection1.Close();
        }

        //function
        // display Customer bills
        public void displayBill()
        {
            DateTime date = dateTimeFrom.Value.Date;
            string d = date.ToString("yyyy-MM-dd");
            DateTime date2 = dateTimeTo.Value.Date;
            string d2 = date2.ToString("yyyy-MM-dd");
            string query = "";
            if (txtClientID.Text != "" && txtCustomerID.Text != "")
            {
                query = "select * from customer_bill where Client_ID='" + txtClientID.Text + "' and Customer_ID='" + txtCustomerID.Text + "' and Bill_Date between '" + d + "' and '" + d2 + "'";
            }
            else if (txtClientID.Text == "" && txtCustomerID.Text != "")
            {
                // query = "select * from customer_bill where Client_ID is null and Customer_ID='" + txtCustomerID.Text + "' and Bill_Date between '" + d + "' and '" + d2 + "'";
                 query = "select * from customer_bill where  Customer_ID='" + txtCustomerID.Text + "' and Bill_Date between '" + d + "' and '" + d2 + "'";
            }
            else
            {
                query = "select * from customer_bill where Client_ID='" + txtClientID.Text + "' and Customer_ID is null and Bill_Date between '" + d + "' and '" + d2 + "'";

            }
            MySqlCommand com = new MySqlCommand(query, dbconnection1);
            MySqlDataReader dr = com.ExecuteReader();

            while (dr.Read())
            {
                int n = dataGridView1.Rows.Add();
                dataGridView1.Rows[n].Cells[0].Value = dr["Total_CostAD"].ToString();
                dataGridView1.Rows[n].Cells[1].Value = "0.00";
                dataGridView1.Rows[n].Cells[2].Value = dr["Dash_Bill_ID"].ToString();
                if (txtCustomerID.Text != "")
                {
                    String q = "select Customer_Name from customer where Customer_ID=" + dr["Customer_ID"].ToString();
                    MySqlCommand com1 = new MySqlCommand(q, dbconnection);
                    string Customer_Name = com1.ExecuteScalar().ToString();
                    dataGridView1.Rows[n].Cells[3].Value = Customer_Name;
                }
                else
                {
                    dataGridView1.Rows[n].Cells[3].Value = "";
                }
              
                    String q1 = "select Customer_Name from customer where Customer_ID=" + dr["Client_ID"].ToString();
                    MySqlCommand com11 = new MySqlCommand(q1, dbconnection);
                    string Customer_Name1 = com11.ExecuteScalar().ToString();
                    dataGridView1.Rows[n].Cells[4].Value = Customer_Name1;
               
                dataGridView1.Rows[n].Cells[5].Value = dr["Bill_Date"].ToString();
            }
            dr.Close();
        }

        // display Customer return bills
        public void displayReturnBill()
        {
            DateTime date = dateTimeFrom.Value.Date;
            string d = date.ToString("yyyy-MM-dd");
            DateTime date2 = dateTimeTo.Value.Date;
            string d2 = date2.ToString("yyyy-MM-dd");
            string query = "";
            if (txtClientID.Text != "" && txtCustomerID.Text != "")
            {
                query = "select * from customer_return_bill where Client_ID='" + txtClientID.Text + "' and Customer_ID='" + txtCustomerID.Text + "' and Date between '" + d + "' and '" + d2 + "'";
            }
            else if (txtClientID.Text == "" && txtCustomerID.Text != "")
            {
                //    query = "select * from customer_return_bill where Client_ID is null and Customer_ID='" + txtCustomerID.Text + "' and Date between '" + d + "' and '" + d2 + "'";
                query = "select * from customer_return_bill where  Customer_ID='" + txtCustomerID.Text + "' and Date between '" + d + "' and '" + d2 + "'";

            }
            else
            {
                query = "select * from customer_return_bill where Client_ID='" + txtClientID.Text + "' and Customer_ID is null and Date between '" + d + "' and '" + d2 + "'";

            }
            MySqlCommand com = new MySqlCommand(query, dbconnection1);
            MySqlDataReader dr = com.ExecuteReader();

            while (dr.Read())
            {
                int n = dataGridView1.Rows.Add();
                dataGridView1.Rows[n].Cells[0].Value = "0.00";
                dataGridView1.Rows[n].Cells[1].Value = dr["TotalCostAD"].ToString();
                dataGridView1.Rows[n].Cells[2].Value = dr["CustomerReturnBill_ID"].ToString();
                if (txtCustomerID.Text != "")
                {
                    String q = "select Customer_Name from customer where Customer_ID=" + dr["Customer_ID"].ToString();
                    MySqlCommand com1 = new MySqlCommand(q, dbconnection);
                    string Customer_Name = com1.ExecuteScalar().ToString();
                    dataGridView1.Rows[n].Cells[3].Value = Customer_Name;
                }
                else
                {
                    dataGridView1.Rows[n].Cells[3].Value = "";
                }
                if (txtClientID.Text != "")
                {
                    String q = "select Customer_Name from customer where Customer_ID=" + dr["Client_ID"].ToString();
                    MySqlCommand com1 = new MySqlCommand(q, dbconnection);
                    string Customer_Name = com1.ExecuteScalar().ToString();
                    dataGridView1.Rows[n].Cells[4].Value = Customer_Name;
                }
                else
                {
                    dataGridView1.Rows[n].Cells[4].Value = "";
                }


                dataGridView1.Rows[n].Cells[5].Value = dr["Date"].ToString();
            }
            dr.Close();
        }

        // display Customer Paid bills
        public void displayPaidBill()
        {
            DateTime date = dateTimeFrom.Value.Date;
            string d = date.ToString("yyyy-MM-dd");
            DateTime date2 = dateTimeTo.Value.Date;
            string d2 = date2.ToString("yyyy-MM-dd");
            string query = "";
            if (txtClientID.Text != "" && txtCustomerID.Text != "")
            {
                query = "select * from transitions where Beneficiary_Name='" + comCustomer.Text + "'  and Transition_Date between '" + d + "' and '" + d2 + "'";
            }
            else if (txtClientID.Text == "" && txtCustomerID.Text != "")
            {
                query = "select * from transitions where Beneficiary_Name='" + comCustomer.Text + "'  and Transition_Date between '" + d + "' and '" + d2 + "'";
            }
            else
            {
                query = "select * from transitions where Beneficiary_Name='" + comClient.Text + "'  and Transition_Date between '" + d + "' and '" + d2 + "'";

            }
            MySqlCommand com = new MySqlCommand(query, dbconnection1);
            MySqlDataReader dr = com.ExecuteReader();

            while (dr.Read())
            {
                int n = dataGridView1.Rows.Add();
                dataGridView1.Rows[n].Cells[0].Value = "0.00";
                dataGridView1.Rows[n].Cells[1].Value = dr["Transition_Amount"].ToString();
                dataGridView1.Rows[n].Cells[2].Value = dr["Transition_ID"].ToString();
                dataGridView1.Rows[n].Cells[3].Value = dr["Beneficiary_Name"].ToString();
                dataGridView1.Rows[n].Cells[4].Value = "";
                dataGridView1.Rows[n].Cells[5].Value = dr["Transition_Date"].ToString();
            }
            dr.Close();
        }

      

        private void customerBills_FormClosed(object sender, FormClosedEventArgs e)
        {
            Environment.Exit(0);
        }

      
    }
}

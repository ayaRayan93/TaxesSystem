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

namespace MainSystem.CustomerService
{
    public partial class displayCustomerBill : Form
    {
        MySqlConnection dbconnection;
        bool loaded = false;

        public displayCustomerBill()
        {
            InitializeComponent();

            dbconnection = new MySqlConnection(connection.connectionString);
        }
        
        private void displayCustomerBill_Load(object sender, EventArgs e)
        {
            try
            {
                dbconnection.Close();
                dbconnection.Open();
                this.txtPhoneNumber.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                this.txtPhoneNumber.AutoCompleteSource = AutoCompleteSource.CustomSource;
                
                string query = "select * from customer ";
                MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                DataTable dt = new DataTable();
                da.Fill(dt);
                comClient.DataSource = dt;
                comClient.DisplayMember = dt.Columns["Customer_Name"].ToString();
                comClient.ValueMember = dt.Columns["Customer_ID"].ToString();
                comClient.Text = "";
                txtClientID.Text = "";

                query = "select group_concat(Phone) from customer_phone;";
                MySqlCommand com = new MySqlCommand(query, dbconnection);
                string str = com.ExecuteScalar().ToString();
                string[] strArr = str.Split(',');

                AutoCompleteStringCollection collection = new AutoCompleteStringCollection();
                collection.AddRange(strArr);

                this.txtPhoneNumber.AutoCompleteCustomSource = collection;

                loaded = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }

        private void txtPhoneNumber_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    dbconnection.Close();
                    dbconnection.Open();
                    string q = "select customer.Customer_ID,Customer_Name from customer_phone inner join customer on customer.Customer_ID=customer_phone.Customer_ID where Phone='" + txtPhoneNumber.Text+"'";

                    MySqlCommand com = new MySqlCommand(q, dbconnection);
                    MySqlDataReader dr = com.ExecuteReader();
                    while (dr.Read())
                    {
                        txtClientID.Text = dr[0].ToString();
                        comClient.Text = dr[1].ToString();
                    }
                    dr.Close();
                    displayProducts();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }

        private void txtClientID_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    dbconnection.Close();
                    dbconnection.Open();
                    string query = "select Customer_Name from customer where Customer_ID=" + txtClientID.Text + "";
                    MySqlCommand com = new MySqlCommand(query, dbconnection);
                    if (com.ExecuteScalar() != null)
                    {
                        Name = (string)com.ExecuteScalar();
                        comClient.Text = Name;
                        loaded = false;
                        comClient.SelectedValue = txtClientID.Text;
                        loaded = true;
                        displayProducts();
                    }
                    else
                    {
                        MessageBox.Show("there is no item with this id");
                        dbconnection.Close();
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }

        private void comClient_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnNewChooes_Click(object sender, EventArgs e)
        {
            try
            {
                comClient.Text = "";
                txtClientID.Text = "";
                txtPhoneNumber.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                displayProducts();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
      
        //function
        public void displayProducts()
        {
            try
            {
                string subQuery = "";
                if (txtClientID.Text != "")
                {
                    subQuery = " where Customer_ID=" + txtClientID.Text+ " or Client_ID=" +txtClientID.Text;
                }
                
                if (txtPhoneNumber.Text != "")
                {
                    string q = "select Customer_ID from customer_phone where Phone='"+txtPhoneNumber.Text+"'";
           
                    MySqlCommand com = new MySqlCommand(q, dbconnection);
                    int Customer_ID =Convert.ToInt16(com.ExecuteScalar());
                    subQuery = " where Customer_ID=" + Customer_ID + " or Client_ID=" + txtClientID.Text;
                }

                string query = "SELECT CustomerBill_ID, customer_bill.Total_CostAD as 'اجمالي الفاتورة',branch.Branch_Name as 'الفرع',customer_bill.Branch_BillNumber as 'رقم الفاتورة', Type_Buy as 'نوع الفاتورة',Bill_Date as 'التاريخ' FROM customer_bill inner join branch on branch.Branch_ID=customer_bill.Branch_ID " + subQuery;
                MySqlDataAdapter adapterSets = new MySqlDataAdapter(query, dbconnection);
                string itemName = "concat( product.Product_Name,' ',type.Type_Name,' ',factory.Factory_Name,' ',groupo.Group_Name,' ' ,COALESCE(color.Color_Name,''),' ',COALESCE(size.Size_Value,''),' ',COALESCE(sort.Sort_Value,''),' ',COALESCE(data.Classification,''),' ',COALESCE(data.Description,''))as 'البند'";
                query = "SELECT product_bill.CustomerBill_ID ,data.Code as 'الكود' ," + itemName + " ,Quantity as 'الكمية' ,Returned as 'تم استرجاعها' from data INNER JOIN type ON type.Type_ID = data.Type_ID INNER JOIN product ON product.Product_ID = data.Product_ID INNER JOIN factory ON data.Factory_ID = factory.Factory_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID LEFT outer JOIN color ON data.Color_ID = color.Color_ID LEFT outer  JOIN size ON data.Size_ID = size.Size_ID LEFT outer  JOIN sort ON data.Sort_ID = sort.Sort_ID  inner join product_bill on product_bill.Data_ID=data.Data_ID inner join customer_bill on customer_bill.CustomerBill_ID=product_bill.CustomerBill_ID " + subQuery;

                MySqlDataAdapter AdapterProducts = new MySqlDataAdapter(query, dbconnection);
                DataSet dataSet11 = new DataSet();

                //Create DataTable objects for representing database's tables 
                adapterSets.Fill(dataSet11, "customer_bill");
                AdapterProducts.Fill(dataSet11, "product_bill");

                //Set up a master-detail relationship between the DataTables 
                DataColumn keyColumn = dataSet11.Tables["customer_bill"].Columns["CustomerBill_ID"];
                DataColumn foreignKeyColumn = dataSet11.Tables["product_bill"].Columns["CustomerBill_ID"];
                dataSet11.Relations.Add("بنود الفاتورة", keyColumn, foreignKeyColumn);

                //Bind the grid control to the data source 
                dataGridView1.DataSource = dataSet11.Tables["customer_bill"];
                gridView2.Columns[0].Visible = false;
         
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

    }
}

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

namespace MainSystem.Store.Export
{
    public partial class CustomerDeliveryReport : Form
    {
        MySqlConnection dbconnection;
        bool load = false;
        public CustomerDeliveryReport()
        {
            InitializeComponent();
            dbconnection = new MySqlConnection(connection.connectionString);
        }

        private void CustomerDeliveryReport_Load(object sender, EventArgs e)
        {
            try
            {
                dbconnection.Open();
                string query = "select * from store";
                MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                DataTable dt = new DataTable();
                da.Fill(dt);
                comStore.DataSource = dt;
                comStore.DisplayMember = dt.Columns["Store_Name"].ToString();
                comStore.ValueMember = dt.Columns["Store_ID"].ToString();
                comStore.Text = "";
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
                displayProducts();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }

        //function
        public void displayProducts()
        {
            try
            {
                load = false;
                string subQuery = " date(customer_permissions.Date) between '" + dateTimeFrom.Value.ToString("yyyy-MM-dd") + "' and '" + dateTimeTo.Value.ToString("yyyy-MM-dd") + "'";
                if (comStore.Text != "")
                {
                    subQuery += " and customer_permissions.Store_ID=" + comStore.SelectedValue;
                }

                if (txtPermissionStore.Text != "")
                {
                    subQuery = " customer_permissions.Customer_Permissin_ID=" + txtPermissionStore.Text;
                }

                string query = "SELECT Customer_Permissin_ID, customer_permissions.Customer_Permissin_ID as 'رقم الأذن', store.Store_Name as 'المخزن',customer_permissions.Date as 'التاريخ' FROM customer_permissions INNER JOIN store ON store.Store_ID = customer_permissions.Store_ID   WHERE " + subQuery;
                MySqlDataAdapter adapterSets = new MySqlDataAdapter(query, dbconnection);
                string itemName = "concat( product.Product_Name,' ',type.Type_Name,' ',factory.Factory_Name,' ',groupo.Group_Name,' ' ,COALESCE(color.Color_Name,''),' ',COALESCE(size.Size_Value,''),' ',COALESCE(sort.Sort_Value,''),' ',COALESCE(data.Classification,''),' ',COALESCE(data.Description,''))as 'البند'";
                query = "SELECT customer_permissions_details.Customer_Permissin_ID as 'رقم الأذن',data.Code as 'الكود' ," + itemName + " ,DeliveredQuantity as 'الكمية المستلمة',Quantity as 'الكمية' from data INNER JOIN type ON type.Type_ID = data.Type_ID INNER JOIN product ON product.Product_ID = data.Product_ID INNER JOIN factory ON data.Factory_ID = factory.Factory_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID LEFT outer JOIN color ON data.Color_ID = color.Color_ID LEFT outer  JOIN size ON data.Size_ID = size.Size_ID LEFT outer  JOIN sort ON data.Sort_ID = sort.Sort_ID  inner join customer_permissions_details on customer_permissions_details.Data_ID=data.Data_ID inner join customer_permissions on customer_permissions.Customer_Permissin_ID=customer_permissions_details.Customer_Permissin_ID  WHERE " + subQuery;

                MySqlDataAdapter AdapterProducts = new MySqlDataAdapter(query, dbconnection);
                DataSet dataSet11 = new DataSet();

                //Create DataTable objects for representing database's tables 
                adapterSets.Fill(dataSet11, "customer_permissions");
                AdapterProducts.Fill(dataSet11, "customer_permissions_details");

                //Set up a master-detail relationship between the DataTables 
                DataColumn keyColumn = dataSet11.Tables["customer_permissions"].Columns["رقم الأذن"];
                DataColumn foreignKeyColumn = dataSet11.Tables["customer_permissions_details"].Columns["رقم الأذن"];
                dataSet11.Relations.Add("بنود الأذن", keyColumn, foreignKeyColumn);

                //Bind the grid control to the data source 
                dataGridView1.DataSource = dataSet11.Tables["customer_permissions"];
                gridView2.Columns[0].Visible = false;
                load = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

    }
}

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
    public partial class MakeShipping : Form
    {
        MySqlConnection dbconnection, dbconnection1;
        List<int> listOfPermissinNumbers;
        bool loaded=false;

        public MakeShipping(List<int> arrInt)
        {
            try
            {
                dbconnection = new MySqlConnection(connection.connectionString);
                dbconnection1 = new MySqlConnection(connection.connectionString);
                listOfPermissinNumbers = new List<int>();
                List<BillID_StoreID> listOfBillID_StoreID = new List<BillID_StoreID>();
                InitializeComponent();
                listOfBillID_StoreID=getShippingData(arrInt);
                listOfPermissinNumbers = getPermissionNumbers(listOfBillID_StoreID);
                display(arrInt, listOfPermissinNumbers);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void MakeShipping_Load(object sender, EventArgs e)
        {
            try
            {
                dbconnection.Open();

                string query = "select * from cars ";
                MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                DataTable dt = new DataTable();
                da.Fill(dt);
                comCar.DataSource = dt;
                comCar.DisplayMember = dt.Columns["Car_Number"].ToString();
                comCar.ValueMember = dt.Columns["Car_ID"].ToString();
                comCar.Text = "";
                txtCar.Text = "";

                query = "select * from drivers ";
                da = new MySqlDataAdapter(query, dbconnection);
                dt = new DataTable();
                da.Fill(dt);
                comDriver.DataSource = dt;
                comDriver.DisplayMember = dt.Columns["Driver_Name"].ToString();
                comDriver.ValueMember = dt.Columns["Driver_ID"].ToString();
                comDriver.Text = "";
                txtDriver.Text = "";

                loaded = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }

        private void comDriver_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (loaded)
                {
                    try
                    {
                        txtDriver.Text = comDriver.SelectedValue.ToString();
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

        private void comCar_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (loaded)
                {
                    try
                    {
                        txtCar.Text = comCar.SelectedValue.ToString();
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

        private void txtDriver_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    dbconnection.Open();
                    string query = "select Driver_Name from zone where Driver_ID=" + txtDriver.Text + "";
                    MySqlCommand com = new MySqlCommand(query, dbconnection);
                    if (com.ExecuteScalar() != null)
                    {
                        Name = (string)com.ExecuteScalar();
                        comDriver.Text = Name;
                        txtDriver.Focus();
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

        private void txtCar_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    dbconnection.Open();
                    string query = "select Car_Name from zone where Car_ID=" + txtCar.Text + "";
                    MySqlCommand com = new MySqlCommand(query, dbconnection);
                    if (com.ExecuteScalar() != null)
                    {
                        Name = (string)com.ExecuteScalar();
                        comCar.Text = Name;
                        txtCar.Focus();
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

        private void btnShippingRecord_Click(object sender, EventArgs e)
        {
            try
            {
                dbconnection.Open();
                string query = "insert into CustomerShippingStorage (Driver_ID,Car_ID,Date) values (@Driver_ID,@Car_ID,@Date)";
                MySqlCommand com = new MySqlCommand(query,dbconnection);
                com.Parameters.Add("@Driver_ID", MySqlDbType.Int16, 11);
                com.Parameters["@Driver_ID"].Value = txtDriver.Text;
                com.Parameters.Add("@Car_ID", MySqlDbType.Int16, 11);
                com.Parameters["@Car_ID"].Value = txtCar.Text;
                com.Parameters.Add("@Date", MySqlDbType.Date);
                com.Parameters["@Date"].Value = DateTime.Now.Date;
                com.ExecuteNonQuery();

                query = "select CustomerShippingStorage_ID from CustomerShippingStorage order by CustomerShippingStorage_ID desc limit 1";
                com = new MySqlCommand(query, dbconnection);
                int id =(int) com.ExecuteScalar();

                string perIds = "";
                for (int i = 0; i < listOfPermissinNumbers.Count; i++)
                {
                    perIds += listOfPermissinNumbers[i] + ",";
                }
                perIds += 0;

                query = "update customer_permissions set CustomerShippingStorage_ID="+id + " where Permissin_ID in ("+ perIds + ")";
                com = new MySqlCommand(query, dbconnection);
                com.ExecuteNonQuery();
                List<StorePermissionsNumbers> listOfStorePermissionsNumbers = new List<StorePermissionsNumbers>();
                listOfStorePermissionsNumbers = getListOfStoreID_PremNumbers(perIds);
                ReportViewer ReportViewer = new ReportViewer(listOfStorePermissionsNumbers);
                ReportViewer.Show();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }

        //functions
        public void display(List<int> arrInt, List<int> arrPerID)
        {
            string ids = "",perId="";
            for (int i = 0; i < arrInt.Count; i++)
            {
                ids += arrInt[i] + ",";
            }
            ids += 0;

            for (int i = 0; i < arrPerID.Count; i++)
            {
                perId += arrPerID[i] + ",";
            }
            perId += 0;

            string query = "select DISTINCT Store_ID as 'كود المخزن',Store_Name as 'اسم المخزن' from product_bill where CustomerBill_ID in(" + ids+")";
            MySqlDataAdapter adapterSets = new MySqlDataAdapter(query, dbconnection);
            query = "SELECT Store_ID as 'كود المخزن',Permissin_ID as 'اذن المخزن' from customer_permissions where Permissin_ID in(" + perId + ") ";
            MySqlDataAdapter AdapterProducts = new MySqlDataAdapter(query, dbconnection);
            DataSet dataSet11 = new DataSet();

            //Create DataTable objects for representing database's tables 
            adapterSets.Fill(dataSet11, "Shipping");
            AdapterProducts.Fill(dataSet11, "Products");

            //Set up a master-detail relationship between the DataTables 
            DataColumn keyColumn = dataSet11.Tables["Shipping"].Columns["كود المخزن"];
            DataColumn foreignKeyColumn = dataSet11.Tables["Products"].Columns["كود المخزن"];
            dataSet11.Relations.Add("بنود الفاتورة", keyColumn, foreignKeyColumn);

            //Bind the grid control to the data source 
            gridControl1.DataSource = dataSet11.Tables["Shipping"];
        }

        public List<BillID_StoreID> getShippingData(List<int> arrInt)
        {
            List<BillID_StoreID> list = new List<BillID_StoreID>();

            for (int i = 0; i < arrInt.Count; i++)
            {
                BillID_StoreID billID_StoreID = new BillID_StoreID();
                billID_StoreID.Customer_Bill_ID = arrInt[i];
                billID_StoreID.listOfStoreID = new List<int>();
                dbconnection.Open();
                string query = "SELECT DISTINCT Store_ID from product_bill where CustomerBill_ID=" + arrInt[i];
                MySqlCommand com = new MySqlCommand(query, dbconnection);
                MySqlDataReader dr = com.ExecuteReader();
                while (dr.Read())
                {
                    billID_StoreID.listOfStoreID.Add((int)dr[0]);
                }
                dr.Close();

                list.Add(billID_StoreID);
                dbconnection.Close();
            }

            return list;
        }

        public List<int> getStoreIDs(List<int> arrInt)
        {
            string ids = "";
            for (int i = 0; i < arrInt.Count; i++)
            {
                ids += i + ",";
            }
            ids += 0;
            List<int> storeIds = new List<int>();
            string query = "SELECT DISTINCT Store_ID from product_bill where CustomerBill_ID in(" + ids+")";
            MySqlCommand com = new MySqlCommand(query, dbconnection);
            MySqlDataReader dr = com.ExecuteReader();
            while (dr.Read())
            {
                storeIds.Add((int)dr[0]);
            }
            return storeIds;
        }

        public List<int> getPermissionNumbers(List<BillID_StoreID> listOfBillID_StoreID)
        {
            dbconnection.Open();
            List<int> listOfPermissionIDs = new List<int>();
            for (int i = 0; i < listOfBillID_StoreID.Count; i++)
            {
              
                for (int j = 0; j < listOfBillID_StoreID[i].listOfStoreID.Count; j++)
                {
                    string query = "insert into customer_permissions (CustomerBill_ID,Store_ID) values (@CustomerBill_ID,@Store_ID)";
                    MySqlCommand com = new MySqlCommand(query, dbconnection);
                    com.Parameters.Add("@CustomerBill_ID", MySqlDbType.Int16, 11);
                    com.Parameters["@CustomerBill_ID"].Value = listOfBillID_StoreID[i].Customer_Bill_ID;
                    com.Parameters.Add("@Store_ID", MySqlDbType.Int16, 11);
                    com.Parameters["@Store_ID"].Value = listOfBillID_StoreID[i].listOfStoreID[j];
                    com.ExecuteNonQuery();
                    query = "select Permissin_ID from customer_permissions order by  Permissin_ID DESC limit 1";
                    com = new MySqlCommand(query, dbconnection);
                    int permissionId = (int)com.ExecuteScalar();
                    listOfPermissionIDs.Add(permissionId);
                }
            }
            dbconnection.Close();
            return listOfPermissionIDs;
        }

        public List<StorePermissionsNumbers> getListOfStoreID_PremNumbers(string perIds)
        {
            List<StorePermissionsNumbers> listOfStorePermissionsNumbers = new List<StorePermissionsNumbers>();
            string query = "SELECT customer_permissions.Store_ID,Store_Name,Permissin_ID from customer_permissions inner join store on customer_permissions.Store_ID=store.Store_ID where Permissin_ID in (" + perIds + ") GROUP BY Store_ID";
            MySqlCommand com = new MySqlCommand(query, dbconnection);
            MySqlDataReader dr = com.ExecuteReader();
            while (dr.Read())
            {
                StorePermissionsNumbers StorePermissionsNumbers = new StorePermissionsNumbers();
                StorePermissionsNumbers.PermissinNumbers =(int) dr[2];
                StorePermissionsNumbers.StoreName = dr[1].ToString();          
                listOfStorePermissionsNumbers.Add(StorePermissionsNumbers);
            }
            dr.Close();
            return listOfStorePermissionsNumbers;
        }
        public struct BillID_StoreID
        {
           public int Customer_Bill_ID;
           public List<int> listOfStoreID;
        }
        
     
      
    }
}

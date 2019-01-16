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
                billID_StoreID.listOfStoreID_DataID = new List<StoreID_DataID>();
                dbconnection.Open();
                string query = "SELECT DISTINCT Store_ID from product_bill where CustomerBill_ID="+ arrInt[i];
                MySqlCommand com = new MySqlCommand(query, dbconnection);
                MySqlDataReader dr = com.ExecuteReader();
                while(dr.Read())
                {
                    dbconnection1.Open();
                    StoreID_DataID storeID_DataID = new StoreID_DataID();
                    storeID_DataID.StoreID= (int)dr[0];
                    storeID_DataID.listOfDataIds = new List<int>();
                    string query1 = "SELECT  Data_ID from product_bill where Store_ID=" + dr[0].ToString() + " and CustomerBill_ID=" + arrInt[i];
                    MySqlCommand com1 = new MySqlCommand(query1, dbconnection1);
                    MySqlDataReader dr1 = com1.ExecuteReader();
                    while (dr1.Read())
                    {
                        storeID_DataID.listOfDataIds.Add((int)dr1[0]);
                    }
                    dr1.Close();
                    dbconnection1.Close();
                    billID_StoreID.listOfStoreID_DataID.Add(storeID_DataID);
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
                for (int j = 0; j < listOfBillID_StoreID[i].listOfStoreID_DataID.Count; j++)
                {
                    string query = "insert into customer_permissions (CustomerBill_ID,Store_ID) values (@CustomerBill_ID,@Store_ID)";
                    MySqlCommand com = new MySqlCommand(query, dbconnection);
                    com.Parameters.Add("@CustomerBill_ID", MySqlDbType.Int16, 11);
                    com.Parameters["@CustomerBill_ID"].Value = listOfBillID_StoreID[i].Customer_Bill_ID;
                    com.Parameters.Add("@Store_ID", MySqlDbType.Int16, 11);
                    com.Parameters["@Store_ID"].Value = listOfBillID_StoreID[i].listOfStoreID_DataID[j].StoreID;
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
        
        public struct BillID_StoreID
        {
           public int Customer_Bill_ID;
           public List<StoreID_DataID> listOfStoreID_DataID;
        }

        private void MakeShipping_Load(object sender, EventArgs e)
        {
            try
            {
                dbconnection.Open();

                string query = "select * from zone ";

                MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                DataTable dt = new DataTable();
                da.Fill(dt);
                //comZone.DataSource = dt;
                //comZone.DisplayMember = dt.Columns["Zone_Name"].ToString();
                //comZone.ValueMember = dt.Columns["Zone_ID"].ToString();
                //comZone.Text = "";
                //txtZone.Text = "";

                loaded = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }

        public struct StoreID_DataID
        {
            public int StoreID;
            public List<int> listOfDataIds;
        }


    }
}

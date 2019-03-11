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
    public partial class ShippingBillsWanted : Form
    {
        MySqlConnection dbconnection;
        bool loaded = false;
        MainForm MainForm;
        public ShippingBillsWanted(MainForm MainForm)
        {
            try
            {
                dbconnection = new MySqlConnection(connection.connectionString);
                this.MainForm = MainForm;
                InitializeComponent();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void ShippingBillsWanted_Load(object sender, EventArgs e)
        {
            try
            {
                dbconnection.Open();

                string query = "select * from zone ";

                MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                DataTable dt = new DataTable();
                da.Fill(dt);
                comZone.DataSource = dt;
                comZone.DisplayMember = dt.Columns["Zone_Name"].ToString();
                comZone.ValueMember = dt.Columns["Zone_ID"].ToString();
                comZone.Text = "";
                txtZone.Text = "";

                loaded = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }

        private void comZone_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                if (loaded)
                {
                    try
                    {
                        txtZone.Text = comZone.SelectedValue.ToString();
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

        private void txtZone_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    dbconnection.Open();
                    string query = "select Zone_Name from zone where Zone_ID=" + txtZone.Text + "";
                    MySqlCommand com = new MySqlCommand(query, dbconnection);
                    if (com.ExecuteScalar() != null)
                    {
                        Name = (string)com.ExecuteScalar();
                        comZone.Text = Name;
                        txtZone.Focus();
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


        private void newChoose_Click(object sender, EventArgs e)
        {
            try
            {
                comZone.Text = "";
                txtZone.Text = "";
                dateTimeFrom.Value = DateTime.Now.Date;
                dateTimeTo.Value = DateTime.Now.Date;
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
                string subQuery = "";
                if (txtZone.Text != "")
                {
                    subQuery = "and zone.Zone_ID="+txtZone.Text;
                }

                string query = "SELECT Shipping_ID, shipping.CustomerBill_ID as 'كود الفاتورة', shipping.Bill_Number as 'رقم الفاتورة',branch.Branch_Name as 'الفرع',customer.Customer_Name as 'العميل',shipping.Phone as 'التليفون',shipping.Address as 'العنوان',area.Area_Name as 'المنطقة',shipping.Description as 'البيان',shipping.Date as 'التاريخ' FROM shipping INNER JOIN customer ON customer.Customer_ID = shipping.Customer_ID INNER JOIN branch ON branch.Branch_ID = shipping.Branch_ID INNER JOIN area ON area.Area_ID = shipping.Area_ID inner join zone on area.Zone_ID=zone.Zone_ID WHERE shipping.Delivered=0 and date(shipping.Date) between '" + dateTimeFrom.Value.ToString("yyyy-MM-dd") + "' and '" + dateTimeTo.Value.ToString("yyyy-MM-dd") + "' "+subQuery;
                MySqlDataAdapter adapterSets = new MySqlDataAdapter(query, dbconnection);
                string itemName = "concat( product.Product_Name,' ',type.Type_Name,' ',factory.Factory_Name,' ',groupo.Group_Name,' ' ,COALESCE(color.Color_Name,''),' ',COALESCE(size.Size_Value,''),' ',COALESCE(sort.Sort_Value,''),' ',COALESCE(data.Classification,''),' ',COALESCE(data.Description,''))as 'البند'";
                query = "SELECT product_bill.CustomerBill_ID as 'كود الفاتورة',data.Code as 'الكود' ," + itemName + ",product_bill.Quantity as 'الكمية',data.Carton as 'الكرتنة',(product_bill.Quantity/data.Carton) as 'عدد الكراتين/الوحدات',product_bill.Store_Name as 'المخزن', data_photo.Photo as 'صورة' from data INNER JOIN type ON type.Type_ID = data.Type_ID INNER JOIN product ON product.Product_ID = data.Product_ID INNER JOIN factory ON data.Factory_ID = factory.Factory_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID LEFT outer JOIN color ON data.Color_ID = color.Color_ID LEFT outer  JOIN size ON data.Size_ID = size.Size_ID LEFT outer  JOIN sort ON data.Sort_ID = sort.Sort_ID INNER JOIN product_bill on data.Data_ID=product_bill.Data_ID INNER JOIN customer_bill on customer_bill.CustomerBill_ID=product_bill.CustomerBill_ID left join data_photo on data_photo.Data_ID=data.Data_ID inner join shipping on shipping.CustomerBill_ID=customer_bill.CustomerBill_ID  WHERE shipping.Delivered=0 and Returned='لا' and date(shipping.Date) between '" + dateTimeFrom.Value.ToString("yyyy-MM-dd") + "' and '" + dateTimeTo.Value.ToString("yyyy-MM-dd") + "' " + subQuery;
                MySqlDataAdapter AdapterProducts = new MySqlDataAdapter(query, dbconnection);
                DataSet dataSet11 = new DataSet();

                //Create DataTable objects for representing database's tables 
                adapterSets.Fill(dataSet11, "Shipping");
                AdapterProducts.Fill(dataSet11, "Products");

                //Set up a master-detail relationship between the DataTables 
                DataColumn keyColumn = dataSet11.Tables["Shipping"].Columns["كود الفاتورة"];
                DataColumn foreignKeyColumn = dataSet11.Tables["Products"].Columns["كود الفاتورة"];
                dataSet11.Relations.Add("بنود الفاتورة", keyColumn, foreignKeyColumn);
                
                //Bind the grid control to the data source 
                gridControl1.DataSource = dataSet11.Tables["Shipping"];
                gridView1.Columns[0].Visible = false;
             
            }
            catch (Exception ex)
            {
                gridControl1.DataSource = null;
               // MessageBox.Show(ex.Message);
            }
        }

        private void btnShippingRecord_Click(object sender, EventArgs e)
        {
            try
            {
                List<int> arrInt=new List<int>();
                string shippingIDs = "";
                foreach (int item in gridView1.GetSelectedRows())
                {
                    DataRowView dataRow =(DataRowView) gridView1.GetRow(item);
                    int billNum = (int)dataRow[1];
                    arrInt.Add(billNum);
                    int shippingID = (int)dataRow[0];
                    shippingIDs+=shippingID+",";
                }
                dbconnection.Open();
                shippingIDs += "0";
                string query = "update Shipping set Delivered=1 where Shipping_ID in ("+ shippingIDs + ")";
                MySqlCommand com = new MySqlCommand(query, dbconnection);
                com.ExecuteNonQuery();
                if (arrInt.Count>0)
                    MainForm.displayMakeShippingForm(arrInt);
                else
                    MessageBox.Show("حدد الفواتير المراد شحنها");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }
    }
}

using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Columns;
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
    public partial class PermissionsDelivery : Form
    {
        private MySqlConnection dbconnection, dbconnectionr;
        bool loaded = false;
        MainForm MainForm;
        public PermissionsDelivery(MainForm MainForm)
        {
            try
            {
                InitializeComponent();
                dbconnection = new MySqlConnection(connection.connectionString);
                dbconnectionr = new MySqlConnection(connection.connectionString);
                this.MainForm = MainForm;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void PermissionsDelivery_Load(object sender, EventArgs e)
        {
            try
            {
                dbconnection.Open();
                string query = "select * from store ";
                MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                DataTable dt = new DataTable();
                da.Fill(dt);
                comStore.DataSource = dt;
                comStore.DisplayMember = dt.Columns["Store_Name"].ToString();
                comStore.ValueMember = dt.Columns["Store_ID"].ToString();
                comStore.Text = "";
                comStore.Text = "";
                loaded = true;
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
                txtStoreID.Text = "";
                comStore.Text = "";
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
                dbconnection.Open();
                dbconnectionr.Open();

                ShippingPermissions();
                CustomerDeliveryBills();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
            dbconnectionr.Close();
        }

        private void btnPermissionDelivery_Click(object sender, EventArgs e)
        {
            try
            {
                MainForm.bindDisplayDeliveryForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        void gridView1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            try
            {
                DataRow dataRow = gridView1.GetDataRow(gridView1.FocusedRowHandle);             
                MainForm.bindDisplayDeliveryForm(dataRow.ItemArray[1].ToString(), 1);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        void gridView2_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            try
            {
                DataRow dataRow = gridView1.GetDataRow(gridView1.FocusedRowHandle);
                MainForm.bindDisplayDeliveryForm(dataRow.ItemArray[0].ToString(), 2);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
       
        //functions
        public void ShippingPermissions()
        {
            DateTime date = dateTimeFrom.Value;
            string d = date.ToString("yyyy-MM-dd HH:mm:ss");
            DateTime date2 = dateTimeTo.Value;
            string d2 = date2.ToString("yyyy-MM-dd HH:mm:ss");
            string subQuery = "";
            if (txtStoreID.Text != "")
                subQuery = " and customershippingstorage.Store_ID=" + txtStoreID.Text;

            string itemName = "concat( product.Product_Name,' ',type.Type_Name,' ',factory.Factory_Name,' ',groupo.Group_Name,' ' ,COALESCE(color.Color_Name,''),' ',COALESCE(size.Size_Value,''),' ',COALESCE(sort.Sort_Value,''),' ',COALESCE(data.Classification,''),' ',COALESCE(data.Description,''))as 'البند'";
            string DataTableRelations = "INNER JOIN type ON type.Type_ID = data.Type_ID INNER JOIN product ON product.Product_ID = data.Product_ID INNER JOIN factory ON data.Factory_ID = factory.Factory_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID LEFT outer JOIN color ON data.Color_ID = color.Color_ID LEFT outer  JOIN size ON data.Size_ID = size.Size_ID LEFT outer  JOIN sort ON data.Sort_ID = sort.Sort_ID";

            string query = "select customer_permissions.CustomerBill_ID as 'كود الفاتورة' from customershippingstorage inner join customer_permissions on customershippingstorage.CustomerShippingStorage_ID=customer_permissions.CustomerShippingStorage_ID  where  Date between '" + d + "' and '" + d2 + "' " + subQuery;
            MySqlCommand com = new MySqlCommand(query, dbconnectionr);
            MySqlDataReader dr = com.ExecuteReader();

            string customer_IDs = "";
            while (dr.Read())
            {
                customer_IDs += dr[0] + ",";
            }
            dr.Close();
            customer_IDs += 0;

            query = "select customer_permissions.CustomerBill_ID as 'كود الفاتورة', Permissin_ID as 'اذن رقم' ,Store_Name as 'المخزن' from customershippingstorage inner join customer_permissions on customershippingstorage.CustomerShippingStorage_ID=customer_permissions.CustomerShippingStorage_ID inner join store on Store.Store_ID=customer_permissions.Store_ID where  Date between '" + d + "' and '" + d2 + "' " + subQuery;
            MySqlDataAdapter adapterSets = new MySqlDataAdapter(query, dbconnection);
            query = "SELECT product_bill.CustomerBill_ID as 'كود الفاتورة',data.Code as 'الكود' ," + itemName + ",product_bill.Quantity as 'الكمية',data.Carton as 'الكرتنة',(product_bill.Quantity/data.Carton) as 'عدد الكراتين/الوحدات', data_photo.Photo as 'صورة' from data " + DataTableRelations + " INNER JOIN product_bill on data.Data_ID=product_bill.Data_ID INNER JOIN customer_bill on customer_bill.CustomerBill_ID=product_bill.CustomerBill_ID left join data_photo on data_photo.Data_ID=data.Data_ID inner join shipping on shipping.CustomerBill_ID=customer_bill.CustomerBill_ID  WHERE shipping.Delivered=1 and date(shipping.Date) between '" + dateTimeFrom.Value.ToString("yyyy-MM-dd") + "' and '" + dateTimeTo.Value.ToString("yyyy-MM-dd") + "' and product_bill.CustomerBill_ID in(" + customer_IDs + ") " + subQuery;
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
            AddUnboundColumngridView1();
            AddRepositorygridView1();
        }
        public void CustomerDeliveryBills()
        {
            DateTime date = dateTimeFrom.Value;
            string d = date.ToString("yyyy-MM-dd HH:mm:ss");
            DateTime date2 = dateTimeTo.Value;
            string d2 = date2.ToString("yyyy-MM-dd HH:mm:ss");
            string subQuery = "";
            if (txtStoreID.Text != "")
                subQuery = " and product_bill.Store_ID=" + txtStoreID.Text;

            string itemName = "concat( product.Product_Name,' ',type.Type_Name,' ',factory.Factory_Name,' ',groupo.Group_Name,' ' ,COALESCE(color.Color_Name,''),' ',COALESCE(size.Size_Value,''),' ',COALESCE(sort.Sort_Value,''),' ',COALESCE(data.Classification,''),' ',COALESCE(data.Description,''))as 'البند'";
            string DataTableRelations = "INNER JOIN type ON type.Type_ID = data.Type_ID INNER JOIN product ON product.Product_ID = data.Product_ID INNER JOIN factory ON data.Factory_ID = factory.Factory_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID LEFT outer JOIN color ON data.Color_ID = color.Color_ID LEFT outer  JOIN size ON data.Size_ID = size.Size_ID LEFT outer  JOIN sort ON data.Sort_ID = sort.Sort_ID";

            string query = "select CustomerBill_ID as 'كود الفاتورة' from customer_bill where RecivedType='العميل' and Paid_Status=1 and Shipped_Date between '" + d + "' and '" + d2 + "' " + subQuery;
            MySqlCommand com = new MySqlCommand(query, dbconnectionr);
            MySqlDataReader dr = com.ExecuteReader();

            string customer_IDs = "";
            while (dr.Read())
            {
                customer_IDs += dr[0] + ",";
            }
            dr.Close();
            customer_IDs += 0;

            query = "select customer_bill.CustomerBill_ID as 'كود الفاتورة', customer_bill.Customer_Name as 'العميل' ,store.Store_Name as 'المخزن' from customer_bill inner join product_bill on product_bill.CustomerBill_ID=customer_bill.CustomerBill_ID inner join store on Store.Store_ID=product_bill.Store_ID where  Shipped_Date between '" + d + "' and '" + d2 + "' " + subQuery;
            MySqlDataAdapter adapterSets = new MySqlDataAdapter(query, dbconnection);
            query = "SELECT product_bill.CustomerBill_ID as 'كود الفاتورة',data.Code as 'الكود' ," + itemName + ",product_bill.Quantity as 'الكمية',data.Carton as 'الكرتنة',(product_bill.Quantity/data.Carton) as 'عدد الكراتين/الوحدات', data_photo.Photo as 'صورة' from data " + DataTableRelations + " INNER JOIN product_bill on data.Data_ID=product_bill.Data_ID INNER JOIN customer_bill on customer_bill.CustomerBill_ID=product_bill.CustomerBill_ID left join data_photo on data_photo.Data_ID=data.Data_ID  WHERE date(Customer_bill.Shipped_Date) between '" + dateTimeFrom.Value.ToString("yyyy-MM-dd") + "' and '" + dateTimeTo.Value.ToString("yyyy-MM-dd") + "' and product_bill.CustomerBill_ID in(" + customer_IDs + ") " + subQuery;
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
            gridControl2.DataSource = dataSet11.Tables["Shipping"];
            AddUnboundColumngridView2();
            AddRepositorygridView2();
        }
        private void AddRepositorygridView1()
        {
            RepositoryItemButtonEdit edit = new RepositoryItemButtonEdit();
            edit.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            edit.ButtonClick += gridView1_ButtonClick;
            edit.Buttons[0].Caption = "تسليم اذن";
            edit.Buttons[0].Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Left;
            gridView1.Columns["تسليم اذن"].ColumnEdit = edit;
        }
        private void AddUnboundColumngridView1()
        {
            if (gridView1.Columns["تسليم اذن"] == null)
            {
                GridColumn unbColumn = gridView1.Columns.AddField("تسليم اذن");
                unbColumn.VisibleIndex = gridView1.Columns.Count;
                unbColumn.UnboundType = DevExpress.Data.UnboundColumnType.Decimal;
            }
        }
        private void AddRepositorygridView2()
        {
            RepositoryItemButtonEdit edit = new RepositoryItemButtonEdit();
            edit.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            edit.ButtonClick += gridView2_ButtonClick;
            edit.Buttons[0].Caption = "تسليم اذن";
            edit.Buttons[0].Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph;
            gridView2.Columns["تسليم اذن"].ColumnEdit = edit;
       
        }       
        private void AddUnboundColumngridView2()
        {
            if (gridView2.Columns["تسليم اذن"] == null)
            {
                GridColumn unbColumn = gridView2.Columns.AddField("تسليم اذن");
                unbColumn.VisibleIndex = gridView2.Columns.Count;
                unbColumn.UnboundType = DevExpress.Data.UnboundColumnType.Decimal;
            }
        }

    }
}

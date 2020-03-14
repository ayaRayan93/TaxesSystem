using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
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
    public partial class PermissionsDeliveryBillsConfirm : Form
    {
        private MySqlConnection dbconnection, dbconnectionr;
        bool loaded = false;
        MainForm MainForm;

        public PermissionsDeliveryBillsConfirm(MainForm MainForm)
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
                txtStoreID.Text = "";

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
                txtBranchID.Text = "";
                comBranch.Text = "";
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

                // ShippingPermissions();
                // CustomerDeliveryBills();
                displayBill();
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

    

        void gridView2_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            try
            {

                DataRow dataRow = gridView2.GetDataRow(gridView2.FocusedRowHandle);
                MainForm.bindDisplayDeliveryConfirmForm(dataRow.ItemArray[0].ToString(), dataRow.ItemArray[2].ToString(), 2);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void comStore_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                txtStoreID.Text = comStore.SelectedValue.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void comBranch_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                txtBranchID.Text = comBranch.SelectedValue.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txtStoreID_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    dbconnection.Close();
                    string query = "select Store_Name from store where Store_ID=" + txtStoreID.Text;
                    MySqlCommand comand = new MySqlCommand(query, dbconnection);
                    dbconnection.Open();
                    string Store_Name = comand.ExecuteScalar().ToString();
                    dbconnection.Close();
                    comStore.Text = Store_Name;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
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

        //functions    
        public void CustomerDeliveryBills()
        {
            DateTime date = dateTimeFrom.Value;
            string d = date.ToString("yyyy-MM-dd");
            DateTime date2 = dateTimeTo.Value;
            string d2 = date2.ToString("yyyy-MM-dd");
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

            query = "select distinct customer_bill.CustomerBill_ID as 'كود الفاتورة', customer_bill.Customer_Name as 'العميل' ,store.Store_Name as 'المخزن' from customer_bill inner join product_bill on product_bill.CustomerBill_ID=customer_bill.CustomerBill_ID inner join store on Store.Store_ID=product_bill.Store_ID where  Shipped_Date between '" + d + "' and '" + d2 + "' " + subQuery;
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
        public void displayBill()
        {
            DateTime date = dateTimeFrom.Value;
            string d = date.ToString("yyyy-MM-dd hh:mm:ss");
            DateTime date2 = dateTimeTo.Value;
            string d2 = date2.ToString("yyyy-MM-dd hh:mm:ss");
            string subQuery = "";
            if (txtStoreID.Text != "")
                subQuery += " and product_bill.Store_ID=" + txtStoreID.Text;
            if (txtBranchID.Text != "")
                subQuery += " and customer_bill.Branch_ID=" + txtBranchID.Text;


            string xQuery = "select CustomerBill_ID from customer_return_bill inner join customer_return_bill_details on customer_return_bill.CustomerReturnBill_ID=customer_return_bill_details.CustomerReturnBill_ID ";
            MySqlCommand com = new MySqlCommand(xQuery, dbconnection);
            MySqlDataReader dr = com.ExecuteReader();
            string str1 = "";
            while (dr.Read())
            {
                if(dr[0].ToString()!="0")
                str1 += dr[0].ToString() + ",";
            }
            dr.Close();
            str1 += 0;
          
            string query = "";
            if (d == d2)
                query = "select distinct Branch_BillNumber ,Branch_Name  ,Branch_ID,Customer_Name,Client_Name ,Bill_Date ,Shipped_Date  from customer_bill inner join product_bill on customer_bill.CustomerBill_ID=product_bill.CustomerBill_ID where customer_bill.CustomerBill_ID not in (" + str1 + ") and Shipped_Date <= '" + d2 + "' and RecivedType='العميل' and RecivedFlag='Draft' and  case when Type_Buy='كاش' then Paid_Status=1 when Type_Buy='آجل' then Type_Buy='آجل' end " + subQuery;
            else
                query = "select distinct Branch_BillNumber ,Branch_Name ,Customer_Name ,Client_Name ,Bill_Date ,Shipped_Date  from customer_bill inner join product_bill on customer_bill.CustomerBill_ID=product_bill.CustomerBill_ID where customer_bill.CustomerBill_ID not in (" + str1 + ") and Shipped_Date between '" + d + "' and '" + d2 + "' and RecivedType='العميل' and RecivedFlag='Draft' and case when Type_Buy='كاش' then Paid_Status=1 when Type_Buy='آجل' then Type_Buy='آجل' end  " + subQuery;

          
            MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
            DataTable dt = new DataTable();
            da.Fill(dt);
            gridControl2.DataSource = dt;
            gridView2.Columns[2].Visible = false;
      
            AddUnboundColumngridView2();
            AddRepositorygridView2();
        }
  
        private void gridView2_CustomUnboundColumnData(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
        {
            if (e.IsGetData)
            {
                e.Value = gridView2.GetRowHandle(e.ListSourceRowIndex) + 1;
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

        private void btnReport_Click(object sender, EventArgs e)
        {
            try
            {
                MainForm.bindReportPermissionForm(gridControl2, "تقرير أذونات تم تسليمها");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
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

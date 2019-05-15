using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using MySql.Data.MySqlClient;

namespace MainSystem
{
    public partial class CustomerReturnReportToPeriod : DevExpress.XtraEditors.XtraForm
    {
        MySqlConnection dbconnection;
        public CustomerReturnReportToPeriod()
        {
            InitializeComponent();
            dbconnection = new MySqlConnection(connection.connectionString);
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                string query = "SELECT CustomerReturnPermission_ID as 'رقم أذن المرتجع', CustomerBill_ID as 'كود الفاتورة', ClientReturnName as 'العميل',ClientRetunPhone as 'تلفون العميل',Date as 'تاريخ الاسترجاع' FROM customer_return_permission WHERE  date(Date) between '" + dateTimeFrom.Value.ToString("yyyy-MM-dd") + "' and '" + dateTimeTo.Value.ToString("yyyy-MM-dd")+"'";
                MySqlDataAdapter adapterSets = new MySqlDataAdapter(query, dbconnection);
                //string itemName = "concat( product.Product_Name,' ',type.Type_Name,' ',factory.Factory_Name,' ',groupo.Group_Name,' ' ,COALESCE(color.Color_Name,''),' ',COALESCE(size.Size_Value,''),' ',COALESCE(sort.Sort_Value,''),' ',COALESCE(data.Classification,''),' ',COALESCE(data.Description,''))as 'البند'";
                query = "SELECT customer_return_permission_details.CustomerReturnPermission_ID as 'رقم أذن المرتجع',data.Code as 'الكود' ,concat( product.Product_Name,' ',type.Type_Name,' ',factory.Factory_Name,' ',groupo.Group_Name,' ' ,COALESCE(color.Color_Name,''),' ',COALESCE(size.Size_Value,''),' ',COALESCE(sort.Sort_Value,''),' ',COALESCE(data.Classification,''),' ',COALESCE(data.Description,''))as 'البند',customer_return_permission_details.TotalQuantity as 'الكمية المسترجعة',customer_return_permission_details.Carton as 'الكرتنة',(customer_return_permission_details.TotalQuantity/customer_return_permission_details.Carton) as 'عدد الكراتين/الوحدات' from customer_return_permission_details INNER JOIN customer_return_permission on customer_return_permission.CustomerReturnPermission_ID=customer_return_permission_details.CustomerReturnPermission_ID INNER JOIN data INNER JOIN type ON type.Type_ID = data.Type_ID INNER JOIN product ON product.Product_ID = data.Product_ID INNER JOIN factory ON data.Factory_ID = factory.Factory_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID LEFT outer JOIN color ON data.Color_ID = color.Color_ID LEFT outer  JOIN size ON data.Size_ID = size.Size_ID LEFT outer  JOIN sort ON data.Sort_ID = sort.Sort_ID INNER JOIN product_bill on data.Data_ID=product_bill.Data_ID INNER JOIN customer_bill on customer_bill.CustomerBill_ID=product_bill.CustomerBill_ID left join data_photo on data_photo.Data_ID=data.Data_ID inner join shipping on shipping.CustomerBill_ID=customer_bill.CustomerBill_ID  WHERE  date(customer_return_permission.Date) between '" + dateTimeFrom.Value.ToString("yyyy-MM-dd") + "' and '" + dateTimeTo.Value.ToString("yyyy-MM-dd")+"'";
                MySqlDataAdapter AdapterProducts = new MySqlDataAdapter(query, dbconnection);
                DataSet dataSet11 = new DataSet();

                //Create DataTable objects for representing database's tables 
                adapterSets.Fill(dataSet11, "customer_return_permission");
                AdapterProducts.Fill(dataSet11, "customer_return_permission_details");

                //Set up a master-detail relationship between the DataTables 
                DataColumn keyColumn = dataSet11.Tables["customer_return_permission"].Columns["رقم أذن المرتجع"];
                DataColumn foreignKeyColumn = dataSet11.Tables["customer_return_permission_details"].Columns["رقم أذن المرتجع"];
                dataSet11.Relations.Add("بنود الفاتورة", keyColumn, foreignKeyColumn);

                //Bind the grid control to the data source 
                gridControl1.DataSource = dataSet11.Tables["customer_return_permission"];
                gridView1.Columns[0].Visible = false;

            }
            catch
            {
                gridControl1.DataSource = null;
                // MessageBox.Show(ex.Message);
            }
        }

        private void newChoose_Click(object sender, EventArgs e)
        {
            try
            {
                dateTimeFrom.Value = DateTime.Now.Date;
                dateTimeTo.Value = DateTime.Now.Date;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
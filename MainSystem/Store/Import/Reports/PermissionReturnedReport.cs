using DevExpress.XtraTab;
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
    public partial class PermissionReturnedReport : Form
    {
        MySqlConnection dbconnection;
        bool loaded = false;

        public PermissionReturnedReport(MainForm mainform, XtraTabControl tabControlContent)
        {
            InitializeComponent();
            dbconnection = new MySqlConnection(connection.connectionString);
        }
        private void requestStored_Load(object sender, EventArgs e)
        {
            try
            {
                string query = "select * from store";
                MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                DataTable dt = new DataTable();
                da.Fill(dt);
                comStore.DataSource = dt;
                comStore.DisplayMember = dt.Columns["Store_Name"].ToString();
                comStore.ValueMember = dt.Columns["Store_ID"].ToString();
                comStore.Text = "";
                txtStoreID.Text = "";

                loaded = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }
        
        private void comBranch_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (loaded)
                {
                    txtStoreID.Text = comStore.SelectedValue.ToString();
                    labBillNumber.Visible = true;
                    txtPermissionNumber.Visible = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txtBranchID_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    dbconnection.Open();
                    string query = "select Store_Name from store where Store_ID=" + txtStoreID.Text + "";
                    MySqlCommand com = new MySqlCommand(query, dbconnection);
                    if (com.ExecuteScalar() != null)
                    {
                        Name = (string)com.ExecuteScalar();
                        comStore.Text = Name;
                        comStore.SelectedValue = txtStoreID.Text;
                    }
                    else
                    {
                        MessageBox.Show("there is no item with this id");

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                dbconnection.Close();
            }
        }

        private void txtBillNumber_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    int supplierID, billNum = 0;
                    if (int.TryParse(txtStoreID.Text, out supplierID) && comStore.SelectedValue != null && int.TryParse(txtPermissionNumber.Text, out billNum))
                    {
                        gridControl1.DataSource = null;
                        DataSet sourceDataSet = new DataSet();
                        MySqlDataAdapter adapterPerm = null;
                        MySqlDataAdapter adapterSup = null;
                        MySqlDataAdapter adapterDetails = null;
                        adapterPerm = new MySqlDataAdapter("SELECT DISTINCT import_storage_return.ImportStorageReturn_ID as 'التسلسل',import_storage_return.Returned_Permission_Number as 'رقم اذن المرتجع',import_storage_return.Retrieval_Date as 'تاريخ الاسترجاع',import_storage_return.Reason as 'سبب الاسترجاع' FROM import_storage_return INNER JOIN import_storage_return_supplier ON import_storage_return_supplier.ImportStorageReturn_ID = import_storage_return.ImportStorageReturn_ID INNER JOIN import_storage_return_details ON import_storage_return_details.ImportStorageReturnSupplier_ID = import_storage_return_supplier.ImportStorageReturnSupplier_ID INNER JOIN supplier ON supplier.Supplier_ID = import_storage_return_supplier.Supplier_ID left JOIN store_places ON store_places.Store_Place_ID = import_storage_return_details.Store_Place_ID where import_storage_return.Store_ID=" + comStore.SelectedValue.ToString() + " and import_storage_return.Returned_Permission_Number=" + billNum, dbconnection);
                        adapterSup = new MySqlDataAdapter("SELECT DISTINCT import_storage_return.ImportStorageReturn_ID as 'التسلسل',supplier.Supplier_Name as 'المورد',import_storage_return_supplier.Supplier_Permission_Number as 'رقم اذن الاستلام' FROM import_storage_return INNER JOIN import_storage_return_supplier ON import_storage_return_supplier.ImportStorageReturn_ID = import_storage_return.ImportStorageReturn_ID INNER JOIN import_storage_return_details ON import_storage_return_details.ImportStorageReturnSupplier_ID = import_storage_return_supplier.ImportStorageReturnSupplier_ID INNER JOIN supplier ON supplier.Supplier_ID = import_storage_return_supplier.Supplier_ID left JOIN store_places ON store_places.Store_Place_ID = import_storage_return_details.Store_Place_ID where import_storage_return.Store_ID=" + comStore.SelectedValue.ToString() + " and import_storage_return.Returned_Permission_Number=" + billNum, dbconnection);
                        //,store_places.Store_Place_Code as 'مكان التخزين'
                        //order by SUBSTR(data.Code,1,16),color.Color_Name,data.Description,data.Sort_ID
                        adapterDetails = new MySqlDataAdapter("SELECT import_storage_return.ImportStorageReturn_ID as 'التسلسل',data.Code as 'الكود',type.Type_Name as 'النوع',concat(product.Product_Name,' ',COALESCE(color.Color_Name,''),' ',data.Description,' ',groupo.Group_Name,' ',factory.Factory_Name,' ',COALESCE(size.Size_Value,''),' ',COALESCE(sort.Sort_Value,'')) as 'الاسم',import_storage_return_details.Balatat as 'عدد البلتات',import_storage_return_details.Carton_Balata as 'عدد الكراتين',import_storage_return_details.Total_Meters as 'متر/قطعة',DATE_FORMAT(import_storage_return_details.Date, '%d-%m-%Y %T') as 'وقت الاسترجاع',import_storage_return_details.Reason as 'السبب' FROM import_storage_return INNER JOIN import_storage_return_supplier ON import_storage_return_supplier.ImportStorageReturn_ID = import_storage_return.ImportStorageReturn_ID INNER JOIN import_storage_return_details ON import_storage_return_details.ImportStorageReturnSupplier_ID = import_storage_return_supplier.ImportStorageReturnSupplier_ID INNER JOIN supplier ON supplier.Supplier_ID = import_storage_return_supplier.Supplier_ID left JOIN store_places ON store_places.Store_Place_ID = import_storage_return_details.Store_Place_ID INNER JOIN data ON import_storage_return_details.Data_ID = data.Data_ID INNER JOIN type ON type.Type_ID = data.Type_ID INNER JOIN product ON product.Product_ID = data.Product_ID INNER JOIN factory ON data.Factory_ID = factory.Factory_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID LEFT JOIN color ON color.Color_ID = data.Color_ID LEFT JOIN size ON size.Size_ID = data.Size_ID LEFT JOIN sort ON sort.Sort_ID = data.Sort_ID  where import_storage_return.Store_ID=" + comStore.SelectedValue.ToString() + " and import_storage_return.Returned_Permission_Number=" + billNum + " ", dbconnection);
                        adapterPerm.Fill(sourceDataSet, "import_storage_return");
                        adapterSup.Fill(sourceDataSet, "import_storage_return_supplier");
                        adapterDetails.Fill(sourceDataSet, "import_storage_return_details");
                        //Set up a master-detail relationship between the DataTables 
                        DataColumn keyColumn = sourceDataSet.Tables["import_storage_return"].Columns["التسلسل"];
                        DataColumn foreignKeyColumn = sourceDataSet.Tables["import_storage_return_supplier"].Columns["التسلسل"];
                        DataColumn foreignKeyColumn2 = sourceDataSet.Tables["import_storage_return_details"].Columns["التسلسل"];
                        sourceDataSet.Relations.Add("موردين الاذن", keyColumn, foreignKeyColumn);
                        sourceDataSet.Relations.Add("تفاصيل الاذن", foreignKeyColumn, foreignKeyColumn2);
                        gridControl1.DataSource = sourceDataSet.Tables["import_storage_return"];
                    }
                    else
                    {
                        MessageBox.Show("تاكد من البيانات");
                    }
                   
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                dbconnection.Close();
            }
        }

        private void btnDisplay_Click(object sender, EventArgs e)
        {
            try
            {
                DataSet sourceDataSet = new DataSet();
                int supplierID= 0;
                MySqlDataAdapter adapterPerm = null;
                MySqlDataAdapter adapterSup = null;
                MySqlDataAdapter adapterDetails = null;
                DateTime date = dateTimePicker1.Value.Date;
                string d = date.ToString("yyyy-MM-dd");
                DateTime date2 = dateTimePicker2.Value.Date;
                string d2 = date2.ToString("yyyy-MM-dd");
                if (int.TryParse(txtStoreID.Text, out supplierID) && comStore.SelectedValue != null)
                {
                    adapterPerm = new MySqlDataAdapter("SELECT DISTINCT import_storage_return.ImportStorageReturn_ID as 'التسلسل',import_storage_return.Returned_Permission_Number as 'رقم اذن المرتجع',import_storage_return.Retrieval_Date as 'تاريخ الاسترجاع',import_storage_return.Reason as 'سبب الاسترجاع' FROM import_storage_return INNER JOIN import_storage_return_supplier ON import_storage_return_supplier.ImportStorageReturn_ID = import_storage_return.ImportStorageReturn_ID INNER JOIN import_storage_return_details ON import_storage_return_details.ImportStorageReturnSupplier_ID = import_storage_return_supplier.ImportStorageReturnSupplier_ID INNER JOIN supplier ON supplier.Supplier_ID = import_storage_return_supplier.Supplier_ID left JOIN store_places ON store_places.Store_Place_ID = import_storage_return_details.Store_Place_ID where import_storage_return.Store_ID=" + comStore.SelectedValue.ToString() + " and import_storage_return.Retrieval_Date >='" + d + "' and import_storage_return.Retrieval_Date <='" + d2 + "'", dbconnection);
                    adapterSup = new MySqlDataAdapter("SELECT DISTINCT import_storage_return.ImportStorageReturn_ID as 'التسلسل',supplier.Supplier_Name as 'المورد',import_storage_return_supplier.Supplier_Permission_Number as 'رقم اذن الاستلام' FROM import_storage_return INNER JOIN import_storage_return_supplier ON import_storage_return_supplier.ImportStorageReturn_ID = import_storage_return.ImportStorageReturn_ID INNER JOIN import_storage_return_details ON import_storage_return_details.ImportStorageReturnSupplier_ID = import_storage_return_supplier.ImportStorageReturnSupplier_ID INNER JOIN supplier ON supplier.Supplier_ID = import_storage_return_supplier.Supplier_ID left JOIN store_places ON store_places.Store_Place_ID = import_storage_return_details.Store_Place_ID where import_storage_return.Store_ID=" + comStore.SelectedValue.ToString() + " and import_storage_return.Retrieval_Date >='" + d + "' and import_storage_return.Retrieval_Date <='" + d2 + "'", dbconnection);
                    //,store_places.Store_Place_Code as 'مكان التخزين'
                    //order by SUBSTR(data.Code,1,16),color.Color_Name,data.Description,data.Sort_ID
                    adapterDetails = new MySqlDataAdapter("SELECT import_storage_return.ImportStorageReturn_ID as 'التسلسل',data.Code as 'الكود',type.Type_Name as 'النوع',concat(product.Product_Name,' ',COALESCE(color.Color_Name,''),' ',data.Description,' ',groupo.Group_Name,' ',factory.Factory_Name,' ',COALESCE(size.Size_Value,''),' ',COALESCE(sort.Sort_Value,'')) as 'الاسم',import_storage_return_details.Balatat as 'عدد البلتات',import_storage_return_details.Carton_Balata as 'عدد الكراتين',import_storage_return_details.Total_Meters as 'متر/قطعة',DATE_FORMAT(import_storage_return_details.Date, '%d-%m-%Y %T') as 'وقت الاسترجاع',import_storage_return_details.Reason as 'السبب' FROM import_storage_return INNER JOIN import_storage_return_supplier ON import_storage_return_supplier.ImportStorageReturn_ID = import_storage_return.ImportStorageReturn_ID INNER JOIN import_storage_return_details ON import_storage_return_details.ImportStorageReturnSupplier_ID = import_storage_return_supplier.ImportStorageReturnSupplier_ID INNER JOIN supplier ON supplier.Supplier_ID = import_storage_return_supplier.Supplier_ID left JOIN store_places ON store_places.Store_Place_ID = import_storage_return_details.Store_Place_ID INNER JOIN data ON import_storage_return_details.Data_ID = data.Data_ID INNER JOIN type ON type.Type_ID = data.Type_ID INNER JOIN product ON product.Product_ID = data.Product_ID INNER JOIN factory ON data.Factory_ID = factory.Factory_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID LEFT JOIN color ON color.Color_ID = data.Color_ID LEFT JOIN size ON size.Size_ID = data.Size_ID LEFT JOIN sort ON sort.Sort_ID = data.Sort_ID where import_storage_return.Store_ID=" + comStore.SelectedValue.ToString() + " and import_storage_return.Retrieval_Date >='" + d + "' and import_storage_return.Retrieval_Date <='" + d2 + "' ", dbconnection);
                    adapterPerm.Fill(sourceDataSet, "import_storage_return");
                    adapterSup.Fill(sourceDataSet, "import_storage_return_supplier");
                    adapterDetails.Fill(sourceDataSet, "import_storage_return_details");
                    //Set up a master-detail relationship between the DataTables 
                    DataColumn keyColumn = sourceDataSet.Tables["import_storage_return"].Columns["التسلسل"];
                    DataColumn foreignKeyColumn = sourceDataSet.Tables["import_storage_return_supplier"].Columns["التسلسل"];
                    DataColumn foreignKeyColumn2 = sourceDataSet.Tables["import_storage_return_details"].Columns["التسلسل"];
                    sourceDataSet.Relations.Add("موردين الاذن", keyColumn, foreignKeyColumn);
                    sourceDataSet.Relations.Add("تفاصيل الاذن", foreignKeyColumn, foreignKeyColumn2);
                    gridControl1.DataSource = sourceDataSet.Tables["import_storage_return"];
                }
                else
                {
                    MessageBox.Show("تاكد من البيانات");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }
    }
}

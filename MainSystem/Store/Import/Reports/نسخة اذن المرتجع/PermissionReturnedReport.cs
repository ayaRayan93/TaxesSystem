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
        MySqlConnection dbconnection, dbconnection1, dbconnection2, dbconnection3, dbconnection4;
        bool loaded = false;
        //DataRow row1 = null;
        XtraTabControl tabControlContentStore = null;
        int ImportStorageReturnID = 0;

        public PermissionReturnedReport(MainForm mainform, XtraTabControl tabControlContent)
        {
            InitializeComponent();
            dbconnection = new MySqlConnection(connection.connectionString);
            dbconnection1 = new MySqlConnection(connection.connectionString);
            dbconnection2 = new MySqlConnection(connection.connectionString);
            dbconnection3 = new MySqlConnection(connection.connectionString);
            dbconnection4 = new MySqlConnection(connection.connectionString);
            tabControlContentStore = tabControlContent;
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
                
                query = "select * from supplier";
                da = new MySqlDataAdapter(query, dbconnection);
                dt = new DataTable();
                da.Fill(dt);
                comSupplier.DataSource = dt;
                comSupplier.DisplayMember = dt.Columns["Supplier_Name"].ToString();
                comSupplier.ValueMember = dt.Columns["Supplier_ID"].ToString();
                comSupplier.Text = "";
                txtSupplierId.Text = "";
                
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

        private void gridView1_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            /*if (loaded)
            {
                try
                {
                    row1 = gridView1.GetDataRow(gridView1.GetRowHandle(e.RowHandle));
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                dbconnection.Close();
            }*/
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if(UserControl.userType == 1 || UserControl.userType == 13 || UserControl.userType == 7)
            {
                int supplierID, billNum = 0;
                if (int.TryParse(txtStoreID.Text, out supplierID) && comStore.SelectedValue != null && int.TryParse(txtReturnedPermissionNum.Text, out billNum))
                {
                    try
                    {
                        StorageReturnBill_Update form = new StorageReturnBill_Update(ImportStorageReturnID, comStore.SelectedValue.ToString(), billNum, txtPermissionNum.Text, txtReason.Text, dateTimePicker1.Value, this, tabControlContentStore);
                        form.ShowDialog();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }

        private void txtBillNumber_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    int supplierID, billNum = 0;
                    if (int.TryParse(txtStoreID.Text, out supplierID) && comStore.SelectedValue != null && int.TryParse(txtReturnedPermissionNum.Text, out billNum))
                    {
                        #region edit
                        /*gridControl1.DataSource = null;
                        DataSet sourceDataSet = new DataSet();
                        MySqlDataAdapter adapterPerm = null;
                        MySqlDataAdapter adapterSup = null;
                        MySqlDataAdapter adapterDetails = null;
                        adapterPerm = new MySqlDataAdapter("SELECT DISTINCT import_storage_return.ImportStorageReturn_ID as 'التسلسل',import_storage_return.Returned_Permission_Number as 'رقم اذن المرتجع',import_storage_return.Retrieval_Date as 'تاريخ الاسترجاع',import_storage_return.Reason as 'سبب الاسترجاع',import_storage_return.Import_Permission_Number as 'رقم اذن المخزن' FROM import_storage_return INNER JOIN import_storage_return_supplier ON import_storage_return_supplier.ImportStorageReturn_ID = import_storage_return.ImportStorageReturn_ID INNER JOIN import_storage_return_details ON import_storage_return_details.ImportStorageReturnSupplier_ID = import_storage_return_supplier.ImportStorageReturnSupplier_ID INNER JOIN supplier ON supplier.Supplier_ID = import_storage_return_supplier.Supplier_ID left JOIN store_places ON store_places.Store_Place_ID = import_storage_return_details.Store_Place_ID where import_storage_return.Store_ID=" + comStore.SelectedValue.ToString() + " and import_storage_return.Returned_Permission_Number=" + billNum, dbconnection);
                        adapterSup = new MySqlDataAdapter("SELECT DISTINCT import_storage_return.ImportStorageReturn_ID as 'التسلسل',supplier.Supplier_Name as 'المورد',import_storage_return_supplier.Supplier_Permission_Number as 'رقم اذن الاستلام',import_storage_return_supplier.ImportStorageReturnSupplier_ID as 'ID' FROM import_storage_return INNER JOIN import_storage_return_supplier ON import_storage_return_supplier.ImportStorageReturn_ID = import_storage_return.ImportStorageReturn_ID INNER JOIN import_storage_return_details ON import_storage_return_details.ImportStorageReturnSupplier_ID = import_storage_return_supplier.ImportStorageReturnSupplier_ID INNER JOIN supplier ON supplier.Supplier_ID = import_storage_return_supplier.Supplier_ID left JOIN store_places ON store_places.Store_Place_ID = import_storage_return_details.Store_Place_ID where import_storage_return.Store_ID=" + comStore.SelectedValue.ToString() + " and import_storage_return.Returned_Permission_Number=" + billNum, dbconnection);
                        //,store_places.Store_Place_Code as 'مكان التخزين'
                        //order by SUBSTR(data.Code,1,16),color.Color_Name,data.Description,data.Sort_ID
                        adapterDetails = new MySqlDataAdapter("SELECT import_storage_return_details.ImportStorageReturnSupplier_ID as 'التسلسل',data.Code as 'الكود',type.Type_Name as 'النوع',concat(product.Product_Name,' ',COALESCE(color.Color_Name,''),' ',data.Description,' ',groupo.Group_Name,' ',factory.Factory_Name,' ',COALESCE(size.Size_Value,''),' ',COALESCE(sort.Sort_Value,'')) as 'الاسم',import_storage_return_details.Balatat as 'عدد البلتات',import_storage_return_details.Carton_Balata as 'عدد الكراتين',import_storage_return_details.Total_Meters as 'متر/قطعة',DATE_FORMAT(import_storage_return_details.Date, '%d-%m-%Y %T') as 'وقت الاسترجاع',import_storage_return_details.Reason as 'السبب' FROM import_storage_return INNER JOIN import_storage_return_supplier ON import_storage_return_supplier.ImportStorageReturn_ID = import_storage_return.ImportStorageReturn_ID INNER JOIN import_storage_return_details ON import_storage_return_details.ImportStorageReturnSupplier_ID = import_storage_return_supplier.ImportStorageReturnSupplier_ID INNER JOIN supplier ON supplier.Supplier_ID = import_storage_return_supplier.Supplier_ID left JOIN store_places ON store_places.Store_Place_ID = import_storage_return_details.Store_Place_ID INNER JOIN data ON import_storage_return_details.Data_ID = data.Data_ID INNER JOIN type ON type.Type_ID = data.Type_ID INNER JOIN product ON product.Product_ID = data.Product_ID INNER JOIN factory ON data.Factory_ID = factory.Factory_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID LEFT JOIN color ON color.Color_ID = data.Color_ID LEFT JOIN size ON size.Size_ID = data.Size_ID LEFT JOIN sort ON sort.Sort_ID = data.Sort_ID  where import_storage_return.Store_ID=" + comStore.SelectedValue.ToString() + " and import_storage_return.Returned_Permission_Number=" + billNum + " ", dbconnection);
                        adapterPerm.Fill(sourceDataSet, "import_storage_return");
                        adapterSup.Fill(sourceDataSet, "import_storage_return_supplier");
                        adapterDetails.Fill(sourceDataSet, "import_storage_return_details");
                        //Set up a master-detail relationship between the DataTables 
                        DataColumn keyColumn = sourceDataSet.Tables["import_storage_return"].Columns["التسلسل"];
                        DataColumn foreignKeyColumn = sourceDataSet.Tables["import_storage_return_supplier"].Columns["التسلسل"];
                        DataColumn foreignKeyColumn2 = sourceDataSet.Tables["import_storage_return_supplier"].Columns["ID"];
                        DataColumn foreignKeyColumn3 = sourceDataSet.Tables["import_storage_return_details"].Columns["التسلسل"];
                        sourceDataSet.Relations.Add("موردين الاذن", keyColumn, foreignKeyColumn);
                        sourceDataSet.Relations.Add("تفاصيل الاذن", foreignKeyColumn2, foreignKeyColumn3);
                        gridControl1.DataSource = sourceDataSet.Tables["import_storage_return"];*/
                        #endregion

                        dbconnection.Open();
                        string query = "SELECT DISTINCT import_storage_return.ImportStorageReturn_ID as 'التسلسل',import_storage_return_supplier.Supplier_ID,supplier.Supplier_Name as 'المورد',import_storage_return.Retrieval_Date as 'التاريخ',import_storage_return.Reason as 'سبب الاسترجاع',import_storage_return.Import_Permission_Number as 'رقم اذن المخزن' FROM import_storage_return INNER JOIN import_storage_return_supplier ON import_storage_return_supplier.ImportStorageReturn_ID = import_storage_return.ImportStorageReturn_ID INNER JOIN import_storage_return_details ON import_storage_return_details.ImportStorageReturnSupplier_ID = import_storage_return_supplier.ImportStorageReturnSupplier_ID INNER JOIN supplier ON supplier.Supplier_ID = import_storage_return_supplier.Supplier_ID left JOIN store_places ON store_places.Store_Place_ID = import_storage_return_details.Store_Place_ID where import_storage_return.Store_ID=" + comStore.SelectedValue.ToString() + " and import_storage_return.Returned_Permission_Number=" + billNum;
                        MySqlCommand comand = new MySqlCommand(query, dbconnection);
                        MySqlDataReader dr = comand.ExecuteReader();
                        while (dr.Read())
                        {
                            ImportStorageReturnID = Convert.ToInt16(dr["التسلسل"].ToString());
                            comSupplier.SelectedIndex = -1;
                            comSupplier.Text = dr["المورد"].ToString();
                            txtSupplierId.Text = dr["Supplier_ID"].ToString();
                            dateTimePicker1.Value = Convert.ToDateTime(dr["التاريخ"].ToString());
                            txtReason.Text = dr["سبب الاسترجاع"].ToString();
                            txtPermissionNum.Text = dr["رقم اذن المخزن"].ToString();
                        }
                        dr.Close();

                        query = "SELECT data.Code as 'الكود',type.Type_Name as 'النوع',concat(product.Product_Name,' ',COALESCE(color.Color_Name,''),' ',data.Description,' ',groupo.Group_Name,' ',factory.Factory_Name,' ',COALESCE(size.Size_Value,''),' ',COALESCE(sort.Sort_Value,'')) as 'الاسم',import_storage_return_details.Balatat as 'عدد البلتات',import_storage_return_details.Carton_Balata as 'عدد الكراتين',import_storage_return_details.Total_Meters as 'متر/قطعة',import_storage_return_supplier.Supplier_Permission_Number as 'رقم اذن الاستلام',DATE_FORMAT(import_storage_return_details.Date, '%d-%m-%Y %T') as 'وقت الاسترجاع',import_storage_return_details.Reason as 'السبب' FROM import_storage_return INNER JOIN import_storage_return_supplier ON import_storage_return_supplier.ImportStorageReturn_ID = import_storage_return.ImportStorageReturn_ID INNER JOIN import_storage_return_details ON import_storage_return_details.ImportStorageReturnSupplier_ID = import_storage_return_supplier.ImportStorageReturnSupplier_ID INNER JOIN supplier ON supplier.Supplier_ID = import_storage_return_supplier.Supplier_ID left JOIN store_places ON store_places.Store_Place_ID = import_storage_return_details.Store_Place_ID INNER JOIN data ON import_storage_return_details.Data_ID = data.Data_ID INNER JOIN type ON type.Type_ID = data.Type_ID INNER JOIN product ON product.Product_ID = data.Product_ID INNER JOIN factory ON data.Factory_ID = factory.Factory_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID LEFT JOIN color ON color.Color_ID = data.Color_ID LEFT JOIN size ON size.Size_ID = data.Size_ID LEFT JOIN sort ON sort.Sort_ID = data.Sort_ID  where import_storage_return.Store_ID=" + comStore.SelectedValue.ToString() + " and import_storage_return.Returned_Permission_Number=" + billNum;
                        MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        gridControl1.DataSource = dt;
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
            /*try
            {
                DataSet sourceDataSet = new DataSet();
                int supplierID= 0;
                MySqlDataAdapter adapterPerm = null;
                MySqlDataAdapter adapterSup = null;
                MySqlDataAdapter adapterDetails = null;
                DateTime date = dateTimePicker1.Value.Date;
                string d = date.ToString("dd-MM-yyyy");
                DateTime date2 = dateTimePicker2.Value.Date;
                string d2 = date2.ToString("dd-MM-yyyy");
                if (int.TryParse(txtStoreID.Text, out supplierID) && comStore.SelectedValue != null)
                {
                    adapterPerm = new MySqlDataAdapter("SELECT DISTINCT import_storage_return.ImportStorageReturn_ID as 'التسلسل',import_storage_return.Returned_Permission_Number as 'رقم اذن المرتجع',import_storage_return.Retrieval_Date as 'تاريخ الاسترجاع',import_storage_return.Reason as 'سبب الاسترجاع',import_storage_return.Import_Permission_Number as 'رقم اذن المخزن' FROM import_storage_return INNER JOIN import_storage_return_supplier ON import_storage_return_supplier.ImportStorageReturn_ID = import_storage_return.ImportStorageReturn_ID INNER JOIN import_storage_return_details ON import_storage_return_details.ImportStorageReturnSupplier_ID = import_storage_return_supplier.ImportStorageReturnSupplier_ID INNER JOIN supplier ON supplier.Supplier_ID = import_storage_return_supplier.Supplier_ID left JOIN store_places ON store_places.Store_Place_ID = import_storage_return_details.Store_Place_ID where import_storage_return.Store_ID=" + comStore.SelectedValue.ToString() + " and DATE(import_storage_return.Retrieval_Date) between '" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "' and '" + dateTimePicker2.Value.ToString("yyyy-MM-dd") + "'", dbconnection);
                    adapterSup = new MySqlDataAdapter("SELECT DISTINCT import_storage_return.ImportStorageReturn_ID as 'التسلسل',supplier.Supplier_Name as 'المورد',import_storage_return_supplier.Supplier_Permission_Number as 'رقم اذن الاستلام',import_storage_return_supplier.ImportStorageReturnSupplier_ID as 'ID' FROM import_storage_return INNER JOIN import_storage_return_supplier ON import_storage_return_supplier.ImportStorageReturn_ID = import_storage_return.ImportStorageReturn_ID INNER JOIN import_storage_return_details ON import_storage_return_details.ImportStorageReturnSupplier_ID = import_storage_return_supplier.ImportStorageReturnSupplier_ID INNER JOIN supplier ON supplier.Supplier_ID = import_storage_return_supplier.Supplier_ID left JOIN store_places ON store_places.Store_Place_ID = import_storage_return_details.Store_Place_ID where import_storage_return.Store_ID=" + comStore.SelectedValue.ToString() + " and DATE(import_storage_return.Retrieval_Date) between '" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "' and '" + dateTimePicker2.Value.ToString("yyyy-MM-dd") + "'", dbconnection);
                    //,store_places.Store_Place_Code as 'مكان التخزين'
                    //order by SUBSTR(data.Code,1,16),color.Color_Name,data.Description,data.Sort_ID
                    adapterDetails = new MySqlDataAdapter("SELECT import_storage_return_details.ImportStorageReturnSupplier_ID as 'التسلسل',data.Code as 'الكود',type.Type_Name as 'النوع',concat(product.Product_Name,' ',COALESCE(color.Color_Name,''),' ',data.Description,' ',groupo.Group_Name,' ',factory.Factory_Name,' ',COALESCE(size.Size_Value,''),' ',COALESCE(sort.Sort_Value,'')) as 'الاسم',import_storage_return_details.Balatat as 'عدد البلتات',import_storage_return_details.Carton_Balata as 'عدد الكراتين',import_storage_return_details.Total_Meters as 'متر/قطعة',DATE_FORMAT(import_storage_return_details.Date, '%d-%m-%Y %T') as 'وقت الاسترجاع',import_storage_return_details.Reason as 'السبب' FROM import_storage_return INNER JOIN import_storage_return_supplier ON import_storage_return_supplier.ImportStorageReturn_ID = import_storage_return.ImportStorageReturn_ID INNER JOIN import_storage_return_details ON import_storage_return_details.ImportStorageReturnSupplier_ID = import_storage_return_supplier.ImportStorageReturnSupplier_ID INNER JOIN supplier ON supplier.Supplier_ID = import_storage_return_supplier.Supplier_ID left JOIN store_places ON store_places.Store_Place_ID = import_storage_return_details.Store_Place_ID INNER JOIN data ON import_storage_return_details.Data_ID = data.Data_ID INNER JOIN type ON type.Type_ID = data.Type_ID INNER JOIN product ON product.Product_ID = data.Product_ID INNER JOIN factory ON data.Factory_ID = factory.Factory_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID LEFT JOIN color ON color.Color_ID = data.Color_ID LEFT JOIN size ON size.Size_ID = data.Size_ID LEFT JOIN sort ON sort.Sort_ID = data.Sort_ID where import_storage_return.Store_ID=" + comStore.SelectedValue.ToString() + " and DATE(import_storage_return.Retrieval_Date) between '" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "' and '" + dateTimePicker2.Value.ToString("yyyy-MM-dd") + "'", dbconnection);
                    adapterPerm.Fill(sourceDataSet, "import_storage_return");
                    adapterSup.Fill(sourceDataSet, "import_storage_return_supplier");
                    adapterDetails.Fill(sourceDataSet, "import_storage_return_details");
                    //Set up a master-detail relationship between the DataTables 
                    DataColumn keyColumn = sourceDataSet.Tables["import_storage_return"].Columns["التسلسل"];
                    DataColumn foreignKeyColumn = sourceDataSet.Tables["import_storage_return_supplier"].Columns["التسلسل"];
                    DataColumn foreignKeyColumn2 = sourceDataSet.Tables["import_storage_return_supplier"].Columns["ID"];
                    DataColumn foreignKeyColumn3 = sourceDataSet.Tables["import_storage_return_details"].Columns["التسلسل"];
                    sourceDataSet.Relations.Add("موردين الاذن", keyColumn, foreignKeyColumn);
                    sourceDataSet.Relations.Add("تفاصيل الاذن", foreignKeyColumn2, foreignKeyColumn3);
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
            dbconnection.Close();*/
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            try
            {
                int storeID = 0;
                int billNum = 0;
                if (int.TryParse(txtStoreID.Text, out storeID) && comStore.SelectedValue != null && int.TryParse(txtReturnedPermissionNum.Text, out billNum) && gridView1.RowCount > 0)
                {
                    string suppliers_Name = "";
                    dbconnection.Open();
                    //string query = "select Store_Name from store where Store_ID=" + comStore.SelectedValue.ToString();
                    //MySqlCommand com = new MySqlCommand(query, dbconnection);
                    string storeName = comStore.Text;

                    string q1, q2, q3 = "";
                    dbconnection1.Open();
                    dbconnection2.Open();
                    dbconnection3.Open();
                    q1 = "SELECT DISTINCT import_storage_return.ImportStorageReturn_ID as 'التسلسل',import_storage_return.Returned_Permission_Number as 'رقم اذن المرتجع',import_storage_return.Retrieval_Date as 'تاريخ الاسترجاع',import_storage_return.Reason as 'سبب الاسترجاع',import_storage_return.Import_Permission_Number as 'رقم اذن المخزن' FROM import_storage_return INNER JOIN import_storage_return_supplier ON import_storage_return_supplier.ImportStorageReturn_ID = import_storage_return.ImportStorageReturn_ID INNER JOIN import_storage_return_details ON import_storage_return_details.ImportStorageReturnSupplier_ID = import_storage_return_supplier.ImportStorageReturnSupplier_ID INNER JOIN supplier ON supplier.Supplier_ID = import_storage_return_supplier.Supplier_ID left JOIN store_places ON store_places.Store_Place_ID = import_storage_return_details.Store_Place_ID where import_storage_return.Returned_Permission_Number=" + txtReturnedPermissionNum.Text + " and import_storage_return.Store_ID=" + comStore.SelectedValue.ToString();
                    MySqlCommand com1 = new MySqlCommand(q1, dbconnection1);
                    MySqlDataReader dr1 = com1.ExecuteReader();
                    while (dr1.Read())
                    {
                        List<string> supplierList = new List<string>();
                        List<StorageReturn_Items> bi = new List<StorageReturn_Items>();
                        int supplierCount = 0;
                        int gridcount = 0;
                        q2 = "SELECT DISTINCT import_storage_return.ImportStorageReturn_ID as 'التسلسل',supplier.Supplier_Name as 'المورد',import_storage_return_supplier.Supplier_Permission_Number as 'رقم اذن الاستلام',import_storage_return_supplier.ImportStorageReturnSupplier_ID as 'ID' FROM import_storage_return INNER JOIN import_storage_return_supplier ON import_storage_return_supplier.ImportStorageReturn_ID = import_storage_return.ImportStorageReturn_ID INNER JOIN import_storage_return_details ON import_storage_return_details.ImportStorageReturnSupplier_ID = import_storage_return_supplier.ImportStorageReturnSupplier_ID INNER JOIN supplier ON supplier.Supplier_ID = import_storage_return_supplier.Supplier_ID left JOIN store_places ON store_places.Store_Place_ID = import_storage_return_details.Store_Place_ID where import_storage_return.ImportStorageReturn_ID=" + dr1["التسلسل"].ToString();
                        MySqlCommand com2 = new MySqlCommand(q2, dbconnection2);
                        MySqlDataReader dr2 = com2.ExecuteReader();
                        while (dr2.Read())
                        {
                            int supPermNum = 0;
                            supplierList.Add(dr2["المورد"].ToString());
                            bool flagTest = false;
                            q3 = "SELECT import_storage_return.ImportStorageReturn_ID as 'التسلسل',data.Code as 'الكود',type.Type_Name as 'النوع',concat(product.Product_Name,' ',COALESCE(color.Color_Name,''),' ',data.Description,' ',groupo.Group_Name,' ',factory.Factory_Name,' ',COALESCE(size.Size_Value,''),' ',COALESCE(sort.Sort_Value,'')) as 'الاسم',import_storage_return_details.Balatat as 'عدد البلتات',import_storage_return_details.Carton_Balata as 'عدد الكراتين',import_storage_return_details.Total_Meters as 'متر/قطعة',DATE_FORMAT(import_storage_return_details.Date, '%d-%m-%Y %T') as 'وقت الاسترجاع',import_storage_return_details.Reason as 'السبب' FROM import_storage_return INNER JOIN import_storage_return_supplier ON import_storage_return_supplier.ImportStorageReturn_ID = import_storage_return.ImportStorageReturn_ID INNER JOIN import_storage_return_details ON import_storage_return_details.ImportStorageReturnSupplier_ID = import_storage_return_supplier.ImportStorageReturnSupplier_ID INNER JOIN supplier ON supplier.Supplier_ID = import_storage_return_supplier.Supplier_ID left JOIN store_places ON store_places.Store_Place_ID = import_storage_return_details.Store_Place_ID INNER JOIN data ON import_storage_return_details.Data_ID = data.Data_ID INNER JOIN type ON type.Type_ID = data.Type_ID INNER JOIN product ON product.Product_ID = data.Product_ID INNER JOIN factory ON data.Factory_ID = factory.Factory_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID LEFT JOIN color ON color.Color_ID = data.Color_ID LEFT JOIN size ON size.Size_ID = data.Size_ID LEFT JOIN sort ON sort.Sort_ID = data.Sort_ID where import_storage_return_supplier.ImportStorageReturnSupplier_ID=" + dr2["ID"].ToString();
                            MySqlCommand com3 = new MySqlCommand(q3, dbconnection3);
                            MySqlDataReader dr3 = com3.ExecuteReader();
                            while (dr3.Read())
                            {
                                gridcount++;
                                double carton = 0;
                                double balate = 0;
                                double quantity = 0;

                                if (dr3["عدد البلتات"].ToString() != "")
                                {
                                    balate = Convert.ToDouble(dr3["عدد البلتات"].ToString());
                                }
                                if (dr3["عدد الكراتين"].ToString() != "")
                                {
                                    carton = Convert.ToDouble(dr3["عدد الكراتين"].ToString());
                                }
                                if (dr3["متر/قطعة"].ToString() != "")
                                {
                                    quantity = Convert.ToDouble(dr3["متر/قطعة"].ToString());
                                }
                                if (dr2["رقم اذن الاستلام"].ToString() != "")
                                {
                                    supPermNum = Convert.ToInt32(dr2["رقم اذن الاستلام"].ToString());
                                }

                                StorageReturn_Items item = new StorageReturn_Items() { Code = dr3["الكود"].ToString(), Product_Type = dr3["النوع"].ToString(), Product_Name = dr3["الاسم"].ToString(), Balatat = balate, Carton_Balata = carton, Total_Meters = quantity, Supplier_Permission_Number = supPermNum, Date = Convert.ToDateTime(dr3["وقت الاسترجاع"].ToString()).ToString("yyyy-MM-dd hh:mm:ss"), Reason = dr3["السبب"].ToString() };
                                bi.Add(item);
                            }
                            dr3.Close();

                            if (supplierCount == 0)
                            {
                                suppliers_Name += dr2["المورد"].ToString();
                            }

                            for (int j = 0; j < supplierList.Count; j++)
                            {
                                if (dr2["المورد"].ToString() == supplierList[j])
                                {
                                    flagTest = true;
                                }
                            }
                            if (!flagTest)
                            {
                                suppliers_Name += "," + dr2["المورد"].ToString();
                            }
                            supplierCount++;

                            Report_StorageReturnCopy f = new Report_StorageReturnCopy();
                            f.PrintInvoice(storeName, txtPermissionNum.Text, suppliers_Name, billNum, dateTimePicker1.Value.ToString(), txtReason.Text, bi);
                            f.ShowDialog();
                        }
                        dr2.Close();
                    }
                    dr1.Close();
                }
                else
                {
                    MessageBox.Show("يجب ادخال البيانات كاملة");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
            dbconnection1.Close();
            dbconnection2.Close();
            dbconnection3.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (UserControl.userType == 1)
            {
                try
                {
                    if (gridView1.RowCount > 0)
                    {
                        if (MessageBox.Show("هل انت متاكد انك تريد الحذف؟", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            dbconnection.Open();
                            dbconnection2.Open();
                            dbconnection3.Open();
                            string q2 = "SELECT DISTINCT import_storage_return.ImportStorageReturn_ID as 'التسلسل',supplier.Supplier_Name as 'المورد',import_storage_return_supplier.Supplier_Permission_Number as 'رقم اذن الاستلام',import_storage_return_supplier.ImportStorageReturnSupplier_ID as 'ID' FROM import_storage_return INNER JOIN import_storage_return_supplier ON import_storage_return_supplier.ImportStorageReturn_ID = import_storage_return.ImportStorageReturn_ID INNER JOIN import_storage_return_details ON import_storage_return_details.ImportStorageReturnSupplier_ID = import_storage_return_supplier.ImportStorageReturnSupplier_ID INNER JOIN supplier ON supplier.Supplier_ID = import_storage_return_supplier.Supplier_ID left JOIN store_places ON store_places.Store_Place_ID = import_storage_return_details.Store_Place_ID where import_storage_return.ImportStorageReturn_ID=" + ImportStorageReturnID;
                            MySqlCommand com2 = new MySqlCommand(q2, dbconnection2);
                            MySqlDataReader dr2 = com2.ExecuteReader();
                            while (dr2.Read())
                            {
                                string q3 = "SELECT import_storage_return.ImportStorageReturn_ID as 'التسلسل',data.Data_ID,data.Code as 'الكود',type.Type_Name as 'النوع',concat(product.Product_Name,' ',COALESCE(color.Color_Name,''),' ',data.Description,' ',groupo.Group_Name,' ',factory.Factory_Name,' ',COALESCE(size.Size_Value,''),' ',COALESCE(sort.Sort_Value,'')) as 'الاسم',import_storage_return_details.Balatat as 'عدد البلتات',import_storage_return_details.Carton_Balata as 'عدد الكراتين',import_storage_return_details.Total_Meters as 'متر/قطعة',DATE_FORMAT(import_storage_return_details.Date, '%d-%m-%Y %T') as 'وقت الاسترجاع',import_storage_return_details.Reason as 'السبب' FROM import_storage_return INNER JOIN import_storage_return_supplier ON import_storage_return_supplier.ImportStorageReturn_ID = import_storage_return.ImportStorageReturn_ID INNER JOIN import_storage_return_details ON import_storage_return_details.ImportStorageReturnSupplier_ID = import_storage_return_supplier.ImportStorageReturnSupplier_ID INNER JOIN supplier ON supplier.Supplier_ID = import_storage_return_supplier.Supplier_ID left JOIN store_places ON store_places.Store_Place_ID = import_storage_return_details.Store_Place_ID INNER JOIN data ON import_storage_return_details.Data_ID = data.Data_ID INNER JOIN type ON type.Type_ID = data.Type_ID INNER JOIN product ON product.Product_ID = data.Product_ID INNER JOIN factory ON data.Factory_ID = factory.Factory_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID LEFT JOIN color ON color.Color_ID = data.Color_ID LEFT JOIN size ON size.Size_ID = data.Size_ID LEFT JOIN sort ON sort.Sort_ID = data.Sort_ID where import_storage_return_supplier.ImportStorageReturnSupplier_ID=" + dr2["ID"].ToString();
                                MySqlCommand com3 = new MySqlCommand(q3, dbconnection3);
                                MySqlDataReader dr3 = com3.ExecuteReader();
                                while (dr3.Read())
                                {
                                    string query = "select Total_Meters from storage where Store_ID=" + comStore.SelectedValue.ToString() + " and Data_ID=" + dr3["Data_ID"].ToString();
                                    MySqlCommand com = new MySqlCommand(query, dbconnection);
                                    if (com.ExecuteScalar() != null)
                                    {
                                        double totalf = Convert.ToInt32(com.ExecuteScalar());
                                        query = "update storage set Total_Meters=" + (totalf + Convert.ToDouble(dr3["متر/قطعة"].ToString())) + " where Store_ID=" + comStore.SelectedValue.ToString() + " and Data_ID=" + dr3["Data_ID"].ToString();
                                        com = new MySqlCommand(query, dbconnection);
                                        com.ExecuteNonQuery();
                                        UserControl.ItemRecord("storage", "تعديل", Convert.ToInt16(dr3["Data_ID"].ToString()), DateTime.Now, "الغاء مرتجع اذن مخزن", dbconnection);
                                    }
                                    /*else
                                    {
                                        query = "SELECT store_places.Store_Place_ID FROM store_places INNER JOIN store ON store_places.Store_ID = store.Store_ID where store_places.Store_ID=" + comStore.SelectedValue.ToString();
                                        com = new MySqlCommand(query, dbconnection);
                                        string storePlaceId = com.ExecuteScalar().ToString();

                                        query = "insert into storage (Store_ID,Store_Place_ID,Type,Storage_Date,Data_ID,Total_Meters,Note) values (@Store_ID,@Store_Place_ID,@Type,@Date,@Data_ID,@TotalOfMeters,@Note)";
                                        com = new MySqlCommand(query, dbconnection);
                                        com.Parameters.Add("@Store_ID", MySqlDbType.Int16);
                                        com.Parameters["@Store_ID"].Value = comStore.SelectedValue.ToString();
                                        com.Parameters.Add("@Store_Place_ID", MySqlDbType.Int16);
                                        com.Parameters["@Store_Place_ID"].Value = storePlaceId;
                                        com.Parameters.Add("@Type", MySqlDbType.VarChar);
                                        com.Parameters["@Type"].Value = "بند";
                                        com.Parameters.Add("@Date", MySqlDbType.Date, 0);
                                        com.Parameters["@Date"].Value = DateTime.Now.ToString("yyyy-MM-dd");
                                        com.Parameters.Add("@Data_ID", MySqlDbType.Int16);
                                        com.Parameters["@Data_ID"].Value = dr3["Data_ID"].ToString();
                                        com.Parameters.Add("@TotalOfMeters", MySqlDbType.Decimal);
                                        com.Parameters["@TotalOfMeters"].Value = Convert.ToDecimal(dr3["متر/قطعة"].ToString());
                                        com.Parameters.Add("@Note", MySqlDbType.VarChar);
                                        com.Parameters["@Note"].Value = "الغاء مرتجع اذن مخزن";
                                        com.ExecuteNonQuery();
                                        //UserControl.ItemRecord("storage", "اضافة", Convert.ToInt16(dr3["Data_ID"].ToString()), DateTime.Now, "الغاء مرتجع اذن مخزن", dbconnection);
                                    }*/
                                }
                                dr3.Close();
                            }
                            dr2.Close();

                            string query2 = "delete from import_storage_return where ImportStorageReturn_ID=" + ImportStorageReturnID;
                            MySqlCommand com4 = new MySqlCommand(query2, dbconnection);
                            com4.ExecuteNonQuery();

                            gridView1.DeleteSelectedRows();
                        }
                    }
                    else
                    {
                        MessageBox.Show("يجب اختيار عنصر للحذف");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                dbconnection.Close();
                dbconnection2.Close();
                dbconnection3.Close();
            }
        }
    }
}

using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraTab;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MainSystem
{
    public partial class StorageReturnBill_Update : Form
    {
        MySqlConnection dbconnection, dbconnection2, dbconnection3, dbconnection4;
        
        bool flag = false;
        //int storeId = 0;
        bool flagCarton = false;
        DataRow row1 = null;
        bool loaded = false;
        //bool loadedPer = false;
        int storageReturnSupplierId = 0;
        int ReturnedPermissionNumber = 1;
        XtraTabControl xtraTabControlStores = null;
        DataRow selrow = null;
        string storeIdd = "";
        //int flagConfirm = 2;
        //int rowHandel1;

        public StorageReturnBill_Update(DataRow Selrow, string StoreIdd, PermissionReturnedReport permissionsReport, XtraTabControl tabControlContentStore)
        {
            InitializeComponent();
            dbconnection = new MySqlConnection(connection.connectionString);
            dbconnection2 = new MySqlConnection(connection.connectionString);
            dbconnection3 = new MySqlConnection(connection.connectionString);
            dbconnection4 = new MySqlConnection(connection.connectionString);
            
            comType.AutoCompleteMode = AutoCompleteMode.Suggest;
            comType.AutoCompleteSource = AutoCompleteSource.ListItems;
            comFactory.AutoCompleteMode = AutoCompleteMode.Suggest;
            comFactory.AutoCompleteSource = AutoCompleteSource.ListItems;
            comGroup.AutoCompleteMode = AutoCompleteMode.Suggest;
            comGroup.AutoCompleteSource = AutoCompleteSource.ListItems;
            comProduct.AutoCompleteMode = AutoCompleteMode.Suggest;
            comProduct.AutoCompleteSource = AutoCompleteSource.ListItems;

            xtraTabControlStores = tabControlContentStore;
            selrow = Selrow;
            storeIdd = StoreIdd;
        }

        private void StorageReturnBill_Load(object sender, EventArgs e)
        {
            try
            {
                DataHelper2 dh = new DataHelper2(DSparametr2.doubleDS);
                gridControl2.DataSource = dh.DataSet;
                gridControl2.DataMember = dh.DataMember;
                gridView2.InitNewRow += GridView2_InitNewRow;

                string query = "select * from store";
                MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                DataTable dt = new DataTable();
                da.Fill(dt);
                comStore.DataSource = dt;
                comStore.DisplayMember = dt.Columns["Store_Name"].ToString();
                comStore.ValueMember = dt.Columns["Store_ID"].ToString();
                comStore.Text = "";
                txtStoreID.Text = "";

                query = "select * from store";
                da = new MySqlDataAdapter(query, dbconnection);
                dt = new DataTable();
                da.Fill(dt);
                comStoreFilter.DataSource = dt;
                comStoreFilter.DisplayMember = dt.Columns["Store_Name"].ToString();
                comStoreFilter.ValueMember = dt.Columns["Store_ID"].ToString();
                comStoreFilter.Text = "";
                txtStoreFilterId.Text = "";

                query = "select * from supplier";
                da = new MySqlDataAdapter(query, dbconnection);
                dt = new DataTable();
                da.Fill(dt);
                comSupplier.DataSource = dt;
                comSupplier.DisplayMember = dt.Columns["Supplier_Name"].ToString();
                comSupplier.ValueMember = dt.Columns["Supplier_ID"].ToString();
                comSupplier.SelectedIndex = -1;
                txtSupplierId.Text = "";

                query = "select * from type";
                da = new MySqlDataAdapter(query, dbconnection);
                dt = new DataTable();
                da.Fill(dt);
                comType.DataSource = dt;
                comType.DisplayMember = dt.Columns["Type_Name"].ToString();
                comType.ValueMember = dt.Columns["Type_ID"].ToString();
                comType.Text = "";
                txtType.Text = "";

                query = "select * from factory";
                da = new MySqlDataAdapter(query, dbconnection);
                dt = new DataTable();
                da.Fill(dt);
                comFactory.DataSource = dt;
                comFactory.DisplayMember = dt.Columns["Factory_Name"].ToString();
                comFactory.ValueMember = dt.Columns["Factory_ID"].ToString();
                comFactory.Text = "";
                txtFactory.Text = "";

                query = "select * from groupo";
                da = new MySqlDataAdapter(query, dbconnection);
                dt = new DataTable();
                da.Fill(dt);
                comGroup.DataSource = dt;
                comGroup.DisplayMember = dt.Columns["Group_Name"].ToString();
                comGroup.ValueMember = dt.Columns["Group_ID"].ToString();
                comGroup.Text = "";
                txtGroup.Text = "";

                query = "select * from product";
                da = new MySqlDataAdapter(query, dbconnection);
                dt = new DataTable();
                da.Fill(dt);
                comProduct.DataSource = dt;
                comProduct.DisplayMember = dt.Columns["Product_Name"].ToString();
                comProduct.ValueMember = dt.Columns["Product_ID"].ToString();
                comProduct.Text = "";
                txtProduct.Text = "";

                if (selrow["رقم اذن المخزن"].ToString() != "")
                {
                    radioButtonReturnPermission.Checked = true;
                    comStore.SelectedValue = storeIdd;
                    txtPermissionNum.Text = selrow["رقم اذن المخزن"].ToString();
                }
                else
                {
                    radioButtonWithOutReturnPermission.Checked = true;
                    comStoreFilter.SelectedValue = storeIdd;
                }

                dbconnection.Open();
                if (radioButtonReturnPermission.Checked)
                {
                    #region add with permission
                    query = "SELECT import_storage_return.ImportStorageReturn_ID as 'التسلسل',data.Data_ID,data.Code as 'الكود',type.Type_Name as 'النوع',concat(product.Product_Name,' ',COALESCE(color.Color_Name,''),' ',data.Description,' ',groupo.Group_Name,' ',factory.Factory_Name,' ',COALESCE(size.Size_Value,''),' ',COALESCE(sort.Sort_Value,'')) as 'الاسم',import_storage_return_details.Balatat as 'عدد البلتات',import_storage_return_details.Carton_Balata as 'عدد الكراتين',import_storage_return_details.Total_Meters as 'متر/قطعة',data.Carton as 'الكرتنة',DATE_FORMAT(import_storage_return_details.Date, '%d-%m-%Y %T') as 'وقت الاسترجاع',import_storage_return_details.Reason as 'السبب',supplier_permission_details.Supplier_Permission_Details_ID,import_storage_return_supplier.Supplier_ID,import_storage_return_supplier.Supplier_Permission_Number as 'اذن استلام',supplier.Supplier_Name as 'المورد' FROM import_storage_return INNER JOIN import_storage_return_supplier ON import_storage_return_supplier.ImportStorageReturn_ID = import_storage_return.ImportStorageReturn_ID INNER JOIN import_storage_return_details ON import_storage_return_details.ImportStorageReturnSupplier_ID = import_storage_return_supplier.ImportStorageReturnSupplier_ID INNER JOIN storage_import_permission ON storage_import_permission.StorageImportPermission_ID = import_storage_return.StorageImportPermission_ID INNER JOIN import_supplier_permission ON import_supplier_permission.StorageImportPermission_ID = storage_import_permission.StorageImportPermission_ID and import_supplier_permission.Supplier_ID = import_storage_return_supplier.Supplier_ID  AND import_supplier_permission.Supplier_Permission_Number = import_storage_return_supplier.Supplier_Permission_Number INNER JOIN supplier_permission_details ON supplier_permission_details.ImportSupplierPermission_ID = import_supplier_permission.ImportSupplierPermission_ID INNER JOIN supplier ON supplier.Supplier_ID = import_storage_return_supplier.Supplier_ID left JOIN store_places ON store_places.Store_Place_ID = import_storage_return_details.Store_Place_ID INNER JOIN data ON import_storage_return_details.Data_ID = data.Data_ID INNER JOIN type ON type.Type_ID = data.Type_ID INNER JOIN product ON product.Product_ID = data.Product_ID INNER JOIN factory ON data.Factory_ID = factory.Factory_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID LEFT JOIN color ON color.Color_ID = data.Color_ID LEFT JOIN size ON size.Size_ID = data.Size_ID LEFT JOIN sort ON sort.Sort_ID = data.Sort_ID  where import_storage_return.Store_ID=" + comStore.SelectedValue.ToString() + " and import_storage_return.Returned_Permission_Number=" + selrow["رقم اذن المرتجع"].ToString();
                    MySqlCommand com = new MySqlCommand(query, dbconnection);
                    MySqlDataReader dr = com.ExecuteReader();
                    while (dr.Read())
                    {
                        gridView2.AddNewRow();
                        int rowHandle = gridView2.GetRowHandle(gridView2.DataRowCount);
                        if (gridView2.IsNewItemRow(rowHandle))
                        {
                            gridView2.SetRowCellValue(rowHandle, gridView2.Columns[0], dr["Data_ID"].ToString());
                            gridView2.SetRowCellValue(rowHandle, gridView2.Columns[1], dr["الكود"].ToString());
                            gridView2.SetRowCellValue(rowHandle, gridView2.Columns[2], dr["النوع"].ToString());
                            gridView2.SetRowCellValue(rowHandle, gridView2.Columns[3], dr["الاسم"].ToString());
                            gridView2.SetRowCellValue(rowHandle, gridView2.Columns[4], dr["الكرتنة"].ToString());
                            gridView2.SetRowCellValue(rowHandle, gridView2.Columns[5], dr["متر/قطعة"].ToString());
                            gridView2.SetRowCellValue(rowHandle, gridView2.Columns[6], dr["عدد الكراتين"].ToString());
                            gridView2.SetRowCellValue(rowHandle, gridView2.Columns[7], dr["عدد البلتات"].ToString());
                            gridView2.SetRowCellValue(rowHandle, gridView2.Columns[8], dr["السبب"].ToString());
                            gridView2.SetRowCellValue(rowHandle, gridView2.Columns[9], row1["Supplier_Permission_Details_ID"].ToString());
                            gridView2.SetRowCellValue(rowHandle, gridView2.Columns[10], row1["Supplier_ID"].ToString());
                            gridView2.SetRowCellValue(rowHandle, gridView2.Columns[11], row1["اذن استلام"].ToString());
                            //gridView2.SetRowCellValue(rowHandle, gridView2.Columns[12], row1["Store_Place_ID"].ToString());
                            gridView2.SetRowCellValue(rowHandle, gridView2.Columns[13], row1["المورد"].ToString());
                        }
                    }
                    dr.Close();
                    #endregion
                }
                else
                {
                    #region without permission
                    query = "SELECT import_storage_return.ImportStorageReturn_ID as 'التسلسل',data.Data_ID,data.Code as 'الكود',type.Type_Name as 'النوع',concat(product.Product_Name,' ',COALESCE(color.Color_Name,''),' ',data.Description,' ',groupo.Group_Name,' ',factory.Factory_Name,' ',COALESCE(size.Size_Value,''),' ',COALESCE(sort.Sort_Value,'')) as 'الاسم',import_storage_return_details.Balatat as 'عدد البلتات',import_storage_return_details.Carton_Balata as 'عدد الكراتين',import_storage_return_details.Total_Meters as 'متر/قطعة',data.Carton as 'الكرتنة',DATE_FORMAT(import_storage_return_details.Date, '%d-%m-%Y %T') as 'وقت الاسترجاع',import_storage_return_details.Reason as 'السبب',import_storage_return_supplier.Supplier_ID FROM import_storage_return INNER JOIN import_storage_return_supplier ON import_storage_return_supplier.ImportStorageReturn_ID = import_storage_return.ImportStorageReturn_ID INNER JOIN import_storage_return_details ON import_storage_return_details.ImportStorageReturnSupplier_ID = import_storage_return_supplier.ImportStorageReturnSupplier_ID INNER JOIN supplier ON supplier.Supplier_ID = import_storage_return_supplier.Supplier_ID left JOIN store_places ON store_places.Store_Place_ID = import_storage_return_details.Store_Place_ID INNER JOIN data ON import_storage_return_details.Data_ID = data.Data_ID INNER JOIN type ON type.Type_ID = data.Type_ID INNER JOIN product ON product.Product_ID = data.Product_ID INNER JOIN factory ON data.Factory_ID = factory.Factory_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID LEFT JOIN color ON color.Color_ID = data.Color_ID LEFT JOIN size ON size.Size_ID = data.Size_ID LEFT JOIN sort ON sort.Sort_ID = data.Sort_ID  where import_storage_return.Store_ID=" + comStoreFilter.SelectedValue.ToString() + " and import_storage_return.Returned_Permission_Number=" + selrow["رقم اذن المرتجع"].ToString();
                    MySqlCommand com = new MySqlCommand(query, dbconnection);
                    MySqlDataReader dr = com.ExecuteReader();
                    while (dr.Read())
                    {
                        comSupplier.SelectedValue = dr["Supplier_ID"].ToString();
                        txtSupplierId.Text = dr["Supplier_ID"].ToString();
                        gridView2.AddNewRow();
                        int rowHandle = gridView2.GetRowHandle(gridView2.DataRowCount);
                        if (gridView2.IsNewItemRow(rowHandle))
                        {
                            gridView2.SetRowCellValue(rowHandle, gridView2.Columns[0], dr["Data_ID"].ToString());
                            gridView2.SetRowCellValue(rowHandle, gridView2.Columns[1], dr["الكود"].ToString());
                            gridView2.SetRowCellValue(rowHandle, gridView2.Columns[2], dr["النوع"].ToString());
                            gridView2.SetRowCellValue(rowHandle, gridView2.Columns[3], dr["الاسم"].ToString());
                            gridView2.SetRowCellValue(rowHandle, gridView2.Columns[4], dr["الكرتنة"].ToString());
                            gridView2.SetRowCellValue(rowHandle, gridView2.Columns[5], dr["متر/قطعة"].ToString());
                            gridView2.SetRowCellValue(rowHandle, gridView2.Columns[6], dr["عدد الكراتين"].ToString());
                            gridView2.SetRowCellValue(rowHandle, gridView2.Columns[7], dr["عدد البلتات"].ToString());
                            gridView2.SetRowCellValue(rowHandle, gridView2.Columns[8], dr["السبب"].ToString());
                            gridView2.SetRowCellValue(rowHandle, gridView2.Columns[9], "0");
                            gridView2.SetRowCellValue(rowHandle, gridView2.Columns[10], "0");
                            gridView2.SetRowCellValue(rowHandle, gridView2.Columns[11], "0");
                            gridView2.SetRowCellValue(rowHandle, gridView2.Columns[12], "0");
                            gridView2.SetRowCellValue(rowHandle, gridView2.Columns[13], "");
                        }
                    }
                    dr.Close(); 
                    #endregion
                }
                loaded = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }

        private void GridView2_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            GridView view = sender as GridView;
            view.SetRowCellValue(e.RowHandle, gridView2.Columns[0], 0);
            view.SetRowCellValue(e.RowHandle, gridView2.Columns[1], "0");
            view.SetRowCellValue(e.RowHandle, gridView2.Columns[2], "");
            view.SetRowCellValue(e.RowHandle, gridView2.Columns[3], "");
            view.SetRowCellValue(e.RowHandle, gridView2.Columns[4], "0");
            view.SetRowCellValue(e.RowHandle, gridView2.Columns[5], "0");
            view.SetRowCellValue(e.RowHandle, gridView2.Columns[6], "0");
            view.SetRowCellValue(e.RowHandle, gridView2.Columns[7], "");
            view.SetRowCellValue(e.RowHandle, gridView2.Columns[8], "0");
            view.SetRowCellValue(e.RowHandle, gridView2.Columns[9], "0");
            view.SetRowCellValue(e.RowHandle, gridView2.Columns[10], "0");
            view.SetRowCellValue(e.RowHandle, gridView2.Columns[11], "0");
            view.SetRowCellValue(e.RowHandle, gridView2.Columns[12], "");
        }

        private void radioButtonReturnBill_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                panBillNumber.Visible = true;
                groupBox1.Visible = false;
                comType.Text = "";
                txtType.Text = "";
                comFactory.Text = "";
                txtFactory.Text = "";
                comGroup.Text = "";
                txtGroup.Text = "";
                comProduct.Text = "";
                txtProduct.Text = "";
                comStore.SelectedIndex = -1;
                comStoreFilter.SelectedIndex = -1;
                txtStoreID.Text = "";
                txtStoreFilterId.Text = "";
                txtPermissionNum.Text = "";
                comSupplier.SelectedIndex = -1;
                txtSupplierId.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }

        private void radioButtonWithOutReturnBill_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                panBillNumber.Visible = false;
                groupBox1.Visible = true;
                comType.Text = "";
                txtType.Text = "";
                comFactory.Text = "";
                txtFactory.Text = "";
                comGroup.Text = "";
                txtGroup.Text = "";
                comProduct.Text = "";
                txtProduct.Text = "";
                comStore.SelectedIndex = -1;
                comStoreFilter.SelectedIndex = -1;
                txtStoreID.Text = "";
                txtStoreFilterId.Text = "";
                txtPermissionNum.Text = "";
                comSupplier.SelectedIndex = -1;
                txtSupplierId.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }

        private void comType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                dbconnection.Close();
                dbconnection.Open();
                if (loaded)
                {
                    loaded = false;
                    txtType.Text = comType.SelectedValue.ToString();
                    comFactory.Focus();

                    filterFactory();
                    dbconnection.Close();
                    dbconnection.Open();
                    filterGroup();

                    if (txtType.Text != "1" && txtType.Text != "2" && txtType.Text != "9")
                    {
                        string query = "select * from product";
                        MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        comProduct.DataSource = dt;
                        comProduct.DisplayMember = dt.Columns["Product_Name"].ToString();
                        comProduct.ValueMember = dt.Columns["Product_ID"].ToString();
                        comProduct.Text = "";
                        txtProduct.Text = "";
                        label4.Text = "الصنف";
                        filterProduct();
                    }
                    else
                    {
                        string query = "select * from size";
                        MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        comProduct.DataSource = dt;
                        comProduct.DisplayMember = dt.Columns["Size_Value"].ToString();
                        comProduct.ValueMember = dt.Columns["Size_ID"].ToString();
                        comProduct.Text = "";
                        txtProduct.Text = "";
                        label4.Text = "المقاس";
                        filterProduct();
                    }
                    loaded = true;

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }
        private void comGroup_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (loaded)
                {
                    loaded = false;
                    dbconnection.Close();
                    dbconnection.Open();
                    txtGroup.Text = comGroup.SelectedValue.ToString();
                    comProduct.Focus();
                    filterProduct();
                    loaded = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }
        private void comFactory_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (loaded)
                {
                    loaded = false;
                    dbconnection.Close();
                    dbconnection.Open();
                    txtFactory.Text = comFactory.SelectedValue.ToString();
                    comGroup.Focus();
                    filterGroup();
                    filterProduct();
                    loaded = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }
        private void comProduct_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (loaded)
                {
                    loaded = false;
                    dbconnection.Close();
                    dbconnection.Open();
                    txtProduct.Text = comProduct.SelectedValue.ToString();
                    comType.Focus();
                    loaded = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }
        private void txtBox_KeyDown(object sender, KeyEventArgs e)
        {
            TextBox txtBox = (TextBox)sender;
            string query;
            MySqlCommand com;
            string Name;

            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    if (txtBox.Text != "")
                    {
                        dbconnection.Open();
                        switch (txtBox.Name)
                        {
                            case "txtType":
                                query = "select Type_Name from type where Type_ID='" + txtType.Text + "'";
                                com = new MySqlCommand(query, dbconnection);
                                if (com.ExecuteScalar() != null)
                                {
                                    Name = (string)com.ExecuteScalar();
                                    comType.Text = Name;
                                    txtFactory.Focus();
                                    dbconnection.Close();
                                }
                                else
                                {
                                    MessageBox.Show("there is no item with this id");
                                    dbconnection.Close();
                                    return;
                                }
                                break;
                            case "txtFactory":
                                query = "select Factory_Name from factory where Factory_ID='" + txtFactory.Text + "'";
                                com = new MySqlCommand(query, dbconnection);
                                if (com.ExecuteScalar() != null)
                                {
                                    Name = (string)com.ExecuteScalar();
                                    comFactory.Text = Name;
                                    txtGroup.Focus();
                                    dbconnection.Close();
                                }
                                else
                                {
                                    MessageBox.Show("there is no item with this id");
                                    dbconnection.Close();
                                    return;
                                }
                                break;
                            case "txtGroup":
                                query = "select Group_Name from groupo where Group_ID='" + txtGroup.Text + "'";
                                com = new MySqlCommand(query, dbconnection);
                                if (com.ExecuteScalar() != null)
                                {
                                    Name = (string)com.ExecuteScalar();
                                    comGroup.Text = Name;
                                    txtProduct.Focus();
                                    dbconnection.Close();
                                }
                                else
                                {
                                    MessageBox.Show("there is no item with this id");
                                    dbconnection.Close();
                                    return;
                                }
                                break;
                            case "txtProduct":
                                query = "select Product_Name from product where Product_ID='" + txtProduct.Text + "'";
                                com = new MySqlCommand(query, dbconnection);
                                if (com.ExecuteScalar() != null)
                                {
                                    Name = (string)com.ExecuteScalar();
                                    comProduct.Text = Name;
                                    txtType.Focus();
                                    dbconnection.Close();
                                }
                                else
                                {
                                    MessageBox.Show("there is no item with this id");
                                    dbconnection.Close();
                                    return;
                                }
                                break;
                            case "txtStoreID":
                                query = "select Store_Name from store where Store_ID=" + txtStoreID.Text;
                                com = new MySqlCommand(query, dbconnection);
                                if (com.ExecuteScalar() != null)
                                {
                                    Name = (string)com.ExecuteScalar();
                                    comStore.Text = Name;
                                }
                                else
                                {
                                    MessageBox.Show("there is no item with this id");
                                    dbconnection.Close();
                                    return;
                                }
                                break;

                            case "txtStoreFilterId":
                                query = "select Store_Name from store where Store_ID=" + txtStoreFilterId.Text;
                                com = new MySqlCommand(query, dbconnection);
                                if (com.ExecuteScalar() != null)
                                {
                                    Name = (string)com.ExecuteScalar();
                                    comStoreFilter.Text = Name;
                                }
                                else
                                {
                                    MessageBox.Show("there is no item with this id");
                                    dbconnection.Close();
                                    return;
                                }
                                break;

                            case "txtSupplierId":
                                query = "select Supplier_Name from supplier where Supplier_ID=" + txtSupplierId.Text;
                                com = new MySqlCommand(query, dbconnection);
                                if (com.ExecuteScalar() != null)
                                {
                                    Name = (string)com.ExecuteScalar();
                                    comSupplier.Text = Name;
                                }
                                else
                                {
                                    MessageBox.Show("there is no item with this id");
                                    dbconnection.Close();
                                    return;
                                }
                                break;
                        }
                    }
                }
                catch
                {
                    // MessageBox.Show(ex.ToString());
                }
                dbconnection.Close();
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (comType.Text != "" && comFactory.Text != "")
                {
                    if (comType.Text == "سيراميك" || comType.Text == "بورسلين")
                    {
                        if (comGroup.Text == "")
                        {
                            gridControl1.DataSource = null;
                            gridView1.Columns.Clear();

                            //for (int i = 0; i < gridView2.RowCount; i++)
                            //{
                            //    int rowHandle = gridView2.GetRowHandle(i);
                            //    gridView2.DeleteRow(rowHandle);
                            //}
                            MessageBox.Show("يجب اختيار النوع والمصنع والمجموعة على الاقل");
                            return;
                        }
                    }
                    displayData();
                }
                else
                {
                    gridControl1.DataSource = null;
                    gridView1.Columns.Clear();

                    //for (int i = 0; i < gridView2.RowCount; i++)
                    //{
                    //    int rowHandle = gridView2.GetRowHandle(i);
                    //    gridView2.DeleteRow(rowHandle);
                    //}
                    MessageBox.Show("يجب اختيار النوع والمصنع على الاقل");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }

        private void btnNewChooes_Click(object sender, EventArgs e)
        {
            try
            {
                comType.Text = "";
                comFactory.Text = "";
                comGroup.Text = "";
                comProduct.Text = "";

                txtType.Text = "";
                txtFactory.Text = "";
                txtGroup.Text = "";
                txtProduct.Text = "";

                displayData();
                for (int i = 0; i < gridView2.RowCount; i++)
                {
                    int rowHandle = gridView2.GetRowHandle(i);
                    gridView2.DeleteRow(rowHandle);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }

        private void btnAddToReturnBill_Click(object sender, EventArgs e)
        {
            try
            {
                if (row1 != null && ((comStore.SelectedValue != null && txtPermissionNum.Text != "") || comStoreFilter.SelectedValue != null) && txtCarton.Text != "" && txtBalat.Text != "" && txtCode.Text != "" && txtTotalMeter.Text != "" && txtItemReason.Text != "")
                {
                    int NoBalatat = 0;
                    int NoCartons = 0;
                    int permNum = 0;
                    double total = 0;
                    if (int.TryParse(txtBalat.Text, out NoBalatat) && int.TryParse(txtCarton.Text, out NoCartons) && double.TryParse(txtTotalMeter.Text, out total))
                    {
                        double carton = Convert.ToDouble(row1["الكرتنة"].ToString());
                        
                        if (radioButtonReturnPermission.Checked)
                        {
                            #region add with permission
                            if (int.TryParse(txtPermissionNum.Text, out permNum))
                            {
                                //if (IsAdded())
                                //{
                                //    MessageBox.Show("هذا العنصر تم اضافتة من قبل");
                                //    return;
                                //}

                                if (row1["عدد البلتات"].ToString() != "")
                                {
                                    if (NoBalatat > Convert.ToInt32(row1["عدد البلتات"].ToString()))
                                    {
                                        MessageBox.Show("تاكد من عدد البلتات");
                                        return;
                                    }
                                }
                                if (row1["عدد الكراتين"].ToString() != "")
                                {
                                    if (NoCartons > Convert.ToInt32(row1["عدد الكراتين"].ToString()))
                                    {
                                        MessageBox.Show("تاكد من عدد الكراتين");
                                        return;
                                    }
                                }
                                if (row1["متر/قطعة"].ToString() != "")
                                {
                                    if (total > Convert.ToDouble(row1["متر/قطعة"].ToString()))
                                    {
                                        MessageBox.Show("تاكد من متر/قطعة");
                                        return;
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("تاكد من متر/قطعة");
                                    return;
                                }

                                if (row1["الكمية المتاحة"].ToString() != "")
                                {
                                    if (total > Convert.ToDouble(row1["الكمية المتاحة"].ToString()))
                                    {
                                        MessageBox.Show("لا يوجد كمية كافية");
                                        return;
                                    }
                                }
                                else
                                {

                                    MessageBox.Show("لا يوجد كمية كافية");
                                    return;
                                }

                                gridView2.AddNewRow();

                                int rowHandle = gridView2.GetRowHandle(gridView2.DataRowCount);
                                if (gridView2.IsNewItemRow(rowHandle) && row1 != null)
                                {
                                    gridView2.SetRowCellValue(rowHandle, gridView2.Columns[0], row1["Data_ID"].ToString());
                                    gridView2.SetRowCellValue(rowHandle, gridView2.Columns[1], row1["الكود"].ToString());
                                    gridView2.SetRowCellValue(rowHandle, gridView2.Columns[2], row1["النوع"].ToString());
                                    gridView2.SetRowCellValue(rowHandle, gridView2.Columns[3], row1["الاسم"].ToString());
                                    gridView2.SetRowCellValue(rowHandle, gridView2.Columns[4], row1["الكرتنة"].ToString());
                                    gridView2.SetRowCellValue(rowHandle, gridView2.Columns[5], txtTotalMeter.Text);
                                    gridView2.SetRowCellValue(rowHandle, gridView2.Columns[6], txtCarton.Text);
                                    gridView2.SetRowCellValue(rowHandle, gridView2.Columns[7], txtBalat.Text);
                                    gridView2.SetRowCellValue(rowHandle, gridView2.Columns[8], txtItemReason.Text);
                                    gridView2.SetRowCellValue(rowHandle, gridView2.Columns[9], row1["Supplier_Permission_Details_ID"].ToString());
                                    gridView2.SetRowCellValue(rowHandle, gridView2.Columns[10], row1["Supplier_ID"].ToString());
                                    gridView2.SetRowCellValue(rowHandle, gridView2.Columns[11], row1["اذن استلام"].ToString());
                                    //gridView2.SetRowCellValue(rowHandle, gridView2.Columns[12], row1["Store_Place_ID"].ToString());
                                    gridView2.SetRowCellValue(rowHandle, gridView2.Columns[13], row1["المورد"].ToString());
                                }
                            }
                            else
                            {
                                MessageBox.Show("برجاء التاكد من ادخال البيانات بطريقة صحيحة");
                            }
                            #endregion
                        }
                        else
                        {
                            if (IsItemAdded())
                            {
                                MessageBox.Show("هذا العنصر تم اضافتة من قبل");
                                return;
                            }
                            
                            if (row1["الكمية المتاحة"].ToString() != "")
                            {
                                if (total > Convert.ToDouble(row1["الكمية المتاحة"].ToString()))
                                {
                                    MessageBox.Show("لا يوجد كمية كافية");
                                    return;
                                }
                            }
                            else
                            {

                                MessageBox.Show("لا يوجد كمية كافية");
                                return;
                            }

                            gridView2.AddNewRow();

                            int rowHandle = gridView2.GetRowHandle(gridView2.DataRowCount);
                            if (gridView2.IsNewItemRow(rowHandle) && row1 != null)
                            {
                                gridView2.SetRowCellValue(rowHandle, gridView2.Columns[0], row1["Data_ID"].ToString());
                                gridView2.SetRowCellValue(rowHandle, gridView2.Columns[1], row1["الكود"].ToString());
                                gridView2.SetRowCellValue(rowHandle, gridView2.Columns[2], row1["النوع"].ToString());
                                gridView2.SetRowCellValue(rowHandle, gridView2.Columns[3], row1["الاسم"].ToString());
                                gridView2.SetRowCellValue(rowHandle, gridView2.Columns[4], row1["الكرتنة"].ToString());
                                gridView2.SetRowCellValue(rowHandle, gridView2.Columns[5], txtTotalMeter.Text);
                                gridView2.SetRowCellValue(rowHandle, gridView2.Columns[6], txtCarton.Text);
                                gridView2.SetRowCellValue(rowHandle, gridView2.Columns[7], txtBalat.Text);
                                gridView2.SetRowCellValue(rowHandle, gridView2.Columns[8], txtItemReason.Text);
                                gridView2.SetRowCellValue(rowHandle, gridView2.Columns[9], "0");
                                gridView2.SetRowCellValue(rowHandle, gridView2.Columns[10], "0");
                                gridView2.SetRowCellValue(rowHandle, gridView2.Columns[11], "0");
                                gridView2.SetRowCellValue(rowHandle, gridView2.Columns[12], "0");
                                gridView2.SetRowCellValue(rowHandle, gridView2.Columns[13], "");
                            }
                        }
                        txtCode.Text = "";
                        txtBalat.Text = "0";
                        txtCarton.Text = "0";
                        txtTotalMeter.Text = "0";
                        txtItemReason.Text = "";
                    }
                    else
                    {
                        MessageBox.Show("برجاء التاكد من ادخال البيانات بطريقة صحيحة");
                    }
                }
                else
                {
                    MessageBox.Show("يجب ادخال جميع البيانات");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (gridView2.GetSelectedRows().Length > 0)
                {
                    GridView view = gridView2 as GridView;
                    int rowHandle = gridView2.GetRowHandle(gridView2.GetSelectedRows()[0]);
                    //gridView2.DeleteRow(rowHandle);
                    view.DeleteSelectedRows();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }
        
        private void txtPermissionNum_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtPermissionNum.Text != "" && comStore.SelectedValue != null)
                {
                    dbconnection.Close();

                    search();
                    
                    txtCode.Text = "";
                    txtBalat.Text = "0";
                    txtCarton.Text = "0";
                    txtTotalMeter.Text = "0";
                    txtItemReason.Text = "";
                }
                else
                {
                    gridControl1.DataSource = null;

                    for (int i = 0; i < gridView2.RowCount; i++)
                    {
                        int rowHandle = gridView2.GetRowHandle(i);
                        gridView2.DeleteRow(rowHandle);
                    }
                    //gridControl2.DataSource = null;
                    txtCode.Text = "";
                    txtBalat.Text = "0";
                    txtCarton.Text = "0";
                    txtTotalMeter.Text = "0";
                    txtItemReason.Text = "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
            dbconnection3.Close();
        }

        private void txtCarton_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (row1 != null && !flagCarton)
                {
                    int NoCartons = 0;
                    double totalMeter = 0;
                    double carton = double.Parse(row1["الكرتنة"].ToString());
                    if (carton > 0)
                    {
                        if (int.TryParse(txtCarton.Text, out NoCartons))
                        { }
                        if (double.TryParse(txtTotalMeter.Text, out totalMeter))
                        { }

                        double total = carton * NoCartons;
                        flag = true;
                        txtTotalMeter.Text = (total).ToString();
                        flag = false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            dbconnection.Close();
        }

        private void txtTotalMeter_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (row1 != null && !flag)
                {
                    double totalMeter = 0;
                    if (double.TryParse(txtTotalMeter.Text, out totalMeter))
                    {
                        double carton = double.Parse(row1["الكرتنة"].ToString());
                        if (carton > 0)
                        {
                            flagCarton = true;
                            double total = totalMeter / carton;
                            txtCarton.Text = Convert.ToInt32(total).ToString();
                            flagCarton = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            dbconnection.Close();
        }

        private void gridView1_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            try
            {
                row1 = gridView1.GetDataRow(gridView1.GetRowHandle(e.RowHandle));
                string v = row1["الكود"].ToString();
                txtCode.Text = v;
                
                txtTotalMeter.Text = "0";
                txtCarton.Text = "0";
                txtBalat.Text = "0";
                txtItemReason.Text = "";
                
                double carton = double.Parse(row1["الكرتنة"].ToString());
                if (carton == 0)
                {
                    txtCarton.ReadOnly = true;
                    txtBalat.ReadOnly = true;
                }
                else
                {
                    txtCarton.ReadOnly = false;
                    txtBalat.ReadOnly = false;
                    if (row1["عدد الكراتين"].ToString() != "")
                    {
                        txtCarton.Text = row1["عدد الكراتين"].ToString();
                    }
                    if (row1["عدد البلتات"].ToString() != "")
                    {
                        txtBalat.Text = row1["عدد البلتات"].ToString();
                    }
                }
                if (row1["متر/قطعة"].ToString() != "")
                {
                    txtTotalMeter.Text = row1["متر/قطعة"].ToString();
                }
            }
            catch
            {
                //MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (gridView2.RowCount > 0 && ((comStore.SelectedValue != null && txtPermissionNum.Text != "") || (comStoreFilter.SelectedValue != null && comSupplier.SelectedValue != null)))
                {
                    #region with permission
                    if (radioButtonReturnPermission.Checked)
                    {
                        int permNum = 0;
                        if (int.TryParse(txtPermissionNum.Text, out permNum))
                        { }
                        else
                        {
                            MessageBox.Show("برجاء التاكد من ادخال البيانات بطريقة صحيحة");
                            return;
                        }
                        dbconnection.Open();
                        dbconnection2.Open();
                        int storageReturnID = 0;
                        int storageImportPermissionID = 0;
                        ReturnedPermissionNumber = 1;

                        string q = "select StorageImportPermission_ID from storage_import_permission where Store_ID=" + comStore.SelectedValue.ToString() + " and Import_Permission_Number=" + permNum;
                        MySqlCommand com2 = new MySqlCommand(q, dbconnection);
                        if (com2.ExecuteScalar() != null)
                        {
                            storageImportPermissionID = int.Parse(com2.ExecuteScalar().ToString());
                        }
                        else
                        {
                            MessageBox.Show("هذا الاذن غير موجود");
                            dbconnection.Close();
                            dbconnection2.Close();
                            return;
                        }
                        
                        string qq = "select Returned_Permission_Number from import_storage_return where Store_ID=" + comStore.SelectedValue.ToString() + " ORDER BY ImportStorageReturn_ID DESC LIMIT 1";
                        MySqlCommand com3 = new MySqlCommand(qq, dbconnection);
                        if (com3.ExecuteScalar() != null)
                        {
                            int r = int.Parse(com3.ExecuteScalar().ToString());
                            ReturnedPermissionNumber = r + 1;
                        }

                        string query = "insert into import_storage_return (Store_ID,Returned_Permission_Number,Retrieval_Date,Import_Permission_Number,StorageImportPermission_ID,Reason,Employee_ID) values (@Store_ID,@Returned_Permission_Number,@Retrieval_Date,@Import_Permission_Number,@StorageImportPermission_ID,@Reason,@Employee_ID)";
                        MySqlCommand com = new MySqlCommand(query, dbconnection);
                        com.Parameters.Add("@Store_ID", MySqlDbType.Int16);
                        com.Parameters["@Store_ID"].Value = comStore.SelectedValue.ToString();
                        com.Parameters.Add("@Returned_Permission_Number", MySqlDbType.Int16);
                        com.Parameters["@Returned_Permission_Number"].Value = ReturnedPermissionNumber;
                        com.Parameters.Add("@Retrieval_Date", MySqlDbType.DateTime, 0);
                        com.Parameters["@Retrieval_Date"].Value = DateTime.Now;
                        com.Parameters.Add("@Import_Permission_Number", MySqlDbType.Int16);
                        com.Parameters["@Import_Permission_Number"].Value = permNum;
                        com.Parameters.Add("@StorageImportPermission_ID", MySqlDbType.Int16);
                        com.Parameters["@StorageImportPermission_ID"].Value = storageImportPermissionID;
                        com.Parameters.Add("@Reason", MySqlDbType.VarChar);
                        com.Parameters["@Reason"].Value = txtReason.Text;
                        com.Parameters.Add("@Employee_ID", MySqlDbType.Int16);
                        com.Parameters["@Employee_ID"].Value = UserControl.EmpID;
                        com.ExecuteNonQuery();

                        query = "select ImportStorageReturn_ID from import_storage_return order by ImportStorageReturn_ID desc limit 1";
                        com = new MySqlCommand(query, dbconnection);
                        storageReturnID = Convert.ToInt32(com.ExecuteScalar().ToString());

                        UserControl.ItemRecord("import_storage_return", "اضافة", storageReturnID, DateTime.Now, "", dbconnection);

                        for (int i = 0; i < gridView2.RowCount; i++)
                        {
                            DataRow row2 = gridView2.GetDataRow(gridView2.GetRowHandle(i));

                            query = "SELECT import_storage_return_supplier.ImportStorageReturnSupplier_ID FROM import_storage_return_supplier where ImportStorageReturn_ID=" + storageReturnID + " and Supplier_ID =" + row2["Supplier_ID"].ToString() + " and Supplier_Permission_Number=" + row2["Supplier_Permission_Number"].ToString();
                            com = new MySqlCommand(query, dbconnection);
                            if (com.ExecuteScalar() != null)
                            {
                                storageReturnSupplierId = Convert.ToInt32(com.ExecuteScalar().ToString());
                            }
                            else
                            {
                                query = "insert into import_storage_return_supplier (Supplier_ID,Supplier_Permission_Number,ImportStorageReturn_ID) values (@Supplier_ID,@Supplier_Permission_Number,@ImportStorageReturn_ID)";
                                com = new MySqlCommand(query, dbconnection);
                                com.Parameters.Add("@Supplier_ID", MySqlDbType.Int16);
                                com.Parameters["@Supplier_ID"].Value = row2["Supplier_ID"].ToString();
                                com.Parameters.Add("@Supplier_Permission_Number", MySqlDbType.Int16);
                                com.Parameters["@Supplier_Permission_Number"].Value = row2["Supplier_Permission_Number"].ToString();
                                com.Parameters.Add("@ImportStorageReturn_ID", MySqlDbType.Int16);
                                com.Parameters["@ImportStorageReturn_ID"].Value = storageReturnID;
                                com.ExecuteNonQuery();

                                query = "select ImportStorageReturnSupplier_ID from import_storage_return_supplier order by ImportStorageReturnSupplier_ID desc limit 1";
                                com = new MySqlCommand(query, dbconnection);
                                storageReturnSupplierId = Convert.ToInt32(com.ExecuteScalar().ToString());
                            }

                            query = "insert into import_storage_return_details (Store_ID,Date,Data_ID,Balatat,Carton_Balata,Total_Meters,Reason,ImportStorageReturnSupplier_ID) values (@Store_ID,@Date,@Data_ID,@Balatat,@Carton_Balata,@Total_Meters,@Reason,@ImportStorageReturnSupplier_ID)";
                            com = new MySqlCommand(query, dbconnection);
                            com.Parameters.Add("@Store_ID", MySqlDbType.Int16);
                            com.Parameters["@Store_ID"].Value = comStore.SelectedValue.ToString();
                            //com.Parameters.Add("@Store_Place_ID", MySqlDbType.Int16);
                            //com.Parameters["@Store_Place_ID"].Value = row2["Store_Place_ID"].ToString();
                            com.Parameters.Add("@Date", MySqlDbType.DateTime, 0);
                            com.Parameters["@Date"].Value = DateTime.Now;
                            if (Convert.ToDouble(row2["Carton"].ToString()) > 0)
                            {
                                com.Parameters.Add("@Balatat", MySqlDbType.Int16);
                                com.Parameters["@Balatat"].Value = row2["NumOfBalate"].ToString();
                                com.Parameters.Add("@Carton_Balata", MySqlDbType.Int16);
                                com.Parameters["@Carton_Balata"].Value = row2["NumOfCarton"].ToString();
                            }
                            else
                            {
                                com.Parameters.Add("@Balatat", MySqlDbType.Int16);
                                com.Parameters["@Balatat"].Value = null;
                                com.Parameters.Add("@Carton_Balata", MySqlDbType.Int16);
                                com.Parameters["@Carton_Balata"].Value = null;
                            }
                            com.Parameters.Add("@Data_ID", MySqlDbType.Int16);
                            com.Parameters["@Data_ID"].Value = row2[0].ToString();
                            com.Parameters.Add("@Total_Meters", MySqlDbType.Decimal);
                            com.Parameters["@Total_Meters"].Value = row2["TotalQuantity"].ToString();
                            com.Parameters.Add("@Reason", MySqlDbType.VarChar);
                            com.Parameters["@Reason"].Value = row2["ReturnItemReason"].ToString();
                            com.Parameters.Add("@ImportStorageReturnSupplier_ID", MySqlDbType.Int16);
                            com.Parameters["@ImportStorageReturnSupplier_ID"].Value = storageReturnSupplierId;
                            com.ExecuteNonQuery();

                            query = "select Total_Meters from storage where Data_ID=" + row2["Data_ID"].ToString() + " and Store_ID=" + comStore.SelectedValue.ToString() /*+ " and Store_Place_ID=" + row2["Store_Place_ID"].ToString()*/;
                            com = new MySqlCommand(query, dbconnection);
                            if (com.ExecuteScalar() != null)
                            {
                                double totalMeter = Convert.ToDouble(com.ExecuteScalar().ToString());

                                query = "update storage set Total_Meters=" + (totalMeter - Convert.ToDouble(row2["TotalQuantity"].ToString())) + " where Data_ID=" + row2["Data_ID"].ToString() + " and Store_ID=" + comStore.SelectedValue.ToString() /*+ " and Store_Place_ID=" + row2["Store_Place_ID"].ToString()*/;
                                com = new MySqlCommand(query, dbconnection);
                                com.ExecuteNonQuery();

                                UserControl.ItemRecord("storage", "تعديل", Convert.ToInt16(row2["Data_ID"].ToString()), DateTime.Now, "من مرتجع اذن مخزن", dbconnection);
                            }
                        }
                        dbconnection.Close();

                        string suppliers_Name = "";
                        dbconnection.Open();
                        string query2 = "select Store_Name from store where Store_ID=" + comStore.SelectedValue.ToString();
                        MySqlCommand com4 = new MySqlCommand(query2, dbconnection);
                        string storeName = com4.ExecuteScalar().ToString();
                        dbconnection.Close();
                        
                        List<StorageReturn_Items> bi = new List<StorageReturn_Items>();
                        for (int i = 0; i < gridView2.RowCount; i++)
                        {
                            double carton = 0;
                            double balate = 0;
                            double quantity = 0;
                            int rowHand = gridView2.GetRowHandle(i);
                            bool flagTest = false;
                            if (gridView2.GetRowCellDisplayText(rowHand, gridView2.Columns["NumOfBalate"]) != "")
                            {
                                balate = Convert.ToDouble(gridView2.GetRowCellDisplayText(rowHand, gridView2.Columns["NumOfBalate"]));
                            }
                            if (gridView2.GetRowCellDisplayText(rowHand, gridView2.Columns["NumOfCarton"]) != "")
                            {
                                carton = Convert.ToDouble(gridView2.GetRowCellDisplayText(rowHand, gridView2.Columns["NumOfCarton"]));
                            }
                            if (gridView2.GetRowCellDisplayText(rowHand, gridView2.Columns["TotalQuantity"]) != "")
                            {
                                quantity = Convert.ToDouble(gridView2.GetRowCellDisplayText(rowHand, gridView2.Columns["TotalQuantity"]));
                            }

                            StorageReturn_Items item = new StorageReturn_Items() { Code = gridView2.GetRowCellDisplayText(rowHand, gridView2.Columns["Code"]), Product_Type = gridView2.GetRowCellDisplayText(rowHand, gridView2.Columns["ItemType"]), Product_Name = gridView2.GetRowCellDisplayText(rowHand, gridView2.Columns["ItemName"]), Balatat = balate, Carton_Balata = carton, Total_Meters = quantity, Supplier_Permission_Number = Convert.ToInt32(gridView2.GetRowCellDisplayText(rowHand, gridView2.Columns["Supplier_Permission_Number"])), Reason = gridView2.GetRowCellDisplayText(rowHand, gridView2.Columns["ReturnItemReason"]) };
                            bi.Add(item);

                            if (i == 0)
                            {
                                suppliers_Name += gridView2.GetRowCellDisplayText(rowHand, gridView2.Columns["Supplier_Name"]);
                            }

                            if (i < (gridView2.RowCount - 1))
                            {
                                for (int j = 0; j < gridView2.RowCount; j++)
                                {
                                    int rowHand2 = gridView2.GetRowHandle(j);
                                    if (i != j)
                                    {
                                        if (gridView2.GetRowCellDisplayText(rowHand, gridView2.Columns["Supplier_Name"]) == gridView2.GetRowCellDisplayText(rowHand2, gridView2.Columns["Supplier_Name"]))
                                        {
                                            flagTest = true;
                                        }
                                    }
                                }
                                if (!flagTest)
                                {
                                    suppliers_Name += "," + gridView2.GetRowCellDisplayText(rowHand, gridView2.Columns["Supplier_Name"]);
                                }
                            }
                        }

                        bool flagTest2 = false;
                        for (int j = 0; j < gridView2.RowCount - 1; j++)
                        {
                            int rowHand2 = gridView2.GetRowHandle(j);
                            if (gridView2.GetRowCellDisplayText(gridView2.GetRowHandle(gridView2.RowCount - 1), gridView2.Columns["Supplier_Name"]) == gridView2.GetRowCellDisplayText(rowHand2, gridView2.Columns["Supplier_Name"]))
                            {
                                flagTest2 = true;
                            }
                        }
                        if (!flagTest2 && gridView2.RowCount > 1)
                        {
                            suppliers_Name += "," + gridView2.GetRowCellDisplayText(gridView2.GetRowHandle(gridView2.RowCount - 1), gridView2.Columns["Supplier_Name"]);
                        }

                        Report_StorageReturn f = new Report_StorageReturn();
                        f.PrintInvoice(storeName, txtPermissionNum.Text, suppliers_Name, ReturnedPermissionNumber, txtReason.Text, bi);
                        f.ShowDialog();
                        
                        search();
                        txtCode.Text = "";
                        txtTotalMeter.Text = "0";
                        txtCarton.Text = "0";
                        txtBalat.Text = "0";
                        txtReason.Text = "";
                        txtItemReason.Text = "";
                    }
                    #endregion

                    #region with out permission
                    else if (radioButtonWithOutReturnPermission.Checked)
                    {
                        dbconnection.Open();
                        dbconnection2.Open();
                        int storageReturnID = 0;
                        ReturnedPermissionNumber = 1;

                        string qq = "select Returned_Permission_Number from import_storage_return where Store_ID=" + comStoreFilter.SelectedValue.ToString() + " ORDER BY ImportStorageReturn_ID DESC LIMIT 1";
                        MySqlCommand com3 = new MySqlCommand(qq, dbconnection);
                        if (com3.ExecuteScalar() != null)
                        {
                            int r = int.Parse(com3.ExecuteScalar().ToString());
                            ReturnedPermissionNumber = r + 1;
                        }
                        
                        string query = "insert into import_storage_return (Store_ID,Returned_Permission_Number,Retrieval_Date,Import_Permission_Number,StorageImportPermission_ID,Reason,Employee_ID) values (@Store_ID,@Returned_Permission_Number,@Retrieval_Date,@Import_Permission_Number,@StorageImportPermission_ID,@Reason,@Employee_ID)";
                        MySqlCommand com = new MySqlCommand(query, dbconnection);
                        com.Parameters.Add("@Store_ID", MySqlDbType.Int16);
                        com.Parameters["@Store_ID"].Value = comStoreFilter.SelectedValue.ToString();
                        com.Parameters.Add("@Returned_Permission_Number", MySqlDbType.Int16);
                        com.Parameters["@Returned_Permission_Number"].Value = ReturnedPermissionNumber;
                        com.Parameters.Add("@Retrieval_Date", MySqlDbType.DateTime, 0);
                        com.Parameters["@Retrieval_Date"].Value = DateTime.Now;
                        com.Parameters.Add("@Import_Permission_Number", MySqlDbType.Int16);
                        com.Parameters["@Import_Permission_Number"].Value = null;
                        com.Parameters.Add("@StorageImportPermission_ID", MySqlDbType.Int16);
                        com.Parameters["@StorageImportPermission_ID"].Value = null;
                        com.Parameters.Add("@Reason", MySqlDbType.VarChar);
                        com.Parameters["@Reason"].Value = txtReason.Text;
                        com.Parameters.Add("@Employee_ID", MySqlDbType.Int16);
                        com.Parameters["@Employee_ID"].Value = UserControl.EmpID;
                        com.ExecuteNonQuery();

                        query = "select ImportStorageReturn_ID from import_storage_return order by ImportStorageReturn_ID desc limit 1";
                        com = new MySqlCommand(query, dbconnection);
                        storageReturnID = Convert.ToInt32(com.ExecuteScalar().ToString());

                        UserControl.ItemRecord("import_storage_return", "اضافة", storageReturnID, DateTime.Now, "", dbconnection);

                        query = "SELECT import_storage_return_supplier.ImportStorageReturnSupplier_ID FROM import_storage_return_supplier where ImportStorageReturn_ID=" + storageReturnID + " and Supplier_ID =" + comSupplier.SelectedValue.ToString() + " and Supplier_Permission_Number is null";
                        com = new MySqlCommand(query, dbconnection);
                        if (com.ExecuteScalar() != null)
                        {
                            storageReturnSupplierId = Convert.ToInt32(com.ExecuteScalar().ToString());
                        }
                        else
                        {
                            query = "insert into import_storage_return_supplier (Supplier_ID,Supplier_Permission_Number,ImportStorageReturn_ID) values (@Supplier_ID,@Supplier_Permission_Number,@ImportStorageReturn_ID)";
                            com = new MySqlCommand(query, dbconnection);
                            com.Parameters.Add("@Supplier_ID", MySqlDbType.Int16);
                            com.Parameters["@Supplier_ID"].Value = comSupplier.SelectedValue.ToString();
                            com.Parameters.Add("@Supplier_Permission_Number", MySqlDbType.Int16);
                            com.Parameters["@Supplier_Permission_Number"].Value = null;
                            com.Parameters.Add("@ImportStorageReturn_ID", MySqlDbType.Int16);
                            com.Parameters["@ImportStorageReturn_ID"].Value = storageReturnID;
                            com.ExecuteNonQuery();

                            query = "select ImportStorageReturnSupplier_ID from import_storage_return_supplier order by ImportStorageReturnSupplier_ID desc limit 1";
                            com = new MySqlCommand(query, dbconnection);
                            storageReturnSupplierId = Convert.ToInt32(com.ExecuteScalar().ToString());
                        }

                        for (int i = 0; i < gridView2.RowCount; i++)
                        {
                            DataRow row2 = gridView2.GetDataRow(gridView2.GetRowHandle(i));

                            query = "insert into import_storage_return_details (Store_ID,Date,Data_ID,Balatat,Carton_Balata,Total_Meters,Reason,ImportStorageReturnSupplier_ID) values (@Store_ID,@Date,@Data_ID,@Balatat,@Carton_Balata,@Total_Meters,@Reason,@ImportStorageReturnSupplier_ID)";
                            com = new MySqlCommand(query, dbconnection);
                            com.Parameters.Add("@Store_ID", MySqlDbType.Int16);
                            com.Parameters["@Store_ID"].Value = comStoreFilter.SelectedValue.ToString();
                            //com.Parameters.Add("@Store_Place_ID", MySqlDbType.Int16);
                            //com.Parameters["@Store_Place_ID"].Value = null;
                            com.Parameters.Add("@Date", MySqlDbType.DateTime, 0);
                            com.Parameters["@Date"].Value = DateTime.Now;
                            if (Convert.ToDouble(row2["Carton"].ToString()) > 0)
                            {
                                com.Parameters.Add("@Balatat", MySqlDbType.Int16);
                                com.Parameters["@Balatat"].Value = row2["NumOfBalate"].ToString();
                                com.Parameters.Add("@Carton_Balata", MySqlDbType.Int16);
                                com.Parameters["@Carton_Balata"].Value = row2["NumOfCarton"].ToString();
                            }
                            else
                            {
                                com.Parameters.Add("@Balatat", MySqlDbType.Int16);
                                com.Parameters["@Balatat"].Value = null;
                                com.Parameters.Add("@Carton_Balata", MySqlDbType.Int16);
                                com.Parameters["@Carton_Balata"].Value = null;
                            }
                            com.Parameters.Add("@Data_ID", MySqlDbType.Int16);
                            com.Parameters["@Data_ID"].Value = row2[0].ToString();
                            com.Parameters.Add("@Total_Meters", MySqlDbType.Decimal);
                            com.Parameters["@Total_Meters"].Value = row2["TotalQuantity"].ToString();
                            com.Parameters.Add("@Reason", MySqlDbType.VarChar);
                            com.Parameters["@Reason"].Value = row2["ReturnItemReason"].ToString();
                            com.Parameters.Add("@ImportStorageReturnSupplier_ID", MySqlDbType.Int16);
                            com.Parameters["@ImportStorageReturnSupplier_ID"].Value = storageReturnSupplierId;
                            com.ExecuteNonQuery();

                            double totalQuant = Convert.ToDouble(row2["TotalQuantity"].ToString());

                            dbconnection4.Open();
                            //Store_Place_ID,
                            query = "select Total_Meters from storage where Data_ID=" + row2["Data_ID"].ToString() + " and Store_ID=" + comStoreFilter.SelectedValue.ToString();
                            com = new MySqlCommand(query, dbconnection4);
                            MySqlDataReader dr = com.ExecuteReader();
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    if (totalQuant > 0)
                                    {
                                        double totalMeter = Convert.ToDouble(dr["Total_Meters"]);

                                        if (totalMeter > 0)
                                        {
                                            if ((totalMeter - totalQuant) >= 0)
                                            {
                                                query = "update storage set Total_Meters=" + (totalMeter - totalQuant) + " where Data_ID=" + row2["Data_ID"].ToString() + " and Store_ID=" + comStoreFilter.SelectedValue.ToString() /*+ " and Store_Place_ID=" + dr["Store_Place_ID"].ToString()*/;
                                                com = new MySqlCommand(query, dbconnection);
                                                com.ExecuteNonQuery();

                                                UserControl.ItemRecord("storage", "تعديل", Convert.ToInt16(row2["Data_ID"].ToString()), DateTime.Now, "من مرتجع اذن مخزن", dbconnection);

                                                totalQuant = 0;
                                            }
                                            else if ((totalMeter - totalQuant) < 0)
                                            {
                                                query = "update storage set Total_Meters=" + 0 + " where Data_ID=" + row2["Data_ID"].ToString() + " and Store_ID=" + comStoreFilter.SelectedValue.ToString() /*+ " and Store_Place_ID=" + dr["Store_Place_ID"].ToString()*/;
                                                com = new MySqlCommand(query, dbconnection);
                                                com.ExecuteNonQuery();

                                                UserControl.ItemRecord("storage", "تعديل", Convert.ToInt16(row2["Data_ID"].ToString()), DateTime.Now, "من مرتجع اذن مخزن", dbconnection);

                                                totalQuant = totalQuant - totalMeter;
                                            }
                                        }
                                    }
                                }
                                dr.Close();
                            }
                            dbconnection4.Close();
                        }
                        dbconnection.Close();
                        
                        dbconnection.Open();
                        string query2 = "select Store_Name from store where Store_ID=" + comStoreFilter.SelectedValue.ToString();
                        MySqlCommand com4 = new MySqlCommand(query2, dbconnection);
                        string storeName = com4.ExecuteScalar().ToString();
                        dbconnection.Close();

                        double carton = 0;
                        double balate = 0;
                        double quantity = 0;

                        List<StorageReturn_Items> bi = new List<StorageReturn_Items>();
                        for (int i = 0; i < gridView2.RowCount; i++)
                        {
                            int rowHand = gridView2.GetRowHandle(i);
                            if (gridView2.GetRowCellDisplayText(rowHand, gridView2.Columns["NumOfBalate"]) != "")
                            {
                                balate = Convert.ToDouble(gridView2.GetRowCellDisplayText(rowHand, gridView2.Columns["NumOfBalate"]));
                            }
                            if (gridView2.GetRowCellDisplayText(rowHand, gridView2.Columns["NumOfCarton"]) != "")
                            {
                                carton = Convert.ToDouble(gridView2.GetRowCellDisplayText(rowHand, gridView2.Columns["NumOfCarton"]));
                            }
                            if (gridView2.GetRowCellDisplayText(rowHand, gridView2.Columns["TotalQuantity"]) != "")
                            {
                                quantity = Convert.ToDouble(gridView2.GetRowCellDisplayText(rowHand, gridView2.Columns["TotalQuantity"]));
                            }

                            StorageReturn_Items item = new StorageReturn_Items() { Code = gridView2.GetRowCellDisplayText(rowHand, gridView2.Columns["Code"]), Product_Type = gridView2.GetRowCellDisplayText(rowHand, gridView2.Columns["ItemType"]), Product_Name = gridView2.GetRowCellDisplayText(rowHand, gridView2.Columns["ItemName"]), Balatat = balate, Carton_Balata = carton, Total_Meters = quantity, Supplier_Permission_Number = Convert.ToInt32(gridView2.GetRowCellDisplayText(rowHand, gridView2.Columns["Supplier_Permission_Number"])), Reason = gridView2.GetRowCellDisplayText(rowHand, gridView2.Columns["ReturnItemReason"]) };
                            bi.Add(item);
                        }

                        Report_StorageReturn f = new Report_StorageReturn();
                        f.PrintInvoice(storeName, txtPermissionNum.Text, comSupplier.Text, ReturnedPermissionNumber, txtReason.Text, bi);
                        f.ShowDialog();

                        displayData();

                        txtCode.Text = "";
                        txtTotalMeter.Text = "0";
                        txtCarton.Text = "0";
                        txtBalat.Text = "0";
                        txtReason.Text = "";
                        txtItemReason.Text = "";
                        for (int i = 0; i < gridView2.RowCount; i++)
                        {
                            int rowHandle = gridView2.GetRowHandle(i);
                            gridView2.DeleteRow(rowHandle);
                        }
                    }
                    #endregion
                }
                else
                {
                    MessageBox.Show("يجب ادخال جميع البيانات");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
            dbconnection2.Close();
            dbconnection3.Close();
            dbconnection4.Close();
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            /*try
            {
                if (comSupplier.SelectedValue != null && txtPermissionNum.Text != "" && gridView2.RowCount > 0)
                {
                    dbconnection.Open();
                    string query = "select Store_Name from store where Store_ID=" + comStore.SelectedValue.ToString();
                    MySqlCommand com = new MySqlCommand(query, dbconnection);
                    string storeName = com.ExecuteScalar().ToString();
                    dbconnection.Close();

                    double carton = 0;
                    double balate = 0;
                    double quantity = 0;

                    List<StorageReturn_Items> bi = new List<StorageReturn_Items>();
                    for (int i = 0; i < gridView2.RowCount; i++)
                    {
                        if (gridView2.GetRowCellDisplayText(i, gridView2.Columns["عدد البلتات"]) != "")
                        {
                            balate = Convert.ToDouble(gridView2.GetRowCellDisplayText(i, gridView2.Columns["عدد البلتات"]));
                        }
                        if (gridView2.GetRowCellDisplayText(i, gridView2.Columns["عدد الكراتين"]) != "")
                        {
                            carton = Convert.ToDouble(gridView2.GetRowCellDisplayText(i, gridView2.Columns["عدد الكراتين"]));
                        }
                        if (gridView2.GetRowCellDisplayText(i, gridView2.Columns["متر/قطعة"]) != "")
                        {
                            quantity = Convert.ToDouble(gridView2.GetRowCellDisplayText(i, gridView2.Columns["متر/قطعة"]));
                        }

                        StorageReturn_Items item = new StorageReturn_Items() { Code = gridView2.GetRowCellDisplayText(i, gridView2.Columns["الكود"]), Product_Name = gridView2.GetRowCellDisplayText(i, gridView2.Columns["الاسم"]), Balatat = balate, Carton_Balata = carton, Total_Meters = quantity, Supplier_Permission_Number = Convert.ToInt32(gridView2.GetRowCellDisplayText(i, gridView2.Columns["اذن استلام"])), Date = Convert.ToDateTime(gridView2.GetRowCellDisplayText(i, gridView2.Columns["التاريخ"])).ToString("yyyy-MM-dd hh:mm:ss"), Reason = gridView2.GetRowCellDisplayText(i, gridView2.Columns["السبب"]) };
                        bi.Add(item);
                    }

                    Report_StorageReturn f = new Report_StorageReturn();
                    f.PrintInvoice(storeName, txtPermissionNum.Text, comSupplier.Text, ReturnedPermissionNumber, bi);
                    f.ShowDialog();
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
            dbconnection.Close();*/
        }

        public void search()
        {
            gridControl1.DataSource = null;
            gridView1.Columns.Clear();
            //,supplier_permission_details.Store_Place_ID
            // INNER JOIN store_places ON store_places.Store_Place_ID = supplier_permission_details.Store_Place_ID
            //,store_places.Store_Place_Code as 'مكان التخزين'
            string qq = "select data.Data_ID,data.Code as 'الكود',type.Type_Name as 'النوع',concat(product.Product_Name,' - ',type.Type_Name,' - ',factory.Factory_Name,' - ',groupo.Group_Name,' ',COALESCE(color.Color_Name,''),' ',COALESCE(size.Size_Value,''),' ',COALESCE(sort.Sort_Value,'')) as 'الاسم',supplier.Supplier_Name as 'المورد',import_supplier_permission.Supplier_Permission_Number as 'اذن استلام',(supplier_permission_details.Balatat-ifnull(import_storage_return_details.Balatat,0)) as 'عدد البلتات',(supplier_permission_details.Carton_Balata-ifnull(import_storage_return_details.Carton_Balata,0)) as 'عدد الكراتين',(supplier_permission_details.Total_Meters-ifnull(import_storage_return_details.Total_Meters,0)) as 'متر/قطعة',DATE_FORMAT(supplier_permission_details.Date, '%d-%m-%Y %T') as 'تاريخ التخزين',supplier_permission_details.Note as 'ملاحظة',data.Carton as 'الكرتنة',sum(storage.Total_Meters) as 'الكمية المتاحة',import_supplier_permission.Supplier_ID,supplier_permission_details.Supplier_Permission_Details_ID,storage_import_permission.StorageImportPermission_ID from supplier_permission_details INNER JOIN data ON supplier_permission_details.Data_ID = data.Data_ID LEFT JOIN storage ON storage.Data_ID = data.Data_ID INNER JOIN import_supplier_permission ON supplier_permission_details.ImportSupplierPermission_ID = import_supplier_permission.ImportSupplierPermission_ID INNER JOIN supplier ON import_supplier_permission.Supplier_ID = supplier.Supplier_ID INNER JOIN storage_import_permission ON storage_import_permission.StorageImportPermission_ID = import_supplier_permission.StorageImportPermission_ID LEFT JOIN import_storage_return ON import_storage_return.StorageImportPermission_ID = storage_import_permission.StorageImportPermission_ID LEFT JOIN import_storage_return_supplier ON import_storage_return_supplier.ImportStorageReturn_ID = import_storage_return.ImportStorageReturn_ID LEFT JOIN import_storage_return_details ON import_storage_return_details.ImportStorageReturnSupplier_ID = import_storage_return_supplier.ImportStorageReturnSupplier_ID and supplier_permission_details.Data_ID = import_storage_return_details.Data_ID INNER JOIN type ON type.Type_ID = data.Type_ID INNER JOIN product ON product.Product_ID = data.Product_ID INNER JOIN factory ON data.Factory_ID = factory.Factory_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID LEFT JOIN color ON color.Color_ID = data.Color_ID LEFT JOIN size ON size.Size_ID = data.Size_ID LEFT JOIN sort ON sort.Sort_ID = data.Sort_ID where storage_import_permission.Import_Permission_Number=0 and supplier_permission_details.Store_ID=0 group by data.Data_ID";
            MySqlDataAdapter da = new MySqlDataAdapter(qq, dbconnection);
            DataTable dt = new DataTable();
            da.Fill(dt);
            gridControl1.DataSource = dt;
            
            dbconnection.Open();
            //,supplier_permission_details.Store_Place_ID
            //and supplier_permission_details.Store_Place_ID=storage.Store_Place_ID
            //INNER JOIN store_places ON store_places.Store_Place_ID = supplier_permission_details.Store_Place_ID
            //,store_places.Store_Place_Code as 'مكان التخزين'
            qq = "select data.Data_ID,data.Code as 'الكود',type.Type_Name as 'النوع',concat(product.Product_Name,' ',COALESCE(color.Color_Name,''),' ',data.Description,' ',groupo.Group_Name,' ',factory.Factory_Name,' ',COALESCE(size.Size_Value,''),' ',COALESCE(sort.Sort_Value,'')) as 'الاسم',supplier.Supplier_Name as 'المورد',import_supplier_permission.Supplier_Permission_Number as 'اذن استلام',supplier_permission_details.Balatat as 'عدد البلتات',supplier_permission_details.Carton_Balata as 'عدد الكراتين',supplier_permission_details.Total_Meters as 'متر/قطعة',DATE_FORMAT(supplier_permission_details.Date, '%d-%m-%Y %T') as 'تاريخ التخزين',supplier_permission_details.Note as 'ملاحظة',data.Carton as 'الكرتنة',sum(storage.Total_Meters) as 'الكمية المتاحة',import_supplier_permission.Supplier_ID,supplier_permission_details.Supplier_Permission_Details_ID,storage_import_permission.StorageImportPermission_ID from supplier_permission_details INNER JOIN data ON supplier_permission_details.Data_ID = data.Data_ID LEFT JOIN storage ON storage.Data_ID = data.Data_ID  INNER JOIN import_supplier_permission ON supplier_permission_details.ImportSupplierPermission_ID = import_supplier_permission.ImportSupplierPermission_ID INNER JOIN supplier ON import_supplier_permission.Supplier_ID = supplier.Supplier_ID INNER JOIN storage_import_permission ON storage_import_permission.StorageImportPermission_ID = import_supplier_permission.StorageImportPermission_ID  INNER JOIN type ON type.Type_ID = data.Type_ID INNER JOIN product ON product.Product_ID = data.Product_ID INNER JOIN factory ON data.Factory_ID = factory.Factory_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID LEFT JOIN color ON color.Color_ID = data.Color_ID LEFT JOIN size ON size.Size_ID = data.Size_ID LEFT JOIN sort ON sort.Sort_ID = data.Sort_ID where storage_import_permission.Import_Permission_Number=" + txtPermissionNum.Text + " and supplier_permission_details.Store_ID=" + comStore.SelectedValue.ToString() + " group by data.Data_ID order by SUBSTR(data.Code,1,16),color.Color_Name,data.Description,data.Sort_ID";
            MySqlCommand comand = new MySqlCommand(qq, dbconnection);
            MySqlDataReader dr = comand.ExecuteReader();
            while (dr.Read())
            {
                int noBalat = 0;
                int noCarton = 0;
                double totalMeter = 0;
                gridView1.AddNewRow();
                int rowHandle = gridView1.GetRowHandle(gridView1.DataRowCount);
                if (gridView1.IsNewItemRow(rowHandle))
                {
                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns[0], dr["Data_ID"].ToString());
                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns["الكود"], dr["الكود"].ToString());
                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns["النوع"], dr["النوع"]);
                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns["الاسم"], dr["الاسم"].ToString());
                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns["المورد"], dr["المورد"].ToString());
                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns["اذن استلام"], dr["اذن استلام"].ToString());
                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns["تاريخ التخزين"], dr["تاريخ التخزين"].ToString());
                    //gridView1.SetRowCellValue(rowHandle, gridView1.Columns["مكان التخزين"], dr["مكان التخزين"].ToString());
                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns["ملاحظة"], dr["ملاحظة"].ToString());
                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns["الكرتنة"], dr["الكرتنة"].ToString());
                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns["الكمية المتاحة"], dr["الكمية المتاحة"].ToString());
                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns["Supplier_ID"], dr["Supplier_ID"].ToString());
                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns["Supplier_Permission_Details_ID"], dr["Supplier_Permission_Details_ID"].ToString());
                    //gridView1.SetRowCellValue(rowHandle, gridView1.Columns["Store_Place_ID"], dr["Store_Place_ID"].ToString());
                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns["StorageImportPermission_ID"], dr["StorageImportPermission_ID"].ToString());
                    if (dr["عدد البلتات"].ToString() != "")
                    {
                        noBalat = Convert.ToInt32(dr["عدد البلتات"].ToString());
                    }
                    if (dr["عدد الكراتين"].ToString() != "")
                    {
                        noCarton = Convert.ToInt32(dr["عدد الكراتين"].ToString());
                    }
                    if (dr["متر/قطعة"].ToString() != "")
                    {
                        totalMeter = Convert.ToDouble(dr["متر/قطعة"].ToString());
                    }

                    dbconnection3.Open();
                    string q = "select import_storage_return_details.Balatat as 'عدد البلتات',import_storage_return_details.Carton_Balata as 'عدد الكراتين',import_storage_return_details.Total_Meters as 'متر/قطعة' from import_storage_return inner JOIN import_storage_return_supplier ON import_storage_return_supplier.ImportStorageReturn_ID = import_storage_return.ImportStorageReturn_ID LEFT JOIN import_storage_return_details ON import_storage_return_details.ImportStorageReturnSupplier_ID = import_storage_return_supplier.ImportStorageReturnSupplier_ID  where import_storage_return.Import_Permission_Number=" + txtPermissionNum.Text + " and import_storage_return.Store_ID=" + comStore.SelectedValue.ToString() + " and import_storage_return_details.Data_ID=" + dr[0].ToString() + " and import_storage_return_supplier.Supplier_Permission_Number=" + dr["اذن استلام"].ToString() + " and import_storage_return_supplier.Supplier_ID=" + dr["Supplier_ID"].ToString();
                    MySqlCommand comand2 = new MySqlCommand(q, dbconnection3);
                    MySqlDataReader dr2 = comand2.ExecuteReader();
                    while (dr2.Read())
                    {
                        if (dr2["عدد البلتات"].ToString() != "")
                        {
                            noBalat -= Convert.ToInt32(dr2["عدد البلتات"].ToString());
                        }
                        if (dr2["عدد الكراتين"].ToString() != "")
                        {
                            noCarton -= Convert.ToInt32(dr2["عدد الكراتين"].ToString());
                        }
                        if (dr2["متر/قطعة"].ToString() != "")
                        {
                            totalMeter -= Convert.ToDouble(dr2["متر/قطعة"].ToString());
                        }
                    }
                    dr2.Close();
                    dbconnection3.Close();
                    if (noBalat > 0)
                    {
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns["عدد البلتات"], noBalat);
                    }
                    if (noCarton > 0)
                    {
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns["عدد الكراتين"], noCarton);
                    }
                    if (totalMeter > 0)
                    {
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns["متر/قطعة"], totalMeter);
                    }
                }
            }
            dr.Close();
            gridView1.Columns["Data_ID"].Visible = false;
            gridView1.Columns["Supplier_ID"].Visible = false;
            //gridView1.Columns["Carton"].Visible = false;
            gridView1.Columns["Supplier_Permission_Details_ID"].Visible = false;
            //gridView1.Columns["Store_Place_ID"].Visible = false;
            gridView1.Columns["StorageImportPermission_ID"].Visible = false;
            if (gridView1.IsLastVisibleRow)
            {
                gridView1.FocusedRowHandle = gridView1.RowCount - 1;
            }

            for (int i = 0; i < gridView2.RowCount; i++)
            {
                int rowHandle = gridView2.GetRowHandle(i);
                gridView2.DeleteRow(rowHandle);
            }
            
            /*qq = "select data.Data_ID,data.Code as 'الكود',concat(product.Product_Name,' - ',type.Type_Name,' - ',factory.Factory_Name,' - ',groupo.Group_Name,' ',COALESCE(color.Color_Name,''),' ',COALESCE(size.Size_Value,''),' ',COALESCE(sort.Sort_Value,'')) as 'الاسم',import_storage_return_supplier.Supplier_Permission_Number as 'اذن استلام',import_storage_return_details.Balatat as 'عدد البلتات',import_storage_return_details.Carton_Balata as 'عدد الكراتين',import_storage_return_details.Total_Meters as 'متر/قطعة',DATE_FORMAT(import_storage_return_details.Date, '%d-%m-%Y %T') as 'التاريخ',store_places.Store_Place_Code as 'المكان',import_storage_return_details.Reason as 'السبب',import_storage_return_supplier.Supplier_ID,import_storage_return_details.ImportStorageReturnDetails_ID,import_storage_return_details.Store_Place_ID from import_storage_return_details INNER JOIN data ON import_storage_return_details.Data_ID = data.Data_ID INNER JOIN import_storage_return_supplier ON import_storage_return_details.ImportStorageReturnSupplier_ID = import_storage_return_supplier.ImportStorageReturnSupplier_ID INNER JOIN import_storage_return ON import_storage_return.ImportStorageReturn_ID = import_storage_return_supplier.ImportStorageReturn_ID INNER JOIN store_places ON store_places.Store_Place_ID = import_storage_return_details.Store_Place_ID INNER JOIN type ON type.Type_ID = data.Type_ID INNER JOIN product ON product.Product_ID = data.Product_ID INNER JOIN factory ON data.Factory_ID = factory.Factory_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID LEFT JOIN color ON color.Color_ID = data.Color_ID LEFT JOIN size ON size.Size_ID = data.Size_ID LEFT JOIN sort ON sort.Sort_ID = data.Sort_ID where import_storage_return.Import_Permission_Number=" + txtPermissionNum.Text + " and import_storage_return_details.Store_ID=" + comStore.SelectedValue.ToString();
            da = new MySqlDataAdapter(qq, dbconnection);
            dt = new DataTable();
            da.Fill(dt);
            gridControl2.DataSource = dt;
            gridView2.Columns["Data_ID"].Visible = false;
            gridView2.Columns["Supplier_ID"].Visible = false;
            gridView2.Columns["ImportStorageReturnDetails_ID"].Visible = false;
            gridView2.Columns["Store_Place_ID"].Visible = false;

            if (gridView2.IsLastVisibleRow)
            {
                gridView2.FocusedRowHandle = gridView2.RowCount - 1;
            }*/
            dbconnection.Close();
        }

        public void displayData()
        {
            gridControl1.DataSource = null;
            gridView1.Columns.Clear();

            //for (int i = 0; i < gridView2.RowCount; i++)
            //{
            //    int rowHandle = gridView2.GetRowHandle(i);
            //    gridView2.DeleteRow(rowHandle);
            //}
            
            //gridControl2.DataSource = null;
            //gridView2.Columns.Clear();

            if (comStoreFilter.SelectedValue != null)
            {
                string q1, q2, q3, q4, q5;
                if (txtType.Text == "")
                {
                    q1 = "select Type_ID from data";
                }
                else
                {
                    q1 = txtType.Text;
                }
                if (txtFactory.Text == "")
                {
                    q2 = "select Factory_ID from data";
                }
                else
                {
                    q2 = txtFactory.Text;
                }
                if (txtProduct.Text == "")
                {
                    q3 = "select Product_ID from data";
                }
                else
                {
                    q3 = txtProduct.Text;
                }
                if (txtGroup.Text == "")
                {
                    q4 = "select Group_ID from data";
                }
                else
                {
                    q4 = txtGroup.Text;
                }
                if (txtProduct.Text == "")
                {
                    q5 = "";
                }
                else
                {
                    q5 = "and  data.Size_ID=" + txtProduct.Text;
                }
                string query = "";
                string itemName = "type.Type_Name as 'النوع',concat(product.Product_Name,' ',COALESCE(color.Color_Name,''),' ',data.Description,' ',groupo.Group_Name,' ',factory.Factory_Name,' ',COALESCE(size.Size_Value,''),' ',COALESCE(sort.Sort_Value,'')) as 'الاسم'";
                string DataTableRelations = "INNER JOIN type ON type.Type_ID = data.Type_ID INNER JOIN product ON product.Product_ID = data.Product_ID INNER JOIN factory ON data.Factory_ID = factory.Factory_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID LEFT outer JOIN color ON data.Color_ID = color.Color_ID LEFT outer  JOIN size ON data.Size_ID = size.Size_ID LEFT outer  JOIN sort ON data.Sort_ID = sort.Sort_ID";
                if (txtType.Text == "1" || txtType.Text == "2" || txtType.Text == "9")
                {
                    query = "select data.Data_ID, Code as 'الكود'," + itemName + ",data.Carton as 'الكرتنة',sum(storage.Total_Meters) as 'الكمية المتاحة' from  data LEFT JOIN storage ON storage.Data_ID = data.Data_ID " + DataTableRelations + " where data.Type_ID IN(" + q1 + ") and data.Factory_ID IN(" + q2 + ") " + q5 + " and data.Group_ID IN (" + q4 + ") and storage.Store_ID=" + comStoreFilter.SelectedValue.ToString() + " group by data.Data_ID order by SUBSTR(data.Code,1,16),color.Color_Name,data.Description,data.Sort_ID";
                }
                else
                {
                    query = "select data.Data_ID, Code as 'الكود'," + itemName + ",data.Carton as 'الكرتنة',sum(storage.Total_Meters) as 'الكمية المتاحة' from  data LEFT JOIN storage ON storage.Data_ID = data.Data_ID " + DataTableRelations + " where data.Type_ID IN(" + q1 + ") and data.Factory_ID IN(" + q2 + ") and  data.Product_ID IN(" + q3 + ") and data.Group_ID IN (" + q4 + ") and storage.Store_ID=" + comStoreFilter.SelectedValue.ToString() + " group by data.Data_ID order by SUBSTR(data.Code,1,16),color.Color_Name,data.Description,data.Sort_ID";
                }

                MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                DataTable dt = new DataTable();
                da.Fill(dt);
                gridControl1.DataSource = dt;
                gridView1.Columns[0].Visible = false;
                gridView1.Columns[1].Width = 200;
            }
            else
            {
                MessageBox.Show("تاكد من اختيار المخزن");
            }
        }

        bool IsAdded()
        {
            for (int i = 0; i < gridView2.RowCount; i++)
            {
                DataRow row3 = gridView2.GetDataRow(gridView2.GetRowHandle(i));
                if ((row1["Data_ID"].ToString() == row3["Data_ID"].ToString()) && (row1["Supplier_Permission_Details_ID"].ToString() == row3["Supplier_Permission_Details_ID"].ToString()))
                    return true;
            }
            return false;
        }

        bool IsItemAdded()
        {
            for (int i = 0; i < gridView2.RowCount; i++)
            {
                DataRow row3 = gridView2.GetDataRow(gridView2.GetRowHandle(i));
                if (row1["Data_ID"].ToString() == row3["Data_ID"].ToString())
                    return true;
            }
            return false;
        }

        public void clear(Control tlp)
        {
            foreach (Control co in tlp.Controls)
            {
                if (co is Panel || co is TableLayoutPanel)
                {
                    foreach (Control item in co.Controls)
                    {
                        if (item is System.Windows.Forms.ComboBox)
                        {
                            item.Text = "";
                        }
                        else if (item is TextBox)
                        {
                            item.Text = "";
                        }
                    }
                }
            }
        }

        public void filterFactory()
        {
            if (comType.Text != "")
            {
                string query = "select * from factory inner join type_factory on factory.Factory_ID=type_factory.Factory_ID inner join type on type.Type_ID=type_factory.Type_ID  where type_factory.Type_ID=" + comType.SelectedValue;
                MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                DataTable dt = new DataTable();
                da.Fill(dt);
                comFactory.DataSource = dt;
                comFactory.DisplayMember = dt.Columns["Factory_Name"].ToString();
                comFactory.ValueMember = dt.Columns["Factory_ID"].ToString();
                comFactory.Text = "";
                txtFactory.Text = "";
            }
        }

        private void comStoreFilter_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (comStoreFilter.SelectedValue != null)
                {
                    txtStoreFilterId.Text = comStoreFilter.SelectedValue.ToString();
                }
                else
                {
                    txtStoreFilterId.Text = "";
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }

        private void comSupplier_SelectedValueChanged(object sender, EventArgs e)
        {
            if (loaded)
            {
                try
                {
                    if (comSupplier.SelectedValue != null)
                    {
                        txtSupplierId.Text = comSupplier.SelectedValue.ToString();
                    }
                    else
                    {
                        txtSupplierId.Text = "";
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                dbconnection.Close();
            }
        }

        private void comStore_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (comStore.SelectedValue != null)
                {
                    txtStoreID.Text = comStore.SelectedValue.ToString();
                }
                else
                {
                    txtStoreID.Text = "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }

        public void filterGroup()
        {
            string query = "select TypeCoding_Method from type where Type_ID=" + txtType.Text;
            MySqlCommand com = new MySqlCommand(query, dbconnection);
            int TypeCoding_Method = (int)com.ExecuteScalar();
            if (TypeCoding_Method == 1)
            {
                string query2 = "";
                if (txtType.Text == "2" || txtType.Text == "1")
                    query2 = "select * from groupo where Factory_ID=" + -1;
                else
                    query2 = "select * from groupo where Factory_ID=" + -Convert.ToInt32(txtType.Text) + " and Type_ID=" + txtType.Text;

                MySqlDataAdapter da2 = new MySqlDataAdapter(query2, dbconnection);
                DataTable dt2 = new DataTable();
                da2.Fill(dt2);
                comGroup.DataSource = dt2;
                comGroup.DisplayMember = dt2.Columns["Group_Name"].ToString();
                comGroup.ValueMember = dt2.Columns["Group_ID"].ToString();
                comGroup.Text = "";
                txtGroup.Text = "";
            }
            else
            {
                string q = "";
                if (txtFactory.Text != "")
                {
                    q = " and Factory_ID = " + txtFactory.Text;
                }
                query = "select * from groupo where Type_ID=" + txtType.Text + q;
                MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                DataTable dt = new DataTable();
                da.Fill(dt);
                comGroup.DataSource = dt;
                comGroup.DisplayMember = dt.Columns["Group_Name"].ToString();
                comGroup.ValueMember = dt.Columns["Group_ID"].ToString();
                comGroup.Text = "";
                txtGroup.Text = "";
            }
        }
        public void filterProduct()
        {
            if (comType.Text != "")
            {
                if (comGroup.Text != "" || comFactory.Text != "" || comType.Text != "")
                {
                    if (txtType.Text != "1" && txtType.Text != "2" && txtType.Text != "9")
                    {
                        string supQuery = "";

                        supQuery = " product.Type_ID=" + txtType.Text + "";
                        if (comFactory.Text != "")
                        {
                            supQuery += " and product_factory_group.Factory_ID=" + txtFactory.Text + "";
                        }
                        if (comGroup.Text != "")
                        {
                            supQuery += " and product_factory_group.Group_ID=" + txtGroup.Text + "";
                        }
                        string query = "select distinct  product.Product_ID  ,Product_Name  from product inner join product_factory_group on product.Product_ID=product_factory_group.Product_ID  where  " + supQuery + "   order by product.Product_ID";
                        MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        comProduct.DataSource = dt;
                        comProduct.DisplayMember = dt.Columns["Product_Name"].ToString();
                        comProduct.ValueMember = dt.Columns["Product_ID"].ToString();
                        comProduct.Text = "";
                        txtProduct.Text = "";
                    }
                    else
                    {
                        string supQuery = "";
                        if (comFactory.Text != "")
                        {
                            supQuery += " and type_factory.Factory_ID=" + txtFactory.Text + "";
                        }
                        if (comGroup.Text != "")
                        {
                            supQuery += " and Group_ID=" + txtGroup.Text + "";
                        }
                        string query = "select * from size inner join type_factory on size.Factory_ID=type_factory.Factory_ID where type_factory.Type_ID=" + txtType.Text + supQuery;
                        MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        comProduct.DataSource = dt;
                        comProduct.DisplayMember = dt.Columns["Size_Value"].ToString();
                        comProduct.ValueMember = dt.Columns["Size_ID"].ToString();
                        comProduct.Text = "";
                        txtProduct.Text = "";
                    }
                }
            }

        }
        public void filterSize()
        {
            if (comFactory.Text != "")
            {
                string query = "select * from size  inner join type_factory on size.Factory_ID=type_factory.Factory_ID where Type_ID=" + txtType.Text + " Factory_ID=" + comFactory.SelectedValue;
                MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                DataTable dt = new DataTable();
                da.Fill(dt);
                comProduct.DataSource = dt;
                comProduct.DisplayMember = dt.Columns["Size_Value"].ToString();
                comProduct.ValueMember = dt.Columns["Size_ID"].ToString();
                comProduct.Text = "";
                txtProduct.Text = "";
            }
        }
    }
}

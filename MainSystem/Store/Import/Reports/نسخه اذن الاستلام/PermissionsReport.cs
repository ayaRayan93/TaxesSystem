﻿using DevExpress.Utils;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
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

namespace TaxesSystem
{
    public partial class PermissionsReport : Form
    {
        MySqlConnection dbconnection, dbconnection1, dbconnection2, dbconnection3, dbconnection4;
        bool loaded = false;
        //DataRow row1 = null;
        XtraTabControl tabControlContentStore = null;
        int StorageImportPermissionID = -1;
        bool confirmed = false;

        public PermissionsReport(MainForm mainform, XtraTabControl tabControlContent)
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

        private void txtBillNumber_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    int storeID, billNum = 0;
                    if (int.TryParse(txtStoreID.Text, out storeID) && comStore.SelectedValue != null && int.TryParse(txtPermissionNumber.Text, out billNum))
                    {
                        #region edit
                        //gridControl2.DataSource = null;
                        /*DataSet sourceDataSet = new DataSet();
                        MySqlDataAdapter adapterPerm = null;
                        MySqlDataAdapter adapterSup = null;
                        MySqlDataAdapter adapterDetails = null;
                        adapterPerm = new MySqlDataAdapter("SELECT DISTINCT storage_import_permission.StorageImportPermission_ID as 'التسلسل',storage_import_permission.Import_Permission_Number as 'رقم الاذن',DATE_FORMAT(storage_import_permission.Storage_Date, '%d-%m-%Y') as 'تاريخ التخزين',storage_import_permission.Store_ID FROM storage_import_permission INNER JOIN import_supplier_permission ON import_supplier_permission.StorageImportPermission_ID = storage_import_permission.StorageImportPermission_ID INNER JOIN supplier_permission_details ON supplier_permission_details.ImportSupplierPermission_ID = import_supplier_permission.ImportSupplierPermission_ID INNER JOIN store ON store.Store_ID = storage_import_permission.Store_ID INNER JOIN supplier ON supplier.Supplier_ID = import_supplier_permission.Supplier_ID left JOIN store_places ON store_places.Store_Place_ID = supplier_permission_details.Store_Place_ID where storage_import_permission.Import_Permission_Number=" + txtPermissionNumber.Text + " and supplier_permission_details.Store_ID=" + comStore.SelectedValue.ToString(), dbconnection);
                        adapterSup = new MySqlDataAdapter("SELECT DISTINCT storage_import_permission.StorageImportPermission_ID as 'التسلسل',supplier.Supplier_Name as 'المورد',import_supplier_permission.Supplier_Permission_Number as 'اذن استلام',import_supplier_permission.Order_Number as 'رقم الطلب',import_supplier_permission.ImportSupplierPermission_ID as 'ID' FROM storage_import_permission INNER JOIN import_supplier_permission ON import_supplier_permission.StorageImportPermission_ID = storage_import_permission.StorageImportPermission_ID INNER JOIN supplier_permission_details ON supplier_permission_details.ImportSupplierPermission_ID = import_supplier_permission.ImportSupplierPermission_ID INNER JOIN store ON store.Store_ID = storage_import_permission.Store_ID INNER JOIN supplier ON supplier.Supplier_ID = import_supplier_permission.Supplier_ID left JOIN store_places ON store_places.Store_Place_ID = supplier_permission_details.Store_Place_ID where storage_import_permission.Import_Permission_Number=" + txtPermissionNumber.Text + " and supplier_permission_details.Store_ID=" + comStore.SelectedValue.ToString(), dbconnection);
                        //,store_places.Store_Place_Code as 'مكان التخزين'
                        //order by SUBSTR(data.Code,1,16),color.Color_Name,data.Description,data.Sort_ID
                        adapterDetails = new MySqlDataAdapter("select storage_import_permission.StorageImportPermission_ID as 'التسلسل',data.Code as 'الكود',type.Type_Name as 'النوع',concat(product.Product_Name,' ',COALESCE(color.Color_Name,''),' ',data.Description,' ',groupo.Group_Name,' ',factory.Factory_Name,' ',COALESCE(size.Size_Value,''),' ',COALESCE(sort.Sort_Value,'')) as 'الاسم',supplier_permission_details.Balatat as 'عدد البلتات',supplier_permission_details.Carton_Balata as 'عدد الكراتين',supplier_permission_details.Total_Meters as 'متر/قطعة',DATE_FORMAT(supplier_permission_details.Date, '%d-%m-%Y %T') as 'تاريخ التخزين',supplier_permission_details.Note as 'ملاحظة',supplier_permission_details.ImportSupplierPermission_ID as 'ID' from supplier_permission_details INNER JOIN data ON supplier_permission_details.Data_ID = data.Data_ID INNER JOIN import_supplier_permission ON supplier_permission_details.ImportSupplierPermission_ID = import_supplier_permission.ImportSupplierPermission_ID INNER JOIN storage_import_permission ON storage_import_permission.StorageImportPermission_ID = import_supplier_permission.StorageImportPermission_ID left JOIN store_places ON store_places.Store_Place_ID = supplier_permission_details.Store_Place_ID INNER JOIN supplier ON import_supplier_permission.Supplier_ID = supplier.Supplier_ID INNER JOIN type ON type.Type_ID = data.Type_ID INNER JOIN product ON product.Product_ID = data.Product_ID INNER JOIN factory ON data.Factory_ID = factory.Factory_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID LEFT JOIN color ON color.Color_ID = data.Color_ID LEFT JOIN size ON size.Size_ID = data.Size_ID LEFT JOIN sort ON sort.Sort_ID = data.Sort_ID where storage_import_permission.Import_Permission_Number=" + txtPermissionNumber.Text + " and supplier_permission_details.Store_ID=" + comStore.SelectedValue.ToString() + " ", dbconnection);
                        adapterPerm.Fill(sourceDataSet, "storage_import_permission");
                        adapterSup.Fill(sourceDataSet, "import_supplier_permission");
                        adapterDetails.Fill(sourceDataSet, "supplier_permission_details");
                        //Set up a master-detail relationship between the DataTables 
                        DataColumn keyColumn = sourceDataSet.Tables["storage_import_permission"].Columns["التسلسل"];
                        DataColumn foreignKeyColumn = sourceDataSet.Tables["import_supplier_permission"].Columns["التسلسل"];
                        //DataColumn foreignKeyColumn2 = sourceDataSet.Tables["supplier_permission_details"].Columns["التسلسل"];
                        DataColumn foreignKeyColumn3 = sourceDataSet.Tables["import_supplier_permission"].Columns["ID"];
                        DataColumn foreignKeyColumn4 = sourceDataSet.Tables["supplier_permission_details"].Columns["ID"];
                        sourceDataSet.Relations.Add("موردين الاذن", keyColumn, foreignKeyColumn);
                        //sourceDataSet.Relations.Add("تفاصيل الاذن", foreignKeyColumn, foreignKeyColumn2);
                        sourceDataSet.Relations.Add("تفاصيل الاذن", foreignKeyColumn3, foreignKeyColumn4);
                        gridControl1.DataSource = sourceDataSet.Tables["storage_import_permission"];

                        gridView1.Columns["Store_ID"].Visible = false;*/
                        #endregion

                        confirmed = false;
                        dbconnection.Open();
                        dbconnection2.Open();
                        string query = "SELECT DISTINCT storage_import_permission.StorageImportPermission_ID as 'التسلسل',import_supplier_permission.Supplier_ID,supplier.Supplier_Name as 'المورد',DATE_FORMAT(storage_import_permission.Storage_Date, '%d-%m-%Y %H:%i:%s') as 'التاريخ',storage_import_permission.Confirmed FROM storage_import_permission INNER JOIN import_supplier_permission ON import_supplier_permission.StorageImportPermission_ID = storage_import_permission.StorageImportPermission_ID INNER JOIN supplier_permission_details ON supplier_permission_details.ImportSupplierPermission_ID = import_supplier_permission.ImportSupplierPermission_ID INNER JOIN store ON store.Store_ID = storage_import_permission.Store_ID INNER JOIN supplier ON supplier.Supplier_ID = import_supplier_permission.Supplier_ID left JOIN store_places ON store_places.Store_Place_ID = supplier_permission_details.Store_Place_ID where storage_import_permission.Import_Permission_Number=" + billNum + " and supplier_permission_details.Store_ID=" + comStore.SelectedValue.ToString();
                        MySqlCommand comand = new MySqlCommand(query, dbconnection);
                        MySqlDataReader dr = comand.ExecuteReader();
                        while (dr.Read())
                        {
                            StorageImportPermissionID = Convert.ToInt16(dr["التسلسل"].ToString());
                            comSupplier.SelectedIndex = -1;
                            comSupplier.Text = dr["المورد"].ToString();
                            txtSupplierId.Text = dr["Supplier_ID"].ToString();
                            dateTimePicker1.Value = Convert.ToDateTime(dr["التاريخ"].ToString());
                            if (dr["Confirmed"].ToString() == "1")
                            {
                                confirmed = true;
                            }
                        }
                        dr.Close();

                        /*query = "select data.Code as 'الكود',type.Type_Name as 'النوع',concat(product.Product_Name,' ',COALESCE(color.Color_Name,''),' ',data.Description,' ',groupo.Group_Name,' ',factory.Factory_Name,' ',COALESCE(size.Size_Value,''),' ',COALESCE(sort.Sort_Value,'')) as 'الاسم',supplier_permission_details.Balatat as 'عدد البلتات',supplier_permission_details.Carton_Balata as 'عدد الكراتين',supplier_permission_details.Total_Meters as 'متر/قطعة',import_supplier_permission.Supplier_Permission_Number as 'اذن استلام',import_supplier_permission.Order_Number as 'رقم الطلب',DATE_FORMAT(supplier_permission_details.Date, '%d-%m-%Y %T') as 'تاريخ التخزين',supplier_permission_details.Note as 'ملاحظة' from supplier_permission_details INNER JOIN data ON supplier_permission_details.Data_ID = data.Data_ID INNER JOIN import_supplier_permission ON supplier_permission_details.ImportSupplierPermission_ID = import_supplier_permission.ImportSupplierPermission_ID INNER JOIN storage_import_permission ON storage_import_permission.StorageImportPermission_ID = import_supplier_permission.StorageImportPermission_ID left JOIN store_places ON store_places.Store_Place_ID = supplier_permission_details.Store_Place_ID INNER JOIN supplier ON import_supplier_permission.Supplier_ID = supplier.Supplier_ID INNER JOIN type ON type.Type_ID = data.Type_ID INNER JOIN product ON product.Product_ID = data.Product_ID INNER JOIN factory ON data.Factory_ID = factory.Factory_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID LEFT JOIN color ON color.Color_ID = data.Color_ID LEFT JOIN size ON size.Size_ID = data.Size_ID LEFT JOIN sort ON sort.Sort_ID = data.Sort_ID where storage_import_permission.Import_Permission_Number=" + billNum + " and supplier_permission_details.Store_ID=" + comStore.SelectedValue.ToString();
                        MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        gridControl1.DataSource = dt;*/

                        string qq = "select data.Data_ID,data.Code as 'الكود',type.Type_Name as 'النوع',concat(product.Product_Name,' ',COALESCE(color.Color_Name,''),' ',data.Description,' ',groupo.Group_Name,' ',factory.Factory_Name,' ',COALESCE(size.Size_Value,''),' ',COALESCE(sort.Sort_Value,'')) as 'الاسم',supplier_permission_details.Balatat as 'عدد البلتات',supplier_permission_details.Carton_Balata as 'عدد الكراتين',supplier_permission_details.Total_Meters as 'متر/قطعة',import_supplier_permission.Supplier_Permission_Number as 'اذن استلام',DATE_FORMAT(supplier_permission_details.Date, '%d-%m-%Y %T') as 'تاريخ التخزين',supplier_permission_details.Note as 'ملاحظة',import_supplier_permission.Supplier_ID,supplier_permission_details.Supplier_Permission_Details_ID,'Factory_ID','المصنع','رقم الطلب',import_supplier_permission.Purchase_Bill from supplier_permission_details INNER JOIN data ON supplier_permission_details.Data_ID = data.Data_ID INNER JOIN import_supplier_permission ON supplier_permission_details.ImportSupplierPermission_ID = import_supplier_permission.ImportSupplierPermission_ID INNER JOIN storage_import_permission ON storage_import_permission.StorageImportPermission_ID = import_supplier_permission.StorageImportPermission_ID  INNER JOIN supplier ON import_supplier_permission.Supplier_ID = supplier.Supplier_ID INNER JOIN type ON type.Type_ID = data.Type_ID INNER JOIN product ON product.Product_ID = data.Product_ID INNER JOIN factory ON data.Factory_ID = factory.Factory_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID LEFT JOIN color ON color.Color_ID = data.Color_ID LEFT JOIN size ON size.Size_ID = data.Size_ID LEFT JOIN sort ON sort.Sort_ID = data.Sort_ID where data.Data_ID=0 order by SUBSTR(data.Code,1,16),color.Color_Name,data.Description,data.Sort_ID";
                        MySqlDataAdapter da = new MySqlDataAdapter(qq, dbconnection);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        gridControl1.DataSource = dt;
                        gridView1.Columns["Data_ID"].Visible = false;
                        gridView1.Columns["Supplier_ID"].Visible = false;
                        gridView1.Columns["Supplier_Permission_Details_ID"].Visible = false;
                        gridView1.Columns["Factory_ID"].Visible = false;
                        gridView1.Columns["Purchase_Bill"].Visible = false;

                        for (int i = 0; i < gridView1.Columns.Count; i++)
                        {
                            gridView1.Columns[i].Width = 100;
                        }
                        gridView1.Columns["الكود"].Width = 180;
                        gridView1.Columns["الاسم"].Width = 200;

                        qq = "select data.Data_ID,data.Code as 'الكود',type.Type_Name as 'النوع',concat(product.Product_Name,' ',COALESCE(color.Color_Name,''),' ',data.Description,' ',groupo.Group_Name,' ',factory.Factory_Name,' ',COALESCE(size.Size_Value,''),' ',COALESCE(sort.Sort_Value,'')) as 'الاسم',supplier_permission_details.Balatat as 'عدد البلتات',supplier_permission_details.Carton_Balata as 'عدد الكراتين',supplier_permission_details.Total_Meters as 'متر/قطعة',import_supplier_permission.Supplier_Permission_Number as 'اذن استلام',DATE_FORMAT(supplier_permission_details.Date, '%d-%m-%Y %T') as 'تاريخ التخزين',supplier_permission_details.Note as 'ملاحظة',import_supplier_permission.Supplier_ID,supplier_permission_details.Supplier_Permission_Details_ID,import_supplier_permission.Purchase_Bill from supplier_permission_details INNER JOIN data ON supplier_permission_details.Data_ID = data.Data_ID INNER JOIN import_supplier_permission ON supplier_permission_details.ImportSupplierPermission_ID = import_supplier_permission.ImportSupplierPermission_ID INNER JOIN storage_import_permission ON storage_import_permission.StorageImportPermission_ID = import_supplier_permission.StorageImportPermission_ID  INNER JOIN supplier ON import_supplier_permission.Supplier_ID = supplier.Supplier_ID INNER JOIN type ON type.Type_ID = data.Type_ID INNER JOIN product ON product.Product_ID = data.Product_ID INNER JOIN factory ON data.Factory_ID = factory.Factory_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID LEFT JOIN color ON color.Color_ID = data.Color_ID LEFT JOIN size ON size.Size_ID = data.Size_ID LEFT JOIN sort ON sort.Sort_ID = data.Sort_ID where storage_import_permission.Import_Permission_Number=" + billNum + " and supplier_permission_details.Store_ID=" + comStore.SelectedValue.ToString();
                        MySqlCommand com = new MySqlCommand(qq, dbconnection);
                        dr = com.ExecuteReader();
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                gridView1.AddNewRow();
                                int rowHandle = gridView1.GetRowHandle(gridView1.DataRowCount);
                                if (gridView1.IsNewItemRow(rowHandle))
                                {
                                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns[0], dr["Data_ID"]);
                                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns[1], dr["الكود"]);
                                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns["النوع"], dr["النوع"]);
                                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns["الاسم"], dr["الاسم"]);
                                    //gridView1.SetRowCellValue(rowHandle, gridView1.Columns["المورد"], dr["المورد"]);
                                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns["اذن استلام"], dr["اذن استلام"]);
                                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns["عدد البلتات"], dr["عدد البلتات"]);
                                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns["عدد الكراتين"], dr["عدد الكراتين"]);
                                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns["متر/قطعة"], dr["متر/قطعة"]);
                                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns["تاريخ التخزين"], dr["تاريخ التخزين"]);
                                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns["ملاحظة"], dr["ملاحظة"]);
                                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns["Supplier_ID"], dr["Supplier_ID"]);
                                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns["Supplier_Permission_Details_ID"], dr["Supplier_Permission_Details_ID"]);
                                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns["Purchase_Bill"], dr["Purchase_Bill"]);

                                    qq = "select orders.Order_Number,orders.Factory_ID,factory2.Factory_Name from supplier_permission_details INNER JOIN data ON supplier_permission_details.Data_ID = data.Data_ID INNER JOIN import_supplier_permission ON supplier_permission_details.ImportSupplierPermission_ID = import_supplier_permission.ImportSupplierPermission_ID INNER JOIN storage_import_permission ON storage_import_permission.StorageImportPermission_ID = import_supplier_permission.StorageImportPermission_ID  INNER JOIN supplier ON import_supplier_permission.Supplier_ID = supplier.Supplier_ID inner JOIN order_details ON supplier_permission_details.Data_ID = order_details.Data_ID inner JOIN orders ON order_details.Order_ID = orders.Order_ID INNER JOIN factory as factory2 ON orders.Factory_ID = factory2.Factory_ID INNER JOIN type ON type.Type_ID = data.Type_ID INNER JOIN product ON product.Product_ID = data.Product_ID INNER JOIN factory ON data.Factory_ID = factory.Factory_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID LEFT JOIN color ON color.Color_ID = data.Color_ID LEFT JOIN size ON size.Size_ID = data.Size_ID LEFT JOIN sort ON sort.Sort_ID = data.Sort_ID where storage_import_permission.Import_Permission_Number=" + billNum + " and supplier_permission_details.Store_ID=" + comStore.SelectedValue.ToString() + " and data.Data_ID=" + dr["Data_ID"] + " ";
                                    com = new MySqlCommand(qq, dbconnection2);
                                    MySqlDataReader dr2 = com.ExecuteReader();
                                    if (dr2.HasRows)
                                    {
                                        while (dr2.Read())
                                        {
                                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns["رقم الطلب"], dr2["Order_Number"]);
                                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns["Factory_ID"], dr2["Factory_ID"]);
                                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns["المصنع"], dr2["Factory_Name"]);
                                        }
                                    }
                                    dr2.Close();
                                }
                            }
                        }
                        dr.Close();

                        if (gridView1.IsLastVisibleRow)
                        {
                            gridView1.FocusedRowHandle = gridView1.RowCount - 1;
                        }
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
                dbconnection2.Close();
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (UserControl.userType == 1 || UserControl.userType == 13 || UserControl.userType == 7)
            {
                try
                {
                    int storeID, billNum = 0;
                    if (int.TryParse(txtStoreID.Text, out storeID) && comStore.SelectedValue != null && int.TryParse(txtPermissionNumber.Text, out billNum))
                    {
                        if (!confirmed)
                        {
                            SupplierReceiptUpdate form = new SupplierReceiptUpdate(comStore.SelectedValue.ToString(), txtPermissionNumber.Text, dateTimePicker1.Value, this, tabControlContentStore);
                            form.ShowDialog();
                        }
                        else
                        {
                            MessageBox.Show("هذا الاذن منتهى");
                        }
                    }
                    else
                    {
                        MessageBox.Show("يجب التاكد من البيانات");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void btnDisplay_Click(object sender, EventArgs e)
        {
            /*try
            {
                int storeID = 0;
                if (int.TryParse(txtStoreID.Text, out storeID) && comStore.SelectedValue != null)
                {
                    txtPermissionNumber.Text = "";
                    //gridControl2.DataSource = null;
                    DataSet sourceDataSet = new DataSet();
                    MySqlDataAdapter adapterPerm = null;
                    MySqlDataAdapter adapterSup = null;
                    MySqlDataAdapter adapterDetails = null;
                    //DateTime date = dateTimePicker1.Value.Date;
                    //string d = date.ToString("dd-MM-yyyy");
                    //DateTime date2 = dateTimePicker2.Value.Date;
                    //string d2 = date2.ToString("dd-MM-yyyy");
                    adapterPerm = new MySqlDataAdapter("SELECT DISTINCT storage_import_permission.StorageImportPermission_ID as 'التسلسل',storage_import_permission.Import_Permission_Number as 'رقم الاذن',DATE_FORMAT(storage_import_permission.Storage_Date, '%d-%m-%Y') as 'تاريخ التخزين',storage_import_permission.Store_ID FROM storage_import_permission INNER JOIN import_supplier_permission ON import_supplier_permission.StorageImportPermission_ID = storage_import_permission.StorageImportPermission_ID INNER JOIN supplier_permission_details ON supplier_permission_details.ImportSupplierPermission_ID = import_supplier_permission.ImportSupplierPermission_ID INNER JOIN store ON store.Store_ID = storage_import_permission.Store_ID INNER JOIN supplier ON supplier.Supplier_ID = import_supplier_permission.Supplier_ID left JOIN store_places ON store_places.Store_Place_ID = supplier_permission_details.Store_Place_ID where supplier_permission_details.Store_ID=" + comStore.SelectedValue.ToString() + " and date(storage_import_permission.Storage_Date) between '" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "' and '" + dateTimePicker2.Value.ToString("yyyy-MM-dd") + "'", dbconnection);
                    adapterSup = new MySqlDataAdapter("SELECT DISTINCT storage_import_permission.StorageImportPermission_ID as 'التسلسل',supplier.Supplier_Name as 'المورد',import_supplier_permission.Supplier_Permission_Number as 'اذن استلام',import_supplier_permission.Order_Number as 'رقم الطلب',import_supplier_permission.ImportSupplierPermission_ID as 'ID' FROM storage_import_permission INNER JOIN import_supplier_permission ON import_supplier_permission.StorageImportPermission_ID = storage_import_permission.StorageImportPermission_ID INNER JOIN supplier_permission_details ON supplier_permission_details.ImportSupplierPermission_ID = import_supplier_permission.ImportSupplierPermission_ID INNER JOIN store ON store.Store_ID = storage_import_permission.Store_ID INNER JOIN supplier ON supplier.Supplier_ID = import_supplier_permission.Supplier_ID left JOIN store_places ON store_places.Store_Place_ID = supplier_permission_details.Store_Place_ID where supplier_permission_details.Store_ID=" + comStore.SelectedValue.ToString() + " and date(storage_import_permission.Storage_Date) between '" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "' and '" + dateTimePicker2.Value.ToString("yyyy-MM-dd") + "'", dbconnection);
                    //,store_places.Store_Place_Code as 'مكان التخزين'
                    //order by SUBSTR(data.Code,1,16),color.Color_Name,data.Description,data.Sort_ID
                    adapterDetails = new MySqlDataAdapter("select storage_import_permission.StorageImportPermission_ID as 'التسلسل',data.Code as 'الكود',type.Type_Name as 'النوع',concat(product.Product_Name,' ',COALESCE(color.Color_Name,''),' ',data.Description,' ',groupo.Group_Name,' ',factory.Factory_Name,' ',COALESCE(size.Size_Value,''),' ',COALESCE(sort.Sort_Value,'')) as 'الاسم',supplier_permission_details.Balatat as 'عدد البلتات',supplier_permission_details.Carton_Balata as 'عدد الكراتين',supplier_permission_details.Total_Meters as 'متر/قطعة',DATE_FORMAT(supplier_permission_details.Date, '%d-%m-%Y %T') as 'تاريخ التخزين',supplier_permission_details.Note as 'ملاحظة',supplier_permission_details.ImportSupplierPermission_ID as 'ID' from supplier_permission_details INNER JOIN data ON supplier_permission_details.Data_ID = data.Data_ID INNER JOIN import_supplier_permission ON supplier_permission_details.ImportSupplierPermission_ID = import_supplier_permission.ImportSupplierPermission_ID INNER JOIN storage_import_permission ON storage_import_permission.StorageImportPermission_ID = import_supplier_permission.StorageImportPermission_ID left JOIN store_places ON store_places.Store_Place_ID = supplier_permission_details.Store_Place_ID INNER JOIN supplier ON import_supplier_permission.Supplier_ID = supplier.Supplier_ID INNER JOIN type ON type.Type_ID = data.Type_ID INNER JOIN product ON product.Product_ID = data.Product_ID INNER JOIN factory ON data.Factory_ID = factory.Factory_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID LEFT JOIN color ON color.Color_ID = data.Color_ID LEFT JOIN size ON size.Size_ID = data.Size_ID LEFT JOIN sort ON sort.Sort_ID = data.Sort_ID where supplier_permission_details.Store_ID=" + comStore.SelectedValue.ToString() + " and date(storage_import_permission.Storage_Date) between '" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "' and '" + dateTimePicker2.Value.ToString("yyyy-MM-dd") + "'", dbconnection);
                    adapterPerm.Fill(sourceDataSet, "storage_import_permission");
                    adapterSup.Fill(sourceDataSet, "import_supplier_permission");
                    adapterDetails.Fill(sourceDataSet, "supplier_permission_details");
                    //Set up a master-detail relationship between the DataTables 
                    DataColumn keyColumn = sourceDataSet.Tables["storage_import_permission"].Columns["التسلسل"];
                    DataColumn foreignKeyColumn = sourceDataSet.Tables["import_supplier_permission"].Columns["التسلسل"];
                    //DataColumn foreignKeyColumn2 = sourceDataSet.Tables["supplier_permission_details"].Columns["التسلسل"];
                    DataColumn foreignKeyColumn3 = sourceDataSet.Tables["import_supplier_permission"].Columns["ID"];
                    DataColumn foreignKeyColumn4 = sourceDataSet.Tables["supplier_permission_details"].Columns["ID"];
                    sourceDataSet.Relations.Add("موردين الاذن", keyColumn, foreignKeyColumn);
                    //sourceDataSet.Relations.Add("تفاصيل الاذن", foreignKeyColumn, foreignKeyColumn2);
                    sourceDataSet.Relations.Add("تفاصيل الاذن", foreignKeyColumn3, foreignKeyColumn4);
                    gridControl1.DataSource = sourceDataSet.Tables["storage_import_permission"];

                    gridView1.Columns["Store_ID"].Visible = false;
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

        private void gridView1_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            /*if (loaded)
            {
                try
                {
                    row1 = gridView1.GetDataRow(gridView1.GetRowHandle(e.RowHandle));
                    DataSet sourceDataSet = new DataSet();
                    MySqlDataAdapter adapterPerm = null;
                    MySqlDataAdapter adapterSup = null;
                    MySqlDataAdapter adapterDetails = null;
                    adapterPerm = new MySqlDataAdapter("SELECT DISTINCT import_storage_return.ImportStorageReturn_ID as 'التسلسل',import_storage_return.Returned_Permission_Number as 'رقم اذن المرتجع',DATE_FORMAT(import_storage_return.Retrieval_Date, '%d-%m-%Y') as 'تاريخ الاسترجاع',import_storage_return.Reason as 'سبب الاسترجاع' FROM import_storage_return INNER JOIN import_storage_return_supplier ON import_storage_return_supplier.ImportStorageReturn_ID = import_storage_return.ImportStorageReturn_ID INNER JOIN import_storage_return_details ON import_storage_return_details.ImportStorageReturnSupplier_ID = import_storage_return_supplier.ImportStorageReturnSupplier_ID INNER JOIN supplier ON supplier.Supplier_ID = import_storage_return_supplier.Supplier_ID left JOIN store_places ON store_places.Store_Place_ID = import_storage_return_details.Store_Place_ID where import_storage_return.StorageImportPermission_ID=" + row1["التسلسل"].ToString(), dbconnection);
                    adapterSup = new MySqlDataAdapter("SELECT DISTINCT import_storage_return.ImportStorageReturn_ID as 'التسلسل',supplier.Supplier_Name as 'المورد',import_storage_return_supplier.Supplier_Permission_Number as 'رقم اذن الاستلام',import_storage_return_supplier.ImportStorageReturnSupplier_ID as 'ID' FROM import_storage_return INNER JOIN import_storage_return_supplier ON import_storage_return_supplier.ImportStorageReturn_ID = import_storage_return.ImportStorageReturn_ID INNER JOIN import_storage_return_details ON import_storage_return_details.ImportStorageReturnSupplier_ID = import_storage_return_supplier.ImportStorageReturnSupplier_ID INNER JOIN supplier ON supplier.Supplier_ID = import_storage_return_supplier.Supplier_ID left JOIN store_places ON store_places.Store_Place_ID = import_storage_return_details.Store_Place_ID where import_storage_return.StorageImportPermission_ID=" + row1["التسلسل"].ToString(), dbconnection);
                    //,store_places.Store_Place_Code as 'مكان التخزين'
                    //order by SUBSTR(data.Code,1,16),color.Color_Name,data.Description,data.Sort_ID
                    adapterDetails = new MySqlDataAdapter("SELECT import_storage_return.ImportStorageReturn_ID as 'التسلسل',data.Code as 'الكود',type.Type_Name as 'النوع',concat(product.Product_Name,' ',COALESCE(color.Color_Name,''),' ',data.Description,' ',groupo.Group_Name,' ',factory.Factory_Name,' ',COALESCE(size.Size_Value,''),' ',COALESCE(sort.Sort_Value,'')) as 'الاسم',import_storage_return_details.Balatat as 'عدد البلتات',import_storage_return_details.Carton_Balata as 'عدد الكراتين',import_storage_return_details.Total_Meters as 'متر/قطعة',DATE_FORMAT(import_storage_return_details.Date, '%d-%m-%Y %T') as 'وقت الاسترجاع',import_storage_return_details.Reason as 'السبب' FROM import_storage_return INNER JOIN import_storage_return_supplier ON import_storage_return_supplier.ImportStorageReturn_ID = import_storage_return.ImportStorageReturn_ID INNER JOIN import_storage_return_details ON import_storage_return_details.ImportStorageReturnSupplier_ID = import_storage_return_supplier.ImportStorageReturnSupplier_ID INNER JOIN supplier ON supplier.Supplier_ID = import_storage_return_supplier.Supplier_ID left JOIN store_places ON store_places.Store_Place_ID = import_storage_return_details.Store_Place_ID INNER JOIN data ON import_storage_return_details.Data_ID = data.Data_ID INNER JOIN type ON type.Type_ID = data.Type_ID INNER JOIN product ON product.Product_ID = data.Product_ID INNER JOIN factory ON data.Factory_ID = factory.Factory_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID LEFT JOIN color ON color.Color_ID = data.Color_ID LEFT JOIN size ON size.Size_ID = data.Size_ID LEFT JOIN sort ON sort.Sort_ID = data.Sort_ID  where import_storage_return.StorageImportPermission_ID=" + row1["التسلسل"].ToString() + " ", dbconnection);
                    adapterPerm.Fill(sourceDataSet, "import_storage_return");
                    adapterSup.Fill(sourceDataSet, "import_storage_return_supplier");
                    adapterDetails.Fill(sourceDataSet, "import_storage_return_details");
                    //Set up a master-detail relationship between the DataTables 
                    DataColumn keyColumn = sourceDataSet.Tables["import_storage_return"].Columns["التسلسل"];
                    DataColumn foreignKeyColumn = sourceDataSet.Tables["import_storage_return_supplier"].Columns["التسلسل"];
                    DataColumn foreignKeyColumn2 = sourceDataSet.Tables["import_storage_return_details"].Columns["التسلسل"];
                    sourceDataSet.Relations.Add("موردين الاذن", keyColumn, foreignKeyColumn);
                    sourceDataSet.Relations.Add("تفاصيل الاذن", foreignKeyColumn, foreignKeyColumn2);
                    gridControl2.DataSource = sourceDataSet.Tables["import_storage_return"];
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                dbconnection.Close();
            }*/
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                int storeID = 0;
                if (int.TryParse(txtStoreID.Text, out storeID) && comStore.SelectedValue != null && txtPermissionNumber.Text != "" && gridView1.RowCount > 0)
                {
                    DXMouseEventArgs ea = e as DXMouseEventArgs;
                    GridView view = sender as GridView;
                    GridHitInfo info = view.CalcHitInfo(ea.Location);
                    if (info.InRow || info.InRowCell)
                    {
                        DataRowView row2 = (DataRowView)gridView1.GetRow(gridView1.GetRowHandle(info.RowHandle));
                        if (info.Column.GetCaption() == "متر/قطعة")
                        {
                            if (row2["Purchase_Bill"].ToString() == "0")
                            {
                                SupplierReceiptQuantity_Update sd = new SupplierReceiptQuantity_Update(info.RowHandle, row2, storeID, this, null/*, "PermissionsReport", this*/);
                                sd.ShowDialog();
                            }
                            else
                            {
                                MessageBox.Show("هذا الاذن منتهى");
                            }
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            try
            {
                int storeID = 0;
                if (int.TryParse(txtStoreID.Text, out storeID) && comStore.SelectedValue != null && txtPermissionNumber.Text != "" && gridView1.RowCount > 0)
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
                    q1 = "SELECT DISTINCT storage_import_permission.StorageImportPermission_ID as 'التسلسل',storage_import_permission.Import_Permission_Number as 'رقم الاذن',DATE_FORMAT(storage_import_permission.Storage_Date, '%d-%m-%Y') as 'تاريخ التخزين' FROM storage_import_permission INNER JOIN import_supplier_permission ON import_supplier_permission.StorageImportPermission_ID = storage_import_permission.StorageImportPermission_ID INNER JOIN supplier_permission_details ON supplier_permission_details.ImportSupplierPermission_ID = import_supplier_permission.ImportSupplierPermission_ID INNER JOIN store ON store.Store_ID = storage_import_permission.Store_ID INNER JOIN supplier ON supplier.Supplier_ID = import_supplier_permission.Supplier_ID left JOIN store_places ON store_places.Store_Place_ID = supplier_permission_details.Store_Place_ID where storage_import_permission.Import_Permission_Number=" + txtPermissionNumber.Text + " and supplier_permission_details.Store_ID=" + comStore.SelectedValue.ToString();
                    MySqlCommand com1 = new MySqlCommand(q1, dbconnection1);
                    MySqlDataReader dr1 = com1.ExecuteReader();
                    while (dr1.Read())
                    {
                        List<string> supplierList = new List<string>();
                        List<SupplierReceipt_Items> bi = new List<SupplierReceipt_Items>();
                        int supplierCount = 0;
                        int gridcount = 0;
                        q2 = "SELECT DISTINCT supplier.Supplier_Name as 'المورد',import_supplier_permission.Supplier_Permission_Number as 'اذن استلام',import_supplier_permission.Order_Number as 'رقم الطلب',import_supplier_permission.ImportSupplierPermission_ID as 'ID' FROM storage_import_permission INNER JOIN import_supplier_permission ON import_supplier_permission.StorageImportPermission_ID = storage_import_permission.StorageImportPermission_ID INNER JOIN supplier_permission_details ON supplier_permission_details.ImportSupplierPermission_ID = import_supplier_permission.ImportSupplierPermission_ID INNER JOIN store ON store.Store_ID = storage_import_permission.Store_ID INNER JOIN supplier ON supplier.Supplier_ID = import_supplier_permission.Supplier_ID left JOIN store_places ON store_places.Store_Place_ID = supplier_permission_details.Store_Place_ID where storage_import_permission.Import_Permission_Number=" + txtPermissionNumber.Text + " and supplier_permission_details.Store_ID=" + comStore.SelectedValue.ToString() + " and storage_import_permission.StorageImportPermission_ID=" + dr1["التسلسل"].ToString();
                        MySqlCommand com2 = new MySqlCommand(q2, dbconnection2);
                        MySqlDataReader dr2 = com2.ExecuteReader();
                        while (dr2.Read())
                        {
                            supplierList.Add(dr2["المورد"].ToString());
                            bool flagTest = false;
                            q3 = "select storage_import_permission.StorageImportPermission_ID as 'التسلسل',data.Code as 'الكود',type.Type_Name as 'النوع',concat(product.Product_Name,' ',COALESCE(color.Color_Name,''),' ',data.Description,' ',groupo.Group_Name,' ',factory.Factory_Name,' ',COALESCE(size.Size_Value,''),' ',COALESCE(sort.Sort_Value,'')) as 'الاسم',supplier_permission_details.Balatat as 'عدد البلتات',supplier_permission_details.Carton_Balata as 'عدد الكراتين',supplier_permission_details.Total_Meters as 'متر/قطعة',DATE_FORMAT(supplier_permission_details.Date, '%d-%m-%Y %T') as 'تاريخ التخزين',supplier_permission_details.Note as 'ملاحظة',supplier_permission_details.ImportSupplierPermission_ID as 'ID' from supplier_permission_details INNER JOIN data ON supplier_permission_details.Data_ID = data.Data_ID INNER JOIN import_supplier_permission ON supplier_permission_details.ImportSupplierPermission_ID = import_supplier_permission.ImportSupplierPermission_ID INNER JOIN storage_import_permission ON storage_import_permission.StorageImportPermission_ID = import_supplier_permission.StorageImportPermission_ID left JOIN store_places ON store_places.Store_Place_ID = supplier_permission_details.Store_Place_ID INNER JOIN supplier ON import_supplier_permission.Supplier_ID = supplier.Supplier_ID INNER JOIN type ON type.Type_ID = data.Type_ID INNER JOIN product ON product.Product_ID = data.Product_ID INNER JOIN factory ON data.Factory_ID = factory.Factory_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID LEFT JOIN color ON color.Color_ID = data.Color_ID LEFT JOIN size ON size.Size_ID = data.Size_ID LEFT JOIN sort ON sort.Sort_ID = data.Sort_ID where storage_import_permission.Import_Permission_Number=" + txtPermissionNumber.Text + " and supplier_permission_details.Store_ID=" + comStore.SelectedValue.ToString() + " and import_supplier_permission.ImportSupplierPermission_ID=" + dr2["ID"].ToString();
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

                                SupplierReceipt_Items item = new SupplierReceipt_Items() { Code = dr3["الكود"].ToString(), Product_Type = dr3["النوع"].ToString(), Product_Name = dr3["الاسم"].ToString(), Balatat = balate, Carton_Balata = carton, Total_Meters = quantity, Supplier_Permission_Number = Convert.ToInt32(dr2["اذن استلام"].ToString()), Date = Convert.ToDateTime(dr3["تاريخ التخزين"].ToString()).ToString("yyyy-MM-dd hh:mm:ss"), Note = dr3["ملاحظة"].ToString() };
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
                        }
                        dr2.Close();

                        Report_SupplierReceiptCopy f = new Report_SupplierReceiptCopy();
                        f.PrintInvoice(storeName, dr1["رقم الاذن"].ToString(), suppliers_Name, dateTimePicker1.Value, bi);
                        f.ShowDialog();
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
                    int storeID, billNum = 0;
                    if (gridView1.RowCount > 0 && int.TryParse(txtStoreID.Text, out storeID) && comStore.SelectedValue != null && int.TryParse(txtPermissionNumber.Text, out billNum))
                    {
                        if (!confirmed)
                        {
                            if (MessageBox.Show("هل انت متاكد انك تريد الحذف؟", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                dbconnection.Open();
                                dbconnection1.Open();
                                dbconnection2.Open();
                                dbconnection3.Open();
                                dbconnection4.Open();
                                if (checkQuantity())
                                {
                                    string q2 = "SELECT DISTINCT supplier.Supplier_Name as 'المورد',import_supplier_permission.Supplier_Permission_Number as 'اذن استلام',import_supplier_permission.Order_Number as 'رقم الطلب',import_supplier_permission.ImportSupplierPermission_ID as 'ID',import_supplier_permission.Factory_ID FROM storage_import_permission INNER JOIN import_supplier_permission ON import_supplier_permission.StorageImportPermission_ID = storage_import_permission.StorageImportPermission_ID INNER JOIN supplier_permission_details ON supplier_permission_details.ImportSupplierPermission_ID = import_supplier_permission.ImportSupplierPermission_ID INNER JOIN store ON store.Store_ID = storage_import_permission.Store_ID INNER JOIN supplier ON supplier.Supplier_ID = import_supplier_permission.Supplier_ID left JOIN store_places ON store_places.Store_Place_ID = supplier_permission_details.Store_Place_ID where storage_import_permission.Import_Permission_Number=" + txtPermissionNumber.Text + " and supplier_permission_details.Store_ID=" + comStore.SelectedValue.ToString() + " and storage_import_permission.StorageImportPermission_ID=" + StorageImportPermissionID;
                                    MySqlCommand com2 = new MySqlCommand(q2, dbconnection2);
                                    MySqlDataReader dr2 = com2.ExecuteReader();
                                    while (dr2.Read())
                                    {
                                        string q3 = "select storage_import_permission.StorageImportPermission_ID as 'التسلسل',data.Data_ID,data.Code as 'الكود',type.Type_Name as 'النوع',concat(product.Product_Name,' ',COALESCE(color.Color_Name,''),' ',data.Description,' ',groupo.Group_Name,' ',factory.Factory_Name,' ',COALESCE(size.Size_Value,''),' ',COALESCE(sort.Sort_Value,'')) as 'الاسم',supplier_permission_details.Balatat as 'عدد البلتات',supplier_permission_details.Carton_Balata as 'عدد الكراتين',supplier_permission_details.Total_Meters as 'متر/قطعة',DATE_FORMAT(supplier_permission_details.Date, '%d-%m-%Y %T') as 'تاريخ التخزين',supplier_permission_details.Note as 'ملاحظة',supplier_permission_details.ImportSupplierPermission_ID as 'ID' from supplier_permission_details INNER JOIN data ON supplier_permission_details.Data_ID = data.Data_ID INNER JOIN import_supplier_permission ON supplier_permission_details.ImportSupplierPermission_ID = import_supplier_permission.ImportSupplierPermission_ID INNER JOIN storage_import_permission ON storage_import_permission.StorageImportPermission_ID = import_supplier_permission.StorageImportPermission_ID left JOIN store_places ON store_places.Store_Place_ID = supplier_permission_details.Store_Place_ID INNER JOIN supplier ON import_supplier_permission.Supplier_ID = supplier.Supplier_ID INNER JOIN type ON type.Type_ID = data.Type_ID INNER JOIN product ON product.Product_ID = data.Product_ID INNER JOIN factory ON data.Factory_ID = factory.Factory_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID LEFT JOIN color ON color.Color_ID = data.Color_ID LEFT JOIN size ON size.Size_ID = data.Size_ID LEFT JOIN sort ON sort.Sort_ID = data.Sort_ID where storage_import_permission.Import_Permission_Number=" + txtPermissionNumber.Text + " and supplier_permission_details.Store_ID=" + comStore.SelectedValue.ToString() + " and import_supplier_permission.ImportSupplierPermission_ID=" + dr2["ID"].ToString();
                                        MySqlCommand com3 = new MySqlCommand(q3, dbconnection3);
                                        MySqlDataReader dr3 = com3.ExecuteReader();
                                        while (dr3.Read())
                                        {
                                            string query = "select Total_Meters from storage where Store_ID=" + comStore.SelectedValue.ToString() + " and Data_ID=" + dr3["Data_ID"].ToString();
                                            MySqlCommand com = new MySqlCommand(query, dbconnection);
                                            if (com.ExecuteScalar() != null)
                                            {
                                                double totalf = Convert.ToInt32(com.ExecuteScalar());
                                                if ((totalf - Convert.ToDouble(dr3["متر/قطعة"].ToString())) >= 0)
                                                {
                                                    query = "update storage set Total_Meters=" + (totalf - Convert.ToDouble(dr3["متر/قطعة"].ToString())) + " where Store_ID=" + comStore.SelectedValue.ToString() + " and Data_ID=" + dr3["Data_ID"].ToString();
                                                    com = new MySqlCommand(query, dbconnection);
                                                    com.ExecuteNonQuery();

                                                    #region MyRegion
                                                    if (dr2["Factory_ID"].ToString() != "" && dr2["رقم الطلب"].ToString() != "")
                                                    {
                                                        dbconnection1.Open();
                                                        query = "select order_details.OrderDetails_ID,order_details.Quantity from orders INNER JOIN order_details ON order_details.Order_ID = orders.Order_ID where order_details.Data_ID=" + dr3["Data_ID"].ToString() + " and orders.Factory_ID=" + dr2["Factory_ID"].ToString() + " and orders.Order_Number=" + dr2["رقم الطلب"].ToString();
                                                        com = new MySqlCommand(query, dbconnection1);
                                                        MySqlDataReader dr = com.ExecuteReader();
                                                        if (dr.HasRows)
                                                        {
                                                            while (dr.Read())
                                                            {
                                                                double orderQuantity = Convert.ToDouble(dr["Quantity"].ToString());
                                                                if (orderQuantity == Convert.ToDouble(dr3["متر/قطعة"].ToString()))
                                                                {
                                                                    query = "update order_details set Received=0 where OrderDetails_ID=" + dr["OrderDetails_ID"].ToString();
                                                                }
                                                                else
                                                                {
                                                                    query = "select sum(supplier_permission_details.Total_Meters) as 'Total_Meters' from supplier_permission_details INNER JOIN data ON supplier_permission_details.Data_ID = data.Data_ID INNER JOIN import_supplier_permission ON supplier_permission_details.ImportSupplierPermission_ID = import_supplier_permission.ImportSupplierPermission_ID INNER JOIN storage_import_permission ON storage_import_permission.StorageImportPermission_ID = import_supplier_permission.StorageImportPermission_ID  INNER JOIN supplier ON import_supplier_permission.Supplier_ID = supplier.Supplier_ID inner JOIN order_details ON supplier_permission_details.Data_ID = order_details.Data_ID inner JOIN orders ON order_details.Order_ID = orders.Order_ID INNER JOIN type ON type.Type_ID = data.Type_ID INNER JOIN product ON product.Product_ID = data.Product_ID INNER JOIN factory ON data.Factory_ID = factory.Factory_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID LEFT JOIN color ON color.Color_ID = data.Color_ID LEFT JOIN size ON size.Size_ID = data.Size_ID LEFT JOIN sort ON sort.Sort_ID = data.Sort_ID where data.Data_ID=" + dr3["Data_ID"].ToString() + " and orders.Order_Number=" + dr2["رقم الطلب"].ToString() + " and orders.Factory_ID=" + dr2["Factory_ID"].ToString() + " ";
                                                                    com = new MySqlCommand(query, dbconnection4);
                                                                    MySqlDataReader dr4 = com.ExecuteReader();
                                                                    if (dr4.HasRows)
                                                                    {
                                                                        while (dr4.Read())
                                                                        {
                                                                            if (dr4["Total_Meters"].ToString() != "")
                                                                            {
                                                                                if (Convert.ToDouble(dr4["Total_Meters"].ToString()) > 0)
                                                                                {
                                                                                    query = "update order_details set Received=2 where OrderDetails_ID=" + dr["OrderDetails_ID"].ToString();
                                                                                }
                                                                                else
                                                                                {
                                                                                    query = "update order_details set Received=0 where OrderDetails_ID=" + dr["OrderDetails_ID"].ToString();
                                                                                }
                                                                            }
                                                                            else
                                                                            {
                                                                                query = "update order_details set Received=0 where OrderDetails_ID=" + dr["OrderDetails_ID"].ToString();
                                                                            }
                                                                        }
                                                                    }
                                                                    dr4.Close();
                                                                }

                                                                com = new MySqlCommand(query, dbconnection);
                                                                com.ExecuteNonQuery();
                                                            }
                                                        }
                                                        dr.Close();
                                                    }
                                                    #endregion
                                                }
                                                else
                                                {
                                                    MessageBox.Show("لا يوجد كمية كافية");

                                                    dbconnection.Close();
                                                    dbconnection1.Close();
                                                    dbconnection2.Close();
                                                    dbconnection3.Close();
                                                    dbconnection4.Close();
                                                    return;
                                                }
                                            }
                                            else
                                            {
                                                MessageBox.Show("لا يوجد كمية كافية");
                                                dbconnection.Close();
                                                dbconnection1.Close();
                                                dbconnection2.Close();
                                                dbconnection3.Close();
                                                dbconnection4.Close();
                                                return;
                                            }
                                        }
                                        dr3.Close();
                                    }
                                    dr2.Close();

                                    string query2 = "delete from storage_import_permission where StorageImportPermission_ID=" + StorageImportPermissionID;
                                    MySqlCommand com4 = new MySqlCommand(query2, dbconnection);
                                    com4.ExecuteNonQuery();

                                    gridView1.DeleteSelectedRows();
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show("هذا الاذن منتهى");
                        }
                    }
                    else
                    {
                        MessageBox.Show("يجب التاكد من البيانات");
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
                dbconnection4.Close();
            }
        }

        public bool checkQuantity()
        {
            string q2 = "SELECT DISTINCT supplier.Supplier_Name as 'المورد',import_supplier_permission.Supplier_Permission_Number as 'اذن استلام',import_supplier_permission.Order_Number as 'رقم الطلب',import_supplier_permission.ImportSupplierPermission_ID as 'ID',import_supplier_permission.Factory_ID FROM storage_import_permission INNER JOIN import_supplier_permission ON import_supplier_permission.StorageImportPermission_ID = storage_import_permission.StorageImportPermission_ID INNER JOIN supplier_permission_details ON supplier_permission_details.ImportSupplierPermission_ID = import_supplier_permission.ImportSupplierPermission_ID INNER JOIN store ON store.Store_ID = storage_import_permission.Store_ID INNER JOIN supplier ON supplier.Supplier_ID = import_supplier_permission.Supplier_ID left JOIN store_places ON store_places.Store_Place_ID = supplier_permission_details.Store_Place_ID where storage_import_permission.Import_Permission_Number=" + txtPermissionNumber.Text + " and supplier_permission_details.Store_ID=" + comStore.SelectedValue.ToString() + " and storage_import_permission.StorageImportPermission_ID=" + StorageImportPermissionID;
            MySqlCommand com2 = new MySqlCommand(q2, dbconnection2);
            MySqlDataReader dr2 = com2.ExecuteReader();
            while (dr2.Read())
            {
                string q3 = "select storage_import_permission.StorageImportPermission_ID as 'التسلسل',data.Data_ID,data.Code as 'الكود',type.Type_Name as 'النوع',concat(product.Product_Name,' ',COALESCE(color.Color_Name,''),' ',data.Description,' ',groupo.Group_Name,' ',factory.Factory_Name,' ',COALESCE(size.Size_Value,''),' ',COALESCE(sort.Sort_Value,'')) as 'الاسم',supplier_permission_details.Balatat as 'عدد البلتات',supplier_permission_details.Carton_Balata as 'عدد الكراتين',supplier_permission_details.Total_Meters as 'متر/قطعة',DATE_FORMAT(supplier_permission_details.Date, '%d-%m-%Y %T') as 'تاريخ التخزين',supplier_permission_details.Note as 'ملاحظة',supplier_permission_details.ImportSupplierPermission_ID as 'ID' from supplier_permission_details INNER JOIN data ON supplier_permission_details.Data_ID = data.Data_ID INNER JOIN import_supplier_permission ON supplier_permission_details.ImportSupplierPermission_ID = import_supplier_permission.ImportSupplierPermission_ID INNER JOIN storage_import_permission ON storage_import_permission.StorageImportPermission_ID = import_supplier_permission.StorageImportPermission_ID left JOIN store_places ON store_places.Store_Place_ID = supplier_permission_details.Store_Place_ID INNER JOIN supplier ON import_supplier_permission.Supplier_ID = supplier.Supplier_ID INNER JOIN type ON type.Type_ID = data.Type_ID INNER JOIN product ON product.Product_ID = data.Product_ID INNER JOIN factory ON data.Factory_ID = factory.Factory_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID LEFT JOIN color ON color.Color_ID = data.Color_ID LEFT JOIN size ON size.Size_ID = data.Size_ID LEFT JOIN sort ON sort.Sort_ID = data.Sort_ID where storage_import_permission.Import_Permission_Number=" + txtPermissionNumber.Text + " and supplier_permission_details.Store_ID=" + comStore.SelectedValue.ToString() + " and import_supplier_permission.ImportSupplierPermission_ID=" + dr2["ID"].ToString();
                MySqlCommand com3 = new MySqlCommand(q3, dbconnection3);
                MySqlDataReader dr3 = com3.ExecuteReader();
                while (dr3.Read())
                {
                    string query = "select Total_Meters from storage where Store_ID=" + comStore.SelectedValue.ToString() + " and Data_ID=" + dr3["Data_ID"].ToString();
                    MySqlCommand com = new MySqlCommand(query, dbconnection);
                    if (com.ExecuteScalar() != null)
                    {
                        double totalf = Convert.ToInt32(com.ExecuteScalar());
                        if ((totalf - Convert.ToDouble(dr3["متر/قطعة"].ToString())) >= 0)
                        {
                        }
                        else
                        {
                            MessageBox.Show("لا يوجد كمية كافية");

                            dbconnection.Close();
                            dbconnection1.Close();
                            dbconnection2.Close();
                            dbconnection3.Close();
                            dbconnection4.Close();
                            return false;
                        }
                    }
                    else
                    {
                        MessageBox.Show("لا يوجد كمية كافية");
                        dbconnection.Close();
                        dbconnection1.Close();
                        dbconnection2.Close();
                        dbconnection3.Close();
                        dbconnection4.Close();
                        return false;
                    }
                }
                dr3.Close();
            }
            dr2.Close();

            return true;
        }

        public void refreshView(int rowHandel, double quantity, int CartonNum)
        {
            gridView1.SetRowCellValue(rowHandel, "متر/قطعة", quantity);
            gridView1.SetRowCellValue(rowHandel, "عدد الكراتين", CartonNum);
        }
    }
}
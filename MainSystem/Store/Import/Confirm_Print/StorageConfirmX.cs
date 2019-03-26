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
    public partial class StorageConfirm : Form
    {
        MySqlConnection dbconnection, dbconnection3;
        
        int storeId = 0;
        bool loaded = false;
        int ReturnedPermissionNumber = 0;
        int StorageImportPermissionID = 0;
        int flagConfirm = 2;

        public StorageConfirm(MainForm SalesMainForm, DevExpress.XtraTab.XtraTabControl TabControlStores)
        {
            InitializeComponent();
            dbconnection = new MySqlConnection(connection.connectionString);
            dbconnection3 = new MySqlConnection(connection.connectionString);
            comSupplier.AutoCompleteMode = AutoCompleteMode.Suggest;
            comSupplier.AutoCompleteSource = AutoCompleteSource.ListItems;
        }

        private void StorageReturnBill_Load(object sender, EventArgs e)
        {
            try
            {
                string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"Store.txt");
                storeId = Convert.ToInt16(System.IO.File.ReadAllText(path));

                string query = "select * from supplier";
                MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                DataTable dt = new DataTable();
                da.Fill(dt);
                comSupplier.DataSource = dt;
                comSupplier.DisplayMember = dt.Columns["Supplier_Name"].ToString();
                comSupplier.ValueMember = dt.Columns["Supplier_ID"].ToString();
                comSupplier.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txtPermissionNum_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtPermissionNum.Text != "")
                {
                    dbconnection.Close();

                    search();

                    dbconnection.Open();
                    string qq = "select StorageImportPermission_ID from storage_import_permission where Store_ID=" + storeId + " and Import_Permission_Number=" + txtPermissionNum.Text;
                    MySqlCommand com3 = new MySqlCommand(qq, dbconnection);
                    StorageImportPermissionID = 0;
                    if (com3.ExecuteScalar() != null)
                    {
                        StorageImportPermissionID = int.Parse(com3.ExecuteScalar().ToString());
                    }

                    qq = "select Returned_Permission_Number from import_storage_return where Store_ID=" + storeId + " and Import_Permission_Number=" + txtPermissionNum.Text;
                    com3 = new MySqlCommand(qq, dbconnection);
                    ReturnedPermissionNumber = 0;
                    if (com3.ExecuteScalar() != null)
                    {
                        ReturnedPermissionNumber = int.Parse(com3.ExecuteScalar().ToString());
                    }


                    string q = "select import_supplier_permission.Supplier_ID from import_supplier_permission INNER JOIN storage_import_permission ON storage_import_permission.StorageImportPermission_ID = import_supplier_permission.StorageImportPermission_ID where storage_import_permission.Import_Permission_Number=" + txtPermissionNum.Text + " and storage_import_permission.Store_ID=" + storeId;
                    MySqlCommand com = new MySqlCommand(q, dbconnection);
                    loaded = false;
                    if (com.ExecuteScalar() != null)
                    {
                        comSupplier.SelectedValue = com.ExecuteScalar().ToString();
                        comSupplier.Enabled = false;
                        comSupplier.DropDownStyle = ComboBoxStyle.DropDownList;
                    }
                    else
                    {
                        comSupplier.SelectedIndex = -1;
                        comSupplier.Enabled = true;
                        comSupplier.DropDownStyle = ComboBoxStyle.DropDown;
                    }

                    q = "select storage_import_permission.Confirmed from storage_import_permission where storage_import_permission.Import_Permission_Number=" + txtPermissionNum.Text + " and storage_import_permission.Store_ID=" + storeId;
                    com = new MySqlCommand(q, dbconnection);
                    if (com.ExecuteScalar() != null)
                    {
                        flagConfirm = Convert.ToInt16(com.ExecuteScalar().ToString());
                    }
                    else
                    {
                        flagConfirm = 2;
                    }

                    loaded = true;
                }
                else
                {
                    gridControl1.DataSource = null;
                    gridControl2.DataSource = null;
                    flagConfirm = 2;

                    loaded = false;
                    comSupplier.SelectedIndex = -1;
                    comSupplier.Enabled = true;
                    comSupplier.DropDownStyle = ComboBoxStyle.DropDown;
                    loaded = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
            dbconnection3.Close();
        }
        
        public void search()
        {
            string qq = "select data.Data_ID,data.Code as 'الكود',concat(product.Product_Name,' - ',type.Type_Name,' - ',factory.Factory_Name,' - ',groupo.Group_Name,' ',COALESCE(color.Color_Name,''),' ',COALESCE(size.Size_Value,''),' ',COALESCE(sort.Sort_Value,'')) as 'الاسم',import_supplier_permission.Supplier_Permission_Number as 'اذن استلام',(supplier_permission_details.Balatat-ifnull(import_storage_return_details.Balatat,0)) as 'عدد البلتات',(supplier_permission_details.Carton_Balata-ifnull(import_storage_return_details.Carton_Balata,0)) as 'عدد الكراتين',(supplier_permission_details.Total_Meters-ifnull(import_storage_return_details.Total_Meters,0)) as 'متر/قطعة',DATE_FORMAT(supplier_permission_details.Date, '%d-%m-%Y %T') as 'تاريخ التخزين',store_places.Store_Place_Code as 'مكان التخزين',supplier_permission_details.Note as 'ملاحظة',data.Carton,import_supplier_permission.Supplier_ID,supplier_permission_details.Supplier_Permission_Details_ID,supplier_permission_details.Store_Place_ID,storage_import_permission.StorageImportPermission_ID from supplier_permission_details INNER JOIN data ON supplier_permission_details.Data_ID = data.Data_ID INNER JOIN import_supplier_permission ON supplier_permission_details.ImportSupplierPermission_ID = import_supplier_permission.ImportSupplierPermission_ID INNER JOIN storage_import_permission ON storage_import_permission.StorageImportPermission_ID = import_supplier_permission.StorageImportPermission_ID LEFT JOIN import_storage_return ON import_storage_return.StorageImportPermission_ID = storage_import_permission.StorageImportPermission_ID LEFT JOIN import_storage_return_supplier ON import_storage_return_supplier.ImportStorageReturn_ID = import_storage_return.ImportStorageReturn_ID LEFT JOIN import_storage_return_details ON import_storage_return_details.ImportStorageReturnSupplier_ID = import_storage_return_supplier.ImportStorageReturnSupplier_ID and supplier_permission_details.Data_ID = import_storage_return_details.Data_ID INNER JOIN store_places ON store_places.Store_Place_ID = supplier_permission_details.Store_Place_ID INNER JOIN type ON type.Type_ID = data.Type_ID INNER JOIN product ON product.Product_ID = data.Product_ID INNER JOIN factory ON data.Factory_ID = factory.Factory_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID LEFT JOIN color ON color.Color_ID = data.Color_ID LEFT JOIN size ON size.Size_ID = data.Size_ID LEFT JOIN sort ON sort.Sort_ID = data.Sort_ID where storage_import_permission.Import_Permission_Number=0 and supplier_permission_details.Store_ID=0";
            MySqlDataAdapter da = new MySqlDataAdapter(qq, dbconnection);
            DataTable dt = new DataTable();
            da.Fill(dt);
            gridControl1.DataSource = dt;
            
            dbconnection.Open();
            
            qq = "select data.Data_ID,data.Code as 'الكود',concat(product.Product_Name,' - ',type.Type_Name,' - ',factory.Factory_Name,' - ',groupo.Group_Name,' ',COALESCE(color.Color_Name,''),' ',COALESCE(size.Size_Value,''),' ',COALESCE(sort.Sort_Value,'')) as 'الاسم',import_supplier_permission.Supplier_Permission_Number as 'اذن استلام',supplier_permission_details.Balatat as 'عدد البلتات',supplier_permission_details.Carton_Balata as 'عدد الكراتين',supplier_permission_details.Total_Meters as 'متر/قطعة',DATE_FORMAT(supplier_permission_details.Date, '%d-%m-%Y %T') as 'تاريخ التخزين',store_places.Store_Place_Code as 'مكان التخزين',supplier_permission_details.Note as 'ملاحظة',data.Carton,import_supplier_permission.Supplier_ID,supplier_permission_details.Supplier_Permission_Details_ID,supplier_permission_details.Store_Place_ID,storage_import_permission.StorageImportPermission_ID from supplier_permission_details INNER JOIN data ON supplier_permission_details.Data_ID = data.Data_ID INNER JOIN import_supplier_permission ON supplier_permission_details.ImportSupplierPermission_ID = import_supplier_permission.ImportSupplierPermission_ID INNER JOIN storage_import_permission ON storage_import_permission.StorageImportPermission_ID = import_supplier_permission.StorageImportPermission_ID INNER JOIN store_places ON store_places.Store_Place_ID = supplier_permission_details.Store_Place_ID INNER JOIN type ON type.Type_ID = data.Type_ID INNER JOIN product ON product.Product_ID = data.Product_ID INNER JOIN factory ON data.Factory_ID = factory.Factory_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID LEFT JOIN color ON color.Color_ID = data.Color_ID LEFT JOIN size ON size.Size_ID = data.Size_ID LEFT JOIN sort ON sort.Sort_ID = data.Sort_ID where storage_import_permission.Import_Permission_Number=" + txtPermissionNum.Text + " and supplier_permission_details.Store_ID=" + storeId;
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
                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns["الاسم"], dr["الاسم"].ToString());
                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns["اذن استلام"], dr["اذن استلام"].ToString());
                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns["تاريخ التخزين"], dr["تاريخ التخزين"].ToString());
                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns["مكان التخزين"], dr["مكان التخزين"].ToString());
                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns["ملاحظة"], dr["ملاحظة"].ToString());
                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns["Carton"], dr["Carton"].ToString());
                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns["Supplier_ID"], dr["Supplier_ID"].ToString());
                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns["Supplier_Permission_Details_ID"], dr["Supplier_Permission_Details_ID"].ToString());
                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns["Store_Place_ID"], dr["Store_Place_ID"].ToString());
                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns["StorageImportPermission_ID"], dr["StorageImportPermission_ID"].ToString());
                    if (dr["عدد البلتات"].ToString() != "")
                    {
                        noBalat = Convert.ToInt16(dr["عدد البلتات"].ToString());
                    }
                    if (dr["عدد الكراتين"].ToString() != "")
                    {
                        noCarton = Convert.ToInt16(dr["عدد الكراتين"].ToString());
                    }
                    if (dr["متر/قطعة"].ToString() != "")
                    {
                        totalMeter = Convert.ToDouble(dr["متر/قطعة"].ToString());
                    }
                    
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
            gridView1.Columns["Carton"].Visible = false;
            gridView1.Columns["Supplier_Permission_Details_ID"].Visible = false;
            gridView1.Columns["Store_Place_ID"].Visible = false;
            gridView1.Columns["StorageImportPermission_ID"].Visible = false;
            if (gridView1.IsLastVisibleRow)
            {
                gridView1.FocusedRowHandle = gridView1.RowCount - 1;
            }
            
            qq = "select data.Data_ID,data.Code as 'الكود',concat(product.Product_Name,' - ',type.Type_Name,' - ',factory.Factory_Name,' - ',groupo.Group_Name,' ',COALESCE(color.Color_Name,''),' ',COALESCE(size.Size_Value,''),' ',COALESCE(sort.Sort_Value,'')) as 'الاسم',import_storage_return_supplier.Supplier_Permission_Number as 'اذن استلام',import_storage_return_details.Balatat as 'عدد البلتات',import_storage_return_details.Carton_Balata as 'عدد الكراتين',import_storage_return_details.Total_Meters as 'متر/قطعة',DATE_FORMAT(import_storage_return_details.Date, '%d-%m-%Y %T') as 'التاريخ',store_places.Store_Place_Code as 'المكان',import_storage_return_details.Reason as 'السبب',import_storage_return_supplier.Supplier_ID,import_storage_return_details.ImportStorageReturnDetails_ID,import_storage_return_details.Store_Place_ID from import_storage_return_details INNER JOIN data ON import_storage_return_details.Data_ID = data.Data_ID INNER JOIN import_storage_return_supplier ON import_storage_return_details.ImportStorageReturnSupplier_ID = import_storage_return_supplier.ImportStorageReturnSupplier_ID INNER JOIN import_storage_return ON import_storage_return.ImportStorageReturn_ID = import_storage_return_supplier.ImportStorageReturn_ID INNER JOIN store_places ON store_places.Store_Place_ID = import_storage_return_details.Store_Place_ID INNER JOIN type ON type.Type_ID = data.Type_ID INNER JOIN product ON product.Product_ID = data.Product_ID INNER JOIN factory ON data.Factory_ID = factory.Factory_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID LEFT JOIN color ON color.Color_ID = data.Color_ID LEFT JOIN size ON size.Size_ID = data.Size_ID LEFT JOIN sort ON sort.Sort_ID = data.Sort_ID where import_storage_return.Import_Permission_Number=" + txtPermissionNum.Text + " and import_storage_return_details.Store_ID=" + storeId;
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
            }
            dbconnection.Close();
        }
        
        private void btnReport_Click(object sender, EventArgs e)
        {
            try
            {
                if (flagConfirm == 2)
                {
                    if (comSupplier.SelectedValue != null && txtPermissionNum.Text != "" && gridView1.RowCount > 0)
                    {
                        dbconnection.Open();
                        string query = "select Store_Name from store where Store_ID=" + storeId;
                        MySqlCommand com = new MySqlCommand(query, dbconnection);
                        string storeName = com.ExecuteScalar().ToString();
                        
                        double carton = 0;
                        double balate = 0;
                        double quantity = 0;

                        List<SupplierReceipt_Items> bi = new List<SupplierReceipt_Items>();
                        for (int i = 0; i < gridView1.RowCount; i++)
                        {
                            if (gridView1.GetRowCellDisplayText(i, gridView1.Columns["عدد البلتات"]) != "")
                            {
                                balate = Convert.ToDouble(gridView1.GetRowCellDisplayText(i, gridView1.Columns["عدد البلتات"]));
                            }
                            if (gridView1.GetRowCellDisplayText(i, gridView1.Columns["عدد الكراتين"]) != "")
                            {
                                carton = Convert.ToDouble(gridView1.GetRowCellDisplayText(i, gridView1.Columns["عدد الكراتين"]));
                            }
                            if (gridView1.GetRowCellDisplayText(i, gridView1.Columns["متر/قطعة"]) != "")
                            {
                                quantity = Convert.ToDouble(gridView1.GetRowCellDisplayText(i, gridView1.Columns["متر/قطعة"]));
                            }

                            SupplierReceipt_Items item = new SupplierReceipt_Items() { Code = gridView1.GetRowCellDisplayText(i, gridView1.Columns["الكود"]), Product_Name = gridView1.GetRowCellDisplayText(i, gridView1.Columns["الاسم"]), Balatat = balate, Carton_Balata = carton, Total_Meters = quantity, Supplier_Permission_Number = Convert.ToInt16(gridView1.GetRowCellDisplayText(i, gridView1.Columns["اذن استلام"])), Date = Convert.ToDateTime(gridView1.GetRowCellDisplayText(i, gridView1.Columns["تاريخ التخزين"])).ToString("yyyy-MM-dd hh:mm:ss"), Note = gridView1.GetRowCellDisplayText(i, gridView1.Columns["ملاحظة"]) };
                            bi.Add(item);
                        }

                        Report_SupplierReceipt f = new Report_SupplierReceipt();
                        f.PrintInvoice(storeName, txtPermissionNum.Text, comSupplier.Text, bi);
                        f.ShowDialog();

                        if (gridView2.RowCount > 0)
                        {
                            carton = 0;
                            balate = 0;
                            quantity = 0;

                            List<StorageReturn_Items> bi2 = new List<StorageReturn_Items>();
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

                                StorageReturn_Items item = new StorageReturn_Items() { Code = gridView2.GetRowCellDisplayText(i, gridView2.Columns["الكود"]), Product_Name = gridView2.GetRowCellDisplayText(i, gridView2.Columns["الاسم"]), Balatat = balate, Carton_Balata = carton, Total_Meters = quantity, Supplier_Permission_Number = Convert.ToInt16(gridView2.GetRowCellDisplayText(i, gridView2.Columns["اذن استلام"])), Date = Convert.ToDateTime(gridView2.GetRowCellDisplayText(i, gridView2.Columns["التاريخ"])).ToString("yyyy-MM-dd hh:mm:ss"), Reason = gridView2.GetRowCellDisplayText(i, gridView2.Columns["السبب"]) };
                                bi2.Add(item);
                            }

                            Report_StorageReturn f2 = new Report_StorageReturn();
                            f2.PrintInvoice(storeName, txtPermissionNum.Text, comSupplier.Text, ReturnedPermissionNumber, bi2);
                            f2.ShowDialog();
                        }

                        string q = "update storage_import_permission set Confirmed=0 where StorageImportPermission_ID=" + StorageImportPermissionID;
                        MySqlCommand c = new MySqlCommand(q, dbconnection);
                        c.ExecuteNonQuery();
                        flagConfirm = 0;
                    }
                    else
                    {
                        MessageBox.Show("يجب ادخال البيانات كاملة");
                    }
                }
                else
                {
                    MessageBox.Show("هذا الاذن منتهى");
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

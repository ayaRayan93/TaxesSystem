using DevExpress.XtraGrid.Views.Grid;
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
    public partial class Permissions_Transitions_Report : Form
    {
        MySqlConnection dbconnection;
        bool loaded = false;

        public Permissions_Transitions_Report(MainForm mainform)
        {
            InitializeComponent();
            dbconnection = new MySqlConnection(connection.connectionString);
        }
        private void requestStored_Load(object sender, EventArgs e)
        {
            try
            {
                DataHelperClassPermissionsTransitions dh = new DataHelperClassPermissionsTransitions(DSparametrPermissionsTransitions.doubleDS);
                gridControl1.DataSource = dh.DataSet;
                gridControl1.DataMember = dh.DataMember;
                gridView1.InitNewRow += GridView1_InitNewRow;

                string query = "select * from supplier";
                MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                DataTable dt = new DataTable();
                da.Fill(dt);
                comSupplier.DataSource = dt;
                comSupplier.DisplayMember = dt.Columns["Supplier_Name"].ToString();
                comSupplier.ValueMember = dt.Columns["Supplier_ID"].ToString();
                comSupplier.Text = "";
                txtSupplierID.Text = "";

                loaded = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }

        private void GridView1_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            GridView view = sender as GridView;
            view.SetRowCellValue(e.RowHandle, gridView1.Columns[0], "");
            view.SetRowCellValue(e.RowHandle, gridView1.Columns[1], "");
            view.SetRowCellValue(e.RowHandle, gridView1.Columns[2], "");
            view.SetRowCellValue(e.RowHandle, gridView1.Columns[3], "");
            view.SetRowCellValue(e.RowHandle, gridView1.Columns[4], "");
            view.SetRowCellValue(e.RowHandle, gridView1.Columns[5], "");
        }

        private void comBranch_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (loaded)
                {
                    txtSupplierID.Text = comSupplier.SelectedValue.ToString();
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
                    string query = "select Supplier_Name from supplier where Supplier_ID=" + txtSupplierID.Text + "";
                    MySqlCommand com = new MySqlCommand(query, dbconnection);
                    if (com.ExecuteScalar() != null)
                    {
                        Name = (string)com.ExecuteScalar();
                        comSupplier.Text = Name;
                        comSupplier.SelectedValue = txtSupplierID.Text;
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
        
        private void btnDisplay_Click(object sender, EventArgs e)
        {
            try
            {
                int storeID = 0;
                if (int.TryParse(txtSupplierID.Text, out storeID) && comSupplier.SelectedValue != null)
                {
                    while (gridView1.RowCount != 0)
                    {
                        gridView1.SelectAll();
                        gridView1.DeleteSelectedRows();
                    }

                    dbconnection.Open();
                    string query = "SELECT distinct storage_import_permission.StorageImportPermission_ID as 'ID', storage_import_permission.Import_Permission_Number as 'رقم الاذن',store.Store_Name as 'المخزن',supplier.Supplier_Name as 'المورد',storage_import_permission.Storage_Date as 'التاريخ' FROM storage_import_permission INNER JOIN import_supplier_permission ON storage_import_permission.StorageImportPermission_ID = import_supplier_permission.StorageImportPermission_ID INNER JOIN store ON store.Store_ID = storage_import_permission.Store_ID INNER JOIN supplier ON supplier.Supplier_ID = import_supplier_permission.Supplier_ID where (storage_import_permission.Confirmed=0 or storage_import_permission.Confirmed=1) and import_supplier_permission.Supplier_ID=" + comSupplier.SelectedValue.ToString() + " and date(storage_import_permission.Storage_Date) between '" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "' and '" + dateTimePicker2.Value.ToString("yyyy-MM-dd") + "' order by storage_import_permission.Storage_Date";
                    MySqlCommand comand = new MySqlCommand(query, dbconnection);
                    MySqlDataReader dr = comand.ExecuteReader();
                    while (dr.Read())
                    {
                        gridView1.AddNewRow();
                        int rowHandle = gridView1.GetRowHandle(gridView1.DataRowCount);
                        if (gridView1.IsNewItemRow(rowHandle))
                        {
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns["ColID"], dr["ID"].ToString());
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns["ColType"], "وارد");
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns["ColBill"], dr["رقم الاذن"].ToString());
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns["ColStoreName"], dr["المخزن"].ToString());
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns["ColSupplierName"], dr["المورد"].ToString());
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns["ColDate"], dr["التاريخ"].ToString());
                        }
                    }
                    dr.Close();

                    query = "SELECT distinct import_storage_return.ImportStorageReturn_ID as 'ID', import_storage_return.Returned_Permission_Number as 'رقم الاذن',store.Store_Name as 'المخزن',supplier.Supplier_Name as 'المورد',import_storage_return.Retrieval_Date as 'التاريخ' FROM import_storage_return_supplier INNER JOIN import_storage_return ON import_storage_return_supplier.ImportStorageReturn_ID = import_storage_return.ImportStorageReturn_ID INNER JOIN store ON store.Store_ID = import_storage_return.Store_ID INNER JOIN supplier ON supplier.Supplier_ID = import_storage_return_supplier.Supplier_ID where import_storage_return_supplier.Supplier_ID=" + comSupplier.SelectedValue.ToString() + " and date(import_storage_return.Retrieval_Date) between '" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "' and '" + dateTimePicker2.Value.ToString("yyyy-MM-dd") + "' order by import_storage_return.Retrieval_Date";
                    comand = new MySqlCommand(query, dbconnection);
                    dr = comand.ExecuteReader();
                    while (dr.Read())
                    {
                        gridView1.AddNewRow();
                        int rowHandle = gridView1.GetRowHandle(gridView1.DataRowCount);
                        if (gridView1.IsNewItemRow(rowHandle))
                        {
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns["ColID"], dr["ID"].ToString());
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns["ColType"], "راجع");
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns["ColBill"], dr["رقم الاذن"].ToString());
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns["ColStoreName"], dr["المخزن"].ToString());
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns["ColSupplierName"], dr["المورد"].ToString());
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns["ColDate"], dr["التاريخ"].ToString());
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
                    while (gridView1.RowCount != 0)
                    {
                        gridView1.SelectAll();
                        gridView1.DeleteSelectedRows();
                    }

                    dbconnection.Open();
                    string query = "SELECT distinct storage_import_permission.StorageImportPermission_ID as 'ID', storage_import_permission.Import_Permission_Number as 'رقم الاذن',store.Store_Name as 'المخزن',supplier.Supplier_Name as 'المورد',storage_import_permission.Storage_Date as 'التاريخ' FROM storage_import_permission INNER JOIN import_supplier_permission ON storage_import_permission.StorageImportPermission_ID = import_supplier_permission.StorageImportPermission_ID INNER JOIN store ON store.Store_ID = storage_import_permission.Store_ID INNER JOIN supplier ON supplier.Supplier_ID = import_supplier_permission.Supplier_ID where (storage_import_permission.Confirmed=0 or storage_import_permission.Confirmed=1) and date(storage_import_permission.Storage_Date) between '" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "' and '" + dateTimePicker2.Value.ToString("yyyy-MM-dd") + "' order by storage_import_permission.Storage_Date";
                    MySqlCommand comand = new MySqlCommand(query, dbconnection);
                    MySqlDataReader dr = comand.ExecuteReader();
                    while (dr.Read())
                    {
                        gridView1.AddNewRow();
                        int rowHandle = gridView1.GetRowHandle(gridView1.DataRowCount);
                        if (gridView1.IsNewItemRow(rowHandle))
                        {
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns["ColID"], dr["ID"].ToString());
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns["ColType"], "وارد");
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns["ColBill"], dr["رقم الاذن"].ToString());
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns["ColStoreName"], dr["المخزن"].ToString());
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns["ColSupplierName"], dr["المورد"].ToString());
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns["ColDate"], dr["التاريخ"].ToString());
                        }
                    }
                    dr.Close();

                    query = "SELECT distinct import_storage_return.ImportStorageReturn_ID as 'ID', import_storage_return.Returned_Permission_Number as 'رقم الاذن',store.Store_Name as 'المخزن',supplier.Supplier_Name as 'المورد',import_storage_return.Retrieval_Date as 'التاريخ' FROM import_storage_return_supplier INNER JOIN import_storage_return ON import_storage_return_supplier.ImportStorageReturn_ID = import_storage_return.ImportStorageReturn_ID INNER JOIN store ON store.Store_ID = import_storage_return.Store_ID INNER JOIN supplier ON supplier.Supplier_ID = import_storage_return_supplier.Supplier_ID where date(import_storage_return.Retrieval_Date) between '" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "' and '" + dateTimePicker2.Value.ToString("yyyy-MM-dd") + "' order by import_storage_return.Retrieval_Date";
                    comand = new MySqlCommand(query, dbconnection);
                    dr = comand.ExecuteReader();
                    while (dr.Read())
                    {
                        gridView1.AddNewRow();
                        int rowHandle = gridView1.GetRowHandle(gridView1.DataRowCount);
                        if (gridView1.IsNewItemRow(rowHandle))
                        {
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns["ColID"], dr["ID"].ToString());
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns["ColType"], "راجع");
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns["ColBill"], dr["رقم الاذن"].ToString());
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns["ColStoreName"], dr["المخزن"].ToString());
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns["ColSupplierName"], dr["المورد"].ToString());
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns["ColDate"], dr["التاريخ"].ToString());
                        }
                    }
                    dr.Close();

                    if (gridView1.IsLastVisibleRow)
                    {
                        gridView1.FocusedRowHandle = gridView1.RowCount - 1;
                    }
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
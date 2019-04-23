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

namespace MainSystem
{
    public partial class SupplierBills_Report : Form
    {
        MySqlConnection dbconnection, dbconnection6;
        bool loaded = false;
        XtraTabControl tabControlContent;

        public SupplierBills_Report(MainForm mainform, XtraTabControl TabControlContent)
        {
            InitializeComponent();
            dbconnection = new MySqlConnection(connection.connectionString);
            dbconnection6 = new MySqlConnection(connection.connectionString);
            tabControlContent = TabControlContent;
        }
        private void requestStored_Load(object sender, EventArgs e)
        {
            try
            {
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
        
        private void comBranch_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (loaded)
                {
                    int supplierID = 0;
                    txtSupplierID.Text = comSupplier.SelectedValue.ToString();
                    if (int.TryParse(txtSupplierID.Text, out supplierID) && comSupplier.SelectedValue != null)
                    {
                        search(Convert.ToInt16(comSupplier.SelectedValue.ToString()));
                    }
                    else
                    {
                        MessageBox.Show("تاكد من البيانات");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
            dbconnection6.Close();
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
                        search(Convert.ToInt16(comSupplier.SelectedValue.ToString()));
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
                dbconnection6.Close();
            }
        }

        private void btnDisplay_Click(object sender, EventArgs e)
        {
            try
            {
                loaded = false;
                comSupplier.SelectedIndex = -1;
                txtSupplierID.Text = "";
                loaded = true;
                search(0);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
            dbconnection6.Close();
        }

        public void search(int supplierId)
        {
            double totalBills = 0;
            double TotalReturns = 0;

            MySqlDataAdapter adapter = new MySqlDataAdapter("SELECT supplier_bill.Bill_ID as 'التسلسل','النوع',supplier.Supplier_Name as 'المورد',supplier_bill.Bill_No as 'رقم الفاتورة',store.Store_Name as 'المخزن',supplier_bill.Date as 'التاريخ',supplier_bill.Total_Price_B as 'الاجمالى قبل',supplier_bill.Total_Price_A as 'الاجمالى بعد' FROM supplier_bill INNER JOIN supplier_bill_details ON supplier_bill_details.Bill_ID = supplier_bill.Bill_ID INNER JOIN store ON store.Store_ID = supplier_bill.Store_ID INNER JOIN supplier ON supplier.Supplier_ID = supplier_bill.Supplier_ID where supplier_bill.Bill_ID=0", dbconnection);
            DataTable dtf = new DataTable();
            adapter.Fill(dtf);
            gridControl1.DataSource = dtf;

            dbconnection.Open();
            dbconnection6.Open();
            if (supplierId == 0)
            {
                string query = "SELECT supplier_bill.Bill_ID as 'التسلسل',supplier.Supplier_Name as 'المورد',supplier_bill.Bill_No as 'رقم الفاتورة',supplier_bill.Date as 'التاريخ',supplier_bill.Total_Price_B as 'الاجمالى قبل',supplier_bill.Total_Price_A as 'الاجمالى بعد',store.Store_Name as 'المخزن',supplier_bill.StorageImportPermission_ID FROM supplier_bill INNER JOIN supplier_bill_details ON supplier_bill_details.Bill_ID = supplier_bill.Bill_ID INNER JOIN store ON store.Store_ID = supplier_bill.Store_ID INNER JOIN supplier ON supplier.Supplier_ID = supplier_bill.Supplier_ID";
                MySqlCommand comand = new MySqlCommand(query, dbconnection);
                MySqlDataReader dr = comand.ExecuteReader();
                while (dr.Read())
                {
                    gridView1.AddNewRow();
                    int rowHandle = gridView1.GetRowHandle(gridView1.DataRowCount);
                    if (gridView1.IsNewItemRow(rowHandle))
                    {
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[0], dr["التسلسل"]);
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns["المورد"], dr["المورد"]);
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns["النوع"], "شراء");
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns["رقم الفاتورة"], dr["رقم الفاتورة"]);
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns["التاريخ"], dr["التاريخ"]);
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns["الاجمالى قبل"], dr["الاجمالى قبل"]);
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns["الاجمالى بعد"], dr["الاجمالى بعد"]);
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns["المخزن"], dr["المخزن"]);

                        totalBills += Convert.ToDouble(dr["الاجمالى بعد"]);
                    }

                    string q = "SELECT supplier_return_bill.ReturnBill_ID as 'التسلسل',supplier.Supplier_Name as 'المورد',supplier_return_bill.Return_Bill_No as 'رقم الفاتورة',supplier_return_bill.Date as 'التاريخ',supplier_return_bill.Total_Price_BD as 'الاجمالى قبل',supplier_return_bill.Total_Price_AD as 'الاجمالى بعد',store.Store_Name as 'المخزن' FROM supplier_return_bill INNER JOIN import_storage_return ON supplier_return_bill.ImportStorageReturn_ID = import_storage_return.ImportStorageReturn_ID INNER JOIN supplier_bill ON supplier_bill.StorageImportPermission_ID = import_storage_return.StorageImportPermission_ID INNER JOIN supplier_return_bill_details ON supplier_return_bill_details.ReturnBill_ID = supplier_return_bill.ReturnBill_ID INNER JOIN store ON store.Store_ID = supplier_return_bill.Store_ID INNER JOIN supplier ON supplier.Supplier_ID = supplier_return_bill.Supplier_ID where supplier_bill.StorageImportPermission_ID=" + dr["StorageImportPermission_ID"];
                    MySqlCommand comand2 = new MySqlCommand(q, dbconnection6);
                    MySqlDataReader dr2 = comand2.ExecuteReader();
                    while (dr2.Read())
                    {
                        gridView1.AddNewRow();
                        rowHandle = gridView1.GetRowHandle(gridView1.DataRowCount);
                        if (gridView1.IsNewItemRow(rowHandle))
                        {
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns[0], dr2["التسلسل"]);
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns["المورد"], dr2["المورد"]);
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns["النوع"], "مرتجع");
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns["رقم الفاتورة"], dr2["رقم الفاتورة"]);
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns["التاريخ"], dr2["التاريخ"]);
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns["الاجمالى قبل"], dr2["الاجمالى قبل"]);
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns["الاجمالى بعد"], dr2["الاجمالى بعد"]);
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns["المخزن"], dr2["المخزن"]);

                            TotalReturns += Convert.ToDouble(dr2["الاجمالى بعد"]);
                        }
                    }
                    dr2.Close();
                }
                dr.Close();
            }
            else
            {
                string query = "SELECT supplier_bill.Bill_ID as 'التسلسل',supplier.Supplier_Name as 'المورد',supplier_bill.Bill_No as 'رقم الفاتورة',supplier_bill.Date as 'التاريخ',supplier_bill.Total_Price_B as 'الاجمالى قبل',supplier_bill.Total_Price_A as 'الاجمالى بعد',store.Store_Name as 'المخزن',supplier_bill.StorageImportPermission_ID FROM supplier_bill INNER JOIN supplier_bill_details ON supplier_bill_details.Bill_ID = supplier_bill.Bill_ID INNER JOIN store ON store.Store_ID = supplier_bill.Store_ID INNER JOIN supplier ON supplier.Supplier_ID = supplier_bill.Supplier_ID where supplier_bill.Supplier_ID=" + supplierId;
                MySqlCommand comand = new MySqlCommand(query, dbconnection);
                MySqlDataReader dr = comand.ExecuteReader();
                while (dr.Read())
                {
                    gridView1.AddNewRow();
                    int rowHandle = gridView1.GetRowHandle(gridView1.DataRowCount);
                    if (gridView1.IsNewItemRow(rowHandle))
                    {
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[0], dr["التسلسل"]);
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns["المورد"], dr["المورد"]);
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns["النوع"], "شراء");
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns["رقم الفاتورة"], dr["رقم الفاتورة"]);
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns["التاريخ"], dr["التاريخ"]);
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns["الاجمالى قبل"], dr["الاجمالى قبل"]);
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns["الاجمالى بعد"], dr["الاجمالى بعد"]);
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns["المخزن"], dr["المخزن"]);

                        totalBills += Convert.ToDouble(dr["الاجمالى بعد"]);
                    }

                    string q = "SELECT supplier_return_bill.ReturnBill_ID as 'التسلسل',supplier.Supplier_Name as 'المورد',supplier_return_bill.Return_Bill_No as 'رقم الفاتورة',supplier_return_bill.Date as 'التاريخ',supplier_return_bill.Total_Price_BD as 'الاجمالى قبل',supplier_return_bill.Total_Price_AD as 'الاجمالى بعد',store.Store_Name as 'المخزن' FROM supplier_return_bill INNER JOIN import_storage_return ON supplier_return_bill.ImportStorageReturn_ID = import_storage_return.ImportStorageReturn_ID INNER JOIN supplier_bill ON supplier_bill.StorageImportPermission_ID = import_storage_return.StorageImportPermission_ID INNER JOIN supplier_return_bill_details ON supplier_return_bill_details.ReturnBill_ID = supplier_return_bill.ReturnBill_ID INNER JOIN store ON store.Store_ID = supplier_return_bill.Store_ID INNER JOIN supplier ON supplier.Supplier_ID = supplier_return_bill.Supplier_ID where supplier_bill.StorageImportPermission_ID=" + dr["StorageImportPermission_ID"] + " and supplier_return_bill.Supplier_ID=" + supplierId;
                    MySqlCommand comand2 = new MySqlCommand(q, dbconnection6);
                    MySqlDataReader dr2 = comand2.ExecuteReader();
                    while (dr2.Read())
                    {
                        gridView1.AddNewRow();
                        rowHandle = gridView1.GetRowHandle(gridView1.DataRowCount);
                        if (gridView1.IsNewItemRow(rowHandle))
                        {
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns[0], dr2["التسلسل"]);
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns["المورد"], dr2["المورد"]);
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns["النوع"], "مرتجع");
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns["رقم الفاتورة"], dr2["رقم الفاتورة"]);
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns["التاريخ"], dr2["التاريخ"]);
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns["الاجمالى قبل"], dr2["الاجمالى قبل"]);
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns["الاجمالى بعد"], dr2["الاجمالى بعد"]);
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns["المخزن"], dr2["المخزن"]);

                            TotalReturns += Convert.ToDouble(dr2["الاجمالى بعد"]);
                        }
                    }
                    dr2.Close();
                }
                dr.Close();
            }
            //gridView1.Columns[0].Visible = false;

            if (gridView1.IsLastVisibleRow)
            {
                gridView1.FocusedRowHandle = gridView1.RowCount - 1;
            }

            labelTotalBills.Text = totalBills.ToString();
            labelTotalReturns.Text = TotalReturns.ToString();
            labelSafy.Text = (totalBills - TotalReturns).ToString();
        }

        public XtraTabPage getTabPage(XtraTabControl tabControl, string text)
        {
            for (int i = 0; i < tabControl.TabPages.Count; i++)
                if (tabControl.TabPages[i].Text == text)
                {
                    return tabControl.TabPages[i];
                }
            return null;
        }
    }
}

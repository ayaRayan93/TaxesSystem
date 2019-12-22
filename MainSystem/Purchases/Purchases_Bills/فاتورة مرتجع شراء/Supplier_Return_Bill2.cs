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
    public partial class Supplier_Return_Bill2 : Form
    {
        MySqlConnection dbconnection, dbconnection2;
        bool loaded = false;
        bool loadedStore = false;
        DataRow row1 = null;
        int rowHandle = -1;
        int ImportStorageReturnId = 0;
        int storageImportPermissionId = 0;
        bool loadSup = false;
        bool loadSupPerm = false;

        public Supplier_Return_Bill2(MainForm mainForm)
        {
            InitializeComponent();
            dbconnection = new MySqlConnection(connection.connectionString);
            dbconnection2 = new MySqlConnection(connection.connectionString);
        }
        
        private void Supplier_Return_Bill_Load(object sender, EventArgs e)
        {
            try
            {
                dbconnection.Open();
                string query = "select * from store";
                MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                DataTable dt = new DataTable();
                da.Fill(dt);
                comStore.DataSource = dt;
                comStore.DisplayMember = dt.Columns["Store_Name"].ToString();
                comStore.ValueMember = dt.Columns["Store_ID"].ToString();
                comStore.Text = "";

                query = "select * from supplier";
                da = new MySqlDataAdapter(query, dbconnection);
                dt = new DataTable();
                da.Fill(dt);
                comSupplier.DataSource = dt;
                comSupplier.DisplayMember = dt.Columns["Supplier_Name"].ToString();
                comSupplier.ValueMember = dt.Columns["Supplier_ID"].ToString();
                comSupplier.SelectedIndex = -1;
                loaded = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }

        private void comStore_SelectedValueChanged(object sender, EventArgs e)
        {
            if (loaded)
            {
                try
                {
                    loadedStore = false;
                    dbconnection.Open();
                    if (comStore.Text != "")
                    {
                        string query = "select * from import_storage_return where Store_ID=" + comStore.SelectedValue.ToString() + " and Confirmed=0";
                        MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        comPermessionNum.DataSource = dt;
                        comPermessionNum.DisplayMember = dt.Columns["Returned_Permission_Number"].ToString();
                        comPermessionNum.ValueMember = dt.Columns["ImportStorageReturn_ID"].ToString();
                        comPermessionNum.Text = "";
                        loadedStore = true;
                        
                        loadSup = false;
                        loadSupPerm = false;
                        comSupplier.DataSource = null;
                        comSupPerm.DataSource = null;
                        newReturnBill();
                    }
                    else
                    {
                        comPermessionNum.DataSource = null;
                        loadedStore = false;
                        loadSup = false;
                        loadSupPerm = false;
                        comSupplier.DataSource = null;
                        comSupPerm.DataSource = null;
                        newReturnBill();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                dbconnection.Close();
            }
        }

        private void comPerm_SelectedValueChanged(object sender, EventArgs e)
        {
            if (loadedStore)
            {
                try
                {
                    if (comPermessionNum.Text != "" && comStore.SelectedValue != null)
                    {
                        int billNum = 0;
                        if (int.TryParse(comPermessionNum.Text, out billNum))
                        {
                            dbconnection.Open();
                            newReturnBill();

                            string query = "select ImportStorageReturn_ID from import_storage_return where Store_ID=" + comStore.SelectedValue.ToString() + " and Returned_Permission_Number=" + billNum;
                            MySqlCommand com = new MySqlCommand(query, dbconnection);
                            if (com.ExecuteScalar() != null)
                            {
                                ImportStorageReturnId = Convert.ToInt32(com.ExecuteScalar().ToString());
                            }
                            else
                            {
                                ImportStorageReturnId = 0;
                            }

                            query = "select StorageImportPermission_ID from import_storage_return where Store_ID=" + comStore.SelectedValue.ToString() + " and Returned_Permission_Number=" + billNum;
                            com = new MySqlCommand(query, dbconnection);
                            if (com.ExecuteScalar() != null)
                            {
                                if (com.ExecuteScalar().ToString() != "")
                                {
                                    storageImportPermissionId = Convert.ToInt32(com.ExecuteScalar().ToString());
                                }
                                else
                                {
                                    storageImportPermissionId = 0;
                                }
                            }
                            else
                            {
                                storageImportPermissionId = 0;
                            }
                            displayPermissionSupplier();
                            if (storageImportPermissionId == 0)
                            {
                                displayPermissionDetails();
                            }
                            displayReturnPermissionDetails(0);
                        }
                        else
                        {
                            newReturnBill();
                            loadSup = false;
                            loadSupPerm = false;
                            comSupPerm.DataSource = null;
                            comSupplier.DataSource = null;
                        }
                    }
                    else
                    {
                        newReturnBill();
                        loadSup = false;
                        loadSupPerm = false;
                        comSupPerm.DataSource = null;
                        comSupplier.DataSource = null;
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

        private void comSupplier_SelectedValueChanged(object sender, EventArgs e)
        {
            if (loadSup)
            {
                try
                {
                    string query = "SELECT import_storage_return_supplier.Supplier_Permission_Number,import_storage_return_supplier.ImportStorageReturnSupplier_ID FROM import_storage_return_supplier where import_storage_return_supplier.Supplier_ID=" + comSupplier.SelectedValue.ToString() + " and import_storage_return_supplier.ImportStorageReturn_ID=" + ImportStorageReturnId + " and ReturnedPurchaseBill=0";
                    MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        loadSupPerm = false;
                        comSupPerm.DataSource = dt;
                        comSupPerm.DisplayMember = dt.Columns["Supplier_Permission_Number"].ToString();
                        comSupPerm.ValueMember = dt.Columns["ImportStorageReturnSupplier_ID"].ToString();
                        comSupPerm.SelectedIndex = -1;
                        loadSupPerm = true;
                        newReturnBill();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                dbconnection.Close();
            }
        }

        private void comSupPerm_SelectedValueChanged(object sender, EventArgs e)
        {
            if (loadSupPerm)
            {
                try
                {
                    dbconnection.Open();
                    displayPermissionDetails();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                dbconnection.Close();
                dbconnection2.Close();
            }
        }

        private void newReturnBill_Click(object sender, EventArgs e)
        {
            try
            {
                dbconnection.Open();
                comStore.Text = "";
                loadedStore = false;
                loadSup = false;
                loadSupPerm = false;
                comPermessionNum.DataSource = null;
                comSupPerm.DataSource = null;
                comSupplier.DataSource = null;
                newReturnBill();
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
                loaded = false;
                row1 = gridView1.GetDataRow(gridView1.GetRowHandle(e.RowHandle));
                rowHandle = e.RowHandle;
                txtCode.Text = row1["الكود"].ToString();
                txtPrice.Text = row1["السعر"].ToString();
                txtLastPrice.Text = row1["السعر بالزيادة"].ToString();
                txtPurchasePrice.Text = row1["سعر الشراء"].ToString();
                txtNormalIncrease.Text = row1["الزيادة العادية"].ToString();
                txtCategoricalIncrease.Text = row1["الزيادة القطعية"].ToString();
                txtTotalMeter.Text = row1["متر/قطعة"].ToString();
                if (txtCategoricalIncrease.Text == "" && txtNormalIncrease.Text == "")
                {
                    txtCategoricalIncrease.Text = txtNormalIncrease.Text = "0";
                }
                if (row1["Price_Type"].ToString() == "لستة")
                {
                    radioList.Checked = true;
                    txtDiscount.Text = row1["نسبة الخصم"].ToString();
                }
                else if (row1["Price_Type"].ToString() == "قطعى")
                {
                    radioQata3y.Checked = true;
                    txtDiscount.Text = row1["نسبة الخصم"].ToString();
                }
                if (txtDiscount.Text == "")
                {
                    txtDiscount.Text = "0";
                }
                loaded = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txtBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (loaded && row1 != null)
                {
                    if (txtPrice.Text != "" && txtCategoricalIncrease.Text != "" && txtDiscount.Text != "" && txtNormalIncrease.Text != "" && txtTax.Text != "")
                    {
                        double price, BuyDiscount, NormalIncrease, Categorical_Increase, VAT;
                        if (double.TryParse(txtPrice.Text, out price)
                         &&
                         double.TryParse(txtDiscount.Text, out BuyDiscount)
                         &&
                         double.TryParse(txtNormalIncrease.Text, out NormalIncrease)
                         &&
                         double.TryParse(txtCategoricalIncrease.Text, out Categorical_Increase)
                         &&
                         double.TryParse(txtTax.Text, out VAT))
                        {
                            txtPurchasePrice.Text = (calPurchasesPrice() /*+ VAT*/) + "";
                            txtLastPrice.Text = (lastPrice(calPurchasesPrice())) + "";
                        }
                        else
                        {
                            txtPurchasePrice.Text = "";
                            txtLastPrice.Text = "";
                        }
                    }
                    else
                    {
                        txtPurchasePrice.Text = "";
                        txtLastPrice.Text = "";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void addToReturnBill_Click(object sender, EventArgs e)
        {
            try
            {
                if (row1 != null)
                {
                    //if (!IsAdded())
                    //{
                    if (Convert.ToDouble(row1["متر/قطعة"].ToString()) > 0)
                    {
                        if (txtTotalMeter.Text != "" && txtPrice.Text != "" && txtCategoricalIncrease.Text != "" && txtDiscount.Text != "" && txtNormalIncrease.Text != "" && txtTax.Text != "" && txtPurchasePrice.Text != "")
                        {
                            double price, PurchaseDiscount, NormalIncrease, Categorical_Increase, tax, purchasePrice, quantity;
                            if (double.TryParse(txtPrice.Text, out price)
                             &&
                             double.TryParse(txtDiscount.Text, out PurchaseDiscount)
                             &&
                             double.TryParse(txtNormalIncrease.Text, out NormalIncrease)
                             &&
                             double.TryParse(txtCategoricalIncrease.Text, out Categorical_Increase)
                             &&
                             double.TryParse(txtTotalMeter.Text, out quantity)
                             &&
                             double.TryParse(txtTax.Text, out tax)
                             &&
                             double.TryParse(txtPurchasePrice.Text, out purchasePrice))
                            {
                                if (quantity <= Convert.ToDouble(row1["متر/قطعة"].ToString()))
                                {
                                    dbconnection.Open();
                                    if (gridView2.RowCount == 0)
                                    {
                                        displayReturnPermissionDetails(0);
                                    }
                                    gridView2.AddNewRow();
                                    int rowHandl = gridView2.GetRowHandle(gridView2.DataRowCount);
                                    if (gridView2.IsNewItemRow(rowHandl))
                                    {
                                        gridView2.SetRowCellValue(rowHandl, gridView2.Columns["Data_ID"], row1["Data_ID"].ToString());
                                        gridView2.SetRowCellValue(rowHandl, gridView2.Columns["الكود"], row1["الكود"].ToString());
                                        gridView2.SetRowCellValue(rowHandl, gridView2.Columns["النوع"], row1["النوع"].ToString());
                                        gridView2.SetRowCellValue(rowHandl, gridView2.Columns["الاسم"], row1["الاسم"].ToString());
                                        gridView2.SetRowCellValue(rowHandl, gridView2.Columns["السعر"], System.Math.Round(price, 2));
                                        double lastPrece = Convert.ToDouble(txtLastPrice.Text);
                                        gridView2.SetRowCellValue(rowHandl, gridView2.Columns["السعر بالزيادة"], System.Math.Round(lastPrece, 2));
                                        if (radioList.Checked == true)
                                        {
                                            gridView2.SetRowCellValue(rowHandl, gridView2.Columns["نوع السعر"], "لستة");
                                            gridView2.SetRowCellValue(rowHandl, gridView2.Columns["نسبة الخصم"], System.Math.Round(PurchaseDiscount, 2));
                                            gridView2.SetRowCellValue(rowHandl, gridView2.Columns["الزيادة العادية"], System.Math.Round(NormalIncrease, 2));
                                            gridView2.SetRowCellValue(rowHandl, gridView2.Columns["الزيادة القطعية"], System.Math.Round(Categorical_Increase, 2));
                                        }
                                        else if (radioQata3y.Checked == true)
                                        {
                                            gridView2.SetRowCellValue(rowHandl, gridView2.Columns["نوع السعر"], "قطعى");
                                            gridView2.SetRowCellValue(rowHandl, gridView2.Columns["نسبة الخصم"], System.Math.Round(PurchaseDiscount, 2));
                                            gridView2.SetRowCellValue(rowHandl, gridView2.Columns["الزيادة العادية"], System.Math.Round(NormalIncrease, 2)/*""*/);
                                            gridView2.SetRowCellValue(rowHandl, gridView2.Columns["الزيادة القطعية"], System.Math.Round(Categorical_Increase, 2)/*""*/);
                                        }
                                        gridView2.SetRowCellValue(rowHandl, gridView2.Columns["سعر الشراء"], System.Math.Round(purchasePrice, 2));
                                        gridView2.SetRowCellValue(rowHandl, gridView2.Columns["متر/قطعة"], quantity);
                                        if (row1["BillData_ID"].ToString() != "")
                                        {
                                            gridView2.SetRowCellValue(rowHandl, gridView2.Columns["BillData_ID"], row1["BillData_ID"].ToString());
                                        }
                                        else if (row1["ImportStorageReturnDetails_ID"].ToString() != "")
                                        {
                                            gridView2.SetRowCellValue(rowHandl, gridView2.Columns["ImportStorageReturnDetails_ID"], row1["ImportStorageReturnDetails_ID"].ToString());
                                        }

                                        if (gridView2.IsLastVisibleRow)
                                        {
                                            gridView2.FocusedRowHandle = gridView2.RowCount - 1;
                                        }

                                        double totalAllB = 0;
                                        double totalAllA = 0;
                                        double totalAllDiscount = 0;
                                        double totalAllCatInc = 0;
                                        for (int i = 0; i < gridView2.RowCount; i++)
                                        {
                                            double totalB = 0;
                                            double totalA = 0;
                                            double totalNormInc = 0;
                                            double totalDiscount = 0;
                                            double totalCatInc = 0;
                                            if (gridView2.GetRowCellDisplayText(i, "الزيادة العادية") != "")
                                            {
                                                totalNormInc = Convert.ToDouble(gridView2.GetRowCellDisplayText(i, "الزيادة العادية"));
                                            }
                                            else
                                            {
                                                totalNormInc = 0;
                                            }
                                            if (gridView2.GetRowCellDisplayText(i, "الزيادة القطعية") != "")
                                            {
                                                totalCatInc = Convert.ToDouble(gridView2.GetRowCellDisplayText(i, "الزيادة القطعية"));
                                            }
                                            else
                                            {
                                                totalCatInc = 0;
                                            }
                                            totalB = (Convert.ToDouble(gridView2.GetRowCellDisplayText(i, "السعر")) + totalNormInc) * Convert.ToDouble(gridView2.GetRowCellDisplayText(i, "متر/قطعة"));
                                            totalDiscount = totalB * (Convert.ToDouble(gridView2.GetRowCellDisplayText(i, "نسبة الخصم")) / 100);
                                            totalCatInc = Convert.ToDouble(gridView2.GetRowCellDisplayText(i, "الزيادة القطعية")) * Convert.ToDouble(gridView2.GetRowCellDisplayText(i, "متر/قطعة"));
                                            totalA = (totalB - totalDiscount) + totalCatInc;

                                            totalAllB += totalB;
                                            totalAllDiscount += totalDiscount;
                                            totalAllCatInc += totalCatInc;
                                            totalAllA += totalA;
                                        }
                                        gridView1.DeleteSelectedRows();
                                        row1 = null;
                                        rowHandle = -1;
                                        /////////////////////////////
                                        double totalB1 = 0;
                                        double totalA1 = 0;
                                        for (int i = 0; i < gridView1.RowCount; i++)
                                        {
                                            if (gridView1.GetRowCellDisplayText(i, "السعر") != "")
                                            {
                                                totalB1 += Convert.ToDouble(gridView1.GetRowCellDisplayText(i, "السعر")) * Convert.ToDouble(gridView1.GetRowCellDisplayText(i, "متر/قطعة"));
                                            }
                                            if (gridView1.GetRowCellDisplayText(i, "سعر الشراء") != "")
                                            {
                                                totalA1 += Convert.ToDouble(gridView1.GetRowCellDisplayText(i, "سعر الشراء")) * Convert.ToDouble(gridView1.GetRowCellDisplayText(i, "متر/قطعة"));
                                            }
                                        }
                                        labTotalPriceBD.Text = totalB1.ToString();
                                        labTotalPrice.Text = totalA1.ToString();
                                        ///////////////////////////
                                        txtTotalMeter.Text = txtCode.Text = "";
                                        txtDiscount.Text = txtNormalIncrease.Text = txtCategoricalIncrease.Text = "0";
                                        txtPrice.Text = txtLastPrice.Text = txtPurchasePrice.Text = "";
                                        txtTax.Text = "0";

                                        labelTotalB.Text = totalAllB.ToString("#.000");
                                        labelTotalDiscount.Text = totalAllDiscount.ToString("#.000");
                                        labelTotalCat.Text = totalAllCatInc.ToString("#.000");
                                        labelTotalA.Text = totalAllA.ToString("#.000");
                                        labelTotalSafy.Text = (Convert.ToDouble(labelTotalA.Text) + (Convert.ToDouble(labelTotalA.Text) * (Convert.ToDouble(txtAllTax.Text) / 100))).ToString("#.000");
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("لا يوجد كمية كافية من البند");
                                }
                            }
                            else
                            {
                                MessageBox.Show("تاكد ان البيانات صحيحة");
                            }
                        }
                        else
                        {
                            MessageBox.Show("تاكد من ادخال جميع البيانات");
                        }
                    }
                    else
                    {
                        MessageBox.Show("لا يوجد كمية كافية من البند");
                    }
                }
                else
                {
                    MessageBox.Show("يجب اختيار عنصر");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            dbconnection.Close();
        }

        private void btnAddAll_Click(object sender, EventArgs e)
        {
            try
            {
                displayReturnPermissionDetails(0);
                dbconnection.Open();
                for (int i = 0; i < gridView1.RowCount; i++)
                {
                    if (Convert.ToDouble(gridView1.GetRowCellDisplayText(i, "متر/قطعة")) > 0)
                    {
                        gridView2.AddNewRow();
                        int rowHandl = gridView2.GetRowHandle(gridView2.DataRowCount);
                        if (gridView2.IsNewItemRow(rowHandl))
                        {
                            gridView2.SetRowCellValue(rowHandl, gridView2.Columns["Data_ID"], gridView1.GetRowCellDisplayText(i, "Data_ID"));
                            gridView2.SetRowCellValue(rowHandl, gridView2.Columns["الكود"], gridView1.GetRowCellDisplayText(i, "الكود"));
                            gridView2.SetRowCellValue(rowHandl, gridView2.Columns["النوع"], gridView1.GetRowCellDisplayText(i, "النوع"));
                            gridView2.SetRowCellValue(rowHandl, gridView2.Columns["الاسم"], gridView1.GetRowCellDisplayText(i, "الاسم"));
                            gridView2.SetRowCellValue(rowHandl, gridView2.Columns["السعر"], gridView1.GetRowCellDisplayText(i, "السعر"));
                            gridView2.SetRowCellValue(rowHandl, gridView2.Columns["السعر بالزيادة"], gridView1.GetRowCellDisplayText(i, "السعر بالزيادة"));
                            if (gridView1.GetRowCellDisplayText(i, "Price_Type") == "لستة")
                            {
                                gridView2.SetRowCellValue(rowHandl, gridView2.Columns["نوع السعر"], "لستة");
                                gridView2.SetRowCellValue(rowHandl, gridView2.Columns["نسبة الخصم"], gridView1.GetRowCellDisplayText(i, "نسبة الخصم"));
                            }
                            else if (gridView1.GetRowCellDisplayText(i, "Price_Type") == "قطعى")
                            {
                                gridView2.SetRowCellValue(rowHandl, gridView2.Columns["نوع السعر"], "قطعى");
                                gridView2.SetRowCellValue(rowHandl, gridView2.Columns["نسبة الخصم"], gridView1.GetRowCellDisplayText(i, "نسبة الخصم"));
                            }
                            gridView2.SetRowCellValue(rowHandl, gridView2.Columns["الزيادة العادية"], gridView1.GetRowCellDisplayText(i, "الزيادة العادية"));
                            gridView2.SetRowCellValue(rowHandl, gridView2.Columns["الزيادة القطعية"], gridView1.GetRowCellDisplayText(i, "الزيادة القطعية"));
                            gridView2.SetRowCellValue(rowHandl, gridView2.Columns["سعر الشراء"], gridView1.GetRowCellDisplayText(i, "سعر الشراء"));
                            gridView2.SetRowCellValue(rowHandl, gridView2.Columns["متر/قطعة"], gridView1.GetRowCellDisplayText(i, "متر/قطعة"));
                            if (gridView1.GetRowCellDisplayText(i, "BillData_ID") != "")
                            {
                                gridView2.SetRowCellValue(rowHandl, gridView2.Columns["BillData_ID"], gridView1.GetRowCellDisplayText(i, "BillData_ID"));
                            }
                            else if (gridView1.GetRowCellDisplayText(i, "ImportStorageReturnDetails_ID") != "")
                            {
                                gridView2.SetRowCellValue(rowHandl, gridView2.Columns["ImportStorageReturnDetails_ID"], gridView1.GetRowCellDisplayText(i, "ImportStorageReturnDetails_ID"));
                            }

                            int rowHnd = gridView1.GetRowHandle(i);
                            gridView1.DeleteRow(rowHnd);
                        }
                    }
                }

                if (gridView2.IsLastVisibleRow)
                {
                    gridView2.FocusedRowHandle = gridView2.RowCount - 1;
                }

                double totalAllB = 0;
                double totalAllA = 0;
                double totalAllDiscount = 0;
                double totalAllCatInc = 0;
                for (int i = 0; i < gridView2.RowCount; i++)
                {
                    double totalB = 0;
                    double totalA = 0;
                    double totalNormInc = 0;
                    double totalDiscount = 0;
                    double totalCatInc = 0;
                    double price = 0;
                    double discount = 0;
                    double CatInc = 0;
                    if (gridView2.GetRowCellDisplayText(i, "الزيادة العادية") != "")
                    {
                        totalNormInc = Convert.ToDouble(gridView2.GetRowCellDisplayText(i, "الزيادة العادية"));
                    }
                    else
                    {
                        totalNormInc = 0;
                    }
                    if (gridView2.GetRowCellDisplayText(i, "الزيادة القطعية") != "")
                    {
                        totalCatInc = Convert.ToDouble(gridView2.GetRowCellDisplayText(i, "الزيادة القطعية"));
                    }
                    else
                    {
                        totalCatInc = 0;
                    }
                    if (gridView2.GetRowCellDisplayText(i, "السعر") != "")
                    {
                        price = Convert.ToDouble(gridView2.GetRowCellDisplayText(i, "السعر"));
                    }
                    if (gridView2.GetRowCellDisplayText(i, "نسبة الخصم") != "")
                    {
                        discount = Convert.ToDouble(gridView2.GetRowCellDisplayText(i, "نسبة الخصم"));
                    }
                    if (gridView2.GetRowCellDisplayText(i, "الزيادة القطعية") != "")
                    {
                        CatInc = Convert.ToDouble(gridView2.GetRowCellDisplayText(i, "الزيادة القطعية"));
                    }

                    totalB = (price + totalNormInc) * Convert.ToDouble(gridView2.GetRowCellDisplayText(i, "متر/قطعة"));
                    totalDiscount = totalB * (discount / 100);
                    totalCatInc = CatInc * Convert.ToDouble(gridView2.GetRowCellDisplayText(i, "متر/قطعة"));
                    totalA = (totalB - totalDiscount) + totalCatInc;

                    totalAllB += totalB;
                    totalAllDiscount += totalDiscount;
                    totalAllCatInc += totalCatInc;
                    totalAllA += totalA;
                }
                labelTotalB.Text = totalAllB.ToString("#.000");
                labelTotalDiscount.Text = totalAllDiscount.ToString("#.000");
                labelTotalCat.Text = totalAllCatInc.ToString("#.000");
                labelTotalA.Text = totalAllA.ToString("#.000");
                labelTotalSafy.Text = (Convert.ToDouble(labelTotalA.Text) + (Convert.ToDouble(labelTotalA.Text) * (Convert.ToDouble(txtAllTax.Text) / 100))).ToString("#.000");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            dbconnection.Close();
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            try
            {
                DataRow row2 = gridView2.GetDataRow(gridView2.FocusedRowHandle);
                if (row2 != null)
                {
                    gridView1.AddNewRow();
                    int rowHandl = gridView1.GetRowHandle(gridView1.DataRowCount);
                    if (gridView1.IsNewItemRow(rowHandl))
                    {
                        gridView1.SetRowCellValue(rowHandl, gridView1.Columns["Data_ID"], row2["Data_ID"].ToString());
                        gridView1.SetRowCellValue(rowHandl, gridView1.Columns["الكود"], row2["الكود"].ToString());
                        gridView1.SetRowCellValue(rowHandl, gridView1.Columns["النوع"], row2["النوع"].ToString());
                        gridView1.SetRowCellValue(rowHandl, gridView1.Columns["الاسم"], row2["الاسم"].ToString());
                        gridView1.SetRowCellValue(rowHandl, gridView1.Columns["السعر"], row2["السعر"].ToString());
                        gridView1.SetRowCellValue(rowHandl, gridView1.Columns["السعر بالزيادة"], row2["السعر بالزيادة"].ToString());
                        gridView1.SetRowCellValue(rowHandl, gridView1.Columns["نسبة الخصم"], row2["نسبة الخصم"].ToString());
                        gridView1.SetRowCellValue(rowHandl, gridView1.Columns["الزيادة العادية"], row2["الزيادة العادية"].ToString());
                        gridView1.SetRowCellValue(rowHandl, gridView1.Columns["الزيادة القطعية"], row2["الزيادة القطعية"].ToString());
                        gridView1.SetRowCellValue(rowHandl, gridView1.Columns["سعر الشراء"], row2["سعر الشراء"].ToString());

                        if (row2["BillData_ID"].ToString() != "")
                        {
                            gridView1.SetRowCellValue(rowHandl, gridView1.Columns["BillData_ID"], row2["BillData_ID"].ToString());
                        }
                        else
                        {
                            gridView1.SetRowCellValue(rowHandl, gridView1.Columns["ImportStorageReturnDetails_ID"], row2["ImportStorageReturnDetails_ID"].ToString());
                        }
                        gridView1.SetRowCellValue(rowHandl, gridView1.Columns["Price_Type"], row2["نوع السعر"].ToString());
                        gridView1.SetRowCellValue(rowHandl, gridView1.Columns["متر/قطعة"], row2["متر/قطعة"].ToString());
                    }

                    if (gridView1.IsLastVisibleRow)
                    {
                        gridView1.FocusedRowHandle = gridView1.RowCount - 1;
                    }

                    double totalB1 = 0;
                    double totalA1 = 0;
                    for (int i = 0; i < gridView1.RowCount; i++)
                    {
                        if (gridView1.GetRowCellDisplayText(i, "السعر") != "")
                        {
                            totalB1 += Convert.ToDouble(gridView1.GetRowCellDisplayText(i, "السعر")) * Convert.ToDouble(gridView1.GetRowCellDisplayText(i, "متر/قطعة"));
                        }
                        if (gridView1.GetRowCellDisplayText(i, "سعر الشراء") != "")
                        {
                            totalA1 += Convert.ToDouble(gridView1.GetRowCellDisplayText(i, "سعر الشراء")) * Convert.ToDouble(gridView1.GetRowCellDisplayText(i, "متر/قطعة"));
                        }
                    }
                    labTotalPriceBD.Text = totalB1.ToString();
                    labTotalPrice.Text = totalA1.ToString();
                    //////////////////////////////////////////////
                    gridView2.DeleteRow(gridView2.FocusedRowHandle);

                    double totalAllB = 0;
                    double totalAllA = 0;
                    double totalAllDiscount = 0;
                    double totalAllCatInc = 0;
                    for (int i = 0; i < gridView2.RowCount; i++)
                    {
                        double totalB = 0;
                        double totalA = 0;
                        double totalNormInc = 0;
                        double totalDiscount = 0;
                        double totalCatInc = 0;
                        if (gridView2.GetRowCellDisplayText(i, "الزيادة العادية") != "")
                        {
                            totalNormInc = Convert.ToDouble(gridView2.GetRowCellDisplayText(i, "الزيادة العادية"));
                        }
                        else
                        {
                            totalNormInc = 0;
                        }
                        if (gridView2.GetRowCellDisplayText(i, "الزيادة القطعية") != "")
                        {
                            totalCatInc = Convert.ToDouble(gridView2.GetRowCellDisplayText(i, "الزيادة القطعية"));
                        }
                        else
                        {
                            totalCatInc = 0;
                        }
                        totalB = (Convert.ToDouble(gridView2.GetRowCellDisplayText(i, "السعر")) + totalNormInc) * Convert.ToDouble(gridView2.GetRowCellDisplayText(i, "متر/قطعة"));
                        totalDiscount = totalB * (Convert.ToDouble(gridView2.GetRowCellDisplayText(i, "نسبة الخصم")) / 100);
                        totalCatInc = Convert.ToDouble(gridView2.GetRowCellDisplayText(i, "الزيادة القطعية")) * Convert.ToDouble(gridView2.GetRowCellDisplayText(i, "متر/قطعة"));
                        totalA = (totalB - totalDiscount) + totalCatInc;

                        totalAllB += totalB;
                        totalAllDiscount += totalDiscount;
                        totalAllCatInc += totalCatInc;
                        totalAllA += totalA;
                    }
                    labelTotalB.Text = totalAllB.ToString("#.000");
                    labelTotalDiscount.Text = totalAllDiscount.ToString("#.000");
                    labelTotalCat.Text = totalAllCatInc.ToString("#.000");
                    labelTotalA.Text = totalAllA.ToString("#.000");
                    labelTotalSafy.Text = (Convert.ToDouble(labelTotalA.Text) + (Convert.ToDouble(labelTotalA.Text) * (Convert.ToDouble(txtAllTax.Text) / 100))).ToString("#.000");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                int billNum = 0;
                int returnBillId = 0;
                if (int.TryParse(comPermessionNum.Text, out billNum))
                {
                    if (gridView2.RowCount > 0 && comStore.SelectedValue != null && comSupplier.SelectedValue != null)
                    {
                        dbconnection.Open();
                        int BillNo = 1;
                        string query = "select Return_Bill_No from supplier_return_bill where Supplier_ID=" + comSupplier.SelectedValue.ToString() + " ORDER BY ReturnBill_ID DESC LIMIT 1";
                        MySqlCommand com = new MySqlCommand(query, dbconnection);
                        if (com.ExecuteScalar() != null)
                        {
                            BillNo = Convert.ToInt32(com.ExecuteScalar());
                            BillNo++;
                        }

                        query = "insert into supplier_return_bill (Store_ID,Return_Bill_No,Supplier_ID,Returned_Permission_Number,Total_Price_BD,Total_Price_AD,Date,Supplier_Permission_Number,ImportStorageReturn_ID,Employee_ID,Value_Additive_Tax) values (@Store_ID,@Return_Bill_No,@Supplier_ID,@Returned_Permission_Number,@Total_Price_BD,@Total_Price_AD,@Date,@Supplier_Permission_Number,@ImportStorageReturn_ID,@Employee_ID,@Value_Additive_Tax)";
                        com = new MySqlCommand(query, dbconnection);
                        com.Parameters.Add("@Store_ID", MySqlDbType.Int16);
                        com.Parameters["@Store_ID"].Value = comStore.SelectedValue.ToString();
                        com.Parameters.Add("@Return_Bill_No", MySqlDbType.Int16);
                        com.Parameters["@Return_Bill_No"].Value = BillNo;
                        com.Parameters.Add("@Supplier_ID", MySqlDbType.Int16);
                        com.Parameters["@Supplier_ID"].Value = comSupplier.SelectedValue.ToString();
                        com.Parameters.Add("@Returned_Permission_Number", MySqlDbType.Int16);
                        com.Parameters["@Returned_Permission_Number"].Value = billNum;
                        com.Parameters.Add("@Total_Price_BD", MySqlDbType.Decimal);
                        com.Parameters["@Total_Price_BD"].Value = Convert.ToDouble(labelTotalB.Text);
                        com.Parameters.Add("@Total_Price_AD", MySqlDbType.Decimal);
                        com.Parameters["@Total_Price_AD"].Value = Convert.ToDouble(labelTotalSafy.Text);
                        com.Parameters.Add("@Date", MySqlDbType.DateTime);
                        com.Parameters["@Date"].Value = DateTime.Now;
                        if (comSupPerm.Text != "" && comSupPerm.SelectedValue != null)
                        {
                            com.Parameters.Add("@Supplier_Permission_Number", MySqlDbType.Int16);
                            com.Parameters["@Supplier_Permission_Number"].Value = comSupPerm.Text;
                        }
                        else
                        {
                            com.Parameters.Add("@Supplier_Permission_Number", MySqlDbType.Int16);
                            com.Parameters["@Supplier_Permission_Number"].Value = null;
                        }
                        com.Parameters.Add("@ImportStorageReturn_ID", MySqlDbType.Int16);
                        com.Parameters["@ImportStorageReturn_ID"].Value = ImportStorageReturnId;
                        com.Parameters.Add("@Employee_ID", MySqlDbType.Int16);
                        com.Parameters["@Employee_ID"].Value = UserControl.EmpID;
                        com.Parameters.Add("@Value_Additive_Tax", MySqlDbType.Decimal);
                        com.Parameters["@Value_Additive_Tax"].Value = txtAllTax.Text;
                        com.ExecuteNonQuery();

                        query = "select ReturnBill_ID from supplier_return_bill ORDER BY ReturnBill_ID DESC LIMIT 1 ";
                        com = new MySqlCommand(query, dbconnection);
                        if (com.ExecuteScalar() != null)
                        {
                            returnBillId = (int)com.ExecuteScalar();
                        }

                        for (int i = 0; i < gridView2.RowCount; i++)
                        {
                            query = "insert into supplier_return_bill_details (ReturnBill_ID,Data_ID,Price_Type,Price,Last_Price,Purchasing_Discount,Normal_Increase,Categorical_Increase,Purchasing_Price,Total_Meters,BillData_ID,ImportStorageReturnDetails_ID) values (@ReturnBill_ID,@Data_ID,@Price_Type,@Price,@Last_Price,@Purchasing_Discount,@Normal_Increase,@Categorical_Increase,@Purchasing_Price,@Total_Meters,@BillData_ID,@ImportStorageReturnDetails_ID)";
                            com = new MySqlCommand(query, dbconnection);
                            com.Parameters.Add("@ReturnBill_ID", MySqlDbType.Int16);
                            com.Parameters["@ReturnBill_ID"].Value = returnBillId;
                            com.Parameters.Add("@Data_ID", MySqlDbType.Int16);
                            com.Parameters["@Data_ID"].Value = gridView2.GetRowCellDisplayText(i, gridView2.Columns["Data_ID"]);
                            com.Parameters.Add("@Price_Type", MySqlDbType.VarChar);
                            com.Parameters["@Price_Type"].Value = gridView2.GetRowCellDisplayText(i, gridView2.Columns["نوع السعر"]);
                            com.Parameters.Add("@Price", MySqlDbType.Decimal);
                            com.Parameters["@Price"].Value = gridView2.GetRowCellDisplayText(i, gridView2.Columns["السعر"]);
                            com.Parameters.Add("@Purchasing_Discount", MySqlDbType.Decimal);
                            com.Parameters["@Purchasing_Discount"].Value = gridView2.GetRowCellDisplayText(i, gridView2.Columns["نسبة الخصم"]);
                            com.Parameters.Add("@Normal_Increase", MySqlDbType.Decimal);
                            com.Parameters["@Normal_Increase"].Value = gridView2.GetRowCellDisplayText(i, gridView2.Columns["الزيادة العادية"]);
                            com.Parameters.Add("@Categorical_Increase", MySqlDbType.Decimal);
                            com.Parameters["@Categorical_Increase"].Value = gridView2.GetRowCellDisplayText(i, gridView2.Columns["الزيادة القطعية"]);
                            com.Parameters.Add("@Last_Price", MySqlDbType.Decimal);
                            com.Parameters["@Last_Price"].Value = gridView2.GetRowCellDisplayText(i, gridView2.Columns["السعر بالزيادة"]).ToString();
                            com.Parameters.Add("@Purchasing_Price", MySqlDbType.Decimal);
                            com.Parameters["@Purchasing_Price"].Value = gridView2.GetRowCellDisplayText(i, gridView2.Columns["سعر الشراء"]);
                            com.Parameters.Add("@Total_Meters", MySqlDbType.Decimal);
                            com.Parameters["@Total_Meters"].Value = gridView2.GetRowCellDisplayText(i, gridView2.Columns["متر/قطعة"]);
                            if (gridView2.GetRowCellDisplayText(i, gridView2.Columns["BillData_ID"]) != "")
                            {
                                com.Parameters.Add("@BillData_ID", MySqlDbType.Int16);
                                com.Parameters["@BillData_ID"].Value = gridView2.GetRowCellDisplayText(i, gridView2.Columns["BillData_ID"]);
                                com.Parameters.Add("@ImportStorageReturnDetails_ID", MySqlDbType.Int16);
                                com.Parameters["@ImportStorageReturnDetails_ID"].Value = null;
                            }
                            else if (gridView2.GetRowCellDisplayText(i, gridView2.Columns["ImportStorageReturnDetails_ID"]) != "")
                            {
                                com.Parameters.Add("@BillData_ID", MySqlDbType.Int16);
                                com.Parameters["@BillData_ID"].Value = null;
                                com.Parameters.Add("@ImportStorageReturnDetails_ID", MySqlDbType.Int16);
                                com.Parameters["@ImportStorageReturnDetails_ID"].Value = gridView2.GetRowCellDisplayText(i, gridView2.Columns["ImportStorageReturnDetails_ID"]);
                            }
                            com.ExecuteNonQuery();
                        }

                        bool flagConfirm = false;
                        if (comSupPerm.SelectedValue != null)
                        {
                            string q2 = "update import_storage_return_supplier set ReturnedPurchaseBill=1 where ImportStorageReturnSupplier_ID=" + comSupPerm.SelectedValue.ToString();
                            com = new MySqlCommand(q2, dbconnection);
                            com.ExecuteNonQuery();
                            
                            query = "SELECT import_storage_return_supplier.Supplier_Permission_Number,import_storage_return_supplier.ImportStorageReturnSupplier_ID FROM import_storage_return_supplier INNER JOIN import_storage_return ON import_storage_return_supplier.ImportStorageReturn_ID = import_storage_return.ImportStorageReturn_ID where import_storage_return_supplier.ImportStorageReturn_ID=" + comPermessionNum.SelectedValue.ToString() + " and import_storage_return_supplier.ReturnedPurchaseBill=0 and import_storage_return.Store_ID=" + comStore.SelectedValue.ToString() + " and import_storage_return.Confirmed=0";
                            com = new MySqlCommand(query, dbconnection);
                            MySqlDataReader dr2 = com.ExecuteReader();
                            while (dr2.Read())
                            {
                                flagConfirm = true;
                            }
                            dr2.Close();
                        }
                        
                        if (flagConfirm == false)
                        {
                            query = "update import_storage_return set Confirmed=1 where import_storage_return.Returned_Permission_Number=" + comPermessionNum.Text + " and import_storage_return.Store_ID=" + comStore.SelectedValue.ToString();
                            MySqlCommand c = new MySqlCommand(query, dbconnection);
                            c.ExecuteNonQuery();
                        }

                        DecreaseSupplierAccount();

                        #region report
                        query = "select Store_Name from store where Store_ID=" + comStore.SelectedValue.ToString();
                        com = new MySqlCommand(query, dbconnection);
                        string storeName = com.ExecuteScalar().ToString();

                        double addabtiveTax = 0;
                        List<SupplierReturnBill_Items> bi = new List<SupplierReturnBill_Items>();

                        for (int i = 0; i < gridView2.RowCount; i++)
                        {
                            int rowHand = gridView2.GetRowHandle(i);
                            if (gridView2.GetRowCellDisplayText(i, gridView2.Columns["نوع السعر"]) == "لستة")
                            {
                                SupplierReturnBill_Items item = new SupplierReturnBill_Items() { Code = gridView2.GetRowCellDisplayText(i, gridView2.Columns["الكود"]), Product_Type = gridView2.GetRowCellDisplayText(i, gridView2.Columns["النوع"]), Product_Name = gridView2.GetRowCellDisplayText(i, gridView2.Columns["الاسم"]), Total_Meters = Convert.ToDouble(gridView2.GetRowCellDisplayText(i, gridView2.Columns["متر/قطعة"])), PriceB = Convert.ToDouble(gridView2.GetRowCellDisplayText(i, gridView2.Columns["السعر"])), Discount = Convert.ToDouble(gridView2.GetRowCellDisplayText(i, gridView2.Columns["نسبة الخصم"])), Last_Price = Convert.ToDouble(gridView2.GetRowCellDisplayText(i, gridView2.Columns["السعر بالزيادة"])), Normal_Increase = Convert.ToDouble(gridView2.GetRowCellDisplayText(i, gridView2.Columns["الزيادة العادية"])), Categorical_Increase = Convert.ToDouble(gridView2.GetRowCellDisplayText(i, gridView2.Columns["الزيادة القطعية"])), PriceA = Convert.ToDouble(gridView2.GetRowCellDisplayText(i, gridView2.Columns["سعر الشراء"])) };
                                bi.Add(item);
                            }
                            else if (gridView2.GetRowCellDisplayText(i, gridView2.Columns["نوع السعر"]) == "قطعى")
                            {
                                SupplierReturnBill_Items item = new SupplierReturnBill_Items() { Code = gridView2.GetRowCellDisplayText(i, gridView2.Columns["الكود"]), Product_Type = gridView2.GetRowCellDisplayText(i, gridView2.Columns["النوع"]), Product_Name = gridView2.GetRowCellDisplayText(i, gridView2.Columns["الاسم"]), Total_Meters = Convert.ToDouble(gridView2.GetRowCellDisplayText(i, gridView2.Columns["متر/قطعة"])), PriceB = Convert.ToDouble(gridView2.GetRowCellDisplayText(i, gridView2.Columns["السعر"])), Discount = Convert.ToDouble(gridView2.GetRowCellDisplayText(i, gridView2.Columns["نسبة الخصم"])), Last_Price = Convert.ToDouble(gridView2.GetRowCellDisplayText(i, gridView2.Columns["السعر بالزيادة"])), Normal_Increase = Convert.ToDouble(gridView2.GetRowCellDisplayText(i, gridView2.Columns["الزيادة العادية"])), Categorical_Increase = Convert.ToDouble(gridView2.GetRowCellDisplayText(i, gridView2.Columns["الزيادة القطعية"])), PriceA = Convert.ToDouble(gridView2.GetRowCellDisplayText(i, gridView2.Columns["سعر الشراء"])) };
                                bi.Add(item);
                            }
                        }
                        addabtiveTax = Convert.ToDouble(txtAllTax.Text);
                        Report_SupplierReturnBill f = new Report_SupplierReturnBill();
                        f.PrintInvoice(storeName, BillNo.ToString(), comSupplier.Text, billNum.ToString(), comSupPerm.Text, Convert.ToDouble(labelTotalDiscount.Text), Convert.ToDouble(labelTotalA.Text), addabtiveTax, Convert.ToDouble(labelTotalSafy.Text), bi);
                        f.ShowDialog();
                        #endregion

                        comStore.Text = "";
                        loadedStore = false;
                        loadSup = false;
                        loadSupPerm = false;
                        comPermessionNum.DataSource = null;
                        comSupPerm.DataSource = null;
                        comSupplier.DataSource = null;
                        newReturnBill();
                    }
                    else
                    {
                        MessageBox.Show("تاكد من البيانات");
                    }
                }
                else
                {
                    MessageBox.Show("تاكد من رقم الاذن");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            dbconnection.Close();
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            /*try
            {
                int billNum = 0;
                if (int.TryParse(txtBillNumber.Text, out billNum))
                {
                    if (BillNo > 0 && gridView2.RowCount > 0 && comStore.SelectedValue != null && comSupplier.SelectedValue != null)
                    {
                        dbconnection.Open();
                        string query = "select Store_Name from store where Store_ID=" + comStore.SelectedValue.ToString();
                        MySqlCommand com = new MySqlCommand(query, dbconnection);
                        string storeName = com.ExecuteScalar().ToString();

                        double addabtiveTax = 0;
                        List<SupplierReturnBill_Items> bi = new List<SupplierReturnBill_Items>();
                        for (int i = 0; i < gridView2.RowCount; i++)
                        {
                            int rowHand = gridView2.GetRowHandle(i);
                            if (gridView2.GetRowCellDisplayText(i, gridView2.Columns["نوع السعر"]) == "لستة")
                            {
                                //double lastPrice = 0;
                                //lastPrice = (Convert.ToDouble(gridView2.GetRowCellDisplayText(i, gridView2.Columns["سعر الشراء"])) * 100) / (100 - Convert.ToDouble(gridView2.GetRowCellDisplayText(i, gridView2.Columns["خصم الشراء"])));
                                SupplierReturnBill_Items item = new SupplierReturnBill_Items() { Code = gridView2.GetRowCellDisplayText(i, gridView2.Columns["الكود"]), Product_Type = gridView2.GetRowCellDisplayText(i, gridView2.Columns["النوع"]), Product_Name = gridView2.GetRowCellDisplayText(i, gridView2.Columns["الاسم"]), Total_Meters = Convert.ToDouble(gridView2.GetRowCellDisplayText(i, gridView2.Columns["متر/قطعة"])), PriceB = Convert.ToDouble(gridView2.GetRowCellDisplayText(i, gridView2.Columns["السعر"])), Discount = Convert.ToDouble(gridView2.GetRowCellDisplayText(i, gridView2.Columns["نسبة الخصم"])), Last_Price = Convert.ToDouble(gridView2.GetRowCellDisplayText(i, gridView2.Columns["السعر بالزيادة"])), Normal_Increase = Convert.ToDouble(gridView2.GetRowCellDisplayText(i, gridView2.Columns["الزيادة العادية"])), Categorical_Increase = Convert.ToDouble(gridView2.GetRowCellDisplayText(i, gridView2.Columns["الزيادة القطعية"])), PriceA = Convert.ToDouble(gridView2.GetRowCellDisplayText(i, gridView2.Columns["سعر الشراء"])) };
                                bi.Add(item);
                            }
                            else if (gridView2.GetRowCellDisplayText(i, gridView2.Columns["نوع السعر"]) == "قطعى")
                            {
                                SupplierReturnBill_Items item = new SupplierReturnBill_Items() { Code = gridView2.GetRowCellDisplayText(i, gridView2.Columns["الكود"]), Product_Type = gridView2.GetRowCellDisplayText(i, gridView2.Columns["النوع"]), Product_Name = gridView2.GetRowCellDisplayText(i, gridView2.Columns["الاسم"]), Total_Meters = Convert.ToDouble(gridView2.GetRowCellDisplayText(i, gridView2.Columns["متر/قطعة"])), PriceB = Convert.ToDouble(gridView2.GetRowCellDisplayText(i, gridView2.Columns["السعر"])), Discount = Convert.ToDouble(gridView2.GetRowCellDisplayText(i, gridView2.Columns["نسبة الخصم"])), Last_Price = Convert.ToDouble(gridView2.GetRowCellDisplayText(i, gridView2.Columns["السعر بالزيادة"])), Normal_Increase = Convert.ToDouble(gridView2.GetRowCellDisplayText(i, gridView2.Columns["الزيادة العادية"])), Categorical_Increase = Convert.ToDouble(gridView2.GetRowCellDisplayText(i, gridView2.Columns["الزيادة القطعية"])), PriceA = Convert.ToDouble(gridView2.GetRowCellDisplayText(i, gridView2.Columns["سعر الشراء"])) };
                                bi.Add(item);
                            }
                            //addabtiveTax += Convert.ToDouble(gridView2.GetRowCellDisplayText(i, gridView2.Columns["ضريبة القيمة المضافة"]));
                        }
                        addabtiveTax = Convert.ToDouble(txtAllTax.Text);
                        Report_SupplierReturnBill f = new Report_SupplierReturnBill();
                        f.PrintInvoice(storeName, BillNo.ToString(), comSupplier.Text, billNum.ToString(), comSupPerm.Text, Convert.ToDouble(labelTotalSafy.Text), addabtiveTax, bi);
                        f.ShowDialog();
                    }
                    else
                    {
                        MessageBox.Show("تاكد من البيانات");
                    }
                }
                else
                {
                    MessageBox.Show("تاكد من رقم الاذن");
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();*/
        }

        //functions
        bool IsAdded()
        {
            for (int i = 0; i < gridView2.RowCount; i++)
            {
                DataRow row3 = gridView2.GetDataRow(i);
                if (row1["BillData_ID"].ToString() != "")
                {
                    if ((row1["BillData_ID"].ToString() == row3["BillData_ID"].ToString()) && (row1["Data_ID"].ToString() == row3["Data_ID"].ToString()))
                        return true;
                }
                else if(row1["ImportStorageReturnDetails_ID"].ToString() != "")
                {
                    if ((row1["ImportStorageReturnDetails_ID"].ToString() == row3["ImportStorageReturnDetails_ID"].ToString()) && (row1["Data_ID"].ToString() == row3["Data_ID"].ToString()))
                        return true;
                }
            }
            return false;
        }

        public double calPurchasesPrice()
        {
            double addational = 0.0;
            double price = double.Parse(txtPrice.Text);

            double PurchasesPercent = double.Parse(txtDiscount.Text);
            if (radioQata3y.Checked == true)
            {
                price += Convert.ToDouble(txtNormalIncrease.Text) + Convert.ToDouble(txtCategoricalIncrease.Text);
                return price - (price * PurchasesPercent / 100.0);
            }
            else if (radioList.Checked == true)
            {

                double PurchasesPrice = (price + Convert.ToDouble(txtNormalIncrease.Text)) - ((price + Convert.ToDouble(txtNormalIncrease.Text)) * PurchasesPercent / 100.0);
                PurchasesPrice = PurchasesPrice + Convert.ToDouble(txtCategoricalIncrease.Text);
                return PurchasesPrice + addational;
            }
            else
            {
                return 0;
            }
        }

        public double lastPrice(double purchasePrice)
        {
            double discount = double.Parse(txtDiscount.Text);
            double lastPrice = purchasePrice * 100 / (100 - discount);

            return lastPrice;
        }

        public void displayPermissionDetails()
        {
            #region with permission
            if (storageImportPermissionId > 0)
            {
                //,supplier_bill_details.Purchasing_Ratio as 'نسبة الشراء'
                //,supplier_bill.Value_Additive_Tax as 'ضريبة القيمة المضافة'
                string q = "SELECT supplier_bill_details.Data_ID,data.Code as 'الكود',type.Type_Name as 'النوع',concat(product.Product_Name,' ',COALESCE(color.Color_Name,''),' ',data.Description,' ',groupo.Group_Name,' ',factory.Factory_Name,' ',COALESCE(size.Size_Value,''),' ',COALESCE(sort.Sort_Value,'')) as 'الاسم',supplier_bill_details.Price as 'السعر',supplier_bill_details.Last_Price as 'السعر بالزيادة',supplier_bill_details.Purchasing_Discount as 'نسبة الخصم',supplier_bill_details.Normal_Increase as 'الزيادة العادية',supplier_bill_details.Categorical_Increase as 'الزيادة القطعية',supplier_bill_details.Purchasing_Price as 'سعر الشراء',supplier_bill_details.Total_Meters 'متر/قطعة',supplier_bill_details.Price_Type,'BillData_ID','ImportStorageReturnDetails_ID' FROM supplier_bill INNER JOIN supplier_bill_details ON supplier_bill_details.Bill_ID = supplier_bill.Bill_ID INNER JOIN import_storage_return_details ON supplier_bill_details.Data_ID = import_storage_return_details.Data_ID INNER JOIN data ON data.Data_ID = supplier_bill_details.Data_ID INNER JOIN type ON type.Type_ID = data.Type_ID INNER JOIN product ON product.Product_ID = data.Product_ID INNER JOIN factory ON data.Factory_ID = factory.Factory_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID LEFT JOIN color ON color.Color_ID = data.Color_ID LEFT JOIN size ON size.Size_ID = data.Size_ID LEFT JOIN sort ON sort.Sort_ID = data.Sort_ID where supplier_bill.StorageImportPermission_ID=" + 0;
                MySqlDataAdapter da = new MySqlDataAdapter(q, dbconnection);
                DataTable dt = new DataTable();
                da.Fill(dt);
                gridControl1.DataSource = dt;
                //,supplier_bill_details.Purchasing_Ratio as 'نسبة الشراء'
                q = "SELECT supplier_bill_details.Data_ID,data.Code as 'الكود',type.Type_Name as 'النوع',concat(product.Product_Name,' ',COALESCE(color.Color_Name,''),' ',data.Description,' ',groupo.Group_Name,' ',factory.Factory_Name,' ',COALESCE(size.Size_Value,''),' ',COALESCE(sort.Sort_Value,'')) as 'الاسم',supplier_bill_details.Price as 'السعر',supplier_bill_details.Last_Price as 'السعر بالزيادة',supplier_bill_details.Purchasing_Discount as 'نسبة الخصم',supplier_bill_details.Normal_Increase as 'الزيادة العادية',supplier_bill_details.Categorical_Increase as 'الزيادة القطعية',supplier_bill_details.Purchasing_Price as 'سعر الشراء',import_storage_return_details.Total_Meters 'متر/قطعة',supplier_bill_details.Price_Type,supplier_bill_details.BillData_ID FROM supplier_bill INNER JOIN supplier_bill_details ON supplier_bill_details.Bill_ID = supplier_bill.Bill_ID INNER JOIN storage_import_permission ON supplier_bill.StorageImportPermission_ID = storage_import_permission.StorageImportPermission_ID INNER JOIN import_storage_return ON import_storage_return.StorageImportPermission_ID = storage_import_permission.StorageImportPermission_ID INNER JOIN import_storage_return_supplier ON import_storage_return_supplier.ImportStorageReturn_ID = import_storage_return.ImportStorageReturn_ID INNER JOIN import_storage_return_details ON import_storage_return_details.ImportStorageReturnSupplier_ID = import_storage_return_supplier.ImportStorageReturnSupplier_ID INNER JOIN data ON data.Data_ID = supplier_bill_details.Data_ID  and supplier_bill_details.Data_ID=import_storage_return_details.Data_ID INNER JOIN type ON type.Type_ID = data.Type_ID INNER JOIN product ON product.Product_ID = data.Product_ID INNER JOIN factory ON data.Factory_ID = factory.Factory_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID LEFT JOIN color ON color.Color_ID = data.Color_ID LEFT JOIN size ON size.Size_ID = data.Size_ID LEFT JOIN sort ON sort.Sort_ID = data.Sort_ID where supplier_bill.StorageImportPermission_ID=" + storageImportPermissionId + " and import_storage_return_supplier.ImportStorageReturnSupplier_ID=" + comSupPerm.SelectedValue.ToString() + " and supplier_bill.Supplier_Permission_Number=" + comSupPerm.Text;
                MySqlCommand comand = new MySqlCommand(q, dbconnection);
                MySqlDataReader dr = comand.ExecuteReader();
                while (dr.Read())
                {
                    double totalMeter = 0;
                    gridView1.AddNewRow();
                    int rowHandl = gridView1.GetRowHandle(gridView1.DataRowCount);
                    if (gridView1.IsNewItemRow(rowHandl))
                    {
                        gridView1.SetRowCellValue(rowHandl, gridView1.Columns["Data_ID"], dr["Data_ID"].ToString());
                        gridView1.SetRowCellValue(rowHandl, gridView1.Columns["الكود"], dr["الكود"].ToString());
                        gridView1.SetRowCellValue(rowHandl, gridView1.Columns["النوع"], dr["النوع"].ToString());
                        gridView1.SetRowCellValue(rowHandl, gridView1.Columns["الاسم"], dr["الاسم"].ToString());
                        gridView1.SetRowCellValue(rowHandl, gridView1.Columns["السعر"], dr["السعر"].ToString());
                        gridView1.SetRowCellValue(rowHandl, gridView1.Columns["السعر بالزيادة"], dr["السعر بالزيادة"].ToString());
                        //if (dr["خصم الشراء"].ToString() != "0")
                        //{
                        gridView1.SetRowCellValue(rowHandl, gridView1.Columns["نسبة الخصم"], dr["نسبة الخصم"].ToString());
                        //}
                        //else if (dr["نسبة الشراء"].ToString() != "0")
                        //{
                        //    gridView1.SetRowCellValue(rowHandl, gridView1.Columns["نسبة الشراء"], dr["نسبة الشراء"].ToString());
                        //}
                        gridView1.SetRowCellValue(rowHandl, gridView1.Columns["الزيادة العادية"], dr["الزيادة العادية"].ToString());
                        gridView1.SetRowCellValue(rowHandl, gridView1.Columns["الزيادة القطعية"], dr["الزيادة القطعية"].ToString());
                        //gridView1.SetRowCellValue(rowHandl, gridView1.Columns["ضريبة القيمة المضافة"], dr["ضريبة القيمة المضافة"].ToString());
                        gridView1.SetRowCellValue(rowHandl, gridView1.Columns["سعر الشراء"], dr["سعر الشراء"].ToString());
                        gridView1.SetRowCellValue(rowHandl, gridView1.Columns["BillData_ID"], dr["BillData_ID"].ToString());
                        gridView1.SetRowCellValue(rowHandl, gridView1.Columns["Price_Type"], dr["Price_Type"].ToString());

                        if (dr["متر/قطعة"].ToString() != "")
                        {
                            totalMeter = Convert.ToDouble(dr["متر/قطعة"].ToString());
                        }

                        dbconnection2.Open();
                        q = "SELECT supplier_return_bill_details.Total_Meters as 'متر/قطعة' FROM supplier_return_bill INNER JOIN supplier_return_bill_details ON supplier_return_bill_details.ReturnBill_ID = supplier_return_bill.ReturnBill_ID  where supplier_return_bill.ImportStorageReturn_ID=" + ImportStorageReturnId + " and supplier_return_bill_details.Data_ID=" + dr["Data_ID"].ToString();
                        MySqlCommand comand2 = new MySqlCommand(q, dbconnection2);
                        MySqlDataReader dr2 = comand2.ExecuteReader();
                        while (dr2.Read())
                        {
                            if (dr2["متر/قطعة"].ToString() != "")
                            {
                                totalMeter -= Convert.ToDouble(dr2["متر/قطعة"].ToString());
                            }
                        }
                        dr2.Close();
                        dbconnection2.Close();
                        if (totalMeter >= 0)
                        {
                            gridView1.SetRowCellValue(rowHandl, gridView1.Columns["متر/قطعة"], totalMeter);
                        }
                    }
                }
                dr.Close();
            }
            #endregion

            #region with out permission
            else
            {
                string q = "SELECT import_storage_return_details.Data_ID,data.Code as 'الكود',type.Type_Name as 'النوع',concat(product.Product_Name,' ',COALESCE(color.Color_Name,''),' ',data.Description,' ',groupo.Group_Name,' ',factory.Factory_Name,' ',COALESCE(size.Size_Value,''),' ',COALESCE(sort.Sort_Value,'')) as 'الاسم',purchasing_price.Price as 'السعر',purchasing_price.Last_Price as 'السعر بالزيادة',purchasing_price.Purchasing_Discount as 'نسبة الخصم',purchasing_price.Normal_Increase as 'الزيادة العادية',purchasing_price.Categorical_Increase as 'الزيادة القطعية',purchasing_price.Purchasing_Price as 'سعر الشراء',import_storage_return_details.Total_Meters 'متر/قطعة',purchasing_price.Price_Type,'BillData_ID','ImportStorageReturnDetails_ID' FROM import_storage_return inner join import_storage_return_supplier on import_storage_return.ImportStorageReturn_ID=import_storage_return_supplier.ImportStorageReturn_ID INNER JOIN import_storage_return_details ON import_storage_return_details.ImportStorageReturnSupplier_ID = import_storage_return_supplier.ImportStorageReturnSupplier_ID INNER JOIN data ON data.Data_ID = import_storage_return_details.Data_ID INNER JOIN type ON type.Type_ID = data.Type_ID INNER JOIN product ON product.Product_ID = data.Product_ID INNER JOIN factory ON data.Factory_ID = factory.Factory_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID LEFT JOIN color ON color.Color_ID = data.Color_ID LEFT JOIN size ON size.Size_ID = data.Size_ID LEFT JOIN sort ON sort.Sort_ID = data.Sort_ID left join purchasing_price on purchasing_price.Data_ID=import_storage_return_details.Data_ID where import_storage_return.ImportStorageReturn_ID=" + 0;
                MySqlDataAdapter da = new MySqlDataAdapter(q, dbconnection);
                DataTable dt = new DataTable();
                da.Fill(dt);
                gridControl1.DataSource = dt;

                q = "SELECT import_storage_return_details.Data_ID,data.Code as 'الكود',type.Type_Name as 'النوع',concat(product.Product_Name,' ',COALESCE(color.Color_Name,''),' ',data.Description,' ',groupo.Group_Name,' ',factory.Factory_Name,' ',COALESCE(size.Size_Value,''),' ',COALESCE(sort.Sort_Value,'')) as 'الاسم',purchasing_price.Price as 'السعر',purchasing_price.Last_Price as 'السعر بالزيادة',purchasing_price.Purchasing_Discount as 'نسبة الخصم',purchasing_price.Normal_Increase as 'الزيادة العادية',purchasing_price.Categorical_Increase as 'الزيادة القطعية',purchasing_price.Purchasing_Price as 'سعر الشراء',import_storage_return_details.Total_Meters 'متر/قطعة',purchasing_price.Price_Type,import_storage_return_details.ImportStorageReturnDetails_ID FROM import_storage_return inner join import_storage_return_supplier on import_storage_return.ImportStorageReturn_ID=import_storage_return_supplier.ImportStorageReturn_ID INNER JOIN import_storage_return_details ON import_storage_return_details.ImportStorageReturnSupplier_ID = import_storage_return_supplier.ImportStorageReturnSupplier_ID INNER JOIN data ON data.Data_ID = import_storage_return_details.Data_ID INNER JOIN type ON type.Type_ID = data.Type_ID INNER JOIN product ON product.Product_ID = data.Product_ID INNER JOIN factory ON data.Factory_ID = factory.Factory_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID LEFT JOIN color ON color.Color_ID = data.Color_ID LEFT JOIN size ON size.Size_ID = data.Size_ID LEFT JOIN sort ON sort.Sort_ID = data.Sort_ID left join purchasing_price on purchasing_price.Data_ID=import_storage_return_details.Data_ID where import_storage_return.ImportStorageReturn_ID=" + ImportStorageReturnId;
                MySqlCommand comand = new MySqlCommand(q, dbconnection);
                MySqlDataReader dr = comand.ExecuteReader();
                while (dr.Read())
                {
                    double totalMeter = 0;
                    gridView1.AddNewRow();
                    int rowHandl = gridView1.GetRowHandle(gridView1.DataRowCount);
                    if (gridView1.IsNewItemRow(rowHandl))
                    {
                        gridView1.SetRowCellValue(rowHandl, gridView1.Columns["Data_ID"], dr["Data_ID"].ToString());
                        gridView1.SetRowCellValue(rowHandl, gridView1.Columns["الكود"], dr["الكود"].ToString());
                        gridView1.SetRowCellValue(rowHandl, gridView1.Columns["النوع"], dr["النوع"].ToString());
                        gridView1.SetRowCellValue(rowHandl, gridView1.Columns["الاسم"], dr["الاسم"].ToString());
                        gridView1.SetRowCellValue(rowHandl, gridView1.Columns["السعر"], dr["السعر"].ToString());
                        gridView1.SetRowCellValue(rowHandl, gridView1.Columns["السعر بالزيادة"], dr["السعر بالزيادة"].ToString());
                        //if (dr["خصم الشراء"].ToString() != "0")
                        //{
                        gridView1.SetRowCellValue(rowHandl, gridView1.Columns["نسبة الخصم"], dr["نسبة الخصم"].ToString());
                            //gridView1.SetRowCellValue(rowHandl, gridView1.Columns["نسبة الشراء"], 0);
                        //}
                        //else if (dr["نسبة الشراء"].ToString() != "0")
                        //{
                        //    gridView1.SetRowCellValue(rowHandl, gridView1.Columns["نسبة الشراء"], dr["نسبة الشراء"].ToString());
                        //    gridView1.SetRowCellValue(rowHandl, gridView1.Columns["خصم الشراء"], 0);
                        //}
                        gridView1.SetRowCellValue(rowHandl, gridView1.Columns["الزيادة العادية"], dr["الزيادة العادية"].ToString());
                        gridView1.SetRowCellValue(rowHandl, gridView1.Columns["الزيادة القطعية"], dr["الزيادة القطعية"].ToString());
                        //gridView1.SetRowCellValue(rowHandl, gridView1.Columns["ضريبة القيمة المضافة"], dr["ضريبة القيمة المضافة"].ToString());
                        gridView1.SetRowCellValue(rowHandl, gridView1.Columns["سعر الشراء"], dr["سعر الشراء"].ToString());
                        gridView1.SetRowCellValue(rowHandl, gridView1.Columns["ImportStorageReturnDetails_ID"], dr["ImportStorageReturnDetails_ID"].ToString());
                        gridView1.SetRowCellValue(rowHandl, gridView1.Columns["Price_Type"], dr["Price_Type"].ToString());

                        if (dr["متر/قطعة"].ToString() != "")
                        {
                            totalMeter = Convert.ToDouble(dr["متر/قطعة"].ToString());
                        }

                        dbconnection2.Open();
                        q = "SELECT supplier_return_bill_details.Total_Meters as 'متر/قطعة' FROM supplier_return_bill INNER JOIN supplier_return_bill_details ON supplier_return_bill_details.ReturnBill_ID = supplier_return_bill.ReturnBill_ID  where supplier_return_bill.ImportStorageReturn_ID=" + ImportStorageReturnId + " and supplier_return_bill_details.Data_ID=" + dr["Data_ID"].ToString();
                        MySqlCommand comand2 = new MySqlCommand(q, dbconnection2);
                        MySqlDataReader dr2 = comand2.ExecuteReader();
                        while (dr2.Read())
                        {
                            if (dr2["متر/قطعة"].ToString() != "")
                            {
                                totalMeter -= Convert.ToDouble(dr2["متر/قطعة"].ToString());
                            }
                        }
                        dr2.Close();
                        dbconnection2.Close();
                        if (totalMeter >= 0)
                        {
                            gridView1.SetRowCellValue(rowHandl, gridView1.Columns["متر/قطعة"], totalMeter);
                        }
                    }
                }
                dr.Close();
            }
            #endregion

            gridView1.Columns[0].Visible = false;
            gridView1.Columns["BillData_ID"].Visible = false;
            gridView1.Columns["ImportStorageReturnDetails_ID"].Visible = false;
            gridView1.Columns["Price_Type"].Visible = false;

            if (gridView1.IsLastVisibleRow)
            {
                gridView1.FocusedRowHandle = gridView1.RowCount - 1;
            }

            for (int i = 3; i < gridView1.Columns.Count; i++)
            {
                gridView1.Columns[i].Width = 120;
            }
            gridView1.Columns[1].Width = 180;
            gridView1.Columns[3].Width = 300;

            double totalB = 0;
            double totalA = 0;
            for (int i = 0; i < gridView1.RowCount; i++)
            {
                if (gridView1.GetRowCellDisplayText(i, "السعر") != "")
                {
                    totalB += Convert.ToDouble(gridView1.GetRowCellDisplayText(i, "السعر")) * Convert.ToDouble(gridView1.GetRowCellDisplayText(i, "متر/قطعة"));
                }
                if (gridView1.GetRowCellDisplayText(i, "سعر الشراء") != "")
                {
                    totalA += Convert.ToDouble(gridView1.GetRowCellDisplayText(i, "سعر الشراء")) * Convert.ToDouble(gridView1.GetRowCellDisplayText(i, "متر/قطعة"));
                }
            }
            labTotalPriceBD.Text = totalB.ToString();
            labTotalPrice.Text = totalA.ToString();
        }

        public void displayReturnPermissionDetails(int storageImportPermissionIdF)
        {
            //,supplier_bill_details.Purchasing_Ratio as 'نسبة الشراء'
            string q = "SELECT supplier_bill_details.Data_ID,data.Code as 'الكود',type.Type_Name as 'النوع',concat(product.Product_Name,' ',COALESCE(color.Color_Name,''),' ',data.Description,' ',groupo.Group_Name,' ',factory.Factory_Name,' ',COALESCE(size.Size_Value,''),' ',COALESCE(sort.Sort_Value,'')) as 'الاسم',supplier_bill_details.Price_Type as 'نوع السعر',supplier_bill_details.Price as 'السعر',supplier_bill_details.Last_Price as 'السعر بالزيادة',supplier_bill_details.Purchasing_Discount as 'نسبة الخصم',supplier_bill_details.Normal_Increase as 'الزيادة العادية',supplier_bill_details.Categorical_Increase as 'الزيادة القطعية',supplier_bill_details.Purchasing_Price as 'سعر الشراء',supplier_bill_details.Total_Meters 'متر/قطعة',supplier_bill.Value_Additive_Tax as 'ضريبة القيمة المضافة','BillData_ID','ImportStorageReturnDetails_ID' FROM supplier_bill INNER JOIN supplier_bill_details ON supplier_bill_details.Bill_ID = supplier_bill.Bill_ID INNER JOIN data ON data.Data_ID = supplier_bill_details.Data_ID INNER JOIN type ON type.Type_ID = data.Type_ID INNER JOIN product ON product.Product_ID = data.Product_ID INNER JOIN factory ON data.Factory_ID = factory.Factory_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID LEFT JOIN color ON color.Color_ID = data.Color_ID LEFT JOIN size ON size.Size_ID = data.Size_ID LEFT JOIN sort ON sort.Sort_ID = data.Sort_ID where supplier_bill.StorageImportPermission_ID=" + storageImportPermissionIdF;
            MySqlDataAdapter da = new MySqlDataAdapter(q, dbconnection);
            DataTable dt = new DataTable();
            da.Fill(dt);
            gridControl2.DataSource = dt;
            gridView2.Columns[0].Visible = false;
            gridView2.Columns["BillData_ID"].Visible = false;
            gridView2.Columns["ImportStorageReturnDetails_ID"].Visible = false;
            gridView2.Columns["نوع السعر"].Visible = false;
            gridView2.Columns["ضريبة القيمة المضافة"].Visible = false;

            if (gridView2.IsLastVisibleRow)
            {
                gridView2.FocusedRowHandle = gridView2.RowCount - 1;
            }

            for (int i = 3; i < gridView2.Columns.Count; i++)
            {
                gridView2.Columns[i].Width = 120;
            }
            gridView2.Columns[1].Width = 180;
            gridView2.Columns[3].Width = 300;
            
            double totalAllB = 0;
            double totalAllA = 0;
            double totalAllDiscount = 0;
            double totalAllCatInc = 0;
            double addabVal = 0;
            for (int i = 0; i < gridView2.RowCount; i++)
            {
                double totalB = 0;
                double totalA = 0;
                double totalNormInc = 0;
                double totalDiscount = 0;
                double totalCatInc = 0;
                addabVal = Convert.ToDouble(gridView2.GetRowCellDisplayText(0, "ضريبة القيمة المضافة"));
                if (gridView2.GetRowCellDisplayText(i, "الزيادة العادية") != "")
                {
                    totalNormInc = Convert.ToDouble(gridView2.GetRowCellDisplayText(i, "الزيادة العادية"));
                }
                else
                {
                    totalNormInc = 0;
                }
                if (gridView2.GetRowCellDisplayText(i, "الزيادة القطعية") != "")
                {
                    totalCatInc = Convert.ToDouble(gridView2.GetRowCellDisplayText(i, "الزيادة القطعية"));
                }
                else
                {
                    totalCatInc = 0;
                }
                totalB = (Convert.ToDouble(gridView2.GetRowCellDisplayText(i, "السعر")) + totalNormInc) * Convert.ToDouble(gridView2.GetRowCellDisplayText(i, "متر/قطعة"));
                totalDiscount = totalB * (Convert.ToDouble(gridView2.GetRowCellDisplayText(i, "نسبة الخصم")) / 100);
                totalCatInc = Convert.ToDouble(gridView2.GetRowCellDisplayText(i, "الزيادة القطعية")) * Convert.ToDouble(gridView2.GetRowCellDisplayText(i, "متر/قطعة"));
                totalA = (totalB - totalDiscount) + totalCatInc;

                totalAllB += totalB;
                totalAllDiscount += totalDiscount;
                totalAllCatInc += totalCatInc;
                totalAllA += totalA;
            }
            labelTotalB.Text = totalAllB.ToString("#.000");
            labelTotalDiscount.Text = totalAllDiscount.ToString("#.000");
            labelTotalCat.Text = totalAllCatInc.ToString("#.000");
            labelTotalA.Text = totalAllA.ToString("#.000");
            if (addabVal > 0)
            {
                txtAllTax.Text = addabVal.ToString("#.000");
            }
            else
            {
                txtAllTax.Text = "0.00";
            }
            labelTotalSafy.Text = (Convert.ToDouble(labelTotalA.Text) + (Convert.ToDouble(labelTotalA.Text) * (Convert.ToDouble(txtAllTax.Text) / 100))).ToString("#.000");
        }

        public void displayPermissionSupplier()
        {
            loadSupPerm = false;
            loadSup = false;

            #region with permission
            if (storageImportPermissionId > 0)
            {
                //    string query = "select supplier.Supplier_ID,supplier.Supplier_Name from supplier inner join supplier_bill on supplier.Supplier_ID=supplier_bill.Supplier_ID where supplier_bill.StorageImportPermission_ID=" + storageImportPermissionId;
                //    MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                //    DataTable dt = new DataTable();
                //    da.Fill(dt);
                //    comSupplier.DataSource = dt;
                //    comSupplier.DisplayMember = dt.Columns["Supplier_Name"].ToString();
                //    comSupplier.ValueMember = dt.Columns["Supplier_ID"].ToString();
                string query = "select supplier.Supplier_ID,supplier.Supplier_Name from import_storage_return INNER JOIN import_storage_return_supplier ON import_storage_return_supplier.ImportStorageReturn_ID = import_storage_return.ImportStorageReturn_ID INNER JOIN supplier ON import_storage_return_supplier.Supplier_ID = supplier.Supplier_ID where import_storage_return.ImportStorageReturn_ID=" + ImportStorageReturnId;
                MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                DataTable dt = new DataTable();
                da.Fill(dt);
                comSupplier.DataSource = dt;
                comSupplier.DisplayMember = dt.Columns["Supplier_Name"].ToString();
                comSupplier.ValueMember = dt.Columns["Supplier_ID"].ToString();
                comSupplier.SelectedIndex = -1;

                comSupPerm.DataSource = null;
                labelSupPerm.Visible = true;
                comSupPerm.Visible = true;
            }
            #endregion

            #region with out permission
            else
            {
                string query = "select supplier.Supplier_ID,supplier.Supplier_Name from import_storage_return INNER JOIN import_storage_return_supplier ON import_storage_return_supplier.ImportStorageReturn_ID = import_storage_return.ImportStorageReturn_ID INNER JOIN supplier ON import_storage_return_supplier.Supplier_ID = supplier.Supplier_ID where import_storage_return.ImportStorageReturn_ID=" + ImportStorageReturnId;
                MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                DataTable dt = new DataTable();
                da.Fill(dt);
                comSupplier.DataSource = dt;
                comSupplier.DisplayMember = dt.Columns["Supplier_Name"].ToString();
                comSupplier.ValueMember = dt.Columns["Supplier_ID"].ToString();

                comSupPerm.DataSource = null;
                labelSupPerm.Visible = false;
                comSupPerm.Visible = false;
            }
            #endregion

            loadSupPerm = true;
            loadSup = true;
        }

        //new bill clear all controls 
        public void newReturnBill()
        {
            row1 = null;
            rowHandle = -1;
            gridControl1.DataSource = null;
            gridControl2.DataSource = null;
            txtDiscount.Text = txtCategoricalIncrease.Text = txtCode.Text = "";
            txtPrice.Text = txtLastPrice.Text = txtNormalIncrease.Text = txtTotalMeter.Text = "";
            txtPurchasePrice.Text = labTotalPrice.Text = labTotalPriceBD.Text = labelTotalB.Text = labelTotalA.Text= labelTotalSafy.Text = labelTotalDiscount.Text = labelTotalCat.Text = "";
            txtTax.Text = "0";
            loaded = false;
            txtAllTax.Text = "0.00";
            loaded = true;
        }

        private void radioList_CheckedChanged(object sender, EventArgs e)
        {
            /*try
            {
                if (row1 != null)
                {
                    txtDiscount.Text = row1["خصم الشراء"].ToString();
                    label7.Text = "خصم الشراء";
                    txtNormalIncrease.Visible = true;
                    txtCategoricalIncrease.Visible = true;
                    label8.Visible = true;
                    label6.Visible = true;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }*/
        }

        private void radioQata3y_CheckedChanged(object sender, EventArgs e)
        {
            /*try
            {
                if (row1 != null)
                {
                    txtDiscount.Text = row1["نسبة الشراء"].ToString();
                    label7.Text = "نسبة الشراء";
                    txtNormalIncrease.Visible = false;
                    txtCategoricalIncrease.Visible = false;
                    label8.Visible = false;
                    label6.Visible = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }*/
        }

        private void txtAllTax_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (loaded)
                {
                    if (txtAllTax.Text != "")
                    {
                        double VAT;
                        if (double.TryParse(txtAllTax.Text, out VAT))
                        {
                            labelTotalSafy.Text = (Convert.ToDouble(labelTotalA.Text) + (Convert.ToDouble(labelTotalA.Text) * (Convert.ToDouble(txtAllTax.Text) / 100))).ToString("#.000");
                        }
                        else
                        {
                            labelTotalSafy.Text = "";
                        }
                    }
                    else
                    {
                        labelTotalSafy.Text = "";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void DecreaseSupplierAccount()
        {
            double totalSafy = Convert.ToDouble(labelTotalSafy.Text);
            string query = "select Money from supplier_rest_money where Supplier_ID=" + comSupplier.SelectedValue.ToString();
            MySqlCommand com = new MySqlCommand(query, dbconnection);
            if (com.ExecuteScalar() != null)
            {
                double restMoney = Convert.ToDouble(com.ExecuteScalar());
                query = "update supplier_rest_money set Money=" + (restMoney - totalSafy) + " where Supplier_ID=" + comSupplier.SelectedValue.ToString();
                com = new MySqlCommand(query, dbconnection);
            }
            else
            {
                query = "insert into supplier_rest_money (Supplier_ID,Money) values (@Supplier_ID,@Money)";
                com = new MySqlCommand(query, dbconnection);
                com.Parameters.Add("@Supplier_ID", MySqlDbType.Int16, 11).Value = comSupplier.SelectedValue;
                com.Parameters.Add("@Money", MySqlDbType.Decimal, 10).Value = -1 * totalSafy;
            }
            com.ExecuteNonQuery();
        }
    }
}

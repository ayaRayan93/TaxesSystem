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
        DataRow row1 = null;
        int rowHandle = -1;
        int ImportStorageReturnId = 0;
        int storageImportPermissionId = 0;
        int BillNo = 1;
        bool loadSup = false;
        bool loadSupPerm = false;

        public Supplier_Return_Bill2(MainForm mainForm)
        {
            InitializeComponent();
            dbconnection = new MySqlConnection(connection.connectionString);
            dbconnection2 = new MySqlConnection(connection.connectionString);
        }
        //events
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
            try
            {
                if (loaded)
                {
                    txtBillNumber.Text = "";
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
        }

        private void txtBillNumber_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtBillNumber.Text != "" && comStore.SelectedValue != null)
                {
                    int billNum = 0;
                    if (int.TryParse(txtBillNumber.Text, out billNum))
                    {
                        dbconnection.Open();
                        newReturnBill();

                        string query = "select ImportStorageReturn_ID from import_storage_return where Store_ID=" + comStore.SelectedValue.ToString() + " and Returned_Permission_Number=" + billNum;
                        MySqlCommand com = new MySqlCommand(query, dbconnection);
                        if (com.ExecuteScalar() != null)
                        {
                            ImportStorageReturnId = Convert.ToInt16(com.ExecuteScalar().ToString());
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
                                storageImportPermissionId = Convert.ToInt16(com.ExecuteScalar().ToString());
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

        private void comSupplier_SelectedValueChanged(object sender, EventArgs e)
        {
            if (loadSup)
            {
                try
                {
                    string query = "SELECT import_storage_return_supplier.Supplier_Permission_Number,import_storage_return_supplier.ImportStorageReturnSupplier_ID FROM import_storage_return_supplier where import_storage_return_supplier.Supplier_ID=" + comSupplier.SelectedValue.ToString() + " and import_storage_return_supplier.ImportStorageReturn_ID=" + ImportStorageReturnId;
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
                    #region error
                    //string q = "SELECT import_storage_return_details.Data_ID,data.Code as 'الكود',concat(product.Product_Name,' - ',type.Type_Name,' - ',factory.Factory_Name,' - ',groupo.Group_Name,' ',COALESCE(color.Color_Name,''),' ',COALESCE(size.Size_Value,''),' ',COALESCE(sort.Sort_Value,'')) as 'الاسم',purchasing_price.Price as 'السعر',purchasing_price.ProfitRatio as 'نسبة الشراء',purchasing_price.Purchasing_Discount as 'خصم الشراء',purchasing_price.Normal_Increase as 'الزيادة العادية',purchasing_price.Categorical_Increase as 'الزيادة القطعية', 'ضريبة القيمة المضافة',purchasing_price.Purchasing_Price as 'سعر الشراء',import_storage_return_details.Total_Meters 'متر/قطعة','BillData_ID','ImportStorageReturnDetails_ID' FROM import_storage_return inner join import_storage_return_supplier on import_storage_return.ImportStorageReturn_ID=import_storage_return_supplier.ImportStorageReturn_ID INNER JOIN import_storage_return_details ON import_storage_return_details.ImportStorageReturnSupplier_ID = import_storage_return_supplier.ImportStorageReturnSupplier_ID INNER JOIN data ON data.Data_ID = import_storage_return_details.Data_ID INNER JOIN type ON type.Type_ID = data.Type_ID INNER JOIN product ON product.Product_ID = data.Product_ID INNER JOIN factory ON data.Factory_ID = factory.Factory_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID LEFT JOIN color ON color.Color_ID = data.Color_ID LEFT JOIN size ON size.Size_ID = data.Size_ID LEFT JOIN sort ON sort.Sort_ID = data.Sort_ID inner join purchasing_price on purchasing_price.Data_ID=import_storage_return_details.Data_ID where import_storage_return.ImportStorageReturn_ID=" + 0;
                    //MySqlDataAdapter da = new MySqlDataAdapter(q, dbconnection);
                    //DataTable dt = new DataTable();
                    //da.Fill(dt);
                    //gridControl1.DataSource = dt;

                    //q = "SELECT import_storage_return_details.Data_ID,data.Code as 'الكود',concat(product.Product_Name,' - ',type.Type_Name,' - ',factory.Factory_Name,' - ',groupo.Group_Name,' ',COALESCE(color.Color_Name,''),' ',COALESCE(size.Size_Value,''),' ',COALESCE(sort.Sort_Value,'')) as 'الاسم',purchasing_price.Price as 'السعر',purchasing_price.ProfitRatio as 'نسبة الشراء',purchasing_price.Purchasing_Discount as 'خصم الشراء',purchasing_price.Normal_Increase as 'الزيادة العادية',purchasing_price.Categorical_Increase as 'الزيادة القطعية', 'ضريبة القيمة المضافة',purchasing_price.Purchasing_Price as 'سعر الشراء',import_storage_return_details.Total_Meters 'متر/قطعة',import_storage_return_details.ImportStorageReturnDetails_ID FROM import_storage_return inner join import_storage_return_supplier on import_storage_return.ImportStorageReturn_ID=import_storage_return_supplier.ImportStorageReturn_ID INNER JOIN import_storage_return_details ON import_storage_return_details.ImportStorageReturnSupplier_ID = import_storage_return_supplier.ImportStorageReturnSupplier_ID INNER JOIN data ON data.Data_ID = import_storage_return_details.Data_ID INNER JOIN type ON type.Type_ID = data.Type_ID INNER JOIN product ON product.Product_ID = data.Product_ID INNER JOIN factory ON data.Factory_ID = factory.Factory_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID LEFT JOIN color ON color.Color_ID = data.Color_ID LEFT JOIN size ON size.Size_ID = data.Size_ID LEFT JOIN sort ON sort.Sort_ID = data.Sort_ID inner join purchasing_price on purchasing_price.Data_ID=import_storage_return_details.Data_ID where import_storage_return.ImportStorageReturn_ID=" + ImportStorageReturnId;
                    //MySqlCommand comand = new MySqlCommand(q, dbconnection);
                    //MySqlDataReader dr = comand.ExecuteReader();
                    //while (dr.Read())
                    //{
                    //    double totalMeter = 0;
                    //    gridView1.AddNewRow();
                    //    int rowHandl = gridView1.GetRowHandle(gridView1.DataRowCount);
                    //    if (gridView1.IsNewItemRow(rowHandl))
                    //    {
                    //        gridView1.SetRowCellValue(rowHandl, gridView1.Columns["Data_ID"], dr["Data_ID"].ToString());
                    //        gridView1.SetRowCellValue(rowHandl, gridView1.Columns["الكود"], dr["الكود"].ToString());
                    //        gridView1.SetRowCellValue(rowHandl, gridView1.Columns["الاسم"], dr["الاسم"].ToString());
                    //        gridView1.SetRowCellValue(rowHandl, gridView1.Columns["السعر"], dr["السعر"].ToString());
                    //        if (dr["خصم الشراء"].ToString() != "")
                    //        {
                    //            gridView1.SetRowCellValue(rowHandl, gridView1.Columns["خصم الشراء"], dr["خصم الشراء"].ToString());
                    //        }
                    //        else if (dr["نسبة الشراء"].ToString() != "")
                    //        {
                    //            gridView1.SetRowCellValue(rowHandl, gridView1.Columns["نسبة الشراء"], dr["نسبة الشراء"].ToString());
                    //        }
                    //        gridView1.SetRowCellValue(rowHandl, gridView1.Columns["الزيادة العادية"], dr["الزيادة العادية"].ToString());
                    //        gridView1.SetRowCellValue(rowHandl, gridView1.Columns["الزيادة القطعية"], dr["الزيادة القطعية"].ToString());
                    //        gridView1.SetRowCellValue(rowHandl, gridView1.Columns["ضريبة القيمة المضافة"], dr["ضريبة القيمة المضافة"].ToString());
                    //        gridView1.SetRowCellValue(rowHandl, gridView1.Columns["سعر الشراء"], dr["سعر الشراء"].ToString());
                    //        gridView1.SetRowCellValue(rowHandl, gridView1.Columns["ImportStorageReturnDetails_ID"], dr["ImportStorageReturnDetails_ID"].ToString());

                    //        if (dr["متر/قطعة"].ToString() != "")
                    //        {
                    //            totalMeter = Convert.ToDouble(dr["متر/قطعة"].ToString());
                    //        }

                    //        dbconnection2.Open();
                    //        q = "SELECT supplier_return_bill_details.Total_Meters as 'متر/قطعة' FROM supplier_return_bill INNER JOIN supplier_return_bill_details ON supplier_return_bill_details.ReturnBill_ID = supplier_return_bill.ReturnBill_ID  where supplier_return_bill.ImportStorageReturn_ID=" + ImportStorageReturnId + " and supplier_return_bill_details.Data_ID=" + dr["Data_ID"].ToString();
                    //        MySqlCommand comand2 = new MySqlCommand(q, dbconnection2);
                    //        MySqlDataReader dr2 = comand2.ExecuteReader();
                    //        while (dr2.Read())
                    //        {
                    //            if (dr2["متر/قطعة"].ToString() != "")
                    //            {
                    //                totalMeter -= Convert.ToDouble(dr2["متر/قطعة"].ToString());
                    //            }
                    //        }
                    //        dr2.Close();
                    //        dbconnection2.Close();
                    //        if (totalMeter >= 0)
                    //        {
                    //            gridView1.SetRowCellValue(rowHandl, gridView1.Columns["متر/قطعة"], totalMeter);
                    //        }
                    //    }
                    //}
                    //dr.Close();

                    //gridView1.Columns[0].Visible = false;
                    //gridView1.Columns["BillData_ID"].Visible = false;
                    //gridView1.Columns["ImportStorageReturnDetails_ID"].Visible = false;

                    //if (gridView1.IsLastVisibleRow)
                    //{
                    //    gridView1.FocusedRowHandle = gridView1.RowCount - 1;
                    //}

                    //double totalB = 0;
                    //double totalA = 0;
                    //for (int i = 0; i < gridView1.RowCount; i++)
                    //{
                    //    totalB += Convert.ToDouble(gridView1.GetRowCellDisplayText(i, "السعر")) * Convert.ToDouble(gridView1.GetRowCellDisplayText(i, "متر/قطعة"));
                    //    totalA += Convert.ToDouble(gridView1.GetRowCellDisplayText(i, "سعر الشراء")) * Convert.ToDouble(gridView1.GetRowCellDisplayText(i, "متر/قطعة"));
                    //}
                    //labTotalPriceBD.Text = totalB.ToString();
                    //labTotalPrice.Text = totalA.ToString(); 
                    #endregion
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
                txtBillNumber.Text = "";
                loadSup = false;
                loadSupPerm = false;
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
                txtPurchasePrice.Text = row1["سعر الشراء"].ToString();
                txtNormalIncrease.Text = row1["الزيادة العادية"].ToString();
                txtCategoricalIncrease.Text = row1["الزيادة القطعية"].ToString();
                txtTotalMeter.Text = row1["متر/قطعة"].ToString();
                if (txtCategoricalIncrease.Text == "" && txtNormalIncrease.Text == "")
                {
                    txtCategoricalIncrease.Text = txtNormalIncrease.Text = "0";
                }
                if (row1["خصم الشراء"].ToString() != "")
                {
                    txtDiscount.Text = row1["خصم الشراء"].ToString();
                    label7.Text = "خصم الشراء";
                    txtNormalIncrease.Visible = true;
                    txtCategoricalIncrease.Visible = true;
                    label8.Visible = true;
                    label6.Visible = true;
                }
                else if (row1["نسبة الشراء"].ToString() != "")
                {
                    txtDiscount.Text = row1["نسبة الشراء"].ToString();
                    label7.Text = "نسبة الشراء";
                    txtNormalIncrease.Visible = false;
                    txtCategoricalIncrease.Visible = false;
                    label8.Visible = false;
                    label6.Visible = false;
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
                            txtPurchasePrice.Text = (calPurchasesPrice() + VAT) + "";
                        }
                        else
                        {
                            txtPurchasePrice.Text = "";
                        }
                    }
                    else
                    {
                        txtPurchasePrice.Text = "";
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
                    if (!IsAdded())
                    {
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
                                            gridView2.SetRowCellValue(rowHandl, gridView2.Columns["السعر"], price);
                                            if (row1["خصم الشراء"].ToString() != "")
                                            {
                                                gridView2.SetRowCellValue(rowHandl, gridView2.Columns["خصم الشراء"], PurchaseDiscount);
                                                gridView2.SetRowCellValue(rowHandl, gridView2.Columns["الزيادة العادية"], NormalIncrease);
                                                gridView2.SetRowCellValue(rowHandl, gridView2.Columns["الزيادة القطعية"], Categorical_Increase);
                                            }
                                            else if (row1["نسبة الشراء"].ToString() != "")
                                            {
                                                gridView2.SetRowCellValue(rowHandl, gridView2.Columns["نسبة الشراء"], PurchaseDiscount);
                                                gridView2.SetRowCellValue(rowHandl, gridView2.Columns["الزيادة العادية"], "");
                                                gridView2.SetRowCellValue(rowHandl, gridView2.Columns["الزيادة القطعية"], "");
                                            }
                                            gridView2.SetRowCellValue(rowHandl, gridView2.Columns["ضريبة القيمة المضافة"], tax);
                                            gridView2.SetRowCellValue(rowHandl, gridView2.Columns["سعر الشراء"], purchasePrice);
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

                                            double totalB = 0;
                                            double totalA = 0;
                                            for (int i = 0; i < gridView2.RowCount; i++)
                                            {
                                                totalB += Convert.ToDouble(gridView2.GetRowCellDisplayText(i, "السعر")) * Convert.ToDouble(gridView2.GetRowCellDisplayText(i, "متر/قطعة"));
                                                totalA += Convert.ToDouble(gridView2.GetRowCellDisplayText(i, "سعر الشراء")) * Convert.ToDouble(gridView2.GetRowCellDisplayText(i, "متر/قطعة"));
                                            }

                                            labelTotalB.Text = totalB.ToString();
                                            labelTotalA.Text = totalA.ToString();
                                            row1 = null;
                                            rowHandle = -1;
                                            txtTotalMeter.Text = txtCode.Text = "";
                                            txtDiscount.Text = txtNormalIncrease.Text = txtCategoricalIncrease.Text = "0";
                                            txtPrice.Text = txtPurchasePrice.Text = "";
                                            txtTax.Text = "0";
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
                        MessageBox.Show("هذا البند تم اضافتة من قبل");
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
                            if (gridView1.GetRowCellDisplayText(i, "خصم الشراء") != "")
                            {
                                gridView2.SetRowCellValue(rowHandl, gridView2.Columns["خصم الشراء"], gridView1.GetRowCellDisplayText(i, "خصم الشراء"));
                            }
                            else if (gridView1.GetRowCellDisplayText(i, "نسبة الشراء") != "")
                            {
                                gridView2.SetRowCellValue(rowHandl, gridView2.Columns["نسبة الشراء"], gridView1.GetRowCellDisplayText(i, "نسبة الشراء"));
                            }
                            gridView2.SetRowCellValue(rowHandl, gridView2.Columns["الزيادة العادية"], gridView1.GetRowCellDisplayText(i, "الزيادة العادية"));
                            gridView2.SetRowCellValue(rowHandl, gridView2.Columns["الزيادة القطعية"], gridView1.GetRowCellDisplayText(i, "الزيادة القطعية"));
                            gridView2.SetRowCellValue(rowHandl, gridView2.Columns["ضريبة القيمة المضافة"], 0);
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
                        }
                    }
                }

                if (gridView2.IsLastVisibleRow)
                {
                    gridView2.FocusedRowHandle = gridView2.RowCount - 1;
                }

                double totalB = 0;
                double totalA = 0;
                for (int j = 0; j < gridView2.RowCount; j++)
                {
                    totalB += Convert.ToDouble(gridView2.GetRowCellDisplayText(j, "السعر")) * Convert.ToDouble(gridView2.GetRowCellDisplayText(j, "متر/قطعة"));
                    totalA += Convert.ToDouble(gridView2.GetRowCellDisplayText(j, "سعر الشراء")) * Convert.ToDouble(gridView2.GetRowCellDisplayText(j, "متر/قطعة"));
                }
                
                labelTotalB.Text = totalB.ToString();
                labelTotalA.Text = totalA.ToString();
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
                    gridView2.DeleteRow(gridView2.FocusedRowHandle);
                    
                    double totalB = 0;
                    double totalA = 0;
                    for (int i = 0; i < gridView2.RowCount; i++)
                    {
                        totalB += Convert.ToDouble(gridView2.GetRowCellDisplayText(i, "السعر")) * Convert.ToDouble(gridView2.GetRowCellDisplayText(i, "متر/قطعة"));
                        totalA += Convert.ToDouble(gridView2.GetRowCellDisplayText(i, "سعر الشراء")) * Convert.ToDouble(gridView2.GetRowCellDisplayText(i, "متر/قطعة"));
                    }
                    labelTotalB.Text = totalB.ToString();
                    labelTotalA.Text = totalA.ToString();
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
                if (int.TryParse(txtBillNumber.Text, out billNum))
                {
                    if (gridView2.RowCount > 0 && comStore.SelectedValue != null && comSupplier.SelectedValue != null)
                    {
                        dbconnection.Open();
                        BillNo = 1;
                        string query = "select Return_Bill_No from supplier_return_bill where Supplier_ID=" + comSupplier.SelectedValue.ToString() + " ORDER BY ReturnBill_ID DESC LIMIT 1";
                        MySqlCommand com = new MySqlCommand(query, dbconnection);
                        if (com.ExecuteScalar() != null)
                        {
                            BillNo = Convert.ToInt16(com.ExecuteScalar());
                            BillNo++;
                        }

                        query = "insert into supplier_return_bill (Store_ID,Return_Bill_No,Supplier_ID,Returned_Permission_Number,Total_Price_BD,Total_Price_AD,Date,Supplier_Permission_Number,ImportStorageReturn_ID,Employee_ID) values (@Store_ID,@Return_Bill_No,@Supplier_ID,@Returned_Permission_Number,@Total_Price_BD,@Total_Price_AD,@Date,@Supplier_Permission_Number,@ImportStorageReturn_ID,@Employee_ID)";
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
                        com.Parameters["@Total_Price_AD"].Value = Convert.ToDouble(labelTotalA.Text);
                        com.Parameters.Add("@Date", MySqlDbType.DateTime);
                        com.Parameters["@Date"].Value = DateTime.Now;
                        com.Parameters.Add("@Supplier_Permission_Number", MySqlDbType.Int16);
                        com.Parameters["@Supplier_Permission_Number"].Value = comSupPerm.Text;
                        com.Parameters.Add("@ImportStorageReturn_ID", MySqlDbType.Int16);
                        com.Parameters["@ImportStorageReturn_ID"].Value = ImportStorageReturnId;
                        com.Parameters.Add("@Employee_ID", MySqlDbType.Int16);
                        com.Parameters["@Employee_ID"].Value = UserControl.EmpID;
                        com.ExecuteNonQuery();

                        query = "select ReturnBill_ID from supplier_return_bill ORDER BY ReturnBill_ID DESC LIMIT 1 ";
                        com = new MySqlCommand(query, dbconnection);
                        if (com.ExecuteScalar() != null)
                        {
                            returnBillId = (int)com.ExecuteScalar();
                        }
                        
                        for (int i = 0; i < gridView2.RowCount; i++)
                        {
                            query = "insert into supplier_return_bill_details (ReturnBill_ID,Data_ID,Price,Purchasing_Discount,Purchasing_Ratio,Normal_Increase,Categorical_Increase,Value_Additive_Tax,Purchasing_Price,Total_Meters,BillData_ID,ImportStorageReturnDetails_ID) values (@ReturnBill_ID,@Data_ID,@Price,@Purchasing_Discount,@Purchasing_Ratio,@Normal_Increase,@Categorical_Increase,@Value_Additive_Tax,@Purchasing_Price,@Total_Meters,@BillData_ID,@ImportStorageReturnDetails_ID)";
                            com = new MySqlCommand(query, dbconnection);
                            com.Parameters.Add("@ReturnBill_ID", MySqlDbType.Int16);
                            com.Parameters["@ReturnBill_ID"].Value = returnBillId;
                            com.Parameters.Add("@Data_ID", MySqlDbType.Int16);
                            com.Parameters["@Data_ID"].Value = gridView2.GetRowCellDisplayText(i, gridView2.Columns["Data_ID"]);
                            com.Parameters.Add("@Price", MySqlDbType.Decimal);
                            com.Parameters["@Price"].Value = gridView2.GetRowCellDisplayText(i, gridView2.Columns["السعر"]);
                            if (gridView2.GetRowCellDisplayText(i, gridView2.Columns["خصم الشراء"]) != "")
                            {
                                com.Parameters.Add("@Purchasing_Discount", MySqlDbType.Decimal);
                                com.Parameters["@Purchasing_Discount"].Value = gridView2.GetRowCellDisplayText(i, gridView2.Columns["خصم الشراء"]);
                                com.Parameters.Add("@Purchasing_Ratio", MySqlDbType.Decimal);
                                com.Parameters["@Purchasing_Ratio"].Value = null;
                                com.Parameters.Add("@Normal_Increase", MySqlDbType.Decimal);
                                com.Parameters["@Normal_Increase"].Value = gridView2.GetRowCellDisplayText(i, gridView2.Columns["الزيادة العادية"]);
                                com.Parameters.Add("@Categorical_Increase", MySqlDbType.Decimal);
                                com.Parameters["@Categorical_Increase"].Value = gridView2.GetRowCellDisplayText(i, gridView2.Columns["الزيادة القطعية"]);
                            }
                            else if (gridView2.GetRowCellDisplayText(i, gridView2.Columns["نسبة الشراء"]) != "")
                            {
                                com.Parameters.Add("@Purchasing_Discount", MySqlDbType.Decimal);
                                com.Parameters["@Purchasing_Discount"].Value = null;
                                com.Parameters.Add("@Purchasing_Ratio", MySqlDbType.Decimal);
                                com.Parameters["@Purchasing_Ratio"].Value = gridView2.GetRowCellDisplayText(i, gridView2.Columns["نسبة الشراء"]);
                                com.Parameters.Add("@Normal_Increase", MySqlDbType.Decimal);
                                com.Parameters["@Normal_Increase"].Value = null;
                                com.Parameters.Add("@Categorical_Increase", MySqlDbType.Decimal);
                                com.Parameters["@Categorical_Increase"].Value = null;
                            }
                            com.Parameters.Add("@Value_Additive_Tax", MySqlDbType.Decimal);
                            com.Parameters["@Value_Additive_Tax"].Value = gridView2.GetRowCellDisplayText(i, gridView2.Columns["ضريبة القيمة المضافة"]);
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
                            if (gridView2.GetRowCellDisplayText(i, gridView2.Columns["خصم الشراء"]) != "")
                            {
                                double lastPrice = 0;
                                lastPrice = (Convert.ToDouble(gridView2.GetRowCellDisplayText(i, gridView2.Columns["سعر الشراء"])) * 100) / (100 - Convert.ToDouble(gridView2.GetRowCellDisplayText(i, gridView2.Columns["خصم الشراء"])));
                                SupplierReturnBill_Items item = new SupplierReturnBill_Items() { Code = gridView2.GetRowCellDisplayText(i, gridView2.Columns["الكود"]), Product_Type = gridView2.GetRowCellDisplayText(i, gridView2.Columns["النوع"]), Product_Name = gridView2.GetRowCellDisplayText(i, gridView2.Columns["الاسم"]), Total_Meters = Convert.ToDouble(gridView2.GetRowCellDisplayText(i, gridView2.Columns["متر/قطعة"])), PriceB = Convert.ToDouble(gridView2.GetRowCellDisplayText(i, gridView2.Columns["السعر"])), Discount = Convert.ToDouble(gridView2.GetRowCellDisplayText(i, gridView2.Columns["خصم الشراء"])), Last_Price = lastPrice, Normal_Increase = Convert.ToDouble(gridView2.GetRowCellDisplayText(i, gridView2.Columns["الزيادة العادية"])), Categorical_Increase = Convert.ToDouble(gridView2.GetRowCellDisplayText(i, gridView2.Columns["الزيادة القطعية"])), PriceA = Convert.ToDouble(gridView2.GetRowCellDisplayText(i, gridView2.Columns["سعر الشراء"])) };
                                bi.Add(item);
                            }
                            else if (gridView2.GetRowCellDisplayText(i, gridView2.Columns["نسبة الشراء"]) != "")
                            {
                                //, Normal_Increase = Convert.ToDouble(gridView2.GetRowCellDisplayText(i, gridView2.Columns["الزيادة العادية"])), Categorical_Increase = Convert.ToDouble(gridView2.GetRowCellDisplayText(i, gridView2.Columns["الزيادة القطعية"]))
                                SupplierReturnBill_Items item = new SupplierReturnBill_Items() { Code = gridView2.GetRowCellDisplayText(i, gridView2.Columns["الكود"]), Product_Type = gridView2.GetRowCellDisplayText(i, gridView2.Columns["النوع"]), Product_Name = gridView2.GetRowCellDisplayText(i, gridView2.Columns["الاسم"]), Total_Meters = Convert.ToDouble(gridView2.GetRowCellDisplayText(i, gridView2.Columns["متر/قطعة"])), PriceB = Convert.ToDouble(gridView2.GetRowCellDisplayText(i, gridView2.Columns["السعر"])), Discount = Convert.ToDouble(gridView2.GetRowCellDisplayText(i, gridView2.Columns["نسبة الشراء"])), Last_Price = Convert.ToDouble(gridView2.GetRowCellDisplayText(i, gridView2.Columns["سعر الشراء"])), PriceA = Convert.ToDouble(gridView2.GetRowCellDisplayText(i, gridView2.Columns["سعر الشراء"])) };
                                bi.Add(item);
                            }
                            addabtiveTax += Convert.ToDouble(gridView2.GetRowCellDisplayText(i, gridView2.Columns["ضريبة القيمة المضافة"]));
                        }
                        Report_SupplierReturnBill f = new Report_SupplierReturnBill();
                        f.PrintInvoice(storeName, BillNo.ToString(), comSupplier.Text, billNum.ToString(), comSupPerm.Text, Convert.ToDouble(labelTotalA.Text), addabtiveTax, bi);
                        f.ShowDialog(); 
                        #endregion

                        BillNo = 1;
                        txtBillNumber.Text = "";
                        loadSup = false;
                        loadSupPerm = false;
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
            try
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
                            if (gridView2.GetRowCellDisplayText(i, gridView2.Columns["خصم الشراء"]) != "")
                            {
                                double lastPrice = 0;
                                lastPrice = (Convert.ToDouble(gridView2.GetRowCellDisplayText(i, gridView2.Columns["سعر الشراء"])) * 100) / (100 - Convert.ToDouble(gridView2.GetRowCellDisplayText(i, gridView2.Columns["خصم الشراء"])));
                                SupplierReturnBill_Items item = new SupplierReturnBill_Items() { Code = gridView2.GetRowCellDisplayText(i, gridView2.Columns["الكود"]), Product_Type = gridView2.GetRowCellDisplayText(i, gridView2.Columns["النوع"]), Product_Name = gridView2.GetRowCellDisplayText(i, gridView2.Columns["الاسم"]), Total_Meters = Convert.ToDouble(gridView2.GetRowCellDisplayText(i, gridView2.Columns["متر/قطعة"])), PriceB = Convert.ToDouble(gridView2.GetRowCellDisplayText(i, gridView2.Columns["السعر"])), Discount = Convert.ToDouble(gridView2.GetRowCellDisplayText(i, gridView2.Columns["خصم الشراء"])), Last_Price = lastPrice, Normal_Increase = Convert.ToDouble(gridView2.GetRowCellDisplayText(i, gridView2.Columns["الزيادة العادية"])), Categorical_Increase = Convert.ToDouble(gridView2.GetRowCellDisplayText(i, gridView2.Columns["الزيادة القطعية"])), PriceA = Convert.ToDouble(gridView2.GetRowCellDisplayText(i, gridView2.Columns["سعر الشراء"])) };
                                bi.Add(item);
                            }
                            else if (gridView2.GetRowCellDisplayText(i, gridView2.Columns["نسبة الشراء"]) != "")
                            {
                                SupplierReturnBill_Items item = new SupplierReturnBill_Items() { Code = gridView2.GetRowCellDisplayText(i, gridView2.Columns["الكود"]), Product_Type = gridView2.GetRowCellDisplayText(i, gridView2.Columns["النوع"]), Product_Name = gridView2.GetRowCellDisplayText(i, gridView2.Columns["الاسم"]), Total_Meters = Convert.ToDouble(gridView2.GetRowCellDisplayText(i, gridView2.Columns["متر/قطعة"])), PriceB = Convert.ToDouble(gridView2.GetRowCellDisplayText(i, gridView2.Columns["السعر"])), Discount = Convert.ToDouble(gridView2.GetRowCellDisplayText(i, gridView2.Columns["نسبة الشراء"])), Last_Price = Convert.ToDouble(gridView2.GetRowCellDisplayText(i, gridView2.Columns["سعر الشراء"])), Normal_Increase = Convert.ToDouble(gridView2.GetRowCellDisplayText(i, gridView2.Columns["الزيادة العادية"])), Categorical_Increase = Convert.ToDouble(gridView2.GetRowCellDisplayText(i, gridView2.Columns["الزيادة القطعية"])), PriceA = Convert.ToDouble(gridView2.GetRowCellDisplayText(i, gridView2.Columns["سعر الشراء"])) };
                                bi.Add(item);
                            }
                            addabtiveTax += Convert.ToDouble(gridView2.GetRowCellDisplayText(i, gridView2.Columns["ضريبة القيمة المضافة"]));
                        }
                        Report_SupplierReturnBill f = new Report_SupplierReturnBill();
                        f.PrintInvoice(storeName, BillNo.ToString(), comSupplier.Text, billNum.ToString(), comSupPerm.Text, Convert.ToDouble(labelTotalA.Text), addabtiveTax, bi);
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
            dbconnection.Close();
        }

        //functions
        bool IsAdded()
        {
            for (int i = 0; i < gridView2.RowCount; i++)
            {
                DataRow row3 = gridView2.GetDataRow(i);
                if ((row1["BillData_ID"].ToString() == row3["BillData_ID"].ToString()))
                    return true;
            }
            return false;
        }

        public double calPurchasesPrice()
        {
            double price = double.Parse(txtPrice.Text);
            double PurchasesPercent = double.Parse(txtDiscount.Text);
            if (row1["نسبة الشراء"].ToString() != "")
            {
                return price + (price * PurchasesPercent / 100.0);
            }
            else if(row1["خصم الشراء"].ToString() != "")
            {
                double NormalPercent = double.Parse(txtNormalIncrease.Text);
                double unNormalPercent = double.Parse(txtCategoricalIncrease.Text);
                double PurchasesPrice = (price + NormalPercent) - ((price + NormalPercent) * PurchasesPercent / 100.0);
                PurchasesPrice = PurchasesPrice + unNormalPercent;
                return PurchasesPrice;
            }
            else
            {
                return 0;
            }
        }

        public void displayPermissionDetails()
        {
            #region with permission
            if (storageImportPermissionId > 0)
            {
                string q = "SELECT supplier_bill_details.Data_ID,data.Code as 'الكود',type.Type_Name as 'النوع',concat(product.Product_Name,' ',COALESCE(color.Color_Name,''),' ',data.Description,' ',groupo.Group_Name,' ',factory.Factory_Name,' ',COALESCE(size.Size_Value,''),' ',COALESCE(sort.Sort_Value,'')) as 'الاسم',supplier_bill_details.Price as 'السعر',supplier_bill_details.Purchasing_Ratio as 'نسبة الشراء',supplier_bill_details.Purchasing_Discount as 'خصم الشراء',supplier_bill_details.Normal_Increase as 'الزيادة العادية',supplier_bill_details.Categorical_Increase as 'الزيادة القطعية',supplier_bill_details.Value_Additive_Tax as 'ضريبة القيمة المضافة',supplier_bill_details.Purchasing_Price as 'سعر الشراء',supplier_bill_details.Total_Meters 'متر/قطعة','BillData_ID','ImportStorageReturnDetails_ID' FROM supplier_bill INNER JOIN supplier_bill_details ON supplier_bill_details.Bill_ID = supplier_bill.Bill_ID INNER JOIN import_storage_return_details ON supplier_bill_details.Data_ID = import_storage_return_details.Data_ID INNER JOIN data ON data.Data_ID = supplier_bill_details.Data_ID INNER JOIN type ON type.Type_ID = data.Type_ID INNER JOIN product ON product.Product_ID = data.Product_ID INNER JOIN factory ON data.Factory_ID = factory.Factory_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID LEFT JOIN color ON color.Color_ID = data.Color_ID LEFT JOIN size ON size.Size_ID = data.Size_ID LEFT JOIN sort ON sort.Sort_ID = data.Sort_ID where supplier_bill.StorageImportPermission_ID=" + 0;
                MySqlDataAdapter da = new MySqlDataAdapter(q, dbconnection);
                DataTable dt = new DataTable();
                da.Fill(dt);
                gridControl1.DataSource = dt;

                q = "SELECT supplier_bill_details.Data_ID,data.Code as 'الكود',type.Type_Name as 'النوع',concat(product.Product_Name,' ',COALESCE(color.Color_Name,''),' ',data.Description,' ',groupo.Group_Name,' ',factory.Factory_Name,' ',COALESCE(size.Size_Value,''),' ',COALESCE(sort.Sort_Value,'')) as 'الاسم',supplier_bill_details.Price as 'السعر',supplier_bill_details.Purchasing_Ratio as 'نسبة الشراء',supplier_bill_details.Purchasing_Discount as 'خصم الشراء',supplier_bill_details.Normal_Increase as 'الزيادة العادية',supplier_bill_details.Categorical_Increase as 'الزيادة القطعية',supplier_bill_details.Value_Additive_Tax as 'ضريبة القيمة المضافة',supplier_bill_details.Purchasing_Price as 'سعر الشراء',import_storage_return_details.Total_Meters 'متر/قطعة',supplier_bill_details.BillData_ID FROM supplier_bill INNER JOIN supplier_bill_details ON supplier_bill_details.Bill_ID = supplier_bill.Bill_ID INNER JOIN storage_import_permission ON supplier_bill.StorageImportPermission_ID = storage_import_permission.StorageImportPermission_ID INNER JOIN import_storage_return ON import_storage_return.StorageImportPermission_ID = storage_import_permission.StorageImportPermission_ID INNER JOIN import_storage_return_supplier ON import_storage_return_supplier.ImportStorageReturn_ID = import_storage_return.ImportStorageReturn_ID INNER JOIN import_storage_return_details ON import_storage_return_details.ImportStorageReturnSupplier_ID = import_storage_return_supplier.ImportStorageReturnSupplier_ID INNER JOIN data ON data.Data_ID = supplier_bill_details.Data_ID INNER JOIN type ON type.Type_ID = data.Type_ID INNER JOIN product ON product.Product_ID = data.Product_ID INNER JOIN factory ON data.Factory_ID = factory.Factory_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID LEFT JOIN color ON color.Color_ID = data.Color_ID LEFT JOIN size ON size.Size_ID = data.Size_ID LEFT JOIN sort ON sort.Sort_ID = data.Sort_ID where supplier_bill.StorageImportPermission_ID=" + storageImportPermissionId + " and import_storage_return_supplier.ImportStorageReturnSupplier_ID=" + comSupPerm.SelectedValue.ToString() + " and supplier_bill.Supplier_Permission_Number=" + comSupPerm.Text;
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
                        if (dr["خصم الشراء"].ToString() != "")
                        {
                            gridView1.SetRowCellValue(rowHandl, gridView1.Columns["خصم الشراء"], dr["خصم الشراء"].ToString());
                        }
                        else if (dr["نسبة الشراء"].ToString() != "")
                        {
                            gridView1.SetRowCellValue(rowHandl, gridView1.Columns["نسبة الشراء"], dr["نسبة الشراء"].ToString());
                        }
                        gridView1.SetRowCellValue(rowHandl, gridView1.Columns["الزيادة العادية"], dr["الزيادة العادية"].ToString());
                        gridView1.SetRowCellValue(rowHandl, gridView1.Columns["الزيادة القطعية"], dr["الزيادة القطعية"].ToString());
                        gridView1.SetRowCellValue(rowHandl, gridView1.Columns["ضريبة القيمة المضافة"], dr["ضريبة القيمة المضافة"].ToString());
                        gridView1.SetRowCellValue(rowHandl, gridView1.Columns["سعر الشراء"], dr["سعر الشراء"].ToString());
                        gridView1.SetRowCellValue(rowHandl, gridView1.Columns["BillData_ID"], dr["BillData_ID"].ToString());

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
                string q = "SELECT import_storage_return_details.Data_ID,data.Code as 'الكود',type.Type_Name as 'النوع',concat(product.Product_Name,' ',COALESCE(color.Color_Name,''),' ',data.Description,' ',groupo.Group_Name,' ',factory.Factory_Name,' ',COALESCE(size.Size_Value,''),' ',COALESCE(sort.Sort_Value,'')) as 'الاسم',purchasing_price.Price as 'السعر',purchasing_price.ProfitRatio as 'نسبة الشراء',purchasing_price.Purchasing_Discount as 'خصم الشراء',purchasing_price.Normal_Increase as 'الزيادة العادية',purchasing_price.Categorical_Increase as 'الزيادة القطعية', 'ضريبة القيمة المضافة',purchasing_price.Purchasing_Price as 'سعر الشراء',import_storage_return_details.Total_Meters 'متر/قطعة','BillData_ID','ImportStorageReturnDetails_ID' FROM import_storage_return inner join import_storage_return_supplier on import_storage_return.ImportStorageReturn_ID=import_storage_return_supplier.ImportStorageReturn_ID INNER JOIN import_storage_return_details ON import_storage_return_details.ImportStorageReturnSupplier_ID = import_storage_return_supplier.ImportStorageReturnSupplier_ID INNER JOIN data ON data.Data_ID = import_storage_return_details.Data_ID INNER JOIN type ON type.Type_ID = data.Type_ID INNER JOIN product ON product.Product_ID = data.Product_ID INNER JOIN factory ON data.Factory_ID = factory.Factory_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID LEFT JOIN color ON color.Color_ID = data.Color_ID LEFT JOIN size ON size.Size_ID = data.Size_ID LEFT JOIN sort ON sort.Sort_ID = data.Sort_ID inner join purchasing_price on purchasing_price.Data_ID=import_storage_return_details.Data_ID where import_storage_return.ImportStorageReturn_ID=" + 0;
                MySqlDataAdapter da = new MySqlDataAdapter(q, dbconnection);
                DataTable dt = new DataTable();
                da.Fill(dt);
                gridControl1.DataSource = dt;

                q = "SELECT import_storage_return_details.Data_ID,data.Code as 'الكود',type.Type_Name as 'النوع',concat(product.Product_Name,' ',COALESCE(color.Color_Name,''),' ',data.Description,' ',groupo.Group_Name,' ',factory.Factory_Name,' ',COALESCE(size.Size_Value,''),' ',COALESCE(sort.Sort_Value,'')) as 'الاسم',purchasing_price.Price as 'السعر',purchasing_price.ProfitRatio as 'نسبة الشراء',purchasing_price.Purchasing_Discount as 'خصم الشراء',purchasing_price.Normal_Increase as 'الزيادة العادية',purchasing_price.Categorical_Increase as 'الزيادة القطعية', 'ضريبة القيمة المضافة',purchasing_price.Purchasing_Price as 'سعر الشراء',import_storage_return_details.Total_Meters 'متر/قطعة',import_storage_return_details.ImportStorageReturnDetails_ID FROM import_storage_return inner join import_storage_return_supplier on import_storage_return.ImportStorageReturn_ID=import_storage_return_supplier.ImportStorageReturn_ID INNER JOIN import_storage_return_details ON import_storage_return_details.ImportStorageReturnSupplier_ID = import_storage_return_supplier.ImportStorageReturnSupplier_ID INNER JOIN data ON data.Data_ID = import_storage_return_details.Data_ID INNER JOIN type ON type.Type_ID = data.Type_ID INNER JOIN product ON product.Product_ID = data.Product_ID INNER JOIN factory ON data.Factory_ID = factory.Factory_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID LEFT JOIN color ON color.Color_ID = data.Color_ID LEFT JOIN size ON size.Size_ID = data.Size_ID LEFT JOIN sort ON sort.Sort_ID = data.Sort_ID inner join purchasing_price on purchasing_price.Data_ID=import_storage_return_details.Data_ID where import_storage_return.ImportStorageReturn_ID=" + ImportStorageReturnId;
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
                        if (dr["خصم الشراء"].ToString() != "")
                        {
                            gridView1.SetRowCellValue(rowHandl, gridView1.Columns["خصم الشراء"], dr["خصم الشراء"].ToString());
                        }
                        else if (dr["نسبة الشراء"].ToString() != "")
                        {
                            gridView1.SetRowCellValue(rowHandl, gridView1.Columns["نسبة الشراء"], dr["نسبة الشراء"].ToString());
                        }
                        gridView1.SetRowCellValue(rowHandl, gridView1.Columns["الزيادة العادية"], dr["الزيادة العادية"].ToString());
                        gridView1.SetRowCellValue(rowHandl, gridView1.Columns["الزيادة القطعية"], dr["الزيادة القطعية"].ToString());
                        gridView1.SetRowCellValue(rowHandl, gridView1.Columns["ضريبة القيمة المضافة"], dr["ضريبة القيمة المضافة"].ToString());
                        gridView1.SetRowCellValue(rowHandl, gridView1.Columns["سعر الشراء"], dr["سعر الشراء"].ToString());
                        gridView1.SetRowCellValue(rowHandl, gridView1.Columns["ImportStorageReturnDetails_ID"], dr["ImportStorageReturnDetails_ID"].ToString());

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

            if (gridView1.IsLastVisibleRow)
            {
                gridView1.FocusedRowHandle = gridView1.RowCount - 1;
            }

            double totalB = 0;
            double totalA = 0;
            for (int i = 0; i < gridView1.RowCount; i++)
            {
                totalB += Convert.ToDouble(gridView1.GetRowCellDisplayText(i, "السعر")) * Convert.ToDouble(gridView1.GetRowCellDisplayText(i, "متر/قطعة"));
                totalA += Convert.ToDouble(gridView1.GetRowCellDisplayText(i, "سعر الشراء")) * Convert.ToDouble(gridView1.GetRowCellDisplayText(i, "متر/قطعة"));
            }
            labTotalPriceBD.Text = totalB.ToString();
            labTotalPrice.Text = totalA.ToString();
        }

        public void displayReturnPermissionDetails(int storageImportPermissionIdF)
        {
            string q = "SELECT supplier_bill_details.Data_ID,data.Code as 'الكود',type.Type_Name as 'النوع',concat(product.Product_Name,' ',COALESCE(color.Color_Name,''),' ',data.Description,' ',groupo.Group_Name,' ',factory.Factory_Name,' ',COALESCE(size.Size_Value,''),' ',COALESCE(sort.Sort_Value,'')) as 'الاسم',supplier_bill_details.Price as 'السعر',supplier_bill_details.Purchasing_Ratio as 'نسبة الشراء',supplier_bill_details.Purchasing_Discount as 'خصم الشراء',supplier_bill_details.Normal_Increase as 'الزيادة العادية',supplier_bill_details.Categorical_Increase as 'الزيادة القطعية',supplier_bill_details.Value_Additive_Tax as 'ضريبة القيمة المضافة',supplier_bill_details.Purchasing_Price as 'سعر الشراء',supplier_bill_details.Total_Meters 'متر/قطعة','BillData_ID','ImportStorageReturnDetails_ID' FROM supplier_bill INNER JOIN supplier_bill_details ON supplier_bill_details.Bill_ID = supplier_bill.Bill_ID INNER JOIN data ON data.Data_ID = supplier_bill_details.Data_ID INNER JOIN type ON type.Type_ID = data.Type_ID INNER JOIN product ON product.Product_ID = data.Product_ID INNER JOIN factory ON data.Factory_ID = factory.Factory_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID LEFT JOIN color ON color.Color_ID = data.Color_ID LEFT JOIN size ON size.Size_ID = data.Size_ID LEFT JOIN sort ON sort.Sort_ID = data.Sort_ID where supplier_bill.StorageImportPermission_ID=" + storageImportPermissionIdF;
            MySqlDataAdapter da = new MySqlDataAdapter(q, dbconnection);
            DataTable dt = new DataTable();
            da.Fill(dt);
            gridControl2.DataSource = dt;
            gridView2.Columns[0].Visible = false;
            gridView2.Columns["BillData_ID"].Visible = false;
            gridView2.Columns["ImportStorageReturnDetails_ID"].Visible = false;

            if (gridView2.IsLastVisibleRow)
            {
                gridView2.FocusedRowHandle = gridView2.RowCount - 1;
            }

            double totalB = 0;
            double totalA = 0;
            for (int i = 0; i < gridView2.RowCount; i++)
            {
                totalB += Convert.ToDouble(gridView2.GetRowCellDisplayText(i, "السعر")) * Convert.ToDouble(gridView2.GetRowCellDisplayText(i, "متر/قطعة"));
                totalA += Convert.ToDouble(gridView2.GetRowCellDisplayText(i, "سعر الشراء")) * Convert.ToDouble(gridView2.GetRowCellDisplayText(i, "متر/قطعة"));
            }
            labelTotalB.Text = totalB.ToString();
            labelTotalA.Text = totalA.ToString();
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
            txtPrice.Text = txtNormalIncrease.Text = txtTotalMeter.Text = "";
            txtPurchasePrice.Text = labTotalPrice.Text = labTotalPriceBD.Text = labelTotalB.Text = labelTotalA.Text = "";
            txtTax.Text = "0";
        }

        public void DecreaseSupplierAccount()
        {
            double totalSafy = Convert.ToDouble(labelTotalA.Text);
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

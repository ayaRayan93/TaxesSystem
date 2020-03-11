using DevExpress.XtraGrid.Views.Grid;
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
    public partial class Supplier_Bill : Form
    {
        MySqlConnection conn, conn2;

        bool load = false;
        bool flag = false;
        bool loaded = false;
        bool loadPerm = false;
        bool loadSup = false;
        int storeId = 0;
        MainForm purchasesMainForm;
        DataRow row1 = null;
        int rowHandle = 0;

        public Supplier_Bill(MainForm mainForm)
        {
            InitializeComponent();
            conn = new MySqlConnection(connection.connectionString);
            conn2 = new MySqlConnection(connection.connectionString);
            purchasesMainForm = mainForm;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                string query = "select Store_ID,Store_Name from store";
                MySqlDataAdapter da = new MySqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                comStore.DataSource = dt;
                comStore.DisplayMember = dt.Columns["Store_Name"].ToString();
                comStore.ValueMember = dt.Columns["Store_ID"].ToString();
                comStore.Text = "";
                load = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            conn.Close();
        }

        private void comStore_SelectedValueChanged(object sender, EventArgs e)
        {
            if (load)
            {
                try
                {
                    storeId = Convert.ToInt16(comStore.SelectedValue.ToString());
                    loadFunc();

                    NewBill();
                    flag = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                conn.Close();
            }
        }

        private void comPermessionNum_SelectedValueChanged(object sender, EventArgs e)
        {
            if (flag)
            {
                try
                {
                    loadPerm = false;
                    loadSup = false;
                    string query = "select distinct supplier.Supplier_ID,supplier.Supplier_Name from supplier INNER JOIN import_supplier_permission ON import_supplier_permission.Supplier_ID = supplier.Supplier_ID where import_supplier_permission.StorageImportPermission_ID=" + comPermessionNum.SelectedValue.ToString();
                    MySqlDataAdapter da = new MySqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    comSupplier.DataSource = dt;
                    comSupplier.DisplayMember = dt.Columns["Supplier_Name"].ToString();
                    comSupplier.ValueMember = dt.Columns["Supplier_ID"].ToString();
                    comSupplier.Text = "";

                    if (comSupPerm.DataSource != null)
                    {
                        comSupPerm.DataSource = null;
                    }
                    NewBill();
                    loadPerm = true;
                }
                catch (Exception ex)
                {
                  MessageBox.Show(ex.ToString());
                }
                conn.Close();
            }
        }

        private void comSupplier_SelectedValueChanged(object sender, EventArgs e)
        {
            if (loadPerm)
            {
                try
                {
                    loadSup = false;
                    string query = "SELECT import_supplier_permission.Supplier_Permission_Number,import_supplier_permission.ImportSupplierPermission_ID FROM import_supplier_permission where import_supplier_permission.Supplier_ID=" + comSupplier.SelectedValue.ToString() + " and import_supplier_permission.StorageImportPermission_ID=" + comPermessionNum.SelectedValue.ToString() + " and import_supplier_permission.Purchase_Bill=0";
                    MySqlDataAdapter da = new MySqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    comSupPerm.DataSource = dt;
                    comSupPerm.DisplayMember = dt.Columns["Supplier_Permission_Number"].ToString();
                    comSupPerm.ValueMember = dt.Columns["ImportSupplierPermission_ID"].ToString();
                    comSupPerm.Text = "";
                    
                    NewBill();
                    loadSup = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                conn.Close();
            }
        }

        private void comSupPerm_SelectedValueChanged(object sender, EventArgs e)
        {
            if (loadSup)
            {
                try
                {
                    conn.Close();
                    conn.Open();

                    //NewBill();
                    string q = "SELECT DISTINCT data.Data_ID,data.Code AS 'الكود',type.Type_Name as 'النوع',concat(product.Product_Name,' ',COALESCE(color.Color_Name,''),' ',data.Description,' ',groupo.Group_Name,' ',factory.Factory_Name,' ',COALESCE(size.Size_Value,''),' ',COALESCE(sort.Sort_Value,'')) as 'الاسم',supplier_permission_details.Total_Meters as 'متر/قطعة',purchasing_price.Price AS 'السعر',purchasing_price.Purchasing_Discount AS 'نسبة الخصم',purchasing_price.Normal_Increase AS 'الزيادة العادية',purchasing_price.Categorical_Increase AS 'الزيادة القطعية',purchasing_price.Last_Price AS 'السعر بالزيادة',purchasing_price.Purchasing_Price AS 'سعر الشراء',(purchasing_price.Purchasing_Price*supplier_permission_details.Total_Meters) as 'الاجمالى بعد',purchasing_price.PurchasingPrice_ID,purchasing_price.Price_Type as 'نوع السعر',supplier_permission_details.Date as 'التاريخ',supplier_permission_details.Supplier_Permission_Details_ID FROM storage_import_permission INNER JOIN import_supplier_permission ON import_supplier_permission.StorageImportPermission_ID = storage_import_permission.StorageImportPermission_ID inner join supplier_permission_details on supplier_permission_details.ImportSupplierPermission_ID=import_supplier_permission.ImportSupplierPermission_ID INNER JOIN data ON supplier_permission_details.Data_ID = data.Data_ID INNER JOIN type ON type.Type_ID = data.Type_ID INNER JOIN product ON product.Product_ID = data.Product_ID INNER JOIN factory ON data.Factory_ID = factory.Factory_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID LEFT JOIN color ON color.Color_ID = data.Color_ID LEFT JOIN size ON size.Size_ID = data.Size_ID LEFT JOIN sort ON sort.Sort_ID = data.Sort_ID left JOIN purchasing_price ON data.Data_ID = purchasing_price.Data_ID where storage_import_permission.StorageImportPermission_ID=" + comPermessionNum.SelectedValue.ToString() + " and import_supplier_permission.Supplier_ID=" + comSupplier.SelectedValue.ToString() + " and import_supplier_permission.ImportSupplierPermission_ID=" + comSupPerm.SelectedValue.ToString();
                    MySqlDataAdapter da = new MySqlDataAdapter(q, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    gridControl1.DataSource = dt;
                    gridView1.Columns[0].Visible = false;
                    gridView1.Columns["PurchasingPrice_ID"].Visible = false;
                    gridView1.Columns["نوع السعر"].Visible = false;
                    gridView1.Columns["Supplier_Permission_Details_ID"].Visible = false;

                    if (gridView1.IsLastVisibleRow)
                    {
                        gridView1.FocusedRowHandle = gridView1.RowCount - 1;
                    }
                    for (int i = 3; i < gridView1.Columns.Count; i++)
                    {
                        gridView1.Columns[i].Width = 120;
                    }
                    gridView1.Columns[1].Width = 170;
                    gridView1.Columns[3].Width = 300;
                    //,purchasing_price.ProfitRatio as 'ضريبة القيمة المضافة'
                    q = "SELECT DISTINCT data.Data_ID,data.Code AS 'الكود',type.Type_Name as 'النوع',concat(product.Product_Name,' ',COALESCE(color.Color_Name,''),' ',data.Description,' ',groupo.Group_Name,' ',factory.Factory_Name,' ',COALESCE(size.Size_Value,''),' ',COALESCE(sort.Sort_Value,'')) as 'الاسم',supplier_permission_details.Total_Meters as 'متر/قطعة',purchasing_price.Price AS 'السعر',purchasing_price.Purchasing_Discount AS 'نسبة الخصم',purchasing_price.Normal_Increase AS 'الزيادة العادية',purchasing_price.Categorical_Increase AS 'الزيادة القطعية',purchasing_price.Last_Price AS 'السعر بالزيادة',purchasing_price.Purchasing_Price AS 'سعر الشراء',(purchasing_price.Purchasing_Price*supplier_permission_details.Total_Meters) as 'الاجمالى بعد',purchasing_price.PurchasingPrice_ID,purchasing_price.Price_Type as 'نوع السعر',supplier_permission_details.Supplier_Permission_Details_ID FROM storage_import_permission INNER JOIN import_supplier_permission ON import_supplier_permission.StorageImportPermission_ID = storage_import_permission.StorageImportPermission_ID inner join supplier_permission_details on supplier_permission_details.ImportSupplierPermission_ID=import_supplier_permission.ImportSupplierPermission_ID INNER JOIN data ON supplier_permission_details.Data_ID = data.Data_ID INNER JOIN type ON type.Type_ID = data.Type_ID INNER JOIN product ON product.Product_ID = data.Product_ID INNER JOIN factory ON data.Factory_ID = factory.Factory_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID LEFT JOIN color ON color.Color_ID = data.Color_ID LEFT JOIN size ON size.Size_ID = data.Size_ID LEFT JOIN sort ON sort.Sort_ID = data.Sort_ID left JOIN purchasing_price ON data.Data_ID = purchasing_price.Data_ID where storage_import_permission.StorageImportPermission_ID=0";
                    da = new MySqlDataAdapter(q, conn);
                    dt = new DataTable();
                    da.Fill(dt);
                    gridControl2.DataSource = dt;
                    gridView2.Columns[0].Visible = false;
                    gridView2.Columns["PurchasingPrice_ID"].Visible = false;
                    gridView2.Columns["نوع السعر"].Visible = false;
                    gridView2.Columns["Supplier_Permission_Details_ID"].Visible = false;

                    if (gridView2.IsLastVisibleRow)
                    {
                        gridView2.FocusedRowHandle = gridView2.RowCount - 1;
                    }
                    for (int i = 3; i < gridView2.Columns.Count; i++)
                    {
                        gridView2.Columns[i].Width = 120;
                    }
                    gridView2.Columns[1].Width = 170;
                    gridView2.Columns[3].Width = 300;

                    Clear();
                    txtAllTax.Text = "0.00";
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                conn.Close();
            }
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
                //radioList.Checked = false;
                //radioQata3y.Checked = false;
                if (row1["نوع السعر"].ToString() == "لستة")
                {
                    radioList.Checked= true;
                    txtDiscount.Text = row1["نسبة الخصم"].ToString();
                    label7.Text = "نسبة الخصم";
                    /*txtNormalIncrease.Visible = true;
                    txtCategoricalIncrease.Visible = true;
                    label8.Visible = true;
                    label6.Visible = true;*/
                }
                else if (row1["نوع السعر"].ToString() == "قطعى")
                {
                    radioQata3y.Checked = true;
                    txtDiscount.Text = row1["نسبة الخصم"].ToString();
                    label7.Text = "نسبة الخصم";
                    /*txtNormalIncrease.Visible = false;
                    txtCategoricalIncrease.Visible = false;
                    label8.Visible = false;
                    label6.Visible = false;*/
                }
                if (txtDiscount.Text == "")
                {
                    txtDiscount.Text = "0";
                }
                loaded = true;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txtBox_TextChanged2(object sender, EventArgs e)
        {
            try
            {
                if (loaded)
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

        private void dtnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (row1 != null)
                {
                    //if (!IsAdded())
                    //{
                    if (txtPurchasePrice.Text != "" && (radioList.Checked || radioQata3y.Checked))
                    {
                        if (txtTotalMeter.Text != "" && txtPrice.Text != "" && txtCategoricalIncrease.Text != "" && txtDiscount.Text != "" && txtNormalIncrease.Text != "" && txtTax.Text != "" && txtPurchasePrice.Text != "")
                        {
                            double price, BuyDiscount, NormalIncrease, CategoricalIncrease, VAT, purchasePrice, quantity;
                            if (double.TryParse(txtPrice.Text, out price)
                             &&
                             double.TryParse(txtDiscount.Text, out BuyDiscount)
                             &&
                             double.TryParse(txtNormalIncrease.Text, out NormalIncrease)
                             &&
                             double.TryParse(txtCategoricalIncrease.Text, out CategoricalIncrease)
                             &&
                             double.TryParse(txtTotalMeter.Text, out quantity)
                             &&
                             double.TryParse(txtTax.Text, out VAT)
                             &&
                             double.TryParse(txtPurchasePrice.Text, out purchasePrice))
                            {
                                conn.Open();
                                gridView2.AddNewRow();
                                int rowHandl = gridView2.GetRowHandle(gridView2.DataRowCount);
                                if (gridView2.IsNewItemRow(rowHandl))
                                {
                                    gridView2.SetRowCellValue(rowHandl, gridView2.Columns["Data_ID"], row1["Data_ID"].ToString());
                                    gridView2.SetRowCellValue(rowHandl, gridView2.Columns["الكود"], row1["الكود"].ToString());
                                    gridView2.SetRowCellValue(rowHandl, gridView2.Columns["النوع"], row1["النوع"].ToString());
                                    gridView2.SetRowCellValue(rowHandl, gridView2.Columns["الاسم"], row1["الاسم"].ToString());
                                    gridView2.SetRowCellValue(rowHandl, gridView2.Columns["السعر"], System.Math.Round(Convert.ToDouble(txtPrice.Text), 2));
                                    if (radioList.Checked == true)
                                    {
                                        gridView2.SetRowCellValue(rowHandl, gridView2.Columns["نوع السعر"], "لستة");
                                        gridView2.SetRowCellValue(rowHandl, gridView2.Columns["نسبة الخصم"], System.Math.Round(BuyDiscount, 2));
                                        gridView2.SetRowCellValue(rowHandl, gridView2.Columns["الزيادة العادية"], System.Math.Round(NormalIncrease, 2));
                                        gridView2.SetRowCellValue(rowHandl, gridView2.Columns["الزيادة القطعية"], System.Math.Round(CategoricalIncrease, 2));
                                    }
                                    else if (radioQata3y.Checked == true)
                                    {
                                        gridView2.SetRowCellValue(rowHandl, gridView2.Columns["نوع السعر"], "قطعى");
                                        gridView2.SetRowCellValue(rowHandl, gridView2.Columns["نسبة الخصم"], System.Math.Round(BuyDiscount, 2));
                                        gridView2.SetRowCellValue(rowHandl, gridView2.Columns["الزيادة العادية"], System.Math.Round(NormalIncrease, 2)/*""*/);
                                        gridView2.SetRowCellValue(rowHandl, gridView2.Columns["الزيادة القطعية"], System.Math.Round(CategoricalIncrease, 2)/*""*/);
                                    }
                                    //gridView2.SetRowCellValue(rowHandl, gridView2.Columns["ضريبة القيمة المضافة"], VAT);
                                    if (purchasePrice == 0)
                                    {
                                        gridView2.SetRowCellValue(rowHandl, gridView2.Columns["سعر الشراء"], purchasePrice);
                                        gridView2.SetRowCellValue(rowHandl, gridView2.Columns["السعر بالزيادة"], 0);

                                    }
                                    else
                                    {
                                        gridView2.SetRowCellValue(rowHandl, gridView2.Columns["سعر الشراء"], System.Math.Round(purchasePrice, 2));
                                        gridView2.SetRowCellValue(rowHandl, gridView2.Columns["السعر بالزيادة"], System.Math.Round(Convert.ToDouble(txtLastPrice.Text), 2));
                                    }
                                    gridView2.SetRowCellValue(rowHandl, gridView2.Columns["متر/قطعة"], quantity);
                                    gridView2.SetRowCellValue(rowHandl, gridView2.Columns["الاجمالى بعد"], System.Math.Round(purchasePrice, 2) * quantity);
                                    gridView2.SetRowCellValue(rowHandl, gridView2.Columns["PurchasingPrice_ID"], row1["PurchasingPrice_ID"].ToString());
                                    gridView2.SetRowCellValue(rowHandl, gridView2.Columns["Supplier_Permission_Details_ID"], row1["Supplier_Permission_Details_ID"].ToString());

                                    gridView1.DeleteRow(gridView1.FocusedRowHandle);

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
                                    Clear();
                                    row1 = null;
                                    labelTotalB.Text = totalAllB.ToString("#.000");
                                    labelTotalDiscount.Text = totalAllDiscount.ToString("#.000");
                                    labelTotalCat.Text = totalAllCatInc.ToString("#.000");
                                    labelTotalA.Text = totalAllA.ToString("#.000");
                                    labelTotalSafy.Text = (Convert.ToDouble(labelTotalA.Text) + (Convert.ToDouble(labelTotalA.Text) * (Convert.ToDouble(txtAllTax.Text) / 100))).ToString("#.000");
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
                        MessageBox.Show("يجب تسعير البند اولا");
                    }
                    //}
                    //else
                    //{
                    //    MessageBox.Show("هذا البند تم اضافتة من قبل");
                    //}
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
            conn.Close();
        }
        
        private void btnRemove_Click(object sender, EventArgs e)
        {
            try
            {
                DataRow row2 = gridView2.GetDataRow(gridView2.FocusedRowHandle);
                if (row2 != null)
                {
                    gridView2.DeleteRow(gridView2.FocusedRowHandle);

                    string str = "";
                    if (gridView2.RowCount > 0)
                    {
                        for (int i = 0; i < gridView2.RowCount - 1; i++)
                        {
                            str += Convert.ToDouble(gridView2.GetRowCellDisplayText(i, "Supplier_Permission_Details_ID")) + ",";
                        }
                        str += Convert.ToDouble(gridView2.GetRowCellDisplayText(gridView2.RowCount - 1, "Supplier_Permission_Details_ID"));
                    }
                    string q = "";
                    if (str == "")
                    {
                        q = "SELECT DISTINCT data.Data_ID,data.Code AS 'الكود',type.Type_Name as 'النوع',concat(product.Product_Name,' ',COALESCE(color.Color_Name,''),' ',data.Description,' ',groupo.Group_Name,' ',factory.Factory_Name,' ',COALESCE(size.Size_Value,''),' ',COALESCE(sort.Sort_Value,'')) as 'الاسم',supplier_permission_details.Total_Meters as 'متر/قطعة',purchasing_price.Price AS 'السعر',purchasing_price.Purchasing_Discount AS 'نسبة الخصم',purchasing_price.Normal_Increase AS 'الزيادة العادية',purchasing_price.Categorical_Increase AS 'الزيادة القطعية',purchasing_price.Last_Price AS 'السعر بالزيادة',purchasing_price.Purchasing_Price AS 'سعر الشراء',(purchasing_price.Purchasing_Price*supplier_permission_details.Total_Meters) as 'الاجمالى بعد',purchasing_price.PurchasingPrice_ID,purchasing_price.Price_Type as 'نوع السعر',supplier_permission_details.Date as 'التاريخ',supplier_permission_details.Supplier_Permission_Details_ID FROM storage_import_permission INNER JOIN import_supplier_permission ON import_supplier_permission.StorageImportPermission_ID = storage_import_permission.StorageImportPermission_ID inner join supplier_permission_details on supplier_permission_details.ImportSupplierPermission_ID=import_supplier_permission.ImportSupplierPermission_ID INNER JOIN data ON supplier_permission_details.Data_ID = data.Data_ID INNER JOIN type ON type.Type_ID = data.Type_ID INNER JOIN product ON product.Product_ID = data.Product_ID INNER JOIN factory ON data.Factory_ID = factory.Factory_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID LEFT JOIN color ON color.Color_ID = data.Color_ID LEFT JOIN size ON size.Size_ID = data.Size_ID LEFT JOIN sort ON sort.Sort_ID = data.Sort_ID left JOIN purchasing_price ON data.Data_ID = purchasing_price.Data_ID where storage_import_permission.StorageImportPermission_ID=" + comPermessionNum.SelectedValue.ToString() + " and import_supplier_permission.Supplier_ID=" + comSupplier.SelectedValue.ToString() + " and import_supplier_permission.ImportSupplierPermission_ID=" + comSupPerm.SelectedValue.ToString();
                    }
                    else
                    {
                        q = "SELECT DISTINCT data.Data_ID,data.Code AS 'الكود',type.Type_Name as 'النوع',concat(product.Product_Name,' ',COALESCE(color.Color_Name,''),' ',data.Description,' ',groupo.Group_Name,' ',factory.Factory_Name,' ',COALESCE(size.Size_Value,''),' ',COALESCE(sort.Sort_Value,'')) as 'الاسم',supplier_permission_details.Total_Meters as 'متر/قطعة',purchasing_price.Price AS 'السعر',purchasing_price.Purchasing_Discount AS 'نسبة الخصم',purchasing_price.Normal_Increase AS 'الزيادة العادية',purchasing_price.Categorical_Increase AS 'الزيادة القطعية',purchasing_price.Last_Price AS 'السعر بالزيادة',purchasing_price.Purchasing_Price AS 'سعر الشراء',(purchasing_price.Purchasing_Price*supplier_permission_details.Total_Meters) as 'الاجمالى بعد',purchasing_price.PurchasingPrice_ID,purchasing_price.Price_Type as 'نوع السعر',supplier_permission_details.Date as 'التاريخ',supplier_permission_details.Supplier_Permission_Details_ID FROM storage_import_permission INNER JOIN import_supplier_permission ON import_supplier_permission.StorageImportPermission_ID = storage_import_permission.StorageImportPermission_ID inner join supplier_permission_details on supplier_permission_details.ImportSupplierPermission_ID=import_supplier_permission.ImportSupplierPermission_ID INNER JOIN data ON supplier_permission_details.Data_ID = data.Data_ID INNER JOIN type ON type.Type_ID = data.Type_ID INNER JOIN product ON product.Product_ID = data.Product_ID INNER JOIN factory ON data.Factory_ID = factory.Factory_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID LEFT JOIN color ON color.Color_ID = data.Color_ID LEFT JOIN size ON size.Size_ID = data.Size_ID LEFT JOIN sort ON sort.Sort_ID = data.Sort_ID left JOIN purchasing_price ON data.Data_ID = purchasing_price.Data_ID where storage_import_permission.StorageImportPermission_ID=" + comPermessionNum.SelectedValue.ToString() + " and import_supplier_permission.Supplier_ID=" + comSupplier.SelectedValue.ToString() + " and import_supplier_permission.ImportSupplierPermission_ID=" + comSupPerm.SelectedValue.ToString() + " and supplier_permission_details.Supplier_Permission_Details_ID not in (" + str + ")";
                    }
                    MySqlDataAdapter da = new MySqlDataAdapter(q, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    gridControl1.DataSource = dt;
                    gridView1.Columns[0].Visible = false;
                    gridView1.Columns["PurchasingPrice_ID"].Visible = false;
                    gridView1.Columns["نوع السعر"].Visible = false;
                    gridView1.Columns["Supplier_Permission_Details_ID"].Visible = false;

                    if (gridView1.IsLastVisibleRow)
                    {
                        gridView1.FocusedRowHandle = gridView1.RowCount - 1;
                    }
                    for (int i = 3; i < gridView1.Columns.Count; i++)
                    {
                        gridView1.Columns[i].Width = 120;
                    }
                    gridView1.Columns[1].Width = 170;
                    gridView1.Columns[3].Width = 300;

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
                    row2 = null;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (gridView2.RowCount > 0 && txtAllTax.Text != "" && comPermessionNum.SelectedValue != null && comSupplier.SelectedValue != null && comSupPerm.SelectedValue != null)
                {
                    conn.Open();
                    string q = "select Bill_No from supplier_bill where Supplier_ID=" + comSupplier.SelectedValue.ToString() + " ORDER BY Bill_ID DESC LIMIT 1 ";
                    MySqlCommand comm = new MySqlCommand(q, conn);
                    int BillNo = 1;
                    if (comm.ExecuteScalar() != null)
                    {
                        BillNo = Convert.ToInt32(comm.ExecuteScalar().ToString());
                        BillNo++;
                    }

                    string query = "insert into supplier_bill (Bill_No,Date,Import_Permission_Number,Store_ID,Total_Price_B,Total_Price_A,StorageImportPermission_ID,Supplier_ID,Supplier_Permission_Number,Employee_ID,Value_Additive_Tax,Import_Date) values (@Bill_No,@Date,@Import_Permission_Number,@Store_ID,@Total_Price_B,@Total_Price_A,@StorageImportPermission_ID,@Supplier_ID,@Supplier_Permission_Number,@Employee_ID,@Value_Additive_Tax,@Import_Date)";
                    MySqlCommand com = new MySqlCommand(query, conn);
                    //com.Parameters.Add("@Branch_ID", MySqlDbType.Int16);
                    //com.Parameters["@Branch_ID"].Value = UserControl.EmpBranchID;
                    com.Parameters.Add("@Bill_No", MySqlDbType.Int16);
                    com.Parameters["@Bill_No"].Value = BillNo;
                    com.Parameters.Add("@Date", MySqlDbType.DateTime);
                    com.Parameters["@Date"].Value = DateTime.Now;
                    com.Parameters.Add("@Import_Permission_Number", MySqlDbType.Int16);
                    com.Parameters["@Import_Permission_Number"].Value = comPermessionNum.Text;
                    com.Parameters.Add("@Store_ID", MySqlDbType.Int16);
                    com.Parameters["@Store_ID"].Value = storeId;
                    com.Parameters.Add("@Total_Price_B", MySqlDbType.Decimal);
                    com.Parameters["@Total_Price_B"].Value = labelTotalB.Text;
                    com.Parameters.Add("@Total_Price_A", MySqlDbType.Decimal);
                    com.Parameters["@Total_Price_A"].Value = labelTotalSafy.Text;
                    com.Parameters.Add("@StorageImportPermission_ID", MySqlDbType.Int16);
                    com.Parameters["@StorageImportPermission_ID"].Value = comPermessionNum.SelectedValue.ToString();
                    com.Parameters.Add("@Employee_ID", MySqlDbType.Int16);
                    com.Parameters["@Employee_ID"].Value = UserControl.EmpID;
                    com.Parameters.Add("@Supplier_ID", MySqlDbType.Int16);
                    com.Parameters["@Supplier_ID"].Value = comSupplier.SelectedValue.ToString();
                    com.Parameters.Add("@Supplier_Permission_Number", MySqlDbType.Int16);
                    com.Parameters["@Supplier_Permission_Number"].Value = comSupPerm.Text;
                    com.Parameters.Add("@Value_Additive_Tax", MySqlDbType.Decimal);
                    com.Parameters["@Value_Additive_Tax"].Value = txtAllTax.Text;
                    com.Parameters.Add("@Import_Date", MySqlDbType.Date);
                    com.Parameters["@Import_Date"].Value = dateTimePicker1.Value.Date;
                    com.ExecuteNonQuery();

                    string q1 = "select Bill_ID from supplier_bill ORDER BY Bill_ID DESC LIMIT 1";
                    comm = new MySqlCommand(q1, conn);
                    int id = Convert.ToInt32(comm.ExecuteScalar().ToString());

                    for (int i = 0; i < gridView2.RowCount; i++)
                    {
                        DataRow row3 = gridView2.GetDataRow(i);

                        query = "insert into supplier_bill_details (Bill_ID,Data_ID,Price_Type,Price,Last_Price,Purchasing_Discount,Normal_Increase,Categorical_Increase,Purchasing_Price,Total_Meters,Supplier_Permission_Details_ID) values (@Bill_ID,@Data_ID,@Price_Type,@Price,@Last_Price,@Purchasing_Discount,@Normal_Increase,@Categorical_Increase,@Purchasing_Price,@Total_Meters,@Supplier_Permission_Details_ID)";
                        com = new MySqlCommand(query, conn);
                        com.Parameters.Add("@Bill_ID", MySqlDbType.Int16);
                        com.Parameters["@Bill_ID"].Value = id;
                        com.Parameters.Add("@Data_ID", MySqlDbType.Int16);
                        com.Parameters["@Data_ID"].Value = row3["Data_ID"].ToString();
                        com.Parameters.Add("@Price_Type", MySqlDbType.VarChar);
                        com.Parameters["@Price_Type"].Value = row3["نوع السعر"].ToString();
                        com.Parameters.Add("@Price", MySqlDbType.Decimal);
                        com.Parameters["@Price"].Value = row3["السعر"].ToString();
                        if (row3["نوع السعر"].ToString() == "لستة")
                        {
                            com.Parameters.Add("@Purchasing_Discount", MySqlDbType.Decimal);
                            com.Parameters["@Purchasing_Discount"].Value = row3["نسبة الخصم"].ToString();
                            com.Parameters.Add("@Last_Price", MySqlDbType.Decimal);
                            com.Parameters["@Last_Price"].Value = row3["السعر بالزيادة"].ToString();
                            com.Parameters.Add("@Normal_Increase", MySqlDbType.Decimal);
                            com.Parameters["@Normal_Increase"].Value = row3["الزيادة العادية"].ToString();
                            com.Parameters.Add("@Categorical_Increase", MySqlDbType.Decimal);
                            com.Parameters["@Categorical_Increase"].Value = row3["الزيادة القطعية"].ToString();
                        }
                        else if (row3["نوع السعر"].ToString() == "قطعى")
                        {
                            com.Parameters.Add("@Purchasing_Discount", MySqlDbType.Decimal);
                            com.Parameters["@Purchasing_Discount"].Value = row3["نسبة الخصم"].ToString();
                            com.Parameters.Add("@Last_Price", MySqlDbType.Decimal);
                            com.Parameters["@Last_Price"].Value = row3["السعر بالزيادة"].ToString();
                            com.Parameters.Add("@Normal_Increase", MySqlDbType.Decimal);
                            com.Parameters["@Normal_Increase"].Value = row3["الزيادة العادية"].ToString();
                            com.Parameters.Add("@Categorical_Increase", MySqlDbType.Decimal);
                            com.Parameters["@Categorical_Increase"].Value = row3["الزيادة القطعية"].ToString();
                        }
                        //com.Parameters.Add("@Value_Additive_Tax", MySqlDbType.Decimal);
                        //com.Parameters["@Value_Additive_Tax"].Value = row3["ضريبة القيمة المضافة"].ToString();
                        com.Parameters.Add("@Purchasing_Price", MySqlDbType.Decimal);
                        com.Parameters["@Purchasing_Price"].Value = row3["سعر الشراء"].ToString();
                        com.Parameters.Add("@Total_Meters", MySqlDbType.Decimal);
                        com.Parameters["@Total_Meters"].Value = row3["متر/قطعة"].ToString();
                        com.Parameters.Add("@Supplier_Permission_Details_ID", MySqlDbType.Int16);
                        com.Parameters["@Supplier_Permission_Details_ID"].Value = row3["Supplier_Permission_Details_ID"].ToString();
                        com.ExecuteNonQuery();
                    }

                    string q2 = "update import_supplier_permission set Purchase_Bill=1 where ImportSupplierPermission_ID=" + comSupPerm.SelectedValue.ToString();
                    com = new MySqlCommand(q2, conn);
                    com.ExecuteNonQuery();
                    
                    bool flagConfirm = false;
                    conn2.Open();
                    //query = "select StorageImportPermission_ID,Import_Permission_Number from storage_import_permission where Store_ID=" + storeId + " and Confirmed=0";
                    //com = new MySqlCommand(query, conn);
                    //MySqlDataReader dr = com.ExecuteReader();
                    //while (dr.Read())
                    //{
                        query = "SELECT import_supplier_permission.Supplier_Permission_Number,import_supplier_permission.ImportSupplierPermission_ID FROM import_supplier_permission INNER JOIN storage_import_permission ON import_supplier_permission.StorageImportPermission_ID = storage_import_permission.StorageImportPermission_ID where import_supplier_permission.StorageImportPermission_ID=" + comPermessionNum.SelectedValue.ToString()/*dr["StorageImportPermission_ID"].ToString()*/ + " and import_supplier_permission.Purchase_Bill=0 and storage_import_permission.Store_ID=" + storeId + " and storage_import_permission.Confirmed=0";
                        com = new MySqlCommand(query, conn2);
                        MySqlDataReader dr2 = com.ExecuteReader();
                        while (dr2.Read())
                        {
                            flagConfirm = true;
                        }
                        dr2.Close();
                    //}
                    //dr.Close();
                    conn.Close();

                    conn.Open();
                    if (flagConfirm == false)
                    {
                        query = "update storage_import_permission set Confirmed=1 where storage_import_permission.Import_Permission_Number=" + comPermessionNum.Text + " and storage_import_permission.Store_ID=" + storeId;
                        MySqlCommand c = new MySqlCommand(query, conn);
                        c.ExecuteNonQuery();
                    }

                    IncreaseSupplierAccount();

                    #region report
                    query = "select Store_Name from store where Store_ID=" + storeId;
                    com = new MySqlCommand(query, conn);
                    string storeName = com.ExecuteScalar().ToString();

                    double addabtiveTax = 0;
                    List<SupplierBill_Items> bi = new List<SupplierBill_Items>();
                    double discount = 0;
                    for (int i = 0; i < gridView2.RowCount; i++)
                    {
                        //int rowHand = gridView2.GetRowHandle(i);
                        if (gridView2.GetRowCellDisplayText(i, gridView2.Columns["نوع السعر"]) == "لستة")
                        {
                            /*double lastPrice = 0;
                            lastPrice = (Convert.ToDouble(gridView2.GetRowCellDisplayText(i, gridView2.Columns["سعر الشراء"])) * 100) / (100 - Convert.ToDouble(gridView2.GetRowCellDisplayText(i, gridView2.Columns["نسبة الخصم"])));*/
                            /*query = "SELECT sum(additional_increase_purchasingprice.AdditionalValue) FROM additional_increase_purchasingprice INNER JOIN purchasing_price ON additional_increase_purchasingprice.PurchasingPrice_ID = purchasing_price.PurchasingPrice_ID where additional_increase_purchasingprice.Type='عادية' and purchasing_price.Data_ID=" + gridView2.GetRowCellDisplayText(i, gridView2.Columns["Data_ID"].ToString());
                            com = new MySqlCommand(query, conn);
                            double normincrease = Convert.ToDouble(gridView2.GetRowCellDisplayText(i, gridView2.Columns["الزيادة العادية"])) + Convert.ToDouble(com.ExecuteScalar().ToString());

                            query = "SELECT sum(additional_increase_purchasingprice.AdditionalValue) FROM additional_increase_purchasingprice INNER JOIN purchasing_price ON additional_increase_purchasingprice.PurchasingPrice_ID = purchasing_price.PurchasingPrice_ID where additional_increase_purchasingprice.Type='قطعية' and purchasing_price.Data_ID=" + gridView2.GetRowCellDisplayText(i, gridView2.Columns["Data_ID"].ToString());
                            com = new MySqlCommand(query, conn);
                            double catincrease = Convert.ToDouble(gridView2.GetRowCellDisplayText(i, gridView2.Columns["الزيادة القطعية"])) + Convert.ToDouble(com.ExecuteScalar().ToString());
                            */
                            discount += (Convert.ToDouble(gridView2.GetRowCellDisplayText(i, gridView2.Columns["السعر بالزيادة"]).ToString()) * Convert.ToDouble(gridView2.GetRowCellDisplayText(i, gridView2.Columns["متر/قطعة"]).ToString())) * (Convert.ToDouble(gridView2.GetRowCellDisplayText(i, gridView2.Columns["نسبة الخصم"]).ToString()) / 100);
                            SupplierBill_Items item = new SupplierBill_Items() { Code = gridView2.GetRowCellDisplayText(i, gridView2.Columns["الكود"]), Product_Type = gridView2.GetRowCellDisplayText(i, gridView2.Columns["النوع"]), Product_Name = gridView2.GetRowCellDisplayText(i, gridView2.Columns["الاسم"]), Total_Meters = Convert.ToDouble(gridView2.GetRowCellDisplayText(i, gridView2.Columns["متر/قطعة"])), PriceB = Convert.ToDouble(gridView2.GetRowCellDisplayText(i, gridView2.Columns["السعر"])), Discount = Convert.ToDouble(gridView2.GetRowCellDisplayText(i, gridView2.Columns["نسبة الخصم"])), Last_Price = Convert.ToDouble(gridView2.GetRowCellDisplayText(i, gridView2.Columns["السعر بالزيادة"])), Normal_Increase = Convert.ToDouble(gridView2.GetRowCellDisplayText(i, gridView2.Columns["الزيادة العادية"])), Categorical_Increase = Convert.ToDouble(gridView2.GetRowCellDisplayText(i, gridView2.Columns["الزيادة القطعية"])), PriceA = Convert.ToDouble(gridView2.GetRowCellDisplayText(i, gridView2.Columns["سعر الشراء"])) };
                            bi.Add(item);
                        }
                        else if(gridView2.GetRowCellDisplayText(i, gridView2.Columns["نوع السعر"]) == "قطعى")
                        {
                            //, Normal_Increase = Convert.ToDouble(gridView2.GetRowCellDisplayText(i, gridView2.Columns["الزيادة العادية"])), Categorical_Increase = Convert.ToDouble(gridView2.GetRowCellDisplayText(i, gridView2.Columns["الزيادة القطعية"]))
                            discount += (Convert.ToDouble(gridView2.GetRowCellDisplayText(i, gridView2.Columns["السعر بالزيادة"]).ToString()) * Convert.ToDouble(gridView2.GetRowCellDisplayText(i, gridView2.Columns["متر/قطعة"]).ToString())) * (Convert.ToDouble(gridView2.GetRowCellDisplayText(i, gridView2.Columns["نسبة الخصم"]).ToString()) / 100);
                            SupplierBill_Items item = new SupplierBill_Items() { Code = gridView2.GetRowCellDisplayText(i, gridView2.Columns["الكود"]), Product_Type = gridView2.GetRowCellDisplayText(i, gridView2.Columns["النوع"]), Product_Name = gridView2.GetRowCellDisplayText(i, gridView2.Columns["الاسم"]), Total_Meters = Convert.ToDouble(gridView2.GetRowCellDisplayText(i, gridView2.Columns["متر/قطعة"])), PriceB = Convert.ToDouble(gridView2.GetRowCellDisplayText(i, gridView2.Columns["السعر"])), Discount = Convert.ToDouble(gridView2.GetRowCellDisplayText(i, gridView2.Columns["نسبة الخصم"])), Last_Price = Convert.ToDouble(gridView2.GetRowCellDisplayText(i, gridView2.Columns["السعر بالزيادة"])), Normal_Increase = Convert.ToDouble(gridView2.GetRowCellDisplayText(i, gridView2.Columns["الزيادة العادية"])), Categorical_Increase = Convert.ToDouble(gridView2.GetRowCellDisplayText(i, gridView2.Columns["الزيادة القطعية"])), PriceA = Convert.ToDouble(gridView2.GetRowCellDisplayText(i, gridView2.Columns["سعر الشراء"])) };
                            bi.Add(item);
                        }
                        //addabtiveTax += Convert.ToDouble(gridView2.GetRowCellDisplayText(i, gridView2.Columns["ضريبة القيمة المضافة"]));
                    }
                    addabtiveTax = Convert.ToDouble(txtAllTax.Text);
                    Report_SupplierBill f = new Report_SupplierBill();
                    f.PrintInvoice(storeName, BillNo.ToString(), comSupplier.Text, comSupPerm.Text, comPermessionNum.Text, discount, Convert.ToDouble(labelTotalA.Text), addabtiveTax, Convert.ToDouble(labelTotalSafy.Text), dateTimePicker1.Value.Date, bi);
                    f.ShowDialog();
                    #endregion

                    loaded = false;
                    loadFunc();
                    flag = true;

                    NewBill();
                    loaded = true;
                }
                else
                {
                    MessageBox.Show("يجب التاكد من البيانات واختيار البنود");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            conn2.Close();
            conn.Close();
        }

        //function
        void loadFunc()
        {
            flag = false;
            loadSup = false;
            loadPerm = false;
            string query = "select StorageImportPermission_ID,Import_Permission_Number from storage_import_permission where Store_ID=" + storeId + " and Confirmed=0";
            MySqlDataAdapter da = new MySqlDataAdapter(query, conn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            comPermessionNum.DataSource = dt;
            comPermessionNum.DisplayMember = dt.Columns["Import_Permission_Number"].ToString();
            comPermessionNum.ValueMember = dt.Columns["StorageImportPermission_ID"].ToString();
            comPermessionNum.Text = "";

            if (comSupplier.DataSource != null)
            {
                comSupplier.DataSource = null;
            }
            if (comSupPerm.DataSource != null)
            {
                comSupPerm.DataSource = null;
            }
        }
        bool IsAdded()
        {
            for (int i = 0; i < gridView2.RowCount; i++)
            {
                DataRow row3 = gridView2.GetDataRow(i);
                if ((row1["Supplier_Permission_Details_ID"].ToString() == row3["Supplier_Permission_Details_ID"].ToString()))
                    return true;
            }
            return false;
        }
        //new bill
        public void NewBill()
        {
            gridControl2.DataSource = null;
            gridControl1.DataSource = null;
            Clear();
            txtAllTax.Text = "0.00";
        }
        //clear fields
        public void Clear()
        {
            txtCode.Text = txtPrice.Text = txtLastPrice.Text = txtTotalMeter.Text = "";
            //txtAllTax.Text = "0.00";
            txtTax.Text = txtCategoricalIncrease.Text = txtDiscount.Text = txtNormalIncrease.Text = "0";
            labelTotalCat.Text = labelTotalB.Text = labelTotalA.Text = txtPurchasePrice.Text = txtLastPrice.Text /*= labelTotalVal.Text*/ = labelTotalDiscount.Text = labelTotalSafy.Text = "";
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
            
            /*double price = double.Parse(txtPrice.Text);
            double PurchasesPercent = double.Parse(txtDiscount.Text);
            if (radioQata3y.Checked == true)
            {
                return price + (price * PurchasesPercent / 100.0);
            }
            else if (radioList.Checked == true)
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
            }*/
        }

        public double lastPrice(double purchasePrice)
        {
            double discount = double.Parse(txtDiscount.Text);
            double lastPrice = purchasePrice * 100 / (100 - discount);

            return lastPrice;
        }

        public void IncreaseSupplierAccount()
        {
            double totalSafy = Convert.ToDouble(labelTotalSafy.Text);
            string query = "select Money from supplier_rest_money where Supplier_ID=" + comSupplier.SelectedValue.ToString();
            MySqlCommand com = new MySqlCommand(query, conn);
            if (com.ExecuteScalar() != null)
            {
                double restMoney = Convert.ToDouble(com.ExecuteScalar());
                query = "update supplier_rest_money set Money=" + (restMoney + totalSafy) + " where Supplier_ID=" + comSupplier.SelectedValue.ToString();
                com = new MySqlCommand(query, conn);
            }
            else
            {
                query = "insert into supplier_rest_money (Supplier_ID,Money) values (@Supplier_ID,@Money)";
                com = new MySqlCommand(query, conn);
                com.Parameters.Add("@Supplier_ID", MySqlDbType.Int16, 11).Value = comSupplier.SelectedValue;
                com.Parameters.Add("@Money", MySqlDbType.Decimal, 10).Value = totalSafy;
            }
            com.ExecuteNonQuery();
        }

        private void radioList_CheckedChanged(object sender, EventArgs e)
        {
            /*try
            {
                if (row1 != null && radioList.Checked == true)
                {
                    txtDiscount.Text = row1["نسبة الخصم"].ToString();
                    label7.Text = "نسبة الخصم";
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
                if (row1 != null && radioQata3y.Checked == true)
                {
                   txtDiscount.Text = row1["نسبة الخصم"].ToString();
                    label7.Text = "نسبة الخصم";
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

        public void updateGrid(/*string PriceType, */double PurchasingPrice, double ProfitRatio, double PurchasingDiscount, double Price, double NormalIncrease, double CategoricalIncrease)
        {
            gridView1.SetRowCellValue(rowHandle, "سعر الشراء", PurchasingPrice);
            //gridView1.SetRowCellValue(rowHandle, "قيمة الخصم", ProfitRatio);
            gridView1.SetRowCellValue(rowHandle, "نسبة الخصم", PurchasingDiscount);
            gridView1.SetRowCellValue(rowHandle, "السعر", Price);
            gridView1.SetRowCellValue(rowHandle, "الزيادة العادية", NormalIncrease);
            gridView1.SetRowCellValue(rowHandle, "الزيادة القطعية", CategoricalIncrease);
        }
    }
}

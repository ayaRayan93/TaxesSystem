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
                string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"Store.txt");
                storeId = Convert.ToInt16(System.IO.File.ReadAllText(path));

                loadFunc();
                
                flag = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            conn.Close();
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
            }
            conn.Close();
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
                    string q = "SELECT DISTINCT data.Data_ID,data.Code AS 'الكود',type.Type_Name as 'النوع',concat(product.Product_Name,' ',COALESCE(color.Color_Name,''),' ',data.Description,' ',groupo.Group_Name,' ',factory.Factory_Name,' ',COALESCE(size.Size_Value,''),' ',COALESCE(sort.Sort_Value,'')) as 'الاسم',supplier_permission_details.Total_Meters as 'متر/قطعة',purchasing_price.Price AS 'السعر',purchasing_price.Purchasing_Discount AS 'خصم الشراء',purchasing_price.Normal_Increase AS 'الزيادة العادية',purchasing_price.Categorical_Increase AS 'الزيادة القطعية',purchasing_price.ProfitRatio as 'نسبة الشراء',purchasing_price.Purchasing_Price AS 'سعر الشراء',purchasing_price.PurchasingPrice_ID,purchasing_price.Price_Type as 'نوع السعر',supplier_permission_details.Supplier_Permission_Details_ID FROM storage_import_permission INNER JOIN import_supplier_permission ON import_supplier_permission.StorageImportPermission_ID = storage_import_permission.StorageImportPermission_ID inner join supplier_permission_details on supplier_permission_details.ImportSupplierPermission_ID=import_supplier_permission.ImportSupplierPermission_ID INNER JOIN data ON supplier_permission_details.Data_ID = data.Data_ID INNER JOIN type ON type.Type_ID = data.Type_ID INNER JOIN product ON product.Product_ID = data.Product_ID INNER JOIN factory ON data.Factory_ID = factory.Factory_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID LEFT JOIN color ON color.Color_ID = data.Color_ID LEFT JOIN size ON size.Size_ID = data.Size_ID LEFT JOIN sort ON sort.Sort_ID = data.Sort_ID left JOIN purchasing_price ON data.Data_ID = purchasing_price.Data_ID where storage_import_permission.StorageImportPermission_ID=" + comPermessionNum.SelectedValue.ToString() + " and import_supplier_permission.Supplier_ID=" + comSupplier.SelectedValue.ToString() + " and import_supplier_permission.ImportSupplierPermission_ID=" + comSupPerm.SelectedValue.ToString();
                    MySqlDataAdapter da = new MySqlDataAdapter(q, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    gridControl1.DataSource = dt;
                    gridView1.Columns[0].Visible = false;
                    gridView1.Columns["PurchasingPrice_ID"].Visible = false;
                    gridView1.Columns["نوع السعر"].Visible = false;
                    gridView1.Columns["Supplier_Permission_Details_ID"].Visible = false;

                    gridView1.Columns[1].Width = 170;
                    gridView1.Columns[2].Width = 300;
                    if (gridView1.IsLastVisibleRow)
                    {
                        gridView1.FocusedRowHandle = gridView1.RowCount - 1;
                    }
                    for (int i = 3; i < gridView1.Columns.Count; i++)
                    {
                        gridView1.Columns[i].Width = 120;
                    }

                    q = "SELECT DISTINCT data.Data_ID,data.Code AS 'الكود',type.Type_Name as 'النوع',concat(product.Product_Name,' ',COALESCE(color.Color_Name,''),' ',data.Description,' ',groupo.Group_Name,' ',factory.Factory_Name,' ',COALESCE(size.Size_Value,''),' ',COALESCE(sort.Sort_Value,'')) as 'الاسم',supplier_permission_details.Total_Meters as 'متر/قطعة',purchasing_price.Price AS 'السعر',purchasing_price.Purchasing_Discount AS 'خصم الشراء',purchasing_price.Normal_Increase AS 'الزيادة العادية',purchasing_price.Categorical_Increase AS 'الزيادة القطعية',purchasing_price.ProfitRatio as 'نسبة الشراء','ضريبة القيمة المضافة',purchasing_price.Purchasing_Price AS 'سعر الشراء',purchasing_price.PurchasingPrice_ID,purchasing_price.Price_Type as 'نوع السعر',supplier_permission_details.Supplier_Permission_Details_ID FROM storage_import_permission INNER JOIN import_supplier_permission ON import_supplier_permission.StorageImportPermission_ID = storage_import_permission.StorageImportPermission_ID inner join supplier_permission_details on supplier_permission_details.ImportSupplierPermission_ID=import_supplier_permission.ImportSupplierPermission_ID INNER JOIN data ON supplier_permission_details.Data_ID = data.Data_ID INNER JOIN type ON type.Type_ID = data.Type_ID INNER JOIN product ON product.Product_ID = data.Product_ID INNER JOIN factory ON data.Factory_ID = factory.Factory_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID LEFT JOIN color ON color.Color_ID = data.Color_ID LEFT JOIN size ON size.Size_ID = data.Size_ID LEFT JOIN sort ON sort.Sort_ID = data.Sort_ID left JOIN purchasing_price ON data.Data_ID = purchasing_price.Data_ID where storage_import_permission.StorageImportPermission_ID=0";
                    da = new MySqlDataAdapter(q, conn);
                    dt = new DataTable();
                    da.Fill(dt);
                    gridControl2.DataSource = dt;
                    gridView2.Columns[0].Visible = false;
                    gridView2.Columns["PurchasingPrice_ID"].Visible = false;
                    gridView2.Columns["نوع السعر"].Visible = false;
                    gridView2.Columns["Supplier_Permission_Details_ID"].Visible = false;

                    gridView2.Columns[1].Width = 170;
                    gridView2.Columns[2].Width = 300;
                    if (gridView2.IsLastVisibleRow)
                    {
                        gridView2.FocusedRowHandle = gridView2.RowCount - 1;
                    }
                    for (int i = 3; i < gridView2.Columns.Count; i++)
                    {
                        gridView2.Columns[i].Width = 120;
                    }
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
                txtPurchasePrice.Text = row1["سعر الشراء"].ToString();
                txtNormalIncrease.Text = row1["الزيادة العادية"].ToString();
                txtCategoricalIncrease.Text = row1["الزيادة القطعية"].ToString();
                txtTotalMeter.Text = row1["متر/قطعة"].ToString();
                
                if (txtCategoricalIncrease.Text == "" && txtNormalIncrease.Text == "")
                {
                    txtCategoricalIncrease.Text = txtNormalIncrease.Text = "0";
                }
                if (row1["نوع السعر"].ToString() == "لستة")
                {
                    txtDiscount.Text = row1["خصم الشراء"].ToString();
                    label7.Text = "خصم الشراء";
                    txtNormalIncrease.Visible = true;
                    txtCategoricalIncrease.Visible = true;
                    label8.Visible = true;
                    label6.Visible = true;
                }
                else if (row1["نوع السعر"].ToString() == "قطعى")
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

        private void dtnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (row1 != null)
                {
                    if (!IsAdded())
                    {
                        if (row1["سعر الشراء"].ToString() != "")
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
                                        gridView2.SetRowCellValue(rowHandl, gridView2.Columns["السعر"], txtPrice.Text);
                                        if (row1["نوع السعر"].ToString() == "لستة")
                                        {
                                            gridView2.SetRowCellValue(rowHandl, gridView2.Columns["خصم الشراء"], BuyDiscount);
                                            gridView2.SetRowCellValue(rowHandl, gridView2.Columns["الزيادة العادية"], NormalIncrease);
                                            gridView2.SetRowCellValue(rowHandl, gridView2.Columns["الزيادة القطعية"], CategoricalIncrease);
                                        }
                                        else if (row1["نوع السعر"].ToString() == "قطعى")
                                        {
                                            gridView2.SetRowCellValue(rowHandl, gridView2.Columns["نسبة الشراء"], BuyDiscount);
                                            gridView2.SetRowCellValue(rowHandl, gridView2.Columns["الزيادة العادية"], "");
                                            gridView2.SetRowCellValue(rowHandl, gridView2.Columns["الزيادة القطعية"], "");
                                        }
                                        gridView2.SetRowCellValue(rowHandl, gridView2.Columns["ضريبة القيمة المضافة"], VAT);
                                        gridView2.SetRowCellValue(rowHandl, gridView2.Columns["سعر الشراء"], purchasePrice);
                                        gridView2.SetRowCellValue(rowHandl, gridView2.Columns["متر/قطعة"], quantity);
                                        gridView2.SetRowCellValue(rowHandl, gridView2.Columns["PurchasingPrice_ID"], row1["PurchasingPrice_ID"].ToString());
                                        gridView2.SetRowCellValue(rowHandl, gridView2.Columns["نوع السعر"], row1["نوع السعر"].ToString());
                                        gridView2.SetRowCellValue(rowHandl, gridView2.Columns["Supplier_Permission_Details_ID"], row1["Supplier_Permission_Details_ID"].ToString());

                                        gridView1.DeleteRow(gridView1.FocusedRowHandle);

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
                                        Clear();
                                        row1 = null;
                                        labelTotalB.Text = totalB.ToString();
                                        labelTotalA.Text = totalA.ToString();
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
                        str += Convert.ToDouble(gridView2.GetRowCellDisplayText(gridView2.RowCount, "Supplier_Permission_Details_ID"));
                    }
                    string q = "";
                    if (str == "")
                    {
                        q = "SELECT DISTINCT data.Data_ID,data.Code AS 'الكود',type.Type_Name as 'النوع',concat(product.Product_Name,' ',COALESCE(color.Color_Name,''),' ',data.Description,' ',groupo.Group_Name,' ',factory.Factory_Name,' ',COALESCE(size.Size_Value,''),' ',COALESCE(sort.Sort_Value,'')) as 'الاسم',supplier_permission_details.Total_Meters as 'متر/قطعة',purchasing_price.Price AS 'السعر',purchasing_price.Purchasing_Discount AS 'خصم الشراء',purchasing_price.Normal_Increase AS 'الزيادة العادية',purchasing_price.Categorical_Increase AS 'الزيادة القطعية',purchasing_price.ProfitRatio as 'نسبة الشراء',purchasing_price.Purchasing_Price AS 'سعر الشراء',purchasing_price.PurchasingPrice_ID,purchasing_price.Price_Type as 'نوع السعر',supplier_permission_details.Supplier_Permission_Details_ID FROM storage_import_permission INNER JOIN import_supplier_permission ON import_supplier_permission.StorageImportPermission_ID = storage_import_permission.StorageImportPermission_ID inner join supplier_permission_details on supplier_permission_details.ImportSupplierPermission_ID=import_supplier_permission.ImportSupplierPermission_ID INNER JOIN data ON supplier_permission_details.Data_ID = data.Data_ID INNER JOIN type ON type.Type_ID = data.Type_ID INNER JOIN product ON product.Product_ID = data.Product_ID INNER JOIN factory ON data.Factory_ID = factory.Factory_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID LEFT JOIN color ON color.Color_ID = data.Color_ID LEFT JOIN size ON size.Size_ID = data.Size_ID LEFT JOIN sort ON sort.Sort_ID = data.Sort_ID left JOIN purchasing_price ON data.Data_ID = purchasing_price.Data_ID where storage_import_permission.StorageImportPermission_ID=" + comPermessionNum.SelectedValue.ToString() + " and import_supplier_permission.Supplier_ID=" + comSupplier.SelectedValue.ToString() + " and import_supplier_permission.ImportSupplierPermission_ID=" + comSupPerm.SelectedValue.ToString();
                    }
                    else
                    {
                        q = "SELECT DISTINCT data.Data_ID,data.Code AS 'الكود',type.Type_Name as 'النوع',concat(product.Product_Name,' ',COALESCE(color.Color_Name,''),' ',data.Description,' ',groupo.Group_Name,' ',factory.Factory_Name,' ',COALESCE(size.Size_Value,''),' ',COALESCE(sort.Sort_Value,'')) as 'الاسم',supplier_permission_details.Total_Meters as 'متر/قطعة',purchasing_price.Price AS 'السعر',purchasing_price.Purchasing_Discount AS 'خصم الشراء',purchasing_price.Normal_Increase AS 'الزيادة العادية',purchasing_price.Categorical_Increase AS 'الزيادة القطعية',purchasing_price.ProfitRatio as 'نسبة الشراء',purchasing_price.Purchasing_Price AS 'سعر الشراء',purchasing_price.PurchasingPrice_ID,purchasing_price.Price_Type as 'نوع السعر',supplier_permission_details.Supplier_Permission_Details_ID FROM storage_import_permission INNER JOIN import_supplier_permission ON import_supplier_permission.StorageImportPermission_ID = storage_import_permission.StorageImportPermission_ID inner join supplier_permission_details on supplier_permission_details.ImportSupplierPermission_ID=import_supplier_permission.ImportSupplierPermission_ID INNER JOIN data ON supplier_permission_details.Data_ID = data.Data_ID INNER JOIN type ON type.Type_ID = data.Type_ID INNER JOIN product ON product.Product_ID = data.Product_ID INNER JOIN factory ON data.Factory_ID = factory.Factory_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID LEFT JOIN color ON color.Color_ID = data.Color_ID LEFT JOIN size ON size.Size_ID = data.Size_ID LEFT JOIN sort ON sort.Sort_ID = data.Sort_ID left JOIN purchasing_price ON data.Data_ID = purchasing_price.Data_ID where storage_import_permission.StorageImportPermission_ID=" + comPermessionNum.SelectedValue.ToString() + " and import_supplier_permission.Supplier_ID=" + comSupplier.SelectedValue.ToString() + " and import_supplier_permission.ImportSupplierPermission_ID=" + comSupPerm.SelectedValue.ToString() + " and supplier_permission_details.Supplier_Permission_Details_ID not in (" + str + ")";
                    }
                    MySqlDataAdapter da = new MySqlDataAdapter(q, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    gridControl1.DataSource = dt;
                    gridView1.Columns[0].Visible = false;
                    gridView1.Columns["PurchasingPrice_ID"].Visible = false;
                    gridView1.Columns["نوع السعر"].Visible = false;
                    gridView1.Columns["Supplier_Permission_Details_ID"].Visible = false;

                    gridView1.Columns[1].Width = 170;
                    gridView1.Columns[2].Width = 300;
                    if (gridView1.IsLastVisibleRow)
                    {
                        gridView1.FocusedRowHandle = gridView1.RowCount - 1;
                    }
                    for (int i = 3; i < gridView1.Columns.Count; i++)
                    {
                        gridView1.Columns[i].Width = 120;
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
                if (gridView2.RowCount > 0 && comPermessionNum.SelectedValue != null && comSupplier.SelectedValue != null && comSupPerm.SelectedValue != null)
                {
                    conn.Open();
                    string q = "select Bill_No from supplier_bill where Supplier_ID=" + comSupplier.SelectedValue.ToString() + " ORDER BY Bill_ID DESC LIMIT 1 ";
                    MySqlCommand comm = new MySqlCommand(q, conn);
                    int BillNo = 1;
                    if (comm.ExecuteScalar() != null)
                    {
                        BillNo = Convert.ToInt16(comm.ExecuteScalar().ToString());
                        BillNo++;
                    }

                    string query = "insert into supplier_bill (Bill_No,Date,Import_Permission_Number,Store_ID,Total_Price_B,Total_Price_A,StorageImportPermission_ID,Supplier_ID,Supplier_Permission_Number,Employee_ID) values (@Bill_No,@Date,@Import_Permission_Number,@Store_ID,@Total_Price_B,@Total_Price_A,@StorageImportPermission_ID,@Supplier_ID,@Supplier_Permission_Number,@Employee_ID)";
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
                    com.Parameters["@Total_Price_A"].Value = labelTotalA.Text;
                    com.Parameters.Add("@StorageImportPermission_ID", MySqlDbType.Int16);
                    com.Parameters["@StorageImportPermission_ID"].Value = comPermessionNum.SelectedValue.ToString();
                    com.Parameters.Add("@Employee_ID", MySqlDbType.Int16);
                    com.Parameters["@Employee_ID"].Value = UserControl.EmpID;
                    com.Parameters.Add("@Supplier_ID", MySqlDbType.Int16);
                    com.Parameters["@Supplier_ID"].Value = comSupplier.SelectedValue.ToString();
                    com.Parameters.Add("@Supplier_Permission_Number", MySqlDbType.Int16);
                    com.Parameters["@Supplier_Permission_Number"].Value = comSupPerm.Text;
                    com.ExecuteNonQuery();

                    string q1 = "select Bill_ID from supplier_bill ORDER BY Bill_ID DESC LIMIT 1";
                    comm = new MySqlCommand(q1, conn);
                    int id = Convert.ToInt16(comm.ExecuteScalar().ToString());

                    for (int i = 0; i < gridView2.RowCount; i++)
                    {
                        DataRow row3 = gridView2.GetDataRow(i);

                        query = "insert into supplier_bill_details (Bill_ID,Data_ID,Price,Purchasing_Ratio,Purchasing_Discount,Normal_Increase,Categorical_Increase,Value_Additive_Tax,Purchasing_Price,Total_Meters,Supplier_Permission_Details_ID) values (@Bill_ID,@Data_ID,@Price,@Purchasing_Ratio,@Purchasing_Discount,@Normal_Increase,@Categorical_Increase,@Value_Additive_Tax,@Purchasing_Price,@Total_Meters,@Supplier_Permission_Details_ID)";
                        com = new MySqlCommand(query, conn);
                        com.Parameters.Add("@Bill_ID", MySqlDbType.Int16);
                        com.Parameters["@Bill_ID"].Value = id;
                        com.Parameters.Add("@Data_ID", MySqlDbType.Int16);
                        com.Parameters["@Data_ID"].Value = row3["Data_ID"].ToString();
                        com.Parameters.Add("@Price", MySqlDbType.Decimal);
                        com.Parameters["@Price"].Value = row3["السعر"].ToString();
                        if (row3["نوع السعر"].ToString() == "لستة")
                        {
                            com.Parameters.Add("@Purchasing_Discount", MySqlDbType.Decimal);
                            com.Parameters["@Purchasing_Discount"].Value = row3["خصم الشراء"].ToString();
                            com.Parameters.Add("@Purchasing_Ratio", MySqlDbType.Decimal);
                            com.Parameters["@Purchasing_Ratio"].Value = null;
                            com.Parameters.Add("@Normal_Increase", MySqlDbType.Decimal);
                            com.Parameters["@Normal_Increase"].Value = row3["الزيادة العادية"].ToString();
                            com.Parameters.Add("@Categorical_Increase", MySqlDbType.Decimal);
                            com.Parameters["@Categorical_Increase"].Value = row3["الزيادة القطعية"].ToString();
                        }
                        else if (row3["نوع السعر"].ToString() == "قطعى")
                        {
                            com.Parameters.Add("@Purchasing_Discount", MySqlDbType.Decimal);
                            com.Parameters["@Purchasing_Discount"].Value = null;
                            com.Parameters.Add("@Purchasing_Ratio", MySqlDbType.Decimal);
                            com.Parameters["@Purchasing_Ratio"].Value = row3["نسبة الشراء"].ToString();
                            com.Parameters.Add("@Normal_Increase", MySqlDbType.Decimal);
                            com.Parameters["@Normal_Increase"].Value = null;
                            com.Parameters.Add("@Categorical_Increase", MySqlDbType.Decimal);
                            com.Parameters["@Categorical_Increase"].Value = null;
                        }
                        com.Parameters.Add("@Value_Additive_Tax", MySqlDbType.Decimal);
                        com.Parameters["@Value_Additive_Tax"].Value = row3["ضريبة القيمة المضافة"].ToString();
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
                    query = "select StorageImportPermission_ID,Import_Permission_Number from storage_import_permission where Store_ID=" + storeId + " and Confirmed=0";
                    com = new MySqlCommand(query, conn);
                    MySqlDataReader dr = com.ExecuteReader();
                    while (dr.Read())
                    {
                        query = "SELECT import_supplier_permission.Supplier_Permission_Number,import_supplier_permission.ImportSupplierPermission_ID FROM import_supplier_permission where import_supplier_permission.StorageImportPermission_ID=" + dr["StorageImportPermission_ID"].ToString() + " and import_supplier_permission.Purchase_Bill=0";
                        com = new MySqlCommand(query, conn2);
                        MySqlDataReader dr2 = com.ExecuteReader();
                        while (dr2.Read())
                        {
                            flagConfirm = true;
                        }
                        dr2.Close();
                    }
                    dr.Close();
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
                    for (int i = 0; i < gridView2.RowCount; i++)
                    {
                        int rowHand = gridView2.GetRowHandle(i);
                        if (gridView2.GetRowCellDisplayText(i, gridView2.Columns["نوع السعر"]) == "لستة")
                        {
                            double lastPrice = 0;
                            lastPrice = (Convert.ToDouble(gridView2.GetRowCellDisplayText(i, gridView2.Columns["سعر الشراء"])) * 100) / (100 - Convert.ToDouble(gridView2.GetRowCellDisplayText(i, gridView2.Columns["خصم الشراء"])));
                            SupplierBill_Items item = new SupplierBill_Items() { Code = gridView2.GetRowCellDisplayText(i, gridView2.Columns["الكود"]), Product_Type = gridView2.GetRowCellDisplayText(i, gridView2.Columns["النوع"]), Product_Name = gridView2.GetRowCellDisplayText(i, gridView2.Columns["الاسم"]), Total_Meters = Convert.ToDouble(gridView2.GetRowCellDisplayText(i, gridView2.Columns["متر/قطعة"])), PriceB = Convert.ToDouble(gridView2.GetRowCellDisplayText(i, gridView2.Columns["السعر"])), Discount = Convert.ToDouble(gridView2.GetRowCellDisplayText(i, gridView2.Columns["خصم الشراء"])), Last_Price = lastPrice, Normal_Increase = Convert.ToDouble(gridView2.GetRowCellDisplayText(i, gridView2.Columns["الزيادة العادية"])), Categorical_Increase = Convert.ToDouble(gridView2.GetRowCellDisplayText(i, gridView2.Columns["الزيادة القطعية"])), PriceA = Convert.ToDouble(gridView2.GetRowCellDisplayText(i, gridView2.Columns["سعر الشراء"])) };
                            bi.Add(item);
                        }
                        else if(gridView2.GetRowCellDisplayText(i, gridView2.Columns["نوع السعر"]) == "قطعى")
                        {
                            SupplierBill_Items item = new SupplierBill_Items() { Code = gridView2.GetRowCellDisplayText(i, gridView2.Columns["الكود"]), Product_Type = gridView2.GetRowCellDisplayText(i, gridView2.Columns["النوع"]), Product_Name = gridView2.GetRowCellDisplayText(i, gridView2.Columns["الاسم"]), Total_Meters = Convert.ToDouble(gridView2.GetRowCellDisplayText(i, gridView2.Columns["متر/قطعة"])), PriceB = Convert.ToDouble(gridView2.GetRowCellDisplayText(i, gridView2.Columns["السعر"])), Discount = Convert.ToDouble(gridView2.GetRowCellDisplayText(i, gridView2.Columns["نسبة الشراء"])), Last_Price = Convert.ToDouble(gridView2.GetRowCellDisplayText(i, gridView2.Columns["سعر الشراء"])), Normal_Increase = Convert.ToDouble(gridView2.GetRowCellDisplayText(i, gridView2.Columns["الزيادة العادية"])), Categorical_Increase = Convert.ToDouble(gridView2.GetRowCellDisplayText(i, gridView2.Columns["الزيادة القطعية"])), PriceA = Convert.ToDouble(gridView2.GetRowCellDisplayText(i, gridView2.Columns["سعر الشراء"])) };
                            bi.Add(item);
                        }
                        addabtiveTax += Convert.ToDouble(gridView2.GetRowCellDisplayText(i, gridView2.Columns["ضريبة القيمة المضافة"]));
                    }
                    Report_SupplierBill f = new Report_SupplierBill();
                    f.PrintInvoice(storeName, BillNo.ToString(), comSupplier.Text, comSupPerm.Text, comPermessionNum.Text, Convert.ToDouble(labelTotalA.Text), addabtiveTax, bi);
                    f.ShowDialog();
                    #endregion

                    loadFunc();

                    NewBill();
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
        }
        //clear fields
        public void Clear()
        {
            txtCode.Text = txtPrice.Text = txtTotalMeter.Text = "";
            txtTax.Text = txtCategoricalIncrease.Text = txtDiscount.Text = txtNormalIncrease.Text = "0";
            labelTotalB.Text = txtPurchasePrice.Text = labelTotalA.Text = "";
        }

        public double calPurchasesPrice()
        {
            double price = double.Parse(txtPrice.Text);
            double PurchasesPercent = double.Parse(txtDiscount.Text);
            if (row1["نوع السعر"].ToString() == "قطعى")
            {
                return price + (price * PurchasesPercent / 100.0);
            }
            else if (row1["نوع السعر"].ToString() == "لستة")
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

        public void IncreaseSupplierAccount()
        {
            double totalSafy = Convert.ToDouble(labelTotalA.Text);
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

        public void updateGrid(/*string PriceType, */double PurchasingPrice, double ProfitRatio, double PurchasingDiscount, double Price, double NormalIncrease, double CategoricalIncrease)
        {
            gridView1.SetRowCellValue(rowHandle, "سعر الشراء", PurchasingPrice);
            gridView1.SetRowCellValue(rowHandle, "نسبة الشراء", ProfitRatio);
            gridView1.SetRowCellValue(rowHandle, "خصم الشراء", PurchasingDiscount);
            gridView1.SetRowCellValue(rowHandle, "السعر", Price);
            gridView1.SetRowCellValue(rowHandle, "الزيادة العادية", NormalIncrease);
            gridView1.SetRowCellValue(rowHandle, "الزيادة القطعية", CategoricalIncrease);
        }
    }
}

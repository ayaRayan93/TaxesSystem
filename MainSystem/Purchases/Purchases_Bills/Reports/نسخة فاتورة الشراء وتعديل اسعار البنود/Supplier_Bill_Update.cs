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
    public partial class Supplier_Bill_Update : Form
    {
        MySqlConnection conn, conn2;

        bool load = false;
        bool flag = false;
        bool loaded = false;
        bool loadPerm = false;
        bool loadSup = false;
        int storeId = 0;
        PurchaseBill_Report purchasesMainForm;
        DataRow row1 = null;
        DataRow selRow = null;
        int rowHandle = 0;

        public Supplier_Bill_Update(PurchaseBill_Report mainForm, DataRow SelRow)
        {
            InitializeComponent();
            conn = new MySqlConnection(connection.connectionString);
            conn2 = new MySqlConnection(connection.connectionString);
            purchasesMainForm = mainForm;
            selRow = SelRow;
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
                comStore.SelectedIndex = -1;
                load = true;
                comStore.Text = selRow["المخزن"].ToString();
                if (selRow["تاريخ الشراء"].ToString() != "")
                {
                    dateTimePicker1.Value = Convert.ToDateTime(selRow["تاريخ الشراء"].ToString());
                }
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
                    comSupplier.SelectedIndex = -1;
                    loadPerm = true;
                    comSupplier.Text = selRow["المورد"].ToString();
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

                    string SupplierPermissionDetailsID = "";

                    conn.Open();
                    string q = "SELECT supplier_bill_details.Supplier_Permission_Details_ID FROM supplier_bill INNER JOIN supplier_bill_details ON supplier_bill_details.Bill_ID = supplier_bill.Bill_ID INNER JOIN store ON store.Store_ID = supplier_bill.Store_ID INNER JOIN supplier ON supplier.Supplier_ID = supplier_bill.Supplier_ID INNER JOIN data ON supplier_bill_details.Data_ID = data.Data_ID INNER JOIN type ON type.Type_ID = data.Type_ID INNER JOIN product ON product.Product_ID = data.Product_ID INNER JOIN factory ON data.Factory_ID = factory.Factory_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID LEFT JOIN color ON color.Color_ID = data.Color_ID LEFT JOIN size ON size.Size_ID = data.Size_ID LEFT JOIN sort ON sort.Sort_ID = data.Sort_ID where supplier_bill.Bill_ID=" + selRow[0].ToString();
                    MySqlCommand com = new MySqlCommand(q, conn);
                    MySqlDataReader dr = com.ExecuteReader();
                    while (dr.Read())
                    {
                        SupplierPermissionDetailsID += dr["Supplier_Permission_Details_ID"].ToString() + ",";
                    }
                    dr.Close();

                    if (SupplierPermissionDetailsID.EndsWith(","))
                        SupplierPermissionDetailsID = SupplierPermissionDetailsID.Substring(0, SupplierPermissionDetailsID.Length - 1);

                    string query = "SELECT distinct import_supplier_permission.Supplier_Permission_Number,import_supplier_permission.ImportSupplierPermission_ID FROM supplier_permission_details INNER JOIN import_supplier_permission ON supplier_permission_details.ImportSupplierPermission_ID = import_supplier_permission.ImportSupplierPermission_ID where import_supplier_permission.Supplier_ID=" + comSupplier.SelectedValue.ToString() + " and import_supplier_permission.StorageImportPermission_ID=" + comPermessionNum.SelectedValue.ToString() + " and supplier_permission_details.Supplier_Permission_Details_ID in (" + SupplierPermissionDetailsID + ")";
                    MySqlDataAdapter da = new MySqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    comSupPerm.DataSource = dt;
                    comSupPerm.DisplayMember = dt.Columns["Supplier_Permission_Number"].ToString();
                    comSupPerm.ValueMember = dt.Columns["ImportSupplierPermission_ID"].ToString();
                    comSupPerm.SelectedIndex = -1;
                    loadSup = true;
                    comSupPerm.Text = selRow["اذن الاستلام"].ToString();
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

                    string q = "SELECT DISTINCT data.Data_ID,data.Code AS 'الكود',type.Type_Name as 'النوع',concat(product.Product_Name,' ',COALESCE(color.Color_Name,''),' ',data.Description,' ',groupo.Group_Name,' ',factory.Factory_Name,' ',COALESCE(size.Size_Value,''),' ',COALESCE(sort.Sort_Value,'')) as 'الاسم',supplier_bill_details.Total_Meters as 'متر/قطعة',supplier_bill_details.Price AS 'السعر',supplier_bill_details.Purchasing_Discount AS 'نسبة الخصم',supplier_bill_details.Normal_Increase AS 'الزيادة العادية',supplier_bill_details.Categorical_Increase AS 'الزيادة القطعية',supplier_bill_details.Last_Price AS 'السعر بالزيادة',supplier_bill_details.Purchasing_Price AS 'سعر الشراء',(supplier_bill_details.Purchasing_Price*supplier_bill_details.Total_Meters) as 'الاجمالى بعد',purchasing_price.PurchasingPrice_ID,supplier_bill_details.Price_Type as 'نوع السعر',supplier_bill_details.Supplier_Permission_Details_ID FROM supplier_bill_details INNER JOIN supplier_bill ON supplier_bill_details.Bill_ID = supplier_bill.Bill_ID INNER JOIN data ON supplier_bill_details.Data_ID = data.Data_ID INNER JOIN type ON type.Type_ID = data.Type_ID INNER JOIN product ON product.Product_ID = data.Product_ID INNER JOIN factory ON data.Factory_ID = factory.Factory_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID LEFT JOIN color ON color.Color_ID = data.Color_ID LEFT JOIN size ON size.Size_ID = data.Size_ID LEFT JOIN sort ON sort.Sort_ID = data.Sort_ID left JOIN purchasing_price ON data.Data_ID = purchasing_price.Data_ID where supplier_bill.StorageImportPermission_ID=" + comPermessionNum.SelectedValue.ToString() + " and supplier_bill.Supplier_ID = " + comSupplier.SelectedValue.ToString() + " and supplier_bill.Supplier_Permission_Number=" + comSupPerm.Text + " and supplier_Bill.Bill_No=" + selRow["رقم الفاتورة"].ToString();
                    MySqlDataAdapter da = new MySqlDataAdapter(q, conn);
                    DataTable dt = new DataTable();
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
                    txtAllTax.Text = selRow["ضريبة القيمة المضافة"].ToString();
                    labelTotalB.Text = totalAllB.ToString("#.000");
                    labelTotalDiscount.Text = totalAllDiscount.ToString("#.000");
                    labelTotalCat.Text = totalAllCatInc.ToString("#.000");
                    labelTotalA.Text = totalAllA.ToString("#.000");
                    labelTotalSafy.Text = (Convert.ToDouble(labelTotalA.Text) + (Convert.ToDouble(labelTotalA.Text) * (Convert.ToDouble(txtAllTax.Text) / 100))).ToString("#.000");
                    
                    string SupplierPermissionDetailsID = "";
                    if (gridView2.RowCount > 0)
                    {
                        for (int i = 0; i < gridView2.RowCount - 1; i++)
                        {
                            int rowhandle = gridView2.GetRowHandle(i);
                            SupplierPermissionDetailsID += gridView2.GetRowCellDisplayText(rowhandle, "Supplier_Permission_Details_ID").ToString() + ",";
                        }
                        int rowhandl = gridView2.GetRowHandle(gridView2.RowCount - 1);
                        SupplierPermissionDetailsID += gridView2.GetRowCellDisplayText(rowhandl, "Supplier_Permission_Details_ID").ToString();
                    }
                    q = "SELECT DISTINCT data.Data_ID,data.Code AS 'الكود',type.Type_Name as 'النوع',concat(product.Product_Name,' ',COALESCE(color.Color_Name,''),' ',data.Description,' ',groupo.Group_Name,' ',factory.Factory_Name,' ',COALESCE(size.Size_Value,''),' ',COALESCE(sort.Sort_Value,'')) as 'الاسم',supplier_permission_details.Total_Meters as 'متر/قطعة',purchasing_price.Price AS 'السعر',purchasing_price.Purchasing_Discount AS 'نسبة الخصم',purchasing_price.Normal_Increase AS 'الزيادة العادية',purchasing_price.Categorical_Increase AS 'الزيادة القطعية',purchasing_price.Last_Price AS 'السعر بالزيادة',purchasing_price.Purchasing_Price AS 'سعر الشراء',(purchasing_price.Purchasing_Price*supplier_permission_details.Total_Meters) as 'الاجمالى بعد',purchasing_price.PurchasingPrice_ID,purchasing_price.Price_Type as 'نوع السعر',supplier_permission_details.Date as 'التاريخ',supplier_permission_details.Supplier_Permission_Details_ID FROM storage_import_permission INNER JOIN import_supplier_permission ON import_supplier_permission.StorageImportPermission_ID = storage_import_permission.StorageImportPermission_ID inner join supplier_permission_details on supplier_permission_details.ImportSupplierPermission_ID=import_supplier_permission.ImportSupplierPermission_ID INNER JOIN data ON supplier_permission_details.Data_ID = data.Data_ID INNER JOIN type ON type.Type_ID = data.Type_ID INNER JOIN product ON product.Product_ID = data.Product_ID INNER JOIN factory ON data.Factory_ID = factory.Factory_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID LEFT JOIN color ON color.Color_ID = data.Color_ID LEFT JOIN size ON size.Size_ID = data.Size_ID LEFT JOIN sort ON sort.Sort_ID = data.Sort_ID left JOIN purchasing_price ON data.Data_ID = purchasing_price.Data_ID where storage_import_permission.StorageImportPermission_ID=" + comPermessionNum.SelectedValue.ToString() + " and import_supplier_permission.Supplier_ID=" + comSupplier.SelectedValue.ToString() + " and import_supplier_permission.ImportSupplierPermission_ID=" + comSupPerm.SelectedValue.ToString() + " and supplier_permission_details.Supplier_Permission_Details_ID not in (" + SupplierPermissionDetailsID + ")";
                    da = new MySqlDataAdapter(q, conn);
                    dt = new DataTable();
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

                    loaded = true;
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
                if (row1["نوع السعر"].ToString() == "لستة")
                {
                    radioList.Checked = true;
                    txtDiscount.Text = row1["نسبة الخصم"].ToString();
                    label7.Text = "نسبة الخصم";
                }
                else if (row1["نوع السعر"].ToString() == "قطعى")
                {
                    radioQata3y.Checked = true;
                    txtDiscount.Text = row1["نسبة الخصم"].ToString();
                    label7.Text = "نسبة الخصم";
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
                                    gridView2.SetRowCellValue(rowHandl, gridView2.Columns["السعر بالزيادة"], System.Math.Round(Convert.ToDouble(txtLastPrice.Text), 2));
                                    gridView2.SetRowCellValue(rowHandl, gridView2.Columns["سعر الشراء"], System.Math.Round(purchasePrice, 2));
                                    gridView2.SetRowCellValue(rowHandl, gridView2.Columns["متر/قطعة"], quantity);
                                    gridView2.SetRowCellValue(rowHandl, gridView2.Columns["الاجمالى بعد"], System.Math.Round(purchasePrice, 2) * quantity);
                                    gridView2.SetRowCellValue(rowHandl, gridView2.Columns["PurchasingPrice_ID"], row1["PurchasingPrice_ID"].ToString());
                                    gridView2.SetRowCellValue(rowHandl, gridView2.Columns["Supplier_Permission_Details_ID"], row1["Supplier_Permission_Details_ID"].ToString());
                                    
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
                                    labelTotalB.Text = labelTotalA.Text = labelTotalDiscount.Text = labelTotalSafy.Text= labelTotalCat.Text = "";
                                    labelTotalB.Text = totalAllB.ToString("#.000");
                                    labelTotalDiscount.Text = totalAllDiscount.ToString("#.000");
                                    labelTotalCat.Text = totalAllCatInc.ToString("#.000");
                                    labelTotalA.Text = totalAllA.ToString("#.000");
                                    labelTotalSafy.Text = (Convert.ToDouble(labelTotalA.Text) + (Convert.ToDouble(labelTotalA.Text) * (Convert.ToDouble(txtAllTax.Text) / 100))).ToString("#.000");
                                    
                                    #region Save
                                    string query = "update supplier_bill set Total_Price_B=@Total_Price_B,Total_Price_A=@Total_Price_A,Value_Additive_Tax=@Value_Additive_Tax where Bill_ID=" + selRow[0].ToString();
                                    MySqlCommand com = new MySqlCommand(query, conn);
                                    com.Parameters.Add("@Total_Price_B", MySqlDbType.Decimal);
                                    com.Parameters["@Total_Price_B"].Value = labelTotalB.Text;
                                    com.Parameters.Add("@Total_Price_A", MySqlDbType.Decimal);
                                    com.Parameters["@Total_Price_A"].Value = labelTotalSafy.Text;
                                    com.Parameters.Add("@Value_Additive_Tax", MySqlDbType.Decimal);
                                    com.Parameters["@Value_Additive_Tax"].Value = txtAllTax.Text;
                                    com.ExecuteNonQuery();

                                    query = "insert into supplier_bill_details (Bill_ID,Data_ID,Price_Type,Price,Last_Price,Purchasing_Discount,Normal_Increase,Categorical_Increase,Purchasing_Price,Total_Meters,Supplier_Permission_Details_ID) values (@Bill_ID,@Data_ID,@Price_Type,@Price,@Last_Price,@Purchasing_Discount,@Normal_Increase,@Categorical_Increase,@Purchasing_Price,@Total_Meters,@Supplier_Permission_Details_ID)";
                                    com = new MySqlCommand(query, conn);
                                    com.Parameters.Add("@Bill_ID", MySqlDbType.Int16);
                                    com.Parameters["@Bill_ID"].Value = selRow[0].ToString();
                                    com.Parameters.Add("@Data_ID", MySqlDbType.Int16);
                                    com.Parameters["@Data_ID"].Value = row1["Data_ID"].ToString();
                                    com.Parameters.Add("@Price", MySqlDbType.Decimal);
                                    com.Parameters["@Price"].Value = System.Math.Round(Convert.ToDouble(txtPrice.Text), 2);
                                    if (radioList.Checked == true)
                                    {
                                        com.Parameters.Add("@Price_Type", MySqlDbType.VarChar);
                                        com.Parameters["@Price_Type"].Value = "لستة";
                                        com.Parameters.Add("@Purchasing_Discount", MySqlDbType.Decimal);
                                        com.Parameters["@Purchasing_Discount"].Value = System.Math.Round(BuyDiscount, 2);
                                        com.Parameters.Add("@Normal_Increase", MySqlDbType.Decimal);
                                        com.Parameters["@Normal_Increase"].Value = System.Math.Round(NormalIncrease, 2);
                                        com.Parameters.Add("@Categorical_Increase", MySqlDbType.Decimal);
                                        com.Parameters["@Categorical_Increase"].Value = System.Math.Round(CategoricalIncrease, 2);
                                    }
                                    else if (radioQata3y.Checked == true)
                                    {
                                        com.Parameters.Add("@Price_Type", MySqlDbType.VarChar);
                                        com.Parameters["@Price_Type"].Value = "قطعى";
                                        com.Parameters.Add("@Purchasing_Discount", MySqlDbType.Decimal);
                                        com.Parameters["@Purchasing_Discount"].Value = System.Math.Round(BuyDiscount, 2);
                                        com.Parameters.Add("@Normal_Increase", MySqlDbType.Decimal);
                                        com.Parameters["@Normal_Increase"].Value = System.Math.Round(NormalIncrease, 2);
                                        com.Parameters.Add("@Categorical_Increase", MySqlDbType.Decimal);
                                        com.Parameters["@Categorical_Increase"].Value = System.Math.Round(CategoricalIncrease, 2);
                                    }
                                    com.Parameters.Add("@Last_Price", MySqlDbType.Decimal);
                                    com.Parameters["@Last_Price"].Value = System.Math.Round(Convert.ToDouble(txtLastPrice.Text), 2);
                                    com.Parameters.Add("@Purchasing_Price", MySqlDbType.Decimal);
                                    com.Parameters["@Purchasing_Price"].Value = System.Math.Round(purchasePrice, 2);
                                    com.Parameters.Add("@Total_Meters", MySqlDbType.Decimal);
                                    com.Parameters["@Total_Meters"].Value = quantity;
                                    com.Parameters.Add("@Supplier_Permission_Details_ID", MySqlDbType.Int16);
                                    com.Parameters["@Supplier_Permission_Details_ID"].Value = row1["Supplier_Permission_Details_ID"].ToString();
                                    com.ExecuteNonQuery();

                                    IncreaseSupplierAccount();

                                    gridView1.DeleteRow(gridView1.FocusedRowHandle);
                                    row1 = null;
                                    txtCode.Text = txtPrice.Text = txtLastPrice.Text = txtTotalMeter.Text = "";
                                    txtTax.Text = txtCategoricalIncrease.Text = txtDiscount.Text = txtNormalIncrease.Text = "0";
                                    txtPurchasePrice.Text = txtLastPrice.Text = "";
                                    #endregion
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
                    conn.Open();
                    string query = "delete from supplier_bill_details where Bill_ID=" + selRow[0].ToString() + " and Data_ID=" + row2["Data_ID"].ToString() + " and Supplier_Permission_Details_ID=" + row2["Supplier_Permission_Details_ID"].ToString();
                    MySqlCommand com = new MySqlCommand(query, conn);
                    com.ExecuteNonQuery();

                    DecreaseSupplierAccount(row2);

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

                    #region Save
                    query = "update supplier_bill set Total_Price_B=@Total_Price_B,Total_Price_A=@Total_Price_A,Value_Additive_Tax=@Value_Additive_Tax where Bill_ID=" + selRow[0].ToString();
                    com = new MySqlCommand(query, conn);
                    com.Parameters.Add("@Total_Price_B", MySqlDbType.Decimal);
                    com.Parameters["@Total_Price_B"].Value = labelTotalB.Text;
                    com.Parameters.Add("@Total_Price_A", MySqlDbType.Decimal);
                    com.Parameters["@Total_Price_A"].Value = labelTotalSafy.Text;
                    com.Parameters.Add("@Value_Additive_Tax", MySqlDbType.Decimal);
                    com.Parameters["@Value_Additive_Tax"].Value = txtAllTax.Text;
                    com.ExecuteNonQuery();
                    #endregion
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            conn.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (gridView2.RowCount > 0 && txtAllTax.Text != "" && comPermessionNum.SelectedValue != null && comSupplier.SelectedValue != null && comSupPerm.SelectedValue != null)
            {
                try
                {
                    conn.Open();
                    string query = "update supplier_bill set Import_Date=@Import_Date where Bill_ID=" + selRow[0].ToString();
                    MySqlCommand com = new MySqlCommand(query, conn);
                    com.Parameters.Add("@Import_Date", MySqlDbType.Date);
                    com.Parameters["@Import_Date"].Value = dateTimePicker1.Value.Date;
                    com.ExecuteNonQuery();

                    #region report
                    query = "select Store_Name from store where Store_ID=" + storeId;
                    com = new MySqlCommand(query, conn);
                    string storeName = com.ExecuteScalar().ToString();

                    double addabtiveTax = 0;
                    List<SupplierBill_Items> bi = new List<SupplierBill_Items>();
                    double discount = 0;
                    for (int i = 0; i < gridView2.RowCount; i++)
                    {
                        if (gridView2.GetRowCellDisplayText(i, gridView2.Columns["نوع السعر"]) == "لستة")
                        {
                            discount += (Convert.ToDouble(gridView2.GetRowCellDisplayText(i, gridView2.Columns["السعر بالزيادة"]).ToString()) * Convert.ToDouble(gridView2.GetRowCellDisplayText(i, gridView2.Columns["متر/قطعة"]).ToString())) * (Convert.ToDouble(gridView2.GetRowCellDisplayText(i, gridView2.Columns["نسبة الخصم"]).ToString()) / 100);
                            SupplierBill_Items item = new SupplierBill_Items() { Code = gridView2.GetRowCellDisplayText(i, gridView2.Columns["الكود"]), Product_Type = gridView2.GetRowCellDisplayText(i, gridView2.Columns["النوع"]), Product_Name = gridView2.GetRowCellDisplayText(i, gridView2.Columns["الاسم"]), Total_Meters = Convert.ToDouble(gridView2.GetRowCellDisplayText(i, gridView2.Columns["متر/قطعة"])), PriceB = Convert.ToDouble(gridView2.GetRowCellDisplayText(i, gridView2.Columns["السعر"])), Discount = Convert.ToDouble(gridView2.GetRowCellDisplayText(i, gridView2.Columns["نسبة الخصم"])), Last_Price = Convert.ToDouble(gridView2.GetRowCellDisplayText(i, gridView2.Columns["السعر بالزيادة"])), Normal_Increase = Convert.ToDouble(gridView2.GetRowCellDisplayText(i, gridView2.Columns["الزيادة العادية"])), Categorical_Increase = Convert.ToDouble(gridView2.GetRowCellDisplayText(i, gridView2.Columns["الزيادة القطعية"])), PriceA = Convert.ToDouble(gridView2.GetRowCellDisplayText(i, gridView2.Columns["سعر الشراء"])) };
                            bi.Add(item);
                        }
                        else if (gridView2.GetRowCellDisplayText(i, gridView2.Columns["نوع السعر"]) == "قطعى")
                        {
                            discount += (Convert.ToDouble(gridView2.GetRowCellDisplayText(i, gridView2.Columns["السعر بالزيادة"]).ToString()) * Convert.ToDouble(gridView2.GetRowCellDisplayText(i, gridView2.Columns["متر/قطعة"]).ToString())) * (Convert.ToDouble(gridView2.GetRowCellDisplayText(i, gridView2.Columns["نسبة الخصم"]).ToString()) / 100);
                            SupplierBill_Items item = new SupplierBill_Items() { Code = gridView2.GetRowCellDisplayText(i, gridView2.Columns["الكود"]), Product_Type = gridView2.GetRowCellDisplayText(i, gridView2.Columns["النوع"]), Product_Name = gridView2.GetRowCellDisplayText(i, gridView2.Columns["الاسم"]), Total_Meters = Convert.ToDouble(gridView2.GetRowCellDisplayText(i, gridView2.Columns["متر/قطعة"])), PriceB = Convert.ToDouble(gridView2.GetRowCellDisplayText(i, gridView2.Columns["السعر"])), Discount = Convert.ToDouble(gridView2.GetRowCellDisplayText(i, gridView2.Columns["نسبة الخصم"])), Last_Price = Convert.ToDouble(gridView2.GetRowCellDisplayText(i, gridView2.Columns["السعر بالزيادة"])), Normal_Increase = Convert.ToDouble(gridView2.GetRowCellDisplayText(i, gridView2.Columns["الزيادة العادية"])), Categorical_Increase = Convert.ToDouble(gridView2.GetRowCellDisplayText(i, gridView2.Columns["الزيادة القطعية"])), PriceA = Convert.ToDouble(gridView2.GetRowCellDisplayText(i, gridView2.Columns["سعر الشراء"])) };
                            bi.Add(item);
                        }
                    }
                    addabtiveTax = Convert.ToDouble(txtAllTax.Text);
                    Report_SupplierBillCopy f = new Report_SupplierBillCopy();
                    f.PrintInvoice(storeName, selRow["رقم الفاتورة"].ToString(), comSupplier.Text, comSupPerm.Text, comPermessionNum.Text, selRow["التاريخ"].ToString(), discount, Convert.ToDouble(labelTotalA.Text), addabtiveTax, Convert.ToDouble(labelTotalSafy.Text), dateTimePicker1.Value.Date.ToString(), bi);
                    f.ShowDialog();
                    #endregion
                    
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                conn2.Close();
                conn.Close();
            }
            else
            {
                MessageBox.Show("يجب التاكد من البيانات واختيار البنود");
            }
        }

        //function
        void loadFunc()
        {
            string query = "select StorageImportPermission_ID,Import_Permission_Number from storage_import_permission where Store_ID=" + storeId;
            MySqlDataAdapter da = new MySqlDataAdapter(query, conn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            comPermessionNum.DataSource = dt;
            comPermessionNum.DisplayMember = dt.Columns["Import_Permission_Number"].ToString();
            comPermessionNum.ValueMember = dt.Columns["StorageImportPermission_ID"].ToString();
            comPermessionNum.SelectedIndex = -1;
            flag = true;
            comPermessionNum.Text = selRow["اذن المخزن"].ToString();
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
            txtTax.Text = txtCategoricalIncrease.Text = txtDiscount.Text = txtNormalIncrease.Text = "0";
            labelTotalB.Text = labelTotalA.Text = txtPurchasePrice.Text = txtLastPrice.Text /*= labelTotalVal.Text*/ = labelTotalDiscount.Text = labelTotalSafy.Text = labelTotalCat.Text = "";
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

        public void IncreaseSupplierAccount()
        {
            double totalSafy = Convert.ToDouble(txtPurchasePrice.Text) * Convert.ToDouble(txtTotalMeter.Text);
            string query = "select Money from supplier_rest_money where Supplier_ID=" + comSupplier.SelectedValue.ToString();
            MySqlCommand com = new MySqlCommand(query, conn);
            if (com.ExecuteScalar() != null)
            {
                double restMoney = Convert.ToDouble(com.ExecuteScalar());
                query = "update supplier_rest_money set Money=" + (restMoney + totalSafy) + " where Supplier_ID=" + comSupplier.SelectedValue.ToString();
                com = new MySqlCommand(query, conn);
            }
            com.ExecuteNonQuery();
        }

        public void DecreaseSupplierAccount(DataRow row2)
        {
            double totalSafy = Convert.ToDouble(row2["الاجمالى بعد"].ToString());
            string query = "select Money from supplier_rest_money where Supplier_ID=" + comSupplier.SelectedValue.ToString();
            MySqlCommand com = new MySqlCommand(query, conn);
            if (com.ExecuteScalar() != null)
            {
                double restMoney = Convert.ToDouble(com.ExecuteScalar());
                query = "update supplier_rest_money set Money=" + (restMoney - totalSafy) + " where Supplier_ID=" + comSupplier.SelectedValue.ToString();
                com = new MySqlCommand(query, conn);
            }
            com.ExecuteNonQuery();
        }

        private void radioList_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void radioQata3y_CheckedChanged(object sender, EventArgs e)
        {
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

        private void comStore_SelectedValueChanged(object sender, EventArgs e)
        {
            if (load)
            {
                try
                {
                    storeId = Convert.ToInt16(comStore.SelectedValue.ToString());
                    loadFunc();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                conn.Close();
            }
        }
    }
}

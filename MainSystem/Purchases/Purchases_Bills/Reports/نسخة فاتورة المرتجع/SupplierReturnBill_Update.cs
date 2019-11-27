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
    public partial class SupplierReturnBill_Update : Form
    {
        MySqlConnection dbconnection, dbconnection2;
        bool loaded = false;
        DataRow row1 = null;
        int rowHandle = -1;
        int ImportStorageReturnId = 0;
        int storageImportPermissionId = 0;
        bool loadSup = false;
        bool loadSupPerm = false;
        DataRow selrow = null;

        public SupplierReturnBill_Update(ReturnedPurchaseBill_Report mainForm, DataRow SelRow)
        {
            InitializeComponent();
            dbconnection = new MySqlConnection(connection.connectionString);
            dbconnection2 = new MySqlConnection(connection.connectionString);
            selrow = SelRow;
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
                comStore.SelectedIndex = -1;
                comStore.Text = selrow["المخزن"].ToString();
                
                txtBillNumber.Text = selrow["اذن المخزن"].ToString();

                comSupplier.Text = selrow["المورد"].ToString();
                
                comSupPerm.Text = selrow["اذن الاستلام"].ToString();
                loaded = true;
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
                        loadSup = false;
                        loadSupPerm = false;
                        comSupPerm.DataSource = null;
                        comSupplier.DataSource = null;
                    }
                }
                else
                {
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
                                    double lastPrece = Convert.ToDouble(txtLastPrice.Text);
                                    dbconnection.Open();
                                    string query = "insert into supplier_return_bill_details (ReturnBill_ID,Data_ID,Price_Type,Price,Last_Price,Purchasing_Discount,Normal_Increase,Categorical_Increase,Purchasing_Price,Total_Meters,BillData_ID,ImportStorageReturnDetails_ID) values (@ReturnBill_ID,@Data_ID,@Price_Type,@Price,@Last_Price,@Purchasing_Discount,@Normal_Increase,@Categorical_Increase,@Purchasing_Price,@Total_Meters,@BillData_ID,@ImportStorageReturnDetails_ID)";
                                    MySqlCommand com = new MySqlCommand(query, dbconnection);
                                    com.Parameters.Add("@ReturnBill_ID", MySqlDbType.Int16);
                                    com.Parameters["@ReturnBill_ID"].Value = selrow[0].ToString();
                                    com.Parameters.Add("@Data_ID", MySqlDbType.Int16);
                                    com.Parameters["@Data_ID"].Value = row1["Data_ID"].ToString();

                                    if (radioList.Checked == true)
                                    {
                                        com.Parameters.Add("@Price_Type", MySqlDbType.VarChar);
                                        com.Parameters["@Price_Type"].Value = "لستة";
                                    }
                                    else if (radioQata3y.Checked == true)
                                    {
                                        com.Parameters.Add("@Price_Type", MySqlDbType.VarChar);
                                        com.Parameters["@Price_Type"].Value = "قطعى";
                                    }
                                    
                                    com.Parameters.Add("@Price", MySqlDbType.Decimal);
                                    com.Parameters["@Price"].Value = System.Math.Round(price, 2);
                                    com.Parameters.Add("@Purchasing_Discount", MySqlDbType.Decimal);
                                    com.Parameters["@Purchasing_Discount"].Value = System.Math.Round(PurchaseDiscount, 2);
                                    com.Parameters.Add("@Normal_Increase", MySqlDbType.Decimal);
                                    com.Parameters["@Normal_Increase"].Value = System.Math.Round(NormalIncrease, 2);
                                    com.Parameters.Add("@Categorical_Increase", MySqlDbType.Decimal);
                                    com.Parameters["@Categorical_Increase"].Value = System.Math.Round(Categorical_Increase, 2);
                                    com.Parameters.Add("@Last_Price", MySqlDbType.Decimal);
                                    com.Parameters["@Last_Price"].Value = System.Math.Round(lastPrece, 2);
                                    com.Parameters.Add("@Purchasing_Price", MySqlDbType.Decimal);
                                    com.Parameters["@Purchasing_Price"].Value = System.Math.Round(purchasePrice, 2);
                                    com.Parameters.Add("@Total_Meters", MySqlDbType.Decimal);
                                    com.Parameters["@Total_Meters"].Value = quantity;
                                    if (row1["BillData_ID"].ToString() != "")
                                    {
                                        com.Parameters.Add("@BillData_ID", MySqlDbType.Int16);
                                        com.Parameters["@BillData_ID"].Value = row1["BillData_ID"].ToString();
                                        com.Parameters.Add("@ImportStorageReturnDetails_ID", MySqlDbType.Int16);
                                        com.Parameters["@ImportStorageReturnDetails_ID"].Value = null;
                                    }
                                    else if (row1["ImportStorageReturnDetails_ID"].ToString() != "")
                                    {
                                        com.Parameters.Add("@BillData_ID", MySqlDbType.Int16);
                                        com.Parameters["@BillData_ID"].Value = null;
                                        com.Parameters.Add("@ImportStorageReturnDetails_ID", MySqlDbType.Int16);
                                        com.Parameters["@ImportStorageReturnDetails_ID"].Value = row1["ImportStorageReturnDetails_ID"].ToString();
                                    }
                                    com.ExecuteNonQuery();

                                    query = "select ReturnBillDetails_ID from supplier_return_bill_details order by ReturnBillDetails_ID desc limit 1";
                                    com = new MySqlCommand(query, dbconnection);
                                    string ReturnBillDetailsID = com.ExecuteScalar().ToString();

                                    DecreaseSupplierAccount(System.Math.Round(purchasePrice, 2));
                                    
                                    gridView2.AddNewRow();
                                    int rowHandl = gridView2.GetRowHandle(gridView2.DataRowCount);
                                    if (gridView2.IsNewItemRow(rowHandl))
                                    {
                                        gridView2.SetRowCellValue(rowHandl, gridView2.Columns["ReturnBillDetails_ID"], ReturnBillDetailsID);
                                        gridView2.SetRowCellValue(rowHandl, gridView2.Columns["Data_ID"], row1["Data_ID"].ToString());
                                        gridView2.SetRowCellValue(rowHandl, gridView2.Columns["الكود"], row1["الكود"].ToString());
                                        gridView2.SetRowCellValue(rowHandl, gridView2.Columns["النوع"], row1["النوع"].ToString());
                                        gridView2.SetRowCellValue(rowHandl, gridView2.Columns["الاسم"], row1["الاسم"].ToString());
                                        gridView2.SetRowCellValue(rowHandl, gridView2.Columns["السعر"], System.Math.Round(price, 2));
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

                                        displayPermissionDetails();

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
                                        row1 = null;
                                        rowHandle = -1;
                                        txtTotalMeter.Text = txtCode.Text = "";
                                        txtDiscount.Text = txtNormalIncrease.Text = txtCategoricalIncrease.Text = "0";
                                        txtPrice.Text = txtLastPrice.Text = txtPurchasePrice.Text = "";
                                        txtTax.Text = "0";
                                        labelTotalB.Text = totalAllB.ToString("#.000");
                                        labelTotalDiscount.Text = totalAllDiscount.ToString("#.000");
                                        labelTotalCat.Text = totalAllCatInc.ToString("#.000");
                                        labelTotalA.Text = totalAllA.ToString("#.000");
                                        labelTotalSafy.Text = (Convert.ToDouble(labelTotalA.Text) + (Convert.ToDouble(labelTotalA.Text) * (Convert.ToDouble(txtAllTax.Text) / 100))).ToString("#.000");
                                        
                                        query = "update supplier_return_bill set Total_Price_BD=@Total_Price_BD,Total_Price_AD=@Total_Price_AD,Value_Additive_Tax=@Value_Additive_Tax where ReturnBill_ID=" + selrow[0].ToString();
                                        com = new MySqlCommand(query, dbconnection);
                                        com.Parameters.Add("@Total_Price_BD", MySqlDbType.Decimal);
                                        com.Parameters["@Total_Price_BD"].Value = Convert.ToDouble(labelTotalB.Text);
                                        com.Parameters.Add("@Total_Price_AD", MySqlDbType.Decimal);
                                        com.Parameters["@Total_Price_AD"].Value = Convert.ToDouble(labelTotalSafy.Text);
                                        com.Parameters.Add("@Value_Additive_Tax", MySqlDbType.Decimal);
                                        com.Parameters["@Value_Additive_Tax"].Value = txtAllTax.Text;
                                        com.ExecuteNonQuery();
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
        
        private void btnRemove_Click(object sender, EventArgs e)
        {
            try
            {
                DataRow row2 = gridView2.GetDataRow(gridView2.FocusedRowHandle);
                if (row2 != null)
                {
                    dbconnection.Open();
                    string query = "delete from supplier_return_bill_details where ReturnBillDetails_ID=" + row2["ReturnBillDetails_ID"].ToString();
                    MySqlCommand com = new MySqlCommand(query, dbconnection);
                    com.ExecuteNonQuery();

                    IncreaseSupplierAccount(row2);

                    gridView2.DeleteRow(gridView2.FocusedRowHandle);

                    displayPermissionDetails();

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

                    query = "update supplier_return_bill set Total_Price_BD=@Total_Price_BD,Total_Price_AD=@Total_Price_AD,Value_Additive_Tax=@Value_Additive_Tax where ReturnBill_ID=" + selrow[0].ToString();
                    com = new MySqlCommand(query, dbconnection);
                    com.Parameters.Add("@Total_Price_BD", MySqlDbType.Decimal);
                    com.Parameters["@Total_Price_BD"].Value = Convert.ToDouble(labelTotalB.Text);
                    com.Parameters.Add("@Total_Price_AD", MySqlDbType.Decimal);
                    com.Parameters["@Total_Price_AD"].Value = Convert.ToDouble(labelTotalSafy.Text);
                    com.Parameters.Add("@Value_Additive_Tax", MySqlDbType.Decimal);
                    com.Parameters["@Value_Additive_Tax"].Value = txtAllTax.Text;
                    com.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (gridView2.RowCount > 0)
                {
                    dbconnection.Open();

                    #region report
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
                    Report_SupplierReturnBillCopy f = new Report_SupplierReturnBillCopy();
                    f.PrintInvoice(storeName, selrow["رقم الفاتورة"].ToString(), comSupplier.Text, txtBillNumber.Text, comSupPerm.Text, selrow["التاريخ"].ToString(), Convert.ToDouble(labelTotalDiscount.Text), Convert.ToDouble(labelTotalA.Text), addabtiveTax, Convert.ToDouble(labelTotalSafy.Text), bi);
                    f.ShowDialog();
                    #endregion

                    dbconnection.Close();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("تاكد من البيانات");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            dbconnection.Close();
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
                string q = "SELECT supplier_bill_details.Data_ID,data.Code as 'الكود',type.Type_Name as 'النوع',concat(product.Product_Name,' ',COALESCE(color.Color_Name,''),' ',data.Description,' ',groupo.Group_Name,' ',factory.Factory_Name,' ',COALESCE(size.Size_Value,''),' ',COALESCE(sort.Sort_Value,'')) as 'الاسم',supplier_bill_details.Price as 'السعر',supplier_bill_details.Last_Price as 'السعر بالزيادة',supplier_bill_details.Purchasing_Discount as 'نسبة الخصم',supplier_bill_details.Normal_Increase as 'الزيادة العادية',supplier_bill_details.Categorical_Increase as 'الزيادة القطعية',supplier_bill_details.Purchasing_Price as 'سعر الشراء',supplier_bill_details.Total_Meters 'متر/قطعة',supplier_bill_details.Price_Type,'BillData_ID','ImportStorageReturnDetails_ID' FROM supplier_bill INNER JOIN supplier_bill_details ON supplier_bill_details.Bill_ID = supplier_bill.Bill_ID INNER JOIN import_storage_return_details ON supplier_bill_details.Data_ID = import_storage_return_details.Data_ID INNER JOIN data ON data.Data_ID = supplier_bill_details.Data_ID INNER JOIN type ON type.Type_ID = data.Type_ID INNER JOIN product ON product.Product_ID = data.Product_ID INNER JOIN factory ON data.Factory_ID = factory.Factory_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID LEFT JOIN color ON color.Color_ID = data.Color_ID LEFT JOIN size ON size.Size_ID = data.Size_ID LEFT JOIN sort ON sort.Sort_ID = data.Sort_ID where supplier_bill.StorageImportPermission_ID=" + 0;
                MySqlDataAdapter da = new MySqlDataAdapter(q, dbconnection);
                DataTable dt = new DataTable();
                da.Fill(dt);
                gridControl1.DataSource = dt;

                q = "SELECT supplier_bill_details.Data_ID,data.Code as 'الكود',type.Type_Name as 'النوع',concat(product.Product_Name,' ',COALESCE(color.Color_Name,''),' ',data.Description,' ',groupo.Group_Name,' ',factory.Factory_Name,' ',COALESCE(size.Size_Value,''),' ',COALESCE(sort.Sort_Value,'')) as 'الاسم',supplier_bill_details.Price as 'السعر',supplier_bill_details.Last_Price as 'السعر بالزيادة',supplier_bill_details.Purchasing_Discount as 'نسبة الخصم',supplier_bill_details.Normal_Increase as 'الزيادة العادية',supplier_bill_details.Categorical_Increase as 'الزيادة القطعية',supplier_bill_details.Purchasing_Price as 'سعر الشراء',import_storage_return_details.Total_Meters 'متر/قطعة',supplier_bill_details.Price_Type,supplier_bill_details.BillData_ID FROM supplier_bill INNER JOIN supplier_bill_details ON supplier_bill_details.Bill_ID = supplier_bill.Bill_ID INNER JOIN storage_import_permission ON supplier_bill.StorageImportPermission_ID = storage_import_permission.StorageImportPermission_ID INNER JOIN import_storage_return ON import_storage_return.StorageImportPermission_ID = storage_import_permission.StorageImportPermission_ID INNER JOIN import_storage_return_supplier ON import_storage_return_supplier.ImportStorageReturn_ID = import_storage_return.ImportStorageReturn_ID INNER JOIN import_storage_return_details ON import_storage_return_details.ImportStorageReturnSupplier_ID = import_storage_return_supplier.ImportStorageReturnSupplier_ID INNER JOIN data ON data.Data_ID = import_storage_return_details.Data_ID and import_storage_return_details.Data_ID=supplier_bill_details.Data_ID INNER JOIN type ON type.Type_ID = data.Type_ID INNER JOIN product ON product.Product_ID = data.Product_ID INNER JOIN factory ON data.Factory_ID = factory.Factory_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID LEFT JOIN color ON color.Color_ID = data.Color_ID LEFT JOIN size ON size.Size_ID = data.Size_ID LEFT JOIN sort ON sort.Sort_ID = data.Sort_ID where supplier_bill.StorageImportPermission_ID=" + storageImportPermissionId + " and import_storage_return_supplier.ImportStorageReturnSupplier_ID=" + comSupPerm.SelectedValue.ToString() + " and supplier_bill.Supplier_Permission_Number=" + comSupPerm.Text;
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
                        gridView1.SetRowCellValue(rowHandl, gridView1.Columns["نسبة الخصم"], dr["نسبة الخصم"].ToString());
                        gridView1.SetRowCellValue(rowHandl, gridView1.Columns["الزيادة العادية"], dr["الزيادة العادية"].ToString());
                        gridView1.SetRowCellValue(rowHandl, gridView1.Columns["الزيادة القطعية"], dr["الزيادة القطعية"].ToString());
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
                string q = "SELECT import_storage_return_details.Data_ID,data.Code as 'الكود',type.Type_Name as 'النوع',concat(product.Product_Name,' ',COALESCE(color.Color_Name,''),' ',data.Description,' ',groupo.Group_Name,' ',factory.Factory_Name,' ',COALESCE(size.Size_Value,''),' ',COALESCE(sort.Sort_Value,'')) as 'الاسم',purchasing_price.Price as 'السعر',purchasing_price.Last_Price as 'السعر بالزيادة',purchasing_price.Purchasing_Discount as 'نسبة الخصم',purchasing_price.Normal_Increase as 'الزيادة العادية',purchasing_price.Categorical_Increase as 'الزيادة القطعية',purchasing_price.Purchasing_Price as 'سعر الشراء',import_storage_return_details.Total_Meters 'متر/قطعة',purchasing_price.Price_Type,'BillData_ID','ImportStorageReturnDetails_ID' FROM import_storage_return inner join import_storage_return_supplier on import_storage_return.ImportStorageReturn_ID=import_storage_return_supplier.ImportStorageReturn_ID INNER JOIN import_storage_return_details ON import_storage_return_details.ImportStorageReturnSupplier_ID = import_storage_return_supplier.ImportStorageReturnSupplier_ID INNER JOIN data ON data.Data_ID = import_storage_return_details.Data_ID INNER JOIN type ON type.Type_ID = data.Type_ID INNER JOIN product ON product.Product_ID = data.Product_ID INNER JOIN factory ON data.Factory_ID = factory.Factory_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID LEFT JOIN color ON color.Color_ID = data.Color_ID LEFT JOIN size ON size.Size_ID = data.Size_ID LEFT JOIN sort ON sort.Sort_ID = data.Sort_ID inner join purchasing_price on purchasing_price.Data_ID=import_storage_return_details.Data_ID where import_storage_return.ImportStorageReturn_ID=" + 0;
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
                        gridView1.SetRowCellValue(rowHandl, gridView1.Columns["نسبة الخصم"], dr["نسبة الخصم"].ToString());
                        gridView1.SetRowCellValue(rowHandl, gridView1.Columns["الزيادة العادية"], dr["الزيادة العادية"].ToString());
                        gridView1.SetRowCellValue(rowHandl, gridView1.Columns["الزيادة القطعية"], dr["الزيادة القطعية"].ToString());
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
            string q = "SELECT supplier_return_bill_details.ReturnBillDetails_ID,supplier_return_bill_details.Data_ID,data.Code as 'الكود',type.Type_Name as 'النوع',concat(product.Product_Name,' ',COALESCE(color.Color_Name,''),' ',data.Description,' ',groupo.Group_Name,' ',factory.Factory_Name,' ',COALESCE(size.Size_Value,''),' ',COALESCE(sort.Sort_Value,'')) as 'الاسم',supplier_return_bill_details.Price_Type as 'نوع السعر',supplier_return_bill_details.Price as 'السعر',supplier_return_bill_details.Last_Price as 'السعر بالزيادة',supplier_return_bill_details.Purchasing_Discount as 'نسبة الخصم',supplier_return_bill_details.Normal_Increase as 'الزيادة العادية',supplier_return_bill_details.Categorical_Increase as 'الزيادة القطعية',supplier_return_bill_details.Purchasing_Price as 'سعر الشراء',supplier_return_bill_details.Total_Meters 'متر/قطعة',supplier_return_bill.Value_Additive_Tax as 'ضريبة القيمة المضافة',supplier_return_bill_details.BillData_ID,supplier_return_bill_details.ImportStorageReturnDetails_ID FROM supplier_return_bill INNER JOIN supplier_return_bill_details ON supplier_return_bill.ReturnBill_ID = supplier_return_bill_details.ReturnBill_ID INNER JOIN data ON data.Data_ID = supplier_return_bill_details.Data_ID INNER JOIN type ON type.Type_ID = data.Type_ID INNER JOIN product ON product.Product_ID = data.Product_ID INNER JOIN factory ON data.Factory_ID = factory.Factory_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID LEFT JOIN color ON color.Color_ID = data.Color_ID LEFT JOIN size ON size.Size_ID = data.Size_ID LEFT JOIN sort ON sort.Sort_ID = data.Sort_ID where supplier_return_bill.ReturnBill_ID=" + selrow[0].ToString();
            MySqlDataAdapter da = new MySqlDataAdapter(q, dbconnection);
            DataTable dt = new DataTable();
            da.Fill(dt);
            gridControl2.DataSource = dt;
            gridView2.Columns[0].Visible = false;
            gridView2.Columns[1].Visible = false;
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

        public void DecreaseSupplierAccount(double purchasePrice)
        {
            double totalSafy = purchasePrice * Convert.ToDouble(row1["متر/قطعة"].ToString());
            string query = "select Money from supplier_rest_money where Supplier_ID=" + comSupplier.SelectedValue.ToString();
            MySqlCommand com = new MySqlCommand(query, dbconnection);
            if (com.ExecuteScalar() != null)
            {
                double restMoney = Convert.ToDouble(com.ExecuteScalar());
                query = "update supplier_rest_money set Money=" + (restMoney - totalSafy) + " where Supplier_ID=" + comSupplier.SelectedValue.ToString();
                com = new MySqlCommand(query, dbconnection);
                com.ExecuteNonQuery();
            }
        }

        public void IncreaseSupplierAccount(DataRow row2)
        {
            double totalSafy = Convert.ToDouble(row2["سعر الشراء"].ToString()) * Convert.ToDouble(row2["متر/قطعة"].ToString());
            string query = "select Money from supplier_rest_money where Supplier_ID=" + comSupplier.SelectedValue.ToString();
            MySqlCommand com = new MySqlCommand(query, dbconnection);
            if (com.ExecuteScalar() != null)
            {
                double restMoney = Convert.ToDouble(com.ExecuteScalar());
                query = "update supplier_rest_money set Money=" + (restMoney + totalSafy) + " where Supplier_ID=" + comSupplier.SelectedValue.ToString();
                com = new MySqlCommand(query, dbconnection);
                com.ExecuteNonQuery();
            }
        }
    }
}

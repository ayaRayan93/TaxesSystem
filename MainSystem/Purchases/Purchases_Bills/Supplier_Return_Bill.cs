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
    public partial class Supplier_Return_Bill : Form
    {
        MySqlConnection dbconnection, dbconnection2;
        bool loaded = false;
        DataRow row1 = null;
        int rowHandle = -1;
        int billId = 0;

        public Supplier_Return_Bill()
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
                string query = "select * from supplier";
                MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                DataTable dt = new DataTable();
                da.Fill(dt);
                comSupplier.DataSource = dt;
                comSupplier.DisplayMember = dt.Columns["Supplier_Name"].ToString();
                comSupplier.ValueMember = dt.Columns["Supplier_ID"].ToString();
                comSupplier.Text = "";
                loaded = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }

        private void comSupplier_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (loaded)
                {
                    txtBillNumber.Text = "";
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
                if (txtBillNumber.Text != "" && comSupplier.SelectedValue != null)
                {
                    int billNum = 0;
                    if (int.TryParse(txtBillNumber.Text, out billNum))
                    {
                        dbconnection.Open();
                        newReturnBill();
                        displayPermissionDetails(comSupplier.SelectedValue.ToString(), billNum);
                        displayReturnPermissionDetails("0", 0);

                        string query = "select Bill_ID from supplier_bill where Supplier_ID=" + comSupplier.SelectedValue.ToString() + " and supplier_bill.Bill_No=" + billNum;
                        MySqlCommand com = new MySqlCommand(query, dbconnection);
                        if (com.ExecuteScalar() != null)
                        {
                            billId = (int)com.ExecuteScalar();
                        }
                        else
                        {
                            billId = 0;
                        }
                    }
                }
                else
                {
                    newReturnBill();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
            dbconnection2.Close();
        }

        private void newReturnBill_Click(object sender, EventArgs e)
        {
            try
            {
                dbconnection.Open();
                txtBillNumber.Text = "";
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
                txtDiscount.Text = row1["خصم الشراء"].ToString();
                txtCategoricalIncrease.Text = row1["الزيادة القطعية"].ToString();
                txtTotalMeter.Text = row1["متر/قطعة"].ToString();
                if (txtCategoricalIncrease.Text == "" && txtNormalIncrease.Text == "")
                {
                    txtCategoricalIncrease.Text = txtNormalIncrease.Text = "0.00";
                }
                if (row1["خصم الشراء"].ToString() != "")
                {
                    label7.Text = "خصم الشراء";
                    txtNormalIncrease.Visible = true;
                    txtCategoricalIncrease.Visible = true;
                    label8.Visible = true;
                    label6.Visible = true;
                }
                else if (row1["نسبة الشراء"].ToString() != "")
                {
                    label7.Text = "نسبة الشراء";
                    txtNormalIncrease.Visible = false;
                    txtCategoricalIncrease.Visible = false;
                    label8.Visible = false;
                    label6.Visible = false;
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
                                double price, PurchaseDiscount, NormalIncrease, Categorical_Increase, tax, quantity;
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
                                 double.TryParse(txtTax.Text, out tax))
                                {
                                    if (quantity <= Convert.ToDouble(row1["متر/قطعة"].ToString()))
                                    {
                                        dbconnection.Open();
                                        //displayReturnPermissionDetails("0", 0);
                                        gridView2.AddNewRow();
                                        int rowHandl = gridView2.GetRowHandle(gridView2.DataRowCount);
                                        if (gridView2.IsNewItemRow(rowHandl))
                                        {
                                            gridView2.SetRowCellValue(rowHandl, gridView2.Columns["Data_ID"], row1["Data_ID"].ToString());
                                            gridView2.SetRowCellValue(rowHandl, gridView2.Columns["الكود"], row1["الكود"].ToString());
                                            gridView2.SetRowCellValue(rowHandl, gridView2.Columns["الاسم"], row1["الاسم"].ToString());
                                            gridView2.SetRowCellValue(rowHandl, gridView2.Columns["السعر"], price);
                                            if (row1["خصم الشراء"].ToString() != "")
                                            {
                                                gridView2.SetRowCellValue(rowHandl, gridView2.Columns["خصم الشراء"], PurchaseDiscount);
                                            }
                                            else if (row1["نسبة الشراء"].ToString() != "")
                                            {
                                                gridView2.SetRowCellValue(rowHandl, gridView2.Columns["نسبة الشراء"], PurchaseDiscount);
                                            }
                                            gridView2.SetRowCellValue(rowHandl, gridView2.Columns["الزيادة العادية"], NormalIncrease);
                                            gridView2.SetRowCellValue(rowHandl, gridView2.Columns["الزيادة القطعية"], Categorical_Increase);
                                            gridView2.SetRowCellValue(rowHandl, gridView2.Columns["ضريبة القيمة المضافة"], tax);
                                            gridView2.SetRowCellValue(rowHandl, gridView2.Columns["سعر الشراء"], txtPurchasePrice.Text);
                                            gridView2.SetRowCellValue(rowHandl, gridView2.Columns["متر/قطعة"], quantity);
                                            gridView2.SetRowCellValue(rowHandl, gridView2.Columns["BillData_ID"], row1["BillData_ID"].ToString());

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

                                            row1 = null;
                                            labelTotalB.Text = totalB.ToString();
                                            labelTotalA.Text = totalA.ToString();
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
                displayReturnPermissionDetails("0", 0);
                for (int i = 0; i < gridView1.RowCount; i++)
                {
                    if (Convert.ToDouble(gridView1.GetRowCellDisplayText(i, "متر/قطعة")) > 0)
                    {
                        dbconnection.Open();
                        gridView2.AddNewRow();
                        int rowHandl = gridView2.GetRowHandle(gridView2.DataRowCount);
                        if (gridView2.IsNewItemRow(rowHandl))
                        {
                            gridView2.SetRowCellValue(rowHandl, gridView2.Columns["Data_ID"], gridView1.GetRowCellDisplayText(i, "Data_ID"));
                            gridView2.SetRowCellValue(rowHandl, gridView2.Columns["الكود"], gridView1.GetRowCellDisplayText(i, "الكود"));
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
                            gridView2.SetRowCellValue(rowHandl, gridView2.Columns["ضريبة القيمة المضافة"], gridView1.GetRowCellDisplayText(i, "ضريبة القيمة المضافة"));
                            gridView2.SetRowCellValue(rowHandl, gridView2.Columns["سعر الشراء"], gridView1.GetRowCellDisplayText(i, "سعر الشراء"));
                            gridView2.SetRowCellValue(rowHandl, gridView2.Columns["متر/قطعة"], gridView1.GetRowCellDisplayText(i, "متر/قطعة"));
                            gridView2.SetRowCellValue(rowHandl, gridView2.Columns["BillData_ID"], gridView1.GetRowCellDisplayText(i, "BillData_ID"));
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
                if (gridView2.RowCount > 0 && comSupplier.SelectedValue != null)
                {
                    dbconnection.Open();
                    //txtBillNumber_KeyDown
                    int BillNo = 1;
                    string query = "select Return_Bill_No from supplier_return_bill ORDER BY ReturnBill_ID DESC LIMIT 1 ";
                    MySqlCommand com = new MySqlCommand(query, dbconnection);
                    if (com.ExecuteScalar() != null)
                    {
                        BillNo = (int)com.ExecuteScalar();
                        BillNo++;
                    }

                    query = "insert into supplier_return_bill (Return_Bill_No,Supplier_ID,Supplier_Bill_No,Total_Price_BD,Total_Price_AD,Date,Bill_ID,Employee_ID) values (@Return_Bill_No,@Supplier_ID,@Supplier_Bill_No,@Total_Price_BD,@Total_Price_AD,@Date,@Bill_ID,@Employee_ID)";
                    com = new MySqlCommand(query, dbconnection);
                    com.Parameters.Add("@Return_Bill_No", MySqlDbType.Int16);
                    com.Parameters["@Return_Bill_No"].Value = BillNo;
                    com.Parameters.Add("@Supplier_ID", MySqlDbType.Int16);
                    com.Parameters["@Supplier_ID"].Value = comSupplier.SelectedValue;
                    com.Parameters.Add("@Supplier_Bill_No", MySqlDbType.Int16);
                    com.Parameters["@Supplier_Bill_No"].Value = billNum;
                    com.Parameters.Add("@Total_Price_BD", MySqlDbType.Decimal);
                    com.Parameters["@Total_Price_BD"].Value = Convert.ToDouble(labelTotalB.Text);
                    com.Parameters.Add("@Total_Price_AD", MySqlDbType.Decimal);
                    com.Parameters["@Total_Price_AD"].Value = Convert.ToDouble(labelTotalA.Text);
                    com.Parameters.Add("@Date", MySqlDbType.DateTime);
                    com.Parameters["@Date"].Value = DateTime.Now;
                    com.Parameters.Add("@Bill_ID", MySqlDbType.Int16);
                    com.Parameters["@Bill_ID"].Value = billId;
                    com.Parameters.Add("@Employee_ID", MySqlDbType.Int16);
                    com.Parameters["@Employee_ID"].Value = UserControl.EmpID;
                    com.ExecuteNonQuery();

                    query = "select ReturnBill_ID from supplier_return_bill ORDER BY ReturnBill_ID DESC LIMIT 1 ";
                    com = new MySqlCommand(query, dbconnection);
                    if (com.ExecuteScalar() != null)
                    {
                        returnBillId = (int)com.ExecuteScalar();
                    }

                    //////////////////add button/////////////////////////
                    for (int i = 0; i < gridView2.RowCount; i++)
                    {
                        query = "insert into supplier_return_bill_details (ReturnBill_ID,Data_ID,Price,Purchasing_Discount,Purchasing_Ratio,Normal_Increase,Categorical_Increase,Value_Additive_Tax,Purchasing_Price,Total_Meters,BillData_ID) values (@ReturnBill_ID,@Data_ID,@Price,@Purchasing_Discount,@Purchasing_Ratio,@Normal_Increase,@Categorical_Increase,@Value_Additive_Tax,@Purchasing_Price,@Total_Meters,@BillData_ID)";
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
                        }
                        else if (gridView2.GetRowCellDisplayText(i, gridView2.Columns["نسبة الشراء"]) != "")
                        {
                            com.Parameters.Add("@Purchasing_Discount", MySqlDbType.Decimal);
                            com.Parameters["@Purchasing_Discount"].Value = null;
                            com.Parameters.Add("@Purchasing_Ratio", MySqlDbType.Decimal);
                            com.Parameters["@Purchasing_Ratio"].Value = gridView2.GetRowCellDisplayText(i, gridView2.Columns["نسبة الشراء"]);
                        }
                        com.Parameters.Add("@Normal_Increase", MySqlDbType.Decimal);
                        com.Parameters["@Normal_Increase"].Value = gridView2.GetRowCellDisplayText(i, gridView2.Columns["الزيادة العادية"]);
                        com.Parameters.Add("@Categorical_Increase", MySqlDbType.Decimal);
                        com.Parameters["@Categorical_Increase"].Value = gridView2.GetRowCellDisplayText(i, gridView2.Columns["الزيادة القطعية"]);
                        com.Parameters.Add("@Value_Additive_Tax", MySqlDbType.Decimal);
                        com.Parameters["@Value_Additive_Tax"].Value = gridView2.GetRowCellDisplayText(i, gridView2.Columns["ضريبة القيمة المضافة"]);
                        com.Parameters.Add("@Purchasing_Price", MySqlDbType.Decimal);
                        com.Parameters["@Purchasing_Price"].Value = gridView2.GetRowCellDisplayText(i, gridView2.Columns["سعر الشراء"]);
                        com.Parameters.Add("@Total_Meters", MySqlDbType.Decimal);
                        com.Parameters["@Total_Meters"].Value = gridView2.GetRowCellDisplayText(i, gridView2.Columns["متر/قطعة"]);
                        com.Parameters.Add("@BillData_ID", MySqlDbType.Int16);
                        com.Parameters["@BillData_ID"].Value = gridView2.GetRowCellDisplayText(i, gridView2.Columns["BillData_ID"]);
                        com.ExecuteNonQuery();
                    }

                    txtBillNumber.Text = "";
                    newReturnBill();
                }
                else
                {
                    MessageBox.Show("تاكد من رقم الفاتورة");
                    dbconnection.Close();
                    return;
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
                string qq = "select Return_Bill_Date ,Store_Name ,Supplier_Name ,Code ,Price ,Buy_Discount ,Normal_Increase ,Categorical_Increase ,Value_Additive_Tax ,Buy_Price ,Total_Meters  from return_bill_details where Return_Bill_ID in (" + str + ") ";
                MySqlDataAdapter da = new MySqlDataAdapter(qq, dbconnection);
                DataSet ds = new DataSet();
                da.Fill(ds);
                Form2 f = new Form2(ds);
                f.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }*/
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
            else
            {
                double NormalPercent = double.Parse(txtNormalIncrease.Text);
                double unNormalPercent = double.Parse(txtCategoricalIncrease.Text);
                double PurchasesPrice = (price + NormalPercent) - ((price + NormalPercent) * PurchasesPercent / 100.0);
                PurchasesPrice = PurchasesPrice + unNormalPercent;
                return PurchasesPrice;
            }
        }

        public void displayPermissionDetails(string supplierId, int billNum)
        {
            string q = "SELECT supplier_bill_details.Data_ID,data.Code as 'الكود',concat(product.Product_Name,' - ',type.Type_Name,' - ',factory.Factory_Name,' - ',groupo.Group_Name,' ',COALESCE(color.Color_Name,''),' ',COALESCE(size.Size_Value,''),' ',COALESCE(sort.Sort_Value,'')) as 'الاسم',supplier_bill_details.Price as 'السعر',supplier_bill_details.Purchasing_Ratio as 'نسبة الشراء',supplier_bill_details.Purchasing_Discount as 'خصم الشراء',supplier_bill_details.Normal_Increase as 'الزيادة العادية',supplier_bill_details.Categorical_Increase as 'الزيادة القطعية',supplier_bill_details.Value_Additive_Tax as 'ضريبة القيمة المضافة',supplier_bill_details.Purchasing_Price as 'سعر الشراء',supplier_bill_details.Total_Meters 'متر/قطعة',supplier_bill_details.BillData_ID FROM supplier_bill INNER JOIN supplier_bill_details ON supplier_bill_details.Bill_ID = supplier_bill.Bill_ID INNER JOIN data ON data.Data_ID = supplier_bill_details.Data_ID INNER JOIN type ON type.Type_ID = data.Type_ID INNER JOIN product ON product.Product_ID = data.Product_ID INNER JOIN factory ON data.Factory_ID = factory.Factory_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID LEFT JOIN color ON color.Color_ID = data.Color_ID LEFT JOIN size ON size.Size_ID = data.Size_ID LEFT JOIN sort ON sort.Sort_ID = data.Sort_ID where supplier_bill.Supplier_ID=" + 0 + " and supplier_bill.Bill_No=" + 0;
            MySqlDataAdapter da = new MySqlDataAdapter(q, dbconnection);
            DataTable dt = new DataTable();
            da.Fill(dt);
            gridControl1.DataSource = dt;

            q = "SELECT supplier_bill_details.Data_ID,data.Code as 'الكود',concat(product.Product_Name,' - ',type.Type_Name,' - ',factory.Factory_Name,' - ',groupo.Group_Name,' ',COALESCE(color.Color_Name,''),' ',COALESCE(size.Size_Value,''),' ',COALESCE(sort.Sort_Value,'')) as 'الاسم',supplier_bill_details.Price as 'السعر',supplier_bill_details.Purchasing_Ratio as 'نسبة الشراء',supplier_bill_details.Purchasing_Discount as 'خصم الشراء',supplier_bill_details.Normal_Increase as 'الزيادة العادية',supplier_bill_details.Categorical_Increase as 'الزيادة القطعية',supplier_bill_details.Value_Additive_Tax as 'ضريبة القيمة المضافة',supplier_bill_details.Purchasing_Price as 'سعر الشراء',supplier_bill_details.Total_Meters 'متر/قطعة',supplier_bill_details.BillData_ID FROM supplier_bill INNER JOIN supplier_bill_details ON supplier_bill_details.Bill_ID = supplier_bill.Bill_ID INNER JOIN data ON data.Data_ID = supplier_bill_details.Data_ID INNER JOIN type ON type.Type_ID = data.Type_ID INNER JOIN product ON product.Product_ID = data.Product_ID INNER JOIN factory ON data.Factory_ID = factory.Factory_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID LEFT JOIN color ON color.Color_ID = data.Color_ID LEFT JOIN size ON size.Size_ID = data.Size_ID LEFT JOIN sort ON sort.Sort_ID = data.Sort_ID where supplier_bill.Supplier_ID=" + supplierId + " and supplier_bill.Bill_No=" + billNum;
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
                    q = "SELECT supplier_return_bill_details.Total_Meters as 'متر/قطعة' FROM supplier_return_bill INNER JOIN supplier_return_bill_details ON supplier_return_bill_details.ReturnBill_ID = supplier_return_bill.ReturnBill_ID  where supplier_return_bill.Supplier_ID=" + comSupplier.SelectedValue.ToString() + " and supplier_return_bill.Return_Bill_No=" + billNum + " and supplier_return_bill_details.Data_ID=" + dr["Data_ID"].ToString();
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

            gridView1.Columns[0].Visible = false;
            gridView1.Columns["BillData_ID"].Visible = false;

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

        public void displayReturnPermissionDetails(string supplierId, int billNum)
        {
            string q = "SELECT supplier_bill_details.Data_ID,data.Code as 'الكود',concat(product.Product_Name,' - ',type.Type_Name,' - ',factory.Factory_Name,' - ',groupo.Group_Name,' ',COALESCE(color.Color_Name,''),' ',COALESCE(size.Size_Value,''),' ',COALESCE(sort.Sort_Value,'')) as 'الاسم',supplier_bill_details.Price as 'السعر',supplier_bill_details.Purchasing_Ratio as 'نسبة الشراء',supplier_bill_details.Purchasing_Discount as 'خصم الشراء',supplier_bill_details.Normal_Increase as 'الزيادة العادية',supplier_bill_details.Categorical_Increase as 'الزيادة القطعية',supplier_bill_details.Value_Additive_Tax as 'ضريبة القيمة المضافة',supplier_bill_details.Purchasing_Price as 'سعر الشراء',supplier_bill_details.Total_Meters 'متر/قطعة',supplier_bill_details.BillData_ID FROM supplier_bill INNER JOIN supplier_bill_details ON supplier_bill_details.Bill_ID = supplier_bill.Bill_ID INNER JOIN data ON data.Data_ID = supplier_bill_details.Data_ID INNER JOIN type ON type.Type_ID = data.Type_ID INNER JOIN product ON product.Product_ID = data.Product_ID INNER JOIN factory ON data.Factory_ID = factory.Factory_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID LEFT JOIN color ON color.Color_ID = data.Color_ID LEFT JOIN size ON size.Size_ID = data.Size_ID LEFT JOIN sort ON sort.Sort_ID = data.Sort_ID where supplier_bill.Supplier_ID=" + supplierId + " and supplier_bill.Bill_No=" + billNum;
            MySqlDataAdapter da = new MySqlDataAdapter(q, dbconnection);
            DataTable dt = new DataTable();
            da.Fill(dt);
            gridControl2.DataSource = dt;
            gridView2.Columns[0].Visible = false;
            gridView2.Columns["BillData_ID"].Visible = false;

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
    }
}

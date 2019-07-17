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
    public partial class PurchaseBill_Update : Form
    {
        MySqlConnection conn;
        PurchaseBill_Report purchaseBillReport = null;
        bool loaded = false;
        DataRow row1 = null;
        DataRow sellRow = null;
        int rowHandle = 0;

        public PurchaseBill_Update(PurchaseBill_Report PurchaseBillReport, DataRow sellrow)
        {
            InitializeComponent();
            conn = new MySqlConnection(connection.connectionString);
            purchaseBillReport = PurchaseBillReport;
            sellRow = sellrow;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                search();
                loaded = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            conn.Close();
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
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txtBox_TextChanged2(object sender, EventArgs e)
        {
            try
            {
                if (loaded & row1 != null)
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

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (row1 != null)
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

                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns["السعر"], txtPrice.Text);
                            if (row1["خصم الشراء"].ToString() != "")
                            {
                                gridView1.SetRowCellValue(rowHandle, gridView1.Columns["خصم الشراء"], BuyDiscount);
                                gridView1.SetRowCellValue(rowHandle, gridView1.Columns["الزيادة العادية"], NormalIncrease);
                                gridView1.SetRowCellValue(rowHandle, gridView1.Columns["الزيادة القطعية"], CategoricalIncrease);
                            }
                            else if (row1["نسبة الشراء"].ToString() != "")
                            {
                                gridView1.SetRowCellValue(rowHandle, gridView1.Columns["نسبة الشراء"], BuyDiscount);
                                gridView1.SetRowCellValue(rowHandle, gridView1.Columns["الزيادة العادية"], "");
                                gridView1.SetRowCellValue(rowHandle, gridView1.Columns["الزيادة القطعية"], "");
                            }
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns["ضريبة القيمة المضافة"], VAT);
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns["سعر الشراء"], purchasePrice);
                            
                            double totalB = 0;
                            double totalA = 0;
                            for (int i = 0; i < gridView1.RowCount; i++)
                            {
                                totalB += Convert.ToDouble(gridView1.GetRowCellDisplayText(i, "السعر")) * Convert.ToDouble(gridView1.GetRowCellDisplayText(i, "متر/قطعة"));
                                totalA += Convert.ToDouble(gridView1.GetRowCellDisplayText(i, "سعر الشراء")) * Convert.ToDouble(gridView1.GetRowCellDisplayText(i, "متر/قطعة"));
                            }

                            Clear();
                            row1 = null;
                            labelTotalB.Text = totalB.ToString();
                            labelTotalA.Text = totalA.ToString();
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
                    MessageBox.Show("يجب اختيار عنصر");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            conn.Close();
        }
        
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                string query = "update supplier_bill set Total_Price_B=@Total_Price_B,Total_Price_A=@Total_Price_A where Bill_ID=" + sellRow[0].ToString();
                MySqlCommand com = new MySqlCommand(query, conn);
                com.Parameters.Add("@Total_Price_B", MySqlDbType.Decimal);
                com.Parameters["@Total_Price_B"].Value = labelTotalB.Text;
                com.Parameters.Add("@Total_Price_A", MySqlDbType.Decimal);
                com.Parameters["@Total_Price_A"].Value = labelTotalA.Text;
                com.ExecuteNonQuery();

                for (int i = 0; i < gridView1.RowCount; i++)
                {
                    DataRow row3 = gridView1.GetDataRow(i);
                    //,Purchasing_Ratio=@Purchasing_Ratio
                    query = "update supplier_bill_details set Price=@Price,Purchasing_Discount=@Purchasing_Discount,Normal_Increase=@Normal_Increase,Categorical_Increase=@Categorical_Increase,Value_Additive_Tax=@Value_Additive_Tax,Purchasing_Price=@Purchasing_Price where BillData_ID=" + row3[0].ToString();
                    com = new MySqlCommand(query, conn);
                    com.Parameters.Add("@Price", MySqlDbType.Decimal);
                    com.Parameters["@Price"].Value = row3["السعر"].ToString();
                    if (row3["خصم الشراء"].ToString() != "")
                    {
                        com.Parameters.Add("@Purchasing_Discount", MySqlDbType.Decimal);
                        com.Parameters["@Purchasing_Discount"].Value = row3["خصم الشراء"].ToString();
                        //com.Parameters.Add("@Purchasing_Ratio", MySqlDbType.Decimal);
                        //com.Parameters["@Purchasing_Ratio"].Value = null;
                        com.Parameters.Add("@Normal_Increase", MySqlDbType.Decimal);
                        com.Parameters["@Normal_Increase"].Value = row3["الزيادة العادية"].ToString();
                        com.Parameters.Add("@Categorical_Increase", MySqlDbType.Decimal);
                        com.Parameters["@Categorical_Increase"].Value = row3["الزيادة القطعية"].ToString();
                    }
                    else if (row3["نسبة الشراء"].ToString() != "")
                    {
                        com.Parameters.Add("@Purchasing_Discount", MySqlDbType.Decimal);
                        com.Parameters["@Purchasing_Discount"].Value = row3["خصم الشراء"].ToString();
                        //com.Parameters.Add("@Purchasing_Ratio", MySqlDbType.Decimal);
                        //com.Parameters["@Purchasing_Ratio"].Value = row3["نسبة الشراء"].ToString();
                        com.Parameters.Add("@Normal_Increase", MySqlDbType.Decimal);
                        com.Parameters["@Normal_Increase"].Value = null;
                        com.Parameters.Add("@Categorical_Increase", MySqlDbType.Decimal);
                        com.Parameters["@Categorical_Increase"].Value = null;
                    }
                    com.Parameters.Add("@Value_Additive_Tax", MySqlDbType.Decimal);
                    com.Parameters["@Value_Additive_Tax"].Value = row3["ضريبة القيمة المضافة"].ToString();
                    com.Parameters.Add("@Purchasing_Price", MySqlDbType.Decimal);
                    com.Parameters["@Purchasing_Price"].Value = row3["سعر الشراء"].ToString();
                    com.ExecuteNonQuery();
                }
                try
                {
                    purchaseBillReport.search();
                }
                catch
                { }
                conn.Close();
                this.Hide();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            conn.Close();
        }

        //function
        void search()
        {
            DataSet sourceDataSet = new DataSet();
            //,supplier_bill_details.Purchasing_Ratio as 'نسبة الشراء'
            MySqlDataAdapter adapterDetails = new MySqlDataAdapter("SELECT supplier_bill_details.BillData_ID,data.Code as 'الكود',type.Type_Name as 'النوع',concat(product.Product_Name,' ',COALESCE(color.Color_Name,''),' ',data.Description,' ',groupo.Group_Name,' ',factory.Factory_Name,' ',COALESCE(size.Size_Value,''),' ',COALESCE(sort.Sort_Value,'')) as 'الاسم',supplier_bill_details.Price as 'السعر',supplier_bill_details.Purchasing_Discount as 'خصم الشراء',supplier_bill_details.Normal_Increase as 'الزيادة العادية',supplier_bill_details.Categorical_Increase as 'الزيادة القطعية',supplier_bill_details.Value_Additive_Tax as 'ضريبة القيمة المضافة',supplier_bill_details.Purchasing_Price as 'سعر الشراء',supplier_bill_details.Total_Meters as 'متر/قطعة' FROM supplier_bill INNER JOIN supplier_bill_details ON supplier_bill_details.Bill_ID = supplier_bill.Bill_ID INNER JOIN store ON store.Store_ID = supplier_bill.Store_ID INNER JOIN supplier ON supplier.Supplier_ID = supplier_bill.Supplier_ID INNER JOIN data ON supplier_bill_details.Data_ID = data.Data_ID INNER JOIN type ON type.Type_ID = data.Type_ID INNER JOIN product ON product.Product_ID = data.Product_ID INNER JOIN factory ON data.Factory_ID = factory.Factory_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID LEFT JOIN color ON color.Color_ID = data.Color_ID LEFT JOIN size ON size.Size_ID = data.Size_ID LEFT JOIN sort ON sort.Sort_ID = data.Sort_ID where supplier_bill.Bill_ID =" + sellRow[0].ToString() + " order by SUBSTR(data.Code,1,16),color.Color_Name,data.Description,data.Sort_ID", conn);
            adapterDetails.Fill(sourceDataSet);
            gridControl1.DataSource = sourceDataSet.Tables[0];
            gridView1.Columns[0].Visible = false;
            for (int i = 0; i < gridView1.Columns.Count; i++)
            {
                gridView1.Columns[i].Width = 120;
            }
            gridView1.Columns["الاسم"].Width = 250;
            gridView1.Columns["الكود"].Width = 170;
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
            labelTotalB.Text = totalB.ToString();
            labelTotalA.Text = totalA.ToString();
        }
        public double calPurchasesPrice()
        {
            double price = double.Parse(txtPrice.Text);
            double PurchasesPercent = double.Parse(txtDiscount.Text);
            if (row1["نسبة الشراء"].ToString() != "")
            {
                return price + (price * PurchasesPercent / 100.0);
            }
            else if (row1["خصم الشراء"].ToString() != "")
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
        //clear fields
        public void Clear()
        {
            txtCode.Text = txtPrice.Text = txtTotalMeter.Text = txtPurchasePrice.Text = "";
            txtTax.Text = txtCategoricalIncrease.Text = txtDiscount.Text = txtNormalIncrease.Text = "0";
        }
    }
}

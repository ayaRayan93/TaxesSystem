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

namespace TaxesSystem
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
                txtLastPrice.Text = row1["السعر بالزيادة"].ToString();
                txtPurchasePrice.Text = row1["سعر الشراء"].ToString();
                txtNormalIncrease.Text = row1["الزيادة العادية"].ToString();
                txtCategoricalIncrease.Text = row1["الزيادة القطعية"].ToString();
                txtTotalMeter.Text = row1["متر/قطعة"].ToString();
                
                if (txtCategoricalIncrease.Text == "" && txtNormalIncrease.Text == "")
                {
                    txtCategoricalIncrease.Text = txtNormalIncrease.Text = "0";
                }
                /*if (row1["نوع السعر"].ToString() != "")
                {
                    txtDiscount.Text = row1["نسبة الخصم"].ToString();
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
                }*/
                if (txtDiscount.Text == "")
                {
                    txtDiscount.Text = "0";
                }
                else
                {
                    txtDiscount.Text = row1["نسبة الخصم"].ToString();
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
                            txtPurchasePrice.Text = (calPurchasesPrice() /*+ VAT*/) + "";
                            txtLastPrice.Text = (lastPrice(calPurchasesPrice()) ) + "";
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
                            if (row1["نوع السعر"].ToString() == "لستة")
                            {
                                gridView1.SetRowCellValue(rowHandle, gridView1.Columns["نسبة الخصم"], BuyDiscount);
                                gridView1.SetRowCellValue(rowHandle, gridView1.Columns["الزيادة العادية"], NormalIncrease);
                                gridView1.SetRowCellValue(rowHandle, gridView1.Columns["الزيادة القطعية"], CategoricalIncrease);
                            }
                            else if (row1["نوع السعر"].ToString() == "قطعى")
                            {
                                gridView1.SetRowCellValue(rowHandle, gridView1.Columns["نسبة الخصم"], BuyDiscount);
                                gridView1.SetRowCellValue(rowHandle, gridView1.Columns["الزيادة العادية"], NormalIncrease);
                                gridView1.SetRowCellValue(rowHandle, gridView1.Columns["الزيادة القطعية"], CategoricalIncrease);
                            }
                            //gridView1.SetRowCellValue(rowHandle, gridView1.Columns["ضريبة القيمة المضافة"], VAT);
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns["السعر بالزيادة"], txtLastPrice.Text);
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns["سعر الشراء"], purchasePrice);

                            double totalAllB = 0;
                            double totalAllA = 0;
                            double totalAllDiscount = 0;
                            double totalAllCatInc = 0;
                            for (int i = 0; i < gridView1.RowCount; i++)
                            {
                                double totalB = 0;
                                double totalA = 0;
                                double totalNormInc = 0;
                                double totalDiscount = 0;
                                double totalCatInc = 0;
                                if (gridView1.GetRowCellDisplayText(i, "الزيادة العادية") != "")
                                {
                                    totalNormInc = Convert.ToDouble(gridView1.GetRowCellDisplayText(i, "الزيادة العادية"));
                                }
                                else
                                {
                                    totalNormInc = 0;
                                }
                                if (gridView1.GetRowCellDisplayText(i, "الزيادة القطعية") != "")
                                {
                                    totalCatInc = Convert.ToDouble(gridView1.GetRowCellDisplayText(i, "الزيادة القطعية"));
                                }
                                else
                                {
                                    totalCatInc = 0;
                                }
                                totalB = (Convert.ToDouble(gridView1.GetRowCellDisplayText(i, "السعر")) + totalNormInc) * Convert.ToDouble(gridView1.GetRowCellDisplayText(i, "متر/قطعة"));
                                totalDiscount = totalB * (Convert.ToDouble(gridView1.GetRowCellDisplayText(i, "نسبة الخصم")) / 100);
                                totalCatInc = Convert.ToDouble(gridView1.GetRowCellDisplayText(i, "الزيادة القطعية")) * Convert.ToDouble(gridView1.GetRowCellDisplayText(i, "متر/قطعة"));
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
                string query = "update supplier_bill set Total_Price_B=@Total_Price_B,Total_Price_A=@Total_Price_A,Value_Additive_Tax=@Value_Additive_Tax where Bill_ID=" + sellRow[0].ToString();
                MySqlCommand com = new MySqlCommand(query, conn);
                com.Parameters.Add("@Total_Price_B", MySqlDbType.Decimal);
                com.Parameters["@Total_Price_B"].Value = labelTotalB.Text;
                com.Parameters.Add("@Total_Price_A", MySqlDbType.Decimal);
                com.Parameters["@Total_Price_A"].Value = labelTotalSafy.Text;
                com.Parameters.Add("@Value_Additive_Tax", MySqlDbType.Decimal);
                com.Parameters["@Value_Additive_Tax"].Value = txtAllTax.Text;
                com.ExecuteNonQuery();

                for (int i = 0; i < gridView1.RowCount; i++)
                {
                    DataRow row3 = gridView1.GetDataRow(i);
                    //,Purchasing_Ratio=@Purchasing_Ratio
                    query = "update supplier_bill_details set Price=@Price,Last_Price=@Last_Price,Purchasing_Discount=@Purchasing_Discount,Normal_Increase=@Normal_Increase,Categorical_Increase=@Categorical_Increase,Purchasing_Price=@Purchasing_Price where BillData_ID=" + row3[0].ToString();
                    com = new MySqlCommand(query, conn);
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
                    com.ExecuteNonQuery();
                }
                DecreaseSupplierAccount();
                IncreaseSupplierAccount();
                try
                {
                    purchaseBillReport.search(0);
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
            MySqlDataAdapter adapterDetails = new MySqlDataAdapter("SELECT supplier_bill_details.BillData_ID,data.Code as 'الكود',type.Type_Name as 'النوع',concat(product.Product_Name,' ',COALESCE(color.Color_Name,''),' ',data.Description,' ',groupo.Group_Name,' ',factory.Factory_Name,' ',COALESCE(size.Size_Value,''),' ',COALESCE(sort.Sort_Value,'')) as 'الاسم',supplier_bill_details.Price as 'السعر',supplier_bill_details.Purchasing_Discount as 'نسبة الخصم',supplier_bill_details.Normal_Increase as 'الزيادة العادية',supplier_bill_details.Categorical_Increase as 'الزيادة القطعية',supplier_bill_details.Last_Price AS 'السعر بالزيادة',supplier_bill_details.Purchasing_Price as 'سعر الشراء',supplier_bill_details.Total_Meters as 'متر/قطعة',supplier_bill.Value_Additive_Tax,supplier_bill_details.Price_Type as 'نوع السعر' FROM supplier_bill INNER JOIN supplier_bill_details ON supplier_bill_details.Bill_ID = supplier_bill.Bill_ID INNER JOIN store ON store.Store_ID = supplier_bill.Store_ID INNER JOIN supplier ON supplier.Supplier_ID = supplier_bill.Supplier_ID INNER JOIN data ON supplier_bill_details.Data_ID = data.Data_ID INNER JOIN type ON type.Type_ID = data.Type_ID INNER JOIN product ON product.Product_ID = data.Product_ID INNER JOIN factory ON data.Factory_ID = factory.Factory_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID LEFT JOIN color ON color.Color_ID = data.Color_ID LEFT JOIN size ON size.Size_ID = data.Size_ID LEFT JOIN sort ON sort.Sort_ID = data.Sort_ID where supplier_bill.Bill_ID =" + sellRow[0].ToString() + " order by SUBSTR(data.Code,1,16),color.Color_Name,data.Description,data.Sort_ID", conn);
            adapterDetails.Fill(sourceDataSet);
            gridControl1.DataSource = sourceDataSet.Tables[0];
            gridView1.Columns[0].Visible = false;
            gridView1.Columns["Value_Additive_Tax"].Visible = false;
            gridView1.Columns["نوع السعر"].Visible = false;
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
            
            double totalAllB = 0;
            double totalAllA = 0;
            double totalAllDiscount = 0;
            double totalAllCatInc = 0;
            for (int i = 0; i < gridView1.RowCount; i++)
            {
                double totalB = 0;
                double totalA = 0;
                double totalNormInc = 0;
                double totalDiscount = 0;
                double totalCatInc = 0;
                if (gridView1.GetRowCellDisplayText(i, "الزيادة العادية") != "")
                {
                    totalNormInc = Convert.ToDouble(gridView1.GetRowCellDisplayText(i, "الزيادة العادية"));
                }
                else
                {
                    totalNormInc = 0;
                }
                if (gridView1.GetRowCellDisplayText(i, "الزيادة القطعية") != "")
                {
                    totalCatInc = Convert.ToDouble(gridView1.GetRowCellDisplayText(i, "الزيادة القطعية"));
                }
                else
                {
                    totalCatInc = 0;
                }
                totalB = (Convert.ToDouble(gridView1.GetRowCellDisplayText(i, "السعر")) + totalNormInc) * Convert.ToDouble(gridView1.GetRowCellDisplayText(i, "متر/قطعة"));
                totalDiscount = totalB * (Convert.ToDouble(gridView1.GetRowCellDisplayText(i, "نسبة الخصم")) / 100);
                totalCatInc = Convert.ToDouble(gridView1.GetRowCellDisplayText(i, "الزيادة القطعية")) * Convert.ToDouble(gridView1.GetRowCellDisplayText(i, "متر/قطعة"));
                totalA = (totalB - totalDiscount) + totalCatInc;

                totalAllB += totalB;
                totalAllDiscount += totalDiscount;
                totalAllCatInc += totalCatInc;
                totalAllA += totalA;
            }
            txtAllTax.Text = gridView1.GetRowCellDisplayText(0, "Value_Additive_Tax").ToString();
            labelTotalB.Text = totalAllB.ToString("#.000");
            labelTotalDiscount.Text = totalAllDiscount.ToString("#.000");
            labelTotalCat.Text = totalAllCatInc.ToString("#.000");
            labelTotalA.Text = totalAllA.ToString("#.000");
            labelTotalSafy.Text = (Convert.ToDouble(labelTotalA.Text) + (Convert.ToDouble(labelTotalA.Text) * (Convert.ToDouble(txtAllTax.Text) / 100))).ToString("#.000");
        }
        public double calPurchasesPrice()
        {
            double addational = 0.0;
            double price = double.Parse(txtPrice.Text);

            double PurchasesPercent = double.Parse(txtDiscount.Text);
            if (row1["نوع السعر"].ToString() == "قطعى")
            {
                price += Convert.ToDouble(txtNormalIncrease.Text) + Convert.ToDouble(txtCategoricalIncrease.Text);
                return price - (price * PurchasesPercent / 100.0);
            }
            else if (row1["نوع السعر"].ToString() == "لستة")
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
            }*/
        }
        public double lastPrice(double purchasePrice)
        {
            double discount = double.Parse(txtDiscount.Text);
            double lastPrice = purchasePrice * 100 / (100 - discount);

            return lastPrice;
        }
        //clear fields
        public void Clear()
        {
            txtCode.Text = txtPrice.Text = txtLastPrice.Text = txtTotalMeter.Text = txtPurchasePrice.Text = "";
            //txtAllTax.Text = "0.00";
            txtTax.Text = txtCategoricalIncrease.Text = txtDiscount.Text = txtNormalIncrease.Text = "0";
            //labelTotalB.Text = labelTotalA.Text = txtPurchasePrice.Text /*= labelTotalVal.Text*/ = labelTotalDiscount.Text = labelTotalSafy.Text = "";
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
                            labelTotalSafy.Text = (Convert.ToDouble(labelTotalA.Text) + (Convert.ToDouble(labelTotalA.Text) * (Convert.ToDouble(txtAllTax.Text) / 100))).ToString();
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
            //double totalSafy = Convert.ToDouble(labelTotalSafy.Text);
            double totalSafy = Convert.ToDouble(sellRow["الاجمالى بعد"]);
            string query = "select Money from supplier_rest_money where Supplier_ID=" + sellRow["Supplier_ID"].ToString();
            MySqlCommand com = new MySqlCommand(query, conn);
            if (com.ExecuteScalar() != null)
            {
                double restMoney = Convert.ToDouble(com.ExecuteScalar());
                query = "update supplier_rest_money set Money=" + (restMoney - totalSafy) + " where Supplier_ID=" + sellRow["Supplier_ID"].ToString();
                com = new MySqlCommand(query, conn);
            }
            /*else
            {
                query = "insert into supplier_rest_money (Supplier_ID,Money) values (@Supplier_ID,@Money)";
                com = new MySqlCommand(query, conn);
                com.Parameters.Add("@Supplier_ID", MySqlDbType.Int16, 11).Value = comSupplier.SelectedValue;
                com.Parameters.Add("@Money", MySqlDbType.Decimal, 10).Value = -1 * totalSafy;
            }*/
            com.ExecuteNonQuery();
        }

        public void IncreaseSupplierAccount()
        {
            double totalSafy = Convert.ToDouble(labelTotalSafy.Text);
            string query = "select Money from supplier_rest_money where Supplier_ID=" + sellRow["Supplier_ID"].ToString();
            MySqlCommand com = new MySqlCommand(query, conn);
            if (com.ExecuteScalar() != null)
            {
                double restMoney = Convert.ToDouble(com.ExecuteScalar());
                query = "update supplier_rest_money set Money=" + (restMoney + totalSafy) + " where Supplier_ID=" + sellRow["Supplier_ID"].ToString();
                com = new MySqlCommand(query, conn);
            }
            /*else
            {
                query = "insert into supplier_rest_money (Supplier_ID,Money) values (@Supplier_ID,@Money)";
                com = new MySqlCommand(query, conn);
                com.Parameters.Add("@Supplier_ID", MySqlDbType.Int16, 11).Value = comSupplier.SelectedValue;
                com.Parameters.Add("@Money", MySqlDbType.Decimal, 10).Value = totalSafy;
            }*/
            com.ExecuteNonQuery();
        }
    }
}

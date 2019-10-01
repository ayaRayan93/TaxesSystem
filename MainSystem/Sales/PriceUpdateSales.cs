using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using MySql.Data.MySqlClient;
using DevExpress.XtraTab;

namespace MainSystem
{
    public partial class PriceUpdateSales : DevExpress.XtraEditors.XtraForm
    {
        int rowHandel = 0;
        DataRowView selRow;
        MySqlConnection dbconnection;
        bool priceFlag = true;
        bool discountFlag = true;

        public PriceUpdateSales(int rowhandel, DataRowView Selrow)
        {
            InitializeComponent();
            dbconnection = new MySqlConnection(connection.connectionString);
            rowHandel = rowhandel;
            selRow = Selrow;
        }

        private void PriceUpdateSales_Load(object sender, EventArgs e)
        {
            try
            {
                txtDiscount.Text = selRow["النسبة"].ToString();
                txtPrice.Text = selRow["بعد الخصم"].ToString();
                txtTotalPrice.Text = selRow["الاجمالى بعد"].ToString();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtDiscount.Text != "" && txtPrice.Text != "")
                {
                    double discount = 0;
                    double price = 0;
                    double totalPrice = 0;
                    if (double.TryParse(txtDiscount.Text, out discount) && double.TryParse(txtPrice.Text, out price) && double.TryParse(txtTotalPrice.Text, out totalPrice))
                    {
                        if (price <= Convert.ToDouble(selRow["السعر"].ToString()))
                        {
                            MainForm.objFormBillConfirm.refreshPriceView(rowHandel, discount, price, totalPrice);
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("تاكد من السعر");
                        }
                    }
                    else
                    {
                        MessageBox.Show("القيم يجب ان تكون عدد");
                    }
                }
                else
                {
                    MessageBox.Show("يجب ادخال البيانات كاملة");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }

        private void txtDiscount_KeyDown(object sender, KeyEventArgs e)
        {
            /*if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    double discount = 0;
                    if (double.TryParse(txtDiscount.Text, out discount))
                    {
                        double discountValue = Convert.ToDouble(selRow["السعر"].ToString()) * (discount / 100);
                        txtPrice.Text = (Convert.ToDouble(selRow["السعر"].ToString()) - discountValue).ToString();
                        txtTotalPrice.Text = (Convert.ToDouble(txtPrice.Text) * Convert.ToDouble(selRow["الكمية"].ToString())).ToString();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }*/
        }

        private void txtPrice_KeyDown(object sender, KeyEventArgs e)
        {
            /*if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    double price = 0;
                    if (double.TryParse(txtPrice.Text, out price))
                    {
                        double priceValue = Convert.ToDouble(selRow["السعر"].ToString()) - price;
                        txtDiscount.Text = ((priceValue / Convert.ToDouble(selRow["السعر"].ToString())) * 100).ToString("0.##");
                        txtTotalPrice.Text = (Convert.ToDouble(txtPrice.Text) * Convert.ToDouble(selRow["الكمية"].ToString())).ToString();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }*/
        }

        private void txtDiscount_TextChanged(object sender, EventArgs e)
        {
            if (discountFlag)
            {
                try
                {
                    double discount = 0;
                    if (double.TryParse(txtDiscount.Text, out discount))
                    {
                        priceFlag = false;
                        double discountValue = Convert.ToDouble(selRow["السعر"].ToString()) * (discount / 100);
                        txtPrice.Text = (Convert.ToDouble(selRow["السعر"].ToString()) - discountValue).ToString("0.##");
                        txtTotalPrice.Text = (Convert.ToDouble(txtPrice.Text) * Convert.ToDouble(selRow["الكمية"].ToString())).ToString();
                        priceFlag = true;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void txtPrice_TextChanged(object sender, EventArgs e)
        {
            if (priceFlag)
            {
                try
                {
                    double price = 0;
                    if (double.TryParse(txtPrice.Text, out price))
                    {
                        discountFlag = false;
                        double priceValue = Convert.ToDouble(selRow["السعر"].ToString()) - price;
                        txtDiscount.Text = ((priceValue / Convert.ToDouble(selRow["السعر"].ToString())) * 100).ToString("0.##");
                        txtTotalPrice.Text = (Convert.ToDouble(txtPrice.Text) * Convert.ToDouble(selRow["الكمية"].ToString())).ToString();
                        discountFlag = true;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}
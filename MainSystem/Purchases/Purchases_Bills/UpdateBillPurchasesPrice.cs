using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraTab;
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
    public partial class UpdateBillPurchasesPrice : Form
    {
        MySqlConnection dbconnection;
        DataRow rows = null;
        bool load = false;
        Supplier_Bill supplierBill = null;

        public UpdateBillPurchasesPrice(DataRow rows, Supplier_Bill SupplierBill)
        {
            try
            {
                InitializeComponent();
                this.rows = rows;
                dbconnection = new MySqlConnection(connection.connectionString);
                supplierBill = SupplierBill;
                
                //panContent.Controls["panContent"].Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void UpdateBillPurchasesPrice_Load(object sender, EventArgs e)
        {
            try
            {
                dbconnection.Open();
                
                setData(rows);
                load = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            dbconnection.Close();
        }
        //deign event
        private void chBoxAdditionalIncrease_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chBoxAdditionalIncrease.Checked)
                {
                    panelAdditional.Visible = true;
                }
                else
                {
                    panelAdditional.Visible = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void radioList_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                label14.Text = "خصم الشراء";
                txtNormal.Visible = true;
                txtUnNormal.Visible = true;
                label15.Visible = true;
                label16.Visible = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void radioQata3y_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                label14.Text = "نسبة الشراء";
                txtNormal.Visible = false;
                txtUnNormal.Visible = false;
                label15.Visible = false;
                label16.Visible = false;

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
                if (load)
                {
                    labPurchasesPrice.Text = calPurchasesPrice() + "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btnNewPlus_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtCode.Text != "")
                {
                    if (txtDes.Text != "" && txtPlus.Text != "")
                    {
                        if (radioNormal.Checked)
                        {
                            int n = dataGridView1.Rows.Add();
                            dataGridView1.Rows[n].Cells[0].Value = txtPlus.Text;
                            dataGridView1.Rows[n].Cells[1].Value = "عادية";
                            dataGridView1.Rows[n].Cells[2].Value = txtDes.Text;
                            labPurchasesPrice.Text = "" + calPurchasesPrice();
                        }
                        else if (radioQata3a.Checked)
                        {
                            int n = dataGridView1.Rows.Add();
                            dataGridView1.Rows[n].Cells[0].Value = txtPlus.Text;
                            dataGridView1.Rows[n].Cells[1].Value = "قطعية";
                            dataGridView1.Rows[n].Cells[2].Value = txtDes.Text;
                            labPurchasesPrice.Text = "" + calPurchasesPrice();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                DataGridViewRow row1 = dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex];
                if (row1 != null)
                {
                    dataGridView1.Rows.Remove(row1);
                    labPurchasesPrice.Text = calPurchasesPrice()+"";
                }
                else
                {
                    MessageBox.Show("you must select an item");
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        //function 
        
        public void setData(DataRow row1)
        {
            txtCode.Text = row1["الكود"].ToString();
            txtPrice.Text = row1["السعر"].ToString();
            string str = row1["نوع السعر"].ToString();
            if (str == "لستة")
            {
                txtPurchases.Text = row1["خصم الشراء"].ToString();
                txtNormal.Text = row1["الزيادة العادية"].ToString();
                txtUnNormal.Text = row1["الزيادة القطعية"].ToString();
                radioList.Checked = true;
            }
            else
            {
                txtPurchases.Text = row1["نسبة الشراء"].ToString();
                radioQata3y.Checked = true;
            }
            labPurchasesPrice.Text = row1["سعر الشراء"].ToString();
            if (row1["PurchasingPrice_ID"].ToString() != "")
            {
                string query = "select AdditionalValue,Type,Description from additional_increase_purchasingprice where PurchasingPrice_ID=" + row1["PurchasingPrice_ID"].ToString();
                MySqlCommand com = new MySqlCommand(query, dbconnection);
                MySqlDataReader dr = com.ExecuteReader();
                while (dr.Read())
                {
                    int n = dataGridView1.Rows.Add();
                    dataGridView1.Rows[n].Cells[0].Value = dr[0].ToString();
                    dataGridView1.Rows[n].Cells[1].Value = dr[1].ToString();
                    dataGridView1.Rows[n].Cells[2].Value = dr[2].ToString();

                }
                dr.Close();
            }
            calPurchasesPrice();
        }
        
        public bool IsClear()
        {
            if (txtCode.Text == "" &&
                txtPrice.Text == "0" &&
                txtPurchases.Text == "0" &&
                txtNormal.Text == "0" &&
                txtUnNormal.Text == "0" &&
                radioList.Checked == true)
                return true;
            else
                return false;
        }
        public void Clear()
        {
            txtCode.Text = "";
            txtPrice.Text = "0";
            txtPurchases.Text = "0";
            txtNormal.Text = "0";
            txtUnNormal.Text = "0";
            txtDes.Text = "";
            txtPlus.Text = "";
            dataGridView1.Rows.Clear();
            radioList.Checked = true;
        }
        public double calPurchasesPrice()
        {
            double addational = 0.0;
            foreach (DataGridViewRow item in dataGridView1.Rows)
            {
                addational += Convert.ToDouble(item.Cells[0].Value);
            }
            double price = double.Parse(txtPrice.Text);
            double PurchasesPercent = double.Parse(txtPurchases.Text);
            if (radioQata3y.Checked == true)
            {
                return price + (price * PurchasesPercent / 100.0) + addational;

            }
            else
            {
                double NormalPercent = double.Parse(txtNormal.Text);
                double unNormalPercent = double.Parse(txtUnNormal.Text);
                double PurchasesPrice = (price + NormalPercent) - ((price + NormalPercent) * PurchasesPercent / 100.0);
                PurchasesPrice = PurchasesPrice + unNormalPercent;
                return PurchasesPrice + addational;
            }
        }
        
        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                double price = double.Parse(txtPrice.Text);
                double PurchasesPercent = double.Parse(txtPurchases.Text);

                if (radioQata3y.Checked == true)
                {
                    #region set qata3yPrice for list item

                    double NormalPercent = double.Parse(txtNormal.Text);
                    double UnNormalPercent = double.Parse(txtUnNormal.Text);

                    supplierBill.updateGrid(/*"قطعى", */price + (price * PurchasesPercent / 100.0), PurchasesPercent, 0.00, price, 0.00, 0.00);

                    #endregion
                }
                else
                {
                    #region set priceList for collection of items

                    double NormalPercent = double.Parse(txtNormal.Text);
                    double unNormalPercent = double.Parse(txtUnNormal.Text);

                    double PurchasesPrice = (price + NormalPercent) - ((price + NormalPercent) * PurchasesPercent / 100.0);

                    PurchasesPrice = PurchasesPrice + unNormalPercent;

                    supplierBill.updateGrid(/*"لستة", */PurchasesPrice, 0.00, double.Parse(txtPurchases.Text), price, double.Parse(txtNormal.Text), double.Parse(txtUnNormal.Text));

                    #endregion
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}

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
    public partial class SetPurchasesPricePopup : Form
    {
        MySqlConnection dbconnection;
        static int id = 0;
        static string code = "";
        static DataRowView mrow;
        public SetPurchasesPricePopup()
        {
            InitializeComponent();
            dbconnection = new MySqlConnection(connection.connectionString);
        }
        private void SetPurchasesPricePopup_Load(object sender, EventArgs e)
        {
            try
            {
                id = StoragePurchases_Report.ID;
                txtCode.Text = id + "";
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
                    labPurchasesPrice.Text = calPurchasesPrice() + "";
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
        private void btnAdd_Click(object sender, EventArgs e)
        {

            try
            {
                if (labPurchasesPrice.Text != "")
                {
                    dbconnection.Open();
                    int PurchasesPrice_ID = 0, OldPurchasingPrice_ID = 0;
                    double price = double.Parse(txtPrice.Text);
                    double PurchasesPercent = double.Parse(txtPurchases.Text);

                    if (radioQata3y.Checked == true)
                    {
                        #region set qata3yPrice for one item

                        if (id != 0)
                        {
                            string query = "INSERT INTO purchasing_price (Normal_Increase,Categorical_Increase,Purchasing_Discount,Price_Type,Last_Price, Purchasing_Price, Data_ID, Price, Date) VALUES(@Normal_Increase,@Categorical_Increase,?Purchasing_Discount,?Price_Type,?Last_Price,?Purchasing_Price,?Data_ID,?Price,?Date)";
                            MySqlCommand command = new MySqlCommand(query, dbconnection);
                            command.Parameters.AddWithValue("?Price_Type", "قطعى");
                            command.Parameters.AddWithValue("?Purchasing_Price", calPurchasesPrice());
                            command.Parameters.AddWithValue("?Data_ID", id);
                            command.Parameters.AddWithValue("@Normal_Increase", getNormalIncrease());
                            command.Parameters.AddWithValue("@Categorical_Increase", getUnNormalIncrease());
                            command.Parameters.AddWithValue("?Price", price);
                            command.Parameters.AddWithValue("?Last_Price", calPurchasesPrice());
                            command.Parameters.AddWithValue("?Purchasing_Discount", double.Parse(txtPurchases.Text));
                            command.Parameters.Add("?Date", MySqlDbType.Date);
                            command.Parameters["?Date"].Value = DateTime.Now.Date;
                            command.ExecuteNonQuery();

                            query = "INSERT INTO oldpurchasing_price (Normal_Increase,Categorical_Increase,Purchasing_Discount,Price_Type,Last_Price, Purchasing_Price, Data_ID, Price, Date) VALUES(@Normal_Increase,@Categorical_Increase,?Purchasing_Discount,?Price_Type,?Last_Price,?Purchasing_Price,?Data_ID,?Price,?Date)";
                            command = new MySqlCommand(query, dbconnection);
                            command.Parameters.AddWithValue("?Price_Type", "قطعى");
                            command.Parameters.AddWithValue("?Purchasing_Price", calPurchasesPrice());
                            command.Parameters.AddWithValue("?Data_ID", id);
                            command.Parameters.AddWithValue("@Normal_Increase", getNormalIncrease());
                            command.Parameters.AddWithValue("@Categorical_Increase", getUnNormalIncrease());
                            command.Parameters.AddWithValue("?Price", price);
                            command.Parameters.AddWithValue("?Last_Price", calPurchasesPrice());
                            command.Parameters.AddWithValue("?Purchasing_Discount", double.Parse(txtPurchases.Text));
                            command.Parameters.Add("?Date", MySqlDbType.Date);
                            command.Parameters["?Date"].Value = DateTime.Now.Date;
                            command.ExecuteNonQuery();
                            insertIntoAdditionalIncrease(ref PurchasesPrice_ID, ref OldPurchasingPrice_ID);
                            UserControl.ItemRecord("purchasing_price", "اضافة", PurchasesPrice_ID, DateTime.Now, "", dbconnection);

                        }
                        else
                        {
                            MessageBox.Show("error in Data_ID");
                            dbconnection.Close();
                            return;
                        }

                        #endregion
                    }
                    else
                    {
                        #region set priceList for one item

                        if (id != 0)
                        {

                            string query = "INSERT INTO purchasing_price (Last_Price,Price_Type,Purchasing_Price,Data_ID,Purchasing_Discount,Price,Normal_Increase,Categorical_Increase,Date) VALUES (?Last_Price,?Price_Type,?Purchasing_Price,?Data_ID,?Purchasing_Discount,?Price,?Normal_Increase,?Categorical_Increase,?Date)";
                            MySqlCommand command = new MySqlCommand(query, dbconnection);
                            command.Parameters.AddWithValue("@Price_Type", "لستة");
                            command.Parameters.AddWithValue("@Purchasing_Price", calPurchasesPrice());
                            command.Parameters.AddWithValue("?Data_ID", id);
                            command.Parameters.AddWithValue("@Purchasing_Discount", double.Parse(txtPurchases.Text));
                            command.Parameters.AddWithValue("@Price", price);
                            command.Parameters.AddWithValue("@Last_Price", lastPrice(calPurchasesPrice()));
                            command.Parameters.AddWithValue("@Normal_Increase", getNormalIncrease());
                            command.Parameters.AddWithValue("@Categorical_Increase", getUnNormalIncrease());
                            command.Parameters.Add("?Date", MySqlDbType.Date);
                            command.Parameters["?Date"].Value = DateTime.Now.Date;

                            command.ExecuteNonQuery();

                            query = "INSERT INTO oldpurchasing_price (Last_Price,Price_Type,Purchasing_Price,Data_ID,Purchasing_Discount,Price,Normal_Increase,Categorical_Increase,Date) VALUES (?Last_Price,?Price_Type,?Purchasing_Price,?Data_ID,?Purchasing_Discount,?Price,?Normal_Increase,?Categorical_Increase,?Date)";
                            command = new MySqlCommand(query, dbconnection);
                            command.Parameters.AddWithValue("@Price_Type", "لستة");
                            command.Parameters.AddWithValue("@Purchasing_Price", calPurchasesPrice());
                            command.Parameters.AddWithValue("?Data_ID", id);
                            command.Parameters.AddWithValue("@Purchasing_Discount", double.Parse(txtPurchases.Text));
                            command.Parameters.AddWithValue("@Price", price);
                            command.Parameters.AddWithValue("@Last_Price", lastPrice(calPurchasesPrice()));
                            command.Parameters.AddWithValue("@Normal_Increase", getNormalIncrease());
                            command.Parameters.AddWithValue("@Categorical_Increase", getUnNormalIncrease());
                            command.Parameters.Add("?Date", MySqlDbType.Date);
                            command.Parameters["?Date"].Value = DateTime.Now.Date;

                            command.ExecuteNonQuery();

                            insertIntoAdditionalIncrease(ref PurchasesPrice_ID, ref OldPurchasingPrice_ID);
                            UserControl.ItemRecord("purchasing_price", "اضافة", PurchasesPrice_ID, DateTime.Now, "", dbconnection);

                        }
                        else
                        {
                            MessageBox.Show("error in Data_ID");
                            dbconnection.Close();
                            return;
                        }

                        #endregion
                    }

                    MessageBox.Show("تم");
                
                    mrow[6] = Convert.ToDouble(labPurchasesPrice.Text);
                    Clear();
                    //productsPurchasesPriceForm.displayProducts();
                }
                else
                {
                    MessageBox.Show("لم يتم تسجيل سعر الشراء");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }
        //functions
        public double calPurchasesPrice()
        {
            double price = double.Parse(txtPrice.Text);

            double PurchasesPercent = double.Parse(txtPurchases.Text);
            if (radioQata3y.Checked == true)
            {
                //return price + (price * PurchasesPercent / 100.0) + addational;
                price += getNormalIncrease() + getUnNormalIncrease(); ;
                return price - (price * PurchasesPercent / 100.0);
            }
            else
            {
                double PurchasesPrice = (price + getNormalIncrease()) - ((price + getNormalIncrease()) * PurchasesPercent / 100.0);
                PurchasesPrice = PurchasesPrice + getUnNormalIncrease();
                return PurchasesPrice;
            }
        }
        public double getNormalIncrease()
        {
            double result = 0;
            if (dataGridView1.Rows.Count > 0)
            {
                foreach (DataGridViewRow item in dataGridView1.Rows)
                {
                    if (item.Cells[1].Value.ToString() == "عادية")
                    {
                        result += Convert.ToDouble(item.Cells[0].Value);
                    }
                }
            }
            return result;
        }
        public double getUnNormalIncrease()
        {
            double result = 0;
            if (dataGridView1.Rows.Count > 0)
            {
                foreach (DataGridViewRow item in dataGridView1.Rows)
                {
                    if (item.Cells[1].Value.ToString() == "قطعية")
                    {
                        result += Convert.ToDouble(item.Cells[0].Value);
                    }
                }
            }
            return result;
        }
        public double lastPrice(double purchasePrice)
        {
            double discount = double.Parse(txtPurchases.Text);
            double lastPrice = purchasePrice * 100 / (100 - discount);

            return lastPrice;
        }
        public void Clear()
        {
            txtCode.Text = "";
            txtPrice.Text = "0";
            txtPurchases.Text = "0";
            txtDes.Text = "";
            txtPlus.Text = "";
            labPurchasesPrice.Text = "";
            dataGridView1.Rows.Clear();
            //txtCodePart1.Text = txtCodePart2.Text = txtCodePart3.Text = txtCodePart4.Text = txtCodePart5.Text = "";
            radioList.Checked = true;
            dbconnection.Close();
        }
        public void insertIntoAdditionalIncrease(ref int PurchasingPrice_ID, ref int OldPurchasingPrice_ID)
        {
            string queryx = "select PurchasingPrice_ID from purchasing_price order by PurchasingPrice_ID desc limit 1";
            MySqlCommand com = new MySqlCommand(queryx, dbconnection);
            if (com.ExecuteScalar() != null)
            {
                PurchasingPrice_ID = Convert.ToInt32(com.ExecuteScalar());
            }
            //for archive table
            queryx = "select OldPurchasingPrice_ID from oldpurchasing_price order by OldPurchasingPrice_ID desc limit 1";
            com = new MySqlCommand(queryx, dbconnection);
            if (com.ExecuteScalar() != null)
            {
                OldPurchasingPrice_ID = Convert.ToInt32(com.ExecuteScalar());
            }
            foreach (DataGridViewRow item in dataGridView1.Rows)
            {
                double addational = Convert.ToDouble(item.Cells[0].Value);
                queryx = "insert into additional_increase_purchasingprice (PurchasingPrice_ID,AdditionalValue,Type,Description) values (@PurchasingPrice_ID,@AdditionalValue,@Type,@Description)";
                com = new MySqlCommand(queryx, dbconnection);
                com.Parameters.AddWithValue("@PurchasingPrice_ID", PurchasingPrice_ID);
                com.Parameters.AddWithValue("@Type", item.Cells[1].Value);
                com.Parameters.AddWithValue("@AdditionalValue", item.Cells[0].Value);
                com.Parameters.AddWithValue("@Description", item.Cells[2].Value);
                com.ExecuteNonQuery();

                //insert into archive table
                queryx = "insert into old_additional_increase_purchasingprice (OldPurchasingPrice_ID,AdditionalValue,Type,Description) values (@OldPurchasingPrice_ID,@AdditionalValue,@Type,@Description)";
                com = new MySqlCommand(queryx, dbconnection);
                com.Parameters.AddWithValue("@OldPurchasingPrice_ID", OldPurchasingPrice_ID);
                com.Parameters.AddWithValue("@Type", item.Cells[1].Value);
                com.Parameters.AddWithValue("@AdditionalValue", item.Cells[0].Value);
                com.Parameters.AddWithValue("@Description", item.Cells[2].Value);
                com.ExecuteNonQuery();

            }
        }

        public static void setData(SetPurchasesPricePopup f, DataRowView row)
        {
            id = StoragePurchases_Report.ID;
            code = StoragePurchases_Report.codef1;
            mrow = row;
            f.dd();
        }
        public void dd()
        {
            txtCode.Text = code;
        }

        private void txtPrice_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if(e.KeyCode==Keys.Enter)
                    labPurchasesPrice.Text = calPurchasesPrice() + ""; 
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}

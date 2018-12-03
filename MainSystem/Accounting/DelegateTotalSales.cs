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

namespace MainSystem
{
    public partial class DelegateTotalSales : DevExpress.XtraEditors.XtraForm
    {
        private MySqlConnection dbconnection;
        bool loaded = false;
        public DelegateTotalSales()
        {
            try
            {
                InitializeComponent();
                dbconnection = new MySqlConnection(connection.connectionString);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
          
        }

        private void DelegateTotalSales_Load(object sender, EventArgs e)
        {
            try
            {
                dbconnection.Open();
                string query = "select * from delegate ";
                MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                DataTable dt = new DataTable();
                da.Fill(dt);
                comDelegate.DataSource = dt;
                comDelegate.DisplayMember = dt.Columns["Delegate_Name"].ToString();
                comDelegate.ValueMember = dt.Columns["Delegate_ID"].ToString();
                comDelegate.Text = "";
                txtDelegateID.Text = "";
                loaded = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }
        private void comDelegate_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (loaded)
                {
                    try
                    {
                        txtDelegateID.Text = comDelegate.SelectedValue.ToString();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txtDelegateID_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                string query = "select Customer_Name from customer where Customer_ID=" + txtDelegateID.Text + "";
                MySqlCommand com = new MySqlCommand(query, dbconnection);
                if (com.ExecuteScalar() != null)
                {
                    Name = (string)com.ExecuteScalar();
                    comDelegate.Text = Name;

                }
                else
                {
                    MessageBox.Show("there is no item with this id");
                    dbconnection.Close();
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                dbconnection.Open();
                DateTime date = dateTimeFrom.Value;
                string d = date.ToString("yyyy-MM-dd HH:mm:ss");
                DateTime date2 = dateTimeTo.Value;
                string d2 = date2.ToString("yyyy-MM-dd HH:mm:ss");

                string query= "select CustomerBill_ID from customer_bill where Paid_Status=1 and Bill_Date between '" + d + "' and '" + d2 + "'";
                MySqlCommand com = new MySqlCommand(query, dbconnection);
                MySqlDataReader dr = com.ExecuteReader();
                string str = "";
                while (dr.Read())
                {
                    str += dr[0].ToString() + ",";
                }
                dr.Close();
                str += 0;

                query = "select CustomerBill_ID from customer_return_bill_details inner join customer_return_bill on customer_return_bill_details.CustomerReturnBill_ID=customer_return_bill.CustomerReturnBill_ID where  Date between '" + d + "' and '" + d2 + "'";
                com = new MySqlCommand(query, dbconnection);
                dr = com.ExecuteReader();
                string str1 = "";
                while (dr.Read())
                {
                    str1 += dr[0].ToString() + ",";
                }
                dr.Close();
                str1 += 0;
                query = "select sum(product_bill.PriceAD*Quantity) as 'اجمالي المبيعات',sum(TotalAD) as 'اجمالي المرتجعات',(sum(product_bill.PriceAD*Quantity)-sum(TotalAD)) as 'الصافي' from product_bill,customer_return_bill_details where customer_return_bill_details.CustomerBill_ID in(" + str1+ ") and customer_return_bill_details.Delegate_ID=" + txtDelegateID.Text+ " and product_bill.CustomerBill_ID in(" + str + ") and product_bill.Delegate_ID=" + txtDelegateID.Text;
                MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                DataTable dt = new DataTable();

                da.Fill(dt);
                gridControl1.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close(); 
        }

     
    }
}
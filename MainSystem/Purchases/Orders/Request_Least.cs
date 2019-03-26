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
    public partial class Request_Least : Form
    {
        MySqlConnection conn;
        List<DataRow> row1 = null;

        public Request_Least(List<DataRow> Row1)
        {
            InitializeComponent();
            conn = new MySqlConnection(connection.connectionString);
            row1 = Row1;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                //txtCode.Text = row1["الكود"].ToString();
                conn.Open();
                string query = "SELECT distinct Supplier_ID,Supplier_Name FROM supplier";
                MySqlDataAdapter command = new MySqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                command.Fill(dt);
                comSupplier.ValueMember = "Supplier_ID";
                comSupplier.DisplayMember = "Supplier_Name";
                comSupplier.DataSource = dt;
                comSupplier.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            conn.Close();
        }

        private void btnRequest_Click(object sender, EventArgs e)
        {
            try
            {
                if (comSupplier.SelectedValue != null && txtEmployee.Text != "" && txtQuantity.Text != "")
                {
                    double quantity = 0;
                    if (double.TryParse(txtQuantity.Text, out quantity))
                    {
                        conn.Open();
                        string query = "insert into request_Least (Order_Date,Delivery_Date,Supplier_ID,Data_ID,Quantity,Employee_Name,Employee_ID) values ('" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "' , '" + dateTimePicker2.Value.ToString("yyyy-MM-dd") + "' , " + comSupplier.SelectedValue.ToString() + " , " + row1[0]["Data_ID"].ToString() + ", " + quantity + " , '" + UserControl.EmpName + "'," + UserControl.EmpID + ")";
                        MySqlCommand comand = new MySqlCommand(query, conn);
                        comand.ExecuteNonQuery();
                        conn.Close();
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("الكمية يجب ان تكون عدد");
                    }
                }
                else
                {
                    MessageBox.Show("تاكد من ادخال جميع البيانات");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            conn.Close();
        }
    }
}

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

namespace TaxesSystem.Shipping.Recording
{
    public partial class ShippingOperationRecord : Form
    {
        MySqlConnection dbconnection;
        bool loaded = false;
        public ShippingOperationRecord()
        {
            dbconnection = new MySqlConnection(connection.connectionString);
            InitializeComponent();
        }

        private void ShippingOperationRecord_Load(object sender, EventArgs e)
        {
            try
            {
                dbconnection.Open();

                string query = "select * from cars ";
                MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                DataTable dt = new DataTable();
                da.Fill(dt);
                comCar.DataSource = dt;
                comCar.DisplayMember = dt.Columns["Car_Number"].ToString();
                comCar.ValueMember = dt.Columns["Car_ID"].ToString();
                comCar.Text = "";
                txtCar.Text = "";

                query = "select * from drivers ";
                da = new MySqlDataAdapter(query, dbconnection);
                dt = new DataTable();
                da.Fill(dt);
                comDriver.DataSource = dt;
                comDriver.DisplayMember = dt.Columns["Driver_Name"].ToString();
                comDriver.ValueMember = dt.Columns["Driver_ID"].ToString();
                comDriver.Text = "";
                txtDriver.Text = "";

                loaded = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }
    }
}

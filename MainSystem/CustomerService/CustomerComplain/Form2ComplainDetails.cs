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

namespace CustomerServiceSearchPhone
{
    public partial class Form2ComplainDetails : Form
    {
        MySqlConnection dbconnection;
        string[] data;
        public Form2ComplainDetails(string [] data)
        {
            this.data = data;
            InitializeComponent();
        //    dbconnection = new MySqlConnection(connection.connectionString);
        }

        private void Form2ComplainDetails_Load(object sender, EventArgs e)
        {
            try
            {
                dbconnection.Open();
                string query = "select * from customer where Customer_ID=" + data[0];
                MySqlCommand com = new MySqlCommand(query, dbconnection);
                MySqlDataReader dr = com.ExecuteReader();
                while (dr.Read())
                {
                    txtCustomerName.Text = dr["Customer_Name"].ToString();
                    txtCustomerPhone.Text= dr["Customer_Phone"].ToString();
                    txtCustomerAddress.Text = dr["Customer_Address"].ToString();
                }
                dr.Close();
                txtEmployeeName.Text = data[1];
                txtComplain.Text = data[2];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }

        private void Form2ComplainDetails_FormClosed(object sender, FormClosedEventArgs e)
        {
            Environment.Exit(0);
        }
    }
}

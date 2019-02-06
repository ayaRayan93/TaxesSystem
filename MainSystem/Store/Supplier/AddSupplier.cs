using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MainSystem
{
    public partial class AddSupplier : Form
    {
        MySqlConnection connection;
        public AddSupplier()
        {
            InitializeComponent();
            string connectionString;
            connectionString = "SERVER=localhost;DATABASE=ccc;user=root;PASSWORD=root;CHARSET=utf8";

            connection = new MySqlConnection(connectionString);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {

                double output;
                if (Double.TryParse(textBox5.Text, out output))
                {
                }
                else
                {
                    MessageBox.Show("enter number");
                    connection.Close();
                    return;
                }
                if (Double.TryParse(textBox6.Text, out output))
                {
                }
                else
                {
                    MessageBox.Show("enter number");
                    connection.Close();
                    return;
                }
                
                connection.Open();
                string query;
                query = "insert into Suppliers (Name,Address,Phone,Fax,E_mail,Start_Date,AddationalDetails,OpenCashTo,OpenCashFrom) values (@Name,@Address,@Phone,@Fax,@E_mail,@Start_Date,@AddationalDetails,@To1,@From1)";
                MySqlCommand com = new MySqlCommand(query, connection);
                com.Parameters.Add("@Name", MySqlDbType.VarChar, 255);
                com.Parameters["@Name"].Value =textBox1.Text;
                com.Parameters.Add("@Address", MySqlDbType.VarChar, 255);
                com.Parameters["@Address"].Value = textBox2.Text;
                com.Parameters.Add("@Phone", MySqlDbType.VarChar, 255);
                com.Parameters["@Phone"].Value = textBox3.Text;
                com.Parameters.Add("@Fax", MySqlDbType.VarChar, 255);
                com.Parameters["@Fax"].Value = textBox9.Text;
                com.Parameters.Add("@E_mail", MySqlDbType.VarChar, 255);
                com.Parameters["@E_mail"].Value = textBox4.Text;
                com.Parameters.Add("@Start_Date", MySqlDbType.Date, 0);
                com.Parameters["@Start_Date"].Value = dateTimePicker1.Value;
                com.Parameters.Add("@AddationalDetails", MySqlDbType.VarChar, 255);
                com.Parameters["@AddationalDetails"].Value = textBox7.Text;
                com.Parameters.Add("@To1", MySqlDbType.VarChar,255);
                com.Parameters["@To1"].Value = textBox5.Text;
                com.Parameters.Add("@From1", MySqlDbType.VarChar,255);
                com.Parameters["@From1"].Value = textBox6.Text;

                com.ExecuteNonQuery();
                MessageBox.Show("add success");
            } catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());

            }
            connection.Close();
        }
    }
}

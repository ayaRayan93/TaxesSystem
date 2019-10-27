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
    public partial class Form1 : Form
    {
        MySqlConnection dbconnection, dbconnection1;
        public Form1()
        {
            InitializeComponent();
            dbconnection = new MySqlConnection(connection.connectionString);
            dbconnection1 = new MySqlConnection(connection.connectionString);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dbconnection.Open();
            dbconnection1.Open();
            string q = "select Data_ID from table1";
            MySqlCommand com = new MySqlCommand(q, dbconnection);
            MySqlDataReader dr = com.ExecuteReader();
            while (dr.Read())
            {
                string query = " INSERT INTO open_storage_account (Data_ID,Quantity,Store_ID,Store_Place_ID,Date) VALUES ("+dr[0].ToString()+",0.00,5,13,'2019-10-24 00:00:00')";
                MySqlCommand com1 = new MySqlCommand(query, dbconnection1);
                com1.ExecuteNonQuery();
            }
            dr.Close();
            dbconnection.Close();
            dbconnection1.Close();
        }
    }
}

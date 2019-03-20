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

namespace PurchasesDepartment
{
    public partial class Form1 : Form
    {
        MySqlConnection dconnection;

        public Form1()
        {
            InitializeComponent();
            dconnection = new MySqlConnection(connection.ConnectionString);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            testQuantity();
        }

        void testQuantity()
        {
            try
            {
                string query = "SELECT data.Code,SUM(storage.Total_Meters),data.Least_Quantity FROM data INNER JOIN storage ON data.Code = storage.Code group by storage.Code having (SUM(storage.Total_Meters) <= data.Least_Quantity=1)";
                dconnection.Open();
                MySqlDataAdapter da = new MySqlDataAdapter(query, dconnection);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    dataGridView1.DataSource = dt;
                }
                dconnection.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
    public static class connection
    {
        public static string ConnectionString = "SERVER=192.168.1.200;DATABASE=ccc;user=Devccc;PASSWORD=rootroot;CHARSET=utf8";
    }
}

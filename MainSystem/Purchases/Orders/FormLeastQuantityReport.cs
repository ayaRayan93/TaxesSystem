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
    public partial class FormLeastQuantityReport : Form
    {
        MySqlConnection dconnection;

        public FormLeastQuantityReport()
        {
            InitializeComponent();
            dconnection = new MySqlConnection(connection.connectionString);
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
}

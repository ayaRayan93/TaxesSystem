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
    public partial class LeastQuantityReport : Form
    {
        MySqlConnection dconnection;

        public LeastQuantityReport()
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
                string query = "SELECT data.Code,SUM(storage.Total_Meters),least_offer.Least_Quantity FROM least_offer INNER JOIN data ON least_offer.Data_ID = data.Data_ID INNER JOIN storage ON storage.Data_ID = data.Data_ID group by data.Data_ID having (SUM(storage.Total_Meters) <= least_offer.Least_Quantity=1)";
                dconnection.Open();
                MySqlDataAdapter da = new MySqlDataAdapter(query, dconnection);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    gridControl1.DataSource = dt;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dconnection.Close();
        }

        private void gridView1_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {

        }
    }
}

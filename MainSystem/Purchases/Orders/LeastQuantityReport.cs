using DevExpress.XtraEditors.Repository;
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
        List<DataRow> row1 = null;

        public LeastQuantityReport()
        {
            InitializeComponent();
            dconnection = new MySqlConnection(connection.connectionString);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                testQuantity();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dconnection.Close();
        }

        void testQuantity()
        {
            dconnection.Open();

            string q1 = "select Type_ID from type";

            string query = "SELECT data.Code as 'الكود',SUM(storage.Total_Meters) as 'الكمية المتاحة',least_offer.Least_Quantity as 'الحد الادنى','تسوية' FROM least_offer INNER JOIN data ON least_offer.Data_ID = data.Data_ID INNER JOIN storage ON storage.Data_ID = data.Data_ID group by data.Data_ID having (SUM(storage.Total_Meters) <= least_offer.Least_Quantity=1)";
            MySqlDataAdapter da = new MySqlDataAdapter(query, dconnection);
            DataTable dt = new DataTable();
            da.Fill(dt);
            gridControl1.DataSource = dt;

            RepositoryItemCheckEdit repositoryCheckEdit1 = gridControl1.RepositoryItems.Add("CheckEdit") as RepositoryItemCheckEdit;
            repositoryCheckEdit1.ValueChecked = "True";
            repositoryCheckEdit1.ValueUnchecked = "False";
            gridView1.Columns["تسوية"].ColumnEdit = repositoryCheckEdit1;
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            /*try
            {
                row1 = gridView1.GetDataRow(gridView1.FocusedRowHandle);
                if (row1 != null)
                {
                    Request_Least form = new Request_Least(row1);
                    form.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }*/
        }

        private void btnOpenBill_Click(object sender, EventArgs e)
        {
            try
            {
                if (gridView1.SelectedRowsCount > 0)
                {
                    for (int i = 0; i < gridView1.SelectedRowsCount; i++)
                    {
                        row1.Add(gridView1.GetDataRow(gridView1.GetRowHandle(i)));
                    }
                    Request_Least form = new Request_Least(row1);
                    form.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}

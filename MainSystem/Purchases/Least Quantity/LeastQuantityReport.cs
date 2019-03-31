using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraTab;
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
        MySqlConnection dbconnection;
        MainForm mainForm = null;
        XtraTabControl xtraTabControlPurchases;

        public LeastQuantityReport(MainForm mainform, XtraTabControl XtraTabControlPurchases)
        {
            InitializeComponent();
            dbconnection = new MySqlConnection(connection.connectionString);
            mainForm = mainform;
            xtraTabControlPurchases = XtraTabControlPurchases;
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
            dbconnection.Close();
        }

        void testQuantity()
        {
            dbconnection.Open();

            string q1 = "select Data_ID from storage_least_taswya";
            string q2 = "SELECT order_details.Data_ID FROM orders INNER JOIN order_details ON order_details.Order_ID = orders.Order_ID where orders.Received=0";

            string query = "SELECT data.Data_ID,data.Code as 'الكود',SUM(storage.Total_Meters) as 'الكمية المتاحة',least_offer.Least_Quantity as 'الحد الادنى','تسوية' FROM least_offer INNER JOIN data ON least_offer.Data_ID = data.Data_ID INNER JOIN storage ON storage.Data_ID = data.Data_ID group by data.Data_ID having (SUM(storage.Total_Meters) <= least_offer.Least_Quantity=1) and data.Data_ID not in(" + q1 + ") and data.Data_ID not in(" + q2 + ")";
            MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
            DataTable dt = new DataTable();
            da.Fill(dt);
            gridControl1.DataSource = dt;
            gridView1.Columns[0].Visible = false;

            RepositoryItemCheckEdit repositoryCheckEdit1 = gridControl1.RepositoryItems.Add("CheckEdit") as RepositoryItemCheckEdit;
            repositoryCheckEdit1.ValueChecked = "True";
            repositoryCheckEdit1.ValueUnchecked = "False";
            gridView1.Columns["تسوية"].ColumnEdit = repositoryCheckEdit1;
            repositoryCheckEdit1.CheckedChanged += new EventHandler(CheckedChanged);
        }

        private void CheckedChanged(object sender, System.EventArgs e)
        {
            try
            {
                CheckEdit edit = sender as CheckEdit;
                switch (edit.Checked)
                {
                    case true:
                        if (MessageBox.Show("هل انت متاكد انك تريد الحذف؟", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            dbconnection.Open();
                            string query = "insert into storage_least_taswya (Data_ID,Date) values (@Data_ID,@Date)";
                            MySqlCommand com = new MySqlCommand(query, dbconnection);
                            com.Parameters.Add("@Data_ID", MySqlDbType.Int16);
                            com.Parameters["@Data_ID"].Value = gridView1.GetFocusedRowCellDisplayText(gridView1.Columns["Data_ID"]);
                            com.Parameters.Add("@Date", MySqlDbType.DateTime);
                            com.Parameters["@Date"].Value = DateTime.Now;
                            com.ExecuteNonQuery();
                            dbconnection.Close();
                            testQuantity();
                            mainForm.LeastQuantityFunction();
                        }
                        else
                        {
                            edit.CheckState = CheckState.Unchecked;
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }

        private void btnOpenBill_Click(object sender, EventArgs e)
        {
            try
            {
                if (gridView1.SelectedRowsCount > 0)
                {
                    List<DataRow> row1 = new List<DataRow>();
                    for (int i = 0; i < gridView1.SelectedRowsCount; i++)
                    {
                        row1.Add(gridView1.GetDataRow(gridView1.GetSelectedRows()[i]));
                    }
                    mainForm.bindRecordOrderForm(null, row1);
                    //Order_Record form = new Order_Record(row1, null, xtraTabControlPurchases);
                    //form.ShowDialog();
                }
                else
                {
                    MessageBox.Show("يجب اختيار البنود اولا");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}

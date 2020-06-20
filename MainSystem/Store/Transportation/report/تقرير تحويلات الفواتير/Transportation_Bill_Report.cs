using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraPrinting;
using DevExpress.XtraPrinting.Preview;
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
    public partial class Transportation_Bill_Report : Form
    {
        MySqlConnection conn;
        MySqlConnection connectionReader1, connectionReader2;
        MainForm MainForm = null;
        
        public Transportation_Bill_Report(MainForm mainForm)
        {
            InitializeComponent();
            conn = new MySqlConnection(connection.connectionString);
            connectionReader1 = new MySqlConnection(connection.connectionString);
            connectionReader2 = new MySqlConnection(connection.connectionString);
            MainForm = mainForm;
        }

        private void Bills_Transitions_Report_Load(object sender, EventArgs e)
        {
            try
            {
                string query = "select * from Store";
                MySqlDataAdapter da = new MySqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                comStore.DataSource = dt;
                comStore.DisplayMember = dt.Columns["Store_Name"].ToString();
                comStore.ValueMember = dt.Columns["Store_ID"].ToString();
                comStore.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (comStore.SelectedValue != null)
                {
                    search();
                }
                else
                {
                    MessageBox.Show("يجب اختيار المخزن");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            conn.Close();
            connectionReader1.Close();
        }

        private void btnNewChosen_Click(object sender, EventArgs e)
        {
            try
            {
                clearCom();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void search()
        {
            DataSet sourceDataSet = new DataSet();
            MySqlDataAdapter adapterSupplier = new MySqlDataAdapter("SELECT distinct transfer_product.TransferProduct_ID as 'رقم التحويل',concat(customer_bill.Branch_Name,' ',customer_bill.Branch_BillNumber) as 'الفاتورة',transfer_product.Date as 'تاريخ التحويل' FROM transfer_product inner join store as storeFrom on storeFrom.Store_ID = transfer_product.From_Store INNER JOIN transfer_product_details ON transfer_product_details.TransferProduct_ID = transfer_product.TransferProduct_ID INNER JOIN customer_bill ON transfer_product_details.CustomerBill_ID = customer_bill.CustomerBill_ID where transfer_product.Canceled=0 and transfer_product_details.CustomerBill_ID<>0 and transfer_product.From_Store=" + comStore.SelectedValue+" and date(transfer_product.Date) between '" + dateTimePicker1.Value.Date.ToString("yyyy-MM-dd") + "' and '" + dateTimePicker2.Value.Date.ToString("yyyy-MM-dd") + "'", conn);
            adapterSupplier.Fill(sourceDataSet);
            gridControl1.DataSource = sourceDataSet.Tables[0];
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            try
            {
                List<TransportationBill_Items> bi = new List<TransportationBill_Items>();

                for (int i = 0; i < gridView1.RowCount; i++)
                {
                    TransportationBill_Items item = new TransportationBill_Items() { Date = Convert.ToDateTime(gridView1.GetRowCellDisplayText(i, gridView1.Columns["تاريخ التحويل"])), Bill_Number = gridView1.GetRowCellDisplayText(i, gridView1.Columns["الفاتورة"]), Transportation_Number = Convert.ToInt16(gridView1.GetRowCellDisplayText(i, gridView1.Columns["رقم التحويل"])) };

                    bi.Add(item);
                }
                Report_Transportation_Bill2 f = new Report_Transportation_Bill2();
                f.PrintInvoice(comStore.Text, dateTimePicker1.Value.Date, dateTimePicker2.Value.Date, bi);
                f.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void clearCom()
        {
            dateTimePicker1.Value = DateTime.Now.Date;
            dateTimePicker2.Value = DateTime.Now.Date;
            gridControl1.DataSource = null;
        }
    }
}

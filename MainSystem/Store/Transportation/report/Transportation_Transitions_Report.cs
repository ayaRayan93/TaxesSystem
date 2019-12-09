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
    public partial class Transportation_Transitions_Report : Form
    {
        MySqlConnection conn;
        MySqlConnection connectionReader1, connectionReader2;
        MainForm MainForm = null;
        
        public Transportation_Transitions_Report(MainForm mainForm)
        {
            InitializeComponent();
            conn = new MySqlConnection(connection.connectionString);
            connectionReader1 = new MySqlConnection(connection.connectionString);
            connectionReader2 = new MySqlConnection(connection.connectionString);
            MainForm = mainForm;
        }

        private void Bills_Transitions_Report_Load(object sender, EventArgs e)
        {
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                search();
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
            MySqlDataAdapter adapterSupplier = new MySqlDataAdapter("SELECT distinct transfer_product.TransferProduct_ID as 'رقم التحويل',storeFrom.Store_Name as 'من مخزن',storeTo.Store_Name as 'الى مخزن',transfer_product.Date as 'تاريخ التحويل',COALESCE(IF(transfer_product_details.CustomerBill_ID=0,'مخزن','فاتورة'),'CUSTOM') as 'البيان' FROM transfer_product left JOIN store as storeTo ON storeTo.Store_ID = transfer_product.To_Store left join store as storeFrom on storeFrom.Store_ID = transfer_product.From_Store INNER JOIN transfer_product_details ON transfer_product_details.TransferProduct_ID = transfer_product.TransferProduct_ID where transfer_product.Canceled=0 and date(transfer_product.Date) between '" + dateTimePicker1.Value.Date.ToString("yyyy-MM-dd") + "' and '" + dateTimePicker2.Value.Date.ToString("yyyy-MM-dd") + "'", conn);
            adapterSupplier.Fill(sourceDataSet);
            gridControl1.DataSource = sourceDataSet.Tables[0];
        }
        
        public void clearCom()
        {
            dateTimePicker1.Value = DateTime.Now.Date;
            dateTimePicker2.Value = DateTime.Now.Date;
            gridControl1.DataSource = null;
        }
    }
}

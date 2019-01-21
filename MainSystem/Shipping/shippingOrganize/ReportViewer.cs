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
    public partial class ReportViewer : Form
    {
        MySqlConnection dbconnection;
        string query;
        List<StorePermissionsNumbers> listOfStorePermissionsNumbers;
        public ReportViewer(string query)
        {
            try
            {
                dbconnection = new MySqlConnection(connection.connectionString);
                InitializeComponent();
                this.query = query;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ReportViewer_Load(object sender, EventArgs e)
        {
            try
            {
                //StorePerNums StorePerNums = new StorePerNums();
                //StorePerNums.InitializeData(listOfStorePermissionsNumbers,query);
                ShippingReport ShippingReport = new ShippingReport();
                ShippingReport.InitializeData(query);
                documentViewer1.DocumentSource = ShippingReport;
                ShippingReport.CreateDocument();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}

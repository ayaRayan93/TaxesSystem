using DevExpress.XtraGrid.Views.Grid;
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
    public partial class Permission_Report : Form
    {
        // Sytem variables
        MySqlConnection dbConnection;

        public Permission_Report()
        {
            try
            {
                InitializeComponent();
                dbConnection = new MySqlConnection(connection.connectionString);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //Sytem Events
        private void Permission_Report_Load(object sender, EventArgs e)
        {
            try
            {
                dbConnection.Open();
                displayAll();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbConnection.Close();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                dbConnection.Open();
                displayPermission();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbConnection.Close();
        }
        
        //System Functions
        public void displayAll()
        {
            string query = "SELECT shipping.Bill_Number as 'رقم الفاتورة',branch.Branch_Name as 'الفرع',customer.Customer_Name as 'العميل',shipping.Phone as 'التليفون',shipping.Address as 'العنوان',zone.Zone_Name as 'الزون',shipping.Description as 'البيان',shipping.Date as 'التاريخ' FROM shipping INNER JOIN customer ON customer.Customer_ID = shipping.Customer_ID INNER JOIN branch ON branch.Branch_ID = shipping.Branch_ID INNER JOIN zone ON zone.Zone_ID = shipping.Zone_ID WHERE shipping.Delivered=0";
            MySqlDataAdapter adabter = new MySqlDataAdapter(query, dbConnection);
            DataTable dTable = new DataTable();
            adabter.Fill(dTable);
            gridControl1.DataSource = dTable;
        }

        public void displayPermission()
        {
            string query = "SELECT shipping.Bill_Number as 'رقم الفاتورة',branch.Branch_Name as 'الفرع',customer.Customer_Name as 'العميل',shipping.Phone as 'التليفون',shipping.Address as 'العنوان',zone.Zone_Name as 'الزون',shipping.Description as 'البيان',shipping.Date as 'التاريخ' FROM shipping INNER JOIN customer ON customer.Customer_ID = shipping.Customer_ID INNER JOIN branch ON branch.Branch_ID = shipping.Branch_ID INNER JOIN zone ON zone.Zone_ID = shipping.Zone_ID WHERE shipping.Delivered=0 and date(shipping.Date) between '" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "' and '" + dateTimePicker2.Value.ToString("yyyy-MM-dd") + "'";
            MySqlDataAdapter adabter = new MySqlDataAdapter(query, dbConnection);
            DataTable dTable = new DataTable();
            adabter.Fill(dTable);
            gridControl1.DataSource = dTable;
        }
    }
}

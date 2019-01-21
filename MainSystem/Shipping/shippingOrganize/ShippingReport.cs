using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using MySql.Data.MySqlClient;
using System.Data;

namespace MainSystem
{
    public partial class ShippingReport : DevExpress.XtraReports.UI.XtraReport
    {
        MySqlConnection dbconnection;
        public ShippingReport()
        {
            InitializeComponent();
            dbconnection = new MySqlConnection(connection.connectionString);
        }

        public void InitializeData(string q)
        {
            string qq = "SELECT customer_permissions.Store_ID,Store_Name,Permissin_ID from customer_permissions inner join store on customer_permissions.Store_ID=store.Store_ID where Permissin_ID in (45,46)";
     
            sqlDataSource1.Queries[0].Parameters[0].Value = q;
        }
    }
}

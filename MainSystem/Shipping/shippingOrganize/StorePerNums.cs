using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System.Data;

namespace MainSystem
{
    public partial class StorePerNums : DevExpress.XtraReports.UI.XtraReport
    {
        MySqlConnection dbconnection;
        public StorePerNums()
        {
            InitializeComponent();
            dbconnection = new MySqlConnection(connection.connectionString);
        }
        public void InitializeData(List<StorePermissionsNumbers> listOfStorePermissionsNumbers,string query)
        {
            MySqlCommand com = new MySqlCommand(query, dbconnection);
            MySqlDataAdapter adpter = new MySqlDataAdapter(query, dbconnection);
            DataTable dt = new DataTable();
            adpter.Fill(dt);
            DataSource =dt ;
        }
    }
}

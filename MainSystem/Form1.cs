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
    public partial class Form1 : Form
    {
        MySqlConnection dbconnection, dbconnection1;
        public Form1()
        {
            InitializeComponent();
            dbconnection = new MySqlConnection(connection.connectionString);
            dbconnection1 = new MySqlConnection(connection.connectionString);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            List<string> str = new List<string>();
            dbconnection.Open();
            dbconnection1.Open();
            //string query = "select Data_ID from table1";
            //MySqlCommand com = new MySqlCommand(query,dbconnection);
            //MySqlDataReader dr = com.ExecuteReader();
            //while (dr.Read())
            //{
            //    query = "select Data_ID from table2 where Data_ID="+dr[0];
            //    com = new MySqlCommand(query, dbconnection1);
            //    if (com.ExecuteScalar() == null)
            //    {
                    //str.Add(dr[0].ToString());
            string query = "insert into open_storage_account (Data_ID,Quantity,Store_ID,Store_Place_ID,Date,Note) values (@Data_ID,@Quantity,@Store_ID,@Store_Place_ID,@Date,@Note)";
            MySqlCommand com = new MySqlCommand(query, dbconnection1);
                    com.Parameters.Add("@Data_ID", MySqlDbType.Int16);
                    com.Parameters["@Data_ID"].Value = 10945;
                    com.Parameters.Add("@Quantity", MySqlDbType.Decimal);
                    com.Parameters["@Quantity"].Value = 0;
                    com.Parameters.Add("@Store_ID", MySqlDbType.Int16);
                    com.Parameters["@Store_ID"].Value = 3;
                    com.Parameters.Add("@Store_Place_ID", MySqlDbType.Int16);
                    com.Parameters["@Store_Place_ID"].Value = 14;
                    com.Parameters.Add("@Date", MySqlDbType.Date, 0);
                    com.Parameters["@Date"].Value = DateTime.Now.Date;
                    com.Parameters.Add("@Note", MySqlDbType.VarChar);
                    com.Parameters["@Note"].Value = "مرتجع عميل";
                    com.ExecuteNonQuery();

                    string q = "select sum(TotalMeter) from customer_return_bill_details where Data_ID=" + 10945 + " and Store_ID=3 group by Data_ID";
                    MySqlCommand c = new MySqlCommand(q, dbconnection1);
                    double totalMetre = Convert.ToDouble(c.ExecuteScalar().ToString());

                    //save to storage with gard value
                    query = "insert into storage (Store_ID,Type,Storage_Date,Data_ID,Store_Place_ID,Total_Meters,Note) values (@Store_ID,@Type,@Date,@Data_ID,@PlaceOfStore,@TotalOfMeters,@Note)";
                    com = new MySqlCommand(query, dbconnection1);
                    com.Parameters.Add("@Store_ID", MySqlDbType.Int16);
                    com.Parameters["@Store_ID"].Value = 3;
                    com.Parameters.Add("@Type", MySqlDbType.VarChar);
                    com.Parameters["@Type"].Value = "بند";
                    com.Parameters.Add("@Date", MySqlDbType.Date, 0);
                    com.Parameters["@Date"].Value = DateTime.Now.Date;
                    com.Parameters.Add("@Data_ID", MySqlDbType.Int16);
                    com.Parameters["@Data_ID"].Value = 10945;
                    com.Parameters.Add("@PlaceOfStore", MySqlDbType.Int16);
                    com.Parameters["@PlaceOfStore"].Value = 14;
                    com.Parameters.Add("@TotalOfMeters", MySqlDbType.Decimal);
                    com.Parameters["@TotalOfMeters"].Value = totalMetre;
                    com.Parameters.Add("@Note", MySqlDbType.VarChar);
                    com.Parameters["@Note"].Value = "مرتجع عميل";
                    com.ExecuteNonQuery();
            //    }
            //}
            //dr.Close();
            //MessageBox.Show(str.Count.ToString());
            dbconnection.Close();
            dbconnection1.Close();
        }
    }
}

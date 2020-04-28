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

namespace MainSystem.Store.Storage.الجرد
{
    public partial class Form1 : Form
    {
        MySqlConnection dbconnection;
        public Form1()
        {
            InitializeComponent();
            dbconnection = new MySqlConnection(connection.connectionString);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dbconnection.Open();
            string query = "select data.Code as 'الكود',type.Type_Name as 'النوع',concat(product.Product_Name,' ',COALESCE(color.Color_Name,''),' ',data.Description,' ',groupo.Group_Name,' ',factory.Factory_Name,' ',COALESCE(size.Size_Value,''),' ',COALESCE(sort.Sort_Value,'')) as 'الاسم', storage.Total_Meters as 'الكمية',store.Store_Name as 'المخزن' from storage inner join store on store.Store_ID=storage.Store_ID inner join data on storage.Data_ID=data.Data_ID INNER JOIN type ON data.Type_ID = type.Type_ID INNER JOIN product ON product.Product_ID = data.Product_ID INNER JOIN factory ON factory.Factory_ID = data.Factory_ID INNER JOIN groupo ON groupo.Group_ID = data.Group_ID LEFT JOIN color ON data.Color_ID = color.Color_ID LEFT JOIN size ON data.Size_ID = size.Size_ID LEFT JOIN sort ON data.Sort_ID = sort.Sort_ID where storage.Data_ID in (select distinct Data_ID from table11 where Store_ID=2) and storage.Store_ID=2;";
            MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
            DataTable dt = new DataTable();
            da.Fill(dt);
            gridControl2.DataSource = dt;
            dbconnection.Close();
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            try
            {
                Product_Report objForm = new Product_Report(gridControl2, "");
            
            
                objForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                objForm.Dock = DockStyle.Fill;
                objForm.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}

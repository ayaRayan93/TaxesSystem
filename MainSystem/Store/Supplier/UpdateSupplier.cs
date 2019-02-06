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
    public partial class UpdateSupplier : Form
    {
        MySqlConnection dbconnection;
        bool loaded = false;
        public UpdateSupplier()
        {
            InitializeComponent();
            dbconnection = new MySqlConnection(connection.connectionString);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                dbconnection.Open();
                suppliersListCombobox();
                loaded = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }
        private void btnDisplayAll_Click(object sender, EventArgs e)
        {
            try
            {
                if (loaded)
                {
                    dbconnection.Close();
                    dbconnection.Open();
                    //display all Supplier data
                    allSupplierData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }
        private void cbSuppliers_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (loaded)
                {
                    dbconnection.Close();
                    dbconnection.Open();
                    //display Supplier data
                    supplierData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }
        //functions
        //assign suppliers names to combobox 
        public void suppliersListCombobox()
        {
            string query = "select * from supplier";
            MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
            DataTable dt = new DataTable();
            da.Fill(dt);
            cbSuppliers.DataSource = dt;
            cbSuppliers.DisplayMember = dt.Columns["Supplier_Name"].ToString();
            cbSuppliers.ValueMember = dt.Columns["Supplier_ID"].ToString();
            cbSuppliers.Text = "";
        }
        //display data of specific supplier
        public void supplierData()
        {
            string query = "select * from supplier where Supplier_ID="+cbSuppliers.SelectedValue+"";
            MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }
        //display data of all suppliers
        public void allSupplierData()
        {
            string query = "select Supplier_Name as 'الاسم',Supplier_Address as 'العنوان',Supplier_Phone as'التلفون',Supplier_Fax as 'فاكس',Supplier_Mail as 'البريد الالكتروني',Supplier_Debt as'رصيد افتتاحي الي',Supplier_Credit as'رصيد افتتاحي من',Supplier_Start as 'تاريخ بدء العمل', from supplier";
            MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }

    }
}

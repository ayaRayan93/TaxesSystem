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
    public partial class Branch_Record : Form
    {
        MySqlConnection conn;
        XtraTabControl tabControlBranch;

        public Branch_Record(Branch_Report form, XtraTabControl MainTabControlBranch)
        {
            InitializeComponent();
            conn = new MySqlConnection(connection.connectionString);
            tabControlBranch = MainTabControlBranch;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtName.Text != "")
                {
                    string query = "select Branch_Name from branch where Branch_Name='" + txtName.Text + "'";
                    MySqlCommand com = new MySqlCommand(query, conn);
                    conn.Open();

                    if (com.ExecuteScalar() == null)
                    {
                        conn.Close();
                        MySqlCommand command = conn.CreateCommand();
                        command.CommandText = "INSERT INTO branch (Branch_Name,Branch_Address,Branch_Phone,Branch_Mail,Branch_Fax,Postal_Code) VALUES (?Branch_Name,?Branch_Address,?Branch_Phone,?Branch_Mail,?Branch_Fax,?Postal_Code)";
                        command.Parameters.AddWithValue("?Branch_Name", txtName.Text);
                        command.Parameters.AddWithValue("?Branch_Address", txtAddress.Text);
                        command.Parameters.AddWithValue("?Branch_Phone", txtPhone.Text);
                        command.Parameters.AddWithValue("?Branch_Mail", txtEmail.Text);
                        command.Parameters.AddWithValue("?Branch_Fax", txtFax.Text);
                        command.Parameters.AddWithValue("?Postal_Code", txtPostalCode.Text);
                        conn.Open();
                        command.ExecuteNonQuery();
                        conn.Close();
                        txtName.Text = "";
                        txtAddress.Text = "";
                        txtPhone.Text = "";
                        txtEmail.Text = "";
                        txtFax.Text = "";
                        txtPostalCode.Text = "";
                        MessageBox.Show("تمت الاضافة");

                        //Branch_Report bReport = new Branch_Report();
                        //bReport.search();
                        
                    }
                    else
                    {
                        MessageBox.Show("هذا الفرع تمت اضافتة من قبل");
                    }
                }
                else
                {
                    MessageBox.Show("برجاء ادخال جميع البيانات المطلوبة");
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            conn.Close();
        }

        private void text_TextChanged(object sender, EventArgs e)
        {
            try
            {
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }

}

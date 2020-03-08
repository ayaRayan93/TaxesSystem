using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data;
using System.IO;
using MySql.Data.MySqlClient;
using DevExpress.XtraTab;
using System.Reflection;

namespace MainSystem
{
    public partial class Bank_Main : Form
    {
        MySqlConnection dbconnection;

        public Bank_Main()
        {
            try
            {
                InitializeComponent();
                dbconnection = new MySqlConnection(connection.connectionString);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
     
        private void AdminUserRecord_Load(object sender, EventArgs e)
        {
        }
        
        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtName.Text != "" && comType.Text != "")
                {
                    dbconnection.Open();
                    string q = "select MainBank_ID from bank_main where MainBank_Name='" + txtName.Text + "'";
                    MySqlCommand com = new MySqlCommand(q, dbconnection);
                    if (com.ExecuteScalar() == null)
                    {
                        string query = "INSERT INTO bank_main (MainBank_Name,MainBank_Type) VALUES (@MainBank_Name,@MainBank_Type)";
                        MySqlCommand cmd = new MySqlCommand(query, dbconnection);
                        cmd.Parameters.Add("@MainBank_Name", MySqlDbType.VarChar, 255);
                        cmd.Parameters["@MainBank_Name"].Value = txtName.Text;
                        cmd.Parameters.Add("@MainBank_Type", MySqlDbType.VarChar, 255);
                        cmd.Parameters["@MainBank_Type"].Value = comType.Text;
                        cmd.ExecuteNonQuery();

                        comType.SelectedIndex = -1;
                        txtName.Text = "";
                    }
                    else
                    {
                        MessageBox.Show("هذا الاسم موجود من قبل");
                    }
                }
                else
                {
                    MessageBox.Show("تاكد من البيانات");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }
        
        //functions
        public void clear()
        {
            foreach (Control item in this.Controls)
            {
                if (item is TextBox)
                {
                    item.Text = "";
                }
                else if (item is ComboBox)
                {
                    item.Text = "";
                }
            }
        }

        private bool IsClear()
        {
            foreach (Control item in this.Controls)
            {
                if (item is TextBox)
                {
                    if (item.Text != "" )
                        if(item.Text != "0")
                            return false;
                }
                else if (item is ComboBox)
                {
                    if (item.Text != "")
                        return false;
                }
                else if (item is DateTimePicker)
                {
                    DateTimePicker item1 = (DateTimePicker)item;
                    if (item1.Value.Date != DateTime.Now.Date)
                        return false;
                }
            }
          
            return true;
        }
    }
}

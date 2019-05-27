using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MainSystem
{
    public partial class ChangeIP : Form
    {
        MySqlConnection dbconnection;
        public ChangeIP()
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

        private void ChangeIP_Load(object sender, EventArgs e)
        {
            try
            {
                dbconnection.Open();
                string query = "select * from branch";
                MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                DataTable dt = new DataTable();
                da.Fill(dt);
                comBranchName.DataSource = dt;
                comBranchName.DisplayMember = dt.Columns["Branch_Name"].ToString();
                comBranchName.ValueMember = dt.Columns["Branch_ID"].ToString();
                string path = "C:\\Users\\User\\Documents\\MainSystem";
                string BranchID = "1";
                string IPAddress = "192.168.1.200";
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                    using (StreamWriter writer = new StreamWriter(path + "\\Branch.txt"))
                    {
                        writer.WriteLine(BranchID);
                    }
                    using (StreamWriter writer = new StreamWriter(path + "\\IP_Address.txt"))
                    {
                        writer.WriteLine(IPAddress);
                    }
                }
                else
                {
                    BranchID = File.ReadAllText(path+"\\Branch.txt");
                    IPAddress = File.ReadAllText(path+"\\IP_Address.txt");
                }

                labOldIP.Text =IPAddress;            
                query = "select Branch_Name from branch where Branch_ID=" + BranchID;
                MySqlCommand com = new MySqlCommand(query, dbconnection);
                comBranchName.Text = com.ExecuteScalar().ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }

        private void btnTestConnection_Click(object sender, EventArgs e)
        {
            string connectionString = "SERVER=" + txtNewIP.Text + ";DATABASE=testPrice;user=root;PASSWORD=root;CHARSET=utf8;SslMode=none";
           
            MySqlConnection dbconnection = new MySqlConnection(connectionString);
            try
            {
                dbconnection.Open();
                pictureBoxCheckConnection.Image = Properties.Resources.icons8_Checkmark_48px;
            }
            catch
            {
                pictureBoxCheckConnection.Image = Properties.Resources.icons8_Delete_48px;
            }
            dbconnection.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                //Write to a file
                using (StreamWriter writer = new StreamWriter("C:\\Users\\User\\Documents\\MainSystem\\Branch.txt"))
                {
                    writer.WriteLine(comBranchName.SelectedValue);
                }
                if (txtNewIP.Text != "")
                {
                    using (StreamWriter writer = new StreamWriter("C:\\Users\\User\\Documents\\MainSystem\\IP_Address.txt"))
                    {
                        writer.WriteLine(txtNewIP.Text);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}

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
        string oldBranch = "";
        bool testFlag = false;
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
              
                string BranchID = "1";
                string IPAddress = "192.168.1.200";
                if (!File.Exists("Branch.txt"))
                {
                    using (StreamWriter writer = new StreamWriter("Branch.txt"))
                    {
                        writer.WriteLine(BranchID);
                    }
                    using (StreamWriter writer = new StreamWriter("IP_Address.txt"))
                    {
                        writer.WriteLine(IPAddress);
                    }
                }
                else
                {
                    BranchID = File.ReadAllText("Branch.txt");
                    IPAddress = File.ReadAllText("IP_Address.txt");
                }

                labOldIP.Text =IPAddress;            
                query = "select Branch_Name from branch where Branch_ID=" + BranchID;
                MySqlCommand com = new MySqlCommand(query, dbconnection);
                comBranchName.Text = com.ExecuteScalar().ToString();
                oldBranch = comBranchName.Text;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }

        private void btnTestConnection_Click(object sender, EventArgs e)
        {
            string connectionString = "SERVER=" + txtNewIP.Text + ";DATABASE=cccmaindb;user=root;PASSWORD=root;CHARSET=utf8;SslMode=none";
           
            MySqlConnection dbconnection = new MySqlConnection(connectionString);
            try
            {
                dbconnection.Open();
                pictureBoxCheckConnection.Image = Properties.Resources.icons8_Checkmark_48px;
                testFlag = true;
            }
            catch
            {
                pictureBoxCheckConnection.Image = Properties.Resources.icons8_Delete_48px;
                testFlag = false;
            }
            dbconnection.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtNewIP.Text != "")
                {
                    if (testFlag)
                    {
                        //Write to a file
                        using (StreamWriter writer = new StreamWriter("Branch.txt"))
                        {
                            writer.WriteLine(comBranchName.SelectedValue);
                        }

                        using (StreamWriter writer = new StreamWriter("IP_Address.txt"))
                        {
                            writer.WriteLine(txtNewIP.Text);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Test Connection First");
                    }
                }
                else
                {
                    if (comBranchName.Text != oldBranch)
                    {
                        using (StreamWriter writer = new StreamWriter("Branch.txt"))
                        {
                            writer.WriteLine(comBranchName.SelectedValue);
                        }
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

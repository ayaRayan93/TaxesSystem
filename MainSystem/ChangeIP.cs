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

namespace TaxesSystem
{
    public partial class ChangeIP : Form
    {
        MySqlConnection dbconnection;
        string oldBranch = "";
        string oldStore = "";
        bool testFlag = false;
        public ChangeIP()
        {
            try
            {
                InitializeComponent();
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
                if (File.Exists("IP_Address.txt"))
                {
                    dbconnection = new MySqlConnection(connection.connectionString);
                    dbconnection.Open();
                    string query = "select * from branch";
                    MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    comBranchName.DataSource = dt;
                    comBranchName.DisplayMember = dt.Columns["Branch_Name"].ToString();
                    comBranchName.ValueMember = dt.Columns["Branch_ID"].ToString();

                    query = "select * from store";
                    da = new MySqlDataAdapter(query, dbconnection);
                    dt = new DataTable();
                    da.Fill(dt);
                    comStore.DataSource = dt;
                    comStore.DisplayMember = dt.Columns["Store_Name"].ToString();
                    comStore.ValueMember = dt.Columns["Store_ID"].ToString();

                    string BranchID = File.ReadAllText("Branch.txt");
                    string StoreID = File.ReadAllText("Store.txt");
                    string IPAddress = File.ReadAllText("IP_Address.txt");
                    labOldIP.Text = IPAddress;
                    query = "select Branch_Name from branch where Branch_ID=" + BranchID;
                    MySqlCommand com = new MySqlCommand(query, dbconnection);
                    comBranchName.Text = com.ExecuteScalar().ToString();
                    oldBranch = comBranchName.Text;
                    query = "select Store_Name from store where Store_ID=" + StoreID;
                    com = new MySqlCommand(query, dbconnection);
                    comStore.Text = com.ExecuteScalar().ToString();
                    oldStore = comStore.Text;
                    dbconnection.Close();
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
           
        }

        /*private void textBox_Click(object sender, EventArgs e)
        {
            openOnScreenKeyboard();
        }

        private void textBox_Leave(object sender, EventArgs e)
        {
            killOnScreenKeyboard();
        }

        private static void openOnScreenKeyboard()
        {
            System.Diagnostics.Process.Start("C:\\Program Files\\Common Files\\Microsoft shared\\ink\\TabTip.exe");

        }
        private static void killOnScreenKeyboard()
        {
            if (System.Diagnostics.Process.GetProcessesByName("TabTip").Count() > 0)
            {
                System.Diagnostics.Process asd = System.Diagnostics.Process.GetProcessesByName("TabTip").First();
                asd.Kill();
            }

        }*/

        private void btnTestConnection_Click(object sender, EventArgs e)
        {
            try
            {
                string connectionString = "SERVER=" + txtNewIP.Text + ";DATABASE=cccmaindb;user=root;PASSWORD=A!S#D37;CHARSET=utf8;SslMode=none";
           
                MySqlConnection dbconnection = new MySqlConnection(connectionString);
                try
                {
                    dbconnection.Open();
                    pictureBoxCheckConnection.Image = Properties.Resources.icons8_Checkmark_48px;
                    testFlag = true;
                    string query = "select * from branch";
                    MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    comBranchName.DataSource = dt;
                    comBranchName.DisplayMember = dt.Columns["Branch_Name"].ToString();
                    comBranchName.ValueMember = dt.Columns["Branch_ID"].ToString();

                    query = "select * from store";
                    da = new MySqlDataAdapter(query, dbconnection);
                    dt = new DataTable();
                    da.Fill(dt);
                    comStore.DataSource = dt;
                    comStore.DisplayMember = dt.Columns["Store_Name"].ToString();
                    comStore.ValueMember = dt.Columns["Store_ID"].ToString();
                }
                catch
                {
                    pictureBoxCheckConnection.Image = Properties.Resources.icons8_Delete_48px;
                    testFlag = false;
                }
                BaseData.connStatus = testFlag;
                dbconnection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
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

                        using (StreamWriter writer = new StreamWriter("Store.txt"))
                        {
                            writer.WriteLine(comStore.SelectedValue);
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
                    if (comStore.Text != oldStore)
                    {
                        using (StreamWriter writer = new StreamWriter("Store.txt"))
                        {
                            writer.WriteLine(comStore.SelectedValue);
                        }
                    }
                }
                BaseData.connStatus = testFlag;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ChangeIP_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                BaseData.connStatus = testFlag;
                Environment.Exit(0);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}

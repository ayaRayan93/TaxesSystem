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
         
                labOldIP.Text = baseData.IPAddress;
             
                query = "select Branch_Name from branch where Branch_ID=" + baseData.BranchID;
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
                string fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Branch.txt");
                using (System.IO.StreamWriter file = new System.IO.StreamWriter(fileName))
                {
                    //if the file doesn't exist, create it
                    if (!File.Exists(fileName))
                        File.Create(fileName);

                    file.Write(comBranchName.SelectedValue.ToString());
                }
                baseData.BranchID = (int)comBranchName.SelectedValue;
                if (txtNewIP.Text != "")
                    baseData.IPAddress = txtNewIP.Text;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}

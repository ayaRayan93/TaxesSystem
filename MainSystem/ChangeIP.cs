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
         
                string supString = Properties.Resources.IP_Address;
                labOldIP.Text = supString;
                int branchID =Convert.ToInt16(Properties.Resources.Branch);
                query = "select Branch_Name from branch where Branch_ID=" + branchID;
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
            catch (Exception ex)
            {
                pictureBoxCheckConnection.Image = Properties.Resources.icons8_Delete_48px;
            }
            dbconnection.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                ////wright on ipAddtress file
                string filename = "IP_Address.txt";
                if (System.IO.File.Exists(filename))

                    System.IO.File.WriteAllText(filename, txtNewIP.Text);

                filename = "Branch.txt";
                if (System.IO.File.Exists(filename))
                    System.IO.File.WriteAllText(filename, comBranchName.SelectedValue.ToString());

                string x = Properties.Resources.Branch.Insert(0, comBranchName.SelectedValue.ToString());

                using (StreamWriter file = new StreamWriter(filename))
                {
                    //if the file doesn't exist, create it
                    if (File.Exists(filename))
                        // File.WriteAllText();
                        file.Write(comBranchName.SelectedValue.ToString());
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}

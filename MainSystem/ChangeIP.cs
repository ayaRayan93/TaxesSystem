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
    public partial class ChangeIP : Form
    {
        public ChangeIP()
        {
            InitializeComponent();
        }

        private void ChangeIP_Load(object sender, EventArgs e)
        {
            try
            {
                string supString = Properties.Resources.IP_Address;
                labOldIP.Text = supString;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
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
                //wright on ipAddtress file
                string filename = "IP_Address.txt";
                if (!System.IO.File.Exists(filename))
                    System.IO.File.WriteAllText(filename,txtNewIP.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}

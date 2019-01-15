using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MainSystem
{
    public partial class PermissionImage : Form
    {
        int permissionNumber = 0;
        int supplierPermissionNumber = 0;
        string type = "";
        MySqlConnection dbconnection;
        byte[] bin;
        int bufferSize = 1024;
        MemoryStream ms = null;
        Image image = null;

        public PermissionImage(int PermissionNumber, int SupplierPermissionNumber, string Type)
        {
            InitializeComponent();
            dbconnection = new MySqlConnection(connection.connectionString);
            bin = new byte[bufferSize];
            image = null;
            permissionNumber = PermissionNumber;
            supplierPermissionNumber = SupplierPermissionNumber;
            type = Type;
        }

        private void PermissionImage_Load(object sender, EventArgs e)
        {
            try
            {
                pictureBox1.SizeMode = PictureBoxSizeMode.CenterImage;
                pictureBox1.Image = Properties.Resources.Back_32;
                
                Thread t1 = new Thread(displayImage);
                t1.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void displayImage()
        {
            try
            {
                dbconnection.Open();

                string query = "select Permission_Image from gate_permission where Permission_Number=" + permissionNumber + " and Type='" + type + "' and Supplier_PermissionNumber=" + supplierPermissionNumber;
                MySqlCommand com = new MySqlCommand(query, dbconnection);
                
                if (com.ExecuteScalar() != null)
                {
                    bin = (byte[])com.ExecuteScalar();

                    ms = new MemoryStream(bin);
                    image = Image.FromStream(ms);
                    pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                    pictureBox1.Image = image;
                }
                else
                {
                    pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                    pictureBox1.Image = Properties.Resources.Delete_52px;
                }
            
            }
            catch (Exception ex)
            {
                // MessageBox.Show(ex.Message);
                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                pictureBox1.Image = Properties.Resources.Delete_52px;
               
            }
            dbconnection.Close();
        }
    }
}

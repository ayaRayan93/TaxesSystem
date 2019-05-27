using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using MySql.Data.MySqlClient;
using System.IO;
using System.Drawing.Imaging;

namespace MainSystem
{
    public partial class AddSpecialOrderScanner : DevExpress.XtraEditors.XtraForm
    {
        MySqlConnection dbconnection;
        byte[] selectedRequestImage = null;
        byte[] selectedProductImage = null;
        int SpecialOrderID = 0;
        SpecialOrderConfirm specialOrderConfirm;

        public AddSpecialOrderScanner(SpecialOrderConfirm SpecialOrderConfirm, int specialOrderID)
        {
            InitializeComponent();
            dbconnection = new MySqlConnection(connection.connectionString);
            SpecialOrderID = specialOrderID;
            specialOrderConfirm = SpecialOrderConfirm;
        }

        private void AddSpecialOrderScanner_Load(object sender, EventArgs e)
        {
            try
            {
                dbconnection.Open();
                string query = "select special_order.Picture,special_order.Product_Picture,special_order.Description FROM special_order where SpecialOrder_ID=" + SpecialOrderID;
                MySqlCommand adapter = new MySqlCommand(query, dbconnection);
                MySqlDataReader dr = adapter.ExecuteReader();
                
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        txtDescription.Text = dr["Description"].ToString();
                        byte[] img = null;
                        if(dr["Picture"].ToString() != "")
                        {
                            img = (byte[])dr["Picture"];
                            selectedRequestImage = img;
                            pictureBoxRequest.Image = byteArrayToImage(img);
                            pictureBoxRequest.SizeMode = PictureBoxSizeMode.StretchImage;
                        }
                        byte[] imgProduct = null;
                        if (dr["Product_Picture"].ToString() != "")
                        {
                            imgProduct = (byte[])dr["Product_Picture"];
                            selectedProductImage = imgProduct;
                            layoutControlItemProductPicture.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                            labelRemoveProductImage.Image = MainSystem.Properties.Resources.closeIcon;
                            pictureBoxProduct.Image = byteArrayToImage(imgProduct);
                            pictureBoxProduct.SizeMode = PictureBoxSizeMode.StretchImage;
                        }
                    }
                }
                dr.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtDescription.Text != "" && pictureBoxRequest.Image != null)
                {
                    dbconnection.Open();
                    
                    string query = "update special_order set Picture=@Picture,Product_Picture=@Product_Picture,Description=@Description where SpecialOrder_ID=" + SpecialOrderID;
                    MySqlCommand com = new MySqlCommand(query, dbconnection);
                    if (pictureBoxRequest.Image != null)
                    {
                        com.Parameters.Add("@Picture", MySqlDbType.LongBlob, 0).Value = selectedRequestImage;
                    }
                    else
                    {
                        com.Parameters.Add("@Picture", MySqlDbType.LongBlob, 0).Value = null;
                    }
                    if (selectedProductImage != null)
                    {
                        com.Parameters.Add("@Product_Picture", MySqlDbType.LongBlob, 0).Value = selectedProductImage;
                    }
                    else
                    {
                        com.Parameters.Add("@Product_Picture", MySqlDbType.LongBlob, 0).Value = null;
                    }
                    com.Parameters.Add("@Description", MySqlDbType.VarChar, 255).Value = txtDescription.Text;
                    com.ExecuteNonQuery();
                    
                    dbconnection.Close();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("يجب ادخال صورة الطلب وادخال التفاصيل");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (layoutControlItemProductPicture.Visibility == DevExpress.XtraLayout.Utils.LayoutVisibility.Never)
            {
                layoutControlItemProductPicture.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                labelRemoveProductImage.Image = MainSystem.Properties.Resources.closeIcon;
            }
            else
            {
                layoutControlItemProductPicture.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                labelRemoveProductImage.Image = null;
            }
        }

        private void pictureBoxRequest_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog1 = new OpenFileDialog();
                //openFileDialog1.Filter = ("Image Files |*.png; *.bmp; *.jpg;*.jpeg; *.gif;");
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    string selectedFile = openFileDialog1.FileName;
                    selectedRequestImage = File.ReadAllBytes(selectedFile);
                    pictureBoxRequest.Image = Image.FromFile(openFileDialog1.FileName);
                    pictureBoxRequest.SizeMode = PictureBoxSizeMode.StretchImage;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void pictureBoxProduct_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog1 = new OpenFileDialog();
                //openFileDialog1.Filter = ("Image Files |*.png; *.bmp; *.jpg;*.jpeg; *.gif;");
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    string selectedFile = openFileDialog1.FileName;
                    selectedProductImage = File.ReadAllBytes(selectedFile);
                    pictureBoxProduct.Image = Image.FromFile(openFileDialog1.FileName);
                    pictureBoxProduct.SizeMode = PictureBoxSizeMode.StretchImage;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void labelRemoveRequestImage_Click(object sender, EventArgs e)
        {
            try
            {
                selectedRequestImage = null;
                pictureBoxRequest.Image = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void labelRemoveProductImage_Click(object sender, EventArgs e)
        {
            try
            {
                if (labelRemoveProductImage.Image != null)
                {
                    selectedProductImage = null;
                    pictureBoxProduct.Image = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void AddSpecialOrderScanner_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                specialOrderConfirm.search();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public Image byteArrayToImage(byte[] byteArrayIn)
        {
            MemoryStream ms = new MemoryStream(byteArrayIn);
            Image returnImage = Image.FromStream(ms);
            return returnImage;
        }
    }
}
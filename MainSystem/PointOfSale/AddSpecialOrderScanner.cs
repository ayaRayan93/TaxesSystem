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
        int DashBillNum = 0;
        int EmpBranchId = 0;
        int branchBillNumber = 0;
        int DelegateId = 0;
        int ClientId = 0;
        byte[] selectedRequestImage = null;
        byte[] selectedProductImage = null;

        public AddSpecialOrderScanner(int dashBillNum, int empBranchId, int delegateId, int clientId)
        {
            InitializeComponent();
            dbconnection = new MySqlConnection(connection.connectionString);
            DashBillNum = dashBillNum;
            EmpBranchId = empBranchId;
            DelegateId = delegateId;
            ClientId = clientId;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (pictureBoxRequest.Image != null)
                {
                    //Image image = pictureBoxRequest.Image;
                    //MemoryStream memoryStream = new MemoryStream();
                    //image.Save(memoryStream, ImageFormat.Png);
                    //byte[] imageBt = memoryStream.ToArray();

                    dbconnection.Open();
                    string query = "insert into special_order (Picture,Product_Picture,Dash_ID,Description) values(@Picture,@Product_Picture,@Dash_ID,@Description)";
                    MySqlCommand com = new MySqlCommand(query, dbconnection);
                    com.Parameters.Add("@Picture", MySqlDbType.LongBlob, 0).Value = selectedRequestImage;
                    if (selectedProductImage != null)
                    {
                        com.Parameters.Add("@Product_Picture", MySqlDbType.LongBlob, 0).Value = selectedProductImage;
                    }
                    else
                    {
                        com.Parameters.Add("@Product_Picture", MySqlDbType.LongBlob, 0).Value = null;
                    }
                    com.Parameters.Add("@Dash_ID", MySqlDbType.Int16, 11).Value = DashBillNum;
                    com.Parameters.Add("@Description", MySqlDbType.VarChar, 255).Value = txtDescription.Text;
                    com.ExecuteNonQuery();
                    insertRequest();
                    dbconnection.Close();
                    MessageBox.Show("طلب رقم : " + branchBillNumber.ToString());
                    this.Close();
                }
                else
                {
                    MessageBox.Show("يجب ادخال صورة الطلب");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }

        //to get request number and give it to the client
        public void insertRequest()
        {
            int SpecialOrderID = 0;
            string cutomerType = "";

            string query = "select BranchBillNumber from requests where Branch_ID=" + EmpBranchId + " order by Request_ID desc limit 1";
            MySqlCommand com = new MySqlCommand(query, dbconnection);
            if (com.ExecuteScalar() != null)
            {
                branchBillNumber = Convert.ToInt16(com.ExecuteScalar()) + 1;
            }
            else
            {
                branchBillNumber = 1;
            }

            query = "select SpecialOrder_ID from special_order order by SpecialOrder_ID desc limit 1";
            com = new MySqlCommand(query, dbconnection);
            if (com.ExecuteScalar() != null)
            {
                SpecialOrderID = Convert.ToInt16(com.ExecuteScalar().ToString());
            }

            query = "select Customer_Type from customer where Customer_ID=" + ClientId;
            com = new MySqlCommand(query, dbconnection);
            if (com.ExecuteScalar() != null)
            {
                cutomerType = com.ExecuteScalar().ToString();
            }

            /*query = "select Branch_Name from Branch where Branch_ID=" + EmpBranchId;
            com = new MySqlCommand(query, dbconnection);
            string BranchName = com.ExecuteScalar().ToString();*/

            if (cutomerType == "عميل")
            {
                query = "insert into requests (Branch_ID,BranchBillNumber,SpecialOrder_ID,Delegate_ID,Client_ID) values (@Branch_ID,@BranchBillNumber,@SpecialOrder_ID,@Delegate_ID,@Client_ID)";
                com = new MySqlCommand(query, dbconnection);
                com.Parameters.Add("@Branch_ID", MySqlDbType.Int16);
                com.Parameters["@Branch_ID"].Value = EmpBranchId;
                //com.Parameters.Add("@Branch_Name", MySqlDbType.VarChar);
                //com.Parameters["@Branch_Name"].Value = BranchName;
                com.Parameters.Add("@BranchBillNumber", MySqlDbType.Int16);
                com.Parameters["@BranchBillNumber"].Value = branchBillNumber;
                com.Parameters.Add("@SpecialOrder_ID", MySqlDbType.Int16);
                com.Parameters["@SpecialOrder_ID"].Value = SpecialOrderID;
                com.Parameters.Add("@Delegate_ID", MySqlDbType.Int16);
                com.Parameters["@Delegate_ID"].Value = DelegateId;
                com.Parameters.Add("@Client_ID", MySqlDbType.Int16);
                com.Parameters["@Client_ID"].Value = ClientId;
                com.ExecuteNonQuery();
            }
            else
            {
                query = "insert into requests (Branch_ID,BranchBillNumber,SpecialOrder_ID,Delegate_ID,Customer_ID) values (@Branch_ID,@BranchBillNumber,@SpecialOrder_ID,@Delegate_ID,@Customer_ID)";
                com = new MySqlCommand(query, dbconnection);
                com.Parameters.Add("@Branch_ID", MySqlDbType.Int16);
                com.Parameters["@Branch_ID"].Value = EmpBranchId;
                com.Parameters.Add("@BranchBillNumber", MySqlDbType.Int16);
                com.Parameters["@BranchBillNumber"].Value = branchBillNumber;
                com.Parameters.Add("@SpecialOrder_ID", MySqlDbType.Int16);
                com.Parameters["@SpecialOrder_ID"].Value = SpecialOrderID;
                com.Parameters.Add("@Delegate_ID", MySqlDbType.Int16);
                com.Parameters["@Delegate_ID"].Value = DelegateId;
                com.Parameters.Add("@Customer_ID", MySqlDbType.Int16);
                com.Parameters["@Customer_ID"].Value = ClientId;
                com.ExecuteNonQuery();
            }
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
    }
}
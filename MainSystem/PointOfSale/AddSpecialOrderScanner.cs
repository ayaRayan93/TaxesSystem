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

        public AddSpecialOrderScanner(int dashBillNum, int empBranchId, int delegateId)
        {
            InitializeComponent();
            dbconnection = new MySqlConnection(connection.connectionString);
            DashBillNum = dashBillNum;
            EmpBranchId = empBranchId;
            DelegateId = delegateId;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (pictureBox1.Image != null)
                {
                    Image image = pictureBox1.Image;
                    MemoryStream memoryStream = new MemoryStream();
                    image.Save(memoryStream, ImageFormat.Png);
                    byte[] imageBt = memoryStream.ToArray();

                    dbconnection.Open();
                    string query = "insert into special_order (Picture,Dash_ID) values(@Picture,@Dash_ID)";
                    MySqlCommand com = new MySqlCommand(query, dbconnection);
                    com.Parameters.Add("@Picture", MySqlDbType.LongBlob, 0).Value = imageBt;
                    com.Parameters.Add("@Dash_ID", MySqlDbType.Int16, 11).Value = DashBillNum;
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
                SpecialOrderID = Convert.ToInt16(com.ExecuteScalar());
            }

            /*query = "select Branch_Name from Branch where Branch_ID=" + EmpBranchId;
            com = new MySqlCommand(query, dbconnection);
            string BranchName = com.ExecuteScalar().ToString();*/

            //,Client_ID  ,@Client_ID
            query = "insert into requests (Branch_ID,BranchBillNumber,SpecialOrder_ID,Delegate_ID) values (@Branch_ID,@BranchBillNumber,@SpecialOrder_ID,@Delegate_ID)";
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
            com.ExecuteNonQuery();
        }

        private void btnLoadPicture_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = ("Image Files |*.png; *.bmp; *.jpg;*.jpeg; *.gif;");

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image = Image.FromFile(openFileDialog1.FileName);
                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            }
        }
    }
}
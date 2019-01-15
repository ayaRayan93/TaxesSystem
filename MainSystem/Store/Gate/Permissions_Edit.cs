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
using System.Collections;
using MySql.Data.MySqlClient;
using System.IO;

namespace MainSystem
{
    public partial class Permissions_Edit : Form
    {
        MySqlConnection conn;
        int permissionNum = 0;
        Gate_Out gateOut;

        public Permissions_Edit(Gate_Out GateOut, int PermissionNum)
        {
            InitializeComponent();
            conn = new MySqlConnection(connection.connectionString);
            permissionNum = PermissionNum;
            gateOut = GateOut;
        }

        private void Permissions_Edit_Load(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                string query = "select gate_permission.Supplier_PermissionNumber from gate_permission where gate_permission.Permission_Number=" + permissionNum + " and gate_permission.Type='خروج'";
                MySqlCommand com = new MySqlCommand(query, conn);
                MySqlDataReader dr = com.ExecuteReader();
                while (dr.Read())
                {
                    checkedListBoxControlNum.Items.Add(dr[0].ToString());
                }
                dr.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            conn.Close();
        }

        private void btnAddNum_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtPermisionNum.Text != "")
                {
                    for (int i = 0; i < checkedListBoxControlNum.ItemCount; i++)
                    {
                        if (txtPermisionNum.Text == checkedListBoxControlNum.Items[i].Value.ToString())
                        {
                            MessageBox.Show("هذا الاذن تم اضافتة");
                            return;
                        }
                    }

                    OpenFileDialog openFileDialog1 = new OpenFileDialog();
                    if (openFileDialog1.ShowDialog() == DialogResult.OK)
                    {
                        string selectedFile = openFileDialog1.FileName;
                        byte[] selectedRequestImage = null;
                        selectedRequestImage = File.ReadAllBytes(selectedFile);
                        checkedListBoxControlNum.Items.Add(txtPermisionNum.Text);
                        imageListBoxControl1.Items.Add(selectedFile);
                        txtPermisionNum.Text = "";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnDeleteNum_Click(object sender, EventArgs e)
        {
            try
            {
                if (checkedListBoxControlNum.CheckedItemsCount > 0)
                {
                    if (MessageBox.Show("هل انت متاكد انك تريد الحذف؟", "", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                        return;

                    ArrayList temp = new ArrayList();
                    ArrayList tempimg = new ArrayList();
                    foreach (int index in checkedListBoxControlNum.CheckedIndices)
                    {
                        temp.Add(checkedListBoxControlNum.Items[index]);
                        tempimg.Add(imageListBoxControl1.Items[index]);
                    }
                    foreach (object item in temp)
                    {
                        checkedListBoxControlNum.Items.Remove(item);
                    }
                    foreach (object item in tempimg)
                    {
                        imageListBoxControl1.Items.Remove(item);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                string query = "delete from gate_permission where gate_permission.Permission_Number=" + permissionNum + " and gate_permission.Type='خروج'";
                MySqlCommand com = new MySqlCommand(query, conn);
                com.ExecuteNonQuery();

                for (int i = 0; i < checkedListBoxControlNum.ItemCount; i++)
                {
                    query = "insert into gate_permission(Permission_Number,Supplier_PermissionNumber,Type,Permission_Image) values(@Permission_Number,@Supplier_PermissionNumber,@Type,@Permission_Image)";
                    com = new MySqlCommand(query, conn);
                    com.Parameters.Add("@Permission_Number", MySqlDbType.Int16, 11);
                    com.Parameters["@Permission_Number"].Value = permissionNum;
                    com.Parameters.Add("@Supplier_PermissionNumber", MySqlDbType.Int16, 11);
                    com.Parameters["@Supplier_PermissionNumber"].Value = checkedListBoxControlNum.Items[i].Value.ToString();
                    com.Parameters.Add("@Type", MySqlDbType.VarChar, 255);
                    com.Parameters["@Type"].Value = "خروج";
                    com.Parameters.Add("@Permission_Image", MySqlDbType.LongBlob, 0);
                    com.Parameters["@Permission_Image"].Value = imageToByteArray(Image.FromFile(imageListBoxControl1.Items[i].Value.ToString()));
                    com.ExecuteNonQuery();
                }
                conn.Close();
                gateOut.search();
                this.Hide();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            conn.Close();
        }

        public byte[] imageToByteArray(System.Drawing.Image imageIn)
        {
            MemoryStream ms = new MemoryStream();
            imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
            return ms.ToArray();
        }
    }
}
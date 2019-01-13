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
                string query = "select transport_permission.Supplier_PermissionNumber from transport_permission where transport_permission.Permission_Number=" + permissionNum + " and transport_permission.Type='خروج'";
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

                    checkedListBoxControlNum.Items.Add(txtPermisionNum.Text);
                    txtPermisionNum.Text = "";
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
                    foreach (int index in checkedListBoxControlNum.CheckedIndices)
                        temp.Add(checkedListBoxControlNum.Items[index]);
                    foreach (object item in temp)
                        checkedListBoxControlNum.Items.Remove(item);
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
                string query = "delete from transport_permission where transport_permission.Permission_Number=" + permissionNum + " and transport_permission.Type='خروج'";
                MySqlCommand com = new MySqlCommand(query, conn);
                com.ExecuteNonQuery();

                for (int i = 0; i < checkedListBoxControlNum.ItemCount; i++)
                {
                    query = "insert into transport_permission(Permission_Number,Supplier_PermissionNumber,Type) values(@Permission_Number,@Supplier_PermissionNumber,@Type)";
                    com = new MySqlCommand(query, conn);
                    com.Parameters.Add("@Permission_Number", MySqlDbType.Int16, 11);
                    com.Parameters["@Permission_Number"].Value = permissionNum;
                    com.Parameters.Add("@Supplier_PermissionNumber", MySqlDbType.Int16, 11);
                    com.Parameters["@Supplier_PermissionNumber"].Value = checkedListBoxControlNum.Items[i].Value.ToString();
                    com.Parameters.Add("@Type", MySqlDbType.VarChar, 255);
                    com.Parameters["@Type"].Value = "خروج";
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
    }
}
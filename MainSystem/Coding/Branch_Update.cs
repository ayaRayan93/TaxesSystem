using DevExpress.XtraTab;
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
    public partial class Branch_Update : Form
    {
        MySqlConnection conn;
        DataRowView celRow;
        XtraTabControl tabControlBranch;
        XtraTabPage xtraTabPage;
        bool loaded = false;

        public Branch_Update(DataRowView CelRow, Branch_Report form, XtraTabControl MainTabControlBranch)
        {
            InitializeComponent();
            conn = new MySqlConnection(connection.connectionString);
            celRow = CelRow;
            tabControlBranch = MainTabControlBranch;
        }

        private void Branch_Update_Load(object sender, EventArgs e)
        {
            try
            {
                txtName.Text = celRow[1].ToString();
                txtAddress.Text = celRow[2].ToString();
                txtPhone.Text = celRow[3].ToString();
                txtEmail.Text = celRow[4].ToString();
                txtFax.Text = celRow[5].ToString();
                txtPostalCode.Text = celRow[6].ToString();
                loaded = true;
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
                if (txtName.Text != "")
                {
                    string query = "select Branch_Name from branch where Branch_ID!=" + celRow[0].ToString() + " and Branch_Name='" + txtName.Text + "'";
                    MySqlCommand com = new MySqlCommand(query, conn);
                    conn.Open();

                    if (com.ExecuteScalar() == null)
                    {
                        conn.Close();
                        MySqlCommand command = conn.CreateCommand();
                        command.CommandText = "update branch set Branch_Name=?Branch_Name,Branch_Address=?Branch_Address,Branch_Phone=?Branch_Phone,Branch_Mail=?Branch_Mail,Branch_Fax=?Branch_Fax,Postal_Code=?Postal_Code where Branch_ID=" + celRow[0].ToString();
                        command.Parameters.AddWithValue("?Branch_Name", txtName.Text);
                        command.Parameters.AddWithValue("?Branch_Address", txtAddress.Text);
                        command.Parameters.AddWithValue("?Branch_Phone", txtPhone.Text);
                        command.Parameters.AddWithValue("?Branch_Mail", txtEmail.Text);
                        command.Parameters.AddWithValue("?Branch_Fax", txtFax.Text);
                        command.Parameters.AddWithValue("?Postal_Code", txtPostalCode.Text);
                        conn.Open();
                        command.ExecuteNonQuery();
                        conn.Close();
                        //txtName.Text = "";
                        //txtAddress.Text = "";
                        //txtPhone.Text = "";
                        //txtEmail.Text = "";
                        //txtFax.Text = "";
                        //txtPostalCode.Text = "";

                        //Branch_Report bReport = new Branch_Report();
                        //bReport.search();
                        
                        xtraTabPage = getTabPage("تعديل فرع");
                        tabControlBranch.TabPages.Remove(xtraTabPage);
                    }
                    else
                    {
                        MessageBox.Show("هذا الفرع تمت اضافتة من قبل");
                    }
                }
                else
                {
                    MessageBox.Show("برجاء ادخال البيانات المطلوبة");
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            conn.Close();
        }

        private void text_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (loaded)
                {
                    xtraTabPage = getTabPage("تعديل فرع");
                    if (!IsClear())
                    {
                        xtraTabPage.ImageOptions.Image = Properties.Resources.unsave;
                    }
                    else
                    {
                        xtraTabPage.ImageOptions.Image = null;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public XtraTabPage getTabPage(string text)
        {
            for (int i = 0; i < tabControlBranch.TabPages.Count; i++)
                if (tabControlBranch.TabPages[i].Text == text)
                {
                    return tabControlBranch.TabPages[i];
                }
            return null;
        }

        public bool IsClear()
        {
            bool flag5 = false;
            foreach (Control co in this.panel1.Controls)
            {
                foreach (Control item in co.Controls)
                {
                    if (item is ComboBox)
                    {
                        if (item.Text == "")
                            flag5 = true;
                        else
                            return false;
                    }
                    else if (item is TextBox)
                    {
                        if (item.Text == "")
                            flag5 = true;
                        else
                            return false;
                    }
                }
            }

            return flag5;
        }
    }

}

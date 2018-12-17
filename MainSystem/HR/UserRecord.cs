using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data;
using System.IO;
using MySql.Data.MySqlClient;
using DevExpress.XtraTab;

namespace MainSystem
{
    public partial class UserRecord : Form
    {
        MySqlConnection dbconnection;
        bool load = false;
        MainForm HRMainForm;
        XtraTabControl xtraTabControlHRContent;

        public UserRecord(MainForm HrMainForm, XtraTabControl XtraTabControlHRContent)
        {
            try
            {
                InitializeComponent();
                dbconnection = new MySqlConnection(connection.connectionString);
                HRMainForm = HrMainForm;
                xtraTabControlHRContent = XtraTabControlHRContent;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
     
        private void UserRecord_Load(object sender, EventArgs e)
        {
            try
            {
                dbconnection.Open();

                string query = "SELECT Employee_Name,Employee_ID FROM employee";
                MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                DataTable dt = new DataTable();
                da.Fill(dt);
                comEmployee.DataSource = dt;
                comEmployee.DisplayMember = dt.Columns["Employee_Name"].ToString();
                comEmployee.ValueMember = dt.Columns["Employee_ID"].ToString();
                comEmployee.Text = "";

                query = "SELECT Department_Name,Department_ID FROM departments";
                da = new MySqlDataAdapter(query, dbconnection);
                dt = new DataTable();
                da.Fill(dt);
                comDepartment.DataSource = dt;
                comDepartment.DisplayMember = dt.Columns["Department_Name"].ToString();
                comDepartment.ValueMember = dt.Columns["Department_ID"].ToString();
                comDepartment.Text = "";

                load = true;
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
            dbconnection.Close();
        }

        private void rEmployee_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (rEmployee.Checked)
                {
                    dbconnection.Open();
                    string query = "SELECT Employee_Name,Employee_ID FROM employee";
                    MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    load = false;
                    comEmployee.DataSource = dt;
                    comEmployee.DisplayMember = dt.Columns["Employee_Name"].ToString();
                    comEmployee.ValueMember = dt.Columns["Employee_ID"].ToString();
                    comEmployee.Text = "";
                    load = true;
                    clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }

        private void rDelegate_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (rDelegate.Checked)
                {
                    dbconnection.Open();
                    string query = "SELECT Delegate_Name,Delegate_ID FROM delegate";
                    MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    load = false;
                    comEmployee.DataSource = dt;
                    comEmployee.DisplayMember = dt.Columns["Delegate_Name"].ToString();
                    comEmployee.ValueMember = dt.Columns["Delegate_ID"].ToString();
                    comEmployee.Text = "";
                    load = true;
                    clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }

        private void comEmployee_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (load)
                {
                    if (rEmployee.Checked == true)
                    {
                        dbconnection.Open();
                        string query = "SELECT Department_ID FROM employee where Employee_ID=" + comEmployee.SelectedValue.ToString();
                        MySqlCommand com = new MySqlCommand(query, dbconnection);
                        if (com.ExecuteScalar() != null && com.ExecuteScalar().ToString() != "")
                        {
                            comDepartment.SelectedValue = com.ExecuteScalar().ToString();
                        }
                        else
                        {
                            comDepartment.SelectedIndex = -1;
                        }
                    }
                    else if (rDelegate.Checked == true)
                    {
                        dbconnection.Open();
                        string query = "SELECT Department_ID FROM delegate where Delegate_ID=" + comEmployee.SelectedValue.ToString();
                        MySqlCommand com = new MySqlCommand(query, dbconnection);
                        if (com.ExecuteScalar() != null && com.ExecuteScalar().ToString() != "")
                        {
                            comDepartment.SelectedValue = com.ExecuteScalar().ToString();
                        }
                        else
                        {
                            comDepartment.SelectedIndex = -1;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtName.Text != "" && txtName.TextLength > 1 && txtPassword.Text != "" && txtPassword.TextLength > 2 && comEmployee.SelectedIndex != -1 && comDepartment.SelectedIndex != -1)
                {
                    dbconnection.Open();
                    string q = "select User_ID from users where User_Name='" + txtName.Text + "'";
                    MySqlCommand com = new MySqlCommand(q, dbconnection);
                    if (com.ExecuteScalar() == null)
                    {
                        string query = "INSERT INTO users (Employee_ID,User_Type,User_Name,Password) VALUES (@Employee_ID,@User_Type,@User_Name,@Password)";
                        MySqlCommand cmd = new MySqlCommand(query, dbconnection);
                        cmd.Parameters.Add("@Employee_ID", MySqlDbType.Int16, 11);
                        cmd.Parameters["@Employee_ID"].Value = Convert.ToInt16(comEmployee.SelectedValue);
                        cmd.Parameters.Add("@User_Type", MySqlDbType.Int16, 11);
                        cmd.Parameters["@User_Type"].Value = Convert.ToInt16(comDepartment.SelectedValue);
                        cmd.Parameters.Add("@User_Name", MySqlDbType.VarChar, 255);
                        cmd.Parameters["@User_Name"].Value = txtName.Text;
                        cmd.Parameters.Add("@Password", MySqlDbType.VarChar, 255);
                        cmd.Parameters["@Password"].Value = txtPassword.Text;

                        if (cmd.ExecuteNonQuery() == 1)
                        {
                            clear();
                        }
                    }
                    else
                    {
                        MessageBox.Show("هذا المستخدم موجود من قبل");
                    }
                }
                else
                {
                    MessageBox.Show("تاكد من البيانات");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }

        private void txtBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (load)
                {
                    if (sender is TextBox)
                    {
                        TextBox text = (TextBox)sender;
                        if (text.Name == "txtName")
                        {
                            if (txtName.TextLength > 0)
                            {
                                dbconnection.Open();
                                string q = "select User_ID from users where User_Name='" + txtName.Text + "'";
                                MySqlCommand com = new MySqlCommand(q, dbconnection);
                                if (com.ExecuteScalar() != null)
                                {
                                    txtName.BackColor = Color.PaleVioletRed;
                                }
                                else
                                {
                                    txtName.BackColor = Color.White;
                                }
                            }
                        }
                    }

                    XtraTabPage xtraTabPage = getTabPage("اضافة مستخدم");
                    if (!IsClear())
                        xtraTabPage.ImageOptions.Image = Properties.Resources.unsave__2_;
                    else
                        xtraTabPage.ImageOptions.Image = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }
        //functions
        public void clear()
        {
            foreach (Control item in this.Controls)
            {
                if (item is TextBox)
                {
                    item.Text = "";
                }
                else if (item is ComboBox)
                {
                    item.Text = "";
                }
            }
        }
        public XtraTabPage getTabPage(string text)
        {
            for (int i = 0; i < xtraTabControlHRContent.TabPages.Count; i++)
                if (xtraTabControlHRContent.TabPages[i].Text == text)
                {
                    return xtraTabControlHRContent.TabPages[i];
                }
            return null;
        }
        private bool IsClear()
        {
            foreach (Control item in this.Controls)
            {
                if (item is TextBox)
                {
                    if (item.Text != "" )
                        if(item.Text != "0")
                            return false;
                }
                else if (item is ComboBox)
                {
                    if (item.Text != "")
                        return false;
                }
                else if (item is DateTimePicker)
                {
                    DateTimePicker item1 = (DateTimePicker)item;
                    if (item1.Value.Date != DateTime.Now.Date)
                        return false;
                }
            }
          
            return true;
        }
    }
}

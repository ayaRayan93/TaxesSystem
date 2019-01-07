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
using System.Reflection;

namespace MainSystem
{
    public partial class AdminUserRecord : Form
    {
        MySqlConnection dbconnection, dbconnection2;
        bool load = false;
        int employeeId = 0;

        public AdminUserRecord(int EmployeeId)
        {
            try
            {
                InitializeComponent();
                dbconnection = new MySqlConnection(connection.connectionString);
                dbconnection2 = new MySqlConnection(connection.connectionString);
                employeeId = EmployeeId;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
     
        private void AdminUserRecord_Load(object sender, EventArgs e)
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
                comEmployee.SelectedValue = employeeId;

                query = "SELECT Department_Name,Department_ID FROM departments";
                da = new MySqlDataAdapter(query, dbconnection);
                dt = new DataTable();
                da.Fill(dt);
                comDepartment.DataSource = dt;
                comDepartment.DisplayMember = dt.Columns["Department_Name"].ToString();
                comDepartment.ValueMember = dt.Columns["Department_ID"].ToString();
                comDepartment.SelectedValue = 1;
                
                load = true;
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
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
                            query = "select User_ID from users order by User_ID desc limit 1";
                            com = new MySqlCommand(query, dbconnection);
                            int userId = Convert.ToInt16(com.ExecuteScalar().ToString());
                            
                            UserControl.ItemRecord("users", "اضافة", userId, DateTime.Now, "", dbconnection);

                            UserControl.userID = userId;
                            UserControl.userName = txtName.Text;
                            UserControl.userType = 1;
                            UserControl.EmpType = "مدير";

                            string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"Branch.txt");
                            UserControl.EmpBranchID = Convert.ToInt16(System.IO.File.ReadAllText(path));
                            
                            string query2 = "SELECT branch.Branch_Name FROM branch where branch.Branch_ID=" + UserControl.EmpBranchID;
                            com = new MySqlCommand(query2, dbconnection);
                            UserControl.EmpBranchName = com.ExecuteScalar().ToString();

                            dbconnection2.Open();
                            query2 = "SELECT users.Employee_ID,employee.Employee_Name FROM users INNER JOIN employee ON users.Employee_ID = employee.Employee_ID where users.User_ID=" + userId;
                            com = new MySqlCommand(query2, dbconnection2);
                            MySqlDataReader dr = com.ExecuteReader();
                            while (dr.Read())
                            {
                                UserControl.EmpID = Convert.ToInt16(dr["Employee_ID"].ToString());
                                UserControl.EmpName = dr["Employee_Name"].ToString();
                            }
                            dr.Close();
                            MainForm mainForm = new MainForm();
                            mainForm.Show();
                            this.Hide();
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
            dbconnection2.Close();
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
                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }

        private void AdminUserRecord_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                Environment.Exit(0);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
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

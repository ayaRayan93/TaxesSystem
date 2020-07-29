using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TaxesSystem
{
    public partial class Login_Admin : Form
    {
        MySqlConnection conn;
        MySqlConnection dbconnection/*, dbconnection2*/;
        Timer t;
        public static MainForm mainForm;

        public Login_Admin()
        {
            InitializeComponent();
            conn = new MySqlConnection(connection.connectionString);
            dbconnection = new MySqlConnection(connection.connectionString);
            //dbconnection2 = new MySqlConnection(connection.connectionString);
            txtName.Focus();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                string query = "select User_ID,User_Name,User_Type from users where User_Name=@Name and Password=@Pass";
                conn.Open();
                MySqlCommand comand = new MySqlCommand(query, conn);
                comand.Parameters.AddWithValue("@Name", txtName.Text);
                comand.Parameters.AddWithValue("@Pass", txtPassword.Text);
                MySqlDataReader result = comand.ExecuteReader();
                
                if (result.HasRows)
                {
                    while (result.Read())
                    {
                        if ((int)result[2] == 1)
                        {
                            UserControl.userID = (int)result[0];
                            UserControl.userName = result[1].ToString();
                            UserControl.userType = (int)result[2];
                            UserControl.EmpType = "مدير";

                            //string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"Branch.txt");
                            //UserControl.EmpBranchID = Convert.ToInt32(System.IO.File.ReadAllText(path));

                            string supString = BaseData.BranchID;
                            UserControl.EmpBranchID = Convert.ToInt32(supString);

                            dbconnection.Open();
                            string query2 = "SELECT branch.Branch_Name FROM branch where branch.Branch_ID=" + UserControl.EmpBranchID;
                            MySqlCommand com = new MySqlCommand(query2, dbconnection);
                            UserControl.EmpBranchName = com.ExecuteScalar().ToString();
                            
                            query2 = "SELECT users.Employee_ID,employee.Employee_Name FROM users INNER JOIN employee ON users.Employee_ID = employee.Employee_ID where users.User_ID=" + (int)result[0];
                            com = new MySqlCommand(query2, dbconnection);
                            MySqlDataReader dr = com.ExecuteReader();
                            while (dr.Read())
                            {
                                UserControl.EmpID = Convert.ToInt32(dr["Employee_ID"].ToString());
                                UserControl.EmpName = dr["Employee_Name"].ToString();
                            }
                            dr.Close();
                            mainForm = new MainForm();
                            mainForm.Show();
                            this.Hide();
                        }
                        else if ((int)result[2] == 5)
                        {
                            string q = "SELECT delegate.Branch_ID FROM users INNER JOIN delegate ON users.Employee_ID = delegate.Delegate_ID where users.User_ID=" + (int)result[0];
                            MySqlCommand com = new MySqlCommand(q, dbconnection);
                            dbconnection.Open();
                            if (com.ExecuteScalar().ToString() != "")
                            {
                                int EmpBranchID1 = Convert.ToInt32(com.ExecuteScalar().ToString());

                                //string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"Branch.txt");
                                //int EmpBranchID2 = Convert.ToInt32(System.IO.File.ReadAllText(path));

                                string supString = BaseData.BranchID;
                                int EmpBranchID2 = Convert.ToInt32(supString);

                                if (EmpBranchID1 == EmpBranchID2)
                                {
                                    UserControl.userID = (int)result[0];
                                    UserControl.userName = result[1].ToString();
                                    UserControl.userType = (int)result[2];
                                    UserControl.EmpType = "مندوب";
                                    UserControl.EmpBranchID = EmpBranchID2;

                                    string query2 = "SELECT branch.Branch_Name FROM branch where branch.Branch_ID=" + UserControl.EmpBranchID;
                                    com = new MySqlCommand(query2, dbconnection);
                                    UserControl.EmpBranchName = com.ExecuteScalar().ToString();

                                    query2 = "SELECT users.Employee_ID,delegate.Delegate_Name FROM users INNER JOIN delegate ON users.Employee_ID = delegate.Delegate_ID where users.User_ID=" + (int)result[0];
                                    com = new MySqlCommand(query2, dbconnection);
                                    MySqlDataReader dr = com.ExecuteReader();
                                    while (dr.Read())
                                    {
                                        UserControl.EmpID = Convert.ToInt32(dr["Employee_ID"].ToString());
                                        UserControl.EmpName = dr["Delegate_Name"].ToString();
                                    }
                                    dr.Close();

                                    mainForm = new MainForm();
                                    mainForm.Show();
                                    this.Hide();
                                }
                                else
                                {
                                    MessageBox.Show("حاول مرة اخرى");
                                }
                            }
                            else
                            {
                                MessageBox.Show("حاول مرة اخرى");
                            }
                        }
                        else
                        {
                            string q = "SELECT employee.Branch_ID FROM users INNER JOIN employee ON users.Employee_ID = employee.Employee_ID where users.User_ID=" + (int)result[0];
                            MySqlCommand com = new MySqlCommand(q, dbconnection);
                            dbconnection.Open();
                            if (com.ExecuteScalar().ToString() != "")
                            {
                                int EmpBranchID1 = Convert.ToInt32(com.ExecuteScalar().ToString());

                                //string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"Branch.txt");
                                //int EmpBranchID2 = Convert.ToInt32(System.IO.File.ReadAllText(path));

                                string supString = BaseData.BranchID;
                                int EmpBranchID2 = Convert.ToInt32(supString);

                                if (EmpBranchID1 == EmpBranchID2)
                                {
                                    UserControl.userID = (int)result[0];
                                    UserControl.userName = result[1].ToString();
                                    UserControl.userType = (int)result[2];
                                    UserControl.EmpType = "موظف";
                                    UserControl.EmpBranchID = EmpBranchID2;

                                    string query2 = "SELECT branch.Branch_Name FROM branch where branch.Branch_ID=" + UserControl.EmpBranchID;
                                    com = new MySqlCommand(query2, dbconnection);
                                    UserControl.EmpBranchName = com.ExecuteScalar().ToString();

                                    query2 = "SELECT users.Employee_ID,employee.Employee_Name FROM users INNER JOIN employee ON users.Employee_ID = employee.Employee_ID where users.User_ID=" + (int)result[0];
                                    com = new MySqlCommand(query2, dbconnection);
                                    MySqlDataReader dr = com.ExecuteReader();
                                    while (dr.Read())
                                    {
                                        UserControl.EmpID = Convert.ToInt32(dr["Employee_ID"].ToString());
                                        UserControl.EmpName = dr["Employee_Name"].ToString();
                                    }
                                    dr.Close();

                                    mainForm = new MainForm();
                                    mainForm.Show();
                                    this.Hide();
                                }
                                else
                                {
                                    MessageBox.Show("حاول مرة اخرى");
                                }
                            }
                            else
                            {
                                MessageBox.Show("حاول مرة اخرى");
                            }
                        }
                    }
                    result.Close();
                }
                else
                {
                    MessageBox.Show("حاول مرة اخرى");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
            conn.Close();
        }

        private void txtName_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    string query = "select User_Name from users where User_Name=@Name ";
                    conn.Open();
                    MySqlCommand comand = new MySqlCommand(query, conn);
                    comand.Parameters.AddWithValue("@Name", txtName.Text);
                    var result = comand.ExecuteScalar();

                    if (result != null)
                    {
                        txtPassword.Focus();
                    }
                    else
                    {
                        txtName.Focus();
                        MessageBox.Show("ادخل اسم مستخدم صحيح");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            conn.Close();
        }
        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    string query = "select User_ID,User_Name,User_Type from users where User_Name=@Name and Password=@Pass";
                    conn.Open();
                    MySqlCommand comand = new MySqlCommand(query, conn);
                    comand.Parameters.AddWithValue("@Name", txtName.Text);
                    comand.Parameters.AddWithValue("@Pass", txtPassword.Text);
                    MySqlDataReader result = comand.ExecuteReader();

                    if (result.HasRows)
                    {
                        while (result.Read())
                        {
                            if ((int)result[2] == 1)
                            {
                                UserControl.userID = (int)result[0];
                                UserControl.userName = result[1].ToString();
                                UserControl.userType = (int)result[2];
                                UserControl.EmpType = "مدير";

                                //string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"Branch.txt");
                                //UserControl.EmpBranchID = Convert.ToInt32(System.IO.File.ReadAllText(path));

                                string supString = BaseData.BranchID;
                                UserControl.EmpBranchID = Convert.ToInt32(supString);

                                dbconnection.Open();
                                string query2 = "SELECT branch.Branch_Name FROM branch where branch.Branch_ID=" + UserControl.EmpBranchID;
                                MySqlCommand com = new MySqlCommand(query2, dbconnection);
                                UserControl.EmpBranchName = com.ExecuteScalar().ToString();

                                query2 = "SELECT users.Employee_ID,employee.Employee_Name FROM users INNER JOIN employee ON users.Employee_ID = employee.Employee_ID where users.User_ID=" + (int)result[0];
                                com = new MySqlCommand(query2, dbconnection);
                                MySqlDataReader dr = com.ExecuteReader();
                                while (dr.Read())
                                {
                                    UserControl.EmpID = Convert.ToInt32(dr["Employee_ID"].ToString());
                                    UserControl.EmpName = dr["Employee_Name"].ToString();
                                }
                                dr.Close();
                                mainForm = new MainForm();
                                mainForm.Show();
                                this.Hide();
                            }
                            else if ((int)result[2] == 5)
                            {
                                string q = "SELECT delegate.Branch_ID FROM users INNER JOIN delegate ON users.Employee_ID = delegate.Delegate_ID where users.User_ID=" + (int)result[0];
                                MySqlCommand com = new MySqlCommand(q, dbconnection);
                                dbconnection.Open();
                                if (com.ExecuteScalar().ToString() != "")
                                {
                                    int EmpBranchID1 = Convert.ToInt32(com.ExecuteScalar().ToString());

                                    //string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"Branch.txt");
                                    //int EmpBranchID2 = Convert.ToInt32(System.IO.File.ReadAllText(path));

                                    string supString = BaseData.BranchID;
                                    int EmpBranchID2 = Convert.ToInt32(supString);

                                    if (EmpBranchID1 == EmpBranchID2)
                                    {
                                        UserControl.userID = (int)result[0];
                                        UserControl.userName = result[1].ToString();
                                        UserControl.userType = (int)result[2];
                                        UserControl.EmpType = "مندوب";
                                        UserControl.EmpBranchID = EmpBranchID2;

                                        string query2 = "SELECT branch.Branch_Name FROM branch where branch.Branch_ID=" + UserControl.EmpBranchID;
                                        com = new MySqlCommand(query2, dbconnection);
                                        UserControl.EmpBranchName = com.ExecuteScalar().ToString();

                                        query2 = "SELECT users.Employee_ID,delegate.Delegate_Name FROM users INNER JOIN delegate ON users.Employee_ID = delegate.Delegate_ID where users.User_ID=" + (int)result[0];
                                        com = new MySqlCommand(query2, dbconnection);
                                        MySqlDataReader dr = com.ExecuteReader();
                                        while (dr.Read())
                                        {
                                            UserControl.EmpID = Convert.ToInt32(dr["Employee_ID"].ToString());
                                            UserControl.EmpName = dr["Delegate_Name"].ToString();
                                        }
                                        dr.Close();

                                        mainForm = new MainForm();
                                        mainForm.Show();
                                        this.Hide();
                                    }
                                    else
                                    {
                                        MessageBox.Show("حاول مرة اخرى");
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("حاول مرة اخرى");
                                }
                            }
                            else
                            {
                                string q = "SELECT employee.Branch_ID FROM users INNER JOIN employee ON users.Employee_ID = employee.Employee_ID where users.User_ID=" + (int)result[0];
                                MySqlCommand com = new MySqlCommand(q, dbconnection);
                                dbconnection.Open();
                                if (com.ExecuteScalar().ToString() != "")
                                {
                                    int EmpBranchID1 = Convert.ToInt32(com.ExecuteScalar().ToString());

                                    //string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"Branch.txt");
                                    //int EmpBranchID2 = Convert.ToInt32(System.IO.File.ReadAllText(path));

                                    string supString = BaseData.BranchID;
                                    int EmpBranchID2 = Convert.ToInt32(supString);

                                    if (EmpBranchID1 == EmpBranchID2)
                                    {
                                        UserControl.userID = (int)result[0];
                                        UserControl.userName = result[1].ToString();
                                        UserControl.userType = (int)result[2];
                                        UserControl.EmpType = "موظف";
                                        UserControl.EmpBranchID = EmpBranchID2;

                                        string query2 = "SELECT branch.Branch_Name FROM branch where branch.Branch_ID=" + UserControl.EmpBranchID;
                                        com = new MySqlCommand(query2, dbconnection);
                                        UserControl.EmpBranchName = com.ExecuteScalar().ToString();

                                        query2 = "SELECT users.Employee_ID,employee.Employee_Name FROM users INNER JOIN employee ON users.Employee_ID = employee.Employee_ID where users.User_ID=" + (int)result[0];
                                        com = new MySqlCommand(query2, dbconnection);
                                        MySqlDataReader dr = com.ExecuteReader();
                                        while (dr.Read())
                                        {
                                            UserControl.EmpID = Convert.ToInt32(dr["Employee_ID"].ToString());
                                            UserControl.EmpName = dr["Employee_Name"].ToString();
                                        }
                                        dr.Close();

                                        mainForm = new MainForm();
                                        mainForm.Show();
                                        this.Hide();
                                    }
                                    else
                                    {
                                        MessageBox.Show("حاول مرة اخرى");
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("حاول مرة اخرى");
                                }
                            }
                        }
                        result.Close();
                    }
                    else
                    {
                        txtPassword.Focus();
                        MessageBox.Show("ادخل رقم سرى صحيح");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
            conn.Close();
        }
   

        private void Login_Load(object sender, EventArgs e)
        {
            try
            {
                t = new Timer();
                
                t.Interval = 2000; // specify interval time as you want
                t.Tick += new EventHandler(timer_Tick);
                t.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        void timer_Tick(object sender, EventArgs e)
        {
            try
            {
                t.Stop();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
       
        }

        private void panClose_Click(object sender, EventArgs e)
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

        private void btnSignUp_Click(object sender, EventArgs e)
        {
            try
            {
                AdminRecord form = new AdminRecord();
                form.Show();
                this.Hide();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}

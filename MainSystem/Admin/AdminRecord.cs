using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using MySql.Data.MySqlClient;
using DevExpress.XtraTab;
using System.Reflection;

namespace MainSystem
{
    public partial class AdminRecord : Form
    {
        MySqlConnection dbconnection;
        bool load = false;

        public AdminRecord()
        {
            try
            {
                InitializeComponent();
                dbconnection = new MySqlConnection(connection.connectionString);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void AdminRecord_Load(object sender, EventArgs e)
        {
            try
            {
                /*string query = "select * from branch";
                MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                DataTable dt = new DataTable();
                da.Fill(dt);
                comBranch.DataSource = dt;
                comBranch.DisplayMember = dt.Columns["Branch_Name"].ToString();
                comBranch.ValueMember = dt.Columns["Branch_ID"].ToString();

                string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"Branch.txt");
                comBranch.SelectedValue = System.IO.File.ReadAllText(path);*/

                string query = "select * from departments";
                MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                DataTable dt = new DataTable();
                da.Fill(dt);
                comDepartment.DataSource = dt;
                comDepartment.DisplayMember = dt.Columns["Department_Name"].ToString();
                comDepartment.ValueMember = dt.Columns["Department_ID"].ToString();
                comDepartment.SelectedValue = 1;

                load = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnAddEmployee_Click(object sender, EventArgs e)
        {
            try
            {
                byte[] img = null;

                if (ImageBox.Image != null)
                {
                    MemoryStream ms = new MemoryStream();
                    ImageBox.Image.Save(ms, ImageBox.Image.RawFormat);
                    img = ms.ToArray();
                }
                double x;

                if (txtSalary.Text == "")
                {
                    x = 0;
                }
                else
                {
                    x = Double.Parse(txtSalary.Text);
                }

                dbconnection.Open();
                
                #region Add New Employee
                string insert = "INSERT INTO Employee (Employee_Number,Employee_Name,Employee_Phone,Employee_Address,Employee_Info,Employee_Qualification,Employee_Start_Date,Employee_Job,Department_ID,Employee_Birth_Date,Employee_Salary,Employee_Mail,Employee_Photo,National_ID,Social_Status,SocialInsuranceNumber,EmploymentType,ExperienceYears) VALUES (@Employee_Number,@Employee_Name,@Employee_Phone,@Employee_Address,@Employee_Info,@Employee_Qualification,@Employee_Start,@Employee_Job,@Department_ID,@Employee_Birth,@Employee_Salary,@Employee_Mail,@Employee_Photo,@National_ID,@Social_Status,@SocialInsuranceNumber,@EmploymentType,@ExperienceYears)";
                MySqlCommand cmd = new MySqlCommand(insert, dbconnection);
                cmd.Parameters.Add("@Employee_Number", MySqlDbType.Int16);
                if (txtEmployeeNumber.Text != "")
                {
                    cmd.Parameters["@Employee_Number"].Value = Convert.ToInt16(txtEmployeeNumber.Text);
                    labNumberReqired.Visible = false;
                }
                else
                {
                    txtEmployeeNumber.Focus();
                    labNumberReqired.Visible = true;
                    dbconnection.Close();
                    return;
                }
                cmd.Parameters.Add("@Employee_Name", MySqlDbType.VarChar, 255);
                if (txtEmployeeName.Text != "")
                {
                    cmd.Parameters["@Employee_Name"].Value = txtEmployeeName.Text;
                    labName.Visible = false;
                }
                else
                {
                    txtEmployeeName.Focus();
                    labName.Visible = true;
                    dbconnection.Close();
                    return;
                }

                cmd.Parameters.Add("@National_ID", MySqlDbType.VarChar, 255);
                cmd.Parameters["@National_ID"].Value = txtNationalID.Text;
                cmd.Parameters.Add("@Employee_Phone", MySqlDbType.VarChar, 255);
                cmd.Parameters["@Employee_Phone"].Value = txtPhone.Text;
                cmd.Parameters.Add("@Employee_Address", MySqlDbType.VarChar, 255);
                cmd.Parameters["@Employee_Address"].Value = txtAddress.Text;
                cmd.Parameters.Add("@Employee_Qualification", MySqlDbType.VarChar, 255);
                cmd.Parameters["@Employee_Qualification"].Value = txtQualification.Text;
                cmd.Parameters.Add("@Employee_Start", MySqlDbType.Date, 0);
                cmd.Parameters["@Employee_Start"].Value = dateTimePickerStartDate.Value;
                cmd.Parameters.Add("@Employee_Job", MySqlDbType.VarChar, 255);
                cmd.Parameters["@Employee_Job"].Value = txtJob.Text;
                cmd.Parameters.Add("@Employee_Salary", MySqlDbType.Decimal, 10);
                cmd.Parameters["@Employee_Salary"].Value = x;
                cmd.Parameters.Add("@Employee_Birth", MySqlDbType.Date, 0);
                cmd.Parameters["@Employee_Birth"].Value = dateTimePickerBirthDate.Value;
                cmd.Parameters.Add("@Employee_Mail", MySqlDbType.VarChar, 255);
                cmd.Parameters["@Employee_Mail"].Value = txtMail.Text;
                /*cmd.Parameters.Add("@Branch_ID", MySqlDbType.Int16);
                if (comBranch.Text != "")
                {
                    cmd.Parameters["@Branch_ID"].Value = comBranch.SelectedValue;
                    labelBranch.Visible = false;
                }
                else
                {
                    comBranch.Focus();
                    labelBranch.Visible = true;
                    dbconnection.Close();
                    return;
                }*/
                cmd.Parameters.Add("@Department_ID", MySqlDbType.Int16);
                if (comDepartment.Text != "")
                {
                    cmd.Parameters["@Department_ID"].Value = comDepartment.SelectedValue;
                    labelDepartement.Visible = false;
                }
                else
                {
                    comDepartment.Focus();
                    labelDepartement.Visible = true;
                    dbconnection.Close();
                    return;
                }
                cmd.Parameters.Add("@Employee_Info", MySqlDbType.VarChar, 255);
                cmd.Parameters["@Employee_Info"].Value = txtNotes.Text;
                cmd.Parameters.Add("@Employee_Photo", MySqlDbType.Blob);
                cmd.Parameters["@Employee_Photo"].Value = img;
                cmd.Parameters.Add("@Social_Status", MySqlDbType.VarChar, 255);
                cmd.Parameters["@Social_Status"].Value = txtSocialStatus.Text;

                cmd.Parameters.Add("@SocialInsuranceNumber", MySqlDbType.Int16);
                if (txtSocialInsuranceNumber.Text != "")
                    cmd.Parameters["@SocialInsuranceNumber"].Value = Convert.ToInt16(txtSocialInsuranceNumber.Text);
                else
                    cmd.Parameters["@SocialInsuranceNumber"].Value = 0;

                cmd.Parameters.Add("@EmploymentType", MySqlDbType.VarChar, 255);
                cmd.Parameters["@EmploymentType"].Value = txtWorkType.Text;
                cmd.Parameters.Add("@ExperienceYears", MySqlDbType.Int16);
                if (txtExperienceYears.Text != "")
                    cmd.Parameters["@ExperienceYears"].Value = Convert.ToInt16(txtExperienceYears.Text);
                else
                    cmd.Parameters["@ExperienceYears"].Value = 0;
                #endregion

                if (cmd.ExecuteNonQuery() == 1)
                {
                    string query = "select Employee_ID from employee order by Employee_ID desc limit 1";
                    MySqlCommand com = new MySqlCommand(query, dbconnection);
                    int employeeId = Convert.ToInt16(com.ExecuteScalar().ToString());

                    UserControl.ItemRecord("employee", "اضافة", employeeId, DateTime.Now,"", dbconnection);

                    AdminUserRecord form = new AdminUserRecord(employeeId);
                    form.Show();
                    this.Hide();
                }
            }
            catch
            {
                MessageBox.Show("يوجد خطأ في البيانات");
            }
            dbconnection.Close();
        }

        private void btnUploadImage_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog opf = new OpenFileDialog();
                opf.Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png";
                if (opf.ShowDialog() == DialogResult.OK)
                {
                    ImageBox.Image = Image.FromFile(opf.FileName);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
    
        }

        private void rDelegate_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                comDepartment.Text = "نقطة البيع";
                comDepartment.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void rEmployee_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                comDepartment.Text = "";
                comDepartment.SelectedIndex = -1;
                comDepartment.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void AdminRecord_FormClosed(object sender, FormClosedEventArgs e)
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

        //function
        //clear
        public void clear()
        {
            foreach (Control item in this.Controls["panel1"].Controls["panel2"].Controls)
            {
                if (item is TextBox)
                    item.Text = "";
                else if (item is ComboBox)
                    item.Text = "";
            }
            ImageBox.Image = null;
        }
        
        private bool IsClear()
        {
            foreach (Control item in this.Controls["panel1"].Controls["panel2"].Controls)
            {
                if (item is TextBox)
                {
                    if(item.Text != "")
                    return false;
                }
                else if (item is ComboBox)
                {
                    if (item.Text != "")
                        return false;
                }
            }
            if (ImageBox.Image != null)
                return false;

            return true;
        }
    }
}

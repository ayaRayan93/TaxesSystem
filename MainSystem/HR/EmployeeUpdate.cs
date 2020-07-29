﻿using DevExpress.XtraTab;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TaxesSystem
{
    public partial class EmployeeUpdate : Form
    {
        MySqlConnection dbconnection;
        DataRowView row;
        Employees Employees;
        bool imgFlag = false,load=false;
        XtraTabControl xtraTabControlHRContent;
        public EmployeeUpdate(DataRowView r, Employees Employees, XtraTabControl xtraTabControlHRContent)
        {
            try
            {
                InitializeComponent();
                dbconnection = new MySqlConnection(connection.connectionString);
                this.Employees = Employees;
                this.row = r;
                this.xtraTabControlHRContent = xtraTabControlHRContent;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
      
        }

        private void EmployeeUpdate_Load(object sender, EventArgs e)
        {
            try
            {
                dbconnection.Open();
                string query = "select * from branch";
                MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                DataTable dt = new DataTable();
                da.Fill(dt);
                comBranch.DataSource = dt;
                comBranch.DisplayMember = dt.Columns["Branch_Name"].ToString();
                comBranch.ValueMember = dt.Columns["Branch_ID"].ToString();
                comBranch.Text = row["الفرع"].ToString();

                query = "select * from departments";
                da = new MySqlDataAdapter(query, dbconnection);
                dt = new DataTable();
                da.Fill(dt);
                comDepartment.DataSource = dt;
                comDepartment.DisplayMember = dt.Columns["Department_Name"].ToString();
                comDepartment.ValueMember = dt.Columns["Department_ID"].ToString();
                comDepartment.Text = row["مكان العمل"].ToString();
                setData();

                load = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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
                    imgFlag = true;
                    XtraTabPage xtraTabPage = getTabPage("تعديل موظف");
                    xtraTabPage.ImageOptions.Image = Properties.Resources.unsave;
            
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                byte[] img = null;

                if (ImageBox.Image != null&&imgFlag)
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
                if (row[0].ToString() == "موظف")
                {
                    #region Add New Employee
                    string query = "update employee set Employee_Number=@Employee_Number ,Employee_Name=@Employee_Name ,Employee_Phone=@Employee_Phone,Employee_Address=@Employee_Address,Employee_Mail=@Employee_Mail,Employee_Birth_Date=@Employee_Birth_Date,Employee_Qualification=@Employee_Qualification,SocialInsuranceNumber=@SocialInsuranceNumber,National_ID=@National_ID,Social_Status=@Social_Status,Employee_Start_Date=@Employee_Start_Date,Branch_ID=@Branch_ID,Employee_Job=@Employee_Job,Department_ID=@Department_ID,Employee_Salary=@Employee_Salary,Employee_Photo=@Employee_Photo,EmploymentType=@EmploymentType,ExperienceYears=@ExperienceYears,Employee_Info=@Employee_Info where Employee_ID=" + row[1].ToString();
                    MySqlCommand cmd = new MySqlCommand(query, dbconnection);
                    cmd.Parameters.Add("@Employee_Number", MySqlDbType.Int16);
                    if (txtEmployeeNumber.Text != "")
                    {
                        cmd.Parameters["@Employee_Number"].Value = Convert.ToInt32(txtEmployeeNumber.Text);
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
                    cmd.Parameters.Add("@Employee_Start_Date", MySqlDbType.Date, 0);
                    cmd.Parameters["@Employee_Start_Date"].Value = dateTimePickerStartDate.Value;
                    cmd.Parameters.Add("@Employee_Job", MySqlDbType.VarChar, 255);
                    cmd.Parameters["@Employee_Job"].Value = txtJob.Text;
                    cmd.Parameters.Add("@Employee_Salary", MySqlDbType.Decimal, 10);
                    cmd.Parameters["@Employee_Salary"].Value = x;
                    cmd.Parameters.Add("@Employee_Birth_Date", MySqlDbType.Date, 0);
                    cmd.Parameters["@Employee_Birth_Date"].Value = dateTimePickerBirthDate.Value;
                    cmd.Parameters.Add("@Employee_Mail", MySqlDbType.VarChar, 255);
                    cmd.Parameters["@Employee_Mail"].Value = txtMail.Text;
                    cmd.Parameters.Add("@Branch_ID", MySqlDbType.Int16);
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
                    }
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
                        cmd.Parameters["@SocialInsuranceNumber"].Value = Convert.ToInt32(txtSocialInsuranceNumber.Text);
                    else
                        cmd.Parameters["@SocialInsuranceNumber"].Value = 0;

                    cmd.Parameters.Add("@EmploymentType", MySqlDbType.VarChar, 255);
                    cmd.Parameters["@EmploymentType"].Value = txtWorkType.Text;
                    cmd.Parameters.Add("@ExperienceYears", MySqlDbType.Int16);
                    if (txtExperienceYears.Text != "")
                        cmd.Parameters["@ExperienceYears"].Value = Convert.ToInt32(txtExperienceYears.Text);
                    else
                        cmd.Parameters["@ExperienceYears"].Value = 0;
                    #endregion

                    if (cmd.ExecuteNonQuery() == 1)
                    {
                        Employees.displayEmployee();
                        UserControl.ItemRecord("employee", "تعديل", Convert.ToInt32(row[1].ToString()), DateTime.Now, "", dbconnection);
                        XtraTabPage xtraTabPage = getTabPage("تعديل موظف");
                        xtraTabControlHRContent.TabPages.Remove(xtraTabPage);
                    }
                }
                else
                {
                    #region Add New Delegate
                    //,Department_ID = @Department_ID
                    string query = "update Delegate set Delegate_Number=@Delegate_Number ,Delegate_Name=@Delegate_Name ,Delegate_Phone=@Delegate_Phone,Delegate_Address=@Delegate_Address,Delegate_Mail=@Delegate_Mail,Delegate_Birth_Date=@Delegate_Birth_Date,Delegate_Qualification=@Delegate_Qualification,SocialInsuranceNumber=@SocialInsuranceNumber,National_ID=@National_ID,Social_Status=@Social_Status,Delegate_Start_Date=@Delegate_Start_Date,Branch_ID=@Branch_ID,Delegate_Job=@Delegate_Job,Delegate_Salary=@Delegate_Salary,Delegate_Photo=@Delegate_Photo,EmploymentType=@EmploymentType,ExperienceYears=@ExperienceYears,Delegate_Taraget=@Delegate_Taraget,Delegate_Info=@Delegate_Info where Delegate_ID=" + row[1].ToString();
                    MySqlCommand cmd = new MySqlCommand(query, dbconnection);
                    cmd.Parameters.Add("@Delegate_Number", MySqlDbType.Int16);
                    if (txtEmployeeNumber.Text != "")
                    {
                        cmd.Parameters["@Delegate_Number"].Value = Convert.ToInt32(txtEmployeeNumber.Text);
                        labNumberReqired.Visible = false;
                    }
                    else
                    {
                        txtEmployeeNumber.Focus();
                        labNumberReqired.Visible = true;
                        dbconnection.Close();
                        return;
                    }
                    cmd.Parameters.Add("@Delegate_Name", MySqlDbType.VarChar, 255);
                    if (txtEmployeeName.Text != "")
                    {
                        cmd.Parameters["@Delegate_Name"].Value = txtEmployeeName.Text;
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
                    cmd.Parameters.Add("@Delegate_Phone", MySqlDbType.VarChar, 255);
                    cmd.Parameters["@Delegate_Phone"].Value = txtPhone.Text;
                    cmd.Parameters.Add("@Delegate_Address", MySqlDbType.VarChar, 255);
                    cmd.Parameters["@Delegate_Address"].Value = txtAddress.Text;
                    cmd.Parameters.Add("@Delegate_Qualification", MySqlDbType.VarChar, 255);
                    cmd.Parameters["@Delegate_Qualification"].Value = txtQualification.Text;
                    cmd.Parameters.Add("@Delegate_Start_Date", MySqlDbType.Date, 0);
                    cmd.Parameters["@Delegate_Start_Date"].Value = dateTimePickerStartDate.Value;
                    cmd.Parameters.Add("@Delegate_Job", MySqlDbType.VarChar, 255);
                    cmd.Parameters["@Delegate_Job"].Value = txtJob.Text;
                    cmd.Parameters.Add("@Delegate_Salary", MySqlDbType.Decimal, 10);
                    cmd.Parameters["@Delegate_Salary"].Value = x;
                    cmd.Parameters.Add("@Delegate_Birth_Date", MySqlDbType.Date, 0);
                    cmd.Parameters["@Delegate_Birth_Date"].Value = dateTimePickerBirthDate.Value;
                    cmd.Parameters.Add("@Delegate_Mail", MySqlDbType.VarChar, 255);
                    cmd.Parameters["@Delegate_Mail"].Value = txtMail.Text;
                    cmd.Parameters.Add("@Branch_ID", MySqlDbType.Int16);
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
                    }
                    //cmd.Parameters.Add("@Department_ID", MySqlDbType.Int16);
                    //cmd.Parameters["@Department_ID"].Value = comDepartment.SelectedValue;
                    cmd.Parameters.Add("@Delegate_Info", MySqlDbType.VarChar, 255);
                    cmd.Parameters["@Delegate_Info"].Value = txtNotes.Text;
                    cmd.Parameters.Add("@Delegate_Photo", MySqlDbType.Blob);
                    cmd.Parameters["@Delegate_Photo"].Value = img;
                    cmd.Parameters.Add("@Social_Status", MySqlDbType.VarChar, 255);
                    cmd.Parameters["@Social_Status"].Value = txtSocialStatus.Text;
                    if (txtTaraget.Text != "")
                    {
                        cmd.Parameters.Add("@Delegate_Taraget", MySqlDbType.Decimal, 10);
                        cmd.Parameters["@Delegate_Taraget"].Value = Convert.ToDouble(txtTaraget.Text);
                    }
                    else
                    {
                        cmd.Parameters.Add("@Delegate_Taraget", MySqlDbType.Decimal, 10);
                        cmd.Parameters["@Delegate_Taraget"].Value = 0.00;
                    }
                    cmd.Parameters.Add("@SocialInsuranceNumber", MySqlDbType.Int16);
                    if (txtSocialInsuranceNumber.Text != "")
                        cmd.Parameters["@SocialInsuranceNumber"].Value = Convert.ToInt32(txtSocialInsuranceNumber.Text);
                    else
                        cmd.Parameters["@SocialInsuranceNumber"].Value = 0;

                    cmd.Parameters.Add("@EmploymentType", MySqlDbType.VarChar, 255);
                    cmd.Parameters["@EmploymentType"].Value = txtWorkType.Text;
                    cmd.Parameters.Add("@ExperienceYears", MySqlDbType.Int16);
                    if (txtExperienceYears.Text != "")
                        cmd.Parameters["@ExperienceYears"].Value = Convert.ToInt32(txtExperienceYears.Text);
                    else
                        cmd.Parameters["@ExperienceYears"].Value = 0;
                    #endregion

                    if (cmd.ExecuteNonQuery() == 1)
                    {
                        Employees.displayEmployee();
                        UserControl.ItemRecord("delegate", "تعديل",Convert.ToInt32(row[1].ToString()), DateTime.Now,"", dbconnection);
                        XtraTabPage xtraTabPage = getTabPage("تعديل موظف");
                        xtraTabControlHRContent.TabPages.Remove(xtraTabPage);
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }

        private void txtSalary_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (load)
                {
                    XtraTabPage xtraTabPage = getTabPage("تعديل موظف");
                    if (!IsClear())
                        xtraTabPage.ImageOptions.Image = Properties.Resources.unsave;
                    else
                        xtraTabPage.ImageOptions.Image = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //function
        //set data
        public void setData()
        {
            txtEmployeeNumber.Text = row["الرقم الوظيفي"].ToString();
            txtEmployeeName.Text = row["اسم الموظف"].ToString();
            txtNationalID.Text = row["الرقم القومي"].ToString();
            txtSocialInsuranceNumber.Text = row["رقم التامين الاجتماعي"].ToString();
            txtJob.Text = row["الوظيفة"].ToString();
            dateTimePickerStartDate.Text = row["تاريخ التعيين"].ToString();
            txtWorkType.Text = row["نوع التوظيف"].ToString();
            comDepartment.Text = row["مكان العمل"].ToString();
            txtQualification.Text = row["المؤهل العلمي"].ToString();
            txtExperienceYears.Text = row["عدد سنوات الخبرة"].ToString();
            txtSalary.Text = row["الراتب الاساسي"].ToString();
            txtAddress.Text = row["عنوان السكن"].ToString();
            txtPhone.Text = row["رقم الهاتف"].ToString();
            txtNotes.Text = row["ملاحظات"].ToString();
            dateTimePickerBirthDate.Text= row["تاريخ الميلاد"].ToString();
            txtMail.Text = row["البريد الالكتروني"].ToString();

            if (row[0].ToString() == "موظف")
            {
                label19.Visible = false;
                txtTaraget.Visible = false;
                comDepartment.Enabled = true;
            }
            else
            {
                label19.Visible = true;
                txtTaraget.Visible = true;
                comDepartment.Enabled = false;
                comDepartment.DropDownStyle = ComboBoxStyle.DropDownList;
                txtTaraget.Text= row["الهدف الشهري"].ToString();
            }


            ImageBox.Image = Properties.Resources.animated_gif_loading;
            Thread t1 = new Thread(displayImage);
            t1.Start();
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
            DateTime date = dateTimePickerStartDate.Value.Date;
            string d = date.ToString("dd/MM/yyyy");
            date = dateTimePickerBirthDate.Value.Date;
            string d2 = date.ToString("dd/MM/yyyy");
        
            if ( txtEmployeeNumber.Text == row["الرقم الوظيفي"].ToString()&&
            txtEmployeeName.Text == row["اسم الموظف"].ToString()&&
            txtNationalID.Text == row["الرقم القومي"].ToString()&&
            txtSocialInsuranceNumber.Text == row["رقم التامين الاجتماعي"].ToString()&&
            txtJob.Text == row["الوظيفة"].ToString()&&
            d== row["تاريخ التعيين"].ToString().Split(' ')[0] &&
            txtWorkType.Text == row["نوع التوظيف"].ToString()&&
            comDepartment.Text == row["مكان العمل"].ToString()&&
            txtQualification.Text == row["المؤهل العلمي"].ToString()&&
            txtExperienceYears.Text == row["عدد سنوات الخبرة"].ToString()&&
            txtSalary.Text == row["الراتب الاساسي"].ToString()&&
            txtAddress.Text == row["عنوان السكن"].ToString()&&
            txtPhone.Text == row["رقم الهاتف"].ToString()&&
            txtNotes.Text == row["ملاحظات"].ToString()&&
            d2 == row["تاريخ الميلاد"].ToString().Split(' ')[0] &&
            txtMail.Text == row["البريد الالكتروني"].ToString())
            {

                if (row[0].ToString() == "موظف")
                {
                    label19.Visible = false;
                    txtTaraget.Visible = false;
                    return true;
                }
                else
                {
                    label19.Visible = true;
                    txtTaraget.Visible = true;
                    if (txtTaraget.Text == row["الهدف الشهري"].ToString())
                        return true;
                }
            }
            return false;
        }

        public void displayImage()
        {
            try
            {
                dbconnection.Open();
                string query = "select Employee_Photo from employee where Employee_ID=" + row[0] ;
                MySqlCommand com = new MySqlCommand(query, dbconnection);
                byte[] photo = (byte[])com.ExecuteScalar();

                if (photo != null)
                {
                    MemoryStream ms = new MemoryStream(photo);
                    ImageBox.Image = Image.FromStream(ms);
                }
                else
                {
                    ImageBox.Image = Properties.Resources.notFound;
                }
            }
            catch
            {
                //  MessageBox.Show(ex.Message);
                ImageBox.Image = Properties.Resources.notFound;
            }
            dbconnection.Close();
        }

        
    }
}

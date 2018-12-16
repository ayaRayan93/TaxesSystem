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

        public UserRecord(MainForm HrMainForm)
        {
            try
            {
                InitializeComponent();
                dbconnection = new MySqlConnection(connection.connectionString);
                HRMainForm = HrMainForm;
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

                string query = "SELECT Employee_Name,Employee_ID FROM employee ";
                MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                DataTable dt = new DataTable();
                da.Fill(dt);
                comEmployee.DataSource = dt;
                comEmployee.DisplayMember = dt.Columns["Employee_Name"].ToString();
                comEmployee.ValueMember = dt.Columns["Employee_ID"].ToString();
                comEmployee.Text = "";
                
                load = true;
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
            dbconnection.Close();
        }

        private void comEmployee_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (load)
                {
                    dbconnection.Open();
                    if (rEmployee.Checked)
                    {
                        string query = "SELECT Employee_Number FROM employee WHERE Employee_ID = " + comEmployee.SelectedValue;
                        MySqlCommand com = new MySqlCommand(query, dbconnection);
                        txtEmployee_Name.Text = com.ExecuteScalar().ToString();
                    }
                    else if (rDelegate.Checked)
                    {
                        string query = "SELECT Delegate_Number FROM delegate WHERE Delegate_ID = " + comEmployee.SelectedValue;
                        MySqlCommand com = new MySqlCommand(query, dbconnection);
                        txtEmployee_Name.Text = com.ExecuteScalar().ToString();
                    }

                    load = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }

        private void txtEmployeeNumber_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                dbconnection.Open();
                if (e.KeyCode == Keys.Enter)
                {
                    if (rEmployee.Checked)
                    {
                        string query = "SELECT Employee_Name,Employee_ID FROM employee WHERE Employee_Number = " + txtEmployee_Name.Text;
                        MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                        dbconnection.Close();
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        comEmployee.DataSource = dt;
                        comEmployee.DisplayMember = dt.Columns["Employee_Name"].ToString();
                        comEmployee.ValueMember = dt.Columns["Employee_ID"].ToString();
                    }
                    else if (rDelegate.Checked)
                    {
                        string query = "SELECT Delegate_Name,Delegate_ID FROM delegate WHERE Delegate_Number = " + txtEmployee_Name.Text;
                        MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                        dbconnection.Close();
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        comEmployee.DataSource = dt;
                        comEmployee.DisplayMember = dt.Columns["Delegate_Name"].ToString();
                        comEmployee.ValueMember = dt.Columns["Delegate_ID"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }

        private void txtStimulus_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //txtTotalSalary.Text = (Double.Parse(txtSalary.Text) + Double.Parse(txtStimulus.Text) - Double.Parse(txtDeductions.Text) - Double.Parse(txtDeductionsSocial.Text) ).ToString();
            }
        }

        private void btnAddSalary_Click(object sender, EventArgs e)
        {
            try
            {
                dbconnection.Open();
                string insert = "INSERT INTO Salary (Employee_ID,Salary,Stimulus,Deductions,DeductionsSocial,Salary_Total,Date,Worker_Type,Note) VALUES (@Employee_ID,@Salary,@Stimulus,@Deductions,@DeductionsSocial,@Salary_Total,@Date,@Worker_Type,@Note)";

                MySqlCommand cmd = new MySqlCommand(insert, dbconnection);

                cmd.Parameters.Add("@Employee_ID", MySqlDbType.Int16, 11);
                cmd.Parameters["@Employee_ID"].Value =Convert.ToInt16( comEmployee.SelectedValue);
                cmd.Parameters.Add("@Salary", MySqlDbType.Decimal, 10);
                if (txtPassword.Text != "")
                    cmd.Parameters["@Salary"].Value =Convert.ToDecimal(txtPassword.Text);
                else
                    cmd.Parameters["@Salary"].Value = 0.00;
                
                if (rEmployee.Checked)
                {
                    cmd.Parameters.Add("@Worker_Type", MySqlDbType.VarChar, 255);
                    cmd.Parameters["@Worker_Type"].Value = "موظف";
                }
                else if (rDelegate.Checked)
                {
                    cmd.Parameters.Add("@Worker_Type", MySqlDbType.VarChar, 255);
                    cmd.Parameters["@Worker_Type"].Value = "مندوب";
                }

                if (cmd.ExecuteNonQuery() == 1)
                {
                    MessageBox.Show("تم ادخال البيانات بنجاح");
                }
                clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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
                    string query = "SELECT Employee_Name,Employee_ID FROM employee";// WHERE Employee_Number = " + txtEmployee_Number.Text;
                    MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    load = false;
                    comEmployee.DataSource = dt;
                    comEmployee.DisplayMember = dt.Columns["Employee_Name"].ToString();
                    comEmployee.ValueMember = dt.Columns["Employee_ID"].ToString();
                    comEmployee.Text = "";
                    load = true;
                    //XtraTabPage xtraTabPage = getTabPage("أضافة راتب موظف");
                    //xtraTabPage.ImageOptions.Image = Properties.Resources.unsave__2_;
                
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
                    string query = "SELECT Delegate_Name,Delegate_ID FROM delegate ";//WHERE Delegate_Number = " + txtEmployee_Number.Text;
                    MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    load = false;
                    comEmployee.DataSource = dt;
                    comEmployee.DisplayMember = dt.Columns["Delegate_Name"].ToString();
                    comEmployee.ValueMember = dt.Columns["Delegate_ID"].ToString();
                    comEmployee.Text = "";
                    load = true;
                    //XtraTabPage xtraTabPage = getTabPage("أضافة راتب موظف");
                    //xtraTabPage.ImageOptions.Image = Properties.Resources.unsave__2_;
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
                    //XtraTabPage xtraTabPage = getTabPage("أضافة راتب موظف");
                    //if (!IsClear())
                    //    xtraTabPage.ImageOptions.Image = Properties.Resources.unsave__2_;
                    //else
                    //    xtraTabPage.ImageOptions.Image = null;
                }
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
                else if (item is DateTimePicker)
                {
                    ((DateTimePicker)item).Value = DateTime.Now.Date;
                }
            }
        }
        /*public XtraTabPage getTabPage(string text)
        {
            for (int i = 0; i < xtraTabControlHRContent.TabPages.Count; i++)
                if (xtraTabControlHRContent.TabPages[i].Text == text)
                {
                    return xtraTabControlHRContent.TabPages[i];
                }
            return null;
        }*/
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

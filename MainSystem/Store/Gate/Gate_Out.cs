using DevExpress.XtraEditors;
using DevExpress.XtraTab;
using MySql.Data.MySqlClient;
using System;
using System.Collections;
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

namespace MainSystem
{
    public partial class Gate_Out : Form
    {
        MySqlConnection conn;
        MainForm mainform = null;
        XtraTabControl xtraTabControlStoresContent = null;
        bool loaded = false;
        bool flag = false;
        bool flag2 = false;

        public Gate_Out(MainForm Mainform, XtraTabControl TabControlStoresContent)
        {
            try
            {
                InitializeComponent();
                mainform = Mainform;
                string constr = connection.connectionString;
                conn = new MySqlConnection(constr);
                xtraTabControlStoresContent = TabControlStoresContent;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Gate_Record_Load(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                /*string query = "select Driver_ID,Driver_Name from drivers";
                MySqlDataAdapter da = new MySqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                comDriver.DataSource = dt;
                comDriver.DisplayMember = dt.Columns["Driver_Name"].ToString();
                comDriver.ValueMember = dt.Columns["Driver_ID"].ToString();
                comDriver.SelectedIndex = -1;

                query = "select * from cars";
                da = new MySqlDataAdapter(query, conn);
                dt = new DataTable();
                da.Fill(dt);
                comCar.DataSource = dt;
                comCar.DisplayMember = dt.Columns["Car_Number"].ToString();
                comCar.ValueMember = dt.Columns["Car_ID"].ToString();
                comCar.SelectedIndex = -1;

                query = "select * from employee where Employee_Job='مسئول تعتيق'";
                da = new MySqlDataAdapter(query, conn);
                dt = new DataTable();
                da.Fill(dt);
                comEmployee.DataSource = dt;
                comEmployee.DisplayMember = dt.Columns["Employee_Name"].ToString();
                comEmployee.ValueMember = dt.Columns["Employee_ID"].ToString();
                comEmployee.SelectedIndex = -1;

                comReason.DisplayMember = "Text";
                comReason.ValueMember = "Value";
                var items = new[] {
                                new { Text = "وارد", Value = "1" },
                                new { Text = "صادر", Value = "2" }
                                };
                comReason.DataSource = items;
                comReason.SelectedIndex = -1;

                comResponsible.DisplayMember = "Text";
                comResponsible.ValueMember = "Value";
                var items2 = new[] {
                                new { Text = "شحن", Value = "1" },
                                new { Text = "عميل", Value = "2" }
                                };
                comResponsible.DataSource = items2;
                comResponsible.SelectedIndex = -1;*/
                loaded = true;
            }
            catch (Exception ex)
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

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                
                /*string query = "insert into transport (Reason,Type,Responsible,Car_ID,Car_Number,Driver_ID,Driver_Name,License_Number,Date_Enter,Store_ID,TatiqEmp_ID,Description,Employee_ID) values (@Reason,@Type,@Responsible,@Car_ID,@Car_Number,@Driver_ID,@Driver_Name,@License_Number,@Date_Enter,@Store_ID,@TatiqEmp_ID,@Description,@Employee_ID)";
                MySqlCommand com = new MySqlCommand(query, conn);
                com.Parameters.Add("@Reason", MySqlDbType.VarChar, 255);
                com.Parameters["@Reason"].Value = comReason.Text;
                com.Parameters.Add("@Type", MySqlDbType.VarChar, 255);
                com.Parameters["@Type"].Value = comType.Text;
                com.Parameters.Add("@Responsible", MySqlDbType.VarChar, 255);
                com.Parameters["@Responsible"].Value = comResponsible.Text;
                if (comCar.Text != "")
                {
                    com.Parameters.Add("@Car_ID", MySqlDbType.Int16, 11);
                    com.Parameters["@Car_ID"].Value = comCar.SelectedValue.ToString();
                    com.Parameters.Add("@Car_Number", MySqlDbType.VarChar, 255);
                    com.Parameters["@Car_Number"].Value = null;
                }
                else
                {
                    com.Parameters.Add("@Car_ID", MySqlDbType.Int16, 11);
                    com.Parameters["@Car_ID"].Value = null;
                    com.Parameters.Add("@Car_Number", MySqlDbType.VarChar, 255);
                    com.Parameters["@Car_Number"].Value = txtCar.Text;
                }
                if (comDriver.Text != "")
                {
                    com.Parameters.Add("@Driver_ID", MySqlDbType.Int16, 11);
                    com.Parameters["@Driver_ID"].Value = comDriver.SelectedValue.ToString();
                    com.Parameters.Add("@Driver_Name", MySqlDbType.VarChar, 255);
                    com.Parameters["@Driver_Name"].Value = null;
                }
                else
                {
                    com.Parameters.Add("@Driver_ID", MySqlDbType.Int16, 11);
                    com.Parameters["@Driver_ID"].Value = null;
                    com.Parameters.Add("@Driver_Name", MySqlDbType.VarChar, 255);
                    com.Parameters["@Driver_Name"].Value = txtDriver.Text;
                }
                com.Parameters.Add("@License_Number", MySqlDbType.VarChar, 255);
                com.Parameters["@License_Number"].Value = txtPermisionNum.Text;
                com.Parameters.Add("@Date_Enter", MySqlDbType.DateTime, 0);
                com.Parameters["@Date_Enter"].Value = DateTime.Now;

                string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"Store.txt");
                int storeId = Convert.ToInt16(System.IO.File.ReadAllText(path));

                com.Parameters.Add("@Store_ID", MySqlDbType.Int16, 11);
                com.Parameters["@Store_ID"].Value = storeId;
                com.Parameters.Add("@TatiqEmp_ID", MySqlDbType.Int16, 11);
                com.Parameters["@TatiqEmp_ID"].Value = comEmployee.SelectedValue.ToString();
                com.Parameters.Add("@Description", MySqlDbType.VarChar, 255);
                com.Parameters["@Description"].Value = txtDescription.Text;
                com.Parameters.Add("@Employee_ID", MySqlDbType.Int16, 11);
                com.Parameters["@Employee_ID"].Value = UserControl.EmpID;
                com.ExecuteNonQuery();

                query = "select Permission_Number from transport order by Permission_Number desc limit 1";
                com = new MySqlCommand(query, conn);
                int permissionNum = Convert.ToInt16(com.ExecuteScalar().ToString());

                for (int i = 0; i < checkedListBoxControlNum.ItemCount; i++)
                {
                    query = "insert into transport_permission(Permission_Number,Supplier_PermissionNumber) values(@Permission_Number,@Supplier_PermissionNumber)";
                    com = new MySqlCommand(query, conn);
                    com.Parameters.Add("@Permission_Number", MySqlDbType.Int16, 11);
                    com.Parameters["@Permission_Number"].Value = permissionNum;
                    com.Parameters.Add("@Supplier_PermissionNumber", MySqlDbType.Int16, 11);
                    com.Parameters["@Supplier_PermissionNumber"].Value = checkedListBoxControlNum.Items[i].Value.ToString();
                    com.ExecuteNonQuery();
                }

                clearAll();

                comType.Visible = false;
                labelType.Visible = false;
                comResponsible.Visible = false;
                labelResponsible.Visible = false;
                comEmployee.Visible = false;
                labelEmp.Visible = false;
                labelPerNum.Visible = false;
                txtPermisionNum.Visible = false;
                labelDriver.Visible = false;
                comDriver.Visible = false;
                txtDriver.Visible = false;
                labelCar.Visible = false;
                comCar.Visible = false;
                txtCar.Visible = false;
                txtLicense.Visible = false;
                labelLicense.Visible = false;
                txtDescription.Visible = false;
                labelDescription.Visible = false;
                btnAddNum.Visible = false;
                checkedListBoxControlNum.Visible = false;
                btnDeleteNum.Visible = false;*/
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            conn.Close();
        }

        private void txtBox_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    TextBox t = (TextBox)sender;
                    switch (t.Name)
                    {
                        case "txtName":
                            txtPermisionNum.Focus();
                            break;
                        case "txtAddress":
                            //txtPhone.Focus();
                            break;
                        case "txtPhone":
                            //txtDriver.Focus();
                            break;
                       

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void txtBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                XtraTabPage xtraTabPage = getTabPage("تسجيل دخول السيارات");
                if(!IsClear())
                xtraTabPage.ImageOptions.Image = Properties.Resources.unsave;
                else
                    xtraTabPage.ImageOptions.Image = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //function
        public void clear()
        {
            foreach (Control co in this.panContent.Controls)
            {
                //if (co is System.Windows.Forms.ComboBox)
                //{
                //    co.Text = "";
                //}
                if (co is TextBox)
                {
                    co.Text = "";
                }
                else if (co is CheckedListBoxControl)
                {
                    int cont = checkedListBoxControlNum.ItemCount;
                    for (int i = 0; i < cont; i++)
                    {
                        checkedListBoxControlNum.Items.RemoveAt(0);
                    }
                }
            }
        }
        public void clearAll()
        {
            foreach (Control co in this.panContent.Controls)
            {
                if (co is System.Windows.Forms.ComboBox)
                {
                    co.Text = "";

                    loaded = false;
                    flag = false;
                    flag2 = false;
                    /*comDriver.SelectedIndex = -1;
                    comCar.SelectedIndex = -1;
                    comEmployee.SelectedIndex = -1;
                    comType.SelectedIndex = -1;
                    comResponsible.SelectedIndex = -1;
                    comReason.SelectedIndex = -1;*/
                    flag2 = true;
                    flag = true;
                    loaded = true;
                }
                else if (co is TextBox)
                {
                    co.Text = "";
                }
                else if (co is CheckedListBoxControl)
                {
                    int cont = checkedListBoxControlNum.ItemCount;
                    for (int i = 0; i < cont; i++)
                    {
                        checkedListBoxControlNum.Items.RemoveAt(0);
                    }
                }
            }
        }
        public XtraTabPage getTabPage(string text)
        {
            for (int i = 0; i < xtraTabControlStoresContent.TabPages.Count; i++)
                if (xtraTabControlStoresContent.TabPages[i].Text == text)
                {
                    return xtraTabControlStoresContent.TabPages[i];
                }
            return null;
        }
        public bool IsClear()
        {
            if (/*txtDriver.Text == "" &&*/ txtPermisionNum.Text == "" /*&& txtPhone.Text == ""*/)
                return true;
            else
                return false;
        }
    }
   
}

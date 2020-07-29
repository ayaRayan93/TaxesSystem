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

namespace TaxesSystem
{
    public partial class CarPartnerIncomeUpdate : Form
    {
        MySqlConnection dbconnection;
        double gate;
        double Taateg;
        double SafayCar_Number;
        double totalSafay,totalGate;
        int ID;
        MySqlDataAdapter da1;
        DataTable dt1;
        MySqlCommandBuilder combuilder;
        CarPartnerIncomes carIncomes;
        XtraTabControl xtraTabControlCarsContent;
        List<string> data;
        object datasorce;
        bool load = false;
        public CarPartnerIncomeUpdate(int id, CarPartnerIncomes carIncomes, XtraTabControl xtraTabControlCarsContent)
        {
            try
            {
                ID = id;
                this.carIncomes = carIncomes;
                this.xtraTabControlCarsContent = xtraTabControlCarsContent;
                InitializeComponent();
                dbconnection = new MySqlConnection(connection.connectionString);
                data = new List<string>();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Form5_Load(object sender, EventArgs e)
        {
            try
            {
                dbconnection.Open();
                string query = "select * from cars";
                MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                DataTable dt = new DataTable();
                da.Fill(dt);
                comCarNumber.DataSource = dt;
                comCarNumber.DisplayMember = dt.Columns["Car_Number"].ToString();
                comCarNumber.ValueMember = dt.Columns["Car_ID"].ToString();
                comCarNumber.Text = "";
                
                query = "select * from car_partner_income inner join cars on car_partner_income.Car_ID=cars.Car_ID  where CarPartner_Income_ID=" + ID+"";
                MySqlCommand com = new MySqlCommand(query, dbconnection);
                MySqlDataReader dr = com.ExecuteReader();
                while (dr.Read())
                {
                    txtAddress.Text = dr["ClientName"].ToString();
                    data.Add(dr["ClientName"].ToString());
                    txtNoCarton.Text = dr["NoCarton"].ToString();
                    data.Add(dr["NoCarton"].ToString());
                    txtNoColumns.Text = dr["NoColumns"].ToString();
                    data.Add(dr["NoColumns"].ToString());
                    txtNoComp.Text = dr["NoCompinations"].ToString();
                    data.Add(dr["NoCompinations"].ToString());
                    txtNoDocks.Text = dr["NoDocks"].ToString();
                    data.Add(dr["NoDocks"].ToString());
                    txtNoPanio.Text = dr["NoPanio"].ToString();
                    data.Add(dr["NoPanio"].ToString());
                    txtNoSets.Text = dr["NoSets"].ToString();
                    data.Add(dr["NoSets"].ToString());
                    txtNote.Text = dr["Note"].ToString();
                    data.Add(dr["Note"].ToString());
                    comCarNumber.Text = dr["Car_Number"].ToString();
                    data.Add(dr["Car_Number"].ToString());
                    dateTimePicker1.Text = dr["Date"].ToString();
                    string str= dr["Date"].ToString();
                    data.Add(str.Split(' ')[0]);
                }
                dr.Close();
              
                query = "select Permission_Number as 'رقم الاذن' from car_partner_permission where CarPartner_Income_ID=" + ID + "";
                com = new MySqlCommand(query, dbconnection);
                dr = com.ExecuteReader();
                while (dr.Read())
                {
                    int n = dataGridView1.Rows.Add();
                    int n1 = dataGridView2.Rows.Add();
                    dataGridView1.Rows[n].Cells[0].Value = Convert.ToDouble(dr["رقم الاذن"].ToString());
                    dataGridView2.Rows[n1].Cells[0].Value = Convert.ToDouble(dr["رقم الاذن"].ToString());
                }
                dr.Close();
            
                load = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (ID > -1)
                {
                    dbconnection.Close();
                    dbconnection.Open();
                    
                    string query = "update car_partner_income set Car_ID ='" + comCarNumber.SelectedValue + "' ,ClientName='" + txtAddress.Text + "',NoCarton='" + txtNoCarton.Text + "',NoSets=" + txtNoSets.Text + ",NoDocks=" + txtNoDocks.Text + ",NoColumns=" + txtNoColumns.Text + ",NoCompinations=" + txtNoComp.Text + ",NoPanio=" + txtNoPanio.Text + " where CarPartner_Income_ID=" + ID + "";
                    MySqlCommand com = new MySqlCommand(query, dbconnection);
                    com.ExecuteNonQuery();

                    query = "delete from  car_partner_permission  where CarPartner_Income_ID=" + ID + "";
                    com = new MySqlCommand(query, dbconnection);
                    com.ExecuteNonQuery();
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        if (ID > 0)
                        {
                            query = "insert into car_partner_permission (CarPartner_Income_ID,Permission_Number) values (@CarPartner_Income_ID,@Permission_Number)";
                            com = new MySqlCommand(query, dbconnection);

                            com.Parameters.Add("@CarPartner_Income_ID", MySqlDbType.Int16);
                            com.Parameters["@CarPartner_Income_ID"].Value = ID;
                            com.Parameters.Add("@Permission_Number", MySqlDbType.VarChar);
                            com.Parameters["@Permission_Number"].Value = row.Cells[0].Value;
                            com.ExecuteNonQuery();
                        }
                    }


                    MessageBox.Show("Done");
                    carIncomes.displayData();
                }
                else
                {
                    MessageBox.Show("select row please");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                DataGridViewRow row = dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex];
                row.DataGridView.Rows.Remove(row);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txtPermissionNumber_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    if (txtPermissionNumber.Text != "")
                    {
                        int n = dataGridView1.Rows.Add();
                      
                        foreach (DataGridViewColumn item in dataGridView1.Columns)
                        {
                            dataGridView1.Rows[n].Cells[item.Index].Value = txtPermissionNumber.Text;
                        }
                        txtPermissionNumber.Clear();
                        txtPermissionNumber.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnAddPermission_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtPermissionNumber.Text != "")
                {
                    int n = dataGridView1.Rows.Add();
                    foreach (DataGridViewColumn item in dataGridView1.Columns)
                    {
                        dataGridView1.Rows[n].Cells[item.Index].Value = txtPermissionNumber.Text;
                    }
                    txtPermissionNumber.Clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txtBox_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    TextBox txtBox = (TextBox)sender;
                    switch (txtBox.Name)
                    {
                        case "txtAddress":
                            txtNoCarton.Focus();
                            break;
                        case "txtNoCarton":
                            txtNoPanio.Focus();
                            break;
                        case "txtNoPanio":
                            txtNoSets.Focus();
                            break;
                        case "txtNoSets":
                            txtNoColumns.Focus();
                            break;
                        case "txtNoColumns":
                            txtNoDocks.Focus();
                            break;
                        case "txtNoDocks":
                            txtNoComp.Focus();
                            break;
                        case "txtNoComp":
                            txtNote.Focus();
                            break;
                        case "txtNote":
                            comCarNumber.Focus();
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (load)
                {
                    XtraTabPage xtraTabPage = getTabPage("تعديل إيراد السيارات الخاصة");
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

        private void txtPermissionNumber_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (load)
                {
                    XtraTabPage xtraTabPage = getTabPage("تعديل إيراد السيارات الخاصة");
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

        private void dataGridView1_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            try
            {
                if (load)
                {
                    XtraTabPage xtraTabPage = getTabPage("تعديل إيراد السيارات الخاصة");
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

        private void dataGridView1_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            try
            {
                if (load)
                {
                    XtraTabPage xtraTabPage = getTabPage("تعديل إيراد السيارات الخاصة");
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
        //functions
        public XtraTabPage getTabPage(string text)
        {
            for (int i = 0; i < xtraTabControlCarsContent.TabPages.Count; i++)
                if (xtraTabControlCarsContent.TabPages[i].Text == text)
                {
                    return xtraTabControlCarsContent.TabPages[i];
                }
            return null;
        }

        private bool IsClear()
        {
            DateTime date = dateTimePicker1.Value.Date;
            string d = date.ToString("yyyy-MM-dd");

            if (comCarNumber.Text == data[9] &&
            txtAddress.Text == data[0] && txtNote.Text == data[8] && d == data[10] &&
            txtNoSets.Text == data[7] && txtNoPanio.Text == data[6] && txtNoDocks.Text == data[4] && txtNoComp.Text == data[3] && txtNoColumns.Text == data[2] && txtNoCarton.Text == data[1] 
            && Isdatabase())
            {
                return true;
            }
            else
            {
                return false;
            }
            
        }

        public bool Isdatabase()
        {
            bool flag = false;
            if (dataGridView2.Rows.Count == dataGridView1.Rows.Count)
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    foreach (DataGridViewRow row1 in dataGridView2.Rows)
                    {
                        if (row.Cells[0].Value.Equals(row1.Cells[0].Value))
                            flag=true;
                    }
                    if (flag == false)
                        return false;
                }
            }
            else
            {
                return false;
            }
            return true;
        }

       
        

  
    }
}

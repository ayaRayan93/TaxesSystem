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

namespace CCCRequestsSystem
{
    public partial class SearchRecive_Date : Form
    {
        MySqlConnection dbconnection;
        bool loaded = false;
        DataGridViewRow row1;

        public SearchRecive_Date()
        {
            InitializeComponent();
            dbconnection = new MySqlConnection(connection.connectionString);
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                dbconnection.Open();
                DateTime date = dateTimePicker1.Value.Date;
                string d = date.ToString("yyyy-MM-dd");
                DateTime date2 = dateTimePicker2.Value.Date;
                string d2 = date2.ToString("yyyy-MM-dd");
                int branchID = 0;
                string query = "";
                if (int.TryParse(txtBranchID.Text, out branchID))
                {
                    query = "select Branch_Name as 'الفرع',BranchBillNumber as 'رقم الفاتورة',Employee_Name as 'الموظف المسئول',Supplier_Name as 'المورد',Store_Name as 'المخزن',Request_Date as 'تاريخ الطلب',Receive_Date as'تاريخ الاستلام' from orders where orders.Branch_ID=" + branchID + " and Receive_Date >='" + d + "' and Receive_Date <='" + d2 + "'";
                    
                }
                else
                {
                    query = "select Branch_Name as 'الفرع', BranchBillNumber as 'رقم الفاتورة',Employee_Name as 'الموظف المسئول',Supplier_Name as 'المورد',Store_Name as 'المخزن',Request_Date as 'تاريخ الطلب',Receive_Date as'تاريخ الاستلام' from orders where Receive_Date >='" + d + "' and Receive_Date <='" + d2 + "'";
                    
                }
                MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                row1 = dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex];
                
                string query = "SELECT distinct order_details.Code as 'كود',type.Type_Name as 'النوع', factory.Factory_Name as 'المصنع' ,groupo.Group_Name as 'المجموعة', product.Product_Name as 'المنتج' ,data.Colour as 'لون', data.Size as 'المقاس', data.Sort as 'الفرز',data.Classification as 'التصنيف', data.Description as 'الوصف',order_details.Quantity as 'الكمية' FROM orders  INNER JOIN order_details ON orders.order_ID = order_details.order_ID  INNER JOIN data on order_details.Code = data.Code INNER JOIN type ON type.Type_ID = data.Type_ID INNER JOIN product ON product.Product_ID = data.Product_ID INNER JOIN factory ON data.Factory_ID = factory.Factory_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID  inner join storage on storage.Code=order_details.Code where order_details.Order_ID=" + row1.Cells[1].Value.ToString();
                MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView2.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btnMainForm_Click(object sender, EventArgs e)
        {
            //MainForm f = new MainForm();
            //f.Show();
            //this.Hide();
        }

        private void SearchRecive_Date_FormClosed(object sender, FormClosedEventArgs e)
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

        private void SearchRecive_Date_Load(object sender, EventArgs e)
        {
            try
            {
                string query = "select * from branch";
                MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                DataTable dt = new DataTable();
                da.Fill(dt);
                comBranch.DataSource = dt;
                comBranch.DisplayMember = dt.Columns["Branch_Name"].ToString();
                comBranch.ValueMember = dt.Columns["Branch_ID"].ToString();
                comBranch.Text = "";
                txtBranchID.Text = "";

                loaded = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void comBranch_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (loaded)
                    txtBranchID.Text = comBranch.SelectedValue.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txtBranchID_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    dbconnection.Open();
                    string query = "select Branch_Name from branch where Branch_ID=" + txtBranchID.Text + "";
                    MySqlCommand com = new MySqlCommand(query, dbconnection);
                    if (com.ExecuteScalar() != null)
                    {
                        Name = (string)com.ExecuteScalar();
                        comBranch.Text = Name;
                        comBranch.SelectedValue = txtBranchID.Text;
                    }
                    else
                    {
                        MessageBox.Show("there is no item with this id");

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                dbconnection.Close();
            }
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            try
            {
                string Branch_Name = row1.Cells[0].Value.ToString();
                int BranchBillNumber = Convert.ToInt16(row1.Cells[1].Value.ToString());
                string Employee_Name = row1.Cells[2].Value.ToString();
                string Supplier_Name = row1.Cells[3].Value.ToString();
                string Store_Name = row1.Cells[4].Value.ToString();
                DateTime Request_Date = Convert.ToDateTime(row1.Cells[5].Value.ToString());
                DateTime Receive_Date = Convert.ToDateTime(row1.Cells[6].Value.ToString());

                DataTable dt = new DataTable();
                
                string query = "SELECT distinct order_details.Code,type.Type_Name,factory.Factory_Name,groupo.Group_Name,product.Product_Name,data.Colour,data.Size,data.Sort,data.Classification, data.Description,order_details.Quantity FROM orders  INNER JOIN order_details ON orders.order_ID = order_details.order_ID  INNER JOIN data on order_details.Code = data.Code INNER JOIN type ON type.Type_ID = data.Type_ID INNER JOIN product ON product.Product_ID = data.Product_ID INNER JOIN factory ON data.Factory_ID = factory.Factory_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID  inner join storage on storage.Code=order_details.Code where order_details.Order_ID=" + row1.Cells[1].Value.ToString();
                MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                da.Fill(dt);

                /*RequestStored_CrystalReport f = new RequestStored_CrystalReport(dt, Branch_Name, BranchBillNumber, Employee_Name, Supplier_Name, Store_Name, Request_Date, Receive_Date);
                f.Show();
                this.Hide();*/
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}


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
    public partial class BillCancelForm : Form
    {
        private MySqlConnection dbconnection;
        public BillCancelForm()
        {
            InitializeComponent();
            dbconnection = new MySqlConnection(connection.connectionString);
        }

        private void BillCancelForm_Load(object sender, EventArgs e)
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
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                dbconnection.Open();
                DisplayData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }

        private void btnCancelBill_Click(object sender, EventArgs e)
        {
            try
            {
                dbconnection.Open();
                if (gridView1.SelectedRowsCount > 0)
                {
                    DataRow dataRow = gridView1.GetDataRow(gridView1.GetSelectedRows()[0]);

                    if (dataRow.ItemArray[6].ToString() == "لم يتم")
                    {
                        int id = Convert.ToInt32(dataRow.ItemArray[0]);
                        string query = "delete from customer_bill where CustomerBill_ID=" + id;
                        MySqlCommand com = new MySqlCommand(query, dbconnection);
                        com.ExecuteNonQuery();
                        DisplayData();
                    }
                    else
                    {
                        MessageBox.Show("الفاتورة تم تسلمها");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }

        private void comBranch_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                txtBranchID.Text = comBranch.SelectedValue.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txtBranchID_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                dbconnection.Open();
                if (e.KeyCode == Keys.Enter)
                {
                    string query = "select Branch_Name from branch where Branch_ID=" + txtBranchID.Text;
                    MySqlCommand comand = new MySqlCommand(query, dbconnection);
                    string Branch_Name = comand.ExecuteScalar().ToString();
                    dbconnection.Close();
                    comBranch.Text = Branch_Name;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }

        private void txtBranchBillNum_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    DisplayData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //function display data
        public void DisplayData()
        {
            DateTime date = dateTimeFrom.Value;
            string d = date.ToString("yyyy-MM-dd HH:mm:ss");
            DateTime date2 = dateTimeTo.Value;
            string d2 = date2.ToString("yyyy-MM-dd HH:mm:ss");
            string subQuery = "", query = "";
            if (txtBranchBillNum.Text != "" && txtBranchID.Text != "")
            {
                string query1 = "select CustomerBill_ID from customer_bill where  Branch_BillNumber=" + txtBranchBillNum.Text + " and Branch_ID=" + txtBranchID.Text;
                MySqlCommand com1 = new MySqlCommand(query1, dbconnection);
                int id = Convert.ToInt32(com1.ExecuteScalar());
                subQuery = " and customer_bill.CustomerBill_ID=" + id;
                query = " select customer_bill.CustomerBill_ID,Type_Buy ,case when Paid_Status=1 then 'تم' else 'لم يتم' end as Paid_Status ,case when Shipped=0 then 'لم يتم' else 'تم' end as Shipped ,case when Delivered=0 then 'لم يتم' else 'تم' end as Delivered  from customer_bill inner join shipping on shipping.CustomerBill_ID=customer_bill.CustomerBill_ID inner join branch on branch.Branch_ID=customer_bill.Branch_ID    where  Bill_Date between '" + d + "' and '" + d2 + "'" + subQuery;
                MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                DataTable dt = new DataTable();
                da.Fill(dt);
                gridControl1.DataSource = dt;
                gridView1.Columns[0].Visible = false;
                gridView1.Columns[1].Visible = false;
                gridView1.Columns[2].Visible = false;
            }
            else if (txtBranchID.Text != "")
            {
                query = "select customer_bill.CustomerBill_ID, customer_bill.Branch_BillNumber ,Type_Buy ,case when Paid_Status=1 then 'تم' else 'لم يتم' end as Paid_Status ,case when Shipped=0 then 'لم يتم' else 'تم' end as Shipped ,case when Delivered=0 then 'لم يتم' else 'تم' end as Delivered  from customer_bill inner join shipping on shipping.CustomerBill_ID=customer_bill.CustomerBill_ID inner join branch on branch.Branch_ID=customer_bill.Branch_ID    where  Bill_Date between '" + d + "' and '" + d2 + "' and  customer_bill.Branch_ID=" + txtBranchID.Text;
                MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                DataTable dt = new DataTable();
                da.Fill(dt);
                gridControl1.DataSource = dt;
                gridView1.Columns[0].Visible = false;
                gridView1.Columns[1].Visible = false;
            }
            else
            {
                query = "select customer_bill.CustomerBill_ID, branch.Branch_Name as 'الفرع' ,customer_bill.Branch_BillNumber as 'رقم الفاتورة' ,Type_Buy as 'نوع الفاتورة' ,case when Paid_Status=1 then 'تم' else 'لم يتم' end as 'الدفع' ,case when Shipped=0 then 'لم يتم' else 'تم' end as 'الشحن' ,case when Delivered=0 then 'لم يتم' else 'تم' end as 'التسليم'  from customer_bill inner join shipping on shipping.CustomerBill_ID=customer_bill.CustomerBill_ID inner join branch on branch.Branch_ID=customer_bill.Branch_ID    where  Bill_Date between '" + d + "' and '" + d2 + "'";
                MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                DataTable dt = new DataTable();
                da.Fill(dt);
                gridControl1.DataSource = dt;
                gridView1.Columns[0].Visible = false;
            }
            
        }

        private void newChoose_Click(object sender, EventArgs e)
        {
            try
            {
                dateTimeFrom.Value = DateTime.Now.Date;
                dateTimeTo.Value = DateTime.Now.Date;
                comBranch.Text = "";
                txtBranchID.Text = "";
                txtBranchBillNum.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}

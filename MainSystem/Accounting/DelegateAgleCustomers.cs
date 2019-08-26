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

namespace MainSystem
{
    public partial class DelegateAgleCustomers : Form
    {
        private MySqlConnection dbconnection;
        bool loaded = false;
        MainForm MainForm;
        public DelegateAgleCustomers(MainForm MainForm)
        {
            try
            {
                InitializeComponent();
                dbconnection = new MySqlConnection(connection.connectionString);
                this.MainForm = MainForm;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void DelegateLeastBills_Load(object sender, EventArgs e)
        {
            try
            {
                dbconnection.Open();
                string query = "select * from delegate ";
                MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                DataTable dt = new DataTable();
                da.Fill(dt);
                comDelegate.DataSource = dt;
                comDelegate.DisplayMember = dt.Columns["Delegate_Name"].ToString();
                comDelegate.ValueMember = dt.Columns["Delegate_ID"].ToString();
                comDelegate.Text = "";
                txtDelegateID.Text = "";

                query = "select * from branch";
                da = new MySqlDataAdapter(query, dbconnection);
                dt = new DataTable();
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
            dbconnection.Close();
        }
        private void comDelegate_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (loaded)
                {
                    try
                    {
                        txtDelegateID.Text = comDelegate.SelectedValue.ToString();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
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
                {
                    txtBranchID.Text = comBranch.SelectedValue.ToString();
                    dbconnection.Open();
                    loaded = false;
                    string query = "select * from delegate where Branch_ID=" + txtBranchID.Text;
                    MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    comDelegate.DataSource = dt;
                    comDelegate.DisplayMember = dt.Columns["Delegate_Name"].ToString();
                    comDelegate.ValueMember = dt.Columns["Delegate_ID"].ToString();
                    comDelegate.Text = "";
                    txtDelegateID.Text = "";
                    loaded = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }
        private void txtBranchID_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    dbconnection.Close();
                    string query = "select Branch_Name from branch where Branch_ID=" + txtBranchID.Text;
                    MySqlCommand comand = new MySqlCommand(query, dbconnection);
                    dbconnection.Open();
                    string Branch_Name = comand.ExecuteScalar().ToString();
                    dbconnection.Close();
                    comBranch.Text = Branch_Name;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        private void txtDelegateID_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    dbconnection.Open();
                    string query = "select Delegate_Name from delegate where Delegate_ID=" + txtDelegateID.Text + "";
                    MySqlCommand com = new MySqlCommand(query, dbconnection);
                    if (com.ExecuteScalar() != null)
                    {
                        Name = (string)com.ExecuteScalar();
                        comDelegate.Text = Name;
                    }
                    else
                    {
                        MessageBox.Show("there is no item with this id");
                        dbconnection.Close();
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtBranchID.Text != "" && txtDelegateID.Text != "")
                {
                    DateTime date = dateTimeFrom.Value;
                    string d = date.ToString("yyyy-MM-dd HH:mm:ss");
                    DateTime date2 = dateTimeTo.Value;
                    string d2 = date2.ToString("yyyy-MM-dd HH:mm:ss");
                    DataTable mdt = new DataTable();
                    string query = "select distinct concat(transitions.Customer_Name,' ',transitions.Customer_ID) as 'مهندس/مقاول', concat(transitions.Client_Name,' ',transitions.Client_ID) as 'العميل' ,Date as 'تاريخ السداد',Transition as 'نوع السداد', Amount as 'السداد'   from transitions where  transitions.TransitionBranch_ID=" + txtBranchID.Text + " and Delegate_ID=" + txtDelegateID.Text + "  and Date between '" + d + "' and '" + d2 + "' and Type='آجل'";
                    MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                    DataTable dt1 = new DataTable();
                    da.Fill(dt1);
                    //mdt = dt1.Copy();
                    //query = "select distinct concat(customer_return_bill.Customer_Name,' ',customer_return_bill.Customer_ID) as 'مهندس/مقاول', concat(customer_return_bill.Client_Name,' ',customer_return_bill.Client_ID) as 'العميل' ,Bill_Date as 'تاريخ الفاتورة',customer_bill.Branch_BillNumber as 'رقم الفاتورة',Total_CostAD as 'اجمالي الفاتورة','" + 0.0+ "'  as 'السداد',case when Transition ='سحب' then Amount end as 'مرتد السداد'   from  transitions inner join  customer_return_bill on customer_return_bill.Branch_BillNumber=transitions.Bill_Number inner join customer_bill on customer_bill. inner join product_bill on product_bill.CustomerReturnBill_ID=customer_return_bill.CustomerReturnBill_ID  where Paid_Status=2 and transitions.Branch_ID=" + txtBranchID.Text + " and Delegate_ID=" + txtDelegateID.Text + " and customer_return_bill.Type_Buy='كاش' and Bill_Date between '" + d + "' and '" + d2 + "' ";
                    //da = new MySqlDataAdapter(query, dbconnection);
                    //DataTable dt2 = new DataTable();
                    //da.Fill(dt2);
                    //mdt.Merge(dt2,true,MissingSchemaAction.Ignore);
                    gridControl1.DataSource = dt1;
                }
                else
                {
                    MessageBox.Show("ادخل الفرع واسم المندوب");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void newChoose_Click(object sender, EventArgs e)
        {
            try
            {
                txtBranchID.Text = "";
                comBranch.Text = "";
                txtDelegateID.Text = "";
                comDelegate.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btnReport_Click(object sender, EventArgs e)
        {
            try
            {
                dataX d = new dataX(dateTimeFrom.Text, dateTimeTo.Text, comDelegate.Text, "");
                MainForm.displayDelegateReport(gridControl1, d);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

    }
}

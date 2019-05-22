using DevExpress.XtraGrid.Views.Grid;
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

namespace MainSystem.Sales.accounting
{
    public partial class CustomerTaswayaReport : Form
    {
        MySqlConnection dbconnection;
        MainForm salesMainForm;
        public CustomerTaswayaReport(MainForm salesMainForm)
        {
            try
            {
                InitializeComponent();
                dbconnection = new MySqlConnection(connection.connectionString);
                this.salesMainForm = salesMainForm;
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
                DisplayCustomerTaswaya();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            dbconnection.Close();
        }
        private void txtTaswayaCode_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    dbconnection.Open();
                    DisplayCustomerTaswaya(Convert.ToInt16(txtTaswayaCode.Text));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            dbconnection.Close();
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                salesMainForm.bindTaswayaCustomersForm();
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
                DataRowView row1 = (DataRowView)(((GridView)gridControl1.MainView).GetRow(((GridView)gridControl1.MainView).GetSelectedRows()[0]));
                salesMainForm.bindUpdateTaswayaCustomersForm(row1,this);
            }
            catch
            {
                MessageBox.Show("يجب تحديد البند المراد تعديله.");
            }
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                dbconnection.Open();
                DataRowView row1 = (DataRowView)(((GridView)gridControl1.MainView).GetRow(((GridView)gridControl1.MainView).GetSelectedRows()[0]));
                DialogResult dialogResult = MessageBox.Show("Are you sure you want to delete the item?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                    DataTable dataTable = (DataTable)gridControl1.DataSource;
                    for (int i = 0; i < dataTable.Rows.Count; i++)
                    {
                        string query = "delete from customer_taswaya where CustomerTaswaya_ID='" + dataTable.Rows[i][0].ToString() + "'";
                        MySqlCommand comand = new MySqlCommand(query, dbconnection);
                        comand.ExecuteNonQuery();
                        UserControl.ItemRecord("customer_taswaya", "حذف", Convert.ToInt16(row1[0].ToString()), DateTime.Now, "", dbconnection);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }

        //functions
        public void DisplayCustomerTaswaya()
        {
            DateTime date = dateTimeFrom.Value;
            string d = date.ToString("yyyy-MM-dd HH:mm:ss");
            DateTime date2 = dateTimeTo.Value;
            string d2 = date2.ToString("yyyy-MM-dd HH:mm:ss");
            string qeury = "select CustomerTaswaya_ID as 'الكود',c2.Customer_Name as 'المهندس/مقاول/تاجر',c1.Customer_Name as 'العميل',Taswaya_Type as 'نوع التسوية',Money_Paid as 'قيمة التسوية',Info as 'بيان',Date as 'التاريخ' from customer_taswaya inner join customer as c1 on c1.Customer_ID=customer_taswaya.Client_ID inner join customer as c2 on c2.Customer_ID=customer_taswaya.Customer_ID  where Date between '" + d + "' and '" + d2 + "'";
            MySqlDataAdapter adapter = new MySqlDataAdapter(qeury, dbconnection);
            DataSet dataSet = new DataSet();
            adapter.Fill(dataSet);

            gridControl1.DataSource = dataSet.Tables[0];
        }
        public void DisplayCustomerTaswaya(int id)
        {
            string qeury = "select CustomerTaswaya_ID as 'الكود',c2.Customer_Name as 'المهندس/مقاول/تاجر',c1.Customer_Name as 'العميل',Taswaya_Type as 'نوع التسوية',Money_Paid as 'قيمة التسوية',Info as 'بيان',Date as 'التاريخ' from customer_taswaya inner join customer as c1 on c1.Customer_ID=customer_taswaya.Client_ID inner join customer as c2 on c2.Customer_ID=customer_taswaya.Customer_ID where CustomerTaswaya_ID="+id;
            MySqlDataAdapter adapter = new MySqlDataAdapter(qeury, dbconnection);
            DataSet dataSet = new DataSet();
            adapter.Fill(dataSet);

            gridControl1.DataSource = dataSet.Tables[0];
        }

     
    }
}

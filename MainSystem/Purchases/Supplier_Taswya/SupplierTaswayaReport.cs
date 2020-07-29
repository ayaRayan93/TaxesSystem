using DevExpress.XtraGrid.Views.Grid;
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
    public partial class SupplierTaswayaReport : Form
    {
        MySqlConnection dbconnection;
        MainForm accountingMainForm;
        public SupplierTaswayaReport(MainForm AccountingMainForm, XtraTabControl xtraTabControlAccounting)
        {
            try
            {
                InitializeComponent();
                dbconnection = new MySqlConnection(connection.connectionString);
                this.accountingMainForm = AccountingMainForm;
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
                DisplaySupplierTaswaya();
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
                    DisplaySupplierTaswaya(Convert.ToInt32(txtTaswayaCode.Text));
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
                accountingMainForm.bindTaswayaSupplierForm();
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
                accountingMainForm.bindUpdateTaswayaSupplierForm(row1,this);
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
                    string query = "select Money from supplier_rest_money where Supplier_ID=" + row1["Supplier_ID"].ToString();
                    MySqlCommand com = new MySqlCommand(query, dbconnection);
                    if (com.ExecuteScalar() != null)
                    {
                        double restMoney = Convert.ToDouble(com.ExecuteScalar());
                        
                        if (row1["نوع التسوية"].ToString() == "اضافة")
                        {
                            query = "update supplier_rest_money set Money=" + (restMoney - Convert.ToDouble(row1["قيمة التسوية"].ToString())) + " where Supplier_ID=" + row1["Supplier_ID"].ToString();
                        }
                        else
                        {
                            query = "update supplier_rest_money set Money=" + (restMoney + Convert.ToDouble(row1["قيمة التسوية"].ToString())) + " where Supplier_ID=" + row1["Supplier_ID"].ToString();
                        }
                        com = new MySqlCommand(query, dbconnection);
                        com.ExecuteNonQuery();
                    }

                    query = "delete from supplier_taswaya where SupplierTaswaya_ID=" + row1[0].ToString() + "";
                    com = new MySqlCommand(query, dbconnection);
                    com.ExecuteNonQuery();
                    UserControl.ItemRecord("supplier_taswaya", "حذف", Convert.ToInt32(row1[0].ToString()), DateTime.Now, "", dbconnection);

                    gridView1.DeleteSelectedRows();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }

        //functions
        public void DisplaySupplierTaswaya()
        {
            string qeury = "select SupplierTaswaya_ID as 'الكود',supplier.Supplier_Name as 'المورد',Taswaya_Type as 'نوع التسوية',Money_Paid as 'قيمة التسوية',Info as 'بيان',Date as 'التاريخ',supplier_taswaya.Supplier_ID from supplier_taswaya inner join supplier on supplier.Supplier_ID=supplier_taswaya.Supplier_ID";
            MySqlDataAdapter adapter = new MySqlDataAdapter(qeury, dbconnection);
            DataSet dataSet = new DataSet();
            adapter.Fill(dataSet);
            gridControl1.DataSource = dataSet.Tables[0];
            gridView1.Columns["Supplier_ID"].Visible = false;
        }
        public void DisplaySupplierTaswaya(int id)
        {
            string qeury = "select SupplierTaswaya_ID as 'الكود',supplier.Supplier_Name as 'المورد',Taswaya_Type as 'نوع التسوية',Money_Paid as 'قيمة التسوية',Info as 'بيان',Date as 'التاريخ',supplier_taswaya.Supplier_ID from supplier_taswaya inner join supplier on supplier.Supplier_ID=supplier_taswaya.Supplier_ID where SupplierTaswaya_ID=" + id;
            MySqlDataAdapter adapter = new MySqlDataAdapter(qeury, dbconnection);
            DataSet dataSet = new DataSet();
            adapter.Fill(dataSet);
            gridControl1.DataSource = dataSet.Tables[0];
            gridView1.Columns["Supplier_ID"].Visible = false;
        }
    }
}

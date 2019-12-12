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

namespace MainSystem
{
    public partial class SupplierSoonPayments_Report : Form
    {
        MySqlConnection dbconnection;
        bool loaded = false;
        //DataRow row1 = null;
        XtraTabControl tabControlContent;

        public SupplierSoonPayments_Report(MainForm mainform, XtraTabControl TabControlContent)
        {
            InitializeComponent();
            dbconnection = new MySqlConnection(connection.connectionString);
            tabControlContent = TabControlContent;
        }
        private void requestStored_Load(object sender, EventArgs e)
        {
            try
            {
                string query = "select * from supplier";
                MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                DataTable dt = new DataTable();
                da.Fill(dt);
                comSupplier.DataSource = dt;
                comSupplier.DisplayMember = dt.Columns["Supplier_Name"].ToString();
                comSupplier.ValueMember = dt.Columns["Supplier_ID"].ToString();
                comSupplier.Text = "";
                txtSupplierID.Text = "";

                loaded = true;
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
                if (loaded)
                {
                    int supplierID = 0;
                    txtSupplierID.Text = comSupplier.SelectedValue.ToString();
                    if (int.TryParse(txtSupplierID.Text, out supplierID) && comSupplier.SelectedValue != null)
                    {
                        search(Convert.ToInt32(comSupplier.SelectedValue.ToString()));
                    }
                    else
                    {
                        MessageBox.Show("تاكد من البيانات");
                    }
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
                    dbconnection.Open();
                    string query = "select Supplier_Name from supplier where Supplier_ID=" + txtSupplierID.Text + "";
                    MySqlCommand com = new MySqlCommand(query, dbconnection);
                    if (com.ExecuteScalar() != null)
                    {
                        Name = (string)com.ExecuteScalar();
                        comSupplier.Text = Name;
                        comSupplier.SelectedValue = txtSupplierID.Text;
                        search(Convert.ToInt32(comSupplier.SelectedValue.ToString()));
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

        private void btnDisplay_Click(object sender, EventArgs e)
        {
            try
            {
                loaded = false;
                comSupplier.SelectedIndex = -1;
                txtSupplierID.Text = "";
                loaded = true;
                search(0);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (gridView1.SelectedRowsCount > 0)
                {
                    dbconnection.Open();
                    for (int i = 0; i < gridView1.SelectedRowsCount; i++)
                    {
                        string query = "update supplier_transitions set Paid='تحت التحصيل' where SupplierTransition_ID=" + gridView1.GetRowCellDisplayText(gridView1.GetSelectedRows()[i], gridView1.Columns[0]);
                        MySqlCommand com = new MySqlCommand(query, dbconnection);
                        com.ExecuteNonQuery();
                        if (comSupplier.SelectedValue != null)
                        {
                            search(Convert.ToInt32(comSupplier.SelectedValue.ToString()));
                        }
                        else
                        {
                            search(0);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }

        public void search(int supplierId)
        {
            DataSet sourceDataSet = new DataSet();
            MySqlDataAdapter adapterPerm = null;
            DateTime today = DateTime.Today;
            DateTime start = new DateTime(today.Year, today.Month, today.Day);
            DateTime end = start.AddMonths(1);
            if (supplierId == 0)
            {
                string query = "SELECT supplier_transitions.SupplierTransition_ID as 'التسلسل',supplier_transitions.Supplier_ID,supplier.Supplier_Name as 'المورد',supplier_transitions.Bank_ID,supplier_transitions.Bank_Name as 'الخزينة',supplier_transitions.Amount as 'المبلغ',supplier_transitions.Date as 'التاريخ',supplier_transitions.Payment_Method as 'طريقة الدفع',supplier_transitions.Payday as 'تاريخ الاستحقاق',supplier_transitions.Check_Number as 'رقم الشيك/الكارت',supplier_transitions.Visa_Type as 'نوع الكارت',supplier_transitions.Operation_Number as 'رقم العملية',supplier_transitions.Data as 'البيان',Paid as 'الحالة' FROM supplier_transitions inner join supplier on supplier.Supplier_ID=supplier_transitions.Supplier_ID where supplier_transitions.Transition='سداد' and (supplier_transitions.Paid='لا' or supplier_transitions.Paid='تحت التحصيل') and supplier_transitions.Error=0 and Date(supplier_transitions.Payday) >= '" + DateTime.Now.Date.ToString("yyyy-MM-dd") + "' and Date(supplier_transitions.Payday) <= '" + end.Date.ToString("yyyy-MM-dd") + "' order by supplier_transitions.Date";
                adapterPerm = new MySqlDataAdapter(query, dbconnection);
            }
            else
            {
                string query = "SELECT supplier_transitions.SupplierTransition_ID as 'التسلسل',supplier_transitions.Supplier_ID,supplier.Supplier_Name as 'المورد',supplier_transitions.Bank_ID,supplier_transitions.Bank_Name as 'الخزينة',supplier_transitions.Amount as 'المبلغ',supplier_transitions.Date as 'التاريخ',supplier_transitions.Payment_Method as 'طريقة الدفع',supplier_transitions.Payday as 'تاريخ الاستحقاق',supplier_transitions.Check_Number as 'رقم الشيك/الكارت',supplier_transitions.Visa_Type as 'نوع الكارت',supplier_transitions.Operation_Number as 'رقم العملية',supplier_transitions.Data as 'البيان',Paid as 'الحالة' FROM supplier_transitions inner join supplier on supplier.Supplier_ID=supplier_transitions.Supplier_ID where supplier_transitions.Transition='سداد' and (supplier_transitions.Paid='لا' or supplier_transitions.Paid='تحت التحصيل') and supplier_transitions.Error=0 and Date(supplier_transitions.Payday) >= '" + DateTime.Now.Date.ToString("yyyy-MM-dd") + "' and Date(supplier_transitions.Payday) <= '" + end.Date.ToString("yyyy-MM-dd") + "' and supplier_transitions.Supplier_ID=" + comSupplier.SelectedValue.ToString() + " order by supplier_transitions.Date";
                adapterPerm = new MySqlDataAdapter(query, dbconnection);
            }
            adapterPerm.Fill(sourceDataSet);
            gridControl1.DataSource = sourceDataSet.Tables[0];
            gridView1.Columns[0].Visible = false;
            gridView1.Columns["Bank_ID"].Visible = false;
            gridView1.Columns["Supplier_ID"].Visible = false;

            if (gridView1.IsLastVisibleRow)
            {
                gridView1.FocusedRowHandle = gridView1.RowCount - 1;
            }
        }

        public XtraTabPage getTabPage(XtraTabControl tabControl, string text)
        {
            for (int i = 0; i < tabControl.TabPages.Count; i++)
                if (tabControl.TabPages[i].Text == text)
                {
                    return tabControl.TabPages[i];
                }
            return null;
        }
    }
}

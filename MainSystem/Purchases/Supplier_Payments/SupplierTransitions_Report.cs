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
    public partial class SupplierTransitions_Report : Form
    {
        MySqlConnection dbconnection, dbconnection6;
        bool loaded = false;
        XtraTabControl tabControlContent;

        public SupplierTransitions_Report(MainForm mainform, XtraTabControl TabControlContent)
        {
            InitializeComponent();
            dbconnection = new MySqlConnection(connection.connectionString);
            dbconnection6 = new MySqlConnection(connection.connectionString);
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
            dbconnection6.Close();
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
                        loaded = false;
                        comSupplier.Text = Name;
                        comSupplier.SelectedValue = txtSupplierID.Text;
                        search(Convert.ToInt32(comSupplier.SelectedValue.ToString()));
                        loaded = true;
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
                dbconnection6.Close();
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
            dbconnection6.Close();
        }

        public void search(int supplierId)
        {
            double totalTransition = 0;
            double TotalReturns = 0;

            MySqlDataAdapter adapter = new MySqlDataAdapter("SELECT supplier_transitions.SupplierTransition_ID as 'التسلسل',supplier_transitions.Transition as 'النوع',supplier.Supplier_Name as 'المورد',supplier_transitions.Payment_Method as 'طريقة الدفع',bank.Bank_Name as 'الخزينة/البنك',supplier_transitions.Date as 'التاريخ',supplier_transitions.Amount as 'المبلغ',supplier_transitions.Data as 'البيان',supplier_transitions.Payday as 'تاريخ الاستحقاق',supplier_transitions.Check_Number as 'رقم الشيك',supplier_transitions.Operation_Number as 'رقم العملية' FROM supplier_transitions INNER JOIN supplier ON supplier.Supplier_ID = supplier_transitions.Supplier_ID INNER JOIN bank ON bank.Bank_ID = supplier_transitions.Bank_ID where supplier_transitions.Paid='تم' and supplier_transitions.Error=0 and supplier_transitions.SupplierTransition_ID=0", dbconnection);
            DataTable dtf = new DataTable();
            adapter.Fill(dtf);
            gridControl1.DataSource = dtf;

            dbconnection.Open();
            dbconnection6.Open();
            if (supplierId == 0)
            {
                string query = "SELECT supplier_transitions.SupplierTransition_ID as 'التسلسل',supplier_transitions.Transition as 'النوع',supplier.Supplier_Name as 'المورد',supplier_transitions.Payment_Method as 'طريقة الدفع',bank.Bank_Name as 'الخزينة/البنك',supplier_transitions.Date as 'التاريخ',supplier_transitions.Amount as 'المبلغ',supplier_transitions.Data as 'البيان',supplier_transitions.Payday as 'تاريخ الاستحقاق',supplier_transitions.Check_Number as 'رقم الشيك',supplier_transitions.Operation_Number as 'رقم العملية' FROM supplier_transitions INNER JOIN supplier ON supplier.Supplier_ID = supplier_transitions.Supplier_ID INNER JOIN bank ON bank.Bank_ID = supplier_transitions.Bank_ID where supplier_transitions.Paid='تم' and supplier_transitions.Error=0";
                MySqlCommand comand = new MySqlCommand(query, dbconnection);
                MySqlDataReader dr = comand.ExecuteReader();
                while (dr.Read())
                {
                    gridView1.AddNewRow();
                    int rowHandle = gridView1.GetRowHandle(gridView1.DataRowCount);
                    if (gridView1.IsNewItemRow(rowHandle))
                    {
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[0], dr["التسلسل"]);
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns["المورد"], dr["المورد"]);
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns["النوع"], dr["النوع"]);
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns["طريقة الدفع"], dr["طريقة الدفع"]);
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns["الخزينة/البنك"], dr["الخزينة/البنك"]);
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns["المبلغ"], dr["المبلغ"]);
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns["التاريخ"], dr["التاريخ"]);
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns["البيان"], dr["البيان"]);
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns["تاريخ الاستحقاق"], dr["تاريخ الاستحقاق"]);
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns["رقم الشيك"], dr["رقم الشيك"]);
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns["رقم العملية"], dr["رقم العملية"]);
                    }
                }
                dr.Close();
            }
            else
            {
                string query = "SELECT supplier_transitions.SupplierTransition_ID as 'التسلسل',supplier_transitions.Transition as 'النوع',supplier.Supplier_Name as 'المورد',supplier_transitions.Payment_Method as 'طريقة الدفع',bank.Bank_Name as 'الخزينة/البنك',supplier_transitions.Date as 'التاريخ',supplier_transitions.Amount as 'المبلغ',supplier_transitions.Data as 'البيان',supplier_transitions.Payday as 'تاريخ الاستحقاق',supplier_transitions.Check_Number as 'رقم الشيك',supplier_transitions.Operation_Number as 'رقم العملية' FROM supplier_transitions INNER JOIN supplier ON supplier.Supplier_ID = supplier_transitions.Supplier_ID INNER JOIN bank ON bank.Bank_ID = supplier_transitions.Bank_ID where supplier_transitions.Paid='تم' and supplier_transitions.Error=0 and supplier_transitions.Supplier_ID=" + supplierId;
                MySqlCommand comand = new MySqlCommand(query, dbconnection);
                MySqlDataReader dr = comand.ExecuteReader();
                while (dr.Read())
                {
                    gridView1.AddNewRow();
                    int rowHandle = gridView1.GetRowHandle(gridView1.DataRowCount);
                    if (gridView1.IsNewItemRow(rowHandle))
                    {
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[0], dr["التسلسل"]);
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns["المورد"], dr["المورد"]);
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns["النوع"], dr["النوع"]);
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns["طريقة الدفع"], dr["طريقة الدفع"]);
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns["الخزينة/البنك"], dr["الخزينة/البنك"]);
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns["المبلغ"], dr["المبلغ"]);
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns["التاريخ"], dr["التاريخ"]);
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns["البيان"], dr["البيان"]);
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns["تاريخ الاستحقاق"], dr["تاريخ الاستحقاق"]);
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns["رقم الشيك"], dr["رقم الشيك"]);
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns["رقم العملية"], dr["رقم العملية"]);
                    }
                }
                dr.Close();
            }
            //gridView1.Columns[0].Visible = false;

            if (gridView1.IsLastVisibleRow)
            {
                gridView1.FocusedRowHandle = gridView1.RowCount - 1;
            }

            for (int i = 0; i < gridView1.RowCount; i++)
            {
                if (gridView1.GetRowCellDisplayText(i, "النوع") == "سداد")
                {
                    totalTransition += Convert.ToDouble(gridView1.GetRowCellDisplayText(i, "المبلغ"));
                }
                else if (gridView1.GetRowCellDisplayText(i, "النوع") == "مرتد")
                {
                    TotalReturns += Convert.ToDouble(gridView1.GetRowCellDisplayText(i, "المبلغ"));
                }
            }
            labelTotalBills.Text = totalTransition.ToString();
            labelTotalReturns.Text = TotalReturns.ToString();
            labelSafy.Text = (totalTransition - TotalReturns).ToString();
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

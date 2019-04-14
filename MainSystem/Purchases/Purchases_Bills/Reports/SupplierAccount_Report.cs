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
    public partial class SupplierAccount_Report : Form
    {
        MySqlConnection dbconnection;
        bool loaded = false;
        DataRow row1 = null;

        public SupplierAccount_Report(MainForm mainform, XtraTabControl tabControlContent)
        {
            InitializeComponent();
            dbconnection = new MySqlConnection(connection.connectionString);
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
                    txtSupplierID.Text = comSupplier.SelectedValue.ToString();
                    search(Convert.ToInt16(comSupplier.SelectedValue.ToString()));
                }
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
                    string query = "select Supplier_Name from supplier where Supplier_ID=" + txtSupplierID.Text + "";
                    MySqlCommand com = new MySqlCommand(query, dbconnection);
                    if (com.ExecuteScalar() != null)
                    {
                        Name = (string)com.ExecuteScalar();
                        comSupplier.Text = Name;
                        comSupplier.SelectedValue = txtSupplierID.Text;
                        search(Convert.ToInt16(comSupplier.SelectedValue.ToString()));
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
                int supplierID= 0;
                if (int.TryParse(txtSupplierID.Text, out supplierID) && comSupplier.SelectedValue != null)
                {
                    search(0);
                }
                else
                {
                    MessageBox.Show("تاكد من البيانات");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }

        private void gridView1_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            if (loaded)
            {
                row1 = gridView1.GetDataRow(gridView1.GetRowHandle(e.RowHandle));
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (row1 != null)
                {
                    //PurchaseBill_Update form = new PurchaseBill_Update(this, row1);
                    //form.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void search(int supplierId)
        {
            DataSet sourceDataSet = new DataSet();
            MySqlDataAdapter adapterPerm = null;
            if (supplierId == 0)
            {
                adapterPerm = new MySqlDataAdapter("SELECT supplier_rest_money.ID,supplier.Supplier_Name as 'المورد',supplier_rest_money.Money as 'المتبقى' FROM supplier_rest_money INNER JOIN supplier ON supplier.Supplier_ID = supplier_rest_money.Supplier_ID", dbconnection);
            }
            else
            {
                adapterPerm = new MySqlDataAdapter("SELECT supplier_rest_money.ID,supplier.Supplier_Name as 'المورد',supplier_rest_money.Money as 'المتبقى' FROM supplier_rest_money INNER JOIN supplier ON supplier.Supplier_ID = supplier_rest_money.Supplier_ID where supplier_rest_money.Supplier_ID=" + comSupplier.SelectedValue.ToString(), dbconnection);
            }
            adapterPerm.Fill(sourceDataSet);
            gridControl1.DataSource = sourceDataSet.Tables[0];
            gridView1.Columns[0].Visible = false;
            
            if (gridView1.IsLastVisibleRow)
            {
                gridView1.FocusedRowHandle = gridView1.RowCount - 1;
            }
        }
    }
}

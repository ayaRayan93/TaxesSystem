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
    public partial class PurchaseBill_Report : Form
    {
        MySqlConnection dbconnection;
        bool loaded = false;
        DataRow row1 = null;

        public PurchaseBill_Report(MainForm mainform, XtraTabControl tabControlContent)
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
                    labBillNumber.Visible = true;
                    txtBillNumber.Visible = true;
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

        private void txtBillNumber_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    int supplierID, billNum = 0;
                    if (int.TryParse(txtSupplierID.Text, out supplierID) && comSupplier.SelectedValue != null && int.TryParse(txtBillNumber.Text, out billNum))
                    {
                        DataSet sourceDataSet = new DataSet();
                        MySqlDataAdapter adapterPerm = null;
                        MySqlDataAdapter adapterDetails = null;
                        //,supplier_bill_details.Purchasing_Ratio as 'نسبة الشراء'
                        adapterPerm = new MySqlDataAdapter("SELECT distinct supplier_bill.Bill_ID as 'التسلسل',supplier.Supplier_Name as 'المورد',supplier_bill.Bill_No as 'رقم الفاتورة',supplier_bill.Date as 'التاريخ',supplier_bill.Total_Price_B as 'الاجمالى قبل',supplier_bill.Total_Price_A as 'الاجمالى بعد',store.Store_Name as 'المخزن',supplier_bill.Supplier_Permission_Number as 'اذن الاستلام',supplier_bill.Import_Permission_Number as 'اذن المخزن' FROM supplier_bill INNER JOIN supplier_bill_details ON supplier_bill_details.Bill_ID = supplier_bill.Bill_ID INNER JOIN store ON store.Store_ID = supplier_bill.Store_ID INNER JOIN supplier ON supplier.Supplier_ID = supplier_bill.Supplier_ID where supplier_bill.Bill_No=" + txtBillNumber.Text + " and supplier_bill.Supplier_ID=" + comSupplier.SelectedValue.ToString(), dbconnection);
                        adapterDetails = new MySqlDataAdapter("SELECT supplier_bill.Bill_ID as 'التسلسل',data.Code as 'الكود',type.Type_Name as 'النوع',concat(product.Product_Name,' ',COALESCE(color.Color_Name,''),' ',data.Description,' ',groupo.Group_Name,' ',factory.Factory_Name,' ',COALESCE(size.Size_Value,''),' ',COALESCE(sort.Sort_Value,'')) as 'الاسم',supplier_bill_details.Price as 'السعر',supplier_bill_details.Purchasing_Discount as 'خصم الشراء',supplier_bill_details.Normal_Increase as 'الزيادة العادية',supplier_bill_details.Categorical_Increase as 'الزيادة القطعية',supplier_bill_details.Value_Additive_Tax as 'ضريبة القيمة المضافة',supplier_bill_details.Purchasing_Price as 'سعر الشراء',supplier_bill_details.Total_Meters as 'متر/قطعة' FROM supplier_bill INNER JOIN supplier_bill_details ON supplier_bill_details.Bill_ID = supplier_bill.Bill_ID INNER JOIN store ON store.Store_ID = supplier_bill.Store_ID INNER JOIN supplier ON supplier.Supplier_ID = supplier_bill.Supplier_ID INNER JOIN data ON supplier_bill_details.Data_ID = data.Data_ID INNER JOIN type ON type.Type_ID = data.Type_ID INNER JOIN product ON product.Product_ID = data.Product_ID INNER JOIN factory ON data.Factory_ID = factory.Factory_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID LEFT JOIN color ON color.Color_ID = data.Color_ID LEFT JOIN size ON size.Size_ID = data.Size_ID LEFT JOIN sort ON sort.Sort_ID = data.Sort_ID where supplier_bill.Bill_No=" + txtBillNumber.Text + " and supplier_bill.Supplier_ID=" + comSupplier.SelectedValue.ToString() + " order by SUBSTR(data.Code,1,16),color.Color_Name,data.Description,data.Sort_ID", dbconnection);
                        adapterPerm.Fill(sourceDataSet, "supplier_bill");
                        adapterDetails.Fill(sourceDataSet, "supplier_bill_details");
                        //Set up a master-detail relationship between the DataTables 
                        DataColumn keyColumn = sourceDataSet.Tables["supplier_bill"].Columns["التسلسل"];
                        DataColumn foreignKeyColumn = sourceDataSet.Tables["supplier_bill_details"].Columns["التسلسل"];
                        sourceDataSet.Relations.Add("تفاصيل الفاتورة", keyColumn, foreignKeyColumn);
                        gridControl1.DataSource = sourceDataSet.Tables["supplier_bill"];
                        gridView1.Columns[0].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
                        for (int i = 0; i < gridView1.Columns.Count; i++)
                        {
                            gridView1.Columns[i].Width = 115;
                        }
                        if (gridView1.IsLastVisibleRow)
                        {
                            gridView1.FocusedRowHandle = gridView1.RowCount - 1;
                        }
                    }
                    else
                    {
                        gridControl1.DataSource = null;
                        MessageBox.Show("تاكد من البيانات");
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
                    search();
                }
                else
                {
                    gridControl1.DataSource = null;
                    MessageBox.Show("تاكد من البيانات");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }

        void gridView1_MasterRowExpanded(object sender, CustomMasterRowEventArgs e)
        {
            GridView view = sender as GridView;
            GridView detailView = view.GetDetailView(e.RowHandle, e.RelationIndex) as GridView;
            detailView.Columns[0].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            for (int i = 0; i < detailView.Columns.Count; i++)
            {
                detailView.Columns[i].Width = 150;
            }
            detailView.Columns["الاسم"].Width = 250;
            detailView.Columns["الكود"].Width = 170;
        }

        private void gridView1_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            if (loaded)
            {
                row1 = gridView1.GetDataRow(gridView1.GetRowHandle(e.RowHandle));
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (row1 != null)
                {
                    PurchaseBill_Update form = new PurchaseBill_Update(this, row1);
                    form.ShowDialog();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void search()
        {
            DateTime date = dateTimePicker1.Value.Date;
            string d = date.ToString("yyyy-MM-dd");
            DateTime date2 = dateTimePicker2.Value.Date;
            string d2 = date2.ToString("yyyy-MM-dd");
            DataSet sourceDataSet = new DataSet();
            MySqlDataAdapter adapterPerm = null;
            MySqlDataAdapter adapterDetails = null;
            //,supplier_bill_details.Purchasing_Ratio as 'نسبة الشراء'
            adapterPerm = new MySqlDataAdapter("SELECT distinct supplier_bill.Bill_ID as 'التسلسل',supplier.Supplier_Name as 'المورد',supplier_bill.Bill_No as 'رقم الفاتورة',supplier_bill.Date as 'التاريخ',supplier_bill.Total_Price_B as 'الاجمالى قبل',supplier_bill.Total_Price_A as 'الاجمالى بعد',store.Store_Name as 'المخزن',supplier_bill.Supplier_Permission_Number as 'اذن الاستلام',supplier_bill.Import_Permission_Number as 'اذن المخزن' FROM supplier_bill INNER JOIN supplier_bill_details ON supplier_bill_details.Bill_ID = supplier_bill.Bill_ID INNER JOIN store ON store.Store_ID = supplier_bill.Store_ID INNER JOIN supplier ON supplier.Supplier_ID = supplier_bill.Supplier_ID where date(supplier_bill.Date) >='" + d + "' and date(supplier_bill.Date) <='" + d2 + "' and supplier_bill.Supplier_ID=" + comSupplier.SelectedValue.ToString(), dbconnection);
            adapterDetails = new MySqlDataAdapter("SELECT supplier_bill.Bill_ID as 'التسلسل',data.Code as 'الكود',type.Type_Name as 'النوع',concat(product.Product_Name,' ',COALESCE(color.Color_Name,''),' ',data.Description,' ',groupo.Group_Name,' ',factory.Factory_Name,' ',COALESCE(size.Size_Value,''),' ',COALESCE(sort.Sort_Value,'')) as 'الاسم',supplier_bill_details.Price as 'السعر',supplier_bill_details.Purchasing_Discount as 'خصم الشراء',supplier_bill_details.Normal_Increase as 'الزيادة العادية',supplier_bill_details.Categorical_Increase as 'الزيادة القطعية',supplier_bill_details.Value_Additive_Tax as 'ضريبة القيمة المضافة',supplier_bill_details.Purchasing_Price as 'سعر الشراء',supplier_bill_details.Total_Meters as 'متر/قطعة' FROM supplier_bill INNER JOIN supplier_bill_details ON supplier_bill_details.Bill_ID = supplier_bill.Bill_ID INNER JOIN store ON store.Store_ID = supplier_bill.Store_ID INNER JOIN supplier ON supplier.Supplier_ID = supplier_bill.Supplier_ID INNER JOIN data ON supplier_bill_details.Data_ID = data.Data_ID INNER JOIN type ON type.Type_ID = data.Type_ID INNER JOIN product ON product.Product_ID = data.Product_ID INNER JOIN factory ON data.Factory_ID = factory.Factory_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID LEFT JOIN color ON color.Color_ID = data.Color_ID LEFT JOIN size ON size.Size_ID = data.Size_ID LEFT JOIN sort ON sort.Sort_ID = data.Sort_ID where date(supplier_bill.Date) >='" + d + "' and date(supplier_bill.Date) <='" + d2 + "' and supplier_bill.Supplier_ID=" + comSupplier.SelectedValue.ToString() + " order by SUBSTR(data.Code,1,16),color.Color_Name,data.Description,data.Sort_ID", dbconnection);
            adapterPerm.Fill(sourceDataSet, "supplier_bill");
            adapterDetails.Fill(sourceDataSet, "supplier_bill_details");
            //Set up a master-detail relationship between the DataTables 
            DataColumn keyColumn = sourceDataSet.Tables["supplier_bill"].Columns["التسلسل"];
            DataColumn foreignKeyColumn = sourceDataSet.Tables["supplier_bill_details"].Columns["التسلسل"];
            sourceDataSet.Relations.Add("تفاصيل الفاتورة", keyColumn, foreignKeyColumn);
            gridControl1.DataSource = sourceDataSet.Tables["supplier_bill"];
            gridView1.Columns[0].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            for (int i = 0; i < gridView1.Columns.Count; i++)
            {
                gridView1.Columns[i].Width = 115;
            }
            if (gridView1.IsLastVisibleRow)
            {
                gridView1.FocusedRowHandle = gridView1.RowCount - 1;
            }
        }
    }
}

using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraPrinting;
using DevExpress.XtraPrinting.Preview;
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
    public partial class Supplier_Report : Form
    {
        MySqlConnection conn;
        XtraTabControl xtraTabControlPurchases;
        MainForm mainform;
        
        public static Supplier_Print supplierPrint;

        public static GridControl gridcontrol;

        public Supplier_Report(MainForm mainFrom, XtraTabControl tabControl)
        {
            InitializeComponent();
            conn = new MySqlConnection(connection.connectionString);
            xtraTabControlPurchases = tabControl;
            mainform = mainFrom;
            
            gridcontrol = gridControl1;
        }

        private void Delegate_Report_Load(object sender, EventArgs e)
        {
            try
            {
                search();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            conn.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                mainform.bindRecordSupplierForm(this);
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
                DataRowView recordList = (DataRowView)(((GridView)gridControl1.MainView).GetRow(((GridView)gridControl1.MainView).GetSelectedRows()[0]));
                if (recordList != null)
                {
                    mainform.bindUpdateSupplierForm(recordList, this);
                }
                else
                {
                    MessageBox.Show("يجب تحديد البند المراد تعديله");
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
                GridView view = gridView1 as GridView;
                delete(view);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            conn.Close();
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            try
            {
                gridcontrol = gridControl1;
                mainform.bindPrintSupplierForm();

                /*MainForm.MainTabPagePrintCustomer.Name = "tabPagePrintSupplier";
                MainForm.MainTabPagePrintCustomer.Text = "طباعة الموردين";
                
                supplierPrint = new Supplier_Print();
                supplierPrint.Size = new Size(1059, 638);
                supplierPrint.TopLevel = false;
                supplierPrint.FormBorderStyle = FormBorderStyle.None;
                supplierPrint.Dock = DockStyle.Fill;
                MainForm.MainTabPagePrintCustomer.Controls.Add(supplierPrint);
                xtraTabControlPurchases.TabPages.Add(MainForm.MainTabPagePrintCustomer);
                supplierPrint.Show();
                xtraTabControlPurchases.SelectedTabPage = MainForm.MainTabPagePrintCustomer;

                MainForm.loadedPrintCustomer = true;*/
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void gridView1_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Delete)
                {
                    //GridView view = sender as GridView;
                    GridView view = gridView1 as GridView;
                    delete(view);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            conn.Close();
        }

        private void toolTipController1_GetActiveObjectInfo(object sender, DevExpress.Utils.ToolTipControllerGetActiveObjectInfoEventArgs e)
        {
            try
            {
                if (e.SelectedControl != gridControl1) return;
                GridHitInfo gridhitinfo = gridView1.CalcHitInfo(e.ControlMousePosition);
                object o = gridhitinfo.HitTest.ToString();
                string text = gridhitinfo.HitTest.ToString();
                e.Info = new DevExpress.Utils.ToolTipControlInfo(o, text);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //functions
        public void search()
        {
            DataSet sourceDataSet = new DataSet();
            MySqlDataAdapter adapterSupplier = new MySqlDataAdapter("SELECT supplier.Supplier_ID as 'الكود',supplier.Supplier_Name as 'الاسم',supplier.Supplier_Address as 'العنوان',supplier.Supplier_Fax as 'الفاكس',supplier.Supplier_Mail as 'الايميل',supplier.Supplier_Start as 'تاريخ البداية',supplier.Supplier_Credit as 'دائن',supplier.Supplier_Debit as 'مدين',supplier.Supplier_Info as 'البيان' FROM supplier ORDER BY supplier.Supplier_ID", conn);

            MySqlDataAdapter adapterPhone = new MySqlDataAdapter("SELECT supplier.Supplier_ID as 'الكود',Phone as 'رقم التليفون' FROM supplier_phone inner join supplier on supplier.Supplier_ID=supplier_phone.Supplier_ID", conn);

            adapterSupplier.Fill(sourceDataSet, "supplier");
            adapterPhone.Fill(sourceDataSet, "supplier_phone");

            //Set up a master-detail relationship between the DataTables 
            DataColumn keyColumn = sourceDataSet.Tables["supplier"].Columns["الكود"];
            DataColumn foreignKeyColumn = sourceDataSet.Tables["supplier_phone"].Columns["الكود"];
            sourceDataSet.Relations.Add("ارقام التليفون", keyColumn, foreignKeyColumn);
            
            gridControl1.DataSource = sourceDataSet.Tables["supplier"];
            
            //gridView1.Columns[0].Visible = false;
        }

        public XtraTabPage getTabPage(string text)
        {
            for (int i = 0; i < xtraTabControlPurchases.TabPages.Count; i++)
                if (xtraTabControlPurchases.TabPages[i].Text == text)
                {
                    return xtraTabControlPurchases.TabPages[i];
                }
            return null;
        }

        void delete(GridView view)
        {
            int[] selRows = ((GridView)gridControl1.MainView).GetSelectedRows();
            if (selRows.Length > 0)
            {
                if (MessageBox.Show("هل انت متاكد انك تريد الحذف؟", "تحذير", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                    return;

                conn.Open();
                for (int i = 0; i < selRows.Length; i++)
                {
                    DataRowView selRow = (DataRowView)(((GridView)gridControl1.MainView).GetRow(selRows[i]));
                    
                    string query = "delete from supplier where Supplier_ID=" + selRow[0].ToString();
                    MySqlCommand comand = new MySqlCommand(query, conn);
                    comand.ExecuteNonQuery();

                    UserControl.ItemRecord("supplier", "حذف", Convert.ToInt32(selRow[0].ToString()), DateTime.Now, "", conn);
                }
                conn.Close();
                search();
            }
            else
            {
                MessageBox.Show("يجب ان تختار عنصر للحذف");
            }
        }
    }
}

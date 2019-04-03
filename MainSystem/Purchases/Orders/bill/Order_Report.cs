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

namespace MainSystem
{
    public partial class Order_Report : Form
    {
        MySqlConnection conn;
        XtraTabControl mainTabControl;
        MainForm mainForm;

        public static GridControl gridcontrol;

        public Order_Report(MainForm mainform, XtraTabControl tabControl)
        {
            InitializeComponent();
            conn = new MySqlConnection(connection.connectionString);
            mainTabControl = tabControl;
            mainForm = mainform;

            gridcontrol = gridControl1;
        }

        private void Order_Report_Load(object sender, EventArgs e)
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
                List<DataRow> row1 = new List<DataRow>();
                mainForm.bindRecordOrderForm(this, row1);
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
                    mainForm.bindUpdateOrderForm(recordList, this);
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
                mainForm.bindPrintOrderForm();
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
            MySqlDataAdapter adapterOrder = new MySqlDataAdapter("select orders.Order_ID as 'الكود',branch.Branch_Name as 'الفرع',orders.BranchBillNumber as 'رقم الفاتورة',orders.Employee_Name as 'الموظف المسئول',supplier.Supplier_Name as 'المورد',store.Store_Name as 'المخزن',orders.Request_Date as 'تاريخ الطلب',orders.Receive_Date as'تاريخ الاستلام' from orders inner join supplier on supplier.Supplier_ID=orders.Supplier_ID inner join branch on branch.Branch_ID=orders.Branch_ID inner join store on store.Store_ID=orders.Store_ID where orders.Branch_ID=" + UserControl.EmpBranchID, conn);
            MySqlDataAdapter adapterDetails = new MySqlDataAdapter("SELECT orders.Order_ID as 'الكود',data.Code,order_details.Quantity FROM orders INNER JOIN order_details ON orders.Order_ID = order_details.Order_ID INNER JOIN data ON order_details.Data_ID = data.Data_ID", conn);
            adapterOrder.Fill(sourceDataSet, "orders");
            adapterDetails.Fill(sourceDataSet, "order_details");

            //Set up a master-detail relationship between the DataTables 
            DataColumn keyColumn = sourceDataSet.Tables["orders"].Columns["الكود"];
            DataColumn foreignKeyColumn = sourceDataSet.Tables["order_details"].Columns["الكود"];
            sourceDataSet.Relations.Add("تفاصيل الطلب", keyColumn, foreignKeyColumn);
            gridControl1.DataSource = sourceDataSet.Tables["orders"];
            
            /*gridView1.Columns[0].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            for (int i = 1; i < gridView1.Columns.Count; i++)
            {
                gridView1.Columns[i].Width = 150;
            }*/
        }

        public XtraTabPage getTabPage(string text)
        {
            for (int i = 0; i < mainTabControl.TabPages.Count; i++)
                if (mainTabControl.TabPages[i].Name == text)
                {
                    return mainTabControl.TabPages[i];
                }
            return null;
        }

        void delete(GridView view)
        {
            /*int[] selRows = ((GridView)gridControl1.MainView).GetSelectedRows();
            if (selRows.Length > 0)
            {
                if (MessageBox.Show("هل انت متاكد انك تريد الحذف؟", "تحذير", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                    return;

                conn.Open();
                for (int i = 0; i < selRows.Length; i++)
                {
                    DataRowView selRow = (DataRowView)(((GridView)gridControl1.MainView).GetRow(selRows[i]));

                    string query = "delete from customer_phone where Customer_ID=" + selRow[0].ToString();
                    MySqlCommand comand = new MySqlCommand(query, conn);
                    comand.ExecuteNonQuery();

                    query = "delete from customer where Customer_ID=" + selRow[0].ToString();
                    comand = new MySqlCommand(query, conn);
                    comand.ExecuteNonQuery();

                    UserControl.ItemRecord("customer", "حذف", Convert.ToInt16(selRow[0].ToString()), DateTime.Now, "", conn);
                }
                conn.Close();
                search();
            }
            else
            {
                MessageBox.Show("يجب ان تختار عنصر للحذف");
            }*/
        }
    }
}

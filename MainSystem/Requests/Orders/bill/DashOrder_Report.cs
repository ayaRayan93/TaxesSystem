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
    public partial class DashOrder_Report : Form
    {
        MySqlConnection conn;
        XtraTabControl mainTabControl;
        MainForm mainForm;

        public static GridControl gridcontrol;

        public DashOrder_Report(MainForm mainform, XtraTabControl tabControl)
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
                mainForm.bindRecordDashOrderForm(this, row1/*, 0*/);
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
                    //mainForm.bindUpdateOrderForm(recordList, this);
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
            MySqlDataAdapter adapterOrder = new MySqlDataAdapter("select dash_orders.DashOrder_ID as 'التسلسل',factory.Factory_Name as 'الشركة',dash_orders.Dash_Order_Number as 'رقم الطلب',dash_orders.Employee_Name as 'الموظف المسئول',store.Store_Name as 'المخزن',supplier.Supplier_Name as 'المورد',dash_orders.Request_Date as 'تاريخ الطلب' from dash_orders inner join factory on factory.Factory_ID=dash_orders.Factory_ID left join supplier on supplier.Supplier_ID=dash_orders.Supplier_ID inner join store on store.Store_ID=dash_orders.Store_ID where dash_orders.Saved=1 and dash_orders.Confirmed=0 and dash_orders.Canceled=0", conn);
            MySqlDataAdapter adapterDetails = new MySqlDataAdapter("SELECT dash_orders.DashOrder_ID as 'التسلسل',data.Code as 'الكود',type.Type_Name as 'النوع',concat(product.Product_Name,' ',COALESCE(color.Color_Name,''),' ',data.Description,' ',groupo.Group_Name,' ',factory.Factory_Name,' ',COALESCE(size.Size_Value,''),' ',COALESCE(sort.Sort_Value,'')) as 'الاسم',dash_order_details.Quantity as 'عدد متر/قطعة' FROM dash_orders INNER JOIN dash_order_details ON dash_orders.DashOrder_ID = dash_order_details.DashOrder_ID INNER JOIN data ON dash_order_details.Data_ID = data.Data_ID LEFT JOIN color ON color.Color_ID = data.Color_ID LEFT JOIN size ON size.Size_ID = data.Size_ID LEFT JOIN sort ON sort.Sort_ID = data.Sort_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID INNER JOIN factory ON factory.Factory_ID = data.Factory_ID INNER JOIN product ON product.Product_ID = data.Product_ID INNER JOIN type ON type.Type_ID = data.Type_ID where dash_orders.Saved=1 and dash_orders.Confirmed=0 and dash_orders.Canceled=0", conn);
            adapterOrder.Fill(sourceDataSet, "dash_orders");
            adapterDetails.Fill(sourceDataSet, "dash_order_details");

            //Set up a master-detail relationship between the DataTables 
            DataColumn keyColumn = sourceDataSet.Tables["dash_orders"].Columns["التسلسل"];
            DataColumn foreignKeyColumn = sourceDataSet.Tables["dash_order_details"].Columns["التسلسل"];
            sourceDataSet.Relations.Add("تفاصيل الطلب", keyColumn, foreignKeyColumn);
            gridControl1.DataSource = sourceDataSet.Tables["dash_orders"];
            
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
            int[] selRows = ((GridView)gridControl1.MainView).GetSelectedRows();
            if (selRows.Length > 0)
            {
                if (MessageBox.Show("هل انت متاكد انك تريد الالغاء؟", "", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                    return;

                conn.Open();
                for (int i = 0; i < selRows.Length; i++)
                {
                    DataRowView selRow = (DataRowView)(((GridView)gridControl1.MainView).GetRow(selRows[i]));

                    string query = "update dash_orders set Canceled=1 where DashOrder_ID=" + selRow[0].ToString();
                    MySqlCommand comand = new MySqlCommand(query, conn);
                    comand.ExecuteNonQuery();
                    
                    UserControl.ItemRecord("dash_orders", "حذف", Convert.ToInt32(selRow[0].ToString()), DateTime.Now, "", conn);
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

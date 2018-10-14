using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
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
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MainSystem
{
    public partial class SpecialOrders_Report2 : Form
    {
        MySqlConnection conn;
        XtraTabControl MainTabControlPS;

        //Panel panelAddCustomer;
        //Panel panelUpdateCustomer;
        //Panel panelPrintCustomer;

        public static Customer_Print customerPrint;

        public static GridControl gridcontrol;
        int EmpBranchId = 0;
        
        public static RequestImage tipImage = null;
        DataRow row1;

        public SpecialOrders_Report2()
        {
            InitializeComponent();
            conn = new MySqlConnection(connection.connectionString);
            MainTabControlPS = MainForm.tabControlSales;

            //panelAddCustomer = new Panel();
            //panelUpdateCustomer = new Panel();
            //panelPrintCustomer = new Panel();

            gridcontrol = gridControl1;

            EmpBranchId = UserControl.UserBranch(conn);
            
            //gridView1.PostEditor();
            //gridView1.UpdateCurrentRow();
            gridView1.OptionsBehavior.EditingMode = DevExpress.XtraGrid.Views.Grid.GridEditingMode.EditFormInplace;
            gridView1.OptionsEditForm.CustomEditFormLayout = new AdvancedEditForm(EmpBranchId);
            
            //gridView1.ShowInplaceEditForm();
            //gridView1.OptionsBehavior.EditingMode = DevExpress.XtraGrid.Views.Grid.GridEditingMode.Default;
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
        
        //functions
        public void search()
        {
            /*DataTable sourceData = new DataTable();
            MySqlDataAdapter adapterCustomer = new MySqlDataAdapter("SELECT requests.BranchBillNumber as 'رقم الطلب',special_order.Picture as 'صورة الطلب' FROM special_order INNER JOIN dash ON special_order.Dash_ID = dash.Dash_ID INNER JOIN requests ON special_order.SpecialOrder_ID = requests.SpecialOrder_ID where special_order.Record=0 AND dash.Branch_ID=" + EmpBranchId, conn);
            adapterCustomer.Fill(sourceData);
            gridControl1.DataSource = sourceData;*/
            conn.Open();
            MySqlCommand adapter = new MySqlCommand("SELECT special_order.SpecialOrder_ID,requests.BranchBillNumber,special_order.Picture FROM special_order INNER JOIN dash ON special_order.Dash_ID = dash.Dash_ID INNER JOIN requests ON special_order.SpecialOrder_ID = requests.SpecialOrder_ID where special_order.Record=0 AND dash.Branch_ID=" + EmpBranchId, conn);
            MySqlDataReader dr = adapter.ExecuteReader();

            BindingList<GridPicture> lista = new BindingList<GridPicture>();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    byte[] img = (byte[])dr["Picture"];
                    lista.Add(new GridPicture() { SpecialOrderID= Convert.ToInt16(dr["SpecialOrder_ID"].ToString()), RequestNumber = Convert.ToInt16(dr["BranchBillNumber"].ToString()), Picture = img });
                }
                dr.Close();
            }
            gridControl1.DataSource = lista;
        }

        public XtraTabPage getTabPage(string text)
        {
            for (int i = 0; i < MainTabControlPS.TabPages.Count; i++)
                if (MainTabControlPS.TabPages[i].Name == text)
                {
                    return MainTabControlPS.TabPages[i];
                }
            return null;
        }

        private void gridView1_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            try
            {
                if (e.Column.ToString() == "صورة الطلب")
                {
                    //row1 = gridView1.GetDataRow(gridView1.GetRowHandle(e.RowHandle));
                    //MessageBox.Show(gridView1.GetFocusedRowCellValue(gridView1.Columns[0]).ToString());
                    if (tipImage == null)
                    {
                        tipImage = new RequestImage(gridView1.GetFocusedRowCellValue(gridView1.Columns[0]).ToString());
                        tipImage.Location = new Point(Cursor.Position.X, Cursor.Position.Y);
                        tipImage.Show();
                    }
                    else
                    {
                        tipImage.Close();
                        tipImage = new RequestImage(gridView1.GetFocusedRowCellValue(gridView1.Columns[0]).ToString());
                        tipImage.Location = new Point(Cursor.Position.X, Cursor.Position.Y);
                        tipImage.Show();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /*private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                row1 = gridView1.GetDataRow(gridView1.FocusedRowHandle);

                if (tipImage == null)
                {
                    tipImage = new RequestImage(row1["SpecialOrderID"].ToString());
                    tipImage.Location = new Point(Cursor.Position.X, Cursor.Position.Y);
                    tipImage.Show();
                }
                else
                {
                    tipImage.Close();
                    tipImage = new RequestImage(row1["SpecialOrderID"].ToString());
                    tipImage.Location = new Point(Cursor.Position.X, Cursor.Position.Y);
                    tipImage.Show();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }*/
    }

    public class GridPicture
    {
        public int SpecialOrderID { get; set; }
        public int RequestNumber { get; set; }
        public byte[] Picture { get; set; }
    }
}

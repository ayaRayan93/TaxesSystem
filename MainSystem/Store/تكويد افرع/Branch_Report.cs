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
    public partial class Branch_Report : Form
    {
        MySqlConnection conn;
        MainForm branchMainForm = null;
        XtraTabControl tabControlStoresContent;

        XtraTabPage MainTabPagePrintingBranch;
        Panel panelPrintingBranch;

        public static Branch_Print branchPrint;

        public static GridControl gridcontrol;

        public Branch_Report(MainForm BranchMainForm, XtraTabControl TabControlStoresContent)
        {
            InitializeComponent();
            conn = new MySqlConnection(connection.connectionString);
            branchMainForm = BranchMainForm;
            tabControlStoresContent = TabControlStoresContent;

            MainTabPagePrintingBranch = new XtraTabPage();
            panelPrintingBranch = new Panel();

            gridcontrol = gridControl1;
        }

        private void BranchReport_Load(object sender, EventArgs e)
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
                branchMainForm.bindRecordBranchForm(this);
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
                if (((GridView)gridView1).GetSelectedRows().Count() > 0)
                {
                    DataRowView sellRow = (DataRowView)(((GridView)gridView1).GetRow(((GridView)gridView1).GetSelectedRows()[0]));
                    branchMainForm.bindUpdateBranchForm(sellRow, this);
                }
                else
                {
                    MessageBox.Show("يجب تحديد العنصر المراد تعديله");
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
                MainTabPagePrintingBranch.Text = "طباعة الافرع";
                tabControlStoresContent.TabPages.Add(MainTabPagePrintingBranch);

                tabControlStoresContent.SelectedTabPage = MainTabPagePrintingBranch;

                panelPrintingBranch.Controls.Clear();
                branchPrint = new Branch_Print();
                //branchPrint.mGridControl = gridControl1;
                branchPrint.Size = new Size(1059, 638);
                branchPrint.TopLevel = false;
                branchPrint.FormBorderStyle = FormBorderStyle.None;
                branchPrint.Dock = DockStyle.Fill;
                panelPrintingBranch.Controls.Add(branchPrint);
                branchPrint.Show();

                /*Main.loaded = true;*/
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
            MySqlDataAdapter adapter = new MySqlDataAdapter("select Branch_ID as 'الرقم المسلسل',Branch_Name as 'الفرع',Branch_Address as 'العنوان',Branch_Phone as 'رقم التليفون',Branch_Mail as 'الايميل',Branch_Fax as 'الفاكس',Postal_Code as 'الرمز البريدى' from branch", conn);

            DataSet sourceDataSet = new DataSet();
            adapter.Fill(sourceDataSet);

            gridControl1.DataSource = sourceDataSet.Tables[0];
            gridView1.Columns[0].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            //gridView1.FormatRules.
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

                    string query = "delete from branch where Branch_ID=" + selRow[0].ToString();
                    MySqlCommand comand = new MySqlCommand(query, conn);
                    comand.ExecuteNonQuery();
                }
                conn.Close();

                view.DeleteSelectedRows();
            }
            else
            {
                MessageBox.Show("يجب ان تختار عنصر للحذف");
            }
        }
    }
}

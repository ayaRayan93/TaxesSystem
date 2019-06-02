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
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MainSystem
{
    public partial class SpecialOrders_Report2 : Form
    {
        MySqlConnection conn;
        XtraTabControl MainTabControlPS;
        MainForm mainForm = null;
        
        public static Customer_Print customerPrint;

        public static GridControl gridcontrol;
        
        public static RequestImage tipImage = null;

        public SpecialOrders_Report2(MainForm mainform)
        {
            InitializeComponent();
            conn = new MySqlConnection(connection.connectionString);
            MainTabControlPS = MainForm.tabControlSales;
            
            gridcontrol = gridControl1;
            mainForm = mainform;
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

        private void gridView1_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            /*try
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
            }*/
        }

        private void repositoryItemButtonEditDownload_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            try
            {
                if (gridView1.GetFocusedRowCellValue(gridView1.Columns["Picture"]) != null)
                {
                    //string filePath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                    byte[] bytes = (byte[])gridView1.GetFocusedRowCellValue(gridView1.Columns["Picture"]);
                    Image image = byteArrayToImage(bytes);
                    SaveFileDialog f = new SaveFileDialog();
                    f.Filter = "PNG(*.PNG)|*.png";
                    if (f.ShowDialog() == DialogResult.OK)
                    {
                        image.Save(f.FileName);
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void repositoryItemButtonEditDownloadProduct_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            try
            {
                if (gridView1.GetFocusedRowCellValue(gridView1.Columns["ProductPicture"]) != null)
                {
                    byte[] bytes = (byte[])gridView1.GetFocusedRowCellValue(gridView1.Columns["ProductPicture"]);
                    Image image = byteArrayToImage(bytes);
                    SaveFileDialog f = new SaveFileDialog();
                    f.Filter = "PNG(*.PNG)|*.png";
                    if (f.ShowDialog() == DialogResult.OK)
                    {
                        image.Save(f.FileName);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void repositoryItemButtonEditAddOrder_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            try
            {
                List<DataRow> row1 = new List<DataRow>();
                //for (int i = 0; i < gridView1.SelectedRowsCount; i++)
                //{
                //    row1.Add(gridView1.GetDataRow(gridView1.GetSelectedRows()[i]));
                //}
                mainForm.bindRecordDashOrderForm(null, row1/*, Convert.ToInt16(gridView1.GetFocusedRowCellValue(gridView1.Columns[0]))*/);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            conn.Close();
        }

        private void gridView1_KeyDown(object sender, KeyEventArgs e)
        {
            /*if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    AdvancedEditForm2 f2 = new AdvancedEditForm2(EmpBranchId, Convert.ToInt16(gridView1.GetFocusedRowCellValue(gridView1.Columns[1])));
                    f2.ShowDialog();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }*/
        }
        
        //functions
        public void search()
        {
            conn.Open();
            MySqlCommand adapter = new MySqlCommand("SELECT special_order.SpecialOrder_ID,special_order.Picture,special_order.Product_Picture,special_order.Description FROM special_order INNER JOIN dash ON special_order.Dash_ID = dash.Dash_ID where special_order.Record=0 and special_order.Confirmed=1 and special_order.Canceled=0" /*AND dash.Branch_ID=" + EmpBranchId*/, conn);
            MySqlDataReader dr = adapter.ExecuteReader();

            BindingList<GridPicture> lista = new BindingList<GridPicture>();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    byte[] img = null;
                    if (dr["Picture"].ToString() != "")
                    {
                        img = (byte[])dr["Picture"];
                    }
                    byte[] imgProduct = null;
                    if (dr["Product_Picture"].ToString() != "")
                    {
                        imgProduct = (byte[])dr["Product_Picture"];
                    }
                    lista.Add(new GridPicture() { SpecialOrderID = Convert.ToInt16(dr["SpecialOrder_ID"].ToString()), Picture = img, ProductPicture = imgProduct, RequestDescription = dr["Description"].ToString() });
                }
                dr.Close();
            }
            gridControl1.DataSource = lista;
        }

        public Image byteArrayToImage(byte[] byteArrayIn)
        {
            MemoryStream ms = new MemoryStream(byteArrayIn);
            Image returnImage = Image.FromStream(ms);
            return returnImage;
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

        private void btnRecordSpecialOrder_Click(object sender, EventArgs e)
        {
            try
            {
                if (gridView1.SelectedRowsCount > 0)
                {
                    conn.Open();
                    for (int i = 0; i < gridView1.SelectedRowsCount; i++)
                    {
                        int rowNum = gridView1.GetRowHandle(gridView1.GetSelectedRows()[i]);
                        string query = "update special_order set Record=1 where SpecialOrder_ID=" + gridView1.GetRowCellDisplayText(rowNum, gridView1.Columns[0]);
                        MySqlCommand com = new MySqlCommand(query, conn);
                        com.ExecuteNonQuery();
                    }
                    conn.Close();

                    search();
                    mainForm.ConfirmedSpecialOrdersFunction();
                }
                else
                {
                    MessageBox.Show("يجب اختيار طلب واحد على الاقل");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            conn.Close();
        }

        private void btnCanceled_Click(object sender, EventArgs e)
        {
            try
            {
                if (gridView1.SelectedRowsCount > 0)
                {
                    if (MessageBox.Show("هل انت متاكد انك تريد الالغاء؟", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    {
                        return;
                    }
                    conn.Open();
                    for (int i = 0; i < gridView1.SelectedRowsCount; i++)
                    {
                        int rowNum = gridView1.GetRowHandle(gridView1.GetSelectedRows()[i]);
                        string query = "update special_order set Canceled=1 where SpecialOrder_ID=" + gridView1.GetRowCellDisplayText(rowNum, gridView1.Columns[0]);
                        MySqlCommand com = new MySqlCommand(query, conn);
                        com.ExecuteNonQuery();
                    }
                    conn.Close();

                    search();
                    mainForm.ConfirmedSpecialOrdersFunction();
                }
                else
                {
                    MessageBox.Show("يجب اختيار طلب واحد على الاقل");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            conn.Close();
        }
    }

    public class GridPicture
    {
        public int SpecialOrderID { get; set; }
        public byte[] Picture { get; set; }
        public byte[] ProductPicture { get; set; }
        public string RequestDescription { get; set; }
    }
}

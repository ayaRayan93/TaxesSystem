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

        //Panel panelAddCustomer;
        //Panel panelUpdateCustomer;
        //Panel panelPrintCustomer;

        public static Customer_Print customerPrint;

        public static GridControl gridcontrol;
        int EmpBranchId = 0;
        
        public static RequestImage tipImage = null;

        public SpecialOrders_Report2()
        {
            InitializeComponent();
            conn = new MySqlConnection(connection.connectionString);
            MainTabControlPS = MainForm.tabControlSales;

            //panelAddCustomer = new Panel();
            //panelUpdateCustomer = new Panel();
            //panelPrintCustomer = new Panel();

            gridcontrol = gridControl1;

            EmpBranchId = UserControl.EmpBranchID;
            
            //gridView1.PostEditor();
            //gridView1.UpdateCurrentRow();
            /*gridView1.OptionsBehavior.EditingMode = DevExpress.XtraGrid.Views.Grid.GridEditingMode.EditFormInplace;
            gridView1.OptionsEditForm.CustomEditFormLayout = new AdvancedEditForm(EmpBranchId);*/
            
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
                //string filePath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                byte[] bytes = (byte[])gridView1.GetFocusedRowCellValue(gridView1.Columns[2]);
                Image image= byteArrayToImage(bytes);
                SaveFileDialog f = new SaveFileDialog();
                f.Filter = "PNG(*.PNG)|*.png";
                if (f.ShowDialog() == DialogResult.OK)
                {
                    image.Save(f.FileName);
                }
                //System.IO.File.WriteAllBytes(filePath, bytes);
                //File.WriteAllBytes(Server.MapPath(filePath), bytes);
                //MemoryStream imageData = new MemoryStream(bytes);


                /*using (WebClient webClient = new WebClient())
                {
                    var watch = System.Diagnostics.Stopwatch.StartNew();

                    byte[] data = (byte[])gridView1.GetFocusedRowCellValue(gridView1.Columns[2]);
                    using (MemoryStream imageData = new MemoryStream(data))
                    {
                    }

                    watch.Stop();
                }*/
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
                byte[] bytes = (byte[])gridView1.GetFocusedRowCellValue(gridView1.Columns[4]);
                Image image = byteArrayToImage(bytes);
                SaveFileDialog f = new SaveFileDialog();
                f.Filter = "PNG(*.PNG)|*.png";
                if (f.ShowDialog() == DialogResult.OK)
                {
                    image.Save(f.FileName);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void gridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
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
            }
        }
        
        //functions
        public void search()
        {
            /*DataTable sourceData = new DataTable();
            MySqlDataAdapter adapterCustomer = new MySqlDataAdapter("SELECT requests.BranchBillNumber as 'رقم الطلب',special_order.Picture as 'صورة الطلب' FROM special_order INNER JOIN dash ON special_order.Dash_ID = dash.Dash_ID INNER JOIN requests ON special_order.SpecialOrder_ID = requests.SpecialOrder_ID where special_order.Record=0 AND dash.Branch_ID=" + EmpBranchId, conn);
            adapterCustomer.Fill(sourceData);
            gridControl1.DataSource = sourceData;*/
            conn.Open();
            MySqlCommand adapter = new MySqlCommand("SELECT special_order.SpecialOrder_ID,requests.BranchBillNumber,special_order.Picture,special_order.Product_Picture,special_order.Description FROM special_order INNER JOIN dash ON special_order.Dash_ID = dash.Dash_ID INNER JOIN requests ON special_order.SpecialOrder_ID = requests.SpecialOrder_ID where special_order.Record=0 AND dash.Branch_ID=" + EmpBranchId, conn);
            MySqlDataReader dr = adapter.ExecuteReader();

            BindingList<GridPicture> lista = new BindingList<GridPicture>();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    byte[] img = (byte[])dr["Picture"];
                    byte[] imgProduct = null;
                    if (dr["Product_Picture"].ToString() != "")
                    {
                        imgProduct = (byte[])dr["Product_Picture"];
                    }
                    lista.Add(new GridPicture() { SpecialOrderID = Convert.ToInt16(dr["SpecialOrder_ID"].ToString()), RequestNumber = Convert.ToInt16(dr["BranchBillNumber"].ToString()), Picture = img, ProductPicture = imgProduct, RequestDescription = dr["Description"].ToString() });
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
    }

    public class GridPicture
    {
        public int SpecialOrderID { get; set; }
        public int RequestNumber { get; set; }
        public byte[] Picture { get; set; }
        public byte[] ProductPicture { get; set; }
        public string RequestDescription { get; set; }
    }
}

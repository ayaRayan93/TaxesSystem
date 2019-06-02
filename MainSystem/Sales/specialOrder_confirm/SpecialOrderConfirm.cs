using DevExpress.XtraEditors.Repository;
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
    public partial class SpecialOrderConfirm : Form
    {
        MySqlConnection dbconnection;
        MainForm mainForm = null;
        XtraTabControl xtraTabControlSales;

        public SpecialOrderConfirm(MainForm mainform, XtraTabControl XtraTabControlSales)
        {
            InitializeComponent();
            dbconnection = new MySqlConnection(connection.connectionString);
            mainForm = mainform;
            xtraTabControlSales = XtraTabControlSales;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                search();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }

        private void repositoryItemButtonEditSOUpdate_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            try
            {
                AddSpecialOrderScanner form = new AddSpecialOrderScanner(this, Convert.ToInt16(gridView1.GetFocusedRowCellValue(gridView1.Columns[0])));
                form.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }

        private void btnConfirmSpecialOrder_Click(object sender, EventArgs e)
        {
            try
            {
                if (gridView1.SelectedRowsCount > 0)
                {
                    dbconnection.Open();
                    for (int i = 0; i < gridView1.SelectedRowsCount; i++)
                    {
                        //MessageBox.Show(gridView1.GetRowCellDisplayText(i, gridView1.Columns[0]));
                        int rowNum = gridView1.GetRowHandle(gridView1.GetSelectedRows()[i]);
                        string query = "update special_order set Confirmed=1 where SpecialOrder_ID=" + gridView1.GetRowCellDisplayText(rowNum, gridView1.Columns[0]);
                        MySqlCommand com = new MySqlCommand(query, dbconnection);
                        com.ExecuteNonQuery();
                    }
                    dbconnection.Close();

                    search();
                    mainForm.SpecialOrdersFunction();
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
            dbconnection.Close();
        }

        private void gridView1_KeyDown(object sender, KeyEventArgs e)
        {
            /*if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    AdvancedEditForm2 f2 = new AdvancedEditForm2(UserControl.EmpBranchID, Convert.ToInt16(gridView1.GetFocusedRowCellValue(gridView1.Columns[1])));
                    f2.ShowDialog();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }*/
        }

        public void search()
        {
            dbconnection.Open();
            
            MySqlCommand adapter = new MySqlCommand("SELECT special_order.SpecialOrder_ID,special_order.Description,special_order.Picture,special_order.Product_Picture,delegate.Delegate_Name FROM special_order INNER JOIN delegate ON special_order.Delegate_ID = delegate.Delegate_ID where special_order.Record=0 and special_order.Confirmed=0 and special_order.Canceled=0", dbconnection);
            MySqlDataReader dr = adapter.ExecuteReader();

            BindingList<SpecialOrderPicture> lista = new BindingList<SpecialOrderPicture>();
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
                    lista.Add(new SpecialOrderPicture() { SpecialOrderId = Convert.ToInt16(dr["SpecialOrder_ID"].ToString()), Picture = img, ProductPicture = imgProduct, SODescription = dr["Description"].ToString(), DelegateName = dr["Delegate_Name"].ToString() });
                }
                dr.Close();
            }
            gridControl1.DataSource = lista;
        }
    }

    public class SpecialOrderPicture
    {
        public int SpecialOrderId { get; set; }
        public byte[] Picture { get; set; }
        public byte[] ProductPicture { get; set; }
        public string SODescription { get; set; }
        public string DelegateName { get; set; }
    }
}

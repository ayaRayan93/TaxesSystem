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
    public partial class Transportation_Report : Form
    {
        MySqlConnection conn;
        MySqlConnection connectionReader1, connectionReader2;
        MainForm bankMainForm = null;
        XtraTabControl MainTabControlBank;
        DataRow row1 = null;
        
        public static XtraTabPage MainTabPagePrintingTransitions;
        Panel panelPrintingTransitions;

        public static BillsTransitions_Print bankPrint;

        public static GridControl gridcontrol;
        bool loadedBranch = false;

        public Transportation_Report(MainForm BankMainForm)
        {
            InitializeComponent();
            conn = new MySqlConnection(connection.connectionString);
            connectionReader1 = new MySqlConnection(connection.connectionString);
            connectionReader2 = new MySqlConnection(connection.connectionString);
            bankMainForm = BankMainForm;
            MainTabControlBank = MainForm.tabControlBank;
            
            MainTabPagePrintingTransitions = new XtraTabPage();
            panelPrintingTransitions = new Panel();
            
            gridcontrol = gridControl1;

            /*comBranch.AutoCompleteMode = AutoCompleteMode.Suggest;
            comBranch.AutoCompleteSource = AutoCompleteSource.ListItems;*/
        }

        private void Bills_Transitions_Report_Load(object sender, EventArgs e)
        {
            try
            {
                if (!loadedBranch)
                {
                    loadBranch();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            conn.Close();
        }
        
        private void txtBillNum_KeyDown(object sender, KeyEventArgs e)
        {
            /*if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    if (comBranch.Text != "" && txtBranchID.Text != "")
                    {
                        search();
                    }
                    else
                    {
                        MessageBox.Show("يجب اختيار فرع");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                conn.Close();
                myConnection.Close();
                connectionReader1.Close();
            }*/
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                search();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            conn.Close();
            connectionReader1.Close();
        }

        private void btnNewChosen_Click(object sender, EventArgs e)
        {
            try
            {
                /*clearCom();*/
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            if (row1 != null)
            {
                try
                {
                    List<Transportation_Items> bi = new List<Transportation_Items>();
                    conn.Open();
                    string query = "SELECT transfer_product.TransferProduct_ID as 'رقم التحويل',storeFrom.Store_Name as 'من مخزن',storeTo.Store_Name as 'الى مخزن',transfer_product.Date as 'تاريخ التحويل' FROM transfer_product left JOIN store as storeTo ON storeTo.Store_ID = transfer_product.To_Store left join store as storeFrom on storeFrom.Store_ID = transfer_product.From_Store where transfer_product.TransferProduct_ID=" + row1["رقم التحويل"].ToString();
                    MySqlCommand com = new MySqlCommand(query, conn);
                    MySqlDataReader dr = com.ExecuteReader();
                    while (dr.Read())
                    {
                        Transportation_Items item;
                        connectionReader2.Open();

                        string q = "SELECT data.Code as 'الكود',type.Type_Name as 'النوع',concat(product.Product_Name,' ',COALESCE(color.Color_Name,''),' ',data.Description,' ',groupo.Group_Name,' ',factory.Factory_Name,' ',COALESCE(size.Size_Value,''),' ',COALESCE(sort.Sort_Value,'')) as 'الاسم',transfer_product_details.Quantity as 'الكمية' FROM transfer_product_details INNER JOIN transfer_product ON transfer_product_details.TransferProduct_ID = transfer_product.TransferProduct_ID left JOIN store as storeTo ON storeTo.Store_ID = transfer_product.To_Store left join store as storeFrom on storeFrom.Store_ID = transfer_product.From_Store INNER JOIN data ON transfer_product_details.Data_ID = data.Data_ID LEFT JOIN color ON color.Color_ID = data.Color_ID LEFT JOIN size ON size.Size_ID = data.Size_ID LEFT JOIN sort ON sort.Sort_ID = data.Sort_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID INNER JOIN factory ON factory.Factory_ID = data.Factory_ID  INNER JOIN product ON product.Product_ID = data.Product_ID  INNER JOIN type ON type.Type_ID = data.Type_ID where transfer_product.TransferProduct_ID=" + dr["رقم التحويل"].ToString() + " order by SUBSTR(data.Code,1,16),color.Color_Name,data.Description,data.Sort_ID";
                        MySqlCommand c = new MySqlCommand(q, connectionReader2);
                        MySqlDataReader dr1 = c.ExecuteReader();
                        while (dr1.Read())
                        {
                            item = new Transportation_Items() { Code = dr1["الكود"].ToString(), Product_Type = dr1["النوع"].ToString(), Product_Name = dr1["الاسم"].ToString(), Total_Meters = Convert.ToDouble(dr1["الكمية"].ToString()) };
                            bi.Add(item);
                        }
                        dr1.Close();
                    }
                    Report_Transportation_Copy f = new Report_Transportation_Copy();
                    f.PrintInvoice(Convert.ToInt16(dr["رقم التحويل"].ToString()), dr["من مخزن"].ToString(), dr["الى مخزن"].ToString(), dr["تاريخ التحويل"].ToString(), bi);
                    dr.Close();
                    f.ShowDialog();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                conn.Close();
                connectionReader2.Close();
            }
            else
            {
                MessageBox.Show("برجاء التاكد من اختيار عنصر");
            }
        }

        //functions
        private void loadBranch()
        {
            conn.Open();
            string query = "select * from branch";
            MySqlDataAdapter da = new MySqlDataAdapter(query, conn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            /*comBranch.DataSource = dt;
            comBranch.DisplayMember = dt.Columns["Branch_Name"].ToString();
            comBranch.ValueMember = dt.Columns["Branch_ID"].ToString();
            comBranch.SelectedIndex = -1;*/

            loadedBranch = true;
        }

        public void search()
        {
            DataSet sourceDataSet = new DataSet();
            MySqlDataAdapter adapterSupplier = new MySqlDataAdapter("SELECT transfer_product.TransferProduct_ID as 'رقم التحويل',storeFrom.Store_Name as 'من مخزن',storeTo.Store_Name as 'الى مخزن',transfer_product.Date as 'تاريخ التحويل' FROM transfer_product left JOIN store as storeTo ON storeTo.Store_ID = transfer_product.To_Store left join store as storeFrom on storeFrom.Store_ID = transfer_product.From_Store", conn);
            MySqlDataAdapter adapterPhone = new MySqlDataAdapter("SELECT transfer_product.TransferProduct_ID as 'رقم التحويل',data.Code as 'الكود',type.Type_Name as 'النوع',concat(product.Product_Name,' ',COALESCE(color.Color_Name,''),' ',data.Description,' ',groupo.Group_Name,' ',factory.Factory_Name,' ',COALESCE(size.Size_Value,''),' ',COALESCE(sort.Sort_Value,'')) as 'الاسم',transfer_product_details.Quantity as 'الكمية' FROM transfer_product_details INNER JOIN transfer_product ON transfer_product_details.TransferProduct_ID = transfer_product.TransferProduct_ID left JOIN store as storeTo ON storeTo.Store_ID = transfer_product.To_Store left join store as storeFrom on storeFrom.Store_ID = transfer_product.From_Store INNER JOIN data ON transfer_product_details.Data_ID = data.Data_ID LEFT JOIN color ON color.Color_ID = data.Color_ID LEFT JOIN size ON size.Size_ID = data.Size_ID LEFT JOIN sort ON sort.Sort_ID = data.Sort_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID INNER JOIN factory ON factory.Factory_ID = data.Factory_ID  INNER JOIN product ON product.Product_ID = data.Product_ID  INNER JOIN type ON type.Type_ID = data.Type_ID order by SUBSTR(data.Code,1,16),color.Color_Name,data.Description,data.Sort_ID", conn);

            adapterSupplier.Fill(sourceDataSet, "transfer_product");
            adapterPhone.Fill(sourceDataSet, "transfer_product_details");

            //Set up a master-detail relationship between the DataTables 
            DataColumn keyColumn = sourceDataSet.Tables["transfer_product"].Columns["رقم التحويل"];
            DataColumn foreignKeyColumn = sourceDataSet.Tables["transfer_product_details"].Columns["رقم التحويل"];
            sourceDataSet.Relations.Add("بنود التحويل", keyColumn, foreignKeyColumn);

            gridControl1.DataSource = sourceDataSet.Tables["transfer_product"];
        }

        private void gridView1_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            try
            {
                row1 = gridView1.GetDataRow(gridView1.GetRowHandle(e.RowHandle));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void clearCom()
        {
            foreach (Control co in this.tableLayoutPanel3.Controls)
            {
                if (co is System.Windows.Forms.ComboBox)
                {
                    co.Text = "";
                }
                else if (co is TextBox)
                {
                    co.Text = "";
                }
            }
            /*foreach (Control co in this.tableLayoutPanel4.Controls)
            {
                if (co is System.Windows.Forms.ComboBox)
                {
                    co.Text = "";
                }
                else if (co is TextBox)
                {
                    co.Text = "";
                }
                else if (co is DateTimePicker)
                {
                    dateTimePicker1.Value = DateTime.Now;
                }
            }
            txtTotal.Text = "";
            txtDiscount.Text = "";
            txtFinal.Text = "";*/
            gridControl1.DataSource = null;
        }
    }
}

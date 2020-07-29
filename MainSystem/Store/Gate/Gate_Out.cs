using DevExpress.Utils.Drawing;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraTab;
using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TaxesSystem
{
    public partial class Gate_Out : Form
    {
        MySqlConnection conn;
        MainForm mainform = null;
        XtraTabControl xtraTabControlStoresContent = null;

        public Gate_Out(MainForm Mainform, XtraTabControl TabControlStoresContent)
        {
            try
            {
                InitializeComponent();
                mainform = Mainform;
                conn = new MySqlConnection(connection.connectionString);
                xtraTabControlStoresContent = TabControlStoresContent;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Gate_Out_Load(object sender, EventArgs e)
        {
            try
            {
                conn.Open();

                search();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            conn.Close();
        }

        private void gridView1_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            if (e.Column == gridView1.Columns["التسلسل"])
            {
                Permissions_Edit form = new Permissions_Edit(this, Convert.ToInt32(gridView1.GetRowCellDisplayText(e.RowHandle, gridView1.Columns["التسلسل"])));
                form.ShowDialog();
            }
        }

        private void gridView1_SelectionChanged(object sender, DevExpress.Data.SelectionChangedEventArgs e)
        {
            try
            {
                if (e.Action == CollectionChangeAction.Add)
                {
                    if (MessageBox.Show("هل انت متاكد انك تريد تسجيل الخروج؟", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                    {
                        GridView view = gridView1 as GridView;
                        view.SelectionChanged -= gridView1_SelectionChanged;
                        gridView1.UnselectRow(gridView1.FocusedRowHandle);
                        view.SelectionChanged += gridView1_SelectionChanged;
                        return;
                    }
                    conn.Open();
                    string query = "update gate set Date_Out='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' ,OutEmployee_ID=" + UserControl.EmpID + " where gate.Gate_ID=" + gridView1.GetRowCellDisplayText(e.ControllerRow, gridView1.Columns["التسلسل"]);
                    MySqlCommand com = new MySqlCommand(query, conn);
                    com.ExecuteNonQuery();
                    search();
                }
                else if (e.Action == CollectionChangeAction.Remove)
                {
                    GridView view = gridView1 as GridView;
                    view.SelectionChanged -= gridView1_SelectionChanged;
                    gridView1.SelectRow(gridView1.FocusedRowHandle);
                    view.SelectionChanged += gridView1_SelectionChanged;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            conn.Close();
        }
        
        //function
        public void search()
        {
            //string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"Store.txt");
            //int storeId = Convert.ToInt32(System.IO.File.ReadAllText(path));

            string supString = BaseData.StoreID;
            int storeId = Convert.ToInt32(supString);

            DataSet sourceDataSet = new DataSet();
            //,employee.Employee_Name as 'مسئول التعتيق'
            MySqlDataAdapter adaptergate = new MySqlDataAdapter("SELECT gate.Gate_ID as 'التسلسل',gate.Reason as 'سبب الدخول',gate.Type as 'النوع',gate.Responsible as 'المسئول',gate.Date_Enter as 'وقت الدخول',gate.Driver_Name as 'السواق',gate.Car_Number as 'رقم العربية',gate.License_Number as 'رقم الرخصة',employee.Employee_Name as 'مسئول التعتيق',gate.Description as 'البيان' FROM gate LEFT JOIN employee ON employee.Employee_ID = gate.TatiqEmp_ID WHERE gate.Store_ID =" + storeId + " and gate.Date_Out is NULL", conn);
            MySqlDataAdapter adaptersupp = new MySqlDataAdapter("SELECT gate_supplier.Gate_ID as 'التسلسل',gate_supplier.GateSupplier_ID as 'تسلسل المورد',supplier.Supplier_Name as 'المورد'  FROM gate_supplier INNER JOIN supplier ON gate_supplier.Supplier_ID = supplier.Supplier_ID INNER JOIN gate ON gate.Gate_ID = gate_supplier.Gate_ID where gate_supplier.Type='مورد' and gate.Store_ID =" + storeId + " and gate.Date_Out is NULL UNION ALL SELECT gate_supplier.Gate_ID as 'التسلسل',gate_supplier.GateSupplier_ID as 'تسلسل المورد',customer.Customer_Name as 'المورد'  FROM gate_supplier INNER JOIN customer ON gate_supplier.Supplier_ID = customer.Customer_ID INNER JOIN gate ON gate.Gate_ID = gate_supplier.Gate_ID where gate_supplier.Type='عميل' and gate.Store_ID =" + storeId + " and gate.Date_Out is NULL UNION ALL SELECT gate_supplier.Gate_ID as 'التسلسل',gate_supplier.GateSupplier_ID as 'تسلسل المورد',store.Store_Name as 'المورد' FROM gate_supplier INNER JOIN store ON gate_supplier.Supplier_ID = store.Store_ID INNER JOIN gate ON gate.Gate_ID = gate_supplier.Gate_ID where gate_supplier.Type='مخزن' and gate.Store_ID =" + storeId + " and gate.Date_Out is NULL", conn);
            MySqlDataAdapter adapterperm = new MySqlDataAdapter("SELECT gate_supplier.GateSupplier_ID as 'تسلسل المورد',gate_permission.Supplier_PermissionNumber as 'رقم الاذن',gate_permission.Type as 'النوع' FROM gate_permission INNER JOIN gate_supplier ON gate_supplier.GateSupplier_ID = gate_permission.GateSupplier_ID INNER JOIN gate ON gate.Gate_ID = gate_supplier.Gate_ID where gate.Store_ID =" + storeId + " and gate.Date_Out is NULL", conn);

            adaptergate.Fill(sourceDataSet, "gate");
            adaptersupp.Fill(sourceDataSet, "gate_supplier");
            adapterperm.Fill(sourceDataSet, "gate_permission");

            //Set up a master-detail relationship between the DataTables 
            DataColumn keyColumn = sourceDataSet.Tables["gate"].Columns["التسلسل"];
            DataColumn foreignKeyColumn = sourceDataSet.Tables["gate_supplier"].Columns["التسلسل"];
            DataColumn foreignKeyColumn2 = sourceDataSet.Tables["gate_supplier"].Columns["تسلسل المورد"];
            DataColumn foreignKeyColumn3 = sourceDataSet.Tables["gate_permission"].Columns["تسلسل المورد"];
            sourceDataSet.Relations.Add("الموردين", keyColumn, foreignKeyColumn);
            sourceDataSet.Relations.Add("ارقام الاذون", foreignKeyColumn2, foreignKeyColumn3);
            
            gridControl1.DataSource = sourceDataSet.Tables["gate"];
        }

        public void clear()
        {
            foreach (Control co in this.panContent.Controls)
            {
                if (co is TextBox)
                {
                    co.Text = "";
                }
            }
        }
        public void clearAll()
        {
            foreach (Control co in this.panContent.Controls)
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
        }
        public XtraTabPage getTabPage(string text)
        {
            for (int i = 0; i < xtraTabControlStoresContent.TabPages.Count; i++)
                if (xtraTabControlStoresContent.TabPages[i].Text == text)
                {
                    return xtraTabControlStoresContent.TabPages[i];
                }
            return null;
        }
    }
}

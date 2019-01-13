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

namespace MainSystem
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
                Permissions_Edit form = new Permissions_Edit(this, Convert.ToInt16(gridView1.GetRowCellDisplayText(e.RowHandle, gridView1.Columns["التسلسل"])));
                form.ShowDialog();
            }
        }

        private void gridView1_SelectionChanged(object sender, DevExpress.Data.SelectionChangedEventArgs e)
        {
            try
            {
                if (e.Action == CollectionChangeAction.Add)
                {
                    conn.Open();
                    string query = "update transport set Date_Out='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' ,OutEmployee_ID=" + UserControl.EmpID + " where transport.Permission_Number=" + gridView1.GetRowCellDisplayText(e.ControllerRow, gridView1.Columns["التسلسل"]);
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
            string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"Store.txt");
            int storeId = Convert.ToInt16(System.IO.File.ReadAllText(path));

            DataSet sourceDataSet = new DataSet();
            //,employee.Employee_Name as 'مسئول التعتيق'
            MySqlDataAdapter adapterZone = new MySqlDataAdapter("SELECT transport.Permission_Number as 'التسلسل',transport.Reason as 'سبب الدخول',transport.Type as 'النوع',transport.Responsible as 'المسئول',transport.Date_Enter as 'وقت الدخول',transport.Driver_Name as 'السواق',transport.Car_Number as 'رقم العربية',transport.License_Number as 'رقم الرخصة',transport.Description as 'البيان' FROM transport INNER JOIN employee ON employee.Employee_ID = transport.TatiqEmp_ID WHERE transport.Store_ID =" + storeId + " and transport.Date_Out is NULL", conn);

            MySqlDataAdapter adapterArea = new MySqlDataAdapter("SELECT transport_permission.Permission_Number as 'التسلسل',transport_permission.Supplier_PermissionNumber as 'رقم الاذن',transport_permission.Type as 'النوع' FROM transport_permission inner join transport on transport.Permission_Number=transport_permission.Permission_Number where transport.Store_ID =" + storeId + " and transport.Date_Out is NULL", conn);

            adapterZone.Fill(sourceDataSet, "transport");
            adapterArea.Fill(sourceDataSet, "transport_permission");

            //Set up a master-detail relationship between the DataTables 
            DataColumn keyColumn = sourceDataSet.Tables["transport"].Columns["التسلسل"];
            DataColumn foreignKeyColumn = sourceDataSet.Tables["transport_permission"].Columns["التسلسل"];
            sourceDataSet.Relations.Add("ارقام الاذون", keyColumn, foreignKeyColumn);

            gridControl1.DataSource = sourceDataSet.Tables["transport"];
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

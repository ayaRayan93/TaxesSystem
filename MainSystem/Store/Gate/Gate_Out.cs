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
        bool loaded = false;
        bool flag = false;
        bool flag2 = false;

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

                loaded = true;
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
                    //search();
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

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                
                /*string query = "insert into transport (Reason,Type,Responsible,Car_ID,Car_Number,Driver_ID,Driver_Name,License_Number,Date_Enter,Store_ID,TatiqEmp_ID,Description,Employee_ID) values (@Reason,@Type,@Responsible,@Car_ID,@Car_Number,@Driver_ID,@Driver_Name,@License_Number,@Date_Enter,@Store_ID,@TatiqEmp_ID,@Description,@Employee_ID)";
                MySqlCommand com = new MySqlCommand(query, conn);
                com.Parameters.Add("@Reason", MySqlDbType.VarChar, 255);
                com.Parameters["@Reason"].Value = comReason.Text;
                com.Parameters.Add("@Type", MySqlDbType.VarChar, 255);
                com.Parameters["@Type"].Value = comType.Text;
                com.Parameters.Add("@Responsible", MySqlDbType.VarChar, 255);
                com.Parameters["@Responsible"].Value = comResponsible.Text;
                if (comCar.Text != "")
                {
                    com.Parameters.Add("@Car_ID", MySqlDbType.Int16, 11);
                    com.Parameters["@Car_ID"].Value = comCar.SelectedValue.ToString();
                    com.Parameters.Add("@Car_Number", MySqlDbType.VarChar, 255);
                    com.Parameters["@Car_Number"].Value = null;
                }
                else
                {
                    com.Parameters.Add("@Car_ID", MySqlDbType.Int16, 11);
                    com.Parameters["@Car_ID"].Value = null;
                    com.Parameters.Add("@Car_Number", MySqlDbType.VarChar, 255);
                    com.Parameters["@Car_Number"].Value = txtCar.Text;
                }
                if (comDriver.Text != "")
                {
                    com.Parameters.Add("@Driver_ID", MySqlDbType.Int16, 11);
                    com.Parameters["@Driver_ID"].Value = comDriver.SelectedValue.ToString();
                    com.Parameters.Add("@Driver_Name", MySqlDbType.VarChar, 255);
                    com.Parameters["@Driver_Name"].Value = null;
                }
                else
                {
                    com.Parameters.Add("@Driver_ID", MySqlDbType.Int16, 11);
                    com.Parameters["@Driver_ID"].Value = null;
                    com.Parameters.Add("@Driver_Name", MySqlDbType.VarChar, 255);
                    com.Parameters["@Driver_Name"].Value = txtDriver.Text;
                }
                com.Parameters.Add("@License_Number", MySqlDbType.VarChar, 255);
                com.Parameters["@License_Number"].Value = txtPermisionNum.Text;
                com.Parameters.Add("@Date_Enter", MySqlDbType.DateTime, 0);
                com.Parameters["@Date_Enter"].Value = DateTime.Now;

                string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"Store.txt");
                int storeId = Convert.ToInt16(System.IO.File.ReadAllText(path));

                com.Parameters.Add("@Store_ID", MySqlDbType.Int16, 11);
                com.Parameters["@Store_ID"].Value = storeId;
                com.Parameters.Add("@TatiqEmp_ID", MySqlDbType.Int16, 11);
                com.Parameters["@TatiqEmp_ID"].Value = comEmployee.SelectedValue.ToString();
                com.Parameters.Add("@Description", MySqlDbType.VarChar, 255);
                com.Parameters["@Description"].Value = txtDescription.Text;
                com.Parameters.Add("@Employee_ID", MySqlDbType.Int16, 11);
                com.Parameters["@Employee_ID"].Value = UserControl.EmpID;
                com.ExecuteNonQuery();

                query = "select Permission_Number from transport order by Permission_Number desc limit 1";
                com = new MySqlCommand(query, conn);
                int permissionNum = Convert.ToInt16(com.ExecuteScalar().ToString());

                for (int i = 0; i < checkedListBoxControlNum.ItemCount; i++)
                {
                    query = "insert into transport_permission(Permission_Number,Supplier_PermissionNumber) values(@Permission_Number,@Supplier_PermissionNumber)";
                    com = new MySqlCommand(query, conn);
                    com.Parameters.Add("@Permission_Number", MySqlDbType.Int16, 11);
                    com.Parameters["@Permission_Number"].Value = permissionNum;
                    com.Parameters.Add("@Supplier_PermissionNumber", MySqlDbType.Int16, 11);
                    com.Parameters["@Supplier_PermissionNumber"].Value = checkedListBoxControlNum.Items[i].Value.ToString();
                    com.ExecuteNonQuery();
                }

                clearAll();

                comType.Visible = false;
                labelType.Visible = false;
                comResponsible.Visible = false;
                labelResponsible.Visible = false;
                comEmployee.Visible = false;
                labelEmp.Visible = false;
                labelPerNum.Visible = false;
                txtPermisionNum.Visible = false;
                labelDriver.Visible = false;
                comDriver.Visible = false;
                txtDriver.Visible = false;
                labelCar.Visible = false;
                comCar.Visible = false;
                txtCar.Visible = false;
                txtLicense.Visible = false;
                labelLicense.Visible = false;
                txtDescription.Visible = false;
                labelDescription.Visible = false;
                btnAddNum.Visible = false;
                checkedListBoxControlNum.Visible = false;
                btnDeleteNum.Visible = false;*/
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            conn.Close();
        }
        
        //function
        public void search()
        {
            string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"Store.txt");
            int storeId = Convert.ToInt16(System.IO.File.ReadAllText(path));

            DataSet sourceDataSet = new DataSet();
            MySqlDataAdapter adapterZone = new MySqlDataAdapter("SELECT transport.Permission_Number as 'التسلسل',transport.Reason as 'سبب الدخول',transport.Type as 'النوع',transport.Responsible as 'المسئول',transport.License_Number as 'رقم الرخصة',transport.Date_Enter as 'وقت الدخول',transport.Description as 'البيان',transport.Driver_Name as 'السواق',transport.Car_Number as 'رقم العربية',employee.Employee_Name as 'مسئول التعتيق' FROM transport INNER JOIN employee ON employee.Employee_ID = transport.TatiqEmp_ID WHERE transport.Store_ID =" + storeId + " and transport.Date_Out is NULL", conn);

            MySqlDataAdapter adapterArea = new MySqlDataAdapter("SELECT transport_permission.Permission_Number as 'التسلسل',transport_permission.Supplier_PermissionNumber as 'رقم الاذن' FROM transport_permission inner join transport on transport.Permission_Number=transport_permission.Permission_Number where transport.Store_ID =" + storeId + " and transport.Date_Out is NULL", conn);

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

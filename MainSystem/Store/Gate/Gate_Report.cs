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
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TaxesSystem
{
    public partial class Gate_Report : Form
    {
        MySqlConnection conn;
        MainForm bankMainForm = null;
        XtraTabControl MainTabControlStores;
        DataRow row1;
        public static PermissionImage tipImage = null;

        public static GridControl gridcontrol;

        public Gate_Report(MainForm BankMainForm, XtraTabControl xtraTabControlStoresContent)
        {
            InitializeComponent();
            conn = new MySqlConnection(connection.connectionString);
            bankMainForm = BankMainForm;
            MainTabControlStores = xtraTabControlStoresContent;
            
            gridcontrol = gridControl1;
            
            //this.dateTimePicker1.Format = DateTimePickerFormat.Short;
            //this.dateTimePicker2.Format = DateTimePickerFormat.Short;
        }

        private void Bills_Transitions_Report_Load(object sender, EventArgs e)
        {
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
        }

        private void btnNewChosen_Click(object sender, EventArgs e)
        {
            try
            {
                clearCom();
                gridControl1.DataSource = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void gridView1_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            try
            {
                row1 = gridView1.GetDataRow(gridView1.GetRowHandle(e.RowHandle));

                if (e.Column.ToString() == "الصورة")
                {
                    if (tipImage == null)
                    {
                        tipImage = new PermissionImage(Convert.ToInt32(row1["التسلسل"].ToString()), Convert.ToInt32(row1["رقم الاذن"].ToString()), row1["النوع"].ToString());
                        
                        tipImage.Location = new Point(Cursor.Position.X, Cursor.Position.Y);
                        tipImage.Show();
                    }
                    else
                    {
                        tipImage.Close();
                        
                        tipImage = new PermissionImage(Convert.ToInt32(row1["التسلسل"].ToString()), Convert.ToInt32(row1["رقم الاذن"].ToString()), row1["النوع"].ToString());
                        
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

        //functions

        public void search()
        {
            //string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"Store.txt");
            //int storeId = Convert.ToInt32(System.IO.File.ReadAllText(path));

            string supString = BaseData.StoreID;
            int storeId = Convert.ToInt32(supString);

            DataSet sourceDataSet = new DataSet();
            //,employee.Employee_Name as 'مسئول التعتيق',gate.Responsible as 'المسئول'
            MySqlDataAdapter adapterGat = new MySqlDataAdapter("SELECT gate.Gate_ID as 'التسلسل',gate.Reason as 'سبب الدخول',gate.Type as 'النوع',gate.Date_Enter as 'وقت الدخول',gate.Date_Out as 'وقت الخروج',gate.Driver_Name as 'السواق',gate.Car_Number as 'رقم العربية',gate.License_Number as 'رقم الرخصة',gate.Description as 'البيان' FROM gate INNER JOIN gate_supplier ON gate.Gate_ID = gate_supplier.Gate_ID INNER JOIN gate_permission ON  gate_supplier.GateSupplier_ID = gate_permission.GateSupplier_ID LEFT JOIN employee ON employee.Employee_ID = gate.TatiqEmp_ID WHERE gate.Store_ID =" + storeId + " and (date(gate.Date_Enter) between '" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "' and '" + dateTimePicker2.Value.ToString("yyyy-MM-dd") + "' or date(gate.Date_Out) between '" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "' and '" + dateTimePicker2.Value.ToString("yyyy-MM-dd") + "')", conn);
            MySqlDataAdapter adapterSup = new MySqlDataAdapter("SELECT gate_supplier.Gate_ID as 'التسلسل',supplier.Supplier_Name as 'المورد',gate_supplier.Type as 'النوع',gate_supplier.GateSupplier_ID as 'ID' FROM gate INNER JOIN gate_supplier ON gate.Gate_ID = gate_supplier.Gate_ID INNER JOIN supplier ON supplier.Supplier_ID = gate_supplier.Supplier_ID INNER JOIN gate_permission ON  gate_supplier.GateSupplier_ID = gate_permission.GateSupplier_ID where gate.Store_ID =" + storeId + " and (date(gate.Date_Enter) between '" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "' and '" + dateTimePicker2.Value.ToString("yyyy-MM-dd") + "' or date(gate.Date_Out) between '" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "' and '" + dateTimePicker2.Value.ToString("yyyy-MM-dd") + "')", conn);
            MySqlDataAdapter adapterPerm = new MySqlDataAdapter("SELECT gate_permission.GateSupplier_ID as 'التسلسل',gate_permission.Supplier_PermissionNumber as 'رقم الاذن',gate_permission.Type as 'النوع',gate_permission.Permission_Image as 'الصورة' FROM gate INNER JOIN gate_supplier ON gate.Gate_ID = gate_supplier.Gate_ID INNER JOIN gate_permission ON  gate_supplier.GateSupplier_ID = gate_permission.GateSupplier_ID where gate.Store_ID =" + storeId + " and (date(gate.Date_Enter) between '" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "' and '" + dateTimePicker2.Value.ToString("yyyy-MM-dd") + "' or date(gate.Date_Out) between '" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "' and '" + dateTimePicker2.Value.ToString("yyyy-MM-dd") + "')", conn);

            adapterGat.Fill(sourceDataSet, "gate");
            adapterSup.Fill(sourceDataSet, "gate_supplier");
            adapterPerm.Fill(sourceDataSet, "gate_permission");

            //Set up a master-detail relationship between the DataTables 
            DataColumn keyColumn = sourceDataSet.Tables["gate"].Columns["التسلسل"];
            DataColumn foreignKeyColumn2 = sourceDataSet.Tables["gate_supplier"].Columns["التسلسل"];
            DataColumn foreignKeyColumn3 = sourceDataSet.Tables["gate_supplier"].Columns["ID"];
            DataColumn foreignKeyColumn4 = sourceDataSet.Tables["gate_permission"].Columns["التسلسل"];
            sourceDataSet.Relations.Add("ارقام الاذون", keyColumn, foreignKeyColumn2);
            sourceDataSet.Relations.Add("تفاصيل الاذون", foreignKeyColumn3, foreignKeyColumn4);

            gridControl1.DataSource = sourceDataSet.Tables["gate"];
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
                else if (co is DateTimePicker)
                {
                    dateTimePicker1.Value = DateTime.Now;
                    dateTimePicker2.Value = DateTime.Now;
                }
            }
        }
    }
}

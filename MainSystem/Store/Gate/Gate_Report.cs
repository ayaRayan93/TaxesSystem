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

namespace MainSystem
{
    public partial class Gate_Report : Form
    {
        MySqlConnection conn;
        MainForm bankMainForm = null;
        XtraTabControl MainTabControlBank;
        
        public static XtraTabPage MainTabPagePrintingTransitions;
        Panel panelPrintingTransitions;

        public static BillsTransitions_Print bankPrint;

        public static GridControl gridcontrol;
        bool loaded = false;
        bool loadedBranch = false;

        public Gate_Report(MainForm BankMainForm, XtraTabControl xtraTabControlStoresContent)
        {
            InitializeComponent();
            conn = new MySqlConnection(connection.connectionString);
            bankMainForm = BankMainForm;
            MainTabControlBank = MainForm.tabControlBank;
            
            MainTabPagePrintingTransitions = new XtraTabPage();
            panelPrintingTransitions = new Panel();
            
            gridcontrol = gridControl1;
            
            this.dateTimePicker1.Format = DateTimePickerFormat.Short;
            this.dateTimePicker2.Format = DateTimePickerFormat.Short;
        }

        private void Bills_Transitions_Report_Load(object sender, EventArgs e)
        {
            try
            {
                if (!loadedBranch)
                {
                    //loadBranch();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            conn.Close();
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

        //functions

        public void search()
        {
            string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"Store.txt");
            int storeId = Convert.ToInt16(System.IO.File.ReadAllText(path));

            DataSet sourceDataSet = new DataSet();
            //,employee.Employee_Name as 'مسئول التعتيق'
            MySqlDataAdapter adapterZone = new MySqlDataAdapter("SELECT gate.Permission_Number as 'التسلسل',gate.Reason as 'سبب الدخول',gate.Type as 'النوع',gate.Responsible as 'المسئول',gate.Date_Enter as 'وقت الدخول',gate.Date_Out as 'وقت الخروج',gate.Driver_Name as 'السواق',gate.Car_Number as 'رقم العربية',gate.License_Number as 'رقم الرخصة',gate.Description as 'البيان' FROM gate LEFT JOIN employee ON employee.Employee_ID = gate.TatiqEmp_ID WHERE gate.Store_ID =" + storeId + " and (gate.Date_Enter between '" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "' and '" + dateTimePicker2.Value.ToString("yyyy-MM-dd HH:mm:ss") + "' or gate.Date_Out between '" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "' and '" + dateTimePicker2.Value.ToString("yyyy-MM-dd HH:mm:ss") + "')", conn);

            MySqlDataAdapter adapterArea = new MySqlDataAdapter("SELECT gate_permission.Permission_Number as 'التسلسل',gate_permission.Supplier_PermissionNumber as 'رقم الاذن',gate_permission.Type as 'النوع',gate_permission.Permission_Image as 'الصورة' FROM gate_permission inner join gate on gate_permission.Permission_Number=gate.Permission_Number where gate.Store_ID =" + storeId + " and (gate.Date_Enter between '" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "' and '" + dateTimePicker2.Value.ToString("yyyy-MM-dd HH:mm:ss") + "' or gate.Date_Out between '" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "' and '" + dateTimePicker2.Value.ToString("yyyy-MM-dd HH:mm:ss") + "')", conn);

            adapterZone.Fill(sourceDataSet, "gate");
            adapterArea.Fill(sourceDataSet, "gate_permission");

            //Set up a master-detail relationship between the DataTables 
            DataColumn keyColumn = sourceDataSet.Tables["gate"].Columns["التسلسل"];
            DataColumn foreignKeyColumn = sourceDataSet.Tables["gate_permission"].Columns["التسلسل"];
            sourceDataSet.Relations.Add("ارقام الاذون", keyColumn, foreignKeyColumn);

            gridControl1.DataSource = sourceDataSet.Tables["gate"];
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

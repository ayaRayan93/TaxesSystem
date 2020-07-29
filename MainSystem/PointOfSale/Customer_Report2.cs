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
    public partial class Customer_Report2 : Form
    {
        MySqlConnection conn;
        XtraTabControl mainTabControl;

        //Panel panelAddCustomer;
        //Panel panelUpdateCustomer;
        //Panel panelPrintCustomer;

        public static Customer_Print2 customerPrint;

        public static GridControl gridcontrol;

        public Customer_Report2(XtraTabControl tabControl)
        {
            InitializeComponent();
            conn = new MySqlConnection(connection.connectionString);
            mainTabControl = tabControl;

            //panelAddCustomer = new Panel();
            //panelUpdateCustomer = new Panel();
            //panelPrintCustomer = new Panel();

            gridcontrol = gridControl1;
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

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                XtraTabPage xtraTabPage = getTabPage("tabPageAddCustomer2");
                if (xtraTabPage == null)
                {
                    MainForm.MainTabPageAddCustomer2.Controls.Clear();
                    Customer_Record form = new Customer_Record(mainTabControl);
                    MainForm.MainTabPageAddCustomer2.Name = "tabPageAddCustomer2";
                    MainForm.MainTabPageAddCustomer2.Text = "اضافة عميل";
                    MainForm.MainTabPageAddCustomer2.ImageOptions.Image = null;
                    //panelAddCustomer.Name = "panelAddCustomer";
                    //panelAddCustomer.Dock = DockStyle.Fill;
                    form.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                    form.Dock = DockStyle.Fill;
                    form.TopLevel = false;
                    //panelAddCustomer.Controls.Add(form);
                    MainForm.MainTabPageAddCustomer2.Controls.Add(form);
                    mainTabControl.TabPages.Add(MainForm.MainTabPageAddCustomer2);
                    mainTabControl.SelectedTabPage = MainForm.MainTabPageAddCustomer2;
                    form.Show();
                }
                else if (xtraTabPage.ImageOptions.Image != null)
                {
                    DialogResult dialogResult = MessageBox.Show("هناك تعديلات لم تحفظ بعد..هل انت متاكد انك تريد التجاهل؟", "تحذير", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (dialogResult == DialogResult.Yes)
                    {
                        MainForm.MainTabPageAddCustomer2.ImageOptions.Image = null;
                        MainForm.MainTabPageAddCustomer2.Controls.Clear();
                        //panelAddCustomer.Controls.Clear();
                        mainTabControl.SelectedTabPage = MainForm.MainTabPageAddCustomer2;
                        Customer_Record form = new Customer_Record(mainTabControl);
                        form.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                        form.Dock = DockStyle.Fill;
                        form.TopLevel = false;
                        //panelAddCustomer.Controls.Add(form);
                        MainForm.MainTabPageAddCustomer2.Controls.Add(form);
                        form.Show();
                    }
                    else if (dialogResult == DialogResult.No)
                    {
                        mainTabControl.SelectedTabPage = MainForm.MainTabPageAddCustomer2;
                    }
                }
                else
                {
                    MainForm.MainTabPageAddCustomer2.Controls.Clear();
                    //panelAddCustomer.Controls.Clear();
                    mainTabControl.SelectedTabPage = MainForm.MainTabPageAddCustomer2;
                    Customer_Record form = new Customer_Record(mainTabControl);
                    form.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                    form.Dock = DockStyle.Fill;
                    form.TopLevel = false;
                    //panelAddCustomer.Controls.Add(form);
                    MainForm.MainTabPageAddCustomer2.Controls.Add(form);
                    form.Show();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (UserControl.userType == 1)
            {
                try
                {
                    DataRowView selRow = (DataRowView)gridView1.GetRow(gridView1.GetSelectedRows()[0]);
                    if (selRow[0].ToString() != "")
                    {
                        XtraTabPage xtraTabPage = getTabPage("tabPageUpdateCustomer2");
                        if (xtraTabPage == null)
                        {
                            Customer_Update form = new Customer_Update(selRow, mainTabControl);
                            MainForm.MainTabPageUpdateCustomer2.Name = "tabPageUpdateCustomer2";
                            MainForm.MainTabPageUpdateCustomer2.Text = "تعديل بيانات عميل";
                            MainForm.MainTabPageUpdateCustomer2.ImageOptions.Image = null;
                            //panelUpdateCustomer.Name = "panelUpdateCustomer";
                            //panelUpdateCustomer.Dock = DockStyle.Fill;
                            MainForm.MainTabPageUpdateCustomer2.Controls.Clear();
                            form.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                            form.Dock = DockStyle.Fill;
                            form.TopLevel = false;
                            form.Show();
                            //panelUpdateCustomer.Controls.Add(form);
                            MainForm.MainTabPageUpdateCustomer2.Controls.Add(form);
                            mainTabControl.TabPages.Add(MainForm.MainTabPageUpdateCustomer2);
                            mainTabControl.SelectedTabPage = MainForm.MainTabPageUpdateCustomer2;
                        }
                        else if (xtraTabPage.ImageOptions.Image != null)
                        {
                            DialogResult dialogResult = MessageBox.Show("هناك تعديلات لم تحفظ بعد..هل انت متاكد انك تريد التجاهل؟", "تحذير", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                            if (dialogResult == DialogResult.Yes)
                            {
                                MainForm.MainTabPageUpdateCustomer2.ImageOptions.Image = null;
                                Customer_Update form = new Customer_Update(selRow, mainTabControl);
                                form.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                                form.Dock = DockStyle.Fill;
                                form.TopLevel = false;
                                form.Show();
                                MainForm.MainTabPageUpdateCustomer2.Controls.Clear();
                                //panelUpdateCustomer.Controls.Clear();
                                //panelUpdateCustomer.Controls.Add(form);
                                MainForm.MainTabPageUpdateCustomer2.Controls.Add(form);
                                mainTabControl.SelectedTabPage = MainForm.MainTabPageUpdateCustomer2;
                            }
                            else if (dialogResult == DialogResult.No)
                            {
                                mainTabControl.SelectedTabPage = MainForm.MainTabPageUpdateCustomer2;
                            }
                        }
                        else
                        {
                            MainForm.MainTabPageUpdateCustomer2.ImageOptions.Image = null;
                            Customer_Update form = new Customer_Update(selRow, mainTabControl);
                            form.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                            form.Dock = DockStyle.Fill;
                            form.TopLevel = false;
                            form.Show();
                            MainForm.MainTabPageUpdateCustomer2.Controls.Clear();
                            //panelUpdateCustomer.Controls.Clear();
                            //panelUpdateCustomer.Controls.Add(form);
                            MainForm.MainTabPageUpdateCustomer2.Controls.Add(form);
                            mainTabControl.SelectedTabPage = MainForm.MainTabPageUpdateCustomer2;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (UserControl.userType == 1)
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
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            try
            {
                MainForm.MainTabPagePrintCustomer2.Name = "tabPagePrintCustomer2";
                MainForm.MainTabPagePrintCustomer2.Text = "طباعة العملاء";
                customerPrint = new Customer_Print2();
                customerPrint.Size = new Size(1059, 638);
                customerPrint.TopLevel = false;
                customerPrint.FormBorderStyle = FormBorderStyle.None;
                customerPrint.Dock = DockStyle.Fill;
                MainForm.MainTabPagePrintCustomer2.Controls.Add(customerPrint);
                mainTabControl.TabPages.Add(MainForm.MainTabPagePrintCustomer2);
                customerPrint.Show();
                mainTabControl.SelectedTabPage = MainForm.MainTabPagePrintCustomer2;
                MainForm.loadedPrintCustomer = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void gridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (UserControl.userType == 1)
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
            DataSet sourceDataSet = new DataSet();
            //MySqlDataAdapter adapter = new MySqlDataAdapter("SELECT Delegate_ID,Delegate_Name,Phone,Address,Qualification,Birth_Date,National_ID,Social_Status,Mail,Photo,Start_Date,Target,Salary,Job_Hours,Info,Branch_ID,Branch_Name FROM delegate", conn);
            MySqlDataAdapter adapterCustomer = new MySqlDataAdapter("SELECT customer1.Customer_ID as 'الكود',customer1.Customer_Name as 'الاسم',customer1.Customer_Address as 'العنوان',customer1.Customer_Start as 'تاريخ البداية',customer1.Customer_NationalID as 'الرقم القومى',customer1.Customer_Email as 'الايميل',customer1.Customer_Type as 'النوع',customer2.Customer_Name as 'الضامن',customer1.Customer_OpenAccount as 'الرصيد الافتتاحي',customer1.Customer_Info as 'البيان' FROM customer AS customer1 LEFT JOIN custmer_client ON customer1.Customer_ID = custmer_client.Client_ID LEFT JOIN customer AS customer2 ON customer2.Customer_ID = custmer_client.Customer_ID ORDER BY customer1.Customer_ID", conn);

            MySqlDataAdapter adapterPhone = new MySqlDataAdapter("SELECT customer.Customer_ID as 'الكود',Phone as 'رقم التليفون' FROM customer_phone inner join customer on customer.Customer_ID=customer_phone.Customer_ID", conn);

            adapterCustomer.Fill(sourceDataSet, "customer");
            adapterPhone.Fill(sourceDataSet, "customer_phone");

            //Set up a master-detail relationship between the DataTables 
            DataColumn keyColumn = sourceDataSet.Tables["customer"].Columns["الكود"];
            DataColumn foreignKeyColumn = sourceDataSet.Tables["customer_phone"].Columns["الكود"];
            sourceDataSet.Relations.Add("ارقام التليفون", keyColumn, foreignKeyColumn);



            gridControl1.DataSource = sourceDataSet.Tables["customer"];
            gridView1.Columns["الرصيد الافتتاحي"].Visible = false;
            //gridView1.Columns["التسلسل"].Group();

            /*this.gridView1.GroupSummary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
                new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Min, "IdRandom", "Min = {0}", "Min")});*/

            /*gridView1.Columns[0].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            for (int i = 1; i < gridView1.Columns.Count; i++)
            {
                gridView1.Columns[i].Width = 150;
            }*/
        }

        public XtraTabPage getTabPage(string text)
        {
            for (int i = 0; i < mainTabControl.TabPages.Count; i++)
                if (mainTabControl.TabPages[i].Name == text)
                {
                    return mainTabControl.TabPages[i];
                }
            return null;
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

                    string query = "delete from customer_phone where Customer_ID=" + selRow[0].ToString();
                    MySqlCommand comand = new MySqlCommand(query, conn);
                    comand.ExecuteNonQuery();

                    query = "delete from customer where Customer_ID=" + selRow[0].ToString();
                    comand = new MySqlCommand(query, conn);
                    comand.ExecuteNonQuery();

                    UserControl.ItemRecord("customer", "حذف", Convert.ToInt32(selRow[0].ToString()), DateTime.Now, "", conn);
                }
                conn.Close();
                search();
            }
            else
            {
                MessageBox.Show("يجب ان تختار عنصر للحذف");
            }
        }

        private void txtClientPhone_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    DataSet sourceDataSet = new DataSet();
                    MySqlDataAdapter adapterCustomer = new MySqlDataAdapter("SELECT customer1.Customer_ID as 'الكود',customer1.Customer_Name as 'الاسم',customer1.Customer_Address as 'العنوان',customer1.Customer_Start as 'تاريخ البداية',customer1.Customer_NationalID as 'الرقم القومى',customer1.Customer_Email as 'الايميل',customer1.Customer_Type as 'النوع',customer1.Type as 'كاش/اجل',customer2.Customer_Name as 'الضامن',customer1.Customer_OpenAccount as 'الرصيد الافتتاحي',customer1.Customer_Info as 'البيان' FROM customer AS customer1 LEFT JOIN custmer_client ON customer1.Customer_ID = custmer_client.Client_ID LEFT JOIN customer AS customer2 ON customer2.Customer_ID = custmer_client.Customer_ID inner join customer_phone on customer1.Customer_ID=customer_phone.Customer_ID where customer_phone.Phone like '" + txtClientPhone.Text + "%' ORDER BY customer1.Customer_ID", conn);
                    MySqlDataAdapter adapterPhone = new MySqlDataAdapter("SELECT customer.Customer_ID as 'الكود',Phone as 'رقم التليفون' FROM customer_phone inner join customer on customer.Customer_ID=customer_phone.Customer_ID where customer_phone.Phone like '" + txtClientPhone.Text + "%'", conn);
                    adapterCustomer.Fill(sourceDataSet, "customer");
                    adapterPhone.Fill(sourceDataSet, "customer_phone");

                    //Set up a master-detail relationship between the DataTables 
                    DataColumn keyColumn = sourceDataSet.Tables["customer"].Columns["الكود"];
                    DataColumn foreignKeyColumn = sourceDataSet.Tables["customer_phone"].Columns["الكود"];
                    sourceDataSet.Relations.Add("ارقام التليفون", keyColumn, foreignKeyColumn);
                    gridControl1.DataSource = sourceDataSet.Tables["customer"];

                    gridView1.Columns["الرصيد الافتتاحي"].Visible = false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                conn.Close();
            }
        }
    }
}

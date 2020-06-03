using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Columns;
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

namespace MainSystem.CustomerService
{
    public partial class CustomerDeliveredBills : Form
    {
        MySqlConnection dbconnection;
        MainForm MainForm;
        public CustomerDeliveredBills(MainForm MainForm)
        {
            InitializeComponent();
            this.MainForm = MainForm;
            dbconnection = new MySqlConnection(connection.connectionString);
        }

        private void CustomerDeliveredBills_Load(object sender, EventArgs e)
        {
            try
            {
                dbconnection.Open();

                string query = "select * from branch";
                MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                DataTable dt = new DataTable();
                da.Fill(dt);
                comBranch.DataSource = dt;
                comBranch.DisplayMember = dt.Columns["Branch_Name"].ToString();
                comBranch.ValueMember = dt.Columns["Branch_ID"].ToString();
                comBranch.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                dbconnection.Open();
                displayProducts();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }
        private void btnNewChooes_Click(object sender, EventArgs e)
        {
            try
            {
                comBranch.Text = "";
                txtBranch.Text = "";
                dateTimeFrom.Text = DateTime.Now.Date.ToString();
                dateTimeTo.Text = DateTime.Now.Date.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btnReport_Click(object sender, EventArgs e)
        {
            try
            {
                MainForm.bindReportDeliveredCustomerBillsForm(dataGridView1, "تقرير فواتير تم تسليمها");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        void gridView2_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            try
            {
                DataRow dataRow = gridView2.GetDataRow(gridView2.FocusedRowHandle);
                MainForm.bindDisplayDeliveryBillsForm(dataRow);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
  
        //function
        public void displayProducts()
        {
            try
            {
                string subQuery = " date(customer_permissions.Date) between '" + dateTimeFrom.Value.ToString("yyyy-MM-dd") + "' and '" + dateTimeTo.Value.ToString("yyyy-MM-dd") + "'";

                string query = "select CustomerBill_ID from customer_return_permission where date(Date) between '" + dateTimeFrom.Value.ToString("yyyy-MM-dd") + "' and '" + dateTimeTo.Value.ToString("yyyy-MM-dd") + "' and CustomerBill_ID !=0";
                MySqlCommand com = new MySqlCommand(query, dbconnection);
                MySqlDataReader dr = com.ExecuteReader();
                string returnCustomerBill_ID = "";
                while (dr.Read())
                {
                    returnCustomerBill_ID += dr[0].ToString() + ",";
                }
                dr.Close();

                query = "SELECT CustomerBill_ID FROM customer_service_survey";
                com = new MySqlCommand(query, dbconnection);
                dr = com.ExecuteReader();
                while (dr.Read())
                {
                    returnCustomerBill_ID += dr[0].ToString() + ",";
                }
                dr.Close();

                returnCustomerBill_ID += "0";
                if (comBranch.Text != "")
                {
                    subQuery += " and customer_permissions.Branch_ID=" + comBranch.SelectedValue;
                }

                if (txtBranch.Text != "")
                {
                    subQuery += " and customer_permissions.BranchBillNumber=" + txtBranch.Text;
                }

                if (returnCustomerBill_ID != "")
                {
                    subQuery += " and CustomerBill_ID not in (" + returnCustomerBill_ID + ")";
                }

                query = "SELECT Customer_Permissin_ID,branch.Branch_Name as 'الفرع',customer_permissions.BranchBillNumber as 'رقم الفاتورة',customer_permissions.Customer_ID,customer_permissions.Customer_Name as 'مهندس/مقاول/تاجر',GROUP_CONCAT(c2.Phone)as 'تلفون م/م/ت',customer_permissions.Client_ID,customer_permissions.Client_Name as 'العميل',GROUP_CONCAT(c1.Phone)as 'تلفون العميل',DeliveredPerson as 'المستلم',DeliveredPersonPhone as 'تلفون المستلم',CustomerAddress as 'العنوان',customer_permissions.Date as 'التاريخ' FROM customer_permissions left join customer_phone as c1 on customer_permissions.Client_ID=c1.Customer_ID left join customer_phone as c2 on customer_permissions.Customer_ID=c2.Customer_ID INNER JOIN store ON store.Store_ID = customer_permissions.Store_ID inner join branch on branch.Branch_ID=customer_permissions.Branch_ID   WHERE " + subQuery + " group by customer_permissions.BranchBillNumber";

                MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                DataTable dt = new DataTable();
                da.Fill(dt);
                //Bind the grid control to the data source 
                dataGridView1.DataSource = dt;
                gridView2.Columns[0].Visible = false;
                gridView2.Columns["Client_ID"].Visible = false;
                gridView2.Columns["Customer_ID"].Visible = false;

                AddUnboundColumngridView2();
                AddRepositorygridView2();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        private void AddRepositorygridView2()
        {
            RepositoryItemButtonEdit edit = new RepositoryItemButtonEdit();
            edit.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            edit.ButtonClick += gridView2_ButtonClick;
            edit.Buttons[0].Caption = "استبيان";
            edit.Buttons[0].Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph;
            gridView2.Columns["استبيان"].ColumnEdit = edit;
        }
        private void AddUnboundColumngridView2()
        {
            if (gridView2.Columns["استبيان"] == null)
            {
                GridColumn unbColumn = gridView2.Columns.AddField("استبيان");
                unbColumn.VisibleIndex = gridView2.Columns.Count;
                unbColumn.UnboundType = DevExpress.Data.UnboundColumnType.Decimal;
            }
        }
        
    }
}

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

namespace MainSystem.Store.Export
{
    public partial class RestDeliveryItems : Form
    {
        private MySqlConnection dbconnection, dbconnectionr;
        bool loaded = false;
        MainForm MainForm;

        public RestDeliveryItems(MainForm MainForm)
        {
            try
            {
                InitializeComponent();
                dbconnection = new MySqlConnection(connection.connectionString);
                dbconnectionr = new MySqlConnection(connection.connectionString);
                this.MainForm = MainForm;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void RestDeliveryItems_Load(object sender, EventArgs e)
        {
            try
            {
                dbconnection.Open();
                string query = "select * from store ";
                MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                DataTable dt = new DataTable();
                da.Fill(dt);
                comStore.DataSource = dt;
                comStore.DisplayMember = dt.Columns["Store_Name"].ToString();
                comStore.ValueMember = dt.Columns["Store_ID"].ToString();
                comStore.Text = "";
                txtStoreID.Text = "";

                query = "select * from branch ";
                da = new MySqlDataAdapter(query, dbconnection);
                dt = new DataTable();
                da.Fill(dt);
                comBranch.DataSource = dt;
                comBranch.DisplayMember = dt.Columns["Branch_Name"].ToString();
                comBranch.ValueMember = dt.Columns["Branch_ID"].ToString();
                comBranch.Text = "";
                txtBranchID.Text = "";
                loaded = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }

        private void newChoose_Click(object sender, EventArgs e)
        {
            try
            {
                txtStoreID.Text = "";
                comStore.Text = "";
                txtBranchID.Text = "";
                comBranch.Text = "";
                dateTimeFrom.Value = DateTime.Now.Date;
                dateTimeTo.Value = DateTime.Now.Date;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                dbconnection.Open();
                dbconnectionr.Open();
                displayBill();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
            dbconnectionr.Close();
        }
        
        void gridView2_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            try
            {

                DataRow dataRow = gridView2.GetDataRow(gridView2.FocusedRowHandle);
                MainForm.bindDisplayDeliveryForm(dataRow.ItemArray[0].ToString(), dataRow.ItemArray[2].ToString(), 2);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //functions
        public void displayBill()
        {
            DateTime date = dateTimeFrom.Value;
            string d = date.ToString("yyyy-MM-dd hh:mm:ss");
            DateTime date2 = dateTimeTo.Value;
            string d2 = date2.ToString("yyyy-MM-dd hh:mm:ss");
            string subQuery = "";
            if (txtStoreID.Text != "")
                subQuery += " and product_bill.Store_ID=" + txtStoreID.Text;
            if (txtBranchID.Text != "")
                subQuery += " and customer_bill.Branch_ID=" + txtBranchID.Text;


            string xQuery = "select CustomerBill_ID from customer_return_bill inner join customer_return_bill_details on customer_return_bill.CustomerReturnBill_ID=customer_return_bill_details.CustomerReturnBill_ID ";
            MySqlCommand com = new MySqlCommand(xQuery, dbconnection);
            MySqlDataReader dr = com.ExecuteReader();
            string str1 = "";
            while (dr.Read())
            {
                if (dr[0].ToString() != "0")
                    str1 += dr[0].ToString() + ",";
            }
            dr.Close();
            str1 += 0;

            string query = "";
            if (d == d2)
                query = "select distinct Branch_BillNumber as 'كود الفاتورة',Branch_Name as 'الفرع' ,Branch_ID,Customer_Name as 'مهندس مقاول',Client_Name as 'العميل',Bill_Date as 'تاريخ الفاتورة',Shipped_Date as 'تاريخ الاستلام' from customer_bill inner join product_bill on customer_bill.CustomerBill_ID=product_bill.CustomerBill_ID where customer_bill.CustomerBill_ID not in (" + str1 + ") and Shipped_Date <= '" + d2 + "' and RecivedType='العميل' and RecivedFlag='تم تسليم جزء' and  case when Type_Buy='كاش' then Paid_Status=1 when Type_Buy='آجل' then Type_Buy='آجل' end " + subQuery;
            else
                query = "select distinct Branch_BillNumber as 'كود الفاتورة',Branch_Name as 'الفرع' ,Branch_ID,Customer_Name as 'مهندس مقاول',Client_Name as 'العميل',Bill_Date as 'تاريخ الفاتورة',Shipped_Date as 'تاريخ الاستلام' from customer_bill inner join product_bill on customer_bill.CustomerBill_ID=product_bill.CustomerBill_ID where customer_bill.CustomerBill_ID not in (" + str1 + ") and Shipped_Date between '" + d + "' and '" + d2 + "' and RecivedType='العميل' and RecivedFlag='تم تسليم جزء' and case when Type_Buy='كاش' then Paid_Status=1 when Type_Buy='آجل' then Type_Buy='آجل' end  " + subQuery;

            if (UserControl.userType == 1)
            {
                if (d == d2)
                    query = "select distinct Branch_BillNumber as 'كود الفاتورة',Branch_Name as 'الفرع' ,Branch_ID,Customer_Name as 'مهندس مقاول',Client_Name as 'العميل',Bill_Date as 'تاريخ الفاتورة',Shipped_Date as 'تاريخ الاستلام' from customer_bill inner join product_bill on customer_bill.CustomerBill_ID=product_bill.CustomerBill_ID where customer_bill.CustomerBill_ID not in (" + str1 + ") and Shipped_Date <= '" + d2 + "' and RecivedType='العميل' and RecivedFlag='تم تسليم جزء'  " + subQuery;
                else
                    query = "select distinct Branch_BillNumber as 'كود الفاتورة',Branch_Name as 'الفرع' ,Branch_ID,Customer_Name as 'مهندس مقاول',Client_Name as 'العميل',Bill_Date as 'تاريخ الفاتورة',Shipped_Date as 'تاريخ الاستلام' from customer_bill inner join product_bill on customer_bill.CustomerBill_ID=product_bill.CustomerBill_ID where customer_bill.CustomerBill_ID not in (" + str1 + ") and Shipped_Date between '" + d + "' and '" + d2 + "' and RecivedType='العميل' and RecivedFlag='تم تسليم جزء'   " + subQuery;
            }

            MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
            DataTable dt = new DataTable();
            da.Fill(dt);
            gridControl2.DataSource = dt;
            gridView2.Columns[2].Visible = false;
            AddUnboundColumngridView2();
            AddRepositorygridView2();
        }
        private void AddRepositorygridView2()
        {
            RepositoryItemButtonEdit edit = new RepositoryItemButtonEdit();
            edit.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            edit.ButtonClick += gridView2_ButtonClick;
            edit.Buttons[0].Caption = "تسليم اذن";
            edit.Buttons[0].Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph;
            gridView2.Columns["تسليم اذن"].ColumnEdit = edit;

        }
        private void AddUnboundColumngridView2()
        {
            if (gridView2.Columns["تسليم اذن"] == null)
            {
                GridColumn unbColumn = gridView2.Columns.AddField("تسليم اذن");
                unbColumn.VisibleIndex = gridView2.Columns.Count;
                unbColumn.UnboundType = DevExpress.Data.UnboundColumnType.Decimal;
            }
        }
    }
}

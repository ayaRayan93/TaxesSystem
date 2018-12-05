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
    public partial class Bills_Transitions_Report : Form
    {
        MySqlConnection conn;
        MainForm bankMainForm = null;
        XtraTabControl MainTabControlBank;
        
        public static XtraTabPage MainTabPagePrintingDepositCash;
        Panel panelPrintingDepositCash;


        public static BankDepositCash_Print bankPrint;

        public static GridControl gridcontrol;

        public Bills_Transitions_Report(MainForm BankMainForm)
        {
            InitializeComponent();
            conn = new MySqlConnection(connection.connectionString);
            bankMainForm = BankMainForm;
            MainTabControlBank = MainForm.tabControlBank;
            
            MainTabPagePrintingDepositCash = new XtraTabPage();
            panelPrintingDepositCash = new Panel();
            
            gridcontrol = gridControl1;
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

        private void btnReport_Click(object sender, EventArgs e)
        {
            try
            {
                MainTabPagePrintingDepositCash.Name = "tabPagePrintingBillsTransitions";
                MainTabPagePrintingDepositCash.Text = "طباعة حركة السداد";
                panelPrintingDepositCash.Name = "panelPrintingBillsTransitions";
                panelPrintingDepositCash.Dock = DockStyle.Fill;
                
                panelPrintingDepositCash.Controls.Clear();
                bankPrint = new BankDepositCash_Print();
                bankPrint.Size = new Size(1059, 638);
                bankPrint.TopLevel = false;
                bankPrint.FormBorderStyle = FormBorderStyle.None;
                bankPrint.Dock = DockStyle.Fill;
                panelPrintingDepositCash.Controls.Add(bankPrint);
                MainTabPagePrintingDepositCash.Controls.Add(panelPrintingDepositCash);
                MainTabControlBank.TabPages.Add(MainTabPagePrintingDepositCash);
                bankPrint.Show();
                MainTabControlBank.SelectedTabPage = MainTabPagePrintingDepositCash;

                MainForm.loadedPrintCash = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        
        //functions
        public void search()
        {
            MySqlDataAdapter adapterSale = new MySqlDataAdapter("SELECT customer_bill.CustomerBill_ID as 'ID','النوع',customer_bill.Branch_BillNumber as 'الفاتورة',branch.Branch_Name as 'الفرع',customer_bill.Bill_Date as 'التاريخ',customer_bill.Customer_ID,customer1.Customer_Name as 'المهندس/المقاول/التاجر',customer_bill.Client_ID,customer2.Customer_Name as 'العميل',customer_bill.Total_CostBD as 'الاجمالى',customer_bill.Total_Discount as 'الخصم',customer_bill.Total_CostAD as 'الصافى' FROM customer_bill INNER JOIN branch ON branch.Branch_ID = customer_bill.Branch_ID left join customer as customer1 on customer1.Customer_ID=customer_bill.Customer_ID left join customer as customer2 on customer2.Customer_ID=customer_bill.Client_ID ", conn);
            DataTable saledt = new DataTable();
            adapterSale.Fill(saledt);

            MySqlDataAdapter adapterReturn = new MySqlDataAdapter("SELECT customer_return_bill.CustomerReturnBill_ID as 'ID','النوع',customer_return_bill.Branch_BillNumber as 'الفاتورة',branch.Branch_Name as 'الفرع',customer_return_bill.Date as 'التاريخ',customer_return_bill.Customer_ID,customer1.Customer_Name as 'المهندس/المقاول/التاجر',customer_return_bill.Client_ID,customer2.Customer_Name as 'العميل',customer_return_bill.TotalCostAD as 'الصافى' FROM customer_return_bill INNER JOIN branch ON branch.Branch_ID = customer_return_bill.Branch_ID left join customer as customer1 on customer1.Customer_ID=customer_return_bill.Customer_ID left join customer as customer2 on customer2.Customer_ID=customer_return_bill.Client_ID ", conn);
            DataTable returndt = new DataTable();
            adapterReturn.Fill(returndt);

            DataTable dtAll = new DataTable();
            dtAll = saledt.Copy();
            dtAll.Merge(returndt);

            gridControl1.DataSource = dtAll;
            gridView1.Columns[0].Visible = false;
            gridView1.Columns["Customer_ID"].Visible = false;
            gridView1.Columns["Client_ID"].Visible = false;

            for (int i = 0; i < gridView1.RowCount; i++)
            {
                if (gridView1.GetRowCellDisplayText(i, "الاجمالى") != "")
                {
                    gridView1.SetRowCellValue(i, "النوع", "مبيعات");
                }
                else
                {
                    gridView1.SetRowCellValue(i, "النوع", "مرتجع");
                }
            }

            for (int i = 1; i < gridView1.Columns.Count; i++)
            {
                gridView1.Columns[i].Width = 100;
            }
            /////////////////////////////////////////
            MySqlDataAdapter adapter = new MySqlDataAdapter("SELECT transitions.Transition_ID as 'التسلسل',transitions.Type,transitions.Transition,transitions.Bill_Number as 'الفاتورة',transitions.Branch_Name as 'الفرع',transitions.Bank_ID,transitions.Bank_Name as 'الخزينة',transitions.Amount as 'المبلغ',transitions.Date as 'التاريخ',transitions.Payment_Method as 'طريقة الدفع',transitions.Customer_ID,customer1.Customer_Name as 'المهندس/المقاول/التاجر',transitions.Client_ID,customer2.Customer_Name as 'العميل',transitions.Payday as 'تاريخ الاستحقاق',transitions.Check_Number as 'رقم الشيك/الكارت',transitions.Visa_Type as 'نوع الكارت',transitions.Operation_Number as 'رقم العملية',transitions.Data as 'البيان',transitions.Error,transitions.Branch_ID FROM transitions left join customer as customer1 on customer1.Customer_ID=transitions.Customer_ID left join customer as customer2 on customer2.Customer_ID=transitions.Client_ID order by transitions.Date", conn);
            DataSet sourceDataSet = new DataSet();
            adapter.Fill(sourceDataSet);

            gridControl2.DataSource = sourceDataSet.Tables[0];
            gridView2.Columns["Customer_ID"].Visible = false;
            gridView2.Columns["Client_ID"].Visible = false;
            gridView2.Columns["Error"].Visible = false;
            gridView2.Columns["Bank_ID"].Visible = false;
            gridView2.Columns["Branch_ID"].Visible = false;

            gridView2.Columns[0].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            for (int i = 1; i < gridView2.Columns.Count; i++)
            {
                gridView2.Columns[i].Width = 100;
            }
        }
    }
}

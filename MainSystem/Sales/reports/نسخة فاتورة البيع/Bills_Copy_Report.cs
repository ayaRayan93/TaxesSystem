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
    public partial class Bills_Copy_Report : Form
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

        public Bills_Copy_Report(MainForm BankMainForm)
        {
            InitializeComponent();
            conn = new MySqlConnection(connection.connectionString);
            bankMainForm = BankMainForm;
            MainTabControlBank = MainForm.tabControlBank;
            
            MainTabPagePrintingTransitions = new XtraTabPage();
            panelPrintingTransitions = new Panel();
            
            gridcontrol = gridControl1;

            comBranch.AutoCompleteMode = AutoCompleteMode.Suggest;
            comBranch.AutoCompleteSource = AutoCompleteSource.ListItems;

            this.dateTimePicker1.Format = DateTimePickerFormat.Short;
            this.dateTimePicker2.Format = DateTimePickerFormat.Short;
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

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (comBranch.Text != "")
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
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            try
            {
                if (comBranch.Text != "")
                {
                    //int bilNum = 0;
                    double costSale = 0;
                    double costReturn = 0;
                    List<Transition_Items> bi = new List<Transition_Items>();
                    //DataTable dt = (DataTable)gridControl2.DataSource;
                    for (int i = 0; i < gridView1.RowCount; i++)
                    {
                        /*if (gridView2.GetRowCellDisplayText(i, gridView2.Columns["الفاتورة"]) != "")
                        {
                            bilNum = Convert.ToInt16(gridView2.GetRowCellDisplayText(i, gridView2.Columns["الفاتورة"]));
                        }
                        else
                        {
                            bilNum = 0;
                        }*/
                        if (gridView1.GetRowCellDisplayText(i, gridView1.Columns["دائن"]) != "")
                        {
                            costSale = Convert.ToDouble(gridView1.GetRowCellDisplayText(i, gridView1.Columns["دائن"]));
                        }
                        else
                        {
                            costSale = 0;
                        }
                        if (gridView1.GetRowCellDisplayText(i, gridView1.Columns["مدين"]) != "")
                        {
                            costReturn = Convert.ToDouble(gridView1.GetRowCellDisplayText(i, gridView1.Columns["مدين"]));
                        }
                        else
                        {
                            costReturn = 0;
                        }
                        //Transition_Items item = new Transition_Items() { ID = Convert.ToInt16(gridView2.GetRowCellDisplayText(i, gridView2.Columns["التسلسل"])), Operation_Type = gridView2.GetRowCellDisplayText(i, gridView2.Columns["عملية"]), Type = gridView2.GetRowCellDisplayText(i, gridView2.Columns["النوع"]), Bill_Number = gridView2.GetRowCellDisplayText(i, gridView2.Columns["الفاتورة"])/*, Branch_Name = gridView2.GetRowCellDisplayText(i, gridView2.Columns["الفرع"])*/, Client = gridView2.GetRowCellDisplayText(i, gridView2.Columns["العميل"]), Date = Convert.ToDateTime(gridView2.GetRowCellDisplayText(i, gridView2.Columns["التاريخ"])).ToString("yyyy-MM-dd"), CostSale = costSale, CostReturn = costReturn, Description = gridView2.GetRowCellDisplayText(i, gridView2.Columns["البيان"]) };
                        //bi.Add(item);
                    }

                    Print_Transition_Report f = new Print_Transition_Report();
                    f.PrintInvoice(dateTimePicker1.Value.Date, dateTimePicker2.Value.Date, comBranch.Text, bi);
                    f.ShowDialog();
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
        }

        //functions
        private void loadBranch()
        {
            conn.Open();
            string query = "select * from branch";
            MySqlDataAdapter da = new MySqlDataAdapter(query, conn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            comBranch.DataSource = dt;
            comBranch.DisplayMember = dt.Columns["Branch_Name"].ToString();
            comBranch.ValueMember = dt.Columns["Branch_ID"].ToString();
            comBranch.SelectedIndex = -1;

            loadedBranch = true;
        }

        public void search()
        {
            conn.Open();
            MySqlDataAdapter adapterSale = new MySqlDataAdapter("SELECT customer_bill.CustomerBill_ID as 'ID','النوع',customer_bill.Branch_BillNumber as 'الفاتورة',customer_bill.Bill_Date as 'التاريخ',customer_bill.Customer_ID,concat(customer1.Customer_Name,' ',customer_bill.Customer_ID) as 'المهندس/المقاول/التاجر',customer_bill.Client_ID,concat(customer2.Customer_Name,' ',customer_bill.Client_ID) as 'العميل',customer_bill.Total_CostBD as 'الاجمالى',customer_bill.Total_Discount as 'الخصم',customer_bill.Total_CostAD as 'الصافى' FROM customer_bill left join customer as customer1 on customer1.Customer_ID=customer_bill.Customer_ID left join customer as customer2 on customer2.Customer_ID=customer_bill.Client_ID where customer_bill.Branch_ID=" + comBranch.SelectedValue.ToString() + " and customer_bill.Bill_Date between '" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "' and '" + dateTimePicker2.Value.ToString("yyyy-MM-dd HH:mm:ss") + "'", conn);
            DataTable saledt = new DataTable();
            adapterSale.Fill(saledt);

            MySqlDataAdapter adapterReturn = new MySqlDataAdapter("SELECT customer_return_bill.CustomerReturnBill_ID as 'ID','النوع',customer_return_bill.Branch_BillNumber as 'الفاتورة',customer_return_bill.Date as 'التاريخ',customer_return_bill.Customer_ID,concat(customer1.Customer_Name,' ',customer_return_bill.Customer_ID) as 'المهندس/المقاول/التاجر',customer_return_bill.Client_ID,concat(customer2.Customer_Name,' ',customer_return_bill.Client_ID) as 'العميل',customer_return_bill.TotalCostAD as 'الصافى' FROM customer_return_bill left join customer as customer1 on customer1.Customer_ID=customer_return_bill.Customer_ID left join customer as customer2 on customer2.Customer_ID=customer_return_bill.Client_ID where customer_return_bill.Branch_ID=" + comBranch.SelectedValue.ToString() + " and customer_return_bill.Date between '" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "' and '" + dateTimePicker2.Value.ToString("yyyy-MM-dd HH:mm:ss") + "'", conn);
            DataTable returndt = new DataTable();
            adapterReturn.Fill(returndt);

            DataTable dtAll = new DataTable();
            dtAll = saledt.Copy();
            dtAll.Merge(returndt);

            gridControl1.DataSource = dtAll;
            gridView1.Columns[0].Visible = false;
            gridView1.Columns["Customer_ID"].Visible = false;
            gridView1.Columns["Client_ID"].Visible = false;
            if (gridView1.IsLastVisibleRow)
            {
                gridView1.FocusedRowHandle = gridView1.RowCount - 1;
            }
            for (int i = 0; i < gridView1.RowCount; i++)
            {
                if (gridView1.GetRowCellDisplayText(i, "الاجمالى") != "")
                {
                    gridView1.SetRowCellValue(i, "النوع", "بيع");
                }
                else
                {
                    gridView1.SetRowCellValue(i, "النوع", "مرتجع");
                }
            }

            if (!loaded)
            {
                for (int i = 1; i < gridView1.Columns.Count; i++)
                {
                    gridView1.Columns[i].Width = 110;
                }
            }
           
            //labelSale.Text = totalSale.ToString();
            //labelReturn.Text = totalReturn.ToString();
            loaded = true;
        }
    }
}

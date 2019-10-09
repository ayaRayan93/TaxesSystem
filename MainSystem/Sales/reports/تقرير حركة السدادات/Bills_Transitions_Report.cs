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
        
        public static XtraTabPage MainTabPagePrintingTransitions;
        Panel panelPrintingTransitions;

        public static BillsTransitions_Print bankPrint;

        public static GridControl gridcontrol;
        bool loaded = false;
        bool loadedBranch = false;

        public Bills_Transitions_Report(MainForm BankMainForm)
        {
            InitializeComponent();
            conn = new MySqlConnection(connection.connectionString);
            bankMainForm = BankMainForm;
            
            MainTabPagePrintingTransitions = new XtraTabPage();
            panelPrintingTransitions = new Panel();
            
            gridcontrol = gridControl1;

            comBranch.AutoCompleteMode = AutoCompleteMode.Suggest;
            comBranch.AutoCompleteSource = AutoCompleteSource.ListItems;

            //this.dateTimePicker1.Format = DateTimePickerFormat.Short;
            //this.dateTimePicker2.Format = DateTimePickerFormat.Short;
        }

        private void GridView1_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            GridView view = sender as GridView;
            view.SetRowCellValue(e.RowHandle, gridView1.Columns[0], "");
            view.SetRowCellValue(e.RowHandle, gridView1.Columns[1], "");
            view.SetRowCellValue(e.RowHandle, gridView1.Columns[2], "");
            view.SetRowCellValue(e.RowHandle, gridView1.Columns[3], "");
            view.SetRowCellValue(e.RowHandle, gridView1.Columns[4], "");
            view.SetRowCellValue(e.RowHandle, gridView1.Columns[5], "");
            view.SetRowCellValue(e.RowHandle, gridView1.Columns[6], "");
            view.SetRowCellValue(e.RowHandle, gridView1.Columns[7], "");
            view.SetRowCellValue(e.RowHandle, gridView1.Columns[8], "");
            view.SetRowCellValue(e.RowHandle, gridView1.Columns[9], "");
            view.SetRowCellValue(e.RowHandle, gridView1.Columns[10], "");
            view.SetRowCellValue(e.RowHandle, gridView1.Columns[11], "");
        }

        private void Bills_Transitions_Report_Load(object sender, EventArgs e)
        {
            try
            {
                if (!loadedBranch)
                {
                    DataHelperClassBillsTransitions dh = new DataHelperClassBillsTransitions(DSparametrBillsTransitions.doubleDS);
                    gridControl1.DataSource = dh.DataSet;
                    gridControl1.DataMember = dh.DataMember;
                    gridView1.InitNewRow += GridView1_InitNewRow;

                    loadBranch();

                    if(UserControl.userType != 1 && UserControl.userType != 13)
                    {
                        comBranch.Enabled = false;
                        comBranch.DropDownStyle = ComboBoxStyle.DropDownList;
                        comBranch.SelectedValue = UserControl.EmpBranchID;
                    }
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
                if (comBranch.Text != "" && txtBranchID.Text != "")
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
                if (comBranch.Text != "" && txtBranchID.Text != "")
                {
                    //int bilNum = 0;
                    double costSale = 0;
                    double costReturn = 0;
                    List<Transition_Items> bi = new List<Transition_Items>();
                    //DataTable dt = (DataTable)gridControl2.DataSource;
                    for (int i = 0; i < gridView2.RowCount; i++)
                    {
                        /*if (gridView2.GetRowCellDisplayText(i, gridView2.Columns["الفاتورة"]) != "")
                        {
                            bilNum = Convert.ToInt32(gridView2.GetRowCellDisplayText(i, gridView2.Columns["الفاتورة"]));
                        }
                        else
                        {
                            bilNum = 0;
                        }*/
                        if (gridView2.GetRowCellDisplayText(i, gridView2.Columns["دائن"]) != "")
                        {
                            costSale = Convert.ToDouble(gridView2.GetRowCellDisplayText(i, gridView2.Columns["دائن"]));
                        }
                        else
                        {
                            costSale = 0;
                        }
                        if (gridView2.GetRowCellDisplayText(i, gridView2.Columns["مدين"]) != "")
                        {
                            costReturn = Convert.ToDouble(gridView2.GetRowCellDisplayText(i, gridView2.Columns["مدين"]));
                        }
                        else
                        {
                            costReturn = 0;
                        }
                        Transition_Items item = new Transition_Items() { ID = Convert.ToInt32(gridView2.GetRowCellDisplayText(i, gridView2.Columns["التسلسل"])), /*Operation_Type = gridView2.GetRowCellDisplayText(i, gridView2.Columns["عملية"]),*/ Type = gridView2.GetRowCellDisplayText(i, gridView2.Columns["النوع"]), Bill_Number = gridView2.GetRowCellDisplayText(i, gridView2.Columns["الفاتورة"])/*, Branch_Name = gridView2.GetRowCellDisplayText(i, gridView2.Columns["الفرع"])*/, Client = gridView2.GetRowCellDisplayText(i, gridView2.Columns["العميل"]), Date = Convert.ToDateTime(gridView2.GetRowCellDisplayText(i, gridView2.Columns["التاريخ"])).ToString("yyyy-MM-dd"), CostSale = costSale, CostReturn = costReturn, Description = gridView2.GetRowCellDisplayText(i, gridView2.Columns["البيان"]) };
                        bi.Add(item);
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
            double totalBill = 0;
            double totalReturned = 0;
            //dateTimePicker2.Value.ToString("yyyy-MM-dd HH:mm:ss")
            /*MySqlDataAdapter adapterSale = new MySqlDataAdapter("SELECT customer_bill.CustomerBill_ID as 'ID','النوع',customer_bill.Branch_BillNumber as 'الفاتورة',customer_bill.Type_Buy as 'نوع الفاتورة',customer_bill.Bill_Date as 'التاريخ',customer_bill.Customer_ID,concat(customer1.Customer_Name,' ',customer_bill.Customer_ID) as 'المهندس/المقاول/التاجر',customer_bill.Client_ID,concat(customer2.Customer_Name,' ',customer_bill.Client_ID) as 'العميل',customer_bill.Total_CostBD as 'الاجمالى',customer_bill.Total_Discount as 'الخصم',customer_bill.Total_CostAD as 'الصافى' FROM customer_bill left join customer as customer1 on customer1.Customer_ID=customer_bill.Customer_ID left join customer as customer2 on customer2.Customer_ID=customer_bill.Client_ID where customer_bill.Branch_ID=" + comBranch.SelectedValue.ToString() + " and date(customer_bill.Bill_Date) between '" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "' and '" + dateTimePicker2.Value.ToString("yyyy-MM-dd") + "'", conn);
            DataTable saledt = new DataTable();
            adapterSale.Fill(saledt);

            MySqlDataAdapter adapterReturn = new MySqlDataAdapter("SELECT customer_return_bill.CustomerReturnBill_ID as 'ID','النوع',customer_return_bill.Branch_BillNumber as 'الفاتورة',customer_return_bill.Type_Buy as 'نوع الفاتورة',customer_return_bill.Date as 'التاريخ',customer_return_bill.Customer_ID,concat(customer1.Customer_Name,' ',customer_return_bill.Customer_ID) as 'المهندس/المقاول/التاجر',customer_return_bill.Client_ID,concat(customer2.Customer_Name,' ',customer_return_bill.Client_ID) as 'العميل',customer_return_bill.TotalCostAD as 'الصافى' FROM customer_return_bill left join customer as customer1 on customer1.Customer_ID=customer_return_bill.Customer_ID left join customer as customer2 on customer2.Customer_ID=customer_return_bill.Client_ID where customer_return_bill.Branch_ID=" + comBranch.SelectedValue.ToString() + " and date(customer_return_bill.Date) between '" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "' and '" + dateTimePicker2.Value.ToString("yyyy-MM-dd") + "'", conn);
            DataTable returndt = new DataTable();
            adapterReturn.Fill(returndt);

            DataTable dtAll = new DataTable();
            dtAll = saledt.Copy();
            dtAll.Merge(returndt);

            gridControl1.DataSource = dtAll;
            gridView1.Columns[0].Visible = false;
            gridView1.Columns["Customer_ID"].Visible = false;
            gridView1.Columns["Client_ID"].Visible = false;*/
            while (gridView1.RowCount != 0)
            {
                gridView1.SelectAll();
                gridView1.DeleteSelectedRows();
            }

            string query = "SELECT distinct customer_bill.CustomerBill_ID as 'ID',customer_bill.Branch_BillNumber as 'الفاتورة',customer_bill.Type_Buy as 'نوع الفاتورة',customer_bill.Bill_Date as 'التاريخ',customer_bill.Customer_ID,concat(customer1.Customer_Name,' ',customer_bill.Customer_ID) as 'المهندس/المقاول/التاجر',customer_bill.Client_ID,concat(customer2.Customer_Name,' ',customer_bill.Client_ID) as 'العميل',customer_bill.Total_CostBD as 'الاجمالى',customer_bill.Total_Discount as 'الخصم',customer_bill.Total_CostAD as 'الصافى' FROM customer_bill left join customer as customer1 on customer1.Customer_ID=customer_bill.Customer_ID left join customer as customer2 on customer2.Customer_ID=customer_bill.Client_ID INNER JOIN product_bill ON product_bill.CustomerBill_ID = customer_bill.CustomerBill_ID where customer_bill.Branch_ID=" + comBranch.SelectedValue.ToString() + " and date(customer_bill.Bill_Date) between '" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "' and '" + dateTimePicker2.Value.ToString("yyyy-MM-dd") + "'";
            MySqlCommand comand = new MySqlCommand(query, conn);
            MySqlDataReader dr = comand.ExecuteReader();
            while (dr.Read())
            {
                gridView1.AddNewRow();
                int rowHandle = gridView1.GetRowHandle(gridView1.DataRowCount);
                if (gridView1.IsNewItemRow(rowHandle))
                {
                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns["ColID"], dr["ID"].ToString());
                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns["ColType"], "بيع");
                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns["ColBill"], dr["الفاتورة"].ToString());
                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns["ColBillType"], dr["نوع الفاتورة"].ToString());
                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns["ColDate"], dr["التاريخ"].ToString());
                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns["ColCustomer_ID"], dr["Customer_ID"].ToString());
                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns["ColCustomer"], dr["المهندس/المقاول/التاجر"].ToString());
                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns["ColClient_ID"], dr["Client_ID"].ToString());
                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns["ColClient"], dr["العميل"].ToString());
                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns["ColTotal"], dr["الاجمالى"].ToString());
                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns["ColDiscount"], dr["الخصم"].ToString());
                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns["ColSafy"], dr["الصافى"].ToString());
                    totalBill += Convert.ToDouble(dr["الصافى"].ToString());
                }
            }
            dr.Close();

            query = "SELECT distinct customer_return_bill.CustomerReturnBill_ID as 'ID',customer_return_bill.Branch_BillNumber as 'الفاتورة',customer_return_bill.Type_Buy as 'نوع الفاتورة',customer_return_bill.Date as 'التاريخ',customer_return_bill.Customer_ID,concat(customer1.Customer_Name,' ',customer_return_bill.Customer_ID) as 'المهندس/المقاول/التاجر',customer_return_bill.Client_ID,concat(customer2.Customer_Name,' ',customer_return_bill.Client_ID) as 'العميل',customer_return_bill.TotalCostAD as 'الصافى' FROM customer_return_bill left join customer as customer1 on customer1.Customer_ID=customer_return_bill.Customer_ID left join customer as customer2 on customer2.Customer_ID=customer_return_bill.Client_ID INNER JOIN customer_return_bill_details ON customer_return_bill_details.CustomerReturnBill_ID = customer_return_bill.CustomerReturnBill_ID where customer_return_bill.Branch_ID=" + comBranch.SelectedValue.ToString() + " and date(customer_return_bill.Date) between '" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "' and '" + dateTimePicker2.Value.ToString("yyyy-MM-dd") + "'";
            comand = new MySqlCommand(query, conn);
            dr = comand.ExecuteReader();
            while (dr.Read())
            {
                gridView1.AddNewRow();
                int rowHandle = gridView1.GetRowHandle(gridView1.DataRowCount);
                if (gridView1.IsNewItemRow(rowHandle))
                {
                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns["ColID"], dr["ID"].ToString());
                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns["ColType"], "مرتجع");
                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns["ColBill"], dr["الفاتورة"].ToString());
                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns["ColBillType"], dr["نوع الفاتورة"].ToString());
                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns["ColDate"], dr["التاريخ"].ToString());
                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns["ColCustomer_ID"], dr["Customer_ID"].ToString());
                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns["ColCustomer"], dr["المهندس/المقاول/التاجر"].ToString());
                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns["ColClient_ID"], dr["Client_ID"].ToString());
                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns["ColClient"], dr["العميل"].ToString());
                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns["ColSafy"], dr["الصافى"].ToString());
                    totalReturned += Convert.ToDouble(dr["الصافى"].ToString());
                }
            }
            dr.Close();

            if (gridView1.IsLastVisibleRow)
            {
                gridView1.FocusedRowHandle = gridView1.RowCount - 1;
            }
            /*for (int i = 0; i < gridView1.RowCount; i++)
            {
                if (gridView1.GetRowCellDisplayText(i, "ColTotal") != "")
                {
                    gridView1.SetRowCellValue(i, "ColType", "بيع");
                    totalBill += Convert.ToDouble(gridView1.GetRowCellDisplayText(rowHandle, "ColSafy").ToString());
                }
                else
                {
                    gridView1.SetRowCellValue(i, "ColType", "مرتجع");
                    totalReturned += Convert.ToDouble(gridView1.GetRowCellDisplayText(i, "ColSafy").ToString());
                }
            }

            if (!loaded)
            {
                for (int i = 1; i < gridView1.Columns.Count; i++)
                {
                    gridView1.Columns[i].Width = 110;
                }
            }*/

            txtTotalBills.Text = totalBill.ToString();
            txtTotalReturn.Text = totalReturned.ToString();
            txtSafy.Text = (totalBill - totalReturned).ToString();
            /////////////////////////////////////////
            double totalSale = 0;
            double totalReturn = 0;
            //,transitions.Branch_Name as 'الفرع',transitions.Transition as 'عملية'
            MySqlDataAdapter adapter = new MySqlDataAdapter("SELECT transitions.Transition_ID as 'التسلسل',transitions.Type as 'النوع',transitions.Bill_Number as 'الفاتورة',transitions.Date as 'التاريخ',concat(customer2.Customer_Name,' ',transitions.Client_ID) as 'العميل',transitions.Amount as 'دائن',transitions.Amount as 'مدين',transitions.Data as 'البيان',bank.Bank_Name as 'الخزينة',transitions.Payment_Method as 'طريقة الدفع',transitions.Payday as 'تاريخ الاستحقاق',transitions.Check_Number as 'رقم الشيك/الكارت',transitions.Visa_Type as 'نوع الكارت',transitions.Operation_Number as 'رقم العملية',transitions.Bank_ID,transitions.Customer_ID,transitions.Client_ID FROM transitions INNER JOIN branch ON branch.Branch_ID = transitions.Branch_ID INNER JOIN bank ON bank.Bank_ID = transitions.Bank_ID left join customer as customer1 on customer1.Customer_ID=transitions.Customer_ID left join customer as customer2 on customer2.Customer_ID=transitions.Client_ID where Transition_ID=0 and transitions.Error=0 and date(transitions.Date) between '" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "' and '" + dateTimePicker2.Value.ToString("yyyy-MM-dd") + "' order by transitions.Date", conn);
            DataSet sourceDataSet = new DataSet();
            adapter.Fill(sourceDataSet);
            gridControl2.DataSource = sourceDataSet.Tables[0];

            query = "SELECT transitions.Transition_ID as 'التسلسل',transitions.Transition as 'عملية',transitions.Type as 'النوع',transitions.Bill_Number as 'الفاتورة',transitions.Branch_Name as 'الفرع',transitions.Date as 'التاريخ',concat(customer1.Customer_Name,' ',transitions.Customer_ID) as 'المهندس/المقاول/التاجر',concat(customer2.Customer_Name,' ',transitions.Client_ID) as 'العميل',transitions.Amount as 'المبلغ',transitions.Data as 'البيان',bank.Bank_Name as 'الخزينة',transitions.Payment_Method as 'طريقة الدفع',transitions.Payday as 'تاريخ الاستحقاق',transitions.Check_Number as 'رقم الشيك/الكارت',transitions.Visa_Type as 'نوع الكارت',transitions.Operation_Number as 'رقم العملية',transitions.Bank_ID,transitions.Customer_ID,transitions.Client_ID FROM transitions INNER JOIN bank ON bank.Bank_ID = transitions.Bank_ID left join customer as customer1 on customer1.Customer_ID=transitions.Customer_ID left join customer as customer2 on customer2.Customer_ID=transitions.Client_ID where transitions.TransitionBranch_ID=" + comBranch.SelectedValue.ToString() + " and transitions.Error=0 and date(transitions.Date) between '" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "' and '" + dateTimePicker2.Value.ToString("yyyy-MM-dd") + "' order by transitions.Date";
            comand = new MySqlCommand(query, conn);
            dr = comand.ExecuteReader();
            while (dr.Read())
            {
                gridView2.AddNewRow();
                int rowHandle = gridView2.GetRowHandle(gridView2.DataRowCount);
                if (gridView2.IsNewItemRow(rowHandle))
                {
                    gridView2.SetRowCellValue(rowHandle, gridView2.Columns["التسلسل"], dr["التسلسل"].ToString());
                    //gridView2.SetRowCellValue(rowHandle, gridView2.Columns["عملية"], dr["عملية"].ToString());
                    gridView2.SetRowCellValue(rowHandle, gridView2.Columns["النوع"], dr["النوع"].ToString());
                    gridView2.SetRowCellValue(rowHandle, gridView2.Columns["الفاتورة"], dr["الفاتورة"].ToString());
                    //gridView2.SetRowCellValue(rowHandle, gridView2.Columns["الفرع"], dr["الفرع"].ToString());
                    gridView2.SetRowCellValue(rowHandle, gridView2.Columns["التاريخ"], dr["التاريخ"].ToString());
                    if (dr["العميل"].ToString() != "")
                    {
                        gridView2.SetRowCellValue(rowHandle, gridView2.Columns["العميل"], dr["العميل"].ToString());
                    }
                    else
                    {
                        gridView2.SetRowCellValue(rowHandle, gridView2.Columns["العميل"], dr["المهندس/المقاول/التاجر"].ToString());
                    }
                    if (dr["عملية"].ToString() == "ايداع")
                    {
                        gridView2.SetRowCellValue(rowHandle, gridView2.Columns["دائن"], dr["المبلغ"].ToString());
                        totalSale += Convert.ToDouble(dr["المبلغ"].ToString());
                    }
                    else
                    {
                        gridView2.SetRowCellValue(rowHandle, gridView2.Columns["مدين"], dr["المبلغ"].ToString());
                        totalReturn += Convert.ToDouble(dr["المبلغ"].ToString());
                    }
                    gridView2.SetRowCellValue(rowHandle, gridView2.Columns["البيان"], dr["البيان"].ToString());
                    gridView2.SetRowCellValue(rowHandle, gridView2.Columns["الخزينة"], dr["الخزينة"].ToString());
                    gridView2.SetRowCellValue(rowHandle, gridView2.Columns["طريقة الدفع"], dr["طريقة الدفع"].ToString());
                    if (dr["تاريخ الاستحقاق"].ToString() == "")
                    {
                        gridView2.SetRowCellValue(rowHandle, gridView2.Columns["تاريخ الاستحقاق"], null);
                    }
                    else
                    {
                        gridView2.SetRowCellValue(rowHandle, gridView2.Columns["تاريخ الاستحقاق"], dr["تاريخ الاستحقاق"].ToString());
                    }
                    gridView2.SetRowCellValue(rowHandle, gridView2.Columns["رقم الشيك/الكارت"], dr["رقم الشيك/الكارت"].ToString());
                    gridView2.SetRowCellValue(rowHandle, gridView2.Columns["نوع الكارت"], dr["نوع الكارت"].ToString());
                    gridView2.SetRowCellValue(rowHandle, gridView2.Columns["رقم العملية"], dr["رقم العملية"].ToString());
                    gridView2.SetRowCellValue(rowHandle, gridView2.Columns["Bank_ID"], dr["Bank_ID"].ToString());
                    //gridView2.SetRowCellValue(rowHandle, gridView2.Columns["Branch_ID"], dr["Branch_ID"].ToString());
                    gridView2.SetRowCellValue(rowHandle, gridView2.Columns["Customer_ID"], dr["Customer_ID"].ToString());
                    gridView2.SetRowCellValue(rowHandle, gridView2.Columns["Client_ID"], dr["Client_ID"].ToString());
                }
            }
            dr.Close();

            /*query = "SELECT bank_transfer.BankTransfer_ID as 'التسلسل',concat(frombranch.Branch_Name,'/',tobranch.Branch_Name) as 'الفرع',bank_transfer.Date as 'التاريخ',bank_transfer.Money as 'المبلغ',bank_transfer.Description as 'البيان',concat(frombank.Bank_Name,'/',tobank.Bank_Name) as 'الخزينة',bank_transfer.FromBranch_ID,bank_transfer.ToBranch_ID FROM bank_transfer INNER JOIN bank as frombank ON bank_transfer.FromBank_ID = frombank.Bank_ID INNER JOIN bank as tobank ON bank_transfer.ToBank_ID = tobank.Bank_ID  INNER JOIN branch as frombranch ON bank_transfer.FromBranch_ID = frombranch.Branch_ID INNER JOIN branch as tobranch ON bank_transfer.ToBranch_ID = tobranch.Branch_ID where (bank_transfer.FromBranch_ID=" + comBranch.SelectedValue.ToString() + " or bank_transfer.ToBranch_ID=" + comBranch.SelectedValue.ToString() + ") and bank_transfer.Error=0 and bank_transfer.Date between '" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "' and '" + dateTimePicker2.Value.ToString("yyyy-MM-dd HH:mm:ss") + "' order by bank_transfer.Date";
            comand = new MySqlCommand(query, conn);
            dr = comand.ExecuteReader();
            while (dr.Read())
            {
                gridView2.AddNewRow();
                int rowHandle = gridView2.GetRowHandle(gridView2.DataRowCount);
                if (gridView2.IsNewItemRow(rowHandle))
                {
                    gridView2.SetRowCellValue(rowHandle, gridView2.Columns["التسلسل"], dr["التسلسل"].ToString());
                    gridView2.SetRowCellValue(rowHandle, gridView2.Columns["الفرع"], dr["الفرع"].ToString());
                    gridView2.SetRowCellValue(rowHandle, gridView2.Columns["التاريخ"], dr["التاريخ"].ToString());
                    
                    if (dr["ToBranch_ID"].ToString() == comBranch.SelectedValue.ToString())
                    {
                        gridView2.SetRowCellValue(rowHandle, gridView2.Columns["دائن"], dr["المبلغ"].ToString());
                        totalSale += Convert.ToDouble(dr["المبلغ"].ToString());
                    }
                    if (dr["FromBranch_ID"].ToString() == comBranch.SelectedValue.ToString())
                    {
                        gridView2.SetRowCellValue(rowHandle, gridView2.Columns["مدين"], dr["المبلغ"].ToString());
                        totalReturn += Convert.ToDouble(dr["المبلغ"].ToString());
                    }
                    gridView2.SetRowCellValue(rowHandle, gridView2.Columns["البيان"], dr["البيان"].ToString());
                    gridView2.SetRowCellValue(rowHandle, gridView2.Columns["الخزينة"], dr["الخزينة"].ToString());
                }
            }
            dr.Close();*/

            gridView2.Columns["Customer_ID"].Visible = false;
            gridView2.Columns["Client_ID"].Visible = false;
            gridView2.Columns["Bank_ID"].Visible = false;
            //gridView2.Columns["Branch_ID"].Visible = false;
            if (gridView2.IsLastVisibleRow)
            {
                gridView2.FocusedRowHandle = gridView2.RowCount - 1;
            }
            gridView2.Columns[0].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            if (!loaded)
            {
                for (int i = 1; i < gridView2.Columns.Count; i++)
                {
                    gridView2.Columns[i].Width = 100;
                }
            }

            txtSale.Text = totalSale.ToString();
            txtReturn.Text = totalReturn.ToString();
            txtFinal.Text = (totalSale - totalReturn).ToString();
            loaded = true;
        }

        private void btnNewChosen_Click(object sender, EventArgs e)
        {
            try
            {
                clearCom();
                while (gridView1.RowCount != 0)
                {
                    gridView1.SelectAll();
                    gridView1.DeleteSelectedRows();
                }
                gridControl2.DataSource = null;
                txtSale.Text = "0";
                txtReturn.Text = "0";
                txtFinal.Text = "0";
                txtTotalBills.Text = "0";
                txtTotalReturn.Text = "0";
                txtSafy.Text = "0";
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

        private void comBranch_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (loadedBranch)
                {
                    int branchID = 0;
                    if (int.TryParse(comBranch.SelectedValue.ToString(), out branchID))
                    {
                        txtBranchID.Text = comBranch.SelectedValue.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnBillReport_Click(object sender, EventArgs e)
        {
            try
            {
                if (comBranch.Text != "" && txtBranchID.Text != "")
                {
                    /*List<Transition_Items> bi = new List<Transition_Items>();
                    //DataTable dt = (DataTable)gridControl2.DataSource;
                    for (int i = 0; i < gridView2.RowCount; i++)
                    {
                        Transition_Items item = new Transition_Items() { ID = Convert.ToInt32(gridView2.GetRowCellDisplayText(i, gridView2.Columns["التسلسل"])), Operation_Type = gridView2.GetRowCellDisplayText(i, gridView2.Columns["عملية"]), Type = gridView2.GetRowCellDisplayText(i, gridView2.Columns["النوع"]), Bill_Number = gridView2.GetRowCellDisplayText(i, gridView2.Columns["الفاتورة"]), Client = gridView2.GetRowCellDisplayText(i, gridView2.Columns["العميل"]), Date = Convert.ToDateTime(gridView2.GetRowCellDisplayText(i, gridView2.Columns["التاريخ"])).ToString("yyyy-MM-dd"), CostSale = costSale, CostReturn = costReturn, Description = gridView2.GetRowCellDisplayText(i, gridView2.Columns["البيان"]) };
                        bi.Add(item);
                    }*/

                    gridcontrol = gridControl1;
                    BillsTransitions_Print f = new BillsTransitions_Print();
                    //Print_Transition_Report f = new Print_Transition_Report();
                    //f.PrintInvoice(dateTimePicker1.Value.Date, dateTimePicker2.Value.Date, comBranch.Text, bi);
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
    }
}

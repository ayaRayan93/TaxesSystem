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
    public partial class BillsAglTransitions_Report : Form
    {
        MySqlConnection conn;
        MainForm bankMainForm = null;
        
        public static XtraTabPage MainTabPagePrintingTransitions;
        Panel panelPrintingTransitions;

        public static BillsTransitions_Print bankPrint;

        public static GridControl gridcontrol;
        bool loaded = false;
        bool loadedBranch = false;

        public BillsAglTransitions_Report(MainForm BankMainForm)
        {
            InitializeComponent();
            conn = new MySqlConnection(connection.connectionString);
            bankMainForm = BankMainForm;
            
            MainTabPagePrintingTransitions = new XtraTabPage();
            panelPrintingTransitions = new Panel();
            
            gridcontrol = gridControl1;
            
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

        private void btnReport_Click(object sender, EventArgs e)
        {
            try
            {
                double costSale = 0;
                double costReturn = 0;
                List<Transition_Items> bi = new List<Transition_Items>();
                for (int i = 0; i < gridView2.RowCount; i++)
                {
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

                Print_AglTransition_Report f = new Print_AglTransition_Report();
                f.PrintInvoice(dateTimePicker1.Value.Date, dateTimePicker2.Value.Date, bi);
                f.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //functions
        public void search()
        {
            conn.Open();
            double totalBill = 0;
            double totalReturned = 0;
            
            while (gridView1.RowCount != 0)
            {
                gridView1.SelectAll();
                gridView1.DeleteSelectedRows();
            }

            string query = "SELECT customer_bill.CustomerBill_ID as 'ID',customer_bill.Branch_BillNumber as 'الفاتورة',customer_bill.Type_Buy as 'نوع الفاتورة',customer_bill.Bill_Date as 'التاريخ',customer_bill.Customer_ID,concat(customer1.Customer_Name,' ',customer_bill.Customer_ID) as 'المهندس/المقاول/التاجر',customer_bill.Client_ID,concat(customer2.Customer_Name,' ',customer_bill.Client_ID) as 'العميل',customer_bill.Total_CostBD as 'الاجمالى',customer_bill.Total_Discount as 'الخصم',customer_bill.Total_CostAD as 'الصافى' FROM customer_bill left join customer as customer1 on customer1.Customer_ID=customer_bill.Customer_ID left join customer as customer2 on customer2.Customer_ID=customer_bill.Client_ID where customer_bill.Type_Buy='آجل' and date(customer_bill.Bill_Date) between '" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "' and '" + dateTimePicker2.Value.ToString("yyyy-MM-dd") + "'";
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

            query = "SELECT customer_return_bill.CustomerReturnBill_ID as 'ID',customer_return_bill.Branch_BillNumber as 'الفاتورة',customer_return_bill.Type_Buy as 'نوع الفاتورة',customer_return_bill.Date as 'التاريخ',customer_return_bill.Customer_ID,concat(customer1.Customer_Name,' ',customer_return_bill.Customer_ID) as 'المهندس/المقاول/التاجر',customer_return_bill.Client_ID,concat(customer2.Customer_Name,' ',customer_return_bill.Client_ID) as 'العميل',customer_return_bill.TotalCostAD as 'الصافى' FROM customer_return_bill left join customer as customer1 on customer1.Customer_ID=customer_return_bill.Customer_ID left join customer as customer2 on customer2.Customer_ID=customer_return_bill.Client_ID where customer_return_bill.Type_Buy='آجل' and date(customer_return_bill.Date) between '" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "' and '" + dateTimePicker2.Value.ToString("yyyy-MM-dd") + "'";
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
            
            txtTotalBills.Text = totalBill.ToString();
            txtTotalReturn.Text = totalReturned.ToString();
            txtSafy.Text = (totalBill - totalReturned).ToString();
            /////////////////////////////////////////
            double totalSale = 0;
            double totalReturn = 0;
            //,transitions.Branch_Name as 'الفرع',transitions.Transition as 'عملية'
            MySqlDataAdapter adapter = new MySqlDataAdapter("SELECT transitions.Transition_ID as 'التسلسل',transitions.Type as 'النوع',transitions.Bill_Number as 'الفاتورة',transitions.Date as 'التاريخ',concat(customer2.Customer_Name,' ',transitions.Client_ID) as 'العميل',transitions.Amount as 'دائن',transitions.Amount as 'مدين',transitions.Data as 'البيان',bank.Bank_Name as 'الخزينة',transitions.Payment_Method as 'طريقة الدفع',transitions.Payday as 'تاريخ الاستحقاق',transitions.Check_Number as 'رقم الشيك/الكارت',transitions.Visa_Type as 'نوع الكارت',transitions.Operation_Number as 'رقم العملية',transitions.Bank_ID,transitions.Customer_ID,transitions.Client_ID FROM transitions INNER JOIN branch ON branch.Branch_ID = transitions.Branch_ID INNER JOIN bank ON bank.Bank_ID = transitions.Bank_ID left join customer as customer1 on customer1.Customer_ID=transitions.Customer_ID left join customer as customer2 on customer2.Customer_ID=transitions.Client_ID where Transition_ID=0 and transitions.Error=0 and transitions.Type='آجل' and date(transitions.Date) between '" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "' and '" + dateTimePicker2.Value.ToString("yyyy-MM-dd") + "' order by transitions.Date", conn);
            DataSet sourceDataSet = new DataSet();
            adapter.Fill(sourceDataSet);
            gridControl2.DataSource = sourceDataSet.Tables[0];

            query = "SELECT transitions.Transition_ID as 'التسلسل',transitions.Transition as 'عملية',transitions.Type as 'النوع',transitions.Bill_Number as 'الفاتورة',transitions.Branch_Name as 'الفرع',transitions.Date as 'التاريخ',concat(customer1.Customer_Name,' ',transitions.Customer_ID) as 'المهندس/المقاول/التاجر',concat(customer2.Customer_Name,' ',transitions.Client_ID) as 'العميل',transitions.Amount as 'المبلغ',transitions.Data as 'البيان',bank.Bank_Name as 'الخزينة',transitions.Payment_Method as 'طريقة الدفع',transitions.Payday as 'تاريخ الاستحقاق',transitions.Check_Number as 'رقم الشيك/الكارت',transitions.Visa_Type as 'نوع الكارت',transitions.Operation_Number as 'رقم العملية',transitions.Bank_ID,transitions.Customer_ID,transitions.Client_ID FROM transitions INNER JOIN bank ON bank.Bank_ID = transitions.Bank_ID left join customer as customer1 on customer1.Customer_ID=transitions.Customer_ID left join customer as customer2 on customer2.Customer_ID=transitions.Client_ID where transitions.Error=0 and transitions.Type='آجل' and date(transitions.Date) between '" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "' and '" + dateTimePicker2.Value.ToString("yyyy-MM-dd") + "' order by transitions.Date";
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
            
            gridView2.Columns["Customer_ID"].Visible = false;
            gridView2.Columns["Client_ID"].Visible = false;
            gridView2.Columns["Bank_ID"].Visible = false;
            gridView2.Columns["النوع"].Visible = false;
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
        
        private void btnBillReport_Click(object sender, EventArgs e)
        {
            try
            {
                gridcontrol = gridControl1;
                BillsAglTransitions_Print f = new BillsAglTransitions_Print();
                f.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}

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
    public partial class Bills_Delegate_Report : Form
    {
        MySqlConnection conn;

        public static XtraTabPage MainTabPagePrintingTransitions;
        Panel panelPrintingTransitions;
        
        public static GridControl gridcontrol;
        bool loaded = false;

        public Bills_Delegate_Report()
        {
            InitializeComponent();
            conn = new MySqlConnection(connection.connectionString);

            MainTabPagePrintingTransitions = new XtraTabPage();
            panelPrintingTransitions = new Panel();
            
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
        
        public void search()
        {
            conn.Open();
            double totalBill = 0;
            double totalReturned = 0;
            MySqlDataAdapter adapterSale = new MySqlDataAdapter("SELECT customer_bill.CustomerBill_ID as 'ID','النوع',customer_bill.Branch_BillNumber as 'الفاتورة',customer_bill.Type_Buy as 'نوع الفاتورة',customer_bill.Bill_Date as 'التاريخ',customer_bill.Customer_ID,concat(customer1.Customer_Name,' ',customer_bill.Customer_ID) as 'المهندس/المقاول/التاجر',customer_bill.Client_ID,concat(customer2.Customer_Name,' ',customer_bill.Client_ID) as 'العميل',customer_bill.Total_CostBD as 'الاجمالى',customer_bill.Total_Discount as 'الخصم',customer_bill.Total_CostAD as 'الصافى' FROM customer_bill INNER JOIN product_bill ON product_bill.CustomerBill_ID = customer_bill.CustomerBill_ID left join customer as customer1 on customer1.Customer_ID=customer_bill.Customer_ID left join customer as customer2 on customer2.Customer_ID=customer_bill.Client_ID where product_bill.Delegate_ID=" + UserControl.EmpID + " and customer_bill.Bill_Date between '" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "' and '" + dateTimePicker2.Value.ToString("yyyy-MM-dd HH:mm:ss") + "'", conn);
            DataTable saledt = new DataTable();
            adapterSale.Fill(saledt);

            MySqlDataAdapter adapterReturn = new MySqlDataAdapter("SELECT customer_return_bill.CustomerReturnBill_ID as 'ID','النوع',customer_return_bill.Branch_BillNumber as 'الفاتورة',customer_return_bill.Type_Buy as 'نوع الفاتورة',customer_return_bill.Date as 'التاريخ',customer_return_bill.Customer_ID,concat(customer1.Customer_Name,' ',customer_return_bill.Customer_ID) as 'المهندس/المقاول/التاجر',customer_return_bill.Client_ID,concat(customer2.Customer_Name,' ',customer_return_bill.Client_ID) as 'العميل',customer_return_bill.TotalCostAD as 'الصافى' FROM customer_return_bill INNER JOIN customer_return_bill_details ON customer_return_bill_details.CustomerReturnBill_ID = customer_return_bill.CustomerReturnBill_ID left join customer as customer1 on customer1.Customer_ID=customer_return_bill.Customer_ID left join customer as customer2 on customer2.Customer_ID=customer_return_bill.Client_ID where customer_return_bill_details.Delegate_ID=" + UserControl.EmpID + " and customer_return_bill.Date between '" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "' and '" + dateTimePicker2.Value.ToString("yyyy-MM-dd HH:mm:ss") + "'", conn);
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
                    totalBill += Convert.ToDouble(gridView1.GetRowCellDisplayText(i, "الصافى").ToString());
                }
                else
                {
                    gridView1.SetRowCellValue(i, "النوع", "مرتجع");
                    totalReturned += Convert.ToDouble(gridView1.GetRowCellDisplayText(i, "الصافى").ToString());
                }
            }

            if (!loaded)
            {
                for (int i = 1; i < gridView1.Columns.Count; i++)
                {
                    gridView1.Columns[i].Width = 110;
                }
            }

            txtTotalBills.Text = totalBill.ToString();
            txtTotalReturn.Text = totalReturned.ToString();
            txtSafy.Text = (totalBill - totalReturned).ToString();
            /////////////////////////////////////////
            double totalSale = 0;
            double totalReturn = 0;
            //,transitions.Branch_Name as 'الفرع'
            MySqlDataAdapter adapter = new MySqlDataAdapter("SELECT transitions.Transition_ID as 'التسلسل',transitions.Transition as 'عملية',transitions.Type as 'النوع',transitions.Bill_Number as 'الفاتورة',transitions.Date as 'التاريخ',concat(customer2.Customer_Name,' ',transitions.Client_ID) as 'العميل',transitions.Amount as 'دائن',transitions.Amount as 'مدين',transitions.Data as 'البيان',bank.Bank_Name as 'الخزينة',transitions.Payment_Method as 'طريقة الدفع',transitions.Payday as 'تاريخ الاستحقاق',transitions.Check_Number as 'رقم الشيك',transitions.Operation_Number as 'رقم العملية',transitions.Bank_ID,transitions.Customer_ID,transitions.Client_ID FROM transitions INNER JOIN branch ON branch.Branch_ID = transitions.Branch_ID INNER JOIN bank ON bank.Bank_ID = transitions.Bank_ID left join customer as customer1 on customer1.Customer_ID=transitions.Customer_ID left join customer as customer2 on customer2.Customer_ID=transitions.Client_ID where Transition_ID=0 and transitions.Error=0 and transitions.Date between '" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "' and '" + dateTimePicker2.Value.ToString("yyyy-MM-dd HH:mm:ss") + "' order by transitions.Date", conn);
            DataSet sourceDataSet = new DataSet();
            adapter.Fill(sourceDataSet);
            gridControl2.DataSource = sourceDataSet.Tables[0];

            #region error
            /*string query = "SELECT transitions.Transition_ID as 'التسلسل',transitions.Transition as 'عملية',transitions.Type as 'النوع',transitions.Bill_Number as 'الفاتورة',transitions.Branch_Name as 'الفرع',transitions.Date as 'التاريخ',concat(customer1.Customer_Name,' ',transitions.Customer_ID) as 'المهندس/المقاول/التاجر',concat(customer2.Customer_Name,' ',transitions.Client_ID) as 'العميل',transitions.Amount as 'المبلغ',transitions.Data as 'البيان',bank.Bank_Name as 'الخزينة',transitions.Payment_Method as 'طريقة الدفع',transitions.Payday as 'تاريخ الاستحقاق',transitions.Check_Number as 'رقم الشيك',transitions.Operation_Number as 'رقم العملية',transitions.Bank_ID,transitions.Customer_ID,transitions.Client_ID FROM transitions INNER JOIN bank ON bank.Bank_ID = transitions.Bank_ID left join customer as customer1 on customer1.Customer_ID=transitions.Customer_ID left join customer as customer2 on customer2.Customer_ID=transitions.Client_ID where transitions.TransitionBranch_ID=" + comBranch.SelectedValue.ToString() + " and transitions.Error=0 and transitions.Date between '" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "' and '" + dateTimePicker2.Value.ToString("yyyy-MM-dd HH:mm:ss") + "' order by transitions.Date";
                MySqlCommand comand = new MySqlCommand(query, conn);
                MySqlDataReader dr = comand.ExecuteReader();
                while (dr.Read())
                {
                    gridView2.AddNewRow();
                    int rowHandle = gridView2.GetRowHandle(gridView2.DataRowCount);
                    if (gridView2.IsNewItemRow(rowHandle))
                    {
                        gridView2.SetRowCellValue(rowHandle, gridView2.Columns["التسلسل"], dr["التسلسل"].ToString());
                        gridView2.SetRowCellValue(rowHandle, gridView2.Columns["عملية"], dr["عملية"].ToString());
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
                        gridView2.SetRowCellValue(rowHandle, gridView2.Columns["رقم الشيك"], dr["رقم الشيك"].ToString());
                        gridView2.SetRowCellValue(rowHandle, gridView2.Columns["رقم العملية"], dr["رقم العملية"].ToString());
                        gridView2.SetRowCellValue(rowHandle, gridView2.Columns["Bank_ID"], dr["Bank_ID"].ToString());
                        //gridView2.SetRowCellValue(rowHandle, gridView2.Columns["Branch_ID"], dr["Branch_ID"].ToString());
                        gridView2.SetRowCellValue(rowHandle, gridView2.Columns["Customer_ID"], dr["Customer_ID"].ToString());
                        gridView2.SetRowCellValue(rowHandle, gridView2.Columns["Client_ID"], dr["Client_ID"].ToString());
                    }
                }
                dr.Close();*/

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
            #endregion

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
                gridControl1.DataSource = null;
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
    }
}

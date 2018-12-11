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
    public partial class CopyBills_Transitions_Report : Form
    {
        MySqlConnection conn;
        MainForm bankMainForm = null;
        XtraTabControl MainTabControlBank;
        
        public static XtraTabPage MainTabPagePrintingTransitions;
        Panel panelPrintingTransitions;

        public static BillsTransitions_Print bankPrint;

        public static GridControl gridcontrol;
        bool loaded = false;

        public CopyBills_Transitions_Report(MainForm BankMainForm)
        {
            InitializeComponent();
            conn = new MySqlConnection(connection.connectionString);
            bankMainForm = BankMainForm;
            MainTabControlBank = MainForm.tabControlBank;
            
            MainTabPagePrintingTransitions = new XtraTabPage();
            panelPrintingTransitions = new Panel();
            
            gridcontrol = gridControl2;
            

            this.dateTimePicker1.Format = DateTimePickerFormat.Short;
            this.dateTimePicker2.Format = DateTimePickerFormat.Short;
        }

        private void Bills_Transitions_Report_Load(object sender, EventArgs e)
        {
            
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtTransitionID.Text != "")
                {
                    search();
                }
                else
                {
                    MessageBox.Show("يجب ادخال البيانات");
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
                if (txtTransitionID.Text != "")
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
                            bilNum = Convert.ToInt16(gridView2.GetRowCellDisplayText(i, gridView2.Columns["الفاتورة"]));
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
                        Transition_Items item = new Transition_Items() { ID = Convert.ToInt16(gridView2.GetRowCellDisplayText(i, gridView2.Columns["التسلسل"])), Operation_Type = gridView2.GetRowCellDisplayText(i, gridView2.Columns["عملية"]), Type = gridView2.GetRowCellDisplayText(i, gridView2.Columns["النوع"]), Bill_Number = gridView2.GetRowCellDisplayText(i, gridView2.Columns["الفاتورة"])/*, Branch_Name = gridView2.GetRowCellDisplayText(i, gridView2.Columns["الفرع"])*/, Client = gridView2.GetRowCellDisplayText(i, gridView2.Columns["العميل"]), Date = Convert.ToDateTime(gridView2.GetRowCellDisplayText(i, gridView2.Columns["التاريخ"])).ToString("yyyy-MM-dd"), CostSale = costSale, CostReturn = costReturn, Description = gridView2.GetRowCellDisplayText(i, gridView2.Columns["البيان"]) };
                        bi.Add(item);
                    }

                    Print_Transition_Report f = new Print_Transition_Report();
                    //f.PrintInvoice(dateTimePicker1.Value.Date, dateTimePicker2.Value.Date, bi);
                    f.ShowDialog();
                }
                else
                {
                    MessageBox.Show("يجب ادخال البيانات");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        
        public void search()
        {
            conn.Open();
            
            double totalSale = 0;
            double totalReturn = 0;
            MySqlDataAdapter adapter = new MySqlDataAdapter("SELECT transitions.Transition_ID as 'التسلسل',transitions.Transition as 'عملية',transitions.Type as 'النوع',transitions.Bill_Number as 'الفاتورة',transitions.Date as 'التاريخ',concat(customer2.Customer_Name,' ',transitions.Client_ID) as 'العميل',transitions.Amount as 'دائن',transitions.Amount as 'مدين',transitions.Data as 'البيان',bank.Bank_Name as 'الخزينة',transitions.Payment_Method as 'طريقة الدفع',transitions.Payday as 'تاريخ الاستحقاق',transitions.Check_Number as 'رقم الشيك/الكارت',transitions.Visa_Type as 'نوع الكارت',transitions.Operation_Number as 'رقم العملية',transitions.Bank_ID,transitions.Customer_ID,transitions.Client_ID FROM transitions INNER JOIN branch ON branch.Branch_ID = transitions.Branch_ID INNER JOIN bank ON bank.Bank_ID = transitions.Bank_ID left join customer as customer1 on customer1.Customer_ID=transitions.Customer_ID left join customer as customer2 on customer2.Customer_ID=transitions.Client_ID where Transition_ID=0 and transitions.Error=0 and transitions.Date between '" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "' and '" + dateTimePicker2.Value.ToString("yyyy-MM-dd HH:mm:ss") + "' order by transitions.Date", conn);
            DataSet sourceDataSet = new DataSet();
            adapter.Fill(sourceDataSet);
            gridControl2.DataSource = sourceDataSet.Tables[0];

            string query = "SELECT transitions.Transition_ID as 'التسلسل',transitions.Transition as 'عملية',transitions.Type as 'النوع',transitions.Bill_Number as 'الفاتورة',transitions.Date as 'التاريخ',concat(customer1.Customer_Name,' ',transitions.Customer_ID) as 'المهندس/المقاول/التاجر',concat(customer2.Customer_Name,' ',transitions.Client_ID) as 'العميل',transitions.Amount as 'المبلغ',transitions.Data as 'البيان',bank.Bank_Name as 'الخزينة',transitions.Payment_Method as 'طريقة الدفع',transitions.Payday as 'تاريخ الاستحقاق',transitions.Check_Number as 'رقم الشيك/الكارت',transitions.Visa_Type as 'نوع الكارت',transitions.Operation_Number as 'رقم العملية',transitions.Bank_ID,transitions.Customer_ID,transitions.Client_ID FROM transitions INNER JOIN bank ON bank.Bank_ID = transitions.Bank_ID left join customer as customer1 on customer1.Customer_ID=transitions.Customer_ID left join customer as customer2 on customer2.Customer_ID=transitions.Client_ID where transitions.Branch_ID=" + /*comBranch.SelectedValue.ToString() +*/ " and transitions.Error=0 and transitions.Date between '" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "' and '" + dateTimePicker2.Value.ToString("yyyy-MM-dd HH:mm:ss") + "' order by transitions.Date";
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
                    gridView2.SetRowCellValue(rowHandle, gridView2.Columns["تاريخ الاستحقاق"], dr["تاريخ الاستحقاق"].ToString());
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

            labelSale.Text = totalSale.ToString();
            labelReturn.Text = totalReturn.ToString();
            loaded = true;
        }

        private void btnNewChosen_Click(object sender, EventArgs e)
        {
            try
            {
                clearCom();
                gridControl2.DataSource = null;
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

        private void txtTransitionID_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    if (txtTransitionID.Text != "")
                    {
                        search();
                    }
                    else
                    {
                        MessageBox.Show("يجب ادخال البيانات");
                    }
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

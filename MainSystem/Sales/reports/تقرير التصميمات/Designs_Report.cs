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
    public partial class Designs_Report : Form
    {
        MySqlConnection conn;
        MainForm bankMainForm = null;
        
        public static XtraTabPage MainTabPagePrintingTransitions;
        Panel panelPrintingTransitions;

        public static BillsTransitions_Print bankPrint;

        public static GridControl gridcontrol;
        bool loaded = false;
        bool loadedBranch = false;

        public Designs_Report(MainForm BankMainForm)
        {
            InitializeComponent();
            conn = new MySqlConnection(connection.connectionString);
            bankMainForm = BankMainForm;
            
            MainTabPagePrintingTransitions = new XtraTabPage();
            panelPrintingTransitions = new Panel();
            
            comBranch.AutoCompleteMode = AutoCompleteMode.Suggest;
            comBranch.AutoCompleteSource = AutoCompleteSource.ListItems;

            //this.dateTimePicker1.Format = DateTimePickerFormat.Short;
            //this.dateTimePicker2.Format = DateTimePickerFormat.Short;
        }
        
        private void Bills_Transitions_Report_Load(object sender, EventArgs e)
        {
            try
            {
                if (!loadedBranch)
                {
                    loadBranch();

                    if(UserControl.userType != 1 && UserControl.userType != 13 && UserControl.userType != 7)
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
                        Transition_Items item = new Transition_Items() { ID = Convert.ToInt32(gridView2.GetRowCellDisplayText(i, gridView2.Columns["التسلسل"])), /*Operation_Type = gridView2.GetRowCellDisplayText(i, gridView2.Columns["عملية"]),*/ Type = gridView2.GetRowCellDisplayText(i, gridView2.Columns["النوع"]), Bill_Number = gridView2.GetRowCellDisplayText(i, gridView2.Columns["رقم الديزاين"])/*, Branch_Name = gridView2.GetRowCellDisplayText(i, gridView2.Columns["الفرع"])*/, Client = gridView2.GetRowCellDisplayText(i, gridView2.Columns["العميل"]), Date = Convert.ToDateTime(gridView2.GetRowCellDisplayText(i, gridView2.Columns["التاريخ"])).ToString("yyyy-MM-dd"), CostSale = costSale, CostReturn = costReturn, Description = gridView2.GetRowCellDisplayText(i, gridView2.Columns["البيان"]) };
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
            
            double totalSale = 0;
            double totalReturn = 0;

            MySqlDataAdapter adapter = new MySqlDataAdapter("SELECT transitions.Transition_ID as 'التسلسل',transitions.Type as 'النوع',transitions.Bill_Number as 'رقم الديزاين',transitions.Date as 'التاريخ',concat(customer2.Customer_Name,' ',transitions.Client_ID) as 'العميل',transitions.Amount as 'دائن',transitions.Amount as 'مدين',transitions.Delegate_Name as 'المندوب',transitions.Data as 'البيان',transitions.Bank_Name as 'الخزينة',transitions.Payment_Method as 'طريقة الدفع',transitions.Payday as 'تاريخ الاستحقاق',transitions.Check_Number as 'رقم الشيك',transitions.Operation_Number as 'رقم العملية' FROM transitions INNER JOIN branch ON branch.Branch_ID = transitions.Branch_ID left join customer as customer1 on customer1.Customer_ID=transitions.Customer_ID left join customer as customer2 on customer2.Customer_ID=transitions.Client_ID where Transition_ID=0 and transitions.Error=0 and date(transitions.Date) between '" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "' and '" + dateTimePicker2.Value.ToString("yyyy-MM-dd") + "' order by transitions.Date", conn);
            DataSet sourceDataSet = new DataSet();
            adapter.Fill(sourceDataSet);
            gridControl2.DataSource = sourceDataSet.Tables[0];

            string query = "SELECT transitions.Transition_ID as 'التسلسل',transitions.Transition as 'عملية',transitions.Type as 'النوع',transitions.Bill_Number as 'رقم الديزاين',transitions.Branch_Name as 'الفرع',transitions.Date as 'التاريخ',concat(customer1.Customer_Name,' ',transitions.Customer_ID) as 'المهندس/المقاول/التاجر',concat(customer2.Customer_Name,' ',transitions.Client_ID) as 'العميل',transitions.Amount as 'المبلغ',transitions.Delegate_Name as 'المندوب',transitions.Data as 'البيان',transitions.Bank_Name as 'الخزينة',transitions.Payment_Method as 'طريقة الدفع',transitions.Payday as 'تاريخ الاستحقاق',transitions.Check_Number as 'رقم الشيك',transitions.Operation_Number as 'رقم العملية',transitions.Bank_ID,transitions.Customer_ID,transitions.Client_ID FROM transitions left join customer as customer1 on customer1.Customer_ID=transitions.Customer_ID left join customer as customer2 on customer2.Customer_ID=transitions.Client_ID where transitions.TransitionBranch_ID=" + comBranch.SelectedValue.ToString() + " and transitions.Error=0 and date(transitions.Date) between '" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "' and '" + dateTimePicker2.Value.ToString("yyyy-MM-dd") + "' and (transitions.Transition='ايداع تصميم' or transitions.Transition='سحب تصميم') order by transitions.Date";
            MySqlCommand comand = new MySqlCommand(query, conn);
            MySqlDataReader dr = comand.ExecuteReader();
            while (dr.Read())
            {
                gridView2.AddNewRow();
                int rowHandle = gridView2.GetRowHandle(gridView2.DataRowCount);
                if (gridView2.IsNewItemRow(rowHandle))
                {
                    gridView2.SetRowCellValue(rowHandle, gridView2.Columns["التسلسل"], dr["التسلسل"].ToString());
                    gridView2.SetRowCellValue(rowHandle, gridView2.Columns["النوع"], dr["النوع"].ToString());
                    gridView2.SetRowCellValue(rowHandle, gridView2.Columns["رقم الديزاين"], dr["رقم الديزاين"].ToString());
                    gridView2.SetRowCellValue(rowHandle, gridView2.Columns["التاريخ"], dr["التاريخ"].ToString());
                    if (dr["العميل"].ToString() != "")
                    {
                        gridView2.SetRowCellValue(rowHandle, gridView2.Columns["العميل"], dr["العميل"].ToString());
                    }
                    else
                    {
                        gridView2.SetRowCellValue(rowHandle, gridView2.Columns["العميل"], dr["المهندس/المقاول/التاجر"].ToString());
                    }
                    if (dr["عملية"].ToString() == "ايداع تصميم")
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
                }
            }
            dr.Close();
            
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
                
                gridControl2.DataSource = null;
                txtSale.Text = "0";
                txtReturn.Text = "0";
                txtFinal.Text = "0";
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
    }
}

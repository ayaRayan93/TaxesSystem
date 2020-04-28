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
    public partial class BillPayType_Report : Form
    {
        MySqlConnection conn;
        MainForm bankMainForm = null;
        
        public static XtraTabPage MainTabPagePrintingTransitions;
        Panel panelPrintingTransitions;

        public static BillsTransitions_Print bankPrint;

        bool loaded = false;
        bool loadedBranch = false;

        public BillPayType_Report(MainForm BankMainForm)
        {
            InitializeComponent();
            conn = new MySqlConnection(connection.connectionString);
            bankMainForm = BankMainForm;

            MainTabPagePrintingTransitions = new XtraTabPage();
            panelPrintingTransitions = new Panel();
            
            comBranch.AutoCompleteMode = AutoCompleteMode.Suggest;
            comBranch.AutoCompleteSource = AutoCompleteSource.ListItems;
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
                if (comPaymentMethod.Text != "" && comPaymentMethod.SelectedIndex != -1 && comBranch.Text != "" && txtBranchID.Text != "")
                {
                    search();
                }
                else
                {
                    MessageBox.Show("يجب اختيار الفرع وطريقة الدفع");
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

                    Print_Transition_Report f = new Print_Transition_Report();
                    f.PrintInvoice(dateTimePicker1.Value.Date, dateTimePicker2.Value.Date, comBranch.Text + " - " + comPaymentMethod.Text, bi);
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

            MySqlDataAdapter adapter = new MySqlDataAdapter("SELECT transitions.Transition_ID as 'التسلسل',transitions.Type as 'النوع',transitions.Bill_Number as 'الفاتورة',transitions.Date as 'التاريخ',concat(customer2.Customer_Name,' ',transitions.Client_ID) as 'العميل',transitions.Amount as 'دائن',transitions.Amount as 'مدين',transitions.Data as 'البيان',transitions.Bank_Name as 'الخزينة',transitions.Payday as 'تاريخ الاستحقاق',transitions.Check_Number as 'رقم الشيك',transitions.Operation_Number as 'رقم العملية',transitions.Bank_ID,transitions.Customer_ID,transitions.Client_ID FROM transitions INNER JOIN branch ON branch.Branch_ID = transitions.Branch_ID left join customer as customer1 on customer1.Customer_ID=transitions.Customer_ID left join customer as customer2 on customer2.Customer_ID=transitions.Client_ID where Transition_ID=0 and transitions.Error=0 and date(transitions.Date) between '" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "' and '" + dateTimePicker2.Value.ToString("yyyy-MM-dd") + "' order by transitions.Date", conn);
            DataSet sourceDataSet = new DataSet();
            adapter.Fill(sourceDataSet);
            gridControl2.DataSource = sourceDataSet.Tables[0];

            string query = "SELECT transitions.Transition_ID as 'التسلسل',transitions.Transition as 'عملية',transitions.Type as 'النوع',transitions.Bill_Number as 'الفاتورة',transitions.Branch_Name as 'الفرع',transitions.Date as 'التاريخ',concat(customer1.Customer_Name,' ',transitions.Customer_ID) as 'المهندس/المقاول/التاجر',concat(customer2.Customer_Name,' ',transitions.Client_ID) as 'العميل',transitions.Amount as 'المبلغ',transitions.Data as 'البيان',transitions.Bank_Name as 'الخزينة',transitions.Payday as 'تاريخ الاستحقاق',transitions.Check_Number as 'رقم الشيك',transitions.Operation_Number as 'رقم العملية',transitions.Bank_ID,transitions.Customer_ID,transitions.Client_ID FROM transitions left join customer as customer1 on customer1.Customer_ID=transitions.Customer_ID left join customer as customer2 on customer2.Customer_ID=transitions.Client_ID where transitions.TransitionBranch_ID=" + comBranch.SelectedValue.ToString() + " and transitions.Error=0 and date(transitions.Date) between '" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "' and '" + dateTimePicker2.Value.ToString("yyyy-MM-dd") + "' and transitions.Payment_Method='" + comPaymentMethod.Text + "' order by transitions.Date";
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
                    gridView2.SetRowCellValue(rowHandle, gridView2.Columns["الفاتورة"], dr["الفاتورة"].ToString());
                    gridView2.SetRowCellValue(rowHandle, gridView2.Columns["التاريخ"], dr["التاريخ"].ToString());
                    if (dr["العميل"].ToString() != "")
                    {
                        gridView2.SetRowCellValue(rowHandle, gridView2.Columns["العميل"], dr["العميل"].ToString());
                    }
                    else
                    {
                        gridView2.SetRowCellValue(rowHandle, gridView2.Columns["العميل"], dr["المهندس/المقاول/التاجر"].ToString());
                    }
                    if (dr["عملية"].ToString() == "ايداع" || dr["عملية"].ToString() == "ايداع تصميم")
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
                    gridView2.SetRowCellValue(rowHandle, gridView2.Columns["Customer_ID"], dr["Customer_ID"].ToString());
                    gridView2.SetRowCellValue(rowHandle, gridView2.Columns["Client_ID"], dr["Client_ID"].ToString());
                }
            }
            dr.Close();
            
            gridView2.Columns["Customer_ID"].Visible = false;
            gridView2.Columns["Client_ID"].Visible = false;
            gridView2.Columns["Bank_ID"].Visible = false;
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

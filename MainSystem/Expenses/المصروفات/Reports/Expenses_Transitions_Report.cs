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
    public partial class Expenses_Transitions_Report : Form
    {
        MySqlConnection conn;
        XtraTabControl xtraTabControlExpenses;
        
        bool loaded = false;

        public Expenses_Transitions_Report(XtraTabControl XtraTabControlExpenses)
        {
            InitializeComponent();
            conn = new MySqlConnection(connection.connectionString);
            xtraTabControlExpenses = XtraTabControlExpenses;
        }
        
        private void Bills_Transitions_Report_Load(object sender, EventArgs e)
        {
            string query = "";
            if (UserControl.userType == 1)
            {
                query = "select * from bank where Bank_Type='خزينة'";
            }
            else
            {
                query = "select * from bank where Branch_ID=" + UserControl.EmpBranchID + " and Bank_Type='خزينة'";
            }
            MySqlDataAdapter da = new MySqlDataAdapter(query, conn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            comSafe.DataSource = dt;
            comSafe.DisplayMember = dt.Columns["Bank_Name"].ToString();
            comSafe.ValueMember = dt.Columns["Bank_ID"].ToString();
            comSafe.SelectedIndex = -1;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (comSafe.SelectedValue != null)
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
            else
            {
                MessageBox.Show("يجب اختيار خزينة");
            }
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            if (comSafe.SelectedValue != null && gridView1.RowCount > 0)
            {
                try
                {
                    double costExpense = 0;
                    List<ExpensesTransition_Items> bi = new List<ExpensesTransition_Items>();

                    for (int i = 0; i < gridView1.RowCount; i++)
                    {
                        if (gridView1.GetRowCellDisplayText(i, gridView1.Columns["المبلغ"]) != "")
                        {
                            costExpense = Convert.ToDouble(gridView1.GetRowCellDisplayText(i, gridView1.Columns["المبلغ"]));
                        }
                        else
                        {
                            costExpense = 0;
                        }
                        ExpensesTransition_Items item = new ExpensesTransition_Items() { ID = Convert.ToInt32(gridView1.GetRowCellDisplayText(i, gridView1.Columns["التسلسل"])), MainExpense_Name = gridView1.GetRowCellDisplayText(i, gridView1.Columns["المصروف الرئيسى"]), SubExpense_Name = gridView1.GetRowCellDisplayText(i, gridView1.Columns["المصروف الفرعى"]), DepositorName = gridView1.GetRowCellDisplayText(i, gridView1.Columns["المستلم"]), Date = Convert.ToDateTime(gridView1.GetRowCellDisplayText(i, gridView1.Columns["التاريخ"])).ToString("yyyy-MM-dd"), ExpenseAmount = costExpense, Employee_Name = gridView1.GetRowCellDisplayText(i, gridView1.Columns["الموظف"]), Description = gridView1.GetRowCellDisplayText(i, gridView1.Columns["البيان"]) };
                        bi.Add(item);
                    }

                    Print_ExpensesTransition_Report f = new Print_ExpensesTransition_Report();
                    f.PrintInvoice(dateTimePicker1.Value.Date, dateTimePicker2.Value.Date, comSafe.Text, bi);
                    f.ShowDialog();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                MessageBox.Show("يجب التاكد من البيانات");
            }
        }

        //functions
        public void search()
        {
            conn.Open();
            
            double totalReturn = 0;

            MySqlDataAdapter adapter = new MySqlDataAdapter("SELECT expense_transition.ExpenseTransition_ID as 'التسلسل',expense_transition.Date as 'التاريخ',expense_transition.Depositor_Name as 'المستلم',expense_main.MainExpense_Name as 'المصروف الرئيسى',expense_sub.SubExpense_Name as 'المصروف الفرعى',expense_transition.Amount as 'المبلغ',employee.Employee_Name as 'الموظف',expense_transition.Description as 'البيان' FROM expense_transition left JOIN expense_sub ON expense_sub.SubExpense_ID = expense_transition.SubExpense_ID left JOIN expense_main ON expense_main.MainExpense_ID = expense_sub.MainExpense_ID INNER JOIN branch ON branch.Branch_ID = expense_transition.Branch_ID INNER JOIN bank ON bank.Bank_ID = expense_transition.Bank_ID INNER JOIN employee ON expense_transition.Employee_ID = employee.Employee_ID where ExpenseTransition_ID=0", conn);
            DataSet sourceDataSet = new DataSet();
            adapter.Fill(sourceDataSet);
            gridControl1.DataSource = sourceDataSet.Tables[0];

            string query = "SELECT expense_transition.ExpenseTransition_ID as 'التسلسل',expense_main.MainExpense_Name as 'المصروف الرئيسى',expense_sub.SubExpense_Name as 'المصروف الفرعى',expense_transition.Date as 'التاريخ',expense_transition.Depositor_Name as 'المستلم',expense_transition.Amount as 'المبلغ',expense_transition.Description as 'البيان',employee.Employee_Name as 'الموظف' FROM expense_transition left JOIN expense_sub ON expense_transition.SubExpense_ID=expense_sub.SubExpense_ID left JOIN expense_main ON expense_sub.MainExpense_ID=expense_main.MainExpense_ID INNER JOIN branch ON branch.Branch_ID = expense_transition.Branch_ID INNER JOIN bank ON bank.Bank_ID = expense_transition.Bank_ID INNER JOIN employee ON expense_transition.Employee_ID = employee.Employee_ID where expense_transition.Bank_ID=" + comSafe.SelectedValue.ToString() + " and expense_transition.Error=0 and date(expense_transition.Date) between '" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "' and '" + dateTimePicker2.Value.ToString("yyyy-MM-dd") + "' order by expense_transition.Date";
            MySqlCommand comand = new MySqlCommand(query, conn);
            MySqlDataReader dr = comand.ExecuteReader();
            while (dr.Read())
            {
                gridView1.AddNewRow();
                int rowHandle = gridView1.GetRowHandle(gridView1.DataRowCount);
                if (gridView1.IsNewItemRow(rowHandle))
                {
                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns["التسلسل"], dr["التسلسل"].ToString());
                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns["المصروف الرئيسى"], dr["المصروف الرئيسى"].ToString());
                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns["المصروف الفرعى"], dr["المصروف الفرعى"].ToString());
                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns["التاريخ"], dr["التاريخ"].ToString());
                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns["المستلم"], dr["المستلم"].ToString());
                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns["المبلغ"], dr["المبلغ"].ToString());
                    totalReturn += Convert.ToDouble(dr["المبلغ"].ToString());
                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns["البيان"], dr["البيان"].ToString());
                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns["الموظف"], dr["الموظف"].ToString());
                }
            }
            dr.Close();
            
            if (gridView1.IsLastVisibleRow)
            {
                gridView1.FocusedRowHandle = gridView1.RowCount - 1;
            }
            gridView1.Columns[0].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            if (!loaded)
            {
                for (int i = 1; i < gridView1.Columns.Count; i++)
                {
                    gridView1.Columns[i].Width = 100;
                }
            }
            
            txtReturn.Text = totalReturn.ToString();
            loaded = true;
        }

        private void btnNewChosen_Click(object sender, EventArgs e)
        {
            try
            {
                clearCom();
                
                gridControl1.DataSource = null;
                txtReturn.Text = "0";
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

        private void btnPrintCopy_Click(object sender, EventArgs e)
        {

        }
    }
}

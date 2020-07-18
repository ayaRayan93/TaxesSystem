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
    public partial class ExpensesNumTransitions_Report : Form
    {
        MySqlConnection conn;
        XtraTabControl xtraTabControlExpenses;
        DataRowView row1 = null;
        MainForm mainForm = null;

        public ExpensesNumTransitions_Report(XtraTabControl XtraTabControlExpenses, MainForm mainform)
        {
            InitializeComponent();
            conn = new MySqlConnection(connection.connectionString);
            xtraTabControlExpenses = XtraTabControlExpenses;
            mainForm = mainform;
        }
        
        private void Bills_Transitions_Report_Load(object sender, EventArgs e)
        {
        }
        
        //functions
        public void search()
        {
            conn.Open();
            
            MySqlDataAdapter adapter = new MySqlDataAdapter("SELECT expense_transition.ExpenseTransition_ID as 'التسلسل',expense_transition.Type as 'النوع',DATE_FORMAT(expense_transition.Date,'%d-%m-%Y') as 'التاريخ',expense_main.MainExpense_Name as 'المصروف الرئيسى',expense_sub.SubExpense_Name as 'المصروف الفرعى',bank.Bank_Name as 'الخزينة',expense_transition.Amount as 'وارد',expense_transition.Amount as 'مصروف',employee.Employee_Name as 'الموظف',expense_transition.Depositor_Name as 'المودع/المستلم',expense_transition.Description as 'البيان' FROM expense_transition left JOIN expense_sub ON expense_sub.SubExpense_ID = expense_transition.SubExpense_ID left JOIN expense_main ON expense_main.MainExpense_ID = expense_sub.MainExpense_ID INNER JOIN branch ON branch.Branch_ID = expense_transition.Branch_ID INNER JOIN bank ON bank.Bank_ID = expense_transition.Bank_ID INNER JOIN employee ON expense_transition.Employee_ID = employee.Employee_ID where ExpenseTransition_ID=0", conn);
            DataSet sourceDataSet = new DataSet();
            adapter.Fill(sourceDataSet);
            gridControl1.DataSource = sourceDataSet.Tables[0];

            string query = "SELECT expense_transition.ExpenseTransition_ID as 'التسلسل',expense_transition.Type as 'النوع',expense_main.MainExpense_Name as 'المصروف الرئيسى',expense_sub.SubExpense_Name as 'المصروف الفرعى',DATE_FORMAT(expense_transition.Date,'%d-%m-%Y') as 'التاريخ',expense_transition.Depositor_Name as 'المودع/المستلم',bank.Bank_Name as 'الخزينة',expense_transition.Amount as 'المبلغ',expense_transition.Description as 'البيان',employee.Employee_Name as 'الموظف' FROM expense_transition left JOIN expense_sub ON expense_transition.SubExpense_ID=expense_sub.SubExpense_ID left JOIN expense_main ON expense_sub.MainExpense_ID=expense_main.MainExpense_ID INNER JOIN branch ON branch.Branch_ID = expense_transition.Branch_ID INNER JOIN bank ON bank.Bank_ID = expense_transition.Bank_ID INNER JOIN employee ON expense_transition.Employee_ID = employee.Employee_ID where expense_transition.Error=0 and expense_transition.ExpenseTransition_ID=" + txtExpenseNum.Text + " order by expense_transition.Date";
            MySqlCommand comand = new MySqlCommand(query, conn);
            MySqlDataReader dr = comand.ExecuteReader();
            while (dr.Read())
            {
                gridView1.AddNewRow();
                int rowHandle = gridView1.GetRowHandle(gridView1.DataRowCount);
                if (gridView1.IsNewItemRow(rowHandle))
                {
                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns["التسلسل"], dr["التسلسل"].ToString());
                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns["النوع"], dr["النوع"].ToString());
                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns["المصروف الرئيسى"], dr["المصروف الرئيسى"].ToString());
                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns["المصروف الفرعى"], dr["المصروف الفرعى"].ToString());
                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns["التاريخ"], dr["التاريخ"].ToString());
                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns["المودع/المستلم"], dr["المودع/المستلم"].ToString());

                    if (dr["النوع"].ToString() == "ايداع")
                    {
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns["وارد"], dr["المبلغ"].ToString());
                    }
                    else
                    {
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns["مصروف"], dr["المبلغ"].ToString());
                    }

                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns["الخزينة"], dr["الخزينة"].ToString());
                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns["البيان"], dr["البيان"].ToString());
                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns["الموظف"], dr["الموظف"].ToString());
                }
            }
            dr.Close();
            
            if (gridView1.IsLastVisibleRow)
            {
                gridView1.FocusedRowHandle = gridView1.RowCount - 1;
            }

            row1 = (DataRowView)gridView1.GetRow(gridView1.RowCount - 1);

            gridView1.Columns[0].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            
            for (int i = 1; i < gridView1.Columns.Count; i++)
            {
                gridView1.Columns[i].Width = 100;
            }
            
        }

        private void btnNewChosen_Click(object sender, EventArgs e)
        {
            try
            {
                clearCom();
                
                gridControl1.DataSource = null;
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
            }
        }

        private void btnPrintCopy_Click(object sender, EventArgs e)
        {
            try
            {
                if (row1 != null)
                {
                    if (row1["النوع"].ToString() == "صرف")
                    {
                        Print_Expense_Report_Copy f = new Print_Expense_Report_Copy();
                        f.PrintInvoice(Convert.ToInt32(row1["التسلسل"].ToString()), row1["المصروف الرئيسى"].ToString(), row1["المصروف الفرعى"].ToString(), row1["الخزينة"].ToString(), row1["مصروف"].ToString(), row1["المودع/المستلم"].ToString(), row1["البيان"].ToString(), row1["التاريخ"].ToString());
                        f.ShowDialog();
                    }
                    else if (row1["النوع"].ToString() == "ايداع")
                    {
                        Print_IncomeExpense_Report_Copy f = new Print_IncomeExpense_Report_Copy();
                        f.PrintInvoice(Convert.ToInt32(row1["التسلسل"].ToString()), row1["الخزينة"].ToString(), row1["وارد"].ToString(), row1["المودع/المستلم"].ToString(), row1["البيان"].ToString(), row1["التاريخ"].ToString());
                        f.ShowDialog();
                    }
                }
                else
                {
                    MessageBox.Show("يجب ادخال البيانات كاملة");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            conn.Close();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (row1 != null)
                {
                    if (row1["النوع"].ToString() == "صرف")
                    {
                        mainForm.bindUpdateExpenseForm(row1);
                    }
                    else if (row1["النوع"].ToString() == "ايداع")
                    {
                        mainForm.bindUpdateIncomeExpenseForm(row1);
                    }
                }
                else
                {
                    MessageBox.Show("يجب ادخال البيانات كاملة");
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

﻿using DevExpress.XtraGrid;
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
    public partial class Expenses_Transitions_Report : Form
    {
        MySqlConnection conn;
        XtraTabControl xtraTabControlExpenses;
        DataRowView row1 = null;
        MainForm mainForm = null;

        public Expenses_Transitions_Report(XtraTabControl XtraTabControlExpenses, MainForm mainform)
        {
            InitializeComponent();
            conn = new MySqlConnection(connection.connectionString);
            xtraTabControlExpenses = XtraTabControlExpenses;
            mainForm = mainform;
        }
        
        private void Bills_Transitions_Report_Load(object sender, EventArgs e)
        {
            string query = "";
            if (UserControl.userType == 1 || UserControl.userType == 7)
            {
                query = "select * from bank inner join bank_main on bank.MainBank_ID=bank_main.MainBank_ID where MainBank_Type='خزينة' and (MainBank_Name = 'خزينة مبيعات' or MainBank_Name = 'خزينة حسابات')";
            }
            else
            {
                query = "select * from bank inner join bank_main on bank.MainBank_ID=bank_main.MainBank_ID INNER JOIN bank_employee ON bank_employee.Bank_ID = bank.Bank_ID where bank.Branch_ID=" + UserControl.EmpBranchID + " and bank_employee.Employee_ID=" + UserControl.EmpID + " and MainBank_Type='خزينة' and MainBank_Name = 'خزينة مبيعات'";
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
            if (UserControl.userType != 1 && UserControl.userType != 7)
            {
                if (comSafe.SelectedValue == null && comSafe.Text == "")
                {
                    MessageBox.Show("يجب اختيار خزينة");
                    return;
                }
                else
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
            }
            else
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
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            if (gridView1.RowCount > 0)
            {
                try
                {
                    double costIncome = 0;
                    double costExpense = 0;
                    List<ExpensesTransition_Items> bi = new List<ExpensesTransition_Items>();

                    for (int i = 0; i < gridView1.RowCount; i++)
                    {
                        if (gridView1.GetRowCellDisplayText(i, gridView1.Columns["وارد"]) != "")
                        {
                            costIncome = Convert.ToDouble(gridView1.GetRowCellDisplayText(i, gridView1.Columns["وارد"]));
                        }
                        else
                        {
                            costIncome = 0;
                        }
                        if (gridView1.GetRowCellDisplayText(i, gridView1.Columns["مصروف"]) != "")
                        {
                            costExpense = Convert.ToDouble(gridView1.GetRowCellDisplayText(i, gridView1.Columns["مصروف"]));
                        }
                        else
                        {
                            costExpense = 0;
                        }
                        ExpensesTransition_Items item = new ExpensesTransition_Items() { ID = Convert.ToInt32(gridView1.GetRowCellDisplayText(i, gridView1.Columns["التسلسل"])), MainExpense_Name = gridView1.GetRowCellDisplayText(i, gridView1.Columns["المصروف الرئيسى"]), Type = gridView1.GetRowCellDisplayText(i, gridView1.Columns["النوع"]), SubExpense_Name = gridView1.GetRowCellDisplayText(i, gridView1.Columns["المصروف الفرعى"]), DepositorName = gridView1.GetRowCellDisplayText(i, gridView1.Columns["المودع/المستلم"]), Date = Convert.ToDateTime(gridView1.GetRowCellDisplayText(i, gridView1.Columns["التاريخ"])).ToString("yyyy-MM-dd"), IncomeAmount = costIncome, ExpenseAmount = costExpense, Employee_Name = gridView1.GetRowCellDisplayText(i, gridView1.Columns["الموظف"]), Description = gridView1.GetRowCellDisplayText(i, gridView1.Columns["البيان"]) };
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

            double totalIncome = 0;
            double totalExpense = 0;
            string qSafe = "";

            MySqlDataAdapter adapter = new MySqlDataAdapter("SELECT expense_transition.ExpenseTransition_ID as 'التسلسل',expense_transition.Type as 'النوع',expense_transition.PullExpenseTransition_ID as 'رقم المصروف',DATE_FORMAT(expense_transition.Date,'%d-%m-%Y') as 'التاريخ',expense_main.MainExpense_Name as 'المصروف الرئيسى',expense_sub.SubExpense_Name as 'المصروف الفرعى',bank.Bank_Name as 'الخزينة',expense_transition.Amount as 'وارد',expense_transition.Amount as 'مصروف',employee.Employee_Name as 'الموظف',expense_transition.Depositor_Name as 'المودع/المستلم',expense_transition.Description as 'البيان' FROM expense_transition left JOIN expense_sub ON expense_sub.SubExpense_ID = expense_transition.SubExpense_ID left JOIN expense_main ON expense_main.MainExpense_ID = expense_sub.MainExpense_ID INNER JOIN branch ON branch.Branch_ID = expense_transition.Branch_ID INNER JOIN bank ON bank.Bank_ID = expense_transition.Bank_ID INNER JOIN employee ON expense_transition.Employee_ID = employee.Employee_ID where ExpenseTransition_ID=0", conn);
            DataSet sourceDataSet = new DataSet();
            adapter.Fill(sourceDataSet);
            gridControl1.DataSource = sourceDataSet.Tables[0];

            if (comSafe.SelectedValue == null && comSafe.Text == "")
            {
                //qSafe = "select Bank_ID from bank";
                if (UserControl.userType == 1 || UserControl.userType == 7)
                {
                    qSafe = "select bank.Bank_ID from bank inner join bank_main on bank.MainBank_ID=bank_main.MainBank_ID where MainBank_Type='خزينة'";
                }
                else
                {
                    qSafe = "select bank.Bank_ID from bank inner join bank_main on bank.MainBank_ID=bank_main.MainBank_ID INNER JOIN bank_employee ON bank_employee.Bank_ID = bank.Bank_ID where bank.Branch_ID=" + UserControl.EmpBranchID + " and bank_employee.Employee_ID=" + UserControl.EmpID + " and MainBank_Type='خزينة'";
                }
            }
            else
            {
                qSafe = comSafe.SelectedValue.ToString();
            }
            
            string query = "SELECT expense_transition.ExpenseTransition_ID as 'التسلسل',expense_transition.Type as 'النوع',expense_main.MainExpense_Name as 'المصروف الرئيسى',expense_sub.SubExpense_Name as 'المصروف الفرعى',DATE_FORMAT(expense_transition.Date,'%d-%m-%Y') as 'التاريخ',expense_transition.Depositor_Name as 'المودع/المستلم',bank.Bank_Name as 'الخزينة',expense_transition.Amount as 'المبلغ',expense_transition.PullExpenseTransition_ID as 'رقم المصروف',expense_transition.Description as 'البيان',employee.Employee_Name as 'الموظف' FROM expense_transition left JOIN expense_sub ON expense_transition.SubExpense_ID=expense_sub.SubExpense_ID left JOIN expense_main ON expense_sub.MainExpense_ID=expense_main.MainExpense_ID INNER JOIN branch ON branch.Branch_ID = expense_transition.Branch_ID INNER JOIN bank ON bank.Bank_ID = expense_transition.Bank_ID INNER JOIN employee ON expense_transition.Employee_ID = employee.Employee_ID where expense_transition.Bank_ID in(" + qSafe + ") and expense_transition.Error=0 and date(expense_transition.Date) between '" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "' and '" + dateTimePicker2.Value.ToString("yyyy-MM-dd") + "' order by expense_transition.Date";
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
                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns["رقم المصروف"], dr["رقم المصروف"].ToString());

                    if (dr["النوع"].ToString() == "صرف")
                    {
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns["مصروف"], dr["المبلغ"].ToString());
                        totalExpense += Convert.ToDouble(dr["المبلغ"].ToString());
                    }
                    else
                    {
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns["وارد"], dr["المبلغ"].ToString());
                        totalIncome += Convert.ToDouble(dr["المبلغ"].ToString());
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
            gridView1.Columns[0].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            
            for (int i = 1; i < gridView1.Columns.Count; i++)
            {
                gridView1.Columns[i].Width = 100;
            }

            txtIncome.Text = totalIncome.ToString();
            txtExpense.Text = totalExpense.ToString();
            txtSafy.Text = (totalIncome - totalExpense).ToString();
        }

        private void btnNewChosen_Click(object sender, EventArgs e)
        {
            try
            {
                clearCom();
                
                gridControl1.DataSource = null;
                txtIncome.Text = "0";
                txtExpense.Text = "0";
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
                    else if (row1["النوع"].ToString() == "مرتد")
                    {
                        PrintCopy_PullExpense_Report f = new PrintCopy_PullExpense_Report();
                        f.PrintInvoice(Convert.ToInt32(row1["التسلسل"].ToString()), row1["رقم المصروف"].ToString(), row1["الخزينة"].ToString(), row1["وارد"].ToString(), row1["المودع/المستلم"].ToString(), row1["البيان"].ToString(), row1["التاريخ"].ToString());
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

        private void gridView1_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            try
            {
                row1 = (DataRowView)gridView1.GetRow(e.RowHandle);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
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

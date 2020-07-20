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

namespace MainSystem
{
    public partial class SubExpensesTransitions_Report : Form
    {
        MySqlConnection conn, conn2;
        XtraTabControl xtraTabControlExpenses;
        bool loaded = false;
        //DataSet sourceDataSet = new DataSet();

        public SubExpensesTransitions_Report(XtraTabControl XtraTabControlExpenses)
        {
            InitializeComponent();
            conn = new MySqlConnection(connection.connectionString);
            conn2 = new MySqlConnection(connection.connectionString);
            xtraTabControlExpenses = XtraTabControlExpenses;
        }
        
        private void Bills_Transitions_Report_Load(object sender, EventArgs e)
        {
            try
            {
                string query = "select * from expense_main";
                MySqlDataAdapter da = new MySqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                comMain.DataSource = dt;
                comMain.DisplayMember = dt.Columns["MainExpense_Name"].ToString();
                comMain.ValueMember = dt.Columns["MainExpense_ID"].ToString();
                comMain.SelectedIndex = -1;


                //query = "select * from bank where Bank_Type='خزينة' or Bank_Type='خزينة مصروفات'";
                query = "select * from bank inner join bank_main on bank.MainBank_ID=bank_main.MainBank_ID where MainBank_Type='خزينة' and (MainBank_Name = 'خزينة مبيعات' or MainBank_Name = 'خزينة حسابات')";
                da = new MySqlDataAdapter(query, conn);
                dt = new DataTable();
                da.Fill(dt);
                comSafe.DataSource = dt;
                comSafe.DisplayMember = dt.Columns["Bank_Name"].ToString();
                comSafe.ValueMember = dt.Columns["Bank_ID"].ToString();
                comSafe.SelectedIndex = -1;
                loaded = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void comMain_SelectedValueChanged(object sender, EventArgs e)
        {
            if (loaded)
            {
                try
                {
                    if (comMain.SelectedValue != null)
                    {
                        string query = "select * from expense_sub where MainExpense_ID=" + comMain.SelectedValue.ToString();
                        MySqlDataAdapter da = new MySqlDataAdapter(query, conn);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        comSub.DataSource = dt;
                        comSub.DisplayMember = dt.Columns["SubExpense_Name"].ToString();
                        comSub.ValueMember = dt.Columns["SubExpense_ID"].ToString();
                        comSub.SelectedIndex = -1;
                    }
                    else
                    {
                        comSub.DataSource = null;
                    }
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (comMain.SelectedValue != null)
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
                conn2.Close();
            }
            else
            {
                MessageBox.Show("يجب اختيار المصروف الرئيسى");
            }
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            if (gridView1.RowCount > 0)
            {
                try
                {
                    List<Item_SubExpensesTransitions> bi = new List<Item_SubExpensesTransitions>();
                    for (int i = 0; i < gridView1.RowCount; i++)
                    {
                        Item_SubExpensesTransitions item = new Item_SubExpensesTransitions() { ID = Convert.ToInt32(gridView1.GetRowCellDisplayText(i, gridView1.Columns["التسلسل"])), Type= gridView1.GetRowCellDisplayText(i, gridView1.Columns["النوع"]), MainExpenses = gridView1.GetRowCellDisplayText(i, gridView1.Columns["المصروف الرئيسى"]), SubExpenses = gridView1.GetRowCellDisplayText(i, gridView1.Columns["المصروف الفرعى"]), Depesitor = gridView1.GetRowCellDisplayText(i, gridView1.Columns["المستلم"]), Date = Convert.ToDateTime(gridView1.GetRowCellDisplayText(i, gridView1.Columns["التاريخ"])).ToString("yyyy-MM-dd"), Money = Convert.ToDouble(gridView1.GetRowCellDisplayText(i, gridView1.Columns["المبلغ"])), Safe = gridView1.GetRowCellDisplayText(i, gridView1.Columns["الخزينة"]), Employee = gridView1.GetRowCellDisplayText(i, gridView1.Columns["الموظف"]), Description = gridView1.GetRowCellDisplayText(i, gridView1.Columns["البيان"]) };
                        bi.Add(item);
                    }
                    Report_SubExpensesTransitions f = new Report_SubExpensesTransitions();
                    f.PrintInvoice(comMain.Text, comSub.Text, comSafe.Text, dateTimePicker1.Value.Date, dateTimePicker2.Value.Date, txtExpense.Text, bi);
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
            //sourceDataSet = new DataSet();
            conn.Open();
            conn2.Open();

            double totalExpense = 0;
            string qSub = "";
            string qSafe = "";

            MySqlDataAdapter adapter = new MySqlDataAdapter("SELECT expense_transition.ExpenseTransition_ID as 'التسلسل',expense_transition.Type as 'النوع',expense_transition.Date as 'التاريخ',expense_main.MainExpense_Name as 'المصروف الرئيسى',expense_sub.SubExpense_Name as 'المصروف الفرعى',bank.Bank_Name as 'الخزينة',expense_transition.Amount as 'المبلغ',employee.Employee_Name as 'الموظف',expense_transition.Depositor_Name as 'المستلم',expense_transition.Description as 'البيان' FROM expense_transition left JOIN expense_sub ON expense_sub.SubExpense_ID = expense_transition.SubExpense_ID left JOIN expense_main ON expense_main.MainExpense_ID = expense_sub.MainExpense_ID INNER JOIN branch ON branch.Branch_ID = expense_transition.Branch_ID INNER JOIN bank ON bank.Bank_ID = expense_transition.Bank_ID INNER JOIN employee ON expense_transition.Employee_ID = employee.Employee_ID where ExpenseTransition_ID=0", conn);
            DataSet sourceDataSet = new DataSet();
            adapter.Fill(sourceDataSet);
            gridControl1.DataSource = sourceDataSet.Tables[0];

            if (comSub.SelectedValue == null && comSub.Text == "")
            {
                qSub = "select SubExpense_ID from expense_sub";
            }
            else
            {
                qSub = comSub.SelectedValue.ToString();
            }

            if (comSafe.SelectedValue == null && comSafe.Text == "")
            {
                //qSafe = "select Bank_ID from bank";
                qSafe = "select bank.Bank_ID from bank inner join bank_main on bank.MainBank_ID=bank_main.MainBank_ID where MainBank_Type='خزينة'";
            }
            else
            {
                qSafe = comSafe.SelectedValue.ToString();
            }

            /*MySqlDataAdapter adapterExpenseTransition = new MySqlDataAdapter("SELECT expense_transition.ExpenseTransition_ID as 'التسلسل',expense_transition.Date as 'التاريخ',expense_main.MainExpense_Name as 'المصروف الرئيسى',expense_sub.SubExpense_Name as 'المصروف الفرعى',expense_transition.Depositor_Name as 'المستلم',bank.Bank_Name as 'الخزينة',expense_transition.Amount as 'المبلغ',expense_transition.Description as 'البيان',employee.Employee_Name as 'الموظف' FROM expense_transition left JOIN expense_sub ON expense_transition.SubExpense_ID=expense_sub.SubExpense_ID left JOIN expense_main ON expense_sub.MainExpense_ID=expense_main.MainExpense_ID INNER JOIN branch ON branch.Branch_ID = expense_transition.Branch_ID INNER JOIN bank ON bank.Bank_ID = expense_transition.Bank_ID INNER JOIN employee ON expense_transition.Employee_ID = employee.Employee_ID where expense_sub.MainExpense_ID=" + comMain.SelectedValue.ToString() + " and expense_transition.SubExpense_ID in(" + qSub + ") and expense_transition.Bank_ID in(" + qSafe + ") and expense_transition.Error=0 and date(expense_transition.Date) between '" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "' and '" + dateTimePicker2.Value.ToString("yyyy-MM-dd") + "' order by expense_transition.Date", conn);
            adapterExpenseTransition.Fill(sourceDataSet, "expense_transition");

            string ExpenseTransitionID = "0";
            for (int i = 0; i < sourceDataSet.Tables["expense_transition"].Rows.Count-1; i++)
            {
                if (i == 0)
                {
                    ExpenseTransitionID = "";
                }
                ExpenseTransitionID += sourceDataSet.Tables["expense_transition"].Rows[i]["التسلسل"].ToString() + ",";
                totalExpense += Convert.ToDouble(sourceDataSet.Tables["expense_transition"].Rows[i]["المبلغ"].ToString());
            }
            if (sourceDataSet.Tables["expense_transition"].Rows.Count > 0)
            {
                ExpenseTransitionID += sourceDataSet.Tables["expense_transition"].Rows[sourceDataSet.Tables["expense_transition"].Rows.Count - 1]["التسلسل"].ToString();
                totalExpense += Convert.ToDouble(sourceDataSet.Tables["expense_transition"].Rows[sourceDataSet.Tables["expense_transition"].Rows.Count - 1]["المبلغ"].ToString());
            }

            MySqlDataAdapter adapterPullExpenseTransition = new MySqlDataAdapter("SELECT pullExpense_transition.ExpenseTransition_ID as 'التسلسل',pullExpense_transition.PullExpenseTransition_ID as 'رقم المصروف',pullExpense_transition.Date as 'التاريخ',pullExpense_transition.Depositor_Name as 'المستلم',bank.Bank_Name as 'الخزينة',pullExpense_transition.Amount as 'المبلغ',pullExpense_transition.Description as 'البيان',employee.Employee_Name as 'الموظف' FROM expense_transition as pullExpense_transition INNER JOIN branch ON branch.Branch_ID = pullExpense_transition.Branch_ID INNER JOIN bank ON bank.Bank_ID = pullExpense_transition.Bank_ID INNER JOIN employee ON pullExpense_transition.Employee_ID = employee.Employee_ID where pullExpense_transition.Type='مرتد' and pullExpense_transition.PullExpenseTransition_ID in ("+ ExpenseTransitionID + ") and pullExpense_transition.Error=0", conn);
            adapterPullExpenseTransition.Fill(sourceDataSet, "pullExpense_transition");
            
            for (int i = 0; i < sourceDataSet.Tables["pullExpense_transition"].Rows.Count - 1; i++)
            {
                totalExpense -= Convert.ToDouble(sourceDataSet.Tables["pullExpense_transition"].Rows[i]["المبلغ"].ToString());
            }
            if (sourceDataSet.Tables["pullExpense_transition"].Rows.Count > 0)
            {
                totalExpense -= Convert.ToDouble(sourceDataSet.Tables["pullExpense_transition"].Rows[sourceDataSet.Tables["pullExpense_transition"].Rows.Count - 1]["المبلغ"].ToString());
            }

            //Set up a master-detail relationship between the DataTables 
            DataColumn keyColumn = sourceDataSet.Tables["expense_transition"].Columns["التسلسل"];
            DataColumn foreignKeyColumn = sourceDataSet.Tables["pullExpense_transition"].Columns["رقم المصروف"];
            sourceDataSet.Relations.Add("المرتدات", keyColumn, foreignKeyColumn);

            gridControl1.DataSource = sourceDataSet.Tables["expense_transition"];*/

            string query = "SELECT expense_transition.ExpenseTransition_ID as 'التسلسل',expense_transition.Date as 'التاريخ',expense_main.MainExpense_Name as 'المصروف الرئيسى',expense_sub.SubExpense_Name as 'المصروف الفرعى',expense_transition.Depositor_Name as 'المستلم',bank.Bank_Name as 'الخزينة',expense_transition.Amount as 'المبلغ',expense_transition.Description as 'البيان',employee.Employee_Name as 'الموظف' FROM expense_transition left JOIN expense_sub ON expense_transition.SubExpense_ID=expense_sub.SubExpense_ID left JOIN expense_main ON expense_sub.MainExpense_ID=expense_main.MainExpense_ID INNER JOIN branch ON branch.Branch_ID = expense_transition.Branch_ID INNER JOIN bank ON bank.Bank_ID = expense_transition.Bank_ID INNER JOIN employee ON expense_transition.Employee_ID = employee.Employee_ID where expense_sub.MainExpense_ID=" + comMain.SelectedValue.ToString() + " and expense_transition.SubExpense_ID in(" + qSub + ") and expense_transition.Bank_ID in(" + qSafe + ") and expense_transition.Error=0 and date(expense_transition.Date) between '" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "' and '" + dateTimePicker2.Value.ToString("yyyy-MM-dd") + "' order by expense_transition.Date";
            MySqlCommand comand = new MySqlCommand(query, conn);
            MySqlDataReader dr = comand.ExecuteReader();
            while (dr.Read())
            {
                gridView1.AddNewRow();
                int rowHandle = gridView1.GetRowHandle(gridView1.DataRowCount);
                if (gridView1.IsNewItemRow(rowHandle))
                {
                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns["التسلسل"], dr["التسلسل"].ToString());
                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns["النوع"], "صرف");
                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns["المصروف الرئيسى"], dr["المصروف الرئيسى"].ToString());
                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns["المصروف الفرعى"], dr["المصروف الفرعى"].ToString());
                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns["التاريخ"], dr["التاريخ"].ToString());
                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns["المستلم"], dr["المستلم"].ToString());
                   
                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns["المبلغ"], dr["المبلغ"].ToString());
                    totalExpense += Convert.ToDouble(dr["المبلغ"].ToString());
                    
                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns["الخزينة"], dr["الخزينة"].ToString());
                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns["البيان"], dr["البيان"].ToString());
                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns["الموظف"], dr["الموظف"].ToString());
                }

                query = "SELECT pullExpense_transition.ExpenseTransition_ID as 'التسلسل',pullExpense_transition.PullExpenseTransition_ID as 'رقم المصروف',pullExpense_transition.Date as 'التاريخ',pullExpense_transition.Depositor_Name as 'المستلم',bank.Bank_Name as 'الخزينة',pullExpense_transition.Amount as 'المبلغ',pullExpense_transition.Description as 'البيان',employee.Employee_Name as 'الموظف' FROM expense_transition as pullExpense_transition INNER JOIN branch ON branch.Branch_ID = pullExpense_transition.Branch_ID INNER JOIN bank ON bank.Bank_ID = pullExpense_transition.Bank_ID INNER JOIN employee ON pullExpense_transition.Employee_ID = employee.Employee_ID where pullExpense_transition.Type='مرتد' and pullExpense_transition.PullExpenseTransition_ID=" + dr["التسلسل"].ToString() + " and pullExpense_transition.Error=0";
                comand = new MySqlCommand(query, conn2);
                MySqlDataReader dr2 = comand.ExecuteReader();
                while (dr2.Read())
                {
                    gridView1.AddNewRow();
                    int rowHandle2 = gridView1.GetRowHandle(gridView1.DataRowCount);
                    if (gridView1.IsNewItemRow(rowHandle2))
                    {
                        gridView1.SetRowCellValue(rowHandle2, gridView1.Columns["التسلسل"], dr2["التسلسل"].ToString());
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns["النوع"], "مرتد");
                        gridView1.SetRowCellValue(rowHandle2, gridView1.Columns["التاريخ"], dr2["التاريخ"].ToString());
                        gridView1.SetRowCellValue(rowHandle2, gridView1.Columns["المستلم"], dr2["المستلم"].ToString());

                        gridView1.SetRowCellValue(rowHandle2, gridView1.Columns["المبلغ"], dr2["المبلغ"].ToString());
                        totalExpense -= Convert.ToDouble(dr2["المبلغ"].ToString());

                        gridView1.SetRowCellValue(rowHandle2, gridView1.Columns["الخزينة"], dr2["الخزينة"].ToString());
                        gridView1.SetRowCellValue(rowHandle2, gridView1.Columns["البيان"], dr2["البيان"].ToString());
                        gridView1.SetRowCellValue(rowHandle2, gridView1.Columns["الموظف"], dr2["الموظف"].ToString());
                    }
                }
                dr2.Close();
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
            
            txtExpense.Text = totalExpense.ToString();
        }

        private void btnNewChosen_Click(object sender, EventArgs e)
        {
            try
            {
                clearCom();
                
                gridControl1.DataSource = null;
                txtExpense.Text = "0";
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

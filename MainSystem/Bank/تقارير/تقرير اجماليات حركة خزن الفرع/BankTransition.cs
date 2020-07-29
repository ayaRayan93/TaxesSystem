using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace TaxesSystem
{
    public partial class BankTransition : DevExpress.XtraReports.UI.XtraReport
    {
        public BankTransition()
        {
            InitializeComponent();
        }

        public void InitData(string Safe, DateTime dateFrom, DateTime dateTo, string Sales, string Returned, string IncomeExpenses, string Expenses, string pullExpense, string TransferTo, string TransferFrom, string TotalAdd, string TotalSub, string Safy)
        {
            DateNow.Value = DateTime.Now;
            Safe_Name.Value = Safe;
            DateFrom.Value = dateFrom;
            DateTo.Value = dateTo;
            Sales_Money.Value = Sales;
            Returned_Money.Value = Returned;
            IncomeExpense_Money.Value = IncomeExpenses;
            Expense_Money.Value = Expenses;
            PullExpense.Value = pullExpense;
            TransferTo_Money.Value = TransferTo;
            TransferFrom_Money.Value = TransferFrom;
            TotalAdd_Money.Value = TotalAdd;
            TotalSub_Money.Value = TotalSub;
            Safy_Money.Value = Safy;
        }
    }
}

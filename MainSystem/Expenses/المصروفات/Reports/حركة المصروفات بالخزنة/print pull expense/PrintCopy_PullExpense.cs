using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Collections.Generic;

namespace TaxesSystem
{
    public partial class PrintCopy_PullExpense : DevExpress.XtraReports.UI.XtraReport
    {
        public PrintCopy_PullExpense()
        {
            InitializeComponent();
        }

        public void InitData(int ExpenseTransitionID, string ExpenseNum, string Bank, string PullMoney, string Client, string Descrip, string date)
        {
            ID.Value = ExpenseTransitionID;
            PullExpenseNum.Value = ExpenseNum;
            SafeName.Value = Bank;
            Money.Value = PullMoney;
            DepositorName.Value = Client;
            Description.Value = Descrip;
            Date.Value = date;
        }
    }
}

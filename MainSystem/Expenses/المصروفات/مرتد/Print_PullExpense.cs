using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Collections.Generic;

namespace TaxesSystem
{
    public partial class Print_PullExpense : DevExpress.XtraReports.UI.XtraReport
    {
        public Print_PullExpense()
        {
            InitializeComponent();
        }

        public void InitData(int ExpenseTransitionID, string ExpenseNum, string Bank, string PullMoney, string Client, string Descrip)
        {
            ID.Value = ExpenseTransitionID;
            PullExpenseNum.Value = ExpenseNum;
            SafeName.Value = Bank;
            Money.Value = PullMoney;
            DepositorName.Value = Client;
            Description.Value = Descrip;
            Date.Value = DateTime.Now;
        }
    }
}

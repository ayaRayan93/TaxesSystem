using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Collections.Generic;

namespace MainSystem
{
    public partial class Print_IncomeExpense : DevExpress.XtraReports.UI.XtraReport
    {
        public Print_IncomeExpense()
        {
            InitializeComponent();
        }

        public void InitData(int ExpenseTransitionID, string Bank, string PullMoney, string Client, string Descrip)
        {
            ID.Value = ExpenseTransitionID;
            SafeName.Value = Bank;
            Money.Value = PullMoney;
            DepositorName.Value = Client;
            Description.Value = Descrip;
            Date.Value = DateTime.Now;
        }
    }
}

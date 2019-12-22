using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Collections.Generic;

namespace MainSystem
{
    public partial class Print_Expense : DevExpress.XtraReports.UI.XtraReport
    {
        public Print_Expense()
        {
            InitializeComponent();
        }

        public void InitData(string Main, string Sub, string Bank, string PullMoney, string Client, string Descrip)
        {
            MainExpense.Value = Main;
            SubExpense.Value = Sub;
            SafeName.Value = Bank;
            Money.Value = PullMoney;
            DepositorName.Value = Client;
            Description.Value = Descrip;
        }
    }
}

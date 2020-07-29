﻿using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Collections.Generic;

namespace TaxesSystem
{
    public partial class Print_Expense2 : DevExpress.XtraReports.UI.XtraReport
    {
        public Print_Expense2()
        {
            InitializeComponent();
        }

        public void InitData(int ExpenseTransitionID, string Main, string Sub, string Bank, string PullMoney, string Client, string Descrip)
        {
            ID.Value = ExpenseTransitionID;
            MainExpense.Value = Main;
            SubExpense.Value = Sub;
            SafeName.Value = Bank;
            Money.Value = PullMoney;
            DepositorName.Value = Client;
            Description.Value = Descrip;
            Date.Value = DateTime.Now;
        }
    }
}

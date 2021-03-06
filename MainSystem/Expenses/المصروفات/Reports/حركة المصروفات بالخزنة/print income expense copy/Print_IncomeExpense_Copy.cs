﻿using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Collections.Generic;

namespace TaxesSystem
{
    public partial class Print_IncomeExpense_Copy : DevExpress.XtraReports.UI.XtraReport
    {
        public Print_IncomeExpense_Copy()
        {
            InitializeComponent();
        }

        public void InitData(int ExpenseTransitionID, string Bank, string PullMoney, string Client, string Descrip, string date)
        {
            ID.Value = ExpenseTransitionID;
            SafeName.Value = Bank;
            Money.Value = PullMoney;
            DepositorName.Value = Client;
            Description.Value = Descrip;
            Date.Value = date;
        }
    }
}

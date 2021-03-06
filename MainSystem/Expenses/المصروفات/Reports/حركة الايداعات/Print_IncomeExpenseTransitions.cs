﻿using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Collections.Generic;

namespace TaxesSystem
{
    public partial class Print_IncomeExpenseTransitions : DevExpress.XtraReports.UI.XtraReport
    {
        public Print_IncomeExpenseTransitions()
        {
            InitializeComponent();
        }

        public void InitData(string Safe, DateTime fromDate, DateTime toDate, List<Item_SubExpensesTransitions> Bill_Items)
        {
            Safe_Name.Value = Safe;
            FromDate.Value = fromDate;
            ToDate.Value = toDate;
            DateNow.Value = DateTime.Now;
            objectDataSource1.DataSource = Bill_Items;
        }
    }
}

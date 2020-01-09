using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Collections.Generic;

namespace MainSystem
{
    public partial class Print_SubExpensesTransitions : DevExpress.XtraReports.UI.XtraReport
    {
        public Print_SubExpensesTransitions()
        {
            InitializeComponent();
        }

        public void InitData(string Main, string Sub, string Safe, DateTime fromDate, DateTime toDate, List<Item_SubExpensesTransitions> Bill_Items)
        {
            Main_Expense.Value = Main;
            Sub_Expense.Value = Sub;
            Safe_Name.Value = Safe;
            FromDate.Value = fromDate;
            ToDate.Value = toDate;
            DateNow.Value = DateTime.Now;
            objectDataSource1.DataSource = Bill_Items;
        }
    }
}

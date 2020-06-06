using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Collections.Generic;

namespace MainSystem
{
    public partial class Print_SubPropertyTransitions : DevExpress.XtraReports.UI.XtraReport
    {
        public Print_SubPropertyTransitions()
        {
            InitializeComponent();
        }

        public void InitData(string Main, string Sub, string details, string Safe, DateTime fromDate, DateTime toDate, List<Item_SubExpensesTransitions> Bill_Items)
        {
            Main_Expense.Value = Main;
            Sub_Expense.Value = Sub;
            Details_Property.Value = details;
            Safe_Name.Value = Safe;
            FromDate.Value = fromDate;
            ToDate.Value = toDate;
            DateNow.Value = DateTime.Now;
            objectDataSource1.DataSource = Bill_Items;
        }
    }
}

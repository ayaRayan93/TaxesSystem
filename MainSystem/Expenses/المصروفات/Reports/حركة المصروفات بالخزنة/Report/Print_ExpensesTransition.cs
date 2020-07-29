using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Collections.Generic;

namespace TaxesSystem
{
    public partial class Print_ExpensesTransition : DevExpress.XtraReports.UI.XtraReport
    {
        public Print_ExpensesTransition()
        {
            InitializeComponent();
        }

        public void InitData(DateTime fromDate, DateTime toDate, string branch_Name, List<ExpensesTransition_Items> Bill_Items)
        {
            FromDate.Value = fromDate.Date;
            ToDate.Value = toDate.Date;
            DateNow.Value = DateTime.Now;
            Branch_Name.Value = branch_Name;
            objectDataSource1.DataSource = Bill_Items;
        }
    }
}

using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Collections.Generic;

namespace MainSystem
{
    public partial class Print_PayTypeTransition : DevExpress.XtraReports.UI.XtraReport
    {
        public Print_PayTypeTransition()
        {
            InitializeComponent();
        }

        public void InitData(DateTime fromDate, DateTime toDate, string branch_Name, List<Transition_Items> Bill_Items)
        {
            FromDate.Value = fromDate.Date;
            ToDate.Value = toDate.Date;
            DateNow.Value = DateTime.Now;
            Branch_Name.Value = branch_Name;
            objectDataSource1.DataSource = Bill_Items;
        }
    }
}

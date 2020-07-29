using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Collections.Generic;

namespace TaxesSystem
{
    public partial class Print_AglTransition : DevExpress.XtraReports.UI.XtraReport
    {
        public Print_AglTransition()
        {
            InitializeComponent();
        }

        public void InitData(DateTime fromDate, DateTime toDate, List<Transition_Items> Bill_Items)
        {
            FromDate.Value = fromDate.Date;
            ToDate.Value = toDate.Date;
            DateNow.Value = DateTime.Now;
            objectDataSource1.DataSource = Bill_Items;
        }
    }
}

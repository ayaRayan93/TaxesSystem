using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Collections.Generic;

namespace MainSystem.Sales.accounting
{
    public partial class XtraReportTransition : DevExpress.XtraReports.UI.XtraReport
    {
        public XtraReportTransition()
        {
            InitializeComponent();
        }
        public void InitData(List<TransitionData> Bill_Items)
        {
            //objectDataSource1.DataSource = Bill_Items;
        }
    }
}

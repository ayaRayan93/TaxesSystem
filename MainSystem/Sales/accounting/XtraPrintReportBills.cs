using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Collections.Generic;

namespace MainSystem.Sales.accounting
{
    public partial class XtraPrintReportBills : DevExpress.XtraReports.UI.XtraReport
    {
        public XtraPrintReportBills()
        {
            InitializeComponent();
        }
        public void InitData(List<TransitionData> TransitionData, string name)
        {
            objectDataSource1.DataSource = TransitionData;
            Name.Value = name;
        }
    }
}

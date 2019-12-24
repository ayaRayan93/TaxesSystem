using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Collections.Generic;

namespace MainSystem.Sales.accounting
{
    public partial class XtraPrintReport : DevExpress.XtraReports.UI.XtraReport
    {
        public XtraPrintReport()
        {
            InitializeComponent();
        }
        public void InitData(List<TransitionData> TransitionData,string name, double befor, double totalBills, double totalReturns, double safay1,string from,string to)
        {
            objectDataSource1.DataSource = TransitionData;
            Name.Value = name;
            TotalBefor.Value = befor;
            TotalPaid.Value = totalBills;
            TotalReturns.Value = totalReturns;
            TotalSafay.Value = safay1;
            dateFrom.Value = from;
            dateTo.Value = to;
        }
    }
}

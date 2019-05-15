using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Collections.Generic;

namespace MainSystem
{
    public partial class Print_OrderReportX : DevExpress.XtraReports.UI.XtraReport
    {
        public Print_OrderReportX()
        {
            InitializeComponent();
        }

        public void InitData(List<ReportOrder_Items> ReceiptItems)
        {
            DateNow.Value = DateTime.Now;
            objectDataSource1.DataSource = ReceiptItems;
        }
    }
}

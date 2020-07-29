using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Collections.Generic;

namespace TaxesSystem
{
    public partial class Print_OrderReport : DevExpress.XtraReports.UI.XtraReport
    {
        public Print_OrderReport()
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

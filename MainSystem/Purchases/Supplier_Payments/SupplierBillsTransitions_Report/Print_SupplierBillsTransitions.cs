using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Collections.Generic;

namespace MainSystem
{
    public partial class Print_SupplierBillsTransitions : DevExpress.XtraReports.UI.XtraReport
    {
        public Print_SupplierBillsTransitions()
        {
            InitializeComponent();
        }

        public void InitData(string supplierName, string dateFrom, string dateTo, List<SupplierBillsTransitions_Items> ReceiptItems)
        {
            DateFrom.Value = dateFrom;
            DateTo.Value = dateTo;
            SupplierName.Value = supplierName;
            objectDataSource1.DataSource = ReceiptItems;
        }
    }
}

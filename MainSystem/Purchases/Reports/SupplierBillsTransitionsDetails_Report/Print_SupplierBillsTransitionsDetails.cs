using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Collections.Generic;

namespace TaxesSystem
{
    public partial class Print_SupplierBillsTransitionsDetails : DevExpress.XtraReports.UI.XtraReport
    {
        public Print_SupplierBillsTransitionsDetails()
        {
            InitializeComponent();
        }

        public void InitData(string supplierName, string dateFrom, string dateTo, List<SupplierBillsTransitionsDetails_Items> ReceiptItems)
        {
            DateFrom.Value = dateFrom;
            DateTo.Value = dateTo;
            SupplierName.Value = supplierName;
            objectDataSource1.DataSource = ReceiptItems;
        }
    }
}

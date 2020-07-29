using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Collections.Generic;

namespace TaxesSystem
{
    public partial class Print_Transportation_Bill2 : DevExpress.XtraReports.UI.XtraReport
    {
        public Print_Transportation_Bill2()
        {
            InitializeComponent();
        }

        public void InitData(string Store, DateTime dateFrom, DateTime dateTo, List<TransportationBill_Items> ReceiptItems)
        {
            StoreName.Value = Store;
            DateFrom.Value = dateFrom;
            DateTo.Value = dateTo;
            DateNow.Value = DateTime.Now;
            objectDataSource1.DataSource = ReceiptItems;
        }
    }
}

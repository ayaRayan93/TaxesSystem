using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Collections.Generic;

namespace TaxesSystem
{
    public partial class Print_Transportation_Bill_Copy : DevExpress.XtraReports.UI.XtraReport
    {
        public Print_Transportation_Bill_Copy()
        {
            InitializeComponent();
        }

        public void InitData(int transferProductID, string fromStore, string toStore, string date, List<Transportation_Items> ReceiptItems)
        {
            TransferNumber.Value = transferProductID;
            FromStore.Value = fromStore;
            ToStore.Value = toStore;
            DateNow.Value = date;
            objectDataSource1.DataSource = ReceiptItems;
        }
    }
}

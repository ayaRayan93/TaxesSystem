﻿using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Collections.Generic;

namespace TaxesSystem
{
    public partial class Print_Transportation : DevExpress.XtraReports.UI.XtraReport
    {
        public Print_Transportation()
        {
            InitializeComponent();
        }

        public void InitData(int transferProductID, string fromStore, string toStore, List<Transportation_Items> ReceiptItems)
        {
            TransferNumber.Value = transferProductID;
            FromStore.Value = fromStore;
            ToStore.Value = toStore;
            DateNow.Value = DateTime.Now;
            objectDataSource1.DataSource = ReceiptItems;
        }
    }
}

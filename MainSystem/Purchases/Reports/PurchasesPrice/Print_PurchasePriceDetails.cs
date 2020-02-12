using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Collections.Generic;

namespace MainSystem
{
    public partial class Print_PurchasePriceDetails : DevExpress.XtraReports.UI.XtraReport
    {
        public Print_PurchasePriceDetails()
        {
            InitializeComponent();
        }

        public void InitData(string branchName, DateTime fromDate, DateTime toDate, List<Items_Bills_Details> Bill_Items)
        {
            Branch_Name.Value = branchName;
            FromDate.Value = fromDate;
            ToDate.Value = toDate;
            DateNow.Value = DateTime.Now;
            objectDataSource1.DataSource = Bill_Items;
        }
    }
}

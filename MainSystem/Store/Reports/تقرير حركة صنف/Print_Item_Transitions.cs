using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Collections.Generic;

namespace MainSystem
{
    public partial class Print_Item_Transitions : DevExpress.XtraReports.UI.XtraReport
    {
        public Print_Item_Transitions()
        {
            InitializeComponent();
        }

        public void InitData(string Store, DateTime fromDate, DateTime toDate, string productName, double TotalBills, double TotalReturn, double safy, double before, List<Item_Transitions> Bill_Items)
        {
            Store_Name.Value = Store;
            FromDate.Value = fromDate;
            ToDate.Value = toDate;
            ProductName.Value = productName;
            TotalIncrease.Value = TotalBills;
            TotalDecrease.Value = TotalReturn;
            Safy.Value = safy;
            Before.Value = before;
            DateNow.Value = DateTime.Now;
            objectDataSource1.DataSource = Bill_Items;
        }
    }
}

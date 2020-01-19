using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Collections.Generic;

namespace MainSystem
{
    public partial class Print_Items_Storage : DevExpress.XtraReports.UI.XtraReport
    {
        public Print_Items_Storage()
        {
            InitializeComponent();
        }

        public void InitData(DateTime date, double TotalBillPriceAD, List<Items_Storage> Bill_Items)
        {
            TotalQuantity.Value = TotalBillPriceAD;
            DateNow.Value = date;
            objectDataSource1.DataSource = Bill_Items;
        }
    }
}

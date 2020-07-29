using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Collections.Generic;

namespace TaxesSystem
{
    public partial class Print_Items_Storage2 : DevExpress.XtraReports.UI.XtraReport
    {
        public Print_Items_Storage2()
        {
            InitializeComponent();
        }

        public void InitData(string storeId, string store_Name, double TotalBillPriceAD, List<Items_Factory> Bill_Items)
        {
            Store_ID.Value = storeId;
            Store_Name.Value = store_Name;
            FinalBillCost.Value = TotalBillPriceAD;
            DateNow.Value = DateTime.Now;
            objectDataSource2.DataSource = Bill_Items;
        }
    }
}

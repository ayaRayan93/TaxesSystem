using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Collections.Generic;

namespace MainSystem
{
    public partial class Print_Items_StorageWithPurchasesPrice : DevExpress.XtraReports.UI.XtraReport
    {
        public Print_Items_StorageWithPurchasesPrice()
        {
            InitializeComponent();
        }

        public void InitData(string storeName, DateTime date, double TotalBillPriceAD,double TotalPurchases, List<Items_StorageWithPurshasesPrice> Bill_Items)
        {
            Store_Name.Value = storeName;
            TotalQuantity.Value = TotalBillPriceAD;
            DateNow.Value = date;
            TotalQuantityPurchasesPrice.Value = TotalPurchases;
            objectDataSource1.DataSource = Bill_Items;
        }
    }
}

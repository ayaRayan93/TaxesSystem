using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Collections.Generic;

namespace TaxesSystem
{
    public partial class Print_Items_Factory2 : DevExpress.XtraReports.UI.XtraReport
    {
        public Print_Items_Factory2()
        {
            InitializeComponent();
        }

        public void InitData(DateTime date, double TotalBillPriceAD, List<Items_Factory> Bill_Items)
        {
            FinalBillCost.Value = TotalBillPriceAD;
            DateNow.Value = date;
            objectDataSource2.DataSource = Bill_Items;
        }
    }
}

using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Collections.Generic;

namespace TaxesSystem
{
    public partial class Print_ProductsBillsDetails : DevExpress.XtraReports.UI.XtraReport
    {
        public Print_ProductsBillsDetails()
        {
            InitializeComponent();
        }

        public void InitData(string branchName, string factoryName, DateTime fromDate, DateTime toDate, List<Items_Bills_Details> Bill_Items)
        {
            Branch_Name.Value = branchName;
            Factory_Name.Value = factoryName;
            FromDate.Value = fromDate;
            ToDate.Value = toDate;
            DateNow.Value = DateTime.Now;
            objectDataSource1.DataSource = Bill_Items;
        }
    }
}

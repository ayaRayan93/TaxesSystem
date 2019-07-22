using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Collections.Generic;

namespace MainSystem
{
    public partial class Print_ProductsBillsDate : DevExpress.XtraReports.UI.XtraReport
    {
        public Print_ProductsBillsDate()
        {
            InitializeComponent();
        }

        public void InitData(string branchName, List<Items_BillsDate> Bill_Items)
        {
            Branch_Name.Value = branchName;
            DateNow.Value = DateTime.Now;
            objectDataSource1.DataSource = Bill_Items;
        }
    }
}

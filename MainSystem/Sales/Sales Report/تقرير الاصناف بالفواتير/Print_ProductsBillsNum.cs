using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Collections.Generic;

namespace MainSystem
{
    public partial class Print_ProductsBillsNum : DevExpress.XtraReports.UI.XtraReport
    {
        public Print_ProductsBillsNum()
        {
            InitializeComponent();
        }

        public void InitData(string branchName, string fromBill, string toBill, List<Items_Bills> Bill_Items)
        {
            Branch_Name.Value = branchName;
            FromBill.Value = fromBill;
            ToBill.Value = toBill;
            DateNow.Value = DateTime.Now;
            objectDataSource1.DataSource = Bill_Items;
        }
    }
}

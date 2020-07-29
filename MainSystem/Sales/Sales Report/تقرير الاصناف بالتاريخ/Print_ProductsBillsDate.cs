﻿using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Collections.Generic;

namespace TaxesSystem
{
    public partial class Print_ProductsBillsDate : DevExpress.XtraReports.UI.XtraReport
    {
        public Print_ProductsBillsDate()
        {
            InitializeComponent();
        }

        public void InitData(string branchName, DateTime fromDate, DateTime toDate, List<Items_Bills> Bill_Items)
        {
            Branch_Name.Value = branchName;
            FromDate.Value = fromDate;
            ToDate.Value = toDate;
            DateNow.Value = DateTime.Now;
            objectDataSource1.DataSource = Bill_Items;
        }
    }
}

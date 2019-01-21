using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Collections.Generic;

namespace MainSystem
{
    public partial class StorePermNums : DevExpress.XtraReports.UI.XtraReport
    {
        public StorePermNums()
        {
            InitializeComponent();
        }

        public void InitializeData(List<StorePermissionsNumbers> listOfStorePermissionsNumbers)
        {
            DataSource = listOfStorePermissionsNumbers;
        }
    }
}

using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Collections.Generic;

namespace MainSystem
{
    public partial class Print_LeastQuantity : DevExpress.XtraReports.UI.XtraReport
    {
        public Print_LeastQuantity()
        {
            InitializeComponent();
        }

        public void InitData(List<LeastQuantity_Items> ReceiptItems)
        {
            DateNow.Value = DateTime.Now;
            objectDataSource1.DataSource = ReceiptItems;
        }
    }
}

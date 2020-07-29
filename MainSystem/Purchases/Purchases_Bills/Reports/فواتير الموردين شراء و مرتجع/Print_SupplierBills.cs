using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Collections.Generic;

namespace TaxesSystem
{
    public partial class Print_SupplierBills : DevExpress.XtraReports.UI.XtraReport
    {
        public Print_SupplierBills()
        {
            InitializeComponent();
        }

        public void InitData(string totalBills, string totalReturns, string safy, List<SupplierBills_Items> ReceiptItems)
        {
            TotalBills.Value = totalBills;
            TotalReturns.Value = totalReturns;
            Safy.Value = safy;
            objectDataSource1.DataSource = ReceiptItems;
        }
    }
}

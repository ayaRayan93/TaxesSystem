using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Collections.Generic;

namespace MainSystem
{
    public partial class Print_SupplierReceiptCopy : DevExpress.XtraReports.UI.XtraReport
    {
        public Print_SupplierReceiptCopy()
        {
            InitializeComponent();
        }

        public void InitData(string storeName, string permissionNum, string supplierName, List<SupplierReceipt_Items> ReceiptItems)
        {
            DateNow.Value = DateTime.Now;
            StoreName.Value = storeName;
            PermissionNumber.Value = permissionNum;
            SupplierName.Value = supplierName;
            objectDataSource1.DataSource = ReceiptItems;
        }
    }
}

using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Collections.Generic;

namespace TaxesSystem
{
    public partial class Print_SupplierReceiptCopy : DevExpress.XtraReports.UI.XtraReport
    {
        public Print_SupplierReceiptCopy()
        {
            InitializeComponent();
        }

        public void InitData(string storeName, string permissionNum, string supplierName, DateTime Date, List<SupplierReceipt_Items> ReceiptItems)
        {
            DateNow.Value = Date;
            StoreName.Value = storeName;
            PermissionNumber.Value = permissionNum;
            SupplierName.Value = supplierName;
            objectDataSource1.DataSource = ReceiptItems;
        }
    }
}

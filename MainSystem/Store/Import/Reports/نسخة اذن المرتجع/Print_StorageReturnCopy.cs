using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Collections.Generic;

namespace MainSystem
{
    public partial class Print_StorageReturnCopy : DevExpress.XtraReports.UI.XtraReport
    {
        public Print_StorageReturnCopy()
        {
            InitializeComponent();
        }

        public void InitData(string storeName, string permissionNum, string supplierName, int returnedPermissionNumber, string reason, List<StorageReturn_Items> ReceiptItems)
        {
            DateNow.Value = DateTime.Now;
            StoreName.Value = storeName;
            PermissionNumber.Value = permissionNum;
            SupplierName.Value = supplierName;
            ReturnedPermissionNumber.Value = returnedPermissionNumber;
            ReturnedReason.Value = reason;
            objectDataSource1.DataSource = ReceiptItems;
        }
    }
}

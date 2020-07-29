﻿using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Collections.Generic;

namespace TaxesSystem
{
    public partial class Print_StorageReturn : DevExpress.XtraReports.UI.XtraReport
    {
        public Print_StorageReturn()
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

using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Collections.Generic;

namespace MainSystem
{
    public partial class Print_SupplierBill_Copy : DevExpress.XtraReports.UI.XtraReport
    {
        public Print_SupplierBill_Copy()
        {
            InitializeComponent();
        }

        public void InitData(string storeName, string permissionNum, string supplierName, string SupPerm, string storePermessionNum, double TotalA, double addabtiveTax, List<SupplierBill_Items> ReceiptItems)
        {
            DateNow.Value = DateTime.Now;
            StoreName.Value = storeName;
            PermissionNumber.Value = permissionNum;
            SupplierName.Value = supplierName;
            SupplierPermession.Value = SupPerm;
            StorePermessionNum.Value = storePermessionNum;
            Safy.Value = TotalA;
            Value_Additive_Tax.Value = addabtiveTax;
            objectDataSource1.DataSource = ReceiptItems;
        }
    }
}

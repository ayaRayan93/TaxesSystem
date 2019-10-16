using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Collections.Generic;

namespace MainSystem
{
    public partial class Print_SupplierReturnBill : DevExpress.XtraReports.UI.XtraReport
    {
        public Print_SupplierReturnBill()
        {
            InitializeComponent();
        }

        public void InitData(string storeName, string permissionNum, string supplierName, string storePermessionNum, string SupPerm, double discount, double totalA, double addabtiveTax, double totalSafy, List<SupplierReturnBill_Items> ReceiptItems)
        {
            DateNow.Value = DateTime.Now;
            StoreName.Value = storeName;
            PermissionNumber.Value = permissionNum;
            SupplierName.Value = supplierName;
            StorePermessionNum.Value = storePermessionNum;
            SupplierPermession.Value = SupPerm;
            TotalDiscount.Value = discount;
            TotalA.Value = totalA;
            Safy.Value = totalSafy;
            Value_Additive_Tax.Value = addabtiveTax;
            objectDataSource2.DataSource = ReceiptItems;
        }
    }
}

using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Collections.Generic;

namespace MainSystem
{
    public partial class Print_SupplierReturnBillCopy : DevExpress.XtraReports.UI.XtraReport
    {
        public Print_SupplierReturnBillCopy()
        {
            InitializeComponent();
        }

        public void InitData(string storeName, string permissionNum, string supplierName, string storePermessionNum, string SupPerm, string date, double discount, double TotalA, double addabtiveTax, List<SupplierReturnBill_Items> ReceiptItems)
        {
            DateNow.Value = date;
            StoreName.Value = storeName;
            PermissionNumber.Value = permissionNum;
            SupplierName.Value = supplierName;
            StorePermessionNum.Value = storePermessionNum;
            SupplierPermession.Value = SupPerm;
            TotalDiscount.Value = discount;
            Safy.Value = TotalA;
            Value_Additive_Tax.Value = addabtiveTax;
            objectDataSource2.DataSource = ReceiptItems;
        }
    }
}

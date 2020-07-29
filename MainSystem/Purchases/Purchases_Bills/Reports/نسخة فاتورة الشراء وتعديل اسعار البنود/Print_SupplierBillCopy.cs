using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Collections.Generic;

namespace TaxesSystem
{
    public partial class Print_SupplierBillCopy : DevExpress.XtraReports.UI.XtraReport
    {
        public Print_SupplierBillCopy()
        {
            InitializeComponent();
        }

        public void InitData(string storeName, string permissionNum, string supplierName, string SupPerm, string storePermessionNum, string date, double totalDiscount, double Totala, double addabtiveTax, double TotalSafy, string importDate, List<SupplierBill_Items> ReceiptItems)
        {
            DateNow.Value = date;
            StoreName.Value = storeName;
            PermissionNumber.Value = permissionNum;
            SupplierName.Value = supplierName;
            SupplierPermession.Value = SupPerm;
            StorePermessionNum.Value = storePermessionNum;
            TotalDiscount.Value = totalDiscount;
            TotalA.Value = Totala;
            Safy.Value = TotalSafy;
            Value_Additive_Tax.Value = addabtiveTax;
            ImportDate.Value = importDate;
            objectDataSource1.DataSource = ReceiptItems;
        }
    }
}

﻿using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Collections.Generic;

namespace TaxesSystem
{
    public partial class Print_SupplierBill : DevExpress.XtraReports.UI.XtraReport
    {
        public Print_SupplierBill()
        {
            InitializeComponent();
        }

        public void InitData(string storeName, string permissionNum, string supplierName, string SupPerm, string storePermessionNum, double discount, double Totala, double addabtiveTax, double safy, DateTime importDate, List<SupplierBill_Items> ReceiptItems)
        {
            DateNow.Value = DateTime.Now;
            StoreName.Value = storeName;
            PermissionNumber.Value = permissionNum;
            SupplierName.Value = supplierName;
            SupplierPermession.Value = SupPerm;
            StorePermessionNum.Value = storePermessionNum;
            TotalDiscount.Value = discount;
            TotalA.Value = Totala;
            Safy.Value = safy;
            Value_Additive_Tax.Value = addabtiveTax;
            ImportDate.Value = importDate;
            objectDataSource1.DataSource = ReceiptItems;
        }
    }
}

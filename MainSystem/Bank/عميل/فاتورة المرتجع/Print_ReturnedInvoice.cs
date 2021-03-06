﻿using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Collections.Generic;

namespace TaxesSystem
{
    public partial class Print_ReturnedInvoice : DevExpress.XtraReports.UI.XtraReport
    {
        public Print_ReturnedInvoice()
        {
            InitializeComponent();
        }

        public void InitData(string client_Name, string customer_Name, string phoneNumber, DateTime billDate, string pay_Type, int bill_Number, string branchId, string branch_Name, double TotalBillPriceAD, string returnInfo, List<ReturnedBill_Items> Bill_Items,string DelegateName)
        {
            Client_Name.Value = client_Name;
            Customer_Name.Value = customer_Name;
            PhoneNum.Value = phoneNumber;
            Bill_Number.Value = bill_Number;
            Branch_ID.Value = branchId;
            Branch_Name.Value = branch_Name;
            Bill_Date.Value = billDate.Date;
            FinalBillCost.Value = TotalBillPriceAD;
            DateNow.Value = DateTime.Now;
            ReturnInfo.Value = returnInfo;
            objectDataSource2.DataSource = Bill_Items;
            Delegate_Name.Value = DelegateName;
        }
    }
}

using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Collections.Generic;

namespace MainSystem
{
    public partial class Print_Invoice : DevExpress.XtraReports.UI.XtraReport
    {
        public Print_Invoice()
        {
            InitializeComponent();
        }

        public void InitData(string client_Name, string pay_Type, int bill_Number, double TotalBillPriceBD, double TotalBillPriceAD, double TotalDiscount, List<Bill_Items> Bill_Items)
        {
            Client_Name.Value = client_Name;
            PayType.Value = pay_Type;
            Bill_Number.Value = bill_Number;
            Bill_Date.Value = DateTime.Now;
            TotaBillCost.Value = TotalBillPriceBD;
            FinalBillCost.Value = TotalBillPriceAD;
            FinalDiscount.Value = TotalDiscount;
            objectDataSource1.DataSource = Bill_Items;
        }
    }
}

using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Collections.Generic;

namespace MainSystem
{
    public partial class Print_ReturnedInvoice : DevExpress.XtraReports.UI.XtraReport
    {
        public Print_ReturnedInvoice()
        {
            InitializeComponent();
        }

        public void InitData(string client_Name, DateTime billDate, string pay_Type, int bill_Number, string branch_Name, double TotalBillPriceAD, List<Bill_Items> Bill_Items)
        {
            Client_Name.Value = client_Name;
            Bill_Number.Value = bill_Number;
            Branch_Name.Value = branch_Name;
            Bill_Date.Value = billDate.Date;
            FinalBillCost.Value = TotalBillPriceAD;
            DateNow.Value = DateTime.Now;
            objectDataSource2.DataSource = Bill_Items;
        }
    }
}

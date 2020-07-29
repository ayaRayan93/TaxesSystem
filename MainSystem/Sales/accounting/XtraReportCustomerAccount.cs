using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Collections.Generic;

namespace TaxesSystem.Sales.accounting
{
    public partial class XtraReportCustomerAccount : DevExpress.XtraReports.UI.XtraReport
    {
        public XtraReportCustomerAccount()
        {
            InitializeComponent();
        }
        public void InitData(List<customerAccount> TransitionData, double befor, double totalBills, double totalPaid, double safay1, string from, string to)
        {
            objectDataSource1.DataSource = TransitionData;
            totalSafayBefore.Value = befor;
            totalSafayBill.Value = totalBills;
            customerRestMoney.Value = safay1;
            totalSafayPaid.Value = totalPaid;
            dateFrom.Value = from;
            dateTo.Value = to;
        }
    }
}

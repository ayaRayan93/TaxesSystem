﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace MainSystem
{
    public partial class Print_CopyReturnedBill_Report : DevExpress.XtraEditors.XtraForm
    {
        public Print_CopyReturnedBill_Report()
        {
            InitializeComponent();
        }

        public void PrintInvoice(string ClientName, string phoneNumber, DateTime billDate, string PayType, int BillNumber, string branchId, string BranchName, double TotalBillPriceAD, string returnInfo, List<CopyReturnedBill_Items> BillItems)
        {
            Print_CopyReturnedInvoice report = new Print_CopyReturnedInvoice();
            foreach(DevExpress.XtraReports.Parameters.Parameter p in report.Parameters)
            {
                p.Visible = false;
            }
            report.InitData(ClientName, phoneNumber, billDate, PayType, BillNumber, branchId, BranchName, TotalBillPriceAD, returnInfo, BillItems);
            documentViewer1.DocumentSource = report;
            report.CreateDocument();
        }
    }
}
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

namespace TaxesSystem
{
    public partial class Report_SupplierBill : DevExpress.XtraEditors.XtraForm
    {
        public Report_SupplierBill()
        {
            InitializeComponent();
        }

        public void PrintInvoice(string StoreName, string PermissionNum, string SupplierName, string SupPerm, string storePermessionNum, double discount, double TotalA, double addabtiveTax, double TotalSafy, DateTime ImportDate, List<SupplierBill_Items> ReceiptItems)
        {
            Print_SupplierBill report = new Print_SupplierBill();
            foreach(DevExpress.XtraReports.Parameters.Parameter p in report.Parameters)
            {
                p.Visible = false;
            }
            report.InitData(StoreName, PermissionNum, SupplierName, SupPerm, storePermessionNum, discount, TotalA, addabtiveTax, TotalSafy, ImportDate, ReceiptItems);
            documentViewer1.DocumentSource = report;
            report.CreateDocument();
        }
    }
}
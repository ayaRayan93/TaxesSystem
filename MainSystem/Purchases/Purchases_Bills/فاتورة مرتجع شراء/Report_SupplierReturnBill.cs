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
    public partial class Report_SupplierReturnBill : DevExpress.XtraEditors.XtraForm
    {
        public Report_SupplierReturnBill()
        {
            InitializeComponent();
        }

        public void PrintInvoice(string StoreName, string PermissionNum, string SupplierName, string storePermessionNum, string SupPerm, double discount, double TotalA, double addabtiveTax, double TotalSafy,DateTime returnDate, List<SupplierReturnBill_Items> ReceiptItems)
        {
            Print_SupplierReturnBill report = new Print_SupplierReturnBill();
            foreach(DevExpress.XtraReports.Parameters.Parameter p in report.Parameters)
            {
                p.Visible = false;
            }
            report.InitData(StoreName, PermissionNum, SupplierName, storePermessionNum, SupPerm, discount, TotalA, addabtiveTax, TotalSafy,returnDate, ReceiptItems);
            documentViewer1.DocumentSource = report;
            report.CreateDocument();
        }
    }
}
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
    public partial class Report_Items_Factory2 : DevExpress.XtraEditors.XtraForm
    {
        public Report_Items_Factory2()
        {
            InitializeComponent();
        }

        public void PrintInvoice(DateTime date, double TotalBillPriceAD, List<Items_Factory> BillItems)
        {
            Print_Items_Factory2 report = new Print_Items_Factory2();
            foreach(DevExpress.XtraReports.Parameters.Parameter p in report.Parameters)
            {
                p.Visible = false;
            }
            report.InitData(date, TotalBillPriceAD, BillItems);
            documentViewer1.DocumentSource = report;
            report.CreateDocument();
        }
    }
}
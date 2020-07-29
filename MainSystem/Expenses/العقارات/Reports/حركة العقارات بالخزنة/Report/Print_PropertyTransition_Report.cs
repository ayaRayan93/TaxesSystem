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
    public partial class Print_PropertyTransition_Report : DevExpress.XtraEditors.XtraForm
    {
        public Print_PropertyTransition_Report()
        {
            InitializeComponent();
        }

        public void PrintInvoice(DateTime fromDate, DateTime toDate, string branch_Name, List<ExpensesTransition_Items> BillItems)
        {
            Print_PropertyTransition report = new Print_PropertyTransition();
            foreach(DevExpress.XtraReports.Parameters.Parameter p in report.Parameters)
            {
                p.Visible = false;
            }
            report.InitData(fromDate, toDate, branch_Name, BillItems);
            documentViewer1.DocumentSource = report;
            report.CreateDocument();
        }
    }
}
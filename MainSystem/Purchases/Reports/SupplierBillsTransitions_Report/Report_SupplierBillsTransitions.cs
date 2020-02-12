using System;
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
    public partial class Report_SupplierBillsTransitions : DevExpress.XtraEditors.XtraForm
    {
        public Report_SupplierBillsTransitions()
        {
            InitializeComponent();
        }

        public void PrintInvoice(string SupplierName, string dateFrom, string dateTo, List<SupplierBillsTransitions_Items> ReceiptItems)
        {
            Print_SupplierBillsTransitions report = new Print_SupplierBillsTransitions();
            foreach(DevExpress.XtraReports.Parameters.Parameter p in report.Parameters)
            {
                p.Visible = false;
            }
            report.InitData(SupplierName, dateFrom, dateTo, ReceiptItems);
            documentViewer1.DocumentSource = report;
            report.CreateDocument();
        }
    }
}
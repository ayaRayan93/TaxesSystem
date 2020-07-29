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

namespace TaxesSystem
{
    public partial class Report_SupplierBillsTransitionsDetails : DevExpress.XtraEditors.XtraForm
    {
        public Report_SupplierBillsTransitionsDetails()
        {
            InitializeComponent();
        }

        public void PrintInvoice(string SupplierName, string dateFrom, string dateTo, List<SupplierBillsTransitionsDetails_Items> ReceiptItems)
        {
            Print_SupplierBillsTransitionsDetails report = new Print_SupplierBillsTransitionsDetails();
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
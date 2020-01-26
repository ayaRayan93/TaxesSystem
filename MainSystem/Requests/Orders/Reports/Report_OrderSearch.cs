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
    public partial class Report_OrderSearch : DevExpress.XtraEditors.XtraForm
    {
        public Report_OrderSearch()
        {
            InitializeComponent();
        }

        public void PrintInvoice(List<ReportOrder_Items> ReceiptItems)
        {
            Print_OrderReport report = new Print_OrderReport();
            foreach(DevExpress.XtraReports.Parameters.Parameter p in report.Parameters)
            {
                p.Visible = false;
            }
            report.InitData(ReceiptItems);
            documentViewer1.DocumentSource = report;
            report.CreateDocument();
        }
    }
}
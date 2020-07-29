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
    public partial class Report_Transportation_Bill2 : DevExpress.XtraEditors.XtraForm
    {
        public Report_Transportation_Bill2()
        {
            InitializeComponent();
        }

        public void PrintInvoice(string Store, DateTime dateFrom, DateTime dateTo,List<TransportationBill_Items> ReceiptItems)
        {
            Print_Transportation_Bill2 report = new Print_Transportation_Bill2();
            foreach(DevExpress.XtraReports.Parameters.Parameter p in report.Parameters)
            {
                p.Visible = false;
            }
            report.InitData(Store, dateFrom, dateTo, ReceiptItems);
            documentViewer1.DocumentSource = report;
            report.CreateDocument();
        }
    }
}
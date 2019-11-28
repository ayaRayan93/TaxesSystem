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
    public partial class Report_SupplierReceiptCopy : DevExpress.XtraEditors.XtraForm
    {
        public Report_SupplierReceiptCopy()
        {
            InitializeComponent();
        }

        public void PrintInvoice(string StoreName, string PermissionNum, string SupplierName, DateTime Date, List<SupplierReceipt_Items> ReceiptItems)
        {
            Print_SupplierReceiptCopy report = new Print_SupplierReceiptCopy();
            foreach(DevExpress.XtraReports.Parameters.Parameter p in report.Parameters)
            {
                p.Visible = false;
            }
            report.InitData(StoreName, PermissionNum, SupplierName, Date, ReceiptItems);
            documentViewer1.DocumentSource = report;
            report.CreateDocument();
        }
    }
}
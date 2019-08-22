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
    public partial class Report_StorageReturnCopy : DevExpress.XtraEditors.XtraForm
    {
        public Report_StorageReturnCopy()
        {
            InitializeComponent();
        }

        public void PrintInvoice(string StoreName, string PermissionNum, string SupplierName, int ReturnedPermissionNumber, string reason, List<StorageReturn_Items> ReceiptItems)
        {
            Print_StorageReturnCopy report = new Print_StorageReturnCopy();
            foreach(DevExpress.XtraReports.Parameters.Parameter p in report.Parameters)
            {
                p.Visible = false;
            }
            report.InitData(StoreName, PermissionNum, SupplierName, ReturnedPermissionNumber, reason, ReceiptItems);
            documentViewer1.DocumentSource = report;
            report.CreateDocument();
        }
    }
}
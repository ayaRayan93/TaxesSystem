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
    public partial class Report_SupplierBill : DevExpress.XtraEditors.XtraForm
    {
        public Report_SupplierBill()
        {
            InitializeComponent();
        }

        public void PrintInvoice(string StoreName, string PermissionNum, string SupplierName, double TotalA, double addabtiveTax, List<SupplierBill_Items> ReceiptItems)
        {
            Print_SupplierBill report = new Print_SupplierBill();
            foreach(DevExpress.XtraReports.Parameters.Parameter p in report.Parameters)
            {
                p.Visible = false;
            }
            report.InitData(StoreName, PermissionNum, SupplierName, TotalA, addabtiveTax, ReceiptItems);
            documentViewer1.DocumentSource = report;
            report.CreateDocument();
        }
    }
}
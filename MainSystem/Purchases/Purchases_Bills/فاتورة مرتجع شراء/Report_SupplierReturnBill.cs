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
    public partial class Report_SupplierReturnBill : DevExpress.XtraEditors.XtraForm
    {
        public Report_SupplierReturnBill()
        {
            InitializeComponent();
        }

        public void PrintInvoice(string StoreName, string PermissionNum, string SupplierName, string storePermessionNum, double TotalA, double addabtiveTax, List<SupplierReturnBill_Items> ReceiptItems)
        {
            Print_SupplierReturnBill report = new Print_SupplierReturnBill();
            foreach(DevExpress.XtraReports.Parameters.Parameter p in report.Parameters)
            {
                p.Visible = false;
            }
            report.InitData(StoreName, PermissionNum, SupplierName, storePermessionNum, TotalA, addabtiveTax, ReceiptItems);
            documentViewer1.DocumentSource = report;
            report.CreateDocument();
        }
    }
}
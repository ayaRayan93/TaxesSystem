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
    public partial class Report_SupplierBillCopy : DevExpress.XtraEditors.XtraForm
    {
        public Report_SupplierBillCopy()
        {
            InitializeComponent();
        }

        public void PrintInvoice(string StoreName, string PermissionNum, string SupplierName, string SupPerm, string storePermessionNum, string date, double totalDiscount, double TotalA, double addabtiveTax, List<SupplierBill_Items> ReceiptItems)
        {
            Print_SupplierBillCopy report = new Print_SupplierBillCopy();
            foreach(DevExpress.XtraReports.Parameters.Parameter p in report.Parameters)
            {
                p.Visible = false;
            }
            report.InitData(StoreName, PermissionNum, SupplierName, SupPerm, storePermessionNum, date, totalDiscount, TotalA, addabtiveTax, ReceiptItems);
            documentViewer1.DocumentSource = report;
            report.CreateDocument();
        }
    }
}
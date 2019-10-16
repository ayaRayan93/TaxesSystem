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
    public partial class Report_SupplierReturnBillCopy : DevExpress.XtraEditors.XtraForm
    {
        public Report_SupplierReturnBillCopy()
        {
            InitializeComponent();
        }

        public void PrintInvoice(string StoreName, string PermissionNum, string SupplierName, string storePermessionNum, string SupPerm, string date, double discount, double TotalA, double addabtiveTax, double totalSafy, List<SupplierReturnBill_Items> ReceiptItems)
        {
            Print_SupplierReturnBillCopy report = new Print_SupplierReturnBillCopy();
            foreach(DevExpress.XtraReports.Parameters.Parameter p in report.Parameters)
            {
                p.Visible = false;
            }
            report.InitData(StoreName, PermissionNum, SupplierName, storePermessionNum, SupPerm, date, discount, TotalA, addabtiveTax, totalSafy, ReceiptItems);
            documentViewer1.DocumentSource = report;
            report.CreateDocument();
        }
    }
}
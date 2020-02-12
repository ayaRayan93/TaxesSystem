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
    public partial class Report_PurchaseDetails : DevExpress.XtraEditors.XtraForm
    {
        public Report_PurchaseDetails()
        {
            InitializeComponent();
        }

        public void PrintInvoiceQuantity(string BranchName, DateTime fromDate, DateTime toDate, List<Items_Bills_Details> BillItems)
        {
            Print_PurchaseQuantityDetails report = new Print_PurchaseQuantityDetails();
            foreach(DevExpress.XtraReports.Parameters.Parameter p in report.Parameters)
            {
                p.Visible = false;
            }
            report.InitData(BranchName, fromDate, toDate, BillItems);
            documentViewer1.DocumentSource = report;
            report.CreateDocument();
        }

        public void PrintInvoicePrice(string BranchName, DateTime fromDate, DateTime toDate, List<Items_Bills_Details> BillItems)
        {
            Print_PurchasePriceDetails report = new Print_PurchasePriceDetails();
            foreach (DevExpress.XtraReports.Parameters.Parameter p in report.Parameters)
            {
                p.Visible = false;
            }
            report.InitData(BranchName, fromDate, toDate, BillItems);
            documentViewer1.DocumentSource = report;
            report.CreateDocument();
        }
    }
}
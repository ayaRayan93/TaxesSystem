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
    public partial class Print_Bill_ReportAccounting : DevExpress.XtraEditors.XtraForm
    {
        public Print_Bill_ReportAccounting()
        {
            InitializeComponent();
        }

        public void PrintInvoice(string ClientName, string CustomerName, string phoneNumber, string DelegateName, DateTime billDate, string PayType, int BillNumber, string branchId, string BranchName, double TotalBillPriceBD, double TotalBillPriceAD, double TotalDiscount, List<Bill_ItemsAccounting> BillItems)
        {
            Print_InvoiceAccounting report = new Print_InvoiceAccounting();
            foreach(DevExpress.XtraReports.Parameters.Parameter p in report.Parameters)
            {
                p.Visible = false;
            }
            report.InitData(ClientName, CustomerName, phoneNumber, DelegateName, billDate, PayType, BillNumber, branchId, BranchName, TotalBillPriceBD, TotalBillPriceAD, TotalDiscount, BillItems);
            documentViewer1.DocumentSource = report;
            report.CreateDocument();
        }
    }
}
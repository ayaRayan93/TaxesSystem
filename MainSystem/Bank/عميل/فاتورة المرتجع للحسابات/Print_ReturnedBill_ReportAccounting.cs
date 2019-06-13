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
    public partial class Print_ReturnedBill_ReportAccounting : DevExpress.XtraEditors.XtraForm
    {
        public Print_ReturnedBill_ReportAccounting()
        {
            InitializeComponent();
        }

        public void PrintInvoice(string ClientName, DateTime billDate, string PayType, int BillNumber, string branchId, string BranchName, double TotalBillPriceAD, string returnInfo, List<ReturnedBill_ItemsAccounting> BillItems)
        {
            Print_ReturnedInvoiceAccounting report = new Print_ReturnedInvoiceAccounting();
            foreach(DevExpress.XtraReports.Parameters.Parameter p in report.Parameters)
            {
                p.Visible = false;
            }
            report.InitData(ClientName, billDate, PayType, BillNumber, branchId, BranchName, TotalBillPriceAD, returnInfo, BillItems);
            documentViewer1.DocumentSource = report;
            report.CreateDocument();
        }
    }
}
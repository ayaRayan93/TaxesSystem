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
    public partial class Print_CopyBill_Report : DevExpress.XtraEditors.XtraForm
    {
        public Print_CopyBill_Report()
        {
            InitializeComponent();
        }

        public void PrintInvoice(string ClientName, string phoneNumber, string DelegateName, DateTime billDate, string PayType, int BillNumber, string branchId, string BranchName, double TotalBillPriceBD, double TotalBillPriceAD, double TotalDiscount, List<Copy_Bill_Items> BillItems)
        {
            Print_Copy_Invoice report = new Print_Copy_Invoice();
            foreach(DevExpress.XtraReports.Parameters.Parameter p in report.Parameters)
            {
                p.Visible = false;
            }
            report.InitData(ClientName, phoneNumber, DelegateName, billDate, PayType, BillNumber, branchId, BranchName, TotalBillPriceBD, TotalBillPriceAD, TotalDiscount, BillItems);
            documentViewer1.DocumentSource = report;
            report.CreateDocument();
        }
    }
}
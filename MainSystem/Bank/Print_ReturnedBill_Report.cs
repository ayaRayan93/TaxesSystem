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
    public partial class Print_ReturnedBill_Report : DevExpress.XtraEditors.XtraForm
    {
        public Print_ReturnedBill_Report()
        {
            InitializeComponent();
        }

        public void PrintInvoice(string ClientName, DateTime billDate, string PayType, int BillNumber, string branchId, string BranchName, double TotalBillPriceAD, List<ReturnedBill_Items> BillItems)
        {
            Print_ReturnedInvoice report = new Print_ReturnedInvoice();
            foreach(DevExpress.XtraReports.Parameters.Parameter p in report.Parameters)
            {
                p.Visible = false;
            }
            report.InitData(ClientName, billDate, PayType, BillNumber, branchId, BranchName, TotalBillPriceAD, BillItems);
            documentViewer1.DocumentSource = report;
            report.CreateDocument();
        }
    }
}
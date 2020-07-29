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
    public partial class Print_ReturnedBill_Report : DevExpress.XtraEditors.XtraForm
    {
        public Print_ReturnedBill_Report()
        {
            InitializeComponent();
        }

        public void PrintInvoice(string ClientName, string CustomerName, string phoneNumber, DateTime billDate, string PayType, int BillNumber, string branchId, string BranchName, double TotalBillPriceAD, string returnInfo, List<ReturnedBill_Items> BillItems,string Delegate_Name)
        {
            Print_ReturnedInvoice report = new Print_ReturnedInvoice();
            foreach(DevExpress.XtraReports.Parameters.Parameter p in report.Parameters)
            {
                p.Visible = false;
            }
            report.InitData(ClientName, CustomerName, phoneNumber, billDate, PayType, BillNumber, branchId, BranchName, TotalBillPriceAD, returnInfo, BillItems, Delegate_Name);
            documentViewer1.DocumentSource = report;
            report.CreateDocument();
        }
    }
}
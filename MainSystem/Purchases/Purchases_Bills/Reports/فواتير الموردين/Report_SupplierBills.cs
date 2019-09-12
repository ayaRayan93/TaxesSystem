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
    public partial class Report_SupplierBills : DevExpress.XtraEditors.XtraForm
    {
        public Report_SupplierBills()
        {
            InitializeComponent();
        }

        public void PrintInvoice(string TotalBills, string TotalReturns, string Safy, List<SupplierBills_Items> ReceiptItems)
        {
            Print_SupplierBills report = new Print_SupplierBills();
            foreach(DevExpress.XtraReports.Parameters.Parameter p in report.Parameters)
            {
                p.Visible = false;
            }
            report.InitData(TotalBills, TotalReturns, Safy, ReceiptItems);
            documentViewer1.DocumentSource = report;
            report.CreateDocument();
        }
    }
}
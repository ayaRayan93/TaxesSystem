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
    public partial class Report_PurchaseQuantityDetails : DevExpress.XtraEditors.XtraForm
    {
        public Report_PurchaseQuantityDetails()
        {
            InitializeComponent();
        }

        public void PrintInvoice(string BranchName, string factoryName, DateTime fromDate, DateTime toDate, List<Items_Bills_Details> BillItems)
        {
            Print_ProductsBillsDetails report = new Print_ProductsBillsDetails();
            foreach(DevExpress.XtraReports.Parameters.Parameter p in report.Parameters)
            {
                p.Visible = false;
            }
            report.InitData(BranchName, factoryName, fromDate, toDate, BillItems);
            documentViewer1.DocumentSource = report;
            report.CreateDocument();
        }
    }
}
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
    public partial class Report_Items_BillsNum : DevExpress.XtraEditors.XtraForm
    {
        public Report_Items_BillsNum()
        {
            InitializeComponent();
        }

        public void PrintInvoice(string BranchName, string fromBill, string toBill, List<Items_Bills> BillItems)
        {
            Print_ProductsBillsNum report = new Print_ProductsBillsNum();
            foreach(DevExpress.XtraReports.Parameters.Parameter p in report.Parameters)
            {
                p.Visible = false;
            }
            report.InitData(BranchName, fromBill, toBill, BillItems);
            documentViewer1.DocumentSource = report;
            report.CreateDocument();
        }
    }
}
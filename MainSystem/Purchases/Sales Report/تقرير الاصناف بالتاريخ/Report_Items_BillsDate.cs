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
    public partial class Report_Items_BillsDate : DevExpress.XtraEditors.XtraForm
    {
        public Report_Items_BillsDate()
        {
            InitializeComponent();
        }

        public void PrintInvoice(string BranchName, List<Items_BillsDate> BillItems)
        {
            Print_ProductsBillsDate report = new Print_ProductsBillsDate();
            foreach(DevExpress.XtraReports.Parameters.Parameter p in report.Parameters)
            {
                p.Visible = false;
            }
            report.InitData(BranchName, BillItems);
            documentViewer1.DocumentSource = report;
            report.CreateDocument();
        }
    }
}
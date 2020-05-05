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
    public partial class Print_PayType_Report : DevExpress.XtraEditors.XtraForm
    {
        public Print_PayType_Report()
        {
            InitializeComponent();
        }

        public void PrintInvoice(DateTime fromDate, DateTime toDate, string branch_Name, string payType, List<Transition_Items> BillItems)
        {
            Print_PayTypeTransition report = new Print_PayTypeTransition();
            foreach(DevExpress.XtraReports.Parameters.Parameter p in report.Parameters)
            {
                p.Visible = false;
            }
            report.InitData(fromDate, toDate, branch_Name, payType, BillItems);
            documentViewer1.DocumentSource = report;
            report.CreateDocument();
        }
    }
}
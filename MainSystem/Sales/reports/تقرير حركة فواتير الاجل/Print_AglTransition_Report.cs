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
    public partial class Print_AglTransition_Report : DevExpress.XtraEditors.XtraForm
    {
        public Print_AglTransition_Report()
        {
            InitializeComponent();
        }

        public void PrintInvoice(DateTime fromDate, DateTime toDate, List<Transition_Items> BillItems)
        {
            Print_AglTransition report = new Print_AglTransition();
            foreach(DevExpress.XtraReports.Parameters.Parameter p in report.Parameters)
            {
                p.Visible = false;
            }
            report.InitData(fromDate, toDate, BillItems);
            documentViewer1.DocumentSource = report;
            report.CreateDocument();
        }
    }
}
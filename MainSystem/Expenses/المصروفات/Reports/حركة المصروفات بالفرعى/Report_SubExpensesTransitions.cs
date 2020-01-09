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
    public partial class Report_SubExpensesTransitions : DevExpress.XtraEditors.XtraForm
    {
        public Report_SubExpensesTransitions()
        {
            InitializeComponent();
        }

        public void PrintInvoice(string Main, string Sub, string Safe, DateTime fromDate, DateTime toDate, List<Item_SubExpensesTransitions> BillItems)
        {
            Print_SubExpensesTransitions report = new Print_SubExpensesTransitions();
            foreach(DevExpress.XtraReports.Parameters.Parameter p in report.Parameters)
            {
                p.Visible = false;
            }
            report.InitData(Main, Sub, Safe, fromDate, toDate, BillItems);
            documentViewer1.DocumentSource = report;
            report.CreateDocument();
        }
    }
}
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
    public partial class Report_Transportation_Copy : DevExpress.XtraEditors.XtraForm
    {
        public Report_Transportation_Copy()
        {
            InitializeComponent();
        }

        public void PrintInvoice(int transferProductID, string FromStore, string ToStore, string date,List<Transportation_Items> ReceiptItems)
        {
            Print_Transportation_Copy report = new Print_Transportation_Copy();
            foreach(DevExpress.XtraReports.Parameters.Parameter p in report.Parameters)
            {
                p.Visible = false;
            }
            report.InitData(transferProductID, FromStore, ToStore, date, ReceiptItems);
            documentViewer1.DocumentSource = report;
            report.CreateDocument();
        }
    }
}
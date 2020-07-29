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
    public partial class Report_LeastQuantity : DevExpress.XtraEditors.XtraForm
    {
        public Report_LeastQuantity()
        {
            InitializeComponent();
        }

        public void PrintInvoice(List<LeastQuantity_Items> ReceiptItems)
        {
            Print_LeastQuantity report = new Print_LeastQuantity();
            foreach(DevExpress.XtraReports.Parameters.Parameter p in report.Parameters)
            {
                p.Visible = false;
            }
            report.InitData(ReceiptItems);
            documentViewer1.DocumentSource = report;
            report.CreateDocument();
        }
    }
}
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
    public partial class Print_Ticket_Report : DevExpress.XtraEditors.XtraForm
    {
        public Print_Ticket_Report()
        {
            InitializeComponent();
        }

        public void PrintInvoice(List<Ticket_Items> BillItems)
        {
            Print_Product_Ticket report = new Print_Product_Ticket();
            foreach(DevExpress.XtraReports.Parameters.Parameter p in report.Parameters)
            {
                p.Visible = false;
            }
            report.InitData(BillItems);
            documentViewer1.DocumentSource = report;
            report.CreateDocument();
        }
    }
}
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
    public partial class Report_Inventory : DevExpress.XtraEditors.XtraForm
    {
        public Report_Inventory()
        {
            InitializeComponent();
        }

        public void PrintInvoice(List<Inventory_Items> ReceiptItems)
        {
            Print_Inventory report = new Print_Inventory();
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
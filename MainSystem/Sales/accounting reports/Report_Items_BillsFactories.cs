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
    public partial class Report_Items_BillsFactories : DevExpress.XtraEditors.XtraForm
    {
        public Report_Items_BillsFactories()
        {
            InitializeComponent();
        }

        public void PrintInvoice(string factoryName, DateTime fromDate, DateTime toDate, List<Items_Bills> BillItems)
        {
            Print_ProductsBillsFactories report = new Print_ProductsBillsFactories();
            foreach(DevExpress.XtraReports.Parameters.Parameter p in report.Parameters)
            {
                p.Visible = false;
            }
            report.InitData(factoryName, fromDate, toDate, BillItems);
            documentViewer1.DocumentSource = report;
            report.CreateDocument();
        }
    }
}
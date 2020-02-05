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
    public partial class Report_SupplierType : DevExpress.XtraEditors.XtraForm
    {
        public Report_SupplierType()
        {
            InitializeComponent();
        }

        public void PrintInvoice(string SupplierName, string FactoryName, string dateFrom, string dateTo, List<SupplierType_Items> ReceiptItems)
        {
            Print_SupplierType report = new Print_SupplierType();
            foreach(DevExpress.XtraReports.Parameters.Parameter p in report.Parameters)
            {
                p.Visible = false;
            }
            report.InitData(SupplierName, FactoryName, dateFrom, dateTo, ReceiptItems);
            documentViewer1.DocumentSource = report;
            report.CreateDocument();
        }
    }
}
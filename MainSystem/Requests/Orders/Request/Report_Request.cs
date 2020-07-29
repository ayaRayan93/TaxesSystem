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
    public partial class Report_Request : DevExpress.XtraEditors.XtraForm
    {
        public Report_Request()
        {
            InitializeComponent();
        }

        public void PrintInvoice( string FactoryName, int RequestNum, string EmployeeName,List<Order_Items> ReceiptItems)
        {
            Print_Order report = new Print_Order();
            foreach(DevExpress.XtraReports.Parameters.Parameter p in report.Parameters)
            {
                p.Visible = false;
            }
            report.InitData(FactoryName, RequestNum, EmployeeName, ReceiptItems);
            documentViewer1.DocumentSource = report;
            report.CreateDocument();
        }
    }
}
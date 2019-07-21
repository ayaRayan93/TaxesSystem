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
    public partial class Report_Item_Transitions : DevExpress.XtraEditors.XtraForm
    {
        public Report_Item_Transitions()
        {
            InitializeComponent();
        }

        public void PrintInvoice(string Store, DateTime FromDate, DateTime ToDate, string productName, double TotalBills, double TotalReturn, double Safy, List<Item_Transitions> BillItems)
        {
            Print_Item_Transitions report = new Print_Item_Transitions();
            foreach(DevExpress.XtraReports.Parameters.Parameter p in report.Parameters)
            {
                p.Visible = false;
            }
            report.InitData(Store, FromDate, ToDate, productName, TotalBills, TotalReturn, Safy, BillItems);
            documentViewer1.DocumentSource = report;
            report.CreateDocument();
        }
    }
}
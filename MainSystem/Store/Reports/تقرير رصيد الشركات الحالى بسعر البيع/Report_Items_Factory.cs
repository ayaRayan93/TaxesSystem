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
using TaxesSystem.Store.Reports.تقرير_اصناف_الشركات;

namespace TaxesSystem
{
    public partial class Report_Items_Factory : DevExpress.XtraEditors.XtraForm
    {
        public Report_Items_Factory()
        {
            InitializeComponent();
        }

        public void PrintInvoice(string StoreId, string StoreName, double TotalBillPriceAD, List<Items_Factory> BillItems)
        {
            Print_Items_Factory report = new Print_Items_Factory();
            foreach(DevExpress.XtraReports.Parameters.Parameter p in report.Parameters)
            {
                p.Visible = false;
            }
            report.InitData(StoreId, StoreName, TotalBillPriceAD, BillItems);
            documentViewer1.DocumentSource = report;
            report.CreateDocument();
        }
        public void PrintInvoiceQuantity(string StoreId, string StoreName, double TotalBillPriceAD, List<Items_Factory> BillItems)
        {
            Print_Items_Storage2 report = new Print_Items_Storage2();
            foreach (DevExpress.XtraReports.Parameters.Parameter p in report.Parameters)
            {
                p.Visible = false;
            }
            report.InitData(StoreId, StoreName, TotalBillPriceAD, BillItems);
            documentViewer1.DocumentSource = report;
            report.CreateDocument();
        }
        public void PrintReport( List<Data> listData)
        {
            XtraReportStorage report = new XtraReportStorage();
            
            report.InitData(listData);
            documentViewer1.DocumentSource = report;
            report.CreateDocument();
        }
    }
}
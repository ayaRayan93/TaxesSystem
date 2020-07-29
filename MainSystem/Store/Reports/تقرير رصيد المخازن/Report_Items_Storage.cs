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
    public partial class Report_Items_Storage : DevExpress.XtraEditors.XtraForm
    {
        public Report_Items_Storage()
        {
            InitializeComponent();
        }

        public void PrintInvoice(string storeName, DateTime date, double TotalBillPriceAD, List<Items_Storage> BillItems)
        {
            Print_Items_Storage report = new Print_Items_Storage();
            foreach(DevExpress.XtraReports.Parameters.Parameter p in report.Parameters)
            {
                p.Visible = false;
            }
            report.InitData(storeName, date, TotalBillPriceAD, BillItems);
            documentViewer1.DocumentSource = report;
            report.CreateDocument();
        }
        public void PrintInvoice(string storeName, DateTime date, double TotalBillPriceAD, double TotalPurchases, List<Items_StorageWithPurshasesPrice> BillItems)
        {
            Print_Items_StorageWithPurchasesPrice report = new Print_Items_StorageWithPurchasesPrice();
            foreach (DevExpress.XtraReports.Parameters.Parameter p in report.Parameters)
            {
                p.Visible = false;
            }
            report.InitData(storeName, date, TotalBillPriceAD, TotalPurchases, BillItems);
            documentViewer1.DocumentSource = report;
            report.CreateDocument();
        }
    }
}
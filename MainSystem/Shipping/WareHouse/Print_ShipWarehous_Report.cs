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
    public partial class Print_ShipWarehous_Report : DevExpress.XtraEditors.XtraForm
    {
        public Print_ShipWarehous_Report()
        {
            InitializeComponent();
        }

        public void PrintInvoice(int ShippingID, int BillNum, string BranchName, string Customer, string Client, string ReceivedClient, string Area, string Address, string Phone, string Delegate, DateTime dateBill, DateTime dateReceived, string Quantity, string Cartons, string Bank, double Money)
        {
            Print_ShippingWareHouse report = new Print_ShippingWareHouse();
            foreach(DevExpress.XtraReports.Parameters.Parameter p in report.Parameters)
            {
                p.Visible = false;
            }
            report.InitData(ShippingID, BillNum, BranchName, Customer, Client, ReceivedClient, Area, Address, Phone, Delegate, dateBill, dateReceived, Quantity, Cartons, Bank, Money);
            documentViewer1.DocumentSource = report;
            report.CreateDocument();
        }
    }
}
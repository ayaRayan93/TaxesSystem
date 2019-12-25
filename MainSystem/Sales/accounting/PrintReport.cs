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

namespace MainSystem.Sales.accounting
{
    public partial class PrintReport : DevExpress.XtraEditors.XtraForm
    {
        List<TransitionData> Bill_Items;
        List<customerAccount> Report_Items;
        string name,from,to;
        int flag ;
        double befor, totalBills, totalReturns, safay1,totalPaid;
        public PrintReport(List<TransitionData> Bill_Items, string name,int flag, double befor, double totalBills, double totalReturns, double safay1,string from,string to)
        {
            InitializeComponent();
            this.Bill_Items = Bill_Items;
            this.name = name;
            this.flag = flag;
            this.befor = befor;
            this.totalBills = totalBills;
            this.totalReturns = totalReturns;
            this.safay1 = safay1;
            this.from = from;
            this.to = to;
        }

        public PrintReport(List<customerAccount> Report_Items, int flag, double befor, double totalBills, double totalPaid, double safay1, string from, string to)
        {
            InitializeComponent();
            this.Report_Items = Report_Items;
            this.flag = flag;
            this.befor = befor;
            this.totalBills = totalBills;
            this.totalPaid = totalPaid;
            this.safay1 = safay1;
            this.from = from;
            this.to = to;
        }

        private void PrintReport_Load(object sender, EventArgs e)
        {
            switch (flag)
            {
                case 1:
                    XtraPrintReport t1 = new XtraPrintReport();
                    t1.InitData(Bill_Items, name, befor, totalBills, totalReturns, safay1, from, to);
                    documentViewer1.DocumentSource = t1;
                    t1.CreateDocument();
                    break;
                case 2:
                    XtraPrintReportBills t2 = new XtraPrintReportBills();
                    t2.InitData(Bill_Items, name, befor, totalBills, totalReturns, safay1, from, to);
                    documentViewer1.DocumentSource = t2;
                    t2.CreateDocument();
                    break;
                case 3:
                    XtraReportCustomerAccount t3 = new XtraReportCustomerAccount();
                    t3.InitData(Report_Items, befor, totalBills, totalPaid, safay1, from, to);
                    documentViewer1.DocumentSource = t3;
                    t3.CreateDocument();
                    break;
            }
         
        }
    }
}
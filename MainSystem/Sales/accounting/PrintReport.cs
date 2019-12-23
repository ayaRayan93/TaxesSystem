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
        string name;
        bool flag = true;
        double befor, totalBills, totalReturns, safay1;
        public PrintReport(List<TransitionData> Bill_Items, string name,bool flag, double befor, double totalBills, double totalReturns, double safay1)
        {
            InitializeComponent();
            this.Bill_Items = Bill_Items;
            this.name = name;
            this.flag = flag;
            this.befor = befor;
            this.totalBills = totalBills;
            this.totalReturns = totalReturns;
            this.safay1 = safay1;
        }

        private void PrintReport_Load(object sender, EventArgs e)
        {
            if (flag)
            {
                XtraPrintReport tt = new XtraPrintReport();
                tt.InitData(Bill_Items, name, befor, totalBills, totalReturns, safay1);
                documentViewer1.DocumentSource = tt;
                tt.CreateDocument();
            }
            else
            {
                XtraPrintReportBills tt = new XtraPrintReportBills();
                tt.InitData(Bill_Items, name, befor, totalBills, totalReturns, safay1);
                documentViewer1.DocumentSource = tt;
                tt.CreateDocument();
            }
        }
    }
}
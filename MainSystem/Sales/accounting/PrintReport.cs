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
        public PrintReport(List<TransitionData> Bill_Items, string name,bool flag)
        {
            InitializeComponent();
            this.Bill_Items = Bill_Items;
            this.name = name;
            this.flag = flag;
        }

        private void PrintReport_Load(object sender, EventArgs e)
        {
            if (flag)
            {
                XtraPrintReport tt = new XtraPrintReport();
                tt.InitData(Bill_Items, name);
                documentViewer1.DocumentSource = tt;
                tt.CreateDocument();
            }
            else
            {
                XtraPrintReportBills tt = new XtraPrintReportBills();
                tt.InitData(Bill_Items, name);
                documentViewer1.DocumentSource = tt;
                tt.CreateDocument();
            }
        }
    }
}
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
        public PrintReport(List<TransitionData> Bill_Items)
        {
            InitializeComponent();
            this.Bill_Items = Bill_Items;
        }

        private void PrintReport_Load(object sender, EventArgs e)
        {
            XtraReportTransition tt = new XtraReportTransition();
            tt.InitData(Bill_Items);
            documentViewer1.DocumentSource = tt;
            tt.CreateDocument();
        }
    }
}
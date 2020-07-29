using System;
using System.Drawing;
using System.Collections;
using DevExpress.XtraReports.UI;

using System.Collections.Generic;
using TaxesSystem.Store.Reports.تقرير_اصناف_الشركات;

namespace TaxesSystem
{
    public partial class XtraReportStorage : DevExpress.XtraReports.UI.XtraReport
    {
        public XtraReportStorage()
        {
            InitializeComponent();
        }
        public void InitData(List<Data> d)
        {
            objectDataSource1.DataSource = d;
        
        }
    }
}

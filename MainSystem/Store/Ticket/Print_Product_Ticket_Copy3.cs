using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Collections.Generic;

namespace MainSystem
{
    public partial class Print_Product_Ticket_Copy3 : DevExpress.XtraReports.UI.XtraReport
    {
        public Print_Product_Ticket_Copy3()
        {
            InitializeComponent();
        }

        public void InitData(List<Ticket_Items> Bill_Items)
        {
            objectDataSource1.DataSource = Bill_Items;
        }
    }
}

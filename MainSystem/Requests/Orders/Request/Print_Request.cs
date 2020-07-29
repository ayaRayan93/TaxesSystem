using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Collections.Generic;

namespace TaxesSystem
{
    public partial class Print_Request : DevExpress.XtraReports.UI.XtraReport
    {
        public Print_Request()
        {
            InitializeComponent();
        }

        public void InitData(string factoryName, int requestNum, string employeeName,List<Order_Items> ReceiptItems)
        {
            FactoryName.Value = factoryName;
            OrderNumber.Value = requestNum;
            DateNow.Value = DateTime.Now;
            EmployeeName.Value = employeeName;
            objectDataSource1.DataSource = ReceiptItems;
        }
    }
}

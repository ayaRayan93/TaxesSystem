using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Collections.Generic;

namespace MainSystem
{
    public partial class Print_SupplierType : DevExpress.XtraReports.UI.XtraReport
    {
        public Print_SupplierType()
        {
            InitializeComponent();
        }

        public void InitData(string supplierName, string factoryName, string dateFrom, string dateTo,double totalq,double totalc, List<SupplierType_Items> ReceiptItems)
        {
            SupplierName.Value = supplierName;
            FactoryName.Value = factoryName;
            DateFrom.Value = dateFrom;
            DateTo.Value = dateTo;
            TotalQuantity.Value = totalq;
            TotalCost.Value = totalc;
            objectDataSource1.DataSource = ReceiptItems;
        }
    }
}

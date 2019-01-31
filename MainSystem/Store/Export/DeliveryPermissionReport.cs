using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Collections.Generic;

namespace MainSystem
{
    public partial class DeliveryPermissionReport : DevExpress.XtraReports.UI.XtraReport
    {

        public DeliveryPermissionReport(List<DeliveryPermissionClass> listOfData)
        {
            try
            {
                InitializeComponent();
                DataSource = listOfData;
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
        }

    }
}

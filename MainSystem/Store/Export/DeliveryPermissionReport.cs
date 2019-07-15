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

        public DeliveryPermissionReport(List<DeliveryPermissionClass> listOfData, string customerName, string customerPhone, string delegateName, string date, string PerNum, string branchId, string branchName)
        {
            try
            {
                InitializeComponent();
                DataSource = listOfData;
                PermissionNumber.Value = PerNum;
                Client_Name.Value = customerName;
                PhoneNum.Value = customerPhone;
                Bill_Date.Value = date;
                Delegate_Name.Value = delegateName;
                Branch.Value = branchName;
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
        }

    }
}

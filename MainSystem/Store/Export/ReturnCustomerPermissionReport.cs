using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Collections.Generic;

namespace TaxesSystem
{
    public partial class ReturnCustomerPermissionReport : DevExpress.XtraReports.UI.XtraReport
    {
        public ReturnCustomerPermissionReport(List<ReturnPermissionClass> listOfData, string PerNum,string clientName,string clientPhone,string returnDate,string returnReason)
        {
            try
            {
                InitializeComponent();
                DataSource = listOfData;
                PermissionNumber.Value = PerNum;
                ClientName.Value = clientName;
                ClientPhone.Value = clientPhone;
                ReturnDate.Value = returnDate;
                ReturnReason.Value = returnReason;
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
        }

    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MainSystem
{
    public partial class ReturnPermissionReportViewer : Form
    {
        List<ReturnPermissionClass> listOfData;
        string PerNum = "", clientName="", clientPhone="", returnDate="", returnReason="";
        public ReturnPermissionReportViewer(List<ReturnPermissionClass> listOfData, string PerNum, string clientName, string clientPhone, string returnDate, string returnReason)
        {
            InitializeComponent();
            this.listOfData = new List<ReturnPermissionClass>();
            this.listOfData = listOfData;
            this.PerNum = PerNum;
            this.clientName = clientName;
            this.clientPhone = clientPhone;
            this.returnDate = returnDate;
            this.returnReason = returnReason;
        }

        private void ReportViewer_Load(object sender, EventArgs e)
        {
            try
            {
                ReturnCustomerPermissionReport DeliveryPermissionReport = new ReturnCustomerPermissionReport(listOfData, PerNum, clientName,clientPhone,returnDate, returnReason);
                documentViewer1.DocumentSource = DeliveryPermissionReport;
                DeliveryPermissionReport.CreateDocument();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}

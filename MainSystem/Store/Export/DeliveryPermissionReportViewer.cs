using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MainSystem.Store.Export
{
    public partial class DeliveryPermissionReportViewer : Form
    {
        List<DeliveryPermissionClass> listOfData;
        string PerNum = "" , customerName="", customerPhone="", delegateName="", date="", branchId="", branchName="";
        public DeliveryPermissionReportViewer(List<DeliveryPermissionClass> listOfData,string customerName,string customerPhone,string delegateName,string date, string PerNum,string branchId,string branchName )
        {
            InitializeComponent();
            this.listOfData = new List<DeliveryPermissionClass>();
            this.listOfData = listOfData;
            this.PerNum = PerNum;
            this.customerName = customerName;
            this.customerPhone = customerPhone;
            this.delegateName = delegateName;
            this.date = date;
            this.branchId = branchId;
            this.branchName = branchName;
        }

        private void ReportViewer_Load(object sender, EventArgs e)
        {
            try
            {
                DeliveryPermissionReport DeliveryPermissionReport = new DeliveryPermissionReport(listOfData,customerName,customerPhone,delegateName,date, PerNum,branchId,branchName);
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

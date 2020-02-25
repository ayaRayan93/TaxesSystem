using DevExpress.XtraReports.UI;
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
        string BranchBillNumber="", PerNum = "" , customerName="", customerPhone="", delegateName="", date="", branchId="", branchName="", storeKeeper="", customerdelivery="", store_Name="",customerAddress="", customerdeliveryPhone="";
        bool flag = false;
        public DeliveryPermissionReportViewer(List<DeliveryPermissionClass> listOfData,string customerName,string customerPhone,string delegateName,string date, string BranchBillNumber, string PerNum, string branchId,string branchName, string storeKeeper, string customerdelivery, string customerdeliveryPhone, string store_Name,bool flag,string CustomerAddress)
        {
            InitializeComponent();
            this.listOfData = new List<DeliveryPermissionClass>();
            this.listOfData = listOfData;
            this.PerNum = PerNum;
            this.BranchBillNumber = BranchBillNumber;
            this.customerName = customerName;
            this.customerPhone = customerPhone;
            this.delegateName = delegateName;
            this.date = date;
            this.branchId = branchId;
            this.branchName = branchName;
            this.storeKeeper = storeKeeper;
            this.customerdelivery = customerdelivery;
            this.store_Name = store_Name;
            this.flag = flag;
            this.customerAddress = CustomerAddress;
            this.customerdeliveryPhone = customerdeliveryPhone;
        }

        private void ReportViewer_Load(object sender, EventArgs e)
        {
            try
            {
                DeliveryPermissionReport DeliveryPermissionReport = new DeliveryPermissionReport(listOfData,customerName,customerPhone,delegateName,date, BranchBillNumber, PerNum,branchId,branchName, storeKeeper, customerdelivery,customerdeliveryPhone, store_Name,flag,customerAddress);
                documentViewer1.DocumentSource = DeliveryPermissionReport;
                XtraReport1 report = new XtraReport1();
                ReportPrintTool printTool = new ReportPrintTool(DeliveryPermissionReport);
                // Invoke the Print dialog.
                printTool.PrintDialog();
                // Send the report to the default printer.
                printTool.Print();
                // Send the report to the specified printer.
                printTool.Print("myPrinter");
              //  DeliveryPermissionReport.CreateDocument();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}

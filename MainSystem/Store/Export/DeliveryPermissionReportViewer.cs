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
        string PerNum = "";
        public DeliveryPermissionReportViewer(List<DeliveryPermissionClass> listOfData,string PerNum)
        {
            InitializeComponent();
            this.listOfData = new List<DeliveryPermissionClass>();
            this.listOfData = listOfData;
            this.PerNum = PerNum;
        }

        private void ReportViewer_Load(object sender, EventArgs e)
        {
            try
            {
                DeliveryPermissionReport DeliveryPermissionReport = new DeliveryPermissionReport(listOfData, PerNum);
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

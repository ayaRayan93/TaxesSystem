using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace TaxesSystem
{
    public partial class Print_Design_Report : DevExpress.XtraEditors.XtraForm
    {
        public Print_Design_Report()
        {
            InitializeComponent();
        }

        public void PrintInvoice(DateTime dateNow, int customerDesignID, string TransitionID, string branchName, string clientName, double PaidMoney, string PaymentMethod, string bank, string CheckNumber, string Payday, string OperationNumber, string Description, string BankUserName, string DelegateName, string NoItemBath, string CostBath, string TotalBath, string NoItemKitchen, string CosKitchen, string TotalKitchen, string NoItemHall, string CostHall, string TotalHall, string NoItemRoom, string CostRoom, string TotalRoom, string NoItemOther, string CostOther, string TotalOther)
        {
            DesignPrint report = new DesignPrint();
            foreach(DevExpress.XtraReports.Parameters.Parameter p in report.Parameters)
            {
                p.Visible = false;
            }
            report.InitData(dateNow, customerDesignID, TransitionID, branchName, clientName, PaidMoney, PaymentMethod, bank, CheckNumber, Payday, OperationNumber, Description, BankUserName, DelegateName, NoItemBath, CostBath, TotalBath, NoItemKitchen, CosKitchen, TotalKitchen, NoItemHall, CostHall, TotalHall, NoItemRoom, CostRoom, TotalRoom, NoItemOther, CostOther, TotalOther);
            documentViewer1.DocumentSource = report;
            report.CreateDocument();
        }
    }
}
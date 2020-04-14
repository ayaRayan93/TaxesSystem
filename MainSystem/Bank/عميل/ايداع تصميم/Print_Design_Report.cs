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

namespace MainSystem
{
    public partial class Print_Design_Report : DevExpress.XtraEditors.XtraForm
    {
        public Print_Design_Report()
        {
            InitializeComponent();
        }

        public void PrintInvoice(DateTime dateNow, int customerDesignID, string TransitionID, string branchName, string clientName, double PaidMoney, string PaymentMethod, string bank, string CheckNumber, string Payday, string OperationNumber, string Description, string BankUserName, string EngDesign, string DelegateName, string SpaceBath, string NoItemBath, string CostBath, string TotalBath, string SpaceKitchen, string NoItemKitchen, string CosKitchen, string TotalKitchen, string SpaceHall, string NoItemHall, string CostHall, string TotalHall, string SpaceRoom, string NoItemRoom, string CostRoom, string TotalRoom, string SpaceOther, string NoItemOther, string CostOther, string TotalOther)
        {
            DesignPrint report = new DesignPrint();
            foreach(DevExpress.XtraReports.Parameters.Parameter p in report.Parameters)
            {
                p.Visible = false;
            }
            report.InitData(dateNow, customerDesignID, TransitionID, branchName, clientName, PaidMoney, PaymentMethod, bank, CheckNumber, Payday, OperationNumber, Description, BankUserName, EngDesign, DelegateName, SpaceBath, NoItemBath, CostBath, TotalBath, SpaceKitchen, NoItemKitchen, CosKitchen, TotalKitchen, SpaceHall, NoItemHall, CostHall, TotalHall, SpaceRoom, NoItemRoom, CostRoom, TotalRoom, SpaceOther, NoItemOther, CostOther, TotalOther);
            documentViewer1.DocumentSource = report;
            report.CreateDocument();
        }
    }
}
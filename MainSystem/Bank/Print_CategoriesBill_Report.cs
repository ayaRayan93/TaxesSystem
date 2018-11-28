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
    public partial class Print_CategoriesBill_Report : DevExpress.XtraEditors.XtraForm
    {
        public Print_CategoriesBill_Report()
        {
            InitializeComponent();
        }

        public void PrintInvoice(DateTime dateNow, string TransitionID, string branchName, int billNumber, string clientName, DateTime billDate, double PaidMoney, string PaymentMethod, string bank, string CheckNumber, string Payday, string VisaType, string OperationNumber, string Description, string ConfirmEmp, string BankUserName, int q200, int q100, int q50, int q20, int q10, int q5, int q1, int qH, int qQ)
        {
            BillCash report = new BillCash();
            foreach(DevExpress.XtraReports.Parameters.Parameter p in report.Parameters)
            {
                p.Visible = false;
            }
            report.InitData(dateNow, TransitionID, branchName, billNumber, clientName, billDate, PaidMoney, PaymentMethod, bank, CheckNumber, Payday, VisaType, OperationNumber, Description, ConfirmEmp, BankUserName, q200, q100, q50, q20, q10, q5, q1, qH, qQ);
            documentViewer1.DocumentSource = report;
            report.CreateDocument();
        }
    }
}
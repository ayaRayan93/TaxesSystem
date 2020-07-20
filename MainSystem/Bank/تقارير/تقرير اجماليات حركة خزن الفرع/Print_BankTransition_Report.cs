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
    public partial class Print_BankTransition_Report : DevExpress.XtraEditors.XtraForm
    {
        public Print_BankTransition_Report()
        {
            InitializeComponent();
        }

        public void PrintInvoice(string Safe, DateTime DateFrom, DateTime DateTo, string Sales, string Returned, string IncomeExpenses, string Expenses, string pullExpense, string TransferTo, string TransferFrom, string TotalAdd, string TotalSub, string Safy)
        {
            BankTransition report = new BankTransition();
            foreach(DevExpress.XtraReports.Parameters.Parameter p in report.Parameters)
            {
                p.Visible = false;
            }
            report.InitData(Safe, DateFrom, DateTo, Sales, Returned, IncomeExpenses, Expenses, pullExpense, TransferTo, TransferFrom, TotalAdd, TotalSub, Safy);
            documentViewer1.DocumentSource = report;
            report.CreateDocument();
        }
    }
}
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
    public partial class Print_Expense_Report_Copy : DevExpress.XtraEditors.XtraForm
    {
        public Print_Expense_Report_Copy()
        {
            InitializeComponent();
        }

        public void PrintInvoice(int ExpenseTransitionID, string Main, string Sub, string Bank, string PullMoney, string Client, string Descrip, string date)
        {
            Print_Expense2_Copy report = new Print_Expense2_Copy();
            foreach(DevExpress.XtraReports.Parameters.Parameter p in report.Parameters)
            {
                p.Visible = false;
            }
            report.InitData(ExpenseTransitionID, Main, Sub, Bank, PullMoney, Client, Descrip, date);
            documentViewer1.DocumentSource = report;
            report.CreateDocument();
        }
    }
}
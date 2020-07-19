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
    public partial class PrintCopy_PullExpense_Report : DevExpress.XtraEditors.XtraForm
    {
        public PrintCopy_PullExpense_Report()
        {
            InitializeComponent();
        }

        public void PrintInvoice(int ExpenseTransitionID,string ExpenseNum, string Bank, string PullMoney, string Client, string Descrip, string Date)
        {
            PrintCopy_PullExpense report = new PrintCopy_PullExpense();
            foreach(DevExpress.XtraReports.Parameters.Parameter p in report.Parameters)
            {
                p.Visible = false;
            }
            report.InitData(ExpenseTransitionID, ExpenseNum, Bank, PullMoney, Client, Descrip, Date);
            documentViewer1.DocumentSource = report;
            report.CreateDocument();
        }
    }
}
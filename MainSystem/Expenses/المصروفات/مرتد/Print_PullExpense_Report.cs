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
    public partial class Print_PullExpense_Report : DevExpress.XtraEditors.XtraForm
    {
        public Print_PullExpense_Report()
        {
            InitializeComponent();
        }

        public void PrintInvoice(int ExpenseTransitionID,string ExpenseNum, string Bank, string PullMoney, string Client, string Descrip)
        {
            Print_PullExpense report = new Print_PullExpense();
            foreach(DevExpress.XtraReports.Parameters.Parameter p in report.Parameters)
            {
                p.Visible = false;
            }
            report.InitData(ExpenseTransitionID, ExpenseNum, Bank, PullMoney, Client, Descrip);
            documentViewer1.DocumentSource = report;
            report.CreateDocument();
        }
    }
}
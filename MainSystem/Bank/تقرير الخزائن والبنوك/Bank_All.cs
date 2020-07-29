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
using DevExpress.XtraTab;

namespace TaxesSystem
{
    public partial class Bank_All : DevExpress.XtraEditors.XtraForm
    {
        public static Bank_Main MainReport;
        Panel panelMainReport;
        public static Bank_Record BankReport;
        Panel panelBankReport;
        //public static Zone_Report VisaReport;
        //Panel panelVisaReport;

        public Bank_All()
        {
            InitializeComponent();
            panelMainReport = new Panel();
            panelBankReport = new Panel();
            //panelVisaReport = new Panel();
        }

        private void Zone_Area_Load(object sender, EventArgs e)
        {
            try
            {
                panelMainReport.Name = "panelMainReport";
                panelMainReport.Dock = DockStyle.Fill;

                MainReport = new Bank_Main();
                MainReport.Size = new Size(1109, 660);
                MainReport.TopLevel = false;
                MainReport.FormBorderStyle = FormBorderStyle.None;
                MainReport.Dock = DockStyle.Fill;

                panelMainReport.Controls.Clear();
                panelMainReport.Controls.Add(MainReport);
                xtraTabPageMain.Controls.Add(panelMainReport);
                MainReport.Show();
                xtraTabControl1.SelectedTabPage = xtraTabPageMain;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void xtraTabControl1_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            if(xtraTabControl1.SelectedTabPage == xtraTabPageMain)
            {
                btnMainBank.BackColor = Color.White;
                btnBank.BackColor = Color.Gainsboro;
            }
            if (xtraTabControl1.SelectedTabPage == xtraTabPageBank)
            {
                btnBank.BackColor = Color.White;
                btnMainBank.BackColor = Color.Gainsboro;
            }
        }

        private void btnArea_Click(object sender, EventArgs e)
        {
            try
            {
                panelMainReport.Name = "panelMainReport";
                panelMainReport.Dock = DockStyle.Fill;

                MainReport = new Bank_Main();
                MainReport.Size = new Size(1109, 660);
                MainReport.TopLevel = false;
                MainReport.FormBorderStyle = FormBorderStyle.None;
                MainReport.Dock = DockStyle.Fill;

                panelMainReport.Controls.Clear();
                panelMainReport.Controls.Add(MainReport);
                xtraTabPageMain.Controls.Add(panelMainReport);
                MainReport.Show();
                xtraTabControl1.SelectedTabPage = xtraTabPageMain;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnBank_Click(object sender, EventArgs e)
        {
            try
            {
                panelBankReport.Name = "panelBankReport";
                panelBankReport.Dock = DockStyle.Fill;

                BankReport = new Bank_Record();
                BankReport.Size = new Size(1109, 660);
                BankReport.TopLevel = false;
                BankReport.FormBorderStyle = FormBorderStyle.None;
                BankReport.Dock = DockStyle.Fill;

                panelBankReport.Controls.Clear();
                panelBankReport.Controls.Add(BankReport);
                xtraTabPageBank.Controls.Add(panelBankReport);
                BankReport.Show();
                xtraTabControl1.SelectedTabPage = xtraTabPageBank;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnZone_Click(object sender, EventArgs e)
        {

        }
    }
}
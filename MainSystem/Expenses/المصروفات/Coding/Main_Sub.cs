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
    public partial class Main_Sub : DevExpress.XtraEditors.XtraForm
    {
        public static SubExpenses_Report SubReport;
        Panel panelSubReport;
        public static MainExpenses_Report MainReport;
        Panel panelMainReport;

        public Main_Sub(XtraTabControl TabControlExpenses)
        {
            InitializeComponent();
            panelSubReport = new Panel();
            panelMainReport = new Panel();
        }

        private void xtraTabControl1_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            if(xtraTabControl1.SelectedTabPage == xtraTabPageSub)
            {
                btnSub.BackColor = Color.White;
                btnMain.BackColor = Color.Gainsboro;
            }
            if (xtraTabControl1.SelectedTabPage == xtraTabPageMain)
            {
                btnMain.BackColor = Color.White;
                btnSub.BackColor = Color.Gainsboro;
            }
        }

        private void btnArea_Click(object sender, EventArgs e)
        {
            try
            {
                panelSubReport.Name = "panelSubReport";
                panelSubReport.Dock = DockStyle.Fill;

                SubReport = new SubExpenses_Report();
                SubReport.Size = new Size(1109, 660);
                SubReport.TopLevel = false;
                SubReport.FormBorderStyle = FormBorderStyle.None;
                SubReport.Dock = DockStyle.Fill;

                panelSubReport.Controls.Clear();
                panelSubReport.Controls.Add(SubReport);
                xtraTabPageSub.Controls.Add(panelSubReport);
                SubReport.Show();
                xtraTabControl1.SelectedTabPage = xtraTabPageSub;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnZone_Click(object sender, EventArgs e)
        {
            try
            {
                panelMainReport.Name = "panelMainReport";
                panelMainReport.Dock = DockStyle.Fill;

                MainReport = new MainExpenses_Report();
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

        private void Zone_Area_Load(object sender, EventArgs e)
        {
            try
            {
                panelMainReport.Name = "panelMainReport";
                panelMainReport.Dock = DockStyle.Fill;

                MainReport = new MainExpenses_Report();
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
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
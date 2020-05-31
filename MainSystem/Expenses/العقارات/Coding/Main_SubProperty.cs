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

namespace MainSystem
{
    public partial class Main_SubProperty : DevExpress.XtraEditors.XtraForm
    {
        public static MainProperty_Report MainReport;
        Panel panelMainReport;
        public static SubProperty_Report SubReport;
        Panel panelSubReport;
        public static DetailsProperty_Report DetailsReport;
        Panel panelDetailsReport;

        public Main_SubProperty(XtraTabControl TabControlExpenses)
        {
            InitializeComponent();
            panelMainReport = new Panel();
            panelSubReport = new Panel();
            panelDetailsReport = new Panel();
        }

        private void xtraTabControl1_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            if (xtraTabControl1.SelectedTabPage == xtraTabPageDetails)
            {
                btnDetails.BackColor = Color.White;
                btnSub.BackColor = Color.Gainsboro;
                btnMain.BackColor = Color.Gainsboro;
            }
            if (xtraTabControl1.SelectedTabPage == xtraTabPageSub)
            {
                btnSub.BackColor = Color.White;
                btnMain.BackColor = Color.Gainsboro;
                btnDetails.BackColor = Color.Gainsboro;
            }
            if (xtraTabControl1.SelectedTabPage == xtraTabPageMain)
            {
                btnMain.BackColor = Color.White;
                btnSub.BackColor = Color.Gainsboro;
                btnDetails.BackColor = Color.Gainsboro;
            }
        }

        private void btnArea_Click(object sender, EventArgs e)
        {
            try
            {
                panelSubReport.Name = "panelSubReport";
                panelSubReport.Dock = DockStyle.Fill;

                SubReport = new SubProperty_Report();
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

                MainReport = new MainProperty_Report();
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

        private void btnDetails_Click(object sender, EventArgs e)
        {
            try
            {
                panelDetailsReport.Name = "panelDetailsReport";
                panelDetailsReport.Dock = DockStyle.Fill;

                DetailsReport = new DetailsProperty_Report();
                DetailsReport.Size = new Size(1109, 660);
                DetailsReport.TopLevel = false;
                DetailsReport.FormBorderStyle = FormBorderStyle.None;
                DetailsReport.Dock = DockStyle.Fill;

                panelDetailsReport.Controls.Clear();
                panelDetailsReport.Controls.Add(DetailsReport);
                xtraTabPageDetails.Controls.Add(panelDetailsReport);
                DetailsReport.Show();
                xtraTabControl1.SelectedTabPage = xtraTabPageDetails;
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

                MainReport = new MainProperty_Report();
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
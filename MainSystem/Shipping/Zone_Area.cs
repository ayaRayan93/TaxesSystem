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
    public partial class Zone_Area : DevExpress.XtraEditors.XtraForm
    {
        public static Area_Report AreaReport;
        Panel panelAreaReport;
        public static Zone_Report ZoneReport;
        Panel panelZoneReport;

        public Zone_Area()
        {
            InitializeComponent();
            panelAreaReport = new Panel();
            panelZoneReport = new Panel();
        }

        private void xtraTabControl1_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            if(xtraTabControl1.SelectedTabPage == xtraTabPageArea)
            {
                btnArea.BackColor = Color.Gainsboro;
                btnZone.BackColor = Color.White;
            }
            if (xtraTabControl1.SelectedTabPage == xtraTabPageZone)
            {
                btnZone.BackColor = Color.Gainsboro;
                btnArea.BackColor = Color.White;
            }
        }

        private void btnArea_Click(object sender, EventArgs e)
        {
            panelAreaReport.Name = "panelAreaReport";
            panelAreaReport.Dock = DockStyle.Fill;

            AreaReport = new Area_Report();
            AreaReport.Size = new Size(1109, 660);
            AreaReport.TopLevel = false;
            AreaReport.FormBorderStyle = FormBorderStyle.None;
            AreaReport.Dock = DockStyle.Fill;
            
            panelAreaReport.Controls.Clear();
            panelAreaReport.Controls.Add(AreaReport);
            xtraTabPageArea.Controls.Add(panelAreaReport);
            AreaReport.Show();
            xtraTabControl1.SelectedTabPage = xtraTabPageArea;
        }

        private void btnZone_Click(object sender, EventArgs e)
        {
            panelZoneReport.Name = "panelZoneReport";
            panelZoneReport.Dock = DockStyle.Fill;

            ZoneReport = new Zone_Report();
            ZoneReport.Size = new Size(1109, 660);
            ZoneReport.TopLevel = false;
            ZoneReport.FormBorderStyle = FormBorderStyle.None;
            ZoneReport.Dock = DockStyle.Fill;

            panelZoneReport.Controls.Clear();
            panelZoneReport.Controls.Add(ZoneReport);
            xtraTabPageZone.Controls.Add(panelZoneReport);
            ZoneReport.Show();
            xtraTabControl1.SelectedTabPage = xtraTabPageZone;
        }

        private void Zone_Area_Load(object sender, EventArgs e)
        {
            try
            {
                panelAreaReport.Name = "panelAreaReport";
                panelAreaReport.Dock = DockStyle.Fill;

                AreaReport = new Area_Report();
                AreaReport.Size = new Size(1109, 660);
                AreaReport.TopLevel = false;
                AreaReport.FormBorderStyle = FormBorderStyle.None;
                AreaReport.Dock = DockStyle.Fill;

                panelAreaReport.Controls.Clear();
                panelAreaReport.Controls.Add(AreaReport);
                xtraTabPageArea.Controls.Add(panelAreaReport);
                AreaReport.Show();
                xtraTabControl1.SelectedTabPage = xtraTabPageArea;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
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
using DevExpress.XtraGrid;
using DevExpress.XtraTab.ViewInfo;
using DevExpress.XtraNavBar;
using MySql.Data.MySqlClient;

namespace MainSystem
{
    class ShippingSystem
    {
    }
    public partial class MainForm 
    {
        public static XtraTabControl tabControlShipping;

        public static Zone_Area ZoneArea;
        public static Shipping_Record ShippingRecord;
        public static Permission_Report PermissionReport;
        Panel panelPermissionReport;
        Panel panelZoneArea;
        Panel panelShippingRecord;
        


        public void ShippingForm()
        {
            panelZoneArea = new Panel();
            panelShippingRecord = new Panel();
            panelPermissionReport = new Panel();
            tabControlShipping = xtraTabControlShipping;
        }
        
        private void xtraTabControlPointSale_CloseButtonClick(object sender, EventArgs e)
        {
            try
            {
                ClosePageButtonEventArgs arg = e as ClosePageButtonEventArgs;
                XtraTabPage xtraTabPage = (XtraTabPage)arg.Page;
                if (xtraTabPage.ImageOptions.Image != null)
                {
                    DialogResult dialogResult = MessageBox.Show("هل متاكد انك تريد غلق النافذة بدون حفظ التعديلات؟", "تحذير", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (dialogResult == DialogResult.Yes)
                    {

                        xtraTabControlShipping.TabPages.Remove(arg.Page as XtraTabPage);
                    }
                    else if (dialogResult == DialogResult.No)
                    {

                    }
                }
                else
                {
                    xtraTabControlShipping.TabPages.Remove(arg.Page as XtraTabPage);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        
        private void navBarItemZoonReport_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            try
            {
                restForeColorOfNavBarItem();
                NavBarItem navBarItem = (NavBarItem)sender;
                navBarItem.Appearance.ForeColor = Color.FromArgb(54, 70, 151);

                XtraTabPage xtraTabPage = getTabPage("tabPageZoneArea");
                if (xtraTabPage == null)
                {
                    xtraTabPage = new XtraTabPage();
                    xtraTabPage.Name = "tabPageZoneArea";
                    xtraTabPage.Text = "اضافة منطقة & زون";
                    panelZoneArea.Name = "panelZoneArea";
                    panelZoneArea.Dock = DockStyle.Fill;

                    ZoneArea = new Zone_Area();
                    ZoneArea.Size = new Size(1109, 660);
                    ZoneArea.TopLevel = false;
                    ZoneArea.FormBorderStyle = FormBorderStyle.None;
                    ZoneArea.Dock = DockStyle.Fill;
                }
                panelZoneArea.Controls.Clear();
                panelZoneArea.Controls.Add(ZoneArea);
                xtraTabPage.Controls.Add(panelZoneArea);
                tabControlShipping.TabPages.Add(xtraTabPage);
                ZoneArea.Show();
                tabControlShipping.SelectedTabPage = xtraTabPage;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void navBarItemShipping_Record_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            try
            {
                restForeColorOfNavBarItem();
                NavBarItem navBarItem = (NavBarItem)sender;
                navBarItem.Appearance.ForeColor = Color.FromArgb(54, 70, 151);

                XtraTabPage xtraTabPage = getTabPage("tabPageShippingRecord");
                if (xtraTabPage == null)
                {
                    xtraTabPage = new XtraTabPage();
                    xtraTabPage.Name = "tabPageShippingRecord";
                    xtraTabPage.Text = "تسجيل شحنة";
                    panelShippingRecord.Name = "panelShippingRecord";
                    panelShippingRecord.Dock = DockStyle.Fill;

                    ShippingRecord = new Shipping_Record();
                    ShippingRecord.Size = new Size(1109, 660);
                    ShippingRecord.TopLevel = false;
                    ShippingRecord.FormBorderStyle = FormBorderStyle.None;
                    ShippingRecord.Dock = DockStyle.Fill;
                }
                panelShippingRecord.Controls.Clear();
                panelShippingRecord.Controls.Add(ShippingRecord);
                xtraTabPage.Controls.Add(panelShippingRecord);
                tabControlShipping.TabPages.Add(xtraTabPage);
                ShippingRecord.Show();
                tabControlShipping.SelectedTabPage = xtraTabPage;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void navBarItemPermissionsReport_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            try
            {
                restForeColorOfNavBarItem();
                NavBarItem navBarItem = (NavBarItem)sender;
                navBarItem.Appearance.ForeColor = Color.FromArgb(54, 70, 151);

                XtraTabPage xtraTabPage = getTabPage("tabPagePermissionsReport");
                if (xtraTabPage == null)
                {
                    xtraTabPage = new XtraTabPage();
                    xtraTabPage.Name = "tabPagePermissionsReport";
                    xtraTabPage.Text = "تقرير الاذون";
                    panelPermissionReport.Name = "panelPermissionsReport";
                    panelPermissionReport.Dock = DockStyle.Fill;

                    PermissionReport = new Permission_Report();
                    PermissionReport.Size = new Size(1109, 660);
                    PermissionReport.TopLevel = false;
                    PermissionReport.FormBorderStyle = FormBorderStyle.None;
                    PermissionReport.Dock = DockStyle.Fill;
                }
                panelPermissionReport.Controls.Clear();
                panelPermissionReport.Controls.Add(PermissionReport);
                xtraTabPage.Controls.Add(panelPermissionReport);
                tabControlShipping.TabPages.Add(xtraTabPage);
                PermissionReport.Show();
                tabControlShipping.SelectedTabPage = xtraTabPage;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //aya code 
        private void navBarItemDisplayWantedShippingBills_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            try
            {
                restForeColorOfNavBarItem();
                NavBarItem navBarItem = (NavBarItem)sender;
                navBarItem.Appearance.ForeColor = Color.Blue;

                if (!tabControlShipping.Visible)
                    tabControlShipping.Visible = true;

                XtraTabPage xtraTabPage = getTabPage(tabControlShipping, "الفواتير المراد شحنها");
                if (xtraTabPage == null)
                {
                    tabControlShipping.TabPages.Add("الفواتير المراد شحنها");
                    xtraTabPage = getTabPage(tabControlShipping, "الفواتير المراد شحنها");
                }
                xtraTabPage.Controls.Clear();

                tabControlShipping.SelectedTabPage = xtraTabPage;

                ShippingBillsWanted objForm = new ShippingBillsWanted(this);
                objForm.TopLevel = false;

                xtraTabPage.Controls.Add(objForm);
                objForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                objForm.Dock = DockStyle.Fill;
                objForm.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void displayMakeShippingForm(List<int> arrInt)
        {
            try
            {
                if (!tabControlShipping.Visible)
                    tabControlShipping.Visible = true;

                XtraTabPage xtraTabPage = getTabPage(tabControlShipping, "تسجيل شحنة");
                if (xtraTabPage == null)
                {
                    tabControlShipping.TabPages.Add("تسجيل شحنة");
                    xtraTabPage = getTabPage(tabControlShipping, "تسجيل شحنة");
                }
                xtraTabPage.Controls.Clear();

                tabControlShipping.SelectedTabPage = xtraTabPage;

                MakeShipping objForm = new MakeShipping(arrInt);
                objForm.TopLevel = false;

                xtraTabPage.Controls.Add(objForm);
                objForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                objForm.Dock = DockStyle.Fill;
                objForm.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

    }
}

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

        XtraTabPage tabPageZoneArea;
        Panel panelZoneArea;
        XtraTabPage tabPageShippingRecord;
        Panel panelShippingRecord;
        

        public void ShippingForm()
        {
            tabPageZoneArea = new XtraTabPage();
            panelZoneArea = new Panel();
            tabPageShippingRecord = new XtraTabPage();
            panelShippingRecord = new Panel();

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
                    tabPageZoneArea.Name = "tabPageZoneArea";
                    tabPageZoneArea.Text = "اضافة منطقة & زون";
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
                tabPageZoneArea.Controls.Add(panelZoneArea);
                tabControlShipping.TabPages.Add(tabPageZoneArea);
                ZoneArea.Show();
                tabControlShipping.SelectedTabPage = tabPageZoneArea;
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
                    tabPageShippingRecord.Name = "tabPageShippingRecord";
                    tabPageShippingRecord.Text = "تسجيل شحنة";
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
                tabPageShippingRecord.Controls.Add(panelShippingRecord);
                tabControlShipping.TabPages.Add(tabPageShippingRecord);
                ShippingRecord.Show();
                tabControlShipping.SelectedTabPage = tabPageShippingRecord;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}

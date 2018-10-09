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
using DevExpress.XtraTab.ViewInfo;
using DevExpress.XtraTab;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraGrid;
using DevExpress.XtraNavBar;
using System.Diagnostics;
using MySql.Data.MySqlClient;

namespace MainSystem
{
    public partial class Main : DevExpress.XtraEditors.XtraForm
    {
        MySqlConnection conn;
        public static XtraTabControl tabControlShipping;
        
        public static Zone_Area ZoneArea;
        public static Shipping_Record ShippingRecord;

        XtraTabPage tabPageZoneArea;
        Panel panelZoneArea;
        XtraTabPage tabPageShippingRecord;
        Panel panelShippingRecord;

        public static int delegateID = -1;
        public static int billNum = 0;
        bool flag = false;

        public Main()
        {
            InitializeComponent();
            conn = new MySqlConnection(connection.connectionString);
            
            tabPageZoneArea = new XtraTabPage();
            panelZoneArea = new Panel();
            tabPageShippingRecord = new XtraTabPage();
            panelShippingRecord = new Panel();

            xtraTabControlMain.TabPages.Remove(xtraTabPageShipping);

            tabControlShipping = xtraTabControlShipping;
        }

        private void xtraTabControlMain_CloseButtonClick(object sender, EventArgs e)
        {
            try
            {
                ClosePageButtonEventArgs arg = e as ClosePageButtonEventArgs;
                XtraTabPage xtraTabPage = (XtraTabPage)arg.Page;
                if (!IsTabPageSave())
                {
                    DialogResult dialogResult = MessageBox.Show("هل متاكد انك تريد غلق النافذة بدون حفظ التعديلات؟", "تحذير", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (dialogResult == DialogResult.Yes)
                    {
                        xtraTabControlMain.TabPages.Remove(arg.Page as XtraTabPage);
                        flag = false;
                    }
                    else if (dialogResult == DialogResult.No)
                    {

                    }
                }
                else
                {
                    xtraTabControlMain.TabPages.Remove(arg.Page as XtraTabPage);
                    flag = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
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

        private void xtraTabControlPointSale_SelectedPageChanged(object sender, TabPageChangedEventArgs e)
        {
            try
            {
                //if (xtraTabControlPointSale.SelectedTabPage == tabPageProductsReport)
                //{
                //    ProductsReport.search();
                //}

                //else if (xtraTabControlBank.SelectedTabPage == Bank_Report.MainTabPagePrintingBank)
                //{
                //    if (loadedPrintBank)
                //    {
                //        Bank_Report.bankPrint.display();
                //    }
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //functions
        public void restForeColorOfNavBarItem()
        {
            foreach (NavBarItem item in navBarControlShippingReport.Items)
            {
                item.Appearance.ForeColor = Color.Black;
            }
        }

        public XtraTabPage getTabPage(string text)
        {
            for (int i = 0; i < Main.tabControlShipping.TabPages.Count; i++)
                if (Main.tabControlShipping.TabPages[i].Name == text)
                {
                    return Main.tabControlShipping.TabPages[i];
                }
            return null;
        }

        public bool IsTabPageSave()
        {
            for (int i = 0; i < xtraTabControlShipping.TabPages.Count; i++)
                if (xtraTabControlShipping.TabPages[i].ImageOptions.Image != null)
                {
                    return false;
                }
            return true;
        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (!IsTabPageSave())
                {
                    DialogResult dialogResult = MessageBox.Show("هل متاكد انك تريد غلق النافذة بدون حفظ التعديلات؟", "تحذير", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (dialogResult == DialogResult.Yes)
                    {
                        Application.Exit();
                    }
                    else if (dialogResult == DialogResult.No)
                    {
                        e.Cancel = (dialogResult == DialogResult.No);
                    }
                }
                else
                {
                    Application.Exit();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void tileItemShipping_ItemClick(object sender, TileItemEventArgs e)
        {
            try
            {
                if (flag == false)
                {
                    xtraTabControlMain.TabPages.Add(xtraTabPageShipping);
                    flag = true;
                }
                xtraTabControlMain.SelectedTabPage = xtraTabPageShipping;
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
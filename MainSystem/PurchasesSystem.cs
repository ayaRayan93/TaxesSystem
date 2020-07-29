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
using TaxesSystem.Sales.accounting;

namespace TaxesSystem
{
    class PurchasesSystem
    {
    }
    public partial class MainForm
    {
        public static XtraTabControl tabControlPurchases;
        
        public void PurchasesMainForm()
        {
        }
        
        private void navBarItemPurchasesPricesRecord_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            try
            {
                //if (UserControl.userType == 10 || UserControl.userType == 17 || UserControl.userType == 1/* || UserControl.userType == 2*/)
                //{
                    restForeColorOfNavBarItem();
                    NavBarItem navBarItem = (NavBarItem)sender;
                    navBarItem.Appearance.ForeColor = Color.Blue;

                    if (!xtraTabControlPurchases.Visible)
                        xtraTabControlPurchases.Visible = true;

                    XtraTabPage xtraTabPage = getTabPage(xtraTabControlPurchases, "اسعار شراء البنود");
                    if (xtraTabPage == null)
                    {
                        xtraTabControlPurchases.TabPages.Add("اسعار شراء البنود");
                        xtraTabPage = getTabPage(xtraTabControlPurchases, "اسعار شراء البنود");
                    }
                    xtraTabPage.Controls.Clear();

                    xtraTabControlPurchases.SelectedTabPage = xtraTabPage;
                    bindDisplayProductsPurchasesPriceForm(xtraTabPage);
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void navBarItemSupplierBills_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            try
            {
                //if (UserControl.userType == 10 || UserControl.userType == 17 || UserControl.userType == 1)
                //{
                    restForeColorOfNavBarItem();
                    NavBarItem navBarItem = (NavBarItem)sender;
                    navBarItem.Appearance.ForeColor = Color.Blue;

                    if (!xtraTabControlPurchases.Visible)
                        xtraTabControlPurchases.Visible = true;

                    XtraTabPage xtraTabPage = getTabPage(xtraTabControlPurchases, "تسجيل فاتورة شراء");
                    if (xtraTabPage == null)
                    {
                        xtraTabControlPurchases.TabPages.Add("تسجيل فاتورة شراء");
                        xtraTabPage = getTabPage(xtraTabControlPurchases, "تسجيل فاتورة شراء");
                    }
                    xtraTabPage.Controls.Clear();

                    xtraTabControlPurchases.SelectedTabPage = xtraTabPage;
                    bindSupplierBillForm(xtraTabPage);
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        
        private void navBarItemSupplierReturn_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            try
            {
                //if (UserControl.userType == 10 || UserControl.userType == 17 || UserControl.userType == 1)
                //{
                    restForeColorOfNavBarItem();
                    NavBarItem navBarItem = (NavBarItem)sender;
                    navBarItem.Appearance.ForeColor = Color.Blue;

                    if (!xtraTabControlPurchases.Visible)
                        xtraTabControlPurchases.Visible = true;

                    XtraTabPage xtraTabPage = getTabPage(xtraTabControlPurchases, "تسجيل فاتورة مرتجع");
                    if (xtraTabPage == null)
                    {
                        xtraTabControlPurchases.TabPages.Add("تسجيل فاتورة مرتجع");
                        xtraTabPage = getTabPage(xtraTabControlPurchases, "تسجيل فاتورة مرتجع");
                    }
                    xtraTabPage.Controls.Clear();

                    xtraTabControlPurchases.SelectedTabPage = xtraTabPage;
                    bindSupplierRetunBillForm(xtraTabPage);
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void navBarItemSuppliersReport_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            try
            {
                //if (UserControl.userType == 10 || UserControl.userType == 17 || UserControl.userType == 1)
                //{
                    restForeColorOfNavBarItem();
                    NavBarItem navBarItem = (NavBarItem)sender;
                    navBarItem.Appearance.ForeColor = Color.Blue;

                    if (!xtraTabControlPurchases.Visible)
                        xtraTabControlPurchases.Visible = true;

                    XtraTabPage xtraTabPage = getTabPage(xtraTabControlPurchases, "تقرير الموردين");
                    if (xtraTabPage == null)
                    {
                        xtraTabControlPurchases.TabPages.Add("تقرير الموردين");
                        xtraTabPage = getTabPage(xtraTabControlPurchases, "تقرير الموردين");
                    }
                    xtraTabPage.Controls.Clear();

                    xtraTabControlPurchases.SelectedTabPage = xtraTabPage;
                    bindDisplaySupplierForm(xtraTabPage);
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        
        private void navBarItemPurchaseBillPriceUpdate_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            try
            {
                //if (UserControl.userType == 10 || UserControl.userType == 17 || UserControl.userType == 1)
                //{
                    restForeColorOfNavBarItem();
                    NavBarItem navBarItem = (NavBarItem)sender;
                    navBarItem.Appearance.ForeColor = Color.Blue;
                    if (!xtraTabControlPurchases.Visible)
                        xtraTabControlPurchases.Visible = true;

                    XtraTabPage xtraTabPage = getTabPage(xtraTabControlPurchases, "عرض فواتير الشراء");
                    if (xtraTabPage == null)
                    {
                        xtraTabControlPurchases.TabPages.Add("عرض فواتير الشراء");
                        xtraTabPage = getTabPage(xtraTabControlPurchases, "عرض فواتير الشراء");
                    }

                    xtraTabPage.Controls.Clear();
                    xtraTabControlPurchases.SelectedTabPage = xtraTabPage;

                    PurchaseBill_Report objForm = new PurchaseBill_Report(this, xtraTabControlPurchases);

                    objForm.TopLevel = false;
                    xtraTabPage.Controls.Add(objForm);
                    objForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                    objForm.Dock = DockStyle.Fill;
                    objForm.Show();
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void navBarItemReturnedPurchaseBillPriceUpdate_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            try
            {
                //if (UserControl.userType == 10 || UserControl.userType == 17 || UserControl.userType == 1)
                //{
                    restForeColorOfNavBarItem();
                    NavBarItem navBarItem = (NavBarItem)sender;
                    navBarItem.Appearance.ForeColor = Color.Blue;
                    if (!xtraTabControlPurchases.Visible)
                        xtraTabControlPurchases.Visible = true;

                    XtraTabPage xtraTabPage = getTabPage(xtraTabControlPurchases, "عرض فواتير المرتجع");
                    if (xtraTabPage == null)
                    {
                        xtraTabControlPurchases.TabPages.Add("عرض فواتير المرتجع");
                        xtraTabPage = getTabPage(xtraTabControlPurchases, "عرض فواتير المرتجع");
                    }

                    xtraTabPage.Controls.Clear();
                    xtraTabControlPurchases.SelectedTabPage = xtraTabPage;

                    ReturnedPurchaseBill_Report objForm = new ReturnedPurchaseBill_Report(this, xtraTabControlPurchases);

                    objForm.TopLevel = false;
                    xtraTabPage.Controls.Add(objForm);
                    objForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                    objForm.Dock = DockStyle.Fill;
                    objForm.Show();
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        
        private void navBarItemSupplierBillReport_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            try
            {
                //if (UserControl.userType == 10 || UserControl.userType == 17 || UserControl.userType == 1)
                //{
                    restForeColorOfNavBarItem();
                    NavBarItem navBarItem = (NavBarItem)sender;
                    navBarItem.Appearance.ForeColor = Color.Blue;
                    if (!xtraTabControlPurchases.Visible)
                        xtraTabControlPurchases.Visible = true;

                    XtraTabPage xtraTabPage = getTabPage(xtraTabControlPurchases, "تقرير فواتير الموردين");
                    if (xtraTabPage == null)
                    {
                        xtraTabControlPurchases.TabPages.Add("تقرير فواتير الموردين");
                        xtraTabPage = getTabPage(xtraTabControlPurchases, "تقرير فواتير الموردين");
                    }

                    xtraTabPage.Controls.Clear();
                    xtraTabControlPurchases.SelectedTabPage = xtraTabPage;

                    SupplierBills_Report objForm = new SupplierBills_Report(this, xtraTabControlPurchases);

                    objForm.TopLevel = false;
                    xtraTabPage.Controls.Add(objForm);
                    objForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                    objForm.Dock = DockStyle.Fill;
                    objForm.Show();
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        ////////////////////////////////////////////////////////
        private void navBarItemSupplierAccount_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            try
            {
                restForeColorOfNavBarItem();
                NavBarItem navBarItem = (NavBarItem)sender;
                navBarItem.Appearance.ForeColor = Color.Blue;
                if (!xtraTabControlPurchases.Visible)
                    xtraTabControlPurchases.Visible = true;

                XtraTabPage xtraTabPage = getTabPage(xtraTabControlPurchases, "عرض حسابات الموردين");
                if (xtraTabPage == null)
                {
                    xtraTabControlPurchases.TabPages.Add("عرض حسابات الموردين");
                    xtraTabPage = getTabPage(xtraTabControlPurchases, "عرض حسابات الموردين");
                }

                xtraTabPage.Controls.Clear();
                xtraTabControlPurchases.SelectedTabPage = xtraTabPage;

                SupplierAccount_Report objForm = new SupplierAccount_Report(this, xtraTabControlPurchases);

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
        private void navBarItemSupplierSoonPayments_Report_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            try
            {
                restForeColorOfNavBarItem();
                NavBarItem navBarItem = (NavBarItem)sender;
                navBarItem.Appearance.ForeColor = Color.Blue;
                if (!xtraTabControlPurchases.Visible)
                    xtraTabControlPurchases.Visible = true;
                //عرض سدادات الموردين
                XtraTabPage xtraTabPage = getTabPage(xtraTabControlPurchases, "سدادات الموردين");
                if (xtraTabPage == null)
                {
                    xtraTabControlPurchases.TabPages.Add("سدادات الموردين");
                    xtraTabPage = getTabPage(xtraTabControlPurchases, "سدادات الموردين");
                }

                xtraTabPage.Controls.Clear();
                xtraTabControlPurchases.SelectedTabPage = xtraTabPage;

                SupplierSoonPayments_Report objForm = new SupplierSoonPayments_Report(this, xtraTabControlPurchases);

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
        private void navBarItemSupplierTransitionsReport_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            try
            {
                restForeColorOfNavBarItem();
                NavBarItem navBarItem = (NavBarItem)sender;
                navBarItem.Appearance.ForeColor = Color.Blue;
                if (!xtraTabControlPurchases.Visible)
                    xtraTabControlPurchases.Visible = true;

                XtraTabPage xtraTabPage = getTabPage(xtraTabControlPurchases, "حركة سدادات الموردين");
                if (xtraTabPage == null)
                {
                    xtraTabControlPurchases.TabPages.Add("حركة سدادات الموردين");
                    xtraTabPage = getTabPage(xtraTabControlPurchases, "حركة سدادات الموردين");
                }

                xtraTabPage.Controls.Clear();
                xtraTabControlPurchases.SelectedTabPage = xtraTabPage;

                SupplierTransitions_Report objForm = new SupplierTransitions_Report(this, xtraTabControlPurchases);

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

        private void navBarItem292_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            try
            {
                restForeColorOfNavBarItem();
                NavBarItem navBarItem = (NavBarItem)sender;
                navBarItem.Appearance.ForeColor = Color.Blue;
                if (!xtraTabControlPurchases.Visible)
                    xtraTabControlPurchases.Visible = true;

                XtraTabPage xtraTabPage = getTabPage(xtraTabControlPurchases,"تسجيل سداد");
                if (xtraTabPage == null)
                {
                    xtraTabControlPurchases.TabPages.Add("تسجيل سداد");
                    xtraTabPage = getTabPage(xtraTabControlPurchases, "تسجيل سداد");
                }

                xtraTabPage.Controls.Clear();
                xtraTabControlPurchases.SelectedTabPage = xtraTabPage;

                BankSupplierPullAgl_Record2 objForm = new BankSupplierPullAgl_Record2( xtraTabControlPurchases);

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
        private void navBarItemSupplierBillsTransitionsReport_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            try
            {
                restForeColorOfNavBarItem();
                NavBarItem navBarItem = (NavBarItem)sender;
                navBarItem.Appearance.ForeColor = Color.Blue;
                if (!xtraTabControlPurchases.Visible)
                    xtraTabControlPurchases.Visible = true;

                XtraTabPage xtraTabPage = getTabPage(xtraTabControlPurchases, "حركة المسحوبات والمرتجعات");
                if (xtraTabPage == null)
                {
                    xtraTabControlPurchases.TabPages.Add("حركة المسحوبات والمرتجعات");
                    xtraTabPage = getTabPage(xtraTabControlPurchases, "حركة المسحوبات والمرتجعات");
                }

                xtraTabPage.Controls.Clear();
                xtraTabControlPurchases.SelectedTabPage = xtraTabPage;

                SupplierBillsTransitions_Report objForm = new SupplierBillsTransitions_Report(this, xtraTabControlPurchases);

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
        private void navBarItemSupplierBillsTransitionsDetailsReport_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            try
            {
                restForeColorOfNavBarItem();
                NavBarItem navBarItem = (NavBarItem)sender;
                navBarItem.Appearance.ForeColor = Color.Blue;
                if (!xtraTabControlPurchases.Visible)
                    xtraTabControlPurchases.Visible = true;

                XtraTabPage xtraTabPage = getTabPage(xtraTabControlPurchases, "تفاصيل حركة المسحوبات والمرتجعات");
                if (xtraTabPage == null)
                {
                    xtraTabControlPurchases.TabPages.Add("تفاصيل حركة المسحوبات والمرتجعات");
                    xtraTabPage = getTabPage(xtraTabControlPurchases, "تفاصيل حركة المسحوبات والمرتجعات");
                }

                xtraTabPage.Controls.Clear();
                xtraTabControlPurchases.SelectedTabPage = xtraTabPage;

                SupplierBillsTransitionsDetails_Report objForm = new SupplierBillsTransitionsDetails_Report(this, xtraTabControlPurchases);

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
        private void navBarItemSupplierTaswyaReport_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            try
            {
                restForeColorOfNavBarItem();
                NavBarItem navBarItem = (NavBarItem)sender;
                navBarItem.Appearance.ForeColor = Color.Blue;
                if (!xtraTabControlPurchases.Visible)
                    xtraTabControlPurchases.Visible = true;

                XtraTabPage xtraTabPage = getTabPage(xtraTabControlPurchases, "عرض التسويات");
                if (xtraTabPage == null)
                {
                    xtraTabControlPurchases.TabPages.Add("عرض التسويات");
                    xtraTabPage = getTabPage(xtraTabControlPurchases, "عرض التسويات");
                }

                xtraTabPage.Controls.Clear();
                xtraTabControlPurchases.SelectedTabPage = xtraTabPage;

                SupplierTaswayaReport objForm = new SupplierTaswayaReport(this, xtraTabControlPurchases);

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

        private void navBarItemSupplierTypeReport_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            try
            {
                restForeColorOfNavBarItem();
                NavBarItem navBarItem = (NavBarItem)sender;
                navBarItem.Appearance.ForeColor = Color.Blue;
                if (!xtraTabControlPurchases.Visible)
                    xtraTabControlPurchases.Visible = true;

                XtraTabPage xtraTabPage = getTabPage(xtraTabControlPurchases, "حركة مورد بالنوع");
                if (xtraTabPage == null)
                {
                    xtraTabControlPurchases.TabPages.Add("حركة مورد بالنوع");
                    xtraTabPage = getTabPage(xtraTabControlPurchases, "حركة مورد بالنوع");
                }

                xtraTabPage.Controls.Clear();
                xtraTabControlPurchases.SelectedTabPage = xtraTabPage;

                SupplierType_Report objForm = new SupplierType_Report(this, xtraTabControlPurchases);

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

        private void navBarItemPurchaseQuantityReport_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            try
            {
                restForeColorOfNavBarItem();
                NavBarItem navBarItem = (NavBarItem)sender;
                navBarItem.Appearance.ForeColor = Color.Blue;
                if (!xtraTabControlPurchases.Visible)
                    xtraTabControlPurchases.Visible = true;

                XtraTabPage xtraTabPage = getTabPage(xtraTabControlPurchases, "حركة مورد بالكمية");
                if (xtraTabPage == null)
                {
                    xtraTabControlPurchases.TabPages.Add("حركة مورد بالكمية");
                    xtraTabPage = getTabPage(xtraTabControlPurchases, "حركة مورد بالكمية");
                }

                xtraTabPage.Controls.Clear();
                xtraTabControlPurchases.SelectedTabPage = xtraTabPage;

                PurchaseQuantityDetails_Report objForm = new PurchaseQuantityDetails_Report(this, xtraTabControlPurchases);

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

        private void navBarItemPurchasePriceReport_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            try
            {
                restForeColorOfNavBarItem();
                NavBarItem navBarItem = (NavBarItem)sender;
                navBarItem.Appearance.ForeColor = Color.Blue;
                if (!xtraTabControlPurchases.Visible)
                    xtraTabControlPurchases.Visible = true;

                XtraTabPage xtraTabPage = getTabPage(xtraTabControlPurchases, "حركة مورد بالاسعار");
                if (xtraTabPage == null)
                {
                    xtraTabControlPurchases.TabPages.Add("حركة مورد بالاسعار");
                    xtraTabPage = getTabPage(xtraTabControlPurchases, "حركة مورد بالاسعار");
                }

                xtraTabPage.Controls.Clear();
                xtraTabControlPurchases.SelectedTabPage = xtraTabPage;

                PurchasePriceDetails_Report objForm = new PurchasePriceDetails_Report(this, xtraTabControlPurchases);

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

        private void navBarItem91_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            try
            {
                restForeColorOfNavBarItem();
                NavBarItem navBarItem = (NavBarItem)sender;
                navBarItem.Appearance.ForeColor = Color.Blue;
                if (!xtraTabControlPurchases.Visible)
                    xtraTabControlPurchases.Visible = true;

                XtraTabPage xtraTabPage = getTabPage(xtraTabControlPurchases, "رصيد المخزون بسعر الشراء");
                if (xtraTabPage == null)
                {
                    xtraTabControlPurchases.TabPages.Add("رصيد المخزون بسعر الشراء");
                    xtraTabPage = getTabPage(xtraTabControlPurchases, "رصيد المخزون بسعر الشراء");
                }

                xtraTabPage.Controls.Clear();
                xtraTabControlPurchases.SelectedTabPage = xtraTabPage;

                StoragePurchases_Report objForm = new StoragePurchases_Report();

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
        private void navBarItem95_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            try
            {
                restForeColorOfNavBarItem();
                NavBarItem navBarItem = (NavBarItem)sender;
                navBarItem.Appearance.ForeColor = Color.Blue;
                if (!xtraTabControlPurchases.Visible)
                    xtraTabControlPurchases.Visible = true;

                XtraTabPage xtraTabPage = getTabPage(xtraTabControlPurchases, "رصيد المخزون الحالي بسعر الشراء");
                if (xtraTabPage == null)
                {
                    xtraTabControlPurchases.TabPages.Add("رصيد المخزون الحالي بسعر الشراء");
                    xtraTabPage = getTabPage(xtraTabControlPurchases, "رصيد المخزون الحالي بسعر الشراء");
                }

                xtraTabPage.Controls.Clear();
                xtraTabControlPurchases.SelectedTabPage = xtraTabPage;

                CurrentStoragePurchases_Report objForm = new CurrentStoragePurchases_Report();

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
        //Special Orders Report

        //Products Purchase price
        public void bindDisplayProductsPurchasesPriceForm(XtraTabPage xtraTabPage)
        {
            ProductsPurchasesPriceForm objForm = new ProductsPurchasesPriceForm(this);
            objForm.TopLevel = false;

            xtraTabPage.Controls.Add(objForm);
            objForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            objForm.Dock = DockStyle.Fill;
            objForm.Show();
        }
        //record Purchase price 
        public void bindRecordPurchasePriceForm(ProductsPurchasesPriceForm ProductsPurchasesPriceForm)
        {
            if (!xtraTabControlPurchases.Visible)
                xtraTabControlPurchases.Visible = true;

            XtraTabPage xtraTabPage = getTabPage(xtraTabControlPurchases, "تسجيل اسعار شراء البنود");
            if (xtraTabPage == null)
            {
                xtraTabControlPurchases.TabPages.Add("تسجيل اسعار شراء البنود");
                xtraTabPage = getTabPage(xtraTabControlPurchases, "تسجيل اسعار شراء البنود");
            }
            xtraTabPage.Controls.Clear();

            xtraTabControlPurchases.SelectedTabPage = xtraTabPage;
            SetPurchasesPrice2 objForm = new SetPurchasesPrice2(ProductsPurchasesPriceForm, xtraTabControlPurchases);
            objForm.TopLevel = false;

            xtraTabPage.Controls.Add(objForm);
            objForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            objForm.Dock = DockStyle.Fill;
            objForm.Show();
        }
        //update Purchase price 
        public void bindUpdatePurchasesPriceForm(List<DataRowView> rows, ProductsPurchasesPriceForm ProductsPurchasesPriceForm, String query)
        {
            if (!xtraTabControlPurchases.Visible)
                xtraTabControlPurchases.Visible = true;

            XtraTabPage xtraTabPage = getTabPage(xtraTabControlPurchases, "تعديل اسعار شراء البنود");

            if (xtraTabPage == null)
            {
                xtraTabControlPurchases.TabPages.Add("تعديل اسعار شراء البنود");
                xtraTabPage = getTabPage(xtraTabControlPurchases, "تعديل اسعار شراء البنود");
            }
            xtraTabPage.Controls.Clear();

            xtraTabControlPurchases.SelectedTabPage = xtraTabPage;

            UpdatePurchasesPrice objForm = new UpdatePurchasesPrice(rows, ProductsPurchasesPriceForm, query, xtraTabControlPurchases);
            objForm.TopLevel = false;

            xtraTabPage.Controls.Add(objForm);
            objForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            objForm.Dock = DockStyle.Fill;
            objForm.Show();
        }
        //report Purchase price 
        public void bindReportPurchasePriceForm(GridControl gridControl)
        {
            if (!xtraTabControlPurchases.Visible)
                xtraTabControlPurchases.Visible = true;

            XtraTabPage xtraTabPage = getTabPage(xtraTabControlPurchases, "تقرير اسعار شراء البنود");

            if (xtraTabPage == null)
            {
                xtraTabControlPurchases.TabPages.Add("تقرير اسعار شراء البنود");
                xtraTabPage = getTabPage(xtraTabControlPurchases, "تقرير اسعار شراء البنود");
            }
            xtraTabPage.Controls.Clear();

            xtraTabControlPurchases.SelectedTabPage = xtraTabPage;
            ProductPurchasesPricesReport objForm = new ProductPurchasesPricesReport(gridControl);
            objForm.TopLevel = false;

            xtraTabPage.Controls.Add(objForm);
            objForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            objForm.Dock = DockStyle.Fill;
            objForm.Show();
        }
        
        public void bindSupplierBillForm(XtraTabPage xtraTabPage)
        {
            Supplier_Bill objForm = new Supplier_Bill(this);
            objForm.TopLevel = false;

            xtraTabPage.Controls.Add(objForm);
            objForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            objForm.Dock = DockStyle.Fill;
            objForm.Show();
        }

        public void bindSupplierRetunBillForm(XtraTabPage xtraTabPage)
        {
            Supplier_Return_Bill2 objForm = new Supplier_Return_Bill2(this);
            objForm.TopLevel = false;

            xtraTabPage.Controls.Add(objForm);
            objForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            objForm.Dock = DockStyle.Fill;
            objForm.Show();
        }

        public void bindRecordPurchasesPriceForm(ProductsPurchasesPriceForm productsSellPriceForm)
        {
            if (!xtraTabControlPurchases.Visible)
                xtraTabControlPurchases.Visible = true;

            XtraTabPage xtraTabPage = getTabPage(xtraTabControlPurchases, "تسجيل اسعار شراء البنود");
            if (xtraTabPage == null)
            {
                xtraTabControlPurchases.TabPages.Add("تسجيل اسعار شراء البنود");
                xtraTabPage = getTabPage(xtraTabControlPurchases, "تسجيل اسعار شراء البنود");
            }
            xtraTabPage.Controls.Clear();

            xtraTabControlPurchases.SelectedTabPage = xtraTabPage;
            SetPurchasesPrice2 objForm = new SetPurchasesPrice2(productsSellPriceForm, xtraTabControlPurchases);
            objForm.TopLevel = false;

            xtraTabPage.Controls.Add(objForm);
            objForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            objForm.Dock = DockStyle.Fill;
            objForm.Show();
        }
    
        public void bindReportPurchasesPriceForm(GridControl gridControl)
        {
            if (!xtraTabControlPurchases.Visible)
                xtraTabControlPurchases.Visible = true;

            XtraTabPage xtraTabPage = getTabPage(xtraTabControlPurchases, "تقرير اسعار البنود");

            if (xtraTabPage == null)
            {
                xtraTabControlPurchases.TabPages.Add("تقرير اسعار البنود");
                xtraTabPage = getTabPage(xtraTabControlPurchases, "تقرير اسعار البنود");
            }
            xtraTabPage.Controls.Clear();

            xtraTabControlPurchases.SelectedTabPage = xtraTabPage;
            ProductPurchasesPricesReport objForm = new ProductPurchasesPricesReport(gridControl);
            objForm.TopLevel = false;

            xtraTabPage.Controls.Add(objForm);
            objForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            objForm.Dock = DockStyle.Fill;
            objForm.Show();
        }

        public void bindDisplaySupplierForm(XtraTabPage xtraTabPage)
        {
            Supplier_Report objForm = new Supplier_Report(this, xtraTabControlPurchases);
            objForm.TopLevel = false;

            xtraTabPage.Controls.Add(objForm);
            objForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            objForm.Dock = DockStyle.Fill;
            objForm.Show();
        }
        public void bindRecordSupplierForm(Supplier_Report SupplierReport)
        {
            if (!xtraTabControlPurchases.Visible)
                xtraTabControlPurchases.Visible = true;

            XtraTabPage xtraTabPage = getTabPage(xtraTabControlPurchases, "اضافة مورد");
            if (xtraTabPage == null)
            {
                xtraTabControlPurchases.TabPages.Add("اضافة مورد");
                xtraTabPage = getTabPage(xtraTabControlPurchases, "اضافة مورد");
            }
            xtraTabPage.Controls.Clear();

            xtraTabControlPurchases.SelectedTabPage = xtraTabPage;
            AddSupplier objForm = new AddSupplier(SupplierReport, xtraTabControlPurchases);
            objForm.TopLevel = false;

            xtraTabPage.Controls.Add(objForm);
            objForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            objForm.Dock = DockStyle.Fill;
            objForm.Show();
        }
        public void bindUpdateSupplierForm(DataRowView rows, Supplier_Report supplierReport)
        {
            if (!xtraTabControlPurchases.Visible)
                xtraTabControlPurchases.Visible = true;

            XtraTabPage xtraTabPage = getTabPage(xtraTabControlPurchases, "تعديل بيانات مورد");

            if (xtraTabPage == null)
            {
                xtraTabControlPurchases.TabPages.Add("تعديل بيانات مورد");
                xtraTabPage = getTabPage(xtraTabControlPurchases, "تعديل بيانات مورد");
            }
            xtraTabPage.Controls.Clear();

            xtraTabControlPurchases.SelectedTabPage = xtraTabPage;

            UpdateSupplier objForm = new UpdateSupplier(rows, supplierReport, xtraTabControlPurchases);
            objForm.TopLevel = false;

            xtraTabPage.Controls.Add(objForm);
            objForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            objForm.Dock = DockStyle.Fill;
            objForm.Show();
        }
        public void bindPrintSupplierForm()
        {
            if (!xtraTabControlPurchases.Visible)
                xtraTabControlPurchases.Visible = true;

            XtraTabPage xtraTabPage = getTabPage(xtraTabControlPurchases, "طباعة بيانات الموردين");
            if (xtraTabPage == null)
            {
                xtraTabControlPurchases.TabPages.Add("طباعة بيانات الموردين");
                xtraTabPage = getTabPage(xtraTabControlPurchases, "طباعة بيانات الموردين");
            }
            xtraTabPage.Controls.Clear();

            xtraTabControlPurchases.SelectedTabPage = xtraTabPage;
            Supplier_Print objForm = new Supplier_Print();
            objForm.TopLevel = false;

            xtraTabPage.Controls.Add(objForm);
            objForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            objForm.Dock = DockStyle.Fill;
            objForm.Show();
        }

        public void bindTaswayaSupplierForm()
        {
            if (!xtraTabControlPurchases.Visible)
                xtraTabControlPurchases.Visible = true;

            XtraTabPage xtraTabPage = getTabPage(xtraTabControlPurchases, "اضافة تسوية لمورد");
            if (xtraTabPage == null)
            {
                xtraTabControlPurchases.TabPages.Add("اضافة تسوية لمورد");
                xtraTabPage = getTabPage(xtraTabControlPurchases, "اضافة تسوية لمورد");
            }
            xtraTabPage.Controls.Clear();
            xtraTabControlPurchases.SelectedTabPage = xtraTabPage;

            SupplierTaswaya objForm = new SupplierTaswaya();
            objForm.TopLevel = false;
            xtraTabPage.Controls.Add(objForm);
            objForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            objForm.Dock = DockStyle.Fill;
            objForm.Show();
        }
        public void bindUpdateTaswayaSupplierForm(DataRowView row, SupplierTaswayaReport SupplierTaswayaReport)
        {
            if (!xtraTabControlPurchases.Visible)
                xtraTabControlPurchases.Visible = true;

            XtraTabPage xtraTabPage = getTabPage(xtraTabControlPurchases, "تعديل تسوية مورد");
            if (xtraTabPage == null)
            {
                xtraTabControlPurchases.TabPages.Add("تعديل تسوية مورد");
                xtraTabPage = getTabPage(xtraTabControlPurchases, "تعديل تسوية مورد");
            }
            xtraTabPage.Controls.Clear();
            xtraTabControlPurchases.SelectedTabPage = xtraTabPage;

            UpdateSupplierTaswaya objForm = new UpdateSupplierTaswaya(row, SupplierTaswayaReport, xtraTabControlPurchases);
            objForm.TopLevel = false;
            xtraTabPage.Controls.Add(objForm);
            objForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            objForm.Dock = DockStyle.Fill;
            objForm.Show();
        }

        /*public void bindUpdateBillPurchasesPriceForm(DataRow rows)
        {
            if (!xtraTabControlPurchases.Visible)
                xtraTabControlPurchases.Visible = true;

            XtraTabPage xtraTabPage = getTabPage(xtraTabControlPurchases, "تعديل اسعار الشراء للفاتورة");

            if (xtraTabPage == null)
            {
                xtraTabControlPurchases.TabPages.Add("تعديل اسعار الشراء للفاتورة");
                xtraTabPage = getTabPage(xtraTabControlPurchases, "تعديل اسعار الشراء للفاتورة");
            }
            xtraTabPage.Controls.Clear();

            xtraTabControlPurchases.SelectedTabPage = xtraTabPage;

            UpdateBillPurchasesPrice objForm = new UpdateBillPurchasesPrice(rows);
            objForm.TopLevel = false;

            xtraTabPage.Controls.Add(objForm);
            objForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            objForm.Dock = DockStyle.Fill;
            objForm.Show();
        }*/
    }
}

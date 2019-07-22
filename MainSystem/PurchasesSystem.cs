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
using MainSystem.Sales.accounting;

namespace MainSystem
{
    class PurchasesSystem
    {
    }
    public partial class MainForm
    {
        public static XtraTabControl tabControlPurchases;
        public static LeastQuantityReport leastQuantityReport;
        public static SpecialOrders_Report2 SpecialOrdersReport;

        Timer Purchasetimer = new Timer();
        //bool purchaseFlag = false;

        public void PurchasesMainForm()
        {
            try
            {
                if (UserControl.userType == 10 || UserControl.userType == 1)
                {
                    LeastQuantityFunction();
                    ConfirmedSpecialOrdersFunction();

                    Purchasetimer.Interval = 1000 * 60;
                    Purchasetimer.Tick += new EventHandler(GetNonRequestedLeastQuantity);
                    Purchasetimer.Tick += new EventHandler(GetConfirmedSpecialOrder);
                    Purchasetimer.Start();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void pictureBoxPurchase_Click(object sender, EventArgs e)
        {
            try
            {
                if (UserControl.userType == 10 || UserControl.userType == 1 /*|| UserControl.userType == 2*/)
                {
                    //if (purchaseFlag == false)
                    //{
                        if (!xtraTabControlMainContainer.TabPages.Contains(xtraTabPagePurchases))
                        {
                            if (index == 0)
                            {
                                xtraTabControlMainContainer.TabPages.Insert(1, PurchasesTP);
                            }
                            else
                            {
                                xtraTabControlMainContainer.TabPages.Insert(index, PurchasesTP);
                            }
                            index++;
                            //purchaseFlag = true;
                        }
                    //}
                    xtraTabControlMainContainer.SelectedTabPage = PurchasesTP;

                    if (!xtraTabControlPurchases.Visible)
                        xtraTabControlPurchases.Visible = true;

                    XtraTabPage xtraTabPage = getTabPage(xtraTabControlPurchases, "عرض الطلبات الخاصة");
                    if (xtraTabPage == null)
                    {
                        xtraTabControlPurchases.TabPages.Add("عرض الطلبات الخاصة");
                        xtraTabPage = getTabPage(xtraTabControlPurchases, "عرض الطلبات الخاصة");
                    }
                    xtraTabPage.Controls.Clear();

                    xtraTabControlPurchases.SelectedTabPage = xtraTabPage;
                    bindDisplaySpecialOrdersReport(xtraTabPage);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void pictureBoxPurcaseLeast_Click(object sender, EventArgs e)
        {
            try
            {
                if (UserControl.userType == 10 || UserControl.userType == 1)
                {
                    //if (purchaseFlag == false)
                    //{
                        if (!xtraTabControlMainContainer.TabPages.Contains(xtraTabPagePurchases))
                        {
                            if (index == 0)
                            {
                                xtraTabControlMainContainer.TabPages.Insert(1, PurchasesTP);
                            }
                            else
                            {
                                xtraTabControlMainContainer.TabPages.Insert(index, PurchasesTP);
                            }
                            index++;
                            //purchaseFlag = true;
                        }
                    //}
                    xtraTabControlMainContainer.SelectedTabPage = PurchasesTP;

                    if (!xtraTabControlPurchases.Visible)
                        xtraTabControlPurchases.Visible = true;

                    XtraTabPage xtraTabPage = getTabPage(xtraTabControlPurchases, "عرض البنود المطلوبة");
                    if (xtraTabPage == null)
                    {
                        xtraTabControlPurchases.TabPages.Add("عرض البنود المطلوبة");
                        xtraTabPage = getTabPage(xtraTabControlPurchases, "عرض البنود المطلوبة");
                    }
                    xtraTabPage.Controls.Clear();

                    xtraTabControlPurchases.SelectedTabPage = xtraTabPage;
                    bindDisplayLeastQuantityReport(xtraTabPage);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void navBarItemPurchasesPricesRecord_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            try
            {
                if (UserControl.userType == 10 || UserControl.userType == 1 || UserControl.userType == 2)
                {
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
                }
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
                if (UserControl.userType == 10 || UserControl.userType == 1)
                {
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
                }
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
                if (UserControl.userType == 10 || UserControl.userType == 1)
                {
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
                }
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
                if (UserControl.userType == 10 || UserControl.userType == 1)
                {
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
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void navBarItemLeastQuantity_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            try
            {
                if (UserControl.userType == 10 || UserControl.userType == 1)
                {
                    restForeColorOfNavBarItem();
                    NavBarItem navBarItem = (NavBarItem)sender;
                    navBarItem.Appearance.ForeColor = Color.Blue;

                    if (!xtraTabControlPurchases.Visible)
                        xtraTabControlPurchases.Visible = true;

                    XtraTabPage xtraTabPage = getTabPage(xtraTabControlPurchases, "تسجيل الحد الادنى للبنود");
                    if (xtraTabPage == null)
                    {
                        xtraTabControlPurchases.TabPages.Add("تسجيل الحد الادنى للبنود");
                        xtraTabPage = getTabPage(xtraTabControlPurchases, "تسجيل الحد الادنى للبنود");
                    }
                    xtraTabPage.Controls.Clear();

                    xtraTabControlPurchases.SelectedTabPage = xtraTabPage;
                    bindDisplayLeastQuantityForm(xtraTabPage);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void navBarItemOrderReport_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            try
            {
                if (UserControl.userType == 10 || UserControl.userType == 1)
                {
                    restForeColorOfNavBarItem();
                    NavBarItem navBarItem = (NavBarItem)sender;
                    navBarItem.Appearance.ForeColor = Color.Blue;

                    if (!xtraTabControlPurchases.Visible)
                        xtraTabControlPurchases.Visible = true;

                    XtraTabPage xtraTabPage = getTabPage(xtraTabControlPurchases, "عرض الطلبيات");
                    if (xtraTabPage == null)
                    {
                        xtraTabControlPurchases.TabPages.Add("عرض الطلبيات");
                        xtraTabPage = getTabPage(xtraTabControlPurchases, "عرض الطلبيات");
                    }
                    xtraTabPage.Controls.Clear();

                    xtraTabControlPurchases.SelectedTabPage = xtraTabPage;
                    bindDisplayOrderReportForm(xtraTabPage);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void navBarItemExpectedOrders_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            try
            {
                if (UserControl.userType == 10 || UserControl.userType == 1)
                {
                    restForeColorOfNavBarItem();
                    NavBarItem navBarItem = (NavBarItem)sender;
                    navBarItem.Appearance.ForeColor = Color.Blue;
                    if (!xtraTabControlPurchases.Visible)
                        xtraTabControlPurchases.Visible = true;

                    XtraTabPage xtraTabPage = getTabPage(xtraTabControlPurchases, "الطلبات بتاريخ الاستلام");
                    if (xtraTabPage == null)
                    {
                        xtraTabControlPurchases.TabPages.Add("الطلبات بتاريخ الاستلام");
                        xtraTabPage = getTabPage(xtraTabControlPurchases, "الطلبات بتاريخ الاستلام");
                    }

                    xtraTabPage.Controls.Clear();
                    xtraTabControlPurchases.SelectedTabPage = xtraTabPage;

                    SearchRecive_Date objForm = new SearchRecive_Date(this, xtraTabControlPurchases);

                    objForm.TopLevel = false;
                    xtraTabPage.Controls.Add(objForm);
                    objForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                    objForm.Dock = DockStyle.Fill;
                    objForm.Show();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void navBarItemRequestedOrders_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            try
            {
                if (UserControl.userType == 10 || UserControl.userType == 1)
                {
                    restForeColorOfNavBarItem();
                    NavBarItem navBarItem = (NavBarItem)sender;
                    navBarItem.Appearance.ForeColor = Color.Blue;
                    if (!xtraTabControlPurchases.Visible)
                        xtraTabControlPurchases.Visible = true;

                    XtraTabPage xtraTabPage = getTabPage(xtraTabControlPurchases, "الطلبات بتاريخ الطلب");
                    if (xtraTabPage == null)
                    {
                        xtraTabControlPurchases.TabPages.Add("الطلبات بتاريخ الطلب");
                        xtraTabPage = getTabPage(xtraTabControlPurchases, "الطلبات بتاريخ الطلب");
                    }

                    xtraTabPage.Controls.Clear();
                    xtraTabControlPurchases.SelectedTabPage = xtraTabPage;

                    SearchRequest_Date objForm = new SearchRequest_Date(this, xtraTabControlPurchases);

                    objForm.TopLevel = false;
                    xtraTabPage.Controls.Add(objForm);
                    objForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                    objForm.Dock = DockStyle.Fill;
                    objForm.Show();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void navBarItemOneOrder_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            try
            {
                if (UserControl.userType == 10 || UserControl.userType == 1)
                {
                    restForeColorOfNavBarItem();
                    NavBarItem navBarItem = (NavBarItem)sender;
                    navBarItem.Appearance.ForeColor = Color.Blue;
                    if (!xtraTabControlPurchases.Visible)
                        xtraTabControlPurchases.Visible = true;

                    XtraTabPage xtraTabPage = getTabPage(xtraTabControlPurchases, "عرض طلب");
                    if (xtraTabPage == null)
                    {
                        xtraTabControlPurchases.TabPages.Add("عرض طلب");
                        xtraTabPage = getTabPage(xtraTabControlPurchases, "عرض طلب");
                    }

                    xtraTabPage.Controls.Clear();
                    xtraTabControlPurchases.SelectedTabPage = xtraTabPage;

                    OrderStored objForm = new OrderStored(this, xtraTabControlPurchases);

                    objForm.TopLevel = false;
                    xtraTabPage.Controls.Add(objForm);
                    objForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                    objForm.Dock = DockStyle.Fill;
                    objForm.Show();
                }
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
                if (UserControl.userType == 10 || UserControl.userType == 1)
                {
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
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void navBarItemDashOrderReport_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            try
            {
                if (UserControl.userType == 10 || UserControl.userType == 1)
                {
                    restForeColorOfNavBarItem();
                    NavBarItem navBarItem = (NavBarItem)sender;
                    navBarItem.Appearance.ForeColor = Color.Blue;

                    if (!xtraTabControlPurchases.Visible)
                        xtraTabControlPurchases.Visible = true;

                    XtraTabPage xtraTabPage = getTabPage(xtraTabControlPurchases, "عرض الطلبيات المؤقتة");
                    if (xtraTabPage == null)
                    {
                        xtraTabControlPurchases.TabPages.Add("عرض الطلبيات المؤقتة");
                        xtraTabPage = getTabPage(xtraTabControlPurchases, "عرض الطلبيات المؤقتة");
                    }
                    xtraTabPage.Controls.Clear();

                    xtraTabControlPurchases.SelectedTabPage = xtraTabPage;
                    bindDisplayDashOrderReportForm(xtraTabPage);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void navBarItemDashRequestReport_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            try
            {
                if (UserControl.userType == 10 || UserControl.userType == 1)
                {
                    restForeColorOfNavBarItem();
                    NavBarItem navBarItem = (NavBarItem)sender;
                    navBarItem.Appearance.ForeColor = Color.Blue;

                    if (!xtraTabControlPurchases.Visible)
                        xtraTabControlPurchases.Visible = true;

                    XtraTabPage xtraTabPage = getTabPage(xtraTabControlPurchases, "عرض الطلبيات الخاصة المؤقتة");
                    if (xtraTabPage == null)
                    {
                        xtraTabControlPurchases.TabPages.Add("عرض الطلبيات الخاصة المؤقتة");
                        xtraTabPage = getTabPage(xtraTabControlPurchases, "عرض الطلبيات الخاصة المؤقتة");
                    }
                    xtraTabPage.Controls.Clear();

                    xtraTabControlPurchases.SelectedTabPage = xtraTabPage;
                    bindDisplayDashRequestReportForm(xtraTabPage);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void navBarItemSalesProductsDate_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            try
            {
                if (UserControl.userType == 10 || UserControl.userType == 1)
                {
                    restForeColorOfNavBarItem();
                    NavBarItem navBarItem = (NavBarItem)sender;
                    navBarItem.Appearance.ForeColor = Color.Blue;

                    if (!xtraTabControlPurchases.Visible)
                        xtraTabControlPurchases.Visible = true;

                    XtraTabPage xtraTabPage = getTabPage(xtraTabControlPurchases, "الفواتير المباعة بالتاريخ");
                    if (xtraTabPage == null)
                    {
                        xtraTabControlPurchases.TabPages.Add("الفواتير المباعة بالتاريخ");
                        xtraTabPage = getTabPage(xtraTabControlPurchases, "الفواتير المباعة بالتاريخ");
                    }
                    xtraTabPage.Controls.Clear();

                    xtraTabControlPurchases.SelectedTabPage = xtraTabPage;
                    bindDisplaySalesProductsBillsDateForm(xtraTabPage);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //Special Orders Report
        public void bindDisplaySpecialOrdersReport(XtraTabPage xtraTabPage)
        {
            SpecialOrdersReport = new SpecialOrders_Report2(this);
            SpecialOrdersReport.TopLevel = false;

            xtraTabPage.Controls.Add(SpecialOrdersReport);
            SpecialOrdersReport.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            SpecialOrdersReport.Dock = DockStyle.Fill;
            SpecialOrdersReport.Show();
        }
        public void bindDisplayLeastQuantityReport(XtraTabPage xtraTabPage)
        {
            leastQuantityReport = new LeastQuantityReport(this, xtraTabControlPurchases);
            leastQuantityReport.TopLevel = false;

            xtraTabPage.Controls.Add(leastQuantityReport);
            leastQuantityReport.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            leastQuantityReport.Dock = DockStyle.Fill;
            leastQuantityReport.Show();
        }

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

        public void bindDisplayLeastQuantityForm(XtraTabPage xtraTabPage)
        {
            LeastQuantityRecord objForm = new LeastQuantityRecord(this, xtraTabControlPurchases);
            objForm.TopLevel = false;

            xtraTabPage.Controls.Add(objForm);
            objForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            objForm.Dock = DockStyle.Fill;
            objForm.Show();
        }

        public void bindDisplayOrderReportForm(XtraTabPage xtraTabPage)
        {
            Order_Report objForm = new Order_Report(this, xtraTabControlPurchases);
            objForm.TopLevel = false;

            xtraTabPage.Controls.Add(objForm);
            objForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            objForm.Dock = DockStyle.Fill;
            objForm.Show();
        }
        public void bindRecordOrderForm(Order_Report OrderReport, List<DataRow> row1)
        {
            if (!xtraTabControlPurchases.Visible)
                xtraTabControlPurchases.Visible = true;

            XtraTabPage xtraTabPage = getTabPage(xtraTabControlPurchases, "اضافة طلب");
            if (xtraTabPage == null)
            {
                xtraTabControlPurchases.TabPages.Add("اضافة طلب");
                xtraTabPage = getTabPage(xtraTabControlPurchases, "اضافة طلب");
            }
            xtraTabPage.Controls.Clear();

            xtraTabControlPurchases.SelectedTabPage = xtraTabPage;
            //List<DataRow> row1 = new List<DataRow>();
            Order_Record objForm = new Order_Record(row1, OrderReport, xtraTabControlPurchases);
            objForm.TopLevel = false;

            xtraTabPage.Controls.Add(objForm);
            objForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            objForm.Dock = DockStyle.Fill;
            objForm.Show();
        }
        public void bindRecordDashOrderForm(DashOrder_Report DashOrderReport, List<DataRow> row1/*, int SpecialOrderId*/)
        {
            if (!xtraTabControlPurchases.Visible)
                xtraTabControlPurchases.Visible = true;

            XtraTabPage xtraTabPage = getTabPage(xtraTabControlPurchases, "اضافة طلب مؤقت");
            if (xtraTabPage == null)
            {
                xtraTabControlPurchases.TabPages.Add("اضافة طلب مؤقت");
                xtraTabPage = getTabPage(xtraTabControlPurchases, "اضافة طلب مؤقت");
            }
            xtraTabPage.Controls.Clear();

            xtraTabControlPurchases.SelectedTabPage = xtraTabPage;
            //List<DataRow> row1 = new List<DataRow>();
            DashOrder_Record objForm = new DashOrder_Record(row1, DashOrderReport, xtraTabControlPurchases/*, SpecialOrderId*/);
            objForm.TopLevel = false;

            xtraTabPage.Controls.Add(objForm);
            objForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            objForm.Dock = DockStyle.Fill;
            objForm.Show();
        }
        public void bindDisplayDashOrderReportForm(XtraTabPage xtraTabPage)
        {
            DashOrder_Report objForm = new DashOrder_Report(this, xtraTabControlPurchases);
            objForm.TopLevel = false;

            xtraTabPage.Controls.Add(objForm);
            objForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            objForm.Dock = DockStyle.Fill;
            objForm.Show();
        }
        public void bindRecordDashRequestForm(DashRequest_Report DashOrderReport, List<DataRow> row1)
        {
            if (!xtraTabControlPurchases.Visible)
                xtraTabControlPurchases.Visible = true;

            XtraTabPage xtraTabPage = getTabPage(xtraTabControlPurchases, "اضافة طلب خاص مؤقت");
            if (xtraTabPage == null)
            {
                xtraTabControlPurchases.TabPages.Add("اضافة طلب خاص مؤقت");
                xtraTabPage = getTabPage(xtraTabControlPurchases, "اضافة طلب خاص مؤقت");
            }
            xtraTabPage.Controls.Clear();

            xtraTabControlPurchases.SelectedTabPage = xtraTabPage;
            DashRequest_Record objForm = new DashRequest_Record(DashOrderReport, xtraTabControlPurchases);
            objForm.TopLevel = false;

            xtraTabPage.Controls.Add(objForm);
            objForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            objForm.Dock = DockStyle.Fill;
            objForm.Show();
        }
        public void bindRecordRequestForm(Request_Report OrderReport, List<DataRow> row1)
        {
            if (!xtraTabControlPurchases.Visible)
                xtraTabControlPurchases.Visible = true;

            XtraTabPage xtraTabPage = getTabPage(xtraTabControlPurchases, "اضافة طلب خاص");
            if (xtraTabPage == null)
            {
                xtraTabControlPurchases.TabPages.Add("اضافة طلب خاص");
                xtraTabPage = getTabPage(xtraTabControlPurchases, "اضافة طلب خاص");
            }
            xtraTabPage.Controls.Clear();

            xtraTabControlPurchases.SelectedTabPage = xtraTabPage;
            //List<DataRow> row1 = new List<DataRow>();
            Request_Record objForm = new Request_Record(row1, OrderReport, xtraTabControlPurchases);
            objForm.TopLevel = false;

            xtraTabPage.Controls.Add(objForm);
            objForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            objForm.Dock = DockStyle.Fill;
            objForm.Show();
        }
        public void bindDisplayDashRequestReportForm(XtraTabPage xtraTabPage)
        {
            DashRequest_Report objForm = new DashRequest_Report(this, xtraTabControlPurchases);
            objForm.TopLevel = false;

            xtraTabPage.Controls.Add(objForm);
            objForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            objForm.Dock = DockStyle.Fill;
            objForm.Show();
        }
        public void bindDisplaySalesProductsBillsDateForm(XtraTabPage xtraTabPage)
        {
            SalesProductsBillsDate_Report objForm = new SalesProductsBillsDate_Report(this);
            objForm.TopLevel = false;

            xtraTabPage.Controls.Add(objForm);
            objForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            objForm.Dock = DockStyle.Fill;
            objForm.Show();
        }
        public void bindUpdateOrderForm(DataRowView rows, Order_Report OrderReport)
        {
            /*if (!xtraTabControlPurchases.Visible)
                xtraTabControlPurchases.Visible = true;

            XtraTabPage xtraTabPage = getTabPage(xtraTabControlPurchases, "تعديل طلب");

            if (xtraTabPage == null)
            {
                xtraTabControlPurchases.TabPages.Add("تعديل طلب");
                xtraTabPage = getTabPage(xtraTabControlPurchases, "تعديل طلب");
            }
            xtraTabPage.Controls.Clear();

            xtraTabControlPurchases.SelectedTabPage = xtraTabPage;

            UpdateSupplier objForm = new UpdateSupplier(rows, /*OrderReport*null, xtraTabControlPurchases);
            objForm.TopLevel = false;

            xtraTabPage.Controls.Add(objForm);
            objForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            objForm.Dock = DockStyle.Fill;
            objForm.Show();*/
        }
        public void bindUpdateRequestForm(DataRowView rows, Request_Report OrderReport)
        {
            /*if (!xtraTabControlPurchases.Visible)
                xtraTabControlPurchases.Visible = true;

            XtraTabPage xtraTabPage = getTabPage(xtraTabControlPurchases, "تعديل طلب خاص");

            if (xtraTabPage == null)
            {
                xtraTabControlPurchases.TabPages.Add("تعديل طلب خاص");
                xtraTabPage = getTabPage(xtraTabControlPurchases, "تعديل طلب خاص");
            }
            xtraTabPage.Controls.Clear();

            xtraTabControlPurchases.SelectedTabPage = xtraTabPage;

            UpdateSupplier objForm = new UpdateSupplier(rows, /*OrderReport*null, xtraTabControlPurchases);
            objForm.TopLevel = false;

            xtraTabPage.Controls.Add(objForm);
            objForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            objForm.Dock = DockStyle.Fill;
            objForm.Show();*/
        }
        public void bindPrintOrderForm()
        {
            /*if (!xtraTabControlPurchases.Visible)
                xtraTabControlPurchases.Visible = true;

            XtraTabPage xtraTabPage = getTabPage(xtraTabControlPurchases, "طباعة الطلبات");
            if (xtraTabPage == null)
            {
                xtraTabControlPurchases.TabPages.Add("طباعة الطلبات");
                xtraTabPage = getTabPage(xtraTabControlPurchases, "طباعة الطلبات");
            }
            xtraTabPage.Controls.Clear();

            xtraTabControlPurchases.SelectedTabPage = xtraTabPage;
            Supplier_Print objForm = new Supplier_Print();
            objForm.TopLevel = false;

            xtraTabPage.Controls.Add(objForm);
            objForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            objForm.Dock = DockStyle.Fill;
            objForm.Show();*/
        }

        public void GetNonRequestedLeastQuantity(object sender, EventArgs e)
        {
            try
            {
                LeastQuantityFunction();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }

        public void GetConfirmedSpecialOrder(object sender, EventArgs e)
        {
            try
            {
                ConfirmedSpecialOrdersFunction();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }

        public void LeastQuantityFunction()
        {
            if (UserControl.userType == 10 || UserControl.userType == 1)
            {
                string q1 = "select Data_ID from storage_least_taswya";
                string q2 = "SELECT order_details.Data_ID FROM orders INNER JOIN order_details ON order_details.Order_ID = orders.Order_ID where orders.Received=0";
                int count = 0;
                dbconnection.Close();
                string query = "SELECT least_order.Least_Quantity FROM least_order INNER JOIN data ON least_order.Data_ID = data.Data_ID INNER JOIN storage ON storage.Data_ID = data.Data_ID group by data.Data_ID having (SUM(storage.Total_Meters) <= least_order.Least_Quantity=1) and data.Data_ID not in(" + q1 + ") and data.Data_ID not in(" + q2 + ")";
                MySqlCommand command = new MySqlCommand(query, dbconnection);
                dbconnection.Open();
                MySqlDataReader dr = command.ExecuteReader();
                if (dr.HasRows)
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            count++;
                        }
                        labelPurchaseLeast.Text = count.ToString();
                        labelPurchaseLeast.Visible = true;
                        dr.Close();
                    }
                }
                else
                {
                    labelPurchaseLeast.Text = "0";
                    labelPurchaseLeast.Visible = true;
                }
            }
        }

        public void ConfirmedSpecialOrdersFunction()
        {
            if (UserControl.userType == 10 || UserControl.userType == 1)
            {
                dbconnection.Close();
                //INNER JOIN orders ON special_order.SpecialOrder_ID = orders.SpecialOrder_ID 
                string query = "SELECT Count(special_order.SpecialOrder_ID) FROM special_order INNER JOIN dash ON special_order.Dash_ID = dash.Dash_ID where special_order.Record=0 and special_order.Confirmed=1 and special_order.Canceled=0" /* AND dash.Branch_ID=" + EmpBranchId*/;
                MySqlCommand command = new MySqlCommand(query, dbconnection);
                dbconnection.Open();
                string reader = command.ExecuteScalar().ToString();
                labelNotifySpecialOrderPurchase.Text = reader;
                if (Convert.ToInt32(reader) > 0)
                {
                    labelNotifySpecialOrderPurchase.Visible = true;
                }
                else
                {
                    labelNotifySpecialOrderPurchase.Visible = false;
                }
            }
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

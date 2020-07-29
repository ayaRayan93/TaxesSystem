﻿using System;
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
using TaxesSystem.Reports.sales;

namespace TaxesSystem
{
    class SalesSystem
    {
    }
    public partial class MainForm
    {
        public static XtraTabControl tabControlSales;
        public static SpecialOrderConfirm specialOrderConfirm;
        public static Bill_Confirm objFormBillConfirm;
        public static Customer_Report objFormCustomer;
        public static XtraTabPage MainTabPageAddCustomer;
        public static XtraTabPage MainTabPageUpdateCustomer;
        public static XtraTabPage MainTabPagePrintCustomer;
        
        Timer timer = new Timer();
        int EmpBranchId = 0;

        public void SalesMainForm()
        {
            try
            {
                tabControlSales = xtraTabControlSalesContent;

                MainTabPageAddCustomer = new XtraTabPage();
                MainTabPageUpdateCustomer = new XtraTabPage();
                MainTabPagePrintCustomer = new XtraTabPage();

                EmpBranchId = UserControl.EmpBranchID;

                if (/*UserControl.userType == 7 || */UserControl.userType == 6/* || UserControl.userType == 13*/ || UserControl.userType == 1 || UserControl.userType == 15)
                {
                    SpecialOrdersFunction();

                    //Calculate the time of the actual work of the delegates
                    timer.Interval = 1000 * 60;
                    timer.Tick += new EventHandler(GetNonRequestedSpecialOrders);
                    timer.Start();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }
        
        private void navBarItemCustomers_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            try
            {
                if (UserControl.userType == 28 || UserControl.userType == 6 || UserControl.userType == 13 || UserControl.userType == 18 || UserControl.userType == 17 || UserControl.userType == 1 || UserControl.userType == 15 || UserControl.userType == 16|| UserControl.userType == 7)
                {
                    restForeColorOfNavBarItem();
                    NavBarItem navBarItem = (NavBarItem)sender;
                    navBarItem.Appearance.ForeColor = Color.Blue;

                    if (!xtraTabControlSalesContent.Visible)
                        xtraTabControlSalesContent.Visible = true;

                    XtraTabPage xtraTabPage = getTabPage(xtraTabControlSalesContent, "العملاء");
                    if (xtraTabPage == null)
                    {
                        xtraTabControlSalesContent.TabPages.Add("العملاء");
                        xtraTabPage = getTabPage(xtraTabControlSalesContent, "العملاء");
                    }
                    xtraTabPage.Controls.Clear();

                    xtraTabControlSalesContent.SelectedTabPage = xtraTabPage;
                    bindDisplayCustomersForm(xtraTabPage);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void navBarItemConfirmBill_LinkClicked2(object sender, NavBarLinkEventArgs e)
        {
            try
            {
                if (UserControl.userType == 28 || UserControl.userType == 6 || UserControl.userType == 1|| UserControl.userType == 15 || UserControl.userType == 16)
                {
                    restForeColorOfNavBarItem();
                    NavBarItem navBarItem = (NavBarItem)sender;
                    navBarItem.Appearance.ForeColor = Color.Blue;

                    if (!xtraTabControlSalesContent.Visible)
                        xtraTabControlSalesContent.Visible = true;

                    XtraTabPage xtraTabPage = getTabPage("تاكيد الفاتورة");
                    if (xtraTabPage == null)
                    {
                        xtraTabControlSalesContent.TabPages.Add("تاكيد الفاتورة");
                        xtraTabPage = getTabPage("تاكيد الفاتورة");
                    }
                    xtraTabPage.Controls.Clear();

                    xtraTabControlSalesContent.SelectedTabPage = xtraTabPage;
                    bindDisplayConfirmBillForm(xtraTabPage);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnProductSellPrice_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            try
            {
                if (UserControl.userType == 7 || UserControl.userType == 1 || UserControl.userType == 13)
                {
                    restForeColorOfNavBarItem();
                    NavBarItem navBarItem = (NavBarItem)sender;
                    navBarItem.Appearance.ForeColor = Color.Blue;

                    if (!xtraTabControlSalesContent.Visible)
                        xtraTabControlSalesContent.Visible = true;

                    XtraTabPage xtraTabPage = getTabPage("اسعار بيع البنود");
                    if (xtraTabPage == null)
                    {
                        xtraTabControlSalesContent.TabPages.Add("اسعار بيع البنود");
                        xtraTabPage = getTabPage("اسعار بيع البنود");
                    }
                    xtraTabPage.Controls.Clear();

                    xtraTabControlSalesContent.SelectedTabPage = xtraTabPage;
                    bindDisplayProductsSellPriceForm(xtraTabPage);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /*private void btnOffers_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            try
            {
                restForeColorOfNavBarItem();
                NavBarItem navBarItem = (NavBarItem)sender;
                navBarItem.Appearance.ForeColor = Color.Blue;

                if (!xtraTabControlSalesContent.Visible)
                    xtraTabControlSalesContent.Visible = true;

                XtraTabPage xtraTabPage = getTabPage("العروض");
                if (xtraTabPage == null)
                {
                    xtraTabControlSalesContent.TabPages.Add("العروض");
                    xtraTabPage = getTabPage("العروض");
                }
                xtraTabPage.Controls.Clear();

                xtraTabControlSalesContent.SelectedTabPage = xtraTabPage;
                bindDisplayOffersForm(xtraTabPage);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }*/
        
        private void navBarItemOfferReport_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            try
            {
                if (UserControl.userType == 7 || UserControl.userType == 1|| UserControl.userType == 15 || UserControl.userType == 13)
                {
                    restForeColorOfNavBarItem();
                    NavBarItem navBarItem = (NavBarItem)sender;
                    navBarItem.Appearance.ForeColor = Color.Blue;
                    if (!xtraTabControlSalesContent.Visible)
                        xtraTabControlSalesContent.Visible = true;

                    XtraTabPage xtraTabPage = getTabPage(xtraTabControlSalesContent, "عرض العروض");
                    if (xtraTabPage == null)
                    {
                        xtraTabControlSalesContent.TabPages.Add("عرض العروض");
                        xtraTabPage = getTabPage(xtraTabControlSalesContent, "عرض العروض");
                    }

                    xtraTabPage.Controls.Clear();
                    xtraTabControlSalesContent.SelectedTabPage = xtraTabPage;

                    Offer objForm = new Offer(this);

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
        
        private void navBarItemOfferStorage_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            try
            {
                if (UserControl.userType == 7 || UserControl.userType == 1|| UserControl.userType == 15 || UserControl.userType == 13)
                {
                    restForeColorOfNavBarItem();
                    NavBarItem navBarItem = (NavBarItem)sender;
                    navBarItem.Appearance.ForeColor = Color.Blue;
                    if (!xtraTabControlSalesContent.Visible)
                        xtraTabControlSalesContent.Visible = true;

                    XtraTabPage xtraTabPage = getTabPage(xtraTabControlSalesContent, "تسجيل كميات العروض");
                    if (xtraTabPage == null)
                    {
                        xtraTabControlSalesContent.TabPages.Add("تسجيل كميات العروض");
                        xtraTabPage = getTabPage(xtraTabControlSalesContent, "تسجيل كميات العروض");
                    }

                    xtraTabPage.Controls.Clear();
                    xtraTabControlSalesContent.SelectedTabPage = xtraTabPage;

                    OfferStorage objForm = new OfferStorage(this);

                    objForm.TopLevel = false;
                    xtraTabPage.Controls.Add(objForm);
                    objForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                    objForm.Dock = DockStyle.Fill;
                    //objForm.DisplayAtaqm();
                    objForm.Show();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        
        private void navBarItemReturnBillRecord_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            try
            {
                if (UserControl.userType == 28 || UserControl.userType == 6 || UserControl.userType == 1|| UserControl.userType == 15 || UserControl.userType == 16)
                {
                    restForeColorOfNavBarItem();
                    NavBarItem navBarItem = (NavBarItem)sender;
                    navBarItem.Appearance.ForeColor = Color.Blue;
                    if (!xtraTabControlSalesContent.Visible)
                        xtraTabControlSalesContent.Visible = true;

                    XtraTabPage xtraTabPage = getTabPage(xtraTabControlSalesContent, "تسجيل فاتورة مرتجع");
                    if (xtraTabPage == null)
                    {
                        xtraTabControlSalesContent.TabPages.Add("تسجيل فاتورة مرتجع");
                        xtraTabPage = getTabPage(xtraTabControlSalesContent, "تسجيل فاتورة مرتجع");
                    }

                    xtraTabPage.Controls.Clear();
                    xtraTabControlSalesContent.SelectedTabPage = xtraTabPage;

                    CustomerReturnBill objForm = new CustomerReturnBill(this);

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

        private void navBarItemSalesTransitions_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            try
            {
                if (UserControl.userType == 28 || UserControl.userType == 6 || UserControl.userType == 13 || UserControl.userType == 18 || UserControl.userType == 17 || UserControl.userType == 1|| UserControl.userType == 15 || UserControl.userType == 16|| UserControl.userType == 7 )
                {
                    restForeColorOfNavBarItem();
                    NavBarItem navBarItem = (NavBarItem)sender;
                    navBarItem.Appearance.ForeColor = Color.Blue;
                    if (!xtraTabControlSalesContent.Visible)
                        xtraTabControlSalesContent.Visible = true;

                    XtraTabPage xtraTabPage = getTabPage(xtraTabControlSalesContent, "تقرير حركة السداد");
                    if (xtraTabPage == null)
                    {
                        xtraTabControlSalesContent.TabPages.Add("تقرير حركة السداد");
                        xtraTabPage = getTabPage(xtraTabControlSalesContent, "تقرير حركة السداد");
                    }

                    xtraTabPage.Controls.Clear();
                    xtraTabControlSalesContent.SelectedTabPage = xtraTabPage;

                    Bills_Transitions_Report objForm = new Bills_Transitions_Report(this);

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

        private void navBarItemBillCopy_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            try
            {
                if (UserControl.userType == 28 || UserControl.userType == 25 || UserControl.userType == 6 || UserControl.userType == 13 || UserControl.userType == 18 || UserControl.userType == 17 || UserControl.userType == 1|| UserControl.userType == 15 || UserControl.userType == 16|| UserControl.userType == 7)
                {
                    restForeColorOfNavBarItem();
                    NavBarItem navBarItem = (NavBarItem)sender;
                    navBarItem.Appearance.ForeColor = Color.Blue;
                    if (!xtraTabControlSalesContent.Visible)
                        xtraTabControlSalesContent.Visible = true;

                    XtraTabPage xtraTabPage = getTabPage(xtraTabControlSalesContent, "استعلام عن فاتورة بيع");
                    if (xtraTabPage == null)
                    {
                        xtraTabControlSalesContent.TabPages.Add("استعلام عن فاتورة بيع");
                        xtraTabPage = getTabPage(xtraTabControlSalesContent, "استعلام عن فاتورة بيع");
                    }

                    xtraTabPage.Controls.Clear();
                    xtraTabControlSalesContent.SelectedTabPage = xtraTabPage;

                    Bills_Copy_Report objForm = new Bills_Copy_Report(this);

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

        private void navBarItemReturnedBillCopy_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            try
            {
                if (UserControl.userType == 28 || UserControl.userType == 25 || UserControl.userType == 6 || UserControl.userType == 13 || UserControl.userType == 18 || UserControl.userType == 17 || UserControl.userType == 1|| UserControl.userType == 15 || UserControl.userType == 16|| UserControl.userType == 7)
                {
                    restForeColorOfNavBarItem();
                    NavBarItem navBarItem = (NavBarItem)sender;
                    navBarItem.Appearance.ForeColor = Color.Blue;
                    if (!xtraTabControlSalesContent.Visible)
                        xtraTabControlSalesContent.Visible = true;

                    XtraTabPage xtraTabPage = getTabPage(xtraTabControlSalesContent, "استعلام عن فاتورة مرتجع");
                    if (xtraTabPage == null)
                    {
                        xtraTabControlSalesContent.TabPages.Add("استعلام عن فاتورة مرتجع");
                        xtraTabPage = getTabPage(xtraTabControlSalesContent, "استعلام عن فاتورة مرتجع");
                    }

                    xtraTabPage.Controls.Clear();
                    xtraTabControlSalesContent.SelectedTabPage = xtraTabPage;

                    ReturnedBills_Copy_Report objForm = new ReturnedBills_Copy_Report(this);

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

        private void navBarItemTransitionCopy_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            try
            {
                if (UserControl.userType == 28 || UserControl.userType == 25 || UserControl.userType == 6 || UserControl.userType == 13 || UserControl.userType == 18 || UserControl.userType == 17 || UserControl.userType == 1|| UserControl.userType == 15 || UserControl.userType == 16|| UserControl.userType == 7)
                {
                    restForeColorOfNavBarItem();
                    NavBarItem navBarItem = (NavBarItem)sender;
                    navBarItem.Appearance.ForeColor = Color.Blue;
                    if (!xtraTabControlSalesContent.Visible)
                        xtraTabControlSalesContent.Visible = true;

                    XtraTabPage xtraTabPage = getTabPage(xtraTabControlSalesContent, "استعلام عن سداد");
                    if (xtraTabPage == null)
                    {
                        xtraTabControlSalesContent.TabPages.Add("استعلام عن سداد");
                        xtraTabPage = getTabPage(xtraTabControlSalesContent, "استعلام عن سداد");
                    }

                    xtraTabPage.Controls.Clear();
                    xtraTabControlSalesContent.SelectedTabPage = xtraTabPage;

                    BillTransitionCopy_Report objForm = new BillTransitionCopy_Report(this);

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

        private void navBarItemReturnWithPermision_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            try
            {
                if (UserControl.userType == 28 || UserControl.userType == 6 || UserControl.userType == 1|| UserControl.userType == 15 || UserControl.userType == 16)
                {
                    restForeColorOfNavBarItem();
                    NavBarItem navBarItem = (NavBarItem)sender;
                    navBarItem.Appearance.ForeColor = Color.Blue;

                    if (!xtraTabControlSalesContent.Visible)
                        xtraTabControlSalesContent.Visible = true;

                    XtraTabPage xtraTabPage = getTabPage(xtraTabControlSalesContent, "تسجيل مرتجع باذن مخزن");
                    if (xtraTabPage == null)
                    {
                        xtraTabControlSalesContent.TabPages.Add("تسجيل مرتجع باذن مخزن");
                        xtraTabPage = getTabPage(xtraTabControlSalesContent, "تسجيل مرتجع باذن مخزن");
                      
                    }
                    //xtraTabPage.Controls.Clear();

                    xtraTabControlSalesContent.SelectedTabPage = xtraTabPage;
                    CustomerReturnUsingPermissinNumber objForm = new CustomerReturnUsingPermissinNumber();
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
        public void displayDelegateReport(GridControl gridControl, string branchName, dataX d)
        {
            Delegate_Report objForm = new Delegate_Report(gridControl, branchName, d);

            objForm.TopLevel = false;
            XtraTabPage xtraTabPage = getTabPage(xtraTabControlSalesContent, "تقرير مبيعات المناديب");
            xtraTabControlSalesContent.RightToLeft = RightToLeft.Yes;
            
            if (xtraTabPage == null)
            {
                xtraTabControlSalesContent.TabPages.Add("تقرير مبيعات المناديب");
                xtraTabPage = getTabPage(xtraTabControlSalesContent, "تقرير مبيعات المناديب");
            }
            xtraTabPage.Controls.Clear();
            xtraTabPage.RightToLeft = RightToLeft.Yes;
            xtraTabPage.Controls.Add(objForm);

            xtraTabControlSalesContent.SelectedTabPage = xtraTabPage;

            objForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            objForm.Dock = DockStyle.Fill;
            objForm.Show();
        }
        private void navBarItemTotalSales_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            try
            {
                if ( UserControl.userType == 13 || UserControl.userType == 1 || UserControl.userType == 7)
                {
                    restForeColorOfNavBarItem();
                    NavBarItem navBarItem = (NavBarItem)sender;
                    navBarItem.Appearance.ForeColor = Color.Blue;

                    if (!xtraTabControlSalesContent.Visible)
                        xtraTabControlSalesContent.Visible = true;

                    XtraTabPage xtraTabPage = getTabPage(xtraTabControlSalesContent, "اجمالي مبيعات الشركات");
                    if (xtraTabPage == null)
                    {
                        xtraTabControlSalesContent.TabPages.Add("اجمالي مبيعات الشركات");
                        xtraTabPage = getTabPage(xtraTabControlSalesContent, "اجمالي مبيعات الشركات");

                    }
                    //xtraTabPage.Controls.Clear();

                    xtraTabControlSalesContent.SelectedTabPage = xtraTabPage;
                    salesReportForCompany objForm = new salesReportForCompany(this);
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
        private void navBarItemTotalSalesDetails_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            try
            {
                if (UserControl.userType == 13 || UserControl.userType == 1 || UserControl.userType == 7)
                {
                    restForeColorOfNavBarItem();
                    NavBarItem navBarItem = (NavBarItem)sender;
                    navBarItem.Appearance.ForeColor = Color.Blue;

                    if (!xtraTabControlSalesContent.Visible)
                        xtraTabControlSalesContent.Visible = true;

                    XtraTabPage xtraTabPage = getTabPage(xtraTabControlSalesContent, "تفاصيل اجمالي المبيعات");
                    if (xtraTabPage == null)
                    {
                        xtraTabControlSalesContent.TabPages.Add("تفاصيل اجمالي المبيعات");
                        xtraTabPage = getTabPage(xtraTabControlSalesContent, "تفاصيل اجمالي المبيعات");

                    }
                    //xtraTabPage.Controls.Clear();

                    xtraTabControlSalesContent.SelectedTabPage = xtraTabPage;
                    SalesReportForCompanyDetails objForm = new SalesReportForCompanyDetails(this);
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
        private void navBarItemBillCancel_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            try
            {
                if (UserControl.userType == 6 || UserControl.userType == 7 || UserControl.userType == 13 || UserControl.userType == 1|| UserControl.userType == 15)
                {
                    restForeColorOfNavBarItem();
                    NavBarItem navBarItem = (NavBarItem)sender;
                    navBarItem.Appearance.ForeColor = Color.Blue;

                    if (!xtraTabControlSalesContent.Visible)
                        xtraTabControlSalesContent.Visible = true;

                    XtraTabPage xtraTabPage = getTabPage("الغاء فاتورة");
                    if (xtraTabPage == null)
                    {
                        xtraTabControlSalesContent.TabPages.Add("الغاء فاتورة");
                        xtraTabPage = getTabPage("الغاء فاتورة");
                    }
                    xtraTabPage.Controls.Clear();

                    xtraTabControlSalesContent.SelectedTabPage = xtraTabPage;
                    BillCancelForm objForm = new BillCancelForm();
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
        private void navBarItemDelegateLeastBills_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            try
            {
                restForeColorOfNavBarItem();
                NavBarItem navBarItem = (NavBarItem)sender;
                navBarItem.Appearance.ForeColor = Color.Blue;
                if (!xtraTabControlSalesContent.Visible)
                    xtraTabControlSalesContent.Visible = true;

                XtraTabPage xtraTabPage = getTabPage(xtraTabControlSalesContent, "فواتير لم يتم سدادها بالكامل");
                if (xtraTabPage == null)
                {
                    xtraTabControlSalesContent.TabPages.Add("فواتير لم يتم سدادها بالكامل");
                    xtraTabPage = getTabPage(xtraTabControlSalesContent, "فواتير لم يتم سدادها بالكامل");
                    xtraTabPage.RightToLeft = RightToLeft.No;
                }
                else
                {
                    xtraTabPage.RightToLeft = RightToLeft.No;
                }

                xtraTabPage.Controls.Clear();
                xtraTabControlSalesContent.SelectedTabPage = xtraTabPage;

                DelegateLeastBills objForm = new DelegateLeastBills(this);

                objForm.TopLevel = false;
                xtraTabPage.Controls.Add(objForm);
                objForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                objForm.Dock = DockStyle.Fill;
                //objForm.DisplayAtaqm();
                objForm.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void pictureBoxSales_Click(object sender, EventArgs e)
        {
            try
            {
                if (/*UserControl.userType == 6 ||*/ UserControl.userType == 1|| UserControl.userType == 15 || UserControl.userType == 13)
                {
                    //if (flag == false)
                    //{
                        if (!xtraTabControlMainContainer.TabPages.Contains(xtraTabPageSales))
                        {
                            if (index == 0)
                            {
                                xtraTabControlMainContainer.TabPages.Insert(1, SalesTP);
                            }
                            else
                            {
                                xtraTabControlMainContainer.TabPages.Insert(index, SalesTP);
                            }
                            index++;
                            //flag = true;
                        }
                    //}
                    xtraTabControlMainContainer.SelectedTabPage = SalesTP;

                    if (!xtraTabControlSalesContent.Visible)
                        xtraTabControlSalesContent.Visible = true;

                    XtraTabPage xtraTabPage = getTabPage("عرض الطلبات الخاصة المؤقتة");
                    if (xtraTabPage == null)
                    {
                        xtraTabControlSalesContent.TabPages.Add("عرض الطلبات الخاصة المؤقتة");
                        xtraTabPage = getTabPage("عرض الطلبات الخاصة المؤقتة");
                    }
                    xtraTabPage.Controls.Clear();

                    xtraTabControlSalesContent.SelectedTabPage = xtraTabPage;
                    bindDisplaySpecialOrderConfirm(xtraTabPage);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //customers Accounting
        private void btnCustomerAccountStatment_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            try
            {
                if (UserControl.userType == 28 || UserControl.userType == 25 || UserControl.userType == 16 || UserControl.userType == 7 || UserControl.userType == 1|| UserControl.userType == 15 || UserControl.userType == 6 || UserControl.userType == 13)
                {
                    restForeColorOfNavBarItem();
                    NavBarItem navBarItem = (NavBarItem)sender;
                    navBarItem.Appearance.ForeColor = Color.Blue;

                    if (!xtraTabControlSalesContent.Visible)
                        xtraTabControlSalesContent.Visible = true;

                    XtraTabPage xtraTabPage = getTabPage("حركة عملاء لفترة");

                    if (xtraTabPage == null)
                    {
                        xtraTabControlSalesContent.TabPages.Add("حركة عملاء لفترة");
                        xtraTabPage = getTabPage("حركة عملاء لفترة");
                        xtraTabPage.RightToLeft = RightToLeft.No;
                    }
                    {
                        xtraTabPage.RightToLeft = RightToLeft.No;
                    }
                    xtraTabPage.Controls.Clear();

                    xtraTabControlSalesContent.SelectedTabPage = xtraTabPage;

                    customerBills objForm = new customerBills();
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
        private void btnAgalAcountStatment_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            try
            {
                if (UserControl.userType == 28 || UserControl.userType == 25 || UserControl.userType == 16 || UserControl.userType == 7 || UserControl.userType == 1|| UserControl.userType == 15 || UserControl.userType == 6 || UserControl.userType == 13)
                {
                    restForeColorOfNavBarItem();
                    NavBarItem navBarItem = (NavBarItem)sender;
                    navBarItem.Appearance.ForeColor = Color.Blue;
                    navBarItem.Appearance.BackColor = Color.White;

                    if (!xtraTabControlSalesContent.Visible)
                        xtraTabControlSalesContent.Visible = true;

                    XtraTabPage xtraTabPage = getTabPage("كشف حساب عميل");
                    if (xtraTabPage == null)
                    {
                        xtraTabControlSalesContent.TabPages.Add("كشف حساب عميل");
                        xtraTabPage = getTabPage("كشف حساب عميل");
                        xtraTabPage.RightToLeft = RightToLeft.No;
                    }
                    {
                        xtraTabPage.RightToLeft = RightToLeft.No;
                    }
                    xtraTabPage.Controls.Clear();

                    xtraTabControlSalesContent.SelectedTabPage = xtraTabPage;

                    AccountStatement objForm = new AccountStatement(this);
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
    
        private void btnCustomerTaswaya_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            try
            {
                if (UserControl.userType == 7 || UserControl.userType == 1/*|| UserControl.userType == 15*/ /*|| UserControl.userType == 6*/)
                {
                    if (!xtraTabControlSalesContent.Visible)
                        xtraTabControlSalesContent.Visible = true;

                    XtraTabPage xtraTabPage = getTabPage("تسويات العملاء");
                    if (xtraTabPage == null)
                    {
                        xtraTabControlSalesContent.TabPages.Add("تسويات العملاء");
                        xtraTabPage = getTabPage("تسويات العملاء");
                    }
                    xtraTabPage.Controls.Clear();
                    xtraTabControlSalesContent.SelectedTabPage = xtraTabPage;

                    CustomerTaswayaReport objForm = new CustomerTaswayaReport(this);
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

        private void navBarItemBillsAgleTransitionsReport_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            try
            {
                if (UserControl.userType == 25 || UserControl.userType == 6 || UserControl.userType == 13 || UserControl.userType == 18 || UserControl.userType == 17 || UserControl.userType == 1 || UserControl.userType == 15 || UserControl.userType == 16|| UserControl.userType == 7)
                {
                    restForeColorOfNavBarItem();
                    NavBarItem navBarItem = (NavBarItem)sender;
                    navBarItem.Appearance.ForeColor = Color.Blue;
                    if (!xtraTabControlSalesContent.Visible)
                        xtraTabControlSalesContent.Visible = true;

                    XtraTabPage xtraTabPage = getTabPage(xtraTabControlSalesContent, "تقرير حركة الفواتير الآجل");
                    if (xtraTabPage == null)
                    {
                        xtraTabControlSalesContent.TabPages.Add("تقرير حركة الفواتير الآجل");
                        xtraTabPage = getTabPage(xtraTabControlSalesContent, "تقرير حركة الفواتير الآجل");
                    }

                    xtraTabPage.Controls.Clear();
                    xtraTabControlSalesContent.SelectedTabPage = xtraTabPage;

                    BillsAglTransitions_Report objForm = new BillsAglTransitions_Report(this);

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

        private void navBarItemSalesProductsFactoryBranch_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            try
            {
                //if (UserControl.userType == 10 || UserControl.userType == 17 || UserControl.userType == 1)
                //{
                restForeColorOfNavBarItem();
                NavBarItem navBarItem = (NavBarItem)sender;
                navBarItem.Appearance.ForeColor = Color.Blue;

                if (!xtraTabControlSalesContent.Visible)
                    xtraTabControlSalesContent.Visible = true;

                XtraTabPage xtraTabPage = getTabPage(xtraTabControlSalesContent, "الاصناف المباعة لشركة بفرع");
                if (xtraTabPage == null)
                {
                    xtraTabControlSalesContent.TabPages.Add("الاصناف المباعة لشركة بفرع");
                    xtraTabPage = getTabPage(xtraTabControlSalesContent, "الاصناف المباعة لشركة بفرع");
                }
                xtraTabPage.Controls.Clear();

                xtraTabControlSalesContent.SelectedTabPage = xtraTabPage;
                bindDisplaySalesProductsBillsFactoryForm(xtraTabPage);
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void navBarItemSalesProductsFactories_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            try
            {
                restForeColorOfNavBarItem();
                NavBarItem navBarItem = (NavBarItem)sender;
                navBarItem.Appearance.ForeColor = Color.Blue;

                if (!xtraTabControlSalesContent.Visible)
                    xtraTabControlSalesContent.Visible = true;

                XtraTabPage xtraTabPage = getTabPage(xtraTabControlSalesContent, "الاصناف المباعة لشركة");
                if (xtraTabPage == null)
                {
                    xtraTabControlSalesContent.TabPages.Add("الاصناف المباعة لشركة");
                    xtraTabPage = getTabPage(xtraTabControlSalesContent, "الاصناف المباعة لشركة");
                }
                xtraTabPage.Controls.Clear();

                xtraTabControlSalesContent.SelectedTabPage = xtraTabPage;
                bindDisplaySalesProductsBillsFactoriesForm(xtraTabPage);
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
                //if (UserControl.userType == 10 || UserControl.userType == 17 || UserControl.userType == 1)
                //{
                restForeColorOfNavBarItem();
                NavBarItem navBarItem = (NavBarItem)sender;
                navBarItem.Appearance.ForeColor = Color.Blue;

                if (!xtraTabControlSalesContent.Visible)
                    xtraTabControlSalesContent.Visible = true;

                XtraTabPage xtraTabPage = getTabPage(xtraTabControlSalesContent, "الاصناف المباعة بالتاريخ");
                if (xtraTabPage == null)
                {
                    xtraTabControlSalesContent.TabPages.Add("الاصناف المباعة بالتاريخ");
                    xtraTabPage = getTabPage(xtraTabControlSalesContent, "الاصناف المباعة بالتاريخ");
                }
                xtraTabPage.Controls.Clear();

                xtraTabControlSalesContent.SelectedTabPage = xtraTabPage;
                bindDisplaySalesProductsBillsDateForm(xtraTabPage);
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void navBarItemSalesProductsBills_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            try
            {
                //if (UserControl.userType == 10 || UserControl.userType == 17 || UserControl.userType == 1)
                //{
                restForeColorOfNavBarItem();
                NavBarItem navBarItem = (NavBarItem)sender;
                navBarItem.Appearance.ForeColor = Color.Blue;

                if (!xtraTabControlSalesContent.Visible)
                    xtraTabControlSalesContent.Visible = true;

                XtraTabPage xtraTabPage = getTabPage(xtraTabControlSalesContent, "الاصناف المباعة بالفواتير");
                if (xtraTabPage == null)
                {
                    xtraTabControlSalesContent.TabPages.Add("الاصناف المباعة بالفواتير");
                    xtraTabPage = getTabPage(xtraTabControlSalesContent, "الاصناف المباعة بالفواتير");
                }
                xtraTabPage.Controls.Clear();

                xtraTabControlSalesContent.SelectedTabPage = xtraTabPage;
                bindDisplaySalesProductsBillsNumForm(xtraTabPage);
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void navBarItemSaleProductBillDetailsReport_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            try
            {
                //if (UserControl.userType == 10 || UserControl.userType == 17 || UserControl.userType == 1)
                //{
                restForeColorOfNavBarItem();
                NavBarItem navBarItem = (NavBarItem)sender;
                navBarItem.Appearance.ForeColor = Color.Blue;

                if (!xtraTabControlSalesContent.Visible)
                    xtraTabControlSalesContent.Visible = true;

                XtraTabPage xtraTabPage = getTabPage(xtraTabControlSalesContent, "تفاصيل الاصناف المباعة");
                if (xtraTabPage == null)
                {
                    xtraTabControlSalesContent.TabPages.Add("تفاصيل الاصناف المباعة");
                    xtraTabPage = getTabPage(xtraTabControlSalesContent, "تفاصيل الاصناف المباعة");
                }
                xtraTabPage.Controls.Clear();

                xtraTabControlSalesContent.SelectedTabPage = xtraTabPage;
                bindDisplaySalesProductsBillsDetailsForm(xtraTabPage);
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void navBarItemDesignsReport_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            try
            {
                restForeColorOfNavBarItem();
                NavBarItem navBarItem = (NavBarItem)sender;
                navBarItem.Appearance.ForeColor = Color.Blue;

                if (!xtraTabControlSalesContent.Visible)
                    xtraTabControlSalesContent.Visible = true;

                XtraTabPage xtraTabPage = getTabPage(xtraTabControlSalesContent, "حركة التصميمات");
                if (xtraTabPage == null)
                {
                    xtraTabControlSalesContent.TabPages.Add("حركة التصميمات");
                    xtraTabPage = getTabPage(xtraTabControlSalesContent, "حركة التصميمات");
                }
                xtraTabPage.Controls.Clear();

                xtraTabControlSalesContent.SelectedTabPage = xtraTabPage;
                bindDisplayDesignReportForm(xtraTabPage);
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void navBarItemDesignSearch_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            try
            {
                restForeColorOfNavBarItem();
                NavBarItem navBarItem = (NavBarItem)sender;
                navBarItem.Appearance.ForeColor = Color.Blue;

                if (!xtraTabControlSalesContent.Visible)
                    xtraTabControlSalesContent.Visible = true;

                XtraTabPage xtraTabPage = getTabPage(xtraTabControlSalesContent, "استعلام عن تصميم");
                if (xtraTabPage == null)
                {
                    xtraTabControlSalesContent.TabPages.Add("استعلام عن تصميم");
                    xtraTabPage = getTabPage(xtraTabControlSalesContent, "استعلام عن تصميم");
                }
                xtraTabPage.Controls.Clear();

                xtraTabControlSalesContent.SelectedTabPage = xtraTabPage;
                bindDisplayDesignSearchForm(xtraTabPage);
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //functions
        public void bindDisplaySpecialOrderConfirm(XtraTabPage xtraTabPage)
        {
            specialOrderConfirm = new SpecialOrderConfirm(this, xtraTabControlSalesContent);
            specialOrderConfirm.TopLevel = false;

            xtraTabPage.Controls.Add(specialOrderConfirm);
            specialOrderConfirm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            specialOrderConfirm.Dock = DockStyle.Fill;
            specialOrderConfirm.Show();
        }
        //Products sell price
        public void bindDisplayProductsSellPriceForm(XtraTabPage xtraTabPage)
        {
            ProductsSellPriceForm objForm = new ProductsSellPriceForm(this);
            objForm.TopLevel = false;

            xtraTabPage.Controls.Add(objForm);
            objForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            objForm.Dock = DockStyle.Fill;
            objForm.Show();
        }
        //record sell price 
        public void bindRecordSellPriceForm(ProductsSellPriceForm productsSellPriceForm)
        {
            if (!xtraTabControlSalesContent.Visible)
                xtraTabControlSalesContent.Visible = true;

            XtraTabPage xtraTabPage = getTabPage("تسجيل اسعار البنود");
            if (xtraTabPage == null)
            {
                xtraTabControlSalesContent.TabPages.Add("تسجيل اسعار البنود");
                xtraTabPage = getTabPage("تسجيل اسعار البنود");
            }
            xtraTabPage.Controls.Clear();

            xtraTabControlSalesContent.SelectedTabPage = xtraTabPage;
            SetSellPrice objForm = new SetSellPrice(productsSellPriceForm, xtraTabControlSalesContent);
            objForm.TopLevel = false;

            xtraTabPage.Controls.Add(objForm);
            objForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            objForm.Dock = DockStyle.Fill;
            objForm.Show();
        }
        //update sell price 
        public void bindUpdateSellPriceForm(List<DataRowView> rows, ProductsSellPriceForm productsSellPriceForm, String query)
        {
            if (!xtraTabControlSalesContent.Visible)
                xtraTabControlSalesContent.Visible = true;

            XtraTabPage xtraTabPage = getTabPage("تعديل اسعار البنود");

            if (xtraTabPage == null)
            {
                xtraTabControlSalesContent.TabPages.Add("تعديل اسعار البنود");
                xtraTabPage = getTabPage("تعديل اسعار البنود");
            }
            xtraTabPage.Controls.Clear();

            xtraTabControlSalesContent.SelectedTabPage = xtraTabPage;

            UpdateSellPrice objForm = new UpdateSellPrice(rows, productsSellPriceForm, query, xtraTabControlSalesContent);
            objForm.TopLevel = false;

            xtraTabPage.Controls.Add(objForm);
            objForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            objForm.Dock = DockStyle.Fill;
            objForm.Show();
        }
        //report sell price 
        public void bindReportSellPriceForm(GridControl gridControl)
        {
            if (!xtraTabControlSalesContent.Visible)
                xtraTabControlSalesContent.Visible = true;

            XtraTabPage xtraTabPage = getTabPage("تقرير اسعار البنود");

            if (xtraTabPage == null)
            {
                xtraTabControlSalesContent.TabPages.Add("تقرير اسعار البنود");
                xtraTabPage = getTabPage("تقرير اسعار البنود");
            }
            xtraTabPage.Controls.Clear();

            xtraTabControlSalesContent.SelectedTabPage = xtraTabPage;
            ProductSellPricesReport objForm = new ProductSellPricesReport(gridControl);
            objForm.TopLevel = false;

            xtraTabPage.Controls.Add(objForm);
            objForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            objForm.Dock = DockStyle.Fill;
            objForm.Show();
        }
        //offers
        public void bindRecordOfferForm(Offer offer)
        {
            Offer_Record objForm = new Offer_Record(offer);

            objForm.TopLevel = false;
            XtraTabPage xtraTabPage = getTabPage(xtraTabControlSalesContent, "أضافة عرض");
            if (xtraTabPage == null)
            {
                xtraTabControlSalesContent.TabPages.Add("أضافة عرض");
                xtraTabPage = getTabPage(xtraTabControlSalesContent, "أضافة عرض");
            }
            xtraTabPage.Controls.Clear();
            xtraTabPage.Controls.Add(objForm);
            xtraTabControlSalesContent.SelectedTabPage = xtraTabPage;

            objForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            objForm.Dock = DockStyle.Fill;
            objForm.Show();

        }
        
        public void bindUpdateOfferForm(DataRowView prodRow, Offer offer)
        {
            Offer_Update objForm = new Offer_Update(prodRow, offer, xtraTabControlSalesContent);
            objForm.TopLevel = false;
            XtraTabPage xtraTabPage = getTabPage(xtraTabControlSalesContent, "تعديل عرض");
            if (xtraTabPage == null)
            {
                xtraTabControlSalesContent.TabPages.Add("تعديل عرض");
                xtraTabPage = getTabPage(xtraTabControlSalesContent, "تعديل عرض");
                xtraTabPage.Controls.Clear();
                xtraTabPage.Controls.Add(objForm);
                xtraTabControlSalesContent.SelectedTabPage = xtraTabPage;
                objForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                objForm.Dock = DockStyle.Fill;
                objForm.Show();
            }
            else if (xtraTabPage.ImageOptions.Image != null)
            {
                DialogResult dialogResult = MessageBox.Show("There is unsave data To you wound override it?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                    xtraTabPage.Controls.Clear();
                    xtraTabPage.Controls.Add(objForm);
                    xtraTabControlSalesContent.SelectedTabPage = xtraTabPage;
                    objForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                    objForm.Dock = DockStyle.Fill;
                    objForm.Show();
                }
                else if (dialogResult == DialogResult.No)
                {
                    xtraTabControlSalesContent.SelectedTabPage = xtraTabPage;
                }
            }
            else
            {
                xtraTabPage.Controls.Clear();
                xtraTabPage.Controls.Add(objForm);
                xtraTabControlSalesContent.SelectedTabPage = xtraTabPage;
                objForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                objForm.Dock = DockStyle.Fill;
                objForm.Show();
            }

        }

        public void bindReportOffersForm(GridControl gridControl)
        {
            Offer_Report objForm = new Offer_Report(gridControl);
            objForm.TopLevel = false;
            XtraTabPage xtraTabPage = getTabPage(xtraTabControlSalesContent, "تقرير العروض");
            if (xtraTabPage == null)
            {
                xtraTabControlSalesContent.TabPages.Add("تقرير العروض");
                xtraTabPage = getTabPage(xtraTabControlSalesContent, "تقرير العروض");
            }
            xtraTabPage.Controls.Clear();
            xtraTabPage.Controls.Add(objForm);
            xtraTabControlSalesContent.SelectedTabPage = xtraTabPage;
            objForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            objForm.Dock = DockStyle.Fill;
            objForm.Show();
        }

        public void bindOfferSetForm(OfferStorage offer)
        {
            Offer_Collect objForm = new Offer_Collect(offer);
            objForm.TopLevel = false;
            XtraTabPage xtraTabPage = getTabPage(xtraTabControlSalesContent, "تجميع عرض");
            if (xtraTabPage == null)
            {
                xtraTabControlSalesContent.TabPages.Add("تجميع عرض");
                xtraTabPage = getTabPage(xtraTabControlSalesContent, "تجميع عرض");
                xtraTabPage.Controls.Clear();
                xtraTabPage.Controls.Add(objForm);
                xtraTabControlSalesContent.SelectedTabPage = xtraTabPage;
                objForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                objForm.Dock = DockStyle.Fill;
                objForm.Show();
            }
            else if (xtraTabPage.ImageOptions.Image != null)
            {
                DialogResult dialogResult = MessageBox.Show("There is unsave data To you wound override it?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                    xtraTabPage.Controls.Clear();
                    xtraTabPage.Controls.Add(objForm);
                    xtraTabControlSalesContent.SelectedTabPage = xtraTabPage;
                    objForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                    objForm.Dock = DockStyle.Fill;
                    objForm.Show();
                }
                else if (dialogResult == DialogResult.No)
                {
                    xtraTabControlSalesContent.SelectedTabPage = xtraTabPage;
                }
            }
            else
            {
                xtraTabPage.Controls.Clear();
                xtraTabPage.Controls.Add(objForm);
                xtraTabControlSalesContent.SelectedTabPage = xtraTabPage;
                objForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                objForm.Dock = DockStyle.Fill;
                objForm.Show();
            }

        }

        public void bindFakOfferForm(OfferStorage offer)
        {
            Offer_Fak objForm = new Offer_Fak(offer);
            objForm.TopLevel = false;
            XtraTabPage xtraTabPage = getTabPage(xtraTabControlSalesContent, "فك عرض");
            if (xtraTabPage == null)
            {
                xtraTabControlSalesContent.TabPages.Add("فك عرض");
                xtraTabPage = getTabPage(xtraTabControlSalesContent, "فك عرض");
                xtraTabPage.Controls.Clear();
                xtraTabPage.Controls.Add(objForm);
                xtraTabControlSalesContent.SelectedTabPage = xtraTabPage;
                objForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                objForm.Dock = DockStyle.Fill;
                objForm.Show();
            }
            else if (xtraTabPage.ImageOptions.Image != null)
            {
                DialogResult dialogResult = MessageBox.Show("There is unsave data To you wound override it?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                    xtraTabPage.Controls.Clear();
                    xtraTabPage.Controls.Add(objForm);
                    xtraTabControlSalesContent.SelectedTabPage = xtraTabPage;
                    objForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                    objForm.Dock = DockStyle.Fill;
                    objForm.Show();
                }
                else if (dialogResult == DialogResult.No)
                {
                    xtraTabControlSalesContent.SelectedTabPage = xtraTabPage;
                }
            }
            else
            {
                xtraTabPage.Controls.Clear();
                xtraTabPage.Controls.Add(objForm);
                xtraTabControlSalesContent.SelectedTabPage = xtraTabPage;
                objForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                objForm.Dock = DockStyle.Fill;
                objForm.Show();
            }

        }

        public void bindRecordCustomerReturnBillForm(CustomerReturnBill_Report ReturnBillReport)
        {
            /*CustomerReturnBill objForm = new CustomerReturnBill(ReturnBillReport);

            objForm.TopLevel = false;
            XtraTabPage xtraTabPage = getTabPage(xtraTabControlSalesContent, "تسجيل فاتورة مرتجع");
            if (xtraTabPage == null)
            {
                xtraTabControlSalesContent.TabPages.Add("تسجيل فاتورة مرتجع");
                xtraTabPage = getTabPage(xtraTabControlSalesContent, "تسجيل فاتورة مرتجع");
            }
            xtraTabPage.Controls.Clear();
            xtraTabPage.Controls.Add(objForm);
            xtraTabControlSalesContent.SelectedTabPage = xtraTabPage;

            objForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            objForm.Dock = DockStyle.Fill;
            objForm.Show();
            */
        }
        //taswaya customer
        public void bindTaswayaCustomersForm(string customerType, string customerID, string clientID)
        {
            if (!xtraTabControlSalesContent.Visible)
                xtraTabControlSalesContent.Visible = true;

            XtraTabPage xtraTabPage = getTabPage("تسوية عميل");
            if (xtraTabPage == null)
            {
                xtraTabControlSalesContent.TabPages.Add("تسوية عميل");
                xtraTabPage = getTabPage("تسوية عميل");
            }
            xtraTabPage.Controls.Clear();
            xtraTabControlSalesContent.SelectedTabPage = xtraTabPage;

            CustomerTaswaya objForm = new CustomerTaswaya(customerType, customerID, clientID);
            objForm.TopLevel = false;
            xtraTabPage.Controls.Add(objForm);
            objForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            objForm.Dock = DockStyle.Fill;
            objForm.Show();
        }
        public void bindTaswayaCustomersForm()
        {
            if (!xtraTabControlSalesContent.Visible)
                xtraTabControlSalesContent.Visible = true;

            XtraTabPage xtraTabPage = getTabPage("تسوية عميل");
            if (xtraTabPage == null)
            {
                xtraTabControlSalesContent.TabPages.Add("تسوية عميل");
                xtraTabPage = getTabPage("تسوية عميل");
            }
            xtraTabPage.Controls.Clear();
            xtraTabControlSalesContent.SelectedTabPage = xtraTabPage;

            CustomerTaswaya objForm = new CustomerTaswaya();
            objForm.TopLevel = false;
            xtraTabPage.Controls.Add(objForm);
            objForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            objForm.Dock = DockStyle.Fill;
            objForm.Show();
        }
        public void bindUpdateTaswayaCustomersForm(DataRowView row, CustomerTaswayaReport CustomerTaswayaReport)
        {
            if (!xtraTabControlSalesContent.Visible)
                xtraTabControlSalesContent.Visible = true;

            XtraTabPage xtraTabPage = getTabPage("تعديل تسوية عميل");
            if (xtraTabPage == null)
            {
                xtraTabControlSalesContent.TabPages.Add("تعديل تسوية عميل");
                xtraTabPage = getTabPage("تعديل تسوية عميل");
            }
            xtraTabPage.Controls.Clear();
            xtraTabControlSalesContent.SelectedTabPage = xtraTabPage;

            UpdateCustomerTaswaya objForm = new UpdateCustomerTaswaya(row,this, CustomerTaswayaReport);
            objForm.TopLevel = false;
            xtraTabPage.Controls.Add(objForm);
            objForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            objForm.Dock = DockStyle.Fill;
            objForm.Show();
        }
        //customers
        public void bindDisplayCustomersForm(XtraTabPage xtraTabPage)
        {
            objFormCustomer = new Customer_Report(xtraTabControlSalesContent);
            objFormCustomer.TopLevel = false;

            xtraTabPage.Controls.Add(objFormCustomer);
            objFormCustomer.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            objFormCustomer.Dock = DockStyle.Fill;
            objFormCustomer.Show();
        }
        //confirm bill
        public void bindDisplayConfirmBillForm(XtraTabPage xtraTabPage)
        {
            objFormBillConfirm = new Bill_Confirm();
            objFormBillConfirm.TopLevel = false;

            xtraTabPage.Controls.Add(objFormBillConfirm);
            objFormBillConfirm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            objFormBillConfirm.Dock = DockStyle.Fill;
            objFormBillConfirm.Show();
        }
        
        public void bindDisplaySalesProductsBillsFactoriesForm(XtraTabPage xtraTabPage)
        {
            SalesProductsBillsFactories_Report objForm = new SalesProductsBillsFactories_Report(this);
            objForm.TopLevel = false;

            xtraTabPage.Controls.Add(objForm);
            objForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            objForm.Dock = DockStyle.Fill;
            objForm.Show();
        }

        public void bindDisplaySalesProductsBillsDetailsForm(XtraTabPage xtraTabPage)
        {
            SalesProductsBillsDetails_Report objForm = new SalesProductsBillsDetails_Report(this);
            objForm.TopLevel = false;

            xtraTabPage.Controls.Add(objForm);
            objForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            objForm.Dock = DockStyle.Fill;
            objForm.Show();
        }

        public void bindDisplayDesignReportForm(XtraTabPage xtraTabPage)
        {
            Designs_Report objForm = new Designs_Report(this);
            objForm.TopLevel = false;

            xtraTabPage.Controls.Add(objForm);
            objForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            objForm.Dock = DockStyle.Fill;
            objForm.Show();
        }

        public void bindDisplayDesignSearchForm(XtraTabPage xtraTabPage)
        {
            DesignSearch_Report objForm = new DesignSearch_Report(this);
            objForm.TopLevel = false;

            xtraTabPage.Controls.Add(objForm);
            objForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            objForm.Dock = DockStyle.Fill;
            objForm.Show();
        }

        public void bindDisplaySalesProductsBillsFactoryForm(XtraTabPage xtraTabPage)
        {
            SalesProductsBillsFactory_Report objForm = new SalesProductsBillsFactory_Report(this);
            objForm.TopLevel = false;

            xtraTabPage.Controls.Add(objForm);
            objForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            objForm.Dock = DockStyle.Fill;
            objForm.Show();
        }

        public XtraTabPage getTabPage(string text)
        {
            for (int i = 0; i < xtraTabControlSalesContent.TabPages.Count; i++)
                if (xtraTabControlSalesContent.TabPages[i].Text == text)
                {
                    return xtraTabControlSalesContent.TabPages[i];
                }
            return null;
        }
        //
        public void GetNonRequestedSpecialOrders(object sender, EventArgs e)
        {
            try
            {
                SpecialOrdersFunction();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }

        public void SpecialOrdersFunction()
        {
            if (/*UserControl.userType == 7 || UserControl.userType == 6 || UserControl.userType == 13 ||*/ UserControl.userType == 1)
            {
                dbconnection.Close();
                //INNER JOIN orders ON special_order.SpecialOrder_ID = orders.SpecialOrder_ID 
                string query = "SELECT Count(special_order.SpecialOrder_ID) FROM special_order INNER JOIN dash ON special_order.Dash_ID = dash.Dash_ID where special_order.Record=0 and special_order.Confirmed=0 and special_order.Canceled=0" /* AND dash.Branch_ID=" + EmpBranchId*/;
                MySqlCommand command = new MySqlCommand(query, dbconnection);
                dbconnection.Open();
                string reader = command.ExecuteScalar().ToString();
                labelNotifySpecialOrderSales.Text = reader;
                if (Convert.ToInt32(reader) > 0)
                {
                    labelNotifySpecialOrderSales.Visible = true;
                }
                else
                {
                    labelNotifySpecialOrderSales.Visible = false;
                }
            }
        }

        public void displayCompanyReport(GridControl gridControl, dataX d,string title)
        {
            Delegate_Report objForm = new Delegate_Report(gridControl, d, title);
            
            objForm.Show();
        }


    }
}

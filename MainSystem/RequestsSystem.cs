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
    class RequestsSystem
    {
    }
    public partial class MainForm
    {
        public static XtraTabControl tabControlRequests;
        public static LeastQuantityReport leastQuantityReport;
        public static BillLeastQuantityReport BillLeastQuantityReport;
        public static SpecialOrders_Report2 SpecialOrdersReport;

        Timer Purchasetimer = new Timer();
        //bool purchaseFlag = false;

        public void RequestsMainForm()
        {
            try
            {
                //if (UserControl.userType == 10 || UserControl.userType == 17 || UserControl.userType == 1)
                if (UserControl.userType == 19 || UserControl.userType == 1)
                {
                    LeastQuantityFunction();
                    ConfirmedSpecialOrdersFunction();

                    Purchasetimer.Interval = 1000 * 60;
                    Purchasetimer.Tick += new EventHandler(GetNonRequestedLeastQuantity);
                    Purchasetimer.Tick += new EventHandler(GetConfirmedSpecialOrder);
                    Purchasetimer.Start();
                }
                if (UserControl.userType == 20)
                {
                    LeastQuantityFunction();
                    Purchasetimer.Interval = 1000 * 60;
                    Purchasetimer.Tick += new EventHandler(GetNonRequestedLeastQuantity);
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
                //if (UserControl.userType == 10 || UserControl.userType == 17 || UserControl.userType == 1 /*|| UserControl.userType == 2*/)
                //{
                    //if (purchaseFlag == false)
                    //{
                        if (!xtraTabControlMainContainer.TabPages.Contains(xtraTabPageRequest))
                        {
                            if (index == 0)
                            {
                                xtraTabControlMainContainer.TabPages.Insert(1, RequestsTP);
                            }
                            else
                            {
                                xtraTabControlMainContainer.TabPages.Insert(index, RequestsTP);
                            }
                            index++;
                            //purchaseFlag = true;
                        }
                    //}
                    xtraTabControlMainContainer.SelectedTabPage = RequestsTP;

                    if (!xtraTabControlRequests.Visible)
                    xtraTabControlRequests.Visible = true;

                    XtraTabPage xtraTabPage = getTabPage(xtraTabControlRequests, "عرض الطلبات الخاصة");
                    if (xtraTabPage == null)
                    {
                    xtraTabControlRequests.TabPages.Add("عرض الطلبات الخاصة");
                        xtraTabPage = getTabPage(xtraTabControlRequests, "عرض الطلبات الخاصة");
                    }
                    xtraTabPage.Controls.Clear();

                xtraTabControlRequests.SelectedTabPage = xtraTabPage;
                    bindDisplaySpecialOrdersReport(xtraTabPage);
                //}
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
                //if (UserControl.userType == 19 || UserControl.userType == 20 /*/*|| UserControl.userType == 10 || UserControl.userType == 17*/ || UserControl.userType == 1)
                //{
                    //if (purchaseFlag == false)
                    //{
                        if (!xtraTabControlMainContainer.TabPages.Contains(xtraTabPageRequest))
                        {
                            if (index == 0)
                            {
                                xtraTabControlMainContainer.TabPages.Insert(1, RequestsTP);
                            }
                            else
                            {
                                xtraTabControlMainContainer.TabPages.Insert(index, RequestsTP);
                            }
                            index++;
                            //purchaseFlag = true;
                        }
                    //}
                    xtraTabControlMainContainer.SelectedTabPage = RequestsTP;

                    if (!xtraTabControlRequests.Visible)
                    xtraTabControlRequests.Visible = true;

                    XtraTabPage xtraTabPage = getTabPage(xtraTabControlRequests, "عرض البنود المطلوبة");
                    if (xtraTabPage == null)
                    {
                    xtraTabControlRequests.TabPages.Add("عرض البنود المطلوبة");
                        xtraTabPage = getTabPage(xtraTabControlRequests, "عرض البنود المطلوبة");
                    }
                    xtraTabPage.Controls.Clear();

                xtraTabControlRequests.SelectedTabPage = xtraTabPage;
                    bindDisplayLeastQuantityReport(xtraTabPage);
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void navBarItemBillLeastQuantity_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            try
            {
                if (!xtraTabControlMainContainer.TabPages.Contains(xtraTabPageRequest))
                {
                    if (index == 0)
                    {
                        xtraTabControlMainContainer.TabPages.Insert(1, RequestsTP);
                    }
                    else
                    {
                        xtraTabControlMainContainer.TabPages.Insert(index, RequestsTP);
                    }
                    index++;
                }

                xtraTabControlMainContainer.SelectedTabPage = RequestsTP;

                if (!xtraTabControlRequests.Visible)
                    xtraTabControlRequests.Visible = true;

                XtraTabPage xtraTabPage = getTabPage(xtraTabControlRequests, "عرض بنود وصلت للحد الادنى");
                if (xtraTabPage == null)
                {
                    xtraTabControlRequests.TabPages.Add("عرض بنود وصلت للحد الادنى");
                    xtraTabPage = getTabPage(xtraTabControlRequests, "عرض بنود وصلت للحد الادنى");
                }
                xtraTabPage.Controls.Clear();

                xtraTabControlRequests.SelectedTabPage = xtraTabPage;
                bindDisplayBillLeastQuantityReport(xtraTabPage);
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
                //if (UserControl.userType == 10 || UserControl.userType == 17 || UserControl.userType == 1)
                //{
                    restForeColorOfNavBarItem();
                    NavBarItem navBarItem = (NavBarItem)sender;
                    navBarItem.Appearance.ForeColor = Color.Blue;

                    if (!xtraTabControlRequests.Visible)
                    xtraTabControlRequests.Visible = true;

                    XtraTabPage xtraTabPage = getTabPage(xtraTabControlRequests, "تسجيل الحد الادنى للبنود");
                    if (xtraTabPage == null)
                    {
                    xtraTabControlRequests.TabPages.Add("تسجيل الحد الادنى للبنود");
                        xtraTabPage = getTabPage(xtraTabControlRequests, "تسجيل الحد الادنى للبنود");
                    }
                    xtraTabPage.Controls.Clear();

                xtraTabControlRequests.SelectedTabPage = xtraTabPage;
                    bindDisplayLeastQuantityForm(xtraTabPage);
                //}
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
                //if (UserControl.userType == 10 || UserControl.userType == 17 || UserControl.userType == 1)
                //{
                    restForeColorOfNavBarItem();
                    NavBarItem navBarItem = (NavBarItem)sender;
                    navBarItem.Appearance.ForeColor = Color.Blue;

                    if (!xtraTabControlRequests.Visible)
                    xtraTabControlRequests.Visible = true;

                    XtraTabPage xtraTabPage = getTabPage(xtraTabControlRequests, "عرض الطلبيات");
                    if (xtraTabPage == null)
                    {
                    xtraTabControlRequests.TabPages.Add("عرض الطلبيات");
                        xtraTabPage = getTabPage(xtraTabControlRequests, "عرض الطلبيات");
                    }
                    xtraTabPage.Controls.Clear();

                xtraTabControlRequests.SelectedTabPage = xtraTabPage;
                    bindDisplayOrderReportForm(xtraTabPage);
                //}
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
                //if (UserControl.userType == 10 || UserControl.userType == 17 || UserControl.userType == 1)
                //{
                    restForeColorOfNavBarItem();
                    NavBarItem navBarItem = (NavBarItem)sender;
                    navBarItem.Appearance.ForeColor = Color.Blue;
                    if (!xtraTabControlRequests.Visible)
                    xtraTabControlRequests.Visible = true;

                    XtraTabPage xtraTabPage = getTabPage(xtraTabControlRequests, "الطلبات بتاريخ الاستلام");
                    if (xtraTabPage == null)
                    {
                    xtraTabControlRequests.TabPages.Add("الطلبات بتاريخ الاستلام");
                        xtraTabPage = getTabPage(xtraTabControlRequests, "الطلبات بتاريخ الاستلام");
                    }

                    xtraTabPage.Controls.Clear();
                xtraTabControlRequests.SelectedTabPage = xtraTabPage;

                    SearchRecive_Date objForm = new SearchRecive_Date(this, xtraTabControlRequests);

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

        private void navBarItemRequestedOrders_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            try
            {
                //if (UserControl.userType == 10 || UserControl.userType == 17 || UserControl.userType == 1)
                //{
                    restForeColorOfNavBarItem();
                    NavBarItem navBarItem = (NavBarItem)sender;
                    navBarItem.Appearance.ForeColor = Color.Blue;
                    if (!xtraTabControlRequests.Visible)
                    xtraTabControlRequests.Visible = true;

                    XtraTabPage xtraTabPage = getTabPage(xtraTabControlRequests, "الطلبات بتاريخ الطلب");
                    if (xtraTabPage == null)
                    {
                    xtraTabControlRequests.TabPages.Add("الطلبات بتاريخ الطلب");
                        xtraTabPage = getTabPage(xtraTabControlRequests, "الطلبات بتاريخ الطلب");
                    }

                    xtraTabPage.Controls.Clear();
                xtraTabControlRequests.SelectedTabPage = xtraTabPage;

                    SearchRequest_Date objForm = new SearchRequest_Date(this, xtraTabControlRequests);

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

        private void navBarItemOneOrder_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            try
            {
                //if (UserControl.userType == 10 || UserControl.userType == 17 || UserControl.userType == 1)
                //{
                    restForeColorOfNavBarItem();
                    NavBarItem navBarItem = (NavBarItem)sender;
                    navBarItem.Appearance.ForeColor = Color.Blue;
                    if (!xtraTabControlRequests.Visible)
                    xtraTabControlRequests.Visible = true;

                    XtraTabPage xtraTabPage = getTabPage(xtraTabControlRequests, "عرض طلب");
                    if (xtraTabPage == null)
                    {
                    xtraTabControlRequests.TabPages.Add("عرض طلب");
                        xtraTabPage = getTabPage(xtraTabControlRequests, "عرض طلب");
                    }

                    xtraTabPage.Controls.Clear();
                xtraTabControlRequests.SelectedTabPage = xtraTabPage;

                    OrderStored objForm = new OrderStored(this, xtraTabControlRequests);

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
        
        private void navBarItemDashOrderReport_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            try
            {
                //if (UserControl.userType == 10 || UserControl.userType == 17 || UserControl.userType == 1)
                //{
                    restForeColorOfNavBarItem();
                    NavBarItem navBarItem = (NavBarItem)sender;
                    navBarItem.Appearance.ForeColor = Color.Blue;

                    if (!xtraTabControlRequests.Visible)
                    xtraTabControlRequests.Visible = true;

                    XtraTabPage xtraTabPage = getTabPage(xtraTabControlRequests, "عرض الطلبيات المؤقتة");
                    if (xtraTabPage == null)
                    {
                    xtraTabControlRequests.TabPages.Add("عرض الطلبيات المؤقتة");
                        xtraTabPage = getTabPage(xtraTabControlRequests, "عرض الطلبيات المؤقتة");
                    }
                    xtraTabPage.Controls.Clear();

                xtraTabControlRequests.SelectedTabPage = xtraTabPage;
                    bindDisplayDashOrderReportForm(xtraTabPage);
                //}
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
                //if (UserControl.userType == 10 || UserControl.userType == 17 || UserControl.userType == 1)
                //{
                    restForeColorOfNavBarItem();
                    NavBarItem navBarItem = (NavBarItem)sender;
                    navBarItem.Appearance.ForeColor = Color.Blue;

                    if (!xtraTabControlRequests.Visible)
                    xtraTabControlRequests.Visible = true;

                    XtraTabPage xtraTabPage = getTabPage(xtraTabControlRequests, "عرض الطلبيات الخاصة المؤقتة");
                    if (xtraTabPage == null)
                    {
                    xtraTabControlRequests.TabPages.Add("عرض الطلبيات الخاصة المؤقتة");
                        xtraTabPage = getTabPage(xtraTabControlRequests, "عرض الطلبيات الخاصة المؤقتة");
                    }
                    xtraTabPage.Controls.Clear();

                xtraTabControlRequests.SelectedTabPage = xtraTabPage;
                    bindDisplayDashRequestReportForm(xtraTabPage);
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        
        ////////////////////////////////////////////////////////
        
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
            leastQuantityReport = new LeastQuantityReport(this, xtraTabControlRequests);
            leastQuantityReport.TopLevel = false;

            xtraTabPage.Controls.Add(leastQuantityReport);
            leastQuantityReport.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            leastQuantityReport.Dock = DockStyle.Fill;
            leastQuantityReport.Show();
        }

        public void bindDisplayBillLeastQuantityReport(XtraTabPage xtraTabPage)
        {
            BillLeastQuantityReport = new BillLeastQuantityReport(this, xtraTabControlRequests);
            BillLeastQuantityReport.TopLevel = false;

            xtraTabPage.Controls.Add(BillLeastQuantityReport);
            BillLeastQuantityReport.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            BillLeastQuantityReport.Dock = DockStyle.Fill;
            BillLeastQuantityReport.Show();
        }

        //Products Purchase price

        public void bindDisplayLeastQuantityForm(XtraTabPage xtraTabPage)
        {
            LeastQuantityRecord objForm = new LeastQuantityRecord(this, xtraTabControlRequests);
            objForm.TopLevel = false;

            xtraTabPage.Controls.Add(objForm);
            objForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            objForm.Dock = DockStyle.Fill;
            objForm.Show();
        }

        public void bindDisplayOrderReportForm(XtraTabPage xtraTabPage)
        {
            Order_Report objForm = new Order_Report(this, xtraTabControlRequests);
            objForm.TopLevel = false;

            xtraTabPage.Controls.Add(objForm);
            objForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            objForm.Dock = DockStyle.Fill;
            objForm.Show();
        }
        public void bindRecordOrderForm(Order_Report OrderReport, List<DataRow> row1)
        {
            if (!xtraTabControlRequests.Visible)
                xtraTabControlRequests.Visible = true;

            XtraTabPage xtraTabPage = getTabPage(xtraTabControlRequests, "اضافة طلب");
            if (xtraTabPage == null)
            {
                xtraTabControlRequests.TabPages.Add("اضافة طلب");
                xtraTabPage = getTabPage(xtraTabControlRequests, "اضافة طلب");
            }
            xtraTabPage.Controls.Clear();

            xtraTabControlRequests.SelectedTabPage = xtraTabPage;
            //List<DataRow> row1 = new List<DataRow>();
            Order_Record objForm = new Order_Record(row1, OrderReport, xtraTabControlRequests);
            objForm.TopLevel = false;

            xtraTabPage.Controls.Add(objForm);
            objForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            objForm.Dock = DockStyle.Fill;
            objForm.Show();
        }
        public void bindRecordDashOrderForm(DashOrder_Report DashOrderReport, List<DataRow> row1/*, int SpecialOrderId*/)
        {
            if (!xtraTabControlRequests.Visible)
                xtraTabControlRequests.Visible = true;

            XtraTabPage xtraTabPage = getTabPage(xtraTabControlRequests, "اضافة طلب مؤقت");
            if (xtraTabPage == null)
            {
                xtraTabControlRequests.TabPages.Add("اضافة طلب مؤقت");
                xtraTabPage = getTabPage(xtraTabControlRequests, "اضافة طلب مؤقت");
            }
            xtraTabPage.Controls.Clear();

            xtraTabControlRequests.SelectedTabPage = xtraTabPage;
            //List<DataRow> row1 = new List<DataRow>();
            DashOrder_Record objForm = new DashOrder_Record(row1, DashOrderReport, xtraTabControlRequests/*, SpecialOrderId*/);
            objForm.TopLevel = false;

            xtraTabPage.Controls.Add(objForm);
            objForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            objForm.Dock = DockStyle.Fill;
            objForm.Show();
        }
        public void bindDisplayDashOrderReportForm(XtraTabPage xtraTabPage)
        {
            DashOrder_Report objForm = new DashOrder_Report(this, xtraTabControlRequests);
            objForm.TopLevel = false;

            xtraTabPage.Controls.Add(objForm);
            objForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            objForm.Dock = DockStyle.Fill;
            objForm.Show();
        }
        public void bindRecordDashRequestForm(DashRequest_Report DashOrderReport, List<DataRow> row1)
        {
            if (!xtraTabControlRequests.Visible)
                xtraTabControlRequests.Visible = true;

            XtraTabPage xtraTabPage = getTabPage(xtraTabControlRequests, "اضافة طلب خاص مؤقت");
            if (xtraTabPage == null)
            {
                xtraTabControlRequests.TabPages.Add("اضافة طلب خاص مؤقت");
                xtraTabPage = getTabPage(xtraTabControlRequests, "اضافة طلب خاص مؤقت");
            }
            xtraTabPage.Controls.Clear();

            xtraTabControlRequests.SelectedTabPage = xtraTabPage;
            DashRequest_Record objForm = new DashRequest_Record(DashOrderReport, xtraTabControlRequests);
            objForm.TopLevel = false;

            xtraTabPage.Controls.Add(objForm);
            objForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            objForm.Dock = DockStyle.Fill;
            objForm.Show();
        }
        public void bindRecordRequestForm(Request_Report OrderReport, List<DataRow> row1)
        {
            if (!xtraTabControlRequests.Visible)
                xtraTabControlRequests.Visible = true;

            XtraTabPage xtraTabPage = getTabPage(xtraTabControlRequests, "اضافة طلب خاص");
            if (xtraTabPage == null)
            {
                xtraTabControlRequests.TabPages.Add("اضافة طلب خاص");
                xtraTabPage = getTabPage(xtraTabControlRequests, "اضافة طلب خاص");
            }
            xtraTabPage.Controls.Clear();

            xtraTabControlRequests.SelectedTabPage = xtraTabPage;
            //List<DataRow> row1 = new List<DataRow>();
            Request_Record objForm = new Request_Record(row1, OrderReport, xtraTabControlRequests);
            objForm.TopLevel = false;

            xtraTabPage.Controls.Add(objForm);
            objForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            objForm.Dock = DockStyle.Fill;
            objForm.Show();
        }
        public void bindDisplayDashRequestReportForm(XtraTabPage xtraTabPage)
        {
            DashRequest_Report objForm = new DashRequest_Report(this, xtraTabControlRequests);
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
        public void bindDisplaySalesProductsBillsNumForm(XtraTabPage xtraTabPage)
        {
            SalesProductsBillsNum_Report objForm = new SalesProductsBillsNum_Report(this);
            objForm.TopLevel = false;

            xtraTabPage.Controls.Add(objForm);
            objForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            objForm.Dock = DockStyle.Fill;
            objForm.Show();
        }
        
        public void bindUpdateOrderForm(DataRowView rows, Order_Report OrderReport)
        {
            /*if (!xtraTabControlRequests.Visible)
                xtraTabControlRequests.Visible = true;

            XtraTabPage xtraTabPage = getTabPage(xtraTabControlRequests, "تعديل طلب");

            if (xtraTabPage == null)
            {
                xtraTabControlRequests.TabPages.Add("تعديل طلب");
                xtraTabPage = getTabPage(xtraTabControlRequests, "تعديل طلب");
            }
            xtraTabPage.Controls.Clear();

            xtraTabControlRequests.SelectedTabPage = xtraTabPage;

            UpdateSupplier objForm = new UpdateSupplier(rows, /*OrderReport*null, xtraTabControlRequests);
            objForm.TopLevel = false;

            xtraTabPage.Controls.Add(objForm);
            objForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            objForm.Dock = DockStyle.Fill;
            objForm.Show();*/
        }
        public void bindUpdateRequestForm(DataRowView rows, Request_Report OrderReport)
        {
            /*if (!xtraTabControlRequests.Visible)
                xtraTabControlRequests.Visible = true;

            XtraTabPage xtraTabPage = getTabPage(xtraTabControlRequests, "تعديل طلب خاص");

            if (xtraTabPage == null)
            {
                xtraTabControlRequests.TabPages.Add("تعديل طلب خاص");
                xtraTabPage = getTabPage(xtraTabControlRequests, "تعديل طلب خاص");
            }
            xtraTabPage.Controls.Clear();

            xtraTabControlRequests.SelectedTabPage = xtraTabPage;

            UpdateSupplier objForm = new UpdateSupplier(rows, /*OrderReport*null, xtraTabControlRequests);
            objForm.TopLevel = false;

            xtraTabPage.Controls.Add(objForm);
            objForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            objForm.Dock = DockStyle.Fill;
            objForm.Show();*/
        }
        public void bindPrintOrderForm()
        {
            /*if (!xtraTabControlRequests.Visible)
                xtraTabControlRequests.Visible = true;

            XtraTabPage xtraTabPage = getTabPage(xtraTabControlRequests, "طباعة الطلبات");
            if (xtraTabPage == null)
            {
                xtraTabControlRequests.TabPages.Add("طباعة الطلبات");
                xtraTabPage = getTabPage(xtraTabControlRequests, "طباعة الطلبات");
            }
            xtraTabPage.Controls.Clear();

            xtraTabControlRequests.SelectedTabPage = xtraTabPage;
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
            if (/*UserControl.userType == 10 || */UserControl.userType == 19 || UserControl.userType == 20 /*|| UserControl.userType == 17*/ || UserControl.userType == 1)
            {
                string q1 = "select Data_ID from storage_least_taswya";
                string q2 = "SELECT order_details.Data_ID FROM orders INNER JOIN order_details ON order_details.Order_ID = orders.Order_ID where orders.Received=0";
                //string q3 = "select data.Data_ID from data inner join storage on data.Data_ID=storage.Data_ID group by data.Data_ID HAVING SUM(storage.Total_Meters) <= least_order.Least_Quantity";
                int count = 0;
                dbconnection.Close();
                string query = "SELECT least_order.Least_Quantity FROM least_order INNER JOIN data ON least_order.Data_ID = data.Data_ID INNER JOIN storage ON storage.Data_ID = data.Data_ID group by data.Data_ID having (SUM(storage.Total_Meters) <= least_order.Least_Quantity=1) and data.Data_ID not in(" + q1 + ") and data.Data_ID not in(" + q2 + ")";
                //string query = "SELECT least_order.Least_Quantity FROM least_order INNER JOIN data ON least_order.Data_ID = data.Data_ID where data.Data_ID in (" + q3 + ") and data.Data_ID not in(" + q1 + ") and data.Data_ID not in(" + q2 + ")";
                MySqlCommand command = new MySqlCommand(query, dbconnection);
                dbconnection.Open();
                MySqlDataReader dr = command.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        count++;
                    }
                    dr.Close();
                    labelPurchaseLeast.Text = count.ToString();
                    labelPurchaseLeast.Visible = true;
                }
                else
                {
                    labelPurchaseLeast.Text = "0";
                    labelPurchaseLeast.Visible = false;
                }
            }
        }

        public void ConfirmedSpecialOrdersFunction()
        {
            if (/*UserControl.userType == 10 ||*/ UserControl.userType == 19 /*|| UserControl.userType == 17*/ || UserControl.userType == 1)
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
    }
}

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

        Timer Purchasetimer = new Timer();
        bool purchaseFlag = false;

        public void PurchasesMainForm()
        {
            try
            {
                LeastQuantityFunction();

                //Calculate the time of the actual work of the delegates
                timer.Interval = 1000 * 60;
                timer.Tick += new EventHandler(GetNonRequestedLeastQuantity);
                timer.Start();
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
                    if (purchaseFlag == false)
                    {
                        xtraTabControlMainContainer.TabPages.Insert(1, PurchasesTP);
                        index++;
                        purchaseFlag = true;
                    }
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

                    XtraTabPage xtraTabPage = getTabPage(xtraTabControlPurchases, "عرض الطلبات");
                    if (xtraTabPage == null)
                    {
                        xtraTabControlPurchases.TabPages.Add("عرض الطلبات");
                        xtraTabPage = getTabPage(xtraTabControlPurchases, "عرض الطلبات");
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

        public void LeastQuantityFunction()
        {
            string q1 = "select Data_ID from storage_least_taswya";
            string q2 = "SELECT order_details.Data_ID FROM orders INNER JOIN order_details ON order_details.Order_ID = orders.Order_ID where orders.Received=0";
            int count = 0;
            dbconnection.Close();
            string query = "SELECT least_offer.Least_Quantity FROM least_offer INNER JOIN data ON least_offer.Data_ID = data.Data_ID INNER JOIN storage ON storage.Data_ID = data.Data_ID group by data.Data_ID having (SUM(storage.Total_Meters) <= least_offer.Least_Quantity=1) and data.Data_ID not in(" + q1 + ") and data.Data_ID not in(" + q2 + ")";
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

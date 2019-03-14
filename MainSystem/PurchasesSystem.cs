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
        
        public void PurchasesMainForm()
        {
            try
            {
                
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
                if (UserControl.userType == 10 || UserControl.userType == 1)
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
        
        //Products Purchase price
        public void bindDisplayProductsPurchasePriceForm(XtraTabPage xtraTabPage)
        {
            ProductsPurchasePriceForm objForm = new ProductsPurchasePriceForm(this);
            objForm.TopLevel = false;

            xtraTabPage.Controls.Add(objForm);
            objForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            objForm.Dock = DockStyle.Fill;
            objForm.Show();
        }
        //record Purchase price 
        public void bindRecordPurchasePriceForm(ProductsPurchasePriceForm productsPurchasePriceForm)
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
            SetPurchasePrice objForm = new SetPurchasePrice(productsPurchasePriceForm, xtraTabControlPurchases);
            objForm.TopLevel = false;

            xtraTabPage.Controls.Add(objForm);
            objForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            objForm.Dock = DockStyle.Fill;
            objForm.Show();
        }
        //update Purchase price 
        public void bindUpdatePurchasePriceForm(List<DataRowView> rows, ProductsPurchasePriceForm productsPurchasePriceForm, String query)
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

            UpdatePurchasePrice objForm = new UpdatePurchasePrice(rows, productsPurchasePriceForm, query, xtraTabControlPurchases);
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
            ProductPurchasePricesReport objForm = new ProductPurchasePricesReport(gridControl);
            objForm.TopLevel = false;

            xtraTabPage.Controls.Add(objForm);
            objForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            objForm.Dock = DockStyle.Fill;
            objForm.Show();
        }

        public void bindDisplayProductsPurchasesPriceForm(XtraTabPage xtraTabPage)
        {
            ProductsPurchasePriceForm objForm = new ProductsPurchasePriceForm(this);
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
            SetPurchasesPrice objForm = new SetPurchasesPrice(productsSellPriceForm, xtraTabControlPurchases);
            objForm.TopLevel = false;

            xtraTabPage.Controls.Add(objForm);
            objForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            objForm.Dock = DockStyle.Fill;
            objForm.Show();
        }
        public void bindUpdatePurchasesPriceForm(List<DataRowView> rows, ProductsPurchasesPriceForm productsSellPriceForm, String query)
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

            UpdatePurchasesPrice objForm = new UpdatePurchasesPrice(rows, productsSellPriceForm, query, xtraTabControlPurchases);
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

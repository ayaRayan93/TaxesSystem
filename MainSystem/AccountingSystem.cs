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
using DevExpress.XtraGrid.Views.Grid;

namespace MainSystem
{
    class AccountingSystem
    {
    }
    public partial class MainForm
    {
        private void navBarItemDelegateTotalSales_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            try
            {
                restForeColorOfNavBarItem();
                NavBarItem navBarItem = (NavBarItem)sender;
                navBarItem.Appearance.ForeColor = Color.Blue;
                if (!xtraTabControlAccounting.Visible)
                    xtraTabControlAccounting.Visible = true;

                XtraTabPage xtraTabPage = getTabPage(xtraTabControlAccounting, "مبيعات مندوب لفترة");
                if (xtraTabPage == null)
                {
                    xtraTabControlAccounting.TabPages.Add("مبيعات مندوب لفترة");
                    xtraTabPage = getTabPage(xtraTabControlAccounting, "مبيعات مندوب لفترة");
                }

                xtraTabPage.Controls.Clear();
                xtraTabControlAccounting.SelectedTabPage = xtraTabPage;

                DelegateTotalSales objForm = new DelegateTotalSales(this);

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
        private void navBarItemDelegateSalesForCompany_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            try
            {
                restForeColorOfNavBarItem();
                NavBarItem navBarItem = (NavBarItem)sender;
                navBarItem.Appearance.ForeColor = Color.Blue;
                if (!xtraTabControlAccounting.Visible)
                    xtraTabControlAccounting.Visible = true;

                XtraTabPage xtraTabPage = getTabPage(xtraTabControlAccounting, "مبيعات المندوبين لشركة");
                if (xtraTabPage == null)
                {
                    xtraTabControlAccounting.TabPages.Add("مبيعات المندوبين لشركة");
                    xtraTabPage = getTabPage(xtraTabControlAccounting, "مبيعات المندوبين لشركة");
                }

                xtraTabPage.Controls.Clear();
                xtraTabControlAccounting.SelectedTabPage = xtraTabPage;

                DelegateSalesForCompany objForm = new DelegateSalesForCompany(this);

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
        private void navBarItemDelegateSalesForProduct_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            try
            {
                restForeColorOfNavBarItem();
                NavBarItem navBarItem = (NavBarItem)sender;
                navBarItem.Appearance.ForeColor = Color.Blue;
                if (!xtraTabControlAccounting.Visible)
                    xtraTabControlAccounting.Visible = true;

                XtraTabPage xtraTabPage = getTabPage(xtraTabControlAccounting, "مبيعات المندوبين لصنف");
                if (xtraTabPage == null)
                {
                    xtraTabControlAccounting.TabPages.Add("مبيعات المندوبين لصنف");
                    xtraTabPage = getTabPage(xtraTabControlAccounting, "مبيعات المندوبين لصنف");
                    xtraTabPage.RightToLeft = RightToLeft.No;
                }
                else
                {
                    xtraTabPage.RightToLeft = RightToLeft.No;
                }

                xtraTabPage.Controls.Clear();
                xtraTabControlAccounting.SelectedTabPage = xtraTabPage;

                DelegateSalesForProduct objForm = new DelegateSalesForProduct(this);

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
        private void DelegateProfit_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            try
            {
                restForeColorOfNavBarItem();
                NavBarItem navBarItem = (NavBarItem)sender;
                navBarItem.Appearance.ForeColor = Color.Blue;
                if (!xtraTabControlAccounting.Visible)
                    xtraTabControlAccounting.Visible = true;

                XtraTabPage xtraTabPage = getTabPage(xtraTabControlAccounting, "حساب نسبة المندوب");
                if (xtraTabPage == null)
                {
                    xtraTabControlAccounting.TabPages.Add("حساب نسبة المندوب");
                    xtraTabPage = getTabPage(xtraTabControlAccounting, "حساب نسبة المندوب");
                    xtraTabPage.RightToLeft = RightToLeft.No;
                }
                else
                {
                    xtraTabPage.RightToLeft = RightToLeft.No;
                }

                xtraTabPage.Controls.Clear();
                xtraTabControlAccounting.SelectedTabPage = xtraTabPage;

                GetDelegateProfit objForm = new GetDelegateProfit(this);

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

        private void navBarItemSupplierAccount_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            try
            {
                restForeColorOfNavBarItem();
                NavBarItem navBarItem = (NavBarItem)sender;
                navBarItem.Appearance.ForeColor = Color.Blue;
                if (!xtraTabControlAccounting.Visible)
                    xtraTabControlAccounting.Visible = true;

                XtraTabPage xtraTabPage = getTabPage(xtraTabControlAccounting, "عرض حسابات الموردين");
                if (xtraTabPage == null)
                {
                    xtraTabControlAccounting.TabPages.Add("عرض حسابات الموردين");
                    xtraTabPage = getTabPage(xtraTabControlAccounting, "عرض حسابات الموردين");
                }

                xtraTabPage.Controls.Clear();
                xtraTabControlAccounting.SelectedTabPage = xtraTabPage;

                SupplierAccount_Report objForm = new SupplierAccount_Report(this, xtraTabControlAccounting);

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
                if (!xtraTabControlAccounting.Visible)
                    xtraTabControlAccounting.Visible = true;
                //عرض سدادات الموردين
                XtraTabPage xtraTabPage = getTabPage(xtraTabControlAccounting, "عرض تحصيلات الموردين");
                if (xtraTabPage == null)
                {
                    xtraTabControlAccounting.TabPages.Add("عرض تحصيلات الموردين");
                    xtraTabPage = getTabPage(xtraTabControlAccounting, "عرض تحصيلات الموردين");
                }

                xtraTabPage.Controls.Clear();
                xtraTabControlAccounting.SelectedTabPage = xtraTabPage;

                SupplierSoonPayments_Report objForm = new SupplierSoonPayments_Report(this, xtraTabControlAccounting);

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

        private void navBarItemSupplierBillReport_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            try
            {
                restForeColorOfNavBarItem();
                NavBarItem navBarItem = (NavBarItem)sender;
                navBarItem.Appearance.ForeColor = Color.Blue;
                if (!xtraTabControlAccounting.Visible)
                    xtraTabControlAccounting.Visible = true;

                XtraTabPage xtraTabPage = getTabPage(xtraTabControlAccounting, "تقرير فواتير الموردين");
                if (xtraTabPage == null)
                {
                    xtraTabControlAccounting.TabPages.Add("تقرير فواتير الموردين");
                    xtraTabPage = getTabPage(xtraTabControlAccounting, "تقرير فواتير الموردين");
                }

                xtraTabPage.Controls.Clear();
                xtraTabControlAccounting.SelectedTabPage = xtraTabPage;

                SupplierBills_Report objForm = new SupplierBills_Report(this, xtraTabControlAccounting);

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
                if (!xtraTabControlAccounting.Visible)
                    xtraTabControlAccounting.Visible = true;

                XtraTabPage xtraTabPage = getTabPage(xtraTabControlAccounting, "حركة سدادات الموردين");
                if (xtraTabPage == null)
                {
                    xtraTabControlAccounting.TabPages.Add("حركة سدادات الموردين");
                    xtraTabPage = getTabPage(xtraTabControlAccounting, "حركة سدادات الموردين");
                }

                xtraTabPage.Controls.Clear();
                xtraTabControlAccounting.SelectedTabPage = xtraTabPage;

                SupplierTransitions_Report objForm = new SupplierTransitions_Report(this, xtraTabControlAccounting);

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
                if (!xtraTabControlAccounting.Visible)
                    xtraTabControlAccounting.Visible = true;

                XtraTabPage xtraTabPage = getTabPage(xtraTabControlAccounting, "حركة المسحوبات والمرتجعات");
                if (xtraTabPage == null)
                {
                    xtraTabControlAccounting.TabPages.Add("حركة المسحوبات والمرتجعات");
                    xtraTabPage = getTabPage(xtraTabControlAccounting, "حركة المسحوبات والمرتجعات");
                }

                xtraTabPage.Controls.Clear();
                xtraTabControlAccounting.SelectedTabPage = xtraTabPage;

                SupplierBillsTransitions_Report objForm = new SupplierBillsTransitions_Report(this, xtraTabControlAccounting);

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
                if (!xtraTabControlAccounting.Visible)
                    xtraTabControlAccounting.Visible = true;

                XtraTabPage xtraTabPage = getTabPage(xtraTabControlAccounting, "تفاصيل حركة المسحوبات والمرتجعات");
                if (xtraTabPage == null)
                {
                    xtraTabControlAccounting.TabPages.Add("تفاصيل حركة المسحوبات والمرتجعات");
                    xtraTabPage = getTabPage(xtraTabControlAccounting, "تفاصيل حركة المسحوبات والمرتجعات");
                }

                xtraTabPage.Controls.Clear();
                xtraTabControlAccounting.SelectedTabPage = xtraTabPage;

                SupplierBillsTransitionsDetails_Report objForm = new SupplierBillsTransitionsDetails_Report(this, xtraTabControlAccounting);

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
                if (!xtraTabControlAccounting.Visible)
                    xtraTabControlAccounting.Visible = true;

                XtraTabPage xtraTabPage = getTabPage(xtraTabControlAccounting, "عرض التسويات");
                if (xtraTabPage == null)
                {
                    xtraTabControlAccounting.TabPages.Add("عرض التسويات");
                    xtraTabPage = getTabPage(xtraTabControlAccounting, "عرض التسويات");
                }

                xtraTabPage.Controls.Clear();
                xtraTabControlAccounting.SelectedTabPage = xtraTabPage;

                SupplierTaswayaReport objForm = new SupplierTaswayaReport(this, xtraTabControlAccounting);

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

        public void displayDelegateReport(GridControl gridControl, dataX d)
        {
            Delegate_Report objForm = new Delegate_Report(gridControl,d);

            objForm.TopLevel = false;
            XtraTabPage xtraTabPage = getTabPage(xtraTabControlAccounting, "تقرير مبيعات المناديب");
            if (xtraTabPage == null)
            {
                xtraTabControlAccounting.TabPages.Add("تقرير مبيعات المناديب");
                xtraTabPage = getTabPage(xtraTabControlAccounting, "تقرير مبيعات المناديب");

            }
            xtraTabPage.Controls.Clear();
            xtraTabPage.Controls.Add(objForm);
            xtraTabControlAccounting.SelectedTabPage = xtraTabPage;

            objForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            objForm.Dock = DockStyle.Fill;
            objForm.Show();
        }
        
        public void bindTaswayaSupplierForm()
        {
            if (!xtraTabControlAccounting.Visible)
                xtraTabControlAccounting.Visible = true;

            XtraTabPage xtraTabPage = getTabPage(xtraTabControlAccounting, "اضافة تسوية لمورد");
            if (xtraTabPage == null)
            {
                xtraTabControlAccounting.TabPages.Add("اضافة تسوية لمورد");
                xtraTabPage = getTabPage(xtraTabControlAccounting, "اضافة تسوية لمورد");
            }
            xtraTabPage.Controls.Clear();
            xtraTabControlAccounting.SelectedTabPage = xtraTabPage;

            SupplierTaswaya objForm = new SupplierTaswaya();
            objForm.TopLevel = false;
            xtraTabPage.Controls.Add(objForm);
            objForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            objForm.Dock = DockStyle.Fill;
            objForm.Show();
        }
        public void bindUpdateTaswayaSupplierForm(DataRowView row, SupplierTaswayaReport SupplierTaswayaReport)
        {
            if (!xtraTabControlAccounting.Visible)
                xtraTabControlAccounting.Visible = true;

            XtraTabPage xtraTabPage = getTabPage(xtraTabControlAccounting, "تعديل تسوية مورد");
            if (xtraTabPage == null)
            {
                xtraTabControlAccounting.TabPages.Add("تعديل تسوية مورد");
                xtraTabPage = getTabPage(xtraTabControlAccounting, "تعديل تسوية مورد");
            }
            xtraTabPage.Controls.Clear();
            xtraTabControlAccounting.SelectedTabPage = xtraTabPage;

            UpdateSupplierTaswaya objForm = new UpdateSupplierTaswaya(row, this, SupplierTaswayaReport);
            objForm.TopLevel = false;
            xtraTabPage.Controls.Add(objForm);
            objForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            objForm.Dock = DockStyle.Fill;
            objForm.Show();
        }
    }
}

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

        private void navBarItemSupplierPayments_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            try
            {
                restForeColorOfNavBarItem();
                NavBarItem navBarItem = (NavBarItem)sender;
                navBarItem.Appearance.ForeColor = Color.Blue;

                if (!xtraTabControlAccounting.Visible)
                    xtraTabControlAccounting.Visible = true;
                
                XtraTabPage xtraTabPage = getTabPage(xtraTabControlAccounting, "اضافة سداد لمورد");
                if (xtraTabPage == null)
                {
                    xtraTabControlAccounting.TabPages.Add("اضافة سداد لمورد");
                    xtraTabPage = getTabPage(xtraTabControlAccounting, "اضافة سداد لمورد");
                }
                xtraTabPage.Controls.Clear();
                xtraTabControlAccounting.SelectedTabPage = xtraTabPage;
                SupplierPayments_Record objForm = new SupplierPayments_Record(xtraTabControlAccounting);

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
    }
}

﻿using DevExpress.XtraGrid;
using DevExpress.XtraNavBar;
using DevExpress.XtraTab;
using TaxesSystem.CustomerService;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TaxesSystem
{
    class CustomerServiceSystem
    {
    }
    public partial class MainForm
    {

        private void navBarItemSearchByPhone_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            try
            {
                restForeColorOfNavBarItem();
                NavBarItem navBarItem = (NavBarItem)sender;
                navBarItem.Appearance.ForeColor = Color.Blue;
                if (!xtraTabControlCustomerService.Visible)
                    xtraTabControlCustomerService.Visible = true;

                XtraTabPage xtraTabPage = getTabPage(xtraTabControlCustomerService, "تقرير البحث برقم التلفون");
                if (xtraTabPage == null)
                {
                    xtraTabControlCustomerService.TabPages.Add("تقرير البحث برقم التلفون");
                    xtraTabPage = getTabPage(xtraTabControlCustomerService, "تقرير البحث برقم التلفون");
                }

                xtraTabPage.Controls.Clear();
                xtraTabControlCustomerService.SelectedTabPage = xtraTabPage;

                displayCustomerBill objForm = new displayCustomerBill();

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
        private void navBarItem266_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            try
            {
                restForeColorOfNavBarItem();
                NavBarItem navBarItem = (NavBarItem)sender;
                navBarItem.Appearance.ForeColor = Color.Blue;
                if (!xtraTabControlCustomerService.Visible)
                    xtraTabControlCustomerService.Visible = true;

                XtraTabPage xtraTabPage = getTabPage(xtraTabControlCustomerService, "فواتير تم تسليمها");
                if (xtraTabPage == null)
                {
                    xtraTabControlCustomerService.TabPages.Add("فواتير تم تسليمها");
                    xtraTabPage = getTabPage(xtraTabControlCustomerService, "فواتير تم تسليمها");
                }

                xtraTabPage.Controls.Clear();
                xtraTabControlCustomerService.SelectedTabPage = xtraTabPage;

                CustomerDeliveredBills objForm = new CustomerDeliveredBills(this);

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

        //functions
        public void bindReportDeliveredCustomerBillsForm(GridControl gridControl, string title)
        {
            SetReport objForm = new SetReport(gridControl, title);
            objForm.TopLevel = false;
            XtraTabPage xtraTabPage = getTabPage(xtraTabControlCustomerService, title);
            if (xtraTabPage == null)
            {
                xtraTabControlCustomerService.TabPages.Add(title);
                xtraTabPage = getTabPage(xtraTabControlCustomerService, title);
            }
            xtraTabPage.Controls.Clear();
            xtraTabControlCustomerService.SelectedTabPage = xtraTabPage;
            objForm.TopLevel = false;
            xtraTabPage.Controls.Add(objForm);
        
            objForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            objForm.Dock = DockStyle.Fill;
            objForm.Show();
        }
        public void bindDisplayDeliveryBillsForm(DataRow dataRow)
        {
            if (!xtraTabControlCustomerService.Visible)
                xtraTabControlCustomerService.Visible = true;

            XtraTabPage xtraTabPage = getTabPage(xtraTabControlCustomerService, "استبيان");
            if (xtraTabPage == null)
            {
                xtraTabControlCustomerService.TabPages.Add("استبيان");
                xtraTabPage = getTabPage(xtraTabControlCustomerService, "استبيان");
            }

            xtraTabPage.Controls.Clear();
            xtraTabControlCustomerService.SelectedTabPage = xtraTabPage;

            CustomerServiceAfterReceived_Report objForm = new CustomerServiceAfterReceived_Report(xtraTabControlCustomerService, this, dataRow);

            objForm.TopLevel = false;
            xtraTabPage.Controls.Add(objForm);
            objForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            objForm.Dock = DockStyle.Fill;
            objForm.Show();
        }
        private void navBarItem89_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            if (!xtraTabControlCustomerService.Visible)
                xtraTabControlCustomerService.Visible = true;

            XtraTabPage xtraTabPage = getTabPage(xtraTabControlCustomerService, "استبيانات العملاء");
            if (xtraTabPage == null)
            {
                xtraTabControlCustomerService.TabPages.Add("استبيانات العملاء");
                xtraTabPage = getTabPage(xtraTabControlCustomerService, "استبيانات العملاء");
            }

            xtraTabPage.Controls.Clear();
            xtraTabControlCustomerService.SelectedTabPage = xtraTabPage;

            CustomerSurvaryReport objForm = new CustomerSurvaryReport(this);

            objForm.TopLevel = false;
            xtraTabPage.Controls.Add(objForm);
            objForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            objForm.Dock = DockStyle.Fill;
            objForm.Show();
        }
    }

}

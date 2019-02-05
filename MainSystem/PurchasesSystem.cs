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

        public void bindDisplayProductsPurchasesPriceForm(XtraTabPage xtraTabPage)
        {
            ProductsPurchasesPriceForm objForm = new ProductsPurchasesPriceForm(this);
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
    }
}

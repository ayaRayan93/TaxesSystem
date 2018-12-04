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

                DelegateTotalSales objForm = new DelegateTotalSales();

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
    }
}

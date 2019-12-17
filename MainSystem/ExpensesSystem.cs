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
    class ExpensesSystem
    {
    }
    public partial class MainForm
    {
        public static XtraTabControl tabControlExpenses;

        public void initializeBranch()
        {
            //bankSystem
            tabControlExpenses = xtraTabControlExpenses;
        }

        private void navBarItemMainSubReport_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            try
            {
                restForeColorOfNavBarItem();
                NavBarItem navBarItem = (NavBarItem)sender;
                navBarItem.Appearance.ForeColor = Color.Blue;

                if (!xtraTabControlExpenses.Visible)
                    xtraTabControlExpenses.Visible = true;

                XtraTabPage xtraTabPage = getTabPage(xtraTabControlExpenses, "تكويد المصروفات");
                if (xtraTabPage == null)
                {
                    xtraTabControlExpenses.TabPages.Add("تكويد المصروفات");
                    xtraTabPage = getTabPage(xtraTabControlExpenses, "تكويد المصروفات");
                }
                xtraTabPage.Controls.Clear();

                xtraTabControlExpenses.SelectedTabPage = xtraTabPage;
                Main_Sub objFormExpenses = new Main_Sub(xtraTabControlExpenses);
                objFormExpenses.TopLevel = false;

                xtraTabPage.Controls.Add(objFormExpenses);
                objFormExpenses.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                objFormExpenses.Dock = DockStyle.Fill;
                objFormExpenses.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void navBarItemExpenseRecord_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            try
            {
                restForeColorOfNavBarItem();
                NavBarItem navBarItem = (NavBarItem)sender;
                navBarItem.Appearance.ForeColor = Color.Blue;

                if (!xtraTabControlExpenses.Visible)
                    xtraTabControlExpenses.Visible = true;

                XtraTabPage xtraTabPage = getTabPage(xtraTabControlExpenses, "تسجيل مصروف");
                if (xtraTabPage == null)
                {
                    xtraTabControlExpenses.TabPages.Add("تسجيل مصروف");
                    xtraTabPage = getTabPage(xtraTabControlExpenses, "تسجيل مصروف");
                }
                xtraTabPage.Controls.Clear();

                xtraTabControlExpenses.SelectedTabPage = xtraTabPage;
                SafeExpense_Record objFormExpenses = new SafeExpense_Record(xtraTabControlExpenses);
                objFormExpenses.TopLevel = false;

                xtraTabPage.Controls.Add(objFormExpenses);
                objFormExpenses.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                objFormExpenses.Dock = DockStyle.Fill;
                objFormExpenses.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void navBarItemSafeExpenseIncomeRecord_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            try
            {
                restForeColorOfNavBarItem();
                NavBarItem navBarItem = (NavBarItem)sender;
                navBarItem.Appearance.ForeColor = Color.Blue;

                if (!xtraTabControlExpenses.Visible)
                    xtraTabControlExpenses.Visible = true;

                XtraTabPage xtraTabPage = getTabPage(xtraTabControlExpenses, "تسجيل وارد لمصروف");
                if (xtraTabPage == null)
                {
                    xtraTabControlExpenses.TabPages.Add("تسجيل وارد لمصروف");
                    xtraTabPage = getTabPage(xtraTabControlExpenses, "تسجيل وارد لمصروف");
                }
                xtraTabPage.Controls.Clear();

                xtraTabControlExpenses.SelectedTabPage = xtraTabPage;
                SafeExpenseIncome_Record objFormExpenses = new SafeExpenseIncome_Record(xtraTabControlExpenses);
                objFormExpenses.TopLevel = false;

                xtraTabPage.Controls.Add(objFormExpenses);
                objFormExpenses.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                objFormExpenses.Dock = DockStyle.Fill;
                objFormExpenses.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void navBarItemExpensesTransitionsReport_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            try
            {
                restForeColorOfNavBarItem();
                NavBarItem navBarItem = (NavBarItem)sender;
                navBarItem.Appearance.ForeColor = Color.Blue;

                if (!xtraTabControlExpenses.Visible)
                    xtraTabControlExpenses.Visible = true;

                XtraTabPage xtraTabPage = getTabPage(xtraTabControlExpenses, "حركة المصروفات");
                if (xtraTabPage == null)
                {
                    xtraTabControlExpenses.TabPages.Add("حركة المصروفات");
                    xtraTabPage = getTabPage(xtraTabControlExpenses, "حركة المصروفات");
                }
                xtraTabPage.Controls.Clear();

                xtraTabControlExpenses.SelectedTabPage = xtraTabPage;
                Expenses_Transitions_Report objFormExpenses = new Expenses_Transitions_Report(xtraTabControlExpenses);
                objFormExpenses.TopLevel = false;

                xtraTabPage.Controls.Add(objFormExpenses);
                objFormExpenses.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                objFormExpenses.Dock = DockStyle.Fill;
                objFormExpenses.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}

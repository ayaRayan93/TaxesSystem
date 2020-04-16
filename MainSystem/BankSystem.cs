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
    class BankSystem
    {
    }

    public partial class MainForm
    {
        //bankSystem
        public static XtraTabControl tabControlBank;
        public static bool loadedPrintBank = false;
        public static bool loadedPrintAgl = false;
        public static bool loadedPrintCash = false;
        public static bool loadedPrintIncome = false;
        public static bool loadedPrintPayAccount = false;
        public static bool loadedPrintPullAgl = false;
        public static bool loadedPrintPullCash = false;
        public static bool loadedPrintPullExpense = false;
        public static bool loadedPrintPullPayAccount = false;
        public static bool loadedPrintBankTransfer = false;

        public static Bank_Report formShow;
        public static BankDepositCash_Report DepositCashShow;
        public static BankDepositAgl_Report DepositAglShow;
        public static BankDepositPayAccount_Report DepositPayAccountShow;
        public static BankDepositIncome_Report DepositIncomeShow;
        public static BankPullAgl_Report PullAglShow;
        public static BankPullCash_Report PullCashShow;
        public static BankPullExpense_Report PullExpensesShow;
        public static BankPullPayAccount_Report PullPayAccountShow;
        public static BankTransfers_Report BankTransferShow;
        public static BankSupplierPullAgl_Report SupplierPullAglShow;

        XtraTabPage tabPageBankReport;
        Panel panelBankReport;
        XtraTabPage tabPageDepositCashReport;
        Panel panelDepositCashReport;
        XtraTabPage tabPageDepositAglReport;
        Panel panelDepositAglReport;
        XtraTabPage tabPageDepositPayAccountReport;
        Panel panelDepositPayAccountReport;
        XtraTabPage tabPageDepositIncomeReport;
        Panel panelDepositIncomeReport;
        XtraTabPage tabPagePullAglReport;
        Panel panelPullAglReport;
        XtraTabPage tabPagePullCashReport;
        Panel panelPullCashReport;
        XtraTabPage tabPagePullExpensesReport;
        Panel panelPullExpensesReport;
        XtraTabPage tabPagePullPayAccountReport;
        Panel panelPullPayAccountReport;
        XtraTabPage tabPageBankTransferReport;
        Panel panelBankTransferReport;
        XtraTabPage tabPageSupplierPullAglReport;
        Panel panelSupplierPullAglReport;

        public void initialize()
        {
            //bankSystem
            tabControlBank = MainTabControlBank;
            tabPageBankReport = new XtraTabPage();
            panelBankReport = new Panel();
            tabPageDepositCashReport = new XtraTabPage();
            panelDepositCashReport = new Panel();
            tabPageDepositAglReport = new XtraTabPage();
            panelDepositAglReport = new Panel();
            tabPageDepositPayAccountReport = new XtraTabPage();
            panelDepositPayAccountReport = new Panel();
            tabPageDepositIncomeReport = new XtraTabPage();
            panelDepositIncomeReport = new Panel();
            tabPagePullAglReport = new XtraTabPage();
            panelPullAglReport = new Panel();
            tabPagePullCashReport = new XtraTabPage();
            panelPullCashReport = new Panel();
            tabPagePullExpensesReport = new XtraTabPage();
            panelPullExpensesReport = new Panel();
            tabPagePullPayAccountReport = new XtraTabPage();
            panelPullPayAccountReport = new Panel();
            tabPageBankTransferReport = new XtraTabPage();
            panelBankTransferReport = new Panel();
            tabPageSupplierPullAglReport = new XtraTabPage();
            panelSupplierPullAglReport = new Panel();
        }

        private void navBarItemShow_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            try
            {
                if (UserControl.userType == 1)
                {
                    NavBarItem navBarItem = (NavBarItem)sender;
                    navBarItem.Appearance.ForeColor = Color.FromArgb(54, 70, 151);

                    XtraTabPage xtraTabPage = getTabPage(MainTabControlBank, "tabPageBankReport");
                    if (xtraTabPage == null)
                    {
                        tabPageBankReport.Name = "tabPageBankReport";
                        tabPageBankReport.Text = "عرض البنوك";
                        panelBankReport.Name = "panelBankReport";
                        panelBankReport.Dock = DockStyle.Fill;

                        formShow = new Bank_Report();
                        formShow.Size = new Size(1109, 660);
                        formShow.TopLevel = false;
                        formShow.FormBorderStyle = FormBorderStyle.None;
                        formShow.Dock = DockStyle.Fill;
                    }
                    panelBankReport.Controls.Clear();
                    panelBankReport.Controls.Add(formShow);
                    tabPageBankReport.Controls.Add(panelBankReport);
                    MainTabControlBank.TabPages.Add(tabPageBankReport);
                    formShow.Show();
                    MainTabControlBank.SelectedTabPage = tabPageBankReport;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        
        private void navBarItemDepositCash_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            try
            {
                restForeColorOfNavBarItem();
                NavBarItem navBarItem = (NavBarItem)sender;
                navBarItem.Appearance.ForeColor = Color.FromArgb(54, 70, 151);

                XtraTabPage xtraTabPage = getTabPage(MainTabControlBank, "tabPageDepositCashReport");
                if (xtraTabPage == null)
                {
                    tabPageDepositCashReport.Name = "tabPageDepositCashReport";
                    tabPageDepositCashReport.Text = "عرض الايداعات-كاش";
                    panelDepositCashReport.Name = "panelDepositCashReport";
                    panelDepositCashReport.Dock = DockStyle.Fill;

                    DepositCashShow = new BankDepositCash_Report(this);
                    DepositCashShow.Size = new Size(1109, 660);
                    DepositCashShow.TopLevel = false;
                    DepositCashShow.FormBorderStyle = FormBorderStyle.None;
                    DepositCashShow.Dock = DockStyle.Fill;
                }
                panelDepositCashReport.Controls.Clear();
                panelDepositCashReport.Controls.Add(DepositCashShow);
                tabPageDepositCashReport.Controls.Add(panelDepositCashReport);
                MainTabControlBank.TabPages.Add(tabPageDepositCashReport);
                DepositCashShow.Show();
                MainTabControlBank.SelectedTabPage = tabPageDepositCashReport;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void navBarItemDepositAgl_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            try
            {
                restForeColorOfNavBarItem();
                NavBarItem navBarItem = (NavBarItem)sender;
                navBarItem.Appearance.ForeColor = Color.FromArgb(54, 70, 151);

                XtraTabPage xtraTabPage = getTabPage(MainTabControlBank, "tabPageDepositAglReport");
                if (xtraTabPage == null)
                {
                    tabPageDepositAglReport.Name = "tabPageDepositAglReport";
                    tabPageDepositAglReport.Text = "عرض الايداعات-آجل";
                    panelDepositAglReport.Name = "panelDepositAglReport";
                    panelDepositAglReport.Dock = DockStyle.Fill;

                    DepositAglShow = new BankDepositAgl_Report(this);
                    DepositAglShow.Size = new Size(1109, 660);
                    DepositAglShow.TopLevel = false;
                    DepositAglShow.FormBorderStyle = FormBorderStyle.None;
                    DepositAglShow.Dock = DockStyle.Fill;
                }
                panelDepositAglReport.Controls.Clear();
                panelDepositAglReport.Controls.Add(DepositAglShow);
                tabPageDepositAglReport.Controls.Add(panelDepositAglReport);
                MainTabControlBank.TabPages.Add(tabPageDepositAglReport);
                DepositAglShow.Show();
                MainTabControlBank.SelectedTabPage = tabPageDepositAglReport;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void navBarItemPayAccount_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            try
            {
                restForeColorOfNavBarItem();
                NavBarItem navBarItem = (NavBarItem)sender;
                navBarItem.Appearance.ForeColor = Color.FromArgb(54, 70, 151);

                XtraTabPage xtraTabPage = getTabPage(MainTabControlBank, "tabPageDepositPayAccount");
                if (xtraTabPage == null)
                {
                    tabPageDepositPayAccountReport.Name = "tabPageDepositPayAccount";
                    tabPageDepositPayAccountReport.Text = "عرض الايداعات-تحت حساب";
                    panelDepositPayAccountReport.Name = "panelDepositPayAccount";
                    panelDepositPayAccountReport.Dock = DockStyle.Fill;

                    DepositPayAccountShow = new BankDepositPayAccount_Report();
                    DepositPayAccountShow.Size = new Size(1109, 660);
                    DepositPayAccountShow.TopLevel = false;
                    DepositPayAccountShow.FormBorderStyle = FormBorderStyle.None;
                    DepositPayAccountShow.Dock = DockStyle.Fill;
                }
                panelDepositPayAccountReport.Controls.Clear();
                panelDepositPayAccountReport.Controls.Add(DepositPayAccountShow);
                tabPageDepositPayAccountReport.Controls.Add(panelDepositPayAccountReport);
                MainTabControlBank.TabPages.Add(tabPageDepositPayAccountReport);
                DepositPayAccountShow.Show();
                MainTabControlBank.SelectedTabPage = tabPageDepositPayAccountReport;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void navBarItemIncome_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            try
            {
                restForeColorOfNavBarItem();
                NavBarItem navBarItem = (NavBarItem)sender;
                navBarItem.Appearance.ForeColor = Color.FromArgb(54, 70, 151);

                XtraTabPage xtraTabPage = getTabPage(MainTabControlBank, "tabPageDepositIncome");
                if (xtraTabPage == null)
                {
                    tabPageDepositIncomeReport.Name = "tabPageDepositIncome";
                    tabPageDepositIncomeReport.Text = "عرض الايداعات-ايراد";
                    panelDepositIncomeReport.Name = "panelDepositIncome";
                    panelDepositIncomeReport.Dock = DockStyle.Fill;

                    DepositIncomeShow = new BankDepositIncome_Report(this);
                    DepositIncomeShow.Size = new Size(1109, 660);
                    DepositIncomeShow.TopLevel = false;
                    DepositIncomeShow.FormBorderStyle = FormBorderStyle.None;
                    DepositIncomeShow.Dock = DockStyle.Fill;
                }
                panelDepositIncomeReport.Controls.Clear();
                panelDepositIncomeReport.Controls.Add(DepositIncomeShow);
                tabPageDepositIncomeReport.Controls.Add(panelDepositIncomeReport);
                MainTabControlBank.TabPages.Add(tabPageDepositIncomeReport);
                DepositIncomeShow.Show();
                MainTabControlBank.SelectedTabPage = tabPageDepositIncomeReport;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void navBarItemPullCash_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            try
            {
                restForeColorOfNavBarItem();
                NavBarItem navBarItem = (NavBarItem)sender;
                navBarItem.Appearance.ForeColor = Color.FromArgb(54, 70, 151);

                XtraTabPage xtraTabPage = getTabPage(MainTabControlBank, "tabPagePullCashReport");
                if (xtraTabPage == null)
                {
                    tabPagePullCashReport.Name = "tabPagePullCashReport";
                    tabPagePullCashReport.Text = "عرض السحوبات-كاش";
                    panelPullCashReport.Name = "panelPullCashReport";
                    panelPullCashReport.Dock = DockStyle.Fill;

                    PullCashShow = new BankPullCash_Report(this);
                    PullCashShow.Size = new Size(1109, 660);
                    PullCashShow.TopLevel = false;
                    PullCashShow.FormBorderStyle = FormBorderStyle.None;
                    PullCashShow.Dock = DockStyle.Fill;
                }
                panelPullCashReport.Controls.Clear();
                panelPullCashReport.Controls.Add(PullCashShow);
                tabPagePullCashReport.Controls.Add(panelPullCashReport);
                MainTabControlBank.TabPages.Add(tabPagePullCashReport);
                PullCashShow.Show();
                MainTabControlBank.SelectedTabPage = tabPagePullCashReport;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void navBarItemPullAgl_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            try
            {
                restForeColorOfNavBarItem();
                NavBarItem navBarItem = (NavBarItem)sender;
                navBarItem.Appearance.ForeColor = Color.FromArgb(54, 70, 151);

                XtraTabPage xtraTabPage = getTabPage(MainTabControlBank, "tabPagePullAglReport");
                if (xtraTabPage == null)
                {
                    tabPagePullAglReport.Name = "tabPagePullAglReport";
                    tabPagePullAglReport.Text = "عرض السحوبات-آجل";
                    panelPullAglReport.Name = "panelPullAglReport";
                    panelPullAglReport.Dock = DockStyle.Fill;

                    PullAglShow = new BankPullAgl_Report(this);
                    PullAglShow.Size = new Size(1109, 660);
                    PullAglShow.TopLevel = false;
                    PullAglShow.FormBorderStyle = FormBorderStyle.None;
                    PullAglShow.Dock = DockStyle.Fill;
                }
                panelPullAglReport.Controls.Clear();
                panelPullAglReport.Controls.Add(PullAglShow);
                tabPagePullAglReport.Controls.Add(panelPullAglReport);
                MainTabControlBank.TabPages.Add(tabPagePullAglReport);
                PullAglShow.Show();
                MainTabControlBank.SelectedTabPage = tabPagePullAglReport;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void navBarItemExpense_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            try
            {
                restForeColorOfNavBarItem();
                NavBarItem navBarItem = (NavBarItem)sender;
                navBarItem.Appearance.ForeColor = Color.FromArgb(54, 70, 151);

                XtraTabPage xtraTabPage = getTabPage(MainTabControlBank, "tabPagePullExpenseReport");
                if (xtraTabPage == null)
                {
                    tabPagePullExpensesReport.Name = "tabPagePullExpenseReport";
                    tabPagePullExpensesReport.Text = "عرض السحوبات-مصروفات";
                    panelPullExpensesReport.Name = "panelPullExpenseReport";
                    panelPullExpensesReport.Dock = DockStyle.Fill;

                    PullExpensesShow = new BankPullExpense_Report(this);
                    PullExpensesShow.Size = new Size(1109, 660);
                    PullExpensesShow.TopLevel = false;
                    PullExpensesShow.FormBorderStyle = FormBorderStyle.None;
                    PullExpensesShow.Dock = DockStyle.Fill;
                }
                panelPullExpensesReport.Controls.Clear();
                panelPullExpensesReport.Controls.Add(PullExpensesShow);
                tabPagePullExpensesReport.Controls.Add(panelPullExpensesReport);
                MainTabControlBank.TabPages.Add(tabPagePullExpensesReport);
                PullExpensesShow.Show();
                MainTabControlBank.SelectedTabPage = tabPagePullExpensesReport;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void navBarItemPullPayAccount_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            try
            {
                restForeColorOfNavBarItem();
                NavBarItem navBarItem = (NavBarItem)sender;
                navBarItem.Appearance.ForeColor = Color.FromArgb(54, 70, 151);

                XtraTabPage xtraTabPage = getTabPage(MainTabControlBank, "tabPagePullPayAccountReport");
                if (xtraTabPage == null)
                {
                    tabPagePullPayAccountReport.Name = "tabPagePullPayAccountReport";
                    tabPagePullPayAccountReport.Text = "عرض السحوبات-تحت حساب";
                    panelPullPayAccountReport.Name = "panelPullPayAccountReport";
                    panelPullPayAccountReport.Dock = DockStyle.Fill;

                    PullPayAccountShow = new BankPullPayAccount_Report();
                    PullPayAccountShow.Size = new Size(1109, 660);
                    PullPayAccountShow.TopLevel = false;
                    PullPayAccountShow.FormBorderStyle = FormBorderStyle.None;
                    PullPayAccountShow.Dock = DockStyle.Fill;
                }
                panelPullPayAccountReport.Controls.Clear();
                panelPullPayAccountReport.Controls.Add(PullPayAccountShow);
                tabPagePullPayAccountReport.Controls.Add(panelPullPayAccountReport);
                MainTabControlBank.TabPages.Add(tabPagePullPayAccountReport);
                PullPayAccountShow.Show();
                MainTabControlBank.SelectedTabPage = tabPagePullPayAccountReport;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void navBarItemTransfer_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            try
            {
                restForeColorOfNavBarItem();
                NavBarItem navBarItem = (NavBarItem)sender;
                navBarItem.Appearance.ForeColor = Color.FromArgb(54, 70, 151);

                XtraTabPage xtraTabPage = getTabPage(MainTabControlBank, "tabPageBankTransfersReport");
                if (xtraTabPage == null)
                {
                    tabPageBankTransferReport.Name = "tabPageBankTransfersReport";
                    tabPageBankTransferReport.Text = "عرض التحويلات";
                    panelBankTransferReport.Name = "panelBankTransfersReport";
                    panelBankTransferReport.Dock = DockStyle.Fill;

                    BankTransferShow = new BankTransfers_Report();
                    BankTransferShow.Size = new Size(1109, 660);
                    BankTransferShow.TopLevel = false;
                    BankTransferShow.FormBorderStyle = FormBorderStyle.None;
                    BankTransferShow.Dock = DockStyle.Fill;
                }
                panelBankTransferReport.Controls.Clear();
                panelBankTransferReport.Controls.Add(BankTransferShow);
                tabPageBankTransferReport.Controls.Add(panelBankTransferReport);
                MainTabControlBank.TabPages.Add(tabPageBankTransferReport);
                BankTransferShow.Show();
                MainTabControlBank.SelectedTabPage = tabPageBankTransferReport;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void navBarItemSupplierPullAgl_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            try
            {
                restForeColorOfNavBarItem();
                NavBarItem navBarItem = (NavBarItem)sender;
                navBarItem.Appearance.ForeColor = Color.FromArgb(54, 70, 151);

                XtraTabPage xtraTabPage = getTabPage(MainTabControlBank, "tabPageSupplierPullAglReport");
                if (xtraTabPage == null)
                {
                    tabPageSupplierPullAglReport.Name = "tabPageSupplierPullAglReport";
                    tabPageSupplierPullAglReport.Text = "عرض سحوبات الموردين-آجل";
                    panelSupplierPullAglReport.Name = "panelSupplierPullAglReport";
                    panelSupplierPullAglReport.Dock = DockStyle.Fill;

                    SupplierPullAglShow = new BankSupplierPullAgl_Report(this);
                    SupplierPullAglShow.Size = new Size(1109, 660);
                    SupplierPullAglShow.TopLevel = false;
                    SupplierPullAglShow.FormBorderStyle = FormBorderStyle.None;
                    SupplierPullAglShow.Dock = DockStyle.Fill;
                }
                panelSupplierPullAglReport.Controls.Clear();
                panelSupplierPullAglReport.Controls.Add(SupplierPullAglShow);
                tabPageSupplierPullAglReport.Controls.Add(panelSupplierPullAglReport);
                MainTabControlBank.TabPages.Add(tabPageSupplierPullAglReport);
                SupplierPullAglShow.Show();
                MainTabControlBank.SelectedTabPage = tabPageSupplierPullAglReport;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void navBarItemBankTransReport_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            try
            {
                restForeColorOfNavBarItem();
                NavBarItem navBarItem = (NavBarItem)sender;
                navBarItem.Appearance.ForeColor = Color.Blue;
                if (!MainTabControlBank.Visible)
                    MainTabControlBank.Visible = true;

                XtraTabPage xtraTabPage = getTabPage(MainTabControlBank, "حركة الخزينة");
                if (xtraTabPage == null)
                {
                    MainTabControlBank.TabPages.Add("حركة الخزينة");
                    xtraTabPage = getTabPage(MainTabControlBank, "حركة الخزينة");
                }

                xtraTabPage.Controls.Clear();
                MainTabControlBank.SelectedTabPage = xtraTabPage;

                BankTransition_Report objForm = new BankTransition_Report(this);

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

        private void navBarItemDesign_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            try
            {
                restForeColorOfNavBarItem();
                NavBarItem navBarItem = (NavBarItem)sender;
                navBarItem.Appearance.ForeColor = Color.Blue;
                if (!MainTabControlBank.Visible)
                    MainTabControlBank.Visible = true;

                XtraTabPage xtraTabPage = getTabPage(MainTabControlBank, "ايداع تصميم");
                if (xtraTabPage == null)
                {
                    MainTabControlBank.TabPages.Add("ايداع تصميم");
                    xtraTabPage = getTabPage(MainTabControlBank, "ايداع تصميم");
                }

                xtraTabPage.Controls.Clear();
                MainTabControlBank.SelectedTabPage = xtraTabPage;

                BankDepositDesign_Record2 objForm = new BankDepositDesign_Record2();

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
        private void navBarItemPullDesign_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            try
            {
                restForeColorOfNavBarItem();
                NavBarItem navBarItem = (NavBarItem)sender;
                navBarItem.Appearance.ForeColor = Color.Blue;
                if (!MainTabControlBank.Visible)
                    MainTabControlBank.Visible = true;

                XtraTabPage xtraTabPage = getTabPage(MainTabControlBank, "سحب تصميم");
                if (xtraTabPage == null)
                {
                    MainTabControlBank.TabPages.Add("سحب تصميم");
                    xtraTabPage = getTabPage(MainTabControlBank, "سحب تصميم");
                }

                xtraTabPage.Controls.Clear();
                MainTabControlBank.SelectedTabPage = xtraTabPage;

                BankPullDesign_Record objForm = new BankPullDesign_Record();

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

        private void navBarItemBillPayTypeReport_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            try
            {
                restForeColorOfNavBarItem();
                NavBarItem navBarItem = (NavBarItem)sender;
                navBarItem.Appearance.ForeColor = Color.Blue;
                if (!MainTabControlBank.Visible)
                    MainTabControlBank.Visible = true;

                XtraTabPage xtraTabPage = getTabPage(MainTabControlBank, "حركة اليومية");
                if (xtraTabPage == null)
                {
                    MainTabControlBank.TabPages.Add("حركة اليومية");
                    xtraTabPage = getTabPage(MainTabControlBank, "حركة اليومية");
                }

                xtraTabPage.Controls.Clear();
                MainTabControlBank.SelectedTabPage = xtraTabPage;

                BillPayType_Report objForm = new BillPayType_Report(this);

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
        //functions
        public void bindRecordDepositAglForm(BankDepositAgl_Report form)
        {
            BankDepositAgl_Record objForm = new BankDepositAgl_Record(form, MainTabControlBank);
            objForm.TopLevel = false;
            XtraTabPage xtraTabPage = getTabPage(MainTabControlBank, "اضافة ايداع-آجل");
            if (xtraTabPage == null)
            {
                MainTabControlBank.TabPages.Add("اضافة ايداع-آجل");
                xtraTabPage = getTabPage(MainTabControlBank, "اضافة ايداع-آجل");
                xtraTabPage.Controls.Clear();
                xtraTabPage.Controls.Add(objForm);
            }
            MainTabControlBank.SelectedTabPage = xtraTabPage;
            objForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            objForm.Dock = DockStyle.Fill;
            objForm.Show();
        }

        public void bindUpdateDepositAglForm(DataRowView sellRow, BankDepositAgl_Report form)
        {
            BankDepositAgl_Update objForm = new BankDepositAgl_Update(sellRow, form, MainTabControlBank);
            objForm.TopLevel = false;
            XtraTabPage xtraTabPage = getTabPage(MainTabControlBank, "تعديل ايداع-آجل");
            if (xtraTabPage == null)
            {
                MainTabControlBank.TabPages.Add("تعديل ايداع-آجل");
                xtraTabPage = getTabPage(MainTabControlBank, "تعديل ايداع-آجل");
            }
            xtraTabPage.Controls.Clear();
            xtraTabPage.Controls.Add(objForm);
            MainTabControlBank.SelectedTabPage = xtraTabPage;
            objForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            objForm.Dock = DockStyle.Fill;
            objForm.Show();
        }

        public void bindRecordDepositCashForm(BankDepositCash_Report form)
        {
            BankDepositCash_Record objForm = new BankDepositCash_Record(form, MainTabControlBank);
            objForm.TopLevel = false;
            XtraTabPage xtraTabPage = getTabPage(MainTabControlBank, "اضافة ايداع-كاش");
            if (xtraTabPage == null)
            {
                MainTabControlBank.TabPages.Add("اضافة ايداع-كاش");
                xtraTabPage = getTabPage(MainTabControlBank, "اضافة ايداع-كاش");
                xtraTabPage.Controls.Clear();
                xtraTabPage.Controls.Add(objForm);
            }
            MainTabControlBank.SelectedTabPage = xtraTabPage;
            objForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            objForm.Dock = DockStyle.Fill;
            objForm.Show();
        }

        public void bindUpdateDepositCashForm(DataRowView sellRow, BankDepositCash_Report form)
        {
            BankDepositCash_Update objForm = new BankDepositCash_Update(sellRow, form, MainTabControlBank);
            objForm.TopLevel = false;
            XtraTabPage xtraTabPage = getTabPage(MainTabControlBank, "تعديل ايداع-كاش");
            if (xtraTabPage == null)
            {
                MainTabControlBank.TabPages.Add("تعديل ايداع-كاش");
                xtraTabPage = getTabPage(MainTabControlBank, "تعديل ايداع-كاش");
            }
            xtraTabPage.Controls.Clear();
            xtraTabPage.Controls.Add(objForm);
            MainTabControlBank.SelectedTabPage = xtraTabPage;
            objForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            objForm.Dock = DockStyle.Fill;
            objForm.Show();
        }

        public void bindRecordPullAglForm(BankPullAgl_Report form)
        {
            BankPullAgl_Record objForm = new BankPullAgl_Record(form, MainTabControlBank);
            objForm.TopLevel = false;
            XtraTabPage xtraTabPage = getTabPage(MainTabControlBank, "اضافة مرتد-آجل");
            if (xtraTabPage == null)
            {
                MainTabControlBank.TabPages.Add("اضافة مرتد-آجل");
                xtraTabPage = getTabPage(MainTabControlBank, "اضافة مرتد-آجل");
                xtraTabPage.Controls.Clear();
                xtraTabPage.Controls.Add(objForm);
            }
            MainTabControlBank.SelectedTabPage = xtraTabPage;
            objForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            objForm.Dock = DockStyle.Fill;
            objForm.Show();
        }

        public void bindUpdatePullAglForm(DataRowView sellRow, BankPullAgl_Report form)
        {
            BankPullAgl_Update objForm = new BankPullAgl_Update(sellRow, form, MainTabControlBank);
            objForm.TopLevel = false;
            XtraTabPage xtraTabPage = getTabPage(MainTabControlBank, "تعديل مرتد-آجل");
            if (xtraTabPage == null)
            {
                MainTabControlBank.TabPages.Add("تعديل مرتد-آجل");
                xtraTabPage = getTabPage(MainTabControlBank, "تعديل مرتد-آجل");
            }
            xtraTabPage.Controls.Clear();
            xtraTabPage.Controls.Add(objForm);
            MainTabControlBank.SelectedTabPage = xtraTabPage;
            objForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            objForm.Dock = DockStyle.Fill;
            objForm.Show();
        }

        public void bindRecordSupplierPullAglForm(BankSupplierPullAgl_Report form)
        {
            BankSupplierPullAgl_Record objForm = new BankSupplierPullAgl_Record(/*form,*/ MainTabControlBank);
            objForm.TopLevel = false;
            XtraTabPage xtraTabPage = getTabPage(MainTabControlBank, "اضافة مرتد مورد-آجل");
            if (xtraTabPage == null)
            {
                MainTabControlBank.TabPages.Add("اضافة مرتد مورد-آجل");
                xtraTabPage = getTabPage(MainTabControlBank, "اضافة مرتد مورد-آجل");
                xtraTabPage.Controls.Clear();
                xtraTabPage.Controls.Add(objForm);
            }
            MainTabControlBank.SelectedTabPage = xtraTabPage;
            objForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            objForm.Dock = DockStyle.Fill;
            objForm.Show();
        }

        public void bindUpdateSupplierPullAglForm(DataRowView sellRow, BankSupplierPullAgl_Report form)
        {
            BankSupplierPullAgl_Update objForm = new BankSupplierPullAgl_Update(sellRow, form, MainTabControlBank);
            objForm.TopLevel = false;
            XtraTabPage xtraTabPage = getTabPage(MainTabControlBank, "تعديل مرتد مورد-آجل");
            if (xtraTabPage == null)
            {
                MainTabControlBank.TabPages.Add("تعديل مرتد مورد-آجل");
                xtraTabPage = getTabPage(MainTabControlBank, "تعديل مرتد مورد-آجل");
            }
            xtraTabPage.Controls.Clear();
            xtraTabPage.Controls.Add(objForm);
            MainTabControlBank.SelectedTabPage = xtraTabPage;
            objForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            objForm.Dock = DockStyle.Fill;
            objForm.Show();
        }

        public void bindRecordPullCashForm(BankPullCash_Report form)
        {
            BankPullCash_Record objForm = new BankPullCash_Record(form, MainTabControlBank);
            objForm.TopLevel = false;
            XtraTabPage xtraTabPage = getTabPage(MainTabControlBank, "اضافة مرتد-كاش");
            if (xtraTabPage == null)
            {
                MainTabControlBank.TabPages.Add("اضافة مرتد-كاش");
                xtraTabPage = getTabPage(MainTabControlBank, "اضافة مرتد-كاش");
                xtraTabPage.Controls.Clear();
                xtraTabPage.Controls.Add(objForm);
            }
            MainTabControlBank.SelectedTabPage = xtraTabPage;
            objForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            objForm.Dock = DockStyle.Fill;
            objForm.Show();
        }

        public void bindUpdatePullCashForm(DataRowView sellRow, BankPullCash_Report form)
        {
            BankPullCash_Update objForm = new BankPullCash_Update(sellRow, form, MainTabControlBank);
            objForm.TopLevel = false;
            XtraTabPage xtraTabPage = getTabPage(MainTabControlBank, "تعديل مرتد-كاش");
            if (xtraTabPage == null)
            {
                MainTabControlBank.TabPages.Add("تعديل مرتد-كاش");
                xtraTabPage = getTabPage(MainTabControlBank, "تعديل مرتد-كاش");
            }
            xtraTabPage.Controls.Clear();
            xtraTabPage.Controls.Add(objForm);
            MainTabControlBank.SelectedTabPage = xtraTabPage;
            objForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            objForm.Dock = DockStyle.Fill;
            objForm.Show();
        }
        
        public void bindRecordDepositIncomeForm(BankDepositIncome_Report form)
        {
            BankDepositIncome_Record objForm = new BankDepositIncome_Record(form, MainTabControlBank);
            objForm.TopLevel = false;
            XtraTabPage xtraTabPage = getTabPage(MainTabControlBank, "اضافة ايداع-ايراد");
            if (xtraTabPage == null)
            {
                MainTabControlBank.TabPages.Add("اضافة ايداع-ايراد");
                xtraTabPage = getTabPage(MainTabControlBank, "اضافة ايداع-ايراد");
                xtraTabPage.Controls.Clear();
                xtraTabPage.Controls.Add(objForm);
            }
            MainTabControlBank.SelectedTabPage = xtraTabPage;
            objForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            objForm.Dock = DockStyle.Fill;
            objForm.Show();
        }

        public void bindUpdateDepositIncomeForm(DataRowView sellRow, BankDepositIncome_Report form)
        {
            BankDepositIncome_Update objForm = new BankDepositIncome_Update(sellRow, form, MainTabControlBank);
            objForm.TopLevel = false;
            XtraTabPage xtraTabPage = getTabPage(MainTabControlBank, "تعديل ايداع-ايراد");
            if (xtraTabPage == null)
            {
                MainTabControlBank.TabPages.Add("تعديل ايداع-ايراد");
                xtraTabPage = getTabPage(MainTabControlBank, "تعديل ايداع-ايراد");
            }
            xtraTabPage.Controls.Clear();
            xtraTabPage.Controls.Add(objForm);
            MainTabControlBank.SelectedTabPage = xtraTabPage;
            objForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            objForm.Dock = DockStyle.Fill;
            objForm.Show();
        }

        public void bindRecordPullExpenseForm(BankPullExpense_Report form)
        {
            BankPullExpense_Record objForm = new BankPullExpense_Record(form, MainTabControlBank);
            objForm.TopLevel = false;
            XtraTabPage xtraTabPage = getTabPage(MainTabControlBank, "اضافة مرتد-مصروف");
            if (xtraTabPage == null)
            {
                MainTabControlBank.TabPages.Add("اضافة مرتد-مصروف");
                xtraTabPage = getTabPage(MainTabControlBank, "اضافة مرتد-مصروف");
                xtraTabPage.Controls.Clear();
                xtraTabPage.Controls.Add(objForm);
            }
            MainTabControlBank.SelectedTabPage = xtraTabPage;
            objForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            objForm.Dock = DockStyle.Fill;
            objForm.Show();
        }

        public void bindUpdatePullExpenseForm(DataRowView sellRow, BankPullExpense_Report form)
        {
            BankPullExpense_Update objForm = new BankPullExpense_Update(sellRow, form, MainTabControlBank);
            objForm.TopLevel = false;
            XtraTabPage xtraTabPage = getTabPage(MainTabControlBank, "تعديل مرتد-مصروف");
            if (xtraTabPage == null)
            {
                MainTabControlBank.TabPages.Add("تعديل مرتد-مصروف");
                xtraTabPage = getTabPage(MainTabControlBank, "تعديل مرتد-مصروف");
            }
            xtraTabPage.Controls.Clear();
            xtraTabPage.Controls.Add(objForm);
            MainTabControlBank.SelectedTabPage = xtraTabPage;
            objForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            objForm.Dock = DockStyle.Fill;
            objForm.Show();
        }
    }
}

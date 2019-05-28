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
using System.IO;
using System.Reflection;

namespace MainSystem
{
    class POSSystem
    {
    }

    public partial class MainForm
    {
        public static XtraTabControl tabControlPointSale;

        public static bool loadedPrintCustomer = false;


        public static Customer_Report2 CustomerReport;
        public static Products_ReportCopy ProductsReport;
        public static ProductsDetails_Report ProductsDetailsReport;
        public static Bill_Confirm BillConfirm;
        public static Information_Products InformationProducts;
        public static Information_Sets InformationSets;
        public static Information_Offers InformationOffers;
        public static InformationProducts_Report InformationProductsReport;
        public static DelegateBill_Report DelegateBillReport;


        public static XtraTabPage MainTabPageAddCustomer2;
        public static XtraTabPage MainTabPageUpdateCustomer2;
        public static XtraTabPage MainTabPagePrintCustomer2;

        XtraTabPage tabPageCustomerReport;
        Panel panelCustomerReport;
        public static XtraTabPage tabPageProductsReport;
        public static Panel panelProductsReport;
        XtraTabPage tabPageBillConfirm;
        Panel panelBillConfirm;
        public static XtraTabPage tabPageProductsDetailsReport;
        public static Panel panelProductsDetailsReport;
        XtraTabPage tabPageInformationProducts;
        Panel panelInformationProducts;
        XtraTabPage tabPageInformationSets;
        Panel panelInformationSets;
        XtraTabPage tabPageInformationOffers;
        Panel panelInformationOffers;
        XtraTabPage tabPageInformationProductsReport;
        Panel panelInformationProductsReport;
        XtraTabPage tabPageDelegateBillReport;
        Panel panelDelegateBillReport;

        //public static int delegateID = -1;
        public static int billNum = 0;
        bool flag = false;

        public void  POSSystem()
        {
            conn = new MySqlConnection(connection.connectionString);

            tabPageCustomerReport = new XtraTabPage();
            panelCustomerReport = new Panel();
            tabPageProductsReport = new XtraTabPage();
            panelProductsReport = new Panel();
            tabPageProductsDetailsReport = new XtraTabPage();
            panelProductsDetailsReport = new Panel();
            tabPageBillConfirm = new XtraTabPage();
            panelBillConfirm = new Panel();
            tabPageInformationProducts = new XtraTabPage();
            panelInformationProducts = new Panel();
            tabPageInformationSets = new XtraTabPage();
            panelInformationSets = new Panel();
            tabPageInformationOffers = new XtraTabPage();
            panelInformationOffers = new Panel();
            tabPageInformationProductsReport = new XtraTabPage();
            panelInformationProductsReport = new Panel();
            tabPageDelegateBillReport = new XtraTabPage();
            panelDelegateBillReport = new Panel();

            MainTabPageAddCustomer2 = new XtraTabPage();
            MainTabPageUpdateCustomer2 = new XtraTabPage();
            MainTabPagePrintCustomer2 = new XtraTabPage();

            tabControlPointSale = xtraTabControlPointSale;
        }

       
        private void xtraTabControlPointSale_SelectedPageChanged(object sender, TabPageChangedEventArgs e)
        {
            try
            {
                if (xtraTabControlPointSale.SelectedTabPage == tabPageCustomerReport)
                {
                    CustomerReport.search();
                }
                //else if (xtraTabControlPointSale.SelectedTabPage == Customer_Report.MainTabPagePrintCustomer)
                //{
                //    if (loadedPrintCustomer)
                //    {
                //        Customer_Report.customerPrint.display();
                //    }
                //}
                else if (xtraTabControlPointSale.SelectedTabPage == tabPageProductsDetailsReport)
                {
                    panelProductsDetailsReport.Dock = DockStyle.Fill;

                    ProductsDetailsReport = new ProductsDetails_Report(this/*, delegateID*/, billNum);
                    ProductsDetailsReport.TopLevel = false;
                    ProductsDetailsReport.FormBorderStyle = FormBorderStyle.None;
                    ProductsDetailsReport.Dock = DockStyle.Fill;

                    panelProductsDetailsReport.Controls.Clear();
                    panelProductsDetailsReport.Controls.Add(ProductsDetailsReport);
                    tabPageProductsDetailsReport.Controls.Add(panelProductsDetailsReport);
                    ProductsDetailsReport.Show();
                    xtraTabControlPointSale.SelectedTabPage = tabPageProductsDetailsReport;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        
        private void navBarItemCustomerShow_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            try
            {
                restForeColorOfNavBarItem();
                NavBarItem navBarItem = (NavBarItem)sender;
                navBarItem.Appearance.ForeColor = Color.Blue;

                if (!xtraTabControlPointSale.Visible)
                    xtraTabControlPointSale.Visible = true;

                XtraTabPage xtraTabPage = getTabPage(xtraTabControlPointSale, "العملاء");
                if (xtraTabPage == null)
                {
                    xtraTabControlPointSale.TabPages.Add("العملاء");
                    xtraTabPage = getTabPage(xtraTabControlPointSale, "العملاء");
                    bindDisplayCustomersPSForm(xtraTabPage);
                }
                //xtraTabPage.Controls.Clear();

                xtraTabControlPointSale.SelectedTabPage = xtraTabPage;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void navBarItemProductsReport_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            try
            {
                restForeColorOfNavBarItem();
                NavBarItem navBarItem = (NavBarItem)sender;
                navBarItem.Appearance.ForeColor = Color.FromArgb(54, 70, 151);

                XtraTabPage xtraTabPage =getTabPage(xtraTabControlPointSale,"tabPageProductsReport");
                if (xtraTabPage == null)
                {
                    tabPageProductsReport.Name = "tabPageProductsReport";
                    tabPageProductsReport.Text = "تسجيل فاتورة";
                    panelProductsReport.Name = "panelProductsReport";
                    panelProductsReport.Dock = DockStyle.Fill;

                    ProductsReport = new Products_ReportCopy(this, null, "");
                    ProductsReport.Size = new Size(1109, 660);
                    ProductsReport.TopLevel = false;
                    ProductsReport.FormBorderStyle = FormBorderStyle.None;
                    ProductsReport.Dock = DockStyle.Fill;

                    panelProductsReport.Controls.Clear();
                    panelProductsReport.Controls.Add(ProductsReport);
                    tabPageProductsReport.Controls.Add(panelProductsReport);
                    xtraTabControlPointSale.TabPages.Add(tabPageProductsReport);
                    ProductsReport.Show();
                    xtraTabControlPointSale.SelectedTabPage = tabPageProductsReport;
                }
                else
                {
                    xtraTabControlPointSale.SelectedTabPage = tabPageProductsReport;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


   
         //review
         public void test(/*int DelegateId,*/ int BillNum)
        {
            int count = 0;
            BaseData.generateBaseProjectFile();
            int DelegateBranchId = Convert.ToInt16(BaseData.BranchID);

            string query = "SELECT dash_details.Data_ID FROM dash_details INNER JOIN dash ON dash.Dash_ID = dash_details.Dash_ID where dash.Bill_Number=" + BillNum + " and dash.Branch_ID=" + DelegateBranchId + " and dash.Confirmed=0";
            MySqlCommand com = new MySqlCommand(query, conn);
            conn.Open();
            MySqlDataReader dr = com.ExecuteReader();
            while (dr.Read())
            {
                count++;
            }
            labelBaskt.Text = (count).ToString();
            if (Convert.ToInt16(labelBaskt.Text) > 0)
            {
                labelBaskt.Visible = true;
                //delegateID = DelegateId;
                //billNum = BillNum;
            }
            else
            {
                labelBaskt.Visible = false;
                //delegateID = 0;
                //billNum = 0;
            }
            //delegateID = DelegateId;
            billNum = BillNum;
            dr.Close();
            conn.Close();
        }
        
         private void pictureBoxBell_Click(object sender, EventArgs e)
        {
            try
            {
                // || UserControl.userType == 1
                if (UserControl.userType == 5)
                {
                    if (!xtraTabControlMainContainer.TabPages.Contains(xtraTabPagePOS))
                    {
                        if (index == 0)
                        {
                            xtraTabControlMainContainer.TabPages.Insert(1, POSTP);
                        }
                        else
                        {
                            xtraTabControlMainContainer.TabPages.Insert(index, POSTP);
                        }
                        index++;
                    }
                    
                    XtraTabPage xtraTabPage = getTabPage(xtraTabControlPointSale, "tabPageProductsDetailsReport");
                    //if (xtraTabPage == null)
                    //{
                    tabPageProductsDetailsReport.Name = "tabPageProductsDetailsReport";
                    tabPageProductsDetailsReport.Text = "تفاصيل فاتورة";
                    panelProductsDetailsReport.Name = "panelProductsDetailsReport";
                    panelProductsDetailsReport.Dock = DockStyle.Fill;

                    ProductsDetailsReport = new ProductsDetails_Report(this/*, delegateID*/, billNum);
                    ProductsDetailsReport.Size = new Size(1109, 660);
                    ProductsDetailsReport.TopLevel = false;
                    ProductsDetailsReport.FormBorderStyle = FormBorderStyle.None;
                    ProductsDetailsReport.Dock = DockStyle.Fill;
                    //}
                    panelProductsDetailsReport.Controls.Clear();
                    panelProductsDetailsReport.Controls.Add(ProductsDetailsReport);
                    tabPageProductsDetailsReport.Controls.Add(panelProductsDetailsReport);
                    xtraTabControlPointSale.TabPages.Add(tabPageProductsDetailsReport);
                    ProductsDetailsReport.Show();
                    xtraTabControlPointSale.SelectedTabPage = tabPageProductsDetailsReport;
                    //}
                    //else
                    //{
                    //    xtraTabControlPointSale.SelectedTabPage = tabPageProductsDetailsReport;
                    //}
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void navBarItemInformationProducts_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            try
            {
                restForeColorOfNavBarItem();
                NavBarItem navBarItem = (NavBarItem)sender;
                navBarItem.Appearance.ForeColor = Color.FromArgb(54, 70, 151);

                XtraTabPage xtraTabPage = getTabPage(xtraTabControlPointSale, "tabPageInformationProducts");
                if (xtraTabPage == null)
                {
                    tabPageInformationProducts.Name = "tabPageInformationProducts";
                    tabPageInformationProducts.Text = "كارت الاصناف";
                    panelInformationProducts.Name = "panelInformationProducts";
                    panelInformationProducts.Dock = DockStyle.Fill;

                    InformationProducts = new Information_Products(this);
                    InformationProducts.Size = new Size(1109, 660);
                    InformationProducts.TopLevel = false;
                    InformationProducts.FormBorderStyle = FormBorderStyle.None;
                    InformationProducts.Dock = DockStyle.Fill;
                }
                panelInformationProducts.Controls.Clear();
                panelInformationProducts.Controls.Add(InformationProducts);
                tabPageInformationProducts.Controls.Add(panelInformationProducts);
                xtraTabControlPointSale.TabPages.Add(tabPageInformationProducts);
                InformationProducts.Show();
                xtraTabControlPointSale.SelectedTabPage = tabPageInformationProducts;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void navBarItemInformationProductsReport_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            try
            {
                restForeColorOfNavBarItem();
                NavBarItem navBarItem = (NavBarItem)sender;
                navBarItem.Appearance.ForeColor = Color.FromArgb(54, 70, 151);

                XtraTabPage xtraTabPage = getTabPage(xtraTabControlPointSale, "tabPageInformationProductsReport");
                if (xtraTabPage == null)
                {
                    tabPageInformationProductsReport.Name = "tabPageInformationProductsReport";
                    tabPageInformationProductsReport.Text = "تقرير الاصناف بسعر البيع";
                    panelInformationProductsReport.Name = "panelInformationProductsReport";
                    panelInformationProductsReport.Dock = DockStyle.Fill;

                    InformationProductsReport = new InformationProducts_Report(this);
                    InformationProductsReport.Size = new Size(1109, 660);
                    InformationProductsReport.TopLevel = false;
                    InformationProductsReport.FormBorderStyle = FormBorderStyle.None;
                    InformationProductsReport.Dock = DockStyle.Fill;
                }
                panelInformationProductsReport.Controls.Clear();
                panelInformationProductsReport.Controls.Add(InformationProductsReport);
                tabPageInformationProductsReport.Controls.Add(panelInformationProductsReport);
                xtraTabControlPointSale.TabPages.Add(tabPageInformationProductsReport);
                InformationProductsReport.Show();
                xtraTabControlPointSale.SelectedTabPage = tabPageInformationProductsReport;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void navBarItemInformationSets_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            try
            {
                restForeColorOfNavBarItem();
                NavBarItem navBarItem = (NavBarItem)sender;
                navBarItem.Appearance.ForeColor = Color.FromArgb(54, 70, 151);

                XtraTabPage xtraTabPage =getTabPage(xtraTabControlPointSale,"tabPageInformationSets");
                if (xtraTabPage == null)
                {
                    tabPageInformationSets.Name = "tabPageInformationSets";
                    tabPageInformationSets.Text = "كارت الاطقم";
                    panelInformationSets.Name = "panelInformationSets";
                    panelInformationSets.Dock = DockStyle.Fill;

                    InformationSets = new Information_Sets(this);
                    InformationSets.Size = new Size(1109, 660);
                    InformationSets.TopLevel = false;
                    InformationSets.FormBorderStyle = FormBorderStyle.None;
                    InformationSets.Dock = DockStyle.Fill;
                }
                panelInformationSets.Controls.Clear();
                panelInformationSets.Controls.Add(InformationSets);
                tabPageInformationSets.Controls.Add(panelInformationSets);
                xtraTabControlPointSale.TabPages.Add(tabPageInformationSets);
                InformationSets.Show();
                xtraTabControlPointSale.SelectedTabPage = tabPageInformationSets;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void navBarItemInformationOffers_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            try
            {
                restForeColorOfNavBarItem();
                NavBarItem navBarItem = (NavBarItem)sender;
                navBarItem.Appearance.ForeColor = Color.FromArgb(54, 70, 151);

                XtraTabPage xtraTabPage =getTabPage(xtraTabControlPointSale,"tabPageInformationOffers");
                if (xtraTabPage == null)
                {
                    tabPageInformationOffers.Name = "tabPageInformationOffers";
                    tabPageInformationOffers.Text = "كارت العروض";
                    panelInformationOffers.Name = "panelInformationOffers";
                    panelInformationOffers.Dock = DockStyle.Fill;

                    InformationOffers = new Information_Offers(this);
                    InformationOffers.Size = new Size(1109, 660);
                    InformationOffers.TopLevel = false;
                    InformationOffers.FormBorderStyle = FormBorderStyle.None;
                    InformationOffers.Dock = DockStyle.Fill;
                }
                panelInformationOffers.Controls.Clear();
                panelInformationOffers.Controls.Add(InformationOffers);
                tabPageInformationOffers.Controls.Add(panelInformationOffers);
                xtraTabControlPointSale.TabPages.Add(tabPageInformationOffers);
                InformationOffers.Show();
                xtraTabControlPointSale.SelectedTabPage = tabPageInformationOffers;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void navBarItemDelegateBill_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            try
            {
                restForeColorOfNavBarItem();
                NavBarItem navBarItem = (NavBarItem)sender;
                navBarItem.Appearance.ForeColor = Color.FromArgb(54, 70, 151);

                XtraTabPage xtraTabPage = getTabPage(xtraTabControlPointSale, "tabPageDelegateBillReport");
                if (xtraTabPage == null)
                {
                    tabPageDelegateBillReport.Name = "tabPageDelegateBillReport";
                    tabPageDelegateBillReport.Text = "تقرير فواتير المناديب";
                    panelDelegateBillReport.Name = "panelDelegateBillReport";
                    panelDelegateBillReport.Dock = DockStyle.Fill;

                    DelegateBillReport = new DelegateBill_Report();
                    DelegateBillReport.Size = new Size(1109, 660);
                    DelegateBillReport.TopLevel = false;
                    DelegateBillReport.FormBorderStyle = FormBorderStyle.None;
                    DelegateBillReport.Dock = DockStyle.Fill;
                }
                panelDelegateBillReport.Controls.Clear();
                panelDelegateBillReport.Controls.Add(DelegateBillReport);
                tabPageDelegateBillReport.Controls.Add(panelDelegateBillReport);
                xtraTabControlPointSale.TabPages.Add(tabPageDelegateBillReport);
                DelegateBillReport.Show();
                xtraTabControlPointSale.SelectedTabPage = tabPageDelegateBillReport;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Main_Resize(object sender, EventArgs e)
        {
            try
            {
                //if (this.WindowState == FormWindowState.Minimized || this.WindowState == FormWindowState.Maximized)
                //{
                if (Products_Report.tipImage != null)
                {
                    Products_Report.tipImage.Close();
                    Products_Report.tipImage = null;
                }
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Main_LocationChanged(object sender, EventArgs e)
        {
            try
            {
                if (Products_Report.tipImage != null)
                {
                    Products_Report.tipImage.Close();
                    Products_Report.tipImage = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void bindDisplayCustomersPSForm(XtraTabPage xtraTabPage)
        {
            CustomerReport = new Customer_Report2(xtraTabControlPointSale);
            CustomerReport.TopLevel = false;

            xtraTabPage.Controls.Add(CustomerReport);
            CustomerReport.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            CustomerReport.Dock = DockStyle.Fill;
            CustomerReport.Show();
        }
    }
}

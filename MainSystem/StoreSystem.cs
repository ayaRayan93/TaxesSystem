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
    class StoreSystem
    {
    }
    public partial class MainForm 
    {
        Timer Storetimer = new Timer();
        bool StoreFlag = false;
        public static SearchRecive_Date searchReciveDate;

        public void StoreMainForm()
        {
            try
            {
                ExpectedOrdersFunction();
                
                Storetimer.Interval = 1000 * 60;
                Storetimer.Tick += new EventHandler(GetExpectedOrders);
                Storetimer.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //stores
        private void pictureBoxStoreExpectedOrder_Click(object sender, EventArgs e)
        {
            try
            {
                if (UserControl.userType == 2 || UserControl.userType == 1)
                {
                    if (StoreFlag == false)
                    {
                        xtraTabControlMainContainer.TabPages.Insert(1, StoreTP);
                        index++;
                        StoreFlag = true;
                    }
                    xtraTabControlMainContainer.SelectedTabPage = StoreTP;

                    if (!xtraTabControlStoresContent.Visible)
                        xtraTabControlStoresContent.Visible = true;

                    XtraTabPage xtraTabPage = getTabPage(xtraTabControlStoresContent, "الطلبات المتوقع استلامها");
                    if (xtraTabPage == null)
                    {
                        xtraTabControlStoresContent.TabPages.Add("الطلبات المتوقع استلامها");
                        xtraTabPage = getTabPage(xtraTabControlStoresContent, "الطلبات المتوقع استلامها");
                    }
                    xtraTabPage.Controls.Clear();

                    xtraTabControlStoresContent.SelectedTabPage = xtraTabPage;
                    bindDisplayExpectedOrdersReport(xtraTabPage);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnStoreRecord_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            try
            {
                restForeColorOfNavBarItem();
                NavBarItem navBarItem = (NavBarItem)sender;
                navBarItem.Appearance.ForeColor = Color.Blue;

                if (!xtraTabControlStoresContent.Visible)
                    xtraTabControlStoresContent.Visible = true;

                XtraTabPage xtraTabPage = getTabPage(xtraTabControlStoresContent,"عرض المخازن");
                if (xtraTabPage == null)
                {
                    xtraTabControlStoresContent.TabPages.Add("عرض المخازن");
                    xtraTabPage = getTabPage(xtraTabControlStoresContent,"عرض المخازن");
                }
                xtraTabPage.Controls.Clear();

                xtraTabControlStoresContent.SelectedTabPage = xtraTabPage;
                bindDisplayStoresForm(xtraTabPage);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        //ProductItems
        private void btnProductItems_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            try
            {
                restForeColorOfNavBarItem();
                NavBarItem navBarItem = (NavBarItem)sender;
                navBarItem.Appearance.ForeColor = Color.Blue;

                if (!xtraTabControlStoresContent.Visible)
                    xtraTabControlStoresContent.Visible = true;


                XtraTabPage xtraTabPage = getTabPage(xtraTabControlStoresContent,"عناصر البند");
                if (xtraTabPage == null)
                {
                    xtraTabControlStoresContent.TabPages.Add("عناصر البند");
                    xtraTabPage = getTabPage(xtraTabControlStoresContent,"عناصر البند");
                }
                xtraTabPage.Controls.Clear();
                xtraTabControlStoresContent.SelectedTabPage = xtraTabPage;
                ProductItems objForm = new ProductItems();

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
        //Products
        private void btnProducts_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            try
            {
                restForeColorOfNavBarItem();
                NavBarItem navBarItem = (NavBarItem)sender;
                navBarItem.Appearance.ForeColor = Color.Blue;

                if (!xtraTabControlStoresContent.Visible)
                    xtraTabControlStoresContent.Visible = true;



                XtraTabPage xtraTabPage = getTabPage(xtraTabControlStoresContent,"عرض البنود");
                if (xtraTabPage == null)
                {
                    xtraTabControlStoresContent.TabPages.Add("عرض البنود");
                    xtraTabPage = getTabPage(xtraTabControlStoresContent,"عرض البنود");
                }
                xtraTabPage.Controls.Clear();
                xtraTabControlStoresContent.SelectedTabPage = xtraTabPage;

                Products objForm = new Products(this);

                objForm.TopLevel = false;
                xtraTabPage.Controls.Add(objForm);
                objForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                objForm.Dock = DockStyle.Fill;
                objForm.displayProducts();
                objForm.Show();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #region Ataqm
        //Ataqm CURD
        private void btnAtaqm_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            try
            {
                restForeColorOfNavBarItem();
                NavBarItem navBarItem = (NavBarItem)sender;
                navBarItem.Appearance.ForeColor = Color.Blue;
                if (!xtraTabControlStoresContent.Visible)
                    xtraTabControlStoresContent.Visible = true;

                XtraTabPage xtraTabPage = getTabPage(xtraTabControlStoresContent,"عرض الأطقم");
                if (xtraTabPage == null)
                {
                    xtraTabControlStoresContent.TabPages.Add("عرض الأطقم");
                    xtraTabPage = getTabPage(xtraTabControlStoresContent,"عرض الأطقم");
                }

                xtraTabPage.Controls.Clear();
                xtraTabControlStoresContent.SelectedTabPage = xtraTabPage;

                Ataqm objForm = new Ataqm(this);

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
        //Ataqm Tagame3
        private void navBarItemAtaqmTagame3_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            try
            {
                restForeColorOfNavBarItem();
                NavBarItem navBarItem = (NavBarItem)sender;
                navBarItem.Appearance.ForeColor = Color.Blue;
                if (!xtraTabControlStoresContent.Visible)
                    xtraTabControlStoresContent.Visible = true;

                XtraTabPage xtraTabPage = getTabPage(xtraTabControlStoresContent,"تسجيل كميات الاطقم");
                if (xtraTabPage == null)
                {
                    xtraTabControlStoresContent.TabPages.Add("تسجيل كميات الاطقم");
                    xtraTabPage = getTabPage(xtraTabControlStoresContent,"تسجيل كميات الاطقم");
                }

                xtraTabPage.Controls.Clear();
                xtraTabControlStoresContent.SelectedTabPage = xtraTabPage;

                AtaqmStorage objForm = new AtaqmStorage(this);

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
        #endregion

        //storage
        private void navBarItemstorage_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            try
            {
                restForeColorOfNavBarItem();
                NavBarItem navBarItem = (NavBarItem)sender;
                navBarItem.Appearance.ForeColor = Color.Blue;
                if (!xtraTabControlStoresContent.Visible)
                    xtraTabControlStoresContent.Visible = true;

                XtraTabPage xtraTabPage = getTabPage(xtraTabControlStoresContent, "تسجيل كميات البنود");
                if (xtraTabPage == null)
                {
                    xtraTabControlStoresContent.TabPages.Add("تسجيل كميات البنود");
                    xtraTabPage = getTabPage(xtraTabControlStoresContent, "تسجيل كميات البنود");
                }

                xtraTabPage.Controls.Clear();
                xtraTabControlStoresContent.SelectedTabPage = xtraTabPage;

                initialCodeStorage objForm = new initialCodeStorage(this);

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

        private void navBarItemSignInRecord_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            try
            {
                restForeColorOfNavBarItem();
                NavBarItem navBarItem = (NavBarItem)sender;
                navBarItem.Appearance.ForeColor = Color.Blue;
                if (!xtraTabControlStoresContent.Visible)
                    xtraTabControlStoresContent.Visible = true;

                XtraTabPage xtraTabPage = getTabPage(xtraTabControlStoresContent, "تسجيل دخول السيارات");
                if (xtraTabPage == null)
                {
                    xtraTabControlStoresContent.TabPages.Add("تسجيل دخول السيارات");
                    xtraTabPage = getTabPage(xtraTabControlStoresContent, "تسجيل دخول السيارات");
                }

                xtraTabPage.Controls.Clear();
                xtraTabControlStoresContent.SelectedTabPage = xtraTabPage;

                Gate_Enter objForm = new Gate_Enter(this, xtraTabControlStoresContent);

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

        private void navBarItemSignOutRecord_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            try
            {
                restForeColorOfNavBarItem();
                NavBarItem navBarItem = (NavBarItem)sender;
                navBarItem.Appearance.ForeColor = Color.Blue;
                if (!xtraTabControlStoresContent.Visible)
                    xtraTabControlStoresContent.Visible = true;

                XtraTabPage xtraTabPage = getTabPage(xtraTabControlStoresContent, "تسجيل خروج السيارات");
                if (xtraTabPage == null)
                {
                    xtraTabControlStoresContent.TabPages.Add("تسجيل خروج السيارات");
                    xtraTabPage = getTabPage(xtraTabControlStoresContent, "تسجيل خروج السيارات");
                }

                xtraTabPage.Controls.Clear();
                xtraTabControlStoresContent.SelectedTabPage = xtraTabPage;

                Gate_Out objForm = new Gate_Out(this, xtraTabControlStoresContent);

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

        private void navBarItemGateReport_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            try
            {
                restForeColorOfNavBarItem();
                NavBarItem navBarItem = (NavBarItem)sender;
                navBarItem.Appearance.ForeColor = Color.Blue;
                if (!xtraTabControlStoresContent.Visible)
                    xtraTabControlStoresContent.Visible = true;

                XtraTabPage xtraTabPage = getTabPage(xtraTabControlStoresContent, "تقرير حركة البوابة");
                if (xtraTabPage == null)
                {
                    xtraTabControlStoresContent.TabPages.Add("تقرير حركة البوابة");
                    xtraTabPage = getTabPage(xtraTabControlStoresContent, "تقرير حركة البوابة");
                }

                xtraTabPage.Controls.Clear();
                xtraTabControlStoresContent.SelectedTabPage = xtraTabPage;

                Gate_Report objForm = new Gate_Report(this, xtraTabControlStoresContent);

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

        private void navBarItemImportedPermission_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            try
            {
                restForeColorOfNavBarItem();
                NavBarItem navBarItem = (NavBarItem)sender;
                navBarItem.Appearance.ForeColor = Color.Blue;
                if (!xtraTabControlStoresContent.Visible)
                    xtraTabControlStoresContent.Visible = true;

                XtraTabPage xtraTabPage = getTabPage(xtraTabControlStoresContent, "تسجيل وارد باذن");
                if (xtraTabPage == null)
                {
                    xtraTabControlStoresContent.TabPages.Add("تسجيل وارد باذن");
                    xtraTabPage = getTabPage(xtraTabControlStoresContent, "تسجيل وارد باذن");
                }

                xtraTabPage.Controls.Clear();
                xtraTabControlStoresContent.SelectedTabPage = xtraTabPage;

                SupplierReceipt objForm = new SupplierReceipt(this, xtraTabControlStoresContent);

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
        
        private void navBarItemStoreReturn_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            try
            {
                restForeColorOfNavBarItem();
                NavBarItem navBarItem = (NavBarItem)sender;
                navBarItem.Appearance.ForeColor = Color.Blue;
                if (!xtraTabControlStoresContent.Visible)
                    xtraTabControlStoresContent.Visible = true;

                XtraTabPage xtraTabPage = getTabPage(xtraTabControlStoresContent, "مرتجع وارد");
                if (xtraTabPage == null)
                {
                    xtraTabControlStoresContent.TabPages.Add("مرتجع وارد");
                    xtraTabPage = getTabPage(xtraTabControlStoresContent, "مرتجع وارد");
                }

                xtraTabPage.Controls.Clear();
                xtraTabControlStoresContent.SelectedTabPage = xtraTabPage;

                StorageReturnBill objForm = new StorageReturnBill(this, xtraTabControlStoresContent);

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

        private void navBarItemCustomerReturn_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            try
            {
                restForeColorOfNavBarItem();
                NavBarItem navBarItem = (NavBarItem)sender;
                navBarItem.Appearance.ForeColor = Color.Blue;
                if (!xtraTabControlStoresContent.Visible)
                    xtraTabControlStoresContent.Visible = true;

                XtraTabPage xtraTabPage = getTabPage(xtraTabControlStoresContent, "مرتجع عميل");
                if (xtraTabPage == null)
                {
                    xtraTabControlStoresContent.TabPages.Add("مرتجع عميل");
                    xtraTabPage = getTabPage(xtraTabControlStoresContent, "مرتجع عميل");
                }

                xtraTabPage.Controls.Clear();
                xtraTabControlStoresContent.SelectedTabPage = xtraTabPage;

                StoreReturnBill objForm = new StoreReturnBill();

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

        private void navBarItemCustomerReturnItemsReport_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            try
            { 
                restForeColorOfNavBarItem();
                NavBarItem navBarItem = (NavBarItem)sender;
                navBarItem.Appearance.ForeColor = Color.Blue;
                if (!xtraTabControlStoresContent.Visible)
                    xtraTabControlStoresContent.Visible = true;

                XtraTabPage xtraTabPage = getTabPage(xtraTabControlStoresContent, "تقرير مرتجعات العملاء لفترة");
                if (xtraTabPage == null)
                {
                    xtraTabControlStoresContent.TabPages.Add("تقرير مرتجعات العملاء لفترة");
                    xtraTabPage = getTabPage(xtraTabControlStoresContent, "تقرير مرتجعات العملاء لفترة");
                }

                xtraTabPage.Controls.Clear();
                xtraTabControlStoresContent.SelectedTabPage = xtraTabPage;

                CustomerReturnReportToPeriod objForm = new CustomerReturnReportToPeriod();

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

        private void navBarItemDelivery_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            try
            {
                restForeColorOfNavBarItem();
                NavBarItem navBarItem = (NavBarItem)sender;
                navBarItem.Appearance.ForeColor = Color.Blue;
                if (!xtraTabControlStoresContent.Visible)
                    xtraTabControlStoresContent.Visible = true;

                XtraTabPage xtraTabPage = getTabPage(xtraTabControlStoresContent, "تسليم طلب");
                if (xtraTabPage == null)
                {
                    xtraTabControlStoresContent.TabPages.Add("تسليم طلب");
                    xtraTabPage = getTabPage(xtraTabControlStoresContent, "تسليم طلب");
                }

                xtraTabPage.Controls.Clear();
                xtraTabControlStoresContent.SelectedTabPage = xtraTabPage;

                CustomerDelivery objForm = new CustomerDelivery();

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

        private void navBarItemPermissionDelivery_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            try
            {
                restForeColorOfNavBarItem();
                NavBarItem navBarItem = (NavBarItem)sender;
                navBarItem.Appearance.ForeColor = Color.Blue;
                if (!xtraTabControlStoresContent.Visible)
                    xtraTabControlStoresContent.Visible = true;

                XtraTabPage xtraTabPage = getTabPage(xtraTabControlStoresContent, "الاذونات المتوقع تسليمها");
                if (xtraTabPage == null)
                {
                    xtraTabControlStoresContent.TabPages.Add("الاذونات المتوقع تسليمها");
                    xtraTabPage = getTabPage(xtraTabControlStoresContent, "الاذونات المتوقع تسليمها");
                }

                xtraTabPage.Controls.Clear();
                xtraTabControlStoresContent.SelectedTabPage = xtraTabPage;

                PermissionsDelivery objForm = new PermissionsDelivery(this);

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

        private void navBarItemAddingQuantity_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            try
            {
                restForeColorOfNavBarItem();
                NavBarItem navBarItem = (NavBarItem)sender;
                navBarItem.Appearance.ForeColor = Color.Blue;
                if (!xtraTabControlStoresContent.Visible)
                    xtraTabControlStoresContent.Visible = true;

                bindTaswayAddingStorageForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
     
        private void navBarItemSubstractQuantity_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            try
            {
                restForeColorOfNavBarItem();
                NavBarItem navBarItem = (NavBarItem)sender;
                navBarItem.Appearance.ForeColor = Color.Blue;
                if (!xtraTabControlStoresContent.Visible)
                    xtraTabControlStoresContent.Visible = true;

                bindTaswaySubtractStorageForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void navBarItemSupplierPermission_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            try
            {
                restForeColorOfNavBarItem();
                NavBarItem navBarItem = (NavBarItem)sender;
                navBarItem.Appearance.ForeColor = Color.Blue;
                if (!xtraTabControlStoresContent.Visible)
                    xtraTabControlStoresContent.Visible = true;

                XtraTabPage xtraTabPage = getTabPage(xtraTabControlStoresContent, "عرض اذن وارد");
                if (xtraTabPage == null)
                {
                    xtraTabControlStoresContent.TabPages.Add("عرض اذن وارد");
                    xtraTabPage = getTabPage(xtraTabControlStoresContent, "عرض اذن وارد");
                }

                xtraTabPage.Controls.Clear();
                xtraTabControlStoresContent.SelectedTabPage = xtraTabPage;

                PermissionsReport objForm = new PermissionsReport(this, xtraTabControlStoresContent);

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

        private void navBarItemSupplierReturnedPermission_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            try
            {
                restForeColorOfNavBarItem();
                NavBarItem navBarItem = (NavBarItem)sender;
                navBarItem.Appearance.ForeColor = Color.Blue;
                if (!xtraTabControlStoresContent.Visible)
                    xtraTabControlStoresContent.Visible = true;

                XtraTabPage xtraTabPage = getTabPage(xtraTabControlStoresContent, "عرض اذن مرتجع");
                if (xtraTabPage == null)
                {
                    xtraTabControlStoresContent.TabPages.Add("عرض اذن مرتجع");
                    xtraTabPage = getTabPage(xtraTabControlStoresContent, "عرض اذن مرتجع");
                }

                xtraTabPage.Controls.Clear();
                xtraTabControlStoresContent.SelectedTabPage = xtraTabPage;

                PermissionReturnedReport objForm = new PermissionReturnedReport(this, xtraTabControlStoresContent);

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

        private void navBarItemTaswayatAdding_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            try
            {
                restForeColorOfNavBarItem();
                NavBarItem navBarItem = (NavBarItem)sender;
                navBarItem.Appearance.ForeColor = Color.Blue;
                if (!xtraTabControlStoresContent.Visible)
                    xtraTabControlStoresContent.Visible = true;

                XtraTabPage xtraTabPage = getTabPage(xtraTabControlStoresContent, "تسويات الاضافة");
                if (xtraTabPage == null)
                {
                    xtraTabControlStoresContent.TabPages.Add("تسويات الاضافة");
                    xtraTabPage = getTabPage(xtraTabControlStoresContent, "تسويات الاضافة");
                }

                xtraTabPage.Controls.Clear();
                xtraTabControlStoresContent.SelectedTabPage = xtraTabPage;

                TaswayaAdding objForm = new TaswayaAdding(this);

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

        private void navBarItemTaswayatSubtract_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            try
            {
                restForeColorOfNavBarItem();
                NavBarItem navBarItem = (NavBarItem)sender;
                navBarItem.Appearance.ForeColor = Color.Blue;
                if (!xtraTabControlStoresContent.Visible)
                    xtraTabControlStoresContent.Visible = true;

                XtraTabPage xtraTabPage = getTabPage(xtraTabControlStoresContent, "تسويات الخصم");
                if (xtraTabPage == null)
                {
                    xtraTabControlStoresContent.TabPages.Add("تسويات الخصم");
                    xtraTabPage = getTabPage(xtraTabControlStoresContent, "تسويات الخصم");
                }

                xtraTabPage.Controls.Clear();
                xtraTabControlStoresContent.SelectedTabPage = xtraTabPage;

                TaswayaSubtract objForm = new TaswayaSubtract(this);

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
        /// <summary>
        private void xtraTabControlStoresContent_CloseButtonClick(object sender, EventArgs e)
        {
            try
            {
                ClosePageButtonEventArgs arg = e as ClosePageButtonEventArgs;
                XtraTabPage xtraTabPage = (XtraTabPage)arg.Page;
                if (xtraTabPage.ImageOptions.Image != null)
                {
                    DialogResult dialogResult = MessageBox.Show("Are you sure you want to Close this page without save?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dialogResult == DialogResult.Yes)
                    {
                        xtraTabControlStoresContent.TabPages.Remove(arg.Page as XtraTabPage);
                    }
                    else if (dialogResult == DialogResult.No)
                    {

                    }
                }
                else
                {
                    xtraTabControlStoresContent.TabPages.Remove(arg.Page as XtraTabPage);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //functions
        //Stores
        public void bindDisplayStoresForm(XtraTabPage xtraTabPage)
        {
            Stores objForm = new Stores(this);
            objForm.TopLevel = false;

            xtraTabPage.Controls.Add(objForm);
            objForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            objForm.Dock = DockStyle.Fill;
            objForm.Show();
        }
        public void bindRecordStoresForm(Stores stores)
        {
            Store_Record objForm = new Store_Record(stores, xtraTabControlStoresContent);

            objForm.TopLevel = false;
            XtraTabPage xtraTabPage = getTabPage(xtraTabControlStoresContent,"أضافة مخزن");
            if (xtraTabPage == null)
            {
                xtraTabControlStoresContent.TabPages.Add("أضافة مخزن");
                xtraTabPage = getTabPage(xtraTabControlStoresContent,"أضافة مخزن");

            }
            xtraTabPage.Controls.Clear();
            xtraTabPage.Controls.Add(objForm);
            xtraTabControlStoresContent.SelectedTabPage = xtraTabPage;

            objForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            objForm.Dock = DockStyle.Fill;
            objForm.Show();

        }
        public void bindUpdateStoresForm(DataRowView selRow, Stores stores)
        {
            int id = Convert.ToInt16(selRow[0].ToString());

            Store_Update objForm = new Store_Update(id, stores, xtraTabControlStoresContent);
            objForm.TopLevel = false;
            XtraTabPage xtraTabPage = getTabPage(xtraTabControlStoresContent,"تعديل مخزن");
            if (xtraTabPage == null)
            {
                xtraTabControlStoresContent.TabPages.Add("تعديل مخزن");
                xtraTabPage = getTabPage(xtraTabControlStoresContent,"تعديل مخزن");
                xtraTabPage.Controls.Clear();
                xtraTabPage.Controls.Add(objForm);
                xtraTabControlStoresContent.SelectedTabPage = xtraTabPage;
                objForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                objForm.Dock = DockStyle.Fill;
                objForm.Show();
            }
            else if (xtraTabPage.ImageOptions.Image != null)
            {
                DialogResult dialogResult = MessageBox.Show("There is unsave data To you wound override it?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                    xtraTabPage.Controls.Clear();
                    xtraTabPage.Controls.Add(objForm);
                    xtraTabControlStoresContent.SelectedTabPage = xtraTabPage;
                    objForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                    objForm.Dock = DockStyle.Fill;
                    objForm.Show();
                }
                else if (dialogResult == DialogResult.No)
                {
                    xtraTabControlStoresContent.SelectedTabPage = xtraTabPage;
                }
            }
            else
            {
                xtraTabPage.Controls.Clear();
                xtraTabPage.Controls.Add(objForm);
                xtraTabControlStoresContent.SelectedTabPage = xtraTabPage;
                objForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                objForm.Dock = DockStyle.Fill;
                objForm.Show();
            }

        }
        public void bindReportStoresForm(GridControl gridControl)
        {
            Store_Report objForm = new Store_Report(gridControl);
            objForm.TopLevel = false;
            XtraTabPage xtraTabPage = getTabPage(xtraTabControlStoresContent,"تقرير المخازن");
            if (xtraTabPage == null)
            {
                xtraTabControlStoresContent.TabPages.Add("تقرير المخازن");
                xtraTabPage = getTabPage(xtraTabControlStoresContent,"تقرير المخازن");
            }
            xtraTabPage.Controls.Clear();
            xtraTabPage.Controls.Add(objForm);
            xtraTabControlStoresContent.SelectedTabPage = xtraTabPage;
            objForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            objForm.Dock = DockStyle.Fill;
            objForm.Show();
        }
        public void bindStorePlacesForm(DataRowView storeRow)
        {
            DataRowView storeRow1 = storeRow;
            StorePlaces objForm = new StorePlaces(storeRow1);
            objForm.TopLevel = false;
            XtraTabPage xtraTabPage = getTabPage(xtraTabControlStoresContent,"أماكن التخزين");
            if (xtraTabPage == null)
            {
                xtraTabControlStoresContent.TabPages.Add("أماكن التخزين");
                xtraTabPage = getTabPage(xtraTabControlStoresContent,"أماكن التخزين");
            }
            xtraTabPage.Controls.Clear();
            xtraTabPage.Controls.Add(objForm);
            xtraTabControlStoresContent.SelectedTabPage = xtraTabPage;
            objForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            objForm.Dock = DockStyle.Fill;
            objForm.Show();
        }
        //Products
        public void bindDisplayProductsForm(XtraTabPage xtraTabPage)
        {
            Products objForm = new Products(this);
            objForm.TopLevel = false;

            xtraTabPage.Controls.Add(objForm);
            objForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            objForm.Dock = DockStyle.Fill;
            objForm.Show();
        }
        public void bindRecordProductForm(Products products)
        {
            Product_Record objForm = new Product_Record(products, xtraTabControlStoresContent);

            objForm.TopLevel = false;
            XtraTabPage xtraTabPage = getTabPage(xtraTabControlStoresContent,"أضافة بند");
            if (xtraTabPage == null)
            {
                xtraTabControlStoresContent.TabPages.Add("أضافة بند");
                xtraTabPage = getTabPage(xtraTabControlStoresContent,"أضافة بند");
            }
            xtraTabPage.Controls.Clear();
            xtraTabPage.Controls.Add(objForm);
            xtraTabControlStoresContent.SelectedTabPage = xtraTabPage;

            objForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            objForm.Dock = DockStyle.Fill;
            objForm.Show();

        }
        public void bindUpdateProductForm(DataRowView prodRow, Products products)
        {
            Product_Update objForm = new Product_Update(prodRow, products, xtraTabControlStoresContent);
            objForm.TopLevel = false;
            XtraTabPage xtraTabPage = getTabPage(xtraTabControlStoresContent,"تعديل بند");
            if (xtraTabPage == null)
            {
                xtraTabControlStoresContent.TabPages.Add("تعديل بند");
                xtraTabPage = getTabPage(xtraTabControlStoresContent,"تعديل بند");
                xtraTabPage.Controls.Clear();
                xtraTabPage.Controls.Add(objForm);
                xtraTabControlStoresContent.SelectedTabPage = xtraTabPage;
                objForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                objForm.Dock = DockStyle.Fill;
                objForm.Show();
            }
            else if (xtraTabPage.ImageOptions.Image != null)
            {
                DialogResult dialogResult = MessageBox.Show("There is unsave data To you wound override it?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                    xtraTabPage.Controls.Clear();
                    xtraTabPage.Controls.Add(objForm);
                    xtraTabControlStoresContent.SelectedTabPage = xtraTabPage;
                    objForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                    objForm.Dock = DockStyle.Fill;
                    objForm.Show();
                }
                else if (dialogResult == DialogResult.No)
                {
                    xtraTabControlStoresContent.SelectedTabPage = xtraTabPage;
                }
            }
            else
            {
                xtraTabPage.Controls.Clear();
                xtraTabPage.Controls.Add(objForm);
                xtraTabControlStoresContent.SelectedTabPage = xtraTabPage;
                objForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                objForm.Dock = DockStyle.Fill;
                objForm.Show();
            }

        }
        public void bindReportProductForm(GridControl gridControl)
        {
            Product_Report objForm = new Product_Report(gridControl, "تقرير البنود");
            objForm.TopLevel = false;
            XtraTabPage xtraTabPage = getTabPage(xtraTabControlStoresContent,"تقرير البنود");
            if (xtraTabPage == null)
            {
                xtraTabControlStoresContent.TabPages.Add("تقرير البنود");
                xtraTabPage = getTabPage(xtraTabControlStoresContent,"تقرير البنود");
            }
            xtraTabPage.Controls.Clear();
            xtraTabPage.Controls.Add(objForm);
            xtraTabControlStoresContent.SelectedTabPage = xtraTabPage;
            objForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            objForm.Dock = DockStyle.Fill;
            objForm.Show();
        }
        //Sets
        public void bindDisplaySetsForm(XtraTabPage xtraTabPage)
        {
            Ataqm objForm = new Ataqm(this);
            objForm.TopLevel = false;

            xtraTabPage.Controls.Add(objForm);
            objForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            objForm.Dock = DockStyle.Fill;
            objForm.Show();
        }
        public void bindRecordSetForm(Ataqm ataqm)
        {
            SetRecord objForm = new SetRecord(ataqm, xtraTabControlStoresContent);

            objForm.TopLevel = false;
            XtraTabPage xtraTabPage = getTabPage(xtraTabControlStoresContent,"أضافة طقم");
            if (xtraTabPage == null)
            {
                xtraTabControlStoresContent.TabPages.Add("أضافة طقم");
                xtraTabPage = getTabPage(xtraTabControlStoresContent,"أضافة طقم");
            }
            xtraTabPage.Controls.Clear();
            xtraTabPage.Controls.Add(objForm);
            xtraTabControlStoresContent.SelectedTabPage = xtraTabPage;

            objForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            objForm.Dock = DockStyle.Fill;
            objForm.Show();

        }
        public void bindUpdateSetForm(DataRowView prodRow, Ataqm ataqm)
        {
            SetUpdate objForm = new SetUpdate(prodRow, ataqm, xtraTabControlStoresContent);
            objForm.TopLevel = false;
            XtraTabPage xtraTabPage = getTabPage(xtraTabControlStoresContent,"تعديل طقم");
            if (xtraTabPage == null)
            {
                xtraTabControlStoresContent.TabPages.Add("تعديل طقم");
                xtraTabPage = getTabPage(xtraTabControlStoresContent,"تعديل طقم");
                xtraTabPage.Controls.Clear();
                xtraTabPage.Controls.Add(objForm);
                xtraTabControlStoresContent.SelectedTabPage = xtraTabPage;
                objForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                objForm.Dock = DockStyle.Fill;
                objForm.Show();
            }
            else if (xtraTabPage.ImageOptions.Image != null)
            {
                DialogResult dialogResult = MessageBox.Show("There is unsave data To you wound override it?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                    xtraTabPage.Controls.Clear();
                    xtraTabPage.Controls.Add(objForm);
                    xtraTabControlStoresContent.SelectedTabPage = xtraTabPage;
                    objForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                    objForm.Dock = DockStyle.Fill;
                    objForm.Show();
                }
                else if (dialogResult == DialogResult.No)
                {
                    xtraTabControlStoresContent.SelectedTabPage = xtraTabPage;
                }
            }
            else
            {
                xtraTabPage.Controls.Clear();
                xtraTabPage.Controls.Add(objForm);
                xtraTabControlStoresContent.SelectedTabPage = xtraTabPage;
                objForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                objForm.Dock = DockStyle.Fill;
                objForm.Show();
            }

        }
        public void bindReportSetForm(GridControl gridControl)
        {
            SetReport objForm = new SetReport(gridControl);
            objForm.TopLevel = false;
            XtraTabPage xtraTabPage = getTabPage(xtraTabControlStoresContent,"تقرير أطقم");
            if (xtraTabPage == null)
            {
                xtraTabControlStoresContent.TabPages.Add("تقرير أطقم");
                xtraTabPage = getTabPage(xtraTabControlStoresContent,"تقرير أطقم");
            }
            xtraTabPage.Controls.Clear();
            xtraTabPage.Controls.Add(objForm);
            xtraTabControlStoresContent.SelectedTabPage = xtraTabPage;
            objForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            objForm.Dock = DockStyle.Fill;
            objForm.Show();
        }
        //sets Storage
        public void bindDisplaySetsStorageForm(XtraTabPage xtraTabPage)
        {
            AtaqmStorage objForm = new AtaqmStorage(this);
            objForm.TopLevel = false;

            xtraTabPage.Controls.Add(objForm);
            objForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            objForm.Dock = DockStyle.Fill;
            objForm.Show();
        }
        public void bindTagame3SetForm(AtaqmStorage ataqm)
        {
            SetTagame3 objForm = new SetTagame3(ataqm);
            objForm.TopLevel = false;
            XtraTabPage xtraTabPage = getTabPage(xtraTabControlStoresContent,"تجميع طقم");
            if (xtraTabPage == null)
            {
                xtraTabControlStoresContent.TabPages.Add("تجميع طقم");
                xtraTabPage = getTabPage(xtraTabControlStoresContent,"تجميع طقم");
                xtraTabPage.Controls.Clear();
                xtraTabPage.Controls.Add(objForm);
                xtraTabControlStoresContent.SelectedTabPage = xtraTabPage;
                objForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                objForm.Dock = DockStyle.Fill;
                objForm.Show();
            }
            else if (xtraTabPage.ImageOptions.Image != null)
            {
                DialogResult dialogResult = MessageBox.Show("There is unsave data To you wound override it?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                    xtraTabPage.Controls.Clear();
                    xtraTabPage.Controls.Add(objForm);
                    xtraTabControlStoresContent.SelectedTabPage = xtraTabPage;
                    objForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                    objForm.Dock = DockStyle.Fill;
                    objForm.Show();
                }
                else if (dialogResult == DialogResult.No)
                {
                    xtraTabControlStoresContent.SelectedTabPage = xtraTabPage;
                }
            }
            else
            {
                xtraTabPage.Controls.Clear();
                xtraTabPage.Controls.Add(objForm);
                xtraTabControlStoresContent.SelectedTabPage = xtraTabPage;
                objForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                objForm.Dock = DockStyle.Fill;
                objForm.Show();
            }

        }
        public void bindFakSetForm(AtaqmStorage ataqm)
        {
            SetFak objForm = new SetFak(ataqm);
            objForm.TopLevel = false;
            XtraTabPage xtraTabPage = getTabPage(xtraTabControlStoresContent,"فك طقم");
            if (xtraTabPage == null)
            {
                xtraTabControlStoresContent.TabPages.Add("فك طقم");
                xtraTabPage = getTabPage(xtraTabControlStoresContent,"فك طقم");
                xtraTabPage.Controls.Clear();
                xtraTabPage.Controls.Add(objForm);
                xtraTabControlStoresContent.SelectedTabPage = xtraTabPage;
                objForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                objForm.Dock = DockStyle.Fill;
                objForm.Show();
            }
            else if (xtraTabPage.ImageOptions.Image != null)
            {
                DialogResult dialogResult = MessageBox.Show("There is unsave data To you wound override it?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                    xtraTabPage.Controls.Clear();
                    xtraTabPage.Controls.Add(objForm);
                    xtraTabControlStoresContent.SelectedTabPage = xtraTabPage;
                    objForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                    objForm.Dock = DockStyle.Fill;
                    objForm.Show();
                }
                else if (dialogResult == DialogResult.No)
                {
                    xtraTabControlStoresContent.SelectedTabPage = xtraTabPage;
                }
            }
            else
            {
                xtraTabPage.Controls.Clear();
                xtraTabPage.Controls.Add(objForm);
                xtraTabControlStoresContent.SelectedTabPage = xtraTabPage;
                objForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                objForm.Dock = DockStyle.Fill;
                objForm.Show();
            }

        }
        public void bindReportStorageSetForm(GridControl gridControl)
        {
            SetReport objForm = new SetReport(gridControl);
            objForm.TopLevel = false;
            XtraTabPage xtraTabPage = getTabPage(xtraTabControlStoresContent,"تقرير كميات الأطقم");
            if (xtraTabPage == null)
            {
                xtraTabControlStoresContent.TabPages.Add("تقرير كميات الأطقم");
                xtraTabPage = getTabPage(xtraTabControlStoresContent,"تقرير كميات الأطقم");
            }
            xtraTabPage.Controls.Clear();
            xtraTabPage.Controls.Add(objForm);
            xtraTabControlStoresContent.SelectedTabPage = xtraTabPage;
            objForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            objForm.Dock = DockStyle.Fill;
            objForm.Show();
        }
        //storag
        public void bindRecordStorageForm(Storage storage)
        {
            //initialCodeStorage objForm = new initialCodeStorage(storage, xtraTabControlStoresContent);

            //objForm.TopLevel = false;
            //XtraTabPage xtraTabPage = getTabPage(xtraTabControlStoresContent,"تسجيل كميات البنود");
            //if (xtraTabPage == null)
            //{
            //    xtraTabControlStoresContent.TabPages.Add("تسجيل كميات البنود");
            //    xtraTabPage = getTabPage(xtraTabControlStoresContent,"تسجيل كميات البنود");

            //}
            //xtraTabPage.Controls.Clear();
            //xtraTabPage.Controls.Add(objForm);
            //xtraTabControlStoresContent.SelectedTabPage = xtraTabPage;

            //objForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            //objForm.Dock = DockStyle.Fill;
            //objForm.Show();
        }
        public void bindReportStorageForm(GridControl gridControl,string Title)
        {
            Product_Report objForm = new Product_Report(gridControl, Title);
            objForm.TopLevel = false;
            XtraTabPage xtraTabPage = getTabPage(xtraTabControlStoresContent,"تقرير طباعة");
            if (xtraTabPage == null)
            {
                xtraTabControlStoresContent.TabPages.Add("تقرير طباعة");
                xtraTabPage = getTabPage(xtraTabControlStoresContent, "تقرير طباعة");
            }
            xtraTabPage.Controls.Clear();
            xtraTabPage.Controls.Add(objForm);
            xtraTabControlStoresContent.SelectedTabPage = xtraTabPage;
            objForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            objForm.Dock = DockStyle.Fill;
            objForm.Show();
        }
        public void bindUpdateTaswayaAddingForm(int perNum)
        {
            StorageTaswayaAddingUpdate objForm = new StorageTaswayaAddingUpdate(perNum, this, xtraTabControlStoresContent);
            objForm.TopLevel = false;
            XtraTabPage xtraTabPage = getTabPage(xtraTabControlStoresContent,"تعديل  تسوية اضافة");
            if (xtraTabPage == null)
            {
                xtraTabControlStoresContent.TabPages.Add("تعديل  تسوية اضافة");
                xtraTabPage = getTabPage(xtraTabControlStoresContent, "تعديل  تسوية اضافة");
            }
            xtraTabPage.Controls.Clear();
            xtraTabPage.Controls.Add(objForm);
            xtraTabControlStoresContent.SelectedTabPage = xtraTabPage;

            objForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            objForm.Dock = DockStyle.Fill;
            objForm.Show();

        }
        public void bindTaswayAddingStorageForm()
        {
            StorageTaswayaAdding objForm = new StorageTaswayaAdding(this, xtraTabControlStoresContent);

            objForm.TopLevel = false;
            XtraTabPage xtraTabPage = getTabPage(xtraTabControlStoresContent, "تسوية اضافة لكميات البنود");
            if (xtraTabPage == null)
            {
                xtraTabControlStoresContent.TabPages.Add("تسوية اضافة لكميات البنود");
                xtraTabPage = getTabPage(xtraTabControlStoresContent, "تسوية اضافة لكميات البنود");
            }
            xtraTabPage.Controls.Clear();
            xtraTabPage.Controls.Add(objForm);
            xtraTabControlStoresContent.SelectedTabPage = xtraTabPage;

            objForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            objForm.Dock = DockStyle.Fill;
            objForm.Show();
        }
        public void bindUpdateTaswayaSubtractgForm(int perNum)
        {
            StorageTaswayaSubtractUpdate objForm = new StorageTaswayaSubtractUpdate(perNum, this, xtraTabControlStoresContent);
            objForm.TopLevel = false;
            XtraTabPage xtraTabPage = getTabPage(xtraTabControlStoresContent, "تعديل  تسوية خصم");
            if (xtraTabPage == null)
            {
                xtraTabControlStoresContent.TabPages.Add("تعديل  تسوية خصم");
                xtraTabPage = getTabPage(xtraTabControlStoresContent, "تعديل  تسوية خصم");
            }
            xtraTabPage.Controls.Clear();
            xtraTabPage.Controls.Add(objForm);
            xtraTabControlStoresContent.SelectedTabPage = xtraTabPage;

            objForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            objForm.Dock = DockStyle.Fill;
            objForm.Show();

        }
        public void bindTaswaySubtractStorageForm()
        {
            StorageTaswayaSubtract objForm = new StorageTaswayaSubtract(this,xtraTabControlStoresContent);

            objForm.TopLevel = false;
            XtraTabPage xtraTabPage = getTabPage(xtraTabControlStoresContent, "تسوية خصم لكميات البنود");
            if (xtraTabPage == null)
            {
                xtraTabControlStoresContent.TabPages.Add("تسوية خصم لكميات البنود");
                xtraTabPage = getTabPage(xtraTabControlStoresContent, "تسوية خصم لكميات البنود");
            }
            xtraTabPage.Controls.Clear();
            xtraTabPage.Controls.Add(objForm);
            xtraTabControlStoresContent.SelectedTabPage = xtraTabPage;

            objForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            objForm.Dock = DockStyle.Fill;
            objForm.Show();
        }
        //delivery
        public void bindDisplayDeliveryForm()
        {
            if (!xtraTabControlStoresContent.Visible)
                xtraTabControlStoresContent.Visible = true;

            XtraTabPage xtraTabPage = getTabPage(xtraTabControlStoresContent, "تسليم طلب");
            if (xtraTabPage == null)
            {
                xtraTabControlStoresContent.TabPages.Add("تسليم طلب");
                xtraTabPage = getTabPage(xtraTabControlStoresContent, "تسليم طلب");
            }

            xtraTabPage.Controls.Clear();
            xtraTabControlStoresContent.SelectedTabPage = xtraTabPage;

            CustomerDelivery objForm = new CustomerDelivery();

            objForm.TopLevel = false;
            xtraTabPage.Controls.Add(objForm);
            objForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            objForm.Dock = DockStyle.Fill;
            objForm.Show();
        }
        public void bindDisplayDeliveryForm(string permissionNum,int flag)
        {
            if (!xtraTabControlStoresContent.Visible)
                xtraTabControlStoresContent.Visible = true;

            XtraTabPage xtraTabPage = getTabPage(xtraTabControlStoresContent, "تسليم طلب");
            if (xtraTabPage == null)
            {
                xtraTabControlStoresContent.TabPages.Add("تسليم طلب");
                xtraTabPage = getTabPage(xtraTabControlStoresContent, "تسليم طلب");
            }

            xtraTabPage.Controls.Clear();
            xtraTabControlStoresContent.SelectedTabPage = xtraTabPage;

            CustomerDelivery objForm = new CustomerDelivery(permissionNum, flag);

            objForm.TopLevel = false;
            xtraTabPage.Controls.Add(objForm);
            objForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            objForm.Dock = DockStyle.Fill;
            objForm.Show();
        }
        public void bindDisplayExpectedOrdersReport(XtraTabPage xtraTabPage)
        {
            searchReciveDate = new SearchRecive_Date(this, xtraTabControlStoresContent);
            searchReciveDate.TopLevel = false;

            xtraTabPage.Controls.Add(searchReciveDate);
            searchReciveDate.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            searchReciveDate.Dock = DockStyle.Fill;
            searchReciveDate.Show();
        }
        public void GetExpectedOrders(object sender, EventArgs e)
        {
            try
            {
                ExpectedOrdersFunction();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }
        public void ExpectedOrdersFunction()
        {
            int count = 0;
            dbconnection.Close();
            string query = "select orders.Order_ID as 'التسلسل',supplier.Supplier_Name as 'المورد',orders.Order_Number as 'رقم الفاتورة',orders.Employee_Name as 'الموظف المسئول',store.Store_Name as 'المخزن',orders.Request_Date as 'تاريخ الطلب',orders.Receive_Date as 'تاريخ الاستلام' from orders inner join supplier on supplier.Supplier_ID=orders.Supplier_ID inner join store on store.Store_ID=orders.Store_ID where orders.Confirmed=1 and orders.Received=0 and Receive_Date ='" + DateTime.Now.Date.ToString("yyyy-MM-dd") + "'";
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
                    labelStoreExpectedOrder.Text = count.ToString();
                    labelStoreExpectedOrder.Visible = true;
                    dr.Close();
                }
            }
            else
            {
                labelStoreExpectedOrder.Text = "0";
                labelStoreExpectedOrder.Visible = true;
            }
        }
        private void StoreMainForm_Resize(object sender, EventArgs e)
        {
            try
            {
                if (SetUpdate.tipImage != null)
                {
                    SetUpdate.tipImage.Close();
                    SetUpdate.tipImage = null;
                }
                if (SetRecord.tipImage != null)
                {
                    SetRecord.tipImage.Close();
                    SetRecord.tipImage = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        //
    }
}

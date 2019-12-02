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
using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace MainSystem
{
    public partial class MainForm : DevExpress.XtraEditors.XtraForm
    {
        MySqlConnection dbconnection;
        XtraTabPage StoreTP;
        XtraTabPage SalesTP;
        XtraTabPage HRTP;
        XtraTabPage CarsTP;
        XtraTabPage POSTP;
        XtraTabPage BankTP;
        XtraTabPage ReceptionTP;
        XtraTabPage ShippingTP;
        XtraTabPage AccountingTP;
        XtraTabPage CodingTP;
        XtraTabPage PurchasesTP;
        int index = 1;
        static int countBackup = 0;

        public MainForm()
        {
            try
            {
                dbconnection = new MySqlConnection(connection.connectionString);
                InitializeComponent();
                //bankSystem
                if (UserControl.userType == 5)
                {
                    POSSystem();
                }
                else
                {
                    initialize();
                    ReceptionSystem();
                    SalesMainForm();
                    ShippingForm();
                    POSSystem();
                    initializeBranch();
                    PurchasesMainForm();
                }
                if (UserControl.userType == 1)
                {
                    pictureBoxSetting.Visible = true;
                }
                else
                {
                    pictureBoxSetting.Visible = false;
                }
                StoreTP = xtraTabPageStores;
                SalesTP =xtraTabPageSales;
                HRTP = xtraTabPageHR;
                CarsTP = xtraTabPageCars;
                POSTP = xtraTabPagePOS;
                BankTP = xtraTabPageBank;
                ReceptionTP = xtraTabPageReception;
                ShippingTP = xtraTabPageShipping;
                AccountingTP = xtraTabPageAccounting;
                CodingTP = xtraTabPageCoding;
                PurchasesTP = xtraTabPagePurchases;
                xtraTabControlMainContainer.TabPages.Remove(xtraTabPageStores);
                xtraTabControlMainContainer.TabPages.Remove(xtraTabPageSales);
                xtraTabControlMainContainer.TabPages.Remove(xtraTabPageHR);
                xtraTabControlMainContainer.TabPages.Remove(xtraTabPageCars);
                xtraTabControlMainContainer.TabPages.Remove(xtraTabPagePOS);
                xtraTabControlMainContainer.TabPages.Remove(xtraTabPageBank);
                xtraTabControlMainContainer.TabPages.Remove(xtraTabPageReception);
                xtraTabControlMainContainer.TabPages.Remove(xtraTabPageShipping);
                xtraTabControlMainContainer.TabPages.Remove(xtraTabPageAccounting);
                xtraTabControlMainContainer.TabPages.Remove(xtraTabPageCoding);
                xtraTabControlMainContainer.TabPages.Remove(xtraTabPagePurchases);

                var DailyTimeBackup = "19:00:00";
                var timePartsBackup = DailyTimeBackup.Split(new char[1] { ':' });

                var dateNowBackup = DateTime.Now;
                var dateBackup = new DateTime(dateNowBackup.Year, dateNowBackup.Month, dateNowBackup.Day,
                           int.Parse(timePartsBackup[0]), int.Parse(timePartsBackup[1]), int.Parse(timePartsBackup[2]));
                TimeSpan tsBackup;
                if (dateBackup > dateNowBackup)
                    tsBackup = dateBackup - dateNowBackup;
                else
                {
                    dateBackup = dateBackup.AddDays(1);
                    tsBackup = dateBackup - dateNowBackup;
                }

                if (UserControl.userType == 1)
                {
                    //waits certan time and run the code
                    Task.Delay(tsBackup).ContinueWith((x) => BackupMethod());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        private void MainForm_Load(object sender, EventArgs e)
        {
            if (UserControl.userType == 1)
            {
                btnCars.Enabled = true;
                btnCars.Checked = true;
                btnStores.Enabled = true;
                btnStores.Checked = true;
                btnBank.Enabled = true;
                btnBank.Checked = true;
                btnReception.Enabled = true;
                btnReception.Checked = true;
                btnPOS.Enabled = true;
                btnPOS.Checked = true;
                btnSales.Enabled = true;
                btnSales.Checked = true;
                TIElsha7n.Enabled = true;
                TIElsha7n.Checked = true;
                AccountingSystem.Enabled = true;
                AccountingSystem.Checked = true;
                btnPurchases.Enabled = true;
                btnPurchases.Checked = true;
                btnHR.Enabled = true;
                btnHR.Checked = true;
                btnCustomerService.Enabled = true;
                btnCustomerService.Checked = true;
                btnCoding.Enabled = true;
                btnCoding.Checked = true;
                btnReports.Enabled = true;
                btnReports.Checked = true;

                pictureBoxBell.Visible = true;
                pictureBoxSales.Visible = true;
                pictureBoxPurchase.Visible = true;
                pictureBoxPurchaseLeast.Visible = true;
                pictureBoxStoreExpectedOrder.Visible = true;
                pictureBoxCar.Visible = true;
            }
            else if (UserControl.userType == 2)
            {
                btnStores.Enabled = true;
                btnStores.Checked = true;

                pictureBoxStoreExpectedOrder.Visible = true;

                navBarGroup1.Visible = false;
                navBarGroup2.Visible = false;
                navBarGroup11.Visible = false;
                navBarGroup54.Visible = false;
                navBarGroupProductsTicket.Visible = false;
                navBarGroup12.Visible = false;

                navBarGroup3.Visible = false;
                navBarGroup4.Visible = false;
                navBarItemStoreReturn.Visible = false;
            }
            else if (UserControl.userType == 3)
            {
                btnBank.Enabled = true;
                btnBank.Checked = true;
                btnStores.Enabled = true;
                btnStores.Checked = true;

                navBarGroup1.Visible = false;
                navBarGroup2.Visible = false;
                navBarGroup3.Visible = false;
                navBarGroup4.Visible = false;
                navBarGroup5.Visible = false;
                navBarGroup54.Visible = false;
                navBarGroup7.Visible = false;
                navBarGroup8.Visible = false;
                navBarGroup9.Visible = false;
                navBarGroup10.Visible = false;
                navBarGroup11.Visible = false;
                navBarGroupProductsTicket.Visible = false;
                navBarGroup12.Visible = false;

                navBarGroupReportBank.Visible = false;
            }
            else if (UserControl.userType == 4)
            {
                btnReception.Enabled = true;
                btnReception.Checked = true;
            }
            else if (UserControl.userType == 5)
            {
                btnPOS.Enabled = true;
                btnPOS.Checked = true;

                pictureBoxBell.Visible = true;
            }
            else if (UserControl.userType == 6/* || UserControl.userType == 7*/)
            {
                btnSales.Enabled = true;
                btnSales.Checked = true;

                //if (UserControl.userType == 6)
                //{
                    //pictureBoxSales.Visible = true;
                    btnTaswayAgalBills.Visible = false;
                    navBarItem153.Visible = false;
                    //navBarItemSalesTransitions.Visible = false;
                    navBarItemBillsAgleTransitionsReport.Visible = false;
                    navBarItemTotalSales.Visible = false;
                    //btnBank.Enabled = true;
                    //btnBank.Checked = true;

                    btnStores.Enabled = true;
                    btnStores.Checked = true;

                    navBarGroup1.Visible = false;
                    navBarGroup2.Visible = false;
                    navBarGroup3.Visible = false;
                    navBarGroup4.Visible = false;
                    navBarGroup6.Visible = false;
                    navBarGroup54.Visible = false;
                    navBarGroup7.Visible = false;
                    //navBarGroup8.Visible = false;
                    navBarGroup9.Visible = false;
                    navBarGroup10.Visible = false;
                    navBarGroup11.Visible = false;
                    navBarGroupProductsTicket.Visible = false;
                    navBarGroup12.Visible = false;
                    navBarItemStoreReturn.Visible = false;
                    navBarItemConfirmTransferFromStore.Visible = false;
                    navBarItem17.Visible = false;
                //}
                navBarGroup13.Visible = false;
                navBarGroup39.Visible = false;
            }
            else if (UserControl.userType == 7)
            {
                btnStores.Enabled = true;
                btnStores.Checked = true;
                navBarGroupProductsTicket.Visible = false;
                btnSales.Enabled = true;
                btnSales.Checked = true;
                AccountingSystem.Enabled = true;
                AccountingSystem.Checked = true;
                btnPOS.Enabled = true;
                btnPOS.Checked = true;
                navBarGroupBillRecord.Visible = false;
                navBarGroupSupplierPayments.Visible = false;
                navBarGroup49.Visible = false;
                navBarGroup47.Visible = false;
                navBarGroup15.Visible = false;
                navBarGroup16.Visible = false;
                navBarGroup1.Visible = false;
                btnPOS.Enabled = true;
                btnPOS.Checked = true;
                navBarGroupBillRecord.Visible = false;
                //userAccess();
            }
            else if (UserControl.userType == 8)
            {
                TIElsha7n.Enabled = true;
                TIElsha7n.Checked = true;
            }
            else if (UserControl.userType == 9)
            {
                AccountingSystem.Enabled = true;
                AccountingSystem.Checked = true;
            }
            else if (UserControl.userType == 10)
            {
                btnPurchases.Enabled = true;
                btnPurchases.Checked = true;
                AccountingSystem.Enabled = true;
                AccountingSystem.Checked = true;
                btnStores.Enabled = true;
                btnStores.Checked = true;

                navBarGroup51.Visible = false;
                navBarGroup46.Visible = false;
                navBarGroup52.Visible = false;
                navBarGroup53.Visible = false;
                navBarGroupLeastQuantity.Visible = false;
                navBarGroupPurchasesReport.Visible = false;
                navBarGroup32.Visible = false;
                navBarGroup40.Visible = false;

                navBarGroup1.Visible = false;
                navBarGroup2.Visible = false;
                navBarGroup3.Visible = false;
                navBarGroup4.Visible = false;
                navBarGroup5.Visible = false;
                navBarGroup6.Visible = false;
                navBarGroup7.Visible = false;
                navBarGroup8.Visible = false;
                navBarGroup9.Visible = false;
                navBarGroup10.Visible = false;
                navBarGroup11.Visible = false;
                navBarGroupProductsTicket.Visible = false;
                navBarGroup12.Visible = false;

                //pictureBoxPurchase.Visible = true;
                //pictureBoxPurchaseLeast.Visible = true;
            }
            else if (UserControl.userType == 11)
            {
                btnHR.Enabled = true;
                btnHR.Checked = true;
            }
            else if (UserControl.userType == 12)
            {
                btnCustomerService.Enabled = true;
                btnCustomerService.Checked = true;
            }
            //data entry- coding
            else if (UserControl.userType == 13)
            {
                btnStores.Enabled = true;
                btnStores.Checked = true;
                navBarGroup11.Visible = false;
                navBarGroupProductsTicket.Visible = false;
                btnSales.Enabled = true;
                btnSales.Checked = true;
                navBarGroupSupplierPayments.Visible = false;
                btnPOS.Enabled = true;
                btnPOS.Checked = true;
                navBarGroupBillRecord.Visible = false;
                navBarGroup49.Visible = false;
                navBarGroup47.Visible = false;
                navBarGroup15.Visible = false;
                navBarGroup16.Visible = false;
                navBarGroup1.Visible = false;
                navBarItemSalesTransitions.Visible = false;
                btnPOS.Enabled = true;
                btnPOS.Checked = true;
                navBarGroupBillRecord.Visible = false;
                //userAccess();
            }
            //شيخون
            else if (UserControl.userType == 14)
            {
                btnCars.Enabled = true;
                btnCars.Checked = true;

                pictureBoxCar.Visible = true;
            }
            //مدير
            else if (UserControl.userType == 15)
            {
                btnPOS.Enabled = true;
                btnPOS.Checked = true;
                btnSales.Enabled = true;
                btnSales.Checked = true;
                pictureBoxBell.Visible = true;

                btnStores.Enabled = true;
                btnStores.Checked = true;
                navBarGroup1.Visible = false;
                navBarGroup2.Visible = false;
                navBarGroup3.Visible = false;
                navBarGroup4.Visible = false;
                navBarGroup5.Visible = false;
                navBarGroup6.Visible = false;
                navBarGroup7.Visible = false;
                navBarGroup8.Visible = false;
                navBarGroup9.Visible = false;
                navBarGroup10.Visible = false;
                navBarGroup11.Visible = false;
                navBarGroupProductsTicket.Visible = false;
                navBarGroup12.Visible = false;
                navBarItemInformationFactoryReport.Visible = false;
                navBarItemFactoryProduct.Visible = false;

                navBarItemSalesTransitions.Visible = false;
                navBarItemBillsAgleTransitionsReport.Visible = false;
                navBarItemTotalSales.Visible = false;
                btnTaswayAgalBills.Visible = false;
                navBarItem153.Visible = false;
                navBarGroupReportPointSale.Visible = false;

                userAccess();
            }
            else if (UserControl.userType == 16)
            {
                btnSales.Enabled = true;
                btnSales.Checked = true;
                //btnStores.Enabled = true;
                //btnStores.Checked = true;
                btnBank.Enabled = true;
                btnBank.Checked = true;
                btnReception.Enabled = true;
                btnReception.Checked = true;
                btnPOS.Enabled = true;
                btnPOS.Checked = true;
                navBarGroupReportPointSale.Visible = false;
                pictureBoxBell.Visible = true;
                navBarItemBillsAgleTransitionsReport.Visible = false;
                navBarItemTotalSales.Visible = false;
                
                navBarGroup13.Visible = false;
                navBarGroup39.Visible = false;
                btnTaswayAgalBills.Visible = false;
                navBarItem153.Visible = false;
            }
            //eslam
            else if (UserControl.userType == 17)
            {
                btnPurchases.Enabled = true;
                btnPurchases.Checked = true;
                AccountingSystem.Enabled = true;
                AccountingSystem.Checked = true;
                btnSales.Enabled = true;
                btnSales.Checked = true;
                btnStores.Enabled = true;
                btnStores.Checked = true;
                btnPOS.Enabled = true;
                btnPOS.Checked = true;
                navBarGroupBillRecord.Visible = false;

                navBarGroup51.Visible = false;
                navBarGroup46.Visible = false;
                navBarGroup52.Visible = false;
                navBarGroup53.Visible = false;
                navBarGroupLeastQuantity.Visible = false;
                navBarGroupPurchasesReport.Visible = false;
                navBarGroup32.Visible = false;
                navBarGroup40.Visible = false;

                navBarGroup15.Visible = false;
                navBarGroup16.Visible = false;
                navBarGroup17.Visible = false;
                navBarGroup13.Visible = false;
                navBarGroup39.Visible = false;

                navBarGroup1.Visible = false;
                navBarGroup2.Visible = false;
                navBarGroup3.Visible = false;
                navBarGroup4.Visible = false;
                navBarGroup5.Visible = false;
                navBarGroup6.Visible = false;
                navBarGroup7.Visible = false;
                navBarGroup8.Visible = false;
                navBarGroup9.Visible = false;
                navBarGroup10.Visible = false;
                navBarGroup11.Visible = false;
                navBarGroupProductsTicket.Visible = false;
                navBarGroup12.Visible = false;

                //pictureBoxPurchase.Visible = true;
                //pictureBoxPurchaseLeast.Visible = true;
            }
            //marwa
            else if (UserControl.userType == 18)
            {
                btnPurchases.Enabled = true;
                btnPurchases.Checked = true;
                AccountingSystem.Enabled = true;
                AccountingSystem.Checked = true;
                btnSales.Enabled = true;
                btnSales.Checked = true;
                btnStores.Enabled = true;
                btnStores.Checked = true;

                navBarGroup51.Visible = false;
                navBarGroup46.Visible = false;
                navBarGroup52.Visible = false;
                navBarGroup53.Visible = false;
                navBarGroupLeastQuantity.Visible = false;
                navBarGroupPurchasesReport.Visible = false;
                navBarGroup32.Visible = false;
                navBarGroup40.Visible = false;

                navBarGroup15.Visible = false;
                navBarGroup16.Visible = false;
                navBarGroup17.Visible = false;
                navBarGroup13.Visible = false;
                navBarGroup39.Visible = false;

                navBarGroup1.Visible = false;
                navBarGroup2.Visible = false;
                navBarGroup3.Visible = false;
                navBarGroup4.Visible = false;
                navBarGroup5.Visible = false;
                navBarGroup6.Visible = false;
                navBarGroup7.Visible = false;
                navBarGroup8.Visible = false;
                navBarGroup9.Visible = false;
                navBarGroup10.Visible = false;
                navBarGroup11.Visible = false;
                navBarGroupProductsTicket.Visible = false;
                navBarGroup12.Visible = false;

                //pictureBoxPurchase.Visible = true;
                //pictureBoxPurchaseLeast.Visible = true;
            }
            //ahmed sayed
            else if (UserControl.userType == 19)
            {
                btnPurchases.Enabled = true;
                btnPurchases.Checked = true;
                btnStores.Enabled = true;
                btnStores.Checked = true;
                
                navBarGroup1.Visible = false;
                navBarGroup2.Visible = false;
                navBarGroup3.Visible = false;
                navBarGroup4.Visible = false;
                navBarGroup5.Visible = false;
                navBarGroup6.Visible = false;
                navBarGroup7.Visible = false;
                navBarGroup8.Visible = false;
                navBarGroup9.Visible = false;
                navBarGroup10.Visible = false;
                navBarGroup11.Visible = false;
                //navBarGroup12.Visible = false;

                navBarGroup43.Visible = false;
                navBarGroup44.Visible = false;
                //navBarGroup46.Visible = false;
                navBarGroupSupplier.Visible = false;
                navBarGroup48.Visible = false;

                pictureBoxPurchase.Visible = true;
                pictureBoxPurchaseLeast.Visible = true;
            }
            //asmaa rady
            else if (UserControl.userType == 20)
            {
                pictureBoxPurchaseLeast.Visible = true;
                navBarGroup43.Visible = false;
                navBarGroup44.Visible = false;
                navBarGroup51.Visible = false;
                navBarGroup46.Visible = false;
                navBarGroup52.Visible = false;
                navBarGroup53.Visible = false;
                navBarItemLeastQuantity.Visible = false;
                //navBarGroupLeastQuantity.Visible = false;
                navBarGroup48.Visible = false;
                navBarGroupSupplier.Visible = false;
                navBarGroupPurchasesReport.Visible = false;
                navBarGroup45.Visible = false;
                navBarGroup55.Visible = false;
            }
            //بوابة
            else if (UserControl.userType == 21)
            {
                btnStores.Enabled = true;
                btnStores.Checked = true;

                navBarGroup1.Visible = false;
                navBarGroup2.Visible = false;
                navBarGroup4.Visible = false;
                navBarGroup5.Visible = false;
                navBarGroup6.Visible = false;
                navBarGroup7.Visible = false;
                navBarGroup8.Visible = false;
                navBarGroup9.Visible = false;
                navBarGroup10.Visible = false;
                navBarGroup11.Visible = false;
                navBarGroupProductsTicket.Visible = false;
                navBarGroup12.Visible = false;
                navBarGroup54.Visible = false;
            }
            //حركة استلام
            else if (UserControl.userType == 22)
            {
                btnStores.Enabled = true;
                btnStores.Checked = true;

                navBarGroup1.Visible = false;
                navBarGroup2.Visible = false;
                navBarGroup3.Visible = false;
                navBarItem11.Visible = false;
                navBarItemCustomerReturnBillOfPeriod.Visible = false;
                navBarGroup6.Visible = false;
                navBarGroup7.Visible = false;
                navBarGroup8.Visible = false;
                navBarGroup9.Visible = false;
                navBarGroup10.Visible = false;
                navBarGroup11.Visible = false;
                navBarGroupProductsTicket.Visible = false;
                navBarGroup54.Visible = false;
                //navBarItem17.Visible = false;
                //navBarItemTransportationStore.Visible = false;
            }
            //حركة تسليم
            else if (UserControl.userType == 23)
            {
                btnStores.Enabled = true;
                btnStores.Checked = true;

                navBarGroup1.Visible = false;
                navBarGroup2.Visible = false;
                navBarGroup3.Visible = false;
                navBarGroup4.Visible = false;
                navBarItemStoreReturn.Visible = false;
                navBarGroup7.Visible = false;
                navBarGroup9.Visible = false;
                navBarGroup10.Visible = false;
                navBarGroup11.Visible = false;
                navBarGroupProductsTicket.Visible = false;
                navBarGroup54.Visible = false;
                navBarGroup12.Visible = false;
                navBarItem17.Visible = false;
                navBarItemTransportationStore.Visible = false;
            }
            labUserName.Text = UserControl.EmpName;
        }

        static void BackupMethod()
        {
            string fbd = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            string text = File.ReadAllText(@"backup.txt");

            countBackup = int.Parse(text);

            if (countBackup <= 3)
            {
                string file = fbd + @"\backup" + (countBackup.ToString()) + ".sql";

                File.WriteAllText(@"backup.txt", (++countBackup).ToString());

                MySqlConnection conn = new MySqlConnection(connection.connectionString);

                MySqlCommand cmd = new MySqlCommand();

                MySqlBackup mb = new MySqlBackup(cmd);

                cmd.Connection = conn;
                conn.Open();
                mb.ExportToFile(file);
                conn.Close();
            }
            else
            {
                string file = fbd + @"\backup1.sql";

                File.WriteAllText(@"backup.txt", "1");

                MySqlConnection conn = new MySqlConnection(connection.connectionString);

                MySqlCommand cmd = new MySqlCommand();

                MySqlBackup mb = new MySqlBackup(cmd);

                cmd.Connection = conn;
                conn.Open();
                mb.ExportToFile(file);
                conn.Close();
            }
        }

        //events
        private void btnStores_ItemClick(object sender, TileItemEventArgs e)
        {
            try
            {
                if (!xtraTabControlMainContainer.TabPages.Contains(xtraTabPageStores))
                {
                    if (index == 0)
                    {
                        xtraTabControlMainContainer.TabPages.Insert(1, StoreTP);
                    }
                    else
                    {
                        xtraTabControlMainContainer.TabPages.Insert(index, StoreTP);
                    }
                    index++;
                }
                xtraTabControlMainContainer.SelectedTabPage = xtraTabControlMainContainer.TabPages[xtraTabControlMainContainer.TabPages.IndexOf(xtraTabPageStores)];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btnSales_ItemClick(object sender, TileItemEventArgs e)
        {
            try
            {
                if (!xtraTabControlMainContainer.TabPages.Contains(xtraTabPageSales))
                {
                    if (index == 0)
                    {
                        xtraTabControlMainContainer.TabPages.Insert(1, SalesTP);
                    }
                    else
                    {
                        xtraTabControlMainContainer.TabPages.Insert(index, SalesTP);
                    }
                    index++;
                }
                xtraTabControlMainContainer.SelectedTabPage = xtraTabControlMainContainer.TabPages[xtraTabControlMainContainer.TabPages.IndexOf(xtraTabPageSales)];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btnHR_ItemClick(object sender, TileItemEventArgs e)
        {
            try
            {
                if (!xtraTabControlMainContainer.TabPages.Contains(xtraTabPageHR))
                {
                    if (index == 0)
                    {
                        xtraTabControlMainContainer.TabPages.Insert(1, HRTP);
                    }
                    else
                    {
                        xtraTabControlMainContainer.TabPages.Insert(index, HRTP);
                    }
                    index++;
                }
                xtraTabControlMainContainer.SelectedTabPage = xtraTabControlMainContainer.TabPages[xtraTabControlMainContainer.TabPages.IndexOf(xtraTabPageHR)];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btnCars_ItemClick(object sender, TileItemEventArgs e)
        {
            try
            {
                if (!xtraTabControlMainContainer.TabPages.Contains(xtraTabPageCars))
                {
                    if (index == 0)
                    {
                        xtraTabControlMainContainer.TabPages.Insert(1, CarsTP);
                    }
                    else
                    {
                        xtraTabControlMainContainer.TabPages.Insert(index, CarsTP);
                    }
                    index++;
                }
                xtraTabControlMainContainer.SelectedTabPage = xtraTabControlMainContainer.TabPages[xtraTabControlMainContainer.TabPages.IndexOf(xtraTabPageCars)];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btnPOS_ItemClick(object sender, TileItemEventArgs e)
        {
            try
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
                xtraTabControlMainContainer.SelectedTabPage = xtraTabControlMainContainer.TabPages[xtraTabControlMainContainer.TabPages.IndexOf(xtraTabPagePOS)];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btnBank_ItemClick(object sender, TileItemEventArgs e)
        {
            try
            {
                if (!xtraTabControlMainContainer.TabPages.Contains(xtraTabPageBank))
                {
                    if (index == 0)
                    {
                        xtraTabControlMainContainer.TabPages.Insert(1, BankTP);
                    }
                    else
                    {
                        xtraTabControlMainContainer.TabPages.Insert(index, BankTP);
                    }
                    index++;
                }
                xtraTabControlMainContainer.SelectedTabPage = xtraTabControlMainContainer.TabPages[xtraTabControlMainContainer.TabPages.IndexOf(xtraTabPageBank)];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btnReception_ItemClick(object sender, TileItemEventArgs e)
        {
            try
            {
                if (!xtraTabControlMainContainer.TabPages.Contains(xtraTabPageReception))
                {
                    if (index == 0)
                    {
                        xtraTabControlMainContainer.TabPages.Insert(1, ReceptionTP);
                    }
                    else
                    {
                        xtraTabControlMainContainer.TabPages.Insert(index, ReceptionTP);
                    }
                    index++;
                }
                xtraTabControlMainContainer.SelectedTabPage = xtraTabControlMainContainer.TabPages[xtraTabControlMainContainer.TabPages.IndexOf(xtraTabPageReception)];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void TIElsha7n_ItemClick(object sender, TileItemEventArgs e)
        {
            try
            {
                if (!xtraTabControlMainContainer.TabPages.Contains(xtraTabPageShipping))
                {
                    if (index == 0)
                    {
                        xtraTabControlMainContainer.TabPages.Insert(1, ShippingTP);
                    }
                    else
                    {
                        xtraTabControlMainContainer.TabPages.Insert(index, ShippingTP);
                    }
                    index++;
                }
                xtraTabControlMainContainer.SelectedTabPage = xtraTabControlMainContainer.TabPages[xtraTabControlMainContainer.TabPages.IndexOf(xtraTabPageShipping)];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void AccountingSystem_ItemClick(object sender, TileItemEventArgs e)
        {
            try
            {
                if (!xtraTabControlMainContainer.TabPages.Contains(xtraTabPageAccounting))
                {
                    if (index == 0)
                    {
                        xtraTabControlMainContainer.TabPages.Insert(1, AccountingTP);
                    }
                    else
                    {
                        xtraTabControlMainContainer.TabPages.Insert(index, AccountingTP);
                    }
                    index++;
                }
                xtraTabControlMainContainer.SelectedTabPage = xtraTabControlMainContainer.TabPages[xtraTabControlMainContainer.TabPages.IndexOf(xtraTabPageAccounting)];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btnCoding_ItemClick(object sender, TileItemEventArgs e)
        {
            try
            {
                if (!xtraTabControlMainContainer.TabPages.Contains(xtraTabPageCoding))
                {
                    if (index == 0)
                    {
                        xtraTabControlMainContainer.TabPages.Insert(1, CodingTP);
                    }
                    else
                    {
                        xtraTabControlMainContainer.TabPages.Insert(index, CodingTP);
                    }
                    index++;
                }
                xtraTabControlMainContainer.SelectedTabPage = xtraTabControlMainContainer.TabPages[xtraTabControlMainContainer.TabPages.IndexOf(xtraTabPageCoding)];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btnPurchases_ItemClick(object sender, TileItemEventArgs e)
        {
            try
            {
                if (!xtraTabControlMainContainer.TabPages.Contains(xtraTabPagePurchases))
                {
                    if (index == 0)
                    {
                        xtraTabControlMainContainer.TabPages.Insert(1, PurchasesTP);
                    }
                    else
                    {
                        xtraTabControlMainContainer.TabPages.Insert(index, PurchasesTP);
                    }
                    index++;
                }
                xtraTabControlMainContainer.SelectedTabPage = xtraTabControlMainContainer.TabPages[xtraTabControlMainContainer.TabPages.IndexOf(xtraTabPagePurchases)];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (!IsTabPageSave())
                {
                    DialogResult dialogResult = MessageBox.Show("There are unsave Pages To you wound close anyway?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dialogResult == DialogResult.Yes)
                    {
                        Application.Exit();
                    }
                    else if (dialogResult == DialogResult.No)
                    {
                        e.Cancel = (dialogResult == DialogResult.No);
                    }
                }
                else
                {
                    Application.Exit();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void xtraTabControlMainContainer_CloseButtonClick(object sender, EventArgs e)
        {
            try
            {
                ClosePageButtonEventArgs arg = e as ClosePageButtonEventArgs;
                XtraTabPage xtraTabPage = (XtraTabPage)arg.Page;
                if (!IsTabPageSave())
                {
                    DialogResult dialogResult = MessageBox.Show("There are unsave Pages To you wound close anyway?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dialogResult == DialogResult.Yes)
                    {
                        xtraTabControlMainContainer.TabPages.Remove(arg.Page as XtraTabPage);
                        index--;
                       // index--;
                        //if (xtraTabPage.Name == "xtraTabPagePurchases")
                        //{
                        //    purchaseFlag = false;
                        //}
                        //else if (xtraTabPage.Name == "xtraTabPageSales")
                        //{
                        //    flag = false;
                        //}
                    }
                    else if (dialogResult == DialogResult.No)
                    {
                    }
                }
                else
                {
                    xtraTabControlMainContainer.TabPages.Remove(arg.Page as XtraTabPage);
                    index--;
                   // index--;
                    //if (xtraTabPage.Name == "xtraTabPagePurchases")
                    //{
                    //    purchaseFlag = false;
                    //}
                    //else if (xtraTabPage.Name == "xtraTabPageSales")
                    //{
                    //    flag = false;
                    //}
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void xtraTabControlContent_Click(object sender, EventArgs e)
        {
            try
            {
                ClosePageButtonEventArgs arg = e as ClosePageButtonEventArgs;
                XtraTabPage xtraTabPage = (XtraTabPage)arg.Page;
                XtraTabControl XtraTabControl = (XtraTabControl)sender;
                if (xtraTabPage.ImageOptions.Image != null)
                {
                    DialogResult dialogResult = MessageBox.Show("Are you sure you want to Close this page without save?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dialogResult == DialogResult.Yes)
                    {
                        XtraTabControl.TabPages.Remove(arg.Page as XtraTabPage);
                    }
                    else if (dialogResult == DialogResult.No)
                    {
                    }
                }
                else
                {
                   XtraTabControl tabControl=(XtraTabControl) xtraTabPage.Parent;
                    tabControl.TabPages.Remove(arg.Page as XtraTabPage);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //functions
        public XtraTabPage getTabPage(XtraTabControl tabControl,string text)
        {
            for (int i = 0; i < tabControl.TabPages.Count; i++)
                if (tabControl.TabPages[i].Text == text)
                {
                    return tabControl.TabPages[i];
                }
            return null;
        }
        public bool IsTabPageSave()
        {
            for (int i = 0; i < xtraTabControlMainContainer.TabPages.Count; i++)
            {
                foreach (Control item in xtraTabControlMainContainer.TabPages[i].Controls)
                {
                    if (item is XtraTabControl)
                    {
                        XtraTabControl item1 = (XtraTabControl)item;
                        for (int j = 0; j < item1.TabPages.Count; j++)
                            if (item1.TabPages[j].ImageOptions.Image != null)
                            {
                                return false;
                            }
                    }
                }
            }
            return true;
        }
        public void restForeColorOfNavBarItem()
        {
            foreach (XtraTabPage tabPage in xtraTabControlMainContainer.TabPages)
            {
                foreach (Control item in tabPage.Controls)
                {
                    if (item is NavBarControl)
                    {
                        foreach (NavBarItem navBar in navBarControl1.Items)
                        {
                            navBar.Appearance.ForeColor = Color.Black;
                            navBar.Appearance.BackColor = Color.LightGray;
                        }
                    }
                }
            }
        }
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                Form form = this as Form;
                form.FormClosing -= MainForm_FormClosing;
                Application.Exit();
                //form.FormClosing += MainForm_FormClosing;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void pictureBoxLogout_Click(object sender, EventArgs e)
        {
            try
            {
                Login loginForm = new Login();
                loginForm.Show();
                this.Hide();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void pictureBoxProfile_Click(object sender, EventArgs e)
        {
            //if (UserControl.userType == 1)
            //{
                try
                {
                    UserUpdate form = new UserUpdate(this);
                    form.ShowDialog();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            //}
        }
        public void userAccess()
        {
            //navBarGroup15.Visible = false;
            navBarGroup16.Visible = false;
            navBarGroup13.Visible = false;
            navBarGroup39.Visible = false;
            //navBarGroup7.Visible = false;
            //navBarGroup8.Visible = false;
            //navBarGroup9.Visible = false;
            //navBarGroup10.Visible = false;
            //navBarGroup11.Visible = false;
            //navBarGroup12.Visible = false;
            //navBarGroup45.Visible = false;

            //navBarGroup14.Visible = false;
            //navBarGroup15.Visible = false;
            //navBarGroup16.Visible = false;
            //navBarGroup17.Visible = false;
            //navBarGroup18.Visible = false;
            //navBarGroup39.Visible = false;

            //navBarGroup43.Visible = false;
            //navBarGroup44.Visible = false;
            //navBarGroupSupplier.Visible = false;
            //navBarGroupLeastQuantity.Visible = false;
        }
        public void userAccessStore()
        {
            navBarGroup1.Visible = false;
            navBarGroup2.Visible = false;
            navBarGroup7.Visible = false;
            navBarGroup9.Visible = false;
            navBarGroup10.Visible = false;
            navBarGroup11.Visible = false;
            navBarGroupProductsTicket.Visible = false;
            navBarGroup17.Visible = false;
            navBarGroup13.Visible = false;
            navBarGroup39.Visible = false;
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            try
            {
                ChangeIP form = new ChangeIP();
                form.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }

    public static class connection
    {
        static string supString = File.ReadAllText("IP_Address.txt");//'35.232.25.153'
        //public static string supString = System.IO.File.ReadAllText(Path.Combine(Properties.Resources.IP_Address, @"IP_Address.txt"));
        public static string connectionString = "SERVER=" + supString + ";DATABASE=cccmaindb;user=root;PASSWORD=A!S#D37;CHARSET=utf8";//SslMode=none";   
        //public static string connectionString = "SERVER=localhost;DATABASE=cccmaindb;user=root;PASSWORD=root;CHARSET=utf8";
    }
}

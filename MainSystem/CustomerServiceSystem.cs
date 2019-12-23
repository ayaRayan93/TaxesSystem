using DevExpress.XtraNavBar;
using DevExpress.XtraTab;
using MainSystem.CustomerService;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MainSystem
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
    }

}

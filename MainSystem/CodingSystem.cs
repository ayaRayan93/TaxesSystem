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
    class CodingSystem
    {
    }
    public partial class MainForm 
    {
        private void btnCodingItems_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            try
            {
                restForeColorOfNavBarItem();
                NavBarItem navBarItem = (NavBarItem)sender;
                navBarItem.Appearance.ForeColor = Color.Blue;

                if (!xtraTabControlCoding.Visible)
                    xtraTabControlCoding.Visible = true;


                XtraTabPage xtraTabPage = getTabPage(xtraTabControlCoding, "تقرير الافرع");
                if (xtraTabPage == null)
                {
                    xtraTabControlCoding.TabPages.Add("تقرير الافرع");
                    xtraTabPage = getTabPage(xtraTabControlCoding, "تقرير الافرع");
                }
                xtraTabPage.Controls.Clear();
                xtraTabControlCoding.SelectedTabPage = xtraTabPage;
                Branch_Report objForm = new Branch_Report();

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
    }
}

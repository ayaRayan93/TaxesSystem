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
        public static XtraTabControl tabControlBranch;

        public void initializeBranch()
        {
            //bankSystem
            tabControlBranch = xtraTabControlCoding;
        }

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
                Branch_Report objForm = new Branch_Report(this);

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

        public void bindRecordBranchForm(Branch_Report form)
        {
            Branch_Record objForm = new Branch_Record(form, xtraTabControlCoding);
            objForm.TopLevel = false;
            XtraTabPage xtraTabPage = getTabPage(xtraTabControlCoding, "اضافة فرع");
            if (xtraTabPage == null)
            {
                xtraTabControlCoding.TabPages.Add("اضافة فرع");
                xtraTabPage = getTabPage(xtraTabControlCoding, "اضافة فرع");
                xtraTabPage.Controls.Clear();
                xtraTabPage.Controls.Add(objForm);
            }
            xtraTabControlCoding.SelectedTabPage = xtraTabPage;
            objForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            objForm.Dock = DockStyle.Fill;
            objForm.Show();
        }

        public void bindUpdateBranchForm(DataRowView sellRow, Branch_Report form)
        {
            Branch_Update objForm = new Branch_Update(sellRow, form, xtraTabControlCoding);
            objForm.TopLevel = false;
            XtraTabPage xtraTabPage = getTabPage(xtraTabControlCoding, "تعديل فرع");
            if (xtraTabPage == null)
            {
                xtraTabControlCoding.TabPages.Add("تعديل فرع");
                xtraTabPage = getTabPage(xtraTabControlCoding, "تعديل فرع");
            }
            xtraTabPage.Controls.Clear();
            xtraTabPage.Controls.Add(objForm);
            xtraTabControlCoding.SelectedTabPage = xtraTabPage;
            objForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            objForm.Dock = DockStyle.Fill;
            objForm.Show();
        }
    }
}

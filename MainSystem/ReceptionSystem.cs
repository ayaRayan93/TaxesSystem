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

namespace MainSystem
{
    class ReceptionSystem
    {
    }
    public partial class MainForm
    {
        MySqlConnection conn;
        public static XtraTabControl tabControlReception;

        public static Delegate_Movement_Copy3 DelegateMovementShow;
        public static Delegates_Attendance DelegateAttendanceShow;
        public static GraphicActiveMonth_Report GraphicActiveMonthReport;
        public static DelegateAbsence_Report DelegateAbsenceReport;
        public static Commitment_Report CommitmentReport;
        public static GraphicDelegateExistence_Report GraphicDelegateExistenceReport;
        public static GraphicDelegateBusy_Report GraphicDelegateBusyReport;
        public static GraphicDelegateAvailable_Report GraphicDelegateAvailableReport;
        public static GraphicDelegateBreak_Report GraphicDelegateBreakReport;
        public static GraphicDelegateAbsence_Report GraphicDelegateAbsenceReport;
        public static DelegateBusyBill_Report DelegateBusyBillReport;
        public static DelegateBillTime_Report DelegateBillTimeReport;

        XtraTabPage tabPageDelegateMovement;
        Panel panelDelegateMovement;
        XtraTabPage tabPageDelegateAttendance;
        Panel panelDelegateAttendance;
        XtraTabPage tabPageGraphicActiveMonthReport;
        Panel panelGraphicActiveMonthReport;
        XtraTabPage tabPageDelegateAbsenceReport;
        Panel panelDelegateAbsenceReport;
        XtraTabPage tabPageCommitmentReport;
        Panel panelCommitmentReport;
        XtraTabPage tabPageGraphicDelegateExistenceReport;
        Panel panelGraphicDelegateExistenceReport;
        XtraTabPage tabPageGraphicDelegateBusyReport;
        Panel panelGraphicDelegateBusyReport;
        XtraTabPage tabPageGraphicDelegateAvailableReport;
        Panel panelGraphicDelegateAvailableReport;
        XtraTabPage tabPageGraphicDelegateBreakReport;
        Panel panelGraphicDelegateBreakReport;
        XtraTabPage tabPageGraphicDelegateAbsenceReport;
        Panel panelGraphicDelegateAbsenceReport;
        XtraTabPage tabPageDelegateBusyBillReport;
        Panel panelDelegateBusyBillReport;
        XtraTabPage tabPageDelegateBillTimeReport;
        Panel panelDelegateBillTimeReport;

     

        public void ReceptionSystem()
        {
            conn = new MySqlConnection(connection.connectionString);

            tabPageDelegateMovement = new XtraTabPage();
            panelDelegateMovement = new Panel();
            tabPageDelegateAttendance = new XtraTabPage();
            panelDelegateAttendance = new Panel();
            tabPageGraphicActiveMonthReport = new XtraTabPage();
            panelGraphicActiveMonthReport = new Panel();
            tabPageDelegateAbsenceReport = new XtraTabPage();
            panelDelegateAbsenceReport = new Panel();
            tabPageCommitmentReport = new XtraTabPage();
            panelCommitmentReport = new Panel();
            tabPageGraphicDelegateExistenceReport = new XtraTabPage();
            panelGraphicDelegateExistenceReport = new Panel();
            tabPageGraphicDelegateBusyReport = new XtraTabPage();
            panelGraphicDelegateBusyReport = new Panel();
            tabPageGraphicDelegateAvailableReport = new XtraTabPage();
            panelGraphicDelegateAvailableReport = new Panel();
            tabPageGraphicDelegateBreakReport = new XtraTabPage();
            panelGraphicDelegateBreakReport = new Panel();
            tabPageGraphicDelegateAbsenceReport = new XtraTabPage();
            panelGraphicDelegateAbsenceReport = new Panel();
            tabPageDelegateBusyBillReport = new XtraTabPage();
            panelDelegateBusyBillReport = new Panel();
            tabPageDelegateBillTimeReport = new XtraTabPage();
            panelDelegateBillTimeReport = new Panel();

            tabControlReception = xtraTabControlReception;
        }

      
        private void xtraTabControlReception_SelectedPageChanged(object sender, TabPageChangedEventArgs e)
        {
            try
            {
                if (xtraTabControlReception.SelectedTabPage == tabPageDelegateBusyBillReport)
                {
                    DelegateBusyBillReport.search();
                }
                //if (xtraTabControlBank.SelectedTabPage == tabPageBankReport)
                //{
                //    formShow.search();
                //}

                //else if (xtraTabControlBank.SelectedTabPage == Bank_Report.MainTabPagePrintingBank)
                //{
                //    if (loadedPrintBank)
                //    {
                //        Bank_Report.bankPrint.display();
                //    }
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Main_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                if (Debugger.IsAttached)
                {
                    Application.Exit();
                }
                else
                {
                    Application.Exit();
                }
                //Environment.Exit(0);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void navBarItemControl_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            try
            {
                restForeColorOfNavBarItem();
                NavBarItem navBarItem = (NavBarItem)sender;
                navBarItem.Appearance.ForeColor = Color.FromArgb(54, 70, 151);

                XtraTabPage xtraTabPage = getTabPage(xtraTabControlReception, "xtraTabPageReception");
                if (xtraTabPage == null)
                {
                    tabPageDelegateMovement.Name = "xtraTabPageReception";
                    tabPageDelegateMovement.Text = "عرض حركة المندوبين";
                    panelDelegateMovement.Name = "panelDelegateMovement";
                    panelDelegateMovement.Dock = DockStyle.Fill;

                    DelegateMovementShow = new Delegate_Movement_Copy3();
                    DelegateMovementShow.Size = new Size(1109, 660);
                    DelegateMovementShow.TopLevel = false;
                    DelegateMovementShow.FormBorderStyle = FormBorderStyle.None;
                    DelegateMovementShow.Dock = DockStyle.Fill;
                }
                panelDelegateMovement.Controls.Clear();
                panelDelegateMovement.Controls.Add(DelegateMovementShow);
                tabPageDelegateMovement.Controls.Add(panelDelegateMovement);
                xtraTabControlReception.TabPages.Add(tabPageDelegateMovement);
                DelegateMovementShow.Show();
                xtraTabControlReception.SelectedTabPage = tabPageDelegateMovement;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void navBarItemDelegateBill_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            //if (UserControl.userType == 1)
            //{
            try
            {
                restForeColorOfNavBarItem();
                NavBarItem navBarItem = (NavBarItem)sender;
                navBarItem.Appearance.ForeColor = Color.FromArgb(54, 70, 151);

                XtraTabPage xtraTabPage = getTabPage(xtraTabControlReception, "tabPageDelegateBillReport");
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
                xtraTabControlReception.TabPages.Add(tabPageDelegateBillReport);
                DelegateBillReport.Show();
                xtraTabControlReception.SelectedTabPage = tabPageDelegateBillReport;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            //}
        }

        private void navBarItemAttendance_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            try
            {
                restForeColorOfNavBarItem();
                NavBarItem navBarItem = (NavBarItem)sender;
                navBarItem.Appearance.ForeColor = Color.FromArgb(54, 70, 151);

                XtraTabPage xtraTabPage = getTabPage(xtraTabControlReception, "xtraTabPageDelegateAttendance");
                if (xtraTabPage == null)
                {
                    tabPageDelegateAttendance.Name = "xtraTabPageDelegateAttendance";
                    tabPageDelegateAttendance.Text = "حضور وانصراف المناديب";
                    panelDelegateAttendance.Name = "panelDelegateAttendance";
                    panelDelegateAttendance.Dock = DockStyle.Fill;

                    DelegateAttendanceShow = new Delegates_Attendance();
                    DelegateAttendanceShow.Size = new Size(1109, 660);
                    DelegateAttendanceShow.TopLevel = false;
                    DelegateAttendanceShow.FormBorderStyle = FormBorderStyle.None;
                    DelegateAttendanceShow.Dock = DockStyle.Fill;
                }
                panelDelegateAttendance.Controls.Clear();
                panelDelegateAttendance.Controls.Add(DelegateAttendanceShow);
                tabPageDelegateAttendance.Controls.Add(panelDelegateAttendance);
                xtraTabControlReception.TabPages.Add(tabPageDelegateAttendance);
                DelegateAttendanceShow.Show();
                xtraTabControlReception.SelectedTabPage = tabPageDelegateAttendance;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        
        private void navBarItemGraphic_Report_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            try
            {
                restForeColorOfNavBarItem();
                NavBarItem navBarItem = (NavBarItem)sender;
                navBarItem.Appearance.ForeColor = Color.FromArgb(54, 70, 151);

                XtraTabPage xtraTabPage = getTabPage(xtraTabControlReception,"tabPageGraphicActiveMonthReport");
                if (xtraTabPage == null)
                {
                    tabPageGraphicActiveMonthReport.Name = "tabPageGraphicActiveMonthReport";
                    tabPageGraphicActiveMonthReport.Text = "تقرير بيانى";
                    panelGraphicActiveMonthReport.Name = "panelGraphicActiveMonthReport";
                    panelGraphicActiveMonthReport.Dock = DockStyle.Fill;

                    GraphicActiveMonthReport = new GraphicActiveMonth_Report();
                    GraphicActiveMonthReport.Size = new Size(1109, 660);
                    GraphicActiveMonthReport.TopLevel = false;
                    GraphicActiveMonthReport.FormBorderStyle = FormBorderStyle.None;
                    GraphicActiveMonthReport.Dock = DockStyle.Fill;
                }
                panelGraphicActiveMonthReport.Controls.Clear();
                panelGraphicActiveMonthReport.Controls.Add(GraphicActiveMonthReport);
                tabPageGraphicActiveMonthReport.Controls.Add(panelGraphicActiveMonthReport);
                xtraTabControlReception.TabPages.Add(tabPageGraphicActiveMonthReport);
                GraphicActiveMonthReport.Show();
                xtraTabControlReception.SelectedTabPage = tabPageGraphicActiveMonthReport;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void navBarItemDelegateAbsence_Report_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            try
            {
                restForeColorOfNavBarItem();
                NavBarItem navBarItem = (NavBarItem)sender;
                navBarItem.Appearance.ForeColor = Color.FromArgb(54, 70, 151);

                XtraTabPage xtraTabPage = getTabPage(xtraTabControlReception,"tabPageDelegateAbsenceReport");
                if (xtraTabPage == null)
                {
                    tabPageDelegateAbsenceReport.Name = "tabPageDelegateAbsenceReport";
                    tabPageDelegateAbsenceReport.Text = "تقرير غياب المندوبين";
                    panelDelegateAbsenceReport.Name = "panelDelegateAbsenceReport";
                    panelDelegateAbsenceReport.Dock = DockStyle.Fill;

                    DelegateAbsenceReport = new DelegateAbsence_Report();
                    DelegateAbsenceReport.Size = new Size(1109, 660);
                    DelegateAbsenceReport.TopLevel = false;
                    DelegateAbsenceReport.FormBorderStyle = FormBorderStyle.None;
                    DelegateAbsenceReport.Dock = DockStyle.Fill;
                }
                panelDelegateAbsenceReport.Controls.Clear();
                panelDelegateAbsenceReport.Controls.Add(DelegateAbsenceReport);
                tabPageDelegateAbsenceReport.Controls.Add(panelDelegateAbsenceReport);
                xtraTabControlReception.TabPages.Add(tabPageDelegateAbsenceReport);
                DelegateAbsenceReport.Show();
                xtraTabControlReception.SelectedTabPage = tabPageDelegateAbsenceReport;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void navBarItemCommitment_Report_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            try
            {
                restForeColorOfNavBarItem();
                NavBarItem navBarItem = (NavBarItem)sender;
                navBarItem.Appearance.ForeColor = Color.FromArgb(54, 70, 151);

                XtraTabPage xtraTabPage = getTabPage(xtraTabControlReception,"tabPageCommitmentReport");
                if (xtraTabPage == null)
                {
                    tabPageCommitmentReport.Name = "tabPageCommitmentReport";
                    tabPageCommitmentReport.Text = "تقرير الالتزام";
                    panelCommitmentReport.Name = "panelCommitmentReport";
                    panelCommitmentReport.Dock = DockStyle.Fill;

                    CommitmentReport = new Commitment_Report();
                    CommitmentReport.Size = new Size(1109, 660);
                    CommitmentReport.TopLevel = false;
                    CommitmentReport.FormBorderStyle = FormBorderStyle.None;
                    CommitmentReport.Dock = DockStyle.Fill;
                }
                panelCommitmentReport.Controls.Clear();
                panelCommitmentReport.Controls.Add(CommitmentReport);
                tabPageCommitmentReport.Controls.Add(panelCommitmentReport);
                xtraTabControlReception.TabPages.Add(tabPageCommitmentReport);
                CommitmentReport.Show();
                xtraTabControlReception.SelectedTabPage = tabPageCommitmentReport;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void navBarItemDelegateExistence_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            try
            {
                restForeColorOfNavBarItem();
                NavBarItem navBarItem = (NavBarItem)sender;
                navBarItem.Appearance.ForeColor = Color.FromArgb(54, 70, 151);

                XtraTabPage xtraTabPage = getTabPage(xtraTabControlReception,"tabPageGraphicDelegateExistenceReport");
                if (xtraTabPage == null)
                {
                    tabPageGraphicDelegateExistenceReport.Name = "tabPageGraphicDelegateExistenceReport";
                    tabPageGraphicDelegateExistenceReport.Text = "تقرير بيانى";
                    panelGraphicDelegateExistenceReport.Name = "panelGraphicDelegateExistenceReport";
                    panelGraphicDelegateExistenceReport.Dock = DockStyle.Fill;

                    GraphicDelegateExistenceReport = new GraphicDelegateExistence_Report();
                    GraphicDelegateExistenceReport.Size = new Size(1109, 660);
                    GraphicDelegateExistenceReport.TopLevel = false;
                    GraphicDelegateExistenceReport.FormBorderStyle = FormBorderStyle.None;
                    GraphicDelegateExistenceReport.Dock = DockStyle.Fill;
                }
                panelGraphicDelegateExistenceReport.Controls.Clear();
                panelGraphicDelegateExistenceReport.Controls.Add(GraphicDelegateExistenceReport);
                tabPageGraphicDelegateExistenceReport.Controls.Add(panelGraphicDelegateExistenceReport);
                xtraTabControlReception.TabPages.Add(tabPageGraphicDelegateExistenceReport);
                GraphicDelegateExistenceReport.Show();
                xtraTabControlReception.SelectedTabPage = tabPageGraphicDelegateExistenceReport;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void navBarItemDelegateBusy_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            try
            {
                restForeColorOfNavBarItem();
                NavBarItem navBarItem = (NavBarItem)sender;
                navBarItem.Appearance.ForeColor = Color.FromArgb(54, 70, 151);

                XtraTabPage xtraTabPage = getTabPage(xtraTabControlReception,"tabPageGraphicDelegateBusyReport");
                if (xtraTabPage == null)
                {
                    tabPageGraphicDelegateBusyReport.Name = "tabPageGraphicDelegateBusyReport";
                    tabPageGraphicDelegateBusyReport.Text = "تقرير بيانى";
                    panelGraphicDelegateBusyReport.Name = "panelGraphicDelegateBusyReport";
                    panelGraphicDelegateBusyReport.Dock = DockStyle.Fill;

                    GraphicDelegateBusyReport = new GraphicDelegateBusy_Report();
                    GraphicDelegateBusyReport.Size = new Size(1109, 660);
                    GraphicDelegateBusyReport.TopLevel = false;
                    GraphicDelegateBusyReport.FormBorderStyle = FormBorderStyle.None;
                    GraphicDelegateBusyReport.Dock = DockStyle.Fill;
                }
                panelGraphicDelegateBusyReport.Controls.Clear();
                panelGraphicDelegateBusyReport.Controls.Add(GraphicDelegateBusyReport);
                tabPageGraphicDelegateBusyReport.Controls.Add(panelGraphicDelegateBusyReport);
                xtraTabControlReception.TabPages.Add(tabPageGraphicDelegateBusyReport);
                GraphicDelegateBusyReport.Show();
                xtraTabControlReception.SelectedTabPage = tabPageGraphicDelegateBusyReport;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void navBarItemDelegateAvailable_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            try
            {
                restForeColorOfNavBarItem();
                NavBarItem navBarItem = (NavBarItem)sender;
                navBarItem.Appearance.ForeColor = Color.FromArgb(54, 70, 151);

                XtraTabPage xtraTabPage = getTabPage(xtraTabControlReception,"tabPageGraphicDelegateAvailableReport");
                if (xtraTabPage == null)
                {
                    tabPageGraphicDelegateAvailableReport.Name = "tabPageGraphicDelegateAvailableReport";
                    tabPageGraphicDelegateAvailableReport.Text = "تقرير بيانى";
                    panelGraphicDelegateAvailableReport.Name = "panelGraphicDelegateAvailableReport";
                    panelGraphicDelegateAvailableReport.Dock = DockStyle.Fill;

                    GraphicDelegateAvailableReport = new GraphicDelegateAvailable_Report();
                    GraphicDelegateAvailableReport.Size = new Size(1109, 660);
                    GraphicDelegateAvailableReport.TopLevel = false;
                    GraphicDelegateAvailableReport.FormBorderStyle = FormBorderStyle.None;
                    GraphicDelegateAvailableReport.Dock = DockStyle.Fill;
                }
                panelGraphicDelegateAvailableReport.Controls.Clear();
                panelGraphicDelegateAvailableReport.Controls.Add(GraphicDelegateAvailableReport);
                tabPageGraphicDelegateAvailableReport.Controls.Add(panelGraphicDelegateAvailableReport);
                xtraTabControlReception.TabPages.Add(tabPageGraphicDelegateAvailableReport);
                GraphicDelegateAvailableReport.Show();
                xtraTabControlReception.SelectedTabPage = tabPageGraphicDelegateAvailableReport;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void navBarItemDelegateBreak_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            try
            {
                restForeColorOfNavBarItem();
                NavBarItem navBarItem = (NavBarItem)sender;
                navBarItem.Appearance.ForeColor = Color.FromArgb(54, 70, 151);

                XtraTabPage xtraTabPage = getTabPage(xtraTabControlReception,"tabPageGraphicDelegateBreakReport");
                if (xtraTabPage == null)
                {
                    tabPageGraphicDelegateBreakReport.Name = "tabPageGraphicDelegateBreakReport";
                    tabPageGraphicDelegateBreakReport.Text = "تقرير بيانى";
                    panelGraphicDelegateBreakReport.Name = "panelGraphicDelegateBreakReport";
                    panelGraphicDelegateBreakReport.Dock = DockStyle.Fill;

                    GraphicDelegateBreakReport = new GraphicDelegateBreak_Report();
                    GraphicDelegateBreakReport.Size = new Size(1109, 660);
                    GraphicDelegateBreakReport.TopLevel = false;
                    GraphicDelegateBreakReport.FormBorderStyle = FormBorderStyle.None;
                    GraphicDelegateBreakReport.Dock = DockStyle.Fill;
                }
                panelGraphicDelegateBreakReport.Controls.Clear();
                panelGraphicDelegateBreakReport.Controls.Add(GraphicDelegateBreakReport);
                tabPageGraphicDelegateBreakReport.Controls.Add(panelGraphicDelegateBreakReport);
                xtraTabControlReception.TabPages.Add(tabPageGraphicDelegateBreakReport);
                GraphicDelegateBreakReport.Show();
                xtraTabControlReception.SelectedTabPage = tabPageGraphicDelegateBreakReport;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void navBarItemDelegateAbsence_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            try
            {
                restForeColorOfNavBarItem();
                NavBarItem navBarItem = (NavBarItem)sender;
                navBarItem.Appearance.ForeColor = Color.FromArgb(54, 70, 151);

                XtraTabPage xtraTabPage = getTabPage(xtraTabControlReception,"tabPageGraphicDelegateAbsenceReport");
                if (xtraTabPage == null)
                {
                    tabPageGraphicDelegateAbsenceReport.Name = "tabPageGraphicDelegateAbsenceReport";
                    tabPageGraphicDelegateAbsenceReport.Text = "تقرير بيانى";
                    panelGraphicDelegateAbsenceReport.Name = "panelGraphicDelegateAbsenceReport";
                    panelGraphicDelegateAbsenceReport.Dock = DockStyle.Fill;

                    GraphicDelegateAbsenceReport = new GraphicDelegateAbsence_Report();
                    GraphicDelegateAbsenceReport.Size = new Size(1109, 660);
                    GraphicDelegateAbsenceReport.TopLevel = false;
                    GraphicDelegateAbsenceReport.FormBorderStyle = FormBorderStyle.None;
                    GraphicDelegateAbsenceReport.Dock = DockStyle.Fill;
                }
                panelGraphicDelegateAbsenceReport.Controls.Clear();
                panelGraphicDelegateAbsenceReport.Controls.Add(GraphicDelegateAbsenceReport);
                tabPageGraphicDelegateAbsenceReport.Controls.Add(panelGraphicDelegateAbsenceReport);
                xtraTabControlReception.TabPages.Add(tabPageGraphicDelegateAbsenceReport);
                GraphicDelegateAbsenceReport.Show();
                xtraTabControlReception.SelectedTabPage = tabPageGraphicDelegateAbsenceReport;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void navBarItemDelegateBusyBill_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            try
            {
                restForeColorOfNavBarItem();
                NavBarItem navBarItem = (NavBarItem)sender;
                navBarItem.Appearance.ForeColor = Color.FromArgb(54, 70, 151);

                XtraTabPage xtraTabPage = getTabPage(xtraTabControlReception,"tabPageDelegateBusyBillReport");
                if (xtraTabPage == null)
                {
                    tabPageDelegateBusyBillReport.Name = "tabPageDelegateBusyBillReport";
                    tabPageDelegateBusyBillReport.Text = "تقرير الفواتير الحالية للمناديب";
                    panelDelegateBusyBillReport.Name = "panelDelegateBusyBillReport";
                    panelDelegateBusyBillReport.Dock = DockStyle.Fill;

                    DelegateBusyBillReport = new DelegateBusyBill_Report();
                    DelegateBusyBillReport.Size = new Size(1109, 660);
                    DelegateBusyBillReport.TopLevel = false;
                    DelegateBusyBillReport.FormBorderStyle = FormBorderStyle.None;
                    DelegateBusyBillReport.Dock = DockStyle.Fill;
                }
                panelDelegateBusyBillReport.Controls.Clear();
                panelDelegateBusyBillReport.Controls.Add(DelegateBusyBillReport);
                tabPageDelegateBusyBillReport.Controls.Add(panelDelegateBusyBillReport);
                xtraTabControlReception.TabPages.Add(tabPageDelegateBusyBillReport);
                DelegateBusyBillReport.Show();
                xtraTabControlReception.SelectedTabPage = tabPageDelegateBusyBillReport;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void navBarItemDelegateBillTime_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            try
            {
                restForeColorOfNavBarItem();
                NavBarItem navBarItem = (NavBarItem)sender;
                navBarItem.Appearance.ForeColor = Color.FromArgb(54, 70, 151);

                XtraTabPage xtraTabPage = getTabPage(xtraTabControlReception,"tabPageDelegateBillTimeReport");
                if (xtraTabPage == null)
                {
                    tabPageDelegateBillTimeReport.Name = "tabPageDelegateBillTimeReport";
                    tabPageDelegateBillTimeReport.Text = "تقرير استغراق المناديب للفواتير";
                    panelDelegateBillTimeReport.Name = "panelDelegateBillTimeReport";
                    panelDelegateBillTimeReport.Dock = DockStyle.Fill;

                    DelegateBillTimeReport = new DelegateBillTime_Report();
                    DelegateBillTimeReport.Size = new Size(1109, 660);
                    DelegateBillTimeReport.TopLevel = false;
                    DelegateBillTimeReport.FormBorderStyle = FormBorderStyle.None;
                    DelegateBillTimeReport.Dock = DockStyle.Fill;
                }
                panelDelegateBillTimeReport.Controls.Clear();
                panelDelegateBillTimeReport.Controls.Add(DelegateBillTimeReport);
                tabPageDelegateBillTimeReport.Controls.Add(panelDelegateBillTimeReport);
                xtraTabControlReception.TabPages.Add(tabPageDelegateBillTimeReport);
                DelegateBillTimeReport.Show();
                xtraTabControlReception.SelectedTabPage = tabPageDelegateBillTimeReport;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void navBarItemSurvayRecord_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            try
            {
                    restForeColorOfNavBarItem();
                    NavBarItem navBarItem = (NavBarItem)sender;
                    navBarItem.Appearance.ForeColor = Color.Blue;
                    if (!xtraTabControlReception.Visible)
                        xtraTabControlReception.Visible = true;

                    XtraTabPage xtraTabPage = getTabPage(xtraTabControlReception, "استبيان");
                    if (xtraTabPage == null)
                    {
                        xtraTabControlReception.TabPages.Add("استبيان");
                        xtraTabPage = getTabPage(xtraTabControlReception, "استبيان");
                    }

                    xtraTabPage.Controls.Clear();
                    xtraTabControlReception.SelectedTabPage = xtraTabPage;

                    CustomerServiceAfterReceived_Report objForm = new CustomerServiceAfterReceived_Report(this);

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

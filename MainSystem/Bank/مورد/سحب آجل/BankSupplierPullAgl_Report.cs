﻿using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraPrinting;
using DevExpress.XtraPrinting.Preview;
using DevExpress.XtraTab;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TaxesSystem
{
    public partial class BankSupplierPullAgl_Report : Form
    {
        MySqlConnection conn;
        MainForm bankMainForm = null;
        XtraTabControl MainTabControlBank;
        
        public static XtraTabPage MainTabPagePrintingPullAgl;
        Panel panelPrintingPullAgl;
        
        public static BankPullAgl_Print bankPrint;

        public static GridControl gridcontrol;

        public BankSupplierPullAgl_Report(MainForm BankMainForm)
        {
            InitializeComponent();
            conn = new MySqlConnection(connection.connectionString);
            bankMainForm = BankMainForm;
            MainTabControlBank = MainForm.tabControlBank;
            
            MainTabPagePrintingPullAgl = new XtraTabPage();
            panelPrintingPullAgl = new Panel();


            gridcontrol = gridControl1;
        }

        private void BankPullAgl_Report_Load(object sender, EventArgs e)
        {
            try
            {
                search();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            conn.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                bankMainForm.bindRecordSupplierPullAglForm(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (((GridView)gridView1).GetSelectedRows().Count() > 0)
                {
                    DataRowView sellRow = (DataRowView)(((GridView)gridView1).GetRow(((GridView)gridView1).GetSelectedRows()[0]));
                    bankMainForm.bindUpdateSupplierPullAglForm(sellRow, this);
                }
                else
                {
                    MessageBox.Show("يجب تحديد العنصر المراد تعديله");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                GridView view = gridView1 as GridView;
                delete(view);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            conn.Close();
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            try
            {
                MainTabPagePrintingPullAgl.Name = "tabPagePrintingPullAgl";
                MainTabPagePrintingPullAgl.Text = "طباعة السحوبات-آجل";
                panelPrintingPullAgl.Name = "panelPrintingPullAgl";
                panelPrintingPullAgl.Dock = DockStyle.Fill;
                
                panelPrintingPullAgl.Controls.Clear();
                bankPrint = new BankPullAgl_Print();
                bankPrint.Size = new Size(1059, 638);
                bankPrint.TopLevel = false;
                bankPrint.FormBorderStyle = FormBorderStyle.None;
                bankPrint.Dock = DockStyle.Fill;
                panelPrintingPullAgl.Controls.Add(bankPrint);
                MainTabPagePrintingPullAgl.Controls.Add(panelPrintingPullAgl);
                MainTabControlBank.TabPages.Add(MainTabPagePrintingPullAgl);
                bankPrint.Show();
                MainTabControlBank.SelectedTabPage = MainTabPagePrintingPullAgl;

                MainForm.loadedPrintPullAgl = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void gridView1_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Delete)
                {
                    //GridView view = sender as GridView;
                    GridView view = gridView1 as GridView;
                    delete(view);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            conn.Close();
        }

        private void toolTipController1_GetActiveObjectInfo(object sender, DevExpress.Utils.ToolTipControllerGetActiveObjectInfoEventArgs e)
        {
            try
            {
                if (e.SelectedControl != gridControl1) return;
                GridHitInfo gridhitinfo = gridView1.CalcHitInfo(e.ControlMousePosition);
                object o = gridhitinfo.HitTest.ToString();
                string text = gridhitinfo.HitTest.ToString();
                e.Info = new DevExpress.Utils.ToolTipControlInfo(o, text);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void gridView1_RowStyle(object sender, RowStyleEventArgs e)
        {
            try
            {
                GridView View = sender as GridView;
                if (e.RowHandle >= 0)
                {
                    string category = View.GetRowCellDisplayText(e.RowHandle, View.Columns["Error"]);

                    int mod = e.RowHandle % 2;
                    //odd=1 , even=0
                    if (mod == 1)
                    {
                        if (category == "1")
                        {
                            e.Appearance.BackColor = Color.Salmon;
                        }
                        else
                        {
                            e.Appearance.BackColor = Color.DarkGray;
                        }
                    }
                    if (mod == 0)
                    {
                        if (category == "1")
                        {
                            e.Appearance.BackColor = Color.Salmon;
                        }
                        else
                        {
                            e.Appearance.BackColor = Color.White;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //functions
        public void search()
        {
            MySqlDataAdapter adapter = new MySqlDataAdapter("SELECT supplier_transitions.SupplierTransition_ID as 'التسلسل',supplier_transitions.Bank_ID,supplier_transitions.Bank_Name as 'الخزينة',supplier_transitions.Amount as 'المبلغ',supplier_transitions.Date as 'التاريخ',supplier_transitions.Payment_Method as 'طريقة الدفع',supplier_transitions.Supplier_ID,supplier.Supplier_Name as 'المورد',supplier_transitions.Payday as 'تاريخ الاستحقاق',supplier_transitions.Check_Number as 'رقم الشيك',supplier_transitions.Operation_Number as 'رقم العملية',supplier_transitions.Data as 'البيان',supplier_transitions.Employee_Name as 'الموظف',supplier_transitions.Error FROM supplier_transitions left join supplier on supplier.Supplier_ID=supplier_transitions.Supplier_ID where supplier_transitions.Transition='سداد' order by supplier_transitions.Date", conn);
            DataSet sourceDataSet = new DataSet();
            adapter.Fill(sourceDataSet);
            gridControl1.DataSource = sourceDataSet.Tables[0];
            gridView1.Columns["Supplier_ID"].Visible = false;
            gridView1.Columns["Error"].Visible = false;
            gridView1.Columns["Bank_ID"].Visible = false;
            gridView1.Columns["الموظف"].Visible = false;
            gridView1.Columns[0].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            for (int i = 1; i < gridView1.Columns.Count; i++)
            {
                gridView1.Columns[i].Width = 150;
            }
        }

        void delete(GridView view)
        {
            MessageBox.Show("لا يمكنك الحذف");
            //int[] selRows = ((GridView)gridControl1.MainView).GetSelectedRows();
            //if (selRows.Length > 0)
            //{
            //    if (MessageBox.Show("هل انت متاكد انك تريد الحذف؟", "تحذير", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
            //        return;

            //    //Microsoft.VisualBasic.Interaction.InputBox("ما هو سبب الحذف?", "Title", "");
            //    Form prompt = new Form()
            //    {
            //        Width = 500,
            //        Height = 220,
            //        FormBorderStyle = FormBorderStyle.FixedDialog,
            //        Text = "",
            //        StartPosition = FormStartPosition.CenterScreen,
            //        MaximizeBox = false,
            //        MinimizeBox = false
            //    };
            //    Label textLabel = new Label() { Left = 340, Top = 20, Text = "ما هو سبب الحذف؟" };
            //    TextBox textBox = new TextBox() { Left = 50, Top = 50, Width = 385, Multiline = true, Height = 80, RightToLeft = RightToLeft };
            //    Button confirmation = new Button() { Text = "تأكيد", Left = 200, Width = 100, Top = 140, DialogResult = DialogResult.OK };
            //    //confirmation.Click += (sender, e) => { prompt.Close(); };
            //    prompt.Controls.Add(textBox);
            //    prompt.Controls.Add(confirmation);
            //    prompt.Controls.Add(textLabel);
            //    prompt.AcceptButton = confirmation;
            //    //MessageBox.Show(prompt.ShowDialog() == DialogResult.OK ? textBox.Text : "");
            //    if (prompt.ShowDialog() == DialogResult.OK)
            //    {
            //        if (textBox.Text != "")
            //        {
            //            conn.Open();
            //            for (int i = 0; i < selRows.Length; i++)
            //            {
            //                DataRowView selRow = (DataRowView)(((GridView)gridControl1.MainView).GetRow(selRows[i]));

            //                string query = "update Transitions set Error=1 where Transition_ID=" + selRow[0].ToString();
            //                MySqlCommand comand = new MySqlCommand(query, conn);
            //                comand.ExecuteNonQuery();

            //                query = "insert into usercontrol (UserControl_UserID,UserControl_TableName,UserControl_Status,UserControl_RecordID,UserControl_Date,UserControl_Reason) values(@UserControl_UserID,@UserControl_TableName,@UserControl_Status,@UserControl_RecordID,@UserControl_Date,@UserControl_Reason)";
            //                comand = new MySqlCommand(query, conn);
            //                comand.Parameters.Add("@UserControl_UserID", MySqlDbType.Int16, 11).Value = UserControl.userID;
            //                comand.Parameters.Add("@UserControl_TableName", MySqlDbType.VarChar, 255).Value = "transitions";
            //                comand.Parameters.Add("@UserControl_Status", MySqlDbType.VarChar, 255).Value = "حذف";
            //                comand.Parameters.Add("@UserControl_RecordID", MySqlDbType.VarChar, 255).Value = selRow[0].ToString();
            //                comand.Parameters.Add("@UserControl_Date", MySqlDbType.DateTime, 0).Value = DateTime.Now;
            //                comand.Parameters.Add("@UserControl_Reason", MySqlDbType.VarChar, 255).Value = textBox.Text;
            //                comand.ExecuteNonQuery();

            //                MySqlCommand com2 = new MySqlCommand("select Bank_Stock from bank where Bank_ID=" + selRow[2].ToString(), conn);
            //                double amount2 = Convert.ToDouble(com2.ExecuteScalar().ToString());
            //                amount2 += Convert.ToDouble(selRow["المبلغ"].ToString());
            //                MySqlCommand com3 = new MySqlCommand("update bank set Bank_Stock=" + amount2 + " where Bank_ID=" + selRow["Bank_ID"].ToString(), conn);
            //                com3.ExecuteNonQuery();

            //                DecreaseClientsAccounts(selRow);

            //                IncreaseClientPaied(selRow);

            //                //query = "update customer_bill set Paid_Status=2 where Client_ID=" + selRow["Client_ID"].ToString();
            //                //comand = new MySqlCommand(query, conn);
            //                //comand.ExecuteNonQuery();
            //            }
            //            conn.Close();
            //            search();
            //        }
            //        else
            //        {
            //            MessageBox.Show("يجب كتابة السبب");
            //        }
            //    }
            //    else
            //    { }

            //    //view.DeleteSelectedRows();
            //}
            //else
            //{
            //    MessageBox.Show("يجب ان تختار عنصر للحذف");
            //}
        }

        /*public void DecreaseClientsAccounts(DataRowView selrow)
        {
            double paidMoney = Convert.ToDouble(selrow[4].ToString());
            string query = "select Money from customer_accounts where Client_ID=" + selrow[7].ToString();
            MySqlCommand com = new MySqlCommand(query, conn);
            if (com.ExecuteScalar() != null)
            {
                double restMoney = Convert.ToDouble(com.ExecuteScalar());
                query = "update customer_accounts set Money=" + (restMoney + paidMoney) + " where Client_ID=" + selrow[7].ToString();
            }
            com = new MySqlCommand(query, conn);
            com.ExecuteNonQuery();
        }

        public void IncreaseClientPaied(DataRowView selrow)
        {
            double paidMoney = Convert.ToDouble(selrow[4].ToString());
            string query = "select Client_Rest_Money from ClientRestMoney where Client_ID=" + selrow[7].ToString();
            MySqlCommand com = new MySqlCommand(query, conn);
            if (com.ExecuteScalar() != null)
            {
                double restMoney = Convert.ToDouble(com.ExecuteScalar());
                query = "update ClientRestMoney set Client_Rest_Money=" + (restMoney - paidMoney) + " where Client_ID=" + selrow[7].ToString();
            }
            com = new MySqlCommand(query, conn);
            com.ExecuteNonQuery();
        }*/
    }
}

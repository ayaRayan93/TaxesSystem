using DevExpress.XtraGrid;
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

namespace MainSystem
{
    public partial class Bank_Report : Form
    {
        MySqlConnection conn;
        XtraTabControl MainTabControlBank;

        public static XtraTabPage MainTabPageAddBank;
        Panel panelAddBank;

        public static XtraTabPage MainTabPageUpdateBank;
        Panel panelUpdateBank;

        public static XtraTabPage MainTabPagePrintingBank;
        Panel panelPrintingBank;

        public static Bank_Print bankPrint;

        public static GridControl gridcontrol;

        public Bank_Report()
        {
            InitializeComponent();
            conn = new MySqlConnection(connection.connectionString);
            MainTabControlBank = MainForm.tabControlBank;

            MainTabPageAddBank = new XtraTabPage();
            panelAddBank = new Panel();

            MainTabPageUpdateBank = new XtraTabPage();
            panelUpdateBank = new Panel();

            MainTabPagePrintingBank = new XtraTabPage();
            panelPrintingBank = new Panel();

            gridcontrol = gridControl1;
        }

        private void Bank_Report_Load(object sender, EventArgs e)
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
                if (MainTabPageAddBank.ImageOptions.Image == null)
                {
                    MainTabPageAddBank.Name = "tabPageAddBank";
                    MainTabPageAddBank.Text = "اضافة بنك";
                    MainTabPageAddBank.ImageOptions.Image = null;
                    panelAddBank.Name = "panelAddBank";
                    panelAddBank.Dock = DockStyle.Fill;
                    
                    panelAddBank.Controls.Clear();

                    Bank_All form = new Bank_All();
                    form.Size = new Size(1059, 638);
                    form.TopLevel = false;
                    form.FormBorderStyle = FormBorderStyle.None;
                    form.Dock = DockStyle.Fill;
                    panelAddBank.Controls.Add(form);
                    MainTabPageAddBank.Controls.Add(panelAddBank);
                    MainTabControlBank.TabPages.Add(MainTabPageAddBank);
                    form.Show();
                    MainTabControlBank.SelectedTabPage = MainTabPageAddBank;
                }
                else
                {
                    if (MessageBox.Show("هناك تعديلات لم تحفظ بعد..هل انت متاكد انك تريد التجاهل؟", "تحذير", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                    {
                        MainTabControlBank.SelectedTabPage = MainTabPageAddBank;
                        return;
                    }
                    else
                    {
                        panelAddBank.Controls.Clear();
                        MainTabPageAddBank.ImageOptions.Image = null;
                        Bank_Record form = new Bank_Record();
                        form.Size = new Size(1059, 638);
                        form.TopLevel = false;
                        form.FormBorderStyle = FormBorderStyle.None;
                        form.Dock = DockStyle.Fill;
                        panelAddBank.Controls.Add(form);
                        form.Show();

                        MainTabControlBank.SelectedTabPage = MainTabPageAddBank;
                    }
                }
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
                DataRowView selRow = (DataRowView)(((GridView)gridControl1.MainView).GetRow(((GridView)gridControl1.MainView).GetSelectedRows()[0]));
              
                if (MainTabPageUpdateBank.ImageOptions.Image == null)
                {
                    
                    if (selRow.Row[0].ToString() != "")
                    {
                        MainTabPageUpdateBank.Name = "tabPageUpdateBank";
                        MainTabPageUpdateBank.Text = "تعديل بنك";
                        MainTabPageUpdateBank.ImageOptions.Image = null;
                        panelUpdateBank.Name = "panelUpdateBank";
                        panelUpdateBank.Dock = DockStyle.Fill;
                        
                        panelUpdateBank.Controls.Clear();
                        //Bank_Update form = new Bank_Update(selRow);
                        //form.Size = new Size(1059, 638);
                        //form.TopLevel = false;
                        //form.FormBorderStyle = FormBorderStyle.None;
                        //form.Dock = DockStyle.Fill;
                        //panelUpdateBank.Controls.Add(form);
                        //MainTabPageUpdateBank.Controls.Add(panelUpdateBank);
                        //MainTabControlBank.TabPages.Add(MainTabPageUpdateBank);
                        //form.Show();
                        //MainTabControlBank.SelectedTabPage = MainTabPageUpdateBank;
                    }
                    else
                    {
                        MessageBox.Show("يجب ان تختار عنصر");
                    }
                }
                else
                {
                    if (MessageBox.Show("هناك تعديلات لم تحفظ بعد..هل انت متاكد انك تريد التجاهل؟", "تحذير", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                    {
                        MainTabControlBank.SelectedTabPage = MainTabPageUpdateBank;
                        return;
                    }
                    else
                    {
                        if (selRow[0].ToString() != "")
                        {
                            //panelUpdateBank.Controls.Clear();
                            //MainTabPageUpdateBank.ImageOptions.Image = null;
                            //Bank_Update form = new Bank_Update(selRow);
                            //form.Size = new Size(1059, 638);
                            //form.TopLevel = false;
                            //form.FormBorderStyle = FormBorderStyle.None;
                            //form.Dock = DockStyle.Fill;
                            //panelUpdateBank.Controls.Add(form);
                            //form.Show();

                            MainTabControlBank.SelectedTabPage = MainTabPageUpdateBank;
                        }
                        else
                        {
                            MessageBox.Show("يجب ان تختار عنصر");
                        }
                    }
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
                MainTabPagePrintingBank.Name = "tabPagePrintingBank";
                MainTabPagePrintingBank.Text = "طباعة البنوك";
                panelPrintingBank.Name = "panelPrintingBank";
                panelPrintingBank.Dock = DockStyle.Fill;
                
                panelPrintingBank.Controls.Clear();
                bankPrint = new Bank_Print();
                bankPrint.Size = new Size(1059, 638);
                bankPrint.TopLevel = false;
                bankPrint.FormBorderStyle = FormBorderStyle.None;
                bankPrint.Dock = DockStyle.Fill;
                panelPrintingBank.Controls.Add(bankPrint);
                MainTabPagePrintingBank.Controls.Add(panelPrintingBank);
                MainTabControlBank.TabPages.Add(MainTabPagePrintingBank);
                bankPrint.Show();
                MainTabControlBank.SelectedTabPage = MainTabPagePrintingBank;

                MainForm.loadedPrintBank = true;
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

        /*private void gridView1_RowStyle(object sender, RowStyleEventArgs e)
        {
            try
            {
                GridView View = sender as GridView;
                if (e.RowHandle >= 0)
                {
                    string category = View.GetRowCellDisplayText(e.RowHandle, View.Columns["Error"]);
                    if (category == "1")
                    {
                        e.Appearance.BackColor = Color.Salmon;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }*/

        //functions
        public void search()
        {
            // left join bank on bank.MainBank_ID=bank_main.MainBank_ID left join bank_Visa on bank.Bank_ID=bank_visa.Bank_ID
            MySqlDataAdapter adapterMain = new MySqlDataAdapter("select bank_main.MainBank_ID as 'التسلسل', MainBank_Type as 'النوع', MainBank_Name as 'الاسم' from bank_main", conn);
            //left join bank_Visa on bank.Bank_ID=bank_visa.Bank_ID
            MySqlDataAdapter adapterBank = new MySqlDataAdapter("select bank.Bank_ID as 'التسلسل', Bank_Name as 'الاسم/رقم الحساب',Branch_Name as 'الفرع',Initial_Balance as 'الرصيد الافتتاحى',Bank_Stock as 'الرصيد', Start_Date as 'تاريخ بدء التعامل',BankAccount_Type as 'نوع الحساب',Bank_Info as 'بيانات اضافية',bank_main.MainBank_ID as 'ID' from bank inner join bank_main on bank.MainBank_ID=bank_main.MainBank_ID ", conn);

            MySqlDataAdapter adapterVisa = new MySqlDataAdapter("select bank_Visa.Visa_ID as 'التسلسل',bank.Bank_Name as 'على بنك',bank_Visa.Machine_ID as 'رقم المكنة',bank_Visa.Bank_ID as 'ID' from bank_Visa inner join bank on bank.Bank_ID=bank_visa.Bank_ID inner join bank_main on bank.MainBank_ID=bank_main.MainBank_ID", conn);

            DataSet sourceDataSet = new DataSet();
            adapterMain.Fill(sourceDataSet, "bank_main");
            adapterBank.Fill(sourceDataSet, "bank");
            adapterVisa.Fill(sourceDataSet, "bank_visa");

            DataColumn keyColumn = sourceDataSet.Tables["bank_main"].Columns["التسلسل"];
            DataColumn foreignKeyColumn = sourceDataSet.Tables["bank"].Columns["ID"];
            sourceDataSet.Relations.Add("البنوك", keyColumn, foreignKeyColumn);

            try
            {
                DataColumn foreignKeyColumn2 = sourceDataSet.Tables["bank"].Columns["التسلسل"];
                DataColumn foreignKeyColumn3 = sourceDataSet.Tables["bank_visa"].Columns["ID"];
                sourceDataSet.Relations.Add("الفيزا", foreignKeyColumn2, foreignKeyColumn3);
            }
            catch { }

            gridControl1.DataSource = sourceDataSet.Tables["bank_main"];
            gridView1.Columns[0].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            /*gridView1.Columns[0].Visible = false;
            gridView1.Columns[3].Visible = false;
            gridView1.Columns[9].Visible = false;*/
            //gridView1.Columns[13].Visible = false;
            /*for (int i = 1; i < gridView1.Columns.Count; i++)
            {
                gridView1.Columns[i].Width = 150;
            }*/
        }

        void delete(GridView view)
        {
            int[] selRows = ((GridView)gridControl1.MainView).GetSelectedRows();
            if (selRows.Length > 0)
            {
                if (MessageBox.Show("هل انت متاكد انك تريد الحذف؟", "تحذير", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                    return;
                
                Form prompt = new Form()
                {
                    Width = 500,
                    Height = 220,
                    FormBorderStyle = FormBorderStyle.FixedDialog,
                    Text = "",
                    StartPosition = FormStartPosition.CenterScreen,
                    MaximizeBox = false,
                    MinimizeBox = false
                };
                Label textLabel = new Label() { Left = 340, Top = 20, Text = "ما هو سبب الحذف؟" };
                TextBox textBox = new TextBox() { Left = 50, Top = 50, Width = 385, Multiline = true, Height = 80, RightToLeft = RightToLeft };
                Button confirmation = new Button() { Text = "تأكيد", Left = 200, Width = 100, Top = 140, DialogResult = DialogResult.OK };
                prompt.Controls.Add(textBox);
                prompt.Controls.Add(confirmation);
                prompt.Controls.Add(textLabel);
                prompt.AcceptButton = confirmation;
                if (prompt.ShowDialog() == DialogResult.OK)
                {
                    if (textBox.Text != "")
                    {
                        conn.Open();
                        for (int i = 0; i < selRows.Length; i++)
                        {
                            DataRowView selRow = (DataRowView)(((GridView)gridControl1.MainView).GetRow(selRows[i]));
                            
                            string query = "delete from bank_main where MainBank_ID=" + selRow[0].ToString();
                            MySqlCommand comand = new MySqlCommand(query, conn);
                            comand.ExecuteNonQuery();

                            query = "insert into usercontrol (UserControl_UserID,UserControl_TableName,UserControl_Status,UserControl_RecordID,UserControl_Date,UserControl_Reason) values(@UserControl_UserID,@UserControl_TableName,@UserControl_Status,@UserControl_RecordID,@UserControl_Date,@UserControl_Reason)";
                            comand = new MySqlCommand(query, conn);
                            comand.Parameters.Add("@UserControl_UserID", MySqlDbType.Int16, 11).Value = UserControl.userID;
                            comand.Parameters.Add("@UserControl_TableName", MySqlDbType.VarChar, 255).Value = "bank_main";
                            comand.Parameters.Add("@UserControl_Status", MySqlDbType.VarChar, 255).Value = "حذف";
                            comand.Parameters.Add("@UserControl_RecordID", MySqlDbType.VarChar, 255).Value = selRow[0].ToString();
                            comand.Parameters.Add("@UserControl_Date", MySqlDbType.DateTime, 0).Value = DateTime.Now;
                            comand.Parameters.Add("@UserControl_Reason", MySqlDbType.VarChar, 255).Value = textBox.Text;
                            comand.ExecuteNonQuery();
                        }
                        conn.Close();
                        search();
                    }
                    else
                    {
                        MessageBox.Show("يجب كتابة السبب");
                    }
                }
                else
                { }
            }
            else
            {
                MessageBox.Show("يجب ان تختار عنصر للحذف");
            }
        }
    }
}

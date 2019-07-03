using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WMPLib;

namespace MainSystem
{
    public partial class Delegate_Movement_Copy2 : Form
    {
        MySqlConnection dbconnection;
        MySqlConnection dbconnection2;
        MySqlConnection dbconnection5;
        MySqlConnection dbconnection6;
        MySqlDataReader dr;
        public static SoundPlayer snd;
        int EmpBranchId = 0;
        int delId = 0;
        string delName = "";
        
        bool loaded = false;

        RepositoryItemButtonEdit repositoryItemButtonEdit0;
        RepositoryItemButtonEdit repositoryItemButtonEdit1_1;
        RepositoryItemButtonEdit repositoryItemButtonEdit2;
        RepositoryItemButtonEdit repositoryItemButtonEdit3;
        RepositoryItemButtonEdit repositoryItemButtonEdit4;

        //List<Delegate> lstDelegate = new List<Delegate>();

        public static WMPLib.WindowsMediaPlayer wplayer = new WMPLib.WindowsMediaPlayer();

        Timer timer = new Timer();
        
        public Delegate_Movement_Copy2()
        {
            InitializeComponent();
            dbconnection = new MySqlConnection(connection.connectionString);
            dbconnection2 = new MySqlConnection(connection.connectionString);
            dbconnection5 = new MySqlConnection(connection.connectionString);
            dbconnection6 = new MySqlConnection(connection.connectionString);

            repositoryItemButtonEdit0 = new RepositoryItemButtonEdit();
            repositoryItemButtonEdit1_1 = new RepositoryItemButtonEdit();
            repositoryItemButtonEdit2 = new RepositoryItemButtonEdit();
            repositoryItemButtonEdit3 = new RepositoryItemButtonEdit();
            repositoryItemButtonEdit4 = new RepositoryItemButtonEdit();

            panel2.AutoScroll = false;
            panel2.VerticalScroll.Enabled = false;
            panel2.VerticalScroll.Visible = false;
            panel2.VerticalScroll.Maximum = 0;
            panel2.AutoScroll = true;

            panel4.AutoScroll = false;
            panel4.VerticalScroll.Enabled = false;
            panel4.VerticalScroll.Visible = false;
            panel4.VerticalScroll.Maximum = 0;
            panel4.AutoScroll = true;

            snd = new SoundPlayer();

            //Calculate the time of the actual work of the delegates
            timer.Interval = 1000 * 60;
            timer.Tick += new EventHandler(loadStatusEvent);
            timer.Start();
        }

        private void DelegateAttend_Load(object sender, EventArgs e)
        {
            try
            {
                repositoryItemButtonEdit0.ContextImage = Properties.Resources.icons8_Delete_Shield_16;
                repositoryItemButtonEdit1_1.ContextImage = Properties.Resources.icons8_Protect_16;
                repositoryItemButtonEdit2.ContextImage = Properties.Resources.icons8_Do_Not_Disturb_16;
                repositoryItemButtonEdit3.ContextImage = Properties.Resources.icons8_Query_16;
                repositoryItemButtonEdit4.ContextImage = Properties.Resources.icons8_Warning_Shield_16;

                LoadGridData();
                loadStatus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }

        private void txtBox_KeyDown(object sender, KeyEventArgs e)
        {
            TextBox txtBox = (TextBox)sender;
            string query;
            MySqlCommand com;
            string Name;
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    if (txtBox.Text != "")
                    {
                        radNewBill.Visible = false;
                        radOldBill.Visible = false;
                        txtBill.Visible = false;
                        labBill.Visible = false;
                        txtBill.Text = "";
                        txtBill.Enabled = false;
                        btnStart.Visible = false;

                        dbconnection.Open();
                        switch (txtBox.Name)
                        {
                            case "txtClientID":
                                query = "select Customer_Name from customer where Customer_ID=" + txtClientID.Text + "";
                                com = new MySqlCommand(query, dbconnection);
                                if (com.ExecuteScalar() != null)
                                {
                                    Name = (string)com.ExecuteScalar();
                                    dbconnection.Close();
                                    comClient.Text = Name;
                                    comClient.SelectedValue = txtClientID.Text;

                                }
                                else
                                {
                                    MessageBox.Show("there is no item with this id");
                                    dbconnection.Close();
                                    return;
                                }
                                break;
                            case "txtCustomerID":
                                query = "select Customer_Name from customer where Customer_ID=" + txtCustomerID.Text + "";
                                com = new MySqlCommand(query, dbconnection);
                                if (com.ExecuteScalar() != null)
                                {
                                    Name = (string)com.ExecuteScalar();
                                    dbconnection.Close();
                                    comEngCon.Text = Name;
                                    comEngCon.SelectedValue = txtCustomerID.Text;
                                }
                                else
                                {
                                    MessageBox.Show("there is no item with this id");
                                    dbconnection.Close();
                                    return;
                                }
                                break;
                        }

                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                dbconnection.Close();
            }
        }

        private void repositoryItemLookUpEdit1_Closed(object sender, DevExpress.XtraEditors.Controls.ClosedEventArgs e)
        {
            gridView1.PostEditor();
        }

        private void repositoryItemLookUpEdit1_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                LookUpEdit edit = sender as LookUpEdit;

                dbconnection2.Open();
                string q = "SELECT * FROM attendance where Delegate_ID=" + gridView1.GetFocusedRowCellValue(colDelegateID).ToString() + " and date(attendance.Attendance_Date)='" + DateTime.Now.Date.ToString("yyyy-MM-dd") + "'";
                MySqlCommand com = new MySqlCommand(q, dbconnection2);
                MySqlDataReader dr = com.ExecuteReader();
                if (dr.HasRows)
                {
                    ResetStatusTime();
                }
                else
                {
                    string query = "insert into attendance(Delegate_ID,Name,Attendance_Date) values(" + gridView1.GetFocusedRowCellValue(colDelegateID).ToString() + ",'" + gridView1.GetFocusedRowCellValue(colDelegate).ToString() + "','" + DateTime.Now.ToString("yyyy-MM-dd") + "')";
                    MySqlCommand command = new MySqlCommand(query, dbconnection);
                    dbconnection.Open();
                    command.ExecuteNonQuery();
                    dbconnection.Close();
                }
                dbconnection2.Close();

                if (Convert.ToInt32(edit.EditValue) == 2)
                {
                    dbconnection.Open();
                    string qq = "update attendance set Status='مشغول' where Delegate_ID=" + gridView1.GetFocusedRowCellValue(colDelegateID).ToString() + " order by Attendance_ID desc limit 1";
                    MySqlCommand comm = new MySqlCommand(qq, dbconnection);
                    comm.ExecuteNonQuery();
                    dbconnection.Close();

                    radNewBill.Visible = true;
                    radOldBill.Visible = true;
                    radOldBill.Checked = true;
                    labBill.Visible = true;
                    txtBill.Visible = true;
                    txtBill.Text = "";
                    txtBill.Enabled = true;
                    btnStart.Visible = true;
                }
                else if (Convert.ToInt32(edit.EditValue) == 1)
                {
                    dbconnection.Open();
                    string qq = "update attendance set Status='متاح' where Delegate_ID=" + gridView1.GetFocusedRowCellValue(colDelegateID).ToString() + " order by Attendance_ID desc limit 1";
                    MySqlCommand comm = new MySqlCommand(qq, dbconnection);
                    comm.ExecuteNonQuery();

                    qq = "delete from dash_delegate_bill where Delegate_ID=" + gridView1.GetFocusedRowCellValue(colDelegateID).ToString();
                    comm = new MySqlCommand(qq, dbconnection);
                    comm.ExecuteNonQuery();
                    dbconnection.Close();

                    radNewBill.Visible = false;
                    radOldBill.Visible = false;
                    txtBill.Visible = false;
                    labBill.Visible = false;
                    txtBill.Text = "";
                    txtBill.Enabled = false;
                    btnStart.Visible = false;
                }
                else if (Convert.ToInt32(edit.EditValue) == 3)
                {
                    dbconnection.Open();
                    string qq = "update attendance set Status='استراحة' where Delegate_ID=" + gridView1.GetFocusedRowCellValue(colDelegateID).ToString() + " order by Attendance_ID desc limit 1";
                    MySqlCommand comm = new MySqlCommand(qq, dbconnection);
                    comm.ExecuteNonQuery();

                    qq = "delete from dash_delegate_bill where Delegate_ID=" + gridView1.GetFocusedRowCellValue(colDelegateID).ToString();
                    comm = new MySqlCommand(qq, dbconnection);
                    comm.ExecuteNonQuery();
                    dbconnection.Close();

                    radNewBill.Visible = false;
                    radOldBill.Visible = false;
                    txtBill.Visible = false;
                    labBill.Visible = false;
                    txtBill.Text = "";
                    txtBill.Enabled = false;
                    btnStart.Visible = false;
                }

                if (gridView1.FocusedRowHandle != (gridView1.RowCount - 1))
                {
                    gridView1.FocusedRowHandle += 1;
                    gridView1.FocusedRowHandle -= 1;
                }
                else
                {
                    gridView1.FocusedRowHandle -= 1;
                    gridView1.FocusedRowHandle += 1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
            dbconnection2.Close();
        }

        private void btnSound_Click(object sender, EventArgs e)
        {
            try
            {
                Stream str = Properties.Resources.sound;
                snd = new SoundPlayer(str);
                snd.LoadAsync();
                snd.LoadCompleted += new AsyncCompletedEventHandler(snd_LoadCompleted);
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void gridView1_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            try
            {
                delId = Convert.ToInt16(gridView1.GetRowCellValue(e.RowHandle, colDelegateID).ToString());
                delName = gridView1.GetRowCellValue(e.RowHandle, colDelegate).ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void radNewBill_Click(object sender, EventArgs e)
        {
            try
            {
                dbconnection.Open();
                int id = 0;
                string query = "select Bill_Number from dash where Branch_ID=" + EmpBranchId + " order by Dash_ID desc limit 1";
                MySqlCommand cmd = new MySqlCommand(query, dbconnection);
                if (cmd.ExecuteScalar() != null)
                {
                    id = Convert.ToInt16(cmd.ExecuteScalar());
                }
                id = id + 1;
                txtBill.Enabled = false;
                txtBill.Text = id.ToString();

                query = "insert into dash (Bill_Number,Branch_ID,Bill_Date) values (@Bill_Number,@Branch_ID,@Bill_Date)";
                MySqlCommand com = new MySqlCommand(query, dbconnection);
                com.Parameters.Add("@Bill_Number", MySqlDbType.Int16).Value = id;
                com.Parameters.Add("@Branch_ID", MySqlDbType.Int16).Value = EmpBranchId;
                com.Parameters.Add("@Bill_Date", MySqlDbType.DateTime).Value = DateTime.Now;
                com.ExecuteNonQuery();

                string q = "select Dash_ID from dash where Branch_ID=" + EmpBranchId + " and Bill_Number=" + id + " order by Dash_ID desc limit 1";
                MySqlCommand command = new MySqlCommand(q, dbconnection);
                int dashId = Convert.ToInt16(command.ExecuteScalar().ToString());

                UserControl.ItemRecord("dash", "اضافة", dashId, DateTime.Now, null, dbconnection);
                dbconnection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void radOldBill_Click(object sender, EventArgs e)
        {
            try
            {
                txtBill.Text = "";
                txtBill.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            try
            {
                startFunc();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }

        private void txtBill_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    startFunc();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                dbconnection.Close();
            }
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            try
            {
                if (loaded && gridView1.RowCount > 0)
                {
                    if (gridView1.GetFocusedRowCellValue(colStatus).ToString() == "2")
                    {
                        radNewBill.Visible = true;
                        radOldBill.Visible = true;
                        radOldBill.Checked = true;
                        txtBill.Visible = true;
                        labBill.Visible = true;
                        txtBill.Text = "";
                        txtBill.Enabled = true;
                        btnStart.Visible = true;
                    }
                    else
                    {
                        radNewBill.Visible = false;
                        radOldBill.Visible = false;
                        radOldBill.Checked = false;
                        radNewBill.Checked = false;
                        txtBill.Visible = false;
                        labBill.Visible = false;
                        txtBill.Text = "";
                        txtBill.Enabled = false;
                        btnStart.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void GridView1_CustomRowCellEdit(object sender, CustomRowCellEditEventArgs e)
        {
            try
            {
                GridView gridView = sender as GridView;
                if (e.Column != gridView.Columns[3])
                    return;

                int status = Convert.ToInt16(gridView.GetRowCellValue(e.RowHandle, colStatus).ToString());
                if (status == 1)
                {
                    e.RepositoryItem = repositoryItemButtonEdit1_1;
                }
                else if (status == 2)
                {
                    e.RepositoryItem = repositoryItemButtonEdit2;
                }
                else if (status == 3)
                {
                    e.RepositoryItem = repositoryItemButtonEdit3;
                }
                else if (status == 4)
                {
                    e.RepositoryItem = repositoryItemButtonEdit4;
                }
                if (status == -1)
                {
                    e.RepositoryItem = repositoryItemButtonEdit0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnReload_Click(object sender, EventArgs e)
        {
            try
            {
                loaded = false;
                LoadGridData();
                loadStatus();
                txtRecomendedBill.Text = "";
                comEngCon.Text = "";
                comClient.Text = "";
                txtClientID.Text = "";
                txtCustomerID.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
            dbconnection5.Close();
        }

        private void radiotype_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton radio = (RadioButton)sender;
            string Customer_Type = radio.Text;

            loaded = false; //this is flag to prevent action of SelectedValueChanged event until datasource fill combobox
            try
            {
                if (Customer_Type == "عميل")
                {
                    labelEng.Visible = false;
                    comEngCon.Visible = false;
                    txtCustomerID.Visible = false;
                    labelClient.Visible = true;
                    comClient.Visible = true;
                    txtClientID.Visible = true;

                    string query = "select distinct customer.Customer_ID,customer.Customer_Name from  customer  where customer.Customer_Type='" + Customer_Type + "'";
                    MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection2);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    comClient.DataSource = dt;
                    comClient.DisplayMember = dt.Columns["Customer_Name"].ToString();
                    comClient.ValueMember = dt.Columns["Customer_ID"].ToString();
                    comClient.Text = "";
                    txtClientID.Text = "";
                    comEngCon.Text = "";
                    txtCustomerID.Text = "";
                }
                else
                {
                    labelEng.Visible = true;
                    comEngCon.Visible = true;
                    txtCustomerID.Visible = true;
                    labelClient.Visible = false;
                    comClient.Visible = false;
                    txtClientID.Visible = false;

                    string query = "select distinct customer.Customer_ID,customer.Customer_Name from  customer  where customer.Customer_Type='" + Customer_Type + "'";
                    MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection2);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    comEngCon.DataSource = dt;
                    comEngCon.DisplayMember = dt.Columns["Customer_Name"].ToString();
                    comEngCon.ValueMember = dt.Columns["Customer_ID"].ToString();
                    comClient.Text = "";
                    txtClientID.Text = "";
                    comEngCon.Text = "";
                    txtCustomerID.Text = "";
                }

                loaded = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection2.Close();
        }

        private void comEngCon_SelectedValueChanged(object sender, EventArgs e)
        {
            if (loaded)
            {
                try
                {
                    txtCustomerID.Text = comEngCon.SelectedValue.ToString();
                    labelClient.Visible = true;
                    comClient.Visible = true;
                    txtClientID.Visible = true;
                    loaded = false;

                    string query = "select distinct customer.Customer_ID,customer.Customer_Name from customer where customer.Customer_ID in (select custmer_client.Client_ID from custmer_client where custmer_client.Customer_ID=" + comEngCon.SelectedValue.ToString() + ")";
                    MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    comClient.DataSource = dt;
                    comClient.DisplayMember = dt.Columns["Customer_Name"].ToString();
                    comClient.ValueMember = dt.Columns["Customer_ID"].ToString();
                    comClient.Text = "";
                    txtClientID.Text = "";

                    radNewBill.Visible = false;
                    radOldBill.Visible = false;
                    txtBill.Visible = false;
                    labBill.Visible = false;
                    txtBill.Text = "";
                    txtBill.Enabled = false;
                    btnStart.Visible = false;

                    loaded = true;

                    gridSearch(comEngCon.SelectedValue.ToString());
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                dbconnection.Close();
            }
        }

        //functions
        void snd_LoadCompleted(object sender, AsyncCompletedEventArgs e)
        {
            if (snd.IsLoadCompleted)
            {
                snd.PlaySync();
                snd.Stop();
                snd.Stream.Close();
                snd.Stream.Dispose();
            }
        }

        private void LoadGridData()
        {
            EmpBranchId = UserControl.EmpBranchID;
            dbconnection.Open();
            MySqlCommand adapter = new MySqlCommand("SELECT delegate.Delegate_ID,delegate.Delegate_Name FROM delegate where delegate.Branch_ID=" + EmpBranchId, dbconnection);
            dr = adapter.ExecuteReader();

            BindingList<GridData> lista = new BindingList<GridData>();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    lista.Add(new GridData() { DelegateId = Convert.ToInt16(dr["Delegate_ID"].ToString()), DelegateName = dr["Delegate_Name"].ToString(), StatusID = "-1" });
                }
                dr.Close();
            }
            gridControl1.DataSource = lista;
            dbconnection.Close();

            if (gridView1.IsLastVisibleRow)
            {
                gridView1.FocusedRowHandle = gridView1.RowCount - 1;
            }

            List<ComboData> lista_combo = new List<ComboData>();
            lista_combo.Add(new ComboData() { StatusId = 1, Status = "متاح" });
            lista_combo.Add(new ComboData() { StatusId = 2, Status = "مشغول" });
            lista_combo.Add(new ComboData() { StatusId = 3, Status = "استراحة" });

            repositoryItemLookUpEdit1.DataSource = lista_combo;
            repositoryItemLookUpEdit1.DisplayMember = "Status";
            repositoryItemLookUpEdit1.ValueMember = "StatusId";

            repositoryItemLookUpEdit1.Columns.Clear();
            repositoryItemLookUpEdit1.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo() { Caption = "الحالة", FieldName = "Status" });
            repositoryItemLookUpEdit1.EditValueChanged += repositoryItemLookUpEdit1_EditValueChanged;
            
            gridView1.Columns[3].ColumnEdit = repositoryItemButtonEdit1;
        }

        void loadStatus()
        {
            for (int i = 0; i < gridView1.RowCount; i++)
            {
                dbconnection.Open();
                TimeSpan stattime = new TimeSpan();
                TimeSpan worktime = new TimeSpan();

                string query = "SELECT attendance.Status FROM attendance where Delegate_ID=" + gridView1.GetRowCellValue(i, colDelegateID).ToString() + " and date(attendance.Attendance_Date)='" + DateTime.Now.Date.ToString("yyyy-MM-dd") + "'";
                MySqlCommand command = new MySqlCommand(query, dbconnection);
                if (command.ExecuteScalar() != null)
                {
                    string status = command.ExecuteScalar().ToString();
                    if (status == "متاح")
                    {
                        gridView1.SelectRow(i);
                        gridView1.SetRowCellValue(i, colStatus, 1);
                        gridView1.Columns[3].ColumnEdit = repositoryItemButtonEdit1;
                    }
                    else if (status == "مشغول")
                    {
                        gridView1.SelectRow(i);
                        gridView1.SetRowCellValue(i, colStatus, 2);
                        gridView1.Columns[3].ColumnEdit = repositoryItemButtonEdit1;
                    }
                    else if (status == "استراحة")
                    {
                        gridView1.SelectRow(i);
                        gridView1.SetRowCellValue(i, colStatus, 3);
                        gridView1.Columns[3].ColumnEdit = repositoryItemButtonEdit1;
                    }

                    dbconnection5.Open();
                    MySqlCommand adapter2 = new MySqlCommand("SELECT cast(Status_Duration as time) as 'Status_Duration',cast(Work_Duration as time) as 'Work_Duration' FROM attendance where attendance.Delegate_ID=" + gridView1.GetRowCellValue(i, colDelegateID).ToString() + " and DATE_FORMAT(attendance.Attendance_Date,'%Y-%m-%d') ='" + DateTime.Now.Date.ToString("yyyy-MM-dd") + "'", dbconnection5);
                    MySqlDataReader dr2 = adapter2.ExecuteReader();
                    if (dr2.HasRows)
                    {
                        while (dr2.Read())
                        {
                            if (TimeSpan.TryParse(dr2["Status_Duration"].ToString(), out stattime))
                            { }
                            if (TimeSpan.TryParse(dr2["Work_Duration"].ToString(), out worktime))
                            { }
                            
                            gridView1.SetRowCellValue(i, colTimer, stattime);
                            gridView1.SetRowCellValue(i, colWorkTimer, worktime);
                        }
                        dr2.Close();
                    }
                    dbconnection5.Close();
                }
                else
                {
                    gridView1.UnselectRow(i);
                    gridView1.SetRowCellValue(i, colStatus, -1);
                    gridView1.Columns[3].ColumnEdit = repositoryItemButtonEdit1;
                    gridView1.SetRowCellValue(i, colTimer, stattime);
                    gridView1.SetRowCellValue(i, colWorkTimer, worktime);
                }
                dbconnection.Close();
            }
            loaded = true;
        }

        public void loadStatusEvent(object sender, EventArgs e)
        {
            loadStatus();
        }

        public void ResetStatusTime()
        {
            dbconnection.Open();
            TimeSpan time1 = new TimeSpan();
            string q = "update attendance set Status_Duration='" + time1 + "' where Delegate_ID=" + gridView1.GetFocusedRowCellValue(colDelegateID).ToString() + " order by Attendance_ID desc limit 1";
            MySqlCommand com = new MySqlCommand(q, dbconnection);
            com.ExecuteNonQuery();
            gridView1.SetFocusedRowCellValue(colTimer, time1);
            dbconnection.Close();
        }

        public void startFunc()
        {
            if (txtBill.Text != "")
            {
                int billNum = -1;
                if (int.TryParse(txtBill.Text, out billNum))
                {
                    dbconnection.Open();

                    string query = "select Dash_ID from dash where Bill_Number=" + billNum + " and Branch_ID=" + EmpBranchId;
                    MySqlCommand command = new MySqlCommand(query, dbconnection);
                    if (command.ExecuteScalar() != null)
                    {
                        string dashId = command.ExecuteScalar().ToString();

                        query = "insert into dash_delegate_bill(Delegate_ID,Delegate_Name,Bill_Number,Branch_ID,Dash_ID) values(" + gridView1.GetFocusedRowCellValue(colDelegateID).ToString() + ",'" + gridView1.GetFocusedRowCellValue(colDelegate).ToString() + "'," + billNum + "," + EmpBranchId + "," + dashId + ")";
                        command = new MySqlCommand(query, dbconnection);
                        command.ExecuteNonQuery();

                        if (radNewBill.Checked)
                        {
                            query = "insert into notdash_delegate_bill(Delegate_ID,Delegate_Name,Bill_Number,Branch_ID,Dash_ID,Date) values(" + gridView1.GetFocusedRowCellValue(colDelegateID).ToString() + ",'" + gridView1.GetFocusedRowCellValue(colDelegate).ToString() + "'," + billNum + "," + EmpBranchId + "," + dashId + ",'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                            command = new MySqlCommand(query, dbconnection);
                            command.ExecuteNonQuery();
                        }
                        else if (radOldBill.Checked)
                        {
                            query = "select NotDash_Delegate_Bill_ID from notdash_delegate_bill where Bill_Number=" + billNum + " and Branch_ID=" + EmpBranchId + " and Delegate_ID=" + gridView1.GetFocusedRowCellValue(colDelegateID).ToString();
                            command = new MySqlCommand(query, dbconnection);
                            if (command.ExecuteScalar() == null)
                            {
                                query = "insert into notdash_delegate_bill(Delegate_ID,Delegate_Name,Bill_Number,Branch_ID,Dash_ID,Date) values(" + gridView1.GetFocusedRowCellValue(colDelegateID).ToString() + ",'" + gridView1.GetFocusedRowCellValue(colDelegate).ToString() + "'," + billNum + "," + EmpBranchId + "," + dashId + ",'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                                command = new MySqlCommand(query, dbconnection);
                                command.ExecuteNonQuery();
                            }
                        }

                        MessageBox.Show("تم");
                    }
                    else
                    {
                        MessageBox.Show("هذه الفاتورة غير موجودة");
                    }
                }
                else
                {
                    MessageBox.Show("رقم الفاتورة يجب ان يكون عدد");
                }
            }
            else
            {
                MessageBox.Show("يجب ادخال رقم الفاتورة");
            }
        }

        private void comClient_SelectedValueChanged(object sender, EventArgs e)
        {
            if (loaded)
            {
                try
                {
                    txtClientID.Text = comClient.SelectedValue.ToString();
                    radNewBill.Visible = false;
                    radOldBill.Visible = false;
                    txtBill.Visible = false;
                    labBill.Visible = false;
                    txtBill.Text = "";
                    txtBill.Enabled = false;
                    btnStart.Visible = false;
                    gridSearch(comClient.SelectedValue.ToString());
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                dbconnection.Close();
                dbconnection6.Close();
            }
        }

        public void gridSearch(string customId)
        {
            dbconnection.Open();
            MySqlCommand adapter = new MySqlCommand("SELECT delegate_customer.Delegate_ID,delegate.Delegate_Name FROM delegate_customer INNER JOIN delegate ON delegate.Delegate_ID = delegate_customer.Delegate_ID where delegate_customer.Customer_ID=" + customId + " and delegate.Branch_ID=" + EmpBranchId, dbconnection);
            dr = adapter.ExecuteReader();

            BindingList<GridData> lista = new BindingList<GridData>();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    lista.Add(new GridData() { DelegateId = Convert.ToInt16(dr["Delegate_ID"].ToString()), DelegateName = dr["Delegate_Name"].ToString(), StatusID = "-1" });
                }
                dr.Close();
                gridControl1.DataSource = lista;

                dbconnection6.Open();
                string query = "SELECT dash.Bill_Number FROM dash where dash.Customer_ID=" + customId + " and dash.Branch_ID=" + EmpBranchId+ " and dash.Confirmed=0 order by dash.Dash_ID desc limit 1";
                MySqlCommand c = new MySqlCommand(query, dbconnection6);
                if (c.ExecuteScalar() != null)
                {
                    txtRecomendedBill.Text = c.ExecuteScalar().ToString();
                }
                else
                {
                    txtRecomendedBill.Text = "";
                }
                dbconnection6.Close();
            }
            else
            {
                gridControl1.DataSource = null;
                txtRecomendedBill.Text = "";
            }
            
            dbconnection.Close();

            if (gridView1.IsLastVisibleRow)
            {
                gridView1.FocusedRowHandle = gridView1.RowCount - 1;
            }
            loaded = false;
            loadStatus();
        }

        private void txtRecomendedBill_Click(object sender, EventArgs e)
        {
            if (txtRecomendedBill.Text != "")
            {
                txtBill.Text = txtRecomendedBill.Text;
            }
        }
    }

    /*public class ComboData
    {
        public int StatusId { get; set; }
        public string Status { get; set; }
    }

    public class GridData
    {
        public int DelegateId { get; set; }
        public string DelegateName { get; set; }
        public string StatusID { get; set; }
        public TimeSpan StatusTimer { get; set; }
        public TimeSpan WorkTimer { get; set; }
        public TimeSpan AttendId { get; set; }
        public TimeSpan DepartureId { get; set; }
    }*/
}

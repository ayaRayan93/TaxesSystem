using DevExpress.XtraGrid.Views.Grid;
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
    public partial class Delegates_Attendance : Form
    {
        MySqlConnection dbconnection;
        bool loaded = false;

        public Delegates_Attendance()
        {
            InitializeComponent();
            dbconnection = new MySqlConnection(connection.connectionString);

            comDelegate.AutoCompleteMode = AutoCompleteMode.Suggest;
            comDelegate.AutoCompleteSource = AutoCompleteSource.ListItems;
        }

        private void Delegates_Attendance_Load(object sender, EventArgs e)
        {
            try
            {
                dbconnection.Open();
                string query = "select * from delegate where Branch_ID=" + UserControl.EmpBranchID + "";
                MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                DataTable dt = new DataTable();
                da.Fill(dt);
                comDelegate.DataSource = dt;
                comDelegate.DisplayMember = dt.Columns["Delegate_Name"].ToString();
                comDelegate.ValueMember = dt.Columns["Delegate_ID"].ToString();
                comDelegate.Text = "";
                dbconnection.Close();

                search();
                loaded = true;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }

        private void comDelegate_SelectedValueChanged(object sender, EventArgs e)
        {
            if (loaded)
            {
                try
                {
                    loaded = false;
                    search();
                    loaded = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                dbconnection.Close();
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                comDelegate.SelectedIndex = -1;
                loaded = false;
                search();
                loaded = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }

        public void search()
        {
            dbconnection.Open();

            string query = "";
            if (comDelegate.Text != "")
            {
                //,attendance.Attendance_Date,attendance.Departure_Date,attendance.Absence_Date  left join attendance on delegate.Delegate_ID=attendance.Delegate_ID
                query = "SELECT delegate.Delegate_ID,delegate.Delegate_Name as 'المندوب','الحالة' FROM delegate where delegate.Delegate_ID=" + comDelegate.SelectedValue.ToString() + " and delegate.Branch_ID=" + UserControl.EmpBranchID;
            }
            else
            {
                query = "SELECT delegate.Delegate_ID,delegate.Delegate_Name as 'المندوب','الحالة' FROM delegate where delegate.Branch_ID=" + UserControl.EmpBranchID;
            }

            MySqlDataAdapter adpt = new MySqlDataAdapter(query, dbconnection);
            DataSet dset = new DataSet();
            adpt.Fill(dset);

            gridControl1.DataSource = dset.Tables[0];
            gridView1.Columns[0].Visible = false;

            if (gridView1.IsLastVisibleRow)
            {
                gridView1.FocusedRowHandle = gridView1.RowCount - 1;
            }

            loadStatus();
        }
        
        void loadStatus()
        {
            for (int i = 0; i < gridView1.RowCount; i++)
            {
                string query = "SELECT attendance.Status FROM attendance where Delegate_ID=" + gridView1.GetRowCellDisplayText(i, "Delegate_ID") + " and (date(attendance.Attendance_Date)='" + DateTime.Now.Date.ToString("yyyy-MM-dd") + "' or attendance.Absence_Date='" + DateTime.Now.Date.ToString("yyyy-MM-dd") + "')";
                MySqlCommand command = new MySqlCommand(query, dbconnection);
                if (command.ExecuteScalar() != null)
                {
                    string status = command.ExecuteScalar().ToString();
                    if (status == "متاح" || status == "مشغول" || status == "استراحة")
                    {
                        gridView1.SelectRow(i);
                        gridView1.SetRowCellValue(i, "الحالة", "حضور");
                    }
                    else if (status == "انصراف")
                    {
                        gridView1.UnselectRow(i);
                        gridView1.SetRowCellValue(i, "الحالة", "انصراف");
                    }
                    else if (status == "غياب")
                    {
                        gridView1.UnselectRow(i);
                        gridView1.SetRowCellValue(i, "الحالة", "غياب");
                    }
                }
                else
                {
                    gridView1.UnselectRow(i);
                    gridView1.SetRowCellValue(i, "الحالة", "");
                }
            }
        }
        
        private void gridView1_SelectionChanged(object sender, DevExpress.Data.SelectionChangedEventArgs e)
        {
            if (loaded)
            {
                try
                {
                    string query = "";
                    dbconnection.Close();
                    if (e.Action == CollectionChangeAction.Add)
                    {
                        if (gridView1.GetFocusedRowCellDisplayText("الحالة") == "انصراف" || gridView1.GetFocusedRowCellDisplayText("الحالة") == "غياب")
                        {
                            GridView view = sender as GridView;
                            view.SelectionChanged -= gridView1_SelectionChanged;
                            gridView1.UnselectRow(gridView1.FocusedRowHandle);
                            view.SelectionChanged += gridView1_SelectionChanged;
                            MessageBox.Show("عفوا لا يمكن تنفيذ هذا الطلب");
                        }
                        else
                        {
                            query = "insert into attendance(Delegate_ID,Name,Attendance_Date) values(" + gridView1.GetRowCellValue(e.ControllerRow, "Delegate_ID").ToString() + ",'" + gridView1.GetRowCellValue(e.ControllerRow, "المندوب").ToString() + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                            gridView1.SetFocusedRowCellValue("الحالة", "حضور");
                            MySqlCommand command = new MySqlCommand(query, dbconnection);
                            dbconnection.Open();
                            command.ExecuteNonQuery();
                        }
                    }
                    else if (e.Action == CollectionChangeAction.Remove)
                    {
                        query = "update attendance set Departure_Date = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' , Status='انصراف' where Delegate_ID=" + gridView1.GetRowCellValue(e.ControllerRow, "Delegate_ID").ToString() + " order by Attendance_ID desc limit 1";
                        gridView1.SetFocusedRowCellValue("الحالة", "انصراف");
                        MySqlCommand command = new MySqlCommand(query, dbconnection);
                        dbconnection.Open();
                        command.ExecuteNonQuery();

                        string qq1 = "select DATE_FORMAT(attendance.Departure_Date,'%Y-%m-%d') as 'التاريخ' from attendance where Delegate_ID=" + gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Delegate_ID").ToString() + " and attendance.Absence_Date is NULL and Error=0 order by Attendance_ID desc limit 1";
                        MySqlCommand comm1 = new MySqlCommand(qq1, dbconnection);
                        string DepartureDate = comm1.ExecuteScalar().ToString();

                        string qq2 = "select DATE_FORMAT(attendance.Attendance_Date,'%Y-%m-%d') as 'التاريخ' from attendance where Delegate_ID=" + gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Delegate_ID").ToString() + " and attendance.Absence_Date is NULL and Error=0 order by Attendance_ID desc limit 1";
                        MySqlCommand comm2 = new MySqlCommand(qq2, dbconnection);
                        string AttendanceDate = comm2.ExecuteScalar().ToString();

                        string qq3 = "";
                        if (DepartureDate == AttendanceDate)
                        {
                            string qq4 = "select SUBTIME(cast(attendance.Departure_Date as time),cast(attendance.Attendance_Date as time)) as 'المدة' from attendance where Delegate_ID=" + gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Delegate_ID").ToString() + " and attendance.Absence_Date is NULL and Error=0 order by Attendance_ID desc limit 1";
                            MySqlCommand comm4 = new MySqlCommand(qq4, dbconnection);
                            TimeSpan tim1 = TimeSpan.Parse(comm4.ExecuteScalar().ToString());

                            qq3 = "update attendance set Attendance_Duration = '" + tim1 + "' where Delegate_ID=" + gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Delegate_ID").ToString() + " order by Attendance_ID desc limit 1";
                            MySqlCommand comm3 = new MySqlCommand(qq3, dbconnection);
                            comm3.ExecuteNonQuery();
                        }
                        else
                        {
                            string qq4 = "select SUBTIME(cast('23:59:59' as time),cast(attendance.Attendance_Date as time)) as 'المدة' from attendance where Delegate_ID=" + gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Delegate_ID").ToString() + " and attendance.Absence_Date is NULL and Error=0 order by Attendance_ID desc limit 1";
                            MySqlCommand comm4 = new MySqlCommand(qq4, dbconnection);
                            TimeSpan tim1 = TimeSpan.Parse(comm4.ExecuteScalar().ToString());

                            qq4 = "select SUBTIME(cast(attendance.Departure_Date as time),cast('00:00:00' as time)) as 'المدة' from attendance where Delegate_ID=" + gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Delegate_ID").ToString() + " and attendance.Absence_Date is NULL and Error=0 order by Attendance_ID desc limit 1";
                            MySqlCommand comm5 = new MySqlCommand(qq4, dbconnection);
                            TimeSpan tim2 = TimeSpan.Parse(comm5.ExecuteScalar().ToString());

                            qq3 = "update attendance set Attendance_Duration = '" + (tim1.Add(tim2)) + "' where Delegate_ID=" + gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Delegate_ID").ToString() + " order by Attendance_ID desc limit 1";
                            MySqlCommand comm3 = new MySqlCommand(qq3, dbconnection);
                            comm3.ExecuteNonQuery();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                dbconnection.Close();
            }
        }

        private void btnAbsence_Click(object sender, EventArgs e)
        {
            try
            {
                dbconnection.Open();
                if (gridView1.GetFocusedRowCellDisplayText("الحالة") != "")
                {
                    MessageBox.Show("عفوا لا يمكن تنفيذ هذا الطلب");
                }
                else
                {
                    string query = "insert into attendance(Delegate_ID,Name,Absence_Date,Status) values(" + gridView1.GetFocusedRowCellDisplayText("Delegate_ID").ToString() + ",'" + gridView1.GetFocusedRowCellDisplayText("المندوب").ToString() + "','" + DateTime.Now.ToString("yyyy-MM-dd") + "','غياب')";
                    MySqlCommand command = new MySqlCommand(query, dbconnection);
                    command.ExecuteNonQuery();
                    gridView1.SetFocusedRowCellValue("الحالة", "غياب");
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }
    }
}

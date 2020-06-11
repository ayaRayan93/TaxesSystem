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
    public partial class Zone_Report : Form
    {
        // Sytem variables
        MySqlConnection dbConnection;
        MySqlCommandBuilder combuilder;
        MySqlDataAdapter adabter;
        DataTable dTable;
        bool loaded = false;
        string zoneName="";
        int zoneId=-1;
        public Zone_Report()
        {
            InitializeComponent();
            dbConnection = new MySqlConnection(connection.connectionString);

            comZone.AutoCompleteMode = AutoCompleteMode.Suggest;
            comZone.AutoCompleteSource = AutoCompleteSource.ListItems;
        }

        //Sytem Events
        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                panelUpdateZone.Visible = false;
                panelAddZone.Visible = false;
                dbConnection.Open();
                updateLists();
                displayAllZones();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbConnection.Close();
        }

        private void btndisplayAll_Click(object sender, EventArgs e)//select all
        {
            try
            {
                dbConnection.Open();
                displayAllZones();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbConnection.Close();
        }

        private void btnAddZone_Click(object sender, EventArgs e)//add New Zone button
        {
            try
            {
                dbConnection.Open();
                addNewZone();//add new Zone
                txtZoneNameAdd.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbConnection.Close();
        }
  
        private void comZone_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                dbConnection.Close();
                dbConnection.Open();
                if (loaded)
                {
                    DataSet sourceDataSet = new DataSet();
                    MySqlDataAdapter adapterZone = new MySqlDataAdapter("select Zone_ID as 'التسلسل', Zone_Name as 'الزون' from zone where Zone_ID=" + comZone.SelectedValue.ToString(), dbConnection);
                    //inner join zone on area.Zone_ID=zone.Zone_ID
                    MySqlDataAdapter adapterArea = new MySqlDataAdapter("SELECT area.Zone_ID as 'التسلسل',area.Area_Name as 'المناطق' FROM area  where area.Zone_ID=" + comZone.SelectedValue.ToString(), dbConnection);

                    adapterZone.Fill(sourceDataSet, "zone");
                    adapterArea.Fill(sourceDataSet, "area");

                    //Set up a master-detail relationship between the DataTables 
                    DataColumn keyColumn = sourceDataSet.Tables["zone"].Columns["التسلسل"];
                    DataColumn foreignKeyColumn = sourceDataSet.Tables["area"].Columns["التسلسل"];
                    sourceDataSet.Relations.Add("المناطق", keyColumn, foreignKeyColumn);

                    gridControl1.DataSource = sourceDataSet.Tables["zone"];
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbConnection.Close();
        }

        private void btnAddNewZone_Click(object sender, EventArgs e)//add new
        {
            panelAddZone.Visible = true;
            panelUpdateZone.Visible = false;
        }

        private void btnUpdateZone_Click(object sender, EventArgs e)//update
        {
            try
            {
                dbConnection.Open();
                string query;
                MySqlCommand com;
                if (txtZoneNameUpdate.Text != "")
                {
                    /*query = " update zone set Zone_Name='"+ txtZoneNameUpdate.Text + "'where Zone_Name='" + zoneName + "'";
                    com = new MySqlCommand(query, dbConnection);
                    com.ExecuteNonQuery();*/
                            
                    foreach (DataRowView item in checkedListBoxDeleteArea.CheckedItems)
                    {
                        string areaId = item["Area_ID"].ToString();
                        query = "update area set Zone_ID=null where Area_ID=" + areaId;
                        com = new MySqlCommand(query, dbConnection);
                        com.ExecuteNonQuery();
                    }
                    
                    foreach (DataRowView item in checkedListBoxAddArea.CheckedItems)
                    {
                        string areaId = item["Area_ID"].ToString();
                        query = "update area set Zone_ID=@Zone_ID where Area_ID=" + areaId;
                        com = new MySqlCommand(query, dbConnection);
                        com.Parameters.Add("@Zone_ID", MySqlDbType.VarChar).Value = zoneId;
                        com.ExecuteNonQuery();
                    }
                    updateLists();
                    displayAllZones();
                    panelUpdateZone.Visible = false;
                    MessageBox.Show("تم التعديل");
                }
                else
                {
                    MessageBox.Show("يجب ادخال الاسم");
                    dbConnection.Close();
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            dbConnection.Close();
        }

        private void gridView1_KeyDown(object sender, KeyEventArgs e)//update/delete in using gridview
        {
            try
            {
                dbConnection.Open();
                if (e.KeyCode == Keys.Enter)
                {
                    gridControl1.Update();
                    combuilder = new MySqlCommandBuilder(adabter);
                    adabter.Update(dTable);
                }
                else if (e.KeyCode == Keys.Delete)
                {
                    DialogResult dialogResult = MessageBox.Show("هل انت متاكد انك تريد الحذف؟", "تحذير", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (dialogResult == DialogResult.Yes)
                    {
                        panelAddZone.Visible = false;
                        panelUpdateZone.Visible = false;
                        delete();
                        //gridView1.DeleteRow(gridView1.GetSelectedRows()[0]);
                        combuilder = new MySqlCommandBuilder(adabter);
                        adabter.Update(dTable);
                    }
                }
                updateLists();
                //gridControl1.DataSource = dTable;
                displayAllZones();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbConnection.Close();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                dbConnection.Open();
                gridControl1.Update();
                combuilder = new MySqlCommandBuilder(adabter);
                adabter.Update(dTable);
                updateLists();
                /*string query = "select Zone_ID as 'رقم متسلسل', Zone_Name as 'اسم الزون',Area_Name as 'اسم المنطقة' from zone";
                MySqlDataAdapter da = new MySqlDataAdapter(query, dbConnection);
                DataTable dt = new DataTable();
                da.Fill(dt);
                gridControl1.DataSource = dt;*/
                displayAllZones();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            dbConnection.Close();
        }

        private void gridView1_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            try
            {
                panelAddZone.Visible = false;
                panelUpdateZone.Visible = true;
                DataRowView row1 = (DataRowView)gridView1.GetRow(e.RowHandle);
                zoneId = Convert.ToInt32(row1[0].ToString());
                txtZoneNameUpdate.Text = row1[1].ToString();
                zoneName = row1[1].ToString();
                //display areas in this zone

                string query = "select * from area where Zone_ID=" + zoneId;
                MySqlDataAdapter da = new MySqlDataAdapter(query, dbConnection);
                DataTable dt = new DataTable();
                da.Fill(dt);
                checkedListBoxDeleteArea.DataSource = dt;
                checkedListBoxDeleteArea.DisplayMember = dt.Columns["Area_Name"].ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            dbConnection.Close();
        }

        //System Functions

        /*public void displayAllZones()
        {
            string query = "select Zone_ID as 'رقم متسلسل', Zone_Name as 'اسم الزون',Area_Name as 'اسم المنطقة' from zone ";
            adabter = new MySqlDataAdapter(query, dbConnection);
            dTable = new DataTable();
            adabter.Fill(dTable);
            gridControl1.DataSource = dTable;
        }*/

        public void displayAllZones()
        {
            DataSet sourceDataSet = new DataSet();
            string qzone = "select Zone_ID as 'التسلسل', Zone_Name as 'الزون' from zone";
            MySqlDataAdapter adapterZone = new MySqlDataAdapter(qzone, dbConnection);
            //inner join zone on area.Zone_ID=zone.Zone_ID
            string qarea = "SELECT area.Zone_ID as 'التسلسل',area.Area_Name as 'المناطق' FROM area ";
            MySqlDataAdapter adapterArea = new MySqlDataAdapter(qarea, dbConnection);

            adapterZone.Fill(sourceDataSet, "zone");
            adapterArea.Fill(sourceDataSet, "area");

            //Set up a master-detail relationship between the DataTables 
            DataColumn keyColumn = sourceDataSet.Tables["zone"].Columns["التسلسل"];
            DataColumn foreignKeyColumn = sourceDataSet.Tables["area"].Columns["التسلسل"];
            sourceDataSet.Relations.Add("المناطق", keyColumn, foreignKeyColumn);

            gridControl1.DataSource = sourceDataSet.Tables["zone"];

            //use them to update gridview and checklistboxs
            MySqlCommand c1 = new MySqlCommand(qzone, dbConnection);
            MySqlCommand c2 = new MySqlCommand(qarea, dbConnection);
            adabter = new MySqlDataAdapter();
            adabter.SelectCommand = c1;
            adabter.SelectCommand = c2;
            dTable = new DataTable();
            adabter.Fill(dTable);
            //dTable = sourceDataSet.Tables["zone"];
        }


        private void updateLists()
        {
            string query = "select * from zone";
            MySqlDataAdapter da = new MySqlDataAdapter(query, dbConnection);
            DataTable dt = new DataTable();
            da.Fill(dt);
            comZone.DataSource = dt;
            comZone.DisplayMember = dt.Columns["Zone_Name"].ToString();
            comZone.ValueMember = dt.Columns["Zone_ID"].ToString();
            comZone.Text = "";
            query = "select distinct * from area where Zone_ID is null";
            da = new MySqlDataAdapter(query, dbConnection);
            dt = new DataTable();
            da.Fill(dt);
            checkedListBoxAreas.DataSource = dt;
            checkedListBoxAreas.DisplayMember = dt.Columns["Area_Name"].ToString();
            checkedListBoxAddArea.DataSource = dt;
            checkedListBoxAddArea.DisplayMember = dt.Columns["Area_Name"].ToString();

            //display areas in this zone
            if (zoneName != "")
            {
                query = "select * from area where Zone_ID=" + zoneId;
                da = new MySqlDataAdapter(query, dbConnection);
                dt = new DataTable();
                da.Fill(dt);
                checkedListBoxDeleteArea.DataSource = dt;
                checkedListBoxDeleteArea.DisplayMember = dt.Columns["Area_Name"].ToString();
            }
            gridControl1.DataSource = null;
            loaded = true;
        }

        private void addNewZone()
        {
            if (txtZoneNameAdd.Text != "")
            {
                bool flag = false;
                string q = "select Zone_Name from zone where Zone_Name='" + txtZoneNameAdd.Text + "'";
                MySqlCommand c = new MySqlCommand(q, dbConnection);
                if (c.ExecuteScalar() == null)
                {
                    int zonId = 0;
                    foreach (DataRowView item in checkedListBoxAreas.CheckedItems)
                    {
                        string areaName = item["Area_Name"].ToString();
                        if (!flag)
                        {
                            flag = true;

                            string query = "insert into zone (Zone_Name) values (@Zone_Name)";
                            MySqlCommand com = new MySqlCommand(query, dbConnection);
                            com.Parameters.Add("@Zone_Name", MySqlDbType.VarChar).Value = txtZoneNameAdd.Text;
                            com.ExecuteNonQuery();
                            
                            q = "select Zone_ID from zone order by Zone_ID desc limit 1";
                            c = new MySqlCommand(q, dbConnection);
                            zonId = Convert.ToInt32(c.ExecuteScalar().ToString());

                            /*q = "select Zone_ID from area where Area_ID="+ item["Area_ID"].ToString();
                            c = new MySqlCommand(q, dbConnection);
                            if (c.ExecuteScalar() == null)
                            {*/
                            query = "update area set Zone_ID=@Zone_ID where area_ID=" + item["Area_ID"].ToString();
                            com = new MySqlCommand(query, dbConnection);
                            com.Parameters.Add("@Zone_ID", MySqlDbType.VarChar).Value = zonId;
                            com.ExecuteNonQuery();
                            /*}
                            else
                            {}*/
                        }
                        else
                        {
                            string query2 = "update area set Zone_ID=@Zone_ID where area_ID=" + item["Area_ID"].ToString();
                            MySqlCommand com = new MySqlCommand(query2, dbConnection);
                            com.Parameters.Add("@Zone_ID", MySqlDbType.VarChar).Value = zonId;
                            com.ExecuteNonQuery();
                        }
                    }
                    if (!flag)
                    {
                        string query = "insert into zone (Zone_Name) values (@Zone_Name)";
                        MySqlCommand com = new MySqlCommand(query, dbConnection);
                        com.Parameters.Add("@Zone_Name", MySqlDbType.VarChar).Value = txtZoneNameAdd.Text;
                        com.ExecuteNonQuery();
                    }

                    UserControl.ItemRecord("zone", "اضافة", zoneId, DateTime.Now, null, dbConnection);

                    updateLists();//update combox1
                    displayAllZones();
                    panelUpdateZone.Visible = false;
                    panelAddZone.Visible = false;
                    MessageBox.Show("تم الاضافة");
                }
                else
                {
                    MessageBox.Show("هذه الزون تم اضافتها من قبل");
                    dbConnection.Close();
                    return;
                }
            }
            else
            {
                MessageBox.Show("يجب ادخال الاسم");
                dbConnection.Close();
                return;
            }
        }

        void delete()
        {
            string query = "delete from zone where Zone_ID=" + zoneId;
            MySqlCommand com = new MySqlCommand(query, dbConnection);
            com.ExecuteNonQuery();
            
            query = "update area set Zone_ID=null where Zone_ID=" + zoneId;
            com = new MySqlCommand(query, dbConnection);
            com.ExecuteNonQuery();

            UserControl.ItemRecord("zone", "حذف", zoneId, DateTime.Now, null, dbConnection);
        }
    }
}

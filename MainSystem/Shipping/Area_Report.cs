using DevExpress.XtraGrid.Views.Grid;
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
    public partial class Area_Report : Form
    {
        // Sytem variables
        MySqlConnection dbConnection;
        MySqlCommandBuilder combuilder;
        MySqlDataAdapter adabter;
        DataTable dTable;
        bool loaded = false;
        int areaId = 0;

        public Area_Report()
        {
            try
            {
                InitializeComponent();
                dbConnection = new MySqlConnection(connection.connectionString);

                comArea.AutoCompleteMode = AutoCompleteMode.Suggest;
                comArea.AutoCompleteSource = AutoCompleteSource.ListItems;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //Sytem Events
        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                dbConnection.Open();
                updateLists();
                displayAllAreas();
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
                displayAllAreas();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbConnection.Close();
        }

        private void comArea_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                dbConnection.Close();
                if (loaded)
                {
                    dbConnection.Open();
                    string query = "select Area_ID as 'تسلسل', Area_Name as 'المنطقة',zone.Zone_Name as 'الزون' from area left join zone on area.Zone_ID=zone.Zone_ID where Area_ID=" + comArea.SelectedValue.ToString();
                    adabter = new MySqlDataAdapter(query, dbConnection);
                    dTable = new DataTable();
                    adabter.Fill(dTable);
                    gridControl1.DataSource = dTable;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbConnection.Close();
        }

        private void btnAddNewArea_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtArea.Text != "")
                {
                    dbConnection.Open();
                    string q = "select Area_Name from area where Area_Name='" + txtArea.Text + "'";
                    MySqlCommand c = new MySqlCommand(q, dbConnection);
                    if (c.ExecuteScalar() == null)
                    {
                        string query = "insert into area (Area_Name) values (@Area_Name)";
                        MySqlCommand com = new MySqlCommand(query, dbConnection);
                        com.Parameters.Add("@Area_Name", MySqlDbType.VarChar).Value = txtArea.Text;
                        com.ExecuteNonQuery();
                        txtArea.Text = "";

                        q = "select Area_ID from area order by Area_ID desc limit 1";
                        c = new MySqlCommand(q, dbConnection);
                        int areId = Convert.ToInt16(c.ExecuteScalar().ToString());

                        UserControl.ItemRecord("area", "اضافة", areId, DateTime.Now, null, dbConnection);
                        updateLists();
                        displayAllAreas();
                        MessageBox.Show("تم الاضافة");
                    }
                    else
                    {
                        MessageBox.Show("هذه المنطقة تم اضافتها من قبل");
                    }
                }
                else
                {
                    MessageBox.Show("يجب ادخال البيانات كاملة");
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
                        delete();
                        //gridView1.DeleteRow(gridView1.GetSelectedRows()[0]);
                        combuilder = new MySqlCommandBuilder(adabter);
                        adabter.Update(dTable);
                    }
                }
                updateLists();
                //gridControl1.DataSource = dTable;
                displayAllAreas();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbConnection.Close();
        }

        private void gridView1_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            try
            {
                DataRowView row1 = (DataRowView)gridView1.GetRow(e.RowHandle);
                areaId = Convert.ToInt16(row1[0].ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            dbConnection.Close();
        }

        //System Functions

        public void displayAllAreas()
        {
            string query = "select Area_ID as 'تسلسل', Area_Name as 'المنطقة',zone.Zone_Name as 'الزون' from area left join zone on area.Zone_ID=zone.Zone_ID";
            adabter = new MySqlDataAdapter(query, dbConnection);
            dTable = new DataTable();
            adabter.Fill(dTable);
            gridControl1.DataSource = dTable;
        }

        private void updateLists()
        {
            string query = "select * from area";
            MySqlDataAdapter da = new MySqlDataAdapter(query, dbConnection);
            DataTable dt = new DataTable();
            da.Fill(dt);
            comArea.DataSource = dt;
            comArea.DisplayMember = dt.Columns["Area_Name"].ToString();
            comArea.ValueMember = dt.Columns["Area_ID"].ToString();
            comArea.Text = "";

            gridControl1.DataSource = null;
            loaded = true;
        }

        void delete()
        {
            string query = "delete from area where Area_ID=" + areaId;
            MySqlCommand com = new MySqlCommand(query, dbConnection);
            com.ExecuteNonQuery();
            UserControl.ItemRecord("area", "حذف", areaId, DateTime.Now, null, dbConnection);
        }
    }
}

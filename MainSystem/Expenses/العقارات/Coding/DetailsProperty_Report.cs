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
    public partial class DetailsProperty_Report : Form
    {
        // Sytem variables
        MySqlConnection dbConnection;
        MySqlCommandBuilder combuilder;
        MySqlDataAdapter adabter;
        DataTable dTable;
        bool loaded = false;
        DataRowView row1 = null;

        public DetailsProperty_Report()
        {
            InitializeComponent();
            dbConnection = new MySqlConnection(connection.connectionString);

            comDetails.AutoCompleteMode = AutoCompleteMode.Suggest;
            comDetails.AutoCompleteSource = AutoCompleteSource.ListItems;
        }

        //Sytem Events
        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                panelUpdateSub.Visible = false;
                panelAddSub.Visible = false;
                dbConnection.Open();
                updateLists();
                displayAllMain();
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
                displayAllMain();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbConnection.Close();
        }

        private void btnAddMain_Click(object sender, EventArgs e)//add New Main Property button
        {
            if (txtDetailsNameAdd.Text != "" && comSubAdd.SelectedValue != null)
            {
                try
                {
                    dbConnection.Open();
                    string q = "select DetailsProperty_Name from property_details where DetailsProperty_Name='" + txtDetailsNameAdd.Text + "' and SubProperty_ID=" + comSubAdd.SelectedValue.ToString();
                    MySqlCommand c = new MySqlCommand(q, dbConnection);
                    if (c.ExecuteScalar() == null)
                    {
                        string query = "insert into property_details (DetailsProperty_Name,SubProperty_ID) values (@DetailsProperty_Name,@SubProperty_ID)";
                        MySqlCommand com = new MySqlCommand(query, dbConnection);
                        com.Parameters.Add("@DetailsProperty_Name", MySqlDbType.VarChar).Value = txtDetailsNameAdd.Text;
                        com.Parameters.Add("@SubProperty_ID", MySqlDbType.VarChar).Value = comSubAdd.SelectedValue.ToString();
                        com.ExecuteNonQuery();
                        txtDetailsNameAdd.Text = "";
                        comSubAdd.SelectedIndex = -1;

                        q = "select DetailsProperty_ID from property_details order by DetailsProperty_ID desc limit 1";
                        c = new MySqlCommand(q, dbConnection);
                        int SubId = Convert.ToInt32(c.ExecuteScalar().ToString());

                        UserControl.ItemRecord("property_details", "اضافة", SubId, DateTime.Now, null, dbConnection);

                        updateLists();//update combox1
                        displayAllMain();
                        panelUpdateSub.Visible = false;
                        panelAddSub.Visible = false;
                    }
                    else
                    {
                        MessageBox.Show("هذا العقار تم اضافته من قبل");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                dbConnection.Close();
            }
            else
            {
                MessageBox.Show("يجب التاكد من البيانات");
            }
        }

        private void comMain_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                dbConnection.Close();
                dbConnection.Open();
                if (loaded)
                {
                    DataSet sourceDataSet = new DataSet();
                    MySqlDataAdapter adapterSub = new MySqlDataAdapter("SELECT property_details.DetailsProperty_ID as 'التسلسل',property_details.DetailsProperty_Name as 'المصروف الفرعى',property_sub.SubProperty_Name as 'المصروف الرئيسى',property_Main.MainProperty_Name as 'العقار',property_details.SubProperty_ID FROM property_Main INNER JOIN property_sub ON property_sub.MainProperty_ID = property_Main.MainProperty_ID INNER JOIN property_details ON property_sub.SubProperty_ID = property_details.SubProperty_ID where property_details.DetailsProperty_ID=" + comDetails.SelectedValue.ToString(), dbConnection);
                    adapterSub.Fill(sourceDataSet);
                    gridControl1.DataSource = sourceDataSet.Tables[0];

                    gridView1.Columns["SubProperty_ID"].Visible = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbConnection.Close();
        }

        private void btnAddNewMain_Click(object sender, EventArgs e)//add new
        {
            panelAddSub.Visible = true;
            panelUpdateSub.Visible = false;

            string query = "select * from property_sub";
            MySqlDataAdapter da = new MySqlDataAdapter(query, dbConnection);
            DataTable dt = new DataTable();
            da.Fill(dt);
            comSubAdd.DataSource = dt;
            comSubAdd.DisplayMember = dt.Columns["SubProperty_Name"].ToString();
            comSubAdd.ValueMember = dt.Columns["SubProperty_ID"].ToString();
            comSubAdd.SelectedIndex = -1;
        }

        private void btnUpdateMain_Click(object sender, EventArgs e)//update
        {
            if (txtDetailsNameUpdate.Text != "" && comSubUpdate.SelectedValue != null)
            {
                try
                {
                    dbConnection.Open();
                    string q = "select DetailsProperty_Name from property_details where DetailsProperty_Name='" + txtDetailsNameUpdate.Text + "' and SubProperty_ID=" + comSubUpdate.SelectedValue.ToString() + " and DetailsProperty_ID<>" + row1[0].ToString();
                    MySqlCommand c = new MySqlCommand(q, dbConnection);
                    if (c.ExecuteScalar() == null)
                    {
                        string query = "update property_details set DetailsProperty_Name=@DetailsProperty_Name,SubProperty_ID=@SubProperty_ID where DetailsProperty_ID=" + row1[0].ToString();
                        MySqlCommand com = new MySqlCommand(query, dbConnection);
                        com.Parameters.Add("@DetailsProperty_Name", MySqlDbType.VarChar).Value = txtDetailsNameUpdate.Text;
                        com.Parameters.Add("@SubProperty_ID", MySqlDbType.VarChar).Value = comSubUpdate.SelectedValue.ToString();
                        com.ExecuteNonQuery();
                        txtDetailsNameUpdate.Text = "";
                        comSubUpdate.SelectedIndex = -1;
                        
                        UserControl.ItemRecord("property_details", "تعديل", Convert.ToInt32(row1[0].ToString()), DateTime.Now, null, dbConnection);

                        updateLists();//update combox1
                        displayAllMain();
                        panelUpdateSub.Visible = false;
                        panelAddSub.Visible = false;
                    }
                    else
                    {
                        MessageBox.Show("هذا العقار تم اضافته من قبل");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                dbConnection.Close();
            }
            else
            {
                MessageBox.Show("يجب التاكد من البيانات");
            }
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
                    if (row1 != null)
                    {
                        DialogResult dialogResult = MessageBox.Show("هل انت متاكد انك تريد الحذف؟", "تحذير", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                        if (dialogResult == DialogResult.Yes)
                        {
                            panelAddSub.Visible = false;
                            panelUpdateSub.Visible = false;
                            delete();
                            combuilder = new MySqlCommandBuilder(adabter);
                            adabter.Update(dTable);
                        }
                    }
                    else
                    {
                        MessageBox.Show("يجب اختيار عقار");
                    }
                }
                updateLists();
                displayAllMain();
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
                displayAllMain();
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
                panelAddSub.Visible = false;
                panelUpdateSub.Visible = true;
                row1 = (DataRowView)gridView1.GetRow(e.RowHandle);
                txtDetailsNameUpdate.Text = row1[1].ToString();

                //display sub in this main Property
                string query = "select * from property_sub";
                MySqlDataAdapter da = new MySqlDataAdapter(query, dbConnection);
                DataTable dt = new DataTable();
                da.Fill(dt);
                comSubUpdate.DataSource = dt;
                comSubUpdate.DisplayMember = dt.Columns["SubProperty_Name"].ToString();
                comSubUpdate.ValueMember = dt.Columns["SubProperty_ID"].ToString();
                comSubUpdate.SelectedValue = row1["SubProperty_ID"].ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            dbConnection.Close();
        }

        //System Functions
        public void displayAllMain()
        {
            string query = "select DetailsProperty_ID as 'التسلسل',DetailsProperty_Name as 'المصروف الفرعى',property_sub.SubProperty_Name as 'المصروف الرئيسى',property_Main.MainProperty_Name as 'العقار',property_details.SubProperty_ID from property_Main INNER JOIN property_sub ON property_sub.MainProperty_ID = property_Main.MainProperty_ID INNER JOIN property_details ON property_sub.SubProperty_ID = property_details.SubProperty_ID";
            adabter = new MySqlDataAdapter(query, dbConnection);
            dTable = new DataTable();
            adabter.Fill(dTable);
            gridControl1.DataSource = dTable;
            gridView1.Columns["SubProperty_ID"].Visible = false;
        }
        
        private void updateLists()
        {
            string query = "select * from property_details";
            MySqlDataAdapter da = new MySqlDataAdapter(query, dbConnection);
            DataTable dt = new DataTable();
            da.Fill(dt);
            comDetails.DataSource = dt;
            comDetails.DisplayMember = dt.Columns["DetailsProperty_Name"].ToString();
            comDetails.ValueMember = dt.Columns["DetailsProperty_ID"].ToString();
            comDetails.Text = "";
            
            gridControl1.DataSource = null;
            loaded = true;
        }

        void delete()
        {
            string query = "delete from property_details where DetailsProperty_ID=" + row1[0].ToString();
            MySqlCommand com = new MySqlCommand(query, dbConnection);
            com.ExecuteNonQuery();

            UserControl.ItemRecord("property_details", "حذف", Convert.ToInt32(row1[0].ToString()), DateTime.Now, null, dbConnection);
        }
    }
}

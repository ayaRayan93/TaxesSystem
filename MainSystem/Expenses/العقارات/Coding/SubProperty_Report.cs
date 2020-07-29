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
    public partial class SubProperty_Report : Form
    {
        // Sytem variables
        MySqlConnection dbConnection;
        MySqlCommandBuilder combuilder;
        MySqlDataAdapter adabter;
        DataTable dTable;
        bool loaded = false;
        DataRowView row1 = null;

        public SubProperty_Report()
        {
            InitializeComponent();
            dbConnection = new MySqlConnection(connection.connectionString);

            comSub.AutoCompleteMode = AutoCompleteMode.Suggest;
            comSub.AutoCompleteSource = AutoCompleteSource.ListItems;
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
            if (txtSubNameAdd.Text != "" && comMainAdd.SelectedValue != null)
            {
                try
                {
                    dbConnection.Open();
                    string q = "select SubProperty_Name from property_sub where SubProperty_Name='" + txtSubNameAdd.Text + "' and MainProperty_ID=" + comMainAdd.SelectedValue.ToString();
                    MySqlCommand c = new MySqlCommand(q, dbConnection);
                    if (c.ExecuteScalar() == null)
                    {
                        string query = "insert into property_sub (SubProperty_Name,MainProperty_ID) values (@SubProperty_Name,@MainProperty_ID)";
                        MySqlCommand com = new MySqlCommand(query, dbConnection);
                        com.Parameters.Add("@SubProperty_Name", MySqlDbType.VarChar).Value = txtSubNameAdd.Text;
                        com.Parameters.Add("@MainProperty_ID", MySqlDbType.VarChar).Value = comMainAdd.SelectedValue.ToString();
                        com.ExecuteNonQuery();
                        txtSubNameAdd.Text = "";
                        comMainAdd.SelectedIndex = -1;

                        q = "select SubProperty_ID from property_sub order by SubProperty_ID desc limit 1";
                        c = new MySqlCommand(q, dbConnection);
                        int SubId = Convert.ToInt32(c.ExecuteScalar().ToString());

                        UserControl.ItemRecord("property_sub", "اضافة", SubId, DateTime.Now, null, dbConnection);

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
                    MySqlDataAdapter adapterSub = new MySqlDataAdapter("SELECT property_sub.SubProperty_ID as 'التسلسل',property_sub.SubProperty_Name as 'المصروف الرئيسى',property_main.MainProperty_Name as 'العقار',property_sub.MainProperty_ID FROM property_sub INNER JOIN property_main ON property_sub.MainProperty_ID = property_main.MainProperty_ID where property_sub.SubProperty_ID=" + comSub.SelectedValue.ToString(), dbConnection);
                    adapterSub.Fill(sourceDataSet);
                    gridControl1.DataSource = sourceDataSet.Tables[0];

                    gridView1.Columns["MainProperty_ID"].Visible = false;
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

            string query = "select * from property_main";
            MySqlDataAdapter da = new MySqlDataAdapter(query, dbConnection);
            DataTable dt = new DataTable();
            da.Fill(dt);
            comMainAdd.DataSource = dt;
            comMainAdd.DisplayMember = dt.Columns["MainProperty_Name"].ToString();
            comMainAdd.ValueMember = dt.Columns["MainProperty_ID"].ToString();
            comMainAdd.SelectedIndex = -1;
        }

        private void btnUpdateMain_Click(object sender, EventArgs e)//update
        {
            if (txtSubNameUpdate.Text != "" && comMainUpdate.SelectedValue != null)
            {
                try
                {
                    dbConnection.Open();
                    string q = "select SubProperty_Name from property_sub where SubProperty_Name='" + txtSubNameUpdate.Text + "' and MainProperty_ID=" + comMainUpdate.SelectedValue.ToString() + " and SubProperty_ID<>" + row1[0].ToString();
                    MySqlCommand c = new MySqlCommand(q, dbConnection);
                    if (c.ExecuteScalar() == null)
                    {
                        string query = "update property_sub set SubProperty_Name=@SubProperty_Name,MainProperty_ID=@MainProperty_ID where SubProperty_ID=" + row1[0].ToString();
                        MySqlCommand com = new MySqlCommand(query, dbConnection);
                        com.Parameters.Add("@SubProperty_Name", MySqlDbType.VarChar).Value = txtSubNameUpdate.Text;
                        com.Parameters.Add("@MainProperty_ID", MySqlDbType.VarChar).Value = comMainUpdate.SelectedValue.ToString();
                        com.ExecuteNonQuery();
                        txtSubNameUpdate.Text = "";
                        comMainUpdate.SelectedIndex = -1;
                        
                        UserControl.ItemRecord("property_sub", "تعديل", Convert.ToInt32(row1[0].ToString()), DateTime.Now, null, dbConnection);

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
                txtSubNameUpdate.Text = row1[1].ToString();

                //display sub in this main Property
                string query = "select * from property_main";
                MySqlDataAdapter da = new MySqlDataAdapter(query, dbConnection);
                DataTable dt = new DataTable();
                da.Fill(dt);
                comMainUpdate.DataSource = dt;
                comMainUpdate.DisplayMember = dt.Columns["MainProperty_Name"].ToString();
                comMainUpdate.ValueMember = dt.Columns["MainProperty_ID"].ToString();
                comMainUpdate.SelectedValue = row1["MainProperty_ID"].ToString();
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
            string query = "select SubProperty_ID as 'التسلسل',SubProperty_Name as 'المصروف الرئيسى',property_main.MainProperty_Name as 'العقار',property_sub.MainProperty_ID from property_sub INNER JOIN property_main ON property_sub.MainProperty_ID = property_main.MainProperty_ID";
            adabter = new MySqlDataAdapter(query, dbConnection);
            dTable = new DataTable();
            adabter.Fill(dTable);
            gridControl1.DataSource = dTable;
            gridView1.Columns["MainProperty_ID"].Visible = false;
        }
        
        private void updateLists()
        {
            string query = "select * from property_sub";
            MySqlDataAdapter da = new MySqlDataAdapter(query, dbConnection);
            DataTable dt = new DataTable();
            da.Fill(dt);
            comSub.DataSource = dt;
            comSub.DisplayMember = dt.Columns["SubProperty_Name"].ToString();
            comSub.ValueMember = dt.Columns["SubProperty_ID"].ToString();
            comSub.Text = "";
            
            gridControl1.DataSource = null;
            loaded = true;
        }

        void delete()
        {
            string query = "delete from property_sub where SubProperty_ID=" + row1[0].ToString();
            MySqlCommand com = new MySqlCommand(query, dbConnection);
            com.ExecuteNonQuery();

            UserControl.ItemRecord("property_sub", "حذف", Convert.ToInt32(row1[0].ToString()), DateTime.Now, null, dbConnection);
        }
    }
}

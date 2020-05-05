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
    public partial class MainProperty_Report : Form
    {
        // Sytem variables
        MySqlConnection dbConnection;
        MySqlCommandBuilder combuilder;
        MySqlDataAdapter adabter;
        DataTable dTable;
        bool loaded = false;
        DataRowView row1 = null;

        public MainProperty_Report()
        {
            try
            {
                InitializeComponent();
                dbConnection = new MySqlConnection(connection.connectionString);

                comMain.AutoCompleteMode = AutoCompleteMode.Suggest;
                comMain.AutoCompleteSource = AutoCompleteSource.ListItems;
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
                panelUpdateMain.Visible = false;
                panelAddMain.Visible = false;
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

        private void comSub_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                dbConnection.Close();
                if (loaded)
                {
                    dbConnection.Open();

                    DataSet sourceDataSet = new DataSet();
                    string qMain = "select MainProperty_ID as 'التسلسل', MainProperty_Name as 'العقار' from property_main where MainProperty_ID=" + comMain.SelectedValue.ToString();
                    MySqlDataAdapter adapterMain = new MySqlDataAdapter(qMain, dbConnection);

                    string qSub = "SELECT property_sub.MainProperty_ID as 'التسلسل',property_sub.SubProperty_Name as 'نوع المصروف' FROM property_sub where MainProperty_ID=" + comMain.SelectedValue.ToString();
                    MySqlDataAdapter adapterSub = new MySqlDataAdapter(qSub, dbConnection);

                    adapterMain.Fill(sourceDataSet, "property_main");
                    adapterSub.Fill(sourceDataSet, "property_sub");

                    //Set up a master-detail relationship between the DataTables 
                    DataColumn keyColumn = sourceDataSet.Tables["property_main"].Columns["التسلسل"];
                    DataColumn foreignKeyColumn = sourceDataSet.Tables["property_sub"].Columns["التسلسل"];
                    sourceDataSet.Relations.Add("انواع المصروفات", keyColumn, foreignKeyColumn);

                    gridControl1.DataSource = sourceDataSet.Tables["property_main"];

                    //use them to update gridview and checklistboxs
                    MySqlCommand c1 = new MySqlCommand(qMain, dbConnection);
                    MySqlCommand c2 = new MySqlCommand(qSub, dbConnection);
                    adabter = new MySqlDataAdapter();
                    adabter.SelectCommand = c1;
                    adabter.SelectCommand = c2;
                    dTable = new DataTable();
                    adabter.Fill(dTable);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbConnection.Close();
        }

        private void btnAddNewSub_Click(object sender, EventArgs e)
        {
            if (txtMainAdd.Text != "")
            {
                try
                {
                    dbConnection.Open();
                    string q = "select MainProperty_Name from property_main where MainProperty_Name='" + txtMainAdd.Text + "'";
                    MySqlCommand c = new MySqlCommand(q, dbConnection);
                    if (c.ExecuteScalar() == null)
                    {
                        string query = "insert into property_main (MainProperty_Name) values (@MainProperty_Name)";
                        MySqlCommand com = new MySqlCommand(query, dbConnection);
                        com.Parameters.Add("@MainProperty_Name", MySqlDbType.VarChar).Value = txtMainAdd.Text;
                        com.ExecuteNonQuery();
                        txtMainAdd.Text = "";

                        q = "select MainProperty_ID from property_main order by MainProperty_ID desc limit 1";
                        c = new MySqlCommand(q, dbConnection);
                        int MainPropertyId = Convert.ToInt32(c.ExecuteScalar().ToString());

                        UserControl.ItemRecord("property_main", "اضافة", MainPropertyId, DateTime.Now, null, dbConnection);
                        updateLists();
                        displayAllMain();
                        panelAddMain.Visible = false;
                        panelUpdateMain.Visible = false;
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
                            panelAddMain.Visible = false;
                            panelUpdateMain.Visible = false;
                            delete();
                            //gridView1.DeleteRow(gridView1.GetSelectedRows()[0]);
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
                //gridControl1.DataSource = dTable;
                displayAllMain();
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
                panelAddMain.Visible = false;
                panelUpdateMain.Visible = true;
                row1 = (DataRowView)gridView1.GetRow(e.RowHandle);
                txtMainUpdate.Text = row1[1].ToString();
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
            DataSet sourceDataSet = new DataSet();
            string qMain = "select MainProperty_ID as 'التسلسل', MainProperty_Name as 'العقار' from property_main";
            MySqlDataAdapter adapterMain = new MySqlDataAdapter(qMain, dbConnection);

            string qSub = "SELECT property_sub.MainProperty_ID as 'التسلسل',property_sub.SubProperty_Name as 'نوع المصروف' FROM property_sub";
            MySqlDataAdapter adapterSub = new MySqlDataAdapter(qSub, dbConnection);

            adapterMain.Fill(sourceDataSet, "property_main");
            adapterSub.Fill(sourceDataSet, "property_sub");

            //Set up a master-detail relationship between the DataTables 
            DataColumn keyColumn = sourceDataSet.Tables["property_main"].Columns["التسلسل"];
            DataColumn foreignKeyColumn = sourceDataSet.Tables["property_sub"].Columns["التسلسل"];
            sourceDataSet.Relations.Add("انواع المصروفات", keyColumn, foreignKeyColumn);

            gridControl1.DataSource = sourceDataSet.Tables["property_main"];

            //use them to update gridview and checklistboxs
            MySqlCommand c1 = new MySqlCommand(qMain, dbConnection);
            MySqlCommand c2 = new MySqlCommand(qSub, dbConnection);
            adabter = new MySqlDataAdapter();
            adabter.SelectCommand = c1;
            adabter.SelectCommand = c2;
            dTable = new DataTable();
            adabter.Fill(dTable);
        }
        
        private void updateLists()
        {
            string query = "select * from property_main";
            MySqlDataAdapter da = new MySqlDataAdapter(query, dbConnection);
            DataTable dt = new DataTable();
            da.Fill(dt);
            comMain.DataSource = dt;
            comMain.DisplayMember = dt.Columns["MainProperty_Name"].ToString();
            comMain.ValueMember = dt.Columns["MainProperty_ID"].ToString();
            comMain.Text = "";

            gridControl1.DataSource = null;
            loaded = true;
        }

        void delete()
        {
            string query = "delete from property_main where MainProperty_ID=" + row1[0].ToString();
            MySqlCommand com = new MySqlCommand(query, dbConnection);
            com.ExecuteNonQuery();
            
            UserControl.ItemRecord("property_main", "حذف", Convert.ToInt32(row1[0].ToString()), DateTime.Now, null, dbConnection);
        }

        private void btnAddMain_Click(object sender, EventArgs e)
        {
            panelAddMain.Visible = true;
            panelUpdateMain.Visible = false;
        }

        private void btnMainUpdate_Click(object sender, EventArgs e)
        {
            if (txtMainUpdate.Text != "")
            {
                try
                {
                    dbConnection.Open();
                    string q = "select MainProperty_Name from property_main where MainProperty_Name='" + txtMainUpdate.Text + "' and MainProperty_ID<>" + row1[0].ToString();
                    MySqlCommand c = new MySqlCommand(q, dbConnection);
                    if (c.ExecuteScalar() == null)
                    {
                        string query = "update property_main set MainProperty_Name=@MainProperty_Name where MainProperty_ID=" + row1[0].ToString();
                        MySqlCommand com = new MySqlCommand(query, dbConnection);
                        com.Parameters.Add("@MainProperty_Name", MySqlDbType.VarChar).Value = txtMainUpdate.Text;
                        com.ExecuteNonQuery();
                        txtMainUpdate.Text = "";

                        UserControl.ItemRecord("property_main", "تعديل", Convert.ToInt32(row1[0].ToString()), DateTime.Now, null, dbConnection);
                        updateLists();
                        displayAllMain();
                        panelAddMain.Visible = false;
                        panelUpdateMain.Visible = false;
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
    }
}

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
    public partial class MainExpenses_Report : Form
    {
        // Sytem variables
        MySqlConnection dbConnection;
        MySqlCommandBuilder combuilder;
        MySqlDataAdapter adabter;
        DataTable dTable;
        bool loaded = false;
        string mainName = "";
        int mainId = -1;

        public MainExpenses_Report()
        {
            InitializeComponent();
            dbConnection = new MySqlConnection(connection.connectionString);

            comMain.AutoCompleteMode = AutoCompleteMode.Suggest;
            comMain.AutoCompleteSource = AutoCompleteSource.ListItems;
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

        private void btnAddMain_Click(object sender, EventArgs e)//add New Main Expense button
        {
            try
            {
                dbConnection.Open();
                addNewMain();//add new Main Expense
                txtMainNameAdd.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbConnection.Close();
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
                    MySqlDataAdapter adapterMain = new MySqlDataAdapter("select MainExpense_ID as 'التسلسل', MainExpense_Name as 'الزون' from expense_main where MainExpense_ID=" + comMain.SelectedValue.ToString(), dbConnection);
                    MySqlDataAdapter adapterSub = new MySqlDataAdapter("SELECT expense_sub.MainExpense_ID as 'التسلسل',expense_sub.SubExpense_Name as 'المناطق' FROM expense_sub where expense_sub.MainExpense_ID=" + comMain.SelectedValue.ToString(), dbConnection);

                    adapterMain.Fill(sourceDataSet, "main");
                    adapterSub.Fill(sourceDataSet, "sub");

                    //Set up a master-detail relationship between the DataTables 
                    DataColumn keyColumn = sourceDataSet.Tables["main"].Columns["التسلسل"];
                    DataColumn foreignKeyColumn = sourceDataSet.Tables["sub"].Columns["التسلسل"];
                    sourceDataSet.Relations.Add("المناطق", keyColumn, foreignKeyColumn);

                    gridControl1.DataSource = sourceDataSet.Tables["main"];
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
            panelAddMain.Visible = true;
            panelUpdateMain.Visible = false;
        }

        private void btnUpdateMain_Click(object sender, EventArgs e)//update
        {
            try
            {
                dbConnection.Open();
                string query;
                MySqlCommand com;
                if (txtMainNameUpdate.Text != "")
                {
                    foreach (DataRowView item in checkedListBoxDeleteSub.CheckedItems)
                    {
                        string subId = item["SubExpense_ID"].ToString();
                        query = "update expense_sub set MainExpense_ID=null where SubExpense_ID=" + subId;
                        com = new MySqlCommand(query, dbConnection);
                        com.ExecuteNonQuery();
                    }

                    foreach (DataRowView item in checkedListBoxAddSub.CheckedItems)
                    {
                        string subId = item["SubExpense_ID"].ToString();
                        query = "update expense_sub set MainExpense_ID=@MainExpense_ID where SubExpense_ID=" + subId;
                        com = new MySqlCommand(query, dbConnection);
                        com.Parameters.Add("@MainExpense_ID", MySqlDbType.VarChar).Value = mainId;
                        com.ExecuteNonQuery();
                    }
                    updateLists();
                    displayAllMain();
                    panelUpdateMain.Visible = false;
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
                        panelAddMain.Visible = false;
                        panelUpdateMain.Visible = false;
                        delete();
                        combuilder = new MySqlCommandBuilder(adabter);
                        adabter.Update(dTable);
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
                panelAddMain.Visible = false;
                panelUpdateMain.Visible = true;
                DataRowView row1 = (DataRowView)gridView1.GetRow(e.RowHandle);
                mainId = Convert.ToInt32(row1[0].ToString());
                txtMainNameUpdate.Text = row1[1].ToString();
                mainName = row1[1].ToString();

                //display sub in this main expense
                string query = "select * from expense_sub where MainExpense_ID=" + mainId;
                MySqlDataAdapter da = new MySqlDataAdapter(query, dbConnection);
                DataTable dt = new DataTable();
                da.Fill(dt);
                checkedListBoxDeleteSub.DataSource = dt;
                checkedListBoxDeleteSub.DisplayMember = dt.Columns["SubExpense_Name"].ToString();
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
            string qMain = "select MainExpense_ID as 'التسلسل', MainExpense_Name as 'الزون' from expense_main";
            MySqlDataAdapter adapterMain = new MySqlDataAdapter(qMain, dbConnection);

            string qSub = "SELECT expense_sub.MainExpense_ID as 'التسلسل',expense_sub.SubExpense_Name as 'المناطق' FROM expense_sub";
            MySqlDataAdapter adapterSub = new MySqlDataAdapter(qSub, dbConnection);

            adapterMain.Fill(sourceDataSet, "expense_main");
            adapterSub.Fill(sourceDataSet, "expense_sub");

            //Set up a master-detail relationship between the DataTables 
            DataColumn keyColumn = sourceDataSet.Tables["expense_main"].Columns["التسلسل"];
            DataColumn foreignKeyColumn = sourceDataSet.Tables["expense_sub"].Columns["التسلسل"];
            sourceDataSet.Relations.Add("المناطق", keyColumn, foreignKeyColumn);

            gridControl1.DataSource = sourceDataSet.Tables["expense_main"];

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
            string query = "select * from expense_main";
            MySqlDataAdapter da = new MySqlDataAdapter(query, dbConnection);
            DataTable dt = new DataTable();
            da.Fill(dt);
            comMain.DataSource = dt;
            comMain.DisplayMember = dt.Columns["MainExpense_Name"].ToString();
            comMain.ValueMember = dt.Columns["MainExpense_ID"].ToString();
            comMain.Text = "";
            query = "select distinct * from expense_sub where MainExpense_ID is null";
            da = new MySqlDataAdapter(query, dbConnection);
            dt = new DataTable();
            da.Fill(dt);
            checkedListBoxSub.DataSource = dt;
            checkedListBoxSub.DisplayMember = dt.Columns["SubExpense_Name"].ToString();
            checkedListBoxAddSub.DataSource = dt;
            checkedListBoxAddSub.DisplayMember = dt.Columns["SubExpense_Name"].ToString();

            //display expense sub in this main expense
            if (mainName != "")
            {
                query = "select * from expense_sub where MainExpense_ID=" + mainId;
                da = new MySqlDataAdapter(query, dbConnection);
                dt = new DataTable();
                da.Fill(dt);
                checkedListBoxDeleteSub.DataSource = dt;
                checkedListBoxDeleteSub.DisplayMember = dt.Columns["SubExpense_Name"].ToString();
            }
            gridControl1.DataSource = null;
            loaded = true;
        }

        private void addNewMain()
        {
            if (txtMainNameAdd.Text != "")
            {
                bool flag = false;
                string q = "select MainExpense_Name from expense_main where MainExpense_Name='" + txtMainNameAdd.Text + "'";
                MySqlCommand c = new MySqlCommand(q, dbConnection);
                if (c.ExecuteScalar() == null)
                {
                    int zonId = 0;
                    foreach (DataRowView item in checkedListBoxSub.CheckedItems)
                    {
                        string SubName = item["SubExpense_Name"].ToString();
                        if (!flag)
                        {
                            flag = true;

                            string query = "insert into expense_main (MainExpense_Name) values (@MainExpense_Name)";
                            MySqlCommand com = new MySqlCommand(query, dbConnection);
                            com.Parameters.Add("@MainExpense_Name", MySqlDbType.VarChar).Value = txtMainNameAdd.Text;
                            com.ExecuteNonQuery();

                            q = "select MainExpense_ID from expense_main order by MainExpense_ID desc limit 1";
                            c = new MySqlCommand(q, dbConnection);
                            zonId = Convert.ToInt32(c.ExecuteScalar().ToString());
                            
                            query = "update expense_sub set MainExpense_ID=@MainExpense_ID where SubExpense_ID=" + item["SubExpense_ID"].ToString();
                            com = new MySqlCommand(query, dbConnection);
                            com.Parameters.Add("@MainExpense_ID", MySqlDbType.VarChar).Value = zonId;
                            com.ExecuteNonQuery();
                        }
                        else
                        {
                            string query2 = "update expense_sub set MainExpense_ID=@MainExpense_ID where SubExpense_ID=" + item["SubExpense_ID"].ToString();
                            MySqlCommand com = new MySqlCommand(query2, dbConnection);
                            com.Parameters.Add("@MainExpense_ID", MySqlDbType.VarChar).Value = zonId;
                            com.ExecuteNonQuery();
                        }
                    }
                    if (!flag)
                    {
                        string query = "insert into expense_main (MainExpense_Name) values (@MainExpense_Name)";
                        MySqlCommand com = new MySqlCommand(query, dbConnection);
                        com.Parameters.Add("@MainExpense_Name", MySqlDbType.VarChar).Value = txtMainNameAdd.Text;
                        com.ExecuteNonQuery();
                    }

                    UserControl.ItemRecord("expense_main", "اضافة", mainId, DateTime.Now, null, dbConnection);

                    updateLists();//update combox1
                    displayAllMain();
                    panelUpdateMain.Visible = false;
                    panelAddMain.Visible = false;
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
            string query = "delete from expense_main where MainExpense_ID=" + mainId;
            MySqlCommand com = new MySqlCommand(query, dbConnection);
            com.ExecuteNonQuery();

            query = "update expense_sub set MainExpense_ID=null where MainExpense_ID=" + mainId;
            com = new MySqlCommand(query, dbConnection);
            com.ExecuteNonQuery();

            UserControl.ItemRecord("expense_main", "حذف", mainId, DateTime.Now, null, dbConnection);
        }
    }
}

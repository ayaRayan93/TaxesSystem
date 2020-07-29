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
    public partial class SubExpenses_Report : Form
    {
        // Sytem variables
        MySqlConnection dbConnection;
        MySqlCommandBuilder combuilder;
        MySqlDataAdapter adabter;
        DataTable dTable;
        bool loaded = false;
        DataRowView row1 = null;

        public SubExpenses_Report()
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

        private void btnAddMain_Click(object sender, EventArgs e)//add New Main Expense button
        {
            if (txtSubNameAdd.Text != "" && comMainAdd.SelectedValue != null)
            {
                try
                {
                    dbConnection.Open();
                    string q = "select SubExpense_Name from expense_sub where SubExpense_Name='" + txtSubNameAdd.Text + "' and MainExpense_ID=" + comMainAdd.SelectedValue.ToString();
                    MySqlCommand c = new MySqlCommand(q, dbConnection);
                    if (c.ExecuteScalar() == null)
                    {
                        string query = "insert into expense_sub (SubExpense_Name,MainExpense_ID) values (@SubExpense_Name,@MainExpense_ID)";
                        MySqlCommand com = new MySqlCommand(query, dbConnection);
                        com.Parameters.Add("@SubExpense_Name", MySqlDbType.VarChar).Value = txtSubNameAdd.Text;
                        com.Parameters.Add("@MainExpense_ID", MySqlDbType.VarChar).Value = comMainAdd.SelectedValue.ToString();
                        com.ExecuteNonQuery();
                        txtSubNameAdd.Text = "";
                        comMainAdd.SelectedIndex = -1;

                        q = "select SubExpense_ID from expense_sub order by SubExpense_ID desc limit 1";
                        c = new MySqlCommand(q, dbConnection);
                        int SubId = Convert.ToInt32(c.ExecuteScalar().ToString());

                        UserControl.ItemRecord("expense_sub", "اضافة", SubId, DateTime.Now, null, dbConnection);

                        updateLists();//update combox1
                        displayAllMain();
                        panelUpdateSub.Visible = false;
                        panelAddSub.Visible = false;
                    }
                    else
                    {
                        MessageBox.Show("هذا المصروف تم اضافته من قبل");
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
                    MySqlDataAdapter adapterSub = new MySqlDataAdapter("SELECT expense_sub.SubExpense_ID as 'التسلسل',expense_sub.SubExpense_Name as 'المصروف الفرعى',expense_main.MainExpense_Name as 'المصروف الرئيسى',expense_sub.MainExpense_ID FROM expense_sub INNER JOIN expense_main ON expense_sub.MainExpense_ID = expense_main.MainExpense_ID where expense_sub.SubExpense_ID=" + comSub.SelectedValue.ToString(), dbConnection);
                    adapterSub.Fill(sourceDataSet);
                    gridControl1.DataSource = sourceDataSet.Tables[0];

                    gridView1.Columns["MainExpense_ID"].Visible = false;
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

            string query = "select * from expense_main";
            MySqlDataAdapter da = new MySqlDataAdapter(query, dbConnection);
            DataTable dt = new DataTable();
            da.Fill(dt);
            comMainAdd.DataSource = dt;
            comMainAdd.DisplayMember = dt.Columns["MainExpense_Name"].ToString();
            comMainAdd.ValueMember = dt.Columns["MainExpense_ID"].ToString();
            comMainAdd.SelectedIndex = -1;
        }

        private void btnUpdateMain_Click(object sender, EventArgs e)//update
        {
            if (txtSubNameUpdate.Text != "" && comMainUpdate.SelectedValue != null)
            {
                try
                {
                    dbConnection.Open();
                    string q = "select SubExpense_Name from expense_sub where SubExpense_Name='" + txtSubNameUpdate.Text + "' and MainExpense_ID=" + comMainUpdate.SelectedValue.ToString() + " and SubExpense_ID<>" + row1[0].ToString();
                    MySqlCommand c = new MySqlCommand(q, dbConnection);
                    if (c.ExecuteScalar() == null)
                    {
                        string query = "update expense_sub set SubExpense_Name=@SubExpense_Name,MainExpense_ID=@MainExpense_ID where SubExpense_ID=" + row1[0].ToString();
                        MySqlCommand com = new MySqlCommand(query, dbConnection);
                        com.Parameters.Add("@SubExpense_Name", MySqlDbType.VarChar).Value = txtSubNameUpdate.Text;
                        com.Parameters.Add("@MainExpense_ID", MySqlDbType.VarChar).Value = comMainUpdate.SelectedValue.ToString();
                        com.ExecuteNonQuery();
                        txtSubNameUpdate.Text = "";
                        comMainUpdate.SelectedIndex = -1;
                        
                        UserControl.ItemRecord("expense_sub", "تعديل", Convert.ToInt32(row1[0].ToString()), DateTime.Now, null, dbConnection);

                        updateLists();//update combox1
                        displayAllMain();
                        panelUpdateSub.Visible = false;
                        panelAddSub.Visible = false;
                    }
                    else
                    {
                        MessageBox.Show("هذا المصروف تم اضافته من قبل");
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
                        MessageBox.Show("يجب اختيار مصروف");
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

                //display sub in this main expense
                string query = "select * from expense_main";
                MySqlDataAdapter da = new MySqlDataAdapter(query, dbConnection);
                DataTable dt = new DataTable();
                da.Fill(dt);
                comMainUpdate.DataSource = dt;
                comMainUpdate.DisplayMember = dt.Columns["MainExpense_Name"].ToString();
                comMainUpdate.ValueMember = dt.Columns["MainExpense_ID"].ToString();
                comMainUpdate.SelectedValue = row1["MainExpense_ID"].ToString();
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
            string query = "select SubExpense_ID as 'التسلسل',SubExpense_Name as 'المصروف الفرعى',expense_main.MainExpense_Name as 'المصروف الرئيسى',expense_sub.MainExpense_ID from expense_sub INNER JOIN expense_main ON expense_sub.MainExpense_ID = expense_main.MainExpense_ID";
            adabter = new MySqlDataAdapter(query, dbConnection);
            dTable = new DataTable();
            adabter.Fill(dTable);
            gridControl1.DataSource = dTable;
            gridView1.Columns["MainExpense_ID"].Visible = false;
        }
        
        private void updateLists()
        {
            string query = "select * from expense_sub";
            MySqlDataAdapter da = new MySqlDataAdapter(query, dbConnection);
            DataTable dt = new DataTable();
            da.Fill(dt);
            comSub.DataSource = dt;
            comSub.DisplayMember = dt.Columns["SubExpense_Name"].ToString();
            comSub.ValueMember = dt.Columns["SubExpense_ID"].ToString();
            comSub.Text = "";
            
            gridControl1.DataSource = null;
            loaded = true;
        }

        void delete()
        {
            string query = "delete from expense_sub where SubExpense_ID=" + row1[0].ToString();
            MySqlCommand com = new MySqlCommand(query, dbConnection);
            com.ExecuteNonQuery();

            UserControl.ItemRecord("expense_sub", "حذف", Convert.ToInt32(row1[0].ToString()), DateTime.Now, null, dbConnection);
        }
    }
}

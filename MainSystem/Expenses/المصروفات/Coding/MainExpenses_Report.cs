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
    public partial class MainExpenses_Report : Form
    {
        // Sytem variables
        MySqlConnection dbConnection;
        MySqlCommandBuilder combuilder;
        MySqlDataAdapter adabter;
        DataTable dTable;
        bool loaded = false;
        DataRowView row1 = null;

        public MainExpenses_Report()
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
                    string qMain = "select MainExpense_ID as 'التسلسل', MainExpense_Name as 'المصروف الرئيسى' from expense_main where MainExpense_ID=" + comMain.SelectedValue.ToString();
                    MySqlDataAdapter adapterMain = new MySqlDataAdapter(qMain, dbConnection);

                    string qSub = "SELECT expense_sub.MainExpense_ID as 'التسلسل',expense_sub.SubExpense_Name as 'المصروف الفرعى' FROM expense_sub where MainExpense_ID=" + comMain.SelectedValue.ToString();
                    MySqlDataAdapter adapterSub = new MySqlDataAdapter(qSub, dbConnection);

                    adapterMain.Fill(sourceDataSet, "expense_main");
                    adapterSub.Fill(sourceDataSet, "expense_sub");

                    //Set up a master-detail relationship between the DataTables 
                    DataColumn keyColumn = sourceDataSet.Tables["expense_main"].Columns["التسلسل"];
                    DataColumn foreignKeyColumn = sourceDataSet.Tables["expense_sub"].Columns["التسلسل"];
                    sourceDataSet.Relations.Add("المصروفات", keyColumn, foreignKeyColumn);

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
                    string q = "select MainExpense_Name from expense_main where MainExpense_Name='" + txtMainAdd.Text + "'";
                    MySqlCommand c = new MySqlCommand(q, dbConnection);
                    if (c.ExecuteScalar() == null)
                    {
                        string query = "insert into expense_main (MainExpense_Name) values (@MainExpense_Name)";
                        MySqlCommand com = new MySqlCommand(query, dbConnection);
                        com.Parameters.Add("@MainExpense_Name", MySqlDbType.VarChar).Value = txtMainAdd.Text;
                        com.ExecuteNonQuery();
                        txtMainAdd.Text = "";

                        q = "select MainExpense_ID from expense_main order by MainExpense_ID desc limit 1";
                        c = new MySqlCommand(q, dbConnection);
                        int MainExpenseId = Convert.ToInt32(c.ExecuteScalar().ToString());

                        UserControl.ItemRecord("expense_main", "اضافة", MainExpenseId, DateTime.Now, null, dbConnection);
                        updateLists();
                        displayAllMain();
                        panelAddMain.Visible = false;
                        panelUpdateMain.Visible = false;
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
                        MessageBox.Show("يجب اختيار مصروف");
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
            string qMain = "select MainExpense_ID as 'التسلسل', MainExpense_Name as 'المصروف الرئيسى' from expense_main";
            MySqlDataAdapter adapterMain = new MySqlDataAdapter(qMain, dbConnection);

            string qSub = "SELECT expense_sub.MainExpense_ID as 'التسلسل',expense_sub.SubExpense_Name as 'المصروف الفرعى' FROM expense_sub";
            MySqlDataAdapter adapterSub = new MySqlDataAdapter(qSub, dbConnection);

            adapterMain.Fill(sourceDataSet, "expense_main");
            adapterSub.Fill(sourceDataSet, "expense_sub");

            //Set up a master-detail relationship between the DataTables 
            DataColumn keyColumn = sourceDataSet.Tables["expense_main"].Columns["التسلسل"];
            DataColumn foreignKeyColumn = sourceDataSet.Tables["expense_sub"].Columns["التسلسل"];
            sourceDataSet.Relations.Add("المصروفات", keyColumn, foreignKeyColumn);

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

        /*void RecordExpense()
        {
            dbconnection.Open();
            string query = "insert into expenses (ExpenseType_ID,Expense_Type,Error) values (@ExpenseType_ID,@Expense_Type,@Error)";
            MySqlCommand com = new MySqlCommand(query, dbconnection);
            if (cmbExpenseType.SelectedValue != null)
            {
                com.Parameters.Add("@ExpenseType_ID", MySqlDbType.Int16, 11).Value = cmbExpenseType.SelectedValue;
            }
            else
            {
                int ExpenseType_ID = 0;
                string q = "select ID from expense_type where Type='" + cmbExpenseType.Text + "'";
                MySqlCommand comand = new MySqlCommand(q, dbconnection);
                if (comand.ExecuteScalar() == null)
                {
                    q = "insert into expense_type (Type) values (@Type)";
                    comand = new MySqlCommand(q, dbconnection);
                    comand.Parameters.Add("@Type", MySqlDbType.VarChar, 255).Value = cmbExpenseType.Text;
                    comand.ExecuteNonQuery();

                    q = "select ID from expense_type order by ID desc limit 1";
                    comand = new MySqlCommand(q, dbconnection);
                    ExpenseType_ID = Convert.ToInt32(comand.ExecuteScalar().ToString());
                }
                else
                {
                    cmbExpenseType.SelectedValue = comand.ExecuteScalar();
                    ExpenseType_ID = Convert.ToInt32(cmbExpenseType.SelectedValue.ToString());
                }

                com.Parameters.Add("@ExpenseType_ID", MySqlDbType.Int16, 11).Value = ExpenseType_ID;
            }
            com.Parameters.Add("@Expense_Type", MySqlDbType.VarChar, 255).Value = cmbExpenseType.Text;
            com.Parameters.Add("@Error", MySqlDbType.Int16, 11).Value = 0;
            com.ExecuteNonQuery();

            query = "select Expense_ID from expense order by Expense_ID desc limit 1";
            com = new MySqlCommand(query, dbconnection);
            ID = Convert.ToInt32(com.ExecuteScalar().ToString());

            //////////record adding/////////////
            query = "insert into usercontrol (UserControl_UserID,UserControl_TableName,UserControl_Status,UserControl_RecordID,UserControl_Date,UserControl_Reason) values(@UserControl_UserID,@UserControl_TableName,@UserControl_Status,@UserControl_RecordID,@UserControl_Date,@UserControl_Reason)";
            com = new MySqlCommand(query, dbconnection);
            com.Parameters.Add("@UserControl_UserID", MySqlDbType.Int16, 11).Value = UserControl.userID;
            com.Parameters.Add("@UserControl_TableName", MySqlDbType.VarChar, 255).Value = "expense";
            com.Parameters.Add("@UserControl_Status", MySqlDbType.VarChar, 255).Value = "اضافة";
            com.Parameters.Add("@UserControl_RecordID", MySqlDbType.VarChar, 255).Value = ID;
            com.Parameters.Add("@UserControl_Date", MySqlDbType.DateTime, 0).Value = DateTime.Now;
            com.Parameters.Add("@UserControl_Reason", MySqlDbType.VarChar, 255).Value = null;
            com.ExecuteNonQuery();
            ////////////////////////////////////
            dbconnection.Close();
            successFlag = true;
        }*/

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

            gridControl1.DataSource = null;
            loaded = true;
        }

        void delete()
        {
            string query = "delete from expense_main where MainExpense_ID=" + row1[0].ToString();
            MySqlCommand com = new MySqlCommand(query, dbConnection);
            com.ExecuteNonQuery();
            
            /*query = "update expense_sub set MainExpense_ID=null where MainExpense_ID=" + MainExpenseId;
            com = new MySqlCommand(query, dbConnection);
            com.ExecuteNonQuery();*/

            UserControl.ItemRecord("expense_main", "حذف", Convert.ToInt32(row1[0].ToString()), DateTime.Now, null, dbConnection);
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
                    string q = "select MainExpense_Name from expense_main where MainExpense_Name='" + txtMainUpdate.Text + "' and MainExpense_ID<>" + row1[0].ToString();
                    MySqlCommand c = new MySqlCommand(q, dbConnection);
                    if (c.ExecuteScalar() == null)
                    {
                        string query = "update expense_main set MainExpense_Name=@MainExpense_Name where MainExpense_ID=" + row1[0].ToString();
                        MySqlCommand com = new MySqlCommand(query, dbConnection);
                        com.Parameters.Add("@MainExpense_Name", MySqlDbType.VarChar).Value = txtMainUpdate.Text;
                        com.ExecuteNonQuery();
                        txtMainUpdate.Text = "";

                        UserControl.ItemRecord("expense_main", "تعديل", Convert.ToInt32(row1[0].ToString()), DateTime.Now, null, dbConnection);
                        updateLists();
                        displayAllMain();
                        panelAddMain.Visible = false;
                        panelUpdateMain.Visible = false;
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
    }
}

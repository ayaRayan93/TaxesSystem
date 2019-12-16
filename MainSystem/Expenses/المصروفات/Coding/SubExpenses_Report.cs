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
    public partial class SubExpenses_Report : Form
    {
        // Sytem variables
        MySqlConnection dbConnection;
        MySqlCommandBuilder combuilder;
        MySqlDataAdapter adabter;
        DataTable dTable;
        bool loaded = false;
        int subExpenseId = 0;

        public SubExpenses_Report()
        {
            try
            {
                InitializeComponent();
                dbConnection = new MySqlConnection(connection.connectionString);

                comSub.AutoCompleteMode = AutoCompleteMode.Suggest;
                comSub.AutoCompleteSource = AutoCompleteSource.ListItems;
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
                displayAllSub();
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
                displayAllSub();
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
                    string query = "select SubExpense_ID as 'التسلسل', SubExpense_Name as 'المصروف الفرعى',expense_main.MainExpense_Name as 'المصروف الرئيسى' from expense_sub left join expense_main on expense_sub.MainExpense_ID=expense_main.MainExpense_ID where SubExpense_ID=" + comSub.SelectedValue.ToString();
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

        private void btnAddNewSub_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtSub.Text != "")
                {
                    dbConnection.Open();
                    string q = "select SubExpense_Name from expense_sub where SubExpense_Name='" + txtSub.Text + "'";
                    MySqlCommand c = new MySqlCommand(q, dbConnection);
                    if (c.ExecuteScalar() == null)
                    {
                        string query = "insert into expense_sub (SubExpense_Name) values (@SubExpense_Name)";
                        MySqlCommand com = new MySqlCommand(query, dbConnection);
                        com.Parameters.Add("@SubExpense_Name", MySqlDbType.VarChar).Value = txtSub.Text;
                        com.ExecuteNonQuery();
                        txtSub.Text = "";

                        q = "select SubExpense_ID from expense_sub order by SubExpense_ID desc limit 1";
                        c = new MySqlCommand(q, dbConnection);
                        int areId = Convert.ToInt32(c.ExecuteScalar().ToString());

                        UserControl.ItemRecord("expense_sub", "اضافة", areId, DateTime.Now, null, dbConnection);
                        updateLists();
                        displayAllSub();
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
                displayAllSub();
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
                subExpenseId = Convert.ToInt32(row1[0].ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            dbConnection.Close();
        }

        //System Functions

        public void displayAllSub()
        {
            string query = "select SubExpense_ID as 'التسلسل', SubExpense_Name as 'المصروف الفرعى',expense_main.MainExpense_Name as 'المصروف الرئيسى' from expense_sub left join expense_main on expense_sub.MainExpense_ID=expense_main.MainExpense_ID";
            adabter = new MySqlDataAdapter(query, dbConnection);
            dTable = new DataTable();
            adabter.Fill(dTable);
            gridControl1.DataSource = dTable;
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
            string query = "delete from expense_sub where SubExpense_ID=" + subExpenseId;
            MySqlCommand com = new MySqlCommand(query, dbConnection);
            com.ExecuteNonQuery();
            UserControl.ItemRecord("expense_sub", "حذف", subExpenseId, DateTime.Now, null, dbConnection);
        }
    }
}

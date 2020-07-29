using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraPrinting;
using DevExpress.XtraPrinting.Preview;
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

namespace TaxesSystem
{
    public partial class SubPropertyTransitions_Report : Form
    {
        MySqlConnection conn;
        XtraTabControl xtraTabControlProperty;
        DataRowView row1 = null;
        bool loaded = false;
        bool loaded2 = false;

        public SubPropertyTransitions_Report(XtraTabControl XtraTabControlProperty)
        {
            InitializeComponent();
            conn = new MySqlConnection(connection.connectionString);
            xtraTabControlProperty = XtraTabControlProperty;
        }
        
        private void Bills_Transitions_Report_Load(object sender, EventArgs e)
        {
            try
            {
                string query = "select * from property_main";
                MySqlDataAdapter da = new MySqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                comMain.DataSource = dt;
                comMain.DisplayMember = dt.Columns["MainProperty_Name"].ToString();
                comMain.ValueMember = dt.Columns["MainProperty_ID"].ToString();
                comMain.SelectedIndex = -1;

                query = "select * from bank inner join bank_main on bank.MainBank_ID=bank_main.MainBank_ID where MainBank_Type='خزينة' and MainBank_Name='خزينة عقارات'";
                da = new MySqlDataAdapter(query, conn);
                dt = new DataTable();
                da.Fill(dt);
                comSafe.DataSource = dt;
                comSafe.DisplayMember = dt.Columns["Bank_Name"].ToString();
                comSafe.ValueMember = dt.Columns["Bank_ID"].ToString();
                comSafe.SelectedIndex = -1;
                loaded = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void comMain_SelectedValueChanged(object sender, EventArgs e)
        {
            if (loaded)
            {
                try
                {
                    if (comMain.SelectedValue != null)
                    {
                        loaded2 = false;
                        string query = "select * from property_sub where MainProperty_ID=" + comMain.SelectedValue.ToString();
                        MySqlDataAdapter da = new MySqlDataAdapter(query, conn);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        comSub.DataSource = dt;
                        comSub.DisplayMember = dt.Columns["SubProperty_Name"].ToString();
                        comSub.ValueMember = dt.Columns["SubProperty_ID"].ToString();
                        comSub.SelectedIndex = -1;
                        comDetails.DataSource = null;
                        loaded2 = true;
                    }
                    else
                    {
                        comSub.DataSource = null;
                        comDetails.DataSource = null;
                    }
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void comSub_SelectedValueChanged(object sender, EventArgs e)
        {
            if (loaded && loaded2)
            {
                try
                {
                    if (comSub.SelectedValue != null)
                    {
                        string query = "select * from property_details where SubProperty_ID=" + comSub.SelectedValue.ToString();
                        MySqlDataAdapter da = new MySqlDataAdapter(query, conn);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        comDetails.DataSource = dt;
                        comDetails.DisplayMember = dt.Columns["DetailsProperty_Name"].ToString();
                        comDetails.ValueMember = dt.Columns["DetailsProperty_ID"].ToString();
                        comDetails.SelectedIndex = -1;
                    }
                    else
                    {
                        comDetails.DataSource = null;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (comMain.SelectedValue != null)
            {
                try
                {
                    search();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                conn.Close();
            }
            else
            {
                MessageBox.Show("يجب اختيار العقار");
            }
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            if (gridView1.RowCount > 0)
            {
                try
                {
                    List<Item_SubExpensesTransitions> bi = new List<Item_SubExpensesTransitions>();
                    for (int i = 0; i < gridView1.RowCount; i++)
                    {
                        Item_SubExpensesTransitions item = new Item_SubExpensesTransitions() { ID = Convert.ToInt32(gridView1.GetRowCellDisplayText(i, gridView1.Columns["التسلسل"])), MainExpenses = gridView1.GetRowCellDisplayText(i, gridView1.Columns["العقار"]), SubExpenses = gridView1.GetRowCellDisplayText(i, gridView1.Columns["المصروف الرئيسى"]), DetailsProperty = gridView1.GetRowCellDisplayText(i, gridView1.Columns["المصروف الفرعى"]), Depesitor = gridView1.GetRowCellDisplayText(i, gridView1.Columns["المستلم"]), Date = Convert.ToDateTime(gridView1.GetRowCellDisplayText(i, gridView1.Columns["التاريخ"])).ToString("yyyy-MM-dd"), Money = Convert.ToDouble(gridView1.GetRowCellDisplayText(i, gridView1.Columns["المبلغ"])), Safe = gridView1.GetRowCellDisplayText(i, gridView1.Columns["الخزينة"]), Employee = gridView1.GetRowCellDisplayText(i, gridView1.Columns["الموظف"]), Description = gridView1.GetRowCellDisplayText(i, gridView1.Columns["البيان"]) };
                        bi.Add(item);
                    }
                    Report_SubExpensesTransitions f = new Report_SubExpensesTransitions();
                    f.PrintInvoice2(comMain.Text, comSub.Text, comDetails.Text, comSafe.Text, dateTimePicker1.Value.Date, dateTimePicker2.Value.Date, bi);
                    f.ShowDialog();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                MessageBox.Show("يجب التاكد من البيانات");
            }
        }

        //functions
        public void search()
        {
            conn.Open();
            
            double totalProperty = 0;
            string qSub = "";
            string qDetails = "";
            string qSafe = "";
            
            MySqlDataAdapter adapter = new MySqlDataAdapter("SELECT property_transition.PropertyTransition_ID as 'التسلسل',property_transition.Date as 'التاريخ',property_main.MainProperty_Name as 'العقار',property_sub.SubProperty_Name as 'المصروف الرئيسى',property_details.DetailsProperty_Name as 'المصروف الفرعى',bank.Bank_Name as 'الخزينة',property_transition.Amount as 'المبلغ',employee.Employee_Name as 'الموظف',property_transition.Depositor_Name as 'المستلم',property_transition.Description as 'البيان' FROM property_transition left JOIN property_details ON property_details.DetailsProperty_ID = property_transition.DetailsProperty_ID left JOIN property_sub ON property_sub.SubProperty_ID = property_details.SubProperty_ID left JOIN property_main ON property_main.MainProperty_ID = property_sub.MainProperty_ID INNER JOIN branch ON branch.Branch_ID = property_transition.Branch_ID INNER JOIN bank ON bank.Bank_ID = property_transition.Bank_ID INNER JOIN employee ON property_transition.Employee_ID = employee.Employee_ID where PropertyTransition_ID=0", conn);
            DataSet sourceDataSet = new DataSet();
            adapter.Fill(sourceDataSet);
            gridControl1.DataSource = sourceDataSet.Tables[0];

            if (comSub.SelectedValue == null && comSub.Text == "")
            {
                qSub = "select SubProperty_ID from property_sub";
            }
            else
            {
                qSub = comSub.SelectedValue.ToString();
            }

            if (comDetails.SelectedValue == null && comDetails.Text == "")
            {
                qDetails = "select DetailsProperty_ID from property_details";
            }
            else
            {
                qDetails = comDetails.SelectedValue.ToString();
            }

            if (comSafe.SelectedValue == null && comSafe.Text == "")
            {
                //qSafe = "select Bank_ID from bank";
                qSafe = "select bank.Bank_ID from bank inner join bank_main on bank.MainBank_ID=bank_main.MainBank_ID where MainBank_Type='خزينة'";
            }
            else
            {
                qSafe = comSafe.SelectedValue.ToString();
            }

            string query = "SELECT property_transition.PropertyTransition_ID as 'التسلسل',property_transition.Date as 'التاريخ',property_main.MainProperty_Name as 'العقار',property_sub.SubProperty_Name as 'المصروف الرئيسى',property_details.DetailsProperty_Name as 'المصروف الفرعى',property_transition.Depositor_Name as 'المستلم',bank.Bank_Name as 'الخزينة',property_transition.Amount as 'المبلغ',property_transition.Description as 'البيان',employee.Employee_Name as 'الموظف' FROM property_transition left JOIN property_details ON property_details.DetailsProperty_ID = property_transition.DetailsProperty_ID left JOIN property_sub ON property_details.SubProperty_ID=property_sub.SubProperty_ID left JOIN property_main ON property_sub.MainProperty_ID=property_main.MainProperty_ID INNER JOIN branch ON branch.Branch_ID = property_transition.Branch_ID INNER JOIN bank ON bank.Bank_ID = property_transition.Bank_ID INNER JOIN employee ON property_transition.Employee_ID = employee.Employee_ID where property_sub.MainProperty_ID=" + comMain.SelectedValue.ToString() + " and property_sub.SubProperty_ID in(" + qSub + ") and property_transition.DetailsProperty_ID in(" + qDetails + ") and property_transition.Bank_ID in(" + qSafe + ") and property_transition.Error=0 and date(property_transition.Date) between '" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "' and '" + dateTimePicker2.Value.ToString("yyyy-MM-dd") + "' order by property_transition.Date";
            MySqlCommand comand = new MySqlCommand(query, conn);
            MySqlDataReader dr = comand.ExecuteReader();
            while (dr.Read())
            {
                gridView1.AddNewRow();
                int rowHandle = gridView1.GetRowHandle(gridView1.DataRowCount);
                if (gridView1.IsNewItemRow(rowHandle))
                {
                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns["التسلسل"], dr["التسلسل"].ToString());
                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns["العقار"], dr["العقار"].ToString());
                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns["المصروف الرئيسى"], dr["المصروف الرئيسى"].ToString());
                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns["المصروف الفرعى"], dr["المصروف الفرعى"].ToString());
                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns["التاريخ"], dr["التاريخ"].ToString());
                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns["المستلم"], dr["المستلم"].ToString());
                   
                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns["المبلغ"], dr["المبلغ"].ToString());
                    totalProperty += Convert.ToDouble(dr["المبلغ"].ToString());
                    
                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns["الخزينة"], dr["الخزينة"].ToString());
                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns["البيان"], dr["البيان"].ToString());
                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns["الموظف"], dr["الموظف"].ToString());
                }
            }
            dr.Close();

            if (gridView1.IsLastVisibleRow)
            {
                gridView1.FocusedRowHandle = gridView1.RowCount - 1;
            }
            gridView1.Columns[0].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;

            for (int i = 1; i < gridView1.Columns.Count; i++)
            {
                gridView1.Columns[i].Width = 100;
            }
            
            txtExpense.Text = totalProperty.ToString();
        }

        private void btnNewChosen_Click(object sender, EventArgs e)
        {
            try
            {
                clearCom();
                
                gridControl1.DataSource = null;
                txtExpense.Text = "0";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void clearCom()
        {
            foreach (Control co in this.tableLayoutPanel3.Controls)
            {
                if (co is System.Windows.Forms.ComboBox)
                {
                    co.Text = "";
                }
                else if (co is TextBox)
                {
                    co.Text = "";
                }
                else if (co is DateTimePicker)
                {
                    dateTimePicker1.Value = DateTime.Now;
                    dateTimePicker2.Value = DateTime.Now;
                }
            }
        }
        
        private void gridView1_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            try
            {
                row1 = (DataRowView)gridView1.GetRow(e.RowHandle);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}

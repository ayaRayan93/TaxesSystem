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
    public partial class PropertyNumTransitions_Report : Form
    {
        MySqlConnection conn;
        XtraTabControl xtraTabControlProperty;
        MainForm mainForm = null;
        DataRowView row1 = null;

        public PropertyNumTransitions_Report(XtraTabControl XtraTabControlProperty, MainForm mainform)
        {
            InitializeComponent();
            conn = new MySqlConnection(connection.connectionString);
            xtraTabControlProperty = XtraTabControlProperty;
            mainForm = mainform;
        }
        
        private void Bills_Transitions_Report_Load(object sender, EventArgs e)
        {
        }
        
        //functions
        public void search()
        {
            conn.Open();

            MySqlDataAdapter adapter = new MySqlDataAdapter("SELECT property_transition.PropertyTransition_ID as 'التسلسل',property_transition.Type as 'النوع',DATE_FORMAT(property_transition.Date,'%d-%m-%Y') as 'التاريخ',property_main.MainProperty_Name as 'العقار',property_sub.SubProperty_Name as 'المصروف الرئيسى',property_details.DetailsProperty_Name as 'المصروف الفرعى',bank.Bank_Name as 'الخزينة',property_transition.Amount as 'وارد',property_transition.Amount as 'مصروف',employee.Employee_Name as 'الموظف',property_transition.Depositor_Name as 'المودع/المستلم',property_transition.Description as 'البيان' FROM property_transition left JOIN property_details ON property_details.DetailsProperty_ID = property_transition.DetailsProperty_ID left JOIN property_sub ON property_sub.SubProperty_ID = property_details.Subproperty_ID left JOIN property_main ON property_main.MainProperty_ID = property_sub.MainProperty_ID INNER JOIN branch ON branch.Branch_ID = property_transition.Branch_ID INNER JOIN bank ON bank.Bank_ID = property_transition.Bank_ID INNER JOIN employee ON property_transition.Employee_ID = employee.Employee_ID where PropertyTransition_ID=0", conn);
            DataSet sourceDataSet = new DataSet();
            adapter.Fill(sourceDataSet);
            gridControl1.DataSource = sourceDataSet.Tables[0];

            string query = "SELECT property_transition.PropertyTransition_ID as 'التسلسل',property_transition.Type as 'النوع',property_main.MainProperty_Name as 'العقار',property_sub.SubProperty_Name as 'المصروف الرئيسى',property_details.DetailsProperty_Name as 'المصروف الفرعى',DATE_FORMAT(property_transition.Date,'%d-%m-%Y') as 'التاريخ',property_transition.Depositor_Name as 'المودع/المستلم',bank.Bank_Name as 'الخزينة',property_transition.Amount as 'المبلغ',property_transition.Description as 'البيان',employee.Employee_Name as 'الموظف' FROM property_transition left JOIN property_details ON property_details.DetailsProperty_ID = property_transition.DetailsProperty_ID left JOIN property_sub ON property_details.SubProperty_ID=property_sub.SubProperty_ID left JOIN property_main ON property_sub.MainProperty_ID=property_main.MainProperty_ID INNER JOIN branch ON branch.Branch_ID = property_transition.Branch_ID INNER JOIN bank ON bank.Bank_ID = property_transition.Bank_ID INNER JOIN employee ON property_transition.Employee_ID = employee.Employee_ID where property_transition.PropertyTransition_ID=" + txtPropertyNum.Text + " and property_transition.Error=0 order by property_transition.Date";
            MySqlCommand comand = new MySqlCommand(query, conn);
            MySqlDataReader dr = comand.ExecuteReader();
            while (dr.Read())
            {
                gridView1.AddNewRow();
                int rowHandle = gridView1.GetRowHandle(gridView1.DataRowCount);
                if (gridView1.IsNewItemRow(rowHandle))
                {
                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns["التسلسل"], dr["التسلسل"].ToString());
                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns["النوع"], dr["النوع"].ToString());
                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns["العقار"], dr["العقار"].ToString());
                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns["المصروف الرئيسى"], dr["المصروف الرئيسى"].ToString());
                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns["المصروف الفرعى"], dr["المصروف الفرعى"].ToString());
                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns["التاريخ"], dr["التاريخ"].ToString());
                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns["المودع/المستلم"], dr["المودع/المستلم"].ToString());

                    if (dr["النوع"].ToString() == "ايداع")
                    {
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns["وارد"], dr["المبلغ"].ToString());
                    }
                    else
                    {
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns["مصروف"], dr["المبلغ"].ToString());
                    }

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

            row1 = (DataRowView)gridView1.GetRow(gridView1.RowCount - 1);


            gridView1.Columns[0].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            
            for (int i = 1; i < gridView1.Columns.Count; i++)
            {
                gridView1.Columns[i].Width = 100;
            }
        }

        private void btnNewChosen_Click(object sender, EventArgs e)
        {
            try
            {
                clearCom();
                
                gridControl1.DataSource = null;
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
            }
        }
        
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (gridView1.GetRowCellDisplayText(0, "النوع") == "صرف")
                {
                    mainForm.bindUpdatePropertyExpenseForm(row1);
                }
                else if (gridView1.GetRowCellDisplayText(0, "النوع") == "ايداع")
                {
                    mainForm.bindUpdatePropertyIncomeForm(row1);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            conn.Close();
        }

        private void txtPropertyNum_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    if (txtPropertyNum.Text != "")
                    {
                        int bilNum = 0;
                        if (int.TryParse(txtPropertyNum.Text, out bilNum))
                        {
                            search();
                        }
                        else
                        {
                            MessageBox.Show("رقم المصروف يجب ان يكون عدد");
                        }
                    }
                    else
                    {
                        MessageBox.Show("يجب ادخال رقم المصروف");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                conn.Close();
            }
        }
    }
}

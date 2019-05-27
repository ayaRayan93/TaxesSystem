using DevExpress.XtraGrid.Views.Grid;
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

namespace MainSystem
{
    public partial class DelegateBill_Report : Form
    {
        MySqlConnection conn;
        bool loaded = false;

        public DelegateBill_Report()
        {
            InitializeComponent();
            conn = new MySqlConnection(connection.connectionString);

            comDelegate.AutoCompleteMode = AutoCompleteMode.Suggest;
            comDelegate.AutoCompleteSource = AutoCompleteSource.ListItems;
        }

        private void DelegateBill_Report_Load(object sender, EventArgs e)
        {
            try
            {
                string query = "select * from delegate where Branch_ID=" + UserControl.EmpBranchID + "";
                conn.Open();
                MySqlDataAdapter da = new MySqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                comDelegate.DataSource = dt;
                comDelegate.DisplayMember = dt.Columns["Delegate_Name"].ToString();
                comDelegate.ValueMember = dt.Columns["Delegate_ID"].ToString();
                comDelegate.SelectedIndex = -1;
                loaded = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            conn.Close();
        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (comDelegate.SelectedValue == null)
                {
                    searchAll();
                }
                else
                {
                    search();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            conn.Close();
        }

        private void txtBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (loaded)
                {
                    XtraTabPage xtraTabPage = getTabPage("tabPageDelegateBillReport");
                    if (!IsClear())
                    {
                        xtraTabPage.ImageOptions.Image = Properties.Resources.unsave;
                    }
                    else
                    {
                        xtraTabPage.ImageOptions.Image = null;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void search()
        {
            string query = "SELECT distinct delegate.Delegate_Name as 'المندوب',dash.Bill_Number as 'الفاتورة',dash.Customer_Name as 'العميل',dash.Bill_Date as 'التاريخ' FROM dash INNER JOIN dash_details ON dash_details.Dash_ID = dash.Dash_ID INNER JOIN delegate ON dash_details.Delegate_ID = delegate.Delegate_ID where DATE_FORMAT(dash.Bill_Date,'%Y-%m-%d') between '" + this.dateTimePicker1.Value.ToString("yyyy-MM-dd") + "' and '" + this.dateTimePicker2.Value.ToString("yyyy-MM-dd") + "' and dash.Branch_ID=" + UserControl.EmpBranchID + " and dash_details.Delegate_ID=" + comDelegate.SelectedValue.ToString();
            conn.Open();
            MySqlDataAdapter adpt = new MySqlDataAdapter(query, conn);
            DataSet dset = new DataSet();
            adpt.Fill(dset);
            gridControl1.DataSource = dset.Tables[0];
        }

        public void searchAll()
        {
            string query = "SELECT distinct delegate.Delegate_Name as 'المندوب',dash.Bill_Number as 'الفاتورة',dash.Customer_Name as 'العميل',dash.Bill_Date as 'التاريخ' FROM dash INNER JOIN dash_details ON dash_details.Dash_ID = dash.Dash_ID INNER JOIN delegate ON dash_details.Delegate_ID = delegate.Delegate_ID where DATE_FORMAT(dash.Bill_Date,'%Y-%m-%d') between '" + this.dateTimePicker1.Value.ToString("yyyy-MM-dd") + "' and '" + this.dateTimePicker2.Value.ToString("yyyy-MM-dd") + "' and dash.Branch_ID=" + UserControl.EmpBranchID;
            conn.Open();
            MySqlDataAdapter adpt = new MySqlDataAdapter(query, conn);
            DataSet dset = new DataSet();
            adpt.Fill(dset);
            gridControl1.DataSource = dset.Tables[0];
        }

        public XtraTabPage getTabPage(string text)
        {
            for (int i = 0; i < MainForm.tabControlPointSale.TabPages.Count; i++)
                if (MainForm.tabControlPointSale.TabPages[i].Name == text)
                {
                    return MainForm.tabControlPointSale.TabPages[i];
                }
            return null;
        }

        public bool IsClear()
        {
            bool flag5 = false;
            foreach (Control co in this.panel1.Controls)
            {
                foreach (Control item in co.Controls)
                {
                    if (item is ComboBox)
                    {
                        if (item.Text == "")
                            flag5 = true;
                        else
                            return false;
                    }
                    else if (item is TextBox)
                    {
                        if (item.Text == "")
                            flag5 = true;
                        else
                            return false;
                    }
                }
            }

            return flag5;
        }
    }
}
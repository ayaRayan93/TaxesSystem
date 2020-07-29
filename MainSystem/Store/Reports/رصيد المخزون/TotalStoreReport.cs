using DevExpress.Utils;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraPrinting;
using DevExpress.XtraPrinting.Preview;
using DevExpress.XtraTab;
using TaxesSystem.Store.Reports.تقرير_اصناف_الشركات;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TaxesSystem
{
    public partial class TotalStoreReport : Form
    {
        MySqlConnection dbconnection;
        bool load = false;
        public TotalStoreReport()
        {
            try
            {
                InitializeComponent();
                dbconnection = new MySqlConnection(connection.connectionString);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void TotalStoreReport_Load(object sender, EventArgs e)
        {
            try
            {
                dbconnection.Open();

                string query = "select * from store";
                MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                DataTable dt = new DataTable();
                da.Fill(dt);
                comStore.DataSource = dt;
                comStore.DisplayMember = dt.Columns["Store_Name"].ToString();
                comStore.ValueMember = dt.Columns["Store_ID"].ToString();
                comStore.Text = "";
                load = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }

        //functions
        public void  Search()
        {
            string query = "";
            if (comStore.Text == "")
            {
                query = "select Store_Name as 'أسم المخزن', sum(Total_Meters*Sell_Price) as 'اجمالي المخزون بسعر البيع',sum(Total_Meters*Purchasing_Price) as 'اجمالي المخزون بسعر الشراء' from storage left join sellprice on storage.Data_ID=sellprice.Data_ID left join purchasing_price on storage.Data_ID=purchasing_price.Data_ID inner join store on store.Store_ID=storage.Store_ID group by storage.Store_ID";
            }
            else
            {
                query = "select Store_Name as 'أسم المخزن', sum(Total_Meters*Sell_Price) as 'اجمالي المخزون بسعر البيع',sum(Total_Meters*Purchasing_Price) as 'اجمالي المخزون بسعر الشراء' from storage left join sellprice on storage.Data_ID=sellprice.Data_ID left join purchasing_price on storage.Data_ID=purchasing_price.Data_ID inner join store on store.Store_ID=storage.Store_ID group by storage.Store_ID having storage.Store_ID=" + comStore.SelectedValue;
            }
            MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
            DataTable td = new DataTable();
            da.Fill(td);
            gridControl1.DataSource = td;
            double totalSellPrice = 0,totalPurshasePrice = 0;
            for (int i = 0; i < td.Rows.Count; i++)
            {
                if(td.Rows[i].ItemArray[1].ToString() != "")
                    totalSellPrice +=Convert.ToDouble(td.Rows[i].ItemArray[1]);
                if (td.Rows[i].ItemArray[2].ToString() != "")
                    totalPurshasePrice += Convert.ToDouble(td.Rows[i].ItemArray[2]);
            }
            txtTotalSales.Text = totalSellPrice.ToString();
            txtTotalPurshases.Text = totalPurshasePrice.ToString();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                dbconnection.Open();
                Search();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }

        private void comStore_SelectedValueChanged(object sender, EventArgs e)
        {
            if (load)
            {
                try
                {
                    dbconnection.Open();
                    Search();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                dbconnection.Close();
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                List<Data> dataList = new List<Data>();
                for (int i = 0; i < gridView1.RowCount; i++)
                {
                    Data data = new Data();
                    data.Store_Name = gridView1.GetRowCellDisplayText(i,gridView1.Columns[0].ToString());
                    if(gridView1.GetRowCellDisplayText(i, gridView1.Columns[1]).ToString()!="")
                        data.totalPurshasePrice = Convert.ToDouble(gridView1.GetRowCellDisplayText(i,gridView1.Columns[1]));
                    if (gridView1.GetRowCellDisplayText(i, gridView1.Columns[2]).ToString() != "")
                        data.totalSellPrice = Convert.ToDouble(gridView1.GetRowCellDisplayText(i, gridView1.Columns[2]));
                    dataList.Add(data);
                }
                Report_Items_Factory f = new Report_Items_Factory();
                f.PrintReport(dataList);
                f.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}

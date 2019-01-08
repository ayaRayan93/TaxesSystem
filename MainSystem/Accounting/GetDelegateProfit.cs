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
    public partial class GetDelegateProfit : Form
    {
        private MySqlConnection dbconnection;
        private MySqlConnection dbconnection2;
        public bool loaded = false;
        MainForm MainForm;
        public GetDelegateProfit(MainForm MainForm)
        {
            try
            {
                InitializeComponent();
                dbconnection = new MySqlConnection(connection.connectionString);
                dbconnection2 = new MySqlConnection(connection.connectionString);
                this.MainForm = MainForm;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void GetDelegateProfit_Load(object sender, EventArgs e)
        {
            try
            {
                dbconnection.Open();

                string query = "select * from delegate ";

                MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                DataTable dt = new DataTable();
            
                da.Fill(dt);
                comDelegate.DataSource = dt;
                comDelegate.DisplayMember = dt.Columns["Delegate_Name"].ToString();
                comDelegate.ValueMember = dt.Columns["Delegate_ID"].ToString();
                comDelegate.Text = "";
                txtDelegateID.Text = "";

                loaded = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }
  
        private void comDelegate_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (loaded)
                {
                    try
                    {
                        dbconnection.Open();
                        txtDelegateID.Text = comDelegate.SelectedValue.ToString();
                        string query = "select Date_To from Delegates_Profit where Delegate_ID=" + txtDelegateID.Text + " order by Delegate_Profit_ID limit 1";
                        MySqlCommand com = new MySqlCommand(query, dbconnection);
                        if (com.ExecuteScalar() != null)
                        {
                            dateTimeFrom.Text = com.ExecuteScalar().ToString();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }
 
        private void txtDelegateID_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    dbconnection.Open();
                    string query = "select Delegate_Name from delegate where Delegate_ID=" + txtDelegateID.Text ;
                    MySqlCommand com = new MySqlCommand(query, dbconnection);
                    if (com.ExecuteScalar() != null)
                    {
                        Name = (string)com.ExecuteScalar();
                        comDelegate.Text = Name;
                        query = "select Date_To from Delegates_Profit where Delegate_ID=" + txtDelegateID.Text+ " order by Delegate_Profit_ID limit 1";
                        com = new MySqlCommand(query, dbconnection);
                        if (com.ExecuteScalar() != null)
                        {
                            dateTimeFrom.Text = com.ExecuteScalar().ToString();
                        }
                    }
                    else
                    {
                        MessageBox.Show("there is no item with this id");
                        dbconnection.Close();
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                dbconnection.Open();
                //peraper datasource
                DataTable _Table = peraperDataTable2();
                DataTable _Table2 = peraperDataTable();
                DataTable _Table3 = peraperDataTable3();//all items of all companys
                DateTime date = dateTimeFrom.Value;
                string d = date.ToString("yyyy-MM-dd HH:mm:ss");
                DateTime date2 = dateTimeTo.Value;
                string d2 = date2.ToString("yyyy-MM-dd HH:mm:ss");
                string itemName = "concat( product.Product_Name,' ',type.Type_Name,' ',factory.Factory_Name,' ',groupo.Group_Name,' ' ,COALESCE(color.Color_Name,''),' ',COALESCE(size.Size_Value,''),' ',COALESCE(sort.Sort_Value,''),' ',COALESCE(data.Classification,''),' ',COALESCE(data.Description,''))as 'البند'";
                string DataTableRelations = "INNER JOIN type ON type.Type_ID = data.Type_ID INNER JOIN product ON product.Product_ID = data.Product_ID INNER JOIN factory ON data.Factory_ID = factory.Factory_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID LEFT outer JOIN color ON data.Color_ID = color.Color_ID LEFT outer  JOIN size ON data.Size_ID = size.Size_ID LEFT outer  JOIN sort ON data.Sort_ID = sort.Sort_ID";

                string query = "select CustomerBill_ID from customer_bill inner join transitions on customer_bill.Branch_BillNumber=transitions.Bill_Number where Paid_Status=1 and Type_Buy='كاش' and Date between '" + d + "' and '" + d2 + "'";
                MySqlCommand com = new MySqlCommand(query, dbconnection);
                MySqlDataReader dr = com.ExecuteReader();
                string str = "";
                while (dr.Read())
                {
                    str += dr[0].ToString() + ",";
                }
                dr.Close();

                query = "select CustomerBill_ID from customer_bill  where Paid_Status=1 and Type_Buy='آجل' and AgelBill_PaidDate between '" + d + "' and '" + d2 + "'";
                com = new MySqlCommand(query, dbconnection);
                dr = com.ExecuteReader();

                while (dr.Read())
                {
                    str += dr[0].ToString() + ",";
                }
                dr.Close();

                str += 0;

                query = "select CustomerReturnBill_ID from customer_return_bill where  Date between '" + d + "' and '" + d2 + "'";
                com = new MySqlCommand(query, dbconnection);
                dr = com.ExecuteReader();
                string str1 = "";
                while (dr.Read())
                {
                    str1 += dr[0].ToString() + ",";
                }
                dr.Close();
                str1 += 0;
                
                _Table3 = getTotalSoldProfitOfCompany(_Table3, DataTableRelations, d, d2, str);
                _Table3 = getTotalReturnedProfitOfCompany(_Table3, DataTableRelations, d, d2, str1);
                _Table = _Table3;

                DataTable temp = peraperDataTable3();
             
                double totalDelegateProfit = 0.0;
                for (int i = 0; i < _Table3.Rows.Count; i++)
                {
                    int x = i;
                    for (int j = 1; j < _Table.Rows.Count; j++)
                    {
                        if (_Table3.Rows[i]["FactoryID"].ToString() == _Table.Rows[j]["FactoryID"].ToString())
                        {
                            _Table3.Rows[i]["ValueDelegate"] = Convert.ToDouble(_Table.Rows[j]["ValueDelegate"].ToString()) + Convert.ToDouble(_Table3.Rows[i]["ValueDelegate"].ToString());
                           i++;
                        }
                    }
                    temp.Rows.Add(_Table3.Rows[x].ItemArray);
                }
                for (int i = 0; i < temp.Rows.Count; i++)
                {
                    totalDelegateProfit += Convert.ToDouble(temp.Rows[i]["ValueDelegate"].ToString());
                }
                gridControl2.DataSource = temp;
                gridView1.SelectRows(0, gridView1.RowCount - 1);

                labTotalDelegateProfit.Text = totalDelegateProfit.ToString();
                _Table2 = getSoldQuantity(_Table2 ,itemName, DataTableRelations, d, d2,str);
                _Table2 = getReturnedQuantity(_Table2, itemName, DataTableRelations, d, d2,str1);

                gridControl1.DataSource = _Table2;
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("حددالمندوب");
                txtDelegateID.Focus();
            }
            dbconnection.Close();
            dbconnection2.Close();
        }

        private void gridView2_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            try
            {
                if (e.Column.Name == "PercentageDelegate")
                {
                    GridView view = (GridView)sender;
                    DataRow dataRow = view.GetFocusedDataRow();
                    double oldValue = Convert.ToDouble(dataRow["ValueDelegate"].ToString());
                    double totalValue = Convert.ToDouble(labTotalDelegateProfit.Text);
                    totalValue -= oldValue;
                    double cellValue = Convert.ToDouble(e.Value);
                    double totalItemValue = Convert.ToDouble(dataRow["Quantity"].ToString());
                    double re = cellValue * totalItemValue;

                    view.SetRowCellValue(view.GetSelectedRows()[0], "ValueDelegate", re);

                    labTotalDelegateProfit.Text = (cellValue * totalItemValue) + totalValue + "";

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void gridView1_SelectionChanged(object sender, DevExpress.Data.SelectionChangedEventArgs e)
        {
            try
            {
                GridView view = (GridView)sender;
                DataRow dataRow = view.GetFocusedDataRow();
                double total = 0.0;
                double result = Convert.ToDouble(labTotalDelegateProfit.Text);
                total = Convert.ToDouble(dataRow["ValueDelegate"].ToString());
                if (view.GetFocusedValue().ToString() == "False")
                {
                    result = result - total;
                }
                else
                {
                    result = result + total;
                }

                labTotalDelegateProfit.Text = result.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                dbconnection.Open();
                if (txtDelegateID.Text != "" && labTotalDelegateProfit.Text != "")
                {
                    string query = "INSERT INTO Delegates_Profit (Delegate_ID,Delegate_Profit, Date_From, Date_To) VALUES(@Delegate_ID,@Delegate_Profit, @Date_From, @Date_To)";
                    MySqlCommand command = new MySqlCommand(query, dbconnection);

                    command.Parameters.Add("@Delegate_ID", MySqlDbType.Int16);
                    command.Parameters["@Delegate_ID"].Value = txtDelegateID.Text;
                    command.Parameters.Add("@Delegate_Profit", MySqlDbType.Double);
                    command.Parameters["@Delegate_Profit"].Value = labTotalDelegateProfit.Text;
                    command.Parameters.Add("@Date_From", MySqlDbType.Date);
                    command.Parameters["@Date_From"].Value = dateTimeFrom.Value.Date;
                    command.Parameters.Add("@Date_To", MySqlDbType.Date);
                    command.Parameters["@Date_To"].Value = dateTimeTo.Value.Date;

                    command.ExecuteNonQuery();
                    reset();
                }
                else
                {
                    MessageBox.Show("ادخل البيانات بالكامل");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }

        private void newChoose_Click(object sender, EventArgs e)
        {
            try
            {
                reset();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //functions
        public DataTable getSoldQuantity(DataTable _Table,  string itemName, string DataTableRelations, string dateFrom, string dateTo,string strcustomerBill_IDs)
        {
            string query = "select product_bill.Data_ID," + itemName + ",sum(Quantity) as 'الكمية',PriceAD from  product_bill inner join data on product_bill.Data_ID=data.Data_ID " + DataTableRelations + " inner join delegate on delegate.Delegate_ID=product_bill.Delegate_ID  where CustomerBill_ID in (" + strcustomerBill_IDs + ") and product_bill.Delegate_ID=" + txtDelegateID.Text + "  group by Data.Data_ID  ";
            MySqlCommand com = new MySqlCommand(query, dbconnection);
            MySqlDataReader dr = com.ExecuteReader();

            while (dr.Read())
            {
                dbconnection2.Close();
                dbconnection2.Open();
                string q = "SELECT  sellprice.PercentageDelegate from sellprice where Data_ID=" + dr[0].ToString() + "  ORDER BY  Date desc LIMIT 1";
                MySqlCommand c = new MySqlCommand(q, dbconnection2);
                double PercentageDelegate =Convert.ToDouble(c.ExecuteScalar().ToString());
                if (PercentageDelegate == 0)
                {
                    DataRow row = _Table.NewRow();
                    row["Data_ID"] = dr[0].ToString();
                    row["CodeName"] = dr[1].ToString();
                    row["PercentageDelegate"] = PercentageDelegate;
                    row["Quantity"] = dr[2].ToString();
                    row["Cost"] = Convert.ToDouble(dr[2].ToString()) * Convert.ToDouble(dr[3].ToString());
                    double str = Convert.ToDouble(dr[2].ToString()) * Convert.ToDouble(dr[3].ToString());

                    row["ValueDelegate"] = PercentageDelegate / 100 * str;
                    _Table.Rows.Add(row);
                    dbconnection2.Close();

                }
           
            }
            dr.Close();

            return _Table;
        }
        public DataTable getReturnedQuantity(DataTable _Table, string itemName, string DataTableRelations, string dateFrom, string dateTo,string customerReturnBill_IDs)
        {
            string query = "select customer_return_bill_details.Data_ID," + itemName + ",sum(TotalMeter) as 'الكمية',PriceAD from  customer_return_bill_details inner join data on data.Data_ID=customer_return_bill_details.Data_ID " + DataTableRelations + " inner join delegate on delegate.Delegate_ID=customer_return_bill_details.Delegate_ID  where CustomerReturnBill_ID in (" + customerReturnBill_IDs + ") and customer_return_bill_details.Delegate_ID=" + txtDelegateID.Text + "  group by customer_return_bill_details.Data_ID  ";

            MySqlCommand com = new MySqlCommand(query, dbconnection);
            MySqlDataReader dr = com.ExecuteReader();
            DataTable temp = peraperDataTable();
            bool flag = true;
            while (dr.Read())
            {
                foreach (DataRow item in _Table.Rows)
                {
                    if (item[0].ToString() == dr[0].ToString())
                    {
                        double dd = Convert.ToDouble(item["Quantity"].ToString());
                        double d = Convert.ToDouble(dr[2].ToString());
                        d = dd - d;
                        string x = (Convert.ToDouble(item["Quantity"].ToString()) - Convert.ToDouble(dr[2].ToString())).ToString();
                        item["Quantity"] = x;
                        item["Cost"] = Convert.ToDouble(item["Quantity"].ToString()) * Convert.ToDouble(dr[3].ToString());
                        double str = Convert.ToDouble(item["Quantity"].ToString()) * Convert.ToDouble(dr[3].ToString());

                        item["ValueDelegate"] =Convert.ToDouble(item["ValueDelegate"].ToString()) - Convert.ToDouble(item["PercentageDelegate"].ToString()) / 100 * str;

                        flag = false;
                    }

                }
         
                if (flag)
                {
                    dbconnection2.Close();
                    dbconnection2.Open();
                    string q = "SELECT  sellprice.PercentageDelegate from sellprice where Data_ID=" + dr[0].ToString() + "  ORDER BY  Date desc LIMIT 1";
                    MySqlCommand c = new MySqlCommand(q, dbconnection2);
                    double PercentageDelegate = Convert.ToDouble(c.ExecuteScalar().ToString());
                    if (PercentageDelegate == 0)
                    {
                        DataRow row = temp.NewRow();
                        row["Data_ID"] = dr[0].ToString();
                        row["CodeName"] = dr[1].ToString();
                        row["Quantity"] = dr[2].ToString();

                        row["PercentageDelegate"] = PercentageDelegate;
                        row["Cost"] = Convert.ToDouble(dr[2].ToString()) * Convert.ToDouble(dr[3].ToString());
                        double str = Convert.ToDouble(dr[2].ToString()) * Convert.ToDouble(dr[3].ToString());
                        row["ValueDelegate"] = - PercentageDelegate / 100 * str;

                        temp.Rows.Add(row);
                        dbconnection2.Close();
                    }
                   
                }

            }
            dr.Close();
            foreach (DataRow item in temp.Rows)
            {
                _Table.Rows.Add(item.ItemArray);
            }

            return _Table;
        }

        public DataTable getTotalSoldProfitOfCompany(DataTable _Table, string DataTableRelations, string dateFrom, string dateTo,string customerBill_IDs)
        {
            string query = "select data.Factory_ID,factory.Factory_Name,sum(Quantity) as 'الكمية',PriceAD,product_bill.Data_ID from  product_bill inner join data on product_bill.Data_ID=data.Data_ID " + DataTableRelations + " inner join delegate on delegate.Delegate_ID=product_bill.Delegate_ID  where CustomerBill_ID in (" + customerBill_IDs + ") and product_bill.Delegate_ID=" + txtDelegateID.Text + "  group by product_bill.Data_ID ";

            MySqlCommand com = new MySqlCommand(query, dbconnection);
            MySqlDataReader dr = com.ExecuteReader();

            while (dr.Read())
            {
                dbconnection2.Open();
                string q = "SELECT  sellprice.PercentageDelegate from sellprice where Data_ID=" + dr[4].ToString() + "  ORDER BY  Date desc LIMIT 1";
                MySqlCommand c = new MySqlCommand(q, dbconnection2);
                double PercentageDelegate = Convert.ToDouble(c.ExecuteScalar().ToString());
                if (PercentageDelegate > 0)
                {
                    DataRow row = _Table.NewRow();

                    row["FactoryID"] = dr[0].ToString();
                    row["FactoryName"] = dr[1].ToString();
                    row["PercentageDelegate"] = PercentageDelegate;
                    row["Quantity"] = dr[2].ToString();
                    row["Cost"] = Convert.ToDouble(dr[2].ToString()) * Convert.ToDouble(dr[3].ToString());
                    double str = Convert.ToDouble(dr[2].ToString()) * Convert.ToDouble(dr[3].ToString());

                    row["ValueDelegate"] = PercentageDelegate / 100 * str;
                    row["Data_ID"] = dr[4].ToString();
                    _Table.Rows.Add(row);
                    dbconnection2.Close();
                }
                
            }
            dr.Close();

            return _Table;
        }
        public DataTable getTotalReturnedProfitOfCompany(DataTable _Table, string DataTableRelations, string dateFrom, string dateTo,string customerBill_IDs)
        {
            string query = "select data.Factory_ID,factory.Factory_Name,sum(TotalMeter) as 'الكمية',PriceAD,customer_return_bill_details.Data_ID from  customer_return_bill_details inner join data on customer_return_bill_details.Data_ID=data.Data_ID " + DataTableRelations + " inner join delegate on delegate.Delegate_ID=customer_return_bill_details.Delegate_ID  where CustomerReturnBill_ID in (" + customerBill_IDs + ") and customer_return_bill_details.Delegate_ID=" + txtDelegateID.Text + "  group by customer_return_bill_details.Data_ID ";

            MySqlCommand com = new MySqlCommand(query, dbconnection);
            MySqlDataReader dr = com.ExecuteReader();
            DataTable temp = peraperDataTable3();
            bool flag = true;
            while (dr.Read())
            {
                dbconnection2.Open();
                string q = "SELECT  sellprice.PercentageDelegate from sellprice where Data_ID=" + dr[4].ToString() + "  ORDER BY  Date desc LIMIT 1";
                MySqlCommand c = new MySqlCommand(q, dbconnection2);
                double PercentageDelegate = Convert.ToDouble(c.ExecuteScalar().ToString());
                if (PercentageDelegate > 0)
                {
                    foreach (DataRow item in _Table.Rows)
                    {
                        if (item[4].ToString() == dr[4].ToString())
                        {
                            item["Quantity"] = (Convert.ToDouble(item[2].ToString()) - Convert.ToDouble(dr[2].ToString())).ToString();
                            item["Cost"] = Convert.ToDouble(item["Quantity"].ToString()) * Convert.ToDouble(dr[3].ToString());
                            double str = Convert.ToDouble(item["Quantity"].ToString()) * Convert.ToDouble(dr[3].ToString());

                            item["ValueDelegate"] =Convert.ToDouble(item["ValueDelegate"].ToString()) - (PercentageDelegate / 100 * str);
                        
                            flag = false;
                        }
                    }
                    if (flag)
                    {
                        DataRow row = temp.NewRow();

                        row["PercentageDelegate"] = PercentageDelegate;
                        row["Quantity"] = dr[2].ToString();
                        row["FactoryID"] = dr[0].ToString();
                        row["FactoryName"] = dr[1].ToString();
                        row["Cost"] = Convert.ToDouble(dr[2].ToString()) * Convert.ToDouble(dr[3].ToString());
                        double str = Convert.ToDouble(dr[2].ToString()) * Convert.ToDouble(dr[3].ToString());
                        row["ValueDelegate"] = - PercentageDelegate / 100 * str;
                        row["Data_ID"] = dr[4].ToString();
                        temp.Rows.Add(row);
                    }
                }

            }
            dr.Close();
            foreach (DataRow item in temp.Rows)
            {
                _Table.Rows.Add(item.ItemArray);
            }
           
            return _Table;
        }

        public DataTable peraperDataTable()
        {
            DataTable _Table = new DataTable("Table");

            _Table.Columns.Add(new DataColumn("Data_ID", typeof(int)));
            _Table.Columns.Add(new DataColumn("CodeName", typeof(string)));
            _Table.Columns.Add(new DataColumn("PercentageDelegate", typeof(string)));
            _Table.Columns.Add(new DataColumn("ValueDelegate", typeof(string)));
            _Table.Columns.Add(new DataColumn("Cost", typeof(string)));
            _Table.Columns.Add(new DataColumn("Quantity", typeof(string)));
            return _Table;
        }
        //total delegate profit for each company which its items have delegate_percentage 
        public DataTable peraperDataTable2()
        {
            DataTable _Table = new DataTable("Table2");

            _Table.Columns.Add(new DataColumn("FactoryID", typeof(int)));
            _Table.Columns.Add(new DataColumn("FactoryName", typeof(string)));
            _Table.Columns.Add(new DataColumn("PercentageDelegate", typeof(string)));
            _Table.Columns.Add(new DataColumn("ValueDelegate", typeof(string)));
            _Table.Columns.Add(new DataColumn("Cost", typeof(string)));
            _Table.Columns.Add(new DataColumn("Quantity", typeof(string)));
            return _Table;
        }
        public DataTable peraperDataTable3()
        {
            DataTable _Table = new DataTable("Table3");

            _Table.Columns.Add(new DataColumn("Data_ID", typeof(int)));
            _Table.Columns.Add(new DataColumn("FactoryID", typeof(int)));
            _Table.Columns.Add(new DataColumn("FactoryName", typeof(string)));
            _Table.Columns.Add(new DataColumn("PercentageDelegate", typeof(string)));
            _Table.Columns.Add(new DataColumn("ValueDelegate", typeof(string)));
            _Table.Columns.Add(new DataColumn("Cost", typeof(string)));
            _Table.Columns.Add(new DataColumn("Quantity", typeof(string)));
            return _Table;
        }
        public void reset()
        {
            dateTimeFrom.Value = DateTime.Now;
            dateTimeTo.Value = DateTime.Now;
            txtDelegateID.Text = "";
            comDelegate.Text = "";
            gridControl1.DataSource = null;
            gridControl2.DataSource = null;
            labTotalDelegateProfit.Text = "";
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            try
            {
                dataX d = new dataX(dateTimeFrom.Text, dateTimeTo.Text, comDelegate.Text, "");
                d.delegateProfit = labTotalDelegateProfit.Text;
                int[] arr = gridView1.GetSelectedRows();
                d.company_profit_list = new List<company_profit>();
                foreach (int item in arr)
                {
                   company_profit x = new company_profit();
                   x.companyName= gridView1.GetRowCellValue(item, "FactoryName").ToString();
                   x.delegateProfit = gridView1.GetRowCellValue(item, "ValueDelegate").ToString();
                    d.company_profit_list.Add(x);
                }
                MainForm.displayDelegateReport(gridControl1, d);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}

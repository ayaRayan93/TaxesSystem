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
    public partial class DelegateSalesForProduct : Form
    {
        private MySqlConnection dbconnection;
        public bool loaded = false;
        public DelegateSalesForProduct()
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

        private void DelegateSalesForProduct_Load(object sender, EventArgs e)
        {
            try
            {
                dbconnection.Open();

                string query = "select * from factory ";

                MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                DataTable dt = new DataTable();
                da.Fill(dt);
                comFactory.DataSource = dt;
                comFactory.DisplayMember = dt.Columns["Factory_Name"].ToString();
                comFactory.ValueMember = dt.Columns["Factory_ID"].ToString();
                comFactory.Text = "";
                txtFactory.Text = "";

                query = "select * from delegate ";
                da = new MySqlDataAdapter(query, dbconnection);
                dt = new DataTable();
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

        private void dateTimeFrom_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                dbconnection.Open();
                DateTime date = dateTimeFrom.Value;
                string d = date.ToString("yyyy-MM-dd HH:mm:ss");
                DateTime date2 = dateTimeTo.Value;
                string d2 = date2.ToString("yyyy-MM-dd HH:mm:ss");

                string query = "select factory.Factory_ID,Factory_Name from customer_bill inner join product_bill on customer_bill.CustomerBill_ID=product_bill.CustomerBill_ID inner join data on data.Data_ID=product_bill.Data_ID inner join factory on data.Factory_ID=factory.Factory_ID  where Paid_Status=1 and Bill_Date between '" + d + "' and '" + d2 + "'";
                MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                DataTable dt = new DataTable();
                da.Fill(dt);
                comFactory.DataSource = dt;
                comFactory.DisplayMember = dt.Columns["Factory_Name"].ToString();
                comFactory.ValueMember = dt.Columns["Factory_ID"].ToString();
                comFactory.Text = "";
                txtFactory.Text = "";
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
                        txtDelegateID.Text = comDelegate.SelectedValue.ToString();
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
        }
        private void comFactory_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (loaded)
                {
                    try
                    {
                        txtFactory.Text = comFactory.SelectedValue.ToString();
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
        }
        private void txtFactory_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    dbconnection.Open();
                    string query = "select Factory_Name from factory where Factory_ID=" + txtFactory.Text + "";
                    MySqlCommand com = new MySqlCommand(query, dbconnection);
                    if (com.ExecuteScalar() != null)
                    {
                        Name = (string)com.ExecuteScalar();
                        comFactory.Text = Name;
                        txtDelegateID.Focus();
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
        private void txtDelegateID_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    dbconnection.Open();
                    string query = "select Delegate_Name from delegate where Delegate_ID=" + txtDelegateID.Text + "";
                    MySqlCommand com = new MySqlCommand(query, dbconnection);
                    if (com.ExecuteScalar() != null)
                    {
                        Name = (string)com.ExecuteScalar();
                        comDelegate.Text = Name;
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
                DataTable _Table = peraperDataTable();

                DateTime date = dateTimeFrom.Value;
                string d = date.ToString("yyyy-MM-dd HH:mm:ss");
                DateTime date2 = dateTimeTo.Value;
                string d2 = date2.ToString("yyyy-MM-dd HH:mm:ss");
                string itemName = "concat( product.Product_Name,' ',type.Type_Name,' ',factory.Factory_Name,' ',groupo.Group_Name,' ' ,color.Color_Name,' ' ,size.Size_Value ," +/*sort.Sort_Value*/"" + " data.Classification,data.Description)as 'البند'";
                string DataTableRelations = "INNER JOIN type ON type.Type_ID = data.Type_ID INNER JOIN product ON product.Product_ID = data.Product_ID INNER JOIN factory ON data.Factory_ID = factory.Factory_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID LEFT outer JOIN color ON data.Color_ID = color.Color_ID LEFT outer  JOIN size ON data.Size_ID = size.Size_ID LEFT outer  JOIN sort ON data.Sort_ID = sort.Sort_ID";

                _Table = getSoldQuantity(_Table,itemName,DataTableRelations,d,d2);
                _Table = getReturnedQuantity(_Table, itemName, DataTableRelations, d, d2);

                gridControl1.DataSource = _Table;
            }
            catch (Exception ex)
            {
                MessageBox.Show("حدد الشركة والمندوب");
                txtFactory.Focus();
            }
            dbconnection.Close();
        }

        //functions
        public DataTable getSoldQuantity(DataTable _Table,string itemName,string DataTableRelations,string dateFrom,string dateTo)
        {
            string query = "select product_bill.Data_ID," + itemName + ",sum(Quantity) as 'الكمية',PriceAD from customer_bill inner join product_bill on customer_bill.CustomerBill_ID=product_bill.CustomerBill_ID inner join data on data.Data_ID=product_bill.Data_ID " + DataTableRelations + " inner join delegate on delegate.Delegate_ID=product_bill.Delegate_ID  where Paid_Status=1 and Bill_Date between '" + dateFrom + "' and '" + dateTo + "' and product_bill.Delegate_ID=" + txtDelegateID.Text + " and data.Factory_ID=" + txtFactory.Text + " group by data.Code ,customer_bill.CustomerBill_ID ,delegate.Delegate_ID ";
            MySqlCommand com = new MySqlCommand(query, dbconnection);
            MySqlDataReader dr = com.ExecuteReader();

            while (dr.Read())
            {
                DataRow row = _Table.NewRow();

                row["Data_ID"] = dr[0].ToString();
                row["CodeName"] = dr[1].ToString();
                row["QuantitySaled"] = dr[2].ToString();
                row["Quantity"] = dr[2].ToString();
                row["Cost"] =Convert.ToDouble(dr[2].ToString())* Convert.ToDouble(dr[3].ToString());
                _Table.Rows.Add(row);
            }
            dr.Close();

            return _Table;
        }
        public DataTable getReturnedQuantity(DataTable _Table, string itemName, string DataTableRelations, string dateFrom, string dateTo)
        {
            string query = "select customer_return_bill_details.Data_ID," + itemName + ",sum(TotalMeter) as 'الكمية',PriceAD from customer_return_bill inner join customer_return_bill_details on customer_return_bill.CustomerReturnBill_ID=customer_return_bill_details.CustomerReturnBill_ID inner join data on data.Data_ID=customer_return_bill_details.Data_ID " + DataTableRelations + " inner join delegate on delegate.Delegate_ID=customer_return_bill_details.Delegate_ID  where Date between '" + dateFrom + "' and '" + dateTo + "' and customer_return_bill_details.Delegate_ID=" + txtDelegateID.Text + " and data.Factory_ID=" + txtFactory.Text + " group by data.Code ,customer_return_bill.CustomerReturnBill_ID ,delegate.Delegate_ID ";
            MySqlCommand com = new MySqlCommand(query, dbconnection);
            MySqlDataReader dr = com.ExecuteReader();
            DataTable temp = peraperDataTable();
            DataTable temp1 = peraperDataTable();
            bool flag = true;
            while (dr.Read())
            {
                foreach (DataRow item in _Table.Rows)
                {
                    if (item[0].ToString() == dr[0].ToString())
                    {
                        item["QuantityReturned"] = dr[2].ToString();
                        item["Quantity"] = (Convert.ToDouble(item[2].ToString()) - Convert.ToDouble(dr[2].ToString())).ToString();
                        item["Cost"] = Convert.ToDouble(item["Quantity"].ToString()) * Convert.ToDouble(dr[3].ToString());
                        flag = false;
                    }

                }
                if (flag)
                {
                    DataRow row = temp.NewRow();

                    row["QuantityReturned"] = dr[2].ToString();
                    row["Quantity"] = dr[2].ToString();
                    row["Data_ID"] = dr[0].ToString();
                    row["CodeName"] = dr[1].ToString();
                    row["Cost"] = Convert.ToDouble(dr[2].ToString()) * Convert.ToDouble(dr[3].ToString());

                    temp.Rows.Add(row);
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

            _Table.Columns.Add(new DataColumn("Data_ID", typeof(int)) );
            _Table.Columns.Add(new DataColumn("CodeName", typeof(string)));
            _Table.Columns.Add(new DataColumn("QuantitySaled", typeof(string)));
            _Table.Columns.Add(new DataColumn("QuantityReturned", typeof(string)));
            _Table.Columns.Add(new DataColumn("Cost", typeof(string)));
            _Table.Columns.Add(new DataColumn("Quantity", typeof(string)));
            return _Table;
        }
    }

}

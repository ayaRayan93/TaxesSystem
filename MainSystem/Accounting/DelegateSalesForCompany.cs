﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using MySql.Data.MySqlClient;
using DevExpress.XtraGrid.Columns;

namespace MainSystem
{
    public partial class DelegateSalesForCompany : DevExpress.XtraEditors.XtraForm
    {
        private MySqlConnection dbconnection;
        bool loaded = false;
        public DelegateSalesForCompany()
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
        private void DelegateSalesForCompany_Load(object sender, EventArgs e)
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

                query = "select * from factory ";
                da = new MySqlDataAdapter(query, dbconnection);
                dt = new DataTable();
                da.Fill(dt);
                comFactory.DataSource = dt;
                comFactory.DisplayMember = dt.Columns["Factory_Name"].ToString();
                comFactory.ValueMember = dt.Columns["Factory_ID"].ToString();
                comFactory.Text = "";
                txtFactory.Text = "";

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

                string query = "select DISTINCT factory.Factory_ID,Factory_Name from customer_bill inner join product_bill on customer_bill.CustomerBill_ID=product_bill.CustomerBill_ID inner join data on data.Data_ID=product_bill.Data_ID inner join factory on data.Factory_ID=factory.Factory_ID  where Paid_Status=1 and Bill_Date between '" + d + "' and '" + d2 + "'";
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
                        txtFactory.Focus();
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
                DateTime date = dateTimeFrom.Value;
                string d = date.ToString("yyyy-MM-dd HH:mm:ss");
                DateTime date2 = dateTimeTo.Value;
                string d2 = date2.ToString("yyyy-MM-dd HH:mm:ss");

                string query = "select CustomerBill_ID from customer_bill where Paid_Status=1 and Bill_Date between '" + d + "' and '" + d2 + "'";
                MySqlCommand com = new MySqlCommand(query, dbconnection);
                MySqlDataReader dr = com.ExecuteReader();
                string str = "";
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
                DataTable _Table = peraperDataTable();
                if (txtDelegateID.Text != "")
                {
                    query = " and delegate.Delegate_ID= " + txtDelegateID.Text;
                    gridView1.Columns["Delegate_Name"].Visible = false;
                }
                else
                {
                    query = "";
                    gridView1.Columns["Delegate_Name"].Visible = true;
                }

                if (txtFactory.Text != "")
                {
                    query += " and data.Factory_ID=" + txtFactory.Text;
                    gridView1.Columns["Factory_Name"].Visible = false;
                }
                else
                {
                    gridView1.Columns["Factory_Name"].Visible = true;
                }
                _Table=getTotalSales(_Table, str, query);
                _Table = getTotalReturn(_Table, str1, query);

                GridControl1.DataSource = _Table;
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
                txtDelegateID.Text = "";
                comDelegate.Text = "";
                txtFactory.Text = "";
                comFactory.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //functions
        public DataTable getTotalSales(DataTable _Table,string customerBill_ids,string subQuery)
        {
            string query = "select delegate.Delegate_ID,Delegate_Name,Factory_Name,sum(product_bill.PriceAD*Quantity) from product_bill inner join delegate on delegate.Delegate_ID=product_bill.Delegate_ID inner join data on data.Data_ID=product_bill.Data_ID inner join factory on data.Factory_ID=factory.Factory_ID where CustomerBill_ID in (" + customerBill_ids+ ") "+subQuery+ " group by delegate.Delegate_ID ";
            MySqlCommand com = new MySqlCommand(query, dbconnection);
            MySqlDataReader dr = com.ExecuteReader();

            while (dr.Read())
            {
                DataRow row = _Table.NewRow();

                row["Delegate_ID"] = dr[0].ToString();
                row["Delegate_Name"] = dr[1].ToString();
                row["Factory_Name"] = dr[2].ToString();
                row["TotalSales"] = dr[3].ToString();
                row["Safaya"] = dr[3].ToString();
                _Table.Rows.Add(row);
            }
            dr.Close();

            return _Table;
        }
        public DataTable getTotalReturn(DataTable _Table, string CustomerReturnBill_IDs, string subQuery)
        {
            string query = "select delegate.Delegate_ID,Delegate_Name,Factory_Name,sum(TotalAD) from customer_return_bill_details inner join delegate on delegate.Delegate_ID=customer_return_bill_details.Delegate_ID inner join data on data.Data_ID=customer_return_bill_details.Data_ID inner join factory on data.Factory_ID=factory.Factory_ID where CustomerReturnBill_ID in (" + CustomerReturnBill_IDs + ")  "+ subQuery + " group by delegate.Delegate_ID ";
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
                        item["TotalReturn"] = dr[3].ToString();
                        item["Safaya"] = (Convert.ToDouble(item["TotalSales"].ToString()) - Convert.ToDouble(dr[3].ToString())).ToString();
                        flag = false;
                    }

                }
                if (flag)
                {
                    DataRow row = temp.NewRow();

                    row["TotalReturn"] = dr[3].ToString();
                    row["Safaya"] = dr[3].ToString();
                    row["Delegate_ID"] = dr[0].ToString();
                    row["Delegate_Name"] = dr[1].ToString();
                    row["Factory_Name"] = dr[1].ToString();
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
            DataTable _Table = new DataTable("Table2");

            _Table.Columns.Add(new DataColumn("Delegate_ID", typeof(int)));
            _Table.Columns.Add(new DataColumn("Delegate_Name", typeof(string)));
            _Table.Columns.Add(new DataColumn("Factory_Name", typeof(string)));
            _Table.Columns.Add(new DataColumn("TotalSales", typeof(string)));
            _Table.Columns.Add(new DataColumn("TotalReturn", typeof(string)));
            _Table.Columns.Add(new DataColumn("Safaya", typeof(string)));
            return _Table;
        }

    }
}
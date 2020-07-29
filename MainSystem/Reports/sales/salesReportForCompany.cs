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

namespace TaxesSystem
{
    public partial class salesReportForCompany : DevExpress.XtraEditors.XtraForm
    {
        private MySqlConnection dbconnection;
        bool loaded = false;
        MainForm MainForm;
        public salesReportForCompany(MainForm MainForm)
        {
            try
            {
                InitializeComponent();
                dbconnection = new MySqlConnection(connection.connectionString);
                this.MainForm = MainForm;
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
                string query = "select * from branch ";
                MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                DataTable dt = new DataTable();
                da.Fill(dt);
                comBranch.DataSource = dt;
                comBranch.DisplayMember = dt.Columns["Branch_Name"].ToString();
                comBranch.ValueMember = dt.Columns["Branch_ID"].ToString();
                comBranch.Text = "";
                txtBranchID.Text = "";

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
                string d = date.ToString("yyyy-MM-dd ");
                d += "00:00:00";
                DateTime date2 = dateTimeTo.Value;
                string d2 = date2.ToString("yyyy-MM-dd ");
                d2 += "23:59:59";
                string supQuery = "";
                if (comBranch.Text != "")
                {
                    supQuery= " and customer_bill.Branch_ID=" + txtBranchID.Text; 
                }
                string query = "select CustomerBill_ID from customer_bill  where Bill_Date between '" + d + "' and '" + d2 + "' "+supQuery;

                MySqlCommand com = new MySqlCommand(query, dbconnection);
                MySqlDataReader dr = com.ExecuteReader();
                string str = "";
                while (dr.Read())
                {
                    str += dr[0].ToString() + ",";
                }
                dr.Close();

                str += 0;
                string supQuery1 = "";
                if (comBranch.Text != "")
                {
                    supQuery1 = " and customer_return_bill.Branch_ID=" + txtBranchID.Text;
                }
                query = "select CustomerReturnBill_ID from customer_return_bill where  Date between '" + d + "' and '" + d2 + "' "+supQuery1;

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
              

                _Table = getTotalSales(_Table, str);
                _Table = getTotalReturn(_Table, str1);

                GridControl1.DataSource = _Table;


                CalTotal(_Table);
              
            }
            catch
            {
                MessageBox.Show("اختار الفرع");
            }
            dbconnection.Close();
        }
        private void newChoose_Click(object sender, EventArgs e)
        {
            try
            {
                txtFactory.Text = "";
                comFactory.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void txtBranchID_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    dbconnection.Close();
                    string query = "select Branch_Name from branch where Branch_ID=" + txtBranchID.Text;
                    MySqlCommand comand = new MySqlCommand(query, dbconnection);
                    dbconnection.Open();
                    string Branch_Name = comand.ExecuteScalar().ToString();
                    dbconnection.Close();
                    comBranch.Text = Branch_Name;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void comBranch_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (loaded)
                {
                    txtBranchID.Text = comBranch.SelectedValue.ToString();
                
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //functions
        public DataTable getTotalSales(DataTable _Table, string customerBill_ids)
        {
            string query = "";
            if (txtFactory.Text != "")
            {
                query = "select data.Factory_ID, Factory_Name,FORMAT(sum(product_bill.PriceAD*Quantity),2) from product_bill  inner join data on data.Data_ID=product_bill.Data_ID inner join factory on data.Factory_ID=factory.Factory_ID where CustomerBill_ID in (" + customerBill_ids + ")  and data.Factory_ID=" + txtFactory.Text + "  ";
            }
            else
            {
                query = "select data.Factory_ID, Factory_Name,FORMAT(sum(product_bill.PriceAD*Quantity),2) from product_bill  inner join data on data.Data_ID=product_bill.Data_ID left join factory on data.Factory_ID=factory.Factory_ID where CustomerBill_ID in (" + customerBill_ids + ") group by  factory.Factory_ID";
            }

            MySqlCommand com = new MySqlCommand(query, dbconnection);
            MySqlDataReader dr = com.ExecuteReader();

            while (dr.Read())
            {
                DataRow row = _Table.NewRow();
                if (dr[0].ToString() != "")
                    row["Factory_ID"] = dr[0].ToString();
                if (dr[1].ToString() != "")
                    row["Factory_Name"] = dr[1].ToString();
                if (dr[2].ToString() != "")
                    row["TotalSales"] = dr[2].ToString();
                else
                    row["TotalSales"] = 0;
                if (dr[2].ToString() != "")
                    row["Safaya"] = dr[2].ToString();
                else
                    row["Safaya"] = 0;
             

                
                _Table.Rows.Add(row);
            }
            dr.Close();

            return _Table;
        }
        public DataTable getTotalReturn(DataTable _Table, string CustomerReturnBill_IDs)
        {
            string query = "";
            if (txtFactory.Text != "")
            {
                query = "select data.Factory_ID, Factory_Name,FORMAT(sum(TotalAD),2) from customer_return_bill_details inner join data on data.Data_ID=customer_return_bill_details.Data_ID inner join factory on data.Factory_ID=factory.Factory_ID where CustomerReturnBill_ID in (" + CustomerReturnBill_IDs + ")  and data.Factory_ID=" + txtFactory.Text;
            }
            else
            {
                query = "select data.Factory_ID, Factory_Name,FORMAT(sum(TotalAD),2) from customer_return_bill_details inner join data on data.Data_ID=customer_return_bill_details.Data_ID inner join factory on data.Factory_ID=factory.Factory_ID where CustomerReturnBill_ID in (" + CustomerReturnBill_IDs + ")  group by  factory.Factory_ID  ";
            }

            MySqlCommand com = new MySqlCommand(query, dbconnection);
            MySqlDataReader dr = com.ExecuteReader();
            DataTable temp = peraperDataTable();

            while (dr.Read())
            {
                bool flag = true;
                foreach (DataRow item in _Table.Rows)
                {
                    string x = dr[0].ToString();
                    if (item[0].ToString() == dr[0].ToString())
                    {
                        if (dr[2].ToString() != "")
                        {
                            item["TotalReturn"] = dr[2].ToString();
                            item["Safaya"] = (Convert.ToDouble(item["TotalSales"].ToString()) - Convert.ToDouble(dr[2].ToString())).ToString();
                        }
                        else
                        {
                            item["TotalReturn"] = 0;
                            item["Safaya"] = (Convert.ToDouble(item["TotalSales"].ToString()) - 0);
                        }
                        flag = false;
                    }
                }
                if (flag)
                {
                    DataRow row = temp.NewRow();
                    if (dr[2].ToString() != "")
                    {
                        row["TotalReturn"] = dr[2].ToString();
                        if (dr[2].ToString() != "")
                            row["Safaya"] = -Convert.ToDouble(dr[2].ToString());

                        row["Factory_ID"] = dr[0].ToString();
                        row["Factory_Name"] = dr[1].ToString();
                        temp.Rows.Add(row);
                    }
                    else
                    {
                        //row["TotalReturn"] = 0;
                        //row["Safaya"] = 0;
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
            DataTable _Table = new DataTable("Table2");
            _Table.Columns.Add(new DataColumn("Factory_ID", typeof(string)));
            _Table.Columns.Add(new DataColumn("Factory_Name", typeof(string)));
            _Table.Columns.Add(new DataColumn("TotalSales", typeof(string)));
            _Table.Columns.Add(new DataColumn("TotalReturn", typeof(string)));
            _Table.Columns.Add(new DataColumn("Safaya", typeof(decimal)));
            return _Table;
        }
        public void CalTotal(DataTable _Tabl)
        {
            double totalSales = 0, totalReturn = 0, totalSafay = 0/*, totalProfit = 0*/;
            foreach (DataRow item in _Tabl.Rows)
            {
                if (item["TotalSales"].ToString() != "")
                    totalSales += Convert.ToDouble(item["TotalSales"].ToString());
                if (item["TotalReturn"].ToString() != "")
                    totalReturn += Convert.ToDouble(item["TotalReturn"].ToString());
                if (item["Safaya"].ToString() != "")
                    totalSafay += Convert.ToDouble(item["Safaya"].ToString());
            }
            txtTotalSales.Text = totalSales.ToString("0.00");
            txtTotalReturn.Text = totalReturn.ToString("0.00");
            txtTotalSafay.Text = totalSafay.ToString("0.00");

        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            try
            {
                dataX d = new dataX(dateTimeFrom.Text, dateTimeTo.Text, "", comFactory.Text);
                MainForm.displayCompanyReport(GridControl1, d,"تقرير مبيعات الشركات");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

    }

}
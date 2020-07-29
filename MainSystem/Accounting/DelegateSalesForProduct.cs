﻿using DevExpress.XtraGrid.Views.Grid;
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
    public partial class DelegateSalesForProduct : Form
    {
        private MySqlConnection dbconnection;
        public bool loaded = false;
        MainForm MainForm;
        public DelegateSalesForProduct(MainForm MainForm)
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
                
                query = "select * from branch";
                da = new MySqlDataAdapter(query, dbconnection);
                dt = new DataTable();
                da.Fill(dt);
                comBranch.DataSource = dt;
                comBranch.DisplayMember = dt.Columns["Branch_Name"].ToString();
                comBranch.ValueMember = dt.Columns["Branch_ID"].ToString();
                comBranch.Text = "";
                txtBranchID.Text = "";

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
                string d = date.ToString("yyyy-MM-dd ");
                d += "00:00:00";
                DateTime date2 = dateTimeTo.Value;
                string d2 = date2.ToString("yyyy-MM-dd ");
                d2 += "23:59:59";

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
        private void comBranch_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (loaded)
                {
                    txtBranchID.Text = comBranch.SelectedValue.ToString();
                    dbconnection.Open();
                    loaded = false;
                    string query = "select * from delegate where Branch_ID=" + txtBranchID.Text;
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
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
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
                string d = date.ToString("yyyy-MM-dd ");
                d += "00:00:00";
                DateTime date2 = dateTimeTo.Value;
                string d2 = date2.ToString("yyyy-MM-dd ");
                d2 += "23:59:59";
                string itemName = "concat( product.Product_Name,' ',type.Type_Name,' ',factory.Factory_Name,' ',groupo.Group_Name,' ' ,COALESCE(color.Color_Name,''),' ',COALESCE(size.Size_Value,''),' ',COALESCE(sort.Sort_Value,''),' ',COALESCE(data.Classification,''),' ',COALESCE(data.Description,''))as 'البند'";

                string DataTableRelations = "INNER JOIN type ON type.Type_ID = data.Type_ID INNER JOIN product ON product.Product_ID = data.Product_ID INNER JOIN factory ON data.Factory_ID = factory.Factory_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID LEFT outer JOIN color ON data.Color_ID = color.Color_ID LEFT outer  JOIN size ON data.Size_ID = size.Size_ID LEFT outer  JOIN sort ON data.Sort_ID = sort.Sort_ID";

                //string query = "select CustomerBill_ID from customer_bill inner join transitions on customer_bill.Branch_BillNumber=transitions.Bill_Number where Paid_Status=1 and Type_Buy='كاش' and Date between '" + d + "' and '" + d2 + "' and customer_bill.Branch_ID=" + txtBranchID.Text;
                string query = "select CustomerBill_ID from customer_bill  where  Bill_Date between '" + d + "' and '" + d2 + "' and customer_bill.Branch_ID=" + txtBranchID.Text;

                MySqlCommand com = new MySqlCommand(query, dbconnection);
                MySqlDataReader dr = com.ExecuteReader();
                string str = "";
                while (dr.Read())
                {
                    str += dr[0].ToString() + ",";
                }
                dr.Close();
                
                str += 0;

                query = "select CustomerReturnBill_ID from customer_return_bill where  Date between '" + d + "' and '" + d2 + "' and customer_return_bill.Branch_ID=" + txtBranchID.Text;
                com = new MySqlCommand(query, dbconnection);
                dr = com.ExecuteReader();
                string str1 = "";
                while (dr.Read())
                {
                    str1 += dr[0].ToString() + ",";
                }
                dr.Close();
                str1 += 0;

                _Table = getSoldQuantity1(_Table,itemName,DataTableRelations,d,d2,str);
                _Table = getReturnedQuantity(_Table, itemName, DataTableRelations, d, d2,str1);

                gridControl1.DataSource = _Table;
                CalTotal(_Table);
            }
            catch
            {
                MessageBox.Show("حدد الشركة والمندوب والفرع");
                txtFactory.Focus();
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
        //get sold quantity from cash bill
        public DataTable getSoldQuantity1(DataTable _Table,string itemName,string DataTableRelations,string dateFrom,string dateTo,string customerBill_IDs)
        {
            string query = "select product_bill.Data_ID,data.Code," + itemName + ",Quantity as 'الكمية',PriceAD from product_bill inner join data on data.Data_ID=product_bill.Data_ID " + DataTableRelations + " inner join delegate on delegate.Delegate_ID=product_bill.Delegate_ID  where CustomerBill_ID in (" + customerBill_IDs + ") and product_bill.Delegate_ID=" + txtDelegateID.Text + " and data.Factory_ID=" + txtFactory.Text + " group by product_bill.Data_ID ";
            MySqlCommand com = new MySqlCommand(query, dbconnection);
            MySqlDataReader dr = com.ExecuteReader();

            while (dr.Read())
            {
                DataRow row = _Table.NewRow();

                row["Data_ID"] = dr[0].ToString();
                row["Code"] = dr[1].ToString();
                row["CodeName"] = dr[2].ToString();
                row["QuantitySaled"] = dr[3].ToString();
                row["Quantity"] = dr[3].ToString();
                row["PriceAD"] = dr[4].ToString();
                row["Cost"] =Convert.ToDouble(dr[3].ToString())* Convert.ToDouble(dr[4].ToString());
                _Table.Rows.Add(row);
            }
            dr.Close();

            return _Table;
        }
        //get sold quantity from agel bill
        public DataTable getSoldQuantity2(DataTable _Table, string itemName, string DataTableRelations, string dateFrom, string dateTo)
        {
            string query = "select product_bill.Data_ID,data.Code," + itemName + ",sum(Quantity) as 'الكمية',PriceAD from customer_bill inner join transitions on customer_bill.Branch_BillNumber=transitions.Bill_Number inner join product_bill on customer_bill.CustomerBill_ID=product_bill.CustomerBill_ID inner join data on data.Data_ID=product_bill.Data_ID " + DataTableRelations + " inner join delegate on delegate.Delegate_ID=product_bill.Delegate_ID  where Paid_Status=1 and Type_Buy='آجل' and AgelBill_PaidDate between '" + dateFrom + "' and '" + dateTo + "' and product_bill.Delegate_ID=" + txtDelegateID.Text + " and data.Factory_ID=" + txtFactory.Text + " group by data.Code ,customer_bill.CustomerBill_ID ,delegate.Delegate_ID ";
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
                        item["QuantitySaled"] = dr[3].ToString();
                        item["Quantity"] = (Convert.ToDouble(item[3].ToString()) + Convert.ToDouble(dr[3].ToString())).ToString();
                        item["Cost"] = Convert.ToDouble(item["Quantity"].ToString()) * Convert.ToDouble(dr[4].ToString());
                        flag = false;
                    }

                }
                if (flag)
                {
                    DataRow row = temp.NewRow();

                    row["QuantitySaled"] = dr[3].ToString();
                    if (dr[2].ToString()!="")
                    row["Quantity"] = -Convert.ToDouble(dr[3].ToString());
                    row["Data_ID"] = dr[0].ToString();
                    row["CodeName"] = dr[2].ToString();
                    row["Code"] = dr[1].ToString();
                    row["Cost"] = Convert.ToDouble(dr[3].ToString()) * Convert.ToDouble(dr[4].ToString());

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

        public DataTable getReturnedQuantity(DataTable _Table, string itemName, string DataTableRelations, string dateFrom, string dateTo, string customerReturnBill_IDs)
        {
            string query = "select customer_return_bill_details.Data_ID,data.Code," + itemName + ",TotalMeter as 'الكمية',PriceAD from customer_return_bill_details inner join data on data.Data_ID=customer_return_bill_details.Data_ID " + DataTableRelations + " inner join delegate on delegate.Delegate_ID=customer_return_bill_details.Delegate_ID  where CustomerReturnBill_ID in (" + customerReturnBill_IDs + ") and customer_return_bill_details.Delegate_ID=" + txtDelegateID.Text + " and data.Factory_ID=" + txtFactory.Text + " group by data.Data_ID  ";
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
                        item["QuantityReturned"] = dr[3].ToString();
                        item["Quantity"] = (Convert.ToDouble(item[3].ToString()) - Convert.ToDouble(dr[3].ToString())).ToString();
                        item["Cost"] = Convert.ToDouble(item["Quantity"].ToString()) * Convert.ToDouble(dr[4].ToString());
                        flag = false;
                    }

                }
                if (flag)
                {
                    DataRow row = temp.NewRow();

                    row["QuantityReturned"] = dr[3].ToString();
                    row["Quantity"] = dr[3].ToString();
                    row["PriceAD"] = dr[4].ToString();
                    row["Data_ID"] = dr[0].ToString();
                    row["Code"] = dr[1].ToString();
                    row["CodeName"] = dr[2].ToString();
                    row["Cost"] =- Convert.ToDouble(dr[4].ToString()) * Convert.ToDouble(dr[3].ToString());

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
            _Table.Columns.Add(new DataColumn("Code", typeof(string)));
            _Table.Columns.Add(new DataColumn("CodeName", typeof(string)));
            _Table.Columns.Add(new DataColumn("QuantitySaled", typeof(string)));
            _Table.Columns.Add(new DataColumn("QuantityReturned", typeof(string)));
            _Table.Columns.Add(new DataColumn("PriceAD", typeof(string)));
            _Table.Columns.Add(new DataColumn("Cost", typeof(decimal)));
            _Table.Columns.Add(new DataColumn("Quantity", typeof(string)));
            return _Table;

        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            try
            {
                dataX d = new dataX(dateTimeFrom.Text, dateTimeTo.Text, comDelegate.Text, comFactory.Text);
                MainForm.displayDelegateReport2(gridControl1,"", d);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void CalTotal(DataTable _Tabl)
        {
            double totalSales = 0, totalReturn = 0, totalSafay = 0,totalCost=0;
            foreach (DataRow item in _Tabl.Rows)
            {
                if(item["QuantitySaled"].ToString()!="")
                    totalSales += Convert.ToDouble(item["QuantitySaled"].ToString());
                if (item["QuantityReturned"].ToString() != "")
                    totalReturn += Convert.ToDouble(item["QuantityReturned"].ToString());
                if (item["Quantity"].ToString() != "")
                    totalSafay += Convert.ToDouble(item["Quantity"].ToString());
                if (item["Cost"].ToString() != "")
                    totalCost += Convert.ToDouble(item["Cost"].ToString());
            }
            txtTotalSalesQuantity.Text = totalSales.ToString();
            txtTotalReturnQuantity.Text = totalReturn.ToString();
            txtTotalSafayQuantity.Text = totalSafay.ToString();
            txttotalSales.Text = totalCost.ToString();
        }
    }

}

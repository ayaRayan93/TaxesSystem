﻿using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TaxesSystem.Reports.sales
{
    public partial class SalesReportForCompanyDetails : Form
    {
        MySqlConnection dbconnection;
        bool load = false;
        bool factoryFlage = false;
        bool groupFlage = false;
        bool flagProduct = false;
        MainForm MainForm;
        public SalesReportForCompanyDetails(MainForm MainForm)
        {
            try
            {
                InitializeComponent();
                dbconnection = new MySqlConnection(connection.connectionString);
                this.MainForm=MainForm;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void SalesReportForCompanyDetails_Load(object sender, EventArgs e)
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

                query = "select * from type";
                da = new MySqlDataAdapter(query, dbconnection);
                dt = new DataTable();
                da.Fill(dt);
                comType.DataSource = dt;
                comType.DisplayMember = dt.Columns["Type_Name"].ToString();
                comType.ValueMember = dt.Columns["Type_ID"].ToString();
                comType.Text = "";
                txtType.Text = "";

                query = "select * from factory";
                da = new MySqlDataAdapter(query, dbconnection);
                dt = new DataTable();
                da.Fill(dt);
                comFactory.DataSource = dt;
                comFactory.DisplayMember = dt.Columns["Factory_Name"].ToString();
                comFactory.ValueMember = dt.Columns["Factory_ID"].ToString();
                comFactory.Text = "";
                txtFactory.Text = "";

                query = "select * from groupo";
                da = new MySqlDataAdapter(query, dbconnection);
                dt = new DataTable();
                da.Fill(dt);
                comGroup.DataSource = dt;
                comGroup.DisplayMember = dt.Columns["Group_Name"].ToString();
                comGroup.ValueMember = dt.Columns["Group_ID"].ToString();
                comGroup.Text = "";
                txtGroup.Text = "";

                query = "select * from product";
                da = new MySqlDataAdapter(query, dbconnection);
                dt = new DataTable();
                da.Fill(dt);
                comProduct.DataSource = dt;
                comProduct.DisplayMember = dt.Columns["Product_Name"].ToString();
                comProduct.ValueMember = dt.Columns["Product_ID"].ToString();
                comProduct.Text = "";
                txtProduct.Text = "";

                query = "select * from size";
                da = new MySqlDataAdapter(query, dbconnection);
                dt = new DataTable();
                da.Fill(dt);
                comSize.DataSource = dt;
                comSize.DisplayMember = dt.Columns["Size_Value"].ToString();
                comSize.ValueMember = dt.Columns["Size_ID"].ToString();
                comSize.Text = "";
                txtSize.Text = "";
                
                load = true;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            dbconnection.Close();
        }
        private void comBox_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (load)
                {
                    ComboBox comBox = (ComboBox)sender;

                    switch (comBox.Name)
                    {
                        case "comType":
                            if (load)
                            {
                                txtType.Text = comType.SelectedValue.ToString();
                                string query = "select * from factory inner join type_factory on factory.Factory_ID=type_factory.Factory_ID inner join type on type_factory.Type_ID=type.Type_ID where type_factory.Type_ID=" + txtType.Text;
                                MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                                DataTable dt = new DataTable();
                                da.Fill(dt);
                                comFactory.DataSource = dt;
                                comFactory.DisplayMember = dt.Columns["Factory_Name"].ToString();
                                comFactory.ValueMember = dt.Columns["Factory_ID"].ToString();
                                comFactory.Text = "";
                                txtFactory.Text = "";
                                dbconnection.Close();

                                dbconnection.Open();
                                query = "select TypeCoding_Method from type where Type_ID=" + txtType.Text;
                                MySqlCommand com = new MySqlCommand(query, dbconnection);
                                int TypeCoding_Method = (int)com.ExecuteScalar();
                                dbconnection.Close();
                                if (TypeCoding_Method == 1)
                                {
                                    string query2 = "";
                                    if (txtType.Text == "2" || txtType.Text == "1")
                                    {
                                        query2 = "select * from groupo where Factory_ID=-1";
                                    }
                                    else
                                    {
                                        query2 = "select * from groupo where Factory_ID=" + -Convert.ToInt32(txtType.Text) + " and Type_ID=" + txtType.Text;
                                    }

                                    MySqlDataAdapter da2 = new MySqlDataAdapter(query2, dbconnection);
                                    DataTable dt2 = new DataTable();
                                    da2.Fill(dt2);
                                    comGroup.DataSource = dt2;
                                    comGroup.DisplayMember = dt2.Columns["Group_Name"].ToString();
                                    comGroup.ValueMember = dt2.Columns["Group_ID"].ToString();
                                    comGroup.Text = "";
                                    txtGroup.Text = "";
                                    groupFlage = true;
                                }
                                factoryFlage = true;
                                
                                comFactory.Focus();
                            }
                            break;
                        case "comFactory":
                            if (factoryFlage)
                            {
                                txtFactory.Text = comFactory.SelectedValue.ToString();
                                dbconnection.Close();
                                dbconnection.Open();
                                string query = "select TypeCoding_Method from type where Type_ID=" + txtType.Text;
                                MySqlCommand com = new MySqlCommand(query, dbconnection);
                                int TypeCoding_Method = (int)com.ExecuteScalar();
                                dbconnection.Close();
                                if (TypeCoding_Method == 2)
                                {
                                    string query2f = "select * from groupo where Type_ID=" + txtType.Text + " and Factory_ID=" + txtFactory.Text;
                                    MySqlDataAdapter da2f = new MySqlDataAdapter(query2f, dbconnection);
                                    DataTable dt2f = new DataTable();
                                    da2f.Fill(dt2f);
                                    comGroup.DataSource = dt2f;
                                    comGroup.DisplayMember = dt2f.Columns["Group_Name"].ToString();
                                    comGroup.ValueMember = dt2f.Columns["Group_ID"].ToString();
                                    comGroup.Text = "";
                                    txtGroup.Text = "";
                                }
                                else
                                {
                                    filterProduct();
                                }

                                groupFlage = true;

                                string query2 = "select * from size where Factory_ID=" + txtFactory.Text;
                                MySqlDataAdapter da2 = new MySqlDataAdapter(query2, dbconnection);
                                DataTable dt2 = new DataTable();
                                da2.Fill(dt2);
                                comSize.DataSource = dt2;
                                comSize.DisplayMember = dt2.Columns["Size_Value"].ToString();
                                comSize.ValueMember = dt2.Columns["Size_ID"].ToString();
                                comSize.Text = "";
                                txtSize.Text = "";
                                comGroup.Focus();

                      
                            }
                            break;
                        case "comGroup":
                            if (groupFlage)
                            {
                                txtGroup.Text = comGroup.SelectedValue.ToString();
                                string supQuery = "", subQuery1 = "";
                                if (txtType.Text != "")
                                {
                                    supQuery += " and product.Type_ID=" + txtType.Text;
                                }
                                if (txtFactory.Text != "")
                                {
                                    supQuery += " and product_factory_group.Factory_ID=" + txtFactory.Text;
                                    subQuery1 += " and Factory_ID=" + txtFactory.Text;
                                }
                                string query3 = "select distinct  product.Product_ID  ,Product_Name  from product inner join product_factory_group on product.Product_ID=product_factory_group.Product_ID  where product_factory_group.Group_ID=" + txtGroup.Text + supQuery + "  order by product.Product_ID";
                                MySqlDataAdapter da3 = new MySqlDataAdapter(query3, dbconnection);
                                DataTable dt3 = new DataTable();
                                da3.Fill(dt3);
                                comProduct.DataSource = dt3;
                                comProduct.DisplayMember = dt3.Columns["Product_Name"].ToString();
                                comProduct.ValueMember = dt3.Columns["Product_ID"].ToString();
                                comProduct.Text = "";
                                txtProduct.Text = "";

                                string query2 = "select * from size where Group_ID=" + txtGroup.Text + subQuery1;
                                MySqlDataAdapter da2 = new MySqlDataAdapter(query2, dbconnection);
                                DataTable dt2 = new DataTable();
                                da2.Fill(dt2);
                                comSize.DataSource = dt2;
                                comSize.DisplayMember = dt2.Columns["Size_Value"].ToString();
                                comSize.ValueMember = dt2.Columns["Size_ID"].ToString();
                                comSize.Text = "";
                                txtSize.Text = "";

                                comProduct.Focus();
                                flagProduct = true;
                            }
                            break;

                        case "comProduct":

                            txtProduct.Text = comProduct.SelectedValue.ToString();
                            comSize.Focus();

                            break;

                        case "comSize":
                            txtSize.Text = comSize.SelectedValue.ToString();
                            break;

                        case "comBranch":
                            txtBranchID.Text = comBranch.SelectedValue.ToString();
                            comType.Focus();
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }
        private void txtBox_KeyDown(object sender, KeyEventArgs e)
        {
            TextBox txtBox = (TextBox)sender;
            string query;
            MySqlCommand com;
            string Name;

            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    if (txtBox.Text != "")
                    {
                        dbconnection.Open();
                        switch (txtBox.Name)
                        {
                            case "txtType":
                                query = "select Type_Name from type where Type_ID='" + txtType.Text + "'";
                                com = new MySqlCommand(query, dbconnection);
                                if (com.ExecuteScalar() != null)
                                {
                                    Name = (string)com.ExecuteScalar();
                                    comType.Text = Name;
                                    txtFactory.Focus();
                                    dbconnection.Close();

                                }
                                else
                                {
                                    MessageBox.Show("there is no item with this id");
                                    dbconnection.Close();
                                    return;
                                }
                                break;
                            case "txtFactory":
                                query = "select Factory_Name from factory where Factory_ID='" + txtFactory.Text + "'";
                                com = new MySqlCommand(query, dbconnection);
                                if (com.ExecuteScalar() != null)
                                {
                                    Name = (string)com.ExecuteScalar();
                                    comFactory.Text = Name;
                                    txtGroup.Focus();
                                    dbconnection.Close();
                                }
                                else
                                {
                                    MessageBox.Show("there is no item with this id");
                                    dbconnection.Close();
                                    return;
                                }
                                break;
                            case "txtGroup":
                                query = "select Group_Name from groupo where Group_ID='" + txtGroup.Text + "'";
                                com = new MySqlCommand(query, dbconnection);
                                if (com.ExecuteScalar() != null)
                                {
                                    Name = (string)com.ExecuteScalar();
                                    comGroup.Text = Name;
                                    txtProduct.Focus();
                                    dbconnection.Close();
                                }
                                else
                                {
                                    MessageBox.Show("there is no item with this id");
                                    dbconnection.Close();
                                    return;
                                }
                                break;
                            case "txtProduct":
                                query = "select Product_Name from product where Product_ID='" + txtProduct.Text + "'";
                                com = new MySqlCommand(query, dbconnection);
                                if (com.ExecuteScalar() != null)
                                {
                                    Name = (string)com.ExecuteScalar();
                                    comProduct.Text = Name;
                                    txtType.Focus();
                                    dbconnection.Close();
                                }
                                else
                                {
                                    MessageBox.Show("there is no item with this id");
                                    dbconnection.Close();
                                    return;
                                }
                                break;

                            case "txtSize":
                                query = "select Size_Value from size where Size_ID='" + txtSize.Text + "'";
                                com = new MySqlCommand(query, dbconnection);
                                if (com.ExecuteScalar() != null)
                                {
                                    Name = (string)com.ExecuteScalar();
                                    comSize.Text = Name;
                                    txtType.Focus();
                                    dbconnection.Close();
                                }
                                else
                                {
                                    MessageBox.Show("there is no item with this id");
                                    dbconnection.Close();
                                    return;
                                }
                                break;
                     
                        }

                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                dbconnection.Close();
            }
        }
        private void btnNewChooes_Click(object sender, EventArgs e)
        {
            try
            {
                comProduct.Text = "";
                comType.Text = "";
                comFactory.Text = "";
                comGroup.Text = "";
                comSize.Text = "";

                txtType.Text = "";
                txtFactory.Text = "";
                txtGroup.Text = "";
                txtProduct.Text = "";
                txtSize.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtBranchID.Text != "")
                {
                    dbconnection.Open();
                    DateTime date = dateTimeFrom.Value;
                    string d = date.ToString("yyyy-MM-dd ");
                    d += "00:00:00";
                    DateTime date2 = dateTimeTo.Value;
                    string d2 = date2.ToString("yyyy-MM-dd ");
                    d2 += "23:59:59";
                    
                    string query = "select CustomerBill_ID from customer_bill  where Bill_Date between '" + d + "' and '" + d2 + "' and customer_bill.Branch_ID=" + txtBranchID.Text;

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
              
                    DataTable _Table = peraperDataTable();

                    _Table = getTotalSales(_Table, str);
                    _Table = getTotalReturn(_Table, str1);

                    gridControl1.DataSource = _Table;

                    gridView1.BestFitColumns();
                    CalTotal(_Table);
                }
                else
                {
                    MessageBox.Show("اختار الفرع ");
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }

        //functions
        public void filterProduct()
        {
            if (comType.Text != "")
            {
                if (comGroup.Text != "" || comFactory.Text != "" || comType.Text != "")
                {
                    string supQuery = "";

                    supQuery = " product.Type_ID=" + comType.SelectedValue + "";
                    if (comFactory.Text != "")
                    {
                        supQuery += " and product_factory_group.Factory_ID=" + comFactory.SelectedValue + "";
                    }
                    if (comGroup.Text != "")
                    {
                        supQuery += " and product_factory_group.Group_ID=" + comGroup.SelectedValue + "";
                    }
                    string query = "select distinct  product.Product_ID  ,Product_Name  from product inner join product_factory_group on product.Product_ID=product_factory_group.Product_ID  where  " + supQuery + "   order by product.Product_ID";
                    MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    comProduct.DataSource = dt;
                    comProduct.DisplayMember = dt.Columns["Product_Name"].ToString();
                    comProduct.ValueMember = dt.Columns["Product_ID"].ToString();
                    comProduct.Text = "";
                    txtProduct.Text = "";
                }
            }

        }
        public DataTable getTotalSales(DataTable _Table, string customerBill_ids)
        {
            string q1, q2, q3, q4, fQuery = "";
            if (txtType.Text == "")
            {
                q1 = "select Type_ID from data";
            }
            else
            {
                q1 = txtType.Text;
            }
            if (txtFactory.Text == "")
            {
                q2 = "select Factory_ID from data";
            }
            else
            {
                q2 = txtFactory.Text;
            }
            if (txtProduct.Text == "")
            {
                q3 = "select Product_ID from data";
            }
            else
            {
                q3 = txtProduct.Text;
            }
            if (txtGroup.Text == "")
            {
                q4 = "select Group_ID from data";
            }
            else
            {
                q4 = txtGroup.Text;
            }

            if (comSize.Text != "")
            {
                fQuery += "and size.Size_ID='" + comSize.SelectedValue + "'";
            }

       
            string subString = " INNER JOIN type ON type.Type_ID = data.Type_ID INNER JOIN product ON product.Product_ID = data.Product_ID INNER JOIN factory ON data.Factory_ID = factory.Factory_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID LEFT outer JOIN color ON data.Color_ID = color.Color_ID LEFT outer  JOIN size ON data.Size_ID = size.Size_ID LEFT outer  JOIN sort ON data.Sort_ID = sort.Sort_ID";
            string itemName = "concat( product.Product_Name,' ',type.Type_Name,' ',factory.Factory_Name,' ',groupo.Group_Name,' ' ,COALESCE(color.Color_Name,''),' ',COALESCE(size.Size_Value,''),' ',COALESCE(sort.Sort_Value,''),' ',COALESCE(data.Classification,''),' ',COALESCE(data.Description,''))as 'البند'";

            string query = "select data.Data_ID,data.Code as 'الكود',"+itemName+",FORMAT(sum(product_bill.PriceAD*Quantity),2) from product_bill  inner join data on data.Data_ID=product_bill.Data_ID "+subString+" where CustomerBill_ID in (" + customerBill_ids + ") and data.Type_ID IN(" + q1 + ") and  data.Factory_ID  IN(" + q2 + ") and  data.Product_ID  IN(" + q3 + ") and data.Group_ID IN (" + q4 + ")  " + fQuery + " group by  data.Data_ID";
            
            MySqlCommand com = new MySqlCommand(query, dbconnection);
            MySqlDataReader dr = com.ExecuteReader();

            while (dr.Read())
            {
                DataRow row = _Table.NewRow();
                if (dr[0].ToString() != "")
                    row["Data_ID"] = dr[0].ToString();
                if (dr[1].ToString() != "")
                    row["Code"] = dr[1].ToString();
                if (dr[2].ToString() != "")
                    row["Item_Name"] = dr[2].ToString();
                if (dr[3].ToString() != "")
                    row["TotalSales"] = dr[3].ToString();
                else
                    row["TotalSales"] = 0;
                if (dr[3].ToString() != "")
                    row["Safaya"] = dr[3].ToString();
                else
                    row["Safaya"] = 0;
                
                _Table.Rows.Add(row);
            }
            dr.Close();

            return _Table;
        }
        public DataTable getTotalReturn(DataTable _Table, string CustomerReturnBill_IDs)
        {
            string q1, q2, q3, q4, fQuery = "";
            if (txtType.Text == "")
            {
                q1 = "select Type_ID from data";
            }
            else
            {
                q1 = txtType.Text;
            }
            if (txtFactory.Text == "")
            {
                q2 = "select Factory_ID from data";
            }
            else
            {
                q2 = txtFactory.Text;
            }
            if (txtProduct.Text == "")
            {
                q3 = "select Product_ID from data";
            }
            else
            {
                q3 = txtProduct.Text;
            }
            if (txtGroup.Text == "")
            {
                q4 = "select Group_ID from data";
            }
            else
            {
                q4 = txtGroup.Text;
            }

            if (comSize.Text != "")
            {
                fQuery += "and size.Size_ID='" + comSize.SelectedValue + "'";
            }

          

            string subString = " INNER JOIN type ON type.Type_ID = data.Type_ID INNER JOIN product ON product.Product_ID = data.Product_ID INNER JOIN factory ON data.Factory_ID = factory.Factory_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID LEFT outer JOIN color ON data.Color_ID = color.Color_ID LEFT outer  JOIN size ON data.Size_ID = size.Size_ID LEFT outer  JOIN sort ON data.Sort_ID = sort.Sort_ID";
            string itemName = "concat( product.Product_Name,' ',type.Type_Name,' ',factory.Factory_Name,' ',groupo.Group_Name,' ' ,COALESCE(color.Color_Name,''),' ',COALESCE(size.Size_Value,''),' ',COALESCE(sort.Sort_Value,''),' ',COALESCE(data.Classification,''),' ',COALESCE(data.Description,''))as 'البند'";

            string query = "select data.Data_ID,data.Code as 'الكود',"+itemName+",FORMAT(sum(TotalAD),2) from customer_return_bill_details inner join data on data.Data_ID=customer_return_bill_details.Data_ID "+subString+" where CustomerReturnBill_ID in (" + CustomerReturnBill_IDs + ") and data.Type_ID IN(" + q1 + ") and  data.Factory_ID  IN(" + q2 + ") and  data.Product_ID  IN(" + q3 + ") and data.Group_ID IN (" + q4 + ")  " + fQuery + "  group by  factory.Factory_ID  ";
          
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
                        if (dr[3].ToString() != "")
                        {
                            item["TotalReturn"] = dr[3].ToString();
                            item["Safaya"] = (Convert.ToDouble(item["TotalSales"].ToString()) - Convert.ToDouble(dr[3].ToString())).ToString();
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
                    if (dr[3].ToString() != "")
                    {
                        row["TotalReturn"] = dr[3].ToString();
                        if (dr[2].ToString() != "")
                            row["Safaya"] = -Convert.ToDouble(dr[3].ToString());
                        
                            row["Data_ID"] = dr[0].ToString();
                            row["Code"] = dr[1].ToString();
                            row["Item_Name"] = dr[2].ToString();
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
            _Table.Columns.Add(new DataColumn("Data_ID", typeof(string)));
            _Table.Columns.Add(new DataColumn("Code", typeof(string)));
            _Table.Columns.Add(new DataColumn("Item_Name", typeof(string)));

            _Table.Columns.Add(new DataColumn("TotalSales", typeof(string)));
            _Table.Columns.Add(new DataColumn("TotalReturn", typeof(string)));
            _Table.Columns.Add(new DataColumn("Safaya", typeof(decimal)));
            return _Table;
        }
        private void btnReport_Click(object sender, EventArgs e)
        {
            try
            {
                dataX d = new dataX(dateTimeFrom.Text, dateTimeTo.Text, "", comFactory.Text);
                MainForm.displayCompanyReport(gridControl1, d, "تقرير  تفاصيل مبيعات الشركات");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void CalTotal(DataTable _Tabl)
        {
            double totalSales = 0, totalReturn = 0, totalSafay = 0, totalProfit = 0;
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

    }
}

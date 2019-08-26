using System;
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
        MainForm MainForm;
        public DelegateSalesForCompany(MainForm MainForm)
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
                string d = date.ToString("yyyy-MM-dd ");
                d += "00:00:00";
                DateTime date2 = dateTimeTo.Value;
                string d2 = date2.ToString("yyyy-MM-dd ");
                d2 += "23:59:59";
                string query = "select CustomerBill_ID from customer_bill inner join transitions on customer_bill.Branch_BillNumber=transitions.Bill_Number where Paid_Status=1 and Type_Buy='كاش' and Bill_Date between '" + d + "' and '" + d2 + "' and customer_bill.Branch_ID=" + txtBranchID.Text;
                MySqlCommand com = new MySqlCommand(query, dbconnection);
                MySqlDataReader dr = com.ExecuteReader();
                string str = "";
                while (dr.Read())
                {
                    str += dr[0].ToString() + ",";
                }
                dr.Close();

                //query = "select CustomerBill_ID from customer_bill  where Paid_Status=1 and Type_Buy='آجل' and AgelBill_PaidDate between '" + d + "' and '" + d2 + "' and customer_bill.Branch_ID=" + txtBranchID.Text;
                //com = new MySqlCommand(query, dbconnection);
                //dr = com.ExecuteReader();

                //while (dr.Read())
                //{
                //    str += dr[0].ToString() + ",";
                //}
                //dr.Close();

                str += 0;

                //query = "select CustomerReturnBill_ID from customer_return_bill where  Date between '" + d + "' and '" + d2 + "' and customer_return_bill.Branch_ID=" + txtBranchID.Text;
                query = "select Bill_Number from transitions where  Date between '" + d + "' and '" + d2 + "' and Type='كاش' and Transition='سحب' and transitions.TransitionBranch_ID=" + txtBranchID.Text;

                com = new MySqlCommand(query, dbconnection);
                dr = com.ExecuteReader();
                string str1 = "";
                while (dr.Read())
                {
                    str1 += dr[0].ToString() + ",";
                }
                dr.Close();
                str1 += 0;
                //string query = "select distinct customer_bill.CustomerBill_ID from customer_bill inner join product_bill on customer_bill.CustomerBill_ID=product_bill.CustomerBill_ID inner join delegate on delegate.Delegate_ID=product_bill.Delegate_ID inner join transitions on customer_bill.Branch_BillNumber=transitions.Bill_Number where Paid_Status=1 and Type_Buy='كاش' and Date between '" + d + "' and '" + d2 + "' and customer_bill.Branch_ID=" + txtBranchID.Text ;
                //MySqlCommand com = new MySqlCommand(query, dbconnection);
                //MySqlDataReader dr = com.ExecuteReader();
                //string str = "";
                //while (dr.Read())
                //{
                //    str += dr[0].ToString() + ",";
                //}
                //dr.Close();

                //query = "select distinct customer_bill.CustomerBill_ID from customer_bill inner join product_bill on customer_bill.CustomerBill_ID=product_bill.CustomerBill_ID inner join delegate on delegate.Delegate_ID=product_bill.Delegate_ID  where Paid_Status=1 and Type_Buy='آجل' and AgelBill_PaidDate between '" + d + "' and '" + d2 + "' and customer_bill.Branch_ID=" + txtBranchID.Text + " and delegate.Delegate_ID= " + txtDelegateID.Text;
                //com = new MySqlCommand(query, dbconnection);
                //dr = com.ExecuteReader();

                //while (dr.Read())
                //{
                //    str += dr[0].ToString() + ",";
                //}
                //dr.Close();

                //str += 0;

                //query = "select distinct customer_return_bill.CustomerReturnBill_ID from customer_return_bill inner join customer_return_bill_details on customer_return_bill_details.CustomerReturnBill_ID=customer_return_bill.CustomerReturnBill_ID inner join delegate on delegate.Delegate_ID=customer_return_bill_details.Delegate_ID where  Date between '" + d + "' and '" + d2 + "' and customer_return_bill.Branch_ID=" + txtBranchID.Text +" and delegate.Delegate_ID= " + txtDelegateID.Text  ;
                //com = new MySqlCommand(query, dbconnection);
                //dr = com.ExecuteReader();
                //string str1 = "";
                //while (dr.Read())
                //{
                //    str1 += dr[0].ToString() + ",";
                //}
                //dr.Close();
               // str1 += 0;
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


                CalTotal(_Table);
            }
            catch (Exception ex)
            {
                MessageBox.Show("اختار الفرع");
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
        private void comBranch_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (loaded)
                {
                    txtBranchID.Text = comBranch.SelectedValue.ToString();
                    dbconnection.Open();
                    loaded = false;
                    string query = "select * from delegate where Branch_ID="+ txtBranchID.Text;
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
        
        //functions
        public DataTable getTotalSales(DataTable _Table,string customerBill_ids,string subQuery)
        {
            string query = "";
            if (txtFactory.Text != "")
            {
                query = "select delegate.Delegate_ID,Delegate_Name,Factory_Name,sum(product_bill.PriceAD*Quantity) from product_bill inner join delegate on delegate.Delegate_ID=product_bill.Delegate_ID inner join data on data.Data_ID=product_bill.Data_ID inner join factory on data.Factory_ID=factory.Factory_ID where CustomerBill_ID in (" + customerBill_ids + ") " + subQuery + " and data.Factory_ID=" + txtFactory.Text + " group by delegate.Delegate_ID ";
            }
            else if (txtDelegateID.Text != "")
            {
                query = "select delegate.Delegate_ID,Delegate_Name,Factory_Name,sum(product_bill.PriceAD*Quantity) from product_bill inner join delegate on delegate.Delegate_ID=product_bill.Delegate_ID inner join data on data.Data_ID=product_bill.Data_ID inner join factory on data.Factory_ID=factory.Factory_ID where CustomerBill_ID in (" + customerBill_ids + ") " + subQuery + " and delegate.Delegate_ID=" + txtDelegateID.Text + " group by Factory_Name ";
            }
            else
            {
                query = "select delegate.Delegate_ID,Delegate_Name,Factory_Name,product_bill.PriceAD*Quantity from product_bill left join delegate on delegate.Delegate_ID=product_bill.Delegate_ID inner join data on data.Data_ID=product_bill.Data_ID left join factory on data.Factory_ID=factory.Factory_ID where CustomerBill_ID in (" + customerBill_ids + ")  ";
            }

            MySqlCommand com = new MySqlCommand(query, dbconnection);
            MySqlDataReader dr = com.ExecuteReader();

            while (dr.Read())
            {
                DataRow row = _Table.NewRow();

                if(dr[0].ToString()!="")
                    row["Delegate_ID"] =dr[0].ToString();
                if (dr[1].ToString() != "")
                    row["Delegate_Name"] = dr[1].ToString();
                if (dr[2].ToString() != "")
                    row["Factory_Name"] = dr[2].ToString();
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
        public DataTable getTotalReturn(DataTable _Table, string CustomerReturnBill_IDs, string subQuery)
        {
            string query = "";
            if (txtFactory.Text != "")
            {
                query = "select delegate.Delegate_ID,Delegate_Name,Factory_Name,sum(TotalAD) from customer_return_bill_details inner join delegate on delegate.Delegate_ID=customer_return_bill_details.Delegate_ID inner join data on data.Data_ID=customer_return_bill_details.Data_ID inner join factory on data.Factory_ID=factory.Factory_ID where CustomerReturnBill_ID in (" + CustomerReturnBill_IDs + ")  " + subQuery + " group by Factory_Name ";
            }
            else if (txtDelegateID.Text != "")
            {
                //query = "select delegate.Delegate_ID,Delegate_Name,Factory_Name,sum(TotalAD) from customer_return_bill inner join customer_return_bill_details on customer_return_bill.CustomerReturnBill_ID=customer_return_bill_details.CustomerReturnBill_ID inner join delegate on delegate.Delegate_ID=customer_return_bill_details.Delegate_ID inner join data on data.Data_ID=customer_return_bill_details.Data_ID inner join factory on data.Factory_ID=factory.Factory_ID where customer_return_bill.CustomerReturnBill_ID in (" + CustomerReturnBill_IDs + ")  " + subQuery + " group by factory.Factory_ID";
                query = "select delegate.Delegate_ID,Delegate_Name,Factory_Name,sum(TotalAD) from customer_return_bill inner join customer_return_bill_details on customer_return_bill.CustomerReturnBill_ID=customer_return_bill_details.CustomerReturnBill_ID inner join delegate on delegate.Delegate_ID=customer_return_bill_details.Delegate_ID inner join data on data.Data_ID=customer_return_bill_details.Data_ID inner join factory on data.Factory_ID=factory.Factory_ID where customer_return_bill.Branch_BillNumber in (" + CustomerReturnBill_IDs + ")  " + subQuery + " group by factory.Factory_ID";

            }
            else
            {
                query = "select delegate.Delegate_ID,Delegate_Name,Factory_Name,sum(TotalAD) from customer_return_bill_details inner join delegate on delegate.Delegate_ID=customer_return_bill_details.Delegate_ID inner join data on data.Data_ID=customer_return_bill_details.Data_ID inner join factory on data.Factory_ID=factory.Factory_ID where CustomerReturnBill_ID in (" + CustomerReturnBill_IDs + ")  ";
            }

            MySqlCommand com = new MySqlCommand(query, dbconnection);
            MySqlDataReader dr = com.ExecuteReader();
            DataTable temp = peraperDataTable();
         
            while (dr.Read())
            {
                bool flag = true;
                foreach (DataRow item in _Table.Rows)
                {
                    if (txtFactory.Text == "")
                    {
                        if (item[2].ToString() == dr[2].ToString())
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
                    else if (txtDelegateID.Text == "")
                    {
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

                }
                if (flag)
                {
                    DataRow row = temp.NewRow();
                    if (dr[3].ToString() != "")
                    {
                        row["TotalReturn"] = dr[3].ToString();
                        if(dr[3].ToString()!="")
                            row["Safaya"] = -Convert.ToDouble(dr[3].ToString());
                    }
                    else
                    {
                        row["TotalReturn"] = 0;
                        row["Safaya"] = 0;
                    }
                    row["Delegate_ID"] = dr[0].ToString();
                    row["Delegate_Name"] = dr[1].ToString();
                    row["Factory_Name"] = dr[2].ToString();
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
        private void btnReport_Click(object sender, EventArgs e)
        {
            try
            {
                dataX d = new dataX(dateTimeFrom.Text, dateTimeTo.Text, comDelegate.Text, comFactory.Text);
                MainForm.displayDelegateReport(GridControl1,d);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void CalTotal(DataTable _Tabl)
        {
            double totalSales = 0, totalReturn = 0, totalSafay = 0;
            foreach (DataRow item in _Tabl.Rows)
            {
                if(item["TotalSales"].ToString()!="")
                    totalSales += Convert.ToDouble(item["TotalSales"].ToString());
                if (item["TotalReturn"].ToString() != "")
                    totalReturn += Convert.ToDouble(item["TotalReturn"].ToString());
                if (item["Safaya"].ToString() != "")
                    totalSafay += Convert.ToDouble(item["Safaya"].ToString());
            }
            txtTotalSales.Text = totalSales.ToString();
            txtTotalReturn.Text = totalReturn.ToString();
            txtTotalSafay.Text = totalSafay.ToString();
        }
    }
    public class dataX
    {
        public dataX(string dateFrom,string dateTo,string delegateName,string company)
        {
            this.dateFrom = dateFrom;
            this.dateTo = dateTo;
            if (delegateName == "")
                this.delegateName = delegateName;
            else
                this.delegateName = "مندوب :"+delegateName  ;
            if (company == "")
                this.company = company;
            else
                this.company =" شركة : "+ company;

            delegateProfit = "";
            company_profit_list = null;
        }
      
        public string dateFrom { get; set; }
        public string dateTo { get; set; }
        public string delegateName { get; set; }
        public string company { get; set; }
        public string delegateProfit { get; set; }
        public List<company_profit> company_profit_list { get; set; }
    }
    public struct company_profit
    {
       public string companyName;
       public string delegateProfit;
    }
}
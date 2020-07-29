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

namespace TaxesSystem
{
    public partial class DelegateTotalSalesAgel : DevExpress.XtraEditors.XtraForm
    {
        private MySqlConnection dbconnection;
        bool loaded = false;
        MainForm MainForm;
        public DelegateTotalSalesAgel(MainForm MainForm)
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

        private void DelegateTotalSales_Load(object sender, EventArgs e)
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
                DateTime date = dateTimeFrom.Value;
                string d = date.ToString("yyyy-MM-dd ");
                d += "00:00:00";
                DateTime date2 = dateTimeTo.Value;
                string d2 = date2.ToString("yyyy-MM-dd ");
                d2 += "23:59:59";
                string str = "";
                //string query= "select CustomerBill_ID from customer_bill inner join transitions on customer_bill.Branch_BillNumber=transitions.Bill_Number where Paid_Status=1 and Type_Buy='كاش' and Bill_Date between '" + d + "' and '" + d2 + "' and customer_bill.Branch_ID="+txtBranchID.Text;
                //MySqlCommand com = new MySqlCommand(query, dbconnection);
                //MySqlDataReader dr = com.ExecuteReader();
                //string str = "";
                //while (dr.Read())
                //{
                //    str += dr[0].ToString() + ",";
                //}
                //dr.Close();

                string query = "select CustomerBill_ID from customer_bill  where Type_Buy='آجل' and Bill_Date between '" + d + "' and '" + d2 + "' and customer_bill.Branch_ID=" + txtBranchID.Text;
                MySqlCommand com = new MySqlCommand(query, dbconnection);
                MySqlDataReader dr = com.ExecuteReader();
           
                while (dr.Read())
                {
                   str += dr[0].ToString() + ",";
                }
                dr.Close();

                str += 0;

                query = "select Branch_BillNumber from customer_return_bill where  Type_Buy='آجل' and Date between '" + d + "' and '" + d2 + "' and customer_return_bill.Branch_ID=" + txtBranchID.Text;
                //query = "select Bill_Number from transitions where  Date between '" + d + "' and '" + d2 + "' and Type='آجل' and Transition='سحب' and transitions.TransitionBranch_ID=" + txtBranchID.Text+ " and Bill_Number!=null";

                com = new MySqlCommand(query, dbconnection);
                dr = com.ExecuteReader();
                string str1 = "";
                while (dr.Read())
                {
                    str1 += dr[0].ToString() + ",";
                }
                dr.Close();
                str1 += 0;


                if (txtDelegateID.Text != "")
                {
                    query = " and delegate.Delegate_ID= " + txtDelegateID.Text;
                    gridView1.Columns[1].Visible = false;
                }
                else
                {
                    query = "";
                    gridView1.Columns[1].Visible = true;
                }
                DataTable _Table = peraperDataTable();
                _Table = getTotalSales(_Table, str, query);
                _Table = getTotalReturn(_Table, str1, query);

                gridControl1.DataSource = _Table;

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
                txtDelegateID.Text = "";
                comDelegate.Text = "";

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //functions
        public DataTable getTotalSales(DataTable _Table, string customerBill_ids, string subQuery)
        {
            string query = "select delegate.Delegate_ID,Delegate_Name,sum(product_bill.PriceAD*Quantity) from product_bill inner join customer_bill on customer_bill.CustomerBill_ID=product_bill.CustomerBill_ID inner join delegate on delegate.Delegate_ID=product_bill.Delegate_ID  where product_bill.CustomerBill_ID in (" + customerBill_ids + ")  " + subQuery + " group by delegate.Delegate_ID ";
            MySqlCommand com = new MySqlCommand(query, dbconnection);
            MySqlDataReader dr = com.ExecuteReader();

            while (dr.Read())
            {
                DataRow row = _Table.NewRow();

                row["Delegate_ID"] = dr[0].ToString();
                row["Delegate_Name"] = dr[1].ToString();
                row["TotalSales"] = dr[2].ToString();
                row["Safaya"] = dr[2].ToString();
                _Table.Rows.Add(row);
            }
            dr.Close();

            return _Table;
        }
        public DataTable getTotalReturn(DataTable _Table, string CustomerReturnBill_IDs, string subQuery)
        {
            string query = "select delegate.Delegate_ID,Delegate_Name,sum(TotalAD) from customer_return_bill_details inner join customer_return_bill on customer_return_bill_details.CustomerReturnBill_ID=customer_return_bill.CustomerReturnBill_ID inner join delegate on delegate.Delegate_ID=customer_return_bill_details.Delegate_ID  where customer_return_bill.Branch_BillNumber in (" + CustomerReturnBill_IDs + ") and  customer_return_bill.Branch_ID=" + txtBranchID.Text + " " + subQuery + " group by delegate.Delegate_ID ";
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
                        item["TotalReturn"] = dr[2].ToString();
                        item["Safaya"] = (Convert.ToDouble(item[2].ToString()) - Convert.ToDouble(dr[2].ToString())).ToString();
                        flag = false;
                    }

                }
                if (flag)
                {
                    DataRow row = temp.NewRow();

                    row["TotalReturn"] = dr[2].ToString();
                    if(dr[2].ToString()!="")
                    row["Safaya"] = -Convert.ToDouble(dr[2].ToString());
                    row["Delegate_ID"] = dr[0].ToString();
                    row["Delegate_Name"] = dr[1].ToString();

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
            _Table.Columns.Add(new DataColumn("TotalSales", typeof(decimal)));
            _Table.Columns.Add(new DataColumn("TotalReturn", typeof(decimal)));
            _Table.Columns.Add(new DataColumn("Safaya", typeof(decimal)));
            return _Table;
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
        private void btnReport_Click(object sender, EventArgs e)
        {
            try
            {
                dataX d = new dataX(dateTimeFrom.Text, dateTimeTo.Text, comDelegate.Text, "");
                MainForm.displayDelegateReport2(gridControl1,"", d);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

      
    }
}
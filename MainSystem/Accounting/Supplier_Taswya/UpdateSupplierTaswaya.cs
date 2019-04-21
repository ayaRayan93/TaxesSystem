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
    public partial class UpdateSupplierTaswaya : Form
    {
        MySqlConnection dbconnection;
        MySqlConnection dbconnection1;
        MainForm saleMainForm;
        SupplierTaswayaReport CustomerTaswayaReport;
        DataRowView row;
        private string Customer_Type;
        private bool loaded = false;
        int id = -1;
        string customerID="", clientID="";
        bool flag = false;
        public UpdateSupplierTaswaya(DataRowView row, MainForm saleMainForm, SupplierTaswayaReport CustomerTaswayaReport)
        {
            try
            {
                InitializeComponent();
                dbconnection = new MySqlConnection(connection.connectionString);
                dbconnection1 = new MySqlConnection(connection.connectionString);
                dbconnection.Open();
                this.row = row;
                this.saleMainForm = saleMainForm;
                this.CustomerTaswayaReport = CustomerTaswayaReport;
                string query = "select * from supplier";
                MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                DataTable dt = new DataTable();
                da.Fill(dt);
                comSupplier.DataSource = dt;
                comSupplier.DisplayMember = dt.Columns["Supplier_Name"].ToString();
                comSupplier.ValueMember = dt.Columns["Supplier_ID"].ToString();
                comSupplier.Text = "";
                loaded = true;
                setData();
            }
            catch (Exception ex)
            {
               MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }
        private void comClient_SelectedValueChanged(object sender, EventArgs e)
        {
            if (loaded)
            {
                try
                {
                    txtSupplierID.Text = comSupplier.SelectedValue.ToString();
                }
                catch (Exception ex)
                {
                  //  MessageBox.Show(ex.Message);
                }
            }
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
                            case "txtClientID":
                                query = "select Customer_Name from customer where Customer_ID=" + txtSupplierID.Text + "";
                                com = new MySqlCommand(query, dbconnection);
                                if (com.ExecuteScalar() != null)
                                {
                                    Name = (string)com.ExecuteScalar();
                                    comSupplier.Text = Name;
                                    comSupplier.SelectedValue = txtSupplierID.Text;
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
        private void btnTaswaya_Click(object sender, EventArgs e)
        {
            try
            {
                if (id != -1)
                {
                    dbconnection.Open();
                    string query = "update  customer_taswaya set Client_ID=@Client_ID,Taswaya_Type=@Taswaya_Type,Money_Paid=@Money_Paid,Info=@Info,Date=@Date where CustomerTaswaya_ID=" + id;
                    MySqlCommand com = new MySqlCommand(query, dbconnection);
                    com.Parameters.Add("@Client_ID", MySqlDbType.Int16);
                    com.Parameters["@Client_ID"].Value = Convert.ToInt16(txtSupplierID.Text);
                    if (radioButtonDiscount.Checked)
                    {
                        com.Parameters.Add("@Taswaya_Type", MySqlDbType.VarChar);
                        com.Parameters["@Taswaya_Type"].Value = radioButtonDiscount.Text;
                    }
                    else
                    {
                        com.Parameters.Add("@Taswaya_Type", MySqlDbType.VarChar);
                        com.Parameters["@Taswaya_Type"].Value = radioButtonAdd.Text;
                    }
                    com.Parameters.Add("@Money_Paid", MySqlDbType.Decimal);
                    com.Parameters["@Money_Paid"].Value = Convert.ToDouble(txtMoney.Text);
                    com.Parameters.Add("@Info", MySqlDbType.VarChar);
                    com.Parameters["@Info"].Value = txtInfo.Text;
                    com.Parameters.Add("@Date", MySqlDbType.Date);
                    com.Parameters["@Date"].Value = dateTimeFrom.Value.Date;

                    com.ExecuteNonQuery();
                    CustomerTaswayaReport.DisplaySupplierTaswaya();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }


        //function
        public void checkCustomerType()
        {
            string query = "select * from supplier";
            MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
            DataTable dt = new DataTable();
            da.Fill(dt);
            comSupplier.DataSource = dt;
            comSupplier.DisplayMember = dt.Columns["Customer_Name"].ToString();
            comSupplier.ValueMember = dt.Columns["Customer_ID"].ToString();
            comSupplier.Text = "";
            txtSupplierID.Text = "";
        }
        public void setIDs()
        {
            Customer_Type = "عميل";
            
            string query = "select Customer_Name from customer where Customer_ID=" + clientID + "";
            MySqlCommand com = new MySqlCommand(query, dbconnection);
            if (com.ExecuteScalar() != null)
            {
                Name = (string)com.ExecuteScalar();
                comSupplier.Text = Name;
                txtSupplierID.Text = clientID;
            }
        }
        public void setData()
        {
            id = Convert.ToInt16(row[0].ToString());
            string qeury = "select CustomerTaswaya_ID as 'الكود',customer_taswaya.Customer_ID,customer_taswaya.Client_ID,c2.Customer_Name as 'المهندس/مقاول/تاجر',c1.Customer_Name as 'العميل',Taswaya_Type ,Money_Paid ,Info ,Date  from customer_taswaya inner join customer as c1 on c1.Customer_ID=customer_taswaya.Client_ID inner join customer as c2 on c2.Customer_ID=customer_taswaya.Customer_ID where CustomerTaswaya_ID=" + id;
            MySqlCommand com = new MySqlCommand(qeury, dbconnection);
            MySqlDataReader dr = com.ExecuteReader();
            while (dr.Read())
            {
                if (dr["Customer_ID"] != null)
                {
                    customerID = dr["Customer_ID"].ToString();
                }
                if (dr["Client_ID"] != null)
                {
                    clientID = dr["Client_ID"].ToString();
                }
              
                txtMoney.Text= dr["Money_Paid"].ToString();
                txtInfo.Text = dr["Info"].ToString();
                dateTimeFrom.Text= dr["Date"].ToString();
                if (dr["Taswaya_Type"].ToString() == "اضافة")
                {
                    radioButtonAdd.Checked = true;
                }
                else
                {
                    radioButtonDiscount.Checked = true;
                }
            }
            dr.Close();
            setIDs();
            //setCustomerType();
            flag = true;
        }
    }
}

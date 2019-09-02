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
        SupplierTaswayaReport supplierTaswayaReport;
        DataRowView row1;
        private bool loaded = false;
        int id = -1;
        string supplierId = "";
        bool flag = false;
        public UpdateSupplierTaswaya(DataRowView row1, MainForm saleMainForm, SupplierTaswayaReport supplierTaswayaReport)
        {
            try
            {
                InitializeComponent();
                dbconnection = new MySqlConnection(connection.connectionString);
                dbconnection1 = new MySqlConnection(connection.connectionString);
                dbconnection.Open();
                this.row1 = row1;
                this.saleMainForm = saleMainForm;
                this.supplierTaswayaReport = supplierTaswayaReport;
                string query = "select * from supplier";
                MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                DataTable dt = new DataTable();
                da.Fill(dt);
                comSupplier.DataSource = dt;
                comSupplier.DisplayMember = dt.Columns["Supplier_Name"].ToString();
                comSupplier.ValueMember = dt.Columns["Supplier_ID"].ToString();
                comSupplier.Text = "";
                setData();
                loaded = true;
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
                    dbconnection.Open();
                    search();
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                dbconnection.Close();
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
                            case "txtSupplierID":
                                query = "select Supplier_Name from supplier where Supplier_ID=" + txtSupplierID.Text + "";
                                com = new MySqlCommand(query, dbconnection);
                                if (com.ExecuteScalar() != null)
                                {
                                    Name = (string)com.ExecuteScalar();
                                    loaded = false;
                                    comSupplier.Text = Name;
                                    comSupplier.SelectedValue = txtSupplierID.Text;
                                    search();
                                    loaded = true;
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
                    if (comSupplier.SelectedValue != null && txtMoney.Text != "" && (radioButtonAdd.Checked || radioButtonDiscount.Checked) && txtSupplierAccount.Text != "")
                    {
                        double paidMoney = 0;
                        if (double.TryParse(txtMoney.Text, out paidMoney))
                        {
                            dbconnection.Open();
                            string query = "update  supplier_taswaya set Supplier_ID=@Supplier_ID,Taswaya_Type=@Taswaya_Type,Money_Paid=@Money_Paid,Info=@Info,Date=@Date where SupplierTaswaya_ID=" + id;
                            MySqlCommand com = new MySqlCommand(query, dbconnection);
                            com.Parameters.Add("@Supplier_ID", MySqlDbType.Int16);
                            com.Parameters["@Supplier_ID"].Value = Convert.ToInt32(txtSupplierID.Text);
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
                            
                            UserControl.ItemRecord("supplier_taswaya", "تعديل", id, DateTime.Now, "", dbconnection);

                            query = "select Money from supplier_rest_money where Supplier_ID=" + comSupplier.SelectedValue.ToString();
                            com = new MySqlCommand(query, dbconnection);
                            if (com.ExecuteScalar() != null)
                            {
                                double restMoney = Convert.ToDouble(com.ExecuteScalar());

                                if (row1["نوع التسوية"].ToString() == "خصم")
                                {
                                    restMoney = restMoney + Convert.ToDouble(row1["قيمة التسوية"].ToString());
                                }
                                else
                                {
                                    restMoney = restMoney - Convert.ToDouble(row1["قيمة التسوية"].ToString());
                                }

                                if (radioButtonDiscount.Checked)
                                {
                                    query = "update supplier_rest_money set Money=" + (restMoney - paidMoney) + " where Supplier_ID=" + comSupplier.SelectedValue.ToString();
                                }
                                else
                                {
                                    query = "update supplier_rest_money set Money=" + (restMoney + paidMoney) + " where Supplier_ID=" + comSupplier.SelectedValue.ToString();
                                }
                                com = new MySqlCommand(query, dbconnection);
                                com.ExecuteNonQuery();
                            }

                            supplierTaswayaReport.DisplaySupplierTaswaya();
                            clear();
                        }
                        else
                        {
                            MessageBox.Show("المبلغ يجب ان يكون عدد");
                        }
                    }
                    else
                    {
                        MessageBox.Show("تاكد من ادخال البيانات المطلوبة");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }


        //function
        /*public void checkCustomerType()
        {
            string query = "select * from supplier";
            MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
            DataTable dt = new DataTable();
            da.Fill(dt);
            comSupplier.DataSource = dt;
            comSupplier.DisplayMember = dt.Columns["Supplier_Name"].ToString();
            comSupplier.ValueMember = dt.Columns["Supplier_ID"].ToString();
            comSupplier.Text = "";
            txtSupplierID.Text = "";
        }*/
        /*public void setIDs()
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
        }*/
        public void setData()
        {
            id = Convert.ToInt32(row1[0].ToString());
            string qeury = "select SupplierTaswaya_ID as 'الكود',supplier_taswaya.Supplier_ID,supplier.Supplier_Name as 'المورد',Taswaya_Type ,Money_Paid ,Info ,Date  from supplier_taswaya inner join supplier on supplier.Supplier_ID=supplier_taswaya.Supplier_ID where SupplierTaswaya_ID=" + id;
            MySqlCommand com = new MySqlCommand(qeury, dbconnection);
            MySqlDataReader dr = com.ExecuteReader();
            while (dr.Read())
            {
                supplierId = dr["Supplier_ID"].ToString();
                comSupplier.SelectedValue = supplierId;
                txtSupplierID.Text = supplierId;
                
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

            qeury = "select Money from supplier_rest_money where Supplier_ID=" + comSupplier.SelectedValue.ToString();
            com = new MySqlCommand(qeury, dbconnection);
            if (com.ExecuteScalar() != null)
            {
                txtSupplierAccount.Text = com.ExecuteScalar().ToString();
            }
            //setIDs();
            //setCustomerType();
            flag = true;
        }

        void search()
        {
            string query = "select supplier_rest_money.Money as 'المتبقى' FROM supplier_rest_money INNER JOIN supplier ON supplier.Supplier_ID = supplier_rest_money.Supplier_ID where supplier_rest_money.Supplier_ID = " + comSupplier.SelectedValue.ToString();
            MySqlCommand com = new MySqlCommand(query, dbconnection);
            if (com.ExecuteScalar() != null)
            {
                txtSupplierAccount.Text = com.ExecuteScalar().ToString();
            }
            else
            {
                txtSupplierAccount.Text = "";
            }
        }

        public void clear()
        {
            foreach (Control item in panContent.Controls)
            {
                if (item is System.Windows.Forms.ComboBox)
                {
                    item.Text = "";
                    loaded = false;
                    comSupplier.SelectedIndex = -1;
                    loaded = true;
                }
                else if (item is TextBox)
                {
                    item.Text = "";
                }
                else if (item is DateTimePicker)
                {
                    dateTimeFrom.Value = DateTime.Now;
                }
                else if (item is Panel)
                {
                    radioButtonAdd.Checked = false;
                    radioButtonDiscount.Checked = false;
                }
            }
        }
    }
}

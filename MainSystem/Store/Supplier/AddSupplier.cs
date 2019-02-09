using DevExpress.XtraTab;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MainSystem
{
    public partial class AddSupplier : Form
    {
        MySqlConnection dbconnection;
        Supplier_Report supplierReport;
        XtraTabControl xtraTabControlPurchases;
        XtraTabPage xtraTabPage;

        public AddSupplier(Supplier_Report SupplierReport, XtraTabControl XtraTabControlPurchases)
        {
            InitializeComponent();

            dbconnection = new MySqlConnection(connection.connectionString);
            supplierReport = SupplierReport;
            xtraTabControlPurchases = XtraTabControlPurchases;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                double output;
                if (double.TryParse(txtDebit.Text, out output))
                { }
                else
                {
                    MessageBox.Show("enter number");
                    dbconnection.Close();
                    return;
                }
                if (double.TryParse(txtCredit.Text, out output))
                { }
                else
                {
                    MessageBox.Show("enter number");
                    dbconnection.Close();
                    return;
                }

                if (checkPhoneExist())
                {
                    dbconnection.Open();
                    string query = "insert into Supplier (Supplier_Name,Supplier_Address,Supplier_Phone,Supplier_Fax,Supplier_Mail,Supplier_NationalID,Supplier_Start,Supplier_Info,Supplier_Credit,Supplier_Debit) values (@Name,@Address,@Phone,@Fax,@E_mail,@NationalID,@Start_Date,@Info,@Supplier_Credit,@Supplier_Debit)";
                    MySqlCommand com = new MySqlCommand(query, dbconnection);
                    com.Parameters.Add("@Name", MySqlDbType.VarChar, 255);
                    com.Parameters["@Name"].Value = txtName.Text;
                    com.Parameters.Add("@Address", MySqlDbType.VarChar, 255);
                    com.Parameters["@Address"].Value = txtAddress.Text;
                    com.Parameters.Add("@Phone", MySqlDbType.VarChar, 255);
                    com.Parameters["@Phone"].Value = txtPhone.Text;
                    com.Parameters.Add("@Fax", MySqlDbType.VarChar, 255);
                    com.Parameters["@Fax"].Value = txtFax.Text;
                    com.Parameters.Add("@E_mail", MySqlDbType.VarChar, 255);
                    com.Parameters["@E_mail"].Value = txtMail.Text;
                    com.Parameters.Add("@Start_Date", MySqlDbType.Date, 0);
                    com.Parameters["@Start_Date"].Value = dateTimePicker1.Value.Date;
                    com.Parameters.Add("@NationalID", MySqlDbType.VarChar, 255);
                    com.Parameters["@NationalID"].Value = txtNationalId.Text;
                    com.Parameters.Add("@Supplier_Credit", MySqlDbType.Decimal, 10);
                    com.Parameters["@Supplier_Credit"].Value = txtCredit.Text;
                    com.Parameters.Add("@Supplier_Debit", MySqlDbType.Decimal, 10);
                    com.Parameters["@Supplier_Debit"].Value = txtDebit.Text;
                    com.Parameters.Add("@Info", MySqlDbType.VarChar, 255);
                    com.Parameters["@Info"].Value = txtInfo.Text;

                    com.ExecuteNonQuery();
                    clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());

            }
            dbconnection.Close();
        }

        private void txtBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                xtraTabPage = getTabPage("اضافة مورد");
                if (!IsClear())
                {
                    xtraTabPage.ImageOptions.Image = Properties.Resources.unsave;
                }
                else
                {
                    xtraTabPage.ImageOptions.Image = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public bool checkPhoneExist()
        {
            string query = "SELECT customer.Customer_ID FROM customer INNER JOIN customer_phone ON customer_phone.Customer_ID = customer.Customer_ID where customer_phone.Phone='" + txtPhone.Text + "'";
            MySqlCommand com = new MySqlCommand(query, dbconnection);
            if (com.ExecuteScalar() != null)
                return false;
            else
                return true;
        }

        //clear function
        public void clear()
        {
            foreach (Control item in layoutControl1.Controls)
            {
                if (item is System.Windows.Forms.ComboBox)
                {
                    item.Text = "";
                }
                else if (item is TextBox)
                {
                    item.Text = "";
                }
            }
        }

        public XtraTabPage getTabPage(string text)
        {
            for (int i = 0; i < xtraTabControlPurchases.TabPages.Count; i++)
                if (xtraTabControlPurchases.TabPages[i].Text == text)
                {
                    return xtraTabControlPurchases.TabPages[i];
                }
            return null;
        }

        public bool IsClear()
        {
            bool flag5 = false;
            foreach (Control co in layoutControl1.Controls)
            {
                foreach (Control item in co.Controls)
                {
                    if (item is System.Windows.Forms.ComboBox)
                    {
                        if (item.Text == "")
                            flag5 = true;
                        else
                            return false;
                    }
                    else if (item is TextBox)
                    {
                        if (item.Text == "")
                            flag5 = true;
                        else
                            return false;
                    }
                }
            }

            return flag5;
        }
    }
}

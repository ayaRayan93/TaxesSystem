using DevExpress.XtraEditors;
using DevExpress.XtraTab;
using MySql.Data.MySqlClient;
using System;
using System.Collections;
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

        private void btnAddPhone_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtPhone.Text != "")
                {
                    dbconnection.Open();
                    if (checkPhoneExist())
                    {
                        for (int i = 0; i < checkedListBoxControlPhone.ItemCount; i++)
                        {
                            if (txtPhone.Text == checkedListBoxControlPhone.Items[i].Value.ToString())
                            {
                                MessageBox.Show("هذا الرقم تم اضافتة");
                                dbconnection.Close();
                                return;
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("هذا الرقم موجود من قبل");
                        dbconnection.Close();
                        return;
                    }

                    checkedListBoxControlPhone.Items.Add(txtPhone.Text);
                    txtPhone.Text = "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }

        private void btnDeletePhone_Click(object sender, EventArgs e)
        {
            try
            {
                if (checkedListBoxControlPhone.CheckedItemsCount > 0)
                {
                    if (MessageBox.Show("هل انت متاكد انك تريد الحذف؟", "", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                        return;

                    ArrayList temp = new ArrayList();
                    foreach (int index in checkedListBoxControlPhone.CheckedIndices)
                        temp.Add(checkedListBoxControlPhone.Items[index]);
                    foreach (object item in temp)
                        checkedListBoxControlPhone.Items.Remove(item);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtName.Text != "" && checkedListBoxControlPhone.ItemCount > 0)
                {
                    dbconnection.Open();
                    if (checkPhonesExist())
                    {
                        double outputDebit;
                        double outputCredit;
                        if (double.TryParse(txtDebit.Text, out outputDebit))
                        { }
                        else
                        {
                            MessageBox.Show("يجب التاكد من ادخال عدد فى مبلغ المدين");
                            dbconnection.Close();
                            return;
                        }
                        if (double.TryParse(txtCredit.Text, out outputCredit))
                        { }
                        else
                        {
                            MessageBox.Show("يجب التاكد من ادخال عدد فى مبلغ الدائن");
                            dbconnection.Close();
                            return;
                        }

                        string query = "insert into supplier (Supplier_Name,Supplier_Address,Supplier_Fax,Supplier_Mail,Supplier_Start,Supplier_Info,Supplier_Credit,Supplier_Debit) values (@Name,@Address,@Fax,@E_mail,@Start_Date,@Info,@Supplier_Credit,@Supplier_Debit)";
                        MySqlCommand com = new MySqlCommand(query, dbconnection);
                        com.Parameters.Add("@Name", MySqlDbType.VarChar, 255);
                        com.Parameters["@Name"].Value = txtName.Text;
                        com.Parameters.Add("@Address", MySqlDbType.VarChar, 255);
                        com.Parameters["@Address"].Value = txtAddress.Text;
                        com.Parameters.Add("@Fax", MySqlDbType.VarChar, 255);
                        com.Parameters["@Fax"].Value = txtFax.Text;
                        com.Parameters.Add("@E_mail", MySqlDbType.VarChar, 255);
                        com.Parameters["@E_mail"].Value = txtMail.Text;
                        com.Parameters.Add("@Start_Date", MySqlDbType.Date, 0);
                        com.Parameters["@Start_Date"].Value = dateTimePicker1.Value.Date;
                        com.Parameters.Add("@Supplier_Credit", MySqlDbType.Decimal, 10);
                        com.Parameters["@Supplier_Credit"].Value = outputCredit;
                        com.Parameters.Add("@Supplier_Debit", MySqlDbType.Decimal, 10);
                        com.Parameters["@Supplier_Debit"].Value = outputDebit;
                        com.Parameters.Add("@Info", MySqlDbType.VarChar, 255);
                        com.Parameters["@Info"].Value = txtInfo.Text;
                        com.ExecuteNonQuery();

                        AddPhoneNumbers();
                        clear();
                        xtraTabPage.ImageOptions.Image = null;
                    }
                    else
                    {
                        MessageBox.Show("يوجد رقم تليفون موجود من قبل");
                        dbconnection.Close();
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("برجاء ادخال البيانات كاملة");
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

        //functions
        public bool checkPhonesExist()
        {
            MySqlCommand com;
            for (int i = 0; i < checkedListBoxControlPhone.ItemCount; i++)
            {
                string query = "SELECT supplier.Supplier_ID FROM supplier INNER JOIN supplier_phone ON supplier_phone.Supplier_ID = supplier.Supplier_ID where supplier_phone.Phone='" + checkedListBoxControlPhone.Items[i].Value.ToString() + "'";
                com = new MySqlCommand(query, dbconnection);
                if (com.ExecuteScalar() != null)
                    return false;
            }
            return true;
        }

        public bool checkPhoneExist()
        {
            string query = "SELECT supplier.Supplier_ID FROM supplier INNER JOIN supplier_phone ON supplier_phone.Supplier_ID = supplier.Supplier_ID where supplier_phone.Phone='" + txtPhone.Text + "'";
            MySqlCommand com = new MySqlCommand(query, dbconnection);
            if (com.ExecuteScalar() != null)
                return false;
            else
                return true;
        }

        void AddPhoneNumbers()
        {
            string query = "SELECT Supplier_ID FROM supplier ORDER BY Supplier_ID DESC LIMIT 1";
            MySqlCommand com = new MySqlCommand(query, dbconnection);
            int id = 0;
            if (com.ExecuteScalar() != null)
            {
                id = (int)com.ExecuteScalar();
            }

            for (int i = 0; i < checkedListBoxControlPhone.ItemCount; i++)
            {
                query = "insert into supplier_phone(Supplier_ID,Phone) values(@Supplier_ID,@Phone)";
                com = new MySqlCommand(query, dbconnection);
                com.Parameters.Add("@Supplier_ID", MySqlDbType.Int16, 11);
                com.Parameters["@Supplier_ID"].Value = id;
                com.Parameters.Add("@Phone", MySqlDbType.VarChar, 255);
                com.Parameters["@Phone"].Value = checkedListBoxControlPhone.Items[i].Value.ToString();
                com.ExecuteNonQuery();
            }
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
                else if (item is CheckedListBoxControl)
                {
                    int cont = checkedListBoxControlPhone.ItemCount;
                    for (int i = 0; i < cont; i++)
                    {
                        checkedListBoxControlPhone.Items.RemoveAt(0);
                    }
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

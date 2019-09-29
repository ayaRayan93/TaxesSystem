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
    public partial class UpdateSupplier : Form
    {
        MySqlConnection dbconnection;
        Supplier_Report SupplierReport;
        XtraTabControl xtraTabControlPurchases;
        XtraTabPage xtraTabPage;
        DataRowView selRow;
        bool loadedflag = false;

        public UpdateSupplier(DataRowView rows, Supplier_Report supplierReport, XtraTabControl XtraTabControlPurchases)
        {
            InitializeComponent();

            dbconnection = new MySqlConnection(connection.connectionString);
            selRow = rows;
            SupplierReport = supplierReport;
            xtraTabControlPurchases = XtraTabControlPurchases;
        }

        private void UpdateSupplier_Load(object sender, EventArgs e)
        {
            try
            {
                txtName.Text = selRow["الاسم"].ToString();
                txtAddress.Text = selRow["العنوان"].ToString();
                txtMail.Text = selRow["الايميل"].ToString();
                txtFax.Text = selRow["الفاكس"].ToString();
                txtCredit.Text = selRow["دائن"].ToString();
                txtDebit.Text = selRow["مدين"].ToString();
                txtInfo.Text = selRow["البيان"].ToString();
                dateTimePicker1.Text = selRow["تاريخ البداية"].ToString();

                dbconnection.Open();
                string qury = "SELECT supplier.Supplier_ID as 'التسلسل',Phone as 'رقم التليفون' FROM supplier_phone inner join supplier on supplier.Supplier_ID=supplier_phone.Supplier_ID where supplier.Supplier_ID=" + selRow[0].ToString();
                MySqlCommand comm = new MySqlCommand(qury, dbconnection);
                MySqlDataReader dr1 = comm.ExecuteReader();
                if (dr1.HasRows)
                {
                    while (dr1.Read())
                    {
                        checkedListBoxControlPhone.Items.Add(dr1["رقم التليفون"].ToString());
                    }
                }
                dr1.Close();
                
                loadedflag = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
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

                    AddPhoneNumbers();
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
                    dbconnection.Open();
                    foreach (object item in temp)
                    {
                        string query = "delete from supplier_phone where Supplier_ID=" + selRow[0].ToString() + " and phone='" + item.ToString() + "'";
                        MySqlCommand com = new MySqlCommand(query, dbconnection);
                        com.ExecuteNonQuery();

                        checkedListBoxControlPhone.Items.Remove(item);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                Form prompt = new Form()
                {
                    Width = 500,
                    Height = 220,
                    FormBorderStyle = FormBorderStyle.FixedDialog,
                    Text = "",
                    StartPosition = FormStartPosition.CenterScreen,
                    MaximizeBox = false,
                    MinimizeBox = false
                };
                Label textLabel = new Label() { Left = 340, Top = 20, Text = "ما هو سبب التعديل؟" };
                TextBox textBox = new TextBox() { Left = 50, Top = 50, Width = 385, Multiline = true, Height = 80, RightToLeft = RightToLeft };
                Button confirmation = new Button() { Text = "تأكيد", Left = 200, Width = 100, Top = 140, DialogResult = DialogResult.OK };
                prompt.Controls.Add(textBox);
                prompt.Controls.Add(confirmation);
                prompt.Controls.Add(textLabel);
                prompt.AcceptButton = confirmation;
                if (prompt.ShowDialog() == DialogResult.OK)
                {
                    if (textBox.Text != "")
                    {
                        if (txtName.Text != "" && checkedListBoxControlPhone.ItemCount > 0)
                        {
                            dbconnection.Open();
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

                            string query = "update supplier set Supplier_Address=@Supplier_Address,Supplier_Fax=@Supplier_Fax,Supplier_Mail=@Supplier_Mail,Supplier_Start=@Supplier_Start,Supplier_Info=@Supplier_Info,Supplier_Credit=@Supplier_Credit,Supplier_Debit=@Supplier_Debit where Supplier_ID=" + selRow[0].ToString();
                            MySqlCommand com = new MySqlCommand(query, dbconnection);
                            com.Parameters.Add("@Supplier_Address", MySqlDbType.VarChar, 255);
                            com.Parameters["@Supplier_Address"].Value = txtAddress.Text;
                            com.Parameters.Add("@Supplier_Fax", MySqlDbType.VarChar, 255);
                            com.Parameters["@Supplier_Fax"].Value = txtFax.Text;
                            com.Parameters.Add("@Supplier_Mail", MySqlDbType.VarChar, 255);
                            com.Parameters["@Supplier_Mail"].Value = txtMail.Text;
                            com.Parameters.Add("@Supplier_Start", MySqlDbType.Date, 0);
                            com.Parameters["@Supplier_Start"].Value = dateTimePicker1.Value.Date;
                            com.Parameters.Add("@Supplier_Credit", MySqlDbType.Decimal, 10);
                            com.Parameters["@Supplier_Credit"].Value = outputCredit;
                            com.Parameters.Add("@Supplier_Debit", MySqlDbType.Decimal, 10);
                            com.Parameters["@Supplier_Debit"].Value = outputDebit;
                            com.Parameters.Add("@Supplier_Info", MySqlDbType.VarChar, 255);
                            com.Parameters["@Supplier_Info"].Value = txtInfo.Text;
                            com.ExecuteNonQuery();

                            UserControl.ItemRecord("supplier", "تعديل", Convert.ToInt32(selRow[0].ToString()), DateTime.Now, textBox.Text, dbconnection);

                            xtraTabPage = getTabPage("تعديل بيانات مورد");
                            xtraTabControlPurchases.TabPages.Remove(xtraTabPage);
                        }
                        else
                        {
                            MessageBox.Show("برجاء ادخال البيانات كاملة");
                        }
                    }
                    else
                    {
                        MessageBox.Show("يجب كتابة السبب");
                    }
                }
                else
                { }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());

            }
            dbconnection.Close();
        }

        private void txtBox_TextChanged(object sender, EventArgs e)
        {
            if (loadedflag)
            {
                try
                {
                    xtraTabPage = getTabPage("تعديل بيانات مورد");
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
            string query = "insert into supplier_phone(Supplier_ID,Phone) values(@Supplier_ID,@Phone)";
            MySqlCommand com = new MySqlCommand(query, dbconnection);
            com.Parameters.Add("@Supplier_ID", MySqlDbType.Int16, 11);
            com.Parameters["@Supplier_ID"].Value = Convert.ToInt32(selRow[0].ToString());
            com.Parameters.Add("@Phone", MySqlDbType.VarChar, 255);
            com.Parameters["@Phone"].Value = txtPhone.Text;
            com.ExecuteNonQuery();
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

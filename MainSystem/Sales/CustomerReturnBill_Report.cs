using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Card;
using DevExpress.XtraGrid.Views.Grid;
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
    public partial class CustomerReturnBill_Report : Form
    {
        MySqlConnection dbconnection;
        MainForm salesMainForm=null;
        bool loaded = false;
        bool flag = false;

        public CustomerReturnBill_Report(MainForm SalesMainForm)
        {
            try
            {
                InitializeComponent();
                this.salesMainForm = SalesMainForm;
                dbconnection = new MySqlConnection(connection.connectionString);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
           
        }

        private void CustomerReturnBill_Report_Load(object sender, EventArgs e)
        {
            try
            {
                loadDataToBox();
                DisplayBills();
                loaded = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void comBox_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (loaded)
                {
                    ComboBox comBox = (ComboBox)sender;

                    switch (comBox.Name)
                    {
                        case "comBranch":
                            if (loaded)
                            {
                                txtBranchID.Text = comBranch.SelectedValue.ToString();
                                DisplayBillNumber();
                            }
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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
                            case "txtBranchID":
                                query = "select Branch_Name from branch where Branch_ID=" + txtBranchID.Text;
                                com = new MySqlCommand(query, dbconnection);
                                if (com.ExecuteScalar() != null)
                                {
                                    Name = (string)com.ExecuteScalar();
                                    comBranch.Text = Name;
                                    DisplayBillNumber();
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
                catch
                {
                  //  MessageBox.Show(ex.ToString());
                }
                dbconnection.Close();
            }
        }
        
        private void btnAddNew_Click(object sender, EventArgs e)
        {
            try
            {
              salesMainForm.bindRecordCustomerReturnBillForm(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                dbconnection.Open();
                DataRowView billRow = (DataRowView)(((GridView)dataGridView1.MainView).GetRow(((GridView)dataGridView1.MainView).GetSelectedRows()[0]));
                
                if (billRow != null)
                {
                    DialogResult dialogResult = MessageBox.Show("Are you sure you want to delete the item?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dialogResult == DialogResult.Yes)
                    {

                        deleteBill(Convert.ToInt32(billRow[0].ToString()));

                        string query = "ALTER TABLE customer_return_bill AUTO_INCREMENT = 1;";
                        MySqlCommand com = new MySqlCommand(query, dbconnection);
                        com.ExecuteNonQuery();

                        UserControl.ItemRecord("customer_return_bill", "حذف", Convert.ToInt32(billRow[0].ToString()), DateTime.Now, "", dbconnection);
                        dbconnection.Close();
                        DisplayBills();
                        loadDataToBox();
                    }
                    else if (dialogResult == DialogResult.No)
                    { }

                }
                else
                {
                    MessageBox.Show("select row");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            dbconnection.Close();
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            /*try
            {
                salesMainForm.bindReportOffersForm(dataGridView1);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }*/
        }

        private void btnNewChooes_Click(object sender, EventArgs e)
        {
            try
            {
                comBills.Text = "";
                comBranch.Text = "";
                txtBranchID.Text = "";

                DisplayBills();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //function
        public void DisplayBills()
        {
            try
            {
                dbconnection.Open();
                loaded = false;
                string q1 = "";

                if (comBills.Text == "")
                {
                    q1 = "select CustomerReturnBill_ID from customer_return_bill";
                }
                else
                {
                    q1 = comBills.SelectedValue.ToString();
                }
                //,concat(customer_bill.Branch_BillNumber,' ',customer_bill.Branch_Name) as 'تابعة لفاتورة'  INNER JOIN customer_bill ON customer_return_bill.CustomerBill_ID = customer_bill.CustomerBill_ID
                string query = "SELECT customer_return_bill.CustomerReturnBill_ID AS 'التسلسل',branch.Branch_Name AS 'الفرع',customer_return_bill.Branch_BillNumber AS 'فاتورة رقم',customer_return_bill.Date AS 'التاريخ',customer_return_bill.TotalCostAD AS 'الاجمالى',customer_return_bill.ReturnInfo AS 'البيان' FROM customer_return_bill INNER JOIN branch ON branch.Branch_ID = customer_return_bill.Branch_ID  where customer_return_bill.CustomerReturnBill_ID IN(" + q1 + ") order by customer_return_bill.CustomerReturnBill_ID";
                MySqlDataAdapter adapterBill = new MySqlDataAdapter(query, dbconnection);
                
                query = "SELECT customer_return_bill_details.CustomerReturnBill_ID as 'التسلسل',data.Code as 'الكود',customer_return_bill_details.TotalMeter as 'الكمية',customer_return_bill_details.PriceAD as 'السعر',customer_return_bill_details.SellDiscount as 'نسبة الخصم',customer_return_bill_details.TotalAD as 'الاجمالى',product.Product_Name as 'الصنف',color.Color_Name as 'اللون',size.Size_Value as 'المقاس',sort.Sort_Value as 'الفرز',data.Description as 'الوصف',data.Carton as 'الكرتنة',data_photo.Photo as 'صورة' FROM customer_return_bill_details INNER JOIN customer_return_bill ON customer_return_bill.CustomerReturnBill_ID = customer_return_bill_details.CustomerReturnBill_ID INNER JOIN data on data.Data_ID=customer_return_bill_details.Data_ID INNER JOIN type ON type.Type_ID = data.Type_ID INNER JOIN product ON product.Product_ID = data.Product_ID INNER JOIN factory ON data.Factory_ID = factory.Factory_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID LEFT outer JOIN color ON data.Color_ID = color.Color_ID LEFT outer  JOIN size ON data.Size_ID = size.Size_ID LEFT outer  JOIN sort ON data.Sort_ID = sort.Sort_ID left join data_photo on data_photo.Data_ID=data.Data_ID where customer_return_bill_details.CustomerReturnBill_ID IN(" + q1 + ") and customer_return_bill_details.Type='بند' order by data.Data_ID";
                MySqlDataAdapter AdapterProducts = new MySqlDataAdapter(query, dbconnection);

                query = "SELECT customer_return_bill_details.CustomerReturnBill_ID as 'التسلسل',customer_return_bill_details.TotalMeter as 'الكمية',customer_return_bill_details.PriceAD as 'السعر',customer_return_bill_details.SellDiscount as 'نسبة الخصم',customer_return_bill_details.TotalAD as 'الاجمالى',sets.Set_Name as 'الصنف',sets.Description as 'الوصف',set_photo.Photo as 'صورة' FROM customer_return_bill_details INNER JOIN customer_return_bill ON customer_return_bill.CustomerReturnBill_ID = customer_return_bill_details.CustomerReturnBill_ID INNER JOIN sets on sets.Set_ID=customer_return_bill_details.Data_ID INNER JOIN type ON type.Type_ID = sets.Type_ID INNER JOIN factory ON sets.Factory_ID = factory.Factory_ID INNER JOIN groupo ON sets.Group_ID = groupo.Group_ID left join set_photo on set_photo.Set_ID=sets.Set_ID where customer_return_bill_details.CustomerReturnBill_ID IN(" + q1 + ") and customer_return_bill_details.Type='طقم' order by sets.Set_ID";
                MySqlDataAdapter AdapterSets = new MySqlDataAdapter(query, dbconnection);

                DataSet dataSet11 = new DataSet();

                //Create DataTable objects for representing database's tables 
                adapterBill.Fill(dataSet11, "customer_return_bill");
                AdapterProducts.Fill(dataSet11, "customer_return_bill_details");
                AdapterSets.Fill(dataSet11, "customer_return_bill_details");

                //Set up a master-detail relationship between the DataTables 
                DataColumn keyColumn = dataSet11.Tables["customer_return_bill"].Columns["التسلسل"];
                DataColumn foreignKeyColumn = dataSet11.Tables["customer_return_bill_details"].Columns["التسلسل"];
                dataSet11.Relations.Add("بنود الفاتورة", keyColumn, foreignKeyColumn);

                //Bind the grid control to the data source 
                dataGridView1.DataSource = dataSet11.Tables["customer_return_bill"];
                loaded = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();

        }

        public void deleteBill(int id)
        {
            string query = "delete from customer_return_bill where CustomerReturnBill_ID=" + id;
            MySqlCommand com = new MySqlCommand(query,dbconnection);
            com.ExecuteNonQuery();
        }

        public void loadDataToBox()
        {
            string query = "select Branch_Name,Branch_ID from branch ";
            MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
            DataTable dt = new DataTable();
            da.Fill(dt);
            comBranch.DataSource = dt;
            comBranch.DisplayMember = dt.Columns["Branch_Name"].ToString();
            comBranch.ValueMember = dt.Columns["Branch_ID"].ToString();
            comBranch.Text = "";
            txtBranchID.Text = "";
        }
        
        //display bill number for selected customer/client
        public void DisplayBillNumber()
        {
            if (txtBranchID.Text != "")
            {
                label3.Visible = true;
                comBills.Visible = true;
                try
                {
                    flag = false;
                    dbconnection.Open();

                    string query = "select Branch_BillNumber,CustomerReturnBill_ID from customer_return_bill where Branch_ID=" + txtBranchID.Text;
                    
                    MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    comBills.DataSource = dt;
                    comBills.DisplayMember = dt.Columns["Branch_BillNumber"].ToString();
                    comBills.ValueMember = dt.Columns["CustomerReturnBill_ID"].ToString();
                    comBills.Text = "";
                    flag = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                dbconnection.Close();
            }
        }

        private void comBills_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (flag)
                {
                    DisplayBills();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}

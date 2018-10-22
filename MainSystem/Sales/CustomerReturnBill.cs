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
    public partial class CustomerReturnBill : Form
    {
        MySqlConnection dbconnection , connectionReader, connectionReader2;
        private string Customer_Type;
        private bool loaded = false;
        private bool flag = false;
        bool comBranchLoaded = false;
        DataGridViewRow row1;
        private int[] addedRecordIDs;
        int recordCount = 0;
        private bool Added = false;
        DataGridViewRow[] myRows;
        List<int> listOfRow2In;
        private string[] listOfCode;//code of item which added to bill
       
        public CustomerReturnBill(MainForm mainForm)
        {
            InitializeComponent();
            dbconnection = new MySqlConnection(connection.connectionString);
            connectionReader = new MySqlConnection(connection.connectionString);
            connectionReader2 = new MySqlConnection(connection.connectionString);
            addedRecordIDs = new int[100];
            listOfCode = new string[100];
            listOfRow2In = new List<int>();
            labClient.Visible = false;
            labCustomer.Visible = false;//label of مهندس/مقاول
            comClient.Visible = false;
            comCustomer.Visible = false;
            txtCustomerID.Visible = false;
            txtClientID.Visible = false;

            labBillNumber.Visible = false;
            comBillNumber.Visible = false;
        }

        private void CustomerReturnBill_Load(object sender, EventArgs e)
        {
            try
            {
                string query = "select * from branch";
                MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                DataTable dt = new DataTable();
                da.Fill(dt);
                comBranch.DataSource = dt;
                comBranch.DisplayMember = dt.Columns["Branch_Name"].ToString();
                comBranch.ValueMember = dt.Columns["Branch_ID"].ToString();
                comBranch.Text = "";
                txtBranchID.Text = "";
                comBranchLoaded = true;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //check type of customer if engineer,client or contract 
        private void radiotype_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton radio = (RadioButton)sender;
            Customer_Type = radio.Text;
            groupBox2.Visible = true;
            labBillNumber.Visible = false;
            comBillNumber.Visible = false;
            loaded = false;
            try
            {
                if (Customer_Type == "عميل")
                {
                    labCustomer.Visible = false;
                    comCustomer.Visible = false;
                    txtCustomerID.Visible = false;
                    labClient.Visible = true;
                    comClient.Visible = true;
                    txtClientID.Visible = true;

                    string query = "select * from customer where Customer_Type='" + Customer_Type + "'";
                    MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    comClient.DataSource = dt;
                    comClient.DisplayMember = dt.Columns["Customer_Name"].ToString();
                    comClient.ValueMember = dt.Columns["Customer_ID"].ToString();
                    comClient.Text = "";

                    txtClientID.Text = "";
                }
                else
                {
                    labCustomer.Visible = true;
                    comCustomer.Visible = true;
                    txtCustomerID.Visible = true;
                    labClient.Visible = false;
                    comClient.Visible = false;
                    txtClientID.Visible = false;


                    string query = "select * from customer where Customer_Type='" + Customer_Type + "'";
                    MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    comCustomer.DataSource = dt;
                    comCustomer.DisplayMember = dt.Columns["Customer_Name"].ToString();
                    comCustomer.ValueMember = dt.Columns["Customer_ID"].ToString();
                    comCustomer.Text = "";
                    txtCustomerID.Text = "";


                }
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
                    txtClientID.Text = comClient.SelectedValue.ToString();
                    if (txtBranchID.Text != "")
                    {
                        DisplayBillNumber(Convert.ToInt16(comCustomer.SelectedValue), Convert.ToInt16(comClient.SelectedValue));
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        //when select customer(مهندس,مقاول)display in comCustomer the all clients of th customer 
        private void comCustomer_SelectedValueChanged(object sender, EventArgs e)
        {
            if (loaded)
            {
                txtCustomerID.Text = comCustomer.SelectedValue.ToString();
                if (txtBranchID.Text != "")
                {
                    DisplayBillNumber(Convert.ToInt16(comCustomer.SelectedValue), Convert.ToInt16(comClient.SelectedValue));
                }

                labClient.Visible = true;
                comClient.Visible = true;
                txtClientID.Visible = true;

                try
                {
                    loaded = false;
                    string query = "select * from customer where Customer_ID in(select Client_ID from custmer_client where Customer_ID=" + comCustomer.SelectedValue + ")";
                    MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    comClient.DataSource = dt;
                    comClient.DisplayMember = dt.Columns["Customer_Name"].ToString();
                    comClient.ValueMember = dt.Columns["Customer_ID"].ToString();
                    comClient.Text = "";
                    comClient.SelectedValue = 0;
                    txtClientID.Text = "";
                    loaded = true;
                    //DisplayBillNumber(Convert.ToInt16(comCustomer.SelectedValue), Convert.ToInt16(comClient.SelectedValue));
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
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
                            case "txtCustomerID":
                                query = "select Customer_Name from customer where Customer_ID=" + txtClientID.Text + "";
                                com = new MySqlCommand(query, dbconnection);
                                if (com.ExecuteScalar() != null)
                                {
                                    Name = (string)com.ExecuteScalar();
                                    comClient.Text = Name;
                                    comClient.SelectedValue = txtClientID.Text;

                                }
                                else
                                {
                                    MessageBox.Show("there is no item with this id");
                                    dbconnection.Close();
                                    return;
                                }
                                break;
                            case "txtEngConID":
                                query = "select Customer_Name from customer where Customer_ID=" + txtCustomerID.Text + "";
                                com = new MySqlCommand(query, dbconnection);
                                if (com.ExecuteScalar() != null)
                                {
                                    Name = (string)com.ExecuteScalar();
                                    comCustomer.Text = Name;
                                    comCustomer.SelectedValue = txtCustomerID.Text;
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

        private void comBillNumber_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (flag && comBillNumber.Text != "")
                {
                    int billNum = Convert.ToInt16(comBillNumber.Text);
                    string query = "select data.Data_ID, data.Code as 'الكود',product_bill.Quantity as ' الكمية',product_bill.Price as 'السعر',product_bill.Discount as 'خصم البيع',product_bill.PriceAD as 'السعر بعد الخصم' , product.Product_Name as 'الصنف', type.Type_Name as 'النوع', factory.Factory_Name as 'المصنع' ,groupo.Group_Name as 'المجموعة' ,color.Color_Name as 'اللون',size.Size_Value as 'المقاس',sort.Sort_Value as 'الفرز',data.Classification as 'التصنيف', data.Description as 'الوصف'  from product_bill inner join data on data.Data_ID=product_bill.Data_ID LEFT JOIN color ON color.Color_ID = data.Color_ID LEFT JOIN size ON size.Size_ID = data.Size_ID LEFT JOIN sort ON sort.Sort_ID = data.Sort_ID INNER JOIN type ON type.Type_ID = data.Type_ID INNER JOIN product ON product.Product_ID = data.Product_ID INNER JOIN factory ON data.Factory_ID = factory.Factory_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID  where product_bill.CustomerBill_ID=" + comBillNumber.SelectedValue;

                    MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dataGridView1.DataSource = dt;
                    dataGridView1.Columns[0].Visible = false;
                    dataGridView2.Rows.Clear();
                    
                    dbconnection.Open();
                    query = "select * from customer_bill where CustomerBill_ID=" + comBillNumber.SelectedValue;
                    MySqlCommand com = new MySqlCommand(query, dbconnection);
                    MySqlDataReader dr = com.ExecuteReader();
                    while (dr.Read())
                    {
                        //labBillTotalCostBD.Text = dr["Total_CostBD"].ToString();
                        labBillTotalCostAD.Text = dr["Total_CostAD"].ToString();
                        labBillDate.Text = Convert.ToDateTime(dr["Bill_Date"].ToString()).ToShortDateString();
                    }
                    dr.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                Added = false;
                for (int i = 0; i < recordCount; i++)
                {
                    if (addedRecordIDs[i] == dataGridView1.SelectedCells[0].RowIndex + 1)
                        Added = true;
                }

                row1 = dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex];
                txtCode.Text = row1.Cells["الكود"].Value.ToString();
                txtTotalMeter.Text = row1.Cells["الكمية"].Value.ToString();
                //   txtPrice.Text= row1.Cells["السعر"].Value.ToString();
                txtPriceAD.Text = row1.Cells["السعر بعد الخصم"].Value.ToString();
                //  txtDiscount.Text= row1.Cells["خصم البيع"].Value.ToString();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txtTotalMeter_TextChanged(object sender, EventArgs e)
        {
            try
            {
                double quantity, priceAD;
                if (txtTotalMeter.Text != "" && txtPriceAD.Text != "")
                {
                    if (double.TryParse(txtTotalMeter.Text, out quantity) & double.TryParse(txtPriceAD.Text, out priceAD))
                    {
                        labTotalAD.Text = (priceAD * quantity).ToString();
                    }
                    else
                    {
                        MessageBox.Show("enter correct value");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnAddToReturnBill_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Added && row1 != null && txtCode.Text != "")
                {
                    addedRecordIDs[recordCount] = dataGridView1.SelectedCells[0].RowIndex + 1;
                    listOfCode[recordCount] = txtCode.Text;
                    dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex].DefaultCellStyle.BackColor = Color.Silver;
                    recordCount++;
                    int n = dataGridView2.Rows.Add();
                    dataGridView2.Rows[n].Cells["Data_ID"].Value = row1.Cells["Data_ID"].Value;
                    dataGridView2.Rows[n].Cells["Code"].Value = txtCode.Text;
                    dataGridView2.Rows[n].Cells["Quantity"].Value = txtTotalMeter.Text;
                    dataGridView2.Rows[n].Cells["priceAD"].Value = txtPriceAD.Text;
                    dataGridView2.Rows[n].Cells["totalAD"].Value = labTotalAD.Text;

                    dataGridView2.Rows[n].Cells["Type_Name"].Value = row1.Cells["النوع"].Value;
                    dataGridView2.Rows[n].Cells["Factory_Name"].Value = row1.Cells["المصنع"].Value;
                    dataGridView2.Rows[n].Cells["Group_Name"].Value = row1.Cells["المجموعة"].Value;
                    dataGridView2.Rows[n].Cells["Product_Name"].Value = row1.Cells["الصنف"].Value;
                    dataGridView2.Rows[n].Cells["Colour"].Value = row1.Cells["اللون"].Value;
                    dataGridView2.Rows[n].Cells["Size"].Value = row1.Cells["المقاس"].Value;
                    dataGridView2.Rows[n].Cells["Sort"].Value = row1.Cells["الفرز"].Value;
                    dataGridView2.Rows[n].Cells["Classification"].Value = row1.Cells["التصنيف"].Value;
                    dataGridView2.Rows[n].Cells["Description"].Value = row1.Cells["الوصف"].Value;

                    listOfRow2In.Add(n);
                    double totalAD = 0;
                    foreach (DataGridViewRow item in dataGridView2.Rows)
                    {
                        totalAD += Convert.ToDouble(item.Cells["totalAD"].Value);
                    }
                    labTotalReturnBillAD.Text = totalAD.ToString();
                }
                else
                {
                    MessageBox.Show("this recorded already added or select row please");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnCreateReturnBill_Click(object sender, EventArgs e)
        {
            try
            {
                dbconnection.Open();
                if (labBillTotalCostAD.Text != "")
                {
                    string query = "select Branch_BillNumber from customer_return_bill where Branch_ID=" + txtBranchID.Text+ " order by Branch_BillNumber limit 1";
                    MySqlCommand com = new MySqlCommand(query, dbconnection);
                    int Branch_BillNumber = 1;
                    if (com.ExecuteScalar() != null)
                    {
                        Branch_BillNumber = Convert.ToInt16(com.ExecuteScalar());
                        Branch_BillNumber++;
                    }
                    
                    query = "insert into customer_return_bill (Type_Buy,Delegate_ID,Delegate_Name,Branch_BillNumber,Branch_ID,Branch_Name,CustomerBill_ID,Customer_ID,Client_ID,Date,TotalCostAD,ReturnInfo) values (@Type_Buy,@Delegate_ID,@Delegate_Name,@Branch_BillNumber,@Branch_ID,@Branch_Name,@CustomerBill_ID,@Customer_ID,@Client_ID,@Date,@TotalCostAD,@ReturnInfo)";
                    com = new MySqlCommand(query, dbconnection);
                    com.Parameters.Add("@Branch_BillNumber", MySqlDbType.Int16);
                    com.Parameters["@Branch_BillNumber"].Value = Convert.ToInt16(Branch_BillNumber);
                    com.Parameters.Add("@Branch_ID", MySqlDbType.Int16);
                    com.Parameters["@Branch_ID"].Value = Convert.ToInt16(txtBranchID.Text);
                    com.Parameters.Add("@Branch_Name", MySqlDbType.VarChar);
                    com.Parameters["@Branch_Name"].Value = comBranch.Text;
                    com.Parameters.Add("@CustomerBill_ID", MySqlDbType.Int16);
                    com.Parameters["@CustomerBill_ID"].Value = Convert.ToInt16(comBillNumber.Text);
                    com.Parameters.Add("@Customer_ID", MySqlDbType.Int16);
                    com.Parameters["@Customer_ID"].Value = Convert.ToInt16(txtCustomerID.Text);
                    com.Parameters.Add("@Client_ID", MySqlDbType.Int16);
                    com.Parameters["@Client_ID"].Value = Convert.ToInt16(txtClientID.Text);
                    query = "select customer_bill.Type_Buy,customer_bill.Delegate_ID ,Delegate_Name from customer_bill inner join delegate on delegate.Delegate_ID=customer_bill.Delegate_ID where customer_bill.Branch_BillNumber=" + comBillNumber.Text + " and customer_bill.Branch_ID=" + txtBranchID.Text;
                    MySqlCommand com1 = new MySqlCommand(query, dbconnection);
                    MySqlDataReader dr1 = com1.ExecuteReader();
                    while (dr1.Read())
                    {
                        com.Parameters.Add("@Type_Buy", MySqlDbType.VarChar);
                        com.Parameters["@Type_Buy"].Value = dr1["Type_Buy"].ToString();
                        com.Parameters.Add("@Delegate_ID", MySqlDbType.Int16);
                        com.Parameters["@Delegate_ID"].Value = Convert.ToInt16(dr1["Delegate_ID"].ToString());
                        com.Parameters.Add("@Delegate_Name", MySqlDbType.VarChar);
                        com.Parameters["@Delegate_Name"].Value = dr1["Delegate_Name"].ToString();
                    }
                    dr1.Close();
                    com.Parameters.Add("@Date", MySqlDbType.Date);
                    com.Parameters["@Date"].Value = DateTime.Now.Date;
                    if (txtInfo.Text != "")
                    {
                        com.Parameters.Add("@ReturnInfo", MySqlDbType.VarChar);
                        com.Parameters["@ReturnInfo"].Value = txtInfo.Text;
                    }
                    else
                    {
                        com.Parameters.Add("@ReturnInfo", MySqlDbType.VarChar);
                        com.Parameters["@ReturnInfo"].Value = "";
                    }
                    com.Parameters.Add("@TotalCostAD", MySqlDbType.Decimal);
                    com.Parameters["@TotalCostAD"].Value = Convert.ToDouble(labBillTotalCostAD.Text);
                    com.ExecuteNonQuery();

                    query = "select CustomerReturnBill_ID from customer_return_bill order by CustomerReturnBill_ID desc limit 1";
                    com = new MySqlCommand(query, dbconnection);
                    int id = Convert.ToInt16(com.ExecuteScalar());

                    query = "insert into customer_return_bill_details (CustomerReturnBill_ID,Data_ID,TotalMeter,PriceAD,TotalAD)values (@CustomerReturnBill_ID,@Data_ID,@TotalMeter,@PriceAD,@TotalAD)";
                    com = new MySqlCommand(query, dbconnection);
                    com.Parameters.Add("@CustomerReturnBill_ID", MySqlDbType.Int16);
                    com.Parameters.Add("@Data_ID", MySqlDbType.VarChar);
                    com.Parameters.Add("@TotalMeter", MySqlDbType.Decimal);
                    com.Parameters.Add("@PriceAD", MySqlDbType.Decimal);
                    com.Parameters.Add("@TotalAD", MySqlDbType.Decimal);
                    foreach (DataGridViewRow row1 in dataGridView2.Rows)
                    {
                        if (row1.Cells[0].Value != null)
                        {
                            com.Parameters["@CustomerReturnBill_ID"].Value = id;

                            com.Parameters["@Data_ID"].Value = row1.Cells[0].Value;

                            com.Parameters["@TotalMeter"].Value = Convert.ToDouble(row1.Cells[1].Value);

                            com.Parameters["@PriceAD"].Value = Convert.ToDouble(row1.Cells[2].Value);

                            com.Parameters["@TotalAD"].Value = Convert.ToDouble(row1.Cells[3].Value);

                            com.ExecuteNonQuery();
                        }

                    }
                    INcreaseProductQuantity(id);
                    MessageBox.Show("insert Done");
                    dbconnection.Close();


                    ////////////////////////////////////////////////////////

                    DataTable dt = new DataTable();
                    for (int i = 0; i < dataGridView2.Columns.Count; i++)
                    {
                        dt.Columns.Add(dataGridView2.Columns[i].Name.ToString());
                    }
                    for (int i = 0; i < dataGridView2.Rows.Count - 1; i++)
                    {
                        DataRow dr = dt.NewRow();
                        for (int j = 0; j < dataGridView2.Columns.Count; j++)
                        {
                            dr[dataGridView2.Columns[j].Name.ToString()] = dataGridView2.Rows[i].Cells[j].Value;
                        }

                        dt.Rows.Add(dr);
                    }

                    if (comClient.Text != "" && txtClientID.Text != "")
                    {
                        dbconnection.Open();
                        string q = "select Customer_Phone from customer where Customer_ID=" + txtClientID.Text;
                        MySqlCommand comand = new MySqlCommand(q, dbconnection);
                        string phone = comand.ExecuteScalar().ToString();
                        dbconnection.Close();

                        dbconnection.Open();
                        q = "select Customer_Address from customer where Customer_ID=" + txtClientID.Text;
                        comand = new MySqlCommand(q, dbconnection);
                        string address = comand.ExecuteScalar().ToString();
                        dbconnection.Close();

                        //CustomerReturnBill_CrystalReport f = new CustomerReturnBill_CrystalReport(dt, Branch_BillNumber, dateTimePicker1.Value.Date, comClient.Text, phone, address, comBranch.Text);
                        //f.Show();
                        //this.Hide();
                    }
                    else if (comCustomer.Text != "" && txtCustomerID.Text != "")
                    {
                        dbconnection.Open();
                        string q = "select Customer_Phone from customer where Customer_ID=" + txtCustomerID.Text;
                        MySqlCommand comand = new MySqlCommand(q, dbconnection);
                        string phone = comand.ExecuteScalar().ToString();
                        dbconnection.Close();

                        dbconnection.Open();
                        q = "select Customer_Address from customer where Customer_ID=" + txtCustomerID.Text;
                        comand = new MySqlCommand(q, dbconnection);
                        string address = comand.ExecuteScalar().ToString();
                        dbconnection.Close();

                        //CustomerReturnBill_CrystalReport f = new CustomerReturnBill_CrystalReport(dt, Branch_BillNumber, dateTimePicker1.Value.Date, comCustomer.Text, phone, address, comBranch.Text);
                        //f.Show();
                        //this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("fill requird fields");
                    }

                    clrearAll();
                }
                else
                {
                    MessageBox.Show("return bill is empty insert row");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                int dgv2Index = dataGridView2.SelectedCells[0].RowIndex;
                for (int i = 0; i < listOfRow2In.Count; i++)
                {
                    if (listOfRow2In[i] == dgv2Index)
                    {
                        dataGridView2.Rows.Remove(dataGridView2.Rows[dgv2Index]);
                        dataGridView1.Rows[addedRecordIDs[i] - 1].DefaultCellStyle.BackColor = Color.White;
                        addedRecordIDs[i] = 0;
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
                if (comBranchLoaded)
                {
                    txtBranchID.Text = comBranch.SelectedValue.ToString();
                    if (txtCustomerID.Text != "")
                    {
                        DisplayBillNumber(Convert.ToInt16(comCustomer.SelectedValue), Convert.ToInt16(comClient.SelectedValue));
                    }
                    else
                    {
                        DisplayBillNumber(0, Convert.ToInt16(comClient.SelectedValue));
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
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
        
        private void CustomerReturnBill_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                Environment.Exit(0);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
           
        }

        //function
        //display bill number for selected customer/client
        public void DisplayBillNumber(int customerID, int clientID)
        {
            if (txtBranchID.Text != "")
            {
                labBillNumber.Visible = true;
                comBillNumber.Visible = true;
                string strQuery = "";
                try
                {
                    dbconnection.Open();

                    if (clientID > 0)
                    {

                        strQuery = " and Client_ID = " + clientID + "";
                    }
                    if (customerID > 0)
                    {

                        strQuery += " and Customer_ID = " + customerID + "";
                    }
                    comBillNumber.DataSource = null;
                    flag = false;
                    string query = "";

                    if (strQuery != "")
                    {
                        query = "select Branch_BillNumber,CustomerBill_ID from customer_bill where Branch_ID=" + txtBranchID.Text + strQuery;
                    }
                    else
                    {
                        query = "select Branch_BillNumber,CustomerBill_ID from customer_bill where Branch_ID=" + txtBranchID.Text;
                    }
                    MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    comBillNumber.DataSource = dt;
                    comBillNumber.DisplayMember = dt.Columns["Branch_BillNumber"].ToString();
                    comBillNumber.ValueMember= dt.Columns["CustomerBill_ID"].ToString();
                    comBillNumber.Text = "";

                    flag = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                dbconnection.Close();
            }
        }

        //clear all fields
        private void clrearAll()
        {
            try
            {
                radClient.Checked = false;
                radCon.Checked = false;
                radEng.Checked = false;

                comBillNumber.Text = comClient.Text = comCustomer.Text = txtPriceAD.Text= "";

                txtClientID.Text = txtCustomerID.Text = txtCode.Text =/* txtPrice.Text = txtDiscount.Text = txtPrice.Text =*/ txtTotalMeter.Text = "";

                labBillDate.Text = labBillTotalCostAD.Text =/* labBillTotalCostBD.Text = labTotalAD.Text = labTotalBD.Text =*/ labTotalReturnBillAD.Text = "";

                dataGridView1.DataSource = null;
                dataGridView2.Rows.Clear();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        bool IsAdded(DataGridViewRow row1)
        {
            foreach (DataGridViewRow item in myRows)
            {
                if (row1 == item)
                    return true;
            }
            return false;
        }

        //return quantity to store
        public void INcreaseProductQuantity(int billNumber)
        {
           
            connectionReader.Open();
            connectionReader2.Open();
            string q;
            int id;
            bool flag = false;
            double storageQ, productQ;
            string query = "select Data_ID,TotalMeter from customer_return_bill_details where CustomerReturnBill_ID=" + billNumber;
            MySqlCommand com = new MySqlCommand(query, connectionReader);
            MySqlDataReader dr = com.ExecuteReader();

            while (dr.Read())
            {

                string query2 = "select Storage_ID,Total_Meters from storage where Data_ID='" + dr["Data_ID"].ToString()+"'";
                MySqlCommand com2 = new MySqlCommand(query2, connectionReader2);
                MySqlDataReader dr2 = com2.ExecuteReader();
                while (dr2.Read())
                {

                    storageQ = Convert.ToDouble(dr2["Total_Meters"]);
                    productQ = Convert.ToDouble(dr["TotalMeter"]);
                       
                    storageQ += productQ;
                    id = Convert.ToInt16(dr2["Storage_ID"]);
                    q = "update storage set Total_Meters=" + storageQ + " where Storage_ID=" + id;
                    MySqlCommand comm = new MySqlCommand(q, dbconnection);
                    comm.ExecuteNonQuery();
                    flag = true;
                    break;
                        
                }
                dr2.Close();

                query2 = "select StorageTaxesID,Total_Meters from storage_taxes where Data_ID='" + dr["Data_ID"].ToString()+"'";
                com2 = new MySqlCommand(query2, connectionReader2);
                dr2 = com2.ExecuteReader();
                while (dr2.Read())
                {

                    storageQ = Convert.ToDouble(dr2["Total_Meters"]);
                    productQ = Convert.ToDouble(dr["TotalMeter"]);

                    storageQ += productQ;
                    id = Convert.ToInt16(dr2["StorageTaxesID"]);
                    q = "update storage_taxes set Total_Meters=" + storageQ + " where StorageTaxesID=" + id;
                    MySqlCommand comm = new MySqlCommand(q, dbconnection);
                    comm.ExecuteNonQuery();
                    flag = true;
                    break;

                }
                dr2.Close();

                if (!flag)
                {
                    MessageBox.Show(dr["Data_ID"].ToString() + "not valid in store");
                }
                flag = false;
            }
            dr.Close();
            
            connectionReader2.Close();
            connectionReader.Close();
        }
    }
}

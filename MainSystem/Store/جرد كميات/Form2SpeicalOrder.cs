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
    public partial class Form2SpeicalOrder : Form
    {
        MySqlConnection dbconnection;
        bool loaded = false;
        bool finish = false;
        private int recordFinishedCount=0;
        string[] recordFinishedCode;
        private int recordRestCount=0;
        string[] addedRecordRestCode;
        private int dgv2RowsCount = 0;
        string[] dgv2RowsCode;
        DataGridViewRow row1;
        int storeId, branchID=0, BranchBillNum=0;
        public Form2SpeicalOrder()
        {
            InitializeComponent();
            dbconnection = new MySqlConnection(connection.connectionString);
            recordFinishedCode = new string[50];
            addedRecordRestCode= new string[50];
            dgv2RowsCode = new string[50];
        }

        private void Form1_Load(object sender, EventArgs e)
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

                query = "select * from store";
                da = new MySqlDataAdapter(query, dbconnection);
                dt = new DataTable();
                da.Fill(dt);
                comStore.DataSource = dt;
                comStore.DisplayMember = dt.Columns["Store_Name"].ToString();
                comStore.ValueMember = dt.Columns["Store_ID"].ToString();
                comStore.Text = "";
                txtStoreID.Text = "";
                
                loaded = true;

                dbconnection.Open();
                query = "select * from received_bill_store";
                MySqlCommand com = new MySqlCommand(query,dbconnection);
                MySqlDataReader dr = com.ExecuteReader();
                while (dr.Read())
                {
                    if (dr["finish"].ToString() == "نعم")
                    {
                        recordFinishedCode[recordFinishedCount] = dr["Code"].ToString() +"*"+ dr["Branch_BillNumber"].ToString() +"*"+ dr["Branch_ID"].ToString();
                        recordFinishedCount++;
                    }
                    else if (dr["finish"].ToString() == "لا")
                    {
                        addedRecordRestCode[recordRestCount] = dr["Code"].ToString()+"*" + dr["Branch_BillNumber"].ToString()+"*" + dr["Branch_ID"].ToString();
                        recordRestCount++;
                    }
                    else
                    {
                        MessageBox.Show("error");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }

        private void comBranch_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (loaded)
                {
                    txtBranchID.Text = comBranch.SelectedValue.ToString();
                    label2.Visible = true;
                    txtBillNumber.Visible = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void comStore_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (loaded)
                {
                    txtStoreID.Text = comStore.SelectedValue.ToString();  
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void txtBillNumber_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                dbconnection.Open();
                if (e.KeyCode == Keys.Enter)
                {
                    for (int i = 0; i < dgv2RowsCode.Length; i++)
                    {
                        dgv2RowsCode[i] = "";
                    }

                    dataGridView1.DataSource = null;
                    dataGridView2.DataSource = null;
                    bool flag2 = false;

                    int billNum=0, customerID=0, clientID=0, delegateID=0;
                    if (int.TryParse(txtBillNumber.Text, out BranchBillNum) && int.TryParse(txtBranchID.Text, out branchID) && int.TryParse(txtStoreID.Text,out storeId))
                    {
                        string query = "select * from requests  where requests.BranchBillNumber=" + BranchBillNum + " and Branch_ID="+ branchID+ " and Store_ID="+storeId;
                        MySqlCommand com = new MySqlCommand(query, dbconnection);
                        MySqlDataReader dr = com.ExecuteReader();
                       
                        while (dr.Read())
                        {
                            flag2 = true;
                            billNum= Convert.ToInt16(dr["Request_ID"]);
                            if (dr["Customer_ID"].ToString() != "")
                                customerID = Convert.ToInt16(dr["Customer_ID"]);

                            clientID = Convert.ToInt16(dr["Client_ID"]);
                            delegateID = Convert.ToInt16(dr["Delegate_ID"]);
                            dateTimePicker2.Value = Convert.ToDateTime(dr["Recive_Date"]);
                        }
                        dr.Close();
                        if (flag2 == true)
                        {
                            groupBox1.Visible = true;
                            //extract delgate info
                            if (delegateID > 0)
                            {
                                query = "select * from Delegate where Delegate_ID=" + delegateID;
                                com = new MySqlCommand(query, dbconnection);
                                dr = com.ExecuteReader();
                                while (dr.Read())
                                {
                                    txtDelegateName.Text = dr["Delegate_Name"].ToString();
                                    txtDelegateID.Text = dr["Delegate_ID"].ToString();
                                }
                                dr.Close();
                            }
                            else
                            {
                                dbconnection.Close();
                                flag2 = false;
                                MessageBox.Show("error..Must have delegate");
                                return;
                            }
                            
                            //extract customer info
                            if (clientID > 0)
                            {
                                query = "select * from customer where Customer_ID=" + clientID + "";
                                com = new MySqlCommand(query, dbconnection);
                                dr = com.ExecuteReader();
                                while (dr.Read())
                                {
                                    txtClientName.Text = dr["Customer_Name"].ToString();
                                    txtClientID.Text = dr["Customer_ID"].ToString();
                                    txtPhoneNumber.Text = dr["Customer_Phone"].ToString();
                                    txtAddress.Text = dr["Customer_Address"].ToString();

                                }
                                dr.Close();
                            }
                            else
                            {
                                MessageBox.Show("error..No client");
                                dbconnection.Close();
                                return;
                            }
                            if (customerID > 0)
                            {
                                query = "select * from customer where Customer_ID=" + customerID + "";
                                com = new MySqlCommand(query, dbconnection);
                                dr = com.ExecuteReader();
                                while (dr.Read())
                                {
                                    txtCustomerName.Text = dr["Customer_Name"].ToString();
                                    txtCustomerID.Text = dr["Customer_ID"].ToString();
                                    txtPhoneNumber.Text = dr["Customer_Phone"].ToString();
                                    txtAddress.Text = dr["Customer_Address"].ToString();
                                }
                                dr.Close();
                            }
                            else
                            {
                                txtCustomerName.Visible = false;
                                labCustomer.Visible = false;
                                txtCustomerID.Visible = false;
                            }
                            flag2 = false;
                      
                           // query= "select Storage_Date from storage inner join product_bill on storage.Code=product_bill.Code where product_bill.Dash_Bill_ID=" + billNum + " and storage.Store_ID=" + storeID + " order by Storage_Date limit 1"

                            query = "select distinct request_details.Code as 'كود',request_details.Quantity as ' الكمية',storage.Store_Place as 'مكان التخزين', type.Type_Name as 'النوع', factory.Factory_Name as 'المصنع' ,groupo.Group_Name as 'المجموعة', product.Product_Name as 'المنتج' ,data.Colour as 'اللون', data.Size as 'المقاس', data.Sort as 'الفرز',data.Classification as 'التصنيف', data.Description as 'الوصف'  from request_details inner join data on data.Code=request_details.Code INNER JOIN requests on requests.Request_ID=request_details.Request_ID INNER JOIN type ON type.Type_ID = data.Type_ID INNER JOIN product ON product.Product_ID = data.Product_ID INNER JOIN factory ON data.Factory_ID = factory.Factory_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID left join storage on storage.Code=request_details.Code  where request_details.Request_ID=" + billNum+ " and requests.Store_ID=" + storeId+ " and storage.Storage_Date=(select Storage_Date from storage inner join request_details on storage.Code=request_details.Code where request_details.Request_ID=" + billNum + " and storage.Store_ID=" + storeId + " order by Storage_Date limit 1)";
                            MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                            DataTable dt = new DataTable();
                            da.Fill(dt);
                            dataGridView1.DataSource = dt;

                            /////////////////
                            

                            for (int i = 0; i < recordRestCount; i++)
                            {
                                for (int j = 0; j < dataGridView1.Rows.Count - 1; j++)
                                {
                                    if (addedRecordRestCode[i].Split('*')[0] == dataGridView1.Rows[j].Cells[0].Value.ToString() && addedRecordRestCode[i].Split('*')[1] == BranchBillNum.ToString() && addedRecordRestCode[i].Split('*')[2] == branchID.ToString())
                                    {
                                        dataGridView1.Rows[dataGridView1.Rows[j].Cells[0].RowIndex].DefaultCellStyle.BackColor = Color.Silver;
                                    }
                                }

                            }
                            for (int i = 0; i < recordFinishedCount; i++)
                            {
                                for (int j = 0; j < dataGridView1.Rows.Count-1; j++)
                                {
                                    if (recordFinishedCode[i].Split('*')[0] == dataGridView1.Rows[j].Cells[0].Value.ToString() && recordFinishedCode[i].Split('*')[1] == BranchBillNum.ToString() && recordFinishedCode[i].Split('*')[2] == branchID.ToString())
                                    {
                                       
                                        dataGridView1.Rows[dataGridView1.Rows[j].Cells[0].RowIndex].DefaultCellStyle.BackColor = Color.Gray;
                                    }
                                }
                            }
                            clear();
                            dataGridView2.Rows.Clear();
                        }
                        else
                        {

                            MessageBox.Show("error..this bill not exist");
                        }

                    }
                    else
                    {
                        MessageBox.Show("insert correct value");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                dbconnection.Open();
                row1 = dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex];
                txtCode.Text = row1.Cells[0].Value.ToString();
                string code= row1.Cells[0].Value.ToString();
                double quantity = Convert.ToDouble(row1.Cells[1].Value);
                
                if (int.TryParse(txtStoreID.Text, out storeId) && int.TryParse(txtBranchID.Text, out branchID) && int.TryParse(txtBillNumber.Text, out BranchBillNum))
                {
                    string query = "select Store_Place from storage where Store_ID=" + storeId + " and Code='" + code+"' order by Storage_Date ";
                    MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    comStorePlace.DataSource = dt;
                    comStorePlace.DisplayMember = dt.Columns["Store_Place"].ToString();
                    comStorePlace.Text = "";

                    query = "select sum(Received_Quantity) from received_bill_store where Branch_ID=" + branchID + " and Branch_BillNumber=" + BranchBillNum+" and Code="+code;
                    MySqlCommand com = new MySqlCommand(query,dbconnection);
                    if (com.ExecuteScalar().ToString() != "")
                    {
                        double recivedQuantity = Convert.ToDouble(com.ExecuteScalar());
                        txtRecivedQuantity.Text = (quantity - recivedQuantity).ToString();

                    }
                    else
                    {
                        txtRecivedQuantity.Text = quantity.ToString();
                    }

                    finish = false;

                
                    for (int i = 0; i < recordFinishedCount; i++)
                    {
                        if (recordFinishedCode[i].Split('*')[0] == code && recordFinishedCode[i].Split('*')[1] == BranchBillNum.ToString() && recordFinishedCode[i].Split('*')[2] == branchID.ToString())
                        {
                            finish = true;
                        }   
                    }
                    groupBox3.Visible = true;
                }
                else
                {
                    MessageBox.Show("insert correct value");
                }
                dbconnection.Close();
                
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
                dbconnection.Open();
                bool exist = false;
                for (int i = 0; i < dgv2RowsCode.Length; i++)
                {
                    if (dgv2RowsCode[i] == txtCode.Text)
                        exist = true;
                }
                if (!finish)
                {
                    if (!exist)
                    {
                        if (row1 != null && txtRecivedQuantity.Text != "" && txtCode.Text != "")
                        {
                            dgv2RowsCode[dgv2RowsCount] = txtCode.Text;
                            dgv2RowsCount++;

                            int n = dataGridView2.Rows.Add();
                            dataGridView2.Rows[n].Cells[0].Value = row1.Cells[0].Value.ToString();
                            dataGridView2.Rows[n].Cells[2].Value = row1.Cells[3].Value.ToString();
                            dataGridView2.Rows[n].Cells[3].Value = row1.Cells[4].Value.ToString();
                            dataGridView2.Rows[n].Cells[4].Value = row1.Cells[5].Value.ToString();
                            dataGridView2.Rows[n].Cells[5].Value = row1.Cells[6].Value.ToString();
                            dataGridView2.Rows[n].Cells[6].Value = row1.Cells[7].Value.ToString();
                            dataGridView2.Rows[n].Cells[7].Value = row1.Cells[8].Value.ToString();
                            dataGridView2.Rows[n].Cells[8].Value = row1.Cells[9].Value.ToString();
                            dataGridView2.Rows[n].Cells[9].Value = row1.Cells[10].Value.ToString();
                            dataGridView2.Rows[n].Cells[10].Value = row1.Cells[11].Value.ToString();

                            double recivedQuantity, totalQuantity, totalrecivedQuantity = 0;
                            string query = "select sum(Received_Quantity) from received_bill_store where Branch_ID=" + branchID + " and Branch_BillNumber=" + BranchBillNum + " and Code=" + txtCode.Text + " and Status=1";
                            MySqlCommand com1 = new MySqlCommand(query, dbconnection);
                            if (com1.ExecuteScalar().ToString() != "")
                            {
                                totalrecivedQuantity = Convert.ToDouble(com1.ExecuteScalar());

                            }
                            if (double.TryParse(txtRecivedQuantity.Text, out recivedQuantity) && double.TryParse(row1.Cells[1].Value.ToString(), out totalQuantity))
                            {
                                totalrecivedQuantity += recivedQuantity;
                                if ((totalQuantity - totalrecivedQuantity) == 0)
                                {
                                    dataGridView1.Rows[row1.Cells[0].RowIndex].DefaultCellStyle.BackColor = Color.Gray;
                                    recordFinishedCode[recordFinishedCount] = dataGridView1.SelectedCells[0].Value.ToString() + "*" + BranchBillNum + "*" + branchID;
                                    recordFinishedCount++;

                                    dataGridView2.Rows[n].DefaultCellStyle.BackColor = Color.Gray;
                                    dataGridView2.Rows[n].Cells[1].Value = txtRecivedQuantity.Text;
                                    dataGridView2.Rows[n].Cells[11].Value = "نعم";

                                    clear();
                                    groupBox3.Visible = false;
                                }
                                else if ((totalQuantity - totalrecivedQuantity) == totalQuantity)
                                {

                                    dataGridView2.Rows[n].Cells[1].Value = txtRecivedQuantity.Text;
                                    dataGridView2.Rows[n].Cells[11].Value = "لا";

                                    clear();
                                    groupBox3.Visible = false;
                                }
                                else if (totalQuantity > totalrecivedQuantity)
                                {
                                    dataGridView1.Rows[row1.Cells[0].RowIndex].DefaultCellStyle.BackColor = Color.Silver;
                                    addedRecordRestCode[recordRestCount] = dataGridView1.SelectedCells[0].Value.ToString() + "*" + BranchBillNum + "*" + branchID;
                                    recordRestCount++;

                                    dataGridView2.Rows[n].DefaultCellStyle.BackColor = Color.Silver;
                                    dataGridView2.Rows[n].Cells[1].Value = txtRecivedQuantity.Text;
                                    dataGridView2.Rows[n].Cells[11].Value = "تم تسليم جزء";

                                    clear();
                                    groupBox3.Visible = false;
                                }
                                else
                                {
                                    MessageBox.Show("insert correct value");
                                }
                            }
                            else
                            {
                                MessageBox.Show("insert correct value");
                            }
                        }
                        else
                        {
                            MessageBox.Show("insert correct value");
                        }

                    }
                    else
                    {
                        MessageBox.Show("this record already exist");
                    }
                }
                else
                {
                    MessageBox.Show("هذا البند تم تسليمه بالكامل");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }

     
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
               
                if (dataGridView2.Rows.Count > 1 && txtStoreID.Text != "" && txtBranchID.Text != "" && txtBillNumber.Text != "" && comBranch.Text != "" && comStore.Text != "")
                {
                    dbconnection.Open();
                    foreach (DataGridViewRow row2 in dataGridView2.Rows)
                    {
                        string query = "insert into received_bill_store (Code,Date,Received_Quantity,Branch_Name,Branch_ID,Branch_BillNumber,Store_ID,Store_Name,Status,finish) values (@Code,@Date,@Received_Quantity,@Branch_Name,@Branch_ID,@Branch_BillNumber,@Store_ID,@Store_Name,@Status,@finish)";
                        MySqlCommand com = new MySqlCommand(query, dbconnection);
                        com.Parameters.Add("@Code", MySqlDbType.VarChar);
                        com.Parameters["@Code"].Value = row2.Cells[0].Value.ToString();
                        com.Parameters.Add("@Date", MySqlDbType.Date);
                        com.Parameters["@Date"].Value = dateTimePicker1.Value.Date;
                        com.Parameters.Add("@Received_Quantity", MySqlDbType.Decimal);
                        com.Parameters["@Received_Quantity"].Value = Convert.ToDecimal(row2.Cells[1].Value);
                        com.Parameters.Add("@Branch_Name", MySqlDbType.VarChar);
                        com.Parameters["@Branch_Name"].Value = comBranch.Text;
                        com.Parameters.Add("@Branch_ID", MySqlDbType.Int16);
                        com.Parameters["@Branch_ID"].Value = Convert.ToInt16(txtBranchID.Text);
                        com.Parameters.Add("@Branch_BillNumber", MySqlDbType.Int16);
                        com.Parameters["@Branch_BillNumber"].Value = Convert.ToInt16(txtBillNumber.Text);
                        com.Parameters.Add("@Store_ID", MySqlDbType.Int16);
                        com.Parameters["@Store_ID"].Value = Convert.ToInt16(txtStoreID.Text);
                        com.Parameters.Add("@Store_Name", MySqlDbType.VarChar);
                        com.Parameters["@Store_Name"].Value = comStore.Text;
                        com.Parameters.Add("@Status", MySqlDbType.Int16);
                        com.Parameters["@Status"].Value = 1;
                        com.Parameters.Add("@finish", MySqlDbType.VarChar);
                        com.Parameters["@finish"].Value = row2.Cells[11].Value.ToString();
                        com.ExecuteNonQuery();

                        clear();
                        dataGridView2.Rows.Clear();

                    }
                }
                else
                {
                    MessageBox.Show("insert correct value");
                }  
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }
        public void clear()
        {
            txtCode.Text = txtRecivedQuantity.Text = "";
            comStorePlace.DataSource = null;
        }
    }

}

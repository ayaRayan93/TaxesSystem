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
    public partial class StoreReturnBill : Form
    {
        MySqlConnection dbconnection, connectionReader, connectionReader2;
        bool loaded = false;
        int Billid = 0;
        string Customer_Type = "";

        public StoreReturnBill()
        {
            try
            {
                InitializeComponent();
                dbconnection = new MySqlConnection(connection.connectionString);
                connectionReader = new MySqlConnection(connection.connectionString);
                connectionReader2 = new MySqlConnection(connection.connectionString);
                //panBillNumber.Visible = false;
                //groupBox1.Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        private void StoreReturnBill_Load(object sender, EventArgs e)
        {
            try
            {
                DataHelper dh = new DataHelper(DSparametr.doubleDS);
                gridControl2.DataSource = dh.DataSet;
                gridControl2.DataMember = dh.DataMember;
                gridView2.InitNewRow += GridView1_InitNewRow;
                dbconnection.Open();
                string query = "select * from type";
                MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                DataTable dt = new DataTable();
                da.Fill(dt);
                comType.DataSource = dt;
                comType.DisplayMember = dt.Columns["Type_Name"].ToString();
                comType.ValueMember = dt.Columns["Type_ID"].ToString();
                comType.Text = "";
                txtType.Text = "";

                query = "select * from factory";
                da = new MySqlDataAdapter(query, dbconnection);
                dt = new DataTable();
                da.Fill(dt);
                comFactory.DataSource = dt;
                comFactory.DisplayMember = dt.Columns["Factory_Name"].ToString();
                comFactory.ValueMember = dt.Columns["Factory_ID"].ToString();
                comFactory.Text = "";
                txtFactory.Text = "";

                query = "select * from groupo";
                da = new MySqlDataAdapter(query, dbconnection);
                dt = new DataTable();
                da.Fill(dt);
                comGroup.DataSource = dt;
                comGroup.DisplayMember = dt.Columns["Group_Name"].ToString();
                comGroup.ValueMember = dt.Columns["Group_ID"].ToString();
                comGroup.Text = "";
                txtGroup.Text = "";

                query = "select * from product";
                da = new MySqlDataAdapter(query, dbconnection);
                dt = new DataTable();
                da.Fill(dt);
                comProduct.DataSource = dt;
                comProduct.DisplayMember = dt.Columns["Product_Name"].ToString();
                comProduct.ValueMember = dt.Columns["Product_ID"].ToString();
                comProduct.Text = "";
                txtProduct.Text = "";

                query = "select * from branch";
                da = new MySqlDataAdapter(query, dbconnection);
                dt = new DataTable();
                da.Fill(dt);
             
                comBranch1.DataSource = dt;
                comBranch1.DisplayMember = dt.Columns["Branch_Name"].ToString();
                comBranch1.ValueMember = dt.Columns["Branch_ID"].ToString();
                comBranch1.Text = "";
                txtBranch1.Text = "";

                query = "select * from store";
                da = new MySqlDataAdapter(query, dbconnection);
                dt = new DataTable();
                da.Fill(dt);
                comStore.DataSource = dt;
                comStore.DisplayMember = dt.Columns["Store_Name"].ToString();
                comStore.ValueMember = dt.Columns["Store_ID"].ToString();
                comStore.Text = "";
                txtStore.Text = "";

                loaded = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            dbconnection.Close();
        }
        private void GridView1_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            GridView view = sender as GridView;
            view.SetRowCellValue(e.RowHandle, gridView2.Columns[0], 0);
            view.SetRowCellValue(e.RowHandle, gridView2.Columns[1], "0");
            view.SetRowCellValue(e.RowHandle, gridView2.Columns[2], "0");
            view.SetRowCellValue(e.RowHandle, gridView2.Columns[3], "0");
            view.SetRowCellValue(e.RowHandle, gridView2.Columns[4], "0");
            view.SetRowCellValue(e.RowHandle, gridView2.Columns[5], "0");
            view.SetRowCellValue(e.RowHandle, gridView2.Columns[6], "0");
            view.SetRowCellValue(e.RowHandle, gridView2.Columns[7], "0");
        }
        private void radioButtonReturnBill_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                panBillNumber.Visible = true;
                groupBox1.Visible = false;
                panCustomer.Visible = false;
                gridControl1.DataSource = null;
                gridView1.Columns.Clear();
                clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void radioButtonWithOutReturnBill_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                panBillNumber.Visible = false;
                groupBox1.Visible = true;
                panCustomer.Visible = true;
                comType.Text = "";
                comFactory.Text = "";
                comGroup.Text = "";
                comProduct.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void comType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                dbconnection.Close();
                dbconnection.Open();
                if (loaded)
                {
                    loaded = false;
                    txtType.Text = comType.SelectedValue.ToString();
                    comFactory.Focus();

                    filterFactory();
                    dbconnection.Close();
                    dbconnection.Open();
                    filterGroup();

                    if (txtType.Text != "1" && txtType.Text != "2" && txtType.Text != "9")
                    {
                        string query = "select * from product";
                        MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        comProduct.DataSource = dt;
                        comProduct.DisplayMember = dt.Columns["Product_Name"].ToString();
                        comProduct.ValueMember = dt.Columns["Product_ID"].ToString();
                        comProduct.Text = "";
                        txtProduct.Text = "";
                        label1.Text = "الصنف";
                        filterProduct();
                    }
                    else
                    {
                        string query = "select * from size";
                        MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        comProduct.DataSource = dt;
                        comProduct.DisplayMember = dt.Columns["Size_Value"].ToString();
                        comProduct.ValueMember = dt.Columns["Size_ID"].ToString();
                        comProduct.Text = "";
                        txtProduct.Text = "";
                        label1.Text = "المقاس";
                        filterProduct();
                    }
                    loaded = true;
                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }
        private void comGroup_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (loaded)
                {
                    loaded = false;
                    dbconnection.Close();
                    dbconnection.Open();
                    txtGroup.Text = comGroup.SelectedValue.ToString();
                    comProduct.Focus();
                    filterProduct();
                    loaded = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }
        private void comFactory_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (loaded)
                {
                    loaded = false;
                    dbconnection.Close();
                    dbconnection.Open();
                    txtFactory.Text = comFactory.SelectedValue.ToString();
                    comGroup.Focus();
                    filterGroup();
                    filterProduct();
                    loaded = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }
        private void comProduct_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (loaded)
                {
                    loaded = false;
                    dbconnection.Close();
                    dbconnection.Open();
                    txtProduct.Text = comProduct.SelectedValue.ToString();
                    comType.Focus();
                    loaded = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
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
                            case "txtType":
                                query = "select Type_Name from type where Type_ID='" + txtType.Text + "'";
                                com = new MySqlCommand(query, dbconnection);
                                if (com.ExecuteScalar() != null)
                                {
                                    Name = (string)com.ExecuteScalar();
                                    comType.Text = Name;
                                    txtFactory.Focus();
                                    dbconnection.Close();
                                }
                                else
                                {
                                    MessageBox.Show("there is no item with this id");
                                    dbconnection.Close();
                                    return;
                                }
                                break;
                            case "txtFactory":
                                query = "select Factory_Name from factory where Factory_ID='" + txtFactory.Text + "'";
                                com = new MySqlCommand(query, dbconnection);
                                if (com.ExecuteScalar() != null)
                                {
                                    Name = (string)com.ExecuteScalar();
                                    comFactory.Text = Name;
                                    txtGroup.Focus();
                                    dbconnection.Close();
                                    displayData();
                                }
                                else
                                {
                                    MessageBox.Show("there is no item with this id");
                                    dbconnection.Close();
                                    return;
                                }
                                break;
                            case "txtGroup":
                                query = "select Group_Name from groupo where Group_ID='" + txtGroup.Text + "'";
                                com = new MySqlCommand(query, dbconnection);
                                if (com.ExecuteScalar() != null)
                                {
                                    Name = (string)com.ExecuteScalar();
                                    comGroup.Text = Name;
                                    txtProduct.Focus();
                                    dbconnection.Close();
                                    displayData();
                                }
                                else
                                {
                                    MessageBox.Show("there is no item with this id");
                                    dbconnection.Close();
                                    return;
                                }
                                break;
                            case "txtProduct":
                                query = "select Product_Name from product where Product_ID='" + txtProduct.Text + "'";
                                com = new MySqlCommand(query, dbconnection);
                                if (com.ExecuteScalar() != null)
                                {
                                    Name = (string)com.ExecuteScalar();
                                    comProduct.Text = Name;
                                    txtType.Focus();
                                    dbconnection.Close();
                                    displayData();
                                }
                                else
                                {
                                    MessageBox.Show("there is no item with this id");
                                    dbconnection.Close();
                                    return;
                                }
                                break;
                        
                            case "txtBranch1":
                                query = "select Branch_Name from branch where Branch_ID='" + txtBranch1.Text + "'";
                                com = new MySqlCommand(query, dbconnection);
                                if (com.ExecuteScalar() != null)
                                {
                                    Name = (string)com.ExecuteScalar();
                                    comBranch1.Text = Name;
                                }
                                else
                                {
                                    MessageBox.Show("there is no item with this id");
                                    dbconnection.Close();
                                    return;
                                }
                                break;
                            case "txtStore":
                                query = "select Store_Name from store where Store_ID='" + txtStore.Text + "'";
                                com = new MySqlCommand(query, dbconnection);
                                if (com.ExecuteScalar() != null)
                                {
                                    Name = (string)com.ExecuteScalar();
                                    comStore.Text = Name;
                                }
                                else
                                {
                                    MessageBox.Show("there is no item with this id");
                                    dbconnection.Close();
                                    return;
                                }
                                break;
                            case "txtCustomerID":
                                query = "select Customer_Name from customer where Customer_ID=" + txtCustomerID.Text + "";
                                com = new MySqlCommand(query, dbconnection);
                                if (com.ExecuteScalar() != null)
                                {
                                    Name = (string)com.ExecuteScalar();
                                    comEngCon.Text = Name;
                                    comEngCon.SelectedValue = txtCustomerID.Text;

                                }
                                else
                                {
                                    MessageBox.Show("there is no item with this id");
                                    dbconnection.Close();
                                    return;
                                }
                                break;
                            case "txtClientID":
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

                        }

                    }

                }
                catch
                {
                    // MessageBox.Show(ex.ToString());
                }
                dbconnection.Close();
            }
        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                displayData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btnNewChooes_Click(object sender, EventArgs e)
        {
            try
            {
                comType.Text = "";
                comFactory.Text = "";
                comGroup.Text = "";
                comProduct.Text = "";

                txtType.Text = "";
                txtFactory.Text = "";
                txtGroup.Text = "";
                txtProduct.Text = "";

                displayData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void comBranch1_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (loaded)
                {
                    txtBranch1.Text = comBranch1.SelectedValue.ToString();
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
                    txtStore.Text = comStore.SelectedValue.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void txtBranchBillNum_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                dbconnection.Open();
                if (e.KeyCode == Keys.Enter)
                {
                    displayBill();
                   // displayData1();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }
        DataRow row;
        int rowHandel1;
        private void gridView1_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            try
            {
                row = gridView1.GetDataRow(gridView1.GetRowHandle(e.RowHandle));
                rowHandel1 = e.RowHandle;

                txtCode.Text = row["الكود"].ToString();
                txtCarton.Text = row["الكرتنة"].ToString();
                txtReturnedQuantity.Text = (Convert.ToDouble(row["الكمية"].ToString()) - Convert.ToDouble(row["الكمية المرتجعة"].ToString())).ToString();
             
                if(Convert.ToDouble(row["الكرتنة"].ToString())!=0)
                {
                    txtNumOfCarton.Text = (Convert.ToDouble(row["الكمية"].ToString()) / Convert.ToDouble(row["الكرتنة"].ToString())).ToString("0.00");
                }
               
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void gridView1_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            try
            {
                GridView View = sender as GridView;
                if (e.RowHandle >= 0)
                {
                    double x1 = Convert.ToDouble(View.GetRowCellDisplayText(e.RowHandle, View.Columns[4]));
                    double x2 = Convert.ToDouble(View.GetRowCellDisplayText(e.RowHandle, View.Columns[5]));

                    if (x1 > x2)
                    {
                        e.Appearance.BackColor = Color.Salmon;
                        e.Appearance.BackColor2 = Color.SeaShell;
                        e.HighPriority = true;
                    }
                }
            }
            catch
            {
            }
        }
        private void btnPut_Click(object sender, EventArgs e)
        {
            try
            {
                addNewRow();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btnRemove_Click(object sender, EventArgs e)
        {
            try
            {
                int rowHandle = gridView2.GetRowHandle(gridView2.GetSelectedRows()[0]);
                gridView2.DeleteRow(rowHandle);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //CustomerReturnItems_Report form=null;
        private void btnCreateReturnBill_Click(object sender, EventArgs e)
        {
            try
            {
                dbconnection.Open();
                List<ReturnPermissionClass> listOfData = new List<ReturnPermissionClass>();
                if (gridView2.RowCount > 0 && txtStore.Text != "" && txtBranch1.Text != ""&& customerNameSelected())
                {
                    string query = "insert into customer_return_permission (CustomerBill_ID,Customer_ID,Client_ID,ClientReturnName,ClientRetunPhone,Date,Branch_ID,Branch_Name) values (@CustomerBill_ID,@Customer_ID,@Client_ID,@ClientReturnName,@ClientRetunPhone,@Date,@Branch_ID,@Branch_Name)";
                    MySqlCommand com = new MySqlCommand(query, dbconnection);
                
                    com.Parameters.Add("@CustomerBill_ID", MySqlDbType.Int16);
                    com.Parameters["@CustomerBill_ID"].Value = Billid;
                   
                    if (txtCustomerID.Text != "" && txtCustomerID.Visible == true)
                    {
                        com.Parameters.Add("@Customer_ID", MySqlDbType.Int16);
                        com.Parameters["@Customer_ID"].Value = Convert.ToInt32(txtCustomerID.Text);
                    }
                    else
                    {
                        com.Parameters.Add("@Customer_ID", MySqlDbType.Int16);
                        com.Parameters["@Customer_ID"].Value = null;
                    }
                    if (txtClientID.Text != "" && txtClientID.Visible == true)
                    {
                        com.Parameters.Add("@Client_ID", MySqlDbType.Int16);
                        com.Parameters["@Client_ID"].Value = Convert.ToInt32(txtClientID.Text);
                    }
                    else
                    {
                        com.Parameters.Add("@Client_ID", MySqlDbType.Int16);
                        com.Parameters["@Client_ID"].Value = null;
                    }
                  
                    com.Parameters.Add("@ClientReturnName", MySqlDbType.VarChar);
                    com.Parameters["@ClientReturnName"].Value = txtClientName.Text;
                    com.Parameters.Add("@ClientRetunPhone", MySqlDbType.VarChar);
                    com.Parameters["@ClientRetunPhone"].Value = txtPhone.Text;
                    com.Parameters.Add("@Date", MySqlDbType.DateTime);
                    com.Parameters["@Date"].Value = dateTimePicker1.Value;
                    com.Parameters.Add("@Branch_ID", MySqlDbType.Int16);
                    com.Parameters["@Branch_ID"].Value = Convert.ToInt32(txtBranch1.Text);
                    com.Parameters.Add("@Branch_Name", MySqlDbType.VarChar);
                    com.Parameters["@Branch_Name"].Value = comBranch1.Text;
                    com.ExecuteNonQuery();

                    query = "select CustomerReturnPermission_ID from customer_return_permission order by CustomerReturnPermission_ID desc limit 1";
                    com = new MySqlCommand(query, dbconnection);
                    int CustomerReturnPermission_ID = Convert.ToInt32(com.ExecuteScalar());

                    for (int i = 0; i < gridView2.RowCount; i++)
                    {
                        DataRow row1 = gridView2.GetDataRow(gridView2.GetRowHandle(i));
                        query = "insert into customer_return_permission_details (CustomerReturnPermission_ID,Data_ID,Carton,NumOfCarton,TotalQuantity,ReturnItemReason,Store_ID,TypeItem) values (@CustomerReturnPermission_ID,@Data_ID,@Carton,@NumOfCarton,@TotalQuantity,@ReturnItemReason,@Store_ID,@TypeItem)";
                        com = new MySqlCommand(query, dbconnection);

                        com.Parameters.Add("@CustomerReturnPermission_ID", MySqlDbType.Int16);
                        com.Parameters["@CustomerReturnPermission_ID"].Value = CustomerReturnPermission_ID;
                   
                        com.Parameters.Add("@Data_ID", MySqlDbType.Int16);
                        com.Parameters["@Data_ID"].Value = row1[0].ToString();
                        //if (row1[1].ToString() == "عرض")
                        //{
                        //    com.Parameters.Add("@Carton", MySqlDbType.Decimal);
                        //    com.Parameters["@Carton"].Value = 0;
                        //    com.Parameters.Add("@NumOfCarton", MySqlDbType.Decimal);
                        //    com.Parameters["@NumOfCarton"].Value =0;
                        //}
                        com.Parameters.Add("@Carton", MySqlDbType.Decimal);
                        com.Parameters["@Carton"].Value = row1[5].ToString();
                        com.Parameters.Add("@NumOfCarton", MySqlDbType.Decimal);
                        com.Parameters["@NumOfCarton"].Value = row1[6].ToString();

                        com.Parameters.Add("@TotalQuantity", MySqlDbType.Decimal);
                        com.Parameters["@TotalQuantity"].Value = row1[4].ToString();
                        com.Parameters.Add("@ReturnItemReason", MySqlDbType.VarChar);
                        com.Parameters["@ReturnItemReason"].Value = row1[7].ToString();
                        com.Parameters.Add("@Store_ID", MySqlDbType.Int16);
                        com.Parameters["@Store_ID"].Value = Convert.ToInt32(txtStore.Text);
                        com.Parameters.Add("@TypeItem", MySqlDbType.VarChar);
                        com.Parameters["@TypeItem"].Value ="بند";
                        com.ExecuteNonQuery();

                        ReturnPermissionClass returnPermissionClass = new ReturnPermissionClass();
                        returnPermissionClass.Data_ID = (int)row1["Data_ID"];
                        returnPermissionClass.Code = row1[1].ToString();
                        returnPermissionClass.ItemName = row1[2].ToString();
                        returnPermissionClass.NumOfCarton = Convert.ToDouble(row1[6]);
                        returnPermissionClass.Carton = Convert.ToDouble(row1[5]);
                        returnPermissionClass.ReturnQuantity = Convert.ToDouble(row1[4]);
                        returnPermissionClass.ReturnItemReason = row1[7].ToString();
                        listOfData.Add(returnPermissionClass);

                    }
                    //if (radioButtonWithOutReturnBill.Checked)
                    //{
                    //    IncreaseProductQuantity(CustomerReturnPermission_ID);
                    //}
                    ReturnPermissionReportViewer ReturnCustomerPermissionReport = new ReturnPermissionReportViewer(listOfData, CustomerReturnPermission_ID.ToString(), txtClientName.Text, txtPhone.Text, dateTimePicker1.Text, txtReturnReason.Text);
                    ReturnCustomerPermissionReport.Show();
                    clear();
                }
                else
                {
                    MessageBox.Show("ادخل الفرع والمخزنوالعميل");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }
        //check type of customer if engineer,client or contract 
        private void radiotype_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton radio = (RadioButton)sender;
            Customer_Type = radio.Text;
            comClient.Text = "";
            txtClientID.Text = "";
            comEngCon.Text = "";
            txtCustomerID.Text = "";

            loaded = false; //this is flag to prevent action of SelectedValueChanged event until datasource fill combobox
            try
            {
                if (Customer_Type == "عميل")
                {
                    labelEng.Visible = false;
                    comEngCon.Visible = false;
                    txtCustomerID.Visible = false;
                    labelClient.Visible = true;
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
                    labelEng.Visible = true;
                    comEngCon.Visible = true;
                    txtCustomerID.Visible = true;
                    labelClient.Visible = false;
                    comClient.Visible = false;
                    txtClientID.Visible = false;

                    string query = "select * from customer where Customer_Type='" + Customer_Type + "'";
                    MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    comEngCon.DataSource = dt;
                    comEngCon.DisplayMember = dt.Columns["Customer_Name"].ToString();
                    comEngCon.ValueMember = dt.Columns["Customer_ID"].ToString();
                    comEngCon.Text = "";
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
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        //when select customer(مهندس,مقاول)display in comCustomer the all clients of th customer 
        private void comEngCon_SelectedValueChanged(object sender, EventArgs e)
        {
            if (loaded)
            {
                try
                {
                    loaded = false;
                    txtCustomerID.Text = comEngCon.SelectedValue.ToString();
                    labelClient.Visible = true;
                    comClient.Visible = true;
                    txtClientID.Visible = true;

                    string query = "select * from customer where Customer_ID in(select Client_ID from custmer_client where Customer_ID=" + comEngCon.SelectedValue + ")";
                    MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    comClient.DataSource = dt;
                    comClient.DisplayMember = dt.Columns["Customer_Name"].ToString();
                    comClient.ValueMember = dt.Columns["Customer_ID"].ToString();
                    comClient.Text = "";
                    loaded = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        //functions
        public void displayData()
        {
            string q1, q2, q3, q4,q5;
            if (txtType.Text == "")
            {
                q1 = "select Type_ID from data";
            }
            else
            {
                q1 = txtType.Text;
            }
            if (txtFactory.Text == "")
            {
                q2 = "select Factory_ID from data";
            }
            else
            {
                q2 = txtFactory.Text;
            }
            if (txtProduct.Text == "")
            {
                q3 = "select Product_ID from data";
            }
            else
            {
                q3 = txtProduct.Text;
            }
            if (txtGroup.Text == "")
            {
                q4 = "select Group_ID from data";
            }
            else
            {
                q4 = txtGroup.Text;
            }
            if (txtProduct.Text == "")
            {
                q5 = "";
            }
            else
            {
                q5 = "and  data.Size_ID=" + txtProduct.Text;
            }
            //string query = "SELECT data.Data_ID, data.Code as 'الكود',product.Product_Name as 'الصنف',type.Type_Name as 'النوع',factory.Factory_Name as 'المصنع',groupo.Group_Name as 'المجموعة',color.Color_Name as 'اللون',size.Size_Value as 'المقاس',sort.Sort_Value as 'الفرز',data.Classification as 'التصنيف',data.Description as 'الوصف',data.Carton as 'الكرتنة' from data INNER JOIN type ON type.Type_ID = data.Type_ID INNER JOIN product ON product.Product_ID = data.Product_ID INNER JOIN factory ON data.Factory_ID = factory.Factory_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID LEFT outer JOIN color ON data.Color_ID = color.Color_ID LEFT outer  JOIN size ON data.Size_ID = size.Size_ID LEFT outer  JOIN sort ON data.Sort_ID = sort.Sort_ID where  data.Type_ID IN(" + q1 + ") and  data.Factory_ID  IN(" + q2 + ") and  data.Product_ID  IN(" + q3 + ") and data.Group_ID IN (" + q4 + ") ";
            //string itemName = "concat( product.Product_Name,' ',type.Type_Name,' ',factory.Factory_Name,' ',groupo.Group_Name,' ' ,COALESCE(color.Color_Name,''),' ',COALESCE(size.Size_Value,''),' ',COALESCE(sort.Sort_Value,''),' ',COALESCE(data.Classification,''),' ',COALESCE(data.Description,''))as 'البند'";
            //string DataTableRelations = "INNER JOIN type ON type.Type_ID = data.Type_ID INNER JOIN product ON product.Product_ID = data.Product_ID INNER JOIN factory ON data.Factory_ID = factory.Factory_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID LEFT outer JOIN color ON data.Color_ID = color.Color_ID LEFT outer  JOIN size ON data.Size_ID = size.Size_ID LEFT outer  JOIN sort ON data.Sort_ID = sort.Sort_ID";
            //query = "select data.Data_ID, Code as 'الكود'," + itemName + ",data.Carton as 'الكرتنة' from  data " + DataTableRelations ;
            string itemName = "concat( product.Product_Name,' ',type.Type_Name,' ',factory.Factory_Name,' ',groupo.Group_Name,' ' ,COALESCE(color.Color_Name,''),' ',COALESCE(size.Size_Value,''),' ',COALESCE(sort.Sort_Value,''),' ',COALESCE(data.Classification,''),' ',COALESCE(data.Description,''))as 'البند'";
            string query = "";
            if (txtType.Text == "1" || txtType.Text == "2" || txtType.Text == "9")
            {
                query = "SELECT data.Data_ID, data.Code as 'الكود'," + itemName + ",data.Carton as 'الكرتنة' from data INNER JOIN type ON type.Type_ID = data.Type_ID INNER JOIN product ON product.Product_ID = data.Product_ID INNER JOIN factory ON data.Factory_ID = factory.Factory_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID LEFT outer JOIN color ON data.Color_ID = color.Color_ID LEFT outer  JOIN size ON data.Size_ID = size.Size_ID LEFT outer  JOIN sort ON data.Sort_ID = sort.Sort_ID where  data.Type_ID IN(" + q1 + ") and  data.Factory_ID  IN(" + q2 + ") "+q5+" and data.Group_ID IN (" + q4 + ")  order by SUBSTR(data.Code,1,16),color.Color_Name,data.Description,data.Sort_ID";
            }
            else
            {
                query = "SELECT data.Data_ID, data.Code as 'الكود'," + itemName + ",data.Carton as 'الكرتنة' from data INNER JOIN type ON type.Type_ID = data.Type_ID INNER JOIN product ON product.Product_ID = data.Product_ID INNER JOIN factory ON data.Factory_ID = factory.Factory_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID LEFT outer JOIN color ON data.Color_ID = color.Color_ID LEFT outer  JOIN size ON data.Size_ID = size.Size_ID LEFT outer  JOIN sort ON data.Sort_ID = sort.Sort_ID where  data.Type_ID IN(" + q1 + ") and  data.Factory_ID  IN(" + q2 + ") and  data.Product_ID  IN(" + q3 + ") and data.Group_ID IN (" + q4 + ")  order by SUBSTR(data.Code,1,16),color.Color_Name,data.Description,data.Sort_ID";
            }
            MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
            DataTable dt = new DataTable();
            da.Fill(dt);
            gridControl1.DataSource = null;

            gridControl1.DataSource = dt;
            gridView1.Columns[0].Visible = false;
            gridView1.Columns[1].Width = 200;
        }
        public void displayBill()
        {
            string q = "select CustomerBill_ID,ReturnedFlag from customer_bill where Branch_ID=" + txtBranch1.Text+ " and Branch_BillNumber=" +txtBranchBillNum.Text;
            MySqlCommand com = new MySqlCommand(q, dbconnection);
            MySqlDataReader dr = com.ExecuteReader();
            while (dr.Read())
            {
                Billid = Convert.ToInt16(dr[0].ToString());
                if (dr[1].ToString()=="نعم")
                {
                    MessageBox.Show("هذه الفاتورة تم استرجاعها بالكامل");
                    txtBranchBillNum.Text = "";
                    return;
                }
            }
            dr.Close();
            

            string query = "select CustomerBill_ID,customer_bill.Customer_ID,c1.Customer_Name,Client_ID,c2.Customer_Name from customer_bill left join customer as c1 on c1.Customer_ID=customer_bill.Customer_ID left join customer as c2 on c2.Customer_ID=customer_bill.Client_ID where CustomerBill_ID=" + Billid;
            com = new MySqlCommand(query, dbconnection);            
            dr = com.ExecuteReader();
            while (dr.Read())
            {
                Billid = Convert.ToInt16(dr[0].ToString());
                if (dr[1].ToString() != "")
                {
                    //com.Parameters.Add("@Customer_ID", MySqlDbType.Int16);
                    //com.Parameters["@Customer_ID"].Value = Convert.ToInt32(dr[1].ToString());
                    //com.Parameters.Add("@Customer_Name", MySqlDbType.VarChar);
                    //com.Parameters["@Customer_Name"].Value = dr[2].ToString();
                    txtCustomerID.Text = dr[1].ToString();
                    comEngCon.Text = dr[2].ToString();
                }
                else
                {
                    //com.Parameters.Add("@Customer_ID", MySqlDbType.Int16);
                    //com.Parameters["@Customer_ID"].Value = null;
                    //com.Parameters.Add("@Customer_Name", MySqlDbType.VarChar);
                    //com.Parameters["@Customer_Name"].Value = "";
                }
                if (dr[3].ToString() != "")
                {
                    //com.Parameters.Add("@Client_ID", MySqlDbType.Int16);
                    //com.Parameters["@Client_ID"].Value = Convert.ToInt32(dr[3].ToString());
                    //com.Parameters.Add("@Client_Name", MySqlDbType.VarChar);
                    //com.Parameters["@Client_Name"].Value = dr[4].ToString();

                }
                else
                {
                    //com.Parameters.Add("@Client_ID", MySqlDbType.Int16);
                    //com.Parameters["@Client_ID"].Value = null;
                    //com.Parameters.Add("@Client_Name", MySqlDbType.VarChar);
                    //com.Parameters["@Client_Name"].Value = "";
                    txtClientID.Text = dr[3].ToString();
                    comClient.Text = dr[4].ToString();
                }
            }
            dr.Close();

            query = "select * from customer_return_permission where CustomerBill_ID=" + Billid;
            com = new MySqlCommand(query, dbconnection);
            dr = com.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Close();
                DataTable dtAll = new DataTable();
                string itemName = "concat( product.Product_Name,' ',type.Type_Name,' ',factory.Factory_Name,' ',groupo.Group_Name,' ' ,COALESCE(color.Color_Name,''),' ',COALESCE(size.Size_Value,''),' ',COALESCE(sort.Sort_Value,''),' ',COALESCE(data.Classification,''),' ',COALESCE(data.Description,''))as 'الاسم'";
                string DataTableRelations = "INNER JOIN type ON type.Type_ID = data.Type_ID INNER JOIN product ON product.Product_ID = data.Product_ID INNER JOIN factory ON data.Factory_ID = factory.Factory_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID LEFT outer JOIN color ON data.Color_ID = color.Color_ID LEFT outer  JOIN size ON data.Size_ID = size.Size_ID LEFT outer  JOIN sort ON data.Sort_ID = sort.Sort_ID";
                query = "select product_bill.Data_ID, product_bill.Type  as 'الفئة', Code as'الكود'," + itemName + ",Quantity as 'الكمية',sum(TotalQuantity) as 'الكمية المرتجعة',customer_return_permission_details.Carton as 'الكرتنة' from product_bill inner join data on data.Data_ID=product_bill.Data_ID " + DataTableRelations + "  inner join  customer_return_permission_details on customer_return_permission_details.Data_ID=product_bill.Data_ID inner join customer_return_permission on customer_return_permission.CustomerReturnPermission_ID=customer_return_permission_details.CustomerReturnPermission_ID where customer_return_permission.CustomerBill_ID=" + Billid + " group by product_bill.Data_ID";

                MySqlDataAdapter ad = new MySqlDataAdapter(query, dbconnection);
                DataTable dtProduct1 = new DataTable();
                ad.Fill(dtProduct1);

                query = "select product_bill.Data_ID, product_bill.Type  as 'الفئة' ,Code  as'الكود', " + itemName + " ,Quantity as 'الكمية', '" + 0 + " ' as 'الكمية المرتجعة', Carton  as 'الكرتنة' from product_bill inner join data on data.Data_ID=product_bill.Data_ID " + DataTableRelations + " where CustomerBill_ID=" + Billid+ " and product_bill.Data_ID not in (select group_concat(Data_ID) from customer_return_permission inner join customer_return_permission_details on customer_return_permission.CustomerReturnPermission_ID=customer_return_permission_details.CustomerReturnPermission_ID where customer_return_permission.CustomerBill_ID=" + Billid + " )";
                ad = new MySqlDataAdapter(query, dbconnection);
                DataTable dtProduct = new DataTable();
                ad.Fill(dtProduct);

                dtAll = dtProduct.Copy();
                dtAll.Merge(dtProduct1, true, MissingSchemaAction.Ignore);
                gridControl1.DataSource = null;

                gridView1.ClearDocument();
                gridControl1.DataSource = dtAll;
                gridView1.Columns[0].Visible = false;
               // gridView1.Columns[1].Width = 200;
            }
            else
            {
                dr.Close();
                DataTable dtAll = new DataTable();
                string itemName = "concat( product.Product_Name,' ',type.Type_Name,' ',factory.Factory_Name,' ',groupo.Group_Name,' ' ,COALESCE(color.Color_Name,''),' ',COALESCE(size.Size_Value,''),' ',COALESCE(sort.Sort_Value,''),' ',COALESCE(data.Classification,''),' ',COALESCE(data.Description,''))";
                string DataTableRelations = "INNER JOIN type ON type.Type_ID = data.Type_ID INNER JOIN product ON product.Product_ID = data.Product_ID INNER JOIN factory ON data.Factory_ID = factory.Factory_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID LEFT outer JOIN color ON data.Color_ID = color.Color_ID LEFT outer  JOIN size ON data.Size_ID = size.Size_ID LEFT outer  JOIN sort ON data.Sort_ID = sort.Sort_ID";
                query = "select product_bill.Data_ID, product_bill.Type  as 'الفئة' ,Code  as'الكود', " + itemName + "  as 'الاسم' ,Quantity as 'الكمية', '" + 0 + " ' as 'الكمية المرتجعة', Carton  as 'الكرتنة' from product_bill inner join data on data.Data_ID=product_bill.Data_ID " + DataTableRelations + " where CustomerBill_ID=" + Billid;
                MySqlDataAdapter ad = new MySqlDataAdapter(query, dbconnection);
                DataTable dtProduct = new DataTable();
                ad.Fill(dtProduct);
                query = "select sets.Set_ID as 'Data_ID', product_bill.Type  as 'الفئة',concat(sets.Set_ID,' ') as 'الكود',sets.Set_Name as 'الاسم',product_bill.Type as 'الفئة', product_bill.Quantity as 'الكمية','" + 0 + " ' as 'الكمية المرتجعة'," + 0 + " as 'الكرتنة' from product_bill inner join sets on sets.Set_ID=product_bill.Data_ID  where product_bill.CustomerBill_ID=" + Billid + " and product_bill.Type='طقم' and (product_bill.Returned='لا' or product_bill.Returned='جزء')";
                ad = new MySqlDataAdapter(query, dbconnection);
                DataTable dtSet = new DataTable();
                ad.Fill(dtSet);


                query = "select offer.Offer_ID as 'Data_ID', product_bill.Type  as 'الفئة',concat(offer.Offer_ID,' ') as 'الكود',offer.Offer_Name as 'الاسم',product_bill.Type as 'الفئة', product_bill.Quantity as 'الكمية','" + 0 + " ' as 'الكمية المرتجعة'," + 0 + " as 'الكرتنة' from product_bill inner join offer on offer.Offer_ID=product_bill.Data_ID  where product_bill.CustomerBill_ID=" + Billid + " and product_bill.Type='عرض' and (product_bill.Returned='لا' or product_bill.Returned='جزء')";
                ad = new MySqlDataAdapter(query, dbconnection);
                DataTable dtOffer = new DataTable();
                ad.Fill(dtOffer);


                dtAll = dtProduct.Copy();
                dtAll.Merge(dtSet, true, MissingSchemaAction.Ignore);
                dtAll.Merge(dtOffer, true, MissingSchemaAction.Ignore);

           
                //ad.Fill(dtAll);
                gridControl1.DataSource = dtAll;
                gridView1.Columns[0].Visible = false;
            }
        }
        public void displayData1()
        {
            string query = "select CustomerBill_ID,Type_Buy from customer_bill where Branch_BillNumber=" + txtBranchBillNum.Text + " and Branch_ID=" + txtBranch1.Text;

            MySqlCommand com = new MySqlCommand(query, dbconnection);
            MySqlDataReader dr = com.ExecuteReader();
            int id = -1;
            while (dr.Read())
            {
                id = Convert.ToInt32(dr[0]);
              //  TypeBuy = dr[1].ToString();
            }
            dr.Close();
            
            displayCustomerData(id.ToString());
            gridControl1.DataSource = null;
            gridView1.Columns.Clear();
            DataTable dtAll = new DataTable();
            query = "select data.Data_ID,data.Code as 'الكود',concat(type.Type_Name,' ',product.Product_Name,' ',COALESCE(color.Color_Name,''),' ',COALESCE(data.Description,''),' ',groupo.Group_Name,' ',factory.Factory_Name,' ',COALESCE(size.Size_Value,''),' ',COALESCE(sort.Sort_Value,'')) as 'الاسم',product_bill.Type as 'الفئة',data.Carton as 'الكرتنة',product_bill.Quantity as 'الكمية',(case  when data.Carton !=0 then (product_bill.Quantity /data.Carton) end ) as 'عدد الكراتين','" + " " + " ' as 'الكمية المسلمة',data.Description as 'الوصف',product_bill.Returned as 'تم الاسترجاع',Delegate_Name,product_bill.CustomerBill_ID,product_bill.Store_ID from product_bill inner join data on data.Data_ID=product_bill.Data_ID LEFT JOIN color ON color.Color_ID = data.Color_ID LEFT JOIN size ON size.Size_ID = data.Size_ID LEFT JOIN sort ON sort.Sort_ID = data.Sort_ID INNER JOIN type ON type.Type_ID = data.Type_ID INNER JOIN product ON product.Product_ID = data.Product_ID INNER JOIN factory ON data.Factory_ID = factory.Factory_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID inner join delegate on delegate.Delegate_ID=product_bill.Delegate_ID  where CustomerBill_ID=" + id + " and product_bill.Type='بند'  and (product_bill.Returned='لا' or product_bill.Returned='جزء')";
            MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
            DataTable dtProduct = new DataTable();
            da.Fill(dtProduct);

            query = "select sets.Set_ID as 'Data_ID',sets.Set_ID as 'الكود',sets.Set_Name as 'الاسم',product_bill.Type as 'الفئة', product_bill.Quantity as 'الكمية','" + " " + " ' as 'الكمية المسلمة',sets.Description as 'الوصف',product_bill.Returned as 'تم الاسترجاع',Delegate_Name,product_bill.CustomerBill_ID,product_bill.Store_ID from product_bill inner join sets on sets.Set_ID=product_bill.Data_ID inner join delegate on delegate.Delegate_ID=product_bill.Delegate_ID where CustomerBill_ID=" + id + " and product_bill.Type='طقم' and (product_bill.Returned='لا' or product_bill.Returned='جزء')";
            da = new MySqlDataAdapter(query, dbconnection);
            DataTable dtSet = new DataTable();
            da.Fill(dtSet);

            query = "select offer.Offer_ID as 'Data_ID',offer.Offer_ID as 'الكود',offer.Offer_Name as 'الاسم',product_bill.Type as 'الفئة', product_bill.Quantity as 'الكمية','" + " " + " ' as 'الكمية المسلمة',offer.Description as 'الوصف',product_bill.Returned as 'تم الاسترجاع',Delegate_Name,product_bill.CustomerBill_ID,product_bill.Store_ID from product_bill inner join offer on offer.Offer_ID=product_bill.Data_ID inner join delegate on delegate.Delegate_ID=product_bill.Delegate_ID where CustomerBill_ID=" + id + " and product_bill.Type='عرض' and (product_bill.Returned='لا' or product_bill.Returned='جزء')";
            da = new MySqlDataAdapter(query, dbconnection);
            DataTable dtOffer = new DataTable();
            da.Fill(dtOffer);

            dtAll = dtProduct.Copy();
            dtAll.Merge(dtSet, true, MissingSchemaAction.Ignore);
            dtAll.Merge(dtOffer, true, MissingSchemaAction.Ignore);
            gridControl1.DataSource = dtAll;
            gridView1.Columns[0].Visible = false;
            gridView1.Columns["CustomerBill_ID"].Visible = false;
            //gridView1.Columns["الفئة"].Visible = false;
            gridView1.Columns["الوصف"].Visible = false;
            gridView1.Columns["Delegate_Name"].Visible = false;
            gridView1.Columns["Store_ID"].Visible = false;
           // txtDelegate.Text = gridView1.GetDataRow(0)["Delegate_Name"].ToString();
        }
        public void displayCustomerData(string CustomerBill_ID)
        {
            string query = "select c1.Customer_ID,c1.Customer_Name,c2.Customer_ID,c2.Customer_Name,cc1.Phone,cc2.Phone,Bill_Date from customer_bill left join  customer as c1  on c1.Customer_ID=customer_bill.Customer_ID left join customer as c2 on c2.Customer_ID=customer_bill.Client_ID left join customer_phone as cc1 on cc1.Customer_ID=c1.Customer_ID left join customer_phone as cc2 on cc2.Customer_ID=c2.Customer_ID where CustomerBill_ID=" + CustomerBill_ID;
            MySqlCommand com = new MySqlCommand(query, dbconnection);
            MySqlDataReader dr = com.ExecuteReader();
            while (dr.Read())
            {
                txtCustomerID.Text = dr[0].ToString();
             //   txtCustomerName.Text = dr[1].ToString();
                txtClientID.Text = dr[2].ToString();
                txtClientName.Text = dr[3].ToString();
                if (txtCustomerID.Text != "")
                    txtPhone.Text = dr[4].ToString();
                if (txtClientID.Text != "")
                    txtPhone.Text = dr[5].ToString();
                //labDate.Text = dr[6].ToString();
            }
            dr.Close();
        }
        public void addNewRow()
        {
            if (Convert.ToDouble(row["الكمية"].ToString()) > Convert.ToDouble(row["الكمية المرتجعة"].ToString()))
            {
              
                if ((Convert.ToDouble(row["الكمية"].ToString())- Convert.ToDouble(row["الكمية المرتجعة"].ToString())) >= Convert.ToDouble(txtReturnedQuantity.Text))
                {
                    gridView2.AddNewRow();
                    int rowHandle = gridView2.GetRowHandle(gridView2.DataRowCount);
                    if (gridView2.IsNewItemRow(rowHandle) && rowHandel1 != -1)
                    {
                        gridView2.SetRowCellValue(rowHandle, gridView2.Columns[0], row[0]);
                        gridView2.SetRowCellValue(rowHandle, gridView2.Columns[1], row[2]);
                        gridView2.SetRowCellValue(rowHandle, gridView2.Columns[2], row[3]);
                        gridView2.SetRowCellValue(rowHandle, gridView2.Columns[3], row[4]);

                        double re = 0, carton = 0;
                        if (Convert.ToDouble(txtCarton.Text) != 0)
                            re = Convert.ToDouble(txtReturnedQuantity.Text) / Convert.ToDouble(txtCarton.Text);
                        carton = Convert.ToDouble(txtCarton.Text);

                        gridView2.SetRowCellValue(rowHandle, gridView2.Columns[6], re + "");
                        gridView2.SetRowCellValue(rowHandle, gridView2.Columns[5], carton + "");
                        gridView2.SetRowCellValue(rowHandle, gridView2.Columns[4], txtReturnedQuantity.Text);
                        gridView2.SetRowCellValue(rowHandle, gridView2.Columns[7], txtReturnItemReason.Text);
                    }
                }
                else
                {
                    MessageBox.Show("الكمية المرتجعة اكبر من الكمية المباعة!!");
                }
            }
            else {
                MessageBox.Show("هذا البند تم استرجاعه بالكامل");
            }
            //}
            //else
            //{
            //    gridView2.AddNewRow();

            //    int rowHandle = gridView2.GetRowHandle(gridView2.DataRowCount);
            //    if (gridView2.IsNewItemRow(rowHandle) && rowHandel1 != -1)
            //    {
            //        gridView2.SetRowCellValue(rowHandle, gridView2.Columns[0], row[0]);
            //        gridView2.SetRowCellValue(rowHandle, gridView2.Columns[1], row[1]);
            //        gridView2.SetRowCellValue(rowHandle, gridView2.Columns[2], row[2]);
            //        gridView2.SetRowCellValue(rowHandle, gridView2.Columns[3], "");

            //        double re = 0, carton = 0;
            //        if (Convert.ToDouble(txtCarton.Text) != 0)
            //            re = Convert.ToDouble(txtReturnedQuantity.Text) / Convert.ToDouble(txtCarton.Text);
            //        carton = Convert.ToDouble(txtCarton.Text);

            //        gridView2.SetRowCellValue(rowHandle, gridView2.Columns[6], re + "");
            //        gridView2.SetRowCellValue(rowHandle, gridView2.Columns[5], carton + "");
            //        gridView2.SetRowCellValue(rowHandle, gridView2.Columns[4], txtReturnedQuantity.Text);
            //        gridView2.SetRowCellValue(rowHandle, gridView2.Columns[7], txtReturnItemReason.Text);
            //    }
            //}
            loaded = true;
        }
        public void clear()
        {
            comType.Text = "";
            comFactory.Text = "";
            comGroup.Text = "";
            comProduct.Text = "";

            txtType.Text = "";
            txtFactory.Text = "";
            txtGroup.Text = "";
            txtProduct.Text = "";

            comClient.Text = "";
            comEngCon.Text = "";
            txtClientID.Text = "";
            txtCustomerID.Text = "";
            foreach (Control item in panel2.Controls)
            {
                if (item is TextBox)
                {
                    item.Text = "";
                }
            }
            foreach (Control item in panel3.Controls)
            {
                if (item is TextBox)
                {
                    item.Text = "";
                }
            }
            dateTimePicker1.Value = DateTime.Now.Date;
            gridControl1.DataSource = null;
            gridControl2.DataSource = null;
            DataHelper dh = new DataHelper(DSparametr.doubleDS);
            gridControl2.DataSource = dh.DataSet;
            gridControl2.DataMember = dh.DataMember;
            gridView2.InitNewRow += GridView1_InitNewRow;
        }
        public void filterFactory()
        {
            if (comType.Text != "")
            {
                string query = "select * from factory inner join type_factory on factory.Factory_ID=type_factory.Factory_ID inner join type on type.Type_ID=type_factory.Type_ID  where type_factory.Type_ID=" + comType.SelectedValue;
                MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                DataTable dt = new DataTable();
                da.Fill(dt);
                comFactory.DataSource = dt;
                comFactory.DisplayMember = dt.Columns["Factory_Name"].ToString();
                comFactory.ValueMember = dt.Columns["Factory_ID"].ToString();
                comFactory.Text = "";
                txtFactory.Text = "";
            }
        }
        public void filterGroup()
        {
            string query = "select TypeCoding_Method from type where Type_ID=" + txtType.Text;
            MySqlCommand com = new MySqlCommand(query, dbconnection);
            int TypeCoding_Method = (int)com.ExecuteScalar();
            if (TypeCoding_Method == 1)
            {
                string query2 = "";
                if (txtType.Text == "2" || txtType.Text == "1")
                    query2 = "select * from groupo where Factory_ID=" + -1;
                else
                    query2 = "select * from groupo where Factory_ID=" + -Convert.ToInt32(txtType.Text) + " and Type_ID=" + txtType.Text;

                MySqlDataAdapter da2 = new MySqlDataAdapter(query2, dbconnection);
                DataTable dt2 = new DataTable();
                da2.Fill(dt2);
                comGroup.DataSource = dt2;
                comGroup.DisplayMember = dt2.Columns["Group_Name"].ToString();
                comGroup.ValueMember = dt2.Columns["Group_ID"].ToString();
                comGroup.Text = "";
                txtGroup.Text = "";
            }
            else
            {
                string q = "";
                if (txtFactory.Text != "")
                {
                    q = " and Factory_ID = " + txtFactory.Text;
                }
                query = "select * from groupo where Type_ID=" + txtType.Text + q;
                MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                DataTable dt = new DataTable();
                da.Fill(dt);
                comGroup.DataSource = dt;
                comGroup.DisplayMember = dt.Columns["Group_Name"].ToString();
                comGroup.ValueMember = dt.Columns["Group_ID"].ToString();
                comGroup.Text = "";
                txtGroup.Text = "";
            }
        }
        public void filterProduct()
        {
            if (comType.Text != "")
            {
                if (comGroup.Text != "" || comFactory.Text != "" || comType.Text != "")
                {
                    if (txtType.Text != "1" && txtType.Text != "2" && txtType.Text != "9")
                    {
                        string supQuery = "";

                        supQuery = " product.Type_ID=" + txtType.Text + "";
                        if (comFactory.Text != "")
                        {
                            supQuery += " and product_factory_group.Factory_ID=" + txtFactory.Text + "";
                        }
                        if (comGroup.Text != "")
                        {
                            supQuery += " and product_factory_group.Group_ID=" + txtGroup.Text + "";
                        }
                        string query = "select distinct  product.Product_ID  ,Product_Name  from product inner join product_factory_group on product.Product_ID=product_factory_group.Product_ID  where  " + supQuery + "   order by product.Product_ID";
                        MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        comProduct.DataSource = dt;
                        comProduct.DisplayMember = dt.Columns["Product_Name"].ToString();
                        comProduct.ValueMember = dt.Columns["Product_ID"].ToString();
                        comProduct.Text = "";
                        txtProduct.Text = "";
                    }
                    else
                    {
                        string supQuery = "";
                        if (comFactory.Text != "")
                        {
                            supQuery += " and type_factory.Factory_ID=" + txtFactory.Text + "";
                        }
                        if (comGroup.Text != "")
                        {
                            supQuery += " and Group_ID=" + txtGroup.Text + "";
                        }
                        string query = "select * from size inner join type_factory on size.Factory_ID=type_factory.Factory_ID where type_factory.Type_ID=" + txtType.Text + supQuery;
                        MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        comProduct.DataSource = dt;
                        comProduct.DisplayMember = dt.Columns["Size_Value"].ToString();
                        comProduct.ValueMember = dt.Columns["Size_ID"].ToString();
                        comProduct.Text = "";
                        txtProduct.Text = "";
                    }
                }
            }

        }
        public void filterSize()
        {
            if (comFactory.Text != "")
            {
                string query = "select * from size  inner join type_factory on size.Factory_ID=type_factory.Factory_ID where Type_ID=" + txtType.Text + " Factory_ID=" + comFactory.SelectedValue;
                MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                DataTable dt = new DataTable();
                da.Fill(dt);
                comProduct.DataSource = dt;
                comProduct.DisplayMember = dt.Columns["Size_Value"].ToString();
                comProduct.ValueMember = dt.Columns["Size_ID"].ToString();
                comProduct.Text = "";
                txtProduct.Text = "";
            }
        }
        public void IncreaseProductQuantity(int billNumber)
        {
            connectionReader.Open();
            connectionReader2.Open();
            string q;
            int id;
            bool flag = false;
            double storageQ, productQ;
           
            string query = "select Data_ID,TotalQuantity from customer_return_permission_details where CustomerReturnPermission_ID=" + billNumber;
            MySqlCommand com = new MySqlCommand(query, connectionReader);
            MySqlDataReader dr = com.ExecuteReader();

            while (dr.Read())
            {
                string query2 = "select Storage_ID,Total_Meters from storage where Data_ID=" + dr["Data_ID"].ToString() + " and Type='بند'";
                MySqlCommand com2 = new MySqlCommand(query2, connectionReader2);
                MySqlDataReader dr2 = com2.ExecuteReader();
                while (dr2.Read())
                {
                    storageQ = Convert.ToDouble(dr2["Total_Meters"]);
                    productQ = Convert.ToDouble(dr["TotalQuantity"]);

                    storageQ += productQ;
                    id = Convert.ToInt32(dr2["Storage_ID"]);
                    q = "update storage set Total_Meters=" + storageQ + " where Storage_ID=" + id;
                    MySqlCommand comm = new MySqlCommand(q, dbconnection);
                    comm.ExecuteNonQuery();
                    flag = true;
                    break;
                }
                dr2.Close();
          
                if (!flag)
                {
                    recordDataInOpenAccountStorage(dr["Data_ID"].ToString() , dr["TotalQuantity"].ToString());
                }
                flag = false;
            }
            dr.Close();

            connectionReader2.Close();
            connectionReader.Close();
        }
        public void recordDataInOpenAccountStorage(string Data_ID,string TotalQuantity)
        {
            string query = "insert into open_storage_account (Data_ID,Quantity,Store_ID,Store_Place_ID,Date) values (@Data_ID,@Quantity,@Store_ID,@Store_Place_ID,@Date)";
            MySqlCommand com = new MySqlCommand(query, dbconnection);
            com.Parameters.Add("@Data_ID", MySqlDbType.Int16);
            com.Parameters["@Data_ID"].Value = Data_ID;
            com.Parameters.Add("@Quantity", MySqlDbType.Decimal);
            com.Parameters["@Quantity"].Value = Convert.ToDecimal(TotalQuantity);
            com.Parameters.Add("@Store_ID", MySqlDbType.Int16);
            com.Parameters["@Store_ID"].Value = 1;
            com.Parameters.Add("@Store_Place_ID", MySqlDbType.Int16);
            com.Parameters["@Store_Place_ID"].Value =8;
            com.Parameters.Add("@Date", MySqlDbType.Date, 0);
            DateTime date = Convert.ToDateTime(dateTimePicker1.Text);
            string d = date.ToString("yyyy-MM-dd");
            com.Parameters["@Date"].Value = d;
            com.ExecuteNonQuery();

            query = "insert into Storage (Store_ID,Type,Storage_Date,Data_ID,Store_Place_ID,Total_Meters) values (@Store_ID,@Type,@Date,@Data_ID,@PlaceOfStore,@TotalOfMeters)";
            com = new MySqlCommand(query, dbconnection);
            com.Parameters.Add("@Store_ID", MySqlDbType.Int16);
            com.Parameters["@Store_ID"].Value =1;
            com.Parameters.Add("@Type", MySqlDbType.VarChar);
            com.Parameters["@Type"].Value = "بند";
            com.Parameters.Add("@Date", MySqlDbType.Date, 0);
            date = Convert.ToDateTime(dateTimePicker1.Text);
            d = date.ToString("yyyy-MM-dd");
            com.Parameters["@Date"].Value = d;
            com.Parameters.Add("@Data_ID", MySqlDbType.Int16);
            com.Parameters["@Data_ID"].Value = Data_ID;
            com.Parameters.Add("@PlaceOfStore", MySqlDbType.Int16);
            com.Parameters["@PlaceOfStore"].Value = 8;
            com.Parameters.Add("@TotalOfMeters", MySqlDbType.Decimal);
            com.Parameters["@TotalOfMeters"].Value = Convert.ToDecimal(TotalQuantity);
            com.ExecuteNonQuery();

            query = "select OpenStorageAccount_ID from open_storage_account order by OpenStorageAccount_ID desc limit 1";
            com = new MySqlCommand(query, dbconnection);
            int id = Convert.ToInt32(com.ExecuteScalar());

            UserControl.ItemRecord("open_storage_account", "اضافة", id, DateTime.Now, "", dbconnection);

            query = "select Storage_ID from Storage order by Storage_ID desc limit 1";
            com = new MySqlCommand(query, dbconnection);
            id = Convert.ToInt32(com.ExecuteScalar());

            UserControl.ItemRecord("Storage", "اضافة", id, DateTime.Now, "", dbconnection);

        }
        public bool customerNameSelected()
        {
            if (radioButtonWithOutReturnBill.Checked)
            {
                if (txtClientID.Text != "" || txtCustomerID.Text != "")
                    return true;
                else
                    return false;
            }
            return true;
        }

    }
}

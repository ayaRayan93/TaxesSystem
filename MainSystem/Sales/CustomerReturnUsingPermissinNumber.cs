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

namespace MainSystem
{
    public partial class CustomerReturnUsingPermissinNumber : Form
    {
        MySqlConnection dbconnection;
        string type = "كاش";
        int Branch_ID = 0;
        double totalBill = 0;
        bool load = false;
        bool load1 = false;//test case datagridview row 
        public CustomerReturnUsingPermissinNumber()
        {
            try
            {
                InitializeComponent();
                dbconnection = new MySqlConnection(connection.connectionString);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        private void CustomerReturnUsingPermissinNumber_Load(object sender, EventArgs e)
        {
            try
            {
                dbconnection.Open();
                string query = "select * from delegate";
                MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                DataTable dt = new DataTable();
                da.Fill(dt);
                comDelegate.DataSource = dt;
                comDelegate.DisplayMember = dt.Columns["Delegate_Name"].ToString();
                comDelegate.ValueMember = dt.Columns["Delegate_ID"].ToString();
                comDelegate.Text = "";
                txtDelegate.Text = "";

                load = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }
        private void txtReturnPermission_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    txtInfo.Text = "";
                    txtBillTotalCostAD.Text = "";
                    labBillNumber.Text = "";
                    labClientName.Text = "";
                    labClientPhone.Text = "";
                    labBillDate.Text = "";
                    dataGridView2.Rows.Clear();
                    totalBill = 0;
                    dbconnection.Open();
                    int billNum = Convert.ToInt16(txtReturnPermission.Text);

                    string query = "";
                    MySqlCommand com;
                    query = "select Store_Permission_Number from customer_return_bill where Store_Permission_Number="+billNum;
                    com = new MySqlCommand(query, dbconnection);
                    if (com.ExecuteScalar() != null)
                    {
                        MessageBox.Show("اذن المرتجع تم تسجيله من قبل");
                        dbconnection.Close();
                        return;
                    }
                
                    MySqlDataReader dr;
                    DataTable dtAll = new DataTable();

                    string relation = " LEFT JOIN color ON color.Color_ID = data.Color_ID LEFT JOIN size ON size.Size_ID = data.Size_ID LEFT JOIN sort ON sort.Sort_ID = data.Sort_ID INNER JOIN type ON type.Type_ID = data.Type_ID INNER JOIN product ON product.Product_ID = data.Product_ID INNER JOIN factory ON data.Factory_ID = factory.Factory_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID";
                    string supQuery = "concat(product.Product_Name,' - ',type.Type_Name,' - ',factory.Factory_Name,' - ',groupo.Group_Name,' ',COALESCE(color.Color_Name,''),' ',COALESCE(size.Size_Value,''),' ',COALESCE(sort.Sort_Value,'')) as 'الاسم'";

                    query = "select IFNULL(Branch_BillNumber,''),IFNULL(customer_bill.Branch_ID,''),IFNULL(branch.Branch_Name,''),ClientReturnName,ClientRetunPhone,Date from customer_return_permission left join customer_bill on customer_return_permission.CustomerBill_ID=customer_bill.CustomerBill_ID left join branch on branch.Branch_ID=customer_bill.Branch_ID where CustomerReturnPermission_ID=" + billNum;
                    com = new MySqlCommand(query, dbconnection);

                    dr = com.ExecuteReader();
                    while (dr.Read())
                    {
                        if (dr[1].ToString() != "")
                        {
                            Branch_ID = Convert.ToInt16(dr[1].ToString());
                        }
                        labBillNumber.Text = dr[0].ToString();
                        labBranchName.Text = dr[2].ToString();
                        labClientName.Text = dr[3].ToString();
                        labClientPhone.Text = dr[4].ToString();
                        labBillDate.Text = dr[5].ToString();
                    }
                    dr.Close();

                    if (labBillNumber.Text != "")
                    {
                        query = "select DISTINCT product_bill.Data_ID,data.Code as 'الكود'," + supQuery + ",customer_return_permission_details.TotalQuantity as 'الكمية',product_bill.Price as 'السعر',product_bill.Discount as 'نسبة الخصم',product_bill.PriceAD as 'السعر بعد الخصم',(customer_return_permission_details.TotalQuantity*product_bill.PriceAD) as 'الاجمالي',product_bill.Delegate_ID,product_bill.CustomerBill_ID ,product_bill.Type from product_bill inner join data on data.Data_ID=product_bill.Data_ID " + relation + " inner join customer_return_permission on customer_return_permission.CustomerBill_ID=product_bill.CustomerBill_ID inner join customer_return_permission_details on customer_return_permission.CustomerReturnPermission_ID=customer_return_permission_details.CustomerReturnPermission_ID  where customer_return_permission_details.CustomerReturnPermission_ID=" + txtReturnPermission.Text + " and product_bill.Type='بند' ";
                    }
                    else
                    {
                        query = "select customer_return_permission_details.Data_ID,data.Code as 'الكود'," + supQuery + ",customer_return_permission_details.TotalQuantity as 'الكمية',sellprice.Last_Price as 'السعر',sellprice.Sell_Discount as 'نسبة الخصم',sellprice.Sell_Price as 'السعر بعد الخصم',(customer_return_permission_details.TotalQuantity*sellprice.Sell_Price) as 'الاجمالي','','' ,'"+ "بند" + "' from customer_return_permission  inner join customer_return_permission_details on customer_return_permission.CustomerReturnPermission_ID=customer_return_permission_details.CustomerReturnPermission_ID inner join data on data.Data_ID=customer_return_permission_details.Data_ID " + relation + " inner join sellprice on sellprice.Data_ID=customer_return_permission_details.Data_ID where customer_return_permission_details.CustomerReturnPermission_ID=" + txtReturnPermission.Text;
                    }

                    com = new MySqlCommand(query, dbconnection);
                    dr = com.ExecuteReader();
                  
                    load1 = false;
                    while (dr.Read())
                    {
                        int n = dataGridView2.Rows.Add();
                        dataGridView2.Rows[n].Cells[0].Value = dr[0].ToString();
                        dataGridView2.Rows[n].Cells[1].Value = dr[1].ToString();
                        dataGridView2.Rows[n].Cells[2].Value = dr[2].ToString();
                        dataGridView2.Rows[n].Cells[3].Value = dr[3].ToString();
                        dataGridView2.Rows[n].Cells[4].Value = dr[4].ToString();
                        dataGridView2.Rows[n].Cells[5].Value = dr[5].ToString();
                        dataGridView2.Rows[n].Cells[6].Value = dr[6].ToString();
                        dataGridView2.Rows[n].Cells[7].Value = dr[7].ToString();
                        dataGridView2.Rows[n].Cells[8].Value = dr[8].ToString();
                        dataGridView2.Rows[n].Cells[9].Value = dr[9].ToString();
                        dataGridView2.Rows[n].Cells["type1"].Value = dr[10].ToString();
                        totalBill += Convert.ToDouble(dr[7].ToString());
                    }
                    dr.Close();
                    txtBillTotalCostAD.Text = totalBill.ToString();
                    load1 = true;

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }
        private void btnCreateReturnBill_Click(object sender, EventArgs e)
        {
            try
            {
                dbconnection.Close();
                dbconnection.Open();
                if (dataGridView2.RowCount > 0 && txtReturnPermission.Text != "")
                {
                    string query = "select Branch_BillNumber from customer_return_bill where Branch_ID=" +BaseData.BranchID + " order by CustomerReturnBill_ID desc limit 1";
                    MySqlCommand com = new MySqlCommand(query, dbconnection);
                    int Branch_BillNumber = 1;
                    if (com.ExecuteScalar() != null)
                    {
                        Branch_BillNumber = Convert.ToInt16(com.ExecuteScalar()) + 1;
                    }
                    query = "insert into customer_return_bill (Customer_ID,Client_ID,Branch_ID,Branch_BillNumber,Store_Permission_Number,Date,TotalCostAD,ReturnInfo,Type_Buy,Employee_ID,Employee_Name) values (@Customer_ID,@Client_ID,@Branch_ID,@Branch_BillNumber,@Store_Permission_Number,@Date,@TotalCostAD,@ReturnInfo,@Type_Buy,@Employee_ID,@Employee_Name)";
                    com = new MySqlCommand(query, dbconnection);
                    
                    com.Parameters.Add("@Branch_BillNumber", MySqlDbType.Int16);
                    com.Parameters["@Branch_BillNumber"].Value = Branch_BillNumber;
                    com.Parameters.Add("@Branch_ID", MySqlDbType.Int16);
                    com.Parameters["@Branch_ID"].Value = BaseData.BranchID;
                    int storeNum = 0;
                    if (int.TryParse(txtReturnPermission.Text, out storeNum))
                    {
                        com.Parameters.Add("@Store_Permission_Number", MySqlDbType.Int16);
                        com.Parameters["@Store_Permission_Number"].Value = storeNum;
                    }
                    else
                    {
                        MessageBox.Show("اذن المخزن يجب ان يكون عدد");
                        dbconnection.Close();
                        return;
                    }

                    string q = "select Customer_ID,Client_ID from customer_return_permission where CustomerReturnPermission_ID=" + storeNum;
                    MySqlCommand c = new MySqlCommand(q, dbconnection);
                    MySqlDataReader dr = c.ExecuteReader();
                    while (dr.Read())
                    {
                        if (dr[0].ToString() != "")
                        {
                            com.Parameters.Add("@Customer_ID", MySqlDbType.Int16);
                            com.Parameters["@Customer_ID"].Value = Convert.ToInt16(dr[0].ToString());
                        }
                        else
                        {
                            com.Parameters.Add("@Customer_ID", MySqlDbType.Int16);
                            com.Parameters["@Customer_ID"].Value = null;
                        }
                        if (dr[1].ToString() != "")
                        {
                            com.Parameters.Add("@Client_ID", MySqlDbType.Int16);
                            com.Parameters["@Client_ID"].Value = Convert.ToInt16(dr[1].ToString());
                        }
                        else
                        {
                            com.Parameters.Add("@Client_ID", MySqlDbType.Int16);
                            com.Parameters["@Client_ID"].Value = null;
                        }
                    }
                    dr.Close();

                    com.Parameters.Add("@Date", MySqlDbType.DateTime);
                    com.Parameters["@Date"].Value = DateTime.Now;
                    com.Parameters.Add("@ReturnInfo", MySqlDbType.VarChar);
                    com.Parameters["@ReturnInfo"].Value = txtInfo.Text;
                    com.Parameters.Add("@TotalCostAD", MySqlDbType.Decimal);
                    com.Parameters["@TotalCostAD"].Value = Convert.ToDouble(txtBillTotalCostAD.Text);
                    com.Parameters.Add("@Type_Buy", MySqlDbType.VarChar);
                    com.Parameters["@Type_Buy"].Value = type;
                    com.Parameters.Add("@Employee_ID", MySqlDbType.Int16);
                    com.Parameters["@Employee_ID"].Value = UserControl.EmpID;
                    com.Parameters.Add("@Employee_Name", MySqlDbType.VarChar);
                    com.Parameters["@Employee_Name"].Value = UserControl.EmpName;
                    com.ExecuteNonQuery();

                    query = "select CustomerReturnBill_ID from customer_return_bill order by CustomerReturnBill_ID desc limit 1";
                    com = new MySqlCommand(query, dbconnection);
                    int id = Convert.ToInt16(com.ExecuteScalar());

                    query = "insert into customer_return_bill_details (CustomerReturnBill_ID,Data_ID,Type,TotalMeter,PriceBD,PriceAD,TotalAD,SellDiscount,CustomerBill_ID,Delegate_ID)values (@CustomerReturnBill_ID,@Data_ID,@Type,@TotalMeter,@PriceBD,@PriceAD,@TotalAD,@SellDiscount,@CustomerBill_ID,@Delegate_ID)";
                    com = new MySqlCommand(query, dbconnection);
                    com.Parameters.Add("@CustomerReturnBill_ID", MySqlDbType.Int16);
                    com.Parameters.Add("@Data_ID", MySqlDbType.Int16);
                    com.Parameters.Add("@Type", MySqlDbType.VarChar);
                    com.Parameters.Add("@TotalMeter", MySqlDbType.Decimal);
                    com.Parameters.Add("@PriceBD", MySqlDbType.Decimal);
                    com.Parameters.Add("@PriceAD", MySqlDbType.Decimal);
                    com.Parameters.Add("@TotalAD", MySqlDbType.Decimal);
                    com.Parameters.Add("@SellDiscount", MySqlDbType.Decimal);
                    com.Parameters.Add("@CustomerBill_ID", MySqlDbType.Int16);
                    com.Parameters.Add("@Delegate_ID", MySqlDbType.Int16);
                    foreach (DataGridViewRow row2 in dataGridView2.Rows)
                    {
                        if (row2.Cells[0].Value != null)
                        {
                            com.Parameters["@CustomerReturnBill_ID"].Value = id;
                            com.Parameters["@Data_ID"].Value = Convert.ToInt16(row2.Cells[0].Value);
                            com.Parameters["@Type"].Value = row2.Cells["type1"].Value;
                            com.Parameters["@TotalMeter"].Value = Convert.ToDouble(row2.Cells["Quantity"].Value);
                            com.Parameters["@priceBD"].Value = Convert.ToDouble(row2.Cells["priceBD"].Value);
                            com.Parameters["@PriceAD"].Value = Convert.ToDouble(row2.Cells["priceAD"].Value);
                            com.Parameters["@TotalAD"].Value = Convert.ToDouble(row2.Cells["totalAD"].Value);
                            com.Parameters["@SellDiscount"].Value = Convert.ToDouble(row2.Cells["Discount"].Value);
                            if (row2.Cells["CustomerBill_ID"].Value!= null && row2.Cells["Delegate_ID"].Value != null)
                            {
                                com.Parameters["@CustomerBill_ID"].Value = Convert.ToInt16(row2.Cells["CustomerBill_ID"].Value);
                                com.Parameters["@Delegate_ID"].Value = Convert.ToInt16(row2.Cells["Delegate_ID"].Value);

                            }
                            else
                            {
                                com.Parameters["@CustomerBill_ID"].Value = 0;
                                com.Parameters["@Delegate_ID"].Value = Convert.ToInt16(txtDelegate.Text);
                            }
                            com.ExecuteNonQuery();

                        }
                    }

                    MessageBox.Show("فاتورة مرتجع رقم "+Branch_BillNumber);
                    UserControl.ItemRecord("customer_return_bill", "اضافة", id, DateTime.Now, "", dbconnection);
                    clear();
                }
                else
                {
                    MessageBox.Show("تاكد من البيانات");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("تاكد من ادخال اسم المندوب");
            }
            dbconnection.Close();
        }
        private void rdbCash_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (rdbCash.Checked)
                {
                    type = "كاش";
                }
                else
                {
                    type = "آجل";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void comDelegate_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (load)
                    txtDelegate.Text = comDelegate.SelectedValue.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }
        private void txtDelegate_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                string query = "select Delegate_Name from delegate where Delegate_ID='" + txtDelegate.Text + "'";
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }
        private void dataGridView2_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (load1)
                {
                    if (e.ColumnIndex == 4 || e.ColumnIndex == 5)
                    {
                        DataGridViewRow dataRow = dataGridView2.Rows[e.RowIndex];
                        totalBill -= Convert.ToDouble(dataRow.Cells[7].Value.ToString());
                        double sellprice = Convert.ToDouble(dataRow.Cells[4].Value.ToString());
                        double discount = Convert.ToDouble(dataRow.Cells[5].Value.ToString());
                        double priceAD = sellprice;
                        if (discount != 0)
                        {
                           priceAD = sellprice * (sellprice * discount / 100);
                        }
                        dataRow.Cells[6].Value = priceAD;
                        dataRow.Cells[7].Value = priceAD* Convert.ToDouble(dataRow.Cells[3].Value.ToString());
                        totalBill += Convert.ToDouble(dataRow.Cells[7].Value.ToString());
                        txtBillTotalCostAD.Text = totalBill.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //function
        public void clear()
        {
            txtReturnPermission.Text = "";
            txtInfo.Text = "";
            txtBillTotalCostAD.Text = "";
            labBillNumber.Text = "";
            labClientName.Text = "";
            labClientPhone.Text = "";
            txtDelegate.Text = "";
            comDelegate.Text = "";
            dataGridView2.Rows.Clear();
        }
 
    }
}

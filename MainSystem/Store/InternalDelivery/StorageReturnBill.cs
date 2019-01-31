using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MainSystem
{
    public partial class StorageReturnBill : Form
    {
        MySqlConnection dbconnection , connectionReader, connectionReader2;
        
        bool flag = false;
        int[] addedRecordIDs;
        int recordCount = 0;
        List<int> listOfRow2In;
        int EmpBranchId = 0;
        int storeId = 0;
        bool flagCarton = false;
        DataRow row1 = null;
        bool loaded = false;

        public StorageReturnBill(MainForm SalesMainForm, DevExpress.XtraTab.XtraTabControl TabControlStores)
        {
            InitializeComponent();
            dbconnection = new MySqlConnection(connection.connectionString);
            connectionReader = new MySqlConnection(connection.connectionString);
            connectionReader2 = new MySqlConnection(connection.connectionString);
            addedRecordIDs = new int[100];
            listOfRow2In = new List<int>();
            comSupplier.AutoCompleteMode = AutoCompleteMode.Suggest;
            comSupplier.AutoCompleteSource = AutoCompleteSource.ListItems;
        }

        private void StorageReturnBill_Load(object sender, EventArgs e)
        {
            try
            {
                EmpBranchId = UserControl.EmpBranchID;

                string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"Store.txt");
                storeId = Convert.ToInt16(System.IO.File.ReadAllText(path));

                string query = "select * from supplier";
                MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                DataTable dt = new DataTable();
                da.Fill(dt);
                comSupplier.DataSource = dt;
                comSupplier.DisplayMember = dt.Columns["Supplier_Name"].ToString();
                comSupplier.ValueMember = dt.Columns["Supplier_ID"].ToString();
                comSupplier.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        
        /*private void comBillNumber_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (flag && comBillNumber.Text != "")
                {
                    int billNum = Convert.ToInt16(comBillNumber.Text);
                    DataTable dtAll = new DataTable();
                    string query = "select data.Data_ID,data.Code as 'الكود',concat(product.Product_Name,' - ',type.Type_Name,' - ',factory.Factory_Name,' - ',groupo.Group_Name,' ',COALESCE(color.Color_Name,''),' ',COALESCE(size.Size_Value,''),' ',COALESCE(sort.Sort_Value,'')) as 'الاسم',storage.Total_Meters as 'الكمية',data.Description as 'الوصف',storage.Storage_ID from storage inner join data on data.Data_ID=storage.Data_ID LEFT JOIN color ON color.Color_ID = data.Color_ID LEFT JOIN size ON size.Size_ID = data.Size_ID LEFT JOIN sort ON sort.Sort_ID = data.Sort_ID INNER JOIN type ON type.Type_ID = data.Type_ID INNER JOIN product ON product.Product_ID = data.Product_ID INNER JOIN factory ON data.Factory_ID = factory.Factory_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID  where storage.Store_ID=" + storeId+ " and storage.Permission_Number=";
                    MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                    DataTable dtProduct = new DataTable();
                    da.Fill(dtProduct);
                    
                    dataGridView1.DataSource = dtAll;
                    dataGridView1.Columns[0].Visible = false;
                    dataGridView1.Columns["CustomerBill_ID"].Visible = false;
                    dataGridView1.Columns["الفئة"].Visible = false;
                    dataGridView1.Columns["الوصف"].Visible = false;
                    dataGridView1.Columns["Delegate_ID"].Visible = false;
                    //dataGridView2.Rows.Clear();

                    txtCode.Text = "";
                    txtTotalMeter.Text = "";
                    txtReturnedQuantity.Text = "";
                    labBillDate.Text = "";

                    dbconnection.Open();
                    query = "select * from customer_bill where customer_bill.Branch_BillNumber=" + comBillNumber.Text + " and customer_bill.Branch_ID=" + storeId;
                    MySqlCommand com = new MySqlCommand(query, dbconnection);
                    MySqlDataReader dr = com.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            labBillDate.Text = Convert.ToDateTime(dr["Bill_Date"].ToString()).ToShortDateString();

                            if (!listBoxControlCustomerBill.Items.Contains(comBillNumber.SelectedValue.ToString()))
                            {
                                listBoxControlCustomerBill.Items.Add(comBillNumber.SelectedValue.ToString());
                            }
                        }
                        dr.Close();
                    }
                    else
                    {
                        //customerBillId = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }*/

        /*private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                row1 = dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex];

                dbconnection.Open();
                string query = "SELECT sum(customer_return_bill_details.TotalMeter) FROM customer_return_bill_details where customer_return_bill_details.CustomerBill_ID=" + row1.Cells["CustomerBill_ID"].Value.ToString() + " and customer_return_bill_details.Data_ID=" + row1.Cells["Data_ID"].Value.ToString() + " and customer_return_bill_details.Type='" + row1.Cells["الفئة"].Value.ToString() + "'";
                MySqlCommand com = new MySqlCommand(query, dbconnection);
                if (com.ExecuteScalar() != null)
                {
                    txtReturnedQuantity.Text = com.ExecuteScalar().ToString();
                }
                else
                {
                    txtReturnedQuantity.Text = "0";
                }

                txtCode.Text = row1.Cells["الكود"].Value.ToString();
                if (txtReturnedQuantity.Text == "")
                {
                    txtTotalMeter.Text = row1.Cells["الكمية"].Value.ToString();
                }
                else
                {
                    txtTotalMeter.Text = (Convert.ToDouble(row1.Cells["الكمية"].Value.ToString()) - Convert.ToDouble(txtReturnedQuantity.Text)).ToString();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }*/
        
        private void txtTotalMeter_TextChanged(object sender, EventArgs e)
        {
            try
            {
                double quantity, priceAD;
                if (txtBalat.Text != "" )
                {
                    if (double.TryParse(txtBalat.Text, out quantity))
                    {
                        //txtTotalAD.Text = (priceAD * quantity).ToString();
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
                // && txtCode.Text != ""
                /*if (!IsAdded(row1) && row1 != null)
                {
                    double totalMeter = 0;
                    if (!double.TryParse(txtBalat.Text, out totalMeter))
                    {
                        MessageBox.Show("اجمالى عدد الوحدات يجب ان يكون رقم");
                        return;
                    }

                    double returnedQuantity = 0;
                    if (txtCarton.Text != "")
                    {
                        returnedQuantity = Convert.ToDouble(txtCarton.Text);
                    }

                    if ((totalMeter + returnedQuantity) > Convert.ToDouble(row1.Cells["الكمية"].Value))
                    {
                        MessageBox.Show("الكمية لا تكفى");
                        return;
                    }
                    //myRows.Add(row1);
                    addedRecordIDs[recordCount] = dataGridView1.SelectedCells[0].RowIndex + 1;
                    dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex].DefaultCellStyle.BackColor = Color.Silver;
                    recordCount++;
                    int n = dataGridView2.Rows.Add();
                    dataGridView2.Rows[n].Cells["Data_ID"].Value = row1.Cells["Data_ID"].Value;
                    dataGridView2.Rows[n].Cells["Code"].Value = txtCode.Text;
                    dataGridView2.Rows[n].Cells["Type"].Value = row1.Cells["الفئة"].Value;
                    dataGridView2.Rows[n].Cells["Quantity"].Value = txtTotalMeter.Text;
                    dataGridView2.Rows[n].Cells["priceBD"].Value = row1.Cells["السعر"].Value;
                    dataGridView2.Rows[n].Cells["Discount"].Value = row1.Cells["نسبة الخصم"].Value;
                    dataGridView2.Rows[n].Cells["Product_Name"].Value = row1.Cells["الاسم"].Value;
                    
                    dataGridView2.Rows[n].Cells["Description"].Value = row1.Cells["الوصف"].Value;
                    if ((totalMeter + returnedQuantity) == Convert.ToDouble(row1.Cells["الكمية"].Value))
                    {
                        dataGridView2.Rows[n].Cells["Returned"].Value = "نعم";
                    }
                    else if ((totalMeter + returnedQuantity) < Convert.ToDouble(row1.Cells["الكمية"].Value))
                    {
                        dataGridView2.Rows[n].Cells["Returned"].Value = "جزء";
                    }

                    dataGridView2.Rows[n].Cells["Delegate_ID"].Value = row1.Cells["Delegate_ID"].Value;
                    dataGridView2.Rows[n].Cells["CustomerBill_ID"].Value = row1.Cells["CustomerBill_ID"].Value;

                    listOfRow2In.Add(n);
                    double totalAD = 0;
                    foreach (DataGridViewRow item in dataGridView2.Rows)
                    {
                        totalAD += Convert.ToDouble(item.Cells["totalAD"].Value);
                    }
                }
                else
                {
                    MessageBox.Show("برجاء اختيار عنصر والتاكد انه لم يتم اضافتة من قبل");
                }*/
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
                /*if (dataGridView2.Rows.Count > 0)
                {
                    string query = "select Branch_BillNumber from customer_return_bill where Branch_ID=" + storeId + " order by CustomerReturnBill_ID desc limit 1";
                    MySqlCommand com = new MySqlCommand(query, dbconnection);
                    int Branch_BillNumber = 1;
                    if (com.ExecuteScalar() != null)
                    {
                        Branch_BillNumber = Convert.ToInt16(com.ExecuteScalar()) + 1;
                    }
                    //Type_Buy
                    query = "insert into customer_return_bill (Branch_BillNumber,Branch_ID,Customer_ID,Customer_Name,Client_ID,Client_Name,Date,TotalCostAD,ReturnInfo,Store_Permission_Number,Type_Buy,Employee_ID,Employee_Name) values (@Branch_BillNumber,@Branch_ID,@Customer_ID,@Customer_Name,@Client_ID,@Client_Name,@Date,@TotalCostAD,@ReturnInfo,@Store_Permission_Number,@Type_Buy,@Employee_ID,@Employee_Name)";
                    com = new MySqlCommand(query, dbconnection);
                    com.Parameters.Add("@Branch_BillNumber", MySqlDbType.Int16);
                    com.Parameters["@Branch_BillNumber"].Value = Branch_BillNumber;
                    com.Parameters.Add("@Branch_ID", MySqlDbType.Int16);
                    com.Parameters["@Branch_ID"].Value = EmpBranchId;
                    //com.Parameters.Add("@CustomerBill_ID", MySqlDbType.Int16);
                    //com.Parameters["@CustomerBill_ID"].Value = customerBillId;

                    /*int storeNum = 0;
                    if (int.TryParse(txtStorePermission.Text, out storeNum))
                    {
                        com.Parameters.Add("@Store_Permission_Number", MySqlDbType.Int16);
                        com.Parameters["@Store_Permission_Number"].Value = storeNum;
                    }
                    else
                    {
                        MessageBox.Show("اذن المخزن يجب ان يكون عدد");
                        dbconnection.Close();
                        return;
                    }*
                    
                    com.Parameters.Add("@Date", MySqlDbType.DateTime);
                    com.Parameters["@Date"].Value = DateTime.Now;
                    com.Parameters.Add("@ReturnInfo", MySqlDbType.VarChar);
                    com.Parameters["@ReturnInfo"].Value = txtInfo.Text;
                    com.Parameters.Add("@TotalCostAD", MySqlDbType.Decimal);
                    //com.Parameters["@TotalCostAD"].Value = Convert.ToDouble(txtTotalReturnBillAD.Text);
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
                    /*foreach (DataGridViewRow row2 in dataGridView2.Rows)
                    {
                        if (row2.Cells[0].Value != null)
                        {
                            com.Parameters["@CustomerReturnBill_ID"].Value = id;
                            com.Parameters["@Data_ID"].Value = Convert.ToInt16(row2.Cells[0].Value);
                            com.Parameters["@Type"].Value = row2.Cells["Type"].Value;
                            com.Parameters["@TotalMeter"].Value = Convert.ToDouble(row2.Cells["Quantity"].Value);
                            com.Parameters["@priceBD"].Value = Convert.ToDouble(row2.Cells["priceBD"].Value);
                            com.Parameters["@PriceAD"].Value = Convert.ToDouble(row2.Cells["priceAD"].Value);
                            com.Parameters["@TotalAD"].Value = Convert.ToDouble(row2.Cells["totalAD"].Value);
                            com.Parameters["@SellDiscount"].Value = Convert.ToDouble(row2.Cells["Discount"].Value);
                            com.Parameters["@CustomerBill_ID"].Value = Convert.ToInt16(row2.Cells["CustomerBill_ID"].Value);
                            com.Parameters["@Delegate_ID"].Value = Convert.ToInt16(row2.Cells["Delegate_ID"].Value);
                            com.ExecuteNonQuery();
                            
                            string queryf = "update product_bill set Returned='" + row2.Cells["Returned"].Value + "' where CustomerBill_ID=" + row2.Cells["CustomerBill_ID"].Value + " and Data_ID=" + Convert.ToInt16(row2.Cells[0].Value) + " and Type='" + row2.Cells["Type"].Value + "'";
                            MySqlCommand c = new MySqlCommand(queryf, dbconnection);
                            c.ExecuteNonQuery();
                        }
                    }

                    /*for (int i = 0; i < listBoxControlCustomerBill.Items.Count; i++)
                    {
                        string queryf2 = "insert into customerbill_return (CustomerReturnBill_ID,CustomerBill_ID)values (@CustomerReturnBill_ID,@CustomerBill_ID)";
                        MySqlCommand c2 = new MySqlCommand(queryf2, dbconnection);
                        c2.Parameters.Add("@CustomerReturnBill_ID", MySqlDbType.Int16);
                        c2.Parameters["@CustomerReturnBill_ID"].Value = id;
                        c2.Parameters.Add("@CustomerBill_ID", MySqlDbType.Int16);
                        c2.Parameters["@CustomerBill_ID"].Value = Convert.ToInt16(listBoxControlCustomerBill.Items[i].ToString());
                        c2.ExecuteNonQuery();
                    }

                    //IncreaseProductQuantity(id);

                    UserControl.ItemRecord("customer_return_bill", "اضافة", id, DateTime.Now, "", dbconnection);
                    
                    MessageBox.Show("فاتورة رقم : " + Branch_BillNumber);

                    clrearAll();
                    clear(tableLayoutPanel1);
                    //returnBillReport.DisplayBillNumber();
                    //returnBillReport.DisplayBills();
                }
                else
                {
                    MessageBox.Show("تاكد من البيانات");
                }*/
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
                /*if (dataGridView2.Rows.Count > 0)
                {
                    int dgv2Index = dataGridView2.SelectedCells[0].RowIndex;
                    for (int i = 0; i < listOfRow2In.Count; i++)
                    {
                        if (listOfRow2In[i] == dgv2Index)
                        {
                            //myRows.RemoveAt(dgv2Index);
                            dataGridView2.Rows.Remove(dataGridView2.Rows[dgv2Index]);
                            dataGridView1.Rows[addedRecordIDs[i] - 1].DefaultCellStyle.BackColor = Color.White;
                            addedRecordIDs[i] = 0;
                            listOfRow2In.Remove(dgv2Index);
                            recordCount--;

                            double totalAD = 0;
                            foreach (DataGridViewRow item in dataGridView2.Rows)
                            {
                                totalAD += Convert.ToDouble(item.Cells["totalAD"].Value);
                            }
                        }
                    }
                }*/
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
                /*dataGridView1.DataSource = null;
                dataGridView2.Rows.Clear();*/
                txtPermissionNum.Text = "";
                txtCode.Text = "";
                txtBalat.Text = "";
                txtCarton.Text = "";
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txtPermissionNum_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (/*flag &&*/ txtPermissionNum.Text != "")
                {
                    int billNum = Convert.ToInt16(txtPermissionNum.Text);
                    
                    string query = "select data.Data_ID,data.Code as 'الكود',concat(product.Product_Name,' - ',type.Type_Name,' - ',factory.Factory_Name,' - ',groupo.Group_Name,' ',COALESCE(color.Color_Name,''),' ',COALESCE(size.Size_Value,''),' ',COALESCE(sort.Sort_Value,'')) as 'الاسم',storage.Total_Meters as 'الكمية',data.Description as 'الوصف',storage.Storage_ID from storage inner join data on data.Data_ID=storage.Data_ID LEFT JOIN color ON color.Color_ID = data.Color_ID LEFT JOIN size ON size.Size_ID = data.Size_ID LEFT JOIN sort ON sort.Sort_ID = data.Sort_ID INNER JOIN type ON type.Type_ID = data.Type_ID INNER JOIN product ON product.Product_ID = data.Product_ID INNER JOIN factory ON data.Factory_ID = factory.Factory_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID  where storage.Store_ID=" + storeId + " and storage.Permission_Number=" + txtPermissionNum.Text;
                    MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                    DataTable dtProduct = new DataTable();
                    da.Fill(dtProduct);

                    /*dataGridView1.DataSource = dtProduct;
                    dataGridView1.Columns[0].Visible = false;
                    dataGridView1.Columns["Storage_ID"].Visible = false;
                    dataGridView2.Rows.Clear();*/

                    txtCode.Text = "";
                    txtBalat.Text = "";
                    txtCarton.Text = "";

                    dbconnection.Open();
                    query = "select * from customer_bill where customer_bill.Branch_BillNumber=" + txtPermissionNum.Text + " and customer_bill.Branch_ID=" + storeId;
                    MySqlCommand com = new MySqlCommand(query, dbconnection);
                    MySqlDataReader dr = com.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            /*labBillDate.Text = Convert.ToDateTime(dr["Bill_Date"].ToString()).ToShortDateString();

                            if (!listBoxControlCustomerBill.Items.Contains(txtPermissionNum.Text))
                            {
                                listBoxControlCustomerBill.Items.Add(txtPermissionNum.Text);
                            }*/
                        }
                        dr.Close();
                    }
                    else
                    {
                        //customerBillId = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }

        private void txtPermissionNum_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtPermissionNum.Text != "")
                {
                    dbconnection.Close();

                    search();

                    dbconnection.Open();
                    string q = "select import_supplier_permission.Supplier_ID from import_supplier_permission INNER JOIN storage_import_permission ON storage_import_permission.StorageImportPermission_ID = import_supplier_permission.StorageImportPermission_ID where storage_import_permission.Import_Permission_Number=" + txtPermissionNum.Text + " and storage_import_permission.Store_ID=" + storeId;
                    MySqlCommand com = new MySqlCommand(q, dbconnection);
                    loaded = false;
                    if (com.ExecuteScalar() != null)
                    {
                        comSupplier.SelectedValue = com.ExecuteScalar().ToString();
                        comSupplier.Enabled = false;
                        comSupplier.DropDownStyle = ComboBoxStyle.DropDownList;
                    }
                    else
                    {
                        comSupplier.SelectedIndex = -1;
                        comSupplier.Enabled = true;
                        comSupplier.DropDownStyle = ComboBoxStyle.DropDown;
                    }
                    loaded = true;
                }
                else
                {
                    gridControl2.DataSource = null;

                    loaded = false;
                    comSupplier.SelectedIndex = -1;
                    loaded = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }

        private void txtCarton_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (row1 != null && !flagCarton)
                {
                    int NoCartons = 0;
                    double totalMeter = 0;
                    double carton = double.Parse(row1["الكرتنة"].ToString());
                    if (carton > 0)
                    {
                        if (int.TryParse(txtCarton.Text, out NoCartons))
                        { }
                        if (double.TryParse(txtTotalMeter.Text, out totalMeter))
                        { }

                        double total = carton * NoCartons;
                        flag = true;
                        txtTotalMeter.Text = (total).ToString();
                        flag = false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void txtTotalMeter_TextChanged_1(object sender, EventArgs e)
        {
            try
            {
                if (row1 != null && !flag)
                {
                    double totalMeter = 0;
                    if (double.TryParse(txtTotalMeter.Text, out totalMeter))
                    {
                        double carton = double.Parse(row1["الكرتنة"].ToString());
                        if (carton > 0)
                        {
                            flagCarton = true;
                            double total = totalMeter / carton;
                            txtCarton.Text = Convert.ToInt16(total).ToString();
                            flagCarton = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void gridView1_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            try
            {
                row1 = gridView1.GetDataRow(gridView1.GetRowHandle(e.RowHandle));
                string v = row1["الكود"].ToString();
                txtCode.Text = v;
                
                txtTotalMeter.Text = "0";
                txtCarton.Text = "0";
                txtBalat.Text = "0";
                double carton = double.Parse(row1["الكرتنة"].ToString());
                if (carton == 0)
                {
                    txtCarton.ReadOnly = true;
                    txtBalat.ReadOnly = true;
                }
                else
                {
                    txtCarton.ReadOnly = false;
                    txtBalat.ReadOnly = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void search()
        {
            string qq = "select data.Data_ID,data.Code as 'الكود',concat(product.Product_Name,' - ',type.Type_Name,' - ',factory.Factory_Name,' - ',groupo.Group_Name,' ',COALESCE(color.Color_Name,''),' ',COALESCE(size.Size_Value,''),' ',COALESCE(sort.Sort_Value,'')) as 'الاسم',import_supplier_permission.Supplier_Permission_Number as 'اذن استلام',supplier_permission_details.Balatat as 'عدد البلتات',supplier_permission_details.Carton_Balata as 'عدد الكراتين',supplier_permission_details.Total_Meters as 'متر/قطعة',DATE_FORMAT(supplier_permission_details.Date, '%d-%m-%Y %T') as 'تاريخ التخزين',store_places.Store_Place_Code as 'مكان التخزين',supplier_permission_details.Note as 'ملاحظة',import_supplier_permission.Supplier_ID,supplier_permission_details.Supplier_Permission_Details_ID,supplier_permission_details.Store_Place_ID from supplier_permission_details INNER JOIN data ON supplier_permission_details.Data_ID = data.Data_ID INNER JOIN import_supplier_permission ON supplier_permission_details.ImportSupplierPermission_ID = import_supplier_permission.ImportSupplierPermission_ID INNER JOIN storage_import_permission ON storage_import_permission.StorageImportPermission_ID = import_supplier_permission.StorageImportPermission_ID INNER JOIN store_places ON store_places.Store_Place_ID = supplier_permission_details.Store_Place_ID INNER JOIN type ON type.Type_ID = data.Type_ID INNER JOIN product ON product.Product_ID = data.Product_ID INNER JOIN factory ON data.Factory_ID = factory.Factory_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID LEFT JOIN color ON color.Color_ID = data.Color_ID LEFT JOIN size ON size.Size_ID = data.Size_ID LEFT JOIN sort ON sort.Sort_ID = data.Sort_ID where storage_import_permission.Import_Permission_Number=" + txtPermissionNum.Text + " and supplier_permission_details.Store_ID=" + storeId;
            MySqlDataAdapter da = new MySqlDataAdapter(qq, dbconnection);
            DataTable dt = new DataTable();
            da.Fill(dt);
            gridControl1.DataSource = dt;
            gridView1.Columns["Data_ID"].Visible = false;
            gridView1.Columns["Supplier_ID"].Visible = false;
            gridView1.Columns["Supplier_Permission_Details_ID"].Visible = false;
            gridView1.Columns["Store_Place_ID"].Visible = false;

            qq = "select data.Data_ID,data.Code as 'الكود',concat(product.Product_Name,' - ',type.Type_Name,' - ',factory.Factory_Name,' - ',groupo.Group_Name,' ',COALESCE(color.Color_Name,''),' ',COALESCE(size.Size_Value,''),' ',COALESCE(sort.Sort_Value,'')) as 'الاسم',import_supplier_permission.Supplier_Permission_Number as 'اذن استلام',supplier_permission_details.Balatat as 'عدد البلتات',supplier_permission_details.Carton_Balata as 'عدد الكراتين',supplier_permission_details.Total_Meters as 'متر/قطعة',DATE_FORMAT(supplier_permission_details.Date, '%d-%m-%Y %T') as 'تاريخ التخزين',supplier_permission_details.Note as 'ملاحظة',import_supplier_permission.Supplier_ID,supplier_permission_details.Supplier_Permission_Details_ID,supplier_permission_details.Store_Place_ID from supplier_permission_details INNER JOIN data ON supplier_permission_details.Data_ID = data.Data_ID INNER JOIN import_supplier_permission ON supplier_permission_details.ImportSupplierPermission_ID = import_supplier_permission.ImportSupplierPermission_ID INNER JOIN storage_import_permission ON storage_import_permission.StorageImportPermission_ID = import_supplier_permission.StorageImportPermission_ID INNER JOIN store_places ON store_places.Store_Place_ID = supplier_permission_details.Store_Place_ID INNER JOIN type ON type.Type_ID = data.Type_ID INNER JOIN product ON product.Product_ID = data.Product_ID INNER JOIN factory ON data.Factory_ID = factory.Factory_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID LEFT JOIN color ON color.Color_ID = data.Color_ID LEFT JOIN size ON size.Size_ID = data.Size_ID LEFT JOIN sort ON sort.Sort_ID = data.Sort_ID where storage_import_permission.Import_Permission_Number=" + txtPermissionNum.Text + " and supplier_permission_details.Store_ID=" + storeId;
            da = new MySqlDataAdapter(qq, dbconnection);
            dt = new DataTable();
            da.Fill(dt);
            gridControl2.DataSource = dt;
            gridView2.Columns["Data_ID"].Visible = false;
            gridView2.Columns["Supplier_ID"].Visible = false;
            gridView2.Columns["Supplier_Permission_Details_ID"].Visible = false;
            gridView2.Columns["Store_Place_ID"].Visible = false;
        }

        bool IsAdded(DataGridViewRow row1)
        {
            /*foreach (DataGridViewRow item in dataGridView2.Rows)
            {
                if ((row1.Cells["Data_ID"].Value.ToString() == item.Cells["Data_ID"].Value.ToString()) && (row1.Cells["الفئة"].Value.ToString() == item.Cells["Type"].Value.ToString()) && (row1.Cells["CustomerBill_ID"].Value.ToString() == item.Cells["CustomerBill_ID"].Value.ToString()))
                    return true;
            }*/
            return false;
        }
        
        //return quantity to store
        public void IncreaseProductQuantity(int billNumber)
        {
            connectionReader.Open();
            connectionReader2.Open();
            string q;
            int id;
            bool flag = false;
            double storageQ, productQ;
            string query = "select Data_ID,Type,TotalMeter from customer_return_bill_details where CustomerReturnBill_ID=" + billNumber;
            MySqlCommand com = new MySqlCommand(query, connectionReader);
            MySqlDataReader dr = com.ExecuteReader();

            while (dr.Read())
            {
                #region بند
                if (dr["Type"].ToString() == "بند")
                {
                    string query2 = "select Storage_ID,Total_Meters from storage where Data_ID=" + dr["Data_ID"].ToString() + " and Type='" + dr["Type"].ToString() + "'";
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
                }
                #endregion

                #region طقم
                if (dr["Type"].ToString() == "طقم")
                {
                    string query2 = "select Storage_ID,Total_Meters from storage where Set_ID=" + dr["Data_ID"].ToString() + " and Type='" + dr["Type"].ToString() + "'";
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
                }
                #endregion

                #region StorageTaxes
                string query3 = "select StorageTaxesID,Total_Meters from storage_taxes where Data_ID=" + dr["Data_ID"].ToString() + " and Type='" + dr["Type"].ToString() + "'";
                MySqlCommand com3 = new MySqlCommand(query3, connectionReader2);
                MySqlDataReader dr3 = com3.ExecuteReader();
                while (dr3.Read())
                {

                    storageQ = Convert.ToDouble(dr3["Total_Meters"]);
                    productQ = Convert.ToDouble(dr["TotalMeter"]);

                    storageQ += productQ;
                    id = Convert.ToInt16(dr3["StorageTaxesID"]);
                    q = "update storage_taxes set Total_Meters=" + storageQ + " where StorageTaxesID=" + id;
                    MySqlCommand comm = new MySqlCommand(q, dbconnection);
                    comm.ExecuteNonQuery();
                    flag = true;
                    break;

                }
                dr3.Close(); 
                #endregion

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

        //function
        //display bill number for selected customer/client
        public void DisplayBillNumber(int customerID, int clientID)
        {
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
                txtPermissionNum.Text = "";
                flag = false;
                string query = "";

                if (strQuery != "")
                {
                    query = "select Branch_BillNumber,CustomerBill_ID from customer_bill where Branch_ID=" + storeId + strQuery;
                }
                else
                {
                    query = "select Branch_BillNumber,CustomerBill_ID from customer_bill where Branch_ID=" + storeId;
                }
                //MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                //DataTable dt = new DataTable();
                //da.Fill(dt);
                //comBillNumber.DataSource = dt;
                //comBillNumber.DisplayMember = dt.Columns["Branch_BillNumber"].ToString();
                //comBillNumber.ValueMember = dt.Columns["CustomerBill_ID"].ToString();
                //comBillNumber.Text = "";

                flag = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        //clear all fields
        private void clrearAll()
        {
            try
            {
                txtReason.Text = "";

                txtCarton.Text = txtCode.Text = txtBalat.Text = "";

                /*dataGridView1.DataSource = null;
                dataGridView2.Rows.Clear();*/
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void clear(Control tlp)
        {
            foreach (Control co in tlp.Controls)
            {
                if (co is Panel || co is TableLayoutPanel)
                {
                    foreach (Control item in co.Controls)
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
            }
        }
    }
}

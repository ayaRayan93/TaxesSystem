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
    public partial class Supplier_Bill : Form
    {
        MySqlConnection conn;
        int[] courrentIDs;
        int[] addedRecordIDs;
        int recordCount = 0;
        int count = 0;
        int BillNo;
        int SupplierBillNo;
        double BuyPrice;
        bool flag = false;
        bool loaded2 = false;
        bool notAdded = false;
        string str;
        int storeId = 1;

        public Supplier_Bill()
        {
            InitializeComponent();
            courrentIDs = new int[100];
            addedRecordIDs = new int[100];
            conn = new MySqlConnection(connection.connectionString);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                //string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"Store.txt");
                //storeId = Convert.ToInt16(System.IO.File.ReadAllText(path));
                
                string query = "select StorageImportPermission_ID,Import_Permission_Number from storage_import_permission where Store_ID=" + storeId + " and Confirmed=0";
                MySqlDataAdapter da = new MySqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                comPermessionNum.DataSource = dt;
                comPermessionNum.DisplayMember = dt.Columns["Import_Permission_Number"].ToString();
                comPermessionNum.ValueMember = dt.Columns["StorageImportPermission_ID"].ToString();
                comPermessionNum.Text = "";

                flag = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            conn.Close();
        }

        private void dtnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                double quantity;
                if (double.TryParse(txtTotalMeter.Text, out quantity))
                {
                    conn.Open();
                if (!notAdded)
                {
                    addedRecordIDs[recordCount] = dataGridView1.SelectedCells[0].RowIndex+1;
                    dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex].DefaultCellStyle.BackColor = Color.LawnGreen;
                    recordCount++;
                }
                else
                {
                    MessageBox.Show("this recorded already added");
                    conn.Close();
                    return;
                }

                string query = "select Store_Name,Storage_Date from storage where Permission_Number=" + comPermessionNum.Text+" and Supplier_Name='"/*+comSupplier.Text+"'"*/;
                MySqlCommand com= new MySqlCommand(query, conn);

                MySqlDataReader reader= com.ExecuteReader();
                    //DateTime storeDate=DateTime.Now.Date;
                string storeDate="";
                while (reader.Read())
                {
                   
                    query = "insert into bill (Bill_Date,Store_Name,Supplier_Name,Code,Bill_Price,Buy_Discount,Normal_Increase,Categorical_Increase,Value_Additive_Tax,Total_Meters,Buy_Price) values (@Bill_Date,@Store_Name,@Supplier_Name,@Code,@Bill_Price,@Buy_Discount,@Normal_Increase,@Categorical_Increase,@Value_Additive_Tax,@Total_Meters,@Buy_Price)";
                    com = new MySqlCommand(query, conn);
                    com.Parameters.Add("@Bill_Date", MySqlDbType.Date);
                    com.Parameters["@Bill_Date"].Value = reader["Storage_Date"];
                    storeDate = reader["Storage_Date"].ToString();
                    string[] d = storeDate.Split('/');
                    if (d[1].Length < 2)
                    {
                        d[1] = "0" + d[1];
                    }
                    if (d[0].Length < 2)
                    {
                        d[0] = "0" + d[0];
                    }
                    storeDate = d[2].Split(' ')[0] + "-" + d[1] + "-" + d[0];
                    //  com.Parameters["@Bill_Date"].Value = dateTimePicker1.Value.Date;
                    com.Parameters.Add("@Store_Name", MySqlDbType.VarChar);
                    com.Parameters["@Store_Name"].Value = reader["Store_Name"];
                    //com.Parameters.Add("@Supplier_Name", MySqlDbType.VarChar);
                    //com.Parameters["@Supplier_Name"].Value = comSupplier.Text;
                    com.Parameters.Add("@Code", MySqlDbType.VarChar);
                    com.Parameters["@Code"].Value = txtCode.Text;
                    com.Parameters.Add("@Bill_Price", MySqlDbType.Decimal);
                    com.Parameters["@Bill_Price"].Value = txtPrice.Text;
                    com.Parameters.Add("@Buy_Discount", MySqlDbType.Decimal);
                    com.Parameters["@Buy_Discount"].Value = txtDiscount.Text;
                    com.Parameters.Add("@Normal_Increase", MySqlDbType.Decimal);
                    com.Parameters["@Normal_Increase"].Value = txtNormalIncrease.Text;
                    com.Parameters.Add("@Categorical_Increase", MySqlDbType.Decimal);
                    com.Parameters["@Categorical_Increase"].Value = txtCategoricalIncrease.Text;
                    com.Parameters.Add("@Value_Additive_Tax", MySqlDbType.Decimal);
                    com.Parameters["@Value_Additive_Tax"].Value = txtTax.Text;
                    com.Parameters.Add("@Total_Meters", MySqlDbType.Decimal);
                    com.Parameters["@Total_Meters"].Value = txtTotalMeter.Text;

                    if (BuyPrice > 0)
                    {
                        com.Parameters.Add("@Buy_Price", MySqlDbType.Decimal);
                        com.Parameters["@Buy_Price"].Value = BuyPrice;
                    }
                    else
                    {
                        MessageBox.Show("calculate Buy_Price first");
                        conn.Close();
                        return;
                    }
                }
                reader.Close();
                com.ExecuteNonQuery();

                
                query = "select Total_Meters from storage_taxes where Code='" + txtCode.Text+ "' and Buy_Price="+txtPurchasePrice.Text+ " and Date='"+storeDate+"'";
                    MySqlCommand comtaxes = new MySqlCommand(query, conn);
                if (comtaxes.ExecuteScalar() != null)
                {
                    double StoreQuantity = Convert.ToDouble(comtaxes.ExecuteScalar());
                    StoreQuantity += quantity;
                    query = "update storage_taxes set Total_Meters="+StoreQuantity+" where Code='" + txtCode.Text + "' and Buy_Price=" + txtPurchasePrice.Text + " and Date='" + storeDate + "'";
                        comtaxes = new MySqlCommand(query,conn);
                        comtaxes.ExecuteNonQuery();
                 }
                else
                {
                    //insert into storage Taxes table
                    query = "insert into storage_taxes (Code,Total_Meters,Buy_Price,Date) values (@Code,@Total_Meters,@Buy_Price,@Date)";
                        comtaxes = new MySqlCommand(query, conn);
                        comtaxes.Parameters.Add("@Code", MySqlDbType.VarChar);
                        comtaxes.Parameters["@Code"].Value = txtCode.Text;
                        comtaxes.Parameters.Add("@Total_Meters", MySqlDbType.Decimal);
                        comtaxes.Parameters["@Total_Meters"].Value = txtTotalMeter.Text;
                        comtaxes.Parameters.Add("@Buy_Price", MySqlDbType.Decimal);
                        comtaxes.Parameters["@Buy_Price"].Value = BuyPrice;
                        comtaxes.Parameters.Add("@Date", MySqlDbType.Date);
                        comtaxes.Parameters["@Date"].Value = storeDate;
                        comtaxes.ExecuteNonQuery();
                }
                    string q1 = "select Bill_ID from bill ORDER BY Bill_ID DESC LIMIT 1";
                    MySqlCommand comm = new MySqlCommand(q1, conn);
                    int id = (int)comm.ExecuteScalar();

                    //add to bill_data

                    query = "insert into Bill_Data (Bill_ID,Bill_No,Supplier_ID,Supplier_Bill_No) values (@Bill_ID,@Bill_No,@Supplier_ID,@Supplier_Bill_No)";
                    com = new MySqlCommand(query, conn);
                    com.Parameters.Add("@Bill_ID", MySqlDbType.Int16);
                    com.Parameters["@Bill_ID"].Value = id;
                    com.Parameters.Add("@Bill_No", MySqlDbType.Int16);
                    com.Parameters["@Bill_No"].Value = BillNo;
                    //com.Parameters.Add("@Supplier_ID", MySqlDbType.Int16);
                    //com.Parameters["@Supplier_ID"].Value = comSupplier.SelectedValue;
                    com.Parameters.Add("@Supplier_Bill_No", MySqlDbType.Int16);
                    com.Parameters["@Supplier_Bill_No"].Value = SupplierBillNo;
                    com.ExecuteNonQuery();

                    courrentIDs[count] = id;
                    count++;

                    str = "";
                    for (int i = 0; i < courrentIDs.Length - 1; i++)
                    {
                        if (courrentIDs[i] != 0)
                        {
                            str += courrentIDs[i] + ",";
                        }
                    }
                    str += courrentIDs[courrentIDs.Length - 1];

                    string qq = "select Bill_Date as 'التاريخ',Store_Name as 'المخزن',Supplier_Name as 'المورد',Code as 'كود',Bill_Price as 'السعر',Buy_Discount as 'خصم الشراء',Normal_Increase as'زيادة عادية',Categorical_Increase as 'زيادة قطعية',Value_Additive_Tax as 'ضريبة القيمة المضافة',Buy_Price as 'سعر الشراء',Total_Meters as 'اجمالي عدد الامتار' from bill where Bill_ID in (" + str + ") ";
                    MySqlDataAdapter da = new MySqlDataAdapter(qq, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dataGridView2.DataSource = dt;
                    dataGridView2.FirstDisplayedScrollingRowIndex = dataGridView2.RowCount - 1;
                    string query1 = "select sum(bill.Buy_Price*bill.Total_Meters) from bill INNER JOIN Bill_Data ON bill.Bill_ID=Bill_Data.Bill_ID where Bill_Data.Bill_No=" + BillNo + "";
                    com = new MySqlCommand(query1, conn);
                    string query2 = "select sum(bill.Bill_Price*bill.Total_Meters) from bill INNER JOIN Bill_Data ON bill.Bill_ID=Bill_Data.Bill_ID where Bill_Data.Bill_No=" + BillNo + "";
                    MySqlCommand com2 = new MySqlCommand(query2, conn);
                    if (com.ExecuteScalar() == null && com2.ExecuteScalar() == null)
                    {
                        MessageBox.Show("error");
                    }
                    else
                    {
                        decimal x = (decimal)com.ExecuteScalar();
                        decimal x2 = (decimal)com2.ExecuteScalar();
                        label15.Text = x.ToString();
                        label1.Text = x2.ToString();
                        string q2 = "update bill_data set Total_Price=" + x + " , Total_Price_B_D=" + x2 + " where Bill_No=" + BillNo + "";
                        com = new MySqlCommand(q2, conn);
                        com.ExecuteNonQuery();

                    }
                    MessageBox.Show("Add success");
                }
                else
                {

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            conn.Close();
        }
      
        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (txtPrice.Text != "" && txtCategoricalIncrease.Text != "" && txtDiscount.Text != "" && txtNormalIncrease.Text != "" && txtTax.Text != "")
            {
                double price, BuyDiscount, NormalIncrease, Categorical_Increase,VAT;
                if (double.TryParse(txtPrice.Text, out price)
                 &&
                 double.TryParse(txtDiscount.Text, out BuyDiscount)
                 &&
                 double.TryParse(txtNormalIncrease.Text, out NormalIncrease)
                 &&
                 double.TryParse(txtCategoricalIncrease.Text, out Categorical_Increase)
                 &&
                 double.TryParse(txtTax.Text, out VAT))
                {
                    BuyPrice = price + NormalIncrease;
                    BuyPrice -= BuyDiscount;
                    BuyPrice += Categorical_Increase;
                    BuyPrice += VAT;
                    txtPurchasePrice.Text = BuyPrice.ToString();
                }
            }
            else
            {
                txtPurchasePrice.Text = "";
            }
        }

        /*private void comboBox2_SelectedValueChanged(object sender, EventArgs e)
        {
            if (flag)
            {
                
                try
                {
                    conn.Open();
                    //get Bill Number
                    string q = "select Bill_No from Bill_Data ORDER BY ID DESC LIMIT 1 ";
                    MySqlCommand com = new MySqlCommand(q, conn);
                    BillNo = (int)com.ExecuteScalar();
                    BillNo++;
                    
                    comPermessionNum.Visible = true;
                    //string query = "select Import_Permission_Number from storage_import_permission where Store_ID=" + storeId + " and Confirmed=0";
                    //MySqlDataAdapter da = new MySqlDataAdapter(query, conn);
                    //DataTable dt = new DataTable();
                    //da.Fill(dt);
                    //comPermessionNum.DataSource = dt;
                    //comPermessionNum.DisplayMember = dt.Columns["Permission_Number"].ToString();
                    //comPermessionNum.Text = "";
                    loaded2 = true;
                    NewBill();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                conn.Close();
            }
        }*/

        private void comPermessionNum_SelectedValueChanged(object sender, EventArgs e)
        {
           
            if (flag)
            {
                try
                {
                    conn.Close();
                    conn.Open();
                    NewBill();
                    string q = "SELECT DISTINCT data.Data_ID,data.Code AS 'الكود',product.Product_Name AS 'الصنف',type.Type_Name AS 'النوع',factory.Factory_Name AS 'المصنع',groupo.Group_Name AS 'المجموعه',purchasing_price.Price AS 'السعر',purchasing_price.Normal_Increase AS 'الزيادة العادية',purchasing_price.Categorical_Increase AS 'الزيادة القطعية',purchasing_price.Purchasing_Discount AS 'خصم الشراء',purchasing_price.Price AS 'سعر الشراء',supplier_permission_details.Total_Meters as 'اجمالى عدد الامتار' FROM storage_import_permission INNER JOIN import_supplier_permission ON import_supplier_permission.StorageImportPermission_ID = storage_import_permission.StorageImportPermission_ID inner join supplier_permission_details on supplier_permission_details.ImportSupplierPermission_ID=import_supplier_permission.ImportSupplierPermission_ID INNER JOIN data ON supplier_permission_details.Data_ID = data.Data_ID INNER JOIN type ON type.Type_ID = data.Type_ID INNER JOIN product ON product.Product_ID = data.Product_ID INNER JOIN factory ON data.Factory_ID = factory.Factory_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID left JOIN purchasing_price ON data.Data_ID = purchasing_price.Data_ID where storage_import_permission.StorageImportPermission_ID=" + comPermessionNum.SelectedValue.ToString();
                    MySqlDataAdapter da = new MySqlDataAdapter(q, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dataGridView1.DataSource = dt;
                    dataGridView1.Columns[0].Visible = false;

                    q = "select Bill_No from Bill_Data ORDER BY ID DESC LIMIT 1 ";
                    MySqlCommand com = new MySqlCommand(q, conn);
                    BillNo = (int)com.ExecuteScalar();
                    BillNo++;
                }
                catch (Exception ex)
                {
                  MessageBox.Show(ex.ToString());
                }
            }
            conn.Close();
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            try
            {
                string qq = "select Bill_Date ,Store_Name ,Supplier_Name,Code,Bill_Price ,Buy_Discount ,Normal_Increase ,Categorical_Increase ,Value_Additive_Tax ,Buy_Price ,Total_Meters  from bill where Bill_ID in (" + str + ") ";
                MySqlDataAdapter da = new MySqlDataAdapter(qq, conn);
                DataSet ds = new DataSet();
                da.Fill(ds);
                /*Form2 f = new Form2(ds);
                f.Show();*/
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //function
        //new bill
        public void NewBill()
        {
           
            for (int i = 0; i < courrentIDs.Length - 1; i++)
            {
                if (courrentIDs[i] != 0)
                {
                    courrentIDs[i] = 0;
                }
            }
            count = 0;

            for (int i = 0; i < addedRecordIDs.Length - 1; i++)
            {
                if (addedRecordIDs[i] != 0)
                {
                    addedRecordIDs[i] = 0;
                }
            }
            recordCount = 0;
            dataGridView2.DataSource = null;
            dataGridView1.DataSource = null;
            Clear();
        }
        //clear fields
        public void Clear()
        {
            txtCode.Text = txtPrice.Text = txtCategoricalIncrease.Text = txtDiscount.Text = txtNormalIncrease.Text  = txtTotalMeter.Text = "";
            txtTax.Text = "0";
            label1.Text = txtPurchasePrice.Text = label15.Text = "";
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex];
            txtCode.Text = row.Cells["الكود"].Value.ToString();
            txtPrice.Text = row.Cells["السعر"].Value.ToString();
            txtPurchasePrice.Text = row.Cells["سعر الشراء"].Value.ToString();
            txtNormalIncrease.Text = row.Cells["الزيادة العادية"].Value.ToString();
            txtDiscount.Text = row.Cells["خصم الشراء"].Value.ToString();
            txtCategoricalIncrease.Text = row.Cells["الزيادة القطعية"].Value.ToString();
            txtTotalMeter.Text = row.Cells["اجمالى عدد الامتار"].Value.ToString();
            if (txtCategoricalIncrease.Text == "" && txtNormalIncrease.Text == "")
            {
                txtCategoricalIncrease.Text = txtNormalIncrease.Text = "0.00";
            }

            for (int i = 0; i < addedRecordIDs.Length; i++)
            {
                if (addedRecordIDs[i] == dataGridView1.SelectedCells[0].RowIndex + 1)
                {
                    notAdded = true;

                    break;
                }
            }
        }
    }
}

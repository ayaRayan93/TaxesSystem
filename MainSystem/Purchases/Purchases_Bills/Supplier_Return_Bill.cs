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
    public partial class Supplier_Return_Bill : Form
    {
        //variables 
        MySqlConnection dbconnection;
        bool loaded = false;
        bool flag = false;
        private int [] addedRecordIDs;
        int recordCount = 0;
        private bool Added = false;
        double BuyPrice;
        private int count=0;
       
        private int [] courrentIDs;
        private int BillNo;
        string str;

        public Supplier_Return_Bill()
        {
            InitializeComponent();
            addedRecordIDs = new int[50];
            courrentIDs = new int[50];
            dbconnection = new MySqlConnection(connection.connectionString);
            //cbPermissionNumber.Visible = false;
        }
        //events
        private void Supplier_Return_Bill_Load(object sender, EventArgs e)
        {
            try
            {
                dbconnection.Open();
                suppliersListCombobox();
                loaded = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }
        private void cbSuppliers_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (loaded)
                {
                    dbconnection.Close();
                    dbconnection.Open();
                    
                    permissionNumberOfSupplier(cbSuppliers.Text);
                    //cbPermissionNumber.Visible = true;
                    flag = true;

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }
        /*private void cbPermissionNumber_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (flag)
                {
                    dbconnection.Close();
                    dbconnection.Open();
                    newReturnBill();
                    displayPermissionDetails(cbSuppliers.Text, cbPermissionNumber.Text);
                    
                    //get Bill Number
                    string query = "select Return_Bill_No from returnBill_returnitems ORDER BY Return_Bill_No DESC LIMIT 1 ";
                    MySqlCommand com = new MySqlCommand(query, dbconnection);
                    if (com.ExecuteScalar() != null)
                    {
                        BillNo = (int)com.ExecuteScalar();
                        BillNo++;
                    }
                    else
                    {
                        BillNo = 1;
                    }

                    query = "insert into Return_Bill (Return_Bill_No,Supplier_ID,Supplier_Return_Bill_No) values (@Bill_No,@Supplier_ID,@Supplier_Bill_No)";
                    com = new MySqlCommand(query, dbconnection);
                    
                    com.Parameters.Add("@Bill_No", MySqlDbType.Int16);
                    com.Parameters["@Bill_No"].Value = BillNo;
                    com.Parameters.Add("@Supplier_ID", MySqlDbType.Int16);
                    com.Parameters["@Supplier_ID"].Value = cbSuppliers.SelectedValue;
                    com.Parameters.Add("@Supplier_Bill_No", MySqlDbType.Int16);
                    int id;
                    if (int.TryParse(cbPermissionNumber.Text, out id))
                    {
                        com.Parameters["@Supplier_Bill_No"].Value = id;
                    }
                    else
                    {
                        MessageBox.Show("error in Permission Number");
                        dbconnection.Close();
                        return;
                    }
                    com.ExecuteNonQuery();
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
            DataGridViewRow row = dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex];
            string v = row.Cells[0].Value.ToString();
            txtCode.Text = v;
            v = row.Cells[6].Value.ToString();
            txtPrice.Text = v;
            v = row.Cells[7].Value.ToString();
            txtNormalIncrease.Text = v;
            v = row.Cells[8].Value.ToString();
            txtBuyDiscount.Text = v;
            v = row.Cells[9].Value.ToString();
            txtCategoricalIncrease.Text = v;
            v = row.Cells[5].Value.ToString();
            txtTotalMeters.Text = v;
            if (txtCategoricalIncrease.Text == "" && txtNormalIncrease.Text == "")
            {
                txtCategoricalIncrease.Text = txtNormalIncrease.Text = "0.00";
            }

            for (int i = 0; i < addedRecordIDs.Length; i++)
            {
                if (addedRecordIDs[i] == dataGridView1.SelectedCells[0].RowIndex + 1)
                {
                    Added = true;

                    break;
                }
            }
        }*/
        private void newReturnBill_Click(object sender, EventArgs e)
        {
            try
            {
                dbconnection.Open();
                //cbPermissionNumber.Visible = false;
                newReturnBill();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            dbconnection.Close();
        }
        private void txtNormalIncrease_TextChanged(object sender, EventArgs e)
        {
            if (txtPrice.Text != "" && txtCategoricalIncrease.Text != "" && txtBuyDiscount.Text != "" && txtNormalIncrease.Text != "" && txtTAV.Text != "")
            {
                double price, BuyDiscount, NormalIncrease, Categorical_Increase, VAT;
                if (double.TryParse(txtPrice.Text, out price)
                 &&
                 double.TryParse(txtBuyDiscount.Text, out BuyDiscount)
                 &&
                 double.TryParse(txtNormalIncrease.Text, out NormalIncrease)
                 &&
                 double.TryParse(txtCategoricalIncrease.Text, out Categorical_Increase)
                 &&
                 double.TryParse(txtTAV.Text, out VAT))
                {
                    BuyPrice = price + NormalIncrease;
                    BuyPrice -= BuyDiscount;
                    BuyPrice += Categorical_Increase;
                    BuyPrice += VAT;
                    txtBuyPrice.Text = BuyPrice.ToString();
                }
            }
            else
            {
                txtBuyPrice.Text = "";
            }
        }
        private void addToReturnBill_Click(object sender, EventArgs e)
        {
            try
            {
                dbconnection.Open();
                if (!Added)
                {
                    /*addedRecordIDs[recordCount] = dataGridView1.SelectedCells[0].RowIndex + 1;
                    dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex].DefaultCellStyle.BackColor = Color.LawnGreen;*/
                    recordCount++;
                }
                else
                {
                    MessageBox.Show("this recorded already added");
                    dbconnection.Close();
                    return;
                }

                string query = "select Store_Name,Storage_Date from storage where Permission_Number=" /*+ cbPermissionNumber.Text */+ " and Supplier_Name='" + cbSuppliers.Text + "'";
                MySqlCommand com = new MySqlCommand(query, dbconnection);

                MySqlDataReader reader = com.ExecuteReader();

                while (reader.Read())
                {

                    query = "insert into return_bill_details (Return_Bill_Date,Store_Name,Supplier_Name,Code,Price,Buy_Discount,Normal_Increase,Categorical_Increase,Value_Additive_Tax,Total_Meters,Buy_Price) values (@Bill_Date,@Store_Name,@Supplier_Name,@Code,@Bill_Price,@Buy_Discount,@Normal_Increase,@Categorical_Increase,@Value_Additive_Tax,@Total_Meters,@Buy_Price)";
                    com = new MySqlCommand(query, dbconnection);
                    com.Parameters.Add("@Bill_Date", MySqlDbType.Date);
                    com.Parameters["@Bill_Date"].Value = reader["Storage_Date"];
                    com.Parameters.Add("@Store_Name", MySqlDbType.VarChar);
                    com.Parameters["@Store_Name"].Value = reader["Store_Name"];
                    com.Parameters.Add("@Supplier_Name", MySqlDbType.VarChar);
                    com.Parameters["@Supplier_Name"].Value = cbSuppliers.Text;
                    com.Parameters.Add("@Code", MySqlDbType.VarChar);
                    com.Parameters["@Code"].Value = txtCode.Text;
                    com.Parameters.Add("@Bill_Price", MySqlDbType.Decimal);
                    com.Parameters["@Bill_Price"].Value = txtPrice.Text;
                    com.Parameters.Add("@Buy_Discount", MySqlDbType.Decimal);
                    com.Parameters["@Buy_Discount"].Value = txtBuyDiscount.Text;
                    com.Parameters.Add("@Normal_Increase", MySqlDbType.Decimal);
                    com.Parameters["@Normal_Increase"].Value = txtNormalIncrease.Text;
                    com.Parameters.Add("@Categorical_Increase", MySqlDbType.Decimal);
                    com.Parameters["@Categorical_Increase"].Value = txtCategoricalIncrease.Text;
                    com.Parameters.Add("@Value_Additive_Tax", MySqlDbType.Decimal);
                    com.Parameters["@Value_Additive_Tax"].Value = txtTAV.Text;
                    com.Parameters.Add("@Total_Meters", MySqlDbType.Decimal);
                    com.Parameters["@Total_Meters"].Value = txtTotalMeters.Text;

                    if (BuyPrice > 0)
                    {
                        com.Parameters.Add("@Buy_Price", MySqlDbType.Decimal);
                        com.Parameters["@Buy_Price"].Value = BuyPrice;
                    }
                    else
                    {
                        MessageBox.Show("calculate Buy_Price first");
                        dbconnection.Close();
                        return;
                    }
                }
                reader.Close();
                com.ExecuteNonQuery();


                query = "select Return_Bill_ID from return_bill_details ORDER BY Return_Bill_ID DESC LIMIT 1";
                com = new MySqlCommand(query, dbconnection);
                int id = (int)com.ExecuteScalar();

                //add to returnBill_returnitems table which content the ids of items of each returnBill

                query = "INSERT into returnBill_returnitems (Return_Bill_Item_ID,Return_Bill_No) values (@Return_Bill_Item_ID,@Return_Bill_No)";
                com = new MySqlCommand(query, dbconnection);
                com.Parameters.Add("@Return_Bill_Item_ID", MySqlDbType.Int16);
                com.Parameters["@Return_Bill_Item_ID"].Value = id;
                com.Parameters.Add("@Return_Bill_No", MySqlDbType.Int16);
                com.Parameters["@Return_Bill_No"].Value = BillNo;
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

                string qq = "select Return_Bill_Date as 'التاريخ',Store_Name as 'المخزن',Supplier_Name as 'المورد',Code as 'كود',Price as 'السعر',Buy_Discount as 'خصم الشراء',Normal_Increase as'زيادة عادية',Categorical_Increase as 'زيادة قطعية',Value_Additive_Tax as 'ضريبة القيمة المضافة',Buy_Price as 'سعر الشراء',Total_Meters as 'اجمالي عدد الامتار' from return_bill_details where Return_Bill_ID in (" + str + ") ";
                MySqlDataAdapter da = new MySqlDataAdapter(qq, dbconnection);
                DataTable dt = new DataTable();
                da.Fill(dt);
                /*dataGridView2.DataSource = dt;
                dataGridView2.FirstDisplayedScrollingRowIndex = dataGridView2.RowCount - 1;*/
                string query1 = "select sum(return_bill_details.Buy_Price) from return_bill_details INNER JOIN returnBill_returnitems ON return_bill_details.Return_Bill_ID=returnBill_returnitems.Return_Bill_Item_ID where returnBill_returnitems.Return_Bill_No=" + BillNo + "";
                com = new MySqlCommand(query1, dbconnection);
                string query2 = "select sum(return_bill_details.Price) from return_bill_details INNER JOIN returnBill_returnitems ON return_bill_details.Return_Bill_ID=returnBill_returnitems.Return_Bill_Item_ID where returnBill_returnitems.Return_Bill_No=" + BillNo + "";
                MySqlCommand com2 = new MySqlCommand(query2, dbconnection);
                if (com.ExecuteScalar() == null && com2.ExecuteScalar() == null)
                {
                    MessageBox.Show("error");
                }
                else
                {
                    double x = double.Parse(com.ExecuteScalar().ToString());
                    double x2 = double.Parse(com2.ExecuteScalar().ToString());
                    labTotalPrice.Text = com.ExecuteScalar().ToString();
                    labTotalPriceBD.Text = com2.ExecuteScalar().ToString();
                    string q2 = "update return_bill set Total_Price=" + x + " , Total_Price_BD=" + x2 + " where Return_Bill_No=" + BillNo + "";
                    com = new MySqlCommand(q2, dbconnection);
                    com.ExecuteNonQuery();

                }
                MessageBox.Show("Add success");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            dbconnection.Close();
        }
        private void btnReport_Click(object sender, EventArgs e)
        {
            /*try
            {
                string qq = "select Return_Bill_Date ,Store_Name ,Supplier_Name ,Code ,Price ,Buy_Discount ,Normal_Increase ,Categorical_Increase ,Value_Additive_Tax ,Buy_Price ,Total_Meters  from return_bill_details where Return_Bill_ID in (" + str + ") ";
                MySqlDataAdapter da = new MySqlDataAdapter(qq, dbconnection);
                DataSet ds = new DataSet();
                da.Fill(ds);
                Form2 f = new Form2(ds);
                f.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }*/
        }
        
        //functions
        //assign suppliers names to combobox 
        public void suppliersListCombobox()
        {
            string query = "select * from supplier";
            MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
            DataTable dt = new DataTable();
            da.Fill(dt);
            cbSuppliers.DataSource = dt;
            cbSuppliers.DisplayMember = dt.Columns["Supplier_Name"].ToString();
            cbSuppliers.ValueMember = dt.Columns["Supplier_ID"].ToString();
            cbSuppliers.Text = "";
        }
        //assign Permission_Numbers of specific supplier
        public void permissionNumberOfSupplier(string supplierName)
        {
            /*string query = "select distinct Permission_Number  from storage where Supplier_Name='" + supplierName + "' order by Permission_Number DESC ";
            MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
            DataTable dt = new DataTable();
            da.Fill(dt);
            cbPermissionNumber.DataSource = dt;
            cbPermissionNumber.DisplayMember = dt.Columns["Permission_Number"].ToString();
            cbPermissionNumber.Text = "";*/
        }
        //display permission details on grid view
        public void displayPermissionDetails(string supplierName,string permissionNumber )
        {
            string q = "select storage.Code as 'كود', type.Type_Name as 'نوع',factory.Factory_Name as 'المصنع',groupo.Group_Name as 'المجموعه',product.Product_Name as 'المنتج', storage.Total_Meters as 'اجمالي عدد الامتار',price.Price as 'السعر', price.Normal_Increase as 'الزيادة العادية',price.Buy_Discount as 'خصم الشراء',price.Categorical_Increase as 'الزيادة القطعية' from storage INNER JOIN data  ON storage.Code = data.Code INNER JOIN type ON type.Type_ID = data.Type_ID INNER JOIN product ON product.Product_ID = data.Product_ID INNER JOIN factory ON data.Factory_ID = factory.Factory_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID INNER JOIN price ON storage.Code = price.Code where storage.Supplier_Name='" + supplierName + "' and  storage.Permission_Number=" + permissionNumber + "";
            MySqlDataAdapter da = new MySqlDataAdapter(q, dbconnection);
            DataTable dt = new DataTable();
            da.Fill(dt);
            /*dataGridView1.DataSource = dt;*/
        }
        //new bill clear all controls 
        public void newReturnBill()
        {         
            for (int i = 0; i < courrentIDs.Length - 1; i++)
            {
                if (courrentIDs[i] != 0)
                {
                    courrentIDs[i] = 0;
                }
            }
            count = 0;
            for (int i = 0; i < addedRecordIDs.Length; i++)
            {
                addedRecordIDs[i] = -1;
               
            }
            recordCount = 0;
            /*dataGridView2.DataSource = null;
            dataGridView1.DataSource = null;*/
            txtBuyDiscount.Text = txtCategoricalIncrease.Text = txtCode.Text = "";
            txtPrice.Text = txtNormalIncrease.Text= txtTotalMeters.Text = "";
            txtBuyPrice.Text = labTotalPrice.Text = labTotalPriceBD.Text = "";
        }

    }
}

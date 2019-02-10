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
        public Supplier_Bill()
        {
            InitializeComponent();
            courrentIDs = new int[100];
            addedRecordIDs = new int[100];
            comboBox3.Visible = false;
            conn = new MySqlConnection(connection.connectionString);
            

            try
            {
             
                string query = "select * from supplier";
                MySqlDataAdapter da = new MySqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                comboBox2.DataSource = dt;
                comboBox2.DisplayMember = dt.Columns["Supplier_Name"].ToString();
                comboBox2.ValueMember = dt.Columns["Supplier_ID"].ToString();
                comboBox2.Text = "";

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            conn.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                flag = true;
                conn.Open();

                comboBox2.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            conn.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
            DataGridViewRow row = dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex];
            string v = row.Cells[0].Value.ToString();
            textBox1.Text = v;
             v = row.Cells[6].Value.ToString();
            textBox2.Text = v;
             v = row.Cells[7].Value.ToString();
            textBox5.Text = v;
            v = row.Cells[8].Value.ToString();
            textBox4.Text = v;
            v = row.Cells[9].Value.ToString();
            textBox3.Text = v;
            v = row.Cells[5].Value.ToString();
            textBox7.Text = v;
            if (textBox3.Text==""&& textBox5.Text == "")
            {
                textBox3.Text = textBox5.Text = "0.00";
            }
            
            for (int i = 0; i < addedRecordIDs.Length; i++)
            {
                if (addedRecordIDs[i] == dataGridView1.SelectedCells[0].RowIndex+1)
                {
                    notAdded = true;
                    
                    break;
                }
            }

            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                double quantity;
                if (double.TryParse(textBox7.Text, out quantity))
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

                string query = "select Store_Name,Storage_Date from storage where Permission_Number=" + comboBox3.Text+" and Supplier_Name='"+comboBox2.Text+"'";
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
                    com.Parameters.Add("@Supplier_Name", MySqlDbType.VarChar);
                    com.Parameters["@Supplier_Name"].Value = comboBox2.Text;
                    com.Parameters.Add("@Code", MySqlDbType.VarChar);
                    com.Parameters["@Code"].Value = textBox1.Text;
                    com.Parameters.Add("@Bill_Price", MySqlDbType.Decimal);
                    com.Parameters["@Bill_Price"].Value = textBox2.Text;
                    com.Parameters.Add("@Buy_Discount", MySqlDbType.Decimal);
                    com.Parameters["@Buy_Discount"].Value = textBox4.Text;
                    com.Parameters.Add("@Normal_Increase", MySqlDbType.Decimal);
                    com.Parameters["@Normal_Increase"].Value = textBox5.Text;
                    com.Parameters.Add("@Categorical_Increase", MySqlDbType.Decimal);
                    com.Parameters["@Categorical_Increase"].Value = textBox3.Text;
                    com.Parameters.Add("@Value_Additive_Tax", MySqlDbType.Decimal);
                    com.Parameters["@Value_Additive_Tax"].Value = textBox6.Text;
                    com.Parameters.Add("@Total_Meters", MySqlDbType.Decimal);
                    com.Parameters["@Total_Meters"].Value = textBox7.Text;

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

                
                query = "select Total_Meters from storage_taxes where Code='" + textBox1.Text+ "' and Buy_Price="+label13.Text+ " and Date='"+storeDate+"'";
                    MySqlCommand comtaxes = new MySqlCommand(query, conn);
                if (comtaxes.ExecuteScalar() != null)
                {
                    double StoreQuantity = Convert.ToDouble(comtaxes.ExecuteScalar());
                    StoreQuantity += quantity;
                    query = "update storage_taxes set Total_Meters="+StoreQuantity+" where Code='" + textBox1.Text + "' and Buy_Price=" + label13.Text + " and Date='" + storeDate + "'";
                        comtaxes = new MySqlCommand(query,conn);
                        comtaxes.ExecuteNonQuery();
                 }
                else
                {
                    //insert into storage Taxes table
                    query = "insert into storage_taxes (Code,Total_Meters,Buy_Price,Date) values (@Code,@Total_Meters,@Buy_Price,@Date)";
                        comtaxes = new MySqlCommand(query, conn);
                        comtaxes.Parameters.Add("@Code", MySqlDbType.VarChar);
                        comtaxes.Parameters["@Code"].Value = textBox1.Text;
                        comtaxes.Parameters.Add("@Total_Meters", MySqlDbType.Decimal);
                        comtaxes.Parameters["@Total_Meters"].Value = textBox7.Text;
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
                    com.Parameters.Add("@Supplier_ID", MySqlDbType.Int16);
                    com.Parameters["@Supplier_ID"].Value = comboBox2.SelectedValue;
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
            if (textBox2.Text != "" && textBox3.Text != "" && textBox4.Text != "" && textBox5.Text != "" && textBox6.Text != "")
            {
                double price, BuyDiscount, NormalIncrease, Categorical_Increase,VAT;
                if (double.TryParse(textBox2.Text, out price)
                 &&
                 double.TryParse(textBox4.Text, out BuyDiscount)
                 &&
                 double.TryParse(textBox5.Text, out NormalIncrease)
                 &&
                 double.TryParse(textBox3.Text, out Categorical_Increase)
                 &&
                 double.TryParse(textBox6.Text, out VAT))
                {
                    BuyPrice = price + NormalIncrease;
                    BuyPrice -= BuyDiscount;
                    BuyPrice += Categorical_Increase;
                    BuyPrice += VAT;
                    label13.Text = BuyPrice.ToString();
                }
            }
            else
            {
                label13.Text = "";
            }
        }

        private void comboBox2_SelectedValueChanged(object sender, EventArgs e)
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

                   
                    comboBox3.Visible = true;
                    string query = "select distinct Permission_Number  from storage where Supplier_Name='" + comboBox2.Text+ "' order by Permission_Number DESC ";
                    MySqlDataAdapter da = new MySqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    comboBox3.DataSource = dt;
                    comboBox3.DisplayMember = dt.Columns["Permission_Number"].ToString();
                    comboBox3.Text = "";
                    loaded2 = true;
                    NewBill();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                conn.Close();
            }
        }

        private void comboBox3_SelectedValueChanged(object sender, EventArgs e)
        {
           
            if (loaded2)
            {

                try
                {
                    conn.Close();
                    conn.Open();
                    NewBill();
                    string q = "select distinct storage.Code as 'كود', type.Type_Name as 'نوع',factory.Factory_Name as 'المصنع',groupo.Group_Name as 'المجموعه',product.Product_Name as 'المنتج', storage.Total_Meters as 'اجمالي عدد الامتار',price.Price as 'السعر', price.Normal_Increase as 'الزيادة العادية',price.Buy_Discount as 'خصم الشراء',price.Categorical_Increase as 'الزيادة القطعية' from storage INNER JOIN data  ON storage.Code = data.Code INNER JOIN type ON type.Type_ID = data.Type_ID INNER JOIN product ON product.Product_ID = data.Product_ID INNER JOIN factory ON data.Factory_ID = factory.Factory_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID INNER JOIN price ON storage.Code = price.Code where storage.Supplier_Name='" + comboBox2.Text + "' and  storage.Permission_Number=" + comboBox3.Text + "";
                    MySqlDataAdapter da = new MySqlDataAdapter(q, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dataGridView1.DataSource = dt;

                   
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
            textBox1.Text = textBox2.Text = textBox3.Text = textBox4.Text = textBox5.Text  = textBox7.Text = "";
            textBox6.Text = "0";
            label1.Text = label13.Text = label15.Text = "";
        }
    }
}

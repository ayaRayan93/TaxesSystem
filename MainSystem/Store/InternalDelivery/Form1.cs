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
    public partial class Form1 : Form
    {
        MySqlConnection conn;
        int[] courrentIDs;
        int count = 0;
        int sum;

        public Form1()
        {
            InitializeComponent();
            courrentIDs = new int[100];
            conn = new MySqlConnection(connection.connectionString);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                string query = "select * from type";
                MySqlDataAdapter da = new MySqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                comboBox1.DataSource = dt;               
                comboBox1.DisplayMember = dt.Columns["Type_Name"].ToString();
                comboBox1.ValueMember = dt.Columns["Type_ID"].ToString();
                comboBox1.Text = "";
                textBox1.Text = "";

                query = "select * from factory";
                 da = new MySqlDataAdapter(query, conn);
                 dt = new DataTable();
                da.Fill(dt);
                comboBox2.DataSource = dt;             
                comboBox2.DisplayMember = dt.Columns["Factory_Name"].ToString();
                comboBox2.ValueMember = dt.Columns["Factory_ID"].ToString();
                comboBox2.Text = "";
                textBox2.Text = "";

                query = "select * from groupo";
                da = new MySqlDataAdapter(query, conn);
                dt = new DataTable();
                da.Fill(dt);
                comboBox4.DataSource = dt;
                comboBox4.DisplayMember = dt.Columns["Group_Name"].ToString();
                comboBox4.ValueMember = dt.Columns["Group_ID"].ToString();
                comboBox4.Text = "";
                textBox4.Text = "";

                query = "select * from product";
                da = new MySqlDataAdapter(query, conn);
                dt = new DataTable();
                da.Fill(dt);
                comboBox3.DataSource = dt;
                comboBox3.DisplayMember = dt.Columns["Product_Name"].ToString();
                comboBox3.ValueMember = dt.Columns["Product_ID"].ToString();
                comboBox3.Text = "";
                textBox3.Text = "";

                query = "select * from store";
                da = new MySqlDataAdapter(query, conn);
                dt = new DataTable();
                da.Fill(dt);
                comboBox6.DataSource = dt;
                comboBox6.DisplayMember = dt.Columns["Store_Name"].ToString();
                comboBox6.ValueMember = dt.Columns["Store_ID"].ToString();
                comboBox6.Text = "";

                query = "select * from supplier";
                da = new MySqlDataAdapter(query, conn);
                dt = new DataTable();
                da.Fill(dt);
                comboBox5.DataSource = dt;
                comboBox5.DisplayMember = dt.Columns["Supplier_Name"].ToString();
                comboBox5.ValueMember = dt.Columns["Supplier_ID"].ToString();
                comboBox5.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            conn.Close();
        }

        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            textBox1.Text = comboBox1.SelectedValue.ToString();
        }
        private void comboBox2_SelectedValueChanged(object sender, EventArgs e)
        {
            textBox2.Text = comboBox2.SelectedValue.ToString();
        }
        private void comboBox3_SelectedValueChanged(object sender, EventArgs e)
        {
            textBox3.Text = comboBox3.SelectedValue.ToString();
        }
        private void comboBox4_SelectedValueChanged(object sender, EventArgs e)
        {
            textBox4.Text = comboBox4.SelectedValue.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string q1, q2, q3, q4;
            if (textBox1.Text == "")
            {
                q1 = "select Type_ID from data";
            }
            else
            {
                q1 = textBox1.Text;
            }
            if (textBox2.Text == "")
            {
                q2 = "select Factory_ID from data";
            }
            else
            {
                q2 = textBox2.Text;
            }
            if (textBox3.Text == "")
            {
                q3 = "select Product_ID from data";
            }
            else
            {
                q3 = textBox3.Text;
            }
            if (textBox4.Text == "")
            {
                q4 = "select Group_ID from data";
            }
            else
            {
                q4 = textBox4.Text;
            }

            //string query = "select Colour as 'لون', Size as 'حجم', Sort as 'الفرز', Description as 'الوصف', Type_Name as 'النوع', Factory_Name as 'المصنع' ,Group_Name as 'المجموعة', Product_Name as 'المنتج' from data , type,factory,groupo,product where data.Type_ID=type.Type_ID and Type_ID IN(" + q1 + ") and data.Factory_ID=factory.Factory_ID and Factory_ID  IN(" + q2 + ") and data.Group_ID=groupo.Group_ID and Group_ID IN (" + q4 + ") and data.Product_ID=product.Product_ID and Product_ID IN (" + q3 + ")";
            string query = "select data.Code as 'كود' , data.Colour as 'لون', data.Size as 'حجم', data.Sort as 'الفرز', data.Description as 'الوصف', type.Type_Name as 'النوع', factory.Factory_Name as 'المصنع' ,groupo.Group_Name as 'المجموعة', product.Product_Name as 'المنتج' from data INNER JOIN type ON type.Type_ID = data.Type_ID INNER JOIN product ON product.Product_ID = data.Product_ID INNER JOIN factory ON data.Factory_ID = factory.Factory_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID   where data.Type_ID IN(" + q1 + ") and  data.Factory_ID  IN(" + q2 + ") and data.Group_ID IN (" + q4 + ") and data.Product_ID IN (" + q3 + ")";

            MySqlDataAdapter da = new MySqlDataAdapter(query, conn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }
        
        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode==Keys.Enter)
            {
                try
                {

                    if (textBox1.Text != "")
                    {
                        conn.Open();
                        string query = "select Type_Name from type where Type_ID='" + textBox1.Text + "'";
                        MySqlCommand com = new MySqlCommand(query, conn);
                        string TypeName = (string)com.ExecuteScalar();
                        comboBox1.Text = TypeName;
                        textBox2.Focus();
                    }
                   
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                conn.Close();
            }
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {

                    if (textBox2.Text != "")
                    {
                        conn.Open();
                        string query = "select Factory_Name from factory where Factory_ID='" + textBox2.Text + "'";
                        MySqlCommand com = new MySqlCommand(query, conn);
                        string TypeName = (string)com.ExecuteScalar();
                        comboBox2.Text = TypeName;
                        textBox4.Focus();
                    }
                    
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                conn.Close();
            }
        }

        private void textBox4_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode==Keys.Enter)
            {
                try
                {

                    if (textBox4.Text != "")
                    {
                        conn.Open();
                        string query = "select Group_Name from groupo where Group_ID='" + textBox4.Text + "'";
                        MySqlCommand com = new MySqlCommand(query, conn);
                        string TypeName = (string)com.ExecuteScalar();
                        comboBox4.Text = TypeName;
                        textBox3.Focus();
                    }
                    
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                conn.Close();
            }
        }

        private void textBox3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode==Keys.Enter)
            {
                try
                {

                    if (textBox3.Text != "")
                    {
                        conn.Open();
                        string query = "select Product_Name from product where Product_ID='" + textBox3.Text + "'";
                        MySqlCommand com = new MySqlCommand(query, conn);
                        string TypeName = (string)com.ExecuteScalar();
                        comboBox3.Text = TypeName;
                        textBox1.Focus();
                    }
                    
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                conn.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                //string q1 = "ALTER TABLE Storage ADD FOREIGN KEY (Data_ID) REFERENCES data (Data_ID) ON DELETE CASCADE ON UPDATE CASCADE";
                //MySqlCommand com1 = new MySqlCommand(q1, conn);
                //com1.ExecuteNonQuery();
                
                if (textBox5.Text != "" && textBox6.Text != "" && textBox7.Text != "" && textBox9.Text != "")
                {
                    string code = textBox9.Text;
                    int StoreID = int.Parse(comboBox6.SelectedValue.ToString());
                    string q = "select carton from data where Code='" + code + "'";
                    MySqlCommand com = new MySqlCommand(q, conn);
                    double carton = double.Parse(com.ExecuteScalar().ToString());
                    int NoBalatat;
                    int.TryParse(textBox6.Text, out NoBalatat);
                    int NoCartons;
                    int.TryParse(textBox5.Text, out NoCartons);
                    double total = carton * NoBalatat * NoCartons;
                    label12.Text = (total).ToString();

                    if (int.Parse(txtPermissionNum.Text) <= sum)
                    {
                        string query = "insert into Storage (Store_ID,Store_Name,Storage_Date,Balatat,Carton_Balata,Code,Store_Place,Total_Meters,Supplier_Name,Note,Permission_Number) values (@Store_ID,@Store_Name,@Date,@NoBalatat,@NoCartonInBalata,@CodeOfProduct,@PlaceOfStore,@TotalOfMeters,@Supplier,@Note,@Permission_Number)";
                        com = new MySqlCommand(query, conn);
                        com.Parameters.Add("@Store_ID", MySqlDbType.Int16);
                        com.Parameters["@Store_ID"].Value = StoreID;
                        com.Parameters.Add("@Store_Name", MySqlDbType.VarChar);
                        com.Parameters["@Store_Name"].Value = comboBox6.Text;
                        com.Parameters.Add("@Date", MySqlDbType.Date, 0);
                        com.Parameters["@Date"].Value = dateTimePicker1.Value;
                        com.Parameters.Add("@NoBalatat", MySqlDbType.Int16);
                        com.Parameters["@NoBalatat"].Value = NoBalatat;
                        com.Parameters.Add("@NoCartonInBalata", MySqlDbType.Int16);
                        com.Parameters["@NoCartonInBalata"].Value = NoCartons;
                        com.Parameters.Add("@CodeOfProduct", MySqlDbType.VarChar);
                        com.Parameters["@CodeOfProduct"].Value = textBox9.Text;
                        com.Parameters.Add("@PlaceOfStore", MySqlDbType.VarChar);
                        com.Parameters["@PlaceOfStore"].Value = textBox7.Text;
                        com.Parameters.Add("@TotalOfMeters", MySqlDbType.Decimal);
                        com.Parameters["@TotalOfMeters"].Value = total;
                        com.Parameters.Add("@Supplier", MySqlDbType.VarChar);
                        com.Parameters["@Supplier"].Value = comboBox5.Text;
                        com.Parameters.Add("@Note", MySqlDbType.VarChar);
                        com.Parameters["@Note"].Value = textBox8.Text;
                        com.Parameters.Add("@Permission_Number", MySqlDbType.Int16);
                        com.Parameters["@Permission_Number"].Value = int.Parse(txtPermissionNum.Text);
                        com.ExecuteNonQuery();
                        

                        string q1 = "select Storage_ID from storage ORDER BY Storage_ID DESC LIMIT 1";
                        MySqlCommand comm = new MySqlCommand(q1, conn);
                        int id = (int)comm.ExecuteScalar();

                        courrentIDs[count] = id;
                        count++;

                        string str = "";
                        for (int i = 0; i < courrentIDs.Length - 1; i++)
                        {
                            if (courrentIDs[i] != 0)
                            {
                                str += courrentIDs[i] + ",";
                            }
                        }
                        str += courrentIDs[courrentIDs.Length - 1];

                        string qq = "select storage.Code as 'كود',type.Type_Name as 'النوع', factory.Factory_Name as 'المصنع',groupo.Group_Name as 'المجموعة',product.Product_Name as 'المنتج', storage.Store_Name as 'المخزن', storage.Supplier_Name as 'المورد',storage.Balatat as 'بلتات', storage.Carton_Balata as 'عدد الكراتين',storage.Total_Meters as 'اجمالي عدد الامتار', storage.Storage_Date as 'تاريخ التخزين' , storage.Store_Place as 'مكان التخزين'  , storage.Note as 'ملاحظة',storage.Permission_Number as 'اذن المخزن' from storage INNER JOIN data  ON storage.Code = data.Code INNER JOIN type ON type.Type_ID = data.Type_ID INNER JOIN product ON product.Product_ID = data.Product_ID INNER JOIN factory ON data.Factory_ID = factory.Factory_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID where Storage_ID in (" + str + ") ";
                        MySqlDataAdapter da = new MySqlDataAdapter(qq, conn);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        dataGridView2.DataSource = dt;
                        dataGridView2.FirstDisplayedScrollingRowIndex = dataGridView2.RowCount - 1;


                        MessageBox.Show("Add success");
                    }
                    else
                    {
                        MessageBox.Show("please enter permission number less than or equal to " + sum);
                    }
                }
                else
                {
                    MessageBox.Show("you must fill all fields please");
                    conn.Close();
                    return;
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            conn.Close();
        }
        
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row  =  dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex];
            string v = row.Cells[0].Value.ToString();
            textBox9.Text = v;

            textBox5.Text = "";
            textBox6.Text = "";
            textBox8.Text = "";
            textBox7.Text = "";
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                string code = textBox9.Text;
                int StoreID = int.Parse(comboBox6.SelectedValue.ToString());
                string q = "select carton from data where Code='" + code + "'";
                MySqlCommand com = new MySqlCommand(q, conn);
                double carton = double.Parse(com.ExecuteScalar().ToString());
                int NoBalatat;
                int.TryParse(textBox6.Text, out NoBalatat);
                int NoCartons;
                int.TryParse(textBox5.Text, out NoCartons);
                double total = carton * NoBalatat * NoCartons;
                label12.Text = (total).ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            conn.Close();
        }
        
        private void comboBox5_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode==Keys.Enter)
            {
                label15.Visible = true;
                txtPermissionNum.Visible = true;
                //MessageBox.Show(comboBox5.Text.ToString());
                string q = "select Permission_Number from storage where Supplier_Name='" + comboBox5.Text + "' ORDER BY Storage_ID DESC LIMIT 1 ";
                conn.Open();
                MySqlCommand com = new MySqlCommand(q, conn);
                try
                {
                    int r = int.Parse(com.ExecuteScalar().ToString());
                    sum = r + 1;
                    txtPermissionNum.Text = sum.ToString();
                    conn.Close();
                }
                catch
                {
                    label15.Visible = true;
                    txtPermissionNum.Visible = true;
                    txtPermissionNum.Text = "1";
                    conn.Close();
                }
            }
        }

        private void txtPermissionNum_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string q = "select storage.Code as 'كود',type.Type_Name as 'النوع', factory.Factory_Name as 'المصنع',groupo.Group_Name as 'المجموعة',product.Product_Name as 'المنتج', storage.Store_Name as 'المخزن', storage.Supplier_Name as 'المورد',storage.Balatat as 'بلتات', storage.Carton_Balata as 'عدد الكراتين',storage.Total_Meters as 'اجمالي عدد الامتار', storage.Storage_Date as 'تاريخ التخزين' , storage.Store_Place as 'مكان التخزين'  , storage.Note as 'ملاحظة',storage.Permission_Number as 'اذن المخزن' from storage INNER JOIN data  ON storage.Code = data.Code INNER JOIN type ON type.Type_ID = data.Type_ID INNER JOIN product ON product.Product_ID = data.Product_ID INNER JOIN factory ON data.Factory_ID = factory.Factory_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID where Permission_Number=" + txtPermissionNum.Text + " and Supplier_Name='" + comboBox5.Text + "'";
                try
                {
                    MySqlDataAdapter da = new MySqlDataAdapter(q, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dataGridView2.DataSource = dt;
                    textBox9.Focus();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}

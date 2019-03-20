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

namespace CCCRequestsSystem
{
    public partial class Order_Record : Form
    {
        MySqlConnection dbconnection;
        bool loaded = false;
        DataGridViewRow row;
        private int[] addedRecordIDs;
        int recordCount = 0;
        private bool Added = false;
        int id;

        public Order_Record()
        {
            InitializeComponent();
            dbconnection = new MySqlConnection(connection.connectionString);
            addedRecordIDs = new int[100];
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
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

                query = "select * from supplier";
                da = new MySqlDataAdapter(query, dbconnection);
                dt = new DataTable();
                da.Fill(dt);
                comSupplier.DataSource = dt;
                comSupplier.DisplayMember = dt.Columns["Supplier_Name"].ToString();
                comSupplier.ValueMember = dt.Columns["Supplier_ID"].ToString();
                comSupplier.Text = "";

                query = "select * from branch";
                da = new MySqlDataAdapter(query, dbconnection);
                dt = new DataTable();
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

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }
        
        private void comBox_SelectedValueChanged(object sender, EventArgs e)
        {
            if (loaded)
            {
                ComboBox comBox = (ComboBox)sender;

                switch (comBox.Name)
                {
                    case "comType":
                        txtType.Text = comType.SelectedValue.ToString();
                        break;
                    case "comFactory":
                        txtFactory.Text = comFactory.SelectedValue.ToString();
                        break;
                    case "comGroup":
                        txtGroup.Text = comGroup.SelectedValue.ToString();
                        break;
                    case "comProduct":
                        txtProduct.Text = comProduct.SelectedValue.ToString();
                        break;
                    
                    case "comSupplier":
                        txtSupplier.Text = comSupplier.SelectedValue.ToString();
                        break;
                    case "comStore":
                        txtStoreID.Text = comStore.SelectedValue.ToString();
                        break;

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
                            case "txtType":
                                query = "select Type_Name from type where Type_ID='" + txtType.Text + "'";
                                com = new MySqlCommand(query, dbconnection);
                                if (com.ExecuteScalar() != null)
                                {
                                    Name = (string)com.ExecuteScalar();
                                    comType.Text = Name;
                                    txtFactory.Focus();
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
                                }
                                else
                                {
                                    MessageBox.Show("there is no item with this id");
                                    dbconnection.Close();
                                    return;
                                }
                                break;

                            case "txtSupplier":
                                query = "select Supplier_Name from supplier where Supplier_ID=" + txtSupplier.Text + "";
                                com = new MySqlCommand(query, dbconnection);
                                if (com.ExecuteScalar().ToString() != "")
                                {
                                    Name = (string)com.ExecuteScalar();
                                    comSupplier.Text = Name;
                                    comSupplier.SelectedValue = txtSupplier.Text;
                                }
                                else
                                {
                                    MessageBox.Show("there is no item with this id");
                                    dbconnection.Close();
                                    return;
                                }
                                break;
                                
                            case "txtBranchID":
                                query = "select Branch_Name from branch where Branch_ID=" + txtBranchID.Text + "";
                                com = new MySqlCommand(query, dbconnection);
                                if (com.ExecuteScalar().ToString() != "")
                                {
                                    Name = (string)com.ExecuteScalar();
                                    comBranch.Text = Name;
                                    comBranch.SelectedValue = txtBranchID.Text;
                                }
                                else
                                {
                                    MessageBox.Show("there is no item with this id");
                                    dbconnection.Close();
                                    return;
                                }
                                break;
                            case "txtStoreID":
                                query = "select Store_Name from store where Store_ID=" + txtStoreID.Text + "";
                                com = new MySqlCommand(query, dbconnection);
                                if (com.ExecuteScalar().ToString() != "")
                                {
                                    Name = (string)com.ExecuteScalar();
                                    comStore.Text = Name;
                                    comStore.SelectedValue = txtStoreID.Text;
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
        private void btnSearch_Click(object sender, EventArgs e)
        {
            string q1, q2, q3, q4;
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

            string query = "select distinct data.Code as 'كود' , type.Type_Name as 'النوع', factory.Factory_Name as 'المصنع' ,groupo.Group_Name as 'المجموعة', product.Product_Name as 'المنتج' ,data.Colour as 'لون', data.Size as 'المقاس', data.Sort as 'الفرز',data.Classification as 'التصنيف', data.Description as 'الوصف'  from data INNER JOIN type ON type.Type_ID = data.Type_ID INNER JOIN product ON product.Product_ID = data.Product_ID INNER JOIN factory ON data.Factory_ID = factory.Factory_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID  where  data.Type_ID IN(" + q1 + ") and  data.Factory_ID  IN(" + q2 + ") and data.Group_ID IN (" + q4 + ") ";

            MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            row = dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex];

            string value = row.Cells[0].Value.ToString();
            txtCode.Text = value;
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!Added && row != null && txtTotalMeters.Text != "")
            {
                addedRecordIDs[recordCount] = dataGridView1.SelectedCells[0].RowIndex + 1;
                dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex].DefaultCellStyle.BackColor = Color.Silver;
                recordCount++;
                int n = dataGridView2.Rows.Add();
                foreach (DataGridViewColumn item in dataGridView1.Columns)
                {
                    dataGridView2.Rows[n].Cells[item.Index].Value = dataGridView1.Rows[row.Index].Cells[item.Index].Value.ToString();

                }
                dataGridView2.Rows[n].Cells[10].Value = txtTotalMeters.Text;


                //clear fields
                txtCode.Text = txtTotalMeters.Text = "";
            }
            else
            {
                MessageBox.Show("insert all values with correct format");
                return;
            }
        }
        private void btnConfirm_Click(object sender, EventArgs e)
        {
            int branchId = 0;
            if (txtEmployee.Text != "" && comBranch.Text != "" && int.TryParse(txtBranchID.Text, out branchId) && comStore.Text != "" && txtStoreID.Text != "" && txtSupplier.Text != "")
            {
                dbconnection.Open();

                int branchBillNumber = 1;
                string query = "select BranchBillNumber from orders where Branch_ID=" + branchId + " order by BranchBillNumber desc limit 1";
                MySqlCommand com = new MySqlCommand(query, dbconnection);
                if (com.ExecuteScalar() != null)
                {
                    branchBillNumber = Convert.ToInt16(com.ExecuteScalar()) + 1;
                }
                try
                {
                    query = "insert into orders (Supplier_ID,Supplier_Name,Store_ID,Store_Name,Branch_ID,Branch_Name,BranchBillNumber,Employee_Name,Request_Date,Receive_Date)values(@Supplier_ID,@Supplier_Name,@Store_ID,@Store_Name,@Branch_ID,@Branch_Name,@BranchBillNumber,@Employee_Name,@Request_Date,@Recive_Date)";
                    com = new MySqlCommand(query, dbconnection);

                    com.Parameters.Add("@Employee_Name", MySqlDbType.VarChar);
                    com.Parameters["@Employee_Name"].Value = txtEmployee.Text;
                    com.Parameters.Add("@Request_Date", MySqlDbType.Date);
                    com.Parameters["@Request_Date"].Value = dateTimePicker1.Value.Date;
                    com.Parameters.Add("@Recive_Date", MySqlDbType.Date);
                    com.Parameters["@Recive_Date"].Value = dateTimePicker2.Value.Date;
                    int res2;
                    if (int.TryParse(txtSupplier.Text, out res2))
                    {
                        com.Parameters.Add("@Supplier_ID", MySqlDbType.Int16);
                        com.Parameters["@Supplier_ID"].Value = res2;
                    }
                    else
                    {
                        MessageBox.Show("insert correct value please :)");
                        dbconnection.Close();
                        return;
                    }
                    com.Parameters.Add("@Supplier_Name", MySqlDbType.VarChar);
                    com.Parameters["@Supplier_Name"].Value = comSupplier.Text;
                    int res1;
                    if (int.TryParse(txtStoreID.Text, out res1))
                    {
                        com.Parameters.Add("@Store_ID", MySqlDbType.Int16);
                        com.Parameters["@Store_ID"].Value = res1;
                    }
                    else
                    {
                        MessageBox.Show("insert correct value please :)");
                        dbconnection.Close();
                        return;
                    }
                    com.Parameters.Add("@Store_Name", MySqlDbType.VarChar);
                    com.Parameters["@Store_Name"].Value = comStore.Text;
                    int res;
                    if (int.TryParse(txtBranchID.Text, out res))
                    {
                        com.Parameters.Add("@Branch_ID", MySqlDbType.Int16);
                        com.Parameters["@Branch_ID"].Value = res;
                    }
                    else
                    {
                        MessageBox.Show("insert correct value please :)");
                        dbconnection.Close();
                        return;
                    }
                    com.Parameters.Add("@Branch_Name", MySqlDbType.VarChar);
                    com.Parameters["@Branch_Name"].Value = comBranch.Text;
                    com.Parameters.Add("@BranchBillNumber", MySqlDbType.Int16);
                    com.Parameters["@BranchBillNumber"].Value = branchBillNumber;

                    com.ExecuteNonQuery();

                    query = "select Order_ID from orders order by Order_ID desc limit 1";
                    com = new MySqlCommand(query, dbconnection);

                    if (com.ExecuteScalar() != null)
                    {
                        id = (int)com.ExecuteScalar();


                        foreach (DataGridViewRow row in dataGridView2.Rows)
                        {

                            query = "insert into order_details (Order_ID,Code,Quantity) values (@Order_ID,@Code,@Quantity)";
                            com = new MySqlCommand(query, dbconnection);

                            com.Parameters.Add("@Order_ID", MySqlDbType.Int16);
                            com.Parameters["@Order_ID"].Value = id;
                            com.Parameters.Add("@Code", MySqlDbType.VarChar);
                            com.Parameters["@Code"].Value = row.Cells["Code"].Value;
                            com.Parameters.Add("@Quantity", MySqlDbType.Double);
                            com.Parameters["@Quantity"].Value = row.Cells["Total_Meter"].Value;


                            com.ExecuteNonQuery();

                        }
                        MessageBox.Show("Done");
                        dbconnection.Close();

                        //لعرض التقرير
                        if (txtEmployee.Text != "" && comSupplier.Text != "")
                        {
                            DataTable dt = new DataTable();
                            for (int i = 0; i < dataGridView2.Columns.Count; i++)
                            {
                                dt.Columns.Add(dataGridView2.Columns[i].Name.ToString());
                            }
                            for (int i = 0; i < dataGridView2.Rows.Count; i++)
                            {
                                DataRow dr = dt.NewRow();
                                for (int j = 0; j < dataGridView2.Columns.Count; j++)
                                {
                                    dr[dataGridView2.Columns[j].Name.ToString()] = dataGridView2.Rows[i].Cells[j].Value;
                                }

                                dt.Rows.Add(dr);
                            }

                            /*OrderRecord_CrystalReport f = new OrderRecord_CrystalReport(comStore.Text, comBranch.Text, branchBillNumber, dateTimePicker1.Value.Date, dateTimePicker2.Value.Date, txtEmployee.Text, comSupplier.Text, dt);
                            f.Show();
                            this.Hide();*/
                        }
                        else
                        {
                            MessageBox.Show("fill requird fields");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                dbconnection.Close();
            }
            else
            {
                MessageBox.Show("fill requird fields");
            }
        }

        private void btnMainForm_Click(object sender, EventArgs e)
        {
            //MainForm f = new MainForm();
            //f.Show();
            //this.Hide();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
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

        private void comBranch_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (loaded)
                    txtBranchID.Text = comBranch.SelectedValue.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }
    }
    public static class connection
    {
        public static string connectionString = "SERVER=192.168.1.200;DATABASE=testprice;user=root;PASSWORD=root;CHARSET=utf8;SslMode=none";
      //public static string connectionString = "SERVER=localhost;DATABASE=ccc;user=root;PASSWORD=root;CHARSET=utf8";
    }
}

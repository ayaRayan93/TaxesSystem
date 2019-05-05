using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraTab;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MainSystem
{
    public partial class SetRecord : Form
    {
        MySqlConnection dbconnection;
        XtraTabControl xtraTabControlStoresContent = null;
        DataGridViewRow row1 = null, row2 = null;
        bool loaded=false;
        bool factoryFlage = false;
        bool groupFlage = false;
        bool flagProduct = false;
        public static TipImage tipImage = null;
        Ataqm ataqm = null;
        byte[] selectedImage = null;

        public SetRecord(Ataqm ataqm, XtraTabControl xtraTabControlStoresContent)
        {
            try
            {
                InitializeComponent();
                this.ataqm = ataqm;
                this.xtraTabControlStoresContent = xtraTabControlStoresContent;
                dbconnection = new MySqlConnection(connection.connectionString);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }
        private void SetRecord_Load(object sender, EventArgs e)
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
                
                loaded = true;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
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

                    //if (chBoxSelectAll.Checked)
                    //    displayProducts();
                    //else
                    //    displayAllProducts();
                    if (txtType.Text != "1")
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
                    dbconnection.Open();
                    txtGroup.Text = comGroup.SelectedValue.ToString();
                    comProduct.Focus();
                    filterProduct();
                    //if (chBoxSelectAll.Checked)
                    //    displayProducts();
                    //else
                    //    displayAllProducts();
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
                    dbconnection.Open();
                    txtFactory.Text = comFactory.SelectedValue.ToString();
                    comGroup.Focus();
                    filterGroup();
                    filterProduct();
                    //if (chBoxSelectAll.Checked)
                    //    displayProducts();
                    //else
                    //    displayAllProducts();
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
                    dbconnection.Open();
                    txtProduct.Text = comProduct.SelectedValue.ToString();
                    comType.Focus();
                    //if (chBoxSelectAll.Checked)
                    //    displayProducts();
                    //else
                    //    displayAllProducts();
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
                                    filterFactory();

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
                    //MessageBox.Show(ex.ToString());
                }
                dbconnection.Close();
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                displayProducts();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        
        }
        private void btnNewChooes_Click(object sender, EventArgs e)
        {
            foreach (Control item in panel1.Controls)
            {
                if (item is TextBox)
                    item.Text = "";
                else if (item is ComboBox)
                {
                    item.Text = "";
                }

            }
            dataGridView1.DataSource = null;
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                row1 = dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex];
                row1.Selected = true;
                txtCode.Text = row1.Cells["Code"].Value.ToString();
                if (e.ColumnIndex == 1)
                {
                    if (tipImage == null)
                    {
                        tipImage = new TipImage(row1.Cells[1].Value.ToString());
                        tipImage.Location = new Point(Cursor.Position.X, Cursor.Position.Y);
                        tipImage.Show();
                    }
                    else
                    {
                        tipImage.Close();
                        tipImage = new TipImage(row1.Cells[1].Value.ToString());
                        tipImage.Location = new Point(Cursor.Position.X, Cursor.Position.Y);
                        tipImage.Show();
                    }
                }
                else
                {
                    if (tipImage != null)
                    {
                        tipImage.Hide();
                    }
                }
            }
            catch (Exception ex)
            {
                // MessageBox.Show(ex.Message);
            }
        }
        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                row2 = dataGridView2.Rows[dataGridView2.SelectedCells[0].RowIndex];
                row2.Selected = true;
                if (e.ColumnIndex == 1)
                {
                    if (tipImage == null)
                    {
                        tipImage = new TipImage(row2.Cells[1].Value.ToString());
                        tipImage.Location = new Point(Cursor.Position.X, Cursor.Position.Y);
                        tipImage.Show();
                    }
                    else
                    {
                        tipImage.Close();
                        tipImage = new TipImage(row2.Cells[1].Value.ToString());
                        tipImage.Location = new Point(Cursor.Position.X, Cursor.Position.Y);
                        tipImage.Show();
                    }
                }
                else
                {
                    if (tipImage != null)
                    {
                        tipImage.Hide();
                    }
                }
            }
            catch (Exception ex)
            {
                // MessageBox.Show(ex.Message);
            }
        }
        private void btnPut_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView2.Rows.Count > 0)
                {
                    DataGridViewRow temp = dataGridView2.Rows[0];
                    if (row1 != null)
                    {
                        if (row1.Cells[2].Value.ToString() == temp.Cells[3].Value.ToString() && row1.Cells[3].Value.ToString() == temp.Cells[4].Value.ToString() && row1.Cells[5].Value.ToString() == temp.Cells[6].Value.ToString())
                        {
                            int n = dataGridView2.Rows.Add();
                            dataGridView2.Rows[n].Cells[0].Value = row1.Cells[0].Value;
                            dataGridView2.Rows[n].Cells[1].Value = row1.Cells[1].Value;
                            dataGridView2.Rows[n].Cells[2].Value = txtQuantity.Text;
                            dataGridView2.Rows[n].Cells[3].Value = row1.Cells[2].Value;
                            dataGridView2.Rows[n].Cells[4].Value = row1.Cells[3].Value;
                            dataGridView2.Rows[n].Cells[5].Value = row1.Cells[4].Value;
                            dataGridView2.Rows[n].Cells[6].Value = row1.Cells[5].Value;
                            dataGridView2.Rows[n].Cells[7].Value = row1.Cells[6].Value;
                            dataGridView2.Rows[n].Cells[8].Value = row1.Cells[7].Value;
                            dataGridView2.Rows[n].Cells[9].Value = row1.Cells[8].Value;
                            dataGridView2.Rows[n].Cells[10].Value = row1.Cells[9].Value;
                            dataGridView2.Rows[n].Cells[11].Value = row1.Cells[10].Value;
                            dataGridView2.Rows[n].Cells[12].Value = row1.Cells[11].Value;
                            dataGridView1.Rows.Remove(row1);
                            row1 = null;
                        }
                    }
                    else
                    {
                        MessageBox.Show("select row");
                    }
                }
                else
                {
                    if (row1 != null)
                    {
                        int n = dataGridView2.Rows.Add();
                        dataGridView2.Rows[n].Cells[0].Value = row1.Cells[0].Value;
                        dataGridView2.Rows[n].Cells[1].Value = row1.Cells[1].Value;
                        dataGridView2.Rows[n].Cells[2].Value = txtQuantity.Text;
                        dataGridView2.Rows[n].Cells[3].Value = row1.Cells[2].Value;
                        dataGridView2.Rows[n].Cells[4].Value = row1.Cells[3].Value;
                        dataGridView2.Rows[n].Cells[5].Value = row1.Cells[4].Value;
                        dataGridView2.Rows[n].Cells[6].Value = row1.Cells[5].Value;
                        dataGridView2.Rows[n].Cells[7].Value = row1.Cells[6].Value;
                        dataGridView2.Rows[n].Cells[8].Value = row1.Cells[7].Value;
                        dataGridView2.Rows[n].Cells[9].Value = row1.Cells[8].Value;
                        dataGridView2.Rows[n].Cells[10].Value = row1.Cells[9].Value;
                        dataGridView2.Rows[n].Cells[11].Value = row1.Cells[10].Value;
                        dataGridView2.Rows[n].Cells[12].Value = row1.Cells[11].Value;
                        dataGridView1.Rows.Remove(row1);
                        row1 = null;                      
                    }
                    else
                    {
                        MessageBox.Show("select row");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    if (row1 != null)
                    {
                     
                        txtQuantity.Focus();

                    }
                    else
                    {
                        MessageBox.Show("select row");
                    }
                }
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
                if (row2 != null)
                {
                    int n = dataGridView1.Rows.Add();
                    dataGridView1.Rows[n].Cells[0].Value = row2.Cells[0].Value;
                    dataGridView1.Rows[n].Cells[1].Value = row2.Cells[1].Value;
                    dataGridView1.Rows[n].Cells[2].Value = row2.Cells[3].Value;
                    dataGridView1.Rows[n].Cells[3].Value = row2.Cells[4].Value;
                    dataGridView1.Rows[n].Cells[4].Value = row2.Cells[5].Value;
                    dataGridView1.Rows[n].Cells[5].Value = row2.Cells[6].Value;
                    dataGridView1.Rows[n].Cells[6].Value = row2.Cells[7].Value;
                    dataGridView1.Rows[n].Cells[7].Value = row2.Cells[8].Value;
                    dataGridView1.Rows[n].Cells[8].Value = row2.Cells[9].Value;
                    dataGridView1.Rows[n].Cells[9].Value = row2.Cells[10].Value;
                    dataGridView1.Rows[n].Cells[10].Value = row2.Cells[11].Value;
                    dataGridView1.Rows[n].Cells[11].Value = row2.Cells[12].Value;
                    dataGridView2.Rows.Remove(row2);
                    row2 = null;

                }
                else
                {
                    MessageBox.Show("select row");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void dataGridView2_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    if (row2 != null)
                    {
                        int n = dataGridView1.Rows.Add();
                        dataGridView1.Rows[n].Cells[0].Value = row2.Cells[0].Value;
                        dataGridView1.Rows[n].Cells[1].Value = row2.Cells[1].Value;
                        dataGridView1.Rows[n].Cells[2].Value = row2.Cells[3].Value;
                        dataGridView1.Rows[n].Cells[3].Value = row2.Cells[4].Value;
                        dataGridView1.Rows[n].Cells[4].Value = row2.Cells[5].Value;
                        dataGridView1.Rows[n].Cells[5].Value = row2.Cells[6].Value;
                        dataGridView1.Rows[n].Cells[6].Value = row2.Cells[7].Value;
                        dataGridView1.Rows[n].Cells[7].Value = row2.Cells[8].Value;
                        dataGridView1.Rows[n].Cells[8].Value = row2.Cells[9].Value;
                        dataGridView1.Rows[n].Cells[9].Value = row2.Cells[10].Value;
                        dataGridView1.Rows[n].Cells[10].Value = row2.Cells[11].Value;
                        dataGridView1.Rows[n].Cells[11].Value = row2.Cells[12].Value;
                        dataGridView2.Rows.Remove(row2);
                        row2 = null;

                    }
                    else
                    {
                        MessageBox.Show("select row");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btnAddProduct_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView2.Rows.Count > 0)
                {
                    if (txtSetName.Text != "" )
                    {
                        string query = "select Set_Name from sets where Set_Name='" + txtSetName.Text+"'";
                        MySqlCommand comand = new MySqlCommand(query, dbconnection);
                        dbconnection.Open();
                        MySqlDataReader dr = comand.ExecuteReader();
                        while (dr.Read())
                        {
                            if (dr["Set_Name"].ToString() == txtSetName.Text)
                            {
                                MessageBox.Show("this set already exist");
                                dr.Close();
                                dbconnection.Close();
                                return;
                            }
                        }
                        dr.Close();

                      if (selectedImage == null)
                        {
                            query = "INSERT INTO sets (Set_Name,Factory_ID,Type_ID,Group_ID) VALUES (@Set_Name,@Factory_ID,@Type_ID,@Group_ID)";
                            comand = new MySqlCommand(query, dbconnection);
                            comand.Parameters.AddWithValue("@Set_Name", txtSetName.Text);
                            string q = "select Type_ID from type where Type_Name='" + dataGridView2.Rows[0].Cells[3].Value.ToString() + "'";
                            MySqlCommand com = new MySqlCommand(q, dbconnection);
                            comand.Parameters.Add("@Type_ID", MySqlDbType.Int16);
                            comand.Parameters["@Type_ID"].Value = com.ExecuteScalar();

                            q = "select Factory_ID from factory where Factory_Name='" + dataGridView2.Rows[0].Cells[4].Value.ToString() + "'";
                            com = new MySqlCommand(q, dbconnection);
                            comand.Parameters.Add("@Factory_ID", MySqlDbType.Int16);
                            comand.Parameters["@Factory_ID"].Value = com.ExecuteScalar();

                            q = "select Group_ID from groupo where Group_Name='" + dataGridView2.Rows[0].Cells[5].Value.ToString() + "'";
                            com = new MySqlCommand(q, dbconnection);
                            comand.Parameters.Add("@Group_ID", MySqlDbType.Int16);
                            comand.Parameters["@Group_ID"].Value = com.ExecuteScalar();
                            comand.ExecuteNonQuery();
                        }
                        else
                        {
                            query = "INSERT INTO sets (Set_Name,Set_Photo,Factory_ID,Type_ID,Group_ID) VALUES (@Set_Name,@Set_Photo,@Factory_ID,@Type_ID,@Group_ID)";
                            comand = new MySqlCommand(query, dbconnection);

                            comand.Parameters.AddWithValue("@Set_Name", txtSetName.Text);
                            comand.Parameters.Add("@Set_Photo", MySqlDbType.LongBlob);
                            comand.Parameters["@Set_Photo"].Value = selectedImage;

                            string q = "select Type_ID from type where Type_Name='" + dataGridView2.Rows[0].Cells[3].Value.ToString()+"'";
                            MySqlCommand com = new MySqlCommand(q, dbconnection);
                            comand.Parameters.Add("@Type_ID", MySqlDbType.Int16);
                            comand.Parameters["@Type_ID"].Value = com.ExecuteScalar();

                            q = "select Factory_ID from factory where Factory_Name='" + dataGridView2.Rows[0].Cells[4].Value.ToString()+"'";
                            com = new MySqlCommand(q, dbconnection);
                            comand.Parameters.Add("@Factory_ID", MySqlDbType.Int16);
                            comand.Parameters["@Factory_ID"].Value = com.ExecuteScalar();

                            q = "select Group_ID from groupo where Group_Name='" + dataGridView2.Rows[0].Cells[5].Value.ToString()+"'";
                            com = new MySqlCommand(q, dbconnection);
                            comand.Parameters.Add("@Group_ID", MySqlDbType.Int16);
                            comand.Parameters["@Group_ID"].Value = com.ExecuteScalar();
                     

                            comand.ExecuteNonQuery();

                        }

                        query = "select Set_ID from sets order by Set_ID desc limit 1";
                        comand = new MySqlCommand(query, dbconnection);
                        int set_id = Convert.ToInt16(comand.ExecuteScalar().ToString());

                        foreach (DataGridViewRow item in dataGridView2.Rows)
                        {
                            query = "INSERT INTO set_Details (Set_ID,Data_ID,Quantity) VALUES (@Set_ID,@Data_ID,@Quantity)";
                            comand = new MySqlCommand(query, dbconnection);
                            comand.Parameters.AddWithValue("@Set_ID", set_id);
                            comand.Parameters.AddWithValue("@Data_ID", item.Cells[0].Value);
                            comand.Parameters.AddWithValue("@Quantity", double.Parse(item.Cells[2].Value.ToString()));
                            comand.ExecuteNonQuery();
                        }

                        query = "INSERT INTO set_photo (Set_ID,Photo) VALUES (@Set_ID,@Photo)";
                        comand = new MySqlCommand(query, dbconnection);
                        comand.Parameters.AddWithValue("@Set_ID", set_id);
                        comand.Parameters.Add("@Photo", MySqlDbType.LongBlob);
                        comand.Parameters["@Photo"].Value = selectedImage;
                        comand.ExecuteNonQuery();

                        UserControl.ItemRecord("sets", "add",Convert.ToInt16(set_id.ToString()), DateTime.Now,"", dbconnection);

                        MessageBox.Show("Done");
                        clear();
                        ataqm.DisplayAtaqm();
                    }
                    else
                    {
                        MessageBox.Show("Please fill all fields with right format");
                    }
                }
                else
                {
                    MessageBox.Show("Please insert at least one item");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }
        private void txtQuantity_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    if (dataGridView2.Rows.Count > 0)
                    {
                        DataGridViewRow temp = dataGridView2.Rows[0];
                        if (row1 != null)
                        {
                            if (row1.Cells[2].Value.ToString() == temp.Cells[3].Value.ToString() && row1.Cells[3].Value.ToString() == temp.Cells[4].Value.ToString() && row1.Cells[5].Value.ToString() == temp.Cells[6].Value.ToString())
                            {
                                int n = dataGridView2.Rows.Add();
                                dataGridView2.Rows[n].Cells[0].Value = row1.Cells[0].Value;
                                dataGridView2.Rows[n].Cells[1].Value = row1.Cells[1].Value;
                                dataGridView2.Rows[n].Cells[2].Value = txtQuantity.Text;
                                dataGridView2.Rows[n].Cells[3].Value = row1.Cells[2].Value;
                                dataGridView2.Rows[n].Cells[4].Value = row1.Cells[3].Value;
                                dataGridView2.Rows[n].Cells[5].Value = row1.Cells[4].Value;
                                dataGridView2.Rows[n].Cells[6].Value = row1.Cells[5].Value;
                                dataGridView2.Rows[n].Cells[7].Value = row1.Cells[6].Value;
                                dataGridView2.Rows[n].Cells[8].Value = row1.Cells[7].Value;
                                dataGridView2.Rows[n].Cells[9].Value = row1.Cells[8].Value;
                                dataGridView2.Rows[n].Cells[10].Value = row1.Cells[9].Value;
                                dataGridView2.Rows[n].Cells[11].Value = row1.Cells[10].Value;
                                dataGridView2.Rows[n].Cells[12].Value = row1.Cells[11].Value;
                                dataGridView1.Rows.Remove(row1);
                                row1 = null;
                            }
                        }
                        else
                        {
                            MessageBox.Show("select row");
                        }
                    }
                    else
                    {
                        if (row1 != null)
                        {
                            int n = dataGridView2.Rows.Add();
                            dataGridView2.Rows[n].Cells[0].Value = row1.Cells[0].Value;
                            dataGridView2.Rows[n].Cells[1].Value = row1.Cells[1].Value;
                            dataGridView2.Rows[n].Cells[2].Value = txtQuantity.Text;
                            dataGridView2.Rows[n].Cells[3].Value = row1.Cells[2].Value;
                            dataGridView2.Rows[n].Cells[4].Value = row1.Cells[3].Value;
                            dataGridView2.Rows[n].Cells[5].Value = row1.Cells[4].Value;
                            dataGridView2.Rows[n].Cells[6].Value = row1.Cells[5].Value;
                            dataGridView2.Rows[n].Cells[7].Value = row1.Cells[6].Value;
                            dataGridView2.Rows[n].Cells[8].Value = row1.Cells[7].Value;
                            dataGridView2.Rows[n].Cells[9].Value = row1.Cells[8].Value;
                            dataGridView2.Rows[n].Cells[10].Value = row1.Cells[9].Value;
                            dataGridView2.Rows[n].Cells[11].Value = row1.Cells[10].Value;
                            dataGridView2.Rows[n].Cells[12].Value = row1.Cells[11].Value;
                            dataGridView1.Rows.Remove(row1);
                            row1 = null;
                        }
                        else
                        {
                            MessageBox.Show("select row");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void txtBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (loaded)
                {
                    XtraTabPage xtraTabPage = getTabPage("أضافة طقم");
                    if (!IsClear())
                        xtraTabPage.ImageOptions.Image = Properties.Resources.unsave;
                    else
                        xtraTabPage.ImageOptions.Image = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void SetRecord_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                if (tipImage != null)
                {
                    tipImage.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void ImageProduct_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog1 = new OpenFileDialog();
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    string selectedFile = openFileDialog1.FileName;
                    selectedImage = File.ReadAllBytes(selectedFile);
                    ImageProduct.Image = Image.FromFile(openFileDialog1.FileName);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //function
        public void displayProducts()
        {
            try
            {
                dbconnection.Open();
                loaded = false;
                string q1, q2, q3, q4;
                if (txtType.Text == "")
                {
                    q1 = "select Type_ID from type";
                }
                else
                {
                    q1 = txtType.Text;
                }
                if (txtFactory.Text == "")
                {
                    q2 = "select Factory_ID from factory";
                }
                else
                {
                    q2 = txtFactory.Text;
                }
                if (txtProduct.Text == "")
                {
                    q3 = "select Product_ID from product";
                }
                else
                {
                    q3 = txtProduct.Text;
                }
                if (txtGroup.Text == "")
                {
                    q4 = "select Group_ID from groupo";
                }
                else
                {
                    q4 = txtGroup.Text;
                }

                string query = "SELECT data.Data_ID,data.Code,product.Product_Name,type.Type_Name,factory.Factory_Name,groupo.Group_Name,color.Color_Name,size.Size_Value,sort.Sort_Value,data.Classification,data.Description,data.Carton from data INNER JOIN type ON type.Type_ID = data.Type_ID INNER JOIN product ON product.Product_ID = data.Product_ID INNER JOIN factory ON data.Factory_ID = factory.Factory_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID LEFT outer JOIN color ON data.Color_ID = color.Color_ID LEFT outer  JOIN size ON data.Size_ID = size.Size_ID LEFT outer  JOIN sort ON data.Sort_ID = sort.Sort_ID where  data.Type_ID IN(" + q1 + ") and  data.Factory_ID  IN(" + q2 + ") and data.Product_ID IN (" + q3 + ") and data.Group_ID IN (" + q4 + ") group by data.Code";

                MySqlCommand comand = new MySqlCommand(query, dbconnection);
                dataGridView1.Rows.Clear();
                MySqlDataReader dr = comand.ExecuteReader();
                while (dr.Read())
                {
                    int n = dataGridView1.Rows.Add();
                    dataGridView1.Rows[n].Cells["Data_ID"].Value = dr[0];
                    dataGridView1.Rows[n].Cells["Code"].Value = dr[1];
                    dataGridView1.Rows[n].Cells["Product_Name"].Value = dr[2];
                    dataGridView1.Rows[n].Cells["Type_Name"].Value = dr[3];
                    dataGridView1.Rows[n].Cells["Factory_Name"].Value = dr[4];
                    dataGridView1.Rows[n].Cells["Group_Name"].Value = dr[5];
                    dataGridView1.Rows[n].Cells["Colour"].Value = dr[6];
                    dataGridView1.Rows[n].Cells["Size"].Value = dr[7];
                    dataGridView1.Rows[n].Cells["Sort"].Value = dr[8];
                    dataGridView1.Rows[n].Cells["Classification"].Value = dr[9];
                    dataGridView1.Rows[n].Cells["Description"].Value = dr[10];
                    dataGridView1.Rows[n].Cells["Carton"].Value = dr[11];
                }
                dr.Close();
                dataGridView1.Columns[1].Width = 180;
                loaded = true;
                filtterRows();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }
        public void filtterRows()
        {
            if (dataGridView2.Rows.Count > 0)
            {
                foreach (DataGridViewRow item in dataGridView2.Rows)
                {
                    foreach (DataGridViewRow item1 in dataGridView1.Rows)
                    {
                        if (item.Cells["Code2"].Value == item1.Cells["Code"].Value)
                            dataGridView1.Rows.Remove(item1);
                    }

                }
            }
        }
        public XtraTabPage getTabPage(string text)
        {
            for (int i = 0; i < xtraTabControlStoresContent.TabPages.Count; i++)
                if (xtraTabControlStoresContent.TabPages[i].Text == text)
                {
                    return xtraTabControlStoresContent.TabPages[i];
                }
            return null;
        }
        public bool IsClear()
        {
            bool flag = false;

            if (txtCode.Text == "" && txtQuantity.Text == "1" && txtSetName.Text == "")
                flag = true;
            else
                return false;

            if(dataGridView2.Rows.Count == 0)
                    flag = true;
            else
                return false;
           
            return flag;
        }       
        public void clear()
        {
            dataGridView1.Rows.Clear();
            dataGridView2.Rows.Clear();
            comFactory.Text = "";
            txtFactory.Text = "";
            comGroup.Text = "";
            txtGroup.Text = "";
            comType.Text = "";
            txtType.Text = "";
            comProduct.Text = "";
            txtProduct.Text = "";
            txtCode.Text = "";
            txtQuantity.Text = "1";
            txtSetName.Text = "";
            ImageProduct.Image = null;
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
                    query2 = "select * from groupo where Factory_ID=" + -Convert.ToInt16(txtType.Text) + " and Type_ID=" + txtType.Text;

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
                    if (txtType.Text != "1")
                    {
                        string supQuery = "";

                        supQuery = " product.Type_ID=" + txtType.Text + "";
                        if (comFactory.Text != "")
                        {
                            supQuery += " and product_factory_group.Factory_ID=" + txtFactory.Text + "";
                        }
                        else if (comGroup.Text != "")
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
                        string query = "select * from size inner join type_factory on size.Factory_ID=type_factory.Factory_ID where type_factory.Type_ID=1 " + supQuery;
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
                string query = "select * from size where Factory_ID=" + comFactory.SelectedValue;
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

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
    public partial class TransportationStore : Form
    {
        MySqlConnection dbconnection;
        bool loaded = false;
        DataGridViewRow row1;

        public TransportationStore()
        {
            InitializeComponent();
            dbconnection = new MySqlConnection(connection.connectionString);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                string query = "select * from Store";
                MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                DataTable dt = new DataTable();
                da.Fill(dt);
                cmbFromStore.DataSource = dt;
                cmbFromStore.DisplayMember = dt.Columns["Store_Name"].ToString();
                cmbFromStore.ValueMember = dt.Columns["Store_ID"].ToString();
                cmbFromStore.Text = "";

                loaded = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cmb_SelectedValueChanged(object sender, EventArgs e)
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
                }
            }
        }

        private void txt_KeyDown(object sender, KeyEventArgs e)
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
            try
            {
                search();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                row1 = dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex];
                txtCode.Text = row1.Cells[0].Value.ToString();

                string store = "";

                if (cmbFromStore.Text != "")
                {
                    store = cmbFromStore.SelectedValue.ToString();
                }
                else
                {
                    store = "select Store_ID from Store";
                }

                string query = "select Store_Place,Storage_ID from Storage where Code='" + txtCode.Text + "' and Store_ID in (" + store + ") order by Storage_Date asc";
                MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                DataTable dt = new DataTable();
                da.Fill(dt);
                cmbPlace.DataSource = dt;
                cmbPlace.DisplayMember = dt.Columns["Store_Place"].ToString();
                cmbPlace.ValueMember = dt.Columns["Storage_ID"].ToString();
                cmbPlace.Text = row1.Cells[12].Value.ToString();

                txtQuantity.Text = "";
                txtQuantity.BackColor = System.Drawing.Color.White;
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

                string store = "";

                if (cmbFromStore.Text != "")
                {
                    store = cmbFromStore.SelectedValue.ToString();
                }
                else
                {
                    store = "select Store_ID from Store";
                }

                if (cmbFromStore.Text != "" && cmbToStore.Text != "")
                {
                    if (cmbFromStore.Text != cmbToStore.Text)
                    {
                        if (txtQuantity.Text != "")
                        {
                            string query = "select Total_Meters from storage where Code='" + txtCode.Text + "' and Store_Place='" + cmbPlace.Text + "' and Store_ID in (" + store + ")";
                            MySqlCommand comand = new MySqlCommand(query, dbconnection);
                            double quantity = Convert.ToDouble(comand.ExecuteScalar().ToString());

                            double neededQuantity = Convert.ToDouble(txtQuantity.Text);
                            if (neededQuantity < quantity)
                            {
                                double meters = quantity - neededQuantity;
                                query = "update storage set Total_Meters=" + meters + " where Code='" + txtCode.Text + "' and Store_Place='" + cmbPlace.Text + "' and Store_ID in (" + store + ")";
                                comand = new MySqlCommand(query, dbconnection);
                                comand.ExecuteNonQuery();

                                int n = dataGridView2.Rows.Add();
                                dataGridView2.Rows[n].Cells[0].Value = row1.Cells[0].Value.ToString();
                                dataGridView2.Rows[n].Cells[1].Value = txtQuantity.Text;
                                dataGridView2.Rows[n].Cells[2].Value = cmbPlace.Text;
                                dataGridView2.Rows[n].Cells[3].Value = row1.Cells[1].Value.ToString();
                                dataGridView2.Rows[n].Cells[4].Value = row1.Cells[2].Value.ToString();
                                dataGridView2.Rows[n].Cells[5].Value = row1.Cells[3].Value.ToString();
                                dataGridView2.Rows[n].Cells[6].Value = row1.Cells[4].Value.ToString();
                                dataGridView2.Rows[n].Cells[7].Value = row1.Cells[5].Value.ToString();
                                dataGridView2.Rows[n].Cells[8].Value = row1.Cells[6].Value.ToString();
                                dataGridView2.Rows[n].Cells[9].Value = row1.Cells[7].Value.ToString();
                                dataGridView2.Rows[n].Cells[10].Value = row1.Cells[8].Value.ToString();
                                dataGridView2.Rows[n].Cells[11].Value = row1.Cells[9].Value.ToString();
                                dataGridView2.Rows[n].Cells[12].Value = row1.Cells[10].Value.ToString();

                                dbconnection.Close();
                                search();

                                txtQuantity.Text = "";
                                txtQuantity.BackColor = System.Drawing.Color.White;
                            }
                            else if (neededQuantity == quantity)
                            {
                                query = "delete from storage where Code='" + txtCode.Text + "' and Store_Place='" + cmbPlace.Text + "' and Store_ID in (" + store + ")";
                                comand = new MySqlCommand(query, dbconnection);
                                comand.ExecuteNonQuery();

                                int n = dataGridView2.Rows.Add();
                                dataGridView2.Rows[n].Cells[0].Value = row1.Cells[0].Value.ToString();
                                dataGridView2.Rows[n].Cells[1].Value = txtQuantity.Text;
                                dataGridView2.Rows[n].Cells[2].Value = cmbPlace.Text;
                                dataGridView2.Rows[n].Cells[3].Value = row1.Cells[1].Value.ToString();
                                dataGridView2.Rows[n].Cells[4].Value = row1.Cells[2].Value.ToString();
                                dataGridView2.Rows[n].Cells[5].Value = row1.Cells[3].Value.ToString();
                                dataGridView2.Rows[n].Cells[6].Value = row1.Cells[4].Value.ToString();
                                dataGridView2.Rows[n].Cells[7].Value = row1.Cells[5].Value.ToString();
                                dataGridView2.Rows[n].Cells[8].Value = row1.Cells[6].Value.ToString();
                                dataGridView2.Rows[n].Cells[9].Value = row1.Cells[7].Value.ToString();
                                dataGridView2.Rows[n].Cells[10].Value = row1.Cells[8].Value.ToString();
                                dataGridView2.Rows[n].Cells[11].Value = row1.Cells[9].Value.ToString();
                                dataGridView2.Rows[n].Cells[12].Value = row1.Cells[10].Value.ToString();

                                dbconnection.Close();
                                search();

                                txtQuantity.Text = "";
                                txtQuantity.BackColor = System.Drawing.Color.White;
                            }
                            else if (neededQuantity > quantity)
                            {
                                MessageBox.Show("You only have " + quantity + " in this place");

                                txtQuantity.BackColor = System.Drawing.Color.Red;
                            }
                        }
                        else
                        {
                            MessageBox.Show("please enter the quantity you need");
                        }
                    }
                    else
                    {
                        MessageBox.Show("insert correct values");
                    }
                }
                else
                {
                    MessageBox.Show("please fill all fields");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }

        void search()
        {
            try
            {
                dbconnection.Open();
                string qt;
                string qp;
                string qg;
                string qf;
                string query;
                string store = "";

                if (cmbFromStore.Text != "")
                {
                    store = cmbFromStore.SelectedValue.ToString();
                }
                else
                {
                    store = "select Store_ID from Store";
                }

                if (txtType.Text == "")
                {
                    qt = "select Type_ID from Type";
                }
                else
                {
                    qt = txtType.Text;
                }

                if (txtGroup.Text == "")
                {
                    qg = "select Group_ID from Groupo";
                }
                else
                {
                    qg = txtGroup.Text;
                }

                if (txtFactory.Text == "")
                {
                    qf = "select Factory_ID from Factory";
                }
                else
                {
                    qf = txtFactory.Text;
                }

                if (txtProduct.Text == "")
                {
                    qp = "select Product_ID from Product";
                }
                else
                {
                    qp = txtProduct.Text;
                }

                query = "SELECT data.Code as 'الكود',type.Type_Name as 'النوع',factory.Factory_Name as 'المصنع',groupo.Group_Name as 'المجموعة',product.Product_Name as 'الصنف',data.Colour as 'اللون',data.Size as 'المقاس',data.Sort as 'الفرز',data.Classification as 'التصنيف',data.Carton as 'الكرتنة',data.Description as 'الوصف',sum(storage.Total_Meters) as 'الكمية',storage.Store_Place as 'مكان التخزين' FROM data INNER JOIN type ON type.Type_ID = data.Type_ID INNER JOIN factory ON factory.Factory_ID = data.Factory_ID INNER JOIN groupo ON groupo.Group_ID = data.Group_ID INNER JOIN product ON product.Product_ID = data.Product_ID INNER JOIN storage ON storage.Code = data.Code where data.Type_ID in (" + qt + ") and data.Group_ID in (" + qg + ") and data.Product_ID in (" + qp + ") and data.Factory_ID in (" + qf + ") and storage.Store_ID in (" + store + ") group by storage.Code order by storage.Storage_Date asc";

                using (MySqlDataAdapter adpt = new MySqlDataAdapter(query, dbconnection))
                {
                    DataSet dset = new DataSet();

                    adpt.Fill(dset);

                    dataGridView1.DataSource = dset.Tables[0];
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }

        private void cmbFromStore_SelectedValueChanged(object sender, EventArgs e)
        {
            if (loaded)
            {
                string query = "select * from Store";
                MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                DataTable dt = new DataTable();
                da.Fill(dt);
                cmbToStore.DataSource = dt;
                cmbToStore.DisplayMember = dt.Columns["Store_Name"].ToString();
                cmbToStore.ValueMember = dt.Columns["Store_ID"].ToString();
                cmbToStore.Text = "";

                txtCode.Text = "";
                txtQuantity.Text = "";
                cmbPlace.DataSource = null;
                dataGridView1.DataSource = null;
                dataGridView2.DataSource = null;
            }
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            for (int i = 0; i < dataGridView2.Columns.Count; i++)
            {
                dt.Columns.Add(dataGridView2.Columns[i].Name.ToString());
            }
            for (int i = 0; i < dataGridView2.Rows.Count - 1; i++)
            {
                DataRow dr = dt.NewRow();
                for (int j = 0; j < dataGridView2.Columns.Count; j++)
                {
                    dr[dataGridView2.Columns[j].Name.ToString()] = dataGridView2.Rows[i].Cells[j].Value;
                }

                dt.Rows.Add(dr);
            }

            /*Form1_CrystalReport f = new Form1_CrystalReport(dt,cmbFromStore.Text,cmbToStore.Text);
            f.Show();
            this.Hide();*/
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Environment.Exit(0);
        }
    }
}

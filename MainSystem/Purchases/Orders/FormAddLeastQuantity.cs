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
    public partial class FormAddLeastQuantity : Form
    {
        MySqlConnection conn;
        public FormAddLeastQuantity()
        {
            InitializeComponent();
            conn = new MySqlConnection(connection.connectionString);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            conn.Open();
            string query = "SELECT distinct Type_ID,Type_Name FROM Type";

            using (MySqlDataAdapter command = new MySqlDataAdapter(query, conn))
            {
                DataTable dt = new DataTable();
                command.Fill(dt);
                cmbtype.ValueMember = "Type_ID";
                cmbtype.DisplayMember = "Type_Name";
                cmbtype.DataSource = dt;
                cmbtype.Text = "";
                txtType.Text = "";
            }

            string query2 = "SELECT distinct Factory_ID,Factory_Name FROM Factory";

            using (MySqlDataAdapter command = new MySqlDataAdapter(query2, conn))
            {
                DataTable dt = new DataTable();
                command.Fill(dt);
                cmbFactory.ValueMember = "Factory_ID";
                cmbFactory.DisplayMember = "Factory_Name";
                cmbFactory.DataSource = dt;
                cmbFactory.Text = "";
                txtFactory.Text = "";
            }

            string query3 = "SELECT distinct Group_ID,Group_Name FROM Groupo";

            using (MySqlDataAdapter command = new MySqlDataAdapter(query3, conn))
            {
                DataTable dt = new DataTable();
                command.Fill(dt);
                cmbGroup.ValueMember = "Group_ID";
                cmbGroup.DisplayMember = "Group_Name";
                cmbGroup.DataSource = dt;
                cmbGroup.Text = "";
                txtGroup.Text = "";
            }

            string query4 = "SELECT distinct Product_ID,Product_Name FROM Product";

            using (MySqlDataAdapter command = new MySqlDataAdapter(query4, conn))
            {
                DataTable dt = new DataTable();
                command.Fill(dt);
                cmbproduct.ValueMember = "Product_ID";
                cmbproduct.DisplayMember = "Product_Name";
                cmbproduct.DataSource = dt;
                cmbproduct.Text = "";
                txtProduct.Text = "";
            }

            conn.Close();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                string qt;
                string qp;
                string qg;
                string qf;
                string query;

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

                query = "SELECT D.Code as 'الكود', T.Type_Name as 'النوع',F.Factory_Name as 'المورد', G.Group_Name as 'المجموعة', p.Product_Name as 'الصنف' ,D.Sort as 'الفرز',D.Colour as 'اللون', D.Size as 'المقاس',D.Classification as 'التصنيف', D.Description as 'الوصف',D.Carton as 'الكرتنه' FROM Data D INNER JOIN Type T ON D.Type_ID=T.Type_ID INNER JOIN Product p ON D.Product_ID=p.Product_ID INNER JOIN Factory F ON D.Factory_ID=F.Factory_ID INNER JOIN Groupo G ON D.Group_ID=G.Group_ID where D.Type_ID in (" + qt + ") and D.Group_ID in (" + qg + ") and D.Product_ID in (" + qp + ") and D.Factory_ID in (" + qf + ")";


                using (MySqlDataAdapter adpt = new MySqlDataAdapter(query, conn))
                {

                    DataSet dset = new DataSet();

                    adpt.Fill(dset);

                    dataGridView1.DataSource = dset.Tables[0];

                }
                conn.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cmbtype_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtType.Text = cmbtype.SelectedValue.ToString();
        }

        private void cmbFactory_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFactory.Text = cmbFactory.SelectedValue.ToString();
        }

        private void cmbGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtGroup.Text = cmbGroup.SelectedValue.ToString();
        }

        private void cmbproduct_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtProduct.Text = cmbproduct.SelectedValue.ToString();
        }

        private void txtType_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                conn.Open();

                string query = "SELECT Type_Name FROM Type where Type_ID=" + int.Parse(txtType.Text);

                using (var command = new MySqlCommand(query, conn))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            cmbtype.SelectedItem = (reader.GetString("Type_Name"));
                        }
                    }
                }
                txtFactory.Focus();
            }
        }

        private void txtFactory_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                conn.Open();

                string query = "SELECT Factory_Name FROM Factory where Factory_ID=" + int.Parse(txtFactory.Text);

                using (var command = new MySqlCommand(query, conn))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            cmbFactory.SelectedItem = (reader.GetString("Factory_Name"));
                        }
                    }
                }
                txtGroup.Focus();
            }
        }

        private void txtGroup_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                conn.Open();

                string query = "SELECT Group_Name FROM Groupo where Group_ID=" + int.Parse(txtGroup.Text);

                using (var command = new MySqlCommand(query, conn))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            cmbGroup.SelectedItem = (reader.GetString("Group_Name"));
                        }
                    }
                }
                txtProduct.Focus();
            }
        }

        private void txtProduct_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                conn.Open();

                string query = "SELECT Product_Name FROM Product where Product_ID=" + int.Parse(txtProduct.Text);

                using (var command = new MySqlCommand(query, conn))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            cmbproduct.SelectedItem = (reader.GetString("Product_Name"));
                        }
                    }
                }
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex];
            string code = row.Cells[0].Value.ToString();
            txtCode.Text = code;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                string query = "select Least_Quantity from least_Offer where Code='" + txtCode.Text + "'";
                MySqlCommand command = new MySqlCommand(query, conn);
                conn.Open();
                var result = command.ExecuteReader();
                if (result.Read())
                {
                    conn.Close();
                    conn.Open();
                    string query2 = "update least_Offer set Least_Quantity=" + txtLeastQuantity.Text + " where Code='" + txtCode.Text + "'";
                    MySqlCommand comand = new MySqlCommand(query2, conn);
                    comand.ExecuteNonQuery();
                    MessageBox.Show("updated");
                }
                else
                {
                    conn.Close();
                    conn.Open();
                    string query2 = "insert into least_Offer (Code,Least_Quantity) values ('" + txtCode.Text + "' , " + txtLeastQuantity.Text + ")";
                    MySqlCommand comand = new MySqlCommand(query2, conn);
                    comand.ExecuteNonQuery();
                    MessageBox.Show("inserted");
                }
                conn.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                conn.Close();
            }
        }
    }
}

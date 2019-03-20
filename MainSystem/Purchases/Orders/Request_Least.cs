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
    public partial class Request_Least : Form
    {
        MySqlConnection conn;
        public Request_Least()
        {
            InitializeComponent();
            conn = new MySqlConnection(connection.connectionString);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                string query = "SELECT distinct Supplier_ID,Supplier_Name FROM supplier";

                using (MySqlDataAdapter command = new MySqlDataAdapter(query, conn))
                {
                    DataTable dt = new DataTable();
                    command.Fill(dt);
                    cmbSupplier.ValueMember = "Supplier_ID";
                    cmbSupplier.DisplayMember = "Supplier_Name";
                    cmbSupplier.DataSource = dt;
                    cmbSupplier.Text = "";
                    cmbSupplier.Text = "";
                }
                conn.Close();
                
                conn.Open();
                query = "SELECT D.Code as 'الكود', T.Type_Name as 'النوع',F.Factory_Name as 'المورد', G.Group_Name as 'المجموعة', p.Product_Name as 'الصنف' ,D.Sort as 'الفرز',D.Colour as 'اللون', D.Size as 'المقاس',D.Classification as 'التصنيف', D.Description as 'الوصف',D.Carton as 'الكرتنه' FROM Data D INNER JOIN Type T ON D.Type_ID=T.Type_ID INNER JOIN Product p ON D.Product_ID=p.Product_ID INNER JOIN Factory F ON D.Factory_ID=F.Factory_ID INNER JOIN Groupo G ON D.Group_ID=G.Group_ID";


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
                conn.Close();
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                DataGridViewRow row = dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex];
                string code = row.Cells[0].Value.ToString();
                txtCode.Text = code;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnRequest_Click(object sender, EventArgs e)
        {
            try
            {
                double test = 0;
                if (double.TryParse(txtQuantity.Text, out test))
                {
                    conn.Open();
                    string query = "insert into request_Least (Order_Date,Delivery_Date,Supplier_ID,Code,Quantity,Employee_Name) values ('" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "' , '" + dateTimePicker2.Value.ToString("yyyy-MM-dd") + "' , " + cmbSupplier.SelectedValue.ToString() + " , '" + txtCode.Text + "', " + txtQuantity.Text + " , '" + txtEmployee.Text + "')";
                    MySqlCommand comand = new MySqlCommand(query, conn);
                    comand.ExecuteNonQuery();
                    MessageBox.Show("inserted");
                    conn.Close();
                }
                else
                {
                    MessageBox.Show("Enter values in correct format");
                    conn.Close();
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                conn.Close();
            }
        }
    }
}

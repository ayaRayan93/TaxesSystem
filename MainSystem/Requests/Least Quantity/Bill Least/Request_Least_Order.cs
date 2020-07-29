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

namespace TaxesSystem
{
    public partial class Request_Least_Order : Form
    {
        MySqlConnection conn;
        public Request_Least_Order()
        {
            InitializeComponent();
            conn = new MySqlConnection(connection.connectionString);
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                string query = "SELECT request_least.Code as 'الكود', T.Type_Name as 'النوع',F.Factory_Name as 'المصنع', G.Group_Name as 'المجموعة', p.Product_Name as 'الصنف' ,D.Sort as 'الفرز',D.Colour as 'اللون', D.Size as 'المقاس',D.Classification as 'التصنيف', D.Description as 'الوصف',D.Carton as 'الكرتنه', request_least.Order_Date as 'تاريخ الطلب',request_least.Delivery_Date as 'تاريخ التسليم',request_least.Quantity as 'الكمية',supplier.Supplier_Name as 'المورد',request_least.Employee_Name as 'الموظف المسئول' FROM Data D INNER JOIN Type T ON D.Type_ID=T.Type_ID INNER JOIN Product p ON D.Product_ID=p.Product_ID INNER JOIN Factory F ON D.Factory_ID=F.Factory_ID INNER JOIN Groupo G ON D.Group_ID=G.Group_ID inner join request_least on D.Code=request_least.Code INNER JOIN supplier ON request_least.Supplier_ID = supplier.Supplier_ID where request_least.Order_Date between '" + this.dateTimePicker1.Value.ToString("yyyy-MM-dd") + "' and '" + this.dateTimePicker2.Value.ToString("yyyy-MM-dd") + "'";
                MySqlDataAdapter da = new MySqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;
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

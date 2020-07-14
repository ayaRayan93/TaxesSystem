using DevExpress.XtraEditors.Repository;
using DevExpress.XtraTab;
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
    public partial class BillLeastQuantityReport : Form
    {
        MySqlConnection dbconnection, dbconnection2, dbconnection3;
        MainForm mainForm = null;
        XtraTabControl xtraTabControlPurchases;
        bool loaded = false;
        bool factoryFlage = false;
        bool groupFlage = false;
        bool flagProduct = false;

        public BillLeastQuantityReport(MainForm mainform, XtraTabControl XtraTabControlPurchases)
        {
            InitializeComponent();
            dbconnection = new MySqlConnection(connection.connectionString);
            dbconnection2 = new MySqlConnection(connection.connectionString);
            dbconnection3 = new MySqlConnection(connection.connectionString);
            mainForm = mainform;
            xtraTabControlPurchases = XtraTabControlPurchases;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }
        
        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                testQuantity();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
            dbconnection2.Close();
            dbconnection3.Close();
        }

        private void btnNewChosen_Click(object sender, EventArgs e)
        {
            clearCom();
        }

        private void btnOpenBill_Click(object sender, EventArgs e)
        {
            if (UserControl.userType == 19 || UserControl.userType == 1)
            {
                try
                {
                    if (gridView1.SelectedRowsCount > 0)
                    {
                        List<DataRow> row1 = new List<DataRow>();
                        for (int i = 0; i < gridView1.SelectedRowsCount; i++)
                        {
                            row1.Add(gridView1.GetDataRow(gridView1.GetSelectedRows()[i]));
                        }
                        mainForm.bindRecordDashOrderForm(null, row1/*, 0*/);
                        //Order_Record form = new Order_Record(row1, null, xtraTabControlPurchases);
                        //form.ShowDialog();
                    }
                    else
                    {
                        MessageBox.Show("يجب اختيار البنود اولا");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        void testQuantity()
        {
            dbconnection.Open();
            dbconnection2.Open();
            dbconnection3.Open();

            string query = "SELECT data.Data_ID,data.Code as 'الكود',type.Type_Name as 'النوع',concat(product.Product_Name,' ',COALESCE(color.Color_Name,''),' ',data.Description,' ',groupo.Group_Name,' ',factory.Factory_Name,' ',COALESCE(size.Size_Value,''),' ',COALESCE(sort.Sort_Value,'')) as 'الاسم',SUM(storage.Total_Meters) as 'الكمية المتاحة','الحد الادنى','تسوية' FROM least_order INNER JOIN data ON least_order.Data_ID = data.Data_ID INNER JOIN storage ON storage.Data_ID = data.Data_ID LEFT JOIN color ON color.Color_ID = data.Color_ID LEFT JOIN size ON size.Size_ID = data.Size_ID LEFT JOIN sort ON sort.Sort_ID = data.Sort_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID INNER JOIN factory ON factory.Factory_ID = data.Factory_ID  INNER JOIN product ON product.Product_ID = data.Product_ID  INNER JOIN type ON type.Type_ID = data.Type_ID where data.Data_ID=0 group by data.Data_ID";
            MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
            DataTable dt = new DataTable();
            da.Fill(dt);
            gridControl1.DataSource = dt;
            gridView1.Columns[0].Visible = false;


            query = "SELECT distinct data.Data_ID FROM customer_bill INNER JOIN transitions ON customer_bill.Branch_ID = transitions.Branch_ID AND customer_bill.Branch_BillNumber = transitions.Bill_Number left join customer as customer1 on customer1.Customer_ID=customer_bill.Customer_ID left join customer as customer2 on customer2.Customer_ID=customer_bill.Client_ID INNER JOIN product_bill ON product_bill.CustomerBill_ID = customer_bill.CustomerBill_ID inner join data on data.Data_ID=product_bill.Data_ID where transitions.Transition='ايداع' and DATE(customer_bill.Bill_Date) = '" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "'";
            MySqlCommand comand = new MySqlCommand(query, dbconnection);
            MySqlDataReader dr2 = comand.ExecuteReader();
            while (dr2.Read())
            {
                string q1 = "select Data_ID from storage_least_taswya";

                string q2 = "SELECT order_details.Data_ID FROM orders INNER JOIN order_details ON order_details.Order_ID = orders.Order_ID where orders.Received=0";

                query = "SELECT distinct data.Data_ID,data.Type_ID,data.Factory_ID,data.Product_ID,data.Group_ID,data.Code as 'الكود',type.Type_Name as 'النوع',concat(product.Product_Name,' ',COALESCE(color.Color_Name,''),' ',data.Description,' ',groupo.Group_Name,' ',factory.Factory_Name,' ',COALESCE(size.Size_Value,''),' ',COALESCE(sort.Sort_Value,'')) as 'الاسم',least_order.Least_Quantity as 'الحد الادنى','تسوية' FROM least_order INNER JOIN data ON least_order.Data_ID = data.Data_ID INNER JOIN storage ON storage.Data_ID = data.Data_ID LEFT JOIN color ON color.Color_ID = data.Color_ID LEFT JOIN size ON size.Size_ID = data.Size_ID LEFT JOIN sort ON sort.Sort_ID = data.Sort_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID INNER JOIN factory ON factory.Factory_ID = data.Factory_ID  INNER JOIN product ON product.Product_ID = data.Product_ID INNER JOIN type ON type.Type_ID = data.Type_ID group by data.Data_ID having (SUM(storage.Total_Meters) <= least_order.Least_Quantity=1) and data.Data_ID not in(" + q1 + ") and data.Data_ID not in(" + q2 + ") and data.Data_ID =" + dr2["Data_ID"].ToString();
                MySqlCommand com = new MySqlCommand(query, dbconnection2);
                MySqlDataReader dr = com.ExecuteReader();
                while (dr.Read())
                {
                    query = "SELECT distinct SUM(storage.Total_Meters) as 'الكمية المتاحة' FROM least_order INNER JOIN data ON least_order.Data_ID = data.Data_ID INNER JOIN storage ON storage.Data_ID = data.Data_ID where data.Data_ID =" + dr["Data_ID"].ToString() + " group by data.Data_ID";
                    com = new MySqlCommand(query, dbconnection3);

                    gridView1.AddNewRow();
                    int rowHandle = gridView1.GetRowHandle(gridView1.DataRowCount);
                    if (gridView1.IsNewItemRow(rowHandle))
                    {
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[0], dr["Data_ID"]);
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns["الكود"], dr["الكود"]);
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns["النوع"], dr["النوع"]);
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns["الاسم"], dr["الاسم"]);
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns["الكمية المتاحة"], com.ExecuteScalar().ToString());
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns["الحد الادنى"], dr["الحد الادنى"]);
                    }
                }
                dr.Close();

            }
            dr2.Close();
            RepositoryItemCheckEdit repositoryCheckEdit1 = gridControl1.RepositoryItems.Add("CheckEdit") as RepositoryItemCheckEdit;
            repositoryCheckEdit1.ValueChecked = "True";
            repositoryCheckEdit1.ValueUnchecked = "False";
            gridView1.Columns["تسوية"].ColumnEdit = repositoryCheckEdit1;
            repositoryCheckEdit1.CheckedChanged += new EventHandler(CheckedChanged);

            if (gridView1.IsLastVisibleRow)
            {
                gridView1.FocusedRowHandle = gridView1.RowCount - 1;
            }

            for (int i = 4; i < gridView1.Columns.Count; i++)
            {
                gridView1.Columns[i].Width = 110;
            }
            gridView1.Columns["النوع"].Width = 80;
            gridView1.Columns["الكود"].Width = 180;
            gridView1.Columns["الاسم"].Width = 270;
        }

        private void CheckedChanged(object sender, System.EventArgs e)
        {
            if (UserControl.userType == 19 || UserControl.userType == 1)
            {
                try
                {
                    DevExpress.XtraEditors.CheckEdit edit = sender as DevExpress.XtraEditors.CheckEdit;
                    switch (edit.Checked)
                    {
                        case true:
                            if (MessageBox.Show("هل انت متاكد انك تريد الحذف؟", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                dbconnection.Open();
                                string query = "insert into storage_least_taswya (Data_ID,Date) values (@Data_ID,@Date)";
                                MySqlCommand com = new MySqlCommand(query, dbconnection);
                                com.Parameters.Add("@Data_ID", MySqlDbType.Int16);
                                com.Parameters["@Data_ID"].Value = gridView1.GetFocusedRowCellDisplayText(gridView1.Columns["Data_ID"]);
                                com.Parameters.Add("@Date", MySqlDbType.DateTime);
                                com.Parameters["@Date"].Value = DateTime.Now;
                                com.ExecuteNonQuery();
                                dbconnection.Close();
                                clearCom();
                                testQuantity();
                                mainForm.LeastQuantityFunction();
                            }
                            else
                            {
                                edit.CheckState = CheckState.Unchecked;
                            }
                            break;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                dbconnection.Close();
                dbconnection2.Close();
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                List<LeastQuantity_Items> bi = new List<LeastQuantity_Items>();
                for (int i = 0; i < gridView1.RowCount; i++)
                {
                    LeastQuantity_Items item = new LeastQuantity_Items() { Code = gridView1.GetRowCellDisplayText(i, gridView1.Columns["الكود"]), Product_Type = gridView1.GetRowCellDisplayText(i, gridView1.Columns["النوع"]), Product_Name = gridView1.GetRowCellDisplayText(i, gridView1.Columns["الاسم"]), Total_Meters = Convert.ToDouble(gridView1.GetRowCellDisplayText(i, gridView1.Columns["الكمية المتاحة"])), Available_Quantity = Convert.ToDouble(gridView1.GetRowCellDisplayText(i, gridView1.Columns["الحد الادنى"])) };
                    bi.Add(item);
                }
                Report_LeastQuantity f = new Report_LeastQuantity();
                f.PrintInvoice(bi);
                f.ShowDialog();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //clear function
        public void clearCom()
        {
            foreach (Control co in this.panel3.Controls)
            {
                if (co is System.Windows.Forms.ComboBox)
                {
                    co.Text = "";
                }
                else if (co is TextBox)
                {
                    co.Text = "";
                }
            }
        }
    }
}

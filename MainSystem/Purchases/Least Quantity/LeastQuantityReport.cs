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
    public partial class LeastQuantityReport : Form
    {
        MySqlConnection dbconnection, dbconnection6;
        MainForm mainForm = null;
        XtraTabControl xtraTabControlPurchases;
        bool loaded = false;
        bool factoryFlage = false;
        bool groupFlage = false;
        bool flagProduct = false;

        public LeastQuantityReport(MainForm mainform, XtraTabControl XtraTabControlPurchases)
        {
            InitializeComponent();
            dbconnection = new MySqlConnection(connection.connectionString);
            dbconnection6 = new MySqlConnection(connection.connectionString);
            mainForm = mainform;
            xtraTabControlPurchases = XtraTabControlPurchases;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                string query = "select * from type";
                MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                DataTable dt = new DataTable();
                da.Fill(dt);
                comType.DataSource = dt;
                comType.DisplayMember = dt.Columns["Type_Name"].ToString();
                comType.ValueMember = dt.Columns["Type_ID"].ToString();
                comType.Text = "";

                query = "select * from sort";
                da = new MySqlDataAdapter(query, dbconnection);
                dt = new DataTable();
                da.Fill(dt);
                comSort.DataSource = dt;
                comSort.DisplayMember = dt.Columns["Sort_Value"].ToString();
                comSort.ValueMember = dt.Columns["Sort_ID"].ToString();
                comSort.Text = "";

                string q1, q2, q3, q4, fQuery = "";
                q1 = "select Type_ID from type";
                q2 = "select Factory_ID from factory";
                q3 = "select Product_ID from product";
                q4 = "select Group_ID from groupo";
                testQuantity(q1, q2, q3, q4, fQuery);

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
                        if (loaded)
                        {
                            string query = "select * from factory inner join type_factory on factory.Factory_ID=type_factory.Factory_ID inner join type on type_factory.Type_ID=type.Type_ID where type_factory.Type_ID=" + comType.SelectedValue.ToString();
                            MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                            DataTable dt = new DataTable();
                            da.Fill(dt);
                            comFactory.DataSource = dt;
                            comFactory.DisplayMember = dt.Columns["Factory_Name"].ToString();
                            comFactory.ValueMember = dt.Columns["Factory_ID"].ToString();
                            comFactory.Text = "";
                            dbconnection.Close();
                            dbconnection.Open();
                            query = "select TypeCoding_Method from type where Type_ID=" + comType.SelectedValue.ToString();
                            MySqlCommand com = new MySqlCommand(query, dbconnection);
                            int TypeCoding_Method = (int)com.ExecuteScalar();
                            dbconnection.Close();
                            if (TypeCoding_Method == 1)
                            {
                                string query2 = "";
                                if (comType.SelectedValue.ToString() == "2" || comType.SelectedValue.ToString() == "1")
                                {
                                    query2 = "select * from groupo where Factory_ID=-1";
                                }
                                else
                                {
                                    query2 = "select * from groupo where Factory_ID=" + -Convert.ToInt32(comType.SelectedValue.ToString()) + " and Type_ID=" + comType.SelectedValue.ToString();
                                }

                                MySqlDataAdapter da2 = new MySqlDataAdapter(query2, dbconnection);
                                DataTable dt2 = new DataTable();
                                da2.Fill(dt2);
                                comGroup.DataSource = dt2;
                                comGroup.DisplayMember = dt2.Columns["Group_Name"].ToString();
                                comGroup.ValueMember = dt2.Columns["Group_ID"].ToString();
                                comGroup.Text = "";
                                groupFlage = true;
                            }
                            factoryFlage = true;

                            query = "select * from color where Type_ID=" + comType.SelectedValue.ToString();
                            da = new MySqlDataAdapter(query, dbconnection);
                            dt = new DataTable();
                            da.Fill(dt);
                            comColor.DataSource = dt;
                            comColor.DisplayMember = dt.Columns["Color_Name"].ToString();
                            comColor.ValueMember = dt.Columns["Color_ID"].ToString();
                            comColor.Text = "";
                            comFactory.Focus();
                        }
                        break;
                    case "comFactory":
                        if (factoryFlage)
                        {
                            dbconnection.Close();
                            dbconnection.Open();
                            string query = "select TypeCoding_Method from type where Type_ID=" + comType.SelectedValue.ToString();
                            MySqlCommand com = new MySqlCommand(query, dbconnection);
                            int TypeCoding_Method = (int)com.ExecuteScalar();
                            dbconnection.Close();
                            if (TypeCoding_Method == 2)
                            {
                                string query2f = "select * from groupo where Type_ID=" + comType.SelectedValue.ToString() + " and Factory_ID=" + comFactory.SelectedValue.ToString();
                                MySqlDataAdapter da2f = new MySqlDataAdapter(query2f, dbconnection);
                                DataTable dt2f = new DataTable();
                                da2f.Fill(dt2f);
                                comGroup.DataSource = dt2f;
                                comGroup.DisplayMember = dt2f.Columns["Group_Name"].ToString();
                                comGroup.ValueMember = dt2f.Columns["Group_ID"].ToString();
                                comGroup.Text = "";
                            }

                            groupFlage = true;

                            string query2 = "select * from size where Factory_ID=" + comFactory.SelectedValue.ToString();
                            MySqlDataAdapter da2 = new MySqlDataAdapter(query2, dbconnection);
                            DataTable dt2 = new DataTable();
                            da2.Fill(dt2);
                            comSize.DataSource = dt2;
                            comSize.DisplayMember = dt2.Columns["Size_Value"].ToString();
                            comSize.ValueMember = dt2.Columns["Size_ID"].ToString();
                            comSize.Text = "";
                            comGroup.Focus();
                        }
                        break;
                    case "comGroup":
                        if (groupFlage)
                        {
                            string supQuery = "", subQuery1 = "";
                            if (comType.SelectedValue.ToString() != "")
                            {
                                supQuery += " and product.Type_ID=" + comType.SelectedValue.ToString();
                            }
                            if (comFactory.SelectedValue.ToString() != "")
                            {
                                supQuery += " and product_factory_group.Factory_ID=" + comFactory.SelectedValue.ToString();
                                subQuery1 += " and Factory_ID=" + comFactory.SelectedValue.ToString();
                            }
                            string query3 = "select distinct  product.Product_ID  ,Product_Name  from product inner join product_factory_group on product.Product_ID=product_factory_group.Product_ID  where product_factory_group.Group_ID=" + comGroup.SelectedValue.ToString() + supQuery + "  order by product.Product_ID";
                            MySqlDataAdapter da3 = new MySqlDataAdapter(query3, dbconnection);
                            DataTable dt3 = new DataTable();
                            da3.Fill(dt3);
                            comProduct.DataSource = dt3;
                            comProduct.DisplayMember = dt3.Columns["Product_Name"].ToString();
                            comProduct.ValueMember = dt3.Columns["Product_ID"].ToString();
                            comProduct.Text = "";

                            string query2 = "select * from size where Group_ID=" + comGroup.SelectedValue.ToString() + subQuery1;
                            MySqlDataAdapter da2 = new MySqlDataAdapter(query2, dbconnection);
                            DataTable dt2 = new DataTable();
                            da2.Fill(dt2);
                            comSize.DataSource = dt2;
                            comSize.DisplayMember = dt2.Columns["Size_Value"].ToString();
                            comSize.ValueMember = dt2.Columns["Size_ID"].ToString();
                            comSize.Text = "";

                            comProduct.Focus();
                            flagProduct = true;
                        }
                        break;

                    case "comProduct":
                        comColor.Focus();

                        break;

                    case "comColor":
                        comSize.Focus();
                        break;

                    case "comSize":
                        break;

                    case "comSort":
                        break;
                }
            }
        }
        
        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                string q1, q2, q3, q4, fQuery = "";
                if (comType.Text == "")
                {
                    q1 = "select Type_ID from type";
                }
                else
                {
                    q1 = comType.SelectedValue.ToString();
                }
                if (comFactory.Text == "")
                {
                    q2 = "select Factory_ID from factory";
                }
                else
                {
                    q2 = comFactory.SelectedValue.ToString();
                }
                if (comProduct.Text == "")
                {
                    q3 = "select Product_ID from product";
                }
                else
                {
                    q3 = comProduct.SelectedValue.ToString();
                }
                if (comGroup.Text == "")
                {
                    q4 = "select Group_ID from groupo";
                }
                else
                {
                    q4 = comGroup.SelectedValue.ToString();
                }

                if (comSize.Text != "")
                {
                    fQuery += " and size.Size_ID=" + comSize.SelectedValue.ToString();
                }

                if (comColor.Text != "")
                {
                    fQuery += " and color.Color_ID=" + comColor.SelectedValue.ToString();
                }
                if (comSort.Text != "")
                {
                    fQuery += " and Sort.Sort_ID=" + comSort.SelectedValue.ToString();
                }

                testQuantity(q1, q2, q3, q4, fQuery);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
            dbconnection6.Close();
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

        void testQuantity(string qT, string qF, string qP, string qG, string fQuery)
        {
            dbconnection.Open();

            string q1 = "select Data_ID from storage_least_taswya";
            string q2 = "SELECT order_details.Data_ID FROM orders INNER JOIN order_details ON order_details.Order_ID = orders.Order_ID where orders.Received=0";
            //string q3 = "select data.Data_ID from data inner join storage on data.Data_ID=storage.Data_ID group by data.Data_ID HAVING SUM(storage.Total_Meters) <= least_order.Least_Quantity";

            string query = "SELECT data.Data_ID,data.Type_ID,data.Factory_ID,data.Product_ID,data.Group_ID,data.Code as 'الكود',type.Type_Name as 'النوع',concat(product.Product_Name,' ',COALESCE(color.Color_Name,''),' ',data.Description,' ',groupo.Group_Name,' ',factory.Factory_Name,' ',COALESCE(size.Size_Value,''),' ',COALESCE(sort.Sort_Value,'')) as 'الاسم',SUM(storage.Total_Meters) as 'الكمية المتاحة',least_order.Least_Quantity as 'الحد الادنى','تسوية' FROM least_order INNER JOIN data ON least_order.Data_ID = data.Data_ID INNER JOIN storage ON storage.Data_ID = data.Data_ID LEFT JOIN color ON color.Color_ID = data.Color_ID LEFT JOIN size ON size.Size_ID = data.Size_ID LEFT JOIN sort ON sort.Sort_ID = data.Sort_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID INNER JOIN factory ON factory.Factory_ID = data.Factory_ID  INNER JOIN product ON product.Product_ID = data.Product_ID  INNER JOIN type ON type.Type_ID = data.Type_ID group by data.Data_ID having (SUM(storage.Total_Meters) <= least_order.Least_Quantity=1) and data.Data_ID not in(" + q1 + ") and data.Data_ID not in(" + q2 + ") and data.Type_ID IN(" + qT + ") and data.Factory_ID IN(" + qF + ") and data.Product_ID IN (" + qP + ") and data.Group_ID IN (" + qG + ") " + fQuery;
            //string query = "SELECT data.Data_ID,data.Type_ID,data.Factory_ID,data.Product_ID,data.Group_ID,data.Code as 'الكود',type.Type_Name as 'النوع',concat(product.Product_Name,' ',COALESCE(color.Color_Name,''),' ',data.Description,' ',groupo.Group_Name,' ',factory.Factory_Name,' ',COALESCE(size.Size_Value,''),' ',COALESCE(sort.Sort_Value,'')) as 'الاسم',SUM(storage.Total_Meters) as 'الكمية المتاحة',least_order.Least_Quantity as 'الحد الادنى','تسوية' FROM least_order INNER JOIN data ON least_order.Data_ID = data.Data_ID INNER JOIN storage ON storage.Data_ID = data.Data_ID LEFT JOIN color ON color.Color_ID = data.Color_ID LEFT JOIN size ON size.Size_ID = data.Size_ID LEFT JOIN sort ON sort.Sort_ID = data.Sort_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID INNER JOIN factory ON factory.Factory_ID = data.Factory_ID  INNER JOIN product ON product.Product_ID = data.Product_ID  INNER JOIN type ON type.Type_ID = data.Type_ID group by data.Data_ID where data.Data_ID in (" + q3 + ") and data.Data_ID not in(" + q1 + ") and data.Data_ID not in(" + q2 + ") and data.Type_ID IN(" + qT + ") and data.Factory_ID IN(" + qF + ") and data.Product_ID IN (" + qP + ") and data.Group_ID IN (" + qG + ") " + fQuery;
            MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
            DataTable dt = new DataTable();
            da.Fill(dt);
            gridControl1.DataSource = dt;
            gridView1.Columns[0].Visible = false;
            gridView1.Columns["Type_ID"].Visible = false;
            gridView1.Columns["Factory_ID"].Visible = false;
            gridView1.Columns["Product_ID"].Visible = false;
            gridView1.Columns["Group_ID"].Visible = false;

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
                                string q1, q2, q3, q4, fQuery = "";
                                q1 = "select Type_ID from type";
                                q2 = "select Factory_ID from factory";
                                q3 = "select Product_ID from product";
                                q4 = "select Group_ID from groupo";
                                testQuantity(q1, q2, q3, q4, fQuery);
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

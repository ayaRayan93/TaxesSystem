using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraPrinting;
using DevExpress.XtraPrinting.Preview;
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
    public partial class Transportation_Report : Form
    {
        MySqlConnection conn;
        MySqlConnection connectionReader1, connectionReader2;
        MainForm MainForm = null;
        DataRow row1 = null;
        
        public Transportation_Report(MainForm mainForm)
        {
            InitializeComponent();
            conn = new MySqlConnection(connection.connectionString);
            connectionReader1 = new MySqlConnection(connection.connectionString);
            connectionReader2 = new MySqlConnection(connection.connectionString);
            MainForm = mainForm;
        }

        private void Bills_Transitions_Report_Load(object sender, EventArgs e)
        {
        }
        
        private void txtBillNum_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    if (txtBillNum.Text != "")
                    {
                        search(Convert.ToInt16(txtBillNum.Text));
                    }
                    else
                    {
                        MessageBox.Show("يجب تحديد رقم الفاتورة");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                conn.Close();
                connectionReader1.Close();
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                txtBillNum.Text = "";
                search(0);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            conn.Close();
            connectionReader1.Close();
        }

        private void btnNewChosen_Click(object sender, EventArgs e)
        {
            try
            {
                clearCom();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            if (row1 != null)
            {
                try
                {
                    List<Transportation_Items> bi = new List<Transportation_Items>();
                    conn.Open();
                    string query = "SELECT transfer_product.TransferProduct_ID as 'رقم التحويل',storeFrom.Store_Name as 'من مخزن',storeTo.Store_Name as 'الى مخزن',transfer_product.Date as 'تاريخ التحويل' FROM transfer_product left JOIN store as storeTo ON storeTo.Store_ID = transfer_product.To_Store left join store as storeFrom on storeFrom.Store_ID = transfer_product.From_Store where transfer_product.TransferProduct_ID=" + row1["رقم التحويل"].ToString();
                    MySqlCommand com = new MySqlCommand(query, conn);
                    MySqlDataReader dr = com.ExecuteReader();
                    while (dr.Read())
                    {
                        Transportation_Items item;
                        connectionReader2.Open();

                        string q = "SELECT data.Code as 'الكود',type.Type_Name as 'النوع',concat(product.Product_Name,' ',COALESCE(color.Color_Name,''),' ',data.Description,' ',groupo.Group_Name,' ',factory.Factory_Name,' ',COALESCE(size.Size_Value,''),' ',COALESCE(sort.Sort_Value,'')) as 'الاسم',transfer_product_details.Quantity as 'الكمية' FROM transfer_product_details INNER JOIN transfer_product ON transfer_product_details.TransferProduct_ID = transfer_product.TransferProduct_ID left JOIN store as storeTo ON storeTo.Store_ID = transfer_product.To_Store left join store as storeFrom on storeFrom.Store_ID = transfer_product.From_Store INNER JOIN data ON transfer_product_details.Data_ID = data.Data_ID LEFT JOIN color ON color.Color_ID = data.Color_ID LEFT JOIN size ON size.Size_ID = data.Size_ID LEFT JOIN sort ON sort.Sort_ID = data.Sort_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID INNER JOIN factory ON factory.Factory_ID = data.Factory_ID  INNER JOIN product ON product.Product_ID = data.Product_ID  INNER JOIN type ON type.Type_ID = data.Type_ID where transfer_product.TransferProduct_ID=" + dr["رقم التحويل"].ToString() + " order by SUBSTR(data.Code,1,16),color.Color_Name,data.Description,data.Sort_ID";
                        MySqlCommand c = new MySqlCommand(q, connectionReader2);
                        MySqlDataReader dr1 = c.ExecuteReader();
                        while (dr1.Read())
                        {
                            item = new Transportation_Items() { Code = dr1["الكود"].ToString(), Product_Type = dr1["النوع"].ToString(), Product_Name = dr1["الاسم"].ToString(), Total_Meters = Convert.ToDouble(dr1["الكمية"].ToString()) };
                            bi.Add(item);
                        }
                        dr1.Close();
                    }
                    Report_Transportation_Copy f = new Report_Transportation_Copy();
                    f.PrintInvoice(Convert.ToInt16(dr["رقم التحويل"].ToString()), dr["من مخزن"].ToString(), dr["الى مخزن"].ToString(), dr["تاريخ التحويل"].ToString(), bi);
                    dr.Close();
                    f.ShowDialog();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                conn.Close();
                connectionReader2.Close();
            }
            else
            {
                MessageBox.Show("برجاء التاكد من اختيار عنصر");
            }
        }

        public void search(int billNum)
        {
            DataSet sourceDataSet = new DataSet();
            MySqlDataAdapter adapterSupplier = null;
            MySqlDataAdapter adapterPhone = null;
            if (billNum == 0)
            {
                adapterSupplier = new MySqlDataAdapter("SELECT transfer_product.TransferProduct_ID as 'رقم التحويل',storeFrom.Store_Name as 'من مخزن',storeTo.Store_Name as 'الى مخزن',transfer_product.Date as 'تاريخ التحويل' FROM transfer_product left JOIN store as storeTo ON storeTo.Store_ID = transfer_product.To_Store left join store as storeFrom on storeFrom.Store_ID = transfer_product.From_Store where transfer_product.Canceled=0 and date(transfer_product.Date) between '" + dateTimePicker1.Value.Date.ToString("yyyy-MM-dd") + "' and '" + dateTimePicker2.Value.Date.ToString("yyyy-MM-dd") + "'", conn);
                adapterPhone = new MySqlDataAdapter("SELECT transfer_product.TransferProduct_ID as 'رقم التحويل',data.Code as 'الكود',type.Type_Name as 'النوع',concat(product.Product_Name,' ',COALESCE(color.Color_Name,''),' ',data.Description,' ',groupo.Group_Name,' ',factory.Factory_Name,' ',COALESCE(size.Size_Value,''),' ',COALESCE(sort.Sort_Value,'')) as 'الاسم',transfer_product_details.Quantity as 'الكمية' FROM transfer_product_details INNER JOIN transfer_product ON transfer_product_details.TransferProduct_ID = transfer_product.TransferProduct_ID left JOIN store as storeTo ON storeTo.Store_ID = transfer_product.To_Store left join store as storeFrom on storeFrom.Store_ID = transfer_product.From_Store INNER JOIN data ON transfer_product_details.Data_ID = data.Data_ID LEFT JOIN color ON color.Color_ID = data.Color_ID LEFT JOIN size ON size.Size_ID = data.Size_ID LEFT JOIN sort ON sort.Sort_ID = data.Sort_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID INNER JOIN factory ON factory.Factory_ID = data.Factory_ID  INNER JOIN product ON product.Product_ID = data.Product_ID  INNER JOIN type ON type.Type_ID = data.Type_ID where transfer_product.Canceled=0 and date(transfer_product.Date) between '" + dateTimePicker1.Value.Date.ToString("yyyy-MM-dd") + "' and '" + dateTimePicker2.Value.Date.ToString("yyyy-MM-dd") + "' order by SUBSTR(data.Code,1,16),color.Color_Name,data.Description,data.Sort_ID", conn);
            }
            else
            {
                adapterSupplier = new MySqlDataAdapter("SELECT transfer_product.TransferProduct_ID as 'رقم التحويل',storeFrom.Store_Name as 'من مخزن',storeTo.Store_Name as 'الى مخزن',transfer_product.Date as 'تاريخ التحويل' FROM transfer_product left JOIN store as storeTo ON storeTo.Store_ID = transfer_product.To_Store left join store as storeFrom on storeFrom.Store_ID = transfer_product.From_Store where transfer_product.Canceled=0 and transfer_product.TransferProduct_ID=" + billNum, conn);
                adapterPhone = new MySqlDataAdapter("SELECT transfer_product.TransferProduct_ID as 'رقم التحويل',data.Code as 'الكود',type.Type_Name as 'النوع',concat(product.Product_Name,' ',COALESCE(color.Color_Name,''),' ',data.Description,' ',groupo.Group_Name,' ',factory.Factory_Name,' ',COALESCE(size.Size_Value,''),' ',COALESCE(sort.Sort_Value,'')) as 'الاسم',transfer_product_details.Quantity as 'الكمية' FROM transfer_product_details INNER JOIN transfer_product ON transfer_product_details.TransferProduct_ID = transfer_product.TransferProduct_ID left JOIN store as storeTo ON storeTo.Store_ID = transfer_product.To_Store left join store as storeFrom on storeFrom.Store_ID = transfer_product.From_Store INNER JOIN data ON transfer_product_details.Data_ID = data.Data_ID LEFT JOIN color ON color.Color_ID = data.Color_ID LEFT JOIN size ON size.Size_ID = data.Size_ID LEFT JOIN sort ON sort.Sort_ID = data.Sort_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID INNER JOIN factory ON factory.Factory_ID = data.Factory_ID  INNER JOIN product ON product.Product_ID = data.Product_ID  INNER JOIN type ON type.Type_ID = data.Type_ID where transfer_product.Canceled=0 and transfer_product.TransferProduct_ID=" + billNum + " order by SUBSTR(data.Code,1,16),color.Color_Name,data.Description,data.Sort_ID", conn);
            }
            adapterSupplier.Fill(sourceDataSet, "transfer_product");
            adapterPhone.Fill(sourceDataSet, "transfer_product_details");

            //Set up a master-detail relationship between the DataTables 
            DataColumn keyColumn = sourceDataSet.Tables["transfer_product"].Columns["رقم التحويل"];
            DataColumn foreignKeyColumn = sourceDataSet.Tables["transfer_product_details"].Columns["رقم التحويل"];
            sourceDataSet.Relations.Add("بنود التحويل", keyColumn, foreignKeyColumn);

            gridControl1.DataSource = sourceDataSet.Tables["transfer_product"];
        }

        private void gridView1_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            try
            {
                row1 = gridView1.GetDataRow(gridView1.GetRowHandle(e.RowHandle));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (UserControl.userType == 1)
            {
                if (row1 != null)
                {
                    try
                    {
                        MainForm.bindUpdateTransporationForm(row1, this);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                else
                {
                    MessageBox.Show("يجب تحديد البند المراد تعديله");
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (UserControl.userType == 1)
            {
                if (MessageBox.Show("هل انت متاكد انك تريد الالغاء؟", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        conn.Open();
                        connectionReader1.Open();
                        int rowhnd = gridView1.GetSelectedRows()[0];
                        DataRow row2 = gridView1.GetDataRow(rowhnd);

                        string query = "SELECT data.Data_ID,transfer_product_details.Quantity as 'الكمية',transfer_product_details.CustomerBill_ID,transfer_product.From_Store,transfer_product.To_Store FROM transfer_product_details INNER JOIN transfer_product ON transfer_product_details.TransferProduct_ID = transfer_product.TransferProduct_ID left JOIN store as storeTo ON storeTo.Store_ID = transfer_product.To_Store left join store as storeFrom on storeFrom.Store_ID = transfer_product.From_Store left JOIN customer_bill ON transfer_product_details.CustomerBill_ID = customer_bill.CustomerBill_ID INNER JOIN data ON transfer_product_details.Data_ID = data.Data_ID LEFT JOIN color ON color.Color_ID = data.Color_ID LEFT JOIN size ON size.Size_ID = data.Size_ID LEFT JOIN sort ON sort.Sort_ID = data.Sort_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID INNER JOIN factory ON factory.Factory_ID = data.Factory_ID  INNER JOIN product ON product.Product_ID = data.Product_ID  INNER JOIN type ON type.Type_ID = data.Type_ID where transfer_product.TransferProduct_ID=" + row2[0].ToString() + " and transfer_product.Canceled=0 order by SUBSTR(data.Code, 1, 16),color.Color_Name,data.Description,data.Sort_ID";
                        MySqlCommand com = new MySqlCommand(query, connectionReader1);
                        MySqlDataReader dr = com.ExecuteReader();
                        while (dr.Read())
                        {
                            if (dr["CustomerBill_ID"].ToString() == "0")
                            {
                                query = "select sum(Total_Meters) from storage where Data_ID=" + dr[0].ToString() + " and Store_ID=" + dr["From_Store"].ToString() + " group by Data_ID";
                                com = new MySqlCommand(query, conn);
                                double quantity = Convert.ToDouble(com.ExecuteScalar().ToString());
                                double meters = quantity + Convert.ToDouble(dr["الكمية"].ToString());
                                query = "update storage set Total_Meters=" + meters + " where Data_ID=" + dr[0].ToString() + " and Store_ID=" + dr["From_Store"].ToString();
                                com = new MySqlCommand(query, conn);
                                com.ExecuteNonQuery();

                                query = "select sum(Total_Meters) from storage where Data_ID=" + dr[0].ToString() + " and Store_ID=" + dr["To_Store"].ToString() + " group by Data_ID";
                                com = new MySqlCommand(query, conn);
                                quantity = Convert.ToDouble(com.ExecuteScalar().ToString());
                                meters = quantity - Convert.ToDouble(dr["الكمية"].ToString());
                                query = "update storage set Total_Meters=" + meters + " where Data_ID=" + dr[0].ToString() + " and Store_ID=" + dr["To_Store"].ToString();
                                com = new MySqlCommand(query, conn);
                                com.ExecuteNonQuery();
                            }
                        }
                        dr.Close();

                        query = "update transfer_product set Canceled=1 where TransferProduct_ID=" + row2[0].ToString();
                        com = new MySqlCommand(query, conn);
                        com.ExecuteNonQuery();

                        GridView view = gridView1 as GridView;
                        view.DeleteRow(rowhnd);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    conn.Close();
                    connectionReader1.Close();
                }
            }
        }

        public void clearCom()
        {
            txtBillNum.Text = "";
            dateTimePicker1.Value = DateTime.Now.Date;
            dateTimePicker2.Value = DateTime.Now.Date;
            gridControl1.DataSource = null;
        }
    }
}

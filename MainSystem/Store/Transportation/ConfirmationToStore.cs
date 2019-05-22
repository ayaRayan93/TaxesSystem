using DevExpress.XtraGrid.Views.Grid;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MainSystem
{
    public partial class ConfirmationToStore : Form
    {
        MySqlConnection dbconnection;
        bool loaded = false;
        DataRow row1;
        int storeId = 0;

        public ConfirmationToStore(MainForm mainForm)
        {
            InitializeComponent();
            dbconnection = new MySqlConnection(connection.connectionString);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"Store.txt");
                storeId = Convert.ToInt16(System.IO.File.ReadAllText(path));

                DataSet sourceDataSet = new DataSet();
                MySqlDataAdapter adapterSupplier = new MySqlDataAdapter("SELECT transfer_product.TransferProduct_ID as 'رقم التحويل',store.Store_Name as 'الى مخزن',transfer_product.Date as 'تاريخ التحويل' FROM transfer_product INNER JOIN store ON store.Store_ID = transfer_product.To_Store where transfer_product.From_Store=" + storeId + " and transfer_product.Confirm_From=0", dbconnection);

                MySqlDataAdapter adapterPhone = new MySqlDataAdapter("SELECT transfer_product.TransferProduct_ID as 'رقم التحويل',data.Code as 'الكود',type.Type_Name as 'النوع',concat(product.Product_Name,' ',COALESCE(color.Color_Name,''),' ',data.Description,' ',groupo.Group_Name,' ',factory.Factory_Name,' ',COALESCE(size.Size_Value,''),' ',COALESCE(sort.Sort_Value,'')) as 'الاسم',transfer_product_details.Quantity as 'الكمية' FROM transfer_product_details INNER JOIN transfer_product ON transfer_product_details.TransferProduct_ID = transfer_product.TransferProduct_ID INNER JOIN store ON store.Store_ID = transfer_product.To_Store INNER JOIN data ON transfer_product_details.Data_ID = data.Data_ID LEFT JOIN color ON color.Color_ID = data.Color_ID LEFT JOIN size ON size.Size_ID = data.Size_ID LEFT JOIN sort ON sort.Sort_ID = data.Sort_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID INNER JOIN factory ON factory.Factory_ID = data.Factory_ID  INNER JOIN product ON product.Product_ID = data.Product_ID  INNER JOIN type ON type.Type_ID = data.Type_ID where transfer_product.From_Store=" + storeId + " and transfer_product.Confirm_From=0 group by data.Data_ID order by SUBSTR(data.Code,1,16),color.Color_Name,data.Sort_ID", dbconnection);

                adapterSupplier.Fill(sourceDataSet, "transfer_product");
                adapterPhone.Fill(sourceDataSet, "transfer_product_details");

                //Set up a master-detail relationship between the DataTables 
                DataColumn keyColumn = sourceDataSet.Tables["transfer_product"].Columns["رقم التحويل"];
                DataColumn foreignKeyColumn = sourceDataSet.Tables["transfer_product_details"].Columns["رقم التحويل"];
                sourceDataSet.Relations.Add("بنود التحويل", keyColumn, foreignKeyColumn);

                gridControl1.DataSource = sourceDataSet.Tables["transfer_product"];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void gridView1_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
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
        
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                /*if (gridView2.RowCount > 0 && comFromStore.Text != "" && comToStore.Text != "" && comFromStore.SelectedValue != null && comToStore.SelectedValue != null)
                {
                    dbconnection.Open();
                    string query = "insert into transfer_product (From_Store,To_Store,Date) values (@From_Store,@To_Store,@Date)";
                    MySqlCommand com = new MySqlCommand(query, dbconnection);
                    com.Parameters.Add("@From_Store", MySqlDbType.Int16);
                    com.Parameters["@From_Store"].Value = comFromStore.SelectedValue.ToString();
                    com.Parameters.Add("@To_Store", MySqlDbType.Int16);
                    com.Parameters["@To_Store"].Value = comToStore.SelectedValue.ToString();
                    com.Parameters.Add("@Date", MySqlDbType.DateTime);
                    com.Parameters["@Date"].Value = DateTime.Now;
                    com.ExecuteNonQuery();

                    query = "select TransferProduct_ID from transfer_product order by TransferProduct_ID desc limit 1";
                    com = new MySqlCommand(query, dbconnection);
                    int transferProductID = Convert.ToInt16(com.ExecuteScalar().ToString());

                    for (int i = 0; i < gridView2.RowCount; i++)
                    {
                        int rowhnd = gridView2.GetRowHandle(i);
                        DataRow row2 = gridView2.GetDataRow(rowhnd);
                        query = "insert into transfer_product_details (Data_ID,Quantity,TransferProduct_ID) values (@Data_ID,@Quantity,@TransferProduct_ID)";
                        com = new MySqlCommand(query, dbconnection);
                        com.Parameters.Add("@Data_ID", MySqlDbType.Int16);
                        com.Parameters["@Data_ID"].Value = row2["Data_ID"].ToString();
                        //com.Parameters.Add("@Balatat", MySqlDbType.Int16);
                        //com.Parameters["@Balatat"].Value = row2["عدد البلتات"].ToString();
                        //com.Parameters.Add("@Carton_Balata", MySqlDbType.Int16);
                        //com.Parameters["@Carton_Balata"].Value = row2["عدد الكراتين"].ToString();
                        com.Parameters.Add("@Quantity", MySqlDbType.Decimal);
                        com.Parameters["@Quantity"].Value = row2["الكمية"].ToString();
                        com.Parameters.Add("@TransferProduct_ID", MySqlDbType.Int16);
                        com.Parameters["@TransferProduct_ID"].Value = transferProductID;
                        com.ExecuteNonQuery();

                        query = "select sum(Total_Meters) from storage where Data_ID=" + row2[0].ToString() + " and Store_ID=" + comFromStore.SelectedValue.ToString() + " group by Data_ID";
                        com = new MySqlCommand(query, dbconnection);
                        double quantity = Convert.ToDouble(com.ExecuteScalar().ToString());
                        double meters = quantity - Convert.ToDouble(row2["الكمية"].ToString());
                        query = "update storage set Total_Meters=" + meters + " where Data_ID=" + row2[0].ToString() + " and Store_ID=" + comFromStore.SelectedValue.ToString();
                        com = new MySqlCommand(query, dbconnection);
                        com.ExecuteNonQuery();

                        query = "select sum(Total_Meters) from storage where Data_ID=" + row2[0].ToString() + " and Store_ID=" + comToStore.SelectedValue.ToString() + " group by Data_ID";
                        com = new MySqlCommand(query, dbconnection);
                        if (com.ExecuteScalar() != null)
                        {
                            quantity = Convert.ToDouble(com.ExecuteScalar().ToString());
                            meters = quantity + Convert.ToDouble(row2["الكمية"].ToString());
                            query = "update storage set Total_Meters=" + meters + " where Data_ID=" + row2[0].ToString() + " and Store_ID=" + comToStore.SelectedValue.ToString();
                            com = new MySqlCommand(query, dbconnection);
                            com.ExecuteNonQuery();
                        }
                        else
                        {
                            query = "insert into storage (Store_ID,Storage_Date,Type,Data_ID,Total_Meters) values (@Store_ID,@Storage_Date,@Type,@Data_ID,@Total_Meters)";
                            com = new MySqlCommand(query, dbconnection);
                            com.Parameters.Add("@Store_ID", MySqlDbType.Int16);
                            com.Parameters["@Store_ID"].Value = comToStore.SelectedValue.ToString();
                            com.Parameters.Add("@Storage_Date", MySqlDbType.DateTime);
                            com.Parameters["@Storage_Date"].Value = DateTime.Now;
                            com.Parameters.Add("@Type", MySqlDbType.VarChar);
                            com.Parameters["@Type"].Value = "بند";
                            com.Parameters.Add("@Data_ID", MySqlDbType.Int16);
                            com.Parameters["@Data_ID"].Value = row2["Data_ID"].ToString();
                            com.Parameters.Add("@Total_Meters", MySqlDbType.Decimal);
                            com.Parameters["@Total_Meters"].Value = row2["الكمية"].ToString();
                            com.ExecuteNonQuery();
                        }
                    }

                    #region report
                    List<Transportation_Items> bi = new List<Transportation_Items>();
                    for (int i = 0; i < gridView2.RowCount; i++)
                    {
                        int rowHand = gridView2.GetRowHandle(i);

                        Transportation_Items item = new Transportation_Items() { Code = gridView2.GetRowCellDisplayText(rowHand, gridView2.Columns["الكود"]), Product_Type = gridView2.GetRowCellDisplayText(rowHand, gridView2.Columns["النوع"]), Product_Name = gridView2.GetRowCellDisplayText(rowHand, gridView2.Columns["الاسم"]), Total_Meters = Convert.ToDouble(gridView2.GetRowCellDisplayText(rowHand, gridView2.Columns["الكمية"])) };
                        bi.Add(item);
                    }
                    Report_Transportation f = new Report_Transportation();
                    f.PrintInvoice(transferProductID, comFromStore.Text, comToStore.Text, bi);
                    f.ShowDialog();
                    #endregion

                    clear();
                }
                else
                {
                    MessageBox.Show("يجب ادخال جميع البيانات");
                }*/
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }
        
        public void clear()
        {
            loaded = false;
            gridControl1.DataSource = null;
            loaded = true;
        }
    }
}

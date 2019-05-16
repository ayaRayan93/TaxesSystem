using DevExpress.XtraGrid.Views.Grid;
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
    public partial class ConfirmationFromStore : Form
    {
        MySqlConnection dbconnection;
        bool loaded = false;
        DataRow row1;

        public ConfirmationFromStore(MainForm mainForm)
        {
            InitializeComponent();
            dbconnection = new MySqlConnection(connection.connectionString);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void txtTransferNumber_KeyDown(object sender, KeyEventArgs e)
        {

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
            txtTransferNumber.Text = "";
            gridControl1.DataSource = null;
            loaded = true;
        }
    }
}

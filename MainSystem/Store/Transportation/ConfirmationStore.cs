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
    public partial class ConfirmationStore : Form
    {
        MySqlConnection dbconnection;
        //DataRow row1;
        int storeId = 0;

        public ConfirmationStore(MainForm mainForm)
        {
            InitializeComponent();
            dbconnection = new MySqlConnection(connection.connectionString);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                //string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"Store.txt");
                //storeId = Convert.ToInt16(System.IO.File.ReadAllText(path));

                string supString = BaseData.StoreID;
                storeId = Convert.ToInt16(supString);

                search();
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
                if (gridView1.SelectedRowsCount > 0)
                {
                    dbconnection.Open();
                    string query = "select Store_Name from store where Store_ID=" + storeId;
                    MySqlCommand com = new MySqlCommand(query, dbconnection);
                    string storeName = com.ExecuteScalar().ToString();

                    for (int i = 0; i < gridView1.SelectedRowsCount; i++)
                    {
                        int rowhnd = gridView1.GetRowHandle(i);
                        DataRow row2 = gridView1.GetDataRow(rowhnd);
                        
                        if (row2["من مخزن"].ToString() == storeName)
                        {
                            query = "update transfer_product set Confirm_From=1,Confirm_From_Date=@Date where TransferProduct_ID=" + row2[0].ToString();
                        }
                        else //if(row2["الى مخزن"].ToString()==storeName)
                        {
                             query = "update transfer_product set Confirm_To=1,Confirm_To_Date=@Date where TransferProduct_ID=" + row2[0].ToString();
                        }
                        com = new MySqlCommand(query, dbconnection);
                        com.Parameters.Add("@Date", MySqlDbType.DateTime);
                        com.Parameters["@Date"].Value = DateTime.Now;
                        com.ExecuteNonQuery();
                    }
                    search();
                }
                else
                {
                    MessageBox.Show("يجب اختيار التحويل");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }

        public void search()
        {
            DataSet sourceDataSet = new DataSet();
            MySqlDataAdapter adapterSupplier = new MySqlDataAdapter("SELECT transfer_product.TransferProduct_ID as 'رقم التحويل',storeFrom.Store_Name as 'من مخزن',storeTo.Store_Name as 'الى مخزن',transfer_product.Date as 'تاريخ التحويل' FROM transfer_product left JOIN store as storeTo ON storeTo.Store_ID = transfer_product.To_Store left join store as storeFrom on storeFrom.Store_ID = transfer_product.From_Store where (transfer_product.From_Store=" + storeId + " and transfer_product.Confirm_From=0) or (transfer_product.To_Store=" + storeId + " and transfer_product.Confirm_To=0)", dbconnection);
            MySqlDataAdapter adapterPhone = new MySqlDataAdapter("SELECT transfer_product.TransferProduct_ID as 'رقم التحويل',data.Code as 'الكود',type.Type_Name as 'النوع',concat(product.Product_Name,' ',COALESCE(color.Color_Name,''),' ',data.Description,' ',groupo.Group_Name,' ',factory.Factory_Name,' ',COALESCE(size.Size_Value,''),' ',COALESCE(sort.Sort_Value,'')) as 'الاسم',transfer_product_details.Quantity as 'الكمية' FROM transfer_product_details INNER JOIN transfer_product ON transfer_product_details.TransferProduct_ID = transfer_product.TransferProduct_ID left JOIN store as storeTo ON storeTo.Store_ID = transfer_product.To_Store left join store as storeFrom on storeFrom.Store_ID = transfer_product.From_Store INNER JOIN data ON transfer_product_details.Data_ID = data.Data_ID LEFT JOIN color ON color.Color_ID = data.Color_ID LEFT JOIN size ON size.Size_ID = data.Size_ID LEFT JOIN sort ON sort.Sort_ID = data.Sort_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID INNER JOIN factory ON factory.Factory_ID = data.Factory_ID  INNER JOIN product ON product.Product_ID = data.Product_ID  INNER JOIN type ON type.Type_ID = data.Type_ID where (transfer_product.From_Store=" + storeId + " and transfer_product.Confirm_From=0) or (transfer_product.To_Store=" + storeId + " and transfer_product.Confirm_To=0) order by SUBSTR(data.Code,1,16),color.Color_Name,data.Description,data.Sort_ID", dbconnection);

            adapterSupplier.Fill(sourceDataSet, "transfer_product");
            adapterPhone.Fill(sourceDataSet, "transfer_product_details");

            //Set up a master-detail relationship between the DataTables 
            DataColumn keyColumn = sourceDataSet.Tables["transfer_product"].Columns["رقم التحويل"];
            DataColumn foreignKeyColumn = sourceDataSet.Tables["transfer_product_details"].Columns["رقم التحويل"];
            sourceDataSet.Relations.Add("بنود التحويل", keyColumn, foreignKeyColumn);

            gridControl1.DataSource = sourceDataSet.Tables["transfer_product"];
        }
    }
}

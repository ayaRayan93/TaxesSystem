using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraRichEdit.Utils;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MainSystem
{
    public partial class CustomerDelivery : Form
    {
        MySqlConnection dbconnection;
        string Store_ID = "0";
        
        public CustomerDelivery()
        {
            try
            {
                InitializeComponent();
                dbconnection = new MySqlConnection(connection.connectionString);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                DataHelper dh = new DataHelper(DSparametr.simpleDS);
                gridControl2.DataSource = dh.DataSet;
                gridControl2.DataMember = dh.DataMember;
                gridView2.InitNewRow += GridView1_InitNewRow;
   
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }
        private void GridView1_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            GridView view = sender as GridView;
            view.SetRowCellValue(e.RowHandle, gridView2.Columns[0], "1");
            view.SetRowCellValue(e.RowHandle, gridView2.Columns[1], "2");
            view.SetRowCellValue(e.RowHandle, gridView2.Columns[2], "3");
            view.SetRowCellValue(e.RowHandle, gridView2.Columns[3], "4");
            view.SetRowCellValue(e.RowHandle, gridView2.Columns[4], "5");
        }
        private void radioButton_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (radioBtnCustomerDelivery.Checked)
                {
                    labDescription.Text = "فاتورة رقم";
                }
                else if(radioBtnDriverDelivery.Checked)
                {
                    labDescription.Text = "اذن رقم";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void txtBillNumber_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    dbconnection.Open();
                   if (radioBtnDriverDelivery.Checked)
                    {
                        string CustomerBill_ID_Store_ID = displayBillDataFromPermatissionNumber(txtPermBillNumber.Text);
                        string query = "select * from customer_permissions_details where customer_permissions_ID=" + txtPermBillNumber.Text;
                        MySqlCommand com = new MySqlCommand(query, dbconnection);
                        MySqlDataReader dr = com.ExecuteReader();
                        if (dr.HasRows)
                        {
                            dr.Close();
                            string itemName = "concat( product.Product_Name,' ',type.Type_Name,' ',factory.Factory_Name,' ',groupo.Group_Name,' ' ,COALESCE(color.Color_Name,''),' ',COALESCE(size.Size_Value,''),' ',COALESCE(sort.Sort_Value,''),' ',COALESCE(data.Classification,''),' ',COALESCE(data.Description,''))as 'البند'";
                            string DataTableRelations = "INNER JOIN type ON type.Type_ID = data.Type_ID INNER JOIN product ON product.Product_ID = data.Product_ID INNER JOIN factory ON data.Factory_ID = factory.Factory_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID LEFT outer JOIN color ON data.Color_ID = color.Color_ID LEFT outer  JOIN size ON data.Size_ID = size.Size_ID LEFT outer  JOIN sort ON data.Sort_ID = sort.Sort_ID";
                            query = "select product_bill.Data_ID, Code as'الكود'," + itemName + ",Quantity as 'الكمية',sum(DeliveredQuantity) as 'الكمية المسلمة',customer_permissions_details.Carton as 'الكرتنة' from product_bill inner join data on data.Data_ID=product_bill.Data_ID " + DataTableRelations + " left join customer_permissions_details on customer_permissions_details.Data_ID=product_bill.Data_ID where CustomerBill_ID=" + CustomerBill_ID_Store_ID.Split('*')[0] + " and Store_ID=" + CustomerBill_ID_Store_ID.Split('*')[1] + " group by product_bill.Data_ID";

                            MySqlDataAdapter ad = new MySqlDataAdapter(query, dbconnection);
                            DataTable dt = new DataTable();
                            ad.Fill(dt);
                            gridControl1.DataSource = dt;
                            gridView1.Columns[0].Visible = false;
                        }
                        else
                        {
                            dr.Close();
                            string itemName = "concat( product.Product_Name,' ',type.Type_Name,' ',factory.Factory_Name,' ',groupo.Group_Name,' ' ,COALESCE(color.Color_Name,''),' ',COALESCE(size.Size_Value,''),' ',COALESCE(sort.Sort_Value,''),' ',COALESCE(data.Classification,''),' ',COALESCE(data.Description,''))as 'البند'";
                            string DataTableRelations = "INNER JOIN type ON type.Type_ID = data.Type_ID INNER JOIN product ON product.Product_ID = data.Product_ID INNER JOIN factory ON data.Factory_ID = factory.Factory_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID LEFT outer JOIN color ON data.Color_ID = color.Color_ID LEFT outer  JOIN size ON data.Size_ID = size.Size_ID LEFT outer  JOIN sort ON data.Sort_ID = sort.Sort_ID";
                            query = "select product_bill.Data_ID, Code as'الكود'," + itemName + ",Quantity as 'الكمية',Cartons as 'الكرتنة' from product_bill inner join data on data.Data_ID=product_bill.Data_ID " + DataTableRelations + " where CustomerBill_ID=" + CustomerBill_ID_Store_ID.Split('*')[0] + " and Store_ID=" + CustomerBill_ID_Store_ID.Split('*')[1];

                            MySqlDataAdapter ad = new MySqlDataAdapter(query, dbconnection);
                            DataTable dt = new DataTable();
                            ad.Fill(dt);
                            gridControl1.DataSource = dt;
                            gridView1.Columns[0].Visible = false;
                        }
                    }
                    else
                    {
                        string itemName = "concat( product.Product_Name,' ',type.Type_Name,' ',factory.Factory_Name,' ',groupo.Group_Name,' ' ,COALESCE(color.Color_Name,''),' ',COALESCE(size.Size_Value,''),' ',COALESCE(sort.Sort_Value,''),' ',COALESCE(data.Classification,''),' ',COALESCE(data.Description,''))as 'البند'";
                        string DataTableRelations = "INNER JOIN type ON type.Type_ID = data.Type_ID INNER JOIN product ON product.Product_ID = data.Product_ID INNER JOIN factory ON data.Factory_ID = factory.Factory_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID LEFT outer JOIN color ON data.Color_ID = color.Color_ID LEFT outer  JOIN size ON data.Size_ID = size.Size_ID LEFT outer  JOIN sort ON data.Sort_ID = sort.Sort_ID";
                        string query = "select product_bill.Data_ID, Code as'الكود'," + itemName + ",Quantity as 'الكمية',sum(DeliveredQuantity) as 'الكمية المسلمة',customer_permissions_details.Carton as 'الكرتنة',store.Store_Name as 'المخزن',product_bill.Store_ID from product_bill inner join store on product_bill.Store_ID=store.Store_ID inner join data on data.Data_ID=product_bill.Data_ID " + DataTableRelations + " left join customer_permissions_details on customer_permissions_details.Data_ID=product_bill.Data_ID inner join customer_bill on customer_bill.CustomerBill_ID=product_bill.CustomerBill_ID where RecivedType='العميل' and product_bill.CustomerBill_ID=" + txtPermBillNumber .Text + " group by product_bill.Data_ID";

                        MySqlDataAdapter ad = new MySqlDataAdapter(query, dbconnection);
                        DataTable dt = new DataTable();
                        ad.Fill(dt);
                        gridControl1.DataSource = dt;
                        gridView1.Columns[0].Visible = false;
                        gridView1.Columns[7].Visible = false;
                        displayBillDataFromCustomerBill(txtPermBillNumber.Text);
                    }

                }
            }
            catch (Exception ex)
            {
               // MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
           
        }
        private void gridView1_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            try
            {
                GridView View = sender as GridView;
                if (e.RowHandle >= 0)
                {
                    double x1 =Convert.ToDouble(View.GetRowCellDisplayText(e.RowHandle, View.Columns[3]));
                    double x2 = Convert.ToDouble(View.GetRowCellDisplayText(e.RowHandle, View.Columns[4]));

                    if (x1>x2)
                    {
                        e.Appearance.BackColor = Color.Salmon;
                        e.Appearance.BackColor2 = Color.SeaShell;
                        e.HighPriority = true;
                    }
                }
            }
            catch
            {
            }
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //try
            //{
            //    dbconnection.Open();
            //    row1 = dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex];
            //    txtCode.Text = row1.Cells[0].Value.ToString();
            //    string code= row1.Cells[0].Value.ToString();
            //    double quantity = Convert.ToDouble(row1.Cells[1].Value);
                
            //    if (int.TryParse(txtStoreID.Text, out storeId) && int.TryParse(txtBranchID.Text, out branchID) && int.TryParse(txtBillNumber.Text, out BranchBillNum))
            //    {
            //        string query = "select Store_Place from storage where Store_ID=" + storeId + " and Code='" + code+"' order by Storage_Date ";
            //        MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
            //        DataTable dt = new DataTable();
            //        da.Fill(dt);
            //        comStorePlace.DataSource = dt;
            //        comStorePlace.DisplayMember = dt.Columns["Store_Place"].ToString();
            //        comStorePlace.Text = "";

            //        query = "select sum(Received_Quantity) from received_bill_store where Branch_ID=" + branchID + " and Branch_BillNumber=" + BranchBillNum+" and Code="+code;
            //        MySqlCommand com = new MySqlCommand(query,dbconnection);
            //        if (com.ExecuteScalar().ToString() != "")
            //        {
            //            double recivedQuantity = Convert.ToDouble(com.ExecuteScalar());
            //            txtRecivedQuantity.Text = (quantity - recivedQuantity).ToString();

            //        }
            //        else
            //        {
            //            txtRecivedQuantity.Text = quantity.ToString();
            //        }

            //        finish = false;

                
            //        for (int i = 0; i < recordFinishedCount; i++)
            //        {
            //            if (recordFinishedCode[i].Split('*')[0] == code && recordFinishedCode[i].Split('*')[1] == BranchBillNum.ToString() && recordFinishedCode[i].Split('*')[2] == branchID.ToString())
            //            {
            //                finish = true;
                               
            //            }   
            //        }
            //        groupBox3.Visible = true;
            //    }
            //    else
            //    {
            //        MessageBox.Show("insert correct value");
            //    }
              
                
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
            //dbconnection.Close();
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                addNewRow(row);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
       
        }
        private void btnRemove_Click(object sender, EventArgs e)
        {
            try
            {
                int rowHandle = gridView2.GetRowHandle(gridView2.GetSelectedRows()[0]);
                gridView2.DeleteRow(rowHandle);
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
                if (gridView2.RowCount > 0 && txtPermBillNumber.Text != "")
                {
                    dbconnection.Open();
                    for (int i = 0; i < gridView2.RowCount; i++)
                    {
                        DataRow row1 = gridView2.GetDataRow(gridView2.GetRowHandle(i));
                        string query = "insert into customer_permissions_details (customer_permissions_ID,Data_ID,Date,Carton,DeliveredQuantity) values (@customer_permissions_ID,@Data_ID,@Date,@Carton,@DeliveredQuantity)";
                        MySqlCommand com = new MySqlCommand(query, dbconnection);
                        com.Parameters.Add("@customer_permissions_ID", MySqlDbType.Int16);
                        com.Parameters["@customer_permissions_ID"].Value = txtPermBillNumber.Text;
                        com.Parameters.Add("@Data_ID", MySqlDbType.Int16);
                        com.Parameters["@Data_ID"].Value = row1["Data_ID"].ToString();
                        com.Parameters.Add("@DeliveredQuantity", MySqlDbType.Double);
                        com.Parameters["@DeliveredQuantity"].Value = row1[3].ToString();
                        com.Parameters.Add("@Carton", MySqlDbType.Double);
                        com.Parameters["@Carton"].Value = row1[4].ToString();
                        com.Parameters.Add("@Date", MySqlDbType.Date);
                        com.Parameters["@Date"].Value = dateTimePicker1.Value.Date;

                        com.ExecuteNonQuery();

                        clear();
                    }
                }
                else
                {
                    MessageBox.Show("insert correct value");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }
        DataRow row=null;
        int rowHandel1 = -1;
        private void gridView1_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            try
            {
                row = gridView1.GetDataRow(gridView1.GetRowHandle(e.RowHandle));
                rowHandel1 = e.RowHandle;
                txtCode.Text= row[1].ToString();
                if (radioBtnCustomerDelivery.Checked)
                    Store_ID = row[7].ToString();
                string query = "select concat(Store_Place_Code ,'   ',Total_Meters) as 'x' from storage inner join store_places on store_places.Store_Place_ID=storage.Store_Place_ID where storage.Store_ID=" + Store_ID + " and  Data_ID=" + row[0].ToString();
                MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                DataTable dt = new DataTable();
                da.Fill(dt);
                comStorePlace.DataSource = dt;
                comStorePlace.DisplayMember = dt.Columns["x"].ToString();
                txtRecivedQuantity.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void txtRecivedQuantity_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    if (row != null)
                    {
                        addNewRow(row);
                        txtCode.Text = "";
                        txtRecivedQuantity.Text = "";
                        comStorePlace.DataSource = null;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //functions 
        public string displayBillDataFromPermatissionNumber(string PermissionNumber)
        {
            string CustomerBill_ID = "";
            string CustomerBill_ID_Store_ID = "";
            string query = "select CustomerBill_ID,Store_Name,customer_permissions.Store_ID from customer_permissions inner join store on customer_permissions.Store_ID=store.Store_ID where Permissin_ID=" + PermissionNumber;
            MySqlCommand com = new MySqlCommand(query,dbconnection);
            MySqlDataReader dr = com.ExecuteReader();
            while (dr.Read())
            {
                CustomerBill_ID = dr[0].ToString();
                labStoreName.Text = dr[1].ToString();
                Store_ID = dr[2].ToString();
                CustomerBill_ID_Store_ID = CustomerBill_ID + "*" + Store_ID;
            }
            dr.Close();

            query = "select * from customer_bill inner join shipping on customer_bill.CustomerBill_ID=shipping.CustomerBill_ID where customer_bill.CustomerBill_ID=" + CustomerBill_ID;
            com = new MySqlCommand(query, dbconnection);
            dr = com.ExecuteReader();
            while (dr.Read())
            {
                txtCustomerName.Text = dr["Customer_Name"].ToString();
                txtCustomerID.Text = dr["Customer_ID"].ToString();
                txtClientName.Text = dr["Client_Name"].ToString();
                txtClientID.Text = dr["Client_ID"].ToString();
                txtPhoneNumber.Text = dr["Phone"].ToString();
                txtAddress.Text = dr["Address"].ToString();
            }
            dr.Close();

            return CustomerBill_ID_Store_ID;
        }
        public void displayBillDataFromCustomerBill(string CustomerBill_ID)
        {
            string query = "select * from customer_bill  where CustomerBill_ID=" + CustomerBill_ID;
            MySqlCommand com = new MySqlCommand(query, dbconnection);
            MySqlDataReader dr = com.ExecuteReader();
            while (dr.Read())
            {
                txtCustomerName.Text = dr["Customer_Name"].ToString();
                txtCustomerID.Text = dr["Customer_ID"].ToString();
                txtClientName.Text = dr["Client_Name"].ToString();
                txtClientID.Text = dr["Client_ID"].ToString();
            }
            dr.Close();
        }
        public void addNewRow(DataRow row)
        {
            if (!IsExist(row[0].ToString()))
            {
                gridView2.AddNewRow();

                int rowHandle = gridView2.GetRowHandle(gridView2.DataRowCount);
                if (gridView2.IsNewItemRow(rowHandle)&&rowHandel1 !=-1)
                {
                    gridView2.SetRowCellValue(rowHandle, gridView2.Columns[0], row[0]);
                    gridView2.SetRowCellValue(rowHandle, gridView2.Columns[1], row[1]);
                    gridView2.SetRowCellValue(rowHandle, gridView2.Columns[2], row[2]);
                    gridView2.SetRowCellValue(rowHandle, gridView2.Columns[3], txtRecivedQuantity.Text);
                    gridView2.SetRowCellValue(rowHandle, gridView2.Columns[4], row["الكرتنة"]);
                }
            }
        }     
        public bool IsExist(string Data_ID)
        {
            for (int i = 0; i < gridView2.RowCount; i++)
            {
                int rowHandle = gridView2.GetRowHandle(i);
                DataRow ss = gridView2.GetDataRow(rowHandle);
                if (ss[0].ToString() == Data_ID)
                    return true;
            }
            
            return false;
        }
        public void clear()
        {
            foreach (Control item in groupBox1.Controls)
            {
                if (item is TextBox)
                    item.Text = "";
            }
            foreach (Control item in groupBox2.Controls)
            {
                if (item is TextBox)
                    item.Text = "";
            }
            foreach (Control item in groupBox3.Controls)
            {
                if (item is TextBox)
                    item.Text = "";
            }
            txtPermBillNumber.Text = labStoreName.Text = "";
            comStorePlace.DataSource = null;
            gridControl1.DataSource = null;
            DataHelper dh = new DataHelper(DSparametr.simpleDS);
            gridControl2.DataSource = dh.DataSet;
            gridControl2.DataMember = dh.DataMember;
        }
    }

   
}

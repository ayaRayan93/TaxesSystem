using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraRichEdit.Utils;
using MainSystem.Store.Export;
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
        bool loaded = false;
        string permissionNum;
        int flag;
        bool comBranchLoaded=false;
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
        public CustomerDelivery(string permissionNum,int flag)
        {
            try
            {
                InitializeComponent();
                dbconnection = new MySqlConnection(connection.connectionString);
                dbconnection.Open();
                this.permissionNum = permissionNum;
                this.flag = flag;
                if (flag == 1)
                {
                    radioBtnDriverDelivery.Checked = true;
                }
                else
                {
                    radioBtnCustomerDelivery.Checked = true;
                }
                txtPermBillNumber.Text = permissionNum;
                displayPermission(permissionNum);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                DataHelper dh = new DataHelper(DSparametr.simpleDS);
                gridControl2.DataSource = dh.DataSet;
                gridControl2.DataMember = dh.DataMember;
                gridView2.InitNewRow += GridView1_InitNewRow;

                string query = "select * from branch";
                MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                DataTable dt = new DataTable();
                da.Fill(dt);
                comBranch.DataSource = dt;
                comBranch.DisplayMember = dt.Columns["Branch_Name"].ToString();
                comBranch.ValueMember = dt.Columns["Branch_ID"].ToString();
                comBranch.Text = "";
                txtBranchID.Text = "";
                comBranchLoaded = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }
        private void comBranch_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (comBranchLoaded)
                {
                    txtBranchID.Text = comBranch.SelectedValue.ToString();
                  
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void txtBranchID_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    dbconnection.Close();
                    string query = "select Branch_Name from branch where Branch_ID=" + txtBranchID.Text;
                    MySqlCommand comand = new MySqlCommand(query, dbconnection);
                    dbconnection.Open();
                    string Branch_Name = comand.ExecuteScalar().ToString();
                    dbconnection.Close();
                    comBranch.Text = Branch_Name;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        private void GridView1_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            GridView view = sender as GridView;
            view.SetRowCellValue(e.RowHandle, gridView2.Columns[0], 0);
            view.SetRowCellValue(e.RowHandle, gridView2.Columns[1], "0");
            view.SetRowCellValue(e.RowHandle, gridView2.Columns[2], "0");
            view.SetRowCellValue(e.RowHandle, gridView2.Columns[3], "0");
            view.SetRowCellValue(e.RowHandle, gridView2.Columns[4], "0");
            view.SetRowCellValue(e.RowHandle, gridView2.Columns[5], "0");
            view.SetRowCellValue(e.RowHandle, gridView2.Columns[6], "0");
        }
        private void radioButton_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (radioBtnCustomerDelivery.Checked)
                {
                    labDescription.Text = "فاتورة رقم";
                    panBranch.Visible = true;
                    comBranch.Text = "";
                    txtBranchID.Text = "";
                }
                else if(radioBtnDriverDelivery.Checked)
                {
                    labDescription.Text = "اذن رقم";
                    panBranch.Visible = false;
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
                    displayData();
                    //displayPermission();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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
        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (row != null)
                {
                    if (Convert.ToDouble(txtRecivedQuantity.Text) < Convert.ToDouble(row["الكمية"]))
                    {
                        addNewRow(row);
                        txtCode.Text = "";
                        txtRecivedQuantity.Text = "";
                        comStorePlace.DataSource = null;
                    }
                    else
                    {
                        txtRecivedQuantity.Text = "0";
                        txtRecivedQuantity.Focus();
                    }
                }
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
                    List<DeliveryPermissionClass> listOfData = new List<DeliveryPermissionClass>();
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
                        com.Parameters["@DeliveredQuantity"].Value = row1[4].ToString();
                        com.Parameters.Add("@Carton", MySqlDbType.Double);
                        com.Parameters["@Carton"].Value = row1[5].ToString();
                        com.Parameters.Add("@NumOfCarton", MySqlDbType.Double);
                        com.Parameters["@NumOfCarton"].Value = row1[6].ToString();
                        com.Parameters.Add("@Date", MySqlDbType.Date);
                        com.Parameters["@Date"].Value = dateTimePicker1.Value.Date;

                        com.ExecuteNonQuery();

                        DeliveryPermissionClass deliveryPermissionClass = new DeliveryPermissionClass();
                        deliveryPermissionClass.Data_ID = (int)row1["Data_ID"];
                        deliveryPermissionClass.Code = row1[1].ToString();
                        deliveryPermissionClass.ItemName = row1[2].ToString();
                        deliveryPermissionClass.TotalQuantity =Convert.ToDouble(row1[3]);
                        deliveryPermissionClass.Carton = Convert.ToDouble(row1[5]);
                        deliveryPermissionClass.DeliveryQuantity = Convert.ToDouble(row1[4]);
                        listOfData.Add(deliveryPermissionClass);
                       
                    }
                    DeliveryPermissionReportViewer DeliveryPermissionReport = new DeliveryPermissionReportViewer(listOfData,txtPermBillNumber.Text);
                    DeliveryPermissionReport.Show();
                    clear();
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
                txtRecivedQuantity.Text = row[3].ToString();
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
        private void gridView2_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
       {
            try
            {
                if (loaded)
                {
                    if (e.Column.Name == "Carton")
                    {
                        GridView view = (GridView)sender;
                        DataRow dataRow = view.GetFocusedDataRow();
                        double totalQuantityDelivery = Convert.ToDouble(dataRow["DeliveryQuantity"].ToString());
                        double cellValue = Convert.ToDouble(e.Value);
                        double re = totalQuantityDelivery / cellValue;
                        view.SetRowCellValue(view.GetSelectedRows()[0], "NumOfCarton", re + "");
                    }
                    else if (e.Column.Name == "DeliveryQuantity")
                    {
                        GridView view = (GridView)sender;
                        DataRow dataRow = view.GetFocusedDataRow();
                        double Carton = Convert.ToDouble(dataRow["Carton"].ToString());
                        double cellValue = Convert.ToDouble(e.Value);
                        double re = 0;
                        if (Carton != 0)
                            re = cellValue / Carton;
                        if ((double)dataRow["DeliveryQuantity"] < (double)dataRow["Quantity"])
                        {
                            view.SetRowCellValue(view.GetSelectedRows()[0], "NumOfCarton", re + "");
                        }
                        else
                        {
                            view.SetRowCellValue(view.GetSelectedRows()[0], "DeliveryQuantity",  "0");
                        }
                    }
                    else if (e.Column.Name == "Carton")
                    {
                        GridView view = (GridView)sender;
                        DataRow dataRow = view.GetFocusedDataRow();
                        double NumOfCarton = Convert.ToDouble(dataRow["NumOfCarton"].ToString());
                        double cellValue = Convert.ToDouble(e.Value);
                        double re = cellValue * NumOfCarton;

                        view.SetRowCellValue(view.GetSelectedRows()[0], "DeliveryQuantity", re + "");
                    }
                }
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
                        if (Convert.ToDouble(txtRecivedQuantity.Text) <= Convert.ToDouble(row[3].ToString()))
                        {
                            addNewRow(row);
                            txtCode.Text = "";
                            txtRecivedQuantity.Text = "";
                            comStorePlace.DataSource = null;
                        }
                        else
                        {
                            txtRecivedQuantity.Text = "0";
                            txtRecivedQuantity.Focus();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                if (gridView1.RowCount > 0 && txtPermBillNumber.Text != "")
                {
                    dbconnection.Open();
                    List<DeliveryPermissionClass> listOfData = new List<DeliveryPermissionClass>();
                    for (int i = 0; i < gridView1.RowCount; i++)
                    {
                        DataRow row1 = gridView1.GetDataRow(gridView1.GetRowHandle(i));
                        //string query = "insert into customer_permissions_details (customer_permissions_ID,Data_ID,Date,Carton,DeliveredQuantity) values (@customer_permissions_ID,@Data_ID,@Date,@Carton,@DeliveredQuantity)";
                        //MySqlCommand com = new MySqlCommand(query, dbconnection);
                        //com.Parameters.Add("@customer_permissions_ID", MySqlDbType.Int16);
                        //com.Parameters["@customer_permissions_ID"].Value = txtPermBillNumber.Text;
                        //com.Parameters.Add("@Data_ID", MySqlDbType.Int16);
                        //com.Parameters["@Data_ID"].Value = row1["Data_ID"].ToString();
                        //com.Parameters.Add("@DeliveredQuantity", MySqlDbType.Double);
                        //com.Parameters["@DeliveredQuantity"].Value = row1[4].ToString();
                        //com.Parameters.Add("@Carton", MySqlDbType.Double);
                        //com.Parameters["@Carton"].Value = row1[5].ToString();
                        //com.Parameters.Add("@NumOfCarton", MySqlDbType.Double);
                        //com.Parameters["@NumOfCarton"].Value = row1[6].ToString();
                        //com.Parameters.Add("@Date", MySqlDbType.Date);
                        //com.Parameters["@Date"].Value = dateTimePicker1.Value.Date;

                        //com.ExecuteNonQuery();

                        DeliveryPermissionClass deliveryPermissionClass = new DeliveryPermissionClass();
                        deliveryPermissionClass.Data_ID = (int)row1["Data_ID"];
                        deliveryPermissionClass.Code = row1[1].ToString();
                        deliveryPermissionClass.ItemName = row1[2].ToString();
                        deliveryPermissionClass.TotalQuantity = Convert.ToDouble(row1[3]);
                        if (row1[5].ToString() != "")
                        {
                            deliveryPermissionClass.Carton = Convert.ToDouble(row1[5]);
                        }
                        else
                        {
                            deliveryPermissionClass.Carton = Convert.ToDouble(0);
                        }
                        deliveryPermissionClass.DeliveryQuantity = Convert.ToDouble(0);
                        listOfData.Add(deliveryPermissionClass);

                    }
                    DeliveryPermissionReportViewer DeliveryPermissionReport = new DeliveryPermissionReportViewer(listOfData, txtPermBillNumber.Text);
                    DeliveryPermissionReport.Show();
                    clear();
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
        public void displayBillDataFromCustomerBill(string BranchID,string BranchBillNumber)
        {
            string query = "select * from customer_bill  where Branch_ID=" + BranchID + " and Branch_BillNumber="+ BranchBillNumber;
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
            if (!IsExist(row[0].ToString(), row[5].ToString()))
            {
                gridView2.AddNewRow();

                int rowHandle = gridView2.GetRowHandle(gridView2.DataRowCount);
                if (gridView2.IsNewItemRow(rowHandle)&&rowHandel1 !=-1)
                {
                    gridView2.SetRowCellValue(rowHandle, gridView2.Columns[0], row[0]);
                    gridView2.SetRowCellValue(rowHandle, gridView2.Columns[1], row[1]);
                    gridView2.SetRowCellValue(rowHandle, gridView2.Columns[2], row[2]);
                    gridView2.SetRowCellValue(rowHandle, gridView2.Columns[3], row[3]);

                    double re = 0,carton=0;
                    if (row[5].ToString() != "")
                    {
                        if (Convert.ToDouble(row[5]) != 0)
                            re = Convert.ToDouble(txtRecivedQuantity.Text) / Convert.ToDouble(row[5]);
                        carton = Convert.ToDouble(row[5]);
                    }
                    gridView2.SetRowCellValue(rowHandle, gridView2.Columns["عدد الكراتين المستلمة"],re+"");
                    gridView2.SetRowCellValue(rowHandle, gridView2.Columns["الكرتنة"], carton+"");
                    gridView2.SetRowCellValue(rowHandle, gridView2.Columns[4], txtRecivedQuantity.Text);
                }
            loaded = true;
            }
        }
        public void displayPermission()
        {
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
                    query = "select product_bill.Data_ID, Code as'الكود'," + itemName + ",Quantity as 'الكمية',sum(DeliveredQuantity) as 'الكمية المسلمة',customer_permissions_details.Carton as 'الكرتنة',product_bill.Returned as 'تم الاسترجاع' from product_bill inner join data on data.Data_ID=product_bill.Data_ID " + DataTableRelations + " left join customer_permissions_details on customer_permissions_details.Data_ID=product_bill.Data_ID where CustomerBill_ID=" + CustomerBill_ID_Store_ID.Split('*')[0] + " and Store_ID=" + CustomerBill_ID_Store_ID.Split('*')[1] + " group by product_bill.Data_ID";

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
                    query = "select product_bill.Data_ID, Code as'الكود'," + itemName + ",Quantity as 'الكمية', '" + 0 + " ' as 'الكمية المسلمة',Cartons as 'الكرتنة',product_bill.Returned as 'تم الاسترجاع' from product_bill inner join data on data.Data_ID=product_bill.Data_ID " + DataTableRelations + " where CustomerBill_ID=" + CustomerBill_ID_Store_ID.Split('*')[0] + " and Store_ID=" + CustomerBill_ID_Store_ID.Split('*')[1];

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
                string query = "select product_bill.Data_ID, Code as'الكود'," + itemName + ",Quantity as 'الكمية',sum(DeliveredQuantity) as 'الكمية المسلمة',customer_permissions_details.Carton as 'الكرتنة',store.Store_Name as 'المخزن',product_bill.Store_ID,product_bill.Returned as 'تم الاسترجاع' from product_bill inner join store on product_bill.Store_ID=store.Store_ID inner join data on data.Data_ID=product_bill.Data_ID " + DataTableRelations + " left join customer_permissions_details on customer_permissions_details.Data_ID=product_bill.Data_ID inner join customer_bill on customer_bill.CustomerBill_ID=product_bill.CustomerBill_ID where RecivedType='العميل' and Branch_BillNumber=" + txtPermBillNumber.Text + " and Branch_ID="+txtBranchID.Text+" and Type='بند' group by product_bill.Data_ID";

                MySqlDataAdapter ad = new MySqlDataAdapter(query, dbconnection);
                DataTable dt = new DataTable();
                ad.Fill(dt);
                gridControl1.DataSource = dt;
                gridView1.Columns[0].Visible = false;
                gridView1.Columns[7].Visible = false;


                displayBillDataFromCustomerBill(txtBranchID.Text,txtPermBillNumber.Text);
            }
        }
        public void displayPermission(string permissionNum)
        {
            if (radioBtnDriverDelivery.Checked)
            {
                string CustomerBill_ID_Store_ID = displayBillDataFromPermatissionNumber(permissionNum);
                string query = "select * from customer_permissions_details where customer_permissions_ID=" + permissionNum;
                MySqlCommand com = new MySqlCommand(query, dbconnection);
                MySqlDataReader dr = com.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Close();
                    string itemName = "concat( product.Product_Name,' ',type.Type_Name,' ',factory.Factory_Name,' ',groupo.Group_Name,' ' ,COALESCE(color.Color_Name,''),' ',COALESCE(size.Size_Value,''),' ',COALESCE(sort.Sort_Value,''),' ',COALESCE(data.Classification,''),' ',COALESCE(data.Description,''))as 'البند'";
                    string DataTableRelations = "INNER JOIN type ON type.Type_ID = data.Type_ID INNER JOIN product ON product.Product_ID = data.Product_ID INNER JOIN factory ON data.Factory_ID = factory.Factory_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID LEFT outer JOIN color ON data.Color_ID = color.Color_ID LEFT outer  JOIN size ON data.Size_ID = size.Size_ID LEFT outer  JOIN sort ON data.Sort_ID = sort.Sort_ID";
                    query = "select product_bill.Data_ID, Code as'الكود'," + itemName + ",Quantity as 'الكمية',sum(DeliveredQuantity) as 'الكمية المسلمة',customer_permissions_details.Carton as 'الكرتنة',product_bill.Returned as 'تم الاسترجاع' from product_bill inner join data on data.Data_ID=product_bill.Data_ID " + DataTableRelations + " left join customer_permissions_details on customer_permissions_details.Data_ID=product_bill.Data_ID where CustomerBill_ID=" + CustomerBill_ID_Store_ID.Split('*')[0] + " and Store_ID=" + CustomerBill_ID_Store_ID.Split('*')[1] + " group by product_bill.Data_ID";

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
                    query = "select product_bill.Data_ID, Code as'الكود'," + itemName + ",Quantity as 'الكمية', '" + 0 + " ' as 'الكمية المسلمة',Cartons as 'الكرتنة',product_bill.Returned as 'تم الاسترجاع' from product_bill inner join data on data.Data_ID=product_bill.Data_ID " + DataTableRelations + " where CustomerBill_ID=" + CustomerBill_ID_Store_ID.Split('*')[0] + " and Store_ID=" + CustomerBill_ID_Store_ID.Split('*')[1];

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
                string query = "select product_bill.Data_ID, Code as'الكود'," + itemName + ",Quantity as 'الكمية',sum(DeliveredQuantity) as 'الكمية المسلمة',customer_permissions_details.Carton as 'الكرتنة',store.Store_Name as 'المخزن',product_bill.Store_ID,product_bill.Returned as 'تم الاسترجاع' from product_bill inner join store on product_bill.Store_ID=store.Store_ID inner join data on data.Data_ID=product_bill.Data_ID " + DataTableRelations + " left join customer_permissions_details on customer_permissions_details.Data_ID=product_bill.Data_ID inner join customer_bill on customer_bill.CustomerBill_ID=product_bill.CustomerBill_ID where RecivedType='العميل' and product_bill.CustomerBill_ID=" + permissionNum + " group by product_bill.Data_ID";

                MySqlDataAdapter ad = new MySqlDataAdapter(query, dbconnection);
                DataTable dt = new DataTable();
                ad.Fill(dt);
                gridControl1.DataSource = dt;
                gridView1.Columns[0].Visible = false;
                gridView1.Columns[7].Visible = false;
                displayBillDataFromCustomerBill(txtBranchID.Text,permissionNum);
            }
        }
        public bool IsExist(string Data_ID,string carton)
        {
            for (int i = 0; i < gridView2.RowCount; i++)
            {
                int rowHandle = gridView2.GetRowHandle(i);
                DataRow ss = gridView2.GetDataRow(rowHandle);
                if (ss[0].ToString() == Data_ID&& ss[5].ToString()==carton)
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
        public void displayData()
        {
            string query = "select CustomerBill_ID from customer_bill where Branch_BillNumber=" + txtPermBillNumber.Text + " and Branch_ID=" + txtBranchID.Text;

            MySqlCommand com = new MySqlCommand(query, dbconnection);
            int id = Convert.ToInt16(com.ExecuteScalar());

            DataTable dtAll = new DataTable();
            query = "select data.Data_ID,data.Code as 'الكود',concat(product.Product_Name,' - ',type.Type_Name,' - ',factory.Factory_Name,' - ',groupo.Group_Name,' ',COALESCE(color.Color_Name,''),' ',COALESCE(size.Size_Value,''),' ',COALESCE(sort.Sort_Value,'')) as 'الاسم',product_bill.Type as 'الفئة',product_bill.Quantity as 'الكمية',data.Description as 'الوصف',product_bill.Returned as 'تم الاسترجاع',product_bill.Delegate_ID,product_bill.CustomerBill_ID,product_bill.Store_ID from product_bill inner join data on data.Data_ID=product_bill.Data_ID LEFT JOIN color ON color.Color_ID = data.Color_ID LEFT JOIN size ON size.Size_ID = data.Size_ID LEFT JOIN sort ON sort.Sort_ID = data.Sort_ID INNER JOIN type ON type.Type_ID = data.Type_ID INNER JOIN product ON product.Product_ID = data.Product_ID INNER JOIN factory ON data.Factory_ID = factory.Factory_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID   where CustomerBill_ID=" + id + " and product_bill.Type='بند'  and (product_bill.Returned='لا' or product_bill.Returned='جزء')";
            MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
            DataTable dtProduct = new DataTable();
            da.Fill(dtProduct);
            //type.Type_Name as 'النوع', factory.Factory_Name as 'المصنع', groupo.Group_Name as 'المجموعة'
            query = "select sets.Set_ID as 'الكود',sets.Set_Name as 'الاسم',product_bill.Type as 'الفئة', product_bill.Quantity as 'الكمية',sets.Description as 'الوصف',product_bill.Returned as 'تم الاسترجاع',product_bill.Delegate_ID,product_bill.CustomerBill_ID,product_bill.Store_ID from product_bill inner join sets on sets.Set_ID=product_bill.Data_ID  where CustomerBill_ID=" + id + " and product_bill.Type='طقم' and (product_bill.Returned='لا' or product_bill.Returned='جزء')";
            da = new MySqlDataAdapter(query, dbconnection);
            DataTable dtSet = new DataTable();
            da.Fill(dtSet);
            dtAll = dtProduct.Copy();
            dtAll.Merge(dtSet, true, MissingSchemaAction.Ignore);
            gridControl1.DataSource = dtAll;
            gridView1.Columns[0].Visible = false;
            gridView1.Columns["CustomerBill_ID"].Visible = false;
            gridView1.Columns["الفئة"].Visible = false;
            gridView1.Columns["الوصف"].Visible = false;
            gridView1.Columns["Delegate_ID"].Visible = false;
            gridView1.Columns["Store_ID"].Visible = false;

        }
        
    }

   
}

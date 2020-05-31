using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraReports.UI;
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
    public partial class CustomerDeliveryDraft : Form
    {
        MySqlConnection dbconnection, dbconnection1;
        string Store_ID = "0";
        bool loaded = false;
        string permissionNum;
        int flag;
        DataRow addrow = null;
        int rowHandel2 = -1;
        string SelectType = "";
        bool comBranchLoaded=false;
        string branchID = "", BranchName = "";
        string TypeBuy = "";

        public CustomerDeliveryDraft()
        {
            try
            {
                InitializeComponent();
                dbconnection = new MySqlConnection(connection.connectionString);
                dbconnection1 = new MySqlConnection(connection.connectionString);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public CustomerDeliveryDraft(string permissionNum,string branchID,int flag)
        {
            try
            {
                InitializeComponent();
                dbconnection = new MySqlConnection(connection.connectionString);
                dbconnection1 = new MySqlConnection(connection.connectionString);
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
                txtBranchID.Text = branchID;
                string query = "select Branch_Name from branch where Branch_ID=" + txtBranchID.Text;
                MySqlCommand comand = new MySqlCommand(query, dbconnection);
           
                comBranch.Text = comand.ExecuteScalar().ToString();
                this.branchID = branchID;
                this.BranchName = comBranch.Text;
                if (IsBillHavePerviousPermission())
                {
                    displayDatawithPerviousPer();
                }
                else
                {
                    displayData();
                }
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
                string query = "select * from branch";
                MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                DataTable dt = new DataTable();
                da.Fill(dt);
                comBranch.DataSource = dt;
                comBranch.DisplayMember = dt.Columns["Branch_Name"].ToString();
                comBranch.ValueMember = dt.Columns["Branch_ID"].ToString();
                
                comBranch.Text = BranchName;
                txtBranchID.Text = branchID;
               
                query = "select * from store";
                da = new MySqlDataAdapter(query, dbconnection);
                dt = new DataTable();
                da.Fill(dt);
                comStore.DataSource = dt;
                comStore.DisplayMember = dt.Columns["Store_Name"].ToString();
                comStore.ValueMember = dt.Columns["Store_ID"].ToString();
                comStore.Text = "";
                txtStore.Text = "";
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
        private void comStore_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (comBranchLoaded)
                {
                    txtStore.Text = comStore.SelectedValue.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void txtStore_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    dbconnection.Close();
                    string query = "select Store_Name from store where Store_ID=" + txtStore.Text;
                    MySqlCommand comand = new MySqlCommand(query, dbconnection);
                    dbconnection.Open();
                    string Store_Name = comand.ExecuteScalar().ToString();
                    dbconnection.Close();
                    comStore.Text = Store_Name;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
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
                    if (IsBillHavePerviousPermission())
                    {
                        displayDatawithPerviousPer();
                    }
                    else
                    {
                        displayData();
                    }
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

                        DeliveryPermissionClass deliveryPermissionClass = new DeliveryPermissionClass();
                        deliveryPermissionClass.ID = i + 1;
                        deliveryPermissionClass.Data_ID = (int)row1["Data_ID"];

                        deliveryPermissionClass.Code = row1[1].ToString();
                        if (row1["الفئة"].ToString() == "عرض")
                        {
                            string itemName = "concat( product.Product_Name,' ',type.Type_Name,' ',factory.Factory_Name,' ',groupo.Group_Name,' ' ,COALESCE(color.Color_Name,''),' ',COALESCE(size.Size_Value,''),' ',COALESCE(sort.Sort_Value,''),' ',COALESCE(data.Classification,''),' ',COALESCE(data.Description,''))as 'البند'";
                            string DataTableRelations = "INNER JOIN type ON type.Type_ID = data.Type_ID INNER JOIN product ON product.Product_ID = data.Product_ID INNER JOIN factory ON data.Factory_ID = factory.Factory_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID LEFT outer JOIN color ON data.Color_ID = color.Color_ID LEFT outer  JOIN size ON data.Size_ID = size.Size_ID LEFT outer  JOIN sort ON data.Sort_ID = sort.Sort_ID";
                            string query = "select Code as'الكود'," + itemName + " from offer inner join offer_details on offer.Offer_ID=offer_details.Offer_ID inner join data on data.Data_ID=offer_details.Data_ID " + DataTableRelations + "  where offer.Offer_ID=" + (int)row1["Data_ID"];
                            MySqlCommand com = new MySqlCommand(query, dbconnection);
                            MySqlDataReader dr = com.ExecuteReader();
                            string str = "";
                            int cont = 1;
                            while (dr.Read())
                            {
                                str += cont + "-" + dr[1].ToString() + "\n";
                                cont++;
                            }
                            dr.Close();
                            deliveryPermissionClass.ItemName = "-" + row1[2].ToString() + "\n" + str;
                        }
                        else
                        {
                            deliveryPermissionClass.Type = row1[2].ToString();
                            deliveryPermissionClass.ItemName = row1[3].ToString();
                        }
                        deliveryPermissionClass.TotalQuantity = Convert.ToDouble(row1[6]);
                        if (row1[5].ToString() != "" && Convert.ToDouble(row1[5]) != 0)
                        {
                            deliveryPermissionClass.NumOfCarton = Convert.ToDouble(row1[6]) / Convert.ToDouble(row1[5]);
                        }
                        else
                        {
                            deliveryPermissionClass.NumOfCarton = Convert.ToDouble(row1[6]);
                        }
                        deliveryPermissionClass.DeliveryQuantity = row1[7].ToString();
                        deliveryPermissionClass.StoreName = row1[9].ToString();
                        listOfData.Add(deliveryPermissionClass);
                    }
                
                    DeliveryPermissionReport DeliveryPermissionReport = new DeliveryPermissionReport(listOfData,txtCustomerName.Text+" "+txtCustomerID.Text,txtClientName.Text + " " + txtClientID.Text, txtPhoneNumber.Text, txtDelegate.Text, labDate.Text, txtPermBillNumber.Text + "  " + TypeBuy, "" /*id.ToString()*/, txtBranchID.ToString(), comBranch.Text, "", "", "", comStore.Text, true, "");

                    XtraReport1 report = new XtraReport1();
                    ReportPrintTool printTool = new ReportPrintTool(DeliveryPermissionReport);
                    // Invoke the Print dialog.
                    printTool.PrintDialog();
               
                    string query1 = "update customer_bill set RecivedFlag='Draft' where Branch_BillNumber=" + txtPermBillNumber.Text + " and Branch_ID=" + txtBranchID.Text;
                    MySqlCommand com1 = new MySqlCommand(query1, dbconnection);
                    com1.ExecuteNonQuery();
                    UserControl.ItemRecord("customer_bill_Draft_Print", "print"+ txtBranchID.Text, Convert.ToInt16(txtPermBillNumber.Text), DateTime.Now, "", dbconnection);

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
                    string itemName = "type.Type_Name as 'النوع',concat(product.Product_Name,' ',COALESCE(color.Color_Name,''),' ',data.Description,' ',groupo.Group_Name,' ',factory.Factory_Name,' ',COALESCE(size.Size_Value,''),' ',COALESCE(sort.Sort_Value,'')) as 'البند'";
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
                    string itemName = "type.Type_Name as 'النوع',concat(product.Product_Name,' ',COALESCE(color.Color_Name,''),' ',data.Description,' ',groupo.Group_Name,' ',factory.Factory_Name,' ',COALESCE(size.Size_Value,''),' ',COALESCE(sort.Sort_Value,'')) as 'البند'";
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
                displayData();
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
                    string itemName = "concat(type.Type_Name,' ',product.Product_Name,' ',COALESCE(color.Color_Name,''),' ',data.Description,' ',groupo.Group_Name,' ',factory.Factory_Name,' ',COALESCE(size.Size_Value,''),' ',COALESCE(sort.Sort_Value,''),' ',COALESCE(data.Classification,''))as 'البند'";
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
                    string itemName = "concat(type.Type_Name,' ',product.Product_Name,' ',COALESCE(color.Color_Name,''),' ',data.Description,' ',groupo.Group_Name,' ',factory.Factory_Name,' ',COALESCE(size.Size_Value,''),' ',COALESCE(sort.Sort_Value,''),' ',COALESCE(data.Classification,''))as 'البند'";
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
                string itemName = "concat(type.Type_Name,' ',product.Product_Name,' ',COALESCE(color.Color_Name,''),' ',data.Description,' ',groupo.Group_Name,' ',factory.Factory_Name,' ',COALESCE(size.Size_Value,''),' ',COALESCE(sort.Sort_Value,''),' ',COALESCE(data.Classification,''))as 'البند'";
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
        public void displayData()
        {
            string query = "select CustomerBill_ID,Type_Buy from customer_bill where Branch_BillNumber=" + txtPermBillNumber.Text + " and Branch_ID=" + txtBranchID.Text;

            MySqlCommand com = new MySqlCommand(query, dbconnection);
            MySqlDataReader dr = com.ExecuteReader();
            int id=-1;
            while (dr.Read())
            {
               id = Convert.ToInt32(dr[0]);
               TypeBuy = dr[1].ToString();
            }
            dr.Close();

            displayCustomerData(id.ToString());
            gridControl1.DataSource = null;
            gridView1.Columns.Clear();
            DataTable dtAll = new DataTable();
            query = "select data.Data_ID,data.Code as 'الكود',type.Type_Name as 'النوع',concat(product.Product_Name,' ',COALESCE(color.Color_Name,''),' ',data.Description,' ',groupo.Group_Name,' ',factory.Factory_Name,' ',COALESCE(size.Size_Value,''),' ',COALESCE(sort.Sort_Value,'')) as 'البند',product_bill.Type as 'الفئة',data.Carton as 'الكرتنة',product_bill.Quantity as 'الكمية'"/*,(case  when data.Carton !=0 then (product_bill.Quantity /data.Carton) end ) as 'عدد الكراتين'*/+ ",'" + "" + "' as 'الكمية المسلمة',product_bill.Returned as 'تم الاسترجاع',store.Store_Name as 'المخزن',Delegate_Name,product_bill.CustomerBill_ID,product_bill.Store_ID from product_bill inner join data on data.Data_ID=product_bill.Data_ID LEFT JOIN color ON color.Color_ID = data.Color_ID LEFT JOIN size ON size.Size_ID = data.Size_ID LEFT JOIN sort ON sort.Sort_ID = data.Sort_ID INNER JOIN type ON type.Type_ID = data.Type_ID INNER JOIN product ON product.Product_ID = data.Product_ID INNER JOIN factory ON data.Factory_ID = factory.Factory_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID inner join delegate on delegate.Delegate_ID=product_bill.Delegate_ID inner join store on product_bill.Store_ID=store.Store_ID where CustomerBill_ID=" + id + " and product_bill.Type='بند'  and (product_bill.Returned='لا' or product_bill.Returned='جزء')";
            MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
            DataTable dtProduct = new DataTable();
            da.Fill(dtProduct);

            query = "select sets.Set_ID as 'Data_ID',sets.Set_ID as 'الكود',sets.Set_Name as 'الاسم',product_bill.Type as 'الفئة', product_bill.Quantity as 'الكمية','" + " " + " ' as 'الكمية المسلمة',sets.Description as 'الوصف',product_bill.Returned as 'تم الاسترجاع',Delegate_Name,product_bill.CustomerBill_ID,product_bill.Store_ID from product_bill inner join sets on sets.Set_ID=product_bill.Data_ID inner join delegate on delegate.Delegate_ID=product_bill.Delegate_ID where CustomerBill_ID=" + id + " and product_bill.Type='طقم' and (product_bill.Returned='لا' or product_bill.Returned='جزء')";
            da = new MySqlDataAdapter(query, dbconnection);
            DataTable dtSet = new DataTable();
            da.Fill(dtSet);

            query = "select offer.Offer_ID as 'Data_ID',offer.Offer_ID as 'الكود',offer.Offer_Name as 'الاسم',product_bill.Type as 'الفئة', product_bill.Quantity as 'الكمية','" + " " + " ' as 'الكمية المسلمة',offer.Description as 'الوصف',product_bill.Returned as 'تم الاسترجاع',Delegate_Name,product_bill.CustomerBill_ID,product_bill.Store_ID from product_bill inner join offer on offer.Offer_ID=product_bill.Data_ID inner join delegate on delegate.Delegate_ID=product_bill.Delegate_ID where CustomerBill_ID=" + id + " and product_bill.Type='عرض' and (product_bill.Returned='لا' or product_bill.Returned='جزء')";
            da = new MySqlDataAdapter(query, dbconnection);
            DataTable dtOffer = new DataTable();
            da.Fill(dtOffer);

            dtAll = dtProduct.Copy();
            dtAll.Merge(dtSet, true, MissingSchemaAction.Ignore);
            dtAll.Merge(dtOffer, true, MissingSchemaAction.Ignore);
            gridControl1.DataSource = dtAll;
            gridView1.Columns[0].Visible = false;
            gridView1.Columns["CustomerBill_ID"].Visible = false;
            //gridView1.Columns["الفئة"].Visible = false;
            //gridView1.Columns["الوصف"].Visible = false;
            gridView1.Columns["Delegate_Name"].Visible = false;
            gridView1.Columns["Store_ID"].Visible = false;
            txtDelegate.Text = gridView1.GetDataRow(0)["Delegate_Name"].ToString();
        }
        public void displayCustomerData(string CustomerBill_ID)
        {
            string query = "select c1.Customer_ID,c1.Customer_Name,c2.Customer_ID,c2.Customer_Name,cc1.Phone,cc2.Phone,Bill_Date from customer_bill left join  customer as c1  on c1.Customer_ID=customer_bill.Customer_ID left join customer as c2 on c2.Customer_ID=customer_bill.Client_ID left join customer_phone as cc1 on cc1.Customer_ID=c1.Customer_ID left join customer_phone as cc2 on cc2.Customer_ID=c2.Customer_ID where CustomerBill_ID=" + CustomerBill_ID;
            MySqlCommand com = new MySqlCommand(query, dbconnection);
            MySqlDataReader dr = com.ExecuteReader();
            while (dr.Read())
            {
                txtCustomerID.Text = dr[0].ToString();
                txtCustomerName.Text = dr[1].ToString();
                txtClientID.Text = dr[2].ToString();
                txtClientName.Text = dr[3].ToString();
                if (txtCustomerID.Text!="")
                    txtPhoneNumber.Text = dr[4].ToString();
                if (txtClientID.Text != "")
                    txtPhoneNumber.Text = dr[5].ToString();
                labDate.Text = dr[6].ToString();
            }
            dr.Close();
        }
        public bool IsBillHavePerviousPermission()
        {
            string query = "select Customer_Permissin_ID from customer_permissions where BranchBillNumber=" + txtPermBillNumber.Text + " and Branch_ID=" + txtBranchID.Text;
            MySqlCommand com = new MySqlCommand(query, dbconnection);
            MySqlDataReader dr = com.ExecuteReader();
            while (dr.Read())
            {
                dr.Close();
                return true;
            }
            dr.Close();

            return false;
        }
        public void displayDatawithPerviousPer()
        {
            string query = "select CustomerBill_ID from customer_bill where Branch_BillNumber=" + txtPermBillNumber.Text + " and Branch_ID=" + txtBranchID.Text;

            MySqlCommand com = new MySqlCommand(query, dbconnection);

            int id = Convert.ToInt32(com.ExecuteScalar());
            displayCustomerData(id.ToString());
            gridControl1.DataSource = null;
            gridView1.Columns.Clear();
            DataTable dtAll = new DataTable();
            string itemName = "type.Type_Name as 'النوع',concat(product.Product_Name,' ',COALESCE(color.Color_Name,''),' ',data.Description,' ',groupo.Group_Name,' ',factory.Factory_Name,' ',COALESCE(size.Size_Value,''),' ',COALESCE(sort.Sort_Value,'')) as 'البند'";

            query = "select data.Data_ID,data.Code as 'الكود',"+itemName+",customer_permissions_details.ItemType as 'الفئة',customer_permissions_details.Carton as 'الكرتنة',customer_permissions_details.Quantity as 'الكمية',sum(customer_permissions_details.DeliveredQuantity) as 'الكمية المستلمة' from customer_permissions_details inner join data on data.Data_ID=customer_permissions_details.Data_ID LEFT JOIN color ON color.Color_ID = data.Color_ID LEFT JOIN size ON size.Size_ID = data.Size_ID LEFT JOIN sort ON sort.Sort_ID = data.Sort_ID INNER JOIN type ON type.Type_ID = data.Type_ID INNER JOIN product ON product.Product_ID = data.Product_ID INNER JOIN factory ON data.Factory_ID = factory.Factory_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID inner join customer_permissions on  customer_permissions.Customer_Permissin_ID=customer_permissions_details.Customer_Permissin_ID where customer_permissions_details.ItemType='بند' and BranchBillNumber=" + txtPermBillNumber.Text + " and Branch_ID=" + txtBranchID.Text + " group by data.Data_ID";
            MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
            DataTable dtper = new DataTable();
            da.Fill(dtper);
            dtAll = dtper.Copy();

            query = "select sets.Set_ID as 'Data_ID',sets.Set_ID as 'الكود',sets.Set_Name as 'الاسم',customer_permissions_details.ItemType as 'الفئة',customer_permissions_details.Carton as 'الكرتنة',customer_permissions_details.Quantity as 'الكمية',sum(customer_permissions_details.DeliveredQuantity) as 'الكمية المستلمة' from customer_permissions_details inner join sets on sets.Set_ID=customer_permissions_details.Data_ID inner join customer_permissions on  customer_permissions.Customer_Permissin_ID=customer_permissions_details.Customer_Permissin_ID where customer_permissions_details.ItemType='طقم' and BranchBillNumber=" + txtPermBillNumber.Text + " and Branch_ID=" + txtBranchID.Text + " group by sets.Set_ID";
            da = new MySqlDataAdapter(query, dbconnection);
            DataTable dtper1 = new DataTable();
            da.Fill(dtper1);

            query = "select offer.Offer_ID as 'Data_ID',offer.Offer_ID as 'الكود',offer.Offer_Name as 'الاسم',customer_permissions_details.ItemType as 'الفئة',customer_permissions_details.Carton as 'الكرتنة',customer_permissions_details.Quantity as 'الكمية',sum(customer_permissions_details.DeliveredQuantity) as 'الكمية المستلمة' from customer_permissions_details inner join offer on offer.Offer_ID=customer_permissions_details.Data_ID inner join customer_permissions on  customer_permissions.Customer_Permissin_ID=customer_permissions_details.Customer_Permissin_ID where customer_permissions_details.ItemType='عرض' and BranchBillNumber=" + txtPermBillNumber.Text + " and Branch_ID=" + txtBranchID.Text + " group by offer.Offer_ID";
            da = new MySqlDataAdapter(query, dbconnection);
            DataTable dtper2 = new DataTable();
            da.Fill(dtper2);

            dtAll.Merge(dtper1, true, MissingSchemaAction.Ignore);
            string re = getDeliveredDataItems("بند");
            if (re != "")
            {
                query = "select data.Data_ID,data.Code as 'الكود',"+itemName+",product_bill.Type as 'الفئة',product_bill.Cartons as 'الكرتنة',product_bill.Quantity as 'الكمية' , '" + 0 + " ' as 'الكمية المسلمة' from product_bill inner join data on data.Data_ID=product_bill.Data_ID LEFT JOIN color ON color.Color_ID = data.Color_ID LEFT JOIN size ON size.Size_ID = data.Size_ID LEFT JOIN sort ON sort.Sort_ID = data.Sort_ID INNER JOIN type ON type.Type_ID = data.Type_ID INNER JOIN product ON product.Product_ID = data.Product_ID INNER JOIN factory ON data.Factory_ID = factory.Factory_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID inner join delegate on delegate.Delegate_ID=product_bill.Delegate_ID  where CustomerBill_ID=" + id + " and product_bill.Type='بند' and product_bill.Data_ID not in ( " + re + ") ";// and (product_bill.Returned='لا' or product_bill.Returned='جزء')";
                da = new MySqlDataAdapter(query, dbconnection);
                DataTable dtProduct = new DataTable();
                da.Fill(dtProduct);
                dtAll.Merge(dtProduct, true, MissingSchemaAction.Ignore);
            }

            re = getDeliveredDataItems("طقم");
            if (re != "")
            {
                query = "select sets.Set_ID as 'Data_ID',sets.Set_ID as 'الكود',sets.Set_Name as 'الاسم',product_bill.Type as 'الفئة', product_bill.Quantity as 'الكمية', '" + 0 + " ' as 'الكمية المسلمة' from product_bill inner join sets on sets.Set_ID=product_bill.Data_ID inner join delegate on delegate.Delegate_ID=product_bill.Delegate_ID where CustomerBill_ID=" + id + " and product_bill.Type='طقم' and Set_ID not in (" + re + ")";// and (product_bill.Returned='لا' or product_bill.Returned='جزء')";
                da = new MySqlDataAdapter(query, dbconnection);
                DataTable dtSet = new DataTable();
                da.Fill(dtSet);
                dtAll.Merge(dtSet, true, MissingSchemaAction.Ignore);
            }

            re = getDeliveredDataItems("عرض");
            if (re != "")
            {
                query = "select offer.Offer_ID as 'Data_ID',offer.Offer_ID as 'الكود',offer.Offer_Name as 'الاسم',customer_permissions_details.ItemType as 'الفئة',customer_permissions_details.Carton as 'الكرتنة',customer_permissions_details.Quantity as 'الكمية',sum(customer_permissions_details.DeliveredQuantity) as 'الكمية المستلمة' from customer_permissions_details inner join offer on offer.Offer_ID=customer_permissions_details.Data_ID inner join customer_permissions on  customer_permissions.Customer_Permissin_ID=customer_permissions_details.Customer_Permissin_ID where customer_permissions_details.ItemType='عرض' and BranchBillNumber=" + txtPermBillNumber.Text + " and Branch_ID=" + txtBranchID.Text + " group by offer.Offer_ID";
                da = new MySqlDataAdapter(query, dbconnection);
                DataTable dtOffer = new DataTable();
                da.Fill(dtOffer);
                dtAll.Merge(dtOffer, true, MissingSchemaAction.Ignore);
            }

            gridControl1.DataSource = dtAll;
            gridView1.Columns[0].Visible = false;
            gridView1.Columns["الفئة"].Visible = false;
            //gridView1.Columns["الوصف"].Visible = false;
            //gridView1.Columns["Delegate_Name"].Visible = false;
            //gridView1.Columns["Store_ID"].Visible = false;
            //txtDelegate.Text = gridView1.GetDataRow(0)["Delegate_Name"].ToString();
        }
        public string getDeliveredDataItems(string type)
        {
            string query = "select group_concat(distinct Data_ID) from customer_permissions_details inner join customer_permissions on customer_permissions.Customer_Permissin_ID=customer_permissions_details.Customer_Permissin_ID where ItemType='" + type+"' and BranchBillNumber=" + txtPermBillNumber.Text + " and Branch_ID=" + txtBranchID.Text;
            MySqlCommand com = new MySqlCommand(query,dbconnection);

            string result = com.ExecuteScalar().ToString();

            return result;
        }
        public void IsBillRecived()
        {
            DataTable dtAll = new DataTable();
            string query = "select data.Data_ID,data.Code as 'الكود',customer_permissions_details.ItemType as 'الفئة',customer_permissions_details.Carton as 'الكرتنة',customer_permissions_details.Quantity as 'الكمية',sum(customer_permissions_details.DeliveredQuantity) as 'الكمية المستلمة' from customer_permissions_details inner join data on data.Data_ID=customer_permissions_details.Data_ID  inner join customer_permissions on  customer_permissions.Customer_Permissin_ID=customer_permissions_details.Customer_Permissin_ID where customer_permissions_details.ItemType='بند' and BranchBillNumber=" + txtPermBillNumber.Text + " and Branch_ID=" + txtBranchID.Text + " group by data.Data_ID";
            MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
            DataTable dtper = new DataTable();
            da.Fill(dtper);
            dtAll = dtper.Copy();

            query = "select sets.Set_ID as 'Data_ID',sets.Set_ID as 'الكود',customer_permissions_details.ItemType as 'الفئة',customer_permissions_details.Carton as 'الكرتنة',customer_permissions_details.Quantity as 'الكمية',sum(customer_permissions_details.DeliveredQuantity) as 'الكمية المستلمة' from customer_permissions_details inner join sets on sets.Set_ID=customer_permissions_details.Data_ID inner join customer_permissions on  customer_permissions.Customer_Permissin_ID=customer_permissions_details.Customer_Permissin_ID where customer_permissions_details.ItemType='طقم' and BranchBillNumber=" + txtPermBillNumber.Text + " and Branch_ID=" + txtBranchID.Text + " group by sets.Set_ID";
            da = new MySqlDataAdapter(query, dbconnection);
            DataTable dtper1 = new DataTable();
            da.Fill(dtper1);
            dtAll.Merge(dtper1, true, MissingSchemaAction.Ignore);
       

            bool flag = true;
            dbconnection.Close();
            if (IsQuantityEqual())
            {
                dbconnection.Open();
                foreach (DataRow item in dtAll.Rows)
                {
                    if (Convert.ToDouble(item[4]) > Convert.ToDouble(item[5]))
                    {
                        flag = false;
                        query = "update customer_bill set RecivedFlag='تم تسليم جزء' where Branch_BillNumber=" + txtPermBillNumber.Text + " and Branch_ID=" + txtBranchID.Text;
                        MySqlCommand com = new MySqlCommand(query, dbconnection);
                        com.ExecuteNonQuery();
                    }

                }
                if (flag)
                {
                    query = "update customer_bill set RecivedFlag='تم' where Branch_BillNumber=" + txtPermBillNumber.Text + " and Branch_ID=" + txtBranchID.Text;
                    MySqlCommand com = new MySqlCommand(query, dbconnection);
                    com.ExecuteNonQuery();
                }
            }
            else
            {
                dbconnection.Open();
                query = "update customer_bill set RecivedFlag='تم تسليم جزء' where Branch_BillNumber=" + txtPermBillNumber.Text + " and Branch_ID=" + txtBranchID.Text;
                MySqlCommand com = new MySqlCommand(query, dbconnection);
                com.ExecuteNonQuery();
            }
         
        }
        public bool IsDelveryQuantityHaveValue(string x)
        {
            try
            {
                Convert.ToDouble(x);
                return true;
            }
            catch 
            {
                return false;
            }
        }
        public bool IsQuantityEqual()
        {
            dbconnection.Open();
            //check number of records
            string query = "select CustomerBill_ID from customer_bill where Branch_BillNumber=" + txtPermBillNumber.Text + " and Branch_ID=" + txtBranchID.Text;
            MySqlCommand com1 = new MySqlCommand(query, dbconnection);
            int id = Convert.ToInt16(com1.ExecuteScalar());

            query = "select count(*) from product_bill where CustomerBill_ID=" + id;
            com1 = new MySqlCommand(query, dbconnection);
            int count1 = Convert.ToInt16(com1.ExecuteScalar());

            query = "select Customer_Permissin_ID from customer_permissions where CustomerBill_ID=" + id;
            com1 = new MySqlCommand(query, dbconnection);
            MySqlDataReader dr = com1.ExecuteReader();
            string str = "";
            while (dr.Read())
            {
                str+=dr[0].ToString()+",";
            }
            dr.Close();
            str += "0";
            query = "select count(*) from customer_permissions_details where Customer_Permissin_ID in (" + str +")";
            com1 = new MySqlCommand(query, dbconnection);
            int count2 = Convert.ToInt16(com1.ExecuteScalar());

            dbconnection.Close();
            if (count1 == count2)
                return true;
            else
                return false;
        }
        public void updateRecivedFlag(int customerBill_ID,int Data_ID,string flag)
        {
            dbconnection1.Open();
            string query = "update product_bill set Recived_Flag='"+flag+ "' where CustomerBill_ID="+customerBill_ID+" and Data_ID="+Data_ID;
            MySqlCommand com = new MySqlCommand(query,dbconnection1);
            com.ExecuteNonQuery();
            dbconnection1.Close();
        }
        public void checkIfItemRecivedTotaly(int customerBill_ID)
        {
            dbconnection.Close();
            dbconnection.Open();
            string query = "select group_concat(Customer_Permissin_ID) from customer_permissions where CustomerBill_ID=" + customerBill_ID;
            MySqlCommand com = new MySqlCommand(query, dbconnection);
            string Customer_Permissin_IDs =com.ExecuteScalar().ToString();

            query = "select group_concat(Data_ID) from product_bill where CustomerBill_ID=" + customerBill_ID;
            com = new MySqlCommand(query, dbconnection);
            string product_bills = com.ExecuteScalar().ToString();
            string[] strarr = product_bills.Split(',');
            for (int i = 0; i < strarr.Length; i++)
            {
                query = "select sum(DeliveredQuantity),Quantity from customer_permissions_details where Customer_Permissin_ID in (" + Customer_Permissin_IDs + ") and Data_ID=" + strarr[i];
                com = new MySqlCommand(query, dbconnection);
                MySqlDataReader dr = com.ExecuteReader();
                while (dr.Read())
                {
                    if (dr[0].ToString() == dr[1].ToString()&& dr[1].ToString()!="")
                    {
                        updateRecivedFlag(customerBill_ID, Convert.ToInt16(strarr[i]), "تم");
                    }
                    else
                    {
                        updateRecivedFlag(customerBill_ID, Convert.ToInt16(strarr[i]), "تم تسليم جزء");
                    }
                }
                dr.Close();
            }
       
        }
        public void checkIfBillRecivedTotaly(int customerBill_ID)
        {
            bool flag = true;
            dbconnection1.Open();
            string query = "select Recived_Flag from product_bill where CustomerBill_ID=" + customerBill_ID;
            MySqlCommand com = new MySqlCommand(query, dbconnection);
            MySqlDataReader dr = com.ExecuteReader();
            while (dr.Read())
            {
                if (dr[0].ToString() != "تم")
                {
                    query = "update customer_bill set RecivedFlag='تم تسليم جزء' where CustomerBill_ID=" + customerBill_ID ;
                    com = new MySqlCommand(query, dbconnection1);
                    com.ExecuteNonQuery();
                    flag = false;
                }
            }
            dr.Close();

            if (flag)
            {
                query = "update customer_bill set RecivedFlag='تم' where CustomerBill_ID=" + customerBill_ID;
                com = new MySqlCommand(query, dbconnection1);
                com.ExecuteNonQuery();
            }
            dbconnection1.Close();
        }

    }
    
}

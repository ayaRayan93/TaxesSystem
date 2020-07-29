using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Grid;
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

namespace TaxesSystem
{
    public partial class DashRequest_Record : Form
    {
        MySqlConnection dbconnection, dbconnection2, dbconnection3;
        bool loaded = false;
        int orderNumber = 1;
        int orderId = 0;
        bool addFlage = true;
        bool flagRequestNum = false;

        public DashRequest_Record(DashRequest_Report OrderReport, XtraTabControl xtraTabControlPurchases)
        {
            InitializeComponent();
            dbconnection = new MySqlConnection(connection.connectionString);
            dbconnection2 = new MySqlConnection(connection.connectionString);
            dbconnection3 = new MySqlConnection(connection.connectionString);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                DataHelperRequest dh = new DataHelperRequest(DSparametrRequest.doubleDS);
                gridControl2.DataSource = dh.DataSet;
                gridControl2.DataMember = dh.DataMember;
                gridView2.InitNewRow += GridView2_InitNewRow;
                
                string query = "select * from supplier";
                MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                DataTable dt = new DataTable();
                da.Fill(dt);
                comSupplier.DataSource = dt;
                comSupplier.DisplayMember = dt.Columns["Supplier_Name"].ToString();
                comSupplier.ValueMember = dt.Columns["Supplier_ID"].ToString();
                comSupplier.SelectedIndex = -1;
                txtSupplier.Text = "";

                query = "select * from factory";
                da = new MySqlDataAdapter(query, dbconnection);
                dt = new DataTable();
                da.Fill(dt);
                comFactory.DataSource = dt;
                comFactory.DisplayMember = dt.Columns["Factory_Name"].ToString();
                comFactory.ValueMember = dt.Columns["Factory_ID"].ToString();
                comFactory.SelectedIndex = -1;
                txtFactory.Text = "";

                query = "select * from store";
                da = new MySqlDataAdapter(query, dbconnection);
                dt = new DataTable();
                da.Fill(dt);
                comStore.DataSource = dt;
                comStore.DisplayMember = dt.Columns["Store_Name"].ToString();
                comStore.ValueMember = dt.Columns["Store_ID"].ToString();
                comStore.SelectedIndex = -1;
                txtStoreID.Text = "";

                query = "select * from customer where Customer_Type<>'عميل'";
                da = new MySqlDataAdapter(query, dbconnection);
                dt = new DataTable();
                da.Fill(dt);
                comEngCon.DataSource = dt;
                comEngCon.DisplayMember = dt.Columns["Customer_Name"].ToString();
                comEngCon.ValueMember = dt.Columns["Customer_ID"].ToString();
                comEngCon.Text = "";

                query = "select * from customer where Customer_Type='عميل'";
                da = new MySqlDataAdapter(query, dbconnection);
                dt = new DataTable();
                da.Fill(dt);
                comClient.DataSource = dt;
                comClient.DisplayMember = dt.Columns["Customer_Name"].ToString();
                comClient.ValueMember = dt.Columns["Customer_ID"].ToString();
                comClient.Text = "";

                loaded = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
            dbconnection2.Close();
        }

        private void GridView2_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            GridView view = sender as GridView;
            view.SetRowCellValue(e.RowHandle, gridView2.Columns[0], "");
            view.SetRowCellValue(e.RowHandle, gridView2.Columns[1], "");
        }

        private void comBox_SelectedValueChanged(object sender, EventArgs e)
        {
            if (loaded)
            {
                try
                {
                    ComboBox comBox = (ComboBox)sender;

                    switch (comBox.Name)
                    {
                        case "comFactory":
                            txtFactory.Text = comFactory.SelectedValue.ToString();
                            if (comFactory.SelectedValue != null)
                            {
                                flagRequestNum = false;
                                dbconnection.Close();
                                dbconnection.Open();
                                string query = "select Dash_Order_Number from dash_orders where Factory_ID=" + comFactory.SelectedValue.ToString() + " order by DashOrder_ID desc limit 1";
                                MySqlCommand com = new MySqlCommand(query, dbconnection);
                                if (com.ExecuteScalar() != null)
                                {
                                    orderNumber = Convert.ToInt32(com.ExecuteScalar().ToString());
                                }
                                else
                                {
                                    orderNumber = 1;
                                }
                                dbconnection.Close();

                                txtOrderNum.Text = "";
                                loaded = true;
                                flagRequestNum = true;
                                txtOrderNum.Text = orderNumber.ToString();
                            }
                            ////////////////////////////////////////////
                            txtOrderNum.Focus();
                            break;
                        case "comSupplier":
                            txtSupplier.Text = comSupplier.SelectedValue.ToString();
                            break;
                        case "comStore":
                            txtStoreID.Text = comStore.SelectedValue.ToString();
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
        private void txtBox_KeyDown(object sender, KeyEventArgs e)
        {
            TextBox txtBox = (TextBox)sender;
            string query;
            MySqlCommand com;
            string Name;

            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    if (txtBox.Text != "")
                    {
                        dbconnection.Open();
                        switch (txtBox.Name)
                        {
                            case "txtFactory":
                                query = "select Factory_Name from factory where Factory_ID='" + txtFactory.Text + "'";
                                com = new MySqlCommand(query, dbconnection);
                                if (com.ExecuteScalar() != null)
                                {
                                    Name = (string)com.ExecuteScalar();
                                    comFactory.Text = Name;
                                    comFactory.SelectedValue = txtFactory.Text;
                                }
                                else
                                {
                                    MessageBox.Show("there is no item with this id");
                                    comFactory.SelectedIndex = -1;
                                    dbconnection.Close();
                                    return;
                                }
                                break;
                            case "txtStoreID":
                                query = "select Store_Name from store where Store_ID=" + txtStoreID.Text;
                                com = new MySqlCommand(query, dbconnection);
                                if (com.ExecuteScalar() != null)
                                {
                                    Name = (string)com.ExecuteScalar();
                                    comStore.Text = Name;
                                    comStore.SelectedValue = txtStoreID.Text;
                                }
                                else
                                {
                                    MessageBox.Show("there is no item with this id");
                                    comStore.SelectedIndex = -1;
                                    dbconnection.Close();
                                    return;
                                }
                                break;
                            case "txtSupplier":
                                query = "select Supplier_Name from supplier where Supplier_ID=" + txtSupplier.Text + "";
                                com = new MySqlCommand(query, dbconnection);
                                if (com.ExecuteScalar() != null)
                                {
                                    Name = (string)com.ExecuteScalar();
                                    comSupplier.Text = Name;
                                    comSupplier.SelectedValue = txtSupplier.Text;
                                }
                                else
                                {
                                    MessageBox.Show("there is no item with this id");
                                    comSupplier.SelectedIndex = -1;
                                    dbconnection.Close();
                                    return;
                                }
                                break;
                        }
                    }
                }
                catch
                {
                    // MessageBox.Show(ex.ToString());
                }
                dbconnection.Close();
            }
        }

        private void txtRequestNum_TextChanged(object sender, EventArgs e)
        {
            if (loaded && flagRequestNum && txtOrderNum.Text != "")
            {
                try
                {
                    if (int.TryParse(txtOrderNum.Text, out orderNumber))
                    {
                        if (comFactory.SelectedValue != null)
                        {
                            int cont = gridView2.RowCount;
                            for (int i = 0; i < cont; i++)
                            {
                                int rowHandle = gridView2.GetRowHandle(0);
                                gridView2.DeleteRow(rowHandle);
                            }
                            dbconnection.Open();
                            dbconnection3.Open();
                            string query = "SELECT dash_orders.DashOrder_ID,dash_orders.Store_ID,dash_orders.Employee_Name,dash_orders.Supplier_ID,dash_orders.Request_Date,dash_orders.Confirmed,dash_orders.Canceled,dash_orders.Saved,dash_orders.SpecialOrder_ID FROM dash_orders left join dash_order_details ON dash_order_details.DashOrder_ID = dash_orders.DashOrder_ID where dash_orders.Factory_ID=" + comFactory.SelectedValue.ToString() + " and dash_orders.Dash_Order_Number=" + orderNumber;
                            MySqlCommand comand = new MySqlCommand(query, dbconnection3);
                            MySqlDataReader dr = comand.ExecuteReader();
                            if(dr.HasRows)
                            {
                                while(dr.Read())
                                {
                                    if (dr["SpecialOrder_ID"].ToString() == "" || dr["Saved"].ToString() == "1" || dr["Confirmed"].ToString() == "1" || dr["Canceled"].ToString() == "1")
                                    {
                                        addFlage = false;
                                        loaded = false;
                                        txtEmployee.ReadOnly = true;
                                        comStore.Enabled = false;
                                        txtStoreID.ReadOnly = true;
                                        comSupplier.Enabled = false;
                                        txtSupplier.ReadOnly = true;
                                        txtSpecialOrderNo.ReadOnly = true;
                                        orderId = Convert.ToInt32(dr["DashOrder_ID"].ToString());
                                        txtEmployee.Text = dr["Employee_Name"].ToString();
                                        txtSpecialOrderNo.Text = dr["SpecialOrder_ID"].ToString();
                                        //search();
                                        comStore.SelectedValue = dr["Store_ID"].ToString();
                                        txtStoreID.Text = dr["Store_ID"].ToString();
                                        if (dr["Supplier_ID"].ToString() != "")
                                        {
                                            comSupplier.SelectedValue = dr["Supplier_ID"].ToString();
                                            txtSupplier.Text = dr["Supplier_ID"].ToString();
                                        }
                                        else
                                        {
                                            comSupplier.SelectedIndex = -1;
                                            txtSupplier.Text = "";
                                        }
                                        loaded = true;
                                    }
                                    else
                                    {
                                        addFlage = true;
                                        loaded = false;
                                        txtEmployee.ReadOnly = true;
                                        comStore.Enabled = false;
                                        txtStoreID.ReadOnly = true;
                                        comSupplier.Enabled = false;
                                        txtSupplier.ReadOnly = true;
                                        txtSpecialOrderNo.ReadOnly = true;
                                        orderId = Convert.ToInt32(dr["DashOrder_ID"].ToString());
                                        txtEmployee.Text = dr["Employee_Name"].ToString();
                                        txtSpecialOrderNo.Text = dr["SpecialOrder_ID"].ToString();
                                        dbconnection2.Close();
                                        dbconnection2.Open();
                                        search();
                                        comStore.SelectedValue = dr["Store_ID"].ToString();
                                        txtStoreID.Text = dr["Store_ID"].ToString();
                                        if (dr["Supplier_ID"].ToString() != "")
                                        {
                                            comSupplier.SelectedValue = dr["Supplier_ID"].ToString();
                                            txtSupplier.Text = dr["Supplier_ID"].ToString();
                                        }
                                        else
                                        {
                                            comSupplier.SelectedIndex = -1;
                                            txtSupplier.Text = "";
                                        }
                                        loaded = true;
                                    }
                                }
                                dr.Close();
                            }
                            else
                            {
                                addFlage = true;
                                loaded = false;
                                orderId = 0;
                                txtEmployee.Text = "";
                                txtSpecialOrderNo.Text = "";
                                clearClient();
                                comStore.SelectedIndex = -1;
                                txtStoreID.Text = "";
                                comSupplier.SelectedIndex = -1;
                                txtSupplier.Text = "";
                                txtEmployee.ReadOnly = false;
                                txtSpecialOrderNo.ReadOnly = false;
                                comStore.Enabled = true;
                                txtStoreID.ReadOnly = false;
                                comSupplier.Enabled = true;
                                txtSupplier.ReadOnly = false;
                                int cont2 = gridView2.RowCount;
                                for (int i = 0; i < cont2; i++)
                                {
                                    int rowHandle = gridView2.GetRowHandle(0);
                                    gridView2.DeleteRow(rowHandle);
                                }
                                loaded = true;
                            }

                            query = "SELECT dash_order_details.Item_Description as 'الاسم',dash_order_details.Quantity as 'عدد المتر/القطعة' FROM dash_orders inner join dash_order_details ON dash_order_details.DashOrder_ID = dash_orders.DashOrder_ID where dash_orders.Factory_ID=" + comFactory.SelectedValue.ToString() + " and dash_orders.Dash_Order_Number=" + orderNumber + " and dash_orders.Saved=0 and dash_orders.Confirmed=0 and dash_orders.Canceled=0 and dash_orders.SpecialOrder_ID is not null";
                            comand = new MySqlCommand(query, dbconnection);
                            dr = comand.ExecuteReader();
                            if (dr.HasRows)
                            {
                                dbconnection2.Close();
                                dbconnection2.Open();
                                while (dr.Read())
                                {
                                    gridView2.AddNewRow();
                                    int rowHandle = gridView2.GetRowHandle(gridView2.DataRowCount);
                                    if (gridView2.IsNewItemRow(rowHandle))
                                    {
                                        gridView2.SetRowCellValue(rowHandle, gridView2.Columns["ItemName"], dr["الاسم"]);
                                        gridView2.SetRowCellValue(rowHandle, gridView2.Columns["TotalQuantity"], dr["عدد المتر/القطعة"]);
                                    }
                                }
                            }
                            dr.Close();
                        }
                        else
                        {
                            MessageBox.Show("يجب تحديد المورد");

                            loaded = false;
                            orderId = 0;
                            txtEmployee.Text = "";
                            txtSpecialOrderNo.Text = "";
                            clearClient();
                            comStore.SelectedIndex = -1;
                            txtStoreID.Text = "";
                            comSupplier.SelectedIndex = -1;
                            txtSupplier.Text = "";
                            txtEmployee.ReadOnly = false;
                            txtSpecialOrderNo.ReadOnly = false;
                            comStore.Enabled = true;
                            txtStoreID.ReadOnly = false;
                            comSupplier.Enabled = true;
                            txtSupplier.ReadOnly = false;
                            int cont = gridView2.RowCount;
                            for (int i = 0; i < cont; i++)
                            {
                                int rowHandle = gridView2.GetRowHandle(0);
                                gridView2.DeleteRow(rowHandle);
                            }
                            loaded = true;
                        }
                    }
                    else
                    {
                        MessageBox.Show("رقم الطلب يجب ان يكون عدد");

                        loaded = false;
                        orderId = 0;
                        txtEmployee.Text = "";
                        txtSpecialOrderNo.Text = "";
                        clearClient();
                        comStore.SelectedIndex = -1;
                        txtStoreID.Text = "";
                        comSupplier.SelectedIndex = -1;
                        txtSupplier.Text = "";
                        txtEmployee.ReadOnly = false;
                        txtSpecialOrderNo.ReadOnly = false;
                        comStore.Enabled = true;
                        txtStoreID.ReadOnly = false;
                        comSupplier.Enabled = true;
                        txtSupplier.ReadOnly = false;
                        int cont = gridView2.RowCount;
                        for (int i = 0; i < cont; i++)
                        {
                            int rowHandle = gridView2.GetRowHandle(0);
                            gridView2.DeleteRow(rowHandle);
                        }
                        loaded = true;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                dbconnection.Close();
                dbconnection2.Close();
                dbconnection3.Close();
            }
            else
            {
                loaded = false;
                orderId = 0;
                txtEmployee.Text = "";
                txtSpecialOrderNo.Text = "";
                clearClient();
                comStore.SelectedIndex = -1;
                txtStoreID.Text = "";
                comSupplier.SelectedIndex = -1;
                txtSupplier.Text = "";
                txtEmployee.ReadOnly = false;
                txtSpecialOrderNo.ReadOnly = false;
                comStore.Enabled = true;
                txtStoreID.ReadOnly = false;
                comSupplier.Enabled = true;
                txtSupplier.ReadOnly = false;
                int cont = gridView2.RowCount;
                for (int i = 0; i < cont; i++)
                {
                    int rowHandle = gridView2.GetRowHandle(0);
                    gridView2.DeleteRow(rowHandle);
                }
                loaded = true;
            }
        }

        /*private void gridView1_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            try
            {
                row1 = layoutView1.GetDataRow(layoutView1.GetRowHandle(e.RowHandle));
                rowHandl = e.RowHandle;
                txtTotalMeters.Text = "0";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }*/

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (addFlage && txtTotalMeters.Text != "" && txtSpecialOrderNo.Text != "" && txtEmployee.Text != "" && txtSpecialOrderNo.Text != "" && comStore.SelectedValue != null && comFactory.SelectedValue != null)
                {
                    double total;
                    if (double.TryParse(txtTotalMeters.Text, out total))
                    {
                        if (IsItemAdded())
                        {
                            MessageBox.Show("هذا العنصر تم اضافتة من قبل");
                            return;
                        }
                        dbconnection.Open();
                        if (orderId == 0)
                        {
                            string query2 = "insert into dash_orders (Factory_ID,Supplier_ID,Dash_Order_Number,Store_ID,Employee_Name,Request_Date,Employee_ID,SpecialOrder_ID)values(@Factory_ID,@Supplier_ID,@Dash_Order_Number,@Store_ID,@Employee_Name,@Request_Date,@Employee_ID,@SpecialOrder_ID)";
                            MySqlCommand com2 = new MySqlCommand(query2, dbconnection);

                            com2.Parameters.Add("@Factory_ID", MySqlDbType.Int16);
                            com2.Parameters["@Factory_ID"].Value = comFactory.SelectedValue.ToString();
                            com2.Parameters.Add("@Dash_Order_Number", MySqlDbType.Int16);
                            com2.Parameters["@Dash_Order_Number"].Value = orderNumber;
                            com2.Parameters.Add("@Employee_Name", MySqlDbType.VarChar);
                            com2.Parameters["@Employee_Name"].Value = txtEmployee.Text;
                            com2.Parameters.Add("@Request_Date", MySqlDbType.Date);
                            com2.Parameters["@Request_Date"].Value = DateTime.Now.Date; //dateTimePicker1.Value.Date;
                            if (comSupplier.SelectedValue != null)
                            {
                                com2.Parameters.Add("@Supplier_ID", MySqlDbType.Int16);
                                com2.Parameters["@Supplier_ID"].Value = comSupplier.SelectedValue.ToString();
                            }
                            else
                            {
                                com2.Parameters.Add("@Supplier_ID", MySqlDbType.Int16);
                                com2.Parameters["@Supplier_ID"].Value = null;
                            }
                            com2.Parameters.Add("@Store_ID", MySqlDbType.Int16);
                            com2.Parameters["@Store_ID"].Value = comStore.SelectedValue.ToString();
                            com2.Parameters.Add("@Employee_ID", MySqlDbType.Int16);
                            com2.Parameters["@Employee_ID"].Value = UserControl.EmpID;
                            com2.Parameters.Add("@SpecialOrder_ID", MySqlDbType.Int16);
                            com2.Parameters["@SpecialOrder_ID"].Value = txtSpecialOrderNo.Text;
                            com2.ExecuteNonQuery();

                            query2 = "select DashOrder_ID from dash_orders order by DashOrder_ID desc limit 1";
                            com2 = new MySqlCommand(query2, dbconnection);

                            orderId = Convert.ToInt32(com2.ExecuteScalar().ToString());
                        }
                        
                        string query = "insert into dash_order_details (DashOrder_ID,Quantity,Type,Item_Description) values (@DashOrder_ID,@Quantity,@Type,@Item_Description)";
                        MySqlCommand com = new MySqlCommand(query, dbconnection);
                        com.Parameters.Add("@DashOrder_ID", MySqlDbType.Int16);
                        com.Parameters["@DashOrder_ID"].Value = orderId;
                        com.Parameters.Add("@Quantity", MySqlDbType.Double);
                        com.Parameters["@Quantity"].Value = total;
                        com.Parameters.Add("@Type", MySqlDbType.VarChar);
                        com.Parameters["@Type"].Value = "خاص";
                        com.Parameters.Add("@Item_Description", MySqlDbType.VarChar);
                        com.Parameters["@Item_Description"].Value = txtName.Text;
                        com.ExecuteNonQuery();

                        gridView2.AddNewRow();
                        int rowHandle = gridView2.GetRowHandle(gridView2.DataRowCount);
                        if (gridView2.IsNewItemRow(rowHandle))
                        {
                            gridView2.SetRowCellValue(rowHandle, gridView2.Columns["ItemName"], txtName.Text);
                            gridView2.SetRowCellValue(rowHandle, gridView2.Columns["TotalQuantity"], total);
                        }
                        
                        txtEmployee.ReadOnly = true;
                        txtSpecialOrderNo.ReadOnly = true;
                        comStore.Enabled = false;
                        txtStoreID.ReadOnly = true;
                        comSupplier.Enabled = false;
                        txtSupplier.ReadOnly = true;
                        txtName.Text = "";
                        txtTotalMeters.Text = "0";
                    }
                    else
                    {
                        MessageBox.Show("برجاء التاكد من ادخال البيانات بطريقة صحيحة");
                    }
                }
                else
                {
                    MessageBox.Show("يجب ادخال جميع البيانات");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (gridView2.GetSelectedRows().Length > 0)
            {
                if (MessageBox.Show("هل انت متاكد انك تريد الحذف؟", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        dbconnection.Open();
                        for (int j = 0; j < gridView2.GetSelectedRows().Length; j++)
                        {
                            string query = "delete from dash_order_details where DashOrder_ID=" + orderId + " and Item_Description='" + gridView2.GetRowCellDisplayText(gridView2.GetSelectedRows()[j], "ItemName") + "'";
                            MySqlCommand com = new MySqlCommand(query, dbconnection);
                            com.ExecuteNonQuery();
                        }
                        GridView view = gridView2 as GridView;
                        view.DeleteSelectedRows();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    dbconnection.Close();
                }
            }
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            try
            {
                if (gridView2.RowCount > 0 && txtOrderNum.Text != "" && txtEmployee.Text != "" && txtSpecialOrderNo.Text != "" && comStore.SelectedValue != null && comFactory.SelectedValue != null)
                {
                    dbconnection.Open();
                    string query = "update dash_orders set Saved=1 where DashOrder_ID=" + orderId;
                    MySqlCommand com = new MySqlCommand(query, dbconnection);
                    com.ExecuteNonQuery();

                    #region report
                    /*List<Order_Items> bi = new List<Order_Items>();
                    for (int i = 0; i < gridView2.RowCount; i++)
                    {
                        int rowHand = gridView2.GetRowHandle(i);

                        query = "select concat(product.Product_Name,' ',COALESCE(color.Color_Name,''),' ',data.Description,' ',groupo.Group_Name,' ',factory.Factory_Name,' ',COALESCE(size.Size_Value,'')) as 'الاسم' FROM data LEFT JOIN color ON color.Color_ID = data.Color_ID LEFT JOIN size ON size.Size_ID = data.Size_ID LEFT JOIN sort ON sort.Sort_ID = data.Sort_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID INNER JOIN factory ON factory.Factory_ID = data.Factory_ID  INNER JOIN product ON product.Product_ID = data.Product_ID  INNER JOIN type ON type.Type_ID = data.Type_ID where data.Data_ID=" + gridView2.GetRowCellDisplayText(rowHand, gridView2.Columns["Data_ID"]);
                        com = new MySqlCommand(query, dbconnection);
                        Order_Items item = new Order_Items() { Code = gridView2.GetRowCellDisplayText(rowHand, gridView2.Columns["Code"]), Product_Type = gridView2.GetRowCellDisplayText(rowHand, gridView2.Columns["ItemType"]), Product_Name = com.ExecuteScalar().ToString(), Balatat = Convert.ToInt32(gridView2.GetRowCellDisplayText(rowHand, gridView2.Columns["Balatat"])), Total_Meters = Convert.ToDouble(gridView2.GetRowCellDisplayText(rowHand, gridView2.Columns["TotalQuantity"])) };
                        bi.Add(item);
                    }
                    Report_Order f = new Report_Order();
                    f.PrintInvoice(comFactory.Text, Convert.ToInt32(txtOrderNum.Text), txtEmployee.Text, bi);
                    f.ShowDialog();*/
                    #endregion

                    clear();
                }
                else
                {
                    MessageBox.Show("برجاء التاكد من جميع البيانات");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }

        private void txtBillNo_TextChanged(object sender, EventArgs e)
        {
            if (loaded)
            {
                if (txtSpecialOrderNo.Text != "")
                {
                    try
                    {
                        int billNumb = 0;
                        if (int.TryParse(txtSpecialOrderNo.Text, out billNumb))
                        { }
                        else
                        {
                            MessageBox.Show("تاكد من رقم الفاتورة");
                            return;
                        }

                        dbconnection.Open();
                        search();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    dbconnection.Close();
                    dbconnection2.Close();
                }
                else
                {
                    gridControl1.DataSource = null;
                    gridControl2.DataSource = null;
                    
                }
            }
        }

        //functions
        void clearClient()
        {
            radCon.Checked = false;
            radClient.Checked = false;
            radDealer.Checked = false;
            radEng.Checked = false;
            comClient.SelectedIndex = -1;
            txtClientID.Text = "";
            comEngCon.SelectedIndex = -1;
            txtCustomerID.Text = "";
        }
        void search()
        {
            string query = "SELECT special_order.Description as 'الوصف',special_order.Picture as 'صورة الطلب',special_order.Product_Picture as 'صورة البند' FROM special_order where special_order.Record=0 and special_order.SpecialOrder_ID=0";
            MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
            DataTable dt = new DataTable();
            da.Fill(dt);
            gridControl1.DataSource = dt;
            
            query = "SELECT special_order.Description as 'الوصف',special_order.Picture as 'صورة الطلب',special_order.Product_Picture as 'صورة البند',special_order.Client_ID,special_order.Customer_ID,special_order.Delegate_ID FROM special_order where special_order.Record=0 and special_order.SpecialOrder_ID=" + txtSpecialOrderNo.Text;
            MySqlCommand com = new MySqlCommand(query, dbconnection);
            MySqlDataReader dataReader = com.ExecuteReader();
            while (dataReader.Read())
            {
                int co, ci = 0;
                string customType, q = "";
                if (int.TryParse(dataReader["Customer_ID"].ToString(), out co))
                {
                    q = "select Customer_Type from customer where Customer_ID=" + co;
                }
                if (int.TryParse(dataReader["Client_ID"].ToString(), out ci))
                {
                    q = "select Customer_Type from customer where Customer_ID=" + ci;
                }
                MySqlCommand command = new MySqlCommand(q, dbconnection2);
                customType = command.ExecuteScalar().ToString();
                if (customType == "عميل")
                {
                    radClient.Checked = true;
                    //comClient.Text = dataReader["Customer_Name"].ToString();
                    comClient.SelectedValue = ci;
                    txtClientID.Text = ci.ToString();
                    comEngCon.SelectedIndex = -1;
                    txtCustomerID.Text = "";
                }
                else if (customType == "مهندس")
                {
                    radEng.Checked = true;
                    //comEngCon.Text = dataReader["Customer_Name"].ToString();
                    comEngCon.SelectedValue = co;
                    txtCustomerID.Text = co.ToString();
                    comClient.SelectedIndex = -1;
                    txtClientID.Text = "";
                }
                else if (customType == "مقاول")
                {
                    radCon.Checked = true;
                    //comEngCon.Text = dataReader["Customer_Name"].ToString();
                    comEngCon.SelectedValue = co;
                    txtCustomerID.Text = co.ToString();
                    comClient.SelectedIndex = -1;
                    txtClientID.Text = "";
                }
                else if (customType == "تاجر")
                {
                    radDealer.Checked = true;
                    //comEngCon.Text = dataReader["Customer_Name"].ToString();
                    comEngCon.SelectedValue = co;
                    txtCustomerID.Text = co.ToString();
                    comClient.SelectedIndex = -1;
                    txtClientID.Text = "";
                }
                layoutView1.AddNewRow();
                int rowHandle = layoutView1.GetRowHandle(layoutView1.DataRowCount);
                if (layoutView1.IsNewItemRow(rowHandle))
                {
                    layoutView1.SetRowCellValue(rowHandle, layoutView1.Columns["الوصف"], dataReader["الوصف"].ToString());
                    layoutView1.SetRowCellValue(rowHandle, layoutView1.Columns["صورة الطلب"], dataReader["صورة الطلب"]);
                    layoutView1.SetRowCellValue(rowHandle, layoutView1.Columns["صورة البند"], dataReader["صورة البند"]);
                }
            }
            dataReader.Close();

            if (layoutView1.IsLastVisibleRow)
            {
                layoutView1.FocusedRowHandle = layoutView1.RowCount - 1;
            }
        }
        bool IsItemAdded()
        {
            for (int i = 0; i < gridView2.RowCount; i++)
            {
                DataRow row3 = gridView2.GetDataRow(gridView2.GetRowHandle(i));
                if (txtName.Text == row3["ItemName"].ToString())
                    return true;
            }
            return false;
        }

        void clear()
        {
            loaded = false;
            gridControl1.DataSource = null;
            int cont = gridView2.RowCount;
            for (int i = 0; i < cont; i++)
            {
                int rowHandle = gridView2.GetRowHandle(0);
                gridView2.DeleteRow(rowHandle);
            }
            comFactory.SelectedIndex = -1;
            txtFactory.Text = "";
            comSupplier.SelectedIndex = -1;
            txtSupplier.Text = "";
            txtOrderNum.Text = "";
            comStore.SelectedIndex = -1;
            txtStoreID.Text = "";
            txtEmployee.Text = "";
            txtSpecialOrderNo.Text = "";
            txtName.Text = "";
            txtTotalMeters.Text = "0";
            txtEmployee.ReadOnly = false;
            txtSpecialOrderNo.ReadOnly = false;
            comStore.Enabled = true;
            txtStoreID.ReadOnly = false;
            comSupplier.Enabled = true;
            txtSupplier.ReadOnly = false;
            loaded = true;
        }
    }
}

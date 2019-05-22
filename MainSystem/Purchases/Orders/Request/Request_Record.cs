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

namespace MainSystem
{
    public partial class Request_Record : Form
    {
        MySqlConnection dbconnection, dbconnection2, dbconnection3;
        //bool loaded = false;
        List<DataRow> lstrow = new List<DataRow>();
        DataRow row1 = null;
        bool loaded = false;
        bool factoryFlage = false;
        bool groupFlage = false;
        bool flagProduct = false;
        bool flagRequestNum = false;
        int orderNumber = 1;
        int dashOrderNumber = 0;
        int rowHandl = 0;
        int orderId = 0;
        bool addFlage = true;
        bool flagCarton = false;
        bool flag = false;
        string request = "";
        int dashOrderID = 0;

        public Request_Record(List<DataRow> Row1, Request_Report OrderReport, XtraTabControl xtraTabControlPurchases)
        {
            InitializeComponent();
            dbconnection = new MySqlConnection(connection.connectionString);
            dbconnection2 = new MySqlConnection(connection.connectionString);
            dbconnection3 = new MySqlConnection(connection.connectionString);
            lstrow = Row1;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                DataHelperOrder dh = new DataHelperOrder(DSparametrOrder.doubleDS);
                gridControl2.DataSource = dh.DataSet;
                gridControl2.DataMember = dh.DataMember;
                gridView2.InitNewRow += GridView2_InitNewRow;

                #region MyRegion
                string query = "select * from factory";//inner join type_factory on factory.Factory_ID=type_factory.Factory_ID inner join type on type_factory.Type_ID=type.Type_ID";
                MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                DataTable dt = new DataTable();
                da.Fill(dt);
                comFactory.DataSource = dt;
                comFactory.DisplayMember = dt.Columns["Factory_Name"].ToString();
                comFactory.ValueMember = dt.Columns["Factory_ID"].ToString();
                comFactory.Text = "";
                //dbconnection.Close();
                dbconnection.Open();
                query = "select TypeCoding_Method from type";
                MySqlCommand com = new MySqlCommand(query, dbconnection);
                int TypeCoding_Method = (int)com.ExecuteScalar();
                dbconnection.Close();
                if (TypeCoding_Method == 1)
                {
                    string query2 = "select * from groupo where Factory_ID=-1";
                    
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

                query = "select * from color";
                da = new MySqlDataAdapter(query, dbconnection);
                dt = new DataTable();
                da.Fill(dt);
                comColor.DataSource = dt;
                comColor.DisplayMember = dt.Columns["Color_Name"].ToString();
                comColor.ValueMember = dt.Columns["Color_ID"].ToString();
                comColor.Text = "";
                comFactory.Focus();
                #endregion

                //dbconnection.Open();
                query = "select * from sort";
                da = new MySqlDataAdapter(query, dbconnection);
                dt = new DataTable();
                da.Fill(dt);
                comSort.DataSource = dt;
                comSort.DisplayMember = dt.Columns["Sort_Value"].ToString();
                comSort.ValueMember = dt.Columns["Sort_ID"].ToString();
                comSort.Text = "";

                query = "select * from supplier";
                da = new MySqlDataAdapter(query, dbconnection);
                dt = new DataTable();
                da.Fill(dt);
                comSupplier.DataSource = dt;
                comSupplier.DisplayMember = dt.Columns["Supplier_Name"].ToString();
                comSupplier.ValueMember = dt.Columns["Supplier_ID"].ToString();
                comSupplier.SelectedIndex = -1;
                txtSupplier.Text = "";
                
                query = "select * from store";
                da = new MySqlDataAdapter(query, dbconnection);
                dt = new DataTable();
                da.Fill(dt);
                comStore.DataSource = dt;
                comStore.DisplayMember = dt.Columns["Store_Name"].ToString();
                comStore.ValueMember = dt.Columns["Store_ID"].ToString();
                comStore.SelectedIndex = -1;
                txtStoreID.Text = "";

                if(lstrow.Count > 0)
                {
                    search();
                }
                
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
            view.SetRowCellValue(e.RowHandle, gridView2.Columns[0], 0);
            view.SetRowCellValue(e.RowHandle, gridView2.Columns[1], "");
            view.SetRowCellValue(e.RowHandle, gridView2.Columns[2], "");
            view.SetRowCellValue(e.RowHandle, gridView2.Columns[3], "");
            view.SetRowCellValue(e.RowHandle, gridView2.Columns[4], "");
            view.SetRowCellValue(e.RowHandle, gridView2.Columns[5], "");
            view.SetRowCellValue(e.RowHandle, gridView2.Columns[6], "");
            view.SetRowCellValue(e.RowHandle, gridView2.Columns[7], "");
            view.SetRowCellValue(e.RowHandle, gridView2.Columns[8], "");
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
                            string query = "SELECT orders.Order_ID,orders.Store_ID,orders.Employee_Name,orders.Supplier_ID,orders.Request_Date,orders.Receive_Date,orders.Confirmed,orders.Received,orders.Canceled,Dash_Order_Number,DashOrder_ID FROM orders left join order_details ON order_details.Order_ID = orders.Order_ID where orders.Factory_ID=" + comFactory.SelectedValue.ToString() + " and orders.Order_Number=" + orderNumber;
                            MySqlCommand comand = new MySqlCommand(query, dbconnection3);
                            MySqlDataReader dr = comand.ExecuteReader();
                            if(dr.HasRows)
                            {
                                while(dr.Read())
                                {
                                    if (dr["Confirmed"].ToString() == "1" || dr["Received"].ToString() == "1" || dr["Received"].ToString() == "2" || dr["Canceled"].ToString() == "1")
                                    {
                                        if(dr["Canceled"].ToString() == "1")
                                        {
                                            checkBoxAvailable.Checked = false;
                                            checkBoxCanceled.Checked = true;
                                            checkBoxConfirmed.Checked = false;
                                            checkBoxReceived.Checked = false;
                                            checkBoxReceivedPart.Checked = false;
                                        }
                                        else if (dr["Received"].ToString() == "1")
                                        {
                                            checkBoxAvailable.Checked = false;
                                            checkBoxCanceled.Checked = false;
                                            checkBoxConfirmed.Checked = false;
                                            checkBoxReceived.Checked = true;
                                            checkBoxReceivedPart.Checked = false;
                                        }
                                        else if (dr["Received"].ToString() == "2")
                                        {
                                            checkBoxAvailable.Checked = false;
                                            checkBoxCanceled.Checked = false;
                                            checkBoxConfirmed.Checked = false;
                                            checkBoxReceived.Checked = false;
                                            checkBoxReceivedPart.Checked = true;
                                        }
                                        else if (dr["Confirmed"].ToString() == "1")
                                        {
                                            checkBoxAvailable.Checked = false;
                                            checkBoxCanceled.Checked = false;
                                            checkBoxConfirmed.Checked = true;
                                            checkBoxReceived.Checked = false;
                                            checkBoxReceivedPart.Checked = false;
                                        }
                                        
                                        addFlage = false;
                                        loaded = false;
                                        /*txtEmployee.ReadOnly = true;
                                        comStore.Enabled = false;
                                        txtStoreID.ReadOnly = true;
                                        comSupplier.Enabled = false;
                                        txtSupplier.ReadOnly = true;
                                        dateTimePicker1.Enabled = false;*/
                                        dateTimePicker2.Enabled = false;
                                        txtDashOrderNum.ReadOnly = true;
                                        orderId = Convert.ToInt16(dr["Order_ID"].ToString());
                                        txtEmployee.Text = dr["Employee_Name"].ToString();
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
                                        dateTimePicker1.Value = Convert.ToDateTime(dr["Request_Date"].ToString());
                                        dateTimePicker2.Value = Convert.ToDateTime(dr["Receive_Date"].ToString());
                                        txtDashOrderNum.Text = dr["Dash_Order_Number"].ToString();
                                        
                                        if (dr["Dash_Order_Number"].ToString() != "")
                                        {
                                            dashOrderNumber = Convert.ToInt16(dr["Dash_Order_Number"].ToString());
                                        }
                                        else
                                        {
                                            dashOrderNumber = 0;
                                        }
                                        if (dr["DashOrder_ID"].ToString() != "")
                                        {
                                            dashOrderID = Convert.ToInt16(dr["DashOrder_ID"].ToString());
                                        }
                                        else
                                        {
                                            dashOrderID = 0;
                                        }
                                        loaded = true;
                                    }
                                    else
                                    {
                                        checkBoxAvailable.Checked = true;
                                        checkBoxCanceled.Checked = false;
                                        checkBoxConfirmed.Checked = false;
                                        checkBoxReceived.Checked = false;
                                        checkBoxReceivedPart.Checked = false;

                                        addFlage = true;
                                        loaded = false;
                                        /*txtEmployee.ReadOnly = true;
                                        comStore.Enabled = false;
                                        txtStoreID.ReadOnly = true;
                                        comSupplier.Enabled = false;
                                        txtSupplier.ReadOnly = true;
                                        dateTimePicker1.Enabled = false;*/
                                        dateTimePicker2.Enabled = false;
                                        txtDashOrderNum.ReadOnly = true;
                                        orderId = Convert.ToInt16(dr["Order_ID"].ToString());
                                        txtEmployee.Text = dr["Employee_Name"].ToString();
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
                                        dateTimePicker1.Value = Convert.ToDateTime(dr["Request_Date"].ToString());
                                        dateTimePicker2.Value = Convert.ToDateTime(dr["Receive_Date"].ToString());
                                        txtDashOrderNum.Text = dr["Dash_Order_Number"].ToString();
                                        if (dr["Dash_Order_Number"].ToString() != "")
                                        {
                                            dashOrderNumber = Convert.ToInt16(dr["Dash_Order_Number"].ToString());
                                        }
                                        else
                                        {
                                            dashOrderNumber = 0;
                                        }
                                        if (dr["DashOrder_ID"].ToString() != "")
                                        {
                                            dashOrderID = Convert.ToInt16(dr["DashOrder_ID"].ToString());
                                        }
                                        else
                                        {
                                            dashOrderID = 0;
                                        }
                                        loaded = true;
                                    }
                                }
                                dr.Close();
                            }
                            else
                            {
                                checkBoxAvailable.Checked = false;
                                checkBoxCanceled.Checked = false;
                                checkBoxConfirmed.Checked = false;
                                checkBoxReceived.Checked = false;
                                checkBoxReceivedPart.Checked = false;

                                addFlage = true;
                                loaded = false;
                                orderId = 0;
                                txtEmployee.Text = "";
                                comStore.SelectedIndex = -1;
                                txtStoreID.Text = "";
                                comSupplier.SelectedIndex = -1;
                                txtSupplier.Text = "";
                                dateTimePicker1.Value = DateTime.Now.Date;
                                dateTimePicker2.Value = DateTime.Now.Date;
                                txtDashOrderNum.Text = "";
                                /*txtEmployee.ReadOnly = false;
                                comStore.Enabled = true;
                                txtStoreID.ReadOnly = false;
                                comSupplier.Enabled = true;
                                txtSupplier.ReadOnly = false;
                                dateTimePicker1.Enabled = true;*/
                                dateTimePicker2.Enabled = true;
                                txtDashOrderNum.ReadOnly = false;
                                int cont2 = gridView2.RowCount;
                                for (int i = 0; i < cont2; i++)
                                {
                                    int rowHandle = gridView2.GetRowHandle(0);
                                    gridView2.DeleteRow(rowHandle);
                                }
                                loaded = true;
                            }

                            query = "SELECT data.Data_ID,data.Code as 'الكود',type.Type_Name as 'النوع',concat(product.Product_Name,' ',COALESCE(color.Color_Name,''),' ',data.Description,' ',groupo.Group_Name,' ',factory.Factory_Name,' ',COALESCE(size.Size_Value,''),' ',COALESCE(sort.Sort_Value,'')) as 'الاسم',data.Carton as 'الكرتنة',sum(order_details.Balatat) as 'عدد البلتات',sum(order_details.Carton_Balata) as 'عدد الكراتين',sum(order_details.Quantity) as 'عدد المتر/القطعة',order_details.Type,orders.Order_ID,orders.Store_ID,orders.Employee_Name,orders.Request_Date,orders.Receive_Date FROM orders left join order_details ON order_details.Order_ID = orders.Order_ID inner join data on data.Data_ID = order_details.Data_ID LEFT JOIN color ON color.Color_ID = data.Color_ID LEFT JOIN size ON size.Size_ID = data.Size_ID LEFT JOIN sort ON sort.Sort_ID = data.Sort_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID INNER JOIN factory ON factory.Factory_ID = data.Factory_ID  INNER JOIN product ON product.Product_ID = data.Product_ID  INNER JOIN type ON type.Type_ID = data.Type_ID  LEFT JOIN storage ON storage.Data_ID = data.Data_ID where orders.Factory_ID=" + comFactory.SelectedValue.ToString() + " and orders.Order_Number=" + orderNumber + " and orders.Confirmed=0 and orders.Received=0 and orders.Canceled=0 group by data.Data_ID order by SUBSTR(data.Code,1,16),color.Color_Name,data.Sort_ID";
                            comand = new MySqlCommand(query, dbconnection);
                            dr = comand.ExecuteReader();
                            if (dr.HasRows)
                            {
                                dbconnection2.Open();
                                while (dr.Read())
                                {
                                    if (dr["Data_ID"].ToString() != "")
                                    {
                                        gridView2.AddNewRow();
                                        int rowHandle = gridView2.GetRowHandle(gridView2.DataRowCount);
                                        if (gridView2.IsNewItemRow(rowHandle))
                                        {
                                            gridView2.SetRowCellValue(rowHandle, gridView2.Columns[0], dr["Data_ID"]);
                                            gridView2.SetRowCellValue(rowHandle, gridView2.Columns[1], dr["الكود"]);
                                            gridView2.SetRowCellValue(rowHandle, gridView2.Columns["ItemType"], dr["النوع"]);
                                            gridView2.SetRowCellValue(rowHandle, gridView2.Columns["ItemName"], dr["الاسم"]);
                                            gridView2.SetRowCellValue(rowHandle, gridView2.Columns["Carton"], dr["الكرتنة"]);
                                            gridView2.SetRowCellValue(rowHandle, gridView2.Columns["Balatat"], dr["عدد البلتات"]);
                                            gridView2.SetRowCellValue(rowHandle, gridView2.Columns["Cartons_Balate"], dr["عدد الكراتين"]);
                                            gridView2.SetRowCellValue(rowHandle, gridView2.Columns["TotalQuantity"], dr["عدد المتر/القطعة"]);
                                            gridView2.SetRowCellValue(rowHandle, gridView2.Columns["Type"], dr["Type"].ToString());
                                        }
                                    }
                                }
                            }
                            dr.Close();
                        }
                        else
                        {
                            MessageBox.Show("يجب تحديد المورد");
                            checkBoxAvailable.Checked = false;
                            checkBoxCanceled.Checked = false;
                            checkBoxConfirmed.Checked = false;
                            checkBoxReceived.Checked = false;
                            checkBoxReceivedPart.Checked = false;

                            loaded = false;
                            orderId = 0;
                            txtEmployee.Text = "";
                            comStore.SelectedIndex = -1;
                            txtStoreID.Text = "";
                            comSupplier.SelectedIndex = -1;
                            txtSupplier.Text = "";
                            dateTimePicker1.Value = DateTime.Now.Date;
                            dateTimePicker2.Value = DateTime.Now.Date;
                            txtDashOrderNum.Text = "";
                            /*txtEmployee.ReadOnly = false;
                            comStore.Enabled = true;
                            txtStoreID.ReadOnly = false;
                            comSupplier.Enabled = true;
                            txtSupplier.ReadOnly = false;
                            dateTimePicker1.Enabled = true;*/
                            dateTimePicker2.Enabled = true;
                            txtDashOrderNum.ReadOnly = false;
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
                        checkBoxAvailable.Checked = false;
                        checkBoxCanceled.Checked = false;
                        checkBoxConfirmed.Checked = false;
                        checkBoxReceived.Checked = false;
                        checkBoxReceivedPart.Checked = false;

                        loaded = false;
                        orderId = 0;
                        txtEmployee.Text = "";
                        comStore.SelectedIndex = -1;
                        txtStoreID.Text = "";
                        comSupplier.SelectedIndex = -1;
                        txtSupplier.Text = "";
                        dateTimePicker1.Value = DateTime.Now.Date;
                        dateTimePicker2.Value = DateTime.Now.Date;
                        txtDashOrderNum.Text = "";
                        /*txtEmployee.ReadOnly = false;
                        comStore.Enabled = true;
                        txtStoreID.ReadOnly = false;
                        comSupplier.Enabled = true;
                        txtSupplier.ReadOnly = false;
                        dateTimePicker1.Enabled = true;*/
                        dateTimePicker2.Enabled = true;
                        txtDashOrderNum.ReadOnly = false;
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
                checkBoxAvailable.Checked = false;
                checkBoxCanceled.Checked = false;
                checkBoxConfirmed.Checked = false;
                checkBoxReceived.Checked = false;
                checkBoxReceivedPart.Checked = false;

                loaded = false;
                orderId = 0;
                txtEmployee.Text = "";
                comStore.SelectedIndex = -1;
                txtStoreID.Text = "";
                comSupplier.SelectedIndex = -1;
                txtSupplier.Text = "";
                dateTimePicker1.Value = DateTime.Now.Date;
                dateTimePicker2.Value = DateTime.Now.Date;
                txtDashOrderNum.Text = "";
                /*txtEmployee.ReadOnly = false;
                comStore.Enabled = true;
                txtStoreID.ReadOnly = false;
                comSupplier.Enabled = true;
                txtSupplier.ReadOnly = false;
                dateTimePicker1.Enabled = true;*/
                dateTimePicker2.Enabled = true;
                txtDashOrderNum.ReadOnly = false;
                int cont = gridView2.RowCount;
                for (int i = 0; i < cont; i++)
                {
                    int rowHandle = gridView2.GetRowHandle(0);
                    gridView2.DeleteRow(rowHandle);
                }
                loaded = true;
            }
        }

        private void txtDashOrderNum_TextChanged(object sender, EventArgs e)
        {
            if (loaded)
            {
                if (txtDashOrderNum.Text != "")
                {
                    request = "request";
                    try
                    {
                        if (int.TryParse(txtDashOrderNum.Text, out dashOrderNumber))
                        {
                            if (comFactory.SelectedValue != null)
                            {
                                //int cont = gridView1.RowCount;
                                //for (int i = 0; i < cont; i++)
                                //{
                                //    int rowHandle = gridView1.GetRowHandle(0);
                                //    gridView1.DeleteRow(rowHandle);
                                //}
                                row1 = null;
                                txtCode.Text = "";
                                txtTotalMeters.Text = "0";
                                txtCarton.Text = "0";
                                txtBalat.Text = "0";
                                gridControl1.DataSource = null;
                                gridView1.Columns.Clear();

                                dbconnection.Open();
                                string query = "SELECT data.Data_ID,data.Code as 'الكود',type.Type_Name as 'النوع',concat(product.Product_Name,' ',COALESCE(color.Color_Name,''),' ',data.Description,' ',groupo.Group_Name,' ',factory.Factory_Name,' ',COALESCE(size.Size_Value,''),' ',COALESCE(sort.Sort_Value,'')) as 'الاسم',data.Carton as 'الكرتنة',sum(dash_order_details.Balatat) as 'عدد البلتات',sum(dash_order_details.Carton_Balata) as 'عدد الكراتين',sum(dash_order_details.Quantity) as 'عدد المتر/القطعة','الحالة',dash_order_details.Type FROM dash_orders left join dash_order_details ON dash_order_details.DashOrder_ID = dash_orders.DashOrder_ID inner join data on data.Data_ID = dash_order_details.Data_ID LEFT JOIN color ON color.Color_ID = data.Color_ID LEFT JOIN size ON size.Size_ID = data.Size_ID LEFT JOIN sort ON sort.Sort_ID = data.Sort_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID INNER JOIN factory ON factory.Factory_ID = data.Factory_ID  INNER JOIN product ON product.Product_ID = data.Product_ID  INNER JOIN type ON type.Type_ID = data.Type_ID  LEFT JOIN storage ON storage.Data_ID = data.Data_ID where dash_orders.Factory_ID=" + comFactory.SelectedValue.ToString() + " and dash_orders.Dash_Order_Number=" + dashOrderNumber + " and dash_orders.Saved=1 and dash_orders.Confirmed=0 and dash_orders.Canceled=0 and data.Data_ID=0 group by data.Data_ID order by SUBSTR(data.Code,1,16),color.Color_Name,data.Sort_ID";
                                MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                                DataTable dt = new DataTable();
                                da.Fill(dt);
                                gridControl1.DataSource = dt;

                                RepositoryItemCheckEdit repositoryCheckEdit1 = gridControl1.RepositoryItems.Add("CheckEdit") as RepositoryItemCheckEdit;
                                repositoryCheckEdit1.ValueChecked = "True";
                                repositoryCheckEdit1.ValueUnchecked = "False";
                                gridView1.Columns["الحالة"].ColumnEdit = repositoryCheckEdit1;

                                gridView1.Columns[0].Visible = false;
                                gridView1.Columns["Type"].Visible = false;

                                for (int i = 2; i < gridView1.Columns.Count; i++)
                                {
                                    gridView1.Columns[i].Width = 100;
                                }
                                gridView1.Columns["الكود"].Width = 180;
                                gridView1.Columns["الاسم"].Width = 270;
                                gridView1.Columns["الكرتنة"].Width = 60;
                                if (gridView1.IsLastVisibleRow)
                                {
                                    gridView1.FocusedRowHandle = gridView1.RowCount - 1;
                                }

                                /*string*/
                                query = "SELECT data.Data_ID,data.Code as 'الكود',type.Type_Name as 'النوع',concat(product.Product_Name,' ',COALESCE(color.Color_Name,''),' ',data.Description,' ',groupo.Group_Name,' ',factory.Factory_Name,' ',COALESCE(size.Size_Value,''),' ',COALESCE(sort.Sort_Value,'')) as 'الاسم',data.Carton as 'الكرتنة',sum(dash_order_details.Balatat) as 'عدد البلتات',sum(dash_order_details.Carton_Balata) as 'عدد الكراتين',sum(dash_order_details.Quantity) as 'عدد المتر/القطعة',dash_order_details.Type,dash_orders.DashOrder_ID,dash_orders.Store_ID,dash_orders.Employee_Name,dash_orders.Request_Date,dash_orders.Supplier_ID FROM dash_orders left join dash_order_details ON dash_order_details.DashOrder_ID = dash_orders.DashOrder_ID inner join data on data.Data_ID = dash_order_details.Data_ID LEFT JOIN color ON color.Color_ID = data.Color_ID LEFT JOIN size ON size.Size_ID = data.Size_ID LEFT JOIN sort ON sort.Sort_ID = data.Sort_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID INNER JOIN factory ON factory.Factory_ID = data.Factory_ID  INNER JOIN product ON product.Product_ID = data.Product_ID  INNER JOIN type ON type.Type_ID = data.Type_ID  LEFT JOIN storage ON storage.Data_ID = data.Data_ID where dash_orders.Factory_ID=" + comFactory.SelectedValue.ToString() + " and dash_orders.Dash_Order_Number=" + dashOrderNumber + " and dash_orders.Saved=1 and dash_orders.Confirmed=0 and dash_orders.Canceled=0 group by data.Data_ID order by SUBSTR(data.Code,1,16),color.Color_Name,data.Sort_ID";
                                MySqlCommand comand = new MySqlCommand(query, dbconnection);
                                MySqlDataReader dr = comand.ExecuteReader();
                                if (dr.HasRows)
                                {
                                    dbconnection2.Open();
                                    while (dr.Read())
                                    {
                                        if (dr["DashOrder_ID"].ToString() != "")
                                        {
                                            dashOrderID = Convert.ToInt16(dr["DashOrder_ID"].ToString());
                                        }
                                        else
                                        {
                                            dashOrderID = 0;
                                        }
                                        loaded = false;
                                        /*txtEmployee.ReadOnly = true;
                                        comStore.Enabled = false;
                                        txtStoreID.ReadOnly = true;
                                        comSupplier.Enabled = false;
                                        txtSupplier.ReadOnly = true;
                                        dateTimePicker1.Enabled = false;*/
                                        txtEmployee.Text = dr["Employee_Name"].ToString();
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
                                        dateTimePicker1.Value = Convert.ToDateTime(dr["Request_Date"].ToString());
                                        loaded = true;
                                        if (dr["Data_ID"].ToString() != "")
                                        {
                                            gridView1.AddNewRow();
                                            int rowHandle = gridView1.GetRowHandle(gridView1.DataRowCount);
                                            if (gridView1.IsNewItemRow(rowHandle))
                                            {
                                                gridView1.SetRowCellValue(rowHandle, gridView1.Columns[0], dr["Data_ID"]);
                                                gridView1.SetRowCellValue(rowHandle, gridView1.Columns[1], dr["الكود"]);
                                                gridView1.SetRowCellValue(rowHandle, gridView1.Columns["النوع"], dr["النوع"]);
                                                gridView1.SetRowCellValue(rowHandle, gridView1.Columns["الاسم"], dr["الاسم"]);
                                                gridView1.SetRowCellValue(rowHandle, gridView1.Columns["الكرتنة"], dr["الكرتنة"]);
                                                gridView1.SetRowCellValue(rowHandle, gridView1.Columns["عدد البلتات"], dr["عدد البلتات"]);
                                                gridView1.SetRowCellValue(rowHandle, gridView1.Columns["عدد الكراتين"], dr["عدد الكراتين"]);
                                                gridView1.SetRowCellValue(rowHandle, gridView1.Columns["عدد المتر/القطعة"], dr["عدد المتر/القطعة"]);
                                                gridView1.SetRowCellValue(rowHandle, gridView1.Columns["Type"], dr["Type"].ToString());

                                                string q = "SELECT order_details.Data_ID FROM orders INNER JOIN order_details ON order_details.Order_ID = orders.Order_ID where orders.Received=0 and orders.Canceled=0 and order_details.Data_ID=" + dr["Data_ID"].ToString();
                                                MySqlCommand comand2 = new MySqlCommand(q, dbconnection2);
                                                MySqlDataReader dr2 = comand2.ExecuteReader();
                                                if (dr2.HasRows)
                                                {
                                                    while (dr2.Read())
                                                    {
                                                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns["الحالة"], true);
                                                    }
                                                    dr2.Close();
                                                }
                                                else
                                                {
                                                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns["الحالة"], false);
                                                }
                                                dr2.Close();
                                            }
                                        }
                                    }
                                }
                                dr.Close();
                            }
                            else
                            {
                                dashOrderNumber = 0;
                                dashOrderID = 0;
                                MessageBox.Show("يجب اختيار المصنع");
                                loaded = false;
                                row1 = null;
                                txtCode.Text = "";
                                txtTotalMeters.Text = "0";
                                txtCarton.Text = "0";
                                txtBalat.Text = "0";

                                txtEmployee.Text = "";
                                comStore.SelectedIndex = -1;
                                txtStoreID.Text = "";
                                comSupplier.SelectedIndex = -1;
                                txtSupplier.Text = "";
                                dateTimePicker1.Value = DateTime.Now.Date;
                                dateTimePicker2.Value = DateTime.Now.Date;
                                /*txtEmployee.ReadOnly = false;
                                comStore.Enabled = true;
                                txtStoreID.ReadOnly = false;
                                comSupplier.Enabled = true;
                                txtSupplier.ReadOnly = false;
                                dateTimePicker1.Enabled = true;*/
                                dateTimePicker2.Enabled = true;
                                int cont2 = gridView1.RowCount;
                                for (int i = 0; i < cont2; i++)
                                {
                                    int rowHandle = gridView1.GetRowHandle(0);
                                    gridView1.DeleteRow(rowHandle);
                                }
                                loaded = true;
                            }
                        }
                        else
                        {
                            dashOrderNumber = 0;
                            dashOrderID = 0;
                            loaded = false;
                            row1 = null;
                            txtCode.Text = "";
                            txtTotalMeters.Text = "0";
                            txtCarton.Text = "0";
                            txtBalat.Text = "0";

                            txtEmployee.Text = "";
                            comStore.SelectedIndex = -1;
                            txtStoreID.Text = "";
                            comSupplier.SelectedIndex = -1;
                            txtSupplier.Text = "";
                            dateTimePicker1.Value = DateTime.Now.Date;
                            dateTimePicker2.Value = DateTime.Now.Date;
                            /*txtEmployee.ReadOnly = false;
                            comStore.Enabled = true;
                            txtStoreID.ReadOnly = false;
                            comSupplier.Enabled = true;
                            txtSupplier.ReadOnly = false;
                            dateTimePicker1.Enabled = true;*/
                            dateTimePicker2.Enabled = true;
                            int cont2 = gridView1.RowCount;
                            for (int i = 0; i < cont2; i++)
                            {
                                int rowHandle = gridView1.GetRowHandle(0);
                                gridView1.DeleteRow(rowHandle);
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
                }
                else
                {
                    dashOrderNumber = 0;
                    dashOrderID = 0;
                    loaded = false;
                    row1 = null;
                    txtCode.Text = "";
                    txtTotalMeters.Text = "0";
                    txtCarton.Text = "0";
                    txtBalat.Text = "0";

                    txtEmployee.Text = "";
                    comStore.SelectedIndex = -1;
                    txtStoreID.Text = "";
                    comSupplier.SelectedIndex = -1;
                    txtSupplier.Text = "";
                    dateTimePicker1.Value = DateTime.Now.Date;
                    dateTimePicker2.Value = DateTime.Now.Date;
                    /*txtEmployee.ReadOnly = false;
                    comStore.Enabled = true;
                    txtStoreID.ReadOnly = false;
                    comSupplier.Enabled = true;
                    txtSupplier.ReadOnly = false;
                    dateTimePicker1.Enabled = true;*/
                    dateTimePicker2.Enabled = true;
                    int cont2 = gridView1.RowCount;
                    for (int i = 0; i < cont2; i++)
                    {
                        int rowHandle = gridView1.GetRowHandle(0);
                        gridView1.DeleteRow(rowHandle);
                    }
                    loaded = true;
                }
            }
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
                            if (factoryFlage)
                            {
                                txtFactory.Text = comFactory.SelectedValue.ToString();
                                dbconnection.Close();
                                dbconnection.Open();
                                string query = "select TypeCoding_Method from type";
                                MySqlCommand com = new MySqlCommand(query, dbconnection);
                                int TypeCoding_Method = (int)com.ExecuteScalar();
                                dbconnection.Close();
                                if (TypeCoding_Method == 2)
                                {
                                    string query2f = "select * from groupo where Factory_ID=" + comFactory.SelectedValue.ToString();
                                    MySqlDataAdapter da2f = new MySqlDataAdapter(query2f, dbconnection);
                                    DataTable dt2f = new DataTable();
                                    da2f.Fill(dt2f);
                                    comGroup.DataSource = dt2f;
                                    comGroup.DisplayMember = dt2f.Columns["Group_Name"].ToString();
                                    comGroup.ValueMember = dt2f.Columns["Group_ID"].ToString();
                                    comGroup.Text = "";
                                    txtGroup.Text = "";
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
                                ////////////////////////////////////////////
                                if (comFactory.SelectedValue != null)
                                {
                                    flagRequestNum = false;
                                    dbconnection.Close();
                                    dbconnection.Open();
                                    query = "select Order_Number from orders where Factory_ID=" + comFactory.SelectedValue.ToString() + " order by Order_ID desc limit 1";
                                    com = new MySqlCommand(query, dbconnection);
                                    if (com.ExecuteScalar() != null)
                                    {
                                        orderNumber = Convert.ToInt16(com.ExecuteScalar().ToString());
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
                            }
                            break;
                        case "comGroup":
                            if (groupFlage)
                            {
                                txtGroup.Text = comGroup.SelectedValue.ToString();
                                string supQuery = "", subQuery1 = "";
                                
                                if (comFactory.SelectedValue.ToString() != "")
                                {
                                    supQuery += " and product_factory_group.Factory_ID=" + comFactory.SelectedValue.ToString();
                                    subQuery1 += " and Factory_ID=" + comFactory.SelectedValue.ToString();
                                }
                                string query3 = "select distinct product.Product_ID,Product_Name  from product inner join product_factory_group on product.Product_ID=product_factory_group.Product_ID  where product_factory_group.Group_ID=" + comGroup.SelectedValue.ToString() + supQuery + "  order by product.Product_ID";
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
                            if (flagProduct)
                            {
                                txtProduct.Text = comProduct.SelectedValue.ToString();
                                comSize.Focus();
                            }
                            break;
                        case "comColor":
                            comColor.Focus();
                            break;
                        case "comSize":
                            comSort.Focus();
                            break;
                        case "comSort":
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
                                    //txtRequestNum.Focus();
                                    dbconnection.Close();
                                }
                                else
                                {
                                    MessageBox.Show("there is no item with this id");
                                    comFactory.SelectedIndex = -1;
                                    dbconnection.Close();
                                    return;
                                }
                                break;
                            case "txtGroup":
                                query = "select Group_Name from groupo where Group_ID='" + txtGroup.Text + "'";
                                com = new MySqlCommand(query, dbconnection);
                                if (com.ExecuteScalar() != null)
                                {
                                    Name = (string)com.ExecuteScalar();
                                    comGroup.Text = Name;
                                    txtProduct.Focus();
                                    dbconnection.Close();
                                }
                                else
                                {
                                    MessageBox.Show("there is no item with this id");
                                    comGroup.SelectedIndex = -1;
                                    dbconnection.Close();
                                    return;
                                }
                                break;
                            case "txtProduct":
                                query = "select Product_Name from product where Product_ID='" + txtProduct.Text + "'";
                                com = new MySqlCommand(query, dbconnection);
                                if (com.ExecuteScalar() != null)
                                {
                                    Name = (string)com.ExecuteScalar();
                                    comProduct.Text = Name;
                                    dbconnection.Close();
                                }
                                else
                                {
                                    MessageBox.Show("there is no item with this id");
                                    comProduct.SelectedIndex = -1;
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

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                request = "search";
                string q2, q3, q4, fQuery = "";
                
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

                row1 = null;
                txtCode.Text = "";
                txtTotalMeters.Text = "0";
                txtCarton.Text = "0";
                txtBalat.Text = "0";

                gridControl1.DataSource = null;
                gridView1.Columns.Clear();

                dbconnection.Open();
                dbconnection2.Open();

                string query = "select data.Data_ID,data.Code as 'الكود',type.Type_Name as 'النوع',concat(product.Product_Name,' - ',type.Type_Name,' - ',factory.Factory_Name,' - ',groupo.Group_Name,' ',COALESCE(color.Color_Name,''),' ',COALESCE(size.Size_Value,''),' ',COALESCE(sort.Sort_Value,'')) as 'الاسم',sum(storage.Total_Meters) as 'الكمية المتاحة','الحد الادنى',data.Carton as 'الكرتنة','Type','الحالة' FROM data LEFT JOIN color ON color.Color_ID = data.Color_ID LEFT JOIN size ON size.Size_ID = data.Size_ID LEFT JOIN sort ON sort.Sort_ID = data.Sort_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID INNER JOIN factory ON factory.Factory_ID = data.Factory_ID  INNER JOIN product ON product.Product_ID = data.Product_ID  INNER JOIN type ON type.Type_ID = data.Type_ID LEFT JOIN storage ON storage.Data_ID = data.Data_ID where  data.Factory_ID  IN(" + q2 + ") and data.Group_ID IN (" + q4 + ") and data.Data_ID=0 group by data.Data_ID";
                MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                DataTable dt = new DataTable();
                da.Fill(dt);
                gridControl1.DataSource = dt;

                RepositoryItemCheckEdit repositoryCheckEdit1 = gridControl1.RepositoryItems.Add("CheckEdit") as RepositoryItemCheckEdit;
                repositoryCheckEdit1.ValueChecked = "True";
                repositoryCheckEdit1.ValueUnchecked = "False";
                gridView1.Columns["الحالة"].ColumnEdit = repositoryCheckEdit1;
                //repositoryCheckEdit1.CheckedChanged += new EventHandler(CheckedChanged);
                
                query = "SELECT data.Data_ID,data.Code as 'الكود',type.Type_Name as 'النوع',concat(product.Product_Name,' ',COALESCE(color.Color_Name,''),' ',data.Description,' ',groupo.Group_Name,' ',factory.Factory_Name,' ',COALESCE(size.Size_Value,''),' ',COALESCE(sort.Sort_Value,'')) as 'الاسم',data.Carton as 'الكرتنة',sum(storage.Total_Meters) as 'الكمية المتاحة' FROM data LEFT JOIN color ON color.Color_ID = data.Color_ID LEFT JOIN size ON size.Size_ID = data.Size_ID LEFT JOIN sort ON sort.Sort_ID = data.Sort_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID INNER JOIN factory ON factory.Factory_ID = data.Factory_ID  INNER JOIN product ON product.Product_ID = data.Product_ID  INNER JOIN type ON type.Type_ID = data.Type_ID  LEFT JOIN storage ON storage.Data_ID = data.Data_ID where data.Factory_ID IN(" + q2 + ") and data.Product_ID IN (" + q3 + ") and data.Group_ID IN (" + q4 + ") " + fQuery + " group by data.Data_ID order by SUBSTR(data.Code,1,16),color.Color_Name,data.Sort_ID";
                MySqlCommand comand = new MySqlCommand(query, dbconnection);
                MySqlDataReader dr = comand.ExecuteReader();
                while (dr.Read())
                {
                    gridView1.AddNewRow();
                    int rowHandle = gridView1.GetRowHandle(gridView1.DataRowCount);
                    if (gridView1.IsNewItemRow(rowHandle))
                    {
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[0], dr["Data_ID"]);
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[1], dr["الكود"]);
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns["النوع"], dr["النوع"]);
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns["الاسم"], dr["الاسم"]);
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns["الكرتنة"], dr["الكرتنة"]);
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns["الكمية المتاحة"], dr["الكمية المتاحة"]);
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns["Type"], "عادى");
                        
                        string q5 = "select Data_ID from storage_least_taswya";
                        string q = "SELECT data.Data_ID,SUM(storage.Total_Meters) as 'الكمية المتاحة',least_offer.Least_Quantity as 'الحد الادنى' FROM least_offer INNER JOIN data ON least_offer.Data_ID = data.Data_ID INNER JOIN storage ON storage.Data_ID = data.Data_ID group by data.Data_ID having (SUM(storage.Total_Meters) <= least_offer.Least_Quantity=1) and data.Data_ID not in (" + q5 + ") and data.Data_ID=" + dr["Data_ID"].ToString();
                        MySqlCommand comand2 = new MySqlCommand(q, dbconnection2);
                        MySqlDataReader dr2 = comand2.ExecuteReader();
                        while (dr2.Read())
                        {
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns["الحد الادنى"], dr2["الحد الادنى"]);
                        }
                        dr2.Close();
                        q = "SELECT order_details.Data_ID FROM orders INNER JOIN order_details ON order_details.Order_ID = orders.Order_ID where orders.Received=0 and orders.Canceled=0 and order_details.Data_ID=" + dr["Data_ID"].ToString();
                        comand2 = new MySqlCommand(q, dbconnection2);
                        dr2 = comand2.ExecuteReader();
                        if (dr2.HasRows)
                        {
                            while (dr2.Read())
                            {
                                gridView1.SetRowCellValue(rowHandle, gridView1.Columns["الحالة"], true);
                            }
                            dr2.Close();
                        }
                        else
                        {
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns["الحالة"], false);
                        }
                        dr2.Close();
                    }
                }
                dr.Close();
                gridView1.Columns[0].Visible = false;
                gridView1.Columns["Type"].Visible = false;

                for (int i = 2; i < gridView1.Columns.Count; i++)
                {
                    gridView1.Columns[i].Width = 100;
                }
                gridView1.Columns["الكود"].Width = 180;
                gridView1.Columns["الاسم"].Width = 270;
                gridView1.Columns["الكرتنة"].Width = 60;
                if (gridView1.IsLastVisibleRow)
                {
                    gridView1.FocusedRowHandle = gridView1.RowCount - 1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
            dbconnection2.Close();
        }

        private void btnNewChosen_Click(object sender, EventArgs e)
        {
            clearCom();
        }

        private void gridView1_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            try
            {
                row1 = gridView1.GetDataRow(/*gridView1.GetRowHandle(*/e.RowHandle/*)*/);
                rowHandl = e.RowHandle;
                string value = row1["الكود"].ToString();
                txtCode.Text = value;

                loaded = false;
                txtTotalMeters.Text = "0";
                txtCarton.Text = "0";
                txtBalat.Text = "0";
                if (request == "request")
                {
                    txtTotalMeters.Text = row1["عدد المتر/القطعة"].ToString();
                    txtCarton.Text = row1["عدد الكراتين"].ToString();
                    txtBalat.Text = row1["عدد البلتات"].ToString();
                }
                double carton = double.Parse(row1["الكرتنة"].ToString());
                if (carton == 0)
                {
                    txtCarton.ReadOnly = true;
                    txtBalat.ReadOnly = true;
                }
                else
                {
                    txtCarton.ReadOnly = false;
                    txtBalat.ReadOnly = false;
                }
                loaded = true;

                if (e.Column.ToString() == "الكمية المتاحة")
                {
                    StoresDetails sd = new StoresDetails(row1["Data_ID"].ToString());
                    sd.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txtNumCarton_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (loaded && row1 != null && !flagCarton)
                {
                    int NoCartons = 0;
                    double totalMeter = 0;
                    double carton = double.Parse(row1["الكرتنة"].ToString());
                    if (carton > 0)
                    {
                        if (int.TryParse(txtCarton.Text, out NoCartons))
                        { }
                        if (double.TryParse(txtTotalMeters.Text, out totalMeter))
                        { }

                        double total = carton * NoCartons;
                        flag = true;
                        txtTotalMeters.Text = (total).ToString();
                        flag = false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void txtTotalMeter_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (loaded && row1 != null && !flag)
                {
                    double totalMeter = 0;
                    if (double.TryParse(txtTotalMeters.Text, out totalMeter))
                    {
                        double carton = double.Parse(row1["الكرتنة"].ToString());
                        if (carton > 0)
                        {
                            flagCarton = true;
                            double total = totalMeter / carton;
                            txtCarton.Text = Convert.ToInt16(total).ToString();
                            flagCarton = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (addFlage && row1 != null && txtTotalMeters.Text != "" && txtEmployee.Text != "" && comStore.SelectedValue != null && comFactory.SelectedValue != null && txtDashOrderNum.Text != "")
                {
                    double total;
                    int balate, cartons_balate = 0;
                    if (double.TryParse(txtTotalMeters.Text, out total) && int.TryParse(txtBalat.Text, out balate) && int.TryParse(txtCarton.Text, out cartons_balate))
                    {
                        bool stat = Convert.ToBoolean(row1["الحالة"]);
                        if (IsItemAdded() || stat)
                        {
                            MessageBox.Show("هذا العنصر تم اضافتة من قبل");
                            return;
                        }

                        dbconnection.Open();
                        if (orderId == 0)
                        {
                            string query2 = "insert into orders (Factory_ID,Supplier_ID,Order_Number,Store_ID,Employee_Name,Request_Date,Receive_Date,Employee_ID,Dash_Order_Number,DashOrder_ID)values(@Factory_ID,@Supplier_ID,@Order_Number,@Store_ID,@Employee_Name,@Request_Date,@Recive_Date,@Employee_ID,@Dash_Order_Number,@DashOrder_ID)";
                            MySqlCommand com2 = new MySqlCommand(query2, dbconnection);

                            com2.Parameters.Add("@Factory_ID", MySqlDbType.Int16);
                            com2.Parameters["@Factory_ID"].Value = comFactory.SelectedValue.ToString();
                            com2.Parameters.Add("@Order_Number", MySqlDbType.Int16);
                            com2.Parameters["@Order_Number"].Value = orderNumber;
                            com2.Parameters.Add("@Employee_Name", MySqlDbType.VarChar);
                            com2.Parameters["@Employee_Name"].Value = txtEmployee.Text;
                            com2.Parameters.Add("@Request_Date", MySqlDbType.Date);
                            com2.Parameters["@Request_Date"].Value = dateTimePicker1.Value.Date;
                            com2.Parameters.Add("@Recive_Date", MySqlDbType.Date);
                            com2.Parameters["@Recive_Date"].Value = dateTimePicker2.Value.Date;
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
                            com2.Parameters.Add("@Dash_Order_Number", MySqlDbType.Int16);
                            com2.Parameters["@Dash_Order_Number"].Value = dashOrderNumber;
                            com2.Parameters.Add("@DashOrder_ID", MySqlDbType.Int16);
                            com2.Parameters["@DashOrder_ID"].Value = dashOrderID;
                            com2.ExecuteNonQuery();

                            query2 = "select Order_ID from orders order by Order_ID desc limit 1";
                            com2 = new MySqlCommand(query2, dbconnection);
                            orderId = Convert.ToInt16(com2.ExecuteScalar().ToString());
                        }
                        
                        string query = "insert into order_details (Order_ID,Data_ID,Balatat,Carton_Balata,Quantity,Type) values (@Order_ID,@Data_ID,@Balatat,@Carton_Balata,@Quantity,@Type)";
                        MySqlCommand com = new MySqlCommand(query, dbconnection);
                        com.Parameters.Add("@Order_ID", MySqlDbType.Int16);
                        com.Parameters["@Order_ID"].Value = orderId;
                        com.Parameters.Add("@Data_ID", MySqlDbType.Int16);
                        com.Parameters["@Data_ID"].Value = row1["Data_ID"].ToString();
                        com.Parameters.Add("@Balatat", MySqlDbType.Int16);
                        com.Parameters["@Balatat"].Value = balate;
                        com.Parameters.Add("@Carton_Balata", MySqlDbType.Int16);
                        com.Parameters["@Carton_Balata"].Value = cartons_balate;
                        com.Parameters.Add("@Quantity", MySqlDbType.Decimal);
                        com.Parameters["@Quantity"].Value = total;
                        com.Parameters.Add("@Type", MySqlDbType.VarChar);
                        com.Parameters["@Type"].Value = row1["Type"].ToString();
                        com.ExecuteNonQuery();

                        gridView2.AddNewRow();
                        int rowHandle = gridView2.GetRowHandle(gridView2.DataRowCount);
                        if (gridView2.IsNewItemRow(rowHandle) && row1 != null)
                        {
                            gridView2.SetRowCellValue(rowHandle, gridView2.Columns["Data_ID"], row1["Data_ID"].ToString());
                            gridView2.SetRowCellValue(rowHandle, gridView2.Columns["Code"], row1["الكود"].ToString());
                            gridView2.SetRowCellValue(rowHandle, gridView2.Columns["ItemType"], row1["النوع"].ToString());
                            gridView2.SetRowCellValue(rowHandle, gridView2.Columns["ItemName"], row1["الاسم"].ToString());
                            gridView2.SetRowCellValue(rowHandle, gridView2.Columns["Balatat"], balate);
                            gridView2.SetRowCellValue(rowHandle, gridView2.Columns["Cartons_Balate"], cartons_balate);
                            gridView2.SetRowCellValue(rowHandle, gridView2.Columns["TotalQuantity"], total);
                            gridView2.SetRowCellValue(rowHandle, gridView2.Columns["Carton"], row1["الكرتنة"].ToString());
                            gridView2.SetRowCellValue(rowHandle, gridView2.Columns["Type"], row1["Type"].ToString());
                        }

                        gridView1.SetRowCellValue(rowHandl, gridView1.Columns["الحالة"], true);
                        txtDashOrderNum.ReadOnly = true;
                        dateTimePicker2.Enabled = false;
                        row1 = null;
                        txtCode.Text = "";
                        txtTotalMeters.Text = "0";
                        txtCarton.Text = "0";
                        txtBalat.Text = "0";
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
                            string query = "delete from order_details where Order_ID=" + orderId + " and Data_ID=" + gridView2.GetRowCellDisplayText(gridView2.GetSelectedRows()[j], "Data_ID");
                            MySqlCommand com = new MySqlCommand(query, dbconnection);
                            com.ExecuteNonQuery();

                            for (int i = 0; i < gridView1.RowCount; i++)
                            {
                                DataRow row2 = gridView1.GetDataRow(gridView1.GetRowHandle(i));
                                if (row2["Data_ID"].ToString() == gridView2.GetRowCellDisplayText(gridView2.GetSelectedRows()[j], "Data_ID"))
                                {
                                    gridView1.SetRowCellValue(gridView1.GetRowHandle(i), gridView1.Columns["الحالة"], false);
                                    break;
                                }
                            }
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
                if (gridView2.RowCount > 0 && txtOrderNum.Text != "" && txtDashOrderNum.Text != "" && txtEmployee.Text != "" && comStore.SelectedValue != null && comFactory.SelectedValue != null)
                {
                    dbconnection.Open();
                    string query = "update orders set Confirmed=1 where Order_ID=" + orderId;
                    MySqlCommand com = new MySqlCommand(query, dbconnection);
                    com.ExecuteNonQuery();

                    query = "update dash_orders set Confirmed=1 where DashOrder_ID=" + dashOrderID;
                    com = new MySqlCommand(query, dbconnection);
                    com.ExecuteNonQuery();

                    #region report
                    List<Order_Items> bi = new List<Order_Items>();
                    for (int i = 0; i < gridView2.RowCount; i++)
                    {
                        int rowHand = gridView2.GetRowHandle(i);
                        
                        Order_Items item = new Order_Items() { Code = gridView2.GetRowCellDisplayText(rowHand, gridView2.Columns["Code"]), Product_Type = gridView2.GetRowCellDisplayText(rowHand, gridView2.Columns["ItemType"]), Product_Name = gridView2.GetRowCellDisplayText(rowHand, gridView2.Columns["ItemName"]), Balatat = Convert.ToInt16(gridView2.GetRowCellDisplayText(rowHand, gridView2.Columns["Balatat"])), Total_Meters = Convert.ToDouble(gridView2.GetRowCellDisplayText(rowHand, gridView2.Columns["TotalQuantity"])) };
                        bi.Add(item);
                    }
                    Report_Order f = new Report_Order();
                    f.PrintInvoice(comFactory.Text, Convert.ToInt16(txtOrderNum.Text), txtEmployee.Text, bi);
                    f.ShowDialog();
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

        //functions
        public void search()
        {
            dbconnection.Close();
            dbconnection.Open();
            dbconnection2.Open();

            string query = "select data.Data_ID,data.Code as 'الكود',type.Type_Name as 'النوع',concat(product.Product_Name,' - ',type.Type_Name,' - ',factory.Factory_Name,' - ',groupo.Group_Name,' ',COALESCE(color.Color_Name,''),' ',COALESCE(size.Size_Value,''),' ',COALESCE(sort.Sort_Value,'')) as 'الاسم',sum(storage.Total_Meters) as 'الكمية المتاحة','الحد الادنى',data.Carton as 'الكرتنة','Type','الحالة' FROM data LEFT JOIN color ON color.Color_ID = data.Color_ID LEFT JOIN size ON size.Size_ID = data.Size_ID LEFT JOIN sort ON sort.Sort_ID = data.Sort_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID INNER JOIN factory ON factory.Factory_ID = data.Factory_ID  INNER JOIN product ON product.Product_ID = data.Product_ID  INNER JOIN type ON type.Type_ID = data.Type_ID LEFT JOIN storage ON storage.Data_ID = data.Data_ID where data.Data_ID=0 group by data.Data_ID";
            MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
            DataTable dt = new DataTable();
            da.Fill(dt);
            gridControl1.DataSource = dt;

            RepositoryItemCheckEdit repositoryCheckEdit1 = gridControl1.RepositoryItems.Add("CheckEdit") as RepositoryItemCheckEdit;
            repositoryCheckEdit1.ValueChecked = "True";
            repositoryCheckEdit1.ValueUnchecked = "False";
            gridView1.Columns["الحالة"].ColumnEdit = repositoryCheckEdit1;
            //repositoryCheckEdit1.CheckedChanged += new EventHandler(CheckedChanged);

            string dataIds = "";
            for (int i = 0; i < lstrow.Count - 1; i++)
            {
                dataIds += lstrow[i]["Data_ID"].ToString() + ",";
            }
            dataIds += lstrow[lstrow.Count - 1]["Data_ID"].ToString();

            query = "SELECT data.Data_ID,data.Code as 'الكود',type.Type_Name as 'النوع',concat(product.Product_Name,' ',COALESCE(color.Color_Name,''),' ',data.Description,' ',groupo.Group_Name,' ',factory.Factory_Name,' ',COALESCE(size.Size_Value,''),' ',COALESCE(sort.Sort_Value,'')) as 'الاسم',data.Carton as 'الكرتنة',sum(storage.Total_Meters) as 'الكمية المتاحة' FROM data LEFT JOIN color ON color.Color_ID = data.Color_ID LEFT JOIN size ON size.Size_ID = data.Size_ID LEFT JOIN sort ON sort.Sort_ID = data.Sort_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID INNER JOIN factory ON factory.Factory_ID = data.Factory_ID  INNER JOIN product ON product.Product_ID = data.Product_ID  INNER JOIN type ON type.Type_ID = data.Type_ID  LEFT JOIN storage ON storage.Data_ID = data.Data_ID where data.Data_ID in (" + dataIds + ") group by data.Data_ID order by SUBSTR(data.Code,1,16),color.Color_Name,data.Sort_ID";
            MySqlCommand comand = new MySqlCommand(query, dbconnection);
            MySqlDataReader dr = comand.ExecuteReader();
            while (dr.Read())
            {
                gridView1.AddNewRow();
                int rowHandle = gridView1.GetRowHandle(gridView1.DataRowCount);
                if (gridView1.IsNewItemRow(rowHandle))
                {
                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns[0], dr["Data_ID"]);
                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns[1], dr["الكود"]);
                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns["النوع"], dr["النوع"]);
                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns["الاسم"], dr["الاسم"]);
                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns["الكرتنة"], dr["الكرتنة"]);
                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns["الكمية المتاحة"], dr["الكمية المتاحة"]);
                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns["Type"], "الحد الادنى");
                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns["الحالة"], false);
                    
                    string q5 = "select Data_ID from storage_least_taswya";
                    string q = "SELECT data.Data_ID,SUM(storage.Total_Meters) as 'الكمية المتاحة',least_offer.Least_Quantity as 'الحد الادنى' FROM least_offer INNER JOIN data ON least_offer.Data_ID = data.Data_ID INNER JOIN storage ON storage.Data_ID = data.Data_ID group by data.Data_ID having (SUM(storage.Total_Meters) <= least_offer.Least_Quantity=1) and data.Data_ID not in (" + q5 + ") and data.Data_ID=" + dr["Data_ID"].ToString();
                    MySqlCommand comand2 = new MySqlCommand(q, dbconnection2);
                    MySqlDataReader dr2 = comand2.ExecuteReader();
                    while (dr2.Read())
                    {
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns["الحد الادنى"], dr2["الحد الادنى"]);
                    }
                    dr2.Close();
                }
            }
            dr.Close();
            gridView1.Columns[0].Visible = false;
            gridView1.Columns["Type"].Visible = false;

            for (int i = 2; i < gridView1.Columns.Count; i++)
            {
                gridView1.Columns[i].Width = 100;
            }
            gridView1.Columns["الكود"].Width = 180;
            gridView1.Columns["الاسم"].Width = 270;
            gridView1.Columns["الكرتنة"].Width = 60;
            if (gridView1.IsLastVisibleRow)
            {
                gridView1.FocusedRowHandle = gridView1.RowCount - 1;
            }
        }

        public void clearCom()
        {
            foreach (Control co in this.groupBox2.Controls)
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

        bool IsItemAdded()
        {
            for (int i = 0; i < gridView2.RowCount; i++)
            {
                DataRow row3 = gridView2.GetDataRow(gridView2.GetRowHandle(i));
                if (row1["Data_ID"].ToString() == row3["Data_ID"].ToString())
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
            txtDashOrderNum.Text = "";
            comStore.SelectedIndex = -1;
            txtStoreID.Text = "";
            txtEmployee.Text = "";
            orderId = 0;
            orderNumber = 0;
            dashOrderNumber = 0;
            dashOrderID = 0;
            row1 = null;
            lstrow = new List<DataRow>();
            txtCode.Text = "";
            txtTotalMeters.Text = "0";
            txtCarton.Text = "0";
            txtBalat.Text = "0";
            /*txtEmployee.ReadOnly = false;
            comStore.Enabled = true;
            txtStoreID.ReadOnly = false;
            comSupplier.Enabled = true;
            txtSupplier.ReadOnly = false;
            dateTimePicker1.Enabled = true;*/
            dateTimePicker2.Enabled = true;

            checkBoxAvailable.Checked = false;
            checkBoxCanceled.Checked = false;
            checkBoxConfirmed.Checked = false;
            checkBoxReceived.Checked = false;
            checkBoxReceivedPart.Checked = false;
            clearCom();
            loaded = true;
        }
    }
}

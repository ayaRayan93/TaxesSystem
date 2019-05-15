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
    public partial class OrderStored : Form
    {
        MySqlConnection dbconnection;
        bool loaded = false;

        public OrderStored(MainForm mainform, XtraTabControl tabControlContent)
        {
            InitializeComponent();
            dbconnection = new MySqlConnection(connection.connectionString);
            
        }
        private void requestStored_Load(object sender, EventArgs e)
        {
            try
            {
                string query = "select * from factory";
                MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                DataTable dt = new DataTable();
                da.Fill(dt);
                comFactory.DataSource = dt;
                comFactory.DisplayMember = dt.Columns["Factory_Name"].ToString();
                comFactory.ValueMember = dt.Columns["Factory_ID"].ToString();
                comFactory.Text = "";
                txtFactoryID.Text = "";

                loaded = true;
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
                if (loaded)
                {
                    txtFactoryID.Text = comFactory.SelectedValue.ToString();
                    labBillNumber.Visible = true;
                    txtBillNumber.Visible = true;
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
                    dbconnection.Open();
                    string query = "select Factory_Name from factory where Factory_ID=" + txtFactoryID.Text + "";
                    MySqlCommand com = new MySqlCommand(query, dbconnection);
                    if (com.ExecuteScalar() != null)
                    {
                        Name = (string)com.ExecuteScalar();
                        comFactory.Text = Name;
                        comFactory.SelectedValue = txtFactoryID.Text;
                    }
                    else
                    {
                        MessageBox.Show("there is no item with this id");

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                dbconnection.Close();
            }
        }

        private void txtBillNumber_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    DataSet sourceDataSet = new DataSet();
                    int factoryID, billNum = 0;
                    MySqlDataAdapter adapterOrder = null;
                    MySqlDataAdapter adapterDetails = null;
                    if (int.TryParse(txtFactoryID.Text, out factoryID) && comFactory.SelectedValue != null && int.TryParse(txtBillNumber.Text, out billNum))
                    {
                        adapterOrder = new MySqlDataAdapter("select orders.Order_ID as 'التسلسل',factory.Factory_Name as 'المصنع',orders.Order_Number as 'رقم الفاتورة',orders.Employee_Name as 'الموظف المسئول',store.Store_Name as 'المخزن',orders.Request_Date as 'تاريخ الطلب',orders.Receive_Date as 'تاريخ الاستلام' from orders inner join factory on factory.Factory_ID=orders.Factory_ID inner join store on store.Store_ID=orders.Store_ID where orders.Confirmed=1 and orders.Received=0 and orders.Canceled=0 and orders.Factory_ID=" + factoryID + " and orders.Order_Number=" + billNum, dbconnection);
                        adapterDetails = new MySqlDataAdapter("SELECT orders.Order_ID as 'التسلسل',data.Code as 'الكود',type.Type_Name as 'النوع',concat(product.Product_Name,' ',COALESCE(color.Color_Name,''),' ',data.Description,' ',groupo.Group_Name,' ',factory.Factory_Name,' ',COALESCE(size.Size_Value,''),' ',COALESCE(sort.Sort_Value,'')) as 'الاسم',order_details.Quantity as 'عدد متر/قطعة' FROM orders INNER JOIN order_details ON orders.Order_ID = order_details.Order_ID INNER JOIN data ON order_details.Data_ID = data.Data_ID LEFT JOIN color ON color.Color_ID = data.Color_ID LEFT JOIN size ON size.Size_ID = data.Size_ID LEFT JOIN sort ON sort.Sort_ID = data.Sort_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID INNER JOIN factory ON factory.Factory_ID = data.Factory_ID  INNER JOIN product ON product.Product_ID = data.Product_ID  INNER JOIN type ON type.Type_ID = data.Type_ID where orders.Confirmed=1 and orders.Received=0 and orders.Canceled=0 and orders.Factory_ID=" + factoryID + " and orders.Order_Number=" + billNum, dbconnection);
                        adapterOrder.Fill(sourceDataSet, "orders");
                        adapterDetails.Fill(sourceDataSet, "order_details");
                        //Set up a master-detail relationship between the DataTables 
                        DataColumn keyColumn = sourceDataSet.Tables["orders"].Columns["التسلسل"];
                        DataColumn foreignKeyColumn = sourceDataSet.Tables["order_details"].Columns["التسلسل"];
                        sourceDataSet.Relations.Add("تفاصيل الطلب", keyColumn, foreignKeyColumn);
                        gridControl1.DataSource = sourceDataSet.Tables["orders"];
                    }
                    else
                    {
                        MessageBox.Show("تاكد من البيانات");
                    }
                   
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                dbconnection.Close();
            }
        }

        private void btnDisplay_Click(object sender, EventArgs e)
        {
            try
            {
                DataSet sourceDataSet = new DataSet();
                DateTime date = dateTimePicker1.Value.Date;
                string d = date.ToString("yyyy-MM-dd");
                DateTime date2 = dateTimePicker2.Value.Date;
                string d2 = date2.ToString("yyyy-MM-dd");
                int factoryID = 0;
                MySqlDataAdapter adapterOrder = null;
                MySqlDataAdapter adapterDetails = null;
                if (int.TryParse(txtFactoryID.Text, out factoryID) && comFactory.SelectedValue != null)
                {
                    adapterOrder = new MySqlDataAdapter("select orders.Order_ID as 'التسلسل',factory.Factory_Name as 'المصنع',orders.Order_Number as 'رقم الطلب',orders.Employee_Name as 'الموظف المسئول',store.Store_Name as 'المخزن',orders.Request_Date as 'تاريخ الطلب',orders.Receive_Date as 'تاريخ الاستلام' from orders inner join factory on factory.Factory_ID=orders.Factory_ID inner join store on store.Store_ID=orders.Store_ID where orders.Confirmed=1 and orders.Received=0 and orders.Canceled=0 and orders.Factory_ID=" + factoryID + " and Request_Date >='" + d + "' and Request_Date <='" + d2 + "'", dbconnection);
                    adapterDetails = new MySqlDataAdapter("SELECT orders.Order_ID as 'التسلسل',data.Code as 'الكود',type.Type_Name as 'النوع',concat(product.Product_Name,' ',COALESCE(color.Color_Name,''),' ',data.Description,' ',groupo.Group_Name,' ',factory.Factory_Name,' ',COALESCE(size.Size_Value,''),' ',COALESCE(sort.Sort_Value,'')) as 'الاسم',order_details.Quantity as 'عدد متر/قطعة' FROM orders INNER JOIN order_details ON orders.Order_ID = order_details.Order_ID INNER JOIN data ON order_details.Data_ID = data.Data_ID LEFT JOIN color ON color.Color_ID = data.Color_ID LEFT JOIN size ON size.Size_ID = data.Size_ID LEFT JOIN sort ON sort.Sort_ID = data.Sort_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID INNER JOIN factory ON factory.Factory_ID = data.Factory_ID  INNER JOIN product ON product.Product_ID = data.Product_ID  INNER JOIN type ON type.Type_ID = data.Type_ID where orders.Confirmed=1 and orders.Received=0 and orders.Canceled=0 and orders.Factory_ID=" + factoryID + " and Request_Date >='" + d + "' and Request_Date <='" + d2 + "'", dbconnection);
                }
                else
                {
                    adapterOrder = new MySqlDataAdapter("select orders.Order_ID as 'التسلسل',factory.Factory_Name as 'المصنع',orders.Order_Number as 'رقم الطلب',orders.Employee_Name as 'الموظف المسئول',store.Store_Name as 'المخزن',orders.Request_Date as 'تاريخ الطلب',orders.Receive_Date as 'تاريخ الاستلام' from orders inner join factory on factory.Factory_ID=orders.Factory_ID inner join store on store.Store_ID=orders.Store_ID where orders.Confirmed=1 and orders.Received=0 and orders.Canceled=0 and Request_Date >='" + d + "' and Request_Date <='" + d2 + "'", dbconnection);
                    adapterDetails = new MySqlDataAdapter("SELECT orders.Order_ID as 'التسلسل',data.Code as 'الكود',type.Type_Name as 'النوع',concat(product.Product_Name,' ',COALESCE(color.Color_Name,''),' ',data.Description,' ',groupo.Group_Name,' ',factory.Factory_Name,' ',COALESCE(size.Size_Value,''),' ',COALESCE(sort.Sort_Value,'')) as 'الاسم',order_details.Quantity as 'عدد متر/قطعة' FROM orders INNER JOIN order_details ON orders.Order_ID = order_details.Order_ID INNER JOIN data ON order_details.Data_ID = data.Data_ID LEFT JOIN color ON color.Color_ID = data.Color_ID LEFT JOIN size ON size.Size_ID = data.Size_ID LEFT JOIN sort ON sort.Sort_ID = data.Sort_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID INNER JOIN factory ON factory.Factory_ID = data.Factory_ID  INNER JOIN product ON product.Product_ID = data.Product_ID  INNER JOIN type ON type.Type_ID = data.Type_ID where orders.Confirmed=1 and orders.Received=0 and orders.Canceled=0 and Request_Date >='" + d + "' and Request_Date <='" + d2 + "'", dbconnection);
                }
                adapterOrder.Fill(sourceDataSet, "orders");
                adapterDetails.Fill(sourceDataSet, "order_details");

                //Set up a master-detail relationship between the DataTables 
                DataColumn keyColumn = sourceDataSet.Tables["orders"].Columns["التسلسل"];
                DataColumn foreignKeyColumn = sourceDataSet.Tables["order_details"].Columns["التسلسل"];
                sourceDataSet.Relations.Add("تفاصيل الطلب", keyColumn, foreignKeyColumn);
                gridControl1.DataSource = sourceDataSet.Tables["orders"];
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
                if (gridView1.RowCount > 0)
                {
                    List<ReportOrder_Items> bi = new List<ReportOrder_Items>();
                    for (int i = 0; i < gridView1.RowCount; i++)
                    {
                        int rowHand = gridView1.GetRowHandle(i);
                        GridView view2 = (gridView1.GetDetailView(rowHand, gridView1.GetRelationIndex(rowHand, "تفاصيل الطلب"))) as GridView;
                        List<ReportOrderDetails_Items> childItem = new List<ReportOrderDetails_Items>();
                        for (int j = 0; j < view2.RowCount; j++)
                        {
                            ReportOrderDetails_Items item2 = new ReportOrderDetails_Items();
                            item2.ChildOrder_ID = Convert.ToInt16(view2.GetRowCellDisplayText(j, view2.Columns["التسلسل"]));
                            item2.Code = view2.GetRowCellDisplayText(j, view2.Columns["الكود"]);
                            item2.Product_Type = view2.GetRowCellDisplayText(j, view2.Columns["النوع"]);
                            item2.Product_Name = view2.GetRowCellDisplayText(j, view2.Columns["الاسم"]); /*Balatat = Convert.ToInt16(gridView1.GetRowCellDisplayText(rowHand, gridView1.Columns["عدد البلتات"])),*/
                            item2.Total_Meters = Convert.ToDouble(view2.GetRowCellDisplayText(j, view2.Columns["عدد متر/قطعة"]));
                            childItem.Add(item2);
                        }
                        ReportOrder_Items item = new ReportOrder_Items() { Order_ID = Convert.ToInt16(gridView1.GetRowCellDisplayText(rowHand, gridView1.Columns["التسلسل"])), Factory_Name = gridView1.GetRowCellDisplayText(rowHand, gridView1.Columns["المصنع"]), Order_Number = Convert.ToInt16(gridView1.GetRowCellDisplayText(rowHand, gridView1.Columns["رقم الطلب"])), Employee_Name = gridView1.GetRowCellDisplayText(rowHand, gridView1.Columns["الموظف المسئول"]), Store_Name = gridView1.GetRowCellDisplayText(rowHand, gridView1.Columns["المخزن"]), items = childItem };
                        bi.Add(item);
                    }
                    Report_OrderSearch f = new Report_OrderSearch();
                    f.PrintInvoice(bi);
                    f.ShowDialog();
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
        }
    }
}

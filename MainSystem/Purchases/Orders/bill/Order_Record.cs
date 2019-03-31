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
    public partial class Order_Record : Form
    {
        MySqlConnection dbconnection, dbconnection6;
        //bool loaded = false;
        List<DataRow> lstrow = new List<DataRow>();
        DataRow row1 = null;
        bool loaded = false;
        bool factoryFlage = false;
        bool groupFlage = false;
        bool flagProduct = false;

        public Order_Record(List<DataRow> Row1, Order_Report OrderReport, XtraTabControl xtraTabControlPurchases)
        {
            InitializeComponent();
            dbconnection = new MySqlConnection(connection.connectionString);
            dbconnection6 = new MySqlConnection(connection.connectionString);
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

                dbconnection.Open();
                string query = "select * from type";
                MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                DataTable dt = new DataTable();
                da.Fill(dt);
                comType.DataSource = dt;
                comType.DisplayMember = dt.Columns["Type_Name"].ToString();
                comType.ValueMember = dt.Columns["Type_ID"].ToString();
                comType.Text = "";
                txtType.Text = "";
                
                query = "select * from supplier";
                da = new MySqlDataAdapter(query, dbconnection);
                dt = new DataTable();
                da.Fill(dt);
                comSupplier.DataSource = dt;
                comSupplier.DisplayMember = dt.Columns["Supplier_Name"].ToString();
                comSupplier.ValueMember = dt.Columns["Supplier_ID"].ToString();
                comSupplier.Text = "";
                
                query = "select * from store";
                da = new MySqlDataAdapter(query, dbconnection);
                dt = new DataTable();
                da.Fill(dt);
                comStore.DataSource = dt;
                comStore.DisplayMember = dt.Columns["Store_Name"].ToString();
                comStore.ValueMember = dt.Columns["Store_ID"].ToString();
                comStore.Text = "";
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
            dbconnection6.Close();
        }

        private void GridView2_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            GridView view = sender as GridView;
            view.SetRowCellValue(e.RowHandle, gridView2.Columns[0], 0);
            view.SetRowCellValue(e.RowHandle, gridView2.Columns[1], "");
            view.SetRowCellValue(e.RowHandle, gridView2.Columns[2], "");
            view.SetRowCellValue(e.RowHandle, gridView2.Columns[3], "");
            view.SetRowCellValue(e.RowHandle, gridView2.Columns[4], "0");
            view.SetRowCellValue(e.RowHandle, gridView2.Columns[5], "0");
            view.SetRowCellValue(e.RowHandle, gridView2.Columns[6], "0");
        }

        private void comBox_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (loaded)
                {
                    ComboBox comBox = (ComboBox)sender;

                    switch (comBox.Name)
                    {
                        case "comType":
                            if (loaded)
                            {
                                string query = "select * from factory inner join type_factory on factory.Factory_ID=type_factory.Factory_ID inner join type on type_factory.Type_ID=type.Type_ID where type_factory.Type_ID=" + comType.SelectedValue.ToString();
                                MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                                DataTable dt = new DataTable();
                                da.Fill(dt);
                                comFactory.DataSource = dt;
                                comFactory.DisplayMember = dt.Columns["Factory_Name"].ToString();
                                comFactory.ValueMember = dt.Columns["Factory_ID"].ToString();
                                comFactory.Text = "";
                                dbconnection.Close();
                                dbconnection.Open();
                                query = "select TypeCoding_Method from type where Type_ID=" + comType.SelectedValue.ToString();
                                MySqlCommand com = new MySqlCommand(query, dbconnection);
                                int TypeCoding_Method = (int)com.ExecuteScalar();
                                dbconnection.Close();
                                if (TypeCoding_Method == 1)
                                {
                                    string query2 = "";
                                    if (comType.SelectedValue.ToString() == "2" || comType.SelectedValue.ToString() == "1")
                                    {
                                        query2 = "select * from groupo where Factory_ID=-1";
                                    }
                                    else
                                    {
                                        query2 = "select * from groupo where Factory_ID=" + -Convert.ToInt16(comType.SelectedValue.ToString()) + " and Type_ID=" + comType.SelectedValue.ToString();
                                    }

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

                                query = "select * from color where Type_ID=" + comType.SelectedValue.ToString();
                                da = new MySqlDataAdapter(query, dbconnection);
                                dt = new DataTable();
                                da.Fill(dt);
                                comColor.DataSource = dt;
                                comColor.DisplayMember = dt.Columns["Color_Name"].ToString();
                                comColor.ValueMember = dt.Columns["Color_ID"].ToString();
                                comColor.Text = "";
                                comFactory.Focus();
                            }
                            break;
                        case "comFactory":
                            if (factoryFlage)
                            {
                                dbconnection.Close();
                                dbconnection.Open();
                                string query = "select TypeCoding_Method from type where Type_ID=" + comType.SelectedValue.ToString();
                                MySqlCommand com = new MySqlCommand(query, dbconnection);
                                int TypeCoding_Method = (int)com.ExecuteScalar();
                                dbconnection.Close();
                                if (TypeCoding_Method == 2)
                                {
                                    string query2f = "select * from groupo where Type_ID=" + comType.SelectedValue.ToString() + " and Factory_ID=" + comFactory.SelectedValue.ToString();
                                    MySqlDataAdapter da2f = new MySqlDataAdapter(query2f, dbconnection);
                                    DataTable dt2f = new DataTable();
                                    da2f.Fill(dt2f);
                                    comGroup.DataSource = dt2f;
                                    comGroup.DisplayMember = dt2f.Columns["Group_Name"].ToString();
                                    comGroup.ValueMember = dt2f.Columns["Group_ID"].ToString();
                                    comGroup.Text = "";
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
                                comGroup.Focus();
                            }
                            break;
                        case "comGroup":
                            if (groupFlage)
                            {
                                string supQuery = "", subQuery1 = "";
                                if (comType.SelectedValue.ToString() != "")
                                {
                                    supQuery += " and product.Type_ID=" + comType.SelectedValue.ToString();
                                }
                                if (comFactory.SelectedValue.ToString() != "")
                                {
                                    supQuery += " and product_factory_group.Factory_ID=" + comFactory.SelectedValue.ToString();
                                    subQuery1 += " and Factory_ID=" + comFactory.SelectedValue.ToString();
                                }
                                string query3 = "select distinct  product.Product_ID  ,Product_Name  from product inner join product_factory_group on product.Product_ID=product_factory_group.Product_ID  where product_factory_group.Group_ID=" + comGroup.SelectedValue.ToString() + supQuery + "  order by product.Product_ID";
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
                            comColor.Focus();
                            break;
                        case "comColor":
                            comSize.Focus();
                            break;
                        case "comSize":
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
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
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
                            case "txtType":
                                query = "select Type_Name from type where Type_ID='" + txtType.Text + "'";
                                com = new MySqlCommand(query, dbconnection);
                                if (com.ExecuteScalar() != null)
                                {
                                    Name = (string)com.ExecuteScalar();
                                    comType.Text = Name;
                                    txtFactory.Focus();
                                    dbconnection.Close();
                                }
                                else
                                {
                                    MessageBox.Show("there is no item with this id");
                                    dbconnection.Close();
                                    return;
                                }
                                break;
                            case "txtFactory":
                                query = "select Factory_Name from factory where Factory_ID='" + txtFactory.Text + "'";
                                com = new MySqlCommand(query, dbconnection);
                                if (com.ExecuteScalar() != null)
                                {
                                    Name = (string)com.ExecuteScalar();
                                    comFactory.Text = Name;
                                    txtGroup.Focus();
                                    dbconnection.Close();
                                }
                                else
                                {
                                    MessageBox.Show("there is no item with this id");
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
                                    txtType.Focus();
                                    dbconnection.Close();
                                }
                                else
                                {
                                    MessageBox.Show("there is no item with this id");
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
                                    dbconnection.Close();
                                    return;
                                }
                                break;
                            case "txtSupplier":
                                query = "select Supplier_Name from supplier where Supplier_ID=" + txtSupplier.Text + "";
                                com = new MySqlCommand(query, dbconnection);
                                if (com.ExecuteScalar().ToString() != "")
                                {
                                    Name = (string)com.ExecuteScalar();
                                    comSupplier.Text = Name;
                                    comSupplier.SelectedValue = txtSupplier.Text;
                                }
                                else
                                {
                                    MessageBox.Show("there is no item with this id");
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
                if (lstrow.Count == 0)
                {
                    string q1, q2, q3, q4, fQuery = "";
                    if (comType.Text == "")
                    {
                        q1 = "select Type_ID from type";
                    }
                    else
                    {
                        q1 = comType.SelectedValue.ToString();
                    }
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

                    dbconnection.Open();
                    dbconnection6.Open();

                    string query = "select data.Data_ID,data.Code as 'الكود',type.Type_Name as 'النوع',concat(product.Product_Name,' - ',type.Type_Name,' - ',factory.Factory_Name,' - ',groupo.Group_Name,' ',COALESCE(color.Color_Name,''),' ',COALESCE(size.Size_Value,''),' ',COALESCE(sort.Sort_Value,'')) as 'الاسم',sum(storage.Total_Meters) as 'الكمية المتاحة','الحد الادنى',sellprice.Sell_Price as 'السعر',data.Carton as 'الكرتنة' FROM data LEFT JOIN color ON color.Color_ID = data.Color_ID LEFT JOIN size ON size.Size_ID = data.Size_ID LEFT JOIN sort ON sort.Sort_ID = data.Sort_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID INNER JOIN factory ON factory.Factory_ID = data.Factory_ID  INNER JOIN product ON product.Product_ID = data.Product_ID  INNER JOIN type ON type.Type_ID = data.Type_ID  INNER JOIN sellprice ON sellprice.Data_ID = data.Data_ID LEFT JOIN storage ON storage.Data_ID = data.Data_ID where  data.Type_ID IN(" + q1 + ") and  data.Factory_ID  IN(" + q2 + ") and data.Group_ID IN (" + q4 + ") and data.Data_ID=0 group by data.Data_ID";
                    MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    gridControl1.DataSource = dt;

                    query = "SELECT data.Data_ID,data.Code as 'الكود',type.Type_Name as 'النوع',concat(product.Product_Name,' ',COALESCE(color.Color_Name,''),' ',data.Description,' ',groupo.Group_Name,' ',factory.Factory_Name,' ',COALESCE(size.Size_Value,''),' ',COALESCE(sort.Sort_Value,'')) as 'الاسم',data.Carton as 'الكرتنة',sum(storage.Total_Meters) as 'الكمية المتاحة' FROM data LEFT JOIN color ON color.Color_ID = data.Color_ID LEFT JOIN size ON size.Size_ID = data.Size_ID LEFT JOIN sort ON sort.Sort_ID = data.Sort_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID INNER JOIN factory ON factory.Factory_ID = data.Factory_ID  INNER JOIN product ON product.Product_ID = data.Product_ID  INNER JOIN type ON type.Type_ID = data.Type_ID  LEFT JOIN storage ON storage.Data_ID = data.Data_ID where data.Type_ID IN(" + q1 + ") and data.Factory_ID IN(" + q2 + ") and data.Product_ID IN (" + q3 + ") and data.Group_ID IN (" + q4 + ") " + fQuery + " group by data.Data_ID order by SUBSTR(data.Code,1,16),color.Color_Name,data.Sort_ID";
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
                            string q = "select sellprice.Sell_Price as 'السعر' from data INNER JOIN sellprice ON sellprice.Data_ID = data.Data_ID where data.Data_ID=" + dr["Data_ID"].ToString() + " order by sellprice.Date desc limit 1";
                            MySqlCommand comand2 = new MySqlCommand(q, dbconnection6);
                            MySqlDataReader dr2 = comand2.ExecuteReader();
                            while (dr2.Read())
                            {
                                gridView1.SetRowCellValue(rowHandle, gridView1.Columns["السعر"], dr2["السعر"]);
                            }
                            dr2.Close();
                            string q5 = "select Data_ID from storage_least_taswya";
                            q = "SELECT data.Data_ID,SUM(storage.Total_Meters) as 'الكمية المتاحة',least_offer.Least_Quantity as 'الحد الادنى' FROM least_offer INNER JOIN data ON least_offer.Data_ID = data.Data_ID INNER JOIN storage ON storage.Data_ID = data.Data_ID group by data.Data_ID having (SUM(storage.Total_Meters) <= least_offer.Least_Quantity=1) and data.Data_ID not in (" + q5 + ") and data.Data_ID=" + dr["Data_ID"].ToString();
                            comand2 = new MySqlCommand(q, dbconnection6);
                            dr2 = comand2.ExecuteReader();
                            while (dr2.Read())
                            {
                                gridView1.SetRowCellValue(rowHandle, gridView1.Columns["الحد الادنى"], dr2["الحد الادنى"]);
                            }
                            dr2.Close();
                        }
                    }
                    dr.Close();
                    gridView1.Columns[0].Visible = false;

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
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
            dbconnection6.Close();
        }

        private void btnNewChosen_Click(object sender, EventArgs e)
        {
            clearCom();
        }

        private void gridView1_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            try
            {
                row1 = gridView1.GetDataRow(gridView1.GetRowHandle(e.RowHandle));
                string value = row1["الكود"].ToString();
                txtCode.Text = value;
                txtTotalMeters.Text = "";
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

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (row1 != null && txtTotalMeters.Text != "")
                {
                    double total = 0;
                    if (double.TryParse(txtTotalMeters.Text, out total))
                    {
                        double carton = Convert.ToDouble(row1["الكرتنة"].ToString());

                        if (IsItemAdded())
                        {
                            MessageBox.Show("هذا العنصر تم اضافتة من قبل");
                            return;
                        }
                        
                        gridView2.AddNewRow();
                        int rowHandle = gridView2.GetRowHandle(gridView2.DataRowCount);
                        if (gridView2.IsNewItemRow(rowHandle) && row1 != null)
                        {
                            gridView2.SetRowCellValue(rowHandle, gridView2.Columns["Data_ID"], row1["Data_ID"].ToString());
                            gridView2.SetRowCellValue(rowHandle, gridView2.Columns["Code"], row1["الكود"].ToString());
                            gridView2.SetRowCellValue(rowHandle, gridView2.Columns["ItemType"], row1["النوع"].ToString());
                            gridView2.SetRowCellValue(rowHandle, gridView2.Columns["ItemName"], row1["الاسم"].ToString());
                            gridView2.SetRowCellValue(rowHandle, gridView2.Columns["TotalQuantity"], total);
                            gridView2.SetRowCellValue(rowHandle, gridView2.Columns["Price"], row1["السعر"].ToString());
                            gridView2.SetRowCellValue(rowHandle, gridView2.Columns["Carton"], row1["الكرتنة"].ToString());
                        }
                        txtCode.Text = "";
                        txtTotalMeters.Text = "";
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
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (gridView2.GetSelectedRows().Length > 0)
                {
                    GridView view = gridView2 as GridView;
                    int rowHandle = gridView2.GetRowHandle(gridView2.GetSelectedRows()[0]);
                    view.DeleteSelectedRows();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            try
            {
                int branchId = 0;
                if (gridView2.RowCount > 0 && txtEmployee.Text != "" && comStore.SelectedValue != null && comSupplier.SelectedValue != null)
                {
                    dbconnection.Open();

                    int branchBillNumber = 1;
                    string query = "select BranchBillNumber from orders where Branch_ID=" + branchId + " order by Order_ID desc limit 1";
                    MySqlCommand com = new MySqlCommand(query, dbconnection);
                    if (com.ExecuteScalar() != null)
                    {
                        branchBillNumber = Convert.ToInt16(com.ExecuteScalar().ToString()) + 1;
                    }
                    query = "insert into orders (Supplier_ID,Store_ID,Branch_ID,BranchBillNumber,Employee_Name,Request_Date,Receive_Date,Type,Employee_ID)values(@Supplier_ID,@Store_ID,@Branch_ID,@BranchBillNumber,@Employee_Name,@Request_Date,@Recive_Date,@Type,@Employee_ID)";
                    com = new MySqlCommand(query, dbconnection);

                    com.Parameters.Add("@Employee_Name", MySqlDbType.VarChar);
                    com.Parameters["@Employee_Name"].Value = txtEmployee.Text;
                    com.Parameters.Add("@Request_Date", MySqlDbType.Date);
                    com.Parameters["@Request_Date"].Value = dateTimePicker1.Value.Date;
                    com.Parameters.Add("@Recive_Date", MySqlDbType.Date);
                    com.Parameters["@Recive_Date"].Value = dateTimePicker2.Value.Date;
                    com.Parameters.Add("@Supplier_ID", MySqlDbType.Int16);
                    com.Parameters["@Supplier_ID"].Value = comSupplier.SelectedValue.ToString();
                    com.Parameters.Add("@Store_ID", MySqlDbType.Int16);
                    com.Parameters["@Store_ID"].Value = comStore.SelectedValue.ToString();
                    com.Parameters.Add("@Branch_ID", MySqlDbType.Int16);
                    com.Parameters["@Branch_ID"].Value = UserControl.EmpBranchID;
                    com.Parameters.Add("@BranchBillNumber", MySqlDbType.Int16);
                    com.Parameters["@BranchBillNumber"].Value = branchBillNumber;
                    if (lstrow.Count > 0)
                    {
                        com.Parameters.Add("@Type", MySqlDbType.VarChar);
                        com.Parameters["@Type"].Value = "حد ادنى";
                    }
                    else
                    {
                        com.Parameters.Add("@Type", MySqlDbType.VarChar);
                        com.Parameters["@Type"].Value = "عادى";
                    }
                    com.Parameters.Add("@Employee_ID", MySqlDbType.Int16);
                    com.Parameters["@Employee_ID"].Value = UserControl.EmpID;
                    com.ExecuteNonQuery();

                    query = "select Order_ID from orders order by Order_ID desc limit 1";
                    com = new MySqlCommand(query, dbconnection);

                    int id = Convert.ToInt16(com.ExecuteScalar().ToString());

                    for (int i = 0; i < gridView2.RowCount; i++)
                    {
                        DataRow row2 = gridView2.GetDataRow(gridView2.GetRowHandle(i));
                        query = "insert into order_details (Order_ID,Data_ID,Quantity) values (@Order_ID,@Data_ID,@Quantity)";
                        com = new MySqlCommand(query, dbconnection);
                        com.Parameters.Add("@Order_ID", MySqlDbType.Int16);
                        com.Parameters["@Order_ID"].Value = id;
                        com.Parameters.Add("@Data_ID", MySqlDbType.Int16);
                        com.Parameters["@Data_ID"].Value = row2["Data_ID"].ToString();
                        com.Parameters.Add("@Quantity", MySqlDbType.Double);
                        com.Parameters["@Quantity"].Value = row2["TotalQuantity"].ToString();
                        com.ExecuteNonQuery();
                    }
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
            dbconnection6.Open();

            string query = "select data.Data_ID,data.Code as 'الكود',type.Type_Name as 'النوع',concat(product.Product_Name,' - ',type.Type_Name,' - ',factory.Factory_Name,' - ',groupo.Group_Name,' ',COALESCE(color.Color_Name,''),' ',COALESCE(size.Size_Value,''),' ',COALESCE(sort.Sort_Value,'')) as 'الاسم',sum(storage.Total_Meters) as 'الكمية المتاحة','الحد الادنى',sellprice.Sell_Price as 'السعر',data.Carton as 'الكرتنة' FROM data LEFT JOIN color ON color.Color_ID = data.Color_ID LEFT JOIN size ON size.Size_ID = data.Size_ID LEFT JOIN sort ON sort.Sort_ID = data.Sort_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID INNER JOIN factory ON factory.Factory_ID = data.Factory_ID  INNER JOIN product ON product.Product_ID = data.Product_ID  INNER JOIN type ON type.Type_ID = data.Type_ID  INNER JOIN sellprice ON sellprice.Data_ID = data.Data_ID LEFT JOIN storage ON storage.Data_ID = data.Data_ID where data.Data_ID=0 group by data.Data_ID";
            MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
            DataTable dt = new DataTable();
            da.Fill(dt);
            gridControl1.DataSource = dt;

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
                    string q = "select sellprice.Sell_Price as 'السعر' from data INNER JOIN sellprice ON sellprice.Data_ID = data.Data_ID where data.Data_ID=" + dr["Data_ID"].ToString() + " order by sellprice.Date desc limit 1";
                    MySqlCommand comand2 = new MySqlCommand(q, dbconnection6);
                    MySqlDataReader dr2 = comand2.ExecuteReader();
                    while (dr2.Read())
                    {
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns["السعر"], dr2["السعر"]);
                    }
                    dr2.Close();
                    string q5 = "select Data_ID from storage_least_taswya";
                    q = "SELECT data.Data_ID,SUM(storage.Total_Meters) as 'الكمية المتاحة',least_offer.Least_Quantity as 'الحد الادنى' FROM least_offer INNER JOIN data ON least_offer.Data_ID = data.Data_ID INNER JOIN storage ON storage.Data_ID = data.Data_ID group by data.Data_ID having (SUM(storage.Total_Meters) <= least_offer.Least_Quantity=1) and data.Data_ID not in (" + q5 + ") and data.Data_ID=" + dr["Data_ID"].ToString();
                    comand2 = new MySqlCommand(q, dbconnection6);
                    dr2 = comand2.ExecuteReader();
                    while (dr2.Read())
                    {
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns["الحد الادنى"], dr2["الحد الادنى"]);
                    }
                    dr2.Close();
                }
            }
            dr.Close();
            gridView1.Columns[0].Visible = false;

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
            gridControl2.DataSource = null;
            comStore.SelectedIndex = -1;
            txtStoreID.Text = "";
            comSupplier.SelectedIndex = -1;
            txtSupplier.Text = "";
            txtEmployee.Text = "";
            row1 = null;
            lstrow = new List<DataRow>();
            txtCode.Text = "";
            txtTotalMeters.Text = "";
            clearCom();
            loaded = true;
        }
    }
}

using DevExpress.XtraReports.Design;
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
    public partial class SupplierReceipt : Form
    {
        MySqlConnection conn, conn2, conn6, conn3;
        //int[] courrentIDs;
        //int count = 0;
        int sum = 1;
        bool loaded = false;
        bool factoryFlage = false;
        bool groupFlage = false;
        bool flagProduct = false;
        DataRow row1 = null;
        int storeId = 0;
        bool flag = false;
        bool flagCarton = false;
        int importSupplierPermissionID = 0;
        DevExpress.XtraTab.XtraTabControl xtraTabControlStores = null;
        int flagConfirm = 2;

        public SupplierReceipt(MainForm mainForm, DevExpress.XtraTab.XtraTabControl XtraTabControlStores)
        {
            InitializeComponent();
            //courrentIDs = new int[100];
            conn = new MySqlConnection(connection.connectionString);
            conn2 = new MySqlConnection(connection.connectionString);
            conn6 = new MySqlConnection(connection.connectionString);
            conn3 = new MySqlConnection(connection.connectionString);
            xtraTabControlStores = XtraTabControlStores;
            
            comType.AutoCompleteMode = AutoCompleteMode.Suggest;
            comType.AutoCompleteSource = AutoCompleteSource.ListItems;
            comFactory.AutoCompleteMode = AutoCompleteMode.Suggest;
            comFactory.AutoCompleteSource = AutoCompleteSource.ListItems;
            comOrderFactory.AutoCompleteMode = AutoCompleteMode.Suggest;
            comOrderFactory.AutoCompleteSource = AutoCompleteSource.ListItems;
            comGroup.AutoCompleteMode = AutoCompleteMode.Suggest;
            comGroup.AutoCompleteSource = AutoCompleteSource.ListItems;
            comProduct.AutoCompleteMode = AutoCompleteMode.Suggest;
            comProduct.AutoCompleteSource = AutoCompleteSource.ListItems;
            comSupplier.AutoCompleteMode = AutoCompleteMode.Suggest;
            comSupplier.AutoCompleteSource = AutoCompleteSource.ListItems;
            //comStorePlace.AutoCompleteMode = AutoCompleteMode.Suggest;
            //comStorePlace.AutoCompleteSource = AutoCompleteSource.ListItems;
        }

        private void SupplierReceipt_Load(object sender, EventArgs e)
        {
            try
            {
                //string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"Store.txt");
                //storeId = Convert.ToInt32(System.IO.File.ReadAllText(path));

                string supString = BaseData.StoreID;
                storeId = Convert.ToInt32(supString);

                conn.Open();
                string query = "select * from type";
                MySqlDataAdapter da = new MySqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                comType.DataSource = dt;
                comType.DisplayMember = dt.Columns["Type_Name"].ToString();
                comType.ValueMember = dt.Columns["Type_ID"].ToString();
                comType.Text = "";

                query = "select * from sort";
                da = new MySqlDataAdapter(query, conn);
                dt = new DataTable();
                da.Fill(dt);
                comSort.DataSource = dt;
                comSort.DisplayMember = dt.Columns["Sort_Value"].ToString();
                comSort.ValueMember = dt.Columns["Sort_ID"].ToString();
                comSort.Text = "";

                query = "select * from supplier";
                da = new MySqlDataAdapter(query, conn);
                dt = new DataTable();
                da.Fill(dt);
                comSupplier.DataSource = dt;
                comSupplier.DisplayMember = dt.Columns["Supplier_Name"].ToString();
                comSupplier.ValueMember = dt.Columns["Supplier_ID"].ToString();
                comSupplier.Text = "";
                
                //query = "select * from store_places where Store_ID=" + storeId;
                //da = new MySqlDataAdapter(query, conn);
                //dt = new DataTable();
                //da.Fill(dt);
                //comStorePlace.DataSource = dt;
                //comStorePlace.DisplayMember = dt.Columns["Store_Place_Code"].ToString();
                //comStorePlace.ValueMember = dt.Columns["Store_Place_ID"].ToString();
                //comStorePlace.Text = "";

                query = "select * from factory";
                da = new MySqlDataAdapter(query, conn);
                dt = new DataTable();
                da.Fill(dt);
                comOrderFactory.DataSource = dt;
                comOrderFactory.DisplayMember = dt.Columns["Factory_Name"].ToString();
                comOrderFactory.ValueMember = dt.Columns["Factory_ID"].ToString();
                comOrderFactory.Text = "";

                string q = "select storage_import_permission.Import_Permission_Number from storage_import_permission where storage_import_permission.Store_ID=" + storeId + " ORDER BY storage_import_permission.StorageImportPermission_ID DESC LIMIT 1";
                MySqlCommand com = new MySqlCommand(q, conn);
                if (com.ExecuteScalar() != null)
                {
                    int r = int.Parse(com.ExecuteScalar().ToString());
                    sum = r + 1;
                    txtPermissionNum.Text = sum.ToString();
                }
                else
                {
                    txtPermissionNum.Text = "1";
                }

                loaded = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            conn.Close();
        }
        
        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (comType.Text != "" && comFactory.Text != "")
                {
                    if (comType.Text == "سيراميك" || comType.Text == "بورسلين")
                    {
                        if (comGroup.Text == "")
                        {
                            gridControl1.DataSource = null;
                            MessageBox.Show("يجب اختيار النوع والمصنع والمجموعة على الاقل");
                            return;
                        }
                    }
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

                    conn.Open();
                    conn6.Open();
                    string query = "select data.Data_ID,data.Code as 'الكود','Type',type.Type_Name as 'النوع',concat(product.Product_Name,' - ',type.Type_Name,' - ',factory.Factory_Name,' - ',groupo.Group_Name,' ',COALESCE(color.Color_Name,''),' ',COALESCE(size.Size_Value,''),' ',COALESCE(sort.Sort_Value,'')) as 'الاسم',data.Carton as 'الكرتنة' FROM data LEFT JOIN color ON color.Color_ID = data.Color_ID LEFT JOIN size ON size.Size_ID = data.Size_ID LEFT JOIN sort ON sort.Sort_ID = data.Sort_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID INNER JOIN factory ON factory.Factory_ID = data.Factory_ID  INNER JOIN product ON product.Product_ID = data.Product_ID  INNER JOIN type ON type.Type_ID = data.Type_ID LEFT JOIN storage ON storage.Data_ID = data.Data_ID where  data.Type_ID IN(" + q1 + ") and  data.Factory_ID  IN(" + q2 + ") and data.Group_ID IN (" + q4 + ") and data.Data_ID=0 group by data.Data_ID";
                    MySqlDataAdapter da = new MySqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    gridControl1.DataSource = dt;

                    query = "SELECT data.Data_ID,data.Code as 'الكود',type.Type_Name as 'النوع',concat(product.Product_Name,' ',COALESCE(color.Color_Name,''),' ',data.Description,' ',groupo.Group_Name,' ',factory.Factory_Name,' ',COALESCE(size.Size_Value,''),' ',COALESCE(sort.Sort_Value,'')) as 'الاسم',data.Carton as 'الكرتنة',sum(storage.Total_Meters) as 'الكمية' FROM data LEFT JOIN color ON color.Color_ID = data.Color_ID LEFT JOIN size ON size.Size_ID = data.Size_ID LEFT JOIN sort ON sort.Sort_ID = data.Sort_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID INNER JOIN factory ON factory.Factory_ID = data.Factory_ID  INNER JOIN product ON product.Product_ID = data.Product_ID  INNER JOIN type ON type.Type_ID = data.Type_ID  LEFT JOIN storage ON storage.Data_ID = data.Data_ID where data.Type_ID IN(" + q1 + ") and data.Factory_ID IN(" + q2 + ") and data.Product_ID IN (" + q3 + ") and data.Group_ID IN (" + q4 + ") " + fQuery + " group by data.Data_ID order by SUBSTR(data.Code,1,16),color.Color_Name,data.Description,data.Sort_ID";
                    MySqlCommand comand = new MySqlCommand(query, conn);
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
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns["Type"], "بند");
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns["الكرتنة"], dr["الكرتنة"]);
                        }
                    }
                    dr.Close();
                    if (gridView1.IsLastVisibleRow)
                    {
                        gridView1.FocusedRowHandle = gridView1.RowCount - 1;
                    }
                    gridView1.Columns[0].Visible = false;
                    gridView1.Columns["Type"].Visible = false;
                }
                else
                {
                    gridControl1.DataSource = null;
                    MessageBox.Show("يجب اختيار النوع والمصنع على الاقل");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            conn.Close();
            conn6.Close();
        }

        private void comBox_SelectedValueChanged(object sender, EventArgs e)
        {
            if (loaded)
            {
                ComboBox comBox = (ComboBox)sender;

                switch (comBox.Name)
                {
                    case "comType":
                        if (loaded)
                        {
                            string query = "select * from factory inner join type_factory on factory.Factory_ID=type_factory.Factory_ID inner join type on type_factory.Type_ID=type.Type_ID where type_factory.Type_ID=" + comType.SelectedValue;
                            MySqlDataAdapter da = new MySqlDataAdapter(query, conn);
                            DataTable dt = new DataTable();
                            da.Fill(dt);
                            comFactory.DataSource = dt;
                            comFactory.DisplayMember = dt.Columns["Factory_Name"].ToString();
                            comFactory.ValueMember = dt.Columns["Factory_ID"].ToString();
                            comFactory.Text = "";
                            conn.Close();
                            conn.Open();
                            query = "select TypeCoding_Method from type where Type_ID=" + comType.SelectedValue;
                            MySqlCommand com = new MySqlCommand(query, conn);
                            int TypeCoding_Method = (int)com.ExecuteScalar();
                            conn.Close();
                            if (TypeCoding_Method == 1)
                            {
                                string query2 = "";
                                if (comType.SelectedValue.ToString() == "2" || comType.SelectedValue.ToString() == "1")
                                {
                                    query2 = "select * from groupo where Factory_ID=-1";
                                }
                                else
                                {
                                    query2 = "select * from groupo where Factory_ID=" + -Convert.ToInt32(comType.SelectedValue) + " and Type_ID=" + comType.SelectedValue;
                                }

                                MySqlDataAdapter da2 = new MySqlDataAdapter(query2, conn);
                                DataTable dt2 = new DataTable();
                                da2.Fill(dt2);
                                comGroup.DataSource = dt2;
                                comGroup.DisplayMember = dt2.Columns["Group_Name"].ToString();
                                comGroup.ValueMember = dt2.Columns["Group_ID"].ToString();
                                comGroup.Text = "";
                                groupFlage = true;
                            }
                            factoryFlage = true;

                            query = "select * from color where Type_ID=" + comType.SelectedValue;
                            da = new MySqlDataAdapter(query, conn);
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
                            conn.Close();
                            conn.Open();
                            string query = "select TypeCoding_Method from type where Type_ID=" + comType.SelectedValue.ToString();
                            MySqlCommand com = new MySqlCommand(query, conn);
                            int TypeCoding_Method = (int)com.ExecuteScalar();
                            conn.Close();
                            if (TypeCoding_Method == 2)
                            {
                                string query2f = "select * from groupo where Type_ID=" + comType.SelectedValue.ToString() + " and Factory_ID=" + comFactory.SelectedValue.ToString();
                                MySqlDataAdapter da2f = new MySqlDataAdapter(query2f, conn);
                                DataTable dt2f = new DataTable();
                                da2f.Fill(dt2f);
                                comGroup.DataSource = dt2f;
                                comGroup.DisplayMember = dt2f.Columns["Group_Name"].ToString();
                                comGroup.ValueMember = dt2f.Columns["Group_ID"].ToString();
                                comGroup.Text = "";
                            }
                            else
                            {
                                filterProduct();
                            }

                            groupFlage = true;

                            string query2 = "select * from size where Factory_ID=" + comFactory.SelectedValue.ToString();
                            MySqlDataAdapter da2 = new MySqlDataAdapter(query2, conn);
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
                            if (comType.Text != "")
                            {
                                supQuery += " and product.Type_ID=" + comType.SelectedValue.ToString();
                            }
                            if (comFactory.Text != "")
                            {
                                supQuery += " and product_factory_group.Factory_ID=" + comFactory.SelectedValue.ToString();
                                subQuery1 += " and Factory_ID=" + comFactory.SelectedValue.ToString();
                            }
                            string query3 = "select distinct product.Product_ID ,Product_Name  from product inner join product_factory_group on product.Product_ID=product_factory_group.Product_ID  where product_factory_group.Group_ID=" + comGroup.SelectedValue.ToString() + supQuery + "  order by product.Product_ID";
                            MySqlDataAdapter da3 = new MySqlDataAdapter(query3, conn);
                            DataTable dt3 = new DataTable();
                            da3.Fill(dt3);
                            comProduct.DataSource = dt3;
                            comProduct.DisplayMember = dt3.Columns["Product_Name"].ToString();
                            comProduct.ValueMember = dt3.Columns["Product_ID"].ToString();
                            comProduct.Text = "";

                            string query2 = "select * from size where Group_ID=" + comGroup.SelectedValue.ToString() + subQuery1;
                            MySqlDataAdapter da2 = new MySqlDataAdapter(query2, conn);
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
                }
            }
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
                string v = row1["الكود"].ToString();
                txtCode.Text = v;
                loaded = false;
                txtSupPermissionNum.Text = "";
                txtOrderNum.Text = "";
                //comOrderFactory.SelectedIndex = -1;
                //txtOrderFactory.Text = "";
                txtDescription.Text = "";
                //comStorePlace.SelectedIndex = -1;
                txtTotalMeter.Text = "0";
                txtCarton.Text = "0";
                txtBalat.Text = "0";
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
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            conn.Close();
        }

        private void txtNumCarton_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (row1 != null && !flagCarton)
                {
                    int NoCartons = 0;
                    double totalMeter = 0;
                    double carton = double.Parse(row1["الكرتنة"].ToString());
                    if (carton > 0)
                    {
                        if (int.TryParse(txtCarton.Text, out NoCartons))
                        { }
                        if (double.TryParse(txtTotalMeter.Text, out totalMeter))
                        { }
                        
                        double total = carton * NoCartons;
                        flag = true;
                        txtTotalMeter.Text = (total).ToString();
                        flag = false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            conn.Close();
        }

        private void txtTotalMeter_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (row1 != null && !flag)
                {
                    double totalMeter = 0;
                    if (double.TryParse(txtTotalMeter.Text, out totalMeter))
                    {
                        double carton = double.Parse(row1["الكرتنة"].ToString());
                        if (carton > 0)
                        {
                            flagCarton = true;
                            double total = totalMeter / carton;
                            txtCarton.Text = Convert.ToInt32(total).ToString();
                            flagCarton = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            conn.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (flagConfirm == 2)
                {
                    if (row1 != null && comSupplier.SelectedValue != null && txtPermissionNum.Text != "" && txtSupPermissionNum.Text != "" && txtCarton.Text != "" && txtBalat.Text != "" /*&& comStorePlace.Text != ""*/ && txtCode.Text != "" && txtTotalMeter.Text != "")
                    {
                        int NoBalatat = 0;
                        int NoCartons = 0;
                        int permNum = 0;
                        int supPermNum = 0;
                        int orderNum = 0;
                        double total = 0;
                        if(txtOrderNum.Text != "" && comOrderFactory.SelectedValue != null)
                        {
                            if(int.TryParse(txtOrderNum.Text, out orderNum))
                            {
                            }
                        }
                        if (int.TryParse(txtBalat.Text, out NoBalatat) && int.TryParse(txtCarton.Text, out NoCartons) && int.TryParse(txtPermissionNum.Text, out permNum) && int.TryParse(txtSupPermissionNum.Text, out supPermNum) && double.TryParse(txtTotalMeter.Text, out total))
                        {
                            double carton = Convert.ToDouble(row1["الكرتنة"].ToString());

                            //double total = carton * NoCartons;

                            if (int.Parse(txtPermissionNum.Text) <= sum)
                            {
                                /*if (IsAdded())
                                {
                                    MessageBox.Show("هذا العنصر تم اضافتة من قبل");
                                    conn.Close();
                                    conn2.Close();
                                    return;
                                }*/

                                conn.Open();
                                conn2.Open();
                                if (txtOrderNum.Text != "" && comOrderFactory.SelectedValue != null)
                                {
                                    string query2 = "select order_details.OrderDetails_ID,order_details.Quantity from orders INNER JOIN order_details ON order_details.Order_ID = orders.Order_ID where order_details.Data_ID=" + row1["Data_ID"].ToString() + " and orders.Factory_ID=" + comOrderFactory.SelectedValue.ToString() + " and orders.Order_Number=" + orderNum;
                                    MySqlCommand com2 = new MySqlCommand(query2, conn);
                                    MySqlDataReader dr = com2.ExecuteReader();
                                    if (dr.HasRows)
                                    {
                                        while (dr.Read())
                                        {
                                            double orderQuantity = Convert.ToDouble(dr["Quantity"].ToString());
                                            if (orderQuantity == total)
                                            {
                                                query2 = "update order_details set Received=1 where OrderDetails_ID=" + dr["OrderDetails_ID"].ToString();
                                            }
                                            else if ((orderQuantity - total) > 0)
                                            {
                                                double orderTotal = 0;
                                                //INNER JOIN store_places ON store_places.Store_Place_ID = supplier_permission_details.Store_Place_ID
                                                //order by SUBSTR(data.Code,1,16),color.Color_Name,data.Description,data.Sort_ID
                                                string qq = "select sum(supplier_permission_details.Total_Meters) as 'Total_Meters' from supplier_permission_details INNER JOIN data ON supplier_permission_details.Data_ID = data.Data_ID INNER JOIN import_supplier_permission ON supplier_permission_details.ImportSupplierPermission_ID = import_supplier_permission.ImportSupplierPermission_ID INNER JOIN storage_import_permission ON storage_import_permission.StorageImportPermission_ID = import_supplier_permission.StorageImportPermission_ID  INNER JOIN supplier ON import_supplier_permission.Supplier_ID = supplier.Supplier_ID inner JOIN order_details ON supplier_permission_details.Data_ID = order_details.Data_ID inner JOIN orders ON order_details.Order_ID = orders.Order_ID INNER JOIN type ON type.Type_ID = data.Type_ID INNER JOIN product ON product.Product_ID = data.Product_ID INNER JOIN factory ON data.Factory_ID = factory.Factory_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID LEFT JOIN color ON color.Color_ID = data.Color_ID LEFT JOIN size ON size.Size_ID = data.Size_ID LEFT JOIN sort ON sort.Sort_ID = data.Sort_ID where data.Data_ID=" + row1["Data_ID"].ToString() + " and orders.Order_Number=" + orderNum + " and orders.Factory_ID=" + comOrderFactory.SelectedValue.ToString() + " ";
                                                MySqlCommand com3 = new MySqlCommand(qq, conn2);
                                                MySqlDataReader dr2 = com3.ExecuteReader();
                                                if (dr2.HasRows)
                                                {
                                                    while (dr2.Read())
                                                    {
                                                        double permissionTotal = 0;
                                                        if (dr2["Total_Meters"].ToString() != "")
                                                        {
                                                            if (Convert.ToDouble(dr2["Total_Meters"].ToString()) > 0)
                                                            {
                                                                permissionTotal = Convert.ToDouble(dr2["Total_Meters"].ToString());
                                                            }
                                                        }

                                                        orderTotal = total + permissionTotal;
                                                        if (orderTotal == orderQuantity)
                                                        {
                                                            query2 = "update order_details set Received=1 where OrderDetails_ID=" + dr["OrderDetails_ID"].ToString();
                                                        }
                                                        else if (orderTotal < orderQuantity)
                                                        {
                                                            query2 = "update order_details set Received=2 where OrderDetails_ID=" + dr["OrderDetails_ID"].ToString();
                                                        }
                                                    }
                                                }
                                                dr2.Close();

                                                //query2 = "update order_details set Received=2 where OrderDetails_ID=" + dr["OrderDetails_ID"].ToString();
                                            }

                                            com2 = new MySqlCommand(query2, conn2);
                                            com2.ExecuteNonQuery();
                                        }
                                    }
                                    else
                                    {
                                        if (MessageBox.Show("يوجد خطا فى الطلب هل تريد الاستمرار؟", "", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                                        {
                                            conn.Close();
                                            conn2.Close();
                                            return;
                                        }
                                    }
                                    dr.Close();
                                }
                                /*query = "update orders set Received=1 where Factory_ID=" + comOrderFactory.SelectedValue.ToString() + " and Order_Number=" + orderNum;
                                com = new MySqlCommand(query, conn);
                                try
                                {
                                    if (com.ExecuteNonQuery() == 1)
                                    { }
                                    else
                                    {
                                        if (MessageBox.Show("يوجد خطا فى الطلب هل تريد الاستمرار؟", "", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                                            return;
                                    }
                                }
                                catch
                                {
                                    if (MessageBox.Show("يوجد خطا فى الطلب هل تريد الاستمرار؟", "", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                                        return;
                                }*/

                                int storageImportPermissionID = 0;
                                string query = "select StorageImportPermission_ID from storage_import_permission where Import_Permission_Number=" + permNum + " and Store_ID=" + storeId;
                                MySqlCommand com = new MySqlCommand(query, conn);
                                if (com.ExecuteScalar() == null)
                                {
                                    query = "insert into storage_import_permission (Store_ID,Storage_Date,Import_Permission_Number) values (@Store_ID,@Storage_Date,@Import_Permission_Number)";
                                    com = new MySqlCommand(query, conn);
                                    com.Parameters.Add("@Store_ID", MySqlDbType.Int16);
                                    com.Parameters["@Store_ID"].Value = storeId;
                                    com.Parameters.Add("@Storage_Date", MySqlDbType.DateTime, 0);
                                    com.Parameters["@Storage_Date"].Value = DateTime.Now;
                                    com.Parameters.Add("@Import_Permission_Number", MySqlDbType.Int16);
                                    com.Parameters["@Import_Permission_Number"].Value = permNum;
                                    com.ExecuteNonQuery();

                                    query = "select StorageImportPermission_ID from storage_import_permission order by StorageImportPermission_ID desc limit 1";
                                    com = new MySqlCommand(query, conn);
                                    storageImportPermissionID = Convert.ToInt32(com.ExecuteScalar().ToString());

                                    UserControl.ItemRecord("storage_import_permission", "اضافة", storageImportPermissionID, DateTime.Now, "", conn);

                                    query = "SELECT gate.Car_ID,gate.Car_Number,gate.Driver_ID,gate.Driver_Name FROM gate INNER JOIN gate_supplier ON gate.Gate_ID = gate_supplier.Gate_ID INNER JOIN gate_permission ON gate_supplier.GateSupplier_ID = gate_permission.GateSupplier_ID where gate.Store_ID=" + storeId + " and gate_supplier.Supplier_ID=" + comSupplier.SelectedValue.ToString() + " and gate_permission.Supplier_PermissionNumber=" + supPermNum + " and gate_permission.Type='دخول'";
                                    com = new MySqlCommand(query, conn2);
                                    MySqlDataReader dr2 = com.ExecuteReader();
                                    if (dr2.HasRows)
                                    {
                                        while (dr2.Read())
                                        {
                                            query = "insert into import_supplier_permission (Supplier_ID,Supplier_Permission_Number,Factory_ID,Order_Number,Car_ID,Car_Number,Driver_ID,Driver_Name,StorageImportPermission_ID) values (@Supplier_ID,@Supplier_Permission_Number,@Factory_ID,@Order_Number,@Car_ID,@Car_Number,@Driver_ID,@Driver_Name,@StorageImportPermission_ID)";
                                            com = new MySqlCommand(query, conn);
                                            com.Parameters.Add("@Supplier_ID", MySqlDbType.Int16);
                                            com.Parameters["@Supplier_ID"].Value = Convert.ToInt32(comSupplier.SelectedValue.ToString());
                                            com.Parameters.Add("@Supplier_Permission_Number", MySqlDbType.Int16);
                                            com.Parameters["@Supplier_Permission_Number"].Value = supPermNum;
                                            if (orderNum > 0 && comOrderFactory.SelectedValue != null)
                                            {
                                                com.Parameters.Add("@Order_Number", MySqlDbType.Int16);
                                                com.Parameters["@Order_Number"].Value = orderNum;
                                                com.Parameters.Add("@Factory_ID", MySqlDbType.Int16);
                                                com.Parameters["@Factory_ID"].Value = comOrderFactory.SelectedValue.ToString();
                                            }
                                            else
                                            {
                                                com.Parameters.Add("@Order_Number", MySqlDbType.Int16);
                                                com.Parameters["@Order_Number"].Value = null;
                                                com.Parameters.Add("@Factory_ID", MySqlDbType.Int16);
                                                com.Parameters["@Factory_ID"].Value = null;
                                            }
                                            if (dr2["Car_ID"].ToString() != "")
                                            {
                                                com.Parameters.Add("@Car_ID", MySqlDbType.Int16);
                                                com.Parameters["@Car_ID"].Value = Convert.ToInt32(dr2["Car_ID"].ToString());
                                            }
                                            else
                                            {
                                                com.Parameters.Add("@Car_ID", MySqlDbType.Int16);
                                                com.Parameters["@Car_ID"].Value = null;
                                            }
                                            com.Parameters.Add("@Car_Number", MySqlDbType.VarChar);
                                            com.Parameters["@Car_Number"].Value = dr2["Car_Number"].ToString();
                                            if (dr2["Driver_ID"].ToString() != "")
                                            {
                                                com.Parameters.Add("@Driver_ID", MySqlDbType.Int16);
                                                com.Parameters["@Driver_ID"].Value = Convert.ToInt32(dr2["Driver_ID"].ToString());
                                            }
                                            else
                                            {
                                                com.Parameters.Add("@Driver_ID", MySqlDbType.Int16);
                                                com.Parameters["@Driver_ID"].Value = null;
                                            }
                                            com.Parameters.Add("@Driver_Name", MySqlDbType.VarChar);
                                            com.Parameters["@Driver_Name"].Value = dr2["Driver_Name"].ToString();
                                            com.Parameters.Add("@StorageImportPermission_ID", MySqlDbType.Int16);
                                            com.Parameters["@StorageImportPermission_ID"].Value = storageImportPermissionID;
                                            com.ExecuteNonQuery();

                                            query = "select ImportSupplierPermission_ID from import_supplier_permission order by ImportSupplierPermission_ID desc limit 1";
                                            com = new MySqlCommand(query, conn);
                                            importSupplierPermissionID = Convert.ToInt32(com.ExecuteScalar().ToString());
                                        }
                                        dr2.Close();
                                    }
                                    else
                                    {
                                        importSupplierPermissionID = 0;
                                    }
                                }
                                else
                                {
                                    storageImportPermissionID = Convert.ToInt32(com.ExecuteScalar().ToString());
                                    query = "select ImportSupplierPermission_ID from import_supplier_permission where StorageImportPermission_ID=" + storageImportPermissionID + " and Supplier_ID=" + comSupplier.SelectedValue.ToString() + " and Supplier_Permission_Number=" + supPermNum;
                                    com = new MySqlCommand(query, conn);
                                    if (com.ExecuteScalar() == null)
                                    {
                                        query = "SELECT gate.Car_ID,gate.Car_Number,gate.Driver_ID,gate.Driver_Name FROM gate INNER JOIN gate_supplier ON gate.Gate_ID = gate_supplier.Gate_ID INNER JOIN gate_permission ON gate_supplier.GateSupplier_ID = gate_permission.GateSupplier_ID where gate.Store_ID=" + storeId + " and gate_supplier.Supplier_ID=" + comSupplier.SelectedValue.ToString() + " and gate_permission.Supplier_PermissionNumber=" + supPermNum + " and gate_permission.Type='دخول'";
                                        com = new MySqlCommand(query, conn2);
                                        MySqlDataReader dr2 = com.ExecuteReader();
                                        if (dr2.HasRows)
                                        {
                                            while (dr2.Read())
                                            {
                                                query = "insert into import_supplier_permission (Supplier_ID,Supplier_Permission_Number,Factory_ID,Order_Number,Car_ID,Car_Number,Driver_ID,Driver_Name,StorageImportPermission_ID) values (@Supplier_ID,@Supplier_Permission_Number,@Factory_ID,@Order_Number,@Car_ID,@Car_Number,@Driver_ID,@Driver_Name,@StorageImportPermission_ID)";
                                                com = new MySqlCommand(query, conn);
                                                com.Parameters.Add("@Supplier_ID", MySqlDbType.Int16);
                                                com.Parameters["@Supplier_ID"].Value = comSupplier.SelectedValue.ToString();
                                                com.Parameters.Add("@Supplier_Permission_Number", MySqlDbType.Int16);
                                                com.Parameters["@Supplier_Permission_Number"].Value = supPermNum;
                                                if (orderNum > 0 && comOrderFactory.SelectedValue != null)
                                                {
                                                    com.Parameters.Add("@Order_Number", MySqlDbType.Int16);
                                                    com.Parameters["@Order_Number"].Value = orderNum;
                                                    com.Parameters.Add("@Factory_ID", MySqlDbType.Int16);
                                                    com.Parameters["@Factory_ID"].Value = comOrderFactory.SelectedValue.ToString();
                                                }
                                                else
                                                {
                                                    com.Parameters.Add("@Order_Number", MySqlDbType.Int16);
                                                    com.Parameters["@Order_Number"].Value = null;
                                                    com.Parameters.Add("@Factory_ID", MySqlDbType.Int16);
                                                    com.Parameters["@Factory_ID"].Value = null;
                                                }
                                                if (dr2["Car_ID"].ToString() != "")
                                                {
                                                    com.Parameters.Add("@Car_ID", MySqlDbType.Int16);
                                                    com.Parameters["@Car_ID"].Value = Convert.ToInt32(dr2["Car_ID"].ToString());
                                                }
                                                else
                                                {
                                                    com.Parameters.Add("@Car_ID", MySqlDbType.Int16);
                                                    com.Parameters["@Car_ID"].Value = null;
                                                }
                                                com.Parameters.Add("@Car_Number", MySqlDbType.VarChar);
                                                com.Parameters["@Car_Number"].Value = dr2["Car_Number"].ToString();
                                                if (dr2["Driver_ID"].ToString() != "")
                                                {
                                                    com.Parameters.Add("@Driver_ID", MySqlDbType.Int16);
                                                    com.Parameters["@Driver_ID"].Value = Convert.ToInt32(dr2["Driver_ID"].ToString());
                                                }
                                                else
                                                {
                                                    com.Parameters.Add("@Driver_ID", MySqlDbType.Int16);
                                                    com.Parameters["@Driver_ID"].Value = null;
                                                }
                                                com.Parameters.Add("@Driver_Name", MySqlDbType.VarChar);
                                                com.Parameters["@Driver_Name"].Value = dr2["Driver_Name"].ToString();
                                                com.Parameters.Add("@StorageImportPermission_ID", MySqlDbType.Int16);
                                                com.Parameters["@StorageImportPermission_ID"].Value = storageImportPermissionID;
                                                com.ExecuteNonQuery();

                                                query = "select ImportSupplierPermission_ID from import_supplier_permission order by ImportSupplierPermission_ID desc limit 1";
                                                com = new MySqlCommand(query, conn);
                                                importSupplierPermissionID = Convert.ToInt32(com.ExecuteScalar().ToString());
                                            }
                                            dr2.Close();
                                        }
                                        else
                                        {
                                            importSupplierPermissionID = 0;
                                        }
                                    }
                                    else
                                    {
                                        importSupplierPermissionID = Convert.ToInt32(com.ExecuteScalar().ToString());
                                    }
                                }

                                if (importSupplierPermissionID > 0)
                                {
                                    query = "insert into supplier_permission_details (Store_ID,Date,Data_ID,Balatat,Carton_Balata,Total_Meters,Note,ImportSupplierPermission_ID,Employee_ID) values (@Store_ID,@Date,@Data_ID,@Balatat,@Carton_Balata,@Total_Meters,@Note,@ImportSupplierPermission_ID,@Employee_ID)";
                                    com = new MySqlCommand(query, conn);
                                    com.Parameters.Add("@Store_ID", MySqlDbType.Int16);
                                    com.Parameters["@Store_ID"].Value = storeId;
                                    //com.Parameters.Add("@Store_Place_ID", MySqlDbType.Int16);
                                    //com.Parameters["@Store_Place_ID"].Value = comStorePlace.SelectedValue.ToString();
                                    com.Parameters.Add("@Date", MySqlDbType.DateTime, 0);
                                    com.Parameters["@Date"].Value = DateTime.Now;
                                    if (carton > 0)
                                    {
                                        com.Parameters.Add("@Balatat", MySqlDbType.Int16);
                                        com.Parameters["@Balatat"].Value = NoBalatat;
                                        com.Parameters.Add("@Carton_Balata", MySqlDbType.Int16);
                                        com.Parameters["@Carton_Balata"].Value = NoCartons;
                                    }
                                    else
                                    {
                                        com.Parameters.Add("@Balatat", MySqlDbType.Int16);
                                        com.Parameters["@Balatat"].Value = null;
                                        com.Parameters.Add("@Carton_Balata", MySqlDbType.Int16);
                                        com.Parameters["@Carton_Balata"].Value = null;
                                    }
                                    com.Parameters.Add("@Data_ID", MySqlDbType.Int16);
                                    com.Parameters["@Data_ID"].Value = row1[0].ToString();
                                    com.Parameters.Add("@Total_Meters", MySqlDbType.Decimal);
                                    com.Parameters["@Total_Meters"].Value = total;
                                    com.Parameters.Add("@Note", MySqlDbType.VarChar);
                                    com.Parameters["@Note"].Value = txtDescription.Text;
                                    com.Parameters.Add("@ImportSupplierPermission_ID", MySqlDbType.Int16);
                                    com.Parameters["@ImportSupplierPermission_ID"].Value = importSupplierPermissionID;
                                    com.Parameters.Add("@Employee_ID", MySqlDbType.Int16);
                                    com.Parameters["@Employee_ID"].Value = UserControl.EmpID;
                                    com.ExecuteNonQuery();
                                }
                                else
                                {
                                    MessageBox.Show("برجاء التاكد من رقم اذن الاستلام");
                                    conn.Close();
                                    conn2.Close();
                                    return;
                                }

                                query = "select OpenStorageAccount_ID from open_storage_account where Data_ID=" + row1["Data_ID"].ToString() + " and Store_ID=" + storeId;
                                com = new MySqlCommand(query, conn);
                                if (com.ExecuteScalar() == null)
                                {
                                    query = "insert into open_storage_account (Data_ID,Quantity,Store_ID,Date) values (@Data_ID,@Quantity,@Store_ID,@Date)";
                                    com = new MySqlCommand(query, conn);
                                    com.Parameters.Add("@Data_ID", MySqlDbType.Int16);
                                    com.Parameters["@Data_ID"].Value = row1["Data_ID"].ToString();
                                    com.Parameters.Add("@Quantity", MySqlDbType.Decimal);
                                    com.Parameters["@Quantity"].Value = 0;
                                    com.Parameters.Add("@Store_ID", MySqlDbType.Int16);
                                    com.Parameters["@Store_ID"].Value = storeId;
                                    com.Parameters.Add("@Date", MySqlDbType.Date, 0);
                                    DateTime date = DateTime.Now;
                                    string d = date.ToString("yyyy-MM-dd");
                                    com.Parameters["@Date"].Value = d;
                                    com.ExecuteNonQuery();

                                    UserControl.ItemRecord("open_storage_account", "اضافة", Convert.ToInt32(row1["Data_ID"].ToString()), DateTime.Now, "", conn);
                                }

                                query = "select Total_Meters from storage where Data_ID=" + row1["Data_ID"].ToString() + " and Store_ID=" + storeId; //+ " and Store_Place_ID=" + comStorePlace.SelectedValue.ToString();
                                com = new MySqlCommand(query, conn);
                                if (com.ExecuteScalar() != null)
                                {
                                    double totalMeter = Convert.ToDouble(com.ExecuteScalar().ToString());

                                    query = "update storage set Total_Meters=" + (totalMeter + total) + " where Data_ID=" + row1["Data_ID"].ToString() + " and Store_ID=" + storeId; //+ " and Store_Place_ID=" + comStorePlace.SelectedValue.ToString();
                                    com = new MySqlCommand(query, conn);
                                    com.ExecuteNonQuery();
                                }
                                else
                                {
                                    query = "insert into Storage (Store_ID,Type,Data_ID,Total_Meters) values (@Store_ID,@Type,@Data_ID,@Total_Meters)";
                                    com = new MySqlCommand(query, conn);
                                    com.Parameters.Add("@Store_ID", MySqlDbType.Int16);
                                    com.Parameters["@Store_ID"].Value = storeId;
                                    //com.Parameters.Add("@Store_Place_ID", MySqlDbType.Int16);
                                    //com.Parameters["@Store_Place_ID"].Value = comStorePlace.SelectedValue.ToString();
                                    com.Parameters.Add("@Type", MySqlDbType.VarChar);
                                    com.Parameters["@Type"].Value = "بند";
                                    com.Parameters.Add("@Data_ID", MySqlDbType.Int16);
                                    com.Parameters["@Data_ID"].Value = row1[0].ToString();
                                    com.Parameters.Add("@Total_Meters", MySqlDbType.Decimal);
                                    com.Parameters["@Total_Meters"].Value = total;
                                    com.ExecuteNonQuery();
                                }
                                
                                search();

                                loaded = false;
                                txtCode.Text = "";
                                txtSupPermissionNum.Text = "";
                                txtOrderNum.Text = "";
                                comOrderFactory.SelectedIndex = -1;
                                txtOrderFactory.Text = "";
                                txtTotalMeter.Text = "0";
                                txtCarton.Text = "0";
                                txtBalat.Text = "0";
                                txtDescription.Text = "";
                                //comStorePlace.SelectedIndex = -1;
                                loaded = true;
                            }
                            else
                            {
                                MessageBox.Show("يجب ادخال رقم اذن اقل من " + sum);
                            }
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
                else
                {
                    MessageBox.Show("هذا الاذن منتهى");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            conn.Close();
            conn2.Close();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (flagConfirm == 2)
                {
                    DataRow row2 = gridView2.GetDataRow(gridView2.FocusedRowHandle);
                    if (row2 != null)
                    {
                        if (MessageBox.Show("هل انت متاكد انك تريد الحذف؟", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            conn.Open();

                            string query = "select Total_Meters from storage where Store_ID=" + storeId /*+ " and Store_Place_ID=" + row2["Store_Place_ID"].ToString()*/ + " and Data_ID=" + row2["Data_ID"].ToString();
                            MySqlCommand com = new MySqlCommand(query, conn);
                            if (com.ExecuteScalar() != null)
                            {
                                double totalf = Convert.ToInt32(com.ExecuteScalar());
                                if ((totalf - Convert.ToDouble(row2["متر/قطعة"].ToString())) >= 0)
                                {
                                    //Store_ID=" + storeId + " and Store_Place_ID=" + row2["Store_Place_ID"].ToString() + " and  Data_ID=" + row2["Data_ID"].ToString()
                                    query = "delete from supplier_permission_details where Supplier_Permission_Details_ID=" + row2["Supplier_Permission_Details_ID"].ToString();
                                    com = new MySqlCommand(query, conn);
                                    com.ExecuteNonQuery();

                                    query = "update storage set Total_Meters=" + (totalf - Convert.ToDouble(row2["متر/قطعة"].ToString())) + " where Store_ID=" + storeId /*+ " and Store_Place_ID=" + row2["Store_Place_ID"].ToString()*/ + " and Data_ID=" + row2["Data_ID"].ToString();
                                    com = new MySqlCommand(query, conn);
                                    com.ExecuteNonQuery();

                                    #region MyRegion
                                    if (row2["Factory_ID"].ToString() != "" && row2["اذن الطلب"].ToString() != "")
                                    {
                                        conn2.Open();
                                        query = "select order_details.OrderDetails_ID,order_details.Quantity from orders INNER JOIN order_details ON order_details.Order_ID = orders.Order_ID where order_details.Data_ID=" + row2["Data_ID"].ToString() + " and orders.Factory_ID=" + row2["Factory_ID"].ToString() + " and orders.Order_Number=" + row2["اذن الطلب"].ToString();
                                        com = new MySqlCommand(query, conn);
                                        MySqlDataReader dr = com.ExecuteReader();
                                        if (dr.HasRows)
                                        {
                                            while (dr.Read())
                                            {
                                                double orderQuantity = Convert.ToDouble(dr["Quantity"].ToString());
                                                if (orderQuantity == Convert.ToDouble(row2["متر/قطعة"].ToString()))
                                                {
                                                    query = "update order_details set Received=0 where OrderDetails_ID=" + dr["OrderDetails_ID"].ToString();
                                                }
                                                else
                                                {
                                                    //INNER JOIN store_places ON store_places.Store_Place_ID = supplier_permission_details.Store_Place_ID
                                                    //order by SUBSTR(data.Code,1,16),color.Color_Name,data.Description,data.Sort_ID
                                                    query = "select sum(supplier_permission_details.Total_Meters) as 'Total_Meters' from supplier_permission_details INNER JOIN data ON supplier_permission_details.Data_ID = data.Data_ID INNER JOIN import_supplier_permission ON supplier_permission_details.ImportSupplierPermission_ID = import_supplier_permission.ImportSupplierPermission_ID INNER JOIN storage_import_permission ON storage_import_permission.StorageImportPermission_ID = import_supplier_permission.StorageImportPermission_ID  INNER JOIN supplier ON import_supplier_permission.Supplier_ID = supplier.Supplier_ID inner JOIN order_details ON supplier_permission_details.Data_ID = order_details.Data_ID inner JOIN orders ON order_details.Order_ID = orders.Order_ID INNER JOIN type ON type.Type_ID = data.Type_ID INNER JOIN product ON product.Product_ID = data.Product_ID INNER JOIN factory ON data.Factory_ID = factory.Factory_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID LEFT JOIN color ON color.Color_ID = data.Color_ID LEFT JOIN size ON size.Size_ID = data.Size_ID LEFT JOIN sort ON sort.Sort_ID = data.Sort_ID where data.Data_ID=" + row1["Data_ID"].ToString() + " and orders.Order_Number=" + row2["اذن الطلب"].ToString() + " and orders.Factory_ID=" + row2["Factory_ID"].ToString() + " ";
                                                    //query = "select sum(order_details.Quantity) as 'Quantity' from orders INNER JOIN order_details ON order_details.Order_ID = orders.Order_ID where order_details.Data_ID=" + row2["Data_ID"].ToString() + " and orders.Received<>1 and (orders.Factory_ID<>" + row2["Factory_ID"].ToString() + " and orders.Order_Number<>" + row2["Order_Number"].ToString() + ")";
                                                    com = new MySqlCommand(query, conn2);
                                                    MySqlDataReader dr2 = com.ExecuteReader();
                                                    if (dr2.HasRows)
                                                    {
                                                        while (dr2.Read())
                                                        {
                                                            if (dr2["Total_Meters"].ToString() != "")
                                                            {
                                                                if (Convert.ToDouble(dr2["Total_Meters"].ToString()) > 0)
                                                                {
                                                                    query = "update order_details set Received=2 where OrderDetails_ID=" + dr["OrderDetails_ID"].ToString();
                                                                }
                                                                else
                                                                {
                                                                    query = "update order_details set Received=0 where OrderDetails_ID=" + dr["OrderDetails_ID"].ToString();
                                                                }
                                                            }
                                                            else
                                                            {
                                                                query = "update order_details set Received=0 where OrderDetails_ID=" + dr["OrderDetails_ID"].ToString();
                                                            }
                                                        }
                                                    }
                                                    //else
                                                    //{
                                                    //    query = "update order_details set Received=0 where OrderDetails_ID=" + dr["OrderDetails_ID"].ToString();
                                                    //}
                                                    dr2.Close();
                                                }

                                                com = new MySqlCommand(query, conn2);
                                                com.ExecuteNonQuery();
                                            }
                                        }
                                        dr.Close();
                                    }
                                    #endregion

                                    search();
                                }
                                else
                                {
                                    MessageBox.Show("لا يوجد كمية كافية");
                                }
                            }
                            else
                            {
                                MessageBox.Show("لا يوجد كمية كافية");
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("يجب اختيار عنصر للحذف");
                    }
                }
                else
                {
                    MessageBox.Show("هذا الاذن منتهى");
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            conn.Close();
            conn2.Close();
        }

        private void btnCodingProduct_Click(object sender, EventArgs e)
        {
            try
            {
                Product_Record form = new Product_Record(null, xtraTabControlStores);
                form.ShowDialog();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            conn.Close();
        }

        private void txtPermissionNum_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtPermissionNum.Text != "")
                {
                    conn.Close();
                    conn2.Close();

                    conn.Open();
                    conn2.Open();
                    search();

                    string q = "select import_supplier_permission.Supplier_ID from import_supplier_permission INNER JOIN storage_import_permission ON storage_import_permission.StorageImportPermission_ID = import_supplier_permission.StorageImportPermission_ID where storage_import_permission.Import_Permission_Number=" + txtPermissionNum.Text + " and storage_import_permission.Store_ID=" + storeId;
                    MySqlCommand com = new MySqlCommand(q, conn);
                    loaded = false;
                    if (com.ExecuteScalar() != null)
                    {
                        comSupplier.SelectedValue = com.ExecuteScalar().ToString();
                        txtSupplier.Text = com.ExecuteScalar().ToString();
                        //comSupplier.Enabled = false;
                        //comSupplier.DropDownStyle = ComboBoxStyle.DropDownList;
                    }
                    else
                    {
                        comSupplier.SelectedIndex = -1;
                        txtSupplier.Text = "";
                        //comSupplier.Enabled = true;
                        //comSupplier.DropDownStyle = ComboBoxStyle.DropDown;
                    }
                    loaded = true;

                    q = "select storage_import_permission.Confirmed from storage_import_permission where storage_import_permission.Import_Permission_Number=" + txtPermissionNum.Text + " and storage_import_permission.Store_ID=" + storeId;
                    com = new MySqlCommand(q, conn);
                    if (com.ExecuteScalar() != null)
                    {
                        flagConfirm = Convert.ToInt32(com.ExecuteScalar().ToString());
                    }
                    else
                    {
                        flagConfirm = 2;
                    }
                }
                else
                {
                    gridControl2.DataSource = null;
                    flagConfirm = 2;
                    loaded = false;
                    comSupplier.SelectedIndex = -1;
                    txtSupplier.Text = "";
                    //comSupplier.Enabled = true;
                    //comSupplier.DropDownStyle = ComboBoxStyle.DropDown;
                    loaded = true;
                }
                //MessageBox.Show(flagConfirm.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            conn.Close();
            conn2.Close();
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            if (flagConfirm == 2)
            {
                try
                {
                    if (comSupplier.SelectedValue != null && txtPermissionNum.Text != "" && gridView2.RowCount > 0)
                    {
                        string suppliers_Name = "";
                        conn.Open();
                        string query = "select Store_Name from store where Store_ID=" + storeId;
                        MySqlCommand com = new MySqlCommand(query, conn);
                        string storeName = com.ExecuteScalar().ToString();

                        string q = "update storage_import_permission set Confirmed=0 where storage_import_permission.Import_Permission_Number=" + txtPermissionNum.Text + " and storage_import_permission.Store_ID=" + storeId;
                        MySqlCommand c = new MySqlCommand(q, conn);
                        c.ExecuteNonQuery();
                        flagConfirm = 0;
                        conn.Close();

                        double carton = 0;
                        double balate = 0;
                        double quantity = 0;

                        List<SupplierReceipt_Items> bi = new List<SupplierReceipt_Items>();
                        for (int i = 0; i < gridView2.RowCount; i++)
                        {
                            int rowHand = gridView2.GetRowHandle(i);
                            bool flagTest = false;
                            if (gridView2.GetRowCellDisplayText(i, gridView2.Columns["عدد البلتات"]) != "")
                            {
                                balate = Convert.ToDouble(gridView2.GetRowCellDisplayText(i, gridView2.Columns["عدد البلتات"]));
                            }
                            if (gridView2.GetRowCellDisplayText(i, gridView2.Columns["عدد الكراتين"]) != "")
                            {
                                carton = Convert.ToDouble(gridView2.GetRowCellDisplayText(i, gridView2.Columns["عدد الكراتين"]));
                            }
                            if (gridView2.GetRowCellDisplayText(i, gridView2.Columns["متر/قطعة"]) != "")
                            {
                                quantity = Convert.ToDouble(gridView2.GetRowCellDisplayText(i, gridView2.Columns["متر/قطعة"]));
                            }

                            SupplierReceipt_Items item = new SupplierReceipt_Items() { Code = gridView2.GetRowCellDisplayText(i, gridView2.Columns["الكود"]), Product_Type = gridView2.GetRowCellDisplayText(i, gridView2.Columns["النوع"]), Product_Name = gridView2.GetRowCellDisplayText(i, gridView2.Columns["الاسم"]), Balatat = balate, Carton_Balata = carton, Total_Meters = quantity, Supplier_Permission_Number = Convert.ToInt32(gridView2.GetRowCellDisplayText(i, gridView2.Columns["اذن استلام"])), Date = Convert.ToDateTime(gridView2.GetRowCellDisplayText(i, gridView2.Columns["تاريخ التخزين"])).ToString("yyyy-MM-dd hh:mm:ss"), Note = gridView2.GetRowCellDisplayText(i, gridView2.Columns["ملاحظة"]) };
                            bi.Add(item);

                            if (i == 0)
                            {
                                suppliers_Name += gridView2.GetRowCellDisplayText(rowHand, gridView2.Columns["المورد"]);
                            }

                            if (i < (gridView2.RowCount - 1))
                            {
                                for (int j = 0; j < gridView2.RowCount; j++)
                                {
                                    int rowHand2 = gridView2.GetRowHandle(j);
                                    if (i != j)
                                    {
                                        if (gridView2.GetRowCellDisplayText(rowHand, gridView2.Columns["المورد"]) == gridView2.GetRowCellDisplayText(rowHand2, gridView2.Columns["المورد"]))
                                        {
                                            flagTest = true;
                                        }
                                    }
                                }
                                if (!flagTest)
                                {
                                    suppliers_Name += "," + gridView2.GetRowCellDisplayText(rowHand, gridView2.Columns["المورد"]);
                                }
                            }
                        }

                        bool flagTest2 = false;
                        for (int j = 0; j < gridView2.RowCount - 1; j++)
                        {
                            int rowHand2 = gridView2.GetRowHandle(j);
                            if (gridView2.GetRowCellDisplayText(gridView2.GetRowHandle(gridView2.RowCount - 1), gridView2.Columns["المورد"]) == gridView2.GetRowCellDisplayText(rowHand2, gridView2.Columns["المورد"]))
                            {
                                flagTest2 = true;
                            }
                        }
                        if (!flagTest2 && gridView2.RowCount > 1)
                        {
                            suppliers_Name += "," + gridView2.GetRowCellDisplayText(gridView2.GetRowHandle(gridView2.RowCount - 1), gridView2.Columns["المورد"]);
                        }

                        Report_SupplierReceipt f = new Report_SupplierReceipt();
                        f.PrintInvoice(storeName, txtPermissionNum.Text, suppliers_Name, bi);
                        f.ShowDialog();
                    }
                    else
                    {
                        MessageBox.Show("يجب ادخال البيانات كاملة");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                conn.Close();
            }
        }

        private void btnCodingDetails_Click(object sender, EventArgs e)
        {
            try
            {
                ProductItems form = new ProductItems();
                form.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            conn.Close();
        }

        private void txtSupplier_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                conn.Open();
                string query = "select Supplier_Name from supplier where Supplier_ID=" + txtSupplier.Text + "";
                MySqlCommand com = new MySqlCommand(query, conn);
                if (com.ExecuteScalar().ToString() != "")
                {
                    Name = (string)com.ExecuteScalar();
                    comSupplier.Text = Name;
                    comSupplier.SelectedValue = txtSupplier.Text;
                }
                else
                {
                    MessageBox.Show("there is no item with this id");
                    conn.Close();
                    return;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            conn.Close();
        }

        private void comSupplier_SelectedValueChanged(object sender, EventArgs e)
        {
            if (loaded)
            {
                try
                {
                    txtSupplier.Text = comSupplier.SelectedValue.ToString();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                conn.Close();
            }
        }

        private void comOrderFactory_SelectedValueChanged(object sender, EventArgs e)
        {
            if (loaded)
            {
                try
                {
                    txtOrderFactory.Text = comOrderFactory.SelectedValue.ToString();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                conn.Close();
            }
        }

        private void txtOrderFactory_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    comOrderFactory.SelectedValue = txtOrderFactory.Text;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                conn.Close();
            }
        }

        public void search()
        {
            //storage_import_permission.Import_Permission_Number=" + txtPermissionNum.Text + " and supplier_permission_details.Store_ID=" + storeId + "
            //,store_places.Store_Place_Code as 'مكان التخزين'
            //INNER JOIN store_places ON store_places.Store_Place_ID = supplier_permission_details.Store_Place_ID
            //,supplier_permission_details.Store_Place_ID
            string qq = "select data.Data_ID,data.Code as 'الكود',type.Type_Name as 'النوع',concat(product.Product_Name,' ',COALESCE(color.Color_Name,''),' ',data.Description,' ',groupo.Group_Name,' ',factory.Factory_Name,' ',COALESCE(size.Size_Value,''),' ',COALESCE(sort.Sort_Value,'')) as 'الاسم',supplier.Supplier_Name as 'المورد',import_supplier_permission.Supplier_Permission_Number as 'اذن استلام',supplier_permission_details.Balatat as 'عدد البلتات',supplier_permission_details.Carton_Balata as 'عدد الكراتين',supplier_permission_details.Total_Meters as 'متر/قطعة',DATE_FORMAT(supplier_permission_details.Date, '%d-%m-%Y %T') as 'تاريخ التخزين',supplier_permission_details.Note as 'ملاحظة',import_supplier_permission.Supplier_ID,supplier_permission_details.Supplier_Permission_Details_ID,'Factory_ID','المصنع','اذن الطلب' from supplier_permission_details INNER JOIN data ON supplier_permission_details.Data_ID = data.Data_ID INNER JOIN import_supplier_permission ON supplier_permission_details.ImportSupplierPermission_ID = import_supplier_permission.ImportSupplierPermission_ID INNER JOIN storage_import_permission ON storage_import_permission.StorageImportPermission_ID = import_supplier_permission.StorageImportPermission_ID  INNER JOIN supplier ON import_supplier_permission.Supplier_ID = supplier.Supplier_ID INNER JOIN type ON type.Type_ID = data.Type_ID INNER JOIN product ON product.Product_ID = data.Product_ID INNER JOIN factory ON data.Factory_ID = factory.Factory_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID LEFT JOIN color ON color.Color_ID = data.Color_ID LEFT JOIN size ON size.Size_ID = data.Size_ID LEFT JOIN sort ON sort.Sort_ID = data.Sort_ID where data.Data_ID=0 order by SUBSTR(data.Code,1,16),color.Color_Name,data.Description,data.Sort_ID";
            MySqlDataAdapter da = new MySqlDataAdapter(qq, conn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            gridControl2.DataSource = dt;
            gridView2.Columns["Data_ID"].Visible = false;
            gridView2.Columns["Supplier_ID"].Visible = false;
            gridView2.Columns["Supplier_Permission_Details_ID"].Visible = false;
            //gridView2.Columns["Store_Place_ID"].Visible = false;
            //gridView2.Columns["Order_Number"].Visible = false;
            gridView2.Columns["Factory_ID"].Visible = false;

            for (int i = 0; i < gridView2.Columns.Count; i++)
            {
                gridView2.Columns[i].Width = 100;
            }
            gridView2.Columns["الكود"].Width = 180;
            gridView2.Columns["الاسم"].Width = 200;
            //,store_places.Store_Place_Code as 'مكان التخزين'
            //INNER JOIN store_places ON store_places.Store_Place_ID = supplier_permission_details.Store_Place_ID
            //,supplier_permission_details.Store_Place_ID
            //order by SUBSTR(data.Code,1,16),color.Color_Name,data.Description,data.Sort_ID
            qq = "select data.Data_ID,data.Code as 'الكود',type.Type_Name as 'النوع',concat(product.Product_Name,' ',COALESCE(color.Color_Name,''),' ',data.Description,' ',groupo.Group_Name,' ',factory.Factory_Name,' ',COALESCE(size.Size_Value,''),' ',COALESCE(sort.Sort_Value,'')) as 'الاسم',supplier.Supplier_Name as 'المورد',import_supplier_permission.Supplier_Permission_Number as 'اذن استلام',supplier_permission_details.Balatat as 'عدد البلتات',supplier_permission_details.Carton_Balata as 'عدد الكراتين',supplier_permission_details.Total_Meters as 'متر/قطعة',DATE_FORMAT(supplier_permission_details.Date, '%d-%m-%Y %T') as 'تاريخ التخزين',supplier_permission_details.Note as 'ملاحظة',import_supplier_permission.Supplier_ID,supplier_permission_details.Supplier_Permission_Details_ID from supplier_permission_details INNER JOIN data ON supplier_permission_details.Data_ID = data.Data_ID INNER JOIN import_supplier_permission ON supplier_permission_details.ImportSupplierPermission_ID = import_supplier_permission.ImportSupplierPermission_ID INNER JOIN storage_import_permission ON storage_import_permission.StorageImportPermission_ID = import_supplier_permission.StorageImportPermission_ID  INNER JOIN supplier ON import_supplier_permission.Supplier_ID = supplier.Supplier_ID INNER JOIN type ON type.Type_ID = data.Type_ID INNER JOIN product ON product.Product_ID = data.Product_ID INNER JOIN factory ON data.Factory_ID = factory.Factory_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID LEFT JOIN color ON color.Color_ID = data.Color_ID LEFT JOIN size ON size.Size_ID = data.Size_ID LEFT JOIN sort ON sort.Sort_ID = data.Sort_ID where storage_import_permission.Import_Permission_Number=" + txtPermissionNum.Text + " and supplier_permission_details.Store_ID=" + storeId + " ";
            MySqlCommand com = new MySqlCommand(qq, conn);
            MySqlDataReader dr = com.ExecuteReader();
            if(dr.HasRows)
            {
                while(dr.Read())
                {
                    gridView2.AddNewRow();
                    int rowHandle = gridView2.GetRowHandle(gridView2.DataRowCount);
                    if (gridView2.IsNewItemRow(rowHandle))
                    {
                        gridView2.SetRowCellValue(rowHandle, gridView2.Columns[0], dr["Data_ID"]);
                        gridView2.SetRowCellValue(rowHandle, gridView2.Columns[1], dr["الكود"]);
                        gridView2.SetRowCellValue(rowHandle, gridView2.Columns["النوع"], dr["النوع"]);
                        gridView2.SetRowCellValue(rowHandle, gridView2.Columns["الاسم"], dr["الاسم"]);
                        gridView2.SetRowCellValue(rowHandle, gridView2.Columns["المورد"], dr["المورد"]);
                        gridView2.SetRowCellValue(rowHandle, gridView2.Columns["اذن استلام"], dr["اذن استلام"]);
                        gridView2.SetRowCellValue(rowHandle, gridView2.Columns["عدد البلتات"], dr["عدد البلتات"]);
                        gridView2.SetRowCellValue(rowHandle, gridView2.Columns["عدد الكراتين"], dr["عدد الكراتين"]);
                        gridView2.SetRowCellValue(rowHandle, gridView2.Columns["متر/قطعة"], dr["متر/قطعة"]);
                        gridView2.SetRowCellValue(rowHandle, gridView2.Columns["تاريخ التخزين"], dr["تاريخ التخزين"]);
                        //gridView2.SetRowCellValue(rowHandle, gridView2.Columns["مكان التخزين"], dr["مكان التخزين"]);
                        gridView2.SetRowCellValue(rowHandle, gridView2.Columns["ملاحظة"], dr["ملاحظة"]);
                        gridView2.SetRowCellValue(rowHandle, gridView2.Columns["Supplier_ID"], dr["Supplier_ID"]);
                        gridView2.SetRowCellValue(rowHandle, gridView2.Columns["Supplier_Permission_Details_ID"], dr["Supplier_Permission_Details_ID"]);
                        //gridView2.SetRowCellValue(rowHandle, gridView2.Columns["Store_Place_ID"], dr["Store_Place_ID"]);
                        //INNER JOIN store_places ON store_places.Store_Place_ID = supplier_permission_details.Store_Place_ID
                        //order by SUBSTR(data.Code,1,16),color.Color_Name,data.Description,data.Sort_ID
                        qq = "select orders.Order_Number,orders.Factory_ID,factory2.Factory_Name from supplier_permission_details INNER JOIN data ON supplier_permission_details.Data_ID = data.Data_ID INNER JOIN import_supplier_permission ON supplier_permission_details.ImportSupplierPermission_ID = import_supplier_permission.ImportSupplierPermission_ID INNER JOIN storage_import_permission ON storage_import_permission.StorageImportPermission_ID = import_supplier_permission.StorageImportPermission_ID  INNER JOIN supplier ON import_supplier_permission.Supplier_ID = supplier.Supplier_ID inner JOIN order_details ON supplier_permission_details.Data_ID = order_details.Data_ID inner JOIN orders ON order_details.Order_ID = orders.Order_ID INNER JOIN factory as factory2 ON orders.Factory_ID = factory2.Factory_ID INNER JOIN type ON type.Type_ID = data.Type_ID INNER JOIN product ON product.Product_ID = data.Product_ID INNER JOIN factory ON data.Factory_ID = factory.Factory_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID LEFT JOIN color ON color.Color_ID = data.Color_ID LEFT JOIN size ON size.Size_ID = data.Size_ID LEFT JOIN sort ON sort.Sort_ID = data.Sort_ID where storage_import_permission.Import_Permission_Number=" + txtPermissionNum.Text + " and supplier_permission_details.Store_ID=" + storeId + " and data.Data_ID="+dr["Data_ID"]+ " ";
                        com = new MySqlCommand(qq, conn2);
                        MySqlDataReader dr2 = com.ExecuteReader();
                        if (dr2.HasRows)
                        {
                            while (dr2.Read())
                            {
                                gridView2.SetRowCellValue(rowHandle, gridView2.Columns["اذن الطلب"], dr2["Order_Number"]);
                                gridView2.SetRowCellValue(rowHandle, gridView2.Columns["Factory_ID"], dr2["Factory_ID"]);
                                gridView2.SetRowCellValue(rowHandle, gridView2.Columns["المصنع"], dr2["Factory_Name"]);
                            }
                        }
                        dr2.Close();
                    }
                }
            }
            dr.Close();

            if (gridView2.IsLastVisibleRow)
            {
                gridView2.FocusedRowHandle = gridView2.RowCount - 1;
            }
        }

        bool IsAdded()
        {
            for (int i = 0; i < gridView2.RowCount; i++)
            {
                DataRow row3 = gridView2.GetDataRow(i);
                if ((row1["Data_ID"].ToString() == row3["Data_ID"].ToString()) && (txtSupPermissionNum.Text == row3["اذن استلام"].ToString()) && (comSupplier.SelectedValue.ToString() == row3["Supplier_ID"].ToString()))
                    return true;
            }
            return false;
        }
        
        public void clearCom()
        {
            foreach (Control co in this.panel3.Controls)
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

        public void filterProduct()
        {
            if (comType.Text != "")
            {
                if (comGroup.Text != "" || comFactory.Text != "" || comType.Text != "")
                {
                    string supQuery = "";

                    supQuery = " product.Type_ID=" + comType.SelectedValue + "";
                    if (comFactory.Text != "")
                    {
                        supQuery += " and product_factory_group.Factory_ID=" + comFactory.SelectedValue + "";
                    }
                    if (comGroup.Text != "")
                    {
                        supQuery += " and product_factory_group.Group_ID=" + comGroup.SelectedValue + "";
                    }
                    string query = "select distinct  product.Product_ID  ,Product_Name  from product inner join product_factory_group on product.Product_ID=product_factory_group.Product_ID  where  " + supQuery + "   order by product.Product_ID";
                    MySqlDataAdapter da = new MySqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    comProduct.DataSource = dt;
                    comProduct.DisplayMember = dt.Columns["Product_Name"].ToString();
                    comProduct.ValueMember = dt.Columns["Product_ID"].ToString();
                    comProduct.Text = "";
                }
            }

        }
    }
}

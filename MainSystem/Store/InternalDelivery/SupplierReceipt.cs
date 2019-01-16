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
        MySqlConnection conn, dbconnection6, dbconnection3;
        int[] courrentIDs;
        int count = 0;
        int sum;
        bool loaded = false;
        bool factoryFlage = false;
        bool groupFlage = false;
        bool flagProduct = false;
        DataRow row1;
        int storeId = 0;

        public SupplierReceipt(MainForm mainForm, DevExpress.XtraTab.XtraTabControl xtraTabControlStores)
        {
            InitializeComponent();
            courrentIDs = new int[100];
            conn = new MySqlConnection(connection.connectionString);
            dbconnection6 = new MySqlConnection(connection.connectionString);
            dbconnection3 = new MySqlConnection(connection.connectionString);
            
            comType.AutoCompleteMode = AutoCompleteMode.Suggest;
            comType.AutoCompleteSource = AutoCompleteSource.ListItems;
            comFactory.AutoCompleteMode = AutoCompleteMode.Suggest;
            comFactory.AutoCompleteSource = AutoCompleteSource.ListItems;
            comGroup.AutoCompleteMode = AutoCompleteMode.Suggest;
            comGroup.AutoCompleteSource = AutoCompleteSource.ListItems;
            comProduct.AutoCompleteMode = AutoCompleteMode.Suggest;
            comProduct.AutoCompleteSource = AutoCompleteSource.ListItems;
        }

        private void SupplierReceipt_Load(object sender, EventArgs e)
        {
            try
            {
                string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"Store.txt");
                storeId = Convert.ToInt16(System.IO.File.ReadAllText(path));

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

                query = "select * from store_places where Store_ID=" + storeId;
                da = new MySqlDataAdapter(query, conn);
                dt = new DataTable();
                da.Fill(dt);
                comStorePlace.DataSource = dt;
                comStorePlace.DisplayMember = dt.Columns["Store_Place_Code"].ToString();
                comStorePlace.ValueMember = dt.Columns["Store_Place_ID"].ToString();
                comStorePlace.Text = "";

                loaded = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            conn.Close();
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
                            string query = "select * from factory inner join type_factory on factory.Factory_ID=type_factory.Factory_ID inner join type on type_factory.Type_ID=type.Type_ID where type_factory.Type_ID=" + comType.SelectedValue.ToString();
                            MySqlDataAdapter da = new MySqlDataAdapter(query, conn);
                            DataTable dt = new DataTable();
                            da.Fill(dt);
                            comFactory.DataSource = dt;
                            comFactory.DisplayMember = dt.Columns["Factory_Name"].ToString();
                            comFactory.ValueMember = dt.Columns["Factory_ID"].ToString();
                            comFactory.Text = "";
                            if (comType.SelectedValue.ToString() == "1" || comType.SelectedValue.ToString() == "2")
                            {
                                string query2 = "select * from groupo where Factory_ID=0 and Type_ID=1";
                                MySqlDataAdapter da2 = new MySqlDataAdapter(query2, conn);
                                DataTable dt2 = new DataTable();
                                da2.Fill(dt2);
                                comGroup.DataSource = dt2;
                                comGroup.DisplayMember = dt2.Columns["Group_Name"].ToString();
                                comGroup.ValueMember = dt2.Columns["Group_ID"].ToString();
                                comGroup.Text = "";
                            }
                            else if (comType.SelectedValue.ToString() == "4")
                            {
                                string query2 = "select * from groupo where Factory_ID=-1 and Type_ID=4";
                                MySqlDataAdapter da2 = new MySqlDataAdapter(query2, conn);
                                DataTable dt2 = new DataTable();
                                da2.Fill(dt2);
                                comGroup.DataSource = dt2;
                                comGroup.DisplayMember = dt2.Columns["Group_Name"].ToString();
                                comGroup.ValueMember = dt2.Columns["Group_ID"].ToString();
                                comGroup.Text = "";
                            }
                            factoryFlage = true;

                            query = "select * from color where Type_ID=" + comType.SelectedValue.ToString();
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
                            if (comType.SelectedValue.ToString() != "1" && comType.SelectedValue.ToString() != "2" && comType.SelectedValue.ToString() != "4")
                            {
                                string query2f = "select * from groupo where Factory_ID=" + comFactory.SelectedValue.ToString();
                                MySqlDataAdapter da2f = new MySqlDataAdapter(query2f, conn);
                                DataTable dt2f = new DataTable();
                                da2f.Fill(dt2f);
                                comGroup.DataSource = dt2f;
                                comGroup.DisplayMember = dt2f.Columns["Group_Name"].ToString();
                                comGroup.ValueMember = dt2f.Columns["Group_ID"].ToString();
                                comGroup.Text = "";
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
                            string query3 = "select distinct  product.Product_ID  ,Product_Name  from product inner join product_factory_group on product.Product_ID=product_factory_group.Product_ID  where product.Type_ID=" + comType.SelectedValue.ToString() + " and product_factory_group.Factory_ID=" + comFactory.SelectedValue.ToString() + " and product_factory_group.Group_ID=" + comGroup.SelectedValue.ToString() + "  order by product.Product_ID";
                            MySqlDataAdapter da3 = new MySqlDataAdapter(query3, conn);
                            DataTable dt3 = new DataTable();
                            da3.Fill(dt3);
                            comProduct.DataSource = dt3;
                            comProduct.DisplayMember = dt3.Columns["Product_Name"].ToString();
                            comProduct.ValueMember = dt3.Columns["Product_ID"].ToString();
                            comProduct.Text = "";


                            string query2 = "select * from size where Factory_ID=" + comFactory.SelectedValue.ToString() + " and Group_ID=" + comGroup.SelectedValue.ToString();
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
                        if (flagProduct)
                        {
                            //flagProduct = false;
                            comColor.Focus();
                        }
                        break;

                    case "comColour":
                        comSize.Focus();
                        break;

                    case "comSize":
                        comSort.Focus();
                        break;

                    case "comSort":
                        { }
                        break;
                }
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                if (txtCarton.Text != "" && txtBalat.Text != "" && comStorePlace.Text != "" && txtCarton.Text != "" && txtSupPermissionNum.Text != "" && txtCode.Text != "" && txtTotalMeter.Text != "")
                {
                    string q = "select carton from data where Data_ID=" + row1[0].ToString();
                    MySqlCommand com = new MySqlCommand(q, conn);
                    double carton = double.Parse(com.ExecuteScalar().ToString());
                    int NoBalatat;
                    int.TryParse(txtBalat.Text, out NoBalatat);
                    int NoCartons;
                    int.TryParse(txtCarton.Text, out NoCartons);
                    double total = carton * NoBalatat * NoCartons;
                    /*txtTotalMeter.Text = (total).ToString();*/

                    if (int.Parse(txtPermissionNum.Text) <= sum)
                    {
                        string query = "insert into Storage (Store_ID,Storage_Date,Balatat,Carton_Balata,Data_ID,Type,Store_Place_ID,Total_Meters,Supplier_ID,Note,Permission_Number,Supplier_Permission_Number) values (@Store_ID,@Date,@NoBalatat,@NoCartonInBalata,@Data_ID,@Type,@PlaceOfStore,@TotalOfMeters,@Supplier,@Note,@Permission_Number,@Supplier_Permission_Number)";
                        com = new MySqlCommand(query, conn);
                        com.Parameters.Add("@Store_ID", MySqlDbType.Int16);
                        com.Parameters["@Store_ID"].Value = storeId;
                        com.Parameters.Add("@Date", MySqlDbType.Date, 0);
                        com.Parameters["@Date"].Value = DateTime.Now;
                        com.Parameters.Add("@NoBalatat", MySqlDbType.Int16);
                        com.Parameters["@NoBalatat"].Value = NoBalatat;
                        com.Parameters.Add("@NoCartonInBalata", MySqlDbType.Int16);
                        com.Parameters["@NoCartonInBalata"].Value = NoCartons;
                        com.Parameters.Add("@Data_ID", MySqlDbType.Int16);
                        com.Parameters["@Data_ID"].Value = row1[0].ToString();
                        com.Parameters.Add("@PlaceOfStore", MySqlDbType.Int16);
                        com.Parameters["@PlaceOfStore"].Value = comStorePlace.SelectedValue.ToString();
                        com.Parameters.Add("@TotalOfMeters", MySqlDbType.Decimal);
                        com.Parameters["@TotalOfMeters"].Value = total;
                        com.Parameters.Add("@Supplier", MySqlDbType.Int16);
                        com.Parameters["@Supplier"].Value = comSupplier.SelectedValue.ToString();
                        com.Parameters.Add("@Note", MySqlDbType.VarChar);
                        com.Parameters["@Note"].Value = txtDescription.Text;
                        com.Parameters.Add("@Permission_Number", MySqlDbType.Int16);
                        com.Parameters["@Permission_Number"].Value = int.Parse(txtPermissionNum.Text);
                        com.Parameters.Add("@Supplier_Permission_Number", MySqlDbType.Int16);
                        com.Parameters["@Supplier_Permission_Number"].Value = int.Parse(txtSupPermissionNum.Text);
                        com.ExecuteNonQuery();
                        

                        string q1 = "select Storage_ID from storage ORDER BY Storage_ID DESC LIMIT 1";
                        MySqlCommand comm = new MySqlCommand(q1, conn);
                        int id = (int)comm.ExecuteScalar();

                        courrentIDs[count] = id;
                        count++;

                        string str = "";
                        for (int i = 0; i < courrentIDs.Length - 1; i++)
                        {
                            if (courrentIDs[i] != 0)
                            {
                                str += courrentIDs[i] + ",";
                            }
                        }
                        str += courrentIDs[courrentIDs.Length - 1];

                        string qq = "select storage.Code as 'كود',type.Type_Name as 'النوع', factory.Factory_Name as 'المصنع',groupo.Group_Name as 'المجموعة',product.Product_Name as 'المنتج', storage.Store_Name as 'المخزن', storage.Supplier_Name as 'المورد',storage.Balatat as 'بلتات', storage.Carton_Balata as 'عدد الكراتين',storage.Total_Meters as 'اجمالي عدد الامتار', storage.Storage_Date as 'تاريخ التخزين' , storage.Store_Place as 'مكان التخزين'  , storage.Note as 'ملاحظة',storage.Permission_Number as 'اذن المخزن' from storage INNER JOIN data  ON storage.Code = data.Code INNER JOIN type ON type.Type_ID = data.Type_ID INNER JOIN product ON product.Product_ID = data.Product_ID INNER JOIN factory ON data.Factory_ID = factory.Factory_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID where Storage_ID in (" + str + ") ";
                        MySqlDataAdapter da = new MySqlDataAdapter(qq, conn);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        gridControl2.DataSource = dt;

                        MessageBox.Show("Add success");
                    }
                    else
                    {
                        MessageBox.Show("please enter permission number less than or equal to " + sum);
                    }
                }
                else
                {
                    MessageBox.Show("you must fill all fields please");
                    conn.Close();
                    return;
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            conn.Close();
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                /*string code = textBox9.Text;
                int StoreID = int.Parse(comStore.SelectedValue.ToString());*/
                string q = "select carton from data where Data_ID=" + row1[0].ToString();
                MySqlCommand com = new MySqlCommand(q, conn);
                double carton = double.Parse(com.ExecuteScalar().ToString());
                int NoBalatat;
                int.TryParse(txtBalat.Text, out NoBalatat);
                int NoCartons;
                int.TryParse(txtCarton.Text, out NoCartons);
                double total = carton * NoBalatat * NoCartons;
                txtTotalMeter.Text = (total).ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            conn.Close();
        }

        private void btnNewChosen_Click(object sender, EventArgs e)
        {
            clearCom();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
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

                conn.Open();
                dbconnection6.Open();
                //,sellprice.Price_Type,sellprice.Last_Price as 'السعر',sellprice.Sell_Discount as 'الخصم',sellprice.Sell_Price as 'بعد الخصم',sum(storage.Total_Meters) as 'الكمية'
                string query = "select data.Data_ID,data.Code as 'الكود','Type',concat(product.Product_Name,' - ',type.Type_Name,' - ',factory.Factory_Name,' - ',groupo.Group_Name,' ',COALESCE(color.Color_Name,''),' ',COALESCE(size.Size_Value,''),' ',COALESCE(sort.Sort_Value,'')) as 'الاسم',data.Carton as 'الكرتنة',data.Description as 'الوصف' FROM data LEFT JOIN color ON color.Color_ID = data.Color_ID LEFT JOIN size ON size.Size_ID = data.Size_ID LEFT JOIN sort ON sort.Sort_ID = data.Sort_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID INNER JOIN factory ON factory.Factory_ID = data.Factory_ID  INNER JOIN product ON product.Product_ID = data.Product_ID  INNER JOIN type ON type.Type_ID = data.Type_ID  INNER JOIN sellprice ON sellprice.Data_ID = data.Data_ID LEFT JOIN storage ON storage.Data_ID = data.Data_ID where  data.Type_ID IN(" + q1 + ") and  data.Factory_ID  IN(" + q2 + ") and data.Group_ID IN (" + q4 + ") and data.Data_ID=0 group by data.Data_ID";
                MySqlDataAdapter da = new MySqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                gridControl1.DataSource = dt;

                query = "SELECT data.Data_ID,data.Code as 'الكود',concat(product.Product_Name,' - ',type.Type_Name,' - ',factory.Factory_Name,' - ',groupo.Group_Name,' ',COALESCE(color.Color_Name,''),' ',COALESCE(size.Size_Value,''),' ',COALESCE(sort.Sort_Value,'')) as 'الاسم',data.Carton as 'الكرتنة',data.Description as 'الوصف',sum(storage.Total_Meters) as 'الكمية' FROM data LEFT JOIN color ON color.Color_ID = data.Color_ID LEFT JOIN size ON size.Size_ID = data.Size_ID LEFT JOIN sort ON sort.Sort_ID = data.Sort_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID INNER JOIN factory ON factory.Factory_ID = data.Factory_ID  INNER JOIN product ON product.Product_ID = data.Product_ID  INNER JOIN type ON type.Type_ID = data.Type_ID  LEFT JOIN storage ON storage.Data_ID = data.Data_ID where data.Type_ID IN(" + q1 + ") and data.Factory_ID IN(" + q2 + ") and data.Product_ID IN (" + q3 + ") and data.Group_ID IN (" + q4 + ") " + fQuery + " group by data.Data_ID";
                MySqlCommand comand = new MySqlCommand(query, conn);
                MySqlDataReader dr = comand.ExecuteReader();
                while (dr.Read())
                {
                    string q = "select sellprice.Last_Price as 'السعر',sellprice.Sell_Discount as 'الخصم',sellprice.Sell_Price as 'بعد الخصم',sellprice.Price_Type from data INNER JOIN sellprice ON sellprice.Data_ID = data.Data_ID where data.Data_ID=" + dr["Data_ID"].ToString() + " order by sellprice.Date desc limit 1";
                    MySqlCommand comand2 = new MySqlCommand(q, dbconnection6);
                    MySqlDataReader dr2 = comand2.ExecuteReader();
                    while (dr2.Read())
                    {
                        gridView1.AddNewRow();
                        int rowHandle = gridView1.GetRowHandle(gridView1.DataRowCount);
                        if (gridView1.IsNewItemRow(rowHandle))
                        {
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns[0], dr["Data_ID"]);
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns[1], dr["الكود"]);
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns["الاسم"], dr["الاسم"]);
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns["Type"], "بند");
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns["الكرتنة"], dr["الكرتنة"]);
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns["الوصف"], dr["الوصف"]);
                            /*gridView1.SetRowCellValue(rowHandle, gridView1.Columns["الكمية"], dr["الكمية"]);

                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns["السعر"], dr2["السعر"]);
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns["الخصم"], dr2["الخصم"]);
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns["بعد الخصم"], dr2["بعد الخصم"]);*/
                        }
                    }
                    dr2.Close();
                }
                dr.Close();
                if (gridView1.IsLastVisibleRow)
                {
                    gridView1.FocusedRowHandle = gridView1.RowCount - 1;
                }
                gridView1.Columns[0].Visible = false;
                gridView1.Columns["Type"].Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            conn.Close();
            dbconnection6.Close();
        }

        private void gridView1_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            row1 = gridView1.GetDataRow(gridView1.GetRowHandle(e.RowHandle));
            string v = row1["الكود"].ToString();
            txtCode.Text = v;

            txtCarton.Text = "";
            txtBalat.Text = "";
            txtDescription.Text = "";
            /*textBox7.Text = "";*/
        }

        private void btnCoding_Click(object sender, EventArgs e)
        {

        }

        private void txtPermissionNum_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string q = "select storage.Data_ID as 'كود',type.Type_Name as 'النوع', factory.Factory_Name as 'المصنع',groupo.Group_Name as 'المجموعة',product.Product_Name as 'المنتج', store.Store_Name as 'المخزن', supplier.Supplier_Name as 'المورد',storage.Balatat as 'بلتات', storage.Carton_Balata as 'عدد الكراتين',storage.Total_Meters as 'اجمالي عدد الامتار', storage.Storage_Date as 'تاريخ التخزين' , store_places.Store_Place_Code as 'مكان التخزين'  , storage.Note as 'ملاحظة',storage.Permission_Number as 'اذن المخزن' from storage inner join store on store.Store_ID=storage.Store_ID inner join store_places on store_places.Store_Place_ID=storage.Store_Place_ID inner join supplier on storage.Supplier_ID=supplier.Supplier_ID INNER JOIN data  ON storage.Data_ID = data.Data_ID INNER JOIN type ON type.Type_ID = data.Type_ID INNER JOIN product ON product.Product_ID = data.Product_ID INNER JOIN factory ON data.Factory_ID = factory.Factory_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID where Permission_Number=" + txtPermissionNum.Text + " and storage.Supplier_ID=" + comSupplier.SelectedValue.ToString();
                try
                {
                    MySqlDataAdapter da = new MySqlDataAdapter(q, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    gridControl2.DataSource = dt;
                    txtCode.Focus();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
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
    }
}

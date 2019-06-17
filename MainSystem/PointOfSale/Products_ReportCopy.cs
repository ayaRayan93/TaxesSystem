using DevExpress.Utils;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraPrinting;
using DevExpress.XtraPrinting.Preview;
using DevExpress.XtraTab;
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
    public partial class Products_ReportCopy : Form
    {
        MySqlConnection dbconnection, dbconnection2, dbconnection3, dbconnection4, dbconnection5, dbconnection6, dbconnection7;
        XtraTabControl MainTabControlPointSale;
        bool loaded = false;
        bool factoryFlage = false;
        bool groupFlage = false;
        bool flagProduct = false;
        bool AddedToBill = false;
        XtraTabPage xtraTabPage;
        
        public static GridControl gridcontrol;
        
        List<string> arr;
        bool billExist = false;
        bool mainBillExist = false;
        int ClintID = 0;
        MainForm main;
        DataRow loadedRow;
        string productType = "";
        DataRow row1;
        
        public static TipImage tipImage = null;
        
        int DashBillNum = 0;
        int cartons = 0;

        string code1 = "0000";
        string code2 = "0000";
        string code3 = "0000";
        string code4 = "0000";
        string code5 = "0000";
        bool flag1 = false;
        bool flag2 = false;
        bool flag3 = false;
        bool flag4 = false;
        bool flag5 = false;
        
        int empBranchID = 0;
        int empID = 0;

        public Products_ReportCopy(MainForm Min, DataRow Row1, string Type)
        {
            try
            {
                InitializeComponent();
                
                string supString = BaseData.BranchID;
                empBranchID = Convert.ToInt16(supString);

                dbconnection = new MySqlConnection(connection.connectionString);
                dbconnection2 = new MySqlConnection(connection.connectionString);
                dbconnection3 = new MySqlConnection(connection.connectionString);
                dbconnection4 = new MySqlConnection(connection.connectionString);
                dbconnection5 = new MySqlConnection(connection.connectionString);
                dbconnection6 = new MySqlConnection(connection.connectionString);
                dbconnection7 = new MySqlConnection(connection.connectionString);
             
                MainTabControlPointSale = MainForm.tabControlPointSale;

                xtraTabPage = new XtraTabPage();

                gridcontrol = gridControl1;
                arr = new List<string>();

                comClient.AutoCompleteMode = AutoCompleteMode.Suggest;
                comClient.AutoCompleteSource = AutoCompleteSource.CustomSource;

                comType.AutoCompleteMode = AutoCompleteMode.Suggest;
                comType.AutoCompleteSource = AutoCompleteSource.ListItems;
                comFactory.AutoCompleteMode = AutoCompleteMode.Suggest;
                comFactory.AutoCompleteSource = AutoCompleteSource.ListItems;
                comGroup.AutoCompleteMode = AutoCompleteMode.Suggest;
                comGroup.AutoCompleteSource = AutoCompleteSource.ListItems;
                comProduct.AutoCompleteMode = AutoCompleteMode.Suggest;
                comProduct.AutoCompleteSource = AutoCompleteSource.ListItems;

                main = Min;
                loadedRow = Row1;
                productType = Type;
                if (Type=="بند")
                {
                    radProducts.Checked = true;
                }
                else if (Type == "طقم")
                {
                    radSets.Checked = true;
                }

                panel2.AutoScroll = false;
                panel2.VerticalScroll.Enabled = false;
                panel2.VerticalScroll.Visible = false;
                panel2.VerticalScroll.Maximum = 0;
                panel2.AutoScroll = true;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void textBox_Click(object sender, EventArgs e)
        {
            openOnScreenKeyboard();
        }

        private void textBox_Leave(object sender, EventArgs e)
        {
            killOnScreenKeyboard();
        }

        private static void openOnScreenKeyboard()
        {
            System.Diagnostics.Process.Start("C:\\Program Files\\Common Files\\Microsoft shared\\ink\\TabTip.exe");

        }
        private static void killOnScreenKeyboard()
        {
            if (System.Diagnostics.Process.GetProcessesByName("TabTip").Count() > 0)
            {
                System.Diagnostics.Process asd = System.Diagnostics.Process.GetProcessesByName("TabTip").First();
                asd.Kill();
            }

        }
        
        private void SearchProduct_Load(object sender, EventArgs e)
        {
            try
            {
                dbconnection.Open();
                string query = "select * from type";
                MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                DataTable dt = new DataTable();
                da.Fill(dt);
                comType.DataSource = dt;
                comType.DisplayMember = dt.Columns["Type_Name"].ToString();
                comType.ValueMember = dt.Columns["Type_ID"].ToString();
                comType.Text = "";
                txtCodeSearch1.Text = "";

                query = "select * from factory";
                da = new MySqlDataAdapter(query, dbconnection);
                dt = new DataTable();
                da.Fill(dt);
                comFactory.DataSource = dt;
                comFactory.DisplayMember = dt.Columns["Factory_Name"].ToString();
                comFactory.ValueMember = dt.Columns["Factory_ID"].ToString();
                comFactory.Text = "";
                txtCodeSearch2.Text = "";

                query = "select * from groupo";
                da = new MySqlDataAdapter(query, dbconnection);
                dt = new DataTable();
                da.Fill(dt);
                comGroup.DataSource = dt;
                comGroup.DisplayMember = dt.Columns["Group_Name"].ToString();
                comGroup.ValueMember = dt.Columns["Group_ID"].ToString();
                comGroup.Text = "";
                txtCodeSearch3.Text = "";

                query = "select * from product";
                da = new MySqlDataAdapter(query, dbconnection);
                dt = new DataTable();
                da.Fill(dt);
                comProduct.DataSource = dt;
                comProduct.DisplayMember = dt.Columns["Product_Name"].ToString();
                comProduct.ValueMember = dt.Columns["Product_ID"].ToString();
                comProduct.Text = "";
                txtCodeSearch4.Text = "";

                query = "select * from size";
                da = new MySqlDataAdapter(query, dbconnection);
                dt = new DataTable();
                da.Fill(dt);
                comSize.DataSource = dt;
                comSize.DisplayMember = dt.Columns["Size_Value"].ToString();
                comSize.ValueMember = dt.Columns["Size_ID"].ToString();
                comSize.Text = "";
                txtSize.Text = "";

                query = "select * from color";
                da = new MySqlDataAdapter(query, dbconnection);
                dt = new DataTable();
                da.Fill(dt);
                comColor.DataSource = dt;
                comColor.DisplayMember = dt.Columns["Color_Name"].ToString();
                comColor.ValueMember = dt.Columns["Color_ID"].ToString();
                comColor.Text = "";
                txtColor.Text = "";

                query = "select * from sort";
                da = new MySqlDataAdapter(query, dbconnection);
                dt = new DataTable();
                da.Fill(dt);
                comSort.DataSource = dt;
                comSort.DisplayMember = dt.Columns["Sort_Value"].ToString();
                comSort.ValueMember = dt.Columns["Sort_ID"].ToString();
                comSort.Text = "";
                txtSort.Text = "";

                query = "select * from store";
                da = new MySqlDataAdapter(query, dbconnection);
                dt = new DataTable();
                da.Fill(dt);
                comStore.DataSource = dt;
                comStore.DisplayMember = dt.Columns["Store_Name"].ToString();
                comStore.ValueMember = dt.Columns["Store_ID"].ToString();
                comStore.Text = "";

                query = "select distinct Classification from data";
                da = new MySqlDataAdapter(query, dbconnection);
                dt = new DataTable();
                da.Fill(dt);
                comClassfication.DataSource = dt;
                comClassfication.DisplayMember = dt.Columns["Classification"].ToString();
                comClassfication.Text = "";

                query = "select * from customer";
                da = new MySqlDataAdapter(query, dbconnection);
                dt = new DataTable();
                da.Fill(dt);
                comClient.DataSource = dt;
                comClient.DisplayMember = dt.Columns["Customer_Name"].ToString();
                comClient.ValueMember = dt.Columns["Customer_ID"].ToString();
                comClient.Text = "";
                search2();
                loaded = true;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            dbconnection.Close();
        }
    
        //البنود
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
                                txtCodeSearch1.Text = comType.SelectedValue.ToString();
                                string query = "select * from factory inner join type_factory on factory.Factory_ID=type_factory.Factory_ID inner join type on type_factory.Type_ID=type.Type_ID where type_factory.Type_ID=" + txtCodeSearch1.Text;
                                MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                                DataTable dt = new DataTable();
                                da.Fill(dt);
                                comFactory.DataSource = dt;
                                comFactory.DisplayMember = dt.Columns["Factory_Name"].ToString();
                                comFactory.ValueMember = dt.Columns["Factory_ID"].ToString();
                                comFactory.Text = "";
                                txtCodeSearch2.Text = "";
                                dbconnection.Close();
                                dbconnection.Open();
                                query = "select TypeCoding_Method from type where Type_ID=" + txtCodeSearch1.Text;
                                MySqlCommand com = new MySqlCommand(query, dbconnection);
                                int TypeCoding_Method = (int)com.ExecuteScalar();
                                dbconnection.Close();
                                if (TypeCoding_Method == 1)
                                {
                                    string query2 = "";
                                    if (txtCodeSearch1.Text == "2" || txtCodeSearch1.Text == "1")
                                    {
                                        query2 = "select * from groupo where Factory_ID=-1";
                                    }
                                    else
                                    {
                                        query2 = "select * from groupo where Factory_ID=" + -Convert.ToInt16(txtCodeSearch1.Text) + " and Type_ID=" + txtCodeSearch1.Text;
                                    }

                                    MySqlDataAdapter da2 = new MySqlDataAdapter(query2, dbconnection);
                                    DataTable dt2 = new DataTable();
                                    da2.Fill(dt2);
                                    comGroup.DataSource = dt2;
                                    comGroup.DisplayMember = dt2.Columns["Group_Name"].ToString();
                                    comGroup.ValueMember = dt2.Columns["Group_ID"].ToString();
                                    comGroup.Text = "";
                                    txtCodeSearch3.Text = "";
                                    groupFlage = true;
                                }
                                factoryFlage = true;

                                query = "select * from color where Type_ID=" + txtCodeSearch1.Text;
                                da = new MySqlDataAdapter(query, dbconnection);
                                dt = new DataTable();
                                da.Fill(dt);
                                comColor.DataSource = dt;
                                comColor.DisplayMember = dt.Columns["Color_Name"].ToString();
                                comColor.ValueMember = dt.Columns["Color_ID"].ToString();
                                comColor.Text = "";
                                txtColor.Text = "";
                                comFactory.Focus();
                                if (radSets.Checked)
                                {
                                    getSetsComBox();
                                }
                            }

                            break;
                        case "comFactory":
                            if (factoryFlage)
                            {
                                txtCodeSearch2.Text = comFactory.SelectedValue.ToString();
                                dbconnection.Close();
                                dbconnection.Open();
                                string query = "select TypeCoding_Method from type where Type_ID=" + txtCodeSearch1.Text;
                                MySqlCommand com = new MySqlCommand(query, dbconnection);
                                int TypeCoding_Method = (int)com.ExecuteScalar();
                                dbconnection.Close();
                                if (TypeCoding_Method == 2)
                                {
                                    string query2f = "select * from groupo where Type_ID=" + txtCodeSearch1.Text + " and Factory_ID=" + txtCodeSearch2.Text;
                                    MySqlDataAdapter da2f = new MySqlDataAdapter(query2f, dbconnection);
                                    DataTable dt2f = new DataTable();
                                    da2f.Fill(dt2f);
                                    comGroup.DataSource = dt2f;
                                    comGroup.DisplayMember = dt2f.Columns["Group_Name"].ToString();
                                    comGroup.ValueMember = dt2f.Columns["Group_ID"].ToString();
                                    comGroup.Text = "";
                                    txtCodeSearch3.Text = "";
                                }

                                groupFlage = true;

                                string query2 = "select * from size where Factory_ID=" + txtCodeSearch2.Text;
                                MySqlDataAdapter da2 = new MySqlDataAdapter(query2, dbconnection);
                                DataTable dt2 = new DataTable();
                                da2.Fill(dt2);
                                comSize.DataSource = dt2;
                                comSize.DisplayMember = dt2.Columns["Size_Value"].ToString();
                                comSize.ValueMember = dt2.Columns["Size_ID"].ToString();
                                comSize.Text = "";
                                txtSize.Text = "";
                                comGroup.Focus();

                                query = "select distinct Classification from data where Factory_ID=" + txtCodeSearch2.Text;
                                da2 = new MySqlDataAdapter(query, dbconnection);
                                dt2 = new DataTable();
                                da2.Fill(dt2);
                                comClassfication.DataSource = dt2;
                                comClassfication.DisplayMember = dt2.Columns["Classification"].ToString();
                                comClassfication.Text = "";
                                if (radSets.Checked)
                                {
                                    getSetsComBox();
                                }
                            }
                            break;
                        case "comGroup":
                            if (groupFlage)
                            {
                                txtCodeSearch3.Text = comGroup.SelectedValue.ToString();
                                string supQuery = "", subQuery1 = "";
                                if (txtCodeSearch1.Text != "")
                                {
                                    supQuery += " and product.Type_ID=" + txtCodeSearch1.Text;
                                }
                                if (txtCodeSearch2.Text != "")
                                {
                                    supQuery += " and product_factory_group.Factory_ID=" + txtCodeSearch2.Text;
                                    subQuery1 += " and Factory_ID=" + txtCodeSearch2.Text;
                                }
                                if (radProducts.Checked)
                                {
                                    string query3 = "select distinct  product.Product_ID  ,Product_Name  from product inner join product_factory_group on product.Product_ID=product_factory_group.Product_ID  where product_factory_group.Group_ID=" + txtCodeSearch3.Text + supQuery + "  order by product.Product_ID";
                                    MySqlDataAdapter da3 = new MySqlDataAdapter(query3, dbconnection);
                                    DataTable dt3 = new DataTable();
                                    da3.Fill(dt3);
                                    flagProduct = false;
                                    comProduct.DataSource = dt3;
                                    comProduct.DisplayMember = dt3.Columns["Product_Name"].ToString();
                                    comProduct.ValueMember = dt3.Columns["Product_ID"].ToString();
                                }
                                else if(radSets.Checked)
                                {
                                    getSetsComBox();
                                }
                                comProduct.Text = "";
                                txtCodeSearch4.Text = "";
                                flagProduct = true;
                                string query2 = "select * from size where Group_ID=" + txtCodeSearch3.Text + subQuery1;
                                MySqlDataAdapter da2 = new MySqlDataAdapter(query2, dbconnection);
                                DataTable dt2 = new DataTable();
                                da2.Fill(dt2);
                                comSize.DataSource = dt2;
                                comSize.DisplayMember = dt2.Columns["Size_Value"].ToString();
                                comSize.ValueMember = dt2.Columns["Size_ID"].ToString();
                                comSize.Text = "";
                                txtSize.Text = "";

                                comProduct.Focus();
                           
                            }
                            break;

                        case "comProduct":
                                if (flagProduct)
                                {
                                    txtCodeSearch4.Text = comProduct.SelectedValue.ToString();
                                    comColor.Focus();
                                }
                            break;

                        case "comColor":
                            txtColor.Text = comColor.SelectedValue.ToString();
                            comSize.Focus();
                            break;

                        case "comSize":
                            txtSize.Text = comSize.SelectedValue.ToString();
                            break;

                        case "comSort":
                            txtSort.Text = comSort.SelectedValue.ToString();
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
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
                            case "txtCodeSearch1":
                                query = "select Type_Name from type where Type_ID='" + txtCodeSearch1.Text + "'";
                                com = new MySqlCommand(query, dbconnection);
                                if (com.ExecuteScalar() != null)
                                {
                                    Name = (string)com.ExecuteScalar();
                                    comType.Text = Name;
                                    txtCodeSearch2.Focus();
                                }
                                else
                                {
                                    MessageBox.Show("there is no item with this id");
                                    dbconnection.Close();
                                    return;
                                }
                                break;
                            case "txtCodeSearch2":
                                query = "select Factory_Name from factory where Factory_ID='" + txtCodeSearch2.Text + "'";
                                com = new MySqlCommand(query, dbconnection);
                                if (com.ExecuteScalar() != null)
                                {
                                    Name = (string)com.ExecuteScalar();
                                    comFactory.Text = Name;
                                    txtCodeSearch3.Focus();
                                }
                                else
                                {
                                    MessageBox.Show("there is no item with this id");
                                    dbconnection.Close();
                                    return;
                                }
                                break;
                            case "txtCodeSearch3":
                                query = "select Group_Name from groupo where Group_ID='" + txtCodeSearch3.Text + "'";
                                com = new MySqlCommand(query, dbconnection);
                                if (com.ExecuteScalar() != null)
                                {
                                    Name = (string)com.ExecuteScalar();
                                    comGroup.Text = Name;
                                    txtCodeSearch4.Focus();
                                }
                                else
                                {
                                    MessageBox.Show("there is no item with this id");
                                    dbconnection.Close();
                                    return;
                                }
                                break;
                            case "txtCodeSearch4":
                                query = "select Product_Name from product where Product_ID='" + txtCodeSearch4.Text + "'";
                                com = new MySqlCommand(query, dbconnection);
                                if (com.ExecuteScalar() != null)
                                {
                                    Name = (string)com.ExecuteScalar();
                                    comProduct.Text = Name;
                                    txtCodeSearch5.Focus();

                                    query = "select data.Data_ID,data.Code as 'الكود','Type',type.Type_Name as 'النوع',concat(product.Product_Name,' - ',type.Type_Name,' - ',factory.Factory_Name,' - ',groupo.Group_Name,' ',COALESCE(color.Color_Name,''),' ',COALESCE(size.Size_Value,''),' ',COALESCE(sort.Sort_Value,'')) as 'الاسم',sum(storage.Total_Meters) as 'الكمية',sellprice.Last_Price as 'السعر',sellprice.Sell_Discount as 'الخصم',sellprice.Sell_Price as 'بعد الخصم',data.Carton as 'الكرتنة' FROM data LEFT JOIN color ON color.Color_ID = data.Color_ID LEFT JOIN size ON size.Size_ID = data.Size_ID LEFT JOIN sort ON sort.Sort_ID = data.Sort_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID INNER JOIN factory ON factory.Factory_ID = data.Factory_ID  INNER JOIN product ON product.Product_ID = data.Product_ID  INNER JOIN type ON type.Type_ID = data.Type_ID  INNER JOIN sellprice ON sellprice.Data_ID = data.Data_ID LEFT JOIN storage ON storage.Data_ID = data.Data_ID where data.Code like '" + code1 + code2 + code3 + code4 + "%' and data.Data_ID=0 group by data.Data_ID";
                                    MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                                    DataTable dt = new DataTable();
                                    da.Fill(dt);
                                    gridControl1.DataSource = dt;
                                    gridView1.Columns["Type"].Visible = false;
                                    gridView1.Columns[0].Visible = false;

                                    dbconnection6.Open();
                                    query = "SELECT data.Data_ID,data.Code as 'الكود',type.Type_Name as 'النوع',concat(product.Product_Name,' ',COALESCE(color.Color_Name,''),' ',data.Description,' ',groupo.Group_Name,' ',factory.Factory_Name,' ',COALESCE(size.Size_Value,''),' ',COALESCE(sort.Sort_Value,'')) as 'الاسم',data.Carton as 'الكرتنة',sum(storage.Total_Meters) as 'الكمية' FROM data LEFT JOIN color ON color.Color_ID = data.Color_ID LEFT JOIN size ON size.Size_ID = data.Size_ID LEFT JOIN sort ON sort.Sort_ID = data.Sort_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID INNER JOIN factory ON factory.Factory_ID = data.Factory_ID  INNER JOIN product ON product.Product_ID = data.Product_ID  INNER JOIN type ON type.Type_ID = data.Type_ID  LEFT JOIN storage ON storage.Data_ID = data.Data_ID where data.Code like '" + code1 + code2 + code3 + code4 + "%' group by data.Data_ID order by SUBSTR(data.Code,1,16),color.Color_Name,data.Description,data.Sort_ID";
                                    MySqlCommand comand = new MySqlCommand(query, dbconnection);
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
                                                gridView1.SetRowCellValue(rowHandle, gridView1.Columns["النوع"], dr["النوع"]);
                                                gridView1.SetRowCellValue(rowHandle, gridView1.Columns["الاسم"], dr["الاسم"]);
                                                gridView1.SetRowCellValue(rowHandle, gridView1.Columns["Type"], "بند");
                                                gridView1.SetRowCellValue(rowHandle, gridView1.Columns["الكرتنة"], dr["الكرتنة"]);
                                                gridView1.SetRowCellValue(rowHandle, gridView1.Columns["الكمية"], dr["الكمية"]);

                                                gridView1.SetRowCellValue(rowHandle, gridView1.Columns["السعر"], dr2["السعر"]);
                                                gridView1.SetRowCellValue(rowHandle, gridView1.Columns["الخصم"], dr2["الخصم"]);
                                                gridView1.SetRowCellValue(rowHandle, gridView1.Columns["بعد الخصم"], dr2["بعد الخصم"]);
                                            }
                                        }
                                        dr2.Close();
                                    }
                                    dr.Close();
                                    if (gridView1.IsLastVisibleRow)
                                    {
                                        gridView1.FocusedRowHandle = gridView1.RowCount - 1;
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("there is no item with this id");
                                    dbconnection.Close();
                                    return;
                                }
                                break;

                            case "txtColor":
                                query = "select Color_Name from color where Color_ID='" + txtColor.Text + "'";
                                com = new MySqlCommand(query, dbconnection);
                                if (com.ExecuteScalar() != null)
                                {
                                    Name = (string)com.ExecuteScalar();
                                    comColor.Text = Name;
                                    txtSize.Focus();
                                    dbconnection.Close();
                                }
                                else
                                {
                                    MessageBox.Show("there is no item with this id");
                                    dbconnection.Close();
                                    return;
                                }
                                break;
                            case "txtSize":
                                query = "select Size_Value from size where Size_ID='" + txtSize.Text + "'";
                                com = new MySqlCommand(query, dbconnection);
                                if (com.ExecuteScalar() != null)
                                {
                                    Name = (string)com.ExecuteScalar();
                                    comSize.Text = Name;
                                    txtSort.Focus();
                                    dbconnection.Close();
                                }
                                else
                                {
                                    MessageBox.Show("there is no item with this id");
                                    dbconnection.Close();
                                    return;
                                }
                                break;
                            case "txtSort":
                                query = "select Sort_Value from sort where Sort_ID='" + txtSort.Text + "'";
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
                                    dbconnection.Close();
                                    return;
                                }
                                break;

                        }

                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                dbconnection.Close();
            }
        }
        private void txtCodeSearch_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    dbconnection.Open();
                    dbconnection6.Open();
                    string query = "select data.Data_ID,data.Code as 'الكود','Type',type.Type_Name as 'النوع',concat(product.Product_Name,' - ',type.Type_Name,' - ',factory.Factory_Name,' - ',groupo.Group_Name,' ',COALESCE(color.Color_Name,''),' ',COALESCE(size.Size_Value,''),' ',COALESCE(sort.Sort_Value,'')) as 'الاسم',sum(storage.Total_Meters) as 'الكمية',sellprice.Last_Price as 'السعر',sellprice.Sell_Discount as 'الخصم',sellprice.Sell_Price as 'بعد الخصم',data.Carton as 'الكرتنة' FROM data LEFT JOIN color ON color.Color_ID = data.Color_ID LEFT JOIN size ON size.Size_ID = data.Size_ID LEFT JOIN sort ON sort.Sort_ID = data.Sort_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID INNER JOIN factory ON factory.Factory_ID = data.Factory_ID  INNER JOIN product ON product.Product_ID = data.Product_ID  INNER JOIN type ON type.Type_ID = data.Type_ID  INNER JOIN sellprice ON sellprice.Data_ID = data.Data_ID LEFT JOIN storage ON storage.Data_ID = data.Data_ID where data.Code='" + code1 + code2 + code3 + code4 + code5 + "' and data.Data_ID=0 group by data.Data_ID";
                    MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    gridControl1.DataSource = dt;
                    gridView1.Columns["Type"].Visible = false;
                    gridView1.Columns[0].Visible = false;

                    query = "SELECT data.Data_ID,data.Code as 'الكود',type.Type_Name as 'النوع',concat(product.Product_Name,' ',COALESCE(color.Color_Name,''),' ',data.Description,' ',groupo.Group_Name,' ',factory.Factory_Name,' ',COALESCE(size.Size_Value,''),' ',COALESCE(sort.Sort_Value,'')) as 'الاسم',data.Carton as 'الكرتنة',sum(storage.Total_Meters) as 'الكمية' FROM data LEFT JOIN color ON color.Color_ID = data.Color_ID LEFT JOIN size ON size.Size_ID = data.Size_ID LEFT JOIN sort ON sort.Sort_ID = data.Sort_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID INNER JOIN factory ON factory.Factory_ID = data.Factory_ID  INNER JOIN product ON product.Product_ID = data.Product_ID  INNER JOIN type ON type.Type_ID = data.Type_ID LEFT JOIN storage ON storage.Data_ID = data.Data_ID where data.Code='" + code1 + code2 + code3 + code4 + code5 + "' group by data.Data_ID order by SUBSTR(data.Code,1,16),color.Color_Name,data.Description,data.Sort_ID";
                    MySqlCommand comand = new MySqlCommand(query, dbconnection);
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
                                gridView1.SetRowCellValue(rowHandle, gridView1.Columns["النوع"], dr["النوع"]);
                                gridView1.SetRowCellValue(rowHandle, gridView1.Columns["الاسم"], dr["الاسم"]);
                                gridView1.SetRowCellValue(rowHandle, gridView1.Columns["Type"], "بند");
                                gridView1.SetRowCellValue(rowHandle, gridView1.Columns["الكرتنة"], dr["الكرتنة"]);
                                gridView1.SetRowCellValue(rowHandle, gridView1.Columns["الكمية"], dr["الكمية"]);

                                gridView1.SetRowCellValue(rowHandle, gridView1.Columns["السعر"], dr2["السعر"]);
                                gridView1.SetRowCellValue(rowHandle, gridView1.Columns["الخصم"], dr2["الخصم"]);
                                gridView1.SetRowCellValue(rowHandle, gridView1.Columns["بعد الخصم"], dr2["بعد الخصم"]);
                            }
                        }
                        dr2.Close();
                    }
                    dr.Close();
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
        private void txtCodeSearch1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                int result = 0;
                if (txtCodeSearch1.Text != "")
                {
                    if (int.TryParse(txtCodeSearch1.Text, out result))
                    {
                        if (!flag1)
                        {
                            if (txtCodeSearch1.TextLength == 1)
                            {
                                code1 = "000" + txtCodeSearch1.Text;
                            }
                            else if (txtCodeSearch1.TextLength == 2)
                            {
                                code1 = "00" + txtCodeSearch1.Text;
                            }
                            else if (txtCodeSearch1.TextLength == 3)
                            {
                                code1 = "0" + txtCodeSearch1.Text;
                            }
                            else if (txtCodeSearch1.TextLength == 4)
                            {
                                code1 = txtCodeSearch1.Text;
                            }
                            else if (txtCodeSearch1.TextLength > 4)
                            {
                                txtCodeSearch1.Text = code1;
                            }
                            else if (txtCodeSearch1.TextLength == 0)
                            {
                                code1 = "0000";
                            }
                        }
                    }
                    else
                    {
                        flag1 = true;
                        if (txtCodeSearch1.TextLength == 1)
                        {
                            txtCodeSearch1.Text = "";
                        }
                        else if (txtCodeSearch1.TextLength > 1)
                        {
                            for (int i = 0; i < txtCodeSearch1.TextLength - 1; i++)
                            {
                                txtCodeSearch1.Text = txtCodeSearch1.Text[i].ToString();
                            }
                        }
                        MessageBox.Show("يمكنك ادخال ارقام فقط");
                        flag1 = false;
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void txtCodeSearch2_TextChanged(object sender, EventArgs e)
        {
            try
            {
                int result = 0;
                if (txtCodeSearch2.Text != "")
                {
                    if (int.TryParse(txtCodeSearch2.Text, out result))
                    {
                        if (!flag2)
                        {
                            if (txtCodeSearch2.TextLength == 1)
                            {
                                code2 = "000" + txtCodeSearch2.Text;
                            }
                            else if (txtCodeSearch2.TextLength == 2)
                            {
                                code2 = "00" + txtCodeSearch2.Text;
                            }
                            else if (txtCodeSearch2.TextLength == 3)
                            {
                                code2 = "0" + txtCodeSearch2.Text;
                            }
                            else if (txtCodeSearch2.TextLength == 4)
                            {
                                code2 = txtCodeSearch2.Text;
                            }
                            else if (txtCodeSearch2.TextLength > 4)
                            {
                                txtCodeSearch2.Text = code2;
                            }
                            else if (txtCodeSearch2.TextLength == 0)
                            {
                                code2 = "0000";
                            }
                        }
                    }
                    else
                    {
                        flag2 = true;
                        if (txtCodeSearch2.TextLength == 1)
                        {
                            txtCodeSearch2.Text = "";
                        }
                        else if (txtCodeSearch2.TextLength > 1)
                        {
                            for (int i = 0; i < txtCodeSearch2.TextLength - 1; i++)
                            {
                                txtCodeSearch2.Text = txtCodeSearch2.Text[i].ToString();
                            }
                        }
                        MessageBox.Show("يمكنك ادخال ارقام فقط");
                        flag2 = false;
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void txtCodeSearch3_TextChanged(object sender, EventArgs e)
        {
            try
            {
                int result = 0;
                if (txtCodeSearch3.Text != "")
                {
                    if (int.TryParse(txtCodeSearch3.Text, out result))
                    {
                        if (!flag3)
                        {
                            if (txtCodeSearch3.TextLength == 1)
                            {
                                code3 = "000" + txtCodeSearch3.Text;
                            }
                            else if (txtCodeSearch3.TextLength == 2)
                            {
                                code3 = "00" + txtCodeSearch3.Text;
                            }
                            else if (txtCodeSearch3.TextLength == 3)
                            {
                                code3 = "0" + txtCodeSearch3.Text;
                            }
                            else if (txtCodeSearch3.TextLength == 4)
                            {
                                code3 = txtCodeSearch3.Text;
                            }
                            else if (txtCodeSearch3.TextLength > 4)
                            {
                                txtCodeSearch3.Text = code3;
                            }
                            else if (txtCodeSearch3.TextLength == 0)
                            {
                                code3 = "0000";
                            }
                        }
                    }
                    else
                    {
                        flag3 = true;
                        if (txtCodeSearch3.TextLength == 1)
                        {
                            txtCodeSearch3.Text = "";
                        }
                        else if (txtCodeSearch3.TextLength > 1)
                        {
                            for (int i = 0; i < txtCodeSearch3.TextLength - 1; i++)
                            {
                                txtCodeSearch3.Text = txtCodeSearch3.Text[i].ToString();
                            }
                        }
                        MessageBox.Show("يمكنك ادخال ارقام فقط");
                        flag3 = false;
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void txtCodeSearch4_TextChanged(object sender, EventArgs e)
        {
            try
            {
                int result = 0;
                if (txtCodeSearch4.Text != "")
                {
                    if (int.TryParse(txtCodeSearch4.Text, out result))
                    {
                        if (!flag4)
                        {
                            if (txtCodeSearch4.TextLength == 1)
                            {
                                code4 = "000" + txtCodeSearch4.Text;
                            }
                            else if (txtCodeSearch4.TextLength == 2)
                            {
                                code4 = "00" + txtCodeSearch4.Text;
                            }
                            else if (txtCodeSearch4.TextLength == 3)
                            {
                                code4 = "0" + txtCodeSearch4.Text;
                            }
                            else if (txtCodeSearch4.TextLength == 4)
                            {
                                code4 = txtCodeSearch4.Text;
                            }
                            else if (txtCodeSearch4.TextLength > 4)
                            {
                                txtCodeSearch4.Text = code4;
                            }
                            else if (txtCodeSearch4.TextLength == 0)
                            {
                                code4 = "0000";
                            }
                        }
                    }
                    else
                    {
                        flag4 = true;
                        if (txtCodeSearch4.TextLength == 1)
                        {
                            txtCodeSearch4.Text = "";
                        }
                        else if (txtCodeSearch4.TextLength > 1)
                        {
                            for (int i = 0; i < txtCodeSearch4.TextLength - 1; i++)
                            {
                                txtCodeSearch4.Text = txtCodeSearch4.Text[i].ToString();
                            }
                        }
                        MessageBox.Show("يمكنك ادخال ارقام فقط");
                        flag4 = false;
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void txtCodeSearch5_TextChanged(object sender, EventArgs e)
        {
            try
            {
                int result = 0;
                if (txtCodeSearch5.Text != "")
                {
                    if (int.TryParse(txtCodeSearch5.Text, out result))
                    {
                        if (!flag5)
                        {
                            if (txtCodeSearch5.TextLength == 1)
                            {
                                code5 = "000" + txtCodeSearch5.Text;
                            }
                            else if (txtCodeSearch5.TextLength == 2)
                            {
                                code5 = "00" + txtCodeSearch5.Text;
                            }
                            else if (txtCodeSearch5.TextLength == 3)
                            {
                                code5 = "0" + txtCodeSearch5.Text;
                            }
                            else if (txtCodeSearch5.TextLength == 4)
                            {
                                code5 = txtCodeSearch5.Text;
                            }
                            else if (txtCodeSearch5.TextLength > 4)
                            {
                                txtCodeSearch5.Text = code5;
                            }
                            else if (txtCodeSearch5.TextLength == 0)
                            {
                                code5 = "0000";
                            }
                        }
                    }
                    else
                    {
                        flag5 = true;
                        if (txtCodeSearch5.TextLength == 1)
                        {
                            txtCodeSearch5.Text = "";
                        }
                        else if (txtCodeSearch5.TextLength > 1)
                        {
                            for (int i = 0; i < txtCodeSearch5.TextLength - 1; i++)
                            {
                                txtCodeSearch5.Text = txtCodeSearch5.Text[i].ToString();
                            }
                        }
                        MessageBox.Show("يمكنك ادخال ارقام فقط");
                        flag5 = false;
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btnNewChooes_Click(object sender, EventArgs e)
        {
            try
            {
                comProduct.Text = "";
                comType.Text = "";
                comFactory.Text = "";
                comGroup.Text = "";
                comColor.Text = "";
                comSize.Text = "";
                comSort.Text = "";
                comClassfication.Text = "";

                txtCodeSearch1.Text = "";
                txtCodeSearch2.Text = "";
                txtCodeSearch3.Text = "";
                txtCodeSearch4.Text = "";
                txtCodeSearch5.Text = "";
                txtColor.Text = "";
                txtSize.Text = "";
                txtSort.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
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
                if (comClassfication.Text != "")
                {
                    fQuery += "and data.Classification='" + comClassfication.Text + "'";
                }

                dbconnection.Open();
                dbconnection6.Open();

                string query = "select data.Data_ID,data.Code as 'الكود','Type',type.Type_Name as 'النوع',concat(product.Product_Name,' - ',type.Type_Name,' - ',factory.Factory_Name,' - ',groupo.Group_Name,' ',COALESCE(color.Color_Name,''),' ',COALESCE(size.Size_Value,''),' ',COALESCE(sort.Sort_Value,'')) as 'الاسم',sum(storage.Total_Meters) as 'الكمية',sellprice.Last_Price as 'السعر',sellprice.Sell_Discount as 'الخصم',sellprice.Sell_Price as 'بعد الخصم',data.Carton as 'الكرتنة',sellprice.Price_Type FROM data LEFT JOIN color ON color.Color_ID = data.Color_ID LEFT JOIN size ON size.Size_ID = data.Size_ID LEFT JOIN sort ON sort.Sort_ID = data.Sort_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID INNER JOIN factory ON factory.Factory_ID = data.Factory_ID  INNER JOIN product ON product.Product_ID = data.Product_ID  INNER JOIN type ON type.Type_ID = data.Type_ID  INNER JOIN sellprice ON sellprice.Data_ID = data.Data_ID LEFT JOIN storage ON storage.Data_ID = data.Data_ID where  data.Type_ID IN(" + q1 + ") and  data.Factory_ID  IN(" + q2 + ") and data.Group_ID IN (" + q4 + ") and data.Data_ID=0 group by data.Data_ID";
                MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                DataTable dt = new DataTable();
                da.Fill(dt);
                gridControl1.DataSource = dt;
                if (radProducts.Checked)
                {
                    query = "SELECT data.Data_ID,data.Code as 'الكود',type.Type_Name as 'النوع',concat(product.Product_Name,' ',COALESCE(color.Color_Name,''),' ',data.Description,' ',groupo.Group_Name,' ',factory.Factory_Name,' ',COALESCE(size.Size_Value,''),' ',COALESCE(sort.Sort_Value,'')) as 'الاسم',data.Carton as 'الكرتنة',sum(storage.Total_Meters) as 'الكمية' FROM data LEFT JOIN color ON color.Color_ID = data.Color_ID LEFT JOIN size ON size.Size_ID = data.Size_ID LEFT JOIN sort ON sort.Sort_ID = data.Sort_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID INNER JOIN factory ON factory.Factory_ID = data.Factory_ID  INNER JOIN product ON product.Product_ID = data.Product_ID  INNER JOIN type ON type.Type_ID = data.Type_ID  LEFT JOIN storage ON storage.Data_ID = data.Data_ID where data.Type_ID IN(" + q1 + ") and data.Factory_ID IN(" + q2 + ") and data.Product_ID IN (" + q3 + ") and data.Group_ID IN (" + q4 + ") " + fQuery + " group by data.Data_ID order by SUBSTR(data.Code,1,16),color.Color_Name,data.Description,data.Sort_ID";
                    MySqlCommand comand = new MySqlCommand(query, dbconnection);
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
                                gridView1.SetRowCellValue(rowHandle, gridView1.Columns["النوع"], dr["النوع"]);
                                gridView1.SetRowCellValue(rowHandle, gridView1.Columns["الاسم"], dr["الاسم"]);
                                gridView1.SetRowCellValue(rowHandle, gridView1.Columns["Type"], "بند");
                                gridView1.SetRowCellValue(rowHandle, gridView1.Columns["الكرتنة"], dr["الكرتنة"]);
                                gridView1.SetRowCellValue(rowHandle, gridView1.Columns["الكمية"], dr["الكمية"]);

                                gridView1.SetRowCellValue(rowHandle, gridView1.Columns["السعر"], dr2["السعر"]);
                                gridView1.SetRowCellValue(rowHandle, gridView1.Columns["الخصم"], dr2["الخصم"]);
                                gridView1.SetRowCellValue(rowHandle, gridView1.Columns["بعد الخصم"], dr2["بعد الخصم"]);
                            }
                        }
                        dr2.Close();
                    }
                    dr.Close();
                }
                else if (radSets.Checked)
                {
                    if (comProduct.Text == "")
                    {
                        q3 = "select Set_ID from sets";
                    }
                    else
                    {
                        q3 = comProduct.SelectedValue.ToString();
                    }

                    MySqlDataAdapter adapter = new MySqlDataAdapter("SELECT data.Data_ID,data.Code as 'الكود','Type',type.Type_Name as 'النوع',concat(product.Product_Name,' - ',type.Type_Name,' - ',factory.Factory_Name,' - ',groupo.Group_Name,' ',COALESCE(color.Color_Name,''),' ',COALESCE(size.Size_Value,''),' ',COALESCE(sort.Sort_Value,'')) as 'الاسم',sum(storage.Total_Meters) as 'الكمية',sellprice.Last_Price as 'السعر',sellprice.Sell_Discount as 'الخصم',sellprice.Sell_Price as 'بعد الخصم',data.Carton as 'الكرتنة' FROM data LEFT JOIN color ON color.Color_ID = data.Color_ID LEFT JOIN size ON size.Size_ID = data.Size_ID LEFT JOIN sort ON sort.Sort_ID = data.Sort_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID INNER JOIN factory ON factory.Factory_ID = data.Factory_ID  INNER JOIN product ON product.Product_ID = data.Product_ID  INNER JOIN type ON type.Type_ID = data.Type_ID  INNER JOIN sellprice ON sellprice.Data_ID = data.Data_ID LEFT JOIN storage ON storage.Data_ID = data.Data_ID where data.Data_ID=0 group by data.Data_ID", dbconnection);
                    DataTable dtf = new DataTable();
                    adapter.Fill(dtf);
                    gridControl1.DataSource = dtf;
                    //gridView1.Columns["Type"].Visible = false;
                    //gridView1.Columns[0].Visible = false;

                    query = "SELECT sets.Set_ID as 'الكود','Type',type.Type_Name as 'النوع',sets.Set_Name as 'الاسم',sets.Description as 'الوصف' FROM sets INNER JOIN type ON type.Type_ID = sets.Type_ID INNER JOIN factory ON factory.Factory_ID = sets.Factory_ID where sets.Type_ID IN(" + q1 + ") and sets.Factory_ID IN(" + q2 + ") and sets.Set_ID IN (" + q3 + ")";
                    MySqlCommand comand = new MySqlCommand(query, dbconnection);
                    MySqlDataReader dr = comand.ExecuteReader();
                    while (dr.Read())
                    {
                        dbconnection3.Open();
                        double price = 0;
                        double priceF = 0;
                        double totalQuanity = 0;
                        gridView1.AddNewRow();
                        int rowHandle = gridView1.GetRowHandle(gridView1.DataRowCount);
                        if (gridView1.IsNewItemRow(rowHandle))
                        {
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns[1], dr["الكود"]);
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns["النوع"], dr["النوع"]);
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns["الاسم"], dr["الاسم"]);
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns["Type"], "طقم");

                            query = "SELECT sum(set_details.Quantity*sellprice.Last_Price) as 'السعر',(set_details.Quantity*sellprice.Sell_Discount) as 'الخصم',sum(set_details.Quantity*sellprice.Sell_Price) as 'بعد الخصم' FROM sets INNER JOIN set_details ON set_details.Set_ID = sets.Set_ID INNER JOIN sellprice ON set_details.Data_ID = sellprice.Data_ID where sets.Set_ID=" + dr["الكود"] + " group by sets.Set_ID order by sellprice.Date desc";
                            comand = new MySqlCommand(query, dbconnection3);
                            MySqlDataReader dr1 = comand.ExecuteReader();
                            while (dr1.Read())
                            {
                                price += Convert.ToDouble(dr1["السعر"].ToString());
                                priceF += Convert.ToDouble(dr1["بعد الخصم"].ToString());

                                gridView1.SetRowCellValue(rowHandle, gridView1.Columns["الخصم"], dr1["الخصم"]);
                            }
                            dr1.Close();

                            query = "SELECT sum(storage.Total_Meters) as 'الكمية' FROM sets LEFT JOIN storage ON storage.Set_ID = sets.Set_ID where sets.Set_ID=" + dr["الكود"] + " group by sets.Set_ID";
                            comand = new MySqlCommand(query, dbconnection3);
                            dr1 = comand.ExecuteReader();
                            while (dr1.Read())
                            {
                                if (dr1["الكمية"].ToString() != "")
                                {
                                    totalQuanity += Convert.ToDouble(dr1["الكمية"].ToString());
                                }
                                else
                                {
                                    totalQuanity += 0;
                                }
                            }
                            dr1.Close();

                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns["السعر"], price);
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns["بعد الخصم"], priceF);
                            if (totalQuanity != 0)
                            {
                                gridView1.SetRowCellValue(rowHandle, gridView1.Columns["الكمية"], totalQuanity);
                            }
                            else
                            {
                                gridView1.SetRowCellValue(rowHandle, gridView1.Columns["الكمية"], "");
                            }
                        }
                        dbconnection3.Close();
                    }
                    dr.Close();
                    
                }


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
            dbconnection6.Close();
        }

        private void labSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (tLPanCpntent.RowStyles[1].Height == 120)
                {
                    tLPanCpntent.RowStyles[1].Height = 200;
                }
                else
                {
                    tLPanCpntent.RowStyles[1].Height = 120;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void RadioButton_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                dbconnection.Open();
                RadioButton radio = (RadioButton)sender;
                switch (radio.Name)
                {
                    case "radProducts":
                        label1.Text = "الصنف";
                        labSearch.Visible = true;
                        label13.Text = "بحث بكود الصنف";
                        txtCodeSearch5.Visible = true;
                        string query = "select * from product";
                        MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        flagProduct = false;
                        comProduct.DataSource = dt;
                        comProduct.DisplayMember = dt.Columns["Product_Name"].ToString();
                        comProduct.ValueMember = dt.Columns["Product_ID"].ToString();
                        comProduct.Text = "";
                        txtCodeSearch4.Text = "";
                        flagProduct = true;
                        break;
                    case "radSets":
                        label1.Text = "الطقم";
                        labSearch.Visible = false;
                        label13.Text = "بحث بكود الطقم";
                        txtCodeSearch5.Visible = false;
                        if (tLPanCpntent.RowStyles[1].Height == 200)
                        {
                            tLPanCpntent.RowStyles[1].Height = 120;
                        }
                        query = "select * from sets";
                        da = new MySqlDataAdapter(query, dbconnection);
                        dt = new DataTable();
                        da.Fill(dt);
                        flagProduct = false;
                        comProduct.DataSource = dt;
                        comProduct.DisplayMember = dt.Columns["Set_Name"].ToString();
                        comProduct.ValueMember = dt.Columns["Set_ID"].ToString();
                        comProduct.Text = "";
                        txtCodeSearch4.Text = "";
                        flagProduct = true;
                        break;
                    case "radOffers":
                        label1.Text = "العرض";
                        labSearch.Visible = false;
                        label13.Text = "بحث بكود العرض";
                        txtCodeSearch5.Visible = true;
                        if (tLPanCpntent.RowStyles[1].Height == 200)
                        {
                            tLPanCpntent.RowStyles[1].Height = 120;
                        }

                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }

        private void txtBillNum_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                MySqlDataReader dr2 = null;
                try
                {
                    int billnum = 0;
                    if (int.TryParse(txtBillNum.Text, out billnum))
                    {
                        dbconnection5.Open();
                        string qt = "select dash_delegate_bill.Delegate_ID from dash_delegate_bill inner join dash on dash_delegate_bill.Dash_ID=dash.Dash_ID where dash_delegate_bill.Bill_Number=" + billnum + " and dash_delegate_bill.Branch_ID=" + empBranchID + " and dash.Confirmed=0 order by dash.Dash_ID desc limit 1";
                        MySqlCommand comt = new MySqlCommand(qt, dbconnection5);
                        if (comt.ExecuteScalar() == null)
                        {
                            empID = 0;
                            comClient.Text = "";
                            txtPhone.Text = "";
                            comClient.Enabled = true;
                            txtPhone.Enabled = true;
                            txtClientId.Enabled = true;
                            txtClientId.Text = "";
                            AddedToBill = false;
                            billExist = false;
                            mainBillExist = false;
                            main.test(0);
                            dbconnection5.Close();
                            MessageBox.Show("يوجد خطا فى هذه الفاتورة");
                            return;
                        }
                        empID = Convert.ToInt16(comt.ExecuteScalar().ToString());

                        string q = "select * from dash where dash.Branch_ID=" + empBranchID + " and dash.Bill_Number=" + billnum + " and dash.Confirmed=0 order by dash.Dash_ID desc limit 1";
                        MySqlCommand cc = new MySqlCommand(q, dbconnection5);
                        MySqlDataReader dr4 = cc.ExecuteReader();
                        if (dr4.HasRows)
                        {
                            while (dr4.Read())
                            {
                                DashBillNum = Convert.ToInt16(dr4["Dash_ID"].ToString());
                                if (CheckDelegateBill(DashBillNum))
                                {
                                    mainBillExist = true;
                                    if (dr4["Customer_ID"].ToString() != "")
                                    {
                                        dbconnection4.Open();
                                        string query = "select * from customer inner join customer_phone on customer_phone.Customer_ID=customer.Customer_ID where customer.Customer_ID=" + dr4["Customer_ID"].ToString();
                                        MySqlCommand cd = new MySqlCommand(query, dbconnection4);
                                        MySqlDataReader dr3 = cd.ExecuteReader();
                                        if (dr3.HasRows)
                                        {
                                            while (dr3.Read())
                                            {
                                                txtPhone.Enabled = false;
                                                comClient.Enabled = false;
                                                txtClientId.Enabled = false;
                                                txtPhone.Text = dr3["Phone"].ToString();
                                                comClient.Text = dr3["Customer_Name"].ToString();
                                                comClient.SelectedValue = dr4["Customer_ID"].ToString();
                                                ClintID = Convert.ToInt16(dr4["Customer_ID"].ToString());
                                                txtClientId.Text = ClintID.ToString();
                                                AddedToBill = true;
                                            }
                                            dr3.Close();
                                        }
                                    }
                                    else
                                    {
                                        comClient.Text = "";
                                        txtPhone.Text = "";
                                        comClient.Enabled = true;
                                        txtPhone.Enabled = true;
                                        txtClientId.Enabled = true;
                                        txtClientId.Text = "";
                                        AddedToBill = false;
                                    }
                                }
                                else
                                {
                                    ////////////////////////
                                    comClient.Text = "";
                                    txtPhone.Text = "";
                                    comClient.Enabled = true;
                                    txtPhone.Enabled = true;
                                    txtClientId.Enabled = true;
                                    txtClientId.Text = "";
                                    AddedToBill = false;
                                    billExist = false;
                                    mainBillExist = false;
                                    main.test(0);
                                    ///////////////////////
                                    MessageBox.Show("المندوب غير مسجل علي هذه الفاتورة");
                                    txtBillNum.Text = "";
                                    dbconnection4.Close();
                                    dbconnection2.Close();
                                    dbconnection5.Close();
                                    return;
                                }
                            }

                            string qu = "select * from dash INNER JOIN dash_details ON dash_details.Dash_ID = dash.Dash_ID where dash.Branch_ID=" + empBranchID + " and dash.Bill_Number=" + billnum + " and dash.Confirmed=0 order by dash.Dash_ID desc limit 1";
                            dbconnection2.Open();
                            MySqlCommand com = new MySqlCommand(qu, dbconnection2);
                            dr2 = com.ExecuteReader();
                            if (dr2.HasRows)
                            {
                                billExist = true;
                                main.test(billnum);
                            }
                            else
                            {
                                billExist = false;
                                main.test(billnum);
                            }
                        }
                        else
                        {
                            comClient.Text = "";
                            txtPhone.Text = "";
                            comClient.Enabled = true;
                            txtPhone.Enabled = true;
                            txtClientId.Enabled = true;
                            txtClientId.Text = "";
                            billExist = false;
                            mainBillExist = false;
                            AddedToBill = false;
                            main.test(0);
                            MessageBox.Show("هذه الفاتورة غير موجودة");
                        }
                    }
                    else
                    {
                        MessageBox.Show("رقم الفاتورة يجب ان يكون عدد");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                dbconnection4.Close();
                dbconnection2.Close();
                dbconnection5.Close();
            }
        }
        private void txtPhone_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    dbconnection.Open();
                    string query = "select Customer_Name,customer.Customer_ID from customer inner join customer_phone on customer_phone.Customer_ID=customer.Customer_ID where customer_phone.Phone='" + txtPhone.Text + "'";
                    MySqlCommand com = new MySqlCommand(query, dbconnection);
                    MySqlDataReader dr = com.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            comClient.Text = dr["Customer_Name"].ToString();
                            comClient.SelectedValue = dr["Customer_ID"].ToString();
                        }
                    }
                    else
                    {
                        comClient.Text = "";
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                dbconnection.Close();
            }
        }
        private void comClient_SelectedValueChanged(object sender, EventArgs e)
        {
            if (loaded)
            {
                try
                {
                    dbconnection7.Open();
                    string query = "select customer_phone.Phone from customer inner join customer_phone on customer_phone.Customer_ID=customer.Customer_ID where customer.Customer_ID=" + comClient.SelectedValue.ToString() + " order by customer_phone.CustomerPhone_ID desc limit 1";
                    MySqlCommand com = new MySqlCommand(query, dbconnection7);
                    if (com.ExecuteScalar() != null)
                    {
                        txtPhone.Text = com.ExecuteScalar().ToString();
                        txtClientId.Text = comClient.SelectedValue.ToString();
                    }
                    else
                    {
                        txtPhone.Text = "";
                        txtClientId.Text = "";
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                dbconnection7.Close();
            }
        }
        private void txtClientId_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txtClientId.Text != "")
                {
                    try
                    {
                        dbconnection.Open();
                        string query = "select customer_phone.Phone,customer.Customer_Name from customer inner join customer_phone on customer_phone.Customer_ID=customer.Customer_ID where customer.Customer_ID=" + txtClientId.Text + " order by customer_phone.CustomerPhone_ID desc limit 1";
                        MySqlCommand com = new MySqlCommand(query, dbconnection);
                        MySqlDataReader dr = com.ExecuteReader();
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                txtPhone.Text = dr["Phone"].ToString();
                                comClient.Text = dr["Customer_Name"].ToString();
                                comClient.SelectedValue = txtClientId.Text;
                            }
                            dr.Close();
                        }
                        else
                        {
                            txtPhone.Text = "";
                            comClient.Text = "";
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    dbconnection.Close();
                }
            }
        }
        private void gridView1_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            try
            {
                row1 = gridView1.GetDataRow(gridView1.GetRowHandle(e.RowHandle));
                if (loaded)
                {
                    if (e.Column.ToString() == "الكود")
                    {
                        if (tipImage == null)
                        {
                            if (row1["الكود"].ToString().Length >= 20)
                            {
                                tipImage = new TipImage(row1["Data_ID"].ToString(), "بند");
                            }
                            else if (row1["Type"].ToString() == "طقم")
                            {
                                tipImage = new TipImage(row1["الكود"].ToString(), "طقم");
                            }
                            else if (row1["Type"].ToString() == "عرض")
                            {
                                tipImage = new TipImage(row1["الكود"].ToString(), "عرض");
                            }
                            tipImage.Location = new Point(Cursor.Position.X, Cursor.Position.Y);
                            tipImage.Show();
                        }
                        else
                        {
                            tipImage.Close();
                            if (row1["الكود"].ToString().Length >= 20)
                            {
                                tipImage = new TipImage(row1["Data_ID"].ToString(), "بند");
                            }
                            else if (row1["Type"].ToString() == "طقم")
                            {
                                tipImage = new TipImage(row1["الكود"].ToString(), "طقم");
                            }
                            else if (row1["Type"].ToString() == "عرض")
                            {
                                tipImage = new TipImage(row1["الكود"].ToString(), "عرض");
                            }
                            tipImage.Location = new Point(Cursor.Position.X, Cursor.Position.Y);
                            tipImage.Show();
                        }
                    }

                    else if (e.Column.ToString() == "الكمية")
                    {
                        if (row1["الكود"].ToString().Length >= 20)
                        {
                            StoresDetails sd = new StoresDetails(row1["Data_ID"].ToString());
                            sd.ShowDialog();
                        }
                    }

                    else if (e.Column.ToString() == "الاسم")
                    {
                        if (row1["الكود"].ToString().Length >= 20)
                        {

                        }
                        else if (row1["Type"].ToString() == "طقم")
                        {
                            ItemDetails itemD = new ItemDetails(row1["الكود"].ToString(), "طقم");
                            itemD.ShowDialog();
                        }
                        else if (row1["Type"].ToString() == "عرض")
                        {
                            ItemDetails itemD = new ItemDetails(row1["الكود"].ToString(), "عرض");
                            itemD.ShowDialog();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                int billNum = 0;
                if (txtBillNum.Text == "")
                {
                    MessageBox.Show("يجب ادخال رقم الفاتورة");
                    return;
                }

                if (int.TryParse(txtBillNum.Text, out billNum))
                { }
                else
                {
                    MessageBox.Show("رقم الفاتورة يجب ان يكون عدد");
                    return;
                }

                XtraTabPage xtraTabPage = getTabPage("tabPageProductsDetailsReport");
                MainForm.tabPageProductsDetailsReport.Name = "tabPageProductsDetailsReport";
                MainForm.tabPageProductsDetailsReport.Text = "تفاصيل فاتورة";
                MainForm.panelProductsDetailsReport.Name = "panelProductsDetailsReport";
                MainForm.panelProductsDetailsReport.Dock = DockStyle.Fill;

                MainForm.ProductsDetailsReport = new ProductsDetails_Report(main, billNum);
                MainForm.ProductsDetailsReport.Size = new Size(1109, 660);
                MainForm.ProductsDetailsReport.TopLevel = false;
                MainForm.ProductsDetailsReport.FormBorderStyle = FormBorderStyle.None;
                MainForm.ProductsDetailsReport.Dock = DockStyle.Fill;

                MainForm.panelProductsDetailsReport.Controls.Clear();
                MainForm.panelProductsDetailsReport.Controls.Add(MainForm.ProductsDetailsReport);
                MainForm.tabPageProductsDetailsReport.Controls.Add(MainForm.panelProductsDetailsReport);
                MainTabControlPointSale.TabPages.Add(MainForm.tabPageProductsDetailsReport);
                MainForm.ProductsDetailsReport.Show();
                MainTabControlPointSale.SelectedTabPage = MainForm.tabPageProductsDetailsReport;

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
                dbconnection.Open();
                if (txtBillNum.Text != "")
                {
                    int billNo;
                    if (int.TryParse(txtBillNum.Text, out billNo))
                    {
                        if (mainBillExist)
                        {
                            if (gridView1.SelectedRowsCount > 0)
                            {
                                if (gridView1.GetRowCellDisplayText(gridView1.GetSelectedRows()[0], "الكمية").ToString() != "" && gridView1.GetRowCellDisplayText(gridView1.GetSelectedRows()[0], "الكمية").ToString() != "0")
                                {
                                    if (txtQuantity.Text != "" && comStore.Text != "")
                                    {
                                        double TotalMeters = 0;
                                        if (double.TryParse(txtQuantity.Text, out TotalMeters))
                                        {
                                            if (checkQuantityInStore(TotalMeters))
                                            {
                                                cartons = 0;
                                                if (gridView1.GetRowCellDisplayText(gridView1.GetSelectedRows()[0], "الكود").Length >= 20 && Convert.ToDouble(gridView1.GetRowCellDisplayText(gridView1.GetSelectedRows()[0], "الكرتنة")) > 0)
                                                {
                                                    if (cartonNumCheck())
                                                    { }
                                                    else
                                                    {
                                                        dbconnection.Close();
                                                        return;
                                                    }
                                                }
                                                string query = "";
                                                MySqlCommand com;

                                                if (!AddedToBill)
                                                {
                                                    if (comClient.Text != "" && txtPhone.Text != "")
                                                    {
                                                        query = "select customer.Customer_ID from customer inner join customer_phone on customer_phone.Customer_ID=customer.Customer_ID where customer_phone.Phone='" + txtPhone.Text + "'";
                                                        com = new MySqlCommand(query, dbconnection);
                                                        if (com.ExecuteScalar() != null)
                                                        {
                                                            ClintID = Convert.ToInt16(com.ExecuteScalar().ToString());
                                                        }
                                                        else
                                                        {
                                                            query = "insert into customer (Customer_Name,Customer_Start,Customer_Type) values(@Customer_Name,@Customer_Start,@Customer_Type)";
                                                            com = new MySqlCommand(query, dbconnection);
                                                            com.Parameters.Add("@Customer_Name", MySqlDbType.VarChar, 255).Value = comClient.Text;
                                                            com.Parameters.Add("@Customer_Start", MySqlDbType.Date, 0).Value = DateTime.Now.Date;
                                                            com.Parameters.Add("@Customer_Type", MySqlDbType.VarChar, 255).Value = "عميل";
                                                            com.ExecuteNonQuery();

                                                            query = "select Customer_ID from customer order by Customer_ID desc limit 1";
                                                            com = new MySqlCommand(query, dbconnection);
                                                            ClintID = Convert.ToInt16(com.ExecuteScalar().ToString());
                                                            txtClientId.Text = ClintID.ToString();

                                                            query = "insert into customer_phone (Customer_ID,Phone) values(@Customer_ID,@Phone)";
                                                            com = new MySqlCommand(query, dbconnection);
                                                            com.Parameters.Add("@Customer_ID", MySqlDbType.Int16, 11).Value = ClintID;
                                                            com.Parameters.Add("@Phone", MySqlDbType.VarChar, 255).Value = txtPhone.Text;
                                                            com.ExecuteNonQuery();
                                                        }

                                                        query = "update dash set Customer_ID=@Customer_ID,Customer_Name=@Customer_Name where Bill_Number=" + billNo + " and Branch_ID=" + empBranchID + " order by Dash_ID desc limit 1";
                                                        com = new MySqlCommand(query, dbconnection);
                                                        com.Parameters.Add("@Customer_ID", MySqlDbType.Int16).Value = ClintID;
                                                        com.Parameters.Add("@Customer_Name", MySqlDbType.VarChar).Value = comClient.Text;
                                                        com.ExecuteNonQuery();
                                                        AddedToBill = true;
                                                    }
                                                    else
                                                    {
                                                        MessageBox.Show("تاكد من تحديد العميل");
                                                        dbconnection.Close();
                                                        return;
                                                    }
                                                }

                                                #region new bill
                                                if (!billExist)
                                                {
                                                    billExist = true;

                                                    string q = "select Dash_ID from dash where Branch_ID=" + empBranchID + " and Bill_Number=" + txtBillNum.Text + " order by Dash_ID desc limit 1";
                                                    MySqlCommand command = new MySqlCommand(q, dbconnection);
                                                    int dashId = Convert.ToInt16(command.ExecuteScalar().ToString());


                                                    query = "insert into dash_details (Dash_ID,Type,Data_ID,Quantity,Store_ID,Store_Name,Delegate_ID,Cartons,Emp_Type) values (@Dash_ID,@Type,@Data_ID,@Quantity,@Store_ID,@Store_Name,@Delegate_ID,@Cartons,@Emp_Type)";
                                                    com = new MySqlCommand(query, dbconnection);
                                                    com.Parameters.Add("@Dash_ID", MySqlDbType.Int16).Value = dashId;
                                                    if (gridView1.GetRowCellDisplayText(gridView1.GetSelectedRows()[0], "الكود").Length >= 20)
                                                    {
                                                        com.Parameters.Add("@Type", MySqlDbType.VarChar).Value = "بند";
                                                        com.Parameters.Add("@Data_ID", MySqlDbType.Int16).Value = gridView1.GetRowCellDisplayText(gridView1.GetSelectedRows()[0], "Data_ID");
                                                    }
                                                    else
                                                    {
                                                        com.Parameters.Add("@Type", MySqlDbType.VarChar).Value = gridView1.GetRowCellDisplayText(gridView1.GetSelectedRows()[0], "Type");
                                                        com.Parameters.Add("@Data_ID", MySqlDbType.Int16).Value = gridView1.GetRowCellDisplayText(gridView1.GetSelectedRows()[0], "الكود");
                                                    }
                                                    com.Parameters.Add("@Quantity", MySqlDbType.Decimal).Value = TotalMeters;
                                                    com.Parameters.Add("@Store_ID", MySqlDbType.Int16).Value = comStore.SelectedValue.ToString();
                                                    com.Parameters.Add("@Store_Name", MySqlDbType.VarChar).Value = comStore.Text;
                                                    com.Parameters.Add("@Emp_Type", MySqlDbType.VarChar).Value = UserControl.EmpType;
                                                    com.Parameters.Add("@Delegate_ID", MySqlDbType.Int16).Value = empID;
                                                    com.Parameters.Add("@Cartons", MySqlDbType.Int16).Value = cartons;
                                                    com.ExecuteNonQuery();

                                                    dbconnection.Close();
                                                    txtQuantity.Text = "";
                                                    comStore.Text = "";
                                                    txtRequiredQuantity.Text = "";
                                                    txtNumCartons.Text = "";
                                                    main.test(billNo);
                                                }
                                                #endregion

                                                #region bill already exist
                                                else if (billExist)
                                                {
                                                    if ((gridView1.GetRowCellDisplayText(gridView1.GetSelectedRows()[0], "Type")) == "بند")
                                                    {
                                                        string qq = "SELECT dash_details.Data_ID FROM dash_details INNER JOIN dash ON dash.Dash_ID = dash_details.Dash_ID where dash.Bill_Number=" + billNo + " and dash.Branch_ID=" + empBranchID + " and dash.Confirmed=0 and dash_details.Data_ID=" + gridView1.GetRowCellDisplayText(gridView1.GetSelectedRows()[0], "Data_ID") + " and dash_details.Type='" + row1["Type"].ToString() + "' and dash_details.Store_ID=" + comStore.SelectedValue.ToString() + " order by dash.Dash_ID desc limit 1";
                                                        MySqlCommand comm = new MySqlCommand(qq, dbconnection);
                                                        if (comm.ExecuteScalar() != null)
                                                        {
                                                            MessageBox.Show("هذا البند تم اضافتة للسلة من قبل");
                                                            dbconnection.Close();
                                                            return;
                                                        }
                                                    }
                                                    else if ((gridView1.GetRowCellDisplayText(gridView1.GetSelectedRows()[0], "Type")) == "طقم" || (gridView1.GetRowCellDisplayText(gridView1.GetSelectedRows()[0], "Type")) == "عرض")
                                                    {
                                                        string qq = "SELECT dash_details.Data_ID FROM dash_details INNER JOIN dash ON dash.Dash_ID = dash_details.Dash_ID where dash.Bill_Number=" + billNo + " and dash.Branch_ID=" + empBranchID + " and dash.Confirmed=0 and dash_details.Data_ID=" + gridView1.GetRowCellDisplayText(gridView1.GetSelectedRows()[0], "الكود") + " and dash_details.Type='" + row1["Type"].ToString() + "' and dash_details.Store_ID=" + comStore.SelectedValue.ToString() + " order by dash.Dash_ID desc limit 1";
                                                        MySqlCommand comm = new MySqlCommand(qq, dbconnection);
                                                        if (comm.ExecuteScalar() != null)
                                                        {
                                                            MessageBox.Show("هذا البند تم اضافتة للسلة من قبل");
                                                            dbconnection.Close();
                                                            return;
                                                        }
                                                    }

                                                    string q = "select Dash_ID from dash where Bill_Number=" + billNo + " and Branch_ID=" + empBranchID + "  order by Dash_ID desc limit 1";
                                                    MySqlCommand command = new MySqlCommand(q, dbconnection);
                                                    int dashId = Convert.ToInt16(command.ExecuteScalar().ToString());

                                                    query = "insert into dash_details (Dash_ID,Type,Data_ID,Quantity,Store_ID,Store_Name,Delegate_ID,Cartons,Emp_Type) values (@Dash_ID,@Type,@Data_ID,@Quantity,@Store_ID,@Store_Name,@Delegate_ID,@Cartons,@Emp_Type)";
                                                    com = new MySqlCommand(query, dbconnection);
                                                    com.Parameters.Add("@Dash_ID", MySqlDbType.Int16).Value = dashId;
                                                    if (gridView1.GetRowCellDisplayText(gridView1.GetSelectedRows()[0], "الكود").Length >= 20)
                                                    {
                                                        com.Parameters.Add("@Type", MySqlDbType.VarChar).Value = "بند";
                                                        com.Parameters.Add("@Data_ID", MySqlDbType.Int16).Value = gridView1.GetRowCellDisplayText(gridView1.GetSelectedRows()[0], "Data_ID");
                                                    }
                                                    else
                                                    {
                                                        com.Parameters.Add("@Type", MySqlDbType.VarChar).Value = gridView1.GetRowCellDisplayText(gridView1.GetSelectedRows()[0], "Type");
                                                        com.Parameters.Add("@Data_ID", MySqlDbType.Int16).Value = gridView1.GetRowCellDisplayText(gridView1.GetSelectedRows()[0], "الكود");
                                                    }
                                                    com.Parameters.Add("@Quantity", MySqlDbType.Decimal).Value = TotalMeters;
                                                    com.Parameters.Add("@Store_ID", MySqlDbType.Int16).Value = comStore.SelectedValue.ToString();
                                                    com.Parameters.Add("@Store_Name", MySqlDbType.VarChar).Value = comStore.Text;
                                                    com.Parameters.Add("@Emp_Type", MySqlDbType.VarChar).Value = UserControl.EmpType;
                                                    com.Parameters.Add("@Delegate_ID", MySqlDbType.Int16).Value = empID;
                                                    com.Parameters.Add("@Cartons", MySqlDbType.Int16).Value = cartons;
                                                    com.ExecuteNonQuery();

                                                    dbconnection.Close();
                                                    txtQuantity.Text = "";
                                                    comStore.Text = "";
                                                    txtRequiredQuantity.Text = "";
                                                    txtNumCartons.Text = "";
                                                    main.test(billNo);
                                                }
                                                #endregion

                                                if (UserControl.userType != 1)
                                                {
                                                    dbconnection.Open();
                                                    query = "SELECT delegate_customer.DelegateCustomer_ID FROM delegate_customer where delegate_customer.Delegate_ID=" + empID + " and delegate_customer.Customer_ID=" + ClintID;
                                                    com = new MySqlCommand(query, dbconnection);
                                                    if (com.ExecuteScalar() == null)
                                                    {
                                                        query = "insert into delegate_customer (Customer_ID,Delegate_ID) values(@Customer_ID,@Delegate_ID)";
                                                        com = new MySqlCommand(query, dbconnection);
                                                        com.Parameters.Add("@Customer_ID", MySqlDbType.Int16, 11).Value = ClintID;
                                                        com.Parameters.Add("@Delegate_ID", MySqlDbType.Int16, 11).Value = empID;
                                                        com.ExecuteNonQuery();
                                                    }
                                                    dbconnection.Close();
                                                }
                                            }
                                            else
                                            {
                                                MessageBox.Show("لا يوجد كمية كافية");
                                            }
                                        }
                                        else
                                        {
                                            MessageBox.Show("الكمية يجب ان تكون عدد");
                                        }
                                    }
                                    else
                                    {
                                        MessageBox.Show("برجاء ادخال الوصفات كاملة");
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("لا يوجد اى كمية من هذا العنصر");
                                }
                            }
                            else
                            {
                                MessageBox.Show("برجاء اختيار عنصر للاضافة");
                            }
                        }
                        else
                        {
                            MessageBox.Show("هذه الفاتورة غير موجودة من قبل");
                        }
                    }
                    else
                    {
                        MessageBox.Show("رقم الفاتورة يجب ان يكون عدد");
                    }
                }
                else
                {
                    MessageBox.Show("برجاء ادخال الوصفات كاملة");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }
        private void bunifuTileButtonAddSpecialOrder_Click(object sender, EventArgs e)
        {
            try
            {
                if (DashBillNum != 0 && comClient.Text != "" && txtPhone.Text != "")
                {
                    int clientIdSO = 0;

                    dbconnection.Open();
                    string query = "select customer.Customer_ID from customer inner join customer_phone on customer_phone.Customer_ID=customer.Customer_ID where customer_phone.Phone='" + txtPhone.Text + "'";
                    MySqlCommand com = new MySqlCommand(query, dbconnection);
                    if (com.ExecuteScalar() != null)
                    {
                        clientIdSO = Convert.ToInt16(com.ExecuteScalar().ToString());
                    }
                    dbconnection.Close();

                    AddSpecialOrder soForm = new AddSpecialOrder(DashBillNum, clientIdSO, empID);
                    soForm.ShowDialog();
                }
                else
                {
                    MessageBox.Show("تاكد من رقم الفاتورة ومن تحديد العميل");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }

        private void txtRequiredQuantity_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtRequiredQuantity.Text != "")
                {
                    if (row1["الكود"].ToString().Length >= 20 && Convert.ToDecimal(row1["الكرتنة"].ToString()) != 0)
                    {
                        decimal numCartn = Convert.ToDecimal(txtRequiredQuantity.Text) / Convert.ToDecimal(row1["الكرتنة"].ToString());
                        txtNumCartons.Text = decimal.Ceiling(numCartn).ToString();//"0.##"
                        txtQuantity.Text = (Convert.ToDecimal(row1["الكرتنة"].ToString()) * decimal.Ceiling(numCartn)).ToString();
                    }
                    else //if (Convert.ToDecimal(row1["الكرتنة"].ToString()) == 0)
                    {
                        txtNumCartons.Text = "0";
                        txtQuantity.Text = txtRequiredQuantity.Text;
                    }
                }
                else
                {
                    txtQuantity.Text = "";
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //function
        public void getSetsComBox()
        {
            string q1, q2, q3, q4;
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
                q3 = "select Set_ID from sets";
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

            string query3 = "select distinct  sets.Set_ID  ,Set_Name  from sets  INNER JOIN type ON type.Type_ID = sets.Type_ID INNER JOIN factory ON factory.Factory_ID = sets.Factory_ID where sets.Type_ID IN(" + q1 + ") and sets.Factory_ID IN(" + q2 + ") and sets.Set_ID IN (" + q3 + ")";
            MySqlDataAdapter da3 = new MySqlDataAdapter(query3, dbconnection);
            DataTable dt3 = new DataTable();
            da3.Fill(dt3);
            flagProduct = false;
            comProduct.DataSource = dt3;
            comProduct.DisplayMember = dt3.Columns["Set_Name"].ToString();
            comProduct.ValueMember = dt3.Columns["Set_ID"].ToString();
            comProduct.Text = "";
            txtCodeSearch4.Text = "";
        }
        public bool CheckDelegateBill(int dashID)
        {
            if (UserControl.EmpType == "مدير")
                return true;
            dbconnection.Open();
            string query = "select Delegate_ID from dash_delegate_bill where Dash_ID=" + dashID;
            MySqlCommand com = new MySqlCommand(query, dbconnection);
            MySqlDataReader dr = com.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    int Delegate_ID = Convert.ToInt16(dr[0]);
                    if (Delegate_ID == empID)
                    {
                        dbconnection.Close();
                        return true;
                    }
                }
                dr.Close();
            }
            else
            {
                dbconnection.Close();
                return false;
            }
            dbconnection.Close();
            return false;
        }
        public bool checkQuantityInStore(double totalMeter)
        {
            if (row1["Type"].ToString() == "بند")
            {
                string query = "SELECT sum(storage.Total_Meters) as 'الكمية' FROM storage where storage.Store_ID=" + comStore.SelectedValue.ToString() + " and storage.Data_ID=" + gridView1.GetRowCellDisplayText(gridView1.GetSelectedRows()[0], "Data_ID") + " group by storage.Data_ID,storage.Store_ID";
                MySqlCommand com = new MySqlCommand(query, dbconnection);
                if (com.ExecuteScalar() != null)
                {
                    double totalquant = Convert.ToDouble(com.ExecuteScalar().ToString());
                    if (totalMeter <= totalquant)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            else if (row1["Type"].ToString() == "طقم")
            {
                string query = "SELECT sum(storage.Total_Meters) as 'الكمية' FROM storage where storage.Store_ID=" + comStore.SelectedValue.ToString() + " and storage.Set_ID=" + gridView1.GetRowCellDisplayText(gridView1.GetSelectedRows()[0], "الكود") + " group by storage.Set_ID,storage.Store_ID";
                MySqlCommand com = new MySqlCommand(query, dbconnection);
                if (com.ExecuteScalar() != null)
                {
                    double totalquant = Convert.ToDouble(com.ExecuteScalar().ToString());
                    if (totalMeter <= totalquant)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            else if (row1["Type"].ToString() == "عرض")
            {
                double totalquant = Convert.ToDouble(gridView1.GetRowCellDisplayText(gridView1.GetSelectedRows()[0], "الكمية"));
                if (totalMeter <= totalquant)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }
        public bool cartonNumCheck()
        {
            try
            {
                string query = "select Carton from data where Code='" + gridView1.GetRowCellDisplayText(gridView1.GetSelectedRows()[0], "الكود") + "'";
                MySqlCommand com = new MySqlCommand(query, dbconnection);
                if (com.ExecuteScalar() != null)
                {
                    decimal Carton = Convert.ToDecimal(com.ExecuteScalar());
                    decimal totalMeters = decimal.Parse(txtQuantity.Text);
                    if (totalMeters % Carton == 0)
                    {
                        //MessageBox.Show("تحتاج " + totalMeters / Carton + " كرتونة");
                        cartons = (int)(totalMeters / Carton);
                        return true;
                    }
                    else
                    {
                        //MessageBox.Show("تحتاج " + totalMeters / Carton + " كرتونة و " + totalMeters % Carton + " متر");
                        MessageBox.Show("عدد الكراتين يجب ان يكون رقم صحيح");
                        cartons = 0;
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return false;
        }
        public XtraTabPage getTabPage(string text)
        {
            for (int i = 0; i < MainForm.tabControlPointSale.TabPages.Count; i++)
                if (MainForm.tabControlPointSale.TabPages[i].Name == text)
                {
                    return MainForm.tabControlPointSale.TabPages[i];
                }
            return null;
        }
        public void search2()
        {
            MySqlDataAdapter adapter = new MySqlDataAdapter("SELECT data.Data_ID,data.Code as 'الكود','Type',type.Type_Name as 'النوع',concat(product.Product_Name,' - ',type.Type_Name,' - ',factory.Factory_Name,' - ',groupo.Group_Name,' ',COALESCE(color.Color_Name,''),' ',COALESCE(size.Size_Value,''),' ',COALESCE(sort.Sort_Value,'')) as 'الاسم',sum(storage.Total_Meters) as 'الكمية',sellprice.Last_Price as 'السعر',sellprice.Sell_Discount as 'الخصم',sellprice.Sell_Price as 'بعد الخصم',data.Carton as 'الكرتنة' FROM data LEFT JOIN color ON color.Color_ID = data.Color_ID LEFT JOIN size ON size.Size_ID = data.Size_ID LEFT JOIN sort ON sort.Sort_ID = data.Sort_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID INNER JOIN factory ON factory.Factory_ID = data.Factory_ID  INNER JOIN product ON product.Product_ID = data.Product_ID  INNER JOIN type ON type.Type_ID = data.Type_ID  INNER JOIN sellprice ON sellprice.Data_ID = data.Data_ID LEFT JOIN storage ON storage.Data_ID = data.Data_ID where data.Data_ID=0 group by data.Data_ID", dbconnection);
            DataTable dtf = new DataTable();
            adapter.Fill(dtf);
            gridControl1.DataSource = dtf;

            dbconnection.Open();
            if (productType == "بند")
            {
                #region بند
                dbconnection6.Open();
                string query = "SELECT data.Data_ID,data.Code as 'الكود',type.Type_Name as 'النوع',concat(product.Product_Name,' ',COALESCE(color.Color_Name,''),' ',data.Description,' ',groupo.Group_Name,' ',factory.Factory_Name,' ',COALESCE(size.Size_Value,''),' ',COALESCE(sort.Sort_Value,'')) as 'الاسم',data.Carton as 'الكرتنة',sum(storage.Total_Meters) as 'الكمية' FROM data LEFT JOIN color ON color.Color_ID = data.Color_ID LEFT JOIN size ON size.Size_ID = data.Size_ID LEFT JOIN sort ON sort.Sort_ID = data.Sort_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID INNER JOIN factory ON factory.Factory_ID = data.Factory_ID  INNER JOIN product ON product.Product_ID = data.Product_ID  INNER JOIN type ON type.Type_ID = data.Type_ID LEFT JOIN storage ON storage.Data_ID = data.Data_ID where data.Data_ID=" + loadedRow["Data_ID"].ToString() + " group by data.Data_ID order by SUBSTR(data.Code,1,16),color.Color_Name,data.Description,data.Sort_ID";
                MySqlCommand comand = new MySqlCommand(query, dbconnection);
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
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns["النوع"], dr["النوع"]);
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns["الاسم"], dr["الاسم"]);
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns["Type"], "بند");
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns["الكرتنة"], dr["الكرتنة"]);
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns["الكمية"], dr["الكمية"]);

                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns["السعر"], dr2["السعر"]);
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns["الخصم"], dr2["الخصم"]);
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns["بعد الخصم"], dr2["بعد الخصم"]);
                        }
                    }
                    dr2.Close();
                }
                dr.Close();
                #endregion
            }
            else if (productType == "طقم")
            {
                #region طقم
                string query = "SELECT sets.Set_ID as 'الكود',type.Type_Name as 'النوع',sets.Set_Name as 'الاسم',sets.Description as 'الوصف' FROM sets INNER JOIN type ON type.Type_ID = sets.Type_ID INNER JOIN factory ON factory.Factory_ID = sets.Factory_ID where sets.Set_ID=" + loadedRow["الكود"].ToString();
                MySqlCommand comand = new MySqlCommand(query, dbconnection);
                MySqlDataReader dr = comand.ExecuteReader();
                while (dr.Read())
                {
                    dbconnection3.Open();
                    double price = 0;
                    double priceF = 0;
                    double totalQuanity = 0;
                    gridView1.AddNewRow();
                    int rowHandle = gridView1.GetRowHandle(gridView1.DataRowCount);
                    if (gridView1.IsNewItemRow(rowHandle))
                    {
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[1], dr["الكود"]);
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns["النوع"], dr["النوع"]);
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns["الاسم"], dr["الاسم"]);
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns["Type"], "طقم");

                        query = "SELECT sum(set_details.Quantity*sellprice.Last_Price) as 'السعر',(set_details.Quantity*sellprice.Sell_Discount) as 'الخصم',sum(set_details.Quantity*sellprice.Sell_Price) as 'بعد الخصم',sum(storage.Total_Meters)/COUNT(set_details.Data_ID) as 'الكمية' FROM sets INNER JOIN set_details ON set_details.Set_ID = sets.Set_ID INNER JOIN sellprice ON set_details.Data_ID = sellprice.Data_ID LEFT JOIN storage ON storage.Set_ID = sets.Set_ID where sets.Set_ID=" + dr["الكود"] + " group by set_details.Set_ID, storage.Set_ID order by sellprice.Date desc";
                        comand = new MySqlCommand(query, dbconnection3);
                        MySqlDataReader dr1 = comand.ExecuteReader();
                        while (dr1.Read())
                        {
                            price += Convert.ToDouble(dr1["السعر"].ToString());
                            priceF += Convert.ToDouble(dr1["بعد الخصم"].ToString());
                            if (dr1["الكمية"].ToString() != "")
                            {
                                totalQuanity += Convert.ToDouble(dr1["الكمية"].ToString());
                            }
                            else
                            {
                                totalQuanity += 0;
                            }
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns["الخصم"], dr1["الخصم"]);
                        }
                        dr1.Close();

                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns["السعر"], price);
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns["بعد الخصم"], priceF);
                        if (totalQuanity != 0)
                        {
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns["الكمية"], totalQuanity);
                        }
                        else
                        {
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns["الكمية"], "");
                        }
                    }
                    dbconnection3.Close();
                }
                dr.Close();
                #endregion
            }
            else if (productType == "عرض")
            {
                #region عرض
                string query = "select offer.Offer_ID as 'الكود',Offer_Name as 'الاسم',Price as 'السعر',sum(storage.Total_Meters) as 'الكمية' from offer LEFT JOIN storage ON storage.Offer_ID = offer.Offer_ID where offer.Offer_ID=" + loadedRow["الكود"].ToString() + " group by storage.Offer_ID";
                MySqlCommand comand = new MySqlCommand(query, dbconnection);
                MySqlDataReader dr = comand.ExecuteReader();
                while (dr.Read())
                {
                    gridView1.AddNewRow();
                    int rowHandle = gridView1.GetRowHandle(gridView1.DataRowCount);
                    if (gridView1.IsNewItemRow(rowHandle))
                    {
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[1], dr["الكود"]);
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns["الاسم"], dr["الاسم"]);
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns["Type"], "عرض");
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns["السعر"], dr["السعر"]);
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns["بعد الخصم"], dr["السعر"]);
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns["الكمية"], dr["الكمية"]);
                    }
                }
                dr.Close();
                #endregion
            }
            dbconnection.Close();
            gridView1.Columns[1].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
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











    }
}

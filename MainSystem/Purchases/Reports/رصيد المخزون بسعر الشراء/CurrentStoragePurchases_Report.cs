using DevExpress.XtraGrid;
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
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MainSystem
{
    public partial class CurrentStoragePurchases_Report : Form
    {
        MySqlConnection dbconnection, dbconnection2, dbconnection3;   
        public static XtraTabPage MainTabPagePrintingTransitions;
        Panel panelPrintingTransitions;
        public static BillsTransitions_Print bankPrint;
        public static int ID;
        public static string codef1;
        bool loaded = false;
        bool factoryFlage = false;
        bool groupFlage = false;
        bool flagProduct = false;
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
        SetPurchasesPricePopup f1;
        public CurrentStoragePurchases_Report()
        {
            InitializeComponent();
            dbconnection = new MySqlConnection(connection.connectionString);
            dbconnection2 = new MySqlConnection(connection.connectionString);
            dbconnection3 = new MySqlConnection(connection.connectionString);

            MainTabPagePrintingTransitions = new XtraTabPage();
            panelPrintingTransitions = new Panel();
            f1 = new SetPurchasesPricePopup();
        }
        private void Item_Transitions_Report_Load(object sender, EventArgs e)
        {
            try
            {
                loadBranch();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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
                                        query2 = "select * from groupo where Factory_ID=" + -Convert.ToInt32(txtCodeSearch1.Text) + " and Type_ID=" + txtCodeSearch1.Text;
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
                                else
                                {
                                    filterProduct();
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

                                /*query = "select distinct Classification from data where Factory_ID=" + txtCodeSearch2.Text;
                                da2 = new MySqlDataAdapter(query, dbconnection);
                                dt2 = new DataTable();
                                da2.Fill(dt2);
                                comClassfication.DataSource = dt2;
                                comClassfication.DisplayMember = dt2.Columns["Classification"].ToString();
                                comClassfication.Text = "";*/
                                
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
                                
                                string query3 = "select distinct  product.Product_ID  ,Product_Name  from product inner join product_factory_group on product.Product_ID=product_factory_group.Product_ID  where product_factory_group.Group_ID=" + txtCodeSearch3.Text + supQuery + "  order by product.Product_ID";
                                MySqlDataAdapter da3 = new MySqlDataAdapter(query3, dbconnection);
                                DataTable dt3 = new DataTable();
                                da3.Fill(dt3);
                                flagProduct = false;
                                comProduct.DataSource = dt3;
                                comProduct.DisplayMember = dt3.Columns["Product_Name"].ToString();
                                comProduct.ValueMember = dt3.Columns["Product_ID"].ToString();
                                
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
                                    
                                    dbconnection2.Open();
                                    query = "select data.Data_ID,data.Code as 'الكود',type.Type_Name as 'النوع',concat(product.Product_Name,' - ',type.Type_Name,' - ',factory.Factory_Name,' - ',groupo.Group_Name,' ',COALESCE(color.Color_Name,''),' ',COALESCE(size.Size_Value,''),' ',COALESCE(sort.Sort_Value,'')) as 'الاسم',data.Carton as 'الكرتنة',sum(storage.Total_Meters) as 'الكمية' FROM data LEFT JOIN color ON color.Color_ID = data.Color_ID LEFT JOIN size ON size.Size_ID = data.Size_ID LEFT JOIN sort ON sort.Sort_ID = data.Sort_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID INNER JOIN factory ON factory.Factory_ID = data.Factory_ID  INNER JOIN product ON product.Product_ID = data.Product_ID  INNER JOIN type ON type.Type_ID = data.Type_ID  left JOIN sellprice ON sellprice.Data_ID = data.Data_ID LEFT JOIN storage ON storage.Data_ID = data.Data_ID where data.Data_ID=0 group by data.Data_ID";
                                    MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                                    DataTable dt = new DataTable();
                                    da.Fill(dt);
                                    gridControl1.DataSource = dt;
                                    gridView1.Columns["Data_ID"].Visible = false;

                                    txtSafy.Text = "0";

                                    dbconnection.Close();
                                    dbconnection.Open();
                                    dbconnection3.Open();
                                    //date(storage.Storage_Date) between '2019-10-30' and '" + dateTimePicker2.Value.ToString("yyyy-MM-dd") + "' and
                                    query = "SELECT data.Data_ID,data.Code as 'الكود',type.Type_Name as 'النوع',concat(product.Product_Name,' ',COALESCE(color.Color_Name,''),' ',data.Description,' ',groupo.Group_Name,' ',factory.Factory_Name,' ',COALESCE(size.Size_Value,''),' ',COALESCE(sort.Sort_Value,'')) as 'الاسم',data.Carton as 'الكرتنة',sum(storage.Total_Meters) as 'الكمية' FROM data LEFT JOIN color ON color.Color_ID = data.Color_ID LEFT JOIN size ON size.Size_ID = data.Size_ID LEFT JOIN sort ON sort.Sort_ID = data.Sort_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID INNER JOIN factory ON factory.Factory_ID = data.Factory_ID  INNER JOIN product ON product.Product_ID = data.Product_ID  INNER JOIN type ON type.Type_ID = data.Type_ID  LEFT JOIN storage ON storage.Data_ID = data.Data_ID where data.Code like '" + code1 + code2 + code3 + code4 + "%' group by data.Data_ID order by SUBSTR(data.Code,1,16),color.Color_Name,data.Description,data.Sort_ID";
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
                                            
                                            double quantti = 0;
                                            if (dr["الكمية"].ToString() != "")
                                            {
                                               // quantti = searchSelectedItem(dr["Data_ID"].ToString());
                                                gridView1.SetRowCellValue(rowHandle, gridView1.Columns["الكمية"], Math.Round((Double)quantti, 2));
                                            }
                                        }
                                    }
                                    dr.Close();

                                    if (gridView1.IsLastVisibleRow)
                                    {
                                        gridView1.FocusedRowHandle = gridView1.RowCount - 1;
                                    }

                                    double safyi = 0;
                                    for (int i = 0; i < gridView1.RowCount; i++)
                                    {
                                        double pricee = 0;
                                        if (gridView1.GetRowCellDisplayText(i, gridView1.Columns["الكمية"]) != "")
                                        {
                                            pricee = Convert.ToDouble(gridView1.GetRowCellDisplayText(i, gridView1.Columns["الكمية"]));
                                        }
                                        safyi += pricee;
                                    }
                                    txtSafy.Text = safyi.ToString("#.00");
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
                dbconnection2.Close();
                dbconnection3.Close();
            }
        }
        private void txtCodeSearch_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    dbconnection.Open();
                    dbconnection2.Open();
                    dbconnection3.Open();
                    string query = "select data.Data_ID,data.Code as 'الكود',type.Type_Name as 'النوع',concat(product.Product_Name,' - ',type.Type_Name,' - ',factory.Factory_Name,' - ',groupo.Group_Name,' ',COALESCE(color.Color_Name,''),' ',COALESCE(size.Size_Value,''),' ',COALESCE(sort.Sort_Value,'')) as 'الاسم',data.Carton as 'الكرتنة',sum(storage.Total_Meters) as 'الكمية' FROM data LEFT JOIN color ON color.Color_ID = data.Color_ID LEFT JOIN size ON size.Size_ID = data.Size_ID LEFT JOIN sort ON sort.Sort_ID = data.Sort_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID INNER JOIN factory ON factory.Factory_ID = data.Factory_ID  INNER JOIN product ON product.Product_ID = data.Product_ID  INNER JOIN type ON type.Type_ID = data.Type_ID  left JOIN sellprice ON sellprice.Data_ID = data.Data_ID LEFT JOIN storage ON storage.Data_ID = data.Data_ID where data.Data_ID=0 group by data.Data_ID";
                    MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    gridControl1.DataSource = dt;
                    gridView1.Columns["Data_ID"].Visible = false;

                    txtSafy.Text = "0";

                    //date(storage.Storage_Date) between '2019-10-30' and '" + dateTimePicker2.Value.ToString("yyyy-MM-dd") + "' and
                    query = "SELECT data.Data_ID,data.Code as 'الكود',type.Type_Name as 'النوع',concat(product.Product_Name,' ',COALESCE(color.Color_Name,''),' ',data.Description,' ',groupo.Group_Name,' ',factory.Factory_Name,' ',COALESCE(size.Size_Value,''),' ',COALESCE(sort.Sort_Value,'')) as 'الاسم',data.Carton as 'الكرتنة',sum(storage.Total_Meters) as 'الكمية' FROM data LEFT JOIN color ON color.Color_ID = data.Color_ID LEFT JOIN size ON size.Size_ID = data.Size_ID LEFT JOIN sort ON sort.Sort_ID = data.Sort_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID INNER JOIN factory ON factory.Factory_ID = data.Factory_ID  INNER JOIN product ON product.Product_ID = data.Product_ID  INNER JOIN type ON type.Type_ID = data.Type_ID  LEFT JOIN storage ON storage.Data_ID = data.Data_ID where data.Code='" + code1 + code2 + code3 + code4 + code5 + "' group by data.Data_ID order by SUBSTR(data.Code,1,16),color.Color_Name,data.Description,data.Sort_ID";
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
                            
                            double quantti = 0;
                            if (dr["الكمية"].ToString() != "")
                            {
                              //  quantti = searchSelectedItem(dr["Data_ID"].ToString());
                                gridView1.SetRowCellValue(rowHandle, gridView1.Columns["الكمية"], Math.Round((Double)quantti, 2));
                            }
                        }
                    }
                    dr.Close();
                    
                    if (gridView1.IsLastVisibleRow)
                    {
                        gridView1.FocusedRowHandle = gridView1.RowCount - 1;
                    }

                    double safyi = 0;
                    for (int i = 0; i < gridView1.RowCount; i++)
                    {
                        double pricee = 0;
                        if (gridView1.GetRowCellDisplayText(i, gridView1.Columns["الكمية"]) != "")
                        {
                            pricee = Convert.ToDouble(gridView1.GetRowCellDisplayText(i, gridView1.Columns["الكمية"]));
                        }
                        safyi += pricee;
                    }
                    txtSafy.Text = safyi.ToString("#.00");
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
        private void gridView1_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            try
            {
                if (e.Column.Name == "colالكود")
                {
                    dbconnection.Open();
                    string code = e.CellValue.ToString();
                    string query = "select Data_ID from data where Code='" + code + "'";
                    codef1 = code;
                    MySqlCommand com = new MySqlCommand(query, dbconnection);
                    ID = Convert.ToInt16(com.ExecuteScalar());
                    if (f1.ActiveControl != null)
                    {
                        f1.Show();
                        f1.Focus();
                    }
                    else
                    {
                        f1 = new SetPurchasesPricePopup();
                        f1.Show();
                        f1.Focus();
                    }
                    DataRowView row = (DataRowView)gridView1.GetRow(e.RowHandle);
                    SetPurchasesPricePopup.setData(f1, row);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }
        private void comStore_SelectedValueChanged(object sender, EventArgs e)
        {
            if (loaded)
            {
                try
                {
                    gridControl1.DataSource = null;
                    txtSafy.Text = "0";
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                gridControl1.DataSource = null;
                txtSafy.Text = "0";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void labSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (tableLayoutPanel1.RowStyles[0].Height == 130)
                {
                    tableLayoutPanel1.RowStyles[0].Height = 200;
                }
                else
                {
                    tableLayoutPanel1.RowStyles[0].Height = 130;
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
                newChoose();
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
                gridControl1.DataSource = null;
                
                txtSafy.Text = "0";
                searchItems();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
            dbconnection2.Close();
            dbconnection3.Close();
        }
        private void btnReport_Click(object sender, EventArgs e)
        {
            try
            {
                double totale = 0;
                double totalePur = 0;
                List<Items_StorageWithPurshasesPrice> bi = new List<Items_StorageWithPurshasesPrice>();

                for (int i = 0; i < gridView1.RowCount; i++)
                {
                    double quantti = 0;
                    double totalPurchases = 0;
                    if (gridView1.GetRowCellDisplayText(i, gridView1.Columns["الكمية"]) != "")
                    {
                        quantti = Convert.ToDouble(gridView1.GetRowCellDisplayText(i, gridView1.Columns["الكمية"]));
                        totalPurchases = Convert.ToDouble(gridView1.GetRowCellDisplayText(i, gridView1.Columns["الاجمالي"]));
                    }

                    totale += quantti;
                    totalePur += totalPurchases;
                    Items_StorageWithPurshasesPrice item = new Items_StorageWithPurshasesPrice() { Code = gridView1.GetRowCellDisplayText(i, gridView1.Columns["الكود"]), Product_Type = gridView1.GetRowCellDisplayText(i, gridView1.Columns["النوع"]), Product_Name = gridView1.GetRowCellDisplayText(i, gridView1.Columns["الاسم"]), Quantity = quantti, Carton = gridView1.GetRowCellDisplayText(i, gridView1.Columns["الكرتنة"]),Purchases_Price= gridView1.GetRowCellDisplayText(i, gridView1.Columns["سعر الشراء"]),Total= gridView1.GetRowCellDisplayText(i, gridView1.Columns["الاجمالي"]) };
                    bi.Add(item);
                }

                Report_Items_Storage f = new Report_Items_Storage();
                f.PrintInvoice(comStore.Text, DateTime.Now.Date, totale, totalePur, bi);
                f.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //functions
        public void newChoose()
        {
            comProduct.Text = "";
            comType.Text = "";
            comFactory.Text = "";
            comGroup.Text = "";
            comColor.Text = "";
            comSize.Text = "";
            comSort.Text = "";
            //comClassfication.Text = "";

            txtCodeSearch1.Text = "";
            txtCodeSearch2.Text = "";
            txtCodeSearch3.Text = "";
            txtCodeSearch4.Text = "";
            txtCodeSearch5.Text = "";
            txtColor.Text = "";
            txtSize.Text = "";
            txtSort.Text = "";

            gridControl1.DataSource = null;
            txtSafy.Text = "0";
        }
        private void loadBranch()
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
            comStore.SelectedIndex = -1;

            loaded = true;
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
                    string query = "select distinct product.Product_ID,Product_Name from product inner join product_factory_group on product.Product_ID=product_factory_group.Product_ID where " + supQuery + " order by product.Product_ID";
                    MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    comProduct.DataSource = dt;
                    comProduct.DisplayMember = dt.Columns["Product_Name"].ToString();
                    comProduct.ValueMember = dt.Columns["Product_ID"].ToString();
                    comProduct.Text = "";
                }
            }
        }
        public void searchItems()
        {
            if (comType.Text != "" && comFactory.Text != "")
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
                dbconnection2.Open();
                dbconnection3.Open();


                // string query = "select data.Data_ID,data.Code as 'الكود',type.Type_Name as 'النوع',concat(product.Product_Name,' - ',type.Type_Name,' - ',factory.Factory_Name,' - ',groupo.Group_Name,' ',COALESCE(color.Color_Name,''),' ',COALESCE(size.Size_Value,''),' ',COALESCE(sort.Sort_Value,'')) as 'الاسم',data.Carton as 'الكرتنة',sum(storage.Total_Meters) as 'الكمية', Purchasing_Price as 'سعر الشراء',storage.Total_Meters as 'الاجمالي' FROM data LEFT JOIN color ON color.Color_ID = data.Color_ID LEFT JOIN size ON size.Size_ID = data.Size_ID LEFT JOIN sort ON sort.Sort_ID = data.Sort_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID INNER JOIN factory ON factory.Factory_ID = data.Factory_ID  INNER JOIN product ON product.Product_ID = data.Product_ID  INNER JOIN type ON type.Type_ID = data.Type_ID  left JOIN purchasing_price ON purchasing_price.Data_ID = data.Data_ID LEFT JOIN storage ON storage.Data_ID = data.Data_ID where  data.Type_ID IN(" + q1 + ") and  data.Factory_ID  IN(" + q2 + ") and data.Group_ID IN (" + q4 + ") and data.Data_ID=0 group by data.Data_ID";
                //  string query = "SELECT data.Data_ID,data.Code as 'الكود',type.Type_Name as 'النوع',concat(product.Product_Name,' ',COALESCE(color.Color_Name,''),' ',data.Description,' ',groupo.Group_Name,' ',factory.Factory_Name,' ',COALESCE(size.Size_Value,''),' ',COALESCE(sort.Sort_Value,'')) as 'الاسم',data.Carton as 'الكرتنة',sum(storage.Total_Meters) as 'الكمية',Purchasing_Price as 'سعر الشراء',(sum(storage.Total_Meters)*Purchasing_Price) as 'الاجمالي' FROM data LEFT JOIN color ON color.Color_ID = data.Color_ID LEFT JOIN size ON size.Size_ID = data.Size_ID LEFT JOIN sort ON sort.Sort_ID = data.Sort_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID INNER JOIN factory ON factory.Factory_ID = data.Factory_ID  INNER JOIN product ON product.Product_ID = data.Product_ID  INNER JOIN type ON type.Type_ID = data.Type_ID left JOIN purchasing_price ON purchasing_price.Data_ID = data.Data_ID  LEFT JOIN storage ON storage.Data_ID = data.Data_ID where data.Type_ID IN(" + q1 + ") and data.Factory_ID IN(" + q2 + ") and data.Product_ID IN (" + q3 + ") and data.Group_ID IN (" + q4 + ") " + fQuery + " group by data.Data_ID order by SUBSTR(data.Code,1,16),color.Color_Name,data.Description,data.Sort_ID";
                string query = "SELECT data.Data_ID,data.Code as 'الكود',type.Type_Name as 'النوع',concat(product.Product_Name,' ',COALESCE(color.Color_Name,''),' ',data.Description,' ',groupo.Group_Name,' ',factory.Factory_Name,' ',COALESCE(size.Size_Value,''),' ',COALESCE(sort.Sort_Value,'')) as 'الاسم',data.Carton as 'الكرتنة' FROM data LEFT JOIN color ON color.Color_ID = data.Color_ID LEFT JOIN size ON size.Size_ID = data.Size_ID LEFT JOIN sort ON sort.Sort_ID = data.Sort_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID INNER JOIN factory ON factory.Factory_ID = data.Factory_ID  INNER JOIN product ON product.Product_ID = data.Product_ID  INNER JOIN type ON type.Type_ID = data.Type_ID   where data.Type_ID IN(" + q1 + ") and data.Factory_ID IN(" + q2 + ") and data.Product_ID IN (" + q3 + ") and data.Group_ID IN (" + q4 + ") " + fQuery + " group by data.Data_ID order by SUBSTR(data.Code,1,16),color.Color_Name,data.Description,data.Sort_ID";
                //MySqlCommand com = new MySqlCommand(query, dbconnection);
                //MySqlDataReader dr = com.ExecuteReader();
                MySqlDataAdapter da = new MySqlDataAdapter(query,dbconnection);
                DataTable dt = peraperDataTable();
                //while (dr.Read())
                //{
                //    DataRow row = dt.NewRow();
                //    row[0] = Convert.ToInt16(dr[0]);
                //    row[1] = dr[1].ToString();
                //    row[2] = dr[2].ToString();
                //    row[3] = dr[3].ToString();
                //    row[4] = dr[4].ToString();
                //    row[5] = searchSelectedItem(dr[0].ToString());
                //    row[6] = getPurchasesPrice(dr[0].ToString());
                //    row[7] =Convert.ToDecimal(row[5]) * Convert.ToDecimal(row[6]);
                //    dt.Rows.Add(row);
                //}
                //dr.Close();
                 da.Fill(dt);
                gridControl1.DataSource = dt;
                gridView1.Columns["Data_ID"].Visible = false;

                gridView1.Columns["الاجمالي"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
                gridView1.Columns["الاجمالي"].SummaryItem.DisplayFormat = "{0:n2}";
                gridView1.Columns["الكمية"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
                gridView1.Columns["الكمية"].SummaryItem.DisplayFormat = "{0:n2}";

                //if (gridView1.IsLastVisibleRow)
                //{
                //    gridView1.FocusedRowHandle = gridView1.RowCount - 1;
                //}

                //double safyi = 0;
                //double safyi2 = 0;
                //for (int i = 0; i < gridView1.RowCount; i++)
                //{
                //    double pricee = 0;
                //    double pricee2 = 0;
                //    string g = "";
                //    if (gridView1.GetRowCellDisplayText(i, gridView1.Columns["الكمية"]) != "")
                //    {
                //        if(gridView1.GetRowCellDisplayText(i, gridView1.Columns["الكمية"]).ToString() != "")
                //            pricee = Convert.ToDouble(gridView1.GetRowCellDisplayText(i, gridView1.Columns["الكمية"]));

                //        if (gridView1.GetRowCellDisplayText(i, gridView1.Columns["الاجمالي"]).ToString() != "")
                //            pricee2 = Convert.ToDouble(gridView1.GetRowCellDisplayText(i, gridView1.Columns["الاجمالي"]));
                //    }
                //    safyi += pricee;
                //    safyi2 += pricee2;
                //}
                //txtSafy.Text = safyi.ToString("#.00");
                //textBox1.Text = safyi2.ToString("#.00");

            }
            else
            {
                gridControl1.DataSource = null;
                MessageBox.Show("يجب اختيار النوع والمصنع على الاقل");
            }
        }

        public DataTable peraperDataTable()
        {
            DataTable _Table = new DataTable("Table");

            _Table.Columns.Add(new DataColumn("Data_ID", typeof(int)));
            _Table.Columns.Add(new DataColumn("الكود", typeof(string)));
            _Table.Columns.Add(new DataColumn("النوع", typeof(string)));
            _Table.Columns.Add(new DataColumn("البند", typeof(string)));
            _Table.Columns.Add(new DataColumn("الكرتنة", typeof(string)));
            _Table.Columns.Add(new DataColumn("الكمية", typeof(decimal)));
            _Table.Columns.Add(new DataColumn("سعر الشراء", typeof(decimal)));
            _Table.Columns.Add(new DataColumn("الاجمالي", typeof(decimal)));
            return _Table;
        }

    }
}

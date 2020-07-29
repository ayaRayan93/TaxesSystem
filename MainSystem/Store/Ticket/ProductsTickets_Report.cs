﻿using DevExpress.Utils;
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

namespace TaxesSystem
{
    public partial class ProductsTickets_Report : Form
    {
        MySqlConnection dbconnection, connectionReader;
        MySqlConnection dbconnection2;
        MySqlConnection dbconnection3;
        MySqlConnection dbconnection4;
        MySqlConnection dbconnection5;
        MySqlConnection dbconnection6;
        XtraTabControl MainTabControlPointSale;
        bool loaded = false;
        bool factoryFlage = false;
        bool groupFlage = false;
        bool flagProduct = false;
        XtraTabPage xtraTabPage;

        //public static XtraTabPage MainTabPageUpdatePSDetails;
        //Panel panelUpdatePSDetails;

        public static GridControl gridcontrol;
        
        List<string> arr;
        MainForm main;
        DataRow row1;
        public static TipImage tipImage = null;
        int EmpBranchId = 0;
        //int DashBillNum = 0;

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

        public ProductsTickets_Report(MainForm Min)
        {
            InitializeComponent();
            dbconnection = new MySqlConnection(connection.connectionString);
            dbconnection2 = new MySqlConnection(connection.connectionString);
            dbconnection3 = new MySqlConnection(connection.connectionString);
            dbconnection4 = new MySqlConnection(connection.connectionString);
            dbconnection5 = new MySqlConnection(connection.connectionString);
            dbconnection6 = new MySqlConnection(connection.connectionString);
            connectionReader = new MySqlConnection(connection.connectionString);
            //list_product = new List<Product>();
            MainTabControlPointSale = MainForm.tabControlPointSale;

            xtraTabPage = new XtraTabPage();

            gridcontrol = gridControl1;
            arr = new List<string>();

            comType.AutoCompleteMode = AutoCompleteMode.Suggest;
            comType.AutoCompleteSource = AutoCompleteSource.ListItems;
            comFactory.AutoCompleteMode = AutoCompleteMode.Suggest;
            comFactory.AutoCompleteSource = AutoCompleteSource.ListItems;
            comGroup.AutoCompleteMode = AutoCompleteMode.Suggest;
            comGroup.AutoCompleteSource = AutoCompleteSource.ListItems;
            comProduct.AutoCompleteMode = AutoCompleteMode.Suggest;
            comProduct.AutoCompleteSource = AutoCompleteSource.ListItems;
            
            main = Min;

            panel2.AutoScroll = false;
            panel2.VerticalScroll.Enabled = false;
            panel2.VerticalScroll.Visible = false;
            panel2.VerticalScroll.Maximum = 0;
            panel2.AutoScroll = true;
        }

        private void SearchProduct_Load(object sender, EventArgs e)
        {
            try
            {
                BaseData.generateBaseProjectFile();
                EmpBranchId = Convert.ToInt32(BaseData.BranchID);
                search();
                search2();
                foreach (GridColumn column in gridView1.Columns)
                    column.OptionsColumn.AllowSort = DefaultBoolean.False;
                loaded = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }

        /*private void textBox_Click(object sender, EventArgs e)
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

        }*/

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

                dbconnection.Open();
                dbconnection6.Open();
                //AND size.Factory_ID = factory.Factory_ID AND groupo.Factory_ID = factory.Factory_ID  AND product.Group_ID = groupo.Group_ID  AND factory.Type_ID = type.Type_ID AND color.Type_ID = type.Type_ID
                string query = "select data.Data_ID,data.Code as 'الكود','Type',type.Type_Name as 'النوع',concat(product.Product_Name,' - ',type.Type_Name,' - ',factory.Factory_Name,' - ',groupo.Group_Name,' ',COALESCE(color.Color_Name,''),' ',COALESCE(size.Size_Value,''),' ',COALESCE(sort.Sort_Value,'')) as 'الاسم',data.Carton as 'الكرتنة',sellprice.Last_Price as 'السعر',sellprice.Sell_Discount as 'الخصم',sellprice.Sell_Price as 'بعد الخصم',sum(storage.Total_Meters) as 'الكمية',sellprice.Price_Type FROM data LEFT JOIN color ON color.Color_ID = data.Color_ID LEFT JOIN size ON size.Size_ID = data.Size_ID LEFT JOIN sort ON sort.Sort_ID = data.Sort_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID INNER JOIN factory ON factory.Factory_ID = data.Factory_ID  INNER JOIN product ON product.Product_ID = data.Product_ID  INNER JOIN type ON type.Type_ID = data.Type_ID  INNER JOIN sellprice ON sellprice.Data_ID = data.Data_ID LEFT JOIN storage ON storage.Data_ID = data.Data_ID where  data.Type_ID IN(" + q1 + ") and  data.Factory_ID  IN(" + q2 + ") and data.Group_ID IN (" + q4 + ") and data.Data_ID=0 group by data.Data_ID";
                MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                DataTable dt = new DataTable();
                da.Fill(dt);
                gridControl1.DataSource = dt;

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

        private void btnSearchAll_Click(object sender, EventArgs e)
        {
            try
            {
                //"SELECT data.Data_ID,data.Code as 'الكود',product.Product_Name as 'المنتج',type.Type_Name 'النوع',factory.Factory_Name as 'المصنع',groupo.Group_Name as 'المجموعة',color.Color_Name as 'اللون',size.Size_Value as 'المقاس',sort.Sort_Value as 'الفرز',data.Classification as '',data.Carton as 'الكرتنة',price.Price as 'السعر',sum(storage.Total_Meters) as 'الكمية' FROM data INNER JOIN color ON color.Color_ID = data.Color_ID INNER JOIN size ON size.Size_ID = data.Size_ID INNER JOIN sort ON sort.Sort_ID = data.Sort_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID INNER JOIN factory ON factory.Factory_ID = data.Factory_ID AND size.Factory_ID = factory.Factory_ID AND groupo.Factory_ID = factory.Factory_ID INNER JOIN product ON product.Product_ID = data.Product_ID AND product.Group_ID = groupo.Group_ID INNER JOIN type ON type.Type_ID = data.Type_ID AND factory.Type_ID = type.Type_ID AND color.Type_ID = type.Type_ID INNER JOIN price ON price.Code = data.Code LEFT JOIN storage ON storage.Code = data.Code group by storage.Code", dbconnection);

                MySqlDataAdapter adapter = new MySqlDataAdapter("SELECT data.Data_ID,data.Code as 'الكود','Type',type.Type_Name as 'النوع',product.Product_Name as 'الاسم',data.Carton as 'الكرتنة',sellprice.Last_Price as 'السعر',sellprice.Sell_Discount as 'الخصم',sellprice.Sell_Price as 'بعد الخصم',sum(storage.Total_Meters) as 'الكمية' FROM data LEFT JOIN color ON color.Color_ID = data.Color_ID LEFT JOIN size ON size.Size_ID = data.Size_ID LEFT JOIN sort ON sort.Sort_ID = data.Sort_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID INNER JOIN factory ON factory.Factory_ID = data.Factory_ID  INNER JOIN product ON product.Product_ID = data.Product_ID  INNER JOIN type ON type.Type_ID = data.Type_ID  INNER JOIN sellprice ON sellprice.Data_ID = data.Data_ID LEFT JOIN storage ON storage.Data_ID = data.Data_ID where data.Data_ID=0 group by data.Data_ID", dbconnection);
                DataTable dtf = new DataTable();
                adapter.Fill(dtf);
                gridControl1.DataSource = dtf;

                dbconnection.Open();
                dbconnection6.Open();
                #region بند
                string query = "SELECT data.Data_ID,data.Code as 'الكود',type.Type_Name as 'النوع',concat(product.Product_Name,' ',COALESCE(color.Color_Name,''),' ',data.Description,' ',groupo.Group_Name,' ',factory.Factory_Name,' ',COALESCE(size.Size_Value,''),' ',COALESCE(sort.Sort_Value,'')) as 'الاسم',data.Carton as 'الكرتنة',sum(storage.Total_Meters) as 'الكمية' FROM data LEFT JOIN color ON color.Color_ID = data.Color_ID LEFT JOIN size ON size.Size_ID = data.Size_ID LEFT JOIN sort ON sort.Sort_ID = data.Sort_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID INNER JOIN factory ON factory.Factory_ID = data.Factory_ID  INNER JOIN product ON product.Product_ID = data.Product_ID  INNER JOIN type ON type.Type_ID = data.Type_ID LEFT JOIN storage ON storage.Data_ID = data.Data_ID group by data.Data_ID order by SUBSTR(data.Code,1,16),color.Color_Name,data.Description,data.Sort_ID";
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

                #region طقم
                query = "SELECT sets.Set_ID as 'الكود',sets.Set_Name as 'الاسم' FROM sets INNER JOIN type ON type.Type_ID = sets.Type_ID INNER JOIN factory ON factory.Factory_ID = sets.Factory_ID";
                comand = new MySqlCommand(query, dbconnection);
                dr = comand.ExecuteReader();
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
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns["الاسم"], dr["الاسم"]);
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns["Type"], "طقم");

                        query = "SELECT sum(set_details.Quantity*sellprice.Last_Price) as 'السعر',(sellprice.Sell_Discount) as 'الخصم',sum(set_details.Quantity*sellprice.Sell_Price) as 'بعد الخصم' FROM sets INNER JOIN set_details ON set_details.Set_ID = sets.Set_ID INNER JOIN sellprice ON set_details.Data_ID = sellprice.Data_ID where sets.Set_ID=" + dr["الكود"] + " group by sets.Set_ID order by sellprice.Date desc";
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
                #endregion

                #region عرض
                query = "select offer.Offer_ID as 'الكود',Offer_Name as 'الاسم',Price as 'السعر',sum(storage.Total_Meters) as 'الكمية' from offer LEFT JOIN storage ON storage.Offer_ID = offer.Offer_ID group by storage.Offer_ID";
                comand = new MySqlCommand(query, dbconnection);
                dr = comand.ExecuteReader();
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

                dbconnection.Close();

                /*gridView1.Columns[1].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
                gridView1.Columns[1].Width = 150;*/
                gridView1.Columns[0].Visible = false;
                gridView1.Columns["Type"].Visible = false;
                /*for (int i = 2; i < gridView1.Columns.Count; i++)
                {
                    gridView1.Columns[i].Width = 100;
                }*/
                //gridView1.Columns["الفرز"].Width = 50;
                /*gridView1.Columns["الكرتنة"].Width = 60;*/
                //gridView1.Columns["بعد الخصم"].Width = 130;
                //gridView1.Columns["الكمية"].Width = 120;

                if (gridView1.IsLastVisibleRow)
                {
                    gridView1.FocusedRowHandle = gridView1.RowCount - 1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection3.Close();
            dbconnection.Close();
            dbconnection6.Close();
        }

        private void txtCodeSearch_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    dbconnection.Open();
                    dbconnection6.Open();
                    string query = "select data.Data_ID,data.Code as 'الكود','Type',type.Type_Name as 'النوع',concat(product.Product_Name,' - ',type.Type_Name,' - ',factory.Factory_Name,' - ',groupo.Group_Name,' ',COALESCE(color.Color_Name,''),' ',COALESCE(size.Size_Value,''),' ',COALESCE(sort.Sort_Value,'')) as 'الاسم',data.Carton as 'الكرتنة',sellprice.Last_Price as 'السعر',sellprice.Sell_Discount as 'الخصم',sellprice.Sell_Price as 'بعد الخصم',sum(storage.Total_Meters) as 'الكمية' FROM data LEFT JOIN color ON color.Color_ID = data.Color_ID LEFT JOIN size ON size.Size_ID = data.Size_ID LEFT JOIN sort ON sort.Sort_ID = data.Sort_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID INNER JOIN factory ON factory.Factory_ID = data.Factory_ID  INNER JOIN product ON product.Product_ID = data.Product_ID  INNER JOIN type ON type.Type_ID = data.Type_ID  INNER JOIN sellprice ON sellprice.Data_ID = data.Data_ID LEFT JOIN storage ON storage.Data_ID = data.Data_ID where data.Code='" + code1 + code2 + code3 + code4 + code5 + "' and data.Data_ID=0 group by data.Data_ID";
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
                                string query = "select * from factory inner join type_factory on factory.Factory_ID=type_factory.Factory_ID inner join type on type_factory.Type_ID=type.Type_ID where type_factory.Type_ID=" + comType.SelectedValue.ToString();
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
                                        query2 = "select * from groupo where Factory_ID=" + -Convert.ToInt32(comType.SelectedValue.ToString()) + " and Type_ID=" + comType.SelectedValue.ToString();
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
                                txtCodeSearch2.Text = comFactory.SelectedValue.ToString();
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
                                    txtCodeSearch3.Text = "";
                                }
                                else
                                {
                                    filterProduct();
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
                                txtCodeSearch3.Text = comGroup.SelectedValue.ToString();
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
                                //flagProduct = false;
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
                                txtCodeSearch4.Text = "";
                                flagProduct = true;
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
                            comSize.Focus();
                            break;

                        case "comSize":
                            break;

                        case "comSort":
                            break;
                    }
                }
            }
            catch (Exception ex)
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

                                    query = "select data.Data_ID,data.Code as 'الكود','Type',type.Type_Name as 'النوع',concat(product.Product_Name,' - ',type.Type_Name,' - ',factory.Factory_Name,' - ',groupo.Group_Name,' ',COALESCE(color.Color_Name,''),' ',COALESCE(size.Size_Value,''),' ',COALESCE(sort.Sort_Value,'')) as 'الاسم',data.Carton as 'الكرتنة',sellprice.Last_Price as 'السعر',sellprice.Sell_Discount as 'الخصم',sellprice.Sell_Price as 'بعد الخصم',sum(storage.Total_Meters) as 'الكمية' FROM data LEFT JOIN color ON color.Color_ID = data.Color_ID LEFT JOIN size ON size.Size_ID = data.Size_ID LEFT JOIN sort ON sort.Sort_ID = data.Sort_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID INNER JOIN factory ON factory.Factory_ID = data.Factory_ID  INNER JOIN product ON product.Product_ID = data.Product_ID  INNER JOIN type ON type.Type_ID = data.Type_ID  INNER JOIN sellprice ON sellprice.Data_ID = data.Data_ID LEFT JOIN storage ON storage.Data_ID = data.Data_ID where data.Code like '" + code1 + code2 + code3 + code4 + "%' and data.Data_ID=0 group by data.Data_ID";
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
                        }

                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                dbconnection.Close();
                dbconnection6.Close();
            }
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

        private void gridView1_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            try
            {
                row1 = gridView1.GetDataRow(gridView1.GetRowHandle(e.RowHandle));
                //row1.Selected = true;
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

        //functions
        public void search()
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
            //txtType.Text = "";
            
            query = "select * from sort";
            da = new MySqlDataAdapter(query, dbconnection);
            dt = new DataTable();
            da.Fill(dt);
            comSort.DataSource = dt;
            comSort.DisplayMember = dt.Columns["Sort_Value"].ToString();
            comSort.ValueMember = dt.Columns["Sort_ID"].ToString();
            comSort.Text = "";
            
            //search2();
            dbconnection.Close();
        }
        public void search2()
        {
            MySqlDataAdapter adapter = new MySqlDataAdapter("SELECT data.Data_ID,data.Code as 'الكود','Type',type.Type_Name as 'النوع',concat(product.Product_Name,' ',COALESCE(color.Color_Name,''),' ',data.Description,' ',groupo.Group_Name,' ',factory.Factory_Name,' ',COALESCE(size.Size_Value,''),' ',COALESCE(sort.Sort_Value,'')) as 'الاسم',data.Carton as 'الكرتنة',sum(storage.Total_Meters) as 'الكمية',sellprice.Last_Price as 'السعر',sellprice.Sell_Discount as 'الخصم',sellprice.Sell_Price as 'بعد الخصم' FROM data LEFT JOIN color ON color.Color_ID = data.Color_ID LEFT JOIN size ON size.Size_ID = data.Size_ID LEFT JOIN sort ON sort.Sort_ID = data.Sort_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID INNER JOIN factory ON factory.Factory_ID = data.Factory_ID  INNER JOIN product ON product.Product_ID = data.Product_ID  INNER JOIN type ON type.Type_ID = data.Type_ID  INNER JOIN sellprice ON sellprice.Data_ID = data.Data_ID LEFT JOIN storage ON storage.Data_ID = data.Data_ID where data.Data_ID=0 group by data.Data_ID", dbconnection);
            DataTable dtf = new DataTable();
            adapter.Fill(dtf);
            gridControl1.DataSource = dtf;

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
        //clear function
        public void clear()
        {
            foreach (Control co in this.panel1.Controls)
            {
                //if (co is GroupBox)
                //{
                foreach (Control item in co.Controls)
                {
                    if (item is ComboBox)
                    {
                        item.Text = "";
                    }
                    else if (item is TextBox)
                    {
                        item.Text = "";
                    }
                }
                //}
            }
        }
        public bool IsClear()
        {
            bool flag5 = false;
            foreach (Control co in this.panel1.Controls)
            {
                foreach (Control item in co.Controls)
                {
                    if (item is ComboBox)
                    {
                        if (item.Text == "")
                            flag5 = true;
                        else
                            return false;
                    }
                    else if (item is TextBox)
                    {
                        if (item.Text == "")
                            flag5 = true;
                        else
                            return false;
                    }
                }
            }

            return flag5;
        }    
        private void btnNewChosen_Click(object sender, EventArgs e)
        {
            clearCom();
            txtCodeSearch1.Text = "";
            txtCodeSearch2.Text = "";
            txtCodeSearch3.Text = "";
            txtCodeSearch4.Text = "";
            txtCodeSearch5.Text = "";
            gridControl1.DataSource = null;
        }
        //clear function
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

        private void btnReport_Click(object sender, EventArgs e)
        {
            try
            {
                if (gridView1.SelectedRowsCount > 0)
                {
                    if (gridView1.SelectedRowsCount == 1)
                    {
                        Form prompt = new Form()
                        {
                            Width = 200,
                            Height = 200,
                            FormBorderStyle = FormBorderStyle.FixedDialog,
                            Text = "",
                            StartPosition = FormStartPosition.CenterScreen,
                            MaximizeBox = false,
                            MinimizeBox = false
                        };
                        Label textLabel = new Label() { Left = 110, Top = 10, Text = ": العدد" };
                        TextBox textBox = new TextBox() { Left = 50, Top = 40, Width = 100, Multiline = true, Height = 50, RightToLeft = RightToLeft };
                        Button confirmation = new Button() { Text = "OK", Left = 50, Width = 100, Top = 110, DialogResult = DialogResult.OK };
                        prompt.Controls.Add(textBox);
                        prompt.Controls.Add(confirmation);
                        prompt.Controls.Add(textLabel);
                        prompt.AcceptButton = confirmation;
                        if (prompt.ShowDialog() == DialogResult.OK)
                        {
                            if (textBox.Text != "")
                            {
                                int count = 0;
                                if (int.TryParse(textBox.Text, out count))
                                {
                                    List<Ticket_Items> bi = new List<Ticket_Items>();
                                    dbconnection.Open();
                                    for (int j = 0; j < count; j++)
                                    {
                                        int rowhand = 0;
                                        rowhand = gridView1.GetSelectedRows()[0];
                                        Ticket_Items item;

                                        string query = "SELECT concat(product.Product_Name,' ',COALESCE(color.Color_Name,''),' ',data.Description,' ',groupo.Group_Name,' ',factory.Factory_Name,' ',COALESCE(size.Size_Value,'')) as 'الاسم' FROM data LEFT JOIN color ON color.Color_ID = data.Color_ID LEFT JOIN size ON size.Size_ID = data.Size_ID LEFT JOIN sort ON sort.Sort_ID = data.Sort_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID INNER JOIN factory ON factory.Factory_ID = data.Factory_ID  INNER JOIN product ON product.Product_ID = data.Product_ID INNER JOIN type ON type.Type_ID = data.Type_ID where data.Data_ID=" + gridView1.GetRowCellDisplayText(rowhand, "Data_ID");
                                        MySqlCommand com = new MySqlCommand(query, dbconnection);

                                        item = new Ticket_Items() { Data_ID = Convert.ToInt32(gridView1.GetRowCellDisplayText(rowhand, "Data_ID")), /*Type = gridView1.GetRowCellDisplayText(i, "النوع"), Product_Type = "بند",*/ Product_Name = com.ExecuteScalar().ToString(), Cost = /*Convert.ToDouble(*/gridView1.GetRowCellDisplayText(rowhand, "السعر")/*)*/, Carton = gridView1.GetRowCellDisplayText(rowhand, "الكرتنة") };
                                        item.Code = gridView1.GetRowCellDisplayText(rowhand, "الكود");
                                        bi.Add(item);
                                    }

                                    Print_Ticket_Report f = new Print_Ticket_Report();
                                    f.PrintInvoice(bi);
                                    f.ShowDialog();
                                }
                                else
                                {
                                    MessageBox.Show("يجب التاكد من ادخال عدد صحيح");
                                    dbconnection.Close();
                                    return;
                                }
                            }
                        }
                    }
                    else
                    {
                        List<Ticket_Items> bi = new List<Ticket_Items>();
                        dbconnection.Open();
                        for (int j = 0; j < gridView1.SelectedRowsCount; j++)
                        {
                            int rowhand = 0;
                            rowhand = gridView1.GetSelectedRows()[j];
                            Ticket_Items item;

                            string query = "SELECT concat(product.Product_Name,' ',COALESCE(color.Color_Name,''),' ',data.Description,' ',groupo.Group_Name,' ',factory.Factory_Name,' ',COALESCE(size.Size_Value,'')) as 'الاسم' FROM data LEFT JOIN color ON color.Color_ID = data.Color_ID LEFT JOIN size ON size.Size_ID = data.Size_ID LEFT JOIN sort ON sort.Sort_ID = data.Sort_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID INNER JOIN factory ON factory.Factory_ID = data.Factory_ID  INNER JOIN product ON product.Product_ID = data.Product_ID INNER JOIN type ON type.Type_ID = data.Type_ID where data.Data_ID=" + gridView1.GetRowCellDisplayText(rowhand, "Data_ID");
                            MySqlCommand com = new MySqlCommand(query, dbconnection);

                            item = new Ticket_Items() { Data_ID = Convert.ToInt32(gridView1.GetRowCellDisplayText(rowhand, "Data_ID")), /*Type = gridView1.GetRowCellDisplayText(i, "النوع"), Product_Type = "بند",*/ Product_Name = com.ExecuteScalar().ToString(), Cost = /*Convert.ToDouble(*/gridView1.GetRowCellDisplayText(rowhand, "السعر")/*)*/, Carton = gridView1.GetRowCellDisplayText(rowhand, "الكرتنة") };
                            item.Code = gridView1.GetRowCellDisplayText(rowhand, "الكود").ToString();
                            bi.Add(item);
                        }

                        Print_Ticket_Report f = new Print_Ticket_Report();
                        f.PrintInvoice(bi);
                        f.ShowDialog();
                    }
                }
                else
                {
                    MessageBox.Show("يجب اختيار البنود اولا");
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using MySql.Data.MySqlClient;
using DevExpress.XtraTab;

namespace MainSystem
{
    public partial class Information_Products : DevExpress.XtraEditors.XtraForm
    {
        MySqlConnection dbconnection;
        MySqlConnection dbconnection2;
        DataRow row1;
        
        bool loaded = false;
        bool factoryFlage = false;
        bool groupFlage = false;
        bool flagProduct = false;

        MainForm main;

        public Information_Products(MainForm Min)
        {
            InitializeComponent();
            dbconnection = new MySqlConnection(connection.connectionString);
            dbconnection2 = new MySqlConnection(connection.connectionString);

            main = Min;

            panel1.AutoScroll = false;
            panel1.VerticalScroll.Enabled = false;
            panel1.VerticalScroll.Visible = false;
            panel1.VerticalScroll.Maximum = 0;
            panel1.AutoScroll = true;
        }

        private void Information_Products_Load(object sender, EventArgs e)
        {
            try
            {
                //string query = "SELECT data.Data_ID,data.Code as 'الكود',CONCAT(product.Product_Name, ' , ', type.Type_Name, ' , ', factory.Factory_Name, ' , ', groupo.Group_Name) as 'الاسم',color.Color_Name as 'اللون',size.Size_Value as 'المقاس',sort.Sort_Value as 'الفرز',data.Carton as 'الكرتنة',sellprice.Last_Price as 'السعر',sellprice.Sell_Discount as 'الخصم',sellprice.Sell_Price as 'بعد الخصم',sum(storage.Total_Meters) as 'الكمية',data.Description as 'الوصف',data_photo.Photo as 'الصورة' from data INNER JOIN type ON type.Type_ID = data.Type_ID INNER JOIN product ON product.Product_ID = data.Product_ID INNER JOIN factory ON data.Factory_ID = factory.Factory_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID LEFT JOIN color ON data.Color_ID = color.Color_ID LEFT JOIN size ON data.Size_ID = size.Size_ID LEFT JOIN sort ON data.Sort_ID = sort.Sort_ID INNER JOIN sellprice ON sellprice.Data_ID = data.Data_ID LEFT JOIN storage ON storage.Data_ID = data.Data_ID LEFT JOIN data_photo ON data_photo.Data_ID = data.Data_ID group by data.Data_ID order by sellprice.Date desc";
                //MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                //DataTable dt = new DataTable();
                //da.Fill(dt);
                //gridControl1.DataSource = dt;
                //layoutView1.Columns[0].Visible = false;

                //if (layoutView1.IsLastVisibleRow)
                //{
                //    layoutView1.FocusedRowHandle = layoutView1.RowCount - 1;
                //}

                search();

                string q1, q2, q3, q4 = "";
                
                q1 = "select Type_ID from type";
                
                q2 = "select Factory_ID from factory";
                
                q3 = "select Product_ID from product";
                 
                q4 = "select Group_ID from groupo";
                
                displayPrduct(q1, q2, q3, q4, "");
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
            dbconnection2.Close();
        }

        private void comBox_SelectedValueChanged(object sender, EventArgs e)
        {
            if (loaded)
            {
                System.Windows.Forms.ComboBox comBox = (System.Windows.Forms.ComboBox)sender;

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
                }
            }
        }

        private void layoutView1_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                row1 = layoutView1.GetDataRow(layoutView1.GetRowHandle(layoutView1.FocusedRowHandle));
                XtraTabPage xtraTabPage = getTabPage("tabPageProductsReport");
                if (xtraTabPage == null)
                {
                    MainForm.tabPageProductsReport.Name = "tabPageProductsReport";
                    MainForm.tabPageProductsReport.Text = "عرض البنود";
                    MainForm.panelProductsReport.Name = "panelProductsReport";
                    MainForm.panelProductsReport.Dock = DockStyle.Fill;

                    MainForm.ProductsReport = new Products_Report(main, row1, "بند");
                    MainForm.ProductsReport.Size = new Size(1109, 660);
                    MainForm.ProductsReport.TopLevel = false;
                    MainForm.ProductsReport.FormBorderStyle = FormBorderStyle.None;
                    MainForm.ProductsReport.Dock = DockStyle.Fill;
                    //}
                    MainForm.panelProductsReport.Controls.Clear();
                    MainForm.panelProductsReport.Controls.Add(MainForm.ProductsReport);
                    MainForm.tabPageProductsReport.Controls.Add(MainForm.panelProductsReport);
                    MainForm.tabControlPointSale.TabPages.Add(MainForm.tabPageProductsReport);
                    MainForm.ProductsReport.Show();
                    MainForm.tabControlPointSale.SelectedTabPage = MainForm.tabPageProductsReport;
                }
                else
                {
                    MainForm.ProductsReport = new Products_Report(main, row1, "بند");
                    MainForm.ProductsReport.Size = new Size(1109, 660);
                    MainForm.ProductsReport.TopLevel = false;
                    MainForm.ProductsReport.FormBorderStyle = FormBorderStyle.None;
                    MainForm.ProductsReport.Dock = DockStyle.Fill;

                    MainForm.panelProductsReport.Controls.Clear();
                    MainForm.panelProductsReport.Controls.Add(MainForm.ProductsReport);
                    MainForm.ProductsReport.Show();
                    MainForm.tabControlPointSale.SelectedTabPage = MainForm.tabPageProductsReport;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
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

                displayPrduct(q1, q2, q3, q4, fQuery);

                fQuery = "";

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
            dbconnection2.Close();
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

            /*query = "select * from factory";
            da = new MySqlDataAdapter(query, dbconnection);
            dt = new DataTable();
            da.Fill(dt);
            comFactory.DataSource = dt;
            comFactory.DisplayMember = dt.Columns["Factory_Name"].ToString();
            comFactory.ValueMember = dt.Columns["Factory_ID"].ToString();
            comFactory.Text = "";
            //txtFactory.Text = "";

            query = "select * from groupo";
            da = new MySqlDataAdapter(query, dbconnection);
            dt = new DataTable();
            da.Fill(dt);
            comGroup.DataSource = dt;
            comGroup.DisplayMember = dt.Columns["Group_Name"].ToString();
            comGroup.ValueMember = dt.Columns["Group_ID"].ToString();
            comGroup.Text = "";
            //txtGroup.Text = "";

            query = "select * from product";
            da = new MySqlDataAdapter(query, dbconnection);
            dt = new DataTable();
            da.Fill(dt);
            comProduct.DataSource = dt;
            comProduct.DisplayMember = dt.Columns["Product_Name"].ToString();
            comProduct.ValueMember = dt.Columns["Product_ID"].ToString();
            comProduct.Text = "";
            //txtProduct.Text = "";
            
            query = "select * from size";
            da = new MySqlDataAdapter(query, dbconnection);
            dt = new DataTable();
            da.Fill(dt);
            comSize.DataSource = dt;
            comSize.DisplayMember = dt.Columns["Size_Value"].ToString();
            comSize.ValueMember = dt.Columns["Size_ID"].ToString();
            comSize.Text = "";

            query = "select * from color";
            da = new MySqlDataAdapter(query, dbconnection);
            dt = new DataTable();
            da.Fill(dt);
            comColor.DataSource = dt;
            comColor.DisplayMember = dt.Columns["Color_Name"].ToString();
            comColor.ValueMember = dt.Columns["Color_ID"].ToString();
            comColor.Text = "";*/

            query = "select * from sort";
            da = new MySqlDataAdapter(query, dbconnection);
            dt = new DataTable();
            da.Fill(dt);
            comSort.DataSource = dt;
            comSort.DisplayMember = dt.Columns["Sort_Value"].ToString();
            comSort.ValueMember = dt.Columns["Sort_ID"].ToString();
            comSort.Text = "";
            
            dbconnection.Close();
            loaded = true;
        }

        public void displayPrduct(string q1, string q2, string q3, string q4, string fQuery)
        {
            dbconnection.Open();
            string query = "SELECT data.Data_ID,data.Code as 'الكود',CONCAT(product.Product_Name, ' , ', type.Type_Name, ' , ', factory.Factory_Name, ' , ', groupo.Group_Name) as 'الاسم',color.Color_Name as 'اللون',size.Size_Value as 'المقاس',sort.Sort_Value as 'الفرز',data.Carton as 'الكرتنة',sellprice.Last_Price as 'السعر',sellprice.Sell_Discount as 'الخصم',sellprice.Sell_Price as 'بعد الخصم',sum(storage.Total_Meters) as 'الكمية',data.Description as 'الوصف',data_photo.Photo as 'الصورة' from data INNER JOIN type ON type.Type_ID = data.Type_ID INNER JOIN product ON product.Product_ID = data.Product_ID INNER JOIN factory ON data.Factory_ID = factory.Factory_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID LEFT JOIN color ON data.Color_ID = color.Color_ID LEFT JOIN size ON data.Size_ID = size.Size_ID LEFT JOIN sort ON data.Sort_ID = sort.Sort_ID INNER JOIN sellprice ON sellprice.Data_ID = data.Data_ID LEFT JOIN storage ON storage.Data_ID = data.Data_ID LEFT JOIN data_photo ON data_photo.Data_ID = data.Data_ID where data.Data_ID=0 group by data.Data_ID";
            MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
            DataTable dt = new DataTable();
            da.Fill(dt);
            gridControl1.DataSource = dt;

            dbconnection2.Open();
            query = "SELECT data.Data_ID,data.Code as 'الكود',CONCAT(product.Product_Name, ' , ', type.Type_Name, ' , ', factory.Factory_Name, ' , ', groupo.Group_Name) as 'الاسم',color.Color_Name as 'اللون',size.Size_Value as 'المقاس',sort.Sort_Value as 'الفرز',data.Carton as 'الكرتنة',sum(storage.Total_Meters) as 'الكمية',data.Description as 'الوصف',data_photo.Photo as 'الصورة' FROM data LEFT JOIN color ON color.Color_ID = data.Color_ID LEFT JOIN size ON size.Size_ID = data.Size_ID LEFT JOIN sort ON sort.Sort_ID = data.Sort_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID INNER JOIN factory ON factory.Factory_ID = data.Factory_ID  INNER JOIN product ON product.Product_ID = data.Product_ID  INNER JOIN type ON type.Type_ID = data.Type_ID  LEFT JOIN storage ON storage.Data_ID = data.Data_ID LEFT JOIN data_photo ON data_photo.Data_ID = data.Data_ID where data.Type_ID IN(" + q1 + ") and data.Factory_ID IN(" + q2 + ") and data.Product_ID IN (" + q3 + ") and data.Group_ID IN (" + q4 + ") " + fQuery + " group by data.Data_ID";
            MySqlCommand comand = new MySqlCommand(query, dbconnection);
            MySqlDataReader dr = comand.ExecuteReader();
            while (dr.Read())
            {
                string q = "select sellprice.Last_Price as 'السعر',sellprice.Sell_Discount as 'الخصم',sellprice.Sell_Price as 'بعد الخصم',sellprice.Price_Type from data INNER JOIN sellprice ON sellprice.Data_ID = data.Data_ID where data.Data_ID=" + dr["Data_ID"].ToString() + " order by sellprice.Date desc limit 1";
                MySqlCommand comand2 = new MySqlCommand(q, dbconnection2);
                MySqlDataReader dr2 = comand2.ExecuteReader();
                while (dr2.Read())
                {
                    layoutView1.AddNewRow();
                    int rowHandle = layoutView1.GetRowHandle(layoutView1.DataRowCount);
                    if (layoutView1.IsNewItemRow(rowHandle))
                    {
                        layoutView1.SetRowCellValue(rowHandle, layoutView1.Columns[0], dr["Data_ID"]);
                        layoutView1.SetRowCellValue(rowHandle, layoutView1.Columns[1], dr["الكود"]);
                        layoutView1.SetRowCellValue(rowHandle, layoutView1.Columns[2], dr["الاسم"]);
                        layoutView1.SetRowCellValue(rowHandle, layoutView1.Columns["اللون"], dr["اللون"]);
                        layoutView1.SetRowCellValue(rowHandle, layoutView1.Columns["المقاس"], dr["المقاس"]);
                        layoutView1.SetRowCellValue(rowHandle, layoutView1.Columns["الفرز"], dr["الفرز"]);
                        layoutView1.SetRowCellValue(rowHandle, layoutView1.Columns["الكرتنة"], dr["الكرتنة"]);
                        layoutView1.SetRowCellValue(rowHandle, layoutView1.Columns["الوصف"], dr["الوصف"]);
                        layoutView1.SetRowCellValue(rowHandle, layoutView1.Columns["الكمية"], dr["الكمية"]);

                        layoutView1.SetRowCellValue(rowHandle, layoutView1.Columns["السعر"], dr2["السعر"]);
                        layoutView1.SetRowCellValue(rowHandle, layoutView1.Columns["الخصم"], dr2["الخصم"]);
                        layoutView1.SetRowCellValue(rowHandle, layoutView1.Columns["بعد الخصم"], dr2["بعد الخصم"]);
                    }
                }
                dr2.Close();
            }
            dr.Close();
            layoutView1.Columns[0].Visible = false;
            if (layoutView1.IsLastVisibleRow)
            {
                layoutView1.FocusedRowHandle = layoutView1.RowCount - 1;
            }
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

        private void btnNewChosen_Click(object sender, EventArgs e)
        {
            clear();

            gridControl1.DataSource = null;
        }

        //clear function
        public void clear()
        {
            foreach (Control co in this.panel2.Controls)
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
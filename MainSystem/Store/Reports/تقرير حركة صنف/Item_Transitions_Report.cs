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
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MainSystem
{
    public partial class Item_Transitions_Report : Form
    {
        MySqlConnection dbconnection, dbconnection2;
        
        public static XtraTabPage MainTabPagePrintingTransitions;
        Panel panelPrintingTransitions;

        public static BillsTransitions_Print bankPrint;

        public static GridControl gridcontrol;
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
        DataRow row1;

        public Item_Transitions_Report()
        {
            InitializeComponent();
            dbconnection = new MySqlConnection(connection.connectionString);
            dbconnection2 = new MySqlConnection(connection.connectionString);

            MainTabPagePrintingTransitions = new XtraTabPage();
            panelPrintingTransitions = new Panel();
            
            gridcontrol = gridControl1;

            comStore.AutoCompleteMode = AutoCompleteMode.Suggest;
            comStore.AutoCompleteSource = AutoCompleteSource.ListItems;

            //this.dateTimePicker1.Format = DateTimePickerFormat.Short;
            //this.dateTimePicker2.Format = DateTimePickerFormat.Short;
        }

        private void Item_Transitions_Report_Load(object sender, EventArgs e)
        {
            try
            {
                DataHelperClassProducts dh = new DataHelperClassProducts(DSparametrProducts.doubleDS);
                gridControl1.DataSource = dh.DataSet;
                gridControl1.DataMember = dh.DataMember;
                gridView1.InitNewRow += GridView1_InitNewRow;

                loadBranch();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }

        private void GridView1_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            GridView view = sender as GridView;
            view.SetRowCellValue(e.RowHandle, gridView1.Columns[0], "");
            view.SetRowCellValue(e.RowHandle, gridView1.Columns[1], "");
            view.SetRowCellValue(e.RowHandle, gridView1.Columns[2], "");
            view.SetRowCellValue(e.RowHandle, gridView1.Columns[3], "");
            view.SetRowCellValue(e.RowHandle, gridView1.Columns[4], "");
            view.SetRowCellValue(e.RowHandle, gridView1.Columns[5], "");
            view.SetRowCellValue(e.RowHandle, gridView1.Columns[6], "");
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

                                    query = "select data.Data_ID,data.Code as 'الكود',type.Type_Name as 'النوع',concat(product.Product_Name,' - ',type.Type_Name,' - ',factory.Factory_Name,' - ',groupo.Group_Name,' ',COALESCE(color.Color_Name,''),' ',COALESCE(size.Size_Value,''),' ',COALESCE(sort.Sort_Value,'')) as 'الاسم',data.Carton as 'الكرتنة' FROM data LEFT JOIN color ON color.Color_ID = data.Color_ID LEFT JOIN size ON size.Size_ID = data.Size_ID LEFT JOIN sort ON sort.Sort_ID = data.Sort_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID INNER JOIN factory ON factory.Factory_ID = data.Factory_ID  INNER JOIN product ON product.Product_ID = data.Product_ID  INNER JOIN type ON type.Type_ID = data.Type_ID  INNER JOIN sellprice ON sellprice.Data_ID = data.Data_ID LEFT JOIN storage ON storage.Data_ID = data.Data_ID where data.Code like '" + code1 + code2 + code3 + code4 + "%' and data.Data_ID=0 group by data.Data_ID";
                                    MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                                    DataTable dt = new DataTable();
                                    da.Fill(dt);
                                    gridControl2.DataSource = dt;
                                    gridView2.Columns[0].Visible = false;

                                    int cont = gridView1.RowCount;
                                    for (int i = 0; i < cont; i++)
                                    {
                                        int rowHandle = gridView1.GetRowHandle(0);
                                        gridView1.DeleteRow(rowHandle);
                                    }
                                    txtTotalBills.Text = "0";
                                    txtTotalReturn.Text = "0";
                                    txtSafy.Text = "0";

                                    if (comStore.Text != "" && txtStoreID.Text != "")
                                    {
                                        dbconnection.Close();
                                        dbconnection.Open();
                                        query = "SELECT data.Data_ID,data.Code as 'الكود',type.Type_Name as 'النوع',concat(product.Product_Name,' ',COALESCE(color.Color_Name,''),' ',data.Description,' ',groupo.Group_Name,' ',factory.Factory_Name,' ',COALESCE(size.Size_Value,''),' ',COALESCE(sort.Sort_Value,'')) as 'الاسم',data.Carton as 'الكرتنة' FROM data LEFT JOIN color ON color.Color_ID = data.Color_ID LEFT JOIN size ON size.Size_ID = data.Size_ID LEFT JOIN sort ON sort.Sort_ID = data.Sort_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID INNER JOIN factory ON factory.Factory_ID = data.Factory_ID  INNER JOIN product ON product.Product_ID = data.Product_ID  INNER JOIN type ON type.Type_ID = data.Type_ID  LEFT JOIN storage ON storage.Data_ID = data.Data_ID where data.Code like '" + code1 + code2 + code3 + code4 + "%' group by data.Data_ID order by SUBSTR(data.Code,1,16),color.Color_Name,data.Description,data.Sort_ID";
                                        MySqlCommand comand = new MySqlCommand(query, dbconnection);
                                        MySqlDataReader dr = comand.ExecuteReader();
                                        while (dr.Read())
                                        {
                                            gridView2.AddNewRow();
                                            int rowHandle = gridView2.GetRowHandle(gridView2.DataRowCount);
                                            if (gridView2.IsNewItemRow(rowHandle))
                                            {
                                                gridView2.SetRowCellValue(rowHandle, gridView2.Columns[0], dr["Data_ID"]);
                                                gridView2.SetRowCellValue(rowHandle, gridView2.Columns[1], dr["الكود"]);
                                                gridView2.SetRowCellValue(rowHandle, gridView2.Columns["النوع"], dr["النوع"]);
                                                gridView2.SetRowCellValue(rowHandle, gridView2.Columns["الاسم"], dr["الاسم"]);
                                                gridView2.SetRowCellValue(rowHandle, gridView2.Columns["الكرتنة"], dr["الكرتنة"]);
                                            }
                                        }
                                        dr.Close();
                                        if (gridView2.IsLastVisibleRow)
                                        {
                                            gridView2.FocusedRowHandle = gridView2.RowCount - 1;
                                        }
                                    }
                                    else
                                    {
                                        MessageBox.Show("يجب اختيار مخزن");
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
                    //searchProduct();

                    dbconnection.Open();
                    string query = "select data.Data_ID,data.Code as 'الكود',type.Type_Name as 'النوع',concat(product.Product_Name,' - ',type.Type_Name,' - ',factory.Factory_Name,' - ',groupo.Group_Name,' ',COALESCE(color.Color_Name,''),' ',COALESCE(size.Size_Value,''),' ',COALESCE(sort.Sort_Value,'')) as 'الاسم',data.Carton as 'الكرتنة' FROM data LEFT JOIN color ON color.Color_ID = data.Color_ID LEFT JOIN size ON size.Size_ID = data.Size_ID LEFT JOIN sort ON sort.Sort_ID = data.Sort_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID INNER JOIN factory ON factory.Factory_ID = data.Factory_ID  INNER JOIN product ON product.Product_ID = data.Product_ID  INNER JOIN type ON type.Type_ID = data.Type_ID  INNER JOIN sellprice ON sellprice.Data_ID = data.Data_ID LEFT JOIN storage ON storage.Data_ID = data.Data_ID where data.Code='" + code1 + code2 + code3 + code4 + code5 + "' and data.Data_ID=0 group by data.Data_ID";
                    MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    gridControl2.DataSource = dt;
                    gridView2.Columns[0].Visible = false;

                    int cont = gridView1.RowCount;
                    for (int i = 0; i < cont; i++)
                    {
                        int rowHandle = gridView1.GetRowHandle(0);
                        gridView1.DeleteRow(rowHandle);
                    }
                    txtTotalBills.Text = "0";
                    txtTotalReturn.Text = "0";
                    txtSafy.Text = "0";

                    if (comStore.Text != "" && txtStoreID.Text != "")
                    {
                        query = "SELECT data.Data_ID,data.Code as 'الكود',type.Type_Name as 'النوع',concat(product.Product_Name,' ',COALESCE(color.Color_Name,''),' ',data.Description,' ',groupo.Group_Name,' ',factory.Factory_Name,' ',COALESCE(size.Size_Value,''),' ',COALESCE(sort.Sort_Value,'')) as 'الاسم',data.Carton as 'الكرتنة' FROM data LEFT JOIN color ON color.Color_ID = data.Color_ID LEFT JOIN size ON size.Size_ID = data.Size_ID LEFT JOIN sort ON sort.Sort_ID = data.Sort_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID INNER JOIN factory ON factory.Factory_ID = data.Factory_ID  INNER JOIN product ON product.Product_ID = data.Product_ID  INNER JOIN type ON type.Type_ID = data.Type_ID LEFT JOIN storage ON storage.Data_ID = data.Data_ID where data.Code='" + code1 + code2 + code3 + code4 + code5 + "' group by data.Data_ID order by SUBSTR(data.Code,1,16),color.Color_Name,data.Description,data.Sort_ID";
                        MySqlCommand comand = new MySqlCommand(query, dbconnection);
                        MySqlDataReader dr = comand.ExecuteReader();
                        while (dr.Read())
                        {
                            gridView2.AddNewRow();
                            int rowHandle = gridView2.GetRowHandle(gridView2.DataRowCount);
                            if (gridView2.IsNewItemRow(rowHandle))
                            {
                                gridView2.SetRowCellValue(rowHandle, gridView2.Columns[0], dr["Data_ID"]);
                                gridView2.SetRowCellValue(rowHandle, gridView2.Columns[1], dr["الكود"]);
                                gridView2.SetRowCellValue(rowHandle, gridView2.Columns["النوع"], dr["النوع"]);
                                gridView2.SetRowCellValue(rowHandle, gridView2.Columns["الاسم"], dr["الاسم"]);
                                gridView2.SetRowCellValue(rowHandle, gridView2.Columns["الكرتنة"], dr["الكرتنة"]);
                            }
                        }
                        dr.Close();
                        if (gridView2.IsLastVisibleRow)
                        {
                            gridView2.FocusedRowHandle = gridView2.RowCount - 1;
                        }
                    }
                    else
                    {
                        MessageBox.Show("يجب اختيار مخزن");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
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
                //comClassfication.Text = "";

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
                gridControl2.DataSource = null;
                int cont = gridView1.RowCount;
                for (int i = 0; i < cont; i++)
                {
                    int rowHandle = gridView1.GetRowHandle(0);
                    gridView1.DeleteRow(rowHandle);
                }
                txtTotalBills.Text = "0";
                txtTotalReturn.Text = "0";
                txtSafy.Text = "0";

                if (comStore.Text != "" && txtStoreID.Text != "")
                {
                    searchItems();
                }
                else
                {
                    MessageBox.Show("يجب اختيار مخزن");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            try
            {
                if (comStore.SelectedValue != null && gridView1.RowCount > 0 && row1 != null)
                {
                    List<Item_Transitions> bi = new List<Item_Transitions>();

                    for (int i = 0; i < gridView1.RowCount; i++)
                    {
                        double ItemIncrease = 0;
                        double ItemDecrease = 0;
                        double pricee = 0;
                        double totali = 0;
                        if (gridView1.GetRowCellDisplayText(i, gridView1.Columns["اضافة"]) != "")
                        {
                            ItemIncrease = Convert.ToDouble(gridView1.GetRowCellDisplayText(i, gridView1.Columns["اضافة"]));
                        }
                        if (gridView1.GetRowCellDisplayText(i, gridView1.Columns["خصم"]) != "")
                        {
                            ItemDecrease = Convert.ToDouble(gridView1.GetRowCellDisplayText(i, gridView1.Columns["خصم"]));
                        }
                        if (gridView1.GetRowCellDisplayText(i, gridView1.Columns["السعر"]) != "")
                        {
                            pricee = Convert.ToDouble(gridView1.GetRowCellDisplayText(i, gridView1.Columns["السعر"]));
                        }
                        if (gridView1.GetRowCellDisplayText(i, gridView1.Columns["الاجمالى"]) != "")
                        {
                            totali = Convert.ToDouble(gridView1.GetRowCellDisplayText(i, gridView1.Columns["الاجمالى"]));
                        }

                        Item_Transitions item = new Item_Transitions() { Note= gridView1.GetRowCellDisplayText(i, gridView1.Columns["بيان"]), Bill = gridView1.GetRowCellDisplayText(i, gridView1.Columns["رقم الفاتورة"]), Date = gridView1.GetRowCellDisplayText(i, gridView1.Columns["التاريخ"]), Client = gridView1.GetRowCellDisplayText(i, gridView1.Columns["العميل"]), Item_Increase = ItemIncrease, Item_Decrease = ItemDecrease, Price = pricee, Total_Cost = totali };
                        bi.Add(item);
                    }

                    Report_Item_Transitions f = new Report_Item_Transitions();
                    f.PrintInvoice(comStore.Text, dateTimePicker1.Value.Date, dateTimePicker2.Value.Date, row1["النوع"].ToString() + " " + row1["الاسم"].ToString(), Convert.ToDouble(txtTotalBills.Text), Convert.ToDouble(txtTotalReturn.Text), Convert.ToDouble(txtSafy.Text), bi);
                    f.ShowDialog();
                }
                else
                {
                    MessageBox.Show("يجب اختيار مخزن");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //functions
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
            comStore.Text = "";

            /*query = "select distinct Classification from data";
            da = new MySqlDataAdapter(query, dbconnection);
            dt = new DataTable();
            da.Fill(dt);
            comClassfication.DataSource = dt;
            comClassfication.DisplayMember = dt.Columns["Classification"].ToString();
            comClassfication.Text = "";*/

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
            try
            {
                if (comType.Text != "" && comFactory.Text != "" && comGroup.Text != "")
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

                    string query = "select data.Data_ID,data.Code as 'الكود',type.Type_Name as 'النوع',concat(product.Product_Name,' - ',type.Type_Name,' - ',factory.Factory_Name,' - ',groupo.Group_Name,' ',COALESCE(color.Color_Name,''),' ',COALESCE(size.Size_Value,''),' ',COALESCE(sort.Sort_Value,'')) as 'الاسم',data.Carton as 'الكرتنة' FROM data LEFT JOIN color ON color.Color_ID = data.Color_ID LEFT JOIN size ON size.Size_ID = data.Size_ID LEFT JOIN sort ON sort.Sort_ID = data.Sort_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID INNER JOIN factory ON factory.Factory_ID = data.Factory_ID  INNER JOIN product ON product.Product_ID = data.Product_ID  INNER JOIN type ON type.Type_ID = data.Type_ID  INNER JOIN sellprice ON sellprice.Data_ID = data.Data_ID LEFT JOIN storage ON storage.Data_ID = data.Data_ID where  data.Type_ID IN(" + q1 + ") and  data.Factory_ID  IN(" + q2 + ") and data.Group_ID IN (" + q4 + ") and data.Data_ID=0 group by data.Data_ID";
                    MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    gridControl2.DataSource = dt;
                    gridView2.Columns[0].Visible = false;

                    query = "SELECT data.Data_ID,data.Code as 'الكود',type.Type_Name as 'النوع',concat(product.Product_Name,' ',COALESCE(color.Color_Name,''),' ',data.Description,' ',groupo.Group_Name,' ',factory.Factory_Name,' ',COALESCE(size.Size_Value,''),' ',COALESCE(sort.Sort_Value,'')) as 'الاسم',data.Carton as 'الكرتنة' FROM data LEFT JOIN color ON color.Color_ID = data.Color_ID LEFT JOIN size ON size.Size_ID = data.Size_ID LEFT JOIN sort ON sort.Sort_ID = data.Sort_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID INNER JOIN factory ON factory.Factory_ID = data.Factory_ID  INNER JOIN product ON product.Product_ID = data.Product_ID  INNER JOIN type ON type.Type_ID = data.Type_ID  LEFT JOIN storage ON storage.Data_ID = data.Data_ID where data.Type_ID IN(" + q1 + ") and data.Factory_ID IN(" + q2 + ") and data.Product_ID IN (" + q3 + ") and data.Group_ID IN (" + q4 + ") " + fQuery + " group by data.Data_ID order by SUBSTR(data.Code,1,16),color.Color_Name,data.Description,data.Sort_ID";
                    MySqlCommand comand = new MySqlCommand(query, dbconnection);
                    MySqlDataReader dr = comand.ExecuteReader();
                    while (dr.Read())
                    {
                        gridView2.AddNewRow();
                        int rowHandle = gridView2.GetRowHandle(gridView2.DataRowCount);
                        if (gridView2.IsNewItemRow(rowHandle))
                        {
                            gridView2.SetRowCellValue(rowHandle, gridView2.Columns[0], dr["Data_ID"]);
                            gridView2.SetRowCellValue(rowHandle, gridView2.Columns[1], dr["الكود"]);
                            gridView2.SetRowCellValue(rowHandle, gridView2.Columns["النوع"], dr["النوع"]);
                            gridView2.SetRowCellValue(rowHandle, gridView2.Columns["الاسم"], dr["الاسم"]);
                            gridView2.SetRowCellValue(rowHandle, gridView2.Columns["الكرتنة"], dr["الكرتنة"]);
                        }
                    }
                    dr.Close();
                    
                    if (gridView2.IsLastVisibleRow)
                    {
                        gridView2.FocusedRowHandle = gridView2.RowCount - 1;
                    }
                }
                else
                {
                    gridControl1.DataSource = null;
                    MessageBox.Show("يجب اختيار النوع والمصنع والمجموعة على الاقل");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }

        public void searchSelectedItem(string dataId)
        {
            dbconnection.Open();
            dbconnection2.Open();
            //DATE_FORMAT(   , '%h:%i:%s %d-%m-%Y')
            string query = "SELECT Quantity as 'الكمية',open_storage_account.Date as 'التاريخ' FROM open_storage_account inner join data on data.Data_ID=open_storage_account.Data_ID LEFT JOIN color ON color.Color_ID = data.Color_ID LEFT JOIN size ON size.Size_ID = data.Size_ID LEFT JOIN sort ON sort.Sort_ID = data.Sort_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID INNER JOIN factory ON factory.Factory_ID = data.Factory_ID INNER JOIN product ON product.Product_ID = data.Product_ID INNER JOIN type ON type.Type_ID = data.Type_ID where data.Data_ID =" + dataId + " and open_storage_account.Store_ID=" + comStore.SelectedValue.ToString() + " and DATE(open_storage_account.Date) between '" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "' and '" + dateTimePicker2.Value.ToString("yyyy-MM-dd") + "' order by SUBSTR(data.Code,1,16),color.Color_Name,data.Description,data.Sort_ID";
            MySqlCommand comand = new MySqlCommand(query, dbconnection);
            MySqlDataReader dr = comand.ExecuteReader();
            while (dr.Read())
            {
                gridView1.AddNewRow();
                int rowHandle = gridView1.GetRowHandle(gridView1.DataRowCount);
                if (gridView1.IsNewItemRow(rowHandle))
                {
                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns["بيان"], "الرصيد الافتتاحى");
                    //gridView1.SetRowCellValue(rowHandle, gridView1.Columns["العميل"], "الرصيد الافتتاحى");
                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns["اضافة"], dr["الكمية"].ToString());
                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns["التاريخ"], dr["التاريخ"]);
                }
            }
            dr.Close();

            query = "SELECT distinct concat(customer_bill.Branch_BillNumber,' ',customer_bill.Branch_Name) as 'رقم الفاتورة',customer_bill.Bill_Date as 'التاريخ',concat(customer1.Customer_Name,' ',customer_bill.Customer_ID) as 'المهندس/المقاول/التاجر',concat(customer2.Customer_Name,' ',customer_bill.Client_ID) as 'العميل',product_bill.Price as 'السعر',product_bill.Discount as 'نسبة الخصم',product_bill.PriceAD as 'بعد الخصم',product_bill.Quantity as 'الكمية' FROM customer_bill INNER JOIN transitions ON customer_bill.Branch_ID = transitions.Branch_ID AND customer_bill.Branch_BillNumber = transitions.Bill_Number left join customer as customer1 on customer1.Customer_ID=customer_bill.Customer_ID left join customer as customer2 on customer2.Customer_ID=customer_bill.Client_ID INNER JOIN product_bill ON product_bill.CustomerBill_ID = customer_bill.CustomerBill_ID inner join data on data.Data_ID=product_bill.Data_ID LEFT JOIN color ON color.Color_ID = data.Color_ID LEFT JOIN size ON size.Size_ID = data.Size_ID LEFT JOIN sort ON sort.Sort_ID = data.Sort_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID INNER JOIN factory ON factory.Factory_ID = data.Factory_ID INNER JOIN product ON product.Product_ID = data.Product_ID INNER JOIN type ON type.Type_ID = data.Type_ID where transitions.Transition='ايداع' and data.Data_ID=" + dataId + " and product_bill.Store_ID=" + comStore.SelectedValue.ToString() + " and DATE(customer_bill.Bill_Date) between '" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "' and '" + dateTimePicker2.Value.ToString("yyyy-MM-dd") + "'";
            comand = new MySqlCommand(query, dbconnection);
            dr = comand.ExecuteReader();
            while (dr.Read())
            {
                gridView1.AddNewRow();
                int rowHandle = gridView1.GetRowHandle(gridView1.DataRowCount);
                if (gridView1.IsNewItemRow(rowHandle))
                {
                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns["بيان"], "مبيعات");
                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns["رقم الفاتورة"], dr["رقم الفاتورة"].ToString());
                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns["التاريخ"], dr["التاريخ"]);
                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns["العميل"], dr["العميل"].ToString() + " " + dr["المهندس/المقاول/التاجر"].ToString());
                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns["خصم"], dr["الكمية"].ToString());
                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns["السعر"], dr["بعد الخصم"].ToString());
                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns["الاجمالى"], Convert.ToDouble(dr["الكمية"].ToString()) * Convert.ToDouble(dr["بعد الخصم"].ToString()));
                }
            }
            dr.Close();

            query = "SELECT distinct concat(customer_return_bill.Branch_BillNumber,' ',customer_return_bill.Branch_Name) as 'رقم الفاتورة',customer_return_bill.Date as 'التاريخ',concat(customer1.Customer_Name,' ',customer_return_bill.Customer_ID) as 'المهندس/المقاول/التاجر',concat(customer2.Customer_Name,' ',customer_return_bill.Client_ID) as 'العميل',customer_return_bill_details.PriceBD as 'السعر',customer_return_bill_details.SellDiscount as 'نسبة الخصم',customer_return_bill_details.PriceAD as 'بعد الخصم',customer_return_bill_details.TotalMeter as 'الكمية' FROM customer_return_bill  INNER JOIN transitions ON customer_return_bill.Branch_ID = transitions.Branch_ID AND customer_return_bill.Branch_BillNumber = transitions.Bill_Number left join customer as customer1 on customer1.Customer_ID=customer_return_bill.Customer_ID left join customer as customer2 on customer2.Customer_ID=customer_return_bill.Client_ID INNER JOIN customer_return_bill_details ON customer_return_bill_details.CustomerReturnBill_ID = customer_return_bill.CustomerReturnBill_ID inner join data on data.Data_ID=customer_return_bill_details.Data_ID LEFT JOIN color ON color.Color_ID = data.Color_ID LEFT JOIN size ON size.Size_ID = data.Size_ID LEFT JOIN sort ON sort.Sort_ID = data.Sort_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID INNER JOIN factory ON factory.Factory_ID = data.Factory_ID INNER JOIN product ON product.Product_ID = data.Product_ID INNER JOIN type ON type.Type_ID = data.Type_ID where transitions.Transition='سحب' and data.Data_ID =" + dataId + " and customer_return_bill_details.Store_ID=" + comStore.SelectedValue.ToString() + " and DATE(customer_return_bill.Date) between '" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "' and '" + dateTimePicker2.Value.ToString("yyyy-MM-dd") + "'";
            comand = new MySqlCommand(query, dbconnection);
            dr = comand.ExecuteReader();
            while (dr.Read())
            {
                gridView1.AddNewRow();
                int rowHandle = gridView1.GetRowHandle(gridView1.DataRowCount);
                if (gridView1.IsNewItemRow(rowHandle))
                {
                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns["بيان"], "مبيعات");
                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns["رقم الفاتورة"], dr["رقم الفاتورة"].ToString());
                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns["التاريخ"], dr["التاريخ"]);
                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns["العميل"], dr["العميل"].ToString() + " " + dr["المهندس/المقاول/التاجر"].ToString());
                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns["اضافة"], dr["الكمية"].ToString());
                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns["السعر"], dr["بعد الخصم"].ToString());
                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns["الاجمالى"], Convert.ToDouble(dr["الكمية"].ToString()) * Convert.ToDouble(dr["بعد الخصم"].ToString()));
                }
            }
            dr.Close();
            //,supplier_bill_details.Purchasing_Ratio as 'نسبة الشراء'
            //,GROUP_CONCAT(DISTINCT concat(supplier.Supplier_Name,' ',supplier.Supplier_ID) SEPARATOR ',')
            query = "SELECT concat(storage_import_permission.Import_Permission_Number,' ',store.Store_Name) as 'رقم الفاتورة',storage_import_permission.Storage_Date as 'التاريخ',concat(supplier.Supplier_Name,' ',supplier.Supplier_ID) as 'العميل',supplier_permission_details.Total_Meters as 'الكمية',import_supplier_permission.Supplier_ID,import_supplier_permission.Supplier_Permission_Number,import_supplier_permission.StorageImportPermission_ID FROM import_supplier_permission INNER JOIN storage_import_permission ON import_supplier_permission.StorageImportPermission_ID = storage_import_permission.StorageImportPermission_ID INNER JOIN supplier_permission_details ON supplier_permission_details.ImportSupplierPermission_ID = import_supplier_permission.ImportSupplierPermission_ID INNER JOIN store ON store.Store_ID = storage_import_permission.Store_ID INNER JOIN supplier ON import_supplier_permission.Supplier_ID = supplier.Supplier_ID inner join data on data.Data_ID=supplier_permission_details.Data_ID LEFT JOIN color ON color.Color_ID = data.Color_ID LEFT JOIN size ON size.Size_ID = data.Size_ID LEFT JOIN sort ON sort.Sort_ID = data.Sort_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID INNER JOIN factory ON factory.Factory_ID = data.Factory_ID INNER JOIN product ON product.Product_ID = data.Product_ID INNER JOIN type ON type.Type_ID = data.Type_ID where data.Data_ID=" + dataId + " and storage_import_permission.Store_ID=" + comStore.SelectedValue.ToString() + " and DATE(storage_import_permission.Storage_Date) between '" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "' and '" + dateTimePicker2.Value.ToString("yyyy-MM-dd") + "'";
            comand = new MySqlCommand(query, dbconnection);
            dr = comand.ExecuteReader();
            while (dr.Read())
            {
                if (dr["رقم الفاتورة"].ToString() != "")
                {
                    gridView1.AddNewRow();
                    int rowHandle = gridView1.GetRowHandle(gridView1.DataRowCount);
                    if (gridView1.IsNewItemRow(rowHandle))
                    {
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns["بيان"], "اذن مخزن");
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns["رقم الفاتورة"], dr["رقم الفاتورة"].ToString());
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns["التاريخ"], dr["التاريخ"]);
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns["العميل"], dr["العميل"].ToString());
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns["اضافة"], dr["الكمية"].ToString());
                        if (UserControl.userType == 1 || UserControl.userType == 10 || UserControl.userType == 17 || UserControl.userType == 18)
                        {
                            double priceA = 1;
                            if (dr["StorageImportPermission_ID"].ToString() != "")
                            {
                                query = "SELECT supplier_bill.Bill_No 'رقم الفاتورة',supplier_bill.Date as 'التاريخ',concat(supplier.Supplier_Name,' ',supplier_bill.Supplier_ID) as 'العميل',supplier_bill_details.Price as 'السعر',supplier_bill_details.Purchasing_Discount as 'نسبة الخصم',supplier_bill_details.Purchasing_Price as 'بعد الخصم',supplier_bill_details.Total_Meters as 'الكمية' FROM supplier_bill INNER JOIN supplier_bill_details ON supplier_bill_details.Bill_ID = supplier_bill.Bill_ID INNER JOIN supplier ON supplier.Supplier_ID = supplier_bill.Supplier_ID inner join data on data.Data_ID=supplier_bill_details.Data_ID LEFT JOIN color ON color.Color_ID = data.Color_ID LEFT JOIN size ON size.Size_ID = data.Size_ID LEFT JOIN sort ON sort.Sort_ID = data.Sort_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID INNER JOIN factory ON factory.Factory_ID = data.Factory_ID INNER JOIN product ON product.Product_ID = data.Product_ID INNER JOIN type ON type.Type_ID = data.Type_ID where data.Data_ID=" + dataId + " and supplier_bill.Store_ID=" + comStore.SelectedValue.ToString() + " and date(supplier_bill.Date) between '" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "' and '" + dateTimePicker2.Value.ToString("yyyy-MM-dd") + "' and supplier_bill.Supplier_ID=" + dr["Supplier_ID"].ToString() + " and supplier_bill.Supplier_Permission_Number=" + dr["Supplier_Permission_Number"].ToString() + " and supplier_bill.StorageImportPermission_ID=" + dr["StorageImportPermission_ID"].ToString() + "";
                                comand = new MySqlCommand(query, dbconnection2);
                                MySqlDataReader dr2 = comand.ExecuteReader();
                                while (dr2.Read())
                                {
                                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns["بيان"], "اذن مخزن" + " - " + "شراء");

                                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns["رقم الفاتورة"], dr["رقم الفاتورة"].ToString() + " - " + dr2["رقم الفاتورة"].ToString());

                                    if (dr2["بعد الخصم"].ToString() != "")
                                    {
                                        priceA = Convert.ToDouble(dr2["بعد الخصم"].ToString());
                                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns["السعر"], dr2["بعد الخصم"].ToString());
                                    }
                                    else
                                    {
                                        priceA = 1;
                                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns["السعر"], "");
                                    }
                                }
                                dr2.Close();
                            }
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns["الاجمالى"], Convert.ToDouble(dr["الكمية"].ToString()) * priceA);
                        }
                    }
                }
            }
            dr.Close();
            //,supplier_return_bill_details.Purchasing_Ratio as 'نسبة الشراء'
            //,GROUP_CONCAT(DISTINCT concat(supplier.Supplier_Name,' ',supplier.Supplier_ID) SEPARATOR ',')
            query = "SELECT concat(import_storage_return.Returned_Permission_Number,' ',store.Store_Name) 'رقم الفاتورة',import_storage_return.Retrieval_Date as 'التاريخ',concat(supplier.Supplier_Name,' ',supplier.Supplier_ID) as 'العميل',import_storage_return_details.Total_Meters as 'الكمية',import_storage_return.ImportStorageReturn_ID FROM import_storage_return_supplier INNER JOIN import_storage_return ON import_storage_return_supplier.ImportStorageReturn_ID = import_storage_return.ImportStorageReturn_ID INNER JOIN import_storage_return_details ON import_storage_return_details.ImportStorageReturnSupplier_ID = import_storage_return_supplier.ImportStorageReturnSupplier_ID INNER JOIN store ON store.Store_ID = import_storage_return.Store_ID INNER JOIN supplier ON supplier.Supplier_ID = import_storage_return_supplier.Supplier_ID inner join data on data.Data_ID=import_storage_return_details.Data_ID LEFT JOIN color ON color.Color_ID = data.Color_ID LEFT JOIN size ON size.Size_ID = data.Size_ID LEFT JOIN sort ON sort.Sort_ID = data.Sort_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID INNER JOIN factory ON factory.Factory_ID = data.Factory_ID INNER JOIN product ON product.Product_ID = data.Product_ID INNER JOIN type ON type.Type_ID = data.Type_ID where data.Data_ID=" + dataId + " and import_storage_return.Store_ID=" + comStore.SelectedValue.ToString() + " and date(import_storage_return.Retrieval_Date) between '" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "' and '" + dateTimePicker2.Value.ToString("yyyy-MM-dd") + "'";
            comand = new MySqlCommand(query, dbconnection);
            dr = comand.ExecuteReader();
            while (dr.Read())
            {
                if (dr["رقم الفاتورة"].ToString() != "")
                {
                    gridView1.AddNewRow();
                    int rowHandle = gridView1.GetRowHandle(gridView1.DataRowCount);
                    if (gridView1.IsNewItemRow(rowHandle))
                    {
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns["بيان"], "اذن مخزن");
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns["رقم الفاتورة"], dr["رقم الفاتورة"].ToString());
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns["التاريخ"], dr["التاريخ"]);
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns["العميل"], dr["العميل"].ToString());
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns["خصم"], dr["الكمية"].ToString());
                        if (UserControl.userType == 1 || UserControl.userType == 10 || UserControl.userType == 17 || UserControl.userType == 18)
                        {
                            double priceA = 1;
                            if (dr["ImportStorageReturn_ID"].ToString() != "")
                            {
                                query = "SELECT supplier_return_bill.Return_Bill_No 'رقم الفاتورة',supplier_return_bill.Date as 'التاريخ',concat(supplier.Supplier_Name,' ',supplier_return_bill.Supplier_ID) as 'العميل',supplier_return_bill_details.Price as 'السعر',supplier_return_bill_details.Purchasing_Discount as 'نسبة الخصم',supplier_return_bill_details.Purchasing_Price as 'بعد الخصم',supplier_return_bill_details.Total_Meters as 'الكمية' FROM supplier_return_bill INNER JOIN supplier_return_bill_details ON supplier_return_bill_details.ReturnBill_ID = supplier_return_bill.ReturnBill_ID INNER JOIN supplier ON supplier_return_bill.Supplier_ID = supplier.Supplier_ID inner join data on data.Data_ID=supplier_return_bill_details.Data_ID LEFT JOIN color ON color.Color_ID = data.Color_ID LEFT JOIN size ON size.Size_ID = data.Size_ID LEFT JOIN sort ON sort.Sort_ID = data.Sort_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID INNER JOIN factory ON factory.Factory_ID = data.Factory_ID INNER JOIN product ON product.Product_ID = data.Product_ID INNER JOIN type ON type.Type_ID = data.Type_ID where data.Data_ID=" + dataId + " and supplier_return_bill.Store_ID=" + comStore.SelectedValue.ToString() + " and date(supplier_return_bill.Date) between '" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "' and '" + dateTimePicker2.Value.ToString("yyyy-MM-dd") + "' and supplier_return_bill.ImportStorageReturn_ID=" + dr["ImportStorageReturn_ID"].ToString();
                                comand = new MySqlCommand(query, dbconnection2);
                                MySqlDataReader dr2 = comand.ExecuteReader();
                                while (dr2.Read())
                                {
                                    if (dr2["بعد الخصم"].ToString() != "")
                                    {
                                        priceA = Convert.ToDouble(dr2["بعد الخصم"].ToString());
                                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns["السعر"], dr2["بعد الخصم"].ToString());
                                    }
                                    else
                                    {
                                        priceA = 1;
                                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns["السعر"], "");
                                    }
                                }
                                dr2.Close();
                            }
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns["الاجمالى"], Convert.ToDouble(dr["الكمية"].ToString()) * priceA);
                        }
                    }
                }
            }
            dr.Close();

            query = "SELECT taswayaa_adding_permision.PermissionNum 'رقم الفاتورة',taswayaa_adding_permision.Date as 'التاريخ',addstorage.AddingQuantity as 'الكمية' FROM taswayaa_adding_permision INNER JOIN addstorage ON addstorage.PermissionNum = taswayaa_adding_permision.PermissionNum inner join data on data.Data_ID=addstorage.Data_ID LEFT JOIN color ON color.Color_ID = data.Color_ID LEFT JOIN size ON size.Size_ID = data.Size_ID LEFT JOIN sort ON sort.Sort_ID = data.Sort_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID INNER JOIN factory ON factory.Factory_ID = data.Factory_ID INNER JOIN product ON product.Product_ID = data.Product_ID INNER JOIN type ON type.Type_ID = data.Type_ID where data.Data_ID=" + dataId + " and taswayaa_adding_permision.Store_ID=" + comStore.SelectedValue.ToString() + " and date(taswayaa_adding_permision.Date) between '" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "' and '" + dateTimePicker2.Value.ToString("yyyy-MM-dd") + "' order by SUBSTR(data.Code,1,16),color.Color_Name,data.Description,data.Sort_ID";
            comand = new MySqlCommand(query, dbconnection);
            dr = comand.ExecuteReader();
            while (dr.Read())
            {
                gridView1.AddNewRow();
                int rowHandle = gridView1.GetRowHandle(gridView1.DataRowCount);
                if (gridView1.IsNewItemRow(rowHandle))
                {
                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns["بيان"], "تسوية");
                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns["رقم الفاتورة"], dr["رقم الفاتورة"].ToString());
                    //gridView1.SetRowCellValue(rowHandle, gridView1.Columns["العميل"], "تسوية");
                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns["التاريخ"], dr["التاريخ"]);
                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns["اضافة"], dr["الكمية"].ToString());
                }
            }
            dr.Close();

            query = "SELECT taswayaa_subtract_permision.PermissionNum 'رقم الفاتورة',taswayaa_subtract_permision.Date as 'التاريخ',substorage.SubtractQuantity as 'الكمية' FROM taswayaa_subtract_permision INNER JOIN substorage ON substorage.PermissionNum = taswayaa_subtract_permision.PermissionNum inner join data on data.Data_ID=substorage.Data_ID LEFT JOIN color ON color.Color_ID = data.Color_ID LEFT JOIN size ON size.Size_ID = data.Size_ID LEFT JOIN sort ON sort.Sort_ID = data.Sort_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID INNER JOIN factory ON factory.Factory_ID = data.Factory_ID INNER JOIN product ON product.Product_ID = data.Product_ID INNER JOIN type ON type.Type_ID = data.Type_ID where data.Data_ID=" + dataId + " and taswayaa_subtract_permision.Store_ID=" + comStore.SelectedValue.ToString() + " and date(taswayaa_subtract_permision.Date) between '" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "' and '" + dateTimePicker2.Value.ToString("yyyy-MM-dd") + "' order by SUBSTR(data.Code,1,16),color.Color_Name,data.Description,data.Sort_ID";
            comand = new MySqlCommand(query, dbconnection);
            dr = comand.ExecuteReader();
            while (dr.Read())
            {
                gridView1.AddNewRow();
                int rowHandle = gridView1.GetRowHandle(gridView1.DataRowCount);
                if (gridView1.IsNewItemRow(rowHandle))
                {
                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns["بيان"], "تسوية");
                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns["رقم الفاتورة"], dr["رقم الفاتورة"].ToString());
                    //gridView1.SetRowCellValue(rowHandle, gridView1.Columns["العميل"], "تسوية");
                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns["التاريخ"], dr["التاريخ"]);
                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns["خصم"], dr["الكمية"].ToString());
                }
            }
            dr.Close();
            //Type_ID IN(" + q1 + ") and data.Factory_ID IN(" + q2 + ") and data.Product_ID IN (" + q3 + ") and data.Group_ID IN (" + q4 + ") " + fQuery
            query = "SELECT transfer_product.TransferProduct_ID 'رقم الفاتورة',transfer_product.Date as 'التاريخ',transfer_product_details.Quantity as 'الكمية',transfer_product.From_Store,transfer_product.To_Store FROM transfer_product INNER JOIN transfer_product_details ON transfer_product_details.TransferProduct_ID = transfer_product.TransferProduct_ID inner join data on data.Data_ID=transfer_product_details.Data_ID LEFT JOIN color ON color.Color_ID = data.Color_ID LEFT JOIN size ON size.Size_ID = data.Size_ID LEFT JOIN sort ON sort.Sort_ID = data.Sort_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID INNER JOIN factory ON factory.Factory_ID = data.Factory_ID INNER JOIN product ON product.Product_ID = data.Product_ID INNER JOIN type ON type.Type_ID = data.Type_ID where transfer_product_details.CustomerBill_ID=0 and data.Data_ID=" + dataId + " and (transfer_product.From_Store=" + comStore.SelectedValue.ToString() + " or transfer_product.To_Store=" + comStore.SelectedValue.ToString() + ") and date(transfer_product.Date) between '" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "' and '" + dateTimePicker2.Value.ToString("yyyy-MM-dd") + "' order by SUBSTR(data.Code,1,16),color.Color_Name,data.Description,data.Sort_ID";
            comand = new MySqlCommand(query, dbconnection);
            dr = comand.ExecuteReader();
            while (dr.Read())
            {
                gridView1.AddNewRow();
                int rowHandle = gridView1.GetRowHandle(gridView1.DataRowCount);
                if (gridView1.IsNewItemRow(rowHandle))
                {
                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns["بيان"], "تحويل");
                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns["رقم الفاتورة"], dr["رقم الفاتورة"].ToString());
                    //gridView1.SetRowCellValue(rowHandle, gridView1.Columns["العميل"], "تحويل");
                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns["التاريخ"], dr["التاريخ"]);
                    if (dr["From_Store"].ToString() == comStore.SelectedValue.ToString())
                    {
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns["خصم"], dr["الكمية"].ToString());
                    }
                    else if (dr["To_Store"].ToString() == comStore.SelectedValue.ToString())
                    {
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns["اضافة"], dr["الكمية"].ToString());
                    }
                }
            }
            dr.Close();

            query = "SELECT concat(inventory.Inventory_Num,' ',store.Store_Name) 'رقم الفاتورة',inventory_details.Date as 'التاريخ',inventory_details.Current_Quantity,inventory_details.Old_Quantity FROM inventory_details INNER JOIN inventory ON inventory_details.Inventory_ID = inventory.Inventory_ID INNER JOIN store ON store.Store_ID = inventory.Store_ID inner join data on data.Data_ID=inventory_details.Data_ID LEFT JOIN color ON color.Color_ID = data.Color_ID LEFT JOIN size ON size.Size_ID = data.Size_ID LEFT JOIN sort ON sort.Sort_ID = data.Sort_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID INNER JOIN factory ON factory.Factory_ID = data.Factory_ID INNER JOIN product ON product.Product_ID = data.Product_ID INNER JOIN type ON type.Type_ID = data.Type_ID where data.Data_ID=" + dataId + " and inventory.Store_ID=" + comStore.SelectedValue.ToString() + " and date(inventory_details.Date) between '" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "' and '" + dateTimePicker2.Value.ToString("yyyy-MM-dd") + "' order by SUBSTR(data.Code,1,16),color.Color_Name,data.Description,data.Sort_ID";
            comand = new MySqlCommand(query, dbconnection);
            dr = comand.ExecuteReader();
            while (dr.Read())
            {
                gridView1.AddNewRow();
                int rowHandle = gridView1.GetRowHandle(gridView1.DataRowCount);
                if (gridView1.IsNewItemRow(rowHandle))
                {
                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns["بيان"], "تسوية جرد");
                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns["رقم الفاتورة"], dr["رقم الفاتورة"].ToString());
                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns["التاريخ"], dr["التاريخ"]);
                    
                    if (dr["Old_Quantity"].ToString() == "" && dr["Current_Quantity"].ToString() == "")
                    {
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns["اضافة"], 0);
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns["خصم"], 0);
                    }
                    else if (dr["Old_Quantity"].ToString() == "" && dr["Current_Quantity"].ToString() != "")
                    {
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns["اضافة"], dr["Current_Quantity"].ToString());
                    }
                    else if (dr["Old_Quantity"].ToString() != "" && dr["Current_Quantity"].ToString() == "")
                    {
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns["اضافة"], 0);
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns["خصم"], 0);
                    }
                    else if (dr["Old_Quantity"].ToString() != "" && dr["Current_Quantity"].ToString() != "")
                    {
                        if ((Convert.ToDouble(dr["Current_Quantity"].ToString()) -  Convert.ToDouble(dr["Old_Quantity"].ToString())) > 0)
                        {
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns["اضافة"], (Convert.ToDouble(dr["Current_Quantity"].ToString()) - Convert.ToDouble(dr["Old_Quantity"].ToString())));
                        }
                        else if ((Convert.ToDouble(dr["Current_Quantity"].ToString()) - Convert.ToDouble(dr["Old_Quantity"].ToString())) < 0)
                        {
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns["خصم"], -1 * (Convert.ToDouble(dr["Current_Quantity"].ToString()) - Convert.ToDouble(dr["Old_Quantity"].ToString())));
                        }
                        else if ((Convert.ToDouble(dr["Current_Quantity"].ToString()) - Convert.ToDouble(dr["Old_Quantity"].ToString())) == 0)
                        {
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns["اضافة"], 0);
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns["خصم"], 0);
                        }
                    }
                }
            }
            dr.Close();

            query = "SELECT offer.Offer_ID as 'رقم الفاتورة',offer_openstorage_quantity.Date as 'التاريخ',offer.Offer_Name as 'العميل',offer.Price as 'بعد الخصم',(offer_openstorage_quantity.Quantity* offer_details.Quantity) as 'الكمية' FROM offer INNER JOIN offer_details ON offer_details.Offer_ID = offer.Offer_ID INNER JOIN offer_openstorage_quantity ON offer_openstorage_quantity.Offer_ID = offer.Offer_ID AND offer_details.Offer_ID = offer_openstorage_quantity.Offer_ID inner join data on data.Data_ID=offer_details.Data_ID LEFT JOIN color ON color.Color_ID = data.Color_ID LEFT JOIN size ON size.Size_ID = data.Size_ID LEFT JOIN sort ON sort.Sort_ID = data.Sort_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID INNER JOIN factory ON factory.Factory_ID = data.Factory_ID INNER JOIN product ON product.Product_ID = data.Product_ID INNER JOIN type ON type.Type_ID = data.Type_ID where data.Data_ID=" + dataId + " and offer_openstorage_quantity.Store_ID=" + comStore.SelectedValue.ToString() + " and DATE(offer_openstorage_quantity.Date) between '" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "' and '" + dateTimePicker2.Value.ToString("yyyy-MM-dd") + "'";
            comand = new MySqlCommand(query, dbconnection);
            dr = comand.ExecuteReader();
            while (dr.Read())
            {
                gridView1.AddNewRow();
                int rowHandle = gridView1.GetRowHandle(gridView1.DataRowCount);
                if (gridView1.IsNewItemRow(rowHandle))
                {
                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns["بيان"], "عرض");
                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns["رقم الفاتورة"], dr["رقم الفاتورة"].ToString());
                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns["التاريخ"], dr["التاريخ"]);
                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns["العميل"], dr["العميل"].ToString());
                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns["خصم"], dr["الكمية"].ToString());
                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns["السعر"], dr["بعد الخصم"].ToString());
                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns["الاجمالى"], Convert.ToDouble(dr["الكمية"].ToString()) * Convert.ToDouble(dr["بعد الخصم"].ToString()));
                }
            }
            dr.Close();

            if (gridView1.IsLastVisibleRow)
            {
                gridView1.FocusedRowHandle = gridView1.RowCount - 1;
            }

            if (!loaded)
            {
                for (int i = 1; i < gridView1.Columns.Count; i++)
                {
                    gridView1.Columns[i].Width = 110;
                }
            }

            double totalBill = 0;
            double totalReturned = 0;

            for (int i = 0; i < gridView1.RowCount; i++)
            {
                if (gridView1.GetRowCellDisplayText(i, "اضافة") != "")
                {
                    totalBill += Convert.ToDouble(gridView1.GetRowCellDisplayText(i, "اضافة").ToString());
                }
                else if (gridView1.GetRowCellDisplayText(i, "خصم") != "")
                {
                    totalReturned += Convert.ToDouble(gridView1.GetRowCellDisplayText(i, "خصم").ToString());
                }
            }

            txtTotalBills.Text = totalBill.ToString();
            txtTotalReturn.Text = totalReturned.ToString();
            txtSafy.Text = (totalBill - totalReturned).ToString("#.00");

            loaded = true;
        }

        /*public void searchTxt()
        {
            if (comStore.SelectedValue != null && txtCodeSearch1.Text != "" && txtCodeSearch2.Text != "" && txtCodeSearch3.Text != "" && txtCodeSearch4.Text != "" && txtCodeSearch5.Text != "")
            {
                int cont = gridView1.RowCount;
                for (int i = 0; i < cont; i++)
                {
                    int rowHandle = gridView1.GetRowHandle(0);
                    gridView1.DeleteRow(rowHandle);
                }
                txtTotalBills.Text = "0";
                txtTotalReturn.Text = "0";
                txtSafy.Text = "0";

                dbconnection.Open();
                string query = "SELECT Quantity as 'الكمية',open_storage_account.Date as 'التاريخ' FROM open_storage_account inner join data on data.Data_ID=open_storage_account.Data_ID LEFT JOIN color ON color.Color_ID = data.Color_ID LEFT JOIN size ON size.Size_ID = data.Size_ID LEFT JOIN sort ON sort.Sort_ID = data.Sort_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID INNER JOIN factory ON factory.Factory_ID = data.Factory_ID INNER JOIN product ON product.Product_ID = data.Product_ID INNER JOIN type ON type.Type_ID = data.Type_ID where data.Code='" + code1 + code2 + code3 + code4 + code5 + "' and open_storage_account.Store_ID=" + comStore.SelectedValue.ToString() + " and date(open_storage_account.Date) between '" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "' and '" + dateTimePicker2.Value.ToString("yyyy-MM-dd") + "' order by SUBSTR(data.Code,1,16),color.Color_Name,data.Description,data.Sort_ID";
                MySqlCommand comand = new MySqlCommand(query, dbconnection);
                MySqlDataReader dr = comand.ExecuteReader();
                while (dr.Read())
                {
                    gridView1.AddNewRow();
                    int rowHandle = gridView1.GetRowHandle(gridView1.DataRowCount);
                    if (gridView1.IsNewItemRow(rowHandle))
                    {
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns["العميل"], "الرصيد الافتتاحى");
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns["اضافة"], dr["الكمية"].ToString());
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns["التاريخ"], dr["التاريخ"]);
                    }
                }
                dr.Close();

                query = "SELECT concat(customer_bill.Branch_BillNumber,' ',customer_bill.Branch_Name) as 'رقم الفاتورة',customer_bill.Bill_Date as 'التاريخ',concat(customer1.Customer_Name,' ',customer_bill.Customer_ID) as 'المهندس/المقاول/التاجر',concat(customer2.Customer_Name,' ',customer_bill.Client_ID) as 'العميل',product_bill.Price as 'السعر',product_bill.Discount as 'نسبة الخصم',product_bill.PriceAD as 'بعد الخصم',product_bill.Quantity as 'الكمية' FROM customer_bill left join customer as customer1 on customer1.Customer_ID=customer_bill.Customer_ID left join customer as customer2 on customer2.Customer_ID=customer_bill.Client_ID INNER JOIN product_bill ON product_bill.CustomerBill_ID = customer_bill.CustomerBill_ID inner join data on data.Data_ID=product_bill.Data_ID LEFT JOIN color ON color.Color_ID = data.Color_ID LEFT JOIN size ON size.Size_ID = data.Size_ID LEFT JOIN sort ON sort.Sort_ID = data.Sort_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID INNER JOIN factory ON factory.Factory_ID = data.Factory_ID INNER JOIN product ON product.Product_ID = data.Product_ID INNER JOIN type ON type.Type_ID = data.Type_ID where data.Code='" + code1 + code2 + code3 + code4 + code5 + "' and product_bill.Store_ID=" + comStore.SelectedValue.ToString() + " and date(customer_bill.Bill_Date) between '" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "' and '" + dateTimePicker2.Value.ToString("yyyy-MM-dd") + "' order by SUBSTR(data.Code,1,16),color.Color_Name,data.Description,data.Sort_ID";
                comand = new MySqlCommand(query, dbconnection);
                dr = comand.ExecuteReader();
                while (dr.Read())
                {
                    gridView1.AddNewRow();
                    int rowHandle = gridView1.GetRowHandle(gridView1.DataRowCount);
                    if (gridView1.IsNewItemRow(rowHandle))
                    {
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns["رقم الفاتورة"], dr["رقم الفاتورة"].ToString());
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns["التاريخ"], dr["التاريخ"]);
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns["العميل"], dr["العميل"].ToString() + " " + dr["المهندس/المقاول/التاجر"].ToString());
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns["خصم"], dr["الكمية"].ToString());
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns["السعر"], dr["بعد الخصم"].ToString());
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns["الاجمالى"], Convert.ToDouble(dr["الكمية"].ToString()) * Convert.ToDouble(dr["بعد الخصم"].ToString()));
                    }
                }
                dr.Close();

                query = "SELECT concat(customer_return_bill.Branch_BillNumber,' ',customer_return_bill.Branch_Name) as 'رقم الفاتورة',customer_return_bill.Date as 'التاريخ',concat(customer1.Customer_Name,' ',customer_return_bill.Customer_ID) as 'المهندس/المقاول/التاجر',concat(customer2.Customer_Name,' ',customer_return_bill.Client_ID) as 'العميل',customer_return_bill_details.PriceBD as 'السعر',customer_return_bill_details.SellDiscount as 'نسبة الخصم',customer_return_bill_details.PriceAD as 'بعد الخصم',customer_return_bill_details.TotalMeter as 'الكمية' FROM customer_return_bill left join customer as customer1 on customer1.Customer_ID=customer_return_bill.Customer_ID left join customer as customer2 on customer2.Customer_ID=customer_return_bill.Client_ID INNER JOIN customer_return_bill_details ON customer_return_bill_details.CustomerReturnBill_ID = customer_return_bill.CustomerReturnBill_ID inner join data on data.Data_ID=customer_return_bill_details.Data_ID LEFT JOIN color ON color.Color_ID = data.Color_ID LEFT JOIN size ON size.Size_ID = data.Size_ID LEFT JOIN sort ON sort.Sort_ID = data.Sort_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID INNER JOIN factory ON factory.Factory_ID = data.Factory_ID INNER JOIN product ON product.Product_ID = data.Product_ID INNER JOIN type ON type.Type_ID = data.Type_ID where data.Code='" + code1 + code2 + code3 + code4 + code5 + "' and customer_return_bill_details.Store_ID=" + comStore.SelectedValue.ToString() + " and date(customer_return_bill.Date) between '" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "' and '" + dateTimePicker2.Value.ToString("yyyy-MM-dd") + "' order by SUBSTR(data.Code,1,16),color.Color_Name,data.Description,data.Sort_ID";
                comand = new MySqlCommand(query, dbconnection);
                dr = comand.ExecuteReader();
                while (dr.Read())
                {
                    gridView1.AddNewRow();
                    int rowHandle = gridView1.GetRowHandle(gridView1.DataRowCount);
                    if (gridView1.IsNewItemRow(rowHandle))
                    {
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns["رقم الفاتورة"], dr["رقم الفاتورة"].ToString());
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns["التاريخ"], dr["التاريخ"]);
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns["العميل"], dr["العميل"].ToString() + " " + dr["المهندس/المقاول/التاجر"].ToString());
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns["اضافة"], dr["الكمية"].ToString());
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns["السعر"], dr["بعد الخصم"].ToString());
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns["الاجمالى"], Convert.ToDouble(dr["الكمية"].ToString()) * Convert.ToDouble(dr["بعد الخصم"].ToString()));
                    }
                }
                dr.Close();
                //,supplier_bill_details.Purchasing_Ratio as 'نسبة الشراء'
                query = "SELECT supplier_bill.Bill_No 'رقم الفاتورة',supplier_bill.Date as 'التاريخ',concat(supplier.Supplier_Name,' ',supplier_bill.Supplier_ID) as 'العميل',supplier_bill_details.Price as 'السعر',supplier_bill_details.Purchasing_Discount as 'نسبة الخصم',supplier_bill_details.Purchasing_Price as 'بعد الخصم',supplier_bill_details.Total_Meters as 'الكمية' FROM supplier_bill INNER JOIN supplier_bill_details ON supplier_bill_details.Bill_ID = supplier_bill.Bill_ID INNER JOIN supplier ON supplier.Supplier_ID = supplier_bill.Supplier_ID inner join data on data.Data_ID=supplier_bill_details.Data_ID LEFT JOIN color ON color.Color_ID = data.Color_ID LEFT JOIN size ON size.Size_ID = data.Size_ID LEFT JOIN sort ON sort.Sort_ID = data.Sort_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID INNER JOIN factory ON factory.Factory_ID = data.Factory_ID INNER JOIN product ON product.Product_ID = data.Product_ID INNER JOIN type ON type.Type_ID = data.Type_ID where data.Code='" + code1 + code2 + code3 + code4 + code5 + "' and supplier_bill.Store_ID=" + comStore.SelectedValue.ToString() + " and date(supplier_bill.Date) between '" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "' and '" + dateTimePicker2.Value.ToString("yyyy-MM-dd") + "' order by SUBSTR(data.Code,1,16),color.Color_Name,data.Description,data.Sort_ID";
                comand = new MySqlCommand(query, dbconnection);
                dr = comand.ExecuteReader();
                while (dr.Read())
                {
                    gridView1.AddNewRow();
                    int rowHandle = gridView1.GetRowHandle(gridView1.DataRowCount);
                    if (gridView1.IsNewItemRow(rowHandle))
                    {
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns["رقم الفاتورة"], dr["رقم الفاتورة"].ToString());
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns["التاريخ"], dr["التاريخ"]);
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns["العميل"], dr["العميل"].ToString());
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns["اضافة"], dr["الكمية"].ToString());
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns["السعر"], dr["بعد الخصم"].ToString());
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns["الاجمالى"], Convert.ToDouble(dr["الكمية"].ToString()) * Convert.ToDouble(dr["بعد الخصم"].ToString()));
                    }
                }
                dr.Close();
                //,supplier_return_bill_details.Purchasing_Ratio as 'نسبة الشراء'
                query = "SELECT supplier_return_bill.Return_Bill_No 'رقم الفاتورة',supplier_return_bill.Date as 'التاريخ',concat(supplier.Supplier_Name,' ',supplier_return_bill.Supplier_ID) as 'العميل',supplier_return_bill_details.Price as 'السعر',supplier_return_bill_details.Purchasing_Discount as 'نسبة الخصم',supplier_return_bill_details.Purchasing_Price as 'بعد الخصم',supplier_return_bill_details.Total_Meters as 'الكمية' FROM supplier_return_bill INNER JOIN supplier_return_bill_details ON supplier_return_bill_details.ReturnBill_ID = supplier_return_bill.ReturnBill_ID INNER JOIN supplier ON supplier_return_bill.Supplier_ID = supplier.Supplier_ID inner join data on data.Data_ID=supplier_return_bill_details.Data_ID LEFT JOIN color ON color.Color_ID = data.Color_ID LEFT JOIN size ON size.Size_ID = data.Size_ID LEFT JOIN sort ON sort.Sort_ID = data.Sort_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID INNER JOIN factory ON factory.Factory_ID = data.Factory_ID INNER JOIN product ON product.Product_ID = data.Product_ID INNER JOIN type ON type.Type_ID = data.Type_ID where data.Code='" + code1 + code2 + code3 + code4 + code5 + "' and supplier_return_bill.Store_ID=" + comStore.SelectedValue.ToString() + " and date(supplier_return_bill.Date) between '" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "' and '" + dateTimePicker2.Value.ToString("yyyy-MM-dd") + "' order by SUBSTR(data.Code,1,16),color.Color_Name,data.Description,data.Sort_ID";
                comand = new MySqlCommand(query, dbconnection);
                dr = comand.ExecuteReader();
                while (dr.Read())
                {
                    gridView1.AddNewRow();
                    int rowHandle = gridView1.GetRowHandle(gridView1.DataRowCount);
                    if (gridView1.IsNewItemRow(rowHandle))
                    {
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns["رقم الفاتورة"], dr["رقم الفاتورة"].ToString());
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns["التاريخ"], dr["التاريخ"]);
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns["العميل"], dr["العميل"].ToString());
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns["خصم"], dr["الكمية"].ToString());
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns["السعر"], dr["بعد الخصم"].ToString());
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns["الاجمالى"], Convert.ToDouble(dr["الكمية"].ToString()) * Convert.ToDouble(dr["بعد الخصم"].ToString()));
                    }
                }
                dr.Close();

                query = "SELECT taswayaa_adding_permision.PermissionNum 'رقم الفاتورة',taswayaa_adding_permision.Date as 'التاريخ',addstorage.AddingQuantity as 'الكمية' FROM taswayaa_adding_permision INNER JOIN addstorage ON addstorage.PermissionNum = taswayaa_adding_permision.PermissionNum inner join data on data.Data_ID=addstorage.Data_ID LEFT JOIN color ON color.Color_ID = data.Color_ID LEFT JOIN size ON size.Size_ID = data.Size_ID LEFT JOIN sort ON sort.Sort_ID = data.Sort_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID INNER JOIN factory ON factory.Factory_ID = data.Factory_ID INNER JOIN product ON product.Product_ID = data.Product_ID INNER JOIN type ON type.Type_ID = data.Type_ID where data.Code='" + code1 + code2 + code3 + code4 + code5 + "' and taswayaa_adding_permision.Store_ID=" + comStore.SelectedValue.ToString() + " and date(taswayaa_adding_permision.Date) between '" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "' and '" + dateTimePicker2.Value.ToString("yyyy-MM-dd") + "' order by SUBSTR(data.Code,1,16),color.Color_Name,data.Description,data.Sort_ID";
                comand = new MySqlCommand(query, dbconnection);
                dr = comand.ExecuteReader();
                while (dr.Read())
                {
                    gridView1.AddNewRow();
                    int rowHandle = gridView1.GetRowHandle(gridView1.DataRowCount);
                    if (gridView1.IsNewItemRow(rowHandle))
                    {
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns["رقم الفاتورة"], dr["رقم الفاتورة"].ToString());
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns["العميل"], "تسوية");
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns["التاريخ"], dr["التاريخ"]);
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns["اضافة"], dr["الكمية"].ToString());
                    }
                }
                dr.Close();

                query = "SELECT taswayaa_subtract_permision.PermissionNum 'رقم الفاتورة',taswayaa_subtract_permision.Date as 'التاريخ',substorage.SubtractQuantity as 'الكمية' FROM taswayaa_subtract_permision INNER JOIN substorage ON substorage.PermissionNum = taswayaa_subtract_permision.PermissionNum inner join data on data.Data_ID=substorage.Data_ID LEFT JOIN color ON color.Color_ID = data.Color_ID LEFT JOIN size ON size.Size_ID = data.Size_ID LEFT JOIN sort ON sort.Sort_ID = data.Sort_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID INNER JOIN factory ON factory.Factory_ID = data.Factory_ID INNER JOIN product ON product.Product_ID = data.Product_ID INNER JOIN type ON type.Type_ID = data.Type_ID where data.Code='" + code1 + code2 + code3 + code4 + code5 + "' and taswayaa_subtract_permision.Store_ID=" + comStore.SelectedValue.ToString() + " and date(taswayaa_subtract_permision.Date) between '" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "' and '" + dateTimePicker2.Value.ToString("yyyy-MM-dd") + "' order by SUBSTR(data.Code,1,16),color.Color_Name,data.Description,data.Sort_ID";
                comand = new MySqlCommand(query, dbconnection);
                dr = comand.ExecuteReader();
                while (dr.Read())
                {
                    gridView1.AddNewRow();
                    int rowHandle = gridView1.GetRowHandle(gridView1.DataRowCount);
                    if (gridView1.IsNewItemRow(rowHandle))
                    {
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns["رقم الفاتورة"], dr["رقم الفاتورة"].ToString());
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns["العميل"], "تسوية");
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns["التاريخ"], dr["التاريخ"]);
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns["خصم"], dr["الكمية"].ToString());
                    }
                }
                dr.Close();

                query = "SELECT transfer_product.TransferProduct_ID 'رقم الفاتورة',transfer_product.Date as 'التاريخ',transfer_product_details.Quantity as 'الكمية',transfer_product.From_Store,transfer_product.To_Store FROM transfer_product INNER JOIN transfer_product_details ON transfer_product_details.TransferProduct_ID = transfer_product.TransferProduct_ID inner join data on data.Data_ID=transfer_product_details.Data_ID LEFT JOIN color ON color.Color_ID = data.Color_ID LEFT JOIN size ON size.Size_ID = data.Size_ID LEFT JOIN sort ON sort.Sort_ID = data.Sort_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID INNER JOIN factory ON factory.Factory_ID = data.Factory_ID INNER JOIN product ON product.Product_ID = data.Product_ID INNER JOIN type ON type.Type_ID = data.Type_ID where data.Code='" + code1 + code2 + code3 + code4 + code5 + "' and (transfer_product.From_Store=" + comStore.SelectedValue.ToString() + " or transfer_product.To_Store=" + comStore.SelectedValue.ToString() + ") and date(transfer_product.Date) between '" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "' and '" + dateTimePicker2.Value.ToString("yyyy-MM-dd") + "' order by SUBSTR(data.Code,1,16),color.Color_Name,data.Description,data.Sort_ID";
                comand = new MySqlCommand(query, dbconnection);
                dr = comand.ExecuteReader();
                while (dr.Read())
                {
                    gridView1.AddNewRow();
                    int rowHandle = gridView1.GetRowHandle(gridView1.DataRowCount);
                    if (gridView1.IsNewItemRow(rowHandle))
                    {
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns["رقم الفاتورة"], dr["رقم الفاتورة"].ToString());
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns["العميل"], "تحويل");
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns["التاريخ"], dr["التاريخ"]);
                        if (dr["From_Store"].ToString() == comStore.SelectedValue.ToString())
                        {
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns["خصم"], dr["الكمية"].ToString());
                        }
                        else if (dr["To_Store"].ToString() == comStore.SelectedValue.ToString())
                        {
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns["اضافة"], dr["الكمية"].ToString());
                        }
                    }
                }
                dr.Close();

                if (gridView1.IsLastVisibleRow)
                {
                    gridView1.FocusedRowHandle = gridView1.RowCount - 1;
                }

                if (!loaded)
                {
                    for (int i = 1; i < gridView1.Columns.Count; i++)
                    {
                        gridView1.Columns[i].Width = 110;
                    }
                }

                double totalBill = 0;
                double totalReturned = 0;

                for (int i = 0; i < gridView1.RowCount; i++)
                {
                    if (gridView1.GetRowCellDisplayText(i, "اضافة") != "")
                    {
                        totalBill += Convert.ToDouble(gridView1.GetRowCellDisplayText(i, "اضافة").ToString());
                    }
                    else
                    {
                        totalReturned += Convert.ToDouble(gridView1.GetRowCellDisplayText(i, "خصم").ToString());
                    }
                }

                txtTotalBills.Text = totalBill.ToString();
                txtTotalReturn.Text = totalReturned.ToString();
                txtSafy.Text = (totalBill - totalReturned).ToString();

                loaded = true;
            }
            else
            {
                int cont = gridView1.RowCount;
                for (int i = 0; i < cont; i++)
                {
                    int rowHandle = gridView1.GetRowHandle(0);
                    gridView1.DeleteRow(rowHandle);
                }
                txtTotalBills.Text = "0";
                txtTotalReturn.Text = "0";
                txtSafy.Text = "0";
                MessageBox.Show("برجاء التاكد من البيانات");
            }
        }*/

        private void comStore_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (loaded)
                {
                    int StoreID = 0;
                    if (int.TryParse(comStore.SelectedValue.ToString(), out StoreID))
                    {
                        txtStoreID.Text = comStore.SelectedValue.ToString();
                    }
                }
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

        private void gridView2_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            try
            {
                row1 = gridView2.GetDataRow(gridView2.GetRowHandle(e.RowHandle));
                if (row1 != null && comStore.Text != "" && txtStoreID.Text != "")
                {
                    int cont = gridView1.RowCount;
                    for (int i = 0; i < cont; i++)
                    {
                        int rowHandle = gridView1.GetRowHandle(0);
                        gridView1.DeleteRow(rowHandle);
                    }
                    txtTotalBills.Text = "0";
                    txtTotalReturn.Text = "0";
                    txtSafy.Text = "0";

                    searchSelectedItem(row1[0].ToString());
                }
                else
                {
                    MessageBox.Show("يجب اختيار مخزن");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
            dbconnection2.Close();
        }
    }
}

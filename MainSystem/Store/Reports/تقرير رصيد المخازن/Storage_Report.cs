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
    public partial class Storage_Report : Form
    {
        MySqlConnection dbconnection, dbconnection2, dbconnection3;
        
        public static XtraTabPage MainTabPagePrintingTransitions;
        Panel panelPrintingTransitions;

        public static BillsTransitions_Print bankPrint;
        
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

        public Storage_Report(MainForm mainForm)
        {
            InitializeComponent();
            dbconnection = new MySqlConnection(connection.connectionString);
            dbconnection2 = new MySqlConnection(connection.connectionString);
            dbconnection3 = new MySqlConnection(connection.connectionString);

            MainTabPagePrintingTransitions = new XtraTabPage();
            panelPrintingTransitions = new Panel();
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
                                                quantti = searchSelectedItem(dr["Data_ID"].ToString());
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
                                quantti = searchSelectedItem(dr["Data_ID"].ToString());
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
                List<Items_Storage> bi = new List<Items_Storage>();

                for (int i = 0; i < gridView1.RowCount; i++)
                {
                    double quantti = 0;
                    if (gridView1.GetRowCellDisplayText(i, gridView1.Columns["الكمية"]) != "")
                    {
                        quantti = Convert.ToDouble(gridView1.GetRowCellDisplayText(i, gridView1.Columns["الكمية"]));
                    }

                    totale += quantti;
                    Items_Storage item = new Items_Storage() { Code = gridView1.GetRowCellDisplayText(i, gridView1.Columns["الكود"]), Product_Type = gridView1.GetRowCellDisplayText(i, gridView1.Columns["النوع"]), Product_Name = gridView1.GetRowCellDisplayText(i, gridView1.Columns["الاسم"]), Quantity = quantti, Carton = gridView1.GetRowCellDisplayText(i, gridView1.Columns["الكرتنة"]) };
                    bi.Add(item);
                }

                Report_Items_Storage f = new Report_Items_Storage();
                f.PrintInvoice(comStore.Text, dateTimePicker2.Value.Date, totale, bi);
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

                string query = "select data.Data_ID,data.Code as 'الكود',type.Type_Name as 'النوع',concat(product.Product_Name,' - ',type.Type_Name,' - ',factory.Factory_Name,' - ',groupo.Group_Name,' ',COALESCE(color.Color_Name,''),' ',COALESCE(size.Size_Value,''),' ',COALESCE(sort.Sort_Value,'')) as 'الاسم',data.Carton as 'الكرتنة',sum(storage.Total_Meters) as 'الكمية' FROM data LEFT JOIN color ON color.Color_ID = data.Color_ID LEFT JOIN size ON size.Size_ID = data.Size_ID LEFT JOIN sort ON sort.Sort_ID = data.Sort_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID INNER JOIN factory ON factory.Factory_ID = data.Factory_ID  INNER JOIN product ON product.Product_ID = data.Product_ID  INNER JOIN type ON type.Type_ID = data.Type_ID  left JOIN sellprice ON sellprice.Data_ID = data.Data_ID LEFT JOIN storage ON storage.Data_ID = data.Data_ID where  data.Type_ID IN(" + q1 + ") and  data.Factory_ID  IN(" + q2 + ") and data.Group_ID IN (" + q4 + ") and data.Data_ID=0 group by data.Data_ID";
                MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                DataTable dt = new DataTable();
                da.Fill(dt);
                gridControl1.DataSource = dt;
                gridView1.Columns["Data_ID"].Visible = false;

                //date(storage.Storage_Date) between '2019-10-30' and '" + dateTimePicker2.Value.ToString("yyyy-MM-dd") + "' and
                query = "SELECT data.Data_ID,data.Code as 'الكود',type.Type_Name as 'النوع',concat(product.Product_Name,' ',COALESCE(color.Color_Name,''),' ',data.Description,' ',groupo.Group_Name,' ',factory.Factory_Name,' ',COALESCE(size.Size_Value,''),' ',COALESCE(sort.Sort_Value,'')) as 'الاسم',data.Carton as 'الكرتنة',sum(storage.Total_Meters) as 'الكمية' FROM data LEFT JOIN color ON color.Color_ID = data.Color_ID LEFT JOIN size ON size.Size_ID = data.Size_ID LEFT JOIN sort ON sort.Sort_ID = data.Sort_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID INNER JOIN factory ON factory.Factory_ID = data.Factory_ID  INNER JOIN product ON product.Product_ID = data.Product_ID  INNER JOIN type ON type.Type_ID = data.Type_ID  LEFT JOIN storage ON storage.Data_ID = data.Data_ID where data.Type_ID IN(" + q1 + ") and data.Factory_ID IN(" + q2 + ") and data.Product_ID IN (" + q3 + ") and data.Group_ID IN (" + q4 + ") " + fQuery + " group by data.Data_ID order by SUBSTR(data.Code,1,16),color.Color_Name,data.Description,data.Sort_ID";
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
                            quantti = searchSelectedItem(dr["Data_ID"].ToString());
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
                gridControl1.DataSource = null;
                MessageBox.Show("يجب اختيار النوع والمصنع على الاقل");
            }
        }

        public double searchSelectedItem(string dataId)
        {
            string qStore = "";
            if (comStore.Text == "")
            {
                qStore = "select Store_ID from store";
            }
            else
            {
                qStore = comStore.SelectedValue.ToString();
            }

            double quantity = 0;
            string q = "select Store_ID from store where Store_ID in(" + qStore + ")";
            MySqlCommand c = new MySqlCommand(q, dbconnection3);
            MySqlDataReader d = c.ExecuteReader();
            while (d.Read())
            {
                bool firstTime = true;
                string query2 = "SELECT concat(inventory.Inventory_Num,' ',store.Store_Name) 'رقم الفاتورة',inventory.Date as 'التاريخ',inventory_details.Current_Quantity,inventory_details.Old_Quantity,inventory.Store_ID FROM store left join inventory ON store.Store_ID = inventory.Store_ID INNER JOIN inventory_details ON inventory_details.Inventory_ID = inventory.Inventory_ID INNER JOIN data on data.Data_ID=inventory_details.Data_ID LEFT JOIN color ON color.Color_ID = data.Color_ID LEFT JOIN size ON size.Size_ID = data.Size_ID LEFT JOIN sort ON sort.Sort_ID = data.Sort_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID INNER JOIN factory ON factory.Factory_ID = data.Factory_ID INNER JOIN product ON product.Product_ID = data.Product_ID INNER JOIN type ON type.Type_ID = data.Type_ID where inventory.Store_ID =" + d["Store_ID"].ToString() + " and data.Data_ID=" + dataId + " and date(inventory_details.Date) between '2019-10-30' and '" + dateTimePicker2.Value.ToString("yyyy-MM-dd") + "' order by SUBSTR(data.Code,1,16),color.Color_Name,data.Description,data.Sort_ID";
                MySqlCommand comand2 = new MySqlCommand(query2, dbconnection2);
                MySqlDataReader dr2 = comand2.ExecuteReader();
                if (dr2.HasRows)
                {
                    while (dr2.Read())
                    {
                        if (firstTime)
                        {
                            if (dr2["Current_Quantity"].ToString() != "")
                            {
                                quantity += Convert.ToDouble(dr2["Current_Quantity"].ToString());
                            }
                            else if (dr2["Old_Quantity"].ToString() != "")
                            {
                                quantity += Convert.ToDouble(dr2["Old_Quantity"].ToString());
                            }
                            firstTime = false;
                        }
                        else
                        {
                            if (dr2["Old_Quantity"].ToString() == "" && dr2["Current_Quantity"].ToString() != "")
                            {
                                quantity += Convert.ToDouble(dr2["Current_Quantity"].ToString());
                            }
                            else if (dr2["Old_Quantity"].ToString() != "" && dr2["Current_Quantity"].ToString() != "")
                            {
                                if ((Convert.ToDouble(dr2["Current_Quantity"].ToString()) - Convert.ToDouble(dr2["Old_Quantity"].ToString())) > 0)
                                {
                                    quantity += (Convert.ToDouble(dr2["Current_Quantity"].ToString()) - Convert.ToDouble(dr2["Old_Quantity"].ToString()));
                                }
                                else if ((Convert.ToDouble(dr2["Current_Quantity"].ToString()) - Convert.ToDouble(dr2["Old_Quantity"].ToString())) < 0)
                                {
                                    quantity -= -1 * (Convert.ToDouble(dr2["Current_Quantity"].ToString()) - Convert.ToDouble(dr2["Old_Quantity"].ToString()));
                                }
                            }
                        }
                    }
                }
                else
                {
                    dr2.Close();
                    quantity += searchSelectedItemBefore(dataId, d["Store_ID"].ToString());
                }
                dr2.Close();
            }
            d.Close();


            string query = "SELECT distinct concat(customer_bill.Branch_BillNumber,' ',customer_bill.Branch_Name) as 'رقم الفاتورة',customer_bill.Bill_Date as 'التاريخ',concat(customer1.Customer_Name,' ',customer_bill.Customer_ID) as 'المهندس/المقاول/التاجر',concat(customer2.Customer_Name,' ',customer_bill.Client_ID) as 'العميل',product_bill.Quantity as 'الكمية' FROM customer_bill INNER JOIN transitions ON customer_bill.Branch_ID = transitions.Branch_ID AND customer_bill.Branch_BillNumber = transitions.Bill_Number left join customer as customer1 on customer1.Customer_ID=customer_bill.Customer_ID left join customer as customer2 on customer2.Customer_ID=customer_bill.Client_ID INNER JOIN product_bill ON product_bill.CustomerBill_ID = customer_bill.CustomerBill_ID inner join data on data.Data_ID=product_bill.Data_ID LEFT JOIN color ON color.Color_ID = data.Color_ID LEFT JOIN size ON size.Size_ID = data.Size_ID LEFT JOIN sort ON sort.Sort_ID = data.Sort_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID INNER JOIN factory ON factory.Factory_ID = data.Factory_ID INNER JOIN product ON product.Product_ID = data.Product_ID INNER JOIN type ON type.Type_ID = data.Type_ID where product_bill.Store_ID in(" + qStore + ") and transitions.Transition='ايداع' and data.Data_ID=" + dataId + " and DATE(customer_bill.Bill_Date) between '2019-10-30' and '" + dateTimePicker2.Value.ToString("yyyy-MM-dd") + "'";
            MySqlCommand comand = new MySqlCommand(query, dbconnection2);
            MySqlDataReader dr = comand.ExecuteReader();
            while (dr.Read())
            {
                quantity -= Convert.ToDouble(dr["الكمية"].ToString());
            }
            dr.Close();

            query = "SELECT distinct concat(customer_return_bill.Branch_BillNumber,' ',customer_return_bill.Branch_Name) as 'رقم الفاتورة',customer_return_bill.Date as 'التاريخ',concat(customer1.Customer_Name,' ',customer_return_bill.Customer_ID) as 'المهندس/المقاول/التاجر',concat(customer2.Customer_Name,' ',customer_return_bill.Client_ID) as 'العميل',customer_return_bill_details.TotalMeter as 'الكمية' FROM customer_return_bill  INNER JOIN transitions ON customer_return_bill.Branch_ID = transitions.Branch_ID AND customer_return_bill.Branch_BillNumber = transitions.Bill_Number left join customer as customer1 on customer1.Customer_ID=customer_return_bill.Customer_ID left join customer as customer2 on customer2.Customer_ID=customer_return_bill.Client_ID INNER JOIN customer_return_bill_details ON customer_return_bill_details.CustomerReturnBill_ID = customer_return_bill.CustomerReturnBill_ID inner join data on data.Data_ID=customer_return_bill_details.Data_ID LEFT JOIN color ON color.Color_ID = data.Color_ID LEFT JOIN size ON size.Size_ID = data.Size_ID LEFT JOIN sort ON sort.Sort_ID = data.Sort_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID INNER JOIN factory ON factory.Factory_ID = data.Factory_ID INNER JOIN product ON product.Product_ID = data.Product_ID INNER JOIN type ON type.Type_ID = data.Type_ID where customer_return_bill_details.Store_ID in(" + qStore + ") and transitions.Transition='سحب' and data.Data_ID =" + dataId + " and DATE(customer_return_bill.Date) between '2019-10-30' and '" + dateTimePicker2.Value.ToString("yyyy-MM-dd") + "'";
            comand = new MySqlCommand(query, dbconnection2);
            dr = comand.ExecuteReader();
            while (dr.Read())
            {
                quantity += Convert.ToDouble(dr["الكمية"].ToString());
            }
            dr.Close();

            query = "SELECT concat(storage_import_permission.Import_Permission_Number,' ',store.Store_Name) as 'رقم الفاتورة',storage_import_permission.Storage_Date as 'التاريخ',concat(supplier.Supplier_Name,' ',supplier.Supplier_ID) as 'العميل',supplier_permission_details.Total_Meters as 'الكمية',import_supplier_permission.Supplier_ID,import_supplier_permission.Supplier_Permission_Number,import_supplier_permission.StorageImportPermission_ID FROM import_supplier_permission INNER JOIN storage_import_permission ON import_supplier_permission.StorageImportPermission_ID = storage_import_permission.StorageImportPermission_ID INNER JOIN supplier_permission_details ON supplier_permission_details.ImportSupplierPermission_ID = import_supplier_permission.ImportSupplierPermission_ID INNER JOIN store ON store.Store_ID = storage_import_permission.Store_ID INNER JOIN supplier ON import_supplier_permission.Supplier_ID = supplier.Supplier_ID inner join data on data.Data_ID=supplier_permission_details.Data_ID LEFT JOIN color ON color.Color_ID = data.Color_ID LEFT JOIN size ON size.Size_ID = data.Size_ID LEFT JOIN sort ON sort.Sort_ID = data.Sort_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID INNER JOIN factory ON factory.Factory_ID = data.Factory_ID INNER JOIN product ON product.Product_ID = data.Product_ID INNER JOIN type ON type.Type_ID = data.Type_ID where storage_import_permission.Store_ID in(" + qStore + ") and data.Data_ID=" + dataId + " and DATE(storage_import_permission.Storage_Date) between '2019-10-30' and '" + dateTimePicker2.Value.ToString("yyyy-MM-dd") + "'";
            comand = new MySqlCommand(query, dbconnection2);
            dr = comand.ExecuteReader();
            while (dr.Read())
            {
                if (dr["رقم الفاتورة"].ToString() != "")
                {
                    quantity += Convert.ToDouble(dr["الكمية"].ToString());
                }
            }
            dr.Close();

            query = "SELECT concat(import_storage_return.Returned_Permission_Number,' ',store.Store_Name) 'رقم الفاتورة',import_storage_return.Retrieval_Date as 'التاريخ',concat(supplier.Supplier_Name,' ',supplier.Supplier_ID) as 'العميل',import_storage_return_details.Total_Meters as 'الكمية',import_storage_return.ImportStorageReturn_ID FROM import_storage_return_supplier INNER JOIN import_storage_return ON import_storage_return_supplier.ImportStorageReturn_ID = import_storage_return.ImportStorageReturn_ID INNER JOIN import_storage_return_details ON import_storage_return_details.ImportStorageReturnSupplier_ID = import_storage_return_supplier.ImportStorageReturnSupplier_ID INNER JOIN store ON store.Store_ID = import_storage_return.Store_ID INNER JOIN supplier ON supplier.Supplier_ID = import_storage_return_supplier.Supplier_ID inner join data on data.Data_ID=import_storage_return_details.Data_ID LEFT JOIN color ON color.Color_ID = data.Color_ID LEFT JOIN size ON size.Size_ID = data.Size_ID LEFT JOIN sort ON sort.Sort_ID = data.Sort_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID INNER JOIN factory ON factory.Factory_ID = data.Factory_ID INNER JOIN product ON product.Product_ID = data.Product_ID INNER JOIN type ON type.Type_ID = data.Type_ID where import_storage_return.Store_ID in(" + qStore + ") and data.Data_ID=" + dataId + " and date(import_storage_return.Retrieval_Date) between '2019-10-30' and '" + dateTimePicker2.Value.ToString("yyyy-MM-dd") + "'";
            comand = new MySqlCommand(query, dbconnection2);
            dr = comand.ExecuteReader();
            while (dr.Read())
            {
                if (dr["رقم الفاتورة"].ToString() != "")
                {
                    quantity -= Convert.ToDouble(dr["الكمية"].ToString());
                }
            }
            dr.Close();

            query = "SELECT taswayaa_adding_permision.PermissionNum 'رقم الفاتورة',taswayaa_adding_permision.Date as 'التاريخ',addstorage.AddingQuantity as 'الكمية' FROM taswayaa_adding_permision INNER JOIN addstorage ON addstorage.PermissionNum = taswayaa_adding_permision.PermissionNum inner join data on data.Data_ID=addstorage.Data_ID LEFT JOIN color ON color.Color_ID = data.Color_ID LEFT JOIN size ON size.Size_ID = data.Size_ID LEFT JOIN sort ON sort.Sort_ID = data.Sort_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID INNER JOIN factory ON factory.Factory_ID = data.Factory_ID INNER JOIN product ON product.Product_ID = data.Product_ID INNER JOIN type ON type.Type_ID = data.Type_ID where taswayaa_adding_permision.Store_ID in(" + qStore + ") and data.Data_ID=" + dataId + " and date(taswayaa_adding_permision.Date) between '2019-10-30' and '" + dateTimePicker2.Value.ToString("yyyy-MM-dd") + "' order by SUBSTR(data.Code,1,16),color.Color_Name,data.Description,data.Sort_ID";
            comand = new MySqlCommand(query, dbconnection2);
            dr = comand.ExecuteReader();
            while (dr.Read())
            {
                quantity += Convert.ToDouble(dr["الكمية"].ToString());
            }
            dr.Close();

            query = "SELECT taswayaa_subtract_permision.PermissionNum 'رقم الفاتورة',taswayaa_subtract_permision.Date as 'التاريخ',substorage.SubtractQuantity as 'الكمية' FROM taswayaa_subtract_permision INNER JOIN substorage ON substorage.PermissionNum = taswayaa_subtract_permision.PermissionNum inner join data on data.Data_ID=substorage.Data_ID LEFT JOIN color ON color.Color_ID = data.Color_ID LEFT JOIN size ON size.Size_ID = data.Size_ID LEFT JOIN sort ON sort.Sort_ID = data.Sort_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID INNER JOIN factory ON factory.Factory_ID = data.Factory_ID INNER JOIN product ON product.Product_ID = data.Product_ID INNER JOIN type ON type.Type_ID = data.Type_ID where taswayaa_subtract_permision.Store_ID in(" + qStore + ") and data.Data_ID=" + dataId + " and date(taswayaa_subtract_permision.Date) between '2019-10-30' and '" + dateTimePicker2.Value.ToString("yyyy-MM-dd") + "' order by SUBSTR(data.Code,1,16),color.Color_Name,data.Description,data.Sort_ID";
            comand = new MySqlCommand(query, dbconnection2);
            dr = comand.ExecuteReader();
            while (dr.Read())
            {
                quantity -= Convert.ToDouble(dr["الكمية"].ToString());
            }
            dr.Close();

            if (comStore.SelectedValue != null)
            {
                query = "SELECT transfer_product.TransferProduct_ID 'رقم الفاتورة',transfer_product.Date as 'التاريخ',transfer_product_details.Quantity as 'الكمية',transfer_product.From_Store,transfer_product.To_Store FROM transfer_product INNER JOIN transfer_product_details ON transfer_product_details.TransferProduct_ID = transfer_product.TransferProduct_ID inner join data on data.Data_ID=transfer_product_details.Data_ID LEFT JOIN color ON color.Color_ID = data.Color_ID LEFT JOIN size ON size.Size_ID = data.Size_ID LEFT JOIN sort ON sort.Sort_ID = data.Sort_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID INNER JOIN factory ON factory.Factory_ID = data.Factory_ID INNER JOIN product ON product.Product_ID = data.Product_ID INNER JOIN type ON type.Type_ID = data.Type_ID where transfer_product_details.CustomerBill_ID=0 and data.Data_ID=" + dataId + " and (transfer_product.From_Store =" + comStore.SelectedValue.ToString() + " or transfer_product.To_Store =" + comStore.SelectedValue.ToString() + ") and date(transfer_product.Date) between '2019-10-30' and '" + dateTimePicker2.Value.ToString("yyyy-MM-dd") + "' order by SUBSTR(data.Code,1,16),color.Color_Name,data.Description,data.Sort_ID";
                comand = new MySqlCommand(query, dbconnection2);
                dr = comand.ExecuteReader();
                while (dr.Read())
                {
                    if (dr["From_Store"].ToString() == comStore.SelectedValue.ToString())
                    {
                        quantity -= Convert.ToDouble(dr["الكمية"].ToString());
                    }
                    else if (dr["To_Store"].ToString() == comStore.SelectedValue.ToString())
                    {
                        quantity += Convert.ToDouble(dr["الكمية"].ToString());
                    }
                }
                dr.Close();
            }

            query = "SELECT offer.Offer_ID as 'رقم الفاتورة',offer_openstorage_quantity.Date as 'التاريخ',offer.Offer_Name as 'العميل',(offer_openstorage_quantity.Quantity* offer_details.Quantity) as 'الكمية' FROM offer INNER JOIN offer_details ON offer_details.Offer_ID = offer.Offer_ID INNER JOIN offer_openstorage_quantity ON offer_openstorage_quantity.Offer_ID = offer.Offer_ID AND offer_details.Offer_ID = offer_openstorage_quantity.Offer_ID inner join data on data.Data_ID=offer_details.Data_ID LEFT JOIN color ON color.Color_ID = data.Color_ID LEFT JOIN size ON size.Size_ID = data.Size_ID LEFT JOIN sort ON sort.Sort_ID = data.Sort_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID INNER JOIN factory ON factory.Factory_ID = data.Factory_ID INNER JOIN product ON product.Product_ID = data.Product_ID INNER JOIN type ON type.Type_ID = data.Type_ID where offer_openstorage_quantity.Store_ID in(" + qStore + ") and data.Data_ID=" + dataId + " and DATE(offer_openstorage_quantity.Date) between '2019-10-30' and '" + dateTimePicker2.Value.ToString("yyyy-MM-dd") + "'";
            comand = new MySqlCommand(query, dbconnection2);
            dr = comand.ExecuteReader();
            while (dr.Read())
            {
                quantity -= Convert.ToDouble(dr["الكمية"].ToString());
            }
            dr.Close();

            return quantity;
        }

        public double searchSelectedItemBefore(string dataId, string qStore)
        {
            /*if (comStore.Text == "")
            {
                qStore = "select Store_ID from store";
            }
            else
            {
                qStore = comStore.SelectedValue.ToString();
            }*/

            double quantity = 0;
            
            string query = "SELECT Quantity as 'الكمية',open_storage_account.Date as 'التاريخ' FROM open_storage_account inner join data on data.Data_ID=open_storage_account.Data_ID LEFT JOIN color ON color.Color_ID = data.Color_ID LEFT JOIN size ON size.Size_ID = data.Size_ID LEFT JOIN sort ON sort.Sort_ID = data.Sort_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID INNER JOIN factory ON factory.Factory_ID = data.Factory_ID INNER JOIN product ON product.Product_ID = data.Product_ID INNER JOIN type ON type.Type_ID = data.Type_ID where data.Data_ID =" + dataId + " and open_storage_account.Store_ID in(" + qStore + ") and DATE(open_storage_account.Date) <= '" + dateTimePicker2.Value.ToString("yyyy-MM-dd") + "' order by SUBSTR(data.Code,1,16),color.Color_Name,data.Description,data.Sort_ID";
            MySqlCommand comand = new MySqlCommand(query, dbconnection2);
            MySqlDataReader dr = comand.ExecuteReader();
            while (dr.Read())
            {
                quantity += Convert.ToDouble(dr["الكمية"].ToString());
            }
            dr.Close();

            query = "SELECT distinct concat(customer_bill.Branch_BillNumber,' ',customer_bill.Branch_Name) as 'رقم الفاتورة',customer_bill.Bill_Date as 'التاريخ',concat(customer1.Customer_Name,' ',customer_bill.Customer_ID) as 'المهندس/المقاول/التاجر',concat(customer2.Customer_Name,' ',customer_bill.Client_ID) as 'العميل',product_bill.Quantity as 'الكمية' FROM customer_bill INNER JOIN transitions ON customer_bill.Branch_ID = transitions.Branch_ID AND customer_bill.Branch_BillNumber = transitions.Bill_Number left join customer as customer1 on customer1.Customer_ID=customer_bill.Customer_ID left join customer as customer2 on customer2.Customer_ID=customer_bill.Client_ID INNER JOIN product_bill ON product_bill.CustomerBill_ID = customer_bill.CustomerBill_ID inner join data on data.Data_ID=product_bill.Data_ID LEFT JOIN color ON color.Color_ID = data.Color_ID LEFT JOIN size ON size.Size_ID = data.Size_ID LEFT JOIN sort ON sort.Sort_ID = data.Sort_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID INNER JOIN factory ON factory.Factory_ID = data.Factory_ID INNER JOIN product ON product.Product_ID = data.Product_ID INNER JOIN type ON type.Type_ID = data.Type_ID where product_bill.Store_ID in(" + qStore + ") and transitions.Transition='ايداع' and data.Data_ID=" + dataId + " and DATE(customer_bill.Bill_Date) < '2019-10-30'";
            comand = new MySqlCommand(query, dbconnection2);
            dr = comand.ExecuteReader();
            while (dr.Read())
            {
                quantity -= Convert.ToDouble(dr["الكمية"].ToString());
            }
            dr.Close();

            query = "SELECT distinct concat(customer_return_bill.Branch_BillNumber,' ',customer_return_bill.Branch_Name) as 'رقم الفاتورة',customer_return_bill.Date as 'التاريخ',concat(customer1.Customer_Name,' ',customer_return_bill.Customer_ID) as 'المهندس/المقاول/التاجر',concat(customer2.Customer_Name,' ',customer_return_bill.Client_ID) as 'العميل',customer_return_bill_details.TotalMeter as 'الكمية' FROM customer_return_bill  INNER JOIN transitions ON customer_return_bill.Branch_ID = transitions.Branch_ID AND customer_return_bill.Branch_BillNumber = transitions.Bill_Number left join customer as customer1 on customer1.Customer_ID=customer_return_bill.Customer_ID left join customer as customer2 on customer2.Customer_ID=customer_return_bill.Client_ID INNER JOIN customer_return_bill_details ON customer_return_bill_details.CustomerReturnBill_ID = customer_return_bill.CustomerReturnBill_ID inner join data on data.Data_ID=customer_return_bill_details.Data_ID LEFT JOIN color ON color.Color_ID = data.Color_ID LEFT JOIN size ON size.Size_ID = data.Size_ID LEFT JOIN sort ON sort.Sort_ID = data.Sort_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID INNER JOIN factory ON factory.Factory_ID = data.Factory_ID INNER JOIN product ON product.Product_ID = data.Product_ID INNER JOIN type ON type.Type_ID = data.Type_ID where customer_return_bill_details.Store_ID in(" + qStore + ") and transitions.Transition='سحب' and data.Data_ID =" + dataId + " and DATE(customer_return_bill.Date) < '2019-10-30'";
            comand = new MySqlCommand(query, dbconnection2);
            dr = comand.ExecuteReader();
            while (dr.Read())
            {
                quantity += Convert.ToDouble(dr["الكمية"].ToString());
            }
            dr.Close();

            query = "SELECT concat(storage_import_permission.Import_Permission_Number,' ',store.Store_Name) as 'رقم الفاتورة',storage_import_permission.Storage_Date as 'التاريخ',concat(supplier.Supplier_Name,' ',supplier.Supplier_ID) as 'العميل',supplier_permission_details.Total_Meters as 'الكمية',import_supplier_permission.Supplier_ID,import_supplier_permission.Supplier_Permission_Number,import_supplier_permission.StorageImportPermission_ID FROM import_supplier_permission INNER JOIN storage_import_permission ON import_supplier_permission.StorageImportPermission_ID = storage_import_permission.StorageImportPermission_ID INNER JOIN supplier_permission_details ON supplier_permission_details.ImportSupplierPermission_ID = import_supplier_permission.ImportSupplierPermission_ID INNER JOIN store ON store.Store_ID = storage_import_permission.Store_ID INNER JOIN supplier ON import_supplier_permission.Supplier_ID = supplier.Supplier_ID inner join data on data.Data_ID=supplier_permission_details.Data_ID LEFT JOIN color ON color.Color_ID = data.Color_ID LEFT JOIN size ON size.Size_ID = data.Size_ID LEFT JOIN sort ON sort.Sort_ID = data.Sort_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID INNER JOIN factory ON factory.Factory_ID = data.Factory_ID INNER JOIN product ON product.Product_ID = data.Product_ID INNER JOIN type ON type.Type_ID = data.Type_ID where storage_import_permission.Store_ID in(" + qStore + ") and data.Data_ID=" + dataId + " and DATE(storage_import_permission.Storage_Date) < '2019-10-30'";
            comand = new MySqlCommand(query, dbconnection2);
            dr = comand.ExecuteReader();
            while (dr.Read())
            {
                if (dr["رقم الفاتورة"].ToString() != "")
                {
                    quantity += Convert.ToDouble(dr["الكمية"].ToString());
                }
            }
            dr.Close();

            query = "SELECT concat(import_storage_return.Returned_Permission_Number,' ',store.Store_Name) 'رقم الفاتورة',import_storage_return.Retrieval_Date as 'التاريخ',concat(supplier.Supplier_Name,' ',supplier.Supplier_ID) as 'العميل',import_storage_return_details.Total_Meters as 'الكمية',import_storage_return.ImportStorageReturn_ID FROM import_storage_return_supplier INNER JOIN import_storage_return ON import_storage_return_supplier.ImportStorageReturn_ID = import_storage_return.ImportStorageReturn_ID INNER JOIN import_storage_return_details ON import_storage_return_details.ImportStorageReturnSupplier_ID = import_storage_return_supplier.ImportStorageReturnSupplier_ID INNER JOIN store ON store.Store_ID = import_storage_return.Store_ID INNER JOIN supplier ON supplier.Supplier_ID = import_storage_return_supplier.Supplier_ID inner join data on data.Data_ID=import_storage_return_details.Data_ID LEFT JOIN color ON color.Color_ID = data.Color_ID LEFT JOIN size ON size.Size_ID = data.Size_ID LEFT JOIN sort ON sort.Sort_ID = data.Sort_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID INNER JOIN factory ON factory.Factory_ID = data.Factory_ID INNER JOIN product ON product.Product_ID = data.Product_ID INNER JOIN type ON type.Type_ID = data.Type_ID where import_storage_return.Store_ID in(" + qStore + ") and data.Data_ID=" + dataId + " and date(import_storage_return.Retrieval_Date) < '2019-10-30'";
            comand = new MySqlCommand(query, dbconnection2);
            dr = comand.ExecuteReader();
            while (dr.Read())
            {
                if (dr["رقم الفاتورة"].ToString() != "")
                {
                    quantity -= Convert.ToDouble(dr["الكمية"].ToString());
                }
            }
            dr.Close();

            query = "SELECT taswayaa_adding_permision.PermissionNum 'رقم الفاتورة',taswayaa_adding_permision.Date as 'التاريخ',addstorage.AddingQuantity as 'الكمية' FROM taswayaa_adding_permision INNER JOIN addstorage ON addstorage.PermissionNum = taswayaa_adding_permision.PermissionNum inner join data on data.Data_ID=addstorage.Data_ID LEFT JOIN color ON color.Color_ID = data.Color_ID LEFT JOIN size ON size.Size_ID = data.Size_ID LEFT JOIN sort ON sort.Sort_ID = data.Sort_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID INNER JOIN factory ON factory.Factory_ID = data.Factory_ID INNER JOIN product ON product.Product_ID = data.Product_ID INNER JOIN type ON type.Type_ID = data.Type_ID where taswayaa_adding_permision.Store_ID in(" + qStore + ") and data.Data_ID=" + dataId + " and date(taswayaa_adding_permision.Date) < '2019-10-30' order by SUBSTR(data.Code,1,16),color.Color_Name,data.Description,data.Sort_ID";
            comand = new MySqlCommand(query, dbconnection2);
            dr = comand.ExecuteReader();
            while (dr.Read())
            {
                quantity += Convert.ToDouble(dr["الكمية"].ToString());
            }
            dr.Close();

            query = "SELECT taswayaa_subtract_permision.PermissionNum 'رقم الفاتورة',taswayaa_subtract_permision.Date as 'التاريخ',substorage.SubtractQuantity as 'الكمية' FROM taswayaa_subtract_permision INNER JOIN substorage ON substorage.PermissionNum = taswayaa_subtract_permision.PermissionNum inner join data on data.Data_ID=substorage.Data_ID LEFT JOIN color ON color.Color_ID = data.Color_ID LEFT JOIN size ON size.Size_ID = data.Size_ID LEFT JOIN sort ON sort.Sort_ID = data.Sort_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID INNER JOIN factory ON factory.Factory_ID = data.Factory_ID INNER JOIN product ON product.Product_ID = data.Product_ID INNER JOIN type ON type.Type_ID = data.Type_ID where taswayaa_subtract_permision.Store_ID in(" + qStore + ") and data.Data_ID=" + dataId + " and date(taswayaa_subtract_permision.Date) < '2019-10-30' order by SUBSTR(data.Code,1,16),color.Color_Name,data.Description,data.Sort_ID";
            comand = new MySqlCommand(query, dbconnection2);
            dr = comand.ExecuteReader();
            while (dr.Read())
            {
                quantity -= Convert.ToDouble(dr["الكمية"].ToString());
            }
            dr.Close();
            
                query = "SELECT transfer_product.TransferProduct_ID 'رقم الفاتورة',transfer_product.Date as 'التاريخ',transfer_product_details.Quantity as 'الكمية',transfer_product.From_Store,transfer_product.To_Store FROM transfer_product INNER JOIN transfer_product_details ON transfer_product_details.TransferProduct_ID = transfer_product.TransferProduct_ID inner join data on data.Data_ID=transfer_product_details.Data_ID LEFT JOIN color ON color.Color_ID = data.Color_ID LEFT JOIN size ON size.Size_ID = data.Size_ID LEFT JOIN sort ON sort.Sort_ID = data.Sort_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID INNER JOIN factory ON factory.Factory_ID = data.Factory_ID INNER JOIN product ON product.Product_ID = data.Product_ID INNER JOIN type ON type.Type_ID = data.Type_ID where transfer_product_details.CustomerBill_ID=0 and data.Data_ID=" + dataId + " and (transfer_product.From_Store =" + qStore + " or transfer_product.To_Store =" + qStore + ") and date(transfer_product.Date) < '2019-10-30' order by SUBSTR(data.Code,1,16),color.Color_Name,data.Description,data.Sort_ID";
                comand = new MySqlCommand(query, dbconnection2);
                dr = comand.ExecuteReader();
                while (dr.Read())
                {
                    if (dr["From_Store"].ToString() == qStore)
                    {
                        quantity -= Convert.ToDouble(dr["الكمية"].ToString());
                    }
                    else if (dr["To_Store"].ToString() == qStore)
                    {
                        quantity += Convert.ToDouble(dr["الكمية"].ToString());
                    }
                }
                dr.Close();

            query = "SELECT offer.Offer_ID as 'رقم الفاتورة',offer_openstorage_quantity.Date as 'التاريخ',offer.Offer_Name as 'العميل',(offer_openstorage_quantity.Quantity* offer_details.Quantity) as 'الكمية' FROM offer INNER JOIN offer_details ON offer_details.Offer_ID = offer.Offer_ID INNER JOIN offer_openstorage_quantity ON offer_openstorage_quantity.Offer_ID = offer.Offer_ID AND offer_details.Offer_ID = offer_openstorage_quantity.Offer_ID inner join data on data.Data_ID=offer_details.Data_ID LEFT JOIN color ON color.Color_ID = data.Color_ID LEFT JOIN size ON size.Size_ID = data.Size_ID LEFT JOIN sort ON sort.Sort_ID = data.Sort_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID INNER JOIN factory ON factory.Factory_ID = data.Factory_ID INNER JOIN product ON product.Product_ID = data.Product_ID INNER JOIN type ON type.Type_ID = data.Type_ID where offer_openstorage_quantity.Store_ID in(" + qStore + ") and data.Data_ID=" + dataId + " and DATE(offer_openstorage_quantity.Date) < '2019-10-30'";
            comand = new MySqlCommand(query, dbconnection2);
            dr = comand.ExecuteReader();
            while (dr.Read())
            {
                quantity -= Convert.ToDouble(dr["الكمية"].ToString());
            }
            dr.Close();
            
            return quantity;
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
    }
}

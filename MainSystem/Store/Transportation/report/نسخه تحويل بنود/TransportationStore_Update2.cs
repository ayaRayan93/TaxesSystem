﻿using DevExpress.XtraGrid.Views.Grid;
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

namespace TaxesSystem
{
    public partial class TransportationStore_Update2 : Form
    {
        MySqlConnection dbconnection, dbconnection2;
        bool loaded = false;
        bool factoryFlage = false;
        bool groupFlage = false;
        bool flagProduct = false;
        DataRow row1;
        int TransferProductID = 0;
        string FromStore = "";
        string ToStore = "";
        DateTime Date = new DateTime();
        XtraTabControl tabControlStoresContent = null;

        public TransportationStore_Update2(int transferProductID, string fromStore, string toStore, DateTime date, Transportation_Report transportationReport, XtraTabControl xtraTabControlStoresContent)
        {
            InitializeComponent();
            dbconnection = new MySqlConnection(connection.connectionString);
            dbconnection2 = new MySqlConnection(connection.connectionString);
            TransferProductID = transferProductID;
            FromStore = fromStore;
            ToStore = toStore;
            Date = date;
            tabControlStoresContent = xtraTabControlStoresContent;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                string query = "select * from Store";
                MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                DataTable dt = new DataTable();
                da.Fill(dt);
                comFromStore.DataSource = dt;
                comFromStore.DisplayMember = dt.Columns["Store_Name"].ToString();
                comFromStore.ValueMember = dt.Columns["Store_ID"].ToString();
                comFromStore.SelectedIndex = -1;
                comFromStore.SelectedValue = FromStore;

                query = "select * from Store where Store_ID<>" + comFromStore.SelectedValue.ToString();
                da = new MySqlDataAdapter(query, dbconnection);
                dt = new DataTable();
                da.Fill(dt);
                comToStore.DataSource = dt;
                comToStore.DisplayMember = dt.Columns["Store_Name"].ToString();
                comToStore.ValueMember = dt.Columns["Store_ID"].ToString();
                comToStore.SelectedIndex = -1;
                comToStore.SelectedValue = ToStore;

                query = "select * from store_places where Store_ID=" + comToStore.SelectedValue.ToString();
                da = new MySqlDataAdapter(query, dbconnection);
                dt = new DataTable();
                da.Fill(dt);
                comStorePlace.DataSource = dt;
                comStorePlace.DisplayMember = dt.Columns["Store_Place_Code"].ToString();
                comStorePlace.ValueMember = dt.Columns["Store_Place_ID"].ToString();

                query = "select * from type";
                da = new MySqlDataAdapter(query, dbconnection);
                dt = new DataTable();
                da.Fill(dt);
                comType.DataSource = dt;
                comType.DisplayMember = dt.Columns["Type_Name"].ToString();
                comType.ValueMember = dt.Columns["Type_ID"].ToString();
                comType.Text = "";

                query = "select * from factory";
                da = new MySqlDataAdapter(query, dbconnection);
                dt = new DataTable();
                da.Fill(dt);
                comFactory.DataSource = dt;
                comFactory.DisplayMember = dt.Columns["Factory_Name"].ToString();
                comFactory.ValueMember = dt.Columns["Factory_ID"].ToString();
                comFactory.Text = "";

                query = "select * from groupo";
                da = new MySqlDataAdapter(query, dbconnection);
                dt = new DataTable();
                da.Fill(dt);
                comGroup.DataSource = dt;
                comGroup.DisplayMember = dt.Columns["Group_Name"].ToString();
                comGroup.ValueMember = dt.Columns["Group_ID"].ToString();
                comGroup.Text = "";

                query = "select * from product";
                da = new MySqlDataAdapter(query, dbconnection);
                dt = new DataTable();
                da.Fill(dt);
                comProduct.DataSource = dt;
                comProduct.DisplayMember = dt.Columns["Product_Name"].ToString();
                comProduct.ValueMember = dt.Columns["Product_ID"].ToString();
                comProduct.Text = "";

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
                comColor.Text = "";

                query = "select * from sort";
                da = new MySqlDataAdapter(query, dbconnection);
                dt = new DataTable();
                da.Fill(dt);
                comSort.DataSource = dt;
                comSort.DisplayMember = dt.Columns["Sort_Value"].ToString();
                comSort.ValueMember = dt.Columns["Sort_ID"].ToString();
                comSort.Text = "";

                query = "SELECT data.Data_ID,data.Code as 'الكود',type.Type_Name as 'النوع',concat(product.Product_Name, ' ', COALESCE(color.Color_Name, ''), ' ', data.Description, ' ', groupo.Group_Name, ' ', factory.Factory_Name, ' ', COALESCE(size.Size_Value, ''), ' ', COALESCE(sort.Sort_Value, '')) as 'الاسم',data.Carton as 'الكرتنة',transfer_product_details.Quantity as 'الكمية' FROM transfer_product_details INNER JOIN transfer_product ON transfer_product_details.TransferProduct_ID = transfer_product.TransferProduct_ID left JOIN store as storeTo ON storeTo.Store_ID = transfer_product.To_Store left join store as storeFrom on storeFrom.Store_ID = transfer_product.From_Store INNER JOIN data ON transfer_product_details.Data_ID = data.Data_ID LEFT JOIN color ON color.Color_ID = data.Color_ID LEFT JOIN size ON size.Size_ID = data.Size_ID LEFT JOIN sort ON sort.Sort_ID = data.Sort_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID INNER JOIN factory ON factory.Factory_ID = data.Factory_ID  INNER JOIN product ON product.Product_ID = data.Product_ID  INNER JOIN type ON type.Type_ID = data.Type_ID where transfer_product.TransferProduct_ID=" + TransferProductID + " and transfer_product.Canceled=0 order by SUBSTR(data.Code, 1, 16),color.Color_Name,data.Description,data.Sort_ID";
                da = new MySqlDataAdapter(query, dbconnection);
                dt = new DataTable();
                da.Fill(dt);
                gridControl2.DataSource = dt;
                gridView2.Columns[0].Visible = false;
                gridView2.Columns["الاسم"].Width = 300;

                loaded = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void comBox_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (loaded)
                {
                    dbconnection.Close();
                    dbconnection.Open();
                    ComboBox comBox = (ComboBox)sender;

                    switch (comBox.Name)
                    {
                        case "comType":
                            if (loaded)
                            {
                                txtType.Text = comType.SelectedValue.ToString();
                                string query = "select * from factory inner join type_factory on factory.Factory_ID=type_factory.Factory_ID inner join type on type_factory.Type_ID=type.Type_ID where type_factory.Type_ID=" + txtType.Text;
                                MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                                DataTable dt = new DataTable();
                                da.Fill(dt);
                                comFactory.DataSource = dt;
                                comFactory.DisplayMember = dt.Columns["Factory_Name"].ToString();
                                comFactory.ValueMember = dt.Columns["Factory_ID"].ToString();
                                comFactory.Text = "";
                                txtFactory.Text = "";
                                dbconnection.Open();
                                query = "select TypeCoding_Method from type where Type_ID=" + txtType.Text;
                                MySqlCommand com = new MySqlCommand(query, dbconnection);
                                int TypeCoding_Method = Convert.ToInt32(com.ExecuteScalar());
                                if (TypeCoding_Method == 1)
                                {
                                    string query2 = "";
                                    if (txtType.Text == "2" || txtType.Text == "1")
                                    {
                                        query2 = "select * from groupo where Factory_ID=-1";
                                    }
                                    else
                                    {
                                        query2 = "select * from groupo where Factory_ID=" + -Convert.ToInt32(txtType.Text) + " and Type_ID=" + txtType.Text;
                                    }

                                    MySqlDataAdapter da2 = new MySqlDataAdapter(query2, dbconnection);
                                    DataTable dt2 = new DataTable();
                                    da2.Fill(dt2);
                                    comGroup.DataSource = dt2;
                                    comGroup.DisplayMember = dt2.Columns["Group_Name"].ToString();
                                    comGroup.ValueMember = dt2.Columns["Group_ID"].ToString();
                                    comGroup.Text = "";
                                    txtGroup.Text = "";
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
                                txtFactory.Text = comFactory.SelectedValue.ToString();
                                string query = "select TypeCoding_Method from type where Type_ID=" + txtType.Text;
                                MySqlCommand com = new MySqlCommand(query, dbconnection);
                                int TypeCoding_Method = (int)com.ExecuteScalar();
                                if (TypeCoding_Method == 2)
                                {
                                    string query2f = "select * from groupo where Type_ID=" + txtType.Text + " and Factory_ID=" + txtFactory.Text;
                                    MySqlDataAdapter da2f = new MySqlDataAdapter(query2f, dbconnection);
                                    DataTable dt2f = new DataTable();
                                    da2f.Fill(dt2f);
                                    comGroup.DataSource = dt2f;
                                    comGroup.DisplayMember = dt2f.Columns["Group_Name"].ToString();
                                    comGroup.ValueMember = dt2f.Columns["Group_ID"].ToString();
                                    comGroup.Text = "";
                                    txtGroup.Text = "";
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
                                txtGroup.Text = comGroup.SelectedValue.ToString();

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
                                txtProduct.Text = "";
                                
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
                            if (flagProduct)
                            {
                                flagProduct = false;
                                txtProduct.Text = comProduct.SelectedValue.ToString();
                                comSize.Focus();
                            }
                            break;

                        case "comSize":
                            comColor.Focus();
                            break;
                            
                        case "comColor":
                            comSort.Focus();
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
                    dbconnection.Open();
                    switch (txtBox.Name)
                    {
                        case "txtType":
                            query = "select Type_Name from type where Type_ID='" + txtType.Text + "'";
                            com = new MySqlCommand(query, dbconnection);
                            if (com.ExecuteScalar() != null)
                            {
                                Name = (string)com.ExecuteScalar();
                                factoryFlage = true;
                                comBox_SelectedValueChanged(comType, e);
                                comType.Text = Name;
                                txtFactory.Focus();
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
                                groupFlage = true;
                                comBox_SelectedValueChanged(comFactory, e);
                                comFactory.Text = Name;
                                txtGroup.Focus();
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
                                flagProduct = true;
                                comBox_SelectedValueChanged(comGroup, e);
                                comGroup.Text = Name;
                                txtProduct.Focus();
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
                                comBox_SelectedValueChanged(comProduct, e);
                                comProduct.Text = Name;
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
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                dbconnection.Close();
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (comFromStore.Text != "" && comType.Text != "" && comFactory.Text != "")
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
                    string query = "select data.Data_ID,data.Code as 'الكود',type.Type_Name as 'النوع',concat(product.Product_Name,' - ',type.Type_Name,' - ',factory.Factory_Name,' - ',groupo.Group_Name,' ',COALESCE(color.Color_Name,''),' ',COALESCE(size.Size_Value,''),' ',COALESCE(sort.Sort_Value,'')) as 'الاسم',data.Carton as 'الكرتنة',sum(storage.Total_Meters) as 'الكمية' FROM data LEFT JOIN color ON color.Color_ID = data.Color_ID LEFT JOIN size ON size.Size_ID = data.Size_ID LEFT JOIN sort ON sort.Sort_ID = data.Sort_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID INNER JOIN factory ON factory.Factory_ID = data.Factory_ID  INNER JOIN product ON product.Product_ID = data.Product_ID  INNER JOIN type ON type.Type_ID = data.Type_ID  INNER JOIN sellprice ON sellprice.Data_ID = data.Data_ID LEFT JOIN storage ON storage.Data_ID = data.Data_ID where  data.Type_ID IN(" + q1 + ") and  data.Factory_ID  IN(" + q2 + ") and data.Group_ID IN (" + q4 + ") and data.Data_ID=0 group by data.Data_ID";
                    MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    gridControl1.DataSource = dt;

                    if (gridView2.RowCount == 0)
                    {
                        query = "select data.Data_ID,data.Code as 'الكود',type.Type_Name as 'النوع',concat(product.Product_Name,' - ',type.Type_Name,' - ',factory.Factory_Name,' - ',groupo.Group_Name,' ',COALESCE(color.Color_Name,''),' ',COALESCE(size.Size_Value,''),' ',COALESCE(sort.Sort_Value,'')) as 'الاسم',data.Carton as 'الكرتنة',sum(storage.Total_Meters) as 'الكمية' FROM data LEFT JOIN color ON color.Color_ID = data.Color_ID LEFT JOIN size ON size.Size_ID = data.Size_ID LEFT JOIN sort ON sort.Sort_ID = data.Sort_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID INNER JOIN factory ON factory.Factory_ID = data.Factory_ID  INNER JOIN product ON product.Product_ID = data.Product_ID  INNER JOIN type ON type.Type_ID = data.Type_ID  INNER JOIN sellprice ON sellprice.Data_ID = data.Data_ID LEFT JOIN storage ON storage.Data_ID = data.Data_ID where  data.Type_ID IN(" + q1 + ") and  data.Factory_ID  IN(" + q2 + ") and data.Group_ID IN (" + q4 + ") and data.Data_ID=0 group by data.Data_ID";
                        da = new MySqlDataAdapter(query, dbconnection);
                        dt = new DataTable();
                        da.Fill(dt);
                        gridControl2.DataSource = dt;
                        gridView2.Columns[0].Visible = false;
                        gridView2.Columns["الاسم"].Width = 300;
                    }

                    query = "SELECT data.Data_ID,data.Code as 'الكود',type.Type_Name as 'النوع',concat(product.Product_Name,' ',COALESCE(color.Color_Name,''),' ',data.Description,' ',groupo.Group_Name,' ',factory.Factory_Name,' ',COALESCE(size.Size_Value,''),' ',COALESCE(sort.Sort_Value,'')) as 'الاسم',data.Carton as 'الكرتنة',sum(storage.Total_Meters) as 'الكمية' FROM data LEFT JOIN color ON color.Color_ID = data.Color_ID LEFT JOIN size ON size.Size_ID = data.Size_ID LEFT JOIN sort ON sort.Sort_ID = data.Sort_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID INNER JOIN factory ON factory.Factory_ID = data.Factory_ID  INNER JOIN product ON product.Product_ID = data.Product_ID  INNER JOIN type ON type.Type_ID = data.Type_ID inner JOIN storage ON storage.Data_ID = data.Data_ID where data.Type_ID IN(" + q1 + ") and data.Factory_ID IN(" + q2 + ") and data.Product_ID IN (" + q3 + ") and data.Group_ID IN (" + q4 + ") " + fQuery + " and storage.Store_ID=" + comFromStore.SelectedValue.ToString() + " group by data.Data_ID order by SUBSTR(data.Code,1,16),color.Color_Name,data.Description,data.Sort_ID";
                    MySqlCommand comand = new MySqlCommand(query, dbconnection);
                    MySqlDataReader dr = comand.ExecuteReader();
                    while (dr.Read())
                    {
                        gridView1.AddNewRow();
                        int rowHandle = gridView1.GetRowHandle(gridView1.DataRowCount);
                        if (gridView1.IsNewItemRow(rowHandle))
                        {
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns[0], dr["Data_ID"]);
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns["الكود"], dr["الكود"]);
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns["النوع"], dr["النوع"]);
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns["الاسم"], dr["الاسم"]);
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns["الكرتنة"], dr["الكرتنة"]);
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns["الكمية"], dr["الكمية"]);
                        }
                    }
                    dr.Close();
                    gridView1.Columns[0].Visible = false;
                    gridView1.Columns["الاسم"].Width = 300;
                    if (gridView1.IsLastVisibleRow)
                    {
                        gridView1.FocusedRowHandle = gridView1.RowCount - 1;
                    }
                }
                else
                {
                    MessageBox.Show("يجب اختيار المخزن واختيار النوع والمصنع على الاقل");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }

        private void btnNewChosen_Click(object sender, EventArgs e)
        {
            clearCom();
            gridControl1.DataSource = null;
        }

        private void gridView1_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            try
            {
                row1 = gridView1.GetDataRow(gridView1.GetRowHandle(e.RowHandle));
                txtCode.Text = row1["الكود"].ToString();
                txtQuantity.Text = "";
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
                if (comFromStore.Text != "" && comToStore.Text != "" && comFromStore.SelectedValue != null && comToStore.SelectedValue != null)
                {
                    if (row1 == null)
                    {
                        MessageBox.Show("يجب اختيار بند");
                        return;
                    }

                    if (IsItemAdded())
                    {
                        MessageBox.Show("هذا العنصر تم اضافتة من قبل");
                        return;
                    }

                    if (txtQuantity.Text != "")
                    {
                        double neededQuantity = 0;
                        if (!double.TryParse(txtQuantity.Text, out neededQuantity))
                        {
                            MessageBox.Show("الكمية يجب ان تكون عدد");
                            return;
                        }

                        dbconnection.Open();
                        double quantity = 0;
                        if (row1["الكمية"].ToString() != "")
                        {
                            quantity = Convert.ToDouble(row1["الكمية"].ToString());
                        }

                        if (neededQuantity <= quantity)
                        {
                            string query = "insert into transfer_product_details (Data_ID,Quantity,TransferProduct_ID,CustomerBill_ID) values (@Data_ID,@Quantity,@TransferProduct_ID,@CustomerBill_ID)";
                            MySqlCommand com = new MySqlCommand(query, dbconnection);
                            com.Parameters.Add("@Data_ID", MySqlDbType.Int16);
                            com.Parameters["@Data_ID"].Value = row1["Data_ID"].ToString();
                            com.Parameters.Add("@Quantity", MySqlDbType.Decimal);
                            com.Parameters["@Quantity"].Value = neededQuantity;
                            com.Parameters.Add("@TransferProduct_ID", MySqlDbType.Int16);
                            com.Parameters["@TransferProduct_ID"].Value = TransferProductID;
                            com.Parameters.Add("@CustomerBill_ID", MySqlDbType.Int16);
                            com.Parameters["@CustomerBill_ID"].Value = 0;
                            com.ExecuteNonQuery();
                            
                            double meters = quantity - neededQuantity;
                            query = "update storage set Total_Meters=" + meters + " where Data_ID=" + row1[0].ToString() + " and Store_ID=" + comFromStore.SelectedValue.ToString();
                            com = new MySqlCommand(query, dbconnection);
                            com.ExecuteNonQuery();

                            query = "select OpenStorageAccount_ID from open_storage_account where Data_ID=" + row1["Data_ID"].ToString() + " and Store_ID=" + comToStore.SelectedValue.ToString();
                            com = new MySqlCommand(query, dbconnection);
                            if (com.ExecuteScalar() == null)
                            {
                                query = "insert into open_storage_account (Data_ID,Quantity,Store_ID,Store_Place_ID,Date,Note) values (@Data_ID,@Quantity,@Store_ID,@Store_Place_ID,@Date,@Note)";
                                com = new MySqlCommand(query, dbconnection);
                                com.Parameters.Add("@Data_ID", MySqlDbType.Int16);
                                com.Parameters["@Data_ID"].Value = row1["Data_ID"].ToString();
                                com.Parameters.Add("@Quantity", MySqlDbType.Decimal);
                                com.Parameters["@Quantity"].Value = 0;
                                com.Parameters.Add("@Store_ID", MySqlDbType.Int16);
                                com.Parameters["@Store_ID"].Value = comToStore.SelectedValue.ToString();
                                com.Parameters.Add("@Store_Place_ID", MySqlDbType.Int16);
                                com.Parameters["@Store_Place_ID"].Value = comStorePlace.SelectedValue.ToString();
                                com.Parameters.Add("@Date", MySqlDbType.Date, 0);
                                com.Parameters["@Date"].Value = DateTime.Now.ToString("yyyy-MM-dd");
                                com.Parameters.Add("@Note", MySqlDbType.VarChar);
                                com.Parameters["@Note"].Value = "تعديل تحويل";
                                com.ExecuteNonQuery();

                                UserControl.ItemRecord("open_storage_account", "اضافة", Convert.ToInt32(row1["Data_ID"].ToString()), DateTime.Now, "تعديل تحويل", dbconnection);
                            }

                            query = "select sum(Total_Meters) from storage where Data_ID=" + row1[0].ToString() + " and Store_ID=" + comToStore.SelectedValue.ToString() + " group by Data_ID";
                            com = new MySqlCommand(query, dbconnection);
                            if (com.ExecuteScalar() != null)
                            {
                                quantity = Convert.ToDouble(com.ExecuteScalar().ToString());
                                meters = quantity + neededQuantity;
                                query = "update storage set Total_Meters=" + meters + " where Data_ID=" + row1[0].ToString() + " and Store_ID=" + comToStore.SelectedValue.ToString();
                                com = new MySqlCommand(query, dbconnection);
                                com.ExecuteNonQuery();
                            }
                            else
                            {
                                query = "insert into storage (Store_ID,Store_Place_ID,Storage_Date,Type,Data_ID,Total_Meters,Note) values (@Store_ID,@Store_Place_ID,@Storage_Date,@Type,@Data_ID,@Total_Meters,@Note)";
                                com = new MySqlCommand(query, dbconnection);
                                com.Parameters.Add("@Store_ID", MySqlDbType.Int16);
                                com.Parameters["@Store_ID"].Value = comToStore.SelectedValue.ToString();
                                com.Parameters.Add("@Store_Place_ID", MySqlDbType.Int16);
                                com.Parameters["@Store_Place_ID"].Value = comStorePlace.SelectedValue.ToString();
                                com.Parameters.Add("@Storage_Date", MySqlDbType.DateTime);
                                com.Parameters["@Storage_Date"].Value = DateTime.Now;
                                com.Parameters.Add("@Type", MySqlDbType.VarChar);
                                com.Parameters["@Type"].Value = "بند";
                                com.Parameters.Add("@Data_ID", MySqlDbType.Int16);
                                com.Parameters["@Data_ID"].Value = row1["Data_ID"].ToString();
                                com.Parameters.Add("@Total_Meters", MySqlDbType.Decimal);
                                com.Parameters["@Total_Meters"].Value = neededQuantity;
                                com.Parameters.Add("@Note", MySqlDbType.VarChar);
                                com.Parameters["@Note"].Value = "تعديل تحويل";
                                com.ExecuteNonQuery();
                            }

                            gridView2.AddNewRow();
                            int rowHandle = gridView2.GetRowHandle(gridView2.DataRowCount);
                            if (gridView2.IsNewItemRow(rowHandle) && row1 != null)
                            {
                                gridView2.SetRowCellValue(rowHandle, gridView2.Columns["Data_ID"], row1["Data_ID"].ToString());
                                gridView2.SetRowCellValue(rowHandle, gridView2.Columns["الكود"], row1["الكود"].ToString());
                                gridView2.SetRowCellValue(rowHandle, gridView2.Columns["النوع"], row1["النوع"].ToString());
                                gridView2.SetRowCellValue(rowHandle, gridView2.Columns["الاسم"], row1["الاسم"].ToString());
                                gridView2.SetRowCellValue(rowHandle, gridView2.Columns["الكرتنة"], row1["الكرتنة"].ToString());
                                gridView2.SetRowCellValue(rowHandle, gridView2.Columns["الكمية"], neededQuantity);
                            }

                            gridControl1.DataSource = null;
                            txtQuantity.Text = "";
                            txtCode.Text = "";
                        }
                        else if (neededQuantity > quantity)
                        {
                            MessageBox.Show("لا يوجد كمية كافية");
                        }
                    }
                    else
                    {
                        MessageBox.Show("يجب ادخال جميع البيانات");
                    }
                }
                else
                {
                    MessageBox.Show("يجب اختيار المخازن");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (gridView2.GetSelectedRows().Length > 0)
            {
                if (MessageBox.Show("هل انت متاكد انك تريد الحذف؟", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        dbconnection.Open();
                        dbconnection2.Open();
                        int cont = gridView2.GetSelectedRows().Length;
                        for (int i = 0; i < cont; i++)
                        {
                            int rowhnd = gridView2.GetSelectedRows()[0];
                            DataRow row2 = gridView2.GetDataRow(rowhnd);

                            string query = "SELECT transfer_product_details.Quantity as 'الكمية' FROM transfer_product_details INNER JOIN transfer_product ON transfer_product_details.TransferProduct_ID = transfer_product.TransferProduct_ID left JOIN store as storeTo ON storeTo.Store_ID = transfer_product.To_Store left join store as storeFrom on storeFrom.Store_ID = transfer_product.From_Store INNER JOIN data ON transfer_product_details.Data_ID = data.Data_ID LEFT JOIN color ON color.Color_ID = data.Color_ID LEFT JOIN size ON size.Size_ID = data.Size_ID LEFT JOIN sort ON sort.Sort_ID = data.Sort_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID INNER JOIN factory ON factory.Factory_ID = data.Factory_ID  INNER JOIN product ON product.Product_ID = data.Product_ID  INNER JOIN type ON type.Type_ID = data.Type_ID where transfer_product.TransferProduct_ID=" + TransferProductID + " and data.Data_ID=" + row2[0].ToString() + " and transfer_product.Canceled=0 order by SUBSTR(data.Code, 1, 16),color.Color_Name,data.Description,data.Sort_ID";
                            MySqlCommand com = new MySqlCommand(query, dbconnection2);
                            MySqlDataReader dr = com.ExecuteReader();
                            while (dr.Read())
                            {
                                query = "select sum(Total_Meters) from storage where Data_ID=" + row2[0].ToString() + " and Store_ID=" + comFromStore.SelectedValue.ToString() + " group by Data_ID";
                                com = new MySqlCommand(query, dbconnection);
                                double quantity = Convert.ToDouble(com.ExecuteScalar().ToString());
                                double meters = quantity + Convert.ToDouble(row2["الكمية"].ToString());
                                query = "update storage set Total_Meters=" + meters + " where Data_ID=" + row2[0].ToString() + " and Store_ID=" + comFromStore.SelectedValue.ToString();
                                com = new MySqlCommand(query, dbconnection);
                                com.ExecuteNonQuery();

                                query = "select sum(Total_Meters) from storage where Data_ID=" + row2[0].ToString() + " and Store_ID=" + comToStore.SelectedValue.ToString() + " group by Data_ID";
                                com = new MySqlCommand(query, dbconnection);
                                quantity = Convert.ToDouble(com.ExecuteScalar().ToString());
                                meters = quantity - Convert.ToDouble(row2["الكمية"].ToString());
                                query = "update storage set Total_Meters=" + meters + " where Data_ID=" + row2[0].ToString() + " and Store_ID=" + comToStore.SelectedValue.ToString();
                                com = new MySqlCommand(query, dbconnection);
                                com.ExecuteNonQuery();
                            }
                            dr.Close();

                            query = "delete from transfer_product_details where transfer_product_details.TransferProduct_ID=" + TransferProductID + " and transfer_product_details.Data_ID=" + row2["Data_ID"].ToString();
                            com = new MySqlCommand(query, dbconnection);
                            com.ExecuteNonQuery();
                        }
                        GridView view = gridView2 as GridView;
                        view.DeleteSelectedRows();

                        gridControl1.DataSource = null;
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

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (gridView2.RowCount > 0)
                {
                    #region report
                    List<Transportation_Items> bi = new List<Transportation_Items>();
                    for (int i = 0; i < gridView2.RowCount; i++)
                    {
                        int rowHand = gridView2.GetRowHandle(i);

                        Transportation_Items item = new Transportation_Items() { Code = gridView2.GetRowCellDisplayText(rowHand, gridView2.Columns["الكود"]), Product_Type = gridView2.GetRowCellDisplayText(rowHand, gridView2.Columns["النوع"]), Product_Name = gridView2.GetRowCellDisplayText(rowHand, gridView2.Columns["الاسم"]), Total_Meters = Convert.ToDouble(gridView2.GetRowCellDisplayText(rowHand, gridView2.Columns["الكمية"])) };
                        bi.Add(item);
                    }
                    Report_Transportation_Copy f = new Report_Transportation_Copy();
                    f.PrintInvoice(TransferProductID, comFromStore.Text, comToStore.Text, Date.ToString(), bi);
                    f.ShowDialog();
                    #endregion
                    
                    XtraTabPage xtraTabPage = getTabPage("تعديل بيانات تحويل بنود");
                    tabControlStoresContent.TabPages.Remove(xtraTabPage);
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

        //function
        public XtraTabPage getTabPage(string text)
        {
            for (int i = 0; i < tabControlStoresContent.TabPages.Count; i++)
                if (tabControlStoresContent.TabPages[i].Text == text)
                {
                    return tabControlStoresContent.TabPages[i];
                }
            return null;
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

        public void clear()
        {
            loaded = false;
            comFromStore.SelectedIndex = -1;
            comToStore.DataSource = null;
            txtCode.Text = "";
            txtQuantity.Text = "";
            //cmbPlace.DataSource = null;
            gridControl1.DataSource = null;
            gridControl2.DataSource = null;
            loaded = true;
        }
    }
}

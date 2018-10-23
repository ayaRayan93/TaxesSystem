using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraTab;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MainSystem
{
    public partial class Offer_Update : Form
    {
        MySqlConnection dbconnection, dbconnection1, dbconnection2, dbconnection3;
        bool loaded = false;
        bool factoryFlage = false;
        bool groupFlage = false;
        bool flagProduct = false;
        List<DataRowView> myRows;
        DataRowView row1 = null;
        Offer offerForm;
        byte[] selectedImage = null;
        DataRowView rowOffer = null;
        XtraTabControl tabControlSalesContent;

        public Offer_Update(DataRowView RowOffer, Offer offer, XtraTabControl TabControlSalesContent)
        {
            InitializeComponent();
            dbconnection = new MySqlConnection(connection.connectionString);
            dbconnection1 = new MySqlConnection(connection.connectionString);
            dbconnection2 = new MySqlConnection(connection.connectionString);
            dbconnection3 = new MySqlConnection(connection.connectionString);
            myRows = new List<DataRowView>();
            offerForm = offer;
            rowOffer = RowOffer;
            tabControlSalesContent = TabControlSalesContent;
        }

        private void Form1_Load(object sender, EventArgs e)
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
                txtType.Text = "";
                
                query = "select * from sort";
                da = new MySqlDataAdapter(query, dbconnection);
                dt = new DataTable();
                da.Fill(dt);
                comSort.DataSource = dt;
                comSort.DisplayMember = dt.Columns["Sort_Value"].ToString();
                comSort.ValueMember = dt.Columns["Sort_ID"].ToString();
                comSort.Text = "";

                txtOfferName.Text = rowOffer["اسم العرض"].ToString();
                txtPrice.Text = rowOffer["السعر"].ToString();
                txtDelegatePercent.Text = rowOffer["نسبة المندوب"].ToString();
                txtDescription.Text = rowOffer["الوصف"].ToString();
                
                if (rowOffer["الصورة"].ToString() != "")
                {
                    selectedImage = (byte[])rowOffer["الصورة"];
                    Image image = byteArrayToImage(selectedImage);
                    ImageProduct.Image = image;
                }

                query = "select distinct data.Data_ID,data.Code as 'الكود', product.Product_Name as 'الاسم' , type.Type_Name as 'النوع', factory.Factory_Name as 'المصنع' ,groupo.Group_Name as 'المجموعة' ,color.Color_Name as 'اللون',size.Size_Value as 'المقاس',sort.Sort_Value as 'الفرز',data.Classification as 'التصنيف', data.Description as 'الوصف',sum(storage.Total_Meters) as 'اجمالي عدد الوحدات',offer_details.Quantity as 'الكمية',sellprice.Sell_Price*offer_details.Quantity as 'السعر' from data INNER JOIN type ON type.Type_ID = data.Type_ID INNER JOIN product ON product.Product_ID = data.Product_ID INNER JOIN factory ON data.Factory_ID = factory.Factory_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID LEFT JOIN color ON data.Color_ID = color.Color_ID LEFT JOIN size ON data.Size_ID = size.Size_ID LEFT JOIN sort ON data.Sort_ID = sort.Sort_ID left JOIN storage on storage.Data_ID=data.Data_ID INNER JOIN sellprice on sellprice.Data_ID=data.Data_ID inner join offer_details on offer_details.Data_ID=data.Data_ID inner join offer on offer.Offer_ID=offer_details.offer_ID where offer.Offer_ID=" + rowOffer[0].ToString() + " group by data.Data_ID order by sellprice.Date desc";
                da = new MySqlDataAdapter(query, dbconnection);
                dt = new DataTable();
                da.Fill(dt);
                gridControl2.DataSource = dt;
                gridView2.Columns[0].Visible = false;
                gridView2.Columns["اجمالي عدد الوحدات"].Visible = false;

                loaded = true;
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

                string query = "select distinct data.Data_ID,data.Code as 'الكود', product.Product_Name as 'الاسم' , type.Type_Name as 'النوع', factory.Factory_Name as 'المصنع' ,groupo.Group_Name as 'المجموعة' ,color.Color_Name as 'اللون',size.Size_Value as 'المقاس',sort.Sort_Value as 'الفرز',data.Classification as 'التصنيف', data.Description as 'الوصف',sum(storage.Total_Meters) as 'اجمالي عدد الوحدات',sellprice.Sell_Price as 'السعر' from data INNER JOIN type ON type.Type_ID = data.Type_ID INNER JOIN product ON product.Product_ID = data.Product_ID INNER JOIN factory ON data.Factory_ID = factory.Factory_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID LEFT JOIN color ON data.Color_ID = color.Color_ID LEFT JOIN size ON data.Size_ID = size.Size_ID LEFT JOIN sort ON data.Sort_ID = sort.Sort_ID left JOIN storage on storage.Data_ID=data.Data_ID INNER JOIN sellprice on sellprice.Data_ID=data.Data_ID where  data.Type_ID IN(" + q1 + ") and  data.Factory_ID  IN(" + q2 + ") and data.Product_ID  IN(" + q3 + ") and data.Group_ID IN (" + q4 + ") " + fQuery + " group by data.Data_ID order by sellprice.Date desc";
                MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                DataTable dt = new DataTable();
                da.Fill(dt);
                gridControl1.DataSource = dt;
                gridView1.Columns[0].Visible = false;
                filterRows();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void filterRows()
        {
            if (gridView2.RowCount > 0)
            {
                for (int i = 0; i < gridView2.RowCount; i++)
                {
                    DataRowView item = (DataRowView)gridView2.GetRow(i);
                    for (int j = 0; j < gridView1.RowCount; j++)
                    {
                        DataRowView item1 = (DataRowView)gridView1.GetRow(j);
                        if (item["Data_ID"].ToString() == item1["Data_ID"].ToString())
                            gridView1.DeleteRow(j);
                    }

                }
            }
        }

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
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

        private void ComboBox_SelectedValueChanged(object sender, EventArgs e)
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
                                if (comType.SelectedValue.ToString() == "1")
                                {
                                    string query2 = "select * from groupo where Factory_ID=0 and Type_ID=" + comType.SelectedValue.ToString();
                                    MySqlDataAdapter da2 = new MySqlDataAdapter(query2, dbconnection);
                                    DataTable dt2 = new DataTable();
                                    da2.Fill(dt2);
                                    comGroup.DataSource = dt2;
                                    comGroup.DisplayMember = dt2.Columns["Group_Name"].ToString();
                                    comGroup.ValueMember = dt2.Columns["Group_ID"].ToString();
                                    comGroup.Text = "";
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
                                if (comType.SelectedValue.ToString() != "1")
                                {
                                    string query2f = "select * from groupo where Factory_ID=" + comFactory.SelectedValue.ToString();
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
                                string query3 = "select distinct  product.Product_ID  ,Product_Name  from product inner join product_factory_group on product.Product_ID=product_factory_group.Product_ID  where product.Type_ID=" + comType.SelectedValue.ToString() + " and product_factory_group.Factory_ID=" + comFactory.SelectedValue.ToString() + " and product_factory_group.Group_ID=" + comGroup.SelectedValue.ToString() + "  order by product.Product_ID";
                                MySqlDataAdapter da3 = new MySqlDataAdapter(query3, dbconnection);
                                DataTable dt3 = new DataTable();
                                da3.Fill(dt3);
                                comProduct.DataSource = dt3;
                                comProduct.DisplayMember = dt3.Columns["Product_Name"].ToString();
                                comProduct.ValueMember = dt3.Columns["Product_ID"].ToString();
                                comProduct.Text = "";


                                string query2 = "select * from size where Factory_ID=" + comFactory.SelectedValue.ToString() + " and Group_ID=" + comGroup.SelectedValue.ToString();
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (gridView1.SelectedRowsCount > 0 && !IsAdded(row1))
                {
                    double quantity = 0;
                    if (!double.TryParse(txtQuantityInOffer.Text, out quantity))
                    {
                        MessageBox.Show("الكمية يجب ان تكون عدد");
                        dbconnection.Close();
                        return;
                    }

                    if (row1["اجمالي عدد الوحدات"].ToString() == "")
                    {
                        MessageBox.Show("لا يوجد كمية كافية من البند");
                        dbconnection.Close();
                        return;
                    }

                    if (quantity > Convert.ToDouble(row1["اجمالي عدد الوحدات"].ToString()))
                    {
                        MessageBox.Show("لا يوجد كمية كافية من البند");
                        dbconnection.Close();
                        return;
                    }

                    if (gridView2.SelectedRowsCount == 0)
                    {
                        string query = "select distinct data.Data_ID,data.Code as 'الكود', product.Product_Name as 'الاسم' , type.Type_Name as 'النوع', factory.Factory_Name as 'المصنع' ,groupo.Group_Name as 'المجموعة' ,color.Color_Name as 'اللون',size.Size_Value as 'المقاس',sort.Sort_Value as 'الفرز',data.Classification as 'التصنيف', data.Description as 'الوصف',sum(storage.Total_Meters) as 'اجمالي عدد الوحدات',sum(storage.Total_Meters) as 'الكمية',sellprice.Sell_Price as 'السعر' from data INNER JOIN type ON type.Type_ID = data.Type_ID INNER JOIN product ON product.Product_ID = data.Product_ID INNER JOIN factory ON data.Factory_ID = factory.Factory_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID LEFT JOIN color ON data.Color_ID = color.Color_ID LEFT JOIN size ON data.Size_ID = size.Size_ID LEFT JOIN sort ON data.Sort_ID = sort.Sort_ID left JOIN storage on storage.Data_ID=data.Data_ID INNER JOIN sellprice on sellprice.Data_ID=data.Data_ID where data.Data_ID=0 group by data.Data_ID order by sellprice.Date desc";
                        MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        gridControl2.DataSource = dt;
                        gridView2.Columns[0].Visible = false;
                        gridView2.Columns["اجمالي عدد الوحدات"].Visible = false;
                    }

                    myRows.Add(row1);

                    gridView2.AddNewRow();
                    int rowHandle = gridView2.GetRowHandle(gridView2.DataRowCount);
                    if (gridView2.IsNewItemRow(rowHandle))
                    {
                        gridView2.SetRowCellValue(rowHandle, gridView2.Columns["Data_ID"], row1["Data_ID"]);
                        gridView2.SetRowCellValue(rowHandle, gridView2.Columns["الكود"], row1["الكود"]);
                        gridView2.SetRowCellValue(rowHandle, gridView2.Columns["الاسم"], row1["الاسم"]);
                        gridView2.SetRowCellValue(rowHandle, gridView2.Columns["النوع"], row1["النوع"]);
                        gridView2.SetRowCellValue(rowHandle, gridView2.Columns["المصنع"], row1["المصنع"]);
                        gridView2.SetRowCellValue(rowHandle, gridView2.Columns["المجموعة"], row1["المجموعة"]);
                        gridView2.SetRowCellValue(rowHandle, gridView2.Columns["اللون"], row1["اللون"]);
                        gridView2.SetRowCellValue(rowHandle, gridView2.Columns["المقاس"], row1["المقاس"]);
                        gridView2.SetRowCellValue(rowHandle, gridView2.Columns["الفرز"], row1["الفرز"]);
                        gridView2.SetRowCellValue(rowHandle, gridView2.Columns["التصنيف"], row1["التصنيف"]);
                        gridView2.SetRowCellValue(rowHandle, gridView2.Columns["الوصف"], row1["الوصف"]);
                        gridView2.SetRowCellValue(rowHandle, gridView2.Columns["السعر"], Convert.ToDouble(row1["السعر"].ToString()) * quantity);
                        gridView2.SetRowCellValue(rowHandle, gridView2.Columns["الكمية"], quantity);
                        gridView2.SetRowCellValue(rowHandle, gridView2.Columns["اجمالي عدد الوحدات"], row1["اجمالي عدد الوحدات"]);
                    }
                    
                    gridView1.DeleteSelectedRows();

                    if (gridView2.IsLastVisibleRow)
                    {
                        gridView2.FocusedRowHandle = gridView2.RowCount - 1;
                    }

                    txtQuantityInOffer.Text = "";
                    txtCode.Text = "";
                }
                else
                {
                    MessageBox.Show("تاكد من اختيار عنصر لم يتم اختيارة من قبل");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        
        bool IsAdded(DataRowView row1)
        {
            foreach (DataRowView item in myRows)
            {
                if (row1 == item)
                    return true;
            }
            return false;
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            try
            {
                if (gridView2.SelectedRowsCount > 0)
                {
                    DataRowView mRow = (DataRowView)gridView2.GetRow(gridView2.GetSelectedRows()[0]);
                    
                    gridView1.AddNewRow();
                    int rowHandle = gridView1.GetRowHandle(gridView1.DataRowCount);
                    if (gridView1.IsNewItemRow(rowHandle))
                    {
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns["Data_ID"], mRow["Data_ID"]);
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns["الكود"], mRow["الكود"]);
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns["الاسم"], mRow["الاسم"]);
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns["النوع"], mRow["النوع"]);
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns["المصنع"], mRow["المصنع"]);
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns["المجموعة"], mRow["المجموعة"]);
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns["اللون"], mRow["اللون"]);
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns["المقاس"], mRow["المقاس"]);
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns["الفرز"], mRow["الفرز"]);
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns["التصنيف"], mRow["التصنيف"]);
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns["الوصف"], mRow["الوصف"]);
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns["السعر"], Convert.ToDouble(mRow["السعر"].ToString()) / Convert.ToDouble(mRow["الكمية"].ToString()));
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns["اجمالي عدد الوحدات"], mRow["اجمالي عدد الوحدات"]);
                    }
                    
                    gridView2.DeleteRow(gridView2.GetSelectedRows()[0]);
                   
                    for (int i = 0; i < myRows.Count; i++)
                    {
                        if (myRows[i] == mRow)
                            myRows.Remove(mRow);
                    }
                }
                else
                {
                    MessageBox.Show("يجب اختيار عنصر");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnCreateOffer_Click(object sender, EventArgs e)
        {
            try
            {
                if (gridView2.SelectedRowsCount > 0 && txtPrice.Text != "")
                {
                    double price = 0;
                    if (!double.TryParse(txtPrice.Text, out price))
                    {
                        MessageBox.Show("السعر يجب ان يكون عدد");
                        dbconnection.Close();
                        return;
                    }
                    
                    dbconnection.Open();
                    string query = "update offer set Price=@Price,Delegate_Percent=@Delegate_Percent,Description=@Description where Offer_ID=" + rowOffer[0].ToString();
                    MySqlCommand com = new MySqlCommand(query, dbconnection);
                    com.Parameters.Add("@Price", MySqlDbType.Decimal);
                    com.Parameters["@Price"].Value = price;
                    com.Parameters.Add("@Delegate_Percent", MySqlDbType.Decimal);
                    com.Parameters["@Delegate_Percent"].Value = txtDelegatePercent.Text;
                    com.Parameters.Add("@Description", MySqlDbType.VarChar);
                    com.Parameters["@Description"].Value = txtDescription.Text;
                    com.ExecuteNonQuery();

                    //فك كمية البنود
                    dbconnection3.Open();
                    query = "select storage.Store_ID,sum(Total_Meters) as 'الكمية' from storage  where Offer_ID=" + rowOffer[0].ToString() + " group by storage.Store_ID,storage.Offer_ID";
                    com = new MySqlCommand(query, dbconnection3);
                    MySqlDataReader dr = com.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            increaseItemsQuantityInDB(Convert.ToDouble(dr["الكمية"].ToString()), Convert.ToInt16(rowOffer[0].ToString()), Convert.ToInt16(dr["Store_ID"].ToString()));
                        }
                        dr.Close();
                    }
                    ////////////////////

                    query = "delete from offer_details where Offer_ID=" + rowOffer[0].ToString();
                    com = new MySqlCommand(query, dbconnection);
                    com.ExecuteNonQuery();

                    for (int i = 0; i < gridView2.RowCount; i++)
                    {
                        DataRowView item = (DataRowView)gridView2.GetRow(i);
                        query = "insert offer_details (Offer_ID,Data_ID,Quantity) values (@Offer_ID,@Data_ID,@Quantity)";
                        com = new MySqlCommand(query, dbconnection);
                        com.Parameters.Add("@Offer_ID", MySqlDbType.Int16);
                        com.Parameters["@Offer_ID"].Value = rowOffer[0].ToString();
                        com.Parameters.Add("@Data_ID", MySqlDbType.Int16);
                        com.Parameters["@Data_ID"].Value = Convert.ToInt16(item[0].ToString());
                        com.Parameters.Add("@Quantity", MySqlDbType.Decimal);
                        com.Parameters["@Quantity"].Value = Convert.ToDouble(item["الكمية"].ToString());
                        com.ExecuteNonQuery();
                    }

                    if (selectedImage != null)
                    {
                        //selectedImage = ImageToByte(ImageProduct.Image);
                        query = "select Photo from offer_photo where Offer_ID=" + rowOffer[0].ToString();
                        com = new MySqlCommand(query, dbconnection);
                        if (com.ExecuteScalar() != null)
                        {
                            query = "update offer_photo set Photo=@Photo where Offer_ID=" + rowOffer[0].ToString();
                            com = new MySqlCommand(query, dbconnection);
                            com.Parameters.Add("@Photo", MySqlDbType.LongBlob);
                            com.Parameters["@Photo"].Value = selectedImage;
                            com.ExecuteNonQuery();
                        }
                        else
                        {
                            query = "insert into offer_photo (Offer_ID,Photo) values (@Offer_ID,@Photo)";
                            com = new MySqlCommand(query, dbconnection);
                            com.Parameters.Add("@Offer_ID", MySqlDbType.Int16);
                            com.Parameters["@Offer_ID"].Value = rowOffer[0].ToString();
                            com.Parameters.Add("@Photo", MySqlDbType.LongBlob);
                            com.Parameters["@Photo"].Value = selectedImage;
                            com.ExecuteNonQuery();
                        }
                    }
                    else
                    {
                        query = "select Photo from offer_photo where Offer_ID=" + rowOffer[0].ToString();
                        com = new MySqlCommand(query, dbconnection);
                        if (com.ExecuteScalar() != null)
                        {
                            query = "delete from offer_photo where Offer_ID=" + rowOffer[0].ToString();
                            com = new MySqlCommand(query, dbconnection);
                            com.ExecuteNonQuery();
                        }
                    }

                    UserControl.ItemRecord("offer", "تعديل", Convert.ToInt16(rowOffer[0].ToString()), DateTime.Now, "", dbconnection);

                    //clear(tableLayoutPanel1);
                    offerForm.DisplayOffer();
                    offerForm.loadDataToBox();
                    XtraTabPage xtraTabPage = getTabPage("تعديل عرض");
                    tabControlSalesContent.TabPages.Remove(xtraTabPage);
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
            dbconnection.Close();
            dbconnection1.Close();
            dbconnection2.Close();
            dbconnection3.Close();
        }

        private void gridView1_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            try
            {
                row1 = (DataRowView)gridView1.GetRow(gridView1.GetSelectedRows()[0]);
                
                txtCode.Text = row1["الكود"].ToString();
                txtQuantityInOffer.Text = "1";
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
                foreach (Control item in panel3.Controls)
                {
                    if (item is TextBox)
                    {
                        item.Text = "";
                    }
                    else if (item is ComboBox)
                    {
                        item.Text = "";
                    }
                }
                gridControl1.DataSource = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            try
            {
                selectedImage = null;
                ImageProduct.Image = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //functions
        public void clear(Control tlp)
        {
            foreach (Control co in tlp.Controls)
            {
                if (co is Panel || co is TableLayoutPanel)
                {
                    foreach (Control item in co.Controls)
                    {
                        if (item is System.Windows.Forms.ComboBox)
                        {
                            item.Text = "";
                        }
                        else if (item is TextBox)
                        {
                            item.Text = "";
                        }
                    }
                }
                gridControl1.DataSource = null;
                gridControl2.DataSource = null;
                ImageProduct.Image = null;
            }
        }
        
        public void increaseItemsQuantityInDB(double offerQuantity, int offerID, int storeID)
        {
            if (offerQuantity > 0)
            {
                dbconnection1.Open();
                dbconnection2.Open();
                string query = "select Data_ID,Quantity from offer_details  where Offer_ID=" + offerID;
                MySqlCommand com = new MySqlCommand(query, dbconnection);
                MySqlDataReader dr = com.ExecuteReader();
                while (dr.Read())
                {
                    query = "select sum(Total_Meters) from storage where Data_ID='" + dr["Data_ID"].ToString() + "' group by Store_ID having Store_ID=" + storeID;
                    MySqlCommand com1 = new MySqlCommand(query, dbconnection1);
                    double QuantityInStore = Convert.ToDouble(com1.ExecuteScalar());
                    double QuantityInOffer = Convert.ToDouble(dr["Quantity"].ToString());
                    double newQuantity = QuantityInOffer * offerQuantity;

                    query = "select Storage_ID,Total_Meters from storage where Data_ID='" + dr["Data_ID"].ToString() + "' and Store_ID=" + storeID;
                    com1 = new MySqlCommand(query, dbconnection1);
                    MySqlDataReader dr2 = com1.ExecuteReader();
                    int id = 0;
                    while (dr2.Read())
                    {
                        double storageQ = Convert.ToDouble(dr2["Total_Meters"]);
                        //if (storageQ > newQuantity)
                        //{
                        id = Convert.ToInt16(dr2["Storage_ID"]);
                        query = "update storage set Total_Meters=" + (storageQ + newQuantity) + " where Storage_ID=" + id;
                        MySqlCommand comm = new MySqlCommand(query, dbconnection2);
                        comm.ExecuteNonQuery();
                        break;
                        //}

                    }
                    dr2.Close();
                }
                dr.Close();
            }
            dbconnection1.Close();
            dbconnection2.Close();
        }

        private void ImageProduct_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog1 = new OpenFileDialog();
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    string selectedFile = openFileDialog1.FileName;
                    selectedImage = File.ReadAllBytes(selectedFile);
                    ImageProduct.Image = Image.FromFile(openFileDialog1.FileName);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public Image byteArrayToImage(byte[] byteArrayIn)
        {
            MemoryStream ms = new MemoryStream(byteArrayIn);
            Image returnImage = Image.FromStream(ms);
            return returnImage;
        }

        public static byte[] ImageToByte(Image img)
        {
            ImageConverter converter = new ImageConverter();
            return (byte[])converter.ConvertTo(img, typeof(byte[]));
        }

        public XtraTabPage getTabPage(string text)
        {
            for (int i = 0; i < tabControlSalesContent.TabPages.Count; i++)
                if (tabControlSalesContent.TabPages[i].Text == text)
                {
                    return tabControlSalesContent.TabPages[i];
                }
            return null;
        }
    }
}

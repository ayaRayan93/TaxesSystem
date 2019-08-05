using DevExpress.XtraGrid.Views.Grid;
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
    public partial class TransportationStore_Update : Form
    {
        MySqlConnection dbconnection;
        bool loaded = false;
        bool factoryFlage = false;
        bool groupFlage = false;
        bool flagProduct = false;
        DataRow row1;
        int CustomerBillID = 0;

        public TransportationStore_Update(DataRow rows, Transportation_Report transportationReport, XtraTabControl xtraTabControlStoresContent)
        {
            InitializeComponent();
            dbconnection = new MySqlConnection(connection.connectionString);
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
                comFromStore.Text = "";

                query = "select * from type";
                da = new MySqlDataAdapter(query, dbconnection);
                dt = new DataTable();
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

                query = "select * from branch";
                da = new MySqlDataAdapter(query, dbconnection);
                dt = new DataTable();
                da.Fill(dt);
                comBranch.DataSource = dt;
                comBranch.DisplayMember = dt.Columns["Branch_Name"].ToString();
                comBranch.ValueMember = dt.Columns["Branch_ID"].ToString();
                comBranch.Text = "";

                loaded = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cmbFromStore_SelectedValueChanged(object sender, EventArgs e)
        {
            if (loaded)
            {
                string query = "select * from Store where Store_ID<>" + comFromStore.SelectedValue.ToString();
                MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                DataTable dt = new DataTable();
                da.Fill(dt);
                comToStore.DataSource = dt;
                comToStore.DisplayMember = dt.Columns["Store_Name"].ToString();
                comToStore.ValueMember = dt.Columns["Store_ID"].ToString();
                comToStore.Text = "";

                txtCode.Text = "";
                txtQuantity.Text = "";
                comBranch.SelectedIndex = -1;
                txtBillNum.Text = "";
                cmbPlace.DataSource = null;
                gridControl1.DataSource = null;
                gridControl2.DataSource = null;
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
                    CustomerBillID = 0;
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
                        query = "select data.Data_ID,data.Code as 'الكود',type.Type_Name as 'النوع',concat(product.Product_Name,' - ',type.Type_Name,' - ',factory.Factory_Name,' - ',groupo.Group_Name,' ',COALESCE(color.Color_Name,''),' ',COALESCE(size.Size_Value,''),' ',COALESCE(sort.Sort_Value,'')) as 'الاسم',data.Carton as 'الكرتنة',sum(storage.Total_Meters) as 'الكمية','رقم الفاتورة','CustomerBill_ID' FROM data LEFT JOIN color ON color.Color_ID = data.Color_ID LEFT JOIN size ON size.Size_ID = data.Size_ID LEFT JOIN sort ON sort.Sort_ID = data.Sort_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID INNER JOIN factory ON factory.Factory_ID = data.Factory_ID  INNER JOIN product ON product.Product_ID = data.Product_ID  INNER JOIN type ON type.Type_ID = data.Type_ID  INNER JOIN sellprice ON sellprice.Data_ID = data.Data_ID LEFT JOIN storage ON storage.Data_ID = data.Data_ID where  data.Type_ID IN(" + q1 + ") and  data.Factory_ID  IN(" + q2 + ") and data.Group_ID IN (" + q4 + ") and data.Data_ID=0 group by data.Data_ID";
                        da = new MySqlDataAdapter(query, dbconnection);
                        dt = new DataTable();
                        da.Fill(dt);
                        gridControl2.DataSource = dt;
                        gridView2.Columns[0].Visible = false;
                        gridView2.Columns["CustomerBill_ID"].Visible = false;
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
                comBranch.SelectedIndex = -1;
                txtBillNum.Text = "";
                txtQuantity.ReadOnly = false;
                CustomerBillID = 0;

                #region MyRegion
                /*string store = "";
                if (comFromStore.Text != "")
                {
                    store = comFromStore.SelectedValue.ToString();
                }
                else
                {
                    store = "select Store_ID from Store";
                }
                string query = "select Store_Place,Storage_ID from Storage where Code='" + txtCode.Text + "' and Store_ID in (" + store + ") order by Storage_Date asc";
                MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                DataTable dt = new DataTable();
                da.Fill(dt);
                cmbPlace.DataSource = dt;
                cmbPlace.DisplayMember = dt.Columns["Store_Place"].ToString();
                cmbPlace.ValueMember = dt.Columns["Storage_ID"].ToString();
                cmbPlace.Text = row1[12].Value.ToString();
                txtQuantity.BackColor = System.Drawing.Color.White;*/
                #endregion
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

                    if (txtQuantity.Text != ""/* && txtBalat.Text != "" && txtCarton.Text != ""*/)
                    {
                        double neededQuantity = 0;
                        //int balatat, numCartons = 0;
                        if (!double.TryParse(txtQuantity.Text, out neededQuantity)/* || !int.TryParse(txtBalat.Text, out balatat) || !int.TryParse(txtCarton.Text, out numCartons)*/)
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
                                if (comBranch.Text != "" && txtBillNum.Text != "")
                                {
                                    gridView2.SetRowCellValue(rowHandle, gridView2.Columns["رقم الفاتورة"], comBranch.Text + " " + txtBillNum.Text);
                                    gridView2.SetRowCellValue(rowHandle, gridView2.Columns["CustomerBill_ID"], CustomerBillID);
                                }
                                else
                                {
                                    gridView2.SetRowCellValue(rowHandle, gridView2.Columns["رقم الفاتورة"], "");
                                    gridView2.SetRowCellValue(rowHandle, gridView2.Columns["CustomerBill_ID"], 0);
                                }
                                //gridView2.SetRowCellValue(rowHandle, gridView2.Columns["عدد البلتات"], balatat);
                                //gridView2.SetRowCellValue(rowHandle, gridView2.Columns["عدد الكراتين"], numCartons);
                            }

                            txtQuantity.Text = "";
                            txtCode.Text = "";
                            txtBillNum.Text = "";
                            comBranch.SelectedIndex = -1;
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
                        GridView view = gridView2 as GridView;
                        view.DeleteSelectedRows();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (gridView2.RowCount > 0 && comFromStore.Text != "" && comToStore.Text != "" && comFromStore.SelectedValue != null && comToStore.SelectedValue != null)
                {
                    dbconnection.Open();
                    string query = "insert into transfer_product (From_Store,To_Store,Date) values (@From_Store,@To_Store,@Date)";
                    MySqlCommand com = new MySqlCommand(query, dbconnection);
                    com.Parameters.Add("@From_Store", MySqlDbType.Int16);
                    com.Parameters["@From_Store"].Value = comFromStore.SelectedValue.ToString();
                    com.Parameters.Add("@To_Store", MySqlDbType.Int16);
                    com.Parameters["@To_Store"].Value = comToStore.SelectedValue.ToString();
                    com.Parameters.Add("@Date", MySqlDbType.DateTime);
                    com.Parameters["@Date"].Value = DateTime.Now;
                    com.ExecuteNonQuery();

                    query = "select TransferProduct_ID from transfer_product order by TransferProduct_ID desc limit 1";
                    com = new MySqlCommand(query, dbconnection);
                    int transferProductID = Convert.ToInt32(com.ExecuteScalar().ToString());

                    for (int i = 0; i < gridView2.RowCount; i++)
                    {
                        int rowhnd = gridView2.GetRowHandle(i);
                        DataRow row2 = gridView2.GetDataRow(rowhnd);
                        query = "insert into transfer_product_details (Data_ID,Quantity,TransferProduct_ID,CustomerBill_ID) values (@Data_ID,@Quantity,@TransferProduct_ID,@CustomerBill_ID)";
                        com = new MySqlCommand(query, dbconnection);
                        com.Parameters.Add("@Data_ID", MySqlDbType.Int16);
                        com.Parameters["@Data_ID"].Value = row2["Data_ID"].ToString();
                        //com.Parameters.Add("@Balatat", MySqlDbType.Int16);
                        //com.Parameters["@Balatat"].Value = row2["عدد البلتات"].ToString();
                        //com.Parameters.Add("@Carton_Balata", MySqlDbType.Int16);
                        //com.Parameters["@Carton_Balata"].Value = row2["عدد الكراتين"].ToString();
                        com.Parameters.Add("@Quantity", MySqlDbType.Decimal);
                        com.Parameters["@Quantity"].Value = row2["الكمية"].ToString();
                        com.Parameters.Add("@TransferProduct_ID", MySqlDbType.Int16);
                        com.Parameters["@TransferProduct_ID"].Value = transferProductID;
                        com.Parameters.Add("@CustomerBill_ID", MySqlDbType.Int16);
                        com.Parameters["@CustomerBill_ID"].Value = row2["CustomerBill_ID"].ToString();
                        com.ExecuteNonQuery();
                        
                        if (row2["رقم الفاتورة"].ToString() == "")
                        {
                            query = "select sum(Total_Meters) from storage where Data_ID=" + row2[0].ToString() + " and Store_ID=" + comFromStore.SelectedValue.ToString() + " group by Data_ID";
                            com = new MySqlCommand(query, dbconnection);
                            double quantity = Convert.ToDouble(com.ExecuteScalar().ToString());
                            double meters = quantity - Convert.ToDouble(row2["الكمية"].ToString());
                            query = "update storage set Total_Meters=" + meters + " where Data_ID=" + row2[0].ToString() + " and Store_ID=" + comFromStore.SelectedValue.ToString();
                            com = new MySqlCommand(query, dbconnection);
                            com.ExecuteNonQuery();

                            query = "select sum(Total_Meters) from storage where Data_ID=" + row2[0].ToString() + " and Store_ID=" + comToStore.SelectedValue.ToString() + " group by Data_ID";
                            com = new MySqlCommand(query, dbconnection);
                            if (com.ExecuteScalar() != null)
                            {
                                quantity = Convert.ToDouble(com.ExecuteScalar().ToString());
                                meters = quantity + Convert.ToDouble(row2["الكمية"].ToString());
                                query = "update storage set Total_Meters=" + meters + " where Data_ID=" + row2[0].ToString() + " and Store_ID=" + comToStore.SelectedValue.ToString();
                                com = new MySqlCommand(query, dbconnection);
                                com.ExecuteNonQuery();
                            }
                            else
                            {
                                query = "insert into storage (Store_ID,Storage_Date,Type,Data_ID,Total_Meters) values (@Store_ID,@Storage_Date,@Type,@Data_ID,@Total_Meters)";
                                com = new MySqlCommand(query, dbconnection);
                                com.Parameters.Add("@Store_ID", MySqlDbType.Int16);
                                com.Parameters["@Store_ID"].Value = comToStore.SelectedValue.ToString();
                                com.Parameters.Add("@Storage_Date", MySqlDbType.DateTime);
                                com.Parameters["@Storage_Date"].Value = DateTime.Now;
                                com.Parameters.Add("@Type", MySqlDbType.VarChar);
                                com.Parameters["@Type"].Value = "بند";
                                com.Parameters.Add("@Data_ID", MySqlDbType.Int16);
                                com.Parameters["@Data_ID"].Value = row2["Data_ID"].ToString();
                                com.Parameters.Add("@Total_Meters", MySqlDbType.Decimal);
                                com.Parameters["@Total_Meters"].Value = row2["الكمية"].ToString();
                                com.ExecuteNonQuery();
                            }
                        }
                    }

                    #region report
                    List<Transportation_Items> bi = new List<Transportation_Items>();
                    for (int i = 0; i < gridView2.RowCount; i++)
                    {
                        int rowHand = gridView2.GetRowHandle(i);

                        Transportation_Items item = new Transportation_Items() { Code = gridView2.GetRowCellDisplayText(rowHand, gridView2.Columns["الكود"]), Product_Type = gridView2.GetRowCellDisplayText(rowHand, gridView2.Columns["النوع"]), Product_Name = gridView2.GetRowCellDisplayText(rowHand, gridView2.Columns["الاسم"]), Total_Meters = Convert.ToDouble(gridView2.GetRowCellDisplayText(rowHand, gridView2.Columns["الكمية"])) };
                        bi.Add(item);
                    }
                    Report_Transportation f = new Report_Transportation();
                    f.PrintInvoice(transferProductID, comFromStore.Text, comToStore.Text, bi);
                    f.ShowDialog();
                    #endregion

                    clear();
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
            dbconnection.Close();
        }

        //function
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
            comBranch.SelectedIndex = -1;
            txtBillNum.Text = "";
            //cmbPlace.DataSource = null;
            gridControl1.DataSource = null;
            gridControl2.DataSource = null;
            loaded = true;
        }

        private void txtBillNum_TextChanged(object sender, EventArgs e)
        {
            if (row1 != null && txtBillNum.Text != "" && comBranch.Text != "")
            {
                txtQuantity.ReadOnly = true;
                try
                {
                    dbconnection.Open();
                    string query = "SELECT product_bill.Quantity,product_bill.CustomerBill_ID FROM product_bill INNER JOIN customer_bill ON product_bill.CustomerBill_ID = customer_bill.CustomerBill_ID where product_bill.Data_ID=" + row1[0].ToString() + " and customer_bill.Branch_ID=" + comBranch.SelectedValue.ToString() + " and customer_bill.Branch_BillNumber=" + txtBillNum.Text;
                    MySqlCommand com = new MySqlCommand(query, dbconnection);
                    MySqlDataReader dr = com.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            txtQuantity.ReadOnly = true;
                            txtQuantity.Text = dr["Quantity"].ToString();
                            CustomerBillID = Convert.ToInt16(dr["CustomerBill_ID"].ToString());
                        }
                    }
                    else
                    {
                        txtQuantity.ReadOnly = true;
                        txtQuantity.Text = "";
                        CustomerBillID = 0;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                dbconnection.Close();
            }
            else
            {
                txtQuantity.Text = "";
                txtQuantity.ReadOnly = false;
                CustomerBillID = 0;
            }
        }
    }
}

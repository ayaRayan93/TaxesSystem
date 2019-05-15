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
    public partial class TransportationStore : Form
    {
        MySqlConnection dbconnection;
        bool loaded = false;
        bool factoryFlage = false;
        bool groupFlage = false;
        bool flagProduct = false;
        DataRow row1;

        public TransportationStore(MainForm mainForm)
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
                                int TypeCoding_Method = Convert.ToInt16(com.ExecuteScalar());
                                if (TypeCoding_Method == 1)
                                {
                                    string query2 = "";
                                    if (txtType.Text == "2" || txtType.Text == "1")
                                    {
                                        query2 = "select * from groupo where Factory_ID=-1";
                                    }
                                    else
                                    {
                                        query2 = "select * from groupo where Factory_ID=" + -Convert.ToInt16(txtType.Text) + " and Type_ID=" + txtType.Text;
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
                if (comFromStore.Text != "")
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

                    query = "SELECT data.Data_ID,data.Code as 'الكود',type.Type_Name as 'النوع',concat(product.Product_Name,' ',COALESCE(color.Color_Name,''),' ',data.Description,' ',groupo.Group_Name,' ',factory.Factory_Name,' ',COALESCE(size.Size_Value,''),' ',COALESCE(sort.Sort_Value,'')) as 'الاسم',data.Carton as 'الكرتنة',sum(storage.Total_Meters) as 'الكمية' FROM data LEFT JOIN color ON color.Color_ID = data.Color_ID LEFT JOIN size ON size.Size_ID = data.Size_ID LEFT JOIN sort ON sort.Sort_ID = data.Sort_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID INNER JOIN factory ON factory.Factory_ID = data.Factory_ID  INNER JOIN product ON product.Product_ID = data.Product_ID  INNER JOIN type ON type.Type_ID = data.Type_ID  LEFT JOIN storage ON storage.Data_ID = data.Data_ID where data.Type_ID IN(" + q1 + ") and data.Factory_ID IN(" + q2 + ") and data.Product_ID IN (" + q3 + ") and data.Group_ID IN (" + q4 + ") " + fQuery + " and storage.Store_ID=" + comFromStore.SelectedValue.ToString() + " group by data.Data_ID order by SUBSTR(data.Code,1,16),color.Color_Name,data.Sort_ID";
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
                    MessageBox.Show("يجب اختيار المخزن");
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
                txtQuantity.Text = row1["الكمية"].ToString();

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

                string store = "";

                if (comFromStore.Text != "")
                {
                    store = comFromStore.SelectedValue.ToString();
                }
                else
                {
                    store = "select Store_ID from Store";
                }

                if (comFromStore.Text != "" && comToStore.Text != "")
                {
                    if (txtQuantity.Text != "")
                    {
                        string query = "select Total_Meters from storage where Code='" + txtCode.Text + "' and Store_Place='" + cmbPlace.Text + "' and Store_ID in (" + store + ")";
                        MySqlCommand comand = new MySqlCommand(query, dbconnection);
                        double quantity = Convert.ToDouble(comand.ExecuteScalar().ToString());

                        double neededQuantity = Convert.ToDouble(txtQuantity.Text);
                        if (neededQuantity < quantity)
                        {
                            double meters = quantity - neededQuantity;
                            query = "update storage set Total_Meters=" + meters + " where Code='" + txtCode.Text + "' and Store_Place='" + cmbPlace.Text + "' and Store_ID in (" + store + ")";
                            comand = new MySqlCommand(query, dbconnection);
                            comand.ExecuteNonQuery();

                            /*int n = dataGridView2.Rows.Add();
                            dataGridView2.Rows[n].Cells[0].Value = row1.Cells[0].Value.ToString();
                            dataGridView2.Rows[n].Cells[1].Value = txtQuantity.Text;
                            dataGridView2.Rows[n].Cells[2].Value = cmbPlace.Text;
                            dataGridView2.Rows[n].Cells[3].Value = row1.Cells[1].Value.ToString();
                            dataGridView2.Rows[n].Cells[4].Value = row1.Cells[2].Value.ToString();
                            dataGridView2.Rows[n].Cells[5].Value = row1.Cells[3].Value.ToString();
                            dataGridView2.Rows[n].Cells[6].Value = row1.Cells[4].Value.ToString();
                            dataGridView2.Rows[n].Cells[7].Value = row1.Cells[5].Value.ToString();
                            dataGridView2.Rows[n].Cells[8].Value = row1.Cells[6].Value.ToString();
                            dataGridView2.Rows[n].Cells[9].Value = row1.Cells[7].Value.ToString();
                            dataGridView2.Rows[n].Cells[10].Value = row1.Cells[8].Value.ToString();
                            dataGridView2.Rows[n].Cells[11].Value = row1.Cells[9].Value.ToString();
                            dataGridView2.Rows[n].Cells[12].Value = row1.Cells[10].Value.ToString();
                            */
                            dbconnection.Close();
                            search();

                            txtQuantity.Text = "";
                            txtQuantity.BackColor = System.Drawing.Color.White;
                        }
                        else if (neededQuantity == quantity)
                        {
                            query = "delete from storage where Code='" + txtCode.Text + "' and Store_Place='" + cmbPlace.Text + "' and Store_ID in (" + store + ")";
                            comand = new MySqlCommand(query, dbconnection);
                            comand.ExecuteNonQuery();

                            /*int n = dataGridView2.Rows.Add();
                            dataGridView2.Rows[n].Cells[0].Value = row1.Cells[0].Value.ToString();
                            dataGridView2.Rows[n].Cells[1].Value = txtQuantity.Text;
                            dataGridView2.Rows[n].Cells[2].Value = cmbPlace.Text;
                            dataGridView2.Rows[n].Cells[3].Value = row1.Cells[1].Value.ToString();
                            dataGridView2.Rows[n].Cells[4].Value = row1.Cells[2].Value.ToString();
                            dataGridView2.Rows[n].Cells[5].Value = row1.Cells[3].Value.ToString();
                            dataGridView2.Rows[n].Cells[6].Value = row1.Cells[4].Value.ToString();
                            dataGridView2.Rows[n].Cells[7].Value = row1.Cells[5].Value.ToString();
                            dataGridView2.Rows[n].Cells[8].Value = row1.Cells[6].Value.ToString();
                            dataGridView2.Rows[n].Cells[9].Value = row1.Cells[7].Value.ToString();
                            dataGridView2.Rows[n].Cells[10].Value = row1.Cells[8].Value.ToString();
                            dataGridView2.Rows[n].Cells[11].Value = row1.Cells[9].Value.ToString();
                            dataGridView2.Rows[n].Cells[12].Value = row1.Cells[10].Value.ToString();
                            */
                            dbconnection.Close();
                            search();

                            txtQuantity.Text = "";
                            txtQuantity.BackColor = System.Drawing.Color.White;
                        }
                        else if (neededQuantity > quantity)
                        {
                            MessageBox.Show("You only have " + quantity + " in this place");

                            txtQuantity.BackColor = System.Drawing.Color.Red;
                        }
                    }
                    else
                    {
                        MessageBox.Show("please enter the quantity you need");
                    }
                }
                else
                {
                    MessageBox.Show("please fill all fields");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }

        void search()
        {
            try
            {
                dbconnection.Open();
                string qt;
                string qp;
                string qg;
                string qf;
                string query;
                string store = "";

                if (comFromStore.Text != "")
                {
                    store = comFromStore.SelectedValue.ToString();
                }
                else
                {
                    store = "select Store_ID from Store";
                }

                if (txtType.Text == "")
                {
                    qt = "select Type_ID from Type";
                }
                else
                {
                    qt = txtType.Text;
                }

                if (txtGroup.Text == "")
                {
                    qg = "select Group_ID from Groupo";
                }
                else
                {
                    qg = txtGroup.Text;
                }

                if (txtFactory.Text == "")
                {
                    qf = "select Factory_ID from Factory";
                }
                else
                {
                    qf = txtFactory.Text;
                }

                if (txtProduct.Text == "")
                {
                    qp = "select Product_ID from Product";
                }
                else
                {
                    qp = txtProduct.Text;
                }

                query = "SELECT data.Code as 'الكود',type.Type_Name as 'النوع',factory.Factory_Name as 'المصنع',groupo.Group_Name as 'المجموعة',product.Product_Name as 'الصنف',data.Colour as 'اللون',data.Size as 'المقاس',data.Sort as 'الفرز',data.Classification as 'التصنيف',data.Carton as 'الكرتنة',data.Description as 'الوصف',sum(storage.Total_Meters) as 'الكمية',storage.Store_Place as 'مكان التخزين' FROM data INNER JOIN type ON type.Type_ID = data.Type_ID INNER JOIN factory ON factory.Factory_ID = data.Factory_ID INNER JOIN groupo ON groupo.Group_ID = data.Group_ID INNER JOIN product ON product.Product_ID = data.Product_ID INNER JOIN storage ON storage.Code = data.Code where data.Type_ID in (" + qt + ") and data.Group_ID in (" + qg + ") and data.Product_ID in (" + qp + ") and data.Factory_ID in (" + qf + ") and storage.Store_ID in (" + store + ") group by storage.Code order by storage.Storage_Date asc";

                using (MySqlDataAdapter adpt = new MySqlDataAdapter(query, dbconnection))
                {
                    DataSet dset = new DataSet();

                    adpt.Fill(dset);

                    /*dataGridView1.DataSource = dset.Tables[0];*/
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
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

        private void btnReport_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            /*for (int i = 0; i < dataGridView2.Columns.Count; i++)
            {
                dt.Columns.Add(dataGridView2.Columns[i].Name.ToString());
            }
            for (int i = 0; i < dataGridView2.Rows.Count - 1; i++)
            {
                DataRow dr = dt.NewRow();
                for (int j = 0; j < dataGridView2.Columns.Count; j++)
                {
                    dr[dataGridView2.Columns[j].Name.ToString()] = dataGridView2.Rows[i].Cells[j].Value;
                }

                dt.Rows.Add(dr);
            }*/

            /*Form1_CrystalReport f = new Form1_CrystalReport(dt,cmbFromStore.Text,cmbToStore.Text);
            f.Show();
            this.Hide();*/
        }

    }
}

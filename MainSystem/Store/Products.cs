using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Card;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Layout;
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
    public partial class Products : Form
    {
        MySqlConnection dbconnection, dbconnection1;
        MainForm storeMainForm = null;
        bool loaded = false;
        bool load = false;
        //DataGridViewRow row1;
        public  Product_Record product_Record = null;
        public Product_Update product_Update = null;
        TipImage tipImage=null;
        char[] arrCode=null;

        public Products(MainForm storeMainForm)
        {
            try
            {
                InitializeComponent();
                dbconnection = new MySqlConnection(connection.connectionString);
                dbconnection1 = new MySqlConnection(connection.connectionString);
                this.storeMainForm = storeMainForm;
               
               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
           
        }

        //events
        private void Products_Load(object sender, EventArgs e)
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

                query = "select * from factory  ";
                da = new MySqlDataAdapter(query, dbconnection);
                dt = new DataTable();
                da.Fill(dt);
                comFactory.DataSource = dt;
                comFactory.DisplayMember = dt.Columns["Factory_Name"].ToString();
                comFactory.ValueMember = dt.Columns["Factory_ID"].ToString();
                comFactory.Text = "";
                txtFactory.Text = "";

                query = "select * from groupo";
                da = new MySqlDataAdapter(query, dbconnection);
                dt = new DataTable();
                da.Fill(dt);
                comGroup.DataSource = dt;
                comGroup.DisplayMember = dt.Columns["Group_Name"].ToString();
                comGroup.ValueMember = dt.Columns["Group_ID"].ToString();
                comGroup.Text = "";
                txtGroup.Text = "";

                query = "select * from product";
                da = new MySqlDataAdapter(query, dbconnection);
                dt = new DataTable();
                da.Fill(dt);
                comProduct.DataSource = dt;
                comProduct.DisplayMember = dt.Columns["Product_Name"].ToString();
                comProduct.ValueMember = dt.Columns["Product_ID"].ToString();
                comProduct.Text = "";
                txtProduct.Text = "";
                
                loaded = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }  
        private void comType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                dbconnection.Close();
                dbconnection.Open();
                if (loaded)
                {
                    loaded = false;
                    txtType.Text = comType.SelectedValue.ToString();
                    comFactory.Focus();
                   
                    filterFactory();
                    dbconnection.Close();
                    dbconnection.Open();
                    filterGroup();
                  
                    if (txtType.Text != "1")
                    {
                        string query = "select * from product";
                        MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        comProduct.DataSource = dt;
                        comProduct.DisplayMember = dt.Columns["Product_Name"].ToString();
                        comProduct.ValueMember = dt.Columns["Product_ID"].ToString();
                        comProduct.Text = "";
                        txtProduct.Text = "";
                        label1.Text = "الصنف";
                        filterProduct();
                    }
                    else
                    {
                        string query = "select * from size";
                        MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        comProduct.DataSource = dt;
                        comProduct.DisplayMember = dt.Columns["Size_Value"].ToString();
                        comProduct.ValueMember = dt.Columns["Size_ID"].ToString();
                        comProduct.Text = "";
                        txtProduct.Text = "";
                        label1.Text = "المقاس";
                        filterProduct();
                    }
                    loaded = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }
        private void comGroup_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (loaded)
                {
                    loaded = false;
                    dbconnection.Close();
                    dbconnection.Open();
                    txtGroup.Text = comGroup.SelectedValue.ToString();
                    comProduct.Focus();
                    filterProduct();
                    loaded = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }
        private void comFactory_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (loaded)
                {
                    loaded = false;
                    dbconnection.Close();
                    dbconnection.Open();
                    txtFactory.Text = comFactory.SelectedValue.ToString();
                    comGroup.Focus();
                    filterGroup();
                    filterProduct();
                    loaded = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }
        private void comProduct_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (loaded)
                {
                    loaded = false;
                    dbconnection.Close();
                    dbconnection.Open();
                    txtProduct.Text = comProduct.SelectedValue.ToString();
                    comType.Focus();
                    loaded = true;
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
                    dbconnection.Close();
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
                               

                                if (txtType.Text != "1")
                                {
                                    query = "select * from product";
                                    MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection1);
                                    DataTable dt = new DataTable();
                                    da.Fill(dt);
                                    comProduct.DataSource = dt;
                                    comProduct.DisplayMember = dt.Columns["Product_Name"].ToString();
                                    comProduct.ValueMember = dt.Columns["Product_ID"].ToString();
                                    comProduct.Text = "";
                                    txtProduct.Text = "";
                                    label1.Text = "الصنف";
                                    filterProduct();
                                }
                                else
                                {
                                    query = "select * from size";
                                    MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection1);
                                    DataTable dt = new DataTable();
                                    da.Fill(dt);
                                    comProduct.DataSource = dt;
                                    comProduct.DisplayMember = dt.Columns["Size_Value"].ToString();
                                    comProduct.ValueMember = dt.Columns["Size_ID"].ToString();
                                    comProduct.Text = "";
                                    txtProduct.Text = "";
                                    label1.Text = "المقاس";
                                    filterProduct();
                                }
                                dbconnection.Close();
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
                                dbconnection.Close();
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
                                dbconnection.Close();
                            }
                            else
                            {
                                MessageBox.Show("there is no item with this id");
                                dbconnection.Close();
                                return;
                            }
                            break;
                        case "txtProduct":
                            if (label1.Text == "الصنف")
                            {
                                query = "select Product_Name from product where Product_ID='" + txtFactory.Text + "'";
                                com = new MySqlCommand(query, dbconnection);
                                if (com.ExecuteScalar() != null)
                                {
                                    Name = (string)com.ExecuteScalar();
                                    comProduct.Text = Name;
                                    txtType.Focus();
                                    dbconnection.Close();
                                }
                                else
                                {
                                    MessageBox.Show("there is no item with this id");
                                    dbconnection.Close();
                                    return;
                                }
                            }
                            else
                            {
                                query = "select Size_Value from size where Size_ID='" + txtFactory.Text + "'";
                                com = new MySqlCommand(query, dbconnection);
                                if (com.ExecuteScalar() != null)
                                {
                                    Name = (string)com.ExecuteScalar();
                                    comProduct.Text = Name;
                                    txtType.Focus();
                                    dbconnection.Close();
                                }
                                else
                                {
                                    MessageBox.Show("there is no item with this id");
                                    dbconnection.Close();
                                    return;
                                }
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
                if (chBoxSelectAll.Checked)
                    displayProducts();
                else
                    displayAllProducts();
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
                foreach (Control item in panelControl1.Controls)
                {
                    if (item is TextBox)
                        item.Text = "";
                    else if (item is ComboBox)
                    {
                        item.Text = "";
                    }

                }
                dataGridView1.DataSource = null;
            }
            catch
            {
                // MessageBox.Show(ex.Message);
            }
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                DataRowView row1 = (DataRowView)(((GridView)dataGridView1.MainView).GetRow(((GridView)dataGridView1.MainView).GetSelectedRows()[0]));
                if (row1 != null)
                {
                    DialogResult dialogResult = MessageBox.Show("Are you sure you want to delete the item?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dialogResult == DialogResult.Yes)
                    {
                        string query = "delete from data where Data_ID=" + row1[0].ToString();
                        MySqlCommand comand = new MySqlCommand(query, dbconnection);
                        dbconnection.Open();
                        comand.ExecuteNonQuery();

                        query = "ALTER TABLE data AUTO_INCREMENT = 1;";
                        MySqlCommand com = new MySqlCommand(query, dbconnection);
                        com.ExecuteNonQuery(); 

                        UserControl.ItemRecord("data", "حذف",Convert.ToInt16(row1[0].ToString()), DateTime.Now,"", dbconnection);

                        //if (chBoxSelectAll.Checked)
                        //    displayProducts();
                        //else
                        //    displayAllProducts();
                    }
                    else if (dialogResult == DialogResult.No)
                    { }
                }
                else
                {
                    MessageBox.Show("you must select an item");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                DataRowView row1 = (DataRowView)(((GridView)dataGridView1.MainView).GetRow(((GridView)dataGridView1.MainView).GetSelectedRows()[0]));
                if (load)
                {
                    if (tipImage == null)
                    {
                        tipImage = new TipImage(row1[1].ToString());
                        tipImage.Location = new Point(Cursor.Position.X, Cursor.Position.Y);
                        tipImage.Show();
                    }
                    else
                    {
                        tipImage.Close();
                        tipImage = new TipImage(row1[1].ToString());
                        tipImage.Location = new Point(Cursor.Position.X, Cursor.Position.Y);
                        tipImage.Show();
                    }
                }
            }
            catch
            {
             //   MessageBox.Show(ex.Message);
            }
        }
        private void btnAddNew_Click(object sender, EventArgs e)
        {
            try
            {
                storeMainForm.bindRecordProductForm(this);
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
                if (((GridView)dataGridView1.MainView).GetSelectedRows().Count()>0)
                {
                    DataRowView storeRow = (DataRowView)(((GridView)dataGridView1.MainView).GetRow(((GridView)dataGridView1.MainView).GetSelectedRows()[0]));
                    storeMainForm.bindUpdateProductForm(storeRow, this);
                }
                else
                {
                    MessageBox.Show("يجب تحديد البند المراد تعديله.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btnReport_Click(object sender, EventArgs e)
        {
            try
            {
                storeMainForm.bindReportProductForm(dataGridView1);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btnDisplayWithImage_Click(object sender, EventArgs e)
        {
            try
            {
                productsWithImage();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btnClear_Click(object sender, EventArgs e)
        {
            try
            {
                clear();
                //if (chBoxSelectAll.Checked)
                //    displayProducts();
                //else
                //    displayAllProducts();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void dataGridView1_EditorKeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    txtEditCode.Visible = true;
                    label7.Visible = true;
                    DataRowView row = (DataRowView)(((GridView)dataGridView1.MainView).GetRow(((GridView)dataGridView1.MainView).GetSelectedRows()[0]));
                    if (row != null)
                    {
                        arrCode = row[4].ToString().ToCharArray();
                        string str = arrCode[16].ToString() + arrCode[17].ToString() + arrCode[18].ToString() + arrCode[19].ToString() + "";
                        txtEditCode.Text = str;
                        txtEditCode.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void txtEditCode_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    dbconnection.Open();
                    string partCode = txtEditCode.Text;
                    int count = partCode.Length;
                    while (count < 4)
                    {
                        partCode = "0" + partCode;
                        count++;
                    }

                    char[] arrPart = partCode.ToCharArray();
                    if (arrCode != null)
                    {
                        arrCode[16] = arrPart[0];
                        arrCode[17] = arrPart[1];
                        arrCode[18] = arrPart[2];
                        arrCode[19] = arrPart[3];
                    }

                    string code = "";
                    for (int i = 0; i < arrCode.Length; i++)
                    {
                        code += arrCode[i];
                    }
                    string query = "select Data_ID from data where Code='"+code+"'";
                    MySqlCommand com = new MySqlCommand(query, dbconnection);
                    if (com.ExecuteScalar()== null)
                    {
                        DataRowView row = (DataRowView)(((GridView)dataGridView1.MainView).GetRow(((GridView)dataGridView1.MainView).GetSelectedRows()[0]));
                        if (row != null)
                        {
                            query = "update data set Code='" + code + "' where Data_ID=" + row[0].ToString();
                            com = new MySqlCommand(query, dbconnection);
                            com.ExecuteNonQuery();
                            //if (chBoxSelectAll.Checked)
                            //    displayProducts();
                            //else
                            //    displayAllProducts();
                            txtEditCode.Text = "";
                            txtEditCode.Visible = false;
                            label7.Visible = false;
                        }
                    }
                    else
                    {
                        MessageBox.Show("ادخل رقم كود صحيح");
                    }

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }
       
        //function
        public void displayProducts()
        {
            try
            {
                load = false;
                string q1, q2, q3, q4;
                if (txtType.Text == "")
                {
                    q1 = "select Type_ID from type";
                }
                else
                {
                    q1 = txtType.Text;
                }
                if (txtFactory.Text == "")
                {
                    q2 = "select Factory_ID from factory";
                }
                else
                {
                    q2 = txtFactory.Text;
                }
                if (txtProduct.Text == "")
                {
                    q3 = "select Product_ID from product";
                }
                else
                {
                    q3 = txtProduct.Text;
                }
                if (txtGroup.Text == "")
                {
                    q4 = "select Group_ID from groupo";
                }
                else
                {
                    q4 = txtGroup.Text;
                }
                string Month = DateTime.Now.Month.ToString();
                if (Month.Length < 2)
                    Month = "0" + Month;
                string Day= DateTime.Now.Day.ToString();
                if (Day.Length < 2)
                    Day = "0" + Day;

                string date = DateTime.Now.Year + "-" + Month + "-" + Day;
                if (txtType.Text != "1")
                {
                    string query = "SELECT data.Data_ID,data.Color_ID  ,data.Size_ID ,data.Sort_ID ,data.Code as 'الكود',type.Type_Name as 'النوع',groupo.Group_Name as 'المجموعة',factory.Factory_Name as 'المصنع',product.Product_Name as 'الصنف',color.Color_Name as 'اللون',data.Description as 'الوصف',size.Size_Value as 'المقاس',sort.Sort_Value as 'الفرز',data.Carton as 'الكرتنة',data.Classification as 'التصنيف' from data INNER JOIN type ON type.Type_ID = data.Type_ID INNER JOIN product ON product.Product_ID = data.Product_ID INNER JOIN factory ON data.Factory_ID = factory.Factory_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID LEFT outer JOIN color ON data.Color_ID = color.Color_ID LEFT outer  JOIN size ON data.Size_ID = size.Size_ID LEFT outer  JOIN sort ON data.Sort_ID = sort.Sort_ID where  data.Type_ID IN(" + q1 + ") and  data.Factory_ID  IN(" + q2 + ") and  data.Product_ID  IN(" + q3 + ") and data.Group_ID IN (" + q4 + ") and Data_Date='"+ date + "' order by SUBSTR(data.Code,1,16),color.Color_Name,data.Description,data.Sort_ID";
                    MySqlDataAdapter adapter = new MySqlDataAdapter(query, dbconnection);
                    DataSet dataSet = new DataSet();
                    adapter.Fill(dataSet);
                    dataGridView1.DataSource = null;
                    GridView lView = new GridView(dataGridView1);

                    dataGridView1.MainView = lView;
                    lView.Appearance.Row.Font = gridView2.Appearance.Row.Font;
                    lView.Appearance.Row.TextOptions.HAlignment = gridView2.Appearance.Row.TextOptions.HAlignment;
                    lView.Appearance.HeaderPanel.Font = gridView2.Appearance.HeaderPanel.Font;
                    lView.Appearance.HeaderPanel.TextOptions.HAlignment = gridView2.Appearance.HeaderPanel.TextOptions.HAlignment;
                    dataGridView1.DataSource = dataSet.Tables[0];
                    lView.Columns[0].Visible = false;
                    lView.Columns[1].Visible = false;
                    lView.Columns[2].Visible = false;
                    lView.Columns[3].Visible = false;
                    lView.Columns[4].Width = 200;
                }
                else
                {
                    string query = "SELECT data.Data_ID,data.Color_ID ,data.Size_ID ,data.Sort_ID , data.Code as 'الكود',type.Type_Name as 'النوع',groupo.Group_Name as 'المجموعة',factory.Factory_Name as 'المصنع',product.Product_Name as 'الصنف',color.Color_Name as 'اللون',data.Description as 'الوصف',size.Size_Value as 'المقاس',sort.Sort_Value as 'الفرز',data.Carton as 'الكرتنة',data.Classification as 'التصنيف' from data INNER JOIN type ON type.Type_ID = data.Type_ID INNER JOIN product ON product.Product_ID = data.Product_ID INNER JOIN factory ON data.Factory_ID = factory.Factory_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID LEFT outer JOIN color ON data.Color_ID = color.Color_ID LEFT outer  JOIN size ON data.Size_ID = size.Size_ID LEFT outer  JOIN sort ON data.Sort_ID = sort.Sort_ID where  data.Type_ID IN(" + q1 + ") and  data.Factory_ID  IN(" + q2 + ") and  data.Size_ID  IN(" + q3 + ") and data.Group_ID IN (" + q4 + ") and Data_Date='" + date + "' order by SUBSTR(data.Code,1,16),color.Color_Name,data.Description,data.Sort_ID";
                    MySqlDataAdapter adapter = new MySqlDataAdapter(query, dbconnection);
                    DataSet dataSet = new DataSet();
                    adapter.Fill(dataSet);
                    dataGridView1.DataSource = null;
                    GridView lView = new GridView(dataGridView1);

                    dataGridView1.MainView = lView;
                    lView.Appearance.Row.Font = gridView2.Appearance.Row.Font;
                    lView.Appearance.Row.TextOptions.HAlignment = gridView2.Appearance.Row.TextOptions.HAlignment;
                    lView.Appearance.HeaderPanel.Font = gridView2.Appearance.HeaderPanel.Font;
                    lView.Appearance.HeaderPanel.TextOptions.HAlignment = gridView2.Appearance.HeaderPanel.TextOptions.HAlignment;
                    dataGridView1.DataSource = dataSet.Tables[0];
                    lView.Columns[0].Visible = false;
                    lView.Columns[1].Visible = false;
                    lView.Columns[2].Visible = false;
                    lView.Columns[3].Visible = false;
                    lView.Columns[4].Width = 200;
                }
                load = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        public void displayAllProducts()
        {
            try
            {
                load = false;
                string q1, q2, q3, q4,q5;
                if (txtType.Text == "")
                {
                    q1 = "select Type_ID from type";
                }
                else
                {
                    q1 = txtType.Text;
                }
                if (txtFactory.Text == "")
                {
                    q2 = "select Factory_ID from factory";
                }
                else
                {
                    q2 = txtFactory.Text;
                }
                if (txtProduct.Text == "")
                {
                    q3 = "select Product_ID from product";
                }
                else
                {
                    q3 = txtProduct.Text;
                }
                if (txtGroup.Text == "")
                {
                    q4 = "select Group_ID from groupo";
                }
                else
                {
                    q4 = txtGroup.Text;
                }
                if (txtProduct.Text == "")
                {
                    q5 = "select Size_ID from size";
                }
                else
                {
                    q5 = txtProduct.Text;
                }
                if (txtType.Text != "1")
                {
                    string query = "SELECT data.Data_ID,data.Color_ID ,data.Size_ID ,data.Sort_ID, data.Code as 'الكود',type.Type_Name as 'النوع',factory.Factory_Name as 'المصنع',groupo.Group_Name as 'المجموعة',product.Product_Name as 'الصنف',sort.Sort_Value as 'الفرز',color.Color_Name as 'اللون',size.Size_Value as 'المقاس',data.Classification as 'التصنيف',data.Description as 'الوصف',data.Carton as 'الكرتنة' from data INNER JOIN type ON type.Type_ID = data.Type_ID INNER JOIN product ON product.Product_ID = data.Product_ID INNER JOIN factory ON data.Factory_ID = factory.Factory_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID LEFT outer JOIN color ON data.Color_ID = color.Color_ID LEFT outer  JOIN size ON data.Size_ID = size.Size_ID LEFT outer  JOIN sort ON data.Sort_ID = sort.Sort_ID where  data.Type_ID IN(" + q1 + ") and  data.Factory_ID  IN(" + q2 + ") and  data.Product_ID  IN(" + q3 + ") and data.Group_ID IN (" + q4 + ")  order by SUBSTR(data.Code,1,16),color.Color_Name,data.Description,data.Sort_ID";
                    MySqlDataAdapter adapter = new MySqlDataAdapter(query, dbconnection);
                    DataSet dataSet = new DataSet();
                    adapter.Fill(dataSet);
                    dataGridView1.DataSource = null;
                    GridView lView = new GridView(dataGridView1);

                    dataGridView1.MainView = lView;
                    lView.Appearance.Row.Font = gridView2.Appearance.Row.Font;
                    lView.Appearance.Row.TextOptions.HAlignment = gridView2.Appearance.Row.TextOptions.HAlignment;
                    lView.Appearance.HeaderPanel.Font = gridView2.Appearance.HeaderPanel.Font;
                    lView.Appearance.HeaderPanel.TextOptions.HAlignment = gridView2.Appearance.HeaderPanel.TextOptions.HAlignment;
                    dataGridView1.DataSource = dataSet.Tables[0];
                    lView.Columns[0].Visible = false;
                    lView.Columns[1].Visible = false;
                    lView.Columns[2].Visible = false;
                    lView.Columns[3].Visible = false;
                    lView.Columns[4].Width = 200;
                }
                else
                {
                    string query = "SELECT data.Data_ID,data.Color_ID ,data.Size_ID ,data.Sort_ID, data.Code as 'الكود',type.Type_Name as 'النوع',factory.Factory_Name as 'المصنع',groupo.Group_Name as 'المجموعة',product.Product_Name as 'الصنف',sort.Sort_Value as 'الفرز',color.Color_Name as 'اللون',size.Size_Value as 'المقاس',data.Classification as 'التصنيف',data.Description as 'الوصف',data.Carton as 'الكرتنة' from data INNER JOIN type ON type.Type_ID = data.Type_ID INNER JOIN product ON product.Product_ID = data.Product_ID INNER JOIN factory ON data.Factory_ID = factory.Factory_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID LEFT outer JOIN color ON data.Color_ID = color.Color_ID LEFT outer  JOIN size ON data.Size_ID = size.Size_ID LEFT outer  JOIN sort ON data.Sort_ID = sort.Sort_ID where  data.Type_ID IN(" + q1 + ") and  data.Factory_ID  IN(" + q2 + ") and  data.Size_ID  IN(" + q5 + ") and data.Group_ID IN (" + q4 + ") order by  SUBSTR(data.Code,1,16) ,color.Color_Name ,data.Sort_ID";
                    MySqlDataAdapter adapter = new MySqlDataAdapter(query, dbconnection);
                    DataSet dataSet = new DataSet();
                    adapter.Fill(dataSet);
                   // dataGridView1.DataSource = null;
                    GridView lView = new GridView(dataGridView1);

                    dataGridView1.MainView = lView;
                    lView.Appearance.Row.Font = gridView2.Appearance.Row.Font;
                    lView.Appearance.Row.TextOptions.HAlignment = gridView2.Appearance.Row.TextOptions.HAlignment;
                    lView.Appearance.HeaderPanel.Font = gridView2.Appearance.HeaderPanel.Font;
                    lView.Appearance.HeaderPanel.TextOptions.HAlignment = gridView2.Appearance.HeaderPanel.TextOptions.HAlignment;
                    dataGridView1.DataSource = dataSet.Tables[0];
                    lView.Columns[0].Visible = false;
                    lView.Columns[1].Visible = false;
                    lView.Columns[2].Visible = false;
                    lView.Columns[3].Visible = false;
                    lView.Columns[4].Width = 200;
                }
                load = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        private void productsWithImage()
        {
            try
            {
                dbconnection.Open();
                loaded = false;
                string q1, q2, q3, q4;
                if (txtType.Text == "")
                {
                    q1 = "select Type_ID from sets";
                }
                else
                {
                    q1 = txtType.Text;
                }
                if (txtFactory.Text == "")
                {
                    q2 = "select Factory_ID from sets";
                }
                else
                {
                    q2 = txtFactory.Text;
                }
                if (txtFactory.Text == "")
                {
                    q3 = "select Product_ID from product";
                }
                else
                {
                    q3 = txtFactory.Text;
                }
                if (txtGroup.Text == "")
                {
                    q4 = "select Group_ID from sets";
                }
                else
                {
                    q4 = txtGroup.Text;
                }

                dataGridView1.DataSource = null;
                string query = "SELECT distinct data.Code as 'الكود',data_photo.Photo as 'الصورة',product.Product_Name as 'الصنف',type.Type_Name as 'النوع',factory.Factory_Name as 'المصنع',groupo.Group_Name as 'المجموعة',color.Color_Name as 'اللون',size.Size_Value as 'المقاس',sort.Sort_Value as 'الفرز',data.Classification as 'التصنيف',data.Description as 'الوصف',data.Carton as 'الكرتنة' from data INNER JOIN type ON type.Type_ID = data.Type_ID INNER JOIN product ON product.Product_ID = data.Product_ID INNER JOIN factory ON data.Factory_ID = factory.Factory_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID LEFT outer JOIN color ON data.Color_ID = color.Color_ID LEFT outer  JOIN size ON data.Size_ID = size.Size_ID LEFT outer  JOIN sort ON data.Sort_ID = sort.Sort_ID INNER JOIN data_photo on data.Data_ID=data_photo.Data_ID  ";
                MySqlDataAdapter AdapterProducts = new MySqlDataAdapter(query, dbconnection);

                MySqlDataAdapter adapter = new MySqlDataAdapter(query, dbconnection);
                DataSet dataSet = new DataSet();
                adapter.Fill(dataSet);
                LayoutView lView = new LayoutView(dataGridView1);
                dataGridView1.MainView = lView;
                dataGridView1.DataSource = dataSet.Tables[0];
                
                loaded = true;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();

        }
        public void clear()
        {
            foreach (Control item in panelControl1.Controls)
            {
                if (item is TextBox)
                    item.Text = "";
                else if (item is ComboBox)
                {
                    item.Text = "";
                }

            }
        }
        public void filterFactory()
        {
            if (comType.Text != "")
            {
                string query = "select * from factory inner join type_factory on factory.Factory_ID=type_factory.Factory_ID inner join type on type.Type_ID=type_factory.Type_ID  where type_factory.Type_ID=" + comType.SelectedValue;
                MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                DataTable dt = new DataTable();
                da.Fill(dt);
                comFactory.DataSource = dt;
                comFactory.DisplayMember = dt.Columns["Factory_Name"].ToString();
                comFactory.ValueMember = dt.Columns["Factory_ID"].ToString();
                comFactory.Text = "";
                txtFactory.Text = "";
            }
        }
        public void filterGroup()
        {
            string query = "select TypeCoding_Method from type where Type_ID=" + txtType.Text;
            MySqlCommand com = new MySqlCommand(query, dbconnection);
            int TypeCoding_Method = (int)com.ExecuteScalar();
            if (TypeCoding_Method == 1)
            {
                string query2 = "";
                if (txtType.Text=="2"|| txtType.Text == "1")
                    query2 = "select * from groupo where Factory_ID="+-1 ;
                else
                    query2 = "select * from groupo where Factory_ID=" + -Convert.ToInt16(txtType.Text) + " and Type_ID=" + txtType.Text;

                MySqlDataAdapter da2 = new MySqlDataAdapter(query2, dbconnection);
                DataTable dt2 = new DataTable();
                da2.Fill(dt2);
                comGroup.DataSource = dt2;
                comGroup.DisplayMember = dt2.Columns["Group_Name"].ToString();
                comGroup.ValueMember = dt2.Columns["Group_ID"].ToString();
                comGroup.Text = "";
                txtGroup.Text = "";
            }
            else
            {
                string q = "";
                if (txtFactory.Text != "")
                {
                    q = " and Factory_ID = "+txtFactory.Text;
                }
                query = "select * from groupo where Type_ID=" + txtType.Text+q;
                MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                DataTable dt = new DataTable();
                da.Fill(dt);
                comGroup.DataSource = dt;
                comGroup.DisplayMember = dt.Columns["Group_Name"].ToString();
                comGroup.ValueMember = dt.Columns["Group_ID"].ToString();
                comGroup.Text = "";
                txtGroup.Text = "";
            }
        }
        public void filterProduct()
        {
            if (comType.Text != "")
            {
                if (comGroup.Text != "" || comFactory.Text != ""|| comType.Text != "")
                {
                    if (txtType.Text != "1")
                    {
                        string supQuery = "";

                        supQuery = " product.Type_ID=" + txtType.Text + "";
                        if (comFactory.Text != "")
                        {
                            supQuery += " and product_factory_group.Factory_ID=" + txtFactory.Text + "";
                        }
                        else if (comGroup.Text != "")
                        {
                            supQuery += " and product_factory_group.Group_ID=" + txtGroup.Text + "";
                        }
                        string query = "select distinct  product.Product_ID  ,Product_Name  from product inner join product_factory_group on product.Product_ID=product_factory_group.Product_ID  where  "+supQuery+"   order by product.Product_ID";
                        MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        comProduct.DataSource = dt;
                        comProduct.DisplayMember = dt.Columns["Product_Name"].ToString();
                        comProduct.ValueMember = dt.Columns["Product_ID"].ToString();
                        comProduct.Text = "";
                        txtProduct.Text = "";
                    }
                    else
                    {
                        string supQuery = "";
                        if (comFactory.Text != "")
                        {
                            supQuery += " and type_factory.Factory_ID=" + txtFactory.Text + "";
                        }
                        if (comGroup.Text != "")
                        {
                            supQuery += " and Group_ID=" + txtGroup.Text + "";
                        }
                        string query = "select * from size inner join type_factory on size.Factory_ID=type_factory.Factory_ID where type_factory.Type_ID=1 " + supQuery;
                        MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        comProduct.DataSource = dt;
                        comProduct.DisplayMember = dt.Columns["Size_Value"].ToString();
                        comProduct.ValueMember = dt.Columns["Size_ID"].ToString();
                        comProduct.Text = "";
                        txtProduct.Text = "";
                    }
                }
            }
        
        }
        public void filterSize()
        {
            if (comFactory.Text != "")
            {
                string query = "select * from size where Factory_ID=" + comFactory.SelectedValue;
                MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                DataTable dt = new DataTable();
                da.Fill(dt);
                comProduct.DataSource = dt;
                comProduct.DisplayMember = dt.Columns["Size_Value"].ToString();
                comProduct.ValueMember = dt.Columns["Size_ID"].ToString();
                comProduct.Text = "";
                txtProduct.Text = "";
            }
        }
    }
}

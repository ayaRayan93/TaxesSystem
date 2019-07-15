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
    public partial class SetPurchasesPrice2 : Form
    {
        MySqlConnection dbconnection;
        ProductsPurchasesPriceForm productsPurchasesPriceForm = null;
        XtraTabControl xtraTabControlPurchasesContent = null;
        bool load = false;
        bool factoryFlage = false;
        bool groupFlage = false;
        bool flagProduct = false;
        int id = 0;

        public SetPurchasesPrice2(ProductsPurchasesPriceForm productsPurchasesPriceForm, XtraTabControl xtraTabControlPurchasesContent)
        {
            try
            {
                InitializeComponent();
                this.xtraTabControlPurchasesContent = xtraTabControlPurchasesContent;
                this.productsPurchasesPriceForm = productsPurchasesPriceForm;
                dbconnection = new MySqlConnection(connection.connectionString);           
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        
        //deign event
        private void labSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (tLPanCpntent.RowStyles[0].Height == 120)
                {
                    tLPanCpntent.RowStyles[0].Height = 200;
                }
                else
                {
                    tLPanCpntent.RowStyles[0].Height = 120;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }    
        private void chBoxAdditionalIncrease_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chBoxAdditionalIncrease.Checked)
                {
                    tLPanCpntent.RowStyles[2].Height = 360;                  
                }
                else
                {
                    tLPanCpntent.RowStyles[2].Height = 200;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void radioList_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                label14.Text = "خصم الشراء";
                txtNormal.Visible = true;
                txtUnNormal.Visible = true;
                label15.Visible = true;
                label16.Visible = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void radioQata3y_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                label14.Text = "نسبة الاضافة";
                txtNormal.Visible = false;
                txtUnNormal.Visible = false;
                label15.Visible = false;
                label16.Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //main events
        private void SetPurchasesPrice_Load(object sender, EventArgs e)
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

                query = "select * from factory";
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

                query = "select distinct Classification from data";
                da = new MySqlDataAdapter(query, dbconnection);
                dt = new DataTable();
                da.Fill(dt);
                comClassfication.DataSource = dt;
                comClassfication.DisplayMember = dt.Columns["Classification"].ToString();
                comClassfication.Text = "";



                load = true;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            dbconnection.Close();
        }
        private void comBox_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (load)
                {
                    ComboBox comBox = (ComboBox)sender;

                    switch (comBox.Name)
                    {
                        case "comType":
                            if (load)
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
                                dbconnection.Close();
                                dbconnection.Open();
                                query = "select TypeCoding_Method from type where Type_ID=" + txtType.Text;
                                MySqlCommand com = new MySqlCommand(query, dbconnection);
                                int TypeCoding_Method = (int)com.ExecuteScalar();
                                dbconnection.Close();
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
                                    groupFlage = true;
                                }
                                factoryFlage = true;

                                query = "select * from color where Type_ID=" + txtType.Text;
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
                                txtFactory.Text = comFactory.SelectedValue.ToString();
                                dbconnection.Close();
                                dbconnection.Open();
                                string query = "select TypeCoding_Method from type where Type_ID=" + txtType.Text;
                                MySqlCommand com = new MySqlCommand(query, dbconnection);
                                int TypeCoding_Method = (int)com.ExecuteScalar();
                                dbconnection.Close();
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

                                string query2 = "select * from size where Factory_ID=" + txtFactory.Text;
                                MySqlDataAdapter da2 = new MySqlDataAdapter(query2, dbconnection);
                                DataTable dt2 = new DataTable();
                                da2.Fill(dt2);
                                comSize.DataSource = dt2;
                                comSize.DisplayMember = dt2.Columns["Size_Value"].ToString();
                                comSize.ValueMember = dt2.Columns["Size_ID"].ToString();
                                comSize.Text = "";
                                txtSize.Text = "";
                                comGroup.Focus();

                                query = "select distinct Classification from data where Factory_ID=" + txtFactory.Text;
                                da2 = new MySqlDataAdapter(query, dbconnection);
                                dt2 = new DataTable();
                                da2.Fill(dt2);
                                comClassfication.DataSource = dt2;
                                comClassfication.DisplayMember = dt2.Columns["Classification"].ToString();
                                comClassfication.Text = "";

                            }
                            break;
                        case "comGroup":
                            if (groupFlage)
                            {
                                txtGroup.Text = comGroup.SelectedValue.ToString();
                                string supQuery = "", subQuery1 = "";
                                if (txtType.Text != "")
                                {
                                    supQuery += " and product.Type_ID=" + txtType.Text;
                                }
                                if (txtFactory.Text != "")
                                {
                                    supQuery += " and product_factory_group.Factory_ID=" + txtFactory.Text;
                                    subQuery1 += " and Factory_ID=" + txtFactory.Text;
                                }
                                string query3 = "select distinct  product.Product_ID  ,Product_Name  from product inner join product_factory_group on product.Product_ID=product_factory_group.Product_ID  where product_factory_group.Group_ID=" + txtGroup.Text + supQuery + "  order by product.Product_ID";
                                MySqlDataAdapter da3 = new MySqlDataAdapter(query3, dbconnection);
                                DataTable dt3 = new DataTable();
                                da3.Fill(dt3);
                                comProduct.DataSource = dt3;
                                comProduct.DisplayMember = dt3.Columns["Product_Name"].ToString();
                                comProduct.ValueMember = dt3.Columns["Product_ID"].ToString();
                                comProduct.Text = "";
                                txtProduct.Text = "";

                                string query2 = "select * from size where Group_ID=" + txtGroup.Text + subQuery1;
                                MySqlDataAdapter da2 = new MySqlDataAdapter(query2, dbconnection);
                                DataTable dt2 = new DataTable();
                                da2.Fill(dt2);
                                comSize.DataSource = dt2;
                                comSize.DisplayMember = dt2.Columns["Size_Value"].ToString();
                                comSize.ValueMember = dt2.Columns["Size_ID"].ToString();
                                comSize.Text = "";
                                txtSize.Text = "";

                                comProduct.Focus();
                                flagProduct = true;
                            }
                            break;

                        case "comProduct":

                            txtProduct.Text = comProduct.SelectedValue.ToString();
                            comColor.Focus();

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
                            case "txtType":
                                query = "select Type_Name from type where Type_ID='" + txtType.Text + "'";
                                com = new MySqlCommand(query, dbconnection);
                                if (com.ExecuteScalar() != null)
                                {
                                    Name = (string)com.ExecuteScalar();
                                    comType.Text = Name;
                                    txtFactory.Focus();
                                    dbconnection.Close();

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
                                query = "select Product_Name from product where Product_ID='" + txtProduct.Text + "'";
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
                                    displayProducts();
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

                            case "txtCodePart1":
                                query = "select Type_Name from type where Type_ID='" + txtCodePart1.Text + "'";
                                com = new MySqlCommand(query, dbconnection);
                                if (com.ExecuteScalar() != null)
                                {
                                    Name = (string)com.ExecuteScalar();
                                    comType.Text = Name;

                                    makeCode(txtCodePart1);
                                    txtCodePart2.Focus();
                                    dbconnection.Close();
                                }
                                else
                                {
                                    MessageBox.Show("هذا الكود غير مسجل");
                                    dbconnection.Close();
                                    return;
                                }
                                break;
                            case "txtCodePart2":
                                query = "select Factory_Name from factory where Factory_ID='" + txtCodePart2.Text + "'";
                                com = new MySqlCommand(query, dbconnection);
                                if (com.ExecuteScalar() != null)
                                {
                                    Name = (string)com.ExecuteScalar();
                                    comFactory.Text = Name;
                                    makeCode(txtCodePart2);
                                    txtCodePart3.Focus();
                                    dbconnection.Close();
                                }
                                else
                                {
                                    MessageBox.Show("هذا الكود غير مسجل");
                                    dbconnection.Close();
                                    return;
                                }
                                break;
                            case "txtCodePart3":
                                query = "select Group_Name from Groupo where Group_ID='" + txtCodePart3.Text + "'";
                                com = new MySqlCommand(query, dbconnection);
                                if (com.ExecuteScalar() != null)
                                {
                                    Name = (string)com.ExecuteScalar();
                                    comGroup.Text = Name;
                                    makeCode(txtCodePart3);
                                    txtCodePart4.Focus();
                                    dbconnection.Close();
                                }
                                else
                                {
                                    MessageBox.Show("هذا الكود غير مسجل");
                                    dbconnection.Close();
                                    return;
                                }
                                break;
                            case "txtCodePart4":
                                query = "select Product_Name from Product where Product_ID='" + txtCodePart4.Text + "'";
                                com = new MySqlCommand(query, dbconnection);
                                if (com.ExecuteScalar() != null)
                                {
                                    Name = (string)com.ExecuteScalar();
                                    comProduct.Text = Name;
                                    makeCode(txtCodePart4);
                                    txtCodePart5.Focus();
                                    dbconnection.Close();
                                }
                                else
                                {
                                    MessageBox.Show("هذا الكود غير مسجل");
                                    dbconnection.Close();
                                    return;
                                }
                                break;
                            case "txtCodePart5":
                                makeCode(txtCodePart5);
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
        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                displayProducts();
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
                comClassfication.Text = "";

                txtType.Text = "";
                txtFactory.Text = "";
                txtGroup.Text = "";
                txtProduct.Text = "";
                txtColor.Text = "";
                txtSize.Text = "";
                txtSort.Text = "";

                gridControl1.DataSource = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void gridControl1_EditorKeyDown(object sender, KeyEventArgs e)
        {
            /*try
            {
                DataRowView row = (DataRowView)(((GridView)gridControl1.MainView).GetRow(((GridView)gridControl1.MainView).GetSelectedRows()[0]));
                if (row != null)
                {
                    txtCode.Text = row[1].ToString();
                    id = Convert.ToInt32(row[0].ToString());
                    string code = txtCode.Text;
                    displayCode(code);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }*/
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (gridView1.SelectedRowsCount>0)
                {
                    dbconnection.Open();
                    int PurchasesPrice_ID = 0, OldPurchasingPrice_ID = 0;
                    double price = double.Parse(txtPrice.Text);
                    double PurchasesPercent = double.Parse(txtPurchases.Text);

                    if (radioQata3y.Checked == true)
                    {
                        #region set qata3yPrice for list item
                        if (gridView1.SelectedRowsCount>1)
                        {
                            double NormalPercent = double.Parse(txtNormal.Text);
                            double UnNormalPercent = double.Parse(txtUnNormal.Text);
                            int[] listOfSelectedRows = gridView1.GetSelectedRows();
                            for (int i = 0; i < gridView1.SelectedRowsCount; i++)
                            {
                                DataRowView row = (DataRowView)(((GridView)gridControl1.MainView).GetRow(((GridView)gridControl1.MainView).GetSelectedRows()[i]));

                                if (!checkExist(row[0].ToString()))
                                {
                                    string query = "INSERT INTO purchasing_price (Purchasing_Discount,Price_Type, Purchasing_Price, ProfitRatio, Data_ID, Price,Last_Price, Date) VALUES(?Purchasing_Discount,?Price_Type,?Purchasing_Price,?ProfitRatio,?Data_ID,?Price,?Last_Price,?Date)";
                                    MySqlCommand command = new MySqlCommand(query, dbconnection);
                                    command.Parameters.AddWithValue("?Price_Type", "قطعى");
                                    command.Parameters.AddWithValue("?Purchasing_Price", calPurchasesPrice());
                                    command.Parameters.AddWithValue("?Data_ID", row[0].ToString());
                                    command.Parameters.AddWithValue("?ProfitRatio", double.Parse(txtPurchases.Text));
                                    command.Parameters.AddWithValue("?Price", price);
                                    command.Parameters.AddWithValue("?Last_Price", calPurchasesPrice());
                                    command.Parameters.AddWithValue("?Purchasing_Discount", 0.0);
                                    command.Parameters.Add("?Date", MySqlDbType.Date);
                                    command.Parameters["?Date"].Value = DateTime.Now.Date;
                                    command.ExecuteNonQuery();

                                    query = "INSERT INTO oldpurchasing_price (Purchasing_Discount,Price_Type, Purchasing_Price, ProfitRatio, Data_ID, Price,Last_Price, Date) VALUES(?Purchasing_Discount,?Price_Type,?Purchasing_Price,?ProfitRatio,?Data_ID,?Price,?Last_Price,?Date)";
                                    command = new MySqlCommand(query, dbconnection);
                                    command.Parameters.AddWithValue("?Price_Type", "قطعى");
                                    command.Parameters.AddWithValue("?Purchasing_Price", calPurchasesPrice());
                                    command.Parameters.AddWithValue("?Data_ID", row[0].ToString());
                                    command.Parameters.AddWithValue("?ProfitRatio", double.Parse(txtPurchases.Text));
                                    command.Parameters.AddWithValue("?Price", price);
                                    command.Parameters.AddWithValue("?Last_Price", calPurchasesPrice());
                                    command.Parameters.AddWithValue("?Purchasing_Discount", 0.0);
                                    command.Parameters.Add("?Date", MySqlDbType.Date);
                                    command.Parameters["?Date"].Value = DateTime.Now.Date;
                                    command.ExecuteNonQuery();
                                    insertIntoAdditionalIncrease(ref PurchasesPrice_ID, ref OldPurchasingPrice_ID);
                                    UserControl.ItemRecord("purchasing_price", "اضافة", PurchasesPrice_ID, DateTime.Now, "", dbconnection);

                                }
                            }
                        }
                        #endregion
                        #region set qata3yPrice for one item
                        else
                        {
                            if (id != 0)
                            {
                                string query = "INSERT INTO purchasing_price (Purchasing_Discount,Price_Type,Last_Price, Purchasing_Price, ProfitRatio, Data_ID, Price, Date) VALUES(?Purchasing_Discount,?Price_Type,?Last_Price,?Purchasing_Price,?ProfitRatio,?Data_ID,?Price,?Date)";
                                MySqlCommand command = new MySqlCommand(query, dbconnection);
                                command.Parameters.AddWithValue("?Price_Type", "قطعى");
                                command.Parameters.AddWithValue("?Purchasing_Price", calPurchasesPrice());
                                command.Parameters.AddWithValue("?Data_ID", id);
                                command.Parameters.AddWithValue("?ProfitRatio", double.Parse(txtPurchases.Text));
                                command.Parameters.AddWithValue("?Price", price);
                                command.Parameters.AddWithValue("?Last_Price", calPurchasesPrice());
                                command.Parameters.AddWithValue("?Purchasing_Discount", 0.0);
                                command.Parameters.Add("?Date", MySqlDbType.Date);
                                command.Parameters["?Date"].Value = DateTime.Now.Date;
                                command.ExecuteNonQuery();

                                query = "INSERT INTO oldpurchasing_price (Purchasing_Discount,Price_Type,Last_Price, Purchasing_Price, ProfitRatio, Data_ID, Price, Date) VALUES(?Purchasing_Discount,?Price_Type,?Last_Price,?Purchasing_Price,?ProfitRatio,?Data_ID,?Price,?Date)";
                                command = new MySqlCommand(query, dbconnection);
                                command.Parameters.AddWithValue("?Price_Type", "قطعى");
                                command.Parameters.AddWithValue("?Purchasing_Price", calPurchasesPrice());
                                command.Parameters.AddWithValue("?Data_ID", id);
                                command.Parameters.AddWithValue("?ProfitRatio", double.Parse(txtPurchases.Text));
                                command.Parameters.AddWithValue("?Price", price);
                                command.Parameters.AddWithValue("?Last_Price", calPurchasesPrice());
                                command.Parameters.AddWithValue("?Purchasing_Discount", 0.0);
                                command.Parameters.Add("?Date", MySqlDbType.Date);
                                command.Parameters["?Date"].Value = DateTime.Now.Date;
                                command.ExecuteNonQuery();
                                insertIntoAdditionalIncrease(ref PurchasesPrice_ID, ref OldPurchasingPrice_ID);
                                UserControl.ItemRecord("purchasing_price", "اضافة", PurchasesPrice_ID, DateTime.Now, "", dbconnection);

                            }
                            else
                            {
                                MessageBox.Show("error in Data_ID");
                                dbconnection.Close();
                                return;
                            }
                        }
                        #endregion
                    }
                    else
                    {
                        #region set priceList for collection of items
                        if (gridView1.SelectedRowsCount>1)
                        {
                            double NormalPercent = double.Parse(txtNormal.Text);
                            double unNormalPercent = double.Parse(txtUnNormal.Text);

                            double PurchasesPrice = (price + NormalPercent) - ((price + NormalPercent) * PurchasesPercent / 100.0);

                            PurchasesPrice = PurchasesPrice + unNormalPercent;
                            int[] listOfSelectedRows = gridView1.GetSelectedRows();
                            for (int i = 0; i < gridView1.SelectedRowsCount; i++)
                            {
                                DataRowView row = (DataRowView)(((GridView)gridControl1.MainView).GetRow(((GridView)gridControl1.MainView).GetSelectedRows()[i]));

                                if (!checkExist(row[0].ToString()))
                                {
                                    string query = "INSERT INTO purchasing_price (Last_Price,Price_Type,Purchasing_Price,Data_ID,Purchasing_Discount,Price,Normal_Increase,Categorical_Increase,Date) VALUES (?Last_Price,?Price_Type,?Purchasing_Price,?Data_ID,?Purchasing_Discount,?Price,?Normal_Increase,?Categorical_Increase,?Date)";
                                    MySqlCommand command = new MySqlCommand(query, dbconnection);
                                    command.Parameters.AddWithValue("@Price_Type", "لستة");
                                    command.Parameters.AddWithValue("@Purchasing_Price", calPurchasesPrice());
                                    command.Parameters.AddWithValue("?Data_ID", row[0].ToString());
                                    command.Parameters.AddWithValue("@Purchasing_Discount", double.Parse(txtPurchases.Text));
                                    command.Parameters.AddWithValue("@Price", price);
                                    command.Parameters.AddWithValue("@Last_Price", lastPrice(calPurchasesPrice()));
                                    command.Parameters.AddWithValue("@Normal_Increase", double.Parse(txtNormal.Text));
                                    command.Parameters.AddWithValue("@Categorical_Increase", double.Parse(txtUnNormal.Text));
                                    command.Parameters.Add("?Date", MySqlDbType.Date);
                                    command.Parameters["?Date"].Value = DateTime.Now.Date;
                                    command.ExecuteNonQuery();

                                    query = "INSERT INTO oldpurchasing_price (Last_Price,Price_Type,Purchasing_Price,Data_ID,Purchasing_Discount,Price,Normal_Increase,Categorical_Increase,Date) VALUES (?Last_Price,?Price_Type,?Purchasing_Price,?Data_ID,?Purchasing_Discount,?Price,?Normal_Increase,?Categorical_Increase,?Date)";
                                    command = new MySqlCommand(query, dbconnection);
                                    command.Parameters.AddWithValue("@Price_Type", "لستة");
                                    command.Parameters.AddWithValue("@Purchasing_Price", calPurchasesPrice());
                                    command.Parameters.AddWithValue("?Data_ID", row[0].ToString());
                                    command.Parameters.AddWithValue("@Purchasing_Discount", double.Parse(txtPurchases.Text));
                                    command.Parameters.AddWithValue("@Price", price);
                                    command.Parameters.AddWithValue("@Last_Price", lastPrice(calPurchasesPrice()));
                                    command.Parameters.AddWithValue("@Normal_Increase", double.Parse(txtNormal.Text));
                                    command.Parameters.AddWithValue("@Categorical_Increase", double.Parse(txtUnNormal.Text));
                                    command.Parameters.Add("?Date", MySqlDbType.Date);
                                    command.Parameters["?Date"].Value = DateTime.Now.Date;
                                    command.ExecuteNonQuery();
                                    insertIntoAdditionalIncrease(ref PurchasesPrice_ID, ref OldPurchasingPrice_ID);

                                    UserControl.ItemRecord("purchasing_price", "اضافة", PurchasesPrice_ID, DateTime.Now, "", dbconnection);
                                }
                            }
                        }

                        #endregion
                        #region set priceList for one item
                        else
                        {
                            if (id != 0)
                            {
                                double NormalPercent = double.Parse(txtNormal.Text);
                                double unNormalPercent = double.Parse(txtUnNormal.Text);

                                double PurchasesPrice = (price + NormalPercent) - ((price + NormalPercent) * PurchasesPercent / 100.0);

                                PurchasesPrice = PurchasesPrice + unNormalPercent;

                                string query = "INSERT INTO purchasing_price (Last_Price,Price_Type,Purchasing_Price,Data_ID,Purchasing_Discount,Price,Normal_Increase,Categorical_Increase,Date) VALUES (?Last_Price,?Price_Type,?Purchasing_Price,?Data_ID,?Purchasing_Discount,?Price,?Normal_Increase,?Categorical_Increase,?Date)";
                                MySqlCommand command = new MySqlCommand(query, dbconnection);
                                command.Parameters.AddWithValue("@Price_Type", "لستة");
                                command.Parameters.AddWithValue("@Purchasing_Price", calPurchasesPrice());
                                command.Parameters.AddWithValue("?Data_ID", id);
                                command.Parameters.AddWithValue("@Purchasing_Discount", double.Parse(txtPurchases.Text));
                                command.Parameters.AddWithValue("@Price", price);
                                command.Parameters.AddWithValue("@Last_Price", lastPrice(calPurchasesPrice()));
                                command.Parameters.AddWithValue("@Normal_Increase", double.Parse(txtNormal.Text));
                                command.Parameters.AddWithValue("@Categorical_Increase", double.Parse(txtUnNormal.Text));
                                command.Parameters.Add("?Date", MySqlDbType.Date);
                                command.Parameters["?Date"].Value = DateTime.Now.Date;

                                command.ExecuteNonQuery();

                                query = "INSERT INTO oldpurchasing_price (Last_Price,Price_Type,Purchasing_Price,Data_ID,Purchasing_Discount,Price,Normal_Increase,Categorical_Increase,Date) VALUES (?Last_Price,?Price_Type,?Purchasing_Price,?Data_ID,?Purchasing_Discount,?Price,?Normal_Increase,?Categorical_Increase,?Date)";
                                command = new MySqlCommand(query, dbconnection);
                                command.Parameters.AddWithValue("@Price_Type", "لستة");
                                command.Parameters.AddWithValue("@Purchasing_Price", calPurchasesPrice());
                                command.Parameters.AddWithValue("?Data_ID", id);
                                command.Parameters.AddWithValue("@Purchasing_Discount", double.Parse(txtPurchases.Text));
                                command.Parameters.AddWithValue("@Price", price);
                                command.Parameters.AddWithValue("@Last_Price", lastPrice(calPurchasesPrice()));
                                command.Parameters.AddWithValue("@Normal_Increase", double.Parse(txtNormal.Text));
                                command.Parameters.AddWithValue("@Categorical_Increase", double.Parse(txtUnNormal.Text));
                                command.Parameters.Add("?Date", MySqlDbType.Date);
                                command.Parameters["?Date"].Value = DateTime.Now.Date;

                                command.ExecuteNonQuery();

                                insertIntoAdditionalIncrease(ref PurchasesPrice_ID, ref OldPurchasingPrice_ID);
                                UserControl.ItemRecord("purchasing_price", "اضافة", PurchasesPrice_ID, DateTime.Now, "", dbconnection);


                            }
                            else
                            {
                                MessageBox.Show("error in Data_ID");
                                dbconnection.Close();
                                return;
                            }
                        }
                        #endregion
                    }
                   
                    MessageBox.Show("تم");
                    Clear();
                    //productsPurchasesPriceForm.displayProducts();
                }
                else
                {
                    MessageBox.Show("select item");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }
        private void txtBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (load)
                {
                    XtraTabPage xtraTabPage = getTabPage("تسجيل اسعار شراء البنود");
                    if (!IsClear())
                        xtraTabPage.ImageOptions.Image = Properties.Resources.unsave;
                    else
                        xtraTabPage.ImageOptions.Image = null;

                    labPurchasesPrice.Text = calPurchasesPrice() + "";
                }
            }
            catch (Exception)
            {

            }

        }
        private void btnNewPlus_Click(object sender, EventArgs e)
        {
            try
            {
                if (gridView1.SelectedRowsCount>0)
                {
                    if (txtDes.Text != "" && txtPlus.Text != "")
                    {
                        if (radioNormal.Checked)
                        {
                            int n = dataGridView1.Rows.Add();
                            dataGridView1.Rows[n].Cells[0].Value = txtPlus.Text;
                            dataGridView1.Rows[n].Cells[1].Value ="عادية";
                            dataGridView1.Rows[n].Cells[2].Value = txtDes.Text;
                            labPurchasesPrice.Text = "" + calPurchasesPrice();
                        }
                        else if (radioQata3a.Checked)
                        {
                            int n = dataGridView1.Rows.Add();
                            dataGridView1.Rows[n].Cells[0].Value = txtPlus.Text;
                            dataGridView1.Rows[n].Cells[1].Value = "قطعية";
                            dataGridView1.Rows[n].Cells[2].Value = txtDes.Text;
                            labPurchasesPrice.Text = "" + calPurchasesPrice();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                DataGridViewRow row1 = dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex];
                if (row1 != null)
                {
                    dataGridView1.Rows.Remove(row1);
                    labPurchasesPrice.Text = calPurchasesPrice() + "";
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
        }
        private void gridView1_SelectionChanged(object sender, DevExpress.Data.SelectionChangedEventArgs e)
        {
            try
            {
                if (gridView1.SelectedRowsCount == 1)
                {
                    txtCode.Visible = true;
                    panCodeParts.Visible = true;
                    label6.Visible = true;
                    label19.Visible = true;
                    labPurchasesPrice.Visible = true;
                    DataRowView row = (DataRowView)(((GridView)gridControl1.MainView).GetRow(((GridView)gridControl1.MainView).GetSelectedRows()[0]));
                    if (row != null)
                    {
                        txtCode.Text = row[1].ToString();
                        id = Convert.ToInt32(row[0].ToString());
                        String code = txtCode.Text;
                        displayCode(code);
                    }
                }
                else if (gridView1.SelectedRowsCount > 1)
                {
                    DataRowView row = (DataRowView)(((GridView)gridControl1.MainView).GetRow(((GridView)gridControl1.MainView).GetSelectedRows()[0]));

                    txtCode.Visible = false;
                    panCodeParts.Visible = false;
                    label6.Visible = false;
                    label19.Visible = false;
                    labPurchasesPrice.Visible = false;
                }
                else
                {
                    txtCode.Visible = true;
                    panCodeParts.Visible = true;
                    label6.Visible = true;
                    label19.Visible = true;
                    labPurchasesPrice.Visible = true;
                    txtCode.Text = "";
                    txtCodePart1.Text = txtCodePart2.Text = txtCodePart3.Text = txtCodePart4.Text = txtCodePart5.Text = "";
                    id = 0;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //function 
        public void displayProducts()
        {
            string q1, q2, q3, q4, fQuery = "";
            if (txtType.Text == "")
            {
                q1 = "select Type_ID from data";
            }
            else
            {
                q1 = txtType.Text;
            }
            if (txtFactory.Text == "")
            {
                q2 = "select Factory_ID from data";
            }
            else
            {
                q2 = txtFactory.Text;
            }
            if (txtProduct.Text == "")
            {
                q3 = "select Product_ID from data";
            }
            else
            {
                q3 = txtProduct.Text;
            }
            if (txtGroup.Text == "")
            {
                q4 = "select Group_ID from data";
            }
            else
            {
                q4 = txtGroup.Text;
            }

            if (comSize.Text != "")
            {
                fQuery += "and size.Size_ID='" + comSize.SelectedValue + "'";
            }
        
            if (comColor.Text != "")
            {
                fQuery += "and color.Color_ID='" + comColor.SelectedValue + "'";
            }
            if (comSort.Text != "")
            {
                fQuery += "and Sort.Sort_ID='" + comSort.SelectedValue + "'";
            }
            if (comClassfication.Text != "")
            {
                fQuery += "and data.Classification='" + comClassfication.Text + "'";
            }

            string query = "SELECT data.Data_ID,data.Code as 'الكود',product.Product_Name as 'الصنف',type.Type_Name as 'النوع',factory.Factory_Name as 'المصنع',groupo.Group_Name as 'المجموعة',color.Color_Name as 'اللون',size.Size_Value as 'المقاس',sort.Sort_Value as 'الفرز',data.Classification as 'التصنيف',data.Description as 'الوصف',data.Carton as 'الكرتنة' from data INNER JOIN type ON type.Type_ID = data.Type_ID INNER JOIN product ON product.Product_ID = data.Product_ID INNER JOIN factory ON data.Factory_ID = factory.Factory_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID LEFT outer JOIN color ON data.Color_ID = color.Color_ID LEFT outer  JOIN size ON data.Size_ID = size.Size_ID LEFT outer  JOIN sort ON data.Sort_ID = sort.Sort_ID where  data.Type_ID IN(" + q1 + ") and  data.Factory_ID  IN(" + q2 + ") and  data.Product_ID  IN(" + q3 + ") and data.Group_ID IN (" + q4 + ") and data.Data_ID not in (" + getDataIDsWhichHavepurchasing_price ()+ ") " + fQuery+ " order by SUBSTR(data.Code,1,16) ,color.Color_Name,data.Description,data.Sort_ID";
            MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
            DataTable dt = new DataTable();
            da.Fill(dt);
            gridControl1.DataSource = dt;
            gridView1.Columns[0].Visible = false;
            gridView1.Columns[1].Width = 140;
            fQuery = "";
            gridView1.BestFitColumns();
        }
        public XtraTabPage getTabPage(string text)
        {
            for (int i = 0; i < xtraTabControlPurchasesContent.TabPages.Count; i++)
                if (xtraTabControlPurchasesContent.TabPages[i].Text == text)
                {
                    return xtraTabControlPurchasesContent.TabPages[i];
                }
            return null;
        }
        public bool IsClear()
        {
            if (txtCode.Text == "" &&
                txtPrice.Text == "0" &&
                txtPurchases.Text == "0" &&
                txtNormal.Text == "0" &&
                txtUnNormal.Text == "0" &&
                radioList.Checked == true)
                return true;
            else
                return false;
        }
        public void Clear()
        {
            txtCode.Text = "";
            txtPrice.Text = "0";
            txtPurchases.Text = "0";
            txtNormal.Text = "0";
            txtUnNormal.Text = "0";
            txtDes.Text = "";
            txtPlus.Text = "";
            dataGridView1.Rows.Clear();
            txtCodePart1.Text = txtCodePart2.Text = txtCodePart3.Text = txtCodePart4.Text = txtCodePart5.Text = "";
            radioList.Checked = true;
        }
        public double calPurchasesPrice()
        {
            double addational = 0.0;
            double price = double.Parse(txtPrice.Text);
            if (dataGridView1.Rows.Count > 0)
            {
                foreach (DataGridViewRow item in dataGridView1.Rows)
                {
                    if (item.Cells[1].Value.ToString() == "عادية")
                        price += Convert.ToDouble(item.Cells[0].Value);
                    else if (item.Cells[1].Value.ToString() == "قطعية")
                        addational += Convert.ToDouble(item.Cells[0].Value);
                }
            }
            double PurchasesPercent = double.Parse(txtPurchases.Text);
            if (radioQata3y.Checked == true)
            {
                return price + (price * PurchasesPercent / 100.0) + addational;
            }
            else
            {
                double NormalPercent = double.Parse(txtNormal.Text);
                double unNormalPercent = double.Parse(txtUnNormal.Text);
                double PurchasesPrice = (price + NormalPercent) - ((price + NormalPercent) * PurchasesPercent / 100.0);
                PurchasesPrice = PurchasesPrice + unNormalPercent;
                return PurchasesPrice + addational;
            }
        }
        public double lastPrice(double purchasePrice)
        {
            double discount = double.Parse(txtPurchases.Text);
            double lastPrice = purchasePrice * 100 / (100 - discount);

            return lastPrice;
        }
        public void makeCode(TextBox txtBox)
        {
            string code = txtCode.Text;
            int j = 4 - txtBox.TextLength;
            for (int i = 0; i < j; i++)
            {
                code += "0";
            }
            code += txtBox.Text;


            txtCode.Text = code;

        }
        public void displayCode(string code)
        {
            char[] arrCode = code.ToCharArray();
            txtCodePart1.Text = Convert.ToInt32(arrCode[0].ToString() + arrCode[1].ToString() + arrCode[2].ToString() + arrCode[3].ToString()) + "";
            txtCodePart2.Text = Convert.ToInt32(arrCode[4].ToString() + arrCode[5].ToString() + arrCode[6].ToString() + arrCode[7].ToString()) + "";
            txtCodePart3.Text = Convert.ToInt32(arrCode[8].ToString() + arrCode[9].ToString() + arrCode[10].ToString() + arrCode[11].ToString()) + "";
            txtCodePart4.Text = Convert.ToInt32(arrCode[12].ToString() + arrCode[13].ToString() + arrCode[14].ToString() + arrCode[15].ToString()) + "";
            txtCodePart5.Text = "" + Convert.ToInt32(arrCode[16].ToString() + arrCode[17].ToString() + arrCode[18].ToString() + arrCode[19].ToString());
        }
        public void insertIntoAdditionalIncrease(ref int PurchasingPrice_ID, ref int OldPurchasingPrice_ID)
        {
            string queryx = "select PurchasingPrice_ID from purchasing_price order by PurchasingPrice_ID desc limit 1";
            MySqlCommand com = new MySqlCommand(queryx, dbconnection);
            if (com.ExecuteScalar() != null)
            {
                PurchasingPrice_ID = Convert.ToInt32(com.ExecuteScalar());
            }
            //for archive table
            queryx = "select OldPurchasingPrice_ID from oldpurchasing_price order by OldPurchasingPrice_ID desc limit 1";
            com = new MySqlCommand(queryx, dbconnection);
            if (com.ExecuteScalar() != null)
            {
                OldPurchasingPrice_ID = Convert.ToInt32(com.ExecuteScalar());
            }
            foreach (DataGridViewRow item in dataGridView1.Rows)
            {
                double addational = Convert.ToDouble(item.Cells[0].Value);
                queryx = "insert into additional_increase_purchasingprice (PurchasingPrice_ID,AdditionalValue,Type,Description) values (@PurchasingPrice_ID,@AdditionalValue,@Type,@Description)";
                com = new MySqlCommand(queryx, dbconnection);
                com.Parameters.AddWithValue("@PurchasingPrice_ID", PurchasingPrice_ID);
                com.Parameters.AddWithValue("@Type", item.Cells[1].Value);
                com.Parameters.AddWithValue("@AdditionalValue", item.Cells[0].Value);
                com.Parameters.AddWithValue("@Description", item.Cells[2].Value);
                com.ExecuteNonQuery();

                //insert into archive table
                queryx = "insert into old_additional_increase_purchasingprice (OldPurchasingPrice_ID,AdditionalValue,Type,Description) values (@OldPurchasingPrice_ID,@AdditionalValue,@Type,@Description)";
                com = new MySqlCommand(queryx, dbconnection);
                com.Parameters.AddWithValue("@OldPurchasingPrice_ID", OldPurchasingPrice_ID);
                com.Parameters.AddWithValue("@Type", item.Cells[1].Value);
                com.Parameters.AddWithValue("@AdditionalValue", item.Cells[0].Value);
                com.Parameters.AddWithValue("@Description", item.Cells[2].Value);
                com.ExecuteNonQuery();

            }
        }
        public string getDataIDsWhichHavepurchasing_price()
        {
            dbconnection.Open();
            string query = "select Data_ID from purchasing_price where Data_ID is not null";
            MySqlCommand com = new MySqlCommand(query, dbconnection);
            MySqlDataReader dr = com.ExecuteReader();
            string DataIDs = "";
            while (dr.Read())
            {
                DataIDs += dr[0].ToString() + ",";
            }
            dr.Close();
            DataIDs += "0";
            dbconnection.Close();
            return DataIDs;
        }
        public bool checkExist(string Data_ID)
        {
            string query = "select PurchasingPrice_ID from purchasing_price where Data_ID=" + Data_ID;
            MySqlCommand com = new MySqlCommand(query, dbconnection);
            if (com.ExecuteScalar() != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


    }
}

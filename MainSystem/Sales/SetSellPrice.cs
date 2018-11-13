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
    public partial class SetSellPrice : Form
    {
        MySqlConnection dbconnection;
        ProductsSellPriceForm productsSellPriceForm = null;
        XtraTabControl xtraTabControlSalesContent = null;
        bool load = false;
        bool factoryFlage = false;
        bool groupFlage = false;
        bool flagProduct = false;
        int id = 0;

        public SetSellPrice(ProductsSellPriceForm productsSellPriceForm, XtraTabControl xtraTabControlSalesContent)
        {
            try
            {
                InitializeComponent();
                this.xtraTabControlSalesContent = xtraTabControlSalesContent;
                this.productsSellPriceForm = productsSellPriceForm;
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
                label14.Text = "خصم البيع";
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
                label14.Text = "نسبة البيع";
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
        private void SetSellPrice_Load(object sender, EventArgs e)
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

                query = "select * from data";
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
                                if (txtType.Text == "1" || txtType.Text == "2")
                                {
                                    string query2 = "select * from groupo where Factory_ID=0 and Type_ID=1";
                                    MySqlDataAdapter da2 = new MySqlDataAdapter(query2, dbconnection);
                                    DataTable dt2 = new DataTable();
                                    da2.Fill(dt2);
                                    comGroup.DataSource = dt2;
                                    comGroup.DisplayMember = dt2.Columns["Group_Name"].ToString();
                                    comGroup.ValueMember = dt2.Columns["Group_ID"].ToString();
                                    comGroup.Text = "";
                                    txtGroup.Text = "";
                                }
                                else if (txtType.Text == "4")
                                {
                                    string query2 = "select * from groupo where Factory_ID=-1 and Type_ID=4";
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
                                if (txtType.Text != "1" && txtType.Text != "2" && txtType.Text != "4")
                                {
                                    string query2f = "select * from groupo where Factory_ID=" + txtFactory.Text;
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
                            }
                            break;
                        case "comGroup":
                            if (groupFlage)
                            {
                                txtGroup.Text = comGroup.SelectedValue.ToString();

                                string query3 = "select distinct  product.Product_ID  ,Product_Name  from product inner join product_factory_group on product.Product_ID=product_factory_group.Product_ID  where product.Type_ID=" + txtType.Text + " and product_factory_group.Factory_ID=" + txtFactory.Text + " and product_factory_group.Group_ID=" + txtGroup.Text + "  order by product.Product_ID";
                                MySqlDataAdapter da3 = new MySqlDataAdapter(query3, dbconnection);
                                DataTable dt3 = new DataTable();
                                da3.Fill(dt3);
                                comProduct.DataSource = dt3;
                                comProduct.DisplayMember = dt3.Columns["Product_Name"].ToString();
                                comProduct.ValueMember = dt3.Columns["Product_ID"].ToString();
                                comProduct.Text = "";
                                txtProduct.Text = "";


                                string query2 = "select * from size where Factory_ID=" + txtFactory.Text + " and Group_ID=" + txtGroup.Text;
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
                            if (flagProduct)
                            {
                                flagProduct = false;
                                txtProduct.Text = comProduct.SelectedValue.ToString();
                                comColor.Focus();
                            }
                            break;

                        case "comColour":
                            txtColor.Text = comColor.SelectedValue.ToString();
                            comSize.Focus();
                            break;

                        case "comSize":
                            txtSize.Text = comSize.SelectedValue.ToString();
                            txtSort.Focus();
                            break;

                        case "comSort":
                            txtSort.Text = comSort.SelectedValue.ToString();
                            comClassfication.Focus();
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                //  MessageBox.Show(ex.Message);
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
                                    displayProducts();
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
                                    displayProducts();
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
                                    displayProducts();
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
                                    displayProducts();
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
                                    displayProducts();
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
                displayProducts();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void gridControl1_EditorKeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                DataRowView row = (DataRowView)(((GridView)gridControl1.MainView).GetRow(((GridView)gridControl1.MainView).GetSelectedRows()[0]));
                if (row != null)
                {
                    txtCode.Text = row[1].ToString();
                    id = Convert.ToInt16(row[0].ToString());
                    String code = txtCode.Text;
                    displayCode(code);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void comType_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (load)
                {
                    displayProducts();
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
                if (txtCode.Text != "" || chBoxSelectAll.Checked)
                {
                    dbconnection.Open();
                    double price = double.Parse(txtPrice.Text);
                    double SellPercent = double.Parse(txtSell.Text);

                    if (radioQata3y.Checked == true)
                    {
                        #region set qata3yPrice for list item
                        if (txtCode.Text == "" && chBoxSelectAll.Checked)
                        {
                            double NormalPercent = double.Parse(txtNormal.Text);
                            double UnNormalPercent = double.Parse(txtUnNormal.Text);
                            DataTable dataTable = (DataTable)gridControl1.DataSource;
                            for (int i = 0; i < dataTable.Rows.Count; i++)
                            {
                                string query = "INSERT INTO sellprice (Sell_Discount,Price_Type, Sell_Price, ProfitRatio, Data_ID, Price,Last_Price, PercentageDelegate,Date) VALUES(?Sell_Discount,?Price_Type,?Sell_Price,?ProfitRatio,?Data_ID,?Price,?Last_Price,?PercentageDelegate,?Date)";
                                MySqlCommand command = new MySqlCommand(query, dbconnection);
                                command.Parameters.AddWithValue("?Price_Type", "قطعى");
                                command.Parameters.AddWithValue("?Sell_Price", calSellPrice());
                                command.Parameters.AddWithValue("?Data_ID", dataTable.Rows[i][0].ToString());
                                command.Parameters.AddWithValue("?ProfitRatio", double.Parse(txtSell.Text));
                                command.Parameters.AddWithValue("?Price", price);
                                command.Parameters.AddWithValue("?Last_Price", calSellPrice());
                                command.Parameters.AddWithValue("?Sell_Discount", 0.0);
                                command.Parameters.AddWithValue("?PercentageDelegate", double.Parse(txtPercentageDelegate.Text));
                                command.Parameters.Add("?Date", MySqlDbType.Date);
                                command.Parameters["?Date"].Value = DateTime.Now.Date;

                                command.ExecuteNonQuery();
                          
                            }
                        }
                        #endregion
                        #region set qata3yPrice for one item
                        else
                        {
                            if (id != 0)
                            {
                                string query = "INSERT INTO sellprice (Sell_Discount,Price_Type,Last_Price, Sell_Price, ProfitRatio, Data_ID, Price, PercentageDelegate,Date) VALUES(?Sell_Discount,?Price_Type,?Last_Price,?Sell_Price,?ProfitRatio,?Data_ID,?Price,?PercentageDelegate,?Date)";
                                MySqlCommand command = new MySqlCommand(query, dbconnection);
                                command.Parameters.AddWithValue("?Price_Type", "قطعى");
                                command.Parameters.AddWithValue("?Sell_Price", calSellPrice());
                                command.Parameters.AddWithValue("?Data_ID", id);
                                command.Parameters.AddWithValue("?ProfitRatio", double.Parse(txtSell.Text));
                                command.Parameters.AddWithValue("?Price", price);
                                command.Parameters.AddWithValue("?Last_Price", calSellPrice());
                                command.Parameters.AddWithValue("?Sell_Discount", 0.0);
                                command.Parameters.AddWithValue("?PercentageDelegate", double.Parse(txtPercentageDelegate.Text));
                                command.Parameters.Add("?Date", MySqlDbType.Date);
                                command.Parameters["?Date"].Value = DateTime.Now.Date;


                                command.ExecuteNonQuery();
                            
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
                        if (txtCode.Text == "" && chBoxSelectAll.Checked)
                        {
                            double NormalPercent = double.Parse(txtNormal.Text);
                            double unNormalPercent = double.Parse(txtUnNormal.Text);

                            double sellPrice = (price + NormalPercent) - ((price + NormalPercent) * SellPercent / 100.0);

                            sellPrice = sellPrice + unNormalPercent;
                            DataTable dataTable = (DataTable)gridControl1.DataSource;
                            for (int i = 0; i < dataTable.Rows.Count; i++)
                            {

                                string query = "INSERT INTO sellprice (Last_Price,Price_Type,Sell_Price,Data_ID,Sell_Discount,Price,Normal_Increase,Categorical_Increase,PercentageDelegate,Date) VALUES (?Last_Price,?Price_Type,?Sell_Price,?Data_ID,?Sell_Discount,?Price,?Normal_Increase,?Categorical_Increase,?PercentageDelegate,?Date)";
                                MySqlCommand command = new MySqlCommand(query, dbconnection);
                                command.Parameters.AddWithValue("@Price_Type", "لستة");
                                command.Parameters.AddWithValue("@Sell_Price", calSellPrice());
                                command.Parameters.AddWithValue("?Data_ID", dataTable.Rows[i][0].ToString());
                                command.Parameters.AddWithValue("@Sell_Discount", double.Parse(txtSell.Text));
                                command.Parameters.AddWithValue("@Price", price); 
                                command.Parameters.AddWithValue("@Last_Price", lastPrice());
                                command.Parameters.AddWithValue("@Normal_Increase", double.Parse(txtNormal.Text));
                                command.Parameters.AddWithValue("@Categorical_Increase", double.Parse(txtUnNormal.Text));
                                command.Parameters.AddWithValue("@PercentageDelegate", double.Parse(txtPercentageDelegate.Text));
                                command.Parameters.Add("?Date", MySqlDbType.Date);
                                command.Parameters["?Date"].Value = DateTime.Now.Date;

                                command.ExecuteNonQuery();
                             
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

                                double sellPrice = (price + NormalPercent) - ((price + NormalPercent) * SellPercent / 100.0);

                                sellPrice = sellPrice + unNormalPercent;

                                string query = "INSERT INTO sellprice (Last_Price,Price_Type,Sell_Price,Data_ID,Sell_Discount,Price,Normal_Increase,Categorical_Increase,PercentageDelegate,Date) VALUES (?Last_Price,?Price_Type,?Sell_Price,?Data_ID,?Sell_Discount,?Price,?Normal_Increase,?Categorical_Increase,?PercentageDelegate,?Date)";
                                MySqlCommand command = new MySqlCommand(query, dbconnection);
                                command.Parameters.AddWithValue("@Price_Type", "لستة");
                                command.Parameters.AddWithValue("@Sell_Price", calSellPrice());
                                command.Parameters.AddWithValue("?Data_ID", id);
                                command.Parameters.AddWithValue("@Sell_Discount", double.Parse(txtSell.Text));
                                command.Parameters.AddWithValue("@Price", price);
                                command.Parameters.AddWithValue("@Last_Price", lastPrice());
                                command.Parameters.AddWithValue("@Normal_Increase", double.Parse(txtNormal.Text));
                                command.Parameters.AddWithValue("@Categorical_Increase", double.Parse(txtUnNormal.Text));
                                command.Parameters.AddWithValue("@PercentageDelegate", double.Parse(txtPercentageDelegate.Text));
                                command.Parameters.Add("?Date", MySqlDbType.Date);
                                command.Parameters["?Date"].Value = DateTime.Now.Date;

                                command.ExecuteNonQuery();

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

                    int sellPrice_ID = 0;
                    string queryx = "select SellPrice_ID from sellprice order by SellPrice_ID desc limit 1";
                    MySqlCommand com = new MySqlCommand(queryx, dbconnection);
                    if (com.ExecuteScalar() != null)
                    {
                        sellPrice_ID = Convert.ToInt16(com.ExecuteScalar());
                    }
                    foreach (DataGridViewRow item in dataGridView1.Rows)
                    {
                        double addational = Convert.ToDouble(item.Cells[0].Value);
                        queryx = "insert into additional_increase_sellprice (SellPrice_ID,AdditionalValue,Type,Description) values (@SellPrice_ID,@AdditionalValue,@Type,@Description)";
                        com = new MySqlCommand(queryx, dbconnection);
                        com.Parameters.AddWithValue("@SellPrice_ID", sellPrice_ID);
                        com.Parameters.AddWithValue("@Type", item.Cells[1].Value);
                        com.Parameters.AddWithValue("@AdditionalValue", item.Cells[0].Value);
                        com.Parameters.AddWithValue("@Description", item.Cells[2].Value);
                        com.ExecuteNonQuery();

                    }
                    UserControl.ItemRecord("sellprice", "اضافة", sellPrice_ID, DateTime.Now, "", dbconnection);

                    MessageBox.Show("Done");
                    Clear();
                    productsSellPriceForm.displayProducts();
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
        private void chBoxSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                gridView1.OptionsSelection.MultiSelect = true;
                if (chBoxSelectAll.Checked)
                {
                    gridView1.SelectRows(0, gridView1.RowCount - 1);
                }
                else
                {
                    int selectedRowsCount = gridView1.SelectedRowsCount;
                    for (int i = 0; i < selectedRowsCount; i++)
                    {
                        gridView1.UnselectRow(i);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void txtBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (load)
                {
                    XtraTabPage xtraTabPage = getTabPage("تسجيل اسعار البنود");
                    if (!IsClear())
                        xtraTabPage.ImageOptions.Image = Properties.Resources.unsave__2_;
                    else
                        xtraTabPage.ImageOptions.Image = null;

                    labSellPrice.Text = calSellPrice() + "";
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
                if (txtCode.Text != ""||chBoxSelectAll.Checked)
                {
                    if (txtDes.Text != "" && txtPlus.Text != "")
                    {
                        if (radioNormal.Checked)
                        {
                            int n = dataGridView1.Rows.Add();
                            dataGridView1.Rows[n].Cells[0].Value = txtPlus.Text;
                            dataGridView1.Rows[n].Cells[1].Value ="عادية";
                            dataGridView1.Rows[n].Cells[2].Value = txtDes.Text;
                            labSellPrice.Text = "" + calSellPrice();
                        }
                        else if (radioQata3a.Checked)
                        {
                            int n = dataGridView1.Rows.Add();
                            dataGridView1.Rows[n].Cells[0].Value = txtPlus.Text;
                            dataGridView1.Rows[n].Cells[1].Value = "قطعية";
                            dataGridView1.Rows[n].Cells[2].Value = txtDes.Text;
                            labSellPrice.Text = "" + calSellPrice();
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
                    labSellPrice.Text = calSellPrice()+"";
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

            string query = "SELECT data.Data_ID,data.Code as 'الكود',product.Product_Name as 'الصنف',type.Type_Name as 'النوع',factory.Factory_Name as 'المصنع',groupo.Group_Name as 'المجموعة',color.Color_Name as 'اللون',size.Size_Value as 'المقاس',sort.Sort_Value as 'الفرز',data.Classification as 'التصنيف',data.Description as 'الوصف',data.Carton as 'الكرتنة' from data INNER JOIN type ON type.Type_ID = data.Type_ID INNER JOIN product ON product.Product_ID = data.Product_ID INNER JOIN factory ON data.Factory_ID = factory.Factory_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID LEFT outer JOIN color ON data.Color_ID = color.Color_ID LEFT outer  JOIN size ON data.Size_ID = size.Size_ID LEFT outer  JOIN sort ON data.Sort_ID = sort.Sort_ID where  data.Type_ID IN(" + q1 + ") and  data.Factory_ID  IN(" + q2 + ") and  data.Product_ID  IN(" + q3 + ") and data.Group_ID IN (" + q4 + ") " + fQuery;
            MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
            DataTable dt = new DataTable();
            da.Fill(dt);
            gridControl1.DataSource = dt;
            gridView1.Columns[0].Visible = false;
            gridView1.Columns[1].Width = 140;
            fQuery = "";
            chBoxSelectAll.Checked = false;
        }
        public XtraTabPage getTabPage(string text)
        {
            for (int i = 0; i < xtraTabControlSalesContent.TabPages.Count; i++)
                if (xtraTabControlSalesContent.TabPages[i].Text == text)
                {
                    return xtraTabControlSalesContent.TabPages[i];
                }
            return null;
        }
        public bool IsClear()
        {
            if (txtCode.Text == "" &&
                txtPrice.Text == "0" &&
                txtSell.Text == "0" &&
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
            txtSell.Text = "0";
            txtNormal.Text = "0";
            txtUnNormal.Text = "0";
            txtDes.Text = "";
            txtPlus.Text = "";
            dataGridView1.Rows.Clear();
            txtCodePart1.Text = txtCodePart2.Text = txtCodePart3.Text = txtCodePart4.Text = txtCodePart5.Text = "";
            radioList.Checked = true;
        }
        public double calSellPrice()
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
            double SellPercent = double.Parse(txtSell.Text);
            if (radioQata3y.Checked == true)
            {
                return price + (price * SellPercent / 100.0) + addational;

            }
            else
            {
                double NormalPercent = double.Parse(txtNormal.Text);
                double unNormalPercent = double.Parse(txtUnNormal.Text);
                double sellPrice = (price + NormalPercent) - ((price + NormalPercent) * SellPercent / 100.0);
                sellPrice = sellPrice + unNormalPercent;
                return sellPrice + addational;
            }
        }
        public double lastPrice()
        {
            double price = double.Parse(txtPrice.Text);
            if (dataGridView1.Rows.Count > 0)
            {
                foreach (DataGridViewRow item in dataGridView1.Rows)
                {
                    if (item.Cells[1].Value.ToString() == "عادية")
                        price += Convert.ToDouble(item.Cells[0].Value);
                }
            }
            price += double.Parse(txtNormal.Text);
            return price;
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
            txtCodePart1.Text = Convert.ToInt16(arrCode[0].ToString() + arrCode[1].ToString() + arrCode[2].ToString() + arrCode[3].ToString()) + "";
            txtCodePart2.Text = Convert.ToInt16(arrCode[4].ToString() + arrCode[5].ToString() + arrCode[6].ToString() + arrCode[7].ToString()) + "";
            txtCodePart3.Text = Convert.ToInt16(arrCode[8].ToString() + arrCode[9].ToString() + arrCode[10].ToString() + arrCode[11].ToString()) + "";
            txtCodePart4.Text = Convert.ToInt16(arrCode[12].ToString() + arrCode[13].ToString() + arrCode[14].ToString() + arrCode[15].ToString()) + "";
            txtCodePart5.Text = "" + Convert.ToInt16(arrCode[16].ToString() + arrCode[17].ToString() + arrCode[18].ToString() + arrCode[19].ToString());
        }

      
    }
}

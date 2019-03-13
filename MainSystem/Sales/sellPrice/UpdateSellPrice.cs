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
    public partial class UpdateSellPrice : Form
    {
        MySqlConnection dbconnection;
        ProductsSellPriceForm productsSellPriceForm = null;
        XtraTabControl xtraTabControlSalesContent = null;
        List<DataRowView> rows = null;
        customPanel customPanel;
        bool load = false;
        int id = 0;
        String query = "";
        public UpdateSellPrice(List<DataRowView> rows, ProductsSellPriceForm productsSellPriceForm, string query, XtraTabControl xtraTabControlSalesContent)
        {
            try
            {
                InitializeComponent();
                this.xtraTabControlSalesContent = xtraTabControlSalesContent;
                this.rows = rows;
                this.productsSellPriceForm = productsSellPriceForm;
                this.query = query;
                dbconnection = new MySqlConnection(connection.connectionString);

                customPanel = new customPanel();
                panContent.Controls.Add(customPanel.Controls["panContent"]);
                panContent.Controls["panContent"].Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void UpdateSellPrice_Load(object sender, EventArgs e)
        {
            try
            {
                dbconnection.Open();
                string ids = "";
                for (int i = 0; i < rows.Count - 1; i++)
                {
                    ids += rows[i][0] + ",";
                }
                ids += rows[rows.Count - 1][0];
                query = "SELECT SellPrice.SellPrice_ID, data.Code as 'الكود',concat( product.Product_Name,' ',type.Type_Name,' ',factory.Factory_Name,' ',groupo.Group_Name,' ' ,color.Color_Name,' ' ,size.Size_Value )as 'البند',sort.Sort_Value as 'الفرز',data.Classification as 'التصنيف',data.Description as 'الوصف',data.Carton as 'الكرتنة',sellprice.Price as 'السعر',sellprice.Price_Type as 'نوع السعر',sellprice.Sell_Discount as 'خصم البيع',sellprice.Normal_Increase as 'الزيادة العادية',sellprice.Categorical_Increase as 'الزيادة القطعية',sellprice.ProfitRatio as 'نسبة البيع',sellprice.Sell_Price as 'سعر البيع',sellprice.PercentageDelegate as 'نسية المندوب',sellprice.Date as 'التاريخ'   from data INNER JOIN sellprice on sellprice.Data_ID=data.Data_ID  INNER JOIN type ON type.Type_ID = data.Type_ID INNER JOIN product ON product.Product_ID = data.Product_ID INNER JOIN factory ON data.Factory_ID = factory.Factory_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID LEFT outer JOIN color ON data.Color_ID = color.Color_ID LEFT outer  JOIN size ON data.Size_ID = size.Size_ID LEFT outer  JOIN sort ON data.Sort_ID = sort.Sort_ID where   SellPrice.SellPrice_ID in(" + ids + ")";

                MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                DataTable dt = new DataTable();
                da.Fill(dt);

                gridControl1.DataSource = dt;
                gridView1.Columns[0].Visible = false;
                gridView1.Columns[1].Width = 140;
                gridView1.Columns[2].Width = 200;

                setData((DataRowView)gridView1.GetRow(0));
                load = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            dbconnection.Close();
        }
        //deign event
        private void chBoxAdditionalIncrease_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chBoxAdditionalIncrease.Checked)
                {
                    tLPanCpntent.RowStyles[1].Height = 360;
                    
                }
                else
                {
                    tLPanCpntent.RowStyles[1].Height = 200;
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
        private void chBoxSpecialIncrease_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chBoxSpecialIncrease.Checked)
                {
                    tLPanCpntent.RowStyles[1].Height = 200;
                   
                    foreach (Control item in panContent.Controls)
                    {
                        item.Visible = false;
                    }
                    panContent.Controls["panContent"].Visible = true;
                    label19.Visible = false;
                    labSellPrice.Visible = false;
                }
                else
                {
                    if(chBoxAdditionalIncrease.Checked)
                        tLPanCpntent.RowStyles[1].Height = 360;
                    else
                        tLPanCpntent.RowStyles[1].Height = 200;


                    
                    foreach (Control item in panContent.Controls)
                    {
                        item.Visible = true;
                    }
                    panContent.Controls["panContent"].Visible = false;
                    label19.Visible = true;
                    labSellPrice.Visible = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //main events
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
                            case "txtCodePart1":
                                query = "select Type_Name from type where Type_ID='" + txtCodePart1.Text + "'";
                                com = new MySqlCommand(query, dbconnection);
                                if (com.ExecuteScalar() != null)
                                {
                                    Name = (string)com.ExecuteScalar();
                                 
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
        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                dbconnection.Open();
                if (!chBoxSpecialIncrease.Checked)
                {
                    double price = double.Parse(txtPrice.Text);
                    double SellPercent = double.Parse(txtSell.Text);

                    if (radioQata3y.Checked == true)
                    {
                        #region set qata3yPrice for list item

                        double NormalPercent = double.Parse(txtNormal.Text);
                        double UnNormalPercent = double.Parse(txtUnNormal.Text);

                        DataTable dataTable = (DataTable)gridControl1.DataSource;
                        for (int i = 0; i < dataTable.Rows.Count; i++)
                        {
                            if (gridView1.IsRowSelected(i))
                            {
                                String query = "update sellprice set Sell_Discount=@Sell_Discount,Normal_Increase=@Normal_Increase,Categorical_Increase=@Categorical_Increase,Price_Type=@Price_Type,Sell_Price=@Sell_Price,ProfitRatio=@ProfitRatio,Price=@Price,PercentageDelegate=@PercentageDelegate,Date=@Date where SellPrice_ID=" + dataTable.Rows[i][0].ToString();

                                MySqlCommand command = new MySqlCommand(query, dbconnection);
                                command.Parameters.AddWithValue("@Price_Type", "قطعى");
                                command.Parameters.AddWithValue("@Sell_Price", price + (price * SellPercent / 100.0));
                                command.Parameters.AddWithValue("@ProfitRatio", SellPercent);
                                command.Parameters.AddWithValue("@Sell_Discount", 0.00);
                                command.Parameters.AddWithValue("@Price", price);
                                command.Parameters.AddWithValue("@Normal_Increase", 0.00);
                                command.Parameters.AddWithValue("@Categorical_Increase", 0.00);
                                command.Parameters.AddWithValue("@PercentageDelegate", double.Parse(txtPercentageDelegate.Text));
                                command.Parameters.Add("?Date", MySqlDbType.Date);
                                command.Parameters["?Date"].Value = DateTime.Now.Date;
                                command.ExecuteNonQuery();
                                //insert into Archif Table
                                query = "INSERT INTO oldsellprice (Sell_Discount,Price_Type, Sell_Price, ProfitRatio, Data_ID, Price,Last_Price, PercentageDelegate,Date) VALUES(?Sell_Discount,?Price_Type,?Sell_Price,?ProfitRatio,?Data_ID,?Price,?Last_Price,?PercentageDelegate,?Date)";
                                command = new MySqlCommand(query, dbconnection);
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
                    }
                    else
                    {
                        #region set priceList for collection of items

                        double NormalPercent = double.Parse(txtNormal.Text);
                        double unNormalPercent = double.Parse(txtUnNormal.Text);

                        double sellPrice = (price + NormalPercent) - ((price + NormalPercent) * SellPercent / 100.0);

                        sellPrice = sellPrice + unNormalPercent;

                        DataTable dataTable = (DataTable)gridControl1.DataSource;
                        for (int i = 0; i < dataTable.Rows.Count; i++)
                        {
                            if (gridView1.IsRowSelected(i))
                            {
                                string query = "update sellprice set ProfitRatio=@ProfitRatio, Price_Type=@Price_Type,Sell_Price=@Sell_Price,Sell_Discount=@Sell_Discount,Price=@Price,Normal_Increase=@Normal_Increase,Categorical_Increase=@Categorical_Increase,PercentageDelegate=@PercentageDelegate ,Last_Price=@Last_Price,Date=@Date where SellPrice_ID =" + dataTable.Rows[i][0].ToString();

                                MySqlCommand command = new MySqlCommand(query, dbconnection);
                                command.Parameters.AddWithValue("@Price_Type", "لستة");
                                command.Parameters.AddWithValue("@Sell_Price", sellPrice);
                                command.Parameters.AddWithValue("@ProfitRatio", 0.00);
                                command.Parameters.AddWithValue("@Sell_Discount", double.Parse(txtSell.Text));
                                command.Parameters.AddWithValue("@Price", price);
                                command.Parameters.AddWithValue("@Last_Price", lastPrice());
                                command.Parameters.AddWithValue("@Normal_Increase", double.Parse(txtNormal.Text));
                                command.Parameters.AddWithValue("@Categorical_Increase", double.Parse(txtUnNormal.Text));
                                command.Parameters.AddWithValue("@PercentageDelegate", double.Parse(txtPercentageDelegate.Text));
                                command.Parameters.Add("?Date", MySqlDbType.Date);
                                command.Parameters["?Date"].Value = DateTime.Now.Date;

                                command.ExecuteNonQuery();
                                //insert into Archif table
                                query = "INSERT INTO oldsellprice (Last_Price,Price_Type,Sell_Price,Data_ID,Sell_Discount,Price,Normal_Increase,Categorical_Increase,PercentageDelegate,Date) VALUES (?Last_Price,?Price_Type,?Sell_Price,?Data_ID,?Sell_Discount,?Price,?Normal_Increase,?Categorical_Increase,?PercentageDelegate,?Date)";
                                command = new MySqlCommand(query, dbconnection);
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
                    }
                    int sellPrice_ID = 0, oldSellPrice_ID=0;
                    string queryx = "select SellPrice_ID from sellprice order by SellPrice_ID desc limit 1";
                    MySqlCommand com = new MySqlCommand(queryx, dbconnection);
                    if (com.ExecuteScalar() != null)
                    {
                        sellPrice_ID = Convert.ToInt16(com.ExecuteScalar());
                    }
                    //for archive table
                    queryx = "select OldSellPrice_ID from oldsellprice order by OldSellPrice_ID desc limit 1";
                    com = new MySqlCommand(queryx, dbconnection);
                    if (com.ExecuteScalar() != null)
                    {
                        oldSellPrice_ID = Convert.ToInt16(com.ExecuteScalar());
                    }

                    queryx = "delete from additional_increase_sellprice where SellPrice_ID=" + sellPrice_ID;
                    com = new MySqlCommand(queryx, dbconnection);
                    com.ExecuteNonQuery();

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

                        //insert into archive table
                        queryx = "insert into old_additional_increase_sellprice (OldSellPrice_ID,AdditionalValue,Type,Description) values (@OldSellPrice_ID,@AdditionalValue,@Type,@Description)";
                        com = new MySqlCommand(queryx, dbconnection);
                        com.Parameters.AddWithValue("@OldSellPrice_ID", oldSellPrice_ID);
                        com.Parameters.AddWithValue("@Type", item.Cells[1].Value);
                        com.Parameters.AddWithValue("@AdditionalValue", item.Cells[0].Value);
                        com.Parameters.AddWithValue("@Description", item.Cells[2].Value);
                        com.ExecuteNonQuery();

                    }
                    UserControl.ItemRecord("sellprice", "تعديل", sellPrice_ID, DateTime.Now, "", dbconnection);
                }
                else
                {
                    double addationalValue = 0.0;
                    DataGridView DataGridView = (DataGridView)panContent.Controls["panContent"].Controls["dataGridView1"];
                    foreach (DataGridViewRow item1 in DataGridView.Rows)
                    {
                        addationalValue += Convert.ToDouble(item1.Cells[0].Value);
                    }
                    int[] rows = (((GridView)gridControl1.MainView).GetSelectedRows());
                    List<DataRowView> recordList = new List<DataRowView>();
                    for (int i = 0; i < rows.Length; i++)
                    {
                        DataRowView a = (DataRowView)(((GridView)gridControl1.MainView).GetRow(rows[i]));
                        recordList.Add(a);
                    }
                    foreach (DataRowView item in recordList)
                    {
                        String query = "update sellprice set Sell_Price=@Sell_Price where SellPrice_ID=" + item[0].ToString();

                        MySqlCommand command = new MySqlCommand(query, dbconnection);
                        command.Parameters.AddWithValue("@Sell_Price",Convert.ToDouble(item["سعر البيع"])+addationalValue);           
                        command.ExecuteNonQuery();

                        foreach (DataGridViewRow item1 in DataGridView.Rows)
                        {
                            double addational = Convert.ToDouble(item1.Cells[0].Value);
                            string  queryx = "insert into special_increase (SellPrice_ID,Value,Description,Date) values (@SellPrice_ID,@Value,@Description,@Date)";
                            MySqlCommand com = new MySqlCommand(queryx, dbconnection);
                            com.Parameters.AddWithValue("@SellPrice_ID", item[0]);
                            com.Parameters.AddWithValue("@Value", item1.Cells[0].Value);
                            com.Parameters.AddWithValue("@Description", item1.Cells[1].Value);
                            com.Parameters.Add("@Date", MySqlDbType.Date);
                            com.Parameters["@Date"].Value = DateTime.Now.Date;
                            com.ExecuteNonQuery();

                        }
                    }
            
                }
                displayData();
                productsSellPriceForm.displayProducts();

                XtraTabPage xtraTabPage = getTabPage("تعديل اسعار البنود");
                xtraTabPage.ImageOptions.Image = null;

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
                //gridView1.OptionsSelection.MultiSelect = true;
                //if (chBoxSelectAll.Checked)
                //{
                //    gridView1.SelectRows(0, gridView1.RowCount - 1);
                //    txtCode.Text = "";
                //    txtCode.Visible = false;
                //    panCodeParts.Visible = false;
                //    label6.Visible = false;
                //    gridView1.OptionsSelection.MultiSelect = true;
                //    gridView1.OptionsSelection.MultiSelectMode = GridMultiSelectMode.CheckBoxRowSelect;
                //}
                //else
                //{
                //    int selectedRowsCount = gridView1.SelectedRowsCount;
                //    for (int i = 0; i < selectedRowsCount; i++)
                //    {
                //        gridView1.UnselectRow(i);
                //    }
                //    txtCode.Visible = true;
                //    panCodeParts.Visible = true;
                //    label6.Visible = true;
                //    gridView1.OptionsSelection.MultiSelect = false;
                //    gridView1.OptionsSelection.MultiSelectMode = GridMultiSelectMode.RowSelect;
                //}
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
                    XtraTabPage xtraTabPage = getTabPage("تعديل اسعار البنود");
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
                if (gridView1.SelectedRowsCount>0)
                {
                    if (txtDes.Text != "" && txtPlus.Text != "")
                    {
                        if (radioNormal.Checked)
                        {
                            int n = dataGridView1.Rows.Add();
                            dataGridView1.Rows[n].Cells[0].Value = txtPlus.Text;
                            dataGridView1.Rows[n].Cells[1].Value = "عادية";
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
        private void gridView1_SelectionChanged(object sender, DevExpress.Data.SelectionChangedEventArgs e)
        {
            try
            {
                dbconnection.Open();
                if (gridView1.SelectedRowsCount == 1)
                {
                    txtCode.Visible = true;
                    panCodeParts.Visible = true;
                    label6.Visible = true;
                    DataRowView row = (DataRowView)(((GridView)gridControl1.MainView).GetRow(((GridView)gridControl1.MainView).GetSelectedRows()[0]));
                    if (row != null)
                    {
                        txtCode.Text = row[1].ToString();
                        id = Convert.ToInt16(row[0].ToString());
                        String code = txtCode.Text;
                        displayCode(code);
                        setData(row);
                    }
                }
                else if (gridView1.SelectedRowsCount > 1)
                {
                    DataRowView row = (DataRowView)(((GridView)gridControl1.MainView).GetRow(((GridView)gridControl1.MainView).GetSelectedRows()[0]));

                    txtCode.Visible = false;
                    panCodeParts.Visible = false;
                    label6.Visible = false;
                    setData(row);
                }
                else
                {
                    txtCode.Visible = true;
                    panCodeParts.Visible = true;
                    label6.Visible = true;
                    txtCode.Text = "";
                    txtCodePart1.Text = txtCodePart2.Text = txtCodePart3.Text = txtCodePart4.Text = txtCodePart5.Text = "";
                    id = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }

        //function 
        public void displayData()
        {
            MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
            DataTable dt = new DataTable();
            da.Fill(dt);
            gridControl1.DataSource = dt;
        }
        public void setData(DataRowView row1)
        {
           // txtCode.Text = row1["الكود"].ToString();
            txtPrice.Text = row1["السعر"].ToString();
            string str = row1["نوع السعر"].ToString();
            if (str == "لستة")
            {
                txtSell.Text = row1["خصم البيع"].ToString();
                txtNormal.Text = row1["الزيادة العادية"].ToString();
                txtUnNormal.Text = row1["الزيادة القطعية"].ToString();
                radioList.Checked = true;
            }
            else
            {
                txtSell.Text = row1["نسبة البيع"].ToString();
                radioQata3y.Checked = true;
            }
            string query = "select AdditionalValue,Type,Description from additional_increase_sellprice where SellPrice_ID="+ row1[0].ToString(); ;
            MySqlCommand com = new MySqlCommand(query, dbconnection);
            MySqlDataReader dr = com.ExecuteReader();
            while (dr.Read())
            {
                int n = dataGridView1.Rows.Add();
                dataGridView1.Rows[n].Cells[0].Value = dr[0].ToString();
                dataGridView1.Rows[n].Cells[1].Value = dr[1].ToString();
                dataGridView1.Rows[n].Cells[2].Value = dr[2].ToString();

            }
            dr.Close();
            calSellPrice();

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
            foreach (DataGridViewRow item in dataGridView1.Rows)
            {
                addational += Convert.ToDouble(item.Cells[0].Value);
            }
            double price = double.Parse(txtPrice.Text);
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

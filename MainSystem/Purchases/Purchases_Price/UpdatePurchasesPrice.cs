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
    public partial class UpdatePurchasesPrice : Form
    {
        MySqlConnection dbconnection;
        ProductsPurchasesPriceForm productsPurchasesPriceForm = null;
        XtraTabControl xtraTabControlSalesContent = null;
        List<DataRowView> rows = null;
        customPanel customPanel;
        bool load = false;
        int id = 0;
        String query = "", updateType= "NormalCase";

        double Price = -1, ProfitRatio = -1, Purchase_Discount = -1, Normal_Increase = -1, Categorical_Increase = -1;

        public UpdatePurchasesPrice(List<DataRowView> rows, ProductsPurchasesPriceForm productsPurchasesPriceForm, string query, XtraTabControl xtraTabControlSalesContent)
        {
            try
            {
                InitializeComponent();
                this.xtraTabControlSalesContent = xtraTabControlSalesContent;
                this.rows = rows;
                this.productsPurchasesPriceForm = productsPurchasesPriceForm;
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
        private void UpdatePurchasesPrice_Load(object sender, EventArgs e)
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
                query = "SELECT purchasing_price.PurchasingPrice_ID,purchasing_price.Data_ID, data.Code as 'الكود',concat( product.Product_Name,' ',type.Type_Name,' ',factory.Factory_Name,' ',groupo.Group_Name,' ' ,color.Color_Name,' ' ,size.Size_Value )as 'البند',sort.Sort_Value as 'الفرز',data.Classification as 'التصنيف',data.Description as 'الوصف',purchasing_price.Price as 'السعر',purchasing_price.Price_Type as 'نوع السعر',purchasing_price.Purchasing_Discount as 'خصم الشراء',purchasing_price.Normal_Increase as 'الزيادة العادية',purchasing_price.Categorical_Increase as 'الزيادة القطعية',purchasing_price.ProfitRatio as 'نسبة الاضافة',purchasing_price.Purchasing_Price as 'سعر الشراء' from data INNER JOIN purchasing_price on purchasing_price.Data_ID=data.Data_ID  INNER JOIN type ON type.Type_ID = data.Type_ID INNER JOIN product ON product.Product_ID = data.Product_ID INNER JOIN factory ON data.Factory_ID = factory.Factory_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID LEFT outer JOIN color ON data.Color_ID = color.Color_ID LEFT outer  JOIN size ON data.Size_ID = size.Size_ID LEFT outer  JOIN sort ON data.Sort_ID = sort.Sort_ID where   purchasing_price.PurchasingPrice_ID in(" + ids + ")";

                MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                DataTable dt = new DataTable();
                da.Fill(dt);

                gridControl1.DataSource = dt;
                gridView1.BestFitColumns();
                gridView1.Columns[0].Visible = false;
                gridView1.Columns[1].Visible = false;
                //gridView1.Columns[1].Width = 140;
                //gridView1.Columns[2].Width = 200;

                setData((DataRowView)gridView1.GetRow(0));
                gridView1.SelectAll();
                checkBoxPurchasesDiscount.Checked = true;
                checkBoxPrice.Checked = true;
                checkBoxNormalIncrease.Checked = true;
                checkBoxUnNormalIncrease.Checked = true;
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
                label14.Text = "خصم الشراء";
                txtNormal.Visible = true;
                txtUnNormal.Visible = true;
                label15.Visible = true;
                label16.Visible = true;
                checkBoxNormalIncrease.Visible = true;
                checkBoxUnNormalIncrease.Visible = true;
             
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
                checkBoxNormalIncrease.Visible = false;
                checkBoxUnNormalIncrease.Visible = false;
          
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
                    labPurchasesPrice.Visible = false;
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
                    labPurchasesPrice.Visible = true;
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
                    string code = txtCode.Text;
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
                    chackUpdateType();
                    if (updateType == "NormalCase")
                    {
                        #region 
                        double price = double.Parse(txtPrice.Text);
                        double PurchasesPercent = double.Parse(txtPurchases.Text);

                        if (radioQata3y.Checked == true)
                        {
                            #region set qata3yPrice for list item

                            double NormalPercent = double.Parse(txtNormal.Text);
                            double UnNormalPercent = double.Parse(txtUnNormal.Text);

                            DataTable dataTable = (DataTable)gridControl1.DataSource;
                            for (int i = 0; i < dataTable.Rows.Count; i++)
                            {
                                additionalIncreasePurchasesPrice(Convert.ToInt16(dataTable.Rows[i][0].ToString()));

                                String query = "update purchasing_price set Purchasing_Discount=@Purchasing_Discount,Normal_Increase=@Normal_Increase,Categorical_Increase=@Categorical_Increase,Price_Type=@Price_Type,Purchasing_Price=@Purchasing_Price,ProfitRatio=@ProfitRatio,Price=@Price where PurchasingPrice_ID=" + dataTable.Rows[i][0].ToString();

                                MySqlCommand command = new MySqlCommand(query, dbconnection);
                                command.Parameters.AddWithValue("@Price_Type", "قطعى");
                                command.Parameters.AddWithValue("@Purchasing_Price", price + (price * PurchasesPercent / 100.0));
                                command.Parameters.AddWithValue("@ProfitRatio", PurchasesPercent);
                                command.Parameters.AddWithValue("@Purchasing_Discount", 0.00);
                                command.Parameters.AddWithValue("@Price", price);
                                command.Parameters.AddWithValue("@Normal_Increase", 0.00);
                                command.Parameters.AddWithValue("@Categorical_Increase", 0.00);
                                command.ExecuteNonQuery();
                             
                                //insert into Archif table
                                query = "INSERT INTO oldpurchasing_price (Price_Type,Data_ID,Purchasing_Discount,Price,Normal_Increase,Categorical_Increase,Date,ProfitRatio) VALUES (?Price_Type,?Data_ID,?Purchasing_Discount,?Price,?Normal_Increase,?Categorical_Increase,?Date,@ProfitRatio)";
                                command = new MySqlCommand(query, dbconnection);
                                command.Parameters.AddWithValue("@Price_Type", "قطعى");
                                command.Parameters.AddWithValue("@Data_ID", Convert.ToInt16(dataTable.Rows[i][1].ToString()));
                                command.Parameters.AddWithValue("@ProfitRatio", PurchasesPercent);
                                command.Parameters.AddWithValue("@Purchasing_Discount", 0.00);
                                command.Parameters.AddWithValue("@Price", Price);
                                command.Parameters.AddWithValue("@Normal_Increase", 0.00);
                                command.Parameters.AddWithValue("@Categorical_Increase", 0.00);
                                command.Parameters.Add("?Date", MySqlDbType.Date);
                                command.Parameters["?Date"].Value = DateTime.Now.Date;
                                command.ExecuteNonQuery();
                                additionalIncreaseOldPurchasesPrice();



                            }

                            #endregion
                        }
                        else
                        {
                            #region set priceList for collection of items

                            double NormalPercent = double.Parse(txtNormal.Text);
                            double unNormalPercent = double.Parse(txtUnNormal.Text);

                            double PurchasesPrice = (price + NormalPercent) - ((price + NormalPercent) * PurchasesPercent / 100.0);

                            PurchasesPrice = PurchasesPrice + unNormalPercent;

                            DataTable dataTable = (DataTable)gridControl1.DataSource;
                            for (int i = 0; i < dataTable.Rows.Count; i++)
                            {
                                additionalIncreasePurchasesPrice(Convert.ToInt16(dataTable.Rows[i][0].ToString()));

                                string query = "update purchasing_price set ProfitRatio=@ProfitRatio, Price_Type=@Price_Type,Purchasing_Price=@Purchasing_Price,Purchasing_Discount=@Purchasing_Discount,Price=@Price,Normal_Increase=@Normal_Increase,Categorical_Increase=@Categorical_Increase where PurchasingPrice_ID =" + dataTable.Rows[i][0].ToString();

                                MySqlCommand command = new MySqlCommand(query, dbconnection);
                                command.Parameters.AddWithValue("@Price_Type", "لستة");
                                command.Parameters.AddWithValue("@Purchasing_Price", PurchasesPrice);
                                command.Parameters.AddWithValue("@ProfitRatio", 0.00);
                                command.Parameters.AddWithValue("@Purchasing_Discount", double.Parse(txtPurchases.Text));
                                command.Parameters.AddWithValue("@Price", price);
                                command.Parameters.AddWithValue("@Normal_Increase", double.Parse(txtNormal.Text));
                                command.Parameters.AddWithValue("@Categorical_Increase", double.Parse(txtUnNormal.Text));
                                command.ExecuteNonQuery();
                                
                                //insert into Archif table
                                query = "INSERT INTO oldpurchasing_price (Price_Type,Data_ID,Purchasing_Discount,Price,Normal_Increase,Categorical_Increase,Date,ProfitRatio) VALUES (?Price_Type,?Data_ID,?Purchasing_Discount,?Price,?Normal_Increase,?Categorical_Increase,?Date,@ProfitRatio)";
                                command = new MySqlCommand(query, dbconnection);
                                command.Parameters.AddWithValue("@Price_Type", "لستة");
                                command.Parameters.AddWithValue("@Purchasing_Price", PurchasesPrice);
                                command.Parameters.AddWithValue("@Data_ID", Convert.ToInt16(dataTable.Rows[i][1].ToString()));
                                command.Parameters.AddWithValue("@ProfitRatio", 0.00);
                                command.Parameters.AddWithValue("@Purchasing_Discount", double.Parse(txtPurchases.Text));
                                command.Parameters.AddWithValue("@Price", Price);
                                command.Parameters.AddWithValue("@Normal_Increase", double.Parse(txtNormal.Text));
                                command.Parameters.AddWithValue("@Categorical_Increase", double.Parse(txtUnNormal.Text));
                                command.Parameters.Add("?Date", MySqlDbType.Date);
                                command.Parameters["?Date"].Value = DateTime.Now.Date;
                                command.ExecuteNonQuery();
                                additionalIncreaseOldPurchasesPrice();
                            }

                            #endregion
                        } 
                        #endregion

                    }
                    else
                    {
                        #region unIdeal case of update
                        resetValues();
                        setValues();
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
                                    additionalIncreasePurchasesPrice(Convert.ToInt16(dataTable.Rows[i][0].ToString()));

                                    String query = "update purchasing_price set Purchasing_Discount=@Purchasing_Discount,Normal_Increase=@Normal_Increase,Categorical_Increase=@Categorical_Increase,Price_Type=@Price_Type,Purchasing_Price=@Purchasing_Price,ProfitRatio=@ProfitRatio,Price=@Price,Date=@Date where PurchasingPrice_ID=" + dataTable.Rows[i][0].ToString();

                                    MySqlCommand command = new MySqlCommand(query, dbconnection);
                                    command.Parameters.AddWithValue("@Price_Type", "قطعى");
                                    if (Price != -1 && ProfitRatio != -1)
                                    {
                                        command.Parameters.AddWithValue("@Purchasing_Price", Price + (Price * ProfitRatio / 100.0));
                                        command.Parameters.AddWithValue("@ProfitRatio", ProfitRatio);
                                        command.Parameters.AddWithValue("@Price", Price);
                                    }
                                    else if (Price == -1 && ProfitRatio != -1)
                                    {
                                        double xPrice = Convert.ToDouble(dataTable.Rows[i][7]);
                                        command.Parameters.AddWithValue("@Purchasing_Price", xPrice + (xPrice * ProfitRatio / 100.0));
                                        command.Parameters.AddWithValue("@ProfitRatio", ProfitRatio);
                                        command.Parameters.AddWithValue("@Price", xPrice);
                                    }
                                    else if (Price != -1 && ProfitRatio == -1)
                                    {
                                        double xProfitRatio = Convert.ToDouble(dataTable.Rows[i][12]);
                                        command.Parameters.AddWithValue("@Purchasing_Price", Price + (Price * xProfitRatio / 100.0));
                                        command.Parameters.AddWithValue("@ProfitRatio", xProfitRatio);
                                        command.Parameters.AddWithValue("@Price", Price);
                                    }
                                    else if (Price == -1 && ProfitRatio == -1)
                                    {
                                        double xPrice = Convert.ToDouble(dataTable.Rows[i][7]);
                                        double xProfitRatio = Convert.ToDouble(dataTable.Rows[i][12]);
                                        command.Parameters.AddWithValue("@Purchasing_Price", xPrice + (xPrice * xProfitRatio / 100.0));
                                        command.Parameters.AddWithValue("@ProfitRatio", xProfitRatio);
                                        command.Parameters.AddWithValue("@Price", xPrice);
                                    }
                                    command.Parameters.AddWithValue("@Purchasing_Discount", 0.00);
                                    command.Parameters.AddWithValue("@Normal_Increase", 0.00);
                                    command.Parameters.AddWithValue("@Categorical_Increase", 0.00);
                               
                                    command.Parameters.Add("?Date", MySqlDbType.Date);
                                    command.Parameters["?Date"].Value = DateTime.Now.Date;
                                    command.ExecuteNonQuery();
                                    
                                    UserControl.ItemRecord("sellprice", "تعديل", Convert.ToInt16(dataTable.Rows[i][0].ToString()), DateTime.Now, "", dbconnection);

                                    //insert into Archif Table
                                    query = "INSERT INTO oldpurchasing_price (Purchasing_Discount,Price_Type, Purchasing_Price, ProfitRatio, Data_ID, Price,Last_Price,Date,Normal_Increase,Categorical_Increase) VALUES(@Purchasing_Discount,@Price_Type,@Purchasing_Price,@ProfitRatio,@Data_ID,@Price,@Last_Price,@Date,@Normal_Increase,@Categorical_Increase)";
                                    command = new MySqlCommand(query, dbconnection);
                                    command.Parameters.AddWithValue("@Price_Type", "قطعى");
                                    command.Parameters.AddWithValue("@Data_ID", dataTable.Rows[i][1]);
                                    if (Price != -1 && ProfitRatio != -1)
                                    {
                                        command.Parameters.AddWithValue("@Purchasing_Price", Price + (Price * ProfitRatio / 100.0));
                                        command.Parameters.AddWithValue("@Last_Price", Price + (Price * ProfitRatio / 100.0));
                                        command.Parameters.AddWithValue("@ProfitRatio", ProfitRatio);
                                        command.Parameters.AddWithValue("@Price", Price);
                                    }
                                    else if (Price == -1 && ProfitRatio != -1)
                                    {
                                        double xPrice = Convert.ToDouble(dataTable.Rows[i][7]);
                                        command.Parameters.AddWithValue("@Purchasing_Price", xPrice + (xPrice * ProfitRatio / 100.0));
                                        command.Parameters.AddWithValue("@Last_Price", Price + (Price * ProfitRatio / 100.0));
                                        command.Parameters.AddWithValue("@ProfitRatio", ProfitRatio);
                                        command.Parameters.AddWithValue("@Price", xPrice);
                                    }
                                    else if (Price != -1 && ProfitRatio == -1)
                                    {
                                        double xProfitRatio = Convert.ToDouble(dataTable.Rows[i][12]);
                                        command.Parameters.AddWithValue("@Purchasing_Price", Price + (Price * xProfitRatio / 100.0));
                                        command.Parameters.AddWithValue("@Last_Price", Price + (Price * ProfitRatio / 100.0));
                                        command.Parameters.AddWithValue("@ProfitRatio", xProfitRatio);
                                        command.Parameters.AddWithValue("@Price", Price);
                                    }
                                    else if (Price == -1 && ProfitRatio == -1)
                                    {
                                        double xPrice = Convert.ToDouble(dataTable.Rows[i][7]);
                                        double xProfitRatio = Convert.ToDouble(dataTable.Rows[i][12]);
                                        command.Parameters.AddWithValue("@Purchasing_Price", xPrice + (xPrice * xProfitRatio / 100.0));
                                        command.Parameters.AddWithValue("@Last_Price", Price + (Price * ProfitRatio / 100.0));
                                        command.Parameters.AddWithValue("@ProfitRatio", xProfitRatio);
                                        command.Parameters.AddWithValue("@Price", xPrice);
                                    }
                                    command.Parameters.AddWithValue("@Purchasing_Discount", 0.00);
                                    command.Parameters.AddWithValue("@Normal_Increase", 0.00);
                                    command.Parameters.AddWithValue("@Categorical_Increase", 0.00);
                                 
                                    command.Parameters.Add("?Date", MySqlDbType.Date);
                                    command.Parameters["?Date"].Value = DateTime.Now.Date;

                                    command.ExecuteNonQuery();
                                    additionalIncreaseOldPurchasesPrice();
                                }
                            }

                            #endregion
                        }
                        else
                        {
                            #region set priceList for collection of items

                            DataTable dataTable = (DataTable)gridControl1.DataSource;
                            for (int i = 0; i < dataTable.Rows.Count; i++)
                            {
                                if (gridView1.IsRowSelected(i))
                                {
                                    additionalIncreasePurchasesPrice(Convert.ToInt16(dataTable.Rows[i][0].ToString()));

                                    string query = "update purchasing_price set ProfitRatio=@ProfitRatio, Price_Type=@Price_Type," +/*Purchasing_Price=@Purchasing_Price*/"Purchasing_Discount=@Purchasing_Discount,Price=@Price,Normal_Increase=@Normal_Increase,Categorical_Increase=@Categorical_Increase,Date=@Date where PurchasingPrice_ID =" + dataTable.Rows[i][0].ToString();

                                    MySqlCommand command = new MySqlCommand(query, dbconnection);
                                    command.Parameters.AddWithValue("@Price_Type", "لستة");
                                    command.Parameters.AddWithValue("@ProfitRatio", 0.00);
                                    if (Purchase_Discount != -1)
                                    {
                                        command.Parameters.AddWithValue("@Purchasing_Discount", Purchase_Discount);
                                    }
                                    else
                                    {
                                        command.Parameters.AddWithValue("@Purchasing_Discount", Convert.ToDouble(dataTable.Rows[i]["خصم الشراء"]));
                                    }
                                    if (Price != -1)
                                        command.Parameters.AddWithValue("@Price", Price);
                                    else
                                        command.Parameters.AddWithValue("@Price", Convert.ToDouble(dataTable.Rows[i]["السعر"]));
                                    
                                    if (Normal_Increase != -1)
                                        command.Parameters.AddWithValue("@Normal_Increase", Normal_Increase);
                                    else
                                        command.Parameters.AddWithValue("@Normal_Increase", Convert.ToDouble(dataTable.Rows[i]["الزيادة العادية"]));
                                    if (Categorical_Increase != -1)
                                        command.Parameters.AddWithValue("@Categorical_Increase", Categorical_Increase);
                                    else
                                        command.Parameters.AddWithValue("@Categorical_Increase", Convert.ToDouble(dataTable.Rows[i]["الزيادة القطعية"]));

                                 
                                    command.Parameters.Add("?Date", MySqlDbType.Date);
                                    command.Parameters["?Date"].Value = DateTime.Now.Date;
                                    command.ExecuteNonQuery();

                                  
                                    UserControl.ItemRecord("purchasing_price", "تعديل", Convert.ToInt16(dataTable.Rows[i][0].ToString()), DateTime.Now, "", dbconnection);

                                    //insert into Archif table
                                    query = "INSERT INTO oldpurchasing_price (Price_Type,Data_ID,Purchasing_Discount,Price,Normal_Increase,Categorical_Increase,Date,ProfitRatio) VALUES (?Price_Type,?Data_ID,?Purchasing_Discount,?Price,?Normal_Increase,?Categorical_Increase,?Date,@ProfitRatio)";
                                    command = new MySqlCommand(query, dbconnection);
                                    command.Parameters.AddWithValue("@Price_Type", "لستة");
                                    command.Parameters.AddWithValue("@Data_ID",Convert.ToInt16(dataTable.Rows[i][1].ToString()));

                                    command.Parameters.AddWithValue("@ProfitRatio", 0.00);
                                    if (Purchase_Discount != -1)
                                    {
                                        command.Parameters.AddWithValue("@Purchasing_Discount", Purchase_Discount);
                                    }
                                    else
                                    {
                                        command.Parameters.AddWithValue("@Purchasing_Discount", Convert.ToDouble(dataTable.Rows[i]["خصم الشراء"]));
                                    }
                                    if (Price != -1)
                                        command.Parameters.AddWithValue("@Price", Price);
                                    else
                                        command.Parameters.AddWithValue("@Price", Convert.ToDouble(dataTable.Rows[i]["السعر"]));

                                    if (Normal_Increase != -1)
                                        command.Parameters.AddWithValue("@Normal_Increase", Normal_Increase);
                                    else
                                        command.Parameters.AddWithValue("@Normal_Increase", Convert.ToDouble(dataTable.Rows[i]["الزيادة العادية"]));
                                    if (Categorical_Increase != -1)
                                        command.Parameters.AddWithValue("@Categorical_Increase", Categorical_Increase);
                                    else
                                        command.Parameters.AddWithValue("@Categorical_Increase", Convert.ToDouble(dataTable.Rows[i]["الزيادة القطعية"]));

                             
                                    command.Parameters.Add("?Date", MySqlDbType.Date);
                                    command.Parameters["?Date"].Value = DateTime.Now.Date;
                                    command.ExecuteNonQuery();
                                    additionalIncreaseOldPurchasesPrice();
                                }
                            }

                            #endregion
                        }
                        #endregion
                    }
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
                        String query = "update purchasing_price set Purchasing_Price=@Purchasing_Price where PurchasingPrice_ID=" + item[0].ToString();

                        MySqlCommand command = new MySqlCommand(query, dbconnection);
                        command.Parameters.AddWithValue("@Purchasing_Price", Convert.ToDouble(item["سعر الشراء"]) + addationalValue);
                        command.ExecuteNonQuery();

                        foreach (DataGridViewRow item1 in DataGridView.Rows)
                        {
                            double addational = Convert.ToDouble(item1.Cells[0].Value);
                            string queryx = "insert into special_increase_purchasing (PurchasingPrice_ID,Value,Description,Date) values (@PurchasingPrice_ID,@Value,@Description,@Date)";
                            MySqlCommand com = new MySqlCommand(queryx, dbconnection);
                            com.Parameters.AddWithValue("@PurchasingPrice_ID", item[0]);
                            com.Parameters.AddWithValue("@Value", item1.Cells[0].Value);
                            com.Parameters.AddWithValue("@Description", item1.Cells[1].Value);
                            com.Parameters.Add("@Date", MySqlDbType.Date);
                            com.Parameters["@Date"].Value = DateTime.Now.Date;
                            com.ExecuteNonQuery();
                        }
                    }
                }
                displayData();
              //  productsPurchasesPriceForm.displayProducts();
                MessageBox.Show("تم");
                gridView1.SelectAll();
                //XtraTabPage xtraTabPage = getTabPage("تعديل اسعار البنود");
                //xtraTabPage.ImageOptions.Image = null;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }
        private void gridView1_SelectionChanged(object sender, DevExpress.Data.SelectionChangedEventArgs e)
        {
            try
            {
                dbconnection.Close();
                dbconnection.Open();
                if (gridView1.SelectedRowsCount == 1)
                {
                    txtCode.Visible = true;
                    panCodeParts.Visible = true;
                    label6.Visible = true;
                    DataRowView row = (DataRowView)(((GridView)gridControl1.MainView).GetRow(((GridView)gridControl1.MainView).GetSelectedRows()[0]));
                    if (row != null)
                    {
                        txtCode.Text = row[2].ToString();
                        id = Convert.ToInt16(row[0].ToString());
                        String code = txtCode.Text;
                        displayCode(code);
                        setData(row);
                        label19.Visible = true;
                        labPurchasesPrice.Visible = true;
                    }
                }
                else if (gridView1.SelectedRowsCount > 1)
                {
                    DataRowView row = (DataRowView)(((GridView)gridControl1.MainView).GetRow(((GridView)gridControl1.MainView).GetSelectedRows()[0]));

                    txtCode.Visible = false;
                    panCodeParts.Visible = false;
                    label6.Visible = false;
                    setData(row);
                    label19.Visible = false;
                    labPurchasesPrice.Text = "";
                    labPurchasesPrice.Visible = false;
                }
                else
                {
                    txtCode.Visible = true;
                    panCodeParts.Visible = true;
                    label6.Visible = true;
                    txtCode.Text = "";
                    txtCodePart1.Text = txtCodePart2.Text = txtCodePart3.Text = txtCodePart4.Text = txtCodePart5.Text = "";
                    id = 0;
                    label19.Visible = true;
                    labPurchasesPrice.Text = "";
                    labPurchasesPrice.Visible = true;
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
                    XtraTabPage xtraTabPage = getTabPage("تعديل اسعار البنود");
                    if (!IsClear())
                        xtraTabPage.ImageOptions.Image = Properties.Resources.unsave__2_;
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
                            dataGridView1.Rows[n].Cells[1].Value = "عادية";
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
                    labPurchasesPrice.Text = calPurchasesPrice()+"";
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
        public void displayData()
        {
            MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
            DataTable dt = new DataTable();
            da.Fill(dt);
            gridControl1.DataSource = dt;
            gridView1.BestFitColumns();
        }
        public void setData(DataRowView row1)
        {
            txtCode.Text = row1["الكود"].ToString();
            txtPrice.Text = row1["السعر"].ToString();
            string str = row1["نوع السعر"].ToString();
            if (str == "لستة")
            {
                txtPurchases.Text = row1["خصم الشراء"].ToString();
                txtNormal.Text = row1["الزيادة العادية"].ToString();
                txtUnNormal.Text = row1["الزيادة القطعية"].ToString();
                radioList.Checked = true;
            }
            else
            {
                txtPurchases.Text = row1["نسبة الاضافة"].ToString();
                radioQata3y.Checked = true;
            }
            dataGridView1.Rows.Clear();
            string query = "select AdditionalValue,Type,Description from additional_increase_purchasingprice where PurchasingPrice_ID=" + row1[0].ToString(); ;
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
            displayCode(txtCode.Text);
            labPurchasesPrice.Text= calPurchasesPrice().ToString();

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
            foreach (DataGridViewRow item in dataGridView1.Rows)
            {
                addational += Convert.ToDouble(item.Cells[0].Value);
            }
            double price = double.Parse(txtPrice.Text);
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
        //for Purshase Price
        public void chackUpdateType()
        {
            if (!checkBoxPurchasesDiscount.Checked || !checkBoxPrice.Checked || !checkBoxNormalIncrease.Checked || !checkBoxUnNormalIncrease.Checked)
            {
                updateType = "UnNormalCase";
            }
        }
        public void resetValues()
        {
            Price = -1;
            ProfitRatio = -1;
            Purchase_Discount = -1;
            Normal_Increase = -1;
            Categorical_Increase = -1;
        }
        public void setValues()
        {
      
            if (checkBoxPurchasesDiscount.Checked)
            {
                try
                {
                    txtPurchases.BackColor = Color.White;
                    if (radioList.Checked)
                        Purchase_Discount = Convert.ToDouble(txtPurchases.Text);
                    else
                        ProfitRatio = Convert.ToDouble(txtPurchases.Text);
                }
                catch
                {
                    txtPurchases.Focus();
                    txtPurchases.BackColor = Color.RosyBrown;
                }
            }
            else
            {
                Purchase_Discount = -1;
                ProfitRatio = -1;
            }
            if (checkBoxPrice.Checked)
            {
                try
                {
                    txtPrice.BackColor = Color.White;
                    Price = Convert.ToDouble(txtPrice.Text);
                }
                catch
                {
                    txtPrice.Focus();
                    txtPrice.BackColor = Color.RosyBrown;
                }
            }
            else
            {
                Price = -1;
            }
            if (checkBoxNormalIncrease.Checked)
            {
                try
                {
                    txtNormal.BackColor = Color.White;
                    Normal_Increase = Convert.ToDouble(txtNormal.Text);
                }
                catch
                {
                    txtNormal.Focus();
                    txtNormal.BackColor = Color.RosyBrown;
                }
            }
            else
            {
                Normal_Increase = -1;
            }
            if (checkBoxUnNormalIncrease.Checked)
            {
                try
                {
                    txtUnNormal.BackColor = Color.White;
                    Categorical_Increase = Convert.ToDouble(txtUnNormal.Text);

                }
                catch
                {
                    txtUnNormal.Focus();
                    txtUnNormal.BackColor = Color.RosyBrown;
                }
            }
            else
            {
                Categorical_Increase = -1;
            }
        }
        public void additionalIncreasePurchasesPrice(int purchasePrice_ID)
        {
            if (chBoxAdditionalIncrease.Checked)
            {
                string queryx = "delete from additional_increase_purchasingprice where PurchasingPrice_ID=" + purchasePrice_ID;
                MySqlCommand com = new MySqlCommand(queryx, dbconnection);
                com.ExecuteNonQuery();

                foreach (DataGridViewRow item in dataGridView1.Rows)
                {
                    #region addational increase
                    double addational = Convert.ToDouble(item.Cells[0].Value);
                    queryx = "insert into additional_increase_purchasingprice (PurchasingPrice_ID,AdditionalValue,Type,Description) values (@PurchasingPrice_ID,@AdditionalValue,@Type,@Description)";
                    com = new MySqlCommand(queryx, dbconnection);
                    com.Parameters.AddWithValue("@PurchasingPrice_ID", purchasePrice_ID);
                    com.Parameters.AddWithValue("@Type", item.Cells[1].Value);
                    com.Parameters.AddWithValue("@AdditionalValue", item.Cells[0].Value);
                    com.Parameters.AddWithValue("@Description", item.Cells[2].Value);
                    com.ExecuteNonQuery();

                    #endregion
                }
            }
        }
        public void additionalIncreaseOldPurchasesPrice()
        {
            if (chBoxAdditionalIncrease.Checked)
            {
                int oldPurchasesPrice_ID = 0;
                string queryx = "select OldPurchasingPrice_ID from oldpurchasing_price order by OldPurchasingPrice_ID desc limit 1";
                MySqlCommand com = new MySqlCommand(queryx, dbconnection);
                if (com.ExecuteScalar() != null)
                {
                    oldPurchasesPrice_ID = Convert.ToInt16(com.ExecuteScalar());
                }

                foreach (DataGridViewRow item in dataGridView1.Rows)
                {
                    #region addational increase
                    double addational = Convert.ToDouble(item.Cells[0].Value);

                    //insert into archive table
                    queryx = "insert into old_additional_increase_purchasingprice (OldPurchasingPrice_ID,AdditionalValue,Type,Description) values (@OldPurchasingPrice_ID,@AdditionalValue,@Type,@Description)";
                    com = new MySqlCommand(queryx, dbconnection);
                    com.Parameters.AddWithValue("@OldPurchasingPrice_ID", oldPurchasesPrice_ID);
                    com.Parameters.AddWithValue("@Type", item.Cells[1].Value);
                    com.Parameters.AddWithValue("@AdditionalValue", item.Cells[0].Value);
                    com.Parameters.AddWithValue("@Description", item.Cells[2].Value);
                    com.ExecuteNonQuery();
                    #endregion
                }
            }
        }
    }
}

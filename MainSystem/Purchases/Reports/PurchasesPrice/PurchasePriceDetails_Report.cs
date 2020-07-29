﻿using DevExpress.XtraGrid;
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

namespace TaxesSystem
{
    public partial class PurchasePriceDetails_Report : Form
    {
        MySqlConnection dbconnection, dbconnection2;
        MainForm bankMainForm = null;
        bool factoryFlage = false;
        bool groupFlage = false;
        bool flagProduct = false;

        public static XtraTabPage MainTabPagePrintingTransitions;
        Panel panelPrintingTransitions;

        public static BillsTransitions_Print bankPrint;

        public static GridControl gridcontrol;
        bool loadedBranch = false;

        public PurchasePriceDetails_Report(MainForm BankMainForm, XtraTabControl xtraTabControlPurchases)
        {
            InitializeComponent();
            dbconnection = new MySqlConnection(connection.connectionString);
            dbconnection2 = new MySqlConnection(connection.connectionString);
            bankMainForm = BankMainForm;

            MainTabPagePrintingTransitions = new XtraTabPage();
            panelPrintingTransitions = new Panel();

            gridcontrol = gridControl1;

            comStore.AutoCompleteMode = AutoCompleteMode.Suggest;
            comStore.AutoCompleteSource = AutoCompleteSource.ListItems;

            //this.dateTimePicker1.Format = DateTimePickerFormat.Short;
            //this.dateTimePicker2.Format = DateTimePickerFormat.Short;
        }

        private void Bills_Transitions_Report_Load(object sender, EventArgs e)
        {
            try
            {
                if (!loadedBranch)
                {
                    loadBranch();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }
        private void comBox_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (loadedBranch)
                {
                    ComboBox comBox = (ComboBox)sender;

                    switch (comBox.Name)
                    {
                        case "comType":
                            if (loadedBranch)
                            {
                                txtType.Text = comType.SelectedValue.ToString();
                                factoryFlage = false;
                                string query = "select * from factory inner join type_factory on factory.Factory_ID=type_factory.Factory_ID inner join type on type_factory.Type_ID=type.Type_ID where type_factory.Type_ID=" + comType.SelectedValue.ToString();
                                MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                                DataTable dt = new DataTable();
                                da.Fill(dt);
                                comFactory.DataSource = dt;
                                comFactory.DisplayMember = dt.Columns["Factory_Name"].ToString();
                                comFactory.ValueMember = dt.Columns["Factory_ID"].ToString();
                                factoryFlage = true;
                                comFactory.Text = "";
                                txtFacory.Text = "";
                                dbconnection.Close();
                                dbconnection.Open();
                                query = "select TypeCoding_Method from type where Type_ID=" + comType.SelectedValue.ToString();
                                MySqlCommand com = new MySqlCommand(query, dbconnection);
                                int TypeCoding_Method = (int)com.ExecuteScalar();
                                dbconnection.Close();
                                if (TypeCoding_Method == 1)
                                {
                                    string query2 = "";
                                    if (comType.SelectedValue.ToString() == "2" || comType.SelectedValue.ToString() == "1")
                                    {
                                        query2 = "select * from groupo where Factory_ID=-1";
                                    }
                                    else
                                    {
                                        query2 = "select * from groupo where Factory_ID=" + -Convert.ToInt32(comType.SelectedValue.ToString()) + " and Type_ID=" + comType.SelectedValue.ToString();
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
                                
                                comFactory.Focus();
                            }
                            break;
                        case "comFactory":
                            if (factoryFlage)
                            {
                                txtFacory.Text = comFactory.SelectedValue.ToString();
                                dbconnection.Close();
                                dbconnection.Open();
                                string query = "select TypeCoding_Method from type where Type_ID=" + comType.SelectedValue.ToString();
                                MySqlCommand com = new MySqlCommand(query, dbconnection);
                                int TypeCoding_Method = (int)com.ExecuteScalar();
                                dbconnection.Close();
                                if (TypeCoding_Method == 2)
                                {
                                    string query2f = "select * from groupo where Type_ID=" + comType.SelectedValue.ToString() + " and Factory_ID=" + comFactory.SelectedValue.ToString();
                                    MySqlDataAdapter da2f = new MySqlDataAdapter(query2f, dbconnection);
                                    DataTable dt2f = new DataTable();
                                    da2f.Fill(dt2f);
                                    comGroup.DataSource = dt2f;
                                    comGroup.DisplayMember = dt2f.Columns["Group_Name"].ToString();
                                    comGroup.ValueMember = dt2f.Columns["Group_ID"].ToString();
                                    comGroup.Text = "";
                                    txtGroup.Text = "";
                                }
                                else
                                {
                                    filterProduct();
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
                                txtSize.Text = "";
                                comGroup.Focus();
                            }
                            break;
                        case "comGroup":
                            if (groupFlage)
                            {
                                txtGroup.Text = comGroup.SelectedValue.ToString();
                                string supQuery = "", subQuery1 = "";
                                if (comType.SelectedValue != null)
                                {
                                    supQuery += " and product.Type_ID=" + comType.SelectedValue.ToString();
                                }
                                if (comFactory.SelectedValue != null)
                                {
                                    supQuery += " and product_factory_group.Factory_ID=" + comFactory.SelectedValue.ToString();
                                    subQuery1 += " and Factory_ID=" + comFactory.SelectedValue.ToString();
                                }

                                string query3 = "select distinct  product.Product_ID  ,Product_Name  from product inner join product_factory_group on product.Product_ID=product_factory_group.Product_ID  where product_factory_group.Group_ID=" + comGroup.SelectedValue.ToString() + supQuery + "  order by product.Product_ID";
                                MySqlDataAdapter da3 = new MySqlDataAdapter(query3, dbconnection);
                                DataTable dt3 = new DataTable();
                                da3.Fill(dt3);
                                flagProduct = false;
                                comProduct.DataSource = dt3;
                                comProduct.DisplayMember = dt3.Columns["Product_Name"].ToString();
                                comProduct.ValueMember = dt3.Columns["Product_ID"].ToString();
                                
                                comProduct.Text = "";
                                txtProduct.Text = "";
                                flagProduct = true;
                                string query2 = "select * from size where Group_ID=" + comGroup.SelectedValue.ToString() + subQuery1;
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
                                txtProduct.Text = comProduct.SelectedValue.ToString();
                            }
                            break;
                            
                        case "comSize":
                            txtSize.Text = comSize.SelectedValue.ToString();
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
                            case "txtType":
                                query = "select Type_Name from type where Type_ID='" + txtType.Text + "'";
                                com = new MySqlCommand(query, dbconnection);
                                if (com.ExecuteScalar() != null)
                                {
                                    Name = (string)com.ExecuteScalar();
                                    comType.Text = Name;
                                    txtFacory.Focus();
                                }
                                else
                                {
                                    MessageBox.Show("there is no item with this id");
                                    dbconnection.Close();
                                    return;
                                }
                                break;
                            case "txtFacory":
                                query = "select Factory_Name from factory where Factory_ID='" + txtFacory.Text + "'";
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
                                    txtSize.Focus();
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

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (comFactory.Text != "" && comFactory.SelectedValue != null)
                {
                    search();
                }
                else
                {
                    MessageBox.Show("يجب اختيار النوع والمصنع");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
            dbconnection2.Close();
        }


        //functions
        private void loadBranch()
        {
            dbconnection.Open();
            string query = "select * from store";
            MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
            DataTable dt = new DataTable();
            da.Fill(dt);
            comStore.DataSource = dt;
            comStore.DisplayMember = dt.Columns["Store_Name"].ToString();
            comStore.ValueMember = dt.Columns["Store_ID"].ToString();
            comStore.SelectedIndex = -1;

            query = "select * from type";
            da = new MySqlDataAdapter(query, dbconnection);
            dt = new DataTable();
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
            txtFacory.Text = "";

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

            loadedBranch = true;
        }

        public void search()
        {
            dbconnection.Open();
            dbconnection2.Open();

            string q1, q2, q3,q4, fQuery = "";
            if (comStore.Text == "")
            {
                q1 = "select Store_ID from store";
            }
            else
            {
                q1 = comStore.SelectedValue.ToString();
            }
            
            if (comGroup.Text == "")
            {
                q2 = "select Group_ID from groupo";
            }
            else
            {
                q2 = comGroup.SelectedValue.ToString();
            }
            if (comProduct.Text == "")
            {
                q3 = "select Product_ID from product";
            }
            else
            {
                q3 = comProduct.SelectedValue.ToString();
            }
            if (comType.Text == "")
            {
                q4 = "select Type_ID from type";

            }
            else
            {
                q4 = comType.SelectedValue.ToString();
            }
            if (comSize.Text != "")
            {
                fQuery += " and size.Size_ID=" + comSize.SelectedValue.ToString();
            }

            double totalQuantity = 0;
            double totalReturnedQuantity = 0;
            string query = "select data.Code as 'الكود',type.Type_Name as 'النوع','الاسم',(supplier_bill_details.Total_Meters) as 'سعر الوارد',(supplier_bill_details.Total_Meters) as 'سعر المرتجع',(supplier_bill_details.Total_Meters) as 'الصافى' FROM supplier_bill inner join supplier_bill_details on supplier_bill.Bill_ID=supplier_bill_details.Bill_ID inner join data on data.Data_ID=supplier_bill_details.Data_ID LEFT JOIN color ON color.Color_ID = data.Color_ID LEFT JOIN size ON size.Size_ID = data.Size_ID LEFT JOIN sort ON sort.Sort_ID = data.Sort_ID INNER JOIN type ON type.Type_ID = data.Type_ID INNER JOIN product ON product.Product_ID = data.Product_ID INNER JOIN factory ON data.Factory_ID = factory.Factory_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID where supplier_bill.Store_ID in (" + q1 + ") and date(supplier_bill.Date) between '" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "' and '" + dateTimePicker2.Value.ToString("yyyy-MM-dd") + "' and data.Factory_ID=" + comFactory.SelectedValue.ToString() + " and data.Group_ID in (" + q2 + ") and data.Product_ID in (" + q3 + ") and data.Type_ID in (" + q4 + ") " + fQuery + " and supplier_bill_details.Data_ID=0 group by supplier_bill_details.Data_ID,supplier_bill_details.Purchasing_Price";
            MySqlDataAdapter adapter = new MySqlDataAdapter(query, dbconnection);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            gridControl1.DataSource = dt;
            gridView1.Columns["الاسم"].Width = 300;
            
            query = "select data.Data_ID,data.Code as 'الكود',type.Type_Name as 'النوع',concat(product.Product_Name,' ',COALESCE(color.Color_Name,''),' ',data.Description,' ',groupo.Group_Name,' ',factory.Factory_Name,' ',COALESCE(size.Size_Value,''),' ',COALESCE(sort.Sort_Value,'')) as 'الاسم',supplier_bill_details.Purchasing_Price*sum(supplier_bill_details.Total_Meters) as 'سعر الوارد' FROM supplier_bill inner join supplier_bill_details on supplier_bill.Bill_ID=supplier_bill_details.Bill_ID inner join data on data.Data_ID=supplier_bill_details.Data_ID LEFT JOIN color ON color.Color_ID = data.Color_ID LEFT JOIN size ON size.Size_ID = data.Size_ID LEFT JOIN sort ON sort.Sort_ID = data.Sort_ID INNER JOIN type ON type.Type_ID = data.Type_ID INNER JOIN product ON product.Product_ID = data.Product_ID INNER JOIN factory ON data.Factory_ID = factory.Factory_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID where supplier_bill.Store_ID in (" + q1 + ") and date(supplier_bill.Date) between '" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "' and '" + dateTimePicker2.Value.ToString("yyyy-MM-dd") + "' and data.Factory_ID=" + comFactory.SelectedValue.ToString() + " and data.Group_ID in (" + q2 + ") and data.Product_ID in (" + q3 + ") and data.Type_ID in (" + q4 + ") " + fQuery + " group by supplier_bill_details.Data_ID,supplier_bill_details.Purchasing_Price  order by SUBSTR(data.Code,1,16),color.Color_Name,data.Sort_ID";
            MySqlCommand c = new MySqlCommand(query, dbconnection);
            MySqlDataReader dataReader1 = c.ExecuteReader();
            while (dataReader1.Read())
            {
                gridView1.AddNewRow();
                int rowHandle = gridView1.GetRowHandle(gridView1.DataRowCount);
                if (gridView1.IsNewItemRow(rowHandle))
                {
                    double ReturnedQuantity = 0;

                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns["الكود"], dataReader1["الكود"].ToString());
                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns["النوع"], dataReader1["النوع"].ToString());
                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns["الاسم"], dataReader1["الاسم"].ToString());
                    
                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns["سعر الوارد"], Convert.ToDouble(dataReader1["سعر الوارد"].ToString()));
                    totalQuantity += Convert.ToDouble(dataReader1["سعر الوارد"].ToString());
                    
                    query = "SELECT supplier_return_bill_details.Purchasing_Price*supplier_return_bill_details.Total_Meters as 'سعر المرتجع' FROM supplier_return_bill INNER JOIN supplier_return_bill_details ON supplier_return_bill.ReturnBill_ID = supplier_return_bill_details.ReturnBill_ID inner join data on data.Data_ID=supplier_return_bill_details.Data_ID where supplier_return_bill.Store_ID in (" + q1 + ") and date(supplier_return_bill.Date) between '" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "' and '" + dateTimePicker2.Value.ToString("yyyy-MM-dd") + "' and supplier_return_bill_details.Data_ID=" + dataReader1["Data_ID"].ToString() + " ";
                    MySqlCommand com2 = new MySqlCommand(query, dbconnection2);
                    if (com2.ExecuteScalar() != null)
                    {
                        ReturnedQuantity = Convert.ToDouble(com2.ExecuteScalar().ToString());
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns["سعر المرتجع"], ReturnedQuantity);
                        totalReturnedQuantity += ReturnedQuantity;
                    }
                    else
                    {
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns["سعر المرتجع"], 0);
                    }

                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns["الصافى"], Convert.ToDouble(dataReader1["سعر الوارد"].ToString()) - ReturnedQuantity);
                }
            }

            if (gridView1.IsLastVisibleRow)
            {
                gridView1.FocusedRowHandle = gridView1.RowCount - 1;
            }
            query = "SELECT DISTINCT supplier_bill.Value_Additive_Tax FROM supplier_bill INNER JOIN supplier ON supplier.Supplier_ID = supplier_bill.Supplier_ID inner join supplier_bill_details on supplier_bill.Bill_ID=supplier_bill_details.Bill_ID inner join data on data.Data_ID=supplier_bill_details.Data_ID where date(supplier_bill.Date) between '" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "' and '" + dateTimePicker2.Value.ToString("yyyy-MM-dd") + "' and data.Factory_ID=" + comFactory.SelectedValue.ToString();
            MySqlCommand comand = new MySqlCommand(query, dbconnection2);
            double Value_Additive_Tax = Convert.ToDouble(comand.ExecuteScalar());
            if (Value_Additive_Tax > 0)

            {
                double taxValue = totalQuantity * 14 / 100;
                totalQuantity += taxValue;
                txtSale.Text = totalQuantity.ToString();
                taxValue = totalReturnedQuantity * 14 / 100;
                totalReturnedQuantity += taxValue;
                txtReturn.Text = totalReturnedQuantity.ToString();
                txtFinal.Text = (totalQuantity - totalReturnedQuantity).ToString();
            }
            else if (Value_Additive_Tax == 0)
            {
                txtSale.Text = totalQuantity.ToString();
                txtReturn.Text = totalReturnedQuantity.ToString();
                txtFinal.Text = (totalQuantity - totalReturnedQuantity).ToString();
            }
            else
            {
                MessageBox.Show("يوجد خطأ");
            }
        }

        private void btnNewChosen_Click(object sender, EventArgs e)
        {
            try
            {
                clearCom();
                gridControl1.DataSource = null;
                txtSale.Text = "0";
                txtReturn.Text = "0";
                txtFinal.Text = "0";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void clearCom()
        {
            foreach (Control co in panel2.Controls)
            {
                if (co is System.Windows.Forms.ComboBox)
                {
                    co.Text = "";
                }
                else if (co is TextBox)
                {
                    co.Text = "";
                }
                else if (co is DateTimePicker)
                {
                    dateTimePicker1.Value = DateTime.Now;
                    dateTimePicker2.Value = DateTime.Now;
                }
            }
        }

        private void comBranch_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (loadedBranch)
                {
                    int branchID = 0;
                    if (int.TryParse(comStore.SelectedValue.ToString(), out branchID))
                    {
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void comFactory_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (loadedBranch)
                {
                    int factoryID = 0;
                    if (int.TryParse(comFactory.SelectedValue.ToString(), out factoryID))
                    {
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnBillReport_Click(object sender, EventArgs e)
        {
            try
            {
                if (comType.SelectedValue != null && comType.Text != "" && comFactory.Text != "" && comFactory.SelectedValue != null && gridView1.RowCount > 0)
                {
                    List<Items_Bills_Details> bi = new List<Items_Bills_Details>();

                    for (int i = 0; i < gridView1.RowCount; i++)
                    {
                        Items_Bills_Details item = new Items_Bills_Details() { Code = gridView1.GetRowCellDisplayText(i, gridView1.Columns["الكود"]), Product_Type = gridView1.GetRowCellDisplayText(i, gridView1.Columns["النوع"]), Product_Name = gridView1.GetRowCellDisplayText(i, gridView1.Columns["الاسم"]), SellingQuantity = Convert.ToDouble(gridView1.GetRowCellDisplayText(i, gridView1.Columns["سعر الوارد"])), ReturnedQuantity = Convert.ToDouble(gridView1.GetRowCellDisplayText(i, gridView1.Columns["سعر المرتجع"])), Safy = Convert.ToDouble(gridView1.GetRowCellDisplayText(i, gridView1.Columns["الصافى"])) };
                        bi.Add(item);
                    }

                    Report_PurchaseDetails f = new Report_PurchaseDetails();
                    f.PrintInvoicePrice(comStore.Text, dateTimePicker1.Value.Date, dateTimePicker2.Value.Date, bi);
                    f.ShowDialog();
                }
                else
                {
                    MessageBox.Show("يجب اختيار النوع والمصنع والتاكد من وجود بنود");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
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
                    string query = "select distinct  product.Product_ID  ,Product_Name  from product inner join product_factory_group on product.Product_ID=product_factory_group.Product_ID  where  " + supQuery + "   order by product.Product_ID";
                    MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    comProduct.DataSource = dt;
                    comProduct.DisplayMember = dt.Columns["Product_Name"].ToString();
                    comProduct.ValueMember = dt.Columns["Product_ID"].ToString();
                    comProduct.Text = "";
                    txtProduct.Text = "";
                }
            }

        }
    }
}

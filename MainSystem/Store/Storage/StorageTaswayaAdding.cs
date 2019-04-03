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

namespace MainSystem
{
    public partial class StorageTaswayaAdding : Form
    {
        MySqlConnection dbconnection;
        bool loaded = false;
        bool factoryFlage = false;
        bool groupFlage = false;
        bool flagProduct = false;
        double noMeter = 0;
        int TaswayaAdding_ID = 0;
        XtraTabControl xtraTabControlStoresContent;
        int Data_ID=-1, Storage_ID=-1;
        string code = "";
        MainForm mainForm;
        DataTable mdt=null;
        DataRowView mRow = null;

        public StorageTaswayaAdding(MainForm mainForm,XtraTabControl xtraTabControlStoresContent)
        {
            try
            {
                InitializeComponent();
                dbconnection = new MySqlConnection(connection.connectionString);
                this.mainForm = mainForm;
                this.xtraTabControlStoresContent = xtraTabControlStoresContent;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

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

                query = "select * from store";
                da = new MySqlDataAdapter(query, dbconnection);
                dt = new DataTable();
                da.Fill(dt);
                comStore.DataSource = dt;
                comStore.DisplayMember = dt.Columns["Store_Name"].ToString();
                comStore.ValueMember = dt.Columns["Store_ID"].ToString();
                comStore.Text = "";
                mdt = new DataTable();
                mdt = createDataTable();
                loaded = true;
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
                if (loaded)
                {
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
                                    groupFlage = true;
                                }
                                factoryFlage = true;

                          
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

                              

                                comProduct.Focus();
                                flagProduct = true;
                            }
                            break;

                        case "comProduct":

                            txtProduct.Text = comProduct.SelectedValue.ToString();
                     

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
                            case "txtCodePart1":
                                query = "select Type_Name from type where Type_ID='" + txtCodePart1.Text + "'";
                                com = new MySqlCommand(query, dbconnection);
                                if (com.ExecuteScalar() != null)
                                {
                                    Name = (string)com.ExecuteScalar();
                                    comType.Text = Name;
                                    txtType.Text = txtCodePart1.Text;
                                    makeCode(txtBox);
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
                                    txtFactory.Text = txtCodePart2.Text;
                                    makeCode(txtBox);
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
                                    txtGroup.Text = txtCodePart3.Text;
                                    makeCode(txtBox);
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
                                    txtProduct.Text = txtCodePart4.Text;
                                    makeCode(txtBox);
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
                                makeCode(txtBox);
                                txtTotalMeter.Focus();
                                break;
                            case "txtAddingQuantity":
                                //add2Store();
                                if (mRow != null)
                                {
                                    add2GridView(mdt, mRow);
                                }
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
        }  

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (comStore.Text != "")
                {
                    displayProducts();
                }
                else
                {
                    MessageBox.Show("حدد المخزن ومكان التخزين");
                }
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
                    noMeter = Convert.ToDouble(row[4].ToString());
                    txtTotalMeter.Text = noMeter.ToString();
                    Data_ID = Convert.ToInt16(row[1].ToString());
                    Storage_ID = Convert.ToInt16(row[0].ToString());
                    code = row[2].ToString();
                    displayCode(code);
                    mRow = row;
                    txtAddingQuantity.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void gridControl1_ProcessGridKey(object sender, KeyEventArgs e)
        {
            try
            {
                txtTotalMeter.Focus();
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
                add2Store();
              
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }

        private void btnNewChooes_Click(object sender, EventArgs e)
        {
            try
            {
                comStore.Text = "";

                comType.Text = "";
                comFactory.Text = "";
                comGroup.Text = "";
                comProduct.Text = "";

                txtType.Text = "";
                txtFactory.Text = "";
                txtGroup.Text = "";
                txtProduct.Text = "";

                gridControl1.DataSource = null;
                gridControl2.DataSource = null;
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void comStore_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                dbconnection.Open();
                string query = "insert into taswayaa_adding_permision (Store_ID,Date)values (Store_ID,Date)";
                MySqlCommand com = new MySqlCommand(query, dbconnection);
                com.Parameters.Add("@Store_ID", MySqlDbType.Int16);
                com.Parameters["@Store_ID"].Value = comStore.SelectedValue;
                com.Parameters.Add("@Date", MySqlDbType.Date, 0);
                com.Parameters["@Date"].Value = dateTimePicker1.Value;
                com.ExecuteNonQuery();

                query = "select TaswayaAdding_ID from taswayaa_adding_permision order by TaswayaAdding_ID desc limit 1";
                com = new MySqlCommand(query, dbconnection);
                TaswayaAdding_ID = Convert.ToInt16(com.ExecuteScalar());
                txtPermission.Text = TaswayaAdding_ID.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            try
            {
                if(TaswayaAdding_ID!=0)
                mainForm.bindReportStorageForm(gridControl2, "أذن تسوية اضافة \n"+ TaswayaAdding_ID+"\n"+comStore.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void gridView2_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            try
            {
                if (e.Column.Name == "الكمية المضافة")
                {
                    GridView view = (GridView)sender;
                    DataRow dataRow = view.GetFocusedDataRow();
                    double addingQuantity = Convert.ToDouble(dataRow["الكمية المضافة"].ToString());
                    double totalBeforAdding = Convert.ToDouble(dataRow["اجمالي عدد الوحدات قبل الاضافة"].ToString());

                    view.SetRowCellValue(view.GetSelectedRows()[0], "اجمالي عدد الوحدات", totalBeforAdding + addingQuantity);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                dbconnection.Open();
                save2DB();
           
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            try
            {
                int rowHandle = gridView2.GetRowHandle(gridView2.GetSelectedRows()[0]);
                gridView2.DeleteRow(rowHandle);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //functions
        public void clear()
        {
            txtCodePart1.Text = txtCodePart2.Text = txtCodePart3.Text = txtCodePart4.Text = txtCodePart5.Text = "";
            txtTotalMeter.Text = "";
            txtAddingQuantity.Text = "";
            txtNote.Text ="";
        }
        public XtraTabPage getTabPage(string text)
        {
            for (int i = 0; i < xtraTabControlStoresContent.TabPages.Count; i++)
                if (xtraTabControlStoresContent.TabPages[i].Text == text)
                {
                    return xtraTabControlStoresContent.TabPages[i];
                }
            return null;
        }
        public bool IsClear()
        {
            foreach (Control item in this.Controls["tableLayoutPanel1"].Controls)
            {
                if (item is TextBox)
                {
                    if (item.Text != "")
                        return false;
                }
                else if (item is ComboBox)
                {
                    if (item.Text != "")
                        return false;
                }
                else if (item is DateTimePicker)
                {
                    DateTimePicker item1 = (DateTimePicker)item;
                    if (item1.Value.Date != DateTime.Now.Date)
                        return false;
                }
            }
            return true;
        }
        public void displayProducts()
        {
            try
            {
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
                string query1 = "";
                if (comStore.Text != "")
                {
                    query1 += " and storage.Store_ID=" + comStore.SelectedValue;
                }
               

                //string Month = DateTime.Now.Month.ToString();
                //if (Month.Length < 2)
                //    Month = "0" + Month;
                //string Day = DateTime.Now.Day.ToString();
                //if (Day.Length < 2)
                //    Day = "0" + Day;

                //string date = DateTime.Now.Year + "-" + Month + "-" + Day;
                string itemName = "concat( product.Product_Name,' ',type.Type_Name,' ',factory.Factory_Name,' ',groupo.Group_Name,' ' ,COALESCE(color.Color_Name,''),' ',COALESCE(size.Size_Value,''),' ',COALESCE(sort.Sort_Value,''),' ',COALESCE(data.Classification,''),' ',COALESCE(data.Description,''))as 'البند'";
                string qq = "select Storage_ID,data.Data_ID, data.Code as 'كود',"+ itemName +",storage.Total_Meters as 'اجمالي عدد الوحدات', storage.Note as 'ملاحظة' from storage INNER JOIN store on storage.Store_ID=store.Store_ID INNER JOIN store_places on storage.Store_Place_ID=store_places.Store_Place_ID  INNER JOIN data  ON storage.Data_ID = data.Data_ID INNER JOIN type ON type.Type_ID = data.Type_ID INNER JOIN product ON product.Product_ID = data.Product_ID INNER JOIN factory ON data.Factory_ID = factory.Factory_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID LEFT outer JOIN color ON data.Color_ID = color.Color_ID LEFT outer  JOIN size ON data.Size_ID = size.Size_ID LEFT outer  JOIN sort ON data.Sort_ID = sort.Sort_ID  where data.Type_ID IN(" + q1 + ") and  data.Factory_ID  IN(" + q2 + ") and  data.Product_ID  IN(" + q3 + ") and data.Group_ID IN (" + q4 + ") " + query1+ " order by Storage_ID desc";
                MySqlDataAdapter da = new MySqlDataAdapter(qq, dbconnection);
                DataTable dt = new DataTable();
                da.Fill(dt);
               
                gridControl1.DataSource = dt;
                gridView1.Columns[0].Visible = false;
                gridView1.Columns[1].Visible = false;
                gridView1.BestFitColumns();
                //load = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        public void displayProductsAddingQuantity()
        {
            try
            {
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
                string query1 = "";
                if (comStore.Text != "")
                {
                    query1 += " and addstorage.Store_ID=" + comStore.SelectedValue;
                }
             

                string Month = DateTime.Now.Month.ToString();
                if (Month.Length < 2)
                    Month = "0" + Month;
                string Day = DateTime.Now.Day.ToString();
                if (Day.Length < 2)
                    Day = "0" + Day;

                string date = DateTime.Now.Year + "-" + Month + "-" + Day;
                string itemName = "concat( product.Product_Name,' ',type.Type_Name,' ',factory.Factory_Name,' ',groupo.Group_Name,' ' ,COALESCE(color.Color_Name,''),' ',COALESCE(size.Size_Value,''),' ',COALESCE(sort.Sort_Value,''),' ',COALESCE(data.Classification,''),' ',COALESCE(data.Description,''))as 'البند'";
                string qq = "select AddStorage_ID as 'الرقم المسلسل', data.Data_ID, data.Code as 'كود'," + itemName + ", store.Store_Name as 'المخزن', Store_Place_Code as 'مكان التخزين'  ,addstorage.QuantityAfterAdding as 'اجمالي عدد الوحدات',addstorage.AddingQuantity as 'الكمية المضافة', addstorage.Note as 'ملاحظة' from addstorage INNER JOIN store on addstorage.Store_ID=store.Store_ID INNER JOIN store_places on addstorage.Store_Place_ID=store_places.Store_Place_ID  INNER JOIN data  ON addstorage.Data_ID = data.Data_ID INNER JOIN type ON type.Type_ID = data.Type_ID INNER JOIN product ON product.Product_ID = data.Product_ID INNER JOIN factory ON data.Factory_ID = factory.Factory_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID LEFT outer JOIN color ON data.Color_ID = color.Color_ID LEFT outer  JOIN size ON data.Size_ID = size.Size_ID LEFT outer  JOIN sort ON data.Sort_ID = sort.Sort_ID  where data.Type_ID IN(" + q1 + ") and  data.Factory_ID  IN(" + q2 + ") and  data.Product_ID  IN(" + q3 + ") and data.Group_ID IN (" + q4 + ") " + query1 + " and Date='"+date+"' order by AddStorage_ID desc";
                MySqlDataAdapter da = new MySqlDataAdapter(qq, dbconnection);
                DataTable dt = new DataTable();
                da.Fill(dt);

                gridControl2.DataSource = dt;
                gridView2.Columns[1].Visible = false;
                gridView2.BestFitColumns();
                //load = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        public void displayCode(string code)
        {
            char[] arrCode = code.ToCharArray();
            txtCodePart1.Text =Convert.ToInt16(arrCode[0].ToString() + arrCode[1].ToString() + arrCode[2].ToString() + arrCode[3].ToString()) + "";
            txtCodePart2.Text = Convert.ToInt16(arrCode[4].ToString() + arrCode[5].ToString() + arrCode[6].ToString() + arrCode[7].ToString() )+ "";
            txtCodePart3.Text = Convert.ToInt16(arrCode[8].ToString() + arrCode[9].ToString() + arrCode[10].ToString() + arrCode[11].ToString()) + "";
            txtCodePart4.Text = Convert.ToInt16(arrCode[12].ToString() + arrCode[13].ToString() + arrCode[14].ToString() + arrCode[15].ToString() )+ "";
            txtCodePart5.Text = "" + Convert.ToInt16(arrCode[16].ToString() + arrCode[17].ToString() + arrCode[18].ToString() + arrCode[19].ToString());
        }
        public void makeCode(TextBox txtBox)
        {
            int j = 4 - txtBox.TextLength;
            for (int i = 0; i < j; i++)
            {
                code += "0";
            }
            code += txtBox.Text;
        }
        public void add2Store()
        {
            if (code.Length==20 && txtAddingQuantity.Text!="" &&txtNote.Text!="")
            {
                string query = "select Data_ID from data where Code='" + code + "'";
                MySqlCommand com = new MySqlCommand(query, dbconnection);
                Data_ID = Convert.ToInt16(com.ExecuteScalar());
               
                query = "insert into addstorage (Store_ID,Date,Data_ID,Store_Place_ID,CurrentQuantity,AddingQuantity,QuantityAfterAdding,Note) values (@Store_ID,@Date,@Data_ID,@Store_Place_ID,@CurrentQuantity,@AddingQuantity,@QuantityAfterAdding,@Note)";
                com = new MySqlCommand(query, dbconnection);
                com.Parameters.Add("@Store_ID", MySqlDbType.Int16);
                com.Parameters["@Store_ID"].Value = comStore.SelectedValue;
                com.Parameters.Add("@Date", MySqlDbType.Date, 0);
                com.Parameters["@Date"].Value = dateTimePicker1.Value;
                com.Parameters.Add("@Data_ID", MySqlDbType.Int16);
                com.Parameters["@Data_ID"].Value = Data_ID;
                com.Parameters.Add("@CurrentQuantity", MySqlDbType.Decimal);
                com.Parameters["@CurrentQuantity"].Value = txtTotalMeter.Text;
                com.Parameters.Add("@AddingQuantity", MySqlDbType.Decimal);
                com.Parameters["@AddingQuantity"].Value = txtAddingQuantity.Text;
                com.Parameters.Add("@QuantityAfterAdding", MySqlDbType.Decimal);
                com.Parameters["@QuantityAfterAdding"].Value = Convert.ToDouble(txtTotalMeter.Text)+Convert.ToDouble(txtAddingQuantity.Text);
                com.Parameters.Add("@Note", MySqlDbType.VarChar);
                com.Parameters["@Note"].Value = txtNote.Text;
                com.ExecuteNonQuery();

                double re = Convert.ToDouble(txtTotalMeter.Text) + Convert.ToDouble(txtAddingQuantity.Text);
                query = " update storage set Total_Meters="+re + " where Data_ID= "+ Data_ID +" and Store_ID ="+ comStore.SelectedValue;
                com = new MySqlCommand(query, dbconnection);
                com.ExecuteNonQuery();

                MessageBox.Show("Add success");
                displayProducts();
                displayProductsAddingQuantity();
                clear();
                txtCodePart1.Focus();
            }
            else
            {
                MessageBox.Show("you must fill all fields please");
                dbconnection.Close();
                return;
            }

        }
        public void save2DB()
        {
            if (code.Length == 20 && txtAddingQuantity.Text != "" && txtNote.Text != "")
            {
             
                gridView2.SelectAll();
                for (int i = 0; i < mdt.Rows.Count; i++)
                {
                    string query = "insert into addstorage (TaswayaAdding_ID,Data_ID,Store_Place_ID,CurrentQuantity,AddingQuantity,QuantityAfterAdding,Note) values (@TaswayaAdding_ID,@Data_ID,@Store_Place_ID,@CurrentQuantity,@AddingQuantity,@QuantityAfterAdding,@Note)";
                    MySqlCommand com = new MySqlCommand(query, dbconnection);
                    com.Parameters.Add("@TaswayaAdding_ID", MySqlDbType.Int16);
                    com.Parameters["@TaswayaAdding_ID"].Value = TaswayaAdding_ID;
                    com.Parameters.Add("@Store_Place_ID", MySqlDbType.Int16);
                    com.Parameters["@Store_Place_ID"].Value = getStore_Place_ID((int)comStore.SelectedValue);
                    com.Parameters.Add("@Data_ID", MySqlDbType.Int16);
                    com.Parameters["@Data_ID"].Value = mdt.Rows[i][1];
                    com.Parameters.Add("@CurrentQuantity", MySqlDbType.Decimal);
                    com.Parameters["@CurrentQuantity"].Value = Convert.ToDouble(mdt.Rows[i][4]);
                    com.Parameters.Add("@AddingQuantity", MySqlDbType.Decimal);
                    com.Parameters["@AddingQuantity"].Value = mdt.Rows[i][5];
                    com.Parameters.Add("@QuantityAfterAdding", MySqlDbType.Decimal);
                    com.Parameters["@QuantityAfterAdding"].Value = mdt.Rows[i][6];
                    com.Parameters.Add("@Note", MySqlDbType.VarChar);
                    com.Parameters["@Note"].Value = mdt.Rows[i][6];
                    com.ExecuteNonQuery();
                }
                labVcode.Visible = false;
                labVaddingMeter.Visible = false;
                labVnote.Visible = false;
            }
            else
            {
                if (code.Length != 20)
                    labVcode.Visible = true;
                if (txtAddingQuantity.Text == "")
                    labVaddingMeter.Visible = true;
                if (txtNote.Text == "")
                    labVnote.Visible = true;

                MessageBox.Show("ادخل كل البيانات المطلوبة");
            }

        }
        public DataTable createDataTable()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add("Data_ID", typeof(int));
            dt.Columns.Add("Store_Place_ID", typeof(int));
            dt.Columns.Add("كود", typeof(string));
            dt.Columns.Add("البند", typeof(string));
            dt.Columns.Add("اجمالي عدد الوحدات قبل الاضافة", typeof(double));
            dt.Columns.Add("اجمالي عدد الوحدات", typeof(double));
            dt.Columns.Add("الكمية المضافة", typeof(double));
            dt.Columns.Add("ملاحظة", typeof(string));

            return dt;
        }
        public void add2GridView(DataTable dt, DataRowView row)
        {
            dt.Rows.Add(new object[] {
                Convert.ToInt16(row[1].ToString()),
                 getStore_Place_ID((int)comStore.SelectedValue),
                row[2].ToString(),
                row[3].ToString(),
                Convert.ToDouble(row[4].ToString()),
                Convert.ToDouble(row[4].ToString())+Convert.ToDouble(txtAddingQuantity.Text),
                Convert.ToDouble(txtAddingQuantity.Text),
                txtNote.Text
            });
            gridControl2.DataSource = dt;
            gridView2.Columns[0].Visible = false;
            gridView2.Columns[1].Visible = false;
            gridView2.Columns[2].OptionsColumn.AllowEdit = false;
            gridView2.Columns[3].OptionsColumn.AllowEdit = false;
            gridView2.Columns[5].OptionsColumn.AllowEdit = false;
            gridView2.Columns[4].Visible = false;

        }
        public int getStore_Place_ID(int Store_ID)
        {
            dbconnection.Close();
            dbconnection.Open();
            string query = "select Store_Place_ID from store_places inner join store on store_places.Store_ID=store.Store_ID where store_places.Store_ID="+Store_ID+" limit 1";
            MySqlCommand com = new MySqlCommand(query, dbconnection);
            int Store_Place_ID = Convert.ToInt16(com.ExecuteScalar());
           
            return Store_Place_ID;
        }
    }

}

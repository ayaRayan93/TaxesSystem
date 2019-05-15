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
using Microsoft.VisualBasic;

namespace MainSystem
{
    public partial class initialCodeStorage : Form
    {
        MySqlConnection dbconnection;
        bool loaded = false;
        bool factoryFlage = false;
        bool groupFlage = false;
        bool flagProduct = false;
        bool flag = false;
        double noMeter = 0;
        MainForm MainForm;
        XtraTabControl xtraTabControlStoresContent;
        DataTable mdt;
        DataTable mdtSaved;
        int Data_ID;
        string code = "";
        DataRowView mRow = null;
        List<int> ListOfDataIDs;
        List<int> ListOfSavedDataIDs;
        List<int> ListOfEditDataIDs;

        public initialCodeStorage(MainForm MainForm/*XtraTabControl xtraTabControlStoresContent*/)
        {
            try
            {
                InitializeComponent();
                dbconnection = new MySqlConnection(connection.connectionString);
                this.MainForm = MainForm;
                ListOfDataIDs = new List<int>();
                ListOfSavedDataIDs = new List<int>();
                ListOfEditDataIDs = new List<int>();
                //this.xtraTabControlStoresContent = xtraTabControlStoresContent;
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
                            code = "";
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
                            displayData(code);
                            DataRowView row = (DataRowView)(((GridView)gridControl1.MainView).GetRow(((GridView)gridControl1.MainView).GetSelectedRows()[0]));
                            mRow = row;
                            txtTotalMeter.Focus();
                            break;
                        case "txtTotalMeter":
                            if (validation())
                            {
                                if (mRow != null)
                                {
                                    if (!IsExistInGridView(code))
                                        add2GridView(mdt, mRow,getOldValueIfExist((int)mRow[0]));
                                    else
                                        MessageBox.Show("البند مضاف");
                                }
                             
                                clear();
                            }
                            else
                            {
                                MessageBox.Show("تاكد من ادخال البيانات المطلوبة واختيار المخزن ومكان التخزين");
                            }
                            
                            txtCodePart1.Focus();

                            break;
                        case "txtNote":
                            txtTotalMeter.Focus();
                            break;
                                
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show("خطأ في الادخال");
                    txtCodePart1.Text = txtCodePart2.Text = txtCodePart3.Text = txtCodePart4.Text = txtCodePart5.Text = "";
                    code = "";
                    txtCodePart1.Focus();
                }
                dbconnection.Close();
            }
        }
        private void comStore_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (loaded)
                {
                    dbconnection.Open();
                    string query = "select * from store_places where Store_ID="+comStore.SelectedValue;
                    MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    comStorePlace.DataSource = dt;
                    comStorePlace.DisplayMember = dt.Columns["Store_Place_Code"].ToString();
                    comStorePlace.ValueMember = dt.Columns["Store_Place_ID"].ToString();
                   
                    comStorePlace.Visible = true;
                    labStorePlace.Visible = true;
                    label11.Visible = true;
                    ListOfEditDataIDs.Clear();
                    ListOfSavedDataIDs.Clear();
                    ListOfDataIDs.Clear();
                    displayProducts();
                }
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
                displayData("");
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
                    //noMeter = Convert.ToDouble(row[11].ToString());
                    Data_ID = Convert.ToInt16(row[0].ToString());
                    code = row[1].ToString();
                    displayCode(code);
                    mRow = row;
                    txtTotalMeter.Focus();
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
                if (validation())
                {

                    if (mRow != null)
                    {
                        if (!IsExistInGridView(code))
                            add2GridView(mdt, mRow, getOldValueIfExist((int)mRow[0]));
                        else
                            MessageBox.Show("البند مضاف");
                    }

                    clear();
                }
                else {
                    MessageBox.Show("تاكد من ادخال البيانات المطلوبة واختيار المخزن ومكان التخزين");
                }
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
                DataRowView row = (DataRowView)(((GridView)gridControl1.MainView).GetRow(((GridView)gridControl1.MainView).GetSelectedRows()[0]));
                int Data_ID = Convert.ToInt16(row[0].ToString());
                if (!IsSaved(Data_ID))
                {
                    int rowHandle = gridView2.GetRowHandle(gridView2.GetSelectedRows()[0]);
                    gridView2.DeleteRow(rowHandle);
                }
                else
                {
                    MessageBox.Show("هذا البند تم حفظه لايمكن حذفه");
                }
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
                comStore.Text = "";
                comStorePlace.Text = "";

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
                mdt.Rows.Clear();
                flag = false;
                
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
                if (gridView2.RowCount > 0)
                {
                    if (flag)//to ensure data save first
                    {
                        MainForm.bindReportStorageForm(gridControl2, "تقرير كميات البنود");
                    }
                    else
                    {
                        MessageBox.Show("يجب حفظ البيانات اولا");
                    }
                }
                else
                {
                    MessageBox.Show("لا يوجد بيانات للطباعة");
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
                if ((ListOfSavedDataIDs.Count-ListOfEditDataIDs.Count) != mdt.Rows.Count)
                {
                    for (int i = 0; i < mdt.Rows.Count; i++)
                    {
                        if (!IsEdited((int)mdt.Rows[i][0]) && !IsSaved((int)mdt.Rows[i][0]))
                        {
                            string query = "select OpenStorageAccount_ID from open_storage_account where Data_ID=" + mdt.Rows[i][0];
                            MySqlCommand com = new MySqlCommand(query, dbconnection);
                            if (com.ExecuteScalar() == null)
                            {
                                query = "insert into open_storage_account (Data_ID,Quantity,Store_ID,Store_Place_ID,Date,Note) values (@Data_ID,@Quantity,@Store_ID,@Store_Place_ID,@Date,@Note)";
                                com = new MySqlCommand(query, dbconnection);
                                com.Parameters.Add("@Data_ID", MySqlDbType.Int16);
                                com.Parameters["@Data_ID"].Value = mdt.Rows[i][0];
                                com.Parameters.Add("@Quantity", MySqlDbType.Decimal);
                                com.Parameters["@Quantity"].Value = mdt.Rows[i][5];
                                com.Parameters.Add("@Store_ID", MySqlDbType.Int16);
                                com.Parameters["@Store_ID"].Value = mdt.Rows[i][1];
                                com.Parameters.Add("@Store_Place_ID", MySqlDbType.Int16);
                                com.Parameters["@Store_Place_ID"].Value = mdt.Rows[i][2];
                                com.Parameters.Add("@Date", MySqlDbType.Date, 0);
                                DateTime date = Convert.ToDateTime(mdt.Rows[i][7]);
                                string d = date.ToString("yyyy-MM-dd");
                                com.Parameters["@Date"].Value = d;
                                com.Parameters.Add("@Note", MySqlDbType.VarChar);
                                com.Parameters["@Note"].Value = mdt.Rows[i][6];
                                com.ExecuteNonQuery();

                                query = "insert into Storage (Store_ID,Type,Storage_Date,Data_ID,Store_Place_ID,Total_Meters,Note) values (@Store_ID,@Type,@Date,@Data_ID,@PlaceOfStore,@TotalOfMeters,@Note)";
                                com = new MySqlCommand(query, dbconnection);
                                com.Parameters.Add("@Store_ID", MySqlDbType.Int16);
                                com.Parameters["@Store_ID"].Value = mdt.Rows[i][1];
                                com.Parameters.Add("@Type", MySqlDbType.VarChar);
                                com.Parameters["@Type"].Value = "بند";
                                com.Parameters.Add("@Date", MySqlDbType.Date, 0);
                                date = Convert.ToDateTime(mdt.Rows[i][7]);
                                d = date.ToString("yyyy-MM-dd");
                                com.Parameters["@Date"].Value = d;
                                com.Parameters.Add("@Data_ID", MySqlDbType.Int16);
                                com.Parameters["@Data_ID"].Value = mdt.Rows[i][0];
                                com.Parameters.Add("@PlaceOfStore", MySqlDbType.Int16);
                                com.Parameters["@PlaceOfStore"].Value = mdt.Rows[i][2];
                                com.Parameters.Add("@TotalOfMeters", MySqlDbType.Decimal);
                                com.Parameters["@TotalOfMeters"].Value = mdt.Rows[i][5];
                                com.Parameters.Add("@Note", MySqlDbType.VarChar);
                                com.Parameters["@Note"].Value = mdt.Rows[i][6];
                                com.ExecuteNonQuery();

                                UserControl.ItemRecord("open_storage_account", "اضافة", (int)mdt.Rows[i][0], DateTime.Now, "", dbconnection);
                            }
                            else
                            {
                                MessageBox.Show("تم حفظ هذاالبند من قبل");
                            }
                        }
                        if (IsEdited((int)mdt.Rows[i][0]))
                        {
                            string query = "update  open_storage_account set Quantity=@Quantity,Store_Place_ID=@Store_Place_ID,Date=@Date,Note=@Note where Data_ID=" + mdt.Rows[i][0] + " and Store_ID=" + mdt.Rows[i][1];
                            MySqlCommand com = new MySqlCommand(query, dbconnection);
                            com.Parameters.Add("@Quantity", MySqlDbType.Decimal);
                            com.Parameters["@Quantity"].Value = mdt.Rows[i][5];
                            com.Parameters.Add("@Store_Place_ID", MySqlDbType.Int16);
                            com.Parameters["@Store_Place_ID"].Value = mdt.Rows[i][2];
                            com.Parameters.Add("@Date", MySqlDbType.Date, 0);
                            DateTime date = Convert.ToDateTime(mdt.Rows[i][7]);
                            string d = date.ToString("yyyy-MM-dd");
                            com.Parameters["@Date"].Value = d;
                            com.Parameters.Add("@Note", MySqlDbType.VarChar);
                            com.Parameters["@Note"].Value = mdt.Rows[i][6];
                            com.ExecuteNonQuery();

                            query = "update Storage set Type=@Type,Storage_Date=@Storage_Date,Store_Place_ID=@Store_Place_ID,Total_Meters=@Total_Meters,Note=@Note where Data_ID=" + mdt.Rows[i][0] + " and Store_ID=" + mdt.Rows[i][1];
                            com = new MySqlCommand(query, dbconnection);
                            com.Parameters.Add("@Type", MySqlDbType.VarChar);
                            com.Parameters["@Type"].Value = "بند";
                            com.Parameters.Add("@Storage_Date", MySqlDbType.Date, 0);
                            date = Convert.ToDateTime(mdt.Rows[i][7]);
                            d = date.ToString("yyyy-MM-dd");
                            com.Parameters["@Storage_Date"].Value = d;
                            com.Parameters.Add("@Store_Place_ID", MySqlDbType.Int16);
                            com.Parameters["@Store_Place_ID"].Value = mdt.Rows[i][2];
                            com.Parameters.Add("@Total_Meters", MySqlDbType.Decimal);
                            com.Parameters["@Total_Meters"].Value = mdt.Rows[i][5];
                            com.Parameters.Add("@Note", MySqlDbType.VarChar);
                            com.Parameters["@Note"].Value = mdt.Rows[i][6];
                            com.ExecuteNonQuery();

                            string value = "";

                            if (initialCodeStorage.InputBox("ادخل سبب التعديل؟", mdt.Rows[i][1] + "هذا البند تم تعديله ", ref value) == DialogResult.OK)
                            {
                                UserControl.ItemRecord("open_storage_account", "تعديل", (int)mdt.Rows[i][0], DateTime.Now, value, dbconnection);
                            }

                        }

                    }
                    MessageBox.Show("تم الحفظ");
                    flag = true;
                    ListOfEditDataIDs.Clear();
                    displayProducts();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }
        private void gridView2_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            try
            {
                GridView View = sender as GridView;
                if (e.RowHandle >= 0)
                {
                    int Data_ID = Convert.ToInt16(View.GetRowCellDisplayText(e.RowHandle, View.Columns[0]));

                    for (int i = 0; i < ListOfSavedDataIDs.Count; i++)
                    {
                        if (Data_ID == ListOfSavedDataIDs[i])
                        {
                            e.Appearance.BackColor = Color.MediumSeaGreen;
                            e.Appearance.BackColor2 = Color.MediumSeaGreen;
                            e.Appearance.ForeColor = Color.White;
                        }
                    }
                    for (int i = 0; i < ListOfEditDataIDs.Count; i++)
                    {
                        if (Data_ID == ListOfEditDataIDs[i])
                        {
                            e.Appearance.BackColor = Color.LightBlue;
                            e.Appearance.BackColor2 = Color.LightBlue;
                            e.Appearance.ForeColor = Color.White;
                        }
                    }
                }
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
                GridView view = (GridView)sender;
                DataRow dataRow = view.GetFocusedDataRow();
                int Data_ID = Convert.ToInt16(dataRow["Data_ID"].ToString());
                if (IsSaved(Data_ID))
                    ListOfEditDataIDs.Add(Data_ID);

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
            txtNote.Text ="";
            txtTotalMeter.Text = "";
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
        public void displayData(string code)
        {
            string q1, q2, q3, q4;
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
            string q = "";
            if (code != "")
            {
                q = " and data.Code='" + code + "'";
            }
            else
            {
                q = "";
            }
            string itemName = "concat( product.Product_Name,' ',type.Type_Name,' ',factory.Factory_Name,' ',groupo.Group_Name,' ' ,COALESCE(color.Color_Name,''),' ',COALESCE(size.Size_Value,''),' ',COALESCE(sort.Sort_Value,''),' ',COALESCE(data.Classification,''),' ',COALESCE(data.Description,''))as 'البند'";
            string query = "SELECT data.Data_ID, data.Code as 'الكود'," + itemName + ",data.Carton as 'الكرتنة' from data INNER JOIN type ON type.Type_ID = data.Type_ID INNER JOIN product ON product.Product_ID = data.Product_ID INNER JOIN factory ON data.Factory_ID = factory.Factory_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID LEFT outer JOIN color ON data.Color_ID = color.Color_ID LEFT outer  JOIN size ON data.Size_ID = size.Size_ID LEFT outer  JOIN sort ON data.Sort_ID = sort.Sort_ID where  data.Type_ID IN(" + q1 + ") and  data.Factory_ID  IN(" + q2 + ") and  data.Product_ID  IN(" + q3 + ") and data.Group_ID IN (" + q4 + ") "+q;// " and data.Data_ID not in(" + getDataIDsWhichHaveQuantity() + ")";

            MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
            DataTable dt = new DataTable();
            da.Fill(dt);
            gridControl1.DataSource = dt;
            gridView1.Columns[0].Visible = false;
            gridView1.Columns[1].Width = 200;
            gridView1.BestFitColumns();
        }
        public void displayProducts()
        {
            try
            {

                //loaded = false;
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
                    query1 += " and open_storage_account.Store_ID=" + comStore.SelectedValue;
                }
                if (comStorePlace.Text != "")
                {
                    query1 += " and open_storage_account.Store_Place_ID=" + comStorePlace.SelectedValue;
                }

                string Month = DateTime.Now.Month.ToString();
                if (Month.Length < 2)
                    Month = "0" + Month;
                string Day = DateTime.Now.Day.ToString();
                if (Day.Length < 2)
                    Day = "0" + Day;

                string date = DateTime.Now.Year + "-" + Month + "-" + Day;
                string itemName = "concat( product.Product_Name,' ',type.Type_Name,' ',factory.Factory_Name,' ',groupo.Group_Name,' ' ,COALESCE(color.Color_Name,''),' ',COALESCE(size.Size_Value,''),' ',COALESCE(sort.Sort_Value,''),' ',COALESCE(data.Classification,''),' ',COALESCE(data.Description,''))as 'البند'";
                string qq = "select data.Data_ID,Store_ID,Store_Place_ID, data.Code as 'كود'," + itemName + ",Quantity as 'رصيد البند', Note as 'ملاحظة',Date as 'التاريخ' from open_storage_account  INNER JOIN data  ON open_storage_account.Data_ID = data.Data_ID INNER JOIN type ON type.Type_ID = data.Type_ID INNER JOIN product ON product.Product_ID = data.Product_ID INNER JOIN factory ON data.Factory_ID = factory.Factory_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID LEFT outer JOIN color ON data.Color_ID = color.Color_ID LEFT outer  JOIN size ON data.Size_ID = size.Size_ID LEFT outer  JOIN sort ON data.Sort_ID = sort.Sort_ID  where data.Type_ID IN(" + q1 + ") and  data.Factory_ID  IN(" + q2 + ") and  data.Product_ID  IN(" + q3 + ") and data.Group_ID IN (" + q4 + ")"/* and Date='" + date + "' */ + query1+ " order by OpenStorageAccount_ID desc";
                MySqlDataAdapter da = new MySqlDataAdapter(qq, dbconnection);
                DataTable dt = new DataTable();
                da.Fill(dt);
                mdt = dt;
                ListOfSavedDataIDs.Clear();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ListOfSavedDataIDs.Add(Convert.ToInt16(dt.Rows[i][0].ToString()));
                }
                gridControl2.DataSource = mdt;
                gridView2.Columns[0].Visible = false;
                gridView2.Columns[1].Visible = false;
                gridView2.Columns[2].Visible = false;
                gridView2.Columns[3].OptionsColumn.AllowEdit = false;
                gridView2.Columns[4].OptionsColumn.AllowEdit = false;
                gridView2.Columns[6].OptionsColumn.AllowEdit = false;
                gridView2.BestFitColumns();
                //load = true;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        public string getDataIDsWhichHaveQuantity()
        {

            dbconnection.Close();
            dbconnection.Open();
            string query = "select Data_ID from storage where Data_ID is not null and Store_ID="+comStore.SelectedValue+" and Store_Place_ID="+comStorePlace.SelectedValue;
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
        public bool validation(int storeID,int StorePlaceID)
        {
            string query = "select Storage_ID from storage where Data_ID="+Data_ID+" and Store_ID="+ storeID + " and Store_Place_ID="+ StorePlaceID;
            MySqlCommand com = new MySqlCommand(query, dbconnection);
            if (com.ExecuteScalar() != null)
            {
                return false;
            }
            else
            {
                return true;
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
            if (code.Length < 20)
            {
                int j = 4 - txtBox.TextLength;
                for (int i = 0; i < j; i++)
                {
                    code += "0";
                }
                code += txtBox.Text;
            }
        }
        public void add2Store()
        {
            if (comStorePlace.Text != "" && code.Length==20)
            {
                string query = "select Data_ID from data where Code='" + code + "'";
                MySqlCommand com = new MySqlCommand(query, dbconnection);
                Data_ID = Convert.ToInt16(com.ExecuteScalar());
                if (validation((int)comStore.SelectedValue, (int)comStorePlace.SelectedValue))
                {
                    query = "insert into open_storage_account (Data_ID,Quantity,Store_ID,Store_Place_ID,Date,Note) values (@Data_ID,@Quantity,@Store_ID,@Store_Place_ID,@Date,@Note)";
                    com = new MySqlCommand(query, dbconnection);
                    com.Parameters.Add("@Data_ID", MySqlDbType.Int16);
                    com.Parameters["@Data_ID"].Value = Data_ID;
                    com.Parameters.Add("@Quantity", MySqlDbType.Decimal);
                    com.Parameters["@Quantity"].Value = txtTotalMeter.Text;
                    com.Parameters.Add("@Store_ID", MySqlDbType.Int16);
                    com.Parameters["@Store_ID"].Value = comStore.SelectedValue;
                    com.Parameters.Add("@Store_Place_ID", MySqlDbType.Int16);
                    com.Parameters["@Store_Place_ID"].Value = comStorePlace.SelectedValue;
                    com.Parameters.Add("@Date", MySqlDbType.Date, 0);
                    com.Parameters["@Date"].Value = dateTimePicker1.Value;
                    com.Parameters.Add("@Note", MySqlDbType.VarChar);
                    com.Parameters["@Note"].Value = txtNote.Text;
                    com.ExecuteNonQuery();

                    query = "insert into Storage (Store_ID,Type,Storage_Date,Data_ID,Store_Place_ID,Total_Meters,Note) values (@Store_ID,@Type,@Date,@Data_ID,@PlaceOfStore,@TotalOfMeters,@Note)";
                    com = new MySqlCommand(query, dbconnection);
                    com.Parameters.Add("@Store_ID", MySqlDbType.Int16);
                    com.Parameters["@Store_ID"].Value = comStore.SelectedValue;
                    com.Parameters.Add("@Type", MySqlDbType.VarChar);
                    com.Parameters["@Type"].Value = "بند";
                    com.Parameters.Add("@Date", MySqlDbType.Date, 0);
                    com.Parameters["@Date"].Value = dateTimePicker1.Value;
                    com.Parameters.Add("@Data_ID", MySqlDbType.Int16);
                    com.Parameters["@Data_ID"].Value = Data_ID;
                    com.Parameters.Add("@PlaceOfStore", MySqlDbType.Int16);
                    com.Parameters["@PlaceOfStore"].Value = comStorePlace.SelectedValue;
                    com.Parameters.Add("@TotalOfMeters", MySqlDbType.Decimal);
                    com.Parameters["@TotalOfMeters"].Value = txtTotalMeter.Text;
                    com.Parameters.Add("@Note", MySqlDbType.VarChar);
                    com.Parameters["@Note"].Value = txtNote.Text;
                    com.ExecuteNonQuery();
                    MessageBox.Show("Add success");
                }
                else
                {
                    MessageBox.Show("هذا البند مضاف فعلا");
                }
            }
            else
            {
                MessageBox.Show("you must fill all fields please");
                dbconnection.Close();
                return;
            }

        }
        public DataTable createDataTable()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add("Data_ID", typeof(int));
            dt.Columns.Add("Store_ID", typeof(int));
            dt.Columns.Add("Store_Place_ID", typeof(int));
            dt.Columns.Add("كود", typeof(string));
            dt.Columns.Add("البند", typeof(string));
            dt.Columns.Add("رصيد البند", typeof(double));
            dt.Columns.Add("ملاحظة", typeof(string));
            dt.Columns.Add("التاريخ", typeof(DateTime));
           
            return dt;
        }
        public void add2GridView(DataTable dt, DataRowView row, string [] re)
        {
            double quantity;
            DateTime date = new DateTime();
            if (re[0] == null)
            {
                quantity = Convert.ToDouble(txtTotalMeter.Text);
            }
            else
            {
                quantity = Convert.ToDouble(re[0]);
            }
            if (re[1] == null)
            {
                date = dateTimePicker1.Value;
            }
            else
            {
                date =Convert.ToDateTime(re[1]);
            }
            dt.Rows.Add(new object[] {
                Convert.ToInt16(row[0].ToString()),
                comStore.SelectedValue,
                 getStore_Place_ID((int)comStore.SelectedValue),
                row[1].ToString(),
                row[2].ToString(),
                quantity,
                txtNote.Text,
                date
            });
         
            gridControl2.DataSource = dt;
            gridView2.Columns[0].Visible = false;
            gridView2.Columns[1].Visible = false;
            gridView2.Columns[2].Visible = false;
            gridView2.Columns[3].OptionsColumn.AllowEdit = false;
            gridView2.Columns[4].OptionsColumn.AllowEdit = false;
            gridView2.Columns[6].OptionsColumn.AllowEdit = false;
            gridView2.BestFitColumns();
        }
        public int getStore_Place_ID(int Store_ID)
        {
            dbconnection.Close();
            dbconnection.Open();
            string query = "select Store_Place_ID from store_places inner join store on store_places.Store_ID=store.Store_ID where store_places.Store_ID=" + Store_ID + " limit 1";
            MySqlCommand com = new MySqlCommand(query, dbconnection);
            int Store_Place_ID = Convert.ToInt16(com.ExecuteScalar());

            return Store_Place_ID;
        }
        public string [] getOldValueIfExist(int Data_ID)
        {
            string [] re =new string[2];
            bool f = false;
            string query = "select Quantity,Date from open_storage_account where Data_ID=" + Data_ID+" and Store_ID="+comStore.SelectedValue;
            MySqlCommand com = new MySqlCommand(query, dbconnection);
            MySqlDataReader dr = com.ExecuteReader();
           while(dr.Read())
            { 
                re[0] =dr[0].ToString();
                re[1] = dr[1].ToString();
                f = true;
                ListOfDataIDs.Add(Data_ID);
            }
           if(f)
            MessageBox.Show("البند مضاف من قبل");           
            return re;
        }
        public bool IsEdited(int DataID)
        {
            for (int i = 0; i < ListOfEditDataIDs.Count; i++)
            {
                if (DataID == ListOfEditDataIDs[i])
                    return true;
            }
            return false;
        }
        public bool IsSaved(int DataID)
        {
            for (int i = 0; i < ListOfSavedDataIDs.Count; i++)
            {
                if (DataID == ListOfSavedDataIDs[i])
                    return true;
            }
            return false;
        }
        public bool IsExistInGridView(string c)
        {
            for (int i = 0; i < gridView2.DataRowCount; i++)
            {
                DataRowView r = (DataRowView)gridView2.GetRow(i);
                if (r[3].ToString()==c)
                {
                    // gridView2.MoveBy(i);
                    int x = gridView2.GetSelectedRows()[0];
                    if(x!=0)
                        gridView2.FocusedRowHandle = 0;
                    else
                        gridView2.FocusedRowHandle = gridView2.DataRowCount-1;
                    gridView2.FocusedRowHandle = i;
                    ListOfDataIDs.Add((int)r[0]);
                    return true;
                }
            }
            return false;
        }
        public bool validation()
        {
            if (code.Length != 20)
                labcode.Visible = true;
            else
                labcode.Visible = false;
            if (txtTotalMeter.Text == "")
                labQuantity.Visible = true;
            else
                labQuantity.Visible = false;
            if (comStore.Text == "")
                labStore.Visible = true;
            else
                labStore.Visible = false;
            if (comStorePlace.Text == "")
                labStorePlace.Visible = true;
            else
                labStorePlace.Visible = false;
            if (code.Length == 20 && txtTotalMeter.Text != "" && comStore.Text != "" && comStorePlace.Text != "")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static DialogResult InputBox(string title, string promptText, ref string value)
        {
            Form form = new Form();
            Label label = new Label();
            TextBox textBox = new TextBox();
            Button buttonOk = new Button();
            Button buttonCancel = new Button();

            form.Text = title;
            label.Text = promptText;
            textBox.Text = value;

            buttonOk.Text = "OK";
            buttonCancel.Text = "Cancel";
            buttonOk.DialogResult = DialogResult.OK;
            buttonCancel.DialogResult = DialogResult.Cancel;

            label.SetBounds(9, 20, 372, 13);
            textBox.SetBounds(12, 36, 372, 20);
            buttonOk.SetBounds(228, 72, 75, 23);
            buttonCancel.SetBounds(309, 72, 75, 23);

            label.AutoSize = true;
            textBox.Anchor = textBox.Anchor | AnchorStyles.Right;
            buttonOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;

            form.ClientSize = new Size(396, 107);
            form.Controls.AddRange(new Control[] { label, textBox, buttonOk, buttonCancel });
            form.ClientSize = new Size(Math.Max(300, label.Right + 10), form.ClientSize.Height);
            form.FormBorderStyle = FormBorderStyle.FixedDialog;
            form.StartPosition = FormStartPosition.CenterScreen;
            form.MinimizeBox = false;
            form.MaximizeBox = false;
            form.AcceptButton = buttonOk;
            form.CancelButton = buttonCancel;

            DialogResult dialogResult = form.ShowDialog();
            value = textBox.Text;
            return dialogResult;
        }
      
    }

}

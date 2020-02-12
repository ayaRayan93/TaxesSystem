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
    public partial class StorageTaswayaAddingUpdate : Form
    {
        MySqlConnection dbconnection, dbconnectionReader, connectionReader, connectionReader1, connectionReader2;
        bool loaded = false;
        bool flag = false;
        double noMeter = 0;
        int TaswayaAdding_ID = 0;
        XtraTabControl xtraTabControlStoresContent;
        int Data_ID=-1;
        string code = "";
        MainForm mainForm;
        DataTable mdt=null;
        DataRowView mRow = null;
        int perNum;
        public StorageTaswayaAddingUpdate(int perNum, MainForm mainForm,XtraTabControl xtraTabControlStoresContent)
        {
            try
            {
                InitializeComponent();
                dbconnection = new MySqlConnection(connection.connectionString);
                connectionReader = new MySqlConnection(connection.connectionString);
                dbconnectionReader = new MySqlConnection(connection.connectionString);
                connectionReader1 = new MySqlConnection(connection.connectionString);
                connectionReader2 = new MySqlConnection(connection.connectionString);
                this.mainForm = mainForm;
                this.xtraTabControlStoresContent = xtraTabControlStoresContent;
                this.perNum = perNum;
                labPermissionNum.Text = perNum.ToString();
           
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
                mdt = new DataTable();
                mdt = createDataTable();
                dbconnection.Open();
                permissionData();
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

                    if (txtType.Text != "1" && txtType.Text != "2" && txtType.Text != "9")
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
                        label3.Text = "الصنف";
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
                        label3.Text = "المقاس";
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
                            displayProducts(code);
                            DataRowView row = (DataRowView)(((GridView)gridControl1.MainView).GetRow(((GridView)gridControl1.MainView).GetSelectedRows()[0]));
                            mRow = row;
                            txtTotalMeter.Text = row[4].ToString();
                            txtAddingQuantity.Focus();
                            break;
                        case "txtAddingQuantity":
                            if (btnReport.Enabled)
                            {
                                if (validation())
                                {
                                    if (mRow != null)
                                    {
                                        if (!IsExistInGridView(code))
                                            add2GridView(mdt, mRow);
                                        else
                                            MessageBox.Show("البند المضاف");

                                        clearPart();
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("ادخل كل البيانات المطلوبة");
                                }
                                code = "";
                                txtCodePart1.Focus();

                            }
                            else
                            {
                                MessageBox.Show("اطبع الاذن او قم بانشاء اذن جديد");
                            }
                            break;
                        case "txtNote":
                            txtCodePart1.Focus();
                            break;

                    }
                    
                }
                catch
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
            if (loaded)
            {
                try
                {

                    dbconnection.Open();
                    string query = "select permissionNum from taswayaa_adding_permision order by permissionNum desc limit 1 ";
                    MySqlCommand com = new MySqlCommand(query, dbconnection);
                    if (com.ExecuteScalar() != null)
                    {
                        int x = Convert.ToInt32(com.ExecuteScalar());
                        labPermissionNum.Text = (x + 1).ToString();
                    }
                    else
                    {
                        labPermissionNum.Text = "1";
                    }
                    txtNote.Focus();

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
                    displayProducts("");
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
                    noMeter = Convert.ToDouble(row[3].ToString());
                    txtTotalMeter.Text = noMeter.ToString();
                    Data_ID = Convert.ToInt32(row[0].ToString());
                    code = row[1].ToString();
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
                    if (validation())
                    {
                        if (mRow != null)
                        {
                            add2GridView(mdt, mRow);
                        }
                    }
                    else
                    {
                        MessageBox.Show("ادخل كل البيانات المطلوبة");
                    }
                    code = "";
                    clearPart();
                    txtCodePart1.Focus();
               
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

                comType.Text = "";
                comFactory.Text = "";
                comGroup.Text = "";
                comProduct.Text = "";

                txtType.Text = "";
                txtFactory.Text = "";
                txtGroup.Text = "";
                txtProduct.Text = "";
                
                
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
                    if (flag)
                    {
                        if (TaswayaAdding_ID != 0)
                            mainForm.bindReportStorageForm(gridControl2, "أذن تسوية اضافة \n" + TaswayaAdding_ID + "\n" + comStore.Text+"\n\n"+txtNote.Text);
                        flag = false;
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

        private void gridView2_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            try
            {
                if (e.Column.Name == "colالكميةالمضافة")
                {
                    GridView view = (GridView)sender;
                    DataRow dataRow = view.GetFocusedDataRow();
                    dbconnection.Close();
                    dbconnection.Open();
                    double currentQuantity= MainForm.currentItemQuantity((int)dataRow[0],(int)comStore.SelectedValue,dbconnection);
                    double taswayaQuantit = Convert.ToDouble(dataRow["الكمية بعد الاضافة"].ToString());
                    if (currentQuantity == taswayaQuantit)
                    {
                        dbconnection.Close();
                        double addingQuantity = Convert.ToDouble(dataRow["الكمية المضافة"].ToString());
                        double totalBeforAdding = Convert.ToDouble(dataRow["الكمية قبل الاضافة"].ToString());
                        view.SetRowCellValue(view.GetSelectedRows()[0], "الكمية بعد الاضافة", totalBeforAdding + addingQuantity);
                    }
                    else
                    {
                        MessageBox.Show("لا يمكن التعديل البند تم استخدامه");
                    }
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
                dbconnection.Close();
                dbconnection.Open();
                if (gridView2.RowCount > 0)
                {
                    update2DB();
                }
                else
                {
                    MessageBox.Show("ادخل البنود المراد حفظها");
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
            comStore.Text = "";
            gridControl1.DataSource = null;
            gridControl2.DataSource = null;
        }
        public void clearPart()
        {
            txtCodePart1.Text = txtCodePart2.Text = txtCodePart3.Text = txtCodePart4.Text = txtCodePart5.Text = "";
            txtTotalMeter.Text = "";
            txtAddingQuantity.Text = "";
            labVcode.Visible = true;
            labVaddingMeter.Visible = true;
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
        public void displayProducts(string code)
        {
            try
            {
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
                string query1 = "";
                if (comStore.Text != "")
                {
                    query1 += " and storage.Store_ID=" + comStore.SelectedValue;
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

                string qq = "";
                if (txtType.Text == "1" || txtType.Text == "2" || txtType.Text == "9")
                {
                    qq = "select data.Data_ID, data.Code as 'كود'," + itemName + ",sum(storage.Total_Meters) as 'رصيد البند' from storage INNER JOIN store on storage.Store_ID=store.Store_ID INNER JOIN store_places on storage.Store_Place_ID=store_places.Store_Place_ID  INNER JOIN data  ON storage.Data_ID = data.Data_ID INNER JOIN type ON type.Type_ID = data.Type_ID INNER JOIN product ON product.Product_ID = data.Product_ID INNER JOIN factory ON data.Factory_ID = factory.Factory_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID LEFT outer JOIN color ON data.Color_ID = color.Color_ID LEFT outer  JOIN size ON data.Size_ID = size.Size_ID LEFT outer  JOIN sort ON data.Sort_ID = sort.Sort_ID  where data.Type_ID IN(" + q1 + ") and  data.Factory_ID  IN(" + q2 + ") and  data.Size_ID  IN(" + q5 + ") and data.Group_ID IN (" + q4 + ") " + query1 + q + " group by storage.Store_ID,storage.Data_ID ";

                }
                else
                {
                    qq = "select data.Data_ID, data.Code as 'كود'," + itemName + ",sum(storage.Total_Meters) as 'رصيد البند' from storage INNER JOIN store on storage.Store_ID=store.Store_ID INNER JOIN store_places on storage.Store_Place_ID=store_places.Store_Place_ID  INNER JOIN data  ON storage.Data_ID = data.Data_ID INNER JOIN type ON type.Type_ID = data.Type_ID INNER JOIN product ON product.Product_ID = data.Product_ID INNER JOIN factory ON data.Factory_ID = factory.Factory_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID LEFT outer JOIN color ON data.Color_ID = color.Color_ID LEFT outer  JOIN size ON data.Size_ID = size.Size_ID LEFT outer  JOIN sort ON data.Sort_ID = sort.Sort_ID  where data.Type_ID IN(" + q1 + ") and  data.Factory_ID  IN(" + q2 + ") and  data.Product_ID  IN(" + q3 + ") and data.Group_ID IN (" + q4 + ") " + query1 + q + " group by storage.Store_ID,storage.Data_ID ";
                }

                MySqlDataAdapter da = new MySqlDataAdapter(qq, dbconnection);
                DataTable dt = new DataTable();
                da.Fill(dt);
               
                gridControl1.DataSource = dt;
                gridView1.Columns[0].Visible = false;
                gridView1.BestFitColumns();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        public void displayCode(string code)
        {
            char[] arrCode = code.ToCharArray();
            txtCodePart1.Text =Convert.ToInt32(arrCode[0].ToString() + arrCode[1].ToString() + arrCode[2].ToString() + arrCode[3].ToString()) + "";
            txtCodePart2.Text = Convert.ToInt32(arrCode[4].ToString() + arrCode[5].ToString() + arrCode[6].ToString() + arrCode[7].ToString() )+ "";
            txtCodePart3.Text = Convert.ToInt32(arrCode[8].ToString() + arrCode[9].ToString() + arrCode[10].ToString() + arrCode[11].ToString()) + "";
            txtCodePart4.Text = Convert.ToInt32(arrCode[12].ToString() + arrCode[13].ToString() + arrCode[14].ToString() + arrCode[15].ToString() )+ "";
            txtCodePart5.Text = "" + Convert.ToInt32(arrCode[16].ToString() + arrCode[17].ToString() + arrCode[18].ToString() + arrCode[19].ToString());
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
        public void update2DB()
        {
            mdt =(DataTable) gridControl2.DataSource;
            string query = "update  taswayaa_adding_permision set Store_ID=@Store_ID,Date=@Date,Note=@Note where PermissionNum="+labPermissionNum.Text;
            MySqlCommand com = new MySqlCommand(query, dbconnection);
            com.Parameters.Add("@Store_ID", MySqlDbType.Int16);
            com.Parameters["@Store_ID"].Value = comStore.SelectedValue;
            com.Parameters.Add("@Date", MySqlDbType.Date, 0);
            com.Parameters["@Date"].Value = dateTimePicker1.Value;
            com.Parameters.Add("@Note", MySqlDbType.VarChar);
            com.Parameters["@Note"].Value = txtNote.Text;
            dbconnection.Close();
            dbconnection.Open();
            com.ExecuteNonQuery();
            DecreaseProductQuantity(Convert.ToInt32(labPermissionNum.Text));
            query = "delete from addstorage where PermissionNum=" + labPermissionNum.Text;
            dbconnection.Close();
            dbconnection.Open();
            com = new MySqlCommand(query, dbconnection);
            com.ExecuteNonQuery();
          
            for (int i = 0; i < gridView2.RowCount; i++)
            {
                DataRow d = gridView2.GetDataRow(i);
                query = "insert into addstorage (PermissionNum,Data_ID,Store_Place_ID,CurrentQuantity,AddingQuantity,QuantityAfterAdding,Note) values (@PermissionNum,@Data_ID,@Store_Place_ID,@CurrentQuantity,@AddingQuantity,@QuantityAfterAdding,@Note)";
                com = new MySqlCommand(query, dbconnection);
                com.Parameters.Add("@PermissionNum", MySqlDbType.Int16);
                com.Parameters["@PermissionNum"].Value = labPermissionNum.Text;
                com.Parameters.Add("@Store_Place_ID", MySqlDbType.Int16);
                com.Parameters["@Store_Place_ID"].Value = getStore_Place_ID((int)comStore.SelectedValue);
                com.Parameters.Add("@Data_ID", MySqlDbType.Int16);
                com.Parameters["@Data_ID"].Value = d[0];
                com.Parameters.Add("@CurrentQuantity", MySqlDbType.Decimal);
                com.Parameters["@CurrentQuantity"].Value = Convert.ToDouble(d[3]);
                com.Parameters.Add("@AddingQuantity", MySqlDbType.Decimal);
                com.Parameters["@AddingQuantity"].Value = Convert.ToDouble(d[4]);
                com.Parameters.Add("@QuantityAfterAdding", MySqlDbType.Decimal);
                com.Parameters["@QuantityAfterAdding"].Value = Convert.ToDouble(d[5]);
                com.Parameters.Add("@Note", MySqlDbType.VarChar);
                com.Parameters["@Note"].Value = d[6];

                com.ExecuteNonQuery();
            }
            IncreaseProductQuantity(Convert.ToInt32(labPermissionNum.Text));
            dbconnection.Close();
            dbconnection.Open();
            UserControl.ItemRecord("taswayaa_adding_permision", "اضافة", Convert.ToInt32(labPermissionNum.Text), DateTime.Now, "", dbconnection);
            MessageBox.Show("تم الحفظ");

            btnReport.Enabled = true;
            flag = true;
        
          
        }
        public DataTable createDataTable()
        {
            DataTable dt = new DataTable();
            
            dt.Columns.Add("Data_ID", typeof(int));
            dt.Columns.Add("كود", typeof(string));
            dt.Columns.Add("البند", typeof(string));
            dt.Columns.Add("الكمية قبل الاضافة", typeof(double));
            dt.Columns.Add("الكمية المضافة", typeof(double));
            dt.Columns.Add("الكمية بعد الاضافة", typeof(double));
            dt.Columns.Add("ملاحظة", typeof(string));

            return dt;
        }
        public void add2GridView(DataTable dt, DataRowView row)
        {
            dt.Rows.Add(new object[] {
                Convert.ToInt32(row[0].ToString()),
                row[1].ToString(),
                row[2].ToString(),
                Convert.ToDouble(Convert.ToDouble(txtAddingQuantity.Text)),
                Convert.ToDouble(row[3].ToString())+Convert.ToDouble(txtAddingQuantity.Text),
                txtItemNote.Text
            });
            gridControl2.DataSource = dt;
            gridView2.Columns[0].Visible = false;
            gridView2.Columns[1].OptionsColumn.AllowEdit = false;
            gridView2.Columns[2].OptionsColumn.AllowEdit = false;
            gridView2.BestFitColumns();
        }
        public int getStore_Place_ID(int Store_ID)
        {
            dbconnection.Close();
            dbconnection.Open();
            string query = "select Store_Place_ID from store_places inner join store on store_places.Store_ID=store.Store_ID where store_places.Store_ID="+Store_ID+" limit 1";
            MySqlCommand com = new MySqlCommand(query, dbconnection);
            int Store_Place_ID = Convert.ToInt32(com.ExecuteScalar());
           
            return Store_Place_ID;
        }
        public bool validation()
        {
            if (code.Length != 20)
                labVcode.Visible = true;
            else
                labVcode.Visible = false;
            if (txtAddingQuantity.Text == "")
                labVaddingMeter.Visible = true;
            else
                labVaddingMeter.Visible = false;
            if (comStore.Text == "")
                labStore.Visible = true;
            else
                labStore.Visible = false;
            if (txtNote.Text == "")
                labVnote.Visible = true;
            else
                labVnote.Visible = false;
            if (code.Length == 20 && txtAddingQuantity.Text != "" && comStore.Text != "" && txtNote.Text != "")
            {
                return true;
            }
            else
            {
                return false;
            }
        }    
        public bool IsExistInGridView(string c)
        {
            for (int i = 0; i < gridView2.DataRowCount; i++)
            {
                DataRowView r = (DataRowView)gridView2.GetRow(i);
                if (r[1].ToString() == c)
                {
                    int x = gridView2.GetSelectedRows()[0];
                    if (x != 0)
                        gridView2.FocusedRowHandle = 0;
                    else
                        gridView2.FocusedRowHandle = gridView2.DataRowCount - 1;
                    gridView2.FocusedRowHandle = i;
                    return true;
                }
            }
            return false;
        }
        public void permissionData()
        {
            string query = "select * from taswayaa_adding_permision where PermissionNum=" + labPermissionNum.Text;
            MySqlCommand com = new MySqlCommand(query,dbconnection);
            MySqlDataReader dr = com.ExecuteReader();
            while (dr.Read())
            {
                loaded = false;
                dateTimePicker1.Text = dr[3].ToString();
                txtNote.Text= dr[4].ToString();
                comStore.SelectedValue = dr[2].ToString();
             
                loaded = true;
            }
            dr.Close();
            string itemName = "concat( product.Product_Name,' ',type.Type_Name,' ',factory.Factory_Name,' ',groupo.Group_Name,' ' ,COALESCE(color.Color_Name,''),' ',COALESCE(size.Size_Value,''),' ',COALESCE(sort.Sort_Value,''),' ',COALESCE(data.Classification,''),' ',COALESCE(data.Description,''))as 'البند'";
            query = "SELECT data.Data_ID, data.Code as 'الكود' ," + itemName + ",CurrentQuantity as 'الكمية قبل الاضافة',AddingQuantity as 'الكمية المضافة',QuantityAfterAdding as 'الكمية بعد الاضافة',addstorage.Note as 'ملاحظة' from data INNER JOIN type ON type.Type_ID = data.Type_ID INNER JOIN product ON product.Product_ID = data.Product_ID INNER JOIN factory ON data.Factory_ID = factory.Factory_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID LEFT outer JOIN color ON data.Color_ID = color.Color_ID LEFT outer  JOIN size ON data.Size_ID = size.Size_ID LEFT outer  JOIN sort ON data.Sort_ID = sort.Sort_ID  inner join addstorage on addstorage.Data_ID=data.Data_ID inner join taswayaa_adding_permision on taswayaa_adding_permision.PermissionNum=addstorage.PermissionNum  WHERE taswayaa_adding_permision.PermissionNum=" + labPermissionNum.Text;
            MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
            DataTable dt = new DataTable();
            da.Fill(dt);
            mdt = dt;
            gridControl2.DataSource = dt;
            gridView2.Columns[0].Visible = false;
            gridView2.Columns[1].OptionsColumn.AllowEdit = false;
            gridView2.Columns[2].OptionsColumn.AllowEdit = false;
            gridView2.Columns[3].OptionsColumn.AllowEdit = false;
            gridView2.BestFitColumns();
            if (dt != null)
                btnReport.Enabled = true;
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
                if (txtType.Text == "2" || txtType.Text == "1")
                    query2 = "select * from groupo where Factory_ID=" + -1;
                else
                    query2 = "select * from groupo where Factory_ID=" + -Convert.ToInt32(txtType.Text) + " and Type_ID=" + txtType.Text;

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
                    q = " and Factory_ID = " + txtFactory.Text;
                }
                query = "select * from groupo where Type_ID=" + txtType.Text + q;
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
                if (comGroup.Text != "" || comFactory.Text != "" || comType.Text != "")
                {
                    if (txtType.Text != "1" && txtType.Text != "2" && txtType.Text != "9")
                    {
                        string supQuery = "";

                        supQuery = " product.Type_ID=" + txtType.Text + "";
                        if (comFactory.Text != "")
                        {
                            supQuery += " and product_factory_group.Factory_ID=" + txtFactory.Text + "";
                        }
                        if (comGroup.Text != "")
                        {
                            supQuery += " and product_factory_group.Group_ID=" + txtGroup.Text + "";
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
                        string query = "select * from size inner join type_factory on size.Factory_ID=type_factory.Factory_ID where type_factory.Type_ID=" + txtType.Text + supQuery;
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
                string query = "select * from size size inner join type_factory on size.Factory_ID=type_factory.Factory_ID where Type_ID=" + txtType.Text + " Factory_ID=" + comFactory.SelectedValue;
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

        public void IncreaseProductQuantity(int billNumber)
        {
            dbconnection.Close();
            dbconnectionReader.Close();
            connectionReader1.Close();
            dbconnection.Open();
            dbconnectionReader.Open();
            connectionReader1.Open();
            string q;
            int id;
            //bool flag = false;
            double storageQ, productQ;

            string query = "select Data_ID,AddingQuantity from addstorage where PermissionNum=" + billNumber;
            MySqlCommand com = new MySqlCommand(query, dbconnection);
            MySqlDataReader dr = com.ExecuteReader();

            while (dr.Read())
            {
                string query2 = "select Storage_ID,Total_Meters from storage where Data_ID=" + dr["Data_ID"].ToString() + " and Type='بند' and Store_ID="+comStore.SelectedValue;
                MySqlCommand com2 = new MySqlCommand(query2, dbconnectionReader);
                MySqlDataReader dr2 = com2.ExecuteReader();
                while (dr2.Read())
                {
                    storageQ = Convert.ToDouble(dr2["Total_Meters"]);
                    productQ = Convert.ToDouble(dr["AddingQuantity"]);

                    storageQ += productQ;
                    id = Convert.ToInt32(dr2["Storage_ID"]);
                    q = "update storage set Total_Meters=" + storageQ + " where Storage_ID=" + id;
                    MySqlCommand comm = new MySqlCommand(q, connectionReader1);
                    comm.ExecuteNonQuery();
                    flag = true;
                    break;
                }
                dr2.Close();


            }
            dr.Close();

            dbconnectionReader.Close();
            dbconnection.Close();
            connectionReader1.Close();
        }
        public void DecreaseProductQuantity(int billNumber)
        {
            dbconnection.Close();
            dbconnectionReader.Close();
            dbconnection.Open();
            dbconnectionReader.Open();
            connectionReader1.Close();
            connectionReader1.Open();
            string q;
            int id;
            //bool flag = false;
            double storageQ, productQ;

            string query = "select Data_ID,AddingQuantity from addstorage where PermissionNum=" + billNumber;
            MySqlCommand com = new MySqlCommand(query, dbconnection);
            MySqlDataReader dr = com.ExecuteReader();

            while (dr.Read())
            {
                string query2 = "select Storage_ID,Total_Meters from storage where Data_ID=" + dr["Data_ID"].ToString() + " and Type='بند'  and Store_ID="+comStore.SelectedValue;
                MySqlCommand com2 = new MySqlCommand(query2, dbconnectionReader);
                MySqlDataReader dr2 = com2.ExecuteReader();
                while (dr2.Read())
                {
                    storageQ = Convert.ToDouble(dr2["Total_Meters"]);
                    productQ = Convert.ToDouble(dr["AddingQuantity"]);

                    storageQ -= productQ;
                    id = Convert.ToInt32(dr2["Storage_ID"]);
                    q = "update storage set Total_Meters=" + storageQ + " where Storage_ID=" + id;
                    MySqlCommand comm = new MySqlCommand(q, connectionReader1);
                    comm.ExecuteNonQuery();
                    flag = true;
                    break;
                }
                dr2.Close();
                
            }
            dr.Close();

            connectionReader1.Close();
            dbconnectionReader.Close();
            dbconnection.Close();
        }
  

    }

}

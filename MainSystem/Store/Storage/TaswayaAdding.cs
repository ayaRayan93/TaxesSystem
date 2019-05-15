using DevExpress.XtraEditors.Repository;
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
    public partial class TaswayaAdding : Form
    {
        MySqlConnection dbconnection;
        MainForm storeMainForm = null;
        bool loaded = false;
        bool load = false;
        //bool factoryFlage = false;
        //bool groupFlage = false;
        //bool flagProduct = false;
        //bool flag = false;
        //DataGridViewRow row1;
        public  Product_Record product_Record = null;
        public Product_Update product_Update = null;
        TipImage tipImage=null;
        public TaswayaAdding(MainForm storeMainForm)
        {
            try
            {
                InitializeComponent();
                dbconnection = new MySqlConnection(connection.connectionString);
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

                string query = "select * from store";
                MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                DataTable dt = new DataTable();
                da.Fill(dt);
                comStore.DataSource = dt;
                comStore.DisplayMember = dt.Columns["Store_Name"].ToString();
                comStore.ValueMember = dt.Columns["Store_ID"].ToString();
                comStore.Text = "";
                
                loaded = true;

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
                dbconnection.Open();
                displayProducts();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
         
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                storeMainForm.bindTaswaySubtractStorageForm();
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
               storeMainForm.bindTaswayAddingStorageForm();
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
                DataRowView a = (DataRowView)(((GridView)dataGridView1.MainView).GetRow(((GridView)dataGridView1.MainView).GetSelectedRows()[0]));
                storeMainForm.bindUpdateTaswayaAddingForm(Convert.ToInt16(a[1]));
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
                storeMainForm.bindReportStorageForm(dataGridView1,"تقرير كميات البنود");
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
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txtPermissionStore_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    displayProducts();
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
            try
            {
              
                load = false;
                string subQuery = " date(taswayaa_adding_permision.Date) between '" + dateTimeFrom.Value.ToString("yyyy-MM-dd") + "' and '" + dateTimeTo.Value.ToString("yyyy-MM-dd") + "'";
                if (comStore.Text != "")
                {
                    subQuery += " and taswayaa_adding_permision.Store_ID=" + comStore.SelectedValue;
                }

                if (txtPermissionStore.Text!="")
                {                              
                    subQuery = " taswayaa_adding_permision.PermissionNum=" + txtPermissionStore.Text;
                }

                string query = "SELECT TaswayaAdding_ID, taswayaa_adding_permision.PermissionNum as 'رقم الأذن', Store_Name as 'المخزن',taswayaa_adding_permision.Date as 'التاريخ',Note as 'الملاحظة' FROM taswayaa_adding_permision INNER JOIN store ON store.Store_ID = taswayaa_adding_permision.Store_ID   WHERE " + subQuery;
                MySqlDataAdapter adapterSets = new MySqlDataAdapter(query, dbconnection);
                string itemName = "concat( product.Product_Name,' ',type.Type_Name,' ',factory.Factory_Name,' ',groupo.Group_Name,' ' ,COALESCE(color.Color_Name,''),' ',COALESCE(size.Size_Value,''),' ',COALESCE(sort.Sort_Value,''),' ',COALESCE(data.Classification,''),' ',COALESCE(data.Description,''))as 'البند'";
                query = "SELECT addstorage.PermissionNum as 'رقم الأذن',data.Code as 'الكود' ," + itemName + ",AddingQuantity as 'الكمية المضافة',QuantityAfterAdding as 'الكمية بعد الاضافة' from data INNER JOIN type ON type.Type_ID = data.Type_ID INNER JOIN product ON product.Product_ID = data.Product_ID INNER JOIN factory ON data.Factory_ID = factory.Factory_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID LEFT outer JOIN color ON data.Color_ID = color.Color_ID LEFT outer  JOIN size ON data.Size_ID = size.Size_ID LEFT outer  JOIN sort ON data.Sort_ID = sort.Sort_ID  inner join addstorage on addstorage.Data_ID=data.Data_ID inner join taswayaa_adding_permision on taswayaa_adding_permision.PermissionNum=addstorage.PermissionNum  WHERE " + subQuery;

                MySqlDataAdapter AdapterProducts = new MySqlDataAdapter(query, dbconnection);
                DataSet dataSet11 = new DataSet();

                //Create DataTable objects for representing database's tables 
                adapterSets.Fill(dataSet11, "taswayaa_adding_permision");
                AdapterProducts.Fill(dataSet11, "addstorage");

                //Set up a master-detail relationship between the DataTables 
                DataColumn keyColumn = dataSet11.Tables["taswayaa_adding_permision"].Columns["رقم الأذن"];
                DataColumn foreignKeyColumn = dataSet11.Tables["addstorage"].Columns["رقم الأذن"];
                dataSet11.Relations.Add("بنود الأذن", keyColumn, foreignKeyColumn);

                //Bind the grid control to the data source 
                dataGridView1.DataSource = dataSet11.Tables["taswayaa_adding_permision"];
                gridView2.Columns[0].Visible = false;
                load = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        
        }

    
    }
}

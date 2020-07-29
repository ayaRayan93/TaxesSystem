﻿using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Card;
using DevExpress.XtraGrid.Views.Grid;
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
    public partial class Ataqm : Form
    {
        MySqlConnection dbconnection;
        MainForm storeMainForm=null;
        bool loaded = false;
        //DataGridViewRow row1;
        public static SetRecord setRecord = null;
        public static SetUpdate setUpdate = null;
        public Ataqm(MainForm StoreMainForm)
        {
            try
            {
                InitializeComponent();
                this.storeMainForm = StoreMainForm;
                dbconnection = new MySqlConnection(connection.connectionString);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
           
        }

        private void Ataqm_Load(object sender, EventArgs e)
        {
            try
            {
                loadDataToBox();
                loaded = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
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
                        case "comFactory":
                            txtFactory.Text = comFactory.SelectedValue.ToString();
                      
                            FilterSet();
                            break;
                        case "comType":
                            txtType.Text = comType.SelectedValue.ToString();
                            FilterFactoryAndSet();
                            break;
                       
                        case "comSets":
                           
                            txtSetsID.Text = comSets.SelectedValue.ToString();
                            
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
                            case "txtFactory":
                                query = "select Factory_Name from sets where Factory_ID='" + txtFactory.Text + "'";
                                com = new MySqlCommand(query, dbconnection);
                                if (com.ExecuteScalar() != null)
                                {
                                    Name = (string)com.ExecuteScalar();
                                    comFactory.Text = Name;
                                    FilterSet();
                                }
                                else
                                {
                                    MessageBox.Show("there is no item with this id");
                                    dbconnection.Close();
                                    return;
                                }
                                break;
                            case "txtType":
                                query = "select Type_Name from sets where Type_ID='" + txtType.Text + "'";
                                com = new MySqlCommand(query, dbconnection);
                                if (com.ExecuteScalar() != null)
                                {
                                    Name = (string)com.ExecuteScalar();
                                    comType.Text = Name;
                                    txtFactory.Focus();
                                    FilterFactoryAndSet();
                                }
                                else
                                {
                                    MessageBox.Show("there is no item with this id");
                                    dbconnection.Close();
                                    return;
                                }
                                break;

                            case "txtSetsID":

                                query = "select Set_Name from sets where Set_ID='" + txtSetsID.Text + "'";
                                com = new MySqlCommand(query, dbconnection);
                                if (com.ExecuteScalar() != null)
                                {
                                    Name = (string)com.ExecuteScalar();
                                    comSets.Text = Name;

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
                catch
                {
                  //  MessageBox.Show(ex.ToString());
                }
                dbconnection.Close();
            }
        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                DisplayAtaqm();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btnAddNew_Click(object sender, EventArgs e)
        {
            try
            {
              storeMainForm.bindRecordSetForm(this);
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
                DataRowView setRow = (DataRowView)(((GridView)dataGridView1.MainView).GetRow(((GridView)dataGridView1.MainView).GetSelectedRows()[0]));
                storeMainForm.bindUpdateSetForm(setRow,this);
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
                dbconnection.Open();
                DataRowView setRow = (DataRowView)(((GridView)dataGridView1.MainView).GetRow(((GridView)dataGridView1.MainView).GetSelectedRows()[0]));


                if (setRow != null)
                {
                    DialogResult dialogResult = MessageBox.Show("Are you sure you want to delete the item?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (dialogResult == DialogResult.Yes)
                        {

                        deleteSet(Convert.ToInt32(setRow[0].ToString()));
                        
                        string query = "ALTER TABLE sets AUTO_INCREMENT = 1;";
                        MySqlCommand com = new MySqlCommand(query, dbconnection);
                        com.ExecuteNonQuery();

                        UserControl.ItemRecord("sets", "delete",Convert.ToInt32(setRow[0].ToString()), DateTime.Now,"", dbconnection);
                        dbconnection.Close();
                
                        }
                        else if (dialogResult == DialogResult.No)
                        { }
              
                }
                else
                {
                    MessageBox.Show("select row");
                }
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
                storeMainForm.bindReportSetForm(dataGridView1);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                // row1 = dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex];

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                dbconnection.Open();
                DataRowView row1 = (DataRowView)(((GridView)dataGridView1.MainView).GetRow(((GridView)dataGridView1.MainView).GetSelectedRows()[0]));
                string query = "SELECT data.Data_ID,data.Code,product.Product_Name,type.Type_Name,factory.Factory_Name,groupo.Group_Name,color.Color_Name,size.Size_Value,sort.Sort_Value,data.Classification,data.Description,data.Carton from data INNER JOIN type ON type.Type_ID = data.Type_ID INNER JOIN product ON product.Product_ID = data.Product_ID INNER JOIN factory ON data.Factory_ID = factory.Factory_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID LEFT outer JOIN color ON data.Color_ID = color.Color_ID LEFT outer  JOIN size ON data.Size_ID = size.Size_ID LEFT outer  JOIN sort ON data.Sort_ID = sort.Sort_ID where  data.Code in(select set_details.Code from set_details where set_details.Set_ID=" + row1[0].ToString() + ") group by data.Code";
                MySqlDataAdapter adapter = new MySqlDataAdapter(query, dbconnection);
                DataSet dataSet = new DataSet();
                adapter.Fill(dataSet);
                dataGridView1.DataSource = dataSet.Tables[0];
            }
            catch
            {
               // MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }

        //function
        public void DisplayAtaqm()
        {
            try
            {
                dbconnection.Open();
                loaded = false;
                string q1, q2, q3;
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
                if (txtSetsID.Text == "")
                {
                    q3 = "select Set_ID from sets";
                }
                else
                {
                    q3 = txtSetsID.Text;
                }
       

                string query = "SELECT sets.Set_ID as 'كود الطقم',Set_Name as 'اسم الطقم',Type_Name as 'النوع',Factory_Name as 'المصنع',Set_Photo as 'صورة' from sets left join set_photo on set_photo.Set_ID=sets.Set_ID INNER JOIN type ON type.Type_ID = sets.Type_ID  INNER JOIN factory ON sets.Factory_ID = factory.Factory_ID  where  sets.Type_ID IN(" + q1 + ") and  sets.Factory_ID  IN(" + q2 + ") and sets.Set_ID IN (" + q3 + ")  order by sets.Set_ID";
              
                MySqlDataAdapter adapterSets = new MySqlDataAdapter(query, dbconnection);             
                query = "SELECT sets.Set_ID as 'كود الطقم',data.Code as 'الكود',set_details.Quantity as 'الكمية',groupo.Group_Name as 'الصنف',color.Color_Name as 'اللون',size.Size_Value as 'المقاس',sort.Sort_Value as 'الفرز',data.Classification as 'التصنيف',data.Description as 'الوصف',data.Carton as 'الكرتنة',data_photo.Photo as 'صورة' from data INNER JOIN type ON type.Type_ID = data.Type_ID INNER JOIN product ON product.Product_ID = data.Product_ID INNER JOIN factory ON data.Factory_ID = factory.Factory_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID LEFT outer JOIN color ON data.Color_ID = color.Color_ID LEFT outer  JOIN size ON data.Size_ID = size.Size_ID LEFT outer  JOIN sort ON data.Sort_ID = sort.Sort_ID INNER JOIN set_details on data.Data_ID=set_details.Data_ID INNER JOIN sets on sets.Set_ID=set_details.Set_ID left join data_photo on data_photo.Data_ID=data.Data_ID where sets.Type_ID IN(" + q1 + ") and  sets.Factory_ID  IN(" + q2 + ") and sets.Set_ID IN (" + q3 + ") order by data.Code";
                MySqlDataAdapter AdapterProducts = new MySqlDataAdapter(query, dbconnection);
                DataSet dataSet11 = new DataSet();

                //Create DataTable objects for representing database's tables 
                adapterSets.Fill(dataSet11, "Sets");
                AdapterProducts.Fill(dataSet11, "Products");
           
                //Set up a master-detail relationship between the DataTables 
                DataColumn keyColumn = dataSet11.Tables["Sets"].Columns["كود الطقم"];
                DataColumn foreignKeyColumn = dataSet11.Tables["Products"].Columns["كود الطقم"];
                dataSet11.Relations.Add("بنود الطقم", keyColumn, foreignKeyColumn);
           
                //Bind the grid control to the data source 
                dataGridView1.DataSource = dataSet11.Tables["Sets"];

                loaded = true;               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        
    }

        public void deleteSet(int id)
        {
            String query = "delete from sets where Set_ID="+id;
            MySqlCommand com = new MySqlCommand(query,dbconnection);
            com.ExecuteNonQuery();
            query = "delete from set_details where Set_ID=" + id;
            com = new MySqlCommand(query, dbconnection);
            com.ExecuteNonQuery();
        }
        public void loadDataToBox()
        {
            string query = "select distinct Factory_Name,sets.Factory_ID from sets inner join Factory on sets.Factory_ID=Factory.Factory_ID";
            MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
            DataTable dt = new DataTable();
            da.Fill(dt);
            comFactory.DataSource = dt;
            comFactory.DisplayMember = dt.Columns["Factory_Name"].ToString();
            comFactory.ValueMember = dt.Columns["Factory_ID"].ToString();
            comFactory.Text = "";
            txtFactory.Text = "";

            query = "select Set_Name,Set_ID from sets ";
            da = new MySqlDataAdapter(query, dbconnection);
            dt = new DataTable();
            da.Fill(dt);
            comSets.DataSource = dt;
            comSets.DisplayMember = dt.Columns["Set_Name"].ToString();
            comSets.ValueMember = dt.Columns["Set_ID"].ToString();
            comSets.Text = "";
            txtSetsID.Text = "";

            query = "select distinct Type_Name,type.Type_ID from sets inner join type on sets.Type_ID=type.Type_ID";
            da = new MySqlDataAdapter(query, dbconnection);
            dt = new DataTable();
            da.Fill(dt);
            comType.DataSource = dt;
            comType.DisplayMember = dt.Columns["Type_Name"].ToString();
            comType.ValueMember = dt.Columns["Type_ID"].ToString();
            comType.Text = "";
            txtType.Text = "";

        
        }

        private void btnNewChooes_Click(object sender, EventArgs e)
        {
            try
            {
                comType.Text = "";
                comFactory.Text = "";
                comSets.Text = "";
                
                txtType.Text = "";
                txtFactory.Text = "";
                txtSetsID.Text = "";

          
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void FilterFactoryAndSet()
        {
            if (txtType.Text != "")
            {
                string query = "select distinct Factory_Name,sets.Factory_ID from sets inner join Factory on sets.Factory_ID=Factory.Factory_ID where sets.Type_ID=" + txtType.Text;
                MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                DataTable dt = new DataTable();
                da.Fill(dt);
                comFactory.DataSource = dt;
                comFactory.DisplayMember = dt.Columns["Factory_Name"].ToString();
                comFactory.ValueMember = dt.Columns["Factory_ID"].ToString();
                comFactory.Text = "";
                txtFactory.Text = "";

                query = "select Set_Name,Set_ID from sets where Type_ID=" + txtType.Text;
                da = new MySqlDataAdapter(query, dbconnection);
                dt = new DataTable();
                da.Fill(dt);
                comSets.DataSource = dt;
                comSets.DisplayMember = dt.Columns["Set_Name"].ToString();
                comSets.ValueMember = dt.Columns["Set_ID"].ToString();
                comSets.Text = "";
                txtSetsID.Text = "";
            }
        }

        public void FilterSet()
        {
            if (txtType.Text != "" && txtFactory.Text != "")
            {
                string query = "select Set_Name,Set_ID from sets where Type_ID=" + txtType.Text + " and Factory_ID=" + txtFactory.Text;
                MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                DataTable dt = new DataTable();
                da.Fill(dt);
                comSets.DataSource = dt;
                comSets.DisplayMember = dt.Columns["Set_Name"].ToString();
                comSets.ValueMember = dt.Columns["Set_ID"].ToString();
                comSets.Text = "";
                txtSetsID.Text = "";

            }
            else if (txtType.Text != "")
            {
                string query = "select Set_Name,Set_ID from sets where Type_ID=" + txtType.Text;
                MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                DataTable dt = new DataTable();
                da.Fill(dt);
                comSets.DataSource = dt;
                comSets.DisplayMember = dt.Columns["Set_Name"].ToString();
                comSets.ValueMember = dt.Columns["Set_ID"].ToString();
                comSets.Text = "";
                txtSetsID.Text = "";
            }
            else if (txtFactory.Text != "")
            {
                string query = "select Set_Name,Set_ID from sets where Factory_ID=" + txtFactory.Text;
                MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                DataTable dt = new DataTable();
                da.Fill(dt);
                comSets.DataSource = dt;
                comSets.DisplayMember = dt.Columns["Set_Name"].ToString();
                comSets.ValueMember = dt.Columns["Set_ID"].ToString();
                comSets.Text = "";
                txtSetsID.Text = "";
            }
        }
    }
}

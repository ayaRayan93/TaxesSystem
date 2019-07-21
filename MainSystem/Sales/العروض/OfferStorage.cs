using DevExpress.XtraEditors.Repository;
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

namespace MainSystem
{
    public partial class OfferStorage : Form
    {
        MySqlConnection dbconnection;
        MainForm salesMainForm =null;
        bool loaded = false;

        public OfferStorage(MainForm SalesMainForm)
        {
            try
            {
                InitializeComponent();
                this.salesMainForm = SalesMainForm;
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
                DisplayOffer();
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
                        case "comStore":
                            txtStoreID.Text = comStore.SelectedValue.ToString();
                            DisplayOffer();
                            break;
                        case "comOffers":
                            txtOffersID.Text = comOffers.SelectedValue.ToString();
                            DisplayOffer();
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
            
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    if (txtBox.Text != "")
                    {
                        dbconnection.Open();
                        switch (txtBox.Name)
                        {
                            case "txtStoreID":
                                comStore.SelectedValue = txtStoreID.Text;
                                break;
                            case "txtOffersID":
                                comOffers.SelectedValue = txtOffersID.Text;
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
                DisplayOffer();
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
                comOffers.Text = "";

                txtStoreID.Text = "";
                txtOffersID.Text = "";

                DisplayOffer();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btnOfferSet_Click(object sender, EventArgs e)
        {
            try
            {
                salesMainForm.bindOfferSetForm(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
           
        }
        private void btnFak_Click(object sender, EventArgs e)
        {
            try
            {
                dbconnection.Open();
                salesMainForm.bindFakOfferForm(this);
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
                salesMainForm.bindReportOffersForm(dataGridView1);
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
                string query = "SELECT data.Data_ID,data.Code,product.Product_Name,type.Type_Name,factory.Factory_Name,groupo.Group_Name,color.Color_Name,size.Size_Value,sort.Sort_Value,data.Classification,data.Description,data.Carton from data INNER JOIN type ON type.Type_ID = data.Type_ID INNER JOIN product ON product.Product_ID = data.Product_ID INNER JOIN factory ON data.Factory_ID = factory.Factory_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID LEFT outer JOIN color ON data.Color_ID = color.Color_ID LEFT outer  JOIN size ON data.Size_ID = size.Size_ID LEFT outer  JOIN sort ON data.Sort_ID = sort.Sort_ID where  data.Data_ID in(select offer_details.Data_ID from offer_details where offer_details.Offer_ID=" + row1[0].ToString() + ") group by data.Code";
                MySqlDataAdapter adapter = new MySqlDataAdapter(query, dbconnection);
                DataSet dataSet = new DataSet();
                adapter.Fill(dataSet);
                dataGridView1.DataSource = dataSet.Tables[0];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }

        //function
        public void DisplayOffer()
        {
            try
            {
                dbconnection.Close();
                dbconnection.Open();
                loaded = false;
                string q3, q5;
                if (txtOffersID.Text == "")
                {
                    q3 = "select Offer_ID from offer";
                }
                else
                {
                    q3 = txtOffersID.Text;
                }
                if (txtStoreID.Text == "")
                {
                    q5 = "select Store_ID from store";
                }
                else
                {
                    q5 = txtStoreID.Text;
                }
                string query = "SELECT offer.Offer_ID as 'كود العرض',Offer_Name as 'اسم العرض',sum(storage.Total_Meters) as 'الكمية',offer_photo.Photo as 'صورة' from offer left join offer_photo on offer_photo.Offer_ID=offer.Offer_ID inner join storage on storage.Offer_ID=offer.Offer_ID where storage.Offer_ID IN(" + q3+") and storage.Store_ID IN ("+q5+ ") group by storage.Offer_ID order by storage.Offer_ID";

                MySqlCommand com = new MySqlCommand(query, dbconnection);
                MySqlDataReader dr = com.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Close();
                    dbconnection.Close();
                    dbconnection.Open();
                    MySqlDataAdapter adapterOffers = new MySqlDataAdapter(query, dbconnection);
                    query = "SELECT offer.Offer_ID as 'كود العرض',data.Code as 'الكود',offer_details.Quantity as 'الكمية',product.Product_Name as 'الصنف',color.Color_Name as 'اللون',size.Size_Value as 'المقاس',sort.Sort_Value as 'الفرز',data.Classification as 'التصنيف',data.Description as 'الوصف',data.Carton as 'الكرتنة',data_photo.Photo as 'صورة' from data INNER JOIN type ON type.Type_ID = data.Type_ID INNER JOIN product ON product.Product_ID = data.Product_ID INNER JOIN factory ON data.Factory_ID = factory.Factory_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID LEFT outer JOIN color ON data.Color_ID = color.Color_ID LEFT outer  JOIN size ON data.Size_ID = size.Size_ID LEFT outer  JOIN sort ON data.Sort_ID = sort.Sort_ID INNER JOIN offer_details on data.Data_ID=offer_details.Data_ID INNER JOIN offer on offer.Offer_ID=offer_details.Offer_ID left join data_photo on data_photo.Data_ID=data.Data_ID order by data.Code";
                    MySqlDataAdapter AdapterProducts = new MySqlDataAdapter(query, dbconnection);
                    DataSet dataSet11 = new DataSet();

                    //Create DataTable objects for representing database's tables 
                    adapterOffers.Fill(dataSet11, "offer");
                    AdapterProducts.Fill(dataSet11, "data");

                    //Set up a master-detail relationship between the DataTables 
                    DataColumn keyColumn = dataSet11.Tables["offer"].Columns["كود العرض"];
                    DataColumn foreignKeyColumn = dataSet11.Tables["data"].Columns["كود العرض"];
                    dataSet11.Relations.Add("بنود العرض", keyColumn, foreignKeyColumn);

                    //Bind the grid control to the data source 
                    dataGridView1.DataSource = dataSet11.Tables["offer"];
                }
                else
                {
                    dataGridView1.DataSource = null;
                }
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
            String query = "delete from offer where Offer_ID=" + id;
            MySqlCommand com = new MySqlCommand(query,dbconnection);
            com.ExecuteNonQuery();
            query = "delete from offer_details where Offer_ID=" + id;
            com = new MySqlCommand(query, dbconnection);
            com.ExecuteNonQuery();
        }
        public void loadDataToBox()
        {
            string query = "select Offer_Name,Offer_ID from offer ";
            MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
            DataTable dt = new DataTable();
            da.Fill(dt);
            comOffers.DataSource = dt;
            comOffers.DisplayMember = dt.Columns["Offer_Name"].ToString();
            comOffers.ValueMember = dt.Columns["Offer_ID"].ToString();
            comOffers.Text = "";
            txtOffersID.Text = "";
            

            query = "select * from store ";
            da = new MySqlDataAdapter(query, dbconnection);
            dt = new DataTable();
            da.Fill(dt);
            comStore.DataSource = dt;
            comStore.DisplayMember = dt.Columns["Store_Name"].ToString();
            comStore.ValueMember = dt.Columns["Store_ID"].ToString();
            comStore.Text = "";
            txtStoreID.Text = "";
        }

        
    }
}

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
    public partial class Offer : Form
    {
        MySqlConnection dbconnection;
        MainForm salesMainForm=null;
        bool loaded = false;
        public static Offer_Record offerRecord = null;
        //public static SetUpdate setUpdate = null;

        public Offer(MainForm SalesMainForm)
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

        private void Offer_Load(object sender, EventArgs e)
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
                        case "comOffers":
                              txtOffersID.Text = comOffers.SelectedValue.ToString();
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
                            case "txtOffersID":
                                query = "select Offer_Name from offer where Offer_ID=" + txtOffersID.Text;
                                com = new MySqlCommand(query, dbconnection);
                                if (com.ExecuteScalar() != null)
                                {
                                    Name = (string)com.ExecuteScalar();
                                    comOffers.Text = Name;
                                       
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
                  //  MessageBox.Show(ex.ToString());
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

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            try
            {
              salesMainForm.bindRecordOfferForm(this);
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
                DataRowView offerRow = (DataRowView)(((GridView)dataGridView1.MainView).GetRow(((GridView)dataGridView1.MainView).GetSelectedRows()[0]));
                /*storeMainForm.bindUpdateOfferForm(offerRow,this);*/
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
                DataRowView offerRow = (DataRowView)(((GridView)dataGridView1.MainView).GetRow(((GridView)dataGridView1.MainView).GetSelectedRows()[0]));
                
                if (offerRow != null)
                {
                    DialogResult dialogResult = MessageBox.Show("Are you sure you want to delete the item?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dialogResult == DialogResult.Yes)
                    {

                        deleteOffer(Convert.ToInt16(offerRow[0].ToString()));

                        string query = "ALTER TABLE offer AUTO_INCREMENT = 1;";
                        MySqlCommand com = new MySqlCommand(query, dbconnection);
                        com.ExecuteNonQuery();

                        UserControl.ItemRecord("offer", "حذف", Convert.ToInt16(offerRow[0].ToString()), DateTime.Now, "", dbconnection);
                        dbconnection.Close();
                        DisplayOffer();
                        loadDataToBox();
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
                /*storeMainForm.bindReportOffersForm(dataGridView1);*/
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        
        //function
        public void DisplayOffer()
        {
            try
            {
                dbconnection.Open();
                loaded = false;
                string q1;
                if (txtOffersID.Text == "")
                {
                    q1 = "select Offer_ID from offer";
                }
                else
                {
                    q1 = txtOffersID.Text;
                }

                string query = "SELECT offer.Offer_ID as 'كود العرض',offer.Offer_Name as 'اسم العرض',offer_photo.Photo as 'الصورة' from offer left join offer_photo on offer.Offer_ID=offer_photo.Offer_ID where offer.Offer_ID IN(" + q1 + ") order by offer.Offer_ID";

                MySqlDataAdapter adapterSets = new MySqlDataAdapter(query, dbconnection);
                query = "SELECT offer.Offer_ID as 'كود العرض',data.Code as 'الكود',offer_details.Quantity as 'الكمية',product.Product_Name as 'الصنف',color.Color_Name as 'اللون',size.Size_Value as 'المقاس',sort.Sort_Value as 'الفرز',data.Classification as 'التصنيف',data.Description as 'الوصف',data.Carton as 'الكرتنة',data_photo.Photo as 'صورة' from offer_details inner join offer on offer.Offer_ID=offer_details.Offer_ID INNER JOIN data on data.Data_ID=offer_details.Data_ID INNER JOIN type ON type.Type_ID = data.Type_ID INNER JOIN product ON product.Product_ID = data.Product_ID INNER JOIN factory ON data.Factory_ID = factory.Factory_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID LEFT outer JOIN color ON data.Color_ID = color.Color_ID LEFT outer  JOIN size ON data.Size_ID = size.Size_ID LEFT outer  JOIN sort ON data.Sort_ID = sort.Sort_ID left join data_photo on data_photo.Data_ID=data.Data_ID where offer.Offer_ID IN(" + q1 + ") order by data.Data_ID";
                MySqlDataAdapter AdapterProducts = new MySqlDataAdapter(query, dbconnection);
                DataSet dataSet11 = new DataSet();

                //Create DataTable objects for representing database's tables 
                adapterSets.Fill(dataSet11, "offer");
                AdapterProducts.Fill(dataSet11, "offer_details");

                //Set up a master-detail relationship between the DataTables 
                DataColumn keyColumn = dataSet11.Tables["offer"].Columns["كود العرض"];
                DataColumn foreignKeyColumn = dataSet11.Tables["offer_details"].Columns["كود العرض"];
                dataSet11.Relations.Add("بنود العرض", keyColumn, foreignKeyColumn);

                //Bind the grid control to the data source 
                dataGridView1.DataSource = dataSet11.Tables["offer"];
                loaded = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();

        }

        public void deleteOffer(int id)
        {
            string query = "delete from offer where Offer_ID=" + id;
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
        }

        private void btnNewChooes_Click(object sender, EventArgs e)
        {
            try
            {
                comOffers.Text = "";
                
                txtOffersID.Text = "";

                DisplayOffer();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}

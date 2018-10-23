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
    public partial class Offer_Fak : Form
    {
        MySqlConnection dbconnection, dbconnection1, dbconnection2;
        bool loaded = false;
        bool flag = false;
        OfferStorage offerStorage;

        public Offer_Fak(OfferStorage OfferStorage)
        {
            InitializeComponent();
            dbconnection = new MySqlConnection(connection.connectionString);
            dbconnection1 = new MySqlConnection(connection.connectionString);
            dbconnection2 = new MySqlConnection(connection.connectionString);
            offerStorage = OfferStorage;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                string query = "select * from store ";
                MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                DataTable dt = new DataTable();
                da.Fill(dt);
                comStore.DataSource = dt;
                comStore.DisplayMember = dt.Columns["Store_Name"].ToString();
                comStore.ValueMember = dt.Columns["Store_ID"].ToString();
                comStore.Text = "";
                txtStoreID.Text = "";
                
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
                            Search();
                            groupBox1.Visible = true;
                            comOffers.Text = "";
                            txtOffersID.Text = "";
                            txtOfferQuantity.Text = "";
                            dataGridView1.Rows.Clear();
                            break;
                        case "comOffers":
                            if (flag)
                            {
                                txtOffersID.Text = comOffers.SelectedValue.ToString();
                                displayQuantityOffer();
                            }
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
                                int StoreId = 0;
                                if (int.TryParse(txtStoreID.Text, out StoreId))
                                {
                                    comStore.SelectedValue = txtStoreID.Text;
                                    Search();
                                    groupBox1.Visible = true;
                                    comOffers.Text = "";
                                    txtOffersID.Text = "";
                                    txtOfferQuantity.Text = "";
                                    txtTotalQuantityOffer.Text = "";
                                    dataGridView1.Rows.Clear();
                                }
                                else
                                {
                                    MessageBox.Show("enter correct value");
                                }
                                break;
                            case "txtOffersID":
                                if (flag)
                                {
                                    int id = 0;
                                    if (int.TryParse(txtOffersID.Text, out id))
                                    {
                                        comOffers.SelectedValue = txtOffersID.Text;
                                        displayQuantityOffer();
                                        txtStoreID.Focus();
                                    }
                                    else
                                    {
                                        MessageBox.Show("enter correct value");
                                    }
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

        private void btnFak_Click(object sender, EventArgs e)
        {
            try
            {
                dbconnection.Open();
                dataGridView1.Rows.Clear();
                double quantityOffer;
                if (double.TryParse(txtOfferQuantity.Text, out quantityOffer))
                {
                    int id;
                    if (int.TryParse(txtOffersID.Text, out id))
                    {
                        double q = fakOffer(quantityOffer, id);
                        increaseItemsQuantity(q, id);
                    }
                    else
                    {
                        MessageBox.Show("enter correct value");
                    }
                }
                else
                {
                    MessageBox.Show("enter correct value");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }

        private void btnDone_Click(object sender, EventArgs e)
        {
            try
            {
                dbconnection.Open();
                RecordOfferQuantityInStorage(Convert.ToDouble(txtOfferQuantity.Text), Convert.ToInt16(txtOffersID.Text));
                increaseItemsQuantityInDB(Convert.ToDouble(txtOfferQuantity.Text), Convert.ToInt16(txtOffersID.Text));
                MessageBox.Show("تم");

                dataGridView1.Rows.Clear();
                comStore.Text = "";
                txtStoreID.Text = "";
                txtOfferQuantity.Text = "";
                txtTotalQuantityOffer.Text = "";
                txtOffersID.Text = "";
                comOffers.Text = "";
                offerStorage.DisplayOffer();
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
                txtStoreID.Text = "";
                txtOfferQuantity.Text = "";
                txtTotalQuantityOffer.Text = "";

                dataGridView1.Rows.Clear();

                Search();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //functions 
        //display quantity of offer in store
        public void displayQuantityOffer()
        {
            dbconnection.Close();
            dbconnection.Open();
            int OfferID, StoreID;
            if (int.TryParse(txtOffersID.Text, out OfferID) && int.TryParse(txtStoreID.Text, out StoreID))
            {
                string query = "select Total_Meters from storage  where Offer_ID=" + OfferID + " and Store_ID=" + StoreID;
                MySqlCommand com = new MySqlCommand(query, dbconnection);
                if (com.ExecuteScalar() != null)
                {
                    double storeQuantity = Convert.ToDouble(com.ExecuteScalar());
                    txtTotalQuantityOffer.Text = storeQuantity.ToString();
                }
                else
                {
                    MessageBox.Show("هذا العرض لايوجد في المخزن");
                    txtOfferQuantity.Text = "";
                    txtTotalQuantityOffer.Text = "";
                }
            }
            else
            {
                MessageBox.Show("ادخل كمية صحيحة");
            }
            dbconnection.Close();
        }
        //العرض تفكيك
        public double fakOffer(double quantityOfferFak, int offerID)
        {
           
            string query = "select Total_Meters from storage  where Offer_ID=" + offerID + " and Store_ID=" + txtStoreID.Text;
            MySqlCommand com = new MySqlCommand(query,dbconnection);
            if (com.ExecuteScalar() != null)
            {
                double storeQuantity = Convert.ToDouble(com.ExecuteScalar());
                if (quantityOfferFak <= storeQuantity)
                {
                   return quantityOfferFak;
                }
                else
                {
                    MessageBox.Show("الكمية غير متوفرة");
                }
            }
            else
            {
                MessageBox.Show("هذا العرض لايوجد في المخزن");
            }
            return -1;
        }
        //record to database
        public void RecordOfferQuantityInStorage(double newQuantityOffer, int offerID)
        {
            string query = "select Total_Meters from storage  where Offer_ID=" + offerID + " and Store_ID=" + txtStoreID.Text;
            MySqlCommand com = new MySqlCommand(query, dbconnection);
            if (com.ExecuteScalar() != null)
            {
                double storeQuantity = Convert.ToDouble(com.ExecuteScalar());
                if ((storeQuantity - newQuantityOffer) > 0)
                {
                    query = "update storage set Total_Meters =" + (storeQuantity - newQuantityOffer) + "  where Offer_ID=" + offerID + " and Store_ID=" + txtStoreID.Text;
                    com = new MySqlCommand(query, dbconnection);
                    com.ExecuteNonQuery();
                }
                else
                {
                    query = "delete from storage  where Offer_ID=" + offerID + " and Store_ID=" + txtStoreID.Text;
                    com = new MySqlCommand(query, dbconnection);
                    com.ExecuteNonQuery();
                }
            }
            else
            {
                MessageBox.Show("هذا العرض لايوجد في المخزن");
            }
        }
        //increase the quantity of offer items
        public void increaseItemsQuantity(double offerQuantity,int offerID)
        {
            if (offerQuantity > 0)
            {
                dbconnection1.Open();
                dbconnection2.Open();
                string query = "select Data_ID,Quantity from offer_details  where Offer_ID=" + offerID;
                MySqlCommand com = new MySqlCommand(query,dbconnection);
                MySqlDataReader dr = com.ExecuteReader();
                while(dr.Read())
                {
                    query = "select sum(Total_Meters) from storage where Data_ID='" + dr["Data_ID"].ToString()+ "' group by Store_ID having Store_ID=" + txtStoreID.Text ;
                    MySqlCommand com1 = new MySqlCommand(query, dbconnection1);
                    double QuantityInStore = Convert.ToDouble(com1.ExecuteScalar());
                    double QuantityInOffer = Convert.ToDouble(dr["Quantity"].ToString());
                    double newQuantity = QuantityInStore + (QuantityInOffer * offerQuantity);
                    query = "SELECT data.Data_ID, data.Code as 'الكود',type.Type_Name as 'النوع',factory.Factory_Name as 'المصنع',groupo.Group_Name as 'المجموعة',product.Product_Name as 'الصنف',sort.Sort_Value as 'الفرز',color.Color_Name as 'اللون',size.Size_Value as 'المقاس',data.Classification as 'التصنيف',data.Description as 'الوصف',data.Carton as 'الكرتنة' from data INNER JOIN type ON type.Type_ID = data.Type_ID INNER JOIN product ON product.Product_ID = data.Product_ID INNER JOIN factory ON data.Factory_ID = factory.Factory_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID LEFT outer JOIN color ON data.Color_ID = color.Color_ID LEFT outer  JOIN size ON data.Size_ID = size.Size_ID LEFT outer  JOIN sort ON data.Sort_ID = sort.Sort_ID  where Data_ID=" + dr["Data_ID"].ToString() + "";

                    com = new MySqlCommand(query, dbconnection1);
                    MySqlDataReader dataReader1 = com.ExecuteReader();
                    while (dataReader1.Read())
                    {
                        int n = dataGridView1.Rows.Add();
                        dataGridView1.Rows[n].Cells["ProductCode"].Value = dataReader1["الكود"].ToString();
                        dataGridView1.Rows[n].Cells["Type_Name"].Value = dataReader1["النوع"].ToString();
                        dataGridView1.Rows[n].Cells["Factory_Name"].Value = dataReader1["المصنع"].ToString();
                        dataGridView1.Rows[n].Cells["Group_Name"].Value = dataReader1["المجموعة"].ToString();
                        dataGridView1.Rows[n].Cells["ProductName"].Value = dataReader1["الصنف"].ToString();
                        dataGridView1.Rows[n].Cells["ProductColor"].Value = dataReader1["اللون"].ToString();
                        dataGridView1.Rows[n].Cells["productSize"].Value = dataReader1["المقاس"].ToString();
                        dataGridView1.Rows[n].Cells["ProductSort"].Value = dataReader1["الفرز"].ToString();
                        dataGridView1.Rows[n].Cells["ProductQuantity"].Value = newQuantity.ToString();
                    }
                    dataReader1.Close();
                }
                dr.Close();
               
            }
            dbconnection1.Close();
            dbconnection2.Close();
        }        
        //
        public void increaseItemsQuantityInDB(double offerQuantity, int offerID)
        {
            if (offerQuantity > 0)
            {
                dbconnection1.Open();
                dbconnection2.Open();
                string query = "select Data_ID,Quantity from offer_details  where Offer_ID=" + offerID;
                MySqlCommand com = new MySqlCommand(query, dbconnection);
                MySqlDataReader dr = com.ExecuteReader();
                while (dr.Read())
                {
                    query = "select sum(Total_Meters) from storage where Data_ID='" + dr["Data_ID"].ToString() + "' group by Store_ID having Store_ID=" + txtStoreID.Text;
                    MySqlCommand com1 = new MySqlCommand(query, dbconnection1);
                    double QuantityInStore = Convert.ToDouble(com1.ExecuteScalar());
                    double QuantityInOffer = Convert.ToDouble(dr["Quantity"].ToString());
                    double newQuantity = QuantityInOffer * offerQuantity;

                    query = "select Storage_ID,Total_Meters from storage where Data_ID='" + dr["Data_ID"].ToString() + "' and Store_ID=" + txtStoreID.Text;
                    com1 = new MySqlCommand(query, dbconnection1);
                    MySqlDataReader dr2 = com1.ExecuteReader();
                    int id = 0;
                    while (dr2.Read())
                    {
                        double storageQ = Convert.ToDouble(dr2["Total_Meters"]);
                        //if (storageQ > newQuantity)
                        //{
                            id = Convert.ToInt16(dr2["Storage_ID"]);
                            query = "update storage set Total_Meters=" + (storageQ + newQuantity) + " where Storage_ID=" + id;
                            MySqlCommand comm = new MySqlCommand(query, dbconnection2);
                            comm.ExecuteNonQuery();
                            break;
                        //}
                    
                    }
                    dr2.Close();
                }
                dr.Close();
              
            }
            dbconnection1.Close();
            dbconnection2.Close();
        }
        //
        private void Search()
        {
            try
            {
                if (loaded)
                {
                    flag = false;
                    string query = "select Offer_ID,Offer_Name from offer";
                    MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    comOffers.DataSource = dt;
                    comOffers.DisplayMember = dt.Columns["Offer_Name"].ToString();
                    comOffers.ValueMember = dt.Columns["Offer_ID"].ToString();
                    groupBox1.Visible = true;
                    comOffers.Text = "";
                    txtOffersID.Text = "";
                    flag = true;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }
 
    }
}

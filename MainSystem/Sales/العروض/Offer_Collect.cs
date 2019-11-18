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
    public partial class Offer_Collect : Form
    {
        MySqlConnection dbconnection, dbconnection1, dbconnection2;
        bool loaded = false;
        bool flag = false;
        OfferStorage offerStorage;

        public Offer_Collect(OfferStorage OfferStorage)
        {
            try
            {
                InitializeComponent();
                dbconnection = new MySqlConnection(connection.connectionString);
                dbconnection1 = new MySqlConnection(connection.connectionString);
                dbconnection2 = new MySqlConnection(connection.connectionString);
                this.offerStorage = OfferStorage;
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
                string query = "select * from store ";
                MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                DataTable dt = new DataTable();
                da.Fill(dt);
                comStore.DataSource = dt;
                comStore.DisplayMember = dt.Columns["Store_Name"].ToString();
                comStore.ValueMember = dt.Columns["Store_ID"].ToString();
                comStore.Text = "";
                txtStoreID.Text = "";
                //Search();
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
                                dbconnection.Close();
                                dbconnection.Open();
                                dataGridView1.Rows.Clear();
                                txtOffersID.Text = comOffers.SelectedValue.ToString();
                                int id = 0;
                                if (int.TryParse(txtOffersID.Text, out id))
                                {
                                    double q= OfferCollect(id);
                                    txtOfferQuantity.Text = q.ToString();
                                    decreaseItemsQuantity(q, id);
                                }
                                else
                                {
                                    MessageBox.Show("ادخل قيمة صحيحة");
                                }
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
                                    dbconnection.Close();
                                    dbconnection.Open();
                                    int id = 0;
                                    if (int.TryParse(txtOffersID.Text, out id))
                                    {
                                        comOffers.SelectedValue = txtOffersID.Text;
                                        double q = OfferCollect(id);
                                        txtOfferQuantity.Text = q.ToString();
                                        decreaseItemsQuantity(q, id);
                                    }
                                    else
                                    {
                                        MessageBox.Show("enter correct value");
                                    }
                                    txtStoreID.Focus();
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

        private void btnDone_Click(object sender, EventArgs e)
        {
            try
            {
                dbconnection.Open();
                double q = OfferCollect(Convert.ToInt32(txtOffersID.Text));
                double quntityRequest = Convert.ToDouble(txtOfferQuantity.Text);
                if (quntityRequest<=q)
                {
                    RecordOfferQuantityInOferStorage(Convert.ToInt32(txtOffersID.Text), Convert.ToInt16(txtOfferQuantity.Text));
                    RecordOfferQuantityInStorage(Convert.ToDouble(txtOfferQuantity.Text), Convert.ToInt32(txtOffersID.Text));
                    decreaseItemsQuantityInDB(Convert.ToDouble(txtOfferQuantity.Text), Convert.ToInt32(txtOffersID.Text));
              
                    MessageBox.Show("تم");
                    // dataGridView1.Rows.Clear();
                    offerStorage.DisplayOffer();
                }
                else
                {
                    MessageBox.Show("الكمية المطلوبة غير متاحة");
                }
               
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

                dataGridView1.Rows.Clear();
                Search();
                groupBox1.Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txtOfferQuantity_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    dbconnection.Open();
                    double q = OfferCollect(Convert.ToInt32(txtOffersID.Text));
                    double quntityRequest = Convert.ToDouble(txtOfferQuantity.Text);
                    if (quntityRequest <= q)
                    {
                        dataGridView1.Rows.Clear();
                        int id;
                        if (int.TryParse(txtOffersID.Text, out id))
                        {
                            decreaseItemsQuantity(quntityRequest, id);
                        }
                        else
                        {
                            MessageBox.Show("ادخل قيمة صحيحة");
                        }
                    }
                    else
                    {
                        MessageBox.Show("الكمية المطلوبة غير متاحة");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }

        //functions       
        //تجميع العرض 
        public double OfferCollect(int offerID)
        {
            string query = "select count(OfferDetails_ID) from offer_details where Offer_ID=" + offerID;
            MySqlCommand com = new MySqlCommand(query,dbconnection);
            int offerItemsNumber = Convert.ToInt32(com.ExecuteScalar());
            int count = 0;
            query = "select sum(storage.Total_Meters)/offer_details.Quantity as q from offer inner join offer_details on offer.Offer_ID=offer_details.Offer_ID inner join storage on offer_details.Data_ID = storage.Data_ID where offer.Offer_ID=" + offerID + " group by storage.Data_ID,Store_ID having Store_ID=" + txtStoreID.Text;
            com = new MySqlCommand(query,dbconnection);
            MySqlDataReader dr = com.ExecuteReader();
          
            double minQuantity = 0;
            
            while (dr.Read())
            {
              minQuantity = Convert.ToDouble(dr["q"].ToString());
                count++;
                break;
            }

            while (dr.Read())
            {
                double quantity = Convert.ToDouble(dr["q"].ToString());
                if (quantity < minQuantity)
                    minQuantity = quantity;

                count++;                                                                   
            }
            dr.Close();
            if (count < offerItemsNumber)
            {
                minQuantity = 0;
            }
           
      
            return minQuantity;
        }

        //decrease the quantity of taqm items
        public void decreaseItemsQuantity(double offerQuantity, int offerID)
        {
            if (offerQuantity > 0)
            {
                dbconnection1.Open();
                dbconnection2.Open();
                string query = "select Data_ID,Quantity from offer_details  where Offer_ID=" + offerID;
                MySqlCommand com = new MySqlCommand(query, dbconnection);
                MySqlDataReader dr = com.ExecuteReader();
                dataGridView1.Rows.Clear();
                while (dr.Read())
                {
                    query = "select sum(Total_Meters) from storage where Data_ID='" + dr["Data_ID"].ToString() + "' group by Store_ID having Store_ID=" + txtStoreID.Text;
                    MySqlCommand com1 = new MySqlCommand(query, dbconnection1);
                    double QuantityInStore = Convert.ToDouble(com1.ExecuteScalar());
                    double QuantityInOffer = Convert.ToDouble(dr["Quantity"].ToString());
                    double newQuantity = QuantityInStore - (QuantityInOffer * offerQuantity);
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

        //record to database
        public void RecordOfferQuantityInStorage(double minQuantity,int offerID)
        {
           string query = "select Total_Meters from storage where Offer_ID=" + offerID + " and Store_ID=" + txtStoreID.Text;
            MySqlCommand com = new MySqlCommand(query, dbconnection);
            if (com.ExecuteScalar() != null)
            {
                double storeQuantity = Convert.ToDouble(com.ExecuteScalar());
                query = "update storage set Total_Meters=" +( minQuantity+ storeQuantity) + " where Offer_ID=" + offerID + " and Store_ID=" + txtStoreID.Text;
                com = new MySqlCommand(query, dbconnection);
                com.ExecuteNonQuery();
            }
            else
            {
                query = "insert into storage (Store_ID,Storage_Date,Total_Meters,Offer_ID,Type) values (@Store_ID,@Storage_Date,@Total_Meters,@Offer_ID,@Type)";
                com = new MySqlCommand(query, dbconnection);
                com.Parameters.Add("@Store_ID", MySqlDbType.Int16);
                com.Parameters["@Store_ID"].Value = Convert.ToInt32(txtStoreID.Text);
                com.Parameters.Add("@Storage_Date", MySqlDbType.Date);
                com.Parameters["@Storage_Date"].Value = DateTime.Now.ToString("yyyy-MM-dd");
                com.Parameters.Add("@Total_Meters", MySqlDbType.Decimal);
                com.Parameters["@Total_Meters"].Value = minQuantity;
                com.Parameters.Add("@Offer_ID", MySqlDbType.Int16);
                com.Parameters["@Offer_ID"].Value = offerID.ToString();
                com.Parameters.Add("@Type", MySqlDbType.VarChar);
                com.Parameters["@Type"].Value = "عرض";
                com.ExecuteNonQuery();
            }
        }
        //record to offer open storage quantity
        public void RecordOfferQuantityInOferStorage(int offerID,int minQuantity)
        {
            string query = "select Quntity from offer_openstorage_quantity where Offer_ID=" + offerID + " and Store_ID=" + txtStoreID.Text;
            MySqlCommand com = new MySqlCommand(query, dbconnection);
            if (com.ExecuteScalar() != null)
            {
                double storeQuantity = Convert.ToDouble(com.ExecuteScalar());
                query = "update offer_openstorage_quantity set Quantity=" + (minQuantity + storeQuantity) + " where Offer_ID=" + offerID + " and Store_ID=" + txtStoreID.Text;
                com = new MySqlCommand(query, dbconnection);
                com.ExecuteNonQuery();
            }
            else
            {
                query = "insert into offer_openstorage_quantity (Store_ID,Offer_ID,Quantity,Date) values (@Store_ID,@Offer_ID,@Quantity,@date)";
                com = new MySqlCommand(query, dbconnection);
                com.Parameters.Add("@Store_ID", MySqlDbType.Int16);
                com.Parameters["@Store_ID"].Value = Convert.ToInt32(txtStoreID.Text);
                com.Parameters.Add("@Offer_ID", MySqlDbType.Date);
                com.Parameters["@Offer_ID"].Value = DateTime.Now.ToString("yyyy-MM-dd");
                com.Parameters.Add("@Quantity", MySqlDbType.Int16);
                com.Parameters["@Quantity"].Value = minQuantity;
                com.Parameters.Add("@Date", MySqlDbType.Int16);
                com.Parameters["@Date"].Value = DateTime.Now.Date;
                com.ExecuteNonQuery();
            }

        }
        //function
        public void decreaseItemsQuantityInDB(double TaqmQuantity, int offerID)
        {
            if (TaqmQuantity > 0)
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
                    double newQuantity = QuantityInOffer * TaqmQuantity;

                    query = "select Storage_ID,Total_Meters from storage where Data_ID='" + dr["Data_ID"].ToString() + "' and Store_ID=" + txtStoreID.Text;
                    com1 = new MySqlCommand(query, dbconnection1);
                    MySqlDataReader dr2 = com1.ExecuteReader();
                    int id = 0;
                    while (dr2.Read())
                    {
                        double storageQ = Convert.ToDouble(dr2["Total_Meters"]);
                        if (storageQ > newQuantity)
                        {
                            id = Convert.ToInt32(dr2["Storage_ID"]);
                            query = "update storage set Total_Meters=" + (storageQ - newQuantity) + " where Storage_ID=" + id;
                            MySqlCommand comm = new MySqlCommand(query, dbconnection2);
                            comm.ExecuteNonQuery();
                            newQuantity -= storageQ;

                            break;
                        }
                        else
                        {
                            id = Convert.ToInt32(dr2["Storage_ID"]);
                            query = "update storage set Total_Meters=" + 0 + " where Storage_ID=" + id;
                            MySqlCommand comm = new MySqlCommand(query, dbconnection2);
                            comm.ExecuteNonQuery();
                            newQuantity -= storageQ;
                        }
                    }
                    dr2.Close();
                }
                dr.Close();
                //query = "select sum(Total_Meters),storage.Data_ID from storage inner join offer_details on storage.Data_ID=offer_details.Data_ID where storage.Offer_ID=" + offerID + " group by storage.Data_ID,Store_ID having Store_ID=" + txtStoreID.Text;
                //MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                //DataTable dt = new DataTable();
                //da.Fill(dt);
                //dataGridView1.DataSource = dt;
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
                    comOffers.Text = "";
                    txtOffersID.Text = "";
                    flag = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}

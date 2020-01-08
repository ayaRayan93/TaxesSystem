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

namespace MainSystem.Store.Export
{
    public partial class updateCustomerPermission : Form
    {
        MySqlConnection dbconnection;

        public updateCustomerPermission(string permissionNum, string branchID)
        {
            try
            {
                InitializeComponent();
                dbconnection = new MySqlConnection(connection.connectionString);
                txtPermBillNumber.Text = permissionNum;
                txtBranchID.Text = branchID;
                string query = "select Branch_Name from branch where Branch_ID=" + txtBranchID.Text;
                MySqlCommand comand = new MySqlCommand(query, dbconnection);

                comBranch.Text = comand.ExecuteScalar().ToString();
                displayDatawithPerviousPer();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //function
        public void displayDatawithPerviousPer()
        {
            string query = "select CustomerBill_ID from customer_bill where Branch_BillNumber=" + txtPermBillNumber.Text + " and Branch_ID=" + txtBranchID.Text;

            MySqlCommand com = new MySqlCommand(query, dbconnection);
            int id = Convert.ToInt32(com.ExecuteScalar());
            displayCustomerData(id.ToString());
            gridControl2.DataSource = null;
            gridView2.Columns.Clear();
            DataTable dtAll = new DataTable();

            query = "select data.Data_ID,data.Code as 'الكود',concat(type.Type_Name,' ',product.Product_Name,' ',COALESCE(color.Color_Name,''),' ',COALESCE(data.Description,''),' ',groupo.Group_Name,' ',factory.Factory_Name,' ',COALESCE(size.Size_Value,''),' ',COALESCE(sort.Sort_Value,'')) as 'الاسم',customer_permissions_details.ItemType as 'الفئة',customer_permissions_details.Carton as 'الكرتنة',customer_permissions_details.Quantity as 'الكمية',sum(customer_permissions_details.DeliveredQuantity) as 'الكمية المستلمة' from customer_permissions_details inner join data on data.Data_ID=customer_permissions_details.Data_ID LEFT JOIN color ON color.Color_ID = data.Color_ID LEFT JOIN size ON size.Size_ID = data.Size_ID LEFT JOIN sort ON sort.Sort_ID = data.Sort_ID INNER JOIN type ON type.Type_ID = data.Type_ID INNER JOIN product ON product.Product_ID = data.Product_ID INNER JOIN factory ON data.Factory_ID = factory.Factory_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID inner join customer_permissions on  customer_permissions.Customer_Permissin_ID=customer_permissions_details.Customer_Permissin_ID where customer_permissions_details.ItemType='بند' and BranchBillNumber=" + txtPermBillNumber.Text + " and Branch_ID=" + txtBranchID.Text + " group by data.Data_ID";
            MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
            DataTable dtper = new DataTable();
            da.Fill(dtper);
            dtAll = dtper.Copy();

            query = "select sets.Set_ID as 'Data_ID',sets.Set_ID as 'الكود',sets.Set_Name as 'الاسم',customer_permissions_details.ItemType as 'الفئة',customer_permissions_details.Carton as 'الكرتنة',customer_permissions_details.Quantity as 'الكمية',sum(customer_permissions_details.DeliveredQuantity) as 'الكمية المستلمة' from customer_permissions_details inner join sets on sets.Set_ID=customer_permissions_details.Data_ID inner join customer_permissions on  customer_permissions.Customer_Permissin_ID=customer_permissions_details.Customer_Permissin_ID where customer_permissions_details.ItemType='طقم' and BranchBillNumber=" + txtPermBillNumber.Text + " and Branch_ID=" + txtBranchID.Text + " group by sets.Set_ID";
            da = new MySqlDataAdapter(query, dbconnection);
            DataTable dtper1 = new DataTable();
            da.Fill(dtper1);

            query = "select offer.Offer_ID as 'Data_ID',offer.Offer_ID as 'الكود',offer.Offer_Name as 'الاسم',customer_permissions_details.ItemType as 'الفئة',customer_permissions_details.Carton as 'الكرتنة',customer_permissions_details.Quantity as 'الكمية',sum(customer_permissions_details.DeliveredQuantity) as 'الكمية المستلمة' from customer_permissions_details inner join offer on offer.Offer_ID=customer_permissions_details.Data_ID inner join customer_permissions on  customer_permissions.Customer_Permissin_ID=customer_permissions_details.Customer_Permissin_ID where customer_permissions_details.ItemType='عرض' and BranchBillNumber=" + txtPermBillNumber.Text + " and Branch_ID=" + txtBranchID.Text + " group by offer.Offer_ID";
            da = new MySqlDataAdapter(query, dbconnection);
            DataTable dtper2 = new DataTable();
            da.Fill(dtper2);

            dtAll.Merge(dtper1, true, MissingSchemaAction.Ignore);
            string re = getDeliveredDataItems("بند");
            if (re != "")
            {
                query = "select data.Data_ID,data.Code as 'الكود',concat(product.Product_Name,' - ',type.Type_Name,' - ',factory.Factory_Name,' - ',groupo.Group_Name,' ',COALESCE(color.Color_Name,''),' ',COALESCE(size.Size_Value,''),' ',COALESCE(sort.Sort_Value,'')) as 'الاسم',product_bill.Type as 'الفئة',product_bill.Cartons as 'الكرتنة',product_bill.Quantity as 'الكمية' , '" + 0 + " ' as 'الكمية المسلمة' from product_bill inner join data on data.Data_ID=product_bill.Data_ID LEFT JOIN color ON color.Color_ID = data.Color_ID LEFT JOIN size ON size.Size_ID = data.Size_ID LEFT JOIN sort ON sort.Sort_ID = data.Sort_ID INNER JOIN type ON type.Type_ID = data.Type_ID INNER JOIN product ON product.Product_ID = data.Product_ID INNER JOIN factory ON data.Factory_ID = factory.Factory_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID inner join delegate on delegate.Delegate_ID=product_bill.Delegate_ID  where CustomerBill_ID=" + id + " and product_bill.Type='بند' and product_bill.Data_ID not in ( " + re + ") ";// and (product_bill.Returned='لا' or product_bill.Returned='جزء')";
                da = new MySqlDataAdapter(query, dbconnection);
                DataTable dtProduct = new DataTable();
                da.Fill(dtProduct);
                dtAll.Merge(dtProduct, true, MissingSchemaAction.Ignore);
            }

            re = getDeliveredDataItems("طقم");
            if (re != "")
            {
                query = "select sets.Set_ID as 'Data_ID',sets.Set_ID as 'الكود',sets.Set_Name as 'الاسم',product_bill.Type as 'الفئة', product_bill.Quantity as 'الكمية', '" + 0 + " ' as 'الكمية المسلمة' from product_bill inner join sets on sets.Set_ID=product_bill.Data_ID inner join delegate on delegate.Delegate_ID=product_bill.Delegate_ID where CustomerBill_ID=" + id + " and product_bill.Type='طقم' and Set_ID not in (" + re + ")";// and (product_bill.Returned='لا' or product_bill.Returned='جزء')";
                da = new MySqlDataAdapter(query, dbconnection);
                DataTable dtSet = new DataTable();
                da.Fill(dtSet);
                dtAll.Merge(dtSet, true, MissingSchemaAction.Ignore);
            }

            re = getDeliveredDataItems("عرض");
            if (re != "")
            {
                query = "select offer.Offer_ID as 'Data_ID',offer.Offer_ID as 'الكود',offer.Offer_Name as 'الاسم',customer_permissions_details.ItemType as 'الفئة',customer_permissions_details.Carton as 'الكرتنة',customer_permissions_details.Quantity as 'الكمية',sum(customer_permissions_details.DeliveredQuantity) as 'الكمية المستلمة' from customer_permissions_details inner join offer on offer.Offer_ID=customer_permissions_details.Data_ID inner join customer_permissions on  customer_permissions.Customer_Permissin_ID=customer_permissions_details.Customer_Permissin_ID where customer_permissions_details.ItemType='عرض' and BranchBillNumber=" + txtPermBillNumber.Text + " and Branch_ID=" + txtBranchID.Text + " group by offer.Offer_ID";
                da = new MySqlDataAdapter(query, dbconnection);
                DataTable dtOffer = new DataTable();
                da.Fill(dtOffer);
                dtAll.Merge(dtOffer, true, MissingSchemaAction.Ignore);
            }

            gridControl2.DataSource = dtAll;
            gridView2.Columns[0].Visible = false;
            gridView2.Columns["الفئة"].Visible = false;
            //gridView1.Columns["الوصف"].Visible = false;
            //gridView1.Columns["Delegate_Name"].Visible = false;
            //gridView1.Columns["Store_ID"].Visible = false;
            //txtDelegate.Text = gridView1.GetDataRow(0)["Delegate_Name"].ToString();
        }
        public void displayCustomerData(string CustomerBill_ID)
        {
            string query = "select c1.Customer_ID,c1.Customer_Name,c2.Customer_ID,c2.Customer_Name,cc1.Phone,cc2.Phone,Bill_Date from customer_bill left join  customer as c1  on c1.Customer_ID=customer_bill.Customer_ID left join customer as c2 on c2.Customer_ID=customer_bill.Client_ID left join customer_phone as cc1 on cc1.Customer_ID=c1.Customer_ID left join customer_phone as cc2 on cc2.Customer_ID=c2.Customer_ID where CustomerBill_ID=" + CustomerBill_ID;
            MySqlCommand com = new MySqlCommand(query, dbconnection);
            MySqlDataReader dr = com.ExecuteReader();
            while (dr.Read())
            {
                txtCustomerID.Text = dr[0].ToString();
                txtCustomerName.Text = dr[1].ToString();
                txtClientID.Text = dr[2].ToString();
                txtClientName.Text = dr[3].ToString();
                if (txtCustomerID.Text != "")
                    txtPhoneNumber.Text = dr[4].ToString();
                if (txtClientID.Text != "")
                    txtPhoneNumber.Text = dr[5].ToString();
                labDate.Text = dr[6].ToString();
            }
            dr.Close();
        }
        public string getDeliveredDataItems(string type)
        {
            string query = "select group_concat(distinct Data_ID) from customer_permissions_details inner join customer_permissions on customer_permissions.Customer_Permissin_ID=customer_permissions_details.Customer_Permissin_ID where ItemType='" + type + "' and BranchBillNumber=" + txtPermBillNumber.Text + " and Branch_ID=" + txtBranchID.Text;
            MySqlCommand com = new MySqlCommand(query, dbconnection);
            string result = com.ExecuteScalar().ToString();

            return result;
        }
    }
}

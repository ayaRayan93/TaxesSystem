using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraPrinting;
using DevExpress.XtraPrinting.Preview;
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

namespace TaxesSystem
{
    public partial class Transportation_Report : Form
    {
        MySqlConnection conn;
        MySqlConnection connectionReader1, connectionReader2;
        MainForm MainForm = null;
        int TransferProductID = 0;
        bool WithBill = false;
        
        public Transportation_Report(MainForm mainForm)
        {
            InitializeComponent();
            conn = new MySqlConnection(connection.connectionString);
            connectionReader1 = new MySqlConnection(connection.connectionString);
            connectionReader2 = new MySqlConnection(connection.connectionString);
            MainForm = mainForm;
        }

        private void Bills_Transitions_Report_Load(object sender, EventArgs e)
        {
            try
            {
                string query = "select * from Store";
                MySqlDataAdapter da = new MySqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                comFromStore.DataSource = dt;
                comFromStore.DisplayMember = dt.Columns["Store_Name"].ToString();
                comFromStore.ValueMember = dt.Columns["Store_ID"].ToString();
                comFromStore.Text = "";

                query = "select * from Store where Store_ID<>" + comFromStore.SelectedValue.ToString();
                da = new MySqlDataAdapter(query, conn);
                dt = new DataTable();
                da.Fill(dt);
                comToStore.DataSource = dt;
                comToStore.DisplayMember = dt.Columns["Store_Name"].ToString();
                comToStore.ValueMember = dt.Columns["Store_ID"].ToString();
                comToStore.Text = "";
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        
        private void txtBillNum_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    if (txtBillNum.Text != "")
                    {
                        TransferProductID = 0;
                        search(Convert.ToInt16(txtBillNum.Text));
                    }
                    else
                    {
                        MessageBox.Show("يجب تحديد رقم الفاتورة");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                conn.Close();
            }
        }

        private void btnNewChosen_Click(object sender, EventArgs e)
        {
            try
            {
                txtBillNum.Text = "";
                clearCom();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            if (TransferProductID > 0)
            {
                try
                {
                    List<Transportation_Items> bi = new List<Transportation_Items>();
                    conn.Open();
                    string query = "SELECT transfer_product.TransferProduct_ID as 'رقم التحويل',storeFrom.Store_Name as 'من مخزن',storeTo.Store_Name as 'الى مخزن',transfer_product.Date as 'تاريخ التحويل' FROM transfer_product left JOIN store as storeTo ON storeTo.Store_ID = transfer_product.To_Store left join store as storeFrom on storeFrom.Store_ID = transfer_product.From_Store where transfer_product.TransferProduct_ID=" + TransferProductID;
                    MySqlCommand com = new MySqlCommand(query, conn);
                    MySqlDataReader dr = com.ExecuteReader();
                    while (dr.Read())
                    {
                        Transportation_Items item;
                        connectionReader2.Open();

                        string q = "SELECT data.Code as 'الكود',type.Type_Name as 'النوع',concat(product.Product_Name,' ',COALESCE(color.Color_Name,''),' ',data.Description,' ',groupo.Group_Name,' ',factory.Factory_Name,' ',COALESCE(size.Size_Value,''),' ',COALESCE(sort.Sort_Value,'')) as 'الاسم',transfer_product_details.Quantity as 'الكمية',concat(customer_bill.Branch_BillNumber,' ',customer_bill.Branch_Name) as 'رقم الفاتورة' FROM transfer_product_details INNER JOIN transfer_product ON transfer_product_details.TransferProduct_ID = transfer_product.TransferProduct_ID left JOIN store as storeTo ON storeTo.Store_ID = transfer_product.To_Store left join store as storeFrom on storeFrom.Store_ID = transfer_product.From_Store left JOIN customer_bill ON transfer_product_details.CustomerBill_ID = customer_bill.CustomerBill_ID INNER JOIN data ON transfer_product_details.Data_ID = data.Data_ID LEFT JOIN color ON color.Color_ID = data.Color_ID LEFT JOIN size ON size.Size_ID = data.Size_ID LEFT JOIN sort ON sort.Sort_ID = data.Sort_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID INNER JOIN factory ON factory.Factory_ID = data.Factory_ID  INNER JOIN product ON product.Product_ID = data.Product_ID  INNER JOIN type ON type.Type_ID = data.Type_ID where transfer_product.TransferProduct_ID=" + dr["رقم التحويل"].ToString() + " order by SUBSTR(data.Code,1,16),color.Color_Name,data.Description,data.Sort_ID";
                        MySqlCommand c = new MySqlCommand(q, connectionReader2);
                        MySqlDataReader dr1 = c.ExecuteReader();
                        while (dr1.Read())
                        {
                            if (WithBill == false)
                            {
                                item = new Transportation_Items() { Code = dr1["الكود"].ToString(), Product_Type = dr1["النوع"].ToString(), Product_Name = dr1["الاسم"].ToString(), Total_Meters = Convert.ToDouble(dr1["الكمية"].ToString())};
                            }
                            else
                            {
                                item = new Transportation_Items() { Code = dr1["الكود"].ToString(), Product_Type = dr1["النوع"].ToString(), Product_Name = dr1["الاسم"].ToString(), Total_Meters = Convert.ToDouble(dr1["الكمية"].ToString()), Bill = dr1["رقم الفاتورة"].ToString() };
                            }
                            bi.Add(item);
                        }
                        dr1.Close();
                    }
                    if (WithBill == false)
                    {
                        Report_Transportation_Copy f = new Report_Transportation_Copy();
                        f.PrintInvoice(Convert.ToInt16(dr["رقم التحويل"].ToString()), dr["من مخزن"].ToString(), dr["الى مخزن"].ToString(), dr["تاريخ التحويل"].ToString(), bi);
                        dr.Close();
                        f.ShowDialog();
                    }
                    else
                    {
                        Report_Transportation_Bill_Copy f = new Report_Transportation_Bill_Copy();
                        f.PrintInvoice(Convert.ToInt16(dr["رقم التحويل"].ToString()), dr["من مخزن"].ToString(), dr["الى مخزن"].ToString(), dr["تاريخ التحويل"].ToString(), bi);
                        dr.Close();
                        f.ShowDialog();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                conn.Close();
                connectionReader2.Close();
            }
            else
            {
                MessageBox.Show("برجاء التاكد من اختيار عنصر");
            }
        }

        public void search(int billNum)
        {
            conn.Open();
            string query = "SELECT distinct transfer_product.TransferProduct_ID as 'رقم التحويل',storeFrom.Store_ID as 'FromStoreID',storeFrom.Store_Name as 'من مخزن',storeTo.Store_ID as 'ToStoreID',storeTo.Store_Name as 'الى مخزن',transfer_product.Date as 'تاريخ التحويل',COALESCE(IF(transfer_product_details.CustomerBill_ID=0,'لا','نعم'),'CUSTOM') as 'رقم الفاتورة' FROM transfer_product left JOIN store as storeTo ON storeTo.Store_ID = transfer_product.To_Store left join store as storeFrom on storeFrom.Store_ID = transfer_product.From_Store INNER JOIN transfer_product_details ON transfer_product_details.TransferProduct_ID = transfer_product.TransferProduct_ID where transfer_product.Canceled=0 and transfer_product.TransferProduct_ID=" + billNum;
            MySqlCommand comand = new MySqlCommand(query, conn);
            MySqlDataReader dr = comand.ExecuteReader();
            while (dr.Read())
            {
                TransferProductID = Convert.ToInt16(dr["رقم التحويل"].ToString());
                comFromStore.SelectedIndex = -1;
                comFromStore.Text = dr["من مخزن"].ToString();
                txtFromStoreId.Text = dr["FromStoreID"].ToString();
                comToStore.SelectedIndex = -1;
                comToStore.Text = dr["الى مخزن"].ToString();
                txtToStore.Text = dr["ToStoreID"].ToString();
                dateTimePicker1.Value = Convert.ToDateTime(dr["تاريخ التحويل"].ToString());
                if (dr["رقم الفاتورة"].ToString() == "نعم")
                {
                    WithBill = true;
                }
                else
                {
                    WithBill = false;
                }
            }
            dr.Close();

            query = "SELECT data.Code as 'الكود',type.Type_Name as 'النوع',concat(product.Product_Name,' ',COALESCE(color.Color_Name,''),' ',data.Description,' ',groupo.Group_Name,' ',factory.Factory_Name,' ',COALESCE(size.Size_Value,''),' ',COALESCE(sort.Sort_Value,'')) as 'الاسم',transfer_product_details.Quantity as 'الكمية',CONCAT(customer_bill.Branch_BillNumber,' ',customer_bill.Branch_Name) as 'رقم الفاتورة' FROM transfer_product_details INNER JOIN transfer_product ON transfer_product_details.TransferProduct_ID = transfer_product.TransferProduct_ID left JOIN store as storeTo ON storeTo.Store_ID = transfer_product.To_Store left join store as storeFrom on storeFrom.Store_ID = transfer_product.From_Store left JOIN customer_bill ON transfer_product_details.CustomerBill_ID = customer_bill.CustomerBill_ID INNER JOIN data ON transfer_product_details.Data_ID = data.Data_ID LEFT JOIN color ON color.Color_ID = data.Color_ID LEFT JOIN size ON size.Size_ID = data.Size_ID LEFT JOIN sort ON sort.Sort_ID = data.Sort_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID INNER JOIN factory ON factory.Factory_ID = data.Factory_ID  INNER JOIN product ON product.Product_ID = data.Product_ID  INNER JOIN type ON type.Type_ID = data.Type_ID where transfer_product.Canceled=0 and transfer_product.TransferProduct_ID=" + billNum + " order by SUBSTR(data.Code,1,16),color.Color_Name,data.Description,data.Sort_ID";
            MySqlDataAdapter da = new MySqlDataAdapter(query, conn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            gridControl1.DataSource = dt;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (UserControl.userType == 1 || UserControl.userType == 6 || UserControl.userType == 7 || UserControl.userType == 19 || UserControl.userType == 24)
            {
                if (TransferProductID > 0)
                {
                    try
                    {
                        if (WithBill == false && UserControl.userType != 6)
                        {
                            MainForm.bindUpdateTransporationForm(TransferProductID, comFromStore.SelectedValue.ToString(), comToStore.SelectedValue.ToString(), dateTimePicker1.Value, this);
                        }
                        else if (WithBill == true && UserControl.userType != 24)
                        {
                            MainForm.bindUpdateTransporationBillForm(TransferProductID, comFromStore.SelectedValue.ToString(), comToStore.SelectedValue.ToString(), dateTimePicker1.Value, this);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                else
                {
                    MessageBox.Show("يجب تحديد التحويل المراد تعديله");
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (UserControl.userType == 1 || UserControl.userType == 6 || UserControl.userType == 7 || UserControl.userType == 19 || UserControl.userType == 24)
            {
                if (TransferProductID > 0)
                {
                    if (WithBill == true && UserControl.userType == 24)
                    {
                        return;
                    }
                    else if (WithBill == false && UserControl.userType == 6)
                    {
                        return;
                    }
                    if (MessageBox.Show("هل انت متاكد انك تريد الالغاء؟", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        try
                        {
                            conn.Open();

                            if (!WithBill)
                            {
                                connectionReader1.Open();
                                string query = "SELECT data.Data_ID,transfer_product_details.Quantity as 'الكمية',transfer_product_details.CustomerBill_ID,transfer_product.From_Store,transfer_product.To_Store FROM transfer_product_details INNER JOIN transfer_product ON transfer_product_details.TransferProduct_ID = transfer_product.TransferProduct_ID left JOIN store as storeTo ON storeTo.Store_ID = transfer_product.To_Store left join store as storeFrom on storeFrom.Store_ID = transfer_product.From_Store left JOIN customer_bill ON transfer_product_details.CustomerBill_ID = customer_bill.CustomerBill_ID INNER JOIN data ON transfer_product_details.Data_ID = data.Data_ID LEFT JOIN color ON color.Color_ID = data.Color_ID LEFT JOIN size ON size.Size_ID = data.Size_ID LEFT JOIN sort ON sort.Sort_ID = data.Sort_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID INNER JOIN factory ON factory.Factory_ID = data.Factory_ID  INNER JOIN product ON product.Product_ID = data.Product_ID  INNER JOIN type ON type.Type_ID = data.Type_ID where transfer_product.TransferProduct_ID=" + TransferProductID + " and transfer_product.Canceled=0 order by SUBSTR(data.Code, 1, 16),color.Color_Name,data.Description,data.Sort_ID";
                                MySqlCommand com = new MySqlCommand(query, connectionReader1);
                                MySqlDataReader dr = com.ExecuteReader();
                                while (dr.Read())
                                {
                                    if (dr["CustomerBill_ID"].ToString() == "0")
                                    {
                                        query = "select sum(Total_Meters) from storage where Data_ID=" + dr[0].ToString() + " and Store_ID=" + dr["From_Store"].ToString() + " group by Data_ID";
                                        com = new MySqlCommand(query, conn);
                                        double quantity = Convert.ToDouble(com.ExecuteScalar().ToString());
                                        double meters = quantity + Convert.ToDouble(dr["الكمية"].ToString());
                                        query = "update storage set Total_Meters=" + meters + " where Data_ID=" + dr[0].ToString() + " and Store_ID=" + dr["From_Store"].ToString();
                                        com = new MySqlCommand(query, conn);
                                        com.ExecuteNonQuery();

                                        query = "select sum(Total_Meters) from storage where Data_ID=" + dr[0].ToString() + " and Store_ID=" + dr["To_Store"].ToString() + " group by Data_ID";
                                        com = new MySqlCommand(query, conn);
                                        quantity = Convert.ToDouble(com.ExecuteScalar().ToString());
                                        meters = quantity - Convert.ToDouble(dr["الكمية"].ToString());
                                        query = "update storage set Total_Meters=" + meters + " where Data_ID=" + dr[0].ToString() + " and Store_ID=" + dr["To_Store"].ToString();
                                        com = new MySqlCommand(query, conn);
                                        com.ExecuteNonQuery();
                                    }
                                }
                                dr.Close();
                            }

                            string query2 = "update transfer_product set Canceled=1 where TransferProduct_ID=" + TransferProductID;
                            MySqlCommand com2 = new MySqlCommand(query2, conn);
                            com2.ExecuteNonQuery();

                            txtBillNum.Text = "";
                            clearCom();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                        conn.Close();
                        connectionReader1.Close();
                    }
                }
                else
                {
                    MessageBox.Show("يجب تحديد التحويل المراد حذفه");
                }
            }
        }

        private void txtBillNum_TextChanged(object sender, EventArgs e)
        {
            clearCom();
        }

        public void clearCom()
        {
            TransferProductID = 0;
            WithBill = false;
            comFromStore.SelectedIndex = -1;
            comToStore.SelectedIndex = -1;
            txtFromStoreId.Text = "";
            txtToStore.Text = "";
            gridControl1.DataSource = null;
        }
    }
}

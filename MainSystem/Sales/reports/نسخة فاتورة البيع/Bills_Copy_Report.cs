﻿using DevExpress.XtraGrid;
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
    public partial class Bills_Copy_Report : Form
    {
        MySqlConnection conn;
        MySqlConnection connectionReader1;
        MySqlConnection connectionReader2;
        MySqlConnection myConnection;
        MainForm bankMainForm = null;
        XtraTabControl MainTabControlBank;
        
        public static XtraTabPage MainTabPagePrintingTransitions;
        Panel panelPrintingTransitions;

        public static BillsTransitions_Print bankPrint;

        public static GridControl gridcontrol;
        bool loaded = false;
        bool loadedBranch = false;
        string delegateName = "";
        int branchID = 0;
        string branchName = "";
        int billNumber = 0;
        bool flag2 = false;
        int customerID = 0;
        int clientID = 0;
        string engName = "";
        string clientName = "";
        string customerPhoneNumber = "";
        string clientPhoneNumber = "";
        string TypeBuy = "";
        DateTime billDate;
        int ID = -1;
        string ConfirmEmp = "";
        double totalCostBD = 0;
        double totalCostAD = 0;
        double totalDiscount = 0;

        public Bills_Copy_Report(MainForm BankMainForm)
        {
            InitializeComponent();
            conn = new MySqlConnection(connection.connectionString);
            connectionReader1 = new MySqlConnection(connection.connectionString);
            connectionReader2 = new MySqlConnection(connection.connectionString);
            myConnection = new MySqlConnection(connection.connectionString);
            bankMainForm = BankMainForm;
            MainTabControlBank = MainForm.tabControlBank;
            
            MainTabPagePrintingTransitions = new XtraTabPage();
            panelPrintingTransitions = new Panel();
            
            gridcontrol = gridControl1;

            comBranch.AutoCompleteMode = AutoCompleteMode.Suggest;
            comBranch.AutoCompleteSource = AutoCompleteSource.ListItems;
        }

        private void Bills_Transitions_Report_Load(object sender, EventArgs e)
        {
            try
            {
                if (!loadedBranch)
                {
                    loadBranch();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            conn.Close();
        }

        private void comBranch_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (loadedBranch)
                {
                    if (int.TryParse(comBranch.SelectedValue.ToString(), out branchID))
                    {
                        txtBranchID.Text = comBranch.SelectedValue.ToString();
                        branchName = comBranch.Text;
                        txtBillNum.Enabled = true;
                        txtBillNum.Focus();
                    }
                }
            }
            catch (Exception ex)
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
                    if (comBranch.Text != "" && txtBranchID.Text != "")
                    {
                        search();
                    }
                    else
                    {
                        MessageBox.Show("يجب اختيار فرع");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                conn.Close();
                myConnection.Close();
                connectionReader1.Close();
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (comBranch.Text != "" && txtBranchID.Text != "")
                {
                    search();
                }
                else
                {
                    MessageBox.Show("يجب اختيار فرع");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            conn.Close();
            myConnection.Close();
            connectionReader1.Close();
        }

        private void btnNewChosen_Click(object sender, EventArgs e)
        {
            try
            {
                clearCom();
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
                if (comBranch.Text != "" && txtBranchID.Text != "" && txtBillNum.Text != "")
                {
                    printBill();
                }
                else
                {
                    MessageBox.Show("يجب ادخال البيانات كاملة");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            conn.Close();
            connectionReader2.Close();
        }

        //functions
        private void loadBranch()
        {
            conn.Open();
            string query = "select * from branch";
            MySqlDataAdapter da = new MySqlDataAdapter(query, conn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            comBranch.DataSource = dt;
            comBranch.DisplayMember = dt.Columns["Branch_Name"].ToString();
            comBranch.ValueMember = dt.Columns["Branch_ID"].ToString();
            comBranch.SelectedIndex = -1;

            loadedBranch = true;
        }

        public void search()
        {
            conn.Open();
            if (int.TryParse(txtBillNum.Text, out billNumber))
            {
                ID = -1;
                delegateName = "";

                string query = "select * from customer_bill where Branch_BillNumber=" + billNumber + " and Branch_ID=" + branchID + " and (Type_Buy='كاش' or Type_Buy='آجل')";
                MySqlCommand com = new MySqlCommand(query, conn);
                MySqlDataReader dr = com.ExecuteReader();

                while (dr.Read())
                {
                    flag2 = true;
                    ID = Convert.ToInt32(dr["CustomerBill_ID"].ToString());
                    TypeBuy = dr["Type_Buy"].ToString();
                    billDate = Convert.ToDateTime(dr["Bill_Date"].ToString());
                    dateTimePicker1.Value = billDate;

                    connectionReader1.Open();
                    string q1 = "SELECT distinct delegate.Delegate_Name FROM customer_bill INNER JOIN product_bill ON product_bill.CustomerBill_ID = customer_bill.CustomerBill_ID INNER JOIN delegate ON delegate.Delegate_ID = product_bill.Delegate_ID where customer_bill.CustomerBill_ID=" + ID;
                    MySqlCommand c1 = new MySqlCommand(q1, connectionReader1);
                    MySqlDataReader dr1 = c1.ExecuteReader();
                    while (dr1.Read())
                    {
                        if (delegateName != "")
                        {
                            delegateName += ",";
                        }
                        delegateName += dr1["Delegate_Name"].ToString();
                    }
                    dr1.Close();
                    connectionReader1.Close();

                    myConnection.Open();
                    string query3 = "SELECT users.User_Name FROM customer_bill INNER JOIN users ON users.User_ID = customer_bill.Employee_ID where customer_bill.CustomerBill_ID=" + ID;
                    MySqlCommand com2 = new MySqlCommand(query3, myConnection);
                    if (com2.ExecuteScalar() != null)
                    {
                        ConfirmEmp = com2.ExecuteScalar().ToString();
                    }
                    myConnection.Close();
                    
                    totalCostBD = Convert.ToDouble(dr["Total_CostBD"].ToString());
                    totalDiscount = Convert.ToDouble(dr["Total_Discount"].ToString());
                    totalCostAD = Convert.ToDouble(dr["Total_CostAD"].ToString());
                    
                    txtTotal.Text = dr["Total_CostBD"].ToString();
                    txtDiscount.Text = dr["Total_Discount"].ToString();
                    txtFinal.Text = dr["Total_CostAD"].ToString();

                    if (dr["Customer_ID"].ToString() != "")
                    {
                        customerID = Convert.ToInt32(dr["Customer_ID"].ToString());
                        engName = dr["Customer_Name"].ToString();
                        comClient.Text = dr["Customer_Name"].ToString();
                        comClient.SelectedValue = customerID;
                        txtClientId.Text = customerID.ToString();
                    }
                    else
                    {
                        customerID = 0;
                        engName = "";
                    }
                    if (dr["Client_ID"].ToString() != "")
                    {
                        clientID = Convert.ToInt32(dr["Client_ID"].ToString());
                        clientName = dr["Client_Name"].ToString();
                        comClient.Text = dr["Client_Name"].ToString();
                        comClient.SelectedValue = clientID;
                        txtClientId.Text = clientID.ToString();
                    }
                    else
                    {
                        clientID = 0;
                        clientName = "";
                    }
                }
                dr.Close();
                if (flag2 == true)
                {
                    //extract customer info
                    if (clientID > 0)
                    {
                        query = "select * from customer inner join customer_phone on customer.Customer_ID=customer_phone.Customer_ID where customer.Customer_ID=" + clientID + " order by customer_phone.CustomerPhone_ID desc limit 1";
                        com = new MySqlCommand(query, conn);
                        dr = com.ExecuteReader();
                        while (dr.Read())
                        {
                            clientPhoneNumber = dr["Phone"].ToString();
                        }
                        dr.Close();
                    }
                    else
                    {
                        clientPhoneNumber = "";
                    }
                    if (customerID > 0)
                    {
                        query = "select * from customer inner join customer_phone on customer.Customer_ID=customer_phone.Customer_ID where customer.Customer_ID=" + customerID + " order by customer_phone.CustomerPhone_ID desc limit 1";
                        com = new MySqlCommand(query, conn);
                        dr = com.ExecuteReader();
                        while (dr.Read())
                        {
                            customerPhoneNumber = dr["Phone"].ToString();
                        }
                        dr.Close();
                    }
                    else
                    {
                        customerPhoneNumber = "";
                    }

                    query = "select data.Code as 'الكود',type.Type_Name as 'النوع',concat(product.Product_Name,' - ',factory.Factory_Name,' - ',groupo.Group_Name,' ',COALESCE(color.Color_Name,''),' ',COALESCE(size.Size_Value,''),' ',COALESCE(sort.Sort_Value,'')) as 'الاسم',product_bill.Type as 'الفئة',product_bill.Quantity as 'الكمية',product_bill.Price as 'السعر',product_bill.Discount as 'نسبة الخصم',product_bill.PriceAD as 'بعد الخصم',product_bill.Cartons as 'اجمالى الكراتين',store.Store_Name as 'المخزن' from product_bill INNER JOIN customer_bill ON product_bill.CustomerBill_ID = customer_bill.CustomerBill_ID INNER JOIN store ON store.Store_ID = product_bill.Store_ID inner join data on data.Data_ID=product_bill.Data_ID LEFT JOIN color ON color.Color_ID = data.Color_ID LEFT JOIN size ON size.Size_ID = data.Size_ID LEFT JOIN sort ON sort.Sort_ID = data.Sort_ID INNER JOIN type ON type.Type_ID = data.Type_ID INNER JOIN product ON product.Product_ID = data.Product_ID INNER JOIN factory ON data.Factory_ID = factory.Factory_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID  where product_bill.CustomerBill_ID=" + ID + " and product_bill.Type='بند'  and (product_bill.Returned='لا' or product_bill.Returned='جزء') and data.Data_ID=0";
                    MySqlDataAdapter da = new MySqlDataAdapter(query, conn);
                    DataTable dtProduct = new DataTable();
                    da.Fill(dtProduct);
                    gridControl1.DataSource = dtProduct;
                    
                    query = "SELECT product_bill.Data_ID,product_bill.Type as 'الفئة',product_bill.Price as 'السعر',product_bill.Discount as 'نسبة الخصم',product_bill.PriceAD as 'بعد الخصم',product_bill.Quantity as 'الكمية',product_bill.Store_Name as 'المخزن',product_bill.Cartons as 'اجمالى الكراتين' FROM product_bill where product_bill.CustomerBill_ID=" + ID;
                    com = new MySqlCommand(query, conn);
                    dr = com.ExecuteReader();
                    while (dr.Read())
                    {
                        connectionReader1.Open();
                        if (dr["الفئة"].ToString() == "بند")
                        {
                            string q = "SELECT data.Code as 'الكود',type.Type_Name as 'النوع',concat(product.Product_Name,' ',COALESCE(color.Color_Name,''),' ',data.Description,' ',groupo.Group_Name,' ',factory.Factory_Name,' ',COALESCE(size.Size_Value,''),' ',COALESCE(sort.Sort_Value,'')) as 'الاسم' FROM data INNER JOIN type ON data.Type_ID = type.Type_ID INNER JOIN product ON product.Product_ID = data.Product_ID INNER JOIN factory ON factory.Factory_ID = data.Factory_ID INNER JOIN groupo ON groupo.Group_ID = data.Group_ID LEFT JOIN color ON data.Color_ID = color.Color_ID LEFT JOIN size ON data.Size_ID = size.Size_ID LEFT JOIN sort ON data.Sort_ID = sort.Sort_ID where Data_ID=" + dr["Data_ID"].ToString();
                            MySqlCommand c = new MySqlCommand(q, connectionReader1);
                            MySqlDataReader dr1 = c.ExecuteReader();
                            while (dr1.Read())
                            {
                                gridView1.AddNewRow();
                                int rowHandle = gridView1.GetRowHandle(gridView1.DataRowCount);
                                if (gridView1.IsNewItemRow(rowHandle))
                                {
                                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns["الكود"], dr1["الكود"].ToString());
                                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns["الفئة"], "بند");
                                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns["الاسم"], dr1["الاسم"].ToString());
                                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns["النوع"], dr1["النوع"].ToString());
                                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns["الكمية"], dr["الكمية"].ToString());
                                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns["السعر"], dr["السعر"].ToString());
                                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns["بعد الخصم"], dr["بعد الخصم"].ToString());
                                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns["نسبة الخصم"], dr["نسبة الخصم"].ToString());
                                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns["اجمالى الكراتين"], dr["اجمالى الكراتين"].ToString());
                                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns["المخزن"], dr["المخزن"].ToString());
                                }
                            }
                            dr1.Close();
                        }
                        else if (dr["الفئة"].ToString() == "طقم")
                        {
                            string q = "SELECT sets.Set_ID as 'الكود',sets.Set_Name as 'الاسم' FROM sets where Set_ID=" + dr["Data_ID"].ToString();
                            MySqlCommand c = new MySqlCommand(q, connectionReader1);
                            MySqlDataReader dr1 = c.ExecuteReader();
                            while (dr1.Read())
                            {
                                gridView1.AddNewRow();
                                int rowHandle = gridView1.GetRowHandle(gridView1.DataRowCount);
                                if (gridView1.IsNewItemRow(rowHandle))
                                {
                                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns["الكود"], dr1["الكود"].ToString());
                                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns["الفئة"], "طقم");
                                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns["النوع"], "طقم");
                                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns["الاسم"], dr1["الاسم"].ToString());
                                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns["الكمية"], dr["الكمية"].ToString());
                                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns["السعر"], dr["السعر"].ToString());
                                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns["بعد الخصم"], dr["بعد الخصم"].ToString());
                                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns["نسبة الخصم"], dr["نسبة الخصم"].ToString());
                                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns["اجمالى الكراتين"], dr["اجمالى الكراتين"].ToString());
                                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns["المخزن"], dr["المخزن"].ToString());
                                }
                            }
                            dr1.Close();
                        }
                        else if (dr["الفئة"].ToString() == "عرض")
                        {
                            string q = "SELECT offer.Offer_ID as 'الكود',offer.Offer_Name as 'الاسم' FROM offer where Offer_ID=" + dr["Data_ID"].ToString();
                            MySqlCommand c = new MySqlCommand(q, connectionReader1);
                            MySqlDataReader dr1 = c.ExecuteReader();
                            while (dr1.Read())
                            {
                                gridView1.AddNewRow();
                                int rowHandle = gridView1.GetRowHandle(gridView1.DataRowCount);
                                if (gridView1.IsNewItemRow(rowHandle))
                                {
                                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns["الكود"], dr1["الكود"].ToString());
                                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns["الفئة"], "عرض");
                                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns["النوع"], "عرض");
                                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns["الاسم"], dr1["الاسم"].ToString());
                                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns["الكمية"], dr["الكمية"].ToString());
                                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns["السعر"], dr["السعر"].ToString());
                                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns["بعد الخصم"], dr["بعد الخصم"].ToString());
                                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns["نسبة الخصم"], dr["نسبة الخصم"].ToString());
                                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns["اجمالى الكراتين"], dr["اجمالى الكراتين"].ToString());
                                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns["المخزن"], dr["المخزن"].ToString());
                                }
                            }
                            dr1.Close();
                        }
                        connectionReader1.Close();
                    }
                    conn.Close();
                    
                    gridView1.Columns["الفئة"].Visible = false;
                    if (gridView1.IsLastVisibleRow)
                    {
                        gridView1.FocusedRowHandle = gridView1.RowCount - 1;
                    }

                    if (!loaded)
                    {
                        for (int i = 0; i < gridView1.Columns.Count; i++)
                        {
                            gridView1.Columns[i].Width = 110;
                        }
                    }
                    loaded = true;

                    flag2 = false;
                }
                else
                {
                    clearCom();
                    MessageBox.Show("لا يوجد فاتورة بهذا الرقم فى الفرع");
                }
            }
            else
            {
                clearCom();
                MessageBox.Show("رقم الفاتورة يجب ان يكون رقم");
            }
        }

        public void clearCom()
        {
            foreach (Control co in this.tableLayoutPanel3.Controls)
            {
                if (co is System.Windows.Forms.ComboBox)
                {
                    co.Text = "";
                }
                else if (co is TextBox)
                {
                    co.Text = "";
                }
            }
            foreach (Control co in this.tableLayoutPanel4.Controls)
            {
                if (co is System.Windows.Forms.ComboBox)
                {
                    co.Text = "";
                }
                else if (co is TextBox)
                {
                    co.Text = "";
                }
                else if (co is DateTimePicker)
                {
                    dateTimePicker1.Value = DateTime.Now;
                }
            }
            txtTotal.Text = "";
            txtDiscount.Text = "";
            txtFinal.Text = "";
            gridControl1.DataSource = null;
        }

        void printBill()
        {
            List<Copy_Bill_Items> bi = new List<Copy_Bill_Items>();

            for (int i = 0; i < gridView1.RowCount; i++)
            {
                if (gridView1.GetRowCellDisplayText(i, gridView1.Columns["الفئة"]) == "بند")
                {
                    Copy_Bill_Items item = new Copy_Bill_Items() { Code = gridView1.GetRowCellDisplayText(i, gridView1.Columns["الكود"]), Product_Type = gridView1.GetRowCellDisplayText(i, gridView1.Columns["الفئة"]), Product_Name = gridView1.GetRowCellDisplayText(i, gridView1.Columns["الاسم"]), Quantity = Convert.ToDouble(gridView1.GetRowCellDisplayText(i, gridView1.Columns["الكمية"])), Cost = Convert.ToDouble(gridView1.GetRowCellDisplayText(i, gridView1.Columns["السعر"])), Total_Cost = Convert.ToDouble(gridView1.GetRowCellDisplayText(i, gridView1.Columns["السعر"])) * Convert.ToDouble(gridView1.GetRowCellDisplayText(i, gridView1.Columns["الكمية"])), Store_Name = gridView1.GetRowCellDisplayText(i, gridView1.Columns["المخزن"]), Carton = Convert.ToInt32(gridView1.GetRowCellDisplayText(i, gridView1.Columns["اجمالى الكراتين"])), Type = gridView1.GetRowCellDisplayText(i, gridView1.Columns["النوع"]), Discount = gridView1.GetRowCellDisplayText(i, gridView1.Columns["نسبة الخصم"]) };
                    bi.Add(item);
                }
                else if (gridView1.GetRowCellDisplayText(i, gridView1.Columns["الفئة"]) == "عرض")
                {
                    conn.Open();
                    connectionReader2.Open();
                    string q = "SELECT offer.Offer_ID,offer.Offer_Name,offer.Description FROM offer where Offer_ID=" + gridView1.GetRowCellDisplayText(i, gridView1.Columns["الكود"]);
                    MySqlCommand c = new MySqlCommand(q, conn);
                    MySqlDataReader dr1 = c.ExecuteReader();
                    while (dr1.Read())
                    {
                        string itemName = "concat(product.Product_Name,' ',type.Type_Name,' ',factory.Factory_Name,' ',groupo.Group_Name,' ' ,COALESCE(color.Color_Name,''),' ',COALESCE(size.Size_Value,''),' ',COALESCE(sort.Sort_Value,''),' ',COALESCE(data.Classification,''),' ',COALESCE(data.Description,''))as 'البند'";
                        string DataTableRelations = "INNER JOIN type ON type.Type_ID = data.Type_ID INNER JOIN product ON product.Product_ID = data.Product_ID INNER JOIN factory ON data.Factory_ID = factory.Factory_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID LEFT outer JOIN color ON data.Color_ID = color.Color_ID LEFT outer  JOIN size ON data.Size_ID = size.Size_ID LEFT outer  JOIN sort ON data.Sort_ID = sort.Sort_ID";
                        string query3 = "select Code as 'الكود'," + itemName + " from offer inner join offer_details on offer.Offer_ID=offer_details.Offer_ID inner join data on data.Data_ID=offer_details.Data_ID " + DataTableRelations + "  where offer.Offer_ID=" + dr1["Offer_ID"];
                        MySqlCommand com3 = new MySqlCommand(query3, connectionReader2);
                        MySqlDataReader dr3 = com3.ExecuteReader();
                        string str = "";
                        int cont = 1;
                        while (dr3.Read())
                        {
                            str += cont + "-" + dr3[1].ToString() + "\n";
                            cont++;
                        }
                        dr3.Close();
                        Copy_Bill_Items item = new Copy_Bill_Items() { Code = gridView1.GetRowCellDisplayText(i, gridView1.Columns["الكود"]), Type = dr1["Description"].ToString(), Product_Type = gridView1.GetRowCellDisplayText(i, gridView1.Columns["الفئة"])/*, Product_Name = gridView1.GetRowCellDisplayText(i, gridView1.Columns["الاسم"])*/, Quantity = Convert.ToDouble(gridView1.GetRowCellDisplayText(i, gridView1.Columns["الكمية"])), Cost = Convert.ToDouble(gridView1.GetRowCellDisplayText(i, gridView1.Columns["السعر"])), Total_Cost = Convert.ToDouble(gridView1.GetRowCellDisplayText(i, gridView1.Columns["السعر"])) * Convert.ToDouble(gridView1.GetRowCellDisplayText(i, gridView1.Columns["الكمية"])), Store_Name = gridView1.GetRowCellDisplayText(i, gridView1.Columns["المخزن"]), Carton = Convert.ToInt32(gridView1.GetRowCellDisplayText(i, gridView1.Columns["اجمالى الكراتين"])), Discount = gridView1.GetRowCellDisplayText(i, gridView1.Columns["نسبة الخصم"]) };
                        item.Product_Name = "-" + gridView1.GetRowCellDisplayText(i, gridView1.Columns["الاسم"]) + "\n" + str;
                        bi.Add(item);
                    }
                    dr1.Close();
                }
            }

            Print_CopyBill_Report f = new Print_CopyBill_Report();
            if (clientID > 0 && customerID > 0)
            {
                f.PrintInvoice(clientName + " " + clientID, engName + " " + customerID, clientPhoneNumber, delegateName + " - " /*+ "("*/ + TypeBuy /*+ ")"*/, billDate, TypeBuy, billNumber, branchID.ToString(), branchName, totalCostBD, totalCostAD, totalDiscount, bi);
            }
            else if (clientID > 0)
            {
                f.PrintInvoice(clientName + " " + clientID, "", clientPhoneNumber, delegateName + " - " /*+ "("*/ + TypeBuy /*+ ")"*/, billDate, TypeBuy, billNumber, branchID.ToString(), branchName, totalCostBD, totalCostAD, totalDiscount, bi);
            }
            else if (customerID > 0)
            {
                f.PrintInvoice(engName + " " + customerID, "", customerPhoneNumber, delegateName + " - " /*+ "("*/ + TypeBuy /*+ ")"*/, billDate, TypeBuy, billNumber, branchID.ToString(), branchName, totalCostBD, totalCostAD, totalDiscount, bi);
            }
            f.ShowDialog();
        }
    }
}

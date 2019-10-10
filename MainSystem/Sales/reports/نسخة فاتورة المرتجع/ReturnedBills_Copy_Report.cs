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

namespace MainSystem
{
    public partial class ReturnedBills_Copy_Report : Form
    {
        MySqlConnection conn;
        MySqlConnection connectionReader1;
        MySqlConnection myConnection;
        MainForm bankMainForm = null;
        XtraTabControl MainTabControlBank;
        string delegateName = "";
        public static XtraTabPage MainTabPagePrintingTransitions;
        Panel panelPrintingTransitions;

        public static BillsTransitions_Print bankPrint;

        public static GridControl gridcontrol;
        bool loaded = false;
        bool loadedBranch = false;
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
        double totalCostAD = 0;
        string returnInfo = "";

        public ReturnedBills_Copy_Report(MainForm BankMainForm)
        {
            InitializeComponent();
            conn = new MySqlConnection(connection.connectionString);
            connectionReader1 = new MySqlConnection(connection.connectionString);
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

                string query = "select * from customer_return_bill where Branch_BillNumber=" + billNumber + " and Branch_ID=" + branchID + " and (Type_Buy='كاش' or Type_Buy='آجل')";
                MySqlCommand com = new MySqlCommand(query, conn);
                MySqlDataReader dr = com.ExecuteReader();

                while (dr.Read())
                {
                    flag2 = true;
                    ID = Convert.ToInt32(dr["CustomerReturnBill_ID"].ToString());
                    TypeBuy = dr["Type_Buy"].ToString();
                    billDate = Convert.ToDateTime(dr["Date"].ToString());
                    returnInfo = dr["ReturnInfo"].ToString();
                    dateTimePicker1.Value = billDate;

                    myConnection.Open();
                    string query3 = "SELECT users.User_Name FROM customer_return_bill INNER JOIN users ON users.User_ID = customer_return_bill.Employee_ID where customer_return_bill.CustomerReturnBill_ID=" + ID;
                    MySqlCommand com2 = new MySqlCommand(query3, myConnection);
                    if (com2.ExecuteScalar() != null)
                    {
                        ConfirmEmp = com2.ExecuteScalar().ToString();
                    }
                    myConnection.Close();
                    
                    totalCostAD = Convert.ToDouble(dr["TotalCostAD"].ToString());
                    txtFinal.Text = dr["TotalCostAD"].ToString();

                    if (dr["Customer_ID"].ToString() != "")
                    {
                        customerID = Convert.ToInt32(dr["Customer_ID"].ToString());
                        engName = dr["Customer_Name"].ToString();
                        comClient.Text = dr["Customer_Name"].ToString();
                        comClient.SelectedValue = customerID;
                        txtClientId.Text = customerID.ToString();
                    }
                    if (dr["Client_ID"].ToString() != "")
                    {
                        clientID = Convert.ToInt32(dr["Client_ID"].ToString());
                        clientName = dr["Client_Name"].ToString();
                        comClient.Text = dr["Client_Name"].ToString();
                        comClient.SelectedValue = clientID;
                        txtClientId.Text = clientID.ToString();
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
                    else if (customerID > 0)
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

                    query = "select data.Code as 'الكود',type.Type_Name as 'النوع',concat(product.Product_Name,' - ',factory.Factory_Name,' - ',groupo.Group_Name,' ',COALESCE(color.Color_Name,''),' ',COALESCE(size.Size_Value,''),' ',COALESCE(sort.Sort_Value,'')) as 'الاسم',customer_return_bill_details.Type as 'الفئة',customer_return_bill_details.TotalMeter as 'الكمية',customer_return_bill_details.PriceBD as 'السعر',customer_return_bill_details.SellDiscount as 'نسبة الخصم',customer_return_bill_details.PriceAD as 'بعد الخصم',((customer_return_bill_details.SellDiscount*customer_return_bill_details.PriceBD)/100)*customer_return_bill_details.TotalMeter as 'SellDiscount' from customer_return_bill_details INNER JOIN customer_return_bill ON customer_return_bill_details.CustomerReturnBill_ID = customer_return_bill.CustomerReturnBill_ID inner join data on data.Data_ID=customer_return_bill_details.Data_ID LEFT JOIN color ON color.Color_ID = data.Color_ID LEFT JOIN size ON size.Size_ID = data.Size_ID LEFT JOIN sort ON sort.Sort_ID = data.Sort_ID INNER JOIN type ON type.Type_ID = data.Type_ID INNER JOIN product ON product.Product_ID = data.Product_ID INNER JOIN factory ON data.Factory_ID = factory.Factory_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID  where customer_return_bill_details.CustomerReturnBill_ID=0 and data.Data_ID=0";
                    MySqlDataAdapter da = new MySqlDataAdapter(query, conn);
                    DataTable dtProduct = new DataTable();
                    da.Fill(dtProduct);
                    gridControl1.DataSource = dtProduct;


                    query = "SELECT customer_return_bill_details.Data_ID,customer_return_bill_details.Type as 'الفئة',customer_return_bill_details.PriceBD as 'السعر',customer_return_bill_details.SellDiscount as 'نسبة الخصم',customer_return_bill_details.PriceAD as 'بعد الخصم',customer_return_bill_details.TotalMeter as 'الكمية',((customer_return_bill_details.SellDiscount*customer_return_bill_details.PriceBD)/100)*customer_return_bill_details.TotalMeter as 'SellDiscount',delegate.Delegate_Name FROM customer_return_bill_details inner join delegate on  delegate.Delegate_ID=customer_return_bill_details.Delegate_ID where customer_return_bill_details.CustomerReturnBill_ID=" + ID;
                    com = new MySqlCommand(query, conn);
                    dr = com.ExecuteReader();
                    
                    while (dr.Read())
                    {
                        delegateName = dr["Delegate_Name"].ToString();
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
                                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns["SellDiscount"], dr["SellDiscount"].ToString());
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
                                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns["الاسم"], dr1["الاسم"].ToString());
                                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns["الكمية"], dr["الكمية"].ToString());
                                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns["السعر"], dr["السعر"].ToString());
                                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns["بعد الخصم"], dr["بعد الخصم"].ToString());
                                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns["نسبة الخصم"], dr["نسبة الخصم"].ToString());
                                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns["SellDiscount"], dr["SellDiscount"].ToString());
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
                                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns["الاسم"], dr1["الاسم"].ToString());
                                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns["الكمية"], dr["الكمية"].ToString());
                                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns["السعر"], dr["السعر"].ToString());
                                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns["بعد الخصم"], dr["بعد الخصم"].ToString());
                                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns["نسبة الخصم"], dr["نسبة الخصم"].ToString());
                                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns["SellDiscount"], 0);
                                }
                            }
                            dr1.Close();
                        }
                        connectionReader1.Close();
                    }
                    conn.Close();
                    
                    gridView1.Columns["الفئة"].Visible = false;
                    gridView1.Columns["SellDiscount"].Visible = false;
                    if (gridView1.IsLastVisibleRow)
                    {
                        gridView1.FocusedRowHandle = gridView1.RowCount - 1;
                    }

                    double totalB = 0;
                    double totalD = 0;
                    for (int i = 0; i < gridView1.RowCount; i++)
                    {
                        totalB += (Convert.ToDouble(gridView1.GetRowCellDisplayText(i, "السعر").ToString())* Convert.ToDouble(gridView1.GetRowCellDisplayText(i, "الكمية").ToString()));
                        totalD += Convert.ToDouble(gridView1.GetRowCellDisplayText(i, "SellDiscount").ToString());
                    }
                    txtTotal.Text = totalB.ToString();
                    txtDiscount.Text = totalD.ToString();

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
            List<CopyReturnedBill_Items> bi = new List<CopyReturnedBill_Items>();

            for (int i = 0; i < gridView1.RowCount; i++)
            {
                CopyReturnedBill_Items item = new CopyReturnedBill_Items() { Code = gridView1.GetRowCellDisplayText(i, gridView1.Columns["الكود"]), Product_Type = gridView1.GetRowCellDisplayText(i, gridView1.Columns["الفئة"]), Product_Name = gridView1.GetRowCellDisplayText(i, gridView1.Columns["الاسم"]), Quantity = Convert.ToDouble(gridView1.GetRowCellDisplayText(i, gridView1.Columns["الكمية"])), CostBD = Convert.ToDouble(gridView1.GetRowCellDisplayText(i, gridView1.Columns["السعر"])), Total_Cost = Convert.ToDouble(gridView1.GetRowCellDisplayText(i, gridView1.Columns["السعر"])) * Convert.ToDouble(gridView1.GetRowCellDisplayText(i, gridView1.Columns["الكمية"])), Type = gridView1.GetRowCellDisplayText(i, gridView1.Columns["النوع"]), Discount = Convert.ToDouble(gridView1.GetRowCellDisplayText(i, gridView1.Columns["SellDiscount"])), Cost = Convert.ToDouble(gridView1.GetRowCellDisplayText(i, gridView1.Columns["بعد الخصم"])) };
                bi.Add(item);
            }

            Print_CopyReturnedBill_Report f = new Print_CopyReturnedBill_Report();
            if (clientID > 0)
            {
                f.PrintInvoice(clientName + " " + clientID, clientPhoneNumber, billDate, TypeBuy, billNumber, comBranch.SelectedValue.ToString(), branchName, totalCostAD, returnInfo, bi, delegateName + " - " + TypeBuy);
            }
            else if (customerID > 0)
            {
                f.PrintInvoice(engName + " " + customerID, customerPhoneNumber, billDate, TypeBuy, billNumber, comBranch.SelectedValue.ToString(), branchName, totalCostAD, returnInfo, bi, delegateName + " - " + TypeBuy);
            }
            f.ShowDialog();
        }
    }
}

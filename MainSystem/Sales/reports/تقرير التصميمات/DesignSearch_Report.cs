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
    public partial class DesignSearch_Report : Form
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
        bool flag2 = false;

        public DesignSearch_Report(MainForm BankMainForm)
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
        }
        
        private void txtBillNum_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    if (txtBillNum.Text != "")
                    {
                        search();
                    }
                    else
                    {
                        MessageBox.Show("يجب ادخال رقم الديزاين");
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
                if (comBranch.Text != "" && txtBillNum.Text != "")
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
        public void search()
        {
            conn.Open();
            int billNumber = 0;
            if (int.TryParse(txtBillNum.Text, out billNumber))
            {
                string query = "select * from customer_design where customer_design.CustomerDesign_ID=" + billNumber;
                MySqlCommand com = new MySqlCommand(query, conn);
                MySqlDataReader dr = com.ExecuteReader();

                while (dr.Read())
                {
                    flag2 = true;
                    dateTimePicker1.Value = Convert.ToDateTime(dr["Date"].ToString());
                    
                    txtFinal.Text = dr["PaidMoney"].ToString();

                    comEngDesign.Text = dr["Engineer_Name"].ToString();
                    txtEngDesign.Text = dr["Engineer_ID"].ToString();

                    comDelegate.Text = dr["Delegate_Name"].ToString();
                    txtDelegate.Text = dr["Delegate_ID"].ToString();

                    comBranch.Text = dr["Branch_Name"].ToString();
                    txtBranch.Text = dr["Branch_ID"].ToString();

                    if (dr["Customer_ID"].ToString() != "")
                    {
                        comClient.Text = dr["Customer_Name"].ToString();
                        txtClientId.Text = dr["Customer_ID"].ToString();
                    }
                    else
                    {
                        comClient.Text = "";
                        txtClientId.Text = "";
                    }
                    if (dr["Client_ID"].ToString() != "")
                    {
                        comClient.Text = dr["Client_Name"].ToString();
                        txtClientId.Text = dr["Client_ID"].ToString();
                    }
                    else
                    {
                        comClient.Text = "";
                        txtClientId.Text = "";
                    }
                }
                dr.Close();
                if (flag2 == true)
                {
                    query = "SELECT customer_design_details.DesignLocation as 'موقع التصميم',customer_design_details.Space as 'المساحة',customer_design_details.NoItems as 'عدد الوحدات',customer_design_details.ItemCost as 'سعر الوحدة',customer_design_details.Total as 'الاجمالى' FROM customer_design_details INNER JOIN customer_design ON customer_design_details.CustomerDesignID = customer_design.CustomerDesign_ID where customer_design_details.CustomerDesignID=0";
                    MySqlDataAdapter da = new MySqlDataAdapter(query, conn);
                    DataTable dtProduct = new DataTable();
                    da.Fill(dtProduct);
                    gridControl1.DataSource = dtProduct;

                    query = "SELECT customer_design_details.DesignLocation,customer_design_details.Space,customer_design_details.NoItems,customer_design_details.ItemCost,customer_design_details.Total FROM customer_design_details INNER JOIN customer_design ON customer_design_details.CustomerDesignID = customer_design.CustomerDesign_ID where customer_design_details.CustomerDesignID=" + billNumber;
                    com = new MySqlCommand(query, conn);
                    dr = com.ExecuteReader();
                    while (dr.Read())
                    {
                        gridView1.AddNewRow();
                        int rowHandle = gridView1.GetRowHandle(gridView1.DataRowCount);
                        if (gridView1.IsNewItemRow(rowHandle))
                        {
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns["موقع التصميم"], dr["DesignLocation"].ToString());
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns["المساحة"], dr["Space"].ToString());
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns["عدد الوحدات"], dr["NoItems"].ToString());
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns["سعر الوحدة"], dr["ItemCost"].ToString());
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns["الاجمالى"], dr["Total"].ToString());
                        }
                    }
                    dr.Close();
                    conn.Close();
                    
                    if (gridView1.IsLastVisibleRow)
                    {
                        gridView1.FocusedRowHandle = gridView1.RowCount - 1;
                    }
                    
                        for (int i = 0; i < gridView1.Columns.Count; i++)
                        {
                            gridView1.Columns[i].Width = 110;
                        }

                    flag2 = false;
                }
                else
                {
                    clearCom();
                    MessageBox.Show("لا يوجد تصميم بهذا الرقم");
                }
            }
            else
            {
                clearCom();
                MessageBox.Show("رقم التصميم يجب ان يكون عدد");
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
            //if (clientID > 0 && customerID > 0)
            //{
            //    f.PrintInvoice(clientName + " " + clientID, engName + " " + customerID, clientPhoneNumber, delegateName + " - " /*+ "("*/ + TypeBuy /*+ ")"*/, Date, TypeBuy, billNumber, branchID.ToString(), branchName, totalCostBD, totalCostAD, totalDiscount, bi);
            //}
            //else if (clientID > 0)
            //{
            //    f.PrintInvoice(clientName + " " + clientID, "", clientPhoneNumber, delegateName + " - " /*+ "("*/ + TypeBuy /*+ ")"*/, Date, TypeBuy, billNumber, branchID.ToString(), branchName, totalCostBD, totalCostAD, totalDiscount, bi);
            //}
            //else if (customerID > 0)
            //{
            //    f.PrintInvoice(engName + " " + customerID, "", customerPhoneNumber, delegateName + " - " /*+ "("*/ + TypeBuy /*+ ")"*/, Date, TypeBuy, billNumber, branchID.ToString(), branchName, totalCostBD, totalCostAD, totalDiscount, bi);
            //}
            f.ShowDialog();
        }
    }
}

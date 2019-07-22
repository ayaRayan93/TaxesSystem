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

namespace MainSystem
{
    public partial class SalesProductsBillsNum_Report : Form
    {
        MySqlConnection conn, conn2;
        MainForm bankMainForm = null;

        public static XtraTabPage MainTabPagePrintingTransitions;
        Panel panelPrintingTransitions;

        public static BillsTransitions_Print bankPrint;

        public static GridControl gridcontrol;
        bool loaded = false;
        bool loadedBranch = false;

        public SalesProductsBillsNum_Report(MainForm BankMainForm)
        {
            InitializeComponent();
            conn = new MySqlConnection(connection.connectionString);
            conn2 = new MySqlConnection(connection.connectionString);
            bankMainForm = BankMainForm;

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

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (comBranch.Text != "" && txtBranchID.Text != "" && txtFromBill.Text != "" && txtToBill.Text != "")
                {
                    search();
                }
                else
                {
                    MessageBox.Show("يجب ادخال جميع البيانات");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            conn.Close();
            conn2.Close();
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
            conn2.Open();

            string query = "select data.Code as 'الكود',type.Type_Name as 'النوع',concat(product.Product_Name,' ',COALESCE(color.Color_Name,''),' ',data.Description,' ',groupo.Group_Name,' ',factory.Factory_Name,' ',COALESCE(size.Size_Value,''),' ',COALESCE(sort.Sort_Value,'')) as 'الاسم',product_bill.Quantity as 'الكمية' FROM customer_bill INNER JOIN product_bill ON product_bill.CustomerBill_ID = customer_bill.CustomerBill_ID inner join data on data.Data_ID=product_bill.Data_ID LEFT JOIN color ON color.Color_ID = data.Color_ID LEFT JOIN size ON size.Size_ID = data.Size_ID LEFT JOIN sort ON sort.Sort_ID = data.Sort_ID INNER JOIN type ON type.Type_ID = data.Type_ID INNER JOIN product ON product.Product_ID = data.Product_ID INNER JOIN factory ON data.Factory_ID = factory.Factory_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID  where customer_bill.Branch_ID=" + comBranch.SelectedValue.ToString() + " and customer_bill.Branch_BillNumber >= " + txtFromBill.Text + " and customer_bill.Branch_BillNumber <=" + txtToBill.Text + " and data.Data_ID=0";
            MySqlDataAdapter da = new MySqlDataAdapter(query, conn);
            DataTable dtProduct = new DataTable();
            da.Fill(dtProduct);
            gridControl1.DataSource = dtProduct;

            query = "select data.Data_ID,data.Code as 'الكود',type.Type_Name as 'النوع',concat(product.Product_Name,' ',COALESCE(color.Color_Name,''),' ',data.Description,' ',groupo.Group_Name,' ',factory.Factory_Name,' ',COALESCE(size.Size_Value,''),' ',COALESCE(sort.Sort_Value,'')) as 'الاسم',sum(product_bill.Quantity) as 'الكمية' FROM customer_bill INNER JOIN product_bill ON product_bill.CustomerBill_ID = customer_bill.CustomerBill_ID inner join data on data.Data_ID=product_bill.Data_ID LEFT JOIN color ON color.Color_ID = data.Color_ID LEFT JOIN size ON size.Size_ID = data.Size_ID LEFT JOIN sort ON sort.Sort_ID = data.Sort_ID INNER JOIN type ON type.Type_ID = data.Type_ID INNER JOIN product ON product.Product_ID = data.Product_ID INNER JOIN factory ON data.Factory_ID = factory.Factory_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID  where customer_bill.Branch_ID=" + comBranch.SelectedValue.ToString() + "  and customer_bill.Branch_BillNumber >= " + txtFromBill.Text + " and customer_bill.Branch_BillNumber <=" + txtToBill.Text + " group by product_bill.Data_ID";
            MySqlCommand c = new MySqlCommand(query, conn);
            MySqlDataReader dataReader1 = c.ExecuteReader();
            while (dataReader1.Read())
            {
                gridView1.AddNewRow();
                int rowHandle = gridView1.GetRowHandle(gridView1.DataRowCount);
                if (gridView1.IsNewItemRow(rowHandle))
                {
                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns["الكود"], dataReader1["الكود"].ToString());
                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns["النوع"], dataReader1["النوع"].ToString());
                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns["الاسم"], dataReader1["الاسم"].ToString());
                    double totalQuantity = 0;
                    double returnedQuantity = 0;
                    //customer_return_bill_details.CustomerBill_ID=" + dataReader1["CustomerBill_ID"].ToString() + " and
                    query = "SELECT sum(customer_return_bill_details.TotalMeter) as 'الكمية' FROM customer_return_bill INNER JOIN customer_return_bill_details ON customer_return_bill_details.CustomerReturnBill_ID = customer_return_bill.CustomerReturnBill_ID where customer_return_bill.Branch_ID=" + comBranch.SelectedValue.ToString() + " and customer_return_bill.Branch_BillNumber >= " + txtFromBill.Text + " and customer_return_bill.Branch_BillNumber <=" + txtToBill.Text + " and customer_return_bill_details.Data_ID=" + dataReader1["Data_ID"].ToString() + " and customer_return_bill_details.Type='بند' group by customer_return_bill_details.Data_ID";
                    MySqlCommand com = new MySqlCommand(query, conn2);
                    //com.ExecuteScalar().ToString() != "" || 
                    if (com.ExecuteScalar() != null)
                    {
                        returnedQuantity = Convert.ToDouble(com.ExecuteScalar().ToString());
                    }
                    else
                    {
                        returnedQuantity = 0;
                    }

                    try
                    {
                        totalQuantity = Convert.ToDouble(dataReader1["الكمية"].ToString());
                    }
                    catch { }

                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns["الكمية"], totalQuantity - returnedQuantity);
                }
            }

            if (gridView1.IsLastVisibleRow)
            {
                gridView1.FocusedRowHandle = gridView1.RowCount - 1;
            }
        }

        private void btnNewChosen_Click(object sender, EventArgs e)
        {
            try
            {
                clearCom();
                gridControl1.DataSource = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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
        }

        private void comBranch_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (loadedBranch)
                {
                    int branchID = 0;
                    if (int.TryParse(comBranch.SelectedValue.ToString(), out branchID))
                    {
                        txtBranchID.Text = comBranch.SelectedValue.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnBillReport_Click(object sender, EventArgs e)
        {
            try
            {
                if (comBranch.Text != "" && txtBranchID.Text != "" && gridView1.RowCount > 0)
                {
                    List<Items_Bills> bi = new List<Items_Bills>();

                    for (int i = 0; i < gridView1.RowCount; i++)
                    {
                        Items_Bills item = new Items_Bills() { Code = gridView1.GetRowCellDisplayText(i, gridView1.Columns["الكود"]), Product_Type = gridView1.GetRowCellDisplayText(i, gridView1.Columns["النوع"]), Product_Name = gridView1.GetRowCellDisplayText(i, gridView1.Columns["الاسم"]), Quantity = Convert.ToDouble(gridView1.GetRowCellDisplayText(i, gridView1.Columns["الكمية"])) };
                        bi.Add(item);
                    }

                    Report_Items_BillsNum f = new Report_Items_BillsNum();
                    f.PrintInvoice(comBranch.Text, txtFromBill.Text, txtToBill.Text, bi);
                    f.ShowDialog();
                }
                else
                {
                    MessageBox.Show("يجب اختيار فرع والتاكد من وجود بنود");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}

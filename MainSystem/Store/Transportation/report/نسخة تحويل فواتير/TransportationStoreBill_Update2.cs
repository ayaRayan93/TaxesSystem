using DevExpress.XtraGrid.Views.Grid;
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
    public partial class TransportationStoreBill_Update2 : Form
    {
        MySqlConnection dbconnection, dbconnection2;
        DataRow row1;
        int TransferProductID = 0;
        string FromStore = "";
        string ToStore = "";
        DateTime Date = new DateTime();
        XtraTabControl tabControlStoresContent = null;

        public TransportationStoreBill_Update2(int transferProductID, string fromStore, string toStore, DateTime date, Transportation_Report transportationReport, XtraTabControl xtraTabControlStoresContent)
        {
            InitializeComponent();
            dbconnection = new MySqlConnection(connection.connectionString);
            dbconnection2 = new MySqlConnection(connection.connectionString);
            TransferProductID = transferProductID;
            FromStore = fromStore;
            ToStore = toStore;
            Date = date;
            tabControlStoresContent = xtraTabControlStoresContent;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                string query = "select * from Store";
                MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                DataTable dt = new DataTable();
                da.Fill(dt);
                comFromStore.DataSource = dt;
                comFromStore.DisplayMember = dt.Columns["Store_Name"].ToString();
                comFromStore.ValueMember = dt.Columns["Store_ID"].ToString();
                comFromStore.SelectedIndex = -1;
                comFromStore.SelectedValue = FromStore;

                query = "select * from Store where Store_ID<>" + comFromStore.SelectedValue.ToString();
                da = new MySqlDataAdapter(query, dbconnection);
                dt = new DataTable();
                da.Fill(dt);
                comToStore.DataSource = dt;
                comToStore.DisplayMember = dt.Columns["Store_Name"].ToString();
                comToStore.ValueMember = dt.Columns["Store_ID"].ToString();
                comToStore.SelectedIndex = -1;
                comToStore.SelectedValue = ToStore;
                
                query = "select * from branch";
                da = new MySqlDataAdapter(query, dbconnection);
                dt = new DataTable();
                da.Fill(dt);
                comBranch.DataSource = dt;
                comBranch.DisplayMember = dt.Columns["Branch_Name"].ToString();
                comBranch.ValueMember = dt.Columns["Branch_ID"].ToString();
                comBranch.Text = "";
                
                query = "SELECT data.Data_ID,data.Code as 'الكود',type.Type_Name as 'النوع',concat(product.Product_Name, ' ', COALESCE(color.Color_Name, ''), ' ', data.Description, ' ', groupo.Group_Name, ' ', factory.Factory_Name, ' ', COALESCE(size.Size_Value, ''), ' ', COALESCE(sort.Sort_Value, '')) as 'الاسم',data.Carton as 'الكرتنة',transfer_product_details.Quantity as 'الكمية',concat(customer_bill.Branch_BillNumber,' ',customer_bill.Branch_Name) as 'فاتورة',customer_bill.CustomerBill_ID FROM transfer_product_details INNER JOIN transfer_product ON transfer_product_details.TransferProduct_ID = transfer_product.TransferProduct_ID left JOIN store as storeTo ON storeTo.Store_ID = transfer_product.To_Store left join store as storeFrom on storeFrom.Store_ID = transfer_product.From_Store left JOIN customer_bill ON transfer_product_details.CustomerBill_ID = customer_bill.CustomerBill_ID INNER JOIN data ON transfer_product_details.Data_ID = data.Data_ID LEFT JOIN color ON color.Color_ID = data.Color_ID LEFT JOIN size ON size.Size_ID = data.Size_ID LEFT JOIN sort ON sort.Sort_ID = data.Sort_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID INNER JOIN factory ON factory.Factory_ID = data.Factory_ID  INNER JOIN product ON product.Product_ID = data.Product_ID  INNER JOIN type ON type.Type_ID = data.Type_ID where transfer_product.TransferProduct_ID=" + TransferProductID + " and transfer_product.Canceled=0 order by SUBSTR(data.Code, 1, 16),color.Color_Name,data.Description,data.Sort_ID";
                da = new MySqlDataAdapter(query, dbconnection);
                dt = new DataTable();
                da.Fill(dt);
                gridControl2.DataSource = dt;
                gridView2.Columns[0].Visible = false;
                gridView2.Columns["CustomerBill_ID"].Visible = false;
                gridView2.Columns["الاسم"].Width = 300;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (comFromStore.Text != "" && txtBillNum.Text != "" && comBranch.Text != "")
                {
                    dbconnection.Open();
                    string query = "select data.Data_ID,data.Code as 'الكود',type.Type_Name as 'النوع',concat(product.Product_Name,' - ',type.Type_Name,' - ',factory.Factory_Name,' - ',groupo.Group_Name,' ',COALESCE(color.Color_Name,''),' ',COALESCE(size.Size_Value,''),' ',COALESCE(sort.Sort_Value,'')) as 'الاسم',data.Carton as 'الكرتنة',sum(storage.Total_Meters) as 'الكمية','CustomerBill_ID' FROM data LEFT JOIN color ON color.Color_ID = data.Color_ID LEFT JOIN size ON size.Size_ID = data.Size_ID LEFT JOIN sort ON sort.Sort_ID = data.Sort_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID INNER JOIN factory ON factory.Factory_ID = data.Factory_ID  INNER JOIN product ON product.Product_ID = data.Product_ID  INNER JOIN type ON type.Type_ID = data.Type_ID  INNER JOIN sellprice ON sellprice.Data_ID = data.Data_ID LEFT JOIN storage ON storage.Data_ID = data.Data_ID where data.Data_ID=0 group by data.Data_ID";
                    MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    gridControl1.DataSource = dt;
                    gridView1.Columns["CustomerBill_ID"].Visible = false;

                    query = "SELECT DISTINCT data.Data_ID,data.Code as 'الكود',type.Type_Name as 'النوع',concat(product.Product_Name,' ',COALESCE(color.Color_Name,''),' ',data.Description,' ',groupo.Group_Name,' ',factory.Factory_Name,' ',COALESCE(size.Size_Value,''),' ',COALESCE(sort.Sort_Value,'')) as 'الاسم',data.Carton as 'الكرتنة',(product_bill.Quantity-IFNULL(transfer_product_details.Quantity,0)) as 'الكمية',product_bill.CustomerBill_ID FROM product_bill INNER JOIN customer_bill ON product_bill.CustomerBill_ID = customer_bill.CustomerBill_ID left JOIN transfer_product_details ON product_bill.CustomerBill_ID = transfer_product_details.CustomerBill_ID AND transfer_product_details.Data_ID = product_bill.Data_ID inner join data on data.Data_ID=product_bill.Data_ID LEFT JOIN color ON color.Color_ID = data.Color_ID LEFT JOIN size ON size.Size_ID = data.Size_ID LEFT JOIN sort ON sort.Sort_ID = data.Sort_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID INNER JOIN factory ON factory.Factory_ID = data.Factory_ID INNER JOIN product ON product.Product_ID = data.Product_ID INNER JOIN type ON type.Type_ID = data.Type_ID where product_bill.Store_ID=" + comFromStore.SelectedValue.ToString() + " and customer_bill.Branch_ID=" + comBranch.SelectedValue.ToString() + " and customer_bill.Branch_BillNumber=" + txtBillNum.Text + " order by SUBSTR(data.Code,1,16),color.Color_Name,data.Description,data.Sort_ID";
                    MySqlCommand comand = new MySqlCommand(query, dbconnection);
                    MySqlDataReader dr = comand.ExecuteReader();
                    while (dr.Read())
                    {
                        gridView1.AddNewRow();
                        int rowHandle = gridView1.GetRowHandle(gridView1.DataRowCount);
                        if (gridView1.IsNewItemRow(rowHandle))
                        {
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns[0], dr["Data_ID"]);
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns["الكود"], dr["الكود"]);
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns["النوع"], dr["النوع"]);
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns["الاسم"], dr["الاسم"]);
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns["الكرتنة"], dr["الكرتنة"]);
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns["الكمية"], dr["الكمية"]);
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns["CustomerBill_ID"], dr["CustomerBill_ID"]);
                        }
                    }
                    dr.Close();
                    gridView1.Columns[0].Visible = false;
                    gridView1.Columns["الاسم"].Width = 300;
                    if (gridView1.IsLastVisibleRow)
                    {
                        gridView1.FocusedRowHandle = gridView1.RowCount - 1;
                    }
                }
                else
                {
                    MessageBox.Show("يجب اختيار المخزن واختيار النوع والمصنع على الاقل");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }

        private void btnNewChosen_Click(object sender, EventArgs e)
        {
            clearCom();
            gridControl1.DataSource = null;
        }

        private void gridView1_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            try
            {
                row1 = gridView1.GetDataRow(gridView1.GetRowHandle(e.RowHandle));
                txtCode.Text = row1["الكود"].ToString();
                txtQuantity.Text = row1["الكمية"].ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (comFromStore.Text != "" && comToStore.Text != "" && comFromStore.SelectedValue != null && comToStore.SelectedValue != null)
                {
                    if (row1 == null)
                    {
                        MessageBox.Show("يجب اختيار بند");
                        return;
                    }

                    if (IsItemAdded())
                    {
                        MessageBox.Show("هذا العنصر تم اضافتة من قبل");
                        return;
                    }

                    if (txtQuantity.Text != "")
                    {
                        double neededQuantity = 0;
                        if (!double.TryParse(txtQuantity.Text, out neededQuantity))
                        {
                            MessageBox.Show("الكمية يجب ان تكون عدد");
                            return;
                        }

                        dbconnection.Open();
                        double quantity = 0;
                        if (row1["الكمية"].ToString() != "")
                        {
                            quantity = Convert.ToDouble(row1["الكمية"].ToString());
                        }
                        
                        if (neededQuantity <= quantity)
                        {
                            string query = "insert into transfer_product_details (Data_ID,Quantity,TransferProduct_ID,CustomerBill_ID) values (@Data_ID,@Quantity,@TransferProduct_ID,@CustomerBill_ID)";
                            MySqlCommand com = new MySqlCommand(query, dbconnection);
                            com.Parameters.Add("@Data_ID", MySqlDbType.Int16);
                            com.Parameters["@Data_ID"].Value = row1["Data_ID"].ToString();
                            com.Parameters.Add("@Quantity", MySqlDbType.Decimal);
                            com.Parameters["@Quantity"].Value = neededQuantity;
                            com.Parameters.Add("@TransferProduct_ID", MySqlDbType.Int16);
                            com.Parameters["@TransferProduct_ID"].Value = TransferProductID;
                            com.Parameters.Add("@CustomerBill_ID", MySqlDbType.Int16);
                            com.Parameters["@CustomerBill_ID"].Value = row1["CustomerBill_ID"].ToString();
                            com.ExecuteNonQuery();
                            
                            gridView2.AddNewRow();
                            int rowHandle = gridView2.GetRowHandle(gridView2.DataRowCount);
                            if (gridView2.IsNewItemRow(rowHandle) && row1 != null)
                            {
                                gridView2.SetRowCellValue(rowHandle, gridView2.Columns["Data_ID"], row1["Data_ID"].ToString());
                                gridView2.SetRowCellValue(rowHandle, gridView2.Columns["الكود"], row1["الكود"].ToString());
                                gridView2.SetRowCellValue(rowHandle, gridView2.Columns["النوع"], row1["النوع"].ToString());
                                gridView2.SetRowCellValue(rowHandle, gridView2.Columns["الاسم"], row1["الاسم"].ToString());
                                gridView2.SetRowCellValue(rowHandle, gridView2.Columns["الكرتنة"], row1["الكرتنة"].ToString());
                                gridView2.SetRowCellValue(rowHandle, gridView2.Columns["الكمية"], neededQuantity);
                                gridView2.SetRowCellValue(rowHandle, gridView2.Columns["فاتورة"], txtBillNum.Text + " " + comBranch.Text);
                                gridView2.SetRowCellValue(rowHandle, gridView2.Columns["CustomerBill_ID"], row1["CustomerBill_ID"].ToString());
                            }

                            txtQuantity.Text = "";
                            txtCode.Text = "";
                        }
                        else if (neededQuantity > quantity)
                        {
                            MessageBox.Show("لا يوجد كمية كافية");
                        }
                    }
                    else
                    {
                        MessageBox.Show("يجب ادخال جميع البيانات");
                    }
                }
                else
                {
                    MessageBox.Show("يجب اختيار المخازن");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (gridView2.GetSelectedRows().Length > 0)
            {
                if (MessageBox.Show("هل انت متاكد انك تريد الحذف؟", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        dbconnection.Open();
                        dbconnection2.Open();
                        int cont = gridView2.GetSelectedRows().Length;
                        for (int i = 0; i < cont; i++)
                        {
                            int rowhnd = gridView2.GetSelectedRows()[0];
                            DataRow row2 = gridView2.GetDataRow(rowhnd);
                            
                            string query = "delete from transfer_product_details where transfer_product_details.TransferProduct_ID=" + TransferProductID + " and transfer_product_details.Data_ID=" + row2["Data_ID"].ToString();
                            MySqlCommand com = new MySqlCommand(query, dbconnection);
                            com.ExecuteNonQuery();
                        }
                        GridView view = gridView2 as GridView;
                        view.DeleteSelectedRows();

                        gridControl1.DataSource = null;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    dbconnection.Close();
                    dbconnection2.Close();
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (gridView2.RowCount > 0)
                {
                    #region report
                    List<Transportation_Items> bi = new List<Transportation_Items>();
                    for (int i = 0; i < gridView2.RowCount; i++)
                    {
                        int rowHand = gridView2.GetRowHandle(i);

                        Transportation_Items item = new Transportation_Items() { Code = gridView2.GetRowCellDisplayText(rowHand, gridView2.Columns["الكود"]), Product_Type = gridView2.GetRowCellDisplayText(rowHand, gridView2.Columns["النوع"]), Product_Name = gridView2.GetRowCellDisplayText(rowHand, gridView2.Columns["الاسم"]), Total_Meters = Convert.ToDouble(gridView2.GetRowCellDisplayText(rowHand, gridView2.Columns["الكمية"])), Bill= gridView2.GetRowCellDisplayText(rowHand, gridView2.Columns["فاتورة"])};
                        bi.Add(item);
                    }
                    Report_Transportation_Bill_Copy f = new Report_Transportation_Bill_Copy();
                    f.PrintInvoice(TransferProductID, comFromStore.Text, comToStore.Text, Date.ToString(), bi);
                    f.ShowDialog();
                    #endregion

                    XtraTabPage xtraTabPage = getTabPage("تعديل بيانات تحويل فاتورة");
                    tabControlStoresContent.TabPages.Remove(xtraTabPage);
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
        }

        //function
        public XtraTabPage getTabPage(string text)
        {
            for (int i = 0; i < tabControlStoresContent.TabPages.Count; i++)
                if (tabControlStoresContent.TabPages[i].Text == text)
                {
                    return tabControlStoresContent.TabPages[i];
                }
            return null;
        }

        bool IsItemAdded()
        {
            for (int i = 0; i < gridView2.RowCount; i++)
            {
                DataRow row3 = gridView2.GetDataRow(gridView2.GetRowHandle(i));
                if (row1["Data_ID"].ToString() == row3["Data_ID"].ToString() && row1["CustomerBill_ID"].ToString() == row3["CustomerBill_ID"].ToString())
                    return true;
            }
            return false;
        }

        public void clearCom()
        {
            foreach (Control co in this.panel3.Controls)
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
            txtBillNum.Text = "";
            txtCode.Text = "";
            txtQuantity.Text = "";
            gridControl1.DataSource = null;
        }

        private void txtBillNum_TextChanged(object sender, EventArgs e)
        {
            txtCode.Text = "";
            txtQuantity.Text = "";
            gridControl1.DataSource = null;
        }
    }
}

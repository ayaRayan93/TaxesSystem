using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
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
    public partial class AdvancedEditForm2 : Form
    {
        MySqlConnection dbconnection;
        bool loaded = false;
        string Customer_Type;
        DataRowView row1;
        private int[] addedRecordIDs;
        int recordCount = 0;
        private bool Added = false;
        //int id;
        int EmpBranchId = 0;

        public AdvancedEditForm2(int empBranchId, int RequestNumber)
        {
            InitializeComponent();
            dbconnection = new MySqlConnection(connection.connectionString);
            addedRecordIDs = new int[100];

            //this.SetBoundFieldName(txtRequestNum, "RequestNumber");
            //this.SetBoundPropertyName(txtRequestNum, "EditValue");

            txtRequestNum.Text = RequestNumber.ToString();

            EmpBranchId = empBranchId;

            comType.AutoCompleteMode = AutoCompleteMode.Suggest;
            comType.AutoCompleteSource = AutoCompleteSource.ListItems;
            comFactory.AutoCompleteMode = AutoCompleteMode.Suggest;
            comFactory.AutoCompleteSource = AutoCompleteSource.ListItems;
            comGroup.AutoCompleteMode = AutoCompleteMode.Suggest;
            comGroup.AutoCompleteSource = AutoCompleteSource.ListItems;
            comProduct.AutoCompleteMode = AutoCompleteMode.Suggest;
            comProduct.AutoCompleteSource = AutoCompleteSource.ListItems;
            comSupplier.AutoCompleteMode = AutoCompleteMode.Suggest;
            comSupplier.AutoCompleteSource = AutoCompleteSource.ListItems;
            comEngCon.AutoCompleteMode = AutoCompleteMode.Suggest;
            comEngCon.AutoCompleteSource = AutoCompleteSource.ListItems;
            comCustomer.AutoCompleteMode = AutoCompleteMode.Suggest;
            comCustomer.AutoCompleteSource = AutoCompleteSource.ListItems;
            comColor.AutoCompleteMode = AutoCompleteMode.Suggest;
            comColor.AutoCompleteSource = AutoCompleteSource.ListItems;
            comSize.AutoCompleteMode = AutoCompleteMode.Suggest;
            comSize.AutoCompleteSource = AutoCompleteSource.ListItems;
            comSort.AutoCompleteMode = AutoCompleteMode.Suggest;
            comSort.AutoCompleteSource = AutoCompleteSource.ListItems;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                dbconnection.Open();

                string query = "select * from type";
                MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                DataTable dt = new DataTable();
                da.Fill(dt);
                comType.DataSource = dt;
                comType.DisplayMember = dt.Columns["Type_Name"].ToString();
                comType.ValueMember = dt.Columns["Type_ID"].ToString();
                comType.Text = "";
                
                query = "select * from sort";
                da = new MySqlDataAdapter(query, dbconnection);
                dt = new DataTable();
                da.Fill(dt);
                comSort.DataSource = dt;
                comSort.DisplayMember = dt.Columns["Sort_Value"].ToString();
                comSort.ValueMember = dt.Columns["Sort_ID"].ToString();
                comSort.Text = "";

                query = "select * from delegate";
                da = new MySqlDataAdapter(query, dbconnection);
                dt = new DataTable();
                da.Fill(dt);
                comDelegate.DataSource = dt;
                comDelegate.DisplayMember = dt.Columns["Delegate_Name"].ToString();
                comDelegate.ValueMember = dt.Columns["Delegate_ID"].ToString();

                query = "select * from supplier";
                da = new MySqlDataAdapter(query, dbconnection);
                dt = new DataTable();
                da.Fill(dt);
                comSupplier.DataSource = dt;
                comSupplier.DisplayMember = dt.Columns["Supplier_Name"].ToString();
                comSupplier.ValueMember = dt.Columns["Supplier_ID"].ToString();
                comSupplier.Text = "";

                query = "SELECT requests.Delegate_ID FROM requests where requests.BranchBillNumber="+ txtRequestNum.Text + " and requests.Branch_ID=" + EmpBranchId;
                MySqlCommand comand = new MySqlCommand(query, dbconnection);
                comDelegate.SelectedValue = comand.ExecuteScalar().ToString();
                
                groupBox2.Visible = false;
                loaded = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }

        //check type of customer if engineer,client or contract 
        private void radiotype_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton radio = (RadioButton)sender;
            Customer_Type = radio.Text;
            groupBox2.Visible = true;
            loaded = false;//this is flag to prevent action of SelectedValueChanged event until datasource fill combobox
            try
            {
                if (Customer_Type == "عميل")
                {
                    label4.Visible = false;
                    comEngCon.Visible = false;
                    label9.Visible = true;
                    comCustomer.Visible = true;

                    string query = "select * from customer where Customer_Type='" + Customer_Type + "'";
                    MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    comCustomer.DataSource = dt;
                    comCustomer.DisplayMember = dt.Columns["Customer_Name"].ToString();
                    comCustomer.ValueMember = dt.Columns["Customer_ID"].ToString();
                    comCustomer.Text = "";
                }
                else
                {
                    label4.Visible = true;
                    comEngCon.Visible = true;
                    label9.Visible = false;
                    comCustomer.Visible = false;


                    string query = "select * from customer where Customer_Type='" + Customer_Type + "'";
                    MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    comEngCon.DataSource = dt;
                    comEngCon.DisplayMember = dt.Columns["Customer_Name"].ToString();
                    comEngCon.ValueMember = dt.Columns["Customer_ID"].ToString();
                    comEngCon.Text = "";
                }


                loaded = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }

        //when select customer(مهندس,مقاول,تاجر)display in comCustomer the all clients of th customer 
        private void comEngCon_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (loaded)
                {
                    if (comEngCon.Text != "")
                    {
                        label9.Visible = true;
                        comCustomer.Visible = true;

                        string query = "select * from customer where Customer_ID in(select Client_ID from custmer_client where Customer_ID=" + comEngCon.SelectedValue + ")";
                        MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        comCustomer.DataSource = dt;
                        comCustomer.DisplayMember = dt.Columns["Customer_Name"].ToString();
                        comCustomer.ValueMember = dt.Columns["Customer_ID"].ToString();
                        comCustomer.Text = "";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void comBox_SelectedValueChanged(object sender, EventArgs e)
        {
            if (loaded)
            {
                ComboBox comBox = (ComboBox)sender;

                switch (comBox.Name)
                {
                    case "comType":
                        {
                            string query = "select * from color where color.Type_ID=" + comType.SelectedValue.ToString();
                            MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                            DataTable dt = new DataTable();
                            da.Fill(dt);
                            comColor.DataSource = dt;
                            comColor.DisplayMember = dt.Columns["Color_Name"].ToString();
                            comColor.ValueMember = dt.Columns["Color_ID"].ToString();
                            comColor.Text = "";

                            string qall = "";
                            if (comType.Text != "")
                            {
                                qall += " and product.Type_ID=" + comType.SelectedValue.ToString();
                            }
                            if (comFactory.Text != "")
                            {
                                qall += " and product.Factory_ID=" + comFactory.SelectedValue.ToString();
                            }
                            if (comGroup.Text != "")
                            {
                                qall += " and product.Product_ID IN (select Product_ID from product_group where Group_ID=" + comGroup.SelectedValue.ToString() + ")";
                            }
                            query = "select * from product where product.Product_ID is not null " + qall;
                            da = new MySqlDataAdapter(query, dbconnection);
                            dt = new DataTable();
                            da.Fill(dt);
                            comProduct.DataSource = dt;
                            comProduct.DisplayMember = dt.Columns["Product_Name"].ToString();
                            comProduct.ValueMember = dt.Columns["Product_ID"].ToString();
                            comProduct.Text = "";
                        }
                        break;
                    case "comFactory":
                        {
                            string query = "select * from size where size.Factory_ID=" + comFactory.SelectedValue.ToString();
                            MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                            DataTable dt = new DataTable();
                            da.Fill(dt);
                            comSize.DataSource = dt;
                            comSize.DisplayMember = dt.Columns["Size_Value"].ToString();
                            comSize.ValueMember = dt.Columns["Size_ID"].ToString();
                            comSize.Text = "";

                            string qall = "";
                            if (comType.Text != "")
                            {
                                qall += " and product.Type_ID=" + comType.SelectedValue.ToString();
                            }
                            if (comFactory.Text != "")
                            {
                                qall += " and product.Factory_ID=" + comFactory.SelectedValue.ToString();
                            }
                            if (comGroup.Text != "")
                            {
                                qall += " and product.Product_ID IN (select Product_ID from product_group where Group_ID=" + comGroup.SelectedValue.ToString() + ")";
                            }
                            query = "select * from product where product.Product_ID is not null " + qall;
                            da = new MySqlDataAdapter(query, dbconnection);
                            dt = new DataTable();
                            da.Fill(dt);
                            comProduct.DataSource = dt;
                            comProduct.DisplayMember = dt.Columns["Product_Name"].ToString();
                            comProduct.ValueMember = dt.Columns["Product_ID"].ToString();
                            comProduct.Text = "";
                        }
                        break;
                    case "comGroup":
                        {
                            string qall = "";
                            if (comType.Text != "")
                            {
                                qall += " and product.Type_ID=" + comType.SelectedValue.ToString();
                            }
                            if (comFactory.Text != "")
                            {
                                qall += " and product.Factory_ID=" + comFactory.SelectedValue.ToString();
                            }
                            if (comGroup.Text != "")
                            {
                                qall += " and product.Product_ID IN (select Product_ID from product_group where Group_ID=" + comGroup.SelectedValue.ToString() + ")";
                            }
                            string query = "select * from product where product.Product_ID is not null " + qall;
                            MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                            DataTable dt = new DataTable();
                            da.Fill(dt);
                            comProduct.DataSource = dt;
                            comProduct.DisplayMember = dt.Columns["Product_Name"].ToString();
                            comProduct.ValueMember = dt.Columns["Product_ID"].ToString();
                            comProduct.Text = "";
                        }
                        break;
                    case "comProduct":
                        {
                        }
                        break;
                }
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                string q1, q2, q3, q4, qall = "";
                if (comType.Text == "")
                {
                    q1 = "select Type_ID from type";
                }
                else
                {
                    q1 = comType.SelectedValue.ToString();
                }
                if (comFactory.Text == "")
                {
                    q2 = "select Factory_ID from factory";
                }
                else
                {
                    q2 = comFactory.SelectedValue.ToString();
                }
                if (comGroup.Text == "")
                {
                    q3 = "select Group_ID from groupo";
                }
                else
                {
                    q3 = comGroup.SelectedValue.ToString();
                }
                if (comProduct.Text == "")
                {
                    q4 = "select Product_ID from product";
                }
                else
                {
                    q4 = comProduct.SelectedValue.ToString();
                }

                if (comSort.Text != "")
                {
                    qall += " and data.Sort_ID=" + comSort.SelectedValue.ToString();
                }
                if (comSize.Text != "")
                {
                    qall += " and data.Size_ID=" + comSize.SelectedValue.ToString();
                }
                if (comColor.Text != "")
                {
                    qall += " and data.Color_ID=" + comColor.SelectedValue.ToString();
                }

                dbconnection.Open();
                //,sellprice.Last_Price as 'السعر',sellprice.Sell_Discount as 'الخصم',sellprice.Sell_Price as 'بعد الخصم',sum(storage.Total_Meters) as 'الكمية'    INNER JOIN sellprice ON sellprice.Data_ID = data.Data_ID LEFT JOIN storage ON storage.Data_ID = data.Data_ID     group by data.Data_ID order by sellprice.Date desc
                string query = "SELECT data.Data_ID,data.Code as 'الكود',product.Product_Name as 'الاسم',type.Type_Name 'النوع',factory.Factory_Name as 'المصنع',groupo.Group_Name as 'المجموعة',color.Color_Name as 'اللون',size.Size_Value as 'المقاس',sort.Sort_Value as 'الفرز',data.Carton as 'الكرتنة' FROM data LEFT JOIN color ON color.Color_ID = data.Color_ID LEFT JOIN size ON size.Size_ID = data.Size_ID LEFT JOIN sort ON sort.Sort_ID = data.Sort_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID INNER JOIN factory ON factory.Factory_ID = data.Factory_ID  INNER JOIN product ON product.Product_ID = data.Product_ID  INNER JOIN type ON type.Type_ID = data.Type_ID where data.Type_ID IN(" + q1 + ") and data.Factory_ID IN(" + q2 + ") and data.Group_ID IN (" + q3 + ") and data.Product_ID IN (" + q4 + ") " + qall + "";
                MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                DataTable dt = new DataTable();
                da.Fill(dt);
                gridControl1.DataSource = dt;
                
                gridView1.Columns[0].Visible = false;
                if (gridView1.IsLastVisibleRow)
                {
                    gridView1.FocusedRowHandle = gridView1.RowCount - 1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }
        
        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                double price = 0;
                double totalMeters = 0;
                if (!Added && row1 != null && double.TryParse(txtPrice.Text, out price) && double.TryParse(txtTotalMeters.Text, out totalMeters))
                {
                    //,sellprice.Last_Price as 'السعر',sellprice.Sell_Discount as 'الخصم',sellprice.Sell_Price as 'بعد الخصم',sum(storage.Total_Meters) as 'الكمية'   INNER JOIN sellprice ON sellprice.Data_ID = data.Data_ID LEFT JOIN storage ON storage.Data_ID = data.Data_ID   group by data.Data_ID order by sellprice.Date desc
                    if (gridView2.RowCount == 0)
                    {
                        string query = "SELECT data.Data_ID,data.Code as 'الكود',product.Product_Name as 'الاسم',type.Type_Name 'النوع',factory.Factory_Name as 'المصنع',groupo.Group_Name as 'المجموعة',color.Color_Name as 'اللون',size.Size_Value as 'المقاس',sort.Sort_Value as 'الفرز',data.Carton as 'الكرتنة','السعر','الكمية' FROM data LEFT JOIN color ON color.Color_ID = data.Color_ID LEFT JOIN size ON size.Size_ID = data.Size_ID LEFT JOIN sort ON sort.Sort_ID = data.Sort_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID INNER JOIN factory ON factory.Factory_ID = data.Factory_ID  INNER JOIN product ON product.Product_ID = data.Product_ID  INNER JOIN type ON type.Type_ID = data.Type_ID where data.Data_ID=0";
                        MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        gridControl2.DataSource = dt;
                        gridView2.Columns[0].Visible = false;
                    }

                    addedRecordIDs[recordCount] = gridView1.GetSelectedRows()[0];
                    /*gridView1.Rows[gridView1.SelectedCells[0].RowIndex].DefaultCellStyle.BackColor = Color.Silver;*/
                    
                    recordCount++;

                    gridView2.AddNewRow();
                    int n = gridView2.GetRowHandle(gridView2.DataRowCount);
                    foreach (GridColumn item in gridView1.Columns)
                    {
                        gridView2.SetRowCellValue(n, item, gridView1.GetRowCellDisplayText(gridView1.GetSelectedRows()[0], item));
                    }
                    gridView2.SetRowCellValue(n, gridView2.Columns["الكمية"], totalMeters);
                    gridView2.SetRowCellValue(n, gridView2.Columns["السعر"], price);

                    //clear fields
                    txtCode.Text = txtPrice.Text = txtTotalMeters.Text = "";
                    gridView1.DeleteRow(gridView1.GetSelectedRows()[0]);

                    if (gridView2.IsLastVisibleRow)
                    {
                        gridView2.FocusedRowHandle = gridView2.RowCount - 1;
                    }
                }
                else
                {
                    MessageBox.Show("يجب اختيار عنصر على الاقل وادخال البيانات بطريقة صحيحة");
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            try
            {
                /*&&comBranch.Text!=""&& int.TryParse(txtBranchID.Text,out branchId)&&comStore.Text!=""&&txtStoreID.Text!=""*/
                if (comDelegate.Text != "" && txtEmployee.Text != "")
                {
                    dbconnection.Open();

                    /*string query = "select Branch_Name from branch where Branch_ID=" + EmpBranchId;
                    MySqlCommand com = new MySqlCommand(query, dbconnection);
                    string branchName = com.ExecuteScalar().ToString();*/

                    string query = "select Request_ID from requests where BranchBillNumber=" + txtRequestNum.Text + " and Branch_ID=" + EmpBranchId;
                    MySqlCommand com = new MySqlCommand(query, dbconnection);
                    int RequestID = Convert.ToInt16(com.ExecuteScalar().ToString());

                    query = "update requests set Supplier_ID=@Supplier_ID,Customer_ID=@Customer_ID,Client_ID=@Client_ID,Employee_Name=@Employee_Name,Request_Date=@Request_Date,Receive_Date=@Receive_Date where BranchBillNumber=" + txtRequestNum.Text + " and Branch_ID=" + EmpBranchId;
                    com = new MySqlCommand(query, dbconnection);
                    com.Parameters.Add("@Client_ID", MySqlDbType.Int16);
                    com.Parameters["@Client_ID"].Value = comCustomer.SelectedValue;
                    com.Parameters.Add("@Customer_ID", MySqlDbType.Int16);
                    com.Parameters["@Customer_ID"].Value = comEngCon.SelectedValue;
                    com.Parameters.Add("@Employee_Name", MySqlDbType.VarChar);
                    com.Parameters["@Employee_Name"].Value = txtEmployee.Text;
                    com.Parameters.Add("@Request_Date", MySqlDbType.Date);
                    com.Parameters["@Request_Date"].Value = DateTime.Now.Date;
                    com.Parameters.Add("@Receive_Date", MySqlDbType.Date);
                    com.Parameters["@Receive_Date"].Value = dateTimePicker2.Value.Date;
                    int res2;
                    if (int.TryParse(comSupplier.SelectedValue.ToString(), out res2))
                    {
                        com.Parameters.Add("@Supplier_ID", MySqlDbType.Int16);
                        com.Parameters["@Supplier_ID"].Value = res2;
                    }
                    else
                    {
                        MessageBox.Show("insert correct value please :)");
                        dbconnection.Close();
                        return;
                    }
                    //com.Parameters.Add("@Supplier_Name", MySqlDbType.VarChar);
                    //com.Parameters["@Supplier_Name"].Value = comSupplier.Text;
                    com.ExecuteNonQuery();

                    for (int i = 0; i < gridView2.RowCount; i++)
                    {
                        DataRowView row2 = (DataRowView)gridView2.GetRow(i);
                        query = "insert into request_details (Request_ID,Data_ID,Quantity,Price) values (@Request_ID,@Data_ID,@Quantity,@Price)";
                        com = new MySqlCommand(query, dbconnection);
                        com.Parameters.Add("@Request_ID", MySqlDbType.Int16);
                        com.Parameters["@Request_ID"].Value = RequestID;
                        com.Parameters.Add("@Data_ID", MySqlDbType.Int16);
                        com.Parameters["@Data_ID"].Value = row2["Data_ID"].ToString();
                        com.Parameters.Add("@Quantity", MySqlDbType.Double);
                        com.Parameters["@Quantity"].Value = row2["الكمية"].ToString();
                        com.Parameters.Add("@Price", MySqlDbType.Double);
                        com.Parameters["@Price"].Value = row2["السعر"].ToString();
                        com.ExecuteNonQuery();
                    }

                    int SpecialOrderID = 0;
                    query = "select SpecialOrder_ID from requests where BranchBillNumber=" + txtRequestNum.Text + " and Branch_ID=" + EmpBranchId;
                    com = new MySqlCommand(query, dbconnection);
                    if (com.ExecuteScalar() != null)
                    {
                        SpecialOrderID = Convert.ToInt16(com.ExecuteScalar());
                    }

                    query = "update special_order set Record=1 where SpecialOrder_ID=" + SpecialOrderID;
                    com = new MySqlCommand(query, dbconnection);
                    com.ExecuteNonQuery();
                    dbconnection.Close();
                    MainForm.SpecialOrdersReport.search();
                    Login.mainForm.SpecialOrdersFunction();
                    this.Close();
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
            dbconnection.Close();
        }

        private void gridView1_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            try
            {
                row1 = (DataRowView)gridView1.GetRow(gridView1.GetSelectedRows()[0]);

                string value = row1[1].ToString();
                txtCode.Text = value;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}

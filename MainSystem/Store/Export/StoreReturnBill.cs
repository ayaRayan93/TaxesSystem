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
    public partial class StoreReturnBill : Form
    {
        MySqlConnection dbconnection;
        bool loaded = false;
        bool factoryFlage = false;
        bool groupFlage = false;
        bool flagProduct = false;
        int Billid = 0;
        public StoreReturnBill()
        {
            try
            {
                InitializeComponent();
                dbconnection = new MySqlConnection(connection.connectionString);
                panBillNumber.Visible = false;
                groupBox1.Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        private void StoreReturnBill_Load(object sender, EventArgs e)
        {
            try
            {
                DataHelper dh = new DataHelper(DSparametr.doubleDS);
                gridControl2.DataSource = dh.DataSet;
                gridControl2.DataMember = dh.DataMember;
                gridView2.InitNewRow += GridView1_InitNewRow;
                dbconnection.Open();
                string query = "select * from type";
                MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                DataTable dt = new DataTable();
                da.Fill(dt);
                comType.DataSource = dt;
                comType.DisplayMember = dt.Columns["Type_Name"].ToString();
                comType.ValueMember = dt.Columns["Type_ID"].ToString();
                comType.Text = "";
                txtType.Text = "";

                query = "select * from factory";
                da = new MySqlDataAdapter(query, dbconnection);
                dt = new DataTable();
                da.Fill(dt);
                comFactory.DataSource = dt;
                comFactory.DisplayMember = dt.Columns["Factory_Name"].ToString();
                comFactory.ValueMember = dt.Columns["Factory_ID"].ToString();
                comFactory.Text = "";
                txtFactory.Text = "";

                query = "select * from groupo";
                da = new MySqlDataAdapter(query, dbconnection);
                dt = new DataTable();
                da.Fill(dt);
                comGroup.DataSource = dt;
                comGroup.DisplayMember = dt.Columns["Group_Name"].ToString();
                comGroup.ValueMember = dt.Columns["Group_ID"].ToString();
                comGroup.Text = "";
                txtGroup.Text = "";

                query = "select * from product";
                da = new MySqlDataAdapter(query, dbconnection);
                dt = new DataTable();
                da.Fill(dt);
                comProduct.DataSource = dt;
                comProduct.DisplayMember = dt.Columns["Product_Name"].ToString();
                comProduct.ValueMember = dt.Columns["Product_ID"].ToString();
                comProduct.Text = "";
                txtProduct.Text = "";

                query = "select * from branch";
                da = new MySqlDataAdapter(query, dbconnection);
                dt = new DataTable();
                da.Fill(dt);
                comBranch.DataSource = dt;
                comBranch.DisplayMember = dt.Columns["Branch_Name"].ToString();
                comBranch.ValueMember = dt.Columns["Branch_ID"].ToString();
                comBranch.Text = "";
                txtBranchID.Text = "";

                loaded = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            dbconnection.Close();
        }
        private void GridView1_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            GridView view = sender as GridView;
            view.SetRowCellValue(e.RowHandle, gridView2.Columns[0], 0);
            view.SetRowCellValue(e.RowHandle, gridView2.Columns[1], "0");
            view.SetRowCellValue(e.RowHandle, gridView2.Columns[2], "0");
            view.SetRowCellValue(e.RowHandle, gridView2.Columns[3], "0");
            view.SetRowCellValue(e.RowHandle, gridView2.Columns[4], "0");
            view.SetRowCellValue(e.RowHandle, gridView2.Columns[5], "0");
            view.SetRowCellValue(e.RowHandle, gridView2.Columns[6], "0");
            view.SetRowCellValue(e.RowHandle, gridView2.Columns[7], "0");
        }
        private void radioButtonReturnBill_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                panBillNumber.Visible = true;
                groupBox1.Visible = false;
                comBranch.Text = "";
                txtBranchID.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void radioButtonWithOutReturnBill_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                panBillNumber.Visible = false;
                groupBox1.Visible = true;
                comType.Text = "";
                comFactory.Text = "";
                comGroup.Text = "";
                comProduct.Text = "";
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
                        case "comType":
                            if (loaded)
                            {
                                txtType.Text = comType.SelectedValue.ToString();
                                string query = "select * from factory inner join type_factory on factory.Factory_ID=type_factory.Factory_ID inner join type on type_factory.Type_ID=type.Type_ID where type_factory.Type_ID=" + txtType.Text;
                                MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                                DataTable dt = new DataTable();
                                da.Fill(dt);
                                comFactory.DataSource = dt;
                                comFactory.DisplayMember = dt.Columns["Factory_Name"].ToString();
                                comFactory.ValueMember = dt.Columns["Factory_ID"].ToString();
                                comFactory.Text = "";
                                txtFactory.Text = "";
                                if (txtType.Text == "1" || txtType.Text == "2")
                                {
                                    string query2 = "select * from groupo where Factory_ID=0 and Type_ID=1";
                                    MySqlDataAdapter da2 = new MySqlDataAdapter(query2, dbconnection);
                                    DataTable dt2 = new DataTable();
                                    da2.Fill(dt2);
                                    comGroup.DataSource = dt2;
                                    comGroup.DisplayMember = dt2.Columns["Group_Name"].ToString();
                                    comGroup.ValueMember = dt2.Columns["Group_ID"].ToString();
                                    comGroup.Text = "";
                                    txtGroup.Text = "";
                                }
                                else if (txtType.Text == "4")
                                {
                                    string query2 = "select * from groupo where Factory_ID=-1 and Type_ID=4";
                                    MySqlDataAdapter da2 = new MySqlDataAdapter(query2, dbconnection);
                                    DataTable dt2 = new DataTable();
                                    da2.Fill(dt2);
                                    comGroup.DataSource = dt2;
                                    comGroup.DisplayMember = dt2.Columns["Group_Name"].ToString();
                                    comGroup.ValueMember = dt2.Columns["Group_ID"].ToString();
                                    comGroup.Text = "";
                                    txtGroup.Text = "";
                                }
                                factoryFlage = true;

                                comFactory.Focus();
                            }
                            break;
                        case "comFactory":
                            if (factoryFlage)
                            {
                                txtFactory.Text = comFactory.SelectedValue.ToString();
                                if (txtType.Text != "1" && txtType.Text != "2" && txtType.Text != "4")
                                {
                                    string query2f = "select * from groupo where Factory_ID=" + txtFactory.Text;
                                    MySqlDataAdapter da2f = new MySqlDataAdapter(query2f, dbconnection);
                                    DataTable dt2f = new DataTable();
                                    da2f.Fill(dt2f);
                                    comGroup.DataSource = dt2f;
                                    comGroup.DisplayMember = dt2f.Columns["Group_Name"].ToString();
                                    comGroup.ValueMember = dt2f.Columns["Group_ID"].ToString();
                                    comGroup.Text = "";
                                    txtGroup.Text = "";
                                }

                                groupFlage = true;

                                displayData();
                                comGroup.Focus();
                            }
                            break;
                        case "comGroup":
                            if (groupFlage)
                            {
                                txtGroup.Text = comGroup.SelectedValue.ToString();

                                string query3 = "select distinct  product.Product_ID  ,Product_Name  from product inner join product_factory_group on product.Product_ID=product_factory_group.Product_ID  where product.Type_ID=" + txtType.Text + " and product_factory_group.Factory_ID=" + txtFactory.Text + " and product_factory_group.Group_ID=" + txtGroup.Text + "  order by product.Product_ID";
                                MySqlDataAdapter da3 = new MySqlDataAdapter(query3, dbconnection);
                                DataTable dt3 = new DataTable();
                                da3.Fill(dt3);
                                comProduct.DataSource = dt3;
                                comProduct.DisplayMember = dt3.Columns["Product_Name"].ToString();
                                comProduct.ValueMember = dt3.Columns["Product_ID"].ToString();
                                comProduct.Text = "";
                                txtProduct.Text = "";
                                comProduct.Focus();
                                displayData();
                                flagProduct = true;
                            }
                            break;

                        case "comProduct":
                            if (flagProduct)
                            {
                                flagProduct = false;
                                txtProduct.Text = comProduct.SelectedValue.ToString();
                                comType.Focus();
                                displayData();
                            }
                            break;
                            
                        case "comBranch":

                            txtBranchID.Text = comBranch.SelectedValue.ToString();

                            break;

                    }
                }

            }
            catch (Exception ex)
            {
                // MessageBox.Show(ex.Message);
            }
        }
        private void txtBox_KeyDown(object sender, KeyEventArgs e)
        {
            TextBox txtBox = (TextBox)sender;
            string query;
            MySqlCommand com;
            string Name;

            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    if (txtBox.Text != "")
                    {
                        dbconnection.Open();
                        switch (txtBox.Name)
                        {
                            case "txtType":
                                query = "select Type_Name from type where Type_ID='" + txtType.Text + "'";
                                com = new MySqlCommand(query, dbconnection);
                                if (com.ExecuteScalar() != null)
                                {
                                    Name = (string)com.ExecuteScalar();
                                    comType.Text = Name;
                                    txtFactory.Focus();
                                    dbconnection.Close();
                                }
                                else
                                {
                                    MessageBox.Show("there is no item with this id");
                                    dbconnection.Close();
                                    return;
                                }
                                break;
                            case "txtFactory":
                                query = "select Factory_Name from factory where Factory_ID='" + txtFactory.Text + "'";
                                com = new MySqlCommand(query, dbconnection);
                                if (com.ExecuteScalar() != null)
                                {
                                    Name = (string)com.ExecuteScalar();
                                    comFactory.Text = Name;
                                    txtGroup.Focus();
                                    dbconnection.Close();
                                    displayData();
                                }
                                else
                                {
                                    MessageBox.Show("there is no item with this id");
                                    dbconnection.Close();
                                    return;
                                }
                                break;
                            case "txtGroup":
                                query = "select Group_Name from groupo where Group_ID='" + txtGroup.Text + "'";
                                com = new MySqlCommand(query, dbconnection);
                                if (com.ExecuteScalar() != null)
                                {
                                    Name = (string)com.ExecuteScalar();
                                    comGroup.Text = Name;
                                    txtProduct.Focus();
                                    dbconnection.Close();
                                    displayData();
                                }
                                else
                                {
                                    MessageBox.Show("there is no item with this id");
                                    dbconnection.Close();
                                    return;
                                }
                                break;
                            case "txtProduct":
                                query = "select Product_Name from product where Product_ID='" + txtProduct.Text + "'";
                                com = new MySqlCommand(query, dbconnection);
                                if (com.ExecuteScalar() != null)
                                {
                                    Name = (string)com.ExecuteScalar();
                                    comProduct.Text = Name;
                                    txtType.Focus();
                                    dbconnection.Close();
                                    displayData();
                                }
                                else
                                {
                                    MessageBox.Show("there is no item with this id");
                                    dbconnection.Close();
                                    return;
                                }
                                break;
                            case "txtBranchID":
                                query = "select Branch_Name from branch where Branch_ID='" + txtBranchID.Text + "'";
                                com = new MySqlCommand(query, dbconnection);
                                if (com.ExecuteScalar() != null)
                                {
                                    Name = (string)com.ExecuteScalar();
                                    comBranch.Text = Name;
                                }
                                else
                                {
                                    MessageBox.Show("there is no item with this id");
                                    dbconnection.Close();
                                    return;
                                }
                                break;

                        }

                    }

                }
                catch (Exception ex)
                {
                    // MessageBox.Show(ex.ToString());
                }
                dbconnection.Close();
            }
        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                displayData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btnNewChooes_Click(object sender, EventArgs e)
        {
            try
            {
                comType.Text = "";
                comFactory.Text = "";
                comGroup.Text = "";
                comProduct.Text = "";

                txtType.Text = "";
                txtFactory.Text = "";
                txtGroup.Text = "";
                txtProduct.Text = "";

                displayData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void txtBranchBillNum_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                dbconnection.Open();
                if (e.KeyCode == Keys.Enter)
                {
                    displayBill();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }
        DataRow row ;
        int rowHandel1;
        private void gridView1_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            try
            {
                row = gridView1.GetDataRow(gridView1.GetRowHandle(e.RowHandle));
                rowHandel1 = e.RowHandle;
                txtCode.Text = row[1].ToString();
     
                if (radioButtonReturnBill.Checked)
                {
                    txtCarton.Text = row[5].ToString();
                    txtReturnedQuantity.Text = row[3].ToString();
                }
                else
                {
                    txtCarton.Text = row[3].ToString();
                }
                try
                {
                    txtNumOfCarton.Text = (Convert.ToDouble(row[3].ToString()) / Convert.ToDouble(row[5].ToString())).ToString();
                }
                catch 
                {
                    txtNumOfCarton.Text ="";
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void gridView1_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            try
            {
                GridView View = sender as GridView;
                if (e.RowHandle >= 0)
                {
                    double x1 = Convert.ToDouble(View.GetRowCellDisplayText(e.RowHandle, View.Columns[3]));
                    double x2 = Convert.ToDouble(View.GetRowCellDisplayText(e.RowHandle, View.Columns[4]));

                    if (x1 > x2)
                    {
                        e.Appearance.BackColor = Color.Salmon;
                        e.Appearance.BackColor2 = Color.SeaShell;
                        e.HighPriority = true;
                    }
                }
            }
            catch
            {
            }
        }
        private void btnPut_Click(object sender, EventArgs e)
        {
            try
            {
                addNewRow();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btnRemove_Click(object sender, EventArgs e)
        {
            try
            {
                int rowHandle = gridView2.GetRowHandle(gridView2.GetSelectedRows()[0]);
                gridView2.DeleteRow(rowHandle);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        CustomerReturnItems_Report form=null;
        private void btnCreateReturnBill_Click(object sender, EventArgs e)
        {
            try
            {
                dbconnection.Open();
                List<ReturnPermissionClass> listOfData = new List<ReturnPermissionClass>();
                if (gridView2.RowCount > 0)
                {
                    string query = "insert into customer_return_permission (CustomerBill_ID,ClientReturnName,ClientRetunPhone,Date) values (@CustomerBill_ID,@ClientReturnName,@ClientRetunPhone,@Date)";
                    MySqlCommand com = new MySqlCommand(query, dbconnection);
                    if (radioButtonReturnBill.Checked)
                    {
                        com.Parameters.Add("@CustomerBill_ID", MySqlDbType.Int16);
                        com.Parameters["@CustomerBill_ID"].Value = Billid;
                    }
                    else
                    {
                        com.Parameters.Add("@CustomerBill_ID", MySqlDbType.Int16);
                        com.Parameters["@CustomerBill_ID"].Value = 0;
                    }
                    com.Parameters.Add("@ClientReturnName", MySqlDbType.VarChar);
                    com.Parameters["@ClientReturnName"].Value = txtClientName.Text;
                    com.Parameters.Add("@ClientRetunPhone", MySqlDbType.VarChar);
                    com.Parameters["@ClientRetunPhone"].Value = txtPhone.Text;
                    com.Parameters.Add("@Date", MySqlDbType.DateTime);
                    com.Parameters["@Date"].Value = dateTimePicker1.Value;
                    com.ExecuteNonQuery();

                    query = "select CustomerReturnPermission_ID from customer_return_permission order by CustomerReturnPermission_ID desc limit 1";
                    com = new MySqlCommand(query,dbconnection);
                    int CustomerReturnPermission_ID =Convert.ToInt16(com.ExecuteScalar());

                    for (int i = 0; i < gridView2.RowCount; i++)
                    {
                        DataRow row1 = gridView2.GetDataRow(gridView2.GetRowHandle(i));
                        query = "insert into customer_return_permission_details (CustomerReturnPermission_ID,Data_ID,Carton,NumOfCarton,TotalQuantity,ReturnItemReason) values (@CustomerReturnPermission_ID,@Data_ID,@Carton,@NumOfCarton,@TotalQuantity,@ReturnItemReason)";
                        com = new MySqlCommand(query, dbconnection);

                        com.Parameters.Add("@CustomerReturnPermission_ID", MySqlDbType.Int16);
                        com.Parameters["@CustomerReturnPermission_ID"].Value = CustomerReturnPermission_ID;
                        com.Parameters.Add("@Data_ID", MySqlDbType.Int16);
                        com.Parameters["@Data_ID"].Value = row1[0].ToString();
                        com.Parameters.Add("@Carton", MySqlDbType.Decimal);
                        com.Parameters["@Carton"].Value = row1[5].ToString();
                        com.Parameters.Add("@NumOfCarton", MySqlDbType.Decimal);
                        com.Parameters["@NumOfCarton"].Value = row1[6].ToString();
                        com.Parameters.Add("@TotalQuantity", MySqlDbType.Decimal);
                        com.Parameters["@TotalQuantity"].Value = row1[4].ToString();
                        com.Parameters.Add("@ReturnItemReason", MySqlDbType.VarChar);
                        com.Parameters["@ReturnItemReason"].Value = row1[7].ToString();

                        com.ExecuteNonQuery();

                        ReturnPermissionClass returnPermissionClass = new ReturnPermissionClass();
                        returnPermissionClass.Data_ID = (int)row1["Data_ID"];
                        returnPermissionClass.Code = row1[1].ToString();
                        returnPermissionClass.ItemName = row1[2].ToString();
                        returnPermissionClass.NumOfCarton = Convert.ToDouble(row1[6]);
                        returnPermissionClass.Carton = Convert.ToDouble(row1[5]);
                        returnPermissionClass.ReturnQuantity = Convert.ToDouble(row1[4]);
                        returnPermissionClass.ReturnItemReason = row1[7].ToString();
                        listOfData.Add(returnPermissionClass);

                    }
                    ReturnPermissionReportViewer ReturnCustomerPermissionReport = new ReturnPermissionReportViewer(listOfData, CustomerReturnPermission_ID.ToString(), txtClientName.Text, txtPhone.Text, txtReturnReason.Text, dateTimePicker1.Text);
                    ReturnCustomerPermissionReport.Show();
       
                    clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }
        //functions
        public void displayData()
        {
            string q1, q2, q3, q4;
            if (txtType.Text == "")
            {
                q1 = "select Type_ID from data";
            }
            else
            {
                q1 = txtType.Text;
            }
            if (txtFactory.Text == "")
            {
                q2 = "select Factory_ID from data";
            }
            else
            {
                q2 = txtFactory.Text;
            }
            if (txtProduct.Text == "")
            {
                q3 = "select Product_ID from data";
            }
            else
            {
                q3 = txtProduct.Text;
            }
            if (txtGroup.Text == "")
            {
                q4 = "select Group_ID from data";
            }
            else
            {
                q4 = txtGroup.Text;
            }

            string query = "SELECT data.Data_ID, data.Code as 'الكود',product.Product_Name as 'الصنف',type.Type_Name as 'النوع',factory.Factory_Name as 'المصنع',groupo.Group_Name as 'المجموعة',color.Color_Name as 'اللون',size.Size_Value as 'المقاس',sort.Sort_Value as 'الفرز',data.Classification as 'التصنيف',data.Description as 'الوصف',data.Carton as 'الكرتنة' from data INNER JOIN type ON type.Type_ID = data.Type_ID INNER JOIN product ON product.Product_ID = data.Product_ID INNER JOIN factory ON data.Factory_ID = factory.Factory_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID LEFT outer JOIN color ON data.Color_ID = color.Color_ID LEFT outer  JOIN size ON data.Size_ID = size.Size_ID LEFT outer  JOIN sort ON data.Sort_ID = sort.Sort_ID where  data.Type_ID IN(" + q1 + ") and  data.Factory_ID  IN(" + q2 + ") and  data.Product_ID  IN(" + q3 + ") and data.Group_ID IN (" + q4 + ") ";
            string itemName = "concat( product.Product_Name,' ',type.Type_Name,' ',factory.Factory_Name,' ',groupo.Group_Name,' ' ,COALESCE(color.Color_Name,''),' ',COALESCE(size.Size_Value,''),' ',COALESCE(sort.Sort_Value,''),' ',COALESCE(data.Classification,''),' ',COALESCE(data.Description,''))as 'البند'";
            string DataTableRelations = "INNER JOIN type ON type.Type_ID = data.Type_ID INNER JOIN product ON product.Product_ID = data.Product_ID INNER JOIN factory ON data.Factory_ID = factory.Factory_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID LEFT outer JOIN color ON data.Color_ID = color.Color_ID LEFT outer  JOIN size ON data.Size_ID = size.Size_ID LEFT outer  JOIN sort ON data.Sort_ID = sort.Sort_ID";
            query = "select data.Data_ID, Code as 'الكود'," + itemName + ",data.Carton as 'الكرتنة' from  data " + DataTableRelations ;

            MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
            DataTable dt = new DataTable();
            da.Fill(dt);
            gridControl1.DataSource = null;

            gridControl1.DataSource = dt;
            gridView1.Columns[0].Visible = false;
            gridView1.Columns[1].Width = 200;
        }
        public void displayBill()
        {
            string query = "select CustomerBill_ID from customer_bill where Branch_ID="+txtBranchID.Text+ " and Branch_BillNumber=" +txtBranchBillNum.Text;
            MySqlCommand com = new MySqlCommand(query, dbconnection);
            Billid = Convert.ToInt16(com.ExecuteScalar());
            query = "select * from customer_permissions where CustomerBill_ID=" + Billid;
            com = new MySqlCommand(query, dbconnection);
            MySqlDataReader dr = com.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Close();
                string itemName = "concat( product.Product_Name,' ',type.Type_Name,' ',factory.Factory_Name,' ',groupo.Group_Name,' ' ,COALESCE(color.Color_Name,''),' ',COALESCE(size.Size_Value,''),' ',COALESCE(sort.Sort_Value,''),' ',COALESCE(data.Classification,''),' ',COALESCE(data.Description,''))as 'البند'";
                string DataTableRelations = "INNER JOIN type ON type.Type_ID = data.Type_ID INNER JOIN product ON product.Product_ID = data.Product_ID INNER JOIN factory ON data.Factory_ID = factory.Factory_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID LEFT outer JOIN color ON data.Color_ID = color.Color_ID LEFT outer  JOIN size ON data.Size_ID = size.Size_ID LEFT outer  JOIN sort ON data.Sort_ID = sort.Sort_ID";
                query = "select product_bill.Data_ID, Code as'الكود'," + itemName + ",Quantity as 'الكمية',sum(DeliveredQuantity) as 'الكمية المسلمة',customer_permissions_details.Carton as 'الكرتنة' from product_bill inner join data on data.Data_ID=product_bill.Data_ID " + DataTableRelations + " left join customer_permissions_details on customer_permissions_details.Data_ID=product_bill.Data_ID where CustomerBill_ID=" + Billid + " group by product_bill.Data_ID";

                MySqlDataAdapter ad = new MySqlDataAdapter(query, dbconnection);
                DataTable dt = new DataTable();
                ad.Fill(dt);
                gridControl1.DataSource = null;
                gridControl1.DataSource = dt;
                gridView1.Columns[0].Visible = false;
                gridView1.Columns[1].Width = 200;
            }
            else
            {
                dr.Close();
                string itemName = "concat( product.Product_Name,' ',type.Type_Name,' ',factory.Factory_Name,' ',groupo.Group_Name,' ' ,COALESCE(color.Color_Name,''),' ',COALESCE(size.Size_Value,''),' ',COALESCE(sort.Sort_Value,''),' ',COALESCE(data.Classification,''),' ',COALESCE(data.Description,''))as 'البند'";
                string DataTableRelations = "INNER JOIN type ON type.Type_ID = data.Type_ID INNER JOIN product ON product.Product_ID = data.Product_ID INNER JOIN factory ON data.Factory_ID = factory.Factory_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID LEFT outer JOIN color ON data.Color_ID = color.Color_ID LEFT outer  JOIN size ON data.Size_ID = size.Size_ID LEFT outer  JOIN sort ON data.Sort_ID = sort.Sort_ID";
                query = "select product_bill.Data_ID, Code as'الكود'," + itemName + ",Quantity as 'الكمية', '" + 0 + " ' as 'الكمية المسلمة',Cartons as 'الكرتنة' from product_bill inner join data on data.Data_ID=product_bill.Data_ID " + DataTableRelations + " where CustomerBill_ID=" + Billid;

                MySqlDataAdapter ad = new MySqlDataAdapter(query, dbconnection);
                DataTable dt = new DataTable();
                ad.Fill(dt);
                gridControl1.DataSource = dt;
                gridView1.Columns[0].Visible = false;
            }
        }
        public void addNewRow()
        {
            if (radioButtonReturnBill.Checked)
            {
                gridView2.AddNewRow();

                int rowHandle = gridView2.GetRowHandle(gridView2.DataRowCount);
                if (gridView2.IsNewItemRow(rowHandle) && rowHandel1 != -1)
                {
                    gridView2.SetRowCellValue(rowHandle, gridView2.Columns[0], row[0]);
                    gridView2.SetRowCellValue(rowHandle, gridView2.Columns[1], row[1]);
                    gridView2.SetRowCellValue(rowHandle, gridView2.Columns[2], row[2]);
                    gridView2.SetRowCellValue(rowHandle, gridView2.Columns[3], row[3]);

                    double re = 0, carton = 0;
                    if (Convert.ToDouble(txtCarton.Text) != 0)
                        re = Convert.ToDouble(txtReturnedQuantity.Text) / Convert.ToDouble(txtCarton.Text);
                    carton = Convert.ToDouble(txtCarton.Text);

                    gridView2.SetRowCellValue(rowHandle, gridView2.Columns[6], re + "");
                    gridView2.SetRowCellValue(rowHandle, gridView2.Columns[5], carton + "");
                    gridView2.SetRowCellValue(rowHandle, gridView2.Columns[4], txtReturnedQuantity.Text);
                    gridView2.SetRowCellValue(rowHandle, gridView2.Columns[7], txtReturnItemReason.Text);
                }
            }
            else
            {
                gridView2.AddNewRow();

                int rowHandle = gridView2.GetRowHandle(gridView2.DataRowCount);
                if (gridView2.IsNewItemRow(rowHandle) && rowHandel1 != -1)
                {
                    gridView2.SetRowCellValue(rowHandle, gridView2.Columns[0], row[0]);
                    gridView2.SetRowCellValue(rowHandle, gridView2.Columns[1], row[1]);
                    gridView2.SetRowCellValue(rowHandle, gridView2.Columns[2], row[2]);
                    gridView2.SetRowCellValue(rowHandle, gridView2.Columns[3], "");

                    double re = 0, carton = 0;
                    if (Convert.ToDouble(txtCarton.Text) != 0)
                        re = Convert.ToDouble(txtReturnedQuantity.Text) / Convert.ToDouble(txtCarton.Text);
                    carton = Convert.ToDouble(txtCarton.Text);

                    gridView2.SetRowCellValue(rowHandle, gridView2.Columns[6], re + "");
                    gridView2.SetRowCellValue(rowHandle, gridView2.Columns[5], carton + "");
                    gridView2.SetRowCellValue(rowHandle, gridView2.Columns[4], txtReturnedQuantity.Text);
                    gridView2.SetRowCellValue(rowHandle, gridView2.Columns[7], txtReturnItemReason.Text);
                }
            }
            loaded = true;
        }
        public void clear()
        {
            comType.Text = "";
            comFactory.Text = "";
            comGroup.Text = "";
            comProduct.Text = "";

            txtType.Text = "";
            txtFactory.Text = "";
            txtGroup.Text = "";
            txtProduct.Text = "";

            foreach (Control item in panel2.Controls)
            {
                if (item is TextBox)
                {
                    item.Text = "";
                }
            }
            foreach (Control item in panel3.Controls)
            {
                if (item is TextBox)
                {
                    item.Text = "";
                }
            }
            dateTimePicker1.Value = DateTime.Now.Date;
            gridControl1.DataSource = null;
            gridControl2.DataSource = null;
        }
      
    }
}

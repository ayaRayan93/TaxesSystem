using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraRichEdit.Utils;
using MainSystem.Store.Export;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MainSystem
{
    public partial class CustomerDelivery : Form
    {
        MySqlConnection dbconnection;
        string Store_ID = "0";
        bool loaded = false;
        string permissionNum;
        int flag;
        DataRow addrow = null;
        int rowHandel2 = -1;
        string SelectType = "";
        bool comBranchLoaded=false;
        string branchID = "", BranchName = "";
        string TypeBuy = "";
        public CustomerDelivery()
        {
            try
            {
                InitializeComponent();
                dbconnection = new MySqlConnection(connection.connectionString);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public CustomerDelivery(string permissionNum,string branchID,int flag)
        {
            try
            {
                InitializeComponent();
                //if (UserControl.EmpType == "مدير")
                //{
                    btnPrint.Visible = true;
               // }
                dbconnection = new MySqlConnection(connection.connectionString);
                dbconnection.Open();
                this.permissionNum = permissionNum;
                this.flag = flag;
                if (flag == 1)
                {
                    radioBtnDriverDelivery.Checked = true;
                }
                else
                {
                    radioBtnCustomerDelivery.Checked = true;
                }
                txtPermBillNumber.Text = permissionNum;
                txtBranchID.Text = branchID;
                string query = "select Branch_Name from branch where Branch_ID=" + txtBranchID.Text;
                MySqlCommand comand = new MySqlCommand(query, dbconnection);
           
                comBranch.Text = comand.ExecuteScalar().ToString();
                this.branchID = branchID;
                this.BranchName = comBranch.Text;
                // displayPermission(permissionNum);
                if (IsBillHavePerviousPermission())
                {
                    displayDatawithPerviousPer();
                }
                else
                {
                    displayData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                DataHelper dh = new DataHelper(DSparametr.simpleDS);
                gridControl2.DataSource = dh.DataSet;
                gridControl2.DataMember = dh.DataMember;
                gridView2.InitNewRow += GridView1_InitNewRow;

                string query = "select * from branch";
                MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                DataTable dt = new DataTable();
                da.Fill(dt);
                comBranch.DataSource = dt;
                comBranch.DisplayMember = dt.Columns["Branch_Name"].ToString();
                comBranch.ValueMember = dt.Columns["Branch_ID"].ToString();
                
                comBranch.Text = BranchName;
                txtBranchID.Text = branchID;
               
                query = "select * from store";
                da = new MySqlDataAdapter(query, dbconnection);
                dt = new DataTable();
                da.Fill(dt);
                comStore.DataSource = dt;
                comStore.DisplayMember = dt.Columns["Store_Name"].ToString();
                comStore.ValueMember = dt.Columns["Store_ID"].ToString();
                comStore.Text = "";
                txtStore.Text = "";
                comBranchLoaded = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }
        private void comBranch_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (comBranchLoaded)
                {
                    txtBranchID.Text = comBranch.SelectedValue.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void txtBranchID_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    dbconnection.Close();
                    string query = "select Branch_Name from branch where Branch_ID=" + txtBranchID.Text;
                    MySqlCommand comand = new MySqlCommand(query, dbconnection);
                    dbconnection.Open();
                    string Branch_Name = comand.ExecuteScalar().ToString();
                    dbconnection.Close();
                    comBranch.Text = Branch_Name;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        private void comStore_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (comBranchLoaded)
                {
                    txtStore.Text = comStore.SelectedValue.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void txtStore_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    dbconnection.Close();
                    string query = "select Store_Name from store where Store_ID=" + txtStore.Text;
                    MySqlCommand comand = new MySqlCommand(query, dbconnection);
                    dbconnection.Open();
                    string Store_Name = comand.ExecuteScalar().ToString();
                    dbconnection.Close();
                    comStore.Text = Store_Name;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        private void GridView1_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            GridView view = sender as GridView;
            if (!IsExist(addrow[0].ToString(), addrow[4].ToString()))
            {
                view.SetRowCellValue(e.RowHandle, view.Columns[0], addrow[0]);
                view.SetRowCellValue(e.RowHandle, view.Columns[1], addrow[1]);
                view.SetRowCellValue(e.RowHandle, view.Columns[2], addrow[2]);
                view.SetRowCellValue(e.RowHandle, view.Columns[3], addrow[5]);
                view.SetRowCellValue(e.RowHandle, view.Columns[7], addrow[3]);
                double re = 0, carton = 0;
                if (addrow[4].ToString() != "")
                {
                    carton = Convert.ToDouble(addrow[4]);
                }

                if (SelectType == "oneRow")
                {
                    if (carton != 0)
                    {
                        re = Convert.ToDouble(txtRecivedQuantity.Text) / carton;
                    }
                    else
                    {
                        re = 0;
                    }
                    view.SetRowCellValue(e.RowHandle, view.Columns[4], txtRecivedQuantity.Text);
                }
                else
                {
                    if (carton != 0)
                    {
                        re = Convert.ToDouble(addrow[5]) / carton;
                    }
                    else
                    {
                        re = 0;
                    }
                    view.SetRowCellValue(e.RowHandle, view.Columns[4], addrow[5].ToString());
                }

                view.SetRowCellValue(e.RowHandle, view.Columns[6], re + "");
                view.SetRowCellValue(e.RowHandle, view.Columns[5], carton + "");
            }
        }
        private void radioButton_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (radioBtnCustomerDelivery.Checked)
                {
                    labDescription.Text = "فاتورة رقم";
                    panBranch.Visible = true;
                    comBranch.Text = "";
                    txtBranchID.Text = "";
                }
                else if(radioBtnDriverDelivery.Checked)
                {
                    labDescription.Text = "اذن رقم";
                    panBranch.Visible = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void txtBillNumber_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    dbconnection.Open();
                    if (IsBillHavePerviousPermission())
                    {
                        displayDatawithPerviousPer();
                    }
                    else
                    {
                        displayData();
                    }
                    //DataHelper dh = new DataHelper(DSparametr.simpleDS);
                    //gridControl2.DataSource = dh.DataSet;
                    //gridControl2.DataMember = dh.DataMember;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
           
        }
        private void gridView1_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            try
            {
                GridView View = sender as GridView;
                if (e.RowHandle >= 0)
                {
                    double x1 =Convert.ToDouble(View.GetRowCellDisplayText(e.RowHandle, View.Columns[3]));
                    double x2 = Convert.ToDouble(View.GetRowCellDisplayText(e.RowHandle, View.Columns[4]));

                    if (x1>x2)
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
        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (gridView1.SelectedRowsCount==1&& txtRecivedQuantity.Text!="")
                {
                    loaded = false;
                    if (Convert.ToDouble(txtRecivedQuantity.Text) <= (Convert.ToDouble(row["الكمية"].ToString()) - Convert.ToDouble(row[6].ToString())))
                    {
                        if (IsDelveryQuantityHaveValue(row[6].ToString()))
                        {
                            if ((Convert.ToDouble(row[6]) < Convert.ToDouble(row["الكمية"])))
                            {
                                addrow = row;
                                SelectType = "oneRow";
                                addNewRow(row);

                                txtCode.Text = "";
                                txtRecivedQuantity.Text = "";
                                comStorePlace.DataSource = null;
                            }
                            else
                            {
                                MessageBox.Show("البند تم تسليمه بالكامل");
                            }
                        }
                        //else
                        //{
                        //    addrow = row;
                        //    SelectType = "oneRow";
                        //    addNewRow(row);

                        //    txtCode.Text = "";
                        //    txtRecivedQuantity.Text = "";
                        //    comStorePlace.DataSource = null;
                        //}
                    }
                    else
                    {
                        txtRecivedQuantity.Text = "0";
                        txtRecivedQuantity.Focus();
                        MessageBox.Show("الكمية اكبر من اجمالي الكمية المطلوبة");
                    }
                }
                else 
                {
                    int[] selectedRows = gridView1.GetSelectedRows();
                    for (int i = 0; i < gridView1.SelectedRowsCount; i++)
                    {
                        DataRow row =gridView1.GetDataRow(selectedRows[i]);
                        if (IsDelveryQuantityHaveValue(row[6].ToString()))
                        {
                            if (Convert.ToDouble(row[6]) < Convert.ToDouble(row[5]))
                            {
                                addrow = row;
                                SelectType = "muliRow";
                                addNewRow(row);
                            }
                        }
                        else
                        {
                            addrow = row;
                            SelectType = "muliRow";
                            addNewRow(row);
                        }
                    
                    }
                }
                loaded = true;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
       
        }
        private void btnRemove_Click(object sender, EventArgs e)
        {
            try
            {
                gridView2.DeleteRow(rowHandel2);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (gridView2.RowCount > 0 && txtPermBillNumber.Text != "" && txtStore.Text!="")
                {
                    dbconnection.Open();

                    string query = "select * from customer_bill  where Branch_ID=" + txtBranchID.Text + " and Branch_BillNumber=" + txtPermBillNumber.Text;
                    MySqlCommand com = new MySqlCommand(query, dbconnection);
                    int CustomerBill_ID = Convert.ToInt16(com.ExecuteScalar());
                    query = "insert into customer_permissions (CustomerBill_ID,Customer_ID,Customer_Name,Store_ID,Store_Name,Client_ID,Client_Name,Delegate_Name,Date,BranchBillNumber,Branch_ID,Branch_Name,Note,DeliveredPerson,StoreKeeper) values (@CustomerBill_ID,@Customer_ID,@Customer_Name,@Store_ID,@Store_Name,@Client_ID,@Client_Name,@Delegate_Name,@Date,@BranchBillNumber,@Branch_ID,@Branch_Name,@Note,@DeliveredPerson,@StoreKeeper)";
                    com = new MySqlCommand(query, dbconnection);
                    com.Parameters.Add("@CustomerBill_ID", MySqlDbType.Int16);
                    com.Parameters["@CustomerBill_ID"].Value = txtPermBillNumber.Text;
                    com.Parameters.Add("@BranchBillNumber", MySqlDbType.Int16);
                    com.Parameters["@BranchBillNumber"].Value = txtPermBillNumber.Text;
                    if (txtCustomerID.Text != "")
                    {
                        com.Parameters.Add("@Customer_ID", MySqlDbType.Int16);
                        com.Parameters["@Customer_ID"].Value = Convert.ToInt16(txtCustomerID.Text);
                        com.Parameters.Add("@Customer_Name", MySqlDbType.VarChar);
                        com.Parameters["@Customer_Name"].Value = txtCustomerName.Text;
                    }
                    else
                    {
                        com.Parameters.Add("@Customer_ID", MySqlDbType.Int16);
                        com.Parameters["@Customer_ID"].Value = 0;
                        com.Parameters.Add("@Customer_Name", MySqlDbType.VarChar);
                        com.Parameters["@Customer_Name"].Value = "";
                    }
                    if (txtClientID.Text != "")
                    {
                        com.Parameters.Add("@Client_ID", MySqlDbType.Int16);
                        com.Parameters["@Client_ID"].Value = Convert.ToInt16(txtClientID.Text);
                        com.Parameters.Add("@Client_Name", MySqlDbType.VarChar);
                        com.Parameters["@Client_Name"].Value = txtClientName.Text;
                    }
                    else
                    {
                        com.Parameters.Add("@Client_ID", MySqlDbType.Int16);
                        com.Parameters["@Client_ID"].Value = 0;
                        com.Parameters.Add("@Client_Name", MySqlDbType.VarChar);
                        com.Parameters["@Client_Name"].Value = "";
                    }
                    com.Parameters.Add("@Store_ID", MySqlDbType.Int16);
                    com.Parameters["@Store_ID"].Value = Convert.ToInt16(txtStore.Text);
                    com.Parameters.Add("@Store_Name", MySqlDbType.VarChar);
                    com.Parameters["@Store_Name"].Value = comStore.Text;
                    com.Parameters.Add("@Delegate_Name", MySqlDbType.VarChar);
                    com.Parameters["@Delegate_Name"].Value = txtDelegate.Text;
                    com.Parameters.Add("@Date", MySqlDbType.Date);
                    com.Parameters["@Date"].Value = dateTimePicker1.Value.Date;
                    com.Parameters.Add("@Branch_ID", MySqlDbType.Int16);
                    com.Parameters["@Branch_ID"].Value = Convert.ToInt16(txtBranchID.Text);
                    com.Parameters.Add("@Branch_Name", MySqlDbType.VarChar);
                    com.Parameters["@Branch_Name"].Value = comStore.Text;
                    com.Parameters.Add("@Note", MySqlDbType.VarChar);
                    com.Parameters["@Note"].Value = txtNote.Text;

                    com.Parameters.Add("@DeliveredPerson", MySqlDbType.VarChar);
                    com.Parameters["@DeliveredPerson"].Value = txtDeliverPerson.Text;
                    com.Parameters.Add("@StoreKeeper", MySqlDbType.VarChar);
                    com.Parameters["@StoreKeeper"].Value = txtStoreKeeper.Text;

                    com.ExecuteNonQuery();
                    query = "select Customer_Permissin_ID from customer_permissions order by Customer_Permissin_ID desc limit 1";
                    com= new MySqlCommand(query, dbconnection);
                    int id =Convert.ToInt16(com.ExecuteScalar());
                    List<DeliveryPermissionClass> listOfData = new List<DeliveryPermissionClass>();
                    for (int i = 0; i < gridView2.RowCount; i++)
                    {
                        DataRow row1 = gridView2.GetDataRow(gridView2.GetRowHandle(i));
                        if (row1 != null&&row1[0].ToString()!="")
                        {
                            query = "insert into customer_permissions_details (Customer_Permissin_ID,Data_ID,Carton,DeliveredQuantity,Quantity,ItemType) values (@Customer_Permissin_ID,@Data_ID,@Carton,@DeliveredQuantity,@Quantity,@ItemType)";
                            com = new MySqlCommand(query, dbconnection);
                            com.Parameters.Add("@Customer_Permissin_ID", MySqlDbType.Int16);
                            com.Parameters["@Customer_Permissin_ID"].Value = id;
                            com.Parameters.Add("@Data_ID", MySqlDbType.Int16);
                            com.Parameters["@Data_ID"].Value = row1["Data_ID"].ToString();
                            com.Parameters.Add("@Quantity", MySqlDbType.Double);
                            com.Parameters["@Quantity"].Value = row1[3].ToString();
                            com.Parameters.Add("@DeliveredQuantity", MySqlDbType.Double);
                            com.Parameters["@DeliveredQuantity"].Value = row1[4].ToString();
                            com.Parameters.Add("@Carton", MySqlDbType.Double);
                            com.Parameters["@Carton"].Value = row1[5].ToString();
                            com.Parameters.Add("@NumOfCarton", MySqlDbType.Double);
                            com.Parameters["@NumOfCarton"].Value = row1[6].ToString();
                            com.Parameters.Add("@ItemType", MySqlDbType.VarChar);
                            com.Parameters["@ItemType"].Value = row1["ItemType"].ToString();
                            com.ExecuteNonQuery();

                            IsBillRecived();
                            DeliveryPermissionClass deliveryPermissionClass = new DeliveryPermissionClass();
                            deliveryPermissionClass.Data_ID = (int)row1["Data_ID"];
                            deliveryPermissionClass.Code = row1[1].ToString();
                            if (row1[7].ToString() == "عرض")
                            {
                                string itemName = "concat( product.Product_Name,' ',type.Type_Name,' ',factory.Factory_Name,' ',groupo.Group_Name,' ' ,COALESCE(color.Color_Name,''),' ',COALESCE(size.Size_Value,''),' ',COALESCE(sort.Sort_Value,''),' ',COALESCE(data.Classification,''),' ',COALESCE(data.Description,''))as 'البند'";
                                string DataTableRelations = "INNER JOIN type ON type.Type_ID = data.Type_ID INNER JOIN product ON product.Product_ID = data.Product_ID INNER JOIN factory ON data.Factory_ID = factory.Factory_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID LEFT outer JOIN color ON data.Color_ID = color.Color_ID LEFT outer  JOIN size ON data.Size_ID = size.Size_ID LEFT outer  JOIN sort ON data.Sort_ID = sort.Sort_ID";
                                query = "select Code as'الكود'," + itemName + " from offer inner join offer_details on offer.Offer_ID=offer_details.Offer_ID inner join data on data.Data_ID=offer_details.Data_ID " + DataTableRelations + "  where offer.Offer_ID=" + (int)row1["Data_ID"];
                                com = new MySqlCommand(query, dbconnection);
                                MySqlDataReader dr = com.ExecuteReader();
                                string str = "";
                                while (dr.Read())
                                {
                                    str += dr[1].ToString() + "\n";
                                }
                                dr.Close();
                                deliveryPermissionClass.ItemName = row1[2].ToString() + "\n" + str;
                            }
                            else
                            {
                                deliveryPermissionClass.ItemName = row1[2].ToString();
                            }
                            deliveryPermissionClass.TotalQuantity = Convert.ToDouble(row1[3]);
                            if (row1[5].ToString() != "" && Convert.ToDouble(row1[5]) != 0)
                            {
                                deliveryPermissionClass.NumOfCarton = Convert.ToDouble(row1[3])/Convert.ToDouble(row1[5]);
                            }
                            else
                            {
                                deliveryPermissionClass.NumOfCarton = Convert.ToDouble(row1[3]);
                            }
                            deliveryPermissionClass.DeliveryQuantity = row1[4].ToString();
                            listOfData.Add(deliveryPermissionClass);
                        }

                    }
                    DeliveryPermissionReportViewer DeliveryPermissionReport;//= new DeliveryPermissionReportViewer(listOfData, txtPermBillNumber.Text);

                    if (txtClientID.Text != "")
                    {
                        DeliveryPermissionReport = new DeliveryPermissionReportViewer(listOfData, txtClientName.Text + " " + txtClientID.Text, txtPhoneNumber.Text , txtDelegate.Text, labDate.Text, txtPermBillNumber.Text + "  " + TypeBuy, id.ToString(), txtBranchID.ToString(), comBranch.Text,txtStoreKeeper.Text,txtDeliverPerson.Text,comStore.Text,false);
                        DeliveryPermissionReport.Show();
                    }
                    else if (txtCustomerID.Text != "")
                    {
                        DeliveryPermissionReport = new DeliveryPermissionReportViewer(listOfData, txtCustomerName.Text + " " + txtCustomerID.Text, txtPhoneNumber.Text , txtDelegate.Text, labDate.Text, txtPermBillNumber.Text + "  " + TypeBuy, id.ToString(), txtBranchID.ToString(), comBranch.Text, txtStoreKeeper.Text, txtDeliverPerson.Text, comStore.Text,false);
                        DeliveryPermissionReport.Show();
                    }

                    clear();
                }
                else
                {
                    MessageBox.Show("ادخل البيانات المطلوبة");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }
        DataRow row=null;
        int rowHandel1 = -1;
        private void gridView1_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            try
            {
                if (gridView1.SelectedRowsCount == 1)
                {
                    row = gridView1.GetDataRow(gridView1.GetRowHandle(e.RowHandle));
                    rowHandel1 = e.RowHandle;
                    txtCode.Text = row[1].ToString();
                    string d = row[6].ToString();
                    if (d!="")
                    {
                        
                        txtRecivedQuantity.Text = (Convert.ToDouble(row[5].ToString()) - Convert.ToDouble(row[6].ToString())).ToString();
                    }
                    else
                    {
                        txtRecivedQuantity.Text = row[5].ToString();
                    }
                    txtRecivedQuantity.Focus();
                }
                else
                {
                    txtCode.Text ="";
                    txtRecivedQuantity.Text = "";
                }
                //if (radioBtnCustomerDelivery.Checked)
                //    Store_ID = row[7].ToString();
                //string query = "select concat(Store_Place_Code ,'   ',Total_Meters) as 'x' from storage inner join store_places on store_places.Store_Place_ID=storage.Store_Place_ID where storage.Store_ID=" + Store_ID + " and  Data_ID=" + row[0].ToString();
                //MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                //DataTable dt = new DataTable();
                //da.Fill(dt);
                //comStorePlace.DataSource = dt;
                //comStorePlace.DisplayMember = dt.Columns["x"].ToString();          
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void gridView2_RowClick(object sender, RowClickEventArgs e)
        {
            try
            {
                rowHandel2 = e.RowHandle;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void gridView2_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
       {
            try
            {
                if (loaded)
                {
                    if (e.Column.Name == "Carton")
                    {
                        GridView view = (GridView)sender;
                        DataRow dataRow = view.GetFocusedDataRow();
                        double totalQuantityDelivery = Convert.ToDouble(dataRow["DeliveryQuantity"].ToString());
                        double cellValue = Convert.ToDouble(e.Value);
                        double re = 0;
                        if (cellValue > 0)
                        {
                            re = totalQuantityDelivery / cellValue;
                        }
                        view.SetRowCellValue(view.GetSelectedRows()[0], "NumOfCarton", re + "");
                    }
                    else if (e.Column.Name == "DeliveryQuantity")
                    {
                        GridView view = (GridView)sender;
                        DataRow dataRow = view.GetFocusedDataRow();
                        double Carton = Convert.ToDouble(dataRow["Carton"].ToString());
                        double cellValue = Convert.ToDouble(e.Value);
                        double re = 0;
                        if (Carton != 0)
                            re = cellValue / Carton;
                        if (Convert.ToDouble(dataRow["DeliveryQuantity"]) <= Convert.ToDouble(dataRow["TotalQuantity"]))
                        {
                            view.SetRowCellValue(view.GetSelectedRows()[0], "NumOfCarton", re + "");
                        }
                        else
                        {
                            view.SetRowCellValue(view.GetSelectedRows()[0], "DeliveryQuantity",  "0");
                        }
                    }
                    //else if (e.Column.Name == "Carton")
                    //{
                    //    GridView view = (GridView)sender;
                    //    DataRow dataRow = view.GetFocusedDataRow();
                    //    double NumOfCarton = Convert.ToDouble(dataRow["NumOfCarton"].ToString());
                    //    double cellValue = Convert.ToDouble(e.Value);
                    //    double re = cellValue * NumOfCarton;

                    //    view.SetRowCellValue(view.GetSelectedRows()[0], "DeliveryQuantity", re + "");
                    //}
                }
            }
            catch
            {
               // MessageBox.Show(ex.Message);
            }
        }
        private void txtRecivedQuantity_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    if (gridView1.SelectedRowsCount == 1)
                    {
                        loaded = false;
                        if (Convert.ToDouble(txtRecivedQuantity.Text) <= (Convert.ToDouble(row[5].ToString())- Convert.ToDouble(row[6].ToString())))
                        {
                            if (IsDelveryQuantityHaveValue(row[6].ToString()))
                            {
                                if (Convert.ToDouble(row[6].ToString()) < Convert.ToDouble(row[5].ToString()))
                                {
                                    addrow = row;
                                    SelectType = "oneRow";
                                    addNewRow(row);
                                    txtCode.Text = "";
                                    txtRecivedQuantity.Text = "";
                                    comStorePlace.DataSource = null;
                                }
                                else
                                {
                                    MessageBox.Show("البند تم تسليمه بالكامل");
                                }
                            }
                            //else
                            //{
                            //    addrow = row;
                            //    SelectType = "oneRow";
                            //    addNewRow(row);
                            //    txtCode.Text = "";
                            //    txtRecivedQuantity.Text = "";
                            //    comStorePlace.DataSource = null;
                            //}
                        }
                        else
                        {
                            txtRecivedQuantity.Text = "0";
                            txtRecivedQuantity.Focus();
                            MessageBox.Show("الكمية اكبر من اجمالي الكمية المطلوبة");
                        }
                    }
                     loaded = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                if (gridView1.RowCount > 0 && txtPermBillNumber.Text != "")
                {
                    dbconnection.Open();
                    List<DeliveryPermissionClass> listOfData = new List<DeliveryPermissionClass>();
                    for (int i = 0; i < gridView1.RowCount; i++)
                    {
                        DataRow row1 = gridView1.GetDataRow(gridView1.GetRowHandle(i));
                       
                        DeliveryPermissionClass deliveryPermissionClass = new DeliveryPermissionClass();
                        deliveryPermissionClass.Data_ID = (int)row1["Data_ID"];
                       
                        deliveryPermissionClass.Code = row1[1].ToString();
                        if (row1["الفئة"].ToString() == "عرض")
                        {
                            string itemName = "concat( product.Product_Name,' ',type.Type_Name,' ',factory.Factory_Name,' ',groupo.Group_Name,' ' ,COALESCE(color.Color_Name,''),' ',COALESCE(size.Size_Value,''),' ',COALESCE(sort.Sort_Value,''),' ',COALESCE(data.Classification,''),' ',COALESCE(data.Description,''))as 'البند'";
                            string DataTableRelations = "INNER JOIN type ON type.Type_ID = data.Type_ID INNER JOIN product ON product.Product_ID = data.Product_ID INNER JOIN factory ON data.Factory_ID = factory.Factory_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID LEFT outer JOIN color ON data.Color_ID = color.Color_ID LEFT outer  JOIN size ON data.Size_ID = size.Size_ID LEFT outer  JOIN sort ON data.Sort_ID = sort.Sort_ID";
                            string query = "select Code as'الكود'," + itemName + " from offer inner join offer_details on offer.Offer_ID=offer_details.Offer_ID inner join data on data.Data_ID=offer_details.Data_ID " + DataTableRelations + "  where offer.Offer_ID=" + (int)row1["Data_ID"];
                            MySqlCommand com = new MySqlCommand(query, dbconnection);
                            MySqlDataReader dr = com.ExecuteReader();
                            string str = "";
                            int cont = 1;
                            while (dr.Read())
                            {
                                str += cont + "-" + dr[1].ToString()+"\n";
                                cont++;
                            }
                            dr.Close();
                            deliveryPermissionClass.ItemName = "-" + row1[2].ToString() + "\n" + str;
                        }

                        else
                        {
                            deliveryPermissionClass.ItemName = row1[2].ToString();
                        }
                        deliveryPermissionClass.TotalQuantity = Convert.ToDouble(row1[5]);
                        if (row1[4].ToString() != ""&& Convert.ToDouble(row1[4]) != 0)
                        {
                            deliveryPermissionClass.NumOfCarton = Convert.ToDouble(row1[5]) / Convert.ToDouble(row1[4]);
                        }
                        else
                        {
                            deliveryPermissionClass.NumOfCarton = Convert.ToDouble(row1[5]);
                        }
                        deliveryPermissionClass.DeliveryQuantity = row1[7].ToString();
                        deliveryPermissionClass.StoreName = row1[10].ToString();
                        listOfData.Add(deliveryPermissionClass);
                    }
                    DeliveryPermissionReportViewer DeliveryPermissionReport;//= new DeliveryPermissionReportViewer(listOfData, txtPermBillNumber.Text);

                    if (txtClientID.Text!="")
                    {
                        DeliveryPermissionReport = new DeliveryPermissionReportViewer(listOfData, txtClientName.Text + " " + txtClientID.Text, txtPhoneNumber.Text, txtDelegate.Text , labDate.Text, txtPermBillNumber.Text + "  " + TypeBuy, "", txtBranchID.ToString(), comBranch.Text,"","","",true);
                        DeliveryPermissionReport.Show();
                    }
                    else if (txtCustomerID.Text!="")
                    {
                        DeliveryPermissionReport = new DeliveryPermissionReportViewer(listOfData, txtCustomerName.Text + " " + txtCustomerID.Text, txtPhoneNumber.Text, txtDelegate.Text, labDate.Text, txtPermBillNumber.Text + "  " + TypeBuy, "", txtBranchID.ToString(), comBranch.Text,"","","",true);
                        DeliveryPermissionReport.Show();
                    }
                  
                }
                else
                {
                    MessageBox.Show("insert correct value");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }

        //functions 
        public string displayBillDataFromPermatissionNumber(string PermissionNumber)
        {
            string CustomerBill_ID = "";
            string CustomerBill_ID_Store_ID = "";
            string query = "select CustomerBill_ID,Store_Name,customer_permissions.Store_ID from customer_permissions inner join store on customer_permissions.Store_ID=store.Store_ID where Permissin_ID=" + PermissionNumber;
            MySqlCommand com = new MySqlCommand(query,dbconnection);
            MySqlDataReader dr = com.ExecuteReader();
            while (dr.Read())
            {
                CustomerBill_ID = dr[0].ToString();
                labStoreName.Text = dr[1].ToString();
                Store_ID = dr[2].ToString();
                CustomerBill_ID_Store_ID = CustomerBill_ID + "*" + Store_ID;
            }
            dr.Close();

            query = "select * from customer_bill inner join shipping on customer_bill.CustomerBill_ID=shipping.CustomerBill_ID where customer_bill.CustomerBill_ID=" + CustomerBill_ID;
            com = new MySqlCommand(query, dbconnection);
            dr = com.ExecuteReader();
            while (dr.Read())
            {
                txtCustomerName.Text = dr["Customer_Name"].ToString();
                txtCustomerID.Text = dr["Customer_ID"].ToString();
                txtClientName.Text = dr["Client_Name"].ToString();
                txtClientID.Text = dr["Client_ID"].ToString();
                txtPhoneNumber.Text = dr["Phone"].ToString();
                //txtAddress.Text = dr["Address"].ToString();
            }
            dr.Close();

            return CustomerBill_ID_Store_ID;
        }
        public void displayBillDataFromCustomerBill(string BranchID,string BranchBillNumber)
        {
            string query = "select * from customer_bill  where Branch_ID=" + BranchID + " and Branch_BillNumber="+ BranchBillNumber;
            MySqlCommand com = new MySqlCommand(query, dbconnection);
            MySqlDataReader dr = com.ExecuteReader();
            while (dr.Read())
            {
                txtCustomerName.Text = dr["Customer_Name"].ToString();
                txtCustomerID.Text = dr["Customer_ID"].ToString();
                txtClientName.Text = dr["Client_Name"].ToString();
                txtClientID.Text = dr["Client_ID"].ToString();
            }
            dr.Close();
        }
        public void addNewRow(DataRow row)
        {
            if (!IsExist(row[0].ToString(), row[4].ToString()))
            {
                gridView2.AddNewRow();
             
            }
        }
        public void displayPermission()
        {
            if (radioBtnDriverDelivery.Checked)
            {
                string CustomerBill_ID_Store_ID = displayBillDataFromPermatissionNumber(txtPermBillNumber.Text);
                string query = "select * from customer_permissions_details where customer_permissions_ID=" + txtPermBillNumber.Text;
                MySqlCommand com = new MySqlCommand(query, dbconnection);
                MySqlDataReader dr = com.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Close();
                    string itemName = "concat( product.Product_Name,' ',type.Type_Name,' ',factory.Factory_Name,' ',groupo.Group_Name,' ' ,COALESCE(color.Color_Name,''),' ',COALESCE(size.Size_Value,''),' ',COALESCE(sort.Sort_Value,''),' ',COALESCE(data.Classification,''),' ',COALESCE(data.Description,''))as 'البند'";
                    string DataTableRelations = "INNER JOIN type ON type.Type_ID = data.Type_ID INNER JOIN product ON product.Product_ID = data.Product_ID INNER JOIN factory ON data.Factory_ID = factory.Factory_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID LEFT outer JOIN color ON data.Color_ID = color.Color_ID LEFT outer  JOIN size ON data.Size_ID = size.Size_ID LEFT outer  JOIN sort ON data.Sort_ID = sort.Sort_ID";
                    query = "select product_bill.Data_ID, Code as'الكود'," + itemName + ",Quantity as 'الكمية',sum(DeliveredQuantity) as 'الكمية المسلمة',customer_permissions_details.Carton as 'الكرتنة',product_bill.Returned as 'تم الاسترجاع' from product_bill inner join data on data.Data_ID=product_bill.Data_ID " + DataTableRelations + " left join customer_permissions_details on customer_permissions_details.Data_ID=product_bill.Data_ID where CustomerBill_ID=" + CustomerBill_ID_Store_ID.Split('*')[0] + " and Store_ID=" + CustomerBill_ID_Store_ID.Split('*')[1] + " group by product_bill.Data_ID";

                    MySqlDataAdapter ad = new MySqlDataAdapter(query, dbconnection);
                    DataTable dt = new DataTable();
                    ad.Fill(dt);
                    gridControl1.DataSource = dt;
                    gridView1.Columns[0].Visible = false;
                }
                else
                {
                    dr.Close();
                    string itemName = "concat( product.Product_Name,' ',type.Type_Name,' ',factory.Factory_Name,' ',groupo.Group_Name,' ' ,COALESCE(color.Color_Name,''),' ',COALESCE(size.Size_Value,''),' ',COALESCE(sort.Sort_Value,''),' ',COALESCE(data.Classification,''),' ',COALESCE(data.Description,''))as 'البند'";
                    string DataTableRelations = "INNER JOIN type ON type.Type_ID = data.Type_ID INNER JOIN product ON product.Product_ID = data.Product_ID INNER JOIN factory ON data.Factory_ID = factory.Factory_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID LEFT outer JOIN color ON data.Color_ID = color.Color_ID LEFT outer  JOIN size ON data.Size_ID = size.Size_ID LEFT outer  JOIN sort ON data.Sort_ID = sort.Sort_ID";
                    query = "select product_bill.Data_ID, Code as'الكود'," + itemName + ",Quantity as 'الكمية', '" + 0 + " ' as 'الكمية المسلمة',Cartons as 'الكرتنة',product_bill.Returned as 'تم الاسترجاع' from product_bill inner join data on data.Data_ID=product_bill.Data_ID " + DataTableRelations + " where CustomerBill_ID=" + CustomerBill_ID_Store_ID.Split('*')[0] + " and Store_ID=" + CustomerBill_ID_Store_ID.Split('*')[1];

                    MySqlDataAdapter ad = new MySqlDataAdapter(query, dbconnection);
                    DataTable dt = new DataTable();
                    ad.Fill(dt);
                    gridControl1.DataSource = dt;
                    gridView1.Columns[0].Visible = false;
                }
            }
            else
            {
                displayData();
            }
        }
        public void displayPermission(string permissionNum)
        {
            if (radioBtnDriverDelivery.Checked)
            {
                string CustomerBill_ID_Store_ID = displayBillDataFromPermatissionNumber(permissionNum);
                string query = "select * from customer_permissions_details where customer_permissions_ID=" + permissionNum;
                MySqlCommand com = new MySqlCommand(query, dbconnection);
                MySqlDataReader dr = com.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Close();
                    string itemName = "concat(type.Type_Name,' ',product.Product_Name,' ',COALESCE(color.Color_Name,''),' ',data.Description,' ',groupo.Group_Name,' ',factory.Factory_Name,' ',COALESCE(size.Size_Value,''),' ',COALESCE(sort.Sort_Value,''),' ',COALESCE(data.Classification,''))as 'البند'";
                    string DataTableRelations = "INNER JOIN type ON type.Type_ID = data.Type_ID INNER JOIN product ON product.Product_ID = data.Product_ID INNER JOIN factory ON data.Factory_ID = factory.Factory_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID LEFT outer JOIN color ON data.Color_ID = color.Color_ID LEFT outer  JOIN size ON data.Size_ID = size.Size_ID LEFT outer  JOIN sort ON data.Sort_ID = sort.Sort_ID";
                    query = "select product_bill.Data_ID, Code as'الكود'," + itemName + ",Quantity as 'الكمية',sum(DeliveredQuantity) as 'الكمية المسلمة',customer_permissions_details.Carton as 'الكرتنة',product_bill.Returned as 'تم الاسترجاع' from product_bill inner join data on data.Data_ID=product_bill.Data_ID " + DataTableRelations + " left join customer_permissions_details on customer_permissions_details.Data_ID=product_bill.Data_ID where CustomerBill_ID=" + CustomerBill_ID_Store_ID.Split('*')[0] + " and Store_ID=" + CustomerBill_ID_Store_ID.Split('*')[1] + " group by product_bill.Data_ID";

                    MySqlDataAdapter ad = new MySqlDataAdapter(query, dbconnection);
                    DataTable dt = new DataTable();
                    ad.Fill(dt);
                    gridControl1.DataSource = dt;
                    gridView1.Columns[0].Visible = false;
                }
                else
                {
                    dr.Close();
                    string itemName = "concat(type.Type_Name,' ',product.Product_Name,' ',COALESCE(color.Color_Name,''),' ',data.Description,' ',groupo.Group_Name,' ',factory.Factory_Name,' ',COALESCE(size.Size_Value,''),' ',COALESCE(sort.Sort_Value,''),' ',COALESCE(data.Classification,''))as 'البند'";
                    string DataTableRelations = "INNER JOIN type ON type.Type_ID = data.Type_ID INNER JOIN product ON product.Product_ID = data.Product_ID INNER JOIN factory ON data.Factory_ID = factory.Factory_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID LEFT outer JOIN color ON data.Color_ID = color.Color_ID LEFT outer  JOIN size ON data.Size_ID = size.Size_ID LEFT outer  JOIN sort ON data.Sort_ID = sort.Sort_ID";
                    query = "select product_bill.Data_ID, Code as'الكود'," + itemName + ",Quantity as 'الكمية', '" + 0 + " ' as 'الكمية المسلمة',Cartons as 'الكرتنة',product_bill.Returned as 'تم الاسترجاع' from product_bill inner join data on data.Data_ID=product_bill.Data_ID " + DataTableRelations + " where CustomerBill_ID=" + CustomerBill_ID_Store_ID.Split('*')[0] + " and Store_ID=" + CustomerBill_ID_Store_ID.Split('*')[1];

                    MySqlDataAdapter ad = new MySqlDataAdapter(query, dbconnection);
                    DataTable dt = new DataTable();
                    ad.Fill(dt);
                    gridControl1.DataSource = dt;
                    gridView1.Columns[0].Visible = false;
                }
            }
            else
            {
                string itemName = "concat(type.Type_Name,' ',product.Product_Name,' ',COALESCE(color.Color_Name,''),' ',data.Description,' ',groupo.Group_Name,' ',factory.Factory_Name,' ',COALESCE(size.Size_Value,''),' ',COALESCE(sort.Sort_Value,''),' ',COALESCE(data.Classification,''))as 'البند'";
                string DataTableRelations = "INNER JOIN type ON type.Type_ID = data.Type_ID INNER JOIN product ON product.Product_ID = data.Product_ID INNER JOIN factory ON data.Factory_ID = factory.Factory_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID LEFT outer JOIN color ON data.Color_ID = color.Color_ID LEFT outer  JOIN size ON data.Size_ID = size.Size_ID LEFT outer  JOIN sort ON data.Sort_ID = sort.Sort_ID";
                string query = "select product_bill.Data_ID, Code as'الكود'," + itemName + ",Quantity as 'الكمية',sum(DeliveredQuantity) as 'الكمية المسلمة',customer_permissions_details.Carton as 'الكرتنة',store.Store_Name as 'المخزن',product_bill.Store_ID,product_bill.Returned as 'تم الاسترجاع' from product_bill inner join store on product_bill.Store_ID=store.Store_ID inner join data on data.Data_ID=product_bill.Data_ID " + DataTableRelations + " left join customer_permissions_details on customer_permissions_details.Data_ID=product_bill.Data_ID inner join customer_bill on customer_bill.CustomerBill_ID=product_bill.CustomerBill_ID where RecivedType='العميل' and product_bill.CustomerBill_ID=" + permissionNum + " group by product_bill.Data_ID";

                MySqlDataAdapter ad = new MySqlDataAdapter(query, dbconnection);
                DataTable dt = new DataTable();
                ad.Fill(dt);
                gridControl1.DataSource = dt;
                gridView1.Columns[0].Visible = false;
                gridView1.Columns[7].Visible = false;
                displayBillDataFromCustomerBill(txtBranchID.Text,permissionNum);
            }
        }
        public bool IsExist(string Data_ID,string carton1)
        {
            DataView dv = (DataView)gridView2.DataSource;
            DataTable dt = dv.Table;
            foreach (DataRow item in dt.Rows)
            {
                double itemCarton = Convert.ToDouble(item[5].ToString());
                if (carton1 != "")
                {
                    double carton = Convert.ToDouble(carton1);
                    if (item[0].ToString() == Data_ID && itemCarton == carton)
                        return true;
                }
                else
                {
                    if (item[0].ToString() == Data_ID )
                        return true;
                }
            }
            return false;
        }
        public void clear()
        {
            foreach (Control item in groupBox1.Controls)
            {
                if (item is TextBox)
                    item.Text = "";
            }
            foreach (Control item in groupBox2.Controls)
            {
                if (item is TextBox)
                    item.Text = "";
            }
            foreach (Control item in groupBox3.Controls)
            {
                if (item is TextBox)
                    item.Text = "";
            }
            txtPermBillNumber.Text = labStoreName.Text = "";
            comStorePlace.DataSource = null;
            gridControl1.DataSource = null;
            DataHelper dh = new DataHelper(DSparametr.simpleDS);
            gridControl2.DataSource = dh.DataSet;
            gridControl2.DataMember = dh.DataMember;
            addrow = null;
            rowHandel2 = -1;
            SelectType = "";
        }
        public void displayData()
        {
            string query = "select CustomerBill_ID,Type_Buy from customer_bill where Branch_BillNumber=" + txtPermBillNumber.Text + " and Branch_ID=" + txtBranchID.Text;

            MySqlCommand com = new MySqlCommand(query, dbconnection);
            MySqlDataReader dr = com.ExecuteReader();
            int id=-1;
            while (dr.Read())
            {
               id = Convert.ToInt32(dr[0]);
               TypeBuy = dr[1].ToString();
            }
            dr.Close();

            displayCustomerData(id.ToString());
            gridControl1.DataSource = null;
            gridView1.Columns.Clear();
            DataTable dtAll = new DataTable();
            query = "select data.Data_ID,data.Code as 'الكود',concat(type.Type_Name,' ',product.Product_Name,' ',COALESCE(color.Color_Name,''),' ',COALESCE(data.Description,''),' ',groupo.Group_Name,' ',factory.Factory_Name,' ',COALESCE(size.Size_Value,''),' ',COALESCE(sort.Sort_Value,'')) as 'الاسم',product_bill.Type as 'الفئة',data.Carton as 'الكرتنة',product_bill.Quantity as 'الكمية'"/*,(case  when data.Carton !=0 then (product_bill.Quantity /data.Carton) end ) as 'عدد الكراتين'*/+",'" + "" + "' as 'الكمية المسلمة',data.Description as 'الوصف',product_bill.Returned as 'تم الاسترجاع',store.Store_Name as 'المخزن',Delegate_Name,product_bill.CustomerBill_ID,product_bill.Store_ID from product_bill inner join data on data.Data_ID=product_bill.Data_ID LEFT JOIN color ON color.Color_ID = data.Color_ID LEFT JOIN size ON size.Size_ID = data.Size_ID LEFT JOIN sort ON sort.Sort_ID = data.Sort_ID INNER JOIN type ON type.Type_ID = data.Type_ID INNER JOIN product ON product.Product_ID = data.Product_ID INNER JOIN factory ON data.Factory_ID = factory.Factory_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID inner join delegate on delegate.Delegate_ID=product_bill.Delegate_ID inner join store on product_bill.Store_ID=store.Store_ID where CustomerBill_ID=" + id + " and product_bill.Type='بند'  and (product_bill.Returned='لا' or product_bill.Returned='جزء')";
            MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
            DataTable dtProduct = new DataTable();
            da.Fill(dtProduct);

            query = "select sets.Set_ID as 'Data_ID',sets.Set_ID as 'الكود',sets.Set_Name as 'الاسم',product_bill.Type as 'الفئة', product_bill.Quantity as 'الكمية','" + " " + " ' as 'الكمية المسلمة',sets.Description as 'الوصف',product_bill.Returned as 'تم الاسترجاع',Delegate_Name,product_bill.CustomerBill_ID,product_bill.Store_ID from product_bill inner join sets on sets.Set_ID=product_bill.Data_ID inner join delegate on delegate.Delegate_ID=product_bill.Delegate_ID where CustomerBill_ID=" + id + " and product_bill.Type='طقم' and (product_bill.Returned='لا' or product_bill.Returned='جزء')";
            da = new MySqlDataAdapter(query, dbconnection);
            DataTable dtSet = new DataTable();
            da.Fill(dtSet);

            query = "select offer.Offer_ID as 'Data_ID',offer.Offer_ID as 'الكود',offer.Offer_Name as 'الاسم',product_bill.Type as 'الفئة', product_bill.Quantity as 'الكمية','" + " " + " ' as 'الكمية المسلمة',offer.Description as 'الوصف',product_bill.Returned as 'تم الاسترجاع',Delegate_Name,product_bill.CustomerBill_ID,product_bill.Store_ID from product_bill inner join offer on offer.Offer_ID=product_bill.Data_ID inner join delegate on delegate.Delegate_ID=product_bill.Delegate_ID where CustomerBill_ID=" + id + " and product_bill.Type='عرض' and (product_bill.Returned='لا' or product_bill.Returned='جزء')";
            da = new MySqlDataAdapter(query, dbconnection);
            DataTable dtOffer = new DataTable();
            da.Fill(dtOffer);

            dtAll = dtProduct.Copy();
            dtAll.Merge(dtSet, true, MissingSchemaAction.Ignore);
            dtAll.Merge(dtOffer, true, MissingSchemaAction.Ignore);
            gridControl1.DataSource = dtAll;
            gridView1.Columns[0].Visible = false;
            gridView1.Columns["CustomerBill_ID"].Visible = false;
            //gridView1.Columns["الفئة"].Visible = false;
            gridView1.Columns["الوصف"].Visible = false;
            gridView1.Columns["Delegate_Name"].Visible = false;
            gridView1.Columns["Store_ID"].Visible = false;
            txtDelegate.Text = gridView1.GetDataRow(0)["Delegate_Name"].ToString();
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
                if (txtCustomerID.Text!="")
                    txtPhoneNumber.Text = dr[4].ToString();
                if (txtClientID.Text != "")
                    txtPhoneNumber.Text = dr[5].ToString();
                labDate.Text = dr[6].ToString();
            }
            dr.Close();
        }
        public bool IsBillHavePerviousPermission()
        {
            string query = "select Customer_Permissin_ID from customer_permissions where BranchBillNumber=" + txtPermBillNumber.Text + " and Branch_ID=" + txtBranchID.Text;
            MySqlCommand com = new MySqlCommand(query, dbconnection);
            MySqlDataReader dr = com.ExecuteReader();
            while (dr.Read())
            {
                dr.Close();
                return true;
            }
            dr.Close();

            return false;
        }
        public void displayDatawithPerviousPer()
        {
            string query = "select CustomerBill_ID from customer_bill where Branch_BillNumber=" + txtPermBillNumber.Text + " and Branch_ID=" + txtBranchID.Text;

            MySqlCommand com = new MySqlCommand(query, dbconnection);
            int id = Convert.ToInt32(com.ExecuteScalar());
            displayCustomerData(id.ToString());
            gridControl1.DataSource = null;
            gridView1.Columns.Clear();
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

            gridControl1.DataSource = dtAll;
            gridView1.Columns[0].Visible = false;
            gridView1.Columns["الفئة"].Visible = false;
            //gridView1.Columns["الوصف"].Visible = false;
            //gridView1.Columns["Delegate_Name"].Visible = false;
            //gridView1.Columns["Store_ID"].Visible = false;
            //txtDelegate.Text = gridView1.GetDataRow(0)["Delegate_Name"].ToString();
        }
        public string getDeliveredDataItems(string type)
        {
            string query = "select group_concat(distinct Data_ID) from customer_permissions_details inner join customer_permissions on customer_permissions.Customer_Permissin_ID=customer_permissions_details.Customer_Permissin_ID where ItemType='" + type+"' and BranchBillNumber=" + txtPermBillNumber.Text + " and Branch_ID=" + txtBranchID.Text;
            MySqlCommand com = new MySqlCommand(query,dbconnection);
            string result = com.ExecuteScalar().ToString();

            return result;
        }
        public void IsBillRecived()
        {
            DataTable dtAll = new DataTable();
            string query = "select data.Data_ID,data.Code as 'الكود',customer_permissions_details.ItemType as 'الفئة',customer_permissions_details.Carton as 'الكرتنة',customer_permissions_details.Quantity as 'الكمية',sum(customer_permissions_details.DeliveredQuantity) as 'الكمية المستلمة' from customer_permissions_details inner join data on data.Data_ID=customer_permissions_details.Data_ID  inner join customer_permissions on  customer_permissions.Customer_Permissin_ID=customer_permissions_details.Customer_Permissin_ID where customer_permissions_details.ItemType='بند' and BranchBillNumber=" + txtPermBillNumber.Text + " and Branch_ID=" + txtBranchID.Text + " group by data.Data_ID";
            MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
            DataTable dtper = new DataTable();
            da.Fill(dtper);
            dtAll = dtper.Copy();

            query = "select sets.Set_ID as 'Data_ID',sets.Set_ID as 'الكود',customer_permissions_details.ItemType as 'الفئة',customer_permissions_details.Carton as 'الكرتنة',customer_permissions_details.Quantity as 'الكمية',sum(customer_permissions_details.DeliveredQuantity) as 'الكمية المستلمة' from customer_permissions_details inner join sets on sets.Set_ID=customer_permissions_details.Data_ID inner join customer_permissions on  customer_permissions.Customer_Permissin_ID=customer_permissions_details.Customer_Permissin_ID where customer_permissions_details.ItemType='طقم' and BranchBillNumber=" + txtPermBillNumber.Text + " and Branch_ID=" + txtBranchID.Text + " group by sets.Set_ID";
            da = new MySqlDataAdapter(query, dbconnection);
            DataTable dtper1 = new DataTable();
            da.Fill(dtper1);
            dtAll.Merge(dtper1, true, MissingSchemaAction.Ignore);
       

            bool flag = true;
            dbconnection.Close();
            if (IsQuantityEqual())
            {
                dbconnection.Open();
                foreach (DataRow item in dtAll.Rows)
                {
                    if (Convert.ToDouble(item[4]) > Convert.ToDouble(item[5]))
                    {
                        flag = false;
                        query = "update customer_bill set RecivedFlag='تم تسليم جزء' where Branch_BillNumber=" + txtPermBillNumber.Text + " and Branch_ID=" + txtBranchID.Text;
                        MySqlCommand com = new MySqlCommand(query, dbconnection);
                        com.ExecuteNonQuery();
                    }

                }
                if (flag)
                {
                    query = "update customer_bill set RecivedFlag='تم' where Branch_BillNumber=" + txtPermBillNumber.Text + " and Branch_ID=" + txtBranchID.Text;
                    MySqlCommand com = new MySqlCommand(query, dbconnection);
                    com.ExecuteNonQuery();
                }
            }
            else
            {
                dbconnection.Open();
                query = "update customer_bill set RecivedFlag='تم تسليم جزء' where Branch_BillNumber=" + txtPermBillNumber.Text + " and Branch_ID=" + txtBranchID.Text;
                MySqlCommand com = new MySqlCommand(query, dbconnection);
                com.ExecuteNonQuery();
            }
         
        }
        public bool IsDelveryQuantityHaveValue(string x)
        {
            try
            {
                Convert.ToDouble(x);
                return true;
            }
            catch 
            {
                return false;
            }
        }

        public bool IsQuantityEqual()
        {
            dbconnection.Open();
            //check number of records
            string query = "select CustomerBill_ID from customer_bill where Branch_BillNumber=" + txtPermBillNumber.Text + " and Branch_ID=" + txtBranchID.Text;
            MySqlCommand com1 = new MySqlCommand(query, dbconnection);
            int id = Convert.ToInt16(com1.ExecuteScalar());

            query = "select count(*) from product_bill where CustomerBill_ID=" + id;
            com1 = new MySqlCommand(query, dbconnection);
            int count1 = Convert.ToInt16(com1.ExecuteScalar());

            query = "select Customer_Permissin_ID from customer_permissions where CustomerBill_ID=" + id;
            com1 = new MySqlCommand(query, dbconnection);
            int Customer_Permissin_ID = Convert.ToInt16(com1.ExecuteScalar());

            query = "select count(*) from customer_permissions_details where Customer_Permissin_ID=" + id;
            com1 = new MySqlCommand(query, dbconnection);
            int count2 = Convert.ToInt16(com1.ExecuteScalar());

            dbconnection.Close();
            if (count1 == count2)
                return true;
            else
                return false;
        }
    }

   
}

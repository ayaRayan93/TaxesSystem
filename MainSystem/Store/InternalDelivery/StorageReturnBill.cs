using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MainSystem
{
    public partial class StorageReturnBill : Form
    {
        MySqlConnection dbconnection, dbconnection2, connectionReader, connectionReader2;
        
        bool flag = false;
        int[] addedRecordIDs;
        int recordCount = 0;
        List<int> listOfRow2In;
        int EmpBranchId = 0;
        int storeId = 0;
        bool flagCarton = false;
        DataRow row1 = null;
        bool loaded = false;
        int importSupplierPermissionID = 0;

        public StorageReturnBill(MainForm SalesMainForm, DevExpress.XtraTab.XtraTabControl TabControlStores)
        {
            InitializeComponent();
            dbconnection = new MySqlConnection(connection.connectionString);
            dbconnection2 = new MySqlConnection(connection.connectionString);
            connectionReader = new MySqlConnection(connection.connectionString);
            connectionReader2 = new MySqlConnection(connection.connectionString);
            addedRecordIDs = new int[100];
            listOfRow2In = new List<int>();
            comSupplier.AutoCompleteMode = AutoCompleteMode.Suggest;
            comSupplier.AutoCompleteSource = AutoCompleteSource.ListItems;
        }

        private void StorageReturnBill_Load(object sender, EventArgs e)
        {
            try
            {
                EmpBranchId = UserControl.EmpBranchID;

                string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"Store.txt");
                storeId = Convert.ToInt16(System.IO.File.ReadAllText(path));

                string query = "select * from supplier";
                MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                DataTable dt = new DataTable();
                da.Fill(dt);
                comSupplier.DataSource = dt;
                comSupplier.DisplayMember = dt.Columns["Supplier_Name"].ToString();
                comSupplier.ValueMember = dt.Columns["Supplier_ID"].ToString();
                comSupplier.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnAddToReturnBill_Click(object sender, EventArgs e)
        {
            try
            {
                if (row1 != null && comSupplier.SelectedValue != null && txtPermissionNum.Text != "" && txtCarton.Text != "" && txtBalat.Text != "" && txtCode.Text != "" && txtTotalMeter.Text != "")
                {
                    int NoBalatat = 0;
                    int NoCartons = 0;
                    int permNum = 0;
                    double total = 0;
                    if (int.TryParse(txtBalat.Text, out NoBalatat) && int.TryParse(txtCarton.Text, out NoCartons) && int.TryParse(txtPermissionNum.Text, out permNum) && double.TryParse(txtTotalMeter.Text, out total))
                    {
                        double carton = Convert.ToDouble(row1["الكرتنة"].ToString());

                        dbconnection.Open();
                        dbconnection2.Open();
                        int storageImportPermissionID = 0;
                        string query = "select StorageImportPermission_ID from storage_import_permission where Import_Permission_Number=" + permNum + " and Store_ID=" + storeId;
                        MySqlCommand com = new MySqlCommand(query, dbconnection);
                        if (com.ExecuteScalar() == null)
                        {
                            query = "insert into storage_import_permission (Store_ID,Storage_Date,Import_Permission_Number) values (@Store_ID,@Storage_Date,@Import_Permission_Number)";
                            com = new MySqlCommand(query, dbconnection);
                            com.Parameters.Add("@Store_ID", MySqlDbType.Int16);
                            com.Parameters["@Store_ID"].Value = storeId;
                            com.Parameters.Add("@Storage_Date", MySqlDbType.DateTime, 0);
                            com.Parameters["@Storage_Date"].Value = DateTime.Now;
                            com.Parameters.Add("@Import_Permission_Number", MySqlDbType.Int16);
                            com.Parameters["@Import_Permission_Number"].Value = permNum;
                            com.ExecuteNonQuery();

                            query = "select StorageImportPermission_ID from storage_import_permission order by StorageImportPermission_ID desc limit 1";
                            com = new MySqlCommand(query, dbconnection);
                            storageImportPermissionID = Convert.ToInt16(com.ExecuteScalar().ToString());

                            query = "SELECT gate.Car_ID,gate.Car_Number,gate.Driver_ID,gate.Driver_Name FROM gate INNER JOIN gate_permission ON gate_permission.Permission_Number = gate.Permission_Number where gate.Supplier_ID=" + comSupplier.SelectedValue.ToString() + " and gate_permission.Supplier_PermissionNumber=" /*+ supPermNum*/ + " and gate_permission.Type='دخول'";
                            com = new MySqlCommand(query, dbconnection2);
                            MySqlDataReader dr2 = com.ExecuteReader();
                            if (dr2.HasRows)
                            {
                                while (dr2.Read())
                                {
                                    query = "insert into import_supplier_permission (Supplier_ID,Supplier_Permission_Number,Car_ID,Car_Number,Driver_ID,Driver_Name,StorageImportPermission_ID) values (@Supplier_ID,@Supplier_Permission_Number,@Car_ID,@Car_Number,@Driver_ID,@Driver_Name,@StorageImportPermission_ID)";
                                    com = new MySqlCommand(query, dbconnection);
                                    com.Parameters.Add("@Supplier_ID", MySqlDbType.Int16);
                                    com.Parameters["@Supplier_ID"].Value = Convert.ToInt16(comSupplier.SelectedValue.ToString());
                                    com.Parameters.Add("@Supplier_Permission_Number", MySqlDbType.Int16);
                                    //com.Parameters["@Supplier_Permission_Number"].Value = supPermNum;
                                    if (dr2["Car_ID"].ToString() != "")
                                    {
                                        com.Parameters.Add("@Car_ID", MySqlDbType.Int16);
                                        com.Parameters["@Car_ID"].Value = Convert.ToInt16(dr2["Car_ID"].ToString());
                                    }
                                    else
                                    {
                                        com.Parameters.Add("@Car_ID", MySqlDbType.Int16);
                                        com.Parameters["@Car_ID"].Value = null;
                                    }
                                    com.Parameters.Add("@Car_Number", MySqlDbType.VarChar);
                                    com.Parameters["@Car_Number"].Value = dr2["Car_Number"].ToString();
                                    if (dr2["Driver_ID"].ToString() != "")
                                    {
                                        com.Parameters.Add("@Driver_ID", MySqlDbType.Int16);
                                        com.Parameters["@Driver_ID"].Value = Convert.ToInt16(dr2["Driver_ID"].ToString());
                                    }
                                    else
                                    {
                                        com.Parameters.Add("@Driver_ID", MySqlDbType.Int16);
                                        com.Parameters["@Driver_ID"].Value = null;
                                    }
                                    com.Parameters.Add("@Driver_Name", MySqlDbType.VarChar);
                                    com.Parameters["@Driver_Name"].Value = dr2["Driver_Name"].ToString();
                                    com.Parameters.Add("@StorageImportPermission_ID", MySqlDbType.Int16);
                                    com.Parameters["@StorageImportPermission_ID"].Value = storageImportPermissionID;
                                    com.ExecuteNonQuery();

                                    query = "select ImportSupplierPermission_ID from import_supplier_permission order by ImportSupplierPermission_ID desc limit 1";
                                    com = new MySqlCommand(query, dbconnection);
                                    importSupplierPermissionID = Convert.ToInt16(com.ExecuteScalar().ToString());
                                }
                                dr2.Close();
                            }
                            else
                            {
                                importSupplierPermissionID = 0;
                            }
                        }
                        else
                        {
                            storageImportPermissionID = Convert.ToInt16(com.ExecuteScalar().ToString());
                            query = "select ImportSupplierPermission_ID from import_supplier_permission where StorageImportPermission_ID=" + storageImportPermissionID + " and Supplier_ID=" + comSupplier.SelectedValue.ToString() + " and Supplier_Permission_Number=" /*+ supPermNum*/;
                            com = new MySqlCommand(query, dbconnection);
                            if (com.ExecuteScalar() == null)
                            {
                                query = "SELECT gate.Car_ID,gate.Car_Number,gate.Driver_ID,gate.Driver_Name FROM gate INNER JOIN gate_permission ON gate_permission.Permission_Number = gate.Permission_Number where gate.Supplier_ID=" + comSupplier.SelectedValue.ToString() + " and gate_permission.Supplier_PermissionNumber=" /*+ supPermNum*/ + " and gate_permission.Type='دخول'";
                                com = new MySqlCommand(query, dbconnection2);
                                MySqlDataReader dr2 = com.ExecuteReader();
                                if (dr2.HasRows)
                                {
                                    while (dr2.Read())
                                    {
                                        query = "insert into import_supplier_permission (Supplier_ID,Supplier_Permission_Number,Car_ID,Car_Number,Driver_ID,Driver_Name,StorageImportPermission_ID) values (@Supplier_ID,@Supplier_Permission_Number,@Car_ID,@Car_Number,@Driver_ID,@Driver_Name,@StorageImportPermission_ID)";
                                        com = new MySqlCommand(query, dbconnection);
                                        com.Parameters.Add("@Supplier_ID", MySqlDbType.Int16);
                                        com.Parameters["@Supplier_ID"].Value = comSupplier.SelectedValue.ToString();
                                        com.Parameters.Add("@Supplier_Permission_Number", MySqlDbType.Int16);
                                        //com.Parameters["@Supplier_Permission_Number"].Value = supPermNum;
                                        if (dr2["Car_ID"].ToString() != "")
                                        {
                                            com.Parameters.Add("@Car_ID", MySqlDbType.Int16);
                                            com.Parameters["@Car_ID"].Value = Convert.ToInt16(dr2["Car_ID"].ToString());
                                        }
                                        else
                                        {
                                            com.Parameters.Add("@Car_ID", MySqlDbType.Int16);
                                            com.Parameters["@Car_ID"].Value = null;
                                        }
                                        com.Parameters.Add("@Car_Number", MySqlDbType.VarChar);
                                        com.Parameters["@Car_Number"].Value = dr2["Car_Number"].ToString();
                                        if (dr2["Driver_ID"].ToString() != "")
                                        {
                                            com.Parameters.Add("@Driver_ID", MySqlDbType.Int16);
                                            com.Parameters["@Driver_ID"].Value = Convert.ToInt16(dr2["Driver_ID"].ToString());
                                        }
                                        else
                                        {
                                            com.Parameters.Add("@Driver_ID", MySqlDbType.Int16);
                                            com.Parameters["@Driver_ID"].Value = null;
                                        }
                                        com.Parameters.Add("@Driver_Name", MySqlDbType.VarChar);
                                        com.Parameters["@Driver_Name"].Value = dr2["Driver_Name"].ToString();
                                        com.Parameters.Add("@StorageImportPermission_ID", MySqlDbType.Int16);
                                        com.Parameters["@StorageImportPermission_ID"].Value = storageImportPermissionID;
                                        com.ExecuteNonQuery();

                                        query = "select ImportSupplierPermission_ID from import_supplier_permission order by ImportSupplierPermission_ID desc limit 1";
                                        com = new MySqlCommand(query, dbconnection);
                                        importSupplierPermissionID = Convert.ToInt16(com.ExecuteScalar().ToString());
                                    }
                                    dr2.Close();
                                }
                                else
                                {
                                    importSupplierPermissionID = 0;
                                }
                            }
                            else
                            {
                                importSupplierPermissionID = Convert.ToInt16(com.ExecuteScalar().ToString());
                            }
                        }

                        if (importSupplierPermissionID > 0)
                        {
                            query = "insert into supplier_permission_details (Store_ID,Store_Place_ID,Date,Data_ID,Balatat,Carton_Balata,Total_Meters,Note,ImportSupplierPermission_ID) values (@Store_ID,@Store_Place_ID,@Date,@Data_ID,@Balatat,@Carton_Balata,@Total_Meters,@Note,@ImportSupplierPermission_ID)";
                            com = new MySqlCommand(query, dbconnection);
                            com.Parameters.Add("@Store_ID", MySqlDbType.Int16);
                            com.Parameters["@Store_ID"].Value = storeId;
                            com.Parameters.Add("@Store_Place_ID", MySqlDbType.Int16);
                            //com.Parameters["@Store_Place_ID"].Value = comStorePlace.SelectedValue.ToString();
                            com.Parameters.Add("@Date", MySqlDbType.DateTime, 0);
                            com.Parameters["@Date"].Value = DateTime.Now;
                            if (carton > 0)
                            {
                                com.Parameters.Add("@Balatat", MySqlDbType.Int16);
                                com.Parameters["@Balatat"].Value = NoBalatat;
                                com.Parameters.Add("@Carton_Balata", MySqlDbType.Int16);
                                com.Parameters["@Carton_Balata"].Value = NoCartons;
                            }
                            else
                            {
                                com.Parameters.Add("@Balatat", MySqlDbType.Int16);
                                com.Parameters["@Balatat"].Value = null;
                                com.Parameters.Add("@Carton_Balata", MySqlDbType.Int16);
                                com.Parameters["@Carton_Balata"].Value = null;
                            }
                            com.Parameters.Add("@Data_ID", MySqlDbType.Int16);
                            com.Parameters["@Data_ID"].Value = row1[0].ToString();
                            com.Parameters.Add("@Total_Meters", MySqlDbType.Decimal);
                            com.Parameters["@Total_Meters"].Value = total;
                            com.Parameters.Add("@Note", MySqlDbType.VarChar);
                            com.Parameters["@Note"].Value = txtReason.Text;
                            com.Parameters.Add("@ImportSupplierPermission_ID", MySqlDbType.Int16);
                            com.Parameters["@ImportSupplierPermission_ID"].Value = importSupplierPermissionID;
                            com.ExecuteNonQuery();
                        }
                        else
                        {
                            MessageBox.Show("برجاء التاكد من رقم اذن الاستلام");
                            dbconnection.Close();
                            dbconnection2.Close();
                            return;
                        }

                        query = "select Total_Meters from storage where Data_ID=" + row1["Data_ID"].ToString() + " and Store_ID=" + storeId + " and Store_Place_ID=" /*+ comStorePlace.SelectedValue.ToString()*/;
                        com = new MySqlCommand(query, dbconnection);
                        if (com.ExecuteScalar() != null)
                        {
                            double totalMeter = Convert.ToDouble(com.ExecuteScalar().ToString());

                            query = "update storage set Total_Meters=" + (totalMeter + total) + " where Data_ID=" + row1["Data_ID"].ToString() + " and Store_ID=" + storeId + " and Store_Place_ID=" /*+ comStorePlace.SelectedValue.ToString()*/;
                            com = new MySqlCommand(query, dbconnection);
                            com.ExecuteNonQuery();
                        }
                        else
                        {
                            query = "insert into Storage (Store_ID,Store_Place_ID,Type,Data_ID,Total_Meters) values (@Store_ID,@Store_Place_ID,@Type,@Data_ID,@Total_Meters)";
                            com = new MySqlCommand(query, dbconnection);
                            com.Parameters.Add("@Store_ID", MySqlDbType.Int16);
                            com.Parameters["@Store_ID"].Value = storeId;
                            com.Parameters.Add("@Store_Place_ID", MySqlDbType.Int16);
                            //com.Parameters["@Store_Place_ID"].Value = comStorePlace.SelectedValue.ToString();
                            com.Parameters.Add("@Type", MySqlDbType.VarChar);
                            com.Parameters["@Type"].Value = "بند";
                            com.Parameters.Add("@Data_ID", MySqlDbType.Int16);
                            com.Parameters["@Data_ID"].Value = row1[0].ToString();
                            com.Parameters.Add("@Total_Meters", MySqlDbType.Decimal);
                            com.Parameters["@Total_Meters"].Value = total;
                            com.ExecuteNonQuery();
                        }

                        comSupplier.Enabled = false;
                        comSupplier.DropDownStyle = ComboBoxStyle.DropDownList;

                        search();

                        txtCode.Text = "";
                        txtTotalMeter.Text = "0";
                        txtCarton.Text = "0";
                        txtBalat.Text = "0";
                        txtReason.Text = "";
                    }
                    else
                    {
                        MessageBox.Show("برجاء التاكد من ادخال البيانات بطريقة صحيحة");
                    }
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
            dbconnection.Close();
            dbconnection2.Close();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                DataRow row2 = gridView2.GetDataRow(gridView2.FocusedRowHandle);
                if (row2 != null)
                {
                    if (MessageBox.Show("هل انت متاكد انك تريد الحذف؟", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        dbconnection.Open();

                        string query = "select Total_Meters from storage where Store_ID=" + storeId + " and Store_Place_ID=" + row2["Store_Place_ID"].ToString() + " and Data_ID=" + row2["Data_ID"].ToString();
                        MySqlCommand com = new MySqlCommand(query, dbconnection);
                        if (com.ExecuteScalar() != null)
                        {
                            double totalf = Convert.ToInt16(com.ExecuteScalar());
                            if ((totalf - Convert.ToDouble(row2["متر/قطعة"].ToString())) >= 0)
                            {
                                //Store_ID=" + storeId + " and Store_Place_ID=" + row2["Store_Place_ID"].ToString() + " and  Data_ID=" + row2["Data_ID"].ToString()
                                query = "delete from supplier_permission_details where Supplier_Permission_Details_ID=" + row2["Supplier_Permission_Details_ID"].ToString();
                                com = new MySqlCommand(query, dbconnection);
                                com.ExecuteNonQuery();

                                query = "update storage set Total_Meters=" + (totalf - Convert.ToDouble(row2["متر/قطعة"].ToString())) + " where Store_ID=" + storeId + " and Store_Place_ID=" + row2["Store_Place_ID"].ToString() + " and Data_ID=" + row2["Data_ID"].ToString();
                                com = new MySqlCommand(query, dbconnection);
                                com.ExecuteNonQuery();

                                search();
                            }
                            else
                            {
                                MessageBox.Show("لا يوجد كمية كافية");
                            }
                        }
                        else
                        {
                            MessageBox.Show("لا يوجد كمية كافية");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }
        
        private void txtPermissionNum_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtPermissionNum.Text != "")
                {
                    dbconnection.Close();

                    search();

                    dbconnection.Open();
                    string q = "select import_supplier_permission.Supplier_ID from import_supplier_permission INNER JOIN storage_import_permission ON storage_import_permission.StorageImportPermission_ID = import_supplier_permission.StorageImportPermission_ID where storage_import_permission.Import_Permission_Number=" + txtPermissionNum.Text + " and storage_import_permission.Store_ID=" + storeId;
                    MySqlCommand com = new MySqlCommand(q, dbconnection);
                    loaded = false;
                    if (com.ExecuteScalar() != null)
                    {
                        comSupplier.SelectedValue = com.ExecuteScalar().ToString();
                        comSupplier.Enabled = false;
                        comSupplier.DropDownStyle = ComboBoxStyle.DropDownList;
                    }
                    else
                    {
                        comSupplier.SelectedIndex = -1;
                        comSupplier.Enabled = true;
                        comSupplier.DropDownStyle = ComboBoxStyle.DropDown;
                    }
                    loaded = true;
                }
                else
                {
                    gridControl2.DataSource = null;

                    loaded = false;
                    comSupplier.SelectedIndex = -1;
                    loaded = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }

        private void txtCarton_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (row1 != null && !flagCarton)
                {
                    int NoCartons = 0;
                    double totalMeter = 0;
                    double carton = double.Parse(row1["Carton"].ToString());
                    if (carton > 0)
                    {
                        if (int.TryParse(txtCarton.Text, out NoCartons))
                        { }
                        if (double.TryParse(txtTotalMeter.Text, out totalMeter))
                        { }

                        double total = carton * NoCartons;
                        flag = true;
                        txtTotalMeter.Text = (total).ToString();
                        flag = false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void txtTotalMeter_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (row1 != null && !flag)
                {
                    double totalMeter = 0;
                    if (double.TryParse(txtTotalMeter.Text, out totalMeter))
                    {
                        double carton = double.Parse(row1["Carton"].ToString());
                        if (carton > 0)
                        {
                            flagCarton = true;
                            double total = totalMeter / carton;
                            txtCarton.Text = Convert.ToInt16(total).ToString();
                            flagCarton = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void gridView1_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            try
            {
                row1 = gridView1.GetDataRow(gridView1.GetRowHandle(e.RowHandle));
                string v = row1["الكود"].ToString();
                txtCode.Text = v;
                
                txtTotalMeter.Text = "0";
                txtCarton.Text = "0";
                txtBalat.Text = "0";
                if (row1["متر/قطعة"].ToString() != "")
                {
                    txtTotalMeter.Text = row1["متر/قطعة"].ToString();
                }
                double carton = double.Parse(row1["Carton"].ToString());
                if (carton == 0)
                {
                    txtCarton.ReadOnly = true;
                    txtBalat.ReadOnly = true;
                }
                else
                {
                    txtCarton.ReadOnly = false;
                    txtBalat.ReadOnly = false;
                    if (row1["عدد الكراتين"].ToString() != "")
                    {
                        txtCarton.Text = row1["عدد الكراتين"].ToString();
                    }
                    if (row1["عدد البلتات"].ToString() != "")
                    {
                        txtBalat.Text = row1["عدد البلتات"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void search()
        {
            string qq = "select data.Data_ID,data.Code as 'الكود',concat(product.Product_Name,' - ',type.Type_Name,' - ',factory.Factory_Name,' - ',groupo.Group_Name,' ',COALESCE(color.Color_Name,''),' ',COALESCE(size.Size_Value,''),' ',COALESCE(sort.Sort_Value,'')) as 'الاسم',import_supplier_permission.Supplier_Permission_Number as 'اذن استلام',supplier_permission_details.Balatat as 'عدد البلتات',supplier_permission_details.Carton_Balata as 'عدد الكراتين',supplier_permission_details.Total_Meters as 'متر/قطعة',DATE_FORMAT(supplier_permission_details.Date, '%d-%m-%Y %T') as 'تاريخ التخزين',store_places.Store_Place_Code as 'مكان التخزين',supplier_permission_details.Note as 'ملاحظة',import_supplier_permission.Supplier_ID,supplier_permission_details.Supplier_Permission_Details_ID,supplier_permission_details.Store_Place_ID from supplier_permission_details INNER JOIN data ON supplier_permission_details.Data_ID = data.Data_ID INNER JOIN import_supplier_permission ON supplier_permission_details.ImportSupplierPermission_ID = import_supplier_permission.ImportSupplierPermission_ID INNER JOIN storage_import_permission ON storage_import_permission.StorageImportPermission_ID = import_supplier_permission.StorageImportPermission_ID INNER JOIN store_places ON store_places.Store_Place_ID = supplier_permission_details.Store_Place_ID INNER JOIN type ON type.Type_ID = data.Type_ID INNER JOIN product ON product.Product_ID = data.Product_ID INNER JOIN factory ON data.Factory_ID = factory.Factory_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID LEFT JOIN color ON color.Color_ID = data.Color_ID LEFT JOIN size ON size.Size_ID = data.Size_ID LEFT JOIN sort ON sort.Sort_ID = data.Sort_ID where storage_import_permission.Import_Permission_Number=" + txtPermissionNum.Text + " and supplier_permission_details.Store_ID=" + storeId;
            MySqlDataAdapter da = new MySqlDataAdapter(qq, dbconnection);
            DataTable dt = new DataTable();
            da.Fill(dt);
            gridControl1.DataSource = dt;
            gridView1.Columns["Data_ID"].Visible = false;
            gridView1.Columns["Supplier_ID"].Visible = false;
            gridView1.Columns["Supplier_Permission_Details_ID"].Visible = false;
            gridView1.Columns["Store_Place_ID"].Visible = false;

            qq = "select data.Data_ID,data.Code as 'الكود',concat(product.Product_Name,' - ',type.Type_Name,' - ',factory.Factory_Name,' - ',groupo.Group_Name,' ',COALESCE(color.Color_Name,''),' ',COALESCE(size.Size_Value,''),' ',COALESCE(sort.Sort_Value,'')) as 'الاسم',storage_return_supplier.Supplier_Permission_Number as 'اذن استلام',storage_return_details.Balatat as 'عدد البلتات',storage_return_details.Carton_Balata as 'عدد الكراتين',storage_return_details.Total_Meters as 'متر/قطعة',DATE_FORMAT(storage_return_details.Date, '%d-%m-%Y %T') as 'تاريخ التخزين',store_places.Store_Place_Code as 'مكان التخزين',storage_return_details.Reason as 'ملاحظة',storage_return_supplier.Supplier_ID,storage_return_details.StorageReturnDetails_ID,storage_return_details.Store_Place_ID from storage_return_details INNER JOIN data ON storage_return_details.Data_ID = data.Data_ID INNER JOIN storage_return_supplier ON storage_return_details.StorageReturnSupplier_ID = storage_return_supplier.StorageReturnSupplier_ID INNER JOIN storage_return ON storage_return.Storage_Return_ID = storage_return_supplier.Storage_Return_ID INNER JOIN store_places ON store_places.Store_Place_ID = storage_return_details.Store_Place_ID INNER JOIN type ON type.Type_ID = data.Type_ID INNER JOIN product ON product.Product_ID = data.Product_ID INNER JOIN factory ON data.Factory_ID = factory.Factory_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID LEFT JOIN color ON color.Color_ID = data.Color_ID LEFT JOIN size ON size.Size_ID = data.Size_ID LEFT JOIN sort ON sort.Sort_ID = data.Sort_ID where storage_return.Storage_Return_ID=" + txtPermissionNum.Text + " and storage_return_details.Store_ID=" + storeId;
            da = new MySqlDataAdapter(qq, dbconnection);
            dt = new DataTable();
            da.Fill(dt);
            gridControl2.DataSource = dt;
            gridView2.Columns["Data_ID"].Visible = false;
            gridView2.Columns["Supplier_ID"].Visible = false;
            gridView2.Columns["StorageReturnDetails_ID"].Visible = false;
            gridView2.Columns["Store_Place_ID"].Visible = false;
        }

        bool IsAdded(DataGridViewRow row1)
        {
            /*foreach (DataGridViewRow item in dataGridView2.Rows)
            {
                if ((row1.Cells["Data_ID"].Value.ToString() == item.Cells["Data_ID"].Value.ToString()) && (row1.Cells["الفئة"].Value.ToString() == item.Cells["Type"].Value.ToString()) && (row1.Cells["CustomerBill_ID"].Value.ToString() == item.Cells["CustomerBill_ID"].Value.ToString()))
                    return true;
            }*/
            return false;
        }
        
        //return quantity to store
        /*public void IncreaseProductQuantity(int billNumber)
        {
            connectionReader.Open();
            connectionReader2.Open();
            string q;
            int id;
            bool flag = false;
            double storageQ, productQ;
            string query = "select Data_ID,Type,TotalMeter from customer_return_bill_details where CustomerReturnBill_ID=" + billNumber;
            MySqlCommand com = new MySqlCommand(query, connectionReader);
            MySqlDataReader dr = com.ExecuteReader();

            while (dr.Read())
            {
                #region بند
                if (dr["Type"].ToString() == "بند")
                {
                    string query2 = "select Storage_ID,Total_Meters from storage where Data_ID=" + dr["Data_ID"].ToString() + " and Type='" + dr["Type"].ToString() + "'";
                    MySqlCommand com2 = new MySqlCommand(query2, connectionReader2);
                    MySqlDataReader dr2 = com2.ExecuteReader();
                    while (dr2.Read())
                    {

                        storageQ = Convert.ToDouble(dr2["Total_Meters"]);
                        productQ = Convert.ToDouble(dr["TotalMeter"]);

                        storageQ += productQ;
                        id = Convert.ToInt16(dr2["Storage_ID"]);
                        q = "update storage set Total_Meters=" + storageQ + " where Storage_ID=" + id;
                        MySqlCommand comm = new MySqlCommand(q, dbconnection);
                        comm.ExecuteNonQuery();
                        flag = true;
                        break;

                    }
                    dr2.Close();
                }
                #endregion

                #region طقم
                if (dr["Type"].ToString() == "طقم")
                {
                    string query2 = "select Storage_ID,Total_Meters from storage where Set_ID=" + dr["Data_ID"].ToString() + " and Type='" + dr["Type"].ToString() + "'";
                    MySqlCommand com2 = new MySqlCommand(query2, connectionReader2);
                    MySqlDataReader dr2 = com2.ExecuteReader();
                    while (dr2.Read())
                    {

                        storageQ = Convert.ToDouble(dr2["Total_Meters"]);
                        productQ = Convert.ToDouble(dr["TotalMeter"]);

                        storageQ += productQ;
                        id = Convert.ToInt16(dr2["Storage_ID"]);
                        q = "update storage set Total_Meters=" + storageQ + " where Storage_ID=" + id;
                        MySqlCommand comm = new MySqlCommand(q, dbconnection);
                        comm.ExecuteNonQuery();
                        flag = true;
                        break;

                    }
                    dr2.Close();
                }
                #endregion

                #region StorageTaxes
                string query3 = "select StorageTaxesID,Total_Meters from storage_taxes where Data_ID=" + dr["Data_ID"].ToString() + " and Type='" + dr["Type"].ToString() + "'";
                MySqlCommand com3 = new MySqlCommand(query3, connectionReader2);
                MySqlDataReader dr3 = com3.ExecuteReader();
                while (dr3.Read())
                {

                    storageQ = Convert.ToDouble(dr3["Total_Meters"]);
                    productQ = Convert.ToDouble(dr["TotalMeter"]);

                    storageQ += productQ;
                    id = Convert.ToInt16(dr3["StorageTaxesID"]);
                    q = "update storage_taxes set Total_Meters=" + storageQ + " where StorageTaxesID=" + id;
                    MySqlCommand comm = new MySqlCommand(q, dbconnection);
                    comm.ExecuteNonQuery();
                    flag = true;
                    break;

                }
                dr3.Close(); 
                #endregion

                if (!flag)
                {
                    MessageBox.Show(dr["Data_ID"].ToString() + "not valid in store");
                }
                flag = false;
            }
            dr.Close();
            
            connectionReader2.Close();
            connectionReader.Close();
        }*/
        
        private void btnReport_Click(object sender, EventArgs e)
        {
            try
            {
                if (comSupplier.SelectedValue != null && txtPermissionNum.Text != "" && gridView2.RowCount > 0)
                {
                    dbconnection.Open();
                    string query = "select Store_Name from store where Store_ID=" + storeId;
                    MySqlCommand com = new MySqlCommand(query, dbconnection);
                    string storeName = com.ExecuteScalar().ToString();
                    dbconnection.Close();

                    double carton = 0;
                    double balate = 0;
                    double quantity = 0;

                    List<SupplierReceipt_Items> bi = new List<SupplierReceipt_Items>();
                    for (int i = 0; i < gridView2.RowCount; i++)
                    {
                        if (gridView2.GetRowCellDisplayText(i, gridView2.Columns["عدد البلتات"]) != "")
                        {
                            balate = Convert.ToDouble(gridView2.GetRowCellDisplayText(i, gridView2.Columns["عدد البلتات"]));
                        }
                        if (gridView2.GetRowCellDisplayText(i, gridView2.Columns["عدد الكراتين"]) != "")
                        {
                            carton = Convert.ToDouble(gridView2.GetRowCellDisplayText(i, gridView2.Columns["عدد الكراتين"]));
                        }
                        if (gridView2.GetRowCellDisplayText(i, gridView2.Columns["متر/قطعة"]) != "")
                        {
                            quantity = Convert.ToDouble(gridView2.GetRowCellDisplayText(i, gridView2.Columns["متر/قطعة"]));
                        }

                        SupplierReceipt_Items item = new SupplierReceipt_Items() { Code = gridView2.GetRowCellDisplayText(i, gridView2.Columns["الكود"]), Product_Name = gridView2.GetRowCellDisplayText(i, gridView2.Columns["الاسم"]), Balatat = balate, Carton_Balata = carton, Total_Meters = quantity, Supplier_Permission_Number = Convert.ToInt16(gridView2.GetRowCellDisplayText(i, gridView2.Columns["اذن استلام"])), Date = Convert.ToDateTime(gridView2.GetRowCellDisplayText(i, gridView2.Columns["تاريخ التخزين"])).ToString("yyyy-MM-dd hh:mm:ss"), Note = gridView2.GetRowCellDisplayText(i, gridView2.Columns["ملاحظة"]) };
                        bi.Add(item);
                    }

                    Report_SupplierReceipt f = new Report_SupplierReceipt();
                    f.PrintInvoice(storeName, txtPermissionNum.Text, comSupplier.Text, bi);
                    f.ShowDialog();
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

        //clear all fields
        private void clrearAll()
        {
            try
            {
                txtReason.Text = "";

                txtCarton.Text = txtCode.Text = txtBalat.Text = "";

                /*dataGridView1.DataSource = null;
                dataGridView2.Rows.Clear();*/
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void clear(Control tlp)
        {
            foreach (Control co in tlp.Controls)
            {
                if (co is Panel || co is TableLayoutPanel)
                {
                    foreach (Control item in co.Controls)
                    {
                        if (item is System.Windows.Forms.ComboBox)
                        {
                            item.Text = "";
                        }
                        else if (item is TextBox)
                        {
                            item.Text = "";
                        }
                    }
                }
            }
        }
    }
}

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
        MySqlConnection dbconnection, dbconnection2, dbconnection3;
        
        bool flag = false;
        int storeId = 0;
        bool flagCarton = false;
        DataRow row1 = null;
        bool loaded = false;
        int storageReturnSupplierId = 0;
        int ReturnedPermissionNumber = 1;

        public StorageReturnBill(MainForm SalesMainForm, DevExpress.XtraTab.XtraTabControl TabControlStores)
        {
            InitializeComponent();
            dbconnection = new MySqlConnection(connection.connectionString);
            dbconnection2 = new MySqlConnection(connection.connectionString);
            dbconnection3 = new MySqlConnection(connection.connectionString);
            comSupplier.AutoCompleteMode = AutoCompleteMode.Suggest;
            comSupplier.AutoCompleteSource = AutoCompleteSource.ListItems;
        }

        private void StorageReturnBill_Load(object sender, EventArgs e)
        {
            try
            {
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
                if (row1 != null && comSupplier.SelectedValue != null && txtPermissionNum.Text != "" && txtCarton.Text != "" && txtBalat.Text != "" && txtCode.Text != "" && txtTotalMeter.Text != "" && txtReason.Text != "")
                {
                    int NoBalatat = 0;
                    int NoCartons = 0;
                    int permNum = 0;
                    double total = 0;
                    if (int.TryParse(txtBalat.Text, out NoBalatat) && int.TryParse(txtCarton.Text, out NoCartons) && int.TryParse(txtPermissionNum.Text, out permNum) && double.TryParse(txtTotalMeter.Text, out total))
                    {
                        double carton = Convert.ToDouble(row1["Carton"].ToString());

                        dbconnection.Open();
                        dbconnection2.Open();
                        int storageReturnID = 0;
                        int storageImportPermissionID = 0;

                        string q = "select StorageImportPermission_ID from storage_import_permission where Store_ID=" + storeId + " and Import_Permission_Number=" + permNum;
                        MySqlCommand com2 = new MySqlCommand(q, dbconnection);
                        if (com2.ExecuteScalar() != null)
                        {
                            storageImportPermissionID = int.Parse(com2.ExecuteScalar().ToString());

                            if(IsAdded())
                            {
                                MessageBox.Show("هذا العنصر تم اضافتة من قبل");
                                dbconnection.Close();
                                dbconnection2.Close();
                                return;
                            }

                            if (row1["عدد البلتات"].ToString() != "")
                            {
                                if (NoBalatat > Convert.ToInt16(row1["عدد البلتات"].ToString()))
                                {
                                    MessageBox.Show("تاكد من عدد البلتات");
                                    dbconnection.Close();
                                    dbconnection2.Close();
                                    return;
                                }
                            }
                            if (row1["عدد الكراتين"].ToString() != "")
                            {
                                if (NoCartons > Convert.ToInt16(row1["عدد الكراتين"].ToString()))
                                {
                                    MessageBox.Show("تاكد من عدد الكراتين");
                                    dbconnection.Close();
                                    dbconnection2.Close();
                                    return;
                                }
                            }
                            if (row1["متر/قطعة"].ToString() != "")
                            {
                                if (total > Convert.ToDouble(row1["متر/قطعة"].ToString()))
                                {
                                    MessageBox.Show("تاكد من متر/قطعة");
                                    dbconnection.Close();
                                    dbconnection2.Close();
                                    return;
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show("هذا الاذن غير موجود");
                            dbconnection.Close();
                            dbconnection2.Close();
                            return;
                        }

                        string query = "select ImportStorageReturn_ID from import_storage_return where Import_Permission_Number=" + permNum + " and Store_ID=" + storeId;
                        MySqlCommand com = new MySqlCommand(query, dbconnection);
                        if (com.ExecuteScalar() == null)
                        {
                            /*string qq = "select Returned_Permission_Number from import_storage_return where Store_ID=" + storeId + " ORDER BY ImportStorageReturn_ID DESC LIMIT 1";
                            MySqlCommand com3 = new MySqlCommand(qq, dbconnection);
                            ReturnedPermissionNumber = 1;
                            if (com3.ExecuteScalar() != null)
                            {
                                int r = int.Parse(com3.ExecuteScalar().ToString());
                                ReturnedPermissionNumber = r + 1;
                            }*/

                            query = "insert into import_storage_return (Store_ID,Returned_Permission_Number,Retrieval_Date,Import_Permission_Number,StorageImportPermission_ID) values (@Store_ID,@Returned_Permission_Number,@Retrieval_Date,@Import_Permission_Number,@StorageImportPermission_ID)";
                            com = new MySqlCommand(query, dbconnection);
                            com.Parameters.Add("@Store_ID", MySqlDbType.Int16);
                            com.Parameters["@Store_ID"].Value = storeId;
                            com.Parameters.Add("@Returned_Permission_Number", MySqlDbType.Int16);
                            com.Parameters["@Returned_Permission_Number"].Value = ReturnedPermissionNumber;
                            com.Parameters.Add("@Retrieval_Date", MySqlDbType.DateTime, 0);
                            com.Parameters["@Retrieval_Date"].Value = DateTime.Now;
                            com.Parameters.Add("@Import_Permission_Number", MySqlDbType.Int16);
                            com.Parameters["@Import_Permission_Number"].Value = permNum;
                            com.Parameters.Add("@StorageImportPermission_ID", MySqlDbType.Int16);
                            com.Parameters["@StorageImportPermission_ID"].Value = storageImportPermissionID;
                            com.ExecuteNonQuery();

                            query = "select ImportStorageReturn_ID from import_storage_return order by ImportStorageReturn_ID desc limit 1";
                            com = new MySqlCommand(query, dbconnection);
                            storageReturnID = Convert.ToInt16(com.ExecuteScalar().ToString());
                            
                            UserControl.ItemRecord("import_storage_return", "اضافة", storageReturnID, DateTime.Now, "", dbconnection);

                            query = "SELECT gate.Car_ID,gate.Car_Number,gate.Driver_ID,gate.Driver_Name FROM gate INNER JOIN gate_permission ON gate_permission.Permission_Number = gate.Permission_Number where gate.Store_ID=" + storeId + " and gate.Supplier_ID=" + comSupplier.SelectedValue.ToString() + " and gate_permission.Supplier_PermissionNumber=" + row1["اذن استلام"] + " and gate_permission.Type='دخول'";
                            com = new MySqlCommand(query, dbconnection2);
                            MySqlDataReader dr2 = com.ExecuteReader();
                            if (dr2.HasRows)
                            {
                                while (dr2.Read())
                                {
                                    query = "insert into import_storage_return_supplier (Supplier_ID,Supplier_Permission_Number,Car_ID,Car_Number,Driver_ID,Driver_Name,StorageImportPermission_ID) values (@Supplier_ID,@Supplier_Permission_Number,@Car_ID,@Car_Number,@Driver_ID,@Driver_Name,@StorageImportPermission_ID)";
                                    com = new MySqlCommand(query, dbconnection);
                                    com.Parameters.Add("@Supplier_ID", MySqlDbType.Int16);
                                    com.Parameters["@Supplier_ID"].Value = Convert.ToInt16(comSupplier.SelectedValue.ToString());
                                    com.Parameters.Add("@Supplier_Permission_Number", MySqlDbType.Int16);
                                    com.Parameters["@Supplier_Permission_Number"].Value = row1["اذن استلام"].ToString();
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
                                    com.Parameters.Add("@ImportStorageReturn_ID", MySqlDbType.Int16);
                                    com.Parameters["@ImportStorageReturn_ID"].Value = storageReturnID;
                                    com.ExecuteNonQuery();

                                    query = "select ImportStorageReturnSupplier_ID from import_storage_return_supplier order by ImportStorageReturnSupplier_ID desc limit 1";
                                    com = new MySqlCommand(query, dbconnection);
                                    storageReturnSupplierId = Convert.ToInt16(com.ExecuteScalar().ToString());
                                }
                                dr2.Close();
                            }
                            else
                            {
                                storageReturnSupplierId = 0;
                            }
                        }
                        else
                        {
                            storageReturnID = Convert.ToInt16(com.ExecuteScalar().ToString());
                            query = "select ImportStorageReturnSupplier_ID from import_storage_return_supplier where ImportStorageReturn_ID=" + storageReturnID + " and Supplier_ID=" + comSupplier.SelectedValue.ToString() + " and Supplier_Permission_Number=" + row1["اذن استلام"].ToString();
                            com = new MySqlCommand(query, dbconnection);
                            if (com.ExecuteScalar() == null)
                            {
                                query = "SELECT gate.Car_ID,gate.Car_Number,gate.Driver_ID,gate.Driver_Name FROM gate INNER JOIN gate_permission ON gate_permission.Permission_Number = gate.Permission_Number where gate.Store_ID=" + storeId + " and gate.Supplier_ID=" + comSupplier.SelectedValue.ToString() + " and gate_permission.Supplier_PermissionNumber=" + row1["اذن استلام"].ToString() + " and gate_permission.Type='دخول'";
                                com = new MySqlCommand(query, dbconnection2);
                                MySqlDataReader dr2 = com.ExecuteReader();
                                if (dr2.HasRows)
                                {
                                    while (dr2.Read())
                                    {
                                        query = "insert into import_storage_return_supplier (Supplier_ID,Supplier_Permission_Number,Car_ID,Car_Number,Driver_ID,Driver_Name,ImportStorageReturn_ID) values (@Supplier_ID,@Supplier_Permission_Number,@Car_ID,@Car_Number,@Driver_ID,@Driver_Name,@ImportStorageReturn_ID)";
                                        com = new MySqlCommand(query, dbconnection);
                                        com.Parameters.Add("@Supplier_ID", MySqlDbType.Int16);
                                        com.Parameters["@Supplier_ID"].Value = comSupplier.SelectedValue.ToString();
                                        com.Parameters.Add("@Supplier_Permission_Number", MySqlDbType.Int16);
                                        com.Parameters["@Supplier_Permission_Number"].Value = row1["اذن استلام"].ToString();
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
                                        com.Parameters.Add("@ImportStorageReturn_ID", MySqlDbType.Int16);
                                        com.Parameters["@ImportStorageReturn_ID"].Value = storageReturnID;
                                        com.ExecuteNonQuery();

                                        query = "select ImportStorageReturnSupplier_ID from import_storage_return_supplier order by ImportStorageReturnSupplier_ID desc limit 1";
                                        com = new MySqlCommand(query, dbconnection);
                                        storageReturnSupplierId = Convert.ToInt16(com.ExecuteScalar().ToString());
                                    }
                                    dr2.Close();
                                }
                                else
                                {
                                    storageReturnSupplierId = 0;
                                }
                            }
                            else
                            {
                                storageReturnSupplierId = Convert.ToInt16(com.ExecuteScalar().ToString());
                            }
                        }

                        if (storageReturnSupplierId > 0)
                        {
                            query = "insert into import_storage_return_details (Store_ID,Store_Place_ID,Date,Data_ID,Balatat,Carton_Balata,Total_Meters,Reason,ImportStorageReturnSupplier_ID,Employee_ID) values (@Store_ID,@Store_Place_ID,@Date,@Data_ID,@Balatat,@Carton_Balata,@Total_Meters,@Reason,@ImportStorageReturnSupplier_ID,@Employee_ID)";
                            com = new MySqlCommand(query, dbconnection);
                            com.Parameters.Add("@Store_ID", MySqlDbType.Int16);
                            com.Parameters["@Store_ID"].Value = storeId;
                            com.Parameters.Add("@Store_Place_ID", MySqlDbType.Int16);
                            com.Parameters["@Store_Place_ID"].Value = row1["Store_Place_ID"].ToString();
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
                            com.Parameters.Add("@Reason", MySqlDbType.VarChar);
                            com.Parameters["@Reason"].Value = txtReason.Text;
                            com.Parameters.Add("@ImportStorageReturnSupplier_ID", MySqlDbType.Int16);
                            com.Parameters["@ImportStorageReturnSupplier_ID"].Value = storageReturnSupplierId;
                            com.Parameters.Add("@Employee_ID", MySqlDbType.Int16);
                            com.Parameters["@Employee_ID"].Value = UserControl.EmpID;
                            com.ExecuteNonQuery();
                        }
                        else
                        {
                            MessageBox.Show("برجاء التاكد من رقم اذن الاستلام");
                            dbconnection.Close();
                            dbconnection2.Close();
                            return;
                        }

                        query = "select Total_Meters from storage where Data_ID=" + row1["Data_ID"].ToString() + " and Store_ID=" + storeId + " and Store_Place_ID=" + row1["Store_Place_ID"].ToString();
                        com = new MySqlCommand(query, dbconnection);
                        if (com.ExecuteScalar() != null)
                        {
                            double totalMeter = Convert.ToDouble(com.ExecuteScalar().ToString());

                            query = "update storage set Total_Meters=" + (totalMeter - total) + " where Data_ID=" + row1["Data_ID"].ToString() + " and Store_ID=" + storeId + " and Store_Place_ID=" + row1["Store_Place_ID"].ToString();
                            com = new MySqlCommand(query, dbconnection);
                            com.ExecuteNonQuery();
                        }

                        comSupplier.Enabled = false;
                        comSupplier.DropDownStyle = ComboBoxStyle.DropDownList;
                        dbconnection.Close();
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
            dbconnection3.Close();
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
                            
                            query = "delete from import_storage_return_details where ImportStorageReturnDetails_ID=" + row2["ImportStorageReturnDetails_ID"].ToString();
                            com = new MySqlCommand(query, dbconnection);
                            com.ExecuteNonQuery();

                            query = "update storage set Total_Meters=" + (totalf + Convert.ToDouble(row2["متر/قطعة"].ToString())) + " where Store_ID=" + storeId + " and Store_Place_ID=" + row2["Store_Place_ID"].ToString() + " and Data_ID=" + row2["Data_ID"].ToString();
                            com = new MySqlCommand(query, dbconnection);
                            com.ExecuteNonQuery();
                            dbconnection.Close();

                            search();
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
            dbconnection3.Close();
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
                    string qq = "select Returned_Permission_Number from import_storage_return where Store_ID=" + storeId + " and Import_Permission_Number=" + txtPermissionNum.Text;
                    MySqlCommand com3 = new MySqlCommand(qq, dbconnection);
                    ReturnedPermissionNumber = 1;
                    if (com3.ExecuteScalar() != null)
                    {
                        ReturnedPermissionNumber = int.Parse(com3.ExecuteScalar().ToString());
                    }
                    else
                    {
                        qq = "select Returned_Permission_Number from import_storage_return where Store_ID=" + storeId + " ORDER BY ImportStorageReturn_ID DESC LIMIT 1";
                        com3 = new MySqlCommand(qq, dbconnection);
                        if (com3.ExecuteScalar() != null)
                        {
                            int r = int.Parse(com3.ExecuteScalar().ToString());
                            ReturnedPermissionNumber = r + 1;
                        }
                    }

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

                    txtCode.Text = "";
                    txtBalat.Text = "0";
                    txtCarton.Text = "0";
                    txtTotalMeter.Text = "0";
                    txtReason.Text = "";
                    loaded = true;
                }
                else
                {
                    gridControl2.DataSource = null;

                    loaded = false;
                    comSupplier.SelectedIndex = -1;
                    txtCode.Text = "";
                    txtBalat.Text = "0";
                    txtCarton.Text = "0";
                    txtTotalMeter.Text = "0";
                    txtReason.Text = "";
                    loaded = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
            dbconnection3.Close();
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
            string qq = "select data.Data_ID,data.Code as 'الكود',concat(product.Product_Name,' - ',type.Type_Name,' - ',factory.Factory_Name,' - ',groupo.Group_Name,' ',COALESCE(color.Color_Name,''),' ',COALESCE(size.Size_Value,''),' ',COALESCE(sort.Sort_Value,'')) as 'الاسم',import_supplier_permission.Supplier_Permission_Number as 'اذن استلام',(supplier_permission_details.Balatat-ifnull(import_storage_return_details.Balatat,0)) as 'عدد البلتات',(supplier_permission_details.Carton_Balata-ifnull(import_storage_return_details.Carton_Balata,0)) as 'عدد الكراتين',(supplier_permission_details.Total_Meters-ifnull(import_storage_return_details.Total_Meters,0)) as 'متر/قطعة',DATE_FORMAT(supplier_permission_details.Date, '%d-%m-%Y %T') as 'تاريخ التخزين',store_places.Store_Place_Code as 'مكان التخزين',supplier_permission_details.Note as 'ملاحظة',data.Carton,import_supplier_permission.Supplier_ID,supplier_permission_details.Supplier_Permission_Details_ID,supplier_permission_details.Store_Place_ID,storage_import_permission.StorageImportPermission_ID from supplier_permission_details INNER JOIN data ON supplier_permission_details.Data_ID = data.Data_ID INNER JOIN import_supplier_permission ON supplier_permission_details.ImportSupplierPermission_ID = import_supplier_permission.ImportSupplierPermission_ID INNER JOIN storage_import_permission ON storage_import_permission.StorageImportPermission_ID = import_supplier_permission.StorageImportPermission_ID LEFT JOIN import_storage_return ON import_storage_return.StorageImportPermission_ID = storage_import_permission.StorageImportPermission_ID LEFT JOIN import_storage_return_supplier ON import_storage_return_supplier.ImportStorageReturn_ID = import_storage_return.ImportStorageReturn_ID LEFT JOIN import_storage_return_details ON import_storage_return_details.ImportStorageReturnSupplier_ID = import_storage_return_supplier.ImportStorageReturnSupplier_ID and supplier_permission_details.Data_ID = import_storage_return_details.Data_ID INNER JOIN store_places ON store_places.Store_Place_ID = supplier_permission_details.Store_Place_ID INNER JOIN type ON type.Type_ID = data.Type_ID INNER JOIN product ON product.Product_ID = data.Product_ID INNER JOIN factory ON data.Factory_ID = factory.Factory_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID LEFT JOIN color ON color.Color_ID = data.Color_ID LEFT JOIN size ON size.Size_ID = data.Size_ID LEFT JOIN sort ON sort.Sort_ID = data.Sort_ID where storage_import_permission.Import_Permission_Number=0 and supplier_permission_details.Store_ID=0";
            MySqlDataAdapter da = new MySqlDataAdapter(qq, dbconnection);
            DataTable dt = new DataTable();
            da.Fill(dt);
            gridControl1.DataSource = dt;
            
            dbconnection.Open();
            
            qq = "select data.Data_ID,data.Code as 'الكود',concat(product.Product_Name,' - ',type.Type_Name,' - ',factory.Factory_Name,' - ',groupo.Group_Name,' ',COALESCE(color.Color_Name,''),' ',COALESCE(size.Size_Value,''),' ',COALESCE(sort.Sort_Value,'')) as 'الاسم',import_supplier_permission.Supplier_Permission_Number as 'اذن استلام',supplier_permission_details.Balatat as 'عدد البلتات',supplier_permission_details.Carton_Balata as 'عدد الكراتين',supplier_permission_details.Total_Meters as 'متر/قطعة',DATE_FORMAT(supplier_permission_details.Date, '%d-%m-%Y %T') as 'تاريخ التخزين',store_places.Store_Place_Code as 'مكان التخزين',supplier_permission_details.Note as 'ملاحظة',data.Carton,import_supplier_permission.Supplier_ID,supplier_permission_details.Supplier_Permission_Details_ID,supplier_permission_details.Store_Place_ID,storage_import_permission.StorageImportPermission_ID from supplier_permission_details INNER JOIN data ON supplier_permission_details.Data_ID = data.Data_ID INNER JOIN import_supplier_permission ON supplier_permission_details.ImportSupplierPermission_ID = import_supplier_permission.ImportSupplierPermission_ID INNER JOIN storage_import_permission ON storage_import_permission.StorageImportPermission_ID = import_supplier_permission.StorageImportPermission_ID INNER JOIN store_places ON store_places.Store_Place_ID = supplier_permission_details.Store_Place_ID INNER JOIN type ON type.Type_ID = data.Type_ID INNER JOIN product ON product.Product_ID = data.Product_ID INNER JOIN factory ON data.Factory_ID = factory.Factory_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID LEFT JOIN color ON color.Color_ID = data.Color_ID LEFT JOIN size ON size.Size_ID = data.Size_ID LEFT JOIN sort ON sort.Sort_ID = data.Sort_ID where storage_import_permission.Import_Permission_Number=" + txtPermissionNum.Text + " and supplier_permission_details.Store_ID=" + storeId;
            MySqlCommand comand = new MySqlCommand(qq, dbconnection);
            MySqlDataReader dr = comand.ExecuteReader();
            while (dr.Read())
            {
                int noBalat = 0;
                int noCarton = 0;
                double totalMeter = 0;
                gridView1.AddNewRow();
                int rowHandle = gridView1.GetRowHandle(gridView1.DataRowCount);
                if (gridView1.IsNewItemRow(rowHandle))
                {
                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns[0], dr["Data_ID"].ToString());
                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns["الكود"], dr["الكود"].ToString());
                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns["الاسم"], dr["الاسم"].ToString());
                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns["اذن استلام"], dr["اذن استلام"].ToString());
                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns["تاريخ التخزين"], dr["تاريخ التخزين"].ToString());
                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns["مكان التخزين"], dr["مكان التخزين"].ToString());
                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns["ملاحظة"], dr["ملاحظة"].ToString());
                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns["Carton"], dr["Carton"].ToString());
                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns["Supplier_ID"], dr["Supplier_ID"].ToString());
                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns["Supplier_Permission_Details_ID"], dr["Supplier_Permission_Details_ID"].ToString());
                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns["Store_Place_ID"], dr["Store_Place_ID"].ToString());
                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns["StorageImportPermission_ID"], dr["StorageImportPermission_ID"].ToString());
                    if (dr["عدد البلتات"].ToString() != "")
                    {
                        noBalat = Convert.ToInt16(dr["عدد البلتات"].ToString());
                    }
                    if (dr["عدد الكراتين"].ToString() != "")
                    {
                        noCarton = Convert.ToInt16(dr["عدد الكراتين"].ToString());
                    }
                    if (dr["متر/قطعة"].ToString() != "")
                    {
                        totalMeter = Convert.ToDouble(dr["متر/قطعة"].ToString());
                    }

                    dbconnection3.Open();
                    string q = "select import_storage_return_details.Balatat as 'عدد البلتات',import_storage_return_details.Carton_Balata as 'عدد الكراتين',import_storage_return_details.Total_Meters as 'متر/قطعة' from import_storage_return inner JOIN import_storage_return_supplier ON import_storage_return_supplier.ImportStorageReturn_ID = import_storage_return.ImportStorageReturn_ID LEFT JOIN import_storage_return_details ON import_storage_return_details.ImportStorageReturnSupplier_ID = import_storage_return_supplier.ImportStorageReturnSupplier_ID  where import_storage_return.Import_Permission_Number=" + txtPermissionNum.Text + " and import_storage_return.Store_ID=" + storeId + " and import_storage_return_details.Data_ID=" + dr[0].ToString() + " and import_storage_return_supplier.Supplier_Permission_Number=" + dr["اذن استلام"].ToString() + " and import_storage_return_supplier.Supplier_ID=" + dr["Supplier_ID"].ToString();
                    MySqlCommand comand2 = new MySqlCommand(q, dbconnection3);
                    MySqlDataReader dr2 = comand2.ExecuteReader();
                    while (dr2.Read())
                    {
                        if (dr2["عدد البلتات"].ToString() != "")
                        {
                            noBalat -= Convert.ToInt16(dr2["عدد البلتات"].ToString());
                        }
                        if (dr2["عدد الكراتين"].ToString() != "")
                        {
                            noCarton -= Convert.ToInt16(dr2["عدد الكراتين"].ToString());
                        }
                        if (dr2["متر/قطعة"].ToString() != "")
                        {
                            totalMeter -= Convert.ToDouble(dr2["متر/قطعة"].ToString());
                        }
                    }
                    dr2.Close();
                    dbconnection3.Close();
                    if (noBalat > 0)
                    {
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns["عدد البلتات"], noBalat);
                    }
                    if (noCarton > 0)
                    {
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns["عدد الكراتين"], noCarton);
                    }
                    if (totalMeter > 0)
                    {
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns["متر/قطعة"], totalMeter);
                    }
                }
            }
            dr.Close();
            gridView1.Columns["Data_ID"].Visible = false;
            gridView1.Columns["Supplier_ID"].Visible = false;
            gridView1.Columns["Carton"].Visible = false;
            gridView1.Columns["Supplier_Permission_Details_ID"].Visible = false;
            gridView1.Columns["Store_Place_ID"].Visible = false;
            gridView1.Columns["StorageImportPermission_ID"].Visible = false;
            if (gridView1.IsLastVisibleRow)
            {
                gridView1.FocusedRowHandle = gridView1.RowCount - 1;
            }
            
            qq = "select data.Data_ID,data.Code as 'الكود',concat(product.Product_Name,' - ',type.Type_Name,' - ',factory.Factory_Name,' - ',groupo.Group_Name,' ',COALESCE(color.Color_Name,''),' ',COALESCE(size.Size_Value,''),' ',COALESCE(sort.Sort_Value,'')) as 'الاسم',import_storage_return_supplier.Supplier_Permission_Number as 'اذن استلام',import_storage_return_details.Balatat as 'عدد البلتات',import_storage_return_details.Carton_Balata as 'عدد الكراتين',import_storage_return_details.Total_Meters as 'متر/قطعة',DATE_FORMAT(import_storage_return_details.Date, '%d-%m-%Y %T') as 'التاريخ',store_places.Store_Place_Code as 'المكان',import_storage_return_details.Reason as 'السبب',import_storage_return_supplier.Supplier_ID,import_storage_return_details.ImportStorageReturnDetails_ID,import_storage_return_details.Store_Place_ID from import_storage_return_details INNER JOIN data ON import_storage_return_details.Data_ID = data.Data_ID INNER JOIN import_storage_return_supplier ON import_storage_return_details.ImportStorageReturnSupplier_ID = import_storage_return_supplier.ImportStorageReturnSupplier_ID INNER JOIN import_storage_return ON import_storage_return.ImportStorageReturn_ID = import_storage_return_supplier.ImportStorageReturn_ID INNER JOIN store_places ON store_places.Store_Place_ID = import_storage_return_details.Store_Place_ID INNER JOIN type ON type.Type_ID = data.Type_ID INNER JOIN product ON product.Product_ID = data.Product_ID INNER JOIN factory ON data.Factory_ID = factory.Factory_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID LEFT JOIN color ON color.Color_ID = data.Color_ID LEFT JOIN size ON size.Size_ID = data.Size_ID LEFT JOIN sort ON sort.Sort_ID = data.Sort_ID where import_storage_return.Import_Permission_Number=" + txtPermissionNum.Text + " and import_storage_return_details.Store_ID=" + storeId;
            da = new MySqlDataAdapter(qq, dbconnection);
            dt = new DataTable();
            da.Fill(dt);
            gridControl2.DataSource = dt;
            gridView2.Columns["Data_ID"].Visible = false;
            gridView2.Columns["Supplier_ID"].Visible = false;
            gridView2.Columns["ImportStorageReturnDetails_ID"].Visible = false;
            gridView2.Columns["Store_Place_ID"].Visible = false;

            if (gridView2.IsLastVisibleRow)
            {
                gridView2.FocusedRowHandle = gridView2.RowCount - 1;
            }
            dbconnection.Close();
        }
        
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

                    List<StorageReturn_Items> bi = new List<StorageReturn_Items>();
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

                        StorageReturn_Items item = new StorageReturn_Items() { Code = gridView2.GetRowCellDisplayText(i, gridView2.Columns["الكود"]), Product_Name = gridView2.GetRowCellDisplayText(i, gridView2.Columns["الاسم"]), Balatat = balate, Carton_Balata = carton, Total_Meters = quantity, Supplier_Permission_Number = Convert.ToInt16(gridView2.GetRowCellDisplayText(i, gridView2.Columns["اذن استلام"])), Date = Convert.ToDateTime(gridView2.GetRowCellDisplayText(i, gridView2.Columns["التاريخ"])).ToString("yyyy-MM-dd hh:mm:ss"), Reason = gridView2.GetRowCellDisplayText(i, gridView2.Columns["السبب"]) };
                        bi.Add(item);
                    }

                    Report_StorageReturn f = new Report_StorageReturn();
                    f.PrintInvoice(storeName, txtPermissionNum.Text, comSupplier.Text, ReturnedPermissionNumber, bi);
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

        bool IsAdded()
        {
            for (int i = 0; i < gridView2.RowCount; i++)
            {
                DataRow row3 = gridView2.GetDataRow(i);
                if ((row1["Data_ID"].ToString() == row3["Data_ID"].ToString()) && (row1["اذن استلام"].ToString() == row3["اذن استلام"].ToString()))
                    return true;
            }
            return false;
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

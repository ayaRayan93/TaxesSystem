using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using MySql.Data.MySqlClient;
using DevExpress.XtraTab;
using DevExpress.XtraGrid.Views.Grid;

namespace TaxesSystem
{
    public partial class StorageReturnQuantity_Update : DevExpress.XtraEditors.XtraForm
    {
        int rowHandel = 0;
        DataRowView selRow;
        MySqlConnection dbconnection;
        int storeID = 0;
        //string formName = "";
        StorageReturnBill_Update storageReturnBillUpdate = null;
        string permissionNum = "";
        double rowQuantity = 0;
        PermissionReturnedReport permissionReturnedReport = null;

        public StorageReturnQuantity_Update(int rowhandel, DataRowView Selrow, int storeId, PermissionReturnedReport PermissionsReport, StorageReturnBill_Update StorageReturnBillUpdate/*, string FormName, object Form*/, string PermissionNum)
        {
            InitializeComponent();
            dbconnection = new MySqlConnection(connection.connectionString);
            rowHandel = rowhandel;
            selRow = Selrow;
            storeID = storeId;
            //formName = FormName;
            storageReturnBillUpdate = StorageReturnBillUpdate;
            permissionNum = PermissionNum;
            permissionReturnedReport = PermissionsReport;
        }

        private void QuantityUpdate_Load(object sender, EventArgs e)
        {
            try
            {
                if (storageReturnBillUpdate != null)
                {
                    rowQuantity = Convert.ToDouble(selRow["TotalQuantity"].ToString());
                    txtQuantity.Text = selRow["TotalQuantity"].ToString();
                }
                else/* if (formName == "PermissionReturnedReport")*/
                {
                    rowQuantity = Convert.ToDouble(selRow["متر/قطعة"].ToString());
                    txtQuantity.Text = selRow["متر/قطعة"].ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (txtQuantity.Text != "")
            {
                double quantity = 0;
                if (double.TryParse(txtQuantity.Text, out quantity))
                {
                    try
                    {
                        dbconnection.Open();
                        string query = "select Total_Meters from storage where Store_ID=" + storeID + " and Data_ID=" + selRow["Data_ID"].ToString();
                        MySqlCommand com = new MySqlCommand(query, dbconnection);
                        if (com.ExecuteScalar() != null)
                        {
                            double totalf = Convert.ToInt32(com.ExecuteScalar());
                            totalf = (totalf + rowQuantity);

                            if ((totalf - quantity) >= 0)
                            {
                                query = "update storage set Total_Meters=" + (totalf - quantity) + " where Store_ID=" + storeID + " and Data_ID=" + selRow["Data_ID"].ToString();
                                com = new MySqlCommand(query, dbconnection);
                                com.ExecuteNonQuery();

                                UserControl.ItemRecord("storage", "تعديل", Convert.ToInt16(selRow["Data_ID"]), DateTime.Now, "تعديل مرتجع اذن مخزن", dbconnection);

                                query = "update import_storage_return_details set Total_Meters=" + quantity + " where ImportStorageReturnDetails_ID=" + selRow["ImportStorageReturnDetails_ID"].ToString();
                                com = new MySqlCommand(query, dbconnection);
                                com.ExecuteNonQuery();

                                dbconnection.Close();
                                if (storageReturnBillUpdate == null)
                                {
                                    permissionReturnedReport.refreshView(rowHandel, Convert.ToDouble(txtQuantity.Text));
                                }
                                else /*if (formName == "StorageReturnBill_Update")*/
                                {
                                    storageReturnBillUpdate.refreshView(rowHandel, Convert.ToDouble(txtQuantity.Text));
                                    
                                    permissionReturnedReport.refreshView(rowHandel, Convert.ToDouble(txtQuantity.Text));
                                }
                                this.Close();
                            }
                            else
                            {
                                MessageBox.Show("لا يوجد كمية كافية");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    dbconnection.Close();
                }
                else
                {
                    MessageBox.Show("الكمية يجب ان تكون عدد");
                }
            }
            else
            {
                MessageBox.Show("يجب ادخال الكمية");
            }
        }
    }
}
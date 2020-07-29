﻿using System;
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

namespace TaxesSystem
{
    public partial class SupplierReceiptQuantity_Update : DevExpress.XtraEditors.XtraForm
    {
        int rowHandel = 0;
        DataRowView selRow;
        MySqlConnection dbconnection, dbconnection2;
        int storeID = 0;
        //string FormName = "";
        //object form;
        PermissionsReport permissionsReport = null;
        SupplierReceiptUpdate supplierReceiptUpdate = null;

        public SupplierReceiptQuantity_Update(int rowhandel, DataRowView Selrow, int storeId, PermissionsReport PermissionsReport, SupplierReceiptUpdate SupplierReceiptUpdate/*, string formName, object Form*/)
        {
            InitializeComponent();
            dbconnection = new MySqlConnection(connection.connectionString);
            dbconnection2 = new MySqlConnection(connection.connectionString);
            rowHandel = rowhandel;
            selRow = Selrow;
            storeID = storeId;
            //FormName = formName;
            permissionsReport = PermissionsReport;
            supplierReceiptUpdate = SupplierReceiptUpdate;
        }

        private void QuantityUpdate_Load(object sender, EventArgs e)
        {
            try
            {
                txtQuantity.Text = selRow["متر/قطعة"].ToString();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (txtQuantity.Text != "")
            {
                double quantity = 0;
                int CartonNum = 0;
                if (double.TryParse(txtQuantity.Text, out quantity))
                {
                    try
                    {
                        dbconnection.Open();

                        string query = "select Total_Meters from storage where Store_ID=" + storeID + " and Data_ID=" + selRow["Data_ID"].ToString();
                        MySqlCommand com = new MySqlCommand(query, dbconnection);
                        if (com.ExecuteScalar() != null)
                        {
                            double totalf = Convert.ToDouble(com.ExecuteScalar());

                            totalf = (totalf - Convert.ToDouble(selRow["متر/قطعة"].ToString()));

                            if ((totalf + quantity) >= 0)
                            {
                                string q = "select Carton from data where Data_ID=" + selRow["Data_ID"].ToString();
                                com = new MySqlCommand(q, dbconnection);

                                double carton = double.Parse(com.ExecuteScalar().ToString());
                                if (carton > 0)
                                {
                                    double total = quantity / carton;
                                    CartonNum = Convert.ToInt32(total);
                                }

                                query = "update supplier_permission_details set Total_Meters=" + quantity + " , Carton_Balata=" + CartonNum + " where Supplier_Permission_Details_ID=" + selRow["Supplier_Permission_Details_ID"].ToString();
                                com = new MySqlCommand(query, dbconnection);
                                com.ExecuteNonQuery();

                                query = "update storage set Total_Meters=" + (totalf + quantity) + " where Store_ID=" + storeID + " and Data_ID=" + selRow["Data_ID"].ToString();
                                com = new MySqlCommand(query, dbconnection);
                                com.ExecuteNonQuery();

                                UserControl.ItemRecord("storage", "تعديل", Convert.ToInt16(selRow["Data_ID"].ToString()), DateTime.Now, "اذن مخزن", dbconnection);


                                #region MyRegion
                                if (selRow["Factory_ID"].ToString() != "" && selRow["رقم الطلب"].ToString() != "")
                                {
                                    dbconnection2.Open();
                                    query = "select order_details.OrderDetails_ID,order_details.Quantity from orders INNER JOIN order_details ON order_details.Order_ID = orders.Order_ID where order_details.Data_ID=" + selRow["Data_ID"].ToString() + " and orders.Factory_ID=" + selRow["Factory_ID"].ToString() + " and orders.Order_Number=" + selRow["رقم الطلب"].ToString();
                                    com = new MySqlCommand(query, dbconnection);
                                    MySqlDataReader dr = com.ExecuteReader();
                                    if (dr.HasRows)
                                    {
                                        while (dr.Read())
                                        {
                                            double orderQuantity = Convert.ToDouble(dr["Quantity"].ToString());
                                            if (orderQuantity == quantity)
                                            {
                                                query = "update order_details set Received=1 where OrderDetails_ID=" + dr["OrderDetails_ID"].ToString();
                                            }
                                            else
                                            {
                                                query = "select sum(supplier_permission_details.Total_Meters) as 'Total_Meters' from supplier_permission_details INNER JOIN data ON supplier_permission_details.Data_ID = data.Data_ID INNER JOIN import_supplier_permission ON supplier_permission_details.ImportSupplierPermission_ID = import_supplier_permission.ImportSupplierPermission_ID INNER JOIN storage_import_permission ON storage_import_permission.StorageImportPermission_ID = import_supplier_permission.StorageImportPermission_ID  INNER JOIN supplier ON import_supplier_permission.Supplier_ID = supplier.Supplier_ID inner JOIN order_details ON supplier_permission_details.Data_ID = order_details.Data_ID inner JOIN orders ON order_details.Order_ID = orders.Order_ID INNER JOIN type ON type.Type_ID = data.Type_ID INNER JOIN product ON product.Product_ID = data.Product_ID INNER JOIN factory ON data.Factory_ID = factory.Factory_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID LEFT JOIN color ON color.Color_ID = data.Color_ID LEFT JOIN size ON size.Size_ID = data.Size_ID LEFT JOIN sort ON sort.Sort_ID = data.Sort_ID where data.Data_ID=" + selRow["Data_ID"].ToString() + " and orders.Order_Number=" + selRow["رقم الطلب"].ToString() + " and orders.Factory_ID=" + selRow["Factory_ID"].ToString() + " ";
                                                com = new MySqlCommand(query, dbconnection2);
                                                MySqlDataReader dr2 = com.ExecuteReader();
                                                if (dr2.HasRows)
                                                {
                                                    while (dr2.Read())
                                                    {
                                                        if (dr2["Total_Meters"].ToString() != "")
                                                        {
                                                            if (Convert.ToDouble(dr2["Total_Meters"].ToString()) > 0)
                                                            {
                                                                query = "update order_details set Received=2 where OrderDetails_ID=" + dr["OrderDetails_ID"].ToString();
                                                            }
                                                            else
                                                            {
                                                                query = "update order_details set Received=0 where OrderDetails_ID=" + dr["OrderDetails_ID"].ToString();
                                                            }
                                                        }
                                                        else
                                                        {
                                                            query = "update order_details set Received=0 where OrderDetails_ID=" + dr["OrderDetails_ID"].ToString();
                                                        }
                                                    }
                                                }
                                                dr2.Close();
                                            }

                                            com = new MySqlCommand(query, dbconnection2);
                                            com.ExecuteNonQuery();
                                        }
                                    }
                                    dr.Close();
                                }
                                #endregion
                                dbconnection.Close();
                                dbconnection2.Close();
                                if (supplierReceiptUpdate == null)
                                {
                                    permissionsReport.refreshView(rowHandel, quantity, CartonNum);
                                }
                                else /*if (FormName == "SupplierReceiptUpdate")*/
                                {
                                    supplierReceiptUpdate.refreshView(rowHandel, quantity, CartonNum);
                                    
                                    permissionsReport.refreshView(rowHandel, quantity, CartonNum);
                                }
                                this.Close();
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
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    dbconnection.Close();
                    dbconnection2.Close();
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
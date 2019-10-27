using DevExpress.XtraEditors.Repository;
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
    public partial class Inventory_Report : Form
    {
        MySqlConnection dbconnection, dbconnection1;
        MainForm mainForm = null;
        XtraTabControl xtraTabControlStore;
        bool loaded = false;
        bool factoryFlage = false;
        bool groupFlage = false;
        bool flagProduct = false;

        public Inventory_Report(MainForm mainform, XtraTabControl XtraTabControlStore)
        {
            InitializeComponent();
            dbconnection = new MySqlConnection(connection.connectionString);
            dbconnection1 = new MySqlConnection(connection.connectionString);
            mainForm = mainform;
            xtraTabControlStore = XtraTabControlStore;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                string query = "select * from type";
                MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                DataTable dt = new DataTable();
                da.Fill(dt);
                comType.DataSource = dt;
                comType.DisplayMember = dt.Columns["Type_Name"].ToString();
                comType.ValueMember = dt.Columns["Type_ID"].ToString();
                comType.Text = "";

                query = "select * from factory";
                da = new MySqlDataAdapter(query, dbconnection);
                dt = new DataTable();
                da.Fill(dt);
                comFactory.DataSource = dt;
                comFactory.DisplayMember = dt.Columns["Factory_Name"].ToString();
                comFactory.ValueMember = dt.Columns["Factory_ID"].ToString();
                comFactory.Text = "";

                query = "select * from groupo";
                da = new MySqlDataAdapter(query, dbconnection);
                dt = new DataTable();
                da.Fill(dt);
                comGroup.DataSource = dt;
                comGroup.DisplayMember = dt.Columns["Group_Name"].ToString();
                comGroup.ValueMember = dt.Columns["Group_ID"].ToString();
                comGroup.Text = "";

                query = "select * from product";
                da = new MySqlDataAdapter(query, dbconnection);
                dt = new DataTable();
                da.Fill(dt);
                comProduct.DataSource = dt;
                comProduct.DisplayMember = dt.Columns["Product_Name"].ToString();
                comProduct.ValueMember = dt.Columns["Product_ID"].ToString();
                comProduct.Text = "";

                query = "select * from size";
                da = new MySqlDataAdapter(query, dbconnection);
                dt = new DataTable();
                da.Fill(dt);
                comSize.DataSource = dt;
                comSize.DisplayMember = dt.Columns["Size_Value"].ToString();
                comSize.ValueMember = dt.Columns["Size_ID"].ToString();
                comSize.Text = "";

                query = "select * from color";
                da = new MySqlDataAdapter(query, dbconnection);
                dt = new DataTable();
                da.Fill(dt);
                comColor.DataSource = dt;
                comColor.DisplayMember = dt.Columns["Color_Name"].ToString();
                comColor.ValueMember = dt.Columns["Color_ID"].ToString();
                comColor.Text = "";

                query = "select * from sort";
                da = new MySqlDataAdapter(query, dbconnection);
                dt = new DataTable();
                da.Fill(dt);
                comSort.DataSource = dt;
                comSort.DisplayMember = dt.Columns["Sort_Value"].ToString();
                comSort.ValueMember = dt.Columns["Sort_ID"].ToString();
                comSort.Text = "";

                query = "select * from store";
                da = new MySqlDataAdapter(query, dbconnection);
                dt = new DataTable();
                da.Fill(dt);
                comStore.DataSource = dt;
                comStore.DisplayMember = dt.Columns["Store_Name"].ToString();
                comStore.ValueMember = dt.Columns["Store_ID"].ToString();
                comStore.Text = "";

                loaded = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
            dbconnection1.Close();
        }

        private void comBox_SelectedValueChanged(object sender, EventArgs e)
        {
            if (loaded)
            {
                try
                {
                    ComboBox comBox = (ComboBox)sender;

                    switch (comBox.Name)
                    {
                        case "comType":
                            if (loaded)
                            {
                                string query = "select * from factory inner join type_factory on factory.Factory_ID=type_factory.Factory_ID inner join type on type_factory.Type_ID=type.Type_ID where type_factory.Type_ID=" + comType.SelectedValue.ToString();
                                MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                                DataTable dt = new DataTable();
                                da.Fill(dt);
                                comFactory.DataSource = dt;
                                comFactory.DisplayMember = dt.Columns["Factory_Name"].ToString();
                                comFactory.ValueMember = dt.Columns["Factory_ID"].ToString();
                                comFactory.Text = "";
                                dbconnection.Close();
                                dbconnection.Open();
                                query = "select TypeCoding_Method from type where Type_ID=" + comType.SelectedValue.ToString();
                                MySqlCommand com = new MySqlCommand(query, dbconnection);
                                int TypeCoding_Method = (int)com.ExecuteScalar();
                                dbconnection.Close();
                                if (TypeCoding_Method == 1)
                                {
                                    string query2 = "";
                                    if (comType.SelectedValue.ToString() == "2" || comType.SelectedValue.ToString() == "1")
                                    {
                                        query2 = "select * from groupo where Factory_ID=-1";
                                    }
                                    else
                                    {
                                        query2 = "select * from groupo where Factory_ID=" + -Convert.ToInt32(comType.SelectedValue.ToString()) + " and Type_ID=" + comType.SelectedValue.ToString();
                                    }

                                    MySqlDataAdapter da2 = new MySqlDataAdapter(query2, dbconnection);
                                    DataTable dt2 = new DataTable();
                                    da2.Fill(dt2);
                                    comGroup.DataSource = dt2;
                                    comGroup.DisplayMember = dt2.Columns["Group_Name"].ToString();
                                    comGroup.ValueMember = dt2.Columns["Group_ID"].ToString();
                                    comGroup.Text = "";
                                    groupFlage = true;
                                }
                                factoryFlage = true;

                                query = "select * from color where Type_ID=" + comType.SelectedValue.ToString();
                                da = new MySqlDataAdapter(query, dbconnection);
                                dt = new DataTable();
                                da.Fill(dt);
                                comColor.DataSource = dt;
                                comColor.DisplayMember = dt.Columns["Color_Name"].ToString();
                                comColor.ValueMember = dt.Columns["Color_ID"].ToString();
                                comColor.Text = "";
                                comFactory.Focus();
                            }
                            break;
                        case "comFactory":
                            if (factoryFlage)
                            {
                                dbconnection.Close();
                                dbconnection.Open();
                                string query = "select TypeCoding_Method from type where Type_ID=" + comType.SelectedValue.ToString();
                                MySqlCommand com = new MySqlCommand(query, dbconnection);
                                int TypeCoding_Method = (int)com.ExecuteScalar();
                                dbconnection.Close();
                                if (TypeCoding_Method == 2)
                                {
                                    string query2f = "select * from groupo where Type_ID=" + comType.SelectedValue.ToString() + " and Factory_ID=" + comFactory.SelectedValue.ToString();
                                    MySqlDataAdapter da2f = new MySqlDataAdapter(query2f, dbconnection);
                                    DataTable dt2f = new DataTable();
                                    da2f.Fill(dt2f);
                                    comGroup.DataSource = dt2f;
                                    comGroup.DisplayMember = dt2f.Columns["Group_Name"].ToString();
                                    comGroup.ValueMember = dt2f.Columns["Group_ID"].ToString();
                                    comGroup.Text = "";
                                }

                                groupFlage = true;

                                string query2 = "select * from size where Factory_ID=" + comFactory.SelectedValue.ToString();
                                MySqlDataAdapter da2 = new MySqlDataAdapter(query2, dbconnection);
                                DataTable dt2 = new DataTable();
                                da2.Fill(dt2);
                                comSize.DataSource = dt2;
                                comSize.DisplayMember = dt2.Columns["Size_Value"].ToString();
                                comSize.ValueMember = dt2.Columns["Size_ID"].ToString();
                                comSize.Text = "";
                                comGroup.Focus();
                            }
                            break;
                        case "comGroup":
                            if (groupFlage)
                            {
                                string supQuery = "", subQuery1 = "";
                                if (comType.SelectedValue.ToString() != "")
                                {
                                    supQuery += " and product.Type_ID=" + comType.SelectedValue.ToString();
                                }
                                if (comFactory.SelectedValue.ToString() != "")
                                {
                                    supQuery += " and product_factory_group.Factory_ID=" + comFactory.SelectedValue.ToString();
                                    subQuery1 += " and Factory_ID=" + comFactory.SelectedValue.ToString();
                                }
                                string query3 = "select distinct  product.Product_ID  ,Product_Name  from product inner join product_factory_group on product.Product_ID=product_factory_group.Product_ID  where product_factory_group.Group_ID=" + comGroup.SelectedValue.ToString() + supQuery + "  order by product.Product_ID";
                                MySqlDataAdapter da3 = new MySqlDataAdapter(query3, dbconnection);
                                DataTable dt3 = new DataTable();
                                da3.Fill(dt3);
                                comProduct.DataSource = dt3;
                                comProduct.DisplayMember = dt3.Columns["Product_Name"].ToString();
                                comProduct.ValueMember = dt3.Columns["Product_ID"].ToString();
                                comProduct.Text = "";

                                string query2 = "select * from size where Group_ID=" + comGroup.SelectedValue.ToString() + subQuery1;
                                MySqlDataAdapter da2 = new MySqlDataAdapter(query2, dbconnection);
                                DataTable dt2 = new DataTable();
                                da2.Fill(dt2);
                                comSize.DataSource = dt2;
                                comSize.DisplayMember = dt2.Columns["Size_Value"].ToString();
                                comSize.ValueMember = dt2.Columns["Size_ID"].ToString();
                                comSize.Text = "";

                                comProduct.Focus();
                                flagProduct = true;
                            }
                            break;

                        case "comProduct":
                            comColor.Focus();

                            break;

                        case "comColor":
                            comSize.Focus();
                            break;

                        case "comSize":
                            break;

                        case "comSort":
                            break;

                        case "comStore":
                            dbconnection.Open();
                            int sum = 1;
                            string q = "select inventory.Inventory_Num from inventory where inventory.Store_ID=" + comStore.SelectedValue.ToString() + " ORDER BY inventory.Inventory_ID DESC LIMIT 1";
                            MySqlCommand com2 = new MySqlCommand(q, dbconnection);
                            if (com2.ExecuteScalar() != null)
                            {
                                int r = int.Parse(com2.ExecuteScalar().ToString());
                                if (radOld.Checked)
                                {
                                    sum = r;
                                    txtInventoryNum.Text = sum.ToString();
                                }
                                else
                                {
                                    sum = r + 1;
                                    txtInventoryNum.Text = sum.ToString();
                                }
                            }
                            else
                            {
                                txtInventoryNum.Text = sum.ToString();
                            }
                            break;
                    }
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                dbconnection.Close();
            }
        }
        
        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (comStore.SelectedValue != null && comStore.Text != "")
            {
                try
                {
                    string q1, q2, q3, q4, fQuery = "";
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
                    if (comProduct.Text == "")
                    {
                        q3 = "select Product_ID from product";
                    }
                    else
                    {
                        q3 = comProduct.SelectedValue.ToString();
                    }
                    if (comGroup.Text == "")
                    {
                        q4 = "select Group_ID from groupo";
                    }
                    else
                    {
                        q4 = comGroup.SelectedValue.ToString();
                    }

                    if (comSize.Text != "")
                    {
                        fQuery += " and size.Size_ID=" + comSize.SelectedValue.ToString();
                    }

                    if (comColor.Text != "")
                    {
                        fQuery += " and color.Color_ID=" + comColor.SelectedValue.ToString();
                    }
                    if (comSort.Text != "")
                    {
                        fQuery += " and Sort.Sort_ID=" + comSort.SelectedValue.ToString();
                    }

                    dbconnection.Open();
                    dbconnection1.Open();
                    testQuantity(q1, q2, q3, q4, fQuery);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                dbconnection.Close();
                dbconnection1.Close();
            }
            else
            {
                MessageBox.Show("يجب اختيار المخزن");
            }
        }

        private void btnNewChosen_Click(object sender, EventArgs e)
        {
            clearCom();
        }

        void testQuantity(string qT, string qF, string qP, string qG, string fQuery)
        {
            string query = "SELECT data.Data_ID,data.Code as 'الكود',type.Type_Name as 'النوع',concat(product.Product_Name,' ',COALESCE(color.Color_Name,''),' ',data.Description,' ',groupo.Group_Name,' ',factory.Factory_Name,' ',COALESCE(size.Size_Value,''),' ',COALESCE(sort.Sort_Value,'')) as 'الاسم',SUM(storage.Total_Meters) as 'الكمية الحالية' FROM data INNER JOIN storage ON storage.Data_ID = data.Data_ID LEFT JOIN color ON color.Color_ID = data.Color_ID LEFT JOIN size ON size.Size_ID = data.Size_ID LEFT JOIN sort ON sort.Sort_ID = data.Sort_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID INNER JOIN factory ON factory.Factory_ID = data.Factory_ID  INNER JOIN product ON product.Product_ID = data.Product_ID  INNER JOIN type ON type.Type_ID = data.Type_ID where data.Data_ID=0 group by data.Data_ID";
            MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
            DataTable dt = new DataTable();
            da.Fill(dt);
            gridControl1.DataSource = dt;
            gridView1.Columns[0].Visible = false;

            query = "SELECT data.Data_ID,data.Code as 'الكود',type.Type_Name as 'النوع',concat(product.Product_Name,' ',COALESCE(color.Color_Name,''),' ',data.Description,' ',groupo.Group_Name,' ',factory.Factory_Name,' ',COALESCE(size.Size_Value,''),' ',COALESCE(sort.Sort_Value,'')) as 'الاسم' FROM data LEFT JOIN color ON color.Color_ID = data.Color_ID LEFT JOIN size ON size.Size_ID = data.Size_ID LEFT JOIN sort ON sort.Sort_ID = data.Sort_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID INNER JOIN factory ON factory.Factory_ID = data.Factory_ID INNER JOIN product ON product.Product_ID = data.Product_ID  INNER JOIN type ON type.Type_ID = data.Type_ID where data.Type_ID IN(" + qT + ") and data.Factory_ID IN(" + qF + ") and data.Product_ID IN (" + qP + ") and data.Group_ID IN (" + qG + ") " + fQuery + " order by SUBSTR(data.Code,1,16),color.Color_Name,data.Description,data.Sort_ID";
            MySqlCommand com = new MySqlCommand(query, dbconnection);
            MySqlDataReader dr = com.ExecuteReader();
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

                    string q = "select sum(storage.Total_Meters) as 'الكمية الحالية' from storage where storage.Data_ID=" + dr["Data_ID"].ToString() + " and storage.Store_ID=" + comStore.SelectedValue.ToString() + " group by storage.Data_ID";
                    MySqlCommand c = new MySqlCommand(q, dbconnection1);
                    if (c.ExecuteScalar() != null)
                    {
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns["الكمية الحالية"], c.ExecuteScalar().ToString());
                    }
                }
            }
            
            if (gridView1.IsLastVisibleRow)
            {
                gridView1.FocusedRowHandle = gridView1.RowCount - 1;
            }
            
            gridView1.Columns["النوع"].Width = 80;
            gridView1.Columns["الكود"].Width = 180;
            gridView1.Columns["الكمية الحالية"].Width = 200;
            //gridView1.Columns["الاسم"].Width = 300;
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (gridView1.RowCount > 0 && comStore.SelectedValue != null && txtInventoryNum.Text != "")
            {
                try
                {
                    dbconnection.Open();
                    dbconnection1.Open();
                    string query = "SELECT inventory.Inventory_ID FROM inventory_details INNER JOIN inventory ON inventory_details.Inventory_ID = inventory.Inventory_ID where inventory.Store_ID=" + comStore.SelectedValue.ToString() + " and inventory.Inventory_Num=" + txtInventoryNum.Text;
                    MySqlCommand com = new MySqlCommand(query, dbconnection);
                    MySqlDataReader dr = com.ExecuteReader();
                    if (!dr.HasRows)
                    {
                        string q = "insert into inventory (Store_ID,Inventory_Num,Date) values (@Store_ID,@Inventory_Num,@Date)";
                        MySqlCommand c = new MySqlCommand(q, dbconnection1);
                        c.Parameters.Add("@Store_ID", MySqlDbType.Int16);
                        c.Parameters["@Store_ID"].Value = comStore.SelectedValue.ToString();
                        c.Parameters.Add("@Inventory_Num", MySqlDbType.Int16);
                        c.Parameters["@Inventory_Num"].Value = txtInventoryNum.Text;
                        c.Parameters.Add("@Date", MySqlDbType.DateTime);
                        c.Parameters["@Date"].Value = DateTime.Now;
                        c.ExecuteNonQuery();

                        q = "select Inventory_ID from inventory order by Inventory_ID desc limit 1";
                        c = new MySqlCommand(q, dbconnection1);
                        int InventoryID = Convert.ToInt32(c.ExecuteScalar().ToString());

                        for (int i = 0; i < gridView1.RowCount; i++)
                        {
                            q = "insert into inventory_details (Inventory_ID,Data_ID,Old_Quantity,Date) values (@Inventory_ID,@Data_ID,@Old_Quantity,@Date)";
                            c = new MySqlCommand(q, dbconnection1);
                            c.Parameters.Add("@Inventory_ID", MySqlDbType.Int16);
                            c.Parameters["@Inventory_ID"].Value = InventoryID;
                            c.Parameters.Add("@Data_ID", MySqlDbType.Int16);
                            c.Parameters["@Data_ID"].Value = gridView1.GetRowCellDisplayText(i, "Data_ID");
                            c.Parameters.Add("@Old_Quantity", MySqlDbType.Decimal);
                            if (gridView1.GetRowCellDisplayText(i, "الكمية الحالية") != "")
                            {
                                c.Parameters["@Old_Quantity"].Value = gridView1.GetRowCellDisplayText(i, "الكمية الحالية");
                            }
                            else
                            {
                                c.Parameters["@Old_Quantity"].Value = null;
                            }
                            c.Parameters.Add("@Date", MySqlDbType.DateTime);
                            c.Parameters["@Date"].Value = DateTime.Now;
                            c.ExecuteNonQuery();
                        }
                    }
                    else
                    {
                        while (dr.Read())
                        {
                            int InventoryID = Convert.ToInt32(dr["Inventory_ID"].ToString());

                            for (int i = 0; i < gridView1.RowCount; i++)
                            {
                                string q2 = "SELECT inventory_details.InventoryDetails_ID FROM inventory_details INNER JOIN inventory ON inventory_details.Inventory_ID = inventory.Inventory_ID where Store_ID=" + comStore.SelectedValue.ToString() + " and Inventory_Num=" + txtInventoryNum.Text + " and Data_ID=" + gridView1.GetRowCellDisplayText(i, "Data_ID");
                                MySqlCommand c2 = new MySqlCommand(q2, dbconnection1);
                                if (c2.ExecuteScalar() == null)
                                {
                                    string q = "insert into inventory_details (Inventory_ID,Data_ID,Old_Quantity,Date) values (@Inventory_ID,@Data_ID,@Old_Quantity,@Date)";
                                    MySqlCommand c = new MySqlCommand(q, dbconnection1);
                                    c.Parameters.Add("@Inventory_ID", MySqlDbType.Int16);
                                    c.Parameters["@Inventory_ID"].Value = InventoryID;
                                    c.Parameters.Add("@Data_ID", MySqlDbType.Int16);
                                    c.Parameters["@Data_ID"].Value = gridView1.GetRowCellDisplayText(i, "Data_ID");
                                    c.Parameters.Add("@Old_Quantity", MySqlDbType.Decimal);
                                    if (gridView1.GetRowCellDisplayText(i, "الكمية الحالية") != "")
                                    {
                                        c.Parameters["@Old_Quantity"].Value = gridView1.GetRowCellDisplayText(i, "الكمية الحالية");
                                    }
                                    else
                                    {
                                        c.Parameters["@Old_Quantity"].Value = null;
                                    }
                                    c.Parameters.Add("@Date", MySqlDbType.DateTime);
                                    c.Parameters["@Date"].Value = DateTime.Now;
                                    c.ExecuteNonQuery();
                                }
                            }
                        }
                        dr.Close();
                    }


                    List<Inventory_Items> bi = new List<Inventory_Items>();
                    for (int i = 0; i < gridView1.RowCount; i++)
                    {
                        Inventory_Items item = new Inventory_Items() { Code = gridView1.GetRowCellDisplayText(i, gridView1.Columns["الكود"]), Product_Type = gridView1.GetRowCellDisplayText(i, gridView1.Columns["النوع"]), Product_Name = gridView1.GetRowCellDisplayText(i, gridView1.Columns["الاسم"]), Total_Meters = gridView1.GetRowCellDisplayText(i, gridView1.Columns["الكمية الحالية"]) };
                        bi.Add(item);
                    }
                    Report_Inventory f = new Report_Inventory();
                    f.PrintInvoice(bi);
                    f.ShowDialog();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                dbconnection.Close();
                dbconnection1.Close();
            }
        }

        private void rad_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                loaded = false;
                comStore.SelectedIndex = -1;
                txtInventoryNum.Text = "";
                gridControl1.DataSource = null;
                loaded = true;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //clear function
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
    }
}

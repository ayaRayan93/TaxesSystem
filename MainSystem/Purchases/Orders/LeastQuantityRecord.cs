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
    public partial class LeastQuantityRecord : Form
    {
        MySqlConnection dbconnection;
        bool loaded = false;
        bool factoryFlage = false;
        bool groupFlage = false;
        bool flagProduct = false;
        DataRow row1;

        public LeastQuantityRecord(MainForm mainFom, XtraTabControl tabControl)
        {
            InitializeComponent();
            dbconnection = new MySqlConnection(connection.connectionString);
        }

        private void Form1_Load(object sender, EventArgs e)
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
            
            //search2();
            dbconnection.Close();
            loaded = true;
        }

        private void comBox_SelectedValueChanged(object sender, EventArgs e)
        {
            if (loaded)
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
                                    query2 = "select * from groupo where Factory_ID=" + -Convert.ToInt16(comType.SelectedValue.ToString()) + " and Type_ID=" + comType.SelectedValue.ToString();
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
                }
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
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
                string query = "select data.Data_ID,data.Code as 'الكود',type.Type_Name as 'النوع',concat(product.Product_Name,' - ',type.Type_Name,' - ',factory.Factory_Name,' - ',groupo.Group_Name,' ',COALESCE(color.Color_Name,''),' ',COALESCE(size.Size_Value,''),' ',COALESCE(sort.Sort_Value,'')) as 'الاسم',data.Carton as 'الكرتنة' FROM data LEFT JOIN color ON color.Color_ID = data.Color_ID LEFT JOIN size ON size.Size_ID = data.Size_ID LEFT JOIN sort ON sort.Sort_ID = data.Sort_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID INNER JOIN factory ON factory.Factory_ID = data.Factory_ID  INNER JOIN product ON product.Product_ID = data.Product_ID  INNER JOIN type ON type.Type_ID = data.Type_ID where  data.Type_ID IN(" + q1 + ") and  data.Factory_ID  IN(" + q2 + ") and data.Group_ID IN (" + q4 + ") and data.Data_ID=0 group by data.Data_ID";
                MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                DataTable dt = new DataTable();
                da.Fill(dt);
                gridControl1.DataSource = dt;

                query = "SELECT data.Data_ID,data.Code as 'الكود',type.Type_Name as 'النوع',concat(product.Product_Name,' ',COALESCE(color.Color_Name,''),' ',data.Description,' ',groupo.Group_Name,' ',factory.Factory_Name,' ',COALESCE(size.Size_Value,''),' ',COALESCE(sort.Sort_Value,'')) as 'الاسم',data.Carton as 'الكرتنة' FROM data LEFT JOIN color ON color.Color_ID = data.Color_ID LEFT JOIN size ON size.Size_ID = data.Size_ID LEFT JOIN sort ON sort.Sort_ID = data.Sort_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID INNER JOIN factory ON factory.Factory_ID = data.Factory_ID  INNER JOIN product ON product.Product_ID = data.Product_ID  INNER JOIN type ON type.Type_ID = data.Type_ID where data.Type_ID IN(" + q1 + ") and data.Factory_ID IN(" + q2 + ") and data.Product_ID IN (" + q3 + ") and data.Group_ID IN (" + q4 + ") " + fQuery + " order by SUBSTR(data.Code,1,16),color.Color_Name,data.Sort_ID";
                MySqlCommand comand = new MySqlCommand(query, dbconnection);
                MySqlDataReader dr = comand.ExecuteReader();
                while (dr.Read())
                {
                    gridView1.AddNewRow();
                    int rowHandle = gridView1.GetRowHandle(gridView1.DataRowCount);
                    if (gridView1.IsNewItemRow(rowHandle))
                    {
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[0], dr["Data_ID"]);
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[1], dr["الكود"]);
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns["النوع"], dr["النوع"]);
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns["الاسم"], dr["الاسم"]);
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns["الكرتنة"], dr["الكرتنة"]);
                    }
                }
                dr.Close();
                gridView1.Columns[0].Visible = false;
                for (int i = 2; i < gridView1.Columns.Count; i++)
                {
                    gridView1.Columns[i].Width = 100;
                }
                gridView1.Columns["الكود"].Width = 180;
                gridView1.Columns["الاسم"].Width = 270;
                gridView1.Columns["الكرتنة"].Width = 60;
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

        private void gridView1_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            try
            {
                row1 = gridView1.GetDataRow(gridView1.GetRowHandle(e.RowHandle));
                string code = row1["الكود"].ToString();
                txtCode.Text = code;

                dbconnection.Open();
                string query = "select Least_Quantity from least_Offer where Data_ID=" + row1[0].ToString();
                MySqlCommand command = new MySqlCommand(query, dbconnection);
                if (command.ExecuteScalar() != null)
                {
                    string result = command.ExecuteScalar().ToString();
                    txtLeastQuantity.Text = result;
                }
                else
                {
                    txtLeastQuantity.Text = "";
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
                if (row1 != null && txtLeastQuantity.Text != "")
                {
                    string query = "select Least_Quantity from least_Offer where Data_ID=" + row1[0].ToString();
                    MySqlCommand command = new MySqlCommand(query, dbconnection);
                    dbconnection.Open();
                    var result = command.ExecuteReader();
                    if (result.Read())
                    {
                        dbconnection.Close();
                        dbconnection.Open();
                        string query2 = "update least_Offer set Least_Quantity=" + txtLeastQuantity.Text + " where Data_ID=" + row1[0].ToString();
                        MySqlCommand comand = new MySqlCommand(query2, dbconnection);
                        comand.ExecuteNonQuery();
                        MessageBox.Show("تم التعديل");
                    }
                    else
                    {
                        dbconnection.Close();
                        dbconnection.Open();
                        string query2 = "insert into least_Offer (Data_ID,Least_Quantity) values (" + row1[0].ToString() + " , " + txtLeastQuantity.Text + ")";
                        MySqlCommand comand = new MySqlCommand(query2, dbconnection);
                        comand.ExecuteNonQuery();
                        MessageBox.Show("تم الاضافة");
                    }
                    txtCode.Text = "";
                    txtLeastQuantity.Text = "";
                }
                else
                {
                    MessageBox.Show("برجاء التاكد من البيانات");
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

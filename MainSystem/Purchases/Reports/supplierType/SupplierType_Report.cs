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
    public partial class SupplierType_Report : Form
    {
        MySqlConnection dbconnection, dbconnection2, dbconnection3, dbconnection4;
        bool loaded = false;
        bool loadedFactory = false;
        XtraTabControl tabControlContent;

        public SupplierType_Report(MainForm mainform, XtraTabControl TabControlContent)
        {
            InitializeComponent();
            dbconnection = new MySqlConnection(connection.connectionString);
            dbconnection2 = new MySqlConnection(connection.connectionString);
            dbconnection3 = new MySqlConnection(connection.connectionString);
            dbconnection4 = new MySqlConnection(connection.connectionString);
            tabControlContent = TabControlContent;
        }

        private void requestStored_Load(object sender, EventArgs e)
        {
            try
            {
                DateTime lastmonth = DateTime.Today.AddMonths(-1);
                string query = "select * from supplier";
                MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                DataTable dt = new DataTable();
                da.Fill(dt);
                comSupplier.DataSource = dt;
                comSupplier.DisplayMember = dt.Columns["Supplier_Name"].ToString();
                comSupplier.ValueMember = dt.Columns["Supplier_ID"].ToString();
                comSupplier.Text = "";
                txtSupplierID.Text = "";

                this.dataGridView1.RightToLeft = RightToLeft.No;

                this.dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;

                this.dataGridView1.ColumnHeadersHeight = this.dataGridView1.ColumnHeadersHeight * 2;

                this.dataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomCenter;

                this.dataGridView1.CellPainting += new DataGridViewCellPaintingEventHandler(dataGridView1_CellPainting);

                this.dataGridView1.Paint += new PaintEventHandler(dataGridView1_Paint);

                this.dataGridView1.Scroll += new ScrollEventHandler(dataGridView1_Scroll);

                this.dataGridView1.ColumnWidthChanged += new DataGridViewColumnEventHandler(dataGridView1_ColumnWidthChanged);

                loaded = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }

        void dataGridView1_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
        {
            try
            {
                Rectangle rtHeader = this.dataGridView1.DisplayRectangle;

                rtHeader.Height = this.dataGridView1.ColumnHeadersHeight / 2;

                this.dataGridView1.Invalidate(rtHeader);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        void dataGridView1_Scroll(object sender, ScrollEventArgs e)
        {
            Rectangle rtHeader = this.dataGridView1.DisplayRectangle;

            rtHeader.Height = this.dataGridView1.ColumnHeadersHeight / 2;

            this.dataGridView1.Invalidate(rtHeader);
        }

        void dataGridView1_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            try
            {
                if (e.RowIndex == -1 && e.ColumnIndex > -1)
                {
                    Rectangle r2 = e.CellBounds;

                    r2.Y += e.CellBounds.Height / 2;

                    r2.Height = e.CellBounds.Height / 2;

                    e.PaintBackground(r2, true);

                    e.PaintContent(r2);

                    e.Handled = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        void dataGridView1_Paint(object sender, PaintEventArgs e)
        {
            try
            {
                string[] monthes = {  "الاجمالى","ف3", "ف2", "ف1" };
                Rectangle r2 = this.dataGridView1.GetCellDisplayRectangle(0, -1, true);
                Rectangle r1 = this.dataGridView1.GetCellDisplayRectangle(0, -1, true);

                for (int j = 0; j < 8;)
                {
                    /*if (j == 1)
                    {
                        r2 = this.dataGridView1.GetCellDisplayRectangle(j, -1, true);

                        int w2 = this.dataGridView1.GetCellDisplayRectangle(j + 1, -1, true).Width;
                        int w3 = this.dataGridView1.GetCellDisplayRectangle(j + 2, -1, true).Width;

                        r2.X += 1;

                        r2.Y += 1;

                        r2.Width = r2.Width + w2 + w3 - 2;

                        r2.Height = r2.Height / 2 - 2;

                        e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(115, 129, 201)), r2);

                        StringFormat format = new StringFormat();

                        format.Alignment = StringAlignment.Center;

                        format.LineAlignment = StringAlignment.Center;

                        e.Graphics.DrawString(monthes[j / 3],
                            this.dataGridView1.ColumnHeadersDefaultCellStyle.Font,
                            new SolidBrush(Color.White),
                            r2,
                            format);

                        r1 = r2;
                        j += 3;
                    }
                    else
                    {*/
                        r1 = this.dataGridView1.GetCellDisplayRectangle(j, -1, true);

                        int w2 = this.dataGridView1.GetCellDisplayRectangle(j + 1, -1, true).Width;

                        r1.X += 1;

                        r1.Y += 1;

                        r1.Width = r1.Width + w2 - 2;

                        r1.Height = r1.Height / 2 - 2;

                        e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(115, 129, 201)), r1);

                        StringFormat format = new StringFormat();

                        format.Alignment = StringAlignment.Center;

                        format.LineAlignment = StringAlignment.Center;

                        e.Graphics.DrawString(monthes[j / 2],
                            this.dataGridView1.ColumnHeadersDefaultCellStyle.Font,
                            new SolidBrush(Color.White),
                            r1,
                            format);
                        
                        j += 2;
                    //}
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        
        private void comBranch_SelectedValueChanged(object sender, EventArgs e)
        {
            if (loaded)
            {
                try
                {
                    txtSupplierID.Text = comSupplier.SelectedValue.ToString();

                    loadedFactory = false;
                    string query = "select * from Factory inner join supplier_factory on factory.Factory_ID=supplier_factory.Factory_ID where Supplier_ID=" + comSupplier.SelectedValue.ToString();
                    MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    comFactory.DataSource = dt;
                    comFactory.DisplayMember = dt.Columns["Factory_Name"].ToString();
                    comFactory.ValueMember = dt.Columns["Factory_ID"].ToString();
                    comFactory.Text = "";
                    txtFactory.Text = "";
                    loadedFactory = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void comFactory_SelectedValueChanged(object sender, EventArgs e)
        {
            if (loadedFactory)
            {
                try
                {
                    txtFactory.Text = comFactory.SelectedValue.ToString();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void txtBranchID_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    dbconnection.Open();
                    string query = "select Supplier_Name from supplier where Supplier_ID=" + txtSupplierID.Text + "";
                    MySqlCommand com = new MySqlCommand(query, dbconnection);
                    if (com.ExecuteScalar() != null)
                    {
                        Name = (string)com.ExecuteScalar();
                        loaded = false;
                        comSupplier.Text = Name;
                        comSupplier.SelectedValue = txtSupplierID.Text;
                        loaded = true;
                    }
                    else
                    {
                        MessageBox.Show("there is no item with this id");

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                dbconnection.Close();
                //dbconnection6.Close();
            }
        }

        private void txtFactory_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    dbconnection.Open();
                    string query = "select Factory_Name from factory where Factory_ID=" + txtFactory.Text;// + " and Factory_ID in (" + comFactory.DataSource + ")";
                    MySqlCommand com = new MySqlCommand(query, dbconnection);
                    if (com.ExecuteScalar() != null)
                    {
                        Name = (string)com.ExecuteScalar();
                        loadedFactory = false;
                        comFactory.Text = Name;
                        comFactory.SelectedValue = txtFactory.Text;
                        loadedFactory = true;
                    }
                    else
                    {
                        MessageBox.Show("there is no item with this id");

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                dbconnection.Close();
            }
        }

        private void btnDisplay_Click(object sender, EventArgs e)
        {
            if (comSupplier.SelectedValue != null && comSupplier.Text != "" && comFactory.SelectedValue != null && comFactory.Text != "")
            {
                try
                {
                    search();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                dbconnection.Close();
                dbconnection2.Close();
                dbconnection3.Close();
                dbconnection4.Close();
            }
            else
            {
                MessageBox.Show("يجب اختيار المورد والمصنع");
            }
        }

        public void search()
        {
            int supplierId = Convert.ToInt16(comSupplier.SelectedValue.ToString());
            int factoryId = Convert.ToInt16(comFactory.SelectedValue.ToString());
            
            double totalBillQuantity = 0;
            double totalReturnsQuantity = 0;
            double totalBillCost = 0;
            double totalReturnsCost = 0;
            
            dataGridView1.Rows.Clear();

            dbconnection.Open();
            dbconnection2.Open();
            dbconnection3.Open();
            dbconnection4.Open();

            #region others
            string query = "SELECT distinct type.Type_Name,data.Type_ID FROM supplier_bill_details INNER JOIN supplier_bill ON supplier_bill_details.Bill_ID = supplier_bill.Bill_ID INNER JOIN data ON data.Data_ID = supplier_bill_details.Data_ID INNER JOIN supplier_factory ON data.Factory_ID = supplier_factory.Factory_ID and supplier_bill.Supplier_ID = supplier_factory.Supplier_ID inner join type on type.Type_ID=data.Type_ID where Date(supplier_bill.Date) >= '" + dateTimePickerFrom.Value.ToString("yyyy-MM-dd") + "' and Date(supplier_bill.Date) <= '" + dateTimePickerTo.Value.ToString("yyyy-MM-dd") + "' and supplier_bill.Supplier_ID=" + supplierId + " and data.Factory_ID=" + factoryId;
            MySqlCommand comand = new MySqlCommand(query, dbconnection);
            MySqlDataReader dr = comand.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    double BillQuantityS1 = 0;
                    double BillCostS1 = 0;
                    double BillQuantityS2 = 0;
                    double BillCostS2 = 0;
                    double BillQuantityS3 = 0;
                    double BillCostS3 = 0;

                    int n = dataGridView1.Rows.Add();
                    dataGridView1.Rows[n].Cells["Descripe"].Value = dr["Type_Name"].ToString();
                    dataGridView1.Rows[n].DefaultCellStyle.BackColor = Color.FromArgb(64, 78, 156);
                    dataGridView1.Rows[n].DefaultCellStyle.ForeColor = Color.White;
                    dataGridView1.Rows[n].DefaultCellStyle.SelectionBackColor = Color.FromArgb(64, 78, 156);
                    dataGridView1.Rows[n].DefaultCellStyle.SelectionForeColor = Color.White;

                    string query2 = "SELECT distinct data.Classification FROM supplier_bill_details INNER JOIN supplier_bill ON supplier_bill_details.Bill_ID = supplier_bill.Bill_ID INNER JOIN data ON data.Data_ID = supplier_bill_details.Data_ID INNER JOIN supplier_factory ON data.Factory_ID = supplier_factory.Factory_ID  and supplier_bill.Supplier_ID = supplier_factory.Supplier_ID where Date(supplier_bill.Date) >= '" + dateTimePickerFrom.Value.ToString("yyyy-MM-dd") + "' and Date(supplier_bill.Date) <= '" + dateTimePickerTo.Value.ToString("yyyy-MM-dd") + "' and supplier_bill.Supplier_ID=" + supplierId + " and data.Factory_ID=" + factoryId + " and data.Type_ID=" + dr["Type_ID"].ToString();
                    MySqlCommand comand2 = new MySqlCommand(query2, dbconnection2);
                    MySqlDataReader dr2 = comand2.ExecuteReader();
                    if (dr2.HasRows)
                    {
                        while (dr2.Read())
                        {
                            n = dataGridView1.Rows.Add();
                            dataGridView1.Rows[n].Cells["Descripe"].Value = dr2["Classification"].ToString();

                            double totalFQuantity = 0;
                            double totalFCost = 0;

                            string query3 = "SELECT sum(supplier_bill_details.Total_Meters) as 'الكمية',sum(supplier_bill_details.Purchasing_Price) as 'المبلغ',data.Sort_ID FROM supplier_bill_details INNER JOIN supplier_bill ON supplier_bill_details.Bill_ID = supplier_bill.Bill_ID INNER JOIN data ON data.Data_ID = supplier_bill_details.Data_ID INNER JOIN supplier_factory ON data.Factory_ID = supplier_factory.Factory_ID  and supplier_bill.Supplier_ID = supplier_factory.Supplier_ID where Date(supplier_bill.Date) >= '" + dateTimePickerFrom.Value.ToString("yyyy-MM-dd") + "' and Date(supplier_bill.Date) <= '" + dateTimePickerTo.Value.ToString("yyyy-MM-dd") + "' and supplier_bill.Supplier_ID=" + supplierId + " and data.Factory_ID=" + factoryId + " and data.Type_ID=" + dr["Type_ID"].ToString() + " and data.Classification='" + dr2["Classification"].ToString() + "' group by data.Sort_ID";
                            MySqlCommand comand3 = new MySqlCommand(query3, dbconnection3);
                            MySqlDataReader dr3 = comand3.ExecuteReader();
                            if (dr3.HasRows)
                            {
                                while (dr3.Read())
                                {
                                    totalBillQuantity = Convert.ToDouble(dr3["الكمية"].ToString());
                                    totalBillCost = Convert.ToDouble(dr3["المبلغ"].ToString())* Convert.ToDouble(dr3["الكمية"].ToString());

                                    string query4 = "SELECT sum(supplier_return_bill_details.Total_Meters) as 'الكمية',sum(supplier_return_bill_details.Purchasing_Price) as 'المبلغ',data.Sort_ID FROM supplier_return_bill_details INNER JOIN supplier_return_bill ON supplier_return_bill_details.ReturnBill_ID = supplier_return_bill.ReturnBill_ID INNER JOIN data ON data.Data_ID = supplier_return_bill_details.Data_ID INNER JOIN supplier_factory ON data.Factory_ID = supplier_factory.Factory_ID  and supplier_return_bill.Supplier_ID = supplier_factory.Supplier_ID where Date(supplier_return_bill.Date) >= '" + dateTimePickerFrom.Value.ToString("yyyy-MM-dd") + "' and Date(supplier_return_bill.Date) <= '" + dateTimePickerTo.Value.ToString("yyyy-MM-dd") + "' and supplier_return_bill.Supplier_ID=" + supplierId + " and data.Factory_ID=" + factoryId + " and data.Type_ID=" + dr["Type_ID"].ToString() + " and data.Classification='" + dr2["Classification"].ToString() + "' group by data.Sort_ID";
                                    MySqlCommand comand4 = new MySqlCommand(query4, dbconnection4);
                                    MySqlDataReader dr4 = comand4.ExecuteReader();
                                    if (dr4.HasRows)
                                    {
                                        while (dr4.Read())
                                        {
                                            if (dr4["المبلغ"].ToString() != "")
                                            {
                                                totalReturnsQuantity = Convert.ToDouble(dr4["الكمية"].ToString());
                                                totalReturnsCost = Convert.ToDouble(dr4["المبلغ"].ToString())* Convert.ToDouble(dr4["الكمية"].ToString());
                                            }
                                        }
                                    }
                                    dr4.Close();

                                    if (dr3["Sort_ID"].ToString() == "1")
                                    {
                                        dataGridView1.Rows[n].Cells["QuantityF1"].Value = totalBillQuantity - totalReturnsQuantity;
                                        dataGridView1.Rows[n].Cells["CostF1"].Value = totalBillCost - totalReturnsCost;

                                        totalFQuantity += totalBillQuantity - totalReturnsQuantity;
                                        totalFCost += totalBillCost - totalReturnsCost;

                                        BillQuantityS1 += totalBillQuantity - totalReturnsQuantity;
                                        BillCostS1 += totalBillCost - totalReturnsCost;
                                    }
                                    else if (dr3["Sort_ID"].ToString() == "2")
                                    {
                                        dataGridView1.Rows[n].Cells["QuantityF2"].Value = totalBillQuantity - totalReturnsQuantity;
                                        dataGridView1.Rows[n].Cells["CostF2"].Value = totalBillCost - totalReturnsCost;

                                        totalFQuantity += totalBillQuantity - totalReturnsQuantity;
                                        totalFCost += totalBillCost - totalReturnsCost;

                                        BillQuantityS2 += totalBillQuantity - totalReturnsQuantity;
                                        BillCostS2 += totalBillCost - totalReturnsCost;
                                    }
                                    else if (dr3["Sort_ID"].ToString() == "3")
                                    {
                                        dataGridView1.Rows[n].Cells["QuantityF3"].Value = totalBillQuantity - totalReturnsQuantity;
                                        dataGridView1.Rows[n].Cells["CostF3"].Value = totalBillCost - totalReturnsCost;

                                        totalFQuantity += totalBillQuantity - totalReturnsQuantity;
                                        totalFCost += totalBillCost - totalReturnsCost;

                                        BillQuantityS3 += totalBillQuantity - totalReturnsQuantity;
                                        BillCostS3 += totalBillCost - totalReturnsCost;
                                    }
                                }
                            }
                            dr3.Close();

                            dataGridView1.Rows[n].Cells["TotalQuantity"].Value = totalFQuantity;
                            dataGridView1.Rows[n].Cells["TotalCost"].Value = totalFCost;
                        }
                    }
                    dr2.Close();


                    n = dataGridView1.Rows.Add();
                    dataGridView1.Rows[n].Cells["Descripe"].Value = "الاجمالى";
                    dataGridView1.Rows[n].DefaultCellStyle.BackColor = Color.LightGray;
                    dataGridView1.Rows[n].DefaultCellStyle.SelectionBackColor = Color.LightSteelBlue;
                    dataGridView1.Rows[n].DefaultCellStyle.SelectionForeColor = Color.Black;

                    dataGridView1.Rows[n].Cells["QuantityF1"].Value = BillQuantityS1;
                    dataGridView1.Rows[n].Cells["CostF1"].Value = BillCostS1;
                    dataGridView1.Rows[n].Cells["QuantityF2"].Value = BillQuantityS2;
                    dataGridView1.Rows[n].Cells["CostF2"].Value = BillCostS2;
                    dataGridView1.Rows[n].Cells["QuantityF3"].Value = BillQuantityS3;
                    dataGridView1.Rows[n].Cells["CostF3"].Value = BillCostS3;
                    dataGridView1.Rows[n].Cells["TotalQuantity"].Value = BillQuantityS1 + BillQuantityS2 + BillQuantityS3;
                    dataGridView1.Rows[n].Cells["TotalCost"].Value = BillCostS1 + BillCostS2 + BillCostS3;
                }
            }
            dr.Close();
            #endregion
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            if (comSupplier.SelectedValue != null && comSupplier.Text != "" && comFactory.SelectedValue != null && comFactory.Text != "" && dataGridView1.RowCount > 0)
            {
                try
                {
                    List<SupplierType_Items> bi = new List<SupplierType_Items>();
                    for (int i = 0; i < dataGridView1.RowCount; i++)
                    {
                        string descripe = "", QS1 = "", CS1 = "", QS2 = "", CS2 = "", QS3 = "", CS3 = "", QF = "", CF = "";

                        if (dataGridView1.Rows[i].Cells["Descripe"].Value != null)
                        {
                            descripe = dataGridView1.Rows[i].Cells["Descripe"].Value.ToString();
                        }
                        if (dataGridView1.Rows[i].Cells["QuantityF1"].Value != null)
                        {
                            QS1 = dataGridView1.Rows[i].Cells["QuantityF1"].Value.ToString();
                        }
                        if (dataGridView1.Rows[i].Cells["QuantityF2"].Value != null)
                        {
                            QS2 = dataGridView1.Rows[i].Cells["QuantityF2"].Value.ToString();
                        }
                        if (dataGridView1.Rows[i].Cells["QuantityF3"].Value != null)
                        {
                            QS3 = dataGridView1.Rows[i].Cells["QuantityF3"].Value.ToString();
                        }
                        if (dataGridView1.Rows[i].Cells["CostF1"].Value != null)
                        {
                            CS1 = dataGridView1.Rows[i].Cells["CostF1"].Value.ToString();
                        }
                        if (dataGridView1.Rows[i].Cells["CostF2"].Value != null)
                        {
                            CS2 = dataGridView1.Rows[i].Cells["CostF2"].Value.ToString();
                        }
                        if (dataGridView1.Rows[i].Cells["CostF3"].Value != null)
                        {
                            CS3 = dataGridView1.Rows[i].Cells["CostF3"].Value.ToString();
                        }
                        if (dataGridView1.Rows[i].Cells["TotalQuantity"].Value != null)
                        {
                            QF = dataGridView1.Rows[i].Cells["TotalQuantity"].Value.ToString();
                        }
                        if (dataGridView1.Rows[i].Cells["TotalCost"].Value != null)
                        {
                            CF = dataGridView1.Rows[i].Cells["TotalCost"].Value.ToString();
                        }
                        SupplierType_Items item = new SupplierType_Items() { Description = descripe, CostS1 = CS1, CostS2 = CS2, CostS3 = CS3, CostSF = CF, QuantityS1 = QS1, QuantityS2 = QS2, QuantityS3 = QS3, QuantitySF = QF };
                        bi.Add(item);
                    }
                    Report_SupplierType f = new Report_SupplierType();
                    f.PrintInvoice(comSupplier.Text, comFactory.Text, dateTimePickerFrom.Value.Date.ToString(), dateTimePickerTo.Value.Date.ToString(), bi);
                    f.ShowDialog();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                MessageBox.Show("يجب اختيار المورد والمصنع");
            }
        }

        public XtraTabPage getTabPage(XtraTabControl tabControl, string text)
        {
            for (int i = 0; i < tabControl.TabPages.Count; i++)
                if (tabControl.TabPages[i].Text == text)
                {
                    return tabControl.TabPages[i];
                }
            return null;
        }
    }
}

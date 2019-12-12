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
    public partial class SupplierBillsTransitions_Report : Form
    {
        MySqlConnection dbconnection, dbconnection6;
        bool loaded = false;
        XtraTabControl tabControlContent;

        public SupplierBillsTransitions_Report(MainForm mainform, XtraTabControl TabControlContent)
        {
            InitializeComponent();
            dbconnection = new MySqlConnection(connection.connectionString);
            dbconnection6 = new MySqlConnection(connection.connectionString);
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
                string[] monthes = { "السدادات والمرتجعات", "المسحوبات", "الرصيد" };
                Rectangle r2 = this.dataGridView1.GetCellDisplayRectangle(0, -1, true);
                Rectangle r1 = this.dataGridView1.GetCellDisplayRectangle(0, -1, true);

                for (int j = 1; j < 8;)
                {
                    if (j == 1)
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
                    {
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

                        e.Graphics.DrawString(monthes[j / 3],
                            this.dataGridView1.ColumnHeadersDefaultCellStyle.Font,
                            new SolidBrush(Color.White),
                            r1,
                            format);
                        
                        j += 2;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        
        private void comBranch_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (loaded)
                {
                    int supplierID = 0;
                    txtSupplierID.Text = comSupplier.SelectedValue.ToString();
                    if (int.TryParse(txtSupplierID.Text, out supplierID) && comSupplier.SelectedValue != null)
                    {
                        search(Convert.ToInt32(comSupplier.SelectedValue.ToString()));
                    }
                    else
                    {
                        MessageBox.Show("تاكد من البيانات");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
            dbconnection6.Close();
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
                        search(Convert.ToInt32(comSupplier.SelectedValue.ToString()));
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
                dbconnection6.Close();
            }
        }

        private void btnDisplay_Click(object sender, EventArgs e)
        {
            try
            {
                loaded = false;
                comSupplier.SelectedIndex = -1;
                txtSupplierID.Text = "";
                loaded = true;
                search(0);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
            dbconnection6.Close();
        }

        public void search(int supplierId)
        {
            double totalTransition = 0;
            double totalBills = 0;
            double totalTaswyaAdd = 0;
            double totalTaswyaDiscount = 0;
            double TotalReturns = 0;

            dataGridView1.Rows.Clear();
            
            dbconnection.Open();
            dbconnection6.Open();
            if (supplierId == 0)
            {
                #region first row
                string query = "SELECT sum(supplier_bill.Total_Price_A) as 'المبلغ' FROM supplier_bill INNER JOIN supplier ON supplier.Supplier_ID = supplier_bill.Supplier_ID where Date(supplier_bill.Date) < '" + dateTimePickerFrom.Value.ToString("yyyy-MM-dd") + "'";
                MySqlCommand comand = new MySqlCommand(query, dbconnection);
                MySqlDataReader dr = comand.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        if (dr["المبلغ"].ToString() != "")
                        {
                            totalBills = Convert.ToDouble(dr["المبلغ"].ToString());
                        }
                    }
                }
                dr.Close();
                query = "SELECT sum(supplier_taswaya.Money_Paid) as 'المبلغ' FROM supplier_taswaya INNER JOIN supplier ON supplier.Supplier_ID = supplier_taswaya.Supplier_ID where supplier_taswaya.Taswaya_Type='اضافة' and supplier_taswaya.Date < '" + dateTimePickerFrom.Value.ToString("yyyy-MM-dd") + "'";
                comand = new MySqlCommand(query, dbconnection);
                dr = comand.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        if (dr["المبلغ"].ToString() != "")
                        {
                            totalTaswyaAdd = Convert.ToDouble(dr["المبلغ"].ToString());
                        }
                    }
                }
                dr.Close();

                query = "SELECT sum(supplier_transitions.Amount) as 'المبلغ' FROM supplier_transitions INNER JOIN supplier ON supplier.Supplier_ID = supplier_transitions.Supplier_ID INNER JOIN bank ON bank.Bank_ID = supplier_transitions.Bank_ID where supplier_transitions.Transition='سداد' and supplier_transitions.Paid='تم' and supplier_transitions.Error=0 and supplier_transitions.Date < '" + dateTimePickerFrom.Value.ToString("yyyy-MM-dd") + "'";
                comand = new MySqlCommand(query, dbconnection);
                dr = comand.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        if (dr["المبلغ"].ToString() != "")
                        {
                            totalTransition = Convert.ToDouble(dr["المبلغ"].ToString());
                        }
                    }
                }
                dr.Close();
                query = "SELECT sum(supplier_return_bill.Total_Price_AD) as 'المبلغ' FROM supplier_return_bill INNER JOIN supplier ON supplier.Supplier_ID = supplier_return_bill.Supplier_ID where Date(supplier_return_bill.Date) < '" + dateTimePickerFrom.Value.ToString("yyyy-MM-dd") + "'";
                comand = new MySqlCommand(query, dbconnection);
                dr = comand.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        if (dr["المبلغ"].ToString() != "")
                        {
                            TotalReturns = Convert.ToDouble(dr["المبلغ"].ToString());
                        }
                    }
                }
                dr.Close();
                query = "SELECT sum(supplier_taswaya.Money_Paid) as 'المبلغ' FROM supplier_taswaya INNER JOIN supplier ON supplier.Supplier_ID = supplier_taswaya.Supplier_ID where supplier_taswaya.Taswaya_Type='خصم' and supplier_taswaya.Date < '" + dateTimePickerFrom.Value.ToString("yyyy-MM-dd") + "'";
                comand = new MySqlCommand(query, dbconnection);
                dr = comand.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        if (dr["المبلغ"].ToString() != "")
                        {
                            totalTaswyaDiscount = Convert.ToDouble(dr["المبلغ"].ToString());
                        }
                    }
                }
                dr.Close();

                int n = dataGridView1.Rows.Add();
                dataGridView1.Rows[n].Cells["Descripe"].Value = "رصيد سابق";
                dataGridView1.Rows[n].Cells["Debit"].Value = totalBills + totalTaswyaAdd;
                dataGridView1.Rows[n].Cells["Credit"].Value = totalTransition + TotalReturns + totalTaswyaDiscount;
                #endregion

                #region others
                totalTransition = totalBills = totalTaswyaAdd = totalTaswyaDiscount = TotalReturns = 0;
                DateTime sdt = dateTimePickerFrom.Value;
                DateTime edt = dateTimePickerTo.Value;
                int numMonths = 0;
                while (sdt <= edt)
                {
                    sdt = sdt.AddMonths(1);
                    numMonths++;
                }

                for (int i = 0; i < numMonths; i++)
                {
                    DateTime today = new DateTime();
                    DateTime start = new DateTime();
                    DateTime end = new DateTime();
                    if (i == 0)
                    {
                        today = dateTimePickerFrom.Value;
                        start = new DateTime(today.Year, today.Month, today.Day);
                        end = start.AddMonths(1);
                    }
                    if (i == (numMonths - 1))
                    {
                        today = dateTimePickerFrom.Value;
                        start = new DateTime(today.Year, today.Month, today.Day);
                        start = start.AddMonths(i);
                        today = dateTimePickerTo.Value;
                        end = new DateTime(today.Year, today.Month, today.Day);
                        end = end.AddDays(1);
                    }
                    else
                    {
                        today = dateTimePickerFrom.Value;
                        start = new DateTime(today.Year, today.Month, today.Day);
                        start = start.AddMonths(i);
                        end = start.AddMonths(1);
                    }

                    query = "SELECT sum(supplier_bill.Total_Price_A) as 'المبلغ' FROM supplier_bill INNER JOIN supplier ON supplier.Supplier_ID = supplier_bill.Supplier_ID where Date(supplier_bill.Date) >= '" + start.ToString("yyyy-MM-dd") + "' and Date(supplier_bill.Date) < '" + end.ToString("yyyy-MM-dd") + "'";
                    comand = new MySqlCommand(query, dbconnection);
                    dr = comand.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            if (dr["المبلغ"].ToString() != "")
                            {
                                totalBills = Convert.ToDouble(dr["المبلغ"].ToString());
                            }
                        }
                    }
                    dr.Close();
                    query = "SELECT sum(supplier_taswaya.Money_Paid) as 'المبلغ' FROM supplier_taswaya INNER JOIN supplier ON supplier.Supplier_ID = supplier_taswaya.Supplier_ID where supplier_taswaya.Taswaya_Type='اضافة' and supplier_taswaya.Date >= '" + start.ToString("yyyy-MM-dd") + "' and supplier_taswaya.Date < '" + end.ToString("yyyy-MM-dd") + "'";
                    comand = new MySqlCommand(query, dbconnection);
                    dr = comand.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            if (dr["المبلغ"].ToString() != "")
                            {
                                totalTaswyaAdd = Convert.ToDouble(dr["المبلغ"].ToString());
                            }
                        }
                    }
                    dr.Close();

                    query = "SELECT sum(supplier_transitions.Amount) as 'المبلغ' FROM supplier_transitions INNER JOIN supplier ON supplier.Supplier_ID = supplier_transitions.Supplier_ID INNER JOIN bank ON bank.Bank_ID = supplier_transitions.Bank_ID where supplier_transitions.Transition='سداد' and supplier_transitions.Paid='تم' and supplier_transitions.Error=0 and supplier_transitions.Date >= '" + start.ToString("yyyy-MM-dd") + "' and supplier_transitions.Date < '" + end.ToString("yyyy-MM-dd") + "'";
                    comand = new MySqlCommand(query, dbconnection);
                    dr = comand.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            if (dr["المبلغ"].ToString() != "")
                            {
                                totalTransition = Convert.ToDouble(dr["المبلغ"].ToString());
                            }
                        }
                    }
                    dr.Close();
                    query = "SELECT sum(supplier_return_bill.Total_Price_AD) as 'المبلغ' FROM supplier_return_bill INNER JOIN supplier ON supplier.Supplier_ID = supplier_return_bill.Supplier_ID where Date(supplier_return_bill.Date) >= '" + start.ToString("yyyy-MM-dd") + "' and Date(supplier_return_bill.Date) < '" + end.ToString("yyyy-MM-dd") + "'";
                    comand = new MySqlCommand(query, dbconnection);
                    dr = comand.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            if (dr["المبلغ"].ToString() != "")
                            {
                                TotalReturns = Convert.ToDouble(dr["المبلغ"].ToString());
                            }
                        }
                    }
                    dr.Close();
                    query = "SELECT sum(supplier_taswaya.Money_Paid) as 'المبلغ' FROM supplier_taswaya INNER JOIN supplier ON supplier.Supplier_ID = supplier_taswaya.Supplier_ID where supplier_taswaya.Taswaya_Type='خصم' and supplier_taswaya.Date >= '" + start.ToString("yyyy-MM-dd") + "' and supplier_taswaya.Date < '" + end.ToString("yyyy-MM-dd") + "'";
                    comand = new MySqlCommand(query, dbconnection);
                    dr = comand.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            if (dr["المبلغ"].ToString() != "")
                            {
                                totalTaswyaDiscount = Convert.ToDouble(dr["المبلغ"].ToString());
                            }
                        }
                    }
                    dr.Close();

                    n = dataGridView1.Rows.Add();
                    dataGridView1.Rows[n].Cells["Descripe"].Value = "بيانات شهر " + start.Month;
                    dataGridView1.Rows[n].Cells["Debit"].Value = "";
                    dataGridView1.Rows[n].Cells["Credit"].Value = "";
                    dataGridView1.Rows[n].DefaultCellStyle.BackColor = Color.LightGray;
                    dataGridView1.Rows[n].DefaultCellStyle.SelectionBackColor = Color.LightSteelBlue;
                    dataGridView1.Rows[n].DefaultCellStyle.SelectionForeColor = Color.Black;

                    n = dataGridView1.Rows.Add();
                    dataGridView1.Rows[n].Cells["Bill"].Value = totalBills;
                    dataGridView1.Rows[n].Cells["TaswyaaAdding"].Value = totalTaswyaAdd;
                    dataGridView1.Rows[n].Cells["ReturnBill"].Value = TotalReturns;
                    dataGridView1.Rows[n].Cells["TaswyaDiscount"].Value = totalTaswyaDiscount;
                    dataGridView1.Rows[n].Cells["Transitions"].Value = totalTransition;
                    dataGridView1.Rows[n].Cells["Debit"].Value = "";
                    dataGridView1.Rows[n].Cells["Credit"].Value = "";

                    n = dataGridView1.Rows.Add();
                    dataGridView1.Rows[n].Cells["Debit"].Value = totalBills + totalTaswyaAdd;
                    dataGridView1.Rows[n].Cells["Credit"].Value = totalTransition + TotalReturns + totalTaswyaDiscount;
                }
                #endregion

                #region Summary
                double totalDebit = 0, totalCredit = 0;
                for (int i = 0; i < dataGridView1.RowCount; i++)
                {
                    if (dataGridView1.Rows[i].Cells["Debit"].Value.ToString() != "")
                    {
                        totalDebit += Convert.ToDouble(dataGridView1.Rows[i].Cells["Debit"].Value.ToString());
                        totalCredit += Convert.ToDouble(dataGridView1.Rows[i].Cells["Credit"].Value.ToString());
                    }
                }

                n = dataGridView1.Rows.Add();
                dataGridView1.Rows[n].Cells["Debit"].Value = "";
                dataGridView1.Rows[n].Cells["Credit"].Value = "";
                dataGridView1.Rows[n].DefaultCellStyle.BackColor = Color.LightGray;
                dataGridView1.Rows[n].DefaultCellStyle.SelectionBackColor = Color.LightSteelBlue;
                dataGridView1.Rows[n].DefaultCellStyle.SelectionForeColor = Color.Black;

                n = dataGridView1.Rows.Add();
                dataGridView1.Rows[n].Cells["Debit"].Value = totalDebit;
                dataGridView1.Rows[n].Cells["Credit"].Value = totalCredit;
                dataGridView1.Rows[n].Cells["Descripe"].Value = "اجمالى العمليات";
                n = dataGridView1.Rows.Add();
                dataGridView1.Rows[n].Cells["Debit"].Value = totalCredit - totalDebit;
                dataGridView1.Rows[n].Cells["Descripe"].Value = "الرصيد";
                #endregion
            }
            else
            {
                #region first row
                string query = "SELECT sum(supplier_bill.Total_Price_A) as 'المبلغ' FROM supplier_bill INNER JOIN supplier ON supplier.Supplier_ID = supplier_bill.Supplier_ID where Date(supplier_bill.Date) < '" + dateTimePickerFrom.Value.ToString("yyyy-MM-dd") + "' and supplier.Supplier_ID=" + supplierId;
                MySqlCommand comand = new MySqlCommand(query, dbconnection);
                MySqlDataReader dr = comand.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        if (dr["المبلغ"].ToString() != "")
                        {
                            totalBills = Convert.ToDouble(dr["المبلغ"].ToString());
                        }
                    }
                }
                dr.Close();
                query = "SELECT sum(supplier_taswaya.Money_Paid) as 'المبلغ' FROM supplier_taswaya INNER JOIN supplier ON supplier.Supplier_ID = supplier_taswaya.Supplier_ID where supplier_taswaya.Taswaya_Type='اضافة' and supplier_taswaya.Date < '" + dateTimePickerFrom.Value.ToString("yyyy-MM-dd") + "' and supplier.Supplier_ID=" + supplierId;
                comand = new MySqlCommand(query, dbconnection);
                dr = comand.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        if (dr["المبلغ"].ToString() != "")
                        {
                            totalTaswyaAdd = Convert.ToDouble(dr["المبلغ"].ToString());
                        }
                    }
                }
                dr.Close();

                query = "SELECT sum(supplier_transitions.Amount) as 'المبلغ' FROM supplier_transitions INNER JOIN supplier ON supplier.Supplier_ID = supplier_transitions.Supplier_ID INNER JOIN bank ON bank.Bank_ID = supplier_transitions.Bank_ID where supplier_transitions.Transition='سداد' and supplier_transitions.Paid='تم' and supplier_transitions.Error=0 and supplier_transitions.Date < '" + dateTimePickerFrom.Value.ToString("yyyy-MM-dd") + "' and supplier.Supplier_ID=" + supplierId;
                comand = new MySqlCommand(query, dbconnection);
                dr = comand.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        if (dr["المبلغ"].ToString() != "")
                        {
                            totalTransition = Convert.ToDouble(dr["المبلغ"].ToString());
                        }
                    }
                }
                dr.Close();
                query = "SELECT sum(supplier_return_bill.Total_Price_AD) as 'المبلغ' FROM supplier_return_bill INNER JOIN supplier ON supplier.Supplier_ID = supplier_return_bill.Supplier_ID where Date(supplier_return_bill.Date) < '" + dateTimePickerFrom.Value.ToString("yyyy-MM-dd") + "' and supplier.Supplier_ID=" + supplierId;
                comand = new MySqlCommand(query, dbconnection);
                dr = comand.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        if (dr["المبلغ"].ToString() != "")
                        {
                            TotalReturns = Convert.ToDouble(dr["المبلغ"].ToString());
                        }
                    }
                }
                dr.Close();
                query = "SELECT sum(supplier_taswaya.Money_Paid) as 'المبلغ' FROM supplier_taswaya INNER JOIN supplier ON supplier.Supplier_ID = supplier_taswaya.Supplier_ID where supplier_taswaya.Taswaya_Type='خصم' and supplier_taswaya.Date < '" + dateTimePickerFrom.Value.ToString("yyyy-MM-dd") + "' and supplier.Supplier_ID=" + supplierId;
                comand = new MySqlCommand(query, dbconnection);
                dr = comand.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        if (dr["المبلغ"].ToString() != "")
                        {
                            totalTaswyaDiscount = Convert.ToDouble(dr["المبلغ"].ToString());
                        }
                    }
                }
                dr.Close();

                int n = dataGridView1.Rows.Add();
                dataGridView1.Rows[n].Cells["Descripe"].Value = "رصيد سابق";
                dataGridView1.Rows[n].Cells["Debit"].Value = totalBills + totalTaswyaAdd;
                dataGridView1.Rows[n].Cells["Credit"].Value = totalTransition + TotalReturns + totalTaswyaDiscount;
                #endregion

                #region others
                totalTransition = totalBills = totalTaswyaAdd = totalTaswyaDiscount = TotalReturns = 0;
                DateTime sdt = dateTimePickerFrom.Value;
                DateTime edt = dateTimePickerTo.Value;
                int numMonths = 0;
                while (sdt <= edt)
                {
                    sdt = sdt.AddMonths(1);
                    numMonths++;
                }

                for (int i = 0; i < numMonths; i++)
                {
                    DateTime today = new DateTime();
                    DateTime start = new DateTime();
                    DateTime end = new DateTime();
                    if (i == 0)
                    {
                        today = dateTimePickerFrom.Value;
                        start = new DateTime(today.Year, today.Month, today.Day);
                        end = start.AddMonths(1);
                    }
                    if (i == (numMonths - 1))
                    {
                        today = dateTimePickerFrom.Value;
                        start = new DateTime(today.Year, today.Month, today.Day);
                        start = start.AddMonths(i);
                        today = dateTimePickerTo.Value;
                        end = new DateTime(today.Year, today.Month, today.Day);
                        end = end.AddDays(1);
                    }
                    else
                    {
                        today = dateTimePickerFrom.Value;
                        start = new DateTime(today.Year, today.Month, today.Day);
                        start = start.AddMonths(i);
                        end = start.AddMonths(1);
                    }

                    query = "SELECT sum(supplier_bill.Total_Price_A) as 'المبلغ' FROM supplier_bill INNER JOIN supplier ON supplier.Supplier_ID = supplier_bill.Supplier_ID where Date(supplier_bill.Date) >= '" + start.ToString("yyyy-MM-dd") + "' and Date(supplier_bill.Date) < '" + end.ToString("yyyy-MM-dd") + "' and supplier.Supplier_ID=" + supplierId;
                    comand = new MySqlCommand(query, dbconnection);
                    dr = comand.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            if (dr["المبلغ"].ToString() != "")
                            {
                                totalBills = Convert.ToDouble(dr["المبلغ"].ToString());
                            }
                        }
                    }
                    dr.Close();
                    query = "SELECT sum(supplier_taswaya.Money_Paid) as 'المبلغ' FROM supplier_taswaya INNER JOIN supplier ON supplier.Supplier_ID = supplier_taswaya.Supplier_ID where supplier_taswaya.Taswaya_Type='اضافة' and supplier_taswaya.Date >= '" + start.ToString("yyyy-MM-dd") + "' and supplier_taswaya.Date < '" + end.ToString("yyyy-MM-dd") + "' and supplier.Supplier_ID=" + supplierId;
                    comand = new MySqlCommand(query, dbconnection);
                    dr = comand.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            if (dr["المبلغ"].ToString() != "")
                            {
                                totalTaswyaAdd = Convert.ToDouble(dr["المبلغ"].ToString());
                            }
                        }
                    }
                    dr.Close();

                    query = "SELECT sum(supplier_transitions.Amount) as 'المبلغ' FROM supplier_transitions INNER JOIN supplier ON supplier.Supplier_ID = supplier_transitions.Supplier_ID INNER JOIN bank ON bank.Bank_ID = supplier_transitions.Bank_ID where supplier_transitions.Transition='سداد' and supplier_transitions.Paid='تم' and supplier_transitions.Error=0 and supplier_transitions.Date >= '" + start.ToString("yyyy-MM-dd") + "' and supplier_transitions.Date < '" + end.ToString("yyyy-MM-dd") + "' and supplier.Supplier_ID=" + supplierId;
                    comand = new MySqlCommand(query, dbconnection);
                    dr = comand.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            if (dr["المبلغ"].ToString() != "")
                            {
                                totalTransition = Convert.ToDouble(dr["المبلغ"].ToString());
                            }
                        }
                    }
                    dr.Close();
                    query = "SELECT sum(supplier_return_bill.Total_Price_AD) as 'المبلغ' FROM supplier_return_bill INNER JOIN supplier ON supplier.Supplier_ID = supplier_return_bill.Supplier_ID where Date(supplier_return_bill.Date) >= '" + start.ToString("yyyy-MM-dd") + "' and Date(supplier_return_bill.Date) < '" + end.ToString("yyyy-MM-dd") + "' and supplier.Supplier_ID=" + supplierId;
                    comand = new MySqlCommand(query, dbconnection);
                    dr = comand.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            if (dr["المبلغ"].ToString() != "")
                            {
                                TotalReturns = Convert.ToDouble(dr["المبلغ"].ToString());
                            }
                        }
                    }
                    dr.Close();
                    query = "SELECT sum(supplier_taswaya.Money_Paid) as 'المبلغ' FROM supplier_taswaya INNER JOIN supplier ON supplier.Supplier_ID = supplier_taswaya.Supplier_ID where supplier_taswaya.Taswaya_Type='خصم' and supplier_taswaya.Date >= '" + start.ToString("yyyy-MM-dd") + "' and supplier_taswaya.Date < '" + end.ToString("yyyy-MM-dd") + "' and supplier.Supplier_ID=" + supplierId;
                    comand = new MySqlCommand(query, dbconnection);
                    dr = comand.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            if (dr["المبلغ"].ToString() != "")
                            {
                                totalTaswyaDiscount = Convert.ToDouble(dr["المبلغ"].ToString());
                            }
                        }
                    }
                    dr.Close();

                    n = dataGridView1.Rows.Add();
                    dataGridView1.Rows[n].Cells["Descripe"].Value = "بيانات شهر " + start.Month;
                    dataGridView1.Rows[n].Cells["Debit"].Value = "";
                    dataGridView1.Rows[n].Cells["Credit"].Value = "";
                    dataGridView1.Rows[n].DefaultCellStyle.BackColor = Color.LightGray;
                    dataGridView1.Rows[n].DefaultCellStyle.SelectionBackColor = Color.LightSteelBlue;
                    dataGridView1.Rows[n].DefaultCellStyle.SelectionForeColor = Color.Black;

                    n = dataGridView1.Rows.Add();
                    dataGridView1.Rows[n].Cells["Bill"].Value = totalBills;
                    dataGridView1.Rows[n].Cells["TaswyaaAdding"].Value = totalTaswyaAdd;
                    dataGridView1.Rows[n].Cells["ReturnBill"].Value = TotalReturns;
                    dataGridView1.Rows[n].Cells["TaswyaDiscount"].Value = totalTaswyaDiscount;
                    dataGridView1.Rows[n].Cells["Transitions"].Value = totalTransition;
                    dataGridView1.Rows[n].Cells["Debit"].Value = "";
                    dataGridView1.Rows[n].Cells["Credit"].Value = "";

                    n = dataGridView1.Rows.Add();
                    dataGridView1.Rows[n].Cells["Debit"].Value = totalBills + totalTaswyaAdd;
                    dataGridView1.Rows[n].Cells["Credit"].Value = totalTransition + TotalReturns + totalTaswyaDiscount;
                }
                #endregion

                #region Summary
                double totalDebit = 0, totalCredit = 0;
                for (int i = 0; i < dataGridView1.RowCount; i++)
                {
                    if (dataGridView1.Rows[i].Cells["Debit"].Value.ToString() != "")
                    {
                        totalDebit += Convert.ToDouble(dataGridView1.Rows[i].Cells["Debit"].Value.ToString());
                        totalCredit += Convert.ToDouble(dataGridView1.Rows[i].Cells["Credit"].Value.ToString());
                    }
                }

                n = dataGridView1.Rows.Add();
                dataGridView1.Rows[n].Cells["Debit"].Value = "";
                dataGridView1.Rows[n].Cells["Credit"].Value = "";
                dataGridView1.Rows[n].DefaultCellStyle.BackColor = Color.LightGray;
                dataGridView1.Rows[n].DefaultCellStyle.SelectionBackColor = Color.LightSteelBlue;
                dataGridView1.Rows[n].DefaultCellStyle.SelectionForeColor = Color.Black;

                n = dataGridView1.Rows.Add();
                dataGridView1.Rows[n].Cells["Debit"].Value = totalDebit;
                dataGridView1.Rows[n].Cells["Credit"].Value = totalCredit;
                dataGridView1.Rows[n].Cells["Descripe"].Value = "اجمالى العمليات";
                n = dataGridView1.Rows.Add();
                dataGridView1.Rows[n].Cells["Debit"].Value = totalCredit - totalDebit;
                dataGridView1.Rows[n].Cells["Descripe"].Value = "الرصيد";
                #endregion
            }
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            try
            {
                List<SupplierBillsTransitions_Items> bi = new List<SupplierBillsTransitions_Items>();
                for (int i = 0; i < dataGridView1.RowCount; i++)
                {
                    string debit = "", credit = "", bill = "", taswyaaAdd = "", returnBill = "", taswyaaSub = "", transitions = "", descripe = "";
                    if (dataGridView1.Rows[i].Cells["Debit"].Value != null)
                    {
                        debit = dataGridView1.Rows[i].Cells["Debit"].Value.ToString();
                    }
                    if (dataGridView1.Rows[i].Cells["Credit"].Value != null)
                    {
                        credit = dataGridView1.Rows[i].Cells["Credit"].Value.ToString();
                    }
                    if (dataGridView1.Rows[i].Cells["Bill"].Value != null)
                    {
                        bill = dataGridView1.Rows[i].Cells["Bill"].Value.ToString();
                    }
                    if (dataGridView1.Rows[i].Cells["TaswyaaAdding"].Value != null)
                    {
                        taswyaaAdd = dataGridView1.Rows[i].Cells["TaswyaaAdding"].Value.ToString();
                    }
                    if (dataGridView1.Rows[i].Cells["ReturnBill"].Value != null)
                    {
                        returnBill = dataGridView1.Rows[i].Cells["ReturnBill"].Value.ToString();
                    }
                    if (dataGridView1.Rows[i].Cells["TaswyaDiscount"].Value != null)
                    {
                        taswyaaSub = dataGridView1.Rows[i].Cells["TaswyaDiscount"].Value.ToString();
                    }
                    if (dataGridView1.Rows[i].Cells["Transitions"].Value != null)
                    {
                        transitions = dataGridView1.Rows[i].Cells["Transitions"].Value.ToString();
                    }
                    if (dataGridView1.Rows[i].Cells["Descripe"].Value != null)
                    {
                        descripe = dataGridView1.Rows[i].Cells["Descripe"].Value.ToString();
                    }
                    SupplierBillsTransitions_Items item = new SupplierBillsTransitions_Items() { debit = debit, credit = credit, BillNum = bill, addingTaswyaa = taswyaaAdd, ReturnBillNum = returnBill, SubTaswyaa = taswyaaSub, Transitions = transitions, Info = descripe };
                    bi.Add(item);
                }
                Report_SupplierBillsTransitions f = new Report_SupplierBillsTransitions();
                f.PrintInvoice(comSupplier.Text, dateTimePickerFrom.Value.Date.ToString(), dateTimePickerTo.Value.Date.ToString(), bi);
                f.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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

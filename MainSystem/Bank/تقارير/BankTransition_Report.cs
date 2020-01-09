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
    public partial class BankTransition_Report : Form
    {
        MySqlConnection dbconnection;
        MainForm mainForm;

        public BankTransition_Report(MainForm mainform)
        {
            InitializeComponent();
            dbconnection = new MySqlConnection(connection.connectionString);
            mainForm = mainform;
        }

        private void BankTransition_Report_Load(object sender, EventArgs e)
        {
            try
            {
                dbconnection.Open();
                string query = "select * from bank where Bank_Type='خزينة مصروفات' or Bank_Type='خزينة'";
                MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                DataTable dt = new DataTable();
                da.Fill(dt);
                comSafe.DataSource = dt;
                comSafe.DisplayMember = dt.Columns["Bank_Name"].ToString();
                comSafe.ValueMember = dt.Columns["Bank_ID"].ToString();
                comSafe.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }
        
        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (comSafe.SelectedValue != null)
                {
                    double totalSales = 0;
                    double totalReturned = 0;

                    dbconnection.Open();
                    string q1 = "SELECT sum(transitions.Amount) as 'المبلغ' FROM transitions where transitions.Transition='ايداع' and transitions.Bank_ID=" + comSafe.SelectedValue.ToString() + " and date(transitions.Date) between '" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "' and '" + dateTimePicker2.Value.ToString("yyyy-MM-dd") + "' and Error=0 group by transitions.Bank_ID";
                    MySqlCommand c1 = new MySqlCommand(q1, dbconnection);
                    MySqlDataReader dr1 = c1.ExecuteReader();
                    if (dr1.HasRows)
                    {
                        while (dr1.Read())
                        {
                            totalSales += Convert.ToDouble(dr1["المبلغ"].ToString());
                            txtSales.Text = dr1["المبلغ"].ToString();
                        }
                    }
                    else
                    {
                        txtSales.Text = "0";
                    }
                    dr1.Close();

                    q1 = "SELECT sum(transitions.Amount) as 'المبلغ' FROM transitions where transitions.Transition='سحب' and transitions.Bank_ID=" + comSafe.SelectedValue.ToString() + " and date(transitions.Date) between '" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "' and '" + dateTimePicker2.Value.ToString("yyyy-MM-dd") + "' and Error=0 group by transitions.Bank_ID";
                    c1 = new MySqlCommand(q1, dbconnection);
                    dr1 = c1.ExecuteReader();
                    if (dr1.HasRows)
                    {
                        while (dr1.Read())
                        {
                            totalReturned += Convert.ToDouble(dr1["المبلغ"].ToString());
                            txtReturned.Text = dr1["المبلغ"].ToString();
                        }
                    }
                    else
                    {
                        txtReturned.Text = "0";
                    }
                    dr1.Close();

                    q1 = "SELECT sum(expense_transition.Amount) as 'المبلغ' FROM expense_transition where expense_transition.Type='ايداع' and expense_transition.Bank_ID=" + comSafe.SelectedValue.ToString() + " and date(expense_transition.Date) between '" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "' and '" + dateTimePicker2.Value.ToString("yyyy-MM-dd") + "' and Error=0 group by expense_transition.Bank_ID";
                    c1 = new MySqlCommand(q1, dbconnection);
                    dr1 = c1.ExecuteReader();
                    if (dr1.HasRows)
                    {
                        while (dr1.Read())
                        {
                            totalSales += Convert.ToDouble(dr1["المبلغ"].ToString());
                            txtIncomeExpenses.Text = dr1["المبلغ"].ToString();
                        }
                    }
                    else
                    {
                        txtIncomeExpenses.Text = "0";
                    }
                    dr1.Close();

                    q1 = "SELECT sum(expense_transition.Amount) as 'المبلغ' FROM expense_transition where expense_transition.Type='صرف' and expense_transition.Bank_ID=" + comSafe.SelectedValue.ToString() + " and date(expense_transition.Date) between '" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "' and '" + dateTimePicker2.Value.ToString("yyyy-MM-dd") + "' and Error=0 group by expense_transition.Bank_ID";
                    c1 = new MySqlCommand(q1, dbconnection);
                    dr1 = c1.ExecuteReader();
                    if (dr1.HasRows)
                    {
                        while (dr1.Read())
                        {
                            totalReturned += Convert.ToDouble(dr1["المبلغ"].ToString());
                            txtExpenses.Text = dr1["المبلغ"].ToString();
                        }
                    }
                    else
                    {
                        txtExpenses.Text = "0";
                    }
                    dr1.Close();

                    q1 = "SELECT sum(bank_transfer.Money) as 'المبلغ' FROM bank_transfer where bank_transfer.ToBank_ID=" + comSafe.SelectedValue.ToString() + " and date(bank_transfer.Date) between '" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "' and '" + dateTimePicker2.Value.ToString("yyyy-MM-dd") + "' and Error=0 group by bank_transfer.ToBank_ID";
                    c1 = new MySqlCommand(q1, dbconnection);
                    dr1 = c1.ExecuteReader();
                    if (dr1.HasRows)
                    {
                        while (dr1.Read())
                        {
                            if (dr1["ToBank_ID"].ToString() == comSafe.SelectedValue.ToString())
                            {
                                totalSales += Convert.ToDouble(dr1["المبلغ"].ToString());
                                txtTransferTo.Text = dr1["المبلغ"].ToString();
                            }
                        }
                    }
                    else
                    {
                        txtTransferTo.Text = "0";
                    }
                    dr1.Close();

                    q1 = "SELECT sum(bank_transfer.Money) as 'المبلغ' FROM bank_transfer where bank_transfer.FromBank_ID=" + comSafe.SelectedValue.ToString() + " and date(bank_transfer.Date) between '" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "' and '" + dateTimePicker2.Value.ToString("yyyy-MM-dd") + "' and Error=0 group by bank_transfer.FromBank_ID";
                    c1 = new MySqlCommand(q1, dbconnection);
                    dr1 = c1.ExecuteReader();
                    if (dr1.HasRows)
                    {
                        while (dr1.Read())
                        {
                            if (dr1["FromBank_ID"].ToString() == comSafe.SelectedValue.ToString())
                            {
                                totalReturned += Convert.ToDouble(dr1["المبلغ"].ToString());
                                txtTransferFrom.Text = dr1["المبلغ"].ToString();
                            }
                        }
                    }
                    else
                    {
                        txtTransferFrom.Text = "0";
                    }
                    dr1.Close();

                    txtTotalAdd.Text = totalSales.ToString();
                    txtTotalSub.Text = totalReturned.ToString();
                    txtSafy.Text = (totalSales - totalReturned).ToString();
                }
                else
                {
                    txtSales.Text = "0";
                    txtReturned.Text = "0";
                    txtIncomeExpenses.Text = "0";
                    txtExpenses.Text = "0";
                    txtTransferTo.Text = "0";
                    txtTransferFrom.Text = "0";
                    txtTotalAdd.Text = "0";
                    txtTotalSub.Text = "0";
                    txtSafy.Text = "0";
                    MessageBox.Show("يجب اختيار الخزينة");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            try
            {
                if (comSafe.SelectedValue != null && comSafe.Text != "" && txtSales.Text != "" && txtReturned.Text != "" && txtIncomeExpenses.Text != "" && txtExpenses.Text != "" && txtTransferFrom.Text != "" && txtTransferTo.Text != "" && txtTotalAdd.Text != "" && txtTotalSub.Text != "" && txtSafy.Text != "")
                {
                    Print_BankTransition_Report f = new Print_BankTransition_Report();
                    f.PrintInvoice(comSafe.Text, dateTimePicker1.Value.Date, dateTimePicker2.Value.Date, txtSales.Text, txtReturned.Text, txtIncomeExpenses.Text, txtExpenses.Text, txtTransferTo.Text, txtTransferFrom.Text, txtTotalAdd.Text, txtTotalSub.Text, txtSafy.Text);
                    f.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }
    }
}

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

namespace TaxesSystem
{
    public partial class BillTransitionCopy_Report : Form
    {
        MySqlConnection conn, conn2;
        MainForm mainForm;
        int transitionId = 0;
        int[] arrRestMoney;
        int[] arrPaidMoney;
        string confirmEmp = "";
        string username = "";

        public BillTransitionCopy_Report(MainForm mainform)
        {
            InitializeComponent();
            conn = new MySqlConnection(connection.connectionString);
            conn2 = new MySqlConnection(connection.connectionString);
            mainForm = mainform;
            arrPaidMoney = new int[9];
            arrRestMoney = new int[9];
        }

        private void txtTransitionId_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    if (int.TryParse(txtTransitionId.Text, out transitionId))
                    {
                        conn.Open();
                        string q1 = "SELECT * FROM transitions where transitions.Transition_ID=" + transitionId + " and Error=0";
                        MySqlCommand c1 = new MySqlCommand(q1, conn);
                        MySqlDataReader dr1 = c1.ExecuteReader();
                        if (dr1.HasRows)
                        {
                            while (dr1.Read())
                            {
                                txtOperation.Text = dr1["Transition"].ToString();
                                txtType.Text = dr1["Type"].ToString();
                                txtDate.Text = Convert.ToDateTime(dr1["Date"].ToString()).ToString("yyyy-MM-dd HH:mm:ss");
                                txtTransitionBranch.Text = dr1["TransitionBranch_Name"].ToString();
                                if (dr1["Branch_ID"].ToString() != "")
                                {
                                    txtBranch.Text = dr1["Branch_Name"].ToString()/* + " " + dr1["Branch_ID"].ToString()*/;
                                }
                                else
                                {
                                    txtBranch.Text = "";
                                }
                                if (dr1["Bill_Number"].ToString() != "")
                                {
                                    txtBillNum.Text = dr1["Bill_Number"].ToString();
                                }
                                else
                                {
                                    txtBillNum.Text = "";
                                }

                                if (dr1["Client_Name"].ToString() != "")
                                {
                                    txtClient.Text = dr1["Client_Name"].ToString() + " " + dr1["Client_ID"].ToString();
                                }
                                else
                                {
                                    txtClient.Text = dr1["Customer_Name"].ToString() + " " + dr1["Customer_ID"].ToString();
                                }
                                /*if (dr1["Client_ID"].ToString() != "")
                                {
                                    conn2.Open();
                                    string q = "select Customer_Name from customer where Customer_ID=" + dr1["Client_ID"].ToString();
                                    MySqlCommand com = new MySqlCommand(q, conn2);
                                    if (com.ExecuteScalar() != null)
                                    {
                                        txtClient.Text = com.ExecuteScalar().ToString() + " " + dr1["Client_ID"].ToString();
                                    }
                                    conn2.Close();
                                }
                                else if (dr1["Customer_ID"].ToString() != "")
                                {
                                    conn2.Open();
                                    string q = "select Customer_Name from customer where Customer_ID=" + dr1["Customer_ID"].ToString();
                                    MySqlCommand com = new MySqlCommand(q, conn2);
                                    if (com.ExecuteScalar() != null)
                                    {
                                        txtClient.Text = com.ExecuteScalar().ToString() + " " + dr1["Customer_ID"].ToString();
                                    }
                                    conn2.Close();
                                }
                                else
                                {
                                    txtClient.Text = "";
                                }*/

                                if (dr1["Transition"].ToString() == "ايداع" && dr1["Type"].ToString() == "كاش")
                                {
                                    conn2.Open();
                                    string q = "select DATE_FORMAT(customer_bill.Bill_Date,'%Y-%m-%d') as date,customer_bill.Employee_Name from customer_bill where Branch_BillNumber=" + dr1["Bill_Number"].ToString() + " and Branch_ID=" + dr1["Branch_ID"].ToString();
                                    MySqlCommand com = new MySqlCommand(q, conn2);
                                    MySqlDataReader dr = com.ExecuteReader();
                                    while(dr.Read())
                                    {
                                        txtBillDate.Text = dr["date"].ToString();
                                        confirmEmp = dr["Employee_Name"].ToString();
                                    }
                                    dr.Close();
                                    conn2.Close();
                                }
                                else if (dr1["Transition"].ToString() == "سحب" && dr1["Type"].ToString() == "كاش")
                                {
                                    conn2.Open();
                                    string q = "select DATE_FORMAT(customer_return_bill.Date,'%Y-%m-%d') as date,customer_return_bill.Employee_Name from customer_return_bill where Branch_BillNumber=" + dr1["Bill_Number"].ToString() + " and Branch_ID=" + dr1["Branch_ID"].ToString();
                                    MySqlCommand com = new MySqlCommand(q, conn2);
                                    MySqlDataReader dr = com.ExecuteReader();
                                    while (dr.Read())
                                    {
                                        txtBillDate.Text = dr["date"].ToString();
                                        confirmEmp = dr["Employee_Name"].ToString();
                                    }
                                    dr.Close();
                                    conn2.Close();
                                }
                                else
                                {
                                    txtBillDate.Text = "";
                                    confirmEmp = "";
                                }

                                txtMoney.Text = dr1["Amount"].ToString();
                                txtInformation.Text = dr1["Data"].ToString();
                                txtPaymentMethod.Text = dr1["Payment_Method"].ToString();
                                txtBank.Text = dr1["Bank_Name"].ToString()/* + " " + dr1["Bank_ID"].ToString()*/;
                                txtCheckNum.Text = dr1["Check_Number"].ToString();
                                txtCheckDate.Text = dr1["Payday"].ToString();
                                txtOperationNum.Text = dr1["Operation_Number"].ToString();
                                txtDelegate.Text = dr1["Delegate_Name"].ToString();
                                username = dr1["Employee_Name"].ToString();

                                /*conn2.Open();
                                string qt = "SELECT users.User_Name FROM transitions INNER JOIN users ON transitions.Employee_ID = users.User_ID where transitions.Transition_ID=" + transitionId;
                                MySqlCommand ct = new MySqlCommand(qt, conn2);
                                if (ct.ExecuteScalar() != null)
                                {
                                    username = ct.ExecuteScalar().ToString();
                                }
                                else
                                {
                                    username = "";
                                }
                                conn2.Close();*/
                            }
                            dr1.Close();
                        }
                        else
                        {
                            txtOperation.Text = "";
                            txtType.Text = "";
                            txtDate.Text = "";
                            txtBranch.Text = "";
                            txtBillNum.Text = "";
                            txtClient.Text = "";
                            txtBillDate.Text = "";
                            txtMoney.Text = "";
                            txtInformation.Text = "";
                            txtPaymentMethod.Text = "";
                            txtBank.Text = "";
                            txtCheckNum.Text = "";
                            txtCheckDate.Text = "";
                            txtOperationNum.Text = "";
                            txtDelegate.Text = "";
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                conn.Close();
                conn2.Close();
            }
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtTransitionId.Text != "" && txtOperation.Text != "" && txtType.Text != "")
                {
                    conn.Open();
                    string query = "SELECT * FROM transition_categories_money where transition_categories_money.Transition_ID=" + transitionId;
                    MySqlCommand com = new MySqlCommand(query, conn);
                    MySqlDataReader dr = com.ExecuteReader();
                    while(dr.Read())
                    {
                        if (dr["a200"].ToString() != "")
                        { arrPaidMoney[0] = Convert.ToInt32(dr["a200"].ToString()); }
                        else
                        { arrPaidMoney[0] = 0; }

                        if (dr["a100"].ToString() != "")
                        { arrPaidMoney[1] = Convert.ToInt32(dr["a100"].ToString()); }
                        else
                        { arrPaidMoney[1] = 0; }

                        if (dr["a50"].ToString() != "")
                        { arrPaidMoney[2] = Convert.ToInt32(dr["a50"].ToString()); }
                        else
                        { arrPaidMoney[2] = 0; }

                        if (dr["a20"].ToString() != "")
                        { arrPaidMoney[3] = Convert.ToInt32(dr["a20"].ToString()); }
                        else
                        { arrPaidMoney[3] = 0; }

                        if (dr["a10"].ToString() != "")
                        { arrPaidMoney[4] = Convert.ToInt32(dr["a10"].ToString()); }
                        else
                        { arrPaidMoney[4] = 0; }

                        if (dr["a5"].ToString() != "")
                        { arrPaidMoney[5] = Convert.ToInt32(dr["a5"].ToString()); }
                        else
                        { arrPaidMoney[5] = 0; }

                        if (dr["a1"].ToString() != "")
                        { arrPaidMoney[6] = Convert.ToInt32(dr["a1"].ToString()); }
                        else
                        { arrPaidMoney[6] = 0; }

                        if (dr["aH"].ToString() != "")
                        { arrPaidMoney[7] = Convert.ToInt32(dr["aH"].ToString()); }
                        else
                        { arrPaidMoney[7] = 0; }

                        if (dr["aQ"].ToString() != "")
                        { arrPaidMoney[8] = Convert.ToInt32(dr["aQ"].ToString()); }
                        else
                        { arrPaidMoney[8] = 0; }
                        ///////////////////////////////////////////////////////
                        if (dr["r200"].ToString() != "")
                        { arrRestMoney[0] = Convert.ToInt32(dr["r200"].ToString()); }
                        else
                        { arrRestMoney[0] = 0; }

                        if (dr["r100"].ToString() != "")
                        { arrRestMoney[1] = Convert.ToInt32(dr["r100"].ToString()); }
                        else
                        { arrRestMoney[1] = 0; }

                        if (dr["r50"].ToString() != "")
                        { arrRestMoney[2] = Convert.ToInt32(dr["r50"].ToString()); }
                        else
                        { arrRestMoney[2] = 0; }

                        if (dr["r20"].ToString() != "")
                        { arrRestMoney[3] = Convert.ToInt32(dr["r20"].ToString()); }
                        else
                        { arrRestMoney[3] = 0; }

                        if (dr["r10"].ToString() != "")
                        { arrRestMoney[4] = Convert.ToInt32(dr["r10"].ToString()); }
                        else
                        { arrRestMoney[4] = 0; }

                        if (dr["r5"].ToString() != "")
                        { arrRestMoney[5] = Convert.ToInt32(dr["r5"].ToString()); }
                        else
                        { arrRestMoney[5] = 0; }

                        if (dr["r1"].ToString() != "")
                        { arrRestMoney[6] = Convert.ToInt32(dr["r1"].ToString()); }
                        else
                        { arrRestMoney[6] = 0; }

                        if (dr["rH"].ToString() != "")
                        { arrRestMoney[7] = Convert.ToInt32(dr["rH"].ToString()); }
                        else
                        { arrRestMoney[7] = 0; }

                        if (dr["rQ"].ToString() != "")
                        { arrRestMoney[8] = Convert.ToInt32(dr["rQ"].ToString()); }
                        else
                        { arrRestMoney[8] = 0; }
                    }
                    dr.Close();
                    conn.Close();
                    if (txtOperation.Text == "ايداع" && txtType.Text == "كاش")
                    {
                        PrintCopy_CategoriesBill_Report f = new PrintCopy_CategoriesBill_Report();
                        f.PrintInvoice(Convert.ToDateTime(txtDate.Text), transitionId.ToString(), txtTransitionBranch.Text, txtBranch.Text, Convert.ToInt32(txtBillNum.Text), txtClient.Text, Convert.ToDateTime(txtBillDate.Text), Convert.ToDouble(txtMoney.Text), txtPaymentMethod.Text, txtBank.Text, txtCheckNum.Text, txtCheckDate.Text, txtOperationNum.Text, txtInformation.Text, confirmEmp, username, arrPaidMoney[0], arrPaidMoney[1], arrPaidMoney[2], arrPaidMoney[3], arrPaidMoney[4], arrPaidMoney[5], arrPaidMoney[6], arrPaidMoney[7], arrPaidMoney[8], arrRestMoney[0], arrRestMoney[1], arrRestMoney[2], arrRestMoney[3], arrRestMoney[4], arrRestMoney[5], arrRestMoney[6], arrRestMoney[7], arrRestMoney[8]);
                        f.ShowDialog();
                    }
                    else if (txtOperation.Text == "ايداع" && txtType.Text == "آجل")
                    {
                        PrintCopy_AglCategoriesBill_Report f = new PrintCopy_AglCategoriesBill_Report();
                        f.PrintInvoice(Convert.ToDateTime(txtDate.Text), transitionId.ToString(), txtTransitionBranch.Text, txtClient.Text, Convert.ToDouble(txtMoney.Text), txtPaymentMethod.Text, txtBank.Text, txtCheckNum.Text, txtCheckDate.Text, txtOperationNum.Text, txtInformation.Text, username, txtDelegate.Text, arrPaidMoney[0], arrPaidMoney[1], arrPaidMoney[2], arrPaidMoney[3], arrPaidMoney[4], arrPaidMoney[5], arrPaidMoney[6], arrPaidMoney[7], arrPaidMoney[8], arrRestMoney[0], arrRestMoney[1], arrRestMoney[2], arrRestMoney[3], arrRestMoney[4], arrRestMoney[5], arrRestMoney[6], arrRestMoney[7], arrRestMoney[8]);
                        f.ShowDialog();
                    }
                    else if (txtOperation.Text == "سحب" && txtType.Text == "كاش")
                    {
                        PrintCopy_ReturnedCategoriesBill_Report f = new PrintCopy_ReturnedCategoriesBill_Report();
                        f.PrintInvoice(Convert.ToDateTime(txtDate.Text), transitionId.ToString(), txtTransitionBranch.Text, txtBranch.Text, Convert.ToInt32(txtBillNum.Text), txtClient.Text, Convert.ToDateTime(txtBillDate.Text), Convert.ToDouble(txtMoney.Text), txtPaymentMethod.Text, txtBank.Text, txtCheckNum.Text, txtCheckDate.Text, txtOperationNum.Text, txtInformation.Text, confirmEmp, username, arrPaidMoney[0], arrPaidMoney[1], arrPaidMoney[2], arrPaidMoney[3], arrPaidMoney[4], arrPaidMoney[5], arrPaidMoney[6], arrPaidMoney[7], arrPaidMoney[8], arrRestMoney[0], arrRestMoney[1], arrRestMoney[2], arrRestMoney[3], arrRestMoney[4], arrRestMoney[5], arrRestMoney[6], arrRestMoney[7], arrRestMoney[8]);
                        f.ShowDialog();
                    }
                    else if (txtOperation.Text == "سحب" && txtType.Text == "آجل")
                    {
                        PrintCopy_ReturnedAglCategoriesBill_Report f = new PrintCopy_ReturnedAglCategoriesBill_Report();
                        f.PrintInvoice(Convert.ToDateTime(txtDate.Text), transitionId.ToString(), txtTransitionBranch.Text, txtClient.Text, Convert.ToDouble(txtMoney.Text), txtPaymentMethod.Text, txtBank.Text, txtCheckNum.Text, txtCheckDate.Text, txtOperationNum.Text, txtInformation.Text, username, txtDelegate.Text, arrPaidMoney[0], arrPaidMoney[1], arrPaidMoney[2], arrPaidMoney[3], arrPaidMoney[4], arrPaidMoney[5], arrPaidMoney[6], arrPaidMoney[7], arrPaidMoney[8], arrRestMoney[0], arrRestMoney[1], arrRestMoney[2], arrRestMoney[3], arrRestMoney[4], arrRestMoney[5], arrRestMoney[6], arrRestMoney[7], arrRestMoney[8]);
                        f.ShowDialog();
                    }
                    for (int i = 0; i < arrPaidMoney.Length; i++)
                        arrPaidMoney[i] = arrRestMoney[i] = 0;
                    for (int i = 0; i < arrRestMoney.Length; i++)
                        arrRestMoney[i] = arrPaidMoney[i] = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            conn.Close();
        }

        //clear function
        public void clear()
        {
            foreach (Control co in this.panel1.Controls)
            {
                //if (co is GroupBox)
                //{
                foreach (Control item in co.Controls)
                {
                    if (item is ComboBox)
                    {
                        item.Text = "";
                    }
                    else if (item is TextBox)
                    {
                        item.Text = "";
                    }
                }
                //}
            }
        }

        public bool IsClear()
        {
            bool flag5 = false;
            foreach (Control co in this.panel1.Controls)
            {
                foreach (Control item in co.Controls)
                {
                    if (item is ComboBox)
                    {
                        if (item.Text == "")
                            flag5 = true;
                        else
                            return false;
                    }
                    else if (item is TextBox)
                    {
                        if (item.Text == "")
                            flag5 = true;
                        else
                            return false;
                    }
                }
            }

            return flag5;
        }
    }
}

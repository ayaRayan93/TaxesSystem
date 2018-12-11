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
    public partial class BillTransitionCopy_Report : Form
    {
        MySqlConnection conn, conn2;
        MainForm mainForm;
        int transitionId = 0;

        public BillTransitionCopy_Report(MainForm mainform)
        {
            InitializeComponent();
            conn = new MySqlConnection(connection.connectionString);
            conn2 = new MySqlConnection(connection.connectionString);
            mainForm = mainform;
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
                        while (dr1.Read())
                        {
                            txtOperation.Text = dr1["Transition"].ToString();
                            txtType.Text = dr1["Type"].ToString();
                            txtBranch.Text = dr1["Branch_Name"].ToString() + " " + dr1["Branch_ID"].ToString();
                            txtBillNum.Text = dr1["Bill_Number"].ToString();

                            if (dr1["Client_ID"].ToString() != "")
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

                            if (dr1["Transition"].ToString() == "ايداع" && dr1["Type"].ToString() == "كاش")
                            {
                                conn2.Open();
                                string q = "select DATE_FORMAT(Bill_Date,'%Y-%m-%d') from customer_bill where Branch_BillNumber=" + dr1["Bill_Number"].ToString() + " and Branch_ID=" + dr1["Branch_ID"].ToString();
                                MySqlCommand com = new MySqlCommand(q, conn2);
                                if (com.ExecuteScalar() != null)
                                {
                                    txtBillDate.Text = com.ExecuteScalar().ToString();
                                }
                                conn2.Close();
                            }
                            else if (dr1["Transition"].ToString() == "سحب" && dr1["Type"].ToString() == "كاش")
                            {
                                conn2.Open();
                                string q = "select DATE_FORMAT(Date,'%Y-%m-%d') from customer_return_bill where Branch_BillNumber=" + dr1["Bill_Number"].ToString() + " and Branch_ID=" + dr1["Branch_ID"].ToString();
                                MySqlCommand com = new MySqlCommand(q, conn2);
                                if (com.ExecuteScalar() != null)
                                {
                                    txtBillDate.Text = com.ExecuteScalar().ToString();
                                }
                                conn2.Close();
                            }
                            else
                            {
                                txtBillDate.Text = "";
                            }

                            txtMoney.Text = dr1["Amount"].ToString();
                            txtInformation.Text = dr1["Data"].ToString();
                            txtStatus.Text = dr1["Payment_Method"].ToString();
                            txtBank.Text = dr1["Bank_Name"].ToString()+" "+ dr1["Bank_ID"].ToString();
                            txtCheckNum.Text = dr1["Check_Number"].ToString();
                            txtCheckDate.Text = dr1["Payday"].ToString();
                            txtCartType.Text = dr1["Visa_Type"].ToString();
                            txtOperationNum.Text = dr1["Operation_Number"].ToString();
                        }
                        dr1.Close();
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

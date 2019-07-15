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

namespace MainSystem
{
    public partial class AddSpecialOrder : DevExpress.XtraEditors.XtraForm
    {
        MySqlConnection dbconnection;
        //int branchBillNumber = 0;
        int DashBillNum = 0;
        //int EmpBranchId = 0;
        int DelegateId = 0;
        int ClientId = 0;

        public AddSpecialOrder(int dashBillNum, int clientId/*, int empBranchId*/, int delegateId)
        {
            InitializeComponent();
            dbconnection = new MySqlConnection(connection.connectionString);
            DashBillNum = dashBillNum;
            ClientId = clientId;
            //EmpBranchId = empBranchId;
            DelegateId = delegateId;
        }

        private void textBox_Click(object sender, EventArgs e)
        {
            openOnScreenKeyboard();
        }

        private void textBox_Leave(object sender, EventArgs e)
        {
            killOnScreenKeyboard();
        }

        private static void openOnScreenKeyboard()
        {
            System.Diagnostics.Process.Start("C:\\Program Files\\Common Files\\Microsoft shared\\ink\\TabTip.exe");

        }
        private static void killOnScreenKeyboard()
        {
            if (System.Diagnostics.Process.GetProcessesByName("TabTip").Count() > 0)
            {
                System.Diagnostics.Process asd = System.Diagnostics.Process.GetProcessesByName("TabTip").First();
                asd.Kill();
            }

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtDescription.Text != "")
                {
                    int SpecialOrderID = 0;
                    string cutomerType = "";

                    dbconnection.Open();
                    string query = "select Customer_Type from customer where Customer_ID=" + ClientId;
                    MySqlCommand com = new MySqlCommand(query, dbconnection);
                    if (com.ExecuteScalar() != null)
                    {
                        cutomerType = com.ExecuteScalar().ToString();
                    }

                    query = "insert into special_order (Description,Dash_ID,Delegate_ID,Client_ID,Customer_ID) values(@Description,@Dash_ID,@Delegate_ID,@Client_ID,@Customer_ID)";
                    com = new MySqlCommand(query, dbconnection);
                    com.Parameters.Add("@Description", MySqlDbType.VarChar, 255).Value = txtDescription.Text;
                    com.Parameters.Add("@Dash_ID", MySqlDbType.Int16, 11).Value = DashBillNum;
                    com.Parameters.Add("@Delegate_ID", MySqlDbType.Int16);
                    com.Parameters["@Delegate_ID"].Value = DelegateId;
                    if (cutomerType == "عميل")
                    {
                        com.Parameters.Add("@Client_ID", MySqlDbType.Int16);
                        com.Parameters["@Client_ID"].Value = ClientId;
                        com.Parameters.Add("@Customer_ID", MySqlDbType.Int16);
                        com.Parameters["@Customer_ID"].Value = null;
                    }
                    else
                    {
                        com.Parameters.Add("@Client_ID", MySqlDbType.Int16);
                        com.Parameters["@Client_ID"].Value = null;
                        com.Parameters.Add("@Customer_ID", MySqlDbType.Int16);
                        com.Parameters["@Customer_ID"].Value = ClientId;
                    }
                    com.ExecuteNonQuery();

                    query = "select SpecialOrder_ID from special_order order by SpecialOrder_ID desc limit 1";
                    com = new MySqlCommand(query, dbconnection);
                    if (com.ExecuteScalar() != null)
                    {
                        SpecialOrderID = Convert.ToInt32(com.ExecuteScalar().ToString());
                    }

                    /*insertRequest();*/
                    dbconnection.Close();
                    MessageBox.Show("طلب رقم : " + SpecialOrderID.ToString());
                    this.Close();
                }
                else
                {
                    MessageBox.Show("يجب ادخال التفاصيل");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }

        //to get request number and give it to the client
        //error function
        public void insertRequest()
        {
            /*int SpecialOrderID = 0;

            string query = "select BranchBillNumber from requests where Branch_ID=" + EmpBranchId + " order by Request_ID desc limit 1";
            MySqlCommand com = new MySqlCommand(query, dbconnection);
            if (com.ExecuteScalar() != null)
            {
                branchBillNumber = Convert.ToInt32(com.ExecuteScalar()) + 1;
            }
            else
            {
                branchBillNumber = 1;
            }

            query = "select SpecialOrder_ID from special_order order by SpecialOrder_ID desc limit 1";
            com = new MySqlCommand(query, dbconnection);
            if (com.ExecuteScalar() != null)
            {
                SpecialOrderID = Convert.ToInt32(com.ExecuteScalar());
            }

            query = "select Branch_Name from Branch where Branch_ID=" + EmpBranchId;
            com = new MySqlCommand(query, dbconnection);
            string BranchName = com.ExecuteScalar().ToString();

            //,Client_ID  ,@Client_ID
            query = "insert into requests (Branch_ID,Branch_Name,BranchBillNumber,SpecialOrder_ID,Delegate_ID) values (@Branch_ID,@Branch_Name,@BranchBillNumber,@SpecialOrder_ID,@Delegate_ID)";
            com = new MySqlCommand(query, dbconnection);
            com.Parameters.Add("@Branch_ID", MySqlDbType.Int16);
            com.Parameters["@Branch_ID"].Value = EmpBranchId;
            com.Parameters.Add("@Branch_Name", MySqlDbType.VarChar);
            com.Parameters["@Branch_Name"].Value = BranchName;
            com.Parameters.Add("@BranchBillNumber", MySqlDbType.Int16);
            com.Parameters["@BranchBillNumber"].Value = branchBillNumber;
            com.Parameters.Add("@SpecialOrder_ID", MySqlDbType.Int16);
            com.Parameters["@SpecialOrder_ID"].Value = SpecialOrderID;
            com.Parameters.Add("@Delegate_ID", MySqlDbType.Int16);
            com.Parameters["@Delegate_ID"].Value = DelegateId;
            com.ExecuteNonQuery();*/
        }
    }
}
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
    public partial class SafeExpense_Record : Form
    {
        MySqlConnection dbconnection;
        bool successFlag = false;
        bool flag = false;
        //int branchID = 0;
        int ID = -1;
        private string PaymentMethod;
        int[] arrOFPhaat; //count of each catagory value of money in store
        int[] arrRestMoney;
        int[] arrPaidMoney;
        bool loaded = false;
        XtraTabPage xtraTabPage;
        XtraTabControl tabControlBank;
        bool flagCategoriesSuccess = false;
        int transitionbranchID = 0;

        public SafeExpense_Record(BankPullExpense_Report form, XtraTabControl MainTabControlBank)
        {
            InitializeComponent();
            dbconnection = new MySqlConnection(connection.connectionString);
            arrOFPhaat = new int[9];
            arrPaidMoney = new int[9];
            arrRestMoney = new int[9];
            tabControlBank = MainTabControlBank;

            cmbBank.AutoCompleteMode = AutoCompleteMode.Suggest;
            cmbBank.AutoCompleteSource = AutoCompleteSource.ListItems;

            cmbExpenseType.AutoCompleteMode = AutoCompleteMode.Suggest;
            cmbExpenseType.AutoCompleteSource = AutoCompleteSource.ListItems;

            this.dateEdit1.Properties.DisplayFormat.FormatString = "yyyy/MM/dd";
            this.dateEdit1.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dateEdit1.Properties.EditFormat.FormatString = "yyyy/MM/dd";
            this.dateEdit1.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dateEdit1.Properties.Mask.EditMask = "yyyy/MM/dd";
        }

        private void BankPullExpense_Record_Load(object sender, EventArgs e)
        {
            try
            {
                if (!loaded)
                {
                    transitionbranchID = UserControl.EmpBranchID;
                    loadBranch();
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
                bool check = false;
                if (PaymentMethod == "نقدى")
                {
                    check = (cmbExpenseType.Text != "" && cmbBank.Text != "" && txtPullMoney.Text != "");
                }
                else if (PaymentMethod == "شيك")
                {
                    check = (cmbExpenseType.Text != "" && cmbBank.Text != "" && txtPullMoney.Text != "" && dateEdit1.Text != "" && txtCheckNumber.Text != "");
                }
                else if (PaymentMethod == "حساب بنكى")
                {
                    check = (cmbExpenseType.Text != "" && cmbBank.Text != "" && txtPullMoney.Text != "" && dateEdit1.Text != "" && txtCheckNumber.Text != "");
                }

                if (check)
                {
                    if (!flagCategoriesSuccess)
                    {
                        if (MessageBox.Show("لم يتم ادخال الفئات..هل تريد الاستمرار؟", "تنبية", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                        {
                            return;
                        }
                    }

                    double outParse;
                    if (double.TryParse(txtPullMoney.Text, out outParse))
                    {
                        string opNumString = null;
                        if (txtOperationNumber.Text != "")
                        {
                            int OpNum = 0;
                            if (int.TryParse(txtOperationNumber.Text, out OpNum))
                            {
                                opNumString = txtOperationNumber.Text;
                            }
                            else
                            {
                                MessageBox.Show("رقم العملية يجب ان يكون عدد");
                                dbconnection.Close();
                                return;
                            }
                        }


                        RecordExpense();
                        if (successFlag == false)
                        {
                            MessageBox.Show("حدث خطأ اثناء التنفيذ");
                            dbconnection.Close();
                            return;
                        }

                        dbconnection.Open();
                        string query = "insert into transitions (TransitionBranch_ID,Client_Name,Client_ID,Transition,Payment_Method,Bank_ID,Bank_Name,Date,Amount,Data,PayDay,Check_Number,Visa_Type,Operation_Number,Bill_Number,Type,Employee_ID,Error) values(@TransitionBranch_ID,@Client_Name,@Client_ID,@Transition,@Payment_Method,@Bank_ID,@Bank_Name,@Date,@Amount,@Data,@PayDay,@Check_Number,@Visa_Type,@Operation_Number,@Bill_Number,@Type,@Employee_ID,@Error)";
                        MySqlCommand com = new MySqlCommand(query, dbconnection);

                        com.Parameters.Add("@Transition", MySqlDbType.VarChar, 255).Value = "سحب";
                        com.Parameters.Add("@Type", MySqlDbType.VarChar, 255).Value = "مصروف";
                        com.Parameters.Add("@TransitionBranch_ID", MySqlDbType.Int16, 11).Value = transitionbranchID;
                        //com.Parameters.Add("@Branch_ID", MySqlDbType.Int16, 11).Value = cmbBranch.SelectedValue;
                        //com.Parameters.Add("@Branch_Name", MySqlDbType.VarChar, 255).Value = cmbBranch.Text;
                        com.Parameters.Add("@Bill_Number", MySqlDbType.Int16, 11).Value = ID;
                        com.Parameters.Add("@Payment_Method", MySqlDbType.VarChar, 255).Value = PaymentMethod;
                        com.Parameters.Add("@Bank_ID", MySqlDbType.Int16, 11).Value = cmbBank.SelectedValue;
                        com.Parameters.Add("@Bank_Name", MySqlDbType.VarChar, 255).Value = cmbBank.Text;
                        com.Parameters.Add("@Date", MySqlDbType.DateTime, 0).Value = DateTime.Now;
                        com.Parameters.Add("@Client_ID", MySqlDbType.Int16, 11).Value = null;
                        com.Parameters.Add("@Client_Name", MySqlDbType.VarChar, 255).Value = txtClient.Text;
                        com.Parameters.Add("@Operation_Number", MySqlDbType.Int16, 11).Value = opNumString;
                        com.Parameters.Add("@Data", MySqlDbType.VarChar, 255).Value = txtDescrip.Text;
                        com.Parameters.Add("@Error", MySqlDbType.Int16, 11).Value = 0;

                        com.Parameters.Add("@Amount", MySqlDbType.Decimal, 10).Value = txtPullMoney.Text;
                        MySqlCommand com2 = new MySqlCommand("select Bank_Stock from bank where Bank_ID=" + cmbBank.SelectedValue, dbconnection);
                        double amount2 = Convert.ToDouble(com2.ExecuteScalar().ToString());
                        amount2 -= outParse;
                        MySqlCommand com3 = new MySqlCommand("update bank set Bank_Stock=" + amount2 + " where Bank_ID=" + cmbBank.SelectedValue, dbconnection);
                        com3.ExecuteNonQuery();
                        
                        if (txtVisaType.Text != "")
                        {
                            com.Parameters.Add("@Visa_Type", MySqlDbType.VarChar, 255).Value = txtVisaType.Text;
                        }
                        else
                        {
                            com.Parameters.Add("@Visa_Type", MySqlDbType.VarChar, 255).Value = null;
                        }

                        if (dateEdit1.Text != "")
                        {
                            com.Parameters.Add("@PayDay", MySqlDbType.Date, 0).Value = dateEdit1.DateTime.Date;
                        }
                        else
                        {
                            com.Parameters.Add("@PayDay", MySqlDbType.Date, 0).Value = null;
                        }

                        if (txtCheckNumber.Text != "")
                        {
                            com.Parameters.Add("@Check_Number", MySqlDbType.VarChar, 255).Value = txtCheckNumber.Text;
                        }
                        else
                        {
                            com.Parameters.Add("@Check_Number", MySqlDbType.VarChar, 255).Value = null;
                        }
                        com.Parameters.Add("@Employee_ID", MySqlDbType.Int16);
                        com.Parameters["@Employee_ID"].Value = UserControl.EmpID;

                        com.ExecuteNonQuery();

                        //////////record adding/////////////
                        query = "select Transition_ID from transitions order by Transition_ID desc limit 1";
                        com = new MySqlCommand(query, dbconnection);
                        string TransitionID = com.ExecuteScalar().ToString();

                        query = "insert into usercontrol (UserControl_UserID,UserControl_TableName,UserControl_Status,UserControl_RecordID,UserControl_Date,UserControl_Reason) values(@UserControl_UserID,@UserControl_TableName,@UserControl_Status,@UserControl_RecordID,@UserControl_Date,@UserControl_Reason)";
                        com = new MySqlCommand(query, dbconnection);
                        com.Parameters.Add("@UserControl_UserID", MySqlDbType.Int16, 11).Value = UserControl.userID;
                        com.Parameters.Add("@UserControl_TableName", MySqlDbType.VarChar, 255).Value = "transitions";
                        com.Parameters.Add("@UserControl_Status", MySqlDbType.VarChar, 255).Value = "اضافة";
                        com.Parameters.Add("@UserControl_RecordID", MySqlDbType.VarChar, 255).Value = TransitionID;
                        com.Parameters.Add("@UserControl_Date", MySqlDbType.DateTime, 0).Value = DateTime.Now;
                        com.Parameters.Add("@UserControl_Reason", MySqlDbType.VarChar, 255).Value = null;
                        com.ExecuteNonQuery();
                        //////////////////////

                        query = "insert into transition_categories_money (a200,a100,a50,a20,a10,a5,a1,aH,aQ,r200,r100,r50,r20,r10,r5,r1,rH,rQ,Transition_ID) values(@a200,@a100,@a50,@a20,@a10,@a5,@a1,@aH,@aQ,@r200,@r100,@r50,@r20,@r10,@r5,@r1,@rH,@rQ,@Transition_ID)";
                        com = new MySqlCommand(query, dbconnection);
                        com.Parameters.Add("@a200", MySqlDbType.Int16, 11).Value = arrPaidMoney[0];
                        com.Parameters.Add("@a100", MySqlDbType.Int16, 11).Value = arrPaidMoney[1];
                        com.Parameters.Add("@a50", MySqlDbType.Int16, 11).Value = arrPaidMoney[2];
                        com.Parameters.Add("@a20", MySqlDbType.Int16, 11).Value = arrPaidMoney[3];
                        com.Parameters.Add("@a10", MySqlDbType.Int16, 11).Value = arrPaidMoney[4];
                        com.Parameters.Add("@a5", MySqlDbType.Int16, 11).Value = arrPaidMoney[5];
                        com.Parameters.Add("@a1", MySqlDbType.Int16, 11).Value = arrPaidMoney[6];
                        com.Parameters.Add("@aH", MySqlDbType.Int16, 11).Value = arrPaidMoney[7];
                        com.Parameters.Add("@aQ", MySqlDbType.Int16, 11).Value = arrPaidMoney[8];
                        com.Parameters.Add("@r200", MySqlDbType.Int16, 11).Value = arrRestMoney[0];
                        com.Parameters.Add("@r100", MySqlDbType.Int16, 11).Value = arrRestMoney[1];
                        com.Parameters.Add("@r50", MySqlDbType.Int16, 11).Value = arrRestMoney[2];
                        com.Parameters.Add("@r20", MySqlDbType.Int16, 11).Value = arrRestMoney[3];
                        com.Parameters.Add("@r10", MySqlDbType.Int16, 11).Value = arrRestMoney[4];
                        com.Parameters.Add("@r5", MySqlDbType.Int16, 11).Value = arrRestMoney[5];
                        com.Parameters.Add("@r1", MySqlDbType.Int16, 11).Value = arrRestMoney[6];
                        com.Parameters.Add("@rH", MySqlDbType.Int16, 11).Value = arrRestMoney[7];
                        com.Parameters.Add("@rQ", MySqlDbType.Int16, 11).Value = arrRestMoney[8];
                        com.Parameters.Add("@Transition_ID", MySqlDbType.Int16, 11).Value = Convert.ToInt32(TransitionID);
                        com.ExecuteNonQuery();
                        flagCategoriesSuccess = false;
                        
                        dbconnection.Close();
                        clear();

                        for (int i = 0; i < arrPaidMoney.Length; i++)
                            arrPaidMoney[i] = arrRestMoney[i] = 0;
                        for (int i = 0; i < arrRestMoney.Length; i++)
                            arrRestMoney[i] = arrPaidMoney[i] = 0;

                        xtraTabPage.ImageOptions.Image = null;
                    }
                    else
                    {
                        MessageBox.Show("المبلغ المدفوع يجب ان يكون عدد");
                        dbconnection.Close();
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("برجاء ادخال جميع البيانات المطلوبة");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }
        
        private void txtBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (loaded)
                {
                    xtraTabPage = getTabPage("اضافة مرتد-مصروف");
                    if (!IsClear())
                    {
                        xtraTabPage.ImageOptions.Image = Properties.Resources.unsave;
                    }
                    else
                    {
                        xtraTabPage.ImageOptions.Image = null;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //clear function
        public void clear()
        {
            foreach (Control co in this.panel1.Controls)
            {
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
            }
        }

        public XtraTabPage getTabPage(string text)
        {
            for (int i = 0; i < tabControlBank.TabPages.Count; i++)
                if (tabControlBank.TabPages[i].Text == text)
                {
                    return tabControlBank.TabPages[i];
                }
            return null;
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

        //functions
        private void loadBranch()
        {
            dbconnection.Open();
            
            string query = "select * from expense_type";
            MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
            DataTable dt = new DataTable();
            da.Fill(dt);
            cmbExpenseType.DataSource = dt;
            cmbExpenseType.DisplayMember = dt.Columns["Type"].ToString();
            cmbExpenseType.ValueMember = dt.Columns["ID"].ToString();
            cmbExpenseType.SelectedIndex = -1;

            loaded = true;
            dbconnection.Close();
        }

        void RecordExpense()
        {
            dbconnection.Open();
            string query = "insert into expenses (ExpenseType_ID,Expense_Type,Error) values (@ExpenseType_ID,@Expense_Type,@Error)";
            MySqlCommand com = new MySqlCommand(query, dbconnection);
            if (cmbExpenseType.SelectedValue != null)
            {
                com.Parameters.Add("@ExpenseType_ID", MySqlDbType.Int16, 11).Value = cmbExpenseType.SelectedValue;
            }
            else
            {
                int ExpenseType_ID = 0;
                string q = "select ID from expense_type where Type='" + cmbExpenseType.Text + "'";
                MySqlCommand comand = new MySqlCommand(q, dbconnection);
                if (comand.ExecuteScalar() == null)
                {
                    q = "insert into expense_type (Type) values (@Type)";
                    comand = new MySqlCommand(q, dbconnection);
                    comand.Parameters.Add("@Type", MySqlDbType.VarChar, 255).Value = cmbExpenseType.Text;
                    comand.ExecuteNonQuery();

                    q = "select ID from expense_type order by ID desc limit 1";
                    comand = new MySqlCommand(q, dbconnection);
                    ExpenseType_ID = Convert.ToInt32(comand.ExecuteScalar().ToString());
                }
                else
                {
                    cmbExpenseType.SelectedValue = comand.ExecuteScalar();
                    ExpenseType_ID = Convert.ToInt32(cmbExpenseType.SelectedValue.ToString());
                }

                com.Parameters.Add("@ExpenseType_ID", MySqlDbType.Int16, 11).Value = ExpenseType_ID;
            }
            com.Parameters.Add("@Expense_Type", MySqlDbType.VarChar, 255).Value = cmbExpenseType.Text;
            com.Parameters.Add("@Error", MySqlDbType.Int16, 11).Value = 0;
            com.ExecuteNonQuery();

            query = "select Expense_ID from expense order by Expense_ID desc limit 1";
            com = new MySqlCommand(query, dbconnection);
            ID = Convert.ToInt32(com.ExecuteScalar().ToString());

            //////////record adding/////////////
            query = "insert into usercontrol (UserControl_UserID,UserControl_TableName,UserControl_Status,UserControl_RecordID,UserControl_Date,UserControl_Reason) values(@UserControl_UserID,@UserControl_TableName,@UserControl_Status,@UserControl_RecordID,@UserControl_Date,@UserControl_Reason)";
            com = new MySqlCommand(query, dbconnection);
            com.Parameters.Add("@UserControl_UserID", MySqlDbType.Int16, 11).Value = UserControl.userID;
            com.Parameters.Add("@UserControl_TableName", MySqlDbType.VarChar, 255).Value = "expense";
            com.Parameters.Add("@UserControl_Status", MySqlDbType.VarChar, 255).Value = "اضافة";
            com.Parameters.Add("@UserControl_RecordID", MySqlDbType.VarChar, 255).Value = ID;
            com.Parameters.Add("@UserControl_Date", MySqlDbType.DateTime, 0).Value = DateTime.Now;
            com.Parameters.Add("@UserControl_Reason", MySqlDbType.VarChar, 255).Value = null;
            com.ExecuteNonQuery();
            ////////////////////////////////////
            dbconnection.Close();
            successFlag = true;
        }
    }
}

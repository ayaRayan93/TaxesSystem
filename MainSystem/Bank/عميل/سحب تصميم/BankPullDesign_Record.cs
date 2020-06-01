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
    public partial class BankPullDesign_Record : Form
    {
        MySqlConnection dbconnection, dbconnectionr;
        bool flag = false;
        string PaymentMethod = "";
        int[] arrOFPhaat; //count of each catagory value of money in store
        int[] arrRestMoney;
        int[] arrPaidMoney;
        bool loadedPayType = false;
        string TransitionID = "";
        bool flagCategoriesSuccess = false;
        XtraTabControl tabControlBank;
        int transitionbranchID = 0;
        List<DesignItem> listDesignItems;

        public BankPullDesign_Record()
        {
            InitializeComponent();
            dbconnection = new MySqlConnection(connection.connectionString);
            dbconnectionr = new MySqlConnection(connection.connectionString);
            arrOFPhaat = new int[9];
            arrPaidMoney = new int[9];
            arrRestMoney = new int[9];
            listDesignItems = new List<DesignItem>();
            if (UserControl.userType == 1)
            {
                cmbBank.AutoCompleteMode = AutoCompleteMode.Suggest;
                cmbBank.AutoCompleteSource = AutoCompleteSource.ListItems;
            }
            else
            {
                cmbBank.Enabled = false;
                cmbBank.DropDownStyle = ComboBoxStyle.DropDownList;
            }
            comClient.AutoCompleteMode = AutoCompleteMode.Suggest;
            comClient.AutoCompleteSource = AutoCompleteSource.ListItems;
            comEng.AutoCompleteMode = AutoCompleteMode.Suggest;
            comEng.AutoCompleteSource = AutoCompleteSource.ListItems;

            this.dateEdit1.Properties.DisplayFormat.FormatString = "yyyy/MM/dd";
            this.dateEdit1.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dateEdit1.Properties.EditFormat.FormatString = "yyyy/MM/dd";
            this.dateEdit1.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dateEdit1.Properties.Mask.EditMask = "yyyy/MM/dd";
        }
        private void BankDepositAgl_Record_Load(object sender, EventArgs e)
        {
            try
            {
                transitionbranchID = UserControl.EmpBranchID;
                dbconnection.Open();             
                comBranchName.Text = UserControl.EmpBranchName;
                string query = "select * from delegate where Branch_ID=" + transitionbranchID;
                MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                DataTable dt = new DataTable();
                da.Fill(dt);
                comDelegate.DataSource = dt;
                comDelegate.DisplayMember = dt.Columns["Delegate_Name"].ToString();
                comDelegate.ValueMember = dt.Columns["Delegate_ID"].ToString();
                comDelegate.Text = "";               
                panelContent.Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }
        private void txtDesignNum_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    dbconnection.Open();
                    string query = "select * from customer_design where CustomerDesign_ID=" + txtDesignNum.Text;
                    MySqlCommand com = new MySqlCommand(query, dbconnection);
                    MySqlDataReader dr = com.ExecuteReader();
                    while (dr.Read())
                    {
                        string designstatus = dr["Design_Status"].ToString();
                        if (designstatus == "ايداع تصميم")
                        {
                            btnAdd.Enabled = true;
                            comEng.Text = dr["Customer_Name"].ToString();
                            txtCustomerID.Text = dr["Customer_ID"].ToString();
                            
                            comClient.Text = dr["Client_Name"].ToString();
                            txtClientID.Text = dr["Client_ID"].ToString();

                            comDelegate.Text = dr["Delegate_Name"].ToString();
                            comDelegate.SelectedValue = dr["Delegate_ID"].ToString();

                            txtPaidMoney.Text = dr["PaidMoney"].ToString();

                            btnAdd.LabelText = "حفظ";
                            panel3.Visible = true;
                            panel4.Visible = true;
                            panelContent.Visible = true;
                            setDesignDetails(Convert.ToInt16(txtDesignNum.Text));
                        }
                        else
                        {
                            if (dr["Branch_ID"].ToString() != null)
                            {
                                DialogResult dialogResult = MessageBox.Show("  ?هذا التصميم تم سحبه من قبل هل تريد تعديل رقم الفاتورة ", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                if (dialogResult == DialogResult.Yes)
                                {
                                    btnAdd.Enabled = true;
                                    btnAdd.LabelText = "حفظ التعديل";
                                    panel3.Visible = false;
                                    panel4.Visible = false;
                                    panelContent.Visible = false;
                                }
                                else
                                {
                                    clear();
                                }
                            }
                        }
                    }
                    dr.Close();
                    panelContent.Visible = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }
        //private void checkBox_CheckedChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        CheckBox ch = (CheckBox)sender;
        //        switch (ch.Text)
        //        {
        //            case "حمام":
        //                if (ch.Checked)
        //                {
        //                    txtNoItemBath.Enabled = true;
        //                    txtCostBath.Enabled = true;
        //                    txtNoItemBath.Focus();
        //                    DesignItem designItem = new DesignItem();
        //                    designItem.DesignLocation = "حمام";
        //                    listDesignItems.Add(designItem);
        //                }
        //                else
        //                {
        //                    txtNoItemBath.Enabled = false;
        //                    txtNoItemBath.Text = "";
        //                    txtCostBath.Enabled = false;
        //                    txtCostBath.Text = "";
        //                    txtTotalBath.Text = "0";

        //                    calTotal();

        //                    DesignItem designItem = getDesignItem("حمام");
        //                    listDesignItems.Remove(designItem);
        //                }
        //                break;
        //            case "مطبخ":
        //                if (ch.Checked)
        //                {
        //                    txtNoItemKitchen.Enabled = true;
        //                    txtCostKitchen.Enabled = true;
        //                    txtNoItemKitchen.Focus();
        //                    DesignItem designItem = new DesignItem();
        //                    designItem.DesignLocation = "مطبخ";
        //                    listDesignItems.Add(designItem);
        //                }
        //                else
        //                {
        //                    txtNoItemKitchen.Enabled = false;
        //                    txtNoItemKitchen.Text = "";
        //                    txtCostKitchen.Enabled = false;
        //                    txtCostKitchen.Text = "";
        //                    txtTotalKitchen.Text = "0";

        //                    calTotal();

        //                    DesignItem designItem = getDesignItem("مطبخ");
        //                    listDesignItems.Remove(designItem);
        //                }
        //                break;
        //            case "صالة":
        //                if (ch.Checked)
        //                {
        //                    txtNoItemHall.Enabled = true;
        //                    txtCostHall.Enabled = true;
        //                    txtNoItemHall.Focus();
        //                    DesignItem designItem = new DesignItem();
        //                    designItem.DesignLocation = "صالة";
        //                    listDesignItems.Add(designItem);
        //                }
        //                else
        //                {
        //                    txtNoItemHall.Enabled = false;
        //                    txtNoItemHall.Text = "";
        //                    txtCostHall.Enabled = false;
        //                    txtCostHall.Text = "";
        //                    txtTotalHall.Text = "0";

        //                    calTotal();

        //                    DesignItem designItem = getDesignItem("صالة");
        //                    listDesignItems.Remove(designItem);
        //                }
        //                break;
        //            case "غرفة":
        //                if (ch.Checked)
        //                {
        //                    txtNoItemRoom.Enabled = true;
        //                    txtCostRoom.Enabled = true;
        //                    txtNoItemRoom.Focus();
        //                    DesignItem designItem = new DesignItem();
        //                    designItem.DesignLocation = "غرفة";
        //                    listDesignItems.Add(designItem);
        //                }
        //                else
        //                {
        //                    txtNoItemRoom.Enabled = false;
        //                    txtNoItemRoom.Text = "";
        //                    txtCostRoom.Enabled = false;
        //                    txtCostRoom.Text = "";
        //                    txtTotalRoom.Text = "0";

        //                    calTotal();

        //                    DesignItem designItem = getDesignItem("غرفة");
        //                    listDesignItems.Remove(designItem);
        //                }
        //                break;
        //            case "اخري":
        //                if (ch.Checked)
        //                {
        //                    txtNoItemOther.Enabled = true;
        //                    txtCostOther.Enabled = true;
        //                    txtNoItemOther.Focus();
        //                    DesignItem designItem = new DesignItem();
        //                    designItem.DesignLocation = "اخري";
        //                    listDesignItems.Add(designItem);
        //                }
        //                else
        //                {
        //                    txtNoItemOther.Enabled = false;
        //                    txtNoItemOther.Text = "";
        //                    txtCostOther.Enabled = false;
        //                    txtCostOther.Text = "";
        //                    txtTotalOther.Text = "0";

        //                    calTotal();

        //                    DesignItem designItem = getDesignItem("اخري");
        //                    listDesignItems.Remove(designItem);
        //                }
        //                break;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
        //}
        private void txtNoItem_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    int re = 0;
                    TextBox txtbox = (TextBox)sender;
                    switch (txtbox.Name)
                    {
                        case "txtNoItemBath":

                            if (!int.TryParse(txtNoItemBath.Text, out re))
                            {
                                MessageBox.Show("ادخل قيمة صحيحة");

                                txtNoItemBath.Text = "";
                                txtNoItemBath.Focus();
                            }
                            else
                            {
                                txtCostBath.Focus();
                                DesignItem designItem = getDesignItem("حمام");
                                designItem.NoItems = re;
                            }
                            break;
                        case "txtNoItemKitchen":

                            if (!int.TryParse(txtNoItemKitchen.Text, out re))
                            {
                                MessageBox.Show("ادخل قيمة صحيحة");

                                txtNoItemKitchen.Text = "";
                                txtNoItemKitchen.Focus();
                            }
                            else
                            {
                                txtCostKitchen.Focus();
                                DesignItem designItem = getDesignItem("مطبخ");
                                designItem.NoItems = re;
                            }
                            break;
                        case "txtNoItemHall":

                            if (!int.TryParse(txtNoItemHall.Text, out re))
                            {
                                MessageBox.Show("ادخل قيمة صحيحة");

                                txtNoItemHall.Text = "";
                                txtNoItemHall.Focus();
                            }
                            else
                            {
                                txtCostHall.Focus();
                                DesignItem designItem = getDesignItem("صالة");
                                designItem.NoItems = re;
                            }
                            break;
                        case "txtNoItemRoom":

                            if (!int.TryParse(txtNoItemRoom.Text, out re))
                            {
                                MessageBox.Show("ادخل قيمة صحيحة");

                                txtNoItemRoom.Text = "";
                                txtNoItemRoom.Focus();
                            }
                            else
                            {
                                txtCostRoom.Focus();
                                DesignItem designItem = getDesignItem("غرفة");
                                designItem.NoItems = re;
                            }
                            break;
                        case "txtNoItemOther":

                            if (!int.TryParse(txtNoItemOther.Text, out re))
                            {
                                MessageBox.Show("ادخل قيمة صحيحة");

                                txtNoItemOther.Text = "";
                                txtNoItemOther.Focus();
                            }
                            else
                            {
                                txtCostOther.Focus();
                                DesignItem designItem = getDesignItem("اخري");
                                designItem.NoItems = re;
                            }
                            break;
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void txtCost_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    double re = 0;
                    double totalrow = 0;
                    double total = 0;
                    TextBox txtbox = (TextBox)sender;
                    switch (txtbox.Name)
                    {
                        case "txtCostBath":

                            if (!double.TryParse(txtCostBath.Text, out re))
                            {
                                MessageBox.Show("ادخل قيمة صحيحة");

                                txtCostBath.Text = "";
                                txtCostBath.Focus();
                            }
                            else
                            {
                                txtTotalBath.Text = (re * (Convert.ToInt16(txtNoItemBath.Text))).ToString();
                                totalrow = Convert.ToDouble(txtTotalBath.Text);
                                total = Convert.ToDouble(txtTotal.Text) + totalrow;
                                calTotal();
                                DesignItem designItem = getDesignItem("حمام");
                                designItem.ItemCost = re;
                                designItem.Total = re * (Convert.ToInt16(txtNoItemBath.Text));
                            }
                            break;
                        case "txtCostKitchen":

                            if (!double.TryParse(txtCostKitchen.Text, out re))
                            {
                                MessageBox.Show("ادخل قيمة صحيحة");

                                txtNoItemKitchen.Text = "";
                                txtNoItemKitchen.Focus();
                            }
                            else
                            {
                                txtTotalKitchen.Text = (re * (Convert.ToInt16(txtNoItemKitchen.Text))).ToString();
                                totalrow = Convert.ToDouble(txtTotalKitchen.Text);
                                total = Convert.ToDouble(txtTotal.Text) + totalrow;
                                calTotal();
                                DesignItem designItem = getDesignItem("مطبخ");
                                designItem.ItemCost = re;
                                designItem.Total = re * (Convert.ToInt16(txtNoItemKitchen.Text));
                            }
                            break;
                        case "txtCostHall":

                            if (!double.TryParse(txtCostHall.Text, out re))
                            {
                                MessageBox.Show("ادخل قيمة صحيحة");

                                txtNoItemHall.Text = "";
                                txtNoItemHall.Focus();
                            }
                            else
                            {
                                txtTotalHall.Text = (re * (Convert.ToInt16(txtNoItemHall.Text))).ToString();
                                totalrow = Convert.ToDouble(txtTotalHall.Text);
                                total = Convert.ToDouble(txtTotal.Text) + totalrow;
                                calTotal();
                                DesignItem designItem = getDesignItem("صالة");
                                designItem.ItemCost = re;
                                designItem.Total = re * (Convert.ToInt16(txtNoItemHall.Text));
                            }
                            break;
                        case "txtCostRoom":

                            if (!double.TryParse(txtCostRoom.Text, out re))
                            {
                                MessageBox.Show("ادخل قيمة صحيحة");

                                txtNoItemRoom.Text = "";
                                txtNoItemRoom.Focus();
                            }
                            else
                            {
                                txtTotalRoom.Text = (re * (Convert.ToInt16(txtNoItemRoom.Text))).ToString();
                                totalrow = Convert.ToDouble(txtTotalRoom.Text);
                                total = Convert.ToDouble(txtTotal.Text) + totalrow;
                                calTotal();
                                DesignItem designItem = getDesignItem("غرفة");
                                designItem.ItemCost = re;
                                designItem.Total = re * (Convert.ToInt16(txtNoItemRoom.Text));
                            }
                            break;
                        case "txtCostOther":

                            if (!double.TryParse(txtCostOther.Text, out re))
                            {
                                MessageBox.Show("ادخل قيمة صحيحة");

                                txtNoItemOther.Text = "";
                                txtNoItemOther.Focus();
                            }
                            else
                            {
                                txtTotalOther.Text = (re * (Convert.ToInt16(txtNoItemOther.Text))).ToString();
                                totalrow = Convert.ToDouble(txtTotalOther.Text);
                                total = Convert.ToDouble(txtTotal.Text) + totalrow;
                                calTotal();
                                DesignItem designItem = getDesignItem("اخري");
                                designItem.ItemCost = re;
                                designItem.Total = (re * (Convert.ToInt16(txtNoItemOther.Text)));
                            }
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void radioButtonSafe_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                RadioButton r = (RadioButton)sender;
                radCash.Enabled = true;
                radCash.Checked = false;
                radCredit.Enabled = true;
                radCredit.Checked = false;
                radBankAccount.Enabled = false;
                radBankAccount.Checked = false;
                //radVisa.Enabled = false;
                //radVisa.Checked = false;

                ///////////////////////
                labType.Text = "خزينة";
                panelContent.Visible = false;
                /////////////////////////////////////

                //radCash.Checked = true;

                if (UserControl.userType == 1)
                {
                    cmbBank.Enabled = true;
                    cmbBank.AutoCompleteMode = AutoCompleteMode.Suggest;
                    cmbBank.AutoCompleteSource = AutoCompleteSource.ListItems;
                }
                else
                {
                    cmbBank.Enabled = false;
                    cmbBank.AutoCompleteMode = AutoCompleteMode.None;
                    cmbBank.DropDownStyle = ComboBoxStyle.DropDownList;
                }

                string query = "select * from bank inner join bank_main on bank.MainBank_ID=bank_main.MainBank_ID where Branch_ID=" + transitionbranchID + " and MainBank_Type='خزينة'";
                MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                DataTable dt = new DataTable();
                da.Fill(dt);
                cmbBank.DataSource = dt;
                cmbBank.DisplayMember = dt.Columns["Bank_Name"].ToString();
                cmbBank.ValueMember = dt.Columns["Bank_ID"].ToString();
                if (UserControl.userType == 1)
                {
                    cmbBank.SelectedIndex = -1;
                }
                else
                {
                    dbconnection.Open();
                    string q = "SELECT bank.Bank_Name,bank_employee.Bank_ID FROM bank_employee INNER JOIN bank ON bank.Bank_ID = bank_employee.Bank_ID where bank_employee.Employee_ID=" + UserControl.EmpID;
                    MySqlCommand com = new MySqlCommand(q, dbconnection);
                    MySqlDataReader dr = com.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            cmbBank.Text = dr["Bank_Name"].ToString();
                            cmbBank.SelectedValue = dr["Bank_ID"].ToString();
                        }
                        dr.Close();
                    }
                    else
                    {
                        cmbBank.SelectedIndex = -1;
                    }
                }
                loadedPayType = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }
        private void radCash_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                RadioButton r = (RadioButton)sender;
                PaymentMethod = r.Text;

                txtCheckNumber.Visible = false;
                labDate.Visible = false;
                dateEdit1.Visible = false;
                labelchekNumber.Visible = false;
                //labelOperationNumber.Visible = false;

                cmbBank.Visible = true;
                labType.Visible = true;
                txtPaidMoney.Visible = true;
                labelPaidMoney.Visible = true;
                txtDescrip.Visible = true;
                labelDescrip.Visible = true;
                loadedPayType = true;
                groupBox1.Enabled = true;
                panelContent.Visible = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void radCredit_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                RadioButton r = (RadioButton)sender;
                PaymentMethod = r.Text;

                txtCheckNumber.Visible = true;
                labDate.Text = "تاريخ الاستحقاق";
                labDate.Visible = true;
                dateEdit1.Visible = true;
                labelchekNumber.Text = "رقم الشيك";
                labelchekNumber.Visible = true;
                groupBox1.Enabled = false;
                cmbBank.Visible = true;
                labType.Visible = true;
                txtPaidMoney.Visible = true;
                labelPaidMoney.Visible = true;
                txtDescrip.Visible = true;
                labelDescrip.Visible = true;
                loadedPayType = true;
                //labelOperationNumber.Visible = false;
                panelContent.Visible = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void radioButtonBank_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                RadioButton r = (RadioButton)sender;
                radCash.Enabled = false;
                radCash.Checked = false;
                radCredit.Enabled = false;
                radCredit.Checked = false;
                //radVisa.Enabled = true;
                //radVisa.Checked = false;
                radBankAccount.Enabled = true;
                radBankAccount.Checked = false;

                cmbBank.Enabled = true;
                cmbBank.AutoCompleteSource = AutoCompleteSource.ListItems;
                cmbBank.AutoCompleteMode = AutoCompleteMode.Suggest;

                ///////////////////////
                panelContent.Visible = false;
                labType.Text = "بنك";

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void radBankAccount_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                RadioButton r = (RadioButton)sender;
                PaymentMethod = r.Text;

                ////////////////////////////////
                //labelOperationNumber.Visible = false;
                labDate.Text = "تاريخ الايداع";
                labelchekNumber.Visible = false;
                txtCheckNumber.Visible = false;

                string query = "select concat(MainBank_Name,' ',Bank_Name) as Bank_Name,Bank_ID from bank inner join bank_main on bank.MainBank_ID=bank_main.MainBank_ID where MainBank_Type = 'حساب بنكى'";
                MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                DataTable dt = new DataTable();
                da.Fill(dt);
                cmbBank.DataSource = dt;
                cmbBank.DisplayMember = dt.Columns["Bank_Name"].ToString();
                cmbBank.ValueMember = dt.Columns["Bank_ID"].ToString();
                cmbBank.SelectedIndex = -1;


                panelContent.Visible = true;
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
                if (txtBranchBillNumber.Text != "")
                  {
                    if (CheckBillNumber())
                    {
                        bool check = false;
                        if (PaymentMethod == "نقدى")
                        {
                            check = (comDelegate.Text != "" && (comClient.Text != "" || comEng.Text != "")/*&& txtRestMoney.Text != "" && cmbBranch.Text != ""*/ && cmbBank.Text != "" && txtPaidMoney.Text != "");
                        }
                        else if (PaymentMethod == "شيك")
                        {
                            check = (comDelegate.Text != "" && (comClient.Text != "" || comEng.Text != "") /*&& txtRestMoney.Text != "" && cmbBranch.Text != ""*/ && cmbBank.Text != "" && txtPaidMoney.Text != "" && dateEdit1.Text != "" && txtCheckNumber.Text != "");
                        }
                        else if (PaymentMethod == "حساب بنكى")
                        {
                            check = (comDelegate.Text != "" && (comClient.Text != "" || comEng.Text != "") /*&& txtRestMoney.Text != "" && cmbBranch.Text != ""*/ && cmbBank.Text != "" && txtPaidMoney.Text != "" && dateEdit1.Text != "" && txtCheckNumber.Text != "");
                        }

                        if (check)
                        {
                            if (!flagCategoriesSuccess)
                            {
                                if (MessageBox.Show("لم يتم ادخال الفئات..هل تريد الاستمرار؟", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                                {
                                    return;
                                }
                            }

                            double outParse;
                            if (double.TryParse(txtPaidMoney.Text, out outParse))
                            {
                                dbconnection.Open();

                                updateCustomerDesign();

                                string query = "insert into transitions (TransitionBranch_ID,TransitionBranch_Name,Branch_ID,Branch_Name,Bill_Number,Client_ID,Client_Name,Customer_ID,Customer_Name,Transition,Payment_Method,Bank_ID,Bank_Name,Date,Amount,Data,PayDay,Check_Number,Type,Error,Employee_ID,Employee_Name,Delegate_ID,Delegate_Name) values(@TransitionBranch_ID,@TransitionBranch_Name,@Branch_ID,@Branch_Name,@Bill_Number,@Client_ID,@Client_Name,@Customer_ID,@Customer_Name,@Transition,@Payment_Method,@Bank_ID,@Bank_Name,@Date,@Amount,@Data,@PayDay,@Check_Number,@Type,@Error,@Employee_ID,@Employee_Name,@Delegate_ID,@Delegate_Name)";
                                MySqlCommand com = new MySqlCommand(query, dbconnection);

                                com.Parameters.Add("@Transition", MySqlDbType.VarChar, 255).Value = "سحب تصميم";
                                com.Parameters.Add("@Type", MySqlDbType.VarChar, 255).Value = "كاش";
                                com.Parameters.Add("@TransitionBranch_ID", MySqlDbType.Int16, 11).Value = transitionbranchID;
                                com.Parameters.Add("@TransitionBranch_Name", MySqlDbType.VarChar, 255).Value = UserControl.EmpBranchName;
                                com.Parameters.Add("@Branch_ID", MySqlDbType.Int16, 11).Value = transitionbranchID;
                                com.Parameters.Add("@Branch_Name", MySqlDbType.VarChar, 255).Value = UserControl.EmpBranchName;
                                com.Parameters.Add("@Bill_Number", MySqlDbType.Int16, 11).Value = txtDesignNum.Text;
                                com.Parameters.Add("@Payment_Method", MySqlDbType.VarChar, 255).Value = PaymentMethod;
                                com.Parameters.Add("@Bank_ID", MySqlDbType.Int16, 11).Value = cmbBank.SelectedValue;
                                com.Parameters.Add("@Bank_Name", MySqlDbType.VarChar, 255).Value = cmbBank.Text;
                                com.Parameters.Add("@Date", MySqlDbType.DateTime, 0).Value = DateTime.Now;
                                if (comClient.Text != "")
                                {
                                    com.Parameters.Add("@Client_ID", MySqlDbType.Int16, 11).Value = txtClientID.Text;
                                    com.Parameters.Add("@Client_Name", MySqlDbType.VarChar, 255).Value = comClient.Text;
                                }
                                else
                                {
                                    com.Parameters.Add("@Client_ID", MySqlDbType.Int16, 11).Value = null;
                                    com.Parameters.Add("@Client_Name", MySqlDbType.VarChar, 255).Value = null;
                                }
                                if (comEng.Text != "")
                                {
                                    com.Parameters.Add("@Customer_ID", MySqlDbType.Int16, 11).Value = txtCustomerID.Text;
                                    com.Parameters.Add("@Customer_Name", MySqlDbType.VarChar, 255).Value = comEng.Text;
                                }
                                else
                                {
                                    com.Parameters.Add("@Customer_ID", MySqlDbType.Int16, 11).Value = null;
                                    com.Parameters.Add("@Customer_Name", MySqlDbType.VarChar, 255).Value = null;
                                }
                                //com.Parameters.Add("@Operation_Number", MySqlDbType.Int16, 11).Value = opNumString;
                                com.Parameters.Add("@Data", MySqlDbType.VarChar, 255).Value = txtDescrip.Text;
                                com.Parameters.Add("@Error", MySqlDbType.Int16, 11).Value = 0;

                                com.Parameters.Add("@Amount", MySqlDbType.Decimal, 10).Value = txtPaidMoney.Text;

                                MySqlCommand com2 = new MySqlCommand("select Bank_Stock from bank where Bank_ID=" + cmbBank.SelectedValue, dbconnection);
                                double amount2 = Convert.ToDouble(com2.ExecuteScalar().ToString());
                                amount2 -= outParse;
                                MySqlCommand com3 = new MySqlCommand("update bank set Bank_Stock=" + amount2 + " where Bank_ID=" + cmbBank.SelectedValue, dbconnection);
                                com3.ExecuteNonQuery();


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
                                com.Parameters.Add("@Employee_ID", MySqlDbType.Int16, 11).Value = UserControl.EmpID;
                                com.Parameters.Add("@Employee_Name", MySqlDbType.VarChar, 255).Value = UserControl.EmpName;
                                if (comDelegate.SelectedValue != null && comDelegate.Text != "")
                                {
                                    com.Parameters.Add("@Delegate_ID", MySqlDbType.Int16, 11).Value = comDelegate.SelectedValue.ToString();
                                    com.Parameters.Add("@Delegate_Name", MySqlDbType.VarChar, 255).Value = comDelegate.Text;
                                }
                                else
                                {
                                    com.Parameters.Add("@Delegate_ID", MySqlDbType.Int16, 11).Value = null;
                                    com.Parameters.Add("@Delegate_Name", MySqlDbType.VarChar, 255).Value = null;
                                }
                                com.ExecuteNonQuery();

                                //////////record adding/////////////
                                query = "select Transition_ID from transitions order by Transition_ID desc limit 1";
                                com = new MySqlCommand(query, dbconnection);
                                TransitionID = com.ExecuteScalar().ToString();

                                query = "insert into usercontrol (UserControl_UserID,UserControl_TableName,UserControl_Status,UserControl_RecordID,UserControl_Date,UserControl_Reason) values(@UserControl_UserID,@UserControl_TableName,@UserControl_Status,@UserControl_RecordID,@UserControl_Date,@UserControl_Reason)";
                                com = new MySqlCommand(query, dbconnection);
                                com.Parameters.Add("@UserControl_UserID", MySqlDbType.Int16, 11).Value = UserControl.userID;
                                com.Parameters.Add("@UserControl_TableName", MySqlDbType.VarChar, 255).Value = "transitions";
                                com.Parameters.Add("@UserControl_Status", MySqlDbType.VarChar, 255).Value = "اضافة";
                                com.Parameters.Add("@UserControl_RecordID", MySqlDbType.VarChar, 255).Value = TransitionID;
                                com.Parameters.Add("@UserControl_Date", MySqlDbType.DateTime, 0).Value = DateTime.Now;
                                com.Parameters.Add("@UserControl_Reason", MySqlDbType.VarChar, 255).Value = null;
                                com.ExecuteNonQuery();

                                //////////insert categories/////////
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

                                //IncreaseClientPaied();

                                //print bill
                                printCategoriesBill(Convert.ToInt16(txtDesignNum.Text));

                                clear();
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
                    else
                    {
                        MessageBox.Show("رقم الفاتوة غير صحيح");
                    }
                }
                else
                {
                    MessageBox.Show("يجب ادخال رقم الفاتورة");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }
        private void PaidMoney_KeyDown(object sender, KeyEventArgs e)
        {
            double totalPaid = 0;

            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    double total = 0;
                    if (txtPaidMoney.Text != "" && double.TryParse(txtPaidMoney.Text, out total))
                    {
                        if (cmbBank.Text == "")
                        {
                            MessageBox.Show("يجب ان تختار الخزينة");
                            return;
                        }
                        dbconnection.Open();

                        string query = "select ID from categories_money where Bank_ID=" + cmbBank.SelectedValue;
                        MySqlCommand com = new MySqlCommand(query, dbconnection);
                        if (com.ExecuteScalar() == null)
                        {
                            query = "insert into categories_money (a200,a100,a50,a20,a10,a5,a1,aH,aQ,Bank_ID,Bank_Name)values(0,0,0,0,0,0,0,0,0,@Bank_ID,@Bank_Name) ";
                            com = new MySqlCommand(query, dbconnection);
                            com.Parameters.Add("@Bank_ID", MySqlDbType.Int16).Value = cmbBank.SelectedValue;
                            com.Parameters.Add("@Bank_Name", MySqlDbType.VarChar).Value = cmbBank.Text;
                            com.ExecuteNonQuery();
                        }

                        if (!flag)
                        {
                            string query2 = "select * from categories_money where Bank_ID=" + cmbBank.SelectedValue;
                            MySqlCommand com2 = new MySqlCommand(query2, dbconnection);
                            MySqlDataReader dr = com2.ExecuteReader();
                            while (dr.Read())
                            {
                                arrOFPhaat[0] = Convert.ToInt32(dr["a200"]);
                                arrOFPhaat[1] = Convert.ToInt32(dr["a100"]);
                                arrOFPhaat[2] = Convert.ToInt32(dr["a50"]);
                                arrOFPhaat[3] = Convert.ToInt32(dr["a20"]);
                                arrOFPhaat[4] = Convert.ToInt32(dr["a10"]);
                                arrOFPhaat[5] = Convert.ToInt32(dr["a5"]);
                                arrOFPhaat[6] = Convert.ToInt32(dr["a1"]);
                                arrOFPhaat[7] = Convert.ToInt32(dr["aH"]);
                                arrOFPhaat[8] = Convert.ToInt32(dr["aQ"]);
                            }
                            flag = true;
                        }
                        dbconnection.Close();
                        if (PaidMoney.Text != "")
                        {
                            totalPaid = Convert.ToDouble(PaidMoney.Text);
                        }
                        TextBox txt = (TextBox)sender;
                        string txtValue = txt.Name.Split('t')[1];
                        int num;
                        num = 0;
                        switch (txtValue)
                        {
                            case "200":
                                if (int.TryParse(t200.Text, out num))
                                {
                                    arrOFPhaat[0] -= arrPaidMoney[0];
                                    arrPaidMoney[0] = num;
                                    arrOFPhaat[0] += num;
                                    t100.Focus();
                                }
                                else
                                {
                                    MessageBox.Show("القيمة المدخلة يجب ان تكون عدد");
                                    return;
                                }
                                break;

                            case "100":
                                if (int.TryParse(t100.Text, out num))
                                {
                                    arrOFPhaat[1] -= arrPaidMoney[1];
                                    arrPaidMoney[1] = num;
                                    arrOFPhaat[1] += num;
                                    t50.Focus();
                                }
                                else
                                {
                                    MessageBox.Show("القيمة المدخلة يجب ان تكون عدد");
                                    return;
                                }
                                break;

                            case "50":
                                if (int.TryParse(t50.Text, out num))
                                {
                                    arrOFPhaat[2] -= arrPaidMoney[2];
                                    arrPaidMoney[2] = num;
                                    arrOFPhaat[2] += num;
                                    t20.Focus();
                                }
                                else
                                {
                                    MessageBox.Show("القيمة المدخلة يجب ان تكون عدد");
                                    return;
                                }
                                break;

                            case "20":
                                if (int.TryParse(t20.Text, out num))
                                {
                                    arrOFPhaat[3] -= arrPaidMoney[3];
                                    arrPaidMoney[3] = num;
                                    arrOFPhaat[3] += num;
                                    t10.Focus();
                                }
                                else
                                {
                                    MessageBox.Show("القيمة المدخلة يجب ان تكون عدد");
                                    return;
                                }
                                break;

                            case "10":
                                if (int.TryParse(t10.Text, out num))
                                {
                                    arrOFPhaat[4] -= arrPaidMoney[4];
                                    arrPaidMoney[4] = num;
                                    arrOFPhaat[4] += num;
                                    t5.Focus();
                                }
                                else
                                {
                                    MessageBox.Show("القيمة المدخلة يجب ان تكون عدد");
                                    return;
                                }
                                break;

                            case "5":
                                if (int.TryParse(t5.Text, out num))
                                {
                                    arrOFPhaat[5] -= arrPaidMoney[5];
                                    arrPaidMoney[5] = num;
                                    arrOFPhaat[5] += num;
                                    t1.Focus();
                                }
                                else
                                {
                                    MessageBox.Show("القيمة المدخلة يجب ان تكون عدد");
                                    return;
                                }
                                break;

                            case "1":
                                if (int.TryParse(t1.Text, out num))
                                {
                                    arrOFPhaat[6] -= arrPaidMoney[6];
                                    arrPaidMoney[6] = num;
                                    arrOFPhaat[6] += num;
                                    tH.Focus();
                                }
                                else
                                {
                                    MessageBox.Show("القيمة المدخلة يجب ان تكون عدد");
                                    return;
                                }
                                break;
                            case "H":
                                if (int.TryParse(tH.Text, out num))
                                {
                                    arrOFPhaat[7] -= arrPaidMoney[7];
                                    arrPaidMoney[7] = num;
                                    arrOFPhaat[7] += num;
                                    tQ.Focus();
                                }
                                else
                                {
                                    MessageBox.Show("القيمة المدخلة يجب ان تكون عدد");
                                    return;
                                }
                                break;
                            case "Q":
                                if (int.TryParse(tQ.Text, out num))
                                {
                                    arrOFPhaat[8] -= arrPaidMoney[8];
                                    arrPaidMoney[8] = num;
                                    arrOFPhaat[8] += num;
                                    r200.Focus();
                                }
                                else
                                {
                                    MessageBox.Show("القيمة المدخلة يجب ان تكون عدد");
                                    return;
                                }
                                break;
                        }

                        totalPaid = arrPaidMoney[0] * 200 + arrPaidMoney[1] * 100 + arrPaidMoney[2] * 50 + arrPaidMoney[3] * 20 + arrPaidMoney[4] * 10 + arrPaidMoney[5] * 5 + arrPaidMoney[6] + arrPaidMoney[7] * 0.5 + arrPaidMoney[8] * 0.25;
                        PaidMoney.Text = totalPaid.ToString();
                        if ((total - totalPaid) > 0)
                        {
                            txtPaidRest.Text = (total - totalPaid).ToString();
                            txtPaidRest2.Text = "0";
                        }
                        else if ((total - totalPaid) == 0)
                        {
                            txtPaidRest.Text = "0";

                            txtPaidRest2.Text = "0";
                        }
                        else
                        {
                            txtPaidRest.Text = "0";
                            double sub = (total - totalPaid);

                            txtPaidRest2.Text = (-1 * (sub + Convert.ToDouble(RestMoney.Text))).ToString();
                        }

                        if ((Convert.ToDouble(txtPaidRest.Text) == 0) && (Convert.ToDouble(txtPaidRest2.Text) == 0))
                        {
                            dbconnection.Open();
                            query = "update categories_money set a200=" + arrOFPhaat[0] + ",a100=" + arrOFPhaat[1] + ",a50=" + arrOFPhaat[2] + ",a20=" + arrOFPhaat[3] + ",a10=" + arrOFPhaat[4] + ",a5=" + arrOFPhaat[5] + ",a1=" + arrOFPhaat[6] + ",aH=" + arrOFPhaat[7] + ",aQ=" + arrOFPhaat[8] + " where Bank_ID=" + cmbBank.SelectedValue;
                            com = new MySqlCommand(query, dbconnection);
                            com.ExecuteNonQuery();
                            flagCategoriesSuccess = true;
                            MessageBox.Show("تم");
                            /*t200.Text = "";
                            t100.Text = "";
                            t50.Text = "";
                            t20.Text = "";
                            t10.Text = "";
                            t5.Text = "";
                            t1.Text = "";
                            tH.Text = "";
                            tQ.Text = "";
                            r200.Text = "";
                            r100.Text = "";
                            r50.Text = "";
                            r20.Text = "";
                            r10.Text = "";
                            r5.Text = "";
                            r1.Text = "";
                            rH.Text = "";
                            rQ.Text = "";*/
                            RestMoney.Text = "0";
                            PaidMoney.Text = "0";
                            txtPaidRest.Text = "0";
                            txtPaidRest2.Text = "0";
                            //for (int i = 0; i < arrPaidMoney.Length; i++)
                            //    arrPaidMoney[i] = arrRestMoney[i] = 0;
                            flag = false;
                        }
                        else
                        { }
                        dbconnection.Close();
                    }
                    else
                    {
                        dbconnection.Close();
                        MessageBox.Show("تاكد من المبلغ المدفوع اولا");
                        return;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                dbconnection.Close();
            }
        }
        private void RestMoney_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                double total = 0;
                if (txtPaidMoney.Text != "" && double.TryParse(txtPaidMoney.Text, out total))
                {
                    double totalRest = 0, test = 0;
                    if (RestMoney.Text != "")
                    {
                        totalRest = Convert.ToDouble(RestMoney.Text);
                    }

                    if (txtPaidRest.Text != "")
                        test = Convert.ToDouble(txtPaidRest.Text);

                    if (e.KeyCode == Keys.Enter)
                    {
                        TextBox txt = (TextBox)sender;
                        string txtValue = txt.Name.Split('r')[1];
                        int num;
                        num = 0;
                        switch (txtValue)
                        {
                            case "200":
                                if (int.TryParse(r200.Text, out num))
                                {
                                    if (arrOFPhaat[0] >= num)
                                    {
                                        arrOFPhaat[0] += arrRestMoney[0];
                                        arrRestMoney[0] = num;
                                        arrOFPhaat[0] -= num;
                                        r100.Focus();
                                    }
                                    else
                                    {
                                        MessageBox.Show("لا يوجد ما يكفى");
                                        return;
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("القيمة المدخلة يجب ان تكون عدد");
                                    return;
                                }
                                break;

                            case "100":
                                if (int.TryParse(r100.Text, out num))
                                {
                                    if (arrOFPhaat[1] >= num)
                                    {
                                        arrOFPhaat[1] += arrRestMoney[1];
                                        arrRestMoney[1] = num;
                                        arrOFPhaat[1] -= num;
                                        r50.Focus();
                                    }
                                    else
                                    {
                                        MessageBox.Show("لا يوجد ما يكفى");
                                        return;
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("القيمة المدخلة يجب ان تكون عدد");
                                    return;
                                }
                                break;

                            case "50":
                                if (int.TryParse(r50.Text, out num))
                                {
                                    if (arrOFPhaat[2] >= num)
                                    {
                                        arrOFPhaat[2] += arrRestMoney[2];
                                        arrRestMoney[2] = num;
                                        arrOFPhaat[2] -= num;
                                        r20.Focus();
                                    }
                                    else
                                    {
                                        MessageBox.Show("لا يوجد ما يكفى");
                                        return;
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("القيمة المدخلة يجب ان تكون عدد");
                                    return;
                                }
                                break;

                            case "20":
                                if (int.TryParse(r20.Text, out num))
                                {
                                    if (arrOFPhaat[3] >= num)
                                    {
                                        arrOFPhaat[3] += arrRestMoney[3];
                                        arrRestMoney[3] = num;
                                        arrOFPhaat[3] -= num;
                                        r10.Focus();
                                    }
                                    else
                                    {
                                        MessageBox.Show("لا يوجد ما يكفى");
                                        return;
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("القيمة المدخلة يجب ان تكون عدد");
                                    return;
                                }
                                break;

                            case "10":
                                if (int.TryParse(r10.Text, out num))
                                {
                                    if (arrOFPhaat[4] >= num)
                                    {
                                        arrOFPhaat[4] += arrRestMoney[4];
                                        arrRestMoney[4] = num;
                                        arrOFPhaat[4] -= num;
                                        r5.Focus();
                                    }
                                    else
                                    {
                                        MessageBox.Show("لا يوجد ما يكفى");
                                        return;
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("القيمة المدخلة يجب ان تكون عدد");
                                    return;
                                }
                                break;

                            case "5":
                                if (int.TryParse(r5.Text, out num))
                                {
                                    if (arrOFPhaat[5] >= num)
                                    {
                                        arrOFPhaat[5] += arrRestMoney[5];
                                        arrRestMoney[5] = num;
                                        arrOFPhaat[5] -= num;
                                        r1.Focus();
                                    }
                                    else
                                    {
                                        MessageBox.Show("لا يوجد ما يكفى");
                                        return;
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("القيمة المدخلة يجب ان تكون عدد");
                                    return;
                                }
                                break;

                            case "1":
                                if (int.TryParse(r1.Text, out num))
                                {
                                    if (arrOFPhaat[6] >= 0)
                                    {
                                        arrOFPhaat[6] += arrRestMoney[6];
                                        arrRestMoney[6] = num;
                                        arrOFPhaat[6] -= num;
                                        rH.Focus();
                                    }
                                    else
                                    {
                                        MessageBox.Show("لا يوجد ما يكفى");
                                        return;
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("القيمة المدخلة يجب ان تكون عدد");
                                    return;
                                }
                                break;

                            case "H":
                                if (int.TryParse(rH.Text, out num))
                                {
                                    if (arrOFPhaat[7] >= 0)
                                    {
                                        arrOFPhaat[7] += arrRestMoney[7];
                                        arrRestMoney[7] = num;
                                        arrOFPhaat[7] -= num;
                                        rQ.Focus();
                                    }
                                    else
                                    {
                                        MessageBox.Show("لا يوجد ما يكفى");
                                        return;
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("القيمة المدخلة يجب ان تكون عدد");
                                    return;
                                }
                                break;

                            case "Q":
                                if (int.TryParse(rQ.Text, out num))
                                {
                                    if (arrOFPhaat[8] >= 0)
                                    {
                                        arrOFPhaat[8] += arrRestMoney[8];
                                        arrRestMoney[8] = num;
                                        arrOFPhaat[8] -= num;
                                    }
                                    else
                                    {
                                        MessageBox.Show("لا يوجد ما يكفى");
                                        return;
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("القيمة المدخلة يجب ان تكون عدد");
                                    return;
                                }
                                break;
                        }

                        totalRest = arrRestMoney[0] * 200 + arrRestMoney[1] * 100 + arrRestMoney[2] * 50 + arrRestMoney[3] * 20 + arrRestMoney[4] * 10 + arrRestMoney[5] * 5 + arrRestMoney[6] + arrRestMoney[7] * 0.5 + arrRestMoney[8] * 0.25;
                        RestMoney.Text = totalRest.ToString();

                        if ((Convert.ToDouble(RestMoney.Text) - (-1 * (Convert.ToDouble(txtPaidMoney.Text) - Convert.ToDouble(PaidMoney.Text)))) < 0)
                        {
                            txtPaidRest.Text = "0";
                            txtPaidRest2.Text = (-1 * ((Convert.ToDouble(RestMoney.Text) - (-1 * (Convert.ToDouble(txtPaidMoney.Text) - Convert.ToDouble(PaidMoney.Text)))))).ToString();
                     
                        }
                        else if ((Convert.ToDouble(RestMoney.Text) - (-1 * (Convert.ToDouble(txtPaidMoney.Text) - Convert.ToDouble(PaidMoney.Text)))) == 0)
                        {
                            txtPaidRest2.Text = "0";

                            txtPaidRest.Text = "0";
                        }
                        else
                        {
                            double sub = (Convert.ToDouble(txtPaidMoney.Text) - Convert.ToDouble(PaidMoney.Text));
                            txtPaidRest.Text = (Convert.ToDouble(RestMoney.Text) - (-1 * sub)).ToString();
                            txtPaidRest2.Text = "0";
                        }

                        if ((Convert.ToDouble(txtPaidRest.Text) == 0) && (Convert.ToDouble(txtPaidRest2.Text) == 0))
                        {
                            dbconnection.Open();
                            string query = "update categories_money set a200=" + arrOFPhaat[0] + ",a100=" + arrOFPhaat[1] + ",a50=" + arrOFPhaat[2] + ",a20=" + arrOFPhaat[3] + ",a10=" + arrOFPhaat[4] + ",a5=" + arrOFPhaat[5] + ",a1=" + arrOFPhaat[6] + ",aH=" + arrOFPhaat[7] + ",aQ=" + arrOFPhaat[8] + " where Bank_ID=" + cmbBank.SelectedValue;
                            MySqlCommand com = new MySqlCommand(query, dbconnection);
                            com.ExecuteNonQuery();
                            flagCategoriesSuccess = true;
                            MessageBox.Show("تم");
                            /*t200.Text = "";
                            t100.Text = "";
                            t50.Text = "";
                            t20.Text = "";
                            t10.Text = "";
                            t5.Text = "";
                            t1.Text = "";
                            tH.Text = "";
                            tQ.Text = "";
                            r200.Text = "";
                            r100.Text = "";
                            r50.Text = "";
                            r20.Text = "";
                            r10.Text = "";
                            r5.Text = "";
                            r1.Text = "";
                            rH.Text = "";
                            rQ.Text = "";*/
                            RestMoney.Text = "0";
                            PaidMoney.Text = "0";
                            txtPaidRest.Text = "0";
                            txtPaidRest2.Text = "0";
                         
                            //for (int i = 0; i < arrRestMoney.Length; i++)
                            //    arrRestMoney[i] = arrPaidMoney[i] = 0;
                            flag = false;
                        }
                        else
                        { }
                        dbconnection.Close();
                    }
                }
                else
                {
                    dbconnection.Close();
                    MessageBox.Show("تاكد من المبلغ المدفوع اولا");
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }

        //functions
        public void clear()
        {
            foreach (Control item in this.panel1.Controls)
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
                //if (co is GroupBox)
                //{
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
                //}
            }

            return flag5;
        }
        void printCategoriesBill(int customerDesignID)
        {
            Print_PullDesign_Report f = new Print_PullDesign_Report();
            if (comClient.Text != "")
            {
                f.PrintInvoice(DateTime.Now, customerDesignID, TransitionID,txtBranchBillNumber.Text, UserControl.EmpBranchName, comClient.Text + " " + txtClientID.Text, Convert.ToDouble(txtPaidMoney.Text), PaymentMethod, cmbBank.Text, txtCheckNumber.Text, dateEdit1.Text, txtDescrip.Text, UserControl.EmpName, comDelegate.Text, txtNoItemBath.Text, txtCostBath.Text, txtTotalBath.Text, txtNoItemKitchen.Text, txtCostKitchen.Text, txtTotalKitchen.Text, txtNoItemHall.Text, txtCostHall.Text, txtTotalHall.Text, txtNoItemRoom.Text, txtCostRoom.Text, txtTotalRoom.Text, txtNoItemOther.Text, txtCostOther.Text, txtTotalOther.Text);
            }
            else if (comEng.Text != "")
            {
                f.PrintInvoice(DateTime.Now, customerDesignID, TransitionID, txtBranchBillNumber.Text, UserControl.EmpBranchName, comEng.Text + " " + txtCustomerID.Text, Convert.ToDouble(txtPaidMoney.Text), PaymentMethod, cmbBank.Text, txtCheckNumber.Text, dateEdit1.Text,  txtDescrip.Text, UserControl.EmpName, comDelegate.Text, txtNoItemBath.Text, txtCostBath.Text, txtTotalBath.Text, txtNoItemKitchen.Text, txtCostKitchen.Text, txtTotalKitchen.Text, txtNoItemHall.Text, txtCostHall.Text, txtTotalHall.Text, txtNoItemRoom.Text, txtCostRoom.Text, txtTotalRoom.Text, txtNoItemOther.Text, txtCostOther.Text, txtTotalOther.Text);
            }
            f.ShowDialog();
            for (int i = 0; i < arrPaidMoney.Length; i++)
                arrPaidMoney[i] = arrRestMoney[i] = 0;
            for (int i = 0; i < arrRestMoney.Length; i++)
                arrRestMoney[i] = arrPaidMoney[i] = 0;
        }
        public void updateCustomerDesign()
        {
            string query = "update customer_design set Design_Status=@Design_Status,BranchBillNumber=@BranchBillNumber,Branch_ID=@Branch_ID,Branch_Name=@Branch_Name where CustomerDesign_ID=" + txtDesignNum.Text;
            MySqlCommand com = new MySqlCommand(query, dbconnection);
            com.Parameters.Add("@Design_Status", MySqlDbType.VarChar, 255).Value = "اتمام فاتورة";
            com.Parameters.Add("@Branch_ID", MySqlDbType.Int16, 11).Value = transitionbranchID;
            com.Parameters.Add("@Branch_Name", MySqlDbType.VarChar, 255).Value =comBranchName.Text;
            com.Parameters.Add("@BranchBillNumber", MySqlDbType.Int16, 11).Value = Convert.ToInt16(txtBranchBillNumber.Text);
            com.ExecuteNonQuery();
        }
        public void setDesignDetails(int CustomerDesignID)
        {
            dbconnectionr.Open();
            string query = "select * from customer_design_details where CustomerDesignID=" + CustomerDesignID;
            MySqlCommand com = new MySqlCommand(query, dbconnectionr);
            MySqlDataReader dr = com.ExecuteReader();
            while (dr.Read())
            {
                switch (dr[2].ToString())
                {
                    case "حمام":
                        chBpath.Checked = true;
                        txtNoItemBath.Text = dr[4].ToString();
                        txtCostBath.Text = dr[5].ToString();
                        txtTotalBath.Text = dr[6].ToString();
                        calTotal();

                        DesignItem designItem = new DesignItem();
                        designItem.DesignLocation = "حمام";
                        designItem.NoItems =Convert.ToInt16(dr[4].ToString());
                        designItem.ItemCost = Convert.ToDouble(dr[5].ToString());
                        designItem.Total = Convert.ToDouble(dr[6].ToString());
                        listDesignItems.Add(designItem);
                       
                        break;
                    case "مطبخ":
                        chBKitchen.Checked = true;
                        txtNoItemKitchen.Text = dr[4].ToString();
                        txtCostKitchen.Text = dr[5].ToString();
                        txtTotalKitchen.Text = dr[6].ToString();
                        calTotal();

                        designItem = new DesignItem();
                        designItem.DesignLocation = "مطبخ";
                        designItem.NoItems = Convert.ToInt16(dr[4].ToString());
                        designItem.ItemCost = Convert.ToDouble(dr[5].ToString());
                        designItem.Total = Convert.ToDouble(dr[6].ToString());
                        listDesignItems.Add(designItem);
                        break;
                    case "صالة":
                        chkBLiving.Checked = true;
                        txtNoItemHall.Text = dr[4].ToString();
                        txtCostHall.Text = dr[5].ToString();
                        txtTotalHall.Text = dr[6].ToString();
                        calTotal();

                        designItem = new DesignItem();
                        designItem.DesignLocation = "صالة";
                        designItem.NoItems = Convert.ToInt16(dr[4].ToString());
                        designItem.ItemCost = Convert.ToDouble(dr[5].ToString());
                        designItem.Total = Convert.ToDouble(dr[6].ToString());
                        listDesignItems.Add(designItem);
                        break;
                    case "غرفة":
                        chBRoom.Checked = true;
                        txtNoItemRoom.Text = dr[4].ToString();
                        txtCostRoom.Text = dr[5].ToString();
                        txtTotalRoom.Text = dr[6].ToString();
                        calTotal();

                        designItem = new DesignItem();
                        designItem.DesignLocation = "غرفة";
                        designItem.NoItems = Convert.ToInt16(dr[4].ToString());
                        designItem.ItemCost = Convert.ToDouble(dr[5].ToString());
                        designItem.Total = Convert.ToDouble(dr[6].ToString());
                        listDesignItems.Add(designItem);
                        break;
                    case "اخري":
                        chBOther.Checked = true;
                        txtNoItemOther.Text = dr[4].ToString();
                        txtCostOther.Text = dr[5].ToString();
                        txtTotalOther.Text = dr[6].ToString();
                        calTotal();

                        designItem = new DesignItem();
                        designItem.DesignLocation = "اخري";
                        designItem.NoItems = Convert.ToInt16(dr[4].ToString());
                        designItem.ItemCost = Convert.ToDouble(dr[5].ToString());
                        designItem.Total = Convert.ToDouble(dr[6].ToString());
                        listDesignItems.Add(designItem);
                        break;

                }
            }
            dr.Close();
            dbconnectionr.Close();
        }
        public void calTotal()
        {
            double total1 = Convert.ToDouble(txtTotalBath.Text);
            double total2 = Convert.ToDouble(txtTotalKitchen.Text);
            double total3 = Convert.ToDouble(txtTotalHall.Text);
            double total4 = Convert.ToDouble(txtTotalRoom.Text);
            double total5 = Convert.ToDouble(txtTotalOther.Text);

            double total = total1 + total2 + total3 + total4 + total5;
            txtTotal.Text = total.ToString();
            txtPaidMoney.Text = total.ToString();
        }
        public DesignItem getDesignItem(string DesignLocation)
        {
            foreach (DesignItem item in listDesignItems)
            {
                if (item.DesignLocation == DesignLocation)
                {
                    return item;
                }
            }
            return null;
        }
        public bool CheckBillNumber()
        {
            
            dbconnection.Open();
            string query = "select CustomerBill_ID from customer_bill where Branch_BillNumber="+txtBranchBillNumber.Text+" and Branch_Name='"+comBranchName.Text+"'";
            MySqlCommand com = new MySqlCommand(query, dbconnection);
            if (com.ExecuteScalar() != null)
            {
                dbconnection.Close();
                return true;
            }
            else
            {
                dbconnection.Close();
                return false;
            }

          
        }
        public class DesignItem
        {

            public string DesignLocation { get; set; }
            public double Space { get; set; }
            public int NoItems { get; set; }
            public double ItemCost { get; set; }
            public double Total { get; set; }
        }
    }
}

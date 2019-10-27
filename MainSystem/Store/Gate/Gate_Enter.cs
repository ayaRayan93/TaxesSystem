using DevExpress.XtraEditors;
using DevExpress.XtraTab;
using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MainSystem
{
    public partial class Gate_Enter : Form
    {
        MySqlConnection conn;
        MainForm mainform = null;
        XtraTabControl xtraTabControlStoresContent = null;
        bool loaded = false;
        bool flag = false;
        bool flag2 = false;
        List<string> arr;
        List<TreeNode> checkedNodes = new List<TreeNode>();
        int storeId = 0;
        string storeName = "";

        public Gate_Enter(MainForm Mainform, XtraTabControl TabControlStoresContent)
        {
            try
            {
                InitializeComponent();
                mainform = Mainform;
                string constr = connection.connectionString;
                conn = new MySqlConnection(constr);
                xtraTabControlStoresContent = TabControlStoresContent;
                arr = new List<string>();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Gate_Enter_Load(object sender, EventArgs e)
        {
            try
            {
                string query = "select Driver_ID,Driver_Name from drivers";
                MySqlDataAdapter da = new MySqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                comDriver.DataSource = dt;
                comDriver.DisplayMember = dt.Columns["Driver_Name"].ToString();
                comDriver.ValueMember = dt.Columns["Driver_ID"].ToString();
                comDriver.SelectedIndex = -1;

                query = "select * from cars";
                da = new MySqlDataAdapter(query, conn);
                dt = new DataTable();
                da.Fill(dt);
                comCar.DataSource = dt;
                comCar.DisplayMember = dt.Columns["Car_Number"].ToString();
                comCar.ValueMember = dt.Columns["Car_ID"].ToString();
                comCar.SelectedIndex = -1;

                query = "select * from employee where Employee_Job='مسئول تعتيق'";
                da = new MySqlDataAdapter(query, conn);
                dt = new DataTable();
                da.Fill(dt);
                comEmployee.DataSource = dt;
                comEmployee.DisplayMember = dt.Columns["Employee_Name"].ToString();
                comEmployee.ValueMember = dt.Columns["Employee_ID"].ToString();
                comEmployee.SelectedIndex = -1;

                query = "select * from supplier";
                da = new MySqlDataAdapter(query, conn);
                dt = new DataTable();
                da.Fill(dt);
                comSupplier.DataSource = dt;
                comSupplier.DisplayMember = dt.Columns["Supplier_Name"].ToString();
                comSupplier.ValueMember = dt.Columns["Supplier_ID"].ToString();
                comSupplier.SelectedIndex = -1;

                query = "select * from customer";
                da = new MySqlDataAdapter(query, conn);
                dt = new DataTable();
                da.Fill(dt);
                comClient.DataSource = dt;
                comClient.DisplayMember = dt.Columns["Customer_Name"].ToString();
                comClient.ValueMember = dt.Columns["Customer_ID"].ToString();
                comClient.SelectedIndex = -1;

                query = "select * from branch";
                da = new MySqlDataAdapter(query, conn);
                dt = new DataTable();
                da.Fill(dt);
                comBranch.DataSource = dt;
                comBranch.DisplayMember = dt.Columns["Branch_Name"].ToString();
                comBranch.ValueMember = dt.Columns["Branch_ID"].ToString();
                comBranch.SelectedIndex = -1;

                //string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"Store.txt");
                //storeId = Convert.ToInt32(System.IO.File.ReadAllText(path));

                string supString = BaseData.StoreID;
                storeId = Convert.ToInt32(supString);

                conn.Open();
                query = "select Store_Name from store where Store_ID=" + storeId;
                MySqlCommand com = new MySqlCommand(query, conn);
                storeName = com.ExecuteScalar().ToString();

                query = "select * from store where Store_ID <>" + storeId;
                da = new MySqlDataAdapter(query, conn);
                dt = new DataTable();
                da.Fill(dt);
                comStore.DataSource = dt;
                comStore.DisplayMember = dt.Columns["Store_Name"].ToString();
                comStore.ValueMember = dt.Columns["Store_ID"].ToString();
                comStore.SelectedIndex = -1;

                comReason.DisplayMember = "Text";
                comReason.ValueMember = "Value";
                var items = new[] {
                                new { Text = "وارد", Value = "1" },
                                new { Text = "صادر", Value = "2" }
                                };
                comReason.DataSource = items;
                comReason.SelectedIndex = -1;

                comResponsible.DisplayMember = "Text";
                comResponsible.ValueMember = "Value";
                var items2 = new[] {
                                new { Text = "شحن", Value = "1" },
                                new { Text = "عميل", Value = "2" }
                                };
                comResponsible.DataSource = items2;
                comResponsible.SelectedIndex = -1;
                loaded = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            conn.Close();
        }

        private void comReason_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (loaded)
                {
                    comResponsible.Visible = false;
                    labelResponsible.Visible = false;
                    comEmployee.Visible = false;
                    labelEmp.Visible = false;
                    labelPerNum.Visible = false;
                    txtPermisionNum.Visible = false;
                    labelDriver.Visible = false;
                    comDriver.Visible = false;
                    txtDriver.Visible = false;
                    labelCar.Visible = false;
                    comCar.Visible = false;
                    txtCar.Visible = false;
                    txtLicense.Visible = false;
                    labelLicense.Visible = false;
                    txtDescription.Visible = false;
                    labelDescription.Visible = false;
                    btnAddNum.Visible = false;
                    treeViewSupPerm.Visible = false;
                    btnDeleteNum.Visible = false;
                    labelStore.Visible = false;
                    comStore.Visible = false;
                    labelSupplier.Visible = false;
                    comSupplier.Visible = false;
                    labelClient.Visible = false;
                    comClient.Visible = false;
                    labelBranch.Visible = false;
                    comBranch.Visible = false;
                    flag = false;
                    clear();
                    loaded = false;
                    comDriver.SelectedIndex = -1;
                    comCar.SelectedIndex = -1;
                    comEmployee.SelectedIndex = -1;
                    comType.SelectedIndex = -1;
                    comResponsible.SelectedIndex = -1;
                    comStore.SelectedIndex = -1;
                    comClient.SelectedIndex = -1;
                    comBranch.SelectedIndex = -1;

                    string query = "select Driver_ID,Driver_Name from drivers";
                    MySqlDataAdapter da = new MySqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    comDriver.DataSource = dt;
                    comDriver.DisplayMember = dt.Columns["Driver_Name"].ToString();
                    comDriver.ValueMember = dt.Columns["Driver_ID"].ToString();
                    comDriver.SelectedIndex = -1;

                    query = "select * from cars";
                    da = new MySqlDataAdapter(query, conn);
                    dt = new DataTable();
                    da.Fill(dt);
                    comCar.DataSource = dt;
                    comCar.DisplayMember = dt.Columns["Car_Number"].ToString();
                    comCar.ValueMember = dt.Columns["Car_ID"].ToString();
                    comCar.SelectedIndex = -1;
                    loaded = true;

                    if (comReason.SelectedValue.ToString() == "1")
                    {
                        comType.Visible = true;
                        labelType.Visible = true;
                        labelType2.Visible = true;

                        comType.DisplayMember = "Text";
                        comType.ValueMember = "Value";
                        var items = new[] {
                                new { Text = "مورد", Value = "1" },
                                new { Text = "المركز التجارى", Value = "2" },
                                new { Text = "مرتجع عميل", Value = "3" },
                                new { Text = "تحويل", Value = "4" }
                                };
                        comType.DataSource = items;
                        comType.SelectedIndex = -1;
                        flag = true;
                    }
                    else if (comReason.SelectedValue.ToString() == "2")
                    {
                        comType.Visible = true;
                        labelType.Visible = true;
                        labelType2.Visible = true;

                        comType.DisplayMember = "Text";
                        comType.ValueMember = "Value";
                        var items = new[] {
                                new { Text = "مبيعات", Value = "1" },
                                new { Text = "مرتجع مورد", Value = "2" },
                                new { Text = "تحويل", Value = "3" },
                                new { Text = "اخرى", Value = "4" }
                                };
                        comType.DataSource = items;
                        comType.SelectedIndex = -1;
                        flag = true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void comType_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (loaded && flag)
                {
                    flag2 = false;
                    clear();
                    loaded = false;
                    comDriver.SelectedIndex = -1;
                    comCar.SelectedIndex = -1;
                    comEmployee.SelectedIndex = -1;
                    comStore.SelectedIndex = -1;
                    comSupplier.SelectedIndex = -1;
                    comClient.SelectedIndex = -1; ;
                    comBranch.SelectedIndex = -1;

                    string query = "select Driver_ID,Driver_Name from drivers";
                    MySqlDataAdapter da = new MySqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    comDriver.DataSource = dt;
                    comDriver.DisplayMember = dt.Columns["Driver_Name"].ToString();
                    comDriver.ValueMember = dt.Columns["Driver_ID"].ToString();
                    comDriver.SelectedIndex = -1;

                    query = "select * from cars";
                    da = new MySqlDataAdapter(query, conn);
                    dt = new DataTable();
                    da.Fill(dt);
                    comCar.DataSource = dt;
                    comCar.DisplayMember = dt.Columns["Car_Number"].ToString();
                    comCar.ValueMember = dt.Columns["Car_ID"].ToString();
                    comCar.SelectedIndex = -1;
                    loaded = true;
                    if (((comReason.SelectedValue.ToString() == "1" && comType.SelectedValue.ToString() == "1") || (comReason.SelectedValue.ToString() == "2" && comType.SelectedValue.ToString() == "2")) || ((comReason.SelectedValue.ToString() == "1" && comType.SelectedValue.ToString() == "2") /*|| (comReason.SelectedValue.ToString() == "2" && comType.SelectedValue.ToString() == "2")*/))
                    {
                        labelSupplier.Visible = true;
                        comSupplier.Visible = true;
                    }
                    else
                    {
                        labelSupplier.Visible = false;
                        comSupplier.Visible = false;
                    }
                    if ((comReason.SelectedValue.ToString() == "1" && comType.SelectedValue.ToString() == "4") || (comReason.SelectedValue.ToString() == "2" && comType.SelectedValue.ToString() == "3"))
                    {
                        labelStore.Visible = true;
                        comStore.Visible = true;
                    }
                    else
                    {
                        labelStore.Visible = false;
                        comStore.Visible = false;
                    }
                    
                    if ((comReason.SelectedValue.ToString() == "1" && comType.SelectedValue.ToString() == "3") || (comReason.SelectedValue.ToString() == "2" && comType.SelectedValue.ToString() == "1"))
                    {
                        labelClient.Visible = false;
                        comClient.Visible = false;
                        labelBranch.Visible = false;
                        comBranch.Visible = false;
                        comResponsible.Visible = true;
                        labelResponsible.Visible = true;

                        comEmployee.Visible = false;
                        labelEmp.Visible = false;
                        labelPerNum.Visible = false;
                        txtPermisionNum.Visible = false;
                        labelDriver.Visible = false;
                        comDriver.Visible = false;
                        txtDriver.Visible = false;
                        labelCar.Visible = false;
                        comCar.Visible = false;
                        txtCar.Visible = false;
                        txtLicense.Visible = false;
                        labelLicense.Visible = false;
                        txtDescription.Visible = false;
                        labelDescription.Visible = false;
                        btnAddNum.Visible = false;
                        treeViewSupPerm.Visible = false;
                        btnDeleteNum.Visible = false;
                        comResponsible.SelectedIndex = -1;
                        flag2 = true;
                    }
                    else
                    {
                        labelClient.Visible = false;
                        comClient.Visible = false;
                        labelBranch.Visible = false;
                        comBranch.Visible = false;
                        comResponsible.Visible = false;
                        labelResponsible.Visible = false;

                        if ((comReason.SelectedValue.ToString() == "1" && comType.SelectedValue.ToString() == "4") || (comReason.SelectedValue.ToString() == "2" && comType.SelectedValue.ToString() == "3"))
                        {
                            comEmployee.Visible = true;
                            labelEmp.Visible = true;
                            labelPerNum.Visible = true;
                            txtPermisionNum.Visible = true;
                            labelDriver.Visible = true;
                            comDriver.Visible = true;
                            txtDriver.Visible = false;
                            labelCar.Visible = true;
                            comCar.Visible = true;
                            txtCar.Visible = false;
                            txtLicense.Visible = true;
                            labelLicense.Visible = true;
                            txtDescription.Visible = true;
                            labelDescription.Visible = true;
                            btnAddNum.Visible = true;
                            treeViewSupPerm.Visible = true;
                            btnDeleteNum.Visible = true;
                        }
                        else if (comReason.SelectedValue.ToString() == "2" && comType.SelectedValue.ToString() == "4")
                        {
                            comEmployee.Visible = false;
                            labelEmp.Visible = false;
                            labelPerNum.Visible = false;
                            txtPermisionNum.Visible = false;
                            labelDriver.Visible = true;
                            comDriver.Visible = false;
                            txtDriver.Visible = true;
                            labelCar.Visible = true;
                            comCar.Visible = false;
                            txtCar.Visible = true;
                            txtLicense.Visible = true;
                            labelLicense.Visible = true;
                            txtDescription.Visible = true;
                            labelDescription.Visible = true;
                            btnAddNum.Visible = false;
                            treeViewSupPerm.Visible = false;
                            btnDeleteNum.Visible = false;
                        }
                        else
                        {
                            comEmployee.Visible = true;
                            labelEmp.Visible = true;
                            labelPerNum.Visible = true;
                            txtPermisionNum.Visible = true;
                            labelDriver.Visible = true;
                            comDriver.Visible = false;
                            txtDriver.Visible = true;
                            labelCar.Visible = true;
                            comCar.Visible = false;
                            txtCar.Visible = true;
                            txtLicense.Visible = true;
                            labelLicense.Visible = true;
                            txtDescription.Visible = true;
                            labelDescription.Visible = true;
                            btnAddNum.Visible = true;
                            treeViewSupPerm.Visible = true;
                            btnDeleteNum.Visible = true;
                        }
                        flag2 = false;
                        comResponsible.SelectedIndex = -1;
                    }

                    if((comReason.SelectedValue.ToString() == "2" && comType.SelectedValue.ToString() == "2")|| (comReason.SelectedValue.ToString() == "2" && comType.SelectedValue.ToString() == "3"))
                    {
                        labelPerNum.Visible = false;
                        txtPermisionNum.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void comResponsible_SelectedValueChanged(object sender, EventArgs e)
        {
            if (loaded && flag2)
            {
                /*labelClient.Visible = true;
                comClient.Visible = true;
                labelBranch.Visible = true;
                comBranch.Visible = true;*/
                if (comResponsible.SelectedValue.ToString() == "1")
                {
                    labelClient.Visible = false;
                    comClient.Visible = false;
                    labelBranch.Visible = false;
                    comBranch.Visible = false;
                    comEmployee.Visible = true;
                    labelEmp.Visible = true;
                    labelPerNum.Visible = true;
                    txtPermisionNum.Visible = true;
                    labelDriver.Visible = true;
                    comDriver.Visible = true;
                    txtDriver.Visible = false;
                    labelCar.Visible = true;
                    comCar.Visible = true;
                    txtCar.Visible = false;
                    txtLicense.Visible = true;
                    labelLicense.Visible = true;
                    txtDescription.Visible = true;
                    labelDescription.Visible = true;
                    btnAddNum.Visible = true;
                    treeViewSupPerm.Visible = true;
                    btnDeleteNum.Visible = true;
                }
                else if (comResponsible.SelectedValue.ToString() == "2")
                {
                    labelClient.Visible = true;
                    comClient.Visible = true;
                    labelBranch.Visible = true;
                    comBranch.Visible = true;
                    comEmployee.Visible = true;
                    labelEmp.Visible = true;
                    labelPerNum.Visible = true;
                    txtPermisionNum.Visible = true;
                    labelDriver.Visible = true;
                    comDriver.Visible = false;
                    txtDriver.Visible = true;
                    labelCar.Visible = true;
                    comCar.Visible = false;
                    txtCar.Visible = true;
                    txtLicense.Visible = true;
                    labelLicense.Visible = true;
                    txtDescription.Visible = true;
                    labelDescription.Visible = true;
                    btnAddNum.Visible = true;
                    treeViewSupPerm.Visible = true;
                    btnDeleteNum.Visible = true;
                }
                clear();
                loaded = false;
                comDriver.SelectedIndex = -1;
                comCar.SelectedIndex = -1;
                comEmployee.SelectedIndex = -1;
                comStore.SelectedIndex = -1;
                comClient.SelectedIndex = -1;
                comBranch.SelectedIndex = -1;

                string query = "select Driver_ID,Driver_Name from drivers";
                MySqlDataAdapter da = new MySqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                comDriver.DataSource = dt;
                comDriver.DisplayMember = dt.Columns["Driver_Name"].ToString();
                comDriver.ValueMember = dt.Columns["Driver_ID"].ToString();
                comDriver.SelectedIndex = -1;

                query = "select * from cars";
                da = new MySqlDataAdapter(query, conn);
                dt = new DataTable();
                da.Fill(dt);
                comCar.DataSource = dt;
                comCar.DisplayMember = dt.Columns["Car_Number"].ToString();
                comCar.ValueMember = dt.Columns["Car_ID"].ToString();
                comCar.SelectedIndex = -1;
                loaded = true;
            }
        }

        private void comDriver_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (loaded)
                {
                    conn.Open();
                    string query = "select * from cars where Car_ID in(select Car_ID from driver_car where Driver_ID=" + comDriver.SelectedValue.ToString() + ")";
                    MySqlDataAdapter da = new MySqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    comCar.DataSource = dt;
                    comCar.DisplayMember = dt.Columns["Car_Number"].ToString();
                    comCar.ValueMember = dt.Columns["Car_ID"].ToString();
                    comCar.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            conn.Close();
        }

        private void comCar_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (loaded)
                {
                    conn.Open();
                    string query = "select Driver_ID,Driver_Name from drivers where Driver_ID in(select Driver_ID from driver_car where Car_ID=" + comCar.SelectedValue.ToString() + ")";
                    MySqlDataAdapter da = new MySqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    comDriver.DataSource = dt;
                    comDriver.DisplayMember = dt.Columns["Driver_Name"].ToString();
                    comDriver.ValueMember = dt.Columns["Driver_ID"].ToString();
                    comDriver.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            conn.Close();
        }

        private void btnAddNum_Click(object sender, EventArgs e)
        {
            try
            {
                //if (txtPermisionNum.Text != "")
                //{
                #region case of store or supplier
                if (comSupplier.SelectedIndex != -1 || comStore.SelectedIndex != -1)
                {
                    if (treeViewSupIdPerm.Nodes.Count > 0)
                    {
                        for (int i = 0; i < treeViewSupIdPerm.Nodes.Count; i++)
                        {
                            if (comSupplier.SelectedValue.ToString() == treeViewSupIdPerm.Nodes[i].Text)
                            {
                                for (int j = 0; j < treeViewSupIdPerm.Nodes[i].Nodes.Count; j++)
                                {
                                    if (txtPermisionNum.Text == treeViewSupIdPerm.Nodes[i].Nodes[j].Text)
                                    {
                                        MessageBox.Show("هذا الاذن تم اضافتة");
                                        return;
                                    }
                                }
                                if (txtPermisionNum.Text != "")
                                {
                                    OpenFileDialog openFileDialog1 = new OpenFileDialog();
                                    if (openFileDialog1.ShowDialog() == DialogResult.OK)
                                    {
                                        string selectedFile = openFileDialog1.FileName;
                                        byte[] selectedRequestImage = null;
                                        selectedRequestImage = File.ReadAllBytes(selectedFile);
                                        treeViewSupIdPerm.Nodes[i].Nodes.Add(txtPermisionNum.Text);
                                        treeViewSupPerm.Nodes[i].Nodes.Add(txtPermisionNum.Text);
                                        imageListBoxControl1.Items.Add(selectedFile);
                                        txtPermisionNum.Text = "";
                                        return;
                                    }
                                    else
                                    {
                                        return;
                                    }
                                }
                            }

                        }
                        treeViewSupIdPerm.Nodes.Add(comSupplier.SelectedValue.ToString());
                        treeViewSupPerm.Nodes.Add(comSupplier.Text);
                        if (txtPermisionNum.Text != "")
                        {
                            OpenFileDialog openFileDialog2 = new OpenFileDialog();
                            if (openFileDialog2.ShowDialog() == DialogResult.OK)
                            {
                                string selectedFile = openFileDialog2.FileName;
                                byte[] selectedRequestImage = null;
                                selectedRequestImage = File.ReadAllBytes(selectedFile);
                                int n = treeViewSupIdPerm.Nodes.Count;
                                treeViewSupIdPerm.Nodes[n - 1].Nodes.Add(txtPermisionNum.Text);
                                treeViewSupPerm.Nodes[n - 1].Nodes.Add(txtPermisionNum.Text);
                                imageListBoxControl1.Items.Add(selectedFile);
                                txtPermisionNum.Text = "";
                                return;
                            }
                            else
                            {
                                return;
                            }
                        }
                    }
                    else
                    {
                        treeViewSupIdPerm.Nodes.Add(comSupplier.SelectedValue.ToString());
                        treeViewSupPerm.Nodes.Add(comSupplier.Text);
                        if (txtPermisionNum.Text != "")
                        {
                            OpenFileDialog openFileDialog1 = new OpenFileDialog();
                            if (openFileDialog1.ShowDialog() == DialogResult.OK)
                            {
                                string selectedFile = openFileDialog1.FileName;
                                byte[] selectedRequestImage = null;
                                selectedRequestImage = File.ReadAllBytes(selectedFile);
                                treeViewSupIdPerm.Nodes[0].Nodes.Add(txtPermisionNum.Text);
                                treeViewSupPerm.Nodes[0].Nodes.Add(txtPermisionNum.Text);
                                imageListBoxControl1.Items.Add(selectedFile);
                                txtPermisionNum.Text = "";
                                return;
                            }
                            else
                            {
                                return;
                            }
                        }
                    }
                }
                #endregion
                #region case of client
                else if (comClient.SelectedIndex != -1 && comBranch.SelectedIndex != -1)
                {
                    if (treeViewSupIdPerm.Nodes.Count > 0)
                    {
                        for (int i = 0; i < treeViewSupIdPerm.Nodes.Count; i++)
                        {
                            if (comClient.SelectedValue.ToString() == treeViewSupIdPerm.Nodes[i].Text)
                            {
                                for (int j = 0; j < treeViewSupIdPerm.Nodes[i].Nodes.Count; j++)
                                {
                                    if ((comBranch.SelectedValue.ToString() + ":" + txtPermisionNum.Text) == treeViewSupIdPerm.Nodes[i].Nodes[j].Text)
                                    {
                                        MessageBox.Show("هذا الاذن تم اضافتة");
                                        return;
                                    }
                                }
                                if (txtPermisionNum.Text != "")
                                {
                                    OpenFileDialog openFileDialog1 = new OpenFileDialog();
                                    if (openFileDialog1.ShowDialog() == DialogResult.OK)
                                    {
                                        string selectedFile = openFileDialog1.FileName;
                                        byte[] selectedRequestImage = null;
                                        selectedRequestImage = File.ReadAllBytes(selectedFile);
                                        treeViewSupIdPerm.Nodes[i].Nodes.Add(comBranch.SelectedValue.ToString() + ":" + txtPermisionNum.Text);
                                        treeViewSupPerm.Nodes[i].Nodes.Add(comBranch.Text + ":" + txtPermisionNum.Text);
                                        imageListBoxControl1.Items.Add(selectedFile);
                                        txtPermisionNum.Text = "";
                                        return;
                                    }
                                    else
                                    {
                                        return;
                                    }
                                }
                            }

                        }
                        treeViewSupIdPerm.Nodes.Add(comClient.SelectedValue.ToString());
                        treeViewSupPerm.Nodes.Add(comClient.Text);
                        if (txtPermisionNum.Text != "")
                        {
                            OpenFileDialog openFileDialog2 = new OpenFileDialog();
                            if (openFileDialog2.ShowDialog() == DialogResult.OK)
                            {
                                string selectedFile = openFileDialog2.FileName;
                                byte[] selectedRequestImage = null;
                                selectedRequestImage = File.ReadAllBytes(selectedFile);
                                int n = treeViewSupIdPerm.Nodes.Count;
                                treeViewSupIdPerm.Nodes[n - 1].Nodes.Add(comBranch.SelectedValue.ToString() + ":" + txtPermisionNum.Text);
                                treeViewSupPerm.Nodes[n - 1].Nodes.Add(comBranch.Text + ":" + txtPermisionNum.Text);
                                imageListBoxControl1.Items.Add(selectedFile);
                                txtPermisionNum.Text = "";
                                return;
                            }
                            else
                            {
                                return;
                            }
                        }
                    }
                    else
                    {
                        treeViewSupIdPerm.Nodes.Add(comClient.SelectedValue.ToString());
                        treeViewSupPerm.Nodes.Add(comClient.Text);
                        if (txtPermisionNum.Text != "")
                        {
                            OpenFileDialog openFileDialog1 = new OpenFileDialog();
                            if (openFileDialog1.ShowDialog() == DialogResult.OK)
                            {
                                string selectedFile = openFileDialog1.FileName;
                                byte[] selectedRequestImage = null;
                                selectedRequestImage = File.ReadAllBytes(selectedFile);
                                treeViewSupIdPerm.Nodes[0].Nodes.Add(comBranch.SelectedValue.ToString() + ":" + txtPermisionNum.Text);
                                treeViewSupPerm.Nodes[0].Nodes.Add(comBranch.Text + ":" + txtPermisionNum.Text);
                                imageListBoxControl1.Items.Add(selectedFile);
                                txtPermisionNum.Text = "";
                                return;
                            }
                            else
                            {
                                return;
                            }
                        }
                    }
                }
                #endregion
                else if (comResponsible.SelectedValue.ToString() == "1")
                {
                    if (treeViewSupIdPerm.Nodes.Count > 0)
                    {
                        for (int i = 0; i < treeViewSupIdPerm.Nodes.Count; i++)
                        {
                            if (storeId.ToString() == treeViewSupIdPerm.Nodes[i].Text)
                            {
                                for (int j = 0; j < treeViewSupIdPerm.Nodes[i].Nodes.Count; j++)
                                {
                                    if (txtPermisionNum.Text == treeViewSupIdPerm.Nodes[i].Nodes[j].Text)
                                    {
                                        MessageBox.Show("هذا الاذن تم اضافتة");
                                        return;
                                    }
                                }
                                if (txtPermisionNum.Text != "")
                                {
                                    OpenFileDialog openFileDialog1 = new OpenFileDialog();
                                    if (openFileDialog1.ShowDialog() == DialogResult.OK)
                                    {
                                        string selectedFile = openFileDialog1.FileName;
                                        byte[] selectedRequestImage = null;
                                        selectedRequestImage = File.ReadAllBytes(selectedFile);
                                        treeViewSupIdPerm.Nodes[i].Nodes.Add(txtPermisionNum.Text);
                                        treeViewSupPerm.Nodes[i].Nodes.Add(txtPermisionNum.Text);
                                        imageListBoxControl1.Items.Add(selectedFile);
                                        txtPermisionNum.Text = "";
                                        return;
                                    }
                                    else
                                    {
                                        return;
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        treeViewSupIdPerm.Nodes.Add(storeId.ToString());
                        treeViewSupPerm.Nodes.Add(storeName);
                        if (txtPermisionNum.Text != "")
                        {
                            OpenFileDialog openFileDialog1 = new OpenFileDialog();
                            if (openFileDialog1.ShowDialog() == DialogResult.OK)
                            {
                                string selectedFile = openFileDialog1.FileName;
                                byte[] selectedRequestImage = null;
                                selectedRequestImage = File.ReadAllBytes(selectedFile);
                                treeViewSupIdPerm.Nodes[0].Nodes.Add(txtPermisionNum.Text);
                                treeViewSupPerm.Nodes[0].Nodes.Add(txtPermisionNum.Text);
                                imageListBoxControl1.Items.Add(selectedFile);
                                txtPermisionNum.Text = "";
                                return;
                            }
                            else
                            {
                                return;
                            }
                        }
                    }
                }
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnDeleteNum_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("هل انت متاكد انك تريد الحذف؟", "", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                    return;

                removeCheckedNodes(treeViewSupPerm.Nodes);
                removeCheckedNodes(treeViewSupIdPerm.Nodes);
                for (int i = 0; i < treeViewSupIdPerm.Nodes.Count; i++)
                {
                    if (treeViewSupIdPerm.Nodes[i].Nodes.Count > 0)
                    { }
                    else
                    {
                        treeViewSupIdPerm.Nodes[i].Remove();
                        treeViewSupPerm.Nodes[i].Remove();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        
        private void treeViewSupPerm_AfterCheck(object sender, TreeViewEventArgs e)
        {
            IsAnyChildChecked(e.Node);
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (comReason.Text != "" && comType.Text != "")
                {
                    if (comSupplier.Visible == true)
                    {
                        if(comSupplier.SelectedValue == null)
                        {
                            MessageBox.Show("يجب اختيار المورد");
                            return;
                        }
                    }
                    conn.Open();
                    string query = "insert into gate (Reason,Type,Responsible,Car_ID,Car_Number,Driver_ID,Driver_Name,License_Number,Date_Enter,Store_ID,TatiqEmp_ID,Description,EnterEmployee_ID) values (@Reason,@Type,@Responsible,@Car_ID,@Car_Number,@Driver_ID,@Driver_Name,@License_Number,@Date_Enter,@Store_ID,@TatiqEmp_ID,@Description,@EnterEmployee_ID)";
                    MySqlCommand com = new MySqlCommand(query, conn);
                    com.Parameters.Add("@Reason", MySqlDbType.VarChar, 255);
                    com.Parameters["@Reason"].Value = comReason.Text;
                    com.Parameters.Add("@Type", MySqlDbType.VarChar, 255);
                    com.Parameters["@Type"].Value = comType.Text;
                    com.Parameters.Add("@Responsible", MySqlDbType.VarChar, 255);
                    com.Parameters["@Responsible"].Value = comResponsible.Text;
                    if (comCar.Text != "")
                    {
                        com.Parameters.Add("@Car_ID", MySqlDbType.Int16, 11);
                        com.Parameters["@Car_ID"].Value = comCar.SelectedValue.ToString();
                        com.Parameters.Add("@Car_Number", MySqlDbType.VarChar, 255);
                        com.Parameters["@Car_Number"].Value = comCar.Text;
                    }
                    else
                    {
                        com.Parameters.Add("@Car_ID", MySqlDbType.Int16, 11);
                        com.Parameters["@Car_ID"].Value = null;
                        com.Parameters.Add("@Car_Number", MySqlDbType.VarChar, 255);
                        com.Parameters["@Car_Number"].Value = txtCar.Text;
                    }
                    if (comDriver.Text != "")
                    {
                        com.Parameters.Add("@Driver_ID", MySqlDbType.Int16, 11);
                        com.Parameters["@Driver_ID"].Value = comDriver.SelectedValue.ToString();
                        com.Parameters.Add("@Driver_Name", MySqlDbType.VarChar, 255);
                        com.Parameters["@Driver_Name"].Value = comDriver.Text;
                    }
                    else
                    {
                        com.Parameters.Add("@Driver_ID", MySqlDbType.Int16, 11);
                        com.Parameters["@Driver_ID"].Value = null;
                        com.Parameters.Add("@Driver_Name", MySqlDbType.VarChar, 255);
                        com.Parameters["@Driver_Name"].Value = txtDriver.Text;
                    }
                    com.Parameters.Add("@License_Number", MySqlDbType.VarChar, 255);
                    com.Parameters["@License_Number"].Value = txtLicense.Text;
                    com.Parameters.Add("@Date_Enter", MySqlDbType.DateTime, 0);
                    com.Parameters["@Date_Enter"].Value = DateTime.Now;
                    
                    com.Parameters.Add("@Store_ID", MySqlDbType.Int16, 11);
                    com.Parameters["@Store_ID"].Value = storeId;
                    if (comEmployee.SelectedValue != null)
                    {
                        com.Parameters.Add("@TatiqEmp_ID", MySqlDbType.Int16, 11);
                        com.Parameters["@TatiqEmp_ID"].Value = comEmployee.SelectedValue.ToString();
                    }
                    else
                    {
                        com.Parameters.Add("@TatiqEmp_ID", MySqlDbType.Int16, 11);
                        com.Parameters["@TatiqEmp_ID"].Value = null;
                    }
                    com.Parameters.Add("@Description", MySqlDbType.VarChar, 255);
                    com.Parameters["@Description"].Value = txtDescription.Text;
                    com.Parameters.Add("@EnterEmployee_ID", MySqlDbType.Int16, 11);
                    com.Parameters["@EnterEmployee_ID"].Value = UserControl.EmpID;
                    
                    com.ExecuteNonQuery();

                    query = "select Gate_ID from gate order by Gate_ID desc limit 1";
                    com = new MySqlCommand(query, conn);
                    int permissionNum = Convert.ToInt32(com.ExecuteScalar().ToString());

                    for (int i = 0; i < treeViewSupIdPerm.Nodes.Count; i++)
                    {
                        query = "insert into gate_supplier(Gate_ID,Supplier_ID,Type) values(@Gate_ID,@Supplier_ID,@Type)";
                        com = new MySqlCommand(query, conn);
                        com.Parameters.Add("@Gate_ID", MySqlDbType.Int16, 11);
                        com.Parameters["@Gate_ID"].Value = permissionNum;
                        com.Parameters.Add("@Supplier_ID", MySqlDbType.Int16, 11);
                        com.Parameters["@Supplier_ID"].Value = treeViewSupIdPerm.Nodes[i].Text;
                        if (comClient.Visible == true)
                        {
                            com.Parameters.Add("@Type", MySqlDbType.VarChar, 255);
                            com.Parameters["@Type"].Value = "عميل";
                        }
                        else if (comSupplier.Visible == true)
                        {
                            com.Parameters.Add("@Type", MySqlDbType.VarChar, 255);
                            com.Parameters["@Type"].Value = "مورد";
                        }
                        else if (comStore.Visible == true)
                        {
                            com.Parameters.Add("@Type", MySqlDbType.VarChar, 255);
                            com.Parameters["@Type"].Value = "مخزن";
                        }
                        else if(comResponsible.SelectedValue.ToString() == "1" && comStore.Visible == false)
                        {
                            com.Parameters.Add("@Type", MySqlDbType.VarChar, 255);
                            com.Parameters["@Type"].Value = "مخزن";
                        }
                        com.ExecuteNonQuery();

                        query = "select GateSupplier_ID from gate_supplier order by GateSupplier_ID desc limit 1";
                        com = new MySqlCommand(query, conn);
                        int gateSupplierId = Convert.ToInt32(com.ExecuteScalar().ToString());

                        for (int j = 0; j < treeViewSupIdPerm.Nodes[i].Nodes.Count; j++)
                        {
                            query = "insert into gate_permission(GateSupplier_ID,Branch_ID,Supplier_PermissionNumber,Type,Permission_Image) values(@GateSupplier_ID,@Branch_ID,@Supplier_PermissionNumber,@Type,@Permission_Image)";
                            com = new MySqlCommand(query, conn);
                            com.Parameters.Add("@GateSupplier_ID", MySqlDbType.Int16, 11);
                            com.Parameters["@GateSupplier_ID"].Value = gateSupplierId;
                            if (comClient.Visible == true)
                            {
                                com.Parameters.Add("@Branch_ID", MySqlDbType.Int16, 11);
                                com.Parameters["@Branch_ID"].Value = treeViewSupIdPerm.Nodes[i].Nodes[j].Text.Split(':')[0];
                                com.Parameters.Add("@Supplier_PermissionNumber", MySqlDbType.Int32, 11);
                                com.Parameters["@Supplier_PermissionNumber"].Value = treeViewSupIdPerm.Nodes[i].Nodes[j].Text.Split(':')[1];
                            }
                            else
                            {
                                com.Parameters.Add("@Branch_ID", MySqlDbType.Int16, 11);
                                com.Parameters["@Branch_ID"].Value = null;
                                com.Parameters.Add("@Supplier_PermissionNumber", MySqlDbType.Int32, 11);
                                com.Parameters["@Supplier_PermissionNumber"].Value = treeViewSupIdPerm.Nodes[i].Nodes[j].Text;
                            }
                            com.Parameters.Add("@Type", MySqlDbType.VarChar, 255);
                            com.Parameters["@Type"].Value = "دخول";
                            com.Parameters.Add("@Permission_Image", MySqlDbType.LongBlob, 0);
                            com.Parameters["@Permission_Image"].Value = imageToByteArray(Image.FromFile(imageListBoxControl1.Items[i].Value.ToString()));
                            com.ExecuteNonQuery();
                        }
                    }

                    loaded = false;
                    flag = false;
                    flag2 = false;
                    clearAll();
                    loaded = true;

                    /*comType.Visible = false;
                    labelType.Visible = false;
                    labelType2.Visible = false;
                    comResponsible.Visible = false;
                    labelResponsible.Visible = false;
                    comEmployee.Visible = false;
                    labelEmp.Visible = false;
                    labelPerNum.Visible = false;
                    txtPermisionNum.Visible = false;
                    labelDriver.Visible = false;
                    comDriver.Visible = false;
                    txtDriver.Visible = false;
                    labelCar.Visible = false;
                    comCar.Visible = false;
                    txtCar.Visible = false;
                    txtLicense.Visible = false;
                    labelLicense.Visible = false;
                    txtDescription.Visible = false;
                    labelDescription.Visible = false;
                    btnAddNum.Visible = false;
                    treeViewSupPerm.Visible = false;
                    btnDeleteNum.Visible = false;
                    labelStore.Visible = false;
                    comStore.Visible = false;
                    labelSupplier.Visible = false;
                    comSupplier.Visible = false;
                    labelBranch.Visible = false;
                    comBranch.Visible = false;
                    labelClient.Visible = false;
                    comClient.Visible = false;*/
                }
                else
                {
                    MessageBox.Show("يجب ادخال البيانات الاساسية");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            conn.Close();
        }

        private void txtBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                XtraTabPage xtraTabPage = getTabPage("تسجيل دخول السيارات");
                if (!IsClear())
                    xtraTabPage.ImageOptions.Image = Properties.Resources.unsave;
                else
                    xtraTabPage.ImageOptions.Image = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //function
        void removeCheckedNodes(TreeNodeCollection nodes)
        {
            foreach (TreeNode node in nodes)
            {
                if (node.Checked)
                {
                    checkedNodes.Add(node);
                }
                else
                {
                    removeCheckedNodes(node.Nodes);
                }
            }
            foreach (TreeNode checkedNode in checkedNodes)
            {
                nodes.Remove(checkedNode);
            }
        }
        void IsAnyChildChecked(TreeNode node)
        {
            if (node.Checked)
            {
                try
                {
                    treeViewSupIdPerm.Nodes[node.Parent.Index].Nodes[node.Index].Checked = true;
                }
                catch
                {
                    treeViewSupIdPerm.Nodes[node.Index].Checked = true;
                }
            }
            else
            {
                try
                {
                    treeViewSupIdPerm.Nodes[node.Parent.Index].Nodes[node.Index].Checked = false;
                }
                catch
                {
                    treeViewSupIdPerm.Nodes[node.Index].Checked = false;
                }
            }
        }
        public byte[] imageToByteArray(System.Drawing.Image imageIn)
        {
            MemoryStream ms = new MemoryStream();
            imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
            return ms.ToArray();
        }

        public void clear()
        {
            foreach (Control co in this.panContent.Controls)
            {
                if (co is TextBox)
                {
                    co.Text = "";
                }
                else if (co is Panel)
                {
                    txtPermisionNum.Text = "";
                    treeViewSupIdPerm.Nodes.Clear();
                    treeViewSupPerm.Nodes.Clear();
                    int cont = imageListBoxControl1.ItemCount;
                    for (int i = 0; i < cont; i++)
                    {
                        imageListBoxControl1.Items.RemoveAt(0);
                    }
                }
            }
        }
        public void clearAll()
        {
            foreach (Control co in this.panContent.Controls)
            {
                if (co is System.Windows.Forms.ComboBox)
                {
                    co.Text = "";

                    //loaded = false;
                    //flag = false;
                    //flag2 = false;
                    //comDriver.SelectedIndex = -1;
                    //comCar.SelectedIndex = -1;
                    comEmployee.SelectedIndex = -1;
                    //comType.SelectedIndex = -1;
                    //comResponsible.SelectedIndex = -1;
                    //comReason.SelectedIndex = -1;

                    string query = "select Driver_ID,Driver_Name from drivers";
                    MySqlDataAdapter da = new MySqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    comDriver.DataSource = dt;
                    comDriver.DisplayMember = dt.Columns["Driver_Name"].ToString();
                    comDriver.ValueMember = dt.Columns["Driver_ID"].ToString();
                    comDriver.SelectedIndex = -1;

                    query = "select * from cars";
                    da = new MySqlDataAdapter(query, conn);
                    dt = new DataTable();
                    da.Fill(dt);
                    comCar.DataSource = dt;
                    comCar.DisplayMember = dt.Columns["Car_Number"].ToString();
                    comCar.ValueMember = dt.Columns["Car_ID"].ToString();
                    comCar.SelectedIndex = -1;
                    //flag2 = true;
                    //flag = true;
                    //loaded = true;
                }
                else if (co is TextBox)
                {
                    co.Text = "";
                }
                else if (co is Panel)
                {
                    comStore.SelectedIndex = -1;
                    comSupplier.SelectedIndex = -1;
                    comClient.SelectedIndex = -1;
                    comBranch.SelectedIndex = -1;
                    txtPermisionNum.Text = "";
                    treeViewSupIdPerm.Nodes.Clear();
                    treeViewSupPerm.Nodes.Clear();
                    int cont = imageListBoxControl1.ItemCount;
                    for (int i = 0; i < cont; i++)
                    {
                        imageListBoxControl1.Items.RemoveAt(0);
                    }
                }
            }
        }
        public XtraTabPage getTabPage(string text)
        {
            for (int i = 0; i < xtraTabControlStoresContent.TabPages.Count; i++)
                if (xtraTabControlStoresContent.TabPages[i].Text == text)
                {
                    return xtraTabControlStoresContent.TabPages[i];
                }
            return null;
        }

        public bool IsClear()
        {
            if (comReason.Text == "" && comType.Text == "" && comResponsible.Text == "" && comDriver.Text == "" && txtDriver.Text == "" && comCar.Text == "" && txtCar.Text == "" && txtLicense.Text == "" && comEmployee.Text == "" && comSupplier.Text == "" && comStore.Text == "" && comBranch.Text == "" && comClient.Text == "")
                return true;
            else
                return false;
        }

        /*private void txtSupplier_TextChanged(object sender, EventArgs e)
        {
            try
            {
                TextBox t = sender as TextBox;
                if (t != null)
                {
                    //say you want to do a search when user types 3 or more chars
                    if (t.Text.Length >= 2)
                    {
                        //SuggestStrings will have the logic to return array of strings either from cache/db
                        conn.Close();
                        conn.Open();
                        string query = "select Supplier_Name from supplier where Supplier_Name like '" + txtSupplier.Text + "%'";
                        MySqlCommand comand = new MySqlCommand(query, conn);
                        MySqlDataReader dr = comand.ExecuteReader();
                        while (dr.Read())
                        {
                            arr.Add(dr["Supplier_Name"].ToString());
                        }
                        dr.Close();
                        string[] strarr = new string[arr.Count];
                        arr.CopyTo(strarr);

                        AutoCompleteStringCollection collection = new AutoCompleteStringCollection();
                        collection.AddRange(strarr);

                        txtSupplier.AutoCompleteCustomSource = collection;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            conn.Close();
        }*/
    }
}

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
using System.Collections;
using MySql.Data.MySqlClient;
using System.IO;
using System.Reflection;

namespace TaxesSystem
{
    public partial class Permissions_Edit : Form
    {
        MySqlConnection conn, conn2;
        int permissionNum = 0;
        Gate_Out gateOut;
        List<TreeNode> checkedNodes = new List<TreeNode>();
        int storeId = 0;
        string storeName = "";

        public Permissions_Edit(Gate_Out GateOut, int PermissionNum)
        {
            InitializeComponent();
            conn = new MySqlConnection(connection.connectionString);
            conn2 = new MySqlConnection(connection.connectionString);
            permissionNum = PermissionNum;
            gateOut = GateOut;
        }

        private void Permissions_Edit_Load(object sender, EventArgs e)
        {
            try
            {
                conn2.Open();

                string query = "select * from supplier";
                MySqlDataAdapter da = new MySqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
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

                query = "SELECT gate_supplier.GateSupplier_ID,gate_supplier.Supplier_ID,supplier.Supplier_Name,gate_supplier.Type,gate.Type as 'Type2' FROM gate_supplier INNER JOIN supplier ON gate_supplier.Supplier_ID = supplier.Supplier_ID INNER JOIN gate ON gate.Gate_ID = gate_supplier.Gate_ID where gate_supplier.Type='مورد' and gate_supplier.Gate_ID=" + permissionNum + "  UNION ALL SELECT gate_supplier.GateSupplier_ID,gate_supplier.Supplier_ID,customer.Customer_Name,gate_supplier.Type,gate.Type as 'Type2' FROM gate_supplier INNER JOIN customer ON gate_supplier.Supplier_ID = customer.Customer_ID INNER JOIN gate ON gate.Gate_ID = gate_supplier.Gate_ID where gate_supplier.Type='عميل' and gate_supplier.Gate_ID=" + permissionNum + "  UNION ALL SELECT gate_supplier.GateSupplier_ID,gate_supplier.Supplier_ID,store.Store_Name,gate_supplier.Type,gate.Type as 'Type2' FROM gate_supplier INNER JOIN store ON gate_supplier.Supplier_ID = store.Store_ID INNER JOIN gate ON gate.Gate_ID = gate_supplier.Gate_ID where gate_supplier.Type='مخزن' and gate_supplier.Gate_ID=" + permissionNum + " ";
                com = new MySqlCommand(query, conn);
                MySqlDataReader dr = com.ExecuteReader();
                while (dr.Read())
                {
                    treeViewGatSupplierId.Nodes.Add(dr["GateSupplier_ID"].ToString());
                    treeViewSupIdPerm.Nodes.Add(dr["Supplier_ID"].ToString());
                    treeViewSupPerm.Nodes.Add(dr["Supplier_Name"].ToString());
                    if (dr["Type"].ToString() == "عميل")
                    {
                        labelBranch.Visible = true;
                        comBranch.Visible = true;
                        labelClient.Visible = true;
                        comClient.Visible = true;
                    }
                    else if (dr["Type"].ToString() == "مخزن" && dr["Type2"].ToString() == "مبيعات")
                    {
                        labelStore.Visible = false;
                        comStore.Visible = false;
                    }
                    else if (dr["Type"].ToString() == "مخزن")
                    {
                        labelStore.Visible = true;
                        comStore.Visible = true;
                    }
                    else if (dr["Type"].ToString() == "مورد")
                    {
                        labelSupplier.Visible = true;
                        comSupplier.Visible = true;
                    }

                    string query2 = "SELECT gate_permission.Branch_ID,branch.Branch_Name as 'الفرع',gate_permission.Supplier_PermissionNumber as 'رقم الاذن' FROM gate_permission INNER JOIN gate_supplier ON gate_supplier.GateSupplier_ID = gate_permission.GateSupplier_ID INNER JOIN gate ON gate.Gate_ID = gate_supplier.Gate_ID left join branch on branch.Branch_ID=gate_permission.Branch_ID where gate_supplier.Gate_ID=" + permissionNum + " and gate_supplier.GateSupplier_ID=" + dr["GateSupplier_ID"].ToString() +" and gate_permission.Type='خروج'";
                    MySqlCommand com2 = new MySqlCommand(query2, conn2);
                    MySqlDataReader dr2 = com2.ExecuteReader();
                    while (dr2.Read())
                    {
                        int n = treeViewSupIdPerm.Nodes.Count;
                        if (dr2["Branch_ID"].ToString() != "")
                        {
                            treeViewSupIdPerm.Nodes[n - 1].Nodes.Add(dr2["Branch_ID"].ToString() + ":" + dr2["رقم الاذن"].ToString());
                            treeViewSupPerm.Nodes[n - 1].Nodes.Add(dr2["الفرع"].ToString() + ":" + dr2["رقم الاذن"].ToString());
                        }
                        else
                        {
                            treeViewSupIdPerm.Nodes[n - 1].Nodes.Add(dr2["رقم الاذن"].ToString());
                            treeViewSupPerm.Nodes[n - 1].Nodes.Add(dr2["رقم الاذن"].ToString());
                        }
                    }
                    dr2.Close();
                }
                dr.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            conn.Close();
            conn2.Close();
        }

        private void btnAddNum_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
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
                                        conn.Close();
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
                                        
                                        string query = "insert into gate_permission(GateSupplier_ID,Branch_ID,Supplier_PermissionNumber,Type,Permission_Image) values(@GateSupplier_ID,@Branch_ID,@Supplier_PermissionNumber,@Type,@Permission_Image)";
                                        MySqlCommand com = new MySqlCommand(query, conn);
                                        com.Parameters.Add("@GateSupplier_ID", MySqlDbType.Int16, 11);
                                        com.Parameters["@GateSupplier_ID"].Value = treeViewGatSupplierId.Nodes[i].Text;
                                        com.Parameters.Add("@Branch_ID", MySqlDbType.Int16, 11);
                                        com.Parameters["@Branch_ID"].Value = null;
                                        com.Parameters.Add("@Supplier_PermissionNumber", MySqlDbType.Int16, 11);
                                        com.Parameters["@Supplier_PermissionNumber"].Value = txtPermisionNum.Text;
                                        com.Parameters.Add("@Type", MySqlDbType.VarChar, 255);
                                        com.Parameters["@Type"].Value = "خروج";
                                        com.Parameters.Add("@Permission_Image", MySqlDbType.LongBlob, 0);
                                        com.Parameters["@Permission_Image"].Value = selectedRequestImage;
                                        com.ExecuteNonQuery();
                                        txtPermisionNum.Text = "";
                                        conn.Close();
                                        return;
                                    }
                                    else
                                    {
                                        conn.Close();
                                        return;
                                    }
                                }
                            }

                        }
                        treeViewSupIdPerm.Nodes.Add(comSupplier.SelectedValue.ToString());
                        treeViewSupPerm.Nodes.Add(comSupplier.Text);

                        string query2 = "insert into gate_supplier(Gate_ID,Supplier_ID,Type) values(@Gate_ID,@Supplier_ID,@Type)";
                        MySqlCommand com2 = new MySqlCommand(query2, conn);
                        com2.Parameters.Add("@Gate_ID", MySqlDbType.Int16, 11);
                        com2.Parameters["@Gate_ID"].Value = permissionNum;
                        if (comSupplier.Visible == true)
                        {
                            com2.Parameters.Add("@Supplier_ID", MySqlDbType.Int16, 11);
                            com2.Parameters["@Supplier_ID"].Value = comSupplier.SelectedValue.ToString();
                            com2.Parameters.Add("@Type", MySqlDbType.VarChar, 255);
                            com2.Parameters["@Type"].Value = "مورد";
                        }
                        else if (comStore.Visible == true)
                        {
                            com2.Parameters.Add("@Supplier_ID", MySqlDbType.Int16, 11);
                            com2.Parameters["@Supplier_ID"].Value = comStore.SelectedValue.ToString();
                            com2.Parameters.Add("@Type", MySqlDbType.VarChar, 255);
                            com2.Parameters["@Type"].Value = "مخزن";
                        }
                        com2.ExecuteNonQuery();

                        query2 = "select GateSupplier_ID from gate_supplier order by GateSupplier_ID desc limit 1";
                        com2 = new MySqlCommand(query2, conn);
                        int gateSupplierId = Convert.ToInt32(com2.ExecuteScalar().ToString());
                        treeViewGatSupplierId.Nodes.Add(gateSupplierId.ToString());

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

                                string query = "insert into gate_permission(GateSupplier_ID,Branch_ID,Supplier_PermissionNumber,Type,Permission_Image) values(@GateSupplier_ID,@Branch_ID,@Supplier_PermissionNumber,@Type,@Permission_Image)";
                                MySqlCommand com = new MySqlCommand(query, conn);
                                com.Parameters.Add("@GateSupplier_ID", MySqlDbType.Int16, 11);
                                com.Parameters["@GateSupplier_ID"].Value = treeViewGatSupplierId.Nodes[n - 1].Text;
                                com.Parameters.Add("@Branch_ID", MySqlDbType.Int16, 11);
                                com.Parameters["@Branch_ID"].Value = null;
                                com.Parameters.Add("@Supplier_PermissionNumber", MySqlDbType.Int16, 11);
                                com.Parameters["@Supplier_PermissionNumber"].Value = txtPermisionNum.Text;
                                com.Parameters.Add("@Type", MySqlDbType.VarChar, 255);
                                com.Parameters["@Type"].Value = "خروج";
                                com.Parameters.Add("@Permission_Image", MySqlDbType.LongBlob, 0);
                                com.Parameters["@Permission_Image"].Value = selectedRequestImage;
                                com.ExecuteNonQuery();
                                txtPermisionNum.Text = "";
                                conn.Close();
                                return;
                            }
                            else
                            {
                                conn.Close();
                                return;
                            }
                        }
                    }
                    else
                    {
                        treeViewSupIdPerm.Nodes.Add(comSupplier.SelectedValue.ToString());
                        treeViewSupPerm.Nodes.Add(comSupplier.Text);

                        string query2 = "insert into gate_supplier(Gate_ID,Supplier_ID,Type) values(@Gate_ID,@Supplier_ID,@Type)";
                        MySqlCommand com2 = new MySqlCommand(query2, conn);
                        com2.Parameters.Add("@Gate_ID", MySqlDbType.Int16, 11);
                        com2.Parameters["@Gate_ID"].Value = permissionNum;
                        if (comSupplier.Visible == true)
                        {
                            com2.Parameters.Add("@Supplier_ID", MySqlDbType.Int16, 11);
                            com2.Parameters["@Supplier_ID"].Value = comSupplier.SelectedValue.ToString();
                            com2.Parameters.Add("@Type", MySqlDbType.VarChar, 255);
                            com2.Parameters["@Type"].Value = "مورد";
                        }
                        else if (comStore.Visible == true)
                        {
                            com2.Parameters.Add("@Supplier_ID", MySqlDbType.Int16, 11);
                            com2.Parameters["@Supplier_ID"].Value = comStore.SelectedValue.ToString();
                            com2.Parameters.Add("@Type", MySqlDbType.VarChar, 255);
                            com2.Parameters["@Type"].Value = "مخزن";
                        }
                        com2.ExecuteNonQuery();

                        query2 = "select GateSupplier_ID from gate_supplier order by GateSupplier_ID desc limit 1";
                        com2 = new MySqlCommand(query2, conn);
                        int gateSupplierId = Convert.ToInt32(com2.ExecuteScalar().ToString());
                        treeViewGatSupplierId.Nodes.Add(gateSupplierId.ToString());

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

                                string query = "insert into gate_permission(GateSupplier_ID,Branch_ID,Supplier_PermissionNumber,Type,Permission_Image) values(@GateSupplier_ID,@Branch_ID,@Supplier_PermissionNumber,@Type,@Permission_Image)";
                                MySqlCommand com = new MySqlCommand(query, conn);
                                com.Parameters.Add("@GateSupplier_ID", MySqlDbType.Int16, 11);
                                com.Parameters["@GateSupplier_ID"].Value = treeViewGatSupplierId.Nodes[0].Text;
                                com.Parameters.Add("@Branch_ID", MySqlDbType.Int16, 11);
                                com.Parameters["@Branch_ID"].Value = null;
                                com.Parameters.Add("@Supplier_PermissionNumber", MySqlDbType.Int16, 11);
                                com.Parameters["@Supplier_PermissionNumber"].Value = txtPermisionNum.Text;
                                com.Parameters.Add("@Type", MySqlDbType.VarChar, 255);
                                com.Parameters["@Type"].Value = "خروج";
                                com.Parameters.Add("@Permission_Image", MySqlDbType.LongBlob, 0);
                                com.Parameters["@Permission_Image"].Value = selectedRequestImage;
                                com.ExecuteNonQuery();
                                txtPermisionNum.Text = "";
                                conn.Close();
                                return;
                            }
                            else
                            {
                                conn.Close();
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
                                        conn.Close();
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

                                        string query = "insert into gate_permission(GateSupplier_ID,Branch_ID,Supplier_PermissionNumber,Type,Permission_Image) values(@GateSupplier_ID,@Branch_ID,@Supplier_PermissionNumber,@Type,@Permission_Image)";
                                        MySqlCommand com = new MySqlCommand(query, conn);
                                        com.Parameters.Add("@GateSupplier_ID", MySqlDbType.Int16, 11);
                                        com.Parameters["@GateSupplier_ID"].Value = treeViewGatSupplierId.Nodes[i].Text;
                                        com.Parameters.Add("@Branch_ID", MySqlDbType.Int16, 11);
                                        com.Parameters["@Branch_ID"].Value = comBranch.SelectedValue.ToString();
                                        com.Parameters.Add("@Supplier_PermissionNumber", MySqlDbType.Int16, 11);
                                        com.Parameters["@Supplier_PermissionNumber"].Value = txtPermisionNum.Text;
                                        com.Parameters.Add("@Type", MySqlDbType.VarChar, 255);
                                        com.Parameters["@Type"].Value = "خروج";
                                        com.Parameters.Add("@Permission_Image", MySqlDbType.LongBlob, 0);
                                        com.Parameters["@Permission_Image"].Value = selectedRequestImage;
                                        com.ExecuteNonQuery();
                                        txtPermisionNum.Text = "";
                                        conn.Close();
                                        return;
                                    }
                                    else
                                    {
                                        conn.Close();
                                        return;
                                    }
                                }
                            }

                        }
                        treeViewSupIdPerm.Nodes.Add(comClient.SelectedValue.ToString());
                        treeViewSupPerm.Nodes.Add(comClient.Text);

                        string query2 = "insert into gate_supplier(Gate_ID,Supplier_ID,Type) values(@Gate_ID,@Supplier_ID,@Type)";
                        MySqlCommand com2 = new MySqlCommand(query2, conn);
                        com2.Parameters.Add("@Gate_ID", MySqlDbType.Int16, 11);
                        com2.Parameters["@Gate_ID"].Value = permissionNum;
                        com2.Parameters.Add("@Supplier_ID", MySqlDbType.Int16, 11);
                        com2.Parameters["@Supplier_ID"].Value = comClient.SelectedValue.ToString();
                        com2.Parameters.Add("@Type", MySqlDbType.VarChar, 255);
                        com2.Parameters["@Type"].Value = "عميل";
                        com2.ExecuteNonQuery();

                        query2 = "select GateSupplier_ID from gate_supplier order by GateSupplier_ID desc limit 1";
                        com2 = new MySqlCommand(query2, conn);
                        int gateSupplierId = Convert.ToInt32(com2.ExecuteScalar().ToString());
                        treeViewGatSupplierId.Nodes.Add(gateSupplierId.ToString());

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

                                string query = "insert into gate_permission(GateSupplier_ID,Branch_ID,Supplier_PermissionNumber,Type,Permission_Image) values(@GateSupplier_ID,@Branch_ID,@Supplier_PermissionNumber,@Type,@Permission_Image)";
                                MySqlCommand com = new MySqlCommand(query, conn);
                                com.Parameters.Add("@GateSupplier_ID", MySqlDbType.Int16, 11);
                                com.Parameters["@GateSupplier_ID"].Value = treeViewGatSupplierId.Nodes[n - 1].Text;
                                com.Parameters.Add("@Branch_ID", MySqlDbType.Int16, 11);
                                com.Parameters["@Branch_ID"].Value = comBranch.SelectedValue.ToString();
                                com.Parameters.Add("@Supplier_PermissionNumber", MySqlDbType.Int16, 11);
                                com.Parameters["@Supplier_PermissionNumber"].Value = txtPermisionNum.Text;
                                com.Parameters.Add("@Type", MySqlDbType.VarChar, 255);
                                com.Parameters["@Type"].Value = "خروج";
                                com.Parameters.Add("@Permission_Image", MySqlDbType.LongBlob, 0);
                                com.Parameters["@Permission_Image"].Value = selectedRequestImage;
                                com.ExecuteNonQuery();
                                txtPermisionNum.Text = "";
                                conn.Close();
                                return;
                            }
                            else
                            {
                                conn.Close();
                                return;
                            }
                        }
                    }
                    else
                    {
                        treeViewSupIdPerm.Nodes.Add(comClient.SelectedValue.ToString());
                        treeViewSupPerm.Nodes.Add(comClient.Text);

                        string query2 = "insert into gate_supplier(Gate_ID,Supplier_ID,Type) values(@Gate_ID,@Supplier_ID,@Type)";
                        MySqlCommand com2 = new MySqlCommand(query2, conn);
                        com2.Parameters.Add("@Gate_ID", MySqlDbType.Int16, 11);
                        com2.Parameters["@Gate_ID"].Value = permissionNum;
                        com2.Parameters.Add("@Supplier_ID", MySqlDbType.Int16, 11);
                        com2.Parameters["@Supplier_ID"].Value = comClient.SelectedValue.ToString();
                        com2.Parameters.Add("@Type", MySqlDbType.VarChar, 255);
                        com2.Parameters["@Type"].Value = "عميل";
                        com2.ExecuteNonQuery();

                        query2 = "select GateSupplier_ID from gate_supplier order by GateSupplier_ID desc limit 1";
                        com2 = new MySqlCommand(query2, conn);
                        int gateSupplierId = Convert.ToInt32(com2.ExecuteScalar().ToString());
                        treeViewGatSupplierId.Nodes.Add(gateSupplierId.ToString());

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

                                string query = "insert into gate_permission(GateSupplier_ID,Branch_ID,Supplier_PermissionNumber,Type,Permission_Image) values(@GateSupplier_ID,@Branch_ID,@Supplier_PermissionNumber,@Type,@Permission_Image)";
                                MySqlCommand com = new MySqlCommand(query, conn);
                                com.Parameters.Add("@GateSupplier_ID", MySqlDbType.Int16, 11);
                                com.Parameters["@GateSupplier_ID"].Value = treeViewGatSupplierId.Nodes[0].Text;
                                com.Parameters.Add("@Branch_ID", MySqlDbType.Int16, 11);
                                com.Parameters["@Branch_ID"].Value = comBranch.SelectedValue.ToString();
                                com.Parameters.Add("@Supplier_PermissionNumber", MySqlDbType.Int16, 11);
                                com.Parameters["@Supplier_PermissionNumber"].Value = txtPermisionNum.Text;
                                com.Parameters.Add("@Type", MySqlDbType.VarChar, 255);
                                com.Parameters["@Type"].Value = "خروج";
                                com.Parameters.Add("@Permission_Image", MySqlDbType.LongBlob, 0);
                                com.Parameters["@Permission_Image"].Value = selectedRequestImage;
                                com.ExecuteNonQuery();
                                txtPermisionNum.Text = "";
                                conn.Close();
                                return;
                            }
                            else
                            {
                                conn.Close();
                                return;
                            }
                        }
                    }
                }
                #endregion
                else
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

                                        string query = "insert into gate_permission(GateSupplier_ID,Branch_ID,Supplier_PermissionNumber,Type,Permission_Image) values(@GateSupplier_ID,@Branch_ID,@Supplier_PermissionNumber,@Type,@Permission_Image)";
                                        MySqlCommand com = new MySqlCommand(query, conn);
                                        com.Parameters.Add("@GateSupplier_ID", MySqlDbType.Int16, 11);
                                        com.Parameters["@GateSupplier_ID"].Value = treeViewGatSupplierId.Nodes[i].Text;
                                        com.Parameters.Add("@Branch_ID", MySqlDbType.Int16, 11);
                                        com.Parameters["@Branch_ID"].Value = null;
                                        com.Parameters.Add("@Supplier_PermissionNumber", MySqlDbType.Int16, 11);
                                        com.Parameters["@Supplier_PermissionNumber"].Value = txtPermisionNum.Text;
                                        com.Parameters.Add("@Type", MySqlDbType.VarChar, 255);
                                        com.Parameters["@Type"].Value = "خروج";
                                        com.Parameters.Add("@Permission_Image", MySqlDbType.LongBlob, 0);
                                        com.Parameters["@Permission_Image"].Value = selectedRequestImage;
                                        com.ExecuteNonQuery();
                                        txtPermisionNum.Text = "";
                                        conn.Close();
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

                        string query2 = "insert into gate_supplier(Gate_ID,Supplier_ID,Type) values(@Gate_ID,@Supplier_ID,@Type)";
                        MySqlCommand com2 = new MySqlCommand(query2, conn);
                        com2.Parameters.Add("@Gate_ID", MySqlDbType.Int16, 11);
                        com2.Parameters["@Gate_ID"].Value = permissionNum;
                        com2.Parameters.Add("@Supplier_ID", MySqlDbType.Int16, 11);
                        com2.Parameters["@Supplier_ID"].Value = storeId;
                        com2.Parameters.Add("@Type", MySqlDbType.VarChar, 255);
                        com2.Parameters["@Type"].Value = "مخزن";
                        com2.ExecuteNonQuery();

                        query2 = "select GateSupplier_ID from gate_supplier order by GateSupplier_ID desc limit 1";
                        com2 = new MySqlCommand(query2, conn);
                        int gateSupplierId = Convert.ToInt32(com2.ExecuteScalar().ToString());
                        treeViewGatSupplierId.Nodes.Add(gateSupplierId.ToString());

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

                                string query = "insert into gate_permission(GateSupplier_ID,Branch_ID,Supplier_PermissionNumber,Type,Permission_Image) values(@GateSupplier_ID,@Branch_ID,@Supplier_PermissionNumber,@Type,@Permission_Image)";
                                MySqlCommand com = new MySqlCommand(query, conn);
                                com.Parameters.Add("@GateSupplier_ID", MySqlDbType.Int16, 11);
                                com.Parameters["@GateSupplier_ID"].Value = treeViewGatSupplierId.Nodes[0].Text;
                                com.Parameters.Add("@Branch_ID", MySqlDbType.Int16, 11);
                                com.Parameters["@Branch_ID"].Value = null;
                                com.Parameters.Add("@Supplier_PermissionNumber", MySqlDbType.Int16, 11);
                                com.Parameters["@Supplier_PermissionNumber"].Value = txtPermisionNum.Text;
                                com.Parameters.Add("@Type", MySqlDbType.VarChar, 255);
                                com.Parameters["@Type"].Value = "خروج";
                                com.Parameters.Add("@Permission_Image", MySqlDbType.LongBlob, 0);
                                com.Parameters["@Permission_Image"].Value = selectedRequestImage;
                                com.ExecuteNonQuery();
                                txtPermisionNum.Text = "";
                                conn.Close();
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            conn.Close();
        }

        private void btnDeleteNum_Click(object sender, EventArgs e)
        {
            /*try
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
            }*/
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            /*try
            {
                conn.Open();
                string query = "SELECT gate_supplier.GateSupplier_ID FROM gate_supplier INNER JOIN supplier ON gate_supplier.Supplier_ID = supplier.Supplier_ID INNER JOIN gate ON gate.Gate_ID = gate_supplier.Gate_ID where gate_supplier.Type='مورد' and gate_supplier.Gate_ID=" + permissionNum + "  UNION ALL SELECT gate_supplier.GateSupplier_ID FROM gate_supplier INNER JOIN customer ON gate_supplier.Supplier_ID = customer.Customer_ID INNER JOIN gate ON gate.Gate_ID = gate_supplier.Gate_ID where gate_supplier.Type='عميل' and gate_supplier.Gate_ID=" + permissionNum + "  UNION ALL SELECT gate_supplier.GateSupplier_ID FROM gate_supplier INNER JOIN store ON gate_supplier.Supplier_ID = store.Store_ID INNER JOIN gate ON gate.Gate_ID = gate_supplier.Gate_ID where gate_supplier.Type='مخزن' and gate_supplier.Gate_ID=" + permissionNum + " ";
                MySqlCommand com = new MySqlCommand(query, conn);
                MySqlDataReader dr = com.ExecuteReader();
                while (dr.Read())
                {
                    for (int i = 0; i < treeViewSupIdPerm.Nodes.Count; i++)
                    {
                        for (int j = 0; j < treeViewSupIdPerm.Nodes[i].Nodes.Count; j++)
                        {
                            query = "insert into gate_permission(GateSupplier_ID,Branch_ID,Supplier_PermissionNumber,Type,Permission_Image) values(@GateSupplier_ID,@Branch_ID,@Supplier_PermissionNumber,@Type,@Permission_Image)";
                            com = new MySqlCommand(query, conn);
                            com.Parameters.Add("@GateSupplier_ID", MySqlDbType.Int16, 11);
                            com.Parameters["@GateSupplier_ID"].Value = dr["GateSupplier_ID"].ToString();
                            if (comBranch.Visible == true)
                            {
                                com.Parameters.Add("@Branch_ID", MySqlDbType.Int16, 11);
                                com.Parameters["@Branch_ID"].Value = treeViewSupIdPerm.Nodes[i].Nodes[j].Text.Split(':')[0];
                                com.Parameters.Add("@Supplier_PermissionNumber", MySqlDbType.Int16, 11);
                                com.Parameters["@Supplier_PermissionNumber"].Value = treeViewSupIdPerm.Nodes[i].Nodes[j].Text.Split(':')[1];
                            }
                            else
                            {
                                com.Parameters.Add("@Branch_ID", MySqlDbType.Int16, 11);
                                com.Parameters["@Branch_ID"].Value = null;
                                com.Parameters.Add("@Supplier_PermissionNumber", MySqlDbType.Int16, 11);
                                com.Parameters["@Supplier_PermissionNumber"].Value = treeViewSupIdPerm.Nodes[i].Nodes[j].Text;
                            }
                            com.Parameters.Add("@Type", MySqlDbType.VarChar, 255);
                            com.Parameters["@Type"].Value = "خروج";
                            com.Parameters.Add("@Permission_Image", MySqlDbType.LongBlob, 0);
                            com.Parameters["@Permission_Image"].Value = imageToByteArray(Image.FromFile(imageListBoxControl1.Items[i].Value.ToString()));
                            com.ExecuteNonQuery();
                        }
                    }
                }
                dr.Close();
                conn.Close();
                gateOut.search();
                this.Hide();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            conn.Close();*/
        }

        public byte[] imageToByteArray(System.Drawing.Image imageIn)
        {
            MemoryStream ms = new MemoryStream();
            imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
            return ms.ToArray();
        }

        private void treeViewSupPerm_AfterCheck(object sender, TreeViewEventArgs e)
        {
            IsAnyChildChecked(e.Node);
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
                    treeViewSupPerm.Nodes[node.Index].Checked = false;
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
    }
}
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
using DevExpress.XtraTreeList.Nodes;
using DevExpress.XtraTreeList;

namespace MainSystem
{
    public partial class AdminControl1 : DevExpress.XtraEditors.XtraForm
    {
        MySqlConnection dbconnection;
        List<treelist> listOfTreeList;
        bool load = false;
        public AdminControl1()
        {
            try
            {
                InitializeComponent();
                dbconnection = new MySqlConnection(connection.connectionString);
                listOfTreeList = new List<treelist>();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
     
        }

        private void AdminControl_Load(object sender, EventArgs e)
        {
            try
            {
                dbconnection.Open();
                InitTree();
                string query = "SELECT Employee_Name,Employee_ID FROM employee where Department_ID <> 1";
                MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                DataTable dt = new DataTable();
                da.Fill(dt);
                comEmployee.DataSource = dt;
                comEmployee.DisplayMember = dt.Columns["Employee_Name"].ToString();
                comEmployee.ValueMember = dt.Columns["Employee_ID"].ToString();
                comEmployee.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }

        private void tileItem3_ItemClick(object sender, TileItemEventArgs e)
        {
            TileItem t = (TileItem)sender;
            string dID = t.Name.Split('D')[1];
            if (!t.Checked)
            {
                t.Checked = true;
                displayDepartmentGroups(dID);
                treelist treelist1 = new treelist();
                treelist1.id = dID;
                treelist1.TreeList = new TreeList();
                treelist1.TreeList=treeList1;
                listOfTreeList.Add(treelist1);
            }
            else
            {
                for (int i = 0; i < listOfTreeList.Count; i++)
                {
                    if (listOfTreeList[i].id == dID)
                    {
                        treeList1.DataSource = null;
                        treeList1.DataSource = listOfTreeList[i].TreeList.DataSource;
                        
                       this.Controls.Add(listOfTreeList[i].TreeList);
                     
                    }
                }
            }
        }

        private void rEmployee_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (rEmployee.Checked)
                {
                    dbconnection.Open();
                    string query = "SELECT Employee_Name,Employee_ID FROM employee";
                    MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    load = false;
                    comEmployee.DataSource = dt;
                    comEmployee.DisplayMember = dt.Columns["Employee_Name"].ToString();
                    comEmployee.ValueMember = dt.Columns["Employee_ID"].ToString();
                    comEmployee.Text = "";
                    load = true;
                    clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }

        private void rDelegate_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (rDelegate.Checked)
                {
                    dbconnection.Open();
                    string query = "SELECT Delegate_Name,Delegate_ID FROM delegate";
                    MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    load = false;
                    comEmployee.DataSource = dt;
                    comEmployee.DisplayMember = dt.Columns["Delegate_Name"].ToString();
                    comEmployee.ValueMember = dt.Columns["Delegate_ID"].ToString();
                    comEmployee.Text = "";
                    load = true;
                    clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }
        //function
        public void displayDepartmentGroups(string id)
        {
            string query = "select DepGroup_ID,DepGroup_Name from department_group where Department_ID=" + id;
            MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
            DataTable dt = new DataTable();
            da.Fill(dt);
            treeList1.DataSource = GetSource(dt);
        }

        internal void InitTree()
        {
            treeList1.ParentFieldName = "ParentId";
            treeList1.KeyFieldName = "UniqueId";
            treeList1.RootValue = string.Empty;
        }

        internal DataTable GetSource(DataTable dt1)
        {
            var dt = new DataTable();
            dt.Columns.Add("UniqueId", typeof(string));
            dt.Columns.Add("ParentId", typeof(string));
            dt.Columns.Add("MyData", typeof(string));

            //root nodes
            int y = 100;
            for (int i = 0; i < dt1.Rows.Count; i++)
            {
              dt.Rows.Add(dt1.Rows[i][0], string.Empty, dt1.Rows[i][1]);
                string query = "select Role_Name from grouprole where DepGroup_ID=" + dt1.Rows[i][0];
                MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                DataTable dt2 = new DataTable();
                da.Fill(dt2);
                int x = dt1.Rows.Count + i;
                for (int j = 0; j < dt2.Rows.Count ; j++)
                {
                    dt.Rows.Add(y, dt1.Rows[i][0], dt2.Rows[j][0]);
                    y++;
                }
            }
            return dt;
        }

        private void treeList1_BeforeCheckNode(object sender, DevExpress.XtraTreeList.CheckNodeEventArgs e)
        {
            TreeListNode node = e.Node;
            if (node.Checked)
            {
                node.UncheckAll();
            }
            else
            {
                node.CheckAll();
            }
            while (node.ParentNode != null)
            {
                node = node.ParentNode;
                bool oneOfChildIsChecked = OneOfChildsIsChecked(node);
                if (oneOfChildIsChecked)
                {
                    node.CheckState = CheckState.Checked;
                }
                else
                {
                    node.CheckState = CheckState.Unchecked;
                }
            }
        }

        private bool OneOfChildsIsChecked(TreeListNode node)
        {
            bool result = false;
            foreach (TreeListNode item in node.Nodes)
            {
                if (item.CheckState == CheckState.Checked)
                {
                    result = true;
                }
            }
            return result;
        }

        public void clear()
        {
            comEmployee.Text = "";
        }

        public struct treelist
        {
           public string id;
           public TreeList TreeList;
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
    }
}
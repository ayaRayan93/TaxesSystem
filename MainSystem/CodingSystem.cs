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
using DevExpress.XtraTab;
using DevExpress.XtraGrid;
using DevExpress.XtraTab.ViewInfo;
using DevExpress.XtraNavBar;
using MySql.Data.MySqlClient;

namespace MainSystem
{
    class CodingSystem
    {
    }
    public partial class MainForm
    {
        public static XtraTabControl tabControlBranch;

        public void initializeBranch()
        {
            //bankSystem
            tabControlBranch = xtraTabControlCoding;
        }
    }
}

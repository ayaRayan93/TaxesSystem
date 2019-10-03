using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainSystem
{
    class BaseData
    {
        static public string BranchID = "2";
        static public string StoreID = "1";
        static public string IPAddress = "197.50.31.80";
        static public bool connStatus = true;
        static public void generateBaseProjectFile()
        {   
            if (!File.Exists("IP_Address.txt"))
            {
                using (StreamWriter writer = new StreamWriter("Branch.txt"))
                {
                    writer.WriteLine(BranchID);
                }
                using (StreamWriter writer = new StreamWriter("Store.txt"))
                {
                    writer.WriteLine("1");
                }
                using (StreamWriter writer = new StreamWriter("backup.txt"))
                {
                    writer.WriteLine("1");
                }
                if (TestConnection(IPAddress))
                {
                    using (StreamWriter writer = new StreamWriter("IP_Address.txt"))
                    {
                        writer.WriteLine(IPAddress);
                    }
                    Login form = new Login();
                    form.ShowDialog();
                }
                else
                {
                    connStatus = false;
                    ChangeIP form = new ChangeIP();
                    form.ShowDialog();
                }

            }
            else
            {
                StoreID = File.ReadAllText("Store.txt");
                BranchID = File.ReadAllText("Branch.txt");
                IPAddress= File.ReadAllText("IP_Address.txt");
            }

            
        }
        static public bool TestConnection(string ip)
        {
            string connectionString = "SERVER=" + ip + ";DATABASE=cccmaindb;user=root;PASSWORD=A!S#D37;CHARSET=utf8;SslMode=none";

            MySqlConnection dbconnection = new MySqlConnection(connectionString);
            try
            {
                dbconnection.Open();
                dbconnection.Close();
                return true;
            }
            catch
            {
               return false;
            }
          
        }
    }
}

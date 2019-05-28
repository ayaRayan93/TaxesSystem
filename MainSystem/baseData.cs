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
        static public string BranchID = "1";
        static public string IPAddress = "192.168.1.200";
        static public void generateBaseProjectFile()
        {   
            if (!File.Exists("Branch.txt"))
            {
               // Directory.CreateDirectory(path);
                using (StreamWriter writer = new StreamWriter("Branch.txt"))
                {
                    writer.WriteLine(BranchID);
                }
                using (StreamWriter writer = new StreamWriter("IP_Address.txt"))
                {
                    writer.WriteLine(IPAddress);
                }
            }
            else
            {
                BranchID= File.ReadAllText("Branch.txt");
                IPAddress= File.ReadAllText("IP_Address.txt");
            }
        }

    }
}

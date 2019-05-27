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
            string path = "C:\\Users\\User\\Documents\\MainSystem";   
            if (!Directory.Exists(path + "\\Branch.txt"))
            {
                Directory.CreateDirectory(path);
                using (StreamWriter writer = new StreamWriter(path + "\\Branch.txt"))
                {
                    writer.WriteLine(BranchID);
                }
                using (StreamWriter writer = new StreamWriter(path + "\\IP_Address.txt"))
                {
                    writer.WriteLine(IPAddress);
                }
            }
            else
            {
                BranchID= File.ReadAllText("C:\\Users\\User\\Documents\\MainSystem\\Branch.txt");
                IPAddress= File.ReadAllText("C:\\Users\\User\\Documents\\MainSystem\\IP_Address.txt");
            }
        }

    }
}

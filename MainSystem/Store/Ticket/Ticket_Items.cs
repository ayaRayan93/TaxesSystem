using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainSystem
{
    public class Ticket_Items
    {
        string _Code;
        public int Data_ID { get; set; }
        public string Code {
            get { return _Code; }
            set { _Code = formatCode(value); }
        }
        public string Product_Name { get; set; }
        public string Cost { get; set; }
        public string Carton { get; set; }

        public string formatCode(string code)
        {
            char[] arr = new char[20];
            arr= code.ToCharArray();
            string c = "";
            int j = 1;
            for (int i = 0; i < arr.Length; i++)
            {
                c += arr[i].ToString();
                if (j % 4 == 0 && i != 19)
                    c += "-";

                j++;
            }
            return c;
        }
    }
}

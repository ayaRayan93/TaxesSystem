using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaxesSystem
{
    public class StorageReturn_Items
    {
        public string Code { get; set; }
        public string Product_Type { get; set; }
        public string Product_Name { get; set; }
        public double Balatat { get; set; }
        public double Carton_Balata { get; set; }
        public double Total_Meters { get; set; }
        public int Supplier_Permission_Number { get; set; }
        public string Date { get; set; }
        public string Reason { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainSystem
{
    public class Items_Storage
    {
        public string Code { get; set; }
        public string Product_Type { get; set; }
        public string Product_Name { get; set; }
        public double Quantity { get; set; }
        public string Store_Name { get; set; }
        public string Carton { get; set; }
    }

    public class Items_StorageWithPurshasesPrice
    {
        public string Code { get; set; }
        public string Product_Type { get; set; }
        public string Product_Name { get; set; }
        public double Quantity { get; set; }
        public string Store_Name { get; set; }
        public string Carton { get; set; }
        public string Purchases_Price { get; set; }
        public string Total { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainSystem
{
    public class SupplierBill_Items
    {
        public string Code { get; set; }
        public string Product_Type { get; set; }
        public string Product_Name { get; set; }
        public double Total_Meters { get; set; }
        public double PriceB { get; set; }
        public double Normal_Increase { get; set; }
        public double Categorical_Increase { get; set; }
        public double Last_Price { get; set; }
        public double Discount { get; set; }
        public double PriceA { get; set; }
    }
}

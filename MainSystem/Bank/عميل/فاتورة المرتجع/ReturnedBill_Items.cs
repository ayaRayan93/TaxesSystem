﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaxesSystem
{
    public class ReturnedBill_Items
    {
        public string Code { get; set; }
        public string Product_Type { get; set; }
        public string Type { get; set; }
        public string Product_Name { get; set; }
        public double Quantity { get; set; }
        public double CostBD { get; set; }
        public double Cost { get; set; }
        public double Discount { get; set; }
        public double Total_Cost { get; set; }
    }
}

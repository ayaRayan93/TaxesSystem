﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaxesSystem
{
    public class ReportOrderDetails_Items
    {
        public int ChildOrder_ID { get; set; }
        public string Code { get; set; }
        public string Product_Type { get; set; }
        public string Product_Name { get; set; }
        public int Balatat { get; set; }
        public double Total_Meters { get; set; }
    }
}

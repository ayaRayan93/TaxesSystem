using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaxesSystem
{
    public class Item_Transitions
    {
        public string Note { get; set; }
        public string Bill { get; set; }
        public string Date { get; set; }
        public string Client { get; set; }
        public double Item_Increase { get; set; }
        public double Item_Decrease { get; set; }
        public double Price { get; set; }
        public double Total_Cost { get; set; }
    }
}

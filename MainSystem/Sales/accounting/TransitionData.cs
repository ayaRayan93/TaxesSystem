using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainSystem.Sales.accounting
{
   public class TransitionData
    {
        public int ID { get; set; }
        public string Operation_Type { get; set; }
        public string Type { get; set; }
        public string Bill_Number { get; set; }
        public string Date { get; set; }
        public string Client { get; set; }
        public double CostSale { get; set; }
        public double CostReturn { get; set; }
        public string Description { get; set; }
    }
}

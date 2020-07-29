using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaxesSystem.Sales.accounting
{
   public class TransitionData
    {
        public string Date { get; set; }
        public string Operation_Type { get; set; }
        public string ClientCode { get; set; }
        public string Client { get; set; }
        public int ID { get; set; }
        public double Returned { get; set; }
        public double Paid { get; set; }
    }
}

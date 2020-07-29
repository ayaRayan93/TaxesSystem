using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaxesSystem.Sales.accounting
{
   public class customerAccount
    {
        public int ID { get; set; }
        public string ClientCode { get; set; }
        public string Client { get; set; }
        public string CustomerCode { get; set; }
        public string CustomerName { get; set; }
        public double ReturnedBill { get; set; }
        public double Bill { get; set; }
        public double Returned { get; set; }
        public double Paid { get; set; }
    }
}

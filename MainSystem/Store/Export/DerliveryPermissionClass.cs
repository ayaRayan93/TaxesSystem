using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaxesSystem
{
    public class DeliveryPermissionClass
    {
        public int ID { get; set; }
        public int Data_ID { get; set; }
        public string Type { get; set; }
        public string ItemName { get; set; }
        public string Code { get; set; }
        public double NumOfCarton { get; set; }
        public double TotalQuantity { get; set; }
        public string DeliveryQuantity { get; set; }
        public string StoreName { get; set; }
    }
}

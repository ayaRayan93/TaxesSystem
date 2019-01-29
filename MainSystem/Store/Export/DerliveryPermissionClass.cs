using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainSystem.Store.Export
{
    class DerliveryPermissionClass
    {
        public int Data_ID { get; set; }
        public string ItemName { get; set; }
        public string Code { get; set; }
        public double Carton { get; set; }
        public double TotalQuantity { get; set; }
        public double DeliveryQuantity { get; set; }
    }
}

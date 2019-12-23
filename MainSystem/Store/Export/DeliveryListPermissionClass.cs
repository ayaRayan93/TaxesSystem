using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainSystem.Store.Export
{
   public class DeliveryListPermissionClass
    {
        public int Customer_Permissin_ID { get; set; }
        public int BranchBillNumber { get; set; }
        public string Branch_Name { get; set; }
        public string Store_Name { get; set; }
        public string StoreKeeper { get; set; }
        public string DeliveredPerson { get; set; }
        public string Note { get; set; }
        public List<DeliveryPermissionClass> DeliveryPermissionClassList { get; set; }
    }
}

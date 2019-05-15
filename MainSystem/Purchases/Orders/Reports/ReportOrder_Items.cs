using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainSystem
{
    public class ReportOrder_Items
    {
        public int Order_ID { get; set; }
        public string Factory_Name { get; set; }
        public int Order_Number { get; set; }
        public string Employee_Name { get; set; }
        public string Store_Name { get; set; }
        public List<ReportOrderDetails_Items> items { get; set; }
    }
}

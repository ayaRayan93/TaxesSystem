using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainSystem
{
    public class ExpensesTransition_Items
    {
        public int ID { get; set; }
        public string Date { get; set; }
        public string Type { get; set; }
        public string DepositorName { get; set; }
        public string MainExpense_Name { get; set; }
        public string SubExpense_Name { get; set; }
        public string DetailsExpense_Name { get; set; }
        public double IncomeAmount { get; set; }
        public double ExpenseAmount { get; set; }
        public string Employee_Name { get; set; }
        public string Description { get; set; }
    }
}

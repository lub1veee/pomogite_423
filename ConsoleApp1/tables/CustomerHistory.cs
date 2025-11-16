using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.tables
{
    internal class CustomerHistory
    {
        public int Id { get; set; }

        public int Day { get; set; }

        public int DetailNeeded { get; set; }

        public decimal RepairPrice { get; set; }

        public string Status { get; set; } = null!;

        public decimal Earnings { get; set; }

        public virtual Detail DetailNeededNavigation { get; set; } = null!;
    }
    
}

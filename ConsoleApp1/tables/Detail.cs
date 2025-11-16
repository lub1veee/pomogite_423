using ConsoleApp1.tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Detail
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public decimal Price { get; set; }

        public decimal RepairFee { get; set; }

        public int Quantity { get; set; }

        public virtual ICollection<CustomerHistory> CustomerHistories { get; set; } = new List<CustomerHistory>();

        public virtual ICollection<PurchaseOrder> PurchaseOrders { get; set; } = new List<PurchaseOrder>();

    }
}

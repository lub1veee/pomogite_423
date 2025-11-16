using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.tables
{
    internal class PurchaseOrder
    {
        public int Id { get; set; }

        public int PartId { get; set; }

        public int Quantity { get; set; }

        public decimal Cost { get; set; }

        public int DeliveryIn { get; set; }

        public DateTime? CreatedAt { get; set; }

        public virtual Detail Detail { get; set; } = null!;
    }
}

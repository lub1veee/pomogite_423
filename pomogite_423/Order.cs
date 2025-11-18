using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Order
    {
        private static int LastId = 0;

        public int Id;
        public Detail detail;
        public int TurnsToDelive;
        public int PartQuantity;

        public Order(Detail detail, int quantity)
        {
            Id = LastId++;
            Detail = detail;
            PartQuantity = quantity;
            TurnsToDelive = 2;

            Console.WriteLine($"Заказ {Id} сформирован!");
        }

        public void GetInfo()
        {
            Console.WriteLine($"{Id}. {Detail}: {PartQuantity} шт.\nБудет доставлено через {TurnsToDelive} д.");
        }
    }
}

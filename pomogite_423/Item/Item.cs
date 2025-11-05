using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pomogite_423
{
    internal class Item
    {
        public string Name;
        public int Durability;

        public Item(string name = "", int durability = 100)
        {
            Name = name;
            Durability = durability;
        }
    }
}

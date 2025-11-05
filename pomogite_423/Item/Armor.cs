using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pomogite_423
{
    internal class Armor : Item
    {
        public static List<Armor> AllArmor = new();

        public int Protection;

        public Armor(string name = "", int durability = 100, int protection = 1) : base(name, durability)
        {
            Name = name;
            Durability = durability;
            Protection = protection;
        }

        public void Info()
        {
            Console.WriteLine($"Броня: {Name}\n    Прочность: {Durability}\n    Защита: {Protection}");
        }
    }
}
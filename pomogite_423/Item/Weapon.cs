using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pomogite_423
{
    internal class Weapon : Item
    {
        public static List<Weapon> AllWeapon = new();

        public int Damage;

        public Weapon(string name = "", int durability = 100, int damage = 5) : base(name, durability)
        {
            Name = name;
            Durability = durability;
            Damage = damage;
        }

        public void Info()
        {
            Console.WriteLine($"Оружие: {Name}\n    Прочность: {Durability}\n    Урон: {Damage}");
        }
    }
}

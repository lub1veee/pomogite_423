using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pomogite_423
{
    public class Ram
    {
        public string Name;
        public int Age;
        public int Weight;

        public Ram(string name, int age, int weight) 
        {
            Name = name;
            Age = age;
            Weight = weight;

        }
        public void Info()
        {
            Console.WriteLine($"Имя: {Name}");
            Console.WriteLine($"Возраст: {Age}");
            Console.WriteLine($"Вес : {Weight}");
        }
    }
}

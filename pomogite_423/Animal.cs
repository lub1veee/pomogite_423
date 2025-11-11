using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pomogite_423
{
    public abstract class Animal
    {
        public string Name;
        public int Age;
        public int Weight;
        public string AnimalType;
       

        public void Info()
        {
            Console.WriteLine($"Имя {Name}");
            Console.WriteLine($"Тип {AnimalType}");
            Console.WriteLine($"Возраст {Age}");
            Console.WriteLine($"Вес: {Weight}");
        }
    }
}

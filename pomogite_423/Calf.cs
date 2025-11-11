using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pomogite_423
{
    public class Calf : Animal
    {
        public Calf(string name, int age, int weight)
        {
            Name = name;
            Age = age;
            Weight = weight;
            AnimalType = "Теленок";
        }
    }
}

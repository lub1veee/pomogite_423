using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pomogite_423
{
    public class Cow : Animal
    {
        public Cow(string name, int age, int weight)
        {
            Name = name;
            Age = age;
            Weight = weight;
            AnimalType = "Корова";
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pomogite_423
{
    internal class Farm
    {
        public List<Animal> Animals = new List<Animal>();

        public void ShowMenu()
        {
            Console.WriteLine("Какое действие?");
            Console.WriteLine("1 - добавить корову");
            Console.WriteLine("2 - добавить теленка");
            Console.WriteLine("3 - Вывод животных");
        }

        public void AllAnimal()
        {
            var sortedAnimals = Animals.OrderBy(a => a is Calf ? 1 : 0);

            Console.WriteLine("Все животные: ");
            foreach (var animal in sortedAnimals)
            {
                animal.Info();
            }
        }

        public void AddCow()
        {
            Console.WriteLine("Введите имя коровы");
            string name = Console.ReadLine();

            Console.WriteLine("Введите возраст коровы");
            int age = int.Parse(Console.ReadLine());

            Console.WriteLine("Введите вес коровы");
            int weight = int.Parse(Console.ReadLine());

            Animals.Add(new Cow(name, age, weight));

            Console.WriteLine("Корова Успешно добавлена");
        }

        public void AddCalf()
        {
            Console.WriteLine("Введите имя теленка");
            string name = Console.ReadLine();

            Console.WriteLine("Введите возраст теленка");
            int age = int.Parse(Console.ReadLine());

            Console.WriteLine("Введите вес теленка");
            int weight = int.Parse(Console.ReadLine());

            Animals.Add(new Calf(name, age, weight));

            Console.WriteLine("Теленок Успешно добавлен");
        }

        public void Initialize()
        {

            Animals.Add(new Cow("Мурка", 15, 700));
            Animals.Add(new Cow("Шурка", 10, 600));
            Animals.Add(new Calf("Уголек", 5, 2000));
            Animals.Add(new Calf("Z", 7, 100));
        }
    }
}

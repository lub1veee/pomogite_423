using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Threading.Tasks;

namespace pomogite_423
{
    public class Farm
    {
        public List<Ram> Rams = new List<Ram>();

        public void ShowRam()
        {
            var sortedRam = Rams.OrderBy(r => r.Name);
            foreach (var ram in sortedRam)
            {
                ram.Info();
            }

        }

        public void AddRam()
        {
            Console.WriteLine("Введите имя:");
            string name = Console.ReadLine();

            Console.WriteLine("Введите возраст: ");
            int age = int.Parse(Console.ReadLine());

            Console.WriteLine("Введите вес:");
            int weight = int.Parse(Console.ReadLine());

            Rams.Add(new Ram(name, age, weight));
        }

        public void DelRam()
        {
            Console.WriteLine("Введите имя барана");
            string nameRam = Console.ReadLine();
            Ram delRam = Rams.FirstOrDefault(r => r.Name == nameRam);
            if (delRam != null)
            {
                Rams.Remove(delRam);
                Console.WriteLine("Баран удален");
            }

            else { Console.WriteLine("Баран не найден"); }
            
        }
        

        public void StartRam()
        {
            Rams.Add(new Ram("Уголек", 15, 200));
            Rams.Add(new Ram("Ситочко", 5, 50));
            Rams.Add(new Ram("Ликерчик", 14, 690));
            Rams.Add(new Ram("Пирожок", 2, 5000));

        }

        public void ShowMenu()
        {
            Console.WriteLine("1 - Добавить барана");
            Console.WriteLine("2 - Удалить бара по имени");
            Console.WriteLine("3 - Вывод всех баранов");
            Console.WriteLine("0 - Выход из программы");
        }
    }
}

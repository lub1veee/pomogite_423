using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace pomogite_423
{
    internal class Factory
    {

        public static Enemy RandomEnemy(string name)
        {
            return name.ToLower() switch
            {
                "скелет" => new Skeleton(),
                "маг" => new Mage(),
                "гоблин" => new Goblin(),
                "слайми" => new Slime(),
                "ввг" => new VVG(),
                "ковальский" => new Kovalsky(),
                "пестов" => new PestovCMM(),
                "архимаг" => new ArchimageCPP()
            };
        }

        public static Enemy RandomMonster()
        {
            string[] monsters = ["Скелет", "Маг", "Гоблин", "Слайми"];
            return RandomEnemy(monsters[StaticRandom.random.Next(monsters.Length)]);
        }

        public static Enemy RandomBoss()
        {
            string[] bosses = ["ВВГ", "Ковальский", "Пестов", "Архимаг"];
            return RandomEnemy(bosses[StaticRandom.random.Next(bosses.Length)]);
        }

    }
}

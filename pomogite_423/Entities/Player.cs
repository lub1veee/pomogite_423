using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pomogite_423
{
    internal class Player : Entity
    {
        public Armor ArmorPlayer;
        public Weapon WeaponPlayer;
#pragma warning disable
        public static Player Instance;

        private bool _armorBroken = false;

        public bool IsFreezed = false;

        public Player(Random random, Armor armorPlayer, Weapon weaponPlayer, int hp = 100, int damage = 5) : base(random, hp, damage)
        {
            Hp = hp;
            Damage = damage;
            ArmorPlayer = armorPlayer;
            WeaponPlayer = weaponPlayer;

            _random = random;
            Initialize();
        }

        public void Initialize()
        {
            if(Instance == null)
            {
                Instance = this;
            }
        }

        public void BrakeArmorAndGetDamage(int damage )
        {
            _armorBroken = true;
            GetDamage(damage);
        }

        public override int GetDamage(int damage = 10)
        {
            if (!_armorBroken)
            {
                damage -= ArmorPlayer.Protection;
                if (damage <= 0) damage = 1;
                Hp -= damage;
                if (Hp <= 0) Hp = 0;
                ArmorPlayer.Durability--;
            }
            else
            {
                Hp -= damage;
                Console.WriteLine("Броня сломана!");
            }
            Console.WriteLine($"ЗАЩИТА\n" + $"{Program.Separator}\n" +
                $"Полученный урон: {damage}"
                );
            if (Hp < 0) Hp = 0;
            Console.WriteLine($"Здоровье {Hp}/{MaxHp}");
            _armorBroken = false;
            return damage;
        }

        public void AttackEnemy(Enemy enemy)
        {
            Console.WriteLine($"АТАКА\n" + $"{Program.Separator}\n" +
                $"Нанесенный урон: " +
                $"{enemy.GetDamage(WeaponPlayer.Damage)}"
                );
            Console.WriteLine($"{enemy.Name} - Оставшееся здоровье: {enemy.Hp}");
            WeaponPlayer.Durability--;
        }

        public void ShowStats()
        {
            Console.WriteLine($"\nИГРОК\n{Program.Separator}\nЗдоровье: {Hp}/{MaxHp}");
            ArmorPlayer.Info();
            WeaponPlayer.Info();
            Console.WriteLine(Program.Separator);
        }

        public void Heal()
        {
            Hp = MaxHp;
            Console.WriteLine("Здоровье восстановлено!");
        }
    }
}

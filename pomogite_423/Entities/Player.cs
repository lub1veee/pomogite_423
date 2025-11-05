using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pomogite_423.Entities
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




        public override void GetDamage(int damage = 10)
        {
            if (!_armorBroken)
            {
                Hp -= damage - ArmorPlayer.Protection;
                ArmorPlayer.Durability--;
            }
            else
            {
                Hp -= damage;
                Console.WriteLine("Броня сломана!");
            }

            _armorBroken = false;
        }

        public void AttackEnemy(Enemy enemy)
        {
            enemy.GetDamage(WeaponPlayer.Damage);
            WeaponPlayer.Durability--;
        }
    }
}

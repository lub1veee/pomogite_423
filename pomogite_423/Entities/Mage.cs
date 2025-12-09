using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pomogite_423
{
    internal class Mage : Enemy
    {
        protected double _freezeChance;
        public Mage()
        {
            Name = "Маг";
            Hp = 23;
            Damage = 12;
            Protection = 4;
            _freezeChance = 0.1;
        }

        public override void AttackPlayer()
        {
            if(StaticRandom.random.NextDouble() <= _freezeChance)
            {
                Player.Instance.IsFreezed = true;
                Console.WriteLine($"{Name} заморозил вас!\nВы пропустите ход!\n{Program.Separator}");
            }

            Player.Instance.GetDamage(Damage);  
        }
    }
}

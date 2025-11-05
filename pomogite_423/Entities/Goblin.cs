using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pomogite_423.Entities
{   
    internal class Goblin : Enemy
    {
        protected double CritChance;
        public Goblin(Random random) : base(random)
        {
            Name = "Гоблин";
            Hp = 30;
            Damage = 15;
            Protection = 7;
            CritChance = 0.2;

            _random = random;
        }

        public override void AttackPlayer()
        {
            if (_random.NextDouble() <= CritChance) Player.Instance.GetDamage(Damage * 2);
            else Player.Instance.GetDamage(Damage);
        }
    }
}

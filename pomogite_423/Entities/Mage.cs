using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pomogite_423.Entities
{
    internal class Mage : Enemy
    {
        protected double _freezeChance;
        public Mage(Random random) : base(random)
        {
            Name = "Маг";
            Hp = 42;
            Damage = 12;
            Protection = 4;
            _freezeChance = 0.1;
        }

        public override void AttackPlayer()
        {
            if(_random.NextDouble() <= _freezeChance) Player.Instance.IsFreezed = true;

            Player.Instance.GetDamage(Damage);  
        }
    }
}

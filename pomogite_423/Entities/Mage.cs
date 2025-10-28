using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pomogite_423.Entities
{
    internal class Mage : Enemy
    {
        private double _freezeChance;

        Random random = new Random();
        public Mage()
        {
            Name = "Маг";
            Hp = 42;
            Damage = 12;
            Protection = 4;
            _freezeChance = 0.1;
        }

        public override void AttackPlayer()
        {
            if(random.NextDouble() <= _freezeChance) Player.Instance.IsFreezed = true;

            Player.Instance.GetDamage(Damage);  
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pomogite_423.Entities.Bosses
{
    internal class PestovCMM : Skeleton
    {
        private double _freezeChance;
        public PestovCMM(Random random) : base(random)
        {
            Skeleton pestov = new Skeleton(random);
            Hp = (int)(pestov.Hp * 1.3);
            Damage = (int)(pestov.Damage * 1.8);
            Protection = (int)(pestov.Protection * 0.6);
            
            _freezeChance = 0.1 + 0.15;
        }

        public override void AttackPlayer()
        {
            Player.Instance.BrakeArmorAndGetDamage(Damage);
            if (_random.NextDouble() <= _freezeChance) Player.Instance.IsFreezed = true;
        }
    }
}

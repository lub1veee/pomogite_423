using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pomogite_423
{
    internal class PestovCMM : Skeleton
    {
        private double _freezeChance;
        public PestovCMM()
        {
            Skeleton pestov = new Skeleton();
            Name = "Босс Пестов С--";
            Hp = (int)(pestov.Hp * 1.3);
            Damage = (int)(pestov.Damage * 1.8);
            Protection = (int)(pestov.Protection * 0.6);
            
            _freezeChance = 0.1 + 0.15;
        }

        public override void AttackPlayer()
        {
            Player.Instance.BrakeArmorAndGetDamage(Damage);
            if (StaticRandom.random.NextDouble() <= _freezeChance)
            {
                Player.Instance.IsFreezed = true;
                Console.WriteLine($"{Name} заморозил вас!\nВы пропустите ход!\n{Program.Separator}");
            }
        }
    }
}

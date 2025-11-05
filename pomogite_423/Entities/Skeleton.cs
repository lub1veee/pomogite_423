using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pomogite_423.Entities
{
    internal class Skeleton : Enemy
    {
        public Skeleton(Random random) : base(random)
        {
            Name = "Скелет";
            Hp = 25;
            Damage = 10;
            Protection = 5;
        }

        public override void AttackPlayer() =>
            Player.Instance.BrakeArmorAndGetDamage(Damage);
    }
}

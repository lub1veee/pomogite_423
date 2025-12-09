using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pomogite_423
{
    internal class Skeleton : Enemy
    {
        public Skeleton()
        {
            Name = "Скелет";
            Hp = 25;
            Damage = 7;
            Protection = 3;
        }

        public override void AttackPlayer() =>
            Player.Instance.BrakeArmorAndGetDamage(Damage);
    }
}

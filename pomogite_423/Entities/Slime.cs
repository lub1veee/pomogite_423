using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pomogite_423
{
    internal class Slime : Enemy
    {
        public Slime() 
        {
            Name = "Слайми";
            Hp = 20;
            Damage = 6;
            Protection = 5;
        }
        public override void AttackPlayer()
        {
            Player.Instance.GetDamage(Damage);
        }
    }
}

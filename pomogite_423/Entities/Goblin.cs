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
        Random random = new();
        public Goblin() : base()
        {
            Name = "Гоблин";
            Hp = 30;
            Damage = 15;
            Protection = 7;
            CritChance = 0.2;
        }

        public override void AttackPlayer()
        {
            if (random.NextDouble() <= CritChance) Player.Instance.GetDamage(Damage * 2);
            else Player.Instance.GetDamage(Damage);
        }
    }
}

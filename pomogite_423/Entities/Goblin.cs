using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pomogite_423.Entities
{
    internal class Goblin : Enemy
    {
        private double _critChance;
        Random random = new();
        public Goblin(int hp = 30, int damage = 10, int protection = 7, double critChance = 0.2)
        {
            Hp = hp;
            Damage = damage;
            Protection = protection;
            _critChance = critChance;
        }

        public override void AttackPlayer()
        {
            if (random.NextDouble() <= _critChance) Player.Instance.GetDamage(Damage * 2);
            else base.AttackPlayer();
        }
    }
}

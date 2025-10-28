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
        public Goblin() : base()
        {
            Name = "Гоблин";
            Hp = 30;
            Damage = 15;
            Protection = 7;
            _critChance = 0.2;
        }

        public override void AttackPlayer()
        {
            if (random.NextDouble() <= _critChance) Player.Instance.GetDamage(Damage * 2);
            else Player.Instance.GetDamage(Damage);
        }
    }
}

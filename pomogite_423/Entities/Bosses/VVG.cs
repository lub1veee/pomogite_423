using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pomogite_423
{
    internal class VVG : Goblin
    {
        public VVG(Random random) : base(random)
        {
            Goblin goblin = new Goblin(random);
            Name = "Босс VVG";
            Hp = (int)(goblin.Hp * 2.0);
            Damage = (int)(goblin.Damage * 1.5);
            Protection = (int)(goblin.Protection * 1.2);
            CritChance = CritChance * 1.1;
        }
    }
}

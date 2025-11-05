using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pomogite_423
{
    internal class Kovalsky : Skeleton
    {
        public Kovalsky(Random random) : base(random) 
        {
            Skeleton skeleton = new Skeleton(random);
            Name = "Босс Ковальский";
            Hp = (int)(skeleton.Hp * 2.5);
            Damage = (int)(skeleton.Damage * 1.3);
            Protection = (int)(skeleton.Protection * 1.4);
        }
    }
}

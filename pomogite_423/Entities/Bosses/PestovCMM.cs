using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pomogite_423.Entities.Bosses
{
    internal class PestovCMM : Skeleton
    {
        public PestovCMM()
        {
            Skeleton pestov = new Skeleton();
            Hp = (int)(pestov.Hp * 1.3);
            Damage = (int)(pestov.Damage * 1.8);
            Protection = (int)(pestov.Protection * 0.6);

        }
    }
}

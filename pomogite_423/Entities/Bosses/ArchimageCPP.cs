using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pomogite_423.Entities.Bosses
{
    internal class ArchimageCPP : Mage
    {
        public ArchimageCPP()
        {
            Mage mage = new Mage();
            Hp = (int)(mage.Hp * 1.8);
            Damage = (int)(mage.Damage * 1.6);
            Protection = (int)(mage.Protection * 1.1);

            _freezeChance += 0.1;
        }
    }
}

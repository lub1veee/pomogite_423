using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pomogite_423.Entities
{
    internal abstract class Enemy : Entity
    {
        public string Name;

        public void AttackPlayer()
        {
            Player.Instance.GetDamage(Damage);
        }
        public 
    }
}

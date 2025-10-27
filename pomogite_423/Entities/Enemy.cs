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
        public int Protection;
        public int ArmDamage;

        public virtual void AttackPlayer()
        {
            Player.Instance.GetDamage(Damage);
        }
        
        public void GetDamage(int damage)
        {
            Hp -= damage - Protection;
        }


    }
}

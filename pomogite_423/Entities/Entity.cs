using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pomogite_423.Entities
{
    internal abstract class Entity
    {
        public int Hp;
        public int Damage;

        public abstract void GetDamage(int damage);

        public Entity(int hp = 30, int damage = 3)
        {
            Hp = hp;
            Damage = damage;
        }
    }
}

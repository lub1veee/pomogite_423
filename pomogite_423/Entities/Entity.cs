using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pomogite_423.Entities
{
    internal abstract class Entity
    {
        protected Random _random;

        public int Hp;
        public int Damage;

        public abstract void GetDamage(int damage);

        public Entity(Random random, int hp = 30, int damage = 3)
        {
            Hp = hp;
            Damage = damage;
            _random = random;
        }
    }
}

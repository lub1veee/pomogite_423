using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pomogite_423
{
    internal abstract class Entity
    {
        protected Random _random;

        public int MaxHp;
        public int Hp;
        public int Damage;

        public abstract int GetDamage(int damage);

        public Entity(Random random, int hp = 30, int damage = 3)
        {
            MaxHp = hp;
            Hp = MaxHp;
            Damage = damage;
            _random = random;
        }
    }
}

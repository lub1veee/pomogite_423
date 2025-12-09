using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pomogite_423
{
    internal abstract class Entity
    {
        public int MaxHp;
        public int Hp;
        public int Damage;

        public abstract int GetDamage(int damage);

        public Entity(int hp = 30, int damage = 3)
        {
            MaxHp = hp;
            Hp = MaxHp;
            Damage = damage;
        }
    }
}

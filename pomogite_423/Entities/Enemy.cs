using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pomogite_423
{
    internal abstract class Enemy : Entity
    {
        public string Name;
        public int Protection;

        public Enemy(Random random, int hp = 69, int damage = 13, int protection = 5, string name = "") : base(random, hp, damage)
        {
            Hp = hp;
            Damage = damage;
            Name = name;
            Protection = protection;
        }

        public abstract void AttackPlayer();
        
        public override int GetDamage(int damage)
        {
            damage = damage - Protection;
            if (damage <= 0) damage = 1;
            Hp -= damage;
            if(Hp <= 0) Hp = 0;
            return damage;
        }

        public void Introduce()
        {
            Console.WriteLine($"ВРАГ\n{Program.Separator}\n{Name}\nЗдоровье: {Hp}\nУрон: {Damage}\n{Program.Separator}");
        }
    }
}

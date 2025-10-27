using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pomogite_423.Entities
{
    internal class Player : Entity
    {
        public Armor ArmorPlayer;
        public Weapon WeaponPlayer;

        public static Player Instance;

        public void Initialize()
        {
            if(Instance == null)
            {
                Instance = this;
            }
        }

        public void GetDamage(int damage)
        {
            Hp -= damage - ArmorPlayer.Protection;
        }
    }
}

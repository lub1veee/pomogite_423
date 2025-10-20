using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace pomogite_423
{
    internal class Game
    {
        private Random random = new();
        public void StartGame()
        {
            while (true)
            {

            }
                                                                                                                                                                                                       
        }

        private void ChestOrFight()
        {
            switch(random.Next(1, 2))
            {
                case 1:
                    OpenChest();
                    break;
                case 2:
                    StartFight();
                    break;
                default:
                    break;

            }
        }
        private void OpenChest()
        {
            switch (random.Next(1, 3))
            {
                case 1:
                    ChooseWeapon();
                    break;
                case 2:
                    ChooseArmor();
                    break;
                case 3:
                    GetHP();
                    break;
                default:
                    break;
            }
        }
        private void ChooseWeapon()
        {

        }

        private void ChooseArmor()
        {

        }

        private void GetHP()
        {

        }



        private void StartFight()
        {

        }
    }
}

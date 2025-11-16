using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Game
    {
        class AvtoService
        {
            private Random random = new Random();

            private int _currentTurn;

            private const int FINE = 50000;
            private const int START_BALANCE = 100000;

            private decimal balance;
            private decimal Balance
            {
                get => balance;
                set
                {
                    balance = value;
                    if (balance < 0) LoseGame();
                }
            }
        public void StartGame()
        {

        }

        private void ChooseMenu(Part part)
        { 

        }

        private void LoseGame()
        { 

        }

        private void PayFine()



        private void ShowBalance()


        public static void WaitForUser()



        private void ShowOrderMenu()


        public void ManageOrders()


        private void DeliveOrder(Order order)


        private void ClaimOrder(Part part)


        private void CancelOrder()


        public void CompensateDamage(Part part)


        private void RepairDetail(Part part)


        private void ShowAllDetailsQuantity()


        private void ShowDetailsQuantity(Part part)


        private decimal CalculateReplacing(Part part)



    }
        

    
}

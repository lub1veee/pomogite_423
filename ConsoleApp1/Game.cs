using ConsoleApp1.tables;
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
        }
        private List<Detail> _details = Core.Context.Details.ToList();

        private List<Order> _orders = new List<Order>();
        public void StartGame()
        {
            Balance = START_BALANCE;
            WaitForUser();
            _currentTurn = 0;
            while (true)
            {
                _currentTurn++;
                ManageOrders();
                Console.WriteLine($"День {_currentTurn}\nУ вас новый клиент!");
                Detail detail = GetRandomPart();
                ChooseMenu(part);
            }

            private void ChooseMenu(Detail detail)
            {
                Console.Clear();
                Console.WriteLine($"Деталь: {detail.Name}. Стоимость ремонта: {detail.Price + detail.RepairFee}.");

                Console.WriteLine("0. Заказать деталь\n1. Все детали\n2. Принять заказ\n3. Отказаться (Штраф)");

                bool pick = false;
                while (!pick)
                {
                    pick = true;
                    ConsoleKey key = Console.ReadKey().Key;
                    Console.Clear();
                    switch (key)
                    {
                        case (ConsoleKey.D0):
                            ShowOrderMenu();
                            ChooseMenu(detail);
                            break;
                        case (ConsoleKey.D1):
                            ShowAllDetailsQuantity();
                            ChooseMenu(detail);
                            break;
                        case (ConsoleKey.D2):
                            ClaimOrder(detail);
                            break;
                        case (ConsoleKey.D3):
                            CancelOrder();
                            break;
                        default:
                            pick = false;
                            break;
                    }
                }
            }
        }
        #region Features

        private void LoseGame()
        {
            ShowBalance();
            Console.WriteLine("Игра окончена, вы в долговой яме.\nСкинуть деньги с вашей карты мне - 1");
            Console.ReadKey();

            throw new Exception("GG");

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

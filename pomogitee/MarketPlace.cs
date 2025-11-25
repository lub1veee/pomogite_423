using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pomogitee
{
    internal class MarketPlace
    {

        private Users CurrentUser = null;
        public void StartMenu()
        {
            if (CurrentUser == null)
            {
                Menu.ShowPick("Регистрация", "Вход", "Просмотр товаров");
                while (true)
                {
                    switch (Console.ReadKey().Key)
                    {
                        case ConsoleKey.D1:
                            SignUp();
                            break;
                        case ConsoleKey.D2:


                            SignIn();
                            break;
                        case ConsoleKey.D3:
                            ShowGoods();
                            break;
                        default:
                            break;
                    }
                }
            }
            else
            {
                Menu.ShowPick("Просмотр товаров", "Корзина", "Выбрать пункт выдачи", "История заказов");
                while (true)
                {
                    switch (Console.ReadKey().Key)
                    {
                        case ConsoleKey.D1:
                            ShowGoods();
                            break;
                        case ConsoleKey.D2:
                            ShowCart();
                            break;
                        case ConsoleKey.D3:
                            ChooseOffice();
                            break;
                        case ConsoleKey.D4:
                            ShowOrders();
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        private void SignUp()
        {

        }

        pr
    }
}

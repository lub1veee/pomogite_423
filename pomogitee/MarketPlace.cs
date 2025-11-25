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
            Menu.Header("Регистрация");

            Users user = new Users();


            bool successSignIn = false;
            while (!successSignIn)
            {
                user.Login = Menu.WriteRead("Введите логин: ");
                if (user.Login != null &&
                    Core.Context.Users.ToList().FirstOrDefault(u => u.Login == user.Login) == null)
                    successSignIn = true;
            }

            user.Name = Menu.WriteRead("Введите имя пользователя: ");
            user.PhoneNumber = Menu.WriteRead("Введите номер телефона: ");
            string password;
            string acceptPassword;

            do
            {
                password = Menu.WriteRead("Введите пароль: ");
                acceptPassword = Menu.WriteRead("Введите пароль повторно: ");

                if (password != acceptPassword)
                {
                    Console.WriteLine("Пароли не совпадают");

                }
            } while (password != acceptPassword);

            user.Password = password;

            Core.Context.Users.Add(user);

            Core.Context.SaveChanges();

            Console.WriteLine("Вы зарегистрированы");

            CurrentUser = user;
            Console.ReadKey();
            ShowGoods();
        }

        private void SignIn()
        {
        }

        private void ShowGoods()
        {

        }

        private void ShowCart() 
        {

        }

        private void ShowOrders()
        {

        }

        private void ChooseOffice()
        {

        }
    }
}

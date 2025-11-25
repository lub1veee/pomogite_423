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
            while (true)
            {
                Menu.Header("ВХОД");

                string login = Menu.WriteRead("Логин: ");
                string password = Menu.WriteRead("Пароль: ");

                if (password == "" || login == "") StartMenu();

                Users user = Core.Context.Users.ToList().FirstOrDefault(u => u.Login == login);

                if (user == null) continue;
                if (user.Password != password)
                    Console.WriteLine("Неверный логин или пароль");
                else
                {
                    CurrentUser = user;
                    Console.WriteLine("Вход успешный!");
                    StartMenu();
                }
            }
        }

        private void ShowGoods()
        {
            while (true)
            {
                Menu.Header("ПРОСМОТР ТОВАРОВ");

                List<Goods> goods = Core.Context.Goods.ToList();

                foreach (Goods g in goods)
                {
                    Console.WriteLine($"{g.Id}. {g.Name}: {g.Price} руб.");
                }

                Menu.Separator();
                Console.WriteLine("0 - Вернуться в меню");
                if (CurrentUser != null)
                {
                    Console.WriteLine("9 - Просмотреть корзину");
                }
                Menu.Separator();

                string input = Menu.WriteRead("Введите ID товара или команду: ");

                if (input == "0")
                {
                    StartMenu();
                    return;
                }
                else if (CurrentUser != null && input == "9")
                {
                    ShowCart();
                    return;
                }

                int id;
                try
                {
                    id = int.Parse(input);
                }
                catch
                {
                    continue;
                }

                Goods p = goods.FirstOrDefault(g => g.Id == id);
                if (p == null)
                {
                    Console.WriteLine("Неверный ID товара");
                    continue;
                }

                ProductMenu(p);
                break;
            }
        }
        private void ProductMenu(Goods product)
        {
            Menu.Header($"{product.Name}");
            Console.WriteLine($"Стоимость: {product.Price} руб.\nОписание: {product.Discription}");
            Menu.Separator();

            if (CurrentUser != null)
            {
                Menu.ShowPick("Купить", "Добавить в корзину", "Вернуться к товарам", "Просмотреть корзину");
            }
            else
            {
                Menu.ShowPick("Купить", "Добавить в корзину", "Вернуться к товарам");
            }

            while (true)
            {
                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.D1:
                        BuyGoods(product);
                        break;
                    case ConsoleKey.D2:
                        AddToCart(product);
                        break;
                    case ConsoleKey.D3:
                        ShowGoods();
                        return;
                    case ConsoleKey.D4:
                        if (CurrentUser != null)
                        {
                            ShowCart();
                            return;
                        }
                        break;
                    case ConsoleKey.D0:
                        ShowGoods();
                        return;
                    case ConsoleKey.Enter:
                        ShowGoods();
                        return;
                    default:
                        break;
                }
            }
        }

        private void BuyGoods(Goods product)
        {

        }

        private void AddToCart(Goods product)
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

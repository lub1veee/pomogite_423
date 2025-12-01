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

       // private void BuyGoods(Goods product)
        {
            if (!CheckSignIn())
            {
                Console.WriteLine("Для покупки необходимо войти в аккаунт!");
                StartMenu();
            }
            else
            {
                if (CurrentUser.Office == null)
                {
                    Console.WriteLine("Сначала выберите пункт выдачи!");
                    ChooseOffice();
                    return;
                }

                Orders order = new Orders();
                order.IdUsers = CurrentUser.Id;
                order.IdOffice = CurrentUser.Office.Id;
                order.Date = DateTime.Now;

                Core.Context.Orders.Add(order);
                Core.Context.SaveChanges();

                OrderGoods og = new OrderGoods();
                og.IdGoods = product.Id;
                og.IdOrders = order.Id;

                try
                {
                    og.Quantity = int.Parse(Menu.WriteRead("Введите количество товаров:"));
                }
                catch
                {
                    Console.WriteLine("Неверное количество!");
                    return;
                }

                Console.WriteLine($"Вы покупаете {og.Quantity} {product.Name}\nСтоимость покупки: {product.Price * og.Quantity}");

                Menu.WriteRead("Нажмите Enter для подтверждения покупки...");

                Core.Context.OrderGoods.Add(og);
                Core.Context.SaveChanges();

                Console.WriteLine("Покупка совершена!");
                Menu.WriteRead("Нажмите любую клавишу для продолжения...");
                ShowGoods();
            }
        }
        private void AddToCart(Goods product)
        {
            if (!CheckSignIn())
            {
                Console.WriteLine("Для добавления в корзину необходимо войти в аккаунт!");
                StartMenu();
                return;
            }

            if (IfInCart(product) == 0)
            {
                Cart cg = new Cart();

                cg.IdUsers = CurrentUser.Id;
                cg.IdGoods = product.Id;
                try
                {
                    cg.Quantity = int.Parse(Menu.WriteRead("Введите количество: "));
                }
                catch
                {
                    Console.WriteLine("Неверное количество!");
                    return;
                }

                Core.Context.Cart.Add(cg);
                Core.Context.SaveChanges();
                Console.WriteLine("Товар добавлен в корзину!");
            }
            else
            {
                Console.WriteLine("В корзине уже есть эти товары!");
            }

            Menu.WriteRead("Нажмите любую клавишу для продолжения...");
            ProductMenu(product);
        }
        private int IfInCart(Goods product)
        {
            Cart god = Core.Context.Cart.FirstOrDefault(p => p.IdGoods == product.Id && p.IdUsers == CurrentUser.Id);

            if (god == null) return 0;
            else return god.Quantity;
        }

        private bool CheckSignIn()
        {
            return CurrentUser != null;
        }
        private void ShowCart() 
        {

        }

        private void ShowOrders()
        {

        }

        private Office ChooseOffice()
        {
            Menu.Header("ВЫБОР ПУНКТА ВЫДАЧИ");
            if (CurrentUser.Office != null)
            {
                Console.WriteLine("У вас уже есть выбранный пункт выдачи. (1 - выбрать другой)");
                if (Console.ReadKey().Key != ConsoleKey.D1) return CurrentUser.Office;
            }
            ShowAllOffices();

            int choose;
            try
            {
                choose = int.Parse(Menu.WriteRead("Выберите номер офиса:"));
            }
            catch
            {
                Console.WriteLine("Неверный ввод!");
                return ChooseOffice();
            }

            Office selectedOffice = Core.Context.Office.ToList().FirstOrDefault(o => o.Id == choose);

            if (selectedOffice != null)
            {
                CurrentUser.Office = selectedOffice;
                Core.Context.SaveChanges();
                Console.WriteLine("Пункт выдачи выбран!");
            }
            else
            {
                Console.WriteLine("Неверный номер офиса!");
                return ChooseOffice();
            }

            return selectedOffice;
        }

        private void ShowAllOffices()
        {
            foreach (Office office in Core.Context.Office.ToList())
            {
                Console.WriteLine($"{office.Id}. {office.Adress}");
            }
        }
    }
}

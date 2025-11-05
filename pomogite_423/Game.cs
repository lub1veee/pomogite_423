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
        private int _currentTurn = 1;
        public static Game Instance;
#pragma warning disable
        private string _turnType;
#pragma warning disable
        private CombatManager _combatManager;

        public void StartGame()
        {
            _combatManager = new CombatManager();
            InitializeGame();
            Player player = new(
                random, 
                new Armor("Рубашка", 100, 10), 
                new Weapon("Кулаки", 100, 10)
            );
            while (true)
            {
                ChestOrFight();
                _currentTurn++;
            }
        }

        public void ShowTurnInfo()
        {
            Console.WriteLine($"ХОД {_currentTurn} - {_turnType}");
            Console.WriteLine(Program.Separator);
        }

        private void InitializeGame()
        {
            if(Instance == null)
            {
                Instance = this;
            }

            Armor.AllArmor.Add(new Armor("Деревянная", 1000, 12));
            Armor.AllArmor.Add(new Armor("Кольчуга", 200, 15));
            Armor.AllArmor.Add(new Armor("Стальные латы", 500, 20));
            Armor.AllArmor.Add(new Armor("Кристаллическая", 1000, 22));
            Armor.AllArmor.Add(new Armor("Пространственная", 1000, 25));

            Weapon.AllWeapon.Add(new Weapon("Кухонный нож", 1000, 12));
            Weapon.AllWeapon.Add(new Weapon("Дубинка", 100, 15));
            Weapon.AllWeapon.Add(new Weapon("Керамбит", 200, 20));
            Weapon.AllWeapon.Add(new Weapon("Нунчаки", 500, 22));
            Weapon.AllWeapon.Add(new Weapon("Коса смерти", 1000000, 25));
        }

        private void ChestOrFight()
        {
            if (_currentTurn % 10 == 0)
            {
                _turnType = "БОЙ";
                BossFight();
                return;
            }
            else
            {
                switch (random.Next(2))
                {
                    case 0:
                        _turnType = "СУНДУК";
                        OpenChest();
                        break;
                    case 1:
                        _turnType = "БОЙ";
                        StartFight();
                        break;
                    default:
                        break;
                }
            }
        }

        private void OpenChest()
        {
            ShowTurnInfo();
            Console.Write("Вы нашли сундук!\nСодержимое: ");
            switch (random.Next(3))
            {
                case 0:
                    Console.Write($"Оружие\n");
                    ChooseWeapon();
                    break;
                case 1:
                    Console.Write($"Броня\n");
                    ChooseArmor();
                    break;
                case 2:
                    Console.Write($"Зелье исцеления\n");
                    GetHP();
                    break;
                default:
                    break;
            }
            Program.WaitForPlayer();
        }

        private void StartFight()
        {
            switch (random.Next(3))
            {
                case 0:
                    _combatManager.StartCombat(new Mage(random));
                    break;
                case 1:
                    _combatManager.StartCombat(new Goblin(random));
                    break;
                case 2:
                    _combatManager.StartCombat(new Skeleton(random));
                    break;
                default:
                    break;
            }
        }

        private void ChooseWeapon()
        {
            Weapon weapon = Weapon.AllWeapon[random.Next(5)];
            weapon.Info();
            Console.WriteLine("Взять оружие - Y\nПойти дальше - N");
            while (true)
            {
                ConsoleKey key = Console.ReadKey().Key;
                if(key == ConsoleKey.Y)
                {
                    Player.Instance.WeaponPlayer = weapon;
                    Console.WriteLine("Оружие экипировано!");
                    return;
                }
                else if(key == ConsoleKey.N)
                {
                    return;
                }
            }
        }

        private void ChooseArmor()
        {
            Armor armor = Armor.AllArmor[random.Next(5)];
            armor.Info();
            Console.WriteLine("Взять броню - Y\nПойти дальше - N");
            while (true)
            {
                ConsoleKey key = Console.ReadKey().Key;
                if (key == ConsoleKey.Y)
                {
                    Player.Instance.ArmorPlayer = armor;
                    return;
                }
                else if (key == ConsoleKey.N)
                {
                    return;
                }
            }
        }

        private void GetHP()
        {
            Player.Instance.Heal();
            Player.Instance.ShowStats();
        }



        private void BossFight()
        {
            switch (random.Next(4))
            {
                case 0:
                    _combatManager.StartCombat(new ArchimageCPP(random));
                    break;
                case 1:
                    _combatManager.StartCombat(new Kovalsky(random));
                    break;
                case 2:
                    _combatManager.StartCombat(new PestovCMM(random));
                    break;
                case 3:
                    _combatManager.StartCombat(new VVG(random));
                    break;
                default:
                    break;
                    
            }
        }
    }
}

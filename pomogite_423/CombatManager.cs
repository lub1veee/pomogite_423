using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pomogite_423
{
    internal class CombatManager
    {
        public void StartCombat(Enemy enemy)
        {
            Game.Instance.ShowTurnInfo();

            // Представляем участников файта
            enemy.Introduce();
            Player.Instance.ShowStats();
            Program.WaitForPlayer();

            while (Player.Instance.Hp > 0 && enemy.Hp > 0)
            {
                Game.Instance.ShowTurnInfo();

                if (Player.Instance.IsFreezed) 
                {
                    Player.Instance.IsFreezed = false; 
                    Console.WriteLine("ПРОПУСК ХОДА");
                }
                else Player.Instance.AttackEnemy(enemy);
                if (Player.Instance.Hp <= 0 || enemy.Hp <= 0) break;
                Program.WaitForPlayer();

                Game.Instance.ShowTurnInfo();

                enemy.AttackPlayer();
                if (Player.Instance.Hp <= 0 || enemy.Hp <= 0) break;
                Program.WaitForPlayer();
            }

            if (enemy.Hp <= 0)
            {
                Console.Clear();
                ShowCenteredVictoryScreen(enemy);

                Program.WaitForPlayer();
            }
            else if (Player.Instance.Hp <= 0)
            {
                Console.Clear();

                ShowCenteredDeathScreen();
                Console.ReadKey();
            }

            void ShowCenteredDeathScreen()
            {
                string[] deathArt = {
        "╔═══════════════════════════════════════════════╗",
        "║                                               ║",
        "║              ▄████  ▄▄▄       ███▄ ▄███▓      ║",
        "║             ██▒ ▀█▒▒████▄    ▓██▒▀█▀ ██▒      ║",
        "║            ▒██░▄▄▄░▒██  ▀█▄  ▓██    ▓██░      ║",
        "║            ░▓█  ██▓░██▄▄▄▄██ ▒██    ▒██       ║",
        "║            ░▒▓███▀▒ ▓█   ▓██▒▒██▒   ░██▒      ║",
        "║             ░▒   ▒  ▒▒   ▓▒█░░ ▒░   ░  ░      ║",
        "║              ░   ░   ▒   ▒▒ ░░  ░      ░      ║",
        "║            ░ ░   ░   ░   ▒   ░      ░         ║",
        "║                  ░       ░  ░       ░         ║",
        "║                                               ║",
        "║             ВАШЕ ПУТЕШЕСТВИЕ ЗАВЕРШЕНО        ║",
        "║                                               ║",
        "╚═══════════════════════════════════════════════╝"
                };

                CenterAndPrint(deathArt);
            }

            void ShowCenteredVictoryScreen(Enemy enemy)
            {
                string[] victoryArt = {
                "          ✦ ✦ ✦ ✦ ✦       ",
                "          ✦ ПОБЕДА! ✦       ",
                "          ✦ ✦ ✦ ✦ ✦       ",
                @"             \ | /           ",
                @"           -- ○ ○ --         ",
                @"             / | \          ",
                "                             ",
                $"        {enemy.Name} повержен!    ",
                "                             ",
                "           ╰(*°▽°*)╯         "
                };

                CenterAndPrint(victoryArt);
            }

            void CenterAndPrint(string[] lines)
            {
                int consoleWidth = Console.WindowWidth;

                foreach (string line in lines)
                {
                    int spaces = (consoleWidth - line.Length) / 2;
                    Console.WriteLine(new string(' ', Math.Max(0, spaces)) + line);
                }
            }
        }
    }
}

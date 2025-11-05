using System.Threading.Channels;

namespace pomogite_423
{
    internal class Program
    {
        public static string Separator = new string('=', 40);

        static void Main(string[] args)
        {
            Game game = new Game();
            game.StartGame();
        }

        public static void WaitForPlayer()
        {
            Console.WriteLine("Намите любую клавишу для продолжения...");
            Console.ReadKey();
            Console.Clear();
        }
    }
}
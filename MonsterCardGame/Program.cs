using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonsterCardGame
{
    class Program
    {
        static void Main(string[] args)
        {
            //User Player1 = new User();
            //User Player2 = new User();
            //Logic GameLogic = new Logic();
            //CardDB Database = new CardDB(Player1);
            //GameLogic.Battle(Player1, Player2);
            Menu MainMenu = new Menu();
            MainMenu.Start();
            Console.WriteLine("Press any key to close application");
            Console.ReadKey();
        }
    }
}

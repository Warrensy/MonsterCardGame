using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonsterCardGame.Classes;

namespace MonsterCardGame
{
    class Program
    {
        static void Main(string[] args)
        {
            User Player1 = new User();
            User Player2 = new User();
            Logic GameLogic = new Logic();
            CardDB Database = new CardDB(Player1);
            Database = new CardDB(Player2);
            GameLogic.Battle(Player1, Player2);
            //Console.Write(Player1.PlayerCardCollection.CardsInStack[0]._CardName);
            //Console.Write(" V.S ");
            //Console.Write(Player2.PlayerCardCollection.CardsInStack[5]._CardName);
            Console.ReadKey();
        }
    }
}

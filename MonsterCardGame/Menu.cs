using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonsterCardGame
{
    class Menu
    {
        bool logedIn = false;
        int selected = 0;
        bool Quit = false;
        int ExitPosition = 2;
        int Menubeginning = 0;
        const int CorrectDeckSize = 4;
        ConnectionForm con = new ConnectionForm();
        User Player1;
        User Player2;
        Logic GameLogic = new Logic();
        Shop shop;
        public void navigation()
        {
            switch (Console.ReadKey(true).Key)
            {
                case ConsoleKey.UpArrow:
                    if (selected > Menubeginning) { selected--; }
                    break;
                case ConsoleKey.DownArrow:
                    if (selected < ExitPosition) { selected++; }
                    break;
                case ConsoleKey.Enter:
                    redirect();
                    break;
                default:
                    break;
            }
        }
        public void redirect()
        {
            switch (selected)
            {
                case 0:
                    logedIn = con.LoginForm();
                    break;
                case 1:
                    con.RegistrationForm();
                    break;
                case 2:
                    if (ExitPosition == 2) { Quit = true; }
                    break;
                case 3:
                    if(Player1.PlayerDeck.CardDeck.Count == CorrectDeckSize && Player2.PlayerDeck.CardDeck.Count == CorrectDeckSize)
                    { GameLogic.Battle(Player1.PlayerDeck, Player2.PlayerDeck); }
                    else
                    { Console.WriteLine($"\nDeck size must be {CorrectDeckSize}"); }
                    System.Threading.Thread.Sleep(1500);
                    break;
                case 4:
                    Console.WriteLine("Coming soon");
                    Console.ReadKey();
                    break;
                case 5:
                    Player1.ManageDeck();
                    break;
                case 6:
                    shop.PrintShop();
                    break;
                case 7:
                    logedIn = false;
                    Menubeginning = 0;
                    ExitPosition = 2;
                    selected = 0;
                    break;
                case 8:
                    Quit = true;
                    break;
                default:
                    break;
            }
        }
        public void Start()
        {
            Menubeginning = 0;
            while(!Quit && !logedIn)
            {
                Console.Clear();
                Console.Write(" Login");
                if (selected == 0){ Console.WriteLine(" <-"); }
                else Console.WriteLine("");
                Console.Write(" Register");
                if (selected == 1){ Console.WriteLine(" <-"); }
                else Console.WriteLine("");
                Console.Write(" Exit Game");
                if (selected == ExitPosition){ Console.WriteLine(" <-"); }
                else Console.WriteLine("");
                navigation();
            }
            if(logedIn)
            {
                selected = 3;
                ExitPosition = 8;
                Player1 = new User();
                Player2 = new User();
                Player1.LoadStack();
                shop = new Shop(Player1);
                GameMenu();
            }
        }
        public void GameMenu()
        {
            Menubeginning = 3;

            while (logedIn && !Quit)
            {
                Console.Clear();
                Console.Write(" Player VS AI");
                if (selected == 3){ Console.WriteLine(" <-"); }
                else Console.WriteLine("");
                Console.Write(" Player VS Player");
                if (selected == 4){ Console.WriteLine(" <-"); }
                else Console.WriteLine("");
                Console.Write(" Card Collection");
                if (selected == 5){ Console.WriteLine(" <-"); }
                else Console.WriteLine("");
                Console.Write(" Shop");
                if (selected == 6){ Console.WriteLine(" <-"); }
                else Console.WriteLine("");
                Console.Write(" Sign out");
                if (selected == 7) { Console.WriteLine(" <-"); }
                else Console.WriteLine("");
                Console.Write(" Exit Game");
                if (selected == ExitPosition){ Console.WriteLine(" <-"); }
                else Console.WriteLine("");
                navigation();
            }
            if(!logedIn && !Quit)
            {
                Start();
            }
        }
    }
}

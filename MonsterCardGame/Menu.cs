using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;

namespace MonsterCardGame
{
    public class Menu
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
        Trading tradeing;

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
                    if (Player1.PlayerDeck.CardDeck.Count == CorrectDeckSize && Player2.PlayerDeck.CardDeck.Count == CorrectDeckSize)
                    {
                        int gamewon = GameLogic.Battle(Player1.PlayerDeck, Player2.PlayerDeck);
                        Player1.UpdatePlayerStatistic(gamewon);
                    }
                    else
                    { Console.WriteLine($"\nDeck size must be {CorrectDeckSize}"); }
                    System.Threading.Thread.Sleep(1500);
                    break;
                case 4:
                    Console.WriteLine("Coming soon");
                    Console.ReadKey();
                    break;
                case 5:
                    Player1.LoadProfile();
                    break;
                case 6:
                    Player1.ManageDeck();
                    break;
                case 7:
                    LoadScoreBoard();
                    break;
                case 8:
                    shop.PrintShop();
                    break;
                case 9:
                    tradeing.OpenTradeCenter();
                    break;
                case 10:
                    logedIn = false;
                    User.UserID = -1;
                    Menubeginning = 0;
                    ExitPosition = 2;
                    selected = 0;
                    tradeing = null;
                    shop = null;
                    Player1 = null;
                    break;
                case 11:
                    Quit = true;
                    break;
                default:
                    break;
            }
        }
        public void Start()
        {
            Menubeginning = 0;
            while (!Quit && !logedIn)
            {
                Console.Clear();
                Console.Write(" Login");
                if (selected == 0) { Console.WriteLine(" <="); }
                else Console.WriteLine("");
                Console.Write(" Register");
                if (selected == 1) { Console.WriteLine(" <="); }
                else Console.WriteLine("");
                Console.Write(" Exit Game");
                if (selected == ExitPosition) { Console.WriteLine(" <="); }
                else Console.WriteLine("");
                navigation();
            }
            if (logedIn)
            {
                selected = 3;
                ExitPosition = 11;
                Player1 = new User();
                Player2 = new User();
                CardDB db = new CardDB(Player2);
                Player1.LoadStack();
                shop = new Shop(ref Player1);
                tradeing = new Trading(ref Player1);
                GameMenu();
            }
        }
        private void LoadScoreBoard()
        {
            NpgsqlConnection connection = Connector.EstablishCon();
            NpgsqlCommand command;
            NpgsqlDataReader dataReader;
            int i = 1, spaceWon, spaceElo, spaceUsername, spaceRating;
            string tmpWon, tmpElo, tmpUser, tmpRating;
            string sql = "SELECT elo, gameswon, username FROM users ORDER BY elo DESC;";
            command = new NpgsqlCommand(sql, connection);
            connection.Open();
            dataReader = command.ExecuteReader();
            Console.WriteLine("        |  Games Won  |  ELO  |  Username  |        ");
            Console.WriteLine("--------+-------------+-------+------------+--------");
            while (dataReader.Read())
            {
                tmpWon = Convert.ToString((int)dataReader["gameswon"]);
                tmpElo = Convert.ToString((int)dataReader["elo"]);
                tmpUser = (string)dataReader["username"];
                tmpRating = Convert.ToString(i);
                spaceRating = 5 - tmpRating.Length;
                spaceWon = 8 - tmpWon.Length;
                spaceElo = 5 - tmpElo.Length;
                spaceUsername = 9 - tmpUser.Length;
                Console.Write($"  {i} ");
                for (int j = 0; j < spaceRating; j++)
                {
                    Console.Write(" ");
                }
                Console.Write($"|     {dataReader["gameswon"]}");
                for (int j = 0; j < spaceWon; j++)
                {
                    Console.Write(" ");
                }
                Console.Write($"|  {dataReader["elo"]}");
                for (int j = 0; j < spaceElo; j++)
                {
                    Console.Write(" ");
                }
                Console.Write($"|   {dataReader["username"]}");
                for (int j = 0; j < spaceUsername; j++)
                {
                    Console.Write(" ");
                }
                Console.Write("|\n");
                i++;
            }
            connection.Close();
            Console.ReadKey();
        }
        public void GameMenu()
        {
            Menubeginning = 3;

            while (logedIn && !Quit)
            {
                Console.Clear();
                Console.Write(" Player VS AI");
                if (selected == 3) { Console.WriteLine(" <="); }
                else Console.WriteLine("");
                Console.Write(" Player VS Player");
                if (selected == 4) { Console.WriteLine(" <="); }
                else Console.WriteLine("");
                Console.Write(" Profile");
                if (selected == 5) { Console.WriteLine(" <="); }
                else Console.WriteLine("");
                Console.Write(" Card Collection");
                if (selected == 6) { Console.WriteLine(" <="); }
                else Console.WriteLine("");
                Console.Write(" Global Scoreboard");
                if (selected == 7) { Console.WriteLine(" <="); }
                else Console.WriteLine("");
                Console.Write(" Shop");
                if (selected == 8) { Console.WriteLine(" <="); }
                else Console.WriteLine("");
                Console.Write(" Trade Cards");
                if (selected == 9) { Console.WriteLine(" <="); }
                else Console.WriteLine("");
                Console.Write(" Sign out");
                if (selected == 10) { Console.WriteLine(" <="); }
                else Console.WriteLine("");
                Console.Write(" Exit Game");
                if (selected == ExitPosition) { Console.WriteLine(" <="); }
                else Console.WriteLine("");
                navigation();
            }
            if (!logedIn && !Quit)
            {
                Start();
            }
        }
    }
}

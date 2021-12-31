using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;

namespace MonsterCardGame
{
    class Shop
    {
        const int CardPackPrice = 5;
        int ShopSelected = 0;
        int ExitPosition = 2;
        int Menubeginning = 0;
        bool QuitShop = false;
        _Player.Coins = Coins;
        User _Player;
        private ConnectionForm con = new ConnectionForm();
        private NpgsqlConnection connection;
        private NpgsqlCommand command;
        private NpgsqlDataReader dataReader;
        private string sql = "";
        public Shop(ref User Player)
        {
            _Player = Player;    
        }
        public void PrintShop()
        {
            QuitShop = false;
            while (!QuitShop)
            {
                Console.Clear();
                Console.WriteLine("                      __________");
                Console.WriteLine($"  Coins {_Player.Coins}          _-'    ___   '-_");
                Console.WriteLine("                _-'     _______    '-_");
                Console.WriteLine("             _-'      ___________     '-_");
                Console.WriteLine("          _-'        _____________       '-_");
                Console.WriteLine("       _-'           | CARD SHOP |          '-_");
                Console.WriteLine("    _-'                                        '-_");
                Console.WriteLine(" _-'______________________________________________'-_");
                Console.Write("  | ___   Buy Card Pack"); if (ShopSelected == 0) { Console.Write(" <=      - 5 Coins      "); } else Console.Write("                        "); Console.Write("___ | ");
                Console.WriteLine("\n  | ___                                        ___ | ");
                Console.Write("  | ___   Sell Cards"); if (ShopSelected == 1) { Console.Write(" <=      + 1 Coins         "); } else Console.Write("                           "); Console.Write("___ |");
                Console.WriteLine("\n  | ___                                        ___ | ");
                Console.Write("  | ___   Exit Shop"); if (ShopSelected == ExitPosition) { Console.Write(" <=                         "); } else Console.Write("                            "); Console.Write("___ | ");
                Console.WriteLine("\n  | ___                                        ___ |");

                ShopNavigation();
            }
        }
        public void ShopNavigation()
        {
            switch (Console.ReadKey(true).Key)
            {
                case ConsoleKey.UpArrow:
                    if (ShopSelected > Menubeginning) { ShopSelected--; }
                    break;
                case ConsoleKey.DownArrow:
                    if (ShopSelected < ExitPosition) { ShopSelected++; }
                    break;
                case ConsoleKey.Enter:
                    ShopRedirect();
                    break;
                default:
                    break;
            }
        }
        public void ShopRedirect()
        {
            switch (ShopSelected)
            {
                case 0:
                    Console.WriteLine("         Buy Package for 5 Coins?");
                    switch (Console.ReadKey(true).Key)
                    {
                        case ConsoleKey.Enter:
                            BuyPackage();
                            break;
                        default:
                            break;
                    }
                    break;
                case 1:
                    SellCard();
                    break;
                default:
                    QuitShop = true;
                    ShopSelected = 0;
                    break;
            }
        }
        public void BuyPackage()
        {
            connection = Connector.EstablishCon();
            connection.Open();
            sql = $"SELECT coins FROM users WHERE userid='{User.UserID}';";
            command = new NpgsqlCommand(sql, connection);
            dataReader = command.ExecuteReader();
            dataReader.Read();
            int Coins = (int)dataReader["coins"];
            if (Coins >= CardPackPrice)
            {
                DBUpdateCoins(Coins, -CardPackPrice);
                Random rnd = new Random();
                int num;
                for (int i = 0; i < 5; i++)
                {
                    connection.Close();
                    num = rnd.Next(21) + 1;
                    connection.Open();
                    sql = $"SELECT * FROM cards WHERE cardid='{num}';";
                    command = new NpgsqlCommand(sql, connection);
                    dataReader = command.ExecuteReader();
                    dataReader.Read();
                    MonsterCard newCard = new MonsterCard(
                        (int)dataReader["dmg"],
                        (string)dataReader["name"],
                        (Card.MonsterType)dataReader["race"],
                        (Card.ElementType)dataReader["element"],
                        (Card.MonsterType)dataReader["race_w"],
                        (Card.ElementType)dataReader["element_w"],
                        (int)dataReader["cardid"]
                        );
                    _Player.PlayerCardCollection.AddMonsterCardToStack(newCard);
                    connection.Close();
                    DBAddCardToStack(newCard._CardID);
                    Console.WriteLine($"{newCard._CardName} has been added");
                }
            }
            else
            {
                Console.WriteLine("\nNot enought coins!");
                Console.ReadKey();
            }
            connection.Close();
        }
        private void DBAddCardToStack(int cardid)
        {
            connection = Connector.EstablishCon();
            connection.Open();
            sql = $"INSERT INTO stack (cardid,userid,status) VALUES ({cardid},{User.UserID},false);";
            command = new NpgsqlCommand(sql, connection);
            dataReader = command.ExecuteReader();
            connection.Close();
        }
        private void DBUpdateCoins(int Coins, int amount)
        {
            Coins += amount; 
            _Player.Coins = Coins;
            connection = Connector.EstablishCon();
            connection.Open();
            sql = $"UPDATE users SET coins={_Player.Coins} WHERE userid={User.UserID};";
            command = new NpgsqlCommand(sql, connection);
            dataReader = command.ExecuteReader();
            connection.Close();
        }
        private void SellCard()
        {
            bool CorrectInput = true;
            string exit = "";
            while (exit != "x")
            {
                Console.Clear();
                _Player.PlayerCardCollection.PrintStack();
                Console.WriteLine("[x + Enter] Exit");
                Console.WriteLine("[Enter] Confirm");
                if (CorrectInput)
                {
                    Console.WriteLine("Select card to sell: ");
                }
                else
                {
                    Console.WriteLine("Invalide Input.\nTry Again: ");
                }
                exit = Console.ReadLine();

                int input, cardid;
                if (CorrectInput = int.TryParse(exit, out input))
                {
                    int StackCount = _Player.PlayerCardCollection.CardsInStack.Count;
                    if (input <= StackCount)
                    {
                        cardid = _Player.PlayerCardCollection.CardsInStack[input - 1]._CardID;
                        _Player.PlayerCardCollection.RemoveCardByIndex(input - 1);
                        DBUpdateCoins(_Player.Coins, 1);
                        _Player.RemoveCard(cardid);
                    }
                }
            }
        }
    }
}

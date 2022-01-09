using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;

namespace MonsterCardGame
{
    public class Shop
    {
        int CardPackPrice = 5;
        int ShopSelected = 0;
        int ExitPosition = 2;
        int Menubeginning = 0;
        bool QuitShop = false;
        User _User;
        private ConnectionForm con = new ConnectionForm();
        private NpgsqlConnection connection;
        private NpgsqlCommand command;
        private NpgsqlDataReader dataReader;
        public Shop(ref User Player)
        {
            _User = Player;
            CheckCoinsinDB();
        }
        public void PrintShop()
        {
            if (_User._Gild == User.Gild.SandNomad)
            { CardPackPrice--; }
            QuitShop = false;
            while (!QuitShop)
            {
                Console.Clear();
                Console.WriteLine("                      __________");
                Console.WriteLine($"  Coins {_User.Coins}          _-'    ___   '-_");
                Console.WriteLine("                _-'     _______    '-_");
                Console.WriteLine("             _-'      ___________     '-_");
                Console.WriteLine("          _-'        _____________       '-_");
                Console.WriteLine("       _-'           | CARD SHOP |          '-_");
                Console.WriteLine("    _-'                                        '-_");
                Console.WriteLine(" _-'______________________________________________'-_");
                Console.Write("  | ___   Buy Card Pack"); if (ShopSelected == 0) { Console.Write($" <=      - {CardPackPrice} Coins      "); } else Console.Write("                        "); Console.Write("___ | ");
                Console.WriteLine("\n  | ___                                        ___ | ");
                Console.Write("  | ___   Sell Cards"); if (ShopSelected == 1) { Console.Write(" <=      + 1 Coins         "); } else Console.Write("                           "); Console.Write("___ |");
                Console.WriteLine("\n  | ___                                        ___ | ");
                Console.Write("  | ___   Exit Shop"); if (ShopSelected == ExitPosition) { Console.Write(" <=                         "); } else Console.Write("                            "); Console.Write("___ | ");
                Console.WriteLine("\n  | ___                                        ___ |");

                ShopNavigation();
            }
            CardPackPrice = 5;
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
                    Console.WriteLine($"         Buy Package for {CardPackPrice} Coins?");
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
        public void CheckCoinsinDB()
        {
            connection = Connector.EstablishCon();
            connection.Open();
            command = new NpgsqlCommand("SELECT coins,gild FROM users WHERE userid=@UserID;", connection);
            command.Parameters.AddWithValue("UserID", User.UserID);
            dataReader = command.ExecuteReader();
            dataReader.Read();
            _User.Coins = (int)dataReader["coins"];
            _User._Gild = (User.Gild)dataReader["gild"];
            connection.Close();
        }
        public void BuyPackage()
        {
            connection = Connector.EstablishCon();
            connection.Open();
            command = new NpgsqlCommand("SELECT coins FROM users WHERE userid=@UserID;", connection);
            command.Parameters.AddWithValue("UserID", User.UserID);
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
                    command = new NpgsqlCommand("SELECT * FROM cards WHERE cardid=@num;", connection);
                    command.Parameters.AddWithValue("num", num);
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
                    connection.Close();
                    _User.PlayerCardCollection.AddMonsterCardToStack(newCard);
                    DBAddCardToStack(newCard._CardID);
                    Console.WriteLine($"{newCard._CardName} has been added");
                }
                Console.ReadKey();
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
            command = new NpgsqlCommand("INSERT INTO stack (cardid,userid,status) VALUES (@cardid,@UserID,false);", connection);
            command.Parameters.AddWithValue("cardid", cardid);
            command.Parameters.AddWithValue("UserID", User.UserID);
            dataReader = command.ExecuteReader();
            connection.Close();
        }
        private void DBUpdateCoins(int Coins, int amount)
        {
            Coins += amount; 
            _User.Coins = Coins;
            connection = Connector.EstablishCon();
            connection.Open();
            command = new NpgsqlCommand("UPDATE users SET coins=@Coins WHERE userid=@UserID;", connection);
            command.Parameters.AddWithValue("Coins", _User.Coins);
            command.Parameters.AddWithValue("UserID", User.UserID);
            dataReader = command.ExecuteReader();
            connection.Close();
        }
        private void SellCard()
        {
            //bool CorrectInput = true;
            //string exit = "";
            bool quit = false, Confirmed = false;
            //bool success = false;
            ShopSelected = 0;
            int StackCount = _User.PlayerCardCollection.CardsInStack.Count, cardid;
            while (!quit)
            {
                while (!Confirmed)
                {
                    StackCount = _User.PlayerCardCollection.CardsInStack.Count;
                    Console.Clear();
                    _User.PlayerCardCollection.PrintStack(ShopSelected);
                    if (ShopSelected == StackCount)
                    {
                        Console.BackgroundColor = ConsoleColor.Blue;
                    }
                    Console.WriteLine("\n Go Back");
                    Console.ResetColor();
                    Console.WriteLine("Select card to sell: ");
                    Console.WriteLine("[Enter] Confirm");
                    switch (Console.ReadKey(true).Key)
                    {
                        case ConsoleKey.UpArrow:
                            if (ShopSelected > 0) { ShopSelected--; }
                            else if (ShopSelected <= 0) { ShopSelected = StackCount; }
                            break;
                        case ConsoleKey.DownArrow:
                            if (ShopSelected < StackCount) { ShopSelected++; }
                            else if (ShopSelected >= StackCount) { ShopSelected = 0; }
                            break;
                        case ConsoleKey.Enter:
                            Confirmed = true;
                            break;
                        default:
                            break;
                    }
                }
                if (ShopSelected < StackCount)
                {
                    cardid = _User.PlayerCardCollection.CardsInStack[ShopSelected]._CardID;
                    _User.PlayerCardCollection.RemoveCardByIndex(ShopSelected);
                    DBUpdateCoins(_User.Coins, 1);
                    _User.RemoveCard(cardid);
                    Confirmed = false;
                }
                else
                {
                    quit = true;
                }
            }
            ShopSelected = 1;
        }
    }
}

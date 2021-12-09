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
        int ExitPosition = 3;
        int Menubeginning = 0;
        bool QuitShop = false;
        User _Player;
        public Shop(User Player)
        {
            _Player = Player;    
        }
        public void PrintShop()
        {
            QuitShop = false;
            while (!QuitShop)
            {
                Console.Clear();
                Console.Write("Buy Card Pack");
                if (ShopSelected == 0) { Console.WriteLine(" <-            - 5 Coins"); }
                else Console.WriteLine("");
                Console.Write("Buy One Randome Card");
                if (ShopSelected == 1) { Console.WriteLine(" <-     - 1 Coin"); }
                else Console.WriteLine("");
                Console.Write("Sell Cards");
                if (ShopSelected == 2) { Console.WriteLine(" <-               + 1 Coin"); }
                else Console.WriteLine("");
                Console.Write("Exit Shop");
                if (ShopSelected == ExitPosition) { Console.WriteLine(" <-"); }
                else Console.WriteLine("");
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
                    Console.WriteLine("Buy Package for 5 Coins?");
                    switch (Console.ReadKey(true).Key)
                    {
                        case ConsoleKey.Enter:
                            BuyPackage(_Player._Coins);
                            break;
                        default:
                            break;
                    }
                    break;
                case 1:
                    Console.WriteLine("Cool bro!");
                    break;
                case 2:
                    Console.WriteLine("Cool bro!");
                    break;
                default:
                    QuitShop = true;
                    ShopSelected = 0;
                    break;
            }
        }
        public void BuyPackage(int Coins)
        {

            if (Coins >= CardPackPrice)
            {
                UpdateCoinsDB();
                Random rnd = new Random();
                int num;
                ConnectionForm con = new ConnectionForm();
                NpgsqlCommand command;
                NpgsqlDataReader dataReader;
                string sql = "";
                NpgsqlConnection connection = Connector.EstablishCon();
                for (int i = 0; i < 5; i++)
                {
                    num = rnd.Next(21) + 1;
                    connection.Open();
                    sql = $"SELECT * FROM cards WHERE cardid='{num}';";
                    command = new NpgsqlCommand(sql, connection);
                    dataReader = command.ExecuteReader();
                    dataReader.Read();
                    MonsterCard newCard = new MonsterCard(
                        (int)dataReader.GetValue(6),
                        (string)dataReader.GetValue(1),
                        (Card.MonsterType)dataReader.GetValue(4),
                        (Card.ElementType)dataReader.GetValue(2),
                        (Card.MonsterType)dataReader.GetValue(5),
                        (Card.ElementType)dataReader.GetValue(3),
                        (int)dataReader.GetValue(0)
                        );
                    _Player.PlayerCardCollection.AddMonsterCardToStack(newCard);
                    connection.Close();
                    AddCardToStackDB(newCard._CardID);
                    Console.WriteLine($"{newCard._CardName} has been added");
                }
            }
        }
        private void AddCardToStackDB(int cardid)
        {
            ConnectionForm con = new ConnectionForm();
            NpgsqlCommand command;
            NpgsqlDataReader dataReader;
            string sql = "";
            NpgsqlConnection connection = Connector.EstablishCon();
            connection.Open();
            sql = $"INSERT INTO stack (cardid,userid,status) VALUES ({cardid},{User.UserID},false);";
            command = new NpgsqlCommand(sql, connection);
            dataReader = command.ExecuteReader();
            connection.Close();
        }
        private void UpdateCoinsDB()
        {
            _Player._Coins -= 5; 
            ConnectionForm con = new ConnectionForm();
            NpgsqlCommand command;
            NpgsqlDataReader dataReader;
            string sql = "";
            NpgsqlConnection connection = Connector.EstablishCon();
            connection.Open();
            sql = $"UPDATE users SET coins={_Player._Coins-5} WHERE userid={User.UserID};";
            command = new NpgsqlCommand(sql, connection);
            dataReader = command.ExecuteReader();
            connection.Close();
        }
    }
}

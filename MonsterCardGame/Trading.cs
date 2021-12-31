using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;

namespace MonsterCardGame
{
    class Trading
    {
        private ConnectionForm con = new ConnectionForm();
        private NpgsqlConnection connection;
        private NpgsqlCommand command;
        private NpgsqlDataReader dataReader;
        private string sql = "";
        int selected = 0, Menubeginning = 0, ExitPosition = 2;
        bool Quit = false;
        User _User;
        public Trading(ref User user)
        {
            _User = user;
        }

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

        private void redirect()
        {
            switch(selected)
            {
                case 0:
                    ConfigureTrade();
                    break;
                case 1:
                    PrintTrades();
                    break;
                case 2:
                    Quit = true;
                    break;
                default:
                    break;
            }
        }

        public void OpenTradeCenter()
        {
            Quit = false;
            selected = 0;
            while (!Quit)
            {
                Console.Clear();
                Console.Write(" Create trading offer");
                if (selected == 0) { Console.WriteLine(" <="); }
                else Console.WriteLine("");
                Console.Write(" Browse trading offers");
                if (selected == 1) { Console.WriteLine(" <="); }
                else Console.WriteLine("");
                Console.Write(" Exit Trade-Center");
                if (selected == ExitPosition) { Console.WriteLine(" <="); }
                else Console.WriteLine("");
                navigation();
            }
        }

        private void ConfigureTrade()
        {
            int choice2 = -1;
            int choice3 = -1;
            int i = 0;
            string CardType = "";
            bool Confirmed = false;
            selected = 0;
            bool success = false;
            while(!Confirmed)
            {
                i = -1;
                Console.Clear();
                Console.Write("Select Card to trade: \n");
                foreach (var MonsterCard in _User.PlayerCardCollection.CardsInStack)
                {
                    i++;
                    if(i == selected)
                    {
                        Console.BackgroundColor = ConsoleColor.Blue;
                    }
                    Console.WriteLine($"\n Name: {MonsterCard._CardName}\n DMG: {MonsterCard._dmg} Type: {MonsterCard._Type}\n Element: {MonsterCard._Element} Weakness: {MonsterCard._Weakness}");
                    Console.ResetColor();
                    Console.Write("\n---------------------------------------------------------");
                }
                switch (Console.ReadKey(true).Key)
                {
                    case ConsoleKey.UpArrow:
                        if (selected > 0) { selected--; }
                        break;
                    case ConsoleKey.DownArrow:
                        if (selected < _User.PlayerCardCollection.CardsInStack.Count-1) { selected++; }
                        break;
                    case ConsoleKey.Enter:
                        Confirmed = true;
                        break;
                    default:
                        break;
                }
            }
            MonsterCard tradeCard = _User.PlayerCardCollection.CardsInStack[selected];
            selected = 0;
            Confirmed = false;
            while (!Confirmed)
            {
                Console.Clear();
                Console.WriteLine("Configure trading requirements");
                Console.Write("Request card type\n\n Monster Card"); if (selected == 0) { Console.WriteLine(" <="); }
                else Console.WriteLine("");
                Console.Write("\n Spell Card"); if (selected == 1) { Console.WriteLine(" <="); }
                else Console.WriteLine("");
                Console.Write("\n Cancel"); if (selected == 2) { Console.WriteLine(" <="); }
                else Console.WriteLine("");
                switch (Console.ReadKey(true).Key)
                {
                    case ConsoleKey.UpArrow:
                        if (selected > 0) { selected--; }
                        break;
                    case ConsoleKey.DownArrow:
                        if (selected < 2) { selected++; }
                        break;
                    case ConsoleKey.Enter:
                        Confirmed = true;
                        break;
                    default:
                        break;
                }
            }

            if (selected == 2)
            {
                selected = 0;
                Confirmed = false;
                tradeCard = null;
                Console.WriteLine("Trading offer has been canceled");
                Console.WriteLine("Press any key to continue");
                Console.ReadKey();
            }
            else 
            { 
                choice2 = selected;
                if(choice2 == 1) { CardType = "Spell"; }
                else { CardType = "Monster"; }
                while(!success)
                {
                    Console.Clear();
                    Console.Write("Enter min DMG to trade for: ");
                    success = int.TryParse(Console.ReadLine(), out choice3);
                }
                Console.Clear();
                Console.WriteLine("Review trading offer\n");
                Console.WriteLine($"Name: {tradeCard._CardName}\n DMG: {tradeCard._dmg} Type: {tradeCard._Type}\n Element: {tradeCard._Element}");
                Console.WriteLine($"Tradeing for card type {CardType} and DMG {choice3}");
                Console.WriteLine("By confirming your card will no longer show up in your stack.");
                if(Console.ReadKey(true).Key == ConsoleKey.Enter)
                {
                    CreateTrade(tradeCard, choice2, choice3);
                    _User.RemoveCard(tradeCard._CardID);
                }
                else
                {
                    Console.WriteLine("Trading offer has been canceled");
                    Console.WriteLine("Press any key to continue");
                    Console.ReadKey();
                }
            }
        }

        private void CreateTrade(MonsterCard tradeCard, int Type, int DMG)
        {
            connection = Connector.EstablishCon();
            connection.Open();
            sql = $"INSERT INTO trade (cardid, userid, wantedtype, wanteddmg) VALUES ({tradeCard._CardID},{User.UserID},{Type},{DMG});";
            command = new NpgsqlCommand(sql, connection);
            command.ExecuteReader();
            connection.Close();
        }

        public void PrintTrades()
        {
            int index = 0;
            string CardType = "Spell";
            Console.Clear();
            Console.WriteLine("     Available Trades");
            connection = Connector.EstablishCon();
            sql = "SELECT * FROM trade JOIN cards ON trade.cardid=cards.cardid";
            connection.Open();
            command = new NpgsqlCommand(sql, connection);
            dataReader = command.ExecuteReader();
            while (dataReader.Read())
            {
                index++;
                Console.Write($"                   [{index}]");
                Console.Write($"\n Offering\n {dataReader["name"]} Type: {(MonsterCard.MonsterType)dataReader["race"]} ");
                if((int)dataReader["wantedtype"] == 1)
                { CardType = "Monster"; }
                else
                { CardType = "Spell"; }
                Console.WriteLine($"DMG:{dataReader["dmg"]} Element:{(MonsterCard.ElementType)dataReader["element"]}\n Requesting: {CardType} {dataReader["wanteddmg"]} DMG");
                Console.WriteLine("_________________________");
            }
            connection.Close();
            Console.ReadKey();
        }
    }
}

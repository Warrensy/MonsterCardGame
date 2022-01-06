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
        public struct TradeOffer
        {
            public TradeOffer(int TradeIDA, int DmgRequestA, MonsterCard.MonsterType TypeRequestA, string CardNameA, MonsterCard.MonsterType CardRaceA, int CardDmgA, MonsterCard.ElementType CardElementA)
            {
                TradeID = TradeIDA;
                DmgRequest = DmgRequestA;
                TypeRequest = TypeRequestA;
                CardName = CardNameA;
                CardRace = CardRaceA;
                CardDmg = CardDmgA;
                CardElement = CardElementA;
            }
            public int TradeID { get; set; }
            public int DmgRequest { get; set; }
            public MonsterCard.MonsterType TypeRequest { get; set; }
            public string CardName { get; set; }
            public int CardDmg { get; set; }
            public MonsterCard.MonsterType CardRace { get; set; }
            public MonsterCard.ElementType CardElement { get; set; }
        }
        private ConnectionForm con = new ConnectionForm();
        private NpgsqlConnection connection;
        private NpgsqlCommand command;
        private NpgsqlDataReader dataReader;
        //private string sql = "";
        int selected = 0, Menubeginning = 0, ExitPosition = 2;
        bool Quit = false;
        User _User;
        private List<TradeOffer> TradeOffers = new List<TradeOffer>();
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
                    LoadTrades();
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
                _User.PlayerCardCollection.PrintStack(selected);
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
                Console.WriteLine($"Trading for card type {CardType} and DMG {choice3}");
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
            command = new NpgsqlCommand("INSERT INTO trade (cardid, userid, wantedtype, wanteddmg) VALUES (@CardID,@UserID,@Type,@DMG);", connection);
            command.Parameters.AddWithValue("CardID", tradeCard._CardID);
            command.Parameters.AddWithValue("UserID", User.UserID);
            command.Parameters.AddWithValue("Type", Type);
            command.Parameters.AddWithValue("DMG", DMG);
            command.ExecuteReader();
            connection.Close();
        }

        public void LoadTrades()
        {
            TradeOffers.Clear();
            Console.Clear();
            connection = Connector.EstablishCon();            
            command = new NpgsqlCommand("SELECT * FROM trade JOIN cards ON trade.cardid=cards.cardid WHERE userid!=@UserID;", connection);
            command.Parameters.AddWithValue("UserID", User.UserID);
            connection.Open();
            dataReader = command.ExecuteReader();
            while (dataReader.Read())
            {
                var offer = new TradeOffer(
                    (int)dataReader["tradeid"],
                    (int)dataReader["wanteddmg"],
                    (MonsterCard.MonsterType)dataReader["wantedtype"],
                    (string)dataReader["name"],
                    (MonsterCard.MonsterType)dataReader["race"],
                    (int)dataReader["dmg"],
                    (MonsterCard.ElementType)dataReader["element"]
                    );
                TradeOffers.Add(offer);
            }
            connection.Close();
        }
        public void PrintTrades()
        {
            bool Confirmed = false;
            int i = 0, selected = 0, count;
            string TypeRequest = "";
            while (!Confirmed)
            {
                count = 0;
                Console.Clear();
                Console.WriteLine("     -<<Available Trades>>-");
                foreach (var item in TradeOffers)
                {
                    if(selected == count)
                    {
                        Console.BackgroundColor = ConsoleColor.Blue;
                    }
                    if(item.TypeRequest == Card.MonsterType.Spell)
                    {
                        TypeRequest = "Spell";
                    }
                    else
                    {
                        TypeRequest = "Monster";
                    }
                    Console.WriteLine("\n-Offer-");
                    Console.Write($" {item.CardName} - {item.CardDmg} DMG - {item.CardElement} Type");
                    Console.WriteLine("\n-Request-");
                    Console.Write($" {item.DmgRequest} DMG - Type {TypeRequest}");
                    Console.ResetColor();
                    Console.WriteLine("\n _______________________________________");
                    count++;
                }

                switch (Console.ReadKey(true).Key)
                {
                    case ConsoleKey.UpArrow:
                        if (selected > 0) { selected--; }
                        else if (selected == 0) { selected = TradeOffers.Count-1; }
                        break;
                    case ConsoleKey.DownArrow:
                        if (selected < (TradeOffers.Count)) { selected++; }
                        else if (selected == TradeOffers.Count-1) { selected = 0; }
                        break;
                    case ConsoleKey.Enter:
                        Confirmed = true;
                        break;
                    default:
                        break;
                }
            }
            if(selected < TradeOffers.Count)
            {
                PrepareTrade(TradeOffers[selected], TypeRequest);
            }
        }
        public void PrepareTrade(TradeOffer item, string TypeRequest)
        {
            int count = 0, selected = 0, index = 0;
            bool Confirmed = false;
            while (!Confirmed)
            {
                Console.Clear();
                Console.WriteLine($"-<<Select Card to Trade>>-\n {item.DmgRequest} + {TypeRequest}");
                count = index = 0;
                foreach (var card in _User.PlayerCardCollection.CardsInStack)
                {
                    if (card._dmg >= item.DmgRequest)
                    {
                        if (item.TypeRequest == MonsterCard.MonsterType.Spell)
                        {
                            if (card._Type == Card.MonsterType.Spell)
                            {
                                Console.WriteLine($"                                                [{count}]");
                                if (selected == index) { Console.BackgroundColor = ConsoleColor.Blue; }
                                Console.WriteLine($" Name: {card._CardName}\n DMG: {card._dmg} Type: {card._Type}\n Element: {card._Element} Weakness: {card._Weakness}");
                                Console.ResetColor();
                                Console.WriteLine("---------------------------------------------------------");
                                index++;
                            }
                        }
                        else
                         {
                            if (card._Type != MonsterCard.MonsterType.Spell)
                            {
                                Console.WriteLine($"                                                [{count}]");
                                if (selected == index) { Console.BackgroundColor = ConsoleColor.Blue; }
                                Console.WriteLine($" Name: {card._CardName}\n DMG: {card._dmg} Type: {card._Type}\n Element: {card._Element} Weakness: {card._Weakness}");
                                Console.ResetColor();
                                Console.WriteLine("---------------------------------------------------------");
                                index++;
                            }
                        }
                    }
                    count++;
                }
                if (selected == index)
                {
                    Console.BackgroundColor = ConsoleColor.Blue;
                }
                Console.WriteLine("\nCancel");
                Console.ResetColor();

                switch (Console.ReadKey(true).Key)
                {
                    case ConsoleKey.UpArrow:
                        if (selected > 0) { selected--; }
                        break;
                    case ConsoleKey.DownArrow:
                        if (selected <= index) { selected++; }
                        break;
                    case ConsoleKey.Enter:
                        Confirmed = true;
                        break;
                    default:
                        break;
                }
            }
            if(selected < index)
            {
                AcceptTrade(item.TradeID, _User.PlayerCardCollection.CardsInStack[selected]);
            }
        }
        public void AcceptTrade(int TradeID, MonsterCard TradeCard)
        {
            int OGID = 0, CardID = 0;
            //Get Trade info
            connection = Connector.EstablishCon();
            connection.Open();
            command = new NpgsqlCommand("SELECT * FROM trade JOIN cards ON trade.cardid=cards.cardid WHERE tradeid=@TradeID;", connection);
            command.Parameters.AddWithValue("TradeID", TradeID);
            dataReader = command.ExecuteReader();
            dataReader.Read();
            var TradeOfferCard = new MonsterCard(
                (int)dataReader["dmg"],
                (string)dataReader["name"],
                (Card.MonsterType)dataReader["race"],
                (Card.ElementType)dataReader["element"],
                (Card.MonsterType)dataReader["race_w"],
                (Card.ElementType)dataReader["element_w"],
                (int)dataReader["cardid"]
            );
            OGID = (int)dataReader["userid"];
            CardID = (int)dataReader["cardid"];
            connection.Close();

            //Create stack entrys in DB for both players
            connection.Open();
            //= $"INSERT INTO stack (cardid, userid) VALUES (@TradeCard._CardID,@OGID);" +
                  //$"INSERT INTO stack (cardid, userid) VALUES (@CardID,@User.UserID);";
            command = new NpgsqlCommand("INSERT INTO stack (cardid, userid) VALUES (@TradeCardID,@OGID);INSERT INTO stack (cardid, userid) VALUES (@CardID,@UserID);", connection);
            command.Parameters.AddWithValue("TradeCardID", TradeCard._CardID);
            command.Parameters.AddWithValue("OGID", OGID);
            command.Parameters.AddWithValue("CardID", CardID);
            command.Parameters.AddWithValue("UserID", User.UserID);
            command.ExecuteReader();
            connection.Close();
            
            //Remove traded cards from user
            connection.Open();
            //sql = $"DELETE FROM trade WHERE tradeid='{TradeID}';";
            command = new NpgsqlCommand("DELETE FROM trade WHERE tradeid=@TradeID;", connection);
            command.Parameters.AddWithValue("TradeID", TradeID);
            command.ExecuteReader();
            connection.Close();
            _User.PlayerCardCollection.RemoveCardFromStackByName(TradeCard._CardName);

            //Add new card to user stack
            _User.PlayerCardCollection.CardsInStack.Add(TradeOfferCard);

            Console.WriteLine($"Trade has been successfull.\n\n {TradeOfferCard._CardName} was added to you stack.");
            Console.ReadKey();
        }
    }
}

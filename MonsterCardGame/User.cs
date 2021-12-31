using System;
using System.Linq;
using Npgsql;

namespace MonsterCardGame
{
    class User
    {
        private ConnectionForm con = new ConnectionForm();
        private NpgsqlCommand command; 
        private NpgsqlDataReader dataReader;
        private string sql = "";
        private NpgsqlConnection connection;
        public static int UserID { set; get; }
        public User()
        {
            PlayerCardCollection = new CardStack();
            PlayerDeck = new Deck();
        }
        public int Coins { get; set; }
        public int Elo { get; set; }
        public Deck PlayerDeck;
        public CardStack PlayerCardCollection;
        public void ManageDeck()
        {
            string exit = "";
            bool CorrectInput = true;
            while (exit != "x")
            {
                Console.Clear();
                PlayerCardCollection.PrintStack();
                PlayerDeck.PrintDeck(PlayerCardCollection.CardsInStack.Count);
                Console.WriteLine("\nAdd/Remove card from active deck ");
                Console.WriteLine("Enter CardID");
                Console.WriteLine("Enter \"x\" to go back");
                Console.WriteLine("Confirm with Enter");
                if(CorrectInput)
                {
                    Console.WriteLine("Input: ");
                }
                else
                {
                    Console.WriteLine("Invalide Input.\nTry Again: ");
                }
                exit = Console.ReadLine();

                int input, id;
                if(CorrectInput = int.TryParse(exit, out input))
                {
                    int StackCount = PlayerCardCollection.CardsInStack.Count;
                    int DeckCount = PlayerDeck.CardDeck.Count;
                    if(DeckCount >= 4 && input <= StackCount)
                    {
                        Console.WriteLine(" -------------");
                        Console.WriteLine("|Deck is full.|");
                        Console.WriteLine(" -------------");
                        Console.ReadKey();
                    }
                    else if (input <= StackCount)
                    {
                        id = PlayerCardCollection.CardsInStack[input-1]._CardID;
                        PlayerDeck.AddCardToDeck(PlayerCardCollection.CardsInStack[input-1]);
                        PlayerCardCollection.RemoveCardByIndex(input-1);
                        UpdateStack(id, "true");
                    }
                    else if (input > StackCount && input <= (StackCount + DeckCount))
                    {
                        id = PlayerDeck.CardDeck[(input - 1) - StackCount]._CardID;
                        PlayerCardCollection.AddMonsterCardToStack(PlayerDeck.CardDeck[(input - 1) - StackCount]);
                        PlayerDeck.RemoveCardFromDeckByName(PlayerCardCollection.CardsInStack.Last()._CardName);
                        UpdateStack(id, "false");
                    }
                }
            }
        }
        private void UpdateStack(int cardid, string status)
        {
            connection = Connector.EstablishCon();
            connection.Open();
            sql = $"UPDATE stack SET status='{status}' WHERE cardid='{cardid}' AND userid='{User.UserID}';";
            command = new NpgsqlCommand(sql, connection);
            dataReader = command.ExecuteReader();
            connection.Close();
        }
        public void TradeCard()
        {
            throw new System.NotImplementedException();
        }

        public void RemoveCard(int cardid)
        {
            int i = 0;
            foreach (var Monstercard in PlayerCardCollection.CardsInStack)
            {
                i++;
                if(cardid == Monstercard._CardID)
                {
                    PlayerCardCollection.RemoveCardByIndex(i);
                    break;
                }
            }
            connection = Connector.EstablishCon();
            connection.Open();
            sql = $"DELETE FROM stack WHERE cardid='{cardid}' AND userid='{User.UserID}';";
            command = new NpgsqlCommand(sql, connection);
            dataReader = command.ExecuteReader();
            connection.Close();
        }
        public void LoadStack()
        {
            connection = Connector.EstablishCon();
            connection.Open();
            sql = $"SELECT * FROM cards JOIN stack ON cards.cardid=stack.cardid WHERE userid='{User.UserID}' AND intrading=false;";
            command = new NpgsqlCommand(sql, connection);       
            dataReader = command.ExecuteReader();

            while (dataReader.Read())
            {
                MonsterCard newCard = new MonsterCard(
                    (int)dataReader["dmg"],
                    (string)dataReader["name"],
                    (Card.MonsterType)dataReader["race"],
                    (Card.ElementType)dataReader["element"],
                    (Card.MonsterType)dataReader["race_w"],
                    (Card.ElementType)dataReader["element_w"],
                    (int)dataReader["cardid"]
                    );
                if(!(bool)dataReader.GetValue(10))
                {
                    PlayerCardCollection.AddMonsterCardToStack(newCard);
                }
                if((bool)dataReader.GetValue(10))
                {
                    PlayerDeck.AddCardToDeck(newCard);
                }
            }
            connection.Close();
        }
        public void UpdatePlayerStatistic(int gamewon)
        {
                int elo, won, played;
                connection = Connector.EstablishCon();
                connection.Open();
                sql = $"SELECT elo, gameswon, gamesplayed FROM users WHERE userid='{User.UserID}';";
                command = new NpgsqlCommand(sql, connection);
                dataReader = command.ExecuteReader();
                dataReader.Read();
                elo = (int)dataReader["elo"];
                won = (int)dataReader["gameswon"];
                played = (int)dataReader["gamesplayed"];
                connection.Close();
                connection = Connector.EstablishCon();
                connection.Open();
                if(gamewon == 1)
                {
                    elo += 3; won++; played++;
                    sql = $"UPDATE users SET elo='{elo}', gameswon='{won}', gamesplayed='{played}' WHERE userid='{User.UserID}';";
                }
                else if(gamewon == 0)
                {
                    elo -= 5; played++;
                    sql = $"UPDATE users SET elo='{elo}', gamesplayed='{played}' WHERE userid='{User.UserID}';";
                }
                else
                {
                    played++;
                    sql = $"UPDATE users SET gamesplayed='{played}' WHERE userid='{User.UserID}';";
                }
                command = new NpgsqlCommand(sql, connection);
                dataReader = command.ExecuteReader();
                connection.Close();
        }
        public void LoadProfile()
        {
            connection = Connector.EstablishCon();
            connection.Open();
            sql = $"SELECT * FROM users WHERE userid='{User.UserID}';";
            command = new NpgsqlCommand(sql, connection);
            dataReader = command.ExecuteReader();
            dataReader.Read();
            Console.WriteLine($"{(string)dataReader["username"]}");
            Console.WriteLine($"ELO {(int)dataReader["elo"]}");
            Console.WriteLine($"Coins {(int)dataReader["coins"]}");
            Console.WriteLine($"      WON / PLAYED");
            Console.WriteLine($"Games   {(int)dataReader["gameswon"]} / {(int)dataReader["gamesplayed"]}");
            Console.WriteLine($"Cards {PlayerCardCollection.CardsInStack.Count + PlayerDeck.CardDeck.Count}");
            connection.Close();
            Console.ReadKey();
        }
    }
}

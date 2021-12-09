using System;
using System.Linq;
using Npgsql;

namespace MonsterCardGame
{
    class User : IUser
    {
        public static int UserID { set; get; }
        public static int Coins { set; get; }
        public static int ELO { set; get; }
        public User()
        {
            PlayerCardCollection = new CardStack();
            PlayerDeck = new Deck();
        }
        public int _Coins { get; set; }
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

                int i, id;
                if(CorrectInput = int.TryParse(exit, out i))
                {
                    int StackCount = PlayerCardCollection.CardsInStack.Count;
                    int DeckCount = PlayerDeck.CardDeck.Count;
                    if(DeckCount >= 4 && i <= StackCount)
                    {
                        Console.WriteLine(" -------------");
                        Console.WriteLine("|Deck is full.|");
                        Console.WriteLine(" -------------");
                        System.Threading.Thread.Sleep(1500);
                    }
                    else if (i <= StackCount)
                    {
                        id = PlayerCardCollection.CardsInStack[i]._CardID;
                        PlayerDeck.AddCardToDeck(PlayerCardCollection.CardsInStack[i-1]);
                        PlayerCardCollection.RemoveCardByIndex(i-1);
                        UpdateStack(id, "true");
                    }
                    else if (i > StackCount && i <= (StackCount + DeckCount))
                    {
                        id = PlayerDeck.CardDeck[(i - 1) - StackCount]._CardID;
                        PlayerCardCollection.AddMonsterCardToStack(PlayerDeck.CardDeck[(i - 1) - StackCount]);
                        PlayerDeck.RemoveCardFromDeckByName(PlayerCardCollection.CardsInStack.Last()._CardName);
                        UpdateStack(id, "false");
                    }
                }
            }
        }
        /*private void AddtoStackDB(int cardid)
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
        }*/
        private void UpdateStack(int cardid, string status)
        {
            ConnectionForm con = new ConnectionForm();
            NpgsqlCommand command;
            NpgsqlDataReader dataReader;
            string sql = "";
            NpgsqlConnection connection = Connector.EstablishCon();
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

        public void RemoveCard()
        {
            throw new System.NotImplementedException();
        }
        public void LoadStack()
        {
            ConnectionForm con = new ConnectionForm();
            NpgsqlCommand command;
            NpgsqlDataReader dataReader;
            string sql = "";
            NpgsqlConnection connection = Connector.EstablishCon();
            connection.Open();
            sql = $"SELECT * FROM cards JOIN stack ON cards.cardid=stack.cardid WHERE userid='{User.UserID}';";
            command = new NpgsqlCommand(sql, connection);       
            dataReader = command.ExecuteReader();

            while (dataReader.Read())
            {
                MonsterCard newCard = new MonsterCard(
                    (int)dataReader.GetValue(6),
                    (string)dataReader.GetValue(1),
                    (Card.MonsterType)dataReader.GetValue(4),
                    (Card.ElementType)dataReader.GetValue(2),
                    (Card.MonsterType)dataReader.GetValue(5),
                    (Card.ElementType)dataReader.GetValue(3),
                    (int)dataReader.GetValue(0)
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
    }
}

using System;
using System.Linq;
using Npgsql;

namespace MonsterCardGame
{
    public class User
    {
        private ConnectionForm con = new ConnectionForm();
        private NpgsqlCommand command;
        private NpgsqlDataReader dataReader;
        private string sql = "";
        private NpgsqlConnection connection;
        private bool Confirmed = false;
        private int selected = 0;
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
        public Gild _Gild;

        public enum Gild
        {
            None,
            Goldrush,
            Wildfire,
            ShadowLegions,
            SandNomads,
        }
        public void ManageDeck()
        {
            int id, StackCount, DeckCount;
            bool quit = false;
            while(!quit)
            {
                StackCount = PlayerCardCollection.CardsInStack.Count;
                DeckCount = PlayerDeck.CardDeck.Count;
                Confirmed = false;
                while (!Confirmed)
                {
                    Console.Clear();
                    PlayerCardCollection.PrintStack(selected);
                    PlayerDeck.PrintDeck(PlayerCardCollection.CardsInStack.Count-1, selected);
                    Console.WriteLine("\nAdd/Remove card from deck ");

                    switch (Console.ReadKey(true).Key)
                    {
                        case ConsoleKey.UpArrow:
                            if (selected > 0) { selected--; }
                            else if (selected == 0) { selected = StackCount + DeckCount; }
                            break;
                        case ConsoleKey.DownArrow:
                            if (selected < (StackCount + DeckCount)) { selected++; }
                            else if (selected == StackCount + DeckCount) { selected = 0; }
                            break;
                        case ConsoleKey.Enter:
                            Confirmed = true;
                            break;
                        default:
                            break;
                    }
                }
                if (DeckCount >= 4 && selected <= StackCount-1)
                {
                    Console.WriteLine(" -------------");
                    Console.WriteLine("|Deck is full.|");
                    Console.WriteLine(" -------------");
                    Console.ReadKey();
                }
                else if (selected <= StackCount-1)
                {
                    id = PlayerCardCollection.CardsInStack[selected]._CardID;
                    PlayerDeck.AddCardToDeck(PlayerCardCollection.CardsInStack[selected]);
                    PlayerCardCollection.RemoveCardByIndex(selected);
                    UpdateStack(id, true);
                }
                else if (selected > StackCount-1 && selected <= (StackCount + DeckCount-1))
                {
                    id = PlayerDeck.CardDeck[(selected) - StackCount]._CardID;
                    PlayerCardCollection.AddMonsterCardToStack(PlayerDeck.CardDeck[(selected) - StackCount]);
                    PlayerDeck.RemoveCardFromDeckByName(PlayerCardCollection.CardsInStack.Last()._CardName);
                    UpdateStack(id, false);
                }
                if(selected > (StackCount + DeckCount - 1))
                {
                    quit = true;                   
                }
            }
                selected = 0;
        }
        private void UpdateStack(int cardid, bool status)
        {
            int i;
            connection = Connector.EstablishCon();
            connection.Open();
            //sql = $"SELECT id FROM stack WHERE cardid='{cardid}' AND userid='{User.UserID}';";
            command = new NpgsqlCommand("SELECT id FROM stack WHERE cardid=@cardid AND userid=@UserID;", connection);
            command.Parameters.AddWithValue("cardid", cardid);
            command.Parameters.AddWithValue("UserID", User.UserID);
            dataReader = command.ExecuteReader();
            dataReader.Read();
            i = (int)dataReader["id"];
            connection.Close();
            connection.Open();
            //sql = $"UPDATE stack SET status='{status}' WHERE id='{i}';";
            command = new NpgsqlCommand("UPDATE stack SET status=@status WHERE id=@index;", connection);
            command.Parameters.AddWithValue("status", status);
            command.Parameters.AddWithValue("index", i);
            command.ExecuteReader();
            connection.Close();
        }

        public void RemoveCard(int cardid)
        {
            int i = 0;
            connection = Connector.EstablishCon();
            connection.Open();
            //sql = $"SELECT id FROM stack WHERE cardid='{cardid}' AND userid='{User.UserID}';";
            command = new NpgsqlCommand("SELECT id FROM stack WHERE cardid=@cardid AND userid=@UserID", connection);
            command.Parameters.AddWithValue("cardid", cardid);
            command.Parameters.AddWithValue("UserID", User.UserID);
            dataReader = command.ExecuteReader();
            dataReader.Read();
            i = (int)dataReader["id"];
            connection.Close();
            connection.Open();
            //sql = $"DELETE FROM stack WHERE id='{i}';";
            command = new NpgsqlCommand("DELETE FROM stack WHERE id=@index;", connection);
            command.Parameters.AddWithValue("index", i);
            command.ExecuteReader();
            connection.Close();
        }
        public void LoadStack()
        {
            connection = Connector.EstablishCon();
            connection.Open();
            //sql = $"SELECT * FROM cards JOIN stack ON cards.cardid=stack.cardid WHERE userid='{User.UserID}';";
            command = new NpgsqlCommand("SELECT * FROM cards JOIN stack ON cards.cardid=stack.cardid WHERE userid=@UserID;", connection);
            command.Parameters.AddWithValue("UserID", User.UserID);
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
                //sql = $"SELECT elo, gameswon, gamesplayed FROM users WHERE userid='{User.UserID}';";
                command = new NpgsqlCommand("SELECT elo, gameswon, gamesplayed FROM users WHERE userid=@UserID;", connection);
                command.Parameters.AddWithValue("UserID", User.UserID);
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
                    //sql = $"UPDATE users SET elo='{elo}', gameswon='{won}', gamesplayed='{played}' WHERE userid='{User.UserID}';";
                    command = new NpgsqlCommand("UPDATE users SET elo=@elo, gameswon=@won, gamesplayed=@played WHERE userid=@UserID;", connection);
                    command.Parameters.AddWithValue("UserID", User.UserID);
                    command.Parameters.AddWithValue("elo", elo);
                    command.Parameters.AddWithValue("won", won);
                    command.Parameters.AddWithValue("played", played);
                }
                else if(gamewon == 0)
                {
                    elo -= 5; played++;
                    //sql = $"UPDATE users SET elo='{elo}', gamesplayed='{played}' WHERE userid='{User.UserID}';";
                    command = new NpgsqlCommand("UPDATE users SET elo=@elo, gamesplayed=@played WHERE userid=@UserID;", connection);
                    command.Parameters.AddWithValue("UserID", User.UserID);
                    command.Parameters.AddWithValue("elo", elo);
                    command.Parameters.AddWithValue("played", played);

                }
                else
                {
                    played++;
                    //sql = $"UPDATE users SET gamesplayed='{played}' WHERE userid='{User.UserID}';";
                    command = new NpgsqlCommand("UPDATE users SET gamesplayed=@played WHERE userid=@UserID;", connection);
                    command.Parameters.AddWithValue("UserID", User.UserID);
                    command.Parameters.AddWithValue("played", played);
                }
                dataReader = command.ExecuteReader();
                connection.Close();
        }
        public void LoadProfile()
        {
            connection = Connector.EstablishCon();
            connection.Open();
            //sql = $"SELECT * FROM users WHERE userid='{User.UserID}';";
            command = new NpgsqlCommand("SELECT * FROM users WHERE userid=@UserID;", connection);
            command.Parameters.AddWithValue("UserID", User.UserID);
            dataReader = command.ExecuteReader();
            dataReader.Read();
            Console.WriteLine($"()");
            Console.Write($"{(string)dataReader["username"]}");
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

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
            SandNomad
        }
        public void ManageDeck()
        {
            //Printing of all cards in player stack with selection function
            int id, StackCount, DeckCount;
            bool quit = false;
            while(!quit)
            {
                StackCount = PlayerCardCollection.CardsInStack.Count;
                DeckCount = PlayerDeck.CardDeck.Count;
                //set select to navigate between cards 
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
            //Find the specific id entry of a card in stack
            int i;
            connection = Connector.EstablishCon();
            connection.Open();
            command = new NpgsqlCommand("SELECT id FROM stack WHERE cardid=@cardid AND userid=@UserID;", connection);
            command.Parameters.AddWithValue("cardid", cardid);
            command.Parameters.AddWithValue("UserID", User.UserID);
            dataReader = command.ExecuteReader();
            dataReader.Read();
            i = (int)dataReader["id"];
            connection.Close();
            //Change deck status of specific card
            connection.Open();
            command = new NpgsqlCommand("UPDATE stack SET status=@status WHERE id=@index;", connection);
            command.Parameters.AddWithValue("status", status);
            command.Parameters.AddWithValue("index", i);
            command.ExecuteReader();
            connection.Close();
        }

        public void RemoveCard(int cardid)
        {
            //Find the specific id entry of a card in stack
            int i = 0;
            connection = Connector.EstablishCon();
            connection.Open();
            command = new NpgsqlCommand("SELECT id FROM stack WHERE cardid=@cardid AND userid=@UserID", connection);
            command.Parameters.AddWithValue("cardid", cardid);
            command.Parameters.AddWithValue("UserID", User.UserID);
            dataReader = command.ExecuteReader();
            dataReader.Read();
            i = (int)dataReader["id"];
            connection.Close();
            //with the specific id I can remove a card without worring about duplicates in the player stack
            connection.Open();
            command = new NpgsqlCommand("DELETE FROM stack WHERE id=@index;", connection);
            command.Parameters.AddWithValue("index", i);
            command.ExecuteReader();
            connection.Close();
        }
        public void LoadStack()
        {
            //Loadstack is loaded as soon as a user is logedin
            connection = Connector.EstablishCon();
            connection.Open();
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
            //1 == won, 0 = lost, -1 == draw gets set by the Logic.Battle function
            int elo, won, played;
            connection = Connector.EstablishCon();
            connection.Open();
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
                elo += 3; won++; played++; Coins++;
                if(_Gild == Gild.Goldrush)
                { Coins++; }
                command = new NpgsqlCommand("UPDATE users SET elo=@elo, coins=@Coins, gameswon=@won, gamesplayed=@played WHERE userid=@UserID;", connection);
                command.Parameters.AddWithValue("UserID", User.UserID);
                command.Parameters.AddWithValue("elo", elo);
                command.Parameters.AddWithValue("won", won);
                command.Parameters.AddWithValue("played", played);
                command.Parameters.AddWithValue("Coins", Coins);
            }
            else if(gamewon == 0)
            {
                elo -= 5; played++; Coins--;
                command = new NpgsqlCommand("UPDATE users SET elo=@elo, coins=@Coins, gamesplayed=@played WHERE userid=@UserID;", connection);
                command.Parameters.AddWithValue("UserID", User.UserID);
                command.Parameters.AddWithValue("elo", elo);
                command.Parameters.AddWithValue("played", played);
                command.Parameters.AddWithValue("Coins", Coins);
            }
            else
            {
                played++;
                command = new NpgsqlCommand("UPDATE users SET gamesplayed=@played WHERE userid=@UserID;", connection);
                command.Parameters.AddWithValue("UserID", User.UserID);
                command.Parameters.AddWithValue("played", played);
            }
            dataReader = command.ExecuteReader();
            connection.Close();
        }
        public void LoadProfilePage()
        {
            //Prints all User Information except the userID. For security reasons it should never be displayed anywhere
            // Allows User to change assigned gild
            bool quit = false;
            Confirmed = false;
            selected = 0;
            while (!quit)
            {
                while (!Confirmed)
                {
                    Console.Clear();
                    connection = Connector.EstablishCon();
                    connection.Open();
                    command = new NpgsqlCommand("SELECT * FROM users WHERE userid=@UserID;", connection);
                    command.Parameters.AddWithValue("UserID", User.UserID);
                    dataReader = command.ExecuteReader();
                    dataReader.Read();
                    Console.WriteLine($"{(User.Gild)dataReader["gild"]}");
                    Console.WriteLine($"user: {(string)dataReader["username"]}");
                    Console.WriteLine($"ELO {(int)dataReader["elo"]}");
                    Console.WriteLine($"Coins {(int)dataReader["coins"]}");
                    Console.WriteLine($"      WON / PLAYED");
                    Console.WriteLine($"Games   {(int)dataReader["gameswon"]} / {(int)dataReader["gamesplayed"]}");
                    Console.WriteLine($"Cards {PlayerCardCollection.CardsInStack.Count + PlayerDeck.CardDeck.Count}");
                    connection.Close();
                    if (selected == 0)
                    { Console.BackgroundColor = ConsoleColor.Blue; }
                    Console.WriteLine("\n\nChange Gild");
                    Console.ResetColor();
                    if (selected == 1)
                    { Console.BackgroundColor = ConsoleColor.Blue; }
                    Console.WriteLine("Change Password");
                    Console.ResetColor();
                    if (selected == 2)
                    { Console.BackgroundColor = ConsoleColor.Blue; }
                    Console.WriteLine("Go Back");
                    Console.ResetColor();

                    switch (Console.ReadKey(true).Key)
                    {
                        case ConsoleKey.UpArrow:
                            if (selected > 0) { selected--; }
                            else if (selected == 0) { selected = 3; }
                            break;
                        case ConsoleKey.DownArrow:
                            if (selected < 3) { selected++; }
                            else if (selected == 2) { selected = 0; }
                            break;
                        case ConsoleKey.Enter:
                            Confirmed = true;
                            break;
                        default:
                            break;
                    }
                }
                switch (selected) 
                {
                    case 0:
                        changeGuild();
                        break;
                    case 1:
                        changePassword();
                        break;
                    case 2:
                        quit = true;
                        break;
                }
                Confirmed = false;  
            }
        }
        void changeGuild()
        {
            string Info = "";
            selected = 0;
            Confirmed = false;
            while (!Confirmed)
            {
                Console.Clear();
                if (selected == 0)
                {   
                    Console.BackgroundColor = ConsoleColor.Blue; 
                    Info = "Get +1 coin when winning a match";
                }
                Console.WriteLine("Goldrush");
                Console.ResetColor();
                if (selected == 1)
                { 
                    Console.BackgroundColor = ConsoleColor.Blue; 
                    Info = "Shop prices are -1 coins";
                }
                Console.WriteLine("SandNomads");
                Console.ResetColor();
                if (selected == 2)
                { 
                    Console.BackgroundColor = ConsoleColor.Blue; 
                    Info = "";
                }
                Console.WriteLine("Cancel\n");
                Console.ResetColor();
                Console.WriteLine(Info);
                switch (Console.ReadKey(true).Key)
                {
                    case ConsoleKey.UpArrow:
                        if (selected > 0) { selected--; }
                        else if (selected == 0) { selected = 3; }
                        break;
                    case ConsoleKey.DownArrow:
                        if (selected < 3) { selected++; }
                        else if (selected == 2) { selected = 0; }
                        break;
                    case ConsoleKey.Enter:
                        Confirmed = true;
                        break;
                    default:
                        break;
                }
            }
            switch (selected)
            {
                case 0:
                    setGuild(Gild.Goldrush);
                    break;
                case 1:
                    setGuild(Gild.SandNomad);
                    break;
                default:
                    break;
            }
        }
        void changePassword()
        {
            Console.Write("Enter new password: ");
            string newPassword = ConnectionForm.GetPassword();
            Console.Write("Repeat new password: ");
            string repeatPassword = ConnectionForm.GetPassword();
            if(newPassword == repeatPassword)
            {
                connection = Connector.EstablishCon();
                connection.Open();
                command = new NpgsqlCommand("UPDATE users SET password=@password WHERE userid=@userid;", connection);
                command.Parameters.AddWithValue("userid", UserID);
                command.Parameters.AddWithValue("password", newPassword);
                command.ExecuteReader();
                connection.Close();
            }
            Console.WriteLine("Password has been changed");
        }
        void setGuild(Gild gild)
        {
            _Gild = gild;
            connection = Connector.EstablishCon();
            connection.Open();
            command = new NpgsqlCommand("UPDATE users SET gild=@Gild WHERE userid=@Userid;", connection);
            command.Parameters.AddWithValue("Gild", (int)gild);
            command.Parameters.AddWithValue("Userid", UserID);
            command.ExecuteReader();
            connection.Close();
        }
    }
}

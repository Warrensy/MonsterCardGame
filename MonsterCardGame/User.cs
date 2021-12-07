using System;
using System.Linq;

namespace MonsterCardGame
{
    class User : IUser
    {
        const int CardPackPrice = 5;
        const int StartCoins = 20;
        public User()
        {
            _Coins = StartCoins;
            PlayerCardCollection = new CardStack();
            PlayerDeck = new Deck();
        }
        public int _Coins { get; set; }
        public Deck PlayerDeck;
        public CardStack PlayerCardCollection;
        
        public void BuyPackage()
        {
            if (_Coins >= CardPackPrice)
            {
                //open Pack, Add 5 Cards to Stack
            }
        }

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

                int i;
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
                        PlayerDeck.AddCardToDeck(PlayerCardCollection.CardsInStack[i-1]);
                        PlayerCardCollection.RemoveCardByIndex(i-1);
                    }
                    else if (i > StackCount && i <= (StackCount + DeckCount))
                    {
                        PlayerCardCollection.AddMonsterCardToStack(PlayerDeck.CardDeck[(i - 1) - StackCount]);
                        PlayerDeck.RemoveCardFromDeckByName(PlayerCardCollection.CardsInStack.Last()._CardName);
                    }
                }
            }
        }
        
        public void TradeCard()
        {
            throw new System.NotImplementedException();
        }

        public void RemoveCard()
        {
            throw new System.NotImplementedException();
        }
    }
}

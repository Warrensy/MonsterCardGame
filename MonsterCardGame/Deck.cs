using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonsterCardGame
{
    public class Deck : IPlayerDeck
    {
        public List<MonsterCard> CardDeck = new List<MonsterCard>();
        Card _DeckPatreon { get; set; }

        public void AddCardToDeck(MonsterCard NewCard)
        {
            CardDeck.Add(NewCard);
        }

        public void RemoveCardFromDeckByName(string CardName)
        {
            for (int i = CardDeck.Count - 1; i >= 0; i--)
            {
                if (CardDeck[i]._CardName == CardName)
                {
                    CardDeck.Remove(CardDeck[i]);
                    break;
                }
            }
        }
        public void PrintDeck(int count, int selected)
        {
            Console.WriteLine("\n --Active Deck--\n");
            foreach (var MonsterCard in CardDeck)
            {
                count++;
                if (selected == count) { Console.BackgroundColor = ConsoleColor.Blue; }
                Console.WriteLine($"                                                [{count}]");
                Console.WriteLine($" Name: {MonsterCard._CardName}\n DMG: {MonsterCard._dmg} Type: {MonsterCard._Type}\n Element: {MonsterCard._Element} Weakness: {MonsterCard._Weakness}");
                Console.ResetColor();
                Console.WriteLine("---------------------------------------------------------");
            }
            if(selected > count) { Console.BackgroundColor = ConsoleColor.Blue; }
            Console.WriteLine("\n Back to menu");
            Console.ResetColor();
        }
        public Deck ShuffleDeck()
        {
            Deck ShuffeledDeck = new Deck();
            var rand = new Random();
            ShuffeledDeck.CardDeck = CardDeck.OrderBy(x => rand.Next()).ToList();

            return ShuffeledDeck;
        }
    }
}

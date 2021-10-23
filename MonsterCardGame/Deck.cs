using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonsterCardGame
{
    class Deck : IPlayerDeck
    {
        public List<MonsterCard> CardDeck = new List<MonsterCard>();
        Card _DeckPatreon { get; set; }

        public void AddCardToDeck(MonsterCard NewCard)
        {
            CardDeck.Add(NewCard);
        }

        public void RemoveCardFromDeckByName(string CardName)
        {
            for (int i = CardDeck.Capacity - 1; i >= 0; i--)
            {
                if (CardDeck[i]._CardName == CardName)
                {
                    CardDeck.Remove(CardDeck[i]);
                    break;
                }
            }
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonsterCardGame
{
    class CardDB
    {
        public CardDB(User Player1, User Player2)
        {
            Player1.PlayerCardCollection.AddMonsterCardToStack(newCard1);
            Player1.PlayerCardCollection.AddMonsterCardToStack(newCard2);
            Player1.PlayerCardCollection.AddMonsterCardToStack(newCard3);
            Player1.PlayerCardCollection.AddMonsterCardToStack(newCard4);
            Player1.PlayerCardCollection.AddMonsterCardToStack(newCard5);
            Player1.PlayerCardCollection.AddMonsterCardToStack(newCard6);
            Player1.PlayerCardCollection.AddMonsterCardToStack(newCard7);
            Player1.PlayerCardCollection.AddMonsterCardToStack(newCard8);
            Player1.PlayerCardCollection.AddMonsterCardToStack(newCard9);
            Player1.PlayerCardCollection.AddMonsterCardToStack(newCard10);
            Player2.PlayerCardCollection.AddMonsterCardToStack(newCard1);
            Player2.PlayerCardCollection.AddMonsterCardToStack(newCard2);
            Player2.PlayerCardCollection.AddMonsterCardToStack(newCard3);
            Player2.PlayerCardCollection.AddMonsterCardToStack(newCard4);
            Player2.PlayerCardCollection.AddMonsterCardToStack(newCard5);
            Player2.PlayerCardCollection.AddMonsterCardToStack(newCard6);
            Player2.PlayerCardCollection.AddMonsterCardToStack(newCard7);
            Player2.PlayerCardCollection.AddMonsterCardToStack(newCard8);
            Player2.PlayerCardCollection.AddMonsterCardToStack(newCard9);
            Player2.PlayerCardCollection.AddMonsterCardToStack(newCard10);
            Player1.PlayerDeck.CardDeck.Add(newCard1);
            Player1.PlayerDeck.CardDeck.Add(newCard2);
            Player1.PlayerDeck.CardDeck.Add(newCard3);
            Player1.PlayerDeck.CardDeck.Add(newCard4);
            Player1.PlayerDeck.CardDeck.Add(newCard5);
            Player2.PlayerDeck.CardDeck.Add(newCard6);
            Player2.PlayerDeck.CardDeck.Add(newCard7);
            Player2.PlayerDeck.CardDeck.Add(newCard8);
            Player2.PlayerDeck.CardDeck.Add(newCard9);
            Player2.PlayerDeck.CardDeck.Add(newCard10);
        }
        MonsterCard newCard1 = new MonsterCard(7, "Fire Dragon", Card.MonsterType.Dragon, Card.ElementType.Fire, 1, Card.MonsterType.FireElves, Card.ElementType.Water);
        MonsterCard newCard2 = new MonsterCard(2, "Shining Knight", Card.MonsterType.Knight, Card.ElementType.Normal, 2, Card.MonsterType.Water, Card.ElementType.Fire);
        MonsterCard newCard3 = new MonsterCard(4, "Goblin", Card.MonsterType.Goblin, Card.ElementType.Normal, 3, Card.MonsterType.Dragon, Card.ElementType.Fire);
        MonsterCard newCard4 = new MonsterCard(7, "Kraken", Card.MonsterType.Kraken, Card.ElementType.Water, 4, Card.MonsterType.None, Card.ElementType.Normal);
        MonsterCard newCard5 = new MonsterCard(4, "Fire Elves", Card.MonsterType.FireElves, Card.ElementType.Fire, 5, Card.MonsterType.None, Card.ElementType.Water);
        MonsterCard newCard6 = new MonsterCard(4, "Ork", Card.MonsterType.Ork, Card.ElementType.Normal, 6, Card.MonsterType.Wizzard, Card.ElementType.Fire);
        MonsterCard newCard7 = new MonsterCard(8, "Water Spell", Card.MonsterType.Spell, Card.ElementType.Water, 7, Card.MonsterType.Kraken, Card.ElementType.Normal);
        MonsterCard newCard8 = new MonsterCard(3, "Fire Spell", Card.MonsterType.Spell, Card.ElementType.Fire, 8, Card.MonsterType.Kraken, Card.ElementType.Water);
        MonsterCard newCard9 = new MonsterCard(3, "Normal Spell", Card.MonsterType.Spell, Card.ElementType.Normal, 9, Card.MonsterType.Kraken, Card.ElementType.Fire);
        MonsterCard newCard10 = new MonsterCard(7, "Fire Wizzard", Card.MonsterType.Wizzard, Card.ElementType.Fire, 10, Card.MonsterType.None, Card.ElementType.Water);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonsterCardGame.Classes;
using MonsterCardGame.Classes;

namespace MonsterCardGame
{
    class CardDB
    {
        public CardDB(User Player)
        {
            Player.PlayerCardCollection.AddMonsterCardToStack(newCard1);
            Player.PlayerCardCollection.AddMonsterCardToStack(newCard2);
            Player.PlayerCardCollection.AddMonsterCardToStack(newCard3);
            Player.PlayerCardCollection.AddMonsterCardToStack(newCard4);
            Player.PlayerCardCollection.AddMonsterCardToStack(newCard5);
            Player.PlayerCardCollection.AddMonsterCardToStack(newCard6);
            Player.PlayerCardCollection.AddMonsterCardToStack(newCard7);
            Player.PlayerCardCollection.AddMonsterCardToStack(newCard8);
            Player.PlayerCardCollection.AddMonsterCardToStack(newCard9);
            Player.PlayerCardCollection.AddMonsterCardToStack(newCard10);
            Player.PlayerDeck.CardDeck.Add(newCard1);
            Player.PlayerDeck.CardDeck.Add(newCard2);
            Player.PlayerDeck.CardDeck.Add(newCard3);
            Player.PlayerDeck.CardDeck.Add(newCard4);
            Player.PlayerDeck.CardDeck.Add(newCard5);
        }
        MonsterCard newCard1 = new MonsterCard(5, "Fire Dragon", Card.MonsterType.Dragon, Card.ElementType.Fire, 1);
        MonsterCard newCard2 = new MonsterCard(3, "Shining Knight", Card.MonsterType.Knight, Card.ElementType.Normal, 2);
        MonsterCard newCard3 = new MonsterCard(4, "Goblin", Card.MonsterType.Goblin, Card.ElementType.Normal, 3);
        MonsterCard newCard4 = new MonsterCard(7, "Kraken", Card.MonsterType.Kraken, Card.ElementType.Water, 4);
        MonsterCard newCard5 = new MonsterCard(4, "FireElves", Card.MonsterType.FireElves, Card.ElementType.Fire, 5);
        MonsterCard newCard6 = new MonsterCard(4, "Ork", Card.MonsterType.Ork, Card.ElementType.Normal, 6);
        MonsterCard newCard7 = new MonsterCard(3, "Water Spell", Card.MonsterType.Spell, Card.ElementType.Water, 7);
        MonsterCard newCard8 = new MonsterCard(3, "Fire Spell", Card.MonsterType.Spell, Card.ElementType.Fire, 8);
        MonsterCard newCard9 = new MonsterCard(3, "Normal Spell", Card.MonsterType.Spell, Card.ElementType.Normal, 9);
        MonsterCard newCard10 = new MonsterCard(7, "Wizzard Fire", Card.MonsterType.Wizzard, Card.ElementType.Fire, 10);


        public MonsterCard RandomMonsterCard()
        {
            MonsterCard RandomCard = new MonsterCard(1, "Fire Dragon", Card.MonsterType.Dragon, Card.ElementType.Fire, 11);
            return RandomCard;
        }
    }
}

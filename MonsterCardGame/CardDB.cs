using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonsterCardGame
{
    class CardDB
    {
        public CardDB(User Player1)
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
            Player1.PlayerCardCollection.AddMonsterCardToStack(newCard11);
            Player1.PlayerCardCollection.AddMonsterCardToStack(newCard12);
            Player1.PlayerCardCollection.AddMonsterCardToStack(newCard13);
            Player1.PlayerCardCollection.AddMonsterCardToStack(newCard14);
            Player1.PlayerCardCollection.AddMonsterCardToStack(newCard15);
            Player1.PlayerCardCollection.AddMonsterCardToStack(newCard16);
            Player1.PlayerCardCollection.AddMonsterCardToStack(newCard17);
            Player1.PlayerCardCollection.AddMonsterCardToStack(newCard18);
            Player1.PlayerCardCollection.AddMonsterCardToStack(newCard19);
            Player1.PlayerCardCollection.AddMonsterCardToStack(newCard20);
            Player1.PlayerCardCollection.AddMonsterCardToStack(newCard21);
            Player1.PlayerDeck.AddCardToDeck(newCard8);
            Player1.PlayerDeck.AddCardToDeck(newCard9);
            Player1.PlayerDeck.AddCardToDeck(newCard7);
            Player1.PlayerDeck.AddCardToDeck(newCard11);
        }

        MonsterCard newCard1 = new MonsterCard(7, "Fire Dragon", Card.MonsterType.Dragon, Card.ElementType.Fire, Card.MonsterType.FireElves, Card.ElementType.Water, 3);
        MonsterCard newCard2 = new MonsterCard(8, "Shining Knight", Card.MonsterType.Knight, Card.ElementType.Normal, Card.MonsterType.WaterSpell, Card.ElementType.Fire, 4);
        MonsterCard newCard3 = new MonsterCard(4, "Goblin", Card.MonsterType.Goblin, Card.ElementType.Normal, Card.MonsterType.Dragon, Card.ElementType.Fire, 5);
        MonsterCard newCard4 = new MonsterCard(7, "Kraken", Card.MonsterType.Kraken, Card.ElementType.Water, Card.MonsterType.None, Card.ElementType.Normal, 6);
        MonsterCard newCard5 = new MonsterCard(4, "Fire Elves", Card.MonsterType.FireElves, Card.ElementType.Fire, Card.MonsterType.None, Card.ElementType.Water, 7);
        MonsterCard newCard6 = new MonsterCard(4, "Ork", Card.MonsterType.Ork, Card.ElementType.Normal, Card.MonsterType.Wizzard, Card.ElementType.Fire, 8);
        MonsterCard newCard7 = new MonsterCard(1, "Water Spell", Card.MonsterType.WaterSpell, Card.ElementType.Water, Card.MonsterType.Kraken, Card.ElementType.Normal, 9);
        MonsterCard newCard8 = new MonsterCard(1, "Fire Spell", Card.MonsterType.FireSpell, Card.ElementType.Fire, Card.MonsterType.Kraken, Card.ElementType.Water, 10);
        MonsterCard newCard9 = new MonsterCard(1, "Normal Spell", Card.MonsterType.NormalSpell, Card.ElementType.Normal, Card.MonsterType.Kraken, Card.ElementType.Fire, 11);
        MonsterCard newCard10 = new MonsterCard(8, "Fire Wizzard", Card.MonsterType.Wizzard, Card.ElementType.Fire, Card.MonsterType.None, Card.ElementType.Water, 12);
        MonsterCard newCard11 = new MonsterCard(1, "Tsunami", Card.MonsterType.WaterSpell, Card.ElementType.Water, Card.MonsterType.Kraken, Card.ElementType.Normal, 13);
        MonsterCard newCard12 = new MonsterCard(7, "Firestorm", Card.MonsterType.FireSpell, Card.ElementType.Fire, Card.MonsterType.Kraken, Card.ElementType.Water, 14);
        MonsterCard newCard13 = new MonsterCard(7, "Sky Bolder", Card.MonsterType.NormalSpell, Card.ElementType.Normal, Card.MonsterType.Kraken, Card.ElementType.Fire, 15);
        MonsterCard newCard14 = new MonsterCard(4, "Waterbording", Card.MonsterType.WaterSpell, Card.ElementType.Water, Card.MonsterType.Kraken, Card.ElementType.Normal, 16);
        MonsterCard newCard15 = new MonsterCard(1, "Single Drop of Water", Card.MonsterType.WaterSpell, Card.ElementType.Water, Card.MonsterType.Kraken, Card.ElementType.Normal, 17);
        MonsterCard newCard16 = new MonsterCard(9, "Lord Groblin", Card.MonsterType.Goblin, Card.ElementType.Normal, Card.MonsterType.Dragon, Card.ElementType.Fire, 18);
        MonsterCard newCard17 = new MonsterCard(1, "Born Chiller", Card.MonsterType.Wizzard, Card.ElementType.Normal, Card.MonsterType.None, Card.ElementType.Fire, 19);
        MonsterCard newCard18 = new MonsterCard(7, "Blaze Atronarch", Card.MonsterType.Spirit, Card.ElementType.Fire, Card.MonsterType.WaterSpell, Card.ElementType.Water, 20);
        MonsterCard newCard19 = new MonsterCard(8, "Haunted Atronarch", Card.MonsterType.Spirit, Card.ElementType.Normal, Card.MonsterType.Wizzard, Card.ElementType.Fire, 21);
        MonsterCard newCard20 = new MonsterCard(7, "Drowned Atronarch", Card.MonsterType.Spirit, Card.ElementType.Water, Card.MonsterType.Wizzard, Card.ElementType.Normal, 22);
        MonsterCard newCard21 = new MonsterCard(11, "Attack Helicopter", Card.MonsterType.Machine, Card.ElementType.Normal, Card.MonsterType.Spirit, Card.ElementType.Fire, 23);
        MonsterCard FireMachine = new MonsterCard(4, "TestFireMonsterCard", Card.MonsterType.Machine, Card.ElementType.Fire, Card.MonsterType.Machine, Card.ElementType.Water, 25);
        MonsterCard WaterSpell = new MonsterCard(4, "TestWaterSpellCard", Card.MonsterType.WaterSpell, Card.ElementType.Water, Card.MonsterType.Kraken, Card.ElementType.Fire, 26);
    }
}

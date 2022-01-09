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
            //Player1.PlayerCardCollection.AddMonsterCardToStack(newCard1);
            //Player1.PlayerCardCollection.AddMonsterCardToStack(newCard2);
            //Player1.PlayerCardCollection.AddMonsterCardToStack(newCard3);
            //Player1.PlayerCardCollection.AddMonsterCardToStack(newCard4);
            //Player1.PlayerCardCollection.AddMonsterCardToStack(newCard5);
            //Player1.PlayerCardCollection.AddMonsterCardToStack(newCard6);
            //Player1.PlayerCardCollection.AddMonsterCardToStack(newCard7);
            //Player1.PlayerCardCollection.AddMonsterCardToStack(newCard8);
            //Player1.PlayerCardCollection.AddMonsterCardToStack(newCard9);
            //Player1.PlayerCardCollection.AddMonsterCardToStack(newCard10);
            //Player1.PlayerCardCollection.AddMonsterCardToStack(newCard11);
            //Player1.PlayerCardCollection.AddMonsterCardToStack(newCard12);
            //Player1.PlayerCardCollection.AddMonsterCardToStack(newCard13);
            //Player1.PlayerCardCollection.AddMonsterCardToStack(newCard14);
            //Player1.PlayerCardCollection.AddMonsterCardToStack(newCard15);
            //Player1.PlayerCardCollection.AddMonsterCardToStack(newCard16);
            //Player1.PlayerCardCollection.AddMonsterCardToStack(newCard17);
            //Player1.PlayerCardCollection.AddMonsterCardToStack(newCard18);
            //Player1.PlayerCardCollection.AddMonsterCardToStack(newCard19);
            //Player1.PlayerCardCollection.AddMonsterCardToStack(newCard20);
            //Player1.PlayerCardCollection.AddMonsterCardToStack(newCard21);
            Player1.PlayerDeck.AddCardToDeck(newCard1);
            Player1.PlayerDeck.AddCardToDeck(newCard2);
            Player1.PlayerDeck.AddCardToDeck(newCard3);
            Player1.PlayerDeck.AddCardToDeck(newCard4);
            //Player2.PlayerCardCollection.AddMonsterCardToStack(newCard1);
            //Player2.PlayerCardCollection.AddMonsterCardToStack(newCard2);
            //Player2.PlayerCardCollection.AddMonsterCardToStack(newCard3);
            //Player2.PlayerCardCollection.AddMonsterCardToStack(newCard4);
            //Player2.PlayerCardCollection.AddMonsterCardToStack(newCard5);
            //Player2.PlayerCardCollection.AddMonsterCardToStack(newCard6);
            //Player2.PlayerCardCollection.AddMonsterCardToStack(newCard7);
            //Player2.PlayerCardCollection.AddMonsterCardToStack(newCard8);
            //Player2.PlayerCardCollection.AddMonsterCardToStack(newCard9);
            //Player2.PlayerCardCollection.AddMonsterCardToStack(newCard10);

        }
        MonsterCard newCard1 = new MonsterCard(7, "Fire Dragon", Card.MonsterType.Dragon, Card.ElementType.Fire, Card.MonsterType.FireElves, Card.ElementType.Water, 3);
        MonsterCard newCard2 = new MonsterCard(2, "Shining Knight", Card.MonsterType.Knight, Card.ElementType.Normal, Card.MonsterType.Water, Card.ElementType.Fire, 4);
        MonsterCard newCard3 = new MonsterCard(4, "Goblin", Card.MonsterType.Goblin, Card.ElementType.Normal, Card.MonsterType.Dragon, Card.ElementType.Fire, 5);
        MonsterCard newCard4 = new MonsterCard(7, "Kraken", Card.MonsterType.Kraken, Card.ElementType.Water, Card.MonsterType.None, Card.ElementType.Normal, 6);
        MonsterCard newCard5 = new MonsterCard(4, "Fire Elves", Card.MonsterType.FireElves, Card.ElementType.Fire, Card.MonsterType.None, Card.ElementType.Water, 7);
        MonsterCard newCard6 = new MonsterCard(4, "Ork", Card.MonsterType.Ork, Card.ElementType.Normal, Card.MonsterType.Wizzard, Card.ElementType.Fire, 8);
        MonsterCard newCard7 = new MonsterCard(8, "Water Spell", Card.MonsterType.Spell, Card.ElementType.Water, Card.MonsterType.Kraken, Card.ElementType.Normal, 9);
        MonsterCard newCard8 = new MonsterCard(3, "Fire Spell", Card.MonsterType.Spell, Card.ElementType.Fire, Card.MonsterType.Kraken, Card.ElementType.Water, 10);
        MonsterCard newCard9 = new MonsterCard(3, "Normal Spell", Card.MonsterType.Spell, Card.ElementType.Normal, Card.MonsterType.Kraken, Card.ElementType.Fire, 11);
        MonsterCard newCard10 = new MonsterCard(8, "Fire Wizzard", Card.MonsterType.Wizzard, Card.ElementType.Fire, Card.MonsterType.None, Card.ElementType.Water, 12);
        MonsterCard newCard11 = new MonsterCard(6, "Tsunami", Card.MonsterType.Spell, Card.ElementType.Water, Card.MonsterType.Kraken, Card.ElementType.Normal, 13);
        MonsterCard newCard12 = new MonsterCard(7, "Firestorm", Card.MonsterType.Spell, Card.ElementType.Fire, Card.MonsterType.Kraken, Card.ElementType.Water, 14);
        MonsterCard newCard13 = new MonsterCard(7, "Sky Bolder", Card.MonsterType.Spell, Card.ElementType.Normal, Card.MonsterType.Kraken, Card.ElementType.Fire, 15);
        MonsterCard newCard14 = new MonsterCard(4, "Waterbording", Card.MonsterType.Spell, Card.ElementType.Water, Card.MonsterType.Kraken, Card.ElementType.Normal, 16);
        MonsterCard newCard15 = new MonsterCard(1, "Single Drop of Water", Card.MonsterType.Spell, Card.ElementType.Water, Card.MonsterType.Kraken, Card.ElementType.Normal, 17);
        MonsterCard newCard16 = new MonsterCard(9, "Lord Groblin", Card.MonsterType.Goblin, Card.ElementType.Normal, Card.MonsterType.Dragon, Card.ElementType.Fire, 18);
        MonsterCard newCard17 = new MonsterCard(1, "Born Chiller", Card.MonsterType.Wizzard, Card.ElementType.Normal, Card.MonsterType.None, Card.ElementType.Fire, 19);
        MonsterCard newCard18 = new MonsterCard(7, "Blaze Atronarch", Card.MonsterType.Spirit, Card.ElementType.Fire, Card.MonsterType.Spell, Card.ElementType.Water, 20);
        MonsterCard newCard19 = new MonsterCard(8, "Haunted Atronarch", Card.MonsterType.Spirit, Card.ElementType.Normal, Card.MonsterType.Spell, Card.ElementType.Fire, 21);
        MonsterCard newCard20 = new MonsterCard(7, "Drowned Atronarch", Card.MonsterType.Spirit, Card.ElementType.Water, Card.MonsterType.Spell, Card.ElementType.Normal, 22);
        MonsterCard newCard21 = new MonsterCard(11, "Attack Helicopter", Card.MonsterType.Machine, Card.ElementType.Normal, Card.MonsterType.Spirit, Card.ElementType.Fire, 23);
        MonsterCard FireMachine = new MonsterCard(4, "TestFireMonsterCard", Card.MonsterType.Machine, Card.ElementType.Fire, Card.MonsterType.Machine, Card.ElementType.Water, 25);
        MonsterCard WaterSpell = new MonsterCard(4, "TestWaterSpellCard", Card.MonsterType.Spell, Card.ElementType.Water, Card.MonsterType.Spell, Card.ElementType.Fire, 26);


        //MonsterCard newCard22 = new MonsterCard(7, "Sky Bolder", Card.MonsterType.Spell, Card.ElementType.Normal, Card.MonsterType.Kraken, Card.ElementType.Fire);
        //MonsterCard newCard23 = new MonsterCard(7, "Sky Bolder", Card.MonsterType.Spell, Card.ElementType.Normal, Card.MonsterType.Kraken, Card.ElementType.Fire);
        //MonsterCard newCard24 = new MonsterCard(7, "Sky Bolder", Card.MonsterType.Spell, Card.ElementType.Normal, Card.MonsterType.Kraken, Card.ElementType.Fire);
        //MonsterCard newCard25 = new MonsterCard(7, "Sky Bolder", Card.MonsterType.Spell, Card.ElementType.Normal, Card.MonsterType.Kraken, Card.ElementType.Fire);
        //MonsterCard newCard26 = new MonsterCard(7, "Sky Bolder", Card.MonsterType.Spell, Card.ElementType.Normal, Card.MonsterType.Kraken, Card.ElementType.Fire);
        //MonsterCard newCard27 = new MonsterCard(7, "Sky Bolder", Card.MonsterType.Spell, Card.ElementType.Normal, Card.MonsterType.Kraken, Card.ElementType.Fire);
        //MonsterCard newCard28 = new MonsterCard(7, "Sky Bolder", Card.MonsterType.Spell, Card.ElementType.Normal, Card.MonsterType.Kraken, Card.ElementType.Fire);
        //MonsterCard newCard29 = new MonsterCard(7, "Sky Bolder", Card.MonsterType.Spell, Card.ElementType.Normal, Card.MonsterType.Kraken, Card.ElementType.Fire);
        //MonsterCard newCard30 = new MonsterCard(7, "Sky Bolder", Card.MonsterType.Spell, Card.ElementType.Normal, Card.MonsterType.Kraken, Card.ElementType.Fire);
        //MonsterCard newCard31 = new MonsterCard(7, "Sky Bolder", Card.MonsterType.Spell, Card.ElementType.Normal, Card.MonsterType.Kraken, Card.ElementType.Fire);
        //MonsterCard newCard32 = new MonsterCard(7, "Sky Bolder", Card.MonsterType.Spell, Card.ElementType.Normal, Card.MonsterType.Kraken, Card.ElementType.Fire);
        //MonsterCard newCard33 = new MonsterCard(7, "Sky Bolder", Card.MonsterType.Spell, Card.ElementType.Normal, Card.MonsterType.Kraken, Card.ElementType.Fire);
        //MonsterCard newCard34 = new MonsterCard(7, "Sky Bolder", Card.MonsterType.Spell, Card.ElementType.Normal, Card.MonsterType.Kraken, Card.ElementType.Fire);
        //MonsterCard newCard35 = new MonsterCard(7, "Sky Bolder", Card.MonsterType.Spell, Card.ElementType.Normal, Card.MonsterType.Kraken, Card.ElementType.Fire);
        //MonsterCard newCard36 = new MonsterCard(7, "Sky Bolder", Card.MonsterType.Spell, Card.ElementType.Normal, Card.MonsterType.Kraken, Card.ElementType.Fire);
        //MonsterCard newCard37 = new MonsterCard(7, "Sky Bolder", Card.MonsterType.Spell, Card.ElementType.Normal, Card.MonsterType.Kraken, Card.ElementType.Fire);
        //MonsterCard newCard38 = new MonsterCard(7, "Sky Bolder", Card.MonsterType.Spell, Card.ElementType.Normal, Card.MonsterType.Kraken, Card.ElementType.Fire);
        //MonsterCard newCard39 = new MonsterCard(7, "Sky Bolder", Card.MonsterType.Spell, Card.ElementType.Normal, Card.MonsterType.Kraken, Card.ElementType.Fire);
        //MonsterCard newCard40 = new MonsterCard(7, "Sky Bolder", Card.MonsterType.Spell, Card.ElementType.Normal, Card.MonsterType.Kraken, Card.ElementType.Fire);
        //MonsterCard newCard41 = new MonsterCard(7, "Sky Bolder", Card.MonsterType.Spell, Card.ElementType.Normal, Card.MonsterType.Kraken, Card.ElementType.Fire);
        //MonsterCard newCard42 = new MonsterCard(7, "Sky Bolder", Card.MonsterType.Spell, Card.ElementType.Normal, Card.MonsterType.Kraken, Card.ElementType.Fire);
    }
}

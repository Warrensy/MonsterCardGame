using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonsterCardGame.Classes;

namespace MonsterCardGame
{
    class Logic
    {
        int multiplier = 2;
        int CardUserDmg = 0;
        int CardEnemyDmg = 0;
        int Deckbeginning = 0;
        bool Card1Weakness = false;
        bool Card2Weakness = false;

        public void Battle(User Player1, User Player2)
        {
            int RoundCount = 0;
            int DeckSizeP1 = 0;
            int DeckSizeP2 = 0;
            MonsterCard LosingCard;
            Deck Player1Deck = Player1.PlayerDeck.ShuffleDeck();
            Deck Player2Deck = Player2.PlayerDeck.ShuffleDeck();
            while(Player1Deck.CardDeck.Count > 0 && Player2Deck.CardDeck.Count > 0)
            {
                DeckSizeP1 = Player1Deck.CardDeck.Count;
                DeckSizeP2 = Player2Deck.CardDeck.Count;
                if (RoundCount >= 100)
                {
                    Console.WriteLine($"Cards left Player1: {Player1Deck.CardDeck.Count} \nCards left Player2: {Player2Deck.CardDeck.Count}");
                    Console.WriteLine("No Winner could be determent. Game Ended.");
                    break;
                }
                Console.WriteLine();
                Console.WriteLine(Player1Deck.CardDeck[DeckSizeP1 - 1]._CardName + " V.S " + Player2Deck.CardDeck[DeckSizeP2 - 1]._CardName);
                
                LosingCard = Fight(Player1Deck.CardDeck[DeckSizeP1 - 1]
                                , Player2Deck.CardDeck[DeckSizeP2 - 1]);
                Console.Write(Player1Deck.CardDeck[DeckSizeP1 - 1]._CardName + ": " + CardUserDmg + " dmg -- ");
                Console.Write(Player2Deck.CardDeck[DeckSizeP2 - 1]._CardName + ": " + CardEnemyDmg + " dmg\n");
                
                if(LosingCard == null) 
                {
                    MonsterCard swap = Player1Deck.CardDeck[DeckSizeP1 - 1];
                    Player1Deck.CardDeck.RemoveAt(DeckSizeP1 - 1);
                    Player1Deck.CardDeck.Insert(Deckbeginning, swap);
                    swap = Player2Deck.CardDeck[DeckSizeP2 - 1];
                    Player2Deck.CardDeck.RemoveAt(DeckSizeP2 - 1);
                    Player2Deck.CardDeck.Insert(Deckbeginning, swap);
                    Console.WriteLine("-Draw- No cards exchanged");
                }
                else if(LosingCard == Player1Deck.CardDeck[DeckSizeP1 - 1])
                {
                    if(Card1Weakness)
                    {
                        Console.WriteLine($"{Player1Deck.CardDeck[DeckSizeP1 - 1]._CardName} weakness has been triggerd.");
                        Card1Weakness = false;
                    }
                    Console.WriteLine($"{Player2Deck.CardDeck[DeckSizeP2 - 1]._CardName} wins. {Player1Deck.CardDeck[DeckSizeP1 - 1]._CardName} gets added to enemy deck");
                    Player1Deck.CardDeck.RemoveAt(DeckSizeP1 - 1);
                    Player2Deck.CardDeck.Insert(Deckbeginning, LosingCard);
                }
                else if(LosingCard == Player2Deck.CardDeck[DeckSizeP2 - 1])
                {
                    if (Card2Weakness)
                    {
                        Console.WriteLine($"{Player2Deck.CardDeck[DeckSizeP2 - 1]._CardName} weakness has been triggerd.");
                        Card2Weakness = false;
                    }
                    Console.WriteLine($"{Player1Deck.CardDeck[DeckSizeP1 - 1]._CardName} wins. {Player2Deck.CardDeck[DeckSizeP2 - 1]._CardName} gets added to enemy deck");
                    Player2Deck.CardDeck.RemoveAt(DeckSizeP2 - 1);
                    Player1Deck.CardDeck.Insert(Deckbeginning, LosingCard);
                }
                RoundCount++;
            }
            if (DeckSizeP1 == 0)
            {

            }
            if(DeckSizeP2 == 0)
            {

            }
        }

        public MonsterCard Fight(MonsterCard Card1, MonsterCard Card2)
        {
            bool SpellFight = false;
            CardUserDmg = Card1._dmg;
            CardEnemyDmg = Card2._dmg;

            SpellFight = CheckForSpell(Card1._Type, Card2._Type);

            if (Card1._Weakness == Card2._Type) { Card1Weakness = true; return Card1; }
            if (Card2._Weakness == Card1._Type) { Card2Weakness = true; return Card2; }

            if(SpellFight)
            {
                CalcDmg(Card1, Card2);
            }
            if(CardUserDmg > CardEnemyDmg) return Card2;
            if(CardUserDmg < CardEnemyDmg) return Card1;
            return null;
        }
        bool CheckForSpell(Card.MonsterType Type1, Card.MonsterType Type2)
        {
            if (Type1 == Card.MonsterType.Spell || Type2 == Card.MonsterType.Spell)
            {
                return true;
            }
            return false;
        }
        void CalcDmg(MonsterCard Card1, MonsterCard Card2)
        {
            if(Card1._Element == Card.ElementType.Fire)
            {
                switch (Card2._Element)
                {
                    case Card.ElementType.Water:
                        CardUserDmg = Card1._dmg / multiplier;
                        CardEnemyDmg = Card2._dmg * multiplier;
                        break;
                    case Card.ElementType.Fire:
                        CardUserDmg = Card1._dmg;
                        CardEnemyDmg = Card2._dmg;
                        break;
                    case Card.ElementType.Normal:
                        CardUserDmg = Card1._dmg * multiplier;
                        CardEnemyDmg = Card2._dmg / multiplier;
                        break;

                }
            }
            if (Card1._Element == Card.ElementType.Water)
            {
                switch (Card2._Element)
                {
                    case Card.ElementType.Water:
                        CardUserDmg = Card1._dmg;
                        CardEnemyDmg = Card2._dmg;
                        break;
                    case Card.ElementType.Fire:
                        CardUserDmg = Card1._dmg * multiplier;
                        CardEnemyDmg = Card2._dmg / multiplier;
                        break;
                    case Card.ElementType.Normal:
                        CardUserDmg = Card1._dmg / multiplier;
                        CardEnemyDmg = Card2._dmg * multiplier;
                        break;

                }
            }
            if (Card1._Element == Card.ElementType.Normal)
            {
                switch (Card2._Element)
                {
                    case Card.ElementType.Water:
                        CardUserDmg = Card1._dmg * multiplier;
                        CardEnemyDmg = Card2._dmg / multiplier;
                        break;
                    case Card.ElementType.Fire:
                        CardUserDmg = Card1._dmg / multiplier;
                        CardEnemyDmg = Card2._dmg * multiplier;
                        break;
                    case Card.ElementType.Normal:
                        CardUserDmg = Card1._dmg;
                        CardEnemyDmg = Card2._dmg;
                        break;

                }
            }
        }
    }
}

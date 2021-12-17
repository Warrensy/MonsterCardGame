using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonsterCardGame
{
    class Logic
    {
        int CardUserDmg = 0;
        int CardEnemyDmg = 0;
        int Deckbeginning = 0;
        bool Card1Weakness = false;
        bool Card2Weakness = false;

        public int Battle(Deck Player1, Deck Player2)
        {
            int won = -1;
            int RoundCount = 0;
            int DeckSizeP1 = 0;
            int DeckSizeP2 = 0;
            MonsterCard LosingCard;
            Deck Player1Deck = Player1.ShuffleDeck();
            Deck Player2Deck = Player2.ShuffleDeck();
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
                Console.WriteLine(Player1Deck.CardDeck.Last()._CardName + " V.S " + Player2Deck.CardDeck.Last()._CardName);
                
                LosingCard = Fight(Player1Deck.CardDeck.Last()
                                , Player2Deck.CardDeck.Last());
                Console.Write(Player1Deck.CardDeck.Last()._CardName + ": " + CardUserDmg + " dmg -- ");
                Console.Write(Player2Deck.CardDeck.Last()._CardName + ": " + CardEnemyDmg + " dmg\n");
                
                if(LosingCard == null) 
                {
                    MonsterCard swap = Player1Deck.CardDeck.Last();
                    Player1Deck.CardDeck.RemoveAt(DeckSizeP1 - 1);
                    Player1Deck.CardDeck.Insert(Deckbeginning, swap);
                    swap = Player2Deck.CardDeck.Last();
                    Player2Deck.CardDeck.RemoveAt(DeckSizeP2 - 1);
                    Player2Deck.CardDeck.Insert(Deckbeginning, swap);
                    Console.WriteLine("-Draw- No cards exchanged");
                }
                else if(LosingCard == Player1Deck.CardDeck.Last())
                {
                    if(Card1Weakness)
                    {
                        Console.WriteLine($"{Player1Deck.CardDeck.Last()._CardName} weakness has been triggerd.");
                        Card1Weakness = false;
                    }
                    Console.WriteLine($"{Player2Deck.CardDeck.Last()._CardName} wins. {Player1Deck.CardDeck.Last()._CardName} gets added to enemy deck");
                    Player1Deck.CardDeck.RemoveAt(DeckSizeP1 - 1);
                    Player2Deck.CardDeck.Insert(Deckbeginning, LosingCard);
                }
                else if(LosingCard == Player2Deck.CardDeck.Last())
                {
                    if (Card2Weakness)
                    {
                        Console.WriteLine($"{Player2Deck.CardDeck.Last()._CardName} weakness has been triggerd.");
                        Card2Weakness = false;
                    }
                    Console.WriteLine($"{Player1Deck.CardDeck.Last()._CardName} wins. {Player2Deck.CardDeck.Last()._CardName} gets added to enemy deck");
                    Player2Deck.CardDeck.RemoveAt(DeckSizeP2 - 1);
                    Player1Deck.CardDeck.Insert(Deckbeginning, LosingCard);
                }
                RoundCount++;
            }
            DeckSizeP1 = Player1Deck.CardDeck.Count;
            DeckSizeP2 = Player2Deck.CardDeck.Count;
            if (DeckSizeP1 <= 0)
            {
                Console.WriteLine("\nOpponent won.");
                won = 0;
            }
            if(DeckSizeP2 <= 0)
            {
                Console.WriteLine("\nYou 1 won.");
                won = 1;
            }
            return won;
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
                CheckElements(ref Card1, ref Card2);
            }
            if(Card1.Attack() > Card2.Attack()) return Card2;
            if(Card1.Attack() < Card2.Attack()) return Card1;
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

        void CheckElements(ref MonsterCard Card1, ref MonsterCard Card2)
        {
            if(Card1._Element == Card.ElementType.Fire && Card2._Element == Card.ElementType.Water) 
            { 
                Card1._Nerf = true; 
                Card2._Boost = true; 
            }
            if (Card1._Element == Card.ElementType.Water && Card2._Element == Card.ElementType.Normal)
            {
                Card1._Nerf = true;
                Card2._Boost = true;
            }
            if (Card1._Element == Card.ElementType.Normal && Card2._Element == Card.ElementType.Fire)
            {
                Card1._Nerf = true;
                Card2._Boost = true;
            }
        }
        
    }
}

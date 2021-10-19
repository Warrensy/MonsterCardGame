using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonsterCardGame.Classes;
using MonsterCardGame.Classes;

namespace MonsterCardGame
{
    class Logic
    {
        int multiplier = 2;
        int CardUserDmg = 0;
        int CardEnemyDmg = 0;
        int Deckbeginning = 0;

        public void Battle(User Player1, User Player2)
        {
            MonsterCard LosingCard;
            while(Player1.PlayerDeck.CardDeck.Count > 0 && Player2.PlayerDeck.CardDeck.Count > 0)
            {
                
                Console.Write(Player1.PlayerDeck.CardDeck[Player1.PlayerDeck.CardDeck.Count - 1]._CardName);
                Console.Write(" V.S ");
                Console.Write(Player2.PlayerDeck.CardDeck[Deckbeginning]._CardName);
                Console.WriteLine();
                
                LosingCard = Fight(Player1.PlayerDeck.CardDeck[Player1.PlayerDeck.CardDeck.Count - 1]
                                , Player2.PlayerDeck.CardDeck[Deckbeginning]);
                
                if(LosingCard == null) 
                {

                }
                else if(LosingCard == Player1.PlayerDeck.CardDeck[Player1.PlayerDeck.CardDeck.Count - 1])
                {
                    Player1.PlayerDeck.CardDeck.RemoveAt(Player1.PlayerDeck.CardDeck.Count - 1);
                    Player2.PlayerDeck.CardDeck.Insert(Deckbeginning, LosingCard);
                }
                else if(LosingCard == Player2.PlayerDeck.CardDeck[Player2.PlayerDeck.CardDeck.Count - 1])
                {
                    Player2.PlayerDeck.CardDeck.RemoveAt(Player2.PlayerDeck.CardDeck.Count - 1);
                    Player1.PlayerDeck.CardDeck.Insert(Deckbeginning, LosingCard);
                }
            }
        }

        public MonsterCard Fight(MonsterCard Card1, MonsterCard Card2)
        {
            bool SpellFight = false;
            CardUserDmg = Card1._dmg;
            CardEnemyDmg = Card2._dmg;

            SpellFight = CheckForSpell(Card1._Type, Card2._Type);

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
                        break;
                    case Card.ElementType.Fire:
                        CardUserDmg = Card1._dmg;
                        break;
                    case Card.ElementType.Normal:
                        CardUserDmg = Card1._dmg * multiplier;
                        break;

                }
            }
            if (Card1._Element == Card.ElementType.Water)
            {
                switch (Card2._Element)
                {
                    case Card.ElementType.Water:
                        CardUserDmg = Card1._dmg;
                        break;
                    case Card.ElementType.Fire:
                        CardUserDmg = Card1._dmg * multiplier;
                        break;
                    case Card.ElementType.Normal:
                        CardUserDmg = Card1._dmg / multiplier;
                        break;

                }
            }
            if (Card1._Element == Card.ElementType.Normal)
            {
                switch (Card2._Element)
                {
                    case Card.ElementType.Water:
                        CardUserDmg = Card1._dmg * multiplier;
                        break;
                    case Card.ElementType.Fire:
                        CardUserDmg = Card1._dmg / multiplier;
                        break;
                    case Card.ElementType.Normal:
                        CardUserDmg = Card1._dmg;
                        break;

                }
            }
        }
    }
}

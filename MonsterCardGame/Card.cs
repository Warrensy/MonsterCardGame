using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonsterCardGame.Interfaces;

namespace MonsterCardGame.Classes
{
    abstract class Card : ICard
    {
        public static int _CardID { get; set; }
        public MonsterType _Type { get; set; }
        public ElementType _Element { get; set; }
        public int _dmg { get; set; }
        public string _CardName { get; set; }
        public abstract void PrintStats();
        public abstract void CardEffect();
        public enum MonsterType
        {
            Goblin,
            Wizzard,
            Dragon,
            Ork,
            Knight,
            Kraken,
            FireElves,
            Spell
        }
        public enum ElementType
        {
            Water,
            Fire,
            Normal
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonsterCardGame
{
    abstract class Card : ICard
    {
        public int _CardID { get; set; }
        public bool _Boost { get; set; }
        public bool _Nerf { get; set; }
        public MonsterType _Type { get; set; }
        public ElementType _Element { get; set; }
        public ElementType _ElementWeakness { get; set; }
        public int _dmg { get; set; }
        public string _CardName { get; set; }
        public MonsterType _Weakness { get; set; }
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
            Spell,
            Water,
            Fire,
            None,
            Spirit,
            Machine
        }
        public enum ElementType
        {
            Water,
            Fire,
            Normal
        }
    }
}

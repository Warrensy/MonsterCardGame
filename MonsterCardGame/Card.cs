using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonsterCardGame
{
    public abstract class Card : ICard
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
        public enum MonsterType
        {
            Goblin,
            Wizzard,
            Spell,
            Dragon,
            Ork,
            Knight,
            Kraken,
            FireElves,
            WaterSpell,
            FireSpell,
            None,
            Spirit,
            Machine,
            NormalSpell
        }
        public enum ElementType
        {
            Water,
            Fire,
            Normal
        }
    }
}

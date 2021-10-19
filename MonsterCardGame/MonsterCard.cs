using System;
using MonsterCardGame.Classes;

namespace MonsterCardGame.Classes
{
    class MonsterCard : Card
    {
        public MonsterCard(int dmg, string Name, MonsterType Type, ElementType Element, int ID)
        {
            _dmg = dmg;
            _CardName = Name;
            _Element = Element;
            _Type = Type;
            _CardID = ID;
        }
        public override void CardEffect()
        {
            throw new System.NotImplementedException();
        }

        public override void PrintStats()
        {
            Console.WriteLine($"Name: {_CardName}\nDamage: {_dmg}\nCard Type: {_Type}\nElement: {_Element}");
        }
    }
}
